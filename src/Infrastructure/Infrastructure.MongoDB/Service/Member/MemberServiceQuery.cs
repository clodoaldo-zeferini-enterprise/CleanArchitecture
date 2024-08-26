using Application.DTO.Request;
using Application.DTO.Response;
using Infrastructure.Base.Abstractions.Grupo;
using MongoDB.Driver;

namespace Infrastructure.MongoDB.Service.Grupo
{
    public class GrupoServiceQuery : IGrupoServiceQuery
    {
        private readonly IMongoCollection<Entities.Grupo> _gruposCollection;
        private GrupoOutResponse _grupoOutResponse;
        private string _userId;
        public GrupoServiceQuery(Base.Configurations.Configuration grupoDatabaseSettings, string userId)
        {
            var mongoClient = new MongoClient(grupoDatabaseSettings.MongoDBSettings.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(grupoDatabaseSettings.MongoDBSettings.DataBaseName);

            _gruposCollection = mongoDatabase.GetCollection<Entities.Grupo>(grupoDatabaseSettings.MongoDBSettings.CollectionName);

            _grupoOutResponse = new GrupoOutResponse();

            _userId = userId;
        }

        /*
        public async Task<GrupoOutResponse> GetGrupoById(string id)
        {
            _grupoOutResponse.SetData(await _gruposCollection.Find(x => x.Id == id).FirstOrDefaultAsync());
            _grupoOutResponse.SetResultado(true);
            return _grupoOutResponse;
        }
        */

        public async Task<GrupoOutResponse> GetGrupos(GetFrom getFrom)
        {
            var result = _gruposCollection.Find(

                x =>
            
                  getFrom.Id != null && x.Id == getFrom.Id
               || getFrom.FiltraPorNome && x.Name == getFrom.FiltroNome
                    && getFrom.FiltraUpdatedOn && x.UpdatedOn >= getFrom.DataInicial && x.UpdatedOn <= getFrom.DataFinal
                    && getFrom.FiltraStatus && x.Status == getFrom.Status
                  

            )
            .Skip(getFrom.PageSize * getFrom.PageNumber)
            .Limit(getFrom.PageSize);

            _grupoOutResponse.SetData(result);

            _grupoOutResponse.SetResultado(true);
            return _grupoOutResponse;
        }
    }
}
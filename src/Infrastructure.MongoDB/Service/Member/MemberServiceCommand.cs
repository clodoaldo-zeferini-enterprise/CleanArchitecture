using Application.DTO.Response;
using NetCore.Base.Enum;
using Infrastructure.Base.Abstractions.Grupo;
using MongoDB.Driver;

namespace Infrastructure.MongoDB.Service.Grupo
{
    public class GrupoServiceCommand : IGrupoServiceCommand
    {
        private readonly IMongoCollection<Entities.Grupo> _gruposCollection;
        private GrupoOutResponse _grupoOutResponse;
        private string _userId;
        public GrupoServiceCommand(Base.Configurations.Configuration grupoDatabaseSettings, string userId)
        {
            var mongoClient = new MongoClient(grupoDatabaseSettings.MongoDBSettings.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(grupoDatabaseSettings.MongoDBSettings.DataBaseName);

            _gruposCollection = mongoDatabase.GetCollection<Entities.Grupo>(grupoDatabaseSettings.MongoDBSettings.CollectionName);

            _grupoOutResponse = new GrupoOutResponse();

            _userId = userId;
        }

        public async Task<GrupoOutResponse> CreateGrupo(Domain.Entities.Grupo grupo)
        {
            /*Verifico se o Parâmetro é nulo*/
            if (grupo == null)
            {
                _grupoOutResponse.SetResultado(false);
                _grupoOutResponse.SetMensagem("O objeto recebido é nulo.");
                return _grupoOutResponse;
            }

            var grupoMongoDB = new Entities.Grupo(grupo.FirstName, grupo.LastName, grupo.Gender, grupo.Email, grupo.Status, _userId);
            await _gruposCollection.InsertOneAsync(grupoMongoDB);

            _grupoOutResponse.SetResultado(true);
            _grupoOutResponse.SetMensagem("Membro cadastrado com Sucesso!");
            _grupoOutResponse.SetData(GrupoMapper.FromEntityToDTO(grupo));
            return _grupoOutResponse;
        }

        public async Task<GrupoOutResponse> DeleteGrupo(string id)
        {
            bool IsIdValido = false;

            if (id == null)
            {
                IsIdValido = false;
            }
            else
            {
                IsIdValido = Guid.TryParse(id.ToString(), out Guid isIdValido);
            }

            if (!IsIdValido)
            {
                _grupoOutResponse.SetResultado(false);
                _grupoOutResponse.SetMensagem("Id inválido.");
                return _grupoOutResponse;
            }

            var grupo = await _gruposCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (grupo == null)
            {
                _grupoOutResponse.SetResultado(true);
                _grupoOutResponse.SetMensagem("Registro não encontrado!");
                return _grupoOutResponse;
            }

            grupo.Status = EStatus.EXCLUIDO;
            grupo.UpdatedOn = DateTime.Now;
            grupo.UpdatedBy = _userId;

            await _gruposCollection.ReplaceOneAsync(x => x.Id == id, grupo);

            _grupoOutResponse.SetData(GrupoMapper.FromMongoDBToDTO(grupo));
            _grupoOutResponse.SetResultado(true);
            _grupoOutResponse.SetMensagem("Grupo Excluído com Sucesso!");

            return _grupoOutResponse;
        }

        
        public async Task<Domain.Entities.Grupo> GetGrupoById(string id)
        {
            bool IsIdValido = false;

            if (id == null)
            {
                IsIdValido = false;
            }
            else
            {
                IsIdValido = Guid.TryParse(id.ToString(), out Guid isIdValido);
            }

            if (!IsIdValido)
            {
                _grupoOutResponse.SetResultado(false);
                _grupoOutResponse.SetMensagem("Id inválido.");
                return null;
            }

            Entities.Grupo grupo = await _gruposCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (grupo == null)
            {
                _grupoOutResponse.SetMensagem("Registro não encontrado.");
                _grupoOutResponse.SetResultado(true);
                return null;
            }

            return new Domain.Entities.Grupo(
                        grupo.Id, grupo.FirstName, grupo.LastName, grupo.Gender, grupo.Email, grupo.Status,
                        grupo.InsertedOn, grupo.InsertedBy, grupo.UpdatedOn, grupo.UpdatedBy);
       }
        

        public async Task<GrupoOutResponse> UpdateGrupo(Domain.Entities.Grupo grupo)
        {
            /*Verifico se o Parâmetro é nulo*/
            if (grupo == null)
            {
                _grupoOutResponse.SetResultado(false);
                _grupoOutResponse.SetMensagem("O objeto recebido é nulo.");
                return _grupoOutResponse;
            }

            var grupoExistente = await _gruposCollection.Find(x => x.Id == grupo.Id).FirstOrDefaultAsync();
           
            if (grupoExistente == null)
            {
                _grupoOutResponse.SetResultado(false); 
                _grupoOutResponse.SetMensagem("Registro não encontrado!");
                return _grupoOutResponse;
            }

            Entities.Grupo newGrupo =
                new Entities.Grupo
                (grupo.Id, grupoExistente._id,
                 grupo.FirstName,
                 grupo.LastName,
                 grupo.Gender,
                 grupo.Email,
                 grupo.Status,
                 grupo.InsertedOn,
                 grupo.InsertedBy,
                 DateTime.Now,
                 _userId
                 );

            await _gruposCollection.ReplaceOneAsync(x => x.Id == grupo.Id, newGrupo);

            _grupoOutResponse.SetData(GrupoMapper.FromEntityToDTO(grupo));
            _grupoOutResponse.SetResultado(true);
            _grupoOutResponse.SetMensagem("Grupo Atualizado com Sucesso!");

            return _grupoOutResponse;
        }
    }
}

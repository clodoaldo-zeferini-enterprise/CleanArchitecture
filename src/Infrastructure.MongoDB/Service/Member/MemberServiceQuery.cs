using Application.DTO.Request;
using Application.DTO.Response;
using Infrastructure.Base.Abstractions.Member;
using MongoDB.Driver;

namespace Infrastructure.MongoDB.Service.Member
{
    public class MemberServiceQuery : IMemberServiceQuery
    {
        private readonly IMongoCollection<Entities.Member> _membersCollection;
        private MemberOutResponse _memberOutResponse;
        private string _userId;
        public MemberServiceQuery(Base.Configurations.Configuration memberDatabaseSettings)
        {
            var mongoClient = new MongoClient(memberDatabaseSettings.MongoDBSettings.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(memberDatabaseSettings.MongoDBSettings.DataBaseName);

            _membersCollection = mongoDatabase.GetCollection<Entities.Member>(memberDatabaseSettings.MongoDBSettings.CollectionName);

            _memberOutResponse = new MemberOutResponse();

            _userId = Guid.NewGuid().ToString();
        }

        public async Task<MemberOutResponse> GetMemberById(string id)
        {
            _memberOutResponse.SetData(await _membersCollection.Find(x => x.Id == id).FirstOrDefaultAsync());
            _memberOutResponse.SetResultado(true);
            return _memberOutResponse;
        }

        public async Task<MemberOutResponse> GetMembers(GetFrom getFrom)
        {
            var result = _membersCollection.Find(

                x =>
            
                  getFrom.Id != null && x.Id == getFrom.Id
               || getFrom.FiltraNome && x.Name == getFrom.FiltroNome
                    && getFrom.FiltraUpdatedOn && x.UpdatedOn >= getFrom.DataInicial && x.UpdatedOn <= getFrom.DataFinal
                    && getFrom.FiltraStatus && x.Status == getFrom.Status
                  

            )
            .Skip(getFrom.PageSize * getFrom.PageNumber)
            .Limit(getFrom.PageSize);

            _memberOutResponse.SetData(result);

            _memberOutResponse.SetResultado(true);
            return _memberOutResponse;
        }
    }
}
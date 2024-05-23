using Application.DTO.Response;
using Application.DTO.Response.Member;
using Domain.Enums;
using Infrastructure.Base.Abstractions.Member;
using MongoDB.Driver;
using System.Reflection;

namespace Infrastructure.MongoDB.Service.Member
{
    public class MemberServiceCommand : IMemberServiceCommand
    {
        private readonly IMongoCollection<Entities.Member> _membersCollection;
        private MemberOutResponse _memberOutResponse;
        private string _userId;
        public MemberServiceCommand(Base.Configurations.Configuration memberDatabaseSettings)
        {
            var mongoClient = new MongoClient(memberDatabaseSettings.MongoDBSettings.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(memberDatabaseSettings.MongoDBSettings.DataBaseName);

            _membersCollection = mongoDatabase.GetCollection<Entities.Member>(memberDatabaseSettings.MongoDBSettings.CollectionName);

            _memberOutResponse = new MemberOutResponse();

            _userId = Guid.NewGuid().ToString();
        }

        public async Task<MemberOutResponse> CreateMember(Domain.Entities.Member member)
        {

            var memberMongoDB = new Entities.Member(member.FirstName, member.LastName, member.Gender, member.Email, member.Status, _userId);
            await _membersCollection.InsertOneAsync(memberMongoDB);

            return await GetMemberById(memberMongoDB.Id);
        }

        public async Task<MemberOutResponse> DeleteMember(string id)
        {
            var memberExistente = await _membersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (memberExistente == null)
            {
                _memberOutResponse.SetResultado(false); return _memberOutResponse;
            }

            memberExistente.Status = EStatus.EXCLUIDO;
            memberExistente.UpdatedOn = DateTime.Now;
            memberExistente.UpdatedBy = _userId;

            await _membersCollection.ReplaceOneAsync(x => x.Id == id, memberExistente);

            return await GetMemberById(id);
        }

        public async Task<MemberOutResponse> GetMemberById(string id)
        {
            Entities.Member member = await _membersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            /*
            MemberResponse memberResponse =
            new MemberResponse(
                    new Domain.Entities.Member(
                        member.Id, member.FirstName, member.LastName, member.Gender, member.Email, member.Status,
                        member.InsertedOn, member.InsertedBy, member.UpdatedOn, member.UpdatedBy));

            _memberOutResponse.SetData(memberResponse);
            */
            _memberOutResponse.SetResultado(true);
            return _memberOutResponse;
        }

        public async Task<MemberOutResponse> UpdateMember(Domain.Entities.Member member)
        {
            var memberExistente = await _membersCollection.Find(x => x.Id == member.Id).FirstOrDefaultAsync();
            if (memberExistente == null)
            {
                _memberOutResponse.SetResultado(false); return _memberOutResponse;
            }

            Entities.Member newMember =
                new Entities.Member
                (member.Id, memberExistente._id,
                 member.FirstName,
                 member.LastName,
                 member.Gender,
                 member.Email,
                 member.Status,
                 member.InsertedOn,
                 member.InsertedBy,
                 DateTime.Now,
                 _userId
                 );

            await _membersCollection.ReplaceOneAsync(x => x.Id == member.Id, newMember);

            return await GetMemberById(member.Id);
        }
    }
}

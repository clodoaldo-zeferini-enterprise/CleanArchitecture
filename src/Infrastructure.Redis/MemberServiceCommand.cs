using Application.DTO.Response;
using Infrastructure.Base.Abstractions.Member;
using Redis.OM;
using Redis.OM.Searching;

namespace Infrastructure.Redis
{
    public class MemberServiceCommand : IMemberServiceCommand
    {
        private readonly RedisCollection<Infrastructure.Redis.Entities.Member> _member;
        private readonly RedisConnectionProvider _provider;
        private MemberOutResponse _memberOutResponse;
        private string _userId;
        public MemberServiceCommand(RedisConnectionProvider provider)
        {
            _memberOutResponse =  new MemberOutResponse();
            _provider = provider;
            _member = (RedisCollection<Infrastructure.Redis.Entities.Member>)provider.RedisCollection<Infrastructure.Redis.Entities.Member>();
            _userId = Guid.NewGuid().ToString();
        }

        public async Task<MemberOutResponse> CreateMember(Domain.Entities.Member member)
        {
            Infrastructure.Redis.Entities.Member memberRedis = new Infrastructure.Redis.Entities.Member(
                member.FirstName, member.LastName, member.Gender, member.Email, member.Status, _userId);

            var result = await _member.InsertAsync(memberRedis);

            _memberOutResponse.SetData(member);
            _memberOutResponse.SetResultado(true);
            _memberOutResponse.SetMensagem("Member Criado com Sucesso!");

            return _memberOutResponse;
        }

        public async Task<MemberOutResponse> DeleteMember(string id)
        {
            var member = _member.Where(x => x.Id == id);
            
            _memberOutResponse.SetResultado(true);
            _memberOutResponse.SetMensagem("Member Excluído com Sucesso!");
            
            return _memberOutResponse;
        }

        public async Task<MemberOutResponse> GetMemberById(string id)
        {
            var member = _member.Where(x => x.Id == id);

            _memberOutResponse.SetResultado(true);
            _memberOutResponse.SetMensagem("Member Obtido com Sucesso!");

            return _memberOutResponse;
        }

        public async Task<MemberOutResponse> UpdateMember(Domain.Entities.Member member)
        {
            var memberToUpdate = _member.Where(x => x.Id == member.Id);

            _memberOutResponse.SetResultado(true);
            _memberOutResponse.SetMensagem("Member Atualizado com Sucesso!");

            return _memberOutResponse;
        }
    }
}

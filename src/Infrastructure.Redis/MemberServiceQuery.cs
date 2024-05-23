using Application.DTO.Request;
using Application.DTO.Response;
using Infrastructure.Base.Abstractions.Member;
using Redis.OM;
using Redis.OM.Searching;

namespace Infrastructure.Redis
{
    public class MemberServiceQuery : IMemberServiceQuery
    {
        private readonly RedisCollection<Infrastructure.Redis.Entities.Member> _member;
        private readonly RedisConnectionProvider _provider;
        private MemberOutResponse _memberOutResponse;
        private string _userId;
        public MemberServiceQuery(RedisConnectionProvider provider)
        {
            _memberOutResponse = new MemberOutResponse();
            _provider = provider;
            _member = (RedisCollection<Infrastructure.Redis.Entities.Member>)provider.RedisCollection<Infrastructure.Redis.Entities.Member>();
            _userId = Guid.NewGuid().ToString();
        }

        public async Task<MemberOutResponse> GetMemberById(string id)
        {
            var member = _member.Where(x => x.Id == id);

            _memberOutResponse.SetResultado(true);
            _memberOutResponse.SetMensagem("Member Obtido com Sucesso!");

            return _memberOutResponse;
        }

        public async Task<MemberOutResponse> GetMembers(GetFrom getFrom)
        {
            var member = _member.Where(

                x =>

                  getFrom.Id != null && x.Id == getFrom.Id
               || getFrom.FiltraNome && x.Name == getFrom.FiltroNome
                    && getFrom.FiltraUpdatedOn && x.UpdatedOn >= getFrom.DataInicial && x.UpdatedOn <= getFrom.DataFinal
                    && getFrom.FiltraStatus && x.Status == getFrom.Status
            ).Skip(getFrom.PageSize * getFrom.PageNumber)
            .Take(getFrom.PageSize);

            _memberOutResponse.SetData(member);

            _memberOutResponse.SetResultado(true);
            return _memberOutResponse; 
        }
    }
}

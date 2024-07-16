using Application.DTO.Request;
using Application.DTO.Response;
using Infrastructure.Base.Abstractions.Grupo;
using Newtonsoft.Json;
using Cassandra;

namespace Infrastructure.Cassandra.Service.Member
{
    public class MemberServiceQuery : IGrupoServiceQuery
    {
        private readonly ISession _session;
        private GrupoOutResponse _memberOutResponse;
        private string _userId;

        public MemberServiceQuery(ISession session)
        {
            _memberOutResponse = new GrupoOutResponse();
            _userId = Guid.NewGuid().ToString();

            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public Task<GrupoOutResponse> GetMembers(GetFrom getFrom)
        {
            return Task.FromResult(_memberOutResponse);
        }
    }
}

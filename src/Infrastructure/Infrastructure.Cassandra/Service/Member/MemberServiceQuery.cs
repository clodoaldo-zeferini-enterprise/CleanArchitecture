using Application.DTO.Request;
using Application.DTO.Response;
using Infrastructure.Base.Abstractions.Grupo;
using Newtonsoft.Json;
using Cassandra;

namespace Infrastructure.Cassandra.Service.Grupo
{
    public class GrupoServiceQuery : IGrupoServiceQuery
    {
        private readonly ISession _session;
        private GrupoOutResponse _memberOutResponse;
        private string _userId;

        public GrupoServiceQuery(ISession session)
        {
            _memberOutResponse = new GrupoOutResponse();
            _userId = Guid.NewGuid().ToString();

            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public Task<GrupoOutResponse> GetGrupos(GetFrom getFrom)
        {
            return Task.FromResult(_memberOutResponse);
        }
    }
}

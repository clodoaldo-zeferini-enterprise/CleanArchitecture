
using Application.DTO.Request;
using Application.DTO.Response;

namespace Infrastructure.Base.Abstractions.Grupo
{
    public interface IGrupoServiceQuery
    {
        Task<GrupoOutResponse> GetGrupos(GetFrom getFrom);
    }
}

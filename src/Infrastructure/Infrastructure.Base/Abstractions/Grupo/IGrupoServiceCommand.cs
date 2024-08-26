
using Application.DTO.Response;
using NetCore.Base.Enum;

namespace Infrastructure.Base.Abstractions.Grupo
{
    public interface IGrupoServiceCommand
    {
        Task<GrupoOutResponse> CreateGrupo(Domain.Entities.Grupo grupo);
        Task<Domain.Entities.Grupo> GetGrupoById(string id);
        Task<GrupoOutResponse> UpdateGrupo(string id, string userId, EStatus status);
        Task<GrupoOutResponse> DeleteGrupo(string id, string userId);
    }
}

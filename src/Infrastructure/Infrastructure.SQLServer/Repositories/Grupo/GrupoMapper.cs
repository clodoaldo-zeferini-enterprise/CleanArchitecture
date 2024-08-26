using Application.DTO.Response.Grupo;

namespace Infrastructure.SQLServer.Repositories.Grupo
{
    public static class GrupoMapper
    {
        public static GrupoResponse FromMongoDBToDTO(List<Domain.Entities.Grupo> grupos)
        {
            if (grupos.Any())
            {
                List<Application.DTO.Response.Grupo.Grupo> gruposDTO = new List<Application.DTO.Response.Grupo.Grupo>();

                foreach (var grupo in grupos)
                {
                    gruposDTO.Add(new Application.DTO.Response.Grupo.Grupo(grupo));
                }

                return new GrupoResponse(gruposDTO);
            }

            return null;
        }
                
        public static GrupoResponse FromEntityToDTO(Domain.Entities.Grupo grupo)
        {
            List<Application.DTO.Response.Grupo.Grupo> gruposDTO = new List<Application.DTO.Response.Grupo.Grupo>();
            gruposDTO.Add(new Application.DTO.Response.Grupo.Grupo(grupo));
            return new GrupoResponse(gruposDTO);       
        }
    }
}

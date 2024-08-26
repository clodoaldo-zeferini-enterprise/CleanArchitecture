using Application.DTO.Response.Grupo;

namespace Infrastructure.SQLServer.Service.Grupo
{
    public static class GrupoMapper
    {
        public static GrupoResponse FromSQLDBToDTO(
            IEnumerable<Application.DTO.Response.Navigator> navigators,
            IEnumerable<Domain.Entities.Grupo> grupos)
        {
            List<Application.DTO.Response.Grupo.Grupo> gruposDTO = new List<Application.DTO.Response.Grupo.Grupo>();
            List<Application.DTO.Response.Navigator> navigatorsDTO = new List<Application.DTO.Response.Navigator>();

            if (grupos.Any())
            {
                foreach (var grupo in grupos)
                {
                    gruposDTO.Add(new Application.DTO.Response.Grupo.Grupo(grupo));
                }                
            }

            if (navigators.Any())
            {
                foreach (var navigator in navigators)
                {
                    navigatorsDTO.Add(navigator);
                }
            }

            return new GrupoResponse(navigatorsDTO, gruposDTO);

        }
        public static GrupoResponse FromSQLDBToDTO(List<Domain.Entities.Grupo> grupos)
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
        public static GrupoResponse FromEntityToDTO(IEnumerable<Domain.Entities.Grupo> enumGrupo)
        {
            List<Application.DTO.Response.Grupo.Grupo> gruposDTO = new List<Application.DTO.Response.Grupo.Grupo>();
            foreach (var grupo in enumGrupo)
            {
                gruposDTO.Add(new Application.DTO.Response.Grupo.Grupo(grupo));
            }

            return new GrupoResponse(gruposDTO);
        }
    }
}

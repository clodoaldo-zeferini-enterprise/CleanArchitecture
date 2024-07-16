using Domain.Entities;
using NetCore.Base.Enum;
using System.Collections.Generic;

namespace Application.DTO.Response.Grupo
{
    public class GrupoResponse
    {
        public List<Navigator> Navigators { get; private set; }
        public List<Grupo> Grupos { get; private set; }

        public GrupoResponse(List<Application.DTO.Response.Grupo.Grupo> grupos)
        {
            Navigators = new List<Navigator>();
            Grupos = grupos;

            Navigator navigator = new Navigator(1, 1, 1, 1);
            Navigators.Add(navigator);
        }

        public GrupoResponse(List<Navigator> navigators, List<Application.DTO.Response.Grupo.Grupo> grupos)
        {
            Navigators = navigators;
            Grupos = new List<Grupo>();

            if (grupos != null)
            {
                Grupos.AddRange(grupos);
            }
        }
    }
}

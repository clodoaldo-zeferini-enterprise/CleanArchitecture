using NetCore.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Request.Grupo
{
    public class UpdateGrupoRequest
    {
        public string Idy { get; set; }
        public string UpdatedBy { get; set; }
        public EStatus Status { get; set; }

        public UpdateGrupoRequest(string idy, string updatedBy, EStatus status)
        {
            Idy = idy;
            UpdatedBy = updatedBy;
            Status = status;
        }

        /*
        public string NomeDoGrupo  {get;set;}
        public string RazaoSocial {get;set;}
        public string NomeFantasia {get;set;}
        public string CpfDoAdministrador {get;set;}
        public string PreNomeDoAdministrador {get;set;}
        public string NomeDoMeioDoAdministrador {get;set;}
        public string SobreNomeDoAdministrador {get;set;}
        public string EmailDoAdministrador {get;set;}
        */


    }
}

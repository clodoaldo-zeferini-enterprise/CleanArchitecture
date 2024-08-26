using NetCore.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Request.Grupo
{
    public class CreateGrupoRequest
    {
        public string NomeDoGrupo  {get;set;}
        public string RazaoSocial {get;set;}
        public string NomeFantasia {get;set;}
        public string Cnpj {get;set;}
        public string InscricaoEstadual {get;set;}
        public string CpfDoAdministrador {get;set;}
        public string PreNomeDoAdministrador {get;set;}
        public string NomeDoMeioDoAdministrador {get;set;}
        public string SobreNomeDoAdministrador {get;set;}
        public string EmailDoAdministrador {get;set;}
        public string InsertedBy {get;set;}

        public CreateGrupoRequest(string nomeDoGrupo, string razaoSocial, string nomeFantasia, string cnpj, string inscricaoEstadual, string cpfDoAdministrador, string preNomeDoAdministrador, string nomeDoMeioDoAdministrador, string sobreNomeDoAdministrador, string emailDoAdministrador, string insertedBy)
        {
            NomeDoGrupo = nomeDoGrupo;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            Cnpj = cnpj;
            InscricaoEstadual = inscricaoEstadual;
            CpfDoAdministrador = cpfDoAdministrador;
            PreNomeDoAdministrador = preNomeDoAdministrador;
            NomeDoMeioDoAdministrador = nomeDoMeioDoAdministrador;
            SobreNomeDoAdministrador = sobreNomeDoAdministrador;
            EmailDoAdministrador = emailDoAdministrador;
            InsertedBy = insertedBy;
        }
    }
}

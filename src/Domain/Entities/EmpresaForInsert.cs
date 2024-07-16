
using NetCore.Base.Enum; 
namespace Domain.Entities
{
    public class EmpresaForInsert
    {
        public short CodigoDaEmpresa { get; set; }
        public string CNPJ { get; set; }
        public string InscricaoEstadual { get; set; }
        public string RazaoSocial { get; set; }
        public string Apelido { get; set; }
        public string NomeDoAdministrador { get; set; }
        public string EmailDoAdministrador { get; set; }
    }
}

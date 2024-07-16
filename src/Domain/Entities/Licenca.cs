using NetCore.Base.Enum; 
namespace Domain.Entities
{
    public class Licenca
    {
        public Guid SistemaEmpresaId { get; set; }

        public Guid LicencaId { get; set; }

        public string Chave { get; set; }

        public System.DateTime DataInicial { get; set; }

        public System.DateTime DataFinal { get; set; }

        public EStatus Status { get; set; }
    }
}

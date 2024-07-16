using NetCore.Base.Enum; namespace Domain.Entities
{    
    public class Versao
    {
        public Guid SistemaId { get;  private set; }
        public Guid VersaoId { get;  private set; }
        public EStatus Status { get;  private set; }
        public string Nome { get;  private set; }
        public System.DateTime DataDePublicacao { get;  private set; }
        public System.DateTime DataLimite { get;  private set; }
    }
}

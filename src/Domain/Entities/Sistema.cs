using NetCore.Base.Enum; namespace Domain.Entities
{
    public class Sistema
    {
        public Guid SistemaId { get;  private set; }
        public EStatus Status { get;  private set; }
        public string NomeDoSistema { get;  private set; }
        public ICollection<Modulo> Modulo { get;  private set; }
    }
}

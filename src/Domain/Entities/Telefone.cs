using NetCore.Base.Enum; namespace Domain.Entities
{
    public class Telefone
    {
        public Guid TelefoneDe { get;  private set; }
        public Guid TelefoneId { get;  private set; }
        public Guid OperadoraId { get;  private set; }
        public EStatus Status { get;  private set; }
        public string Numero { get;  private set; }
    }
}

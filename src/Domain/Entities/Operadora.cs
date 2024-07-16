using NetCore.Base.Enum; namespace Domain.Entities
{
    public class Operadora
    {
        public Guid OperadoraId { get;  private set; }
        public string NomeDaOperadora { get;  private set; }
        public string Prefixo { get;  private set; }
    }
}

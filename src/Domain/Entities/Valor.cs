using NetCore.Base.Enum; namespace Domain.Entities
{
    public class Valor
    {
        public Guid PropriedadeId { get;  private set; }
        public Guid ValorId { get;  private set; }
        public EStatus Status { get;  private set; }
        public Guid EmpresaId { get;  private set; }
        public short ValorNumerico { get;  private set; }
        public string ValorExibido { get;  private set; }
    }
}

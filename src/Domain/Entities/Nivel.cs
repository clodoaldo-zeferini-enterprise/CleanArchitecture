using NetCore.Base.Enum; namespace Domain.Entities
{
    public class Nivel
    {
        public Guid EmpresaId { get;  private set; }
        public Guid NivelId { get;  private set; }
        public EStatus Status { get;  private set; }
        public short NivelNumero { get;  private set; }
        public string Descricao { get;  private set; }
    }
}

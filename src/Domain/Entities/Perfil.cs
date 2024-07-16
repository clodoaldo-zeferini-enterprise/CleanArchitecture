using NetCore.Base.Enum; namespace Domain.Entities
{    public class Perfil
    {
        public Guid EmpresaId { get;  private set; }
        public Guid PerfilId { get;  private set; }
        public EStatus Status { get;  private set; }
        public short PerfilNumero { get;  private set; }
        public string Descricao { get;  private set; }
    }
}

using NetCore.Base.Enum; namespace Domain.Entities
{

    public class Modulo
    {
        public Guid SistemaId { get;  private set; }
        public Guid ModuloId { get;  private set; }
        public EStatus Status { get;  private set; }
        public string NomeDoModulo { get;  private set; }
        public string Link { get;  private set; }
        public ICollection<Programa> Programas { get;  private set; }
    }
}

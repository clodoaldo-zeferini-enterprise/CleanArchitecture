using NetCore.Base.Enum; namespace Domain.Entities
{
    public class Programa
    {
        public Guid ModuloId { get;  private set; }
        public Guid ProgramaId { get;  private set; }
        public EStatus Status { get;  private set; }
        public Guid SistemaId { get;  private set; }
        public string NomeDoPrograma { get;  private set; }
        public string Link { get;  private set; }
        public ICollection<Direito> Direitos { get;  private set; }
    }
}

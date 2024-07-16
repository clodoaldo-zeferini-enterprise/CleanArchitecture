using NetCore.Base.Enum; namespace Domain.Entities
{
    public class Pais
    {
        public Guid PaisId { get;  private set; }
        public string NomeDoPais { get;  private set; }
        public int CodigoDoPais { get;  private set; }
        public string Sigla { get;  private set; }
    }
}
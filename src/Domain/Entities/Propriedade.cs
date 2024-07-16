using NetCore.Base.Enum; namespace Domain.Entities
{
    public class Propriedade
    {
        public Guid EmpresaId { get;  private set; }
        public Guid PropriedadeId { get;  private set; }
        public EStatus Status { get;  private set; }
        public Guid Origem { get;  private set; }
        public string NomeDaPropriedade { get;  private set; }
        public string Descricao { get;  private set; }
    }
}

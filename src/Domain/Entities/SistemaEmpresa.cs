using NetCore.Base.Enum; namespace Domain.Entities
{
    public class SistemaEmpresa
    {
        public Guid SistemaId { get;  private set; }
        public Guid SistemaEmpresaId { get;  private set; }
        public EStatus Status { get;  private set; }
        public Guid EmpresaId { get;  private set; }
        public string NomeDaEmpresa { get;  private set; }
        public string NomeDoSistema { get;  private set; }
    }
}

using NetCore.Base.Enum; 
namespace Domain.Entities
{
    public class Log
    {
        public Guid RegistroLogado { get;  private set; }
        public Guid LogId { get;  private set; }
        public EStatus Status { get;  private set; }
        public Guid SysUsuSessionId { get;  private set; }
        public System.DateTime DataHora { get;  private set; }
        public string Operacao { get;  private set; }
        public Guid TabelaId { get;  private set; }
    }
}

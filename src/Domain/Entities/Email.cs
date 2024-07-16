using NetCore.Base.Enum;

namespace Domain.Entities
{    
    public class Email
    {
        public Guid EmailDe { get;  private set; }
        public Guid EmailId { get;  private set; }
        public EStatus Status { get;  private set; }
        public string Endereco { get;  private set; }
    }
}

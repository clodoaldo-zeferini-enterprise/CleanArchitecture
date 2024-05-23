using Domain.Enums;

namespace Domain.Entities
{
    public abstract class Base
    {        
        public EStatus Status { get; set; }
        public DateTime InsertedOn { get; set; }
        public string InsertedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }    
    }
}

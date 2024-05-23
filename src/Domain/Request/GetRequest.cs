using Domain.Enums;

namespace Domain.Request
{
    public class GetRequest
    {
        public UInt16 PageNumber { get; set; } = 1;
        public UInt16 PageSize { get; set; } = 10;
        public bool FiltraNome { get; set; } = false;
        public string FiltroNome { get; set; } = "";
        public bool FiltraInsertedOn { get; set; } = false;
        public DateTime? DataInicial { get; set; } 
        public DateTime? DataFinal { get; set; }
        public bool FiltraStatus { get; set; } = true;
        public EStatus Status { get; set; } = Domain.Enums.EStatus.ATIVO;
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public GetRequest()
        {
            
        }
    }
}

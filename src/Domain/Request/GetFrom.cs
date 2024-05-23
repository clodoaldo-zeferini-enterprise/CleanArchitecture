using Domain.Enums;

namespace Domain.Request;

public abstract class GetFrom : RequestBase
{
    public string? Id { get; set; }
    public UInt16 PageNumber { get; set; }
    public UInt16 PageSize { get; set; }
    public bool FiltraNome { get; set; }
    public string FiltroNome { get; set; }
    public bool FiltraUpdatedOn { get; set; }
    public DateTime? DataInicial { get; set; }
    public DateTime? DataFinal { get; set; }
    public bool FiltraStatus { get; set; }
    public EStatus Status { get; set; }

    public string Select {  get; set; }

    public GetFrom() {}
    public GetFrom(
        ushort pageNumber, ushort pageSize,
        bool filtraNome, string filtroNome,
        bool filtraInsertedOn, DateTime? dataInicial, DateTime? dataFinal,
        bool filtraStatus, EStatus eStatus
    ) 
    {
        SysUsuSessionId = Guid.NewGuid().ToString();
        Id = Guid.NewGuid().ToString();

        PageNumber = pageNumber;
        PageSize = pageSize;
        FiltraNome = filtraNome;
        FiltroNome = filtroNome;
        FiltraUpdatedOn = filtraInsertedOn;
        DataInicial = dataInicial;
        DataFinal = dataFinal;
        FiltraStatus = filtraStatus;
        Status = eStatus;

    }
}


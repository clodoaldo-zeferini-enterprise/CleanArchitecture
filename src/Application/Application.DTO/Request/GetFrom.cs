using NetCore.Base.Enum;

namespace Application.DTO.Request;

public abstract class GetFrom : RequestBase
{
    public bool FiltraPorId { get; protected set; }
    public string? Id { get; protected set; }
    public UInt16 PageNumber { get; protected set; }
    public UInt16 PageSize { get; protected set; }
    public bool FiltraPorNome { get; protected set; }
    public string? FiltroNome { get; protected set; }
    public bool FiltraUpdatedOn { get; protected set; }
    public DateTime? DataInicial { get; protected set; }
    public DateTime? DataFinal { get; protected set; }
    public bool FiltraStatus { get; protected set; }
    public EStatus Status { get; protected set; }

    public string Select {  get; protected set; }
    
    public GetFrom(
        ushort pageNumber, ushort pageSize,
        bool filtraPorNome, string filtroNome,
        bool filtraInsertedOn, DateTime? dataInicial, DateTime? dataFinal,
        bool filtraStatus, EStatus eStatus, bool filtraPorId, string id
    ) 
    {
        SysUsuSessionId = Guid.NewGuid().ToString();
        FiltraPorId = filtraPorId;
        Id = id;

        PageNumber = pageNumber;
        PageSize = pageSize;
        FiltraPorNome = filtraPorNome;
        FiltroNome = filtroNome;
        FiltraUpdatedOn = filtraInsertedOn;
        DataInicial = dataInicial;
        DataFinal = dataFinal;
        FiltraStatus = filtraStatus;
        Status = eStatus;
    }


}


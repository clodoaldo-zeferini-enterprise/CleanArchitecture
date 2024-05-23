using Application.Base;
using Application.DTO.Request;
using Application.DTO.Response;
using Domain.Enums;
using MediatR;

namespace Application.CQRS.Query.NoSQL.Member.Queries;

public class GetMemberFromMongoDB : GetFrom, IRequest<MemberOutResponse>
{     
    public GetMemberFromMongoDB() {}

    private void Validate()
    {
        var IsSysUsuSessionIdValido = Guid.TryParse(SysUsuSessionId.ToString(), out Guid isSysUsuSessionIdValido);
        var IsIdValido = Guid.TryParse(Id.ToString(), out Guid isIdValido);

        DateTime dataAuxiliar;

        if ((FiltraUpdatedOn) && (DataInicial > DataFinal))
        {
            dataAuxiliar = DataInicial.Value;
            DataInicial = DataFinal.Value;
            DataFinal = dataAuxiliar;
        }

        if (PageSize > 50) PageSize = 50;

        ValidadorDeRegra.Novo()
            .Quando(!IsSysUsuSessionIdValido, "Base.Resource.SysUsuSessionIdInvalido")
            .Quando(!IsIdValido, "Base.Resource.IdInvalido")
            .Quando((FiltraNome && (FiltroNome == null || FiltroNome.Length == 0 || FiltroNome.Length > 100)), "Resource.FiltroNomeInvalido")
            .Quando((FiltraUpdatedOn && (DataInicial == null)), "Resource.DataInicialInvalida")
            .Quando((FiltraUpdatedOn && (DataFinal == null)), "Resource.DataFinalInvalida")
            .DispararExcecaoSeExistir();
    }

    public GetMemberFromMongoDB(
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

        Validate();
    }
}


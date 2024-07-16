using Application.Base;
using Application.DTO.Request;
using Application.DTO.Response;
using NetCore.Base.Enum;
using MediatR;

namespace Application.CQRS.Query.NoSQL.Grupo.Queries;

public class GetGrupoFromNoSQL : GetFrom, IRequest<GrupoOutResponse>
{
    public GetGrupoFromNoSQL(ushort pageNumber, ushort pageSize, bool filtraNome, string filtroNome, bool filtraInsertedOn, DateTime? dataInicial, DateTime? dataFinal, bool filtraStatus, EStatus eStatus, bool filtraPorId, string id) : base(pageNumber, pageSize, filtraNome, filtroNome, filtraInsertedOn, dataInicial, dataFinal, filtraStatus, eStatus, filtraPorId, id)
    {
        Validate();
    }

    private void Validate()
    {
        var IsSysUsuSessionIdValido = Guid.TryParse(SysUsuSessionId.ToString(), out Guid isSysUsuSessionIdValido);

        bool IsIdValido = false;

        if (FiltraPorId)
        {
            IsIdValido = Guid.TryParse(Id.ToString(), out Guid isIdValido);
        }


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
            .Quando((FiltraPorId && !IsIdValido), "Base.Resource.IdInvalido")
            .Quando((FiltraPorNome && (FiltroNome == null || FiltroNome.Length == 0 || FiltroNome.Length > 100)), "Resource.FiltroNomeInvalido")
            .Quando((FiltraUpdatedOn && (DataInicial == null)), "Resource.DataInicialInvalida")
            .Quando((FiltraUpdatedOn && (DataFinal == null)), "Resource.DataFinalInvalida")
            .DispararExcecaoSeExistir();
    }
}


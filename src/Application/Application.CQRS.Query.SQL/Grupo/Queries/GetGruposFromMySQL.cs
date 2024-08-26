using Application.Base;
using Application.DTO.Request;
using Application.DTO.Response;
using NetCore.Base.Enum;
using MediatR;
using System.Reflection;

namespace Application.CQRS.Query.SQL.Grupo.Queries;

public class GetGruposFromMySQL : GetFrom, IRequest<GrupoOutResponse>
{
    public GetGruposFromMySQL(ushort pageNumber, ushort pageSize, bool filtraNome, string filtroNome, bool filtraInsertedOn, DateTime? dataInicial, DateTime? dataFinal, bool filtraStatus, EStatus eStatus, bool filtraPorId, string id) : base(pageNumber, pageSize, filtraNome, filtroNome, filtraInsertedOn, dataInicial, dataFinal, filtraStatus, eStatus, filtraPorId, id)
    {
        Validate();
    }

    private string GetQuery(string select)
    {
        var offSet = (PageNumber - 1) * PageSize;

        string result = $@"

            -- Calculando o número total de registros na tabela Grupos e os parâmetros de paginação
            WITH RecordCount AS (
                SELECT COUNT(*) AS total_records FROM Grupos
            ),
            Navigator AS (
                SELECT
                    total_records AS RecordCount,
                    {PageNumber} AS PageNumber,
                    {PageSize} AS PageSize,
                    CEIL(total_records / {PageSize}) AS PageCount
                FROM RecordCount
            )
            -- Selecionando os dados para a tabela Navigator
            SELECT
                RecordCount,
                PageNumber,
                PageSize,
                PageCount
            FROM Navigator;            

            -- Selecionando os registros da página especificada usando os parâmetros calculados
            PREPARE stmt FROM '{select} ORDER BY Name LIMIT {PageSize} OFFSET {offSet}';
            EXECUTE stmt;
            DEALLOCATE PREPARE stmt;
        ";        

        return result;
    }


    private void SetSelect()
    {
        // Get the type handle of a specified class.
        Type myType = typeof(Domain.Entities.Grupo);

        Select = String.Empty;

        // Get the fields of the specified class.
        PropertyInfo[] myProperties = myType.GetProperties();

        string[] properties = new string[myProperties.Length];

        for (int i = 0; i < (myProperties.Length); i++)
        {
            Select += $@" {myProperties[i].Name} {(i < (myProperties.Length - 1) ? " , " : "")}";
        }

        Select = $@"SELECT {Select} FROM Grupos ";

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


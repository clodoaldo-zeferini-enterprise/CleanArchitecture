using Application.Base;
using Domain.Enums;
using MediatR;
using System.Reflection;

using Application.DTO.Response;
using Application.DTO.Request;

namespace Application.CQRS.Query.SQL.Member.Queries;

public class GetMemberFromSQLServer : GetFrom, IRequest<MemberOutResponse>
{     
    public GetMemberFromSQLServer() {}


    private string GetQuery()
    {
        /*Montando Filtro*/
        string _query = string.Empty;
        string _where = string.Empty;

        return _query;

        string tabela = $@"Members";
        _where = "' Status <> -1 '";

        string whereNome = string.Empty;
        if (FiltraNome && FiltroNome != null && FiltroNome.Trim().Length > 0)
        {
            whereNome = $@"' Nome LIKE ' , '''', '%' , '{FiltroNome}' , '%', ''''";
        }

        string whereInsertedOn = string.Empty;
        if (FiltraUpdatedOn && DataInicial != null && DataFinal != null)
        {
            whereInsertedOn = $@"' InsertedOn BETWEEN ', '''', '{DataInicial.Value:yyyy-MM-dd}{$@" 00:00:00.000"}' , '''', ' AND ' , '''', '{DataFinal.Value:yyyy-MM-dd}{$@" 23:59:59.999"}', ''''";
        }
        _where = $@" SET @WHERE = CONCAT(' WHERE ' , 
                                                        {_where} 
                             
                                                        {(whereNome.Trim().Length > 0 ? $@" AND CONCAT({whereNome}));" : $@"")}

                                                        {(whereInsertedOn.Trim().Length > 0 ? $@" AND CONCAT({whereInsertedOn}));" : $@"")}
                                                    );
                    ";

        _query = $@"
                         DECLARE 
                              @PageNumber            int = {PageNumber}
                            , @PageSize              int = {PageSize}

                            , @WHERE                 NVARCHAR(MAX) 
                            , @SelectNavigator       NVARCHAR(MAX)
                            , @SelectTabelaDesejada  NVARCHAR(MAX)
                        ;

                        {_where}
                        
                        SET @SelectNavigator = CONCAT(
                                                        ' DECLARE
                                                          @RecordCount     INT
                                                        , @PageCount       INT
                                                        , @Resto           INT
                                                        , @PageSize        INT
                                                        , @PageNumber      INT;',
                                                        '
                                                        SET @PageSize   = ', @PageSize, ';',
                                                        '
                                                        SET @PageNumber = ', @PageNumber, ';',
                                                        '
                                                        SET @RecordCount = (SELECT COUNT(*) FROM {tabela} WITH (NOLOCK) ', @WHERE, ' );

                                                        SET @Resto = (@RecordCount % @PageSize);

                                                        SET @PageCount = (SELECT IIF(@Resto = 0, (@RecordCount / @PageSize), ((@RecordCount / @PageSize) + 1)));

                                                        SELECT 
                                                          @RecordCount RecordCount
                                                        , @PageNumber  PageNumber
                                                        , @PageSize    PageSize
                                                        , @PageCount   PageCount; 

                                                        SET @Resto = (@RecordCount % @PageSize);
                                                        SET @PageCount = (SELECT IIF(@Resto = 0, (@RecordCount / @PageSize), ((@RecordCount / @PageSize) + 1)));
                                                                   

                                                        '
                                                        );

                        EXEC sp_executesql @SelectNavigator;

                        SET @SelectTabelaDesejada = CONCAT( ' {Select} '
                                                           , @WHERE, '  ORDER BY InsertedOn ', ' OFFSET ', @PageSize * (@PageNumber - 1)
                                                           , ' ROWS FETCH NEXT ', @PageSize, ' ROWS ONLY ; ', ''
                                                         );

                        EXEC sp_executesql @SelectTabelaDesejada;

                        /*SELECT @SelectTabelaDesejada;*/
                      ";
        return _query;
    }


    private void SetSelect()
    {
        // Get the type handle of a specified class.
        Type myType = typeof(Domain.Entities.Member);

        // Get the fields of the specified class.
        FieldInfo[] myField = myType.GetFields();

        string[] fields = new string[myField.Length];

        for (int i = 0; i < myField.Length; i++)
        {
            Select += $@" {myField[i].Name} {(i < (myField.Length) ? " , " : "")}";
        }

        Select = $@"SELECT {Select} FROM MEMBERS ";
    }

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
        
        SetSelect();
        Select = GetQuery();
    }

    public GetMemberFromSQLServer(
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


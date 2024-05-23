using Application.Base;
using Domain.Enums;
using MediatR;
using System.Reflection;
using Application.DTO.Response;
using Application.DTO.Request;

namespace Application.CQRS.Query.SQL.Member.Queries;

public class GetMemberFromSQLite : GetFrom, IRequest<MemberOutResponse>
{     
    public GetMemberFromSQLite() {}


    private string GetQuery()
    {        
        return Select;
    }


    private void SetSelect()
    {
        // Get the type handle of a specified class.
        Type myType = typeof(Domain.Entities.Member);

        Select = String.Empty;

        // Get the fields of the specified class.
        PropertyInfo[] myProperties = myType.GetProperties();

        string[] properties = new string[myProperties.Length];

        for (int i = 0; i < (myProperties.Length); i++)
        {
            Select += $@" {myProperties[i].Name} {(i < (myProperties.Length - 1) ? " , " : "")}";
        }

        Select = $@"SELECT {Select} FROM MEMBERS ";

        /*
        Select = Select
            .Replace("Id", "substr(Id,1,8)  || substr(Id,9,5)  || substr(Id,14,6) || substr(Id,20,4) || substr(Id,24,13) Id")
            .Replace("InsertedBy", "substr(InsertedBy,1,8)  || substr(InsertedBy,9,5)  || substr(InsertedBy,14,6) || substr(InsertedBy,20,4) || substr(InsertedBy,24,13) InsertedBy")
            .Replace("UpdatedBy", "substr(UpdatedBy,1,8)  || substr(UpdatedBy,9,5)  || substr(UpdatedBy,14,6) || substr(UpdatedBy,20,4) || substr(UpdatedBy,24,13) UpdatedBy");
        */


    }

    private void Validate()
    {
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
            /*.Quando(!IsSysUsuSessionIdValido, "Base.Resource.SysUsuSessionIdInvalido")*/
            .Quando(!IsIdValido, "Base.Resource.IdInvalido")
            .Quando((FiltraNome && (FiltroNome == null || FiltroNome.Length == 0 || FiltroNome.Length > 100)), "Resource.FiltroNomeInvalido")
            .Quando((FiltraUpdatedOn && (DataInicial == null)), "Resource.DataInicialInvalida")
            .Quando((FiltraUpdatedOn && (DataFinal == null)), "Resource.DataFinalInvalida")
            .DispararExcecaoSeExistir();
        
        SetSelect();
        Select = GetQuery();
    }

    public GetMemberFromSQLite(
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


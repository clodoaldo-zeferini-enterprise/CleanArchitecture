using Application.CQRS.Base.Grupo.Notifications;
using Application.CQRS.Query.SQL.Grupo.Queries;
using Application.DTO.Response;
using Application.DTO.Response.Grupo;
using Infrastructure.Base.Abstractions.Grupo;
using Infrastructure.SQLServer.Repositories.Base.Dapper;
using Infrastructure.SQLServer.Repositories.Grupo;
using MediatR;

namespace Application.CQRS.Query.SQL.Grupo.QueryHandlers;


public class GetGruposFromCassandraQueryHandler : IRequestHandler<GetGruposFromCassandra, GrupoOutResponse>
{
    private readonly IGrupoServiceQuery _grupoServiceQuery;
    private readonly IMediator _mediator;

    public GetGruposFromCassandraQueryHandler(IGrupoServiceQuery grupoServiceQuery, IMediator mediator)
    {
        _grupoServiceQuery = grupoServiceQuery;
        _mediator = mediator;
    }

    public async Task<GrupoOutResponse> Handle(GetGruposFromCassandra request, CancellationToken cancellationToken)
    {
        var result = await _grupoServiceQuery.GetGrupos(request);

        await _mediator.Publish(new GrupoNotification(result, result.Resultado ? Domain.Enums.EStatusCommand.CREATED_WITH_SUCCESS : Domain.Enums.EStatusCommand.FAILED_TO_CREATE), cancellationToken);

        return result;        
    }
}



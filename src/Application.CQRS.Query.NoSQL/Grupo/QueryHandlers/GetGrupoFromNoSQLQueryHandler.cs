using Application.CQRS.Base.Grupo.Notifications;
using Application.CQRS.Query.NoSQL.Grupo.Queries;

using Application.DTO.Response;
using Infrastructure.Base.Abstractions.Grupo;
using MediatR;

namespace Application.CQRS.Query.NoSQL.Grupo.QueryHandlers;

public class GetGruposFromNoSQLQueryHandler : IRequestHandler<GetGrupoFromNoSQL, GrupoOutResponse>
{
    private readonly IGrupoServiceQuery _grupoServiceQuery;
    private readonly IMediator _mediator;

    public GetGruposFromNoSQLQueryHandler(IGrupoServiceQuery grupoServiceQuery, IMediator mediator)
    {
        _grupoServiceQuery = grupoServiceQuery;
        _mediator = mediator;
    }
    public async Task<GrupoOutResponse> Handle(GetGrupoFromNoSQL request, CancellationToken cancellationToken)
    {
        var result = await _grupoServiceQuery.GetGrupos(request);

        await _mediator.Publish(new GrupoNotification(result, result.Resultado ? Domain.Enums.EStatusCommand.CREATED_WITH_SUCCESS : Domain.Enums.EStatusCommand.FAILED_TO_CREATE), cancellationToken);

        return result;
    }
}



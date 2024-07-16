using Application.CQRS.Base.Grupo.Notifications;
using Application.CQRS.Command.Grupo.Commands;
using Application.DTO.Request;
using Application.DTO.Response;
using FluentValidation;
using Infrastructure.Base.Abstractions.Grupo;
using MediatR;

namespace Application.CQRS.Command.Grupo.CommandsHandlers;

public class UpdateGrupoCommandHandler : IRequestHandler<UpdateGrupoCommand, GrupoOutResponse>
{
    private readonly IGrupoServiceQuery _grupoServiceQuery;
    private readonly IGrupoServiceCommand _grupoServiceCommand;
    private readonly IValidator<UpdateGrupoCommand> _validator;
    private readonly IMediator _mediator;

    public UpdateGrupoCommandHandler(
        IGrupoServiceCommand grupoServiceCommand,
        IGrupoServiceQuery grupoServiceQuery,
        IValidator<UpdateGrupoCommand> validator,
        IMediator mediator
    )
    {
        _grupoServiceCommand = grupoServiceCommand;
        _grupoServiceQuery = grupoServiceQuery;
        _validator = validator;
        _mediator = mediator;
    }

    public async Task<GrupoOutResponse> Handle(UpdateGrupoCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        /*1 - Obter o registro atual no DB*/

        var grupo = await _grupoServiceQuery.GetGrupos(new GetFrom() . new Domain.Entities.Grupo(request.Id, request.FirstName, request.LastName, request.Gender, request.Email, request.Status, DateTime.Now, request.UserId, DateTime.Now, request.UserId);
        
        var result = await _grupoServiceCommand.UpdateGrupo(request.);

        await _mediator.Publish(new GrupoNotification(result, result.Resultado ? Domain.Enums.EStatusCommand.UPDATED_WITH_SUCCESS : Domain.Enums.EStatusCommand.FAILED_TO_UPDATE), cancellationToken);
       
        return result;
    }
}
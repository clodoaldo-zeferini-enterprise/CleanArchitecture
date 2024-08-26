using Application.CQRS.Base.Grupo.Notifications;
using Application.CQRS.Command.Grupo.Commands;
using Application.DTO.Response;
using FluentValidation;
using Infrastructure.Base.Abstractions.Grupo;
using MediatR;

namespace Application.CQRS.Command.Grupo.CommandsHandlers;

public class DeleteGrupoCommandHandler : IRequestHandler<DeleteGrupoCommand, GrupoOutResponse>
{
    private readonly IGrupoServiceCommand _grupoServiceCommand;
    private readonly IValidator<DeleteGrupoCommand> _validator;
    private readonly IMediator _mediator;

    public DeleteGrupoCommandHandler(
        IGrupoServiceCommand grupoService,
        IValidator<DeleteGrupoCommand> validator,
        IMediator mediator
        )
    {
        _grupoServiceCommand = grupoService;
        _validator = validator;
        _mediator = mediator;
    }
    public async Task<GrupoOutResponse> Handle(DeleteGrupoCommand request,
                 CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var result = await _grupoServiceCommand.DeleteGrupo(request.Id, request.UserId);

        await _mediator.Publish(new GrupoNotification(result, result.Resultado ? Domain.Enums.EStatusCommand.DELETED_WITH_SUCCESS : Domain.Enums.EStatusCommand.FAILED_TO_DELETE), cancellationToken);

        return result;
    }
}


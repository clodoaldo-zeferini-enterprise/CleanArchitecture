using Application.CQRS.Base.Members.Notifications;
using Application.CQRS.Command.Members.Commands;
using Application.DTO.Response;
using FluentValidation;
using Infrastructure.Base.Abstractions.Member;
using MediatR;

namespace Application.CQRS.Command.Members.CommandsHandlers;

public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, MemberOutResponse>
{
    private readonly IMemberServiceCommand _memberServiceCommand;
    private readonly IValidator<DeleteMemberCommand> _validator;
    private readonly IMediator _mediator;

    public DeleteMemberCommandHandler(
        IMemberServiceCommand memberService,
        IValidator<DeleteMemberCommand> validator,
        IMediator mediator
        )
    {
        _memberServiceCommand = memberService;
        _validator = validator;
        _mediator = mediator;
    }
    public async Task<MemberOutResponse> Handle(DeleteMemberCommand request,
                 CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var result = await _memberServiceCommand.DeleteMember(request.Id);

        await _mediator.Publish(new MemberNotification(result, result.Resultado ? Domain.Enums.EStatusCommand.DELETED_WITH_SUCCESS : Domain.Enums.EStatusCommand.FAILED_TO_DELETE), cancellationToken);

        return result;
    }
}


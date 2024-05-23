using Application.CQRS.Base.Members.Notifications;
using Application.CQRS.Command.Members.Commands;
using Application.DTO.Response;
using FluentValidation;
using Infrastructure.Base.Abstractions.Member;
using MediatR;

namespace Application.CQRS.Command.Members.CommandsHandlers;


public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, MemberOutResponse>
{
    private readonly IMemberServiceCommand _memberServiceCommand;
    private readonly IValidator<UpdateMemberCommand> _validator;
    private readonly IMediator _mediator;

    public UpdateMemberCommandHandler(
        IMemberServiceCommand memberService,
        IValidator<UpdateMemberCommand> validator,
        IMediator mediator
        )
    {
        _memberServiceCommand = memberService;
        _validator = validator;
        _mediator = mediator;
    }
    public async Task<MemberOutResponse> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);

        var member = new Domain.Entities.Member(request.Id, request.FirstName, request.LastName, request.Gender, request.Email, request.Status, DateTime.Now, request.UserId, DateTime.Now, request.UserId);
        
        var result = await _memberServiceCommand.UpdateMember(member);

        await _mediator.Publish(new MemberNotification(result, result.Resultado ? Domain.Enums.EStatusCommand.UPDATED_WITH_SUCCESS : Domain.Enums.EStatusCommand.FAILED_TO_UPDATE), cancellationToken);
       
        return result;
    }
}
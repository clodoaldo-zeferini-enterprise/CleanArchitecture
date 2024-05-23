using Application.CQRS.Base.Members.Notifications;
using Application.CQRS.Command.Members.Commands;
using Application.DTO.Response;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using Infrastructure.Base.Abstractions.Member;
using MediatR;

namespace Application.CQRS.Command.Members.CommandsHandlers;

public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, MemberOutResponse>
{
    private readonly IMemberServiceCommand _memberServiceCommand;
    private readonly IValidator<CreateMemberCommand> _validator;
    private readonly IMediator _mediator;

    public CreateMemberCommandHandler(IMemberServiceCommand memberService,
                                      IValidator<CreateMemberCommand> validator,
                                      IMediator mediator)
    {
        _memberServiceCommand = memberService;
        _validator = validator;
        _mediator = mediator;
    }
    public async Task<MemberOutResponse> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);
        var newMember = new Member(request.FirstName, request.LastName, request.Gender, request.Email, request.UserId);
        
        var result = await _memberServiceCommand.CreateMember(newMember);

        await _mediator.Publish(new MemberNotification(result, result.Resultado ? Domain.Enums.EStatusCommand.CREATED_WITH_SUCCESS : Domain.Enums.EStatusCommand.FAILED_TO_CREATE), cancellationToken);
        
        return result;
    }
}



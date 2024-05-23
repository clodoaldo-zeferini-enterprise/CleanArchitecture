using Domain.Enums;

namespace Application.CQRS.Command.Members.Commands;

public class UpdateMemberCommand : MemberCommandBase
{
    public string Id { get; private set; }

    public UpdateMemberCommand(string userId, string id, string? firstName, string? lastName, EGenero gender, string? email, EStatus status) : base(userId, firstName, lastName, gender, email, status)
    {
        Id = id;
    }
}

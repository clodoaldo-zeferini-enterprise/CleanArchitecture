using Domain.Enums;

namespace Application.CQRS.Command.Members.Commands;
public sealed class DeleteMemberCommand : MemberCommandBase
{
    public string Id { get; private set; }
    public DeleteMemberCommand(string id, string userId, string? firstName, string? lastName, EGenero gender, string? email, EStatus status) : base(userId, firstName, lastName, gender, email, status)
    {
        Id = id;
    }
}

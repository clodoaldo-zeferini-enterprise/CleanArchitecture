using Domain.Enums;

namespace Application.CQRS.Command.Members.Commands;
public class CreateMemberCommand : MemberCommandBase
{
    public CreateMemberCommand(string userId, string? firstName, string? lastName, EGenero gender, string? email) : base(userId, firstName, lastName, gender, email, EStatus.ATIVO)
    {
    }
}

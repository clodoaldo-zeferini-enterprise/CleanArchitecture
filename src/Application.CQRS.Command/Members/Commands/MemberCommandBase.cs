using Domain.Enums;
using MediatR;
using Application.DTO.Response;

namespace Application.CQRS.Command.Members.Commands;

public abstract class MemberCommandBase : IRequest<MemberOutResponse>
{
    public string UserId { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public Domain.Enums.EGenero Gender { get; private set; }
    public string? Email { get; private set; }
    public EStatus Status { get; set; }

    protected MemberCommandBase(string userId, string? firstName, string? lastName, EGenero gender, string? email, EStatus status)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Email = email;
        Status = status;
    }
}

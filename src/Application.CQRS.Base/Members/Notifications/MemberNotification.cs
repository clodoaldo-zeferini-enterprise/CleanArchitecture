using Domain.Response;
using Domain.Enums;
using MediatR;


namespace Application.CQRS.Base.Members.Notifications;

public class MemberNotification : INotification
{
    public EStatusCommand EStatusCommand { get; }
    public MemberOutResponse MemberOutResponse { get; }

    public MemberNotification(MemberOutResponse memberOutResponse, EStatusCommand eStatusCommand)
    {
        MemberOutResponse = memberOutResponse;
        EStatusCommand = eStatusCommand;
    }
}

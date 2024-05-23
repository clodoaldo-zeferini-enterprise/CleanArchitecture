using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Base.Members.Notifications;

public class MemberCreatedSMSHandler : INotificationHandler<MemberNotification>
{
    private readonly ILogger<MemberCreatedSMSHandler> _logger;

    public MemberCreatedSMSHandler(ILogger<MemberCreatedSMSHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(MemberNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Confirmation email sent for : Teste");

        //logica para enviar SMS

        return Task.CompletedTask;
    }
}

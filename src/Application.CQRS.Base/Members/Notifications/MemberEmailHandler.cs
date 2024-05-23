using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Base.Members.Notifications;

public class MemberEmailHandler : INotificationHandler<MemberNotification>
{
    private readonly ILogger<MemberEmailHandler>? _logger;

    public MemberEmailHandler(ILogger<MemberEmailHandler>? logger)
    {
        _logger = logger;
    }

    public Task Handle(MemberNotification notification, CancellationToken cancellationToken)
    {

        // Send a confirmation email
        _logger.LogInformation($"Confirmation email sent for : Teste");

        //lógica para enviar email   

        return Task.CompletedTask;
    }
}

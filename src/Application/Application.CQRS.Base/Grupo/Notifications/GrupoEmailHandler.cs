using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Base.Grupo.Notifications;

public class GrupoEmailHandler : INotificationHandler<GrupoNotification>
{
    private readonly ILogger<GrupoEmailHandler>? _logger;

    public GrupoEmailHandler(ILogger<GrupoEmailHandler>? logger)
    {
        _logger = logger;
    }

    public Task Handle(GrupoNotification notification, CancellationToken cancellationToken)
    {

        // Send a confirmation email
        _logger.LogInformation($"Confirmation email sent for : Teste");

        //lógica para enviar email   

        return Task.CompletedTask;
    }
}

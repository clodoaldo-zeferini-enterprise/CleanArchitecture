using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Base.Grupo.Notifications;

public class GrupoCreatedSMSHandler : INotificationHandler<GrupoNotification>
{
    private readonly ILogger<GrupoCreatedSMSHandler> _logger;

    public GrupoCreatedSMSHandler(ILogger<GrupoCreatedSMSHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(GrupoNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Confirmation email sent for : Teste");

        //logica para enviar SMS

        return Task.CompletedTask;
    }
}

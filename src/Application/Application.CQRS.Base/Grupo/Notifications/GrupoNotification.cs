
using Application.DTO.Response;
using NetCore.Base.Enum;
using MediatR;
using Domain.Enums;


namespace Application.CQRS.Base.Grupo.Notifications;

public class GrupoNotification : INotification
{
    public EStatusCommand EStatusCommand { get; }
    public GrupoOutResponse GrupoOutResponse { get; }

    public GrupoNotification(GrupoOutResponse grupoOutResponse, EStatusCommand eStatusCommand)
    {
        GrupoOutResponse = grupoOutResponse;
        EStatusCommand = eStatusCommand;
    }
}

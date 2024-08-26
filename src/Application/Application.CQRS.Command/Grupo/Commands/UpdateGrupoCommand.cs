using NetCore.Base.Enum;

namespace Application.CQRS.Command.Grupo.Commands;

public sealed class UpdateGrupoCommand : GrupoCommandBase
{
    public string Id { get; private set; }
    public string UserId { get; private set; }
    public EStatus Status { get; private set; }


    public UpdateGrupoCommand(string id, string userId, EStatus status)
    {
        Id = id;
        UserId = userId;
        Status = status;
    }
}
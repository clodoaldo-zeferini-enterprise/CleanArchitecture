using NetCore.Base.Enum;

namespace Application.CQRS.Command.Grupo.Commands;
public sealed class DeleteGrupoCommand : GrupoCommandBase
{
    public string Id { get; private set; }
    public string UserId { get; private set; }

    public DeleteGrupoCommand(string id, string userId)
    {
        Id = id;
        UserId = userId;
    }
}

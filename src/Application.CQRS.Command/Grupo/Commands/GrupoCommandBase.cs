using NetCore.Base.Enum;
using MediatR;
using Application.DTO.Response;
using NetCore.Base;
using Domain.Entities;

namespace Application.CQRS.Command.Grupo.Commands;

public abstract class GrupoCommandBase : IRequest<GrupoOutResponse>
{
    
}

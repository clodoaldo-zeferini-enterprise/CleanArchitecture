using Application.CQRS.Base.Grupo.Notifications;
using Application.CQRS.Command.Grupo.Commands;
using Application.DTO.Response;
using Domain.Builders;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Base.Abstractions.Grupo;
using MediatR;

namespace Application.CQRS.Command.Grupo.CommandsHandlers;

public class CreateGrupoCommandHandler : IRequestHandler<CreateGrupoCommand, GrupoOutResponse>
{
    private readonly IGrupoServiceCommand _grupoServiceCommand;
    private readonly IValidator<CreateGrupoCommand> _validator;
    private readonly IMediator _mediator;

    public CreateGrupoCommandHandler(IGrupoServiceCommand grupoService,
                                      IValidator<CreateGrupoCommand> validator,
                                      IMediator mediator)
    {
        _grupoServiceCommand = grupoService;
        _validator = validator;
        _mediator = mediator;
    }
    public async Task<GrupoOutResponse> Handle(CreateGrupoCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);




        var grupoBuilder = new GrupoBuilder()
                .ComNomeDoGrupo(request.NomeDoGrupo)
                .ComRazaoSocial(request.RazaoSocial)
                .ComNomeFantasia(request.NomeFantasia)
                .ComCnpj(request.Cnpj)
                .ComInscricaoEstadual(request.InscricaoEstadual)
                .ComCpfDoAdministrador(request.CpfDoAdministrador)
                .ComPreNomeDoAdministrador(request.PreNomeDoAdministrador)
                .ComNomeDoMeioDoAdministrador(request.NomeDoMeioDoAdministrador)
                .ComSobreNomeDoAdministrador(request.SobreNomeDoAdministrador)
                .ComEmailDoAdministrador(request.EmailDoAdministrador)
                .ComInsertedBy(request.InsertedBy);        

        var result = await _grupoServiceCommand.CreateGrupo(grupoBuilder.BuildForCreate());

        await _mediator.Publish(new GrupoNotification(result, result.Resultado ? Domain.Enums.EStatusCommand.CREATED_WITH_SUCCESS : Domain.Enums.EStatusCommand.FAILED_TO_CREATE), cancellationToken);
        
        return result;
    }
}



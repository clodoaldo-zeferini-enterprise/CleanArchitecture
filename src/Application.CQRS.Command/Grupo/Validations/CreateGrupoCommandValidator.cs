using Application.CQRS.Command.Grupo.Commands;
using Domain.Validation;
using FluentValidation;
namespace Application.CQRS.Command.Grupo.Validations;
public class CreateGrupoCommandValidator : AbstractValidator<CreateGrupoCommand>
{
    public CreateGrupoCommandValidator()
    {
        RuleFor(c => c.FirstName)
          .NotEmpty().WithMessage(GrupoResources.FirstNameIsRequired)
          .Length(GrupoResources.FirstNameMinLength, GrupoResources.FirstNameMaxLength).WithMessage(GrupoResources.FirstNameLengthMessage);

        RuleFor(c => c.LastName)
         .NotEmpty().WithMessage(GrupoResources.LastNameIsRequired)
         .Length(GrupoResources.LastNameMinLength, GrupoResources.LastNameMaxLength).WithMessage(GrupoResources.LastNameLengthMessage);

        RuleFor(c => c.Gender).Must(p => p.GetType() == typeof(Domain.Enums.EGenero))
            .WithMessage(GrupoResources.GenderIsRequired);

        RuleFor(c => c.Email)
           .NotEmpty().WithMessage(GrupoResources.EmailIsRequired)
           .Length(GrupoResources.EmailMinLength, GrupoResources.EmailMaxLength).WithMessage(GrupoResources.LastNameLengthMessage)
           .EmailAddress();
    }
}

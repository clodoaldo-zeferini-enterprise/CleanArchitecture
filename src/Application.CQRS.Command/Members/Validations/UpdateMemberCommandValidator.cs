using Application.CQRS.Command.Members.Commands;
using Domain.Validation;
using FluentValidation;
namespace Application.CQRS.Command.Members.Validations;
public class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
{
    public UpdateMemberCommandValidator()
    {
        RuleFor(x => x.Id.ToString()).NotNull().NotEqual(string.Empty);

        RuleFor(c => c.LastName)
         .NotEmpty().WithMessage(MemberResources.LastNameIsRequired)
         .Length(MemberResources.LastNameMinLength, MemberResources.LastNameMaxLength).WithMessage(MemberResources.LastNameLengthMessage);

        RuleFor(c => c.Gender).Must(p => p.GetType() == typeof(Domain.Enums.EGenero))
            .WithMessage(MemberResources.GenderIsRequired);

        RuleFor(c => c.Email)
           .NotEmpty().WithMessage(MemberResources.EmailIsRequired)
           .Length(MemberResources.EmailMinLength, MemberResources.EmailMaxLength).WithMessage(MemberResources.LastNameLengthMessage)
           .EmailAddress();

    }
}

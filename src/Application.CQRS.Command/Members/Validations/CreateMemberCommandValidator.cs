using Application.CQRS.Command.Members.Commands;
using Domain.Validation;
using FluentValidation;
namespace Application.CQRS.Command.Members.Validations;
public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidator()
    {
        RuleFor(c => c.FirstName)
          .NotEmpty().WithMessage(MemberResources.FirstNameIsRequired)
          .Length(MemberResources.FirstNameMinLength, MemberResources.FirstNameMaxLength).WithMessage(MemberResources.FirstNameLengthMessage);

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

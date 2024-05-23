using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validation
{
    public static class MemberValidate
    {
        public static void ValidateDomain(string? firstName, string? lastName, EGenero gender, string? email, string userId)
        {
            DomainValidation.When(string.IsNullOrEmpty(firstName), MemberResources.FirstNameIsRequired);
            DomainValidation.When(((firstName.Length < MemberResources.FirstNameMinLength) || (firstName.Length > MemberResources.FirstNameMaxLength)), MemberResources.FirstNameLengthMessage);

            DomainValidation.When(string.IsNullOrEmpty(lastName), MemberResources.LastNameIsRequired);
            DomainValidation.When(((lastName.Length < MemberResources.LastNameMinLength) || (lastName.Length > MemberResources.LastNameMaxLength)), MemberResources.LastNameLengthMessage);

            DomainValidation.When(((string.IsNullOrEmpty(email)) || (email?.Length < 6) || (email?.Length > 250)), MemberResources.EmailLengthMessage);
        }
    }
}

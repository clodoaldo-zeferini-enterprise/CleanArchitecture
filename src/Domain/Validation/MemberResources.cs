using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validation
{
    public static class GrupoResources
    {
        public static readonly string IdIsRequired = "Please ensure you have entered the Id";
        public static readonly int IdMinLength = 36;
        public static readonly int IdMaxLength = 36;

        public static readonly string FirstNameIsRequired = "Please ensure you have entered the FirstName";
        public static readonly int FirstNameMinLength = 4;
        public static readonly int FirstNameMaxLength = 100;
        public static readonly string FirstNameLengthMessage = $@"The FirstName must have between {FirstNameMinLength} and {FirstNameMaxLength} characters";

        public static readonly string LastNameIsRequired = "Please ensure you have entered the LastName";
        public static readonly int LastNameMinLength = 4;
        public static readonly int LastNameMaxLength = 100;
        public static readonly string LastNameLengthMessage = $@"The FirstName must have between  {LastNameMinLength} and {LastNameMaxLength} characters";

        public static readonly string EmailIsRequired = "Please ensure you have entered the Email";
        public static readonly int EmailMinLength = 6;
        public static readonly int EmailMaxLength = 250;
        public static readonly string EmailLengthMessage = $@"The Email must have between  {EmailMinLength} and {EmailMaxLength} characters";

        public static readonly string GenderIsRequired = "Please ensure you have entered the Gender";

        public static readonly string ActiveIsRequired = "Please ensure you have entered the ActiveIs";

    }
}

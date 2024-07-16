using NetCore.Base.Enum;

namespace Domain.Validation
{
    public static class GrupoValidate
    {
        public static void ValidateDomain(string firstName, string lastName, Domain.Enums.EGenero gender, string email, string userId)
        {
            NetCore.Base.ValidadorDeRegra
                .Novo()
                .Quando(string.IsNullOrEmpty(firstName), GrupoResources.FirstNameIsRequired)
                .Quando(((firstName.Length < GrupoResources.FirstNameMinLength) || (firstName.Length > GrupoResources.FirstNameMaxLength)), GrupoResources.FirstNameLengthMessage)
                .Quando(string.IsNullOrEmpty(lastName), GrupoResources.LastNameIsRequired)
                .Quando(((lastName.Length < GrupoResources.LastNameMinLength) || (lastName.Length > GrupoResources.LastNameMaxLength)), GrupoResources.LastNameLengthMessage)
                .Quando(((string.IsNullOrEmpty(email)) || (email.Length < 6) || (email.Length > 250)), GrupoResources.EmailLengthMessage)
                .DispararExcecaoSeExistir();
        }
    }
}

using NetCore.Base.Enum;

namespace Domain.Validation
{
    public static class GrupoValidate
    {
        public static void ValidateDomain(string name)
        {
            NetCore.Base.ValidadorDeRegra
                .Novo()
                .Quando(string.IsNullOrEmpty(name), GrupoResources.NomeDoGrupoIsRequired)
                .Quando(((name.Length < GrupoResources.NomeDoGrupoMinLength) || (name.Length > GrupoResources.NomeDoGrupoMaxLength)), GrupoResources.NomeDoGrupoLengthMessage)
                .DispararExcecaoSeExistir();
        }
    }
}

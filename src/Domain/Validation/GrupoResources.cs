namespace Domain.Validation
{
    public static class GrupoResources
    {
        public static readonly string IdIsRequired = "Por favor, assegure-se de ter informado o Id";
        public static readonly int IdMinLength = 36;
        public static readonly int IdMaxLength = 36;

        public static readonly string NomeDoGrupoIsRequired = "Por favor, assegure-se de ter informado o Nome Do Grupo";
        public static readonly int    NomeDoGrupoMinLength = 4;
        public static readonly int    NomeDoGrupoMaxLength = 100;
        public static readonly string NomeDoGrupoLengthMessage = $@"O Nome Do Grupo deve estar entre {NomeDoGrupoMinLength} e {NomeDoGrupoMaxLength} caracteres";


    }
}

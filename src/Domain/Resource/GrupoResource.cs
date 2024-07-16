namespace Domain.Resource
{
    public static class GrupoResource
    {
        public static readonly string IdIsRequired = "Please ensure you have entered the Id";
        public static readonly int IdMinLength = 36;
        public static readonly int IdMaxLength = 36;

        public static readonly string NameIsRequired = "Please ensure you have entered the Name";
        public static readonly int NameMinLength = 4;
        public static readonly int NameMaxLength = 100;
        public static readonly string NameLengthMessage = $@"The Name must have between {NameMinLength} and {NameMaxLength} characters";

        public static readonly string InsertedByIsRequired = "Please ensure you have entered the InsertedBy";

        public static readonly string UpdatedByIsRequired = "Please ensure you have entered the UpdatedBy";

        public static readonly string StatusIsRequired = "Please ensure you have entered the Status";

        public static readonly string EmpresasIsRequired = "Please ensure you have entered the Empresas";
    }
}

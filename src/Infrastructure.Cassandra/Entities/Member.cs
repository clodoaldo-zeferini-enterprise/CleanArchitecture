using NetCore.Base.Enum;
using Domain.Validation;
using NetCore.Base;


namespace Infrastructure.Cassandra.Entities
{
    public class Member : Root
    {
        public string Id { get; set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public Domain.Enums.EGenero Gender { get; private set; }
        public string? Email { get; private set; }        
    }
}

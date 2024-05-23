using Domain.Enums;
using Domain.Validation;

namespace Domain.Entities;

public sealed class Member : Base
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public Domain.Enums.EGenero Gender { get; private set; }
    public string? Email { get; private set; }    
     
    private Member() { }

    public Member(string? firstName, string? lastName, EGenero gender, string? email, string userId)
    {
        MemberValidate.ValidateDomain(firstName, lastName, gender, email, userId);
        
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Email = email;

        Id = Guid.NewGuid().ToString();
        InsertedOn = DateTime.Now;
        InsertedBy = userId;

        UpdatedOn = InsertedOn;
        UpdatedBy = InsertedBy;

        Name = $@"{FirstName} {LastName}";
        Status = EStatus.ATIVO;
    }
    public Member(string id, string? firstName, string? lastName, EGenero gender, string? email, EStatus status, DateTime insertedOn, string insertedBy, DateTime updatedOn, string updatedBy)
    {
        MemberValidate.ValidateDomain(firstName, lastName, gender, email, insertedBy);

        Id = id;

        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Status = status;
        Email = email;

        InsertedOn = insertedOn;
        InsertedBy = insertedBy;

        UpdatedOn = updatedOn;
        UpdatedBy = updatedBy;

        Name = $@"{firstName} {lastName}";    
    }
}

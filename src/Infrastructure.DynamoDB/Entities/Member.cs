using Amazon.DynamoDBv2.DataModel;
using Domain.Enums;
using Domain.Validation;

namespace Infrastructure.DynamoDB.Entities;

[DynamoDBTable("Members")]
public sealed class Member : Domain.Entities.Base
{
    [DynamoDBHashKey]  public string Id { get; private set; }
    [DynamoDBProperty] public string Name { get; private set; }
    [DynamoDBProperty] public string? FirstName { get; private set; }
    [DynamoDBProperty] public string? LastName { get; private set; }
    [DynamoDBProperty] public Domain.Enums.EGenero Gender { get; private set; }
    [DynamoDBProperty] public string? Email { get; private set; }    
     
    public Member() { }

    /*Insert*/
    public Member(string? firstName, string? lastName, EGenero gender, string? email, EStatus status, string userId)
    {
        MemberValidate.ValidateDomain(firstName, lastName, gender, email, userId);

        Id = Guid.NewGuid().ToString();
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Status = EStatus.ATIVO;
        Email = email;

        InsertedOn = DateTime.Now;
        InsertedBy = userId.ToString();
        Name = $@"{firstName} {lastName}";
    }

    /*
    public Member(string id, string? firstName, string? lastName, EGenero gender, string? email,  EStatus status, string userId)
    {
        MemberValidate.ValidateDomain(firstName, lastName, gender, email, userId);

        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Status = status;
        Email = email;

        base.Id = id.ToString();
        UpdatedOn = DateTime.Now;
        UpdatedBy = userId.ToString();
        Name = $@"{firstName} {lastName}";

    }
    */
    public Member(string id, string? firstName, string? lastName, EGenero gender, string? email,  EStatus status, DateTime insertedOn, string insertedBy, DateTime updatedOn, string updatedBy)
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

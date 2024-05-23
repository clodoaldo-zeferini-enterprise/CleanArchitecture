namespace Domain.Response.Member;

public sealed class Member : Domain.Entities.Base
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public Domain.Enums.EGenero Gender { get; private set; }
    public string? Email { get; private set; }


    private Member() { }


    public Member (Domain.Entities.Member member)
    {
        FirstName = member.FirstName;
        LastName = member.LastName;
        Name = member.Name;
        Gender = member.Gender;
        Status = member.Status;
        Email = member.Email;

        Id = member.Id;
        InsertedOn = member.InsertedOn;
        InsertedBy = member.InsertedBy;
        UpdatedOn = member.UpdatedOn;
        InsertedOn = member.InsertedOn;        
    }
}

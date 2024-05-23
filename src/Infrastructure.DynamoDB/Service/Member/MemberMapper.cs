using Application.DTO.Response.Member;

namespace Infrastructure.DynamoDB.Service.Member
{
    public static class MemberMapper
    {
        public static MemberResponse FromDynamoDBToDTO(List<Infrastructure.DynamoDB.Entities.Member> members)
        {
            if (members.Any())
            {
                List<Application.DTO.Response.Member.Member> membersDTO = new List<Application.DTO.Response.Member.Member>();

                foreach (var member in members)
                {
                    Domain.Entities.Member memberDomain = new Domain.Entities.Member(
                    member.Id,
                    member.FirstName,
                    member.LastName,
                    member.Gender,
                    member.Email,
                    member.Status,
                    member.InsertedOn,
                    member.InsertedBy,
                    member.UpdatedOn,
                    member.UpdatedBy
                    );

                    membersDTO.Add(new Application.DTO.Response.Member.Member(memberDomain));
                }

                return new MemberResponse(membersDTO);
            }

            return null;
        }

        public static MemberResponse FromEntityToDTO(Domain.Entities.Member member)
        {
            List<Application.DTO.Response.Member.Member> membersDTO = new List<Application.DTO.Response.Member.Member>();
            membersDTO.Add(new Application.DTO.Response.Member.Member(member));
            return new MemberResponse(membersDTO);       
        }
    }
}

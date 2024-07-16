using Application.DTO.Response.Member;

namespace Infrastructure.Cassandra.Service.Member
{
    public static class MemberMapper
    {
        public static MemberResponse FromCassandraToDTO(List<Infrastructure.Cassandra.Entities.Member> members)
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

        public static Entities.Member FromEntityToCassandra(Domain.Entities.Member member)
        {
            Entities.Member memberCassandra = new Entities.Member(
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

            return memberCassandra;
        }

        public static Domain.Entities.Member FromCassandraToEntity(Entities.Member member)
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

            return memberDomain;
        }

        public static MemberResponse FromEntityToDTO(Domain.Entities.Member member)
        {
            List<Application.DTO.Response.Member.Member> membersDTO = new List<Application.DTO.Response.Member.Member>();
            membersDTO.Add(new Application.DTO.Response.Member.Member(member));
            return new MemberResponse(membersDTO);       
        }
    }
}

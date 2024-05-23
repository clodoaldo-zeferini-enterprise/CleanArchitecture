using Application.DTO.Response.Member;

namespace Infrastructure.SQLServer.Service.Member
{
    public static class MemberMapper
    {
        public static MemberResponse FromSQLDBToDTO(
            IEnumerable<Application.DTO.Response.Navigator> navigators,
            IEnumerable<Domain.Entities.Member> members)
        {
            List<Application.DTO.Response.Member.Member> membersDTO = new List<Application.DTO.Response.Member.Member>();
            List<Application.DTO.Response.Navigator> navigatorsDTO = new List<Application.DTO.Response.Navigator>();

            if (members.Any())
            {
                foreach (var member in members)
                {
                    membersDTO.Add(new Application.DTO.Response.Member.Member(member));
                }                
            }

            if (navigators.Any())
            {
                foreach (var navigator in navigators)
                {
                    navigatorsDTO.Add(navigator);
                }
            }

            return new MemberResponse(navigatorsDTO, membersDTO);

        }
        public static MemberResponse FromSQLDBToDTO(List<Domain.Entities.Member> members)
        {
            if (members.Any())
            {
                List<Application.DTO.Response.Member.Member> membersDTO = new List<Application.DTO.Response.Member.Member>();

                foreach (var member in members)
                {  
                    membersDTO.Add(new Application.DTO.Response.Member.Member(member));
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
        public static MemberResponse FromEntityToDTO(IEnumerable<Domain.Entities.Member> enumMember)
        {
            List<Application.DTO.Response.Member.Member> membersDTO = new List<Application.DTO.Response.Member.Member>();
            foreach (var member in enumMember)
            {
                membersDTO.Add(new Application.DTO.Response.Member.Member(member));
            }

            return new MemberResponse(membersDTO);
        }
    }
}

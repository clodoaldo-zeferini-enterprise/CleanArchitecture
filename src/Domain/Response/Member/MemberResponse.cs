using Domain.Entities;
using Domain.Enums;
using System.Collections.Generic;

namespace Domain.Response.Member
{
    public class MemberResponse
    {
        public List<Navigator> Navigators { get; private set; }
        public List<Member> Members { get; private set; }

        public MemberResponse(List<Domain.Response.Member.Member> members)
        {
            Navigators = new List<Navigator>();
            Members = members;

            Navigator navigator = new Navigator(1, 1, 1, 1);
            Navigators.Add(navigator);
        }

        public MemberResponse(List<Navigator> navigators, List<Domain.Response.Member.Member> members)
        {
            Navigators = navigators;
            Members = new List<Member>();

            if (members != null)
            {
                Members.AddRange(members);
            }
        }
    }
}

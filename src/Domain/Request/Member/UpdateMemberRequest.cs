using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request.Member
{
    public class UpdateMemberRequest
    {
        public string Id { get; private set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public Domain.Enums.EGenero Gender { get; private set; }
        public string? Email { get; private set; }

        public EStatus Status { get; private set; }
        public string UserId { get; private set; }

        public UpdateMemberRequest(string id, string? firstName, string? lastName, EGenero gender, string? email, EStatus status, string userId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Email = email;
            Status = status;
            UserId = userId;
        }
    }
}

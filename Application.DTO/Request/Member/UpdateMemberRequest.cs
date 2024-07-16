using NetCore.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Request.Grupo
{
    public class UpdateGrupoRequest
    {
        public string Id { get; private set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public Domain.Enums.EGenero Gender { get; private set; }
        public string? Email { get; private set; }

        public EStatus Status { get; private set; }
        public string UserId { get; private set; }

        public UpdateGrupoRequest(string id, string? firstName, string? lastName, Domain.Enums.EGenero gender, string? email, EStatus status, string userId)
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

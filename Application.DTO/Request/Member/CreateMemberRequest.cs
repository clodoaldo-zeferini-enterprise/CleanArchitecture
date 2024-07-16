using NetCore.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Request.Grupo
{
    public class CreateGrupoRequest
    {
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public Domain.Enums.EGenero Gender { get; private set; }
        public string? Email { get; private set; }
        public string UserId { get; private set; }

        public CreateGrupoRequest(string? firstName, string? lastName, Domain.Enums.EGenero gender, string? email, string userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Email = email;
            UserId = userId;
        }
    }
}

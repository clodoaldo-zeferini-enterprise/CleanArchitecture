using NetCore.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Request.Grupo
{
    public class DeleteGrupoRequest
    {
        public string Id { get; set; }
        public string DeletedBy { get; set; }

        public DeleteGrupoRequest(string id, string deletedBy)
        {
            Id = id;
            DeletedBy = deletedBy;
        }
    }
}

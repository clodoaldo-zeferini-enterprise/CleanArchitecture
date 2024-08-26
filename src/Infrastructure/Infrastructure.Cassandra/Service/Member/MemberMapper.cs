using Application.DTO.Response.Grupo;

namespace Infrastructure.Cassandra.Service.Grupo
{
    public static class GrupoMapper
    {
        public static GrupoResponse FromCassandraToDTO(List<Infrastructure.Cassandra.Entities.Grupo> members)
        {
            if (members.Any())
            {
                List<Application.DTO.Response.Grupo.Grupo> membersDTO = new List<Application.DTO.Response.Grupo.Grupo>();

                foreach (var member in members)
                {
                    Domain.Entities.Grupo memberDomain = new Domain.Entities.Grupo(
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

                    membersDTO.Add(new Application.DTO.Response.Grupo.Grupo(memberDomain));
                }

                return new GrupoResponse(membersDTO);
            }

            return null;
        }

        public static Entities.Grupo FromEntityToCassandra(Domain.Entities.Grupo member)
        {
            Entities.Grupo memberCassandra = new Entities.Grupo(
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

        public static Domain.Entities.Grupo FromCassandraToEntity(Entities.Grupo member)
        {
            Domain.Entities.Grupo memberDomain = new Domain.Entities.Grupo(
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

        public static GrupoResponse FromEntityToDTO(Domain.Entities.Grupo member)
        {
            List<Application.DTO.Response.Grupo.Grupo> membersDTO = new List<Application.DTO.Response.Grupo.Grupo>();
            membersDTO.Add(new Application.DTO.Response.Grupo.Grupo(member));
            return new GrupoResponse(membersDTO);       
        }
    }
}

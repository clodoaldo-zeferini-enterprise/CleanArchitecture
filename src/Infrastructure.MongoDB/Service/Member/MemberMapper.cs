using Application.DTO.Response.Grupo;
using MongoDB.Bson;

namespace Infrastructure.MongoDB.Service.Grupo
{
    public static class GrupoMapper
    {
        public static GrupoResponse FromMongoDBToDTO(List<Infrastructure.MongoDB.Entities.Grupo> grupos)
        {
            if (grupos.Any())
            {
                List<Application.DTO.Response.Grupo.Grupo> gruposDTO = new List<Application.DTO.Response.Grupo.Grupo>();

                foreach (var grupo in grupos)
                {
                    Domain.Entities.Grupo grupoDomain = new Domain.Entities.Grupo(
                    grupo.Id,
                    grupo.FirstName,
                    grupo.LastName,
                    grupo.Gender,
                    grupo.Email,
                    grupo.Status,
                    grupo.InsertedOn,
                    grupo.InsertedBy,
                    grupo.UpdatedOn,
                    grupo.UpdatedBy
                    );

                    gruposDTO.Add(new Application.DTO.Response.Grupo.Grupo(grupoDomain));
                }

                return new GrupoResponse(gruposDTO);
            }

            return null;
        }

        public static GrupoResponse FromMongoDBToDTO(Infrastructure.MongoDB.Entities.Grupo grupo)
        {
            if (grupo != null)
            {
                List<Application.DTO.Response.Grupo.Grupo> gruposDTO = new List<Application.DTO.Response.Grupo.Grupo>();

                Domain.Entities.Grupo grupoDomain = new Domain.Entities.Grupo(
                grupo.Id,
                grupo.FirstName,
                grupo.LastName,
                grupo.Gender,
                grupo.Email,
                grupo.Status,
                grupo.InsertedOn,
                grupo.InsertedBy,
                grupo.UpdatedOn,
                grupo.UpdatedBy);

                gruposDTO.Add(new Application.DTO.Response.Grupo.Grupo(grupoDomain));                

                return new GrupoResponse(gruposDTO);
            }

            return null;
        }

        public static Entities.Grupo FromEntityToMongoDB(Domain.Entities.Grupo grupo)
        {
            Entities.Grupo grupoMongoDB = new Entities.Grupo(
                   grupo.Id,
                   ObjectId.GenerateNewId(),
                   grupo.FirstName,
                   grupo.LastName,
                   grupo.Gender,
                   grupo.Email,
                   grupo.Status,
                   grupo.InsertedOn,
                   grupo.InsertedBy,
                   grupo.UpdatedOn,
                   grupo.UpdatedBy
                   );

            return grupoMongoDB;
        }

        public static Domain.Entities.Grupo FromMongoDBToEntity(Entities.Grupo grupo)
        {
            Domain.Entities.Grupo grupoDomain = new Domain.Entities.Grupo(
                   grupo.Id,
                   grupo.FirstName,
                   grupo.LastName,
                   grupo.Gender,
                   grupo.Email,
                   grupo.Status,
                   grupo.InsertedOn,
                   grupo.InsertedBy,
                   grupo.UpdatedOn,
                   grupo.UpdatedBy
                   );

            return grupoDomain;
        }

        public static GrupoResponse FromEntityToDTO(Domain.Entities.Grupo grupo)
        {
            List<Application.DTO.Response.Grupo.Grupo> gruposDTO = new List<Application.DTO.Response.Grupo.Grupo>();
            gruposDTO.Add(new Application.DTO.Response.Grupo.Grupo(grupo));
            return new GrupoResponse(gruposDTO);       
        }
    }
}

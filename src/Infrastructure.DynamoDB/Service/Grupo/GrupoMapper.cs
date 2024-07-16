﻿using Application.DTO.Response.Grupo;

namespace Infrastructure.DynamoDB.Service.Grupo
{
    public static class GrupoMapper
    {
        public static GrupoResponse FromDynamoDBToDTO(List<Infrastructure.DynamoDB.Entities.Grupo> grupos)
        {
            if (grupos.Count > 0)
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

        public static GrupoResponse FromEntityToDTO(Domain.Entities.Grupo grupo)
        {
            List<Application.DTO.Response.Grupo.Grupo> gruposDTO = new List<Application.DTO.Response.Grupo.Grupo>();
            gruposDTO.Add(new Application.DTO.Response.Grupo.Grupo(grupo));
            return new GrupoResponse(gruposDTO);       
        }
    }
}

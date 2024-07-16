using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Application.DTO.Response;
using NetCore.Base.Enum;
using Infrastructure.Base.Abstractions.Grupo;
using Domain.Enums;

namespace Infrastructure.DynamoDB.Service.Grupo
{
    public class GrupoServiceCommand : IGrupoServiceCommand
    {
        private GrupoOutResponse _grupoOutResponse;
        private string _userId;

        private readonly IAmazonDynamoDB _dynamoDbClient;
        private readonly string _tableName = "Grupos";

        public GrupoServiceCommand(IAmazonDynamoDB dynamoDbClient, string userId)
        {
            _dynamoDbClient = dynamoDbClient;
            _grupoOutResponse = new GrupoOutResponse();
            _userId = userId;
        }

        private async Task<PutItemRequest> GeneratePutItemRequest(Domain.Entities.Grupo grupo)
        {
            var request = new PutItemRequest
            {
                TableName = _tableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    { "Id"         , new AttributeValue { S = grupo.Id } },
                    { "Name"       , new AttributeValue { S = grupo.Name } },
                    { "FirstName"  , new AttributeValue { S = grupo.FirstName } },
                    { "LastName"   , new AttributeValue { S = grupo.LastName } },
                    { "Gender"     , new AttributeValue { S = grupo.Gender.ToString() } },
                    { "Email"      , new AttributeValue { S = grupo.Email } },
                    { "Status"     , new AttributeValue { S = grupo.Status.ToString() } },
                    { "InsertedOn" , new AttributeValue { S = grupo.InsertedOn.ToString("yyyy-MM-dd HH:mm:ss.fff")} },
                    { "InsertedBy" , new AttributeValue { S = grupo.InsertedBy } },
                    { "UpdatedOn"  , new AttributeValue { S = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") } },
                    { "UpdatedBy"  , new AttributeValue { S = grupo.UpdatedBy } }
                }
            };

            return request;
        }
        public async Task<GrupoOutResponse> CreateGrupo(Domain.Entities.Grupo grupo)
        {
            /*Verifico se o Parâmetro é nulo*/
            if (grupo == null)
            {
                _grupoOutResponse.SetResultado(false);
                _grupoOutResponse.SetMensagem("O objeto recebido é nulo.");
                return _grupoOutResponse;
            }

            var updateResponse = await _dynamoDbClient.PutItemAsync(await GeneratePutItemRequest(grupo));

            if (updateResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                _grupoOutResponse.SetResultado(true);
                _grupoOutResponse.SetMensagem("Membro cadastrado com Sucesso!");
                _grupoOutResponse.SetData(GrupoMapper.FromEntityToDTO(grupo));
                return _grupoOutResponse;
            }
            else
            {
                _grupoOutResponse.SetResultado(false); return _grupoOutResponse;
            }
        }
        public async Task<GrupoOutResponse> DeleteGrupo(string id)
        {
            bool IsIdValido = false;

            if (id == null)
            {
                IsIdValido = false;
            }
            else
            {
                IsIdValido = Guid.TryParse(id.ToString(), out Guid isIdValido);
            }

            if (!IsIdValido)
            {
                _grupoOutResponse.SetResultado(false);
                _grupoOutResponse.SetMensagem("Id inválido.");
                return _grupoOutResponse;
            }

            var grupo = await GetGrupoById(id);

            if (grupo == null)
            {
                _grupoOutResponse.SetResultado(true);
                _grupoOutResponse.SetMensagem("Registro não encontrado!");
                return _grupoOutResponse;
            }

            grupo.Status = EStatus.EXCLUIDO;
            grupo.UpdatedBy = _userId;
            grupo.UpdatedOn = DateTime.Now;

            var updateResponse = await _dynamoDbClient.PutItemAsync(await GeneratePutItemRequest(grupo));

            if (updateResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                _grupoOutResponse.SetData(GrupoMapper.FromEntityToDTO(grupo));
                _grupoOutResponse.SetResultado(true);
                _grupoOutResponse.SetMensagem("Grupo Excluído com Sucesso!");

                return _grupoOutResponse;
            }
            else
            {
                _grupoOutResponse.SetResultado(false);
                _grupoOutResponse.SetMensagem("Ocorreu uma falha ao excluir o registro!");
                return _grupoOutResponse;
            }
        }

        public async Task<Domain.Entities.Grupo> GetGrupoById(string id)
        {
            bool IsIdValido = false;

            if (id == null)
            {
                IsIdValido = false;
            }
            else
            {
                IsIdValido = Guid.TryParse(id.ToString(), out Guid isIdValido);
            }

            if (!IsIdValido)
            {
                _grupoOutResponse.SetResultado(false);
                _grupoOutResponse.SetMensagem("Id inválido.");
                return null;
            }

            var getItemRequest = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "Id", new AttributeValue { S = id } }
                }
            };

            var getItemResponse = await _dynamoDbClient.GetItemAsync(getItemRequest);

            if (getItemResponse.Item == null)
            {
                _grupoOutResponse.SetMensagem("Registro não encontrado.");
                _grupoOutResponse.SetResultado(true);
                return null;
            }

            var grupo = new Domain.Entities.Grupo
            (
              getItemResponse.Item["Id"].S,
              getItemResponse.Item["FirstName"].S,
              getItemResponse.Item["LastName"].S,
              (EGenero)Enum.Parse(typeof(EGenero), getItemResponse.Item["Genero"].ToString()),
              getItemResponse.Item["Email"].S,
              (EStatus)Enum.Parse(typeof(EStatus), getItemResponse.Item["Status"].ToString()),
              DateTime.Parse(getItemResponse.Item["InsertedOn"].S),
              getItemResponse.Item["InsertedBy"].S,
              DateTime.Parse(getItemResponse.Item["UpdatedOn"].S),
              getItemResponse.Item["UpdatedBy"].S              
            );

            return grupo;            
        }

        public async Task<GrupoOutResponse> UpdateGrupo(Domain.Entities.Grupo grupo)
        {
            /*Verifico se o Parâmetro é nulo*/
            if (grupo == null)
            {
                _grupoOutResponse.SetResultado(false);
                _grupoOutResponse.SetMensagem("O objeto recebido é nulo.");
                return _grupoOutResponse;
            }

            var grupoExistente = await  GetGrupoById(grupo.Id);

            if (grupoExistente == null)
            {
                _grupoOutResponse.SetResultado(false);
                _grupoOutResponse.SetMensagem("Registro não encontrado!");
                return _grupoOutResponse;
            }

            var updateResponse = await _dynamoDbClient.PutItemAsync(await GeneratePutItemRequest(grupoExistente));

            if (updateResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                _grupoOutResponse.SetResultado(true);
                _grupoOutResponse.SetData(GrupoMapper.FromEntityToDTO(grupoExistente));
                _grupoOutResponse.SetMensagem("Grupo Atualizado com Sucesso!");

                return _grupoOutResponse;
            }
            else
            {
                _grupoOutResponse.SetResultado(false); return _grupoOutResponse;
            }
        }
    }
}

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Application.DTO.Request;
using Application.DTO.Response;
using Application.DTO.Response.Grupo;
using NetCore.Base.Enum;
using Infrastructure.Base.Abstractions.Grupo;
using Domain.Builders;

namespace Infrastructure.DynamoDB.Service.Grupo
{
    public class GrupoServiceQuery : IGrupoServiceQuery
    {
        private GrupoOutResponse _grupoOutResponse;
        private string _userId;

        private readonly IAmazonDynamoDB _dynamoDbClient;
        private readonly string _tableName = "Grupos";

        public GrupoServiceQuery(IAmazonDynamoDB dynamoDbClient, string userId)
        {
            _dynamoDbClient = dynamoDbClient;
            _grupoOutResponse = new GrupoOutResponse();
            _userId = userId;
        }

        /*
        public async Task<GrupoOutResponse> GetGrupoById(string id)
        {
            var request = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "Id", new AttributeValue { S = id } }
                }
            };

            var response = await _dynamoDbClient.GetItemAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                _grupoOutResponse.SetData(response.Item);
                _grupoOutResponse.SetResultado(true);
            }
            else
            {
                _grupoOutResponse.SetResultado(false);
            }

            return _grupoOutResponse;
        }
        */
        private async Task<List<Infrastructure.DynamoDB.Entities.Grupo>> GetFilteredResultsAsync(GetFrom getFrom)
        {
            var request = new ScanRequest
            {
                TableName = _tableName,
                FilterExpression = BuildFilterExpression(getFrom),
                ExpressionAttributeNames = BuildExpressionAttributeNames(getFrom),
                ExpressionAttributeValues = BuildExpressionAttributeValues(getFrom),
                Limit = getFrom.PageSize
            };
            
            var response = await _dynamoDbClient.ScanAsync(request);

            return response.Items.Select(MapToEntity).ToList();
        }

        private string BuildFilterExpression(GetFrom getFrom)
        {
            var filterExpressions = new List<string>();

            if (!string.IsNullOrEmpty(getFrom.Id))
            {
                filterExpressions.Add("#id = :id");
            }

            if (getFrom.FiltraPorNome && !string.IsNullOrEmpty(getFrom.FiltroNome))
            {
                filterExpressions.Add("contains(#nome, :nome)");
            }

            if (getFrom.FiltraUpdatedOn)
            {
                if (getFrom.DataInicial.HasValue)
                {
                    filterExpressions.Add("#updatedOn >= :dataInicial");
                }
                if (getFrom.DataFinal.HasValue)
                {
                    filterExpressions.Add("#updatedOn <= :dataFinal");
                }
            }

            if (getFrom.FiltraStatus)
            {
                filterExpressions.Add("#status = :status");
            }

            return string.Join(" AND ", filterExpressions);
        }

        private Dictionary<string, string> BuildExpressionAttributeNames(GetFrom getFrom)
        {
            var attributeNames = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(getFrom.Id))
            {
                attributeNames.Add("#id", "Id");
            }

            if (getFrom.FiltraPorNome && !string.IsNullOrEmpty(getFrom.FiltroNome))
            {
                attributeNames.Add("#nome", "Nome");
            }

            if (getFrom.FiltraUpdatedOn)
            {
                attributeNames.Add("#updatedOn", "UpdatedOn");
            }

            if (getFrom.FiltraStatus)
            {
                attributeNames.Add("#status", "Status");
            }

            return attributeNames;
        }

        private Dictionary<string, AttributeValue> BuildExpressionAttributeValues(GetFrom getFrom)
        {
            var attributeValues = new Dictionary<string, AttributeValue>();

            if (!string.IsNullOrEmpty(getFrom.Id))
            {
                attributeValues.Add(":id", new AttributeValue { S = getFrom.Id });
            }

            if (getFrom.FiltraPorNome && !string.IsNullOrEmpty(getFrom.FiltroNome))
            {
                attributeValues.Add(":nome", new AttributeValue { S = getFrom.FiltroNome });
            }

            if (getFrom.FiltraUpdatedOn)
            {
                if (getFrom.DataInicial.HasValue)
                {
                    attributeValues.Add(":dataInicial", new AttributeValue { S = getFrom.DataInicial.Value.ToString("o") });
                }
                if (getFrom.DataFinal.HasValue)
                {
                    attributeValues.Add(":dataFinal", new AttributeValue { S = getFrom.DataFinal.Value.ToString("o") });
                }
            }

            if (getFrom.FiltraStatus)
            {
                attributeValues.Add(":status", new AttributeValue { S = getFrom.Status.ToString() });
            }

            return attributeValues;
        }

        private Domain.Entities.Grupo MapToEntity(Dictionary<string, AttributeValue> item)
        {
            /*string name, EStatus status, DateTime insertedOn, string insertedBy, string updatedBy*/

            EStatus eStatus = EStatus.AUSENTE;
            if (item.ContainsKey("Status"))
            {
                eStatus = Enum.Parse<EStatus>(item["Status"].S);
            }

            DateTime insertedOn = item.ContainsKey("InsertedOn") ? DateTime.Parse(item["InsertedOn"].S) : new DateTime();
            DateTime updatedOn = item.ContainsKey("UpdatedOn") ? DateTime.Parse(item["UpdatedOn"].S) : new DateTime();

            var grupoBuilder = new GrupoBuilder()                
                .ComId(string.IsNullOrEmpty(item["Id"].S) ? item["Id"].S : null)
                .ComNomeDoGrupo(item.ContainsKey("Name") ? item["Name"].S : null)
                .ComStatus(eStatus)
                .ComInsertedOn(insertedOn)
                .ComInsertedBy(item.ContainsKey("InsertedBy") ? item["InsertedBy"].S : null)
                .ComUpdatedOn(updatedOn)
                .ComUpdatedBy(item.ContainsKey("DeletedBy") ? item["DeletedBy"].S : null)
                .AdicionarEmpresa(item.ContainsKey("Empresas") ? item["Empresas"].SS : null)
                ;

            Domain.Entities.Grupo grupo = grupoBuilder.BuildForGet();

            return grupo;
        }
        
        public async Task<GrupoOutResponse> GetGrupos(GetFrom getFrom)
        {
            var listGruposFromDynamoDB = await GetFilteredResultsAsync(getFrom);

            _grupoOutResponse.SetResultado(true);
            _grupoOutResponse.SetData(GrupoMapper.FromDynamoDBToDTO(listGruposFromDynamoDB));

            return _grupoOutResponse;
        }

    }
}
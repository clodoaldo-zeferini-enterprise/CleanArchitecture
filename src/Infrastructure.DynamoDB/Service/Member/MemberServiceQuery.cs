using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Application.DTO.Request;
using Application.DTO.Response;
using Application.DTO.Response.Member;
using Domain.Enums;
using Infrastructure.Base.Abstractions.Member;

namespace Infrastructure.DynamoDB.Service.Member
{
    public class MemberServiceQuery : IMemberServiceQuery
    {
        private MemberOutResponse _memberOutResponse;
        private string _userId;

        private readonly IAmazonDynamoDB _dynamoDbClient;
        private readonly string _tableName = "Members";

        public MemberServiceQuery(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
            _memberOutResponse = new MemberOutResponse();
            _userId = Guid.NewGuid().ToString();
        }

        public async Task<MemberOutResponse> GetMemberById(string id)
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
                _memberOutResponse.SetData(response.Item);
                _memberOutResponse.SetResultado(true);
            }
            else
            {
                _memberOutResponse.SetResultado(false);
            }

            return _memberOutResponse;
        }

        public async Task<List<Infrastructure.DynamoDB.Entities.Member>> GetFilteredResultsAsync(GetFrom getFrom)
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

            return response.Items.Select(MapToYourDynamoDBEntity).ToList();
        }

        private string BuildFilterExpression(GetFrom getFrom)
        {
            var filterExpressions = new List<string>();

            if (!string.IsNullOrEmpty(getFrom.Id))
            {
                filterExpressions.Add("#id = :id");
            }

            if (getFrom.FiltraNome && !string.IsNullOrEmpty(getFrom.FiltroNome))
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

            if (getFrom.FiltraNome && !string.IsNullOrEmpty(getFrom.FiltroNome))
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

            if (getFrom.FiltraNome && !string.IsNullOrEmpty(getFrom.FiltroNome))
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

        private Infrastructure.DynamoDB.Entities.Member MapToYourDynamoDBEntity(Dictionary<string, AttributeValue> item)
        {
            /*string id, string? firstName, string? lastName, EGenero gender, string? email,  EStatus status, DateTime insertedOn, string insertedBy, DateTime updatedOn, string updatedBy*/
            return new Infrastructure.DynamoDB.Entities.Member
                (item.ContainsKey("Id") ? item["Id"].S : null,
                item.ContainsKey("FirstName") ? item["FirstName"].S : null,
                item.ContainsKey("LastName") ? item["LastName"].S : null,
                item.ContainsKey("Gender") ? Enum.Parse<EGenero>(item["Gender"].S) : default,
                item.ContainsKey("Email") ? item["Email"].S : null,
                item.ContainsKey("Status") ? Enum.Parse<EStatus>(item["Status"].S) : default,
                item.ContainsKey("InsertedOn") ? DateTime.Parse(item["InsertedOn"].S) : default,
                item.ContainsKey("InsertedBy") ? item["InsertedBy"].S : null,
                item.ContainsKey("UpdatedOn") ? DateTime.Parse(item["UpdatedOn"].S) : default,
                item.ContainsKey("UpdatedBy") ? item["UpdatedBy"].S : null);
        }
        
        public async Task<MemberOutResponse> GetMembers(GetFrom getFrom)
        {
            var listMembersFromDynamoDB = await GetFilteredResultsAsync(getFrom);

            /*Converter a lista do DynamoDB para DTO */

           // memberMapper memberMapper = new MemberMapper();

            //MemberResponse response = new MemberResponse(new List<Navigator>(), );
            /*
                return results.Skip((getFrom.PageNumber - 1) * getFrom.PageSize)
                          .Take(getFrom.PageSize)
                          .ToList();
            */

            /*
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                _memberOutResponse.SetData(response.Items);
                _memberOutResponse.SetResultado(true);
            }
            else
            {
                _memberOutResponse.SetResultado(false);
            }
            */

            return _memberOutResponse;
        }

        /*
        public async Task<MemberOutResponse> GetMembers(GetFrom getFrom)
        {
            var result = _membersCollection.Find(

                x =>
            
                  getFrom.Id != null && x.Id == getFrom.Id
               || getFrom.FiltraNome && x.Name == getFrom.FiltroNome
                    && getFrom.FiltraUpdatedOn && x.UpdatedOn >= getFrom.DataInicial && x.UpdatedOn <= getFrom.DataFinal
                    && getFrom.FiltraStatus && x.Status == getFrom.Status
                  

            )
            .Skip(getFrom.PageSize * getFrom.PageNumber)
            .Limit(getFrom.PageSize);

            _memberOutResponse.SetData(result);

            _memberOutResponse.SetResultado(true);
            return _memberOutResponse;
        }*/
    }
}
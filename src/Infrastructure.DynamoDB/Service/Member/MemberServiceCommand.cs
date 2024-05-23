using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Application.DTO.Response;
using Application.DTO.Response.Member;
using Domain.Enums;
using Infrastructure.Base.Abstractions.Member;
using System.Text.RegularExpressions;

namespace Infrastructure.DynamoDB.Service.Member
{
    public class MemberServiceCommand : IMemberServiceCommand
    {
        private MemberOutResponse _memberOutResponse;
        private string _userId;

        private readonly IAmazonDynamoDB _dynamoDbClient;
        private readonly string _tableName = "Members";

        public MemberServiceCommand(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
            _memberOutResponse = new MemberOutResponse();
            _userId = Guid.NewGuid().ToString();
        }

        private async Task<PutItemRequest> GeneratePutItemRequest(Domain.Entities.Member member)
        {
            var request = new PutItemRequest
            {
                TableName = _tableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    { "Id"         , new AttributeValue { S = member.Id } },
                    { "Name"       , new AttributeValue { S = member.Name } },
                    { "FirstName"  , new AttributeValue { S = member.FirstName } },
                    { "LastName"   , new AttributeValue { S = member.LastName } },
                    { "Gender"     , new AttributeValue { S = member.Gender.ToString() } },
                    { "Email"      , new AttributeValue { S = member.Email } },
                    { "Status"     , new AttributeValue { S = member.Status.ToString() } },
                    { "InsertedOn" , new AttributeValue { S = member.InsertedOn.ToString("yyyy-MM-dd HH:mm:ss.fff")} },
                    { "InsertedBy" , new AttributeValue { S = member.InsertedBy } },
                    { "UpdatedOn"  , new AttributeValue { S = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") } },
                    { "UpdatedBy"  , new AttributeValue { S = member.UpdatedBy } }
                }
            };

            return request;
        }
        public async Task<MemberOutResponse> CreateMember(Domain.Entities.Member member)
        {
            if (member == null)
            {
                _memberOutResponse.SetResultado(false); return _memberOutResponse;
            }

            var updateResponse = await _dynamoDbClient.PutItemAsync(await GeneratePutItemRequest(member));

            if (updateResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                _memberOutResponse.SetResultado(true);
                _memberOutResponse.SetData(MemberMapper.FromEntityToDTO(member));
                return _memberOutResponse;
            }
            else
            {
                _memberOutResponse.SetResultado(false); return _memberOutResponse;
            }
        }
        public async Task<MemberOutResponse> DeleteMember(string id)
        {
            if (id == null)
            {
                _memberOutResponse.SetResultado(false); return _memberOutResponse;
            }

            var memberExistente = await GetMember(id);

            if (memberExistente == null)
            {
                _memberOutResponse.SetResultado(false); return _memberOutResponse;
            }

            memberExistente.Status = EStatus.EXCLUIDO;
            memberExistente.UpdatedBy = _userId;
            memberExistente.UpdatedOn = DateTime.Now;

            var updateResponse = await _dynamoDbClient.PutItemAsync(await GeneratePutItemRequest(memberExistente));

            if (updateResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                _memberOutResponse.SetResultado(true);
                _memberOutResponse.SetData(MemberMapper.FromEntityToDTO(memberExistente));
                return _memberOutResponse;
            }
            else
            {
                _memberOutResponse.SetResultado(false); return _memberOutResponse;
            }
        }
        private async Task<Domain.Entities.Member> GetMember(string id)
        {
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
                _memberOutResponse.SetMensagem("Registro não encontrado.");
                _memberOutResponse.SetResultado(false);
                return null;
            }

            var member = new Domain.Entities.Member
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

            return member;
            
        }
        public async Task<MemberOutResponse> UpdateMember(Domain.Entities.Member member)
        {
            if (member == null)
            {
                _memberOutResponse.SetResultado(false); return _memberOutResponse;
            }

            var memberExistente = await  GetMember(member.Id);

            if (memberExistente == null)
            {
                _memberOutResponse.SetResultado(false); return _memberOutResponse;
            }

            var updateResponse = await _dynamoDbClient.PutItemAsync(await GeneratePutItemRequest(memberExistente));

            if (updateResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                _memberOutResponse.SetResultado(true);
                _memberOutResponse.SetData(MemberMapper.FromEntityToDTO(memberExistente));
                return _memberOutResponse;
            }
            else
            {
                _memberOutResponse.SetResultado(false); return _memberOutResponse;
            }
        }

        public async Task<MemberOutResponse> GetMemberById(string id)
        {
            await GetMember(id);
            return _memberOutResponse;
        }
    }
}

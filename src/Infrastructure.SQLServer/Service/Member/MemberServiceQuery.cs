using Application.DTO.Request;
using Application.DTO.Response;
using Infrastructure.Base.Abstractions.Member;

namespace Infrastructure.SQLServer.Service.Member
{
    public class MemberServiceQuery : IMemberServiceQuery
    {
        public Task<MemberOutResponse> GetMemberById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<MemberOutResponse> GetMembers(GetFrom getFrom)
        {
            throw new NotImplementedException();
        }
    }
}

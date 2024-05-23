
using Domain.Request;
using Domain.Response;

namespace Infrastructure.Base.Abstractions.Member
{
    public interface IMemberServiceQuery
    {
        Task<MemberOutResponse> GetMemberById(string id);
        Task<MemberOutResponse> GetMembers(GetFrom getFrom);
    }
}

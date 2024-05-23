using Domain.Response;

namespace Infrastructure.Base.Abstractions.Member
{
    public interface IMemberServiceCommand
    {
        Task<MemberOutResponse> CreateMember(Domain.Entities.Member member);
        Task<MemberOutResponse> GetMemberById(string id);
        Task<MemberOutResponse> UpdateMember(Domain.Entities.Member member);
        Task<MemberOutResponse> DeleteMember(string id);
    }
}

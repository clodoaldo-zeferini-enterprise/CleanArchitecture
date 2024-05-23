using Razor.Models;

namespace Razor.Services
{
    public interface IMemberService
    {
        Task<IList<MemberViewModel>> GetMembers();
        Task<MemberViewModel> GetMember(string id);
        Task<MemberViewModel> CreateMember(MemberViewModel member);
        Task<bool> UpdateMember (string id, MemberViewModel member);
        Task<bool> DeleteMember(string id);
    }
}

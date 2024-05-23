using Domain.Entities;
using Razor.Models;
using Razor.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FrontEnd
{
    public class IndexModel : PageModel
    {
        public IList<MemberViewModel> Member { get; set; } = default!;

        private readonly IMemberService _memberService;

        public IndexModel(IMemberService memberService)
        {
            _memberService = memberService;
        }


        public async Task OnGetAsync()
        {
            var enumMembers = await _memberService.GetMembers();
            Member = (IList<MemberViewModel>)enumMembers;
        }
    }
}

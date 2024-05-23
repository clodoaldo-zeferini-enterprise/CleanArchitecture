using Razor.Models;
using Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd
{
    public class DeleteModel : PageModel
    {
        private readonly IMemberService _memberService;

        public DeleteModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [BindProperty]
        public MemberViewModel Member { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _memberService.GetMember(id);

            if (member == null)
            {
                return NotFound();
            }
            else
            {
                Member = member;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _memberService.GetMember(id);
            if (member != null)
            {
                Member = member;
                await _memberService.DeleteMember(id);
            }

            return RedirectToPage("./Index");
        }
    }
}

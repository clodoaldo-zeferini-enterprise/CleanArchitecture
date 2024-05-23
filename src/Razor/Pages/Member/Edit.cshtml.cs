using Razor.Models;
using Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd
{
    public class EditModel : PageModel
    {
        private readonly IMemberService _memberService;

        public EditModel(IMemberService memberService)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _memberService.UpdateMember(Member.Id, Member);

            return RedirectToPage("./Index");
        }
    }
}

using Domain.Entities;
using Razor.Models;
using Razor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd
{
    public class CreateModel : PageModel
    {
        private readonly IMemberService _memberService;

        public CreateModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MemberViewModel Member { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _memberService.CreateMember(Member);
            return RedirectToPage("./Index");
        }
    }
}

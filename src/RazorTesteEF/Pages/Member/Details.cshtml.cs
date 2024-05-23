using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using RazorTesteEF.Data;

namespace RazorTesteEF.Pages.Member
{
    public class DetailsModel : PageModel
    {
        private readonly RazorTesteEF.Data.RazorTesteEFContext _context;

        public DetailsModel(RazorTesteEF.Data.RazorTesteEFContext context)
        {
            _context = context;
        }

        public Domain.Entities.Member Member { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member.FirstOrDefaultAsync(m => m.Id == id);
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.Achievements
{
    public class DeleteModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;

        public DeleteModel(StyleShareWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Achievement Achievement { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Achievements == null)
            {
                return NotFound();
            }

            var achievement = await _context.Achievements.FirstOrDefaultAsync(m => m.Id == id);

            if (achievement == null)
            {
                return NotFound();
            }
            else 
            {
                Achievement = achievement;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Achievements == null)
            {
                return NotFound();
            }
            var achievement = await _context.Achievements.FindAsync(id);

            if (achievement != null)
            {
                Achievement = achievement;
                _context.Achievements.Remove(Achievement);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

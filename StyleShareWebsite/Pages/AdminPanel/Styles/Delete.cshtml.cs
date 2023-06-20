using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.Styles
{
    public class DeleteModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;

        public DeleteModel(StyleShareWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Style Style { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Styles == null)
            {
                return NotFound();
            }

            var style = await _context.Styles.FirstOrDefaultAsync(m => m.Id == id);

            if (style == null)
            {
                return NotFound();
            }
            else 
            {
                Style = style;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Styles == null)
            {
                return NotFound();
            }
            var style = await _context.Styles.FindAsync(id);

            if (style != null)
            {
                Style = style;
                _context.Styles.Remove(Style);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

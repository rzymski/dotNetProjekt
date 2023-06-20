using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel.StyleStylePacks
{
    public class EditModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;

        public EditModel(StyleShareWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StyleStylePack StyleStylePack { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.StyleStylePacks == null)
            {
                return NotFound();
            }

            var styleStylePack =  await _context.StyleStylePacks.FirstOrDefaultAsync(m => m.Id == id);
            if (styleStylePack == null)
            {
                return NotFound();
            }
            StyleStylePack = styleStylePack;
            ViewData["StylePackId"] = new SelectList(_context.StylePacks, "Id", "Name");
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Title");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            _context.Attach(StyleStylePack).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StyleStylePackExists(StyleStylePack.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StyleStylePackExists(int id)
        {
          return (_context.StyleStylePacks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

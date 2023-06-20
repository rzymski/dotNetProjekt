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

namespace StyleShareWebsite.Pages.Achievements
{
    public class EditModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;

        public EditModel(StyleShareWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Achievement Achievement { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Achievements == null)
            {
                return NotFound();
            }

            var achievement =  await _context.Achievements.FirstOrDefaultAsync(m => m.Id == id);
            if (achievement == null)
            {
                return NotFound();
            }
            Achievement = achievement;
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

            _context.Attach(Achievement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AchievementExists(Achievement.Id))
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

        private bool AchievementExists(int id)
        {
          return _context.Achievements.Any(e => e.Id == id);
        }
    }
}

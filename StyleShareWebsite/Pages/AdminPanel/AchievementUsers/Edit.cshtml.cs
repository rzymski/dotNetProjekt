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

namespace StyleShareWebsite.Pages.AdminPanel.au
{
    public class EditModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;

        public EditModel(StyleShareWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AchievementUser AchievementUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AchievementsUsers == null)
            {
                return NotFound();
            }

            var achievementuser =  await _context.AchievementsUsers.FirstOrDefaultAsync(m => m.Id == id);
            if (achievementuser == null)
            {
                return NotFound();
            }
            AchievementUser = achievementuser;
           ViewData["AchievementId"] = new SelectList(_context.Achievements, "Id", "Title");
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //WALIDACJA SIE WYPIERDLA NIE WIEM CZEMU ModelState.IsValid ZAWSZE = false
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.Attach(AchievementUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AchievementUserExists(AchievementUser.Id))
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

        private bool AchievementUserExists(int id)
        {
          return (_context.AchievementsUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StyleShareWebsite.Data;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel.au
{
    public class CreateModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;

        public CreateModel(StyleShareWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["AchievementId"] = new SelectList(_context.Achievements, "Id", "Title");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Nickname");
            return Page();
        }

        [BindProperty]
        public AchievementUser AchievementUser { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //WALIDACJA SIE WYPIERDLA NIE WIEM CZEMU ModelState.IsValid ZAWSZE = false
            //if (!ModelState.IsValid || _context.AchievementsUsers == null || AchievementUser == null)
            //{
            //    return Page();
            //}

            _context.AchievementsUsers.Add(AchievementUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

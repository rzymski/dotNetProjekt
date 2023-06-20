using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel.au
{
    public class DeleteModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;
        private readonly IAchievementService _achievementService;
        private readonly IUserService _userService;

        public DeleteModel(StyleShareWebsite.Data.ApplicationDbContext context, IAchievementService achievementService, IUserService userService)
        {
            _context = context;
            _achievementService = achievementService;
            _userService = userService;
        }

        [BindProperty]
        public AchievementUser AchievementUser { get; set; } = default!;
        public Achievement Achievement { get; set; } = default!;
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AchievementsUsers == null)
            {
                return NotFound();
            }

            var achievementuser = await _context.AchievementsUsers.FirstOrDefaultAsync(m => m.Id == id);
            var achievement = await _achievementService.GetByIdAsync(achievementuser.AchievementId);
            var user = await _userService.GetByIdAsync(achievementuser.UserId);

            if (achievementuser == null)
            {
                return NotFound();
            }
            else 
            {
                AchievementUser = achievementuser;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.AchievementsUsers == null)
            {
                return NotFound();
            }
            var achievementuser = await _context.AchievementsUsers.FindAsync(id);

            if (achievementuser != null)
            {
                AchievementUser = achievementuser;
                _context.AchievementsUsers.Remove(AchievementUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

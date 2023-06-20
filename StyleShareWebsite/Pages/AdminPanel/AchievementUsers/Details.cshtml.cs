using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel.au
{
    public class DetailsModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;
        private readonly IAchievementService _achievementService;
        private readonly IUserService _userService;

        public DetailsModel(StyleShareWebsite.Data.ApplicationDbContext context, IAchievementService achievementService, IUserService userService)
        {
            _context = context;
            _achievementService = achievementService;
            _userService = userService;
        }
        
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
                Achievement = achievement;
                User = user;
            }
            return Page();
        }
    }
}

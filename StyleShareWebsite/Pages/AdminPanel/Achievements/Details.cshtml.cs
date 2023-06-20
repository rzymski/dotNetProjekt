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

namespace StyleShareWebsite.Pages.Achievements
{
    public class DetailsModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;

        private readonly IAchievementUserService _achievementUserService;
        public List<AchievementUser> achievementUsers { get; set; } = new List<AchievementUser>();
        public List<int> AchievementOwnersId { get; set; } = new List<int>();

        public DetailsModel(StyleShareWebsite.Data.ApplicationDbContext context, IAchievementUserService achievementUserService)
        {
            _context = context;
            _achievementUserService = achievementUserService;
        }

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

            achievementUsers = _achievementUserService.GetAllOwnersByAchieveId(id.Value).Result.ToList();
            foreach (var achievementUser in achievementUsers)
            {
                AchievementOwnersId.Add(achievementUser.UserId);
            }

            return Page();
        }
    }
}

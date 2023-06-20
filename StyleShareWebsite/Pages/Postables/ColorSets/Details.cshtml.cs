using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.Postables.ColorSets
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IPostableService _postableService;
        private readonly IUserService _userService;
        public User CurrentUser { get; set; }

        public DetailsModel(ApplicationDbContext context, IPostableService postableService, IUserService userService)
        {
            _context = context;
            _postableService = postableService;
            _userService = userService;
        }

        public ColorSet ColorSet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = _userService.GetAuthorizedUserId(HttpContext);
                CurrentUser = _userService.GetByIdAsync(userId).Result;
            }

            if (id == null || _context.ColorSets == null)
            {
                return NotFound();
            }

            var colorset = await _context.ColorSets.FirstOrDefaultAsync(m => m.Id == id);
            if (colorset == null)
            {
                return NotFound();
            }
            else 
            {
                ColorSet = colorset;
                ColorSet.Tags = _postableService.GetAllTagsByPostableIdAsync(ColorSet.Id).Result.ToList();
            }
            return Page();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.Models;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services;

namespace StyleShareWebsite.Pages.Postables.Styles
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

        public Style Style { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = _userService.GetAuthorizedUserId(HttpContext);
                CurrentUser = _userService.GetByIdAsync(userId).Result;
            }

            if (id == null || _context.Styles == null)
            {
                return NotFound();
            }

            var style = await _context.Styles.FirstOrDefaultAsync(m => m.Id == id);
           // var style2 =  _context.Tags.Where(t => t.Id == id).SelectMany(s => s.Postables);

            if (style == null)
            {
                return NotFound();
            }
            else 
            {
                Style = style;
                Style.Tags = _postableService.GetAllTagsByPostableIdAsync(Style.Id).Result.ToList();
            }
            return Page();
        }
    }
}

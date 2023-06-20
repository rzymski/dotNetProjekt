using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.Tags
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        public User CurrentUser { get; set; }
        public IndexModel(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService= userService;
        }

        public IList<Tag> Tag { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Tags != null)
            {
                Tag = await _context.Tags.ToListAsync();
            }

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = _userService.GetAuthorizedUserId(HttpContext);
                CurrentUser = _userService.GetByIdAsync(userId).Result;
            }
        }
    }
}

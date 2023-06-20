using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages
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

        public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = _userService.GetAuthorizedUserId(HttpContext);
                CurrentUser = _userService.GetByIdAsync(userId).Result;
            }

            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            else 
            {
                Post = post;
                Post.Tags = _postableService.GetAllTagsByPostableIdAsync(Post.Id).Result.ToList();
            }
            return Page();
        }
    }
}

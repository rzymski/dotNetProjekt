using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel
{
    public class CreateModel : PageModel
    {
        private readonly ICommentService _commentService;
        private readonly IPostableService _postService;
        private readonly IUserService _userService;

        [BindProperty]
        public Comment Comment { get; set; } = default;

        public int PostableId { get; set; }

        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;
        public CreateModel(StyleShareWebsite.Data.ApplicationDbContext context, ICommentService commentService, IPostableService postableService, IUserService userService)
        {
            _context = context;
            _commentService = commentService;
            _postService = postableService;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            ViewData["PostableId"] = new SelectList(_context.Postables, "Id", "Title");
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Page();
            }
            else return RedirectToPage("../Login");
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userService.GetAuthorizedUserId(HttpContext);
            Comment.User = _userService.GetByIdAsync(userId).Result;
            Comment.Date = DateTime.Now;

            _context.Comments.Add(Comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

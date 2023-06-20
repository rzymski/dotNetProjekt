using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages
{
    public class AddCommentModel : PageModel
    {
        private readonly ICommentService _commentService;
        private readonly IPostableService _postService;
        private readonly IUserService _userService;

        [BindProperty]
        public Comment comment { get; set; } = default;

        public int PostableId { get; set; }

        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;
        public AddCommentModel(StyleShareWebsite.Data.ApplicationDbContext context, ICommentService commentService, IPostableService postableService, IUserService userService)
        {
            _context = context;
            _commentService = commentService;
            _postService = postableService;
            _userService = userService;
        }

        public IActionResult OnGet(int id)
        {
            PostableId = id;
            //ViewData["PostableId"] = new SelectList(_context.Postables, "Id", "Title");


            return Page();
        }

        /*public IActionResult OnPost()
        {

            var post = _postService.GetByIdAsync(comment.PostableId).Result;
           // comment.PostableId = PostableId;
            comment.Date = DateTime.Now;
            comment.CommentedContent = post;

            _commentService.AddAsync(comment);

            return RedirectToPage("./Index");
        }*/

        public async Task<IActionResult> OnPostAsync()
        {
            var post = _postService.GetByIdAsync(comment.PostableId).Result;
            // comment.PostableId = PostableId;
            var userId = _userService.GetAuthorizedUserId(HttpContext);
            comment.User = _userService.GetByIdAsync(userId).Result;
            comment.Date = DateTime.Now;
            comment.CommentedContent = post;

            //_commentService.AddAsync(comment);

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages
{
    public class CommentsModel : PageModel
    {
        private readonly ICommentService _commentService;

        public List<Comment> comments { get; set; }

        public CommentsModel(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult OnGet(int? id)
        {
            if(id != null)
            {
                comments = (List<Comment>)_commentService.GetAllCommentsByCommentedPostableId((int)id).Result;
                return Page();
            }

            return NotFound();
            
        }
    }
}

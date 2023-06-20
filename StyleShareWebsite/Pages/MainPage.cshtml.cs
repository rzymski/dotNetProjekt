using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages
{
    public class MainPageModel : PageModel
    {
        public readonly IPostableService _postableService;
        private readonly IPostService _postService;
        public readonly ILikeService _likeService;
        private readonly IUserService _userService;
        public readonly ICommentService _commentService;

        public string? UserName { get; set; } = null;
        public User CurrentUser { get; set; }

        public bool CanBeLicked { get; set; }

        public List<Post> Posts { get; set; }

        public MainPageModel(IPostService postService, IPostableService postableService, ILikeService likeService, IUserService userService, ICommentService commentService)
        {
            _postableService = postableService;
            _postService = postService;
            _likeService = likeService;
            _userService = userService;
            _commentService = commentService;
        }

        public void OnGet()
        {
            Posts = _postService.GetAllAsync().Result.ToList();

            if (HttpContext.User.Identity.Name != null)
            {
                UserName = HttpContext.User.Identity.Name;
            }
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = _userService.GetAuthorizedUserId(HttpContext);
                CurrentUser = _userService.GetByIdAsync(userId).Result;
            } 
        }

        
    }
}

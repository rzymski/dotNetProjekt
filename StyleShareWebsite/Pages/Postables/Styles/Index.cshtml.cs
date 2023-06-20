using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages
{
    public class StylesModel : PageModel
    {
        public readonly IPostableService _postableService;
        public readonly IStyleService _styleService;
        public readonly ILikeService _likeService;
        private readonly IUserService _userService;
        public readonly ICommentService _commentService;

        public string? UserName { get; set; } = null;
        public User CurrentUser { get; set; }

        public bool CanBeLicked { get; set; }

        public List<Style> Styles { get; set; }


        public StylesModel(IStyleService styleService, IPostableService postableService, ILikeService likeService, IUserService userService, ICommentService commentService)
        {
            _postableService = postableService;
            _likeService = likeService;
            _userService = userService;
            _styleService = styleService;
            _commentService = commentService;
        }

        public void OnGet()
        {
            Styles = _styleService.GetAllAsync().Result.ToList();

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

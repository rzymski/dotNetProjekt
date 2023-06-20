using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.Postables.ColorSets
{
    public class ColorSetModel : PageModel
    {
        public readonly IPostableService _postableService;
        public readonly IColorSetService _colorsetService;
        public readonly ILikeService _likeService;
        private readonly IUserService _userService;
        public readonly ICommentService _commentService;

        public string? UserName { get; set; } = null;
        public User CurrentUser { get; set; }

        public bool CanBeLicked { get; set; }

        public List<ColorSet> ColorSets { get; set; }


        public ColorSetModel(IColorSetService colorsetService, IPostableService postableService, ILikeService likeService, IUserService userService, ICommentService commentService)
        {
            _postableService = postableService;
            _likeService = likeService;
            _userService = userService;
            _colorsetService = colorsetService;
            _commentService = commentService;
        }

        public void OnGet()
        {
            ColorSets = _colorsetService.GetAllAsync().Result.ToList();

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

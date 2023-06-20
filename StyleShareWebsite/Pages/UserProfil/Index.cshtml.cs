using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NHibernate.Mapping;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.UserProfil
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IAchievementService _achievementService;
        private readonly IAchievementUserService _achievementUserService;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IStyleService _styleService;
        private readonly IColorSetService _colorSetService;

        public User CurrentUser { get; set; }

        public List<AchievementUser> UserAchievements { get; set; } = new List<AchievementUser>();
        public List<int> UserAchievementsId { get; set; } = new List<int>();

        public List<Achievement> CurrentUserAchievements { get; set; } = new List<Achievement>();

        public List<Post> CurrentUserPosts { get; set; } = new List<Post>();
        public List<Style> CurrentUserStyles { get; set; } = new List<Style>();
        public List<ColorSet> CurrentUserColorSets { get; set; } = new List<ColorSet>();

        public List<Comment> CurrentUserComments { get; set; } = new List<Comment>();

        public List<Post> AllPosts { get; set; } = new List<Post>();
        public List<Style> AllStyles { get; set; } = new List<Style>();
        public List<ColorSet> AllColorSets { get; set; } = new List<ColorSet>();

        public IndexModel(IUserService userService, IAchievementService achievementService, IAchievementUserService achievementUserService, IPostService postService, ICommentService commentService, IColorSetService colorSetService, IStyleService styleService)
        {
            _userService = userService;
            _achievementService = achievementService;
            _achievementUserService = achievementUserService;
            _postService = postService;
            _commentService = commentService;
            _colorSetService = colorSetService;
            _styleService = styleService;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                AllPosts = _postService.GetAllAsync().Result.ToList();
                AllStyles = _styleService.GetAllAsync().Result.ToList();
                AllColorSets = _colorSetService.GetAllAsync().Result.ToList();


                var userId = _userService.GetAuthorizedUserId(HttpContext);
                CurrentUser = _userService.GetByIdAsync(userId).Result;
                UserAchievements = _achievementUserService.GetAllAchievementsByOwnerId(userId).Result.ToList();

                foreach (var userAchievement in UserAchievements)
                {
                    CurrentUserAchievements.Add(_achievementService.GetByIdAsync(userAchievement.AchievementId).Result);
                }

                CurrentUserPosts = _postService.GetAllPostsByOwnerId(CurrentUser).Result.ToList();
                CurrentUserStyles = _styleService.GetAllStylesByOwnerId(CurrentUser).Result.ToList();
                CurrentUserColorSets = _colorSetService.GetAllColorSetsByOwnerId(CurrentUser).Result.ToList();
                CurrentUserComments = _commentService.GetAllCommentsByOwnerId(CurrentUser).Result.ToList();

                return Page();
            }
            else return RedirectToPage("../Login");
        }
    }
}

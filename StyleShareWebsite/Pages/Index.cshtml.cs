using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IStyleService _styleService;
        private readonly IAchievementService _achievementService;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public string? UserName { get; set; }
        public User CurrentUser { get; set; }

        public IList<Achievement> Achievements { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IStyleService styleService, IAchievementService achievementService, IPostService postService, IUserService userService)
        {
            _logger = logger;
            _styleService = styleService;
            _achievementService = achievementService;
            _postService = postService;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            /*
            Post post = new Post()
            {
                Content = "Content 1",
                Title = "Title 1",
                Date = DateTime.Now
            };
            _postService.AddAsync(post);
            */

            if(HttpContext.User.Identity.Name != null)
            {
                UserName = HttpContext.User.Identity.Name;
            }
            if(HttpContext.User.Identity.IsAuthenticated) 
            {
                var userId = _userService.GetAuthorizedUserId(HttpContext);
                CurrentUser = _userService.GetByIdAsync(userId).Result;
            }

            //setAchievements();

            Achievements = _achievementService.GetAllAsync().Result;
            return RedirectToPage("MainPage");
        }
        /*
        public void setAchievements()
        {
            Achievement a1 = new Achievement()
            {
                Title = "Test 1",
                Description = "Desc 1"
            };

            Achievement a2 = new Achievement()
            {
                Title = "Test 2",
                Description = "Desc 2"
            };

            Achievement a3 = new Achievement()
            {
                Title = "Test 3",
                Description = "Desc 3"
            };

            Achievement a4 = new Achievement()
            {
                Title = "Test 4",
                Description = "Desc 4"
            };

            Achievement a5 = new Achievement()
            {
                Title = "Test 5",
                Description = "Desc 5"
            };

            _achievementService.AddAsync(a1);
            _achievementService.AddAsync(a2);
            _achievementService.AddAsync(a3);
            _achievementService.AddAsync(a4);
            _achievementService.AddAsync(a5);
        }
        */
    }
}
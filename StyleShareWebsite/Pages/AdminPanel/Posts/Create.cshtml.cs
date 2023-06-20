using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.Posts
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly ITagService _tagService;

        public List<Tag> Tags { get; set; }
        public User CurrentUser { get; set; }

        [BindProperty]
        public Post Post { get; set; }
        [BindProperty]
        public List<int> SelectedTagsIds { get; set; }

        public List<Tag> SelectedTags { get; set; } = new List<Tag>();

        public CreateModel(ApplicationDbContext context, IUserService userService, ITagService tagService)
        {
            _context = context;
            _userService = userService;
            _tagService = tagService;
        }
        
        public IActionResult OnGet()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {

                Tags = _tagService.GetAllAsync().Result.ToList();
                ViewData["selectTags"] = new MultiSelectList(Tags, "Id", "Name");

                return Page();
            }
          else return RedirectToPage("../Login");
            
        }
    


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userService.GetAuthorizedUserId(HttpContext);
            Post.User = _userService.GetByIdAsync(userId).Result;
            Post.Date = DateTime.Now;

            foreach (var tagId in SelectedTagsIds)
            {
                var allTags = _tagService.GetAllAsync().Result;
                Tag currentTag = allTags.Where(t => t.Id == tagId).FirstOrDefault();
                SelectedTags.Add(currentTag);
            }

            Post.Tags = SelectedTags;
            /*
            ModelState.ClearValidationState(nameof(Post));
            if (!TryValidateModel(Post, nameof(Post)))
            {
                return Page();
            }
            */
            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

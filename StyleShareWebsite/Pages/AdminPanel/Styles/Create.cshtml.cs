using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel.Styles
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly ITagService _tagService;

        public List<Tag> Tags { get; set; }
        public User CurrentUser { get; set; }


        [BindProperty]
        public Style Style { get; set; }
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
            Style.User = _userService.GetByIdAsync(userId).Result;
            Style.Date = DateTime.Now;

            foreach(var tagId in SelectedTagsIds)
            {
                var allTags = _tagService.GetAllAsync().Result;
                Tag currentTag = allTags.Where(t => t.Id == tagId).FirstOrDefault();
                SelectedTags.Add(currentTag);
            }

            Style.Tags = SelectedTags;
            /*
            ModelState.ClearValidationState(nameof(Style));
            if (!TryValidateModel(Style, nameof(Style)))
            {
                return Page();
            }
            */
            _context.Styles.Add(Style);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

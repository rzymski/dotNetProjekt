using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.Postables.Posts
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ITagService _tagService;
        private readonly IPostableService _postableService;

        public List<Tag> Tags { get; set; }

        public EditModel(ApplicationDbContext context, ITagService tagService, IPostableService postableService)
        {
            _context = context;
            _tagService = tagService;
            _postableService = postableService;
        }

        [BindProperty]
        public Post Post { get; set; } = default!;
        [BindProperty]
        public List<int> SelectedTagsIds { get; set; } = new List<int>();

        public List<Tag> SelectedTags { get; set; } = new List<Tag>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post =  await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            Post = post;

            Tags = _tagService.GetAllAsync().Result.ToList();
            ViewData["selectTags"] = new MultiSelectList(Tags, "Id", "Name");

            foreach (Tag tag in _postableService.GetAllTagsByPostableIdAsync(Post.Id).Result)
                SelectedTagsIds.Add(tag.Id);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var tagId in SelectedTagsIds)
            {
                var allTags = _tagService.GetAllAsync().Result;
                Tag currentTag = allTags.Where(t => t.Id == tagId).FirstOrDefault();
                SelectedTags.Add(currentTag);
            }

            Post.Tags = SelectedTags;
            Post.User = _postableService.GetOwnerByPostableIdAsync(Post.Id).Result;

            ModelState.ClearValidationState(nameof(Post));
            if (!TryValidateModel(Post, nameof(Post)))
            {
                return Page();
            }

            await _postableService.UpdateAsync(Post);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(Post.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PostExists(int id)
        {
          return _context.Posts.Any(e => e.Id == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel.Styles
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
        public Style Style { get; set; } = default!;
        [BindProperty]
        public List<int> SelectedTagsIds { get; set; } = new List<int>();

        public List<Tag> SelectedTags { get; set; } = new List<Tag>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Styles == null)
            {
                return NotFound();
            }

            var style =  await _context.Styles.FirstOrDefaultAsync(m => m.Id == id);
            if (style == null)
            {
                return NotFound();
            }

            Style = style;

            Tags = _tagService.GetAllAsync().Result.ToList();
            ViewData["selectTags"] = new MultiSelectList(Tags, "Id", "Name");

            foreach(Tag tag in _postableService.GetAllTagsByPostableIdAsync(Style.Id).Result)
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

            Style.Tags = SelectedTags;
            Style.User = _postableService.GetOwnerByPostableIdAsync(Style.Id).Result;

            /*
            ModelState.ClearValidationState(nameof(Style));
            if (!TryValidateModel(Style, nameof(Style)))
            {
                return Page();
            }
            */

            _context.Attach(Style).State = EntityState.Modified;
            await _postableService.UpdateAsync(Style);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StyleExists(Style.Id))
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

        private bool StyleExists(int id)
        {
          return _context.Styles.Any(e => e.Id == id);
        }
    }
}

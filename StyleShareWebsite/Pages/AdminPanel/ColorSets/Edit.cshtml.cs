using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel.ColorSets
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
        public ColorSet ColorSet { get; set; } = default!;
        [BindProperty]
        public List<int> SelectedTagsIds { get; set; } = new List<int>();

        public List<Tag> SelectedTags { get; set; } = new List<Tag>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ColorSets == null)
            {
                return NotFound();
            }

            var colorset =  await _context.ColorSets.FirstOrDefaultAsync(m => m.Id == id);
            if (colorset == null)
            {
                return NotFound();
            }
            ColorSet = colorset;

            Tags = _tagService.GetAllAsync().Result.ToList();
            ViewData["selectTags"] = new MultiSelectList(Tags, "Id", "Name");

            foreach (Tag tag in _postableService.GetAllTagsByPostableIdAsync(ColorSet.Id).Result)
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

            ColorSet.Tags = SelectedTags;
            ColorSet.User = _postableService.GetOwnerByPostableIdAsync(ColorSet.Id).Result;

            /*
            ModelState.ClearValidationState(nameof(ColorSet));
            if (!TryValidateModel(ColorSet, nameof(ColorSet)))
            {
                return Page();
            }
            */

            _context.Attach(ColorSet).State = EntityState.Modified;
            await _postableService.UpdateAsync(ColorSet);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorSetExists(ColorSet.Id))
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

        private bool ColorSetExists(int id)
        {
          return _context.ColorSets.Any(e => e.Id == id);
        }
    }
}

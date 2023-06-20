using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel.Comments
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IPostableService _postableService;

        public int PostableId { get; set; }
        public EditModel(ApplicationDbContext context, IPostableService postableService)
        {
            _context = context;
            _postableService = postableService;
        }

        [BindProperty]
        public Comment Comment { get; set; } = default!;
        [BindProperty]
        public int SelectedPostableId { get; set; } = default!;
        public Postable SelectedPostable { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Comments == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }
            Comment = comment;

            ViewData["PostableId"] = new SelectList(_context.Postables, "Id", "Title");

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Page();
            }
            else return RedirectToPage("../Login");
        }

        
        public async Task<IActionResult> OnPostAsync()
        {
            Comment.User = _postableService.GetOwnerByPostableIdAsync(Comment.PostableId).Result;
            Comment.Date= DateTime.Now;

            _context.Attach(Comment).State= EntityState.Modified;
            await _postableService.UpdateAsync(Comment);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(Comment.Id))
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

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}

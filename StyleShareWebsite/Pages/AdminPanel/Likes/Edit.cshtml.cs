using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel.Likes
{
    public class EditModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;

        public EditModel(StyleShareWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Like Like { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Likes == null)
            {
                return NotFound();
            }

            var like =  await _context.Likes.FirstOrDefaultAsync(m => m.Id == id);
            if (like == null)
            {
                return NotFound();
            }
            Like = like;
           ViewData["PostableId"] = new SelectList(_context.Postables, "Id", "Title");
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _context.Attach(Like).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikeExists(Like.Id))
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

        private bool LikeExists(int id)
        {
          //return (_context.Likes.Any(e => e.Id == id).GetValueOrDefault());
          return (_context.Likes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

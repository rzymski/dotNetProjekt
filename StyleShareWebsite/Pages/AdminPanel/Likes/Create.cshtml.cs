using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel.Likes
{
    public class CreateModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;
        public CreateModel(StyleShareWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["PostableId"] = new SelectList(_context.Postables, "Id", "Title");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Nickname");
            return Page();
        }

        [BindProperty]
        public Like Like { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //Like.Id = (Like.UserId + Like.PostableId) * (Like.UserId + Like.PostableId + 1) / 2 + Like.UserId;
            _context.Likes.Add(Like);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

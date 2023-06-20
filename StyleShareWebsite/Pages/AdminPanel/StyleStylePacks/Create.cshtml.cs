using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StyleShareWebsite.Data;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel.StyleStylePacks
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
            ViewData["StylePackId"] = new SelectList(_context.StylePacks, "Id", "Name");
            //ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Title"+" Html"+"Css");
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public StyleStylePack StyleStylePack { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            _context.StyleStylePacks.Add(StyleStylePack);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

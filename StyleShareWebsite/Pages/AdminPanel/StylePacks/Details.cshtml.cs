using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel.StylePacks
{
    public class DetailsModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;

        public DetailsModel(StyleShareWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public StylePack StylePack { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.StylePacks == null)
            {
                return NotFound();
            }

            var stylepack = await _context.StylePacks.FirstOrDefaultAsync(m => m.Id == id);
            if (stylepack == null)
            {
                return NotFound();
            }
            else 
            {
                StylePack = stylepack;
            }
            return Page();
        }
    }
}

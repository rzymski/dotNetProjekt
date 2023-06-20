using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel.StyleStylePacks
{
    public class DetailsModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;
        private readonly IStylePackService _stylePackService;
        private readonly IStyleService _styleService;

        public DetailsModel(StyleShareWebsite.Data.ApplicationDbContext context, IStylePackService stylePackService, IStyleService styleService)
        {
            _context = context;
            _stylePackService = stylePackService;
            _styleService = styleService;
        }

        public StyleStylePack StyleStylePack { get; set; } = default!;
        public StylePack StylePack { get; set; } = default!;
        public Style Style { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.StyleStylePacks == null)
            {
                return NotFound();
            }

            var styleStylePack = await _context.StyleStylePacks.FirstOrDefaultAsync(m => m.Id == id);
            var stylePack = await _stylePackService.GetByIdAsync(styleStylePack.StylePackId);
            var style = await _styleService.GetByIdAsync(styleStylePack.StyleId);
            if (styleStylePack == null)
            {
                return NotFound();
            }
            else
            {
                StyleStylePack = styleStylePack;
                StylePack = stylePack;
                Style = style;
            }
            return Page();
        }
    }
}

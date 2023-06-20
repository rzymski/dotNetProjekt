using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel.ColorSets
{
    public class DetailsModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;

        public DetailsModel(StyleShareWebsite.Data.ApplicationDbContext context, IPostableService postableService)
        {
            _context = context;
            _postableService = postableService;
        }

        public ColorSet ColorSet { get; set; }
        private readonly IPostableService _postableService;
        public List<Tag> Tags { get; set; } = new List<Tag>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ColorSets == null)
            {
                return NotFound();
            }

            var colorset = await _context.ColorSets.FirstOrDefaultAsync(m => m.Id == id);
            if (colorset == null)
            {
                return NotFound();
            }
            else 
            {
                ColorSet = colorset;
                Tags = _postableService.GetAllTagsByPostableIdAsync(ColorSet.Id).Result.ToList();
            }
            return Page();
        }
    }
}

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
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.Styles
{
    public class DetailsModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;
        private readonly IPostableService _postableService;

        public DetailsModel(StyleShareWebsite.Data.ApplicationDbContext context, IPostableService postableServicee)
        {
            _context = context;
            _postableService = postableServicee;
        }

        public Style Style { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Styles == null)
            {
                return NotFound();
            }

            var style = await _context.Styles.FirstOrDefaultAsync(m => m.Id == id);
            if (style == null)
            {
                return NotFound();
            }
            else 
            {
                Style = style;
                Tags = _postableService.GetAllTagsByPostableIdAsync(style.Id).Result.ToList();
            }
            return Page();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.Achievements
{
    public class IndexModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;

        public IndexModel(StyleShareWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Achievement> Achievement { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Achievements != null)
            {
                Achievement = await _context.Achievements.ToListAsync();
            }
        }
    }
}

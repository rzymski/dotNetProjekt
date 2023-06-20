using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel.Likes
{
    public class IndexModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;

        public IndexModel(StyleShareWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Like> Like { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Likes != null)
            {
                Like = await _context.Likes
                .Include(a => a.Postable)
                .Include(a => a.User).ToListAsync();
            }
        }
    }
}

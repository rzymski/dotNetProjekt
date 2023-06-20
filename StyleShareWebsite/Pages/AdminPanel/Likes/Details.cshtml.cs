using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel.Likes
{
    public class DetailsModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;
        private readonly IPostableService _postableService;
        private readonly IUserService _userService;

        public DetailsModel(StyleShareWebsite.Data.ApplicationDbContext context, IPostableService postableService, IUserService userService)
        {
            _context = context;
            _postableService = postableService;
            _userService = userService;
        }
        
        public Like Like { get; set; } = default!;
        public Postable Postable { get; set; } = default!;
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Debug.WriteLine("My debug string here\n");
            Debug.WriteLine(id.ToString());
            Debug.WriteLine("My debug string here\n");
            if (id == null || _context.Likes == null)
            {
                return NotFound();
            }

            var like = await _context.Likes.FirstOrDefaultAsync(m => m.Id == id);
            var postable = await _postableService.GetByIdAsync(like.PostableId);
            var user = await _userService.GetByIdAsync(like.UserId);
            if (like == null)
            {
                return NotFound();
            }
            else 
            {
                Like = like;
                Postable = postable;
                User = user;
            }
            return Page();
        }
    }
}

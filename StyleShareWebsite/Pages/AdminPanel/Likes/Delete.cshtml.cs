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
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel.Likes
{
    public class DeleteModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;
        private readonly IPostableService _postableService;
        private readonly IUserService _userService;
        public DeleteModel(StyleShareWebsite.Data.ApplicationDbContext context, IPostableService postableService, IUserService userService)
        {
            _context = context;
            _postableService = postableService;
            _userService = userService;
        }

        [BindProperty]
        public Like Like { get; set; } = default!;
        public Postable Postable { get; set; } = default!;
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Likes == null)
            {
                return NotFound();
            }

            var like = await _context.Likes.FirstOrDefaultAsync(m => m.Id == id);
            var postable = await _postableService.GetByIdAsync(like.PostableId);
            var user = await _userService.GetByIdAsync(like.UserId);

            //Debug.WriteLine(" WIDAC ZMIANY\n");
            //Debug.WriteLine(" WIDAC ZMIANY\n");
            //Debug.WriteLine(like.Postable.ToString());
            //Debug.WriteLine(" WIDAC ZMIANY\n");
            //Debug.WriteLine(" WIDAC ZMIANY\n");

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Likes == null)
            {
                return NotFound();
            }

            var like = await _context.Likes.FirstOrDefaultAsync(m => m.Id == id);

            if (like != null)
            {
                Like = like;
                _context.Likes.Remove(Like);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

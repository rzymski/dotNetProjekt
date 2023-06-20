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

namespace StyleShareWebsite.Pages.Posts
{
    public class DetailsModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;

        public DetailsModel(StyleShareWebsite.Data.ApplicationDbContext context, IPostableService postableService)
        {
            _context = context;
            _postableService = postableService;
        }

        public Post Post { get; set; }
        private readonly IPostableService _postableService;
        public List<Tag> Tags { get; set; } = new List<Tag>();


        public List<Postable> PostablePosts { get; set; } = new List<Postable>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            else 
            {
                Post = post;
                Tags = _postableService.GetAllTagsByPostableIdAsync(post.Id).Result.ToList();
            }
            return Page();
        }
    }
}

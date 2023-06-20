using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;

        public EditModel(StyleShareWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; } = default!;
        [BindProperty]
        public string confirmedPassword { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            List<SelectListItem> SelYN = new List<SelectListItem>();
            SelYN.Add(new SelectListItem
            {
                Text = "No",
                Value = false.ToString()
            });
            SelYN.Add(new SelectListItem
            {
                Text = "Yes",
                Value = true.ToString()
            });
            ViewData["YesNo"] = new SelectList(SelYN, "Value", "Text");

            List<SelectListItem> SelRole = new List<SelectListItem>();
            SelRole.Add(new SelectListItem
            {
                Text = "Admin",
                Value = Role.Admin.ToString()
            });
            SelRole.Add(new SelectListItem
            {
                Text = "Manager",
                Value = Role.Manager.ToString()
            });
            SelRole.Add(new SelectListItem
            {
                Text = "NormalUser",
                Value = Role.NormalUser.ToString()
            });
            ViewData["Role"] = new SelectList(SelRole, "Value", "Text");

            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user =  await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            User = user;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || User.Password != confirmedPassword)
            {
                return Page();
            }
            User.Password = Models.User.HashPassword(User.Password);
            _context.Attach(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserExists(int id)
        {
          return _context.Users.Any(e => e.Id == id);
        }
    }
}

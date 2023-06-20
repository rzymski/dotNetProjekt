using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StyleShareWebsite.Data;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly StyleShareWebsite.Data.ApplicationDbContext _context;

        public CreateModel(StyleShareWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
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

            return Page();
        }

        [BindProperty]
        public User User { get; set; }
        [BindProperty]
        public string confirmedPassword { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || User.Password != confirmedPassword)
            {
                return Page();
            }

            User.Password = Models.User.HashPassword(User.Password);
            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

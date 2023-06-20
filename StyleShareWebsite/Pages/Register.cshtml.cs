using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        public User user { get; set; }
        [BindProperty]
        public string confirmedPassword { get; set; }

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid && user.Password == confirmedPassword)
            {
                user.Role = 0;
                user.Blocked = false;
                user.Password = Models.User.HashPassword(user.Password);
                _userService.AddAsync(user);
                return RedirectToPage("./Login");
            }
            return Page();

        }

        public void OnGet()
        {
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services;
using StyleShareWebsite.Models;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace StyleShareWebsite.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        [BindProperty]
        public string UserName { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public bool RememberMe { get; set; }

        [FromQuery(Name = "return_url")]
        public string? ReturnUrl { get; set; }

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            

            User loggedUser = _userService.AuthorizeUser(UserName, Models.User.HashPassword(Password));
            if (loggedUser == null)
            {
                TempData["Error"] = "Niepoprawne dane logowania!";
                return RedirectToPage();
            }
            ClaimsPrincipal principal = CreatePrincipal(loggedUser);
            AuthenticationProperties props = new AuthenticationProperties();
            props.IsPersistent = RememberMe;

            await HttpContext.SignInAsync(principal, props);
            

            if (string.IsNullOrWhiteSpace(ReturnUrl))
                ReturnUrl = "/Index";

            return RedirectToPage(ReturnUrl);
        }

        ClaimsPrincipal CreatePrincipal(User user)
        {
            ClaimsPrincipal result = new ClaimsPrincipal();

            List<Claim> claims = new List<Claim>()
            {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(ClaimTypes.Name, user.Nickname)
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            result.AddIdentity(identity);

            return result;
        }

        public void OnGet()
        {
            /*User user = new User()
            {
                Nickname = "admin",
                Password = Models.User.HashPassword("admin"),
                Role = Role.Admin,
                Blocked = false,
                Email = "email@email.com"
            };
            _userService.AddAsync(user);*/
        }
    }
}

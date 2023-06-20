using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StyleShareWebsite.Pages
{
    public class LogOutModel : PageModel
    {
        [FromQuery(Name = "return_url")]
        public string? ReturnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrWhiteSpace(ReturnUrl))
                ReturnUrl = "/Index";

            
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

           return RedirectToPage(ReturnUrl);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.Pages.AdminPanel
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        public User CurrentUser { get; set; }

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = _userService.GetAuthorizedUserId(HttpContext);
                CurrentUser = _userService.GetByIdAsync(userId).Result;
                if(CurrentUser.Role == Role.Admin)
                {
                    return Page();
                }
                else return RedirectToPage("../Index");
            }
            else return RedirectToPage("../Login");


        }
    }
}

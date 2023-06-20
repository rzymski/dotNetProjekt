using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StyleShareWebsite.DataAccess.Interfaces;

namespace StyleShareWebsite.Pages
{
    public class AddLikeModel : PageModel
    {
        private ILikeService _likeService;

        public AddLikeModel(ILikeService likeService)
        {
            _likeService = likeService;
        }

        public IActionResult OnGet(int? id)
        {

            _likeService.AddLikeToPostableByIdAsync((int)id, HttpContext);

            //return RedirectToPage("./MainPage");
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}

using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services.Common;
using StyleShareWebsite.Models;
using System.Security.Claims;

namespace StyleShareWebsite.DataAccess.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(ApplicationDbContext context) : base(context)
        {
            

        }

        public User AuthorizeUser(string userName, string password)
        {
            var allUsers = GetAllAsync().Result;

            User u = allUsers.Where(x => x.Nickname == userName && x.Password == password).FirstOrDefault();
            return u;
           
        }

        public int GetAuthorizedUserId(HttpContext context)
        {
            var userId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userId);
        }
    }
}

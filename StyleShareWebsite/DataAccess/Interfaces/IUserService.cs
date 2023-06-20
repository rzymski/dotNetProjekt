using StyleShareWebsite.DataAccess.Interfaces.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Interfaces
{
    public interface IUserService : IAsyncService<User>
    {
        User AuthorizeUser(string userName, string password);

        int GetAuthorizedUserId(HttpContext context);
    }
}

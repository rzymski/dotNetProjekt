using StyleShareWebsite.DataAccess.Interfaces.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Interfaces
{
    public interface IPostService : IAsyncService<Post>
    {
        Task<IList<Post>> GetAllPostsByOwnerId(User user);
    }
}

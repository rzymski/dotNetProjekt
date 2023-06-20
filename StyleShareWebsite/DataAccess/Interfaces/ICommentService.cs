using StyleShareWebsite.DataAccess.Interfaces.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Interfaces
{
    public interface ICommentService : IAsyncService<Comment>
    {
        Task<IList<Comment>> GetAllCommentsByOwnerId(User user);

        Task<IList<Comment>> GetAllCommentsByCommentedPostableId(int id);
    }
}

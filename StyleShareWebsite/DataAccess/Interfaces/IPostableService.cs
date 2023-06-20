using StyleShareWebsite.DataAccess.Interfaces.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Interfaces
{
    public interface IPostableService : IAsyncService<Postable>
    {
        Task<IList<Tag>> GetAllTagsByPostableIdAsync(int id);
        Task<User> GetOwnerByPostableIdAsync(int id);
        //Task<Postable> UpdateAllTagsAsync(Postable entity);
        bool IsUserAlreadyLikedSpecificPostable(int userId, int postableId);
    }
}

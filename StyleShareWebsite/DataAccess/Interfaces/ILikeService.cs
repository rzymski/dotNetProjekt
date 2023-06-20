using Microsoft.AspNetCore.Mvc;
using StyleShareWebsite.DataAccess.Interfaces.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Interfaces
{
    public interface ILikeService : IAsyncService<Like>
    {
        Task<IList<Like>> GetAllLikingUsersByPostableId(int id);
        Task<IList<Like>> GetAllPostablesByLikingUsersId(int id);

        Task AddLikeToPostableByIdAsync(int id, HttpContext context);
    }
}

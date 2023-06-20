using Microsoft.AspNetCore.Mvc;
using StyleShareWebsite.DataAccess.Interfaces.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Interfaces
{
    public interface IAchievementUserService : IAsyncService<AchievementUser>
    {
        Task<IList<AchievementUser>> GetAllOwnersByAchieveId(int id);
        Task<IList<AchievementUser>> GetAllAchievementsByOwnerId(int id);
    }
}

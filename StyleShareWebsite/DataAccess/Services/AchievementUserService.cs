using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Services
{
    public class AchievementUserService : BaseService<AchievementUser>, IAchievementUserService
    {
        public AchievementUserService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IList<AchievementUser>> GetAllOwnersByAchieveId(int id)
        {
            IList<AchievementUser> userList = new List<AchievementUser>();
            var all = await GetAllAsync();
            foreach (var user in all)
            {
                if (user.AchievementId == id)
                    userList.Add(user);
            }
            return userList;
        }

        public async Task<IList<AchievementUser>> GetAllAchievementsByOwnerId(int id)
        {
            IList<AchievementUser> achieveList = new List<AchievementUser>();
            var all = await GetAllAsync();
            foreach (var achieve in all)
            {
                if (achieve.UserId == id)
                    achieveList.Add(achieve);
            }
            return achieveList;
        }
    }
}

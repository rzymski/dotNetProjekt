using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Services
{
    public class AchievementService : BaseService<Achievement>, IAchievementService
    {
        public AchievementService(ApplicationDbContext context) : base(context)
        {
        }
    }
}

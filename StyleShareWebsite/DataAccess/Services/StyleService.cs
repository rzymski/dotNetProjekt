using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Services
{
    public class StyleService : BaseService<Style>, IStyleService
    {
        public StyleService(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IList<Style>> GetAllStylesByOwnerId(User user)
        {
            IList<Style> styleList = new List<Style>();
            var all = await GetAllAsync();
            foreach (var style in all)
            {
                if (style.User == user)
                    styleList.Add(style);
            }
            return styleList;
        }
    }
}

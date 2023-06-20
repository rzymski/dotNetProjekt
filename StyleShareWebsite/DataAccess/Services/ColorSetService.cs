using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Services
{
    public class ColorSetService : BaseService<ColorSet>, IColorSetService
    {
        public ColorSetService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IList<ColorSet>> GetAllColorSetsByOwnerId(User user)
        {
            IList<ColorSet> colorSetList = new List<ColorSet>();
            var all = await GetAllAsync();
            foreach (var colorSet in all)
            {
                if (colorSet.User == user)
                    colorSetList.Add(colorSet);
            }
            return colorSetList;
        }
    }
}

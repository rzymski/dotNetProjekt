using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Services
{
    public class StyleStylePackService : BaseService<StyleStylePack>, IStyleStylePackService
    {
        public StyleStylePackService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IList<StyleStylePack>> GetAllStylePacksByStyleId(int id)
        {
            IList<StyleStylePack> stylePackList = new List<StyleStylePack>();
            var all = await GetAllAsync();
            foreach (var stylePackPack in all)
            {
                if (stylePackPack.StyleId == id)
                    stylePackList.Add(stylePackPack);
            }
            return stylePackList;
        }

        public async Task<IList<StyleStylePack>> GetAllStylesByStylePackId(int id)
        {
            IList<StyleStylePack> styleList = new List<StyleStylePack>();
            var all = await GetAllAsync();
            foreach (var stylePackPack in all)
            {
                if (stylePackPack.StylePackId == id)
                    styleList.Add(stylePackPack);
            }
            return styleList;
        }
    }
}

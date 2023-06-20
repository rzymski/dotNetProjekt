using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Services
{
    public class StylePackService : BaseService<StylePack>, IStylePackService
    {
        public StylePackService(ApplicationDbContext context) : base(context)
        {

        }
    }
}

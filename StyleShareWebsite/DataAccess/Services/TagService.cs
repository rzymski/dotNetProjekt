using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Services
{
    public class TagService : BaseService<Tag>, ITagService
    {
        public TagService(ApplicationDbContext context) : base(context)
        {

        }
    }
}

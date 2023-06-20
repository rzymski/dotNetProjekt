using Microsoft.AspNetCore.Mvc;
using StyleShareWebsite.DataAccess.Interfaces.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Interfaces
{
    public interface IStyleStylePackService : IAsyncService<StyleStylePack>
    {
        Task<IList<StyleStylePack>> GetAllStylePacksByStyleId(int id);
        Task<IList<StyleStylePack>> GetAllStylesByStylePackId(int id);
    }
}

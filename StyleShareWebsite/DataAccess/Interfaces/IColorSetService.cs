using StyleShareWebsite.DataAccess.Interfaces.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Interfaces
{
    public interface IColorSetService : IAsyncService<ColorSet>
    {
        Task<IList<ColorSet>> GetAllColorSetsByOwnerId(User user);
    }
}

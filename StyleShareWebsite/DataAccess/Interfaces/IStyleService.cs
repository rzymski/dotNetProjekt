using StyleShareWebsite.DataAccess.Interfaces.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Interfaces
{
    public interface IStyleService : IAsyncService<Style>
    {
        Task<IList<Style>> GetAllStylesByOwnerId(User user);
    }
}

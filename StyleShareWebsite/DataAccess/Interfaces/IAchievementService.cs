using Microsoft.AspNetCore.Mvc;
using StyleShareWebsite.DataAccess.Interfaces.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Interfaces
{
    public interface IAchievementService : IAsyncService<Achievement>
    {
    }
}

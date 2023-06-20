using Microsoft.EntityFrameworkCore;
using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Services
{
    public class PostableService : BaseService<Postable>, IPostableService
    {
        private ApplicationDbContext _context;

        public PostableService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<Tag>> GetAllTagsByPostableIdAsync(int id)
        {
            return await _context.Set<Postable>().Where(p => p.Id == id).SelectMany(p => p.Tags).ToListAsync();
        }

        public async Task<User> GetOwnerByPostableIdAsync(int id)
        {
            return await _context.Set<Postable>().Where(p => p.Id == id).Select(p => p.User).FirstOrDefaultAsync();
        }

        public bool IsUserAlreadyLikedSpecificPostable(int userId, int postableId)
        {
            
            var likes = _context.Set<Like>().Where(l => l.PostableId == postableId);
            var userLikes = likes.Where(l => l.UserId == userId).FirstOrDefault();
            if (userLikes != null) return true;
            else return false;

        }

        /*public async Task<Postable> UpdateAllTagsAsync(Postable entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }*/
    }
}

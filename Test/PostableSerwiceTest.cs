using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;

namespace Test
{
    public class Tests
    {
        private readonly ApplicationDbContext _context;
        private readonly IPostableService _postableService;
        private readonly IUserService _userService;
        private readonly IPostService _postService;

        public Tests(ApplicationDbContext context, IPostableService postableService, IPostService postService)
        {
            _context = context;
            _postableService = postableService;
            _postService = postService;
        }

        [SetUp]
        public void Setup()
        {

        }



        [Test]
        public void TestGetAllTagsByPostableIdAsync()
        {
        }

        /*
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

                public async Task<Postable> UpdateAllTagsAsync(Postable entity)
                {
                    _context.Entry(entity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return entity;
                }*/
    }
}
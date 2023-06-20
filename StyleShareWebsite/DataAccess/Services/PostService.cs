using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Services
{
    public class PostService : BaseService<Post>, IPostService
    {
        public PostService(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IList<Post>> GetAllPostsByOwnerId(User user)
        {
            IList<Post> postList = new List<Post>();
            var all = await GetAllAsync();
            foreach (var post in all)
            {
                if (post.User == user)
                    postList.Add(post);
            }
            return postList;
        }
    }
}

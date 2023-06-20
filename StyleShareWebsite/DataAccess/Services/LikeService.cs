using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Services
{
    public class LikeService : BaseService<Like>, ILikeService
    {
        private IUserService _userService;

        public LikeService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _userService = userService;
        }

        public async Task AddLikeToPostableByIdAsync(int id, HttpContext context)
        {
            Like like = new Like();
            like.PostableId = id;
            var userId = _userService.GetAuthorizedUserId(context);
            like.UserId = userId;
            await AddAsync(like);

        }

        public async Task<IList<Like>> GetAllLikingUsersByPostableId(int id)
        {
            IList<Like> userList = new List<Like>();
            var all = await GetAllAsync();
            foreach (var user in all)
            {
                if (user.PostableId == id)
                    userList.Add(user);
            }
            return userList;
        }

        public async Task<IList<Like>> GetAllPostablesByLikingUsersId(int id)
        {
            IList<Like> postableList = new List<Like>();
            var all = await GetAllAsync();
            foreach (var postable in all)
            {
                if (postable.UserId == id)
                    postableList.Add(postable);
            }
            return postableList;
        }
    }
}
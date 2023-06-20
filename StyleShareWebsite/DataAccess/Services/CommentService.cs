using StyleShareWebsite.Data;
using StyleShareWebsite.DataAccess.Interfaces;
using StyleShareWebsite.DataAccess.Services.Common;
using StyleShareWebsite.Models;

namespace StyleShareWebsite.DataAccess.Services
{
    public class CommentService : BaseService<Comment>, ICommentService
    {
        public CommentService(ApplicationDbContext context) :base(context)
        {

        }

        public async Task<IList<Comment>> GetAllCommentsByCommentedPostableId(int id)
        {
            IList<Comment> comments = new List<Comment>();
            var all = await GetAllAsync();
            foreach(var comment in all)
            {
                if(comment.PostableId == id)
                    comments.Add(comment);
            }

            return comments;
        }



        public async Task<IList<Comment>> GetAllCommentsByOwnerId(User user)
        {
            IList<Comment> commentList = new List<Comment>();
            var all = await GetAllAsync();
            foreach (var comment in all)
            {
                if (comment.User == user)
                    commentList.Add(comment);
            }
            return commentList;
        }
    }
}

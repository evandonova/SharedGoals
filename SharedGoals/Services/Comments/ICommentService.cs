using System.Collections.Generic;

namespace SharedGoals.Services.Comments
{
    public interface ICommentService
    {
        bool Exists(int commentId);
        public IEnumerable<CommentServiceModel> All();
        public void Add(string name, string body, string userId, int goalId);
        void Delete(int id);
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using SharedGoals.Data;
using SharedGoals.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace SharedGoals.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly SharedGoalsDbContext dbContext;
        private readonly IMapper mapper;

        public CommentService(SharedGoalsDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public bool Exists(int commentId)
            => this.dbContext
            .Comments
            .Any(c => c.Id == commentId);

        public IEnumerable<CommentServiceModel> All()
            => this.dbContext.Comments
                .ProjectTo<CommentServiceModel>(this.mapper.ConfigurationProvider)
                .ToList();

        public void Add(string name, string body, string userId, int goalId)
        {
            var comment = new Comment()
            {
                Name = name,
                Body = body,
                UserId = userId,
                GoalId = goalId
            };

            this.dbContext.Comments.Add(comment);
            this.dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var comment = this.dbContext.Comments.Find(id);

            this.dbContext.Comments.Remove(comment);
            this.dbContext.SaveChanges();
        }  
    }
}

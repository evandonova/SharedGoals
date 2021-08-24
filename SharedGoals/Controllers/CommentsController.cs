using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedGoals.Infrastructure;
using SharedGoals.Models.Comments;
using SharedGoals.Services.Comments;
using SharedGoals.Services.Goals;

namespace SharedGoals.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly IGoalService goals;
        private readonly ICommentService comments;

        public CommentsController(
            IGoalService goals,
            ICommentService comments)
        {
            this.goals = goals;
            this.comments = comments;
        }

        public IActionResult Add(int id)
        {
            if (!this.goals.Exists(id))
            {
                return BadRequest();
            }

            return View(new CommentFormModel());
        }

        [HttpPost]
        public IActionResult Add(int id, CommentFormModel commentModel)
        {
            if (!this.goals.Exists(id))
            {
                return BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(commentModel);
            }

            this.comments.Add(commentModel.Name, commentModel.Body, this.User.Id(), id);

            return this.RedirectToAction("Details", "Goals", new { id = id});
        }
    }
}

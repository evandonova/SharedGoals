using Microsoft.AspNetCore.Mvc;
using SharedGoals.Services.Comments;

namespace SharedGoals.Areas.Admin.Controllers
{
    public class CommentsController : AdminController
    {
        private readonly ICommentService comments;

        public CommentsController(ICommentService comments)
        {
            this.comments = comments;
        }

        [Route("/Comments/All")]
        public IActionResult All()
        {
            var comments = this.comments.All();

            return View(comments);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!this.comments.Exists(id))
            {
                return BadRequest();
            }

            this.comments.Delete(id);

            TempData["message"] = "Goal was deleted successfully!";
            return this.RedirectToAction("All");
        }
    }
}

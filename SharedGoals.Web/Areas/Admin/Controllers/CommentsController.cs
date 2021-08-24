using SharedGoals.Services.Comments;
using Microsoft.AspNetCore.Mvc;

namespace SharedGoals.Web.Areas.Admin.Controllers
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

        [Route("/Comments/Delete/{id}")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!this.comments.Exists(id))
            {
                return BadRequest();
            }

            this.comments.Delete(id);

            return this.RedirectToAction("All");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedGoals.Infrastructure;
using SharedGoals.Models.GoalWorks;
using SharedGoals.Services.Creators;
using SharedGoals.Services.GoalWorks;

namespace SharedGoals.Controllers
{
    public class GoalWorksController : Controller
    {
        private readonly IGoalWorkService goalWorks;
        private readonly ICreatorService creators;

        public GoalWorksController(IGoalWorkService goalWorks, ICreatorService creators)
        {
            this.goalWorks = goalWorks;
            this.creators = creators;
        }

        [Authorize]
        public IActionResult Mine()
        {
            var userId = this.User.Id();

            var goalWorks = this.goalWorks.Mine(userId);

            return View(goalWorks);
        }

        [Authorize]
        public IActionResult Work(int id)
        {
            if (!this.goalWorks.GoalExists(id))
            {
                return View();
            }

            if (this.creators.IsCreator(this.User.Id()) && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new GoalWorkFormModel());
        }

        [HttpPost]
        [Authorize]
        public IActionResult Work(int id, GoalWorkFormModel goalWorkModel)
        {
            if (!this.goalWorks.GoalExists(id))
            {
                return View();
            }

            if (this.creators.IsCreator(this.User.Id()) && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            this.goalWorks.Work(
                goalWorkModel.Description,
                this.User.Id(),
                id);

            return this.RedirectToAction("All", "Goals");
        }
    }
}

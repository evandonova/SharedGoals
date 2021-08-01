using Microsoft.AspNetCore.Mvc;
using SharedGoals.Services.GoalWorks;

namespace SharedGoals.Areas.Admin.Controllers
{
    public class GoalWorksController : AdminController
    {
        private readonly IGoalWorkService goalWorks;

        public GoalWorksController(IGoalWorkService goalWorks)
        {
            this.goalWorks = goalWorks;
        }

        [Route("/GoalWorks/All")]
        public IActionResult All()
        {
            var goalWorks = this.goalWorks.All();

            return View(goalWorks);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedGoals.Data;
using SharedGoals.Data.Models;
using SharedGoals.Infrastructure;
using SharedGoals.Models.GoalWorks;
using System.Linq;

namespace SharedGoals.Controllers
{
    public class GoalWorksController : Controller
    {
        private readonly SharedGoalsDbContext dbContext;

        public GoalWorksController(SharedGoalsDbContext dbContext)
            => this.dbContext = dbContext;

        [Authorize]
        public IActionResult All()
        {
            var goals = this.dbContext.GoalWorks
                .Select(g => new GoalWorkListingViewModel()
                {
                    Description = g.Description,
                    WorkDoneInPercents = g.WorkDoneInPercents,
                    User = this.dbContext.Users.FirstOrDefault(u => u.Id == g.UserId).UserName,
                    Goal = this.dbContext.Goals.FirstOrDefault(gl => gl.Id == g.GoalId).Name,
                })
                .ToList();
            
            return View(goals);
        }

        [Authorize]
        public IActionResult Work(int id)
        {
            var goal = this.dbContext.Goals.FirstOrDefault(g => g.Id == id);

            if (goal == null)
            {
                return View();
            }

            if (this.UserIsCreator())
            {
                return Unauthorized("Creators cannot work on goals!");
            }

            return View(new GoalWorkFormModel());
        }

        [HttpPost]
        [Authorize]
        public IActionResult Work(int id, GoalWorkFormModel goalWorkModel)
        {
            var goal = this.dbContext.Goals.FirstOrDefault(g => g.Id == id);
            if (goal == null)
            {
                return this.View();
            }

            if (this.UserIsCreator())
            {
                return Unauthorized("Creators cannot work on goals!");
            }

            var currentUser = this.dbContext.Users.FirstOrDefault(u => u.Id == this.User.GetId());
            var goalWork = new GoalWork()
            {
                Description = goalWorkModel.Description,
                WorkDoneInPercents = goalWorkModel.WorkDoneInPercents,
                UserId = currentUser.Id,
                GoalId = id
            };

            goal.ProgressInPercents += goalWork.WorkDoneInPercents;

            dbContext.GoalWorks.Add(goalWork);
            dbContext.SaveChanges();
            return this.RedirectToAction("All", "Goals");
        }

        private bool UserIsCreator()
           => this.dbContext
               .Creators
               .Any(c => c.UserId == this.User.GetId());
    }
}

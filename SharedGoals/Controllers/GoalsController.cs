using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedGoals.Data;
using SharedGoals.Infrastructure;
using SharedGoals.Models.Goals;
using SharedGoals.Services.Creators;
using SharedGoals.Services.Goals;

namespace SharedGoals.Controllers
{
    public class GoalsController : Controller
    {
        private readonly IGoalService goals;
        private readonly ICreatorService creators;

        public GoalsController(IGoalService goals, ICreatorService creators)
        {
            this.goals = goals;
            this.creators = creators;
        }

        public IActionResult All([FromQuery] AllGoalsQueryModel query)
        {
            var queryResult = this.goals.All(query.GoalsPerPage, query.CurrentPage, query.TotalGoals);

            query.TotalGoals = queryResult.TotalGoals;
            query.Goals = queryResult.Goals;

            return View(query);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var goal = this.goals.Details(id);

            return View(goal);
        }

        [Authorize]
        public IActionResult Create()
        {
            if (!this.creators.IsCreator(this.User.Id()))
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            return View(new CreateGoalFormModel
            {
                Tags = this.goals.Tags()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateGoalFormModel goal)
        {
            var creatorId = this.creators.IdByUser(this.User.Id());

            if (creatorId == null)
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            if (!this.goals.TagExists(goal.TagId))
            {
                this.ModelState.AddModelError(nameof(goal.TagId), "Tag does not exist.");
            }

            if (!this.goals.DateIsValid(goal.DueDate))
            {
                this.ModelState.AddModelError(nameof(goal.DueDate), "Due Date must be in the future and before 2100 year.");
            }

            if (!ModelState.IsValid)
            {
                goal.Tags = this.goals.Tags();

                return View(goal);
            }

            this.goals.Create(goal.Name,
                goal.Description,
                goal.DueDate,
                goal.TagId,
                creatorId);

            return RedirectToAction("All", "Goals");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var userId = this.User.Id();

            var goal = this.goals.Info(id);

            if (goal.UserId != userId)
            {
                return Unauthorized("You cannot edit a goal of another creator!");
            }

            return this.View(new GoalExtendedServiceModel()
            {
                Name = goal.Name,
                Description = goal.Description,
                DueDate = goal.DueDate,
                ProgressInPercents = goal.ProgressInPercents,
                Tag = goal.Tag
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(GoalExtendedServiceModel goalModel)
        {
            var userId = this.User.Id();
            var goalData = this.goals.Info(goalModel.Id);

            if (goalData.UserId != userId)
            {
                return Unauthorized("You cannot edit a goal of another creator!");
            }

            this.goals.Delete(goalModel.Id);

            return this.RedirectToAction("All");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            var goal = this.goals.Info(id);

            if (goal.UserId != userId)
            {
                return Unauthorized("You cannot edit a goal of another creator!");
            }

            return this.View(new CreateGoalFormModel()
            {
                Name = goal.Name,
                Description = goal.Description,
                DueDate = goal.DueDate,
                TagId = goal.TagId,
                Tags = this.goals.Tags()
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, CreateGoalFormModel goal)
        {
            var userId = this.User.Id();
            var goalData = this.goals.Info(id);

            if (goalData.UserId != userId)
            {
                return Unauthorized("You cannot edit a goal of another creator!");
            }

            if (!this.goals.TagExists(goal.TagId))
            {
                this.ModelState.AddModelError(nameof(goal.TagId), "Tag does not exist.");
            }

            if (!this.goals.DateIsValid(goal.DueDate))
            {
                this.ModelState.AddModelError(nameof(goal.DueDate), "Due Date must be in the future and before 2100 year.");
            }

            if (!this.ModelState.IsValid)
            {
                goal.Tags = this.goals.Tags();
                return this.View(goal);
            }

            this.goals.Edit(
                id, 
                goal.Name, 
                goal.Description, 
                goal.DueDate, 
                goal.TagId);

            return this.RedirectToAction("All");
        }

    }
}

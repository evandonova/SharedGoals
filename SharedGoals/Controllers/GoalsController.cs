using SharedGoals.Models.Goals;
using SharedGoals.Services.Goals;
using SharedGoals.Services.Creators;
using SharedGoals.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SharedGoals.Controllers
{
    public class GoalsController : Controller
    {
        private readonly IGoalService goals;
        private readonly ICreatorService creators;
        private readonly IMapper mapper;

        public GoalsController(IGoalService goals, ICreatorService creators, IMapper mapper)
        {
            this.goals = goals;
            this.creators = creators;
            this.mapper = mapper;
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
            if (!this.creators.IsCreator(this.User.Id()) && !this.User.IsAdmin())
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            return View(new GoalFormModel
            {
                Tags = this.goals.Tags()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(GoalFormModel goal)
        {
            var userId = this.User.Id();
            var isCreator = this.creators.IsCreator(userId);
            if (!isCreator && !this.User.IsAdmin())
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            if (!this.goals.TagExists(goal.TagId))
            {
                this.ModelState.AddModelError(nameof(goal.TagId), "Tag does not exist.");
            }

            if (!ModelState.IsValid)
            {
                goal.Tags = this.goals.Tags();

                return View(goal);
            }

            var creatorId = this.creators.IdByUser(this.User.Id());
            this.goals.Create(goal.Name,
                goal.Description,
                goal.DueDate,
                goal.ImageURL,
                goal.TagId,
                creatorId);

            TempData["message"] = "Goal was created successfully!";

            return RedirectToAction("All");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!this.goals.Exists(id))
            {
                return BadRequest();
            }

            var userId = this.User.Id();
            var goalUserId = this.goals.GetCreatorUserId(id);
            if (goalUserId != userId && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var goal = this.goals.Details(id);
            var goalModel = this.mapper.Map<GoalDetailsViewModel>(goal);

            return this.View(goalModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(GoalDetailsViewModel goalModel)
        {
            if (!this.goals.Exists(goalModel.Id))
            {
                return BadRequest();
            }

            var userId = this.User.Id();
            var goalUserId = this.goals.GetCreatorUserId(goalModel.Id);
            if (goalUserId != userId && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            this.goals.Delete(goalModel.Id);

            TempData["message"] = "Goal was deleted successfully!";
            return this.RedirectToAction("All");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            if (!this.goals.Exists(id))
            {
                return BadRequest();
            }

            var userId = this.User.Id();
            var goalUserId = this.goals.GetCreatorUserId(id);
            if (goalUserId != userId && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var goal = this.goals.Details(id);
            var goalForm = this.mapper.Map<GoalFormModel>(goal);
            goalForm.Tags = this.goals.Tags();

            return this.View(goalForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, GoalFormModel goal)
        {
            if (!this.goals.Exists(id))
            {
                return BadRequest();
            }

            var userId = this.User.Id();
            var goalUserId = this.goals.GetCreatorUserId(id);
            if (goalUserId != userId && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            if (!this.goals.TagExists(goal.TagId))
            {
                this.ModelState.AddModelError(nameof(goal.TagId), "Tag does not exist.");
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
                goal.ImageURL,
                goal.TagId);

            TempData["message"] = "Goal was edited successfully!";
            return this.RedirectToAction("All");
        }

        [Authorize]
        public IActionResult Finish(int id)
        {
            if (!this.goals.Exists(id) || this.goals.IsFinished(id))
            {
                return BadRequest();
            }

            var userId = this.User.Id();
            var goalUserId = this.goals.GetCreatorUserId(id);
            if (goalUserId != userId && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var goal = this.goals.Details(id);
            var goalModel = this.mapper.Map<GoalDetailsViewModel>(goal);

            return this.View(goalModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Finish(GoalDetailsViewModel goalModel)
        {
            if (!this.goals.Exists(goalModel.Id) || this.goals.IsFinished(goalModel.Id))
            {
                return BadRequest();
            }

            var userId = this.User.Id();
            var goalUserId = this.goals.GetCreatorUserId(goalModel.Id);
            if (goalUserId != userId && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            this.goals.Finish(goalModel.Id);

            TempData["message"] = "Goal was finished successfully!";
            return this.RedirectToAction("All");
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var creatorId = this.creators.IdByUser(this.User.Id());

            if (creatorId == null && !this.User.IsAdmin())
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
            var goal = this.goals.Info(id);
            if (goal == null)
            {
                return BadRequest();
            }

            var userId = this.User.Id();
            if (goal.UserId != userId && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var goalModel = this.mapper.Map<GoalDetailsViewModel>(goal);

            return this.View(goalModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(GoalDetailsViewModel goalModel)
        {
            var goalData = this.goals.Info(goalModel.Id);
            if (goalData == null)
            {
                return BadRequest();
            }

            var userId = this.User.Id();
            if (goalData.UserId != userId && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var deleted = this.goals.Delete(goalModel.Id);

            if(!deleted)
            {
                return BadRequest();
            }

            TempData["message"] = "Goal was deleted successfully!";
            return this.RedirectToAction("All");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            var goal = this.goals.Info(id);

            if (goal == null)
            {
                return BadRequest();
            }

            if (goal.UserId != userId && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var goalForm = this.mapper.Map<GoalFormModel>(goal);
            goalForm.Tags = this.goals.Tags();
            return this.View(goalForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, GoalFormModel goal)
        {
            var goalData = this.goals.Info(id);
            if (goalData == null)
            {
                return BadRequest();
            }

            var userId = this.User.Id();
            if (goalData.UserId != userId && !this.User.IsAdmin())
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

            var edited = this.goals.Edit(
                id, 
                goal.Name, 
                goal.Description, 
                goal.DueDate, 
                goal.ImageURL,
                goal.TagId);

            if(!edited)
            {
                return BadRequest();
            }

            TempData["message"] = "Goal was edited successfully!";
            return this.RedirectToAction("All");
        }

        [Authorize]
        public IActionResult Finish(int id)
        {
            var goal = this.goals.Info(id);
            if (goal == null || this.goals.IsFinished(id))
            {
                return BadRequest();
            }

            var userId = this.User.Id();
            if (goal.UserId != userId && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var goalModel = this.mapper.Map<GoalDetailsViewModel>(goal);

            return this.View(goalModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Finish(GoalDetailsViewModel goalModel)
        {
            var goalData = this.goals.Info(goalModel.Id);
            if (goalData == null || this.goals.IsFinished(goalModel.Id))
            {
                return BadRequest();
            }

            var userId = this.User.Id();
            if (goalData.UserId != userId && !this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var finished = this.goals.Finish(goalModel.Id);

            if(!finished)
            {
                return BadRequest();
            }

            TempData["message"] = "Goal was finished successfully!";
            return this.RedirectToAction("All");
        }
    }
}

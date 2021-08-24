using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedGoals.Controllers.Infrastructure;
using SharedGoals.Controllers.Models.Goals;
using SharedGoals.Services.Creators;
using SharedGoals.Services.Goals;

namespace SharedGoals.Controllers
{
    [Authorize]
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
            var queryResult = this.goals.All(query.GoalsPerPage, query.CurrentPage);

            if(queryResult.CurrentPage == 0)
            {
                return RedirectToAction("All");
            }

            query.TotalGoals = queryResult.TotalGoals;
            query.Goals = queryResult.Goals;
            return View(query);
        }

        public IActionResult Details(int id)
        {
            if(!this.goals.Exists(id))
            {
                return BadRequest();
            }

            var goal = this.goals.Details(id);

            return View(goal);
        }

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
        public IActionResult Create(GoalFormModel goal)
        {
            var userId = this.User.Id();
            var isCreator = this.creators.IsCreator(userId);
            if (!isCreator)
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

        public IActionResult Delete(int id)
        {
            if (!this.goals.Exists(id))
            {
                return BadRequest();
            }

            var userId = this.User.Id();
            var goalUserId = this.goals.GetCreatorUserId(id);
            if (!this.User.IsAdmin() && goalUserId != userId)
            {
                return Unauthorized();
            }

            var goal = this.goals.Details(id);
            var goalModel = this.mapper.Map<GoalDetailsViewModel>(goal);

            return this.View(goalModel);
        }

        [HttpPost]
        public IActionResult Delete(GoalDetailsViewModel goalModel)
        {
            if (!this.goals.Exists(goalModel.Id))
            {
                return BadRequest();
            }

            var userId = this.User.Id();
            var goalUserId = this.goals.GetCreatorUserId(goalModel.Id);
            if (!this.User.IsAdmin() && goalUserId != userId)
            {
                return Unauthorized();
            }

            this.goals.Delete(goalModel.Id);

            TempData["message"] = "Goal was deleted successfully!";
            return this.RedirectToAction("All");
        }

        public IActionResult Edit(int id)
        {
            if (!this.goals.Exists(id))
            {
                return BadRequest();
            }

            var userId = this.User.Id();
            var goalUserId = this.goals.GetCreatorUserId(id);
            if (!this.User.IsAdmin() && goalUserId != userId)
            {
                return Unauthorized();
            }

            var goal = this.goals.Details(id);
            var goalForm = this.mapper.Map<GoalFormModel>(goal);
            goalForm.Tags = this.goals.Tags();

            return this.View(goalForm);
        }

        [HttpPost]
        public IActionResult Edit(int id, GoalFormModel goal)
        {
            if (!this.goals.Exists(id))
            {
                return BadRequest();
            }

            var userId = this.User.Id();
            var goalUserId = this.goals.GetCreatorUserId(id);
            if (!this.User.IsAdmin() && goalUserId != userId)
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

        public IActionResult Finish(int id)
        {
            if (!this.goals.Exists(id) || this.goals.IsFinished(id))
            {
                return BadRequest();
            }

            var userId = this.User.Id();
            var goalUserId = this.goals.GetCreatorUserId(id);
            if (!this.User.IsAdmin() && goalUserId != userId)
            {
                return Unauthorized();
            }

            var goal = this.goals.Details(id);
            var goalModel = this.mapper.Map<GoalDetailsViewModel>(goal);

            return this.View(goalModel);
        }

        [HttpPost]
        public IActionResult Finish(GoalDetailsViewModel goalModel)
        {
            if (!this.goals.Exists(goalModel.Id) || this.goals.IsFinished(goalModel.Id))
            {
                return BadRequest();
            }

            var userId = this.User.Id();
            var goalUserId = this.goals.GetCreatorUserId(goalModel.Id);
            if (!this.User.IsAdmin() && goalUserId != userId)
            {
                return Unauthorized();
            }

            this.goals.Finish(goalModel.Id);

            TempData["message"] = "Goal was finished successfully!";
            return this.RedirectToAction("All");
        }
    }
}

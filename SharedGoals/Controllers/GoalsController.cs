using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedGoals.Data;
using SharedGoals.Data.Models;
using SharedGoals.Infrastructure;
using SharedGoals.Models.Goals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedGoals.Controllers
{
    public class GoalsController : Controller
    {
        private readonly SharedGoalsDbContext dbContext;

        public GoalsController(SharedGoalsDbContext dbContext)
            => this.dbContext = dbContext;

        public IActionResult All([FromQuery] AllGoalsQueryModel query)
        {
            var goals = this.dbContext.Goals
                .Skip((query.CurrentPage - 1) * AllGoalsQueryModel.CarsPerPage)
                .Take(AllGoalsQueryModel.CarsPerPage)
                .Select(g => new GoalListingViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    DueDate = g.DueDate.ToString("dd-MM-yyyy"),
                    ProgressInPercents = g.ProgressInPercents.ToString(),
                    Tag = this.dbContext.Tags.FirstOrDefault(t => t.Id == g.TagId).Name
                })
                .ToList();

            var totalGoals = this.dbContext.Goals.Count();

            query.TotalGoals = totalGoals;
            query.Goals = goals;

            return View(query);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var goal = this.dbContext.Goals.FirstOrDefault(g => g.Id == id);

            if (goal == null)
            {
                return View();
            }

            var goalData = new GoalDetailsViewModel()
            {
                Id = goal.Id,
                Name = goal.Name,
                Description = goal.Description,
                CreatedOn = goal.CreatedOn.ToString("dd-MM-yyyy"),
                DueDate = goal.DueDate.ToString("dd-MM-yyyy"),
                ProgressInPercents = goal.ProgressInPercents.ToString(),
                Tag = this.dbContext.Tags.FirstOrDefault(t => t.Id == goal.TagId).Name
            };

            return View(goalData);
        }

        [Authorize]
        public IActionResult Create()
        {
            if (!this.UserIsCreator())
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            return View(new CreateGoalFormModel
            {
                Tags = GetGoalTags()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateGoalFormModel goal)
        {
            var creatorId = this.dbContext
                .Creators
                .Where(c => c.UserId == this.User.GetId())
                .FirstOrDefault()
                .Id;

            if (creatorId == null)
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            if (!this.dbContext.Tags.Any(c => c.Id == goal.TagId))
            {
                this.ModelState.AddModelError(nameof(goal.TagId), "Tag does not exist.");
            }

            if (goal.DueDate <= DateTime.UtcNow || goal.DueDate.Year > 2100)
            {
                this.ModelState.AddModelError(nameof(goal.DueDate), "Due Date must be in the future and before 2100 year.");
            }

            if (!ModelState.IsValid)
            {
                goal.Tags = GetGoalTags();

                return View(goal);
            }

            var goalData = new Goal
            {
                Name = goal.Name,
                Description = goal.Description,
                CreatedOn = DateTime.UtcNow,
                DueDate = goal.DueDate,
                TagId = goal.TagId,
                CreatorId = creatorId
            };

            this.dbContext.Goals.Add(goalData);
            this.dbContext.SaveChanges();

            return RedirectToAction("All", "Goals");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var goal = this.dbContext.Goals.FirstOrDefault(g => g.Id == id);

            if (!this.UserIsCreator())
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            if (goal == null)
            {
                return View();
            }

            var currentUser = this.dbContext.Creators.FirstOrDefault(u => u.UserId == this.User.GetId());
            if (goal.CreatorId != currentUser.Id)
            {
                return Unauthorized("You cannot delete a goal of another creator!");
            }

            var tagName = this.dbContext.Tags.FirstOrDefault(t => t.Id == goal.TagId).Name;
            GoalListingViewModel model = new GoalListingViewModel()
            {
                Id = goal.Id,
                Name = goal.Name,
                ProgressInPercents = goal.ProgressInPercents.ToString(),
                DueDate = goal.DueDate.ToString("dd/MM/yyyy"),
                Tag = tagName
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(GoalListingViewModel goalModel)
        {
            var goal = this.dbContext.Goals.FirstOrDefault(g => g.Id == goalModel.Id);
            if (goal == null)
            {
                return this.View();
            }

            if (!this.UserIsCreator())
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            var currentUser = this.dbContext.Creators.FirstOrDefault(u => u.UserId == this.User.GetId());
            if (goal.CreatorId != currentUser.Id)
            {
                return Unauthorized("You cannot delete a goal of another creator!");
            }

            dbContext.Goals.Remove(goal);
            dbContext.SaveChanges();
            return this.RedirectToAction("All");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var goal = this.dbContext.Goals.FirstOrDefault(g => g.Id == id);

            if (goal == null)
            {
                return this.View();
            }

            if (!this.UserIsCreator())
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            var currentUser = this.dbContext.Creators.FirstOrDefault(u => u.UserId == this.User.GetId());
            if (goal.CreatorId != currentUser.Id)
            {
                return Unauthorized("You cannot edit a goal of another creator!");
            }

            CreateGoalFormModel model = new CreateGoalFormModel()
            {
                Name = goal.Name,
                Description = goal.Description,
                DueDate = goal.DueDate,
                TagId = goal.TagId,
                Tags = GetGoalTags()
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, CreateGoalFormModel bindingModel)
        {
            var goal = this.dbContext.Goals.FirstOrDefault(g => g.Id == id);
            if (goal == null)
            {
                return this.View();
            }

            if (!this.UserIsCreator())
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            var currentUser = this.dbContext.Creators.FirstOrDefault(u => u.UserId == this.User.GetId());
            if (goal.CreatorId != currentUser.Id)
            {
                return Unauthorized("You cannot edit a goal of another creator!");
            }

            if (!this.dbContext.Tags.Any(c => c.Id == bindingModel.TagId))
            {
                this.ModelState.AddModelError(nameof(bindingModel.TagId), "Tag does not exist.");
            }

            if (bindingModel.DueDate <= DateTime.UtcNow || bindingModel.DueDate.Year > 2100)
            {
                this.ModelState.AddModelError(nameof(bindingModel.DueDate), "Due Date must be in the future and before 2100 year.");
            }

            if (!this.ModelState.IsValid)
            {
                bindingModel.Tags = GetGoalTags();
                return this.View(bindingModel);
            }

            goal.Name = bindingModel.Name;
            goal.Description = bindingModel.Description;
            goal.DueDate = bindingModel.DueDate;
            goal.TagId = bindingModel.TagId;

            dbContext.SaveChanges();
            return this.RedirectToAction("All");
        }

        private bool UserIsCreator()
            => this.dbContext
                .Creators
                .Any(c => c.UserId == this.User.GetId());

        private IEnumerable<GoalTagViewModel> GetGoalTags()
            => this.dbContext
                .Tags
                .Select(c => new GoalTagViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

    }
}

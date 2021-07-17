using Microsoft.AspNetCore.Mvc;
using SharedGoals.Data;
using SharedGoals.Data.Models;
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

        public IActionResult All()
        {
            var goals = this.dbContext.Goals
                .Select(g => new GoalListingViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    DueDate = g.DueDate.ToString("dd-MM-yyyy"),
                    ProgressInPercents = g.ProgressInPercents.ToString(),
                    Tag = this.dbContext.Tags.FirstOrDefault(t => t.Id == g.TagId).Name
                })
                .ToList();

            return View(goals);
        }

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

        public IActionResult Create() => View(new CreateGoalFormModel
        {
            Tags = GetGoalTags()
        });

        [HttpPost]
        public IActionResult Create(CreateGoalFormModel goal)
        {
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
               TagId = goal.TagId
            };

            this.dbContext.Goals.Add(goalData);
            this.dbContext.SaveChanges();

            return Redirect("/");
        }

        public IActionResult Delete(int id)
        {
            var goal = this.dbContext.Goals.FirstOrDefault(g => g.Id == id);

            if (goal == null)
            {
                return this.View();
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
        public IActionResult Delete(GoalListingViewModel goalModel)
        {
            var goal = this.dbContext.Goals.FirstOrDefault(g => g.Id == goalModel.Id);
            if (goal == null)
            {
                return this.View();
            }

            dbContext.Goals.Remove(goal);
            dbContext.SaveChanges();
            return this.RedirectToAction("All");
        }

        public IActionResult Edit(int id)
        {
            var goal = this.dbContext.Goals.FirstOrDefault(g => g.Id == id);

            if (goal == null)
            {
                return this.View();
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

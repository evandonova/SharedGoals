﻿using Microsoft.AspNetCore.Authorization;
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
        public IActionResult All()
        {
            var goalWorks = this.goalWorks.All();

            return View(goalWorks);
        }

        [Authorize]
        public IActionResult Work(int id)
        {
            if (!this.goalWorks.GoalExists(id))
            {
                return View();
            }

            if (this.creators.IsCreator(this.User.Id()))
            {
                return Unauthorized("Creators cannot work on goals!");
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

            if (this.creators.IsCreator(this.User.Id()))
            {
                return Unauthorized("Creators cannot work on goals!");
            }

            this.goalWorks.Work(
                goalWorkModel.Description,
                goalWorkModel.WorkDoneInPercents,
                this.User.Id(),
                id);

            return this.RedirectToAction("All", "Goals");
        }
    }
}

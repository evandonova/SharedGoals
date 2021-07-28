﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedGoals.Data;
using SharedGoals.Infrastructure;
using SharedGoals.Services.Creators;

namespace SharedGoals.Controllers
{
    public class CreatorsController : Controller
    {
        private readonly ICreatorService creators;

        public CreatorsController(ICreatorService creators)
        {
            this.creators = creators;
        }

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeCreatorServiceModel creator)
        {
            var userId = this.User.Id();

            var userIsAlreadyCreator = this.creators.IsCreator(userId);

            if (userIsAlreadyCreator)
            {
                return BadRequest();
            }

            var userWithNameAlreadyCreator = this.creators.IsCreatorByName(creator.Name);

            if (userWithNameAlreadyCreator)
            {
                this.ModelState.AddModelError(nameof(creator.Name), "This name is already taken!");
            }

            if (!ModelState.IsValid)
            {
                return View(creator);
            }

            this.creators.Become(userId, creator.Name);

            return RedirectToAction("All", "Goals");
        }
    }
}

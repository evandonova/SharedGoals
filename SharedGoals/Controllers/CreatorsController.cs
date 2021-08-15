using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedGoals.Infrastructure;
using SharedGoals.Models.Creators;
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
        public IActionResult Become() 
        {
            var userId = this.User.Id();

            var userIsAlreadyCreator = this.creators.IsCreator(userId);

            if (userIsAlreadyCreator || this.User.IsAdmin())
            {
                return BadRequest();
            }

            return View(); 
        }

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeCreatorFormModel creator)
        {
            var userWithNameAlreadyCreator = this.creators.IsCreatorByName(creator.Name);

            if (userWithNameAlreadyCreator)
            {
                this.ModelState.AddModelError(nameof(creator.Name), "This name is already taken!");
            }

            if (!ModelState.IsValid)
            {
                return View(creator);
            }

            this.creators.Become(this.User.Id(), creator.Name);

            return RedirectToAction("Index", "Home");
        }
    }
}

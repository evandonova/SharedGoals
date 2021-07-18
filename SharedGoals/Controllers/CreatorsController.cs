using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedGoals.Data;
using SharedGoals.Data.Models;
using SharedGoals.Infrastructure;
using SharedGoals.Models.Creators;

namespace SharedGoals.Controllers
{
    public class CreatorsController : Controller
    {
        private readonly SharedGoalsDbContext data;

        public CreatorsController(SharedGoalsDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeCreatorFormModel creator)
        {
            var userId = this.User.GetId();

            var userIdAlreadyCreator = this.data
                .Creators
                .Any(c => c.UserId == userId);

            if (userIdAlreadyCreator)
            {
                return BadRequest();
            }

            var userWithNameAlreadyCreator = this.data
                .Creators
                .Any(c => c.Name == creator.Name);

            if(userWithNameAlreadyCreator)
            {
                this.ModelState.AddModelError(nameof(creator.Name), "This name is already taken!");
            }

            if (!ModelState.IsValid)
            {
                return View(creator);
            }

            var creatorData = new Creator
            {
                Name = creator.Name,
                UserId = userId
            };

            this.data.Creators.Add(creatorData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Goals");
        }
    }
}

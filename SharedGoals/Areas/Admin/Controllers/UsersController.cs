using Microsoft.AspNetCore.Mvc;
using SharedGoals.Services.Users;

namespace SharedGoals.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly IUserService users;

        public UsersController(IUserService users) 
            => this.users = users;

        [Route("Users/All")]
        public IActionResult All()
        {
            var users = this.users.All();

            return View(users);
        }
    }
}

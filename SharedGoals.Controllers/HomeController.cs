using Microsoft.AspNetCore.Mvc;

namespace SharedGoals.Controllers
{
    using static Constants;
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (this.User.IsInRole(AdministratorRoleName))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View();
        }

        public IActionResult Error() => View();

        public IActionResult Error401()
        {
            return this.View();
        }

        public IActionResult Error404()
        {
            return this.View();
        }

        public IActionResult Error500()
        {
            return this.View();
        }
    }
}

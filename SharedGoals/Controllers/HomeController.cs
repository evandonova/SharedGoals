using Microsoft.AspNetCore.Mvc;
using SharedGoals.Models;
using System.Diagnostics;

using static SharedGoals.Areas.Admin.AdminConstants;

namespace SharedGoals.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if(this.User.IsInRole(AdministratorRoleName))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

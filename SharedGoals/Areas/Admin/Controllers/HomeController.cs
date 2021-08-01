using Microsoft.AspNetCore.Mvc;

namespace SharedGoals.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public IActionResult Index() => View();
    }
}

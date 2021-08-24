using Microsoft.AspNetCore.Mvc;

namespace SharedGoals.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public IActionResult Index() => View();
    }
}

﻿using Microsoft.AspNetCore.Mvc;

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
    }
}
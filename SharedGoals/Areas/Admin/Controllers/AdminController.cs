using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SharedGoals.Areas.Admin.Controllers
{
    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public abstract class AdminController : Controller
    {
    }
}

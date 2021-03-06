using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SharedGoals.Web.Areas.Admin.Controllers
{
    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public abstract class AdminController : Controller
    {
    }
}

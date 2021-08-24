using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace SharedGoals.Web.Infrastructure
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void MapDefaultAreaRoute(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
        }
    }
}

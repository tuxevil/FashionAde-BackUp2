using System.Web.Mvc;
using System.Web.Routing;
using SharpArch.Web.Areas;

namespace FashionAde.Web.Controllers
{
    public class RouteRegistrar
    {
        public static void RegisterRoutesTo(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            // Routing config for the root area
            routes.MapRoute(null, "", new { controller = "Home", action = "Index" });
            routes.MapRoute(null, "{controller}/{action}/default.aspx", new { controller = "Home", action = "Index" });
            routes.MapRoute("Rating", "outfit/{controller}/{action}/{id}/default.aspx", new { controller = "Rating", action = "Index" });
            routes.MapRoute("Content", "content/{contentType}/{title}/{id}/default.aspx", new { controller = "Content", action = "Content" });
        }
    }
}

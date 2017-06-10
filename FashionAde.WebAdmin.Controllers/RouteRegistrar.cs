using System.Web.Mvc;
using System.Web.Routing;
using SharpArch.Web.Areas;

namespace FashionAde.WebAdmin.Controllers
{
    public class RouteRegistrar
    {
        public static void RegisterRoutesTo(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(null, "", new { controller = "Grid", action = "Index" });
            routes.MapRoute(null, "{controller}/{action}/default.aspx", new { controller = "Home", action = "Index" });
            routes.MapRoute(null, "{controller}/{action}/{id}/default.aspx");
            routes.MapRoute(null, "{namespace}/{controller}/{action}/default.aspx");
            routes.MapRoute(null, "{namespace}/{controller}/{action}/{id}/default.aspx");
        }
    }
}
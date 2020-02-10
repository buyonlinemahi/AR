using System.Web.Mvc;
using System.Web.Routing;

namespace LMGEDIApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "AssignCheck",
            //    url: "{CheckAssignment}/{AssignCheck}/{OCRId}",
            //    defaults: new { controller = "CheckAssignment", action = "AssignCheck", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "user", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
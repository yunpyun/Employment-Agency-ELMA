using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EmploymentAgency
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Agency", action = "Vacancies", id = UrlParameter.Optional }
            );*/

            /*
            routes.MapRoute(
              "Logout",
              "Logout",
              new { controller = "Home", action = "Logout" }
            );*/

            routes.MapRoute(
                "HomeAction",
                "Home/{action}",
                new { controller = "Home", action = "Login" }
                );
            

            // default route
            routes.MapRoute(
                "Action",
                "{action}",
                new { controller = "Agency", action = "Vacancies" }
            );
        }
    }
}

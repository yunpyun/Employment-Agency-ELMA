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

            routes.MapRoute(
                "Login",
                "Account/Login",
                new { controller = "Home", action = "Login" }
                );

            routes.MapRoute(
                "Logout",
                "Logout",
                new { controller = "Home", action = "Logout" }
                );

            routes.MapRoute(
                "Registration",
                "Registration",
                new { controller = "Home", action = "Registration" }
                );

            routes.MapRoute(
                "UserProfile",
                "UserProfile",
                new { controller = "Home", action = "UserProfile" }
                );

            routes.MapRoute(
                "UserProfileEdit",
                "UserProfileEdit",
                new { controller = "Home", action = "UserProfileEdit" }
                );

            routes.MapRoute(
                "ListUsers",
                "ListUsers",
                new { controller = "Home", action = "ListUsers" }
                );

            routes.MapRoute(
                "ApproveRoleUser",
                "ApproveRoleUser",
                new { controller = "Home", action = "ApproveRoleUser" }
                );

            routes.MapRoute(
                "CandidateAction",
                "AgencyCandidate/{action}",
                new { controller = "AgencyCandidate", action = "Candidates" }
                );

            routes.MapRoute(
                "Default",
                "{action}",
                new { controller = "Agency", action = "Vacancies" }
                );
        }
    }
}

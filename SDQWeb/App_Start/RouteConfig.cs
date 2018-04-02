using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SDQWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                  name: "PresentAdd", 
                  url: "{controller}/{action}/{id}",
                  defaults: new { controller = "Users", action = "ViewAdd", UrlParameter.Optional }
                );
            routes.MapRoute(
                  name: "AddCourse",
                  url: "{controller}/{action}/{id}",
                  defaults: new { controller = "StudentCourses", action = "Create", UrlParameter.Optional }
                );
        }
    }
}

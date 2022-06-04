using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RaoVat
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Blog",
               url: "{controller}/{action}",
               defaults: new { controller = "Blog", action = "Index"}
            );
            routes.MapRoute(
               name: "BlogDetail",
               url: "{controller}/{action}/{slug}",
               defaults: new { controller = "Blog", action = "Detail" ,slug = UrlParameter.Optional}
            );
            routes.MapRoute(
              name: "UserProfile",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "UserProfile", action = "Index" }
            );
            routes.MapRoute(
            name: "EditInfo",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "UserProfile", action = "EditInfo" }
            );
            routes.MapRoute(
           name: "CreateNews",
           url: "{controller}/{action}/{id}",
           defaults: new { controller = "UserProfile", action = "createOfNews" }
           );

        }
    }
}

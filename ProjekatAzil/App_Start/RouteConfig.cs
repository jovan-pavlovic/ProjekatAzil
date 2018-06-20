using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjekatAzil
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "IndexDogs",
                url: "Dogs",
                defaults: new { controller = "Dogs", action = "Index", wishlist = false, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Wishlist",
                url: "Wishlist",
                defaults: new { controller = "Dogs", action = "Index", wishlist = true, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

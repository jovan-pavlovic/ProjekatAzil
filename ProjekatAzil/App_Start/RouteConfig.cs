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
                "DogsIndex",
                "Dogs",
                new { controller = "Dogs", action = "Index", wishlist = false }
            );

            routes.MapRoute(
                "Wishlist",
                "Dogs/Wishlist",
                new { controller = "Dogs", action = "Index", wishlist = true }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

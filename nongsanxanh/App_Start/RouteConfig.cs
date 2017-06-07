using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing.Constraints;
using System.Web.Mvc;
using System.Web.Routing;

namespace nongsanxanh
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "AccountInfo", // Route name
                url: "AccountInfo/{accountid}", // URL with parameters
                defaults: new { controller = "AccountInfo", action = "Index", accountid = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                name: "AccountInfoEdit", // Route name
                url: "AccountInfo/Edit/{accountid}", // URL with parameters
                defaults: new { controller = "AccountInfo", action = "Edit", accountid = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                name: "Contacts", // Route name
                url: "Contact", // URL with parameters
                defaults: new { controller = "Contact", action = "Index" } // Parameter defaults
            );
            routes.MapRoute(
                name: "TinTuc", // Route name
                url: "TinTuc", // URL with parameters
                defaults: new { controller = "TinTuc", action = "Index" } // Parameter defaults
            );
            routes.MapRoute(
                name: "TinTucGroup",
                url: "TinTuc/{catid}",
                defaults: new { controller = "TinTuc", action = "Group", catid = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "NewsDetails",
                url: "TinTuc/{catid}/{newsid}",
                defaults: new { controller = "TinTuc", action = "Details", catid = UrlParameter.Optional, newsid = UrlParameter.Optional }
            );
            routes.MapRoute(
             name: "Products", // Route name
             url: "Products", // URL with parameters
             defaults: new { controller = "Products", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
               name: "ProductsGroup",
               url: "Products/{catid}",
               defaults: new { controller = "Products", action = "Group", catid = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "Details",
              url: "Products/{catid}/{productid}",
              defaults: new { controller = "Products", action = "Details", catid = UrlParameter.Optional, productid = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

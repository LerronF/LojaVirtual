using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LojaVirtual.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Defaults",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Vendas", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Produto",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Produto", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}

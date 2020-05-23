using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Stripe_ASP.NET_MVC5
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );

            #region Stripe Hooks

            routes.MapRoute(
                "Checkout", // Route name
                "create-payment-intent", // URL with parameters
                new {controller = "Checkout", action = "Create"} // Parameter defaults
            );

            routes.MapRoute(
                "WebHook", // Route name
                "webhook", // URL with parameters
                new {controller = "WebHook", action = "Index"} // Parameter defaults
            );

            #endregion
        }
    }
}
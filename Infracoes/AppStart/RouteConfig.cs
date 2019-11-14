using System.Web.Routing;
using System.Web.Mvc;

namespace Infracoes.AppStart
{
    public static class RouteConfig
    {
        public static void MapView(this RouteCollection routes, string path, string template)
        {
            routes.MapRoute(path, path, new
            {
                controller = "App",
                action = "AppView",
                view = template
            });
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Default",
                url:"{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "API",
                url: "{controller}/{action}",
                defaults: null
                );

            routes.MapRoute(
                name: "Index",
                url: "{controller}",
                defaults: new
                {
                    action = "Index"
                },
                constraints: new
                {
                    httpMethod = new HttpMethodConstraint("Get"),
                                
                });

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new
                {
                    action = "Index",
                    controller = "Veiculos"
                },
                constraints: new
                {
                    httpMethod = new HttpMethodConstraint("Get"),

                });

            //routes.MapRoute(
            //    name: "default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new {
            //        action = "BoasVindas",
            //        controller = "Index",
            //        id = UrlParameter.Optional
            //    },
            //    constraints: new
            //    {
            //        httpMethod = new HttpMethodConstraint("Get"),

            //    });

        }
    }
}
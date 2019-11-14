using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Infracoes.AppStart.Bundle;

namespace Infracoes.AppStart
{
    public class AppHttp : HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MvcHandler.DisableMvcResponseHeader = true;

            BundleTable.Bundles.Add(new StyleBundleConfig());
            BundleTable.Bundles.Add(new ScriptBundleConfig());
            BundleTable.EnableOptimizations = true;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Accept, Authorization, Content-Type");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            HttpException httpException = ex as HttpException;

            if (httpException != null)
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        {
                            Response.Redirect("/PaginaNaoEncontrada");
                            break;
                        }
                }
            }
        }
    }
}
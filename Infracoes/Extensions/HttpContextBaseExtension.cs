using System.Text.RegularExpressions;
using System.Web;

namespace Infracoes.Extensions
{
    public static class HttpContextBaseExtension
    {
        //public static Identidade Identidade(this HttpContextBase httpcontext)
        //{
        //    return httpcontext.Items["Identidade"] as Identidade;
        //}

        //public static void Identidade(this HttpContextBase httpcontext, Identidade identidade)
        //{
        //    httpcontext.Items["Identidade"] = identidade;
        //}

        public static string Token(this HttpContextBase httpContext)
        {
            HttpCookie cookie = httpContext.Request.Cookies["tokenPortalSsp"];

            if (cookie == null)
            {
                return null;
            }

            string token = cookie.Value;

            if (!Regex.IsMatch(token, "^[0-9A-F]{32}$"))
            {
                return null;
            }

            return token;
        }
    }
}

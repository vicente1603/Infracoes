using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Infracoes.Extensions
{
    public static class RedirectResultExtension
    {
        public static RedirectResult RedirectTo(this Controller controller, string url)
        {
            return new RedirectResult(url);
        }

        public static RedirectResult RedirectTo(this FilterAttribute controller, string url)
        {
            return new RedirectResult(url);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Infracoes.Controllers.Dbo
{
    public class BoasVindasController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/BoasVindas.cshtml");
        }
    }
}
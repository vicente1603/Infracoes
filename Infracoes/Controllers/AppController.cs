using System.Web.Mvc;

namespace Infracoes.Controllers
{
    public class AppController : Controller
    {
        [HttpGet]
        public ActionResult AppIndex()
        {
            return View("~/Views/Site/Portal.cshtml");
        }

        [HttpGet]
        public ActionResult AppView()
        {
            return View((string)RouteData.Values["view"]);
        }

        [HttpGet]
        public ActionResult AppLogin()
        {
            return View("~/Views/ControleAcesso/Login.cshtml");
        }

        [HttpGet]
        public ActionResult AppResource()
        {
            return File(string.Format("~/{0}", RouteData.Values["resource"]), "application/octet-stream");
        }
    }
}

using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace Infracoes.Extensions
{
    public static class ControllerExtension
    {
        public static string GetUserTokenID(this Controller controller)
        {
            return controller.Request.Headers["Authorization"];
        }

        public static JsonResult Message(this Controller controller, string message)
        {
            controller.ControllerContext.HttpContext.Response.StatusCode = 200;

            JsonResult json = new JsonResult();
            json.Data = message;

            return json;
        }

        public static JsonResult ErrorMessage(this Controller controller, params string[] errors)
        {
            controller.ControllerContext.HttpContext.Response.StatusCode = 422;

            JsonResult json = new JsonResult();
            json.Data = errors;

            return json;
        }

        public static JsonResult ModelErrors(this Controller controller)
        {
            controller.ControllerContext.HttpContext.Response.StatusCode = 422;

            JsonResult json = new JsonResult();
            json.Data = controller.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();

            return json;
        }

        public static HttpStatusCodeResult Success(this Controller controller)
        {
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public static ContentResult AccessDenied(this Controller controller, string message)
        {
            controller.ControllerContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

            ContentResult text = new ContentResult();
            text.Content = message;
            text.ContentType = "text/plain";

            return text;
        }

        public static HttpStatusCodeResult Unauthorized(this Controller controller)
        {
            return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
        }

        public static string ViewToString(this Controller controller, string viewName)
        {
            return ViewToString(controller, viewName, null);
        }

        public static string ViewToString(this Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;

            using (StringWriter stringWriter = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, stringWriter);
                viewResult.View.Render(viewContext, stringWriter);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);

                return stringWriter.GetStringBuilder().ToString();
            }
        }
    }
}
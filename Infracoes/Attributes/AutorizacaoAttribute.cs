using System.Web.Mvc;

namespace Infracoes.Attributes
{
    public class AutorizacaoAttribute : AuthorizeAttribute
    {
        public string Permissao { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool postRequest = filterContext.HttpContext.Request.HttpMethod == "POST";

           // ValidacaoIdentidade validacaoIdentidade = new ValidacaoIdentidade();
           // validacaoIdentidade.Token = filterContext.HttpContext.Token();
           // validacaoIdentidade.Validar();

           // if (validacaoIdentidade.Identidade == null)
           // {
           //     if (postRequest)
           //     {
           //         filterContext.Result = new HttpStatusCodeResult(401);
           //         return;
           //     }
           //     else
           //     {
           //         filterContext.Result = this.RedirectTo("/Login");
           //         return;
           //     }
           // }

           //filterContext.HttpContext.Identidade(validacaoIdentidade.Identidade);
        }
    }
}

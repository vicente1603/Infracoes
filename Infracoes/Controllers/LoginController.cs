using Infracoes.Models;
using System;
using System.Collections.Generic;
using Infracoes.Extensions;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Infracoes.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autorizar(Infracoes.Models.Usuario usuarioModel)
        {
            using (InfracoesEntities db = new InfracoesEntities())
            {
                var detalhesUsuario = db.Usuarios.Where(x => x.NomeUsuario == usuarioModel.NomeUsuario && x.Senha == usuarioModel.Senha).FirstOrDefault();
                if (detalhesUsuario == null)
                {
                    //usuarioModel.MessagemErroLogin = "Usuário ou senha incorreta.";
                    return View("Index", usuarioModel);
                }
                else
                {
                    Session["idUsuario"] = detalhesUsuario.IdUsuario;
                    Session["nomeUsuario"] = detalhesUsuario.NomeUsuario;
                    return RedirectToAction("Index", "BoasVindas");
                }

            }
        }

        public ActionResult Deslogar()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

	}
}
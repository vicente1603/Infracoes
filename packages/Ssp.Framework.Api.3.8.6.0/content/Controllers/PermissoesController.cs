using Ssp.Framework.Api.Attributes;
using Ssp.Framework.Api.ViewModels.Permissoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

// Código gerado automaticamente pelo SSP Framework.

namespace Ssp.Framework.Api.Controllers
{
    public class PermissoesController : Controller
    {
        [HttpPost]
        public ActionResult Cadastradas(PermissoesCadastradasViewModel viewModel)
        {
            List<string> permissoes = new List<string>();

            IEnumerable<MethodInfo> metodosController = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && typeof(Controller).IsAssignableFrom(t))
                .SelectMany(c => c.GetMethods());

            permissoes.AddRange(metodosController
                .Where(m => m.GetCustomAttributes(typeof(IAutorizacaoAttribute), false).Any())
                .SelectMany(m => (IAutorizacaoAttribute[])m.GetCustomAttributes(typeof(IAutorizacaoAttribute), false))
                .Where(a => !string.IsNullOrWhiteSpace(a.Permissao))
                .Select(a => a.Permissao)
                .Distinct());

            string permissao = viewModel.Permissao ?? string.Empty;

            return Json(new
            {
                cadastradas = permissoes
                    .Where(p => p.ToLower().Contains(permissao.ToLower()))
                    .Skip(viewModel.Paginacao.Inicio)
                    .Take(viewModel.Paginacao.Limite)
            });
        }
    }
}

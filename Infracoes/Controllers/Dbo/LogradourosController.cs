using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infracoes.Models.DataModel;
using Infracoes.Models.DomainModel.Dbo;
using Infracoes.Models.ViewModel.Dbo.Logradouros;
using Infracoes.Models.DataModel.Dbo.Queries;
using Ssp.Framework.Api.Extensions;

namespace Infracoes.Controllers.Dbo
{
    public class LogradourosController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Logradouros/ConsultarLogradouros.cshtml");
        }

        [HttpPost]
        public ActionResult Atualizar(AtualizarLogradouroViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelErrors();
            }

            using (DbApplication db = new DbApplication())
            {
                Logradouro logradouroBanco = db
                    .Logradouros
                    .ComCep(viewModel.Cep)
                    .SingleOrDefault();

                if (logradouroBanco != null && logradouroBanco.IdLogradouro != viewModel.idLogradouro)
                {
                    return this.ErrorMessage("Já existe um agente com esse id");
                }

                Logradouro logradouro = db
                    .Logradouros
                    .ComId(viewModel.idLogradouro)
                    .SingleOrDefault();

                logradouro.Rua = viewModel.Rua;
                logradouro.Cidade = viewModel.Cidade;
                logradouro.Bairro = viewModel.Bairro;
                logradouro.Estado = viewModel.Estado;
                logradouro.Cep = viewModel.Cep;
                logradouro.VelocidadeMax = viewModel.VelocidadeMax;

                db.RegistrarAlterado(logradouro);
                db.Salvar();
            }

            return this.Message("Logradouro atualizado com sucesso.");
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastrarLogradouroViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelErrors();
            }

            using (DbApplication db = new DbApplication())
            {
                Logradouro logradouroBanco = db
                    .Logradouros
                    .ComCep(viewModel.Cep)
                    .SingleOrDefault();

                if(logradouroBanco != null)
                {
                    return this.ErrorMessage("Já existe um logradouro com esse cep");
                }

                Logradouro logradouro = new Logradouro();

                logradouro.Cidade = viewModel.Cidade;
                logradouro.Bairro = viewModel.Bairro;
                logradouro.Estado = viewModel.Estado;
                logradouro.Rua = viewModel.Rua;
                logradouro.Cep = viewModel.Cep;
                logradouro.VelocidadeMax = viewModel.VelocidadeMax;

                db.RegistrarNovo(logradouro);
                db.Salvar();

                return this.Message("Logradouro cadastrado com sucesso.");
            }
        }

        [HttpPost]
        public ActionResult Cadastrados(LogradourosCadastradosViewModel viewModel)
        {
            using (DbApplication db = new DbApplication())
            {
                IQueryable<Logradouro> logradourosQuery = db
                    .Logradouros
                    .OndeCepContem(viewModel.Cep)
                    .OrderBy(l => l.Estado);

                ICollection<Logradouro> logradouros = logradourosQuery
                    .Skip(viewModel.Paginacao.Inicio)
                    .Take(viewModel.Paginacao.Limite)
                    .ToList();

                viewModel.Paginacao.TotalRegistros = logradourosQuery.Count();

                List<dynamic> logradourosJson = new List<dynamic>();

                foreach (Logradouro logradouro in logradouros)
                {
                    logradourosJson.Add(new
                    {
                        idLogradouro = logradouro.IdLogradouro,
                        rua = logradouro.Rua,
                        bairro = logradouro.Bairro,
                        cidade = logradouro.Cidade,
                        estado = logradouro.Estado,
                        cep = logradouro.Cep,
                        velocidadeMax = logradouro.VelocidadeMax
                    });
                }

                return Json(new
                {
                    logradouros = logradourosJson,
                    paginacao = viewModel.Paginacao.Json()
                });
            }
        }

        [HttpPost]
        public ActionResult Remover(IdLogradouroViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelErrors();
            }

            using (DbApplication db = new DbApplication()){
                Logradouro logradouro = db
                    .Logradouros
                    .ComId(viewModel.IdLogradouro)
                    .SingleOrDefault();

                if (logradouro == null){
                    return this.ErrorMessage("Logradouro não encontrado");
                }

                db.RegistrarRemovido(logradouro);
                db.Salvar();

                return this.Message("Logradouro removido com sucesso");
            }
        }

        public ActionResult RelatorioLogradouros(LogradourosCadastradosRelatorioViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return this.ModelErrors();

            RelatorioLogradourosViewModel relatorioLogradouros = new RelatorioLogradourosViewModel();

            using (DbApplication db = new DbApplication())
            {
                IQueryable<Logradouro> logradourosQuery = db
                    .Logradouros
                    .OndeCepContem(viewModel.Cep)
                    .OrderBy(a => a.IdLogradouro);

                ICollection<Logradouro> logradouros = logradourosQuery
                    .ToList();

                relatorioLogradouros.TotalLogradouros = logradouros.Count;

                relatorioLogradouros.Logradouros = new List<RelatorioLogradourosViewModel.Logradouro>();

                foreach (Logradouro logradouro in logradouros)
                {
                    relatorioLogradouros.Logradouros.Add(new RelatorioLogradourosViewModel.Logradouro
                    {
                        Rua = logradouro.Rua,
                        Bairro = logradouro.Bairro,
                        Cidade = logradouro.Cidade,
                        Estado = logradouro.Estado,
                        Cep = logradouro.Cep,
                        VelocidadeMax = logradouro.VelocidadeMax
                    });
                }
                return this.PdfView("~/Relatorios/Logradouros/RelatorioLogradouros.cshtml", relatorioLogradouros);
            }
        }

        [HttpPost]
        public ActionResult Todos()
        {
            using (DbApplication db = new DbApplication()){
                ICollection<Logradouro> logradouros = db
                    .Logradouros
                    .OrderBy(l => l.Estado)
                    .ToList();

                List<dynamic> logradourosJson = new List<dynamic>();

                foreach (Logradouro logradouro in logradouros)
                {
                    logradourosJson.Add(new
                    {
                        idLogradouro = logradouro.IdLogradouro,
                        cep = logradouro.Cep,
                    });
                }
                return Json(new
                {
                    logradouros = logradourosJson
                });
            }
        }
    }
}
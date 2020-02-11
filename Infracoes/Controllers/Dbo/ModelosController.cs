using Infracoes.Models.ViewModel.Dbo.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ssp.Framework.Api.Extensions;
using Infracoes.Models.DataModel;
using Infracoes.Models.DataModel.Dbo.Queries;
using Infracoes.Models.DomainModel.Dbo;
using System.Data.Entity;
using Infracoes.Models.ViewModel.Dbo.Modelos;


namespace Infracoes.Controllers.Dbo
{
    public class ModelosController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Modelos/ConsultarModelos.cshtml");
        }

        [HttpPost]
        public ActionResult Atualizar(AtualizarModeloViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelErrors();
            }

            using (DbApplication db = new DbApplication())
            {
                Modelo modeloBanco = db.Modelos
                    .ComDescricao(viewModel.Descricao)
                    .SingleOrDefault();

                if (modeloBanco != null && modeloBanco.IdModelo != viewModel.IdModelo){
                    return this.ErrorMessage("Já existe um modelo com essa descrição");
                }

                Modelo modelo = db.Modelos
                    .ComId(viewModel.IdModelo)
                    .SingleOrDefault();

                modelo.Descricao = viewModel.Descricao;

                db.RegistrarAlterado(modelo);
                db.Salvar();                   
            }

            return this.Message("Modelo atualizado com sucesso.");
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastrarModeloViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelErrors();
            }

            using (DbApplication db = new DbApplication())
            {
                Modelo modeloBanco = db.Modelos
                    .ComDescricao(viewModel.Descricao)
                    .SingleOrDefault();

                if (modeloBanco != null)
                {
                    return this.ErrorMessage("Já existe um modelo cadastrado com essa descrição.");
                }

                Modelo modelo = new Modelo();
                modelo.Descricao = viewModel.Descricao;

                db.RegistrarNovo(modelo);
                db.Salvar();
            }

            return this.Message("Modelo cadastrado com sucesso.");
        }

        [HttpPost]
        public ActionResult Cadastrados(ModelosCadastradosViewModel viewModel)
        {
            using (DbApplication db = new DbApplication())
            {
                IQueryable<Modelo> modelosQuery = db.Modelos
                    .OndeDescricaoContem(viewModel.Descricao)
                    .OrderBy(d => d.Descricao);
                ICollection<Modelo> modelos = modelosQuery
                    .Skip(viewModel.Paginacao.Inicio)
                    .Take(viewModel.Paginacao.Limite)
                    .ToList();

                viewModel.Paginacao.TotalRegistros = modelosQuery.Count();

                List<dynamic> modelosJson = new List<dynamic>();

                foreach(Modelo modelo in modelos){
                    modelosJson.Add(new
                    {
                        idModelo = modelo.IdModelo,
                        descricao = modelo.Descricao,
                    });
                }

                return Json(new{
                    modelos = modelosJson,
                    paginacao = viewModel.Paginacao.Json()
                });
                    
            }
        }

        public ActionResult Remover(IdModeloViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelErrors();
            }

            using (DbApplication db = new DbApplication())
            {
                Modelo modelo = db
                    .Modelos
                    .Include(m => m.Veiculos)
                    .ComId(viewModel.IdModelo)
                    .SingleOrDefault();

                if (modelo == null)
                    return this.ErrorMessage("Modelo não encontrado.");
                if (modelo.Veiculos.Count > 0)
                    return this.ErrorMessage("O modelo está associado a um veículo");

                db.RegistrarRemovido(modelo);
                db.Salvar();

                return this.Message("Modelo removido com sucesso.");
            }
        }

        public ActionResult RelatorioModelos(ModelosCadastradosRelatorioViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return this.ModelErrors();

            RelatorioModelosViewModel relatorioModelos = new RelatorioModelosViewModel();

            using (DbApplication db = new DbApplication())
            {
                IQueryable<Modelo> modelosQuery = db
                    .Modelos
                    .OndeDescricaoContem(viewModel.Descricao)
                    .OrderBy(a => a.IdModelo);

                ICollection<Modelo> modelos = modelosQuery
                    .ToList();

                relatorioModelos.TotalModelos = modelos.Count;

                relatorioModelos.Modelos = new List<RelatorioModelosViewModel.Modelo>();

                foreach (Modelo modelo in modelos)
                {
                    relatorioModelos.Modelos.Add(new RelatorioModelosViewModel.Modelo
                    {
                        Descricao = modelo.Descricao
                    });
                }
                return this.PdfView("~/Relatorios/Modelos/RelatorioModelos.cshtml", relatorioModelos);
            }
        }

        public ActionResult Todos()
        {
            using (DbApplication db = new DbApplication())
            {
                ICollection<Modelo> modelos = db.Modelos
                    .OrderBy(d => d.Descricao)
                    .ToList();

                List<dynamic> modelosJson = new List<dynamic>();

                foreach (Modelo modelo in modelos)
                {
                    modelosJson.Add(new
                    {
                        idModelo = modelo.IdModelo,
                        descricao = modelo.Descricao,
                    });
                }
                return Json(new
                {
                    modelos = modelosJson
                });
            }
        }
    }
}
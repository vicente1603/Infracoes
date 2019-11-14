using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infracoes.Extensions;
using Infracoes.Models.DataModel;
using Infracoes.Models.DomainModel.Dbo;
using Infracoes.Models.ViewModel.Dbo.Agentes;
using Infracoes.Models.DataModel.Dbo.Queries;

namespace Infracoes.Controllers.Dbo
{
    public class AgentesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Agentes/ConsultarAgentes.cshtml");
        }

        [HttpPost]
        public ActionResult Atualizar(AtualizarViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelErrors();
            }

            using (DbApplication db = new DbApplication()){
                Agente agenteBanco = db
                    .Agentes
                    .ComMatricula(viewModel.Matricula)
                    .SingleOrDefault();

                if (agenteBanco != null && agenteBanco.IdAgente != viewModel.IdAgente)
                {
                    return this.ErrorMessage("Já existe um agente com essa matrícula");
                }

                Agente agente = db
                    .Agentes
                    .ComId(viewModel.IdAgente)
                    .SingleOrDefault();

                agente.NomeAgente = viewModel.NomeAgente;
                agente.TempoServico = viewModel.TempoServico;
                agente.Efetivacao = viewModel.Efetivacao;

                db.RegistrarAlterado(agente);
                db.Salvar();                    
            }
            return this.Message("Agente atualizado com sucesso");
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastrarVeiculoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelErrors();
            }

            using (DbApplication db = new DbApplication()){
                Agente agenteBanco = db
                    .Agentes
                    .ComMatricula(viewModel.Matricula)
                    .SingleOrDefault();

                if(agenteBanco != null)
                {
                    return this.ErrorMessage("Já existe um agente com essa matrícula");
                }

                Agente agente = new Agente();

                agente.Matricula = viewModel.Matricula;
                agente.NomeAgente = viewModel.NomeAgente;
                agente.Efetivacao = viewModel.Efetivacao;
                agente.TempoServico = viewModel.TempoServico;

                db.RegistrarNovo(agente);
                db.Salvar();

                return this.Message("Agente cadastrado com sucesso.");
            }
        }

        [HttpPost]
        public ActionResult Cadastrados(AgentesCadastradosViewModel viewModel)
        {
            using (DbApplication db = new DbApplication())
            {
                IQueryable<Agente> agentesQuery = db
                    .Agentes
                    .OndeMatriculaContem(viewModel.Matricula)
                    .OrderBy(a => a.NomeAgente);

                ICollection<Agente> agentes = agentesQuery
                    .Skip(viewModel.Paginacao.Inicio)
                    .Take(viewModel.Paginacao.Limite)
                    .ToList();

                viewModel.Paginacao.TotalRegistros = agentesQuery.Count();
                List<dynamic> agentesJson = new List<dynamic>();

                foreach (Agente agente in agentes)
                {
                    agentesJson.Add(new
                    {
                        idAgente = agente.IdAgente,
                        nomeAgente = agente.NomeAgente,
                        efetivacao = agente.Efetivacao.ToString("dd/MM/yyyy"),
                        tempoServico = agente.TempoServico,
                        matricula = agente.Matricula
                    });
                }

                return Json(new
                {
                    agentes = agentesJson,
                    paginacao = viewModel.Paginacao.Json()
                });
            }
        }

        [HttpPost]
        public ActionResult Remover(IdAgenteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelErrors();
            }

            using (DbApplication db = new DbApplication())
            {
                Agente agente = db
                    .Agentes
                    .ComId(viewModel.IdAgente)
                    .SingleOrDefault();

                if (agente == null)
                {
                    return this.ErrorMessage("Agente não encontrado.");
                }

                db.RegistrarRemovido(agente);
                db.Salvar();

                return this.Message("Agente removido com sucesso.");
            }
        }

        [HttpPost]
        public ActionResult Todos()
        {
            using (DbApplication db = new DbApplication())
            {
                ICollection<Agente> agentes = db
                    .Agentes
                    .OrderBy(a => a.NomeAgente)
                    .ToList();

                List<dynamic> agentesJson = new List<dynamic>();

                foreach (Agente agente in agentes)
                {
                    agentesJson.Add(new
                    {
                        idAgente = agente.IdAgente,
                        matricula = agente.Matricula,
                        nomeAgente = agente.NomeAgente
                    });
                }
                return Json(new
                {
                    agentes = agentesJson
                });
            }
        }
    }
}
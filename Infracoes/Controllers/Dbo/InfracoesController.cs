using Infracoes.Models.DomainModel.Dbo;
using Infracoes.Models.ViewModel.Dbo.Infracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infracoes.Extensions;
using Infracoes.Models.DataModel;
using Infracoes.Models.DataModel.Dbo.Queries;
using System.Data.Entity;


namespace Infracoes.Controllers.Dbo
{
    public class InfracoesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Infracoes/ConsultarInfracoes.cshtml");
        }

        [HttpPost]
        public ActionResult Atualizar(AtualizarInfracaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelErrors();
            }

            using (DbApplication db = new DbApplication())
            {
                Infracao infracaoBanco = db
                    .Infracoes
                    .ComDescricao(viewModel.Descricao)
                    .SingleOrDefault();

                if (infracaoBanco != null && infracaoBanco.IdInfracao != viewModel.IdInfracao)
                {
                    return this.ErrorMessage("Já existe uma infração cadastrada com esse id.");
                }

                Infracao infracao = db
                    .Infracoes
                    .ComId(viewModel.IdInfracao)
                    .SingleOrDefault();

                infracao.Velocidade = viewModel.Velocidade;
                infracao.IdVeiculo = viewModel.IdVeiculo;
                infracao.IdLogradouro = viewModel.IdLogradouro;
                infracao.IdAgente = viewModel.IdAgente;
                infracao.Descricao = viewModel.Descricao;

                db.RegistrarAlterado(infracao);
                db.Salvar();
            }

            return this.Message("Infração atualizada com sucesso");
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastrarInfracaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelErrors();
            }

            using (DbApplication db = new DbApplication())
            {
                Infracao infracaoBanco = db
                    .Infracoes
                    .ComDescricao(viewModel.Descricao)
                    .SingleOrDefault();

                if (infracaoBanco != null)
                {
                    return this.ErrorMessage("Já existe uma infração cadastrada com esse id.");
                }

                Infracao infracao = new Infracao();

                infracao.Descricao = viewModel.Descricao;
                infracao.Velocidade = viewModel.Velocidade;
                infracao.IdVeiculo = viewModel.IdVeiculo;
                infracao.IdLogradouro = viewModel.IdLogradouro;
                infracao.IdAgente = viewModel.IdAgente;

                db.RegistrarNovo(infracao);
                db.Salvar();

                return this.Message("Infração registrada com sucesso.");
            }
        }

        [HttpPost]
        public ActionResult Cadastradas(ProdutosCadastradosViewModel viewModel)
        {
            using (DbApplication db = new DbApplication())
            {
                IQueryable<Infracao> infracoesQuery = db
                    .Infracoes
                    .OndeDescricaoContem(viewModel.Descricao)
                    .Include(i => i.Agente)
                    .Include(i => i.Logradouro)
                    .Include(i => i.Veiculo)
                    .OrderBy(i => i.Descricao);
                ICollection<Infracao> infracoes = infracoesQuery
                    .Skip(viewModel.Paginacao.Inicio)
                    .Take(viewModel.Paginacao.Limite)
                    .ToList();

                viewModel.Paginacao.TotalRegistros = infracoesQuery.Count();

                List<dynamic> infracoesJson = new List<dynamic>();

                foreach (Infracao infracao in infracoes)
                {
                   
                    infracoesJson.Add(new
                    {
                        nomeAgente = infracao.Agente.NomeAgente,
                        cepLogradouro = infracao.Logradouro.Cep,
                        idInfracao = infracao.IdInfracao,
                        velocidade = infracao.Velocidade,
                        descricao = infracao.Descricao,
                        placaVeiculo = infracao.Veiculo.Placa
                    });
                }

                return Json(new
                {
                    infracoes = infracoesJson,
                    paginacao = viewModel.Paginacao.Json()
                });
                    
            }
        }

        [HttpPost]
        public ActionResult Remover(IdInfracaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelErrors();
            }

            using (DbApplication db = new DbApplication())
            {
                Infracao infracao = db
                    .Infracoes
                    .ComId(viewModel.idInfracao)
                    .SingleOrDefault();
                if (infracao == null)
                {
                    return this.ErrorMessage("Infração não encontrada.");
                }

                db.RegistrarRemovido(infracao);
                db.Salvar();

                return this.Message("Infração removida com sucesso.");
            }
        }

        [HttpPost]
        public ActionResult Todas()
        {
            using (DbApplication db = new DbApplication())
            {
                ICollection<Infracao> infracoes = db
                    .Infracoes
                    .OrderBy(n => n.Descricao)
                    .ToList();

                List<dynamic> infracoesJson = new List<dynamic>();
                foreach (Infracao infracao in infracoes)
                {
                    infracoesJson.Add(new
                    {
                        idInfracao = infracao.IdInfracao,
                        descricao = infracao.Descricao,
                    });
                }
                return Json(new
                {
                    infracoes = infracoesJson
                });
            }
        }
    }
}
using Infracoes.Models.DataModel;
using Infracoes.Models.DomainModel.Dbo;
using Infracoes.Models.ViewModel.Veiculos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infracoes.Models.DataModel.Dbo.Queries;
using Infracoes.Extensions;
using Infracoes.Models.ViewModel.Dbo.Veiculos;
using Infracoes.Models.ViewModel.Dbo.Modelo;
using System.Data.Entity;



namespace Infracoes.Controllers.Dbo
{
    public class VeiculosController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Veiculos/ConsultarVeiculos.cshtml");
        }

        [HttpPost]
        public ActionResult Atualizar(AtualizarVeiculoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelErrors();
            }

            using (DbApplication db = new DbApplication())
            {
                Veiculo veiculoBanco = db.Veiculos
                    .ComPlaca(viewModel.Placa)
                    .SingleOrDefault();
                if (veiculoBanco != null && veiculoBanco.IdVeiculo != viewModel.IdVeiculo)
                {
                    return this.ErrorMessage("Já existe um veiculo com essa placa");
                }

                Veiculo veiculo = db.Veiculos
                    .ComId(viewModel.IdVeiculo)
                    .SingleOrDefault();

                veiculo.Placa = viewModel.Placa;
                veiculo.Uf = viewModel.Uf;
                veiculo.IdInfracao = viewModel.IdInfracao;
                veiculo.IdModelo = viewModel.IdModelo;
                veiculo.IdProprietario = viewModel.IdProprietario;

                db.RegistrarAlterado(veiculo);
                db.Salvar();
            }
            return this.Message("Veiculo atualizado com sucesso.");
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastrarVeiculoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.ModelErrors();
            }
            using (DbApplication db = new DbApplication())
            {
                Veiculo veiculoBanco = db.Veiculos
                    .ComPlaca(viewModel.Placa)
                    .SingleOrDefault();
                if (veiculoBanco != null)
                {
                    return this.ErrorMessage("Já existe veiculo com essa placa.");
                }

                Veiculo veiculo = new Veiculo();

                veiculo.Placa = viewModel.Placa;
                veiculo.Uf = viewModel.Uf;
                veiculo.IdInfracao = viewModel.IdInfracao;
                veiculo.IdModelo = viewModel.IdModelo;
                veiculo.IdProprietario = viewModel.IdProprietario;

                db.RegistrarNovo(veiculo);
                db.Salvar();

                return this.Message("Veiculo cadastrado com sucesso.");
            }
        }

        [HttpPost]
        public ActionResult Cadastrados(VeiculosCadastradosViewModel viewModel)
        {
            using (DbApplication db = new DbApplication())
            {
                IQueryable<Veiculo> veiculosQuery = db.Veiculos
                    .OndePlacaContem(viewModel.Placa)
                    .Include(v => v.Modelo)
                    .Include(v => v.Infracoes)
                    .Include(v => v.Proprietario)
                    .OrderBy(v => v.Placa);


                ICollection<Veiculo> veiculos = veiculosQuery
                    .Skip(viewModel.Paginacao.Inicio)
                    .Take(viewModel.Paginacao.Limite)
                    .ToList();

                viewModel.Paginacao.TotalRegistros = veiculosQuery.Count();
                List<dynamic> veiculosJson = new List<dynamic>();
                foreach (Veiculo veiculo in veiculos)
                {
                    List<dynamic> infracoesJson = new List<dynamic>();

                    foreach(Infracao infracao in veiculo.Infracoes){

                        infracoesJson.Add(new
                        {
                            idInfracao = infracao.IdInfracao,
                            descricaoInfracao = infracao.Descricao,
                            velocidadeInfracao = infracao.Velocidade,
                            localInfracao = infracao.IdLogradouro,
                            agenteInfracao = infracao.IdAgente,
                            veiculoInfracao = infracao.Veiculo.Placa
                        });

                    }
                    veiculosJson.Add(new
                    {
                        descricaoModelo = veiculo.Modelo.Descricao,
                        cpfProprietario = veiculo.Proprietario.CpfProprietario,
                        placa = veiculo.Placa,
                        uf = veiculo.Uf,
                        idVeiculo = veiculo.IdVeiculo,   
                        infracoesVeiculos = infracoesJson

                        //proprietario = new
                        //{
                        //    nome = veiculo.Proprietario.NomeProprietario,
                        //    cpf = veiculo.Proprietario.CpfProprietario
                        //}
                    });
                }

                return Json(new
                {
                    veiculos = veiculosJson,
                    paginacao = viewModel.Paginacao.Json()
                });
            }
        }


        [HttpPost]
        public ActionResult Remover(IdVeiculoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                this.ModelErrors();
            }
            using (DbApplication db = new DbApplication())
            {
                Veiculo veiculo = db
                    .Veiculos
                    .ComId(viewModel.IdVeiculo)
                    .SingleOrDefault();

                if (veiculo == null)
                    return this.ErrorMessage("Veiculo não encontrado.");
                db.RegistrarRemovido(veiculo);
                db.Salvar();

                return this.Message("Veiculo removido com sucesso");
            }
        }

        [HttpPost]
        public ActionResult Todos()
        {
            using (DbApplication db = new DbApplication())
            {
                ICollection<Veiculo> veiculos = db.Veiculos
                    .OrderBy(v => v.Placa)
                    .ToList();

                List<dynamic> veiculosJson = new List<dynamic>();
                foreach (Veiculo veiculo in veiculos)
                {
                    veiculosJson.Add(new
                    {
                        idVeiculo = veiculo.IdVeiculo,
                        placa = veiculo.Placa,
                    });
                }
                return Json(new
                {
                    veiculos = veiculosJson
                });
            }
        }
    }
}
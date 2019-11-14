using Infracoes.Models.ViewModel.Dbo.Proprietarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infracoes.Extensions;
using Infracoes.Models.DataModel;
using Infracoes.Models.DomainModel.Dbo;
using Infracoes.Models.DataModel.Dbo.Queries;
using System.Collections;
using Infracoes.Models.ViewModel.Dbo.Veiculos;
using System.Data.Entity;



namespace Infracoes.Controllers.Dbo
{
    public class ProprietariosController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Proprietarios/ConsultarProprietarios.cshtml");
        }

        [HttpPost]
        public ActionResult Atualizar(AtualizarProprietarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return this.ModelErrors();

                using (DbApplication db = new DbApplication())
                {
                    Proprietario proprietarioBanco = db
                        .Proprietarios
                        .ComCpf(viewModel.CpfProprietario)
                        .SingleOrDefault();
                    if (proprietarioBanco != null && proprietarioBanco.IdProprietario != viewModel.IdProprietario)
                    {
                        return this.ErrorMessage("Já existe um Proprietário com esse cpf");
                    }
                    Proprietario proprietario = db.Proprietarios
                        .ComId(viewModel.IdProprietario)
                        .SingleOrDefault();

                    proprietario.IdProprietario = viewModel.IdProprietario;
                    proprietario.NomeProprietario = viewModel.NomeProprietario;
                    proprietario.CpfProprietario = viewModel.CpfProprietario;
                    proprietario.DataNascimento = viewModel.DataNascimento;
                    proprietario.Telefone = viewModel.Telefone;

                    db.RegistrarAlterado(proprietario);
                    db.Salvar();
                }

                return this.Message("Proprietário atualizado com sucesso.");
            }

        [HttpPost]
        public ActionResult Cadastrar(CadastrarProprietarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return this.ModelErrors();
            using (DbApplication db = new DbApplication())
            {
                Proprietario proprietarioBanco = db.Proprietarios
                    .ComCpf(viewModel.CpfProprietario)
                    .SingleOrDefault();

                if (proprietarioBanco != null)
                {
                    return this.ErrorMessage("Já existe um proprietário cadastrado com esse cpf.");
                }
                Proprietario proprietario = new Proprietario();

                proprietario.NomeProprietario = viewModel.NomeProprietario;
                proprietario.CpfProprietario = viewModel.CpfProprietario;
                proprietario.DataNascimento = viewModel.DataNascimento;
                proprietario.Telefone = viewModel.Telefone;

                db.RegistrarNovo(proprietario);
                db.Salvar();

                return this.Message("Proprietário Cadastrado com sucesso.");
              }
          }

        [HttpPost]
        public ActionResult Cadastrados(ProprietariosCadastradosViewModel viewModel)
        {
            using (DbApplication db = new DbApplication())
            {
                IQueryable<Proprietario> proprietariosQuery = db.Proprietarios
                    .ContemCpf(viewModel.CpfProprietario)
                    .Include(p => p.Veiculos)
                    .OrderBy(p => p.NomeProprietario);

                ICollection<Proprietario> proprietarios = proprietariosQuery
                    .Skip(viewModel.Paginacao.Inicio)
                    .Take(viewModel.Paginacao.Limite)
                    .ToList();

                viewModel.Paginacao.TotalRegistros = proprietariosQuery.Count();
                List<dynamic> proprietariosJson = new List<dynamic>();

                foreach (Proprietario proprietario in proprietarios)
                {
                    List<dynamic> veiculosJson = new List<dynamic>();

                    foreach(Veiculo veiculo in proprietario.Veiculos)
                    {
                        veiculosJson.Add(new
                        {
                            idVeiculo = veiculo.IdVeiculo,
                            placa = veiculo.Placa,
                            uf = veiculo.Uf

                        });
                    }
                    proprietariosJson.Add(new
                    {
                        idProprietario = proprietario.IdProprietario,
                        nomeProprietario = proprietario.NomeProprietario,
                        cpfProprietario = proprietario.CpfProprietario,
                        dataNascimento = proprietario.DataNascimento.ToString("dd/MM/yyyy"),
                        telefone = proprietario.Telefone,
                        veiculos = veiculosJson
                    });
                }

                return Json(new
                {
                    proprietarios = proprietariosJson,
                    paginacao = viewModel.Paginacao.Json()
                });
            }
        }

        [HttpPost]
        public ActionResult Remover(IdProprietarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                this.ModelErrors();
            }

            using (DbApplication db = new DbApplication())
            {
                Proprietario proprietario = db
                    .Proprietarios
                    .Include(p => p.Veiculos)
                    .ComId(viewModel.IdProprietario)
                    .SingleOrDefault();

                if (proprietario == null)
                    return this.ErrorMessage("Proprietário não encontrado");
                if (proprietario.Veiculos.Count > 0)
                    return this.ErrorMessage("O proprietário está associado a um veículo");

                db.RegistrarRemovido(proprietario);
                db.Salvar();

                return this.Message("Proprietário removido com sucesso.");
            }
        }

        [HttpPost]
        public ActionResult Todos()
        {
            using (DbApplication db = new DbApplication())
            {
                ICollection<Proprietario> proprietarios = db.Proprietarios
                    .OrderBy(p => p.NomeProprietario)
                    .ToList();

                List<dynamic> proprietariosJson = new List<dynamic>();

                foreach (Proprietario proprietario in proprietarios)
                {
                    proprietariosJson.Add(new
                    {
                        idProprietario = proprietario.IdProprietario,
                        nomeProprietario = proprietario.NomeProprietario,
                        cpfProprietario = proprietario.CpfProprietario
                    });
                }

                return Json(new
                {
                    proprietarios = proprietariosJson
                });
            }
        }

        }
    }
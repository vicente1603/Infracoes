(function () {
    angular.module("app").controller("VeiculosController", [
        "Veiculo",
        "Proprietario",
        "Modelo",
        "Infracao",
        VeiculosController
    ]);

    function VeiculosController(Veiculo, Proprietario, Modelo, Infracao) {
        var _self = this;

        _self.filtro = {
            dados: {},
            limpar: function () {
                _self.filtro.dados = {};
                _self.consultar();
            }
        }

        _self.consultar = function (pagina1) {
            if (pagina1) {
                _self.filtro.paginacao.pagina = 1;
            }

            Veiculo
                .cadastrados()
                .ondePlacaContem(_self.filtro.dados.placa)
                .listar(_self.filtro.paginacao)
                .then(function (response) {
                    _self.cadastrados = response.data.veiculos;
                    _self.filtro.paginacao = response.data.paginacao;
                })
                .catch(function (response) {
                    _self.erros = response.data;
                })
            ["finally"](function () {

            })
        };

        _self.limparMensagens = function () {
            _self.erros = null;
            _self.info = null;
        }

        _self.modal = {
            erros: {},
            remover: function () {
                _self.modal.desativando = true;
                _self.modal.erros = {};
                _self.limparMensagens();

                Veiculo
                    .remover(_self.veiculo)
                    .then(function (response) {
                        _self.info = response.data;
                        _self.modal.confirmarRemover.visivel = false;
                        _self.consultar(true);
                    })
                    .catch(function (response) {
                        _self.modal.erros.aoRemover = response.data;
                    })
                    ["finally"](function () {
                        _self.modal.desativando = false;
                    });
            },
            limparMensagens: function () {
                _self.modal.info = {};
                _self.modal.erros = {};
            },
            salvar: function () {
                _self.modal.salvando = true;
                _self.modal.erros = {};
                _self.limparMensagens();

                Veiculo
                    .salvar(_self.veiculo)
                    .success(function (mensagem) {
                        _self.info = mensagem;
                        _self.modal.dados.visivel = false;
                        _self.consultar(true);
                    })
                .error(function (erros) {
                    _self.modal.erros.aoSalvar = erros;
                })
                ["finally"](function () {
                    _self.modal.salvando = false;
                });
            }

        }

        _self.consultarProprietarios = function () {

            Proprietario
            .todos()
            .then(function (response) {
                _self.proprietarios = response.data.proprietarios;
            })
            .catch(function (response) {
                _self.erros = response.data;
            })
            ["finally"](function () {

            })
        }

        _self.consultarModelos = function () {

            Modelo
            .todos()
            .then(function (response) {
                _self.modelos = response.data.modelos;
            })
            .catch(function (response) {
                _self.erros = response.data;
            })
            ["finally"](function () {

            })
        }

        _self.consultarInfracoes = function () {

            Infracao
            .todas()
            .then(function (response) {
                _self.infracoes = response.data.infracoes;
            })
            .catch(function (response) {
                _self.erros = response.data;
            })
            ["finally"](function () {

            })
        }

        _self.novo = function () {
            _self.veiculo = {

            };
            _self.modal.info = {};
            _self.modal.erros = {};
        }

        _self.selecionar = function (veiculo) {
            _self.modal.limparMensagens();
            _self.veiculo = angular.copy(veiculo);
        }
    }

})();
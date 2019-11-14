(function () {
    angular.module("app").controller("InfracoesController", [
        "Infracao",
        "Agente",
        "Logradouro",
        "Veiculo",
        InfracoesController
    ]);

    function InfracoesController(Infracao, Agente, Logradouro, Veiculo) {

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

            Infracao
                .cadastradas()
                .ondeDescricaoContem(_self.filtro.dados.descricao)
                .listar(_self.filtro.paginacao)
                .then(function (response) {
                    _self.cadastradas = response.data.infracoes;
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

                Infracao
                    .remover(_self.infracao)
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

                Infracao
                    .salvar(_self.infracao)
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

        _self.consultarAgentes = function () {
            Agente
            .todos()
            .then(function (response) {
                _self.agentes = response.data.agentes;
            })
            .catch(function (response) {
                _self.erros = response.data;
            })
            ["finally"](function () {

            })
        }

        _self.consultarLogradouros = function () {
            Logradouro
            .todos()
            .then(function (response) {
                _self.logradouros = response.data.logradouros;
            })
            .catch(function (response) {
                _self.erros = response.data;
            })
            ["finally"](function () {

            })
        }

        _self.consultarVeiculos = function () {
            Veiculo
            .todos()
            .then(function (response) {
                _self.veiculos = response.data.veiculos;
            })
            .catch(function (response) {
                _self.erros = response.data;
            })
            ["finally"](function () {

            })
        }

        _self.nova = function () {
            _self.infracao = {

            };
            _self.modal.info = {};
            _self.modal.erros = {};
        }

        _self.selecionar = function (infracao) {
            _self.modal.limparMensagens();
            _self.infracao = angular.copy(infracao);
        }

    }

})();
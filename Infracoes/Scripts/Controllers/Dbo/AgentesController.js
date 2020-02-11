(function () {
    angular.module("app").controller("AgentesController", [
        "Agente",
        "Buffer",
        "$scope",
        AgentesController
    ]);

    function AgentesController(Agente, Buffer, $scope) {
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

            Agente
                .cadastrados()
                .ondeMatriculaContem(_self.filtro.dados.matricula)
                .listar(_self.filtro.paginacao)
                .then(function (response) {
                    _self.cadastrados = response.data.agentes;
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

                Agente
                    .remover(_self.agente)
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

                Agente
                    .salvar(_self.agente)
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

        _self.novo = function () {
            _self.agente = {

            };
            _self.modal.info = {};
            _self.modal.erros = {};
        }

        _self.selecionar = function (agente) {
            _self.modal.limparMensagens();
            _self.agente = angular.copy(agente);
        }

        _self.gerarRelatorio = function () {

            console.log("gerar");

            Agente
                .cadastrados()
                .ondeMatriculaContem(_self.filtro.dados.matricula)
                .relatorioAgentes()
                .success(angular.bind(this, function (pdf) {
                    Buffer.saveToFile(pdf, "application/pdf", "RelatorioAgentes");
                }));
        };
    }

})();
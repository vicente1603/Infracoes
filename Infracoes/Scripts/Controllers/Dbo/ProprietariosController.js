(function () {
    angular.module("app").controller("ProprietariosController", [
        "Proprietario",
        "Buffer",
        "$scope",
        ProprietariosController
    ]);

    function ProprietariosController(Proprietario, Buffer, $scope) {

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

            Proprietario
                .cadastrados()
                .ContemCpf(_self.filtro.dados.cpfProprietario)
                .OndeNomePContem(_self.filtro.dados.nomeProprietario)
                .listar(_self.filtro.paginacao)
                .then(function (response) {
                    _self.cadastrados = response.data.proprietarios;
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

                Proprietario
                    .remover(_self.proprietario)
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

            //exibir: function () {
            //    _self.modal.desativando = true;
            //    _self.modal.erros = {};
            //    _self.limparMensagens();
            //    Proprietario

            //        .exibir(_self.proprietario)
            //        .then(function (response){
            //            _self.info = response.data;
            //            _self.modal.exibirVeiculos.visivel = false;
            //            _self.consultar(true);
            //        })
            //        .catch(function (response){
            //            _self.modal.erros.aoExibirVeiculos = response.data;
            //        })
            //        ["finally"](function (){
            //            _self.modal.desativando = false;
            //        });
            //},

            limparMensagens: function () {
                _self.modal.info = {};
                _self.modal.erros = {};
            },
            salvar: function () {
                _self.modal.salvando = true;
                _self.modal.erros = {};
                _self.limparMensagens();

                Proprietario
                    .salvar(_self.proprietario)
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

        _self.nova = function () {
            _self.proprietario = {

            };
            _self.modal.info = {};
            _self.modal.erros = {};
        }

        _self.selecionar = function (proprietario) {
            _self.modal.limparMensagens();
            _self.proprietario = angular.copy(proprietario);
        }

        _self.gerarRelatorio = function () {

            Proprietario
                .cadastrados()
                .ContemCpf(_self.filtro.dados.cpf)
                .relatorioProprietarios()
                .success(angular.bind(this, function (pdf) {
                    Buffer.saveToFile(pdf, "application/pdf", "RelatorioProprietarios");
                }));
        };
    }

})();
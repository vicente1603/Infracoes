(function () {
    angular.module("app").service("Modelo", [
        "ApiRequest",
        ModeloService
    ]);

    function ModeloService(ApiRequest) {
        var url;
        var filtro;

        this.cadastrados = function () {
            url = "/Modelos/Cadastrados";
            filtro = {};
            console.log();
            return this;
        }

        this.listar = function (paginacao) {
            filtro.paginacao = paginacao;
            return ApiRequest.json(url, filtro);
        }

        this.ondeDescricaoContem = function (descricao) {
            filtro.descricao = descricao;
            return this;
        }

        this.remover = function (modelo) {
            return ApiRequest.json("/Modelos/Remover", { idModelo: modelo.idModelo });
        }

        this.salvar = function (modelo) {
            if (modelo.idModelo) {
                return ApiRequest.json("/Modelos/Atualizar", modelo)
            }
            else {
                return ApiRequest.json("/Modelos/Cadastrar", modelo)
            }
        }

        this.todos = function () {
            return ApiRequest.json("/Modelos/Todos");
        }
    }

})();
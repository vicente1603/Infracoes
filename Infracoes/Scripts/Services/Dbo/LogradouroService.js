(function () {
    angular.module("app").service("Logradouro", [
        "ApiRequest",
        LogradouroService
    ]);

    function LogradouroService(ApiRequest) {
        var url;
        var filtro;

        this.cadastrados = function () {
            url = "/Logradouros/Cadastrados";
            filtro = {};
            return this;
        }

        this.listar = function (paginacao) {
            filtro.paginacao = paginacao;
            return ApiRequest.json(url, filtro);
        }

        this.ondeCepContem = function (cep) {
            filtro.cep = cep;
            return this;
        }

        this.remover = function (logradouro) {
            return ApiRequest.json("/Logradouros/Remover", { idLogradouro: logradouro.idLogradouro });
        }

        this.salvar = function (logradouro) {
            if (logradouro.idLogradouro) {
                return ApiRequest.json("Logradouros/Atualizar", logradouro);
            } else {
                console.log(logradouro);
                return ApiRequest.json("Logradouros/Cadastrar", logradouro);
            }
        }

        this.todos = function () {
            return ApiRequest.json("Logradouros/Todos");
        }

        this.relatorioLogradouros = function () {
            return ApiRequest.download("/Logradouros/RelatorioLogradouros", filtro);
        }
    }

})();
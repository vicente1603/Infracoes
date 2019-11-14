(function () {
    angular.module("app").service("Agente", [
        "ApiRequest",
        AgenteService
    ]);

    function AgenteService(ApiRequest) {
        var url;
        var filtro;

        this.cadastrados = function () {
            url = "/Agentes/Cadastrados";
            filtro = {};
            return this;
        }

        this.listar = function (paginacao) {
            filtro.paginacao = paginacao;
            return ApiRequest.json(url, filtro);
        }

        this.ondeMatriculaContem = function (matricula) {
            filtro.matricula = matricula;
            return this;
        }

        this.remover = function (agente) {
            return ApiRequest.json("/Agentes/Remover", { idAgente: agente.idAgente });
        }

        this.salvar = function (agente) {
            if (agente.idAgente) {
                return ApiRequest.json("Agentes/Atualizar", agente);
            } else {
                return ApiRequest.json("Agentes/Cadastrar", agente);
            }
        }

        this.todos = function () {
            return ApiRequest.json("Agentes/Todos");
        }
    }

})();
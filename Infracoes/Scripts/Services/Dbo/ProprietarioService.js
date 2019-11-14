(function () {
    angular.module("app").service("Proprietario", [
        "ApiRequest",
        ProprietarioService
    ]);

    function ProprietarioService(ApiRequest) {
        var url;
        var filtro;

        this.cadastrados = function () {
            url = "/Proprietarios/Cadastrados";
            filtro = {};
            return this;
        }

        this.listar = function (paginacao) {
            filtro.paginacao = paginacao;
            return ApiRequest.json(url, filtro);
        }

        this.ContemCpf = function (cpfProprietario) {
            filtro.cpfProprietario = cpfProprietario;
            return this;
        }

        this.OndeNomePContem = function (nomeProprietario) {
            filtro.nomeProprietario = nomeProprietario;
            return this;
        }

        this.remover = function (proprietario) {
            return ApiRequest.json("/Proprietarios/Remover", { idProprietario: proprietario.idProprietario });
        }

        this.salvar = function (proprietario) {
            if (proprietario.idProprietario)
                return ApiRequest.json("/Proprietarios/Atualizar", proprietario);
            else
                return ApiRequest.json("/Proprietarios/Cadastrar", proprietario);
            
        }

        this.todos = function () {
            return ApiRequest.json("/Proprietarios/Todos");
        }
    }
})();
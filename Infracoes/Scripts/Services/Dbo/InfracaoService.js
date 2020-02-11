(function () {
    angular.module("app").service("Infracao", [
        "ApiRequest",
        InfracaoService
    ]);

    function InfracaoService(ApiRequest) {
        var url;
        var filtro;

        this.cadastradas = function () {
            url = "/Infracoes/Cadastradas";
            filtro = {};
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

        this.remover = function (infracao) {
            return ApiRequest.json("/Infracoes/Remover", { idInfracao: infracao.idInfracao });
        }

        this.salvar = function (infracao) {
            if (infracao.idInfracao)
                return ApiRequest.json("/Infracoes/Atualizar", infracao);
            else
                console.log(infracao);
                return ApiRequest.json("/Infracoes/Cadastrar", infracao);
        }

        this.todas = function () {
            return ApiRequest.json("/Infracoes/Todas");
        }

        this.relatorioInfracoes = function () {
            return ApiRequest.download("/Infracoes/RelatorioInfracoes", filtro);
        }
    }

})();
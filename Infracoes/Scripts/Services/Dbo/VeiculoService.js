(function () {
    angular.module("app").service("Veiculo", [
        "ApiRequest",
        VeiculoService
    ]);

    function VeiculoService(ApiRequest) {
        var url;
        var filtro;

        this.cadastrados = function () {
            url = "/Veiculos/Cadastrados";
            filtro = {};
            return this;
        }

        this.listar = function (paginacao) {
            filtro.paginacao = paginacao;
            return ApiRequest.json(url, filtro);
        }

        this.ondePlacaContem = function (placa) {
            filtro.placa = placa;
            return this;
        }
        
        this.remover = function (veiculo) {
            return ApiRequest.json("/Veiculos/Remover", { idVeiculo: veiculo.idVeiculo });
        }

        this.salvar = function (veiculo) {
            if (veiculo.idVeiculo) {
                console.log(veiculo);
                return ApiRequest.json("/Veiculos/Atualizar", veiculo)
            }
            else {
                console.log(veiculo);
                return ApiRequest.json("/Veiculos/Cadastrar", veiculo)
            }
        }

        this.todos = function () {
            return ApiRequest.json("/Veiculos/Todos");
        }
    }

})();
(function () {
    angular.module("app").service("Global", [
        GlobalService
    ]);

    function GlobalService() {
        var _self = this;

        _self.erros = [{ requisicao: "Ocorreu um erro interno durante o processamento da sua requsição. Por favor tente novamente." }];

        _self.textoTotalRegistros = function (registrosExibidos, registrosEncontrados) {
            if (registrosEncontrados > 1) {
                return "Exibindo " + registrosExibidos + " de " + registrosEncontrados + " registros encontrados.";
            }
            else {
                return registrosEncontrados + " registro encontrado.";
            }
        }
    }
})();
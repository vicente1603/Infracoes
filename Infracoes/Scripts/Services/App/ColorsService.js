(function () {
    angular.module("app").service("Cores", [
        CoresService
    ]);

    function CoresService() {
        var _self = this;

        _self.todas = [
                { descricao: "#ffffff" },
                { descricao: "#ff4000" },
                { descricao: "#ff8000" },
                { descricao: "#ffbf00" },
                { descricao: "#ffff00" },
                { descricao: "#bfff00" },
                { descricao: "#80ff00" },
                { descricao: "#40ff00" },
                { descricao: "#00ff00" },
                { descricao: "#00ff40" },
                { descricao: "#00ff80" },
                { descricao: "#00ffbf" },
                { descricao: "#00ffff" },
                { descricao: "#00bfff" },
                { descricao: "#0080ff" },
                { descricao: "#0040ff" },
                { descricao: "#0000ff" },
                { descricao: "#4000ff" },
                { descricao: "#8000ff" },
                { descricao: "#bf00ff" },
                { descricao: "#ff00ff" },
                { descricao: "#ff00bf" },
                { descricao: "#ff0080" },
                { descricao: "#ff0040" },
                { descricao: "#ff0000" }];
    }
})();
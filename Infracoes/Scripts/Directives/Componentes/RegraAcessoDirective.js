(function () {
    angular.module("app").directive("appRegraAcesso", [
        "UsuarioOnline",
        RegraAcessoDirective
    ]);

    function RegraAcessoDirective(UsuarioOnline) {
        return {
            restrict: "A",
            link: function ($scope, $element, attrs) {
                var regrasExigidas = attrs.appRegraAcesso.replace(/\s/, "").split(",");

                $scope.$watch(function () {
                    return UsuarioOnline.regrasAcesso;
                }, function (regrasAcesso) {
                    if (angular.isArray(regrasAcesso)) {
                        for (var u = 0; u < regrasAcesso.length; u++) {
                            var regraUsuario = regrasAcesso[u];

                            for (var e = 0; e < regrasExigidas.length; e++) {
                                var regraExigida = regrasExigidas[e];

                                if (regraUsuario === regraExigida) return;
                            }
                        }

                        $element.remove();
                    }
                });
            }
        };
    }
})();
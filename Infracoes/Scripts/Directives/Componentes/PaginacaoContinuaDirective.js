(function () {
    angular.module("app").directive("appPaginacaoContinua", [
        "$timeout",
        PaginacaoContinuaDirective
    ]);

    function PaginacaoContinuaDirective($timeout) {
        return {
            restrict: "A",
            template: '<button data-ng-show="final()" type="button" class="btn btn-primary-outline btn-block" data-ng-click="proximo()" data-ng-disabled="iniciada">' +
                          '<span data-ng-hide="iniciada">Mostrar mais</span>' +
                          '<span data-ng-show="iniciada">Carregando&nbsp;<i class="fa fa-refresh fa-spin"></i></span>' +
                      '</button>',
            scope: {
                paginacao: "=appPaginacaoContinua",
                atualizar: "&appPaginacaoContinuaAtualizar",
                iniciar: "&appPaginacaoContinuaIniciar",
                quantidade: "=appPaginacaoContinuaQuantidade",
                iniciada: "=appPaginacaoContinuaIniciada",
                registrosExibidos: "=appPaginacaoContinuaRegistrosExibidos"
            },
            link: function ($scope, $element, attrs, ngModelCtrl) {
                $scope.paginacao = {
                    aPartirDo: 0,
                    quantidade: typeof $scope.quantidade === "number" ? $scope.quantidade : 10
                };

                $scope.proximo = function () {
                    if ($scope.registrosExibidos < $scope.paginacao.totalRegistros) {
                        $scope.paginacao.aPartirDo = $scope.registrosExibidos;
                        $scope.atualizar();
                    }
                };

                $scope.final = function () {
                    return !($scope.registrosExibidos === $scope.paginacao.totalRegistros);
                }

                $timeout($scope.iniciar);
            }
        };
    }
})();

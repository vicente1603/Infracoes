(function () {
    angular.module("app").directive("appPaginacao", [
        "$timeout",
        PaginacaoDirective
    ]);

    function PaginacaoDirective($timeout) {
        return {
            restrict: "A",
            template: '<div class="btn-group">' +
                           '<button type="button" class="btn btn-sm btn-default" data-ng-click="primeira()">' +
                               '<span class="fa fa-fast-backward"></span>' +
                           '</button>' +
                           '<button type="button" class="btn btn-sm btn-default" data-ng-click="anterior()">' +
                               '<span class="fa fa-backward"></span>' +
                           '</button>' +
                           '<input type="text" class="btn btn-sm btn-default" value="{{paginacao.pagina}} / {{paginacao.totalPaginas}}" />' +
                           '<button type="button" class="btn btn-sm btn-default" data-ng-click="proxima()">' +
                               '<span class="fa fa-forward"></span>' +
                           '</button>' +
                           '<button type="button" class="btn btn-sm btn-default" data-ng-click="ultima()">' +
                               '<span class="fa fa-fast-forward"></span>' +
                           '</button>' +
                       '</div>',
            scope: {
                paginacao: "=appPaginacao",
                atualizar: "&appPaginacaoAtualizar",
                iniciar: "&appPaginacaoIniciar",
                limite: "=appPaginacaoLimite"
            },
            link: function ($scope, $element, attrs, ngModelCtrl) {
                $scope.paginacao = {
                    pagina: 1,
                    totalPaginas: 1,
                    limite: typeof $scope.limite === "number" ? $scope.limite : 10
                };

                $scope.primeira = function () {
                    if ($scope.paginacao.pagina > 1) {
                        $scope.paginacao.pagina = 1;
                        $scope.atualizar();
                    }
                };

                $scope.anterior = function () {
                    if ($scope.paginacao.pagina > 1) {
                        $scope.paginacao.pagina--;
                        $scope.atualizar();
                    }
                };

                $scope.proxima = function () {
                    if ($scope.paginacao.pagina < $scope.paginacao.totalPaginas) {
                        $scope.paginacao.pagina++;
                        $scope.atualizar();
                    }
                };

                $scope.ultima = function () {
                    if ($scope.paginacao.pagina < $scope.paginacao.totalPaginas) {
                        $scope.paginacao.pagina = $scope.paginacao.totalPaginas;
                        $scope.atualizar();
                    }
                };

                $timeout($scope.iniciar);
            }
        };
    }
})();

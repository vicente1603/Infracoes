(function () {
    angular.module("app").directive("appDigitacao", [
        DigitacaoDirective
    ]);

    function DigitacaoDirective() {
        return {
            require: "ngModel",
            restrict: "A",
            scope: {
                digitacaoIniciada: "&appDigitacaoIniciada",
                digitacaoConcluida: "&appDigitacaoConcluida"
            },
            link: function ($scope, $element, attrs, ngModel) {
                var tempoDigitacao;
                var tempoDigitacaoConcluida = 700;
                var inicializando = true;

                $scope.$watch(function () {
                    return ngModel.$modelValue;
                }, function (newValue) {
                    $element.bind("keydown keypress", function (event) {

                        if (inicializando) {
                            inicializando = false;
                            return;
                        }

                        $scope.digitacaoIniciada();

                        clearTimeout(tempoDigitacao);

                        tempoDigitacao = setTimeout(function () {
                            $scope.$apply(function () {
                                if ($element.is(":focus")) {
                                    $scope.digitacaoConcluida();
                                }
                            });
                        }, tempoDigitacaoConcluida);
                    });
                });
            }
        };
    }
})();

(function () {
    angular.module("app").directive("appCapslock", [
        CapslockDirective
    ]);

    function CapslockDirective() {
        return {
            restrict: "A",
            scope: {
                ativo: "=appCapslock"
            },
            link: function ($scope, $element, attrs) {
                $scope.ativo = false;

                var isMac = /Mac/.test(navigator.platform);

                $element.on("keypress", function (e) {
                    var charCode = e.which;

                    $scope.$apply(function () {
                        if (charCode >= 97 && charCode <= 122) {
                            $scope.ativo = e.shiftKey;
                        } else if (charCode >= 65 && charCode <= 90 && !(e.shiftKey && isMac)) {
                            $scope.ativo = !e.shiftKey;
                        }
                    });
                });
            }
        }
    }
})();


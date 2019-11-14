(function () {
    angular.module("app").directive("appModalTarget", [
        ModalTargetDirective
    ]);

    function ModalTargetDirective() {
        return {
            restrict: "A",
            scope: {
                modal: "=appModalTarget"
            },
            link: function ($scope, $element, attrs) {
                $element.on("click", function () {
                    if (!$scope.modal) return;

                    $scope.$apply(function () {
                        $scope.modal.visivel = true;
                    });
                });
            }
        };
    }
})();

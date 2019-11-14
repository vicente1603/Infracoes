(function () {
    angular.module("app").directive("appModalDismiss", [
        ModalDismissDirective
    ]);

    function ModalDismissDirective() {
        return {
            restrict: "A",
            scope: {
                modal: "=appModalDismiss"
            },
            link: function ($scope, $element) {
                $element.on("click", function () {
                    if (!$scope.modal) return;

                    $scope.$apply(function () {
                        $scope.modal.visivel = false;
                    });
                });
            }
        };
    }
})();

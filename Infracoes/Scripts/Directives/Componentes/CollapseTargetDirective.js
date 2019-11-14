(function () {
    angular.module("app").directive("appCollapseTarget", [
        CollapseTargetDirective
    ]);

    function CollapseTargetDirective() {
        return {
            restrict: "A",
            scope: {
                collapse: "=appCollapseTarget"
            },
            link: function ($scope, $element, attrs) {
                $element.on("click", function () {
                    $scope.$apply(function () {
                        $scope.collapse.visivel = !$scope.collapse.visivel
                    });
                });
            }
        };
    }
})();
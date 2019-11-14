(function () {
    angular.module("app").directive("appCollapse", [
        CollapseDirective
    ]);

    function CollapseDirective() {
        return {
            restrict: "A",
            scope: {
                collapse: "=appCollapse"
            },
            link: function ($scope, $element, attrs) {
                $element.collapse({
                    parent: $element.parent(),
                    toggle: false
                });

                $scope.collapse = { visivel: false };

                $scope.$watch("collapse.visivel", function (visivel) {
                    var event = visivel ? "show" : "hide";
                    $element.collapse(event);
                });

                $element.on("shown.bs.collapse", function (e) {
                    $scope.$apply(function () {
                        $scope.collapse.visivel = true
                    });
                });

                $element.on("hidden.bs.collapse", function (e) {
                    $scope.$apply(function () {
                        $scope.collapse.visivel = false;
                    });
                });
            }
        }
    }
})();
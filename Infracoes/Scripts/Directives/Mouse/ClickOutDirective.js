(function () {
    angular.module("app").directive("appClickOut", [
        ClickOutDirective
    ]);

    function ClickOutDirective() {
        return {
            restrict: "A",
            scope: {
                execute: "&appClickOut"
            },
            link: function ($scope, $element, attrs) {
                $(document).on("click", function (e) {
                    if (!$(e.target).closest($element.parent()).length) {
                        $scope.$apply(function () {
                            $scope.execute();
                        });
                    }
                });
            }
        };
    }
})();

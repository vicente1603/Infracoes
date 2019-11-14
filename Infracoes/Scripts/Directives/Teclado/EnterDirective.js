(function () {
    angular.module("app").directive("appEnter", [
        EnterDirective
    ]);

    function EnterDirective() {
        return {
            restrict: "A",
            link: function ($scope, $element, attrs) {
                $element.bind("keydown keypress", function (e) {
                    if (e.which === 13) {
                        $scope.$apply(attrs.appEnter);
                        e.preventDefault();
                    }
                });
            }
        };
    }
})();

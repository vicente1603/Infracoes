(function () {
    angular.module("app").directive("appSetFocus", [
        "$timeout",
        SetFocusDirective
    ]);

    function SetFocusDirective($timeout) {
        return {
            restrict: "A",
            scope: {
                focus: "=appSetFocus"
            },
            link: function ($scope, $element, attrs) {
                $scope.$watch("focus", function (focus) {
                    if (focus) {
                        $timeout(function () {
                            $element.focus();
                        });
                    }
                });
            }
        };
    }
})();

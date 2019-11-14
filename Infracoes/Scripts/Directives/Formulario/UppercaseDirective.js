(function () {
    angular.module("app").directive("appUppercase", [
        UppercaseDirective
    ]);

    function UppercaseDirective() {
        return {
            require: 'ngModel',
            restrict: 'A',
            link: function ($scope, $element, attrs, ctrl) {
                var toUpperCase = function (value) {
                    if (value) {
                        var valueUpperCase = value.toUpperCase();

                        if (valueUpperCase !== value) {
                            ctrl.$setViewValue(valueUpperCase);
                            ctrl.$render();
                        }

                        return valueUpperCase;
                    }

                    return '';
                }

                ctrl.$parsers.push(toUpperCase);
                toUpperCase($scope[attrs.ngModel]);
            }
        };
    }
})();

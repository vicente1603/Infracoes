(function () {
    angular.module("app").directive("appLowercase", [
        LowercaseDirective
    ]);

    function LowercaseDirective() {
        return {
            require: 'ngModel',
            restrict: 'A',
            link: function ($scope, $element, attrs, ctrl) {
                var toLowerCase = function (value) {
                    if (value) {
                        var valueLowerCase = value.toLowerCase();

                        if (valueLowerCase !== value) {
                            ctrl.$setViewValue(valueLowerCase);
                            ctrl.$render();
                        }

                        return valueLowerCase;
                    }

                    return '';
                }

                ctrl.$parsers.push(toLowerCase);
                toLowerCase($scope[attrs.ngModel]);
            }
        };
    }
})();

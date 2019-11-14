(function () {
    angular.module("app").directive("appMascara", [
        MaskDirective
    ]);

    function MaskDirective() {
        return {
            restrict: "A",
            require: "ngModel",
            link: function ($scope, $element, attrs) {
                $element.mask(attrs["appMascara"])
            }
        }
    }
})();

(function () {
    angular.module("app").directive("appCpf", [
        CpfDirective
    ]);

    function CpfDirective() {
        return {
            restrict: "A",
            require: "ngModel",
            link: function ($scope, $element, attrs) {
                $element.mask("999.999.999-99");
            }
        };
    }
})();

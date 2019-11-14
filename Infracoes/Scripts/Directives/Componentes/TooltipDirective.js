(function () {
    angular.module("app").directive("appTooltip", [
        TooltipDirective
    ]);

    function TooltipDirective() {
        return {
            restrict: 'A',
            link: function ($scope, $element, attrs) {
                $element.tooltip({
                    title: attrs.appTooltip,
                    placement: "top"
                });
            }
        };
    }
})();

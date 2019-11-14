(function () {
    angular.module("app").directive("appTabContent", [
        TabContentDirective
    ]);

    function TabContentDirective() {
        return {
            restrict: "A",
            scope: {
                tabName: "@appTabContent"
            },
            link: function ($scope, $element) {
                $element.attr("id", $scope.tabName.replace(/\./g, "-"));
            }
        };
    }
})();

(function () {
    angular.module("app").directive("appTab", [
        TabDirective
    ]);

    function TabDirective() {
        return {
            restrict: "A",
            scope: {
                tab: "=appTab",
                tabName: "@appTab",
                visivel: "=appTabVisivel"
            },
            link: function ($scope, $element) {
                $scope.tab = {
                    visivel: false
                };

                $scope.$watch("visivel", function (visivel) {
                    if (angular.isUndefined(visivel)) return;
                    $scope.tab.visivel = visivel;
                });

                $scope.$watch("tab.visivel", function (visivel) {
                    if (!visivel) return;
                    $element.tab("show");
                });

                $element.attr("data-target", "#" + $scope.tabName.replace(/\./g, "-"));
                $element.attr("data-toggle", "tab");

                $element.on("click", function (e) {
                    $scope.$apply(function () {
                        $scope.tab.visivel = true;
                    });
                });

                $element.on("hidden.bs.tab", function (e) {
                    $scope.tab.visivel = false;
                });
            }
        };
    }
})();

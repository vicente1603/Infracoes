(function () {
    angular.module("app").directive("appCarregando", [
        "$http",
        CarregandoDirective
    ]);

    function CarregandoDirective($http) {
        return {
            restrict: "A",
            link: function ($scope, $element, attrs) {
                var $backdrop = $("<div/>").addClass("modal-backdrop").css({ "z-index": 5000 }).hide().prependTo("body");
                $element.css({ "z-index": 5001 });

                $scope.$watch(function () {
                    return $http.pendingRequests.length;
                }, function (pendingRequests) {
                    if (pendingRequests > 0) {
                        $backdrop.show();
                        $element.show();
                    } else {
                        $element.hide();
                        $backdrop.hide();
                    }
                });

                $scope.$on("$routeChangeStart", function () {
                    $backdrop.show();
                    $element.show();
                });

                $scope.$on("$routeChangeSuccess", function () {
                    if ($http.pendingRequests.length > 0) return;
                    $backdrop.hide();
                    $element.hide();
                });

                $scope.$on("$routeChangeError", function () {
                    if ($http.pendingRequests.length > 0) return;
                    $backdrop.hide();
                    $element.hide();
                });
            }
        };
    }
})();
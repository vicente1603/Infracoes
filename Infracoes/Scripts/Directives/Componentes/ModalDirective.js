(function () {
    angular.module("app").directive("appModal", [
        ModalDirective
    ]);

    function ModalDirective() {
        return {
            restrict: "A",
            scope: {
                modal: "=appModal",
                visivel: "=appModalVisivel"
            },
            link: function ($scope, $element, attrs) {
                $scope.modal = {
                    visivel: false
                };

                $scope.$watch("visivel", function (visivel) {
                    if (angular.isUndefined(visivel)) return;
                    $scope.modal.visivel = visivel;
                });

                $scope.$watch("modal.visivel", function (visivel) {
                    var event = visivel ? "show" : "hide";
                    $element.modal(event);
                });

                $scope.$on("$routeChangeSuccess", angular.bind(this, function () {
                    $("body").children(".modal-backdrop.in").remove();
                }));

                $element.on("hidden.bs.modal", function (e) {
                    $scope.$apply(function () {
                        $scope.modal.visivel = false;
                    });
                });

                $element.on("visiveln.bs.modal", function (e) {
                    $scope.$apply(function () {
                        $scope.modal.visivel = true;
                    });
                });
            }
        };
    }
})();

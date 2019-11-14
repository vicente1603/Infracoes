(function () {
    angular.module("app").directive("appUpload", [
        "$parse",
        FilUploadDirective
    ]);

    function FilUploadDirective($parse) {
        return {
            restrict: "A",
            link: function ($scope, $element, attrs) {
                var model = $parse(attrs.appUpload);
                var modelSetter = model.assign;

                $element.bind("change", function (e) {
                    $scope.$apply(function () {
                        modelSetter($scope, e.target.files[0]);
                    });
                });
            }
        };
    }
})();
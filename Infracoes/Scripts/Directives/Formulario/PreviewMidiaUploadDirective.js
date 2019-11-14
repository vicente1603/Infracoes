(function () {
    angular.module("app").directive("appPreviewMidiaUpload", [
        PreviewMidiaUploadDirective
    ]);

    function PreviewMidiaUploadDirective() {
        return {
            restrict: "A",
            scope: {
                file: "=appPreviewMidiaUpload"
            },
            link: function ($scope, $element, attrs) {
                $scope.$watch('file', function (file) {
                    if (file) {
                        var reader = new FileReader();

                        reader.onload = function (e) {
                            $element.attr('src', e.target.result);
                        };

                        reader.readAsDataURL($scope.file);
                    }
                })
            }
        };
    }
})();
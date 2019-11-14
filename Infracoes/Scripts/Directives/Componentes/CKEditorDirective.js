(function () {
    angular.module("app").directive("appCkeditor", [
        CKEditorDirective
    ]);

    function CKEditorDirective() {
        return {
            restrict: "A",
            scope: {
                conteudo: "=appCkeditor",
                path: "@appCkeditorPath"
            },
            link: function ($scope, $element, attrs) {
                $scope.conteudo = {};

                var config = {
                    filebrowserBrowseUrl: "/Browser",
                    filebrowserUploadUrl: "/Upload-Thumbnail",
                }

                var editor = CKEDITOR.replace($element[0], config);

                //editor.on('instanceReady', function () {
                //    CKEDITOR.instances.editor1.setData($scope.conteudo);
                //});

                editor.on('change', updateModel);
                editor.on('key', updateModel);
                editor.on('dataReady', updateModel);

                $scope.$watch("conteudo", function (conteudo) {
                    CKEDITOR.instances.editor1.setData($scope.conteudo);
                });

                function updateModel() {
                    $scope.conteudo = editor.getData();
                }
            }
        }
    }
})();
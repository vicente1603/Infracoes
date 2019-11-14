(function () {
    angular.module("app").directive("appCkeditorMidia", [
        CKEditorMidiaDirective
    ]);

    function CKEditorMidiaDirective() {
        return {
            restrict: "A",
            link: function ($scope, $element, attrs) {
                $element.on('click', function () {
                    window.opener.CKEDITOR.tools.callFunction(1,attrs['appCkeditorMidia']);
                    window.close();
                });
            }
        }
    }
})();
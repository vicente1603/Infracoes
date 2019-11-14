(function () {
    var app = angular.module("app", []);

    app.factory("AppHttpInterceptor", [
       "$q",
       "$window",
       AppHttpInterceptor
    ]);

    app.directive("a", [
    AnchorExtensionDirective
    ]);

    app.config([
        "$locationProvider",
        "$httpProvider",
        AppConfiguration
    ]);
    
    var SISTEMA_URL = (window.SISTEMA || "").replace(/\/$/, "");;

    function AppConfiguration($locationProvider, $httpProvider) {
        $httpProvider.interceptors.push("AppHttpInterceptor");
    }

    function AnchorExtensionDirective() {
        return {
            restrict: "E",
            link: function ($scope, $element, attrs) {
                var href = $element.attr("href");

                if (href && href.indexOf("/") === 0) {
                    var urlhrf = SISTEMA_URL === "" ? href : "/" + SISTEMA_URL + href;

                    $element.attr("href", urlhrf);
                }
            }
        };
    }

    $('a[href*="#"]:not([href="#"])').click(function () {
        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
            var target = $(this.hash);
            target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
            if (target.length) {
                $('html, body').animate({
                    scrollTop: target.offset().top
                }, 1000);
                return false;
            }
        }
    });

    function AppHttpInterceptor($q, $window) {
        return {
            "request": function (config) {
                return config || $q.when(config);
            },
            "requestError": function (rejection) {
                return $q.reject(rejection);
            },
            "response": function (response) {
                return response || $q.when(response);
            },
            "responseError": function (rejection) {
                if (rejection.status === 500)
                    $window.location.href = "/RequisicaoNaoCompletada";

                return $q.reject(rejection);
            }
        };
    }
})();
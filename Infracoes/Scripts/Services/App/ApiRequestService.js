(function () {
    angular.module("app").service("ApiRequest", [
        "$http",
        "$q",
        ApiRequestService
    ]);

    function ApiRequestService($http, $q) {

        var deferredAbort = $q.defer();


        this.url = function (url) {
            var urlSistema = window.SISTEMA ? "/" + window.SISTEMA + url : url;

            return urlSistema;
        };

        this.json = function (url, data) {
            return $http({
                url: this.url(url),
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=UTF-8"
                },
                data: data,
                timeout: deferredAbort.promise
            });
        };

        this.abortRequest = function () {
            return deferredAbort.resolve();
        }

        this.getJSONP = function (url, data) {
            return $http({
                url: url + data,
                method: "GET",
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                }
            });
        }
        
        this.download = function (url, data) {
            return $http({
                url: this.url(url),
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=UTF-8"
                },
                responseType: "arraybuffer",
                data: data
            });
        };

        this.upload = function (url, data) {
            var formData;

            if (window.FormData !== undefined) {
                formData = new FormData();
                angular.forEach(data, function (value, key) {
                    formData.append(key, value);
                });
            }

            return $http({
                url: url,
                method: "POST",
                transformRequest: angular.identity,
                headers: {
                    "Content-Type": undefined
                },
                data: formData
            });
        };
    }
})();
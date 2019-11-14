(function () {
    angular.module("app").service("Buffer", [
        "$sce",
        BufferService
    ]);

    function BufferService($sce) {
        this.blob = function (buffer, type) {
            type = type || "application/octet-stream";

            var blob;

            try {
                blob = new Blob([buffer], { type: type });
            } catch (e) {
                var bb = new (window.WebKitBlobBuilder || window.MozBlobBuilder);
                bb.append(buffer);

                blob = bb.getBlob(type);
            }

            return blob;
        };

        //<object data="{{object}}" type="application/pdf" style="width:800px;height:600px;"></object>
        this.object = function (buffer, type) {
            return $sce.trustAsResourceUrl(this.url(buffer, type));
        };

        this.url = function (buffer, type) {
            return URL.createObjectURL(this.blob(buffer, type));
        };

        this.saveToFile = function (buffer, type, filename) {
            var a = document.createElement("a");
            a.href = this.url(buffer, type);
            a.download = filename;
            a.click();
            a.remove();
        };
    }
})();
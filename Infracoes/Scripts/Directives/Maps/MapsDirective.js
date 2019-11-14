(function () {
    angular.module('app').directive('appMaps', [
        'Places',
        'GoogleMaps',
        MapsDirective
    ]);

    function MapsDirective(Places, GoogleMaps) {
        return {
            restrict: 'A',
            scope: {
                googleMaps: "=appMaps",
                methodInitialize: "&appMapsInitializeOnload",
                zoom: "=appMapsZoom",
                scrollwheel: "=appMapsScrollwheel",
                panControl: "=appMapsPanControl",
                zoomControl: "=appMapsZoomControl",
                mapTypeControl: "=appMapsTypeControl",
                scaleControl: "=appMapsScaleControl",
                streetViewControl: "=appMapsStreetViewControl",
                overviewMapControl: "=appMapsOverviewControl",
                centerLat: "=appMapsCenterLatitude",
                centerLong: "=appMapsCenterLongitude",
                waitingBounds: "&appMapsWaitingBounds",
                click: "&appMapsOnClick"
            },
            link: function ($scope, $element, attrs) {
                function initialize() {
                    var options = {
                        element: $element[0],
                        zoom: $scope.zoom,
                        scrollwheel: $scope.scrollwheel,
                        panControl: $scope.panControl,
                        zoomControl: $scope.zoomControl,
                        mapTypeControl: $scope.mapTypeControl,
                        scaleControl: $scope.scaleControl,
                        streetViewControl: $scope.streetViewControl,
                        overviewMapControl: $scope.overviewMapControl,
                        center: new google.maps.LatLng($scope.centerLat, $scope.centerLong),
                        disableDefaultUI: true
                    }

                    $scope.googleMaps = new GoogleMaps.mapa(options);

                    GoogleMaps
                        .setMapa($scope.googleMaps.mapa);

                    if (typeof ($scope.click()) == "function") {
                        google.maps.event.addListener($scope.googleMaps.mapa, "click", function (event) {
                            new google.maps.Geocoder().geocode({ 'latLng': event.latLng }, function (results, status) {
                                if (status == google.maps.GeocoderStatus.OK) {
                                    if (results[0]) {
                                        var options = {
                                            position: event.latLng,
                                            dados: Places.getPlace(results[0])
                                        }

                                        $scope.click()(options);
                                    }
                                }
                            });
                        });
                    }
                }

                if (attrs.hasOwnProperty("appMapsModalResize")) {
                    $(".modal").on("shown.bs.modal", function () {
                        google.maps.event.trigger($scope.googleMaps.mapa, "resize");
                    });
                }

                if (attrs.hasOwnProperty("appMapsInitializeOnload")) {
                    initialize();
                    $scope.methodInitialize();
                }
            }
        }
    };
})();
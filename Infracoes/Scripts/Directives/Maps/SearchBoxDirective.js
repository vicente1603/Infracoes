(function () {
    angular.module('app').directive('appSearchbox', [
        'Places',
        SearchBoxDirective
    ]);

    function SearchBoxDirective(Places) {
        return {
            restrict: 'A',
            scope: {
                map: "=appSearchbox",
                local: "=appSearchboxLocal",
                clearMarkers: "=appSearchBoxClearMarkers",
                consultar: "&appSearchBoxConsultar",
            },
            link: function ($scope, $element, attrs) {

                $scope.$watch('map', function (mapa) {
                    if (mapa) {
                        var map = mapa.mapa;

                        var southWest = new google.maps.LatLng(-11.618130173576244, -38.54990943978942);
                        var northEast = new google.maps.LatLng(-9.458061469950593, -36.32517799447692);

                        var bounds = new google.maps.LatLngBounds(southWest, northEast);
                        map.fitBounds(bounds);

                        var options = {
                            bounds: bounds,
                            strictBounds:true,
                            componentRestrictions: { country: 'br' },
                        }

                        var searchBox = new google.maps.places.Autocomplete($element[0], options);

                        searchBox.addListener('place_changed', function () {
                            var place = searchBox.getPlace();

                            if (place.address_components) {
                                $scope.$apply(function () {
                                    local = Places.getPlace(place);
                                });
                            }
                            else {
                                $scope.$apply(function () {
                                    local.locais.push(Places.getPlace(place));
                                });
                            }

                            $scope.consultar()({ dados: local, position: place.geometry.location });

                            $scope.$apply(function () {
                                var options = {
                                    center: true,
                                    position: place.geometry.location,
                                    title: place.name,
                                }

                                local.marker = mapa.addMarker(options);

                                $scope.local = local;
                            });

                            map.setZoom(18);
                        });
                    }
                });
            }
        }
    }
})();
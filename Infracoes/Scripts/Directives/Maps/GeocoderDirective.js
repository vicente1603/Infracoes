(function () {
    angular.module('app').directive('appGeocoder', [
        GeocoderDirective
    ]);

    function GeocoderDirective() {
        return {
            restrict: 'A',
            scope: {
                tipo: "=appGeocoderTipo",
                parametro: "=appGeocoderParametro",
                localizacao: "=appGeocoderLocalizacao",
                googleMaps: '=appGeocoderMaps'
            },
            link: function ($scope, $element, attrs) {

                var geocoder = new google.maps.Geocoder();
                var tipo = $scope.tipo;
                var parametro = $scope.parametro;

                $scope.$watch('googleMaps', function (maps) {
                    if (maps) {
                        var mapa = maps.mapa;
                        geocoder.geocode({ 'address': $element[0].value }, function (results, status) {
                            if (status === google.maps.GeocoderStatus.OK) {
                                if (results[0]) {
                                    var options = {
                                        center: true,
                                        position: results[0].geometry.location,
                                        title: results[0].name,
                                    }

                                    mapa.addMarker(options);


                                } else {
                                    console.log('No results found');
                                }
                            } else {
                                console.log('Geocoder failed due to: ' + status);
                            }
                        });
                    }
                });
            }
        }
    };
})();
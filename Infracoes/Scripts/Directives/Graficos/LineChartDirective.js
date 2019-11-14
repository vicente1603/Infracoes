(function () {
    angular.module("app").directive("appLineChart", [
        LineChartDirective
    ]);

    function LineChartDirective() {
        return {
            restrict: "A",
            link: function ($scope, $element, attrs) {
                var ctx = $element.get(0).getContext("2d");

                var data = {
                    labels: ["Jan", "Fev", "Mar", "Abr", "Maio", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"],
                    datasets: [
                        {
                            label: "My First dataset",
                            fillColor: "#0275d8",
                            strokeColor: "#0275d8",
                            pointColor: "rgba(220,220,220,1)",
                            pointStrokeColor: "#fff",
                            pointHighlightFill: "#fff",
                            pointHighlightStroke: "rgba(220,220,220,1)",
                            data: [65, 59, 80, 81, 56, 55, 40]
                        }
                    ]
                };

                var myLineChart = new Chart(ctx).Line(data, { responsive: true, scaleShowGridLines: false, pointDotRadius: 4, });
            }
        }
    }
})();
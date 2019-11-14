(function () {
    angular.module("app").directive("appMenuActive", [
        MenuActiveDirective
    ]);

    function MenuActiveDirective() {
        return {
            restrict: 'A',
            link: function ($scope, $element, attrs) {
                $element.find(function () {
                    var ul = $(this);

                    $(this).find('li').each(function () {

                        var li = $(this);

                        $(this).find('a').click(function (e) {
                            var href = $(this);

                            ul.find('li').each(function () {
                                $(this).removeClass('active');
                            })


                            if (!li.hasClass('active')) {
                                li.addClass('active');
                            }
                        });
                    })
                });
            }
        };
    }
})();

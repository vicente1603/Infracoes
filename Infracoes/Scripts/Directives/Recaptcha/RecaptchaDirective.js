(function () {
    angular.module('app').directive('appRecaptcha', [
        RecaptchaDirective
    ]);

    function RecaptchaDirective() {
        return {
            restrict: 'A',
            scope: {
                key: '=appRecaptchaKey'
            },
            require: 'ngModel',
            link: function ($scope, $element, attrs, ngModel) {
                var recaptcha;
                ngModel.captcha = function (modelValue, ViewValue) {
                    return !!ViewValue;
                };

                function update(response) {
                    $scope.$apply(function () {
                        ngModel.$setViewValue(response);
                        ngModel.$render();
                    });
                }

                function expired() {
                    grecaptcha.reset(recaptcha);
                    ngModel.$setViewValue('');
                    ngModel.$render();
                }

                function iscaptchaReady() {
                    if (typeof grecaptcha !== "object") {
                        return setTimeout(iscaptchaReady, 250);
                    }
                    recaptcha = grecaptcha.render(
                         $element[0], {
                             "sitekey": $scope.key,
                             "callback": update,
                             "expired-callback": expired
                         }
                    );
                }
                iscaptchaReady();
            }
        }
    }
})();
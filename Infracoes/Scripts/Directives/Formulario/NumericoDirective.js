(function () {
    angular.module("app").directive("appNumerico", [
        "$filter",
        "$timeout",
        "CursorTexto",
        NumericoDirective
    ]);

    if (!$.browser) {
        $.browser = {};
        $.browser.mozilla = /mozilla/.test(navigator.userAgent.toLowerCase()) && !/webkit/.test(navigator.userAgent.toLowerCase());
        $.browser.webkit = /webkit/.test(navigator.userAgent.toLowerCase());
        $.browser.opera = /opera/.test(navigator.userAgent.toLowerCase());
        $.browser.msie = /msie/.test(navigator.userAgent.toLowerCase());
    }

    function NumericoDirective($filter, $timeout, CursorTexto) {
        return {
            restrict: "A",
            require: "ngModel",
            link: function ($scope, $element, attrs, ngModel) {
                var $numberFilter = $filter("number"),
                    precisao = attrs.appNumerico ? attrs.appNumerico.split(",") : [],
                    digitos = parseInt(precisao[0]) || 0,
                    fracao = parseInt(precisao[1]) || 0,
                    negativo = attrs.hasOwnProperty("appNumericoNegativo"),
                    numero = 0;

                ngModel.$formatters.push(function (modelValue) {
                    return $numberFilter(modelValue, fracao);
                });

                ngModel.$parsers.push(function (viewValue) {
                    return numero;
                });

                function alterarSinal(valor) {
                    if (negativo) {
                        if (valor !== "" && valor.charAt(0) === "-") {
                            valor = valor.replace("-", "");
                        } else if (numero > 0) {
                            valor = "-" + valor;
                        }

                        numero *= -1;
                    }

                    return valor;
                };

                function permiteAdicionarDigitos(valor) {
                    var numeros = valor.replace(/[^0-9]/g, ""),
                        posicaoCursor = CursorTexto.posicao($element),
                        limiteDigitosNaoAtingido = !(numeros.length >= digitos),
                        possuiNumeroSelecionado = (posicaoCursor.inicio !== posicaoCursor.termino &&
                                                   valor.substring(posicaoCursor.inicio, posicaoCursor.termino).match(/\d/)) ? true : false,
                        iniciaComZero = (valor.substring(0, 1) === "0");

                    return limiteDigitosNaoAtingido || possuiNumeroSelecionado || iniciaComZero;
                };

                function removerSinalNegativo(valor) {
                    if (numero < 0) {
                        numero *= -1;
                    }

                    return valor.replace("-", "");
                };

                function mascararValor(valorAnterior) {
                    var sinalNegativo = (valorAnterior.indexOf("-") >= 0 && negativo) ? "-" : "",
                        numeros = valorAnterior.replace(/[^0-9]/g, ""),
                        valorInteiro = numeros.slice(0, numeros.length - fracao),
                        valorAtual;

                    if (valorInteiro === "") {
                        valorInteiro = "";
                    }

                    valorAtual = sinalNegativo + valorInteiro;

                    if (fracao > 0) {
                        var valorDecimal = numeros.slice(numeros.length - fracao),
                            zerosAEsquerda = new Array((fracao + 1) - valorDecimal.length).join(0);

                        valorAtual += "." + zerosAEsquerda + valorDecimal;

                        numero = parseFloat(valorAtual);
                    } else {
                        numero = parseInt(valorAtual);
                    }

                    return $numberFilter(valorAtual, fracao);
                };

                $element.on("keypress", function (e) {
                    var tecla = e.which || e.charCode || e.keyCode,
                        digito,
                        posicaoCursor,
                        valor = $element.val();

                    // IE
                    if (tecla === undefined) return false;

                    // Enter ou tab
                    if (tecla === 13 || tecla === 9) return true;

                    // Permite setas para firefox e bloqueia "%" (e.charCode 0, e.keyCode 37)
                    if ($.browser.mozilla && (tecla === 37 || tecla === 39) && e.charCode === 0) return true;

                    // qualquer valor execeto valores numéricos (0-9)
                    if (tecla < 48 || tecla > 57) {
                        if (tecla === 45) { // sinal de menos (-)
                            valor = alterarSinal(valor);
                        } else if (tecla === 43) { // sinal de mais (+)
                            valor = removerSinalNegativo(valor);
                        } else {
                            e.preventDefault();
                            return true;
                        }
                    } else {
                        if (!permiteAdicionarDigitos(valor)) return false;

                        e.preventDefault();

                        digito = String.fromCharCode(tecla);
                        posicaoCursor = CursorTexto.posicao($element);

                        valor = valor.substring(0, posicaoCursor.inicio) + digito + valor.substring(posicaoCursor.termino, valor.length);
                        valor = mascararValor(valor);
                    }

                    ngModel.$setViewValue(valor);
                    ngModel.$render();
                    $scope.$apply();

                    return false;
                });

                $element.on("keydown", function (e) {
                    e = e || window.event;

                    var tecla = e.which || e.charCode || e.keyCode,
                        posicaoCursor,
                        posicaoInicial,
                        posicaoFinal,
                        valor = $element.val();

                    // IE
                    if (tecla === undefined) return false;

                    posicaoCursor = CursorTexto.posicao($element);
                    posicaoInicial = posicaoCursor.inicio;
                    posicaoFinal = posicaoCursor.termino;

                    // backspace ou delete (incluindo caso específico para safari)
                    if (tecla === 8 || tecla === 46 || tecla === 63272) {
                        e.preventDefault();

                        // sem seleção
                        if (posicaoCursor.inicio === posicaoCursor.termino) {
                            if (tecla === 8) { // backspace
                                posicaoInicial -= 1;
                            } else { //delete
                                posicaoFinal += 1;
                            }
                        }

                        valor = valor.substring(0, posicaoInicial) + valor.substring(posicaoFinal, valor.length);
                        valor = mascararValor(valor);

                        ngModel.$setViewValue(valor);
                        ngModel.$render();
                        $scope.$apply();

                        return false;
                    }

                    return true;
                });

                $element.on("click", function () {
                    var input = $element.get(0), length;

                    if (input.setSelectionRange) {
                        length = $element.val().length;
                        input.setSelectionRange(length, length);
                    }
                });

                $element.on("cut paste", function () {
                    $timeout(function () {
                        var valor = $element.val();
                        valor = mascararValor(valor);

                        ngModel.$setViewValue(valor);
                        ngModel.$render();
                    });
                });
            }
        };
    }
})();

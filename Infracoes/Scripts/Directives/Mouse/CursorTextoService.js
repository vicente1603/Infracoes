(function () {
    angular.module("app").service("CursorTexto", [
        CursorTextoService
    ]);

    function CursorTextoService() {
        this.posicao = function ($element) {
            var element = $element.get(0),
                inicio = 0,
                termino = 0,
                valorNormalizado,
                range,
                textInputRange,
                length,
                terminoRange;

            if (typeof element.selectionStart === "number" && typeof element.selectionEnd === "number") {
                inicio = element.selectionStart;
                termino = element.selectionEnd;
            } else {
                range = document.selection.createRange();

                if (range && range.parentElement() === el) {
                    length = element.value.length;
                    valorNormalizado = element.value.replace(/\r\n/g, "\n");

                    // Create a working TextRange that lives only in the input
                    textInputRange = element.createTextRange();
                    textInputRange.moveToBookmark(range.getBookmark());

                    // Check if the start and end of the selection are at the very termino
                    // of the input, since moveStart/moveEnd doesn't return what we want
                    // in those cases
                    terminoRange = element.createTextRange();
                    terminoRange.collapse(false);

                    if (textInputRange.compareEndPoints("StartToEnd", terminoRange) > -1) {
                        inicio = termino = length;
                    } else {
                        inicio = -textInputRange.moveStart("character", -length);
                        inicio += valorNormalizado.slice(0, inicio).split("\n").length - 1;

                        if (textInputRange.compareEndPoints("EndToEnd", terminoRange) > -1) {
                            termino = length;
                        } else {
                            termino = -textInputRange.moveEnd("character", -length);
                            termino += valorNormalizado.slice(0, termino).split("\n").length - 1;
                        }
                    }
                }
            }

            return {
                inicio: inicio,
                termino: termino
            };
        };
    }
})();
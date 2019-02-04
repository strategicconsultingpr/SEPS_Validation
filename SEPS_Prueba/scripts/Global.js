//AVOID BACK
$.fn.exists = function () { try { return this.length !== 0; } catch (e) { return false; } }
$(document).ready(function () {
    $("*").on('keydown', function (e) { avoidBack(e) });
});
function avoidBack(e) {
    var $avoidStop = $('input[type="password"]:focus').exists() || $('input[type="text"]:focus').exists() || $('textarea:focus').exists();
    if (!e) { e = window.event; }
    if (e.keyCode == 8 && !$avoidStop) {
        e.stopPropagation();
        e.preventDefault();
        e.returnValue = false;
        return false;
    }
}
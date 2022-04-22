$(document).ready(function () {

    // frmActionModeSetup();

    startupFunctions();
    //if (IsPostBack() == "False") {
    //    var accion = document.getElementById("mainBodyContent_WucEpisodioAdmision_hAccion");

    //    if (accion.value === "update") {
    //        AccionUpdate();
    //    }
    //    else {
    //        ddlNivelCuidadoSaludMental();
    //        ddlNivelCuidadoSustancias();
    //        CO_Tipo();
    //    }
    //}

});


function txtFumadoChange(input) {
    var validator = document.getElementById("mainBodyContent_WucEpisodioAdmision_rfvTxtFumado");


    if (isNaN(input.value)) {
        alert("Entre un número valido.");
        input.value = "";
        ValidatorEnable(validator, true);
    }
    else if (parseInt(input.value) < 1) {
        alert("Entre un número mayor o igual que 1.");
        input.value = "";
        ValidatorEnable(validator, true);
    }
}

function ddInFumadoChange() {
    var dropdown = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlInFumado");
    var ddlFrecuenciaFumado = "#mainBodyContent_WucEpisodioAdmision_ddlFrecuenciaFumado";
    var txtNrFumado = "#mainBodyContent_WucEpisodioAdmision_txtNrFumado";


    if (dropdown != null) {
        if (dropdown.value == "1") {
            $(ddlFrecuenciaFumado).prop("disabled", false);
            $(txtNrFumado).prop("disabled", false);
            $(ddlFrecuenciaFumado).val(" ");
            $(txtNrFumado).val(" ");
            $(ddlFrecuenciaFumado + ' option[value=3]').removeAttr('disabled').hide();


        }
        else {

            $(ddlFrecuenciaFumado).prop("disabled", true);
            $(txtNrFumado).prop("disabled", true);
            $(ddlFrecuenciaFumado + ' option[value=3]').removeAttr('disabled').show();

            $(ddlFrecuenciaFumado).val("3");
            $(txtNrFumado).val("0");
            $(txtNrFumado).val("0");


        }
    }


}

function nivelCuidadoValidation(source, arguments) {
    var ddlNivelCuidadoSaludMental = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSaludMental");
    var ddlNivelCuidadoSustancias = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSustancias");

    if (ddlNivelCuidadoSaludMental.value == '99' && ddlNivelCuidadoSustancias.value == '99') {
        alert("Favor elegir un nivel de cuidado. Ambos no pueden ser no aplica.");
        arguments.IsValid = false;
    }
    else { arguments.IsValid = true; }


}



function diagnosticoConcurrente(source, arguments) {
    var ddlDSMVDiagDual = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDSMVDiagDual");
    var hDSMVClinPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_hDSMVClinPrim");
    var hDSMVSusPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_hDSMVSusPrim");

    validateCOOCURRING();


}

//window.onload = function () {
//    alert("Entro 1");
//    var scrollY = parseInt('<%=Request.Form["scrollY"] %>');
//    if (!isNaN(scrollY)) {
//        window.scrollTo(0, scrollY);
//    }
//};
//window.onscroll = function () {
//    alert("Entro 2");
//    var scrollY = document.body.scrollTop;
//    alert(scrollY);
//    if (scrollY == 0) {
//        if (window.pageYOffset) {
//            scrollY = window.pageYOffset;
//        }
//        else {
//            alert("Entro else");
//            scrollY = (document.body.parentElement) ? document.body.parentElement.scrollTop : 0;
//        }
//    }
//    if (scrollY > 0) {
//        var input = document.getElementById("scrollY");
//        if (input == null) {
//            input = document.createElement("input");
//            input.setAttribute("type", "hidden");
//            input.setAttribute("id", "scrollY");
//            input.setAttribute("name", "scrollY");
//            document.forms[0].appendChild(input);
//        }
//        input.value = scrollY;
//    }
//};


function startupFunctions() {

    try {
        changeTabOrder();
        ddlEstadoLegal_Load();
        ddlVaron();

        if (IsPostBack() == "False") {
            ddlGrado();
        }
        ddlFuenteReferido();
        ddlDSMVPsicoAmbiPrim();
        ddlDSMVPsicoAmbiSec();
        ddlPreviosMental();
        ddlPreviosSustancias();
        ddlUltMental();
        ddlUltSustancias();
        ddlReunionesGrupos();
        ddlEstadoLegal();
        //ddlArrestado();
        ddlArrestado30();
        ddlCondLaboral();
        txtNbHogar();


        var accion = document.getElementById("mainBodyContent_WucEpisodioAdmision_hAccion");

        if (accion.value === "update") {
            AccionUpdate();

            if (IsPostBack() != "False") {
                ddlDrogaPrimF();
            }


        }
        else {
            ddlDrogaPrimF();

            if (IsPostBack() == "False") {
                ddlNivelCuidadoSaludMental();
                CO_Tipo();

                ddlNivelCuidadoSustancias();
            }
        }
        ddlDrogaChangeEvent();
        ddlDrogaChange("#mainBodyContent_WucEpisodioAdmision_ddlDrogaPrim", 'ddlViaPrim', 'ddlFrecPrim', 'txtEdadPrim', 'ddlToxicologia1');
        ddlDrogaChange("#mainBodyContent_WucEpisodioAdmision_ddlDrogaSec", 'ddlViaSec', 'ddlFrecSec', 'txtEdadSec', 'ddlToxicologia2');
        ddlDrogaChange("#mainBodyContent_WucEpisodioAdmision_ddlDrogaTerc", 'ddlViaTerc', 'ddlFrecTerc', 'txtEdadTerc', 'ddlToxicologia3');



    }
    catch (ex) {
        throw ex;
    }
}



function IsPostBack() {
    return document.getElementById('postbackControl').value;

}





function CO_Tipo() {
    try {
        var CO_Tipo = document.getElementById("mainBodyContent_WucEpisodioAdmision_CO_Tipo");
        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaPrim");
        var ddlDrogaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaSec");
        var ddlDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaTerc");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaPrim");
        var ddlViaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaSec");
        var ddlViaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaTerc");
        var ddlFrecPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecPrim");
        var ddlFrecSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecSec");
        var ddlFrecTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecTerc");
        var txtEdadPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadPrim");
        var txtEdadSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadSec");
        var txtEdadTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadTerc");
        var GAF = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDSMVFnGlobal");

        // programa abuso de sustancias
        if (CO_Tipo.value == "1" || CO_Tipo.value == "4") {
            ddlDrogaSec.value = sustanciasList.Nousaactualmente;
            ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
            ddlViaSec.value = viaList.NoAplica;
            ddlViaTerc.value = viaList.NoAplica;
            ddlFrecSec.value = 99;
            ddlFrecTerc.value = 99;
            txtEdadSec.value = "126";
            txtEdadTerc.value = "126";
            ddlDrogaSec.disabled = true;
            ddlDrogaTerc.disabled = true;
            ddlViaSec.disabled = true;
            ddlViaTerc.disabled = true;
            ddlFrecSec.disabled = true;
            ddlFrecTerc.disabled = true;
            txtEdadSec.disabled = true;
            txtEdadTerc.disabled = true;
            //Substancia
            //GAF.disabled = true;
        }
        // programa de servicio de salud mental
        else if (CO_Tipo.value == "2" || CO_Tipo.value == "3") {
            ddlDrogaPrim.value = sustanciasList.Noaplica;
            ddlDrogaSec.value = sustanciasList.Noaplica;
            ddlDrogaSec.value = sustanciasList.Noaplica;
            ddlDrogaTerc.value = sustanciasList.Noaplica;
            ddlViaPrim.value = viaList.NoAplica;
            ddlViaSec.value = viaList.NoAplica;
            ddlViaTerc.value = viaList.NoAplica;
            ddlFrecPrim.value = 99;
            ddlFrecSec.value = 99;
            ddlFrecTerc.value = 99;
            txtEdadPrim.value = "126";
            txtEdadSec.value = "126";
            txtEdadTerc.value = "126";
            //ddlDrogaPrim.disabled = true;
            ddlDrogaSec.disabled = true;
            ddlDrogaTerc.disabled = true;
            ddlViaPrim.disabled = true;
            ddlViaSec.disabled = true;
            ddlViaTerc.disabled = true;
            ddlFrecPrim.disabled = true;
            ddlFrecSec.disabled = true;
            ddlFrecTerc.disabled = true;
            txtEdadPrim.disabled = true;
            txtEdadSec.disabled = true;
            txtEdadTerc.disabled = true;
            // Substancias 

        }
    }
    catch (ex) {// block }

    }
    AjustesNiveldeCuidado();
}



//function tabEvent(e) {
//                var keyCode = e.keyCode || e.which;
//    if (keyCode == 9) {
//        var prefix = "mainBodyContent_WucEpisodioAdmision_";
//        var inputs = [prefix + "ddlDrogaPrim"/*0*/, prefix + "ddlViaPrim"/*1*/, prefix + "ddlFrecPrim"/*2*/, prefix + "txtEdadPrim"/*3*/, prefix + "ddlToxicologia1"/*4*/,
//            prefix + "ddlDrogaSec"/*5*/, prefix + "ddlViaSec"/*6*/, prefix + "ddlFrecSec"/*7*/, prefix + "txtEdadSec"/*8*/, prefix + "ddlToxicologia2"/*9*/,
//            prefix + "ddlDrogaTerc"/*10*/, prefix + "ddlViaTerc"/*11*/, prefix + "ddlFrecTerc"/*12*/, prefix + "txtEdadTerc"/*13*/, prefix + "ddlToxicologia3"/*14*/];
//        if (e.shiftKey) {
//            switch (e.currentTarget.id) {


//                case (prefix + "ddlViaPrim"):
//                    for (var i = 0; i >= 0; i--) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;

//                case (prefix + "ddlFrecPrim"):
//                    for (var i = 1; i >= 0; i--) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;


//                case (prefix + "txtEdadPrim"):
//                    for (var i = 2; i >= 0; i--) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;

//                case (prefix + "ddlToxicologia1"):
//                    for (var i = 3; i >= 0; i--) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;

//                case (prefix + "ddlDrogaSec"):
//                    for (var i = 4; i >= 0; i--) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;



//                case (prefix + "ddlViaSec"):
//                    for (var i = 5; i >= 0; i--) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;


//                case (prefix + "ddlFrecSec"):
//                    for (var i = 6; i >= 0; i--) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;

//                case (prefix + "txtEdadSec"):
//                    for (var i = 7; i >= 0; i--) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;



//                case (prefix + "ddlToxicologia2"):
//                    for (var i = 8; i >= 0; i--) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;


//                case (prefix + "ddlDrogaTerc"):
//                    for (var i = 9; i >= 0; i--) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;


//                case (prefix + "ddlViaTerc"):
//                    for (var i = 10; i >= 0; i--) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;


//                case (prefix + "ddlFrecTerc"):
//                    for (var i = 11; i >= 0; i--) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;


//                case (prefix + "txtEdadTerc"):
//                    for (var i = 12; i >= 0; i--) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;


//                case (prefix + "ddlToxicologia3"):
//                    for (var i = 13; i >= 0; i--) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;


//                default: break;
//            }
//        }
//        else {
//            switch (e.currentTarget.id) {



//                case (prefix + "ddlDrogaPrim"):
//                    for (var i = 1; i <= 14; i++) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;

//                        }
//                    }

//                    break;


//                case (prefix + "ddlViaPrim"):
//                    for (var i = 2; i <= 14; i++) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;

//                case (prefix + "ddlFrecPrim"):
//                    for (var i = 3; i <= 14; i++) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;


//                case (prefix + "txtEdadPrim"):
//                    for (var i = 4; i <= 14; i++) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;


//                case (prefix + "ddlToxicologia1"):
//                    for (var i = 5; i <= 14; i++) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;


//                case (prefix + "ddlDrogaSec"):
//                    for (var i = 6; i <= 14; i++) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;


//                 case (prefix + "ddlViaSec"):
//                    for (var i = 7; i <= 14; i++) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;

//                case (prefix + "ddlFrecSec"):
//                    for (var i = 8; i <= 14; i++) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;


//                case (prefix + "txtEdadSec"):
//                    for (var i = 9; i <= 14; i++) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;

//                case (prefix + "ddlToxicologia2"):
//                    for (var i = 10; i <= 14; i++) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;


//                case (prefix + "ddlDrogaTerc"):
//                    for (var i = 11; i <= 14; i++) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;


//                case (prefix + "ddlViaTerc"):
//                    for (var i = 12; i <= 14; i++) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;


//                case (prefix + "ddlFrecTerc"):
//                    for (var i = 13; i <= 14; i++) {
//                        if ($("#" + inputs[i]).is(':enabled')) {
//                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
//                        }
//                    }
//                    break;

//                case (prefix + "txtEdadTerc"):
//                        if ($("#" + inputs[14]).is(':enabled')) {
//                            $("#" + inputs[14]).focus(); e.preventDefault(); return;

//                    }
//                    break;



//                    break;


//                default: break;
//            }
//        }
//    }
//}








/**
 * File: UIFlujo.js
 * Fecha: 5/MAR/2021
 * Editado por: Jose A. Ramos De La Cruz
 * Proposito: Manejar los eventos del tab index relacionados a la ventana de Abuso de sustancias
 * flag:boolean - identifica el tipo de accion a ejecutarce
 * arr: arr[String] - contiene el nombres de los controles
 * i:int - indice del control en el arreglo inicializado
 * 
 */

function TabFocusAbusoDeSustancia(flag, arr, i, e) {
    if (flag) {
        for (var j = i; j <= 14; j++) {
            if ($("#" + arr[j]).is(':enabled')) {
                $("#" + arr[j]).focus(); e.preventDefault(); return;
            }
        }

        document.getElementById("mainBodyContent_WucDatosAdmision_txtComentarios").focus(); e.preventDefault();
    } else {
        for (var j = i; j >= 0; j--) {
            if ($("#" + arr[j]).is(':enabled')) {
                $("#" + arr[j]).focus(); e.preventDefault(); return;
            }
        }

    }

}

/**
 * File: UIFlujo.js
 * Fecha: 5/MAR/2021
 * Editado por: Jose A. Ramos De La Cruz
 * Cambios:
 * 1)Se anadieron los campos ddlToxicologia1,ddlToxicologia2,ddlToxicologia3
 * 2)Se creo metodo generico para manejar los eventos relacionados al tab
 *   con el proposito de eliminar codigo repetitivo que contenia la funcion
 *
 */

function tabEvent(e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode == 9) {
        var prefix = "mainBodyContent_WucEpisodioAdmision_";
        var inputs = [prefix + "ddlDrogaPrim"/*0*/, prefix + "ddlViaPrim"/*1*/, prefix + "ddlFrecPrim"/*2*/, prefix + "txtEdadPrim"/*3*/, prefix + "ddlToxicologia1"/*4*/,
        prefix + "ddlDrogaSec"/*5*/, prefix + "ddlViaSec"/*6*/, prefix + "ddlFrecSec"/*7*/, prefix + "txtEdadSec"/*8*/, prefix + "ddlToxicologia2"/*9*/,
        prefix + "ddlDrogaTerc"/*10*/, prefix + "ddlViaTerc"/*11*/, prefix + "ddlFrecTerc"/*12*/, prefix + "txtEdadTerc"/*13*/, prefix + "ddlToxicologia3"/*14*/];
        if (e.shiftKey) {
            switch (e.currentTarget.id) {
                case (prefix + "ddlViaPrim"):
                    TabFocusAbusoDeSustancia(false, inputs, 0, e);
                    break;

                case (prefix + "ddlFrecPrim"):
                    TabFocusAbusoDeSustancia(false, inputs, 1, e);
                    break;
                case (prefix + "txtEdadPrim"):
                    TabFocusAbusoDeSustancia(false, inputs, 2, e);
                    break;
                case (prefix + "ddlToxicologia1"):
                    TabFocusAbusoDeSustancia(false, inputs, 3, e);
                    break;
                case (prefix + "ddlDrogaSec"):
                    TabFocusAbusoDeSustancia(false, inputs, 4, e);
                    break;
                case (prefix + "ddlViaSec"):
                    TabFocusAbusoDeSustancia(false, inputs, 5, e);
                    break;
                case (prefix + "ddlFrecSec"):
                    TabFocusAbusoDeSustancia(false, inputs, 6, e);
                    break;
                case (prefix + "txtEdadSec"):
                    TabFocusAbusoDeSustancia(false, inputs, 7, e);
                    break;
                case (prefix + "ddlToxicologia2"):
                    TabFocusAbusoDeSustancia(false, inputs, 8, e);
                    break;
                case (prefix + "ddlDrogaTerc"):
                    TabFocusAbusoDeSustancia(false, inputs, 9, e);
                    break;
                case (prefix + "ddlViaTerc"):
                    TabFocusAbusoDeSustancia(false, inputs, 10, e);
                    break;
                case (prefix + "ddlFrecTerc"):
                    TabFocusAbusoDeSustancia(false, inputs, 11, e);
                    break;
                case (prefix + "txtEdadTerc"):
                    TabFocusAbusoDeSustancia(false, inputs, 12, e);
                    break;
                case (prefix + "ddlToxicologia3"):
                    TabFocusAbusoDeSustancia(false, inputs, 13, e);
                    break;
                default: break;
            }
        }
        else {
            switch (e.currentTarget.id) {
                case (prefix + "ddlDrogaPrim"):
                    var div = document.getElementById("mainBodyContent_WucEpisodioAdmision_Hogar_DIV");

                    if (div.style.visibility == 'hidden') {
                        TabFocusAbusoDeSustancia(true, inputs, 1, e);
                    }
                    else {
                        $('#mainBodyContent_WucDatosAdmision_txtDrogaPrim').focus();
                    }

                    break;
                case (prefix + "txtDrogaPrim"):
                    TabFocusAbusoDeSustancia(true, inputs, 1, e);
                    break;

                case (prefix + "ddlViaPrim"):
                    TabFocusAbusoDeSustancia(true, inputs, 2, e);
                    break;
                case (prefix + "ddlFrecPrim"):
                    TabFocusAbusoDeSustancia(true, inputs, 3, e);
                    break;
                case (prefix + "txtEdadPrim"):
                    TabFocusAbusoDeSustancia(true, inputs, 4, e);
                    break;
                case (prefix + "ddlToxicologia1"):
                    TabFocusAbusoDeSustancia(true, inputs, 5, e);
                    break;
                case (prefix + "ddlDrogaSec"):
                    var div = document.getElementById("mainBodyContent_WucEpisodioAdmision_Hogar2_DIV");

                    if (div.style.visibility == 'hidden') {
                        TabFocusAbusoDeSustancia(true, inputs, 6, e);
                    }
                    else {
                        $('#mainBodyContent_WucDatosAdmision_txtDrogaSec').focus();
                    }

                    break;
                case (prefix + "txtDrogaSec"):
                    TabFocusAbusoDeSustancia(true, inputs, 6, e);
                    break;
                case (prefix + "ddlViaSec"):
                    TabFocusAbusoDeSustancia(true, inputs, 7, e);
                    break;
                case (prefix + "ddlFrecSec"):

                    TabFocusAbusoDeSustancia(true, inputs, 8, e);
                    break;
                case (prefix + "txtEdadSec"):
                    TabFocusAbusoDeSustancia(true, inputs, 9, e);
                    break;
                case (prefix + "ddlToxicologia2"):
                    TabFocusAbusoDeSustancia(true, inputs, 10, e);
                    break;

                case (prefix + "ddlDrogaTerc"):
                    var div = document.getElementById("mainBodyContent_WucEpisodioAdmision_Hogar3_DIV");

                    if (div.style.visibility == 'hidden') {
                        TabFocusAbusoDeSustancia(true, inputs, 11, e);
                    }
                    else {
                        $('#mainBodyContent_WucDatosAdmision_txtDrogaTerc').focus();
                    }

                    break;


                case (prefix + "txtDrogaTerc"):
                    TabFocusAbusoDeSustancia(true, inputs, 11, e);
                    break;
                case (prefix + "ddlViaTerc"):
                    TabFocusAbusoDeSustancia(true, inputs, 12, e);
                    break;
                case (prefix + "ddlFrecTerc"):
                    TabFocusAbusoDeSustancia(true, inputs, 13, e);
                    break;
                case (prefix + "txtEdadTerc"):
                    TabFocusAbusoDeSustancia(true, inputs, 14, e);
                    break;
                case (prefix + "ddlToxicologia3"):
                    document.getElementById("mainBodyContent_WucDatosAdmision_txtComentarios").focus(); e.preventDefault();
                    break;

                default: break;
            }
        }
    }
}


function changeTabOrder() {
    try {
        var prefix = "#mainBodyContent_WucEpisodioAdmision_";
        $(prefix + "txtDSMVOtrasObs").on('keydown', function (e) { if (e.keyCode == 9) { document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDSMVDiagDual").focus(); e.preventDefault(); } });

        $(prefix + "ddlDSMVDiagDual").on('keydown', function (e) { if (e.keyCode == 9) { document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlInFumado").focus(); e.preventDefault(); } });



        //$(prefix + "ddlDSMVDiagDual").on('keydown', function (e) {

        //    if (e.keyCode == 9 && $(prefix + "ddlDrogaPrim").is(':enabled')) { document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaPrim").focus(); e.preventDefault(); }
        //    else if (e.keyCode == 9 && !$(prefix + "ddlDrogaPrim").is(':enabled')) {
        //        document.getElementById("mainBodyContent_WucDatosAdmision_txtComentarios").focus(); e.preventDefault();
        //    }
        //});


        $(prefix + "ddlDrogaPrim").on('keydown', function (e) { tabEvent(e) });
        $(prefix + "ddlDrogaSec").on('keydown', function (e) { tabEvent(e) });
        $(prefix + "ddlDrogaTerc").on('keydown', function (e) { tabEvent(e) });
        $(prefix + "ddlViaPrim").on('keydown', function (e) { tabEvent(e) });
        $(prefix + "ddlViaSec").on('keydown', function (e) { tabEvent(e) });
        $(prefix + "ddlViaTerc").on('keydown', function (e) { tabEvent(e) });
        $(prefix + "ddlFrecPrim").on('keydown', function (e) { tabEvent(e) });
        $(prefix + "ddlFrecSec").on('keydown', function (e) { tabEvent(e) });
        $(prefix + "ddlFrecTerc").on('keydown', function (e) { tabEvent(e) });
        $(prefix + "txtEdadPrim").on('keydown', function (e) { tabEvent(e) });
        $(prefix + "txtEdadSec").on('keydown', function (e) { tabEvent(e) });
        $(prefix + "txtEdadTerc").on('keydown', function (e) { tabEvent(e) });
        $(prefix + "ddlToxicologia1").on('keydown', function (e) { tabEvent(e) });
        $(prefix + "ddlToxicologia2").on('keydown', function (e) { tabEvent(e) });
        $(prefix + "ddlToxicologia3").on('keydown', function (e) { tabEvent(e) });
        $(prefix + "txtDrogaPrim").on('keydown', function (e) { tabEvent(e) });
        $(prefix + "txtDrogaSec").on('keydown', function (e) { tabEvent(e) });
        $(prefix + "txtDrogaTerc").on('keydown', function (e) { tabEvent(e) });
    }
    catch (ex) {// catch block 

    }
}
function frmActionModeSetup() {
    try {
        var frmActionMode = document.getElementById("frmActionMode");
        if (frmActionMode.value == "read") {
            $(".SEPSDivs").addClass("SEPSDivsInfo");
            $(".SEPSDivs").removeClass("SEPSDivs");
        }
    }
    catch (ex) { }
}

//modifique la funcion y anadi el parametro de filtro
function showDSMV(filtro, txtDescripcion, txtDescripcionHidden, tipoDescripcion) {
    try {
        var ClinPrim = document.getElementById('mainBodyContent_WucEpisodioAdmision_hDSMVClinPrim').value;

        var ClinSec = document.getElementById('mainBodyContent_WucEpisodioAdmision_hDSMVClinSec').value;




        //var RMPrim = document.getElementById('mainBodyContent_WucEpisodioAdmision_hDSMVRMPrim').value;
        //var RMSec = document.getElementById('mainBodyContent_WucEpisodioAdmision_hDSMVRMSec').value;

        if (txtDescripcion == "mainBodyContent_WucEpisodioAdmision_txtDSMVClinSec") {
            if (ClinPrim == '761') {
                alert("Debe seleccionar un diagnóstico primario válido");
                return;
            }
        }
        else if (txtDescripcion == "mainBodyContent_WucEpisodioAdmision_txtDSMVClinTer") {
            if (ClinSec == '761') {
                alert("Debe seleccionar un diagnóstico secundario válido");
                return;
            }
        }
        //if (txtDescripcion == "mainBodyContent_WucEpisodioAdmision_txtDSMVRMSec") {
        //    if (RMPrim == '761') {
        //        alert("Debe seleccionar un diagnóstico primario válido");
        //        return;
        //    }
        //}
        //else if (txtDescripcion == "mainBodyContent_WucEpisodioAdmision_txtDSMVRMTer") {
        //    if (RMSec == '761') {
        //        alert("Debe seleccionar un diagnóstico secundario válido");
        //        return;
        //    }
        //}

        //Agrege el txtFiltro en el URL
        var url = 'frmdsmi_v.aspx?' + 'txtfiltro=' + filtro + '&txtDescripcion=' + txtDescripcion + '&txtDescripcionHidden=' + txtDescripcionHidden + '&tipoDescripcion=' + tipoDescripcion;
        var ventana = window.open(url, "list", "width=620,height=280,resizable=yes,toolbar=no,status=no,menubar=no");

    }
    catch (ex) { }
}



function showSusDSMV(filtro, txtDescripcion, txtDescripcionHidden, tipoDescripcion) {

    try {
        var ClinPrim = document.getElementById('mainBodyContent_WucEpisodioAdmision_hDSMVSusPrim').value;
        var ClinSec = document.getElementById('mainBodyContent_WucEpisodioAdmision_hDSMVSusSec').value;
        if (txtDescripcion == "mainBodyContent_WucEpisodioAdmision_txtDSMVSusSec") {
            if (ClinPrim == '761') {
                alert("Debe seleccionar un diagnóstico primario válido");
                return;
            }
        }
        else if (txtDescripcion == "mainBodyContent_WucEpisodioAdmision_txtDSMVSusTer") {
            if (ClinSec == '761') {
                alert("Debe seleccionar un diagnóstico secundario válido");
                return;
            }
        }

        var url = 'frmdsmi_v.aspx?' + 'txtfiltro=' + filtro + '&txtDescripcion=' + txtDescripcion + '&txtDescripcionHidden=' + txtDescripcionHidden + '&tipoDescripcion=' + tipoDescripcion;
        var ventana = window.open(url, "list", "width=620,height=280,resizable=yes,toolbar=no,status=no,menubar=no");
    }
    catch (ex) { }
}

function typeDSMV() {
    var tipo = document.getElementById("txtFiltroTipo");

    if (tipo.value == "SUST") {

        try {
            var lbx = document.getElementById("lbxDSMV");
            var txtDescripcion = document.getElementById("txtDescripcion");
            var txtDescripcionHidden = document.getElementById("txtDescripcionHidden");
            var tipoDescripcion = document.getElementById("tipoDescripcion");

            var txtDescripcionPadre = window.opener.document.getElementById(txtDescripcion.value);
            var txtDescripcionHiddenPadre = window.opener.document.getElementById(txtDescripcionHidden.value);
            /* Codigo añadido para obtener los valores de los demas diagnosticos y poder compararlos */
            /* Transtornos Clinicos */
            var ClinHD = window.opener.document.getElementById(document.getElementById("ClinHD").value);
            var ClinTxt1 = document.getElementById("ClinTxt1");
            var ClinHD1 = document.getElementById("ClinHD1");
            var ClinTxt2 = document.getElementById("ClinTxt2");
            var ClinHD2 = document.getElementById("ClinHD2");
            var ClinTxt1Padre = window.opener.document.getElementById(ClinTxt1.value);
            var ClinHD1Padre = window.opener.document.getElementById(ClinHD1.value);
            var ClinTxt2Padre = window.opener.document.getElementById(ClinTxt2.value);
            var ClinHD2Padre = window.opener.document.getElementById(ClinHD2.value);

            /* Transtornos de Personalidad y RM */
            var RMHD = window.opener.document.getElementById(document.getElementById("RMHD").value);
            var RMTxt1 = document.getElementById("RMTxt1");
            var RMHD1 = document.getElementById("RMHD1");
            var RMTxt2 = document.getElementById("RMTxt2");
            var RMHD2 = document.getElementById("RMHD2");
            var RMTxt1Padre = window.opener.document.getElementById(RMTxt1.value);
            var RMHD1Padre = window.opener.document.getElementById(RMHD1.value);
            var RMTxt2Padre = window.opener.document.getElementById(RMTxt2.value);
            var RMHD2Padre = window.opener.document.getElementById(RMHD2.value);

            if (txtDescripcion.value == "mainBodyContent_" + tipoDescripcion.value + "_txtDSMVSusTer" && lbx.value != '761') {
                if (lbx.value == ClinHD.value) {
                    alert("El Diagnostico seleccionado es igual al primer Diagnostico seleccionado");
                    window.close();
                    return;
                }
                else if (lbx.value == ClinHD1Padre.value) {
                    alert("El Diagnostico seleccionado es igual al segundo Diagnostico seleccionado");
                    window.close();
                    return;
                }
            }
            else if (txtDescripcion.value == "mainBodyContent_" + tipoDescripcion.value + "_txtDSMVSusSec") {
                if (lbx.value == '761' && ClinHD2.value != '761') {
                    ClinHD2Padre.value = '761';
                    ClinTxt2Padre.value = "761 - NO SE RECOPILA LA INFORMACIÓN";
                }
                if (lbx.value != '761' && lbx.value == ClinHD.value) {
                    alert("El Diagnostico seleccionado es igual al primer Diagnostico seleccionado");
                    window.close();
                    return;
                }
            }
            else if (txtDescripcion.value == "mainBodyContent_" + tipoDescripcion.value + "_txtDSMVSusPrim" && lbx.value == '761' && ClinHD1.value != '761') {
                ClinHD1Padre.value = '761';
                ClinTxt1Padre.value = "761 - NO SE RECOPILA LA INFORMACIÓN";

                if (ClinHD2.value != '761') {
                    ClinHD2Padre.value = '761';
                    ClinTxt2Padre.value = "761 - NO SE RECOPILA LA INFORMACIÓN";
                }
            }

            if (txtDescripcion.value == "mainBodyContent_" + tipoDescripcion.value + "_txtDSMVRMTer" && lbx.value != '761') {
                if (lbx.value == RMHD.value) {
                    alert("El Diagnostico seleccionado es igual al primer Diagnostico seleccionado");
                    window.close();
                    return;
                }
                else if (lbx.value == RMHD1Padre.value) {
                    alert("El Diagnostico seleccionado es igual al segundo Diagnostico seleccionado");
                    window.close();
                    return;
                }
            }
            else if (txtDescripcion.value == "mainBodyContent_" + tipoDescripcion.value + "_txtDSMVRMSec") {
                if (lbx.value == '761' && RMHD2.value != '761') {
                    RMHD2Padre.value = '761';
                    RMTxt2Padre.value = "761 - NO SE RECOPILA LA INFORMACIÓN";
                }
                if (lbx.value != '761' && lbx.value == RMHD.value) {
                    alert("El Diagnostico seleccionado es igual al primer Diagnostico seleccionado");
                    window.close();
                    return;
                }
            }
            else if (txtDescripcion.value == "mainBodyContent_" + tipoDescripcion.value + "_txtDSMVRMPrim" && lbx.value == '761' && RMHD1.value != '761') {
                RMHD1Padre.value = '761';
                RMTxt1Padre.value = "761 - NO SE RECOPILA LA INFORMACIÓN";

                if (RMHD2.value != '761') {
                    RMHD2Padre.value = '761';
                    RMTxt2Padre.value = "761 - NO SE RECOPILA LA INFORMACIÓN";
                }
            }

            txtDescripcionHiddenPadre.value = lbx.value;
            txtDescripcionPadre.value = lbx[lbx.selectedIndex].text;

            window.close();

        }
        catch (ex) { }

    }
    else { DSMV(); }


}

function DSMV() {
    try {
        var lbx = document.getElementById("lbxDSMV");
        var txtDescripcion = document.getElementById("txtDescripcion");
        var txtDescripcionHidden = document.getElementById("txtDescripcionHidden");
        var tipoDescripcion = document.getElementById("tipoDescripcion");

        var txtDescripcionPadre = window.opener.document.getElementById(txtDescripcion.value);
        var txtDescripcionHiddenPadre = window.opener.document.getElementById(txtDescripcionHidden.value);
        /* Codigo añadido para obtener los valores de los demas diagnosticos y poder compararlos */
        /* Transtornos Clinicos */
        var ClinHD = window.opener.document.getElementById(document.getElementById("ClinHD").value);
        var ClinTxt1 = document.getElementById("ClinTxt1");
        var ClinHD1 = document.getElementById("ClinHD1");
        var ClinTxt2 = document.getElementById("ClinTxt2");
        var ClinHD2 = document.getElementById("ClinHD2");
        var ClinTxt1Padre = window.opener.document.getElementById(ClinTxt1.value);
        var ClinHD1Padre = window.opener.document.getElementById(ClinHD1.value);
        var ClinTxt2Padre = window.opener.document.getElementById(ClinTxt2.value);
        var ClinHD2Padre = window.opener.document.getElementById(ClinHD2.value);

        /* Transtornos de Personalidad y RM */
        var RMHD = window.opener.document.getElementById(document.getElementById("RMHD").value);
        var RMTxt1 = document.getElementById("RMTxt1");
        var RMHD1 = document.getElementById("RMHD1");
        var RMTxt2 = document.getElementById("RMTxt2");
        var RMHD2 = document.getElementById("RMHD2");
        var RMTxt1Padre = window.opener.document.getElementById(RMTxt1.value);
        var RMHD1Padre = window.opener.document.getElementById(RMHD1.value);
        var RMTxt2Padre = window.opener.document.getElementById(RMTxt2.value);
        var RMHD2Padre = window.opener.document.getElementById(RMHD2.value);

        if (txtDescripcion.value == "mainBodyContent_" + tipoDescripcion.value + "_txtDSMVClinTer" && lbx.value != '761') {
            if (lbx.value == ClinHD.value) {
                alert("El Diagnostico seleccionado es igual al primer Diagnostico seleccionado");
                window.close();
                return;
            }
            else if (lbx.value == ClinHD1Padre.value) {
                alert("El Diagnostico seleccionado es igual al segundo Diagnostico seleccionado");
                window.close();
                return;
            }
        }
        else if (txtDescripcion.value == "mainBodyContent_" + tipoDescripcion.value + "_txtDSMVClinSec") {
            if (lbx.value == '761' && ClinHD2.value != '761') {
                ClinHD2Padre.value = '761';
                ClinTxt2Padre.value = "761 - NO SE RECOPILA LA INFORMACIÓN";
            }
            if (lbx.value != '761' && lbx.value == ClinHD.value) {
                alert("El Diagnostico seleccionado es igual al primer Diagnostico seleccionado");
                window.close();
                return;
            }
        }
        else if (txtDescripcion.value == "mainBodyContent_" + tipoDescripcion.value + "_txtDSMVClinPrim" && lbx.value == '761' && ClinHD1.value != '761') {
            ClinHD1Padre.value = '761';
            ClinTxt1Padre.value = "761 - NO SE RECOPILA LA INFORMACIÓN";

            if (ClinHD2.value != '761') {
                ClinHD2Padre.value = '761';
                ClinTxt2Padre.value = "761 - NO SE RECOPILA LA INFORMACIÓN";
            }
        }

        if (txtDescripcion.value == "mainBodyContent_" + tipoDescripcion.value + "_txtDSMVRMTer" && lbx.value != '761') {
            if (lbx.value == RMHD.value) {
                alert("El Diagnostico seleccionado es igual al primer Diagnostico seleccionado");
                window.close();
                return;
            }
            else if (lbx.value == RMHD1Padre.value) {
                alert("El Diagnostico seleccionado es igual al segundo Diagnostico seleccionado");
                window.close();
                return;
            }
        }
        else if (txtDescripcion.value == "mainBodyContent_" + tipoDescripcion.value + "_txtDSMVRMSec") {
            if (lbx.value == '761' && RMHD2.value != '761') {
                RMHD2Padre.value = '761';
                RMTxt2Padre.value = "761 - NO SE RECOPILA LA INFORMACIÓN";
            }
            if (lbx.value != '761' && lbx.value == RMHD.value) {
                alert("El Diagnostico seleccionado es igual al primer Diagnostico seleccionado");
                window.close();
                return;
            }
        }
        else if (txtDescripcion.value == "mainBodyContent_" + tipoDescripcion.value + "_txtDSMVRMPrim" && lbx.value == '761' && RMHD1.value != '761') {
            RMHD1Padre.value = '761';
            RMTxt1Padre.value = "761 - NO SE RECOPILA LA INFORMACIÓN";

            if (RMHD2.value != '761') {
                RMHD2Padre.value = '761';
                RMTxt2Padre.value = "761 - NO SE RECOPILA LA INFORMACIÓN";
            }
        }

        txtDescripcionHiddenPadre.value = lbx.value;
        txtDescripcionPadre.value = lbx[lbx.selectedIndex].text;

        window.close();

    }
    catch (ex) { }
}
function dsmivShowHideClick() {
    var showContentButton = document.getElementById("dsmiv_showContentButton");
    if (showContentButton.getAttribute("aria-expanded") == "false") {
        showContentButton.innerText = "Esconder contenido";
    }
    else {
        showContentButton.innerText = "Mostrar contenido";
    }
}

function ddlPreviosSustancias() {
    try {
        var ddlPreviosSustancias = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlPreviosSustancias");
        var ddlUltSustancias = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlUltSustancias");
        var ddlNivelSustancias = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelSustancias");
        var txtDíasSustUlt = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDíasSustUlt");
        var txtMesesSustUlt = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtMesesSustUlt");
        var ddlNivel = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelSustancias");
        var nivelItem = document.createElement("OPTION");

        nivelItem.text = "No aplica (Este episodio es de Salud Mental)";
        nivelItem.value = 99;

        switch (ddlPreviosSustancias.value) {
            case ("1"): case ("99"): case ("97"):
                for (var i = ddlNivel.options.length - 1; i >= 0; i--) {
                    if (ddlNivel.options[i].value == 99) {
                        continue;
                    }
                    else if (i == 0) ddlNivel.add(nivelItem);
                }
                ddlUltSustancias.value = 99;
                ddlNivelSustancias.value = 99;
                txtDíasSustUlt.value = "0";
                txtMesesSustUlt.value = "0";
                ddlUltSustancias.disabled = true;
                ddlNivelSustancias.disabled = true;
                txtDíasSustUlt.disabled = true;
                txtMesesSustUlt.disabled = true;
                break;
            default:
                if (ddlUltSustancias.value == "99") {
                    ddlUltSustancias.value = "0";
                }

                for (var i = ddlNivel.options.length - 1; i >= 0; i--) {
                    if (ddlNivel.options[i].value == 99) {
                        ddlNivel.remove(i);
                    }
                }

                ddlUltSustancias.disabled = false;
                ddlNivelSustancias.disabled = false;
                txtDíasSustUlt.disabled = false;
                txtMesesSustUlt.disabled = false;
                break;
        }
    }
    catch (ex) { }
}
function ddlPreviosMental() {
    try {
        var ddlPreviosMental = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlPreviosMental");
        var ddlUltMental = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlUltMental");
        var ddlNivelMental = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelMental");
        var txtDíasMentUlt = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDíasMentUlt");
        var txtMesesMentUlt = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtMesesMentUlt");
        var ddlNivel = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelMental");
        var nivelItem = document.createElement("OPTION");

        nivelItem.text = "No aplica (Este episodio es de use de sustancias)";
        nivelItem.value = 99;

        switch (ddlPreviosMental.value) {
            case ("1"): case ("99"): case ("97"):

                for (var i = ddlNivel.options.length - 1; i >= 0; i--) {
                    if (ddlNivel.options[i].value == 99) {
                        continue;
                    }
                    else if (i == 0) ddlNivel.add(nivelItem);
                }
                ddlUltMental.value = 99;
                ddlNivelMental.value = 99;
                txtDíasMentUlt.value = "0";
                txtMesesMentUlt.value = "0";
                ddlUltMental.disabled = true;
                ddlNivelMental.disabled = true;
                txtDíasMentUlt.disabled = true;
                txtMesesMentUlt.disabled = true;
                break;
            default:
                if (ddlUltMental.value == "99") {
                    ddlUltMental.value = "0";
                }

                for (var i = ddlNivel.options.length - 1; i >= 0; i--) {
                    if (ddlNivel.options[i].value == 99) {
                        ddlNivel.remove(i);
                    }
                }
                ddlUltMental.disabled = false;
                ddlNivelMental.disabled = false;
                txtDíasMentUlt.disabled = false;
                txtMesesMentUlt.disabled = false;
                break;
        }
    }
    catch (ex) { }
}
function ddlUltMental() {
    try {
        var ddlPreviosMental = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlPreviosMental");
        var días = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDíasMentUlt");
        var meses = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtMesesMentUlt");
        var ddlUlt = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlUltMental");
        switch (ddlUlt.value) {
            case ("0")://Empty string
            case ("1")://Menos de un mes (30 días)
            case ("2")://1 a 3 meses
            case ("3")://4 a 6 meses
            case ("4")://7 a 11 meses
            case ("5")://1 año a 2 años
            case ("6")://3 a 4 años
            case ("7")://5 a 6 años
            case ("8")://7 años o más
                días.disabled = false;
                meses.disabled = false;
                break;
            case ("95")://No información
                días.value = 0;
                meses.value = 0;
                días.disabled = true;
                meses.disabled = true;
                break;
            case ("99")://No aplica
                if (ddlPreviosMental.value != "1") {
                    if (ddlPreviosMental.value == "0") {
                        alert("Seleccione primero si el paciente tiene episodios previos al tratamiento.");
                    }
                    else {
                        alert("No puede escoger 'No aplica', ya que en la pregunta anterior escogió que el paciente ha tenido episodios o recibió tratamiento anteriormente. El paciente tiene episodios anteriores. No puede escoger NO APLICA.");
                    }
                    ddlUlt.value = "0";
                }
                else {
                    días.value = 0;
                    meses.value = 0;
                    días.disabled = true;
                    meses.disabled = true;
                }
                break;
            default: break;
        }
    }
    catch (ex) { }
}
function ddlUltSustancias() {
    try {
        var ddlPreviosSustancias = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlPreviosSustancias");
        var días = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDíasSustUlt");
        var meses = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtMesesSustUlt");
        var ddlUlt = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlUltSustancias");
        switch (ddlUlt.value) {
            case ("0")://Empty string
            case ("1")://Menos de un mes (30 días)
            case ("2")://1 a 3 meses
            case ("3")://4 a 6 meses
            case ("4")://7 a 11 meses
            case ("5")://1 año a 2 años
            case ("6")://3 a 4 años
            case ("7")://5 a 6 años
            case ("8")://7 años o más
                días.disabled = false;
                meses.disabled = false;
                break;
            case ("95")://No información
                días.value = 0;
                meses.value = 0;
                días.disabled = true;
                meses.disabled = true;
                break;
            case ("99")://No aplica
                if (ddlPreviosSustancias.value != "1") {
                    if (ddlPreviosSustancias.value == "0") {
                        alert("Seleccione primero si el paciente tiene episodios previos al tratamiento.");
                    }
                    else {
                        alert("No puede escoger 'No aplica', ya que en la pregunta anterior escogió que el paciente ha tenido episodios o recibió tratamiento anteriormente.");
                    }
                    ddlUlt.value = "0";
                }
                else {
                    días.value = 0;
                    meses.value = 0;
                    días.disabled = true;
                    meses.disabled = true;
                }
                break;
            default: break;
        }
    }
    catch (ex) { //  catch 
    }
}
function AccionUpdate() {
    try {
        var ddlNivelCuidadoSaludMental = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSaludMental");
        var ddlNivelCuidadoSustancias = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSustancias");

        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaPrim");
        var ddlDrogaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaSec");
        var ddlDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaTerc");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaPrim");
        var ddlViaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaSec");
        var ddlViaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaTerc");
        var ddlFrecPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecPrim");
        var ddlFrecSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecSec");
        var ddlFrecTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecTerc");
        var txtEdadPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadPrim");
        var txtEdadSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadSec");
        var txtEdadTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadTerc");
        var GAF = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDSMVFnGlobal");

        if (ddlNivelCuidadoSaludMental.value !== "99" && ddlNivelCuidadoSustancias.value === "99") {
            //alert("saludmental");
            ddlDrogaPrim.disabled = false;
            ddlDrogaSec.disabled = true;
            ddlDrogaTerc.disabled = true;
            ddlViaPrim.disabled = false;
            ddlViaSec.disabled = true;
            ddlViaTerc.disabled = true;
            ddlFrecPrim.disabled = false;
            ddlFrecSec.disabled = true;
            ddlFrecTerc.disabled = true;
            txtEdadPrim.disabled = false;
            txtEdadSec.disabled = true;
            txtEdadTerc.disabled = true;
            //GAF.disabled = true;
        }
        else if (ddlNivelCuidadoSustancias.value !== "99" && ddlNivelCuidadoSaludMental.value === "99") {
            //alert("sustancia");
            ddlDrogaPrim.disabled = false;
            ddlDrogaSec.disabled = false;
            ddlDrogaTerc.disabled = false;
            ddlViaPrim.disabled = false;
            ddlViaSec.disabled = false;
            ddlViaTerc.disabled = false;
            ddlFrecPrim.disabled = false;
            ddlFrecSec.disabled = false;
            ddlFrecTerc.disabled = false;
            txtEdadPrim.disabled = false;
            txtEdadSec.disabled = false;
            txtEdadTerc.disabled = false;
        }
    }
    catch (e) {
        alert(e.text);
    }
}
function AjustesNiveldeCuidado() {


    try {
        var ddlNivelCuidadoSaludMental = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSaludMental");
        var ddlNivelCuidadoSustancias = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSustancias");
        var ddlDSMVDiagDual = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDSMVDiagDual");


        // se aplica la regla abuso de sutancia 
        if (ddlNivelCuidadoSustancias.value == "99" && ddlNivelCuidadoSaludMental.value == "99") return;
        // SI NO SE HA SELECCIONADO NIVEL DE CUIDADO GOBIERNA CO_TIPO


        var CO_Tipo = document.getElementById("mainBodyContent_WucEpisodioAdmision_CO_Tipo");
        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaPrim");
        var ddlDrogaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaSec");
        var ddlDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaTerc");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaPrim");
        var ddlViaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaSec");
        var ddlViaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaTerc");
        var ddlFrecPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecPrim");
        var ddlFrecSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecSec");
        var ddlFrecTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecTerc");
        var txtEdadPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadPrim");
        var txtEdadSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadSec");
        var txtEdadTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadTerc");
        var GAF = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDSMVFnGlobal");
        var codependiente = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlCodependiente");

        //var txtDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDrogaPrim");
        //txtDrogaPrim.style.visibility = "hidden";

        var hogarDiv = document.getElementById("mainBodyContent_WucEpisodioAdmision_Hogar_DIV");
        hogarDiv.style.visibility = "hidden";

        var hogar2Div = document.getElementById("mainBodyContent_WucEpisodioAdmision_Hogar2_DIV");
        hogar2Div.style.visibility = "hidden";

        var hogar3Div = document.getElementById("mainBodyContent_WucEpisodioAdmision_Hogar3_DIV");
        hogar3Div.style.visibility = "hidden";

        var ddlToxicologia1 = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlToxicologia1");
        var ddlToxicologia2 = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlToxicologia2");
        var ddlToxicologia3 = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlToxicologia3");

        // Si usuario selecciona opción de “Nivel de Cuidado (Salud Mental)”
        if (ddlNivelCuidadoSaludMental.value != "99") {


            ddlDrogaPrim.value = sustanciasList.Noaplica;
            ddlDrogaSec.value = sustanciasList.Noaplica;
            ddlDrogaTerc.value = sustanciasList.Noaplica;
            ddlViaPrim.value = viaList.NoAplica;
            ddlViaSec.value = viaList.NoAplica;
            ddlViaTerc.value = viaList.NoAplica;

            ddlFrecPrim.value = 99;
            ddlFrecSec.value = 99;
            ddlFrecTerc.value = 99;
            txtEdadPrim.value = "126";
            txtEdadSec.value = "126";
            txtEdadTerc.value = "126";

            ddlToxicologia1.value = "99";
            ddlToxicologia2.value = "99";
            ddlToxicologia3.value = "99";

            //ddlDrogaPrim.disabled = true;
            ddlDrogaSec.disabled = true;
            ddlDrogaTerc.disabled = true;
            //ddlViaPrim.disabled = true;
            ddlViaSec.disabled = true;
            ddlViaTerc.disabled = true;
            //ddlFrecPrim.disabled = true;
            ddlFrecSec.disabled = true;
            ddlFrecTerc.disabled = true;
            //txtEdadPrim.disabled = true;
            txtEdadSec.disabled = true;
            txtEdadTerc.disabled = true;
            //GAF.disabled = false;

            codependiente.value = 2;
            codependiente.disabled = true;

            ddlToxicologia1.disabled = true;
            ddlToxicologia2.disabled = true;
            ddlToxicologia3.disabled = true;

        }
        else {

            ddlDrogaPrim.value = sustanciasList.NoSeleccionado;
            ddlDrogaSec.value = sustanciasList.Nousaactualmente;
            ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
            ddlViaPrim.value = viaList.NoSeleccionado;
            ddlViaSec.value = viaList.NoAplica;
            ddlViaTerc.value = viaList.NoAplica;

            ddlFrecPrim.value = 0;
            ddlFrecSec.value = 99;
            ddlFrecTerc.value = 99;
            txtEdadPrim.value = "";
            txtEdadSec.value = "126";
            txtEdadTerc.value = "126";

            ddlToxicologia1.value = "";
            ddlToxicologia2.value = "99";
            ddlToxicologia3.value = "99";

            ddlDrogaPrim.disabled = false;
            ddlDrogaSec.disabled = true;
            ddlDrogaTerc.disabled = true;
            ddlViaPrim.disabled = false;
            ddlViaSec.disabled = true;
            ddlViaTerc.disabled = true;
            ddlFrecPrim.disabled = false;
            ddlFrecSec.disabled = true;
            ddlFrecTerc.disabled = true;
            txtEdadPrim.disabled = false;
            txtEdadSec.disabled = true;
            txtEdadTerc.disabled = true;

            codependiente.value = 0;
            codependiente.disabled = false;

            ddlToxicologia1.disabled = false;
            ddlToxicologia2.disabled = true;
            ddlToxicologia3.disabled = true;


        }


    }


    catch (e) {
        // alert(ex.text);
    }

}

function ddlNivelCuidadoSustancias() {



    try {
        var txtDíasSustancias = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDíasSustancias");
        var nivelS = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSustancias");
        var nivelM = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSaludMental");
        var opiod = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlMetadona");
        switch (nivelS.value) {
            case ("99")://No aplica
                txtDíasSustancias.value = "0";
                txtDíasSustancias.disabled = true;
                opiod.value = "4";
                //opiod.disabled = true;
                break;
            default:
                if (opiod.value === "4") {
                    opiod.value = "0";
                }
                //opiod.disabled = false;
                txtDíasSustancias.disabled = false;
                break;
        }
        if (nivelS.value != "0" && nivelS.value != "99") {
            nivelM.value = "99";
            nivelM.disabled = true;
        }
        else {
            nivelM.disabled = false;
        }
    }
    catch (ex) { }

    AjustesNiveldeCuidado();
}

function txtNbHogar() {

    var nivelM = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSaludMental");
    var divNbHogar = document.getElementById("mainBodyContent_WucEpisodioAdmision_divNbHogar");
    var rfvNbHogar = document.getElementById("mainBodyContent_WucEpisodioAdmision_rfvNbHogar");
    var txtNbHogar = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtNbHogar");




    switch (nivelM.value) {
        case ("24"):
        case ("25"):
        case ("26"):
        case ("33"):

            divNbHogar.style.visibility = 'visible';

            if (txtNbHogar.value == "") {
            ValidatorEnable(rfvNbHogar, true);
            }
            else {
                ValidatorEnable(rfvNbHogar, false);
            }
            

            break;

        default:
            txtNbHogar.value = "";
            divNbHogar.style.visibility = 'hidden';
            ValidatorEnable(rfvNbHogar, false);
            break;


            
            }

}

function ddlNivelCuidadoSaludMental() {


    try {
        var txtDíasMental = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDíasMental");
        var txtDíasSustancias = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDíasSustancias");
        var nivelS = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSustancias");
        var nivelM = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSaludMental");
        var opiod = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlMetadona");
        var ddlDSMVDiagDual = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDSMVDiagDual");

        switch (nivelM.value) {
            case ("99")://No aplica
                txtDíasMental.value = "0";
                txtDíasMental.disabled = true;
                break;
            default:
                //if (ddlDSMVDiagDual.value == "1") {
                //    if (opiod.value != "0" && opiod.value != "4") { }
                //    else { opiod.value = "0"; }
                //    txtDíasSustancias.disabled = false;
                //    opiod.disabled = false;
                //}
                //opiod.disabled = false;
                txtDíasMental.disabled = false;
                break;
        }
        if (nivelM.value != "0" && nivelM.value != "99") {
            nivelS.value = "99";
            nivelS.disabled = true;
        }
        else {
            nivelS.disabled = false;
        }
    }
    catch (ex) {
        //alert(ex.message); 
    }
    AjustesNiveldeCuidado();
    txtNbHogar();
}

function ddlMetadona() {
    try {
        var nivelS = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSustancias");
        var nivelM = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSaludMental");
        var opiod = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlMetadona");
        var ddlDSMVDiagDual = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDSMVDiagDual");

        if (opiod.value == "4" && nivelS.value != "99") {
            opiod.value = "0";
            alert("Tiene un Nivel de Cuidado de Abuso de sustancias valida.");
        }
        else if (opiod.value == "4" && ddlDSMVDiagDual.value == "1") {
            opiod.value = "0";
            alert("Este perfil esta seleccionado como CONCURRENTE, no puede seleccionar " + "'" + "No Aplica" + "'" + ".");
        }
    }
    catch (ex) { }
}

function ddlDSMVPsicoAmbiPrim() {
    try {
        var ddlDSMVPsicoAmbiPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDSMVPsicoAmbiPrim");
        var ddlDSMVPsicoAmbiSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDSMVPsicoAmbiSec");
        var ddlDSMVPsicoAmbiTer = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDSMVPsicoAmbiTer");
        switch (ddlDSMVPsicoAmbiPrim.value) {
            case ("99"):
                ddlDSMVPsicoAmbiSec.value = 99;
                ddlDSMVPsicoAmbiTer.value = 99;
                ddlDSMVPsicoAmbiSec.disabled = true;
                ddlDSMVPsicoAmbiTer.disabled = true;
                break;
            default:
                ddlDSMVPsicoAmbiSec.disabled = false;
                ddlDSMVPsicoAmbiTer.disabled = false;
                break;
        }
    }
    catch (ex) { }
}
function ddlDSMVPsicoAmbiSec() {
    try {
        var ddlDSMVPsicoAmbiSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDSMVPsicoAmbiSec");
        var ddlDSMVPsicoAmbiTer = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDSMVPsicoAmbiTer");
        if (ddlDSMVPsicoAmbiSec.value == 99) {
            ddlDSMVPsicoAmbiTer.value = 99;
            ddlDSMVPsicoAmbiTer.disabled = true;
        }
        else {
            ddlDSMVPsicoAmbiTer.disabled = false;
        }
    }
    catch (ex) { }
}
function ddlVaron() {
    try {
        var ddlVaron = document.getElementById("mainBodyContent_WucDatosDemograficos_ddlVaron");
        var txtNumNinos = document.getElementById("mainBodyContent_WucDatosDemograficos_txtNumNinos");
        var rvNumNinos = document.getElementById("mainBodyContent_WucDatosDemograficos_rvNumNinos");
        switch (ddlVaron.value) {
            case ("1")://SIN HIJOS
                txtNumNinos.value = '0';
                rvNumNinos.minimumvalue = '0';
                txtNumNinos.disabled = true;
                break;
            case ("2")://CON HIJOS
                if (txtNumNinos.value == '0') {
                    txtNumNinos.value = '';
                }
                rvNumNinos.minimumvalue = '1';
                txtNumNinos.disabled = false;
                break;
            case ("99"): break;//No Aplica
            default://En blanco
                rvNumNinos.minimumvalue = '0';
                txtNumNinos.disabled = false;
                break;
        }
    }
    catch (ex) { }
}
function ddlFuenteReferido() {
    try {
        var ddlFuenteReferido = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFuenteReferido");
        var ddlEstadoLegal = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlEstadoLegal");

        var nivelS = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSustancias");
        var nivelM = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSaludMental");
        /*switch (ddlFuenteReferido.value) {
            case ("25"): 
                ddlEstadoLegal.disabled = true;
                ddlEstadoLegal.value = 99;
                break;
            default:
                ddlEstadoLegal.disabled = false;
                break;
        }*/
        if (nivelS.value == "99" && nivelM.value == "99" && ddlFuenteReferido.value != 0) {
            alert("¡Debe escoger un Nivel de Cuidado!");
            ddlFuenteReferido.value = 0;
            nivelS.focus();
        }
        if (ddlFuenteReferido.value == "3" || ddlFuenteReferido.value == "9" || ddlFuenteReferido.value == "14" || ddlFuenteReferido.value == "37") {
            if (ddlEstadoLegal.value == "99" || ddlEstadoLegal.value == "") {
                ddlEstadoLegal.disabled = false;
                ddlEstadoLegal.value = 0;
            }

        }
        else {
            ddlEstadoLegal.disabled = true;
            ddlEstadoLegal.value = 99;
        }


    }
    catch (ex) { }
}
function ddlGrado() {
    try {
        var ddlGrado = document.getElementById("mainBodyContent_WucDatosDemograficos_ddlGrado");
        var ddlDesertor = document.getElementById("mainBodyContent_WucDatosDemograficos_ddlDesertorEscolar");
        var ddlEducacionEspecial = document.getElementById("mainBodyContent_WucDatosDemograficos_ddlEducacionEspecial");
        var situacion = document.getElementById("mainBodyContent_WucDatosDemograficos_ddlSituacionEscolar");

        if (document.getElementById("mainBodyContent_WucDatosDemograficos_edadAdmision").value != null &&
            document.getElementById("mainBodyContent_WucDatosDemograficos_edadAdmision").value != "") {
            var edad = parseInt(document.getElementById("mainBodyContent_WucDatosDemograficos_edadAdmision").value);
        }
        else {
            var edad = parseInt(document.getElementById("mainBodyContent_WucDatosPersonales_lblEdad").innerText);
        }
        if (edad >= 18) {
            switch (ddlGrado.value) {
                case ("12")://Diploma de escuela superior
                case ("14")://Créditos universitarios
                case ("16")://Curso vocacional
                case ("22")://Grado asociado
                case ("23")://Bachillerato
                case ("24")://Maestría
                case ("25")://Doctorado
                case ("25")://Otro (educación especial")
                    // No es desertor
                    ddlDesertor.value = 2;//No
                    ddlDesertor.disabled = true;
                    break;
                case ("96")://No informo
                    // No aplica
                    ddlDesertor.value = 99;//No aplica
                    ddlDesertor.disabled = true;
                    break;
                case ("13")://Ninguna
                case ("26")://Pre-escolar
                case ("27")://Kindergarten
                case ("1")://Primero
                case ("2")://Segundo
                case ("3")://Tercero
                case ("4")://Cuarto
                case ("5")://Quinto
                case ("6")://Sexto
                case ("7")://Séptimo
                case ("8")://Octavo
                case ("9")://Noveno
                case ("10")://Décimo
                case ("11")://Undécimo
                    //Es desertor
                    ddlDesertor.value = 1;//Sí
                    ddlDesertor.disabled = true;
                    break;
            }
        }
        else if (edad < 3) {
            ddlGrado.disabled = true;
            ddlGrado.value = "13";
            ddlDesertor.value = 99;
            ddlDesertor.disabled = true;
            ddlEducacionEspecial.value = 99;
        }

    }
    catch (ex) { }
}
function ddlCondLaboral() {
    try {

        var ddlCondLaboral = document.getElementById("mainBodyContent_WucDatosDemograficos_ddlCondLaboral");
        var ddlNoFueraLaboral = document.getElementById("mainBodyContent_WucDatosDemograficos_ddlNoFueraLaboral");
        var ddlFuenteIngreso = document.getElementById("mainBodyContent_WucDatosDemograficos_ddlFuenteIngreso");
        switch (ddlCondLaboral.value) {
            case ("5"):
                if (ddlNoFueraLaboral.disabled) {
                    ddlNoFueraLaboral.disabled = false;
                    ddlNoFueraLaboral.value = 0;
                }

                break;
            case ("1"): case ("2"):
                ddlNoFueraLaboral.value = 99;
                ddlNoFueraLaboral.disabled = true;
                ddlFuenteIngreso.value = 1;
                break;
            default:
                ddlNoFueraLaboral.value = 99;
                ddlNoFueraLaboral.disabled = true;
                ddlFuenteIngreso.disabled = false;
                break;
        }
    }
    catch (ex) { }
}


/* original
function ddlFuenteIngreso() {
    try {
        var ddlCondLaboral = document.getElementById("mainBodyContent_WucDatosDemograficos_ddlCondLaboral");
        var ddlFuenteIngreso = document.getElementById("mainBodyContent_WucDatosDemograficos_ddlFuenteIngreso");

        if ((ddlCondLaboral.value == "1" || ddlCondLaboral.value == "2") && (ddlFuenteIngreso.value != "1" || ddlFuenteIngreso.value != "9")) {
            alert("No puede escoger esta opción por su selección en el campo 'Condición laboral'. Solo puede seleccionar las opciones: 'Salario / Jornal' ó 'Negocio Propio'.");
        }
    }
    catch (ex) {}
}
*/


// modificado por: strategicconsultingpr. 27-feb-2019

function ddlFuenteIngreso() {
    try {
        var ddlCondLaboral = document.getElementById("mainBodyContent_WucDatosDemograficos_ddlCondLaboral");
        var ddlFuenteIngreso = document.getElementById("mainBodyContent_WucDatosDemograficos_ddlFuenteIngreso");
        var ValidValue = new Array("1", "2", "13");
        // 1: Salario / Jornal
        // 2: Pensión (por retiro) / seguro social
        // 13: seguro social

        if (Number(ddlCondLaboral.value) == 1 && ValidValue.indexOf(ddlFuenteIngreso.value) == -1) {
            alert("No puede escoger esta opción por su selección en el campo 'Condición laboral'.\nSolo puede seleccionar las opciones:\n'Salario / Jornal' ó \n'Negocio Propio' ó \n'Pensión (por retiro) / seguro social' .");
            ddlFuenteIngreso.selectedIndex = -1;


        }

    }
    catch (ex) {
    }
}

function ddlMaltratoNinez() {
    try {
        var año = document.getElementById("mainBodyContent_WucDatosPersonales_txtAño");
        var ddlMaltratoNinez = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlMaltratoNinez");
        if (año.value == '' || año.value == null) {
            ddlMaltratoNinez.value = "0";
            alert("Debe ingresar una 'Fecha de Admision' para poder continuar con el perfil.");
            return false;
        }
        else {
            __doPostBack('ddlMaltratoNinez', '');
            return true;
        }

    }
    catch (ex) { }
}
function ddlNoFueraLaboral() {
    try {
        var ddlNoFueraLaboral = document.getElementById("mainBodyContent_WucDatosDemograficos_ddlNoFueraLaboral");

        if (!ddlNoFueraLaboral.disable) {
            switch (ddlNoFueraLaboral.value) {
                case ("99"):
                    ddlNoFueraLaboral.value = 0;
                    alert("La opción 'No Aplica' no puede ser seleccionada, ya que seleccionó en 'Condición laboral: No participa en la fuerza laboral'");
                    break;

            }
        }

    }
    catch (ex) { }
}
function ddlEstadoLegal() {
    try {
        var ddlArrestado = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlArrestado");
        var ddlArrestado30 = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlArrestado30");
        var ddlEstadoLegal = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlEstadoLegal");
        switch (document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlEstadoLegal").value) {
            case ("1"): case ("2"): case ("3"): case ("5"): case ("6"): case ("9"): case ("11"): case ("12"): case ("14"): case ("15"): case ("22"):
                //ddlArrestado.value = 1;
                //ddlArrestado.disabled = true;
                // ddlArrestado30.disabled = false;
                //ddlArrestado();
                //ddlArrestado30();
                break;
            default:
                // ddlArrestado.disabled = false;
                //ddlArrestado.value = 0;
                // ddlArrestado();
                // ddlArrestado30();
                break;
        }

        if (!ddlEstadoLegal.disabled && ddlEstadoLegal.value == 99) {
            ddlEstadoLegal.value = 0;
            alert("La selección en Fuente del Referido requiere un referido del Estado Legal. Favor seleccionar una opción correcta");
        }
    }
    catch (ex) { }
}
function ddlEstadoLegal_Load() {
    try {
        switch (document.getElementById("hFKPrograma").value) {
            case ("27"): case ("31"): case ("32"): case ("33"): case ("34"): case ("35"): case ("36"): case ("37"): case ("38"): case ("39"): case ("40"): case ("41"): case ("42")://TASC             
                $("#mainBodyContent_WucEpisodioAdmision_ddlEstadoLegal option").filter(function () { return ["14", "15"].indexOf(this.value) < 0; }).remove();
                break;
            default: break;
        }
    }
    catch (ex) { }
}
function ddlArrestado() {
    try {
        var ddlArrestado = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlArrestado");
        var txtArrestos3O = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtArrestos30");
        var ddlArrestado3O = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlArrestado30");
        var rvArrestos3O = document.getElementById("mainBodyContent_WucEpisodioAdmision_rvArrestos30");
        switch (ddlArrestado.value) {
            case ("1")://Sí
                //ddlArrestado3O.value = 2;
                txtArrestos3O.disabled = false;
                ddlArrestado3O.disabled = false;

                //ddlArrestado30();
                break;
            case ("2"): //No
                txtArrestos3O.value = '0';
                ddlArrestado3O.value = '99';
                rvArrestos3O.minimumvalue = "0";
                ddlArrestado3O.disabled = true;
                txtArrestos3O.disabled = true;
                break;

            default:
                // ddlArrestado3O.disabled = false;
                txtArrestos3O.value = '0';
                ddlArrestado3O.value = 2;
                rvArrestos3O.minimumvalue = "0";
                ddlArrestado3O.disabled = true;
                ddlArrestado30();
                break;

        }
    }
    catch (ex) { }
}
function ddlArrestado30() {

    try {
        var ddlArrestado = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlArrestado");
        var txtArrestos30 = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtArrestos30");
        var ddlArrestado30 = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlArrestado30");
        var rvArrestos30 = document.getElementById("mainBodyContent_WucEpisodioAdmision_rvArrestos30");
        switch (ddlArrestado30.value) {
            case ("1")://Sí
                if (ddlArrestado.value == "2" || ddlArrestado.value == "") {
                    ddlArrestado30.value = "2";
                    txtArrestos30.value = "0";
                    rvArrestos30.minimumvalue = "0";
                    ddlArrestado30.disabled = true;
                    txtArrestos30.disabled = true;
                }
                else {
                    if (txtArrestos30.value == "0") {
                        txtArrestos30.value = "";
                        rvArrestos30.minimumvalue = "1";
                        ddlArrestado30.disabled = false;
                        txtArrestos30.disabled = false;
                    }

                }
                break;
            case ("2")://No  

                if (ddlArrestado.value == "2" || ddlArrestado.value == "") {
                    ddlArrestado30.disabled = true;
                    ddlArrestado30.value = '99';

                }
                else {
                    ddlArrestado30.disabled = false;
                }
                txtArrestos30.value = '0';
                rvArrestos30.minimumvalue = "0";
                txtArrestos30.disabled = true;
                break;

            default:
                if (ddlArrestado.value == "1") {
                    ddlArrestado30.value = "1";
                    txtArrestos30.value = "";
                    rvArrestos30.minimumvalue = "1";
                    txtArrestos30.disabled = false;
                }
                else if (ddlArrestado.value == "2") {
                    ddlArrestado30.value = "99";
                    txtArrestos30.value = "0";
                    rvArrestos30.minimumvalue = "0";
                    ddlArrestado30.disabled = true;
                    txtArrestos30.disabled = true;
                }
                else {
                    txtArrestos30.value = '0';
                    rvArrestos30.minimumvalue = "0";
                    txtArrestos30.disabled = true;
                }

                break;
        }
    }
    catch (ex) { }
}

/**
 * Cambios por Jose A. Ramos De La Cruz
 * Fecha: 4/21/20
 * Proposito: Activar y desactivar las opciones de no aplica.
 * */

function ddlDrogaChangeEvent() {
    $("#mainBodyContent_WucEpisodioAdmision_ddlDrogaPrim").change(function () {
        var value = $("#mainBodyContent_WucEpisodioAdmision_ddlDrogaPrim").val();
        ddlDrogaChange(value, 'ddlViaPrim', 'ddlFrecPrim', 'txtEdadPrim', 'ddlToxicologia1');
    });

    $("#mainBodyContent_WucEpisodioAdmision_ddlDrogaSec").change(function () {
        var value = $("#mainBodyContent_WucEpisodioAdmision_ddlDrogaSec").val();
        ddlDrogaChange(value, 'ddlViaSec', 'ddlFrecSec', 'txtEdadSec', 'ddlToxicologia2');
    });

    $("#mainBodyContent_WucEpisodioAdmision_ddlDrogaPrim").change(function () {
        var value = $("#mainBodyContent_WucEpisodioAdmision_ddlDrogaTerc").val();
        ddlDrogaChange(value, 'ddlViaTerc', 'ddlFrecTerc', 'txtEdadTerc', 'ddlToxicologia3');
    });
}

function ddlDrogaChange(value, via, frec, edad, tox) {

    if (value != sustanciasList.Noaplica || value != sustanciasList.Noinformó || value != sustanciasList.Nousaactualmente) {

        $('#mainBodyContent_WucEpisodioAdmision_' + via + ' option[value=99]').removeAttr('disabled').hide();
        $('#mainBodyContent_WucEpisodioAdmision_' + frec + ' option[value=99]').removeAttr('disabled').hide();
        $('#mainBodyContent_WucEpisodioAdmision_' + edad + ' option[value=126]').removeAttr('disabled').hide();
        $('#mainBodyContent_WucEpisodioAdmision_' + tox + ' option[value=99]').removeAttr('disabled').hide();
    } else {

        $('#mainBodyContent_WucEpisodioAdmision_' + via + ' option[value=99]').removeAttr('disabled').show();
        $('#mainBodyContent_WucEpisodioAdmision_' + frec + ' option[value=99]').removeAttr('disabled').show();
        $('#mainBodyContent_WucEpisodioAdmision_' + edad + ' option[value=126]').removeAttr('disabled').show();
        $('#mainBodyContent_WucEpisodioAdmision_' + tox + ' option[value=99]').removeAttr('disabled').show();
    }


}



function ddlReunionesGrupos() {
    try {
        var ddlReunionesGrupos = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlReunionesGrupos");
        var ddlFreq_AutoAyuda = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFreq_AutoAyuda");
        switch (ddlReunionesGrupos.value) {
            case ("2"): case ("94"):
                ddlFreq_AutoAyuda.value = 1;
                ddlFreq_AutoAyuda.disabled = true;
                break;
            default:
                if (ddlFreq_AutoAyuda.disabled) {
                    ddlFreq_AutoAyuda.value = 0;
                    ddlFreq_AutoAyuda.disabled = false;
                }

                break;
        }
    }
    catch (ex) { }
}

function ddlFreq_AutoAyuda() {
    try {
        var ddlReunionesGrupos = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlReunionesGrupos");
        var ddlFreq_AutoAyuda = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFreq_AutoAyuda");
        switch (ddlFreq_AutoAyuda.value) {
            case ("1"): case ("99"):
                if (ddlReunionesGrupos.value == "1") {
                    ddlFreq_AutoAyuda.value = "0";
                    alert("Debe escoger una opción válida, ya que seleccionó que el paciente SI ha participado en reuniones de grupo.");
                }
                break;
        }

    } catch (ex) { }
}

var sustanciasList = {
    NoSeleccionado: "0",
    Alcohol: "1",
    Anfetaminas: "2",
    Barbitúricos: "3",
    Benzodiazepinas: "4",
    Cocaína: "5",
    Otrosopiáceosyopioides: "6",
    Marihuanasintética: "7",
    Crack: "8",
    Ecstasy: "9",
    Heroína: "10",
    Metanfetamina: "11",
    HeroínaCocaínaSpeedball: "12",
    Marihuana: "13",
    Metadona: "14",
    PCP: "15",
    Percocet: "16",
    Sedantes: "17",
    Tabacocigarrillo: "18",
    Nousaactualmente: "19",
    Inhalantes: "20",
    Alucinógenos: "21",
    Otro: "22",
    Medicamentosnorecetados: "23",
    Xanax: "24",
    Anestesiadecaballo: "26",
    CocaínaMarihuanaDiablillo: "27",
    Noinformó: "96",
    Noaplica: "99"
}
var viaList = {
    NoSeleccionado: "0",
    Inyectada: "1",
    Nasal: "2",
    OralBebida: "3",
    Fumada: "4",
    Otro: "10",
    Desconocido: "95",
    NoAplica: "99"
}
function ddlDrogaPrimF() {
    try {
        var CO_Tipo = document.getElementById("mainBodyContent_WucEpisodioAdmision_CO_Tipo");
        var ddlNivelCuidadoSaludMental = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSaludMental");
        var ddlNivelCuidadoSustancias = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSustancias");
        var ddlDSMVDiagDual = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDSMVDiagDual");
        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaPrim");
        var ddlDrogaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaSec");
        var ddlDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaTerc");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaPrim");
        var ddlViaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaSec");
        var ddlViaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaTerc");
        var ddlFrecPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecPrim");
        var ddlFrecSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecSec");
        var ddlFrecTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecTerc");
        var txtEdadPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadPrim");
        var txtEdadSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadSec");
        var txtEdadTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadTerc");

        var txtDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDrogaPrim");

        var hogarDiv = document.getElementById("mainBodyContent_WucEpisodioAdmision_Hogar_DIV");

        var ddlToxicologia1 = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlToxicologia1");
        var ddlToxicologia2 = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlToxicologia2");
        var ddlToxicologia3 = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlToxicologia3");


        ddlViaPrim.disabled = false;
        ddlFrecPrim.disabled = false;
        txtEdadPrim.disabled = false;

        if (IsPostBack() == "False") {
            ddlViaPrim.value = 0;
            ddlFrecPrim.value = "";
            ddlToxicologia1.value = "";
            txtEdadPrim.value = "";

        }



        if ((ddlNivelCuidadoSaludMental.value !== "99" || (ddlNivelCuidadoSustancias.value === "99" && (CO_Tipo.value === "2" || CO_Tipo.value === "3"))) && ddlDrogaPrim.value != sustanciasList.Noaplica) {

            if (ddlDrogaSec.value == sustanciasList.Noaplica && ddlDrogaTerc.value == sustanciasList.Noaplica) {

                ddlDrogaSec.value = sustanciasList.Nousaactualmente;
                ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
            }
        }

        switch (ddlDrogaPrim.value) {
            case (sustanciasList.Alcohol): case (sustanciasList.Ecstasy): case (sustanciasList.Metadona): case (sustanciasList.Percocet): case (sustanciasList.Xanax):
                ddlViaPrim.value = viaList.OralBebida;
                ddlDrogaSec.disabled = false;
                ddlViaPrim.disabled = true;
                if (IsPostBack() == "False") {
                    txtDrogaPrim.value = "";
                    ddlToxicologia1.value = "";
                }
                ddlToxicologia1.disabled = false;
                hogarDiv.style.visibility = 'hidden';
                break;
            case (sustanciasList.Inhalantes):
                ddlViaPrim.value = viaList.Nasal;
                ddlDrogaSec.disabled = false;
                ddlViaPrim.disabled = true;
                txtDrogaPrim.value = "";

                if (IsPostBack() == "False") {
                    ddlToxicologia1.value = "";
                }
                ddlToxicologia1.disabled = false;
                hogarDiv.style.visibility = 'hidden';
                break;
            case (sustanciasList.Anestesiadecaballo):
                ddlViaPrim.value = viaList.Inyectada;
                ddlDrogaSec.disabled = false;
                ddlViaPrim.disabled = true;
                txtDrogaPrim.value = "";

                if (IsPostBack() == "False") {
                    ddlToxicologia1.value = "";
                }
                ddlToxicologia1.disabled = false;
                hogarDiv.style.visibility = 'hidden';
                break;
            case (sustanciasList.Tabacocigarrillo):
                ddlViaPrim.value = viaList.Fumada;
                ddlDrogaSec.disabled = false;
                ddlViaPrim.disabled = true;
                txtDrogaPrim.value = "";
                if (IsPostBack() == "False") {
                    ddlToxicologia1.value = "";
                }
                ddlToxicologia1.disabled = false;
                hogarDiv.style.visibility = 'hidden';
                break;
            case (sustanciasList.Otro): case (sustanciasList.Otrosopiáceosyopioides):
                //txtDrogaPrim.style.visibility = 'visible';
                hogarDiv.style.visibility = 'visible';
                ddlDrogaSec.disabled = false;
                if (IsPostBack() == "False") {
                    ddlToxicologia1.value = "";
                }
                ddlToxicologia1.disabled = false;
                break;
            case (sustanciasList.Nousaactualmente): case (sustanciasList.Noaplica): case (sustanciasList.Noinformó):
            case ("95"): case ("98")://OLDVALUES

                //if ((ddlNivelCuidadoSustancias.value !== "99")
                //    ||
                //    ((ddlNivelCuidadoSustancias.value === "99" && ddlNivelCuidadoSaludMental.value === "99") &&
                //    (CO_Tipo.value === "1" || CO_Tipo.value === "4" || ddlDSMVDiagDual.value === "1"))) {
                //ddlDrogaPrim.value = 0;

                //    if ((ddlNivelCuidadoSustancias.value !== "99") || (CO_Tipo.value === "1" || CO_Tipo.value === "4")) {
                //    alert("Este perfil es de Abuso de Sustancia, no puede seleccionar " + "'" + "No Aplica" + "'" + ".");
                //}
                //else {

                //    // alert("Este perfil esta seleccionado como CONCURRENTE, no puede seleccionar " + "'" + "No Aplica" + "'" + ".");

                //    ddlDrogaPrim.value = 96;
                //    ddlViaPrim.value = 95;
                //    ddlFrecPrim.value=99;
                //    txtEdadPrim.value = 0;

                //    ddlViaPrim.disabled = true;
                //    ddlFrecPrim.disabled = true;
                //    txtEdadPrim.disabled = true;

                //}

                //ddlDrogaSec.value = sustanciasList.Nousaactualmente;
                //ddlDrogaTerc.value = sustanciasList.Nousaactualmente;

                //}
                txtDrogaPrim.value = "";
                hogarDiv.style.visibility = 'hidden';
                if (ddlNivelCuidadoSustancias.value !== "99" || (ddlNivelCuidadoSaludMental.value === "99" && (CO_Tipo.value === "1" || CO_Tipo.value === "4"))) {
                    alert("Este perfil es de Abuso de Sustancia, no puede seleccionar " + "'" + "No Aplica" + "'" + ".");
                    ddlDrogaPrim.value = 0;
                    ddlDrogaSec.value = sustanciasList.Nousaactualmente;
                    ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                    ddlToxicologia1.value = "";
                }
                else {
                    ddlDrogaPrim.value = sustanciasList.Noaplica;
                    ddlDrogaSec.value = sustanciasList.Noaplica;
                    ddlDrogaTerc.value = sustanciasList.Noaplica;
                    ddlViaPrim.value = viaList.NoAplica;
                    ddlViaPrim.disabled = true;
                    ddlFrecPrim.value = 99;
                    ddlFrecPrim.disabled = true;
                    txtEdadPrim.value = "126";
                    txtEdadPrim.disabled = true;

                    ddlToxicologia1.value = "99";
                    ddlToxicologia1.disabled = true;
                }

                ddlViaSec.value = viaList.NoAplica;
                ddlViaTerc.value = viaList.NoAplica;
                ddlFrecSec.value = 99;
                ddlFrecTerc.value = 99;
                txtEdadSec.value = "126";
                txtEdadTerc.value = "126";
                ddlDrogaSec.disabled = true;
                ddlDrogaTerc.disabled = true;
                ddlViaSec.disabled = true;
                ddlViaTerc.disabled = true;
                ddlFrecSec.disabled = true;
                ddlFrecTerc.disabled = true;
                txtEdadSec.disabled = true;
                txtEdadTerc.disabled = true;

                ddlToxicologia2.value = "99";
                ddlToxicologia2.disabled = true;
                ddlToxicologia3.value = "99";
                ddlToxicologia3.disabled = true;

                break;
            case ("0"):
                if (ddlNivelCuidadoSaludMental.value !== "99" || (ddlNivelCuidadoSaludMental.value === "99" && (CO_Tipo.value === "2" || CO_Tipo.value === "3"))) {
                    ddlViaPrim.value = 0;
                    //ddlViaPrim.disabled = true;
                    ddlFrecPrim.value = 0;
                    //ddlFrecPrim.disabled = true;
                    txtEdadPrim.value = "126";
                    //txtEdadPrim.disabled = true;
                    ddlDrogaSec.value = sustanciasList.Nousaactualmente;
                    ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                    ddlViaSec.value = viaList.NoAplica;
                    ddlViaTerc.value = viaList.NoAplica;
                    ddlFrecSec.value = 99;
                    ddlFrecTerc.value = 99;
                    txtEdadSec.value = "126";
                    txtEdadTerc.value = "126";
                    ddlDrogaSec.disabled = true;
                    ddlDrogaTerc.disabled = true;
                    ddlViaSec.disabled = true;
                    ddlViaTerc.disabled = true;
                    ddlFrecSec.disabled = true;
                    ddlFrecTerc.disabled = true;
                    txtEdadSec.disabled = true;
                    txtEdadTerc.disabled = true;

                    ddlToxicologia1.value = "99";
                    ddlToxicologia2.value = "99";
                    ddlToxicologia2.disabled = true;
                    ddlToxicologia3.value = "99";
                    ddlToxicologia3.disabled = true;
                }

                txtDrogaPrim.value = "";
                hogarDiv.style.visibility = 'hidden';
                break;
            default:
                txtDrogaPrim.value = "";
                hogarDiv.style.visibility = 'hidden';
                ddlViaPrim.disabled = false;
                ddlDrogaSec.disabled = false;
                if (IsPostBack() == "False") {
                    ddlToxicologia1.value = "";
                }

                ddlToxicologia1.disabled = false;


                break;
        }
        ddlViaPrimF();
        ddlDrogaSecF();
    }
    catch (ex) { }
}
function ddlDrogaSecF() {
    try {
        var CO_Tipo = document.getElementById("mainBodyContent_WucEpisodioAdmision_CO_Tipo");
        var ddlNivelCuidadoSaludMental = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSaludMental");
        var ddlNivelCuidadoSustancias = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSustancias");
        var ddlDSMVDiagDual = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDSMVDiagDual");
        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaPrim");
        var ddlDrogaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaSec");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaPrim");
        var ddlViaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaSec");
        var ddlFrecPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecPrim");
        var ddlFrecSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecSec");
        var txtEdadPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadPrim");
        var txtEdadSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadSec");
        var ddlDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaTerc");
        var ddlViaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaTerc");
        var ddlFrecTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecTerc");
        var txtEdadTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadTerc");

        var txtDrogaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDrogaSec");

        var ddlToxicologia1 = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlToxicologia1");
        var ddlToxicologia2 = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlToxicologia2");
        var ddlToxicologia3 = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlToxicologia3");


        var hogarDiv = document.getElementById("mainBodyContent_WucEpisodioAdmision_Hogar2_DIV");

        ddlViaSec.disabled = false;
        ddlFrecSec.disabled = false;

        if (IsPostBack() == "False") {
            ddlFrecSec.value = "";
            txtEdadSec.value = "";
        }
        txtEdadSec.disabled = false;




        if (!(ddlDrogaPrim.value === sustanciasList.Noaplica || ddlDrogaPrim.value === sustanciasList.Nousaactualmente || ddlDrogaPrim.value === "0") && (ddlViaPrim.value === "0" || ddlViaPrim.value == viaList.NoAplica || ddlFrecPrim.value === 0 || ddlFrecPrim.value === 99 || txtEdadPrim.value < "0") && !(ddlDrogaSec.value === sustanciasList.Noaplica || ddlDrogaSec.value === sustanciasList.Nousaactualmente)) {

            ddlDrogaSec.value = sustanciasList.Nousaactualmente;
            ddlViaSec.value = viaList.NoAplica;
            ddlViaSec.disabled = true;
            ddlFrecSec.value = 99;
            ddlFrecSec.disabled = true;
            txtEdadSec.value = "126";
            txtEdadSec.disabled = true;
            ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
            ddlViaTerc.value = viaList.NoAplica;
            ddlFrecTerc.value = 99;
            txtEdadTerc.value = "126";
            ddlDrogaTerc.disabled = true;
            ddlViaTerc.disabled = true;
            ddlFrecTerc.disabled = true;
            txtEdadTerc.disabled = true;

            ddlToxicologia2.value = "99";
            ddlToxicologia2.disabled = true;
            ddlToxicologia3.value = "99";
            ddlToxicologia3.disabled = true;
            alert("Debe completar toda información de la primera sustancia");
        }
        else {
            switch (ddlDrogaSec.value) {
                case (sustanciasList.Alcohol): case (sustanciasList.Ecstasy): case (sustanciasList.Metadona): case (sustanciasList.Percocet): case (sustanciasList.Xanax):
                    ddlViaSec.value = viaList.OralBebida;
                    ddlDrogaTerc.disabled = false;
                    ddlViaSec.disabled = true;
                    txtDrogaSec.value = "";

                    if (IsPostBack() == "False") {
                        ddlFrecSec.value = 0;
                        ddlToxicologia2.value = "";
                    }
                    ddlToxicologia2.disabled = false;
                    hogarDiv.style.visibility = 'hidden';
                    break;
                case (sustanciasList.Inhalantes):
                    ddlViaSec.value = viaList.Nasal;
                    ddlDrogaTerc.disabled = false;
                    ddlViaSec.disabled = true;
                    if (IsPostBack() == "False") {
                        ddlFrecSec.value = 0;
                        ddlToxicologia2.value = "";
                    }
                    txtDrogaSec.value = "";

                    ddlToxicologia2.disabled = false;
                    hogarDiv.style.visibility = 'hidden';
                    break;
                case (sustanciasList.Anestesiadecaballo):
                    ddlViaSec.value = viaList.Inyectada;
                    ddlDrogaTerc.disabled = false;
                    ddlViaSec.disabled = true;
                    if (IsPostBack() == "False") {
                        ddlFrecSec.value = 0;
                    }
                    txtDrogaSec.value = "";
                    hogarDiv.style.visibility = 'hidden';
                    break;
                case (sustanciasList.Tabacocigarrillo):
                    ddlViaSec.value = viaList.Fumada;
                    ddlDrogaTerc.disabled = false;
                    ddlViaSec.disabled = true;
                    if (IsPostBack() == "False") {
                        ddlFrecSec.value = 0;
                        ddlToxicologia2.value = "";
                    }
                    txtDrogaSec.value = "";

                    ddlToxicologia2.disabled = false;
                    hogarDiv.style.visibility = 'hidden';
                    break;
                case (sustanciasList.Otro): case (sustanciasList.Otrosopiáceosyopioides):
                    //txtDrogaPrim.style.visibility = 'visible';
                    hogarDiv.style.visibility = 'visible';
                    ddlDrogaTerc.disabled = false;
                    if (IsPostBack() == "False") {
                        ddlToxicologia2.value = "";
                    }
                    ddlToxicologia2.disabled = false;
                    break;
                case (sustanciasList.Nousaactualmente): case (sustanciasList.Noaplica): case (sustanciasList.Noinformó):

                case ("95"): case ("98"): //OLDVALUES
                    if (ddlNivelCuidadoSustancias.value !== "99" || (ddlNivelCuidadoSaludMental.value === "99" && (CO_Tipo.value === "1" || CO_Tipo.value === "4"))) {
                        ddlDrogaSec.value = sustanciasList.Nousaactualmente;
                        ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                    }
                    else if (ddlNivelCuidadoSaludMental.value !== "99" && ddlDrogaPrim.value !== sustanciasList.Noaplica) {
                        ddlDrogaSec.value = sustanciasList.Nousaactualmente;
                        ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                    }
                    else {
                        ddlDrogaSec.value = sustanciasList.Noaplica;
                        ddlDrogaTerc.value = sustanciasList.Noaplica;
                    }
                    ddlViaSec.value = viaList.NoAplica;
                    ddlViaSec.disabled = true;
                    ddlFrecSec.value = 99;
                    ddlFrecSec.disabled = true;
                    txtEdadSec.value = "126";
                    txtEdadSec.disabled = true;
                    ddlViaTerc.value = viaList.NoAplica;
                    ddlFrecTerc.value = 99;
                    txtEdadTerc.value = "126";
                    ddlDrogaTerc.disabled = true;
                    ddlViaTerc.disabled = true;
                    ddlFrecTerc.disabled = true;
                    txtEdadTerc.disabled = true;

                    txtDrogaSec.value = "";

                    ddlToxicologia2.value = "99";
                    ddlToxicologia2.disabled = true;
                    ddlToxicologia3.value = "99";
                    ddlToxicologia3.disabled = true;

                    hogarDiv.style.visibility = 'hidden';
                    break;
                case ("0"):
                     ddlDrogaSec.value = "19";
                    ddlViaSec.value = viaList.NoAplica;
                    ddlViaSec.disabled = true;
                    ddlFrecSec.value = 99;
                    ddlFrecSec.disabled = true;
                    txtEdadSec.value = "126";
                    txtEdadSec.disabled = true;
                    ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                    ddlViaTerc.value = viaList.NoAplica;
                    ddlFrecTerc.value = 99;
                    txtEdadTerc.value = "126";
                    ddlDrogaTerc.disabled = true;
                    ddlViaTerc.disabled = true;
                    ddlFrecTerc.disabled = true;
                    txtEdadTerc.disabled = true;

                    txtDrogaSec.value = "";

                    ddlToxicologia2.value = "99";
                    ddlToxicologia2.disabled = true;
                    ddlToxicologia3.value = "99";
                    ddlToxicologia3.disabled = true;

                    hogarDiv.style.visibility = 'hidden';
                    break;
                default:
                    ddlViaSec.disabled = false;
                    ddlDrogaTerc.disabled = false;
                    if (IsPostBack() == "False") {
                        txtEdadSec.value = "";
                        ddlToxicologia2.value = "";
                    }
                    txtDrogaSec.value = "";

                    ddlToxicologia2.disabled = false;

                    hogarDiv.style.visibility = 'hidden';
                    break;
            }
            if (ddlDrogaSec.value !== sustanciasList.Nousaactualmente && ddlDrogaSec.value !== sustanciasList.Noaplica && ddlDrogaSec.value !== sustanciasList.Noinformó && ddlViaSec.value !== 0) {
                if (ddlDrogaSec.value === ddlDrogaPrim.value && ddlViaSec.value === ddlViaPrim.value) {
                    alert("La droga y la vía de utilización NO pueden ser igual a las selecciones de la primera sustancia.");
                    ddlDrogaSec.value = sustanciasList.Nousaactualmente;
                    ddlViaSec.value = viaList.NoAplica;
                    ddlFrecSec.value = 99;
                    txtEdadSec.value = "126";
                    ddlViaSec.disabled = true;
                    ddlFrecSec.disabled = true;
                    txtEdadSec.disabled = true;

                    ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                    ddlViaTerc.value = viaList.NoAplica;
                    ddlFrecTerc.value = 99;
                    txtEdadTerc.value = "126";
                    ddlDrogaTerc.disabled = true;
                    ddlViaTerc.disabled = true;
                    ddlFrecTerc.disabled = true;
                    txtEdadTerc.disabled = true;

                    txtDrogaSec.value = "";

                    ddlToxicologia2.value = "99";
                    ddlToxicologia2.disabled = true;
                    ddlToxicologia3.value = "99";
                    ddlToxicologia3.disabled = true;

                    hogarDiv.style.visibility = 'hidden';

                }
            }
        }
        ddlViaSecF();
        ddlDrogaTercF();

    }
    catch (ex) { alert(ex.message); }
}
function ddlDrogaTercF() {
    try {
        var CO_Tipo = document.getElementById("mainBodyContent_WucEpisodioAdmision_CO_Tipo");
        var ddlNivelCuidadoSaludMental = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSaludMental");
        var ddlNivelCuidadoSustancias = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSustancias");

        var ddlDSMVDiagDual = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDSMVDiagDual");

        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaPrim");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaPrim");

        var ddlDrogaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaSec");
        var ddlViaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaSec");
        var ddlFrecSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecSec");
        var txtEdadSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadSec");

        var ddlDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaTerc");
        var ddlViaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaTerc");
        var ddlFrecTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecTerc");
        var txtEdadTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadTerc");
        ddlViaTerc.disabled = false;
        ddlFrecTerc.disabled = false;

        txtEdadTerc.disabled = false;


        var txtDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDrogaTerc");

        var ddlToxicologia1 = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlToxicologia1");
        var ddlToxicologia2 = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlToxicologia2");
        var ddlToxicologia3 = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlToxicologia3");


        var hogarDiv = document.getElementById("mainBodyContent_WucEpisodioAdmision_Hogar3_DIV");
        if (IsPostBack() == "False") {
            txtEdadTerc.value = "";
            ddlFrecTerc.value = "";
        }

        if (!(ddlDrogaSec.value === sustanciasList.Noaplica || ddlDrogaSec.value === "0" || ddlDrogaSec.value === sustanciasList.Nousaactualmente) && (ddlViaSec.value === "0" || ddlViaSec.value === viaList.NoAplica || ddlFrecSec.value === 0 || ddlFrecSec.value === 99 || txtEdadSec.value < "0") && !(ddlDrogaTerc.value === sustanciasList.Noaplica || ddlDrogaTerc.value === sustanciasList.Nousaactualmente)) {
            ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
            ddlViaTerc.value = viaList.NoAplica;
            ddlFrecTerc.value = 99;
            txtEdadTerc.value = "126";
            // ddlDrogaTerc.disabled = true;
            ddlViaTerc.disabled = true;
            ddlFrecTerc.disabled = true;
            txtEdadTerc.disabled = true;

            ddlToxicologia3.value = "99";
            ddlToxicologia3.disabled = true;

            alert("Debe completar toda información de la segunda sustancia");
        }
        else {
            switch (ddlDrogaTerc.value) {
                case (sustanciasList.Alcohol): case (sustanciasList.Ecstasy): case (sustanciasList.Metadona): case (sustanciasList.Percocet): case (sustanciasList.Xanax):
                    ddlViaTerc.value = viaList.OralBebida;
                    ddlViaTerc.disabled = true;
                    if (IsPostBack() == "False") {
                        ddlFrecTerc.value = 0;

                        ddlToxicologia3.value = "";
                    }
                    txtDrogaTerc.value = "";
                    ddlToxicologia3.disabled = false;
                    hogarDiv.style.visibility = 'hidden';
                    break;
                case (sustanciasList.Inhalantes):
                    ddlViaTerc.value = viaList.Nasal;
                    ddlViaTerc.disabled = true;
                    if (IsPostBack() == "False") {
                        ddlFrecTerc.value = 0;
                        ddlToxicologia3.value = "";
                    }
                    txtDrogaTerc.value = "";

                    ddlToxicologia3.disabled = false;
                    hogarDiv.style.visibility = 'hidden';
                    break;
                case (sustanciasList.Anestesiadecaballo):
                    ddlViaTerc.value = viaList.Inyectada;
                    ddlViaTerc.disabled = true;
                    if (IsPostBack() == "False") {
                        ddlFrecTerc.value = 0;
                        ddlToxicologia3.value = "";
                    }
                    txtDrogaTerc.value = "";

                    ddlToxicologia3.disabled = false;
                    hogarDiv.style.visibility = 'hidden';
                    break;
                case (sustanciasList.Tabacocigarrillo):
                    ddlViaTerc.value = viaList.Fumada;
                    ddlViaTerc.disabled = true;
                    txtDrogaTerc.value = "";
                    if (IsPostBack() == "False") {
                        ddlFrecTerc.value = 0;
                        ddlToxicologia3.value = "";
                    }
                    ddlToxicologia3.disabled = false;
                    hogarDiv.style.visibility = 'hidden';
                    break;
                case (sustanciasList.Otro): case (sustanciasList.Otrosopiáceosyopioides):
                    //txtDrogaPrim.style.visibility = 'visible';
                    if (IsPostBack() == "False") {
                        ddlToxicologia3.value = "";
                    }
                    ddlToxicologia3.disabled = false;
                    hogarDiv.style.visibility = 'visible';
                    break;
                case (sustanciasList.Nousaactualmente): case (sustanciasList.Noaplica): case (sustanciasList.Noinformó):
                case ("95"): case ("98")://OLDVALUES
                    if (ddlNivelCuidadoSustancias.value !== "99" || (ddlNivelCuidadoSaludMental.value === "99" && (CO_Tipo.value === "1" || CO_Tipo.value === "4"))) {
                        ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                    }
                    else if (ddlNivelCuidadoSaludMental.value !== "99" && ddlDrogaPrim.value !== sustanciasList.Noaplica) {

                        ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                    }
                    else {
                        ddlDrogaTerc.value = sustanciasList.Noaplica;
                    }
                    ddlViaTerc.value = viaList.NoAplica;
                    ddlViaTerc.disabled = true;
                    ddlFrecTerc.value = 99;
                    ddlFrecTerc.disabled = true;
                    txtEdadTerc.value = "126";
                    txtEdadTerc.disabled = true;
                    txtDrogaTerc.value = "";
                    ddlToxicologia3.value = "99";
                    ddlToxicologia3.disabled = true;
                    hogarDiv.style.visibility = 'hidden';
                    break;
                case ("0"):
                    ddlViaTerc.value = viaList.NoAplica;
                    ddlViaTerc.disabled = true;
                    ddlFrecTerc.value = 99;
                    ddlFrecTerc.disabled = true;
                    txtEdadTerc.value = "126";
                    txtEdadTerc.disabled = true;
                    txtDrogaTerc.value = "";
                    ddlToxicologia3.value = "99";
                    ddlToxicologia3.disabled = true;
                    hogarDiv.style.visibility = 'hidden';
                    break;
                default:
                    ddlViaTerc.disabled = false;
                    if (IsPostBack() == "False") {
                        txtEdadTerc.value = "";
                        ddlToxicologia3.value = "";

                    } txtDrogaTerc.value = "";
                    ddlToxicologia3.disabled = false;
                    hogarDiv.style.visibility = 'hidden';
                    break;
            }
        }
        if (ddlDrogaTerc.value !== sustanciasList.Nousaactualmente && ddlDrogaTerc.value !== sustanciasList.Noaplica && ddlDrogaTerc.value !== sustanciasList.Noinformó && ddlViaTerc.value !== 0) {
            if (ddlDrogaTerc.value === ddlDrogaSec.value && ddlViaTerc.value === ddlViaSec.value) {
                alert("La droga y la vía de utilización NO pueden ser igual a las selecciones de la segunda sustancia.");
                ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                ddlViaTerc.value = viaList.NoAplica;
                ddlFrecTerc.value = 99;
                txtEdadTerc.value = "126";
                ddlViaTerc.disabled = true;
                ddlFrecTerc.disabled = true;
                txtEdadTerc.disabled = true;
                txtDrogaTerc.value = "";
                hogarDiv.style.visibility = 'hidden';

            }
            else if (ddlDrogaTerc.value === ddlDrogaPrim.value && ddlViaTerc.value === ddlViaPrim.value) {
                alert("La droga y la vía de utilización NO pueden ser igual a las selecciones de la primera sustancia.");
                ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                ddlViaTerc.value = viaList.NoAplica;
                ddlFrecTerc.value = 99;
                txtEdadTerc.value = "126";
                ddlViaTerc.disabled = true;
                ddlFrecTerc.disabled = true;
                txtEdadTerc.disabled = true;
                txtDrogaTerc.value = "";
                hogarDiv.style.visibility = 'hidden';

            }
        }
        ddlViaTercF();
    }
    catch (ex) { alert(ex.message); }
}
function ddlViaPrimF() {
    try {
        var ddlDroga = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaPrim");
        var ddlVia = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaPrim");

        var ddlDrogaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaSec");
        var ddlViaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaSec");

        var ddlDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaTerc");
        var ddlViaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaTerc");

        if (ddlDroga.value === sustanciasList.Heroína) {
            if (ddlVia.value !== viaList.Nasal && ddlVia.value !== viaList.Inyectada && ddlVia.value != 0) {
                alert('La droga(heroína) permite solo las vias "Nasal" o "Inyectada".');
                ddlVia.value = 0;
            }
        }
        else if (ddlDroga.value === sustanciasList.Marihuana || ddlDroga.value === sustanciasList.Marihuanasintética) {
            if (ddlVia.value === viaList.Nasal) {
                if (ddlDroga.value === sustanciasList.Marihuana) {
                    alert("La droga(Marihuana) no permite la via \"Nasal\".");
                    ddlVia.value = 0;
                }
                else if (ddlDroga.value === sustanciasList.Marihuanasintética) {
                    alert("La droga(Marihuana Sintética) no permite la via \"Nasal\".");
                    ddlVia.value = 0;
                }
            }
        }
        if (ddlVia.value === viaList.NoAplica) {

            if (ddlDroga.value === sustanciasList.Nousaactualmente || ddlDroga.value === sustanciasList.Noaplica || ddlDroga.value === sustanciasList.Noinformó) {

            }
            else {
                alert("Debe escoger una vía de utilización válida, ya que seleccionó una droga válida");
                ddlVia.value = 0;
            }
        }
        if (ddlDroga.value !== sustanciasList.Nousaactualmente && ddlDroga.value !== sustanciasList.Noaplica && ddlDroga.value !== sustanciasList.Noinformó && ddlVia.value !== 0 && ddlVia.value !== viaList.NoAplica) {
            if (ddlDroga.value === ddlDrogaSec.value && ddlVia.value === ddlViaSec.value) {
                ddlDrogaSecF();
            }
            else if (ddlDroga.value === ddlDrogaTerc.value && ddlVia.value === ddlViaTerc.value) {
                ddlDrogaTercF();
            }
        }
        ddlFrecPrim();
    }
    catch (ex) { alert(ex.text); }
}
function ddlViaSecF() {
    try {
        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaPrim");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaPrim");

        var ddlDroga = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaSec");
        var ddlVia = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaSec");
        var ddlFrec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecSec");

        var ddlDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaTerc");
        var ddlViaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaTerc");

        if (ddlDroga.value == sustanciasList.Heroína) {
            if (ddlVia.value != viaList.Nasal && ddlVia.value != viaList.Inyectada && ddlVia.value != 0) {
                alert('La droga(heroína) permite solo las vias "Nasal" o "Inyectada".');
                ddlVia.value = 0;
                if (ddlFrec.value == 99) {
                    ddlFrec.value = 0;
                }
            }
        }
        else if (ddlDroga.value == sustanciasList.Marihuana || ddlDroga.value == sustanciasList.Marihuanasintética) {
            if (ddlVia.value == viaList.Nasal) {
                if (ddlDroga.value == sustanciasList.Marihuana) {
                    alert("La droga(Marihuana) no permite la via \"Nasal\".");
                    ddlVia.value = 0;
                }
                else if (ddlDroga.value == sustanciasList.Marihuanasintética) {
                    alert("La droga(Marihuana Sintética) no permite la via \"Nasal\".");
                    ddlVia.value = 0;
                }
            }
        }
        if (ddlVia.value == viaList.NoAplica) {
            if (ddlDroga.value == sustanciasList.Nousaactualmente || ddlDroga.value == sustanciasList.Noaplica || ddlDroga.value == sustanciasList.Noinformó || ddlDroga.value == "0") {

            }
            else if (ddlFrec.value == 99) {
                ddlVia.value = 0;
                ddlFrec.value = 0;
            }
            else {
                alert("Debe escoger una vía de utilización válida, ya que seleccionó una droga válida");
                ddlVia.value = 0;
            }
        }
        if (ddlDroga.value != sustanciasList.Nousaactualmente && ddlDroga.value != sustanciasList.Noaplica && ddlDroga.value != sustanciasList.Noinformó && ddlVia.value != 0 && ddlVia.value != viaList.NoAplica) {
            if (ddlDroga.value == ddlDrogaPrim.value && ddlVia.value == ddlViaPrim.value) {
                alert("La droga y la vía de utilización NO pueden ser igual a las selecciones de la primera sustancia.");
                ddlVia.value = 0;
                //ddlDrogaPrimF();
            }
            else if (ddlDroga.value == ddlDrogaTerc.value && ddlVia.value == ddlViaTerc.value) {
                ddlDrogaTercF();
            }
        }
        ddlFrecSec();
    }
    catch (ex) { }
}
function ddlViaTercF() {
    try {
        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaPrim");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaPrim");

        var ddlDrogaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaSec");
        var ddlViaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaSec");

        var ddlDroga = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaTerc");
        var ddlVia = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaTerc");
        var ddlFrec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecTerc");
        if (ddlDroga.value == sustanciasList.Heroína) {
            if (ddlVia.value != viaList.Nasal && ddlVia.value != viaList.Inyectada && ddlVia.value != 0) {
                alert('La droga(heroína) permite solo las vias "Nasal" o "Inyectada".');
                ddlVia.value = 0;
                if (ddlFrec.value == 99) {
                    ddlFrec.value = 0;
                }
            }
        }
        else if (ddlDroga.value == sustanciasList.Marihuana || ddlDroga.value == sustanciasList.Marihuanasintética) {
            if (ddlVia.value == viaList.Nasal) {
                if (ddlDroga.value == sustanciasList.Marihuana) {
                    alert("La droga(Marihuana) no permite la via \"Nasal\".");
                    ddlVia.value = 0;
                }
                else if (ddlDroga.value == sustanciasList.Marihuanasintética) {
                    alert("La droga(Marihuana Sintética) no permite la via \"Nasal\".");
                    ddlVia.value = 0;
                }
            }
        }
        if (ddlVia.value == viaList.NoAplica) {

            if (ddlDroga.value == sustanciasList.Nousaactualmente || ddlDroga.value == sustanciasList.Noaplica || ddlDroga.value == sustanciasList.Noinformó || ddlDroga.value == "0") {

            }
            else if (ddlFrec.value == 99) {
                ddlVia.value = 0;
                ddlFrec.value = 0;
            }
            else {
                alert("Debe escoger una vía de utilización válida, ya que seleccionó una droga válida");
                ddlVia.value = 0;
            }
        }
        if (ddlDroga.value != sustanciasList.Nousaactualmente && ddlDroga.value != sustanciasList.Noaplica && ddlDroga.value != sustanciasList.Noinformó && ddlVia.value != 0 && ddlVia.value != viaList.NoAplica) {
            if (ddlDroga.value == ddlDrogaSec.value && ddlVia.value == ddlViaSec.value) {
                alert("La droga y la vía de utilización NO pueden ser igual a las selecciones de la segunda sustancia.");
                ddlVia.value = 0;
                //ddlDrogaPrimF();
            }
            else if (ddlDroga.value == ddlDrogaPrim.value && ddlVia.value == ddlViaPrim.value) {
                alert("La droga y la vía de utilización NO pueden ser igual a las selecciones de la primera sustancia.");
                ddlVia.value = 0;
                //ddlDrogaPrimF();
            }
        }
        ddlFrecTerc();
    }
    catch (ex) { }
}

function ddlFrecPrim() {
    try {
        var ddlDroga = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaPrim");
        var ddlFrec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecPrim");
        if (ddlFrec.value == "99") {
            if (ddlDroga.value == sustanciasList.Nousaactualmente || ddlDroga.value == sustanciasList.Noaplica || ddlDroga.value == sustanciasList.Noinformó) {

            }
            else {
                alert("Debe escoger una frecuencia de uso válida, ya que seleccionó una droga válida");
                ddlFrec.value = 0;
            }
        }

    }
    catch (ex) { }
}

function ddlFrecSec() {
    try {
        var ddlDroga = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaSec");
        var ddlFrec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecSec");

        if (ddlFrec.value == "99") {
            if (ddlDroga.value == sustanciasList.Nousaactualmente || ddlDroga.value == sustanciasList.Noaplica || ddlDroga.value == sustanciasList.Noinformó || ddlDroga.value == "0") {

            }
            else {
                alert("Debe escoger una frecuencia de uso válida, ya que seleccionó una droga válida");
                ddlFrec.value = 0;
            }
        }

    }
    catch (ex) { }
}

function ddlFrecTerc() {
    try {
        var ddlDroga = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaTerc");
        var ddlFrec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecTerc");
        if (ddlFrec.value == "99") {
            if (ddlDroga.value == sustanciasList.Nousaactualmente || ddlDroga.value == sustanciasList.Noaplica || ddlDroga.value == sustanciasList.Noinformó || ddlDroga.value == "0") {

            }
            else {
                alert("Debe escoger una frecuencia de uso válida, ya que seleccionó una droga válida");
                ddlFrec.value = 0;
            }
        }

    }
    catch (ex) { }
}

function ddlDSMVDiagDual(txtType, ddlDSMVDiagDualP) {
    try {
        var accion = document.getElementById("mainBodyContent_WucEpisodioAdmision_hAccion");

        if (accion.value === "update") {
            return;
        }
        var ddlDSMVDiagDual = document.getElementById(txtType + ddlDSMVDiagDualP);
        var CO_Tipo = document.getElementById("mainBodyContent_WucEpisodioAdmision_CO_Tipo");
        var ddlNivelCuidadoSaludMental = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSaludMental");
        var ddlNivelCuidadoSustancias = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSustancias");

        var opiod = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlMetadona");
        var txtDíasSustancias = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDíasSustancias");

        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaPrim");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaPrim");
        var ddlFrecPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecPrim");
        var txtEdadPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadPrim");

        var ddlDrogaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaSec");
        var ddlDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaTerc");
        var ddlViaSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaSec");
        var ddlViaTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlViaTerc");
        var ddlFrecSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecSec");
        var ddlFrecTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFrecTerc");
        var txtEdadSec = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadSec");
        var txtEdadTerc = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtEdadTerc");
        //Substancias
        var GAF = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDSMVFnGlobal");


        // ajustar aqui por nivel de cuidado
        // 2,3 el programa es de salud mental
        //1,4 el programa es abuso de sustancia


        if ((ddlNivelCuidadoSaludMental.value !== "99") ||
            ((ddlNivelCuidadoSustancias.value === "99" && ddlNivelCuidadoSaludMental.value === "99") && (CO_Tipo.value == "2" || CO_Tipo.value == "3"))) {
            switch (ddlDSMVDiagDual.value) {
                case ("1"):
                    ddlDrogaPrim.value = 0;
                    ddlViaPrim.value = 0;
                    ddlFrecPrim.value = 0;
                    txtEdadPrim.value = "126";
                    ddlDrogaPrim.disabled = false;
                    ddlViaPrim.disabled = false;
                    ddlFrecPrim.disabled = false;
                    txtEdadPrim.disabled = false;

                    ddlDrogaSec.value = sustanciasList.Nousaactualmente;
                    ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                    ddlViaSec.value = viaList.NoAplica;
                    ddlViaTerc.value = viaList.NoAplica;
                    ddlFrecSec.value = 99;
                    ddlFrecTerc.value = 99;
                    txtEdadSec.value = "126";
                    txtEdadTerc.value = "126";
                    ddlDrogaSec.disabled = true;
                    ddlDrogaTerc.disabled = true;
                    ddlViaSec.disabled = true;
                    ddlViaTerc.disabled = true;
                    ddlFrecSec.disabled = true;
                    ddlFrecTerc.disabled = true;
                    txtEdadSec.disabled = true;
                    txtEdadTerc.disabled = true;
                    // Substancia

                    txtDíasSustancias.disabled = false;
                    opiod.value = "0";
                    opiod.disabled = false;
                    //Opiaceos
                    break;
                default:
                    ddlDrogaPrim.value = sustanciasList.Noaplica;
                    ddlViaPrim.value = sustanciasList.Noaplica;
                    ddlFrecPrim.value = 99;
                    txtEdadPrim.value = "126";
                    ddlDrogaPrim.disabled = true;
                    ddlViaPrim.disabled = true;
                    ddlFrecPrim.disabled = true;
                    txtEdadPrim.disabled = true;

                    ddlDrogaSec.value = sustanciasList.Noaplica;
                    ddlDrogaTerc.value = sustanciasList.Noaplica;
                    ddlViaSec.value = viaList.NoAplica;
                    ddlViaTerc.value = viaList.NoAplica;
                    ddlFrecSec.value = 99;
                    ddlFrecTerc.value = 99;
                    txtEdadSec.value = "126";
                    txtEdadTerc.value = "126";
                    ddlDrogaSec.disabled = true;
                    ddlDrogaTerc.disabled = true;
                    ddlViaSec.disabled = true;
                    ddlViaTerc.disabled = true;
                    ddlFrecSec.disabled = true;
                    ddlFrecTerc.disabled = true;
                    txtEdadSec.disabled = true;
                    txtEdadTerc.disabled = true;
                    // Substancia

                    if (ddlNivelCuidadoSaludMental.value == "99") {
                        txtDíasSustancias.value = "0";
                        txtDíasSustancias.disabled = true;
                        opiod.value = "4";
                        opiod.disabled = true;
                    }
                //Opiaceoslsa

            }
        }
        else if ((ddlNivelCuidadoSustancias.value !== "99") ||
            ((ddlNivelCuidadoSustancias.value === "99" && ddlNivelCuidadoSaludMental.value === "99") &&
                (CO_Tipo.value === "1" || CO_Tipo.value === "4"))) {
            switch (ddlDSMVDiagDual.value) {
                case ("1"):
                    GAF.disabled = false;
                    break;
                default:
                    GAF.value = "";
                    GAF.disabled = true;
            }
        }

    }
    catch (ex) {
        alert(ex.text);
    }

    // AjustesNiveldeCuidado();
}

function validateCOOCURRING() {
    var ddlNivelCuidadoSaludMental = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSaludMental");
    var ddlNivelCuidadoSustancias = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSustancias");
    var ddlDSMVDiagDual = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDSMVDiagDual");
    var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlDrogaPrim");
    var opiod = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlMetadona");
    var GAF = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDSMVFnGlobal");
    var ddlPreviosSustancias = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlPreviosSustancias");
    var ddlPreviosMental = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlPreviosMental");
    var ClinHD = document.getElementById("mainBodyContent_WucEpisodioAdmision_hDSMVClinPrim");
    var ClinHDSus = document.getElementById("mainBodyContent_WucEpisodioAdmision_hDSMVSusPrim");
    var ddlFreq_AutoAyuda = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlFreq_AutoAyuda");



   
    var campos = "\n";
    var flagConcurrente = true;
    //Si el perfil es de Salud Mental
    if (ddlNivelCuidadoSaludMental.value != "99") {

    
        if (ClinHD.value == '761') {
            alert("!!! ESTE PERFIL DE SALUD MENTAL REFLEJA QUE ES DE TIPO SALUD MENTAL Y USTED NO SELECCIONÓ AL MENOS UN(1) DIAGNOSTICO VALIDO !!!");
            flagConcurrente = false;

            return false;
        }

        var message = " ESTE PERFIL DE SALUD MENTAL REFLEJA QUE ES CONCURRENTE Y USTED SELECCIONO LO CONTRARIO !!!\n\nLos campos que ocasionarón este mensaje son:\n";
        //1)	Uso de medicamentos como parte del tratamiento contra la dependencia de opioides
        if (opiod.value != 4  && ddlDSMVDiagDual.value != "1") {
            campos += "\u2022Seleccionó medicamento para opíaceos\n";
            flagConcurrente = false;
        }
        //2) Episodios anteriores de cualquier servicio de tratamiento de uso de sustancias
        if (ddlPreviosSustancias.value != "99" && ddlPreviosSustancias.value != "1" && ddlDSMVDiagDual.value != "1") {
            campos += "\u2022Número de episodios/servicios de tratamiento que ha recibido anteriormente [TEDS] de Abuso de Sustancia\n";
            flagConcurrente = false;
        }
        //3)	Participación en reuniones de grupos de apoyo o auto-ayuda enfocados en la recuperación de uso de sustancias durante los pasados 30 días
        if (ddlFreq_AutoAyuda.value != '1' && ddlDSMVDiagDual.value != "1") {
            campos += "\u2022Seleccionó que ha participado de reuniones de grupos de auto-ayuda durante los pasados 30 días\n";
            flagConcurrente = false;
        }
        //4)	Diagnósticos de uso de sustancias
        if (ClinHDSus.value != '761' && ddlDSMVDiagDual.value != "1") {
            campos += "\u2022Seleccionó un diagnóstico de abuso de sustancia\n";
            flagConcurrente = false;
        }
       // 5) Campos relacionados a utilización de sustancias
        if (ddlDrogaPrim.value != sustanciasList.Noaplica && ddlDrogaPrim.value != sustanciasList.Tabacocigarrillo &&  ddlDSMVDiagDual.value != "1") {
            campos += "\u2022Seleccionó una droga\n";
            flagConcurrente = false;

        }

        if (flagConcurrente == false) {
            alert(message + campos);
        }

        if (ClinHDSus.value == '761' && opiod.value == 4 && (ddlPreviosSustancias.value == "99" || ddlPreviosSustancias.value == "1") && ddlFreq_AutoAyuda.value == '1' && (ddlDrogaPrim.value == sustanciasList.Noaplica || ddlDrogaPrim.value == sustanciasList.Tabacocigarrillo) && ddlDSMVDiagDual.value == "1") {
            alert("!!! !!! ESTE PERFIL DE SALUD MENTAL REFLEJA QUE NO ES CONCURRENTE Y USTED SELECCIONO LO CONTRARIO !!!");
            return false;
        }

        return flagConcurrente;
        
    }
    else if (ddlNivelCuidadoSustancias.value != "99") {

        var message = "!!! ESTE PERFIL DE ABUSO DE SUSTANCIA REFLEJA QUE ES CONCURRENTE Y USTED SELECCIONO LO CONTRARIO !!!\n\nLos campos que ocacionarón este mensaje son: \n";
        var flagConcurrente = true;

        //1)	Diagnóstico de salud mental
        if ((GAF.value != "") && ddlDSMVDiagDual.value != "1") {

            campos += "\u2022Entró algún valor en Funcionamiento Global\n";

            flagConcurrente = false;
        }


        //2)	Medidas de Funcionamiento Global - CGAS
        if ( ClinHD.value != '761' && ddlDSMVDiagDual.value != "1") {
            campos += "\u2022Entró algún valor en Diagnostico Primario\n";
            flagConcurrente = false;
        }



        if (flagConcurrente == false) {
            alert(message + campos);
        }

        if (GAF.value == "" && ClinHD.value == '761' && ddlDSMVDiagDual.value == "1")
        {
            alert("!!! ESTE PERFIL DE ABUSO DE SUSTANCIA REFLEJA QUE NO ES CONCURRENTE Y USTED SELECCIONO LO CONTRARIO !!!!!!");
            return false;
        }




        return flagConcurrente;
        

    //if (ddlNivelCuidadoSaludMental.value != "99") {
    //    if (ClinHD.value == '761') {
    //        alert("!!! ESTE PERFIL DE SALUD MENTAL REFLEJA QUE ES DE TIPO SALUD MENTAL Y USTED NO SELECCIONÓ AL MENOS UN(1) DIAGNOSTICO VALIDO !!!");
    //        return false;
    //    }
    //    if (((ddlDrogaPrim.value != sustanciasList.Noaplica && ddlDrogaPrim.value != sustanciasList.Tabacocigarrillo) || opiod.value != 4) && ddlDSMVDiagDual.value != "1") {
    //        if (ddlDrogaPrim.value != sustanciasList.Noaplica && ddlDrogaPrim.value != sustanciasList.Tabacocigarrillo) {
    //            campos += "\u2022Seleccionó una droga\n";
    //        }
    //        if (opiod.value != 4) {
    //            campos += "\u2022Seleccionó medicamento para opíaceos\n";
    //        }
    //        alert("!!! ESTE PERFIL DE SALUD MENTAL REFLEJA QUE ES CONCURRENTE Y USTED SELECCIONO LO CONTRARIO !!!\n\nLos campos que ocacionarón este mensaje son:\n" + campos);
    //        return false;
    //    }
    //    else if ((ddlDrogaPrim.value == sustanciasList.Noaplica || ddlDrogaPrim.value == sustanciasList.Tabacocigarrillo) && opiod.value == 4 && ddlDSMVDiagDual.value == "1") {
    //        campos += "\u2022NO seleccionó una droga\n";
    //        campos += "\u2022NO seleccionó medicamento para opíaceos\n";
    //        return confirm("!!! ESTE PERFIL DE SALUD MENTAL REFLEJA QUE NO ES CONCURRENTE Y USTED SELECCIONO LO CONTRARIO !!!\n\nLos campos que ocacionarón este mensaje son:\n" + campos + "\n\nDesea registrar el perfil?");
    //    }
    //    else {
    //        return true;
    //    }
    //}
    //else if (ddlNivelCuidadoSustancias.value != "99") {

    //    if ((GAF.value != "") && ddlDSMVDiagDual.value != "1") {

    //        campos += "\u2022Entró algún valor en Funcionamiento Global\n";

    //        alert("!!! ESTE PERFIL DE ABUSO DE SUSTANCIA REFLEJA QUE ES CONCURRENTE Y USTED SELECCIONO LO CONTRARIO !!!\n\nLos campos que ocacionarón este mensaje son:\n" + campos);
    //        return false;
    //    }
    //    else if ((GAF.value == "" || ClinHD.value == '761') && ddlDSMVDiagDual.value == "1") {
    //        if (ClinHD.value == '761') {
    //            campos += "\u2022NO entró algún valor en Diagnostico Primario\n";
    //        }
    //        if (GAF.value == "") {
    //            campos += "\u2022NO entró algún valor en Funcionamiento Global\n";
    //        }
    //        return confirm("!!! ESTE PERFIL DE ABUSO DE SUSTANCIA REFLEJA QUE ES CONCURRENTE Y USTED SELECCIONO LO CONTRARIO !!!\n\nLos campos que ocacionarón este mensaje son:\n" + campos + "\n\nDesea registrar el perfil?");
    //    }
    //    else {
    //        return true;
    //    }
    }
}


function chkCelular1() {
    var chkCelular1 = document.getElementById("mainBodyContent_WucDatosPersonales_chkCelular1");
    var txtCelular1 = document.getElementById("mainBodyContent_WucDatosPersonales_txtCelular1");
    var rfvCelular1 = document.getElementById("mainBodyContent_WucDatosPersonales_rfvCelular1");


    if (chkCelular1.checked == true) {
        txtCelular1.disabled = false;
        ValidatorEnable(rfvCelular1, true);
    }
    else {
        txtCelular1.value = "";
        txtCelular1.disabled = true;
        ValidatorEnable(rfvCelular1, false);

    }
}

function chkCelular2() {
    var chkCelular = document.getElementById("mainBodyContent_WucDatosPersonales_chkCelular2");
    var txtCelular = document.getElementById("mainBodyContent_WucDatosPersonales_txtCelular2");
    var rfvCelular = document.getElementById("mainBodyContent_WucDatosPersonales_rfvCelular2");


    if (chkCelular.checked == true) {
        txtCelular.disabled = false;
        ValidatorEnable(rfvCelular, true);
    }
    else {
        txtCelular.value = "";
        txtCelular.disabled = true;
        ValidatorEnable(rfvCelular, false);

    }
}





function ValidateEmail(input) {

    var validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

    if (input.value != "") {

        if (input.value.match(validRegex)) {
            return true;

        } else {

            alert("Favor de revisar el formato del email.");
            return false;
        }

    }

}

function ChkEmailClick(flag) {
    var chk1 = document.getElementById("mainBodyContent_WucDatosPersonales_chkEmail");
    var txtbox1 = txtEmail1 = document.getElementById("mainBodyContent_WucDatosPersonales_txtEmail1");
    var validator1 = document.getElementById("mainBodyContent_WucDatosPersonales_rfvEmail1");

    var chk2 = document.getElementById("mainBodyContent_WucDatosPersonales_chkEmail2");
    var txtbox2 = txtEmail1 = document.getElementById("mainBodyContent_WucDatosPersonales_txtEmail2");
    var validator2 = document.getElementById("mainBodyContent_WucDatosPersonales_rfvEmail2");


    switch (flag) {
        case "1":

            if (chk1.checked == true) {
                txtbox1.disabled = false;
                txtbox1.value = "";
                ValidatorEnable(validator1, true);
            }
            else {
                txtbox1.disabled = true;
                txtbox1.value = "";
                ValidatorEnable(validator1, false);

                if (chk2.checked == true) {
                    chk2.checked = false;
                    txtbox2.disabled = true;
                    txtbox2.value = "";
                    ValidatorEnable(validator2, false);
                }
            }
            break;


        case "2":

            if (chk1.checked == false || ValidateEmail(txtbox1) == false) {
                chk2.checked = false;
                txtbox2.disabled = true;
                txtbox2.value = "";
                ValidatorEnable(validator2, false);

            }
            else if (chk2.checked == true) {
                txtbox2.disabled = false;
                txtbox2.value = "";
                ValidatorEnable(validator2, true);
            }
            else {
                txtbox2.disabled = true;
                txtbox2.value = "";
                ValidatorEnable(validator2, false);
            }
            break;
    }
}


function EmailValidator(flag) {
    var txtbox1 = txtEmail1 = document.getElementById("mainBodyContent_WucDatosPersonales_txtEmail1");

    var chk2 = document.getElementById("mainBodyContent_WucDatosPersonales_chkEmail2");
    var txtbox2 = document.getElementById("mainBodyContent_WucDatosPersonales_txtEmail2");
    var validator2 = document.getElementById("mainBodyContent_WucDatosPersonales_rfvEmail2");


    switch (flag) {
        case "1":
            ValidateEmail(txtbox1);
            break;

        case "2":

            if (txtbox1.value == txtbox2.value) {
                alert("Email 1 y email 2 son semejantes.");
                txtbox2.value = "";
                ValidatorEnable(validator2, true);


            }
            else if (txtbox1.value == "") {
                alert("Favor de completar el primer email.");
                txtbox2.value = "";
                txtbox2.disable = true;
                chk2.checked = false;
                ValidatorEnable(validator2, false);

            }
            else { ValidateEmail(txtbox2); }

            break;

    }


}



var saving = false;
function validate() {

    var isValid = Page_ClientValidate();

    if (!saving) {
        if (isValid) {
            var a = validateCOOCURRING();
            if (!a) {
                return a;
            }
            saving = true;
            scrollToTop();
            showUnlosableModal();
            hideTheButtons();
            enableTheDisabled();
        }
        else {
            scrollToTheError();
        }
        return isValid;
    }
    else { return false; }
}


function validateSSN(source, arguments)
{
    var ssn1 = document.getElementById("mainBodyContent_txtNSS1");
    var ssn2 = document.getElementById("mainBodyContent_txtNSS2");
    var ssn3 = document.getElementById("mainBodyContent_txtNSS3");
    var lblMensaje = document.getElementById("lblSSN");


    var ssn = ssn1.value + ssn2.value + ssn3.value;


    //3 Posibilidades

    //Seg Social Completo
    //Seg Social Parcial (Ultimos 4 digitos)
    //Seg Social En blanco 
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    var mode = urlParams.get('accion');


    var pattern1 = /^(\d{3}\d{2}|\*{3}\*{2})\d{4}$/;
    let pattern2 = /^(\d{3}\d{2}|\*{3}\*{2}|SSS\d{2})\d{4}$/;
    var result = false;

    if (mode == "registrar")
    {

        if (ssn == "")
        {
            result = true;
        } else
        {
            result = pattern1.test(ssn);
        }
    }
    else
    {

        if (ssn == "")
        {
            result = false;
        }
        else
        {
            result = pattern2.test(ssn);
        }
    }

    if (result == false)
    {
        lblMensaje.innerText = "Formato Incorrecto";
    }
    else
    {
        lblMensaje.innerText = "";
    }

    arguments.IsValid = result;
    return result;


}










function validatePaciente() {


   
    var isValid = Page_ClientValidate();
    if (!saving) {
        if (isValid) {
            saving = true;
            showUnlosableModalPaciente();
            hideTheButtonsPacientes();
            enableTheDisabled();
        }
        else {
            scrollToTheError();
        }
        return isValid;
    }
    else { return false; }
}
function validateGAF(txtDSMVFnGlobal) {
    try {
        var GAF = document.getElementById("mainBodyContent_" + txtDSMVFnGlobal);
        if (parseInt(GAF.value) > 100) {
            alert("El valor insertado es invalido. Favor de insertar un número menor de 100");
            GAF.value = "";
            GAF.focus();
        }
        else if (parseInt(GAF.value) < 0) {
            alert("El valor insertado es invalido. Favor de insertar un número positivo");
            GAF.value = "";
            GAF.focus();
        }
    }
    catch (ex) { alert(ex.message); }
}
function scrollToTheError() { $('html,body').animate({ scrollTop: $('.rightFloatAsterisk,.leftFloatAsterisk,.asterisk').filter(':visible').first().offset().top - 5 }, 500); }
function scrollToTop() { $('html,body').animate({ scrollTop: 0 }, 250); }
function enableTheDisabled() { $("select").removeAttr("disabled"); $("input").removeAttr("disabled"); }
function hideTheButtons() { $("#mainBodyContent_btnGuardarCambios").hide(); $("#mainBodyContent_btnRegistrar").hide(); }
function hideTheButtonsPacientes() { $("#mainBodyContent_btnActualizarPersona").hide(); $("#mainBodyContent_btnRegistrar").hide(); }
function showUnlosableModal() {
    switch ($("#frmActionMode").val()) {
        case ("create"): $("#mensageModal").text("Guardando perfil de admisión."); break;
        case ("update"): $("#mensageModal").text("Actualizando perfil de admisión."); break;
        default: $("#mensageModal").text("Guardando perfil."); break;
    }
    $("#myModalUnclosable").modal('show');
}
function showUnlosableModalPaciente() {
    switch ($("#frmActionMode").val()) {
        case ("registrar"): $("#mensageModal").text("Registrando paciente."); break;
        case ("editar"): $("#mensageModal").text("Actualizando paciente."); break;
        default: $("#mensageModal").text("Guardando paciente."); break;
    }
    $("#myModalUnclosable").modal('show');
}

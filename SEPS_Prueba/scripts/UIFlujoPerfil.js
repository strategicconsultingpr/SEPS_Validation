$(document).ready(function () {
    frmActionModeSetup();
    startupFunctions();
    if (IsPostBack() == "False") {
        var urlParams = new URLSearchParams(window.location.search);
        if (urlParams.get('accion') == 'update') {
            CO_Tipo_Update();
        }
        else{
            CO_Tipo();
        }
    }
});
function startupFunctions() {
    var urlParams = new URLSearchParams(window.location.search);
    try {
        changeTabOrder();
        setupFechaPerfilLabel();
      
        if (urlParams.get('accion') != 'update') {
            ddlCondLaboral();
            ddlArrestado30();
            ddlDrogaPrimF();
            ddlDrogaSecF();
            ddlDSMVPsicoAmbiPrim();
            ddlDSMVPsicoAmbiSec();
            ddlRazonAlta();
            if (IsPostBack() == "False") {
                ddlGrado();
            }
            TakeHomeParticipa();
           
        }
        ddlDrogaSecF();


        ddlDrogaChangeEvent();
        ddlDrogaChange("#mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim", 'ddlViaPrim', 'ddlFrecPrim', 'txtEdadPrim', 'ddlToxicologia1');
        ddlDrogaChange("#mainBodyContent_WucEpisodioPerfil_ddlDrogaSec", 'ddlViaSec', 'ddlFrecSec', 'txtEdadSec', 'ddlToxicologia2');
        ddlDrogaChange("#mainBodyContent_WucEpisodioPerfil_ddlDrogaTerc", 'ddlViaTerc', 'ddlFrecTerc', 'txtEdadTerc', 'ddlToxicologia3');


        $("#mainBodyContent_WucDatosDemograficosPerfil_lblFePerfil").val($("#mainBodyContent_WucOtrosDatosPerfil_ddlMes").val() + '/' + $("#mainBodyContent_WucOtrosDatosPerfil_ddlDía").val() + '/' + $("#mainBodyContent_WucOtrosDatosPerfil_txtAño").val());

        $("#mainBodyContent_WucOtrosDatosPerfil_ddlMes").change(function () {
            $("#mainBodyContent_WucDatosDemograficosPerfil_lblFePerfil").val($("#mainBodyContent_WucOtrosDatosPerfil_ddlMes").val() + '/' + $("#mainBodyContent_WucOtrosDatosPerfil_ddlDía").val() + '/' + $("#mainBodyContent_WucOtrosDatosPerfil_txtAño").val());
        });

        $("#mainBodyContent_WucOtrosDatosPerfil_ddlDía").change(function () {
            $("#mainBodyContent_WucDatosDemograficosPerfil_lblFePerfil").val($("#mainBodyContent_WucOtrosDatosPerfil_ddlMes").val() + '/' + $("#mainBodyContent_WucOtrosDatosPerfil_ddlDía").val() + '/' + $("#mainBodyContent_WucOtrosDatosPerfil_txtAño").val());

        });

        $("#mainBodyContent_WucOtrosDatosPerfil_txtAño").focusout(function () {
            $("#mainBodyContent_WucDatosDemograficosPerfil_lblFePerfil").val($("#mainBodyContent_WucOtrosDatosPerfil_ddlMes").val() + '/' + $("#mainBodyContent_WucOtrosDatosPerfil_ddlDía").val() + '/' + $("#mainBodyContent_WucOtrosDatosPerfil_txtAño").val());

        });
        

        //CO_Tipo();
        
    }
    catch (ex) {
        throw ex;
    }
}


/**
 * Cambios por Jose A. Ramos De La Cruz
 * Fecha: 4/21/20
 * Proposito: Activar y desactivar las opciones de no aplica.
 * */

function ddlDrogaChangeEvent() {
    $("#mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim").change(function () {
        var value = $("#mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim").val();
        ddlDrogaChange(value, 'ddlViaPrim', 'ddlFrecPrim', 'txtEdadPrim', 'ddlToxicologia1');
    });

    $("#mainBodyContent_WucEpisodioPerfil_ddlDrogaSec").change(function () {
        var value = $("#mainBodyContent_WucEpisodioPerfil_ddlDrogaSec").val();
        ddlDrogaChange(value, 'ddlViaSec', 'ddlFrecSec', 'txtEdadSec', 'ddlToxicologia2');
    });

    $("#mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim").change(function () {
        var value = $("#mainBodyContent_WucEpisodioPerfil_ddlDrogaTerc").val();
        ddlDrogaChange(value, 'ddlViaTerc', 'ddlFrecTerc', 'txtEdadTerc', 'ddlToxicologia3');
    });
}

function ddlDrogaChange(value, via, frec, edad, tox) {

    if (value != sustanciasList.Noaplica || value != sustanciasList.Noinformó || value != sustanciasList.Nousaactualmente) {

        $('#mainBodyContent_WucEpisodioPerfil_' + via + ' option[value=99]').removeAttr('disabled').hide();
        $('#mainBodyContent_WucEpisodioPerfil_' + frec + ' option[value=99]').removeAttr('disabled').hide();
        $('#mainBodyContent_WucEpisodioPerfil_' + edad + ' option[value=126]').removeAttr('disabled').hide();
        $('#mainBodyContent_WucEpisodioPerfil_' + tox + ' option[value=99]').removeAttr('disabled').hide();
    } else {

        $('#mainBodyContent_WucEpisodioPerfil_' + via + ' option[value=99]').removeAttr('disabled').show();
        $('#mainBodyContent_WucEpisodioPerfil_' + frec + ' option[value=99]').removeAttr('disabled').show();
        $('#mainBodyContent_WucEpisodioPerfil_' + edad + ' option[value=126]').removeAttr('disabled').show();
        $('#mainBodyContent_WucEpisodioPerfil_' + tox + ' option[value=99]').removeAttr('disabled').show();
    }


}

function IsPostBack() {
    return document.getElementById('postbackControl').value;

}

function txtFumadoChange(input) {
    var validator = document.getElementById("mainBodyContent_WucEpisodioPerfil_rfvTxtFumado");

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


function diagnosticoConcurrente(source, arguments) {
    var ddlDSMVDiagDual = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDSMVDiagDual");
    var hDSMVClinPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_hDSMVClinPrim");
    var hDSMVSusPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_hDSMVSusPrim");

    validateCOOCURRING();


}


function CO_Tipo() {
    try {
        var CO_Tipo = document.getElementById("mainBodyContent_WucEpisodioPerfil_CO_Tipo");
        var ddlNivelCuidadoSaludMental = document.getElementById("mainBodyContent_WucEpisodioPerfil_hNivelSM");
        var ddlNivelCuidadoSustancias = document.getElementById("mainBodyContent_WucEpisodioPerfil_hNivelAS");
       
        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim");
        var ddlDrogaSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaSec");
        var ddlDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaTerc");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaPrim");
        var ddlViaSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaSec");
        var ddlViaTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaTerc");
        var ddlFrecPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecPrim");
        var ddlFrecSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecSec");
        var ddlFrecTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecTerc");
        var txtEdadPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadPrim");
        var txtEdadSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadSec");
        var txtEdadTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadTerc");
        var GAF = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtDSMVFnGlobal");

        var ddlToxicologia1 = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlToxicologia1");
        var ddlToxicologia2 = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlToxicologia2");
        var ddlToxicologia3 = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlToxicologia3");
        //alert(ddlNivelCuidadoSustancias.value);
        //alert(ddlNivelCuidadoSaludMental.value);
        if (ddlNivelCuidadoSustancias.value != "99" && ddlNivelCuidadoSaludMental.value == "99") {
           
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

            ddlToxicologia2.value = "99";
            ddlToxicologia2.disabled = true;
            ddlToxicologia3.value = "99";
            ddlToxicologia3.disabled = true;
            //Substancia
            //GAF.disabled = true;
        }
        else if (ddlNivelCuidadoSustancias.value == "99" && ddlNivelCuidadoSaludMental.value != "99") {
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

            ddlToxicologia1.value = "99";
            ddlToxicologia1.disabled = true;
            ddlToxicologia2.value = "99";
            ddlToxicologia2.disabled = true;
            ddlToxicologia3.value = "99";
            ddlToxicologia3.disabled = true;
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
            //GAF.disabled = false;
        }
    }
    catch (ex) {
       
    }
    //AjustesNiveldeCuidado();
}

function CO_Tipo_Update() {
    try {
        var CO_Tipo = document.getElementById("mainBodyContent_WucEpisodioPerfil_CO_Tipo");
        var ddlNivelCuidadoSaludMental = document.getElementById("mainBodyContent_WucEpisodioPerfil_hNivelSM");
        var ddlNivelCuidadoSustancias = document.getElementById("mainBodyContent_WucEpisodioPerfil_hNivelAS");

        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim");
        var ddlDrogaSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaSec");
        var ddlDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaTerc");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaPrim");
        var ddlViaSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaSec");
        var ddlViaTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaTerc");
        var ddlFrecPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecPrim");
        var ddlFrecSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecSec");
        var ddlFrecTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecTerc");
        var txtEdadPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadPrim");
        var txtEdadSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadSec");
        var txtEdadTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadTerc");
        var GAF = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtDSMVFnGlobal");
       
       
        if ((ddlDrogaTerc.value == sustanciasList.Noaplica) || (ddlDrogaTerc.value == sustanciasList.Nousaactualmente)) {
            if ((ddlDrogaSec.value == sustanciasList.Noaplica) || (ddlDrogaSec.value == sustanciasList.Nousaactualmente)) {
                ddlDrogaTerc.disabled = true;
            }
            ddlViaTerc.disabled = true;
            ddlFrecTerc.disabled = true;
            txtEdadTerc.disabled = true;
        }

        if ((ddlDrogaSec.value == sustanciasList.Noaplica) || (ddlDrogaSec.value == sustanciasList.Nousaactualmente)) {
            if ((ddlDrogaPrim.value == sustanciasList.Noaplica) || (ddlDrogaPrim.value == sustanciasList.Nousaactualmente)) {
                ddlDrogaSec.disabled = true;
            }
            ddlViaSec.disabled = true;
            ddlFrecSec.disabled = true;
            txtEdadSec.disabled = true;
        }

        if (ddlDrogaPrim.value == sustanciasList.Noaplica) {
            ddlViaPrim.disabled = true;
            ddlFrecPrim.disabled = true;
            txtEdadPrim.disabled = true;
        }


    }
    catch (ex) {

    }
    
}

/**
 * File: UIFlujoPerfil.js
 * Fecha: 11/MAR/2021
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

        if (e.keyCode == 9 && $("#mainBodyContent_WucEpisodioPerfil_btnAgregarPracticasBasadasEvidencia").is(':enabled')) { document.getElementById("mainBodyContent_WucEpisodioPerfil_lbxPracticasBasadasEvidenciaSeleccion").focus(); e.preventDefault(); }
        else if (e.keyCode == 9 && $("#mainBodyContent_WucTakeHome_ddlTHBelong").is(':enabled')) { document.getElementById("mainBodyContent_WucTakeHome_ddlTHBelong").focus(); e.preventDefault(); }
        else if (e.keyCode == 9 && $("#mainBodyContent_WucDatosAlta_ddlRazonAlta").is(':enabled')) { document.getElementById("mainBodyContent_WucDatosAlta_ddlRazonAlta").focus(); e.preventDefault(); }
        else document.getElementById("mainBodyContent_WucDatosEvaluacion_txtComentarios").focus(); e.preventDefault();

    } else {
        for (var j = i; j >= 0; j--) {
            if ($("#" + arr[j]).is(':enabled')) {
                $("#" + arr[j]).focus(); e.preventDefault(); return;
            }
        }

    }

}

/**
 * File: UIFlujoPerfil.js
 * Fecha: 11/MAR/2021
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
        var prefix = "mainBodyContent_WucEpisodioPerfil_";
        var inputs = [prefix + "ddlDrogaPrim"/*0*/, prefix + "ddlViaPrim"/*1*/, prefix + "ddlFrecPrim"/*2*/, prefix + "txtEdadPrim"/*3*/, prefix + "ddlToxicologia1"/*4*/,
        prefix + "ddlDrogaSec"/*5*/, prefix + "ddlViaSec"/*6*/, prefix + "ddlFrecSec"/*7*/, prefix + "txtEdadSec"/*8*/, prefix + "ddlToxicologia2"/*9*/,
        prefix + "ddlDrogaTerc"/*10*/, prefix + "ddlViaTerc"/*11*/, prefix + "ddlFrecTerc"/*12*/, prefix + "txtEdadTerc"/*13*/, prefix + "ddlToxicologia3"/*14*/];
        if (e.shiftKey) {
            switch (e.currentTarget.id) {

                case (prefix + "ddlDrogaPrim"):
                    if ($("#" + prefix + "txtNrFumado").is(':enabled')) {
                        $("#" + prefix + "txtNrFumado").focus(); e.preventDefault(); return;
                    }
                    else { $("#" + prefix + "ddlInFumado").focus(); e.preventDefault(); return; }
                    break;

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
                    var div = document.getElementById("mainBodyContent_WucEpisodioPerfil_Hogar_DIV");

                    if (div.style.visibility == 'hidden') {
                        TabFocusAbusoDeSustancia(true, inputs, 1, e);
                    }
                    else {
                        $('#mainBodyContent_WucEpisodioPerfil_txtDrogaPrim').focus();
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
                    var div = document.getElementById("mainBodyContent_WucEpisodioPerfil_Hogar2_DIV");

                    if (div.style.visibility == 'hidden') {
                        TabFocusAbusoDeSustancia(true, inputs, 6, e);
                    }
                    else {
                        $('#mainBodyContent_WucEpisodioPerfil_txtDrogaSec').focus();
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
                    var div = document.getElementById("mainBodyContent_WucEpisodioPerfil_Hogar3_DIV");

                    if (div.style.visibility == 'hidden') {
                        TabFocusAbusoDeSustancia(true, inputs, 11, e);
                    }
                    else {
                        $('#mainBodyContent_WucEpisodioPerfil_txtDrogaTerc').focus();
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
                    if (e.keyCode == 9 && $("#mainBodyContent_WucEpisodioPerfil_btnAgregarPracticasBasadasEvidencia").is(':enabled')) { document.getElementById("mainBodyContent_WucEpisodioPerfil_lbxPracticasBasadasEvidenciaSeleccion").focus(); e.preventDefault(); }
                    else if (e.keyCode == 9 && $("#mainBodyContent_WucTakeHome_ddlTHBelong").is(':enabled')) { document.getElementById("mainBodyContent_WucTakeHome_ddlTHBelong").focus(); e.preventDefault(); }
                    else if (e.keyCode == 9 && $("#mainBodyContent_WucDatosAlta_ddlRazonAlta").is(':enabled')) { document.getElementById("mainBodyContent_WucDatosAlta_ddlRazonAlta").focus(); e.preventDefault(); }
                    else document.getElementById("mainBodyContent_WucDatosEvaluacion_txtComentarios").focus(); e.preventDefault();
                    break;

                default: break;
            }
        }
    }
}
function setupFechaPerfilLabel() {
    try {
        switch (currentPageName().toUpperCase()) {
            case ("FRMEVALUACION"): $("#fechaPerfil").html("Fecha de evaluación:"); break;
            case ("FRMALTA"): $("#fechaPerfil").html("Fecha de alta:"); break;
            default: break;
        }
    }
    catch (e) { }
}
function currentPageName() {
    var path = window.location.pathname;
    var page = path.substr(path.lastIndexOf("/") + 1).split(".", 1);
    var formname = page[0].toString();
    return formname;
}
function changeTabOrder() {
    try {
        var prefix = "#mainBodyContent_WucEpisodioPerfil_";
        $(prefix + "txtDSMVOtrasObs").on('keydown', function (e) { if (e.keyCode == 9) { document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDSMVDiagDual").focus(); e.preventDefault(); } });
        $(prefix + "ddlDSMVDiagDual").on('keydown', function (e) { if (e.keyCode == 9) { document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlInFumado").focus(); e.preventDefault(); } });

        /*
         * Cambios por Jose A Ramos De la Cruz
         * Proposito: Organizar los tab index en el orden correcto
         * Fecha: 3/10/2021
         */
        $(prefix + "ddlInFumado").on('keydown', function (e)
        {
            if (e.keyCode == 9 && document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlInFumado").value != "1") { document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim").focus(); e.preventDefault(); }
            else
            {
                document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecuenciaFumado").focus(); e.preventDefault();
            }

        });
        $(prefix + "txtNrFumado").on('keydown', function (e) { if (e.keyCode == 9) { document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim").focus(); e.preventDefault(); } });

        $(prefix + "ddlNivelRecuperacion").on('keydown', function (e) {
            if (e.keyCode == 9 && !$("#mainBodyContent_WucEpisodioPerfil_txtHogar").is(':enabled')) { document.getElementById("mainBodyContent_WucEpisodioPerfil_lbxCondicionesDiagnosticadasSeleccion").focus(); e.preventDefault(); }
            else {
                document.getElementById("mainBodyContent_WucEpisodioPerfil_txtHogar").focus(); e.preventDefault();
            }

        });

        $(prefix + "txtHogar").on('keydown', function (e) { if (e.keyCode == 9) { document.getElementById("mainBodyContent_WucEpisodioPerfil_lbxCondicionesDiagnosticadasSeleccion").focus(); e.preventDefault(); } });

        //


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
    }
    catch (ex) { }
}

function ddInFumadoChange() {
    var dropdown = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlInFumado");
    var ddlFrecuenciaFumado = "#mainBodyContent_WucEpisodioPerfil_ddlFrecuenciaFumado";
    var txtNrFumado = "#mainBodyContent_WucEpisodioPerfil_txtNrFumado";


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
            $(ddlFrecuenciaFumado).val("3");
            $(txtNrFumado).val("0");
            $(ddlFrecuenciaFumado + ' option[value=3]').removeAttr('disabled').show();



        }
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
  
function dsmivShowHideClick() {
    try {
        var showContentButton = document.getElementById("dsmiv_showContentButton");
        if (showContentButton.getAttribute("aria-expanded") == "false") {
            showContentButton.innerText = "Esconder contenido";
        }
        else {
            showContentButton.innerText = "Mostrar contenido";
        }
    }
    catch (ex) { }
}

    function typeDSMV() {
        var tipo = document.getElementById("txtFiltroTipo");

        if (tipo.value == "SUST")
        {
            try {
            var lbx = document.getElementById("lbxDSMV");
            var txtDescripcion = document.getElementById("txtDescripcion");
            var txtDescripcionHidden = document.getElementById("txtDescripcionHidden");
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

            if (txtDescripcion.value == "mainBodyContent_WucEpisodioPerfil_txtDSMVSusTer" && lbx.value != '761') {
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
            else if (txtDescripcion.value == "mainBodyContent_WucEpisodioPerfil_txtDSMVSusSec") {
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
            else if (txtDescripcion.value == "mainBodyContent_WucEpisodioPerfil_txtDSMVSusPrim" && lbx.value == '761' && ClinHD1.value != '761') {
                ClinHD1Padre.value = '761';
                ClinTxt1Padre.value = "761 - NO SE RECOPILA LA INFORMACIÓN";

                if (ClinHD2.value != '761') {
                    ClinHD2Padre.value = '761';
                    ClinTxt2Padre.value = "761 - NO SE RECOPILA LA INFORMACIÓN";
                }
            }

            if (txtDescripcion.value == "mainBodyContent_WucEpisodioPerfil_txtDSMVRMTer" && lbx.value != '761') {
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
            else if (txtDescripcion.value == "mainBodyContent_WucEpisodioPerfil_txtDSMVRMSec") {
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
            else if (txtDescripcion.value == "mainBodyContent_WucEpisodioPerfil_txtDSMVRMPrim" && lbx.value == '761' && RMHD1.value != '761') {
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
        else
        {
            DSMV();
        }
    }
function DSMV() {
    try {
        var lbx = document.getElementById("lbxDSMV");
        var txtDescripcion = document.getElementById("txtDescripcion");
        var txtDescripcionHidden = document.getElementById("txtDescripcionHidden");
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

        if (txtDescripcion.value == "mainBodyContent_WucEpisodioPerfil_txtDSMVClinTer" && lbx.value != '761') {
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
        else if (txtDescripcion.value == "mainBodyContent_WucEpisodioPerfil_txtDSMVClinSec") {
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
        else if (txtDescripcion.value == "mainBodyContent_WucEpisodioPerfil_txtDSMVClinPrim" && lbx.value == '761' && ClinHD1.value != '761') {
            ClinHD1Padre.value = '761';
            ClinTxt1Padre.value = "761 - NO SE RECOPILA LA INFORMACIÓN";

            if (ClinHD2.value != '761') {
                ClinHD2Padre.value = '761';
                ClinTxt2Padre.value = "761 - NO SE RECOPILA LA INFORMACIÓN";
            }
        }

        if (txtDescripcion.value == "mainBodyContent_WucEpisodioPerfil_txtDSMVRMTer" && lbx.value != '761') {
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
        else if (txtDescripcion.value == "mainBodyContent_WucEpisodioPerfil_txtDSMVRMSec") {
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
        else if (txtDescripcion.value == "mainBodyContent_WucEpisodioPerfil_txtDSMVRMPrim" && lbx.value == '761' && RMHD1.value != '761') {
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
function showDSMV(filtro, txtDescripcion, txtDescripcionHidden, tipoDescripcion) {
    try {
        var ClinPrim = document.getElementById('mainBodyContent_WucEpisodioPerfil_hDSMVClinPrim').value;
        var ClinSec = document.getElementById('mainBodyContent_WucEpisodioPerfil_hDSMVClinSec').value;
        //var RMPrim = document.getElementById('mainBodyContent_WucEpisodioPerfil_hDSMVRMPrim').value;
        //var RMSec = document.getElementById('mainBodyContent_WucEpisodioPerfil_hDSMVRMSec').value;

        if (txtDescripcion == "mainBodyContent_WucEpisodioPerfil_txtDSMVClinSec") {
            if (ClinPrim == '761') {
                alert("Debe seleccionar un diagnóstico primario válido");
                return;
            }
        }
        else if (txtDescripcion == "mainBodyContent_WucEpisodioPerfil_txtDSMVClinTer") {
            if (ClinSec == '761') {
                alert("Debe seleccionar un diagnóstico secundario válido");
                return;
            }
        }
        //if (txtDescripcion == "mainBodyContent_WucEpisodioPerfil_txtDSMVRMSec") {
        //    if (RMPrim == '761') {
        //        alert("Debe seleccionar un diagnóstico primario válido");
        //        return;
        //    }
        //}
        //else if (txtDescripcion == "mainBodyContent_WucEpisodioPerfil_txtDSMVRMTer") {
        //    if (RMSec == '761') {
        //        alert("Debe seleccionar un diagnóstico secundario válido");
        //        return;
        //    }
        //}

        var url = 'frmdsmi_v.aspx?' + 'txtfiltro=' + filtro + '&txtDescripcion=' + txtDescripcion + '&txtDescripcionHidden=' + txtDescripcionHidden + '&tipoDescripcion=' + tipoDescripcion
        var ventana = window.open(url, "list", "width=620,height=280,resizable=yes,toolbar=no,status=no,menubar=no");
    }
    catch (ex) { }
}

function showSusDSMV(filtro, txtDescripcion, txtDescripcionHidden, tipoDescripcion) {

    try {
        var ClinPrim = document.getElementById('mainBodyContent_WucEpisodioPerfil_hDSMVSusPrim').value;
        var ClinSec = document.getElementById('mainBodyContent_WucEpisodioPerfil_hDSMVSusSec').value;
        if (txtDescripcion == "mainBodyContent_WucEpisodioPerfil_txtDSMVSusSec") {
            if (ClinPrim == '761') {
                alert("Debe seleccionar un diagnóstico primario válido");
                return;
            }
        }
        else if (txtDescripcion == "mainBodyContent_WucEpisodioPerfil_txtDSMVSusTer") {
            if (ClinSec == '761') {
                alert("Debe seleccionar un diagnóstico secundario válido");
                return;
            }
        }

        var url = 'frmdsmi_v.aspx?' + 'txtfiltro=' + filtro + '&txtDescripcion=' + txtDescripcion + '&txtDescripcionHidden=' + txtDescripcionHidden + '&tipoDescripcion=' + tipoDescripcion
        var ventana = window.open(url, "list", "width=620,height=280,resizable=yes,toolbar=no,status=no,menubar=no");
    }
    catch (ex) { }
}

function ddlCondLaboral() {
    try {
        var ddlCondLaboral = document.getElementById("mainBodyContent_WucDatosDemograficosPerfil_ddlCondLaboral");
        var ddlNoFueraLaboral = document.getElementById("mainBodyContent_WucDatosDemograficosPerfil_ddlNoFueraLaboral");
        switch (ddlCondLaboral.value) {
            case ("5"):
                if (ddlNoFueraLaboral.disabled) {
                    ddlNoFueraLaboral.disabled = false;
                    ddlNoFueraLaboral.value = 0;
                }
                break;
            default:
                ddlNoFueraLaboral.value = 99;
                ddlNoFueraLaboral.disabled = true;
                break;
        }
    }
    catch (ex) { }
}
function ddlNoFueraLaboral() {
    try {
        var ddlNoFueraLaboral = document.getElementById("mainBodyContent_WucDatosDemograficosPerfil_ddlNoFueraLaboral");

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
function ddlArrestado30() {
    try {
        var txtArrestos30 = document.getElementById("mainBodyContent_WucDatosDemograficosPerfil_txtArrestos30");
        var rvArrestos30 = document.getElementById("mainBodyContent_WucDatosDemograficosPerfil_rvArrestos30");
        switch (document.getElementById("mainBodyContent_WucDatosDemograficosPerfil_ddlArrestado").value) {
            case ("1")://Sí
                if (txtArrestos30.value == "0") {
                    txtArrestos30.value = "";
                }
                rvArrestos30.minimumvalue = "1";
                txtArrestos30.disabled = false;
                break;
            default://No
                txtArrestos30.value = '0';
                rvArrestos30.minimumvalue = "0";
                txtArrestos30.disabled = true;
                break;
           /* default:
                rvArrestos30.minimumvalue = "0";
                txtArrestos30.disabled = false;
                break; */
        }
    }
    catch (ex) { }
}
function ddlRazonAlta() {
    try {
        var ddlRazonAlta = document.getElementById("mainBodyContent_WucDatosAlta_ddlRazonAlta");
        var ddlCentroReferido = document.getElementById("mainBodyContent_WucDatosAlta_ddlCentroReferido");
        var ddlCategoriasDeCentrosPrivados = document.getElementById("mainBodyContent_WucDatosAlta_ddlCategoriasDeCentrosPrivados");
       
        if (typeof (ddlCategoriasDeCentrosPrivados) != "undefined" && ddlCategoriasDeCentrosPrivados != null) {
            switch (ddlRazonAlta.value) {
                case ("3"):
                    ddlCentroReferido.disabled = false;
                    ddlCategoriasDeCentrosPrivados.disabled = true;
                    ddlCategoriasDeCentrosPrivados.value = 0;
                    break;
                case ("7"):
                    ddlCategoriasDeCentrosPrivados.disabled = false;
                    ddlCentroReferido.disabled = true;
                    ddlCentroReferido.value = 0;
                    break;
                default:
                    ddlCentroReferido.disabled = true;
                    ddlCategoriasDeCentrosPrivados.disabled = true;
                    ddlCentroReferido.value = 0;
                    ddlCategoriasDeCentrosPrivados.value = 0;
                    break;
            }
        }
    }
    catch (ex) { }
}
function TakeHomeParticipa() {
    var frmActionMode = document.getElementById("frmActionMode");
    if (frmActionMode.value == "read") {
        try {
            var lblParticipa = document.getElementById("mainBodyContent_WucTakeHome_lblTHBelong");
            $Participa = $(".Participa");
            $NoParticipa = $(".NoParticipa");
            switch (lblParticipa.innerText) {
                case ("No"): $Participa.hide(); $NoParticipa.show(); break;
                case ("Sí"): $Participa.show(); $NoParticipa.hide(); break
                default: $Participa.hide(); $NoParticipa.hide(); break;
            }
        }
        catch (ex) { }
    }
    else {
        try {
            var ddlParticipa = document.getElementById("mainBodyContent_WucTakeHome_ddlTHBelong");
            var díaFechaEntrada = document.getElementById("mainBodyContent_WucTakeHome_ddlFechaEntradaDía");
            var mesFechaEntrada = document.getElementById("mainBodyContent_WucTakeHome_ddlFechaEntradaMes");
            var añoFechaEntrada = document.getElementById("mainBodyContent_WucTakeHome_txtFechaEntradaAño");
            var díaFechaSalida = document.getElementById("mainBodyContent_WucTakeHome_ddlFechaSalidaDía");
            var mesFechaSalida = document.getElementById("mainBodyContent_WucTakeHome_ddlFechaSalidaMes");
            var añoFechaSalida = document.getElementById("mainBodyContent_WucTakeHome_txtFechaSalidaAño");
            var ddlTHEtapa = document.getElementById("mainBodyContent_WucTakeHome_ddlTHEtapa");
            var txtCantidadBotellas = document.getElementById("mainBodyContent_WucTakeHome_txtCantidadBotellas");
            var ddlFrecuenciaBotellas = document.getElementById("mainBodyContent_WucTakeHome_ddlFrecuenciaBotellas");
            $Participa = $(".Participa");
            $NoParticipa = $(".NoParticipa");
            switch (ddlParticipa.value) {
                case ("1")://Participa
                    díaFechaEntrada.disabled = false;
                    mesFechaEntrada.disabled = false;
                    añoFechaEntrada.disabled = false;
                    díaFechaSalida.disabled = false;
                    mesFechaSalida.disabled = false;
                    añoFechaSalida.disabled = false;
                    ddlTHEtapa.disabled = true;
                    ddlTHEtapa.value = '';
                    ddlTHEtapa.style.visibility = 'hidden';
                    txtCantidadBotellas.disabled = false;
                    ddlFrecuenciaBotellas.disabled = false;
                    $NoParticipa.hide();
                    $Participa.show();
                    break;
                case ("2")://No participa
                    díaFechaEntrada.disabled = true;
                    mesFechaEntrada.disabled = true;
                    añoFechaEntrada.disabled = true;
                    añoFechaEntrada.value = '';
                    díaFechaSalida.disabled = true;
                    mesFechaSalida.disabled = true;
                    añoFechaSalida.disabled = true;
                    añoFechaSalida.value = '';
                    ddlTHEtapa.disabled = true;
                    ddlTHEtapa.value = '';
                    ddlTHEtapa.style.visibility = 'hidden';
                    txtCantidadBotellas.disabled = true;
                    txtCantidadBotellas.value = '';
                    ddlFrecuenciaBotellas.disabled = true;
                    ddlFrecuenciaBotellas.value = '';
                    $Participa.hide();
                    $NoParticipa.show();
                    break;
                default:
                    díaFechaEntrada.disabled = true;
                    mesFechaEntrada.disabled = true;
                    añoFechaEntrada.disabled = true;
                    añoFechaEntrada.value = '';
                    díaFechaSalida.disabled = true;
                    mesFechaSalida.disabled = true;
                    añoFechaSalida.disabled = true;
                    añoFechaSalida.value = '';
                    ddlTHEtapa.disabled = true;
                    ddlTHEtapa.value = '';
                    txtCantidadBotellas.disabled = true;
                    txtCantidadBotellas.value = '';
                    ddlFrecuenciaBotellas.disabled = true;
                    ddlFrecuenciaBotellas.value = '';
                    $Participa.hide();
                    $NoParticipa.hide();
                    break;
            }
        }
        catch (ex) { }
    }
}
function ddlGrado() {
    try {
        var ddlGrado = document.getElementById("mainBodyContent_WucDatosDemograficosPerfil_ddlGrado");
        var ddlDesertor = document.getElementById("mainBodyContent_WucDatosDemograficosPerfil_ddlDesertorEscolar");
        var ddlEducacionEspecial = document.getElementById("mainBodyContent_WucDatosDemograficosPerfil_ddlEducacionEspecial");
        var situacion = document.getElementById("mainBodyContent_WucDatosDemograficosPerfil_ddlSituacionEscolar");

        if (document.getElementById("mainBodyContent_WucDatosDemograficosPerfil_edadPerfil").value != null &&
            document.getElementById("mainBodyContent_WucDatosDemograficosPerfil_edadPerfil").value != "") {
            var edad = parseInt(document.getElementById("mainBodyContent_WucDatosDemograficosPerfil_edadPerfil").value);
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

function ddlDSMVPsicoAmbiPrim() {
    try {
        var ddlDSMVPsicoAmbiPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDSMVPsicoAmbiPrim");
        var ddlDSMVPsicoAmbiSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDSMVPsicoAmbiSec");
        var ddlDSMVPsicoAmbiTer = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDSMVPsicoAmbiTer");
        switch (ddlDSMVPsicoAmbiPrim.value) {
            case ("99"):
                ddlDSMVPsicoAmbiSec.value = 99;
                ddlDSMVPsicoAmbiTer.value = 99;
                ddlDSMVPsicoAmbiSec.disabled = true;
                ddlDSMVPsicoAmbiTer.disabled = true;
                break;
            default:
                ddlDSMVPsicoAmbiSec.value = 0;
                ddlDSMVPsicoAmbiTer.value = 0;
                ddlDSMVPsicoAmbiSec.disabled = false;
                ddlDSMVPsicoAmbiTer.disabled = false;
                break;
        }
    }
    catch (ex) { }
}
function ddlDSMVPsicoAmbiSec() {
    try {
        var ddlDSMVPsicoAmbiSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDSMVPsicoAmbiSec");
        var ddlDSMVPsicoAmbiTer = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDSMVPsicoAmbiTer");
        switch (ddlDSMVPsicoAmbiSec.value) {
            case ("99"):
                ddlDSMVPsicoAmbiTer.value = 99;
                ddlDSMVPsicoAmbiTer.disabled = true;
                break;
            default:
                ddlDSMVPsicoAmbiTer.value = 0;
                ddlDSMVPsicoAmbiTer.disabled = false;
                break;
        }
    }
    catch (ex) { }
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
         var CO_Tipo = document.getElementById("mainBodyContent_WucEpisodioPerfil_CO_Tipo");
         var ddlNivelCuidadoSaludMental = document.getElementById("mainBodyContent_WucEpisodioPerfil_hNivelSM");
         var ddlNivelCuidadoSustancias = document.getElementById("mainBodyContent_WucEpisodioPerfil_hNivelAS");
         var ddlDSMVDiagDual = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDSMVDiagDual");
        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim");
        var ddlDrogaSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaSec");
        var ddlDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaTerc");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaPrim");
        var ddlViaSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaSec");
        var ddlViaTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaTerc");
        var ddlFrecPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecPrim");
        var ddlFrecSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecSec");
        var ddlFrecTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecTerc");
        var txtEdadPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadPrim");
        var txtEdadSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadSec");
         var txtEdadTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadTerc");
         var txtEdadTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtchkEdadPrim");
         var ddlToxicologia1 = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlToxicologia1");
         var ddlToxicologia2 = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlToxicologia2");
         var ddlToxicologia3 = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlToxicologia3");


        


        ddlViaPrim.disabled = false;
         ddlFrecPrim.disabled = false;
         txtEdadPrim.disabled = false;
        

         if (IsPostBack() == "False") {
             ddlFrecPrim.value = "";
             txtEdadPrim.value = "";
         }

         var txtDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtDrogaPrim");

         var hogarDiv = document.getElementById("mainBodyContent_WucEpisodioPerfil_Hogar_DIV");

         if (ddlNivelCuidadoSaludMental.value !== "99" && ddlDrogaPrim.value != sustanciasList.Noaplica) {

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
                txtDrogaPrim.value = "";
                if (IsPostBack() == "False") {
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
                ddlToxicologia1.value = "";}
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
                if (IsPostBack() == "False") {
                    ddlToxicologia1.value = "";
                }
                ddlToxicologia1.disabled = false;
                ddlDrogaSec.disabled = false;
                break;
            case (sustanciasList.Nousaactualmente):
                if (ddlNivelCuidadoSustancias.value != "99") {
                    var a = confirm("Al seleccionar esta opción, significa que el paciente NO utiliza ninguna tipo de droga actualmente. ¿Desea proseguir?");
                    if (a == true) {
                        alert("El paciente NO esta utilizando ninguna droga.");
                        ddlViaPrim.value = viaList.NoAplica;
                        ddlViaPrim.disabled = true;
                        ddlFrecPrim.value = 99;
                        ddlFrecPrim.disabled = true;
                        txtEdadPrim.value = "126";
                        txtEdadPrim.disabled = true;
                        ddlToxicologia1.value = "99";
                        ddlToxicologia1.disabled = true;
                        ddlDrogaSec.disabled = false;
                        ddlDrogaTerc.disabled = false;
                        ddlDrogaSec.value = sustanciasList.Nousaactualmente;
                        ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                        ddlViaSec.value = viaList.NoAplica;
                        ddlViaTerc.value = viaList.NoAplica;
                        ddlFrecSec.value = 99;
                        ddlFrecTerc.value = 99;
                        txtEdadSec.value = "126";
                        txtEdadTerc.value = "126";
                        ddlToxicologia2.value = "99";
                        ddlToxicologia2.disabled = true;
                        ddlToxicologia3.value = "99";
                        ddlToxicologia3.disabled = true;
                        ddlDrogaSec.disabled = true;
                        ddlDrogaTerc.disabled = true;
                        ddlViaSec.disabled = true;
                        ddlViaTerc.disabled = true;
                        ddlFrecSec.disabled = true;
                        ddlFrecTerc.disabled = true;
                        txtEdadSec.disabled = true;
                        txtEdadTerc.disabled = true;
                    }
                    else {
                        ddlDrogaPrim.value = "0";
                        ddlDrogaPrim.focus();
                    }
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
                    ddlViaSec.value = viaList.NoAplica;
                    ddlViaTerc.value = viaList.NoAplica;
                    ddlFrecSec.value = 99;
                    ddlFrecTerc.value = 99;
                    txtEdadSec.value = "126";
                    txtEdadTerc.value = "126";
                    ddlToxicologia2.value = "99";
                    ddlToxicologia2.disabled = true;
                    ddlToxicologia3.value = "99";
                    ddlToxicologia3.disabled = true;
                    ddlDrogaSec.disabled = true;
                    ddlDrogaTerc.disabled = true;
                    ddlViaSec.disabled = true;
                    ddlViaTerc.disabled = true;
                    ddlFrecSec.disabled = true;
                    ddlFrecTerc.disabled = true;
                    txtEdadSec.disabled = true;
                    txtEdadTerc.disabled = true;
                }
                txtDrogaPrim.value = "";
                hogarDiv.style.visibility = 'hidden';
                break;
            case (sustanciasList.Noaplica):
            case ("95"): case ("98")://OLDVALUES
                if (ddlNivelCuidadoSustancias.value != "99") {
                    ddlDrogaPrim.value = 0;
                    ddlDrogaSec.value = sustanciasList.Nousaactualmente;
                    ddlDrogaTerc.value = sustanciasList.Nousaactualmente;

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

                txtDrogaPrim.value = "";
                hogarDiv.style.visibility = 'hidden';
                
                break;
            case ("0"):
                
                    ddlViaPrim.value = 0;
                    //ddlViaPrim.disabled = true;
                    ddlFrecPrim.value = 0;
                    //ddlFrecPrim.disabled = true;
                    txtEdadPrim.value = "";
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

                txtDrogaPrim.value = "";
                hogarDiv.style.visibility = 'hidden';

                break;
            default:
                ddlViaPrim.disabled = false;
                ddlDrogaSec.disabled = false;
                if (IsPostBack() == "False") {
                    ddlToxicologia1.value = "";
                }
                ddlToxicologia1.disabled = false;
                txtDrogaPrim.value = "";
                hogarDiv.style.visibility = 'hidden';

                break;
        }
        ddlViaPrimF();
        ddlDrogaSecF();
    }
     catch (ex) {  }
}
function ddlDrogaSecF() {
    try {
        var CO_Tipo = document.getElementById("mainBodyContent_WucEpisodioPerfil_CO_Tipo");
        var ddlNivelCuidadoSaludMental = document.getElementById("mainBodyContent_WucEpisodioPerfil_hNivelSM");
        var ddlNivelCuidadoSustancias = document.getElementById("mainBodyContent_WucEpisodioPerfil_hNivelAS");
        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaPrim");
        var ddlFrecPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecPrim");
        var txtEdadPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadPrim");
        var ddlDrogaSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaSec");
        var ddlViaSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaSec");
        var ddlFrecSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecSec");
        var txtEdadSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadSec");
        var ddlDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaTerc");
        var ddlViaTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaTerc");
        var ddlFrecTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecTerc");
        var txtEdadTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadTerc");
        var ddlToxicologia1 = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlToxicologia1");
        var ddlToxicologia2 = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlToxicologia2");
        var ddlToxicologia3 = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlToxicologia3");
        ddlViaSec.disabled = false;
        ddlFrecSec.disabled = false;
        txtEdadSec.disabled = false;
        if (IsPostBack() == "False") {
            txtEdadSec.value = "";
        }

        var txtDrogaSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtDrogaSec");

        var hogarDiv = document.getElementById("mainBodyContent_WucEpisodioPerfil_Hogar2_DIV");
        
        if (!(ddlDrogaPrim.value == sustanciasList.Noaplica || ddlDrogaPrim.value == sustanciasList.Nousaactualmente || ddlDrogaPrim.value == "0") && (ddlViaPrim.value == "0" || ddlViaPrim.value == viaList.NoAplica || ddlFrecPrim.value == 0 || ddlFrecPrim.value == 99 || txtEdadPrim.value < "0") && !(ddlDrogaSec.value == sustanciasList.Noaplica || ddlDrogaSec.value == sustanciasList.Nousaactualmente)) {

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
            ddlToxicologia2.value = "99";
            ddlToxicologia2.disabled = true;
            ddlToxicologia3.value = "99";
            ddlToxicologia3.disabled = true;
            ddlDrogaTerc.disabled = true;
            ddlViaTerc.disabled = true;
            ddlFrecTerc.disabled = true;
            txtEdadTerc.disabled = true;
            alert("Debe completar toda información de la primera sustancia");
        }
        else {
            switch (ddlDrogaSec.value) {
                case (sustanciasList.Alcohol): case (sustanciasList.Ecstasy): case (sustanciasList.Metadona): case (sustanciasList.Percocet): case (sustanciasList.Xanax):
                    ddlViaSec.value = viaList.OralBebida;
                    ddlDrogaTerc.disabled = false;
                    ddlViaSec.disabled = true;
                    if (IsPostBack() == "False") {
                        if (ddlFrecSec.value == 99) {
                            ddlFrecSec.value = 0;
                        }
                        ddlToxicologia2.value = "";

                    }
                    txtDrogaSec.value = "";
                    ddlToxicologia2.disabled = false;
                    hogarDiv.style.visibility = 'hidden';
                    break;
                case (sustanciasList.Inhalantes):
                    ddlViaSec.value = viaList.Nasal;
                    ddlDrogaTerc.disabled = false;
                    ddlViaSec.disabled = true;
                    if (IsPostBack() == "False") {
                        if (ddlFrecSec.value == 99) {
                            ddlFrecSec.value = 0;
                        }
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
                        if (ddlFrecSec.value == 99) {
                            ddlFrecSec.value = 0;
                        }
                        ddlToxicologia2.value = "";
                    }
                    txtDrogaSec.value = "";
                 
                    ddlToxicologia2.disabled = false;
                    hogarDiv.style.visibility = 'hidden';
                    break;
                case (sustanciasList.Tabacocigarrillo):
                    ddlViaSec.value = viaList.Fumada;
                    ddlDrogaTerc.disabled = false;
                    ddlViaSec.disabled = true;
                    if (IsPostBack() == "False") {
                        if (ddlFrecSec.value == 99) {
                            ddlFrecSec.value = 0;
                        }
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
                case (sustanciasList.Nousaactualmente):
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
                    txtDrogaSec.value = "";
                    ddlToxicologia2.value = "99";
                    ddlToxicologia2.disabled = true;
                    ddlToxicologia3.value = "99";
                    ddlToxicologia3.disabled = true;
                    hogarDiv.style.visibility = 'hidden';
                    break;
                case (sustanciasList.Noaplica): case (sustanciasList.Noinformó):
                case ("95"): case ("98")://OLDVALUES
                    if (ddlNivelCuidadoSustancias.value !== "99" || (ddlNivelCuidadoSaludMental.value !== "99" && ddlDrogaPrim.value !== sustanciasList.Noaplica)) {
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
                    // ddlDrogaSec.value = "0";                   
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
                        ddlToxicologia2.value = "";
                    }
                    ddlToxicologia2.disabled = false;
                    //txtEdadSec.value = "";
                    break;
            }
            if (ddlDrogaSec.value != sustanciasList.Nousaactualmente && ddlDrogaSec.value != sustanciasList.Noaplica && ddlDrogaSec.value != sustanciasList.Noinformó && ddlViaSec.value != 0) {
                if (ddlDrogaSec.value == ddlDrogaPrim.value && ddlViaSec.value == ddlViaPrim.value) {
                    alert("La droga y la vía de utilización NO pueden ser igual a las selecciones de la primera sustancia.");
                    ddlDrogaSec.value = sustanciasList.Nousaactualmente;
                    ddlViaSec.value = viaList.NoAplica;
                    ddlFrecSec.value = 99;
                    txtEdadSec.value = "126";
                    ddlViaSec.disabled = true;
                    ddlFrecSec.disabled = true;
                    txtEdadSec.disabled = true;
                    txtDrogaSec.value = "";
                    hogarDiv.style.visibility = 'hidden';

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

                }
            }
        }
        ddlViaSecF();
        ddlDrogaTercF();
    }
    catch (ex) { }
}
function ddlDrogaTercF() {
    try {
        var CO_Tipo = document.getElementById("mainBodyContent_WucEpisodioPerfil_CO_Tipo");
        var ddlNivelCuidadoSaludMental = document.getElementById("mainBodyContent_WucEpisodioPerfil_hNivelSM");
        var ddlNivelCuidadoSustancias = document.getElementById("mainBodyContent_WucEpisodioPerfil_hNivelAS");
        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaPrim");

        var ddlDrogaSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaSec");
        var ddlViaSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaSec");
        var ddlFrecSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecSec");
        var txtEdadSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadSec");

        var ddlDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaTerc");
        var ddlViaTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaTerc");
        var ddlFrecTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecTerc");
        var txtEdadTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadTerc");

        var ddlToxicologia3 = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlToxicologia3");
        ddlViaTerc.disabled = false;
        ddlFrecTerc.disabled = false;
        if (IsPostBack() == "False") {
            ddlFrecTerc.value = "";
            txtEdadTerc.value = "";

        }
        txtEdadTerc.disabled = false;

        var txtDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtDrogaTerc");

        var hogarDiv = document.getElementById("mainBodyContent_WucEpisodioPerfil_Hogar3_DIV");

        if (!(ddlDrogaSec.value == sustanciasList.Noaplica || ddlDrogaSec.value == "0" || ddlDrogaSec.value == sustanciasList.Nousaactualmente) && (ddlViaSec.value == "0" || ddlViaSec.value == viaList.NoAplica || ddlFrecSec.value == 0 || ddlFrecSec.value == 99 || txtEdadSec.value < "0") && !(ddlDrogaTerc.value == sustanciasList.Noaplica || ddlDrogaTerc.value == sustanciasList.Nousaactualmente)) {
            ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
            ddlViaTerc.value = viaList.NoAplica;
            if (IsPostBack() == "False") {
                txtEdadTerc.value = "126";
                ddlToxicologia3.value = "99";
            ddlFrecTerc.value = 99;

            }
            // ddlDrogaTerc.disabled = true;
            ddlViaTerc.disabled = true;
            ddlFrecTerc.disabled = true;
            txtEdadTerc.disabled = true;

            ddlToxicologia3.disabled = true;

            alert("Debe completar toda información de la segunda sustancia");
        }
        else {
            switch (ddlDrogaTerc.value) {
                case (sustanciasList.Alcohol): case (sustanciasList.Ecstasy): case (sustanciasList.Metadona): case (sustanciasList.Percocet): case (sustanciasList.Xanax):
                    ddlViaTerc.value = viaList.OralBebida;
                    ddlViaTerc.disabled = true;
                    if (IsPostBack() == "False") {
                        if (ddlFrecTerc.value == 99) {
                            ddlFrecTerc.value = 0;
                        }
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
                        if (ddlFrecTerc.value == 99) {
                            ddlFrecTerc.value = 0;
                        } ddlToxicologia3.value = "";
                    }
                    txtDrogaTerc.value = "";

                    ddlToxicologia3.disabled = false;
                    hogarDiv.style.visibility = 'hidden';
                    break;
                case (sustanciasList.Anestesiadecaballo):
                    ddlViaTerc.value = viaList.Inyectada;
                    ddlViaTerc.disabled = true;
                    if (IsPostBack() == "False") {
                        if (ddlFrecTerc.value == 99) {
                            ddlFrecTerc.value = 0;
                        } ddlToxicologia3.value = "";
                    }

                    txtDrogaTerc.value = "";
                    ddlToxicologia3.disabled = false;
                    hogarDiv.style.visibility = 'hidden';
                    break;
                case (sustanciasList.Tabacocigarrillo):
                    ddlViaTerc.value = viaList.Fumada;
                    ddlViaTerc.disabled = true;
                    if (IsPostBack() == "False") {
                        if (ddlFrecTerc.value == 99) {
                            ddlFrecTerc.value = 0;
                        }
                        ddlToxicologia3.value = "";

                    }
                    txtDrogaTerc.value = "";
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
                case (sustanciasList.Nousaactualmente):
                    ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
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
                case (sustanciasList.Noaplica): case (sustanciasList.Noinformó):
                case ("95"): case ("98")://OLDVALUES
                    if (ddlNivelCuidadoSustancias.value != "99" || (ddlNivelCuidadoSaludMental.value !== "99" && ddlDrogaPrim.value !== sustanciasList.Noaplica)) {
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
                    //txtEdadTerc.value = "";
                    txtDrogaTerc.value = "";
                    if (IsPostBack() == "False") {
                        ddlToxicologia3.value = "";
                    }
                    ddlToxicologia3.disabled = false;
                    hogarDiv.style.visibility = 'hidden';
                    break;
            }
        }
        if (ddlDrogaTerc.value != sustanciasList.Nousaactualmente && ddlDrogaTerc.value != sustanciasList.Noaplica && ddlDrogaTerc.value != sustanciasList.Noinformó && ddlViaTerc.value != 0) {
            if (ddlDrogaTerc.value == ddlDrogaSec.value && ddlViaTerc.value == ddlViaSec.value) {
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
            else if (ddlDrogaTerc.value == ddlDrogaPrim.value && ddlViaTerc.value == ddlViaPrim.value) {
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
    catch (ex) { alert(ex.message);}
}

function ddlViaPrimF() {
    try {
        var ddlDroga = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim");
        var ddlVia = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaPrim");

        var ddlDrogaSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaSec");
        var ddlViaSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaSec");

        var ddlDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaTerc");
        var ddlViaTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaTerc");

        if (ddlDroga.value == sustanciasList.Heroína) {
            if (ddlVia.value != viaList.Nasal && ddlVia.value != viaList.Inyectada && ddlVia.value != 0) {
                alert('La droga(heroína) permite solo las vias "Nasal" o "Inyectada".');
                ddlVia.value = 0;
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

            if (ddlDroga.value == sustanciasList.Nousaactualmente || ddlDroga.value == sustanciasList.Noaplica || ddlDroga.value == sustanciasList.Noinformó) {

            }
            else if (ddlDroga.value == "0") {
                alert("Debe puede escoger la opción de '" + "No Aplica" + "'.");
                ddlVia.value = 0;
            }
            else {
                alert("Debe escoger una vía de utilización válida, ya que seleccionó una droga válida");
                ddlVia.value = 0;
            }
        }
        if (ddlDroga.value != sustanciasList.Nousaactualmente && ddlDroga.value != sustanciasList.Noaplica && ddlDroga.value != sustanciasList.Noinformó && ddlVia.value != 0 && ddlVia.value != viaList.NoAplica) {
            if (ddlDroga.value == ddlDrogaSec.value && ddlVia.value == ddlViaSec.value) {
                ddlDrogaSecF();
            }
            else if (ddlDroga.value == ddlDrogaTerc.value && ddlVia.value == ddlViaTerc.value) {
                ddlDrogaTercF();
            }
        }
        ddlFrecPrim();
    }
    catch (ex) { }
}
function ddlViaSecF() {
    try {
        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaPrim");

        var ddlDroga = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaSec");
        var ddlVia = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaSec");
        var ddlFrec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecSec");

        var ddlDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaTerc");
        var ddlViaTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaTerc");

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
        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaPrim");

        var ddlDrogaSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaSec");
        var ddlViaSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaSec");

        var ddlDroga = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaTerc");
        var ddlVia = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaTerc");
        var ddlFrec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecTerc");
       
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
    catch (ex) { alert(ex.message); }
}
function ddlFrecPrim() {
    try {
        var ddlDroga = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim");
        var ddlFrec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecPrim");
       
        if (ddlFrec.value == "99") {
            if (ddlDroga.value == sustanciasList.Nousaactualmente || ddlDroga.value == sustanciasList.Noaplica || ddlDroga.value == sustanciasList.Noinformó) {

            }
            else if (ddlDroga.value == "0") {
                alert("Debe puede escoger la opción de '"+"No Aplica"+"'.");
                ddlFrec.value = 0;
            }
            else {
                alert("Debe escoger una frecuencia de uso válida, ya que seleccionó una droga válida");
                ddlFrec.value = 0;
            }
        }

    }
    catch (ex) { alert(ex.message); }
}

function ddlFrecSec() {
    try {
        var ddlDroga = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaSec");
        var ddlFrec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecSec");
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
        var ddlDroga = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaTerc");
        var ddlFrec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecTerc");
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
        var ddlDSMVDiagDual = document.getElementById(txtType + ddlDSMVDiagDualP);
        var CO_Tipo = document.getElementById("mainBodyContent_WucEpisodioPerfil_CO_Tipo");

        var ddlNivelCuidadoSaludMental = document.getElementById("mainBodyContent_WucEpisodioPerfil_hNivelSM");
        var ddlNivelCuidadoSustancias = document.getElementById("mainBodyContent_WucEpisodioPerfil_hNivelAS");

        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaPrim");
        var ddlFrecPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecPrim");
        var txtEdadPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadPrim");

        var ddlDrogaSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaSec");
        var ddlDrogaTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaTerc");
        var ddlViaSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaSec");
        var ddlViaTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaTerc");
        var ddlFrecSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecSec");
        var ddlFrecTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecTerc");
        var txtEdadSec = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadSec");
        var txtEdadTerc = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadTerc");
        //Substancias
        var GAF = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtDSMVFnGlobal");

        if (ddlNivelCuidadoSustancias.value == "99" && ddlNivelCuidadoSaludMental.value != "99") {
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

            }
        }
        else if (ddlNivelCuidadoSustancias.value != "99" && ddlNivelCuidadoSaludMental.value == "99") {
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
        // catch
    }

    //AjustesNiveldeCuidado();
}

// revisar   
// al seleccionar nivel de cuidado abuso de sustancia



function AjustesNiveldeCuidado() {


    try {
        var ddlNivelCuidadoSaludMental = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlNivelCuidadoSaludMentalHidden");
        var ddlNivelCuidadoSustancias = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlNivelCuidadoSustanciasHidden");


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


        // Si usuario selecciona opción de “Nivel de Cuidado (Salud Mental)”
        if (ddlNivelCuidadoSaludMental.value != "99") {
            alert("entre Nivel de Cuidado (Salud Mental)");

            ddlDrogaPrim.value = sustanciasList.NoSeleccionado;
            ddlDrogaSec.value = sustanciasList.Nousaactualmente;
            ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
            ddlViaPrim.value = viaList.NoSeleccionado;
            ddlViaSec.value = viaList.NoAplica;
            ddlViaTerc.value = viaList.NoAplica;

            ddlFrecPrim.value = 0;
            ddlFrecSec.value = 99;
            ddlFrecTerc.value = 99;
            txtEdadPrim.value = "126";
            txtEdadSec.value = "126";
            txtEdadTerc.value = "126";

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
            GAF.disabled = true;

        }
        else {
            //alert("Nivel de Cuidado (Abuso Sustancia)");
            ddlDrogaPrim.value = sustanciasList.NoSeleccionado;
            ddlDrogaSec.value = sustanciasList.Nousaactualmente;
            ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
            ddlViaPrim.value = viaList.NoSeleccionado;
            ddlViaSec.value = viaList.NoAplica;
            ddlViaTerc.value = viaList.NoAplica;

            ddlFrecPrim.value = 0;
            ddlFrecSec.value = 99;
            ddlFrecTerc.value = 99;
            txtEdadPrim.value = "126";
            txtEdadSec.value = "126";
            txtEdadTerc.value = "126";

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
            GAF.disabled = true;

        }


    }


    catch (e) {
        // catch
    }

}



function cvTakeHomeRazonesNoParticipaValidation(oSrc, args) {
    try {
        var ddlTHBelong = document.getElementById('mainBodyContent_WucTakeHome_ddlTHBelong');
        var lbxRazonSeleccionado = document.getElementById('mainBodyContent_WucTakeHome_lbxRazonSeleccionado');
        switch (ddlTHBelong.value) {
            case ("2"):
                if (lbxRazonSeleccionado.options.length < 1) {
                    args.IsValid = false;
                }
                else {
                    args.IsValid = true;
                }
                break;
            default:
                args.IsValid = true;
                break;
        }
    }
    catch (ex) { }
}

function cvFechaEntrada(oSrc, args) {
    try {
       
        var ddlTHBelong = document.getElementById('mainBodyContent_WucTakeHome_ddlTHBelong');
        var txtFechaEntradaAño = document.getElementById('mainBodyContent_WucTakeHome_txtFechaEntradaAño');
        switch (ddlTHBelong.value) {
            case ("1"):
                if (txtFechaEntradaAño.value == "") {
                    args.IsValid = false;
                }
                else {
                    args.IsValid = true;
                }
                break;
            default:
                args.IsValid = true;
                break;
        }
    }
    catch (ex) { }
}

function cvFechaSalida(oSrc, args) {
    try {

        var ddlTHBelong = document.getElementById('mainBodyContent_WucTakeHome_ddlTHBelong');
        var txtFechaSalidaAño = document.getElementById('mainBodyContent_WucTakeHome_txtFechaSalidaAño');
        var tipoDePerfil = $("#hTipoPagina").val();
        
        switch (ddlTHBelong.value) {
            case ("1"):
                if (txtFechaSalidaAño.value == "" && tipoDePerfil == "alta") {
                    args.IsValid = false;
                }
                else {
                    args.IsValid = true;
                }
                break;
            default:
                args.IsValid = true;
                break;
        }
    }
    catch (ex) { }
}

function cvCantidadBotellas(oSrc, args) {
    try {

        var ddlTHBelong = document.getElementById('mainBodyContent_WucTakeHome_ddlTHBelong');
        var txtCantidadBotellas = document.getElementById('mainBodyContent_WucTakeHome_txtCantidadBotellas');
        switch (ddlTHBelong.value) {
            case ("1"):
                if (txtCantidadBotellas.value == "" || txtCantidadBotellas.value < 0) {
                    args.IsValid = false;
                }
                else {
                    args.IsValid = true;
                }
                break;
            default:
                args.IsValid = true;
                break;
        }
    }
    catch (ex) { }
}

function cvFrecuenciaBotellas(oSrc, args) {
    try {

        var ddlTHBelong = document.getElementById('mainBodyContent_WucTakeHome_ddlTHBelong');
        var ddlFrecuenciaBotellas = document.getElementById('mainBodyContent_WucTakeHome_ddlFrecuenciaBotellas');
        switch (ddlTHBelong.value) {
            case ("1"):
                if (ddlFrecuenciaBotellas.value == 0) {
                    args.IsValid = false;
                }
                else {
                    args.IsValid = true;
                }
                break;
            default:
                args.IsValid = true;
                break;
        }
    }
    catch (ex) { }
}

function cvEtapa(oSrc, args) {
    try {

        var ddlTHBelong = document.getElementById('mainBodyContent_WucTakeHome_ddlTHBelong');
        var ddlTHEtapa = document.getElementById('mainBodyContent_WucTakeHome_ddlTHEtapa');
        switch (ddlTHBelong.value) {
            case ("1"):
                if (ddlTHEtapa.value == 0) {
                    args.IsValid = false;
                }
                else {
                    args.IsValid = true;
                }
                break;
            default:
                args.IsValid = true;
                break;
        }
    }
    catch (ex) { }
}

function validateCOOCURRING() {
    var ddlNivelCuidadoSaludMental = document.getElementById("mainBodyContent_WucEpisodioPerfil_hNivelSM");
    var ddlNivelCuidadoSustancias = document.getElementById("mainBodyContent_WucEpisodioPerfil_hNivelAS");
    var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim");
    var ddlDSMVDiagDual = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDSMVDiagDual");
    var GAF = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtDSMVFnGlobal");
    var hDual = document.getElementById("mainBodyContent_WucEpisodioPerfil_hDual");
    var ClinHD = document.getElementById("mainBodyContent_WucEpisodioPerfil_hDSMVClinPrim");
    var ClinHDSus = document.getElementById("mainBodyContent_WucEpisodioPerfil_hDSMVSusPrim");
    var ddlFreq_AutoAyuda = document.getElementById("mainBodyContent_WucDatosDemograficosPerfil_ddlFreq_AutoAyuda");




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
        if (ddlDrogaPrim.value != sustanciasList.Noaplica && ddlDrogaPrim.value != sustanciasList.Tabacocigarrillo && ddlDSMVDiagDual.value != "1") {
            campos += "\u2022Seleccionó una droga\n";
            flagConcurrente = false;

        }

        if (flagConcurrente == false) {
            alert(message + campos);
        }

        if (ClinHDSus.value == '761' && ddlFreq_AutoAyuda.value == '1' && (ddlDrogaPrim.value == sustanciasList.Noaplica || ddlDrogaPrim.value == sustanciasList.Tabacocigarrillo) && ddlDSMVDiagDual.value == "1") {
            alert("!!! !!! ESTE PERFIL DE SALUD MENTAL REFLEJA QUE NO ES CONCURRENTE Y USTED SELECCIONO LO CONTRARIO !!!");
            return false;
        }

        return flagConcurrente;

    }
    else if (ddlNivelCuidadoSustancias.value != "99") {

        var message = "!!! ESTE PERFIL DE ABUSO DE SUSTANCIA REFLEJA QUE ES CONCURRENTE Y USTED SELECCIONO LO CONTRARIO !!!\n\nLos campos que ocasionarón este mensaje son: \n";
        var flagConcurrente = true;

        //1)	Diagnóstico de salud mental
        if ((GAF.value != "") && ddlDSMVDiagDual.value != "1") {

            campos += "\u2022Entró algún valor en Funcionamiento Global\n";

            flagConcurrente = false;
        }


        //2)	Medidas de Funcionamiento Global - CGAS
        if (ClinHD.value != '761' && ddlDSMVDiagDual.value != "1") {
            campos += "\u2022Entró algún valor en Diagnostico Primario\n";
            flagConcurrente = false;
        }



        if (flagConcurrente == false) {
            alert(message + campos);
        }

        if (GAF.value == "" && ClinHD.value == '761' && ddlDSMVDiagDual.value == "1") {
            alert("!!! ESTE PERFIL DE ABUSO DE SUSTANCIA REFLEJA QUE NO ES CONCURRENTE Y USTED SELECCIONO LO CONTRARIO !!!!!!");
            return false;
        }




        return flagConcurrente;

    //var campos = "\n";

    //if (ddlNivelCuidadoSaludMental.value != "99") {
    //    if (ClinHD.value == '761') {
    //        alert("!!! ESTE PERFIL DE SALUD MENTAL REFLEJA QUE ES DE TIPO SALUD MENTAL Y USTED NO SELECCIONÓ AL MENOS UN(1) DIAGNOSTICO VALIDO !!!");
    //        return false;
    //    }
    //    if (ddlDrogaPrim.value != sustanciasList.Noaplica && ddlDSMVDiagDual.value != "1") {
    //        if (ddlDrogaPrim.value != sustanciasList.Noaplica) {
    //            campos += "\u2022Seleccionó una droga\n";
    //        }
    //        alert("!!! ESTE PERFIL DE SALUD MENTAL REFLEJA QUE ES CONCURRENTE Y USTED SELECCIONO LO CONTRARIO !!!\n\nLos campos que ocacionarón este mensaje son:\n" + campos);
    //        return false;
    //    }
    //    else if (ddlDrogaPrim.value == sustanciasList.Noaplica && ddlDSMVDiagDual.value == "1") {
    //        campos += "\u2022NO seleccionó una droga\n";
    //        return confirm("!!! ESTE PERFIL DE SALUD MENTAL REFLEJA QUE NO ES CONCURRENTE Y USTED SELECCIONO LO CONTRARIO !!!\n\nLos campos que ocacionarón este mensaje son:\n" + campos + "\n\nDesea registrar el perfil?");
    //    }
    //    else {
    //        return true;
    //    }
    //}
    //else if (ddlNivelCuidadoSustancias.value != "99") {

    //    if (GAF.value != "" && ddlDSMVDiagDual.value != "1") {

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
    //        return confirm("!!! ESTE PERFIL DE ABUSO DE SUSTANCIA REFLEJA NO QUE ES CONCURRENTE Y USTED SELECCIONO LO CONTRARIO !!!\n\nLos campos que ocacionarón este mensaje son:\n" + campos + "\n\nDesea registrar el perfil?");
    //    }
    //    else {
    //        return true;
    //    }
    //}
    //else {
    //    return true;
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

function ddlNivelRecuperacion() {
    var ddlNivelR = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlNivelRecuperacion");
    var txtHogar = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtHogar");

    if (ddlNivelR.value == "99") {
        txtHogar.value = "";
        txtHogar.disabled = true;
    }
    else {
        txtHogar.disabled = false;
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
            showUnclosableModal();
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
function showUnclosableModal() {
    var tipoDePerfil = $("#hTipoPagina").val();
    switch ($("#frmActionMode").val()) {
        case ("create"): $("#mensageModal").text("Guardando perfil de " + tipoDePerfil + "."); break;
        case ("update"): $("#mensageModal").text("Actualizando perfil de " + tipoDePerfil + "."); break;
        default: $("#mensageModal").text("Guardando perfil."); break;
    }
    $("#myModalUnclosable").modal('show');
}
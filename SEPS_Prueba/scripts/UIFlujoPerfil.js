$(document).ready(function () {
    frmActionModeSetup();
    startupFunctions();
});
function startupFunctions() {
    try {
        changeTabOrder();
        setupFechaPerfilLabel();
        ddlCondLaboral();
        ddlArrestado30();
        ddlDrogaPrimF();
        ddlDSMVPsicoAmbiPrim();
        ddlDSMVPsicoAmbiSec();
        ddlRazonAlta();
        ddlGrado();
        TakeHomeParticipa();
        CO_Tipo();
    }
    catch (ex) {
        throw ex;
    }
}
function CO_Tipo() {
    try {
        var CO_Tipo = document.getElementById("mainBodyContent_WucEpisodioPerfil_CO_Tipo");
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


        if (CO_Tipo.value == "1" || CO_Tipo.value == "4") {
            ddlDrogaSec.value = sustanciasList.Nousaactualmente;
            ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
            ddlViaSec.value = viaList.NoAplica;
            ddlViaTerc.value = viaList.NoAplica;
            ddlFrecSec.value = 99;
            ddlFrecTerc.value = 99;
            txtEdadSec.value = "0";
            txtEdadTerc.value = "0";
            ddlDrogaSec.disabled = true;
            ddlDrogaTerc.disabled = true;
            ddlViaSec.disabled = true;
            ddlViaTerc.disabled = true;
            ddlFrecSec.disabled = true;
            ddlFrecTerc.disabled = true;
            txtEdadSec.disabled = true;
            txtEdadTerc.disabled = true;
            //Substancia
            GAF.disabled = true;
        }
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
            txtEdadPrim.value = "0";
            txtEdadSec.value = "0";
            txtEdadTerc.value = "0";
            ddlDrogaPrim.disabled = true;
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
    catch (ex) {
        // 
    }
    AjustesNiveldeCuidado();
}

function tabEvent(e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode == 9) {
        var prefix = "mainBodyContent_WucEpisodioPerfil_";
        var inputs = [prefix + "ddlDrogaPrim"/*0*/, prefix + "ddlViaPrim"/*1*/, prefix + "ddlFrecPrim"/*2*/, prefix + "txtEdadPrim"/*3*/,
                        prefix + "ddlDrogaSec"/*4*/, prefix + "ddlViaSec"/*5*/, prefix + "ddlFrecSec"/*6*/, prefix + "txtEdadSec"/*7*/,
                        prefix + "ddlDrogaTerc"/*8*/, prefix + "ddlViaTerc"/*9*/, prefix + "ddlFrecTerc"/*10*/, prefix + "txtEdadTerc"/*11*/];
        if (e.shiftKey) {
            switch (e.currentTarget.id) {
                case (prefix + "ddlDrogaSec"):
                    for (var i = 3; i >= 0; i--) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "ddlDrogaTerc"):
                    for (var i = 7; i >= 0; i--) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "ddlViaPrim"):
                    for (var i = 0; i >= 0; i--) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "ddlViaSec"):
                    for (var i = 4; i >= 0; i--) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "ddlViaTerc"):
                    for (var i = 8; i >= 0; i--) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "ddlFrecPrim"):
                    for (var i = 1; i >= 0; i--) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "ddlFrecSec"):
                    for (var i = 5; i >= 0; i--) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "ddlFrecTerc"):
                    for (var i = 9; i >= 0; i--) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "txtEdadPrim"):
                    for (var i = 2; i >= 0; i--) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "txtEdadSec"):
                    for (var i = 6; i >= 0; i--) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "txtEdadTerc"):
                    for (var i = 10; i >= 0; i--) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                default: break;
            }
        }
        else {
            switch (e.currentTarget.id) {
                case (prefix + "ddlDrogaPrim"):
                    for (var i = 1; i <= 11; i++) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "ddlDrogaSec"):
                    for (var i = 5; i <= 11; i++) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "ddlDrogaTerc"):
                    for (var i = 9; i <= 11; i++) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "ddlViaPrim"):
                    for (var i = 2; i <= 11; i++) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "ddlViaSec"):
                    for (var i = 6; i <= 11; i++) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "ddlViaTerc"):
                    for (var i = 10; i <= 11; i++) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "ddlFrecPrim"):
                    for (var i = 3; i <= 11; i++) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "ddlFrecSec"):
                    for (var i = 7; i <= 11; i++) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "ddlFrecTerc"):
                    for (var i = 11; i <= 11; i++) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "txtEdadPrim"):
                    for (var i = 4; i <= 11; i++) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
                    break;
                case (prefix + "txtEdadSec"):
                    for (var i = 8; i <= 11; i++) {
                        if ($("#" + inputs[i]).is(':enabled')) {
                            $("#" + inputs[i]).focus(); e.preventDefault(); return;
                        }
                    }
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
    }
    catch (ex) { }
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
function showDSMV(txtDescripcion, txtDescripcionHidden, tipoDescripcion) {
    try {
        var ClinPrim = document.getElementById('mainBodyContent_WucEpisodioPerfil_hDSMVClinPrim').value;
        var ClinSec = document.getElementById('mainBodyContent_WucEpisodioPerfil_hDSMVClinSec').value;
        var RMPrim = document.getElementById('mainBodyContent_WucEpisodioPerfil_hDSMVRMPrim').value;
        var RMSec = document.getElementById('mainBodyContent_WucEpisodioPerfil_hDSMVRMSec').value;
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
        if (txtDescripcion == "mainBodyContent_WucEpisodioPerfil_txtDSMVRMSec") {
            if (RMPrim == '761') {
                alert("Debe seleccionar un diagnóstico primario válido");
                return;
            }
        }
        else if (txtDescripcion == "mainBodyContent_WucEpisodioPerfil_txtDSMVRMTer") {
            if (RMSec == '761') {
                alert("Debe seleccionar un diagnóstico secundario válido");
                return;
            }
        }

        var url = 'frmdsmi_v.aspx?' + 'txtDescripcion=' + txtDescripcion + '&txtDescripcionHidden=' + txtDescripcionHidden + '&tipoDescripcion=' + tipoDescripcion
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
                ddlNoFueraLaboral.disabled = false;
                ddlNoFueraLaboral.value = 0;
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
                    ddlTHEtapa.disabled = false;
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
        ddlViaPrim.disabled = false;
        ddlFrecPrim.disabled = false;
        txtEdadPrim.disabled = false;
        switch (ddlDrogaPrim.value) {
            case (sustanciasList.Alcohol): case (sustanciasList.Ecstasy): case (sustanciasList.Metadona): case (sustanciasList.Percocet): case (sustanciasList.Xanax):
                ddlViaPrim.value = viaList.OralBebida;
                ddlDrogaSec.disabled = false;
                ddlViaPrim.disabled = true;
                break;
            case (sustanciasList.Inhalantes):
                ddlViaPrim.value = viaList.Nasal;
                ddlDrogaSec.disabled = false;
                ddlViaPrim.disabled = true;
                break;
            case (sustanciasList.Anestesiadecaballo):
                ddlViaPrim.value = viaList.Inyectada;
                ddlDrogaSec.disabled = false;
                ddlViaPrim.disabled = true;
                break;
            case (sustanciasList.Tabacocigarrillo):
                ddlViaPrim.value = viaList.Fumada;
                ddlDrogaSec.disabled = false;
                ddlViaPrim.disabled = true;
                break;
            case (sustanciasList.Nousaactualmente):
                if (CO_Tipo.value == "1" || CO_Tipo.value == "4" || ddlDSMVDiagDual.value == "1") {
                    var a = confirm("Al seleccionar esta opción, significa que el paciente NO utiliza ninguna tipo de droga actualmente. ¿Desea proseguir?")
                    if (a == true) {
                        alert("El paciente NO esta utilizando ninguna droga.");
                        ddlViaPrim.value = viaList.NoAplica;
                        ddlViaPrim.disabled = true;
                        ddlFrecPrim.value = 99;
                        ddlFrecPrim.disabled = true;
                        txtEdadPrim.value = "0";
                        txtEdadPrim.disabled = true;
                        ddlDrogaSec.disabled = false;
                        ddlDrogaTerc.disabled = false;
                        ddlDrogaSec.value = sustanciasList.Nousaactualmente;
                        ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                        ddlViaSec.value = viaList.NoAplica;
                        ddlViaTerc.value = viaList.NoAplica;
                        ddlFrecSec.value = 99;
                        ddlFrecTerc.value = 99;
                        txtEdadSec.value = "0";
                        txtEdadTerc.value = "0";
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
                break;
            case (sustanciasList.Noaplica): case (sustanciasList.Noinformó):
            case ("95"): case ("98")://OLDVALUES
                if (CO_Tipo.value == "1" || CO_Tipo.value == "4" || ddlDSMVDiagDual.value == "1") {
                    ddlDrogaPrim.value = 0;

                    if (CO_Tipo.value == "1" || CO_Tipo.value == "4") {
                        alert("Este perfil es de Abuso de Sustancia, no puede seleccionar " + "'" + "No Aplica" + "'" + ".");
                    }
                    else {
                        alert("Este perfil esta seleccionado como CONCURRENTE, no puede seleccionar " + "'" + "No Aplica" + "'" + ".");
                    }

                    ddlDrogaSec.value = sustanciasList.Nousaactualmente;
                    ddlDrogaTerc.value = sustanciasList.Nousaactualmente;

                }
                else {
                    ddlViaPrim.value = viaList.NoAplica;
                    ddlViaPrim.disabled = true;
                    ddlFrecPrim.value = 99;
                    ddlFrecPrim.disabled = true;
                    txtEdadPrim.value = "0";
                    txtEdadPrim.disabled = true;
                    ddlDrogaSec.value = sustanciasList.Noaplica;
                    ddlDrogaTerc.value = sustanciasList.Noaplica;
                }
                    
                    ddlViaSec.value = viaList.NoAplica;
                    ddlViaTerc.value = viaList.NoAplica;
                    ddlFrecSec.value = 99;
                    ddlFrecTerc.value = 99;
                    txtEdadSec.value = "0";
                    txtEdadTerc.value = "0";
                    ddlDrogaSec.disabled = true;
                    ddlDrogaTerc.disabled = true;
                    ddlViaSec.disabled = true;
                    ddlViaTerc.disabled = true;
                    ddlFrecSec.disabled = true;
                    ddlFrecTerc.disabled = true;
                    txtEdadSec.disabled = true;
                    txtEdadTerc.disabled = true;
                
                break;
            case ("0"):
                if (CO_Tipo.value == "1" || CO_Tipo.value == "4" || ddlDSMVDiagDual.value == "1") {
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
                    txtEdadSec.value = "0";
                    txtEdadTerc.value = "0";
                    ddlDrogaSec.disabled = true;
                    ddlDrogaTerc.disabled = true;
                    ddlViaSec.disabled = true;
                    ddlViaTerc.disabled = true;
                    ddlFrecSec.disabled = true;
                    ddlFrecTerc.disabled = true;
                    txtEdadSec.disabled = true;
                    txtEdadTerc.disabled = true;
                }

            default:
                ddlViaPrim.disabled = false;
                ddlDrogaSec.disabled = false;
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
        ddlViaSec.disabled = false;
        ddlFrecSec.disabled = false;
        txtEdadSec.disabled = false;

        if (!(ddlDrogaPrim.value == sustanciasList.Noaplica || ddlDrogaPrim.value == sustanciasList.Nousaactualmente || ddlDrogaPrim.value == "0") && (ddlViaPrim.value == "0" || ddlViaPrim.value == viaList.NoAplica || ddlFrecPrim.value == 0 || ddlFrecPrim.value == 99 || txtEdadPrim.value < "1") && !(ddlDrogaSec.value == sustanciasList.Noaplica || ddlDrogaSec.value == sustanciasList.Nousaactualmente)) {

            ddlDrogaSec.value = sustanciasList.Nousaactualmente;
            ddlViaSec.value = viaList.NoAplica;
            ddlViaSec.disabled = true;
            ddlFrecSec.value = 99;
            ddlFrecSec.disabled = true;
            txtEdadSec.value = "0";
            txtEdadSec.disabled = true;
            ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
            ddlViaTerc.value = viaList.NoAplica;
            ddlFrecTerc.value = 99;
            txtEdadTerc.value = "0";
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
                    ddlFrecSec.value = 0;
                    break;
                case (sustanciasList.Inhalantes):
                    ddlViaSec.value = viaList.Nasal;
                    ddlDrogaTerc.disabled = false;
                    ddlViaSec.disabled = true;
                    ddlFrecSec.value = 0;
                    break;
                case (sustanciasList.Anestesiadecaballo):
                    ddlViaSec.value = viaList.Inyectada;
                    ddlDrogaTerc.disabled = false;
                    ddlViaSec.disabled = true;
                    ddlFrecSec.value = 0;
                    break;
                case (sustanciasList.Tabacocigarrillo):
                    ddlViaSec.value = viaList.Fumada;
                    ddlDrogaTerc.disabled = false;
                    ddlViaSec.disabled = true;
                    ddlFrecSec.value = 0;
                    break;
                case (sustanciasList.Nousaactualmente):
                    ddlDrogaSec.value = sustanciasList.Nousaactualmente;
                    ddlViaSec.value = viaList.NoAplica;
                    ddlViaSec.disabled = true;
                    ddlFrecSec.value = 99;
                    ddlFrecSec.disabled = true;
                    txtEdadSec.value = "0";
                    txtEdadSec.disabled = true;
                    ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                    ddlViaTerc.value = viaList.NoAplica;
                    ddlFrecTerc.value = 99;
                    txtEdadTerc.value = "0";
                    ddlDrogaTerc.disabled = true;
                    ddlViaTerc.disabled = true;
                    ddlFrecTerc.disabled = true;
                    txtEdadTerc.disabled = true;
                    break;
                case (sustanciasList.Noaplica): case (sustanciasList.Noinformó):
                case ("95"): case ("98")://OLDVALUES
                    if (CO_Tipo.value == "1" || CO_Tipo.value == "4" || ddlDSMVDiagDual.value == "1") {
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
                    txtEdadSec.value = "0";
                    txtEdadSec.disabled = true;
                    ddlViaTerc.value = viaList.NoAplica;
                    ddlFrecTerc.value = 99;
                    txtEdadTerc.value = "0";
                    ddlDrogaTerc.disabled = true;
                    ddlViaTerc.disabled = true;
                    ddlFrecTerc.disabled = true;
                    txtEdadTerc.disabled = true;
                    break;
                case ("0"):
                    // ddlDrogaSec.value = "0";                   
                    ddlViaSec.value = viaList.NoAplica;
                    ddlViaSec.disabled = true;
                    ddlFrecSec.value = 99;
                    ddlFrecSec.disabled = true;
                    txtEdadSec.value = "0";
                    txtEdadSec.disabled = true;
                    ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                    ddlViaTerc.value = viaList.NoAplica;
                    ddlFrecTerc.value = 99;
                    txtEdadTerc.value = "0";
                    ddlDrogaTerc.disabled = true;
                    ddlViaTerc.disabled = true;
                    ddlFrecTerc.disabled = true;
                    txtEdadTerc.disabled = true;
                    break;
                default:
                    ddlViaSec.disabled = false;
                    ddlDrogaTerc.disabled = false;
                    txtEdadSec.value = "";
                    break;
            }
            if (ddlDrogaSec.value != sustanciasList.Nousaactualmente && ddlDrogaSec.value != sustanciasList.Noaplica && ddlDrogaSec.value != sustanciasList.Noinformó && ddlViaSec.value != 0) {
                if (ddlDrogaSec.value == ddlDrogaPrim.value && ddlViaSec.value == ddlViaPrim.value) {
                    alert("La droga y la vía de utilización NO pueden ser igual a las selecciones de la primera sustancia.");
                    ddlDrogaSec.value = sustanciasList.Nousaactualmente;
                    ddlViaSec.value = viaList.NoAplica;
                    ddlFrecSec.value = 99;
                    txtEdadSec.value = "0";
                    ddlViaSec.disabled = true;
                    ddlFrecSec.disabled = true;
                    txtEdadSec.disabled = true;

                    ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                    ddlViaTerc.value = viaList.NoAplica;
                    ddlFrecTerc.value = 99;
                    txtEdadTerc.value = "0";
                    ddlDrogaTerc.disabled = true;
                    ddlViaTerc.disabled = true;
                    ddlFrecTerc.disabled = true;
                    txtEdadTerc.disabled = true;

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
        var CO_Tipo = document.getElementById("mainBodyContent_WucEpisodioPerfil_CO_Tipo");
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
        ddlViaTerc.disabled = false;
        ddlFrecTerc.disabled = false;
        txtEdadTerc.disabled = false;

        if (!(ddlDrogaSec.value == sustanciasList.Noaplica || ddlDrogaSec.value == "0" || ddlDrogaSec.value == sustanciasList.Nousaactualmente) && (ddlViaSec.value == "0" || ddlViaSec.value == viaList.NoAplica || ddlFrecSec.value == 0 || ddlFrecSec.value == 99 || txtEdadSec.value < "1") && !(ddlDrogaTerc.value == sustanciasList.Noaplica || ddlDrogaTerc.value == sustanciasList.Nousaactualmente)) {
            ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
            ddlViaTerc.value = viaList.NoAplica;
            ddlFrecTerc.value = 99;
            txtEdadTerc.value = "0";
            // ddlDrogaTerc.disabled = true;
            ddlViaTerc.disabled = true;
            ddlFrecTerc.disabled = true;
            txtEdadTerc.disabled = true;
            alert("Debe completar toda información de la segunda sustancia");
        }
        else {
            switch (ddlDrogaTerc.value) {
                case (sustanciasList.Alcohol): case (sustanciasList.Ecstasy): case (sustanciasList.Metadona): case (sustanciasList.Percocet): case (sustanciasList.Xanax):
                    ddlViaTerc.value = viaList.OralBebida;
                    ddlViaTerc.disabled = true;
                    ddlFrecTerc.value = 0;
                    break;
                case (sustanciasList.Inhalantes):
                    ddlViaTerc.value = viaList.Nasal;
                    ddlViaTerc.disabled = true;
                    ddlFrecTerc.value = 0;
                    break;
                case (sustanciasList.Anestesiadecaballo):
                    ddlViaTerc.value = viaList.Inyectada;
                    ddlViaTerc.disabled = true;
                    ddlFrecTerc.value = 0;
                    break;
                case (sustanciasList.Tabacocigarrillo):
                    ddlViaTerc.value = viaList.Fumada;
                    ddlViaTerc.disabled = true;
                    ddlFrecTerc.value = 0;
                    break;
                case (sustanciasList.Nousaactualmente):
                    ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                    ddlViaTerc.value = viaList.NoAplica;
                    ddlViaTerc.disabled = true;
                    ddlFrecTerc.value = 99;
                    ddlFrecTerc.disabled = true;
                    txtEdadTerc.value = "0";
                    txtEdadTerc.disabled = true;
                    break;
                case (sustanciasList.Noaplica): case (sustanciasList.Noinformó):
                case ("95"): case ("98")://OLDVALUES
                    if (CO_Tipo.value == "1" || CO_Tipo.value == "4" || ddlDSMVDiagDual.value == "1") {
                        ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                    }
                    else {
                        ddlDrogaTerc.value = sustanciasList.Noaplica;
                    }
                    ddlViaTerc.value = viaList.NoAplica;
                    ddlViaTerc.disabled = true;
                    ddlFrecTerc.value = 99;
                    ddlFrecTerc.disabled = true;
                    txtEdadTerc.value = "0";
                    txtEdadTerc.disabled = true;
                    break;
                case ("0"):
                    ddlViaTerc.value = viaList.NoAplica;
                    ddlViaTerc.disabled = true;
                    ddlFrecTerc.value = 99;
                    ddlFrecTerc.disabled = true;
                    txtEdadTerc.value = "0";
                    txtEdadTerc.disabled = true;
                    break;
                default:
                    ddlViaTerc.disabled = false;
                    txtEdadTerc.value = "";
                    break;
            }
        }
        if (ddlDrogaTerc.value != sustanciasList.Nousaactualmente && ddlDrogaTerc.value != sustanciasList.Noaplica && ddlDrogaTerc.value != sustanciasList.Noinformó && ddlViaTerc.value != 0) {
            if (ddlDrogaTerc.value == ddlDrogaSec.value && ddlViaTerc.value == ddlViaSec.value) {
                alert("La droga y la vía de utilización NO pueden ser igual a las selecciones de la segunda sustancia.");
                ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                ddlViaTerc.value = viaList.NoAplica;
                ddlFrecTerc.value = 99;
                txtEdadTerc.value = "0";
                ddlViaTerc.disabled = true;
                ddlFrecTerc.disabled = true;
                txtEdadTerc.disabled = true;

            }
            else if (ddlDrogaTerc.value == ddlDrogaPrim.value && ddlViaTerc.value == ddlViaPrim.value) {
                alert("La droga y la vía de utilización NO pueden ser igual a las selecciones de la primera sustancia.");
                ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                ddlViaTerc.value = viaList.NoAplica;
                ddlFrecTerc.value = 99;
                txtEdadTerc.value = "0";
                ddlViaTerc.disabled = true;
                ddlFrecTerc.disabled = true;
                txtEdadTerc.disabled = true;

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

function ddlDSMVDiagDual(txtType, ddlDSMVDiagDual) {
    try {
        var ddlDSMVDiagDual = document.getElementById(txtType + ddlDSMVDiagDual);
        var CO_Tipo = document.getElementById("mainBodyContent_WucEpisodioPerfil_CO_Tipo");

        var ddlDrogaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlDrogaPrim");
        var ddlViaPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlViaPrim");
        var ddlFrecPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_ddlFrecPrim");
        var txtEdadPrim = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtEdadPrim");

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
        //Substancias
        var GAF = document.getElementById("mainBodyContent_WucEpisodioPerfil_txtDSMVFnGlobal");

        if (CO_Tipo.value == "2" || CO_Tipo.value == "3") {
            switch (ddlDSMVDiagDual.value) {
                case ("1"):
                    ddlDrogaPrim.value = 0;
                    ddlViaPrim.value = 0;
                    ddlFrecPrim.value = 0;
                    txtEdadPrim.value = "";
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
                    txtEdadSec.value = "0";
                    txtEdadTerc.value = "0";
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
                    txtEdadPrim.value = "0";
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
                    txtEdadSec.value = "0";
                    txtEdadTerc.value = "0";
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
        else if (CO_Tipo.value == "1" || CO_Tipo.value == "4") {
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

    AjustesNiveldeCuidado();
}

// revisar   
// al seleccionar nivel de cuidado abuso de sustancia



function AjustesNiveldeCuidado() {


    try {
        var ddlNivelCuidadoSaludMental = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSaludMental");
        var ddlNivelCuidadoSustancias = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlNivelCuidadoSustancias");


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
            txtEdadPrim.value = "0";
            txtEdadSec.value = "0";
            txtEdadTerc.value = "0";

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
            txtEdadPrim.value = "0";
            txtEdadSec.value = "0";
            txtEdadTerc.value = "0";

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
var saving = false;
function validate() {
  
    var isValid = Page_ClientValidate();
    if (!saving) {
        if (isValid) {
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
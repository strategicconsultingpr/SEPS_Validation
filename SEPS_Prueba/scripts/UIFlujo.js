$(document).ready(function () {
    
   // frmActionModeSetup();
    
    startupFunctions();
});



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
         ddlGrado();
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
        var accion = document.getElementById("mainBodyContent_WucEpisodioAdmision_hAccion");

        if (accion.value === "update") {
            AccionUpdate();
        }
        else {
            ddlNivelCuidadoSaludMental();
            ddlNivelCuidadoSustancias();
            CO_Tipo();
        }
        
     }
    catch (ex) {
        throw ex;
    }
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
        catch (ex) {// block }

        }
        AjustesNiveldeCuidado();
    }
    


function tabEvent(e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode == 9) {
        var prefix = "mainBodyContent_WucEpisodioAdmision_";
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
function changeTabOrder() {
    try {
        var prefix = "#mainBodyContent_WucEpisodioAdmision_";
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

function showDSMV(txtDescripcion, txtDescripcionHidden, tipoDescripcion) {
    try {
        var ClinPrim = document.getElementById('mainBodyContent_WucEpisodioAdmision_hDSMVClinPrim').value;
        var ClinSec = document.getElementById('mainBodyContent_WucEpisodioAdmision_hDSMVClinSec').value;               
        var RMPrim = document.getElementById('mainBodyContent_WucEpisodioAdmision_hDSMVRMPrim').value;
        var RMSec = document.getElementById('mainBodyContent_WucEpisodioAdmision_hDSMVRMSec').value;
        
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
        if (txtDescripcion == "mainBodyContent_WucEpisodioAdmision_txtDSMVRMSec") {
            if (RMPrim == '761') {
                alert("Debe seleccionar un diagnóstico primario válido");
                return;
            }
        }
        else if (txtDescripcion == "mainBodyContent_WucEpisodioAdmision_txtDSMVRMTer") {
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
        else if (txtDescripcion.value == "mainBodyContent_" + tipoDescripcion.value + "_txtDSMVClinPrim" && lbx.value == '761' &&ClinHD1.value != '761') {
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
        switch (ddlPreviosSustancias.value) {
            case ("1"): case ("99"):
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
        switch (ddlPreviosMental.value) {
            case ("1"): case ("99"):
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
        var días = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDíasMentUlt");
        var meses = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtMesesMentUlt");
        switch (document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlUltMental").value) {
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
            case ("99")://No aplica
                días.value = 0;
                meses.value = 0;
                días.disabled = true;
                meses.disabled = true;
                break;
            default: break;
        }
    }
    catch (ex) { }
}
function ddlUltSustancias() {
    try {
        var días = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtDíasSustUlt");
        var meses = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtMesesSustUlt");
        switch (document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlUltSustancias").value) {
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
            case ("99")://No aplica
                días.value = 0;
                meses.value = 0;
                días.disabled = true;
                meses.disabled = true;
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
            alert("saludmental");
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
        else if (ddlNivelCuidadoSustancias.value !== "99" && ddlNivelCuidadoSaludMental.value === "99") {
            alert("sustancia");
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

 
            // Si usuario selecciona opción de “Nivel de Cuidado (Salud Mental)”
            if (ddlNivelCuidadoSaludMental.value != "99" && ddlDSMVDiagDual.value != "1") {
                
                ddlDrogaPrim.value = sustanciasList.Noaplica;
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
                GAF.disabled = false;

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

                if (ddlNivelCuidadoSaludMental.value != "99") {
                    GAF.disabled = false;
                }
                else {
                    GAF.disabled = true;
                }


                }
   

        }


        catch (e) {
            alert(ex.text);
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
                opiod.disabled = true;
                break;
            default:
                if (opiod.value === "4") {
                    opiod.value = "0";
                }
                opiod.disabled = false;
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
                if (ddlDSMVDiagDual.value == "1") {
                    if (opiod.value != "0" && opiod.value != "4") { }
                    else { opiod.value = "0"; }
                    txtDíasSustancias.disabled = false;
                    opiod.disabled = false;
                }
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
    catch (ex) { alert(ex.text); }
    AjustesNiveldeCuidado();
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
            ddlEstadoLegal.disabled = false;
            ddlEstadoLegal.value = 0;
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
                ddlNoFueraLaboral.disabled = false;
                ddlNoFueraLaboral.value = 0;
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
    catch (ex) {}
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
    catch (ex) {}
}
function ddlEstadoLegal() {
    try {
        var ddlArrestado = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlArrestado");
        var ddlArrestado30 = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlArrestado30");
        switch (document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlEstadoLegal").value) {
            case ("1"): case ("2"): case ("3"): case ("5"): case ("6"): case ("9"): case ("11"): case ("12"): case ("14"): case ("15"): case ("22"):             
                ddlArrestado.value = 1;
                ddlArrestado.disabled = true;
                ddlArrestado30.disabled = false;
                ddlArrestado();
                ddlArrestado30();
                break;
            default:
                ddlArrestado.disabled = false;
               // ddlArrestado.value = 0;
                ddlArrestado();
                ddlArrestado30();
                break;
        }
    }
    catch (ex) { }
}
function ddlEstadoLegal_Load() {
    try {
        switch (document.getElementById("hFKPrograma").value) {
            case ("27"): case ("31"): case ("32"): case ("33"): case ("34"): case ("35"): case ("36"): case ("37"): case ("38"): case ("39"): case ("40"): case ("41"): case ("42")://TASC
                $("#mainBodyContent_WucEpisodioAdmision_ddlEstadoLegal option").filter(function () { return ["0", "1", "2"].indexOf(this.value) < 0; }).remove();
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
                txtArrestos3O.disabled = false;
                ddlArrestado3O.disabled = false;
                ddlArrestado30();
                break;
            case ("2"): //No
                txtArrestos3O.value = '0';
                ddlArrestado3O.value = 2;
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
        var txtArrestos30 = document.getElementById("mainBodyContent_WucEpisodioAdmision_txtArrestos30");
        var ddlArrestado30 = document.getElementById("mainBodyContent_WucEpisodioAdmision_ddlArrestado30");
        var rvArrestos30 = document.getElementById("mainBodyContent_WucEpisodioAdmision_rvArrestos30");       
        switch (ddlArrestado30.value) {
            case ("1")://Sí
                if (txtArrestos30.value == "0") {
                    txtArrestos30.value = "";
                }
                rvArrestos30.minimumvalue = "1";
                txtArrestos30.disabled = false;
                break;
            case ("2"): case ("")://No
                txtArrestos30.value = '0';
                rvArrestos30.minimumvalue = "0";
                txtArrestos30.disabled = true;     
                break;
            /*default:
                rvArrestos30.minimumvalue = "0";
                txtArrestos30.disabled = false;
                break; */
        }
    }
    catch (ex) { }
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
                ddlFreq_AutoAyuda.disabled = false;
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

        ddlViaPrim.value = 0;
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
            case (sustanciasList.Noaplica): case (sustanciasList.Noinformó):
            case ("95"): case ("98")://OLDVALUES
               
                    if ((ddlNivelCuidadoSustancias.value !== "99")
                        ||
                        ((ddlNivelCuidadoSustancias.value === "99" && ddlNivelCuidadoSaludMental.value === "99") &&
                        (CO_Tipo.value === "1" || CO_Tipo.value === "4" || ddlDSMVDiagDual.value === "1"))) {
                    ddlDrogaPrim.value = 0;

                        if ((ddlNivelCuidadoSustancias.value !== "99") || (CO_Tipo.value === "1" || CO_Tipo.value === "4")) {
                        alert("Este perfil es de Abuso de Sustancia, no puede seleccionar " + "'" + "No Aplica" + "'" + ".");
                    }
                    else {
                         
                        // alert("Este perfil esta seleccionado como CONCURRENTE, no puede seleccionar " + "'" + "No Aplica" + "'" + ".");
                                                                   
                        ddlDrogaPrim.value = 96;
                        ddlViaPrim.value = 95;
                        ddlFrecPrim.value=99;
                        txtEdadPrim.value = 0;

                        ddlViaPrim.disabled = true;
                        ddlFrecPrim.disabled = true;
                        txtEdadPrim.disabled = true;
 
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
                if ((ddlNivelCuidadoSustancias.value !== "99") ||
                    ((ddlNivelCuidadoSustancias.value === "99" && ddlNivelCuidadoSaludMental.value === "99") &&
                        (CO_Tipo.value === "1" || CO_Tipo.value === "4" || ddlDSMVDiagDual.value === "1"))) {
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
                
                break; 
            default:
                ddlViaPrim.disabled = false;
                ddlDrogaSec.disabled = false; 
                break;
        }
        ddlViaPrimF();
        ddlDrogaSecF();
    }
    catch (ex) { alert(ex.text); }
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
        ddlViaSec.disabled = false;
        ddlFrecSec.disabled = false;
        txtEdadSec.disabled = false;

        if (!(ddlDrogaPrim.value === sustanciasList.Noaplica || ddlDrogaPrim.value === sustanciasList.Nousaactualmente || ddlDrogaPrim.value === "0") && (ddlViaPrim.value === "0" || ddlViaPrim.value == viaList.NoAplica || ddlFrecPrim.value === 0 || ddlFrecPrim.value === 99 || txtEdadPrim.value < "1") && !(ddlDrogaSec.value === sustanciasList.Noaplica || ddlDrogaSec.value === sustanciasList.Nousaactualmente)) {
           
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
                case (sustanciasList.Nousaactualmente): case (sustanciasList.Noaplica): case (sustanciasList.Noinformó):
                case ("95"): case ("98"): //OLDVALUES
                    if ((ddlNivelCuidadoSaludMental.value != "99" && ddlDSMVDiagDual.value === "1") ||
                        (ddlNivelCuidadoSustancias.value !== "99") ||
                        ((ddlNivelCuidadoSustancias.value === "99" && ddlNivelCuidadoSaludMental.value === "99") &&
                            (CO_Tipo.value === "1" || CO_Tipo.value === "4" || ddlDSMVDiagDual.value === "1"))) {
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
            if (ddlDrogaSec.value !== sustanciasList.Nousaactualmente && ddlDrogaSec.value !== sustanciasList.Noaplica && ddlDrogaSec.value !== sustanciasList.Noinformó && ddlViaSec.value !== 0) {
                if (ddlDrogaSec.value === ddlDrogaPrim.value && ddlViaSec.value === ddlViaPrim.value) {
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

        if (!(ddlDrogaSec.value === sustanciasList.Noaplica || ddlDrogaSec.value === "0" || ddlDrogaSec.value === sustanciasList.Nousaactualmente) && (ddlViaSec.value === "0" || ddlViaSec.value === viaList.NoAplica || ddlFrecSec.value === 0 || ddlFrecSec.value === 99 || txtEdadSec.value < "1") && !(ddlDrogaTerc.value === sustanciasList.Noaplica || ddlDrogaTerc.value === sustanciasList.Nousaactualmente)) {
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
                case (sustanciasList.Nousaactualmente): case (sustanciasList.Noaplica): case (sustanciasList.Noinformó):
                case ("95"): case ("98")://OLDVALUES
                    if ((ddlNivelCuidadoSaludMental.value != "99" && ddlDSMVDiagDual.value === "1") ||
                        (ddlNivelCuidadoSustancias.value !== "99") ||
                        ((ddlNivelCuidadoSustancias.value === "99" && ddlNivelCuidadoSaludMental.value === "99") &&
                            (CO_Tipo.value === "1" || CO_Tipo.value === "4" || ddlDSMVDiagDual.value === "1"))) {
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
        if (ddlDrogaTerc.value !== sustanciasList.Nousaactualmente && ddlDrogaTerc.value !== sustanciasList.Noaplica && ddlDrogaTerc.value !== sustanciasList.Noinformó && ddlViaTerc.value !== 0) {
            if (ddlDrogaTerc.value === ddlDrogaSec.value && ddlViaTerc.value === ddlViaSec.value) {
                alert("La droga y la vía de utilización NO pueden ser igual a las selecciones de la segunda sustancia.");
                ddlDrogaTerc.value = sustanciasList.Nousaactualmente;
                ddlViaTerc.value = viaList.NoAplica;
                ddlFrecTerc.value = 99;
                txtEdadTerc.value = "0";
                ddlViaTerc.disabled = true;
                ddlFrecTerc.disabled = true;
                txtEdadTerc.disabled = true;

            }
            else if (ddlDrogaTerc.value === ddlDrogaPrim.value && ddlViaTerc.value === ddlViaPrim.value) {
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
    catch (ex) { alert(ex.text);}
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

                    txtDíasSustancias.disabled = false;
                    opiod.value = "0";
                    opiod.disabled = false;
                    //Opiaceos
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

                    if (ddlNivelCuidadoSaludMental.value == "99") {
                        txtDíasSustancias.value = "0";
                        txtDíasSustancias.disabled = true;
                        opiod.value = "4";
                        opiod.disabled = true;
                    }
                    //Opiaceos

            }
        }
        else if ((ddlNivelCuidadoSustancias.value !== "99") ||
            ((ddlNivelCuidadoSustancias.value === "99" && ddlNivelCuidadoSaludMental.value === "99") &&
                (CO_Tipo.value === "1" || CO_Tipo.value === "4"))){
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

var saving = false;
function validate() {

    var isValid = Page_ClientValidate();
    if (!saving) {
        if (isValid) {
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
        var GAF = document.getElementById("mainBodyContent_"+txtDSMVFnGlobal);
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
    catch (ex) { alert(ex.message);}
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

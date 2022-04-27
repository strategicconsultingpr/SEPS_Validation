onload = function () {
    if ($("#mainBodyContent_ddlFiltroDeFecha").length != 0) {
        ddlFiltroDeFecha();
    }
}

function ddlFiltroDeFecha() {
    try {
        switch ($('#mainBodyContent_ddlFiltroDeFecha').val()) {
            case ("1")://Fecha exacta
                $('#mainBodyContent_txtAñoRangoInicio').val('');
                $('#mainBodyContent_txtAñoRangoFin').val('');
                $('#mainBodyContent_ddlDíaRangoFin').attr('selectedIndex', 0);
                $('#mainBodyContent_ddlMesRangoFin').attr('selectedIndex', 0);
                $('#mainBodyContent_ddlDíaRangoInicio').attr('selectedIndex', 0);
                $('#mainBodyContent_ddlMesRangoInicio').attr('selectedIndex', 0);
                $('#divRangosDeFechas').hide();
                $('#divFechasExactas').show();
                break;
            case ("2")://Rango de fecha
                $('#mainBodyContent_txtAñoExacta').val('');
                $('#mainBodyContent_ddlDíaExacta').attr('selectedIndex', 0);
                $('#mainBodyContent_ddlMesExacta').attr('selectedIndex', 0);
                $('#divFechasExactas').hide();
                $('#divRangosDeFechas').show();
                break;
            default: break;
        }
    }
    catch (ex) { }
}
function ddlMesNuevo(wuc, ddlDía, ddlMes, postfix) {
    try {
        if (typeof (postfix) == "undefined") {
            postfix = "";
        }
        
        var ddlDía = document.getElementById('mainBodyContent_' + wuc + ddlDía + postfix);
        var ddlMes = document.getElementById('mainBodyContent_' + wuc + ddlMes + postfix);
        if (wuc != '' && wuc != 'WucOtrosDatosPerfil_' && wuc != 'WucTakeHome_') { //This cases dont use hidden value
            var ddlMesHidden = document.getElementById('mainBodyContent_' + wuc + 'ddlMesHidden');
        }
        ddlDía.value = '1';
        if (wuc != '' && wuc != 'WucOtrosDatosPerfil_' && wuc != 'WucTakeHome_') {
            ddlMesHidden.value = ddlMes.value;
        }
    }
    catch (ex) { throw ex; }
}
function ddlDíaNuevo(wuc, ddlMes, ddlDía, postfix) {
    try {
        if (typeof (postfix) == "undefined") {
            postfix = "";
        }     
        
        var ddlMes = document.getElementById('mainBodyContent_' + wuc + ddlMes + postfix);
        var ddlDía = document.getElementById('mainBodyContent_' + wuc + ddlDía + postfix);
        if (wuc != '' && wuc != 'WucOtrosDatosPerfil_' && wuc != 'WucTakeHome_') {
            var ddlDíaHidden = document.getElementById('mainBodyContent_' + wuc + 'ddlDíaHidden');
        }
        var día = ddlDía.value;
        
        if (wuc != '' && wuc != 'WucOtrosDatosPerfil_' && wuc != 'WucTakeHome_') {
            ddlDíaHidden.value = día;
        }
        switch (ddlMes.value) {
            case ("4"): case ("6"): case ("9"): case ("11"):
                if (día == 31) {
                    alert("El mes seleccionado tiene 30 días, seleccione un día valido.")
                    ddlDía.value = '1';
                }
                break;
            case ("2"):
                if (día > 29) {
                    alert("El mes de Febrero no puede tener un día mayor al 29, seleccione un día valido.")
                    ddlDía.value = '1';
                }
                break;
        }
    }
    catch (ex) { throw ex; }
}
function FechaAdmision(mes_id, día_id, año_id, MesHidden_id, Fe_Nacimiento_id, día_hidden, año_Hidden) {
    try {
         
        var mes = document.getElementById("mainBodyContent_" + mes_id);
        var día = document.getElementById("mainBodyContent_" + día_id);
        var año = document.getElementById("mainBodyContent_" + año_id);
        var MesHidden = document.getElementById("mainBodyContent_" + MesHidden_id);
        var Fe_Nacimiento = document.getElementById("mainBodyContent_" + Fe_Nacimiento_id);
        var día_hidden = document.getElementById("mainBodyContent_" + día_hidden);
        var año_hidden = document.getElementById("mainBodyContent_" + año_Hidden);
        var edadAdmision = document.getElementById("mainBodyContent_wucDatosDemograficos_edadAdmision");
        var btnedadAdmision = document.getElementById("mainBodyContent_wucDatosDemograficos_btnEdadAdmision");
     
      



        var now = new Date();
        var fe_nacimiento = new Date(Fe_Nacimiento.value);
        var FechaActual;
         //alert(((((parseInt(año.value) * 12) * 30.4375) + ((parseInt(mes.value) * 30.4375) + parseInt(día.value))) -
        //    (((fe_nacimiento.getFullYear() * 12) * 30.4375) + ((fe_nacimiento.getMonth() * 30.4375) + fe_nacimiento.getDate()))) / 365.25);
        FechaActual = (now.getMonth() + 1) + '/' + now.getDate() + '/' + now.getFullYear();
        if (año.value < año_hidden) {
            MesHidden.value = mes.value + '/' + día.value + '/' + año.value;

            var fe_admission = new Date(MesHidden.value);
            if (Date.parse(Fe_Nacimiento.value) > Date.parse(MesHidden.value) || Date.parse(Fe_Nacimiento.value) == Date.parse(MesHidden.value)) {
                alert("La fecha de admision no puede ser menor o igual a la fecha de nacimiento");
                día.value = 1;
                año.value = '';
                mes.value = 1;
                mes.focus();
            }
            if (parseInt(mes.value) == 2 && parseInt(día.value) == 29) {
                if (!((parseInt(año.value) % 4 == 0 && parseInt(año.value) % 100 != 0) || (parseInt(año.value) % 400 == 0))) {
                    alert("El año insertado no es un año bisiesto");
                    día.value = 1;
                    año.value = '';
                    mes.value = 1;
                    mes.focus();
                }

            }
            if (Date.parse(MesHidden.value) > Date.parse(FechaActual)) {
                alert("La fecha de admision no puede ser mayor a la fecha de hoy");
                día.value = 1;
                año.value = '';
                mes.value = 1;
                mes.focus(); 
                   
            }
            //if (año.value != '') {  s
            //    var diff = (fe_admission.getTime() - fe_nacimiento.getTime()) / 1000;
            //    diff /= (60 * 60 * 24);
            //   // alert(diff / 365.25);
            //    alert("Edad en admision: "+Math.trunc(diff / 365.25));
            //    // alert(Math.abs(Math.round(diff / 365.25))); 
            //    //document.getElementById('<%=mainBodyContent_wucDatosDemograficos.FindControl("btnEdadAdmision").ClientID %>').click();
            //    btnedadAdmision.click();
               
                
            //}
        }
    }
    catch (ex) { alert(ex.message); }
   
}
function FechaAlta(mes_id, día_id, año_id, FechaAdmision_id, FechaAlta_id, FechaEvaluacion_id) {
    try {
        var FechaAdmision = document.getElementById("mainBodyContent_" + FechaAdmision_id);
        var mes = document.getElementById("mainBodyContent_" + mes_id);
        var día = document.getElementById("mainBodyContent_" + día_id);
        var año = document.getElementById("mainBodyContent_" + año_id);
        var FechaAltaHidden = document.getElementById("mainBodyContent_" + FechaAlta_id);
        var FechaEvaluacionHidden = document.getElementById("mainBodyContent_" + FechaEvaluacion_id);
        FechaAltaHidden.value = mes.value + '/' + día.value + '/' + año.value;
        if (año.value != "") {
            if (Date.parse(FechaAltaHidden.value) < Date.parse(FechaAdmision.value)) {
                alert("La fecha no puede ser menor a la fecha de admision.");
                día.value = 1;
                año.value = '';
                mes.value = 1;
                mes.focus();
            }
            else if (Date.parse(FechaAltaHidden.value) < Date.parse(FechaEvaluacionHidden.value)) {
                alert("La fecha no puede ser menor a la ultima fecha de evaluacion.");
                día.value = 1;
                año.value = '';
                mes.value = 1;
                mes.focus();
            }
            else if (parseInt(mes.value) == 2 && parseInt(día.value) == 29) {
                if (!((parseInt(año.value) % 4 == 0 && parseInt(año.value) % 100 != 0) || (parseInt(año.value) % 400 == 0))) {
                    alert("El año insertado no es un año bisiesto");
                    día.value = 1;
                    año.value = '';
                    mes.value = 1;
                    mes.focus();
                }

            }
        }
    }
    catch (ex) { alert(ex.message); }
}
function LastContactPrevioAPerfil() {
    try {
        var mes = document.getElementById("mainBodyContent_WucOtrosDatosPerfil_ddlMes");
        var día = document.getElementById("mainBodyContent_WucOtrosDatosPerfil_ddlDía");
        var año = document.getElementById("mainBodyContent_WucOtrosDatosPerfil_txtAño");
        var mesC = document.getElementById("mainBodyContent_WucOtrosDatosPerfil_ddlMesContacto");
        var díaC = document.getElementById("mainBodyContent_WucOtrosDatosPerfil_ddlDíaContacto");
        var añoC = document.getElementById("mainBodyContent_WucOtrosDatosPerfil_txtAñoContacto");
        var tipoDePerfilHidden = document.getElementById("mainBodyContent_WucOtrosDatosPerfil_tipoDePerfilHidden");

        if (año.value != "" && añoC.value != "") {
            if (Date.parse(mes.value + '/' + día.value + '/' + año.value) < Date.parse(mesC.value + '/' + díaC.value + '/' + añoC.value)) {
                alert("La fecha de ultimo contacto tiene que ser menor a la fecha de " + tipoDePerfilHidden.value.toLowerCase() + ".");
                díaC.value = 1;
                añoC.value = '';
                mesC.value = 1;
                mesC.focus();
            }
            else if (parseInt(mesC.value) == 2 && parseInt(díaC.value) == 29) {
                if (!((parseInt(añoC.value) % 4 == 0 && parseInt(añoC.value) % 100 != 0) || (parseInt(añoC.value) % 400 == 0))) {
                    alert("El año insertado no es un año bisiesto");
                    díaC.value = 1;
                    añoC.value = '';
                    mesC.value = 1;
                    mesC.focus();
                }
            }
        }
        else if (parseInt(mesC.value) == 2 && parseInt(díaC.value) == 29 && añoC.value != "") {
            if (!((parseInt(añoC.value) % 4 == 0 && parseInt(añoC.value) % 100 != 0) || (parseInt(añoC.value) % 400 == 0))) {
                alert("El año insertado no es un año bisiesto");
                díaC.value = 1;
                añoC.value = '';
                mesC.value = 1;
                mesC.focus();
            }
        }
    }
    catch (ex) { alert(ex.message); }
}

function LastContactCheck() {
    try {
        var ddlMes = document.getElementById("mainBodyContent_WucOtrosDatosPerfil_ddlMesContacto");
        var ddlDía = document.getElementById("mainBodyContent_WucOtrosDatosPerfil_ddlDíaContacto");
        var ddlAño = document.getElementById("mainBodyContent_WucOtrosDatosPerfil_txtAñoContacto");
        var tipoDePerfilHidden = document.getElementById("mainBodyContent_WucOtrosDatosPerfil_tipoDePerfilHidden");

        if (tipoDePerfilHidden.value.toLowerCase() == "alta" && !(ddlMes.value == "1" && ddlDía.value == "1") && ddlAño.value == '') {
            var a = confirm("La fecha de último contacto esta vacia. ¿Desea dejara vacia?") 
            if (a == true) {
                alert("La fecha de último contacto será vacia.");
            }
            else {
                ddlMes.focus();
            }
        }
    }
    catch (ex) { alert(ex.message); }
} 

function IsFutureDate(wuc, dateName, postfix) {
    try {
        if (typeof (postfix) == "undefined") {
            postfix = "";
        }
        var month = document.getElementById("mainBodyContent_" + wuc + "ddlMes" + postfix);
        var day = document.getElementById("mainBodyContent_" + wuc + "ddlDía" + postfix);
        var year = document.getElementById("mainBodyContent_" + wuc + "txtAño" + postfix);
        var now = new Date();
        if (Date.parse(month.value + '/' + day.value + '/' + year.value) > Date.parse((now.getMonth() + 1) + '/' + now.getDate() + '/' + now.getFullYear())) {
            alert("La fecha de " + dateName + " no puede ser mayor a la fecha de hoy.");
            day.value = 1;
            year.value = '';
            month.value = 1;
            month.focus();
        }
    }
    catch (ex) { throw ex; }
}
function FechaConvenio() {
    try {
        var esProgramaAdulto = document.getElementById("mainBodyContent_WucDatosPersonales_hProgramaAdultos");
        var mes = document.getElementById("mainBodyContent_WucDatosPersonales_ddlFechaConvenioMes");
        var día = document.getElementById("mainBodyContent_WucDatosPersonales_ddlFechaConvenioDía");
        var año = document.getElementById("mainBodyContent_WucDatosPersonales_txtFechaConvenioAño");
        var fechaNacimiento = new Date(document.getElementById("mainBodyContent_WucDatosPersonales_lblFENacimientoHidden").value);
        var now = new Date();
        var fechaActual = new Date((now.getMonth() + 1) + '/' + now.getDate() + '/' + now.getFullYear());
        var fechaConvenio = new Date(mes.value + '/' + día.value + '/' + año.value);  
       
        if (fechaConvenio > fechaActual) {
            alert("La fecha de convenio no puede ser mayor a la fecha de hoy.");
            día.value = 1;
            año.value = '';
            mes.value = 1;
            mes.focus();
        }
        else if (esProgramaAdulto.value == true) {
            alert("entre mal");
            var fecha18Yrs = new Date((fechaNacimiento.getMonth() + 1) + '/' + fechaNacimiento.getDate() + '/' + (fechaNacimiento.getFullYear() + 18));           
            if (fecha18Yrs > fechaConvenio) {
                alert("La fecha de convenio no puede ser menor a la fecha en que el paciente cumplio 18 años.");
                día.value = 1;
                año.value = '';
                mes.value = 1;
                mes.focus();
            }
        }        
        else {
            if (fechaNacimiento >= fechaConvenio) {
                alert("La fecha de convenio no puede ser menor o igual a la fecha de nacimiento del paciente.");
                día.value = 1;
                año.value = '';
                mes.value = 1;
                mes.focus();
            }
        }
        if (parseInt(mes.value) == 2 && parseInt(día.value) == 29) {
            if (!((parseInt(año.value) % 4 == 0 && parseInt(año.value) % 100 != 0) || (parseInt(año.value) % 400 == 0))) {
                alert("El año insertado no es un año bisiesto");
                día.value = 1;
                año.value = '';
                mes.value = 1;
                mes.focus();
            }

        }
    }
    catch (e) {
        alert("An error has occurred:\n" + e.message);
    }
}
function TakeHomeFechaEntrada() {
    try {
        var díaFechaEntrada = document.getElementById("mainBodyContent_WucTakeHome_ddlFechaEntradaDía");
        var mesFechaEntrada = document.getElementById("mainBodyContent_WucTakeHome_ddlFechaEntradaMes");
        var añoFechaEntrada = document.getElementById("mainBodyContent_WucTakeHome_txtFechaEntradaAño");
        var fechaEntrada = new Date((mesFechaEntrada.value + '/' + díaFechaEntrada.value + '/' + añoFechaEntrada.value));
        var fechaAdmisión = new Date(document.getElementById("mainBodyContent_WucDatosPersonales_lblFechaAdmision").innerText);
        var fechaAhora = new Date();
        if (isNaN(fechaEntrada.getTime())) {
            if (!IsDayValid(díaFechaEntrada.value, mesFechaEntrada.value)) {
                alert("La combinación de mes y día no es valida.");
                díaFechaEntrada.value = 1;
                mesFechaEntrada.value = 1;
                añoFechaEntrada.value = "";
            }
        }
        else if (!isNaN(fechaAdmisión.getTime()) && !isNaN(fechaEntrada.getTime())) {
            if (!IsDayValid(díaFechaEntrada.value, mesFechaEntrada.value)) {
                alert("La combinación de mes y día no es valida.");
                díaFechaEntrada.value = 1;
                mesFechaEntrada.value = 1;
                añoFechaEntrada.value = "";
            }
            else if (!isNaN(añoFechaEntrada.value)) {
                if (fechaAdmisión > fechaEntrada) {
                    alert("La fecha de entrada debe ser luego de la fecha de admisión.");
                    díaFechaEntrada.value = 1;
                    mesFechaEntrada.value = 1;
                    añoFechaEntrada.value = "";
                }
                else if (fechaEntrada > fechaAhora) {
                    alert("La fecha de entrada no puede ser una fecha futura.");
                    díaFechaEntrada.value = 1;
                    mesFechaEntrada.value = 1;
                    añoFechaEntrada.value = "";
                }
                else if (parseInt(mesFechaEntrada.value) == 2 && parseInt(díaFechaEntrada.value) == 29) {
                    if (!((parseInt(añoFechaEntrada.value) % 4 == 0 && parseInt(añoFechaEntrada.value) % 100 != 0) || (parseInt(añoFechaEntrada.value) % 400 == 0))) {
                        alert("El año insertado no es un año bisiesto");
                        díaFechaEntrada.value = 1;
                        mesFechaEntrada.value = 1;
                        añoFechaEntrada.value = "";
                        mesFechaEntrada.focus();
                    }

                }
            }
        }
        else if (isNaN(fechaAdmisión.getTime())) {
            throw "The date of admission was not available to the system.";
        }
       TakeHomeFechaSalida();
    }
    catch (e) {
        alert("An error has occurred:\n" + e.message);
    }
}
function TakeHomeFechaSalida() {
    try {
        var díaFechaEntrada = document.getElementById("mainBodyContent_WucTakeHome_ddlFechaEntradaDía");
        var mesFechaEntrada = document.getElementById("mainBodyContent_WucTakeHome_ddlFechaEntradaMes");
        var añoFechaEntrada = document.getElementById("mainBodyContent_WucTakeHome_txtFechaEntradaAño");
        var fechaEntrada = new Date((mesFechaEntrada.value + '/' + díaFechaEntrada.value + '/' + añoFechaEntrada.value));
        var díaFechaSalida = document.getElementById("mainBodyContent_WucTakeHome_ddlFechaSalidaDía");
        var mesFechaSalida = document.getElementById("mainBodyContent_WucTakeHome_ddlFechaSalidaMes");
        var añoFechaSalida = document.getElementById("mainBodyContent_WucTakeHome_txtFechaSalidaAño");
        var fechaSalida = new Date((mesFechaSalida.value + '/' + díaFechaSalida.value + '/' + añoFechaSalida.value));
        var fechaAhora = new Date();
        if (isNaN(fechaSalida.getTime())) {                                           // SALIDA INVALID
            if (!IsDayValid(díaFechaSalida.value, mesFechaSalida.value)) {
                alert("La combinación de mes y día no es valida.");
                díaFechaSalida.value = 1;
                mesFechaSalida.value = 1;
                añoFechaSalida.value = "";
            }
        }
        else if (isNaN(fechaEntrada.getTime()) && !isNaN(fechaSalida.getTime())) {    // ONLY SALIDA VALID
            if (!IsDayValid(díaFechaSalida.value, mesFechaSalida.value)) {
                alert("La combinación de mes y día no es valida.");
                díaFechaSalida.value = 1;
                mesFechaSalida.value = 1;
                añoFechaSalida.value = "";
            }
            else if (fechaSalida > fechaAhora) {
                alert("La fecha de salida no puede ser una fecha futura.");
                díaFechaSalida.value = 1;
                mesFechaSalida.value = 1;
                añoFechaSalida.value = "";
            }
        }
        else if (!isNaN(fechaEntrada.getTime()) && !isNaN(fechaSalida.getTime())) {   // BOTH VALID DATES
            if (!IsDayValid(díaFechaSalida.value, mesFechaSalida.value)) {
                alert("La combinación de mes y día no es valida.");
                díaFechaSalida.value = 1;
                mesFechaSalida.value = 1;
                añoFechaSalida.value = "";
            }
            else if (fechaSalida < fechaEntrada && añoFechaSalida.value == "") {
                return;
            }
            else if (fechaSalida < fechaEntrada ) {
                alert("La fecha de salida debe ser despues de la fecha de entrada.");
                díaFechaSalida.value = 1;
                mesFechaSalida.value = 1;
                añoFechaSalida.value = "";
            }
            else if (fechaSalida > fechaAhora) {
                alert("La fecha de salida no puede ser una fecha futura.");
                díaFechaSalida.value = 1;
                mesFechaSalida.value = 1;
                añoFechaSalida.value = "";
            }
            else if (parseInt(mesFechaSalida.value) == 2 && parseInt(díaFechaSalida.value) == 29) {
                if (!((parseInt(añoFechaSalida.value) % 4 == 0 && parseInt(añoFechaSalida.value) % 100 != 0) || (parseInt(añoFechaSalida.value) % 400 == 0))) {
                    alert("El año insertado no es un año bisiesto");
                    díaFechaSalida.value = 1;
                    mesFechaSalida.value = 1;
                    añoFechaSalida.value = "";
                    mesFechaSalida.focus();
                }

            }
        }
    }
    catch (e) {
        alert("An error has occurred:\n" + e.message);
    }
}

function IsDayValid(day, month) {
    try {
        switch (month) {
            case ("4"): case ("6"): case ("9"): case ("11"):
                if (day == 31) {
                    return false;
                }
                else {
                    return true;
                }
                break;
            case ("2"):
                if (day > 29) {
                    return false
                }
                else {
                    return true;
                }
            default: return true;
        }
    }
    catch (ex) { return false; }
}
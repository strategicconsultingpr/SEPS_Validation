<%@ Page language="c#" Inherits="ASSMCA.Perfiles.frmAlta" Codebehind="frmAlta.aspx.cs" MasterPageFile="~/MainUIFP.Master" %>
<%@ Register TagPrefix="uc1" TagName="wucOtrosDatosPerfil" Src="wucOtrosDatosPerfil.ascx" %>
<%@ Register TagPrefix="uc1" TagName="wucDatosDemograficosPerfil" Src="wucDatosDemograficosPerfil.ascx" %>
<%@ Register TagPrefix="uc1" TagName="wucEpisodioPerfil" Src="wucEpisodioPerfil.ascx" %>
<%@ Register TagPrefix="uc1" TagName="wucDatosPersonales" Src="wucDatosPersonales.ascx" %>
<%@ Register TagPrefix="uc1" TagName="wucTakeHome" Src="wucTakeHome.ascx" %>
<%@ Register TagPrefix="uc1" TagName="wucDatosAlta" Src="wucDatosAlta.ascx" %>
<asp:Content ID="evalMainC" runat="server" ContentPlaceHolderID="mainBodyContent">
    <h1 style="display:inline">Perfil de alta</h1> <h2 style="display:inline"><asp:Label ID="lblTipoPerfil" runat="server" ></asp:Label></h2>
    <h1></h1>
    <input type="hidden" id="hTipoPagina" value="alta"/>
    <asp:ScriptManager ID="what" runat = "server"/>
    <uc1:wucdatospersonales id="WucDatosPersonales" runat="server"/>
    <uc1:wucotrosdatosperfil id="WucOtrosDatosPerfil" runat="server" />
    <uc1:wucdatosdemograficosperfil id="WucDatosDemograficosPerfil" runat="server" />
    <uc1:wucepisodioperfil id="WucEpisodioPerfil" runat="server" />
    <uc1:wucTakeHome id="WucTakeHome" runat="server" />
    <uc1:wucdatosalta id="WucDatosAlta" runat="server" />
    <div class="btn-group hidden-print" role="group">          
        <asp:button id="btnGuardarCambios" runat="server" CssClass="btn btn-default" Text="Guardar cambios" CausesValidation="false" OnClientClick="return validate();" onclick="btnGuardarCambios_Click"  />
        <asp:button id="btnRegistrar" runat="server" CssClass="btn btn-default" Text="Registrar perfil de alta" CausesValidation="false" OnClientClick=" return validate(); LastContactCheck();" onclick="btnRegistrar_Click" />
        <asp:button id="btnEliminar" runat="server" CssClass="btn btn-default" Text="Eliminar (Fisicamente)" onclick="btnEliminar_Click" />
        <asp:button id="btnEliminarAdmin" runat="server" CssClass="btn btn-default" Text="Eliminar (Logicamente)" onclick="btnEliminarAdmin_Click"  />
        <asp:button id="btnModificar" runat="server" CssClass="btn btn-default"  Text="Modificar perfil" onclick="btnModificar_Click"  />
        <asp:button id="btnModificarAdmin" runat="server" CssClass="btn btn-default"  Text="Modificar (Admin)" onclick="btnModificarAdmin_Click" />
    </div>
    <asp:validationsummary id="ValidationSummary1" runat="server" Width="496px" ShowSummary="False" HeaderText="Se han encontrado algunos errores en el formulario que debe revisar antes de registrar el perfil de alta. Los siguientes campos son requeridos o contienen valores incorrectos:" ShowMessageBox="True"/>
</asp:Content>
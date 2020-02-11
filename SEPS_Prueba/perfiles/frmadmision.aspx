<%@ Page Language="c#" Inherits="ASSMCA.Perfiles.frmAdmision" MaintainScrollPositionOnPostback="true" CodeBehind="frmAdmision.aspx.cs" MasterPageFile="~/MainUIF.Master" %>

<%@ Register TagPrefix="uc1" TagName="wucDatosAdmision" Src="wucDatosAdmision.ascx" %>
<%@ Register TagPrefix="uc1" TagName="wucOtrosDatos" Src="wucOtrosDatos.ascx" %>
<%@ Register TagPrefix="uc1" TagName="wucDatosDemograficos" Src="wucDatosDemograficos.ascx" %>
<%@ Register TagPrefix="uc1" TagName="wucDatosPersonales" Src="wucDatosPersonales.ascx" %>
<%@ Register TagPrefix="uc1" TagName="wucEpisodioAdmision" Src="wucEpisodioAdmision.ascx" %>

<asp:Content ID="mainC" runat="server" ContentPlaceHolderID="mainBodyContent">
    <asp:ScriptManager ID="mainScript" runat="server" />
  

    <%--<h1>Perfil de admisión</h1><asp:Label ID="lblTipoPerfil" runat="server" ></asp:Label>--%>
    <h1 style="display:inline">Perfil de admisión </h1> <h2 style="display:inline"><asp:Label ID="lblTipoPerfil" runat="server" ></asp:Label></h2>
    <h1></h1>
    <input type="hidden" id="hTipoPagina" value="admisión" />
    <input type="hidden" id="postbackControl" value="<%=Page.IsPostBack.ToString()%>" />
    <div>
        <uc1:wucDatosPersonales ID="WucDatosPersonales" runat="server" />
        <uc1:wucOtrosDatos ID="WucOtrosDatos" runat="server" />
        <uc1:wucDatosDemograficos ID="WucDatosDemograficos" runat="server" />
        <uc1:wucEpisodioAdmision ID="WucEpisodioAdmision" runat="server" />
        <uc1:wucDatosAdmision ID="WucDatosAdmision" runat="server" />
        <div class="btn-group hidden-print" role="group">
            <asp:Button ID="btnGuardarCambios" runat="server" CssClass="btn btn-default" Text="Guardar cambios" CausesValidation="false" OnClientClick="return validate();" OnClick="btnGuardarCambios_Click" />
            <asp:Button ID="btnRegistrar" runat="server" CssClass="btn btn-default" Text="Registrar perfil de admisión" CausesValidation="false" OnClientClick="return validate();" OnClick="btnRegistrar_Click" TabIndex="14" />
            <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-default" Text="Eliminar (Fisicamente)" OnClick="btnEliminar_Click" />
            <asp:Button ID="btnEliminarAdmin" runat="server" CssClass="btn btn-default" Text="Eliminar (Logicamente)" OnClick="btnEliminarAdmin_Click" />
            <asp:Button ID="btnModificar" runat="server" CssClass="btn btn-default" Text="Modificar perfil" OnClick="btnModificar_Click" />
            <asp:Button ID="btnModificarAdmin" runat="server" CssClass="btn btn-default" Text="Modificar (Admin)" OnClick="btnModificarAdmin_Click" />
        </div>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="False" HeaderText="Se han encontrado algunos errores en el formulario que debe revisar antes de registrar el perfil de admisión. Los siguientes campos son requeridos o contienen valores incorrectos:" ShowMessageBox="True" />

 
</asp:Content>

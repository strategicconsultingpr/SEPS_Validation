<%@ Page Language="c#" Inherits="ASSMCA.Perfiles.frmEvaluacion" CodeBehind="frmEvaluacion.aspx.cs" MasterPageFile="~/MainUIFP.Master"  MaintainScrollPositionOnPostback="true" %>
<%@ Register TagPrefix="uc1" TagName="wucDatosPersonales" Src="wucDatosPersonales.ascx" %>
<%@ Register TagPrefix="uc1" TagName="wucOtrosDatosPerfil" Src="wucOtrosDatosPerfil.ascx" %>
<%@ Register TagPrefix="uc1" TagName="wucDatosEvaluacion" Src="wucDatosEvaluacion.ascx" %>
<%@ Register TagPrefix="uc1" TagName="wucDatosDemograficosPerfil" Src="wucDatosDemograficosPerfil.ascx" %>
<%@ Register TagPrefix="uc1" TagName="wucEpisodioPerfil" Src="wucEpisodioPerfil.ascx" %>
<%@ Register Src="wucTakeHome.ascx" TagName="wucTakeHome" TagPrefix="uc2" %>
<asp:Content ID="mainEvalC" runat="server" ContentPlaceHolderID="mainBodyContent">
    <asp:ScriptManager ID="what" runat="server" />
    <h1 style="display:inline">Perfil de evaluación</h1> <h2 style="display:inline"><asp:Label ID="lblTipoPerfil" runat="server" ></asp:Label></h2>
    <h1></h1>
    <input type="hidden" id="hTipoPagina" value="evaluación"/>
    <input type="hidden" id="postbackControl" value="<%=Page.IsPostBack.ToString()%>" />
    <uc1:wucDatosPersonales         ID="WucDatosPersonales"         runat="server" />
    <uc1:wucOtrosDatosPerfil        ID="WucOtrosDatosPerfil"        runat="server" />
    <uc1:wucDatosDemograficosPerfil ID="WucDatosDemograficosPerfil" runat="server" />
    <uc1:wucEpisodioPerfil          ID="WucEpisodioPerfil"          runat="server" />
    <uc2:wucTakeHome                ID="WucTakeHome"                runat="server" />
    <uc1:wucDatosEvaluacion         ID="WucDatosEvaluacion"         runat="server" />
    <div class="btn-group hidden-print" role="group">  
        <asp:Button ID="btnGuardarCambios"  runat="server" CssClass="btn btn-default" Text="Guardar cambios" CausesValidation="false" OnClientClick="return validate();" OnClick="btnGuardarCambios_Click" />
        <asp:Button ID="btnRegistrar"       runat="server" CssClass="btn btn-default" Text="Registrar perfil de evaluación" CausesValidation="false" OnClientClick="return validate();" OnClick="btnRegistrar_Click" />
        <asp:Button ID="btnEliminar"        runat="server" CssClass="btn btn-default" Text="Eliminar (Fisicamente)" OnClick="btnEliminar_Click" />
        <asp:Button ID="btnEliminarAdmin"   runat="server" CssClass="btn btn-default" Text="Eliminar (Logicamente)" OnClick="btnEliminarAdmin_Click" />
        <asp:Button ID="btnModificar"       runat="server" CssClass="btn btn-default" Text="Modificar perfil" OnClick="btnModificar_Click" />
        <asp:Button ID="btnModificarAdmin"  runat="server" CssClass="btn btn-default" Text="Modificar (Admin)" OnClick="btnModificarAdmin_Click" />
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="496px" ShowSummary="False" HeaderText="Se han encontrado algunos errores en el formulario que debe revisar antes de registrar el perfil de evaluación. Los siguientes campos son requeridos o contienen valores incorrectos:" ShowMessageBox="True" />
</asp:Content>
<%@ Page Language="c#" Inherits="ASSMCA.Perfiles.frmEliminarPerfil" CodeBehind="frmEliminarPerfil.aspx.cs" MasterPageFile="~/MainUIFP.Master" %>
<asp:Content ID="deleteMainC" runat="server" ContentPlaceHolderID="mainBodyContent">
    <h1>Eliminaci�n de Perfil</h1>
    <asp:Label ID="lblMsg" runat="server" />
    <div class="btn-group" role="group"> 
        <asp:Button ID="btnPagPrincipal" runat="server"  CssClass="btn btn-default"  Text="Ir a P�g. Principal" Visible="False" OnClick="btnPagPrincipal_Click" />
        <asp:Button ID="btnEliminar" runat="server"  CssClass="btn btn-default"  Text="Continuar con la eliminaci�n del registro" OnClick="btnEliminar_Click" />
    </div>
</asp:Content>

<%@ Page language="c#" Inherits="ASSMCA.Error" Codebehind="Error.aspx.cs" MasterPageFile="~/Main.Master" %>
<asp:Content ID="mainContent" runat="server" ContentPlaceHolderID="mainBodyContent">
    <div>
        <h1>Ha ocurrido un error.</h1>
        <div><asp:Label id="lblError" runat="server" Font-Bold="True" Font-Size="15pt">Un error inesperado ha ocurrido y el administrador del sistema ha sido notificado.</asp:Label></div>
        <div><asp:Label ID="lblErrorMsg" runat="server" /></div>
	</div>
</asp:Content>
<%@ Page language="c#" Inherits="ASSMCA.ErrorView" Codebehind="ErrorView.aspx.cs" MasterPageFile="~/Main.Master" %>
<asp:Content ID="mainContent" runat="server" ContentPlaceHolderID="mainBodyContent">
    <div id ="standard" runat="server">
    <h1>Error view</h1>
      <asp:DataGrid ID="dgErrors" runat="server" CssClass="table table-condensed table-responsive table-hover" AutoGenerateColumns="False" CellPadding="2" GridLines="None" AllowSorting="True">
        <Columns>
            <asp:BoundColumn DataField="IN_Error" SortExpression="IN_Error" HeaderText="Error"/>
            <asp:BoundColumn DataField="PK_Error" SortExpression="PK_Error" HeaderText="# Ref."/>
            <asp:BoundColumn DataField="FE_Error" SortExpression="FE_Error" HeaderText="Date"/>
        </Columns>
    </asp:DataGrid>
        </div>
    <div id ="distinct" runat="server">
    <h1>Error view (distinct)</h1>
      <asp:DataGrid ID="dgErrorsDistinct" runat="server" CssClass="table table-condensed table-responsive table-hover" AutoGenerateColumns="False" CellPadding="2" GridLines="None" AllowSorting="True">
        <Columns>
            <asp:BoundColumn DataField="IN_Error" SortExpression="IN_Error" HeaderText="Error"/>
        </Columns>
    </asp:DataGrid>
        </div>
</asp:Content>
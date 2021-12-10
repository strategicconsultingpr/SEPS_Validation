<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ViewReport.aspx.cs" Inherits="SEPS.WebForm1" %>
 <%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainBodyContent" runat="server">
      <h1 runat="server" id="LblTitle">Ver Reporte</h1>

     <asp:ScriptManager ID="ScriptManagerReport" runat="server"></asp:ScriptManager>        
        <rsweb:ReportViewer ID="rvSiteMapping" runat ="server" ShowPrintButton="false"  Width="99.9%" Height="100%" AsyncRendering="true" ZoomMode="Percent" KeepSessionAlive="true" SizeToReportContent="false"  ProcessingMode="Remote">
        </rsweb:ReportViewer>


<%--   <asp:ScriptManager ID="ScriptManagerReport" runat="server">

            </asp:ScriptManager>
            <rsweb:ReportViewer id="rvSiteMapping" runat ="server" ShowPrintButton="false"  Width="99.9%" Height="100%" AsyncRendering="true" ZoomMode="Percent" KeepSessionAlive="true" SizeToReportContent="false" ></rsweb:ReportViewer>--%>

</asp:Content>

<%@ Control Language="c#" Inherits="ASSMCA.Perfiles.wucDatosEvaluacion" CodeBehind="wucDatosEvaluacion.ascx.cs" %>
<%--Comentarios Evaluación--%>
<div class="panel panel-default" id="divComentarios" runat="server">
  <div class="panel-heading">
    <h3 class="panel-title">Comentarios</h3>
  </div>
  <div class="panel-body">
    <asp:TextBox CssClass="form-control" ID="txtComentarios" runat="server" Width="100%" TextMode="MultiLine" Height="64px" MaxLength="1500"  TabIndex="13" />
    <asp:Label Visible="false" ID="lblComentario" runat="server" Width="100%"/>
  </div>
</div>

<%@ Control Language="c#" Inherits="ASSMCA.Perfiles.wucOtrosDatos" CodeBehind="wucOtrosDatos.ascx.cs" %>
<div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Otros datos</h3>
  </div>
  <div class="panel-body">
    <div class="row">
    <div class="col-md-12 col-lg-6 SEPSDivsInfo"><%-- Nombre centro / Unidad de servicio --%>
        <span class="SEPSLabel">Nombre del centro / Unidad de servicio:</span>
        <asp:Label ID="lblCentro" runat="server"/>
    </div>
    <div class="col-print-6 col-md-6 col-lg-3 SEPSDivsInfo"><%--Número de Episodio --%>
        <span class="SEPSLabel">Número del episodio:</span>
        <asp:Label ID="lblEpisodio" runat="server"/>
        <asp:Label ID="lblEpisodioNuevo" runat="server" Visible="False">(Sin número asignado)</asp:Label>
    </div>
    <div class="col-print-6 col-md-6 col-lg-3 SEPSDivsInfo"><%-- Número de Perfil --%>
        <span class="SEPSLabel">Número del perfil:</span>
        <asp:Label ID="lblPerfil" runat="server"/>
        <asp:Label ID="lblPerfilNuevo" runat="server" Visible="False">(Sin número asignado)</asp:Label>
    </div>
</div>
<div class="row">
    <div class="col-print-12 col-md-6 SEPSDivs"><%-- Seguro de Salud --%>
        <span class="SEPSLabel">Seguro de salud:</span>
        <asp:RequiredFieldValidator ID="rfvSeguroSalud" runat="server" Display="Dynamic" CssClass="rightFloatAsterisk" ErrorMessage="Seguro de salud" ControlToValidate="ddlSeguroSalud" InitialValue="0" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*" />
        <div class="expandibleDiv">
            <asp:DropDownList ID="ddlSeguroSalud" CssClass="form-control"  runat="server" DataSource="<%# dvwSeguroSalud %>" DataTextField="DE_SeguroSalud" DataValueField="PK_SeguroSalud"/>
            <asp:Label ID="lblSeguroSalud" runat="server"/>
        </div>
    </div>
    <div class="col-print-12 col-md-6 SEPSDivs"><%-- Fuente de pago --%>
        <span class="SEPSLabel">Fuente del pago:</span>
        <asp:RequiredFieldValidator ID="rfvFuentePago" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic" ErrorMessage="Fuente del pago" ControlToValidate="ddlFuentePago" InitialValue="0" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*" />
        <div class="expandibleDiv">
            <asp:DropDownList ID="ddlFuentePago" CssClass="form-control"  runat="server" DataSource="<%# dsPerfil %>" DataMember="SA_LKP_TEDS_PAGO" DataTextField="DE_Pago" DataValueField="PK_Pago"/>
            <asp:Label ID="lblFuentePago" runat="server"/>
        </div>
    </div>
</div>
  </div>
</div>
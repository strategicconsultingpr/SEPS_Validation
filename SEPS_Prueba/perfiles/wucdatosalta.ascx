<%@ Control Language="c#" Inherits="ASSMCA.Perfiles.wucDatosAlta" Codebehind="wucDatosAlta.ascx.cs" %>
    <div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Razón de alta</h3>
  </div>
  <div class="panel-body">
        <div class="row">
        <div class="col-md-12 SEPSDivs"><%--Razón de Alta--%>
            <span class="SEPSLabel SEPSDivs">Razón de alta:</span>
            <div class="expandibleDiv">
                <asp:dropdownlist CssClass="form-control" ID="ddlRazonAlta" runat="server" DataTextField="DE_Alta" DataValueField="PK_Alta" DataMember="SA_LKP_ALTA" DataSource="<%# dvwRazonAlta %>" onChange="ddlRazonAlta();"/>
                <asp:label id="lblRazonAlta" runat="server"/>
            </div>
        </div>
        <div class="col-md-6 SEPSDivs"><%--Centro Traslado--%>
            <span class="SEPSLabel">Centro de traslado:</span>
            <div class="expandibleDiv">
                <asp:dropdownlist CssClass="form-control" Width="100%" id=ddlCentroReferido runat="server" DataTextField="NB_Programa" DataValueField="PK_Programa" DataMember="SA_LKP_PROGRAMAS" DataSource="<%# dsPerfil %>"/>
                <asp:label id="lblCentroReferido" runat="server"/>
            </div>
        </div>
        <div class="col-md-6 SEPSDivs"><%--Categoría de centro privado--%>
            <span class="SEPSLabel">Categoría de centro privado:</span>
            <div class="expandibleDiv">
                <asp:dropdownlist CssClass="form-control" id=ddlCategoriasDeCentrosPrivados runat="server"/>
                <asp:label id="lblCategoriaDeCentroPrivado" runat="server"/>
            </div>
        </div>
    </div>
  </div>
</div>

    <div class="panel panel-default" id="divComentarios" runat="server">
  <div class="panel-heading">
    <h3 class="panel-title">Comentarios</h3>
  </div>
  <div class="panel-body">
        <div class="row">
        <div class="col-xs-12"><%--Comentarios TabIndex="13"--%>
            <asp:textbox CssClass="form-control" id="txtComentarios" runat="server" MaxLength="1500" TextMode="MultiLine" Width="100%" Height="64px"  />
            <asp:Label ID="lblComentario" runat="server" Width="100%"/>
        </div>
    </div>
  </div>
</div>
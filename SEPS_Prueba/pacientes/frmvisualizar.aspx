<%@ Page Language="c#" Inherits="ASSMCA.Pacientes.frmVisualizar" CodeBehind="frmVisualizar.aspx.cs" MasterPageFile="~/Main.Master" %>
<asp:Content ID="mainC" runat="server" ContentPlaceHolderID="mainBodyContent">
    <h1>Visualización de participante</h1>
    <div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Datos del participante</h3>
  </div>
  <div class="panel-body">
     <div class="row">
        <div class="col-sm-6 col-lg-3"><%-- Primer apellido --%>
            <span class="SEPSLabel">Primer apellido:</span>
            <asp:Label ID="Label29" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].AP_Primero") %>'/>
        </div>
        <div class="col-sm-6 col-lg-3"><%-- Segundo apellido --%>
            <span class="SEPSLabel">Segundo apellido:</span>
            <asp:Label ID="Label27" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].AP_Segundo") %>'/>
        </div>
        <div class="col-sm-6 col-lg-3"><%-- Primer nombre --%>
            <span class="SEPSLabel">Primer nombre:</span>
            <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].NB_Primero") %>'/>
        </div>
        <div class="col-sm-6 col-lg-3"><%-- Segundo nombre --%>
            <span class="SEPSLabel">Segundo nombre:</span>
            <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].NB_Segundo") %>'/>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-6 col-lg-3"><%-- IUP --%>
            <span class="SEPSLabel" title='Identificador Único de Persona. Código único para cada persona registrada en el sistema. Toda persona tiene un número global diferente que lo identifica univocamente dentro del sistema, y lo diferencia de todos las otras personas registradas. Este código de obiene luego de registrar una persona por medio de la funcionalidad "Registrar" del menú "Persona".'>IUP:</span>
            <asp:Label ID="lblIUP" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].PK_Persona") %>' />
        </div>
        <div class="col-sm-6 col-lg-3"><%-- Expediente --%>
            <span class="SEPSLabel" title="Número entero que identifica una persona para un programa particular. Toda persona debe puede tener mas de un número de expediente diferente dado que haya participado en más de un programa.">Expediente:</span>
            <asp:Label ID="Label23" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].NR_Expediente") %>' />
        </div>
        <div class="col-sm-6 col-lg-3"><%-- Seguro Social --%>
            <span class="SEPSLabel">Seguro social:</span>
            <asp:Label ID="Label21" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].NR_SeguroSocial") %>'/>
        </div>
        <div class="col-sm-6 col-lg-3"><%-- Fecha de nacimiento --%>
            <span class="SEPSLabel">Fecha de nacimiento:</span>
            <asp:Label ID="Label37" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].FE_Nacimiento", "{0:d}") %>'/>
        </div>
        <div class="col-sm-6 col-lg-3"><%-- Edad --%>
            <span class="SEPSLabel">Edad:</span>
            <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].NR_Edad") %>'/>
        </div> 
        <div class="col-sm-6 col-lg-3"><%-- Sexo --%>
            <span class="SEPSLabel">Sexo:</span>
            <asp:Label ID="Label19" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].DE_Sexo") %>'/>
        </div>    
        <div class="col-sm-6 col-lg-3"><%-- Veterano --%>
            <span class="SEPSLabel">Veterano:</span>
            <asp:Label ID="Label33" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].DE_Veterano") %>'/>
        </div>
        <div class="col-sm-6 col-lg-3"><%-- Grupo Etnico --%>
            <span class="SEPSLabel">Grupo étnico:</span>
            <asp:Label ID="Label31" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].DE_GrupoEtnico") %>'/>
        </div>  
        <div class="col-xs-12"><%-- Raza(s) --%>
            <span class="SEPSLabel">Raza(s):</span>
            <asp:Label ID="lblRazas" runat="server"/>
        </div>  
    </div>
    <div class="row"><%-- Botones --%>
        <div class="col-md-12"><%-- Botones --%>
            <div class="btn-group" role="group">
                <asp:Button ID="btnRegistrar" runat="server" Text="Registro de admisión" Visible="False" CssClass="btn btn-default" OnClick="btnRegistrar_Click"></asp:Button>
                <asp:Button ID="btnModificar"  CssClass="btn btn-default"  runat="server" Text="Modificar datos" OnClick="btnModificar_Click"></asp:Button>
                <asp:Button ID="btnRegresar" runat="server" Text="Ir a página de inicio" CssClass="btn btn-default" OnClick="btnRegresar_Click"></asp:Button>
            </div>
        </div>
    </div>
  </div>
</div>
   
    <div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Listado de episodios</h3>
  </div>
  <div class="table-panel-body">
      <asp:DataGrid ID="dgEpisodios" runat="server" DataSource="<%# dvwEpisodios %>"  Width="100%" CssClass="table table-condensed table-responsive table-hover"  AutoGenerateColumns="False" CellPadding="2" GridLines="None" AllowSorting="True" ForeColor="Black">
        <HeaderStyle BackColor="LightGray"/>
        <Columns>
            <asp:HyperLinkColumn DataNavigateUrlField="PK_Episodio" DataNavigateUrlFormatString="../Episodios/frmVisualizar.aspx?pk_episodio={0}" DataTextField="PK_Episodio" SortExpression="PK_Episodio" HeaderText="Episodio">
                <HeaderStyle Width="75px" />
            </asp:HyperLinkColumn>
            <asp:BoundColumn Visible="False" DataField="FK_Programa" SortExpression="FK_Programa" HeaderText="FK_Programa"/>
            <asp:BoundColumn DataField="NB_Programa" SortExpression="NB_Programa" HeaderText="Programa"/>
            <asp:BoundColumn DataField="FE_Episodio" SortExpression="FE_Episodio" HeaderText="Fecha de admisión" DataFormatString="{0:d}"/>
            <asp:BoundColumn DataField="DE_Metadona" SortExpression="DE_Metadona" HeaderText="Tx. opiáceos"/>
            <asp:BoundColumn DataField="DE_ES_Episodio" SortExpression="DE_ES_Episodio" HeaderText="Estado"/>
            <asp:BoundColumn DataField="NR_Perfiles" SortExpression="NR_Perfiles" HeaderText="Perfiles"/>
        </Columns>
    </asp:DataGrid>
    <asp:Label ID="lblMensaje" runat="server" ForeColor="MediumSeaGreen" Font-Bold="True">La persona ha sido registrada con éxito!</asp:Label> 
    <asp:Label ID="lblMsgGrid" runat="server" Font-Bold="True" ForeColor="MediumSeaGreen"></asp:Label>   
  </div>
</div>
</asp:Content>
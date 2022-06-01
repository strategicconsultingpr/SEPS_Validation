<%@ Page Language="c#" Inherits="ASSMCA.Episodios.frmVisualizar" CodeBehind="frmVisualizar.aspx.cs" MasterPageFile="~/Main.Master" %>
<asp:Content ID="mainVisualizarC" runat="server" ContentPlaceHolderID="mainBodyContent">
    <h1>Visualización del episodio</h1>


    <div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Datos del participante</h3>
  </div>
  <div class="panel-body">
    <div class="row">
        <div class="col-print-6 col-sm-6 col-lg-3"><%-- Primer apellido --%>
            <span class="SEPSLabel">Primer apellido:</span>
            <asp:Label ID="Label29" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].AP_Primero") %>'/>
        </div>
        <div class="col-print-6 col-sm-6 col-lg-3"><%-- Segundo apellido --%>
            <span class="SEPSLabel">Segundo apellido:</span>
            <asp:Label ID="Label27" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].AP_Segundo") %>'/>
        </div>
        <div class="col-print-6 col-sm-6 col-lg-3"><%-- Primer nombre  --%>
            <span class="SEPSLabel">Primer nombre:</span>
            <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].NB_Primero") %>'/>
        </div>
        <div class="col-print-6 col-sm-6 col-lg-3"><%-- Segundo nombre --%>
            <span class="SEPSLabel">Segundo nombre:</span>
            <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].NB_Segundo") %>'/>
        </div>
    </div>
    <div class="row">
        <div class="col-print-6 col-sm-6 col-lg-3"><%-- IUP --%>
            <span class="SEPSLabel" title='Identificador Único de Persona. Código único para cada persona registrada en el sistema. Toda persona tiene un número global diferente que lo identifica univocamente dentro del sistema, y lo diferencia de todos las otras personas registradas. Este código de obiene luego de registrar una persona por medio de la funcionalidad "Registrar" del menú "Persona".'>IUP:</span>
            <asp:Label ID="lblIUP" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].PK_Persona") %>'/>
        </div>
        <div class="col-print-6 col-sm-6 col-lg-3"><%-- Expediente --%>
            <span class="SEPSLabel" title="Número entero que identifica una persona para un programa particular. Toda persona debe puede tener mas de un número de expediente diferente dado que haya participado en más de un programa.">Expediente:</span>
            <asp:Label ID="Label23" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].NR_Expediente") %>'/>
        </div>
        <div class="col-print-6 col-sm-6 col-lg-3"><%-- Seguro Social --%>
            <span class="SEPSLabel">Seguro social:</span>
            <asp:Label ID="Label21" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].NR_SeguroSocial") %>'/>
        </div>
        <div class="col-print-6 col-sm-6 col-lg-3"><%-- Fecha de Nacimiento --%>
            <span class="SEPSLabel">Fecha de nacimiento:</span>
            <asp:Label ID="Label37" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].FE_Nacimiento", "{0:d}") %>'/>
        </div>
        <div class="col-print-6 col-sm-6 col-lg-3"><%-- Edad --%>
            <span class="SEPSLabel">Edad:</span>
            <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].NR_Edad") %>'/>
        </div>
        <div class="col-print-6 col-sm-6 col-lg-3"><%-- Sexo --%>
            <span class="SEPSLabel">Sexo:</span>
            <asp:Label ID="Label19" runat="server" Text='<%# DataBinder.Eval(dsPersona, "Tables[SA_PERSONA].DefaultView.[0].DE_Sexo") %>'/>
        </div>
    </div>
  </div>
</div>

    <div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Datos básicos del episodio</h3>
  </div>
  <div class="panel-body">
    <div class="row">
        <div class="col-md-6"><%-- Nombre del centro (Unidad de servicio) --%>
            <span class="SEPSLabel">Nombre del centro / Unidad de servicio:</span>
            <asp:Label ID="lblCentro" runat="server" Text='<%# DataBinder.Eval(dvwEpisodio, "[0].NB_Programa") %>'/>
        </div>
        <div class="col-print-6 col-sm-6 col-md-3"><%-- Numero del episodio --%>
            <span class="SEPSLabel">Número del episodio:</span>
            <asp:Label ID="lblEpisodio" runat="server" Text='<%# DataBinder.Eval(dvwEpisodio, "[0].PK_Episodio") %>'/>
        </div>
        <div class="col-print-6 col-sm-6 col-md-3"><%-- Estado del Episodio --%>
            <span class="SEPSLabel">Estado del episodio:</span>
            <asp:Label ID="lblEstado" runat="server" Text='<%# DataBinder.Eval(dvwEpisodio, "[0].DE_ES_Episodio") %>'/>
        </div>
        <div class="col-xs-12">
            <div class="btn-group" role="group">
                <asp:Button ID="btnEvaluacion" runat="server" Text="Registrar evaluación" CssClass="btn btn-default" OnClick="btnEvaluacion_Click"/>
                <asp:Button ID="btnAlta" runat="server" Text="Registrar alta" CssClass="btn btn-default" OnClick="btnAlta_Click"/>
            </div>
        </div>
    </div>
  </div>
</div>

    <div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Listado de perfiles</h3>
  </div>
  <div class="table-panel-body">
    <asp:DataGrid ID="dgEpisodios" runat="server" DataSource="<%# dsPersona %>" CssClass="table table-condensed table-responsive table-hover" AutoGenerateColumns="False"  GridLines="None" AllowSorting="True" DataMember="SA_PERFILES" ForeColor="Black">
        <HeaderStyle BackColor="LightGray"/>
        <Columns>
            <asp:HyperLinkColumn DataNavigateUrlField="URL" DataTextField="PK_NR_Perfil" HeaderText="Perfil" DataNavigateUrlFormatString="{0}">
                <HeaderStyle Width="100px"/>
            </asp:HyperLinkColumn>
            <asp:BoundColumn DataField="FE_Perfil" SortExpression="FE_Perfil" HeaderText="Fecha" DataFormatString="{0:d}"/>
            <asp:BoundColumn DataField="TI_Perfil" SortExpression="TI_Perfil" HeaderText="Tipo de perfil"/>
        </Columns>
    </asp:DataGrid>

  </div>
</div>
    
</asp:Content>
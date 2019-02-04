<%@ Control Language="c#" Inherits="ASSMCA.Perfiles.wucOtrosDatosPerfil" CodeBehind="wucOtrosDatosPerfil.ascx.cs" %>
<div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Otros datos</h3>
     </div>
  <div class="panel-body">
    <div class="row">
    <div class="col-print-6 col-md-6 SEPSDivsInfo"><%--N�mero de Episodio--%>
        <span class="SEPSLabel">N�mero del episodio:</span>
        <asp:Label ID="lblEpisodio" runat="server" Text='<%# DataBinder.Eval(dsPerfil, "Tables[SA_EPISODIO].DefaultView.[0].PK_Episodio") %>'/>
    </div>
    <div class="col-print-6 col-md-6 SEPSDivsInfo"><%--N�mero de Perfil--%>
        <span class="SEPSLabel">N�mero del perfil:</span>
        <asp:Label ID="lblPerfil" runat="server" Text='<%# DataBinder.Eval(dsPerfil, "Tables[SA_PERFIL].DefaultView.[0].PK_NR_Perfil") %>'/>
        <asp:Label ID="lblPerfilNuevo" runat="server" Visible="False">(SIN N�MERO ASIGNADO)</asp:Label>
        <asp:HiddenField ID="tipoDePerfilHidden" runat="server" />
    </div>
    <div class="col-md-6 SEPSDivsInfo"><%--Administraci�n Auxiliar--%>
        <span class="SEPSLabel">Administraci�n auxiliar de:</span>
        <asp:Label ID="lblAdministracion" runat="server" Text='<%# DataBinder.Eval(dsPerfil, "Tables[SA_EPISODIO].DefaultView.[0].NB_Administracion") %>'/>
    </div>
    <div class="col-md-6 SEPSDivsInfo"><%--Nombre del centro / Unidad de servicio--%>
        <span class="SEPSLabel">Nombre del centro / Unidad de servicio:</span>
        <asp:Label ID="lblCentro" runat="server" Text='<%# DataBinder.Eval(dsPerfil, "Tables[SA_EPISODIO].DefaultView.[0].NB_Programa") %>'/>
    </div>
</div>

<div class="row">
    <div class="col-lg-6 SEPSDivs"><%--Fecha de Perfil--%>
        <span class="SEPSLabel" id="fechaPerfil">Fecha del perfil:</span>
        <div class="leftFloat">
            <asp:DropDownList CssClass="form-control" ID="ddlMes" runat="server" onChange="ddlMesNuevo('WucOtrosDatosPerfil_','ddlD�a','ddlMes'); LastContactPrevioAPerfil(); FechaAlta('WucOtrosDatosPerfil_ddlMes', 'WucOtrosDatosPerfil_ddlD�a', 'WucOtrosDatosPerfil_txtA�o', 'WucOtrosDatosPerfil_FechaAdmisionHidden', 'WucOtrosDatosPerfil_FechaAltaHidden','WucDatosPersonales_hiddenFechaUltimaEvaluacion'); IsFutureDate('WucOtrosDatosPerfil_','perfil' )">
                <asp:ListItem Value="1">Enero</asp:ListItem>
                <asp:ListItem Value="2">Febrero</asp:ListItem>
                <asp:ListItem Value="3">Marzo</asp:ListItem>
                <asp:ListItem Value="4">Abril</asp:ListItem>
                <asp:ListItem Value="5">Mayo</asp:ListItem>
                <asp:ListItem Value="6">Junio</asp:ListItem>
                <asp:ListItem Value="7">Julio</asp:ListItem>
                <asp:ListItem Value="8">Agosto</asp:ListItem>
                <asp:ListItem Value="9">Septiembre</asp:ListItem>
                <asp:ListItem Value="10">Octubre</asp:ListItem>
                <asp:ListItem Value="11">Noviembre</asp:ListItem>
                <asp:ListItem Value="12">Diciembre</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="leftFloat">
            <asp:DropDownList  CssClass="form-control" ID="ddlD�a" runat="server" onChange="ddlD�aNuevo('WucOtrosDatosPerfil_','ddlMes','ddlD�a'); LastContactPrevioAPerfil();  FechaAlta('WucOtrosDatosPerfil_ddlMes', 'WucOtrosDatosPerfil_ddlD�a', 'WucOtrosDatosPerfil_txtA�o', 'WucOtrosDatosPerfil_FechaAdmisionHidden', 'WucOtrosDatosPerfil_FechaAltaHidden','WucDatosPersonales_hiddenFechaUltimaEvaluacion'); IsFutureDate('WucOtrosDatosPerfil_','perfil' );">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
                <asp:ListItem Value="5">5</asp:ListItem>
                <asp:ListItem Value="6">6</asp:ListItem>
                <asp:ListItem Value="7">7</asp:ListItem>
                <asp:ListItem Value="8">8</asp:ListItem>
                <asp:ListItem Value="9">9</asp:ListItem>
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="11">11</asp:ListItem>
                <asp:ListItem Value="12">12</asp:ListItem>
                <asp:ListItem Value="13">13</asp:ListItem>
                <asp:ListItem Value="14">14</asp:ListItem>
                <asp:ListItem Value="15">15</asp:ListItem>
                <asp:ListItem Value="16">16</asp:ListItem>
                <asp:ListItem Value="17">17</asp:ListItem>
                <asp:ListItem Value="18">18</asp:ListItem>
                <asp:ListItem Value="19">19</asp:ListItem>
                <asp:ListItem Value="20">20</asp:ListItem>
                <asp:ListItem Value="21">21</asp:ListItem>
                <asp:ListItem Value="22">22</asp:ListItem>
                <asp:ListItem Value="23">23</asp:ListItem> 
                <asp:ListItem Value="24">24</asp:ListItem>
                <asp:ListItem Value="25">25</asp:ListItem>
                <asp:ListItem Value="26">26</asp:ListItem>
                <asp:ListItem Value="27">27</asp:ListItem>
                <asp:ListItem Value="28">28</asp:ListItem>
                <asp:ListItem Value="29">29</asp:ListItem>
                <asp:ListItem Value="30">30</asp:ListItem>
                <asp:ListItem Value="31">31</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="leftFloat">
            <asp:TextBox  CssClass="form-control" ID="txtA�o" runat="server" autocomplete="off" Width="80px" MaxLength="4" onBlur=" LastContactPrevioAPerfil();  FechaAlta('WucOtrosDatosPerfil_ddlMes', 'WucOtrosDatosPerfil_ddlD�a', 'WucOtrosDatosPerfil_txtA�o', 'WucOtrosDatosPerfil_FechaAdmisionHidden', 'WucOtrosDatosPerfil_FechaAltaHidden','WucDatosPersonales_hiddenFechaUltimaEvaluacion');  IsFutureDate('WucOtrosDatosPerfil_','perfil' )" AutoPostBack="True"/>
            <asp:Label ID="lblFechaPerfil" runat="server" Text='<%# DataBinder.Eval(dsPerfil, "Tables[SA_PERFIL].DefaultView.[0].FE_Perfil", "{0:d}") %>'/>
            <input id="FechaAdmisionHidden" type="hidden" runat="server"/>
            <input id="FechaAltaHidden" type="hidden" runat="server"/>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="leftFloatAsterisk" runat="server" ErrorMessage="Fecha de Admisi�n" ControlToValidate="txtA�o" Display="Dynamic" Text="*"/>
        <asp:RangeValidator ID="rvA�o" runat="server" CssClass="leftFloatAsterisk" ErrorMessage="Fecha Admisi�n" ControlToValidate="txtA�o" Display="Dynamic" Type="Integer" MaximumValue="0" MinimumValue="0" Text="*"/>
    </div>
    <div class="col-lg-6 SEPSDivs"><%--Fecha Ult. Contacto: --%>
        <span class="SEPSLabel">Fecha de �ltimo contacto:</span>
        <div class="leftFloat">
            <asp:DropDownList CssClass="form-control" ID="ddlMesContacto" runat="server" onChange=" LastContactPrevioAPerfil();  ddlMesNuevo('WucOtrosDatosPerfil_','ddlD�aContacto','ddlMesContacto');  IsFutureDate('WucOtrosDatosPerfil_','ultimo contacto','Contacto' )">
                <asp:ListItem Value="1">Enero</asp:ListItem>
                <asp:ListItem Value="2">Febrero</asp:ListItem>
                <asp:ListItem Value="3">Marzo</asp:ListItem>
                <asp:ListItem Value="4">Abril</asp:ListItem>
                <asp:ListItem Value="5">Mayo</asp:ListItem>
                <asp:ListItem Value="6">Junio</asp:ListItem>
                <asp:ListItem Value="7">Julio</asp:ListItem>
                <asp:ListItem Value="8">Agosto</asp:ListItem>
                <asp:ListItem Value="9">Septiembre</asp:ListItem>
                <asp:ListItem Value="10">Octubre</asp:ListItem>
                <asp:ListItem Value="11">Noviembre</asp:ListItem>
                <asp:ListItem Value="12">Diciembre</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="leftFloat">
            <asp:DropDownList  CssClass="form-control" ID="ddlD�aContacto" runat="server" onChange=" LastContactPrevioAPerfil();  ddlD�aNuevo('WucOtrosDatosPerfil_','ddlMesContacto','ddlD�aContacto');  IsFutureDate('WucOtrosDatosPerfil_','ultimo contacto','Contacto' )">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
                <asp:ListItem Value="5">5</asp:ListItem>
                <asp:ListItem Value="6">6</asp:ListItem>
                <asp:ListItem Value="7">7</asp:ListItem>
                <asp:ListItem Value="8">8</asp:ListItem>
                <asp:ListItem Value="9">9</asp:ListItem>
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="11">11</asp:ListItem>
                <asp:ListItem Value="12">12</asp:ListItem>
                <asp:ListItem Value="13">13</asp:ListItem>
                <asp:ListItem Value="14">14</asp:ListItem>
                <asp:ListItem Value="15">15</asp:ListItem>
                <asp:ListItem Value="16">16</asp:ListItem>
                <asp:ListItem Value="17">17</asp:ListItem>
                <asp:ListItem Value="18">18</asp:ListItem>
                <asp:ListItem Value="19">19</asp:ListItem>
                <asp:ListItem Value="20">20</asp:ListItem>
                <asp:ListItem Value="21">21</asp:ListItem>
                <asp:ListItem Value="22">22</asp:ListItem>
                <asp:ListItem Value="23">23</asp:ListItem>
                <asp:ListItem Value="24">24</asp:ListItem>
                <asp:ListItem Value="25">25</asp:ListItem>
                <asp:ListItem Value="26">26</asp:ListItem>
                <asp:ListItem Value="27">27</asp:ListItem>
                <asp:ListItem Value="28">28</asp:ListItem>
                <asp:ListItem Value="29">29</asp:ListItem>
                <asp:ListItem Value="30">30</asp:ListItem>
                <asp:ListItem Value="31">31</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="leftFloat">
            <asp:TextBox CssClass="form-control" onBlur=" LastContactPrevioAPerfil();  IsFutureDate('WucOtrosDatosPerfil_','ultimo contacto','Contacto' )" ID="txtA�oContacto" runat="server" MaxLength="4" Width="80px"/>
            <asp:Label ID="lblFechaContacto" runat="server" Text='<%# DataBinder.Eval(dsPerfil, "Tables[SA_PERFIL].DefaultView.[0].FE_Contacto", "{0:d}") %>'/>
        </div>
        <asp:RangeValidator ID="rvA�oContacto" CssClass="leftFloatAsterisk"  runat="server" ControlToValidate="txtA�oContacto" Display="Dynamic" ErrorMessage="Fecha Admisi�n" MaximumValue="0" MinimumValue="0" Type="Integer" Text="*"/>
    </div>
</div>
  </div>
</div>

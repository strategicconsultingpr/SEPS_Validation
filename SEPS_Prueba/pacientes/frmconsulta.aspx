<%@ Page Language="c#" Inherits="ASSMCA.Pacientes.frmConsulta" CodeBehind="frmConsulta.aspx.cs" MasterPageFile="~/Main.Master" %>
<asp:Content ID="mainC" runat="server" ContentPlaceHolderID="mainBodyContent">
    <asp:Panel ID="busquedaPanel" runat="server" DefaultButton="btnConsultar" >
    <h1><asp:Literal ID="Label4" runat="server">Búsqueda de pacientes</asp:Literal></h1>
<input id="TipoBusqueda" runat="server" type="hidden"/>

        
<div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Filtros de búsqueda</h3>
  </div>
  <div class="panel-body">
                <div class="row">
                <div class="col-sm-6 col-sm-offset-6 col-lg-offset-0 col-lg-2 SEPSDivs"><%--IUP--%>
                    <span class="SEPSLabelConsulta" title='Identificador Único de Persona. Código único para cada persona registrada en el sistema. Toda persona tiene un número global diferente que lo identifica univocamente dentro del sistema, y lo diferencia de todos las otras personas registradas. Este código de obiene luego de registrar una persona por medio de la funcionalidad "Registrar" del menú "Persona".'>IUP:</span>
                    <asp:RangeValidator ID="rvIUP" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic" Type="Double" ErrorMessage="El IUP tiene que ser un número entero." ControlToValidate="txtIUP" MinimumValue="0" MaximumValue="999999999999" Text="*"/>
                    <div class="expandibleDiv">
                        <asp:TextBox ID="txtIUP" runat="server"  CssClass="form-control" />
                    </div>

                </div>
                <div class="col-sm-6  col-lg-5 SEPSDivs"><%--Expediente--%>
                    <span class="SEPSLabelConsulta" title="Número entero que identifica una persona para un programa particular. Toda persona debe puede tener mas de un número de expediente diferente dado que haya participado en más de un programa.">Expediente:</span>
                    <div class="consultaDivDdlContiene">
                        <asp:DropDownList ID="ddlExpediente" CssClass="form-control" runat="server"  >
                            <asp:ListItem Value="Contiene">Contiene</asp:ListItem>
                            <asp:ListItem Value="Es igual a">Es igual a</asp:ListItem>
                            <asp:ListItem Value="Inicia con">Inicia con</asp:ListItem>
                            <asp:ListItem Value="Finaliza en">Finaliza en</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RangeValidator ID="RangeValidator1" CssClass="rightFloatAsterisk" Type="Double" runat="server" Display="Dynamic" ErrorMessage="El expediente tiene que ser un número entero entre 1 y 999.999.999" ControlToValidate="txtExpediente" MinimumValue="0" MaximumValue="999999999999" Text="*"/>
                    <div class="expandibleDiv">
                         <asp:TextBox ID="txtExpediente" CssClass="form-control" runat="server" ToolTip="Escriba un número de expediente válido para el programa en que esta usted registrado. Este atributo es requerido para crear un nuevo registro de persona." MaxLength="9" />
                    </div>

                </div>
                <div class="col-sm-6  col-lg-5 SEPSDivs"><%--Seguro Social--%>
                    <span class="SEPSLabelConsulta">Seguro social:</span>
                    <div class="consultaDivDdlContiene">
                    <asp:DropDownList ID="ddlSeguroSocial" runat="server" CssClass="form-control"    >
                        <asp:ListItem Value="Contiene">Contiene</asp:ListItem>
                        <asp:ListItem Value="Es igual a">Es igual a</asp:ListItem>
                        <asp:ListItem Value="Inicia con">Inicia con</asp:ListItem>
                        <asp:ListItem Value="Finaliza en">Finaliza en</asp:ListItem>
                    </asp:DropDownList>
                        </div>
                    <div class="expandibleDiv">
                        <asp:TextBox ID="txtNSS" runat="server" CssClass="form-control"/>
                    </div>
                </div>
                <div class="col-sm-6 SEPSDivs"><%--Primer apellido--%>
                    <span class="SEPSLabelConsulta">Primer apellido:</span>
                    <div class="consultaDivDdlContiene">
                    <asp:DropDownList ID="ddlPrimerApellido" CssClass="form-control"  runat="server"   >
                        <asp:ListItem Value="Contiene">Contiene</asp:ListItem>
                        <asp:ListItem Value="Es igual a">Es igual a</asp:ListItem>
                        <asp:ListItem Value="Inicia con">Inicia con</asp:ListItem>
                        <asp:ListItem Value="Finaliza en">Finaliza en</asp:ListItem>
                    </asp:DropDownList>
                        </div>
                    <div class="expandibleDiv">
                        <asp:TextBox ID="txtPrimerApellido" CssClass="form-control"  runat="server"     MaxLength="30"/>
                    </div>
                </div>
                <div class="col-sm-6 SEPSDivs"><%--Segundo apellido--%>
                    <span class="SEPSLabelConsulta">Segundo apellido:</span>
                    <div class="consultaDivDdlContiene">
                    <asp:DropDownList ID="ddlSegundoApellido"  CssClass="form-control" runat="server"   >
                        <asp:ListItem Value="Contiene">Contiene</asp:ListItem>
                        <asp:ListItem Value="Es igual a">Es igual a</asp:ListItem>
                        <asp:ListItem Value="Inicia con">Inicia con</asp:ListItem>
                        <asp:ListItem Value="Finaliza en">Finaliza en</asp:ListItem>
                    </asp:DropDownList>
                        </div>
                    <div class="expandibleDiv">
                        <asp:TextBox ID="txtSegundoApellido" CssClass="form-control"  runat="server"     MaxLength="30"/>
                    </div>
                </div>
                <div class="col-sm-6 SEPSDivs"><%--Primer nombre--%>
                    <span class="SEPSLabelConsulta">Primer nombre:</span>
                    <div class="consultaDivDdlContiene">
                    <asp:DropDownList ID="ddlPrimerNombre" CssClass="form-control"  runat="server"   >
                        <asp:ListItem Value="Contiene">Contiene</asp:ListItem>
                        <asp:ListItem Value="Es igual a">Es igual a</asp:ListItem>
                        <asp:ListItem Value="Inicia con">Inicia con</asp:ListItem>
                        <asp:ListItem Value="Finaliza en">Finaliza en</asp:ListItem>
                    </asp:DropDownList>
                        </div>
                    <div class="expandibleDiv">
                        <asp:TextBox ID="txtPrimerNombre" CssClass="form-control"  runat="server"     MaxLength="30"/>
                    </div>
                </div>
                <div class="col-sm-6 SEPSDivs"><%--Segundo nombre--%>
                    <span class="SEPSLabelConsulta">Segundo nombre:</span>
                    <div class="consultaDivDdlContiene">
                    <asp:DropDownList ID="ddlSegundoNombre" CssClass="form-control"  runat="server"   >
                        <asp:ListItem Value="Contiene">Contiene</asp:ListItem>
                        <asp:ListItem Value="Es igual a">Es igual a</asp:ListItem>
                        <asp:ListItem Value="Inicia con">Inicia con</asp:ListItem>
                        <asp:ListItem Value="Finaliza en">Finaliza en</asp:ListItem>
                    </asp:DropDownList>
                        </div>
                    <div class="expandibleDiv">
                        <asp:TextBox ID="txtSegundoNombre" CssClass="form-control"  runat="server" MaxLength="30"/>
                    </div>
                </div>
                <div class="col-sm-6 SEPSDivs"><%--Edad--%>
                    <span class="SEPSLabelConsulta">Edad:</span>
                    <div class="consultaDivDdlEdad">
                        <asp:DropDownList ID="ddlEdad" CssClass="form-control" runat="server">
                            <asp:ListItem Value="Es igual a">Es igual a</asp:ListItem>
                            <asp:ListItem Value="Es mayor que">Es mayor que</asp:ListItem>
                            <asp:ListItem Value="Es menor que">Es menor que</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RangeValidator ID="rvEdad" CssClass="rightFloatAsterisk" runat="server" ControlToValidate="txtEdad" Display="Dynamic" Type="Integer" MinimumValue="0" MaximumValue="100" Text="*" />
                    <div class="expandibleDiv">
                        <asp:TextBox ID="txtEdad" runat="server" CssClass="form-control" MaxLength="2"/>
                    </div>
                </div>
                <div class="col-sm-6 SEPSDivs"><%--Sexo--%>
                    <span class="SEPSLabelConsulta">Sexo:</span>
                    <div class="expandibleDiv">
                        <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control" DataMember="LKP_Sexo" DataSource="<%# dsPersona %>" DataTextField="DE_Sexo" DataValueField="PK_Sexo"/>
                    </div>
                </div>  
                <div class="col-sm-6 SEPSDivs"><%--Veterano--%>
                    <span class="SEPSLabelConsulta">Veterano:</span>
                    <div class="expandibleDiv">
                        <asp:DropDownList ID="ddlVeterano" runat="server" CssClass="form-control" DataMember="LKP_Veterano" DataSource="<%# dsPersona %>" DataTextField="DE_Veterano" DataValueField="PK_Veterano"/>
                    </div>
                </div>
                <div class="col-sm-6 SEPSDivs"><%--Grupo Etnico--%>
                    <span class="SEPSLabelConsulta">Grupo étnico:</span>
                    <div class="expandibleDiv">
                        <asp:DropDownList ID="ddlGrupoEtnico" runat="server" CssClass="form-control" DataMember="LKP_GrupoEtnico" DataSource="<%# dsPersona %>" DataTextField="DE_GrupoEtnico" DataValueField="PK_GrupoEtnico"/>
                    </div>
                </div>
                <div class="col-sm-6 SEPSDivs"><%--Programa--%>
                    <span class="SEPSLabelConsulta" title="Seleccionar de esta opción para solo encontrar pacientes que ya tienen episodios en el programa.">Programa:</span>
                    <div class="expandibleDiv">
                        <asp:DropDownList ID="ddlPrograma" runat="server" CssClass="form-control" DataTextField="NB_Programa" DataValueField="PK_Programa" DataSource="<%# dsSeguridad %>"  DataMember="SA_USUARIO"/>
                    </div>
                </div>
                <div class="col-sm-6 SEPSDivs"><%--Status episodio--%>
                    <span class="SEPSLabelConsulta">Estado de episodios:</span>
                    <div class="expandibleDiv">
                        <asp:DropDownList ID="ddlEstadoEpisodios" runat="server" CssClass="form-control">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="1">Abiertos</asp:ListItem>
                            <asp:ListItem Value="2">Cerrados</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
      <div class="row">
          <div class="col-sm-12 SEPSDivs">
            <span class="SEPSLabel">Filtro de fecha:</span>
                    <div class="expandibleDiv">
                        <asp:DropDownList ID="ddlFiltroDeFecha" runat="server" CssClass="form-control" onChange="ddlFiltroDeFecha()">
                            <asp:ListItem Value="1">Fechas exactas</asp:ListItem>
                            <asp:ListItem Value="2">Rangos de fechas</asp:ListItem>
                        </asp:DropDownList>
                    </div>          
          </div>
          </div>
      <div class ="row" id="divFechasExactas">
                <div class="col-sm-6 SEPSDivs">  <%-- Fecha Nacimiento --%>
                    <span class="SEPSLabel">Tipo de fecha:</span>
                    <div class="expandibleDiv">
                        <asp:DropDownList ID="ddlFechasExactas" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1">Admisión</asp:ListItem>
                            <asp:ListItem Value="2">Evaluación</asp:ListItem>
                            <asp:ListItem Value="3">Alta</asp:ListItem>
                            <asp:ListItem Value="4">Edición</asp:ListItem>
                            <asp:ListItem Value="5">Nacimiento</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    </div>
          <div class="col-sm-6 SEPSDivs">
                                  <span class="SEPSLabel">Fecha:</span>

                    <div class="leftFloat">
                        <asp:DropDownList ID="ddlMesExacta" runat="server" Width="120px" CssClass="form-control" onChange="ddlMesNuevo('','Exacta'); IsFutureDate('','exacta','Exacta' )">
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
                        <asp:DropDownList ID="ddlDíaExacta" runat="server" Width="65px" CssClass="form-control" onChange="ddlDíaNuevo('','Exacta'); IsFutureDate('','exacta','Exacta' )">
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
                        <asp:TextBox ID="txtAñoExacta" runat="server" CssClass="form-control" onblur="IsFutureDate('','exacta','Exacta' )" Width="60px" MaxLength="4"/>
                    </div>
                    <asp:RangeValidator ID="rvAñoExacta" runat="server" CssClass="leftFloatAsterisk" ControlToValidate="txtAñoExacta" MinimumValue="1800" MaximumValue="2500" Type="Integer" Display="Dynamic" Text="*"/>
              </div>
          </div>

      <div id="divRangosDeFechas" style="display:none;">
      <div class="row">
          <div class="col-sm-6 SEPSDivs">
              <span class="SEPSLabel">Tipo de fecha:</span>
              <div class="expandibleDiv">
                        <asp:DropDownList ID="ddlRangoTipoFecha" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1">Admisión</asp:ListItem>
                            <asp:ListItem Value="2">Evaluación</asp:ListItem>
                            <asp:ListItem Value="3">Alta</asp:ListItem>
                            <asp:ListItem Value="4">Edición</asp:ListItem>
                            <asp:ListItem Value="5">Nacimiento</asp:ListItem>
                        </asp:DropDownList>
                    </div>
          </div>    
          </div>
      <div class="row">
           <div class="col-sm-6 SEPSDivs">  <%-- Rangos de fechas --%>  
                    <span class="SEPSLabel">Inicio:</span>
                    <div class="leftFloat">
                        <asp:DropDownList ID="ddlMesRangoInicio" runat="server" Width="120px" CssClass="form-control" onChange="ddlMesNuevo('','RangoInicio'); IsFutureDate('','inicio','RangoInicio' )">
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
                        <asp:DropDownList ID="ddlDíaRangoInicio" runat="server" Width="65px" CssClass="form-control" onChange="ddlDíaNuevo('','RangoInicio'); IsFutureDate('','inicio','RangoInicio' )">
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
                        <asp:TextBox ID="txtAñoRangoInicio" runat="server" CssClass="form-control" onblur="IsFutureDate('','inicio','RangoInicio' )" Width="60px" MaxLength="4"/>
                    </div>
                    <asp:RangeValidator ID="rvRangoInicio" runat="server" CssClass="leftFloatAsterisk" ControlToValidate="txtAñoRangoInicio" MinimumValue="1800" MaximumValue="2500" Type="Integer" Display="Dynamic" Text="*"/>
               </div>
          <div class="col-sm-6 SEPSDivs"> 
                    <span class="SEPSLabel">Fin:</span>
                    <div class="leftFloat">
                        <asp:DropDownList ID="ddlMesRangoFin" runat="server" Width="120px" CssClass="form-control" onChange="ddlMesNuevo('','RangoFin');">
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
                        <asp:DropDownList ID="ddlDíaRangoFin" runat="server" Width="65px" CssClass="form-control" onChange="ddlDíaNuevo('','RangoFin');">
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
                        <asp:TextBox ID="txtAñoRangoFin" runat="server" CssClass="form-control" Width="60px" MaxLength="4"/>
                    </div>
                    <asp:RangeValidator ID="rvRangoFin" runat="server" CssClass="leftFloatAsterisk" ControlToValidate="txtAñoRangoFin" MinimumValue="1800" MaximumValue="2500" Type="Integer" Display="Dynamic" Text="*"/>
                </div>
            </div>
      </div>
      <div class="row">
          <div class="col-sm-6 SEPSDivs">
              <span class="SEPSLabel" title='Seleccione si desea realizar esta búsqueda referente este programa ó bajo todo el sistema SEPS.'>Tipo de búsqueda:</span>
              <div class="expandibleDiv">
                        <asp:DropDownList ID="ddlTipoBusqueda" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1">Bajo todo SEPS</asp:ListItem>
                            <asp:ListItem Value="2">Bajo este Programa</asp:ListItem>
                        </asp:DropDownList>
                    </div>
          </div>    
          </div>

            <div class="row">
                <div class="col-xs-12 SEPSDivs"><%--Botones--%>
                    <div class="btn-group" role="group">
                        <asp:Button ID="btnRegistrar" CssClass="btn btn-default" runat="server" Text="Registrar paciente" Visible="False" OnClientClick="tipoBusqueda();" OnClick="btnRegistrar_Click"/>
                        <asp:Button ID="btnBorrar" runat="server" Text="Borrar campos" CssClass="btn btn-default" OnClick="btnBorrar_Click"/>
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="btn btn-default" OnClick="btnConsultar_Click"/>
                    </div>
                </div>
            </div>
                   </div>
          </div>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Se han encontrado algunos errores en el formulario que debe revisar antes de realizar la búsqueda:" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>


        
<div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Resultados de la búsqueda</h3>
  </div>
  <div class="panel-body" style="padding:0px">
    <asp:Label ID="lblMensaje" runat="server" Style="padding:30px 30px;"  Font-Bold="True" ForeColor="MediumSeaGreen">No existen registros que desplegar, debe realizar una consulta.</asp:Label>
    <asp:DataGrid ID="dgPersonas" runat="server" CssClass="table table-condensed table-responsive table-hover" AutoGenerateColumns="False" CellPadding="2" GridLines="None" ForeColor="Black" DataMember="SA_PERSONAS" DataSource="<%# dsPersona %>" DataKeyField="PK_Persona">
        <HeaderStyle BackColor="LightGray"/>
        <Columns>
            <asp:HyperLinkColumn DataNavigateUrlField="PK_Persona" DataNavigateUrlFormatString="frmVisualizar.aspx?accion=consultar&amp;pk_persona={0}" DataTextField="PK_Persona" HeaderText="IUP">
                <HeaderStyle Width="75px"/>
            </asp:HyperLinkColumn>
            <asp:BoundColumn DataField="NR_SeguroSocial" SortExpression="NR_SeguroSocial" HeaderText="Seguro social"/>
            <asp:BoundColumn DataField="Apellidos" SortExpression="Apellidos" HeaderText="Apellidos"/>
            <asp:BoundColumn DataField="Nombres" SortExpression="Nombres" HeaderText="Nombres"/>
            <asp:BoundColumn DataField="NR_Edad" SortExpression="NR_Edad" HeaderText="Edad"/>
            <asp:BoundColumn DataField="DE_Sexo" SortExpression="DE_Sexo" HeaderText="Sexo"/>
            <asp:BoundColumn DataField="TieneEpisodiosAbiertos" SortExpression="TieneEpisodiosAbiertos" HeaderText="Episodios abiertos"/>
        </Columns>
    </asp:DataGrid>
  </div>
</div>

     </asp:Panel>   
</asp:Content>
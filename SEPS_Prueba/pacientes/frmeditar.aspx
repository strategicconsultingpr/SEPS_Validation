<%@ Page Language="c#" Inherits="ASSMCA.Pacientes.frmEditar" CodeBehind="frmEditar.aspx.cs" MasterPageFile="~/MainUIF.Master" %>

<asp:Content ID="mainEditC" runat="server" ContentPlaceHolderID="mainBodyContent">
    <script type="text/javascript">
        function autoTab(field, limit, next, evt) {
            evt = (evt) ? evt : event;
            var charCode = (evt.charCode) ? evt.charCode : ((evt.keycode) ? evt.keycode : ((evt.which) ? evt.which : 0));
            if (field.value.length == limit) {
                field.form.elements[next].focus();
            }
        }
    </script>
<h1><asp:Literal ID="lTituloPrincipal" runat="server">Registro de pacientes</asp:Literal></h1>
<div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Datos básicos</h3>
  </div>
  <div class="panel-body">
    <div class="row">
        <div class="col-sm-2 SEPSDivs"> <%--IUP--%>
            <span class="SEPSLabel" title='Identificador Único de Persona. Código único para cada persona registrada en el sistema. Toda persona tiene un número global diferente que lo identifica univocamente dentro del sistema, y lo diferencia de todos las otras personas registradas. Este código de obiene luego de registrar una persona por medio de la funcionalidad "Registrar" del menú "Persona".'>IUP:</span>
            <asp:Label ID="lblIUP" runat="server" ForeColor="MediumSeaGreen" ToolTip="Usted esta creando una nueva persona, el sistema le otorgará uun identificador único una vez usted haya guardado el registro.">No registrado</asp:Label>
        </div>
        <div class="col-sm-4 SEPSDivs"> <%--Expediente--%>
            <span class="SEPSLabel" title="Número entero que identifica una persona para un programa particular. Toda persona debe puede tener mas de un número de expediente diferente dado que haya participado en más de un programa.">Expediente:</span>
            <asp:RequiredFieldValidator ID="valExpediente" CssClass="rightFloatAsterisk" runat="server" ErrorMessage="El Expediente en un campo requerido." ControlToValidate="txtExpediente" Display="Dynamic" Text="*"/>
            <asp:RangeValidator ID="valExpedienteRango" CssClass="rightFloatAsterisk" runat="server" ControlToValidate="txtExpediente" Type="Double" ErrorMessage="El expediente tiene que se un número entero entre 1 y 1,000,000,000" MaximumValue="1000000000" MinimumValue="1" Display="Dynamic" Text="*"/>
            <div class="expandibleDiv">
            <asp:TextBox ID="txtExpediente" runat="server" CssClass="form-control" MaxLength="12" ToolTip="Escriba un número de expediente válido para el programa en que esta usted registrado. Este atributo es requerido para crear un nuevo registro de persona." />
                </div>
        </div>
        <div class="col-sm-6 SEPSDivs"> <%--SS--%>
            <span class="SEPSLabel">Seguro social:</span>
            <div class="leftFloat">
                                            <asp:RegularExpressionValidator  ValidationExpression="^\d{3}$" CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ErrorMessage="Formato incorrecto del primer encasillado del NSS." ControlToValidate="txtNSS1" Text="*"/>
                <asp:TextBox ID="txtNSS1" runat="server" CssClass="form-control" Width="55px" onkeyup="autoTab(mainBodyContent_txtNSS1, 3, 'mainBodyContent_txtNSS2', event)" MaxLength="3"/>
            </div>
            <span class="SEPSLabel">-</span>
            <div class="leftFloat">
                <asp:RegularExpressionValidator  ValidationExpression="^\d{2}$" CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ErrorMessage="Formato incorrecto del segundo encasillado del NSS." ControlToValidate="txtNSS2" Text="*"/>

                <asp:TextBox ID="txtNSS2" runat="server" CssClass="form-control" Width="45px" onkeyup="autoTab(mainBodyContent_txtNSS2, 2, 'mainBodyContent_txtNSS3', event)" MaxLength="2"/>
            </div>
            <span class="SEPSLabel">-</span>
            <div class="leftFloat">
                            <asp:RequiredFieldValidator  CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ErrorMessage="Últimos 4 dígitos del número de seguro social son requeridos" ControlToValidate="txtNSS3" Text="*"/>
                            <asp:RegularExpressionValidator  ValidationExpression="^\d{4}$" CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ErrorMessage="Formato incorrecto del tercer encasillado del NSS." ControlToValidate="txtNSS3" Text="*"/>
                <asp:TextBox ID="txtNSS3" runat="server" CssClass="form-control" Width="65px" onkeyup="autoTab(mainBodyContent_txtNSS3, 4, 'mainBodyContent_txtPrimerApellido', event)" MaxLength="4"/>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-6 SEPSDivs">  <%-- Primer Apellido --%>
            <span class="SEPSLabel">Primer apellido:</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ErrorMessage="El Primer Apellido es un campo requerido." ControlToValidate="txtPrimerApellido" Text="*"/>
            <div class="expandibleDiv">
                <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="form-control" MaxLength="30"/>
            </div>
        </div>
        <div class="col-sm-6 SEPSDivs">  <%-- Segundo Apellido --%>
            <span class="SEPSLabel">Segundo apellido:</span>
            <div class="expandibleDiv">
                <asp:TextBox ID="txtSegundoApellido" runat="server" CssClass="form-control" MaxLength="30"/>
            </div>
        </div>
        <div class="col-sm-6 SEPSDivs">  <%-- Primer Nombre --%>
            <span class="SEPSLabel">Primer nombre:</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ErrorMessage="El Primer Nombre es un campo requerido." ControlToValidate="txtPrimerNombre" Text="*" />
            <div class="expandibleDiv">
                <asp:TextBox ID="txtPrimerNombre" runat="server"  CssClass="form-control" MaxLength="30"/>
            </div>
        </div>
        <div class="col-sm-6 SEPSDivs">  <%-- Segundo Nombre --%>
            <span class="SEPSLabel">Segundo nombre:</span>
            <div class="expandibleDiv">
                <asp:TextBox ID="txtSegundoNombre" runat="server" CssClass="form-control" MaxLength="30"/>
            </div>
        </div>
        <div class="col-sm-6 SEPSDivs">  <%-- Sexo --%>
            <span class="SEPSLabel">Sexo:</span>
            <div class="expandibleDiv">
                <asp:DropDownList ID="ddlSexo" runat="server"  CssClass="form-control" DataSource="<%# dsPersona %>" DataMember="LKP_Sexo" DataTextField="DE_Sexo" DataValueField="PK_Sexo" />
            </div>
        </div>
        <div class="col-sm-6 SEPSDivs"><%-- Veterano --%>
            <span class="SEPSLabel">Veterano:</span>
            <asp:RequiredFieldValidator ID="rfvVeterano" runat="server" CssClass="rightFloatAsterisk"  Display="Dynamic" InitialValue="0" ControlToValidate="ddlVeterano" ErrorMessage="Veterano es un campo requerido." ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
            <div class="expandibleDiv">
                <asp:DropDownList ID="ddlVeterano" runat="server" CssClass="form-control" DataSource="<%# dsPersona %>" DataMember="LKP_Veterano" DataTextField="DE_Veterano" DataValueField="PK_Veterano"/>
            </div>
        </div>
        </div>
    <div class="row">
        <div class="col-sm-5 col-lg-6 SEPSDivs"> <%-- Grupo Etnico --%>
            <span class="SEPSLabel">Grupo étnico:</span>
            <div class="expandibleDiv">
                <asp:DropDownList ID="ddlGrupoEtnico" runat="server" CssClass="form-control" DataSource="<%# dsPersona %>" DataMember="LKP_GrupoEtnico" DataTextField="DE_GrupoEtnico" DataValueField="PK_GrupoEtnico"/>
            </div>
        </div>
          <div class="col-sm-7 col-lg-6 SEPSDivs">  <%-- Fecha Nacimiento --%>
            <span class="SEPSLabel">Fecha nacimiento:</span>
            <div class="leftFloat">
                <asp:DropDownList ID="ddlMes" runat="server" Width="120px" CssClass="form-control" onChange="ddlMesNuevo(''); IsFutureDate('','nacimiento' )">
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
                <asp:DropDownList ID="ddlDía" runat="server" Width="65px" CssClass="form-control" onChange="ddlDíaNuevo(''); IsFutureDate('','nacimiento' )">
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
                <asp:TextBox ID="txtAño" runat="server" CssClass="form-control" onblur="IsFutureDate('','nacimiento' )" Width="60px" MaxLength="4"/>
            </div>
            <asp:RangeValidator ID="rvAñoNacimiento" runat="server" CssClass="leftFloatAsterisk" ControlToValidate="txtAño" Type="Integer" Display="Dynamic" Text="*"/>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="leftFloatAsterisk" runat="server" ControlToValidate="txtAño" ErrorMessage="El año de la fecha de Nacimiento es un campo requerido." Display="Dynamic" Text="*"/>
        </div>
    </div>
  </div>
</div>


    <div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Datos de razas</h3>
  </div>
  <div class="panel-body">
    <div style="height:160px"> <%-- Seleccion de razas --%>
        <div class="multipleLeft">
            <span class="SEPSLabel">Razas (Disponibles)</span>
            <asp:ListBox ID="lbxRazaSinSeleccionar" runat="server" DataValueField="PK_Raza" CssClass="form-control" DataTextField="DE_Raza" DataSource="<%# dvwRazasNoSeleccionadas %>" Height="130px" />
        </div>
        <div class="multipleCenter text-center"> <%--Botones --%>
            <div style="height:60px;"></div>
            <div class="btn-group center" role="group">
                <asp:Button ID="btnEliminar" runat="server" Text="<" CausesValidation="False" CssClass="btn btn-default" OnClick="btnEliminar_Click"/>
                <asp:Button ID="btnAgregar" runat="server" Text=">" CausesValidation="False" CssClass="btn btn-default" OnClick="btnAgregar_Click"/>
            </div>
        </div>
        <div class="multipleRight">
                    <div><span class="SEPSLabel">Razas (Seleccionadas)</span></div>
            <asp:ListBox ID="lbxRazaSeleccionadas" runat="server" Height="130px" CssClass="form-control" DataValueField="FK_Raza" DataTextField="DE_Raza" DataMember="SA_RAZA_PERSONA" DataSource="<%# dsPersona %>"/>
        </div>
    </div>
  </div>
</div>
    
    <div class="btn-group" role="group">
        <asp:Button ID="btnActualizarPersona" runat="server" Text="Actualizar datos" CssClass="btn btn-default" CausesValidation="true" OnClientClick="return validatePaciente();" OnClick="btnActualizarPersona_Click"/>
        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-default"  CausesValidation="true" OnClientClick="return validatePaciente();" OnClick="btnRegistrar_Click"/>
    </div>
    <asp:Label ID="lblMensaje" runat="server" ForeColor="MediumSeaGreen" Font-Bold="True"/>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" HeaderText="Se han encontrado algunos errores en el formulario que debe revisar antes de registrar la persona nueva:" ShowSummary="False" />
</asp:Content>

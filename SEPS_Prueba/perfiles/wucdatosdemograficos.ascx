<%@ Control Language="c#" Inherits="ASSMCA.Perfiles.wucDatosDemograficos" CodeBehind="wucDatosDemograficos.ascx.cs" %>
<input type="hidden" id="edadAdmision" runat="server" name="Hidden2"/>
<div class="panel panel-default">
<%-- <asp:Button ID="btnEdadAdmision" runat="server" CausesValidation="False"  style="display:none;" OnClick="edadAdmisionF"  />--%>
    <div class="panel-heading">
        <h3 class="panel-title">Datos demográficos</h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-print-6 col-md-6 SEPSDivs">
                <%-- Estado marital --%>
                <span class="SEPSLabel">Estado marital:</span>
                <asp:RequiredFieldValidator ID="rfvEstadoMarital" CssClass="rightFloatAsterisk" Display="Dynamic" runat="server" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Estado marital" ControlToValidate="ddlEstadoMarital" InitialValue="0" Text="*" />
                <div class="expandibleDiv">
                    <asp:DropDownList CssClass="form-control" ID="ddlEstadoMarital" runat="server" DataTextField="DE_EstadoMarital" DataValueField="PK_EstadoMarital" DataMember="SA_LKP_TEDS_ESTADO_MARITAL" DataSource="<%# dsPerfil %>" />
                    <asp:Label ID="lblEstadoMarital" runat="server" />
                </div>
            </div>
            <div class="col-print-6 col-md-6 SEPSDivs">
                <%-- Si es femina --%>
                <span class="SEPSLabel">Si es fémina:</span>
                <asp:RequiredFieldValidator ID="rfvFemina" CssClass="rightFloatAsterisk" Display="Dynamic" runat="server" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Si es fémina" ControlToValidate="ddlFemina" InitialValue="0" Text="*" />
                <div class="expandibleDiv">
                    <asp:DropDownList CssClass="form-control" ID="ddlFemina" runat="server" DataTextField="DE_Femina" DataValueField="PK_Femina" DataSource="<%# dvwFemina %>" OnLoad="ddlFemina_Load" />
                    <asp:Label ID="lblFemina" runat="server" />
                </div>
            </div>
            <div class="col-print-6 col-md-6 SEPSDivs">
                <%-- Si es varon --%>
                <span class="SEPSLabel">Si es varón:</span>
                <asp:RequiredFieldValidator ID="rfvVaron" CssClass="rightFloatAsterisk" Display="Dynamic" runat="server" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Si es varón" ControlToValidate="ddlVaron" Text="*" />
                <div class="expandibleDiv">
                    <asp:DropDownList CssClass="form-control" ID="ddlVaron" runat="server" onChange="ddlVaron()">
                        <asp:ListItem />
                        <asp:ListItem Value="1">Sin hijos</asp:ListItem>
                        <asp:ListItem Value="2">Con hijos</asp:ListItem>
                        <asp:ListItem Value="99">No aplica</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblVaron" runat="server" />
                </div>
            </div>
            <div class="col-print-6 col-md-6 SEPSDivs">
                <%-- Número de hijos --%>
                <span class="SEPSLabel">Número de hijos:</span>
                <asp:RequiredFieldValidator ID="rfvNumNinos" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic" ToolTip="Campo Requerido. Escriba un valor numerico." ErrorMessage="Número de hijos" ControlToValidate="txtNumNinos" Text="*" />
                <asp:RangeValidator ID="rvNumNinos" runat="server" CssClass="rightFloatAsterisk" ToolTip="Valor invalido." ErrorMessage="Número de hijos" ControlToValidate="txtNumNinos" Type="Integer" MaximumValue="50" MinimumValue="0" Display="Dynamic" Text="*" />
                <div class="expandibleDiv">
                    <asp:TextBox CssClass="form-control" ID="txtNumNinos" runat="server" MaxLength="2" />
                    <asp:Label ID="lblNumNinos" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-print-12 col-md-6 SEPSDivs">
                <%-- Condicion laboral --%>
                <span class="SEPSLabel">Condición laboral (US-SM-NOM):</span>
                <asp:RequiredFieldValidator ID="rfvCondLaboral" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Condición laboral" ControlToValidate="ddlCondLaboral" InitialValue="0" Text="*" />
                <div class="expandibleDiv">
                    <asp:DropDownList CssClass="form-control" ID="ddlCondLaboral" runat="server" DataTextField="DE_CondLaboral" DataValueField="PK_CondLaboral" DataMember="SA_LKP_TEDS_COND_LABORAL" DataSource="<%# dsPerfil %>" onChange="ddlCondLaboral();" />
                    <asp:Label ID="lblCondLaboral" runat="server" />
                </div>
            </div>
            <div class="col-print-12 col-md-6 SEPSDivs">
                <%-- Si no participa en la fuerza laboral --%>
                <span class="SEPSLabel">Si no participa en la fuerza laboral (US-SM-NOM):</span>
                <asp:RequiredFieldValidator ID="rfvNoFueraLaboral" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Si no participa en la fuerza laboral" ControlToValidate="ddlNoFueraLaboral" InitialValue="0" Text="*" />
                <div class="expandibleDiv">
                    <asp:DropDownList CssClass="form-control" ID="ddlNoFueraLaboral" runat="server" DataTextField="DE_NoFuerzaLaboral" DataValueField="PK_NoFuerzaLaboral" DataSource="<%# dvwFuerzaLaboral %>" onChange="ddlNoFueraLaboral();"/>
                    <asp:Label ID="lblNoFueraLaboral" runat="server" />
                </div>
            </div>
        </div>
    </div>
</div>

<div class="panel panel-default">
    <div class="panel-heading">
        <h3 class="panel-title">Escolaridad</h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-print-12 col-xs-12 col-lg-6 SEPSDivs">
                <%-- Ultimo grado completado --%>
                <span class="SEPSLabel">Educación (SM-NOM):</span>
                <asp:RequiredFieldValidator ID="rfvGrado" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Último grado completado" ControlToValidate="ddlGrado" InitialValue="0" Text="*" />
                <div class="expandibleDiv">
                    <asp:DropDownList CssClass="form-control" ID="ddlGrado" runat="server" DataTextField="DE_Grado" DataValueField="PK_Grado" DataSource="<%# dvwUltGrado %>" AutoPostBack="true" EnableViewState="true" OnSelectedIndexChanged="ddlGrado_SelectedIndexChanged" />
                    <asp:Label ID="lblGrado" runat="server" />
                </div>
            </div>
            <div class="col-print-12 col-xs-12 col-md-6 SEPSDivs">
                <%-- Es desertor escolar --%>
                <span class="SEPSLabel">Desertor escolar:</span>
                <asp:RequiredFieldValidator ID="rfvDesertorEscolar" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Es desertor escolar" ControlToValidate="ddlDesertorEscolar" InitialValue="0" Enabled="False" Text="*" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rightFloatAsterisk"  ControlToValidate="ddlDesertorEscolar" Display="Dynamic" ErrorMessage="Es desertor escolar" Text="*" />
                <div class="expandibleDiv">
                    <asp:DropDownList CssClass="form-control" ID="ddlDesertorEscolar" AutoPostBack="true" ViewStateMode="Enabled" OnSelectedIndexChanged="ddlDesertorEscolar_SelectedIndexChanged" runat="server">
                        <asp:ListItem />
                        <asp:ListItem Value="1">Sí</asp:ListItem>
                        <asp:ListItem Value="2">No</asp:ListItem>
                        <asp:ListItem Value="99">No aplica</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblDesertorEscolar" runat="server" />
                </div>
            </div>
            <div class="col-print-12 col-md-6 SEPSDivs">
                <%-- Ha recibido eduacion especial --%>
                <span class="SEPSLabel">¿Ha recibido educación especial?:</span>
                <asp:RequiredFieldValidator ID="rfvEducacionEspecial" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Ha recibido educación especial" ControlToValidate="ddlEducacionEspecial" Text="*" />
                <div class="expandibleDiv">
                    <asp:DropDownList CssClass="form-control" ID="ddlEducacionEspecial" runat="server">
                        <asp:ListItem />
                        <asp:ListItem Value="1">Sí</asp:ListItem>
                        <asp:ListItem Value="2">No</asp:ListItem>
<%--                        <asp:ListItem Value="99">No aplica</asp:ListItem>--%>
<%--                        Cambio por Jose A. Ramos 12/8/2021 se sustituyo no aplica por no informó--%>
                        <asp:ListItem Value="99">No informó</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblEducacionEspecial" runat="server" />
                </div>
            </div>
            <div class="col-print-12 col-xs-12 col-lg-12 SEPSDivs">
                <%-- Situacion Escolar--%>
                <span class="SEPSLabel">Situación escolar al momento de admisión (SM-NOM):</span>
                <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Situación escolar" ControlToValidate="ddlSituacionEscolar" Text="*" />
                <div class="expandibleDiv">
                    <asp:DropDownList CssClass="form-control" ID="ddlSituacionEscolar" runat="server">
                        <asp:ListItem />
                        <asp:ListItem Value="1">Ha asistido a la escuela en algún momento durante los pasados tres meses.</asp:ListItem>
                        <asp:ListItem Value="2">No ha asistido a la escuela durante los pasados tres meses.</asp:ListItem>
                        <asp:ListItem Value="6">No aplica.</asp:ListItem>
                        <asp:ListItem Value="7">Desconocido.</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblSituacionEscolar" runat="server" />
                </div>
            </div>
        </div>
    </div>
</div>

<div class="panel panel-default">
    <div class="panel-heading">
        <h3 class="panel-title">Estructura familiar</h3>
    </div>
    <div class="panel-body">
 
                <div style="height: 160px;" runat="server" id="divCompFamiliar">
                    <div class="multipleLeft">
                        <%-- Listbox left --%>
                        <span class="SEPSLabel">¿Con quién vive la persona? (Disponible)</span>
                        <asp:ListBox CssClass="form-control" ID="lbxCompFamiliarSeleccion" runat="server" Height="130px" />
                    </div>
                    <div class="multipleCenter text-center">
                        <%-- Buttons --%>
                        <div style="height: 60px;"></div>
                        <div class="btn-group center" role="group">
                            <asp:Button ID="btnEliminar" runat="server" CausesValidation="False" CssClass="btn btn-default" OnClick="btnEliminar_Click" Text="<" />
                            <asp:Button ID="btnAgregar" runat="server" CausesValidation="False" CssClass="btn btn-default" OnClick="btnAgregar_Click" Text=">" />
                        </div>
                    </div>
                    <div class="multipleRight">
                        <%-- Listbox right --%>
                        <span class="SEPSLabel">¿Con quién vive la persona? (Seleccionadas)</span>
                        <asp:ListBox CssClass="form-control" ID="lbxCompFamiliarSeleccionado" runat="server" Height="130px" />
                    </div>
                </div>
                <div class="row" runat="server" id="divLblCompFamiliar">
                    <div class="col-xs-12"><span class="SEPSLabel">¿Con quién vive la persona?:</span>
                        <asp:Label ID="lblCompFamiliar" runat="server" /></div>
                </div>
 
        <div class="row">
            <div class="col-print-6 col-md-6 SEPSDivs">
                <%-- Tamaño familia --%>
                <span class="SEPSLabel">Tamaño familia:</span>
                <asp:RequiredFieldValidator ID="rfvNumFamilia" CssClass="rightFloatAsterisk" Display="Dynamic" runat="server" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Tamaño familia" ControlToValidate="txtNumFamilia" Text="*" />
                <asp:RangeValidator ID="rvNumFamilia" runat="server" CssClass="rightFloatAsterisk" ToolTip="Escriba un número entero mayor o igual a cero (0) y menor que 50" ErrorMessage="Tamaño familia" ControlToValidate="txtNumFamilia" Type="Double" MaximumValue="50" MinimumValue="0" Display="Dynamic" Text="*" />
                <div class="expandibleDiv">
                    <asp:TextBox CssClass="form-control" ID="txtNumFamilia" runat="server" MaxLength="2" />
                    <asp:Label ID="lblNumFamilia" runat="server" />
                </div>
            </div>
            <div class="col-print-12 col-md-6 SEPSDivs">
                <%-- Fuente de ingreso --%>
                <span class="SEPSLabel">Fuente de ingresos [TEDS]:</span>
                <asp:RequiredFieldValidator ID="rfvFuenteIngreso" Display="Dynamic" runat="server" CssClass="rightFloatAsterisk" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Fuente de ingresos" ControlToValidate="ddlFuenteIngreso" InitialValue="0" Text="*" />
                <div class="expandibleDiv">
                    <asp:DropDownList CssClass="form-control" ID="ddlFuenteIngreso" runat="server" DataTextField="DE_FuenteIngreso" DataValueField="PK_FuenteIngreso" DataMember="SA_LKP_TEDS_FUENTE_INGRESO" DataSource="<%# dsPerfil %>" onChange="ddlFuenteIngreso();"/>
                    <asp:Label ID="lblFuenteIngreso" runat="server" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-print-6 col-md-6 SEPSDivs">
                <%-- Residencia --%>
                <span class="SEPSLabel">Residencia (US-SM-NOM):</span>
                <asp:RequiredFieldValidator ID="rfvResidencia" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Residencia" ControlToValidate="ddlResidencia" InitialValue="0" Text="*" />
                <div class="expandibleDiv">
                    <asp:DropDownList CssClass="form-control" ID="ddlResidencia" runat="server" DataTextField="DE_Residencia" DataValueField="PK_Residencia" DataSource="<%# dvwResidencia %>" />
                    <asp:Label ID="lblResidencia" runat="server" />
                </div>
            </div>
            <div class="col-print-6 col-md-6 SEPSDivs">
                <%-- Tiempo en residencia --%>
                <span class="SEPSLabel">Tiempo en residencia:</span>
                <asp:RequiredFieldValidator ID="rfvTiempoResidencia" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Tiempo en residencia" ControlToValidate="ddlTiempoResidencia" InitialValue="0" Text="*" />
                <div class="expandibleDiv">
                    <asp:DropDownList CssClass="form-control" ID="ddlTiempoResidencia" runat="server" DataTextField="DE_TiempoResidencia" DataValueField="PK_TiempoResidencia" DataMember="SA_LKP_TIEMPO_RESIDENCIA" DataSource="<%# dsPerfil %>" />
                    <asp:Label ID="lblTiempoResidencia" runat="server" />
                </div>
            </div>
            <div class="col-print-6 col-md-6 SEPSDivs">
                <%-- Municipio --%>
                <span class="SEPSLabel">Municipio de residencia:</span>
                <asp:RequiredFieldValidator ID="rfvMunicipio" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Municipio de residencia" ControlToValidate="ddlMunicipio" InitialValue="0" Text="*" />
                <div class="expandibleDiv">
                    <asp:DropDownList CssClass="form-control" ID="ddlMunicipio" runat="server" DataTextField="DE_Municipio" DataValueField="PK_Municipio" DataMember="SA_LKP_MUNICIPIO_RESIDENCIA" DataSource="<%# dsPerfil %>" />
                    <asp:Label ID="lblMunicipio" runat="server" />
                </div>
            </div>
            <div class="col-print-6 col-md-3 SEPSDivs">
                <%-- Zona Geografica --%>
                <span class="SEPSLabel">Zona geográfica:</span>
                <asp:RequiredFieldValidator ID="rfvZonaGeografia" Display="Dynamic" CssClass="rightFloatAsterisk" runat="server" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Zona geográfica" ControlToValidate="ddlZonaGeografia" Text="*" />
                <div class="expandibleDiv">
                    <asp:DropDownList CssClass="form-control" ID="ddlZonaGeografia" runat="server">
                        <asp:ListItem />
                        <%-- IN ZONA > EPISODIO --%>
                        <asp:ListItem Value="1">Rural</asp:ListItem>
                        <asp:ListItem Value="2">Urbana</asp:ListItem>
                        <asp:ListItem Value="3">No informó</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblZonaGeografia" runat="server" />
                </div>
            </div>
            <div class="col-print-6 col-md-3 SEPSDivs">
                <%-- Codigo Postal --%>
                <span class="SEPSLabel">Código postal:</span>
                <asp:RegularExpressionValidator ID="revZipCode" runat="server" ValidationExpression="^([0-9])+$" CssClass="rightFloatAsterisk" ToolTip="Debe ser un valor númerico" ErrorMessage="Código postal" ControlToValidate="txtZipCode" Text="*" />
                <div class="expandibleDiv">
                    <asp:TextBox CssClass="form-control" ID="txtZipCode" runat="server" MaxLength="5" />
                    <asp:Label ID="lblZipCode" runat="server" />
                </div>
            </div>
        </div>
    </div>
</div>

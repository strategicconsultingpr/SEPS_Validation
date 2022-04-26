<%@ Control Language="c#" Inherits="ASSMCA.Perfiles.wucDatosDemograficosPerfil" CodeBehind="wucDatosDemograficosPerfil.ascx.cs" %>
<input type="hidden" id="edadPerfil" runat="server" name="Hidden2"/>
<div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Datos demogr�ficos</h3>
  </div>
  <div class="panel-body">
   <div class="row">
    <div class="col-md-6 SEPSDivs"><%--Estado marital--%>
        <span class="SEPSLabel">Estado marital:</span>
        <asp:RequiredFieldValidator ID="rfvEstadoMarital" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic" ControlToValidate="ddlEstadoMarital" ErrorMessage="Estado marital" ToolTip="Seleccione un valor de la lista. Este campo es requerido." InitialValue="0" Text="*"/>
        <div class="expandibleDiv">
            <asp:DropDownList CssClass="form-control" ID="ddlEstadoMarital" runat="server" DataSource="<%# dsPerfil %>" DataMember="SA_LKP_TEDS_ESTADO_MARITAL" DataValueField="PK_EstadoMarital" DataTextField="DE_EstadoMarital"/>
            <asp:Label ID="lblEstadoMarital" runat="server"/>
        </div>
    </div>
    <div class="col-md-6 SEPSDivs"><%--Condici�n laboral--%>
        <span class="SEPSLabel">Condici�n laboral (US-SM-NOM):</span>
        <asp:RequiredFieldValidator ID="rfvCondLaboral"  CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Condici�n laboral" ControlToValidate="ddlCondLaboral" InitialValue="0" Text="*"/>
        <div class="expandibleDiv">
            <asp:DropDownList  CssClass="form-control" ID="ddlCondLaboral" runat="server" DataTextField="DE_CondLaboral" DataValueField="PK_CondLaboral" DataMember="SA_LKP_TEDS_COND_LABORAL" DataSource="<%# dsPerfil %>" onChange="ddlCondLaboral();"/>
            <asp:Label ID="lblCondLaboral" runat="server"></asp:Label>
        </div>
    </div>
    <div class="col-md-6 SEPSDivs"><%--Si no participa en la fuerza laboral--%>
        <span class="SEPSLabel">Si no participa en la fuerza laboral (US-SM-NOM):</span>
        <asp:RequiredFieldValidator ID="rfvNoFueraLaboral"  CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ControlToValidate="ddlNoFueraLaboral" ErrorMessage="Si no participa en la fuerza laboral" ToolTip="Seleccione un valor de la lista. Este campo es requerido." InitialValue="0" Text="*"/>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ControlToValidate="ddlNoFueraLaboral" ErrorMessage="Si no participa en la fuerza laboral" ToolTip="Seleccione un valor de la lista. Este campo es requerido." InitialValue="0" Text="*"/>
        <div class="expandibleDiv">
            <asp:DropDownList  CssClass="form-control" ID="ddlNoFueraLaboral" runat="server" DataSource="<%# dvwFuerzaLaboral %>" DataValueField="PK_NoFuerzaLaboral" DataTextField="DE_NoFuerzaLaboral" onChange="ddlNoFueraLaboral();"/>
            <asp:Label ID="lblNoFueraLaboral" runat="server"/>
        </div>
    </div>
    <div class="col-md-6 SEPSDivs"><%--N�mero de hijos--%>
        <span class="SEPSLabel">N�mero de hijos:</span>
        <asp:RequiredFieldValidator ID="rfvNumNinos"  CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ToolTip="Campo Requerido. Escriba un valor numerico." ErrorMessage="N�mero de hijos" ControlToValidate="txtNumNinos" Text="*"/>
        <asp:RangeValidator ID="rvNumNinos" runat="server"  CssClass="rightFloatAsterisk"  ToolTip="Escriba un n�mero entero mayot igual a cero (0)." ErrorMessage="N�mero de hijos" ControlToValidate="txtNumNinos" Display="Dynamic" MinimumValue="0" MaximumValue="50" Type="Integer" Text="*"/>
        <div class="expandibleDiv">
            <asp:TextBox CssClass="form-control" ID="txtNumNinos" runat="server" MaxLength="2"/>
            <asp:Label ID="lblNumNinos" runat="server"/>
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
    <div class="col-md-6 SEPSDivs"><%--�lt. grado comp.--%>
        <span class="SEPSLabel">Educaci�n (SM-NOM):</span>
        <asp:RequiredFieldValidator ID="rfvGrado"  CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ControlToValidate="ddlGrado" ErrorMessage="�ltimo grado completado" ToolTip="Seleccione un valor de la lista. Este campo es requerido." InitialValue="0" Text="*"/>
        <div class="expandibleDiv">
            <asp:DropDownList  CssClass="form-control" ID="ddlGrado" runat="server" DataSource="<%# dvwUltGrado %>" DataValueField="PK_Grado" DataTextField="DE_Grado"  AutoPostBack="true" EnableViewState="true" OnSelectedIndexChanged="ddlGrado_SelectedIndexChanged" />
            <asp:Label ID="lblGrado" runat="server"/>
                                <asp:Label ID="lblFeNacimiento" Visible="false" runat="server" />

        </div>
    </div> 
    <div class="col-print-6 col-md-6 SEPSDivs"><%-- Situaci�n Escolar--%>
         <span class="SEPSLabel">Situaci�n escolar al momento de evaluaci�n (SM-NOM):</span>
        <asp:requiredfieldvalidator id="Requiredfieldvalidator2"  CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Situaci�n escolar" ControlToValidate="ddlSituacionEscolar" Text="*"/>
        <div class="expandibleDiv">
	        <asp:dropdownlist  CssClass="form-control" id="ddlSituacionEscolar" runat="server" >
		        <asp:ListItem/>
		        <asp:ListItem Value="1">Ha asistido a la escuela en alg�n momento durante los pasados tres meses.</asp:ListItem>
		        <asp:ListItem Value="2">No ha asistido a la escuela durante los pasados tres meses.</asp:ListItem>
		        <asp:ListItem Value="6">No aplica.</asp:ListItem>
		        <asp:ListItem Value="7">Desconocido.</asp:ListItem>
	        </asp:dropdownlist>
            <asp:Label id="lblSituacionEscolar" runat="server"/>
        </div>
    </div>
    <div class="col-print-6 col-md-6 SEPSDivs"><%--Ha recibido educaci�n especial--%>
        <span class="SEPSLabel">�Ha recibido o est� recibiendo educaci�n especial?:</span>
        <asp:RequiredFieldValidator ID="rfvEducacionEspecial"  CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Ha recibido educaci�n especial" ControlToValidate="ddlEducacionEspecial" Text="*"/>
        <div class="expandibleDiv">
            <asp:DropDownList  CssClass="form-control" ID="ddlEducacionEspecial" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGrado_SelectedIndexChanged">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="1">S�</asp:ListItem>
                <asp:ListItem Value="2">No</asp:ListItem>
                <asp:ListItem Value="99">No aplica</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lblEducacionEspecial" runat="server"/>
        </div>
    </div>
    <div class="col-md-6 SEPSDivs">

        <%--Es desertor escolar--%>
        <span class="SEPSLabel">Es desertor escolar:</span>
        <asp:RequiredFieldValidator ID="rfvDesertorEscolar"  CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Es desertor escolar" ControlToValidate="ddlDesertorEscolar" InitialValue="0" Text="*"/>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="rightFloatAsterisk" ControlToValidate="ddlDesertorEscolar" Display="Dynamic" ErrorMessage="Es desertor escolar" Text="*" />
        <div class="expandibleDiv">
            <asp:DropDownList CssClass="form-control" ID="ddlDesertorEscolar" AutoPostBack="true" ViewStateMode="Enabled" OnSelectedIndexChanged="ddlDesertorEscolar_SelectedIndexChanged" runat="server">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="1">S�</asp:ListItem>
                <asp:ListItem Value="2">No</asp:ListItem>
                <asp:ListItem Value="99">No aplica</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lblDesertorEscolar" runat="server"/>
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
 
        <div style="height:160px;" runat="server" id="divCompFamiliar">
            <div class="multipleLeft"> <%-- Listbox left --%>
                <span class="SEPSLabel">�Con qui�n vive la persona? (Disponibles)</span>
                <asp:ListBox  CssClass="form-control" ID="lbxCompFamiliarSeleccion" runat="server" Height="130px"/>
            </div>
            <div class="multipleCenter text-center"><%--Buttons--%>
                <div style="height:60px;"></div>  
                <div class="btn-group" role="group">  
                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-default"  CausesValidation="False" OnClick="btnEliminar_Click" Text="<"/>
                    <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-default"  CausesValidation="False" OnClick="btnAgregar_Click" Text=">"/>
                </div>
            </div>
            <div class="multipleRight">
                <span class="SEPSLabel">�Con qui�n vive la persona? (Seleccionados):</span>
                <asp:ListBox  CssClass="form-control"  ID="lbxCompFamiliarSeleccionado" runat="server" Height="130px"/>
            </div>
        </div>
        <div class="row" runat="server" id ="divLblCompFamiliar">
            <div class="col-xs-12">
                <span class="SEPSLabel">Composici�n familiar:</span>
                <asp:Label id="lblCompFamiliar" runat="server"/>
            </div>
        </div>
 
<div class="row">
    <div class="col-md-6 SEPSDivs"><%--Tama�o familia--%>
        <span class="SEPSLabel">Tama�o familia:</span>
        <asp:RequiredFieldValidator ID="rfvNumFamilia"  CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ControlToValidate="txtNumFamilia" ErrorMessage="Tama�o familia"  ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
        <asp:RangeValidator ID="rvNumFamilia"  CssClass="rightFloatAsterisk"  runat="server" ControlToValidate="txtNumFamilia" ErrorMessage="Tama�o familia"  ToolTip="Escriba un n�mero entero mayor o igual a cero (0) y menor que 50" Type="Double" MaximumValue="50" MinimumValue="0" Display="Dynamic" Text="*"/>
        <div class="expandibleDiv">
            <asp:TextBox  CssClass="form-control" ID="txtNumFamilia" runat="server" MaxLength="2"/>
            <asp:Label ID="lblNumFamilia" runat="server"/>
        </div>
    </div>
    <div class="col-md-6 SEPSDivs"><%--Residencia--%>
        <span class="SEPSLabel">Residencia (US-SMNOM):</span>
       <asp:RequiredFieldValidator ID="rfvResidencia"  CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ControlToValidate="ddlResidencia" ErrorMessage="Residencia" ToolTip="Seleccione un valor de la lista. Este campo es requerido." InitialValue="0" Text="*"/>
        <div class="expandibleDiv">
            <asp:DropDownList  CssClass="form-control"  ID="ddlResidencia" runat="server" DataSource="<%# dvwResidencia %>" DataValueField="PK_Residencia" DataTextField="DE_Residencia"/>
            <asp:Label ID="lblResidencia" runat="server"/>
        </div>
    </div>
</div>

<div class="row">
    <div class="col-xs-6 SEPSDivs"><%--�Cu�ntas veces ha participado de reuniones de grupo de apoyo, de auto-ayuda, religiosos o ha buscado ayuda de familiares, amigos u otros durante los pasados 30 d�as como apoyo a su proceso de recuperaci�n?--%>
        <span class="SEPSLabel">�Cu�ntas veces ha participado de reuniones de grupos de auto-ayuda durante los pasados 30 d�as como apoyo a su proceso de recuperaci�n? (SU-NOM, incluye alcoh�licos an�nimos, narc�ticos an�nimos, programas pares etc.)</span>
    </div>
    <div class="col-xs-6 SEPSDivs"><%--�Cu�ntas veces ha participado de reuniones de grupo de apoyo, de auto-ayuda, religiosos o ha buscado ayuda de familiares, amigos u otros durante los pasados 30 d�as como apoyo a su proceso de recuperaci�n?--%>
        <div class="expandibleDiv">
            <asp:DropDownList  CssClass="form-control"  ID="ddlFreq_AutoAyuda" runat="server" DataSource="<%# dvwFreqAutoAyuda %>" DataTextField="DE_FreqAutoAyuda" DataValueField="PK_FreqAutoAyuda"/>
            <asp:Label ID="lblFreq_AutoAyuda" runat="server"/>
        </div>
        <asp:RequiredFieldValidator ID="rfvFreq_AutoAyuda" CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ControlToValidate="ddlFreq_AutoAyuda" ErrorMessage="�Cu�ntas veces ha participado de reuniones de grupo de apoyo, de auto-ayuda, religiosos o ha buscado ayuda de familiares, amigos u otros durante los pasados 30 d�as como apoyo a su proceso de recuperaci�n?"  ToolTip="Seleccione un valor de la lista. Este campo es requerido" Text="*"/>
    </div>
</div>
<div class="row">
    <div class="col-lg-12 SEPSDivs"><%--Ha sido arrestado durante los pasados 30 d�as?--%>
        <span class="SEPSLabel">�Ha sido arrestado durante los pasados 30 d�as? (US-SMNOM)</span>
        <asp:RequiredFieldValidator ID="rfvArrestado" CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ControlToValidate="ddlArrestado" ErrorMessage="�Ha sido arrestado durante los pasados 30 d�as?" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>    
        <div class="">
            <asp:DropDownList  CssClass="form-control" ID="ddlArrestado" runat="server" onChange="ddlArrestado30();">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="1">S�</asp:ListItem>
                <asp:ListItem Value="2">No</asp:ListItem>
                <asp:ListItem Value="99">No aplica</asp:ListItem>

            </asp:DropDownList>
            <asp:Label ID="lblArrestado" runat="server"/>
        </div>
    </div>
    </div>

    <div class="row">
    <div class="col-lg-12 SEPSDivs"><%--N�mero de arrestos en los pasados 30 d�as o durante tratamiento si dur� menos de 30 d�as (US-SM-NOM):--%>
        <span class="SEPSLabel">N�mero de arrestos en los pasados 30 d�as o durante tratamiento si dur� menos de 30 d�as (US-SM-NOM):</span>
        <asp:RequiredFieldValidator ID="rfvArrestos30" CssClass="rightFloatAsterisk"  runat="server" Display="Dynamic" ControlToValidate="txtArrestos30" ErrorMessage="N�mero de arrestos en los pasados 30 d�as o durante tratamiento si dur� menos de 30 d�as (US-SM-NOM):" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
        <asp:RangeValidator ID="rvArrestos30" CssClass="rightFloatAsterisk"  runat="server" ControlToValidate="txtArrestos30" ErrorMessage="N�mero de arrestos en los pasados 30 d�as o durante tratamiento si dur� menos de 30 d�as (US-SM-NOM):" ToolTip="Escriba un n�mero entero mayor o igual a cero (0) y menor que 30" Type="Integer" MaximumValue="30" MinimumValue="0" Display="Dynamic" Text="*"/>
        <div class="">
            <asp:TextBox  CssClass="form-control" ID="txtArrestos30" runat="server" MaxLength="2"/>
            <asp:Label ID="lblArrestos30" runat="server"/>
        </div>
    </div>
</div>
</div>
</div>

   <asp:TextBox Visible="false" Enabled="true" ID="lblFePerfil" runat="server"></asp:TextBox>

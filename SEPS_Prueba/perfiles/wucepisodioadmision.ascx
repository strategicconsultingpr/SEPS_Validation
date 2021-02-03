<%@ Control Language="c#" Inherits="ASSMCA.Perfiles.wucEpisodioAdmision" CodeBehind="wucEpisodioAdmision.ascx.cs" %>
<input id="CO_Tipo" type="hidden" name="Hidden2" runat="server"/>
<input id="hAccion" type="hidden" name="accion" runat="server"/>
<div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Nivel de cuidado de este episodio</h3>
  </div>
  <div class="panel-body">
    <div class="row">
    <%--<div class="col-md-12 SEPSDivs">
        <span class="SEPSLabel">Diagnósticos concurrentes de salud mental y uso de sustancias</span>
        <div class="expandibleDiv">
            <asp:DropDownList CssClass="form-control" ID="ddlDSMVDiagDual" runat="server">
                <asp:ListItem ></asp:ListItem>
                <asp:ListItem Value="1">Sí</asp:ListItem>
                <asp:ListItem Value="2">No</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lblDSMVDiagDual" runat="server" />
        </div>

    </div>--%>
    <div class="col-md-12 SEPSDivs"><%--Etapa del servicio--%>
        <span class="SEPSLabel">Etapa del servicio:</span>          <asp:RequiredFieldValidator ID="rfvMenor" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic"  InitialValue="0" ControlToValidate="ddlEtapaServicio" ErrorMessage="Etapa del servicio" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*" />
        <div class="expandibleDiv">
        <asp:DropDownList  CssClass="form-control" ID="ddlEtapaServicio" runat="server" DataSource="<%# dsPerfil %>" DataMember="SA_LKP_TEDS_ETAPA_SERVICIO" DataTextField="DE_EtapaServicio" DataValueField="PK_EtapaServicio" />
        <asp:Label ID="lblEtapaServicio" runat="server" />
      
        </div>
    </div>
    <div class="col-md-7 SEPSDivs"><%--Nivel de Cuidado Abuso de Sustancias--%>
        <span class="SEPSLabel">Nivel de cuidado de sustancias [TEDS]:</span>
           <asp:RequiredFieldValidator ID="rfvNivelCuidadoSustancias" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic"  InitialValue="0" ControlToValidate="ddlNivelCuidadoSustancias" ErrorMessage="Nivel de cuidado (Abuso de sustancias)" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*" />
        <div class="expandibleDiv">
        <asp:DropDownList  CssClass="form-control" ID="ddlNivelCuidadoSustancias" runat="server" onChange="ddlNivelCuidadoSustancias();"   AutoPostBack="true"/>
        <asp:Label ID="lblNivelCuidadoSustancias" runat="server" />
     
            </div> 
    </div>
    <div class="col-md-5 SEPSDivs"><%--Días de espera para entrar a tratamiento--%>
        <span class="SEPSLabel">Días de espera para entrar a tratamiento [TEDS]:</span>
            <asp:RequiredFieldValidator ID="rfvDíasSustancias" runat="server"  Display="Dynamic" CssClass="rightFloatAsterisk" ControlToValidate="txtDíasSustancias" ErrorMessage="Días de espera para entrar a tratamiento"  ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
        <asp:RangeValidator ID="rvDíasSustancias" runat="server"  Display="Dynamic" CssClass="rightFloatAsterisk" ControlToValidate="txtDíasSustancias" ErrorMessage="Días de espera para entrar a tratamiento"  ToolTip="Escriba un número entero mayor o igual a cero (0) y menor que 255" Type="Integer" MaximumValue="255" MinimumValue="0" Text="*"/>
        <div class="expandibleDiv">
        <asp:TextBox  CssClass="form-control" ID="txtDíasSustancias" runat="server" MaxLength="3" Text="0" />
        <asp:Label ID="lblDíasSustancias" runat="server"/>
    
            </div>
    </div>
    <div class="col-md-12 SEPSDivs"><%--Usa metadona como parte del tratamiento?--%>
        <span class="SEPSLabel">¿Usa medicamento como parte del tratamiento contra la dependencia de opiáceos? [TEDS]:</span>
          <asp:RequiredFieldValidator ID="rfvMetadona" runat="server" Display="Dynamic" CssClass="rightFloatAsterisk"  ControlToValidate="ddlMetadona" ErrorMessage="¿Usa medicamento como parte del tratamiento contra la dependencia de opiáceos?" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
        <div class="expandibleDiv">
        <asp:DropDownList CssClass="form-control" ID="ddlMetadona" runat="server" onChange="ddlMetadona();">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">Metadona</asp:ListItem>         
            <asp:ListItem Value="3">Buprenorfina</asp:ListItem>
            <asp:ListItem Value="2">No</asp:ListItem>
            <asp:ListItem Value="4">No aplica (salud mental)</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="lblMetadona" runat="server" />
      
            </div>
    </div>
    <div class="col-md-12 SEPSDivs"><%--Co- dependiente?--%>
        <span class="SEPSLabel">¿Co-dependiente? (persona que no tiene problemas de sustancias, pero busca servicios debido a problemas que pueden estar surgiendo en su vida a causa de su relación con usuario) [TEDS]:</span>
           <asp:RequiredFieldValidator ID="rfvCodependiente" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic"  ControlToValidate="ddlCodependiente" ErrorMessage="¿Co-dependiente?" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
        <div class="expandibleDiv">
        <asp:DropDownList CssClass="form-control" ID="ddlCodependiente" runat="server">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">Sí</asp:ListItem>
            <asp:ListItem Value="2">No</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="lblCodependiente" runat="server"/>
     
            </div>
    </div>
    <div class="col-md-7 SEPSDivs"><%--Nivel de Cuidado Salud mental--%>
        <span class="SEPSLabel">Nivel de cuidado de Salud mental [TEDS]:</span>
                <asp:RequiredFieldValidator ID="rfvNivelCuidadoSaludMental" runat="server" CssClass="rightFloatAsterisk"  Display="Dynamic" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Nivel de cuidado (Salud mental)" ControlToValidate="ddlNivelCuidadoSaludMental" InitialValue="0" Text="*"/>
        <div class="expandibleDiv">
                <asp:DropDownList CssClass="form-control" ID="ddlNivelCuidadoSaludMental" runat="server" onChange="ddlNivelCuidadoSaludMental()"   AutoPostBack="true"/>
                <asp:Label ID="lblNivelCuidadoSaludMental" runat="server"/>
        
            </div>
    </div>
    <div class="col-md-5 SEPSDivs"><%--Días de espera para entrar a tratamiento--%>  
        <span class="SEPSLabel">Días de espera para entrar a tratamiento [TEDS]:</span>
           <asp:RequiredFieldValidator ID="rfvDíasMental" runat="server" CssClass="rightFloatAsterisk"  Display="Dynamic" ControlToValidate="txtDíasMental" ErrorMessage="Días de espera para entrar a tratamiento" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
        <asp:RangeValidator ID="rvDíasMental" runat="server" CssClass="rightFloatAsterisk" ControlToValidate="txtDíasMental" ErrorMessage="Días de espera para entrar a tratamiento" ToolTip="Escriba un número entero mayor o igual a cero (0) y menor que 255" Type="Integer" MaximumValue="255" MinimumValue="0" Display="Dynamic" Text="*"/>
        <div class="expandibleDiv">
        <asp:TextBox  CssClass="form-control" ID="txtDíasMental" runat="server" MaxLength="3"/>
        <asp:Label ID="lblDíasMental" runat="server"/>
               
            </div>
    </div>
</div>
  </div>
</div>
<div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Otros</h3>
  </div>
  <div class="panel-body">
   <div class="row">
    <div class="col-md-6 SEPSDivs"><%--Fuente del referido--%>
        <span class="SEPSLabel">Fuente del referido:</span>
         <asp:RequiredFieldValidator ID="rfvFuenteReferido" runat="server" CssClass="rightFloatAsterisk"  Display="Dynamic" InitialValue="0" ControlToValidate="ddlFuenteReferido" ErrorMessage="Fuente del referido" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
        <div class="expandibleDiv">
        <asp:DropDownList  CssClass="form-control" ID="ddlFuenteReferido" runat="server" DataSource="<%# dsPerfil %>" DataMember="SA_LKP_TEDS_REFERIDO" DataTextField="DE_Referido" DataValueField="PK_Referido" onChange="ddlFuenteReferido()"/>
        <asp:Label ID="lblFuenteReferido" runat="server"/>
       
    </div>
    </div>
    <div class="col-md-6 SEPSDivs"><%--Estado legal del referido--%>
        <span class="SEPSLabel">Estado legal del referido:</span>
         <asp:RequiredFieldValidator ID="rfvEstadoLegal" runat="server" CssClass="rightFloatAsterisk"  Display="Dynamic" InitialValue="0" ControlToValidate="ddlEstadoLegal" ErrorMessage="Estado legal del referido" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
        <div class="expandibleDiv">
        <asp:DropDownList  CssClass="form-control" ID="ddlEstadoLegal" runat="server" DataSource="<%# dsPerfil %>" DataMember="SA_LKP_TEDS_ESTADO_LEGAL" DataTextField="DE_EstadoLegal" DataValueField="PK_EstadoLegal" onChange="ddlEstadoLegal();" />
        <asp:Label ID="lblEstadoLegal" runat="server"/>
            </div>
    </div>
    <div class="col-md-6 SEPSDivs"><%--Ha sido arrestado anteriormente--%>
        <span class="SEPSLabel">¿Ha sido arrestado alguna vez en su vida?:</span>
        <asp:RequiredFieldValidator ID="rfvArrestado" runat="server"  CssClass="rightFloatAsterisk"  Display="Dynamic" ControlToValidate="ddlArrestado" ErrorMessage="¿Ha sido arrestado anteriormente?" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
        <div class="expandibleDiv">
            <asp:DropDownList  CssClass="form-control" ID="ddlArrestado" runat="server" onChange="ddlArrestado();" OnSelectedIndexChanged="ddlArrestado_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="1">Sí</asp:ListItem>
                <asp:ListItem Value="2">No</asp:ListItem>
            </asp:DropDownList>

            <asp:Label ID="lblArrestado" runat="server"/>
        </div>
    </div>
    <div class="col-md-6 SEPSDivs"><%--Ha sido arrestado en los últ. 30 días--%>
        <span class="SEPSLabel">¿Ha sido arrestado en los pasados 30 días?</span>
         <asp:RequiredFieldValidator ID="rfvArrestado30" runat="server" InitialValue="" CssClass="rightFloatAsterisk" Display="Dynamic"  ControlToValidate="ddlArrestado30" ErrorMessage="¿Ha sido arrestado en los pasados 30 días?" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
        <div class="expandibleDiv">
            <asp:DropDownList  CssClass="form-control" ID="ddlArrestado30" onChange="ddlArrestado30();" runat="server" OnSelectedIndexChanged="ddlArrestado_SelectedIndexChanged" AutoPostBack="true" >
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="1">Sí</asp:ListItem>
                <asp:ListItem Value="2">No</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lblArrestado30" runat="server"/>       
        </div>
    </div>    
    <div class="col-md-6 SEPSDivs"><%--Núm. de arrestos en tratamiento o en últimos 30 días--%>
        <span class="SEPSLabel">Número de arrestos en los pasados 30 días:</span>
            <asp:RequiredFieldValidator ID="rfvArrestos30" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic" ControlToValidate="txtArrestos30" ErrorMessage="Número de arrestos en los pasados 30 días" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
            <asp:RangeValidator ID="rvArrestos30" runat="server" CssClass="rightFloatAsterisk" ControlToValidate="txtArrestos30" ErrorMessage="Número de arrestos en los pasados 30 días" ToolTip="Valor invalido" Type="Integer" MaximumValue="30" MinimumValue="0" Display="Dynamic" Text="*"/>
            <div class="expandibleDiv">
                <asp:TextBox  CssClass="form-control" ID="txtArrestos30" runat="server" MaxLength="2"/>
                <asp:Label ID="lblArrestos30" runat="server"/>
            </div>
    </div>
</div>
 
        <div style="height:160px;"  runat="server" id="divProbJusticia"><%--Multiple seleccion - Faltas--%>
            <div class="multipleLeft"><%--Problema con la justicia/Faltas cometidas--%>
                <span class="SEPSLabel">Listado de problemas de justicia (Disponibles):</span>
                <asp:ListBox CssClass="form-control" ID="lbxProbJusticiaSeleccion" runat="server"  Height="130px"/>
            </div>
            <div class="multipleCenter text-center"><%--Buttons--%>
                <div style="height:60px;"></div>
                <div class="btn-group" role="group"> 
                    <asp:Button ID="Button4" runat="server" CssClass="btn btn-default" CausesValidation="False" OnClick="btnEliminar_Click" Text="<" />
                    <asp:Button ID="Button3" runat="server" CssClass="btn btn-default" CausesValidation="False" OnClick="btnAgregar_Click"  Text=">" />
                </div>
            </div>
            <div class="multipleRight">  <%--Listbox right--%>   
                <span class="SEPSLabel">Listado de problemas de justicia (Seleccionados):</span>
                <asp:ListBox CssClass="form-control" ID="lbxProbJusticiaSeleccionado" runat="server" Height="130px"/>
            </div>
        </div>
        <div class="row" runat="server" id="divLblProbJusticia">
            <div class="col-xs-12">
                <span class="SEPSLabel">Problemas de justicia:</span>
                <asp:Label ID="lblProbJusticia" runat="server"/>
            </div>
        </div>
 
  </div>
</div>

<div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Datos de salud general</h3>
  </div>
  <div class="panel-body">
                                 
        <div style="height:160px;"  runat="server" id="divCondicionesDiagnosticadas">
            <div class="multipleLeft"> <%-- Listbox left --%>
                <span class="SEPSLabel">Condiciones (Disponibles)</span>
                <asp:ListBox CssClass="form-control" ID="lbxCondicionesDiagnosticadasSeleccion" runat="server" Height="130px"/>
            </div>
            <div class="multipleCenter text-center"> <%-- Buttons --%>
                <div style="height:60px;"></div>
                <div class="btn-group" role="group">  
                    <asp:Button ID="btnEliminarCondicionesDiagnosticadas" runat="server" CssClass="btn btn-default" CausesValidation="False" onclick="btnEliminarCondicionesDiagnosticadas_Click" Text="<" />
                    <asp:Button ID="btnAgregarCondicionesDiagnosticadas"  runat="server" CssClass="btn btn-default" CausesValidation="False" onclick="btnAgregarCondicionesDiagnosticadas_Click"  Text=">" />
                </div>
            </div>
            <div class="multipleRight"> <%-- Listbox right --%>
                <span class="SEPSLabel">Condiciones (Seleccionadas)</span>
                <asp:ListBox CssClass="form-control" ID="lbxCondicionesDiagnosticadasSeleccionado" runat="server" Height="130px"/>
            </div>
        </div>
        <div class="row" runat="server" id="divLblCondicionesDiagnosticadas">
            <div class="col-xs-12">
                <span class="SEPSLabel">Condiciones diagnosticadas:</span>
                <asp:Label ID="lblCondicionesDiagnosticadas" runat="server"/>
            </div>
        </div>
 
  </div>
</div>

<div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Información del episodio anterior de servicio de abuso de sustancias</h3>
  </div>
  <div class="panel-body">
    <div class="row">
    <div class="col-md-12 SEPSDivs"><%--Episodios previos al tratamiento--%>
        <span class="SEPSLabel">Número de episodios/servicios de tratamiento que ha recibido anteriormente [TEDS]:</span>
          <asp:RequiredFieldValidator ID="rfvPreviosSustancias" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic"  InitialValue="0" ControlToValidate="ddlPreviosSustancias" ErrorMessage="Episodios previos al tratamiento" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
        <div class="expandibleDiv">
        <asp:DropDownList  CssClass="form-control" ID="ddlPreviosSustancias" onChange="ddlPreviosSustancias()" runat="server" DataSource="<%# dsPerfil %>" DataMember="SA_LKP_TEDS_EPISODIO_PREVIO" DataTextField="DE_EpisodiosPrevios" DataValueField="PK_EpisodiosPrevios" />
        <asp:Label ID="lblPreviosSustancias" runat="server"/>
      
    </div>
    </div>
</div>
<div class="row">
    <div class="col-md-12 SEPSDivs"><%--Duracion del ultimo episodio de servicio de abuso de sustancias--%>
        <span class="SEPSLabel">Duración del último servicio de tratamiento de uso de sustancias:</span>
                <asp:RequiredFieldValidator ID="rfvUltSustancias" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic"  InitialValue="0" ControlToValidate="ddlUltSustancias" ErrorMessage="Duración del último episodio de servicio de abuso de sustancias" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
        <div class="expandibleDiv">
        <asp:DropDownList  CssClass="form-control" ID="ddlUltSustancias" onChange="ddlUltSustancias()" runat="server" DataSource="<%# dvwUltSustancias %>" DataTextField="DE_TiempoUltTrat" DataValueField="PK_TiempoUltTrat" />
        <asp:Label ID="lblUltSustancias" runat="server"/>

    </div>
    </div>
</div>
<div class="row">
    <div class="col-md-12 SEPSDivs"><%--Tiempo desde la ultima alta de servicio para abuso de sustancias--%>
        <span class="SEPSLabel">Tiempo desde la última alta de servicio para uso de sustancias:</span>
        <div class="leftFloat">
            <asp:TextBox  CssClass="form-control" ID="txtDíasSustUlt" runat="server" MaxLength="3" Width="48px" Text="0"/>        
            <asp:Label ID="lblDíasSustUlt" runat="server"/>
        </div>
        <div class="leftFloat">
            <span>días</span>
        </div>
        <asp:RangeValidator ID="rvDíasSustUlt" CssClass="leftFloatAsterisk" runat="server" ControlToValidate="txtDíasSustUlt" ErrorMessage="Tiempo desde la última alta de servicio para abuso de sustancias" ToolTip="Escriba un número entero mayor o igual a cero (0) y menor que 31" Type="Integer" MaximumValue="31" MinimumValue="0" Display="Dynamic" Text="*"/>  
        <div class="leftFloat">
        <asp:TextBox  CssClass="form-control" ID="txtMesesSustUlt" runat="server" MaxLength="3" Width="48px" Text="0"/>
        <asp:Label ID="lblMesesSustUlt" runat="server"/>
         </div>
        <div class="leftFloat">
        <span>meses</span>
        </div>
        <asp:RequiredFieldValidator ID="rfvMesesSustUlt" CssClass="leftFloatAsterisk" runat="server" Display="Dynamic"  ControlToValidate="txtMesesSustUlt" ErrorMessage="Tiempo desde la última alta de servicio para abuso de sustancias" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
        <asp:RangeValidator ID="rvMesesSustUlt" runat="server" CssClass="leftFloatAsterisk" ControlToValidate="txtMesesSustUlt" ErrorMessage="Tiempo desde la última alta de servicio para abuso de sustancias" ToolTip="Escriba un número entero mayor o igual a cero (0) y menor que 120" Type="Integer" MaximumValue="120" MinimumValue="0" Display="Dynamic" Text="*"/>
    </div>
</div>
<div class="row">
    <div class="col-md-12 SEPSDivs"><%--Nivel de cuidado--%>
        <span class="SEPSLabel">Nivel de cuidado:</span>
                <asp:RequiredFieldValidator ID="rfvNivelSustancias" CssClass="rightFloatAsterisk" runat="server"  Display="Dynamic" InitialValue="0" ControlToValidate="ddlNivelSustancias" ErrorMessage="Nivel de cuidado del episodio anterior de abuso de sustancias" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
        <div class="expandibleDiv">
        <asp:DropDownList  CssClass="form-control" ID="ddlNivelSustancias" runat="server" DataSource="<%# dsPerfil %>" DataTextField="DE_AbusoSustancias" DataValueField="PK_AbusoSustancias" DataMember="SA_LKP_ABUSO_SUSTANCIAS_ANTERIOR" />
        <asp:Label ID="lblNivelSustancias" runat="server"/>
    </div>
    </div>
</div>
  </div>
</div>
<div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Información del episodio anterior de servicio de salud mental</h3>
  </div>
  <div class="panel-body">
    <div class="row">
    <div class="col-md-12 SEPSDivs"><%--Episodios previos al tratamiento--%>
        <span class="SEPSLabel">Número de episodios/servicios de tratamiento que ha recibido anteriormente:</span>
        <asp:RequiredFieldValidator ID="rfvPreviosMental" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic"  InitialValue="0" ControlToValidate="ddlPreviosMental" ErrorMessage="Episodios previos al tratamiento" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
        <div class="expandibleDiv">
        <asp:DropDownList CssClass="form-control" ID="ddlPreviosMental" onChange="ddlPreviosMental()" runat="server" DataSource="<%# dvwEpisPreviosMental %>" DataTextField="DE_EpisodiosPrevios" DataValueField="PK_EpisodiosPrevios" />
        <asp:Label ID="lblPreviosMental" runat="server"/>
    </div>
  </div>
</div>
<div class="row">
    <div class="col-md-12 SEPSDivs"><%--Duración del último episodio de servicio de salud mental--%>
        <span class="SEPSLabel">Duración del último servicio de tratamiento de uso de salud mental:</span>
         <asp:RequiredFieldValidator ID="rfvUltMental" CssClass="rightFloatAsterisk" runat="server"  Display="Dynamic" InitialValue="0" ControlToValidate="ddlUltMental" ErrorMessage="Duración del último episodio de servicio de abuso de sustancias" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
        <div class="expandibleDiv">
        <asp:DropDownList CssClass="form-control" ID="ddlUltMental"  onChange="ddlUltMental()" runat="server" DataSource="<%# dvwUltMental %>" DataTextField="DE_TiempoUltTrat" DataValueField="PK_TiempoUltTrat"/>
        <asp:Label ID="lblUltMental" runat="server"/>
       
    </div>
    </div>
</div>
<div class="row">
    <div class="col-md-12 SEPSDivs"><%--Tiempo desde la última alta de servicio para salud mental--%>
        <span class="SEPSLabel">Tiempo desde la última alta de servicio para uso de salud mental:</span>
        <div class="leftFloat">
        <asp:TextBox  CssClass="form-control" ID="txtDíasMentUlt" runat="server" MaxLength="3" Width="48px" Text="0"/>
        <asp:Label ID="lblDíasMentUlt" runat="server"/>
            </div>
        <div class="leftFloat">
            <span>días</span>
        </div>
        <asp:RangeValidator ID="rvDíasMentUlt" runat="server" CssClass="leftFloatAsterisk" ControlToValidate="txtDíasMentUlt" ErrorMessage="Tiempo desde la última alta de servicio para abuso de sustancias" ToolTip="Escriba un número entero mayor o igual a cero (0) y menor que 31" Type="Integer" MaximumValue="31" MinimumValue="0" Display="Dynamic" Text="*" />
        <div class="leftFloat">
        <asp:TextBox CssClass="form-control" ID="txtMesesMentUlt" runat="server" MaxLength="3" Width="48px" Text="0"/>
        <asp:Label ID="lblMesesMentUlt" runat="server"/>
                 </div>
        <div class="leftFloat">
            <span>meses</span>
        </div>
        <asp:RangeValidator ID="rvMesesMentUlt" runat="server" CssClass="leftFloatAsterisk" ControlToValidate="txtMesesMentUlt" ErrorMessage="Tiempo desde la última alta de servicio para abuso de sustancias" ToolTip="Escriba un número entero mayor o igual a cero (0) y menor que 120" Type="Integer" MaximumValue="120" MinimumValue="0" Display="Dynamic" Text="*"/>  
        <asp:RequiredFieldValidator ID="rfvMesesMentUlt" CssClass="leftFloatAsterisk" runat="server"  Display="Dynamic" ControlToValidate="txtMesesMentUlt" ErrorMessage="Tiempo desde la última alta de servicio para abuso de sustancias" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
    </div>
</div>
<div class="row">
    <div class="col-md-12 SEPSDivs"><%--Nivel de cuidado--%>
        <span class="SEPSLabel">Nivel de cuidado:</span>
            <asp:RequiredFieldValidator ID="rfvNivelMental" CssClass="rightFloatAsterisk" runat="server" InitialValue="0" Display="Dynamic"  ControlToValidate="ddlNivelMental" ErrorMessage="Nivel de cuidado del episodio anterior de salud mental" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*" />
        <div class="expandibleDiv">
        <asp:DropDownList CssClass="form-control" ID="ddlNivelMental" runat="server" DataSource="<%# dsPerfil %>" DataTextField="DE_SaludMental" DataValueField="PK_SaludMental" DataMember="SA_LKP_SALUD_MENTAL_ANTERIOR" />
        <asp:Label ID="lblNivelMental" runat="server" />
    
    </div>
    </div>
</div>
  </div>
</div>

<div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Violencia doméstica</h3>
  </div>
  <div class="panel-body">
 
        <div class="row">
            <div class="col-md-6 SEPSDivs"><%--Existe historial de ideas suicidas?--%>
                <span class="SEPSLabel">¿Existe historial de ideas suicidas?:</span>
                <asp:Label ID="lblIdeaSuicida" runat="server" />
                <asp:RequiredFieldValidator ID="rfvIdeaSuicida" runat="server" CssClass="rightFloatAsterisk"  Display="Dynamic" ControlToValidate="ddlIdeaSuicida" ErrorMessage="¿Existe historial de ideas suicidas?" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*" />
                <div class="expandibleDiv">
                <asp:DropDownList  CssClass="form-control" ViewStateMode="Enabled" EnableViewState="true" ID="ddlIdeaSuicida" runat="server">
                <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="1">Sí</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                    <asp:ListItem Value="94">No recuerda</asp:ListItem>
                    <asp:ListItem Value="96">No informó</asp:ListItem>
                </asp:DropDownList>
    </div>
            </div>
            <div class="col-md-6 SEPSDivs"><%--Existe historial de intentos de suicidios?--%>
                <span class="SEPSLabel">¿Existe historial de intentos de suicidios?:</span>
                 <asp:RequiredFieldValidator ID="rfvSuicidios" runat="server" CssClass="rightFloatAsterisk"  Display="Dynamic" ControlToValidate="ddlSuicidios" ErrorMessage="¿Existe historial de intentos de suicidios?" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*" />
                <div class="expandibleDiv">
                <asp:DropDownList  CssClass="form-control" ID="ddlSuicidios" runat="server">
                    <asp:ListItem />
                    <asp:ListItem Value="1">Sí</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                    <asp:ListItem Value="94">No recuerda</asp:ListItem>
                    <asp:ListItem Value="96">No informó</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblSuicidios" runat="server" />
               
                    </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 SEPSDivs"><%--Existe historial de maltrato en la niñez?--%>
                <span class="SEPSLabel">¿Existe historial de maltrato en la niñez?:</span>
                 <asp:RequiredFieldValidator ID="rfvMaltratoNinez" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic"  ControlToValidate="ddlMaltratoNinez" ErrorMessage="¿Existe historial de maltrato en la niñez?" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*" />
                <div class="expandibleDiv">
                <asp:DropDownList CssClass="form-control"  ID="ddlMaltratoNinez" runat="server" AutoPostBack="True" OnChange="return ddlMaltratoNinez();" OnSelectedIndexChanged="ddlMaltratoNinez_SelectedIndexChanged">
                    <asp:ListItem />
                    <asp:ListItem Value="1">Sí</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                    <asp:ListItem Value="94">No recuerda</asp:ListItem>
                    <asp:ListItem Value="96">No informó</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblMaltratoNinez" runat="server" />
               
        </div>
            </div>
        </div>
        <div style="height:160px;" runat="server" id="divMaltrato"> <%--Multiple seleccion - Maltrato--%>
            <div class="multipleLeft"><%--Tipo de maltrato--%>
                <span class="SEPSLabel">Indique el tipo de maltrato:</span>
                <asp:ListBox CssClass="form-control"  ID="lbxMaltratoSeleccion" runat="server" Height="130px"/>
            </div>
            <div class="multipleCenter text-center"><%--Buttons--%>
                <div style="height:60px;"></div>
                <div class="btn-group center" role="group">
                    <asp:Button ID="Button2" runat="server" CssClass="btn btn-default" CausesValidation="False" OnClick="lbxMaltrato_ClickD" Text="<" />
                    <asp:Button ID="Button1" runat="server"  CssClass="btn btn-default" CausesValidation="False" OnClick="lbxMaltrato_ClickA"  Text=">" />
                </div>
            </div>
            <div class="multipleRight">  <%--Listbox right--%>   
                <span class="SEPSLabel">Listado de tipos de maltrato (Seleccionados):</span>
                <asp:ListBox CssClass="form-control"  ID="lbxMaltratoSeleccionado" runat="server" Height="130px"/>
            </div>
        </div>      
        <div class="row" runat="server" id="divLblMaltrato">
            <div class="col-xs-12">
                <span class="SEPSLabel">Tipos de maltrato:</span>
                <asp:Label ID="lblMaltrato" runat="server"/>
            </div>
        </div>
 
<div class="row">
    <div class="col-md-12 SEPSDivs"><%--Ha sido victima de violencia doméstica?--%>
        <span class="SEPSLabel">¿Ha sido victima de violencia doméstica? [violencia de género que sucede en personas que son o fueron pareja, y entre las que existió una relación consensual, Ley num. 54]:</span>
                <asp:RequiredFieldValidator ID="rfvVioDomestic" runat="server" CssClass="rightFloatAsterisk"  Display="Dynamic" ControlToValidate="ddlVioDomestic"  ErrorMessage="¿Ha sido victima de violencia doméstica?" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>

        <div class="expandibleDiv">
        <asp:DropDownList CssClass="form-control" ID="ddlVioDomestic" runat="server">
            <asp:ListItem ></asp:ListItem>
            <asp:ListItem Value="1">Sí</asp:ListItem>
            <asp:ListItem Value="2">No</asp:ListItem>
            <asp:ListItem Value="94">No recuerda</asp:ListItem>
            <asp:ListItem Value="96">No informó</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="lblVioDomestic" runat="server" />
            </div>
    </div>
</div>
<%--
<div class="row">
    <div class="col-md-6 SEPSDivs">
        <span class="SEPSLabel">¿Ha participado en reuniones de grupos de apoyo, auto-ayuda, religiosas o ha buscado ayuda a su tratamiento de familiares, amigos u otros durante los pasados 30 días?:</span>
    </div>
    <div class="clearfix visible-xs-block"></div>
    <div class="clearfix visible-sm-block"></div>    
    <div class="col-md-6 SEPSDivs"><%--Ha participado en reuniones de grupos de apoyo, auto-ayuda, religiosas o ha buscado ayuda a su tratamiento de familiares, amigos u otros durante los pasados 30 días?
         <asp:RequiredFieldValidator ID="rfvReunionesGrupos" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic"  ControlToValidate="ddlReunionesGrupos" ErrorMessage="¿Ha participado en reuniones de grupos de apoyo, auto-ayuda, religiosas o ha buscado ayuda a su tratamiento de familiares, amigos u otros durante los pasados 30 días?" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
        <div class="expandibleDiv">
        <asp:DropDownList CssClass="form-control" ID="ddlReunionesGrupos" runat="server" OnLoad="ddlReunionesGrupos_Load" onChange="ddlReunionesGrupos();">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem Value="1">Sí</asp:ListItem>
            <asp:ListItem Value="2">No</asp:ListItem>
            <asp:ListItem Value="94">No recuerda</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="lblReunionesGrupos" runat="server" />
        </div>
       
    </div>
</div> 
--%>
<div class="row">
    <div class="col-md-6 SEPSDivs">
        <span class="SEPSLabel">¿Cuántas veces ha participado de reuniones de grupos de auto-ayuda durante los pasados 30 días como apoyo a su proceso de recuperación? (SU-NOM, incluye alcohólicos anónimos, narcóticos anónimos, programas pares etc.)</span>
    </div>
    <div class="clearfix visible-xs-block"></div>
    <div class="clearfix visible-sm-block"></div>
    <div class="col-md-6 SEPSDivs"><%--¿Cuántas veces ha participado de reuniones de grupo de apoyo, de auto-ayuda, religiosos o ha buscado ayuda de familiares, amigos u otros durante los pasados 30 días como apoyo a su proceso de recuperación?--%>
        <asp:RequiredFieldValidator ID="rfvFreq_AutoAyuda" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic"  ControlToValidate="ddlFreq_AutoAyuda" ErrorMessage="¿Cuántas veces ha participado de reuniones de grupo de apoyo, de auto-ayuda, religiosos o ha buscado ayuda de familiares, amigos u otros durante los pasados 30 días como apoyo a su proceso de recuperación?" ToolTip="Seleccione un valor de la lista. Este campo es requerido" Text="*"/>
        <div class="expandibleDiv">
            <asp:DropDownList  CssClass="form-control" ID="ddlFreq_AutoAyuda" runat="server"  DataSource="<%# dvwFreqAutoAyuda %>" DataTextField="DE_FreqAutoAyuda" DataValueField="PK_FreqAutoAyuda" AppendDataBoundItems="true" onChange="ddlFreq_AutoAyuda();">
            <asp:ListItem></asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lblFreq_AutoAyuda" runat="server" />
        </div>
    </div>
</div>
  </div>
</div>

<div id="DSMIV_DIV" name="DSMIV_DIV" runat="server">
    <div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Diagnóstico DSM 4</h3>
  </div>
  <div class="table-panel-body">
    <a class="btn btn-default" data-toggle="collapse" id="dsmiv_showContentButton" href="#dsmiv_content" onclick="dsmivShowHideClick();" aria-expanded="false" aria-controls="dsmiv_content">Mostrar contenido</a>
    <div class="collapse" id="dsmiv_content">
        <h3>I. Transtornos clínicos:</h3>
        <table class="table table-striped table-hover">
            <tr>
                <th></th>
                <th><span class="SEPSLabel">Diagnóstico</span></th>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Primario</span></td>
                <td>
                    <asp:Label ID="lblClinPrim" runat="server" />
                </td>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Secundario</span></td>
                <td>
                    <asp:Label ID="lblClinSec" runat="server" />
                </td>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Terciario</span></td>
                <td>
                    <asp:Label ID="lblClinTerc" runat="server" />
                </td>
            </tr>
        </table>
        <h3>II. Trastornos de la personalidad y RM:</h3>
        <table class="table table-striped table-hover">
            <tr>
                <th></th>
                <th><span class="SEPSLabel">Diagnóstico</span></th>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Primario</span></td>
                <td>
                    <asp:Label ID="lblRMPrim" runat="server" />
                </td>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Secundario</span></td>
                <td>
                    <asp:Label ID="lblRMSec" runat="server" />
                </td>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Terciario</span></td>
                <td>
                    <asp:Label ID="lblRMTerc" runat="server" />
                </td>
            </tr>
        </table>
        <h3>III. Condiciones médicas generales:</h3>
        <table class="table table-striped table-hover">
            <tr>
                <th></th>
                <th><span class="SEPSLabel">Diagnóstico</span></th>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Primario</span></td>
                <td>
                    <asp:Label ID="lblIIIP" runat="server" />
                </td>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Secundario</span></td>
                <td>
                    <asp:Label ID="lblIIIS" runat="server" />
                </td>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Terciario</span></td>
                <td>
                    <asp:Label ID="lblIIIT" runat="server" />
                </td>
            </tr>
        </table>
        <h3>IV. Problemas psicosociales y ambientales:</h3>
        <table class="table table-striped table-hover">
            <tr>
                <th></th>
                <th><span class="SEPSLabel">Diagnóstico</span></th>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Primario</span></td>
                <td>
                    <asp:Label ID="lblIVPrim" runat="server" />
                </td>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Secundario</span></td>
                <td>
                    <asp:Label ID="lblIVSec" runat="server" />
                </td>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Terciario</span></td>
                <td>
                    <asp:Label ID="lblIVTerc" runat="server" />
                </td>
            </tr>
        </table>
        <h3>V. Escala C-GAS / GAF:</h3>
        <table class="table table-striped table-hover">
            <tr>
                <th></th>
                <th><span class="SEPSLabel">Diagnóstico</span></th>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Primario</span></td>
                <td>
                    <asp:Label ID="lblEscalaGAF" runat="server" />
                </td>
            </tr>
        </table>
        <h3>Diagnóstico dual:</h3>
        <table class="table table-striped table-hover">
            <tr>
                <th><span class="SEPSLabel">Diagnóstico dual</span></th>
                <th><asp:Label ID="lblDual" runat="server"></asp:Label></th>
            </tr>
        </table>
    </div>
  </div>
</div>
</div>


<div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Diagnóstico</h3>
  </div>

     <div class="table-panel-body" id="DSMVRM_DIV" name="DSMVRM_DIV" runat="server">
        <table class="table table-striped table-hover">
            <tr>
                <th style="width:250px;">Diagnóstico DSM-5 (Antiguo Personalidad y RM)</th>
                <th><span class="SEPSLabel">Diagnóstico primario</span></th>
                <th><span class="SEPSLabel">Diagnóstico secundario</span></th>
                <th><span class="SEPSLabel">Diagnóstico terciario</span></th>
            </tr>
            <tr>
                <th><span class="SEPSLabel">Trastornos de la personalidad y RM</span></th>
                <td>

                    <asp:RequiredFieldValidator ID="rfvDSMVRMPrim" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="txtDSMVRMPrim" ErrorMessage="Trastornos de la personalidad y RM Primario" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
                    <div class="expandibleDiv">
                        <TextArea class="form-control"  ID="txtDSMVRMPrim" TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;" ReadOnly="readonly" >No se recopila la información</TextArea>
                        <asp:Label ID="lblDSMVRMPrim" runat="server" />
                        <asp:HyperLink ID="hlDSMVRMPrim" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showDSMV('mainBodyContent_WucEpisodioAdmision_txtDSMVRMPrim', 'mainBodyContent_WucEpisodioAdmision_hDSMVRMPrim', 'WucEpisodioAdmision')">Buscar...</asp:HyperLink>
                        <input id="hDSMVRMPrim" type="hidden" value="761" name="hDSMVRMPrim" runat="server"/>
                    </div>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvDSMVRMSec" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="txtDSMVRMSec" ErrorMessage="Trastornos de la personalidad y RM Secundario" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
                    <div class="expandibleDiv">
                        <TextArea class="form-control"  ID="txtDSMVRMSec" TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;" ReadOnly="readonly" >No se recopila la información</TextArea>
                        <asp:Label ID="lblDSMVRMSec" runat="server" />
                        <asp:HyperLink ID="hlDSMVRMSec" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showDSMV('mainBodyContent_WucEpisodioAdmision_txtDSMVRMSec', 'mainBodyContent_WucEpisodioAdmision_hDSMVRMSec', 'WucEpisodioAdmision')">Buscar...</asp:HyperLink>
                        <input id="hDSMVRMSec" type="hidden" value="761" name="hDSMVRMSec" runat="server" />
                    </div>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvDSMVRMTer" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="txtDSMVRMTer" ErrorMessage="Trastornos de la personalidad y RM Terciario" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
                    <div class="expandibleDiv">
                        <TextArea class="form-control"  ID="txtDSMVRMTer"  TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;"  ReadOnly="readonly" >No se recopila la información</TextArea>
                        <asp:Label ID="lblDSMVRMTer" runat="server" />
                        <asp:HyperLink ID="hlDSMVRMTer" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showDSMV('mainBodyContent_WucEpisodioAdmision_txtDSMVRMTer', 'mainBodyContent_WucEpisodioAdmision_hDSMVRMTer', 'WucEpisodioAdmision')">Buscar...</asp:HyperLink>
                        <input id="hDSMVRMTer" type="hidden" value="761" name="hDSMVRMTer" runat="server"/>
                    </div>
                </td>
            </tr>
            <tr>
                <th><span class="SEPSLabel">Problemas psicosociales y ambientales</span></th>
                <td>
                    <asp:DropDownList CssClass="form-control" ID="ddlDSMVPsicoAmbiPrim" runat="server" DataSource="<%# dvw_DSMV_ProblemasPsicosocialesAmbientales1 %>" DataTextField="DE_DSMV_ProblemasPsicosocialesAmbientales" DataValueField="PK_DSMV_ProblemasPsicosocialesAmbientales"  onChange="ddlDSMVPsicoAmbiPrim()" />
                    <asp:Label ID="lblDSMVPsicoAmbiPrim" runat="server" />
                </td>
                <td>
                    <asp:DropDownList CssClass="form-control" ID="ddlDSMVPsicoAmbiSec" runat="server"  DataSource="<%# dvw_DSMV_ProblemasPsicosocialesAmbientales2 %>" DataTextField="DE_DSMV_ProblemasPsicosocialesAmbientales" DataValueField="PK_DSMV_ProblemasPsicosocialesAmbientales" onChange="ddlDSMVPsicoAmbiSec()"/>
                    <asp:Label ID="lblDSMVPsicoAmbiSec" runat="server"/>
                </td>
                <td>
                    <asp:DropDownList CssClass="form-control" ID="ddlDSMVPsicoAmbiTer" runat="server"  DataSource="<%# dvw_DSMV_ProblemasPsicosocialesAmbientales3 %>" DataTextField="DE_DSMV_ProblemasPsicosocialesAmbientales" DataValueField="PK_DSMV_ProblemasPsicosocialesAmbientales"/>
                    <asp:Label ID="lblDSMVPsicoAmbiTer" runat="server"/>
                </td>
            </tr>
             <tr>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
                <th>&nbsp;</th>
            </tr>
        </table>
     </div>

  <div class="table-panel-body">
    <table class="table table-striped table-hover">
    <tr>
        <th style="width:250px;">Diagnóstico DSM-5</th>
        <th><span class="SEPSLabel">Diagnóstico primario</span></th>
        <th><span class="SEPSLabel">Diagnóstico secundario</span></th>
        <th><span class="SEPSLabel">Diagnóstico terciario</span></th>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Salud Mental [TEDS]</span></th>
        <td> 
            <asp:RequiredFieldValidator ID="rfvDSMVClinPrim" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic" InitialValue="0" ControlToValidate="txtDSMVClinPrim" ErrorMessage="Eje I. Diagnóstico Primario" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
            <div class="expandibleDiv">
                <TextArea class="form-control"  ID="txtDSMVClinPrim" TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;"  ReadOnly="readonly" >No se recopila la información</TextArea>
                <asp:Label ID="lblDSMVClinPrim" runat="server" />
                <asp:HyperLink ID="hlDSMVClinPrim" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showDSMV('mainBodyContent_WucEpisodioAdmision_txtDSMVClinPrim', 'mainBodyContent_WucEpisodioAdmision_hDSMVClinPrim', 'WucEpisodioAdmision')">Buscar...</asp:HyperLink>
                <input id="hDSMVClinPrim" type="hidden" value="761" name="hDSMVClinPrim" runat="server"/>
            </div>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="rfvDSMVClinSec" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="txtDSMVClinSec" ErrorMessage="Eje I. Diagnóstico Secundarioario" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
            <div class="expandibleDiv">
                <TextArea  class="form-control" ID="txtDSMVClinSec" onChange="txtClinSec()" TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;"  ReadOnly="readonly" >No se recopila la información</TextArea>
                <asp:Label ID="lblDSMVClinSec" runat="server" />
                <asp:HyperLink ID="hlDSMVClinSec" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showDSMV('mainBodyContent_WucEpisodioAdmision_txtDSMVClinSec', 'mainBodyContent_WucEpisodioAdmision_hDSMVClinSec', 'WucEpisodioAdmision')">Buscar...</asp:HyperLink>
                <input id="hDSMVClinSec" type="hidden" value="761" name="hDSMVClinSec" runat="server"/>
            </div>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="rfvDSMVClinTer" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="txtDSMVClinTer" ErrorMessage="Eje I. Diagnóstico Terciario" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
            <div class="expandibleDiv">
                <TextArea class="form-control"  ID="txtDSMVClinTer" onChange="txtClinTer()" TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;"  ReadOnly="readonly" >No se recopila la información</TextArea>
                <asp:Label ID="lblDSMVClinTer" runat="server"/>
                <asp:HyperLink ID="hlDSMVClinTer" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showDSMV('mainBodyContent_WucEpisodioAdmision_txtDSMVClinTer', 'mainBodyContent_WucEpisodioAdmision_hDSMVClinTer', 'WucEpisodioAdmision')">Buscar...</asp:HyperLink>
                <input id="hDSMVClinTer" type="hidden" value="761" name="hDSMVClinTer" runat="server"/>
            </div>
        </td>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Sustancias [TEDS]</span></th>
        <td> 
            <TextArea ID="txtDSMVSusPrim" class="form-control" TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;"  ReadOnly="readonly" >No se recopila la información</TextArea>
            <asp:Label ID="lblDSMVSusPrim" runat="server"/>
            <asp:HyperLink ID="hlDSMVSusPrim" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showSusDSMV('mainBodyContent_WucEpisodioAdmision_txtDSMVSusPrim', 'mainBodyContent_WucEpisodioAdmision_hDSMVSusPrim', 'WucEpisodioAdmision')" Text="Buscar..."/>
            <input id="hDSMVSusPrim" type="hidden" value="761" name="hDSMVSusPrim" runat="server" />
        </td>
        <td>
            <TextArea ID="txtDSMVSusSec" class="form-control" onChange="txtSusSec()" TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;"  ReadOnly="readonly" >No se recopila la información</TextArea>
            <asp:Label ID="lblDSMVSusSec" runat="server"/>
            <asp:HyperLink ID="hlDSMVSusSec" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showSusDSMV('mainBodyContent_WucEpisodioAdmision_txtDSMVSusSec', 'mainBodyContent_WucEpisodioAdmision_hDSMVSusSec', 'WucEpisodioAdmision')" Text="Buscar..."/>
            <input id="hDSMVSusSec" type="hidden" value="761" name="hDSMVSusSec" runat="server" />
        </td>
        <td>
            <TextArea ID="txtDSMVSusTer" class="form-control" onChange="txtSusTer()" TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;"  ReadOnly="readonly" >No se recopila la información</TextArea>
            <asp:Label ID="lblDSMVSusTer" runat="server"/>
            <asp:HyperLink ID="hlDSMVSusTer" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showSusDSMV('mainBodyContent_WucEpisodioAdmision_txtDSMVSusTer', 'mainBodyContent_WucEpisodioAdmision_hDSMVSusTer', 'WucEpisodioAdmision')" Text="Buscar..."/>
            <input id="hDSMVSusTer" type="hidden" value="761" name="hDSMVSusTer" runat="server" />
        </td>
    </tr>
    <%--
    <tr>
        <th><span class="SEPSLabel">Trastornos de la personalidad y RM</span></th>
        <td>

            <asp:RequiredFieldValidator ID="rfvDSMVRMPrim" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="txtDSMVRMPrim" ErrorMessage="Trastornos de la personalidad y RM Primario" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
            <div class="expandibleDiv">
                <TextArea class="form-control"  ID="txtDSMVRMPrim" TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;" ReadOnly="readonly" >No se recopila la información</TextArea>
                <asp:Label ID="lblDSMVRMPrim" runat="server" />
                <asp:HyperLink ID="hlDSMVRMPrim" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showDSMV('mainBodyContent_WucEpisodioAdmision_txtDSMVRMPrim', 'mainBodyContent_WucEpisodioAdmision_hDSMVRMPrim', 'WucEpisodioAdmision')">Buscar...</asp:HyperLink>
                <input id="hDSMVRMPrim" type="hidden" value="761" name="hDSMVRMPrim" runat="server"/>
            </div>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="rfvDSMVRMSec" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="txtDSMVRMSec" ErrorMessage="Trastornos de la personalidad y RM Secundario" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
            <div class="expandibleDiv">
                <TextArea class="form-control"  ID="txtDSMVRMSec" TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;" ReadOnly="readonly" >No se recopila la información</TextArea>
                <asp:Label ID="lblDSMVRMSec" runat="server" />
                <asp:HyperLink ID="hlDSMVRMSec" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showDSMV('mainBodyContent_WucEpisodioAdmision_txtDSMVRMSec', 'mainBodyContent_WucEpisodioAdmision_hDSMVRMSec', 'WucEpisodioAdmision')">Buscar...</asp:HyperLink>
                <input id="hDSMVRMSec" type="hidden" value="761" name="hDSMVRMSec" runat="server" />
            </div>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="rfvDSMVRMTer" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="txtDSMVRMTer" ErrorMessage="Trastornos de la personalidad y RM Terciario" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
            <div class="expandibleDiv">
                <TextArea class="form-control"  ID="txtDSMVRMTer"  TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;"  ReadOnly="readonly" >No se recopila la información</TextArea>
                <asp:Label ID="lblDSMVRMTer" runat="server" />
                <asp:HyperLink ID="hlDSMVRMTer" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showDSMV('mainBodyContent_WucEpisodioAdmision_txtDSMVRMTer', 'mainBodyContent_WucEpisodioAdmision_hDSMVRMTer', 'WucEpisodioAdmision')">Buscar...</asp:HyperLink>
                <input id="hDSMVRMTer" type="hidden" value="761" name="hDSMVRMTer" runat="server"/>
            </div>
        </td>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Problemas psicosociales y ambientales</span></th>
        <td>
            <asp:DropDownList CssClass="form-control" ID="ddlDSMVPsicoAmbiPrim" runat="server" DataSource="<%# dvw_DSMV_ProblemasPsicosocialesAmbientales1 %>" DataTextField="DE_DSMV_ProblemasPsicosocialesAmbientales" DataValueField="PK_DSMV_ProblemasPsicosocialesAmbientales"  onChange="ddlDSMVPsicoAmbiPrim()" />
            <asp:Label ID="lblDSMVPsicoAmbiPrim" runat="server" />
        </td>
        <td>
            <asp:DropDownList CssClass="form-control" ID="ddlDSMVPsicoAmbiSec" runat="server"  DataSource="<%# dvw_DSMV_ProblemasPsicosocialesAmbientales2 %>" DataTextField="DE_DSMV_ProblemasPsicosocialesAmbientales" DataValueField="PK_DSMV_ProblemasPsicosocialesAmbientales" onChange="ddlDSMVPsicoAmbiSec()"/>
            <asp:Label ID="lblDSMVPsicoAmbiSec" runat="server"/>
        </td>
        <td>
            <asp:DropDownList CssClass="form-control" ID="ddlDSMVPsicoAmbiTer" runat="server"  DataSource="<%# dvw_DSMV_ProblemasPsicosocialesAmbientales3 %>" DataTextField="DE_DSMV_ProblemasPsicosocialesAmbientales" DataValueField="PK_DSMV_ProblemasPsicosocialesAmbientales"/>
            <asp:Label ID="lblDSMVPsicoAmbiTer" runat="server"/>
        </td>
    </tr>
    --%>

    <tr>
        <th style="width:250px;">&nbsp;</th>
        <th style="width:250px;">&nbsp;</th>
        <th style="width:250px;">&nbsp;</th>
        <th style="width:250px;">&nbsp;</th>
    </tr>

    <tr>
        <th><span class="SEPSLabel">Comentarios</span></th>
        <td colspan="3">
            <asp:textbox CssClass="form-control" id="txtDSMVComentarios" runat="server" MaxLength="1500" TextMode="MultiLine" Width="100%" Height="64px"/>
            <asp:label id="lblDSMVComentarios" runat="server"/>
        </td>
    </tr> 
    <tr>
        <th><span class="SEPSLabel">Medida de Funcionamiento Global [TEDS, opcional]</span></th>
        <td colspan="3">
            <asp:textbox CssClass="form-control" id="txtDSMVFnGlobal" runat="server" autocomplete="off" onBlur="validateGAF('WucEpisodioAdmision_txtDSMVFnGlobal')" MaxLength="3" Width="100%"/>
            <asp:label id="lblDSMVFnGlobal" runat="server"/>
        </td>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Otras observaciones</span></th>
        <td colspan="3">
            <asp:textbox CssClass="form-control" id="txtDSMVOtrasObs" runat="server" MaxLength="1500" TextMode="MultiLine" Width="100%" Height="64px"/>
            <asp:label id="lblDSMVOtrasObs" runat="server"/>
        </td>
    </tr>
        
         <tr>
        <th><span class="SEPSLabel">Diagnósticos concurrentes de salud mental y uso de sustancias [TEDS]</span></th>
              
        <td colspan="3">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rightFloatAsterisk"  Display="Dynamic" ControlToValidate="ddlDSMVDiagDual"  ErrorMessage="Perfil Concurrente" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
            <div class="expandibleDiv">
            <asp:DropDownList CssClass="form-control" ID="ddlDSMVDiagDual" runat="server">
                <asp:ListItem />
                <asp:ListItem Value="1">Sí</asp:ListItem>
                <asp:ListItem Value="2">No</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lblDSMVDiagDual" runat="server" />
                </div>
        </td>
    </tr>

    
    <%--<tr>
        <th><span class="SEPSLabel">Diagnósticos concurrentes de salud mental y uso de sustancias</span></th>
        <td colspan="3">
            <asp:DropDownList CssClass="form-control" ID="ddlDSMVDiagDual2" runat="server">
                <asp:ListItem ></asp:ListItem>
                <asp:ListItem Value="1">Sí</asp:ListItem>
                <asp:ListItem Value="2">No</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lblDSMVDiagDual2" runat="server" />
        </td>
    </tr>--%>
</table>
  </div>

</div>
<%-- Campo Agregado 12/2020 --%>
<div class="panel panel-default">
    <div class="panel-heading">
        <h3 class="panel-title">Utilización de tabaco o cigarrillo</h3>
    </div>
    <div class="panel-body">
       <div class="row">
            <div class="col-print-6 col-md-6 SEPSDivs">
                    <%-- Zona Geografica --%>
                    <span class="SEPSLabel">¿Ha fumado al menos 100 cigarrillos en toda su vida?:</span>
                    <asp:RequiredFieldValidator ID="rfvInFumado" Display="Dynamic" CssClass="rightFloatAsterisk" runat="server" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Fumado en su vida" ControlToValidate="ddlInFumado" Text="*" />
                    <div class="expandibleDiv">
                        <asp:DropDownList CssClass="form-control" ID="ddlInFumado" runat="server">
                            <asp:ListItem />
                            <%-- IN ZONA > EPISODIO --%>
                            <asp:ListItem Value="1">Si</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                            <asp:ListItem Value="3">Desconoce</asp:ListItem>
                            <asp:ListItem Value="4">No informó</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lblInFumado" runat="server" />
                    </div>
                </div>
            <div class="col-print-6 col-md-6 SEPSDivs">
                    <%-- Zona Geografica --%>
                    <span class="SEPSLabel">Si contesto si, ¿con que frecuencia fuma cigarrillos actualmente?:</span>
                    <asp:RequiredFieldValidator ID="rfvFrecuenciaFumado" Display="Dynamic" CssClass="rightFloatAsterisk" runat="server" ToolTip="Seleccione un valor de la lista. Este campo es requerido." ErrorMessage="Frecuencia de fumar" ControlToValidate="ddlFrecuenciaFumado" Text="*" />
                    <div class="expandibleDiv">
                        <asp:DropDownList CssClass="form-control" ID="ddlFrecuenciaFumado" runat="server">
                            <asp:ListItem />
                            <%-- IN ZONA > EPISODIO --%>
                            <asp:ListItem Value="1">Todos los días</asp:ListItem>
                            <asp:ListItem Value="2">Algunos días</asp:ListItem>
                            <asp:ListItem Value="3">Nunca</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lblFrecuenciaFumado" runat="server" />
                    </div>
                </div>
            <div class="col-print-6 col-md-6 SEPSDivs">
                    <%-- Codigo Postal --%>
                    <span class="SEPSLabel">¿Si fuma todos o algunos días, cuantos cigarrillos en promedio usted fuma en un día?:</span>
                    <asp:RegularExpressionValidator ID="revNrFumado" runat="server" ValidationExpression="^([0-9])+$" CssClass="rightFloatAsterisk" ToolTip="Debe ser un valor númerico" ErrorMessage="Cantidad de Cigarrillos Fumados" ControlToValidate="txtNrFumado" Text="*" />
                    <div class="expandibleDiv">
                        <asp:TextBox CssClass="form-control" ID="txtNrFumado" runat="server" MaxLength="5" />
                        <asp:Label ID="lblNrFumado" runat="server" />
                    </div>
                </div>
        </div>
    </div>
</div>

<div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Abuso de sustancias</h3>
  </div>
  <div class="table-panel-body">
    <table class="table table-striped table-hover">
    <tr>
        <th></th>
        <th><span class="SEPSLabel">Droga de uso primario</span></th>
        <th><span class="SEPSLabel">Droga de uso secundario</span></th>
        <th><span class="SEPSLabel">Droga de uso terciario</span></th>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Droga</span></th>
        <td><%--Diagnóstico Primario--%>
            <asp:RequiredFieldValidator ID="rfvDrogaPrim" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic" InitialValue="0" ControlToValidate="ddlDrogaPrim" ErrorMessage="Droga - Diagnóstico Primario" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*" />
            <div class="expandibleDiv">
                <div class="col-md-12">
                    <div class="row">
                        <asp:DropDownList TabIndex="1"  CssClass="form-control" ID="ddlDrogaPrim" runat="server" DataSource="<%# dvwDrogaPrim %>" DataTextField="DE_Sustancia" DataValueField="PK_Sustancia" onChange="ddlDrogaPrimF();"/>
                    </div>
                    <div class="row">
                        &nbsp;
                    </div>
                    <div class="row" id="Hogar_DIV" name="Hogar_DIV" runat="server">
                        <div class="col-md-4">
                            <span class="SEPSLabel">Nombre</span>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox CssClass="form-control" ID="txtDrogaPrim" runat="server"/>
                        </div>
                        
                    </div>
                </div>
                
                <asp:Label ID="lblDrogaPrim" runat="server" />
            </div>
        </td>
        <td><%--Diagnóstico Secundario--%>
            <asp:RequiredFieldValidator ID="rfvDrogaSec" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic"  InitialValue="0" ControlToValidate="ddlDrogaSec" ErrorMessage="Droga - Diagnóstico Secundario" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*" />
            <div class="expandibleDiv">
                <div class="col-md-12">
                    <div class="row">
                        <asp:DropDownList  TabIndex="5" CssClass="form-control" ID="ddlDrogaSec" runat="server" DataSource="<%# dvwDrogaSec %>" DataTextField="DE_Sustancia" DataValueField="PK_Sustancia" onChange="ddlDrogaSecF();"  />
                    </div>
                    <div class="row">
                        &nbsp;
                    </div>
                    <div class="row" id="Hogar2_DIV" name="Hogar2_DIV" runat="server">
                        <div class="col-md-4">
                            <span class="SEPSLabel">Nombre</span>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox CssClass="form-control" ID="txtDrogaSec" runat="server"/>
                        </div>
                        
                    </div>
                </div>
                
                <asp:Label ID="lblDrogaSec" runat="server" />
            </div>
        </td>
        <td><%--Diagnóstico Terciario--%>
            <asp:RequiredFieldValidator ID="rfvDrogaTerc" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic" InitialValue="0" ControlToValidate="ddlDrogaTerc" ErrorMessage="Droga - Diagnóstico Terciario" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
            <div class="expandibleDiv">
                <div class="col-md-12">
                    <div class="row">
                        <asp:DropDownList  TabIndex="9" CssClass="form-control" ID="ddlDrogaTerc" runat="server" DataSource="<%# dvwDrogaTerc %>" DataTextField="DE_Sustancia" DataValueField="PK_Sustancia" onChange="ddlDrogaTercF();"/>
                    </div>
                    <div class="row">
                        &nbsp;
                    </div>
                    <div class="row" id="Hogar3_DIV" name="Hogar3_DIV" runat="server">
                        <div class="col-md-4">
                            <span class="SEPSLabel">Nombre</span>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox CssClass="form-control" ID="txtDrogaTerc" runat="server"/>
                        </div>
                        
                    </div>
                </div>
                
                <asp:Label ID="lblDrogaTerc" runat="server" />
            </div>
        </td>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Vía de utilización</span></th>
        <td><%--Diagnóstico Primario--%>
            <asp:RequiredFieldValidator ID="rfvViaPrim" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic"  InitialValue="0" ControlToValidate="ddlViaPrim" ErrorMessage="Vía de Utilización - Diagnóstico Primario" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*" />
            <div class="expandibleDiv">
                <asp:DropDownList  TabIndex="2"  CssClass="form-control" ID="ddlViaPrim" runat="server" DataSource="<%# dvwViaPrim %>" DataTextField="DE_ViaUtilizacion" DataValueField="PK_ViaUtilizacion" onChange="ddlViaPrimF();"/>
                <asp:Label ID="lblViaPrim" runat="server" />
            </div>
        </td>
        <td><%--Diagnóstico Secundario--%>
            <asp:RequiredFieldValidator ID="rfvViaSec" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic"  InitialValue="0" ControlToValidate="ddlViaSec" ErrorMessage="Vía de Utilización - Diagnóstico Secundario" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*" />
            <div class="expandibleDiv">
                <asp:DropDownList  TabIndex="6" CssClass="form-control" ID="ddlViaSec" runat="server" DataSource="<%# dvwViaSec %>" DataTextField="DE_ViaUtilizacion" DataValueField="PK_ViaUtilizacion" onChange="ddlViaSecF();"/>
                <asp:Label ID="lblViaSec" runat="server" />
            </div>
        </td>
        <td><%--Diagnóstico Terciario--%>
            <asp:RequiredFieldValidator ID="rfvViaTerc" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic"  InitialValue="0" ControlToValidate="ddlViaTerc" ErrorMessage="Vía de Utilización - Diagnóstico Terciario"   ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*" />
            <div class="expandibleDiv">
                <asp:DropDownList  TabIndex="10" CssClass="form-control" ID="ddlViaTerc" runat="server" DataSource="<%# dvwViaTerc %>" DataTextField="DE_ViaUtilizacion" DataValueField="PK_ViaUtilizacion" onChange="ddlViaTercF();"/>
                <asp:Label ID="lblViaTerc" runat="server" />
            </div>
        </td>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Frecuencia de uso</span></th>
        <td><%--Diagnóstico Primario--%>
            <asp:RequiredFieldValidator ID="rfvFrecPrim" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic"  InitialValue="0" ControlToValidate="ddlFrecPrim" ErrorMessage="Frecuencia de Uso - Diagnóstico Primario" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*" />
            <div class="expandibleDiv">
                <asp:DropDownList  TabIndex="3" CssClass="form-control" ID="ddlFrecPrim" runat="server" DataSource="<%# dvwFrecPrim %>" DataTextField="DE_Frecuencia" DataValueField="PK_Frecuencia" onChange="ddlFrecPrim();"/>
                <asp:Label ID="lblFrecPrim" runat="server" />
            </div>
        </td>
        <td><%--Diagnóstico Secundario--%>
            <asp:RequiredFieldValidator ID="rfvFrecSec" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic"  InitialValue="0" ControlToValidate="ddlFrecSec" ErrorMessage="Frecuencua de Uso - Diagnóstico Secundario"  ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*" />
            <div class="expandibleDiv">
                <asp:DropDownList  TabIndex="7" CssClass="form-control" ID="ddlFrecSec" runat="server" DataSource="<%# dvwFrecSec %>" DataTextField="DE_Frecuencia" DataValueField="PK_Frecuencia" onChange="ddlFrecSec();"/>
                <asp:Label ID="lblFrecSec" runat="server" />
            </div>
        </td>
        <td><%--Diagnóstico Terciario--%>
            <asp:RequiredFieldValidator ID="rfvFrecTerc" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic"  InitialValue="0" ControlToValidate="ddlFrecTerc" ErrorMessage="Frecuencia de Uso - Diagnóstico Terciario" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*" />
            <div class="expandibleDiv">
                <asp:DropDownList  TabIndex="11" CssClass="form-control"  ID="ddlFrecTerc" runat="server" DataSource="<%# dvwFrecTerc %>" DataTextField="DE_Frecuencia" DataValueField="PK_Frecuencia" onChange="ddlFrecTerc();"/>
                <asp:Label ID="lblFrecTerc" runat="server" />
            </div>
        </td>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Edad de inicio</span></th>
        <td><%--Diagnóstico Primario--%>
             <asp:RequiredFieldValidator ID="rfvEdadPrim" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" ControlToValidate="txtEdadPrim" ErrorMessage="Edad de inicio - Diagnóstico Primario" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
            <asp:RangeValidator ID="rvEdadPrim" runat="server" CssClass="rightFloatAsterisk" ControlToValidate="txtEdadPrim" ErrorMessage="Edad de inicio - Diagnóstico Primario" ToolTip="Escriba un número entero mayor o igual a cero (0) y menor que 99" Type="Integer" MaximumValue="99" MinimumValue="0" Display="Dynamic" Text="*"/>
            <div class="expandibleDiv">
                <asp:TextBox  TabIndex="4" CssClass="form-control"  ID="txtEdadPrim" runat="server" MaxLength="2"/>
                <asp:Label ID="lblEdadPrim" runat="server"/>
            </div>
        </td>
        <td><%--Diagnóstico Secundario--%>
            <asp:RequiredFieldValidator ID="rfvEdadSec" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" ControlToValidate="txtEdadSec" ErrorMessage="Edad inicio - Diagnóstico Secundario" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
            <asp:RangeValidator ID="rvEdadSec" runat="server" CssClass="rightFloatAsterisk" ControlToValidate="txtEdadSec" ErrorMessage="Edad inicio - Diagnóstico Secundario" ToolTip="Escriba un número entero mayor o igual a cero (0) y menor que 99" Type="Integer" MaximumValue="99" MinimumValue="0" Display="Dynamic" Text="*"/>
            <div class="expandibleDiv">
                <asp:TextBox  TabIndex="8" CssClass="form-control" ID="txtEdadSec" runat="server" MaxLength="2"/>
                <asp:Label ID="lblEdadSec" runat="server"/>
            </div>
        </td>
        <td><%--Diagnóstico Terciario--%>
            <asp:RequiredFieldValidator ID="rfvEdadTerc" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" ControlToValidate="txtEdadTerc" ErrorMessage="Edad inicio - Diagnóstico Terciario" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*" />
            <asp:RangeValidator ID="rvEdadTerc" runat="server" CssClass="rightFloatAsterisk" ControlToValidate="txtEdadTerc" ErrorMessage="Edad inicio - Diagnóstico Terciario" ToolTip="Escriba un número entero mayor o igual a cero (0) y menor que 99" Type="Integer" MaximumValue="99" MinimumValue="0" Display="Dynamic" Text="*"/>
            <div class="expandibleDiv">
                <asp:TextBox  TabIndex="12" CssClass="form-control" ID="txtEdadTerc" runat="server" MaxLength="2"/>
                <asp:Label ID="lblEdadTerc" runat="server"/>
            </div>
        </td>
    </tr>

<%-- Campo Agregado 12/2020 --%>
    <tr>
        <th><span class="SEPSLabel">Confirmado por toxicología</span></th>
        <td>
            <div class="expandibleDiv">
                <asp:DropDownList CssClass="form-control" ID="ddlToxicologia1" runat="server">
                    <asp:ListItem />
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                    <asp:ListItem Value="99">No Aplica</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblToxicologia1" runat="server" />
            </div>
        </td>
        <td>
            <div class="expandibleDiv">
                <asp:DropDownList CssClass="form-control" ID="ddlToxicologia2" runat="server">
                    <asp:ListItem />
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                    <asp:ListItem Value="99">No Aplica</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblToxicologia2" runat="server" />
            </div>
        </td>
        <td>
            <div class="expandibleDiv">
                <asp:DropDownList CssClass="form-control" ID="ddlToxicologia3" runat="server">
                    <asp:ListItem />
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Value="2">No</asp:ListItem>
                    <asp:ListItem Value="99">No Aplica</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblToxicologia3" runat="server" />
            </div>
        </td>
        
    </tr>
</table>
  </div>
</div>
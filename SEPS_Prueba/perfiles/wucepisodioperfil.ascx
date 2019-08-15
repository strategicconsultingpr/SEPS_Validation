<%@ Control Language="c#" Inherits="ASSMCA.Perfiles.wucEpisodioPerfil" CodeBehind="wucEpisodioPerfil.ascx.cs" %>
<input id="CO_Tipo" type="hidden" name="Hidden2" runat="server"/>
<input id="hNivelSM" type="hidden" name="Hidden6" runat="server"/>
<input id="hNivelAS" type="hidden" name="Hidden7" runat="server"/>

<div runat="server" id="NivelDiv" class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Nivel de cuidado de este episodio</h3>
  </div>
  <div class="panel-body">
   <div class="row">
    <div class="col-md-6"><%--Nivel de Cuidado Salud mental--%>
        <span class="SEPSLabel">Nivel de cuidado (Salud mental):</span>
        <asp:Label ID="lblNivelCuidadoSaludMental" runat="server"/>
        <input id="ddlNivelCuidadoSaludMentalHidden" type="hidden" name="Hidden1" runat="server" />
    </div>
    <div class="col-md-6"><%--Nivel de Cuidado Abuso de Sustancias--%>
        <span class="SEPSLabel">Nivel de cuidado (Abuso de Sustancias):</span>
        <asp:Label ID="lblNivelCuidadoSustancias" runat="server"/>
        <input id="ddlNivelCuidadoSustanciasHidden" type="hidden" name="Hidden3" runat="server" />
    </div>
</div>

  </div>
</div>

<div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Datos de salud general</h3>
  </div>
  <div class="panel-body">
                               
        <div style="height:160px;" runat="server" id="divCondicionesDiagnosticadas">
            <div class="multipleLeft"> <%-- Listbox left --%>
                <span class="SEPSLabel">Condiciones (Disponibles)</span>
                <asp:ListBox CssClass="form-control" ID="lbxCondicionesDiagnosticadasSeleccion" runat="server" Height="130px"/>
            </div>
            <div class="multipleCenter text-center"> <%-- Buttons --%>
                <div style="height:60px;"></div>
                <div class="btn-group" role="group">
                    <asp:Button ID="btnEliminarCondicionesDiagnosticadas" runat="server" CssClass="btn btn-default" CausesValidation="False" onclick="btnEliminarCondicionesDiagnosticadas_Click" Text="<"/>
                    <asp:Button ID="btnAgregarCondicionesDiagnosticadas" runat="server" CssClass="btn btn-default" CausesValidation="False" onclick="btnAgregarCondicionesDiagnosticadas_Click" Text=">"/>
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

<div id="DSMIV_DIV" runat="server">
    <div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Diagnóstico DSM 4</h3>
  </div>
  <div class="panel-body">
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
                    <asp:Label ID="lblClinPrim" runat="server"/>
                </td>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Secundario</span></td>
                <td>
                    <asp:Label ID="lblClinSec" runat="server"/>
                </td>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Terciario</span></td>
                <td>
                    <asp:Label ID="lblClinTerc" runat="server"/>
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
                    <asp:Label ID="lblRMPrim" runat="server"/>
                </td>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Secundario</span></td>
                <td>
                    <asp:Label ID="lblRMSec" runat="server"/>
                </td>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Terciario</span></td>
                <td>
                    <asp:Label ID="lblRMTerc" runat="server"/>
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
                    <asp:Label ID="lblIIIP" runat="server"/>
                </td>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Secundario</span></td>
                <td>
                    <asp:Label ID="lblIIIS" runat="server"/>
                </td>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Terciario</span></td>
                <td>
                    <asp:Label ID="lblIIIT" runat="server"/>
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
                    <asp:Label ID="lblIVPrim" runat="server"/>
                </td>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Secundario</span></td>
                <td>
                    <asp:Label ID="lblIVSec" runat="server"/>
                </td>
            </tr>
            <tr>
                <td><span class="SEPSLabel">Terciario</span></td>
                <td>
                    <asp:Label ID="lblIVTerc" runat="server"/>
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
                    <asp:Label ID="lblEscalaGAF" runat="server"/>
                </td>
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
  <div class="table-panel-body">
    <table class="table table-striped table-hover">
    <tr>
        <th style="width:250px;">&nbsp;</th>
        <th><span class="SEPSLabel">Diagnóstico primario</span></th>
        <th><span class="SEPSLabel">Diagnóstico secundario</span></th>
        <th><span class="SEPSLabel">Diagnóstico terciario</span></th>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Trastornos clínicos</span></th>
        <td> 
            <TextArea ID="txtDSMVClinPrim" class="form-control" TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;"  ReadOnly="readonly" >No se recopila la información</TextArea>
            <asp:Label ID="lblDSMVClinPrim" runat="server"/>
            <asp:HyperLink ID="hlDSMVClinPrim" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showDSMV('mainBodyContent_WucEpisodioPerfil_txtDSMVClinPrim', 'mainBodyContent_WucEpisodioPerfil_hDSMVClinPrim', 'WucEpisodioPerfil')" Text="Buscar..."/>
            <input id="hDSMVClinPrim" type="hidden" value="761" name="hDSMVClinPrim" runat="server" />
        </td>
        <td>
            <TextArea ID="txtDSMVClinSec" class="form-control" onChange="txtClinSec()" TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;"  ReadOnly="readonly" >No se recopila la información</TextArea>
            <asp:Label ID="lblDSMVClinSec" runat="server"/>
            <asp:HyperLink ID="hlDSMVClinSec" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showDSMV('mainBodyContent_WucEpisodioPerfil_txtDSMVClinSec', 'mainBodyContent_WucEpisodioPerfil_hDSMVClinSec', 'WucEpisodioPerfil')" Text="Buscar..."/>
            <input id="hDSMVClinSec" type="hidden" value="761" name="hDSMVClinSec" runat="server" />
        </td>
        <td>
            <TextArea ID="txtDSMVClinTer" class="form-control" onChange="txtClinTer()" TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;"  ReadOnly="readonly" >No se recopila la información</TextArea>
            <asp:Label ID="lblDSMVClinTer" runat="server"/>
            <asp:HyperLink ID="hlDSMVClinTer" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showDSMV('mainBodyContent_WucEpisodioPerfil_txtDSMVClinTer', 'mainBodyContent_WucEpisodioPerfil_hDSMVClinTer', 'WucEpisodioPerfil')" Text="Buscar..."/>
            <input id="hDSMVClinTer" type="hidden" value="761" name="hDSMVClinTer" runat="server" />
        </td>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Trastornos de la personalidad y RM</span></th>
        <td>
            <TextArea ID="txtDSMVRMPrim" class="form-control" TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;" ReadOnly="readonly" >No se recopila la información</TextArea>
            <asp:Label ID="lblDSMVRMPrim" runat="server"/>
            <asp:HyperLink ID="hlDSMVRMPrim" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showDSMV('mainBodyContent_WucEpisodioPerfil_txtDSMVRMPrim', 'mainBodyContent_WucEpisodioPerfil_hDSMVRMPrim', 'WucEpisodioPerfil')" Text="Buscar..."/>
            <input id="hDSMVRMPrim" type="hidden" value="761" name="hDSMVRMPrim" runat="server" />
        </td>
        <td>
            <TextArea ID="txtDSMVRMSec" class="form-control" TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;" ReadOnly="readonly" >No se recopila la información</TextArea>
            <asp:Label ID="lblDSMVRMSec" runat="server"/>
            <asp:HyperLink ID="hlDSMVRMSec" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showDSMV('mainBodyContent_WucEpisodioPerfil_txtDSMVRMSec', 'mainBodyContent_WucEpisodioPerfil_hDSMVRMSec', 'WucEpisodioPerfil')" Text="Buscar..."/>
            <input id="hDSMVRMSec" type="hidden" value="761" name="hDSMVRMSec" runat="server" />
        </td>
        <td>
            <TextArea ID="txtDSMVRMTer" class="form-control" TabIndex="-1" runat="server" style="min-height:50px;resize:vertical;" ReadOnly="readonly">No se recopila la información</TextArea>
            <asp:Label ID="lblDSMVRMTer" runat="server"/>
            <asp:HyperLink ID="hlDSMVRMTer" ForeColor="DarkGreen" runat="server" NavigateUrl="javascript:showDSMV('mainBodyContent_WucEpisodioPerfil_txtDSMVRMTer', 'mainBodyContent_WucEpisodioPerfil_hDSMVRMTer', 'WucEpisodioPerfil')" Text="Buscar..."/>
            <input id="hDSMVRMTer" type="hidden" value="761" name="hDSMVRMTer" runat="server" />
        </td>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Problemas psicosociales y ambientales</span></th>
        <td>
            <asp:DropDownList CssClass="form-control" ID="ddlDSMVPsicoAmbiPrim" runat="server" DataSource="<%# dvw_DSMV_ProblemasPsicosocialesAmbientales1 %>" DataTextField="DE_DSMV_ProblemasPsicosocialesAmbientales" DataValueField="PK_DSMV_ProblemasPsicosocialesAmbientales"  onChange="ddlDSMVPsicoAmbiPrim()"/>
            <asp:Label ID="lblDSMVPsicoAmbiPrim" runat="server"/>
        </td>
        <td>
            <asp:DropDownList CssClass="form-control" ID="ddlDSMVPsicoAmbiSec" runat="server"  DataSource="<%# dvw_DSMV_ProblemasPsicosocialesAmbientales2 %>" DataTextField="DE_DSMV_ProblemasPsicosocialesAmbientales" DataValueField="PK_DSMV_ProblemasPsicosocialesAmbientales" onChange="ddlDSMVPsicoAmbiSec()" />
            <asp:Label ID="lblDSMVPsicoAmbiSec" runat="server" />
        </td>
        <td>
            <asp:DropDownList CssClass="form-control" ID="ddlDSMVPsicoAmbiTer" runat="server" DataSource="<%# dvw_DSMV_ProblemasPsicosocialesAmbientales3 %>" DataTextField="DE_DSMV_ProblemasPsicosocialesAmbientales" DataValueField="PK_DSMV_ProblemasPsicosocialesAmbientales"/>
            <asp:Label ID="lblDSMVPsicoAmbiTer" runat="server" />
        </td>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Comentarios</span></th>
        <td colspan="3">
            <asp:textbox CssClass="form-control" id="txtDSMVComentarios" runat="server" MaxLength="1500" TextMode="MultiLine" Height="64px"/>
            <asp:label id="lblDSMVComentarios" runat="server"/>
        </td>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Diagnósticos concurrentes de salud mental y uso de sustancias</span></th>
        <td colspan="3">
            <asp:DropDownList CssClass="form-control" ID="ddlDSMVDiagDual" runat="server" onChange="ddlDSMVDiagDual('mainBodyContent_WucEpisodioPerfil_','ddlDSMVDiagDual');">
                <asp:ListItem />
                <asp:ListItem Value="1">Sí</asp:ListItem>
                <asp:ListItem Value="2">No</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lblDSMVDiagDual" runat="server" />
        </td>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Funcionamiento global</span></th>
        <td colspan="3">
            <asp:textbox CssClass="form-control" id="txtDSMVFnGlobal" runat="server" autocomplete="off" onBlur="validateGAF('WucEpisodioPerfil_txtDSMVFnGlobal')" MaxLength="3"/>
            <asp:label id="lblDSMVFnGlobal" runat="server"/>
        </td>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Otras observaciones</span></th>
        <td colspan="3">
            <asp:textbox CssClass="form-control" id="txtDSMVOtrasObs" runat="server" MaxLength="1500" TextMode="MultiLine" Height="64px"/>
            <asp:label id="lblDSMVOtrasObs" runat="server"/>
        </td>
    </tr>  
</table>
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
            <asp:RequiredFieldValidator ID="rfvDrogaPrim" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlDrogaPrim" ErrorMessage="Droga - Diagnóstico Primario" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
            <div class="expandibleDiv">
                <asp:DropDownList TabIndex="1"  CssClass="form-control" ID="ddlDrogaPrim" runat="server" DataSource="<%# dvwDrogaPrim %>" DataTextField="DE_Sustancia" DataValueField="PK_Sustancia" onChange="ddlDrogaPrimF();"/>
                <asp:Label ID="lblDrogaPrim" runat="server" />
            </div>
        </td>
        <td><%--Diagnóstico Secundario--%>
            <asp:RequiredFieldValidator ID="rfvDrogaSec" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlDrogaSec" ErrorMessage="Droga - Diagnóstico Secundario" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
            <div class="expandibleDiv">
                <asp:DropDownList  TabIndex="5"  CssClass="form-control" ID="ddlDrogaSec" runat="server" DataSource="<%# dvwDrogaSec %>" DataTextField="DE_Sustancia" DataValueField="PK_Sustancia" onChange="ddlDrogaSecF();"/>
                <asp:Label ID="lblDrogaSec" runat="server" />
            </div>
        </td>
        <td><%--Diagnóstico Terciario--%>
            <asp:RequiredFieldValidator ID="rfvDrogaTerc" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlDrogaTerc" ErrorMessage="Droga - Diagnóstico Terciario" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
            <div class="expandibleDiv">
                <asp:DropDownList   TabIndex="9"  CssClass="form-control" ID="ddlDrogaTerc" runat="server" DataSource="<%# dvwDrogaTerc %>" DataTextField="DE_Sustancia" DataValueField="PK_Sustancia" onChange="ddlDrogaTercF();"/>
                <asp:Label ID="lblDrogaTerc" runat="server" />
            </div>
        </td>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Vía de utilización</span></th>
        <td><%--Diagnóstico Primario--%>
            <asp:RequiredFieldValidator ID="rfvViaPrim" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlViaPrim" ErrorMessage="Vía de Utilización - Diagnóstico Primario" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*" />
            <div class="expandibleDiv">
                <asp:DropDownList  TabIndex="2"    CssClass="form-control" ID="ddlViaPrim" runat="server" DataSource="<%# dvwViaPrim %>" DataTextField="DE_ViaUtilizacion" DataValueField="PK_ViaUtilizacion" onChange="ddlViaPrimF();"/>
                <asp:Label ID="lblViaPrim" runat="server" />
            </div>
        </td>
        <td><%--Diagnóstico Secundario--%>
            <asp:RequiredFieldValidator ID="rfvViaSec" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlViaSec" ErrorMessage="Vía de Utilización - Diagnóstico Secundario" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
            <div class="expandibleDiv">
                <asp:DropDownList TabIndex="6" CssClass="form-control" ID="ddlViaSec" runat="server" DataSource="<%# dvwViaSec %>" DataTextField="DE_ViaUtilizacion" DataValueField="PK_ViaUtilizacion" onChange="ddlViaSecF();"/>
                <asp:Label ID="lblViaSec" runat="server" />
            </div>
        </td>
        <td><%--Diagnóstico Terciario--%>
            <asp:RequiredFieldValidator ID="rfvViaTerc" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlViaTerc" ErrorMessage="Vía de Utilización - Diagnóstico Terciario" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
            <div class="expandibleDiv">
                <asp:DropDownList   TabIndex="10"  CssClass="form-control" ID="ddlViaTerc" runat="server" DataSource="<%# dvwViaTerc %>" DataTextField="DE_ViaUtilizacion" DataValueField="PK_ViaUtilizacion" onChange="ddlViaTercF();"/>
                <asp:Label ID="lblViaTerc" runat="server" />
            </div>
        </td>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Frecuencia de uso</span></th>
        <td><%--Diagnóstico Primario--%>
            <asp:RequiredFieldValidator ID="rfvFrecPrim" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlFrecPrim" ErrorMessage="Frecuencia de Uso - Diagnóstico Primario" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
            <div class="expandibleDiv">
                <asp:DropDownList  TabIndex="3"   CssClass="form-control" ID="ddlFrecPrim" runat="server" DataSource="<%# dvwFrecPrim %>" DataTextField="DE_Frecuencia" DataValueField="PK_Frecuencia" onChange="ddlFrecPrim();"/>
                <asp:Label ID="lblFrecPrim" runat="server" />
            </div>
        </td>
        <td><%--Diagnóstico Secundario--%>       
            <asp:RequiredFieldValidator ID="rfvFrecSec" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlFrecSec" ErrorMessage="Frecuencua de Uso - Diagnóstico Secundario" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
            <div class="expandibleDiv">
                <asp:DropDownList   TabIndex="7"   CssClass="form-control" ID="ddlFrecSec" runat="server" DataSource="<%# dvwFrecSec %>" DataTextField="DE_Frecuencia" DataValueField="PK_Frecuencia" onChange="ddlFrecSec();"/>
                <asp:Label ID="lblFrecSec" runat="server" />
            </div>
        </td>
        <td><%--Diagnóstico Terciario--%>
            <asp:RequiredFieldValidator ID="rfvFrecTerc" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlFrecTerc" ErrorMessage="Frecuencia de Uso - Diagnóstico Terciario" ToolTip="Seleccione un valor de la lista. Este campo es requerido." Text="*"/>
            <div class="expandibleDiv">
                <asp:DropDownList   TabIndex="11"   CssClass="form-control" ID="ddlFrecTerc" runat="server" DataSource="<%# dvwFrecTerc %>" DataTextField="DE_Frecuencia" DataValueField="PK_Frecuencia" onChange="ddlFrecTerc();"/>
                <asp:Label ID="lblFrecTerc" runat="server" />
            </div>
        </td>
    </tr>
    <tr>
        <th><span class="SEPSLabel">Edad de inicio</span></th>
        <td><%--Diagnóstico Primario--%>        
            <asp:RequiredFieldValidator ID="rfvEdadPrim" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" ControlToValidate="txtEdadPrim" ErrorMessage="Edad de inicio - Diagnóstico Primario" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
            <asp:RangeValidator ID="rvEdadPrim" CssClass="rightFloatAsterisk" runat="server" ControlToValidate="txtEdadPrim" ErrorMessage="Edad de inicio - Diagnóstico Primario" ToolTip="Escriba un número entero mayor o igual a cero (0) y menor que 99" Type="Integer" MaximumValue="99" MinimumValue="0" Display="Dynamic" Text="*"/>
            <div class="expandibleDiv">
                <asp:TextBox   TabIndex="4"  CssClass="form-control" ID="txtEdadPrim" runat="server" MaxLength="2" />
                <asp:Label ID="lblEdadPrim" runat="server" />
            </div>
        </td>
        <td><%--Diagnóstico Secundario--%>
            <asp:RequiredFieldValidator ID="rfvEdadSec" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" ControlToValidate="txtEdadSec" ErrorMessage="Edad inicio - Diagnóstico Secundario" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*"/>
            <asp:RangeValidator ID="rvEdadSec" CssClass="rightFloatAsterisk" runat="server" ControlToValidate="txtEdadSec" ErrorMessage="Edad inicio - Diagnóstico Secundario" ToolTip="Escriba un número entero mayor o igual a cero (0) y menor que 99" Type="Integer" MaximumValue="99" MinimumValue="0" Display="Dynamic" Text="*" />
            <div class="expandibleDiv">
                <asp:TextBox   TabIndex="8"   CssClass="form-control" ID="txtEdadSec" runat="server" MaxLength="2" />
                <asp:Label ID="lblEdadSec" runat="server" />
            </div>
        </td>
        <td><%--Diagnóstico Terciario--%> 
            <asp:RequiredFieldValidator ID="rfvEdadTerc" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" ControlToValidate="txtEdadTerc" ErrorMessage="Edad inicio - Diagnóstico Terciario" ToolTip="Campo Requerido. Escriba un valor numerico." Text="*" />
            <asp:RangeValidator ID="rvEdadTerc" CssClass="rightFloatAsterisk" runat="server" ControlToValidate="txtEdadTerc" ErrorMessage="Edad inicio - Diagnóstico Terciario" ToolTip="Escriba un número entero mayor o igual a cero (0) y menor que 99" Type="Integer" MaximumValue="99"  MinimumValue="0" Display="Dynamic" Text="*"/>
            <div class="expandibleDiv">
                <asp:TextBox   TabIndex="12"  CssClass="form-control" ID="txtEdadTerc" runat="server" MaxLength="2" />
                <asp:Label ID="lblEdadTerc" runat="server" />
            </div>
        </td>
    </tr>
</table>
  </div>
</div>

<div id="divPracticasBasadasEnEvidencia" runat="server">
    <div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Prácticas basadas en evidencia</h3>
  </div>
  <div class="panel-body">
     <h3 id="h3PracticasBasadasEnEvidenciaNinoOAdulto" runat="server">Niños y adolescentes o adultos</h3>
                                
            <div style="height:160px;" runat="server" id="divLbxPracticasBasadasEvidencia"> <%-- List boxes - Practicas Basadas en Evidencia--%>
                <div class="multipleLeft"> <%-- Listbox left --%>
                    <span class="SEPSLabel">Prácticas (Disponibles)</span>
                    <asp:ListBox  CssClass="form-control" ID="lbxPracticasBasadasEvidenciaSeleccion" runat="server" Height="130px"/>
                </div>
                <div class="multipleCenter text-center"> <%-- Buttons --%>
                    <div style="height:60px;"></div>
                    <div class="btn-group" role="group">
                        <asp:Button ID="btnEliminarPracticasBasadasEvidencia" runat="server" CssClass="btn btn-default" CausesValidation="False" onclick="btnEliminarPracticasBasadasEvidencia_Click" Text="<" />
                        <asp:Button ID="btnAgregarPracticasBasadasEvidencia" runat="server" CssClass="btn btn-default" CausesValidation="False" onclick="btnAgregarPracticasBasadasEvidencia_Click" Text=">" />
                    </div>
                </div>
                <div class="multipleRight"> <%-- Listbox right --%>
                    <span class="SEPSLabel">Prácticas (Seleccionadas)</span>
                    <asp:ListBox CssClass="form-control" ID="lbxPracticasBasadasEvidenciaSeleccionado" runat="server" Height="130px"/>
                </div>
            </div>
              <div class="row" runat="server" id="divLblPracticasBasadasEvidencia">
            <div class="col-xs-12">
                <span class="SEPSLabel">Prácticas basadas en evidencia:</span>
                <asp:Label ID="lblPracticasBasadasEvidencia" runat="server"/>
            </div>
        </div>
 
  </div>
</div>  
</div>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="Perfiles_wucTakeHome" CodeBehind="wucTakeHome.ascx.cs" %>
<div id="divTakeHome" runat="server">
    <div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Take Home</h3>
  </div>
  <div class="panel-body">

            <div class="row">
                <div class="col-lg-6 SEPSDivs"><%--Participa actualmente en el programa "Take Home"--%>
                    <asp:RequiredFieldValidator runat="server"  ID="rfvTHBelong" ControlToValidate="ddlTHBelong" Display="Dynamic" CssClass="rightFloatAsterisk" Text="*" ErrorMessage="Participa de Take Home" />
                    <span class="SEPSLabel">Participa actualmente en el programa "Take Home":</span>
                    <div class="expandibleDiv">
                        <asp:DropDownList CssClass="form-control" ID="ddlTHBelong" runat="server" OnSelectedIndexChanged="ddlTHBelong_SelectedIndexChanged" onChange="TakeHomeParticipa();">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="1">Sí</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lblTHBelong" runat="server"/>
                    </div>
                </div>
        <div class="col-lg-6 SEPSDivs Participa"><%--Etapa--%>
            <span class="SEPSLabel">Fase:</span>
            <div class="expandibleDiv">
                <asp:DropDownList CssClass="form-control" ID="ddlTHEtapa" runat="server"/>
                <asp:Label ID="lblTHEtapa" runat="server"/>
            </div>
        </div>            </div>
            <div style="height:160px;" class="NoParticipa" runat="server" id="divRazon">
                          <asp:UpdatePanel ID="updTH" runat="server">
        <ContentTemplate>
                <div class="multipleLeft">  
                    <span class="SEPSLabel">Listado de razones (Disponibles)</span>     
                    <asp:ListBox CssClass="form-control"  ID="lbxRazonSeleccion" runat="server" EnableViewState ="true" Height="130px"/>
                </div>
                <div class="multipleCenter text-center">
                    <div style="height:60px;"></div>
                    <div class="btn-group" role="group">                     
                        <asp:Button ID="btnEliminar" runat="server" Text="<"  CssClass="btn btn-default"  CausesValidation="False" OnClick="btnEliminar_Click"/>  
                        <asp:Button ID="btnAgregar" runat="server" Text=">"  CssClass="btn btn-default" CausesValidation="False" OnClick="btnAgregar_Click"/>                 
                    </div>                          
                </div>
                    <asp:CustomValidator ID="cvRazonesNoParticipa" Display="Dynamic" CssClass="rightFloatAsterisk" runat="server" ClientValidationFunction="cvTakeHomeRazonesNoParticipaValidation" ErrorMessage="Razón de no participar del Take Home" Text="*"/>
                <div class="multipleRight"><%--Buttons--%>   
                    <span class="SEPSLabel">Listado de razones (Seleccionadas)</span>                

                    <asp:ListBox CssClass="form-control"  ID="lbxRazonSeleccionado" runat="server" EnableViewState ="true" CausesValidation="true" Height="130px" DataValueField="PK_" DataTextField="DE_"/>       
                </div>
                        </ContentTemplate>
    </asp:UpdatePanel>
            </div> 
            <div class="row NoParticipa" runat="server" id="divLblRazon">
                <div class="col-xs-12 SEPSDivsInfo">
                    <span class="SEPSLabel">Razón de no participar:</span>
                    <asp:Label ID="lblRazon" runat="server"/>
                </div>
            </div>      

    <div class="row Participa">
        <div class="col-lg-12 SEPSDivs"><%--Fecha de Entrada--%>
            <span class="SEPSLabel">Fecha de comienzo TH:</span>
            <div class="leftFloat">
                <asp:DropDownList  CssClass="form-control" ID="ddlFechaEntradaMes" runat="server" onChange="ddlMesNuevo('WucTakeHome_','ddlFechaEntradaDía','ddlFechaEntradaMes')">
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
                <asp:DropDownList CssClass="form-control" ID="ddlFechaEntradaDía" runat="server" onChange="ddlDíaNuevo('WucTakeHome_','ddlFechaEntradaMes','ddlFechaEntradaDía')">
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
                <asp:TextBox CssClass="form-control" ID="txtFechaEntradaAño" runat="server" Width="80px" MaxLength="4" onBlur="TakeHomeFechaEntrada()"/>
                <asp:Label ID="lblFE_In" runat="server"/>
            </div>
        </div>        
        <div class="col-lg-12 SEPSDivs"><%--Fecha de Salida--%>
            <span class="SEPSLabel">Fecha de terminación en TH:</span>
            <div class="leftFloat">
                <asp:DropDownList CssClass="form-control" ID="ddlFechaSalidaMes" onChange="ddlMesNuevo('WucTakeHome_','ddlFechaSalidaDía','ddlFechaSalidaMes')" runat="server">
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
                <asp:DropDownList CssClass="form-control" ID="ddlFechaSalidaDía" runat="server" onChange="ddlDíaNuevo('WucTakeHome_','ddlFechaSalidaMes','ddlFechaSalidaDía')">
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
                <asp:TextBox CssClass="form-control" onBlur="TakeHomeFechaSalida()" ID="txtFechaSalidaAño" runat="server" MaxLength="4" Width="80px"/>
                <asp:Label ID="lblFE_Out" runat="server"/>
            </div>
        </div>


        <div class="col-md-6 SEPSDivs"><%--Número de botellas--%>
            <span class="SEPSLabel">Cantidad de botellas:</span>
            <div class="expandibleDiv">
                <asp:TextBox  CssClass="form-control" ID="txtCantidadBotellas" runat="server" MaxLength="4"/>
                <asp:Label ID="lblCantidadBotellas" runat="server"/>
            </div>
        </div>
        <div class="col-md-6 SEPSDivs"><%--Frecuencia de botellas--%>
            <span class="SEPSLabel">Frecuencia de botellas:</span>
            <div class="expandibleDiv">
                <asp:DropDownList CssClass="form-control" ID="ddlFrecuenciaBotellas" runat="server"/>
                <asp:Label ID="lblFrecuenciaBotellas" runat="server"/>
            </div>
        </div>
    </div>
  </div>
</div>

</div>
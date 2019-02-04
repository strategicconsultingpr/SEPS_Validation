<%@ Page Language="c#" Inherits="ASSMCA.frmLogon" CodeBehind="frmLogon.aspx.cs" MasterPageFile="Main-NotLogged.Master" %>

<asp:Content ID="mainLogin" runat="server" ContentPlaceHolderID="mainBodyContent">
    <div id="LoginBox" runat="server">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">Datos del usuario</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-6 SEPSDivs">
                        <span class="SEPSLabel">Usuario:</span>
                        <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic" ErrorMessage="Entre el nombre de usuario que se le haya asignado." ControlToValidate="txtUsuario" ToolTip="Debe entrar el nombre de usuario" Text="*" />
                        <div class="expandibleDiv">
                            <asp:TextBox ID="txtUsuario" runat="server" class="form-control" />
                            <asp:Label ID="lblLogin" runat="server" />
                        </div>
                    </div>
                    <div class="col-sm-6 SEPSDivs">
                        <span class="SEPSLabel">Contraseña:</span>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" CssClass="rightFloatAsterisk" ErrorMessage="Entre la contraseña." Display="Dynamic" ControlToValidate="txtPassword" ToolTip="Debe entrar la contraseña" Text="*" />
                        <div class="expandibleDiv">
                            <asp:TextBox ID="txtPassword" runat="server" MaxLength="15" class="form-control" TextMode="Password" />
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-xs-12 SEPSDivs">
                        <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-default" Text="Iniciar" OnClick="btnLogin_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div id="divMsg1" runat="server" style="position: relative;">
            <div class="panel panel-default">
                <div class="panel-body">
                    <h2>Antes de iniciar, recuerde los siguientes puntos:</h2>
                    <ol>
                        <li>El nombre de usuario debe existir en la base de datos ASSMCA. El administrador del sistema debe haberle otorgado un nombre de usuario para que usted pueda autenticarse en el sistema.</li>
                        <li>La contraseña es inicialmente otorgada por el administrador del sistema, usted podrá modificar su contraseña una vez autenticado en el sistema.</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <div id="divPrograma" runat="server" style="padding-top: 5px;">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">Selección de programa</h3>
            </div>
            <div class="panel-body">
                <span class="SEPSLabel">Programa:</span>
                <div class="expandibleDiv">
                    <asp:DropDownList ID="ddlPrograma" runat="server" CssClass="form-control" DataTextField="NB_Programa" DataValueField="PK_Programa" DataSource="<%# dsSeguridad %>" DataMember="SA_USUARIO" />
                </div>
                <div id="divBotones" style="padding-top: 10px;">
                    <asp:Button ID="btnAutenticarPrograma" CssClass="btn btn-default" runat="server" Text="Seleccionar" OnClick="btnAutenticarPrograma_Click" />
                </div>
            </div>
        </div>
    </div>
    <br />
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="496px" ShowSummary="False" ShowMessageBox="True" HeaderText="Los siguientes campos son requeridos o contienen valores incorrectos:" />
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Visible="False" />
    </div>
</asp:Content>

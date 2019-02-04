<%@ Page Language="c#" Inherits="ASSMCA.PasswordChangeMandatory" CodeBehind="PasswordChangeMandatory.aspx.cs" MasterPageFile="~/Main-NotLogged.Master" %>
<asp:Content ID="mainEditC" runat="server" ContentPlaceHolderID="mainBodyContent">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h1 class="panel-title">Modificaci�n de contrase�a requerida</h1>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-6 SEPSDivs">
                    <span class="SEPSLabel">Nombre de usuario:</span>
                    <asp:Label ID="lblLogin" runat="server" Text='<%# DataBinder.Eval(dsSeguridad, "Tables[SA_USUARIO].DefaultView.[0].NB_Login") %>'/>
                </div>
                <div class="col-md-6 SEPSDivs"><span class="SEPSLabel">Contrase�a anterior:</span>
                    <asp:RequiredFieldValidator ID="rfvPasswordAnterior" CssClass="rightFloatAsterisk" runat="server" Display="Dynamic" ControlToValidate="txtPasswordAnterior" ToolTip="<-- Atributo requerido" ErrorMessage="La Contrase�a Anterior es una atributo requerido." Text="*"/>
                    <div class="expandibleDiv">
                        <asp:TextBox ID="txtPasswordAnterior" MaxLength="15" CssClass="form-control" runat="server" TextMode="Password"/>
                    </div>
                    
                </div>
                <div class="col-md-6 SEPSDivs"><span class="SEPSLabel">Contrase�a nueva:</span>
                    <asp:RequiredFieldValidator ID="rfvPasswordNuevo" runat="server"  CssClass="rightFloatAsterisk" Display="Dynamic" ControlToValidate="txtPasswordNuevo" ToolTip="<-- Atributo requerido" ErrorMessage="La Contrase�a Nueva es un atributo requerido" Text="*"/>
                    <asp:CompareValidator ID="cvcPasswordNuevo" runat="server" CssClass="rightFloatAsterisk" Display="Dynamic"  ControlToValidate="txtPasswordNuevo" ToolTip="<-- Es igual al Password Anterior." ErrorMessage="La Contrase�a Nueva debe ser diferente a la Contrase�a anterior" ControlToCompare="txtPasswordAnterior" Operator="NotEqual" Text="*"/>
                    <div class="expandibleDiv">
                        <asp:TextBox ID="txtPasswordNuevo" MaxLength="15" runat="server" CssClass="form-control" TextMode="Password"/>
                    </div>
                    
                </div>
                <div class="col-md-6 SEPSDivs"><span class="SEPSLabel">Confirmar contrase�a:</span>
                    <asp:RequiredFieldValidator ID="rfvPasswordValidacion" Display="Dynamic"  CssClass="rightFloatAsterisk" runat="server" ControlToValidate="txtPasswordValidacion" ToolTip="<-- Atributo requerido" ErrorMessage="Confirmar Contrase�a Nueva es un atributo requerido" Text="*"/>
                    <asp:CompareValidator ID="cvcPasswordValidacion" runat="server" CssClass="rightFloatAsterisk" ControlToValidate="txtPasswordValidacion" ToolTip="<-- Es diferente al Nuevo Password" ErrorMessage="Las contrase�as no son iguales." ControlToCompare="txtPasswordNuevo" Display="Dynamic" Text="*"/>
                    <div class="expandibleDiv">
                        <asp:TextBox ID="txtPasswordValidacion" MaxLength="15" runat="server" CssClass="form-control" TextMode="Password"/>
                    </div>
                    
                </div>
                <div class="col-md-6 SEPSDivs">
                    <asp:Button ID="btnPassword" runat="server" CssClass="btn btn-default" Text="Modificar contrase�a" CausesValidation="false" OnClientClick="Page_ClientValidate();" OnClick="btnPassword_Click"/>
                </div>
            </div>
                    
        </div>
    </div>
    <asp:Label id="errorMessage" runat="server" />
    <asp:ValidationSummary ID="ValidationSummary1" Style="Z-INDEX: 108; LEFT: 500px; POSITION: absolute; TOP: 30px; width: 352px;" runat="server" ShowMessageBox="True" HeaderText="Se han encontrado algunos errores en el formulario.  Debe revisar antes de cambiar la constrase�a:" ShowSummary="False"/>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Main-NotLogged.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SEPS.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainBodyContent" runat="server">
         <h1>Dashboard</h1>
    <br />
    <div>
        <ul class="nav nav-tabs" role="tablist">
              <li> <a class="btn btn-default" href="<%=ResolveClientUrl("~/frmLogon.aspx?changeProg=yes")%>">Volver a la página anterior</a></li>
            <li role="presentation" class="active"><a href="#tab1" aria-controls="tab1" role="tab" data-toggle="tab">Administrativo (Clientela Atendida - Episodios)</a></li>
            <li role="presentation"><a href="#tab2" aria-controls="tab2" role="tab" data-toggle="tab">Perfiles Registrados</a></li>
            <li role="presentation"><a href="#tab3" aria-controls="tab3" role="tab" data-toggle="tab">Clientela Atendida (Episodios)</a></li>
            <li role="presentation"><a href="#tab4" aria-controls="tab4" role="tab" data-toggle="tab">Clientes No Duplicados (IUP)</a></li>
            <li role="presentation"><a href="#tab5" aria-controls="tab5" role="tab" data-toggle="tab">Tabla de Diagnóstico y Sustancias</a></li>  
        </ul>
        <div class="tab-content">
            <div role="tabpanel" class="tab-pane active" id="tab1">
                <br />
                <iframe width="100%" height="1060" src="https://app.powerbigov.us/view?r=eyJrIjoiMzg3YzcxMWMtMWNhMy00YTRlLWJkM2MtYzJiM2NlYjYyN2Q0IiwidCI6ImI4MGEwMzc4LTcwZDUtNDBjZi1iNjYzLTY3YWYyMTk3YjBlYiJ9" frameborder="0" allowFullScreen="true"></iframe>
     </div>
            <div role="tabpanel" class="tab-pane" id="tab2">
                <br /> 
                <iframe width="100%" height="1060"  src="https://app.powerbigov.us/view?r=eyJrIjoiODJlMWU3MDEtYzc2MS00NjY1LWE0YzgtNDY1OWYyNTcyZjQxIiwidCI6ImI4MGEwMzc4LTcwZDUtNDBjZi1iNjYzLTY3YWYyMTk3YjBlYiJ9" frameborder="0" allowFullScreen="true"></iframe>
   </div>
            <div role="tabpanel" class="tab-pane" id="tab3">
                <br />
           
               <iframe width="100%" height="1060"  src="https://app.powerbigov.us/view?r=eyJrIjoiOWZlNTk1NDMtZDdkZi00YzYyLWFiOTYtNjE4YzAzZWQxMTIyIiwidCI6ImI4MGEwMzc4LTcwZDUtNDBjZi1iNjYzLTY3YWYyMTk3YjBlYiJ9" frameborder="0" allowFullScreen="true"></iframe>

            </div>
            <div role="tabpanel" class="tab-pane" id="tab4">
                <br />
                <iframe width="100%" height="1060" src="https://app.powerbigov.us/view?r=eyJrIjoiYjc0ODg5NGItZWUxNi00OTZiLWI3N2YtNDAwODMwOTIzZGQ0IiwidCI6ImI4MGEwMzc4LTcwZDUtNDBjZi1iNjYzLTY3YWYyMTk3YjBlYiJ9" frameborder="0" allowFullScreen="true"></iframe>
 </div>
            <div role="tabpanel" class="tab-pane" id="tab5">
                <br />
               <iframe  width="100%" height="1060"   src="https://app.powerbigov.us/view?r=eyJrIjoiYTYyZDI1NWQtNTAwYy00MjJiLThkZWYtMGZmMjJmMTg4NTI0IiwidCI6ImI4MGEwMzc4LTcwZDUtNDBjZi1iNjYzLTY3YWYyMTk3YjBlYiJ9" frameborder="0" allowFullScreen="true"></iframe>
            </div>
        </div>

    </div>

  
   
</asp:Content>

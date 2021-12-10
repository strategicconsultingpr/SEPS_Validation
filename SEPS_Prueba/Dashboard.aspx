<%@ Page Title="" Language="C#" MasterPageFile="~/Main-NotLogged.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SEPS.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainBodyContent" runat="server">
         <h1>Dashboard</h1>
    <br />
    <div>
        <ul class="nav nav-tabs" role="tablist">
              <li> <a class="btn btn-default" href="/frmLogon.aspx?changeProg=yes">Volver a la página anterior</a></li>
            <li role="presentation" class="active"><a href="#tab1" aria-controls="tab1" role="tab" data-toggle="tab">Administrativo (Clientela Atendida - Episodios)</a></li>
            <li role="presentation"><a href="#tab2" aria-controls="tab2" role="tab" data-toggle="tab">Perfiles Registrados</a></li>
            <li role="presentation"><a href="#tab3" aria-controls="tab3" role="tab" data-toggle="tab">Clientela Atendida (Episodios)</a></li>
            <li role="presentation"><a href="#tab4" aria-controls="tab4" role="tab" data-toggle="tab">Clientes No Duplicados (IUP)</a></li>
            <li role="presentation"><a href="#tab5" aria-controls="tab5" role="tab" data-toggle="tab">Tabla de Diagnóstico y Sustancias</a></li>  
        </ul>
        <div class="tab-content">
            <div role="tabpanel" class="tab-pane active" id="tab1">
                <br />
                <iframe width="100%" height="1060" src="https://app.powerbi.com/view?r=eyJrIjoiNmE3ZmM1Y2EtODUwNS00NjBlLThhMGEtMGRjMTY1ODJkNTRkIiwidCI6IjBkZmE1ZGMwLTAzNmYtNDYxNS05OWU0LTk0YWY4MjJmMmI4NCIsImMiOjF9&pageName=ReportSection" frameborder="0" allowFullScreen="true"></iframe>
     </div>
            <div role="tabpanel" class="tab-pane" id="tab2">
                <br /> 
                <iframe width="100%" height="1060"  src="https://app.powerbi.com/view?r=eyJrIjoiOTIyNTIxMGEtNjUxNS00MjYyLTg3NmMtNzY2OGI4YWNkYWI1IiwidCI6IjBkZmE1ZGMwLTAzNmYtNDYxNS05OWU0LTk0YWY4MjJmMmI4NCIsImMiOjF9&pageName=ReportSectionbe98e161cce5ce59ee8a" frameborder="0" allowFullScreen="true"></iframe>
   </div>
            <div role="tabpanel" class="tab-pane" id="tab3">
                <br />
           
               <iframe width="100%" height="1060"  src="https://app.powerbi.com/view?r=eyJrIjoiMzA3ZWI1NDEtOTBmMC00YWE0LTk3NTctMTBiYTFkNTc4ZDcwIiwidCI6IjBkZmE1ZGMwLTAzNmYtNDYxNS05OWU0LTk0YWY4MjJmMmI4NCIsImMiOjF9&pageName=ReportSectionacaa95cc4631395a209b" frameborder="0" allowFullScreen="true"></iframe>

            </div>
            <div role="tabpanel" class="tab-pane" id="tab4">
                <br />
                <iframe width="100%" height="1060" src="https://app.powerbi.com/view?r=eyJrIjoiYTZhM2M5NjMtYjY0Zi00NWVlLTgxNTItOGZkODU0NmRlYmU0IiwidCI6IjBkZmE1ZGMwLTAzNmYtNDYxNS05OWU0LTk0YWY4MjJmMmI4NCIsImMiOjF9&pageName=ReportSection47dae6fd3c2ed73bfd09" frameborder="0" allowFullScreen="true"></iframe>
 </div>
            <div role="tabpanel" class="tab-pane" id="tab5">
                <br />
               <iframe  width="100%" height="1060"   src="https://app.powerbi.com/view?r=eyJrIjoiODYzM2NmNzItMzI1NS00Y2U1LTg3N2MtZjNmNmI4ZWU5ZmJiIiwidCI6IjBkZmE1ZGMwLTAzNmYtNDYxNS05OWU0LTk0YWY4MjJmMmI4NCIsImMiOjF9&pageName=ReportSection59e9ecdec7cff2753124" frameborder="0" allowFullScreen="true"></iframe>
            </div>
        </div>

    </div>

  
   
</asp:Content>

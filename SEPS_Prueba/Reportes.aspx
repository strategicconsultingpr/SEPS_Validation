<%@ Page Language="c#" Inherits="ASSMCA.Reportes.frmReportes" CodeBehind="Reportes.aspx.cs" MasterPageFile="~/Main.Master" %>
<asp:Content ID="mainEditC" runat="server" ContentPlaceHolderID="mainBodyContent">
    <h1>Reportes</h1>
    <div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Tablas URS</h3>
  </div>
  <div class="panel-body" style="padding:0px">
    <table class="table table-striped table-hover table-condensed">
        <tr>
            <th style="width:200px;">Nombre</th>
            <th>Descripción</th>
            <th style="width:50px;">Doc.</th>
        </tr>
        <tr>
            <td>Table 2A-2B</td>
            <td>Profile of persons served, all programs by age, gender and race/ethnicity</td>
            <td><asp:HyperLink ID="RPT_URS_TABLA_II" runat="server" NavigateUrl="RPT_URS_TABLA_II" Text="ver..."/></td>
        <tr>
            <td>Table 3</td>
            <td>Profile of persons served in the community mental health settings, state psychiatric hospital and other settings</td>
            <td><asp:HyperLink ID="RPT_URS_TABLA_III" runat="server" NavigateUrl="RPT_URS_TABLA_III" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Table 4</td>
            <td>Profile of adult clients by employment status</td>
            <td><asp:HyperLink ID="RPT_URS_TABLA_IV" runat="server" NavigateUrl="RPT_URS_TABLA_IV" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Table 5A-5B</td>
            <td>Profile of clients by type of funding support</td>
            <td><asp:HyperLink ID="RPT_URS_TABLA_V" runat="server" NavigateUrl="RPT_URS_TABLA_V" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Table 6</td>
            <td>Profile of client turnover</td>
            <td><asp:HyperLink ID="HyperLink5" runat="server" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Table 15</td>
            <td>Living situation profile</td>
            <td><asp:HyperLink ID="RPT_URS_TABLA_XV" runat="server" NavigateUrl="RPT_URS_TABLA_XV" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Table 18</td>
            <td>Profile of adults with schizophrenia receiving new generation medication during the year</td>
            <td><asp:HyperLink ID="RPT_URS_TABLA_XVIII" runat="server" NavigateUrl="RPT_URS_TABLA_XVIII" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Table 19A</td>
            <td>Profile of adult criminal justice</td>
            <td><asp:HyperLink ID="RPT_URS_TABLA_XIX_A" runat="server" NavigateUrl="RPT_URS_TABLA_XIX_A" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Table 19B</td>
            <td>Profile of Juvenile justice involvement</td>
            <td><asp:HyperLink ID="RPT_URS_TABLA_XIX_B" runat="server" NavigateUrl="RPT_URS_TABLA_XIX_B" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Table 19C</td>
            <td>Profile of school participation</td>
            <td><asp:HyperLink ID="RPT_URS_TABLA_XIX_C" runat="server" NavigateUrl="RPT_URS_TABLA_XIX_C" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Table 19D</td>
            <td>Profile of school performance</td>
            <td><asp:HyperLink ID="RPT_URS_TABLA_XIX_D" runat="server" NavigateUrl="RPT_URS_TABLA_XIX_D" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Table 20A</td>
            <td>Profile of non-forensic (voluntary and civil-involuntary) patients readmission to any state psychiatric inpatient hospital within 30/180 days of discharge</td>
            <td><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="RPT_BLOCK_T8" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Table 20B</td>
            <td>Profile of forensic patients readmission to any state psychiatric inpatient hospital within 30/180 days of discharge</td>
            <td><asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="RPT_BLOCK_T8" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Table 21</td>
            <td>Profile of non-forensic (voluntary and civil-involuntary) patients readmission to any psychiatric inpatient care unit (state operated or other psychiatric inpatient unit) within 30/180 days of discharge</td>
            <td><asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="RPT_BLOCK_T8" Text="ver..."/></td>
        </tr>
    </table>
  </div>
</div>


    <div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Reportes de Bloque</h3>
  </div>
  <div class="panel-body" style="padding:0px">
      <table class="table table-striped table-hover table-condensed">
        <tr>
            <th style="width:200px;">Nombre</th>
            <th>Descripción</th>
            <th style="width:50px;">Doc.</th>
        </tr>
        <tr>
            <td>Form T1 - Employment Status</td>
            <td>Percentage point change in employment status</td>
            <td><asp:HyperLink ID="RPT_BLOCK_T1" runat="server" NavigateUrl="RPT_BLOCK_T1" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Form T2 - Living Status</td>
            <td>Percentage point change in homelessness</td>
            <td><asp:HyperLink ID="RPT_BLOCK_T2" runat="server" NavigateUrl="RPT_BLOCK_T2" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Form T3 - Criminal Justice Involvement</td>
            <td>Percentage point change in persons arrested</td>
            <td><asp:HyperLink ID="RPT_BLOCK_T3" runat="server" NavigateUrl="RPT_BLOCK_T3" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Form T4 - Alcohol Use</td>
            <td>Percentage point change in abstinence</td>
            <td><asp:HyperLink ID="RPT_BLOCK_T4" runat="server" NavigateUrl="RPT_BLOCK_T4" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Form T5 - Other drug use</td>
            <td>Percentage point change in abstinence</td>
            <td><asp:HyperLink ID="RPT_BLOCK_T5" runat="server" NavigateUrl="RPT_BLOCK_T5" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Form T7 - Social Support of Recovery</td>
            <td>Percentage point change in involvement in social support of recovery</td>
            <td><asp:HyperLink ID="RPT_BLOCK_T7" runat="server" NavigateUrl="RPT_BLOCK_T7" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Form T8 - Retention</td>
            <td>Length of Stay  (in weeks), average number of service per client, proportion of clients completing treatment</td>
            <td><asp:HyperLink ID="RPT_BLOCK_T8" runat="server" NavigateUrl="RPT_BLOCK_T8" Text="ver..."/></td>
        </tr>
    </table>
  </div>
</div>



    <div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Reporte de NOMS Datos</h3>
  </div>
  <div class="panel-body" style="padding:0px">
    <table class="table table-striped table-hover table-condensed">
        <tr>
            <th style="width:200px;">Nombre</th>
            <th>Descripción</th>
            <th style="width:50px;">Doc.</th>
        </tr>
        <tr>
            <td>NOMS Data Quality</td>
            <td>Track the count and percentage of Unknown and Not Collected values from NOMS data elements</td>
            <td><asp:HyperLink ID="RPT_NOMSDATAQUALITY" runat="server" NavigateUrl="RPT_NOMSDATAQUALITY" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Primary Diagnosis Count</td>
            <td>Count of each primary DSMV diagnosis</td>
            <td><asp:HyperLink ID="RPT_PRIMARY_DIAGNOSIS" runat="server" NavigateUrl="RPT_PRIMARY_DIAGNOSIS" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Primary Substance of Choice at Admission</td>
            <td>Count of clients for each primary substance of choice at admission</td>
            <td><asp:HyperLink ID="RPT_SUBSTANCE_OF_CHOICE" runat="server" NavigateUrl="RPT_SUBSTANCE_OF_CHOICE" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Comparison of Frequency of Use of Primary Substance</td>
            <td>Track the change of frequency of use from admission to discharge</td>
            <td><asp:HyperLink ID="RPT_SUBSTANCE_FREQUENCY" runat="server" NavigateUrl="RPT_SUBSTANCE_FREQUENCY" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Comparison of Employment Status</td>
            <td>Track the change of employment status from admission to discharge</td>
            <td><asp:HyperLink ID="RPT_EMPLOYMENT_STATUS" runat="server" NavigateUrl="RPT_EMPLOYMENT_STATUS" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Comparison of Criminal Involvement</td>
            <td>Track the change of criminal involvement from admission to discharge</td>
            <td><asp:HyperLink ID="RPT_CRIMINAL_INVOLVEMENT" runat="server" NavigateUrl="RPT_CRIMINAL_INVOLVEMENT" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Comparison of Living Arrangements</td>
            <td>Track the change of living arrangements from admission to discharge</td>
            <td><asp:HyperLink ID="RPT_LIVING_ARRANGEMENTS" runat="server" NavigateUrl="RPT_LIVING_ARRANGEMENTS" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Retention Rate</td>
            <td>Track the average length of stay</td>
            <td><asp:HyperLink ID="RPT_RETENTION_RATE" runat="server" NavigateUrl="RPT_RETENTION_RATE" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Average Length of Stay by Discharge Type</td>
            <td>Track the average length of stay by each type of discharge</td>
            <td><asp:HyperLink ID="RPT_AVG_LNGTH_STAY_BY_DISCHG" runat="server" NavigateUrl="RPT_AVG_LNGTH_STAY_BY_DISCHG" Text="ver..."/></td>
        </tr>
    </table>
  </div>
</div>


    <div class="panel panel-default">
  <div class="panel-heading">
    <h3 class="panel-title">Reportes de TEDS</h3>
  </div>
  <div class="panel-body" style="padding:0px">
        <table class="table table-striped table-hover table-condensed">
        <tr>
            <th style="width:200px;">Nombre</th>
            <th>Descripción</th>
            <th style="width:50px;">Doc.</th>
        </tr>
        <tr>
            <td>Tabla I - Reporte de TEDS</td>
            <td>Reporte de TEDS</td>
            <td><asp:HyperLink ID="RPT_TEDS" runat="server" NavigateUrl="RPT_TEDS" Text="ver..."/></td>
        </tr>
        <tr>
            <td>Informe Trimestral por Programa</td>
            <td>Resumen Perfil Socio Demográfico</td>
            <td><asp:HyperLink ID="rptTrimestral" runat="server" NavigateUrl="rptTrimestral" Text="ver..."/></td>
        </tr>
    </table>
  </div>
</div>
</asp:Content>
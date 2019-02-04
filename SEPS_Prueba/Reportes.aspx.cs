using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
namespace ASSMCA.Reportes
{
	public partial class frmReportes : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (this.Session["dsSeguridad"] == null)
            {
                this.Response.Redirect("~/Error.aspx?errMsg=sesion");
                return;
            }
			System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
			string URL_ReportingServices = ((string)(configurationAppSettings.GetValue("URL_ReportingServices", typeof(string))));
			string Folder_ReportingServices = ((string)(configurationAppSettings.GetValue("Folder_ReportingServices", typeof(string))));
			this.Session["URL_Reports"] = URL_ReportingServices + "?/" + Folder_ReportingServices + "/";
			this.RPT_BLOCK_T1.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_BLOCK_T1.NavigateUrl.ToString();
			this.RPT_BLOCK_T2.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_BLOCK_T2.NavigateUrl.ToString();
			this.RPT_BLOCK_T3.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_BLOCK_T3.NavigateUrl.ToString();
			this.RPT_BLOCK_T4.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_BLOCK_T4.NavigateUrl.ToString();
			this.RPT_BLOCK_T5.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_BLOCK_T5.NavigateUrl.ToString();
			this.RPT_BLOCK_T7.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_BLOCK_T7.NavigateUrl.ToString();
			this.RPT_BLOCK_T8.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_BLOCK_T8.NavigateUrl.ToString();
			this.RPT_URS_TABLA_II.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_URS_TABLA_II.NavigateUrl.ToString();
			this.RPT_URS_TABLA_III.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_URS_TABLA_III.NavigateUrl.ToString();
			this.RPT_URS_TABLA_IV.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_URS_TABLA_IV.NavigateUrl.ToString();
			this.RPT_URS_TABLA_V.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_URS_TABLA_V.NavigateUrl.ToString();
			this.RPT_URS_TABLA_XV.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_URS_TABLA_XV.NavigateUrl.ToString();
			this.RPT_URS_TABLA_XVIII.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_URS_TABLA_XVIII.NavigateUrl.ToString();
			this.RPT_URS_TABLA_XIX_A.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_URS_TABLA_XIX_A.NavigateUrl.ToString();
			this.RPT_URS_TABLA_XIX_B.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_URS_TABLA_XIX_B.NavigateUrl.ToString();
			this.RPT_URS_TABLA_XIX_C.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_URS_TABLA_XIX_C.NavigateUrl.ToString();
			this.RPT_URS_TABLA_XIX_D.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_URS_TABLA_XIX_D.NavigateUrl.ToString();
            this.RPT_NOMSDATAQUALITY.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_NOMSDATAQUALITY.NavigateUrl.ToString();
            this.RPT_AVG_LNGTH_STAY_BY_DISCHG.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_AVG_LNGTH_STAY_BY_DISCHG.NavigateUrl.ToString();
            this.RPT_CRIMINAL_INVOLVEMENT.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_CRIMINAL_INVOLVEMENT.NavigateUrl.ToString();
            this.RPT_EMPLOYMENT_STATUS.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_EMPLOYMENT_STATUS.NavigateUrl.ToString();
            this.RPT_LIVING_ARRANGEMENTS.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_LIVING_ARRANGEMENTS.NavigateUrl.ToString();
            this.RPT_PRIMARY_DIAGNOSIS.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_PRIMARY_DIAGNOSIS.NavigateUrl.ToString();
            this.RPT_RETENTION_RATE.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_RETENTION_RATE.NavigateUrl.ToString();
            this.RPT_SUBSTANCE_FREQUENCY.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_SUBSTANCE_FREQUENCY.NavigateUrl.ToString();
            this.RPT_SUBSTANCE_OF_CHOICE.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_SUBSTANCE_OF_CHOICE.NavigateUrl.ToString();
            this.RPT_TEDS.NavigateUrl = this.Session["URL_Reports"].ToString() + this.RPT_TEDS.NavigateUrl.ToString();
            this.rptTrimestral.NavigateUrl = this.Session["URL_Reports"].ToString() + this.rptTrimestral.NavigateUrl.ToString();
		}
		#region Código generado por el Diseñador de Web Forms
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
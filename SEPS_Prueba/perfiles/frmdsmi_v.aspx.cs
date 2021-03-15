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
using ASSMCA.perfiles;

namespace ASSMCA.Perfiles
{
	public partial class frmDSMI_V : System.Web.UI.Page
	{
		protected ASSMCA.perfiles.dsPerfil dsPerfil;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( this.Session["dsSeguridad"] == null )
			{
                this.Response.Redirect("~/Error.aspx?errMsg=sesion");
				return;
			}
			if(!this.IsPostBack)
			{
                if (this.Request.QueryString["txtfiltro"] != null && this.Request.QueryString["txtDescripcion"] != null && this.Request.QueryString["txtDescripcionHidden"] != null)
                {
                    this.txtDescripcion.Value = this.Request.QueryString["txtDescripcion"].ToString();
                    this.txtDescripcionHidden.Value = this.Request.QueryString["txtDescripcionHidden"].ToString();
                    this.tipoDescripcion.Value = this.Request.QueryString["tipoDescripcion"].ToString();

                    this.txtFiltroTipo.Value = this.Request.QueryString["txtfiltro"].ToString();

                    ClinHD.Value = "mainBodyContent_"+ this.Request.QueryString["tipoDescripcion"].ToString() + "_hDSMVClinPrim";
                    ClinTxt1.Value = "mainBodyContent_" + this.Request.QueryString["tipoDescripcion"].ToString() + "_txtDSMVClinSec";
                    ClinHD1.Value = "mainBodyContent_" + this.Request.QueryString["tipoDescripcion"].ToString() + "_hDSMVClinSec";
                    ClinTxt2.Value = "mainBodyContent_" + this.Request.QueryString["tipoDescripcion"].ToString() + "_txtDSMVClinTer";
                    ClinHD2.Value = "mainBodyContent_" + this.Request.QueryString["tipoDescripcion"].ToString() + "_hDSMVClinTer";

                    RMHD.Value = "mainBodyContent_" + this.Request.QueryString["tipoDescripcion"].ToString() + "_hDSMVRMPrim";
                    RMTxt1.Value = "mainBodyContent_" + this.Request.QueryString["tipoDescripcion"].ToString() + "_txtDSMVRMSec";
                    RMHD1.Value = "mainBodyContent_" + this.Request.QueryString["tipoDescripcion"].ToString() + "_hDSMVRMSec";
                    RMTxt2.Value = "mainBodyContent_" + this.Request.QueryString["tipoDescripcion"].ToString() + "_txtDSMVRMTer";
                    RMHD2.Value = "mainBodyContent_" + this.Request.QueryString["tipoDescripcion"].ToString() + "_hDSMVRMTer";
                    /*if(this.txtDescripcion.Value== "mainBodyContent_WucEpisodioAdmision_txtDSMVClinSec")
                    {
                        ClinHD.Value = "mainBodyContent_WucEpisodioAdmision_hDSMVClinPrim";
                        ClinTxt2.Value = "mainBodyContent_WucEpisodioAdmision_txtDSMVClinTer";
                        ClinHD2.Value = "mainBodyContent_WucEpisodioAdmision_hDSMVClinTer";
                    }
                    else if(this.txtDescripcion.Value == "mainBodyContent_WucEpisodioAdmision_txtDSMVClinPrim")
                    {
                        ClinTxt1.Value = "mainBodyContent_WucEpisodioAdmision_txtDSMVClinSec";
                        ClinHD1.Value = "mainBodyContent_WucEpisodioAdmision_hDSMVClinSec";

                        ClinTxt2.Value = "mainBodyContent_WucEpisodioAdmision_txtDSMVClinTer";
                        ClinHD2.Value = "mainBodyContent_WucEpisodioAdmision_hDSMVClinTer";
                    }

                    if (this.txtDescripcion.Value == "mainBodyContent_WucEpisodioAdmision_txtDSMVRMSec")
                    {
                        RMTxt2.Value = "mainBodyContent_WucEpisodioAdmision_txtDSMVRMTer";
                        RMHD2.Value = "mainBodyContent_WucEpisodioAdmision_hDSMVRMTer";
                    }
                    else if (this.txtDescripcion.Value == "mainBodyContent_WucEpisodioAdmision_txtDSMVRMPrim")
                    {
                        RMTxt1.Value = "mainBodyContent_WucEpisodioAdmision_txtDSMVRMSec";
                        RMHD1.Value = "mainBodyContent_WucEpisodioAdmision_hDSMVClinSec";

                        RMTxt2.Value = "mainBodyContent_WucEpisodioAdmision_txtDSMVRMTer";
                        RMHD2.Value = "mainBodyContent_WucEpisodioAdmision_hDSMVRMTer";
                    }*/


                }
				this.dsPerfil = (ASSMCA.perfiles.dsPerfil)this.Session["dsPerfil"];

                SA_LKP_DSMV1TableAdapter SA_LKP_DSMV = new SA_LKP_DSMV1TableAdapter();

                if (txtFiltroTipo.Value == "NONE")
                    lbxDSMV.DataSource = SA_LKP_DSMV.GetData();
                else
                    lbxDSMV.DataSource = SA_LKP_DSMV.GetData().Where(x => x.CAT_DSMV == txtFiltroTipo.Value || x.CAT_DSMV == "Other");

                lbxDSMV.DataTextField = "FullDescription";

                this.DataBind();
			}
			else
			{                
                this.dsPerfil = (ASSMCA.perfiles.dsPerfil)this.Session["dsPerfil"];
			}
		}

		#region Código generado por el Diseñador de Web Forms
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{    
			this.dsPerfil = new ASSMCA.perfiles.dsPerfil();
			((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).BeginInit();
			this.dsPerfil.DataSetName = "dsPerfil";
			this.dsPerfil.Locale = new System.Globalization.CultureInfo("en-US");
			((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).EndInit();
		}
		#endregion
	}
}

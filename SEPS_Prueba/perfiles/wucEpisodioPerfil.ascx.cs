namespace ASSMCA.Perfiles
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
    using System.Collections.Generic;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	public partial class wucEpisodioPerfil : System.Web.UI.UserControl
	{
		public frmAction m_frmAction;
		protected ASSMCA.perfiles.dsPerfil dsPerfil;
		protected System.Data.DataView dvwDiagPrimario,dvwDiagSecundario,dvwDiagTerciario,dvwIVPrim,dvwIVSec,dvwIVTerc,dvwDrogaPrim,dvwDrogaSec,dvwDrogaTerc,dvwViaPrim,dvwViaSec,dvwViaTerc,dvwFrecPrim,dvwFrecSec,dvwFrecTerc,dvwMediPrim,dvwMediSec,dvwMediTerc,dvw_DSMV_ProblemasPsicosocialesAmbientales1, dvw_DSMV_ProblemasPsicosocialesAmbientales2, dvw_DSMV_ProblemasPsicosocialesAmbientales3;
		protected System.Web.UI.WebControls.TextBox txtDisposicionReferido;
		protected System.Web.UI.WebControls.Label lblDisposicionFinal;
		protected System.Data.SqlClient.SqlDataAdapter daPerfilValidaciones;
		protected System.Data.SqlClient.SqlConnection cnn;
		public string TI_Perfil;
        private static string nivelSM, nivelAS;
        public int m_pk_perfil, m_CO_Tipo;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;

		protected void Page_Load(object sender, System.EventArgs e)
		{
            m_CO_Tipo = Convert.ToInt32(this.Session["co_tipo"].ToString());
            this.CO_Tipo.Value = this.Session["co_tipo"].ToString();
            this.hDual.Value = this.Session["PK_Episodio"].ToString();

            this.hNivelSM.Value = nivelSM;
            this.hNivelAS.Value = nivelAS;
            
            if (!this.IsPostBack)
			{
				this.dsPerfil = (ASSMCA.perfiles.dsPerfil)this.Session["dsPerfil"];
                m_pk_perfil = Convert.ToInt32( dsPerfil.SA_PERFIL[0].PK_NR_Perfil.ToString());
                this.hNivelSM.Value = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoMental"].ToString();
                this.hNivelAS.Value = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoSustancias"].ToString();
                this.dvwDiagPrimario.Table = this.dsPerfil.SA_LKP_DIAGNOSTICO;
				this.dvwDiagSecundario.Table = this.dsPerfil.SA_LKP_DIAGNOSTICO;
				this.dvwDiagTerciario.Table = this.dsPerfil.SA_LKP_DIAGNOSTICO;
				this.dvwIVPrim.Table = this.dsPerfil.SA_LKP_DSMIV_IV;
				this.dvwIVSec.Table = this.dsPerfil.SA_LKP_DSMIV_IV;
				this.dvwIVTerc.Table = this.dsPerfil.SA_LKP_DSMIV_IV;
				this.dvwDrogaPrim.Table = this.dsPerfil.SA_LKP_TEDS_SUSTANCIA;
				this.dvwDrogaSec.Table = this.dsPerfil.SA_LKP_TEDS_SUSTANCIA;
				this.dvwDrogaTerc.Table = this.dsPerfil.SA_LKP_TEDS_SUSTANCIA;
				this.dvwViaPrim.Table = this.dsPerfil.SA_LKP_TEDS_VIA_UTILIZACION;
				this.dvwViaSec.Table = this.dsPerfil.SA_LKP_TEDS_VIA_UTILIZACION;
				this.dvwViaTerc.Table = this.dsPerfil.SA_LKP_TEDS_VIA_UTILIZACION;
				this.dvwFrecPrim.Table = this.dsPerfil.SA_LKP_TEDS_FRECUENCIA;
				this.dvwFrecSec.Table = this.dsPerfil.SA_LKP_TEDS_FRECUENCIA;
				this.dvwFrecTerc.Table = this.dsPerfil.SA_LKP_TEDS_FRECUENCIA;
				this.dvwMediPrim.Table = this.dsPerfil.SA_LKP_MEDIDA;
				this.dvwMediSec.Table = this.dsPerfil.SA_LKP_MEDIDA;
				this.dvwMediTerc.Table = this.dsPerfil.SA_LKP_MEDIDA;
                this.dvw_DSMV_ProblemasPsicosocialesAmbientales1.Table = this.dsPerfil.SA_LKP_DSMV_ProblemasPsicosocialesAmbientales;
                this.dvw_DSMV_ProblemasPsicosocialesAmbientales2.Table = this.dsPerfil.SA_LKP_DSMV_ProblemasPsicosocialesAmbientales;
                this.dvw_DSMV_ProblemasPsicosocialesAmbientales3.Table = this.dsPerfil.SA_LKP_DSMV_ProblemasPsicosocialesAmbientales;
				this.DataBind();
                this.ManagePracticasBasadasEnEvidencia(this.m_frmAction);
                this.ManageCondicionesDiagnosticadas(this.m_frmAction);
                switch(this.m_frmAction)
                {
                    case (frmAction.Create): 
                        this.EditarRegistro();
                        this.ActualizarCamposCrear();
                        this.DSMIV_DIV.Visible = false;
                        break;
                    case (frmAction.Read): 
                        this.LeerRegistro();
                        break;
                    case (frmAction.Update): 
                        this.EditarRegistro();
                        this.ActualizarCampos();
                        this.hAccion.Value = "Update";
                        break;
                    default: 
                        break;
                }
			}
		}

		private void LeerRegistro()
		{
            this.lblIVPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_IV_P"].ToString();
            this.lblIVSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_IV_S"].ToString();
            this.lblIVTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_IV_T"].ToString();
			this.lblClinPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TCP"].ToString();
			this.lblClinSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TCS"].ToString();
			this.lblClinTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TCT"].ToString();
			this.lblRMPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TPP"].ToString();
			this.lblRMSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TPS"].ToString();
			this.lblRMTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TPT"].ToString();
            this.lblIIIP.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["CO_CondicionesMedicasPrimario"].ToString();
            this.lblIIIS.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["CO_CondicionesMedicasSecundario"].ToString();
            this.lblIIIT.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["CO_CondicionesMedicasTerciario"].ToString();
            this.lblEscalaGAF.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_EscalaGAF"].ToString();
            if((this.lblIVPrim.Text=="")&&(this.lblIVSec.Text=="")&&(this.lblIVTerc.Text=="")&&(this.lblClinPrim.Text=="")&&(this.lblClinSec.Text=="")&&(this.lblClinTerc.Text=="")&&(this.lblRMPrim.Text=="")&&(this.lblRMSec.Text=="")&&(this.lblRMTerc.Text==""))
            {
                DSMIV_DIV.Visible = false;
            }
            #region DSMV
            this.lblDSMVClinPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos1"].ToString();
            this.lblDSMVClinSec.Text =  this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos2"].ToString();
            this.lblDSMVClinTer.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos3"].ToString();
            this.lblDSMVRMPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosPersonalidadRM1"].ToString();
            this.lblDSMVRMSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosPersonalidadRM2"].ToString();
            this.lblDSMVRMTer.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosPersonalidadRM3"].ToString();
            this.lblDSMVPsicoAmbiPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_ProblemasPsicosocialesAmbientales1"].ToString();
            this.lblDSMVPsicoAmbiSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_ProblemasPsicosocialesAmbientales2"].ToString();
            this.lblDSMVPsicoAmbiTer.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_ProblemasPsicosocialesAmbientales3"].ToString();
            this.lblDSMVDiagDual.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_DiagnosticoDual"].ToString();
            if (this.hDSMVClinPrim.Value == "")
            {
                this.hDSMVClinPrim.Value = "761";
            }
            if (this.hDSMVClinSec.Value == "")
            {
                this.hDSMVClinSec.Value = "761";
            }
            if (this.hDSMVClinTer.Value == "")
            {
                this.hDSMVClinTer.Value = "761";
            }
            if (this.hDSMVRMPrim.Value == "")
            {
                this.hDSMVRMPrim.Value = "761";
            }
            if (this.hDSMVRMSec.Value == "")
            {
                this.hDSMVRMSec.Value = "761";
            }
            if (this.hDSMVRMTer.Value == "")
            {
                this.hDSMVRMTer.Value = "761";
            }
            this.lblDSMVFnGlobal.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_DSMV_FuncionamientoGlobal"].ToString();
            this.lblDSMVOtrasObs.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_OtrasObservaciones"].ToString();
            this.lblDSMVComentarios.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Comentarios"].ToString();
            #endregion
			this.lblDrogaPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Droga_P"].ToString();
			this.lblDrogaSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Droga_S"].ToString();
			this.lblDrogaTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Droga_T"].ToString();
			this.lblEdadPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioPrimario"].ToString();
			this.lblEdadSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioSecundario"].ToString();
			this.lblEdadTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioTerciario"].ToString();
			this.lblEscalaGAF.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_EscalaGAF"].ToString();
			this.lblFrecPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Frecuencia_P"].ToString();
			this.lblFrecSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Frecuencia_S"].ToString();
			this.lblFrecTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Frecuencia_T"].ToString();
			this.lblNivelCuidadoSaludMental.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_SaludMental"].ToString();
			this.lblNivelCuidadoSustancias.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_AbusoSustancias"].ToString();
			this.lblViaPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Via_P"].ToString();
			this.lblViaSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Via_S"].ToString();
			this.lblViaTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Via_T"].ToString();


            //DSMV
            this.txtDSMVClinPrim.Visible = false;
            this.txtDSMVClinSec.Visible = false;
            this.txtDSMVClinTer.Visible = false;
            this.txtDSMVRMPrim.Visible = false;
            this.txtDSMVRMSec.Visible = false;
            this.txtDSMVRMTer.Visible = false;
            this.txtDSMVOtrasObs.Visible = false;
            this.txtDSMVComentarios.Visible = false;
            this.ddlDSMVDiagDual.Visible = false;
            this.ddlDSMVPsicoAmbiPrim.Visible = false;
            this.ddlDSMVPsicoAmbiSec.Visible = false;
            this.ddlDSMVPsicoAmbiTer.Visible = false;
            this.txtDSMVFnGlobal.Visible = false;
            this.hlDSMVClinPrim.Visible = false;
            this.hlDSMVClinSec.Visible = false;
            this.hlDSMVClinTer.Visible = false;
            this.hlDSMVRMPrim.Visible = false;
            this.hlDSMVRMSec.Visible = false;
            this.hlDSMVRMTer.Visible = false;

			this.ddlDrogaPrim.Visible = false;
			this.ddlDrogaSec.Visible = false;
			this.ddlDrogaTerc.Visible = false;
			this.ddlFrecPrim.Visible = false;
			this.ddlFrecSec.Visible = false;
			this.ddlFrecTerc.Visible = false;
			this.ddlViaPrim.Visible = false;
			this.ddlViaSec.Visible = false;
			this.ddlViaTerc.Visible = false;
			this.txtEdadPrim.Visible = false;
			this.txtEdadSec.Visible = false;
			this.txtEdadTerc.Visible = false;
		}

		private void EditarRegistro()
		{
			this.lblNivelCuidadoSaludMental.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_SaludMental"].ToString();
			this.lblNivelCuidadoSustancias.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_AbusoSustancias"].ToString();

            nivelSM = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoMental"].ToString();
            nivelAS = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoSustancias"].ToString();

        }

		private void ActualizarCamposCrear()
		{
                this.ddlDrogaPrim.SelectedValue = "0";
                this.ddlViaPrim.SelectedValue = "0";
                this.txtEdadPrim.Text = "";
                this.ddlDrogaSec.SelectedValue = "0";
                this.ddlViaSec.SelectedValue = "0";
                this.txtEdadSec.Text = "";
                this.ddlDrogaTerc.SelectedValue = "0";
                this.ddlViaTerc.SelectedValue = "0";
                this.txtEdadTerc.Text = "";
		}

		private void ActualizarCampos()
		{
			this.ddlFrecPrim.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_FrecuenciaPrimario"].ToString();
			this.ddlFrecSec.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_FrecuenciaSecundario"].ToString();
            this.ddlFrecTerc.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_FrecuenciaTerciario"].ToString();
            this.lblIVPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_IV_P"].ToString();
            this.lblIVSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_IV_S"].ToString();
            this.lblIVTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_IV_T"].ToString();
            this.lblClinPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TCP"].ToString();
            this.lblClinSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TCS"].ToString();
            this.lblClinTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TCT"].ToString();
            this.lblRMPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TPP"].ToString();
            this.lblRMSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TPS"].ToString();
            this.lblRMTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TPT"].ToString();
            this.lblIIIP.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["CO_CondicionesMedicasPrimario"].ToString();
            this.lblIIIS.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["CO_CondicionesMedicasSecundario"].ToString();
            this.lblIIIT.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["CO_CondicionesMedicasTerciario"].ToString();
            this.lblEscalaGAF.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_EscalaGAF"].ToString();
            if ((this.lblIVPrim.Text == "") && (this.lblIVSec.Text == "") && (this.lblIVTerc.Text == "") && (this.lblClinPrim.Text == "") && (this.lblClinSec.Text == "") && (this.lblClinTerc.Text == "") && (this.lblRMPrim.Text == "") && (this.lblRMSec.Text == "") && (this.lblRMTerc.Text == ""))
            {
                DSMIV_DIV.Visible = false;
            }

            #region DSMV

            this.txtDSMVClinPrim.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos1"].ToString();
            this.hDSMVClinPrim.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_TrastornosClinicos1"].ToString();
            this.txtDSMVClinSec.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos2"].ToString();
            this.hDSMVClinSec.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_TrastornosClinicos2"].ToString();
            this.txtDSMVClinTer.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos3"].ToString();
            this.hDSMVClinTer.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_TrastornosClinicos3"].ToString();

            this.txtDSMVRMPrim.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosPersonalidadRM1"].ToString();
            this.hDSMVRMPrim.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_TrastornosPersonalidadRM1"].ToString();
            this.txtDSMVRMSec.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosPersonalidadRM2"].ToString();
            this.hDSMVRMSec.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_TrastornosPersonalidadRM2"].ToString();
            this.txtDSMVRMTer.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosPersonalidadRM3"].ToString();
            this.hDSMVRMTer.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_TrastornosPersonalidadRM3"].ToString();

            this.ddlDSMVPsicoAmbiPrim.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_ProblemasPsicosocialesAmbientales1"].ToString();
            this.ddlDSMVPsicoAmbiSec.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_ProblemasPsicosocialesAmbientales2"].ToString();
            this.ddlDSMVPsicoAmbiTer.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_ProblemasPsicosocialesAmbientales3"].ToString();

            //this.ddlDSMVDiagDual.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["IN_DiagnosticoDual"].ToString(); -> Este campo proviene del episodio, no del perfil
            this.ddlDSMVDiagDual.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_DSMV_DiagnosticoDual"].ToString();

            this.txtDSMVFnGlobal.Text = (this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_DSMV_FuncionamientoGlobal"].ToString() == "0") ? "" : this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_DSMV_FuncionamientoGlobal"].ToString();
            this.txtDSMVOtrasObs.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_OtrasObservaciones"].ToString();
            this.txtDSMVComentarios.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Comentarios"].ToString();
            #endregion
                this.ddlDrogaSec.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DrogaSecundario"].ToString();
                this.ddlViaSec.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_ViaSecundario"].ToString();
                this.txtEdadSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioSecundario"].ToString();
				this.ddlDrogaTerc.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DrogaTerciario"].ToString();
				this.ddlViaTerc.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_ViaTerciario"].ToString();
				this.txtEdadTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioTerciario"].ToString();
                this.ddlDrogaPrim.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DrogaPrimario"].ToString();
                this.ddlViaPrim.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_ViaPrimario"].ToString();
                this.txtEdadPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioPrimario"].ToString();
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
			this.dvwDiagPrimario = new System.Data.DataView();
			this.dvwDiagSecundario = new System.Data.DataView();
			this.dvwDiagTerciario = new System.Data.DataView();
			this.dvwIVPrim = new System.Data.DataView();
			this.dvwIVSec = new System.Data.DataView();
			this.dvwIVTerc = new System.Data.DataView();
			this.dvwDrogaPrim = new System.Data.DataView();
			this.dvwDrogaSec = new System.Data.DataView();
			this.dvwDrogaTerc = new System.Data.DataView();
			this.dvwViaPrim = new System.Data.DataView();
			this.dvwViaSec = new System.Data.DataView();
			this.dvwViaTerc = new System.Data.DataView();
			this.dvwFrecPrim = new System.Data.DataView();
			this.dvwFrecSec = new System.Data.DataView();
			this.dvwFrecTerc = new System.Data.DataView();
			this.dvwMediPrim = new System.Data.DataView();
			this.dvwMediSec = new System.Data.DataView();
			this.dvwMediTerc = new System.Data.DataView();
            this.dvw_DSMV_ProblemasPsicosocialesAmbientales1 = new System.Data.DataView();
            this.dvw_DSMV_ProblemasPsicosocialesAmbientales2 = new System.Data.DataView();
            this.dvw_DSMV_ProblemasPsicosocialesAmbientales3 = new System.Data.DataView();
			this.daPerfilValidaciones = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.cnn = new System.Data.SqlClient.SqlConnection();
			((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwDiagPrimario)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwDiagSecundario)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwDiagTerciario)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwIVPrim)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwIVSec)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwIVTerc)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwDrogaPrim)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwDrogaSec)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwDrogaTerc)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwViaPrim)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwViaSec)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwViaTerc)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwFrecPrim)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwFrecSec)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwFrecTerc)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwMediPrim)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwMediSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwMediTerc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvw_DSMV_ProblemasPsicosocialesAmbientales1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvw_DSMV_ProblemasPsicosocialesAmbientales2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvw_DSMV_ProblemasPsicosocialesAmbientales3)).BeginInit();
			this.dsPerfil.DataSetName = "dsPerfil";
			this.dsPerfil.Locale = new System.Globalization.CultureInfo("en-US");
			this.daPerfilValidaciones.SelectCommand = this.sqlSelectCommand1;
			this.daPerfilValidaciones.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
			    new System.Data.Common.DataTableMapping("Table", "PERFIL_VALIDACIONES", new System.Data.Common.DataColumnMapping[] {
					new System.Data.Common.DataColumnMapping("FK_DrogaPrimario", "FK_DrogaPrimario"),
					new System.Data.Common.DataColumnMapping("DE_Droga_P", "DE_Droga_P"),
					new System.Data.Common.DataColumnMapping("FK_DrogaSecundario", "FK_DrogaSecundario"),
					new System.Data.Common.DataColumnMapping("DE_Droga_S", "DE_Droga_S"),
					new System.Data.Common.DataColumnMapping("FK_DrogaTerciario", "FK_DrogaTerciario"),
					new System.Data.Common.DataColumnMapping("DE_Droga_T", "DE_Droga_T"),
					new System.Data.Common.DataColumnMapping("FK_ViaPrimario", "FK_ViaPrimario"),
					new System.Data.Common.DataColumnMapping("DE_Via_P", "DE_Via_P"),
					new System.Data.Common.DataColumnMapping("FK_ViaSecundario", "FK_ViaSecundario"),
					new System.Data.Common.DataColumnMapping("DE_Via_S", "DE_Via_S"),
					new System.Data.Common.DataColumnMapping("FK_ViaTerciario", "FK_ViaTerciario"),
					new System.Data.Common.DataColumnMapping("DE_Via_T", "DE_Via_T"),
					new System.Data.Common.DataColumnMapping("FK_FrecuenciaPrimario", "FK_FrecuenciaPrimario"),
					new System.Data.Common.DataColumnMapping("DE_Frecuencia_P", "DE_Frecuencia_P"),
					new System.Data.Common.DataColumnMapping("FK_FrecuenciaSecundario", "FK_FrecuenciaSecundario"),
					new System.Data.Common.DataColumnMapping("DE_Frecuencia_S", "DE_Frecuencia_S"),
					new System.Data.Common.DataColumnMapping("FK_FrecuenciaTerciario", "FK_FrecuenciaTerciario"),
					new System.Data.Common.DataColumnMapping("DE_Frecuencia_T", "DE_Frecuencia_T"),
					new System.Data.Common.DataColumnMapping("IN_EdadInicioPrimario", "IN_EdadInicioPrimario"),
					new System.Data.Common.DataColumnMapping("IN_EdadInicioSecundario", "IN_EdadInicioSecundario"),
					new System.Data.Common.DataColumnMapping("IN_EdadInicioTerciario", "IN_EdadInicioTerciario"),
					new System.Data.Common.DataColumnMapping("NR_CantidadPrimario", "NR_CantidadPrimario"),
					new System.Data.Common.DataColumnMapping("NR_CantidadSecundario", "NR_CantidadSecundario"),
					new System.Data.Common.DataColumnMapping("NR_CantidadTerciario", "NR_CantidadTerciario"),
					new System.Data.Common.DataColumnMapping("FK_MedidaPrimario", "FK_MedidaPrimario"),
					new System.Data.Common.DataColumnMapping("DE_Medida_P", "DE_Medida_P"),
					new System.Data.Common.DataColumnMapping("FK_MedidaSecundario", "FK_MedidaSecundario"),
					new System.Data.Common.DataColumnMapping("DE_Medida_S", "DE_Medida_S"),
					new System.Data.Common.DataColumnMapping("FK_MedidaTerciario", "FK_MedidaTerciario"),
					new System.Data.Common.DataColumnMapping("DE_Medida_T", "DE_Medida_T"),
					new System.Data.Common.DataColumnMapping("NR_GastoPrimario", "NR_GastoPrimario"),
					new System.Data.Common.DataColumnMapping("NR_GastoSecundario", "NR_GastoSecundario"),
					new System.Data.Common.DataColumnMapping("NR_GastoTerciario", "NR_GastoTerciario"),
					new System.Data.Common.DataColumnMapping("FE_PerfilAnterior", "FE_PerfilAnterior"),
					new System.Data.Common.DataColumnMapping("FE_PefilPosterior", "FE_PefilPosterior")})});
			this.sqlSelectCommand1.CommandText = "[SPR_PERFIL_VALIDACIONES]";
			this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
			this.sqlSelectCommand1.Connection = this.cnn;
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Perfil", System.Data.SqlDbType.Int, 4));
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Episodio", System.Data.SqlDbType.Int, 4));
            this.cnn.ConnectionString = NewSource.connectionString;
			((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwDiagPrimario)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwDiagSecundario)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwDiagTerciario)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwIVPrim)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwIVSec)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwIVTerc)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwDrogaPrim)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwDrogaSec)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwDrogaTerc)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwViaPrim)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwViaSec)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwViaTerc)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwFrecPrim)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwFrecSec)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwFrecTerc)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwMediPrim)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwMediSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwMediTerc)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dvw_DSMV_ProblemasPsicosocialesAmbientales1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvw_DSMV_ProblemasPsicosocialesAmbientales2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvw_DSMV_ProblemasPsicosocialesAmbientales3)).EndInit();

		}
		#endregion
		
		#region Propiedades del Perfil

        #region DSMV Properties
        public int @FK_DSMV_TrastornosClinicos1
        {
            get
            {
                int retVal = 0;
                Int32.TryParse(this.hDSMVClinPrim.Value.ToString(), out retVal);
                return retVal;
            }
        }
        public int @FK_DSMV_TrastornosClinicos2
        {
            get
            {
                int retVal = 0;
                Int32.TryParse(this.hDSMVClinSec.Value.ToString(), out retVal);
                return retVal;
            }
        }
        public int @FK_DSMV_TrastornosClinicos3
        {
            get
            {
                int retVal = 0;
                Int32.TryParse(this.hDSMVClinTer.Value.ToString(), out retVal);
                return retVal;
            }
        }
        public int @FK_DSMV_TrastornosPersonalidadRM1
        {
            get
            {
                int retVal = 0;
                Int32.TryParse(this.hDSMVRMPrim.Value.ToString(), out retVal);
                return retVal;
            }
        }
        public int @FK_DSMV_TrastornosPersonalidadRM2
        {
            get
            {
                int retVal = 0;
                Int32.TryParse(this.hDSMVRMSec.Value.ToString(), out retVal);
                return retVal;
            }
        }
        public int @FK_DSMV_TrastornosPersonalidadRM3
        {
            get
            {
                int retVal = 0;
                Int32.TryParse(this.hDSMVRMTer.Value.ToString(), out retVal);
                return retVal;
            }
        }
        public int @FK_DSMV_ProblemasPsicosocialesAmbientales1
        {
            get
            {
                int retVal = 0;
                Int32.TryParse(this.ddlDSMVPsicoAmbiPrim.SelectedValue.ToString(), out retVal);
                return retVal;
            }
        }
        public int @FK_DSMV_ProblemasPsicosocialesAmbientales2
        {
            get
            {
                int retVal = 0;
                Int32.TryParse(this.ddlDSMVPsicoAmbiSec.SelectedValue.ToString(), out retVal);
                return retVal;
            }
        }
        public int @FK_DSMV_ProblemasPsicosocialesAmbientales3
        {
            get
            {
                int retVal = 0;
                Int32.TryParse(this.ddlDSMVPsicoAmbiTer.SelectedValue.ToString(), out retVal);
                return retVal;
            }
        }
        public int @NR_DSMV_FuncionamientoGlobal
        {
            get
            {
                int retVal = 0;
                Int32.TryParse(this.txtDSMVFnGlobal.Text, out retVal);
                return retVal;
            }
        }
        public string @DE_DSMV_OtrasObservaciones
        {
            get
            {
                return this.txtDSMVOtrasObs.Text;
            }
        }
        public string @DE_DSMV_Comentarios
        {
            get
            {
                return this.txtDSMVComentarios.Text;
            }
        }
        public int @IN_DSMV_DiagnosticoDual
        {
            get
            {
                int retVal = 0;
                Int32.TryParse(this.ddlDSMVDiagDual.SelectedValue.ToString(), out retVal);
                return retVal;
            }
        }
        #endregion
 

		public sbyte @FK_NivelCuidadoSaludMental
		{
			get
			{
                try
                {
                    return Convert.ToSByte(this.ddlNivelCuidadoSaludMentalHidden.Value);
                }
                catch
                {
                    return 99;//Default No aplica
                }
			}
		}
	 
		public sbyte @FK_NivelCuidadoSustancias
		{
			get
			{
                try
                {
                    return Convert.ToSByte(this.ddlNivelCuidadoSustanciasHidden.Value);
                }
                catch
                {
                    return 99;//Default No aplica
                }
			}
		}

        public sbyte @FK_DrogaPrimario
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlDrogaPrim.SelectedValue.ToString());
                }
                catch
                {
                    return 99;//Default No aplica
                }
            }
        }

        public sbyte @FK_DrogaSecundario
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlDrogaSec.SelectedValue.ToString());
                }
                catch
                {
                    return 99;//Default No aplica
                }
            }
        }

        public sbyte @FK_DrogaTerciario
        {
            get
            {
                try
                {
                    this.ddlDrogaTerc.Enabled = true;
                    return Convert.ToSByte(this.ddlDrogaTerc.SelectedValue.ToString());
                }
                catch
                {
                    return 99;//Default No aplica
                }
            }
        }

        public sbyte @FK_ViaPrimario
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlViaPrim.SelectedValue.ToString());
                }
                catch
                {
                    return 99;//Default No aplica
                }
            }
        }

        public sbyte @FK_ViaSecundario
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlViaSec.SelectedValue.ToString());
                }
                catch
                {
                    return 99;//Default No aplica
                }
            }
        }

        public sbyte @FK_ViaTerciario
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlViaTerc.SelectedValue.ToString());
                }
                catch
                {
                    return 99;//Default No aplica
                }
            }
        }

        public sbyte @FK_FrecuenciaPrimario
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlFrecPrim.SelectedValue.ToString());
                }
                catch
                {
                    return 99;//Default No aplica
                }
            }
        }

        public sbyte @FK_FrecuenciaSecundario
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlFrecSec.SelectedValue.ToString());
                }
                catch
                {
                    return 99;//Default No aplica
                }
            }
        }

        public sbyte @FK_FrecuenciaTerciario
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlFrecTerc.SelectedValue.ToString());
                }
                catch
                {
                    return 99;//Default No aplica
                }
            }
        }

        public sbyte @IN_EdadInicioPrimario
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.txtEdadPrim.Text.Trim());
                }
                catch
                {
                    return 0;
                }
            }
        }

        public sbyte @IN_EdadInicioSecundario
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.txtEdadSec.Text.Trim());
                }
                catch
                {
                    return 0;
                }
            }
        }

        public sbyte @IN_EdadInicioTerciario
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.txtEdadTerc.Text.Trim());
                }
                catch
                {
                    return 0;
                }
            }
        }

		#endregion

        #region Prácticas Basadas en Evidencia
        private void ManagePracticasBasadasEnEvidencia(frmAction _frmAction)
        {
            if (EsProgramaMental(Convert.ToInt32(this.Session["PK_Programa"].ToString())))
            {
                PKAdministracion pkAdmin = (PKAdministracion)Convert.ToInt32(this.Session["pk_administracion"].ToString());
                switch (_frmAction)
                {
                    case frmAction.Create:
                    case frmAction.Update:
                        divPracticasBasadasEnEvidencia.Visible = true;
                        divPracticasBasadasEnEvidencia.Disabled = false;
                        btnAgregarPracticasBasadasEvidencia.Enabled = true;
                        btnEliminarPracticasBasadasEvidencia.Enabled = true;
                        if (pkAdmin == PKAdministracion.NinosYAdolecentes)
                        {
                            h3PracticasBasadasEnEvidenciaNinoOAdulto.InnerText = "Niños y adolescentes";
                            LbxPracticasBasadasEvidencia();
                        }
                        else
                        {
                            h3PracticasBasadasEnEvidenciaNinoOAdulto.InnerText = "Adultos";
                            LbxPracticasBasadasEvidencia("Adultos");
                        }
                        break;
                    case frmAction.Read:
                        divPracticasBasadasEnEvidencia.Visible = true;
                        divPracticasBasadasEnEvidencia.Disabled = false;
                        btnAgregarPracticasBasadasEvidencia.Enabled = false;
                        btnEliminarPracticasBasadasEvidencia.Enabled = false;
                        btnEliminarPracticasBasadasEvidencia.Visible = false;
                        btnAgregarPracticasBasadasEvidencia.Visible = false;
                        if (pkAdmin == PKAdministracion.NinosYAdolecentes)
                        {
                            h3PracticasBasadasEnEvidenciaNinoOAdulto.InnerText = "Niños y adolescentes";
                            LbxPracticasBasadasEvidencia();
                        }
                        else
                        {
                            h3PracticasBasadasEnEvidenciaNinoOAdulto.InnerText = "Adultos";
                            LbxPracticasBasadasEvidencia("Adultos");
                        }
                        break;
                    default: break;
                }
            }
            else
            {
                divPracticasBasadasEnEvidencia.Visible = false;
                divPracticasBasadasEnEvidencia.Disabled = true;
            }
        }
        private void LbxPracticasBasadasEvidencia(string adultos = "")
        {

            if (m_frmAction == frmAction.Read)
            {
                string selectedValuesString = "";
                NewSource NS = new NewSource();
                DataTable Dref = new DataTable();
                Dref = NS.getRef("SPR_Ref_PracticasBasadasEnEvidencia", m_pk_perfil);
                if (Dref.Rows.Count > 0)
                {
                    foreach (DataRow r in Dref.Rows)
                    {
                        selectedValuesString += r[1].ToString() + ", ";
                    }
                    selectedValuesString = selectedValuesString.Substring(0, selectedValuesString.Length - 2);
                    Dref = null;
                }
                else
                {
                    selectedValuesString = "No hay valores seleccionado";
                }
                divLbxPracticasBasadasEvidencia.Visible = false;
                lblPracticasBasadasEvidencia.Text = selectedValuesString;
                NS = null;
            }
            else
            {
                NewSource NS = new NewSource();
                DataTable Dt = new DataTable();
                Dt = NS.getAll("SPR_DROP_PracticasBasadasEnEvidencia" + adultos);
                this.lbxPracticasBasadasEvidenciaSeleccion.DataSource = Dt;
                this.lbxPracticasBasadasEvidenciaSeleccion.DataValueField = "PK_Practica";
                this.lbxPracticasBasadasEvidenciaSeleccion.DataTextField = "DE_Practica";
                this.lbxPracticasBasadasEvidenciaSeleccion.DataBind();
                if (m_frmAction == frmAction.Update)
                {
                    DataTable Dref = new DataTable();
                    Dref = NS.getRef("SPR_Ref_PracticasBasadasEnEvidencia", m_pk_perfil);
                    if (Dref.Rows.Count > 0)
                    {
                        foreach (DataRow r in Dref.Rows)
                        {
                            System.Web.UI.WebControls.ListItem li = new ListItem(r[1].ToString(), r[0].ToString());
                            this.lbxPracticasBasadasEvidenciaSeleccionado.Items.Add(li);
                            this.lbxPracticasBasadasEvidenciaSeleccion.Items.Remove(li);
                        }
                        Dref = null;
                    }
                }
                divLblPracticasBasadasEvidencia.Visible = false;
                Dt = null;
                NS = null;
            }
        }

        protected void btnAgregarPracticasBasadasEvidencia_Click(object sender, EventArgs e)
        {
            if (this.lbxPracticasBasadasEvidenciaSeleccion.SelectedItem != null)
            {
                System.Web.UI.WebControls.ListItem li = new ListItem(this.lbxPracticasBasadasEvidenciaSeleccion.SelectedItem.Text, this.lbxPracticasBasadasEvidenciaSeleccion.SelectedItem.Value);
                this.lbxPracticasBasadasEvidenciaSeleccionado.Items.Add(li);
                this.lbxPracticasBasadasEvidenciaSeleccion.Items.Remove(li);
                SortListBox(this.lbxPracticasBasadasEvidenciaSeleccionado);
            }
        }

        protected void btnEliminarPracticasBasadasEvidencia_Click(object sender, EventArgs e)
        {
            if (this.lbxPracticasBasadasEvidenciaSeleccionado.SelectedItem != null)
            {
                System.Web.UI.WebControls.ListItem li = new ListItem(this.lbxPracticasBasadasEvidenciaSeleccionado.SelectedItem.Text, this.lbxPracticasBasadasEvidenciaSeleccionado.SelectedItem.Value);
                this.lbxPracticasBasadasEvidenciaSeleccion.Items.Add(li);
                this.lbxPracticasBasadasEvidenciaSeleccionado.Items.Remove(li);
                SortListBox(this.lbxPracticasBasadasEvidenciaSeleccion);
            }
        }

        private void SortListBox(ListBox listBox)
        {
            SortedList<string, string> list = new SortedList<string, string>();
            foreach (ListItem i in listBox.Items)
            {
                list.Add(i.Text, i.Value);
            }
            listBox.Items.Clear();
            foreach (KeyValuePair<string, string> i in list)
            {
                listBox.Items.Add(new ListItem(i.Key, i.Value));
            }
        }

        public int PracticasBasadasEvidenciaItem(int i)
        {
            try
            {
                return Convert.ToInt32(lbxPracticasBasadasEvidenciaSeleccionado.Items[i].Value);
            }
            catch
            {
                return 0;
            }
        }
        public int PracticasBasadasEvidenciaCount
        {
            get
            {
                try
                {
                    return lbxPracticasBasadasEvidenciaSeleccionado.Items.Count;
                }
                catch
                {
                    return 0;
                }
            }
        }

        #endregion
        #region Condiciones diagnosticadas
        private void ManageCondicionesDiagnosticadas(frmAction _frmAction)
        {
            switch (_frmAction)
            {
                case frmAction.Create:
                case frmAction.Update:
                    btnAgregarCondicionesDiagnosticadas.Enabled = true;
                    btnEliminarCondicionesDiagnosticadas.Enabled = true;
                    btnEliminarCondicionesDiagnosticadas.Visible = true;
                    btnAgregarCondicionesDiagnosticadas.Visible = true;
                    LbxCondicionesDiagnosticadas();
                    break;
                case frmAction.Read:
                    btnAgregarCondicionesDiagnosticadas.Enabled = false;
                    btnEliminarCondicionesDiagnosticadas.Enabled = false;
                    btnEliminarCondicionesDiagnosticadas.Visible = false;
                    btnAgregarCondicionesDiagnosticadas.Visible = false;
                    LbxCondicionesDiagnosticadas();
                    break;
                default: break;
            }
        }

        private void LbxCondicionesDiagnosticadas()
        {
            if (m_frmAction == frmAction.Read)
            {
                string selectedValuesString = "";
                NewSource NS = new NewSource();
                DataTable Dref = new DataTable();
                Dref = NS.getRef("SPR_Ref_CondicionesDiagnosticadas", m_pk_perfil);
                if (Dref.Rows.Count > 0)
                {
                    foreach (DataRow r in Dref.Rows)
                    {
                        selectedValuesString += r[1].ToString() + ", ";
                    }
                    selectedValuesString = selectedValuesString.Substring(0, selectedValuesString.Length - 2);
                    Dref = null;
                }
                else
                {
                    selectedValuesString = "No hay valores seleccionados";
                }
                divCondicionesDiagnosticadas.Visible = false;
                lblCondicionesDiagnosticadas.Text = selectedValuesString;
                NS = null;
            }
            else
            {
                NewSource NS = new NewSource();
                DataTable Dt = new DataTable();
                Dt = NS.getAll("SPR_DROP_CondicionesDiagnosticadas");
                this.lbxCondicionesDiagnosticadasSeleccion.DataSource = Dt;
                this.lbxCondicionesDiagnosticadasSeleccion.DataValueField = "PK_Diagnostico";
                this.lbxCondicionesDiagnosticadasSeleccion.DataTextField = "DE_Diagnostico";
                this.lbxCondicionesDiagnosticadasSeleccion.DataBind();
                if (m_frmAction == frmAction.Update)
                {
                    DataTable Dref = new DataTable();
                    Dref = NS.getRef("SPR_Ref_CondicionesDiagnosticadas", m_pk_perfil);
                    if (Dref.Rows.Count > 0)
                    {
                        foreach (DataRow r in Dref.Rows)
                        {
                            System.Web.UI.WebControls.ListItem li = new ListItem(r[1].ToString(), r[0].ToString());
                            this.lbxCondicionesDiagnosticadasSeleccionado.Items.Add(li);
                            this.lbxCondicionesDiagnosticadasSeleccion.Items.Remove(li);
                        }
                        Dref = null;
                    }
                }
                divLblCondicionesDiagnosticadas.Visible = false;
                Dt = null;
                NS = null;
            }
        }

        protected void btnAgregarCondicionesDiagnosticadas_Click(object sender, EventArgs e)
        {
            if (this.lbxCondicionesDiagnosticadasSeleccion.SelectedItem != null)
            {
                System.Web.UI.WebControls.ListItem li = new ListItem(this.lbxCondicionesDiagnosticadasSeleccion.SelectedItem.Text, this.lbxCondicionesDiagnosticadasSeleccion.SelectedItem.Value);
                this.lbxCondicionesDiagnosticadasSeleccionado.Items.Add(li);
                this.lbxCondicionesDiagnosticadasSeleccion.Items.Remove(li);
                SortListBox(this.lbxCondicionesDiagnosticadasSeleccionado);
            }
        }

        protected void btnEliminarCondicionesDiagnosticadas_Click(object sender, EventArgs e)
        {
            if (this.lbxCondicionesDiagnosticadasSeleccionado.SelectedItem != null)
            {
                System.Web.UI.WebControls.ListItem li = new ListItem(this.lbxCondicionesDiagnosticadasSeleccionado.SelectedItem.Text, this.lbxCondicionesDiagnosticadasSeleccionado.SelectedItem.Value);
                this.lbxCondicionesDiagnosticadasSeleccion.Items.Add(li);
                this.lbxCondicionesDiagnosticadasSeleccionado.Items.Remove(li);
                SortListBox(this.lbxCondicionesDiagnosticadasSeleccion);
            }
        }

        public int CondicionesDiagnosticadasItem(int i)
        {
            try
            {
                return Convert.ToInt32(lbxCondicionesDiagnosticadasSeleccionado.Items[i].Value);
            }
            catch
            {
                return 0;
            }
        }
        public int CondicionesDiagnosticadasCount
        {
            get
            {
                try
                {
                    return lbxCondicionesDiagnosticadasSeleccionado.Items.Count;
                }
                catch
                {
                    return 0;
                }
            }
        }

        #endregion

        public bool EsProgramaMental(int pkPrograma)
        {
            bool esProgramaMental = false;
            switch (pkPrograma)
            {
                case (116):
                case (120):
                case (115):
                case (119):
                case (76):
                case (59):
                case (60):
                case (58):
                case (12):
                case (57):
                case (87):
                case (56):
                case (85):
                case (81):
                case (82):
                case (15):
                case (129):
                case (131):
                case (130):
                case (79):
                case (117):
                case (122):

                /*Modificado Nov-08-18: Se agrego acceso a los proximos programas*/

                case (80):
                case (138):
                case (139):
                case (141):
                case (142):
                case (143):
                case (144):
                case (145):
                case (146):
                case (149):
                case (150):
                case (151):
                case (152):
                case (153):
                case (154):
                case (155):
                case (156):
                case (157):
                case (158):
                case (160):
                case (162):
                case (163):
                case (165):
                case (166):
                case (175):

                    esProgramaMental = true; break;
                default: break;
            }
            return esProgramaMental;
        }

    }
}
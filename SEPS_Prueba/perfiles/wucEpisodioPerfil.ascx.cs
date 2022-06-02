namespace ASSMCA.Perfiles
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
    using System.Collections.Generic;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    using SEPS.Constante;
    using SEPS.Modelos;

    public partial class wucEpisodioPerfil : System.Web.UI.UserControl
	{
        private List<DropDownAgeAbusoDeSustancia> ListAgeAbusoDeSustancia = new List<DropDownAgeAbusoDeSustancia>();

        public frmAction m_frmAction;
		protected ASSMCA.perfiles.dsPerfil dsPerfil;
		protected System.Data.DataView dvwNivelMental, dvwDiagPrimario,dvwDiagSecundario,dvwDiagTerciario,dvwIVPrim,dvwIVSec,dvwIVTerc,dvwDrogaPrim,dvwDrogaSec,dvwDrogaTerc,dvwViaPrim,dvwViaSec,dvwViaTerc,dvwFrecPrim,dvwFrecSec,dvwFrecTerc,dvwMediPrim,dvwMediSec,dvwMediTerc,dvw_DSMV_ProblemasPsicosocialesAmbientales1, dvw_DSMV_ProblemasPsicosocialesAmbientales2, dvw_DSMV_ProblemasPsicosocialesAmbientales3;
		protected System.Web.UI.WebControls.TextBox txtDisposicionReferido;
		protected System.Web.UI.WebControls.Label lblDisposicionFinal;
		protected System.Data.SqlClient.SqlDataAdapter daPerfilValidaciones;
		protected System.Data.SqlClient.SqlConnection cnn;
		public string TI_Perfil;
        private static string nivelSM, nivelAS;
        public int m_pk_perfil, m_CO_Tipo;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private bool isEvaluacion, isAlta;


        protected void Page_Load(object sender, System.EventArgs e)
		{
            m_CO_Tipo = Convert.ToInt32(this.Session["co_tipo"].ToString());
            this.CO_Tipo.Value = this.Session["co_tipo"].ToString();
            this.hDual.Value = this.Session["PK_Episodio"].ToString();

            
            isEvaluacion = this.Session["Tipo_Perfil"].ToString() == "Evaluacion";
            isAlta = this.Session["Tipo_Perfil"].ToString() == "Alta";

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
                this.dvwDrogaPrim.RowFilter = "PK_Sustancia <> 18";
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

                this.dvwNivelMental.Table = this.dsPerfil.SA_LKP_SALUD_MENTAL_ANTERIOR;
                this.dvwNivelMental.RowFilter = "PK_SaludMental IN (24,25,26,33)";
                this.DataBind();

                this.ddlNivelRecuperacion.Items.Insert(0, new ListItem("No aplica", "99"));
                this.ddlNivelRecuperacion.SelectedValue = "99";

          



                this.ManagePracticasBasadasEnEvidencia(this.m_frmAction);
                this.ManageCondicionesDiagnosticadas(this.m_frmAction);
                switch(this.m_frmAction)
                {
                    case (frmAction.Create):

                        LoadtxtEdad(DateTime.Now.Date);

                        this.EditarRegistro();
                        this.EditarRecuperacion();
                        this.ActualizarCamposCrear();
                        this.DSMIV_DIV.Visible = false;
                        this.DSMVRM_DIV.Visible = false;
                        this.Hogar_DIV.Style["visibility"] = "hidden";
                        this.Hogar2_DIV.Style["visibility"] = "hidden";
                        this.Hogar3_DIV.Style["visibility"] = "hidden";

                        


                        
                        break;
                    case (frmAction.Read):
                        this.LeerRegistro();
                        this.DSMVRM_DIV.Visible = false;
                        ReorderDSMV(this.m_frmAction);

                        break;
                    case (frmAction.Update):

                        LoadtxtEdad(DateTime.Parse(this.dsPerfil.SA_PERFIL.DefaultView[0]["FE_Perfil"].ToString()));


                        this.EditarRegistro();
                        this.EditarRecuperacion();
                        this.ActualizarCampos();
                        this.hAccion.Value = "Update";
                        this.DSMVRM_DIV.Visible = false;
                        ReorderDSMV(this.m_frmAction);



                        break;
                    default: 
                        break;
                }
             

            }
		}

        void LoadtxtEdad(DateTime date)
        {
            ListAgeAbusoDeSustancia.Add(new DropDownAgeAbusoDeSustancia { Value = 127, Text = "Desconocida" });
            ListAgeAbusoDeSustancia.Add(new DropDownAgeAbusoDeSustancia { Value = 126, Text = "No Aplica" });

            if (date.Date >= Const.CambiosEnCamposNuevos.Date)
            {

                ListAgeAbusoDeSustancia.Add(new DropDownAgeAbusoDeSustancia { Value = 00, Text = "Recién nacido" });
                for (int i = 1; i <= Convert.ToInt32(Session["edad"].ToString()); i++)
                    ListAgeAbusoDeSustancia.Add(new DropDownAgeAbusoDeSustancia { Value = i, Text = i.ToString() });
            }
            else
                for (int i = 0; i <= Convert.ToInt32(Session["edad"].ToString()); i++)
                    ListAgeAbusoDeSustancia.Add(new DropDownAgeAbusoDeSustancia { Value = i, Text = i.ToString() });

            txtEdadPrim.DataSource = ListAgeAbusoDeSustancia;
            txtEdadPrim.DataValueField = "Value";
            txtEdadPrim.DataTextField = "Text";



            txtEdadPrim.DataBind();

            txtEdadSec.DataSource = ListAgeAbusoDeSustancia;
            txtEdadSec.DataValueField = "Value";
            txtEdadSec.DataTextField = "Text";

            txtEdadSec.DataBind();


            txtEdadTerc.DataSource = ListAgeAbusoDeSustancia;
            txtEdadTerc.DataValueField = "Value";
            txtEdadTerc.DataTextField = "Text";

            txtEdadTerc.DataBind();
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
            //this.lblDSMVClinPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos1"].ToString();
            //this.lblDSMVClinSec.Text =  this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos2"].ToString();
            //this.lblDSMVClinTer.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos3"].ToString();


            this.lblDSMVRMPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosPersonalidadRM1"].ToString();
            this.lblDSMVRMSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosPersonalidadRM2"].ToString();
            this.lblDSMVRMTer.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosPersonalidadRM3"].ToString();
            this.lblDSMVPsicoAmbiPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_ProblemasPsicosocialesAmbientales1"].ToString();
            this.lblDSMVPsicoAmbiSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_ProblemasPsicosocialesAmbientales2"].ToString();
            this.lblDSMVPsicoAmbiTer.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_ProblemasPsicosocialesAmbientales3"].ToString();
            this.lblDSMVDiagDual.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_DiagnosticoDual"].ToString();

            //if ((this.lblDSMVRMPrim.Text == "") && (this.lblDSMVRMSec.Text == "") && (this.lblDSMVRMTer.Text == "") && (this.lblDSMVPsicoAmbiPrim.Text == "") && (this.lblDSMVPsicoAmbiSec.Text == "") && (this.lblDSMVPsicoAmbiTer.Text == ""))
            //{
            //    this.DSMVRM_DIV.Visible = false;
            //}
            

            //this.lblDSMVSusPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Sustancias1"].ToString();
            //this.lblDSMVSusSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Sustancias2"].ToString();
            //this.lblDSMVSusTer.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Sustancias3"].ToString();


            //if (this.hDSMVClinPrim.Value == "")
            //{
            //    this.hDSMVClinPrim.Value = "761";
            //}
            //if (this.hDSMVClinSec.Value == "")
            //{
            //    this.hDSMVClinSec.Value = "761";
            //}
            //if (this.hDSMVClinTer.Value == "")
            //{
            //    this.hDSMVClinTer.Value = "761";
            //}


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


            //if (this.hDSMVSusPrim.Value == "")
            //{
            //    this.hDSMVSusPrim.Value = "761";
            //}
            //if (this.hDSMVSusSec.Value == "")
            //{
            //    this.hDSMVSusSec.Value = "761";
            //}
            //if (this.hDSMVSusTer.Value == "")
            //{
            //    this.hDSMVSusTer.Value = "761";
            //}


            DateTime fe_perfil = DateTime.Parse(this.dsPerfil.SA_PERFIL.DefaultView[0]["FE_Perfil"].ToString());
            DateTime limite = new DateTime(2021, 1, 1);
            if (fe_perfil > limite)
            {
                DSMVRM_DIV.Visible = false;
            }

            if (nivelSM == "23" || nivelSM == "24" || nivelSM == "25" || nivelSM == "26" || nivelSM == "33")
            {
                if (isEvaluacion)
                {
                    this.RecuperacionDiv.Visible = true;
                }
                else
                {
                    this.RecuperacionDiv.Visible = false;
                }
            }
            else
            {
                this.RecuperacionDiv.Visible = false;
            }


            this.lblDSMVFnGlobal.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_DSMV_FuncionamientoGlobal"].ToString();
            this.lblDSMVOtrasObs.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_OtrasObservaciones"].ToString();
            this.lblDSMVComentarios.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Comentarios"].ToString();
            #endregion
			this.lblDrogaPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Droga_P"].ToString();

            if(this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva1"].ToString() != "")
            {
                this.lblDrogaPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva1"].ToString();
            }
            this.Hogar_DIV.Visible = false;
            

			this.lblDrogaSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Droga_S"].ToString();

            if (this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva2"].ToString() != "")
            {
                this.lblDrogaSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva2"].ToString();
            }
            this.Hogar2_DIV.Visible = false;

            this.lblDrogaTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Droga_T"].ToString();

            if (this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva3"].ToString() != "")
            {
                this.lblDrogaTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva3"].ToString();
            }
            this.Hogar3_DIV.Visible = false;

            /**
            * File: wuEpisodioAdmision.ascx.cs
            * Fecha: 9/MAR/2021
            * Editado por: Jose A. Ramos De La Cruz
            * Proposito: Poblar los label segun la edad escogida.        
            */

            SetReadLblEdadInicio(lblEdadPrim, this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioPrimario"].ToString());
            SetReadLblEdadInicio(lblEdadSec, this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioSecundario"].ToString());
            SetReadLblEdadInicio(lblEdadTerc, this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioTerciario"].ToString());

            //         this.lblEdadPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioPrimario"].ToString();
            //this.lblEdadSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioSecundario"].ToString();
            //this.lblEdadTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioTerciario"].ToString();
            this.lblEscalaGAF.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_EscalaGAF"].ToString();
			this.lblFrecPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Frecuencia_P"].ToString();
			this.lblFrecSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Frecuencia_S"].ToString();
			this.lblFrecTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Frecuencia_T"].ToString();
			this.lblNivelCuidadoSaludMental.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_SaludMental"].ToString();
			this.lblNivelCuidadoSustancias.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_AbusoSustancias"].ToString();
			this.lblViaPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Via_P"].ToString();
			this.lblViaSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Via_S"].ToString();
			this.lblViaTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Via_T"].ToString();

            if (this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Toxicologia1"].ToString() == "1")
            {
                this.lblToxicologia1.Text = "Si";
            }
            else if (this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Toxicologia1"].ToString() == "2")
            {
                this.lblToxicologia1.Text = "No";
            }
            else
            {
                this.lblToxicologia1.Text = "No Aplica";
            }

            if (this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Toxicologia2"].ToString() == "1")
            {
                this.lblToxicologia2.Text = "Si";
            }
            else if (this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Toxicologia2"].ToString() == "2")
            {
                this.lblToxicologia2.Text = "No";
            }
            else
            {
                this.lblToxicologia2.Text = "No Aplica";
            }

            if (this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Toxicologia3"].ToString() == "1")
            {
                this.lblToxicologia3.Text = "Si";
            }
            else if (this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Toxicologia3"].ToString() == "2")
            {
                this.lblToxicologia3.Text = "No";
            }
            else
            {
                this.lblToxicologia3.Text = "No Aplica";
            }

            this.lblNivelRecuperacion.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_CarRecuperacionRes"].ToString();
            this.lblHogar.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["HogarRecuperacionRes"].ToString();

            if (this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Fumado"].ToString() == "1")
            { this.lblInFumado.Text = "Si"; }
            else if (this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Fumado"].ToString() == "2")
            { this.lblInFumado.Text = "No"; };

            this.lblFrecuenciaFumado.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_FrecuenciaFumado"].ToString();
            this.lblNrFumado.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_CigarrosXDias"].ToString();



            //DSMV
            this.txtDSMVClinPrim.Visible = false;
            this.txtDSMVClinSec.Visible = false;
            this.txtDSMVClinTer.Visible = false;


            this.txtDSMVRMPrim.Visible = false;
            this.txtDSMVRMSec.Visible = false;
            this.txtDSMVRMTer.Visible = false;

            this.txtDSMVSusPrim.Visible = false;
            this.txtDSMVSusSec.Visible = false;
            this.txtDSMVSusTer.Visible = false;


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

            this.hlDSMVSusPrim.Visible = false;
            this.hlDSMVSusSec.Visible = false;
            this.hlDSMVSusTer.Visible = false;

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
            this.ddlToxicologia1.Visible = false;
            this.ddlToxicologia2.Visible = false;
            this.ddlToxicologia3.Visible = false;

            this.ddlNivelRecuperacion.Visible = false;
            this.txtHogar.Visible = false;
            this.ddlInFumado.Visible = false;
            this.ddlFrecuenciaFumado.Visible = false;
            this.txtNrFumado.Visible = false;
        }

		private void EditarRegistro()
		{
			this.lblNivelCuidadoSaludMental.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_SaludMental"].ToString();
			this.lblNivelCuidadoSustancias.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_AbusoSustancias"].ToString();

            nivelSM = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoMental"].ToString();
            nivelAS = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoSustancias"].ToString();

        }

        private void EditarRecuperacion()
        {
            if (nivelSM == "23" || nivelSM == "24" || nivelSM == "25" || nivelSM == "26" || nivelSM == "33")
            {
                if (isEvaluacion)
                {
                    this.RecuperacionDiv.Visible = true;
                }
                else
                {
                    this.RecuperacionDiv.Visible = false;
                }
            }
            else
            {
                this.RecuperacionDiv.Visible = false;
            }
        }
		private void ActualizarCamposCrear()
		{
                this.ddlDrogaPrim.SelectedValue = "0";
                this.ddlViaPrim.SelectedValue = "0";
                this.txtEdadPrim.SelectedValue = "97";
                this.ddlDrogaSec.SelectedValue = "0";
                this.ddlViaSec.SelectedValue = "0";
                this.txtEdadSec.SelectedValue = "97";
                this.ddlDrogaTerc.SelectedValue = "0";
                this.ddlViaTerc.SelectedValue = "0";
            this.txtEdadTerc.SelectedValue ="97";
		}
        void SetReadLblEdadInicio(Label lbl, string str)
        {
            if (str == "127")
                lbl.Text = "Desconocida";
            else if (str == "126")
                lbl.Text = "No Aplica";
            else if (str == "00" || str == "0" && DateTime.Parse(this.dsPerfil.SA_PERFIL.DefaultView[0]["FE_Perfil"].ToString()) >= Const.CambiosEnCamposNuevos.Date)
                lbl.Text = "Recién nacido";
            else
                lbl.Text = str;



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

            //this.txtDSMVClinPrim.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos1"].ToString();
            //this.hDSMVClinPrim.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_TrastornosClinicos1"].ToString();
            //this.txtDSMVClinSec.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos2"].ToString();
            //this.hDSMVClinSec.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_TrastornosClinicos2"].ToString();
            //this.txtDSMVClinTer.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos3"].ToString();
            //this.hDSMVClinTer.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_TrastornosClinicos3"].ToString();

            this.txtDSMVRMPrim.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosPersonalidadRM1"].ToString();
            this.hDSMVRMPrim.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_TrastornosPersonalidadRM1"].ToString();
            this.txtDSMVRMSec.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosPersonalidadRM2"].ToString();
            this.hDSMVRMSec.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_TrastornosPersonalidadRM2"].ToString();
            this.txtDSMVRMTer.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosPersonalidadRM3"].ToString();
            this.hDSMVRMTer.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_TrastornosPersonalidadRM3"].ToString();

            this.ddlDSMVPsicoAmbiPrim.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_ProblemasPsicosocialesAmbientales1"].ToString();
            this.ddlDSMVPsicoAmbiSec.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_ProblemasPsicosocialesAmbientales2"].ToString();
            this.ddlDSMVPsicoAmbiTer.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_ProblemasPsicosocialesAmbientales3"].ToString();

            //if ((this.txtDSMVRMPrim.Value == "") && (this.txtDSMVRMSec.Value == "") && (this.txtDSMVRMTer.Value == "") && (this.ddlDSMVPsicoAmbiPrim.SelectedValue == "0") && (this.ddlDSMVPsicoAmbiSec.SelectedValue == "0") && (this.ddlDSMVPsicoAmbiTer.SelectedValue == "0"))
            //{
            //    this.DSMVRM_DIV.Visible = false;
            //}

            //this.txtDSMVSusPrim.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Sustancias1"].ToString();
            //this.hDSMVSusPrim.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_Sustancias1"].ToString();
            //this.txtDSMVSusSec.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Sustancias2"].ToString();
            //this.hDSMVSusSec.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_Sustancias2"].ToString();
            //this.txtDSMVSusTer.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Sustancias3"].ToString();
            //this.hDSMVSusTer.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_Sustancias3"].ToString();

            //this.ddlDSMVDiagDual.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["IN_DiagnosticoDual"].ToString(); -> Este campo proviene del episodio, no del perfil
            this.ddlDSMVDiagDual.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_DSMV_DiagnosticoDual"].ToString();

            this.txtDSMVFnGlobal.Text = (this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_DSMV_FuncionamientoGlobal"].ToString() == "0") ? "" : this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_DSMV_FuncionamientoGlobal"].ToString();
            this.txtDSMVOtrasObs.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_OtrasObservaciones"].ToString();
            this.txtDSMVComentarios.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Comentarios"].ToString();
            #endregion
                this.ddlDrogaSec.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DrogaSecundario"].ToString();
                this.ddlViaSec.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_ViaSecundario"].ToString();
                this.txtEdadSec.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioSecundario"].ToString();
				this.ddlDrogaTerc.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DrogaTerciario"].ToString();
				this.ddlViaTerc.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_ViaTerciario"].ToString();
				this.txtEdadTerc.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioTerciario"].ToString();
                this.ddlDrogaPrim.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DrogaPrimario"].ToString();

                if (this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva1"].ToString() != "")
                {
                    this.txtDrogaPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva1"].ToString();
                }
                else
                {
                    this.Hogar_DIV.Style["visibility"] = "hidden";
                }

                if (this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva2"].ToString() != "")
                {
                    this.txtDrogaSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva2"].ToString();
                }
                else
                {
                    this.Hogar2_DIV.Style["visibility"] = "hidden";
                }

                if (this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva3"].ToString() != "")
                {
                    this.txtDrogaTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva3"].ToString();
                }
                else
                {
                    this.Hogar3_DIV.Style["visibility"] = "hidden";
                }

            this.ddlViaPrim.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_ViaPrimario"].ToString();
                this.txtEdadPrim.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioPrimario"].ToString();

            this.ddlToxicologia1.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Toxicologia1"].ToString();
            this.ddlToxicologia2.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Toxicologia2"].ToString();
            this.ddlToxicologia3.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Toxicologia3"].ToString();

            this.ddlNivelRecuperacion.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_CatRecuperacionRes"].ToString();
            if(this.ddlNivelRecuperacion.SelectedValue != "99")
            {
                this.txtHogar.Enabled = true;
            }
            this.txtHogar.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["HogarRecuperacionRes"].ToString();

            this.ddlInFumado.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Fumado"].ToString();
            this.ddlFrecuenciaFumado.SelectedItem.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_FrecuenciaFumado"].ToString();
            this.txtNrFumado.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_CigarrosXDias"].ToString();

            //DateTime fe_perfil = DateTime.Parse(this.dsPerfil.SA_PERFIL.DefaultView[0]["FE_Perfil"].ToString());
            //DateTime limite = new DateTime(2021, 1, 1);
            //if (fe_perfil > limite)
            //{
            //    DSMVRM_DIV.Visible = false;
            //}
        }

        public void ReorderDSMV(frmAction mode)
        {

            var dsmv1 = new DSMV() { Pk = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_TrastornosClinicos1"].ToString(), Diagnostico = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos1"].ToString(), Categoria = this.dsPerfil.SA_PERFIL.DefaultView[0]["CAT_DSMV_TrastornosClinicos1"].ToString(), Orden = 1 };
            var dsmv2 = new DSMV() { Pk = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_TrastornosClinicos2"].ToString(), Diagnostico = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos2"].ToString(), Categoria = this.dsPerfil.SA_PERFIL.DefaultView[0]["CAT_DSMV_TrastornosClinicos2"].ToString(), Orden = 2 };
            var dsmv3 = new DSMV() { Pk = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_TrastornosClinicos3"].ToString(), Diagnostico = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos3"].ToString(), Categoria = this.dsPerfil.SA_PERFIL.DefaultView[0]["CAT_DSMV_TrastornosClinicos3"].ToString(), Orden = 3 };
            var dsmv4 = new DSMV() { Pk = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_Sustancias1"].ToString(), Diagnostico = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Sustancias1"].ToString(), Categoria = this.dsPerfil.SA_PERFIL.DefaultView[0]["CAT_DSMV_Sustancias1"].ToString(), Orden = 4 };
            var dsmv5 = new DSMV() { Pk = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_Sustancias2"].ToString(), Diagnostico = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Sustancias2"].ToString(), Categoria = this.dsPerfil.SA_PERFIL.DefaultView[0]["CAT_DSMV_Sustancias2"].ToString(), Orden = 5 };
            var dsmv6 = new DSMV() { Pk = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_Sustancias3"].ToString(), Diagnostico = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Sustancias3"].ToString(), Categoria = this.dsPerfil.SA_PERFIL.DefaultView[0]["CAT_DSMV_Sustancias3"].ToString(), Orden = 6 };

            List<DSMV> listDSMV = new List<DSMV>();
            listDSMV.Add(dsmv1);
            listDSMV.Add(dsmv2);
            listDSMV.Add(dsmv3);
            listDSMV.Add(dsmv4);
            listDSMV.Add(dsmv5);
            listDSMV.Add(dsmv6);



            //Segregar los diagnosticos
            var sust = listDSMV.FindAll(x => x.Categoria == "SUST");
            var sm = listDSMV.FindAll(x => x.Categoria == "SM");
            var other = listDSMV.FindAll(x => x.Categoria != "SUST" && x.Categoria != "SM" && x.Pk != "" && x.Pk != "761");


            if (other.Count > 0)
            {
                foreach (var dsmv in other)
                {
                    if (dsmv.Orden >= 1 && dsmv.Orden <= 3)
                    {
                        sm.Add(dsmv);
                    }
                    else
                    {
                        sust.Add(dsmv);
                    }
                }
            }

            var countSM = sm.Count;
            var countSust = sust.Count;


            if (mode == frmAction.Read)
            {



                if (countSM >= 1)
                    this.lblDSMVClinPrim.Text = sm[0].Diagnostico;
                else
                    this.lblDSMVClinPrim.Text = "No se recopila la información";



                if (countSM >= 2)
                    this.lblDSMVClinSec.Text = sm[1].Diagnostico;
                else
                    this.lblDSMVClinSec.Text = "No se recopila la información";


                if (countSM >= 3)
                    this.lblDSMVClinTer.Text = sm[2].Diagnostico;
                else
                    this.lblDSMVClinTer.Text = "No se recopila la información";

                if (countSust >= 1)
                    this.lblDSMVSusPrim.Text = sust[0].Diagnostico;
                else
                    this.lblDSMVSusPrim.Text = "No se recopila la información";

                if (countSust >= 2)
                    this.lblDSMVSusSec.Text = sust[1].Diagnostico;
                else
                    this.lblDSMVSusSec.Text = "No se recopila la información";

                if (countSust >= 3)
                    this.lblDSMVSusTer.Text = sust[2].Diagnostico;
                else
                    this.lblDSMVSusTer.Text = "No se recopila la información";

            }
            else if (mode == frmAction.Update)
            {


                if (countSM >= 1)
                {
                    this.txtDSMVClinPrim.Value = sm[0].Diagnostico;
                    this.hDSMVClinPrim.Value = sm[0].Pk;
                }
                else
                    this.hDSMVClinPrim.Value = "761";


                if (countSM >= 2)
                {
                    this.txtDSMVClinSec.Value = sm[1].Diagnostico;
                    this.hDSMVClinSec.Value = sm[1].Pk;
                }
                else
                    this.hDSMVClinSec.Value = "761";

                if (countSM >= 3)
                {
                    this.txtDSMVClinTer.Value = sm[2].Diagnostico;
                    this.hDSMVClinTer.Value = sm[2].Pk;
                }
                else
                    this.hDSMVClinTer.Value = "761";




                if (countSust >= 1)
                {
                    this.txtDSMVSusPrim.Value = sust[0].Diagnostico;
                    this.hDSMVSusPrim.Value = sust[0].Pk;
                }
                else
                    this.hDSMVSusPrim.Value = "761";


                if (countSust >= 2)
                {
                    this.txtDSMVSusSec.Value = sust[1].Diagnostico;
                    this.hDSMVSusSec.Value = sust[1].Pk;
                }
                else
                    this.hDSMVSusSec.Value = "761";


                if (countSust >= 3)
                {

                    this.txtDSMVSusTer.Value = sust[2].Diagnostico;
                    this.hDSMVSusTer.Value = sust[2].Pk;
                }
                else
                    this.hDSMVSusTer.Value = "761";
            }

        }


        /**
          * File: wuEpisodioPerfil.ascx.cs
          * Fecha: 1/ENE/2022
          * Editado por: Jose A. Ramos De La Cruz
          * Proposito: Arreglar dependendias de ddlInfumado.        
          */


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

            this.dvwNivelMental = new System.Data.DataView();

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

            ((System.ComponentModel.ISupportInitialize)(this.dvwNivelMental)).BeginInit();

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

            ((System.ComponentModel.ISupportInitialize)(this.dvwNivelMental)).EndInit();

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
                //Int32.TryParse(this.hDSMVRMPrim.Value.ToString(), out retVal);
                return retVal;
            }
        }
        public int @FK_DSMV_TrastornosPersonalidadRM2
        {
            get
            {
                int retVal = 0;
               // Int32.TryParse(this.hDSMVRMSec.Value.ToString(), out retVal);
                return retVal;
            }
        }
        public int @FK_DSMV_TrastornosPersonalidadRM3
        {
            get
            {
                int retVal = 0;
                //Int32.TryParse(this.hDSMVRMTer.Value.ToString(), out retVal);
                return retVal;
            }
        }
        public int @FK_DSMV_ProblemasPsicosocialesAmbientales1
        {
            get
            {
                int retVal = 0;
                //Int32.TryParse(this.ddlDSMVPsicoAmbiPrim.SelectedValue.ToString(), out retVal);
                return retVal;
            }
        }
        public int @FK_DSMV_ProblemasPsicosocialesAmbientales2
        {
            get
            {
                int retVal = 0;
                //Int32.TryParse(this.ddlDSMVPsicoAmbiSec.SelectedValue.ToString(), out retVal);
                return retVal;
            }
        }
        public int @FK_DSMV_ProblemasPsicosocialesAmbientales3
        {
            get
            {
                int retVal = 0;
               // Int32.TryParse(this.ddlDSMVPsicoAmbiTer.SelectedValue.ToString(), out retVal);
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

        /*DSMV Sustancias*/

        public int @FK_DSMV_Sustancias1
        {
            get
            {
                int retVal = 0;
                Int32.TryParse(this.hDSMVSusPrim.Value.ToString(), out retVal);
                return retVal;
            }
        }
        public int @FK_DSMV_Sustancias2
        {
            get
            {
                int retVal = 0;
                Int32.TryParse(this.hDSMVSusSec.Value.ToString(), out retVal);
                return retVal;
            }
        }

        public int @FK_DSMV_Sustancias3
        {
            get
            {
                int retVal = 0;
                Int32.TryParse(this.hDSMVSusTer.Value.ToString(), out retVal);
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
                    //    return Convert.ToSByte(this.txtEdadPrim.Text.Trim());
                    return Convert.ToSByte(this.txtEdadPrim.SelectedValue.ToString());
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
                    //return Convert.ToSByte(this.txtEdadSec.Text.Trim());
                    return Convert.ToSByte(this.txtEdadSec.SelectedValue.ToString());
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
                    //return Convert.ToSByte(this.txtEdadTerc.Text.Trim());
                    return Convert.ToSByte(this.txtEdadTerc.SelectedValue.ToString());
                }
                catch
                {
                    return 0;
                }
            }
        }

        public sbyte @FK_CatRecuperacionRes
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlNivelRecuperacion.SelectedValue.ToString());
                }
                catch
                {
                    return 99;//Default No aplica
                }
            }
        }


        public string @HogarRecuperacionRes
        {
            get
            {
                return this.txtHogar.Text;
            }
        }

        public sbyte @IN_Fumado
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlInFumado.SelectedValue.ToString());
                }
                catch
                {
                    return 0;//Default No aplica
                }
            }
        }

        public string @DE_FrecuenciaFumado
        {
            get
            {
                return this.ddlFrecuenciaFumado.SelectedItem.Text;
            }
        }

        public int @NR_CigarrosXDias
        {
            get
            {
                int retVal = 0;
                Int32.TryParse(this.txtNrFumado.Text, out retVal);
                return retVal;
            }
        }

        public string @DE_DrogaNueva1
        {
            get
            {
                return this.txtDrogaPrim.Text;
            }
        }

        public string @DE_DrogaNueva2
        {
            get
            {
                return this.txtDrogaSec.Text;
            }
        }

        public string @DE_DrogaNueva3
        {
            get
            {
                return this.txtDrogaTerc.Text;
            }
        }

        public sbyte @IN_Toxicologia1
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlToxicologia1.SelectedValue.ToString());
                }
                catch
                {
                    return 0;//Default No aplica
                }
            }
        }

        public sbyte @IN_Toxicologia2
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlToxicologia2.SelectedValue.ToString());
                }
                catch
                {
                    return 0;//Default No aplica
                }
            }
        }

        public sbyte @IN_Toxicologia3
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlToxicologia3.SelectedValue.ToString());
                }
                catch
                {
                    return 0;//Default No aplica
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
                case (179):
                case (180):
                case (182):
                    esProgramaMental = true; break;
                default: break;
            }
            return esProgramaMental;
        }

    }
    public class DropDownAgeAbusoDeSustancia
    {

        public int Value { get; set; }
        public string Text { get; set; }

    }
}
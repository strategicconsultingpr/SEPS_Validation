using System.Collections.Generic;

namespace ASSMCA.Perfiles
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Threading;
    public partial class wucEpisodioAdmision : System.Web.UI.UserControl
    {
        protected ASSMCA.perfiles.dsPerfil dsPerfil;
        protected System.Web.UI.WebControls.DropDownList ddlEstadoLegao;
        protected System.Data.DataView dvwEpisPreviosSustancias, dvwEpisPreviosMental, dvwUltSustancias, dvwUltMental, dvwNivelSustancias, dvwNivelMental, dvwDiagPrimario, dvwDiagSecundario, dvwDiagTerciario, dvwCatTransPrim, dvwCatTransSec, dvwCatTransTerc, dvwCatRMPrim, dvwCatRMSec, dvwCatRMTerc, dvwIVPrim, dvwIVSec, dvwIVTerc, dvwDrogaPrim, dvwDrogaSec, dvwDrogaTerc, dvwViaPrim, dvwViaTerc, dvwViaSec, dvwFrecPrim, dvwFrecSec, dvwFrecTerc, dvwMediPrim, dvwMediTerc, dvwMediSec, dvwNivelMentalAnterior, dvwNivelSustanciasAnterior, dvwFreqAutoAyuda, dvwFuenteReferido, dvw_DSMV_ProblemasPsicosocialesAmbientales1, dvw_DSMV_ProblemasPsicosocialesAmbientales2, dvw_DSMV_ProblemasPsicosocialesAmbientales3;
        public frmAction m_frmAction;
        private int _probJusticiaCount, _maltratoCount, m_pk_perfil, m_pk_episodio, _pkPrograma, m_CO_Tipo;


        private List<DropDownAgeAbusoDeSustancia> ListAgeAbusoDeSustancia = new List<DropDownAgeAbusoDeSustancia>();

        public string accion;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // rvEdadPrim.MaximumValue = Session["edad"].ToString();
            rvEdadSec.MaximumValue = Session["edad"].ToString();
            rvEdadTerc.MaximumValue = Session["edad"].ToString();
            _pkPrograma = Convert.ToInt32(this.Session["pk_programa"]);
            m_CO_Tipo = Convert.ToInt32(this.Session["co_tipo"].ToString());
            this.CO_Tipo.Value = this.Session["co_tipo"].ToString();
            this.hAccion.Value = accion;
            if (!this.IsPostBack)
            {
                this.dsPerfil = (ASSMCA.perfiles.dsPerfil)this.Session["dsPerfil"];
                this.dvwFuenteReferido.Table = this.dsPerfil.SA_LKP_TEDS_REFERIDO;
                this.dvwEpisPreviosSustancias.Table = this.dsPerfil.SA_LKP_TEDS_EPISODIO_PREVIO;
                this.dvwEpisPreviosMental.Table = this.dsPerfil.SA_LKP_TEDS_EPISODIO_PREVIO;
                this.dvwUltSustancias.Table = this.dsPerfil.SA_LKP_TIEMPO_ULT_TRAT;
                this.dvwUltMental.Table = this.dsPerfil.SA_LKP_TIEMPO_ULT_TRAT;
                this.dvwNivelSustancias.Table = this.dsPerfil.SA_LKP_ABUSO_SUSTANCIAS;
                this.dvwNivelMental.Table = this.dsPerfil.SA_LKP_SALUD_MENTAL;
                this.dvwDiagPrimario.Table = this.dsPerfil.SA_LKP_DIAGNOSTICO;
                this.dvwDiagSecundario.Table = this.dsPerfil.SA_LKP_DIAGNOSTICO;
                this.dvwDiagTerciario.Table = this.dsPerfil.SA_LKP_DIAGNOSTICO;
                this.dvwCatRMPrim.Table = this.dsPerfil.SA_LKP_DSMIV_CAT;
                this.dvwCatRMSec.Table = this.dsPerfil.SA_LKP_DSMIV_CAT;
                this.dvwCatRMTerc.Table = this.dsPerfil.SA_LKP_DSMIV_CAT;
                this.dvwCatTransPrim.Table = this.dsPerfil.SA_LKP_DSMIV_CAT;
                this.dvwCatTransSec.Table = this.dsPerfil.SA_LKP_DSMIV_CAT;
                this.dvwCatTransTerc.Table = this.dsPerfil.SA_LKP_DSMIV_CAT;
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
                this.dvwNivelSustanciasAnterior.Table = this.dsPerfil.SA_LKP_ABUSO_SUSTANCIAS;
                this.dvwNivelMentalAnterior.Table = this.dsPerfil.SA_LKP_SALUD_MENTAL;
                this.dvwFreqAutoAyuda.Table = this.dsPerfil.SA_LKP_TEDS_FRECUENCIA_AUTOAYUDA;
                this.ManageCondicionesDiagnosticadas(this.m_frmAction);

                ListAgeAbusoDeSustancia.Add(new DropDownAgeAbusoDeSustancia { Value = 96, Text = "No Aplica" });
                ListAgeAbusoDeSustancia.Add(new DropDownAgeAbusoDeSustancia { Value = 97, Text = "Desconocida" });
                ListAgeAbusoDeSustancia.Add(new DropDownAgeAbusoDeSustancia { Value = 98, Text = "No Recogida" });
                ListAgeAbusoDeSustancia.Add(new DropDownAgeAbusoDeSustancia { Value = 00, Text = "Recién nacido" });

                for (int i = 1; i <= Convert.ToInt32(Session["edad"].ToString()); i++)
                    ListAgeAbusoDeSustancia.Add(new DropDownAgeAbusoDeSustancia { Value = i, Text = i.ToString() });


                txtEdadPrim.DataSource = ListAgeAbusoDeSustancia;
                txtEdadPrim.DataValueField = "Value";
                txtEdadPrim.DataTextField = "Text";
              
                txtEdadPrim.DataBind();
                


                if (this.Session["pk_administracion"].ToString() == "1")
                {
                    this.dvwFuenteReferido.RowFilter = "PK_Referido <> 30";
                }
                this.DataBind();
                this.load();
                switch (this.m_frmAction)
                {
                    case (frmAction.Create):
                        //this.SetTabIndex();
                        this.EditarRegistro();
                        this.DSMIV_DIV.Visible = false;
                        this.DSMVRM_DIV.Visible = false;
                        this.ddlNivelUnavilable(NivelCuidado.Mental);
                        this.ddlNivelUnavilable(NivelCuidado.Sustancias);
                       
                        break;
                    case (frmAction.Update):
                        //this.SetTabIndex();
                        this.EditarRegistro();
                        this.ActualizarCampos();
                        this.ddlNivelUnavilable(NivelCuidado.Mental);
                        this.ddlNivelUnavilable(NivelCuidado.Sustancias);
                        break;
                    case (frmAction.Read):
                        this.LeerRegistro();
                        break;
                    default:
                        break;
                }

                //                SetEdadChk(!chkEdadPrim.Checked, txtEdadPrim);
                //                SetEdadChk(!chkEdadSec.Checked, txtEdadSec);

                //SetEdadChk(chkEdadPrim.Checked, txtEdadTerc);


            }
        }

        /* //DELETEABLE
        private void SetTabIndex()
        {
            this.ddlDrogaPrim.TabIndex = (short)(this.ddlDSMVDiagDual.TabIndex + 1);
            this.ddlViaPrim.TabIndex = (short)(this.ddlDrogaPrim.TabIndex + 1);
            this.ddlFrecPrim.TabIndex = (short)(this.ddlViaPrim.TabIndex + 1);
            this.txtEdadPrim.TabIndex = (short)(this.ddlFrecPrim.TabIndex + 1);

            this.ddlDrogaSec.TabIndex = (short)(this.txtEdadPrim.TabIndex + 1);
            this.ddlViaSec.TabIndex = (short)(this.ddlDrogaSec.TabIndex + 1);
            this.ddlFrecSec.TabIndex = (short)(this.ddlViaSec.TabIndex + 1);
            this.txtEdadSec.TabIndex = (short)(this.ddlFrecSec.TabIndex + 1);

            this.ddlDrogaTerc.TabIndex = (short)(this.txtEdadSec.TabIndex + 1);
            this.ddlViaTerc.TabIndex = (short)(this.ddlDrogaTerc.TabIndex + 1);
            this.ddlFrecTerc.TabIndex = (short)(this.ddlViaTerc.TabIndex + 1);
            this.txtEdadTerc.TabIndex = (short)(this.ddlFrecTerc.TabIndex + 1);
        }*/

        private void dataEdit()
        {
            Button1.Visible = true;
            Button2.Visible = true;
            Button3.Visible = true;
            Button4.Visible = true;
        }

        private void dataRead()
        {
            Button1.Visible = false;
            Button2.Visible = false;
            Button3.Visible = false;
            Button4.Visible = false;
        }


        private void LeerRegistro()
        {
            this.dataRead();
            this.lblArrestado.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_ArrestadoAnteriormente"].ToString();
            this.lblArrestado30.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Arrestado30dias"].ToString();
            this.lblArrestos30.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_Arrestos30dias"].ToString();

            //DSM IV if its available
            this.lblClinPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TCP"].ToString();
            this.lblClinSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TCS"].ToString();
            this.lblClinTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TCT"].ToString();
            this.lblRMPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TPP"].ToString();
            this.lblRMSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TPS"].ToString();
            this.lblRMTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TPT"].ToString();
            this.lblIIIP.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["CO_CondicionesMedicasPrimario"].ToString();
            this.lblIIIS.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["CO_CondicionesMedicasSecundario"].ToString();
            this.lblIIIT.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["CO_CondicionesMedicasTerciario"].ToString();
            this.lblIVPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_IV_P"].ToString();
            this.lblIVSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_IV_S"].ToString();
            this.lblIVTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_IV_T"].ToString();
            this.lblDual.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_DiagnosticoDual"].ToString();
            this.lblEscalaGAF.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_EscalaGAF"].ToString();
            if ((this.lblIVPrim.Text == "") && (this.lblIVSec.Text == "") && (this.lblIVTerc.Text == "") && (this.lblClinPrim.Text == "") && (this.lblClinSec.Text == "") && (this.lblClinTerc.Text == "") && (this.lblRMPrim.Text == "") && (this.lblRMSec.Text == "") && (this.lblRMTerc.Text == ""))
            {
                DSMIV_DIV.Visible = false;
            }

            #region DSMV
            this.lblDSMVClinPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos1"].ToString();
            this.lblDSMVClinSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos2"].ToString();
            this.lblDSMVClinTer.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosClinicos3"].ToString();


            this.lblDSMVRMPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosPersonalidadRM1"].ToString();
            this.lblDSMVRMSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosPersonalidadRM2"].ToString();
            this.lblDSMVRMTer.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_TrastornosPersonalidadRM3"].ToString();
            this.lblDSMVPsicoAmbiPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_ProblemasPsicosocialesAmbientales1"].ToString();
            this.lblDSMVPsicoAmbiSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_ProblemasPsicosocialesAmbientales2"].ToString();
            this.lblDSMVPsicoAmbiTer.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_ProblemasPsicosocialesAmbientales3"].ToString();
            this.lblDSMVDiagDual.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_DiagnosticoDual"].ToString();


            DateTime fe_perfil = DateTime.Parse(this.dsPerfil.SA_PERFIL.DefaultView[0]["FE_Perfil"].ToString());
            DateTime limite = new DateTime(2021, 1, 1);
            if (fe_perfil > limite)
            {
                DSMVRM_DIV.Visible = false;
            }

            if ((this.lblDSMVRMPrim.Text == "") && (this.lblDSMVRMSec.Text == "") && (this.lblDSMVRMTer.Text == "") && (this.lblDSMVPsicoAmbiPrim.Text == "") && (this.lblDSMVPsicoAmbiSec.Text == "") && (this.lblDSMVPsicoAmbiTer.Text == ""))
            {
                this.DSMVRM_DIV.Visible = false;
            }

            this.lblDSMVSusPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Sustancias1"].ToString();
            this.lblDSMVSusSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Sustancias2"].ToString();
            this.lblDSMVSusTer.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Sustancias3"].ToString();


            this.lblDSMVFnGlobal.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_DSMV_FuncionamientoGlobal"].ToString();
            this.lblDSMVOtrasObs.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_OtrasObservaciones"].ToString();
            this.lblDSMVComentarios.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Comentarios"].ToString();
            #endregion
            this.lblCodependiente.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_CodDependiente"].ToString();
            this.lblDíasMental.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["NR_DiasEsperaMental"].ToString();
            this.lblDíasMentUlt.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["NR_DiasUltimaAltaMental"].ToString();
            this.lblDíasSustancias.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["NR_DiasEsperaSustancias"].ToString();
            this.lblDíasSustUlt.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["NR_DiasUltimaAltaSustancias"].ToString();
            this.lblDrogaPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Droga_P"].ToString();
            this.lblDrogaSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Droga_S"].ToString();
            this.lblDrogaTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Droga_T"].ToString();
            this.lblEdadPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioPrimario"].ToString();
            this.lblEdadSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioSecundario"].ToString();
            this.lblEdadTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioTerciario"].ToString();
            this.lblEstadoLegal.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_EstadoLegal"].ToString();
            this.lblEtapaServicio.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_EtapaServicio"].ToString();
            this.lblFrecPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Frecuencia_P"].ToString();
            this.lblFrecSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Frecuencia_S"].ToString();
            this.lblFrecTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Frecuencia_T"].ToString();
            this.lblFuenteReferido.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_Referido"].ToString();
            this.lblMaltratoNinez.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_Maltrato"].ToString();
            this.lblMesesMentUlt.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["NR_MesesUltimaAltaMental"].ToString();
            this.lblMesesSustUlt.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["NR_MesesUltimaAltaSustancias"].ToString();
            this.lblMetadona.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_Metadona"].ToString();
            this.lblNivelCuidadoSaludMental.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_SaludMental"].ToString();
            this.lblNivelCuidadoSustancias.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_AbusoSustancias"].ToString();
            this.lblNivelSustancias.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_AbusoSustanciasAnterior"].ToString();
            this.lblNivelMental.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_SaludMentalAnterior"].ToString();
            this.lblPreviosMental.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_EpisodiosPreviosMental"].ToString();
            this.lblPreviosSustancias.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_EpisodiosPreviosSustancias"].ToString();
            this.lblFreq_AutoAyuda.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_FreqAutoayuda"].ToString();
            //this.lblReunionesGrupos.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_ParticReunGrupos"].ToString();
            this.lblSuicidios.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_Suicida"].ToString();
            this.lblIdeaSuicida.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_IdeaSuicida"].ToString();
            this.lblUltMental.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_TiempoUltTratMental"].ToString();
            this.lblUltSustancias.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_TiempoUltTratSustancias"].ToString();
            this.lblViaPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Via_P"].ToString();
            this.lblViaSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Via_S"].ToString();
            this.lblViaTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Via_T"].ToString();
            this.lblVioDomestic.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_ViolenciaDomestica"].ToString();
            this.ddlArrestado.Visible = false;
            this.ddlArrestado30.Visible = false;

            if (this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva1"].ToString() != "")
            {
                this.lblDrogaPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva1"].ToString();
            }
            this.Hogar_DIV.Visible = false;

            if (this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva2"].ToString() != "")
            {
                this.lblDrogaSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva2"].ToString();
            }
            this.Hogar2_DIV.Visible = false;

            if (this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva3"].ToString() != "")
            {
                this.lblDrogaTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DrogaNueva3"].ToString();
            }
            this.Hogar3_DIV.Visible = false;

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

            if (this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Fumado"].ToString() == "1")
            { this.lblInFumado.Text = "Si"; }
            else { this.lblInFumado.Text = "No"; };

            this.lblFrecuenciaFumado.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_FrecuenciaFumado"].ToString();
            this.lblNrFumado.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_CigarrosXDias"].ToString();

            //DSMV
            this.txtDSMVClinPrim.Visible = false;
            this.txtDSMVClinSec.Visible = false;
            this.txtDSMVClinTer.Visible = false;

            /*Campos eliminados 12/2020 */
            this.txtDSMVRMPrim.Visible = false;
            this.txtDSMVRMSec.Visible = false;
            this.txtDSMVRMTer.Visible = false;
            this.txtDSMVOtrasObs.Visible = false;
            this.txtDSMVComentarios.Visible = false;
            this.ddlDSMVDiagDual.Visible = false;

            /*Campos eliminados 12/2020 */
            this.ddlDSMVPsicoAmbiPrim.Visible = false;
            this.ddlDSMVPsicoAmbiSec.Visible = false;
            this.ddlDSMVPsicoAmbiTer.Visible = false;
            this.txtDSMVFnGlobal.Visible = false;
            this.hlDSMVClinPrim.Visible = false;
            this.hlDSMVClinSec.Visible = false;
            this.hlDSMVClinTer.Visible = false;

            /*Campos eliminados 12/2020 */
            this.hlDSMVRMPrim.Visible = false;
            this.hlDSMVRMSec.Visible = false;
            this.hlDSMVRMTer.Visible = false;

            this.txtDSMVSusPrim.Visible = false;
            this.txtDSMVSusSec.Visible = false;
            this.txtDSMVSusTer.Visible = false;

            this.ddlCodependiente.Visible = false;
            this.ddlDrogaPrim.Visible = false;
            this.ddlDrogaSec.Visible = false;
            this.ddlDrogaTerc.Visible = false;
            this.ddlEstadoLegal.Visible = false;
            this.ddlEtapaServicio.Visible = false;
            this.ddlFrecPrim.Visible = false;
            this.ddlFrecSec.Visible = false;
            this.ddlFrecTerc.Visible = false;
            this.ddlFuenteReferido.Visible = false;
            this.ddlMaltratoNinez.Visible = false;
            this.ddlMetadona.Visible = false;
            this.ddlNivelCuidadoSaludMental.Visible = false;
            this.ddlNivelCuidadoSustancias.Visible = false;
            this.ddlNivelMental.Visible = false;
            this.ddlNivelSustancias.Visible = false;
            this.ddlPreviosMental.Visible = false;
            this.ddlPreviosSustancias.Visible = false;

            /*Campos eliminados 12/2020 */
            //this.ddlReunionesGrupos.Visible = false;

            this.ddlFreq_AutoAyuda.Visible = false;
            this.ddlSuicidios.Visible = false;
            this.ddlIdeaSuicida.Visible = false;
            this.ddlUltMental.Visible = false;
            this.ddlUltSustancias.Visible = false;
            this.ddlViaPrim.Visible = false;
            this.ddlViaSec.Visible = false;
            this.ddlViaTerc.Visible = false;
            this.ddlVioDomestic.Visible = false;
            this.txtArrestos30.Visible = false;
            this.txtDíasMental.Visible = false;
            this.txtDíasMentUlt.Visible = false;
            this.txtDíasSustancias.Visible = false;
            this.txtDíasSustUlt.Visible = false;
            this.txtEdadPrim.Visible = false;
            this.txtEdadSec.Visible = false;
            this.txtEdadTerc.Visible = false;
            this.txtMesesMentUlt.Visible = false;
            this.txtMesesSustUlt.Visible = false;


            this.ddlToxicologia1.Visible = false;
            this.ddlToxicologia2.Visible = false;
            this.ddlToxicologia3.Visible = false;


            this.ddlInFumado.Visible = false;
            this.ddlFrecuenciaFumado.Visible = false;
            this.txtNrFumado.Visible = false;
        }

        private void EditarRegistro()
        {
            this.dataEdit();
        }


        private void ActualizarCampos()
        {
            this.dataEdit();
            this.ddlArrestado.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["IN_ArrestadoAnteriormente"].ToString();
            this.ddlArrestado30.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Arrestado30dias"].ToString();
            //DSM IV if available 
            this.lblClinPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TCP"].ToString();
            this.lblClinSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TCS"].ToString();
            this.lblClinTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TCT"].ToString();
            this.lblRMPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TPP"].ToString();
            this.lblRMSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TPS"].ToString();
            this.lblRMTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_TPT"].ToString();
            this.lblIIIP.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["CO_CondicionesMedicasPrimario"].ToString();
            this.lblIIIS.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["CO_CondicionesMedicasSecundario"].ToString();
            this.lblIIIT.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["CO_CondicionesMedicasTerciario"].ToString();
            this.lblIVPrim.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_IV_P"].ToString();
            this.lblIVSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_IV_S"].ToString();
            this.lblIVTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMIV_IV_T"].ToString();
            this.lblDual.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_DiagnosticoDual"].ToString();
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

            if ((this.txtDSMVRMPrim.Value == "") && (this.txtDSMVRMSec.Value == "") && (this.txtDSMVRMTer.Value == "") && (this.ddlDSMVPsicoAmbiPrim.SelectedValue == "0") && (this.ddlDSMVPsicoAmbiSec.SelectedValue == "0") && (this.ddlDSMVPsicoAmbiTer.SelectedValue == "0"))
            {
                this.DSMVRM_DIV.Visible = false;
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

            if (this.hDSMVSusPrim.Value == "")
            {
                this.hDSMVSusPrim.Value = "761";
            }
            if (this.hDSMVSusSec.Value == "")
            {
                this.hDSMVSusSec.Value = "761";
            }
            if (this.hDSMVSusTer.Value == "")
            {
                this.hDSMVSusTer.Value = "761";
            }

            this.ddlDSMVPsicoAmbiPrim.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_ProblemasPsicosocialesAmbientales1"].ToString();
            this.ddlDSMVPsicoAmbiSec.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_ProblemasPsicosocialesAmbientales2"].ToString();
            this.ddlDSMVPsicoAmbiTer.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_ProblemasPsicosocialesAmbientales3"].ToString();

            this.txtDSMVSusPrim.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Sustancias1"].ToString();
            this.hDSMVSusPrim.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_Sustancias1"].ToString();
            this.txtDSMVSusSec.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Sustancias2"].ToString();
            this.hDSMVSusSec.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_Sustancias2"].ToString();
            this.txtDSMVSusTer.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Sustancias3"].ToString();
            this.hDSMVSusTer.Value = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DSMV_Sustancias3"].ToString();

            this.ddlDSMVDiagDual.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_DSMV_DiagnosticoDual"].ToString();
            this.txtDSMVFnGlobal.Text = (this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_DSMV_FuncionamientoGlobal"].ToString() == "0") ? "" : this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_DSMV_FuncionamientoGlobal"].ToString();
            this.txtDSMVOtrasObs.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_OtrasObservaciones"].ToString();
            this.txtDSMVComentarios.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DSMV_Comentarios"].ToString();
            #endregion
            this.ddlCodependiente.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["IN_CodDependiente"].ToString();
            this.ddlDrogaPrim.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DrogaPrimario"].ToString();
            this.ddlDrogaSec.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DrogaSecundario"].ToString();
            this.ddlDrogaTerc.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_DrogaTerciario"].ToString();
            this.ddlEstadoLegal.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_EstadoLegal"].ToString();
            this.ddlEtapaServicio.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_EtapaServicio"].ToString();
            this.ddlFrecPrim.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_FrecuenciaPrimario"].ToString();
            this.ddlFrecSec.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_FrecuenciaSecundario"].ToString();
            this.ddlFrecTerc.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_FrecuenciaTerciario"].ToString();
            this.ddlFuenteReferido.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_FuenteReferido"].ToString();
            this.ddlMaltratoNinez.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["IN_Maltrato"].ToString();
            this.ddlMetadona.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["IN_Metadona"].ToString();
            this.ddlNivelCuidadoSaludMental.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoMental"].ToString();
            this.ddlNivelCuidadoSustancias.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoSustancias"].ToString();
            this.ddlNivelMental.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoMentalAnterior"].ToString();
            this.ddlNivelSustancias.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoSustanciasAnterior"].ToString();
            this.ddlPreviosMental.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_EpisodiosMental"].ToString();
            this.ddlPreviosSustancias.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_EpisodiosSustancias"].ToString();
            //this.ddlReunionesGrupos.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_ParticReunGrupos"].ToString();
            this.ddlFreq_AutoAyuda.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_FreqAutoAyuda"].ToString();
            this.ddlSuicidios.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["IN_Suicida"].ToString();
            this.ddlIdeaSuicida.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["IN_IdeaSuicida"].ToString();
            this.ddlUltMental.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_DuracionMental"].ToString();
            this.ddlUltSustancias.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_DuracionSustancias"].ToString();
            this.ddlViaPrim.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_ViaPrimario"].ToString();
            this.ddlViaSec.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_ViaSecundario"].ToString();
            this.ddlViaTerc.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_ViaTerciario"].ToString();
            this.ddlVioDomestic.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["IN_ViolenciaDomestica"].ToString();
            this.txtArrestos30.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_Arrestos30dias"].ToString();
            this.txtDíasMental.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["NR_DiasEsperaMental"].ToString();
            this.txtDíasMentUlt.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["NR_DiasUltimaAltaMental"].ToString();
            this.txtDíasSustancias.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["NR_DiasEsperaSustancias"].ToString();
            this.txtDíasSustUlt.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["NR_DiasUltimaAltaSustancias"].ToString();


            this.txtEdadPrim.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioPrimario"].ToString();
            this.txtEdadSec.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioSecundario"].ToString();
            this.txtEdadTerc.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EdadInicioTerciario"].ToString();



            this.txtMesesMentUlt.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["NR_MesesUltimaAltaMental"].ToString();
            this.txtMesesSustUlt.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["NR_MesesUltimaAltaSustancias"].ToString();


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

            this.ddlToxicologia1.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Toxicologia1"].ToString();
            this.ddlToxicologia2.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Toxicologia2"].ToString();
            this.ddlToxicologia3.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Toxicologia3"].ToString();

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

        #region Código generado por el Diseñador de Web Forms
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.dsPerfil = new ASSMCA.perfiles.dsPerfil();
            this.dvwEpisPreviosSustancias = new System.Data.DataView();
            this.dvwEpisPreviosMental = new System.Data.DataView();
            this.dvwUltSustancias = new System.Data.DataView();
            this.dvwUltMental = new System.Data.DataView();
            this.dvwNivelSustancias = new System.Data.DataView();
            this.dvwNivelMental = new System.Data.DataView();
            this.dvwDiagPrimario = new System.Data.DataView();
            this.dvwDiagSecundario = new System.Data.DataView();
            this.dvwDiagTerciario = new System.Data.DataView();
            this.dvwCatTransPrim = new System.Data.DataView();
            this.dvwCatTransSec = new System.Data.DataView();
            this.dvwCatTransTerc = new System.Data.DataView();
            this.dvwCatRMPrim = new System.Data.DataView();
            this.dvwCatRMSec = new System.Data.DataView();
            this.dvwCatRMTerc = new System.Data.DataView();
            this.dvwIVPrim = new System.Data.DataView();
            this.dvwIVSec = new System.Data.DataView();
            this.dvwIVTerc = new System.Data.DataView();
            this.dvwDrogaPrim = new System.Data.DataView();
            this.dvwDrogaSec = new System.Data.DataView();
            this.dvwDrogaTerc = new System.Data.DataView();
            this.dvwViaPrim = new System.Data.DataView();
            this.dvwViaTerc = new System.Data.DataView();
            this.dvwViaSec = new System.Data.DataView();
            this.dvwFrecPrim = new System.Data.DataView();
            this.dvwFrecSec = new System.Data.DataView();
            this.dvwFrecTerc = new System.Data.DataView();
            this.dvwMediPrim = new System.Data.DataView();
            this.dvwMediTerc = new System.Data.DataView();
            this.dvwMediSec = new System.Data.DataView();
            this.dvw_DSMV_ProblemasPsicosocialesAmbientales1 = new System.Data.DataView();
            this.dvw_DSMV_ProblemasPsicosocialesAmbientales2 = new System.Data.DataView();
            this.dvw_DSMV_ProblemasPsicosocialesAmbientales3 = new System.Data.DataView();
            this.dvwNivelMentalAnterior = new System.Data.DataView();
            this.dvwNivelSustanciasAnterior = new System.Data.DataView();
            this.dvwFuenteReferido = new System.Data.DataView();
            this.dvwFreqAutoAyuda = new System.Data.DataView();
            ((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwEpisPreviosSustancias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwEpisPreviosMental)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwUltSustancias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwUltMental)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwNivelSustancias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwNivelMental)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwDiagPrimario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwDiagSecundario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwDiagTerciario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwCatTransPrim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwCatTransSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwCatTransTerc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwCatRMPrim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwCatRMSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwCatRMTerc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwIVPrim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwIVSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwIVTerc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwDrogaPrim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwDrogaSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwDrogaTerc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwViaPrim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwViaTerc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwViaSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFrecPrim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFrecSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFrecTerc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwMediPrim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwMediTerc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwMediSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwNivelMentalAnterior)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwNivelSustanciasAnterior)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFuenteReferido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFreqAutoAyuda)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)(this.dvw_DSMV_ProblemasPsicosocialesAmbientales1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvw_DSMV_ProblemasPsicosocialesAmbientales2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvw_DSMV_ProblemasPsicosocialesAmbientales3)).BeginInit();
            // 
            // dsPerfil
            // 
            this.dsPerfil.DataSetName = "dsPerfil";
            this.dsPerfil.Locale = new System.Globalization.CultureInfo("en-US");
            // 
            // dvwEpisPreviosSustancias
            // 
            this.dvwEpisPreviosSustancias.Table = this.dsPerfil.SA_LKP_TEDS_EPISODIO_PREVIO;
            // 
            // dvwEpisPreviosMental
            // 
            this.dvwEpisPreviosMental.Table = this.dsPerfil.SA_LKP_TEDS_EPISODIO_PREVIO;
            // 
            // dvwUltSustancias
            // 
            this.dvwUltSustancias.Table = this.dsPerfil.SA_LKP_TIEMPO_ULT_TRAT;
            // 
            // dvwUltMental
            // 
            this.dvwUltMental.Table = this.dsPerfil.SA_LKP_TIEMPO_ULT_TRAT;
            // 
            // dvwNivelSustancias
            // 
            this.dvwNivelSustancias.Table = this.dsPerfil.SA_LKP_ABUSO_SUSTANCIAS;
            // 
            // dvwNivelMental
            // 
            this.dvwNivelMental.Table = this.dsPerfil.SA_LKP_SALUD_MENTAL;
            // 
            // dvwDiagPrimario
            // 
            this.dvwDiagPrimario.Table = this.dsPerfil.SA_LKP_DIAGNOSTICO;
            // 
            // dvwDiagSecundario
            // 
            this.dvwDiagSecundario.Table = this.dsPerfil.SA_LKP_DIAGNOSTICO;
            // 
            // dvwDiagTerciario
            // 
            this.dvwDiagTerciario.Table = this.dsPerfil.SA_LKP_DIAGNOSTICO;
            // 
            // dvwCatTransPrim
            // 
            this.dvwCatTransPrim.Table = this.dsPerfil.SA_LKP_DSMIV_CAT;
            // 
            // dvwCatTransSec
            // 
            this.dvwCatTransSec.Table = this.dsPerfil.SA_LKP_DSMIV_CAT;
            // 
            // dvwCatTransTerc
            // 
            this.dvwCatTransTerc.Table = this.dsPerfil.SA_LKP_DSMIV_CAT;
            // 
            // dvwCatRMPrim
            // 
            this.dvwCatRMPrim.Table = this.dsPerfil.SA_LKP_DSMIV_CAT;
            // 
            // dvwCatRMSec
            // 
            this.dvwCatRMSec.Table = this.dsPerfil.SA_LKP_DSMIV_CAT;
            // 
            // dvwCatRMTerc
            // 
            this.dvwCatRMTerc.Table = this.dsPerfil.SA_LKP_DSMIV_CAT;
            // 
            // dvwIVPrim
            // 
            this.dvwIVPrim.Table = this.dsPerfil.SA_LKP_DSMIV_IV;
            // 
            // dvwIVSec
            // 
            this.dvwIVSec.Table = this.dsPerfil.SA_LKP_DSMIV_IV;
            // 
            // dvwIVTerc
            // 
            this.dvwIVTerc.Table = this.dsPerfil.SA_LKP_DSMIV_IV;
            // 
            // dvwDrogaPrim
            // 
            this.dvwDrogaPrim.Table = this.dsPerfil.SA_LKP_TEDS_SUSTANCIA;
            // 
            // dvwDrogaSec
            // 
            this.dvwDrogaSec.Table = this.dsPerfil.SA_LKP_TEDS_SUSTANCIA;
            // 
            // dvwDrogaTerc
            // 
            this.dvwDrogaTerc.Table = this.dsPerfil.SA_LKP_TEDS_SUSTANCIA;
            // 
            // dvwViaPrim
            // 
            this.dvwViaPrim.Table = this.dsPerfil.SA_LKP_TEDS_VIA_UTILIZACION;
            // 
            // dvwViaTerc
            // 
            this.dvwViaTerc.Table = this.dsPerfil.SA_LKP_TEDS_VIA_UTILIZACION;
            // 
            // dvwViaSec
            // 
            this.dvwViaSec.Table = this.dsPerfil.SA_LKP_TEDS_VIA_UTILIZACION;
            // 
            // dvwFrecPrim
            // 
            this.dvwFrecPrim.Table = this.dsPerfil.SA_LKP_TEDS_FRECUENCIA;
            // 
            // dvwFrecSec
            // 
            this.dvwFrecSec.Table = this.dsPerfil.SA_LKP_TEDS_FRECUENCIA;
            // 
            // dvwFrecTerc
            // 
            this.dvwFrecTerc.Table = this.dsPerfil.SA_LKP_TEDS_FRECUENCIA;
            // 
            // dvwMediPrim
            // 
            this.dvwMediPrim.Table = this.dsPerfil.SA_LKP_MEDIDA;
            // 
            // dvwMediTerc
            // 
            this.dvwMediTerc.Table = this.dsPerfil.SA_LKP_MEDIDA;
            // 
            // dvwMediSec
            // 
            this.dvwMediSec.Table = this.dsPerfil.SA_LKP_MEDIDA;
            // 
            // dvwNivelMentalAnterior
            // 
            this.dvwNivelMentalAnterior.Table = this.dsPerfil.SA_LKP_SALUD_MENTAL;
            // 
            // dvwNivelSustanciasAnterior
            // 
            this.dvwNivelSustanciasAnterior.Table = this.dsPerfil.SA_LKP_ABUSO_SUSTANCIAS;
            //
            // dvwFreqAutoAyuda
            //
            this.dvwFreqAutoAyuda.Table = this.dsPerfil.SA_LKP_TEDS_FRECUENCIA_AUTOAYUDA;
            //
            this.dvw_DSMV_ProblemasPsicosocialesAmbientales1.Table = this.dsPerfil.SA_LKP_DSMV_ProblemasPsicosocialesAmbientales;
            this.dvw_DSMV_ProblemasPsicosocialesAmbientales2.Table = this.dsPerfil.SA_LKP_DSMV_ProblemasPsicosocialesAmbientales;
            this.dvw_DSMV_ProblemasPsicosocialesAmbientales3.Table = this.dsPerfil.SA_LKP_DSMV_ProblemasPsicosocialesAmbientales;
            ((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwEpisPreviosSustancias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwEpisPreviosMental)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwUltSustancias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwUltMental)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwNivelSustancias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwNivelMental)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwDiagPrimario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwDiagSecundario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwDiagTerciario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwCatTransPrim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwCatTransSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwCatTransTerc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwCatRMPrim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwCatRMSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwCatRMTerc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwIVPrim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwIVSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwIVTerc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwDrogaPrim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwDrogaSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwDrogaTerc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwViaPrim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwViaTerc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwViaSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFrecPrim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFrecSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFrecTerc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwMediPrim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwMediTerc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwMediSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwNivelMentalAnterior)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwNivelSustanciasAnterior)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFuenteReferido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFreqAutoAyuda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvw_DSMV_ProblemasPsicosocialesAmbientales1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvw_DSMV_ProblemasPsicosocialesAmbientales2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvw_DSMV_ProblemasPsicosocialesAmbientales3)).EndInit();

        }
        #endregion

        #region properties
        public int PK_Perfil
        {
            set
            {
                this.m_pk_perfil = value;
            }
        }

        public int PK_Episodio
        {
            set
            {
                this.m_pk_episodio = value;
            }
        }

        #region Propiedades del Episodio

        public byte FK_EtapaServicio
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.ddlEtapaServicio.SelectedValue.ToString());
                }
                catch
                {
                    return 1;//DEFAULT ADMISION
                }
            }
        }

        public byte IN_Metadona
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.ddlMetadona.SelectedValue.ToString());
                }
                catch
                {
                    return 4;//DEFAULT NO APLICA
                }
            }
        }

        public byte IN_CodDependiente
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.ddlCodependiente.SelectedValue.ToString());
                }
                catch
                {
                    return 2;//DEFAULT NO
                }
            }
        }

        public byte FK_FuenteReferido
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.ddlFuenteReferido.SelectedValue.ToString());
                }
                catch
                {
                    return 95;//DEFAULT No hay información
                }
            }
        }

        public byte FK_EstadoLegal
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.ddlEstadoLegal.SelectedValue.ToString());
                }
                catch
                {
                    return 97;//Default No información
                }
            }
        }

        public byte IN_ArrestadoAnteriormente
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.ddlArrestado.SelectedValue.ToString());
                }
                catch
                {
                    return 2;//default no
                }
            }
        }

        public List<int> FK_Justicia
        {
            get
            {
                List<int> Lst_justicia = new List<int>();
                foreach (ListItem i in lbxProbJusticiaSeleccionado.Items)
                {
                    Lst_justicia.Add(int.Parse(i.Value));
                }
                return Lst_justicia;
            }
        }

        public byte NR_DiasEsperaSustancias
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.txtDíasSustancias.Text.Trim());
                }
                catch
                {
                    return 0; // Default 0 días
                }
            }
        }

        public byte FK_EpisodiosSustancias
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.ddlPreviosSustancias.SelectedValue.ToString());
                }
                catch
                {
                    return 97; //Default no informacion
                }
            }
        }

        public byte FK_DuracionSustancias
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.ddlUltSustancias.SelectedValue.ToString());
                }
                catch
                {
                    return 95;//Default no información
                }
            }
        }

        public byte NR_DiasUltimaAltaSustancias
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.txtDíasSustUlt.Text.Trim());
                }
                catch
                {
                    return 0;//Default 0 días
                }
            }
        }

        public byte NR_MesesUltimaAltaSustancias
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.txtMesesSustUlt.Text.Trim());
                }
                catch
                {
                    return 0;//Default 0 meses
                }
            }
        }

        public byte FK_NivelCuidadoSustanciasAnterior
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.ddlNivelSustancias.SelectedValue.ToString());
                }
                catch
                {
                    return 99; //Default No aplica
                }
            }
        }

        public byte NR_DiasEsperaMental
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.txtDíasMental.Text.Trim());
                }
                catch
                {
                    return 0; //Default 0 días
                }
            }
        }

        public byte FK_EpisodiosMental
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.ddlPreviosMental.SelectedValue.ToString());
                }
                catch
                {
                    return 97; // No información
                }
            }
        }

        public byte FK_DuracionMental
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.ddlUltMental.SelectedValue.ToString());
                }
                catch
                {
                    return 95;//Default no información
                }
            }
        }

        public byte NR_DiasUltimaAltaMental
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.txtDíasMentUlt.Text.Trim());
                }
                catch
                {
                    return 0; //Default 0 días
                }
            }
        }

        public byte NR_MesesUltimaAltaMental
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.txtMesesMentUlt.Text.Trim());
                }
                catch
                {
                    return 0; //Default 0 meses
                }
            }
        }

        public byte FK_NivelCuidadoMentalAnterior
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.ddlNivelMental.SelectedValue.ToString());
                }
                catch
                {
                    return 99; //Default No aplica
                }
            }
        }

        public byte IN_ViolenciaDomestica
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.ddlVioDomestic.SelectedValue.ToString());
                }
                catch
                {
                    return 96;//Default no informo
                }
            }
        }

        public byte IN_Maltrato
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.ddlMaltratoNinez.SelectedValue.ToString());
                }
                catch
                {
                    return 96;//Default no informo
                }
            }
        }

        public List<int> IN_TI_Maltrato
        {
            get
            {
                List<int> Lst_maltratos = new List<int>();
                foreach (ListItem i in lbxMaltratoSeleccionado.Items)
                {
                    Lst_maltratos.Add(int.Parse(i.Value));
                }
                return Lst_maltratos;
            }
        }

        public byte IN_Suicida
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.ddlSuicidios.SelectedValue.ToString());
                }
                catch
                {
                    return 96;//Default no informo
                }
            }
        }

        public byte IN_IdeaSuicida
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.ddlIdeaSuicida.SelectedValue.ToString());
                }
                catch
                {
                    return 96;//Default no informo
                }
            }
        }

        public DropDownList NivelCuidadoSaludMental
        {
            get
            {
                try
                {
                    return this.ddlNivelCuidadoSaludMental;
                }
                catch
                {
                    return null;
                }
            }
        }

        public DropDownList NivelCuidadoSustancias
        {
            get
            {
                try
                {
                    return this.ddlNivelCuidadoSustancias;
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region Propiedades del Perfil

        public sbyte @IN_ParticReunGrupos
        {
            get
            {
                try
                {
                    return 94;
                    //return Convert.ToSByte(this.ddlReunionesGrupos.SelectedValue.ToString());
                }
                catch
                {
                    return 94; //Default no recuerda
                }
            }
        }

        public sbyte @FK_FreqAutoAyuda
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlFreq_AutoAyuda.SelectedValue.ToString());
                }
                catch
                {
                    return 99; //Default No aplica
                }
            }
        }

        public sbyte @NR_Arrestos30dias
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.txtArrestos30.Text.Trim());
                }
                catch
                {
                    return 0; //Default 0 días
                }
            }
        }

        public sbyte @IN_Arrestado30dias
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlArrestado30.SelectedValue.ToString());
                }
                catch
                {
                    return 2; //Default No
                }
            }
        }

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
                //Int32.TryParse(this.hDSMVRMSec.Value.ToString(), out retVal);
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
                // Int32.TryParse(this.ddlDSMVPsicoAmbiPrim.SelectedValue.ToString(), out retVal);
                return retVal;
            }
        }
        public int @FK_DSMV_ProblemasPsicosocialesAmbientales2
        {
            get
            {
                int retVal = 0;
                // Int32.TryParse(this.ddlDSMVPsicoAmbiSec.SelectedValue.ToString(), out retVal);
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
                    return Convert.ToSByte(this.ddlNivelCuidadoSaludMental.SelectedValue.ToString());
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
                    return Convert.ToSByte(this.ddlNivelCuidadoSustancias.SelectedValue.ToString());
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
                    return Convert.ToSByte(this.txtEdadPrim.SelectedValue);
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
        #endregion

        protected void ddlReunionesGrupos_Load(object sender, EventArgs e)
        {
            //if (this.ddlReunionesGrupos.SelectedValue == "2" || this.ddlReunionesGrupos.SelectedValue == "94")
            //{
            //    this.ddlFreq_AutoAyuda.SelectedValue = "1";
            //    this.ddlFreq_AutoAyuda.Enabled = false;
            //}           
            //else
            //{
            //    this.ddlFreq_AutoAyuda.Enabled = true;
            //}
        }
        public void load()
        {
            lbxMaltrato();
            lbxProbJusticia();
            ddlNivelMentalAdded(_pkPrograma);
            ddlNivelSustanciaAdded(_pkPrograma);
        }

        private void ddlNivelUnavilable(NivelCuidado nivelCuidado)
        {
            bool? es_episodio = null;
            int pk_episodio = 0;
            try
            {
                es_episodio = dsPerfil.SA_EPISODIO[0].ES_Episodio;
                pk_episodio = dsPerfil.SA_EPISODIO[0].PK_Episodio;
            }
            catch { }
            if (this.m_frmAction == frmAction.Create || es_episodio == null || es_episodio == false)
            {
                NewSource NS = new NewSource();
                DataTable Dt = new DataTable();
                if (nivelCuidado == NivelCuidado.Mental)
                {
                    Dt = NS.getNivelUnavailable("[SPR_OPEN_NIVELCUIDADO_MENTAL_FORPATIENT]", Convert.ToInt32(this.dsPerfil.SA_PERSONA[0].PK_Persona.ToString()), pk_episodio);
                    if (Dt.Rows.Count > 0 && Dt != null)
                    {
                        for (int i = 0; Dt.Rows.Count > i; i++)
                        {
                            if (Dt.Rows[i][0].ToString() != "99" && ddlNivelCuidadoSaludMental.SelectedValue != Dt.Rows[i][0].ToString())
                            {

                                ddlNivelCuidadoSaludMental.Items.Remove(ddlNivelCuidadoSaludMental.Items.FindByValue(Dt.Rows[i][0].ToString()));
                            }
                        }
                    }
                }
                else if (nivelCuidado == NivelCuidado.Sustancias)
                {
                    Dt = NS.getNivelUnavailable("[SPR_OPEN_NIVELCUIDADO_SUSTANCIAS_FORPATIENT]", Convert.ToInt32(this.dsPerfil.SA_PERSONA[0].PK_Persona.ToString()), pk_episodio);
                    if (Dt.Rows.Count > 0 && Dt != null)
                    {
                        for (int i = 0; Dt.Rows.Count > i; i++)
                        {
                            if (Dt.Rows[i][0].ToString() != "99" && ddlNivelCuidadoSustancias.SelectedValue != Dt.Rows[i][0].ToString())
                            {
                                ddlNivelCuidadoSustancias.Items.Remove(ddlNivelCuidadoSustancias.Items.FindByValue(Dt.Rows[i][0].ToString()));
                            }
                        }
                    }
                }
                Dt = null;
                NS = null;
            }
        }

        private int countNivelMental(int pk)
        {
            NewSource NS = new NewSource();
            DataTable Dt = new DataTable();
            Dt = NS.getNivel("SP_DROP_NivelCuidadoMental", pk);
            if (Dt == null) { return 0; }
            else { return Dt.Rows.Count; }
        }

        private int countNivelSustancia(int pk)
        {
            NewSource NS = new NewSource();
            DataTable Dt = new DataTable();
            Dt = NS.getNivel("SP_DROP_NivelCuidadoSustancia", pk);

            if (Dt == null) { return 0; }
            else { return Dt.Rows.Count; }

        }

        private void ddlNivelMentalAdded(int pk)
        {
            NewSource NS = new NewSource();
            DataTable Dt = new DataTable();
            Dt = NS.getNivel("SP_DROP_NivelCuidadoMental", pk);
            if (Dt.Rows.Count > 0 && Dt != null)
            {
                this.ddlNivelCuidadoSaludMental.DataSource = Dt;
                this.ddlNivelCuidadoSaludMental.DataValueField = "PK_SaludMental";
                this.ddlNivelCuidadoSaludMental.DataTextField = "DE_SaludMental";
                this.ddlNivelCuidadoSaludMental.DataBind();
            }
            else
            {
                this.ddlNivelCuidadoSaludMental.Enabled = false;
                this.ddlNivelCuidadoSaludMental.Items.Insert(0, new ListItem("No aplica", "99"));
                this.ddlNivelCuidadoSaludMental.SelectedValue = "99";
            }
            if (Dt.Rows.Count == 1 && Dt != null && countNivelSustancia(pk) == 0)
            {
                this.ddlNivelCuidadoSaludMental.SelectedIndex = 1;
            }
            else if (Dt.Rows.Count > 0 && Dt != null && countNivelSustancia(pk) != 0)
            {
                ListItem li = new ListItem("No aplica", "99");
                this.ddlNivelCuidadoSaludMental.Items.Insert(0, li);
                this.ddlNivelCuidadoSaludMental.SelectedIndex = 0;
            }

            Dt = null;
            NS = null;
        }

        private void ddlNivelSustanciaAdded(int pk)
        {
            NewSource NS = new NewSource();
            DataTable Dt = new DataTable();
            Dt = NS.getNivel("SP_DROP_NivelCuidadoSustancia", pk);
            if (Dt.Rows.Count > 0 && Dt != null)
            {
                this.ddlNivelCuidadoSustancias.DataSource = Dt;
                this.ddlNivelCuidadoSustancias.DataValueField = "PK_AbusoSustancias";
                this.ddlNivelCuidadoSustancias.DataTextField = "DE_AbusoSustancias";
                this.ddlNivelCuidadoSustancias.DataBind();
            }
            else
            {
                this.ddlNivelCuidadoSustancias.Enabled = false;
                this.ddlNivelCuidadoSustancias.Items.Insert(0, new ListItem("No aplica", "99"));
                this.ddlNivelCuidadoSustancias.SelectedValue = "99";
            }
            if (Dt.Rows.Count == 1 && Dt != null && countNivelMental(pk) == 0)
            {
                this.ddlNivelCuidadoSustancias.SelectedIndex = 1;
            }
            else if (Dt.Rows.Count > 0 && Dt != null && countNivelMental(pk) != 0)
            {
                ListItem li = new ListItem("No aplica", "99");
                this.ddlNivelCuidadoSustancias.Items.Insert(0, li);
                this.ddlNivelCuidadoSustancias.SelectedIndex = 0;
            }

            Dt = null;
            NS = null;
        }

        private void lbxProbJusticia()
        {
            if (m_frmAction == frmAction.Read)
            {
                string selectedValuesString = "";
                NewSource NS = new NewSource();
                DataTable Dref = new DataTable();
                Dref = NS.getRef("SPR_Ref_ProbJusticia", m_pk_episodio);
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
                divProbJusticia.Visible = false;
                lblProbJusticia.Text = selectedValuesString;
                NS = null;
            }
            else
            {
                NewSource NS = new NewSource();
                DataTable Dt = new DataTable();
                Dt = NS.getAll("SPR_DROP_ProbJusticia");
                this.lbxProbJusticiaSeleccion.DataSource = Dt;


                this.lbxProbJusticiaSeleccion.DataValueField = "PK_ProbJusticia";
                this.lbxProbJusticiaSeleccion.DataTextField = "DE_ProbJusticia";
                this.lbxProbJusticiaSeleccion.DataBind();
                DataTable Dref = new DataTable();
                Dref = NS.getRef("SPR_Ref_ProbJusticia", m_pk_episodio);
                if (Dref.Rows.Count > 0)
                {
                    foreach (DataRow r in Dref.Rows)
                    {
                        System.Web.UI.WebControls.ListItem li = new ListItem(r[1].ToString(), r[0].ToString());
                        this.lbxProbJusticiaSeleccionado.Items.Add(li);
                        this.lbxProbJusticiaSeleccion.Items.Remove(li);
                    }
                    Dref = null;
                }
                divLblProbJusticia.Visible = false;
                Dt = null;
                NS = null;
            }
        }

        private void lbxMaltrato()
        {
            if (m_frmAction == frmAction.Read)
            {
                string selectedValuesString = "";
                NewSource NS = new NewSource();
                DataTable Dref = new DataTable();
                Dref = NS.getRef("SPR_Ref_Maltrato", m_pk_episodio);
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
                divMaltrato.Visible = false;
                lblMaltrato.Text = selectedValuesString;
                NS = null;
            }
            else
            {
                NewSource NS = new NewSource();
                DataTable Dt = new DataTable();
                Dt = NS.getAll("SPR_DROP_Maltrato");
                this.lbxMaltratoSeleccion.DataSource = Dt;
                this.lbxMaltratoSeleccion.DataValueField = "PK_Maltrato";
                this.lbxMaltratoSeleccion.DataTextField = "DE_Maltrato";
                this.lbxMaltratoSeleccion.DataBind();
                DataTable Dref = new DataTable();
                Dref = NS.getRef("SPR_Ref_Maltrato", m_pk_episodio);
                if (Dref.Rows.Count > 0)
                {
                    this.lbxMaltratoSeleccionado.DataSource = null;
                    foreach (DataRow r in Dref.Rows)
                    {
                        System.Web.UI.WebControls.ListItem li = new ListItem(r[1].ToString(), r[0].ToString());
                        this.lbxMaltratoSeleccionado.Items.Add(li);
                        this.lbxMaltratoSeleccion.Items.Remove(li);
                    }
                }
                divLblMaltrato.Visible = false;
                Dt = null;
                NS = null;
            }
        }

        public int ProbJusticiaItem(int i)
        {
            return Convert.ToInt32(lbxProbJusticiaSeleccionado.Items[i].Value);
        }

        public int ProbJusticiaCount
        {
            get
            {
                _probJusticiaCount = lbxProbJusticiaSeleccionado.Items.Count;
                return _probJusticiaCount;
            }
        }

        public int MaltratoItem(int i)
        {
            return Convert.ToInt32(lbxMaltratoSeleccionado.Items[i].Value);
        }

        public int MaltratoCount
        {
            get
            {
                _maltratoCount = lbxMaltratoSeleccionado.Items.Count;
                return _maltratoCount;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {


            if (this.lbxProbJusticiaSeleccion.SelectedItem != null)
            {
                if (lbxProbJusticiaSeleccion.SelectedItem.Value == "99" &&
                    (ddlArrestado.SelectedValue == "1" && ddlArrestado30.SelectedValue == "2" || ddlArrestado.SelectedValue == "2" && ddlArrestado30.SelectedValue == "1"))
                    return;

                System.Web.UI.WebControls.ListItem li = new ListItem(this.lbxProbJusticiaSeleccion.SelectedItem.Text, this.lbxProbJusticiaSeleccion.SelectedItem.Value);
                this.lbxProbJusticiaSeleccionado.Items.Add(li);
                this.lbxProbJusticiaSeleccion.Items.Remove(li);
                SortListBox(this.lbxProbJusticiaSeleccionado);
            }

        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.lbxProbJusticiaSeleccionado.SelectedItem != null)
            {
                System.Web.UI.WebControls.ListItem li = new ListItem(this.lbxProbJusticiaSeleccionado.SelectedItem.Text, this.lbxProbJusticiaSeleccionado.SelectedItem.Value);
                this.lbxProbJusticiaSeleccion.Items.Add(li);
                this.lbxProbJusticiaSeleccionado.Items.Remove(li);
                SortListBox(this.lbxProbJusticiaSeleccion);
            }
        }

        protected void lbxMaltrato_ClickA(object sender, EventArgs e)
        {
            if (this.lbxMaltratoSeleccion.SelectedItem != null)
            {
                System.Web.UI.WebControls.ListItem li = new ListItem(this.lbxMaltratoSeleccion.SelectedItem.Text, this.lbxMaltratoSeleccion.SelectedItem.Value);
                /*Verificar si existe el NA en la otra lista para no insertar nada más*/
                if (li.Value == "99")
                {
                    /*Verificar si hay algo ya seleccionado*/
                    if (this.lbxMaltratoSeleccionado.Items.Count == 0)
                    {
                        this.Button1.Enabled = false;
                    }
                    else
                    {
                        return;
                    }
                }
                this.lbxMaltratoSeleccionado.Items.Add(li);
                this.lbxMaltratoSeleccion.Items.Remove(li);
                SortListBox(this.lbxMaltratoSeleccionado);
            }
        }
        protected void lbxMaltrato_ClickD(object sender, EventArgs e)
        {
            if (this.lbxMaltratoSeleccionado.SelectedItem != null)
            {
                System.Web.UI.WebControls.ListItem li = new ListItem(this.lbxMaltratoSeleccionado.SelectedItem.Text, this.lbxMaltratoSeleccionado.SelectedItem.Value);
                this.lbxMaltratoSeleccion.Items.Add(li);
                this.lbxMaltratoSeleccionado.Items.Remove(li);
                SortListBox(this.lbxMaltratoSeleccion);
                /*Verificar si removieron No Aplica para enable el boton*/
                if (this.lbxMaltratoSeleccionado.Items.Count == 0)
                {
                    this.Button1.Enabled = true;
                }
            }
        }

        private void SortListBox(ListBox listBox)//Added for listbox sorting
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

        protected void ddlMaltratoNinez_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlMaltratoNinez.SelectedValue == "2") //NO
            {
                if (this.lbxMaltratoSeleccionado.Items.Count > 0)
                {
                    this.lbxMaltratoSeleccionado.Items.Clear();
                    this.lbxMaltratoSeleccion.Items.Clear();

                    NewSource NS = new NewSource();
                    DataTable Dt = new DataTable();
                    Dt = NS.getAll("SPR_DROP_Maltrato");
                    this.lbxMaltratoSeleccion.DataSource = Dt;
                    this.lbxMaltratoSeleccion.DataValueField = "PK_Maltrato";
                    this.lbxMaltratoSeleccion.DataTextField = "DE_Maltrato";
                    this.lbxMaltratoSeleccion.DataBind();
                    DataTable Dref = new DataTable();
                    Dt = null;
                    NS = null;

                }

                ListItem item = this.lbxMaltratoSeleccionado.Items.FindByValue("99");
                if (item == null)
                {
                    ListItem li = new ListItem("No aplica", "99");
                    this.lbxMaltratoSeleccionado.Items.Add(li);
                    this.lbxMaltratoSeleccion.Items.Remove(li);
                }
                this.Button1.Enabled = false;
                this.Button2.Enabled = false;
            }
            else
            {
                ListItem item = this.lbxMaltratoSeleccionado.Items.FindByValue("99");
                if (item != null)
                {
                    this.lbxMaltratoSeleccionado.Items.Remove(item);
                }
                this.Button1.Enabled = true;
                this.Button2.Enabled = true;
            }
        }
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
                    //Response.Write("<script>alert(' Entro a Update');</script>");
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
        protected void ddlArrestado_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem li = new ListItem("No aplica", "99");

            if (ddlArrestado.SelectedValue == "2")
            {
                this.lbxProbJusticiaSeleccionado.Items.Remove(li);
                this.lbxProbJusticiaSeleccion.Items.Remove(li);
                this.lbxProbJusticiaSeleccionado.Items.Add(li);
                SortListBox(this.lbxProbJusticiaSeleccionado);

                lbxProbJusticiaSeleccion.Enabled = false;
                lbxProbJusticiaSeleccionado.Enabled = false;
                Button4.Enabled = false;
                Button3.Enabled = false;

            }
            else if (ddlArrestado.SelectedValue == "1")
            {

                this.lbxProbJusticiaSeleccionado.Items.Remove(li);
                this.lbxProbJusticiaSeleccion.Items.Remove(li);
                this.lbxProbJusticiaSeleccion.Items.Add(li);
                SortListBox(this.lbxProbJusticiaSeleccionado);

                lbxProbJusticiaSeleccion.Enabled = true;
                lbxProbJusticiaSeleccionado.Enabled = true;
                Button4.Enabled = true;
                Button3.Enabled = true;

            }
        }
    }


  
}

public class DropDownAgeAbusoDeSustancia
{

    public int Value { get; set; }
    public string Text { get; set; }

}

  
#region using
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
#endregion
namespace ASSMCA.Perfiles
{
    public partial class frmAdmision : System.Web.UI.Page
    {
        #region variables
        protected ASSMCA.perfiles.dsPerfil dsPerfil;
        protected ASSMCA.pacientes.dsPersona dsPersona;
        protected ASSMCA.dsSeguridad dsSeguridad;
        protected System.Data.SqlClient.SqlConnection cnn;
        protected System.Data.SqlClient.SqlCommand sqlSelectCommand1, SPU_EPISODIO, SPC_EPISODIO, sqlSelectCommand2, sqlSelectCommand4, SPC_PERFIL, sqlSelectCommand3, SPU_PERFIL, SPC_NewData, SPU_NewData, SPD_Ref_Maltrato, SPU_Ref_Maltrato,
            SPD_Ref_CondicionesDiagnosticadas, SPU_Ref_CondicionesDiagnosticadas,SPD_Ref_ProbJusticia, SPU_Ref_ProbJusticia, SPD_Ref_CompFamilia, SPU_Ref_CompFamilia;
        protected System.Data.SqlClient.SqlDataAdapter daLkpEpidosio, daLkpPerfil, daAdmision, daLkpNivelCuidado;
        private int m_PK_Programa, PK_Perfil, m_PK_Persona, m_CO_Tipo;
        private const string SCRIPT_DOFOCUS =
    @"window.setTimeout('DoFocus()', 1);
    function DoFocus()
    {
        try {
            document.getElementById('REQUEST_LASTFOCUS').focus();
        } catch (ex) {}
    }";
        #endregion

        protected void ActualizarResidencia( )
        {
            System.Data.DataView dvwResidencia = new System.Data.DataView();
            dvwResidencia.Table = this.dsPerfil.SA_LKP_TEDS_RESIDENCIA;

            List<string> SM = new List<string>() { "0", "1", "2", "3", "4", "7", "8", "11", "13", "14" };

            string value = WucDatosDemograficos.ddlResidencia.SelectedValue;
            /*PK_Residencia DE_Residencia
                ------------ - --------------------------------------------------
                        1             Propia(de los padres, si es menor)
                        2             Alquilada(por los padres, si es menor)
                        3             Familiares
                        4             Amigos
                        5             Institución residencial
                        6             Hogar grupal, orfanato
                        7             Institución correccional
                        8             Vivienda pública
                        9             Hogar de crianza
                        10            Hogar sustituto
                        11            Sin hogar(Deambulante)
                        13            Hogar transicional
                        97            No informó
                 *
                 */

            // salud mental 
            if (WucEpisodioAdmision.NivelCuidadoSaludMental.SelectedValue != "99" && WucEpisodioAdmision.NivelCuidadoSaludMental.SelectedValue != "")
            {
                    dvwResidencia.RowFilter = "PK_Residencia IN (0,1,2,3,4,7,8,11,13,14)";
                    WucDatosDemograficos.dvwResidencia = dvwResidencia;
                    WucDatosDemograficos.ddlResidencia.DataBind();

                if (SM.Contains(value))
                {
                    WucDatosDemograficos.ddlResidencia.SelectedValue = value;
                }
                

                lblTipoPerfil.Text = "Salud Mental.";
            }

            // Abuso de sustancia
            if (WucEpisodioAdmision.NivelCuidadoSustancias.SelectedValue != "99" && WucEpisodioAdmision.NivelCuidadoSustancias.SelectedValue != "")
            {
                value = WucDatosDemograficos.ddlResidencia.SelectedValue;
                WucDatosDemograficos.dvwResidencia = dvwResidencia;
                WucDatosDemograficos.ddlResidencia.DataBind();
                
                WucDatosDemograficos.ddlResidencia.SelectedValue = value;
                
                lblTipoPerfil.Text = "Abuso de Sustancias.";
            }

            //WucDatosDemograficos.ddlResidencia.DataValueField = "PK_Residencia";
            //WucDatosDemograficos.ddlResidencia.DataTextField="DE_Residencia";
            //WucDatosDemograficos.ddlResidencia.DataSource = dvwResidencia;
            //WucDatosDemograficos.ddlResidencia.DataBind();

            //WucDatosDemograficos.dvwResidencia = dvwResidencia;
            //WucDatosDemograficos.ddlResidencia.DataBind();

        }

        protected void Page_Load(object sender, System.EventArgs e)
        {

            
            this.Session["Tipo_Perfil"] = "Admision";
            if (this.Session["dsSeguridad"] == null)
            {
                this.Response.Redirect("~/Error.aspx?errMsg=sesion");
                return;
            }
            if (this.Session["dsPerfil"] != null)
            {
                this.dsPerfil = (ASSMCA.perfiles.dsPerfil)this.Session["dsPerfil"];
            }
            if (this.Session["dsPersona"] != null)
            {
                this.dsPersona = (ASSMCA.pacientes.dsPersona)this.Session["dsPersona"];
            }
            this.m_PK_Programa = Convert.ToInt32(this.Session["pk_programa"].ToString());
            this.m_CO_Tipo = Convert.ToInt32(this.Session["co_tipo"].ToString());
            string Accion = this.Request.QueryString["accion"].ToString();
            WucEpisodioAdmision.accion = Accion;

            if (this.m_CO_Tipo == 1 || this.m_CO_Tipo == 4)
            {
                this.lblTipoPerfil.Text = " : Abuso de Sustancia";
            }
            else
            {
                this.lblTipoPerfil.Text = " : Salud Mental";
            }
            if (!this.IsPostBack)
            {
                HookOnFocus(this.Page as Control);
                DataRow drSA_LKP_ABUSO_SUSTANCIAS = this.dsPerfil.SA_LKP_ABUSO_SUSTANCIAS.NewRow();
                DataRow drSA_LKP_SALUD_MENTAL = this.dsPerfil.SA_LKP_SALUD_MENTAL.NewRow();
                switch (Accion)
                {
                    case ("create"): 
                        this.CrearRegistro();
                        this.dsPerfil.SA_LKP_ABUSO_SUSTANCIAS.Rows.Clear();
                        this.dsPerfil.SA_LKP_SALUD_MENTAL.Rows.Clear();
                        drSA_LKP_ABUSO_SUSTANCIAS["PK_AbusoSustancias"] = 0;
                        drSA_LKP_ABUSO_SUSTANCIAS["DE_AbusoSustancias"] = "";
                        this.dsPerfil.SA_LKP_ABUSO_SUSTANCIAS.Rows.Add(drSA_LKP_ABUSO_SUSTANCIAS);
                        drSA_LKP_SALUD_MENTAL["PK_SaludMental"] = 0;
                        drSA_LKP_SALUD_MENTAL["DE_SaludMental"] = "";
                        this.dsPerfil.SA_LKP_SALUD_MENTAL.Rows.Add(drSA_LKP_SALUD_MENTAL);
                        this.daLkpNivelCuidado.SelectCommand.Parameters["@PK_Persona"].Value = this.m_PK_Persona;
                        this.daLkpNivelCuidado.Fill(this.dsPerfil);
                        this.btnEliminar.Visible = false;
                        this.btnEliminarAdmin.Visible = false;
                        this.btnModificar.Visible = false;
                        this.btnModificarAdmin.Visible = false;
                        this.btnRegistrar.Visible = true;
                        this.btnGuardarCambios.Visible = false;
                        break;
                    case ("update"): 
                        this.WucEpisodioAdmision.PK_Episodio = Convert.ToInt32(this.dsPerfil.SA_EPISODIO[0]["PK_Episodio"].ToString());
                        this.WucEpisodioAdmision.PK_Perfil = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString());
                        this.WucDatosPersonales.PK_Perfil = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString());
                        this.WucDatosDemograficos.PK_Perfil = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString());
                        this.ModificarRegistro();
                        this.dsPerfil.SA_LKP_ABUSO_SUSTANCIAS.Rows.Clear();
                        this.dsPerfil.SA_LKP_SALUD_MENTAL.Rows.Clear();
                        drSA_LKP_ABUSO_SUSTANCIAS["PK_AbusoSustancias"] = 0;
                        drSA_LKP_ABUSO_SUSTANCIAS["DE_AbusoSustancias"] = "";
                        this.dsPerfil.SA_LKP_ABUSO_SUSTANCIAS.Rows.Add(drSA_LKP_ABUSO_SUSTANCIAS);
                        drSA_LKP_SALUD_MENTAL["PK_SaludMental"] = 0;
                        drSA_LKP_SALUD_MENTAL["DE_SaludMental"] = "";
                        this.dsPerfil.SA_LKP_SALUD_MENTAL.Rows.Add(drSA_LKP_SALUD_MENTAL);
                        this.daLkpNivelCuidado.SelectCommand.Parameters["@PK_Persona"].Value = Convert.ToInt32(this.dsPerfil.SA_EPISODIO[0]["FK_Persona"].ToString());
                        this.daLkpNivelCuidado.SelectCommand.Parameters["@PK_Episodio"].Value = Convert.ToInt32(this.dsPerfil.SA_EPISODIO[0]["PK_Episodio"].ToString());
                        this.daLkpNivelCuidado.Fill(this.dsPerfil);
                        this.btnEliminar.Visible = false;
                        this.btnEliminarAdmin.Visible = false;
                        this.btnModificar.Visible = false;
                        this.btnModificarAdmin.Visible = false;
                        this.btnRegistrar.Visible = false;
                        this.btnGuardarCambios.Visible = true;
                        TipoPerfil();
                        break;
                    case ("read"):
                        this.LeerRegistro();
                        this.btnRegistrar.Visible = false;
                        this.btnGuardarCambios.Visible = false;
                        TipoPerfil();
                        break;
                    default: break;
                }
            }
            else
            { 
                              
                if (Request.Form["__EVENTTARGET"] != null &&
                    Request.Form["__EVENTTARGET"] != string.Empty &&
                    Request.Form["__EVENTTARGET"] != "ctl00$changeProgram")

                {                   
                        this.WucDatosDemograficos.edadAdmisionF(this.WucDatosPersonales.FE_Episodio, this.WucDatosPersonales.FE_Nacimiento.ToString());
                    
                }
              
                Page.ClientScript.RegisterStartupScript(
                     typeof(frmAdmision),
                     "ScriptDoFocus",
                     SCRIPT_DOFOCUS.Replace("REQUEST_LASTFOCUS",
                                            Request["__LASTFOCUS"]),
                     true);

            } 

            this.dsSeguridad = (dsSeguridad)this.Session["dsSeguridad"];
            int nr_rowIndex_dsSeguridad = Convert.ToInt32(this.Session["nr_rowIndex_dsSeguridad"].ToString());
            if (this.dsSeguridad.SA_USUARIO[nr_rowIndex_dsSeguridad].IN_D_PCORTA < 1)
            {
                this.btnEliminar.Visible = false;
            }
            if (this.dsSeguridad.SA_USUARIO[nr_rowIndex_dsSeguridad].IN_D_PERFIL < 1) 
            {
                this.btnEliminarAdmin.Visible = false;
            }
            if (this.dsSeguridad.SA_USUARIO[nr_rowIndex_dsSeguridad].IN_U_PCORTA < 1)
            {
                this.btnModificar.Visible = false;
            }
            if (this.dsSeguridad.SA_USUARIO[nr_rowIndex_dsSeguridad].IN_U_PERFIL < 1)
            {
                this.btnModificarAdmin.Visible = false;
            }
            if (this.dsPerfil.SA_PERFIL.Rows.Count == 1)
            {
                if (this.dsPerfil.SA_PERFIL.DefaultView[0]["ES_Perfil"].ToString() == "1")
                {
                    this.btnModificar.Visible = false;
                    this.btnEliminar.Visible = false;
                }
                DateTime FE_Edicion = Convert.ToDateTime(this.dsPerfil.SA_PERFIL.DefaultView[0]["FE_Perfil"].ToString());
                TimeSpan ts = DateTime.Now.Subtract(FE_Edicion);
                
                if (ts.Days >NewSource.nr_dias_edicion_registros)
                {
                    this.btnModificar.Visible = false;
                    this.btnEliminar.Visible = false;
                }
                else
                {
                    this.btnModificarAdmin.Visible = false;
                    this.btnEliminarAdmin.Visible = false;
                }
            }


            ActualizarResidencia();
        }
        #region Metodos
        private void HookOnFocus(Control CurrentControl)
        {
            //checks if control is one of TextBox, DropDownList, ListBox or Button
            if ((CurrentControl is TextBox) ||
                (CurrentControl is DropDownList) ||
                (CurrentControl is ListBox) ||
                (CurrentControl is Button))
                //adds a script which saves active control on receiving focus 
                //in the hidden field __LASTFOCUS.
                (CurrentControl as WebControl).Attributes.Add(
                   "onfocus",
                   "try{document.getElementById('__LASTFOCUS').value=this.id} catch (e) { }");
            //checks if the control has children
            if (CurrentControl.HasControls())
                //if yes do them all recursively
                foreach (Control CurrentChildControl in CurrentControl.Controls)
                    HookOnFocus(CurrentChildControl);
        }

        private void LeerRegistro()
        {
            WucDatosPersonales.m_frmAction = frmAction.Read;
            WucOtrosDatos.m_frmAction = frmAction.Read;
            WucDatosDemograficos.m_frmAction = frmAction.Read;
            WucEpisodioAdmision.m_frmAction = frmAction.Read;
            WucDatosAdmision.m_frmAction = frmAction.Read;
            int PK_Episodio = Convert.ToInt32(Request.QueryString["pk_episodio"].ToString());
            this.dsPerfil.SA_EPISODIO.Rows.Clear();
            this.dsPerfil.SA_PERFIL.Rows.Clear();
            dsPerfil.Tables["SA_Episodio"].Columns["IN_TratamientoResidencial"].AllowDBNull = true;
            dsPerfil.Tables["SA_Episodio"].Columns["IN_TI_Hospital"].AllowDBNull = true;
            dsPerfil.Tables["SA_Episodio"].Columns["IN_HospitalizadoMental"].AllowDBNull = true;
            dsPerfil.Tables["SA_Episodio"].Columns["IN_Ambulatorio"].AllowDBNull = true;
            dsPerfil.Tables["SA_Episodio"].Columns["IN_TratadoMental"].AllowDBNull = true;
            this.daAdmision.SelectCommand.Parameters["@PK_Admision"].Value = PK_Episodio;
            this.daAdmision.Fill(this.dsPerfil);
            WucDatosPersonales.PK_Persona =Convert.ToInt32( this.dsPersona.SA_PERSONA[0]["PK_Persona"].ToString());
            WucDatosPersonales.PK_Programa = this.m_PK_Programa;
            PK_Perfil = Convert.ToInt32(this.dsPerfil.SA_PERFIL[0]["PK_NR_Perfil"].ToString());
            WucDatosPersonales.PK_Perfil = PK_Perfil;
            WucDatosPersonales.setValues();
            WucEpisodioAdmision.PK_Episodio = PK_Episodio;
            WucEpisodioAdmision.PK_Perfil = PK_Perfil;
            WucDatosDemograficos.PK_Perfil = PK_Perfil;
            this.Session["dsPerfil"] = this.dsPerfil;
            this.btnRegistrar.Visible = false;
        }

        private void ModificarRegistro()
        {
            WucDatosPersonales.m_frmAction = frmAction.Update;
            WucOtrosDatos.m_frmAction = frmAction.Update;
            WucDatosDemograficos.m_frmAction = frmAction.Update;
            WucEpisodioAdmision.m_frmAction = frmAction.Update;
            WucDatosAdmision.m_frmAction = frmAction.Update;
            WucDatosPersonales.PK_Persona = Convert.ToInt32(this.dsPerfil.SA_EPISODIO[0]["FK_Persona"].ToString());
            WucDatosPersonales.PK_Programa = this.m_PK_Programa;
            this.Session["dsPerfil"] = this.dsPerfil;
            this.btnRegistrar.Visible = false;
        }

        private void CrearRegistro()
        {
            WucDatosPersonales.m_frmAction = frmAction.Create;
            WucOtrosDatos.m_frmAction = frmAction.Create;
            WucDatosDemograficos.m_frmAction = frmAction.Create;
            WucEpisodioAdmision.m_frmAction = frmAction.Create;
            WucDatosAdmision.m_frmAction = frmAction.Create;
            this.m_PK_Persona = Convert.ToInt32(this.Request.QueryString["pk_persona"].ToString());
            WucDatosPersonales.PK_Persona = this.m_PK_Persona;
            WucDatosPersonales.PK_Programa = this.m_PK_Programa;
            this.btnRegistrar.Visible = true;
        }

        private int GuardarNuevo()
        {
            int PK_Episodio = 0;
            this.m_PK_Persona = Convert.ToInt32(this.Request.QueryString["pk_persona"].ToString());
            this.SPC_EPISODIO.Parameters["@FK_Persona"].Value = this.m_PK_Persona;
            this.SPC_EPISODIO.Parameters["@FK_Programa"].Value = this.m_PK_Programa;
            this.SPC_EPISODIO.Parameters["@FE_Episodio"].Value = this.WucDatosPersonales.FE_Episodio;
            this.SPC_EPISODIO.Parameters["@FE_FechaConvenio"].Value = this.WucDatosPersonales.FE_FechaConvenio;
            this.SPC_EPISODIO.Parameters["@FK_SeguroSalud"].Value = this.WucOtrosDatos.FK_SeguroSalud;
            this.SPC_EPISODIO.Parameters["@FK_FuentePago"].Value = this.WucOtrosDatos.FK_FuentePago;
            this.SPC_EPISODIO.Parameters["@FK_FeminaHijos"].Value = this.WucDatosDemograficos.FK_FeminaHijos;
            this.SPC_EPISODIO.Parameters["@IN_VaronHijos"].Value = this.WucDatosDemograficos.IN_VaronHijos;
            this.SPC_EPISODIO.Parameters["@FK_FuenteIngreso"].Value = this.WucDatosDemograficos.FK_FuenteIngreso;
            this.SPC_EPISODIO.Parameters["@FK_TiempoResidencia"].Value = this.WucDatosDemograficos.FK_TiempoResidencia;
            this.SPC_EPISODIO.Parameters["@FK_Municipio"].Value = this.WucDatosDemograficos.FK_Municipio;
            this.SPC_EPISODIO.Parameters["@IN_Zona"].Value = this.WucDatosDemograficos.IN_Zona;
            if (this.WucDatosDemograficos.NR_ZipCode != null)
            {
                this.SPC_EPISODIO.Parameters["@NR_ZipCode"].Value = this.WucDatosDemograficos.NR_ZipCode;
            }
            this.SPC_EPISODIO.Parameters["@FK_EtapaServicio"].Value = this.WucEpisodioAdmision.FK_EtapaServicio;
            this.SPC_EPISODIO.Parameters["@FK_NivelCuidadoSustancias"].Value = this.WucEpisodioAdmision.FK_NivelCuidadoSustancias;
            this.SPC_EPISODIO.Parameters["@FK_NivelCuidadoMental"].Value = this.WucEpisodioAdmision.FK_NivelCuidadoSaludMental;
            this.SPC_EPISODIO.Parameters["@IN_Metadona"].Value = this.WucEpisodioAdmision.IN_Metadona;
            this.SPC_EPISODIO.Parameters["@IN_CodDependiente"].Value = this.WucEpisodioAdmision.IN_CodDependiente;
            this.SPC_EPISODIO.Parameters["@FK_FuenteReferido"].Value = this.WucEpisodioAdmision.FK_FuenteReferido;
            this.SPC_EPISODIO.Parameters["@FK_EstadoLegal"].Value = this.WucEpisodioAdmision.FK_EstadoLegal;
            this.SPC_EPISODIO.Parameters["@IN_ArrestadoAnteriormente"].Value = this.WucEpisodioAdmision.IN_ArrestadoAnteriormente;
            this.SPC_EPISODIO.Parameters["@FK_Justicia"].Value = 99;
            this.SPC_EPISODIO.Parameters["@NR_DiasEsperaSustancias"].Value = this.WucEpisodioAdmision.NR_DiasEsperaSustancias;
            this.SPC_EPISODIO.Parameters["@FK_EpisodiosSustancias"].Value = this.WucEpisodioAdmision.FK_EpisodiosSustancias;
            this.SPC_EPISODIO.Parameters["@FK_DuracionSustancias"].Value = this.WucEpisodioAdmision.FK_DuracionSustancias;
            this.SPC_EPISODIO.Parameters["@NR_DiasUltimaAltaSustancias"].Value = this.WucEpisodioAdmision.NR_DiasUltimaAltaSustancias;
            this.SPC_EPISODIO.Parameters["@NR_MesesUltimaAltaSustancias"].Value = this.WucEpisodioAdmision.NR_MesesUltimaAltaSustancias;
            this.SPC_EPISODIO.Parameters["@FK_NivelCuidadoSustanciasAnterior"].Value = this.WucEpisodioAdmision.FK_NivelCuidadoSustanciasAnterior;
            this.SPC_EPISODIO.Parameters["@NR_DiasEsperaMental"].Value = this.WucEpisodioAdmision.NR_DiasEsperaMental;
            this.SPC_EPISODIO.Parameters["@FK_EpisodiosMental"].Value = this.WucEpisodioAdmision.FK_EpisodiosMental;
            this.SPC_EPISODIO.Parameters["@FK_DuracionMental"].Value = this.WucEpisodioAdmision.FK_DuracionMental;
            this.SPC_EPISODIO.Parameters["@NR_DiasUltimaAltaMental"].Value = this.WucEpisodioAdmision.NR_DiasUltimaAltaMental;
            this.SPC_EPISODIO.Parameters["@NR_MesesUltimaAltaMental"].Value = this.WucEpisodioAdmision.NR_MesesUltimaAltaMental;
            this.SPC_EPISODIO.Parameters["@FK_NivelCuidadoMentalAnterior"].Value = this.WucEpisodioAdmision.FK_NivelCuidadoMentalAnterior;
            this.SPC_EPISODIO.Parameters["@IN_ViolenciaDomestica"].Value = this.WucEpisodioAdmision.IN_ViolenciaDomestica;
            this.SPC_EPISODIO.Parameters["@IN_Maltrato"].Value = this.WucEpisodioAdmision.IN_Maltrato;
            this.SPC_EPISODIO.Parameters["@IN_TI_Maltrato"].Value = 99;
            this.SPC_EPISODIO.Parameters["@IN_Suicida"].Value = this.WucEpisodioAdmision.IN_Suicida;
            this.SPC_EPISODIO.Parameters["@IN_IdeaSuicida"].Value = this.WucEpisodioAdmision.IN_IdeaSuicida;
            if (this.m_PK_Programa == 61 || this.m_PK_Programa == 62 || this.m_PK_Programa == 63 || this.m_PK_Programa == 64 || this.m_PK_Programa == 65)
            {
                this.SPC_EPISODIO.Parameters["@ES_Episodio"].Value = true;
                this.SPC_EPISODIO.Parameters["@FE_Alta"].Value = this.WucDatosPersonales.FE_Episodio;
            }
            this.SPC_PERFIL.Parameters["@FE_Perfil"].Value = this.WucDatosPersonales.FE_Episodio;
            this.SPC_PERFIL.Parameters["@FK_TipoAdmision"].Value = this.WucDatosPersonales.FK_TipoAdmision;
            if (this.WucDatosPersonales.NR_Expediente != "0")
            {
                this.SPC_PERFIL.Parameters["@NR_Expediente"].Value = this.WucDatosPersonales.NR_Expediente;
            }
            this.SPC_PERFIL.Parameters["@FE_Contacto"].Value = System.DBNull.Value;
            this.SPC_PERFIL.Parameters["@IN_TI_Perfil"].Value = "AD";
            this.SPC_PERFIL.Parameters["@FK_EstadoMarital"].Value = this.WucDatosDemograficos.FK_EstadoMarital;
            this.SPC_PERFIL.Parameters["@FK_CondicionLaboral"].Value = this.WucDatosDemograficos.FK_CondicionLaboral;
            this.SPC_PERFIL.Parameters["@FK_ActividadNoLaboral"].Value = this.WucDatosDemograficos.FK_ActividadNoLaboral;
            this.SPC_PERFIL.Parameters["@NR_Hijos"].Value = this.WucDatosDemograficos.NR_Hijos;
            this.SPC_PERFIL.Parameters["@FK_SituacionEscolar"].Value = this.WucDatosDemograficos.FK_SituacionEscolar;
            this.SPC_PERFIL.Parameters["@FK_Escolaridad"].Value = this.WucDatosDemograficos.FK_Escolaridad;

            this.SPC_PERFIL.Parameters["@IN_EducEspecial"].Value = this.WucDatosDemograficos.IN_EducEspecial;
            this.SPC_PERFIL.Parameters["@IN_DesertorEscolar"].Value = this.WucDatosDemograficos.IN_DesertorEscolar;
            this.SPC_PERFIL.Parameters["@FK_Familia"].Value = 96;
            this.SPC_PERFIL.Parameters["@NR_Familiar"].Value = this.WucDatosDemograficos.NR_Familiar;
            this.SPC_PERFIL.Parameters["@FK_Residencia"].Value = this.WucDatosDemograficos.FK_Residencia;
            this.SPC_PERFIL.Parameters["@IN_ParticReunGrupos"].Value = this.WucEpisodioAdmision.IN_ParticReunGrupos;
            this.SPC_PERFIL.Parameters["@FK_FreqAutoAyuda"].Value = this.WucEpisodioAdmision.FK_FreqAutoAyuda;
            this.SPC_PERFIL.Parameters["@NR_Arrestos30dias"].Value = this.WucEpisodioAdmision.NR_Arrestos30dias;
            this.SPC_PERFIL.Parameters["@IN_Arrestado30dias"].Value = this.WucEpisodioAdmision.IN_Arrestado30dias;
            this.SPC_PERFIL.Parameters["@FK_CategoriaCentroPrivado"].Value = System.DBNull.Value;
            #region DSMV
            this.SPC_PERFIL.Parameters["@FK_DSMV_TrastornosClinicos1"].Value = this.WucEpisodioAdmision.FK_DSMV_TrastornosClinicos1;
            this.SPC_PERFIL.Parameters["@FK_DSMV_TrastornosClinicos2"].Value = this.WucEpisodioAdmision.FK_DSMV_TrastornosClinicos2;
            this.SPC_PERFIL.Parameters["@FK_DSMV_TrastornosClinicos3"].Value = this.WucEpisodioAdmision.FK_DSMV_TrastornosClinicos3;
            this.SPC_PERFIL.Parameters["@FK_DSMV_TrastornosPersonalidadRM1"].Value = this.WucEpisodioAdmision.FK_DSMV_TrastornosPersonalidadRM1;
            this.SPC_PERFIL.Parameters["@FK_DSMV_TrastornosPersonalidadRM2"].Value = this.WucEpisodioAdmision.FK_DSMV_TrastornosPersonalidadRM2;
            this.SPC_PERFIL.Parameters["@FK_DSMV_TrastornosPersonalidadRM3"].Value = this.WucEpisodioAdmision.FK_DSMV_TrastornosPersonalidadRM3;
            this.SPC_PERFIL.Parameters["@FK_DSMV_ProblemasPsicosocialesAmbientales1"].Value = this.WucEpisodioAdmision.FK_DSMV_ProblemasPsicosocialesAmbientales1;
            this.SPC_PERFIL.Parameters["@FK_DSMV_ProblemasPsicosocialesAmbientales2"].Value = this.WucEpisodioAdmision.FK_DSMV_ProblemasPsicosocialesAmbientales2;
            this.SPC_PERFIL.Parameters["@FK_DSMV_ProblemasPsicosocialesAmbientales3"].Value = this.WucEpisodioAdmision.FK_DSMV_ProblemasPsicosocialesAmbientales3;
            this.SPC_PERFIL.Parameters["@NR_DSMV_FuncionamientoGlobal"].Value = this.WucEpisodioAdmision.NR_DSMV_FuncionamientoGlobal;
            this.SPC_PERFIL.Parameters["@DE_DSMV_OtrasObservaciones"].Value = this.WucEpisodioAdmision.DE_DSMV_Comentarios;
            this.SPC_PERFIL.Parameters["@DE_DSMV_Comentarios"].Value = this.WucEpisodioAdmision.DE_DSMV_OtrasObservaciones;
            this.SPC_PERFIL.Parameters["@IN_DSMV_DiagnosticoDual"].Value = this.WucEpisodioAdmision.IN_DSMV_DiagnosticoDual;
            #endregion
            this.SPC_PERFIL.Parameters["@FK_DrogaPrimario"].Value = this.WucEpisodioAdmision.FK_DrogaPrimario;
            this.SPC_PERFIL.Parameters["@FK_ViaPrimario"].Value = this.WucEpisodioAdmision.FK_ViaPrimario;
            this.SPC_PERFIL.Parameters["@FK_FrecuenciaPrimario"].Value = this.WucEpisodioAdmision.FK_FrecuenciaPrimario;
            this.SPC_PERFIL.Parameters["@IN_EdadInicioPrimario"].Value = this.WucEpisodioAdmision.IN_EdadInicioPrimario;
            this.SPC_PERFIL.Parameters["@FK_DrogaSecundario"].Value = this.WucEpisodioAdmision.FK_DrogaSecundario;
            this.SPC_PERFIL.Parameters["@FK_ViaSecundario"].Value = this.WucEpisodioAdmision.FK_ViaSecundario;
            this.SPC_PERFIL.Parameters["@FK_FrecuenciaSecundario"].Value = this.WucEpisodioAdmision.FK_FrecuenciaSecundario;
            this.SPC_PERFIL.Parameters["@IN_EdadInicioSecundario"].Value = this.WucEpisodioAdmision.IN_EdadInicioSecundario;
            this.SPC_PERFIL.Parameters["@FK_DrogaTerciario"].Value = this.WucEpisodioAdmision.FK_DrogaTerciario;
            this.SPC_PERFIL.Parameters["@FK_ViaTerciario"].Value = this.WucEpisodioAdmision.FK_ViaTerciario;
            this.SPC_PERFIL.Parameters["@FK_FrecuenciaTerciario"].Value = this.WucEpisodioAdmision.FK_FrecuenciaTerciario;
            this.SPC_PERFIL.Parameters["@IN_EdadInicioTerciario"].Value = this.WucEpisodioAdmision.IN_EdadInicioTerciario;
            this.SPC_PERFIL.Parameters["@DE_Comentario"].Value = this.WucDatosAdmision.DE_Comentario;
            Guid PK_Sesion = new Guid(this.Session["pk_sesion"].ToString());
            this.SPC_PERFIL.Parameters["@FK_Sesion"].Value = PK_Sesion;
            this.SPC_NewData = new System.Data.SqlClient.SqlCommand();
            this.SPC_NewData.CommandText = "[SPC_NewData]";
            this.SPC_NewData.CommandType = System.Data.CommandType.StoredProcedure;
            this.SPC_NewData.Connection = this.cnn;
            this.SPC_NewData.Parameters.AddWithValue("@FK_Militar", this.WucDatosPersonales.FK_Militar);
            this.SPC_NewData.Parameters.AddWithValue("@FK_FamMilitar", this.WucDatosPersonales.IN_FamiliaMilitar);
            this.SPC_NewData.Parameters.AddWithValue("@FK_Genero", this.WucDatosPersonales.FK_Genero);
            try
            {
                this.cnn.Open();
                this.SPC_EPISODIO.ExecuteNonQuery();
                PK_Episodio = (int)this.SPC_EPISODIO.Parameters["@PK_Episodio"].Value;
                this.SPC_PERFIL.Parameters["@FK_Episodio"].Value = PK_Episodio;
                this.SPC_PERFIL.ExecuteNonQuery();
                int PK_Perfil = Convert.ToInt32(this.SPC_PERFIL.Parameters["@PK_Perfil"].Value);
                this.WucDatosPersonales.PK_Perfil = PK_Perfil;
                this.SPC_NewData.Parameters.AddWithValue("@FK_Perfil", PK_Perfil);
                this.SPC_NewData.ExecuteNonQuery();
                this.SPD_Ref_CondicionesDiagnosticadas = new System.Data.SqlClient.SqlCommand();
                this.SPD_Ref_CondicionesDiagnosticadas.CommandText = "[SPD_Ref_CondicionesDiagnosticadas]";
                this.SPD_Ref_CondicionesDiagnosticadas.CommandType = System.Data.CommandType.StoredProcedure;
                this.SPD_Ref_CondicionesDiagnosticadas.Connection = this.cnn;
                this.SPD_Ref_CondicionesDiagnosticadas.Parameters.AddWithValue("@FK_Perfil", PK_Perfil);
                this.SPD_Ref_CondicionesDiagnosticadas.ExecuteNonQuery();
                for (int i = 0; i < this.WucEpisodioAdmision.CondicionesDiagnosticadasCount; i++)
                {
                    this.SPU_Ref_CondicionesDiagnosticadas = new System.Data.SqlClient.SqlCommand();
                    this.SPU_Ref_CondicionesDiagnosticadas.CommandText = "[SPU_Ref_CondicionesDiagnosticadas]";
                    this.SPU_Ref_CondicionesDiagnosticadas.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPU_Ref_CondicionesDiagnosticadas.Connection = this.cnn;
                    this.SPU_Ref_CondicionesDiagnosticadas.Parameters.AddWithValue("@FK_Perfil", PK_Perfil);
                    this.SPU_Ref_CondicionesDiagnosticadas.Parameters.AddWithValue("@FK_Diagnostico", this.WucEpisodioAdmision.CondicionesDiagnosticadasItem(i));
                    this.SPU_Ref_CondicionesDiagnosticadas.ExecuteNonQuery();
                    this.SPU_Ref_CondicionesDiagnosticadas.Dispose();
                }

                    this.SPD_Ref_Maltrato = new System.Data.SqlClient.SqlCommand();
                    this.SPD_Ref_Maltrato.CommandText = "[SPD_Ref_Maltrato]";
                    this.SPD_Ref_Maltrato.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPD_Ref_Maltrato.Connection = this.cnn;
                    this.SPD_Ref_Maltrato.Parameters.AddWithValue("@FK_Episodio", PK_Episodio);
                    this.SPD_Ref_Maltrato.ExecuteNonQuery();
                    for (int i = 0; i < this.WucEpisodioAdmision.MaltratoCount; i++)
                    {
                        this.SPU_Ref_Maltrato = new System.Data.SqlClient.SqlCommand();
                        this.SPU_Ref_Maltrato.CommandText = "[SPU_Ref_Maltrato]";
                        this.SPU_Ref_Maltrato.CommandType = System.Data.CommandType.StoredProcedure;
                        this.SPU_Ref_Maltrato.Connection = this.cnn;
                        this.SPU_Ref_Maltrato.Parameters.AddWithValue("@FK_Episodio", PK_Episodio);
                        this.SPU_Ref_Maltrato.Parameters.AddWithValue("@FK_Maltrato", this.WucEpisodioAdmision.MaltratoItem(i));
                        this.SPU_Ref_Maltrato.ExecuteNonQuery();
                        this.SPU_Ref_Maltrato.Dispose();
                    }
                    this.SPD_Ref_ProbJusticia = new System.Data.SqlClient.SqlCommand();
                    this.SPD_Ref_ProbJusticia.CommandText = "[SPD_Ref_ProbJusticia]";
                    this.SPD_Ref_ProbJusticia.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPD_Ref_ProbJusticia.Connection = this.cnn;
                    this.SPD_Ref_ProbJusticia.Parameters.AddWithValue("@FK_Episodio", PK_Episodio);
                    this.SPD_Ref_ProbJusticia.ExecuteNonQuery();
                    for (int i = 0; i < this.WucEpisodioAdmision.ProbJusticiaCount; i++)
                    {
                        this.SPU_Ref_ProbJusticia = new System.Data.SqlClient.SqlCommand();
                        this.SPU_Ref_ProbJusticia.CommandText = "[SPU_Ref_ProbJusticia]";
                        this.SPU_Ref_ProbJusticia.CommandType = System.Data.CommandType.StoredProcedure;
                        this.SPU_Ref_ProbJusticia.Connection = this.cnn;
                        this.SPU_Ref_ProbJusticia.Parameters.AddWithValue("@FK_Episodio", PK_Episodio);
                        this.SPU_Ref_ProbJusticia.Parameters.AddWithValue("@FK_ProbJusticia", this.WucEpisodioAdmision.ProbJusticiaItem(i));
                        this.SPU_Ref_ProbJusticia.ExecuteNonQuery();
                        this.SPU_Ref_ProbJusticia.Dispose();
                    }
                
                    this.SPD_Ref_CompFamilia = new System.Data.SqlClient.SqlCommand();
                    this.SPD_Ref_CompFamilia.CommandText = "[SPD_Ref_CompFamilia]";
                    this.SPD_Ref_CompFamilia.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPD_Ref_CompFamilia.Connection = this.cnn;
                    this.SPD_Ref_CompFamilia.Parameters.AddWithValue("@FK_Perfil", PK_Perfil);
                    this.SPD_Ref_CompFamilia.ExecuteNonQuery();
                    for (int i = 0; i < this.WucDatosDemograficos.CompFamCount; i++)
                    {
                        this.SPU_Ref_CompFamilia = new System.Data.SqlClient.SqlCommand();
                        this.SPU_Ref_CompFamilia.CommandText = "[SPU_Ref_CompFamilia]";
                        this.SPU_Ref_CompFamilia.CommandType = System.Data.CommandType.StoredProcedure;
                        this.SPU_Ref_CompFamilia.Connection = this.cnn;
                        this.SPU_Ref_CompFamilia.Parameters.AddWithValue("@FK_Perfil", PK_Perfil);
                        this.SPU_Ref_CompFamilia.Parameters.AddWithValue("@FK_CompFamilia", this.WucDatosDemograficos.CompFamItem(i));
                        this.SPU_Ref_CompFamilia.ExecuteNonQuery();
                        this.SPU_Ref_CompFamilia.Dispose();
                    }
                this.cnn.Close();
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                Trace.Warn("GuardarNuevo()", m, ex);
                if (this.cnn.State != ConnectionState.Closed)
                {
                    this.cnn.Close();
                }
                throw ex;
            }
            return PK_Episodio;
        }

        private void GuardarCambios()
        {






            this.SPU_EPISODIO.Parameters["@PK_Episodio"].Value = Convert.ToInt32(this.dsPerfil.SA_EPISODIO.DefaultView[0]["PK_Episodio"].ToString());
            this.SPU_EPISODIO.Parameters["@FE_Episodio"].Value = this.WucDatosPersonales.FE_Episodio;
            this.SPU_EPISODIO.Parameters["@FE_FechaConvenio"].Value = this.WucDatosPersonales.FE_FechaConvenio;
            this.SPU_EPISODIO.Parameters["@FK_SeguroSalud"].Value = this.WucOtrosDatos.FK_SeguroSalud;
            this.SPU_EPISODIO.Parameters["@FK_FuentePago"].Value = this.WucOtrosDatos.FK_FuentePago;
            this.SPU_EPISODIO.Parameters["@FK_FeminaHijos"].Value = this.WucDatosDemograficos.FK_FeminaHijos;
            this.SPU_EPISODIO.Parameters["@IN_VaronHijos"].Value = this.WucDatosDemograficos.IN_VaronHijos;
            this.SPU_EPISODIO.Parameters["@FK_FuenteIngreso"].Value = this.WucDatosDemograficos.FK_FuenteIngreso;
            this.SPU_EPISODIO.Parameters["@FK_TiempoResidencia"].Value = this.WucDatosDemograficos.FK_TiempoResidencia;
            this.SPU_EPISODIO.Parameters["@FK_Municipio"].Value = this.WucDatosDemograficos.FK_Municipio;
            this.SPU_EPISODIO.Parameters["@IN_Zona"].Value = this.WucDatosDemograficos.IN_Zona;
            if (this.WucDatosDemograficos.NR_ZipCode != null)
            {
                this.SPU_EPISODIO.Parameters["@NR_ZipCode"].Value = this.WucDatosDemograficos.NR_ZipCode;
            }
            this.SPU_EPISODIO.Parameters["@FK_EtapaServicio"].Value = this.WucEpisodioAdmision.FK_EtapaServicio;
            this.SPU_EPISODIO.Parameters["@FK_NivelCuidadoSustancias"].Value = this.WucEpisodioAdmision.FK_NivelCuidadoSustancias;
            this.SPU_EPISODIO.Parameters["@FK_NivelCuidadoMental"].Value = this.WucEpisodioAdmision.FK_NivelCuidadoSaludMental;
            this.SPU_EPISODIO.Parameters["@IN_Metadona"].Value = this.WucEpisodioAdmision.IN_Metadona;
            this.SPU_EPISODIO.Parameters["@IN_CodDependiente"].Value = this.WucEpisodioAdmision.IN_CodDependiente;
            this.SPU_EPISODIO.Parameters["@FK_FuenteReferido"].Value = this.WucEpisodioAdmision.FK_FuenteReferido;
            this.SPU_EPISODIO.Parameters["@FK_EstadoLegal"].Value = this.WucEpisodioAdmision.FK_EstadoLegal;
            this.SPU_EPISODIO.Parameters["@IN_ArrestadoAnteriormente"].Value = this.WucEpisodioAdmision.IN_ArrestadoAnteriormente;
            this.SPU_EPISODIO.Parameters["@FK_Justicia"].Value = 99;
            this.SPU_EPISODIO.Parameters["@NR_DiasEsperaSustancias"].Value = this.WucEpisodioAdmision.NR_DiasEsperaSustancias;
            this.SPU_EPISODIO.Parameters["@FK_EpisodiosSustancias"].Value = this.WucEpisodioAdmision.FK_EpisodiosSustancias;
            this.SPU_EPISODIO.Parameters["@FK_DuracionSustancias"].Value = this.WucEpisodioAdmision.FK_DuracionSustancias;
            this.SPU_EPISODIO.Parameters["@NR_DiasUltimaAltaSustancias"].Value = this.WucEpisodioAdmision.NR_DiasUltimaAltaSustancias;
            this.SPU_EPISODIO.Parameters["@NR_MesesUltimaAltaSustancias"].Value = this.WucEpisodioAdmision.NR_MesesUltimaAltaSustancias;
            this.SPU_EPISODIO.Parameters["@FK_NivelCuidadoSustanciasAnterior"].Value = this.WucEpisodioAdmision.FK_NivelCuidadoSustanciasAnterior;
            this.SPU_EPISODIO.Parameters["@NR_DiasEsperaMental"].Value = this.WucEpisodioAdmision.NR_DiasEsperaMental;
            this.SPU_EPISODIO.Parameters["@FK_EpisodiosMental"].Value = this.WucEpisodioAdmision.FK_EpisodiosMental;
            this.SPU_EPISODIO.Parameters["@FK_DuracionMental"].Value = this.WucEpisodioAdmision.FK_DuracionMental;
            this.SPU_EPISODIO.Parameters["@NR_DiasUltimaAltaMental"].Value = this.WucEpisodioAdmision.NR_DiasUltimaAltaMental;
            this.SPU_EPISODIO.Parameters["@NR_MesesUltimaAltaMental"].Value = this.WucEpisodioAdmision.NR_MesesUltimaAltaMental;
            this.SPU_EPISODIO.Parameters["@FK_NivelCuidadoMentalAnterior"].Value = this.WucEpisodioAdmision.FK_NivelCuidadoMentalAnterior;
            this.SPU_EPISODIO.Parameters["@IN_ViolenciaDomestica"].Value = this.WucEpisodioAdmision.IN_ViolenciaDomestica;
            this.SPU_EPISODIO.Parameters["@IN_Maltrato"].Value = this.WucEpisodioAdmision.IN_Maltrato;
            this.SPU_EPISODIO.Parameters["@IN_TI_Maltrato"].Value = 99;
            this.SPU_EPISODIO.Parameters["@IN_Suicida"].Value = this.WucEpisodioAdmision.IN_Suicida;
            this.SPU_EPISODIO.Parameters["@IN_IdeaSuicida"].Value = this.WucEpisodioAdmision.IN_IdeaSuicida;
            this.SPU_EPISODIO.Parameters["@IN_TratadoMental"].Value = System.DBNull.Value;
            this.SPU_PERFIL.Parameters["@PK_NR_Perfil"].Value = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString());
            this.SPU_PERFIL.Parameters["@FE_Perfil"].Value = this.WucDatosPersonales.FE_Episodio;
            //if (this.WucDatosPersonales.NR_Expediente != "0")
            //{
            //    this.SPU_PERFIL.Parameters["@NR_Expediente"].Value = this.WucDatosPersonales.NR_Expediente;
            //}
            this.SPU_PERFIL.Parameters["@IN_TI_Perfil"].Value = "AD";
            this.SPU_PERFIL.Parameters["@FE_Contacto"].Value = System.DBNull.Value;

            this.SPU_PERFIL.Parameters["@FK_CategoriaCentroPrivado"].Value = System.DBNull.Value;
            this.SPU_PERFIL.Parameters["@FK_TipoAdmision"].Value = this.WucDatosPersonales.FK_TipoAdmision;
            this.SPU_PERFIL.Parameters["@FK_EstadoMarital"].Value = this.WucDatosDemograficos.FK_EstadoMarital;
            this.SPU_PERFIL.Parameters["@FK_CondicionLaboral"].Value = this.WucDatosDemograficos.FK_CondicionLaboral;
            this.SPU_PERFIL.Parameters["@FK_ActividadNoLaboral"].Value = this.WucDatosDemograficos.FK_ActividadNoLaboral;
            this.SPU_PERFIL.Parameters["@NR_Hijos"].Value = this.WucDatosDemograficos.NR_Hijos;
            this.SPU_PERFIL.Parameters["@FK_Escolaridad"].Value = this.WucDatosDemograficos.FK_Escolaridad;
            this.SPU_PERFIL.Parameters["@FK_SituacionEscolar"].Value = this.WucDatosDemograficos.FK_SituacionEscolar;
            this.SPU_PERFIL.Parameters["@IN_EducEspecial"].Value = this.WucDatosDemograficos.IN_EducEspecial;
            this.SPU_PERFIL.Parameters["@IN_DesertorEscolar"].Value = this.WucDatosDemograficos.IN_DesertorEscolar;
            this.SPU_PERFIL.Parameters["@FK_Familia"].Value = 96;
            this.SPU_PERFIL.Parameters["@NR_Familiar"].Value = this.WucDatosDemograficos.NR_Familiar;
            this.SPU_PERFIL.Parameters["@FK_Residencia"].Value = this.WucDatosDemograficos.FK_Residencia;
            this.SPU_PERFIL.Parameters["@IN_ParticReunGrupos"].Value = this.WucEpisodioAdmision.IN_ParticReunGrupos;
            this.SPU_PERFIL.Parameters["@FK_FreqAutoAyuda"].Value = this.WucEpisodioAdmision.FK_FreqAutoAyuda;
            this.SPU_PERFIL.Parameters["@NR_Arrestos30dias"].Value = this.WucEpisodioAdmision.NR_Arrestos30dias;
            this.SPU_PERFIL.Parameters["@IN_Arrestado30dias"].Value = this.WucEpisodioAdmision.IN_Arrestado30dias;
            #region DSMV
            this.SPU_PERFIL.Parameters["@FK_DSMV_TrastornosClinicos1"].Value = this.WucEpisodioAdmision.FK_DSMV_TrastornosClinicos1;
            this.SPU_PERFIL.Parameters["@FK_DSMV_TrastornosClinicos2"].Value = this.WucEpisodioAdmision.FK_DSMV_TrastornosClinicos2;
            this.SPU_PERFIL.Parameters["@FK_DSMV_TrastornosClinicos3"].Value = this.WucEpisodioAdmision.FK_DSMV_TrastornosClinicos3;
            this.SPU_PERFIL.Parameters["@FK_DSMV_TrastornosPersonalidadRM1"].Value = this.WucEpisodioAdmision.FK_DSMV_TrastornosPersonalidadRM1;
            this.SPU_PERFIL.Parameters["@FK_DSMV_TrastornosPersonalidadRM2"].Value = this.WucEpisodioAdmision.FK_DSMV_TrastornosPersonalidadRM2;
            this.SPU_PERFIL.Parameters["@FK_DSMV_TrastornosPersonalidadRM3"].Value = this.WucEpisodioAdmision.FK_DSMV_TrastornosPersonalidadRM3;
            this.SPU_PERFIL.Parameters["@FK_DSMV_ProblemasPsicosocialesAmbientales1"].Value = this.WucEpisodioAdmision.FK_DSMV_ProblemasPsicosocialesAmbientales1;
            this.SPU_PERFIL.Parameters["@FK_DSMV_ProblemasPsicosocialesAmbientales2"].Value = this.WucEpisodioAdmision.FK_DSMV_ProblemasPsicosocialesAmbientales2;
            this.SPU_PERFIL.Parameters["@FK_DSMV_ProblemasPsicosocialesAmbientales3"].Value = this.WucEpisodioAdmision.FK_DSMV_ProblemasPsicosocialesAmbientales3;
            this.SPU_PERFIL.Parameters["@NR_DSMV_FuncionamientoGlobal"].Value = this.WucEpisodioAdmision.NR_DSMV_FuncionamientoGlobal;
            this.SPU_PERFIL.Parameters["@DE_DSMV_OtrasObservaciones"].Value = this.WucEpisodioAdmision.DE_DSMV_Comentarios;
            this.SPU_PERFIL.Parameters["@DE_DSMV_Comentarios"].Value = this.WucEpisodioAdmision.DE_DSMV_OtrasObservaciones;
            this.SPU_PERFIL.Parameters["@IN_DSMV_DiagnosticoDual"].Value = this.WucEpisodioAdmision.IN_DSMV_DiagnosticoDual;
            #endregion
            this.SPU_PERFIL.Parameters["@FK_DrogaPrimario"].Value = this.WucEpisodioAdmision.FK_DrogaPrimario;
            this.SPU_PERFIL.Parameters["@FK_ViaPrimario"].Value = this.WucEpisodioAdmision.FK_ViaPrimario;
            this.SPU_PERFIL.Parameters["@FK_FrecuenciaPrimario"].Value = this.WucEpisodioAdmision.FK_FrecuenciaPrimario;
            this.SPU_PERFIL.Parameters["@IN_EdadInicioPrimario"].Value = this.WucEpisodioAdmision.IN_EdadInicioPrimario;
            this.SPU_PERFIL.Parameters["@FK_DrogaSecundario"].Value = this.WucEpisodioAdmision.FK_DrogaSecundario;
            this.SPU_PERFIL.Parameters["@FK_ViaSecundario"].Value = this.WucEpisodioAdmision.FK_ViaSecundario;
            this.SPU_PERFIL.Parameters["@FK_FrecuenciaSecundario"].Value = this.WucEpisodioAdmision.FK_FrecuenciaSecundario;
            this.SPU_PERFIL.Parameters["@IN_EdadInicioSecundario"].Value = this.WucEpisodioAdmision.IN_EdadInicioSecundario;
            this.SPU_PERFIL.Parameters["@FK_DrogaTerciario"].Value = this.WucEpisodioAdmision.FK_DrogaTerciario;
            this.SPU_PERFIL.Parameters["@FK_ViaTerciario"].Value = this.WucEpisodioAdmision.FK_ViaTerciario;
            this.SPU_PERFIL.Parameters["@FK_FrecuenciaTerciario"].Value = this.WucEpisodioAdmision.FK_FrecuenciaTerciario;
            this.SPU_PERFIL.Parameters["@IN_EdadInicioTerciario"].Value = this.WucEpisodioAdmision.IN_EdadInicioTerciario;
            this.SPU_PERFIL.Parameters["@DE_Comentario"].Value = this.WucDatosAdmision.DE_Comentario;
            Guid PK_Sesion = new Guid(this.Session["pk_sesion"].ToString());
            this.SPU_PERFIL.Parameters["@FK_Sesion"].Value = PK_Sesion;
            this.PK_Perfil = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString());
            this.SPU_NewData = new System.Data.SqlClient.SqlCommand();
            this.SPU_NewData.CommandText = "[SPU_NewData]";
            this.SPU_NewData.CommandType = System.Data.CommandType.StoredProcedure;
            this.SPU_NewData.Connection = this.cnn;
            this.SPU_NewData.Parameters.AddWithValue("@FK_Perfil", PK_Perfil);
            this.SPU_NewData.Parameters.AddWithValue("@FK_Militar", this.WucDatosPersonales.FK_Militar);
            this.SPU_NewData.Parameters.AddWithValue("@FK_FamMilitar", this.WucDatosPersonales.IN_FamiliaMilitar);
            this.SPU_NewData.Parameters.AddWithValue("@FK_Genero", this.WucDatosPersonales.FK_Genero);
            int PK_Episodio = Convert.ToInt32(this.dsPerfil.SA_EPISODIO.DefaultView[0]["PK_Episodio"].ToString());
            try
            {
                this.cnn.Open();
                this.SPU_EPISODIO.ExecuteNonQuery();
                this.SPU_PERFIL.ExecuteNonQuery();
                this.SPU_NewData.ExecuteNonQuery();
                    this.SPD_Ref_Maltrato = new System.Data.SqlClient.SqlCommand();
                    this.SPD_Ref_Maltrato.CommandText = "[SPD_Ref_Maltrato]";
                    this.SPD_Ref_Maltrato.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPD_Ref_Maltrato.Connection = this.cnn;
                    this.SPD_Ref_Maltrato.Parameters.AddWithValue("@FK_Episodio", PK_Episodio);
                    this.SPD_Ref_Maltrato.ExecuteNonQuery();
                    for (int i = 0; i < this.WucEpisodioAdmision.MaltratoCount; i++)
                    {
                        this.SPU_Ref_Maltrato = new System.Data.SqlClient.SqlCommand();
                        this.SPU_Ref_Maltrato.CommandText = "[SPU_Ref_Maltrato]";
                        this.SPU_Ref_Maltrato.CommandType = System.Data.CommandType.StoredProcedure;
                        this.SPU_Ref_Maltrato.Connection = this.cnn;
                        this.SPU_Ref_Maltrato.Parameters.AddWithValue("@FK_Episodio", PK_Episodio);
                        this.SPU_Ref_Maltrato.Parameters.AddWithValue("@FK_Maltrato", this.WucEpisodioAdmision.MaltratoItem(i));
                        this.SPU_Ref_Maltrato.ExecuteNonQuery();
                        this.SPU_Ref_Maltrato.Dispose();
                    }
                    this.SPD_Ref_ProbJusticia = new System.Data.SqlClient.SqlCommand();
                    this.SPD_Ref_ProbJusticia.CommandText = "[SPD_Ref_ProbJusticia]";
                    this.SPD_Ref_ProbJusticia.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPD_Ref_ProbJusticia.Connection = this.cnn;
                    this.SPD_Ref_ProbJusticia.Parameters.AddWithValue("@FK_Episodio", PK_Episodio);
                    this.SPD_Ref_ProbJusticia.ExecuteNonQuery();
                    for (int i = 0; i < this.WucEpisodioAdmision.ProbJusticiaCount; i++)
                    {
                        this.SPU_Ref_ProbJusticia = new System.Data.SqlClient.SqlCommand();
                        this.SPU_Ref_ProbJusticia.CommandText = "[SPU_Ref_ProbJusticia]";
                        this.SPU_Ref_ProbJusticia.CommandType = System.Data.CommandType.StoredProcedure;
                        this.SPU_Ref_ProbJusticia.Connection = this.cnn;
                        this.SPU_Ref_ProbJusticia.Parameters.AddWithValue("@FK_Episodio", PK_Episodio);
                        this.SPU_Ref_ProbJusticia.Parameters.AddWithValue("@FK_ProbJusticia", this.WucEpisodioAdmision.ProbJusticiaItem(i));
                        this.SPU_Ref_ProbJusticia.ExecuteNonQuery();
                        this.SPU_Ref_ProbJusticia.Dispose();
                    }
                    this.SPD_Ref_CondicionesDiagnosticadas = new System.Data.SqlClient.SqlCommand();
                    this.SPD_Ref_CondicionesDiagnosticadas.CommandText = "[SPD_Ref_CondicionesDiagnosticadas]";
                    this.SPD_Ref_CondicionesDiagnosticadas.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPD_Ref_CondicionesDiagnosticadas.Connection = this.cnn;
                    this.SPD_Ref_CondicionesDiagnosticadas.Parameters.AddWithValue("@FK_Perfil", PK_Perfil);
                    this.SPD_Ref_CondicionesDiagnosticadas.ExecuteNonQuery();
                    for (int i = 0; i < this.WucEpisodioAdmision.CondicionesDiagnosticadasCount; i++)
                    {
                        this.SPU_Ref_CondicionesDiagnosticadas = new System.Data.SqlClient.SqlCommand();
                        this.SPU_Ref_CondicionesDiagnosticadas.CommandText = "[SPU_Ref_CondicionesDiagnosticadas]";
                        this.SPU_Ref_CondicionesDiagnosticadas.CommandType = System.Data.CommandType.StoredProcedure;
                        this.SPU_Ref_CondicionesDiagnosticadas.Connection = this.cnn;
                        this.SPU_Ref_CondicionesDiagnosticadas.Parameters.AddWithValue("@FK_Perfil", PK_Perfil);
                        this.SPU_Ref_CondicionesDiagnosticadas.Parameters.AddWithValue("@FK_Diagnostico", this.WucEpisodioAdmision.CondicionesDiagnosticadasItem(i));
                        this.SPU_Ref_CondicionesDiagnosticadas.ExecuteNonQuery();
                        this.SPU_Ref_CondicionesDiagnosticadas.Dispose();
                    }
                    this.SPD_Ref_CompFamilia = new System.Data.SqlClient.SqlCommand();
                    this.SPD_Ref_CompFamilia.CommandText = "[SPD_Ref_CompFamilia]";
                    this.SPD_Ref_CompFamilia.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPD_Ref_CompFamilia.Connection = this.cnn;
                    this.SPD_Ref_CompFamilia.Parameters.AddWithValue("@FK_Perfil", PK_Perfil);
                    this.SPD_Ref_CompFamilia.ExecuteNonQuery();
                    for (int i = 0; i < this.WucDatosDemograficos.CompFamCount; i++)
                    {
                        this.SPU_Ref_CompFamilia = new System.Data.SqlClient.SqlCommand();
                        this.SPU_Ref_CompFamilia.CommandText = "[SPU_Ref_CompFamilia]";
                        this.SPU_Ref_CompFamilia.CommandType = System.Data.CommandType.StoredProcedure;
                        this.SPU_Ref_CompFamilia.Connection = this.cnn;
                        this.SPU_Ref_CompFamilia.Parameters.AddWithValue("@FK_Perfil", PK_Perfil);
                        this.SPU_Ref_CompFamilia.Parameters.AddWithValue("@FK_CompFamilia", this.WucDatosDemograficos.CompFamItem(i));
                        this.SPU_Ref_CompFamilia.ExecuteNonQuery();
                        this.SPU_Ref_CompFamilia.Dispose();
                    }
                this.cnn.Close();
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                Trace.Warn("GuardarCambios()", m, ex);
                if (this.cnn.State != ConnectionState.Closed)
                {
                    this.cnn.Close();
                }
                throw ex;
            }
        }
        #endregion

        private void TipoPerfil()
        {
            string AS = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoSustancias"].ToString();
            string SM = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoMental"].ToString();
            if (SM != "99" && SM != "")
            {
                lblTipoPerfil.Text = "Salud Mental.";
            }
            else if (AS != "99" && AS != "")
            {
                lblTipoPerfil.Text = "Abuso de Sustancias.";
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
            this.daLkpEpidosio = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.cnn = new System.Data.SqlClient.SqlConnection();
            this.dsPerfil = new ASSMCA.perfiles.dsPerfil();
            this.daLkpPerfil = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
            this.daAdmision = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
            this.SPU_EPISODIO = new System.Data.SqlClient.SqlCommand();
            this.SPC_EPISODIO = new System.Data.SqlClient.SqlCommand();
            this.daLkpNivelCuidado = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand4 = new System.Data.SqlClient.SqlCommand();
            this.SPC_PERFIL = new System.Data.SqlClient.SqlCommand();
            this.SPU_PERFIL = new System.Data.SqlClient.SqlCommand();
            this.dsSeguridad = new ASSMCA.dsSeguridad();
            ((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSeguridad)).BeginInit();
            #region daLkpEpisodio
            this.daLkpEpidosio.SelectCommand = this.sqlSelectCommand1;
            this.daLkpEpidosio.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
                new System.Data.Common.DataTableMapping("Table", "SA_LKP_TEDS_SEGURO_SALUD", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_SeguroSalud", "PK_SeguroSalud"),
                new System.Data.Common.DataColumnMapping("DE_SeguroSalud", "DE_SeguroSalud")}),
                new System.Data.Common.DataTableMapping("Table1", "SA_LKP_TEDS_PAGO", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Pago", "PK_Pago"),
                new System.Data.Common.DataColumnMapping("DE_Pago", "DE_Pago")}),
                new System.Data.Common.DataTableMapping("Table2", "SA_LKP_FEMINA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Femina", "PK_Femina"),
                new System.Data.Common.DataColumnMapping("DE_Femina", "DE_Femina")}),
                new System.Data.Common.DataTableMapping("Table3", "SA_LKP_TEDS_FUENTE_INGRESO", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_FuenteIngreso", "PK_FuenteIngreso"),
                new System.Data.Common.DataColumnMapping("DE_FuenteIngreso", "DE_FuenteIngreso")}),
                new System.Data.Common.DataTableMapping("Table4", "SA_LKP_INGRESO_ANUAL", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_IngresoAnual", "PK_IngresoAnual"),
                new System.Data.Common.DataColumnMapping("DE_IngresoAnual", "DE_IngresoAnual")}),
                new System.Data.Common.DataTableMapping("Table5", "SA_LKP_TIEMPO_RESIDENCIA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_TiempoResidencia", "PK_TiempoResidencia"),
                new System.Data.Common.DataColumnMapping("DE_TiempoResidencia", "DE_TiempoResidencia")}),
                new System.Data.Common.DataTableMapping("Table6", "SA_LKP_MUNICIPIO_RESIDENCIA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Municipio", "PK_Municipio"),
                new System.Data.Common.DataColumnMapping("DE_Municipio", "DE_Municipio")}),
                new System.Data.Common.DataTableMapping("Table7", "SA_LKP_TEDS_ETAPA_SERVICIO", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_EtapaServicio", "PK_EtapaServicio"),
                new System.Data.Common.DataColumnMapping("DE_EtapaServicio", "DE_EtapaServicio")}),
                new System.Data.Common.DataTableMapping("Table8", "SA_LKP_TEDS_REFERIDO", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Referido", "PK_Referido"),
                new System.Data.Common.DataColumnMapping("DE_Referido", "DE_Referido")}),
                new System.Data.Common.DataTableMapping("Table9", "SA_LKP_TEDS_ESTADO_LEGAL", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_EstadoLegal", "PK_EstadoLegal"),
                new System.Data.Common.DataColumnMapping("DE_EstadoLegal", "DE_EstadoLegal")}),
                new System.Data.Common.DataTableMapping("Table10", "SA_LKP_PROBLEMA_JUSTICIA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_ProbJusticia", "PK_ProbJusticia"),
                new System.Data.Common.DataColumnMapping("DE_ProbJusticia", "DE_ProbJusticia")}),
                new System.Data.Common.DataTableMapping("Table11", "SA_LKP_TEDS_EPISODIO_PREVIO", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_EpisodiosPrevios", "PK_EpisodiosPrevios"),
                new System.Data.Common.DataColumnMapping("DE_EpisodiosPrevios", "DE_EpisodiosPrevios")}),
                new System.Data.Common.DataTableMapping("Table12", "SA_LKP_TIEMPO_ULT_TRAT", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_TiempoUltTrat", "PK_TiempoUltTrat"),
                new System.Data.Common.DataColumnMapping("DE_TiempoUltTrat", "DE_TiempoUltTrat")}),
                new System.Data.Common.DataTableMapping("Table13", "SA_LKP_ABUSO_SUSTANCIAS_ANTERIOR", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_AbusoSustancias", "PK_AbusoSustancias"),
                new System.Data.Common.DataColumnMapping("DE_AbusoSustancias", "DE_AbusoSustancias")}),
                new System.Data.Common.DataTableMapping("Table14", "SA_LKP_SALUD_MENTAL_ANTERIOR", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_SaludMental", "PK_SaludMental"),
                new System.Data.Common.DataColumnMapping("DE_SaludMental", "DE_SaludMental")})});
            this.sqlSelectCommand1.CommandText = "[SPR_LKP_EPISODIO]";
            this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand1.Connection = this.cnn;
            this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            #endregion
            #region LKP PERFIL
            this.cnn.ConnectionString = NewSource.connectionString;
            this.dsPerfil.DataSetName = "dsPerfil";
            this.dsPerfil.Locale = new System.Globalization.CultureInfo("en-US");
            this.daLkpPerfil.SelectCommand = this.sqlSelectCommand2;
            this.daLkpPerfil.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
                new System.Data.Common.DataTableMapping("Table", "SA_LKP_TEDS_ESTADO_MARITAL", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_EstadoMarital", "PK_EstadoMarital"),
                new System.Data.Common.DataColumnMapping("DE_EstadoMarital", "DE_EstadoMarital")}),
                new System.Data.Common.DataTableMapping("Table1", "SA_LKP_TEDS_COND_LABORAL", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_CondLaboral", "PK_CondLaboral"),
                new System.Data.Common.DataColumnMapping("DE_CondLaboral", "DE_CondLaboral")}),
                new System.Data.Common.DataTableMapping("Table2", "SA_LKP_TEDS_NO_FUERZA_LABORAL", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_NoFuerzaLaboral", "PK_NoFuerzaLaboral"),
                new System.Data.Common.DataColumnMapping("DE_NoFuerzaLaboral", "DE_NoFuerzaLaboral")}),
                new System.Data.Common.DataTableMapping("Table3", "SA_LKP_TEDS_GRADO", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Grado", "PK_Grado"),
                new System.Data.Common.DataColumnMapping("DE_Grado", "DE_Grado")}),
                new System.Data.Common.DataTableMapping("Table4", "SA_LKP_COMPOSICION_FAMILIAR", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Familiar", "PK_Familiar"),
                new System.Data.Common.DataColumnMapping("DE_Familiar", "DE_Familiar")}),
                new System.Data.Common.DataTableMapping("Table5", "SA_LKP_TEDS_RESIDENCIA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Residencia", "PK_Residencia"),
                new System.Data.Common.DataColumnMapping("DE_Residencia", "DE_Residencia")}),
                new System.Data.Common.DataTableMapping("Table6", "SA_LKP_DIAGNOSTICO", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Diagnostico", "PK_Diagnostico"),
                new System.Data.Common.DataColumnMapping("DE_Diagnostico", "DE_Diagnostico")}),
                new System.Data.Common.DataTableMapping("Table7", "SA_LKP_DSMIV_CAT", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_DSMIVCat", "PK_DSMIVCat"),
                new System.Data.Common.DataColumnMapping("DE_DSMIVCat", "DE_DSMIVCat")}),
                new System.Data.Common.DataTableMapping("Table8", "SA_LKP_DSMIV", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_DSMIV", "PK_DSMIV"),
                new System.Data.Common.DataColumnMapping("DE_DSMIV", "DE_DSMIV"),
                new System.Data.Common.DataColumnMapping("PK_DSMIV1", "PK_DSMIV1"),
                new System.Data.Common.DataColumnMapping("FK_DSMIVCat", "FK_DSMIVCat")}),
                new System.Data.Common.DataTableMapping("Table9", "SA_LKP_DSMIV_IV", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_DSMIV_IV", "PK_DSMIV_IV"),
                new System.Data.Common.DataColumnMapping("DE_DSMIV_IV", "DE_DSMIV_IV")}),
                new System.Data.Common.DataTableMapping("Table10", "SA_LKP_REFERIDOS_TX", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_ReferidosTX", "PK_ReferidosTX"),
                new System.Data.Common.DataColumnMapping("DE_ReferidosTX", "DE_ReferidosTX")}),
                new System.Data.Common.DataTableMapping("Table11", "SA_LKP_TEDS_SUSTANCIA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Sustancia", "PK_Sustancia"),
                new System.Data.Common.DataColumnMapping("DE_Sustancia", "DE_Sustancia")}),
                new System.Data.Common.DataTableMapping("Table12", "SA_LKP_TEDS_VIA_UTILIZACION", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_ViaUtilizacion", "PK_ViaUtilizacion"),
                new System.Data.Common.DataColumnMapping("DE_ViaUtilizacion", "DE_ViaUtilizacion")}),
                new System.Data.Common.DataTableMapping("Table13", "SA_LKP_TEDS_FRECUENCIA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Frecuencia", "PK_Frecuencia"),
                new System.Data.Common.DataColumnMapping("DE_Frecuencia", "DE_Frecuencia")}),
                new System.Data.Common.DataTableMapping("Table14", "SA_LKP_MEDIDA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Medida", "PK_Medida"),
                new System.Data.Common.DataColumnMapping("DE_Medida", "DE_Medida")}),
                new System.Data.Common.DataTableMapping("Table15", "SA_LKP_TEDS_FRECUENCIA_AUTOAYUDA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_FreqAutoAyuda", "PK_FreqAutoAyuda"),
                new System.Data.Common.DataColumnMapping("DE_FreqAutoAyuda", "DE_FreqAutoAyuda")}),
                new System.Data.Common.DataTableMapping("Table16", "SA_LKP_TEDS_SITUACION_ESCOLAR", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_SituacionEscolar", "PK_SituacionEscolar"),
                new System.Data.Common.DataColumnMapping("DE_SituacionEscolar", "DE_SituacionEscolar")}),
                new System.Data.Common.DataTableMapping("Table17", "SA_LKP_TEDS_TIPO_ADMISION", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_TipoAdmision", "PK_TipoAdmision"),
                new System.Data.Common.DataColumnMapping("DE_TipoAdmision", "DE_TipoAdmision")}),
                new System.Data.Common.DataTableMapping("Table18", "SA_LKP_DSMV_ProblemasPsicosocialesAmbientales", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_DSMV_ProblemasPsicosocialesAmbientales", "PK_DSMV_ProblemasPsicosocialesAmbientales"),
                new System.Data.Common.DataColumnMapping("DE_DSMV_ProblemasPsicosocialesAmbientales", "DE_DSMV_ProblemasPsicosocialesAmbientales")}),
                new System.Data.Common.DataTableMapping("Table19", "SA_LKP_DSMV", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_DSMV", "PK_DSMV"),
                new System.Data.Common.DataColumnMapping("_PK_DSMV", "_PK_DSMV"),
                new System.Data.Common.DataColumnMapping("DE_DSMV", "DE_DSMV")})});
            this.sqlSelectCommand2.CommandText = "[SPR_LKP_PERFIL]";
            this.sqlSelectCommand2.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand2.Connection = this.cnn;
            this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            #endregion
            #region SPR_Admision
            this.daAdmision.SelectCommand = this.sqlSelectCommand3;
            this.daAdmision.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
                new System.Data.Common.DataTableMapping("Table", "SA_EPISODIO", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Episodio", "PK_Episodio"),
                new System.Data.Common.DataColumnMapping("FK_Persona", "FK_Persona"),
                new System.Data.Common.DataColumnMapping("FK_Programa", "FK_Programa"),
                new System.Data.Common.DataColumnMapping("NB_Programa", "NB_Programa"),
                new System.Data.Common.DataColumnMapping("PK_Administracion", "PK_Administracion"),
                new System.Data.Common.DataColumnMapping("NB_Administracion", "NB_Administracion"),
                new System.Data.Common.DataColumnMapping("FE_Episodio", "FE_Episodio"),
                new System.Data.Common.DataColumnMapping("FK_SeguroSalud", "FK_SeguroSalud"),
                new System.Data.Common.DataColumnMapping("DE_SeguroSalud", "DE_SeguroSalud"),
                new System.Data.Common.DataColumnMapping("FK_FuentePago", "FK_FuentePago"),
                new System.Data.Common.DataColumnMapping("DE_Pago", "DE_Pago"),
                new System.Data.Common.DataColumnMapping("FK_FeminaHijos", "FK_FeminaHijos"),
                new System.Data.Common.DataColumnMapping("DE_Femina", "DE_Femina"),
                new System.Data.Common.DataColumnMapping("IN_VaronHijos", "IN_VaronHijos"),
                new System.Data.Common.DataColumnMapping("DE_VaronHijos", "DE_VaronHijos"),
                new System.Data.Common.DataColumnMapping("FK_FuenteIngreso", "FK_FuenteIngreso"),
                new System.Data.Common.DataColumnMapping("DE_FuenteIngreso", "DE_FuenteIngreso"),
                new System.Data.Common.DataColumnMapping("FK_IngresoIndividual", "FK_IngresoIndividual"),
                new System.Data.Common.DataColumnMapping("DE_IngresoAnualIndividual", "DE_IngresoAnualIndividual"),
                new System.Data.Common.DataColumnMapping("FK_IngresoFamiliar", "FK_IngresoFamiliar"),
                new System.Data.Common.DataColumnMapping("DE_IngresoAnualFamiliar", "DE_IngresoAnualFamiliar"),
                new System.Data.Common.DataColumnMapping("FK_TiempoResidencia", "FK_TiempoResidencia"),
                new System.Data.Common.DataColumnMapping("DE_TiempoResidencia", "DE_TiempoResidencia"),
                new System.Data.Common.DataColumnMapping("FK_Municipio", "FK_Municipio"),
                new System.Data.Common.DataColumnMapping("DE_Municipio", "DE_Municipio"),
                new System.Data.Common.DataColumnMapping("IN_Zona", "IN_Zona"),
                new System.Data.Common.DataColumnMapping("DE_Zona", "DE_Zona"),
                new System.Data.Common.DataColumnMapping("NR_ZipCode", "NR_ZipCode"),
                new System.Data.Common.DataColumnMapping("DE_Barrio", "DE_Barrio"),
                new System.Data.Common.DataColumnMapping("FK_EtapaServicio", "FK_EtapaServicio"),
                new System.Data.Common.DataColumnMapping("DE_EtapaServicio", "DE_EtapaServicio"),
                new System.Data.Common.DataColumnMapping("FK_NivelCuidadoSustancias", "FK_NivelCuidadoSustancias"),
                new System.Data.Common.DataColumnMapping("DE_AbusoSustancias", "DE_AbusoSustancias"),
                new System.Data.Common.DataColumnMapping("FK_NivelCuidadoMental", "FK_NivelCuidadoMental"),
                new System.Data.Common.DataColumnMapping("DE_SaludMental", "DE_SaludMental"),
                new System.Data.Common.DataColumnMapping("IN_Metadona", "IN_Metadona"),
                new System.Data.Common.DataColumnMapping("DE_Metadona", "DE_Metadona"),
                new System.Data.Common.DataColumnMapping("IN_CodDependiente", "IN_CodDependiente"),
                new System.Data.Common.DataColumnMapping("DE_CodDependiente", "DE_CodDependiente"),
                new System.Data.Common.DataColumnMapping("IN_DiagnosticoDual", "IN_DiagnosticoDual"),
                new System.Data.Common.DataColumnMapping("DE_DiagnosticoDual", "DE_DiagnosticoDual"),
                new System.Data.Common.DataColumnMapping("FK_FuenteReferido", "FK_FuenteReferido"),
                new System.Data.Common.DataColumnMapping("DE_Referido", "DE_Referido"),
                new System.Data.Common.DataColumnMapping("FK_EstadoLegal", "FK_EstadoLegal"),
                new System.Data.Common.DataColumnMapping("DE_EstadoLegal", "DE_EstadoLegal"),
                new System.Data.Common.DataColumnMapping("IN_ArrestadoAnteriormente", "IN_ArrestadoAnteriormente"),
                new System.Data.Common.DataColumnMapping("DE_ArrestadoAnteriormente", "DE_ArrestadoAnteriormente"),
                new System.Data.Common.DataColumnMapping("FK_Justicia", "FK_Justicia"),
                new System.Data.Common.DataColumnMapping("DE_ProbJusticia", "DE_ProbJusticia"),
                new System.Data.Common.DataColumnMapping("NR_DiasEsperaSustancias", "NR_DiasEsperaSustancias"),
                new System.Data.Common.DataColumnMapping("FK_EpisodiosSustancias", "FK_EpisodiosSustancias"),
                new System.Data.Common.DataColumnMapping("DE_EpisodiosPreviosSustancias", "DE_EpisodiosPreviosSustancias"),
                new System.Data.Common.DataColumnMapping("FK_DuracionSustancias", "FK_DuracionSustancias"),
                new System.Data.Common.DataColumnMapping("DE_TiempoUltTratSustancias", "DE_TiempoUltTratSustancias"),
                new System.Data.Common.DataColumnMapping("NR_DiasUltimaAltaSustancias", "NR_DiasUltimaAltaSustancias"),
                new System.Data.Common.DataColumnMapping("NR_MesesUltimaAltaSustancias", "NR_MesesUltimaAltaSustancias"),
                new System.Data.Common.DataColumnMapping("FK_NivelCuidadoSustanciasAnterior", "FK_NivelCuidadoSustanciasAnterior"),
                new System.Data.Common.DataColumnMapping("DE_AbusoSustanciasAnterior", "DE_AbusoSustanciasAnterior"),
                new System.Data.Common.DataColumnMapping("IN_SaludMental", "IN_SaludMental"),
                new System.Data.Common.DataColumnMapping("DE_IN_SaludMental", "DE_IN_SaludMental"),
                new System.Data.Common.DataColumnMapping("NR_DiasEsperaMental", "NR_DiasEsperaMental"),
                new System.Data.Common.DataColumnMapping("FK_EpisodiosMental", "FK_EpisodiosMental"),
                new System.Data.Common.DataColumnMapping("DE_EpisodiosPreviosMental", "DE_EpisodiosPreviosMental"),
                new System.Data.Common.DataColumnMapping("FK_DuracionMental", "FK_DuracionMental"),
                new System.Data.Common.DataColumnMapping("DE_TiempoUltTratMental", "DE_TiempoUltTratMental"),
                new System.Data.Common.DataColumnMapping("NR_DiasUltimaAltaMental", "NR_DiasUltimaAltaMental"),
                new System.Data.Common.DataColumnMapping("NR_MesesUltimaAltaMental", "NR_MesesUltimaAltaMental"),
                new System.Data.Common.DataColumnMapping("FK_NivelCuidadoMentalAnterior", "FK_NivelCuidadoMentalAnterior"),
                new System.Data.Common.DataColumnMapping("DE_SaludMentalAnterior", "DE_SaludMentalAnterior"),
                new System.Data.Common.DataColumnMapping("IN_AbusoSustancias", "IN_AbusoSustancias"),
                new System.Data.Common.DataColumnMapping("DE_IN_AbusoSustancias", "DE_IN_AbusoSustancias"),
                new System.Data.Common.DataColumnMapping("IN_ViolenciaDomestica", "IN_ViolenciaDomestica"),
                new System.Data.Common.DataColumnMapping("DE_ViolenciaDomestica", "DE_ViolenciaDomestica"),
                new System.Data.Common.DataColumnMapping("IN_Maltrato", "IN_Maltrato"),
                new System.Data.Common.DataColumnMapping("DE_Maltrato", "DE_Maltrato"),
                new System.Data.Common.DataColumnMapping("IN_TI_Maltrato", "IN_TI_Maltrato"),
                new System.Data.Common.DataColumnMapping("DE_TI_Maltrato", "DE_TI_Maltrato"),
                new System.Data.Common.DataColumnMapping("IN_Suicida", "IN_Suicida"),
                new System.Data.Common.DataColumnMapping("DE_Suicida", "DE_Suicida"),
                new System.Data.Common.DataColumnMapping("IN_IdeaSuicida", "IN_IdeaSuicida"),
                new System.Data.Common.DataColumnMapping("DE_IdeaSuicida", "DE_IdeaSuicida"),
                new System.Data.Common.DataColumnMapping("IN_TratadoMental", "IN_TratadoMental"),
                new System.Data.Common.DataColumnMapping("DE_TratadoMental", "DE_TratadoMental"),
                new System.Data.Common.DataColumnMapping("IN_Ambulatorio", "IN_Ambulatorio"),
                new System.Data.Common.DataColumnMapping("DE_Ambulatorio", "DE_Ambulatorio"),
                new System.Data.Common.DataColumnMapping("IN_HospitalizadoMental", "IN_HospitalizadoMental"),
                new System.Data.Common.DataColumnMapping("DE_HospitalizadoMental", "DE_HospitalizadoMental"),
                new System.Data.Common.DataColumnMapping("IN_TI_Hospital", "IN_TI_Hospital"),
                new System.Data.Common.DataColumnMapping("DE_TI_Hospital", "DE_TI_Hospital"),
                new System.Data.Common.DataColumnMapping("IN_TratamientoResidencial", "IN_TratamientoResidencial"),
                new System.Data.Common.DataColumnMapping("DE_TratamientoResidencial", "DE_TratamientoResidencial"),
                new System.Data.Common.DataColumnMapping("ES_Episodio", "ES_Episodio"),
                new System.Data.Common.DataColumnMapping("FE_FechaConvenio", "FE_FechaConvenio")}),
                new System.Data.Common.DataTableMapping("Table1", "SA_PERFIL", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_NR_Perfil", "PK_NR_Perfil"),
                new System.Data.Common.DataColumnMapping("FK_Episodio", "FK_Episodio"),
                new System.Data.Common.DataColumnMapping("FE_Perfil", "FE_Perfil"),
                new System.Data.Common.DataColumnMapping("IN_TI_Perfil", "IN_TI_Perfil"),
                new System.Data.Common.DataColumnMapping("DE_TI_Perfil", "DE_TI_Perfil"),
                new System.Data.Common.DataColumnMapping("IN_Emancipado", "IN_Emancipado"),
                new System.Data.Common.DataColumnMapping("DE_Emancipado", "DE_Emancipado"),
                new System.Data.Common.DataColumnMapping("FK_EstadoMarital", "FK_EstadoMarital"),
                new System.Data.Common.DataColumnMapping("DE_EstadoMarital", "DE_EstadoMarital"),
                new System.Data.Common.DataColumnMapping("FK_CondicionLaboral", "FK_CondicionLaboral"),
                new System.Data.Common.DataColumnMapping("DE_CondLaboral", "DE_CondLaboral"),
                new System.Data.Common.DataColumnMapping("FK_ActividadNoLaboral", "FK_ActividadNoLaboral"),
                new System.Data.Common.DataColumnMapping("DE_NoFuerzaLaboral", "DE_NoFuerzaLaboral"),
                new System.Data.Common.DataColumnMapping("IN_EmbarazosTratamiento", "IN_EmbarazosTratamiento"),
                new System.Data.Common.DataColumnMapping("DE_EmbarazosTratamiento", "DE_EmbarazosTratamiento"),
                new System.Data.Common.DataColumnMapping("NR_Hijos", "NR_Hijos"),
                new System.Data.Common.DataColumnMapping("FK_Escolaridad", "FK_Escolaridad"),
                new System.Data.Common.DataColumnMapping("DE_Grado", "DE_Grado"),
                new System.Data.Common.DataColumnMapping("FE_UltimoInformeEscolar", "FE_UltimoInformeEscolar"),
                new System.Data.Common.DataColumnMapping("NR_PromedioAcademico", "NR_PromedioAcademico"),
                new System.Data.Common.DataColumnMapping("NR_AusenciasAcad", "NR_AusenciasAcad"),
                new System.Data.Common.DataColumnMapping("IN_EducEspecial", "IN_EducEspecial"),
                new System.Data.Common.DataColumnMapping("DE_EducEspecial", "DE_EducEspecial"),
                new System.Data.Common.DataColumnMapping("IN_DesertorEscolar", "IN_DesertorEscolar"),
                new System.Data.Common.DataColumnMapping("DE_DesertorEscolar", "DE_DesertorEscolar"),
                new System.Data.Common.DataColumnMapping("FK_Familia", "FK_Familia"),
                new System.Data.Common.DataColumnMapping("DE_Familiar", "DE_Familiar"),
                new System.Data.Common.DataColumnMapping("NR_Familiar", "NR_Familiar"),
                new System.Data.Common.DataColumnMapping("FK_Residencia", "FK_Residencia"),
                new System.Data.Common.DataColumnMapping("DE_Residencia", "DE_Residencia"),
                new System.Data.Common.DataColumnMapping("IN_ParticReunGrupos", "IN_ParticReunGrupos"),
                new System.Data.Common.DataColumnMapping("DE_ParticReunGrupos", "DE_ParticReunGrupos"),
                new System.Data.Common.DataColumnMapping("FK_FreqAutoAyuda", "FK_FreqAutoAyuda"),
                new System.Data.Common.DataColumnMapping("DE_FreqAutoAyuda", "DE_FreqAutoAyuda"),
                new System.Data.Common.DataColumnMapping("NR_Arrestos30dias", "NR_Arrestos30dias"),
                new System.Data.Common.DataColumnMapping("IN_Arrestado30dias", "IN_Arrestado30dias"),
                new System.Data.Common.DataColumnMapping("IN_EstLeg", "IN_EstLeg"),
                new System.Data.Common.DataColumnMapping("DE_Arrestado30dias", "DE_Arrestado30dias"),
                new System.Data.Common.DataColumnMapping("DE_EstLeg", "DE_EstLeg"),
                new System.Data.Common.DataColumnMapping("FK_CondicionPrimaria", "FK_CondicionPrimaria"),
                new System.Data.Common.DataColumnMapping("DE_DiagnosticoPrimario", "DE_DiagnosticoPrimario"),
                new System.Data.Common.DataColumnMapping("FK_CondicionSecundaria", "FK_CondicionSecundaria"),
                new System.Data.Common.DataColumnMapping("DE_DiagnosticoSecundario", "DE_DiagnosticoSecundario"),
                new System.Data.Common.DataColumnMapping("FK_CondicionTerciaria", "FK_CondicionTerciaria"),
                new System.Data.Common.DataColumnMapping("DE_DiagnosticoTerciaria", "DE_DiagnosticoTerciaria"),
                new System.Data.Common.DataColumnMapping("FK_TrastornosClinicosPrimario", "FK_TrastornosClinicosPrimario"),
                new System.Data.Common.DataColumnMapping("DE_DSMIV_TCP", "DE_DSMIV_TCP"),
                new System.Data.Common.DataColumnMapping("FK_TrastornosClinicosSecundario", "FK_TrastornosClinicosSecundario"),
                new System.Data.Common.DataColumnMapping("DE_DSMIV_TCS", "DE_DSMIV_TCS"),
                new System.Data.Common.DataColumnMapping("FK_TrastornosClinicosTerciario", "FK_TrastornosClinicosTerciario"),
                new System.Data.Common.DataColumnMapping("DE_DSMIV_TCT", "DE_DSMIV_TCT"),
                new System.Data.Common.DataColumnMapping("FK_TrastornosPersonalidadPrimario", "FK_TrastornosPersonalidadPrimario"),
                new System.Data.Common.DataColumnMapping("DE_DSMIV_TPP", "DE_DSMIV_TPP"),
                new System.Data.Common.DataColumnMapping("FK_TrastornosPersonalidadSecundario", "FK_TrastornosPersonalidadSecundario"),
                new System.Data.Common.DataColumnMapping("DE_DSMIV_TPS", "DE_DSMIV_TPS"),
                new System.Data.Common.DataColumnMapping("FK_TrastornosPersonalidadTerciario", "FK_TrastornosPersonalidadTerciario"),
                new System.Data.Common.DataColumnMapping("DE_DSMIV_TPT", "DE_DSMIV_TPT"),
                new System.Data.Common.DataColumnMapping("CO_CondicionesMedicasPrimario", "CO_CondicionesMedicasPrimario"),
                new System.Data.Common.DataColumnMapping("CO_CondicionesMedicasSecundario", "CO_CondicionesMedicasSecundario"),
                new System.Data.Common.DataColumnMapping("CO_CondicionesMedicasTerciario", "CO_CondicionesMedicasTerciario"),
                new System.Data.Common.DataColumnMapping("FK_ProblemasPsicosocialesPrimario", "FK_ProblemasPsicosocialesPrimario"),
                new System.Data.Common.DataColumnMapping("DE_DSMIV_IV_P", "DE_DSMIV_IV_P"),
                new System.Data.Common.DataColumnMapping("FK_ProblemasPsicosocialesSecundario", "FK_ProblemasPsicosocialesSecundario"),
                new System.Data.Common.DataColumnMapping("DE_DSMIV_IV_S", "DE_DSMIV_IV_S"),
                new System.Data.Common.DataColumnMapping("FK_ProblemasPsicosocialesTerciario", "FK_ProblemasPsicosocialesTerciario"),
                new System.Data.Common.DataColumnMapping("DE_DSMIV_IV_T", "DE_DSMIV_IV_T"),
                new System.Data.Common.DataColumnMapping("NR_EscalaGAF", "NR_EscalaGAF"),
                new System.Data.Common.DataColumnMapping("FK_ReferidosGeneradosTX", "FK_ReferidosGeneradosTX"),
                new System.Data.Common.DataColumnMapping("DE_ReferidosTX", "DE_ReferidosTX"),
                new System.Data.Common.DataColumnMapping("FK_DisposicionFinalReferido", "FK_DisposicionFinalReferido"),
                new System.Data.Common.DataColumnMapping("DE_DisposicionFinal", "DE_DisposicionFinal"),
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
                new System.Data.Common.DataColumnMapping("NR_PromedioVisitas", "NR_PromedioVisitas"),
                new System.Data.Common.DataColumnMapping("FK_Alta", "FK_Alta"),
                new System.Data.Common.DataColumnMapping("DE_Alta", "DE_Alta"),
                new System.Data.Common.DataColumnMapping("FK_CentroTraslado", "FK_CentroTraslado"),
                new System.Data.Common.DataColumnMapping("NB_Programa", "NB_Programa"),
                new System.Data.Common.DataColumnMapping("DE_Comentario", "DE_Comentario"),
                new System.Data.Common.DataColumnMapping("TI_Transaccion", "TI_Transaccion"),
                new System.Data.Common.DataColumnMapping("DE_Transaccion", "DE_Transaccion"),
                new System.Data.Common.DataColumnMapping("PK_SituacionEscolar", "PK_SituacionEscolar"),
                new System.Data.Common.DataColumnMapping("DE_SituacionEscolar", "DE_SituacionEscolar"),
                new System.Data.Common.DataColumnMapping("FK_Sesion", "FK_Sesion"),
                new System.Data.Common.DataColumnMapping("TI_Edicion", "TI_Edicion"),
                new System.Data.Common.DataColumnMapping("DE_Edicion", "DE_Edicion"),
                new System.Data.Common.DataColumnMapping("FE_Edicion", "FE_Edicion"),
                new System.Data.Common.DataColumnMapping("ES_Perfil", "ES_Perfil"),
                new System.Data.Common.DataColumnMapping("DE_SaludMental", "DE_SaludMental"),
                new System.Data.Common.DataColumnMapping("DE_AbusoSustancias", "DE_AbusoSustancias"),
                new System.Data.Common.DataColumnMapping("NB_ProgramaActual", "NB_ProgramaActual"),
                new System.Data.Common.DataColumnMapping("NB_AdministracionActual", "NB_AdministracionActual"),
                new System.Data.Common.DataColumnMapping("FK_Persona", "FK_Persona"),
                new System.Data.Common.DataColumnMapping("DE_TipoAdmision", "DE_TipoAdmision"),
                new System.Data.Common.DataColumnMapping("PK_TipoAdmision", "PK_TipoAdmision"),
                /* DSMV */
                new System.Data.Common.DataColumnMapping("DE_DSMV_TrastornosClinicos1", "DE_DSMV_TrastornosClinicos1"),
                new System.Data.Common.DataColumnMapping("DE_DSMV_TrastornosClinicos2", "DE_DSMV_TrastornosClinicos2"),
                new System.Data.Common.DataColumnMapping("DE_DSMV_TrastornosClinicos3", "DE_DSMV_TrastornosClinicos3"),
                new System.Data.Common.DataColumnMapping("CODE_DSMV_TrastornosClinicos1", "CODE_DSMV_TrastornosClinicos1"),
                new System.Data.Common.DataColumnMapping("CODE_DSMV_TrastornosClinicos2", "CODE_DSMV_TrastornosClinicos2"),
                new System.Data.Common.DataColumnMapping("CODE_DSMV_TrastornosClinicos3", "CODE_DSMV_TrastornosClinicos3"),
                new System.Data.Common.DataColumnMapping("FK_DSMV_TrastornosClinicos1", "FK_DSMV_TrastornosClinicos1"),
                new System.Data.Common.DataColumnMapping("FK_DSMV_TrastornosClinicos2", "FK_DSMV_TrastornosClinicos2"),
                new System.Data.Common.DataColumnMapping("FK_DSMV_TrastornosClinicos3", "FK_DSMV_TrastornosClinicos3"),
                new System.Data.Common.DataColumnMapping("DE_DSMV_TrastornosPersonalidadRM1", "DE_DSMV_TrastornosPersonalidadRM1"),
                new System.Data.Common.DataColumnMapping("DE_DSMV_TrastornosPersonalidadRM2", "DE_DSMV_TrastornosPersonalidadRM2"),
                new System.Data.Common.DataColumnMapping("DE_DSMV_TrastornosPersonalidadRM3", "DE_DSMV_TrastornosPersonalidadRM3"),
                new System.Data.Common.DataColumnMapping("CODE_DSMV_TrastornosPersonalidadRM1", "CODE_DSMV_TrastornosPersonalidadRM1"),
                new System.Data.Common.DataColumnMapping("CODE_DSMV_TrastornosPersonalidadRM2", "CODE_DSMV_TrastornosPersonalidadRM2"),
                new System.Data.Common.DataColumnMapping("CODE_DSMV_TrastornosPersonalidadRM3", "CODE_DSMV_TrastornosPersonalidadRM3"),
                new System.Data.Common.DataColumnMapping("FK_DSMV_TrastornosPersonalidadRM1", "FK_DSMV_TrastornosPersonalidadRM1"),
                new System.Data.Common.DataColumnMapping("FK_DSMV_TrastornosPersonalidadRM2", "FK_DSMV_TrastornosPersonalidadRM2"),
                new System.Data.Common.DataColumnMapping("FK_DSMV_TrastornosPersonalidadRM3", "FK_DSMV_TrastornosPersonalidadRM3"),
                new System.Data.Common.DataColumnMapping("DE_DSMV_ProblemasPsicosocialesAmbientales1", "DE_DSMV_ProblemasPsicosocialesAmbientales1"),
                new System.Data.Common.DataColumnMapping("DE_DSMV_ProblemasPsicosocialesAmbientales2", "DE_DSMV_ProblemasPsicosocialesAmbientales2"),
                new System.Data.Common.DataColumnMapping("DE_DSMV_ProblemasPsicosocialesAmbientales3", "DE_DSMV_ProblemasPsicosocialesAmbientales3"),
                new System.Data.Common.DataColumnMapping("FK_DSMV_ProblemasPsicosocialesAmbientales1", "FK_DSMV_ProblemasPsicosocialesAmbientales1"),
                new System.Data.Common.DataColumnMapping("FK_DSMV_ProblemasPsicosocialesAmbientales2", "FK_DSMV_ProblemasPsicosocialesAmbientales2"),
                new System.Data.Common.DataColumnMapping("FK_DSMV_ProblemasPsicosocialesAmbientales3", "FK_DSMV_ProblemasPsicosocialesAmbientales3"),
                new System.Data.Common.DataColumnMapping("NR_DSMV_FuncionamientoGlobal", "NR_DSMV_FuncionamientoGlobal"),
                new System.Data.Common.DataColumnMapping("DE_DSMV_Comentarios", "DE_DSMV_Comentarios"),
                new System.Data.Common.DataColumnMapping("DE_DSMV_OtrasObservaciones", "DE_DSMV_OtrasObservaciones"),
                new System.Data.Common.DataColumnMapping("IN_DSMV_DiagnosticoDual", "IN_DSMV_DiagnosticoDual"),
                new System.Data.Common.DataColumnMapping("DE_DSMV_DiagnosticoDual", "DE_DSMV_DiagnosticoDual")})});
            this.sqlSelectCommand3.CommandText = "[SPR_ADMISION]";
            this.sqlSelectCommand3.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand3.Connection = this.cnn;
            this.sqlSelectCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            this.sqlSelectCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Admision", System.Data.SqlDbType.Int, 4));
            #endregion
            #region SPU_EPISODIO
            this.SPU_EPISODIO.CommandText = "dbo.[SPU_EPISODIO]";
            this.SPU_EPISODIO.CommandType = System.Data.CommandType.StoredProcedure;
            this.SPU_EPISODIO.Connection = this.cnn;
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Episodio", System.Data.SqlDbType.Int, 4));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_Episodio", System.Data.SqlDbType.DateTime, 8));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_SeguroSalud", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FuentePago", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FeminaHijos", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_VaronHijos", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FuenteIngreso", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TiempoResidencia", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Municipio", System.Data.SqlDbType.SmallInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Zona", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_ZipCode", System.Data.SqlDbType.VarChar, 5));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_EtapaServicio", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_NivelCuidadoSustancias", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_NivelCuidadoMental", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Metadona", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_CodDependiente", System.Data.SqlDbType.TinyInt));
            //this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_DiagnosticoDual", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FuenteReferido", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_EstadoLegal", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_ArrestadoAnteriormente", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Justicia", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_DiasEsperaSustancias", System.Data.SqlDbType.SmallInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_EpisodiosSustancias", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DuracionSustancias", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_DiasUltimaAltaSustancias", System.Data.SqlDbType.SmallInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_MesesUltimaAltaSustancias", System.Data.SqlDbType.SmallInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_NivelCuidadoSustanciasAnterior", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_DiasEsperaMental", System.Data.SqlDbType.SmallInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_EpisodiosMental", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DuracionMental", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_DiasUltimaAltaMental", System.Data.SqlDbType.SmallInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_MesesUltimaAltaMental", System.Data.SqlDbType.SmallInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_NivelCuidadoMentalAnterior", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_ViolenciaDomestica", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Maltrato", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_TI_Maltrato", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Suicida", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_IdeaSuicida", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_TratadoMental", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Ambulatorio", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_HospitalizadoMental", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_TI_Hospital", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_TratamientoResidencial", System.Data.SqlDbType.TinyInt));
            this.SPU_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_FechaConvenio", System.Data.SqlDbType.Date, 8));
            #endregion
            #region SPC_EPISODIO
            this.SPC_EPISODIO.CommandText = "dbo.[SPC_EPISODIO]";
            this.SPC_EPISODIO.CommandType = System.Data.CommandType.StoredProcedure;
            this.SPC_EPISODIO.Connection = this.cnn;
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Persona", System.Data.SqlDbType.Int, 4));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Programa", System.Data.SqlDbType.SmallInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_Episodio", System.Data.SqlDbType.DateTime, 8));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_SeguroSalud", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FuentePago", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FeminaHijos", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_VaronHijos", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FuenteIngreso", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TiempoResidencia", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Municipio", System.Data.SqlDbType.SmallInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Zona", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_ZipCode", System.Data.SqlDbType.VarChar, 5));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_EtapaServicio", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_NivelCuidadoSustancias", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_NivelCuidadoMental", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Metadona", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_CodDependiente", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FuenteReferido", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_EstadoLegal", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_ArrestadoAnteriormente", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Justicia", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_DiasEsperaSustancias", System.Data.SqlDbType.SmallInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_EpisodiosSustancias", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DuracionSustancias", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_DiasUltimaAltaSustancias", System.Data.SqlDbType.SmallInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_MesesUltimaAltaSustancias", System.Data.SqlDbType.SmallInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_NivelCuidadoSustanciasAnterior", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_DiasEsperaMental", System.Data.SqlDbType.SmallInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_EpisodiosMental", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DuracionMental", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_DiasUltimaAltaMental", System.Data.SqlDbType.SmallInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_MesesUltimaAltaMental", System.Data.SqlDbType.SmallInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_NivelCuidadoMentalAnterior", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_ViolenciaDomestica", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Maltrato", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_TI_Maltrato", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Suicida", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_IdeaSuicida", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_TratadoMental", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Ambulatorio", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_HospitalizadoMental", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_TI_Hospital", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_TratamientoResidencial", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ES_Episodio", System.Data.SqlDbType.TinyInt));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_Alta", System.Data.SqlDbType.DateTime, 8));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Episodio", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            this.SPC_EPISODIO.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_FechaConvenio", System.Data.SqlDbType.Date, 8));
            #endregion
            this.daLkpNivelCuidado.SelectCommand = this.sqlSelectCommand4;
            this.daLkpNivelCuidado.TableMappings.AddRange(
                new System.Data.Common.DataTableMapping[] {
                new System.Data.Common.DataTableMapping("Table", "SA_LKP_SALUD_MENTAL", 
                    new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PK_SaludMental", "PK_SaludMental"),
                        new System.Data.Common.DataColumnMapping("DE_SaludMental","DE_SaludMental")}), 
                new System.Data.Common.DataTableMapping("Table1","SA_LKP_ABUSO_SUSTANCIAS", 
                    new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PK_AbusoSustancias", "PK_AbusoSustancias"),
                        new System.Data.Common.DataColumnMapping("DE_AbusoSustancias", "DE_AbusoSustancias")})});
            this.sqlSelectCommand4.CommandText = "[SPR_LKP_NIVEL_CUIDADO]";
            this.sqlSelectCommand4.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand4.Connection = this.cnn;
            this.sqlSelectCommand4.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            this.sqlSelectCommand4.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Persona", System.Data.SqlDbType.Int, 4));
            this.sqlSelectCommand4.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Episodio", System.Data.SqlDbType.Int, 4));
            #region SPC_PERFIL
            this.SPC_PERFIL.CommandText = "dbo.[SPC_PERFIL]";
            this.SPC_PERFIL.CommandType = System.Data.CommandType.StoredProcedure;
            this.SPC_PERFIL.Connection = this.cnn;
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Episodio", System.Data.SqlDbType.Int, 4));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_Expediente", System.Data.SqlDbType.VarChar, 12));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_Perfil", System.Data.SqlDbType.DateTime, 8));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_Contacto", System.Data.SqlDbType.DateTime, 8));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_TI_Perfil", System.Data.SqlDbType.VarChar, 2));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_EstadoMarital", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_CondicionLaboral", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ActividadNoLaboral", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_Hijos", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Escolaridad", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TipoAdmision", System.Data.SqlDbType.Int, 2));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_EducEspecial", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_DesertorEscolar", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Familia", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_CategoriaCentroPrivado", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_Familiar", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Residencia", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_ParticReunGrupos", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FreqAutoAyuda", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Arrestado30dias", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_Arrestos30dias", System.Data.SqlDbType.TinyInt));

            //DISABLED DSM IV
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosClinicosPrimario", System.Data.SqlDbType.SmallInt));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosClinicosSecundario", System.Data.SqlDbType.SmallInt));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosClinicosTerciario", System.Data.SqlDbType.SmallInt));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosPersonalidadPrimario", System.Data.SqlDbType.SmallInt));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosPersonalidadSecundario", System.Data.SqlDbType.SmallInt));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosPersonalidadTerciario", System.Data.SqlDbType.SmallInt));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CO_CondicionesMedicasPrimario", System.Data.SqlDbType.VarChar, 50));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CO_CondicionesMedicasSecundario", System.Data.SqlDbType.VarChar, 50));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CO_CondicionesMedicasTerciario", System.Data.SqlDbType.VarChar, 50));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ProblemasPsicosocialesPrimario", System.Data.SqlDbType.TinyInt));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ProblemasPsicosocialesSecundario", System.Data.SqlDbType.TinyInt));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ProblemasPsicosocialesTerciario", System.Data.SqlDbType.TinyInt));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_EscalaGAF", System.Data.SqlDbType.Int, 4));

            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DisposicionFinalReferido", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DrogaPrimario", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ViaPrimario", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FrecuenciaPrimario", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_EdadInicioPrimario", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DrogaSecundario", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ViaSecundario", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FrecuenciaSecundario", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_EdadInicioSecundario", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DrogaTerciario", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ViaTerciario", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FrecuenciaTerciario", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_EdadInicioTerciario", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_PromedioVisitas", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, false, ((System.Byte)(18)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_SituacionEscolar", System.Data.SqlDbType.Int, 2));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Alta", System.Data.SqlDbType.TinyInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_CentroTraslado", System.Data.SqlDbType.SmallInt));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_Comentario", System.Data.SqlDbType.VarChar, 1500));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Sesion", System.Data.SqlDbType.UniqueIdentifier, 16));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Perfil", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            /* DSMV */
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_TrastornosClinicos1", System.Data.SqlDbType.SmallInt, 3));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_TrastornosClinicos2", System.Data.SqlDbType.SmallInt, 3));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_TrastornosClinicos3", System.Data.SqlDbType.SmallInt, 3));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_TrastornosPersonalidadRM1", System.Data.SqlDbType.SmallInt, 3));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_TrastornosPersonalidadRM2", System.Data.SqlDbType.SmallInt, 3));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_TrastornosPersonalidadRM3", System.Data.SqlDbType.SmallInt, 3));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_ProblemasPsicosocialesAmbientales1", System.Data.SqlDbType.TinyInt, 3));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_ProblemasPsicosocialesAmbientales2", System.Data.SqlDbType.TinyInt, 3));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_ProblemasPsicosocialesAmbientales3", System.Data.SqlDbType.TinyInt, 3));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_DSMV_FuncionamientoGlobal", System.Data.SqlDbType.VarChar, 5));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_DSMV_OtrasObservaciones", System.Data.SqlDbType.VarChar, 1500));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_DSMV_Comentarios", System.Data.SqlDbType.VarChar, 1500));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_DSMV_DiagnosticoDual", System.Data.SqlDbType.SmallInt, 1));


            #endregion
            #region SPU PERFIL
            this.SPU_PERFIL.CommandText = "dbo.[SPU_PERFIL]";
            this.SPU_PERFIL.CommandType = System.Data.CommandType.StoredProcedure;
            this.SPU_PERFIL.Connection = this.cnn;
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_NR_Perfil", System.Data.SqlDbType.Int, 4));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_Perfil", System.Data.SqlDbType.DateTime, 8));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_Contacto", System.Data.SqlDbType.DateTime, 8));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_TI_Perfil", System.Data.SqlDbType.VarChar, 2));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_EstadoMarital", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_CondicionLaboral", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ActividadNoLaboral", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TipoAdmision", System.Data.SqlDbType.Int, 2));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_Hijos", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Escolaridad", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_EducEspecial", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_DesertorEscolar", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Familia", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_Familiar", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Residencia", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_ParticReunGrupos", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FreqAutoAyuda", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Arrestado30dias", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_Arrestos30dias", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_CategoriaCentroPrivado", System.Data.SqlDbType.TinyInt));
            //DISABLED DSM IV
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosClinicosPrimario", System.Data.SqlDbType.SmallInt));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosClinicosSecundario", System.Data.SqlDbType.SmallInt));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosClinicosTerciario", System.Data.SqlDbType.SmallInt));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosPersonalidadPrimario", System.Data.SqlDbType.SmallInt));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosPersonalidadSecundario", System.Data.SqlDbType.SmallInt));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosPersonalidadTerciario", System.Data.SqlDbType.SmallInt));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CO_CondicionesMedicasPrimario", System.Data.SqlDbType.VarChar, 50));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CO_CondicionesMedicasSecundario", System.Data.SqlDbType.VarChar, 50));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CO_CondicionesMedicasTerciario", System.Data.SqlDbType.VarChar, 50));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ProblemasPsicosocialesPrimario", System.Data.SqlDbType.TinyInt));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ProblemasPsicosocialesSecundario", System.Data.SqlDbType.TinyInt));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ProblemasPsicosocialesTerciario", System.Data.SqlDbType.TinyInt));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_EscalaGAF", System.Data.SqlDbType.Int, 4));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_SituacionEscolar", System.Data.SqlDbType.Int, 2));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DisposicionFinalReferido", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DrogaPrimario", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ViaPrimario", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FrecuenciaPrimario", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_EdadInicioPrimario", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DrogaSecundario", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ViaSecundario", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FrecuenciaSecundario", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_EdadInicioSecundario", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DrogaTerciario", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ViaTerciario", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FrecuenciaTerciario", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_EdadInicioTerciario", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_PromedioVisitas", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, false, ((System.Byte)(18)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Alta", System.Data.SqlDbType.TinyInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_CentroTraslado", System.Data.SqlDbType.SmallInt));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_Comentario", System.Data.SqlDbType.VarChar, 1500));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Sesion", System.Data.SqlDbType.UniqueIdentifier, 16));
            /*DSMV*/
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_TrastornosClinicos1", System.Data.SqlDbType.SmallInt, 3));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_TrastornosClinicos2", System.Data.SqlDbType.SmallInt, 3));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_TrastornosClinicos3", System.Data.SqlDbType.SmallInt, 3));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_TrastornosPersonalidadRM1", System.Data.SqlDbType.SmallInt, 3));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_TrastornosPersonalidadRM2", System.Data.SqlDbType.SmallInt, 3));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_TrastornosPersonalidadRM3", System.Data.SqlDbType.SmallInt, 3));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_ProblemasPsicosocialesAmbientales1", System.Data.SqlDbType.TinyInt, 3));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_ProblemasPsicosocialesAmbientales2", System.Data.SqlDbType.TinyInt, 3));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_ProblemasPsicosocialesAmbientales3", System.Data.SqlDbType.TinyInt, 3));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_DSMV_FuncionamientoGlobal", System.Data.SqlDbType.VarChar, 5));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_DSMV_OtrasObservaciones", System.Data.SqlDbType.VarChar, 1500));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_DSMV_Comentarios", System.Data.SqlDbType.VarChar, 1500));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_DSMV_DiagnosticoDual", System.Data.SqlDbType.SmallInt, 1));
#endregion
            this.dsSeguridad.DataSetName = "dsSeguridad";
            this.dsSeguridad.Locale = new System.Globalization.CultureInfo("en-US");
            ((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSeguridad)).EndInit();
        }
        #endregion
        #region Botones
        protected void btnEliminarAdmin_Click(object sender, System.EventArgs e)
        {
            int PK_Episodio = Convert.ToInt32(this.dsPerfil.SA_EPISODIO[0]["PK_Episodio"].ToString());
            int PK_Perfil = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString());
            this.Response.Redirect("frmEliminarPerfil.aspx?PK_Episodio=" + PK_Episodio.ToString() + "&PK_Perfil=" + PK_Perfil.ToString() + "&TI_Perfil=AD&accion=L");
        }
        protected void btnEliminar_Click(object sender, System.EventArgs e)
        {
            int PK_Episodio = Convert.ToInt32(this.dsPerfil.SA_EPISODIO[0]["PK_Episodio"].ToString());
            int PK_Perfil = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString());
            this.Response.Redirect("frmEliminarPerfil.aspx?PK_Episodio=" + PK_Episodio.ToString() + "&PK_Perfil=" + PK_Perfil.ToString() + "&TI_Perfil=AD&accion=F");
        }
        protected void btnModificarAdmin_Click(object sender, System.EventArgs e)
        {
            int PK_Episodio = Convert.ToInt32(this.dsPerfil.SA_EPISODIO[0]["PK_Episodio"].ToString());
            this.Response.Redirect("frmAdmision.aspx?accion=update&pk_episodio=" + PK_Episodio);
        }
        protected void btnModificar_Click(object sender, System.EventArgs e)
        {


            //Page.Validate();
            //if (!Page.IsValid)
            //{
            //    return;
            //}


            int PK_Episodio = Convert.ToInt32(this.dsPerfil.SA_EPISODIO[0]["PK_Episodio"].ToString());
            this.Response.Redirect("frmAdmision.aspx?accion=update&pk_episodio=" + PK_Episodio);
        }
        protected void btnGuardarCambios_Click(object sender, System.EventArgs e)
        {
             this.GuardarCambios();
            int PK_Episodio = Convert.ToInt32(this.dsPerfil.SA_EPISODIO[0]["PK_Episodio"].ToString());
            this.Response.Redirect("frmAdmision.aspx?accion=read&pk_episodio=" + PK_Episodio);
        }
        protected void btnRegistrar_Click(object sender, System.EventArgs e)
        {

            if (WucEpisodioAdmision.NivelCuidadoSustancias.SelectedValue != "99" && WucEpisodioAdmision.NivelCuidadoSustancias.SelectedValue != "")
            {
                Page.Validate();
                if (!Page.IsValid)
                {
                    return;
                }
            }

            this.WucDatosPersonales.lblFechaError.Text = "";
            // se valida fecha si la fecha es valida
            if (ValidarFecha(this.WucDatosPersonales.ddlDía.SelectedValue.ToString(),
                             this.WucDatosPersonales.ddlMes.SelectedValue.ToString(),
                             this.WucDatosPersonales.txtAño.Text)==false)
            {
                this.WucDatosPersonales.lblFechaError.Text = "La fecha es  inválida.";
                this.WucDatosPersonales.lblFechaError.ForeColor = Color.Red;
                return;
            }

            // se valida si la fecha en mayor a la fecha actual
            try
            {
                DateTime TempDate = new DateTime(Convert.ToInt32(WucDatosPersonales.txtAño.Text),
                                                 Convert.ToInt32(WucDatosPersonales.ddlMes.SelectedValue), 
                                                 Convert.ToInt32(WucDatosPersonales.ddlDía.SelectedValue));
                if (TempDate > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
                {
                    this.WucDatosPersonales.lblFechaError.Text = "La fecha no puede ser mayor a la fecha actual.";
                    this.WucDatosPersonales.lblFechaError.ForeColor = Color.Red;
                    return;

                }
                else
                {
                    this.WucDatosPersonales.lblFechaError.Text = "";
                }

            }
            catch ( Exception  )
            {
                return;
            }


            int PK_Episodio = this.GuardarNuevo();
            this.Response.Redirect("frmAdmision.aspx?accion=read&pk_episodio=" + PK_Episodio);
        }


        private bool ValidarFecha(string dia, string mes, string año)
        {
            bool resultado = false;

            try
            {
                DateTime TempDate = new DateTime(Convert.ToInt32(año), Convert.ToInt32(mes), Convert.ToInt32(dia));
                resultado = true;
            }
            catch (Exception)
            {
                
            }

            return resultado;
        }

        #endregion
    }
}
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
using SEPS.Constante;

namespace ASSMCA.Perfiles
{
	public partial class frmEvaluacion : System.Web.UI.Page
	{
		protected ASSMCA.perfiles.dsPerfil dsPerfil;
        protected ASSMCA.dsSeguridad dsSeguridad;
        protected System.Data.SqlClient.SqlConnection cnn;
		protected System.Data.SqlClient.SqlCommand  SPC_PERFIL, SPU_PERFIL,
            sqlSelectCommand1, sqlSelectCommand2, sqlSelectCommand3, sqlSelectCommand4, SPC_NewData, SPU_NewData, SPC_METADONA, SPU_METADONA, SPD_Ref_RazonTH, SPU_Ref_RazonTH, SPD_Ref_PracticasBasadasEnEvidencia, SPU_Ref_PracticasBasadasEnEvidencia,
            SPD_Ref_CondicionesDiagnosticadas, SPU_Ref_CondicionesDiagnosticadas,SPD_Ref_CompFamilia, SPU_Ref_CompFamilia;
        protected System.Data.SqlClient.SqlDataAdapter daPerfil, daPerfilValidaciones, daAdmision, daLkpPerfil;
        private int m_PK_Episodio, PK_Perfil, _m_PK_Perfil, m_PK_Persona, m_PK_Programa, m_CO_Tipo;
        private static string nivelSM, nivelAS;
        private const string SCRIPT_DOFOCUS =
    @"window.setTimeout('DoFocus()', 1);
    function DoFocus()
    {
        try {
            document.getElementById('REQUEST_LASTFOCUS').focus();
        } catch (ex) {}
    }";

        protected void Page_Load(object sender, System.EventArgs e)
		{
            this.Session["Tipo_Perfil"] = "Evaluacion";
			if ( this.Session["dsSeguridad"] == null )
			{
				this.Response.Redirect("~/Error.aspx?errMsg=sesion");
				return;
			}

            if (this.Session["dsPerfil"] != null)
            {
                this.dsPerfil = (ASSMCA.perfiles.dsPerfil)this.Session["dsPerfil"];
            }
            this.m_PK_Programa = Convert.ToInt32(this.Session["pk_programa"].ToString());
            this.m_CO_Tipo = Convert.ToInt32(this.Session["co_tipo"].ToString());

            string Accion = this.Request.QueryString["accion"].ToString();
            this.dsSeguridad = (dsSeguridad)this.Session["dsSeguridad"];
            int nr_rowIndex_dsSeguridad = Convert.ToInt32(this.Session["nr_rowIndex_dsSeguridad"].ToString());

            
            if ( !this.IsPostBack )
			{
				//this.WucEpisodioPerfil.FindControl("NivelDiv").Visible = false;
				

				HookOnFocus(this.Page as Control);
                this.WucTakeHome.PK_Programa = m_PK_Programa;
                switch (Accion)
                {
                    case ("create"):
                        this.m_PK_Episodio = Convert.ToInt32(this.Request.QueryString["PK_Episodio"].ToString());
					    this.CrearRegistro();
					    this.LeerRegistroAdmision(this.m_PK_Episodio);
					    this.Session["dsPerfil"] = this.dsPerfil;
					    this.btnEliminar.Visible = false;
					    this.btnEliminarAdmin.Visible = false;
					    this.btnModificar.Visible = false;
					    this.btnModificarAdmin.Visible = false;
					    this.btnRegistrar.Visible = true;
					    this.btnGuardarCambios.Visible = false;
						break;
                    case ("update"):
						_m_PK_Perfil = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString());
						this.WucDatosPersonales.PK_Perfil = _m_PK_Perfil;
						this.WucDatosPersonales.PK_Persona =
						this.m_PK_Persona = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_Persona"].ToString());
						this.WucDatosDemograficosPerfil.PK_Perfil = _m_PK_Perfil;
						this.WucTakeHome.PK_Perfil = _m_PK_Perfil;
						this.ModificarRegistro();
						this.btnEliminar.Visible = false;
						this.btnEliminarAdmin.Visible = false;
						this.btnModificar.Visible = false;
						this.btnModificarAdmin.Visible = false;
						this.btnRegistrar.Visible = false;
						this.btnGuardarCambios.Visible = true;
                        break;
                    case ("read"):
                        this.LeerRegistro();
					    this.btnRegistrar.Visible = false;
					    this.btnGuardarCambios.Visible = false;
                        break;
                    default: break;
                }

				string nivelRecuperacion = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoMental"].ToString();

				if (nivelRecuperacion == "24" || nivelRecuperacion == "25" || nivelRecuperacion == "26" || nivelRecuperacion == "33")
				{
					this.WucEpisodioPerfil.FindControl("RecuperacionDiv").Visible = true;
				}

			}
            else
            {
                if (Request.Form["__EVENTTARGET"] != null &&
                    Request.Form["__EVENTTARGET"] != string.Empty && 
                    Request.Form["__EVENTTARGET"] != "ctl00$changeProgram")

                {
                    this.WucDatosDemograficosPerfil.edadPerfilF(this.WucOtrosDatosPerfil.FE_Perfil, this.WucDatosPersonales.FE_Nacimiento.ToString());
                }

                Page.ClientScript.RegisterStartupScript(
                typeof(wucDatosPersonales),
                "ScriptDoFocus",
                SCRIPT_DOFOCUS.Replace("REQUEST_LASTFOCUS",
                                       Request["__LASTFOCUS"]),
                true);
            }

            if (nivelAS != "99")
            {
                this.lblTipoPerfil.Text = " : Abuso de Sustancia";
            }
            else if(nivelSM != "99")
            {
                this.lblTipoPerfil.Text = " : Salud Mental";
            }

            if (this.dsSeguridad.SA_USUARIO[nr_rowIndex_dsSeguridad].IN_D_PCORTA < 1) //Si esta denegado
            {
                this.btnEliminar.Visible = false;
            }
            if (this.dsSeguridad.SA_USUARIO[nr_rowIndex_dsSeguridad].IN_D_PERFIL < 1) //Si esta denegado
            {
                this.btnEliminarAdmin.Visible = false;
            }
            if (this.dsSeguridad.SA_USUARIO[nr_rowIndex_dsSeguridad].IN_U_PCORTA < 1) //Si esta denegado
            {
                this.btnModificar.Visible = false;
            }
            if (this.dsSeguridad.SA_USUARIO[nr_rowIndex_dsSeguridad].IN_U_PERFIL < 1) //Si esta denegado
            {
                this.btnModificarAdmin.Visible = false;
            }

			//Se determina el tipo de eliminacion o modificacion disponible para el registro,
			//dadas las condiciones del mismo.

			//El valor NULL en el campo ES_PERFIL indica que el registro puede modificarse
			//de alguna manera, la cual debe verificarse.
			if(this.dsPerfil.SA_PERFIL.Rows.Count  == 1)
			{
				if(this.dsPerfil.SA_PERFIL.DefaultView[0]["ES_Perfil"].ToString() == "1")
				{
					this.btnModificar.Visible = false;
					this.btnEliminar.Visible = false;
				}
			
				DateTime FE_Edicion = Convert.ToDateTime(this.dsPerfil.SA_PERFIL.DefaultView[0]["FE_Perfil"].ToString());
				TimeSpan ts = DateTime.Now.Subtract(FE_Edicion);

                if (ts.Days > NewSource.nr_dias_edicion_registros)
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
		}

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

        private void LeerRegistroAdmision(int PK_Episodio)
		{
			this.dsPerfil.SA_EPISODIO.Rows.Clear();
			this.dsPerfil.SA_PERFIL.Rows.Clear();
			this.daAdmision.SelectCommand.Parameters["@PK_Admision"].Value = PK_Episodio;
            this.daAdmision.Fill(this.dsPerfil);
            nivelSM = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoMental"].ToString();
            nivelAS = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoSustancias"].ToString();
        }

		private void LeerPerfil(int PK_Perfil)
		{
			this.dsPerfil.SA_EPISODIO.Rows.Clear();
			this.dsPerfil.SA_PERFIL.Rows.Clear();
			this.daPerfil.SelectCommand.Parameters["@PK_Perfil"].Value = PK_Perfil;
            this.daPerfil.Fill(this.dsPerfil);
            Session["FechaAdmision"] = this.dsPerfil.SA_EPISODIO[0]["FE_Episodio"].ToString();
            nivelSM = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoMental"].ToString();
            nivelAS = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoSustancias"].ToString();
        }

		private void LeerRegistro()
		{
			WucDatosPersonales.m_frmAction = frmAction.Read;
			WucOtrosDatosPerfil.m_frmAction = frmAction.Read;
			WucDatosDemograficosPerfil.m_frmAction = frmAction.Read;
			WucEpisodioPerfil.m_frmAction = frmAction.Read;
			WucDatosEvaluacion.m_frmAction = frmAction.Read;
            WucTakeHome.m_frmAction = frmAction.Read;
			this.PK_Perfil = Convert.ToInt32(Request.QueryString["pk_perfil"].ToString());
            WucDatosDemograficosPerfil.PK_Perfil = PK_Perfil;
            WucTakeHome.PK_Perfil = PK_Perfil;
            WucDatosPersonales.PK_Perfil = PK_Perfil;
            WucDatosPersonales.setValues();
            dsPerfil.Tables["SA_Episodio"].Columns["IN_TratamientoResidencial"].AllowDBNull = true;
            dsPerfil.Tables["SA_Episodio"].Columns["IN_TI_Hospital"].AllowDBNull = true;
            dsPerfil.Tables["SA_Episodio"].Columns["IN_HospitalizadoMental"].AllowDBNull = true;
            dsPerfil.Tables["SA_Episodio"].Columns["IN_TratadoMental"].AllowDBNull = true;
            dsPerfil.Tables["SA_Episodio"].Columns["IN_Ambulatorio"].AllowDBNull = true;
			this.LeerPerfil(PK_Perfil);
			WucDatosPersonales.PK_Persona = Convert.ToInt32(this.dsPerfil.SA_EPISODIO[0]["FK_Persona"].ToString());
			WucDatosPersonales.PK_Programa = this.m_PK_Programa;
			this.Session["dsPerfil"] = this.dsPerfil;
			this.btnRegistrar.Visible = false;

            nivelSM = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoMental"].ToString();
            nivelAS = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_NivelCuidadoSustancias"].ToString();
        }

		private void ModificarRegistro()
		{
			WucDatosPersonales.m_frmAction = frmAction.Update;
			WucOtrosDatosPerfil.m_frmAction = frmAction.Update;
			WucDatosDemograficosPerfil.m_frmAction = frmAction.Update;
			WucEpisodioPerfil.m_frmAction = frmAction.Update;
			WucDatosEvaluacion.m_frmAction = frmAction.Update;
            WucTakeHome.m_frmAction = frmAction.Update;
            this.PK_Perfil = int.Parse(this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString());

            this.m_PK_Persona = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_Persona"].ToString());
            WucDatosPersonales.PK_Persona = this.m_PK_Persona;
			WucDatosPersonales.PK_Programa = this.m_PK_Programa;
            WucTakeHome.PK_Perfil = PK_Perfil;
			this.Session["dsPerfil"] = this.dsPerfil;
			this.btnRegistrar.Visible = false;
		}

		private void CrearRegistro()
		{
			WucDatosPersonales.m_frmAction = frmAction.Create;
			WucOtrosDatosPerfil.m_frmAction = frmAction.Create;
			WucDatosDemograficosPerfil.m_frmAction = frmAction.Create;
			WucEpisodioPerfil.m_frmAction = frmAction.Create;
			WucDatosEvaluacion.m_frmAction = frmAction.Create;
            WucTakeHome.m_frmAction = frmAction.Create;
			this.m_PK_Persona = Convert.ToInt32(this.Request.QueryString["PK_Persona"].ToString());
			WucDatosPersonales.PK_Persona = this.m_PK_Persona;
			WucDatosPersonales.PK_Programa = this.m_PK_Programa;
			this.btnRegistrar.Visible = true;
		}

		private int GuardarNuevo()
		{
            this._m_PK_Perfil = 0;

			this.m_PK_Persona = Convert.ToInt32(this.Request.QueryString["pk_persona"].ToString());			
			this.m_PK_Episodio = Convert.ToInt32(this.Request.QueryString["PK_Episodio"].ToString());

			this.SPC_PERFIL.Parameters["@FK_Episodio"].Value = this.m_PK_Episodio;

			this.SPC_PERFIL.Parameters["@FE_Perfil"].Value = this.WucOtrosDatosPerfil.FE_Perfil;
            if (this.WucOtrosDatosPerfil.FE_Contacto != null)
            {
                this.SPC_PERFIL.Parameters["@FE_Contacto"].Value = this.WucOtrosDatosPerfil.FE_Contacto;
            }
			this.SPC_PERFIL.Parameters["@IN_TI_Perfil"].Value = "EV";

            this.SPC_PERFIL.Parameters["@FK_CategoriaCentroPrivado"].Value = System.DBNull.Value;
            this.SPC_PERFIL.Parameters["@FK_TipoAdmision"].Value = System.DBNull.Value;
            this.SPC_PERFIL.Parameters["@FK_SituacionEscolar"].Value = this.WucDatosDemograficosPerfil.FK_SituacionEscolar.ToString();

			this.SPC_PERFIL.Parameters["@FK_EstadoMarital"].Value = this.WucDatosDemograficosPerfil.FK_EstadoMarital;
			this.SPC_PERFIL.Parameters["@FK_CondicionLaboral"].Value = this.WucDatosDemograficosPerfil.FK_CondicionLaboral;
			this.SPC_PERFIL.Parameters["@FK_ActividadNoLaboral"].Value = this.WucDatosDemograficosPerfil.FK_ActividadNoLaboral;
			this.SPC_PERFIL.Parameters["@NR_Hijos"].Value = this.WucDatosDemograficosPerfil.NR_Hijos;
			this.SPC_PERFIL.Parameters["@FK_Escolaridad"].Value = this.WucDatosDemograficosPerfil.FK_Escolaridad;


			this.SPC_PERFIL.Parameters["@IN_EducEspecial"].Value = this.WucDatosDemograficosPerfil.IN_EducEspecial;
			this.SPC_PERFIL.Parameters["@IN_DesertorEscolar"].Value = this.WucDatosDemograficosPerfil.IN_DesertorEscolar;
       
            this.SPC_PERFIL.Parameters["@FK_Familia"].Value = 96; //this.WucDatosDemograficosPerfil.FK_Familia;
			this.SPC_PERFIL.Parameters["@NR_Familiar"].Value = this.WucDatosDemograficosPerfil.NR_Familiar;
			this.SPC_PERFIL.Parameters["@FK_Residencia"].Value = this.WucDatosDemograficosPerfil.FK_Residencia;
            this.SPC_PERFIL.Parameters["@FK_FreqAutoAyuda"].Value = this.WucDatosDemograficosPerfil.FK_FreqAutoAyuda;
			this.SPC_PERFIL.Parameters["@IN_Arrestado30dias"].Value = this.WucDatosDemograficosPerfil.IN_Arrestado30dias;
			this.SPC_PERFIL.Parameters["@NR_Arrestos30dias"].Value = this.WucDatosDemograficosPerfil.NR_Arrestos30dias;
            #region DSMV
            this.SPC_PERFIL.Parameters["@FK_DSMV_TrastornosClinicos1"].Value = this.WucEpisodioPerfil.FK_DSMV_TrastornosClinicos1;
            this.SPC_PERFIL.Parameters["@FK_DSMV_TrastornosClinicos2"].Value = this.WucEpisodioPerfil.FK_DSMV_TrastornosClinicos2;
            this.SPC_PERFIL.Parameters["@FK_DSMV_TrastornosClinicos3"].Value = this.WucEpisodioPerfil.FK_DSMV_TrastornosClinicos3;
            this.SPC_PERFIL.Parameters["@FK_DSMV_TrastornosPersonalidadRM1"].Value = this.WucEpisodioPerfil.FK_DSMV_TrastornosPersonalidadRM1;
            this.SPC_PERFIL.Parameters["@FK_DSMV_TrastornosPersonalidadRM2"].Value = this.WucEpisodioPerfil.FK_DSMV_TrastornosPersonalidadRM2;
            this.SPC_PERFIL.Parameters["@FK_DSMV_TrastornosPersonalidadRM3"].Value = this.WucEpisodioPerfil.FK_DSMV_TrastornosPersonalidadRM3;
            this.SPC_PERFIL.Parameters["@FK_DSMV_ProblemasPsicosocialesAmbientales1"].Value = this.WucEpisodioPerfil.FK_DSMV_ProblemasPsicosocialesAmbientales1;
            this.SPC_PERFIL.Parameters["@FK_DSMV_ProblemasPsicosocialesAmbientales2"].Value = this.WucEpisodioPerfil.FK_DSMV_ProblemasPsicosocialesAmbientales2;
            this.SPC_PERFIL.Parameters["@FK_DSMV_ProblemasPsicosocialesAmbientales3"].Value = this.WucEpisodioPerfil.FK_DSMV_ProblemasPsicosocialesAmbientales3;
            this.SPC_PERFIL.Parameters["@NR_DSMV_FuncionamientoGlobal"].Value = this.WucEpisodioPerfil.NR_DSMV_FuncionamientoGlobal;
            this.SPC_PERFIL.Parameters["@DE_DSMV_OtrasObservaciones"].Value = this.WucEpisodioPerfil.DE_DSMV_Comentarios;
            this.SPC_PERFIL.Parameters["@DE_DSMV_Comentarios"].Value = this.WucEpisodioPerfil.DE_DSMV_OtrasObservaciones;
            this.SPC_PERFIL.Parameters["@IN_DSMV_DiagnosticoDual"].Value = this.WucEpisodioPerfil.IN_DSMV_DiagnosticoDual;



			/*DSMV Sustancias*/
			this.SPC_PERFIL.Parameters["@FK_DSMV_Sustancias1"].Value = this.WucEpisodioPerfil.FK_DSMV_Sustancias1;
			this.SPC_PERFIL.Parameters["@FK_DSMV_Sustancias2"].Value = this.WucEpisodioPerfil.FK_DSMV_Sustancias2;
			this.SPC_PERFIL.Parameters["@FK_DSMV_Sustancias3"].Value = this.WucEpisodioPerfil.FK_DSMV_Sustancias3;

			#endregion
			this.SPC_PERFIL.Parameters["@FK_DisposicionFinalReferido"].Value = 1;
			this.SPC_PERFIL.Parameters["@FK_DrogaPrimario"].Value = this.WucEpisodioPerfil.FK_DrogaPrimario;
			this.SPC_PERFIL.Parameters["@FK_ViaPrimario"].Value = this.WucEpisodioPerfil.FK_ViaPrimario;
			this.SPC_PERFIL.Parameters["@FK_FrecuenciaPrimario"].Value = this.WucEpisodioPerfil.FK_FrecuenciaPrimario;
			this.SPC_PERFIL.Parameters["@IN_EdadInicioPrimario"].Value = this.WucEpisodioPerfil.IN_EdadInicioPrimario;
			this.SPC_PERFIL.Parameters["@FK_DrogaSecundario"].Value = this.WucEpisodioPerfil.FK_DrogaSecundario;
			this.SPC_PERFIL.Parameters["@FK_ViaSecundario"].Value = this.WucEpisodioPerfil.FK_ViaSecundario;
			this.SPC_PERFIL.Parameters["@FK_FrecuenciaSecundario"].Value = this.WucEpisodioPerfil.FK_FrecuenciaSecundario;
			this.SPC_PERFIL.Parameters["@IN_EdadInicioSecundario"].Value = this.WucEpisodioPerfil.IN_EdadInicioSecundario;
			this.SPC_PERFIL.Parameters["@FK_DrogaTerciario"].Value = this.WucEpisodioPerfil.FK_DrogaTerciario;
			this.SPC_PERFIL.Parameters["@FK_ViaTerciario"].Value = this.WucEpisodioPerfil.FK_ViaTerciario;
			this.SPC_PERFIL.Parameters["@FK_FrecuenciaTerciario"].Value = this.WucEpisodioPerfil.FK_FrecuenciaTerciario;
			this.SPC_PERFIL.Parameters["@IN_EdadInicioTerciario"].Value = this.WucEpisodioPerfil.IN_EdadInicioTerciario;
			this.SPC_PERFIL.Parameters["@DE_Comentario"].Value = this.WucDatosEvaluacion.DE_Comentario;

			this.SPC_PERFIL.Parameters["@NR_CelularPrimario"].Value = this.WucDatosPersonales.NR_CelularPrimario;
			this.SPC_PERFIL.Parameters["@NR_CelularContacto"].Value = this.WucDatosPersonales.NR_CelularContacto;
			this.SPC_PERFIL.Parameters["@DE_EmailPrimario"].Value = this.WucDatosPersonales.DE_EmailPrimario;
			this.SPC_PERFIL.Parameters["@DE_EmailSecundario"].Value = this.WucDatosPersonales.DE_EmailSecundario;
			            this.SPC_PERFIL.Parameters["@FK_IDENTIDAD_GENERO"].Value = this.WucDatosPersonales.FK_IDENTIDAD_GENERO;

			this.SPC_PERFIL.Parameters["@FK_CatRecuperacionRes"].Value = this.WucEpisodioPerfil.FK_CatRecuperacionRes;
			this.SPC_PERFIL.Parameters["@HogarRecuperacionRes"].Value = this.WucEpisodioPerfil.HogarRecuperacionRes;
			this.SPC_PERFIL.Parameters["@IN_Fumado"].Value = this.WucEpisodioPerfil.IN_Fumado;
			this.SPC_PERFIL.Parameters["@DE_FrecuenciaFumado"].Value = this.WucEpisodioPerfil.DE_FrecuenciaFumado;
			this.SPC_PERFIL.Parameters["@NR_CigarrosXDias"].Value = this.WucEpisodioPerfil.NR_CigarrosXDias;
			this.SPC_PERFIL.Parameters["@DE_DrogaNueva1"].Value = this.WucEpisodioPerfil.DE_DrogaNueva1;
			this.SPC_PERFIL.Parameters["@DE_DrogaNueva2"].Value = this.WucEpisodioPerfil.DE_DrogaNueva2;
			this.SPC_PERFIL.Parameters["@DE_DrogaNueva3"].Value = this.WucEpisodioPerfil.DE_DrogaNueva3;
			this.SPC_PERFIL.Parameters["@IN_Toxicologia1"].Value = this.WucEpisodioPerfil.IN_Toxicologia1;
			this.SPC_PERFIL.Parameters["@IN_Toxicologia2"].Value = this.WucEpisodioPerfil.IN_Toxicologia2;
			this.SPC_PERFIL.Parameters["@IN_Toxicologia3"].Value = this.WucEpisodioPerfil.IN_Toxicologia3;

			Guid PK_Sesion = new Guid(this.Session["pk_sesion"].ToString());
            this.SPC_PERFIL.Parameters["@FK_Sesion"].Value = PK_Sesion;

            //Added New Data
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
                this.SPC_PERFIL.ExecuteNonQuery();
                this._m_PK_Perfil = Convert.ToInt32(this.SPC_PERFIL.Parameters["@PK_Perfil"].Value);
                if (this.WucEpisodioPerfil.EsProgramaMental(m_PK_Programa))
                {
                    this.SPD_Ref_PracticasBasadasEnEvidencia = new System.Data.SqlClient.SqlCommand();
                    this.SPD_Ref_PracticasBasadasEnEvidencia.CommandText = "[SPD_Ref_PracticasBasadasEnEvidencia]";
                    this.SPD_Ref_PracticasBasadasEnEvidencia.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPD_Ref_PracticasBasadasEnEvidencia.Connection = this.cnn;
                    this.SPD_Ref_PracticasBasadasEnEvidencia.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                    this.SPD_Ref_PracticasBasadasEnEvidencia.ExecuteNonQuery();


                    for (int i = 0; i < this.WucEpisodioPerfil.PracticasBasadasEvidenciaCount; i++)
                    {
                        this.SPU_Ref_PracticasBasadasEnEvidencia = new System.Data.SqlClient.SqlCommand();
                        this.SPU_Ref_PracticasBasadasEnEvidencia.CommandText = "[SPU_Ref_PracticasBasadasEnEvidencia]";
                        this.SPU_Ref_PracticasBasadasEnEvidencia.CommandType = System.Data.CommandType.StoredProcedure;
                        this.SPU_Ref_PracticasBasadasEnEvidencia.Connection = this.cnn;
                        this.SPU_Ref_PracticasBasadasEnEvidencia.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                        this.SPU_Ref_PracticasBasadasEnEvidencia.Parameters.AddWithValue("@FK_Practica", this.WucEpisodioPerfil.PracticasBasadasEvidenciaItem(i));
                        this.SPU_Ref_PracticasBasadasEnEvidencia.ExecuteNonQuery();
                        this.SPU_Ref_PracticasBasadasEnEvidencia.Dispose();
                    }
                }

                if (Const.EsProgramaMetadona(m_PK_Programa))
                {
                    this.SPC_METADONA = new System.Data.SqlClient.SqlCommand();
                    this.SPC_METADONA.CommandText = "[SPC_METADONA]";
                    this.SPC_METADONA.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPC_METADONA.Connection = this.cnn;
                    this.SPC_METADONA.Parameters.AddWithValue("@FK_Participa", WucTakeHome.THBelong.ToString());
                    if (WucTakeHome.THBelong != null && WucTakeHome.THBelong == 1/*Sí*/)
                    {
                        this.SPC_METADONA.Parameters.AddWithValue("@FK_Etapa", this.WucTakeHome.EtapaTH.ToString());
                        this.SPC_METADONA.Parameters.AddWithValue("@FE_Entrada", this.WucTakeHome.FE_THIni.ToString());
                        this.SPC_METADONA.Parameters.AddWithValue("@FE_Salida", this.WucTakeHome.FE_THFin.ToString());
                        this.SPC_METADONA.Parameters.AddWithValue("@NR_Botellas", this.WucTakeHome.NR_CantidadBotellas);
                        this.SPC_METADONA.Parameters.AddWithValue("@FK_FrecuenciaBotellas", this.WucTakeHome.FK_FrecuenciaBotellas);
                    }
                    this.SPC_METADONA.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                    this.SPC_METADONA.ExecuteNonQuery();
                    //SPD_Ref_RazonTH
                    this.SPD_Ref_RazonTH = new System.Data.SqlClient.SqlCommand();
                    this.SPD_Ref_RazonTH.CommandText = "[SPD_Ref_RazonTH]";
                    this.SPD_Ref_RazonTH.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPD_Ref_RazonTH.Connection = this.cnn;
                    this.SPD_Ref_RazonTH.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                    this.SPD_Ref_RazonTH.ExecuteNonQuery();


                    for (int i = 0; i < this.WucTakeHome.RazonTHCount; i++)
                    {
                        //SPU_Ref_RazonTH
                        this.SPU_Ref_RazonTH = new System.Data.SqlClient.SqlCommand();
                        this.SPU_Ref_RazonTH.CommandText = "[SPU_Ref_RazonTH]";
                        this.SPU_Ref_RazonTH.CommandType = System.Data.CommandType.StoredProcedure;
                        this.SPU_Ref_RazonTH.Connection = this.cnn;
                        this.SPU_Ref_RazonTH.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                        this.SPU_Ref_RazonTH.Parameters.AddWithValue("@FK_Razon", this.WucTakeHome.RazonTHItem(i));
                        this.SPU_Ref_RazonTH.ExecuteNonQuery();
                        this.SPU_Ref_RazonTH.Dispose();
                    }
                }

                //Added New
                this.SPC_NewData.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                this.SPC_NewData.ExecuteNonQuery();

                this.SPD_Ref_CondicionesDiagnosticadas = new System.Data.SqlClient.SqlCommand();
                this.SPD_Ref_CondicionesDiagnosticadas.CommandText = "[SPD_Ref_CondicionesDiagnosticadas]";
                this.SPD_Ref_CondicionesDiagnosticadas.CommandType = System.Data.CommandType.StoredProcedure;
                this.SPD_Ref_CondicionesDiagnosticadas.Connection = this.cnn;
                this.SPD_Ref_CondicionesDiagnosticadas.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                this.SPD_Ref_CondicionesDiagnosticadas.ExecuteNonQuery();


                for (int i = 0; i < this.WucEpisodioPerfil.CondicionesDiagnosticadasCount; i++)
                {
                    this.SPU_Ref_CondicionesDiagnosticadas = new System.Data.SqlClient.SqlCommand();
                    this.SPU_Ref_CondicionesDiagnosticadas.CommandText = "[SPU_Ref_CondicionesDiagnosticadas]";
                    this.SPU_Ref_CondicionesDiagnosticadas.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPU_Ref_CondicionesDiagnosticadas.Connection = this.cnn;
                    this.SPU_Ref_CondicionesDiagnosticadas.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                    this.SPU_Ref_CondicionesDiagnosticadas.Parameters.AddWithValue("@FK_Diagnostico", this.WucEpisodioPerfil.CondicionesDiagnosticadasItem(i));
                    this.SPU_Ref_CondicionesDiagnosticadas.ExecuteNonQuery();
                    this.SPU_Ref_CondicionesDiagnosticadas.Dispose();
                }

                    //SPD_Ref_CompFamilia
                    this.SPD_Ref_CompFamilia = new System.Data.SqlClient.SqlCommand();
                    this.SPD_Ref_CompFamilia.CommandText = "[SPD_Ref_CompFamilia]";
                    this.SPD_Ref_CompFamilia.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPD_Ref_CompFamilia.Connection = this.cnn;
                    this.SPD_Ref_CompFamilia.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                    this.SPD_Ref_CompFamilia.ExecuteNonQuery();


                    for (int i = 0; i < this.WucDatosDemograficosPerfil.CompFamCount; i++)
                    {
                        //SPU_Ref_CompFamilia
                        this.SPU_Ref_CompFamilia = new System.Data.SqlClient.SqlCommand();
                        this.SPU_Ref_CompFamilia.CommandText = "[SPU_Ref_CompFamilia]";
                        this.SPU_Ref_CompFamilia.CommandType = System.Data.CommandType.StoredProcedure;
                        this.SPU_Ref_CompFamilia.Connection = this.cnn;
                        this.SPU_Ref_CompFamilia.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                        this.SPU_Ref_CompFamilia.Parameters.AddWithValue("@FK_CompFamilia", this.WucDatosDemograficosPerfil.CompFamItem(i));
                        this.SPU_Ref_CompFamilia.ExecuteNonQuery();
                        this.SPU_Ref_CompFamilia.Dispose();
                    }
				this.cnn.Close();
			}
			catch(Exception ex)
			{
				string m = ex.Message;
				Trace.Warn("GuardarNuevo()", m, ex);

                if (this.cnn.State != ConnectionState.Closed)
                {
                    this.cnn.Close();
                }
                throw ex;
			}
            return this._m_PK_Perfil;
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


        private void GuardarCambios()
		{

            this.WucDatosPersonales.lblFechaError.Text = "";
            // se valida fecha si la fecha es valida
            if (ValidarFecha(this.WucDatosPersonales.ddlDía.SelectedValue.ToString(),
                             this.WucDatosPersonales.ddlMes.SelectedValue.ToString(),
                             this.WucDatosPersonales.txtAño.Text) == false)
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
            catch (Exception)
            {
                return;
            }



            this.SPU_PERFIL.Parameters["@PK_NR_Perfil"].Value = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString());
			this.SPU_PERFIL.Parameters["@FE_Perfil"].Value = this.WucOtrosDatosPerfil.FE_Perfil;
            if (this.WucOtrosDatosPerfil.FE_Contacto != null)
            {
                this.SPU_PERFIL.Parameters["@FE_Contacto"].Value = this.WucOtrosDatosPerfil.FE_Contacto;
            }
            this.SPU_PERFIL.Parameters["@FK_TipoAdmision"].Value = System.DBNull.Value;
            this.SPU_PERFIL.Parameters["@FK_CategoriaCentroPrivado"].Value = System.DBNull.Value;
            /*
            if (this.WucDatosPersonales.NR_Expediente != "0")
            {
                this.SPU_PERFIL.Parameters["@NR_Expediente"].Value = this.WucDatosPersonales.NR_Expediente;
            }*/
			this.SPU_PERFIL.Parameters["@IN_TI_Perfil"].Value = "EV";

            this.SPU_PERFIL.Parameters["@FK_SituacionEscolar"].Value = this.WucDatosDemograficosPerfil.FK_SituacionEscolar.ToString();

			this.SPU_PERFIL.Parameters["@FK_EstadoMarital"].Value = this.WucDatosDemograficosPerfil.FK_EstadoMarital;
			this.SPU_PERFIL.Parameters["@FK_CondicionLaboral"].Value = this.WucDatosDemograficosPerfil.FK_CondicionLaboral;
			this.SPU_PERFIL.Parameters["@FK_ActividadNoLaboral"].Value = this.WucDatosDemograficosPerfil.FK_ActividadNoLaboral;
			this.SPU_PERFIL.Parameters["@NR_Hijos"].Value = this.WucDatosDemograficosPerfil.NR_Hijos;
			this.SPU_PERFIL.Parameters["@FK_Escolaridad"].Value = this.WucDatosDemograficosPerfil.FK_Escolaridad;


			this.SPU_PERFIL.Parameters["@IN_EducEspecial"].Value = this.WucDatosDemograficosPerfil.IN_EducEspecial;
			this.SPU_PERFIL.Parameters["@IN_DesertorEscolar"].Value = this.WucDatosDemograficosPerfil.IN_DesertorEscolar;
            this.SPU_PERFIL.Parameters["@FK_Familia"].Value = 96;//this.WucDatosDemograficosPerfil.FK_Familia;
			this.SPU_PERFIL.Parameters["@NR_Familiar"].Value = this.WucDatosDemograficosPerfil.NR_Familiar;
			this.SPU_PERFIL.Parameters["@FK_Residencia"].Value = this.WucDatosDemograficosPerfil.FK_Residencia;
            this.SPU_PERFIL.Parameters["@FK_FreqAutoAyuda"].Value = this.WucDatosDemograficosPerfil.FK_FreqAutoAyuda;
			this.SPU_PERFIL.Parameters["@IN_Arrestado30dias"].Value = this.WucDatosDemograficosPerfil.IN_Arrestado30dias;
			this.SPU_PERFIL.Parameters["@NR_Arrestos30dias"].Value = this.WucDatosDemograficosPerfil.NR_Arrestos30dias;
            #region DSMV
            this.SPU_PERFIL.Parameters["@FK_DSMV_TrastornosClinicos1"].Value = this.WucEpisodioPerfil.FK_DSMV_TrastornosClinicos1;
            this.SPU_PERFIL.Parameters["@FK_DSMV_TrastornosClinicos2"].Value = this.WucEpisodioPerfil.FK_DSMV_TrastornosClinicos2;
            this.SPU_PERFIL.Parameters["@FK_DSMV_TrastornosClinicos3"].Value = this.WucEpisodioPerfil.FK_DSMV_TrastornosClinicos3;
            this.SPU_PERFIL.Parameters["@FK_DSMV_TrastornosPersonalidadRM1"].Value = this.WucEpisodioPerfil.FK_DSMV_TrastornosPersonalidadRM1;
            this.SPU_PERFIL.Parameters["@FK_DSMV_TrastornosPersonalidadRM2"].Value = this.WucEpisodioPerfil.FK_DSMV_TrastornosPersonalidadRM2;
            this.SPU_PERFIL.Parameters["@FK_DSMV_TrastornosPersonalidadRM3"].Value = this.WucEpisodioPerfil.FK_DSMV_TrastornosPersonalidadRM3;
            this.SPU_PERFIL.Parameters["@FK_DSMV_ProblemasPsicosocialesAmbientales1"].Value = this.WucEpisodioPerfil.FK_DSMV_ProblemasPsicosocialesAmbientales1;
            this.SPU_PERFIL.Parameters["@FK_DSMV_ProblemasPsicosocialesAmbientales2"].Value = this.WucEpisodioPerfil.FK_DSMV_ProblemasPsicosocialesAmbientales2;
            this.SPU_PERFIL.Parameters["@FK_DSMV_ProblemasPsicosocialesAmbientales3"].Value = this.WucEpisodioPerfil.FK_DSMV_ProblemasPsicosocialesAmbientales3;
            this.SPU_PERFIL.Parameters["@NR_DSMV_FuncionamientoGlobal"].Value = this.WucEpisodioPerfil.NR_DSMV_FuncionamientoGlobal;
            this.SPU_PERFIL.Parameters["@DE_DSMV_OtrasObservaciones"].Value = this.WucEpisodioPerfil.DE_DSMV_Comentarios;
            this.SPU_PERFIL.Parameters["@DE_DSMV_Comentarios"].Value = this.WucEpisodioPerfil.DE_DSMV_OtrasObservaciones;
            this.SPU_PERFIL.Parameters["@IN_DSMV_DiagnosticoDual"].Value = this.WucEpisodioPerfil.IN_DSMV_DiagnosticoDual;

			/*DSMV Sustancias*/
			this.SPU_PERFIL.Parameters["@FK_DSMV_Sustancias1"].Value = this.WucEpisodioPerfil.FK_DSMV_Sustancias1;
			this.SPU_PERFIL.Parameters["@FK_DSMV_Sustancias2"].Value = this.WucEpisodioPerfil.FK_DSMV_Sustancias2;
			this.SPU_PERFIL.Parameters["@FK_DSMV_Sustancias3"].Value = this.WucEpisodioPerfil.FK_DSMV_Sustancias3;

			#endregion
			this.SPU_PERFIL.Parameters["@FK_DrogaPrimario"].Value = this.WucEpisodioPerfil.FK_DrogaPrimario;
			this.SPU_PERFIL.Parameters["@FK_ViaPrimario"].Value = this.WucEpisodioPerfil.FK_ViaPrimario;
			this.SPU_PERFIL.Parameters["@FK_FrecuenciaPrimario"].Value = this.WucEpisodioPerfil.FK_FrecuenciaPrimario;
			this.SPU_PERFIL.Parameters["@IN_EdadInicioPrimario"].Value = this.WucEpisodioPerfil.IN_EdadInicioPrimario;
			this.SPU_PERFIL.Parameters["@FK_DrogaSecundario"].Value = this.WucEpisodioPerfil.FK_DrogaSecundario;
			this.SPU_PERFIL.Parameters["@FK_ViaSecundario"].Value = this.WucEpisodioPerfil.FK_ViaSecundario;
			this.SPU_PERFIL.Parameters["@FK_FrecuenciaSecundario"].Value = this.WucEpisodioPerfil.FK_FrecuenciaSecundario;
			this.SPU_PERFIL.Parameters["@IN_EdadInicioSecundario"].Value = this.WucEpisodioPerfil.IN_EdadInicioSecundario;
			this.SPU_PERFIL.Parameters["@FK_DrogaTerciario"].Value = this.WucEpisodioPerfil.FK_DrogaTerciario;
			this.SPU_PERFIL.Parameters["@FK_ViaTerciario"].Value = this.WucEpisodioPerfil.FK_ViaTerciario;
			this.SPU_PERFIL.Parameters["@FK_FrecuenciaTerciario"].Value = this.WucEpisodioPerfil.FK_FrecuenciaTerciario;
			this.SPU_PERFIL.Parameters["@IN_EdadInicioTerciario"].Value = this.WucEpisodioPerfil.IN_EdadInicioTerciario;
			this.SPU_PERFIL.Parameters["@DE_Comentario"].Value = this.WucDatosEvaluacion.DE_Comentario;
			this.SPU_PERFIL.Parameters["@NR_CelularPrimario"].Value = this.WucDatosPersonales.NR_CelularPrimario;
			this.SPU_PERFIL.Parameters["@NR_CelularContacto"].Value = this.WucDatosPersonales.NR_CelularContacto;
			this.SPU_PERFIL.Parameters["@DE_EmailPrimario"].Value = this.WucDatosPersonales.DE_EmailPrimario;
			this.SPU_PERFIL.Parameters["@DE_EmailSecundario"].Value = this.WucDatosPersonales.DE_EmailSecundario;
			this.SPU_PERFIL.Parameters["@FK_IDENTIDAD_GENERO"].Value = this.WucDatosPersonales.FK_IDENTIDAD_GENERO;

			this.SPU_PERFIL.Parameters["@FK_CatRecuperacionRes"].Value = this.WucEpisodioPerfil.FK_CatRecuperacionRes;
			this.SPU_PERFIL.Parameters["@HogarRecuperacionRes"].Value = this.WucEpisodioPerfil.HogarRecuperacionRes;
			this.SPU_PERFIL.Parameters["@IN_Fumado"].Value = this.WucEpisodioPerfil.IN_Fumado;
			this.SPU_PERFIL.Parameters["@DE_FrecuenciaFumado"].Value = this.WucEpisodioPerfil.DE_FrecuenciaFumado;
			this.SPU_PERFIL.Parameters["@NR_CigarrosXDias"].Value = this.WucEpisodioPerfil.NR_CigarrosXDias;
			this.SPU_PERFIL.Parameters["@DE_DrogaNueva1"].Value = this.WucEpisodioPerfil.DE_DrogaNueva1;
			this.SPU_PERFIL.Parameters["@DE_DrogaNueva2"].Value = this.WucEpisodioPerfil.DE_DrogaNueva2;
			this.SPU_PERFIL.Parameters["@DE_DrogaNueva3"].Value = this.WucEpisodioPerfil.DE_DrogaNueva3;
			this.SPU_PERFIL.Parameters["@IN_Toxicologia1"].Value = this.WucEpisodioPerfil.IN_Toxicologia1;
			this.SPU_PERFIL.Parameters["@IN_Toxicologia2"].Value = this.WucEpisodioPerfil.IN_Toxicologia2;
			this.SPU_PERFIL.Parameters["@IN_Toxicologia3"].Value = this.WucEpisodioPerfil.IN_Toxicologia3;


			Guid PK_Sesion = new Guid(this.Session["pk_sesion"].ToString());
            this.SPU_PERFIL.Parameters["@FK_Sesion"].Value = PK_Sesion;

            //Added New Data
            this.SPU_NewData = new System.Data.SqlClient.SqlCommand();
            this.SPU_NewData.CommandText = "[SPU_NewData]";
            this.SPU_NewData.CommandType = System.Data.CommandType.StoredProcedure;
            this.SPU_NewData.Connection = this.cnn;
            this.SPU_NewData.Parameters.AddWithValue("@FK_Militar", this.WucDatosPersonales.FK_Militar);
            this.SPU_NewData.Parameters.AddWithValue("@FK_FamMilitar", this.WucDatosPersonales.IN_FamiliaMilitar);
            this.SPU_NewData.Parameters.AddWithValue("@FK_Genero", this.WucDatosPersonales.FK_Genero);

            //Added
            _m_PK_Perfil = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString());



			try
			{
				this.cnn.Open();
				this.SPU_PERFIL.ExecuteNonQuery();

                if (this.WucEpisodioPerfil.EsProgramaMental(m_PK_Programa))
                {
                    this.SPD_Ref_PracticasBasadasEnEvidencia = new System.Data.SqlClient.SqlCommand();
                    this.SPD_Ref_PracticasBasadasEnEvidencia.CommandText = "[SPD_Ref_PracticasBasadasEnEvidencia]";
                    this.SPD_Ref_PracticasBasadasEnEvidencia.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPD_Ref_PracticasBasadasEnEvidencia.Connection = this.cnn;
                    this.SPD_Ref_PracticasBasadasEnEvidencia.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                    this.SPD_Ref_PracticasBasadasEnEvidencia.ExecuteNonQuery();


                    for (int i = 0; i < this.WucEpisodioPerfil.PracticasBasadasEvidenciaCount; i++)
                    {
                        this.SPU_Ref_PracticasBasadasEnEvidencia = new System.Data.SqlClient.SqlCommand();
                        this.SPU_Ref_PracticasBasadasEnEvidencia.CommandText = "[SPU_Ref_PracticasBasadasEnEvidencia]";
                        this.SPU_Ref_PracticasBasadasEnEvidencia.CommandType = System.Data.CommandType.StoredProcedure;
                        this.SPU_Ref_PracticasBasadasEnEvidencia.Connection = this.cnn;
                        this.SPU_Ref_PracticasBasadasEnEvidencia.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                        this.SPU_Ref_PracticasBasadasEnEvidencia.Parameters.AddWithValue("@FK_Practica", this.WucEpisodioPerfil.PracticasBasadasEvidenciaItem(i));
                        this.SPU_Ref_PracticasBasadasEnEvidencia.ExecuteNonQuery();
                        this.SPU_Ref_PracticasBasadasEnEvidencia.Dispose();
                    }
                }

                if(Const.EsProgramaMetadona(m_PK_Programa))
                {
                    this.SPU_METADONA = new System.Data.SqlClient.SqlCommand();
                    this.SPU_METADONA.CommandText = "[SPU_METADONA]";
                    this.SPU_METADONA.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPU_METADONA.Connection = this.cnn;
                    this.SPU_METADONA.Parameters.AddWithValue("@FK_Participa", this.WucTakeHome.THBelong);
                    if (this.WucTakeHome.THBelong != null && this.WucTakeHome.THBelong == 1)
                    {
                        this.SPU_METADONA.Parameters.AddWithValue("@FK_Etapa", this.WucTakeHome.EtapaTH);
                        this.SPU_METADONA.Parameters.AddWithValue("@FE_Entrada", this.WucTakeHome.FE_THIni);
                        this.SPU_METADONA.Parameters.AddWithValue("@FE_Salida", this.WucTakeHome.FE_THFin);
                        this.SPU_METADONA.Parameters.AddWithValue("@NR_Botellas", this.WucTakeHome.NR_CantidadBotellas);
                        this.SPU_METADONA.Parameters.AddWithValue("@FK_FrecuenciaBotellas", this.WucTakeHome.FK_FrecuenciaBotellas);
                    }
                    this.SPU_METADONA.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                    this.SPU_METADONA.ExecuteNonQuery();
                    //SPD_Ref_RazonTH
                    this.SPD_Ref_RazonTH = new System.Data.SqlClient.SqlCommand();
                    this.SPD_Ref_RazonTH.CommandText = "[SPD_Ref_RazonTH]";
                    this.SPD_Ref_RazonTH.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPD_Ref_RazonTH.Connection = this.cnn;
                    this.SPD_Ref_RazonTH.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                    this.SPD_Ref_RazonTH.ExecuteNonQuery();


                    for (int i = 0; i < this.WucTakeHome.RazonTHCount; i++)
                    {
                        //SPU_Ref_RazonTH
                        this.SPU_Ref_RazonTH = new System.Data.SqlClient.SqlCommand();
                        this.SPU_Ref_RazonTH.CommandText = "[SPU_Ref_RazonTH]";
                        this.SPU_Ref_RazonTH.CommandType = System.Data.CommandType.StoredProcedure;
                        this.SPU_Ref_RazonTH.Connection = this.cnn;
                        this.SPU_Ref_RazonTH.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                        this.SPU_Ref_RazonTH.Parameters.AddWithValue("@FK_Razon", this.WucTakeHome.RazonTHItem(i));
                        this.SPU_Ref_RazonTH.ExecuteNonQuery();
                        this.SPU_Ref_RazonTH.Dispose();
                    }
                }
                //Added New
                this.SPU_NewData.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                this.SPU_NewData.ExecuteNonQuery();

                this.SPD_Ref_CondicionesDiagnosticadas = new System.Data.SqlClient.SqlCommand();
                this.SPD_Ref_CondicionesDiagnosticadas.CommandText = "[SPD_Ref_CondicionesDiagnosticadas]";
                this.SPD_Ref_CondicionesDiagnosticadas.CommandType = System.Data.CommandType.StoredProcedure;
                this.SPD_Ref_CondicionesDiagnosticadas.Connection = this.cnn;
                this.SPD_Ref_CondicionesDiagnosticadas.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                this.SPD_Ref_CondicionesDiagnosticadas.ExecuteNonQuery();


                for (int i = 0; i < this.WucEpisodioPerfil.CondicionesDiagnosticadasCount; i++)
                {
                    this.SPU_Ref_CondicionesDiagnosticadas = new System.Data.SqlClient.SqlCommand();
                    this.SPU_Ref_CondicionesDiagnosticadas.CommandText = "[SPU_Ref_CondicionesDiagnosticadas]";
                    this.SPU_Ref_CondicionesDiagnosticadas.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPU_Ref_CondicionesDiagnosticadas.Connection = this.cnn;
                    this.SPU_Ref_CondicionesDiagnosticadas.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                    this.SPU_Ref_CondicionesDiagnosticadas.Parameters.AddWithValue("@FK_Diagnostico", this.WucEpisodioPerfil.CondicionesDiagnosticadasItem(i));
                    this.SPU_Ref_CondicionesDiagnosticadas.ExecuteNonQuery();
                    this.SPU_Ref_CondicionesDiagnosticadas.Dispose();
                }
              
                    //SPD_Ref_CompFamilia
                    this.SPD_Ref_CompFamilia = new System.Data.SqlClient.SqlCommand();
                    this.SPD_Ref_CompFamilia.CommandText = "[SPD_Ref_CompFamilia]";
                    this.SPD_Ref_CompFamilia.CommandType = System.Data.CommandType.StoredProcedure;
                    this.SPD_Ref_CompFamilia.Connection = this.cnn;
                    this.SPD_Ref_CompFamilia.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                    this.SPD_Ref_CompFamilia.ExecuteNonQuery();

                    

                    for (int i = 0; i < this.WucDatosDemograficosPerfil.CompFamCount; i++)
                    {
                        //SPU_Ref_CompFamilia
                        this.SPU_Ref_CompFamilia = new System.Data.SqlClient.SqlCommand();
                        this.SPU_Ref_CompFamilia.CommandText = "[SPU_Ref_CompFamilia]";
                        this.SPU_Ref_CompFamilia.CommandType = System.Data.CommandType.StoredProcedure;
                        this.SPU_Ref_CompFamilia.Connection = this.cnn;
                        this.SPU_Ref_CompFamilia.Parameters.AddWithValue("@FK_Perfil", _m_PK_Perfil);
                        this.SPU_Ref_CompFamilia.Parameters.AddWithValue("@FK_CompFamilia", this.WucDatosDemograficosPerfil.CompFamItem(i));
                        this.SPU_Ref_CompFamilia.ExecuteNonQuery();
                        this.SPU_Ref_CompFamilia.Dispose();
                    }

				this.cnn.Close();
			}
			catch(Exception ex)
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

		#region Código generado por el Diseñador de Web Forms
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{    
			this.dsPerfil = new ASSMCA.perfiles.dsPerfil();
			this.daLkpPerfil = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
			this.cnn = new System.Data.SqlClient.SqlConnection();
			this.daPerfil = new System.Data.SqlClient.SqlDataAdapter();
			this.SPC_PERFIL = new System.Data.SqlClient.SqlCommand();
			this.SPU_PERFIL = new System.Data.SqlClient.SqlCommand();
			this.daPerfilValidaciones = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlSelectCommand3 = new System.Data.SqlClient.SqlCommand();
			this.dsSeguridad = new ASSMCA.dsSeguridad();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.daAdmision = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlSelectCommand4 = new System.Data.SqlClient.SqlCommand();
			((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsSeguridad)).BeginInit();
            this.cnn.ConnectionString = NewSource.connectionString;
            #region dsPerfil
            this.dsPerfil.DataSetName = "dsPerfil";
			this.dsPerfil.Locale = new System.Globalization.CultureInfo("en-US");
            #endregion
            #region daLkpPerfil
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
                        new System.Data.Common.DataColumnMapping("PK_FreqAutoAyuda", "PK_Freq_AutoAyuda"),
                        new System.Data.Common.DataColumnMapping("DE_FreqAutoAyuda", "DE_FreqAutoAyuda")}),
                new System.Data.Common.DataTableMapping("Table16", "SA_LKP_TEDS_SITUACION_ESCOLAR", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("PK_SituacionEscolar", "PK_SituacionEscolar"),
                        new System.Data.Common.DataColumnMapping("DE_SituacionEscolar", "DE_SituacionEscolar")})});
			// 
			// sqlSelectCommand2
			// 
			this.sqlSelectCommand2.CommandText = "[SPR_LKP_PERFIL]";
			this.sqlSelectCommand2.CommandType = System.Data.CommandType.StoredProcedure;
			this.sqlSelectCommand2.Connection = this.cnn;
			this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            #endregion
            #region daPerfil
            this.daPerfil.SelectCommand = this.sqlSelectCommand1;
			this.daPerfil.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
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
	new System.Data.Common.DataColumnMapping("NR_TotalArrestosPasado", "NR_TotalArrestosPasado"),
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
    new System.Data.Common.DataColumnMapping("FE_Contacto", "FE_Contacto"),
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
	new System.Data.Common.DataColumnMapping("IN_Arrestado30dias", "IN_Arrestado30dias"),
	new System.Data.Common.DataColumnMapping("DE_Arrestado30dias", "DE_Arrestado30dias"),
	new System.Data.Common.DataColumnMapping("NR_Arrestos30dias", "NR_Arrestos30dias"),
	new System.Data.Common.DataColumnMapping("IN_EstLeg", "IN_EstLeg"),
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
	new System.Data.Common.DataColumnMapping("FK_SituacionEscolar", "FK_SituacionEscolar"),
	new System.Data.Common.DataColumnMapping("DE_SituacionEscolar", "DE_SituacionEscolar"),
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
                new System.Data.Common.DataColumnMapping("DE_DSMV_DiagnosticoDual", "DE_DSMV_DiagnosticoDual"),
				new System.Data.Common.DataColumnMapping("NR_CelularPrimario", "NR_CelularPrimario"),
				new System.Data.Common.DataColumnMapping("NR_CelularContacto", "NR_CelularContacto"),
				new System.Data.Common.DataColumnMapping("DE_EmailPrimario", "DE_EmailPrimario"),
				new System.Data.Common.DataColumnMapping("DE_EmailSecundario", "DE_EmailSecundario"),
				new System.Data.Common.DataColumnMapping("FK_CatRecuperacionRes", "FK_CatRecuperacionRes"),
				new System.Data.Common.DataColumnMapping("HogarRecuperacionRes", "HogarRecuperacionRes"),
				new System.Data.Common.DataColumnMapping("DE_CarRecuperacionRes", "DE_CarRecuperacionRes"),
				new System.Data.Common.DataColumnMapping("FK_DSMV_Sustancias1", "FK_DSMV_Sustancias1"),
				new System.Data.Common.DataColumnMapping("DE_DSMV_Sustancias1", "DE_DSMV_Sustancias1"),
				new System.Data.Common.DataColumnMapping("FK_DSMV_Sustancias2", "FK_DSMV_Sustancias2"),
				new System.Data.Common.DataColumnMapping("DE_DSMV_Sustancias2", "DE_DSMV_Sustancias2"),
				new System.Data.Common.DataColumnMapping("FK_DSMV_Sustancias3", "FK_DSMV_Sustancias3"),
				new System.Data.Common.DataColumnMapping("DE_DSMV_Sustancias3", "DE_DSMV_Sustancias3"),
				new System.Data.Common.DataColumnMapping("IN_Fumado", "IN_Fumado"),
				new System.Data.Common.DataColumnMapping("DE_FrecuenciaFumado", "DE_FrecuenciaFumado"),
				new System.Data.Common.DataColumnMapping("NR_CigarrosXDias", "NR_CigarrosXDias"),
				new System.Data.Common.DataColumnMapping("DE_DrogaNueva1", "DE_DrogaNueva1"),
				new System.Data.Common.DataColumnMapping("DE_DrogaNueva2", "DE_DrogaNueva2"),
				new System.Data.Common.DataColumnMapping("DE_DrogaNueva3", "DE_DrogaNueva3")})});
            #endregion
            #region SPC_PERFIL
            this.SPC_PERFIL.CommandText = "dbo.[SPC_PERFIL]";
			this.SPC_PERFIL.CommandType = System.Data.CommandType.StoredProcedure;
			this.SPC_PERFIL.Connection = this.cnn;
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));

            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_CategoriaCentroPrivado", System.Data.SqlDbType.Int, 2));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TipoAdmision", System.Data.SqlDbType.Int, 2));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Episodio", System.Data.SqlDbType.Int, 4));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_Expediente", System.Data.SqlDbType.VarChar, 12));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_Perfil", System.Data.SqlDbType.DateTime, 8));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_Contacto", System.Data.SqlDbType.DateTime, 8));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_TI_Perfil", System.Data.SqlDbType.VarChar, 2));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_EstadoMarital", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_CondicionLaboral", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ActividadNoLaboral", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_Hijos", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Escolaridad", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_EducEspecial", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_DesertorEscolar", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Familia", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_Familiar", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Residencia", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_ParticReunGrupos", System.Data.SqlDbType.TinyInt, 1));
            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FreqAutoAyuda", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Arrestado30dias", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_Arrestos30dias", System.Data.SqlDbType.TinyInt, 1));
            //DISABLED DSM IV
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosClinicosPrimario", System.Data.SqlDbType.SmallInt, 2));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosClinicosSecundario", System.Data.SqlDbType.SmallInt, 2));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosClinicosTerciario", System.Data.SqlDbType.SmallInt, 2));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosPersonalidadPrimario", System.Data.SqlDbType.SmallInt, 2));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosPersonalidadSecundario", System.Data.SqlDbType.SmallInt, 2));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosPersonalidadTerciario", System.Data.SqlDbType.SmallInt, 2));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CO_CondicionesMedicasPrimario", System.Data.SqlDbType.VarChar, 50));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CO_CondicionesMedicasSecundario", System.Data.SqlDbType.VarChar, 50));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CO_CondicionesMedicasTerciario", System.Data.SqlDbType.VarChar, 50));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ProblemasPsicosocialesPrimario", System.Data.SqlDbType.TinyInt, 1));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ProblemasPsicosocialesSecundario", System.Data.SqlDbType.TinyInt, 1));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ProblemasPsicosocialesTerciario", System.Data.SqlDbType.TinyInt, 1));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_EscalaGAF", System.Data.SqlDbType.Int, 4));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DisposicionFinalReferido", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DrogaPrimario", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ViaPrimario", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FrecuenciaPrimario", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_EdadInicioPrimario", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DrogaSecundario", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ViaSecundario", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FrecuenciaSecundario", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_EdadInicioSecundario", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DrogaTerciario", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ViaTerciario", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FrecuenciaTerciario", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_EdadInicioTerciario", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_PromedioVisitas", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, false, ((System.Byte)(18)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Alta", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_CentroTraslado", System.Data.SqlDbType.SmallInt, 2));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_Comentario", System.Data.SqlDbType.VarChar, 1500));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Sesion", System.Data.SqlDbType.UniqueIdentifier, 16));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_EtapaTH", System.Data.SqlDbType.Int, 4));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_RazonesTH", System.Data.SqlDbType.Int, 4));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@THApply", System.Data.SqlDbType.Int, 4));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@THBelong", System.Data.SqlDbType.Int, 4));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_THIni", System.Data.SqlDbType.DateTime, 8));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_THFin", System.Data.SqlDbType.DateTime, 8));

            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Militar", System.Data.SqlDbType.Int, 4));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_FamiliaMilitar", System.Data.SqlDbType.Int, 4));
            //this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Genero", System.Data.SqlDbType.Int, 4));

			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Perfil", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));

            this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_SituacionEscolar", System.Data.SqlDbType.Int, 2));
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

			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_CelularPrimario", System.Data.SqlDbType.VarChar, 50));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_CelularContacto", System.Data.SqlDbType.VarChar, 50));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_EmailPrimario", System.Data.SqlDbType.VarChar, 1500));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_EmailSecundario", System.Data.SqlDbType.VarChar, 1500));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_IDENTIDAD_GENERO", System.Data.SqlDbType.Int));


			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_CatRecuperacionRes", System.Data.SqlDbType.TinyInt));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HogarRecuperacionRes", System.Data.SqlDbType.VarChar, 1500));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_Sustancias1", System.Data.SqlDbType.Int, 4));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_Sustancias2", System.Data.SqlDbType.Int, 4));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_Sustancias3", System.Data.SqlDbType.Int, 4));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Fumado", System.Data.SqlDbType.SmallInt, 3));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_FrecuenciaFumado", System.Data.SqlDbType.VarChar, 1500));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_CigarrosXDias", System.Data.SqlDbType.Int, 4));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_DrogaNueva1", System.Data.SqlDbType.VarChar, 1500));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_DrogaNueva2", System.Data.SqlDbType.VarChar, 1500));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_DrogaNueva3", System.Data.SqlDbType.VarChar, 1500));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Toxicologia1", System.Data.SqlDbType.TinyInt, 3));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Toxicologia2", System.Data.SqlDbType.TinyInt, 3));
			this.SPC_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Toxicologia3", System.Data.SqlDbType.TinyInt, 3));


			#endregion
			#region SPU_PERFIL
			this.SPU_PERFIL.CommandText = "dbo.[SPU_PERFIL]";
			this.SPU_PERFIL.CommandType = System.Data.CommandType.StoredProcedure;
			this.SPU_PERFIL.Connection = this.cnn;
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TipoAdmision", System.Data.SqlDbType.Int, 2));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_CategoriaCentroPrivado", System.Data.SqlDbType.Int, 2));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_NR_Perfil", System.Data.SqlDbType.Int, 4));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_Perfil", System.Data.SqlDbType.DateTime, 8));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_Contacto", System.Data.SqlDbType.DateTime, 8));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_TI_Perfil", System.Data.SqlDbType.VarChar, 2));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_EstadoMarital", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_CondicionLaboral", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ActividadNoLaboral", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_Hijos", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Escolaridad", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_EducEspecial", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_DesertorEscolar", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Familia", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_Familiar", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Residencia", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_ParticReunGrupos", System.Data.SqlDbType.TinyInt, 1));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FreqAutoAyuda", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Arrestado30dias", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_Arrestos30dias", System.Data.SqlDbType.TinyInt, 1));

            //DISABLED DSM IV
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosClinicosPrimario", System.Data.SqlDbType.SmallInt, 2));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosClinicosSecundario", System.Data.SqlDbType.SmallInt, 2));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosClinicosTerciario", System.Data.SqlDbType.SmallInt, 2));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosPersonalidadPrimario", System.Data.SqlDbType.SmallInt, 2));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosPersonalidadSecundario", System.Data.SqlDbType.SmallInt, 2));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_TrastornosPersonalidadTerciario", System.Data.SqlDbType.SmallInt, 2));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CO_CondicionesMedicasPrimario", System.Data.SqlDbType.VarChar, 50));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CO_CondicionesMedicasSecundario", System.Data.SqlDbType.VarChar, 50));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CO_CondicionesMedicasTerciario", System.Data.SqlDbType.VarChar, 50));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ProblemasPsicosocialesPrimario", System.Data.SqlDbType.TinyInt, 1));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ProblemasPsicosocialesSecundario", System.Data.SqlDbType.TinyInt, 1));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ProblemasPsicosocialesTerciario", System.Data.SqlDbType.TinyInt, 1));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_EscalaGAF", System.Data.SqlDbType.Int, 4));

			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DisposicionFinalReferido", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DrogaPrimario", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ViaPrimario", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FrecuenciaPrimario", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_EdadInicioPrimario", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DrogaSecundario", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ViaSecundario", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FrecuenciaSecundario", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_EdadInicioSecundario", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DrogaTerciario", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_ViaTerciario", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_FrecuenciaTerciario", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_EdadInicioTerciario", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_PromedioVisitas", System.Data.SqlDbType.Decimal, 9, System.Data.ParameterDirection.Input, false, ((System.Byte)(18)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Alta", System.Data.SqlDbType.TinyInt, 1));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_CentroTraslado", System.Data.SqlDbType.SmallInt, 2));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_Comentario", System.Data.SqlDbType.VarChar, 1500));
            this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_SituacionEscolar", System.Data.SqlDbType.Int,2));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Sesion", System.Data.SqlDbType.UniqueIdentifier, 16));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_EtapaTH", System.Data.SqlDbType.Int, 4));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_RazonesTH", System.Data.SqlDbType.Int, 4));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@THApply", System.Data.SqlDbType.Int, 4));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@THBelong", System.Data.SqlDbType.Int, 4));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_THIni", System.Data.SqlDbType.DateTime, 8));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_THFin", System.Data.SqlDbType.DateTime, 8));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Militar", System.Data.SqlDbType.Int, 4));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_FamiliaMilitar", System.Data.SqlDbType.Int, 4));
            //this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Genero", System.Data.SqlDbType.Int, 4));
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
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_CelularPrimario", System.Data.SqlDbType.VarChar, 50));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_CelularContacto", System.Data.SqlDbType.VarChar, 50));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_EmailPrimario", System.Data.SqlDbType.VarChar, 1500));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_EmailSecundario", System.Data.SqlDbType.VarChar, 1500));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_IDENTIDAD_GENERO", System.Data.SqlDbType.Int));

			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_CatRecuperacionRes", System.Data.SqlDbType.TinyInt));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@HogarRecuperacionRes", System.Data.SqlDbType.VarChar, 1500));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_Sustancias1", System.Data.SqlDbType.Int, 4));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_Sustancias2", System.Data.SqlDbType.Int, 4));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_DSMV_Sustancias3", System.Data.SqlDbType.Int, 4));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Fumado", System.Data.SqlDbType.SmallInt, 3));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_FrecuenciaFumado", System.Data.SqlDbType.VarChar, 1500));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_CigarrosXDias", System.Data.SqlDbType.Int, 4));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_DrogaNueva1", System.Data.SqlDbType.VarChar, 1500));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_DrogaNueva2", System.Data.SqlDbType.VarChar, 1500));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DE_DrogaNueva3", System.Data.SqlDbType.VarChar, 1500));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Toxicologia1", System.Data.SqlDbType.TinyInt, 3));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Toxicologia2", System.Data.SqlDbType.TinyInt, 3));
			this.SPU_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Toxicologia3", System.Data.SqlDbType.TinyInt, 3));
			#endregion
			#region daPerfilValidaciones
			this.daPerfilValidaciones.SelectCommand = this.sqlSelectCommand3;
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
			// 
			// sqlSelectCommand3
			// 
			this.sqlSelectCommand3.CommandText = "[SPR_PERFIL_VALIDACIONES]";
			this.sqlSelectCommand3.CommandType = System.Data.CommandType.StoredProcedure;
			this.sqlSelectCommand3.Connection = this.cnn;
			this.sqlSelectCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
			this.sqlSelectCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Perfil", System.Data.SqlDbType.Int, 4));
			this.sqlSelectCommand3.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Episodio", System.Data.SqlDbType.Int, 4));
            #endregion
            #region dsSeguridad
            // 
			this.dsSeguridad.DataSetName = "dsSeguridad";
			this.dsSeguridad.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// sqlSelectCommand1
			// 
			this.sqlSelectCommand1.CommandText = "[SPR_PERFIL_COMPLETO]";
			this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
			this.sqlSelectCommand1.Connection = this.cnn;
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Perfil", System.Data.SqlDbType.Int, 4));
            #endregion
            #region daAdmision
			this.daAdmision.SelectCommand = this.sqlSelectCommand4;
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
			    new System.Data.Common.DataColumnMapping("NR_TotalArrestosPasado", "NR_TotalArrestosPasado"),
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
                new System.Data.Common.DataColumnMapping("FE_Contacto", "FE_Contacto"),
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
			    new System.Data.Common.DataColumnMapping("IN_Arrestado30dias", "IN_Arrestado30dias"),
			    new System.Data.Common.DataColumnMapping("DE_Arrestado30dias", "DE_Arrestado30dias"),
			    new System.Data.Common.DataColumnMapping("NR_Arrestos30dias", "NR_Arrestos30dias"),
                new System.Data.Common.DataColumnMapping("IN_EstLeg", "IN_EstLeg"),
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
			    new System.Data.Common.DataColumnMapping("FK_SituacionEscolar", "FK_SituacionEscolar"),
			    new System.Data.Common.DataColumnMapping("DE_SituacionEscolar", "DE_SituacionEscolar"),
				new System.Data.Common.DataColumnMapping("NR_CelularPrimario", "NR_CelularPrimario"),
				new System.Data.Common.DataColumnMapping("NR_CelularContacto", "NR_CelularContacto"),
				new System.Data.Common.DataColumnMapping("DE_EmailPrimario", "DE_EmailPrimario"),
				new System.Data.Common.DataColumnMapping("DE_EmailSecundario", "DE_EmailSecundario")})});
			// 
			// sqlSelectCommand4
			// 
			this.sqlSelectCommand4.CommandText = "[SPR_ADMISION]";
			this.sqlSelectCommand4.CommandType = System.Data.CommandType.StoredProcedure;
			this.sqlSelectCommand4.Connection = this.cnn;
			this.sqlSelectCommand4.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
			this.sqlSelectCommand4.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Admision", System.Data.SqlDbType.Int, 4));
            #endregion
            ((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsSeguridad)).EndInit();
		}
		#endregion
        #region Buttons
        protected void btnRegistrar_Click(object sender, System.EventArgs e)
		{
			int PK_Perfil = this.GuardarNuevo();
			this.Response.Redirect("frmEvaluacion.aspx?accion=read&pk_perfil=" + PK_Perfil);

		}

		protected void btnGuardarCambios_Click(object sender, System.EventArgs e)
		{
			this.GuardarCambios();
			int PK_Perfil = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString());
			this.Response.Redirect("frmEvaluacion.aspx?accion=read&pk_perfil=" + PK_Perfil);
		}

		protected void btnModificar_Click(object sender, System.EventArgs e)
		{
			int PK_Perfil = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString());
			this.Response.Redirect("frmEvaluacion.aspx?accion=update&pk_perfil=" + PK_Perfil);
		}

		protected void btnEliminarAdmin_Click(object sender, System.EventArgs e)
		{
			int PK_Episodio = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_Episodio"].ToString());
			int PK_Perfil = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString());
			this.Response.Redirect("frmEliminarPerfil.aspx?PK_Episodio=" + PK_Episodio.ToString() + "&PK_Perfil=" + PK_Perfil.ToString() + "&TI_Perfil=EV&accion=L");
		}

		protected void btnEliminar_Click(object sender, System.EventArgs e)
		{
			int PK_Episodio = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_Episodio"].ToString());
			int PK_Perfil = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString());
			this.Response.Redirect("frmEliminarPerfil.aspx?PK_Episodio=" + PK_Episodio.ToString() + "&PK_Perfil=" + PK_Perfil.ToString() + "&TI_Perfil=EV&accion=F");
		}

		protected void btnModificarAdmin_Click(object sender, System.EventArgs e)
		{
			int PK_Perfil = Convert.ToInt32(this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString());
			this.Response.Redirect("frmEvaluacion.aspx?accion=update&pk_perfil=" + PK_Perfil);
        }
        #endregion
    }
}

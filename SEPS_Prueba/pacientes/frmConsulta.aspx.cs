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

namespace ASSMCA.Pacientes
{
	public partial class frmConsulta : System.Web.UI.Page
	{
		protected System.Data.SqlClient.SqlConnection cnn;
		protected System.Data.SqlClient.SqlDataAdapter daLkpPersona;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		protected ASSMCA.pacientes.dsPersona dsPersona;
		protected System.Data.SqlClient.SqlDataAdapter daPersonas;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
		protected ASSMCA.dsSeguridad dsSeguridad;
		private int m_PK_Programa;
        private PKAdministracion pkAdmin;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( this.Session["dsSeguridad"] == null )
			{
                this.Response.Redirect("~/Error.aspx?errMsg=sesion");
				return;
			}
            this.pkAdmin = (PKAdministracion)Convert.ToInt32(this.Session["pk_administracion"].ToString());
			this.m_PK_Programa = Convert.ToInt32(this.Session["PK_Programa"].ToString());
            if (Request.QueryString["fuente"] != null)
            {
                this.btnRegistrar.Visible = true;
                if (Request.QueryString["fuente"].ToString() == "admision")
                {
                    this.Label4.Text = "Búsqueda de pacientes en admisión";
                    HyperLinkColumn hlc = (HyperLinkColumn)this.dgPersonas.Columns[0];
                    hlc.DataNavigateUrlFormatString = "frmVisualizar.aspx?accion=consultar&pk_persona={0}&fuente=admision";
                }
                else if (Request.QueryString["fuente"].ToString() == "evaluacion")
                {
                    this.Label4.Text = "Búsqueda de pacientes en evaluación";
                    HyperLinkColumn hlc = (HyperLinkColumn)this.dgPersonas.Columns[0];
                    hlc.DataNavigateUrlFormatString = "frmVisualizar.aspx?accion=consultar&pk_persona={0}&fuente=evaluacion";
                }
                else if (Request.QueryString["fuente"].ToString() == "alta")
                {
                    this.Label4.Text = "Búsqueda de pacientes en alta";
                    HyperLinkColumn hlc = (HyperLinkColumn)this.dgPersonas.Columns[0];
                    hlc.DataNavigateUrlFormatString = "frmVisualizar.aspx?accion=consultar&pk_persona={0}&fuente=alta";
                }
            }
            else
            {
                this.btnRegistrar.Visible = false;
            }
			if(!this.IsPostBack)
			{
                this.dsSeguridad = (ASSMCA.dsSeguridad)Session["dsSeguridad"];
                this.ddlPrograma.DataBind();
				this.lblMensaje.Visible = true;
				this.dgPersonas.Visible = false;          
				DataRow drLKP_Sexo = this.dsPersona.LKP_Sexo.NewRow();
				drLKP_Sexo["PK_Sexo"] = 0;
				drLKP_Sexo["DE_Sexo"] = "";
				this.dsPersona.LKP_Sexo.Rows.Add(drLKP_Sexo);
				DataRow drLKP_Veterano = this.dsPersona.LKP_Veterano.NewRow();
				drLKP_Veterano["PK_Veterano"] = 0;
				drLKP_Veterano["DE_Veterano"] = "";
				this.dsPersona.LKP_Veterano.Rows.Add(drLKP_Veterano);
				DataRow drLKP_GrupoEtnico = this.dsPersona.LKP_GrupoEtnico.NewRow();
				drLKP_GrupoEtnico["PK_GrupoEtnico"] = 0;
				drLKP_GrupoEtnico["DE_GrupoEtnico"] = "";
                this.dsPersona.LKP_GrupoEtnico.Rows.Add(drLKP_GrupoEtnico);
				this.daLkpPersona.Fill(this.dsPersona);
				this.DataBind();
                ListItem li = new ListItem("", "0");
                this.ddlPrograma.Items.Insert(0, li);
                this.TipoBusqueda.Value = "0";
            }
			this.dsSeguridad = (dsSeguridad)this.Session["dsSeguridad"];
			int nr_rowIndex_dsSeguridad = Convert.ToInt32(this.Session["nr_rowIndex_dsSeguridad"].ToString());
            if (this.dsSeguridad.SA_USUARIO[nr_rowIndex_dsSeguridad].IN_C_PERSONA < 1) //Si esta denegado
            {
                this.btnRegistrar.Visible = false;
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
			this.cnn = new System.Data.SqlClient.SqlConnection();
			this.daLkpPersona = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.dsPersona = new ASSMCA.pacientes.dsPersona();
			this.daPersonas = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
			this.dsSeguridad = new ASSMCA.dsSeguridad();
			((System.ComponentModel.ISupportInitialize)(this.dsPersona)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsSeguridad)).BeginInit();
            #region cnn
            this.cnn.ConnectionString = NewSource.connectionString;
            #endregion
            #region daLkpPersona
			this.daLkpPersona.SelectCommand = this.sqlSelectCommand1;
			this.daLkpPersona.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
                new System.Data.Common.DataTableMapping("Table", "LKP_Sexo", new System.Data.Common.DataColumnMapping[] {
                    new System.Data.Common.DataColumnMapping("PK_Sexo", "PK_Sexo"),
                    new System.Data.Common.DataColumnMapping("DE_Sexo", "DE_Sexo")}),
                new System.Data.Common.DataTableMapping("Table1", "LKP_Veterano", new System.Data.Common.DataColumnMapping[] {
                    new System.Data.Common.DataColumnMapping("PK_Veterano", "PK_Veterano"),
                    new System.Data.Common.DataColumnMapping("DE_Veterano", "DE_Veterano")}),
                new System.Data.Common.DataTableMapping("Table2", "LKP_GrupoEtnico", new System.Data.Common.DataColumnMapping[] {
                    new System.Data.Common.DataColumnMapping("PK_GrupoEtnico", "PK_GrupoEtnico"),
                    new System.Data.Common.DataColumnMapping("DE_GrupoEtnico", "DE_GrupoEtnico")}),
                new System.Data.Common.DataTableMapping("Table3", "LKP_Raza", new System.Data.Common.DataColumnMapping[] {
                    new System.Data.Common.DataColumnMapping("PK_Raza", "PK_Raza"),
                    new System.Data.Common.DataColumnMapping("DE_Raza", "DE_Raza")})});
            #endregion
            #region sqlSelectCommand1
			this.sqlSelectCommand1.CommandText = "[SPR_LKP_PERSONA]";
			this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
			this.sqlSelectCommand1.Connection = this.cnn;
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            #endregion
            #region dsPersona
			this.dsPersona.DataSetName = "dsPersona";
			this.dsPersona.Locale = new System.Globalization.CultureInfo("en-US");
            #endregion
            #region daPersonas
			this.daPersonas.SelectCommand = this.sqlSelectCommand2;
			this.daPersonas.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
                new System.Data.Common.DataTableMapping("Table", "SA_PERSONAS", new System.Data.Common.DataColumnMapping[] {
	                new System.Data.Common.DataColumnMapping("PK_Persona", "PK_Persona"),
	                new System.Data.Common.DataColumnMapping("NR_SeguroSocial", "NR_SeguroSocial"),
	                new System.Data.Common.DataColumnMapping("Apellidos", "Apellidos"),
	                new System.Data.Common.DataColumnMapping("Nombres", "Nombres"),
	                new System.Data.Common.DataColumnMapping("NR_Edad", "NR_Edad"),
	                new System.Data.Common.DataColumnMapping("DE_Sexo", "DE_Sexo"),
                            new System.Data.Common.DataColumnMapping("TieneEpisodiosAbiertos", "TieneEpisodiosAbiertos"),
	               // new System.Data.Common.DataColumnMapping("DE_GrupoEtnico", "DE_GrupoEtnico"),
	                new System.Data.Common.DataColumnMapping("NR_Expediente", "NR_Expediente")})});
            #endregion
            #region sqlSelectCommand2
			this.sqlSelectCommand2.CommandText = "[SPR_PERSONAS]";
			this.sqlSelectCommand2.CommandType = System.Data.CommandType.StoredProcedure;
			this.sqlSelectCommand2.Connection = this.cnn;
			this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
			this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@strWhere", System.Data.SqlDbType.VarChar, 5000));
            #endregion
            #region dsSeguridad
			this.dsSeguridad.DataSetName = "dsSeguridad";
			this.dsSeguridad.Locale = new System.Globalization.CultureInfo("en-US");
			((System.ComponentModel.ISupportInitialize)(this.dsPersona)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsSeguridad)).EndInit();
            #endregion
        }
        #endregion
        protected void btnConsultar_Click(object sender, System.EventArgs e)
        {
            LblErrorAñoRangoInicio.Visible = false;
            LblErrorAñoRangoFin.Visible = false;

            bool errorFecha = false;

            if (this.ddlFiltroDeFecha.SelectedValue.ToString() == "2")//Rango de fechas
            {

                if (ValidarFecha(ddlDíaRangoInicio.SelectedValue.ToString(), ddlMesRangoInicio.SelectedValue.ToString(), txtAñoRangoInicio.Text) == false && txtAñoRangoInicio.Text != "")
                {
                    LblErrorAñoRangoInicio.Visible = true;
                    errorFecha = true;
                }
                if (ValidarFecha(ddlDíaRangoFin.SelectedValue.ToString(), ddlMesRangoFin.SelectedValue.ToString(), txtAñoRangoFin.Text) == false  && txtAñoRangoFin.Text !="")
                {
                    LblErrorAñoRangoFin.Visible = true;
                    errorFecha = true;
                }

                if (errorFecha == true)
                {
                    dgPersonas.DataSource = null;
                    dgPersonas.DataBind();
                    return;
                }
            }

            string strSQL = this.PrepararClausulaWhere();



         if (this.ddlTipoBusqueda.SelectedValue == "2")
            {
                strSQL += " AND (Episodio.FK_Programa=" + this.m_PK_Programa + ")";
            } 
           
            if ( strSQL.Trim() != "" )
			{
				this.daPersonas.SelectCommand.Parameters["@strWhere"].Value = strSQL;
				this.daPersonas.Fill(this.dsPersona);
				this.dgPersonas.DataBind();
			}
			if(this.dsPersona.SA_PERSONAS.Rows.Count > 0 )
			{
				this.lblMensaje.Visible = false;
				this.dgPersonas.Visible = true;
			}
			else
			{
				this.lblMensaje.Visible = true;
				this.dgPersonas.Visible = false;
			}
		}
		private string PrepararClausulaWhere()
		{
			string strSql = "";
            string AND = "";






            if ( this.txtIUP.Text.Trim() != "" )
			{
                strSql += "(Personas.PK_Persona =" + this.txtIUP.Text.Trim() + ")";
				AND = " AND ";
			}
			if( this.txtExpediente.Text.Trim() != "" )
			{
                strSql += AND + "(Personas.NR_Expediente " + this.PrepararPredicado(this.ddlExpediente.SelectedValue.ToString(), this.txtExpediente.Text.Trim()) + ")";
                strSql += " AND Personas.FK_Programa =" + this.m_PK_Programa;
				AND = " AND ";
			}
			if( this.txtNSS.Text.Trim() != "" )
			{
                strSql += AND + "(Personas.NR_SeguroSocial " + this.PrepararPredicado(this.ddlSeguroSocial.SelectedValue.ToString(), this.txtNSS.Text.Trim()) + ")";
				AND = " AND ";
			}
			if( this.txtPrimerApellido.Text.Trim() != "" )
			{
                strSql += AND + "(Personas.AP_Primero " + this.PrepararPredicado(this.ddlPrimerApellido.SelectedValue.ToString(), this.txtPrimerApellido.Text.Trim()) + " COLLATE SQL_Latin1_General_CP1_CI_AI)";
				AND = " AND ";
			}
			if( this.txtSegundoApellido.Text.Trim() != "" )
			{
                strSql += AND + "(Personas.AP_Segundo " + this.PrepararPredicado(this.ddlSegundoApellido.SelectedValue.ToString(), this.txtSegundoApellido.Text.Trim()) + " COLLATE SQL_Latin1_General_CP1_CI_AI)";
                AND = " AND ";
			}
			if( this.txtPrimerNombre.Text.Trim() != "" )
			{
                strSql += AND + "(Personas.NB_Primero " + this.PrepararPredicado(this.ddlPrimerNombre.SelectedValue.ToString(), this.txtPrimerNombre.Text.Trim()) + " COLLATE SQL_Latin1_General_CP1_CI_AI)";
                AND = " AND ";
			}
			if( this.txtSegundoNombre.Text.Trim() != "" )
			{
                strSql += AND + "(Personas.NB_Segundo " + this.PrepararPredicado(this.ddlSegundoNombre.SelectedValue.ToString(), this.txtSegundoNombre.Text.Trim()) + " COLLATE SQL_Latin1_General_CP1_CI_AI)";
                AND = " AND ";
			}
			if( this.txtEdad.Text.Trim() != "" )
			{
                strSql += AND + "(Personas.NR_Edad " + this.PrepararPredicado(this.ddlEdad.SelectedValue.ToString(), this.txtEdad.Text.Trim()) + ")";
				AND = " AND ";
			}
			if( this.ddlSexo.SelectedValue.ToString() != "0" )
			{
                strSql += AND + "(Personas.FK_Sexo=" + this.ddlSexo.SelectedValue.ToString() + ")";
				AND = " AND ";
			}
			if( this.ddlVeterano.SelectedValue.ToString() != "0" )
			{
                strSql += AND + "(Personas.FK_Veterano=" + this.ddlVeterano.SelectedValue.ToString() + ")";
				AND = " AND ";
            }
            if (this.ddlGrupoEtnico.SelectedValue.ToString() != "0")
            {
                strSql += AND + "(Personas.FK_GrupoEtnico=" + this.ddlGrupoEtnico.SelectedValue.ToString() + ")";
                AND = " AND ";
            }
            if (this.ddlPrograma.SelectedValue.ToString() != "0")
            {
                strSql += AND + "(Personas.FK_Programa=" + this.ddlPrograma.SelectedValue.ToString()+")";
                AND = " AND ";
            }
            if (this.ddlEstadoEpisodios.SelectedValue.ToString() == "1")//Abierto
            {
                strSql += AND + "(Episodio.ES_Episodio<>1 OR Episodio.ES_EPISODIO IS NULL)";
                AND = " AND ";
            }
            else if (this.ddlEstadoEpisodios.SelectedValue.ToString() == "2")//Cerrado
            {
                strSql += AND + "(Episodio.ES_Episodio=1)";
                AND = " AND ";
            }
            #region Filtros de fechas
            if (this.ddlFiltroDeFecha.SelectedValue.ToString() == "1" && this.txtAñoExacta.Text != "")//Fecha exacta
            {
                switch (this.ddlFechasExactas.SelectedValue.ToString())
                {
                    case ("1")://Admisión
                        strSql += AND + "(Perfil.IN_TI_Perfil='AD' AND Perfil.FE_Perfil='" + ddlMesExacta.SelectedValue.ToString() + "/" + ddlDíaExacta.SelectedValue.ToString() + "/" + txtAñoExacta.Text + "')";
                        AND = " AND ";
                        break;
                    case ("2")://Evaluación
                        strSql += AND + "(Perfil.IN_TI_Perfil='EV' AND Perfil.FE_Perfil='" + ddlMesExacta.SelectedValue.ToString() + "/" + ddlDíaExacta.SelectedValue.ToString() + "/" + txtAñoExacta.Text + "')";
                        AND = " AND ";
                        break;
                    case ("3")://Alta
                        strSql += AND + "(Perfil.IN_TI_Perfil='AL' AND Perfil.FE_Perfil='" + ddlMesExacta.SelectedValue.ToString() + "/" + ddlDíaExacta.SelectedValue.ToString() + "/" + txtAñoExacta.Text + "')";
                        AND = " AND ";
                        break;
                    case ("4")://Edición
                        strSql += AND + "(Perfil.FE_Edicion='" + ddlMesExacta.SelectedValue.ToString() + "/" + ddlDíaExacta.SelectedValue.ToString() + "/" + txtAñoExacta.Text + "')";
                        AND = " AND ";
                        break;
                    case ("5")://Nacimiento
                        strSql += AND + "(Personas.FE_Nacimiento='" + ddlMesExacta.SelectedValue.ToString() + "/" + ddlDíaExacta.SelectedValue.ToString() + "/" + txtAñoExacta.Text + "')";
                        AND = " AND ";
                    break;
                    default: break;
                }
            }
            if (this.ddlFiltroDeFecha.SelectedValue.ToString() == "2")//Rango de fechas
            {
                 if (this.txtAñoRangoFin.Text != "" && this.txtAñoRangoInicio.Text != "")//Inicio y fin activos
                {
                     switch (this.ddlRangoTipoFecha.SelectedValue.ToString())
                    { 
                        case ("1")://Admisión
                            strSql += AND + "(Perfil.IN_TI_Perfil='AD' AND Perfil.FE_Perfil<='" + ddlMesRangoFin.SelectedValue.ToString() + "/" + ddlDíaRangoFin.SelectedValue.ToString() + "/" + txtAñoRangoFin.Text + "' AND Perfil.FE_Perfil>='" + ddlMesRangoInicio.SelectedValue.ToString() + "/" + ddlDíaRangoInicio.SelectedValue.ToString() + "/" + txtAñoRangoInicio.Text + "')";
                            AND = " AND ";
                            break;
                        case ("2")://Evaluación
                            strSql += AND + "(Perfil.IN_TI_Perfil='EV' AND Perfil.FE_Perfil<='" + ddlMesRangoFin.SelectedValue.ToString() + "/" + ddlDíaRangoFin.SelectedValue.ToString() + "/" + txtAñoRangoFin.Text + "' AND Perfil.FE_Perfil>='" + ddlMesRangoInicio.SelectedValue.ToString() + "/" + ddlDíaRangoInicio.SelectedValue.ToString() + "/" + txtAñoRangoInicio.Text + "')";
                            AND = " AND ";
                            break;
                        case ("3")://Alta
                            strSql += AND + "(Perfil.IN_TI_Perfil='AL' AND Perfil.FE_Perfil<='" + ddlMesRangoFin.SelectedValue.ToString() + "/" + ddlDíaRangoFin.SelectedValue.ToString() + "/" + txtAñoRangoFin.Text + "' AND Perfil.FE_Perfil>='" + ddlMesRangoInicio.SelectedValue.ToString() + "/" + ddlDíaRangoInicio.SelectedValue.ToString() + "/" + txtAñoRangoInicio.Text + "')";
                            AND = " AND ";
                            break;
                        case ("4")://Edición
                            strSql += AND + "(Perfil.FE_Edicion<='" + ddlMesRangoFin.SelectedValue.ToString() + "/" + ddlDíaRangoFin.SelectedValue.ToString() + "/" + txtAñoRangoFin.Text + "' AND Perfil.FE_Edicion>='" + ddlMesRangoInicio.SelectedValue.ToString() + "/" + ddlDíaRangoInicio.SelectedValue.ToString() + "/" + txtAñoRangoInicio.Text + "')";
                            AND = " AND ";
                            break;
                        case ("5")://Nacimiento                  
                            strSql += AND + "(Personas.FE_Nacimiento<='" + ddlMesRangoFin.SelectedValue.ToString() + "/" + ddlDíaRangoFin.SelectedValue.ToString() + "/" + txtAñoRangoFin.Text + "' AND Personas.FE_Nacimiento=>'" + ddlMesRangoInicio.SelectedValue.ToString() + "/" + ddlDíaRangoInicio.SelectedValue.ToString() + "/" + txtAñoRangoInicio.Text + "')";
                            AND = " AND ";
                            break;
                        default:break;
                    }
                }
                else if (this.txtAñoRangoInicio.Text != "")//Solo Inicio
                {
                    switch (this.ddlRangoTipoFecha.SelectedValue.ToString())
                    {
                        case ("1")://Admisión
                            strSql += AND + "(Perfil.IN_TI_Perfil='AD' AND Perfil.FE_Perfil>='" + ddlMesRangoInicio.SelectedValue.ToString() + "/" + ddlDíaRangoInicio.SelectedValue.ToString() + "/" + txtAñoRangoInicio.Text + "')";
                            AND = " AND ";
                            break;
                        case ("2")://Evaluación
                            strSql += AND + "(Perfil.IN_TI_Perfil='EV' AND Perfil.FE_Perfil>='" + ddlMesRangoInicio.SelectedValue.ToString() + "/" + ddlDíaRangoInicio.SelectedValue.ToString() + "/" + txtAñoRangoInicio.Text + "')";
                            AND = " AND ";
                            break;
                        case ("3")://Alta
                            strSql += AND + "(Perfil.IN_TI_Perfil='AL' AND Perfil.FE_Perfil>='" + ddlMesRangoInicio.SelectedValue.ToString() + "/" + ddlDíaRangoInicio.SelectedValue.ToString() + "/" + txtAñoRangoInicio.Text + "')";
                            AND = " AND ";
                            break;
                        case ("4")://Edición
                            strSql += AND + "(Perfil.FE_Edicion>='" + ddlMesRangoInicio.SelectedValue.ToString() + "/" + ddlDíaRangoInicio.SelectedValue.ToString() + "/" + txtAñoRangoInicio.Text + "')";
                            AND = " AND ";
                            break;
                        case ("5")://Nacimiento  
                            strSql += AND + "(Personas.FE_Nacimiento>='" + ddlMesRangoInicio.SelectedValue.ToString() + "/" + ddlDíaRangoInicio.SelectedValue.ToString() + "/" + txtAñoRangoInicio.Text + "')";
                            AND = " AND ";
                            break;
                        default: break;
                    }
                }
                else if (this.txtAñoRangoFin.Text != "")//Solo Fin
                {
                    switch (this.ddlRangoTipoFecha.SelectedValue.ToString())
                    {
                        case ("1")://Admisión
                            strSql += AND + "(Perfil.IN_TI_Perfil='AD' AND Perfil.FE_Perfil<='" + ddlMesRangoInicio.SelectedValue.ToString() + "/" + ddlDíaRangoInicio.SelectedValue.ToString() + "/" + txtAñoRangoInicio.Text + "')";
                            AND = " AND ";
                            break;
                        case ("2")://Evaluación
                            strSql += AND + "(Perfil.IN_TI_Perfil='EV' AND Perfil.FE_Perfil<='" + ddlMesRangoInicio.SelectedValue.ToString() + "/" + ddlDíaRangoInicio.SelectedValue.ToString() + "/" + txtAñoRangoInicio.Text + "')";
                            AND = " AND ";
                            break;
                        case ("3")://Alta
                            strSql += AND + "(Perfil.IN_TI_Perfil='AL' AND Perfil.FE_Perfil<='" + ddlMesRangoInicio.SelectedValue.ToString() + "/" + ddlDíaRangoInicio.SelectedValue.ToString() + "/" + txtAñoRangoInicio.Text + "')";
                            AND = " AND ";
                            break;
                        case ("4")://Edición
                            strSql += AND + "(Perfil.FE_Edicion<='" + ddlMesRangoInicio.SelectedValue.ToString() + "/" + ddlDíaRangoInicio.SelectedValue.ToString() + "/" + txtAñoRangoInicio.Text + "')";
                            AND = " AND ";
                            break;
                        case ("5")://Nacimiento                  
                            strSql += AND + "(Personas.FE_Nacimiento<='" + ddlMesRangoFin.SelectedValue.ToString() + "/" + ddlDíaRangoFin.SelectedValue.ToString() + "/" + txtAñoRangoFin.Text + "')";
                            AND = " AND ";
                            break;
                        default: break;
                    }
                }
            }
            #endregion         
            return strSql;
            
		}

        private bool ValidarFecha( string dia, string mes, string año)
        {
            bool resultado =false;

            try
            {
                DateTime TempDate = new DateTime( Convert.ToInt32( año), Convert.ToInt32( mes), Convert.ToInt32(dia));
                 resultado = true;
            }
            catch (Exception) {
             }

            return resultado;
        }
		private string PrepararPredicado(string Condicion, string Valor)
		{
			switch(Condicion)
			{
				case "Es igual a":
					return "='" + Valor + "'";
				case "Contiene":
                    return "LIKE '%" + Valor + "%'";
				case "Inicia con":
                    return "LIKE '" + Valor + "%'";
				case "Finaliza en":
                    return "LIKE '%" + Valor + "'";	
				case "Es mayor que":
					return ">" + Valor;	
				case "Es menor que":
					return "<" + Valor;
                default: return "";
			}
		}
		protected void btnRegistrar_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("frmEditar.aspx?accion=registrar&fuente=admision");
		}
		protected void btnBorrar_Click(object sender, System.EventArgs e)
		{
			txtIUP.Text = "";
			txtExpediente.Text = "";
			txtNSS.Text = "";
			txtPrimerApellido.Text = "";
			txtSegundoApellido.Text = "";
			txtPrimerNombre.Text = "";
			txtSegundoNombre.Text = "";
			txtEdad.Text = "";
            txtAñoRangoFin.Text = "";
            txtAñoRangoInicio.Text = "";
            txtAñoExacta.Text = "";
            ddlFechasExactas.SelectedIndex = 0;
            ddlRangoTipoFecha.SelectedIndex = 0;
            ddlFiltroDeFecha.SelectedIndex = 0;
            ddlMesExacta.SelectedIndex = 0;
            ddlDíaExacta.SelectedIndex = 0;
            ddlDíaRangoFin.SelectedIndex = 0;
            ddlMesRangoFin.SelectedIndex = 0;
            ddlDíaRangoInicio.SelectedIndex = 0;
            ddlMesRangoInicio.SelectedIndex = 0;
            ddlEstadoEpisodios.SelectedIndex = 0;
			ddlExpediente.SelectedIndex = 0;
			ddlSeguroSocial.SelectedIndex = 0;
			ddlPrimerApellido.SelectedIndex = 0;
			ddlSegundoApellido.SelectedIndex = 0;
			ddlPrimerNombre.SelectedIndex = 0;
			ddlSegundoNombre.SelectedIndex = 0;
			ddlEdad.SelectedIndex = 0;
			ddlSexo.SelectedIndex = 0;
			ddlVeterano.SelectedIndex = 0;
			ddlGrupoEtnico.SelectedIndex = 0;
            ddlPrograma.SelectedIndex = 0;
		}
	}
}
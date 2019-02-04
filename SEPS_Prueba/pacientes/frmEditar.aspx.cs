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
	public partial class frmEditar : System.Web.UI.Page
	{
		protected System.Data.SqlClient.SqlConnection cnn;
		protected System.Data.SqlClient.SqlDataAdapter daLkpPersona;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		protected ASSMCA.pacientes.dsPersona dsPersona;
		protected System.Data.DataView dvwRazasNoSeleccionadas;
		protected System.Data.SqlClient.SqlCommand SPC_PERSONA;
		protected System.Web.UI.WebControls.RequiredFieldValidator frvExpediente;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator1;
		protected System.Data.SqlClient.SqlCommand SPC_RAZA_PERSONA;
		protected System.Data.SqlClient.SqlCommand SPD_RAZAS_PERSONA;
		protected System.Data.SqlClient.SqlCommand SPU_PERSONA;
		private int m_PK_Programa;
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (this.Session["dsSeguridad"] == null)
            {
                this.Response.Redirect("~/Error.aspx?errMsg=sesion");
                return;
            }
            if (this.Session["pk_administracion"].ToString() == "1")
            {
                ddlVeterano.Enabled = false;
                ddlVeterano.SelectedValue = "3";
            }
			this.m_PK_Programa = Convert.ToInt32(this.Session["pk_programa"].ToString());
			this.rvAñoNacimiento.MaximumValue = DateTime.Now.Year.ToString();
            this.rvAñoNacimiento.MinimumValue = DateTime.Now.AddYears(-100).Year.ToString();
            this.rvAñoNacimiento.ErrorMessage = "El año de la fecha de nacimiento tiene que se un entero entre " + DateTime.Now.Year.ToString() + " y " + DateTime.Now.AddYears(-100).Year + ".";
			if( !this.IsPostBack)
			{
				if( Request.QueryString["accion"].ToString() == "registrar" )
				{
					this.btnRegistrar.Visible = true;
					this.btnActualizarPersona.Visible = false;
					this.daLkpPersona.Fill(this.dsPersona);
					this.DataBind();
					Session["dsPersona"] = this.dsPersona;
				}
				else if( Request.QueryString["accion"].ToString() == "editar" )
				{
					this.btnRegistrar.Visible = false;
					this.btnActualizarPersona.Visible = true;
                    this.lTituloPrincipal.Text= "Modificación de paciente";
					this.dsPersona = (ASSMCA.pacientes.dsPersona)Session["dsPersona"];
					this.daLkpPersona.Fill(this.dsPersona);
					this.dvwRazasNoSeleccionadas.Table = this.dsPersona.LKP_Raza;
					this.DataBind();
                    if (this.dsPersona.SA_PERSONA[0] != null)
                    {
                        this.lblIUP.Text = this.dsPersona.SA_PERSONA[0]["PK_Persona"].ToString();
                        this.txtExpediente.Text = this.dsPersona.SA_PERSONA[0]["NR_Expediente"].ToString();
                        this.txtNSS1.Text = this.dsPersona.SA_PERSONA[0]["NR_SeguroSocial"].ToString().Substring(0, 3);
                        this.txtNSS2.Text = this.dsPersona.SA_PERSONA[0]["NR_SeguroSocial"].ToString().Substring(3, 2);
                        this.txtNSS3.Text = this.dsPersona.SA_PERSONA[0]["NR_SeguroSocial"].ToString().Substring(5, 4);
                        this.txtPrimerApellido.Text = this.dsPersona.SA_PERSONA[0]["AP_Primero"].ToString();
                        this.txtSegundoApellido.Text = this.dsPersona.SA_PERSONA[0]["AP_Segundo"].ToString();
                        this.txtPrimerNombre.Text = this.dsPersona.SA_PERSONA[0]["NB_Primero"].ToString();
                        this.txtSegundoNombre.Text = this.dsPersona.SA_PERSONA[0]["NB_Segundo"].ToString();
                        DateTime fe = DateTime.Parse(this.dsPersona.SA_PERSONA[0]["FE_Nacimiento"].ToString());
                        this.ddlMes.SelectedValue = fe.Month.ToString();
                        this.ddlDía.SelectedValue = fe.Day.ToString();
                        this.txtAño.Text = fe.Year.ToString();
                        this.ddlSexo.SelectedValue = this.dsPersona.SA_PERSONA[0]["FK_Sexo"].ToString();
                        this.ddlGrupoEtnico.SelectedValue = this.dsPersona.SA_PERSONA[0]["FK_GrupoEtnico"].ToString();
                        this.ddlVeterano.SelectedValue = this.dsPersona.SA_PERSONA[0]["FK_Veterano"].ToString();
                        this.ActualizarListaRazas();
                    }
                    else
                    {
                        this.Response.Redirect("../pacientes/frmvisualizar.aspx?accion=consultar&pk_persona=" + this.dsPersona.SA_PERSONA[0]["PK_Persona"].ToString());
                    }
				}
			}
			else
			{
				this.dsPersona = (ASSMCA.pacientes.dsPersona)Session["dsPersona"];
				this.dvwRazasNoSeleccionadas.Table = this.dsPersona.LKP_Raza;
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
			this.daLkpPersona = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.cnn = new System.Data.SqlClient.SqlConnection();
			this.dsPersona = new ASSMCA.pacientes.dsPersona();
			this.dvwRazasNoSeleccionadas = new System.Data.DataView();
			this.SPC_PERSONA = new System.Data.SqlClient.SqlCommand();
			this.SPC_RAZA_PERSONA = new System.Data.SqlClient.SqlCommand();
			this.SPD_RAZAS_PERSONA = new System.Data.SqlClient.SqlCommand();
			this.SPU_PERSONA = new System.Data.SqlClient.SqlCommand();
			((System.ComponentModel.ISupportInitialize)(this.dsPersona)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwRazasNoSeleccionadas)).BeginInit();
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
            #region cnn
            this.cnn.ConnectionString = NewSource.connectionString;
            #endregion
            #region dsPersona
			this.dsPersona.DataSetName = "dsPersona";
			this.dsPersona.Locale = new System.Globalization.CultureInfo("en-US");
            #endregion
            #region dvwRazasNoSeleccionadas
			this.dvwRazasNoSeleccionadas.Table = this.dsPersona.LKP_Raza;
            #endregion
            #region SPC_PERSONA
			this.SPC_PERSONA.CommandText = "dbo.[SPC_PERSONA]";
			this.SPC_PERSONA.CommandType = System.Data.CommandType.StoredProcedure;
			this.SPC_PERSONA.Connection = this.cnn;
			this.SPC_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
			this.SPC_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_SeguroSocial", System.Data.SqlDbType.VarChar, 9));
			this.SPC_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Programa", System.Data.SqlDbType.SmallInt, 2));
            this.SPC_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_Expediente", System.Data.SqlDbType.VarChar, 12));
			this.SPC_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Sexo", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AP_Primero", System.Data.SqlDbType.VarChar, 30));
			this.SPC_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AP_Segundo", System.Data.SqlDbType.VarChar, 30));
			this.SPC_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NB_Primero", System.Data.SqlDbType.VarChar, 30));
			this.SPC_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NB_Segundo", System.Data.SqlDbType.VarChar, 30));
			this.SPC_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_Nacimiento", System.Data.SqlDbType.DateTime, 8));
			this.SPC_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Veterano", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_GrupoEtnico", System.Data.SqlDbType.TinyInt, 1));
			this.SPC_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Sesion", System.Data.SqlDbType.UniqueIdentifier, 16));
			this.SPC_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Persona", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            #endregion
            #region SPC_RAZA_PERSONA
			this.SPC_RAZA_PERSONA.CommandText = "dbo.[SPC_RAZA_PERSONA]";
			this.SPC_RAZA_PERSONA.CommandType = System.Data.CommandType.StoredProcedure;
			this.SPC_RAZA_PERSONA.Connection = this.cnn;
			this.SPC_RAZA_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
			this.SPC_RAZA_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Persona", System.Data.SqlDbType.Int, 4));
			this.SPC_RAZA_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Raza", System.Data.SqlDbType.TinyInt, 1));
            #endregion
            #region SPD_RAZAS_PERSONA
			this.SPD_RAZAS_PERSONA.CommandText = "dbo.[SPD_RAZAS_PERSONA]";
			this.SPD_RAZAS_PERSONA.CommandType = System.Data.CommandType.StoredProcedure;
			this.SPD_RAZAS_PERSONA.Connection = this.cnn;
			this.SPD_RAZAS_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
			this.SPD_RAZAS_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Persona", System.Data.SqlDbType.Int, 4));
            #endregion
            #region SPU_PERSONA
			this.SPU_PERSONA.CommandText = "dbo.[SPU_PERSONA]";
			this.SPU_PERSONA.CommandType = System.Data.CommandType.StoredProcedure;
			this.SPU_PERSONA.Connection = this.cnn;
			this.SPU_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
			this.SPU_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Persona", System.Data.SqlDbType.Int, 4));
			this.SPU_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Programa", System.Data.SqlDbType.SmallInt, 2));
            this.SPU_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_Expediente", System.Data.SqlDbType.VarChar, 12));// Cambio: (SqlDbType.Int, 4) Por (SqlDbType.VarChar, 12)
			this.SPU_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NR_SeguroSocial", System.Data.SqlDbType.VarChar, 9));
			this.SPU_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Sexo", System.Data.SqlDbType.TinyInt, 1));
			this.SPU_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AP_Primero", System.Data.SqlDbType.VarChar, 30));
			this.SPU_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AP_Segundo", System.Data.SqlDbType.VarChar, 30));
			this.SPU_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NB_Primero", System.Data.SqlDbType.VarChar, 30));
			this.SPU_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NB_Segundo", System.Data.SqlDbType.VarChar, 30));
			this.SPU_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FE_Nacimiento", System.Data.SqlDbType.DateTime, 8));
			this.SPU_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Veterano", System.Data.SqlDbType.VarChar, 1));
			this.SPU_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_GrupoEtnico", System.Data.SqlDbType.VarChar, 1));
			this.SPU_PERSONA.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FK_Sesion", System.Data.SqlDbType.UniqueIdentifier, 16));
			((System.ComponentModel.ISupportInitialize)(this.dsPersona)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwRazasNoSeleccionadas)).EndInit();
            #endregion
        }
		#endregion
		protected void btnRegistrar_Click(object sender, System.EventArgs e)
		{
			this.lblMensaje.Visible = false;
			int PK_Persona = this.GuardarRegistro();
			if (PK_Persona != 0)
			{
                if (Request.QueryString["fuente"] != null)
                {
                    if (Request.QueryString["fuente"].ToString() == "admision")
                    {
                        Response.Redirect("frmVisualizar.aspx?accion=registrar&fuente=admision&pk_programa=" + this.m_PK_Programa.ToString() + "&pk_persona=" + PK_Persona);
                    }
                }
                else
                {
                    Response.Redirect("frmVisualizar.aspx?accion=registrar&pk_programa=" + this.m_PK_Programa.ToString() + "&pk_persona=" + PK_Persona);
                }
			}
			else
			{
				this.lblMensaje.Visible = true;
			}
		}
		private int GuardarRegistro()
		{
			int PK_Persona;
			try
			{
				this.PrepararComandoCrear();
				this.cnn.Open();
				this.SPC_PERSONA.ExecuteNonQuery();
				PK_Persona = Convert.ToInt32(this.SPC_PERSONA.Parameters["@PK_Persona"].Value.ToString());

				for( int i = 0; i<this.lbxRazaSeleccionadas.Items.Count ; i++ )
				{
					this.SPC_RAZA_PERSONA.Parameters["@FK_Persona"].Value = PK_Persona;
					this.SPC_RAZA_PERSONA.Parameters["@FK_Raza"].Value = this.lbxRazaSeleccionadas.Items[i].Value;
					this.SPC_RAZA_PERSONA.ExecuteNonQuery();
				}
				this.cnn.Close();
				return PK_Persona;
			}
			catch(Exception ex)
			{
				if( this.cnn.State != ConnectionState.Closed )
					this.cnn.Close();
				this.lblMensaje.Text = ex.Message;
				return 0;
			}
		}

		private void PrepararComandoCrear()
		{
			string NSS = this.txtNSS1.Text + this.txtNSS2.Text + this.txtNSS3.Text;

            if (NSS != "")
            {
                this.SPC_PERSONA.Parameters["@NR_SeguroSocial"].Value = NSS;
            }
			this.SPC_PERSONA.Parameters["@PK_Programa"].Value = this.m_PK_Programa;
			this.SPC_PERSONA.Parameters["@NR_Expediente"].Value = this.txtExpediente.Text;
			this.SPC_PERSONA.Parameters["@FK_Sexo"].Value = Convert.ToSByte(this.ddlSexo.SelectedValue.ToString());
			this.SPC_PERSONA.Parameters["@AP_Primero"].Value = this.txtPrimerApellido.Text.Trim();
            if (this.txtSegundoApellido.Text.Trim() != "")
            {
                this.SPC_PERSONA.Parameters["@AP_Segundo"].Value = this.txtSegundoApellido.Text.Trim();
            }
			this.SPC_PERSONA.Parameters["@NB_Primero"].Value = this.txtPrimerNombre.Text.Trim();
            if (this.txtSegundoNombre.Text.Trim() != "")
            {
                this.SPC_PERSONA.Parameters["@NB_Segundo"].Value = this.txtSegundoNombre.Text.Trim();
            }
            string fe = this.ddlMes.SelectedValue.ToString() + "/" + this.ddlDía.SelectedValue.ToString() + "/" + this.txtAño.Text;
			this.SPC_PERSONA.Parameters["@FE_Nacimiento"].Value = DateTime.Parse(fe);
			this.SPC_PERSONA.Parameters["@FK_Veterano"].Value = Convert.ToSByte(this.ddlVeterano.SelectedValue.ToString());
			this.SPC_PERSONA.Parameters["@FK_GrupoEtnico"].Value = Convert.ToSByte(this.ddlGrupoEtnico.SelectedValue.ToString());
			this.SPC_PERSONA.Parameters["@FK_Sesion"].Value = Guid.NewGuid();;
		}
		protected void btnAgregar_Click(object sender, System.EventArgs e)
		{
			if( this.lbxRazaSinSeleccionar.SelectedItem != null )
			{
				System.Web.UI.WebControls.ListItem li = new ListItem(this.lbxRazaSinSeleccionar.SelectedItem.Text, this.lbxRazaSinSeleccionar.SelectedItem.Value);
				this.lbxRazaSeleccionadas.Items.Add(li);
				this.ActualizarListaRazas();
			}
		}
		protected void btnEliminar_Click(object sender, System.EventArgs e)
		{
			if( this.lbxRazaSeleccionadas.SelectedItem != null )
			{
				this.lbxRazaSeleccionadas.Items.Remove(this.lbxRazaSeleccionadas.SelectedItem);
				this.ActualizarListaRazas();
			}
		}
		private void ActualizarListaRazas()
		{
			string Filtro = "";
			for (int i = 0; i<this.lbxRazaSeleccionadas.Items.Count; i++)
			{
                if (i == 0)
                {
                    Filtro += "PK_Raza <> " + this.lbxRazaSeleccionadas.Items[i].Value;
                }
                else
                {
                    Filtro += " AND PK_Raza <> " + this.lbxRazaSeleccionadas.Items[i].Value;
                }
			}
			this.dvwRazasNoSeleccionadas.RowFilter = Filtro;
			this.lbxRazaSinSeleccionar.DataBind();
		}

		protected void btnActualizarPersona_Click(object sender, System.EventArgs e)
		{
			int PK_Persona = this.GuardarCambios();
            if (Request.QueryString["fuente"] != null)
            {
                if (Request.QueryString["fuente"].ToString() == "admision")
                {
                    Response.Redirect("frmVisualizar.aspx?accion=consultar&fuente=admision&pk_programa=" + this.m_PK_Programa.ToString() + "&pk_persona=" + PK_Persona);
                }
            }
            else
            {
                Response.Redirect("frmVisualizar.aspx?accion=consultar&pk_programa=" + this.m_PK_Programa.ToString() + "&pk_persona=" + PK_Persona);
            }
		}
		private int GuardarCambios()
		{
			int PK_Persona;
			try
			{
				this.PrepararComandoEdicion();
				this.cnn.Open();
				this.SPU_PERSONA.ExecuteNonQuery();
				PK_Persona = Convert.ToInt32(this.dsPersona.SA_PERSONA[0]["PK_Persona"].ToString());
				this.SPD_RAZAS_PERSONA.Parameters["@FK_Persona"].Value = PK_Persona;
				this.SPD_RAZAS_PERSONA.ExecuteNonQuery();
				for( int i = 0; i<this.lbxRazaSeleccionadas.Items.Count ; i++ )
				{
					this.SPC_RAZA_PERSONA.Parameters["@FK_Persona"].Value = PK_Persona;
					this.SPC_RAZA_PERSONA.Parameters["@FK_Raza"].Value = this.lbxRazaSeleccionadas.Items[i].Value;
					this.SPC_RAZA_PERSONA.ExecuteNonQuery();
				}
				this.cnn.Close();
				return PK_Persona;
			}
			catch(Exception ex)
			{
				string m = ex.Message;
				return 0;
			}
		}
		private void PrepararComandoEdicion()
		{
			string NSS = this.txtNSS1.Text + this.txtNSS2.Text + this.txtNSS3.Text;
			this.SPU_PERSONA.Parameters["@PK_Persona"].Value = Convert.ToInt32(this.dsPersona.SA_PERSONA[0]["PK_Persona"].ToString());
            if (NSS != "")
            {
                this.SPU_PERSONA.Parameters["@NR_SeguroSocial"].Value = NSS;
            }
			this.SPU_PERSONA.Parameters["@FK_Programa"].Value = this.m_PK_Programa;
			this.SPU_PERSONA.Parameters["@NR_Expediente"].Value = this.txtExpediente.Text;
			this.SPU_PERSONA.Parameters["@FK_Sexo"].Value = Convert.ToSByte(this.ddlSexo.SelectedValue.ToString());
			this.SPU_PERSONA.Parameters["@AP_Primero"].Value = this.txtPrimerApellido.Text.Trim();
            if (this.txtSegundoApellido.Text.Trim() != "")
            {
                this.SPU_PERSONA.Parameters["@AP_Segundo"].Value = this.txtSegundoApellido.Text.Trim();
            }
			this.SPU_PERSONA.Parameters["@NB_Primero"].Value = this.txtPrimerNombre.Text.Trim();
            if (this.txtSegundoNombre.Text.Trim() != "")
            {
                this.SPU_PERSONA.Parameters["@NB_Segundo"].Value = this.txtSegundoNombre.Text.Trim();
            }
            string fe = this.ddlMes.SelectedValue.ToString() + "/" + this.ddlDía.SelectedValue.ToString() + "/" + this.txtAño.Text;
			this.SPU_PERSONA.Parameters["@FE_Nacimiento"].Value = DateTime.Parse(fe);
			this.SPU_PERSONA.Parameters["@FK_Veterano"].Value = Convert.ToSByte(this.ddlVeterano.SelectedValue.ToString());
			this.SPU_PERSONA.Parameters["@FK_GrupoEtnico"].Value = Convert.ToSByte(this.ddlGrupoEtnico.SelectedValue.ToString());
			this.SPU_PERSONA.Parameters["@FK_Sesion"].Value = Guid.NewGuid();;			
		}
	}
}
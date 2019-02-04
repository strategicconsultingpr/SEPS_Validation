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
using System.Data.SqlClient;
namespace ASSMCA.Pacientes
{
	public partial class frmVisualizar : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ListBox ListBox1, ListBox2;
		protected System.Data.SqlClient.SqlDataAdapter daPersona, daEpisodios;
		protected System.Data.SqlClient.SqlConnection cnn;
		protected ASSMCA.pacientes.dsPersona dsPersona;
		protected System.Data.DataView dvwEpisodios;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1, sqlSelectCommand2;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( this.Session["dsSeguridad"] == null )
			{
                this.Response.Redirect("~/Error.aspx?errMsg=sesion");
				return;
			}
			if(!this.IsPostBack)
			{
				int PK_Persona = Convert.ToInt32(Request.QueryString["pk_persona"].ToString());
				int PK_Programa = Convert.ToInt32(this.Session["pk_programa"].ToString());
				this.daPersona.SelectCommand.Parameters["@PK_Persona"].Value = PK_Persona;
				this.daPersona.SelectCommand.Parameters["@PK_Programa"].Value = PK_Programa;
				this.daPersona.SelectCommand.Parameters["@IN_Raza"].Value = true;
				this.dsPersona.SA_PERSONA.Rows.Clear();
				this.daPersona.Fill(this.dsPersona);
				this.DataBind();
				this.Session["dsPersona"] = this.dsPersona;
				this.dgEpisodios.Visible = false;
                this.SetRazas();
				if (Request.QueryString["fuente"] != null )
				{
					if (Request.QueryString["accion"].ToString() == "consultar")
					{
						this.lblMensaje.Visible = false;
						this.btnModificar.Visible = false;
						this.btnRegresar.Visible = false;
					}
					else
					{
						this.btnRegistrar.Visible = true;
						this.btnModificar.Visible = false;
						this.btnRegresar.Visible = false;
					}
					if (Request.QueryString["fuente"].ToString() == "admision")
					{
                        if (!IsDead(PK_Persona.ToString()) && !HasProfileOpenInResidentialOrHospitalization(PK_Persona.ToString()) && !CheckIfEpisodeWithSameProgramOpenExists(PK_Programa)&&GenderTest(PK_Programa))
                        {
                            this.btnRegistrar.Visible = true;
                        }
						HyperLinkColumn hlc = (HyperLinkColumn)this.dgEpisodios.Columns[0];
						hlc.DataNavigateUrlFormatString = "../Episodios/frmVisualizar.aspx?pk_episodio={0}&fuente=admision";
					}
					else if(Request.QueryString["fuente"].ToString() == "evaluacion")
					{
						HyperLinkColumn hlc = (HyperLinkColumn)this.dgEpisodios.Columns[0];
						hlc.DataNavigateUrlFormatString = "../Episodios/frmVisualizar.aspx?pk_episodio={0}&fuente=evaluacion";
					}
					else if(Request.QueryString["fuente"].ToString() == "alta")
					{
						HyperLinkColumn hlc = (HyperLinkColumn)this.dgEpisodios.Columns[0];
						hlc.DataNavigateUrlFormatString = "../Episodios/frmVisualizar.aspx?pk_episodio={0}&fuente=alta";
					}				
				}
				else if (Request.QueryString["accion"].ToString() == "registrar" )
				{
					this.btnRegistrar.Visible = false;
					this.btnModificar.Visible = false;
					this.btnRegresar.Visible = true;
					this.lblMsgGrid.Text = "La persona no posee episodios registrados.";
				}
				else if (Request.QueryString["accion"].ToString() == "consultar" )
				{
					this.lblMensaje.Visible = false;
					this.btnRegistrar.Visible = false;
					this.btnModificar.Visible = true;
					this.btnRegresar.Visible = false;
				}
			}
			else
			{
				this.dsPersona = (ASSMCA.pacientes.dsPersona)this.Session["dsPersona"];
			}
            this.VerTodosEpisodios();
		}
        private bool HasProfileToday(string persona)
        {
            bool hasProfile = true;
            string sql = "EXEC [dbo].[SPR_IsThereProfile] @ThereIsProfile = @ThereIsProfile OUTPUT, @Persona = @Persona, @TipoPerfil = @TipoPerfil, @FechaPerfil = @FechaPerfil SELECT	@ThereIsProfile as N'@ThereIsProfile'";
            using (SqlConnection conn = new SqlConnection(NewSource.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@Persona", SqlDbType.Int);
                cmd.Parameters["@Persona"].Value = persona;
                cmd.Parameters.Add("@TipoPerfil", SqlDbType.VarChar);
                cmd.Parameters["@TipoPerfil"].Value = "AD";
                cmd.Parameters.Add("@FechaPerfil", SqlDbType.DateTime);
                cmd.Parameters["@FechaPerfil"].Value = DateTime.Now.ToString("yyyy-M-d");
                SqlParameter output = new SqlParameter("@ThereIsProfile", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    hasProfile = (int)output.Value > 0;
                    conn.Close();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return hasProfile;
        }
        private bool HasProfileOpenInResidentialOrHospitalization(string persona)
        {/*
            bool hasProfileOpenInResidentialOrHospitalization = true;
            string sql = " EXEC [dbo].[SPR_HasHospitalizationOrResidentialOpen] @HasHospitalizationOrResidentialOpen = @HasHospitalizationOrResidentialOpen OUTPUT, @Persona = @Persona SELECT	@HasHospitalizationOrResidentialOpen as N'@HasHospitalizationOrResidentialOpen'";
            using (SqlConnection conn = new SqlConnection(NewSource.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@Persona", SqlDbType.Int);
                cmd.Parameters["@Persona"].Value = persona;
                SqlParameter output = new SqlParameter("@HasHospitalizationOrResidentialOpen", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    hasProfileOpenInResidentialOrHospitalization = (int)output.Value > 0;
                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return hasProfileOpenInResidentialOrHospitalization;
          * */
            return false;
        }
        private bool IsDead(string persona)
        {
            bool isDead = true;
            string sql = " EXEC [dbo].[SPR_IsPersonaDead] @deaths = @deaths OUTPUT, @Persona = @Persona SELECT	@deaths as N'@deaths'";
            using (SqlConnection conn = new SqlConnection(NewSource.connectionString)) 
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@Persona", SqlDbType.Int);
                cmd.Parameters["@Persona"].Value = persona;
                SqlParameter output = new SqlParameter("@deaths", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    isDead = (int)output.Value > 0;
                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return isDead;
        }
        private void SetRazas()
        {
            if (dsPersona.SA_RAZA_PERSONA.Count == 0)
            {
                lblRazas.Text = "No hay información de razas para esta persona.";
            }
            for (int i = 0; i < dsPersona.SA_RAZA_PERSONA.Count; i++)
            {
                if(i!=(dsPersona.SA_RAZA_PERSONA.Count-1))
                {
                    lblRazas.Text += dsPersona.SA_RAZA_PERSONA[i]["DE_Raza"].ToString()+", ";
                }
                else
                {
                    lblRazas.Text += dsPersona.SA_RAZA_PERSONA[i]["DE_Raza"].ToString() + ".";
                }
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
			this.daPersona = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.cnn = new System.Data.SqlClient.SqlConnection();
			this.dsPersona = new ASSMCA.pacientes.dsPersona();
			this.daEpisodios = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
			this.dvwEpisodios = new System.Data.DataView();
			((System.ComponentModel.ISupportInitialize)(this.dsPersona)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwEpisodios)).BeginInit();
			this.dgEpisodios.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgEpisodios_SortCommand);
			this.dgEpisodios.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgEpisodios_ItemDataBound);
            #region daPersona
			this.daPersona.SelectCommand = this.sqlSelectCommand1;
			this.daPersona.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
                new System.Data.Common.DataTableMapping("Table", "SA_PERSONA", new System.Data.Common.DataColumnMapping[] {
	                new System.Data.Common.DataColumnMapping("PK_Persona", "PK_Persona"),
	                new System.Data.Common.DataColumnMapping("NR_SeguroSocial", "NR_SeguroSocial"),
	                new System.Data.Common.DataColumnMapping("FK_Sexo", "FK_Sexo"),
	                new System.Data.Common.DataColumnMapping("DE_Sexo", "DE_Sexo"),
	                new System.Data.Common.DataColumnMapping("AP_Primero", "AP_Primero"),
	                new System.Data.Common.DataColumnMapping("AP_Segundo", "AP_Segundo"),
	                new System.Data.Common.DataColumnMapping("NB_Primero", "NB_Primero"),
	                new System.Data.Common.DataColumnMapping("NB_Segundo", "NB_Segundo"),
	                new System.Data.Common.DataColumnMapping("FE_Nacimiento", "FE_Nacimiento"),
	                new System.Data.Common.DataColumnMapping("NR_Edad", "NR_Edad"),
	                new System.Data.Common.DataColumnMapping("FK_Veterano", "FK_Veterano"),
	                new System.Data.Common.DataColumnMapping("DE_Veterano", "DE_Veterano"),
	                new System.Data.Common.DataColumnMapping("FK_GrupoEtnico", "FK_GrupoEtnico"),
	                new System.Data.Common.DataColumnMapping("DE_GrupoEtnico", "DE_GrupoEtnico"),
	                new System.Data.Common.DataColumnMapping("FK_Sesion", "FK_Sesion"),
	                new System.Data.Common.DataColumnMapping("TI_Edicion", "TI_Edicion"),
	                new System.Data.Common.DataColumnMapping("FE_Edicion", "FE_Edicion"),
	                new System.Data.Common.DataColumnMapping("NR_Expediente", "NR_Expediente")}),
                new System.Data.Common.DataTableMapping("Table1", "SA_RAZA_PERSONA", new System.Data.Common.DataColumnMapping[] {
	                new System.Data.Common.DataColumnMapping("FK_Raza", "FK_Raza"),
	                new System.Data.Common.DataColumnMapping("DE_Raza", "DE_Raza")})});
            #endregion
            #region sqlSelectCommand1
			this.sqlSelectCommand1.CommandText = "[SPR_PERSONA]";
			this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
			this.sqlSelectCommand1.Connection = this.cnn;
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Persona", System.Data.SqlDbType.Int, 4));
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Programa", System.Data.SqlDbType.TinyInt, 1));
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Raza", System.Data.SqlDbType.Bit, 1));
            #endregion
            #region cnn
            this.cnn.ConnectionString = NewSource.connectionString;
            #endregion
            #region dsPersona
			this.dsPersona.DataSetName = "dsPersona";
			this.dsPersona.Locale = new System.Globalization.CultureInfo("en-US");
            #endregion
            #region daEpisodios
			this.daEpisodios.SelectCommand = this.sqlSelectCommand2;
			this.daEpisodios.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
                new System.Data.Common.DataTableMapping("Table", "SA_EPISODIOS", new System.Data.Common.DataColumnMapping[] {
	                new System.Data.Common.DataColumnMapping("PK_Episodio", "PK_Episodio"),
	                new System.Data.Common.DataColumnMapping("FK_Persona", "FK_Persona"),
	                new System.Data.Common.DataColumnMapping("FK_Programa", "FK_Programa"),
	                new System.Data.Common.DataColumnMapping("NB_Programa", "NB_Programa"),
	                new System.Data.Common.DataColumnMapping("FE_Episodio", "FE_Episodio"),
	                new System.Data.Common.DataColumnMapping("ES_Episodio", "ES_Episodio"),
	                new System.Data.Common.DataColumnMapping("DE_ES_Episodio", "DE_ES_Episodio"),
	                new System.Data.Common.DataColumnMapping("IN_Metadona", "IN_Metadona"),
	                new System.Data.Common.DataColumnMapping("DE_Metadona", "DE_Metadona"),
	                new System.Data.Common.DataColumnMapping("NR_Perfiles", "NR_Perfiles")})});
            #endregion
            #region sqlSelectCommand2
			this.sqlSelectCommand2.CommandText = "[SPR_EPISODIOS]";
			this.sqlSelectCommand2.CommandType = System.Data.CommandType.StoredProcedure;
			this.sqlSelectCommand2.Connection = this.cnn;
			this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
			this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Persona", System.Data.SqlDbType.Int, 4));
            #endregion
            #region dvwEpisodios
			this.dvwEpisodios.Table = this.dsPersona.SA_EPISODIOS;
			((System.ComponentModel.ISupportInitialize)(this.dsPersona)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwEpisodios)).EndInit();
            #endregion
        }
		#endregion
		protected void btnRegistrar_Click(object sender, System.EventArgs e)
		{
			if (Request.QueryString["fuente"] != null )
			{
				if (Request.QueryString["fuente"].ToString() == "admision")
				{
                   this.Session["Tipo_Perfil"] = "admision";
					Response.Redirect("../Perfiles/frmAdmision.aspx?accion=create&pk_persona=" + this.lblIUP.Text );
				}
			}			
		}
		protected void btnRegresar_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("../frmHome.aspx");
		}

		protected void btnModificar_Click(object sender, System.EventArgs e)
		{
			Session["dsPersona"] = this.dsPersona;
			Response.Redirect("frmEditar.aspx?accion=editar");
		}
        protected bool CheckIfEpisodeWithSameProgramOpenExists(int pk_programa)
        {
            this.dsPersona = (ASSMCA.pacientes.dsPersona)this.Session["dsPersona"];
            if (this.dsPersona.SA_EPISODIOS.Rows.Count == 0)
            {
                this.daEpisodios.SelectCommand.Parameters["@PK_Persona"].Value = Convert.ToInt32(this.dsPersona.SA_PERSONA[0]["PK_Persona"].ToString());
                this.daEpisodios.Fill(this.dsPersona);
            }
            this.dvwEpisodios.Table = this.dsPersona.SA_EPISODIOS;
            this.dvwEpisodios.RowFilter = "( ES_Episodio IS NULL OR ES_Episodio = 0 ) AND FK_Programa = " + pk_programa;
            this.Session["dsPersona"] = this.dsPersona;
            if (this.dvwEpisodios.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool GenderTest(int PK_Programa)
        {
            try
            {
                bool result = true;
                Sexo sexo = (Sexo)Convert.ToInt32(this.dsPersona.SA_PERSONA[0]["FK_Sexo"].ToString());
                PKPrograma programa = (PKPrograma)PK_Programa;
                switch (sexo)
                {
                    case (Sexo.Masculino):
                    case (Sexo.TransgéneroFM):
                        switch (programa)
                        {
                            case (PKPrograma.RESIDENCIAL_DE_MUJERES_SAN_JUAN): result = false; break;
                            default: break;
                        }
                        break;
                    case (Sexo.Femenino):
                    case (Sexo.TransgéneroMF):
                        switch (programa)
                        {
                            case (PKPrograma.RESIDENCIAL_DE_VARONES_DE_PONCE):
                            case (PKPrograma.RESIDENCIAL_DE_VARONES_DE_SAN_JUAN):
                            case (PKPrograma.HOSPITAL_DE_PSIQUIATRÍA_FORENSE_DE_SAN_JUAN):
                            case (PKPrograma.CENTRO_RESIDENCIAL_DEAMBULANTES_VARONES_BAYAMÓN): result = false; break;
                            default: break;
                        }
                        break;
                    default: break;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		protected void VerTodosEpisodios()
		{
			this.dsPersona = (ASSMCA.pacientes.dsPersona)this.Session["dsPersona"];
			if(this.dsPersona.SA_EPISODIOS.Rows.Count == 0)
			{
				this.daEpisodios.SelectCommand.Parameters["@PK_Persona"].Value = Convert.ToInt32(this.dsPersona.SA_PERSONA[0]["PK_Persona"].ToString());
				this.daEpisodios.Fill(this.dsPersona);
			}
			this.dvwEpisodios.Table = this.dsPersona.SA_EPISODIOS;
			this.dvwEpisodios.RowFilter = "";
			this.dgEpisodios.DataSource = this.dvwEpisodios;
			this.dgEpisodios.DataBind();
			this.Session["dsPersona"] = this.dsPersona;
			if( this.dvwEpisodios.Count > 0 )
			{
				this.dgEpisodios.Visible = true;
				this.lblMsgGrid.Visible = false;
			}
			else
			{
				this.dgEpisodios.Visible = false;
				this.lblMsgGrid.Visible = true;
				this.lblMsgGrid.Text = "La persona no tiene ningún tipo de episodio registrado en el sistema.";
			}
		}
		private void dgEpisodios_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				if( e.Item.Cells[1].Text != this.Session["PK_Programa"].ToString())
				{
					HyperLink hl = (HyperLink)e.Item.Cells[0].Controls[0];
                    hl.ForeColor = Color.Gray;
                    hl.Enabled = false;
				}
			}
		}
		private void dgEpisodios_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			this.dvwEpisodios.Table = this.dsPersona.SA_EPISODIOS;
			this.dvwEpisodios.Sort = e.SortExpression;
			this.dgEpisodios.DataSource = this.dvwEpisodios;
			this.dgEpisodios.DataBind();
		}
    }
}

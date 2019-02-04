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
namespace ASSMCA.Episodios
{
	public partial class frmVisualizar : System.Web.UI.Page
	{
		protected System.Data.SqlClient.SqlDataAdapter daPerfiles;
		protected System.Data.SqlClient.SqlConnection cnn;
		protected ASSMCA.pacientes.dsPersona dsPersona;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		protected System.Data.DataView dvwEpisodio;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( this.Session["dsSeguridad"] == null )
			{
                this.Response.Redirect("~/Error.aspx?errMsg=sesion");
				return;
			}
			int PK_Episodio = Convert.ToInt32(this.Request.QueryString["pk_episodio"].ToString());
			this.dsPersona = (ASSMCA.pacientes.dsPersona)this.Session["dsPersona"];
			this.dvwEpisodio.Table = this.dsPersona.SA_EPISODIOS;
			this.dvwEpisodio.RowFilter = "PK_Episodio =" + PK_Episodio.ToString();
			this.dsPersona.SA_PERFILES.Rows.Clear();
			this.daPerfiles.SelectCommand.Parameters["@PK_Episodio"].Value = PK_Episodio;
			this.daPerfiles.Fill(this.dsPersona);
			this.DataBind();
			if(Request.QueryString["fuente"] != null)
			{
                //NOTE::[Evaluacion ley 22 - (Presentencia -> Admisi'on)] -- [Alcoholismo Amb. - (Admisi'ones <-- Charlas --> Altas)]
                bool esProgramaDeEvaluacionLey22 = EsProgramaDeEvaluacionLey22((PKPrograma)(Convert.ToInt32(Session["pk_programa"].ToString())));
                if (Request.QueryString["fuente"].ToString() == "evaluacion" && this.lblEstado.Text == "Abierto")
                {
                    if (esProgramaDeEvaluacionLey22)
                    {
                        this.btnEvaluacion.Visible = false;
                    }
                    else
                    {
                        this.btnEvaluacion.Visible = true;
                    }
                }
                else
                {
                    this.btnEvaluacion.Visible = false;
                }
                if (Request.QueryString["fuente"].ToString() == "alta" && this.lblEstado.Text == "Abierto")
                {
                    if (esProgramaDeEvaluacionLey22)
                    {
                        this.btnAlta.Visible = false;
                        this.btnEvaluacion.Visible = false;
                    }
                    else 
                    {
                        this.btnAlta.Visible = true;
                    }
                }
                else
                {
                    this.btnAlta.Visible = false;
                }
			}
			else
			{
				this.btnAlta.Visible = false;
				this.btnEvaluacion.Visible = false;
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
			this.daPerfiles = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.cnn = new System.Data.SqlClient.SqlConnection();
            this.dsPersona = new ASSMCA.pacientes.dsPersona();
			this.dvwEpisodio = new System.Data.DataView();
			((System.ComponentModel.ISupportInitialize)(this.dsPersona)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwEpisodio)).BeginInit();
            #region daPerfiles
            this.daPerfiles.SelectCommand = this.sqlSelectCommand1;
			this.daPerfiles.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
				new System.Data.Common.DataTableMapping("Table", "SA_PERFILES", new System.Data.Common.DataColumnMapping[] {
				    new System.Data.Common.DataColumnMapping("PK_NR_Perfil", "PK_NR_Perfil"),
				    new System.Data.Common.DataColumnMapping("FE_Perfil", "FE_Perfil"),
				    new System.Data.Common.DataColumnMapping("TI_Perfil", "TI_Perfil"),
				    new System.Data.Common.DataColumnMapping("URL", "URL")})});
            #endregion
            #region sqlSelectCommand1 
			this.sqlSelectCommand1.CommandText = "[SPR_PERFILES]";
			this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
			this.sqlSelectCommand1.Connection = this.cnn;
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Episodio", System.Data.SqlDbType.Int, 4));
            #endregion
            #region cnn
            this.cnn.ConnectionString = NewSource.connectionString;
            #endregion
            #region dsPersona
			this.dsPersona.DataSetName = "dsPersona";
			this.dsPersona.Locale = new System.Globalization.CultureInfo("en-US");
            #endregion
            #region dvwEpisodio
			this.dvwEpisodio.Table = this.dsPersona.SA_EPISODIOS;
			((System.ComponentModel.ISupportInitialize)(this.dsPersona)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwEpisodio)).EndInit();
            #endregion
        }
		#endregion
        private bool IsThereAnotherDischargeToday(string persona)
        {
            bool hasProfile = true;
            string sql = "EXEC [dbo].[SPR_IsThereProfile] @ThereIsProfile = @ThereIsProfile OUTPUT, @Persona = @Persona, @TipoPerfil = @TipoPerfil, @FechaPerfil = @FechaPerfil SELECT	@ThereIsProfile as N'@ThereIsProfile'";
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(NewSource.connectionString))
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.Add("@Persona", SqlDbType.Int);
                cmd.Parameters["@Persona"].Value = persona;
                cmd.Parameters.Add("@TipoPerfil", SqlDbType.VarChar);
                cmd.Parameters["@TipoPerfil"].Value = "AL";
                cmd.Parameters.Add("@FechaPerfil", SqlDbType.DateTime);
                cmd.Parameters["@FechaPerfil"].Value = DateTime.Now.ToString("yyyy-M-d");
                System.Data.SqlClient.SqlParameter output = new System.Data.SqlClient.SqlParameter("@ThereIsProfile", SqlDbType.Int);
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
                    Console.WriteLine(ex.Message);
                }
            }
            return hasProfile;
        }
		protected void btnEvaluacion_Click(object sender, System.EventArgs e)
		{
            Session["Tipo_Perfil"] = "Evaluacion";
            Session["FechaAdmision"] = this.dvwEpisodio[0]["FE_Episodio"].ToString();//.SA_EPISODIO[0]["FE_Episodio"].ToString(); 
            this.Response.Redirect("../Perfiles/frmEvaluacion.aspx?accion=create&pk_episodio=" + this.dvwEpisodio[0]["PK_Episodio"].ToString() + "&pk_persona=" + this.dvwEpisodio[0]["FK_Persona"].ToString());
        }
		protected void btnAlta_Click(object sender, System.EventArgs e)
		{
            Session["Tipo_Perfil"] = "Alta";
			this.Response.Redirect("../Perfiles/frmAlta.aspx?accion=create&pk_episodio=" + this.dvwEpisodio[0]["PK_Episodio"].ToString() + "&pk_persona=" + this.dvwEpisodio[0]["FK_Persona"].ToString());
        }
        private bool EsProgramaDeEvaluacionLey22(PKPrograma programa) 
        { 
            bool esProgramaDeEvaluacionLey22 = false;
            switch (programa) { 
                case PKPrograma.EVALUACIÓN_LEY_22_DE_SAN_JUAN:      // PK_Programa = 61
                case PKPrograma.EVALUACIÓN_LEY_22_DE_PONCE:         // PK_Programa = 62
                case PKPrograma.EVALUACIÓN_LEY_22_DE_MAYAGUEZ:      // PK_Programa = 63
                case PKPrograma.EVALUACIÓN_LEY_22_DE_ARECIBO:       // PK_Programa = 64
                case PKPrograma.EVALUACIÓN_LEY_22_MOCA:             // PK_Programa = 65
                case PKPrograma.EVALUACIÓN_LEY_22_DE_GUAYAMA:       // PK_Programa = 66
                    esProgramaDeEvaluacionLey22 = true; break;
                default: break;
            }
            return esProgramaDeEvaluacionLey22;
        }
	}
}
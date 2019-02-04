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

namespace ASSMCA
{
    public partial class PasswordChangeMandatory : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.TextBox TextBox1;
        protected System.Data.SqlClient.SqlConnection cnn;
        protected System.Data.SqlClient.SqlCommand SPU_USUARIO_PASSWORD;
        protected ASSMCA.dsSeguridad dsSeguridad;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (this.Session["dsSeguridad"] == null)
            {
                this.Response.Redirect("frmLogon.aspx");
                return;
            }
            this.dsSeguridad = (dsSeguridad)this.Session["dsSeguridad"];
            if (!this.IsPostBack)
            {
                this.DataBind();
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
            this.SPU_USUARIO_PASSWORD = new System.Data.SqlClient.SqlCommand();
            this.dsSeguridad = new ASSMCA.dsSeguridad();
            ((System.ComponentModel.ISupportInitialize)(this.dsSeguridad)).BeginInit();
            #region cnn
            this.cnn.ConnectionString = NewSource.connectionString;
            #endregion
            #region SPU_USUARIO_PASSWORD
            this.SPU_USUARIO_PASSWORD.CommandText = "dbo.[SPU_USUARIO_PASSWORD]";
            this.SPU_USUARIO_PASSWORD.CommandType = System.Data.CommandType.StoredProcedure;
            this.SPU_USUARIO_PASSWORD.Connection = this.cnn;
            this.SPU_USUARIO_PASSWORD.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            this.SPU_USUARIO_PASSWORD.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Usuario", System.Data.SqlDbType.Int, 4));
            this.SPU_USUARIO_PASSWORD.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PASSWORD_VIEJO", System.Data.SqlDbType.VarChar, 10));
            this.SPU_USUARIO_PASSWORD.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PASSWORD_NUEVO", System.Data.SqlDbType.VarChar, 10));
            #endregion
            #region dsSeguridad
            this.dsSeguridad.DataSetName = "dsSeguridad";
            this.dsSeguridad.Locale = new System.Globalization.CultureInfo("en-US");
            ((System.ComponentModel.ISupportInitialize)(this.dsSeguridad)).EndInit();
            #endregion
        }
        #endregion
        protected void btnPassword_Click(object sender, System.EventArgs e)
        {
            PasswordChangeMandatoryOptions pcmo;
			try
			{
				this.SPU_USUARIO_PASSWORD.Parameters["@PK_Usuario"].Value = Convert.ToInt32(this.dsSeguridad.SA_USUARIO[0]["PK_Usuario"].ToString());
				this.SPU_USUARIO_PASSWORD.Parameters["@PASSWORD_VIEJO"].Value = this.txtPasswordAnterior.Text.Trim();
				this.SPU_USUARIO_PASSWORD.Parameters["@PASSWORD_NUEVO"].Value = this.txtPasswordNuevo.Text.Trim();
				this.cnn.Open();
				this.SPU_USUARIO_PASSWORD.ExecuteNonQuery();
				this.cnn.Close();
                if (this.dsSeguridad.SA_USUARIO.Rows.Count == 1)
                {
                    this.Session["pk_administracion"] = this.dsSeguridad.SA_USUARIO.Rows[0]["PK_Administracion"].ToString();
                    this.Session["nb_administracion"] = this.dsSeguridad.SA_USUARIO.Rows[0]["NB_Administracion"].ToString();
                    this.Session["pk_programa"] = this.dsSeguridad.SA_USUARIO.Rows[0]["PK_Programa"].ToString();
                    this.Session["nb_programa"] = this.dsSeguridad.SA_USUARIO.Rows[0]["NB_Programa"].ToString();
                    this.Session["nr_rowIndex_dsSeguridad"] = "0";
                    this.Session["usuarioPrograma"] = "1";
                    pcmo = PasswordChangeMandatoryOptions.Home;
                }
                else 
                {
                    pcmo = PasswordChangeMandatoryOptions.SelectProgram;
                    
                }
			}
            catch(Exception ex)
            {
                this.cnn.Close();
                pcmo=PasswordChangeMandatoryOptions.Error;
                errorMessage.Text = ex.Message;
                errorMessage.ForeColor = Color.Red;
            }
            switch (pcmo)
            {
                case(PasswordChangeMandatoryOptions.Home):
                    this.Response.Redirect("frmHome.aspx");
                    break;
                case(PasswordChangeMandatoryOptions.SelectProgram):
                    this.Response.Redirect(ResolveClientUrl("frmLogon.aspx?changeProg=yes"));
                    break;
                default: break;
            }
		}
    }
}
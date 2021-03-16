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
    public partial class frmLogon : System.Web.UI.Page
    {
        protected System.Data.SqlClient.SqlDataAdapter daSesion;
        protected System.Data.SqlClient.SqlConnection cnn;
        protected ASSMCA.dsSeguridad dsSeguridad;
        protected System.Data.SqlClient.SqlCommand SPD_SESION;
        protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request.QueryString["changeProg"] == "yes")
                {
                    this.dsSeguridad = (ASSMCA.dsSeguridad)Session["dsSeguridad"];
                    ddlPrograma.DataSource = this.dsSeguridad.SA_USUARIO;                  
                    this.ddlPrograma.DataBind();
                    lblTotalPrograma.Text = "Total: " + ddlPrograma.Items.Count;
                    this.LoginBox.Visible = false;
                    this.divMsg1.Visible = false;
                    this.divPrograma.Visible = true;
                    this.ddlPrograma.Visible = true;
                    this.btnAutenticarPrograma.Visible = true;
                    this.btnLogin.Visible = false;
                    return;
                }
                this.txtPassword.Visible = true;
                this.rfvPassword.Enabled = true;
                this.ddlPrograma.Visible = false;
                this.btnAutenticarPrograma.Visible = false;
                this.btnLogin.Visible = true;
                this.divPrograma.Visible = false;
                if (this.Request.QueryString["logout"] != null)
                {
                    try
                    {
                        this.SPD_SESION.Parameters["@PK_Sesion"].Value = new Guid(this.Session["pk_sesion"].ToString());
                        this.cnn.Open();
                        this.SPD_SESION.ExecuteNonQuery();
                        this.cnn.Close();
                    }
                    catch (Exception ex)
                    {
                        string m = ex.Message;
                        Trace.Warn("Page_Load()", m, ex);

                        if (this.cnn.State != ConnectionState.Closed)
                        {
                            this.cnn.Close();
                        }
                    }
                }
                this.Session["dsSeguridad"] = null;
                this.Session["pk_administracion"] = null;
                this.Session["nb_administracion"] = null;
                this.Session["pk_programa"] = null;
                this.Session["nb_programa"] = null;
                this.Session["pk_sesion"] = null;
                this.Session["nr_rowIndex_dsSeguridad"] = null;
                this.Session["co_tipo"] = null;
            }
            else
            {
                if (this.Session["dsSeguridad"] != null)
                {
                    this.dsSeguridad = (dsSeguridad)this.Session["dsSeguridad"];
                }
                if (this.dsSeguridad.SA_USUARIO.Rows.Count < 1)
                {
                    this.txtPassword.Visible = true;
                    this.rfvPassword.Enabled = true;
                    this.ddlPrograma.Visible = false;
                    this.btnAutenticarPrograma.Visible = false;
                    this.btnLogin.Visible = true;
                }
                else
                {
                    this.lblMsg.Visible = false;
                    this.txtPassword.Visible = false;
                    this.rfvPassword.Enabled = false;
                    this.ddlPrograma.Visible = true;
                    this.btnAutenticarPrograma.Visible = true;
                    this.btnLogin.Visible = false;


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
            this.daSesion = new System.Data.SqlClient.SqlDataAdapter();
            this.cnn = new System.Data.SqlClient.SqlConnection();
            this.dsSeguridad = new ASSMCA.dsSeguridad();
            this.SPD_SESION = new System.Data.SqlClient.SqlCommand();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            ((System.ComponentModel.ISupportInitialize)(this.dsSeguridad)).BeginInit();
            #region daSesion
            this.daSesion.SelectCommand = this.sqlSelectCommand1;
            this.daSesion.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
                new System.Data.Common.DataTableMapping("Table", "SA_USUARIO", new System.Data.Common.DataColumnMapping[] {
                    new System.Data.Common.DataColumnMapping("PK_Usuario", "PK_Usuario"),
                    new System.Data.Common.DataColumnMapping("NB_Login", "NB_Login"),
                    new System.Data.Common.DataColumnMapping("NB_Usuario", "NB_Usuario"),
                    new System.Data.Common.DataColumnMapping("AP_Usuario", "AP_Usuario"),
                    new System.Data.Common.DataColumnMapping("IN_C_PERSONA", "IN_C_PERSONA"),
                    new System.Data.Common.DataColumnMapping("IN_READ", "IN_READ"),
                    new System.Data.Common.DataColumnMapping("IN_U_PERSONA", "IN_U_PERSONA"),
                    new System.Data.Common.DataColumnMapping("IN_C_PERFIL", "IN_C_PERFIL"),
                    new System.Data.Common.DataColumnMapping("IN_U_PCORTA", "IN_U_PCORTA"),
                    new System.Data.Common.DataColumnMapping("IN_D_PCORTA", "IN_D_PCORTA"),
                    new System.Data.Common.DataColumnMapping("IN_U_PERFIL", "IN_U_PERFIL"),
                    new System.Data.Common.DataColumnMapping("IN_D_PERFIL", "IN_D_PERFIL"),
                    new System.Data.Common.DataColumnMapping("PK_Programa", "PK_Programa"),
                    new System.Data.Common.DataColumnMapping("NB_Programa", "NB_Programa"),
                    new System.Data.Common.DataColumnMapping("PK_Administracion", "PK_Administracion"),
                    new System.Data.Common.DataColumnMapping("NB_Administracion", "NB_Administracion"),
                    new System.Data.Common.DataColumnMapping("IN_CambiarPassword", "IN_CambiarPassword"),
                    new System.Data.Common.DataColumnMapping("CO_Tipo", "CO_Tipo")})});
            #endregion
            #region cnn
            this.cnn.ConnectionString = NewSource.connectionString;
            #endregion
            #region dsSeguridad
            this.dsSeguridad.DataSetName = "dsSeguridad";
            this.dsSeguridad.Locale = new System.Globalization.CultureInfo("en-US");
            #endregion
            #region SPD_SESION
            this.SPD_SESION.CommandText = "dbo.[SPD_SESION]";
            this.SPD_SESION.CommandType = System.Data.CommandType.StoredProcedure;
            this.SPD_SESION.Connection = this.cnn;
            this.SPD_SESION.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            this.SPD_SESION.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Sesion", System.Data.SqlDbType.UniqueIdentifier, 16));
            #endregion
            #region sqlSelectCommand1
            this.sqlSelectCommand1.CommandText = "[SPC_SESION]";
            this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand1.Connection = this.cnn;
            this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NB_Login", System.Data.SqlDbType.VarChar, 10));
            this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PASSWORD", System.Data.SqlDbType.VarChar, 10));
            this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Sesion", System.Data.SqlDbType.UniqueIdentifier, 16, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            ((System.ComponentModel.ISupportInitialize)(this.dsSeguridad)).EndInit();
            #endregion
        }
        #endregion
        protected void btnAutenticarPrograma_Click(object sender, EventArgs e)
        {
            int nr_rowIndex_dsSeguridad = this.ddlPrograma.SelectedIndex;
            this.Session["pk_administracion"] = this.dsSeguridad.SA_USUARIO.Rows[nr_rowIndex_dsSeguridad]["PK_Administracion"].ToString();
            this.Session["nb_administracion"] = this.dsSeguridad.SA_USUARIO.Rows[nr_rowIndex_dsSeguridad]["NB_Administracion"].ToString();
            this.Session["pk_programa"] = this.dsSeguridad.SA_USUARIO.Rows[nr_rowIndex_dsSeguridad]["PK_Programa"].ToString();
            this.Session["nb_programa"] = this.dsSeguridad.SA_USUARIO.Rows[nr_rowIndex_dsSeguridad]["NB_Programa"].ToString();
            this.Session["nr_rowIndex_dsSeguridad"] = nr_rowIndex_dsSeguridad.ToString();
            this.Session["co_tipo"] = this.dsSeguridad.SA_USUARIO.Rows[nr_rowIndex_dsSeguridad]["CO_Tipo"].ToString();
            this.Response.Redirect("frmHome.aspx");
        }
        public void btnLogin_Click(object sender, EventArgs e)
        {
            string strGUID = "";
            this.daSesion.SelectCommand.Parameters["@NB_Login"].Value = this.txtUsuario.Text.Trim();
            this.daSesion.SelectCommand.Parameters["@PASSWORD"].Value = this.txtPassword.Text.Trim();
            try
            {
                this.cnn.Open();
                this.dsSeguridad.SA_USUARIO.Rows.Clear();
                dsSeguridad.EnforceConstraints = false; // Agregado MACR 11-Feb-2019
                this.daSesion.Fill(this.dsSeguridad);
                strGUID = this.daSesion.SelectCommand.Parameters["@PK_Sesion"].Value.ToString();
                this.cnn.Close();
                this.Session["dsSeguridad"] = this.dsSeguridad;
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
            if (this.dsSeguridad.SA_USUARIO.Rows.Count > 0)
            {
                Guid PK_Sesion = new Guid(strGUID);

                this.Session["dsSeguridad"] = this.dsSeguridad;
                this.Session["pk_sesion"] = strGUID;

                if (this.dsSeguridad.SA_USUARIO[0].IN_CambiarPassword)
                {
                    this.Response.Redirect("PasswordChangeMandatory.aspx");
                }
                else
                {
                    if (this.dsSeguridad.SA_USUARIO.Rows.Count == 1)
                    {
                        this.Session["pk_administracion"] = this.dsSeguridad.SA_USUARIO.Rows[0]["PK_Administracion"].ToString();
                        this.Session["nb_administracion"] = this.dsSeguridad.SA_USUARIO.Rows[0]["NB_Administracion"].ToString();
                        this.Session["pk_programa"] = this.dsSeguridad.SA_USUARIO.Rows[0]["PK_Programa"].ToString();
                        this.Session["nb_programa"] = this.dsSeguridad.SA_USUARIO.Rows[0]["NB_Programa"].ToString();
                        this.Session["co_tipo"] = this.dsSeguridad.SA_USUARIO.Rows[0]["CO_Tipo"].ToString();
                        this.Session["nr_rowIndex_dsSeguridad"] = "0";
                        this.Session["usuarioPrograma"] = "1";
                        this.Response.Redirect("frmHome.aspx");
                    }
                    else if (this.dsSeguridad.SA_USUARIO.Rows.Count > 1)//Se ha obtenido un usuario asociado a varios programas
                    {
                        this.ddlPrograma.DataSource = this.dsSeguridad.SA_USUARIO;
                        this.ddlPrograma.DataBind();
                        lblTotalPrograma.Text = "Total: " + ddlPrograma.Items.Count;
                        this.divMsg1.Visible = false;
                        this.divPrograma.Visible = true;
                        this.LoginBox.Visible = false;
                        this.lblMsg.Visible = false;
                        this.txtPassword.Visible = false;
                        this.rfvPassword.Enabled = false;
                        this.ddlPrograma.Visible = true;
                        this.btnAutenticarPrograma.Visible = true;
                        this.btnLogin.Visible = false;
                        
                    }
                }
            }
            else
            {
                this.lblMsg.Text = "Verifique que el nombre de usuario y la contraseña sean correctos e intente denuevo.";
                this.lblMsg.Visible = true;
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
           

            var str = txtFilter.Text;

            if (!string.IsNullOrEmpty(str))
                this.ddlPrograma.DataSource = this.dsSeguridad.SA_USUARIO.Where(x => x.NB_Programa.ToLower().Contains(str.Trim().ToLower()));
            else
                ddlPrograma.DataSource = this.dsSeguridad.SA_USUARIO;
            
            this.ddlPrograma.DataBind();
            lblTotalPrograma.Text = "Total: " + ddlPrograma.Items.Count;
        }


        protected void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            ddlPrograma.DataSource = this.dsSeguridad.SA_USUARIO;
            this.ddlPrograma.DataBind();
            lblTotalPrograma.Text = "Total: " + ddlPrograma.Items.Count;
        }
    }
}
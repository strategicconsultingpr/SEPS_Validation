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
namespace ASSMCA
{
	public partial class ErrorView : System.Web.UI.Page
	{
        private DataSet ds_errors = new DataSet();
		protected void Page_Load(object sender, System.EventArgs e){
            if (this.Session["dsSeguridad"] == null)
            {
                this.Response.Redirect("frmLogon.aspx");
                return;
            }
            string mode = "";
            if (Request.QueryString["mode"] != null)
            {
                mode = Request.QueryString["mode"].ToString();
            }
            switch(mode)
            {
                case("distinct"):
                case("unique"):
                    GetErrorsDistinct();
                    break;
                case("clear"):
                    ClearErrors();
                    break;
                default: GetErrors(); break;
            }
        }
        private void GetErrors()
        {
            string sql = "SELECT * FROM SA_Errors ORDER BY PK_Error";
            using (SqlConnection conn = new SqlConnection(NewSource.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds_errors);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    dgErrors.DataSource = ds_errors;
                    dgErrors.DataBind();
                    conn.Close();
                    da.Dispose();
                    standard.Visible = true;
                    distinct.Visible = false;
                }
                catch
                {
                    conn.Close();
                }
            }
        }
        private void GetErrorsDistinct()
        {
            string sql = "SELECT DISTINCT IN_Error FROM SA_Errors";
            using (SqlConnection conn = new SqlConnection(NewSource.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds_errors);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    dgErrorsDistinct.DataSource = ds_errors;
                    dgErrorsDistinct.DataBind();
                    conn.Close();
                    da.Dispose();
                    standard.Visible = false;
                    distinct.Visible = true;
                }
                catch
                {
                    conn.Close();
                }
            }
        }
        private void ClearErrors()
        {
            string sql = "DELETE FROM SA_Errors";
            using (SqlConnection conn = new SqlConnection(NewSource.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch
                {
                    conn.Close();
                }
            }
        }
		#region Código generado por el Diseñador de Web Forms
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent(){}
		#endregion
	}
}
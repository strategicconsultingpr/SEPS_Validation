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
	public partial class Error : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e){
            Exception err = Session["LastError"] as Exception;
            if (err != null)
            {
                err = err.GetBaseException();
                Session["LastError"] = null;
                lblErrorMsg.Text = "El número de referencia para este error es REF#" + ReportError("Error Message: " + err.Message + " Source: " + err.Source + " InnerException: " + ((err.InnerException != null) ? err.InnerException.ToString() : "") + " StackTrace: " + err.StackTrace).ToString() + ".";
                if (this.Session["dsSeguridad"] == null)
                {
                    this.Response.Redirect("frmLogon.aspx");
                    return;
                }
            }
            else if (this.Session["dsSeguridad"] == null)
            {
                this.Response.Redirect("frmLogon.aspx");
                return;
            }
            else {
                this.Response.Redirect("frmHome.aspx");
                return;
            }
        }
        private int ReportError(string error)
        {
            int refNumber = 0;
            string sql = "EXEC [dbo].[SPC_Errors] @PK_Error = @PK_Error OUTPUT, @ErrorData = @ErrorData SELECT	@PK_Error as N'@PK_Error'";
            using (SqlConnection conn = new SqlConnection(NewSource.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@ErrorData", SqlDbType.VarChar);
                cmd.Parameters["@ErrorData"].Value = error;
                SqlParameter output = new SqlParameter("@PK_Error", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    refNumber = (int)output.Value;
                    conn.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return refNumber;
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
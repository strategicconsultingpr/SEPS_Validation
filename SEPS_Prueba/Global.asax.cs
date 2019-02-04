using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;

namespace ASSMCA 
{
	public class Global : System.Web.HttpApplication
	{
		private System.Data.SqlClient.SqlConnection cnn;
		private System.Data.SqlClient.SqlCommand SPD_SESION;

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{

		}

		protected void Session_Start(Object sender, EventArgs e)
		{
            Session.Timeout = 540;
            Session["LastError"] = ""; 
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{
            try
            {
                Exception err = Server.GetLastError();
                Session.Add("LastError", err);
            }
            catch { }
		}

		protected void Session_End(Object sender, EventArgs e)
		{
			if( this.Session["pk_sesion"] != null)
			{
				try
				{
					this.SPD_SESION.Parameters["@PK_Sesion"].Value = new Guid(this.Session["pk_sesion"].ToString());
					this.cnn.Open();
					this.SPD_SESION.ExecuteNonQuery();
					this.cnn.Close();
				}
				catch(Exception ex)
				{
					string m = ex.Message;
                    if (this.cnn.State != System.Data.ConnectionState.Closed)
                    {
                        this.cnn.Close();
                    }
                    throw ex;
				}
			}
		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Código generado por el Diseñador de Web Forms
		private void InitializeComponent()
		{    
			this.cnn = new System.Data.SqlClient.SqlConnection();
            this.SPD_SESION = new System.Data.SqlClient.SqlCommand();
            this.cnn.ConnectionString = NewSource.connectionString;
            this.SPD_SESION.CommandText = "dbo.[SPD_SESION]";
			this.SPD_SESION.CommandType = System.Data.CommandType.StoredProcedure;
			this.SPD_SESION.Connection = this.cnn;
			this.SPD_SESION.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
			this.SPD_SESION.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Sesion", System.Data.SqlDbType.UniqueIdentifier, 16));
		}
		#endregion
	}
}


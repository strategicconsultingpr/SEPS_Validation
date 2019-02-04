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

namespace ASSMCA.Perfiles
{
	/// <summary>
	/// Descripci�n breve de frmEliminarPerfil.
	/// </summary>
	public partial class frmEliminarPerfil : System.Web.UI.Page
	{
		protected System.Data.SqlClient.SqlConnection cnn;
		protected System.Data.SqlClient.SqlCommand SPD_PERFIL;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!this.IsPostBack)
			{
				//La aplicacion hace un redirect, de forma tal que no se considera un Postback
				// Si se hace un Postbak con un mensaje, implica que debe presentarse en pantalla
				if (this.Request.QueryString["msg"] == null)
				{
					string TI_Perfil = this.Request.QueryString["TI_Perfil"];//Tipo de Perfil a eliminar
					string accion = this.Request.QueryString["accion"];
					int PK_Perfil = Convert.ToInt32(this.Request.QueryString["PK_Perfil"].ToString()); //Clave primaria del perfil a eliminar
					int PK_Episodio = Convert.ToInt32(this.Request.QueryString["PK_Episodio"].ToString()); //Clave primaria del episodio a eliminar

					string msg = "";

					switch (TI_Perfil)
					{
						case "AD": //Admision
							msg = "Usted ha seleccionado eliminar el episodio ";
							msg += "n�mero " + PK_Episodio + ".";
							break;
						case "AL"://Alta
							msg = "Usted ha seleccionado eliminar el perfil de alta ";
							msg += "n�mero " + PK_Perfil + ".";
							break;
						case "EV"://Evaluacion
							msg = "Usted ha seleccionado eliminar el perfil de evaluaci�n ";
							msg += "n�mero " + PK_Perfil + ".";
							break;
						case "PR"://Prevencion
							break;
					}

					//Se complementa el mensaje que se despliega antes de borrar el registro
					//con la accion a tomar. Ya sea una eliminacion l�gica (teds) o f�sica.
					if(accion == "L")
					{
						msg += " El registro no ser� borrado fisicamente de la base de datos, ";
						msg += "dado que la fecha del perfil es mayor al tiempo m�ximo requerido ";
						msg += "para permitir la eliminaci�n f�sica del registro. El registro ser� ";
						msg += "borrado l�gicamente, lo que implica que ser� marcado como eliminado ";
						msg += "para efecto de los reportes de TEDS o cualquier otra herramienta externa ";
						msg += "que haga uso de los datos del presente sistema.";
					}
					if(accion == "F")
					{
						msg += " El registro ser� eliminado fisicamente del sistema y no podr� ser ";
						msg += "recuperado posteriormente. Usted deber� registrar nuevamente el ";
						msg += "perfil en caso que acepte eliminarlo erroneamente.";
					}
					this.lblMsg.Text = msg;
				}
				else
				{
					this.lblMsg.Text =  this.Request.QueryString["msg"].ToString();
					this.lblMsg.Visible = true;
					this.btnEliminar.Visible = false;
					this.btnPagPrincipal.Visible = true;
				}
			}
		}

		#region C�digo generado por el Dise�ador de Web Forms
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: llamada requerida por el Dise�ador de Web Forms ASP.NET.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// M�todo necesario para admitir el Dise�ador. No se puede modificar
		/// el contenido del m�todo con el editor de c�digo.
		/// </summary>
		private void InitializeComponent()
		{    
			this.cnn = new System.Data.SqlClient.SqlConnection();
			this.SPD_PERFIL = new System.Data.SqlClient.SqlCommand();
			// 
			// cnn
			// 
            this.cnn.ConnectionString = NewSource.connectionString;
			// 
			// SPD_PERFIL
			// 
			this.SPD_PERFIL.CommandText = "dbo.[SPD_PERFIL]";
			this.SPD_PERFIL.CommandType = System.Data.CommandType.StoredProcedure;
			this.SPD_PERFIL.Connection = this.cnn;
			this.SPD_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
			this.SPD_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Perfil", System.Data.SqlDbType.Int, 4));
			this.SPD_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Fisico", System.Data.SqlDbType.Bit, 1));
			this.SPD_PERFIL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Sesion", System.Data.SqlDbType.UniqueIdentifier, 16));

		}
		#endregion

		protected void btnEliminar_Click(object sender, System.EventArgs e)
		{
			string TI_Perfil = this.Request.QueryString["TI_Perfil"];//Tipo de Perfil a eliminar
			string accion = this.Request.QueryString["accion"];
			int PK_Perfil = Convert.ToInt32(this.Request.QueryString["PK_Perfil"].ToString()); //Clave primaria del perfil a eliminar

			bool Fisico;
            if (accion == "F")
            {
                Fisico = true;
            }
            else
            {
                Fisico = false;
            }

			this.SPD_PERFIL.Parameters["@PK_Perfil"].Value = PK_Perfil;
			this.SPD_PERFIL.Parameters["@Fisico"].Value = Fisico;
			Guid PK_Sesion = new Guid(this.Session["pk_sesion"].ToString());
			this.SPD_PERFIL.Parameters["@PK_Sesion"].Value = PK_Sesion;

			try
			{
				this.cnn.Open();
				this.SPD_PERFIL.ExecuteNonQuery();
				this.cnn.Close();

				this.Response.Redirect("frmEliminarPerfil.aspx?msg=El registro ha sido eliminado con exito!.");

			}
			catch(Exception ex)
			{
				string m = ex.Message;
				Trace.Warn("btnEliminar_Click()", m, ex);
                if (this.cnn.State != ConnectionState.Closed)
                {
                    this.cnn.Close();
                }
                throw ex;
			}
		}

		protected void btnPagPrincipal_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("../frmHome.aspx");
		}
	}
}

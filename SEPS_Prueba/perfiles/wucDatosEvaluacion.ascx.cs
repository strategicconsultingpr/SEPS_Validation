namespace ASSMCA.Perfiles
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	public partial class wucDatosEvaluacion : System.Web.UI.UserControl
	{
		protected ASSMCA.perfiles.dsPerfil dsPerfil;
		public frmAction m_frmAction;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!this.IsPostBack)
			{
				this.dsPerfil = (ASSMCA.perfiles.dsPerfil)this.Session["dsPerfil"];
				if( this.m_frmAction == frmAction.Create || this.m_frmAction == frmAction.Update )
				{
                    if (this.m_frmAction == frmAction.Update)
                    {
                        this.ActualizarCampos();
                    }
				}
				else
				{
					LeerRegistro();
				}
			}
		}

		private void ActualizarCampos()
		{
			this.txtComentarios.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Comentario"].ToString();
		}

		private void LeerRegistro()
		{
            if (this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Comentario"].ToString() != "")
            {
                this.txtComentarios.Visible = false;
                this.lblComentario.Visible = true;
                this.lblComentario.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Comentario"].ToString();
            }
            else
            {
                divComentarios.Visible = false;
            }
		}

		#region Código generado por el Diseñador de Web Forms
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Método necesario para admitir el Diseñador. No se puede modificar
		///		el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			this.dsPerfil = new ASSMCA.perfiles.dsPerfil();
			((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).BeginInit();
			// 
			// dsPerfil
			// 
			this.dsPerfil.DataSetName = "dsPerfil";
			this.dsPerfil.Locale = new System.Globalization.CultureInfo("en-US");
			((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).EndInit();

		}
		#endregion

		public string @DE_Comentario
		{
			get
			{
                try
                {
                    return this.txtComentarios.Text.Trim();
                }
                catch
                {
                    return "";
                }
			}
		}
	}
}

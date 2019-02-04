namespace ASSMCA.Perfiles
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	public partial class wucOtrosDatosPerfil : System.Web.UI.UserControl
	{
		public frmAction m_frmAction;
		protected System.Web.UI.WebControls.RangeValidator RangeValidator1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist1;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist2;
		protected ASSMCA.perfiles.dsPerfil dsPerfil;
		protected System.Web.UI.WebControls.DropDownList dllMes;
		protected System.Web.UI.WebControls.DropDownList dllDia;

		protected void Page_Load(object sender, System.EventArgs e)
		{
            tipoDePerfilHidden.Value = this.Session["Tipo_Perfil"].ToString();
			if(!this.IsPostBack)
			{
                FechaAdmisionHidden.Value = Session["FechaAdmision"].ToString();
				this.dsPerfil = (ASSMCA.perfiles.dsPerfil)this.Session["dsPerfil"];
				this.DataBind();
				
				if( this.m_frmAction == frmAction.Create || this.m_frmAction == frmAction.Update )
				{
					this.EditarRegistro();

					if ( this.m_frmAction == frmAction.Update )
					{
						this.ActualizarCampos();
						this.lblPerfil.Visible = true;
						
					}
					else //es crear
					{
						this.lblPerfil.Visible = false;
						this.lblPerfilNuevo.Visible = true;
					}
				}
				else
				{
					this.LeerRegistro();
				}
			}
		}

		private void LeerRegistro()
		{
			this.ddlMes.Visible = false;
			this.ddlDía.Visible = false;
            this.txtAño.Visible = false;

            this.ddlMesContacto.Visible = false;
            this.ddlDíaContacto.Visible = false;
            this.txtAñoContacto.Visible = false;


         
		}

		private void EditarRegistro()
		{
			string Min, Max;
			//Defecto #62
//			Min = DateTime.Now.AddYears(-1).Year.ToString();
//			Max = DateTime.Now.Year.ToString();
			Min = "1950";
			Max = DateTime.Now.AddYears(1).Year.ToString();
			//Fin defecto 62

            this.lblFechaPerfil.Visible = false;
            this.rvAño.MinimumValue = Min;
            this.rvAño.MaximumValue = Max;
            this.rvAño.ToolTip = "Escriba una año entre el " + Min + " y el " + Max + ".";

            this.rvAñoContacto.MinimumValue = Min;
            this.rvAñoContacto.MaximumValue = Max;
            this.rvAño.ToolTip = "Escriba una año entre el " + Min + " y el " + Max + ".";
		}

		private void ActualizarCampos()
		{
			this.ddlMes.SelectedValue = DateTime.Parse(this.dsPerfil.SA_PERFIL.DefaultView[0]["FE_Perfil"].ToString()).Month.ToString();;
			this.ddlDía.SelectedValue = DateTime.Parse(this.dsPerfil.SA_PERFIL.DefaultView[0]["FE_Perfil"].ToString()).Day.ToString();;
            this.txtAño.Text = DateTime.Parse(this.dsPerfil.SA_PERFIL.DefaultView[0]["FE_Perfil"].ToString()).Year.ToString();

            if (this.dsPerfil.SA_PERFIL.DefaultView[0]["FE_Contacto"] != System.DBNull.Value)
            {
                this.ddlMesContacto.SelectedValue = DateTime.Parse(this.dsPerfil.SA_PERFIL.DefaultView[0]["FE_Contacto"].ToString()).Month.ToString(); ;
                this.ddlDíaContacto.SelectedValue = DateTime.Parse(this.dsPerfil.SA_PERFIL.DefaultView[0]["FE_Contacto"].ToString()).Day.ToString(); ;
                this.txtAñoContacto.Text = DateTime.Parse(this.dsPerfil.SA_PERFIL.DefaultView[0]["FE_Contacto"].ToString()).Year.ToString();
                this.lblFechaContacto.Visible = false;

            }
            else
            {
                this.ddlMesContacto.SelectedValue = "1";
                this.ddlDíaContacto.SelectedValue = "1";
                this.txtAñoContacto.Text = "";
            }

			this.lblEpisodio.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_Episodio"].ToString();
			this.lblPerfil.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString();
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

		public DateTime FE_Perfil
		{
			get
			{
                string fe = this.ddlMes.SelectedValue.ToString() + "/" + this.ddlDía.SelectedValue.ToString() + "/" + this.txtAño.Text;
				return DateTime.Parse(fe);
            }
		}

        public DateTime? FE_Contacto
        {
            get
            {
                string fe = this.ddlMesContacto.SelectedValue.ToString() + "/" + this.ddlDíaContacto.SelectedValue.ToString() + "/" + this.txtAñoContacto.Text;
                try
                {
                    return DateTime.Parse(fe);
                }
                catch {
                    return null;
                }
            }
        }

        
}
}

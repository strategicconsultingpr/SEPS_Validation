namespace ASSMCA.Perfiles
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	public partial class wucOtrosDatos : System.Web.UI.UserControl
	{
		public frmAction m_frmAction;
		protected ASSMCA.perfiles.dsPerfil dsPerfil;
		protected System.Data.DataView dvwSeguroSalud;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!this.IsPostBack)
			{
				this.dsPerfil = (ASSMCA.perfiles.dsPerfil)this.Session["dsPerfil"];
				this.dvwSeguroSalud.Table = this.dsPerfil.SA_LKP_TEDS_SEGURO_SALUD;

				if (this.Session["pk_administracion"].ToString() == "1")
                {
                    this.dvwSeguroSalud.RowFilter = "PK_SeguroSalud <> 4";
                }
				this.DataBind();
                switch (this.m_frmAction)
                {
                    case frmAction.Update: 
					    this.EditarRegistro();
                        this.ActualizarCampos();
						this.lblPerfil.Visible = true;
						this.lblEpisodio.Visible = true;
                        break;
                    case frmAction.Create: 
					    this.EditarRegistro();
                        this.lblPerfil.Visible = false;
						this.lblPerfilNuevo.Visible = true;
						this.lblEpisodio.Visible = false;
						this.lblEpisodioNuevo.Visible = true;
                        break;
                    default:
                        this.LeerRegistro();
                        break;
                }
			}
		}

		private void LeerRegistro()
		{
			this.ddlFuentePago.Visible = false;
			this.ddlSeguroSalud.Visible = false;
			this.lblEpisodio.Text = this.dsPerfil.SA_EPISODIO[0]["PK_Episodio"].ToString();
			this.lblPerfil.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString();
			this.lblFuentePago.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_Pago"].ToString();
			this.lblSeguroSalud.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_SeguroSalud"].ToString();
			this.lblCentro.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["NB_Programa"].ToString();
		}

		private void EditarRegistro()
		{
			this.lblCentro.Text = this.Session["nb_programa"].ToString();
		}

		private void ActualizarCampos()
		{
			this.ddlFuentePago.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_FuentePago"].ToString();
			this.ddlSeguroSalud.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_SeguroSalud"].ToString();
			this.lblPerfil.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["PK_NR_Perfil"].ToString();
            this.lblEpisodio.Text = this.dsPerfil.SA_EPISODIO[0]["PK_Episodio"].ToString();
		}

		#region Código generado por el Diseñador de Web Forms
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

        private void InitializeComponent()
		{
			this.dsPerfil = new ASSMCA.perfiles.dsPerfil();
			this.dvwSeguroSalud = new System.Data.DataView();
			((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwSeguroSalud)).BeginInit();
			// 
			// dsPerfil
			// 
			this.dsPerfil.DataSetName = "dsPerfil";
			this.dsPerfil.Locale = new System.Globalization.CultureInfo("en-US");
			((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvwSeguroSalud)).EndInit();

		}
		#endregion

		#region Propiedades
		public sbyte FK_SeguroSalud
		{
			get
			{
				return Convert.ToSByte(this.ddlSeguroSalud.SelectedValue.ToString());
            }
		}

		public sbyte FK_FuentePago
		{
			get
			{
				return Convert.ToSByte(this.ddlFuentePago.SelectedValue.ToString());
            }
		}
#endregion

}
}

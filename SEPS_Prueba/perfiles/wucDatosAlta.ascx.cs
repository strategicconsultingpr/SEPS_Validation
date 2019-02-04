namespace ASSMCA.Perfiles
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	public partial class wucDatosAlta : System.Web.UI.UserControl
	{
		protected ASSMCA.perfiles.dsPerfil dsPerfil;
		public frmAction m_frmAction;
        public int pk_perfil;
        public int pk_programa;
        protected System.Data.DataView dvwRazonAlta;
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!this.IsPostBack)
            {
                this.dsPerfil = (ASSMCA.perfiles.dsPerfil)this.Session["dsPerfil"];
                this.dvwRazonAlta.Table = this.dsPerfil.SA_LKP_ALTA;
                if (this.Session["pk_administracion"].ToString() == "1" && this.pk_programa != 75)//Niños y adolecentes
                {
                    this.dvwRazonAlta.RowFilter = " PK_Alta <> 8 ";
                }
                this.dsPerfil = (ASSMCA.perfiles.dsPerfil)this.Session["dsPerfil"];
                this.DataBind();
                ListItem li = new ListItem("", "0");
                this.ddlCategoriasDeCentrosPrivadosALL();
                this.ddlCategoriasDeCentrosPrivados.Items.Insert(0, li);
                this.ddlCentroReferido.Items.Insert(0, li);
                this.RemoverRevocacionDeRazonDeAlta();
                this.setValue();
                switch (this.m_frmAction)
                {
                    case (frmAction.Create): 
                        this.ddlRazonAlta.Items.Insert(0, li);
                        this.EditarRegistro();
                        break;
                    case (frmAction.Update): 
                        this.ActualizarCampos();
                        this.EditarRegistro();
                        break;
                    case (frmAction.Read):
                        this.LeerRegistro();
                        break;
                    default: break;
                }
            }
		}

		private void EditarRegistro()
		{
			if(this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_Alta"].ToString() == "3")
			{
				this.ddlCentroReferido.Enabled = true;
			}
			else
			{
				this.ddlCentroReferido.Enabled = false;
			}
		}

		private void ActualizarCampos()
		{
			this.txtComentarios.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Comentario"].ToString();
            if (this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_Alta"] != System.DBNull.Value)
            {
                this.ddlRazonAlta.ClearSelection();
			    this.ddlRazonAlta.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_Alta"].ToString();
            }
			if(this.ddlCentroReferido.Enabled && this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_CentroTraslado"] != System.DBNull.Value)
			{
                this.ddlCentroReferido.ClearSelection();
				this.ddlCentroReferido.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_CentroTraslado"].ToString();
			}	
		}

		private void LeerRegistro()
		{
            if (this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Comentario"].ToString() != "")
            {
                this.txtComentarios.Visible = false;
                this.lblComentario.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Comentario"].ToString();
            }
            else {
                divComentarios.Visible = false;
            }
            ddlCategoriasDeCentrosPrivados.Visible = false;
            lblCategoriaDeCentroPrivado.Text = ddlCategoriasDeCentrosPrivados.SelectedItem.Text;
			this.ddlRazonAlta.Visible = false;
			this.lblRazonAlta.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Alta"].ToString();
			this.ddlCentroReferido.Visible = false;
            if (this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_Alta"].ToString() == "3")
            {
                this.lblCentroReferido.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NB_Programa"].ToString();
            }
            else
            {
                this.lblCentroReferido.Text = "(NO APLICA)";
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
			this.dsPerfil = new ASSMCA.perfiles.dsPerfil();
            this.dvwRazonAlta = new System.Data.DataView();
            ((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwRazonAlta)).BeginInit();
			this.dsPerfil.DataSetName = "dsPerfil";
			this.dsPerfil.Locale = new System.Globalization.CultureInfo("en-US");
            ((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwRazonAlta)).EndInit();
		}
		#endregion

        #region Properties
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

		public int @FK_Alta
		{
			get
			{
                try
                {
                    return int.Parse(this.ddlRazonAlta.SelectedValue.ToString());
                }
                catch
                {
                    return 95;//Default No informo
                }
			}
		}

		public object @FK_CentroTraslado
		{
			get
			{
                try
                {
                    return int.Parse(this.ddlCentroReferido.SelectedValue.ToString());
                }
                catch
                {
                    return System.DBNull.Value;//Default null
                }
			}
		}

        public object @FK_CategoriaCentroPrivado
        {
            get
            {
                try
                {
                    return Convert.ToInt32(this.ddlCategoriasDeCentrosPrivados.SelectedValue.ToString());
                }
                catch 
                {
                    return System.DBNull.Value;//Default null
                }
            }
        }
        #endregion

        private void ddlCategoriasDeCentrosPrivadosALL()
        {
            NewSource NS = new NewSource();
            DataTable Dt = new DataTable();
            Dt = NS.getAll("SPR_DROP_CategoriasCentrosPrivados");
            this.ddlCategoriasDeCentrosPrivados.DataSource = Dt;
            this.ddlCategoriasDeCentrosPrivados.DataValueField = "PK_CategoriaCentroPrivado";
            this.ddlCategoriasDeCentrosPrivados.DataTextField = "DE_CategoriaCentroPrivado";
            this.ddlCategoriasDeCentrosPrivados.DataBind();
            Dt = null;
            NS = null;
        }
        public bool EsProgramaDesvio(int PK_PROGRAMA)
        {
            bool esProgramaDesvio = false;
            switch (PK_PROGRAMA)
            {
                case (37):
                case (32):
                case (27):
                case (34):
                case (40):
                case (39):
                case (42):
                case (31):
                case (35):
                case (36):
                case (41):
                case (38):
                case (33):
                case (97):
                case (99):
                case (91):
                case (96):
                case (10):
                case (72):
                case (133):
                case (73):
                case (74):
                case (9):
                case (7):
                case (48):
                case (28):
                case (127):
                case (46):
                case (95):
                case (49):
                case (52):
                case (51):
                case (50):
                case (47):
                case (135):
                case (136):
                //case (75):
                    esProgramaDesvio = true; break;
                default: break;
            }
            return esProgramaDesvio;
        }

        public void RemoverRevocacionDeRazonDeAlta()
        {
            if (!this.EsProgramaDesvio(this.pk_programa))
            {
                this.ddlRazonAlta.ClearSelection();
                this.ddlRazonAlta.Items.Remove(this.ddlRazonAlta.Items.FindByValue("9"));
            }
        }

        public void setValue()
        {
            NewSource NS = new NewSource();
            DataTable Dref = new DataTable();
            Dref = NS.getRef("SPR_CategoriasCentrosPrivadosSelectedValue", pk_perfil);
            if (Dref.Rows.Count > 0)
            {
                this.ddlCategoriasDeCentrosPrivados.ClearSelection();
                this.ddlCategoriasDeCentrosPrivados.SelectedValue = Dref.Rows[0][0].ToString();
                Dref = null;
                NS = null;
            }
        }
	}
}

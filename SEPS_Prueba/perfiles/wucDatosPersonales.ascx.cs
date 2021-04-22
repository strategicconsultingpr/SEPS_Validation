namespace ASSMCA.Perfiles
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Data.SqlClient;
    using System.Web.UI;
    using ASSMCA.perfiles;

    public partial class wucDatosPersonales : System.Web.UI.UserControl
    {
        protected System.Data.SqlClient.SqlDataAdapter daPersona;
        protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        protected System.Data.SqlClient.SqlConnection cnn;
        public frmAction m_frmAction;
        protected ASSMCA.perfiles.dsPerfil dsPerfil;
        private int m_pk_persona, m_pk_perfil;
        private int m_PK_Programa;
        private bool dataReadOnly = false;
        private bool isAdmision, isEvaluacion, isAlta, tieneConvenio;
   
        protected void Page_Load(object sender, System.EventArgs e)
        {

            isAdmision = this.Session["Tipo_Perfil"].ToString() == "Admision";
            isEvaluacion = this.Session["Tipo_Perfil"].ToString() == "Evaluacion";
            isAlta = this.Session["Tipo_Perfil"].ToString() == "Alta";
            tieneConvenio =  (bool)this.Session["in_convenio"];
            this.dsPerfil = (ASSMCA.perfiles.dsPerfil)this.Session["dsPerfil"];
            this.daPersona.SelectCommand.Parameters["@PK_Persona"].Value = this.m_pk_persona;
            this.daPersona.SelectCommand.Parameters["@PK_Programa"].Value = this.m_PK_Programa;
            this.daPersona.SelectCommand.Parameters["@IN_Raza"].Value = false;
            this.dsPerfil.SA_PERSONA.Rows.Clear();
            this.daPersona.Fill(this.dsPerfil, "SA_PERSONA");
            if (!this.IsPostBack)
            {
                tieneConvenio = (bool)this.Session["in_convenio"];


                if (!tieneConvenio)
                {
                    divFechaConvenio.Visible = false;
                    divFechaConvenio.Disabled = true;
                }
                string Min, Max;
                //Defecto #62
                //			Min = DateTime.Now.AddYears(-1).Year.ToString();
                //			Max = DateTime.Now.Year.ToString();
                Min = "1950";
                Max = DateTime.Now.AddYears(1).Year.ToString();

                //Fin defecto 62
                this.rvAño.MinimumValue = Min;
                this.rvAño.MaximumValue = Max;
                this.rvAño.ToolTip = "Escriba un año entre el " + Min + " y el " + Max + ".";

                

                #region meses desde admision
                if ((isAdmision && !(this.m_frmAction == frmAction.Create) && this.dsPerfil.SA_EPISODIO.DefaultView[0]["FE_Episodio"] != null) || ((isAlta || isEvaluacion) && this.dsPerfil.SA_EPISODIO.DefaultView[0]["FE_Episodio"] != null))
                {
                    this.lblMesesDesdeAdmision.Text = monthsSinceDate(DateTime.Parse(this.dsPerfil.SA_EPISODIO.DefaultView[0]["FE_Episodio"].ToString())).ToString();
                    this.lblMesesDesdeAdmision.Text += (monthsSinceDate(DateTime.Parse(this.dsPerfil.SA_EPISODIO.DefaultView[0]["FE_Episodio"].ToString())) == 0) || (monthsSinceDate(DateTime.Parse(this.dsPerfil.SA_EPISODIO.DefaultView[0]["FE_Episodio"].ToString())) > 1) ? " meses" : " mes";
                    this.lblMesesDesdeAdmision.ToolTip = daysSinceDate(DateTime.Parse(this.dsPerfil.SA_EPISODIO.DefaultView[0]["FE_Episodio"].ToString())).ToString();
                    this.lblMesesDesdeAdmision.ToolTip += (daysSinceDate(DateTime.Parse(this.dsPerfil.SA_EPISODIO.DefaultView[0]["FE_Episodio"].ToString())) == 0) || (daysSinceDate(DateTime.Parse(this.dsPerfil.SA_EPISODIO.DefaultView[0]["FE_Episodio"].ToString())) > 1) ? " días" : " día";
                }
                else
                {
                    this.divMesesDesdeAdmision.Visible = false;
                }
                #endregion

                #region latest evaluation date
                if (isAlta && this.dsPerfil.SA_EPISODIO.DefaultView[0]["PK_Episodio"] != null)
                {
                    DateTime? latestEvaluationDate = GetLatestEvaluationDate((int)this.dsPerfil.SA_EPISODIO.DefaultView[0]["PK_Episodio"]);
                    if (latestEvaluationDate != null)
                    {
                        hiddenFechaUltimaEvaluacion.Value = ((DateTime)latestEvaluationDate).ToShortDateString();
                    }
                }
                #endregion

                switch (m_frmAction)
                {
                    case (frmAction.Create):
                        if (this.dsPerfil.SA_PERSONA[0]["NR_Expediente"] == System.DBNull.Value)
                        {
                            if (!isAdmision)
                            {
                                this.divTipoDeAdmision.Visible = false;
                                this.ddlDía.Visible = false;
                                this.ddlMes.Visible = false;
                                this.txtAño.Visible = false;
                                this.rfvFechaAdmision.Enabled = false;
                            }
                            if (!UsaTipoDeAdmision(m_PK_Programa))
                            {
                                divTipoDeAdmision.Visible = false;
                            }
                            this.dataReadOnly = false;
                            this.lblExpediente.Visible = false;
                            this.txtExpediente.Visible = true;
                        }
                        if (isAlta || isEvaluacion)
                        {
                            this.lblFechaAdmision.Text = DateTime.Parse(this.dsPerfil.SA_EPISODIO.DefaultView[0]["FE_Episodio"].ToString()).ToShortDateString();
                            Session["FechaAdmision"] = DateTime.Parse(this.dsPerfil.SA_EPISODIO.DefaultView[0]["FE_Episodio"].ToString()).ToShortDateString();
                            this.ddlMes.Visible = false;
                            this.ddlDía.Visible = false;
                            this.txtAño.Visible = false;
                            this.divTipoDeAdmision.Visible = false;
                            this.lblFechaAdmision.Visible = true;
                            this.txtExpediente.Visible = false;
                            this.ddlFechaConvenioDía.Visible = false;
                            this.ddlFechaConvenioMes.Visible = false;
                            this.txtFechaConvenioAño.Visible = false;
                            this.rfvFechaAdmision.Enabled = false;
                            this.divFamiliaMilitar.Visible = false;
                            this.divMilitar.Visible = false;
                        }
                        else if (isAdmision)
                        {
                            if (!UsaTipoDeAdmision(m_PK_Programa))
                            {
                                divTipoDeAdmision.Visible = false;
                            }
                            this.ddlMes.Visible = true;
                            this.ddlDía.Visible = true;
                            this.txtAño.Visible = true;
                            this.lblFechaAdmision.Visible = true;
                            if (!(this.dsPerfil.SA_PERSONA[0]["NR_Expediente"] == System.DBNull.Value))
                            {
                                this.lblExpediente.Visible = true;
                                this.txtExpediente.Text = this.dsPerfil.SA_PERSONA[0]["NR_Expediente"].ToString();
                                this.txtExpediente.Visible = false;
                            }
                            this.set_hProgramaAdultos();
                        }
                        this.dataReadOnly = false;
                        break;
                    case (frmAction.Read):
                        if (!isAdmision || !UsaTipoDeAdmision(m_PK_Programa))
                        {
                            divTipoDeAdmision.Visible = false;
                        }
                        if (!isAdmision)
                        {
                            this.divFamiliaMilitar.Visible = false;
                            this.divMilitar.Visible = false;
                        }
                        this.dataReadOnly = true;
                        this.lblFechaAdmision.Visible = true;
                        this.lblExpediente.Visible = true;
                        this.txtExpediente.Visible = false;
                        this.ddlTipoDeAdmision.Visible = false;
                        this.ddlMes.Visible = false;
                        this.ddlDía.Visible = false;
                        this.txtAño.Visible = false;
                        this.ddlFechaConvenioDía.Visible = false;
                        this.ddlFechaConvenioMes.Visible = false;
                        this.txtFechaConvenioAño.Visible = false;
                        this.lblFechaAdmision.Text = DateTime.Parse(this.dsPerfil.SA_EPISODIO.DefaultView[0]["FE_Episodio"].ToString()).ToShortDateString();

                        this.lblCelular1.Visible = true;
                        this.lblCelular1.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_CelularPrimario"].ToString();
                        this.lblCelular2.Visible = true;
                        this.lblCelular2.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_CelularContacto"].ToString();
                        this.txtCelular1.Visible = false;
                        this.txtCelular2.Visible = false;

                        this.lblEmail1.Visible = true;
                        this.lblEmail1.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_EmailPrimario"].ToString();
                        this.lblEmail2.Visible = true;
                        this.lblEmail2.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_EmailSecundario"].ToString();
                        this.txtEmail1.Visible = false;
                        this.txtEmail2.Visible = false;

                        break;
                    case (frmAction.Update):
                        this.dataReadOnly = false;
                        this.txtExpediente.Visible = false;
                        
                        this.ddlMes.SelectedValue = DateTime.Parse(this.dsPerfil.SA_EPISODIO.DefaultView[0]["FE_Episodio"].ToString()).Month.ToString(); ;
                        this.ddlDía.SelectedValue = DateTime.Parse(this.dsPerfil.SA_EPISODIO.DefaultView[0]["FE_Episodio"].ToString()).Day.ToString(); ;
                        this.txtAño.Text = DateTime.Parse(this.dsPerfil.SA_EPISODIO.DefaultView[0]["FE_Episodio"].ToString()).Year.ToString();
                        DateTime dtnow = new DateTime();
                        dtnow = DateTime.Now;
                        DateTime dt = FE_Episodio;
                        TimeSpan diffResult = dtnow.Date - dt.Date;

                        this.txtCelular1.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_CelularPrimario"].ToString();
                        this.txtCelular2.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_CelularContacto"].ToString();
                        this.txtEmail1.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_EmailPrimario"].ToString();
                        this.txtEmail2.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_EmailSecundario"].ToString();
                        if (isAdmision)
                        {
                            this.txtExpediente.Visible = false;
                            
                            if (!UsaTipoDeAdmision(m_PK_Programa))
                            {
                                divTipoDeAdmision.Visible = false;
                            }
                            this.set_hProgramaAdultos();
                            if ((int)diffResult.Days >= (365 * 2))
                            {
                                this.lblFechaError.Text = "La fecha de admisión fue hace más de dos años.";
                                this.lblFechaError.ForeColor = Color.Blue;
                            }
                            else
                            {
                                this.lblFechaError.Text = "";
                            }
                        }
                        else
                        {
                            this.rfvFechaAdmision.Enabled = false;
                            this.lblFechaAdmision.Text = DateTime.Parse(this.dsPerfil.SA_EPISODIO.DefaultView[0]["FE_Episodio"].ToString()).ToShortDateString();
                            this.divTipoDeAdmision.Visible = false;
                            this.ddlFechaConvenioDía.Visible = false;
                            this.ddlFechaConvenioMes.Visible = false;
                            this.txtFechaConvenioAño.Visible = false;
                            this.ddlDía.Visible = false;
                            this.ddlMes.Visible = false;
                            this.txtAño.Visible = false;
                            this.divFamiliaMilitar.Visible = false;
                            this.divMilitar.Visible = false;
                        }
                        break;
                }

                ListItem li = new ListItem("", "0");
                this.ddlFamiliaMilitar.Items.Insert(0, li);
                
                try
                {
                    this.DataBind();
                }
                catch { }
                if (isAdmision)
                {
                    this.ddlTipoDeAdmision.Items.Insert(0, li);
                }
                
                if (dataReadOnly)
                {
                    this.load();
                    dataRead();
                }
                else
                {
                    dataEdit();
                    this.load();
                    this.ddlMilitar.Items.Insert(0, li);
                    this.ddlGenero.Items.Insert(0, li);
                }
            }
          
        }

        
        private void set_hProgramaAdultos()
        {
            try
            {
                switch ((PKAdministracion)Convert.ToInt32(this.Session["pk_administracion"].ToString()))
                {
                    case (PKAdministracion.Adultos):
                    case (PKAdministracion.Rehabilitacion): hProgramaAdultos.Value = "true"; break;
                    case (PKAdministracion.NinosYAdolecentes): hProgramaAdultos.Value = "false"; break;
                    default: hProgramaAdultos.Value = "true"; break;
                }
            }
            catch
            {
                hProgramaAdultos.Value = "true";
            }
        }

        private int monthsSinceDate(DateTime startDate, DateTime endDate)
        {
            TimeSpan dateRange = startDate.Subtract(endDate);
            return Math.Abs((int)(dateRange.Days / 30));
        }
        private int monthsSinceDate(DateTime date)
        {
            TimeSpan dateRange = date.Subtract(DateTime.Now);
            return Math.Abs((int)(dateRange.Days / 30));
        }
        private int daysSinceDate(DateTime startDate, DateTime endDate)
        {
            TimeSpan dateRange = startDate.Subtract(endDate);
            return Math.Abs(dateRange.Days / 30);
        }
        private int daysSinceDate(DateTime date)
        {
            TimeSpan dateRange = date.Subtract(DateTime.Now);
            return Math.Abs(dateRange.Days);
        }
        private void dataRead()
        {
            ddlGenero.Visible = false;
            ddlMilitar.Visible = false;
            ddlFamiliaMilitar.Visible = false;
            lblFamMilitar.Text = ddlFamiliaMilitar.SelectedItem.Text;
            lblMilitar.Text = ddlMilitar.SelectedItem.Text;
            lblGenero.Text = ddlGenero.SelectedItem.Text;
            if (tieneConvenio)
            {
                try
                {
                    lblFechaConvenio.Text = DateTime.Parse(dsPerfil.SA_EPISODIO[0].FE_FechaConvenio.ToString()).ToShortDateString();
                }
                catch { }
            }
            if (isAdmision)
            {
                lblTipoDeAdmision.Text = ddlTipoDeAdmision.SelectedItem.Text;
            }
        }

        private DateTime? GetLatestEvaluationDate(int pk_episodio)
        {
            DateTime? date = null;
            string sql = " EXEC [dbo].[SPR_MostRecentEvaluationProfile] @ProfileDate = @ProfileDate OUTPUT, @Episode = @Episode SELECT	@ProfileDate as N'@ProfileDate'";
            using (SqlConnection conn = new SqlConnection(NewSource.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@Episode", SqlDbType.Int);
                cmd.Parameters["@Episode"].Value = pk_episodio;
                SqlParameter output = new SqlParameter("@ProfileDate", SqlDbType.Date);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    date = (DateTime?)output.Value;
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return date;
        }

        private void dataEdit()
        {
            ddlGenero.Visible = true;
            ddlMilitar.Visible = true;
            ddlFamiliaMilitar.Visible = true;
            try
            {
                if (tieneConvenio)
                {
                    if (isAdmision && m_frmAction == frmAction.Update)
                    {
                        var lol = dsPerfil.SA_EPISODIO[0].FE_FechaConvenio.ToString();
                        DateTime fechaConvenio = DateTime.Parse(dsPerfil.SA_EPISODIO[0].FE_FechaConvenio.ToString());
                        ddlFechaConvenioMes.ClearSelection();
                        ddlFechaConvenioDía.ClearSelection();
                        ddlFechaConvenioMes.SelectedValue = fechaConvenio.Month.ToString();
                        ddlFechaConvenioDía.SelectedValue = fechaConvenio.Day.ToString();
                        txtFechaConvenioAño.Text = fechaConvenio.Year.ToString();
                    }
                    else if (isEvaluacion || isAlta)
                    {
                        lblFechaConvenio.Text = DateTime.Parse(dsPerfil.SA_EPISODIO[0].FE_FechaConvenio.ToString()).ToShortDateString();
                    }
                }
            }
            catch { }
        }
        #region Código generado por el Diseñador de Web Forms
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.daPersona = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.cnn = new System.Data.SqlClient.SqlConnection();
            this.dsPerfil = new ASSMCA.perfiles.dsPerfil();
            ((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).BeginInit();
            // 
            // daPersona
            // 
            this.daPersona.SelectCommand = this.sqlSelectCommand1;
            this.daPersona.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
                new System.Data.Common.DataTableMapping("Table", "SA_PERSONA", new System.Data.Common.DataColumnMapping[] {
                    new System.Data.Common.DataColumnMapping("PK_Persona", "PK_Persona"),
                    new System.Data.Common.DataColumnMapping("NR_SeguroSocial", "NR_SeguroSocial"),
                    new System.Data.Common.DataColumnMapping("FK_Sexo", "FK_Sexo"),
                    new System.Data.Common.DataColumnMapping("DE_Sexo", "DE_Sexo"),
                    new System.Data.Common.DataColumnMapping("AP_Primero", "AP_Primero"),
                    new System.Data.Common.DataColumnMapping("AP_Segundo", "AP_Segundo"),
                    new System.Data.Common.DataColumnMapping("NB_Primero", "NB_Primero"),
                    new System.Data.Common.DataColumnMapping("NB_Segundo", "NB_Segundo"),
                    new System.Data.Common.DataColumnMapping("FE_Nacimiento", "FE_Nacimiento"),
                    new System.Data.Common.DataColumnMapping("NR_Edad", "NR_Edad"),
                    new System.Data.Common.DataColumnMapping("FK_Veterano", "FK_Veterano"),
                    new System.Data.Common.DataColumnMapping("DE_Veterano", "DE_Veterano"),
                    new System.Data.Common.DataColumnMapping("FK_GrupoEtnico", "FK_GrupoEtnico"),
                    new System.Data.Common.DataColumnMapping("DE_GrupoEtnico", "DE_GrupoEtnico"),
                    new System.Data.Common.DataColumnMapping("FK_Sesion", "FK_Sesion"),
                    new System.Data.Common.DataColumnMapping("TI_Edicion", "TI_Edicion"),
                    new System.Data.Common.DataColumnMapping("FE_Edicion", "FE_Edicion"),
                    new System.Data.Common.DataColumnMapping("NR_Expediente", "NR_Expediente")}),
                    new System.Data.Common.DataTableMapping("Table1", "Table1", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("FK_Raza", "FK_Raza"),
                        new System.Data.Common.DataColumnMapping("DE_Raza", "DE_Raza")})});
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "[SPR_PERSONA]";
            this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
            this.sqlSelectCommand1.Connection = this.cnn;
            this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Persona", System.Data.SqlDbType.Int, 4));
            this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PK_Programa", System.Data.SqlDbType.TinyInt, 1));
            this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IN_Raza", System.Data.SqlDbType.Bit, 1));
            // 
            // cnn
            // 
            this.cnn.ConnectionString = NewSource.connectionString;
            // 
            // dsPerfil
            // 
            this.dsPerfil.DataSetName = "dsPerfil";
            this.dsPerfil.Locale = new System.Globalization.CultureInfo("en-US");
            ((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).EndInit();
        }
        #endregion
        private void ddlGeneroAll()
        {
            NewSource NS = new NewSource();
            DataTable Dt = new DataTable();
            Dt = NS.getAll("SPR_DROP_Genero");
            this.ddlGenero.DataSource = Dt;
            this.ddlGenero.DataValueField = "PK_Genero";
            this.ddlGenero.DataTextField = "DE_Genero";
            this.ddlGenero.ClearSelection();
            try
            {
                this.ddlGenero.DataBind();
            }
            catch
            {
                this.ddlGenero.SelectedValue = "";
            }
            Dt = null;
            NS = null;
        }

        private void ddlTipoDeAdmisionAll()
        {
            NewSource NS = new NewSource();
            DataTable Dt = new DataTable();
            Dt = NS.getAll("SPR_DROP_TipoAdmision");
            this.ddlTipoDeAdmision.DataSource = Dt;
            this.ddlTipoDeAdmision.DataValueField = "PK_TipoAdmision";
            this.ddlTipoDeAdmision.DataTextField = "DE_TipoAdmision";
            this.ddlTipoDeAdmision.DataBind();

            if (UsaTipoDeAdmision(m_PK_Programa))
            {
                this.ddlTipoDeAdmision.Items.RemoveAt(6);
            }
            
            Dt = null;
            NS = null;
        }
        private void ddlMilitarALL()
        {
            NewSource NS = new NewSource();
            DataTable Dt = new DataTable();
            Dt = NS.getAll("SPR_DROP_Militar");
            this.ddlMilitar.DataSource = Dt;
            this.ddlMilitar.DataValueField = "PK_Militar";
            this.ddlMilitar.DataTextField = "DE_Militar";
            try
            {
                this.ddlMilitar.DataBind();
            }
            catch
            {
                this.ddlMilitar.SelectedValue = "";
            }
            Dt = null;
            NS = null;
        }

        public void load()
        {
            ddlGeneroAll();
            ddlMilitarALL();
            if (isAdmision)
            {
                ddlTipoDeAdmisionAll();
            }
            this.setValues();
        }
        public void setValues()
        {
            NewSource NS = new NewSource();
            DataTable Dref = new DataTable();
            Dref = NS.getRef("SPR_NewData", m_pk_perfil);
            if (Dref.Rows.Count > 0)
            {
                ddlMilitar.ClearSelection();
                ddlFamiliaMilitar.ClearSelection();
                ddlGenero.ClearSelection();
                ddlMilitar.SelectedValue = Dref.Rows[0][0].ToString();
                ddlFamiliaMilitar.SelectedValue = Dref.Rows[0][1].ToString();
                ddlGenero.SelectedValue = Dref.Rows[0][2].ToString();
                Dref = null;
                NS = null;
            }
            if (isAdmision)
            {
                NS = new NewSource();
                Dref = new DataTable();
                Dref = NS.getRef("SPR_TipoAdmisionSelectedValue", m_pk_perfil);
                if (Dref.Rows.Count > 0)
                {
                    if (Dref.Rows[0][0].ToString() != "0")
                    {
                        ddlTipoDeAdmision.ClearSelection();
                        ddlTipoDeAdmision.SelectedValue = Dref.Rows[0][0].ToString();
                    }
                    else
                    {
                        ddlTipoDeAdmision.ClearSelection();
                    }
                    Dref = null;
                    NS = null;
                }
            }
        }
        private void SetDDLs(DropDownList d, string val)
        {
            ListItem li;
            for (int i = 0; i < d.Items.Count; i++)
            {
                li = d.Items[i];
                if (li.Value == val)
                {
                    d.SelectedIndex = i;
                    break;
                }
            }
        }

        #region Properties
        public int IN_FamiliaMilitar
        {
            get
            {
                try
                {
                    if (Convert.ToInt32(this.ddlFamiliaMilitar.SelectedValue.ToString()) != 0)
                    {
                        return Convert.ToInt32(this.ddlFamiliaMilitar.SelectedValue.ToString());
                    }
                    else
                    {
                        throw new Exception("No informó");
                    }
                }
                catch
                {
                    return 3; //Default No informó
                }
            }
        }
        public int FK_Genero
        {
            get
            {
                return Convert.ToInt32(this.ddlGenero.SelectedValue.ToString());
            }
        }
        public int FK_TipoAdmision
        {
            get
            {
                try
                {
                    if (this.ddlTipoDeAdmision.SelectedValue.ToString() != "0")
                    {
                        return Convert.ToInt32(this.ddlTipoDeAdmision.SelectedValue.ToString());
                    }
                    else 
                    {
                        throw new Exception("No información");
                    }
                }
                catch
                {
                    return 97;//Default No información
                }
            }
        }
        public int FK_Militar
        {
            get
            {
                try
                {
                    if (Convert.ToInt32(this.ddlMilitar.SelectedValue.ToString()) != 0)
                    {
                        return Convert.ToInt32(this.ddlMilitar.SelectedValue.ToString());
                    }
                    else
                    {
                        throw new Exception("No aplica");
                    }
                }
                catch
                {
                    return 7;//Default No aplica
                }
            }
        }
        //public DateTime FE_Episodio
        //{ 
        //    get
        //    {
        //        string fe = this.ddlMes.SelectedValue.ToString() + "/" + this.ddlDía.SelectedValue.ToString() + "/" + this.txtAño.Text;
        //        return DateTime.Parse(fe);
        //    }
        //}
        // modificado por: strategicconsultingpr. 
        // 2.	Campo Fecha de admisión.
        // De manera similar al punto anterior, si se agrega una fecha de admisión posterior, se genera una alerta, 
        // pero al intentar corregir el campo, ocurre un error.

        public DateTime FE_Episodio
        {
            get
            {
                DateTime dt= new DateTime();
                try
                {
                  dt = new DateTime(Convert.ToInt32(txtAño.Text),
                                           Convert.ToInt32(this.ddlMes.SelectedValue.ToString()), 
                                           Convert.ToInt32(ddlDía.SelectedValue.ToString()));
                return dt;
                }
                catch (Exception)
                {

                    return dt;
                }

            }
        }

        public DateTime FE_Nacimiento
        {
            get
            {
                string fe = this.lblFENacimientoHidden.Value;
                return DateTime.Parse(fe);
            }
        }

        public DateTime? FE_FechaConvenio
        {
            get
            {
                if (tieneConvenio)
                {
                    try
                    {
                        if (isAdmision && (m_frmAction == frmAction.Create || m_frmAction == frmAction.Update))
                        {
                            string fe = this.ddlFechaConvenioMes.SelectedValue.ToString() + "/" + this.ddlFechaConvenioDía.SelectedValue.ToString() + "/" + this.txtFechaConvenioAño.Text;
                            return DateTime.Parse(fe);
                        }
                        else
                        {
                            return DateTime.Parse(lblFechaConvenio.Text);
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        public int PK_Persona
        {
            set
            {
                try
                {
                    this.m_pk_persona = value;
                }
                catch
                { }
            }
        }
        public int PK_Perfil
        {
            set
            {
                try
                {
                    this.m_pk_perfil = value;
                }
                catch { }
            }
        }
        public int PK_Programa
        {
            set
            {
                try
                {
                    this.m_PK_Programa = value;
                }
                catch { }
            }
        }
        public String NR_Expediente
        {
            get
            {
                if (this.txtExpediente.Visible)
                {
                    return this.txtExpediente.Text;
                }
                else
                {
                    return lblExpediente.Text;
                }
            }
        }

        public String NR_CelularPrimario
        {
            get
            {
                if (this.txtCelular1.Visible)
                {
                    return this.txtCelular1.Text;
                }
                else
                {
                    return lblCelular1.Text;
                }
            }
        }

        public String NR_CelularContacto
        {
            get
            {
                if (this.txtCelular2.Visible)
                {
                    return this.txtCelular2.Text;
                }
                else
                {
                    return lblCelular2.Text;
                }
            }
        }

        public String DE_EmailPrimario
        {
            get
            {
                if (this.txtEmail1.Visible)
                {
                    return this.txtEmail1.Text;
                }
                else
                {
                    return lblEmail1.Text;
                }
            }
        }

        public String DE_EmailSecundario
        {
            get
            {
                if (this.txtEmail2.Visible)
                {
                    return this.txtEmail2.Text;
                }
                else
                {
                    return lblEmail2.Text;
                }
            }
        }
        #endregion

        protected void lblEdad_Load(object sender, EventArgs e)
        {
            Session["edad"] = lblEdad.Text;
        }
        public bool TieneConvenio(int PK_PROGRAMA)
        {
            bool tieneConvenio = EsProgramaDesvio(PK_PROGRAMA);
            return tieneConvenio;
        }
        public bool EsProgramaDesvio(int PK_PROGRAMA)
        {
            bool esProgramaDesvio = false;
            switch ((PKPrograma)PK_PROGRAMA)
            {
                case (PKPrograma.TASC_SAN_JUAN):
                case (PKPrograma.TASC_BAYAMÓN):
                case (PKPrograma.TASC_AIBONITO):
                case (PKPrograma.TASC_CAGUAS):
                case (PKPrograma.TASC_MOCA):
                case (PKPrograma.TASC_MAYAGUEZ):
                case (PKPrograma.TASC_GUAYAMA):
                case (PKPrograma.TASC_ARECIBO):
                case (PKPrograma.TASC_FAJARDO):
                case (PKPrograma.TASC_HUMACAO):
                case (PKPrograma.TASC_UTUADO):
                case (PKPrograma.TASC_PONCE):
                case (PKPrograma.TASC_CAROLINA):
                case (PKPrograma.TASC_JUVENIL_SAN_JUAN):
                case (PKPrograma.TASC_JUVENIL_CAGUAS):
                case (PKPrograma.TASC_JUVENIL_ARECIBO):
                case (PKPrograma.TASC_JUVENIL_DE_BAYAMON):
                case (PKPrograma.AMBULATORIO_ADULTOS_ARECIBO):
                case (PKPrograma.AMBULATORIO_ADULTOS_CAROLINA):
                case (PKPrograma.AMBULATORIO_DROGAS_DE_GUAYAMA):
                case (PKPrograma.AMBULATORIO_ADULTOS_HUMACAO):
                case (PKPrograma.AMBULATORIO_ADULTOS_MAYAGUEZ):
                case (PKPrograma.AMBULATORIO_ADULTOS_PONCE):
                case (PKPrograma.AMBULATORIO_ADULTOS_SAN_JUAN):
                case (PKPrograma.PROGRAMA_DE_CORTE_DE_DROGA_DE_ARECIBO):
                case (PKPrograma.PROGRAMA_DE_CORTE_DE_DROGA_DE_BAYAMÓN):
                case (PKPrograma.PROGRAMA_DE_CORTE_DE_DROGA_DE_CAGUAS):
                case (PKPrograma.PROGRAMA_DE_CORTE_DE_DROGA_DE_CAROLINA):
                case (PKPrograma.PROGRAMA_DE_CORTE_DE_DROGA_DE_FAJARDO):
                case (PKPrograma.PROGRAMA_DE_CORTE_DE_DROGA_DE_GUAYAMA):
                case (PKPrograma.PROGRAMA_DE_CORTE_DE_DROGA_DE_HUMACAO):
                case (PKPrograma.PROGRAMA_DE_CORTE_DE_DROGA_DE_MAYAGUEZ):
                case (PKPrograma.PROGRAMA_DE_CORTE_DE_DROGA_DE_PONCE):
                case (PKPrograma.PROGRAMA_DE_CORTE_DE_DROGA_DE_SAN_JUAN):
                    esProgramaDesvio = true; break;
                default: break;
            }
            return esProgramaDesvio;
        }
        protected void lblFENacimiento_Load(object sender, EventArgs e)
        {
            string FechaNacimiento = lblFENacimiento.Text;
            int Tamaño = FechaNacimiento.Length;
            int indice = FechaNacimiento.IndexOf("/", 0);
            int indice2 = FechaNacimiento.LastIndexOf("/", indice);
            int indice3 = FechaNacimiento.LastIndexOf("/", Tamaño);
            Session["Mes"] = FechaNacimiento.Substring(0, indice).ToString();
            Session["dia"] = FechaNacimiento.Substring((indice + 1), (indice2 - 1)).ToString();
            Session["año"] = FechaNacimiento.Substring((indice3 + 1), 4).ToString();
            ddlMesHidden.Value = Session["Mes"].ToString();
            ddlDíaHidden.Value = Session["dia"].ToString();
            txtAñoHidden.Value = Session["año"].ToString();
            this.lblFENacimientoHidden.Value = this.lblFENacimiento.Text;

        }
        private bool UsaTipoDeAdmision(int PK_Programa)
        {
            bool usaTipoDeAdmision = false;
            switch ((PKPrograma)PK_Programa)
            {
               // case (PKPrograma.HOSPITAL_DE_PSIQUIATRÍA_FORENSE_DE_PONCE):
               // case (PKPrograma.HOSPITAL_DE_PSIQUIATRÍA_FORENSE_DE_SAN_JUAN):
                case (PKPrograma.HOSPITAL_DE_PSIQUIATRÍA_GENERAL_DE_RÍO_PIEDRAS):
                    usaTipoDeAdmision = true;
                    break;
                default: break;
            }
            return usaTipoDeAdmision;
        }
        protected void txtAño_TextChanged1(object sender, EventArgs e)
        {
            if (this.txtAño.Text.Trim() != string.Empty)
            {
                if (this.txtAño.Text.Length == 4)
                {
                    
                    DateTime dtnow = new DateTime();
                    dtnow = DateTime.Now;
                    DateTime dt = FE_Episodio;
                    TimeSpan diffResult = dtnow.Date - dt.Date;
                    if ((int)diffResult.Days >= (365 * 2))
                    {
                        this.lblFechaError.Text = "La fecha de admisión fue hace más de dos años.";
                        this.lblFechaError.ForeColor = Color.Blue;
                    }
                    else
                    {
                        this.lblFechaError.Text = "";
                    }

                    // se valida que la fecha no sea mayor a la actual

               

                    try
                    {
                        DateTime TempDate = new DateTime(Convert.ToInt32(txtAño), Convert.ToInt32(ddlMes.SelectedValue), Convert.ToInt32(ddlDía.SelectedValue));
                        if (TempDate > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) )
                        {
                            this.lblFechaError.Text = "La fecha de admisión no puede ser mayor a la fecha actual.";
                            this.lblFechaError.ForeColor = Color.Red;

                        }
                        else
                        {
                            this.lblFechaError.Text = "";
                        }

                    }
                    catch (Exception)
                    {
                          
                    }




                    // edadAdmision.Value = FE_Episodio.ToString();
                }
            }
        }
    }
}

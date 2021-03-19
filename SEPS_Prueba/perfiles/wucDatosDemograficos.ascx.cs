namespace ASSMCA.Perfiles
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Threading;
    public partial class wucDatosDemograficos : System.Web.UI.UserControl
    {
        protected ASSMCA.perfiles.dsPerfil dsPerfil;
        protected System.Data.DataView dvwIngresoIndividual;
        protected System.Data.DataView dvwIngresoFamiliar;
        protected System.Web.UI.WebControls.TextBox txtFeUltimoInfEscolar;
        protected System.Data.DataView dvwFuerzaLaboral;
        public System.Data.DataView dvwResidencia;
        protected System.Data.DataView dvwUltGrado;
        protected System.Data.DataView dvwFemina;
        private int _compFamCount, m_pk_perfil;

        public frmAction m_frmAction;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //ddlResidencia.DataBind();
            if (!this.IsPostBack)
            {
                this.dsPerfil = (ASSMCA.perfiles.dsPerfil)this.Session["dsPerfil"];
                this.dvwFemina.Table = this.dsPerfil.SA_LKP_FEMINA;
                this.dvwIngresoFamiliar.Table = this.dsPerfil.SA_LKP_INGRESO_ANUAL;
                this.dvwIngresoIndividual.Table = this.dsPerfil.SA_LKP_INGRESO_ANUAL;
                this.dvwFuerzaLaboral.Table = this.dsPerfil.SA_LKP_TEDS_NO_FUERZA_LABORAL;
                this.dvwUltGrado.Table = this.dsPerfil.SA_LKP_TEDS_GRADO;

                this.dvwResidencia.Table = this.dsPerfil.SA_LKP_TEDS_RESIDENCIA;

                if (this.Session["pk_administracion"].ToString() == "1")//Niños y adolecentes
                {
                    this.dvwFuerzaLaboral.RowFilter = "PK_NoFuerzaLaboral <> 9";
                    this.dvwUltGrado.RowFilter = "(PK_grado <> 22) AND (PK_grado <> 23) AND (PK_grado <> 24) AND (PK_grado <> 25)";
                }
                if (this.dsPerfil.SA_PERSONA[0]["FK_Sexo"].ToString() == "2" || this.dsPerfil.SA_PERSONA[0]["FK_Sexo"].ToString() == "4") //Femenino o transgenero m->f
                {
                    this.dvwFemina.RowFilter = "PK_Femina <> 99";
                }

                //if (this.Session["co_tipo"].ToString() == "1")
                //{
                //    this.dvwResidencia.RowFilter = "PK_Residencia IN (0,1,2,3,4,7,8,11,13,14,97)";
                //}
        

                this.DataBind();
                load();

                if (this.m_frmAction == frmAction.Create || this.m_frmAction == frmAction.Update)
                {
                    this.EditarRegistro();

                    if (this.m_frmAction == frmAction.Update)
                    {
                        this.ActualizarCampos();
                    }
                }
                else
                {
                    this.LeerRegistro();
                }
            }
            //else
            //{
            //    string CtrlID = string.Empty;

            //    if (Request.Form["__EVENTTARGET"] != null &&

            //        Request.Form["__EVENTTARGET"] != string.Empty)

            //    {

            //        CtrlID = Request.Form["__EVENTTARGET"];
            //        Response.Write("<script>alert('Post Demografico " + CtrlID + "');</script>");

            //    }
                
            //}
            
        }

        private void EditarRegistro()
        {
            dataEdit();
            string Min, Max;
            Min = DateTime.Now.AddYears(-10).Year.ToString();
            Max = DateTime.Now.Year.ToString();
            if (this.dsPerfil.SA_PERSONA[0]["FK_Sexo"].ToString() == "1" || this.dsPerfil.SA_PERSONA[0]["FK_Sexo"].ToString() == "5") //Masculino o Transgenero F-M
            {
                this.ddlFemina.Enabled = false;
                this.ddlFemina.SelectedValue = "99";
            }
            else if (this.dsPerfil.SA_PERSONA[0]["FK_Sexo"].ToString() == "2" || this.dsPerfil.SA_PERSONA[0]["FK_Sexo"].ToString() == "4") //Femenino o Transgenero M-F
            {
                this.ddlVaron.Enabled = false;
                this.ddlVaron.SelectedValue = "99";
            }

            edadAdmisionF(DateTime.Now, this.dsPerfil.SA_PERSONA[0]["FE_Nacimiento"].ToString());
            
        }

        public void edadAdmisionF(DateTime feEpisodio, String feNacimiento)
        {
            bool? ageRange = AgeBetween(3, 17, feEpisodio, feNacimiento);
            bool? ageLessThan3 = AgeLessThan(3, feEpisodio, feNacimiento);   
            bool? ageLessThan18 = AgeLessThan(18, feEpisodio, feNacimiento);

            if (!(ageRange != null && (bool)ageRange) || (ageLessThan3 != null && (bool)ageLessThan3))
            {
                //Situacion Escolar no aplica porque la persona esta entre las edades de 3 a 17.
                ddlSituacionEscolar.Enabled = false;
                ddlSituacionEscolar.SelectedValue = "6";
            }
            else if((ageRange != null && (bool)ageRange) && (ddlSituacionEscolar.SelectedValue == "" || ddlSituacionEscolar.SelectedValue == "6"))
            {
                ddlSituacionEscolar.Enabled = true;
                ddlSituacionEscolar.SelectedValue = "";
            }

            if ((ageLessThan18 != null && (bool)ageLessThan18))
            {
                ddlCondLaboral.SelectedValue = "97";
            }
        
        }

        private bool? AgeBetween(int ageMin, int ageMax, DateTime feEpisodio, String feNacimiento)
        {
            bool result;
            DateTime fechaNacimiento = DateTime.Now;
           
            result = DateTime.TryParse(feNacimiento, out fechaNacimiento);
            

            if (result)
            {
                TimeSpan ts = feEpisodio - fechaNacimiento;
                int yrs = (int)(ts.Days / 365.25f);
                this.edadAdmision.Value = yrs.ToString();
                if ((yrs >= ageMin) && (yrs <= ageMax))
                {
                    
                    return true;
                }
                else
                {   
                    return false;
                }
            }
            else
            {
                return null;
            }
        }

        private bool? AgeLessThan(int age, DateTime feEpisodio, String feNacimiento)
        {
            DateTime fechaNacimiento = DateTime.Now;
            bool result = DateTime.TryParse(feNacimiento, out fechaNacimiento);
            if (result)
            {
                TimeSpan ts = feEpisodio - fechaNacimiento;
                int yrs = (int)(ts.Days / 365.25f);
                if (age > yrs)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return null;
            }
        }

        private void LeerRegistro()
        {
            this.dataRead();
            this.lblSituacionEscolar.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_SituacionEscolar"].ToString(); ;
            this.lblCondLaboral.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_CondLaboral"].ToString();
            this.lblDesertorEscolar.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DesertorEscolar"].ToString();
            this.lblEducacionEspecial.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_EducEspecial"].ToString();
            this.lblEstadoMarital.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_EstadoMarital"].ToString();
            this.lblFemina.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_Femina"].ToString();
            this.lblFuenteIngreso.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_FuenteIngreso"].ToString();
            this.lblGrado.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Grado"].ToString();
            this.lblMunicipio.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_Municipio"].ToString();
            this.lblNoFueraLaboral.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_NoFuerzaLaboral"].ToString();
            this.lblNumFamilia.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_Familiar"].ToString();
            this.lblNumNinos.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_Hijos"].ToString();
            this.lblResidencia.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Residencia"].ToString();
            this.lblTiempoResidencia.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_TiempoResidencia"].ToString();
            this.lblVaron.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_VaronHijos"].ToString();
            try
            {
                this.lblZipCode.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["NR_ZipCode"].ToString();
            }
            catch { }
            this.lblZonaGeografia.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["DE_Zona"].ToString();
            this.ddlCondLaboral.Visible = false;
            this.ddlDesertorEscolar.Visible = false;
            this.ddlEducacionEspecial.Visible = false;
            this.ddlEstadoMarital.Visible = false;
            this.ddlFemina.Visible = false;
            this.ddlFuenteIngreso.Visible = false;
            this.ddlSituacionEscolar.Visible = false;
            this.ddlGrado.Visible = false;
            this.ddlMunicipio.Visible = false;
            this.ddlNoFueraLaboral.Visible = false;
            this.ddlResidencia.Visible = false;
            this.ddlTiempoResidencia.Visible = false;
            this.ddlVaron.Visible = false;
            this.ddlZonaGeografia.Visible = false;
            this.txtNumFamilia.Visible = false;
            this.txtNumNinos.Visible = false;
            this.txtZipCode.Visible = false;
        }

        private void ActualizarCampos()
        {
            dataEdit();
            this.ddlSituacionEscolar.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_SituacionEscolar"].ToString();
            this.ddlCondLaboral.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_CondicionLaboral"].ToString();
            this.ddlDesertorEscolar.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_DesertorEscolar"].ToString();
            this.ddlEducacionEspecial.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EducEspecial"].ToString();
            this.ddlEstadoMarital.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_EstadoMarital"].ToString();
            this.ddlFemina.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_FeminaHijos"].ToString();

            this.ddlFuenteIngreso.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_FuenteIngreso"].ToString();

            this.ddlGrado.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_Escolaridad"].ToString();
            this.ddlMunicipio.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_Municipio"].ToString();
            this.ddlNoFueraLaboral.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_ActividadNoLaboral"].ToString();
            this.ddlResidencia.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_Residencia"].ToString();
            this.ddlTiempoResidencia.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["FK_TiempoResidencia"].ToString();
            this.ddlVaron.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["IN_VaronHijos"].ToString(); ;
            this.ddlZonaGeografia.SelectedValue = this.dsPerfil.SA_EPISODIO.DefaultView[0]["IN_Zona"].ToString();
            this.txtNumFamilia.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_Familiar"].ToString();
            this.txtNumNinos.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_Hijos"].ToString();
            try
            {
                this.txtZipCode.Text = this.dsPerfil.SA_EPISODIO.DefaultView[0]["NR_ZipCode"].ToString();
            }
            catch { }
        }


        private void dataEdit()
        {
            btnAgregar.Visible = true;
            btnEliminar.Visible = true;
        }

        private void dataRead()
        {
            btnAgregar.Visible = false;
            btnEliminar.Visible = false;
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
            this.dvwIngresoIndividual = new System.Data.DataView();
            this.dvwIngresoFamiliar = new System.Data.DataView();
            this.dvwFemina = new System.Data.DataView();
            this.dvwFuerzaLaboral = new System.Data.DataView();
            this.dvwUltGrado = new System.Data.DataView();
            this.dvwResidencia = new System.Data.DataView();
            ((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwIngresoIndividual)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwIngresoFamiliar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFemina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFuerzaLaboral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwUltGrado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwResidencia)).BeginInit();
            // 
            // dsPerfil
            // 
            this.dsPerfil.DataSetName = "dsPerfil";
            this.dsPerfil.Locale = new System.Globalization.CultureInfo("en-US");
            // 
            // dvwIngresoIndividual
            // 
            this.dvwIngresoIndividual.Table = this.dsPerfil.SA_LKP_INGRESO_ANUAL;
            // 
            // dvwIngresoFamiliar
            // 
            this.dvwIngresoFamiliar.Table = this.dsPerfil.SA_LKP_INGRESO_ANUAL;
            ((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwIngresoIndividual)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwIngresoFamiliar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFuerzaLaboral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwUltGrado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFemina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwResidencia)).EndInit();

        }
        #endregion

        public int CompFamItem(int i)
        {
            return Convert.ToInt32(lbxCompFamiliarSeleccionado.Items[i].Value);
        }
        public int CompFamCount
        {
            get
            {
                _compFamCount = lbxCompFamiliarSeleccionado.Items.Count;
                return _compFamCount;
            }
        }
        public int PK_Perfil
        {
            set
            {
                this.m_pk_perfil = value;
            }
        }

        private void lbxCompFamiliar()
        {
            if (m_frmAction == frmAction.Read)
            {
                string selectedValuesString = "";
                NewSource NS = new NewSource();
                DataTable Dref = new DataTable();
                Dref = NS.getRef("SPR_Ref_CompFamilia", m_pk_perfil);
                if (Dref.Rows.Count > 0)
                {
                    foreach (DataRow r in Dref.Rows)
                    {
                        selectedValuesString += r[1].ToString() + ", ";
                    }
                    selectedValuesString = selectedValuesString.Substring(0, selectedValuesString.Length - 2);
                    Dref = null;
                }
                else
                {
                    selectedValuesString = "No hay valores seleccionados";
                }
                divCompFamiliar.Visible = false;
                lblCompFamiliar.Text = selectedValuesString;
                NS = null;
            }
            else
            {
                NewSource NS = new NewSource();
                DataTable Dt = new DataTable();
                Dt = NS.getAll("SPR_DROP_CompFamiliar");
                this.lbxCompFamiliarSeleccion.DataSource = Dt;
                this.lbxCompFamiliarSeleccion.DataValueField = "PK_Familiar";
                this.lbxCompFamiliarSeleccion.DataTextField = "DE_Familiar";
                this.lbxCompFamiliarSeleccion.DataBind();
                DataTable Dref = new DataTable();
                Dref = NS.getRef("SPR_Ref_CompFamilia", m_pk_perfil);
                if (Dref.Rows.Count > 0)
                {
                    foreach (DataRow r in Dref.Rows)
                    {
                        System.Web.UI.WebControls.ListItem li = new ListItem(r[1].ToString(), r[0].ToString());
                        this.lbxCompFamiliarSeleccionado.Items.Add(li);
                        this.lbxCompFamiliarSeleccion.Items.Remove(li);
                    }
                    Dref = null;
                }
                Dt = null;
                NS = null;
                divLblCompFamiliar.Visible = false;
            }
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            lbxCompFamiliarSeleccion.Focus();


            //Thread.Sleep(5000);

            if (this.lbxCompFamiliarSeleccion.SelectedItem != null)
            {
                System.Web.UI.WebControls.ListItem li = new ListItem(this.lbxCompFamiliarSeleccion.SelectedItem.Text, this.lbxCompFamiliarSeleccion.SelectedItem.Value);
                if (Convert.ToInt32(this.lbxCompFamiliarSeleccion.SelectedItem.Value) == 13)
                {
                    if (this.lbxCompFamiliarSeleccionado.Items.Count > 0)
                    {
                        this.lbxCompFamiliarSeleccionado.Items.Clear();
                    }
                    this.lbxCompFamiliarSeleccion.Enabled = false;
                }
                this.lbxCompFamiliarSeleccionado.Items.Add(li);
                this.lbxCompFamiliarSeleccion.Items.Remove(li);
                SortListBox(this.lbxCompFamiliarSeleccionado);
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.lbxCompFamiliarSeleccionado.SelectedItem != null)
            {
                System.Web.UI.WebControls.ListItem li = new ListItem(this.lbxCompFamiliarSeleccionado.SelectedItem.Text, this.lbxCompFamiliarSeleccionado.SelectedItem.Value);
                this.lbxCompFamiliarSeleccion.Items.Add(li);
                if (Convert.ToInt32(this.lbxCompFamiliarSeleccionado.SelectedItem.Value) == 13)
                {
                    lbxCompFamiliar();
                    this.lbxCompFamiliarSeleccion.Enabled = true;
                }
                this.lbxCompFamiliarSeleccionado.Items.Remove(li);
                SortListBox(this.lbxCompFamiliarSeleccion);
            }
        }
        private void SortListBox(ListBox listBox)
        {
            SortedList<string, string> list = new SortedList<string, string>();
            foreach (ListItem i in listBox.Items)
            {
                list.Add(i.Text, i.Value);
            }
            listBox.Items.Clear();
            foreach (KeyValuePair<string, string> i in list)
            {
                listBox.Items.Add(new ListItem(i.Key, i.Value));
            }
        }


        #region Propiedades del Episodio

        public sbyte FK_FeminaHijos
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlFemina.SelectedValue.ToString());
                }
                catch
                {
                    return 99;//Default No aplica
                }
            }
        }

        public sbyte IN_VaronHijos
        {
            get
            {
                try
                {
                    sbyte val = Convert.ToSByte(this.ddlVaron.SelectedValue.ToString());
                    if (val == 0)
                    {
                        return 99;//Default No aplica
                    }
                    else
                    {
                        return val;
                    }
                }
                catch
                {
                    return 99;//Default No aplica

                }
            }
        }

        public sbyte FK_FuenteIngreso
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlFuenteIngreso.SelectedValue.ToString());
                }
                catch
                {
                    return 97;//Default No informó
                }
            }
        }

        public sbyte FK_TiempoResidencia
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlTiempoResidencia.SelectedValue.ToString());
                }
                catch
                {
                    return 95;//No informó
                }
            }
        }

        public short FK_Municipio
        {
            get
            {
                try
                {
                    return Convert.ToInt16(this.ddlMunicipio.SelectedValue.ToString());
                }
                catch
                {
                    return 96;//Default No informó
                }
            }
        }

        public sbyte IN_Zona
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlZonaGeografia.SelectedValue.ToString());
                }
                catch
                {
                    return 3;//Default No informó
                }
            }
        }

        public string NR_ZipCode
        {
            get
            {
                try
                {
                    if (txtZipCode.Text.Trim() != "")
                    {
                        return this.txtZipCode.Text.Trim();
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }
        #endregion

        #region Propiedades del Perfil

        public sbyte @FK_EstadoMarital
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlEstadoMarital.SelectedValue.ToString());
                }
                catch
                {
                    return 96;//Default No informó
                }
            }
        }

        public sbyte @FK_CondicionLaboral
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlCondLaboral.SelectedValue.ToString());
                }
                catch
                {
                    return 97;//Default No informó
                }
            }
        }

        public sbyte @FK_ActividadNoLaboral
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlNoFueraLaboral.SelectedValue.ToString());
                }
                catch
                {
                    return 96;//Default No informó
                }
            }
        }
        public int @NR_Hijos
        {
            get
            {
                try
                {
                    if (this.txtNumNinos.Text.Trim() == "")
                    {
                        return 0;//Default 0 hijos
                    }
                    else
                    {
                        return Convert.ToInt16(this.txtNumNinos.Text.Trim());
                    }
                }
                catch
                {
                    return 0;//Default 0 hijos
                }
            }
        }

        public sbyte @FK_Escolaridad
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlGrado.SelectedValue.ToString());
                }
                catch
                {
                    return 96;//Default No informó
                }
            }
        }

        public int @FK_SituacionEscolar
        {
            get
            {
                try
                {
                    return Convert.ToInt32(this.ddlSituacionEscolar.SelectedValue.ToString());
                }
                catch
                {
                    return 7;//Default Desconocido
                }
            }
        }

        public sbyte @IN_EducEspecial
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlEducacionEspecial.SelectedValue.ToString());
                }
                catch
                {
                    return 99;//Default No aplica
                }
            }
        }

        public sbyte @IN_DesertorEscolar
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlDesertorEscolar.SelectedValue.ToString());
                }
                catch
                {
                    return 99;//Default No aplica
                }
            }
        }

        public List<int> @FK_Familia
        {
            get
            {
                List<int> Lst_familiares = new List<int>();
                foreach (ListItem i in lbxCompFamiliarSeleccionado.Items)
                {
                    Lst_familiares.Add(int.Parse(i.Value));
                }
                return Lst_familiares;
            }
        }

        public sbyte @NR_Familiar
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.txtNumFamilia.Text.Trim());
                }
                catch
                {
                    return 0;//Default 0 familia
                }
            }
        }

        public sbyte @FK_Residencia
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlResidencia.SelectedValue.ToString());
                }
                catch
                {
                    return 97;//Default No informó
                }
            }
        }

        #endregion

        public void load()
        {
            lbxCompFamiliar();
        }

        protected void ddlDesertorEscolar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDesertorEscolar.SelectedIndex != -1)
            {
                ViewState["ddlDesertorEscolar"] = ddlDesertorEscolar.SelectedValue;
            }
        }

        protected void ddlGrado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(ddlGrado.SelectedValue))
                {

                    switch (ddlGrado.SelectedValue)
                    {
                        case ("12")://Diploma de escuela superior
                        case ("14")://Créditos universitarios
                        case ("16")://Curso vocacional
                        case ("22")://Grado asociado
                        case ("23")://Bachillerato
                        case ("24")://Maestría
                        case ("25")://Doctorado
                                    // No es desertor

                            ViewState["ddlDesertorEscolar"] = 2;
                            ddlDesertorEscolar.SelectedValue = "2";

                            ddlDesertorEscolar.Enabled = false;
                            break;
                        case ("96")://No informo
                                    // No aplica
                            ViewState["ddlDesertorEscolar"] = 99;
                            ddlDesertorEscolar.SelectedValue = "99";

                            ddlDesertorEscolar.Enabled = false;
                            break;
                        case ("13")://Ninguna
                        case ("26")://Pre-escolar
                        case ("27")://Kindergarten
                        case ("1")://Primero
                        case ("2")://Segundo
                        case ("3")://Tercero
                        case ("4")://Cuarto
                        case ("5")://Quinto
                        case ("6")://Sexto
                        case ("7")://Séptimo
                        case ("8")://Octavo
                        case ("9")://Noveno
                        case ("10")://Décimo
                        case ("11")://Undécimo
                                    //Es desertor
                            ViewState["ddlDesertorEscolar"] = 1;
                            ddlDesertorEscolar.SelectedValue = "1";

                            ddlDesertorEscolar.Enabled = false;
                            break;
                    }
                    ViewState["ddlGrado"] = ddlGrado.SelectedValue;
                }
            }
            catch { }
 
        }

        protected void ddlFemina_Load(object sender, EventArgs e)
        {
            if (ddlFemina.SelectedValue == "99")
            {
                ddlVaron.Items[3].Enabled = false;
            }
            else
            {
                ddlVaron.Items[3].Enabled = true;
            }
        }
    }
}

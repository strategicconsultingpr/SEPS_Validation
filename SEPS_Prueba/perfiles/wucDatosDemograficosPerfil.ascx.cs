namespace ASSMCA.Perfiles
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    public partial class wucDatosDemograficosPerfil : System.Web.UI.UserControl
    {
        public frmAction m_frmAction;
        protected ASSMCA.perfiles.dsPerfil dsPerfil;
        protected System.Data.DataView dvwFuerzaLaboral;
        protected System.Data.DataView dvwUltGrado;
        protected System.Data.DataView dvwFreqAutoAyuda;
        protected System.Data.DataView dvwResidencia;
        protected System.Web.UI.WebControls.DropDownList dllEmbarazosTratamiento;
        protected System.Web.UI.WebControls.RangeValidator rvArrestosTotal;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvArrestosTotal;
        protected System.Web.UI.WebControls.Label lblArrestosTotal;
        protected System.Web.UI.WebControls.TextBox txtArrestosTotal;
        protected System.Web.UI.WebControls.Label Label64;
        private int _compFamCount, m_pk_perfil;

        protected void Page_Load(object sender, System.EventArgs e)
        {

            if (!this.IsPostBack)
            {
                this.dsPerfil = (ASSMCA.perfiles.dsPerfil)this.Session["dsPerfil"];
                this.dvwFuerzaLaboral.Table = this.dsPerfil.SA_LKP_TEDS_NO_FUERZA_LABORAL;
                this.dvwUltGrado.Table = this.dsPerfil.SA_LKP_TEDS_GRADO;
                this.dvwFreqAutoAyuda.Table = this.dsPerfil.SA_LKP_TEDS_FRECUENCIA_AUTOAYUDA;
                this.dvwResidencia.Table = this.dsPerfil.SA_LKP_TEDS_RESIDENCIA;

                if (this.Session["pk_administracion"].ToString() == "1")//Niños y adolecentes
                {
                    this.dvwFuerzaLaboral.RowFilter = "PK_NoFuerzaLaboral <> 9";
                    this.dvwUltGrado.RowFilter = "(PK_grado <> 22) AND (PK_grado <> 23) AND (PK_grado <> 24) AND (PK_grado <> 25)";
                }
                if (this.Session["co_tipo"].ToString() == "1")
                {
                    this.dvwResidencia.RowFilter = "PK_Residencia IN (0,1,2,3,4,7,8,11,13,14)";
                }
                this.DataBind();
                lbxCompFamiliar();//multi
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
        private void SortListBox(ListBox listBox)//Added for listbox sorting
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
        private void EditarRegistro()
        {
            dataEdit();
            string Min, Max;
            Min = DateTime.Now.AddYears(-10).Year.ToString();
            Max = DateTime.Now.Year.ToString();

            edadPerfilF(DateTime.Now, this.dsPerfil.SA_PERSONA[0]["FE_Nacimiento"].ToString());

            //bool? ageRange = AgeBetween(3, 17);
            //bool? ageLessThan3 = AgeLessThan(3);
            //bool? ageLessThan18 = AgeLessThan(18);
            //if (!(ageRange != null && (bool)ageRange))
            //{
            //    //Situacion Escolar no aplica porque la persona esta entre las edades de 3 a 17.
            //    ddlSituacionEscolar.Enabled = false;
            //    ddlSituacionEscolar.SelectedValue = "6";
            //}
            //if ((ageLessThan18 != null && (bool)ageLessThan18))
            //{
            //    ddlCondLaboral.SelectedValue = "97";
            //}

            //if ((ageLessThan3 != null && (bool)ageLessThan3))
            //{
            //    ddlSituacionEscolar.Enabled = false;
            //    ddlSituacionEscolar.SelectedValue = "6";
            //    ddlGrado.Enabled = false;
            //    ddlGrado.SelectedValue = "13";
            //    ddlDesertorEscolar.Enabled = false;
            //    ddlDesertorEscolar.SelectedValue = "99";
            //    ddlEducacionEspecial.SelectedValue = "99";
            //}
        }

        public void edadPerfilF(DateTime feEpisodio, String feNacimiento)
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
            else if ((ageRange != null && (bool)ageRange) && (ddlSituacionEscolar.SelectedValue == "" || ddlSituacionEscolar.SelectedValue == "6"))
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
                this.edadPerfil.Value = yrs.ToString();
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
            this.lblArrestado.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Arrestado30dias"].ToString();
            this.lblArrestos30.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_Arrestos30dias"].ToString();
            this.lblSituacionEscolar.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_SituacionEscolar"].ToString(); ;
            this.lblCondLaboral.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_CondLaboral"].ToString();
            this.lblDesertorEscolar.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_DesertorEscolar"].ToString();
            this.lblEducacionEspecial.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_EducEspecial"].ToString();
            this.lblEstadoMarital.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_EstadoMarital"].ToString();
            this.lblGrado.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Grado"].ToString();
            this.lblNoFueraLaboral.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_NoFuerzaLaboral"].ToString();
            this.lblNumFamilia.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_Familiar"].ToString();
            this.lblNumNinos.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_Hijos"].ToString();
            this.lblResidencia.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_Residencia"].ToString();
            this.lblFreq_AutoAyuda.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["DE_FreqAutoAyuda"].ToString();
            this.ddlArrestado.Visible = false;
            this.ddlCondLaboral.Visible = false;
            this.ddlSituacionEscolar.Visible = false;
            this.ddlDesertorEscolar.Visible = false;
            this.ddlEducacionEspecial.Visible = false;
            this.ddlEstadoMarital.Visible = false;
            this.ddlGrado.Visible = false;
            this.ddlNoFueraLaboral.Visible = false;
            this.ddlResidencia.Visible = false;
            this.ddlFreq_AutoAyuda.Visible = false;
            this.txtArrestos30.Visible = false;
            this.txtNumFamilia.Visible = false;
            this.txtNumNinos.Visible = false;
        }

        private void ActualizarCampos()
        {
            dataEdit();
            this.ddlSituacionEscolar.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_SituacionEscolar"].ToString();
            this.ddlArrestado.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_Arrestado30dias"].ToString();
            this.ddlCondLaboral.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_CondicionLaboral"].ToString(); ;
            this.ddlDesertorEscolar.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_DesertorEscolar"].ToString(); ;
            this.ddlEducacionEspecial.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["IN_EducEspecial"].ToString();
            this.ddlEstadoMarital.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_EstadoMarital"].ToString();
            this.ddlGrado.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_Escolaridad"].ToString();
            this.ddlNoFueraLaboral.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_ActividadNoLaboral"].ToString();
            this.ddlResidencia.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_Residencia"].ToString();
            this.ddlFreq_AutoAyuda.SelectedValue = this.dsPerfil.SA_PERFIL.DefaultView[0]["FK_FreqAutoAyuda"].ToString();
            this.txtArrestos30.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_Arrestos30dias"].ToString();
            this.txtNumFamilia.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_Familiar"].ToString();
            this.txtNumNinos.Text = this.dsPerfil.SA_PERFIL.DefaultView[0]["NR_Hijos"].ToString();
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
            this.dvwFuerzaLaboral = new System.Data.DataView();
            this.dvwUltGrado = new System.Data.DataView();
            this.dvwFreqAutoAyuda = new System.Data.DataView();
            this.dvwResidencia = new System.Data.DataView();
            ((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFuerzaLaboral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwUltGrado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFreqAutoAyuda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwResidencia)).BeginInit();
            // 
            // dsPerfil
            // 
            this.dsPerfil.DataSetName = "dsPerfil";
            this.dsPerfil.Locale = new System.Globalization.CultureInfo("en-US");
            ((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFuerzaLaboral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwUltGrado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwFreqAutoAyuda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvwResidencia)).EndInit();

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
                    return 97;//Default No informo
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
                    return 7; //Default Desconocido
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
                    return 96;//Default No informo
                }
            }
        }

        public sbyte @NR_Hijos
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.txtNumNinos.Text.Trim());
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
                    return 96;//Default No informo

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

        public int @IN_DesertorEscolar
        {
            get
            {
                try
                {
                    return Convert.ToInt16(this.ddlDesertorEscolar.SelectedValue.ToString());
                }
                catch
                {
                    return 99; //Default No aplica
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
                    return 0;//Default 0 Familiares
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
                   return 97; //Default No informo
                }
            }
        }
        public byte IN_Arrestado30dias
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.ddlArrestado.SelectedValue.ToString());
                }
                catch
                {
                    return 2;//Default No
                }
            }
        }

        public sbyte @NR_Arrestos30dias
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.txtArrestos30.Text.Trim());
                }
                catch
                {
                    return 0;//Default 0 arrestos 30 días
                }
            }
        }

        public byte NR_TotalArrestosPasado
        {
            get
            {
                try
                {
                    return Convert.ToByte(this.txtArrestosTotal.Text.Trim());
                }
                catch
                {
                    return 0;//Default 0 arrestos pasados
                }
            }
        }

        public sbyte @FK_FreqAutoAyuda
        {
            get
            {
                try
                {
                    return Convert.ToSByte(this.ddlFreq_AutoAyuda.SelectedValue.ToString());
                }
                catch
                {
                    return 99; //Default No aplica
                }
            }
        }
        #endregion
    }
}
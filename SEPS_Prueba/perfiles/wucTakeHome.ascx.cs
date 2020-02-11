using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class Perfiles_wucTakeHome : System.Web.UI.UserControl
{
    public frmAction m_frmAction;
    private int m_pk_perfil = 0;
    private int m_pk_programa = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!EsProgramaMetadona(m_pk_programa))
            {
                this.HideTakeHome();
            }
            else
            {
                ddlEtapa();
                DdlFrecuenciaBotellas();
                this.setValues();
                lbxRazon();
                if (this.m_frmAction == frmAction.Create || this.m_frmAction == frmAction.Update)
                {
                    dataEdit();
                }
                else
                {
                    dataRead();
                }
            }
        }
    }

    private void DdlFrecuenciaBotellas()
    {
        NewSource NS = new NewSource();
        DataTable Dt = new DataTable();
        Dt = NS.getAll("SPR_DROP_FrecuenciaBotellasTH");
        this.ddlFrecuenciaBotellas.DataSource = Dt;
        this.ddlFrecuenciaBotellas.DataValueField = "PK_FrecuenciaBotellas";
        this.ddlFrecuenciaBotellas.DataTextField = "DE_FrecuenciaBotellas";
     
        try
        {
            this.ddlFrecuenciaBotellas.DataBind();
            ListItem li = new ListItem("", "0");
            this.ddlFrecuenciaBotellas.Items.Insert(0, li);
        }
        catch (Exception ex)
        {
            Trace.Write("DdlFrecuenciaBotellas()::" + ex.Message);
            throw ex;
        }
        Dt = null;
        NS = null;
    }

    private void HideTakeHome()
    {
        divTakeHome.Visible = false;
        divTakeHome.Disabled = true;
    }

    private void dataEdit()
    {
        ddlFechaEntradaDía.Visible = true;
        ddlFechaEntradaMes.Visible = true;
        txtFechaEntradaAño.Visible = true;
        ddlFechaSalidaDía.Visible = true;
        ddlFechaSalidaMes.Visible = true;
        txtFechaSalidaAño.Visible = true;
        ddlTHBelong.Visible = true;
        ddlTHEtapa.Visible = true;
        btnAgregar.Visible = true;
        btnEliminar.Visible = true;
    }

    private void dataRead()
    {
        ddlFechaEntradaDía.Visible = false;
        ddlFechaEntradaMes.Visible = false;
        txtFechaEntradaAño.Visible = false;
        ddlFechaSalidaDía.Visible = false;
        ddlFechaSalidaMes.Visible = false;
        txtFechaSalidaAño.Visible = false;
        ddlTHBelong.Visible = false;
        ddlTHEtapa.Visible = false;
        btnAgregar.Visible = false;
        btnEliminar.Visible = false;
        ddlFrecuenciaBotellas.Visible = false;
        txtCantidadBotellas.Visible = false;
        if (ddlTHEtapa.SelectedItem.Text != "1")
        {
            lblTHEtapa.Text = ddlTHEtapa.SelectedItem.Text;
        }
        lblTHBelong.Text = ddlTHBelong.SelectedItem.Text;
    }

    private void ddlEtapa()
    {
        NewSource NS = new NewSource();
        DataTable Dt = new DataTable();
        Dt = NS.getAll("SPR_DROP_EtapaTH");
        this.ddlTHEtapa.DataSource = Dt;
        this.ddlTHEtapa.DataValueField = "PK_Etapa";
        this.ddlTHEtapa.DataTextField = "DE_Etapa";
        try
        {
            this.ddlTHEtapa.DataBind();
            ListItem li = new ListItem("", "0");
            this.ddlTHEtapa.Items.Insert(0, li);

        }
        catch (Exception ex)
        {
            Trace.Write("DdlFrecuenciaBotellas()::" + ex.Message);
            throw ex;
        }
        Dt = null;
        NS = null;
    }

    public void setValues()
    {
        NewSource NS = new NewSource();
        DataTable Dref = NS.getRef("SPR_METADONA", m_pk_perfil);
        try
        {
            if (Dref.Rows.Count > 0)
            {
                if (Dref.Rows[0][0] != DBNull.Value)
                {
                    this.ddlTHBelong.SelectedValue = Dref.Rows[0][0].ToString();
                }
                if (Dref.Rows[0][1] != DBNull.Value)
                {
                    this.ddlTHEtapa.SelectedValue = Dref.Rows[0][1].ToString();
                }
                DateTime date;
                if (Dref.Rows[0][2] != DBNull.Value)
                {
                    date = (DateTime)Dref.Rows[0][2];
                    this.ddlFechaEntradaMes.SelectedValue = date.Month.ToString();
                    this.ddlFechaEntradaDía.SelectedValue = date.Day.ToString();
                    this.txtFechaEntradaAño.Text = date.Year.ToString();
                }
                if (Dref.Rows[0][3] != DBNull.Value)
                {
                    date = (DateTime)Dref.Rows[0][3];
                    this.ddlFechaSalidaMes.SelectedValue = date.Month.ToString();
                    this.ddlFechaSalidaDía.SelectedValue = date.Day.ToString();
                    this.txtFechaSalidaAño.Text = date.Year.ToString();
                }
                if (Dref.Rows[0][4] != DBNull.Value)
                {
                    this.txtCantidadBotellas.Text = Dref.Rows[0][4].ToString();
                }
                if (Dref.Rows[0][5] != DBNull.Value)
                {
                    this.ddlFrecuenciaBotellas.SelectedValue = Dref.Rows[0][5].ToString();
                }
                if (m_frmAction == frmAction.Read)
                {
                    if (Dref.Rows[0][2] != DBNull.Value)
                    {
                        lblFE_In.Text = ((DateTime)Dref.Rows[0][2]).ToShortDateString();
                    }
                    else
                    {
                        lblFE_In.Text = "No disponible";
                    }
                    if (Dref.Rows[0][3] != DBNull.Value)
                    {
                        lblFE_Out.Text = ((DateTime)Dref.Rows[0][3]).ToShortDateString();
                    }
                    else
                    {
                        lblFE_Out.Text = "No dispobible";
                    }
                    if (Dref.Rows[0][4] != DBNull.Value)
                    {
                        this.lblCantidadBotellas.Text = Dref.Rows[0][4].ToString();
                    }
                    if (Dref.Rows[0][6] != DBNull.Value)
                    {
                        this.lblFrecuenciaBotellas.Text = Dref.Rows[0][6].ToString();
                    }
                }

                switch (ddlTHBelong.SelectedValue)
                {
                    case ("1"):
                        lbxRazonSeleccion.Attributes.Add("disabled", "true");
                        lbxRazonSeleccionado.Attributes.Add("disabled", "true");
                        btnAgregar.Enabled = false;
                        btnEliminar.Enabled = false;
                        ddlFechaEntradaDía.Enabled = true;
                        ddlFechaEntradaMes.Enabled = true;
                        ddlFechaSalidaDía.Enabled = true;
                        ddlFechaSalidaMes.Enabled = true;
                        ddlFrecuenciaBotellas.Enabled = true;
                        ddlTHEtapa.Enabled = true;
                        txtCantidadBotellas.Enabled = true;
                        txtFechaEntradaAño.Enabled = true;
                        txtFechaSalidaAño.Enabled = true;
                        break;
                    case ("2"):
                        lbxRazonSeleccion.Attributes.Remove("disabled");
                        lbxRazonSeleccionado.Attributes.Remove("disabled");
                        btnEliminar.Enabled = true;
                        btnAgregar.Enabled = true;
                        ddlFechaEntradaDía.Enabled = false;
                        ddlFechaEntradaMes.Enabled = false;
                        ddlFechaSalidaDía.Enabled = false;
                        ddlFechaSalidaMes.Enabled = false;
                        ddlFrecuenciaBotellas.Enabled = false;
                        ddlTHEtapa.Enabled = false;
                        txtCantidadBotellas.Enabled = false;
                        txtFechaEntradaAño.Enabled = false;
                        txtFechaSalidaAño.Enabled = false;
                        break;
                    default:
                        lbxRazonSeleccion.Attributes.Add("disabled", "true");
                        lbxRazonSeleccionado.Attributes.Add("disabled", "true");
                        btnAgregar.Enabled = false;
                        btnEliminar.Enabled = false;
                        ddlFechaEntradaDía.Enabled = false;
                        ddlFechaEntradaMes.Enabled = false;
                        ddlFechaSalidaDía.Enabled = false;
                        ddlFechaSalidaMes.Enabled = false;
                        ddlFrecuenciaBotellas.Enabled = false;
                        ddlTHEtapa.Enabled = false;
                        txtCantidadBotellas.Enabled = false;
                        txtFechaEntradaAño.Enabled = false;
                        txtFechaSalidaAño.Enabled = false;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Trace.Write("setValues()::" + ex.Message);
            throw ex;
        }
        Dref = null;
        NS = null;

    }
    private void lbxRazon()
    {
        if (m_frmAction == frmAction.Read)
        {
            if (ddlTHBelong.SelectedValue == "2"/*=No*/)
            {
                string selectedValuesString = "";
                NewSource NS = new NewSource();
                DataTable Dref = new DataTable();
                Dref = NS.getRef("SPR_Ref_RazonTH", m_pk_perfil);
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
                NS = null;
                divRazon.Visible = false;
                lblRazon.Text = selectedValuesString;
            }
            else
            {
                divRazon.Visible = false;
                divLblRazon.Visible = false;
            }
        }
        else
        {
            NewSource NS = new NewSource();
            DataTable Dt = new DataTable();
            Dt = NS.getAll("SPR_DROP_RazonTH");
            this.lbxRazonSeleccion.DataSource = Dt;
            this.lbxRazonSeleccion.DataValueField = "PK_Razon";
            this.lbxRazonSeleccion.DataTextField = "DE_Razon";
            this.lbxRazonSeleccion.DataBind();
            DataTable Dref = new DataTable();
            Dref = NS.getRef("SPR_Ref_RazonTH", m_pk_perfil);
            if (Dref.Rows.Count > 0)
            {
                foreach (DataRow r in Dref.Rows)
                {
                    System.Web.UI.WebControls.ListItem li = new ListItem(r[1].ToString(), r[0].ToString());
                    this.lbxRazonSeleccionado.Items.Add(li);
                    this.lbxRazonSeleccion.Items.Remove(li);
                }
                Dref = null;
            }
            Dt = null;
            NS = null;
            divLblRazon.Visible = false;
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
                this.m_pk_programa = value;
            }
            catch { }
        }
    }
    public int? RazonTHItem(int i)
    {
        try
        {
            return Convert.ToInt32(lbxRazonSeleccionado.Items[i].Value);
        }
        catch
        {
            return null;
        }
    }
    public int? RazonTHCount
    {
        get
        {
            try
            {
                return lbxRazonSeleccionado.Items.Count;
            }
            catch
            {
                return 0;
            }
        }
    }

    public int? NR_CantidadBotellas
    {
        get
        {
            try
            {
                if (ddlTHBelong.SelectedValue.ToString() == "1"/*Si*/)
                {
                    return Convert.ToInt32(txtCantidadBotellas.Text);
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

    public int? FK_FrecuenciaBotellas
    {
        get
        {
            try
            {
                if (ddlTHBelong.SelectedValue.ToString() == "1"/*Si*/&& ddlFrecuenciaBotellas.SelectedValue.ToString()!="0")
                {
                    return Convert.ToInt32(ddlFrecuenciaBotellas.SelectedValue.ToString());
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

    public int? EtapaTH
    {
        get
        {
            try
            {
                if (ddlTHBelong.SelectedValue.ToString() == "1"/*Si*/&& ddlTHEtapa.SelectedValue.ToString()!="0")
                {
                    return int.Parse(this.ddlTHEtapa.SelectedValue.ToString());
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

    public int? THBelong
    {
        get
        {
            try
            {
                return int.Parse(this.ddlTHBelong.SelectedValue.ToString());
            }
            catch
            {
                return null;
            }
        }
    }
    public DateTime? FE_THIni
    {
        get
        {
            if (Regex.IsMatch(this.txtFechaEntradaAño.Text, @"^\d+$"))
            {
                return DateTime.Parse(this.ddlFechaEntradaMes.SelectedValue.ToString() + "/" + this.ddlFechaEntradaDía.SelectedValue.ToString() + "/" + this.txtFechaEntradaAño.Text);
            }
            else
            {
                return null;
            }
        }
    }
    public DateTime? FE_THFin
    {
        get
        {
            if (Regex.IsMatch(this.txtFechaSalidaAño.Text, @"^\d+$"))
            {
                return DateTime.Parse(this.ddlFechaSalidaMes.SelectedValue.ToString() + "/" + this.ddlFechaSalidaDía.SelectedValue.ToString() + "/" + this.txtFechaSalidaAño.Text);
            }
            else
            {
                return null;
            }
        }
    }
    protected void btnAgregar_Click(object sender, System.EventArgs e)
    {       
        if (this.lbxRazonSeleccion.SelectedItem != null)
        {
            System.Web.UI.WebControls.ListItem li = new ListItem(this.lbxRazonSeleccion.SelectedItem.Text, this.lbxRazonSeleccion.SelectedItem.Value);
            this.lbxRazonSeleccionado.Items.Add(li);
            this.lbxRazonSeleccion.Items.Remove(li);
            SortListBox(this.lbxRazonSeleccionado);
        } 
        
    }

    protected void btnEliminar_Click(object sender, System.EventArgs e)
    {
        if (this.lbxRazonSeleccionado.SelectedItem != null)
        {
            System.Web.UI.WebControls.ListItem li = new ListItem(this.lbxRazonSeleccionado.SelectedItem.Text, this.lbxRazonSeleccionado.SelectedItem.Value);
            this.lbxRazonSeleccion.Items.Add(li);
            this.lbxRazonSeleccionado.Items.Remove(li);
            SortListBox(this.lbxRazonSeleccion);
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
    protected void btnIterar_Click(object sender, System.EventArgs e)
    {
        if (this.RazonTHCount > 0)
        {
            lbxRazonSeleccion.Items.Clear();
            for (int i = 0; i < this.RazonTHCount; i++)
            {
                System.Web.UI.WebControls.ListItem li = new ListItem(this.RazonTHItem(i).ToString(), this.RazonTHItem(i).ToString());
                this.lbxRazonSeleccion.Items.Add(li);
            }
            SortListBox(this.lbxRazonSeleccion);
        }

    }

    public bool EsProgramaMetadona(int PK_PROGRAMA)
    {
        bool esProgramaMetadona = false;
        switch ((PKPrograma)PK_PROGRAMA)
        {
            case (PKPrograma.CENTRO_CON_MANTENIMIENTO_CON_METADONA_DE_SAN_JUAN):     // PK_Programa =  1
            case (PKPrograma.CENTRO_CON_MANTENIMIENTO_CON_METADONA_DE_CAGUAS):       // PK_Programa =  2
            case (PKPrograma.CENTRO_CON_MANTENIMIENTO_CON_METADONA_DE_PONCE):        // PK_Programa =  3
            case (PKPrograma.CENTRO_CON_MANTENIMIENTO_CON_METADONA_DE_AGUADILLA):    // PK_Programa =  4
            case (PKPrograma.CENTRO_CON_MANTENIMIENTO_CON_METADONA_DE_BAYAMÓN):      // PK_Programa =  6
            case (PKPrograma.CENTRO_CON_MANTENIMIENTO_CON_METADONA_DE_CAYEY):        // PK_Programa = 43
                esProgramaMetadona = true; break;
            default: break;
        }
        return esProgramaMetadona;
    }

    protected void ddlTHBelong_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTHBelong.SelectedValue == "2")
        {
            //SortListBox(this.lbxRazonSeleccion);
            SortListBox(this.lbxRazonSeleccionado);
            lbxRazonSeleccion.Attributes.Remove("disabled");
            lbxRazonSeleccionado.Attributes.Remove("disabled");
            btnEliminar.Enabled = true;
            btnAgregar.Enabled = true;
        }
        else
        {
            for (int i = 0; i <= lbxRazonSeleccionado.Items.Count - 1; )
            {
                System.Web.UI.WebControls.ListItem li = new ListItem(this.lbxRazonSeleccionado.Items[i].Text, this.lbxRazonSeleccionado.Items[i].Value);
                this.lbxRazonSeleccion.Items.Add(li);
                this.lbxRazonSeleccionado.Items.Remove(li);
                btnAgregar.Enabled = false;
                btnEliminar.Enabled = false;
            }
            lbxRazonSeleccionado.Attributes.Add("disabled", "true");
            lbxRazonSeleccion.Attributes.Add("disabled", "true");
            SortListBox(this.lbxRazonSeleccion);
        }
    }
}

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
using ASSMCA.perfiles;

namespace ASSMCA
{
	public partial class frmHome : System.Web.UI.Page
	{
		protected System.Data.SqlClient.SqlDataAdapter daLkpEpisodio;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		protected System.Data.SqlClient.SqlConnection cnn;
		protected System.Data.SqlClient.SqlDataAdapter daLkpPerfil;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
		protected ASSMCA.perfiles.dsPerfil dsPerfil;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!this.IsPostBack)
			{
				
				//Response.Write("<script>alert('" + this.Session["co_tipo"].ToString() + "');</script>");
				if (this.Session["dsSeguridad"] == null)
                {
                    this.Response.Redirect("~/Error.aspx?errMsg=sesion");
                    return;
                }
				if(this.Session["dsPerfil"] == null)
				{                  
					this.PreapararCombos();
					this.daLkpEpisodio.Fill(this.dsPerfil);
					this.daLkpPerfil.Fill(this.dsPerfil);
					this.Session["dsPerfil"] = this.dsPerfil;
				}

				

			}
		}

		


		private void PreapararCombos()
        {
            #region Tablas del episodio y perfil de admision
            DataRow drSA_LKP_TEDS_SEGURO_SALUD = this.dsPerfil.SA_LKP_TEDS_SEGURO_SALUD.NewRow();
			drSA_LKP_TEDS_SEGURO_SALUD["PK_SeguroSalud"] = 0;
			drSA_LKP_TEDS_SEGURO_SALUD["DE_SeguroSalud"] = "";
			this.dsPerfil.SA_LKP_TEDS_SEGURO_SALUD.Rows.Add(drSA_LKP_TEDS_SEGURO_SALUD);
			DataRow drSA_LKP_TEDS_PAGO = this.dsPerfil.SA_LKP_TEDS_PAGO.NewRow();
			drSA_LKP_TEDS_PAGO["PK_Pago"] = 0;
			drSA_LKP_TEDS_PAGO["DE_Pago"] = "";
			this.dsPerfil.SA_LKP_TEDS_PAGO.Rows.Add(drSA_LKP_TEDS_PAGO);
			DataRow drSA_LKP_FEMINA = this.dsPerfil.SA_LKP_FEMINA.NewRow();
			drSA_LKP_FEMINA["PK_Femina"] = 0;
			drSA_LKP_FEMINA["DE_Femina"] = "";
			this.dsPerfil.SA_LKP_FEMINA.Rows.Add(drSA_LKP_FEMINA);
			DataRow drSA_LKP_TEDS_FUENTE_INGRESO = this.dsPerfil.SA_LKP_TEDS_FUENTE_INGRESO.NewRow();
			drSA_LKP_TEDS_FUENTE_INGRESO["PK_FuenteIngreso"] = 0;
			drSA_LKP_TEDS_FUENTE_INGRESO["DE_FuenteIngreso"] = "";
			this.dsPerfil.SA_LKP_TEDS_FUENTE_INGRESO.Rows.Add(drSA_LKP_TEDS_FUENTE_INGRESO);
			DataRow drSA_LKP_INGRESO_ANUAL = this.dsPerfil.SA_LKP_INGRESO_ANUAL.NewRow();
			drSA_LKP_INGRESO_ANUAL["PK_IngresoAnual"] = 0;
			drSA_LKP_INGRESO_ANUAL["DE_IngresoAnual"] = "";
			this.dsPerfil.SA_LKP_INGRESO_ANUAL.Rows.Add(drSA_LKP_INGRESO_ANUAL);
			DataRow drSA_LKP_TIEMPO_RESIDENCIA = this.dsPerfil.SA_LKP_TIEMPO_RESIDENCIA.NewRow();
			drSA_LKP_TIEMPO_RESIDENCIA["PK_TiempoResidencia"] = 0;
			drSA_LKP_TIEMPO_RESIDENCIA["DE_TiempoResidencia"] = "";
			this.dsPerfil.SA_LKP_TIEMPO_RESIDENCIA.Rows.Add(drSA_LKP_TIEMPO_RESIDENCIA);
			DataRow drSA_LKP_MUNICIPIO_RESIDENCIA = this.dsPerfil.SA_LKP_MUNICIPIO_RESIDENCIA.NewRow();
			drSA_LKP_MUNICIPIO_RESIDENCIA["PK_Municipio"] = 0;
			drSA_LKP_MUNICIPIO_RESIDENCIA["DE_Municipio"] = "";
			this.dsPerfil.SA_LKP_MUNICIPIO_RESIDENCIA.Rows.Add(drSA_LKP_MUNICIPIO_RESIDENCIA);
			DataRow drSA_LKP_TEDS_ETAPA_SERVICIO = this.dsPerfil.SA_LKP_TEDS_ETAPA_SERVICIO.NewRow();
			drSA_LKP_TEDS_ETAPA_SERVICIO["PK_EtapaServicio"] = 0;
			drSA_LKP_TEDS_ETAPA_SERVICIO["DE_EtapaServicio"] = "";
			this.dsPerfil.SA_LKP_TEDS_ETAPA_SERVICIO.Rows.Add(drSA_LKP_TEDS_ETAPA_SERVICIO);
			DataRow drSA_LKP_TEDS_REFERIDO = this.dsPerfil.SA_LKP_TEDS_REFERIDO.NewRow();
			drSA_LKP_TEDS_REFERIDO["PK_Referido"] = 0;
			drSA_LKP_TEDS_REFERIDO["DE_Referido"] = "";
			this.dsPerfil.SA_LKP_TEDS_REFERIDO.Rows.Add(drSA_LKP_TEDS_REFERIDO);
			DataRow drSA_LKP_TEDS_ESTADO_LEGAL = this.dsPerfil.SA_LKP_TEDS_ESTADO_LEGAL.NewRow();
			drSA_LKP_TEDS_ESTADO_LEGAL["PK_EstadoLegal"] = 0;
			drSA_LKP_TEDS_ESTADO_LEGAL["DE_EstadoLegal"] = "";
			this.dsPerfil.SA_LKP_TEDS_ESTADO_LEGAL.Rows.Add(drSA_LKP_TEDS_ESTADO_LEGAL);
			DataRow drSA_LKP_PROBLEMA_JUSTICIA = this.dsPerfil.SA_LKP_PROBLEMA_JUSTICIA.NewRow();
			drSA_LKP_PROBLEMA_JUSTICIA["PK_ProbJusticia"] = 0;
			drSA_LKP_PROBLEMA_JUSTICIA["DE_ProbJusticia"] = "";
			this.dsPerfil.SA_LKP_PROBLEMA_JUSTICIA.Rows.Add(drSA_LKP_PROBLEMA_JUSTICIA);
			DataRow drSA_LKP_TEDS_EPISODIO_PREVIO = this.dsPerfil.SA_LKP_TEDS_EPISODIO_PREVIO.NewRow();
			drSA_LKP_TEDS_EPISODIO_PREVIO["PK_EpisodiosPrevios"] = 0;
			drSA_LKP_TEDS_EPISODIO_PREVIO["DE_EpisodiosPrevios"] = "";
			this.dsPerfil.SA_LKP_TEDS_EPISODIO_PREVIO.Rows.Add(drSA_LKP_TEDS_EPISODIO_PREVIO);
			DataRow drSA_LKP_TIEMPO_ULT_TRAT = this.dsPerfil.SA_LKP_TIEMPO_ULT_TRAT.NewRow();
			drSA_LKP_TIEMPO_ULT_TRAT["PK_TiempoUltTrat"] = 0;
			drSA_LKP_TIEMPO_ULT_TRAT["DE_TiempoUltTrat"] = "";
			this.dsPerfil.SA_LKP_TIEMPO_ULT_TRAT.Rows.Add(drSA_LKP_TIEMPO_ULT_TRAT);
			DataRow drSA_LKP_ABUSO_SUSTANCIAS_ANTERIOR = this.dsPerfil.SA_LKP_ABUSO_SUSTANCIAS_ANTERIOR.NewRow();
			drSA_LKP_ABUSO_SUSTANCIAS_ANTERIOR["PK_AbusoSustancias"] = 0;
			drSA_LKP_ABUSO_SUSTANCIAS_ANTERIOR["DE_AbusoSustancias"] = "";
			this.dsPerfil.SA_LKP_ABUSO_SUSTANCIAS_ANTERIOR.Rows.Add(drSA_LKP_ABUSO_SUSTANCIAS_ANTERIOR);
			DataRow drSA_LKP_SALUD_MENTAL_ANTERIOR = this.dsPerfil.SA_LKP_SALUD_MENTAL_ANTERIOR.NewRow();
			drSA_LKP_SALUD_MENTAL_ANTERIOR["PK_SaludMental"] = 0;
			drSA_LKP_SALUD_MENTAL_ANTERIOR["DE_SaludMental"] = "";
			this.dsPerfil.SA_LKP_SALUD_MENTAL_ANTERIOR.Rows.Add(drSA_LKP_SALUD_MENTAL_ANTERIOR);
            #endregion
            #region Tabas perfil
            DataRow drSA_LKP_TEDS_ESTADO_MARITAL = this.dsPerfil.SA_LKP_TEDS_ESTADO_MARITAL.NewRow();
			drSA_LKP_TEDS_ESTADO_MARITAL["PK_EstadoMarital"] = 0;
			drSA_LKP_TEDS_ESTADO_MARITAL["DE_EstadoMarital"] = "";
			this.dsPerfil.SA_LKP_TEDS_ESTADO_MARITAL.Rows.Add(drSA_LKP_TEDS_ESTADO_MARITAL);
			DataRow drSA_LKP_TEDS_COND_LABORAL = this.dsPerfil.SA_LKP_TEDS_COND_LABORAL.NewRow();
			drSA_LKP_TEDS_COND_LABORAL["PK_CondLaboral"] = 0;
			drSA_LKP_TEDS_COND_LABORAL["DE_CondLaboral"] = "";
			this.dsPerfil.SA_LKP_TEDS_COND_LABORAL.Rows.Add(drSA_LKP_TEDS_COND_LABORAL);
			DataRow drSA_LKP_TEDS_NO_FUERZA_LABORAL = this.dsPerfil.SA_LKP_TEDS_NO_FUERZA_LABORAL.NewRow();
			drSA_LKP_TEDS_NO_FUERZA_LABORAL["PK_NoFuerzaLaboral"] = 0;
			drSA_LKP_TEDS_NO_FUERZA_LABORAL["DE_NoFuerzaLaboral"] = "";
			this.dsPerfil.SA_LKP_TEDS_NO_FUERZA_LABORAL.Rows.Add(drSA_LKP_TEDS_NO_FUERZA_LABORAL);
			DataRow drSA_LKP_TEDS_GRADO = this.dsPerfil.SA_LKP_TEDS_GRADO.NewRow();
			drSA_LKP_TEDS_GRADO["PK_Grado"] = 0;
			drSA_LKP_TEDS_GRADO["DE_Grado"] = "";
			this.dsPerfil.SA_LKP_TEDS_GRADO.Rows.Add(drSA_LKP_TEDS_GRADO);
			DataRow drSA_LKP_COMPOSICION_FAMILIAR = this.dsPerfil.SA_LKP_COMPOSICION_FAMILIAR.NewRow();
			drSA_LKP_COMPOSICION_FAMILIAR["PK_Familiar"] = 0;
			drSA_LKP_COMPOSICION_FAMILIAR["DE_Familiar"] = "";
			this.dsPerfil.SA_LKP_COMPOSICION_FAMILIAR.Rows.Add(drSA_LKP_COMPOSICION_FAMILIAR);
			DataRow drSA_LKP_TEDS_RESIDENCIA = this.dsPerfil.SA_LKP_TEDS_RESIDENCIA.NewRow();
			drSA_LKP_TEDS_RESIDENCIA["PK_Residencia"] = 0;
			drSA_LKP_TEDS_RESIDENCIA["DE_Residencia"] = "";
			this.dsPerfil.SA_LKP_TEDS_RESIDENCIA.Rows.Add(drSA_LKP_TEDS_RESIDENCIA);
			DataRow drSA_LKP_DIAGNOSTICO = this.dsPerfil.SA_LKP_DIAGNOSTICO.NewRow();
			drSA_LKP_DIAGNOSTICO["PK_Diagnostico"] = 0;
			drSA_LKP_DIAGNOSTICO["DE_Diagnostico"] = "";
			this.dsPerfil.SA_LKP_DIAGNOSTICO.Rows.Add(drSA_LKP_DIAGNOSTICO);
			DataRow drSA_LKP_DSMIV_CAT = this.dsPerfil.SA_LKP_DSMIV_CAT.NewRow();
			drSA_LKP_DSMIV_CAT["PK_DSMIVCat"] = 0;
			drSA_LKP_DSMIV_CAT["DE_DSMIVCat"] = "";
			this.dsPerfil.SA_LKP_DSMIV_CAT.Rows.Add(drSA_LKP_DSMIV_CAT);
			DataRow drSA_LKP_DSMIV = this.dsPerfil.SA_LKP_DSMIV.NewRow();
			drSA_LKP_DSMIV["PK_DSMIV"] = 0;
			drSA_LKP_DSMIV["DE_DSMIV"] = "";
			drSA_LKP_DSMIV["PK_DSMIV1"] = "";
			drSA_LKP_DSMIV["FK_DSMIVCat"] = 0;
			this.dsPerfil.SA_LKP_DSMIV.Rows.Add(drSA_LKP_DSMIV);
			DataRow drSA_LKP_DSMIV_IV = this.dsPerfil.SA_LKP_DSMIV_IV.NewRow();
			drSA_LKP_DSMIV_IV["PK_DSMIV_IV"] = 0;
			drSA_LKP_DSMIV_IV["DE_DSMIV_IV"] = "";
			this.dsPerfil.SA_LKP_DSMIV_IV.Rows.Add(drSA_LKP_DSMIV_IV);
			DataRow drSA_LKP_REFERIDOS_TX = this.dsPerfil.SA_LKP_REFERIDOS_TX.NewRow();
			drSA_LKP_REFERIDOS_TX["PK_ReferidosTX"] = 0;
			drSA_LKP_REFERIDOS_TX["DE_ReferidosTX"] = "";
			this.dsPerfil.SA_LKP_REFERIDOS_TX.Rows.Add(drSA_LKP_REFERIDOS_TX);
			DataRow drSA_LKP_TEDS_SUSTANCIA = this.dsPerfil.SA_LKP_TEDS_SUSTANCIA.NewRow();
			drSA_LKP_TEDS_SUSTANCIA["PK_Sustancia"] = 0;
			drSA_LKP_TEDS_SUSTANCIA["DE_Sustancia"] = "";
			this.dsPerfil.SA_LKP_TEDS_SUSTANCIA.Rows.Add(drSA_LKP_TEDS_SUSTANCIA);
			DataRow drSA_LKP_TEDS_VIA_UTILIZACION = this.dsPerfil.SA_LKP_TEDS_VIA_UTILIZACION.NewRow();
			drSA_LKP_TEDS_VIA_UTILIZACION["PK_ViaUtilizacion"] = 0;
			drSA_LKP_TEDS_VIA_UTILIZACION["DE_ViaUtilizacion"] = "";
			this.dsPerfil.SA_LKP_TEDS_VIA_UTILIZACION.Rows.Add(drSA_LKP_TEDS_VIA_UTILIZACION);
			DataRow drSA_LKP_TEDS_FRECUENCIA = this.dsPerfil.SA_LKP_TEDS_FRECUENCIA.NewRow();
			drSA_LKP_TEDS_FRECUENCIA["PK_Frecuencia"] = 0;
			drSA_LKP_TEDS_FRECUENCIA["DE_Frecuencia"] = "";
			this.dsPerfil.SA_LKP_TEDS_FRECUENCIA.Rows.Add(drSA_LKP_TEDS_FRECUENCIA);
            DataRow drSA_LKP_MEDIDA = this.dsPerfil.SA_LKP_MEDIDA.NewRow();
            drSA_LKP_MEDIDA["PK_Medida"] = 0;
            drSA_LKP_MEDIDA["DE_Medida"] = "";
            this.dsPerfil.SA_LKP_MEDIDA.Rows.Add(drSA_LKP_MEDIDA);
            DataRow drSA_LKP_TEDS_SITUACION_ESCOLAR = this.dsPerfil.SA_LKP_TEDS_SITUACION_ESCOLAR.NewRow();
            drSA_LKP_TEDS_SITUACION_ESCOLAR["PK_SituacionEscolar"] = 0;
            drSA_LKP_TEDS_SITUACION_ESCOLAR["DE_SituacionEscolar"] = "";
            this.dsPerfil.SA_LKP_TEDS_SITUACION_ESCOLAR.Rows.Add(drSA_LKP_TEDS_SITUACION_ESCOLAR);
            DataRow drSA_LKP_TEDS_TIPO_ADMISION = this.dsPerfil.SA_LKP_TEDS_TIPO_ADMISION.NewRow();
            drSA_LKP_TEDS_TIPO_ADMISION["PK_TipoAdmision"] = 0;
            drSA_LKP_TEDS_TIPO_ADMISION["DE_TipoAdmision"] = "";
            this.dsPerfil.SA_LKP_TEDS_TIPO_ADMISION.Rows.Add(drSA_LKP_TEDS_TIPO_ADMISION);
            DataRow drSA_LKP_DSMV_ProblemasPsicosocialesAmbientales = this.dsPerfil.SA_LKP_DSMV_ProblemasPsicosocialesAmbientales.NewRow();
            drSA_LKP_DSMV_ProblemasPsicosocialesAmbientales["PK_DSMV_ProblemasPsicosocialesAmbientales"] = 0;
            drSA_LKP_DSMV_ProblemasPsicosocialesAmbientales["DE_DSMV_ProblemasPsicosocialesAmbientales"] = "";
            this.dsPerfil.SA_LKP_DSMV_ProblemasPsicosocialesAmbientales.Rows.Add(drSA_LKP_DSMV_ProblemasPsicosocialesAmbientales);
            DataRow drSA_LKP_DSMV = this.dsPerfil.SA_LKP_DSMV.NewRow();
            drSA_LKP_DSMV["_PK_DSMV"] = 0;
            drSA_LKP_DSMV["DE_DSMV"] = "";
            drSA_LKP_DSMV["CONCAT_DSMV"] = "";
            drSA_LKP_DSMV["PK_DSMV"] = "";
            this.dsPerfil.SA_LKP_DSMV.Rows.Add(drSA_LKP_DSMV);
            #endregion
        }
		#region Código generado por el Diseñador de Web Forms
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{    
			this.daLkpEpisodio = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.cnn = new System.Data.SqlClient.SqlConnection();
			this.daLkpPerfil = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
			this.dsPerfil = new ASSMCA.perfiles.dsPerfil();
			((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).BeginInit();
            #region daLkpEpisodio
            this.daLkpEpisodio.SelectCommand = this.sqlSelectCommand1;
			this.daLkpEpisodio.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
                new System.Data.Common.DataTableMapping("Table", "SA_LKP_TEDS_SEGURO_SALUD", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_SeguroSalud", "PK_SeguroSalud"),
                new System.Data.Common.DataColumnMapping("DE_SeguroSalud", "DE_SeguroSalud")}),
                new System.Data.Common.DataTableMapping("Table1", "SA_LKP_TEDS_PAGO", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Pago", "PK_Pago"),
                new System.Data.Common.DataColumnMapping("DE_Pago", "DE_Pago")}),
                new System.Data.Common.DataTableMapping("Table2", "SA_LKP_FEMINA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Femina", "PK_Femina"),
                new System.Data.Common.DataColumnMapping("DE_Femina", "DE_Femina")}),
                new System.Data.Common.DataTableMapping("Table3", "SA_LKP_TEDS_FUENTE_INGRESO", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_FuenteIngreso", "PK_FuenteIngreso"),
                new System.Data.Common.DataColumnMapping("DE_FuenteIngreso", "DE_FuenteIngreso")}),
                new System.Data.Common.DataTableMapping("Table4", "SA_LKP_INGRESO_ANUAL", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_IngresoAnual", "PK_IngresoAnual"),
                new System.Data.Common.DataColumnMapping("DE_IngresoAnual", "DE_IngresoAnual")}),
                new System.Data.Common.DataTableMapping("Table5", "SA_LKP_TIEMPO_RESIDENCIA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_TiempoResidencia", "PK_TiempoResidencia"),
                new System.Data.Common.DataColumnMapping("DE_TiempoResidencia", "DE_TiempoResidencia")}),
                new System.Data.Common.DataTableMapping("Table6", "SA_LKP_MUNICIPIO_RESIDENCIA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Municipio", "PK_Municipio"),
                new System.Data.Common.DataColumnMapping("DE_Municipio", "DE_Municipio")}),
                new System.Data.Common.DataTableMapping("Table7", "SA_LKP_TEDS_ETAPA_SERVICIO", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_EtapaServicio", "PK_EtapaServicio"),
                new System.Data.Common.DataColumnMapping("DE_EtapaServicio", "DE_EtapaServicio")}),
                new System.Data.Common.DataTableMapping("Table8", "SA_LKP_TEDS_REFERIDO", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Referido", "PK_Referido"),
                new System.Data.Common.DataColumnMapping("DE_Referido", "DE_Referido")}),
                new System.Data.Common.DataTableMapping("Table9", "SA_LKP_TEDS_ESTADO_LEGAL", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_EstadoLegal", "PK_EstadoLegal"),
                new System.Data.Common.DataColumnMapping("DE_EstadoLegal", "DE_EstadoLegal")}),
                new System.Data.Common.DataTableMapping("Table10", "SA_LKP_PROBLEMA_JUSTICIA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_ProbJusticia", "PK_ProbJusticia"),
                new System.Data.Common.DataColumnMapping("DE_ProbJusticia", "DE_ProbJusticia")}),
                new System.Data.Common.DataTableMapping("Table11", "SA_LKP_TEDS_EPISODIO_PREVIO", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_EpisodiosPrevios", "PK_EpisodiosPrevios"),
                new System.Data.Common.DataColumnMapping("DE_EpisodiosPrevios", "DE_EpisodiosPrevios")}),
                new System.Data.Common.DataTableMapping("Table12", "SA_LKP_TIEMPO_ULT_TRAT", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_TiempoUltTrat", "PK_TiempoUltTrat"),
                new System.Data.Common.DataColumnMapping("DE_TiempoUltTrat", "DE_TiempoUltTrat")}),
                new System.Data.Common.DataTableMapping("Table13", "SA_LKP_ABUSO_SUSTANCIAS_ANTERIOR", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_AbusoSustancias", "PK_AbusoSustancias"),
                new System.Data.Common.DataColumnMapping("DE_AbusoSustancias", "DE_AbusoSustancias")}),
                new System.Data.Common.DataTableMapping("Table14", "SA_LKP_SALUD_MENTAL_ANTERIOR", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_SaludMental", "PK_SaludMental"),
                new System.Data.Common.DataColumnMapping("DE_SaludMental", "DE_SaludMental")})});
            #endregion
            #region sqlSelectCommand1
			this.sqlSelectCommand1.CommandText = "[SPR_LKP_EPISODIO]";
			this.sqlSelectCommand1.CommandType = System.Data.CommandType.StoredProcedure;
			this.sqlSelectCommand1.Connection = this.cnn;
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            #endregion
            #region cnn
            this.cnn.ConnectionString = NewSource.connectionString;
            #endregion
            #region daLkpPerfil 
			this.daLkpPerfil.SelectCommand = this.sqlSelectCommand2;
			this.daLkpPerfil.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
                new System.Data.Common.DataTableMapping("Table", "SA_LKP_TEDS_ESTADO_MARITAL", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_EstadoMarital", "PK_EstadoMarital"),
                new System.Data.Common.DataColumnMapping("DE_EstadoMarital", "DE_EstadoMarital")}),
                new System.Data.Common.DataTableMapping("Table1", "SA_LKP_TEDS_COND_LABORAL", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_CondLaboral", "PK_CondLaboral"),
                new System.Data.Common.DataColumnMapping("DE_CondLaboral", "DE_CondLaboral")}),
                new System.Data.Common.DataTableMapping("Table2", "SA_LKP_TEDS_NO_FUERZA_LABORAL", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_NoFuerzaLaboral", "PK_NoFuerzaLaboral"),
                new System.Data.Common.DataColumnMapping("DE_NoFuerzaLaboral", "DE_NoFuerzaLaboral")}),
                new System.Data.Common.DataTableMapping("Table3", "SA_LKP_TEDS_GRADO", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Grado", "PK_Grado"),
                new System.Data.Common.DataColumnMapping("DE_Grado", "DE_Grado")}),
                new System.Data.Common.DataTableMapping("Table4", "SA_LKP_COMPOSICION_FAMILIAR", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Familiar", "PK_Familiar"),
                new System.Data.Common.DataColumnMapping("DE_Familiar", "DE_Familiar")}),
                new System.Data.Common.DataTableMapping("Table5", "SA_LKP_TEDS_RESIDENCIA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Residencia", "PK_Residencia"),
                new System.Data.Common.DataColumnMapping("DE_Residencia", "DE_Residencia")}),
                new System.Data.Common.DataTableMapping("Table6", "SA_LKP_DIAGNOSTICO", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Diagnostico", "PK_Diagnostico"),
                new System.Data.Common.DataColumnMapping("DE_Diagnostico", "DE_Diagnostico")}),
                new System.Data.Common.DataTableMapping("Table7", "SA_LKP_DSMIV_CAT", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_DSMIVCat", "PK_DSMIVCat"),
                new System.Data.Common.DataColumnMapping("DE_DSMIVCat", "DE_DSMIVCat")}),
                new System.Data.Common.DataTableMapping("Table8", "SA_LKP_DSMIV", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_DSMIV", "PK_DSMIV"),
                new System.Data.Common.DataColumnMapping("DE_DSMIV", "DE_DSMIV"),
                new System.Data.Common.DataColumnMapping("PK_DSMIV1", "PK_DSMIV1"),
                new System.Data.Common.DataColumnMapping("FK_DSMIVCat", "FK_DSMIVCat")}),
                new System.Data.Common.DataTableMapping("Table9", "SA_LKP_DSMIV_IV", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_DSMIV_IV", "PK_DSMIV_IV"),
                new System.Data.Common.DataColumnMapping("DE_DSMIV_IV", "DE_DSMIV_IV")}),
                new System.Data.Common.DataTableMapping("Table10", "SA_LKP_REFERIDOS_TX", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_ReferidosTX", "PK_ReferidosTX"),
                new System.Data.Common.DataColumnMapping("DE_ReferidosTX", "DE_ReferidosTX")}),
                new System.Data.Common.DataTableMapping("Table11", "SA_LKP_TEDS_SUSTANCIA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Sustancia", "PK_Sustancia"),
                new System.Data.Common.DataColumnMapping("DE_Sustancia", "DE_Sustancia")}),
                new System.Data.Common.DataTableMapping("Table12", "SA_LKP_TEDS_VIA_UTILIZACION", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_ViaUtilizacion", "PK_ViaUtilizacion"),
                new System.Data.Common.DataColumnMapping("DE_ViaUtilizacion", "DE_ViaUtilizacion")}),
                new System.Data.Common.DataTableMapping("Table13", "SA_LKP_TEDS_FRECUENCIA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Frecuencia", "PK_Frecuencia"),
                new System.Data.Common.DataColumnMapping("DE_Frecuencia", "DE_Frecuencia")}),
                new System.Data.Common.DataTableMapping("Table14", "SA_LKP_MEDIDA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_Medida", "PK_Medida"),
                new System.Data.Common.DataColumnMapping("DE_Medida", "DE_Medida")}),
                new System.Data.Common.DataTableMapping("Table15", "SA_LKP_TEDS_FRECUENCIA_AUTOAYUDA", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_FreqAutoAyuda", "PK_FreqAutoAyuda"),
                new System.Data.Common.DataColumnMapping("DE_FreqAutoAyuda", "DE_FreqAutoAyuda")}),
                new System.Data.Common.DataTableMapping("Table16", "SA_LKP_TEDS_SITUACION_ESCOLAR", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_SituacionEscolar", "PK_SituacionEscolar"),
                new System.Data.Common.DataColumnMapping("DE_SituacionEscolar", "DE_SituacionEscolar")}),
                new System.Data.Common.DataTableMapping("Table17", "SA_LKP_TEDS_TIPO_ADMISION", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_TipoAdmision", "PK_TipoAdmision"),
                new System.Data.Common.DataColumnMapping("DE_TipoAdmision", "DE_TipoAdmision")}),
                new System.Data.Common.DataTableMapping("Table19", "SA_LKP_DSMV_ProblemasPsicosocialesAmbientales", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_DSMV_ProblemasPsicosocialesAmbientales", "PK_DSMV_ProblemasPsicosocialesAmbientales"),
                new System.Data.Common.DataColumnMapping("DE_DSMV_ProblemasPsicosocialesAmbientales", "DE_DSMV_ProblemasPsicosocialesAmbientales")}),
                new System.Data.Common.DataTableMapping("Table20", "SA_LKP_DSMV", new System.Data.Common.DataColumnMapping[] {
                new System.Data.Common.DataColumnMapping("PK_DSMV", "PK_DSMV"),
                new System.Data.Common.DataColumnMapping("_PK_DSMV", "_PK_DSMV"),
                new System.Data.Common.DataColumnMapping("CONCAT_DSMV", "CONCAT_DSMV"),
                new System.Data.Common.DataColumnMapping("DE_DSMV", "DE_DSMV")})});
            #endregion
            #region sqlSelectCommand2
			this.sqlSelectCommand2.CommandText = "[SPR_LKP_PERFIL]";
			this.sqlSelectCommand2.CommandType = System.Data.CommandType.StoredProcedure;
			this.sqlSelectCommand2.Connection = this.cnn;
			this.sqlSelectCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
            #endregion
            #region dsPerfil
			this.dsPerfil.DataSetName = "dsPerfil";
			this.dsPerfil.Locale = new System.Globalization.CultureInfo("en-US");
			((System.ComponentModel.ISupportInitialize)(this.dsPerfil)).EndInit();
            #endregion
        }
		#endregion
	}
}
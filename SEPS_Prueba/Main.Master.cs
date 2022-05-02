using ASSMCA.perfiles;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ASSMCA.perfiles.dsPerfil;

namespace ASSMCA
{
	public partial class Main : System.Web.UI.MasterPage
	{
		protected int totalEvProgreso;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.Session["dsSeguridad"] == null)
			{
				this.Response.Redirect("frmLogon.aspx");
				return;
			}
			if (Session["nb_programa"] != null)
			{
				this.lblCentro.Text = Session["nb_programa"].ToString();
				this.hFKPrograma.Value = Session["pk_programa"].ToString();
			}
			else
			{
				this.Response.Redirect(ResolveClientUrl("frmLogon.aspx?changeProg=yes"));
			}
			if (Session["usuarioPrograma"] != null)
			{
				if (Session["usuarioPrograma"].ToString() == "1")
				{
					changeProgram.Visible = false;
				}
			}

			if (!this.IsPostBack)
			{
				txtToDatePicker.Text = DateTime.Now.ToShortDateString();
				txtFromDatePicker.Text = DateTime.Now.AddYears(-10).ToShortDateString();

      			FillPopEvProgresoTable();
				
			}

		}
		protected void changeProgramClick(object sender, EventArgs e)
		{
			this.Response.Redirect(ResolveClientUrl("frmLogon.aspx?changeProg=yes"));
		}

		VW_SAEPDataTable GetEVProgreso( DateTime from , DateTime to)
		{
			var programa = Convert.ToInt32(this.Session["pk_programa"]);

					 VW_SAEPTableAdapter saep = new VW_SAEPTableAdapter();


			return saep.GetData((short)programa, from, to);

		}


		public void FillPopEvProgresoTable()
		{
			DateTime from;
			DateTime to;


			if (DateTime.TryParse(txtFromDatePicker.Text, out from) && DateTime.TryParse(txtToDatePicker.Text, out to))
			{
				if (from.Year < 1950 || from.Year > 9999 || to.Year < 1950 || to.Year > 9999)
				{
					BtnExportExcel.Visible = false;
					BtnPrint.Visible = false;
					lblMessage.Visible = true;
					divtable.Visible = false;
					lblTotalEvProgreso.Visible = false;
					lblMessage.Text = "El año escogido debe ser mayor de 1940 y menor de 9999.";
				}
				else
				{
					var list = GetEVProgreso(from, to);
					var str = "";
					totalEvProgreso = 0;

					if (list.Count > 0)
					{

						str += "<table align=\"center\" class=\"table\"><thead>"
							+ "<tr>"
						 + "<th scope=\"col\">Nombre</th>"
						+ "<th scope=\"col\">IUP</th>"
						+ "  <th scope=\"col\">Expediente</th>"
						+ "  <th scope=\"col\">Numero de Episodio</th>"
						+ "  <th scope=\"col\">Fecha Admsión</th>"
						+ "  <th scope=\"col\">Último Perfil</th>"
						+ "  <th scope=\"col\">Tipo de Último Perfil</th>"
						+ "  <th scope=\"col\">Meses sin Perfiles de Evaluación de Progreso</th>"
						+ "</tr>"
						+ "</thead><tbody>";
						foreach (var episodio in list)
						{
							totalEvProgreso++;
							str += "<tr>"
								+ $"<th scope=\"row\"> {episodio.Nombre_Participante}</th>"
								 + $"<td><a href=\"./pacientes/frmVisualizar.aspx?accion=consultar&pk_persona={episodio.IUP}&fuente={FormatTipoPerfilLink(episodio.Tipo_de_Último_Perfil)}\">{episodio.IUP}</a></td>"
								  + $" <td>{ episodio.Expediente}</td>"
									  + $"  <td>{ episodio.Número_de_Episodio}</td>"
										 + $" <td>{ episodio.Fecha_Admsión.Date.ToShortDateString()}</td>"
											 + $" <td>{ episodio.ÚltimoPerfil.Date.ToShortDateString()}</td>"
											  + $"<td>{FormatTipoPerfil(episodio.Tipo_de_Último_Perfil)}</td>"
												 + $"<td>{ episodio.Meses_sin_Perfiles_de_Evaluación_de_Progreso}</td>"
													  + "</tr>";


						}

						divtable.Visible = true;
						str += "</tbody></table>";
						lblTotalEvProgreso.Text = "Total: " + totalEvProgreso.ToString();
						divtable.InnerHtml = str;
						lblMessage.Visible = false;
						lblTotalEvProgreso.Visible = true;
						BtnExportExcel.Visible = true;
						BtnPrint.Visible = true;

					}
					else
					{
						BtnExportExcel.Visible = false;
						BtnPrint.Visible = false;
						lblMessage.Visible = true;
						lblTotalEvProgreso.Visible = false;
						divtable.Visible = false;
						lblMessage.Text = "En estos momentos no existen evaluaciones en progreso";
					}
				}
			}
			else
			{
				BtnExportExcel.Visible = false;
				BtnPrint.Visible = false;
				lblMessage.Visible = true;
				divtable.Visible = false;
				lblTotalEvProgreso.Visible = false;
				lblMessage.Text = "Campos de fecha en blanco o formato incorrecto";
			}
		}

		string FormatTipoPerfil(string str) => (str == "AD") ? "Admisión" : (str == "EV") ? "Evaluación" : "Alta";
		string FormatTipoPerfilLink(string str) => (str == "AD") ? "admision" : (str == "EV") ? "evaluacion" : "alta";


		protected void BtnExportExcel_Click(object sender, EventArgs e)
		{

			DateTime from;
			DateTime to;

			if (DateTime.TryParse(txtFromDatePicker.Text, out from) && DateTime.TryParse(txtToDatePicker.Text, out to))
			{
				var list = GetEVProgreso(from, to);

				if (list.Count > 0)
				{
					Response.ClearContent();

					Response.AddHeader("Content-Disposition", $"Attachment;Filename=SAEP {DateTime.Now.ToShortDateString()} {this.Session["NB_Programa"]}.xls");
					Response.Buffer = true;
					Response.Charset = "UTF-8";
					Response.ContentType = "application/vnd.ms-excel";

					var str = "";
					str += $"<p>Sistema de Alerta de Evaluaciones de Progreso</p>";
					str += $"<p>Programa: {this.Session["NB_Programa"]}</p>";
					str += $"<p>Fecha: {DateTime.Now.ToShortDateString()}/<p>";
					str += "<table><thead>"
						   + "<tr>"
						+ "<th scope=\"col\">Nombre</th>"
					   + "<th scope=\"col\">IUP</th>"
					   + "  <th scope=\"col\">Expediente</th>"
					   + "  <th scope=\"col\">Numero de Episodio</th>"
					   + "  <th scope=\"col\">Fecha Admsión</th>"
					   + "  <th scope=\"col\">Último Perfil</th>"
					   + "  <th scope=\"col\">Tipo de Último Perfil</th>"
					   + "  <th scope=\"col\">Meses sin Perfiles de Evaluación de Progreso</th>"
					   + "</tr>"
					   + "</thead><tbody>";
					foreach (var episodio in list)
					{
						totalEvProgreso++;
						str += "<tr>"
							+ $"<th scope=\"row\"> {episodio.Nombre_Participante}</th>"
							 + $"<td><a>{episodio.IUP}</a></td>"
							  + $" <td>{ episodio.Expediente}</td>"
								  + $"  <td>{ episodio.Número_de_Episodio}</td>"
									 + $" <td>{ episodio.Fecha_Admsión.Date.ToShortDateString()}</td>"
										 + $" <td>{ episodio.ÚltimoPerfil.Date.ToShortDateString()}</td>"
										  + $"<td>{FormatTipoPerfil(episodio.Tipo_de_Último_Perfil)}</td>"
											 + $"<td>{ episodio.Meses_sin_Perfiles_de_Evaluación_de_Progreso}</td>"
												  + "</tr>";
					}


					Response.Write(System.Net.WebUtility.HtmlDecode(str));
					Response.End();

				}

			}
		}

        protected void btnSearchPopup_Click(object sender, EventArgs e)
        {
			FillPopEvProgresoTable();
        }
    }
}
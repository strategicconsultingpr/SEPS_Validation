using ASSMCA.perfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASSMCA
{
    public partial class Main : System.Web.UI.MasterPage
    {
		protected PopupsEvProgresoTableAdapter PopupsEvProgreso = new PopupsEvProgresoTableAdapter();
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
            else{
                this.Response.Redirect(ResolveClientUrl("frmLogon.aspx?changeProg=yes"));
            }
            if (Session["usuarioPrograma"] != null)
            {
                if (Session["usuarioPrograma"].ToString() == "1")
                {
                    changeProgram.Visible = false;
                }
            }

			if(!this.IsPostBack)
			FillPopEvProgresoTable();


		}
        protected void changeProgramClick(object sender, EventArgs e)
        {
            this.Response.Redirect(ResolveClientUrl("frmLogon.aspx?changeProg=yes"));
        }

		void FillPopEvProgresoTable()
		{

			var programa = Convert.ToInt32(this.Session["pk_programa"]);

			var list = PopupsEvProgreso.GetData(programa);
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
									 + $" <td>{ episodio.Último_Perfil.Date.ToShortDateString()}</td>"
									  + $"<td>{FormatTipoPerfil(episodio.Tipo_de_Último_Perfil)}</td>"
										 + $"<td>{ episodio.Meses_sin_Perfiles_de_Evaluación_de_Progreso}</td>"
											  + "</tr>";


				}

				divtable.Visible = true;
				str += "</tbody></table>";
				lblTotalEvProgreso.Text = "Total: " + totalEvProgreso.ToString();
				divtable.InnerHtml = str;
			}
			else
			{
				divtable.Visible = false;
				lblTotalEvProgreso.Text = "En estos momentos no existen evaluaciones en progreso";
			}


		}

		string FormatTipoPerfil(string str) => (str == "AD") ? "Admisión" : (str == "EV") ? "Evaluación" : "Alta";
		string FormatTipoPerfilLink(string str) => (str == "AD") ? "admision" : (str == "EV") ? "evaluacion" : "alta";


	}
}
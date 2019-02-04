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

        }
        protected void changeProgramClick(object sender, EventArgs e)
        {
            this.Response.Redirect(ResolveClientUrl("frmLogon.aspx?changeProg=yes"));
        }
    }
}
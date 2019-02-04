using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASSMCA
{
    public partial class MainNotLogged : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string changeProg = "";
            if (!String.IsNullOrEmpty(this.Request.QueryString["changeProg"]))
            {
                changeProg = this.Request.QueryString["changeProg"].ToString();
            }
            if (changeProg=="yes" && this.Session["dsSeguridad"] == null)
            {
                this.Response.Redirect("frmLogon.aspx");
                return;
            }
        }
    }
}
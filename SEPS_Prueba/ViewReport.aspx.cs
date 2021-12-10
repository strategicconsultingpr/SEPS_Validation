using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEPS
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["dsSeguridad"] == null)
            {
                this.Response.Redirect("~/Error.aspx?errMsg=sesion");
                return;
            }

            if (!this.IsPostBack)
            {
                if (this.Request.QueryString["reportSSRS"] != null && this.Request.QueryString["reportName"] != null)
                {
                    LblTitle.InnerText = "Ver Reporte: "+ this.Request.QueryString["reportName"];
                    ShowSSRSReport(this.Request.QueryString["reportSSRS"]);

                }
                    


            }


        }

        private void ShowSSRSReport(string nameReport)
        {


            rvSiteMapping.Height = Unit.Pixel(800 - 58);
            rvSiteMapping.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ////IReportServerCredentials irsc = new CustomReportCredentials("alexie.ortiz", "Alexito@987654321", "assmca.local"); // e.g.: ("demo-001", "123456789", "ifc")
            ////rvSiteMapping.ServerReport.ReportServerCredentials = irsc;
            //////rvSiteMapping.ServerReport.ReportServerUrl = new Uri("http://192.168.100.24//ReportServer"); //Prod Server - Add the Reporting Server URL 
            

            
            rvSiteMapping.ServerReport.ReportServerUrl = new Uri("http://desktop-2e47ci0/ReportServer"); //Dev Server - Add the Reporting Server URL  
            rvSiteMapping.ServerReport.ReportPath = $"/Informes SEPS Actualizados/{nameReport}"; //Passing the Report Path   
            rvSiteMapping.ServerReport.Refresh();





        }
    }

    public class CustomReportCredentials : IReportServerCredentials
    {
        private string _UserName;
        private string _PassWord;
        private string _DomainName;

        public CustomReportCredentials(string UserName, string PassWord, string DomainName)
        {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public ICredentials NetworkCredentials
        {
            get { return new NetworkCredential(_UserName, _PassWord); }
        }

        ICredentials IReportServerCredentials.NetworkCredentials => throw new NotImplementedException();

        //ICredentials IReportServerCredentials.NetworkCredentials => throw new NotImplementedException();

        public bool GetFormsCredentials(out Cookie authCookie, out string user,
         out string password, out string authority)
        {
            authCookie = null;
            user = password = authority = null;
            return false;
        }



        }
    }
using DataUtilityCore;
using EncryptionQS;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LIM_LibrettiImpiantiViewer : System.Web.UI.Page
{

    protected string IDLibrettoImpianto
    {
        get
        {
            return (string) Request.QueryString["IDLibrettoImpianto"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        string reportPath = ConfigurationManager.AppSettings["ReportPath"];
        string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
        string destinationFile = ConfigurationManager.AppSettings["UploadLibrettiImpianti"];
        string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();

        string urlPdf = "";

        WebClient client = new WebClient();
        Byte[] buffer = null;

        string reportName = ConfigurationManager.AppSettings["ReportNameLibrettiImpianti"];
        urlPdf = ReportingServices.GetLibrettoImpiantoReport(IDLibrettoImpianto, reportName, reportUrl, reportPath, destinationFile, urlSite);

        buffer = client.DownloadData(new Uri(urlPdf));

        if (buffer != null)
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", buffer.Length.ToString());
            Response.BinaryWrite(buffer);
            Response.Flush();
            Response.Close();
        }
    }


    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    string reportPath = ConfigurationManager.AppSettings["ReportPath"];
    //    string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
    //    string destinationFile = ConfigurationManager.AppSettings["UploadLibrettiImpianti"];
    //    string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();

    //    Response.Write("reportPath:" + reportPath + "<br>");
    //    Response.Write("reportUrl:" + reportUrl + "<br>");
    //    Response.Write("destinationFile:" + destinationFile + "<br>");
    //    Response.Write("urlSite:" + urlSite + "<br>");


    //    WebClient client = new WebClient();
    //    Byte[] buffer = null;

    //    string reportName = ConfigurationManager.AppSettings["ReportNameLibrettiImpianti"];
    //    string urlPdf = "";

    //    Response.Write("reportName:" + reportName + "<br>");


    //    string pathLibrettoImpiantoReport = "";

    //    string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
    //    string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());

    //    Response.Write("usernameEncripted:" + usernameEncripted + "<br>");
    //    Response.Write("passwordEncripted:" + passwordEncripted + "<br>");

    //    ReportViewer rview = new ReportViewer();
    //    rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, "");
    //    rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
    //    rview.ServerReport.ReportPath = reportPath + reportName;

    //    ReportParameter rp0 = new ReportParameter("IDLibrettoImpianto", IDLibrettoImpianto);

    //    Response.Write("IDLibrettoImpianto:" + IDLibrettoImpianto + "<br>");

    //    rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

    //    Microsoft.Reporting.WebForms.Warning[] warnings = null;
    //    String[] streams = null;
    //    string format = "PDF";
    //    string name = String.Empty;
    //    string extension = String.Empty;
    //    string mime = String.Empty;
    //    string encoding = String.Empty;

    //    Byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);

    //    string str = Encoding.Default.GetString(ilReport);
    //    Response.Write("byte:" + str + "<br>");

    //    string fileName = reportName + "_" + IDLibrettoImpianto + "." + extension;

    //    string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
    //    string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

    //    pathLibrettoImpiantoReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

    //    Response.Write("pathLibrettoImpiantoReport:" + pathLibrettoImpiantoReport + "<br>");

    //    using (FileStream fs = new FileStream(pathLibrettoImpiantoReport, FileMode.Create))
    //    {
    //        fs.Write(ilReport, 0, ilReport.Length);
    //        fs.Flush();
    //        fs.Close();
    //    }

    //    Response.Write("UrlSITEPDF:" + urlSite + destinationFile + "/" + fileName + "<br>");
    //}

}
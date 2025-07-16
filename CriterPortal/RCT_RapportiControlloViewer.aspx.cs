using System;
using System.Configuration;
using DataUtilityCore;
using System.Web;
using System.Net;

public partial class RCT_RapportiControlloViewer : System.Web.UI.Page
{
    protected string IDRapportoControlloTecnico
    {
        get
        {
            return (string) Request.QueryString["IDRapportoControlloTecnico"];
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        string reportPath = ConfigurationManager.AppSettings["ReportPath"];
        string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
        string destinationFile = ConfigurationManager.AppSettings["UploadRapportiControllo"];
        string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();

        string urlPdf = "";

        WebClient client = new WebClient();
        Byte[] buffer = null;

        string reportName = ConfigurationManager.AppSettings["ReportNameRapportiControllo"];
        urlPdf = ReportingServices.GetRapportoControlloReport(IDRapportoControlloTecnico, reportName, reportUrl, reportPath, destinationFile, urlSite);

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
}
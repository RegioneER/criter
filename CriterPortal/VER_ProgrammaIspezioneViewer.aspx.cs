using DataUtilityCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VER_ProgrammaIspezioneViewer : System.Web.UI.Page
{
    protected string IDProgrammaIspezione
    {
        get
        {
            return (string)Request.QueryString["IDProgrammaIspezione"];
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        string reportPath = ConfigurationManager.AppSettings["ReportPath"];
        string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
        string destinationFile = ConfigurationManager.AppSettings["UploadIspezioni"];
        string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();

        string urlPdf = "";

        WebClient client = new WebClient();
        Byte[] buffer = null;

        string reportName = ConfigurationManager.AppSettings["ReportNameProgrammaIspezione"];
        urlPdf = ReportingServices.GetProgrammaIspezioneReport(IDProgrammaIspezione, reportName, reportUrl, reportPath, destinationFile, urlSite);

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
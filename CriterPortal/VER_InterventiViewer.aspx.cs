using DataUtilityCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VER_InterventiViewer : System.Web.UI.Page
{
    protected string IDAccertamento
    {
        get
        {
            return (string)Request.QueryString["IDAccertamento"];
        }
    }

    protected string IDProceduraAccertamento
    {
        get
        {
            return (string)Request.QueryString["IDProceduraAccertamento"];
        }
    }

    protected string codiceAccertamento
    {
        get
        {
            return (string)Request.QueryString["CodiceAccertamento"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        string reportPath = ConfigurationManager.AppSettings["ReportPath"];
        string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
        string destinationFile = ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + codiceAccertamento;
        string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();

        string urlPdf = "";

        WebClient client = new WebClient();
        Byte[] buffer = null;

        string reportName = ConfigurationManager.AppSettings["ReportNameRevocaIntervento"];
        urlPdf = ReportingServices.GetInterventiRevocaReport(IDAccertamento, IDProceduraAccertamento, reportName, reportUrl, reportPath, destinationFile, urlSite);

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
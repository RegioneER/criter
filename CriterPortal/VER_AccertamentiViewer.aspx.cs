using DataUtilityCore;
using System;
using System.Configuration;
using System.Net;
using System.Web;

public partial class VER_AccertamentiViewer : System.Web.UI.Page
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

    protected string iDTipoAccertamento
    {
        get
        {
            return (string)Request.QueryString["IDTipoAccertamento"];
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
        
        string reportName = string.Empty;
        switch (iDTipoAccertamento)
        {
            case "1":
                #region Rapporto di controllo tecnico
                reportName = ConfigurationManager.AppSettings["ReportNameAccertamento"];
                #endregion
                break;
            case "2":
                #region Rapporto di Ispezione
                reportName = ConfigurationManager.AppSettings["ReportNameAccertamentoIspezione"];
                #endregion
                break;
        }


        urlPdf = ReportingServices.GetAccertamentiReport(IDAccertamento, IDProceduraAccertamento, reportName, reportUrl, reportPath, destinationFile, urlSite);

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
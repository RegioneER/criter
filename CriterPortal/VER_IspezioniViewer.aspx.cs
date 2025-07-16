using DataUtilityCore;
using System;
using System.Configuration;
using System.Net;
using System.Web;

public partial class VER_IspezioniViewer : System.Web.UI.Page
{

    protected string Type
    {
        get
        {
            return (string)Request.QueryString["Type"];
        }
    }

    protected string nomeDocumentoIspezione
    {
        get
        {
            return (string)Request.QueryString["nomeDocumentoIspezione"];
        }
    }

    protected string estensione
    {
        get
        {
            return (string)Request.QueryString["estensione"];
        }
    }

    protected string IDIspezione
    {
        get
        {
            return (string)Request.QueryString["IDIspezione"];
        }
    }

    protected string IDIspezioneVisita
    {
        get
        {
            return (string)Request.QueryString["IDIspezioneVisita"];
        }
    }

    protected string CodiceIspezione
    {
        get
        {
            return (string)Request.QueryString["CodiceIspezione"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        if (Type == "Generate")
        {
            #region Generazione documenti
            string reportPath = ConfigurationManager.AppSettings["ReportPath"];
            string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
            string destinationFile = ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + IDIspezioneVisita + @"\" + CodiceIspezione;
            string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();

            string urlPdf = "";

            WebClient client = new WebClient();
            byte[] buffer = null;

            string reportName = nomeDocumentoIspezione;
            urlPdf = ReportingServices.GetIspezioneDocumentiReport(IDIspezione, reportName, reportUrl, reportPath, destinationFile, urlSite, estensione);

            buffer = client.DownloadData(new Uri(urlPdf));

            if (buffer != null)
            {
                Response.Clear();
                string contentType = string.Empty;
                if (estensione == "PDF")
                {
                    Response.ContentType = "application/pdf";
                }
                else if (estensione == "WORD")
                {
                    Response.ContentType = "application/msword";
                }

                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
                Response.Flush();
                Response.Close();
            }
            #endregion
        }
        else
        {
            #region Download documenti
            string urlDownload = ConfigurationManager.AppSettings["UrlPortal"].ToString() + ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + IDIspezioneVisita + @"\" + CodiceIspezione + @"\"+ nomeDocumentoIspezione;
                        
            WebClient client = new WebClient();
            byte[] buffer = client.DownloadData(new Uri(urlDownload));
            if (buffer != null)
            {
                Response.Clear();
                string contentType = string.Empty;
                if (estensione == ".pdf")
                {
                    Response.ContentType = "application/pdf";
                }
                
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
                Response.Flush();
                Response.Close();
            }
            #endregion
        }


    }
}
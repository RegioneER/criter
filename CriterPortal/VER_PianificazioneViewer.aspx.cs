using DataUtilityCore;
using System;
using System.Configuration;
using System.Net;
using System.Web;

public partial class VER_PianificazioneViewer : System.Web.UI.Page
{
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

    protected string fIspezioneNonSvolta
    {
        get
        {
            return (string)Request.QueryString["fIspezioneNonSvolta"];
        }
    }

    protected string fIspezioneNonSvolta2
    {
        get
        {
            return (string)Request.QueryString["fIspezioneNonSvolta2"];
        }
    }

    protected string fIspezioneRipianificataValue
    {
        get
        {
            return (string)Request.QueryString["fIspezioneRipianificata"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
    
        string reportPath = ConfigurationManager.AppSettings["ReportPath"];
        string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
        string destinationFile = ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + IDIspezioneVisita + @"\" + CodiceIspezione;
        string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();

        string urlPdf = "";

        WebClient client = new WebClient();
        byte[] buffer = null;

        bool fIspezioneNonSvolt = bool.Parse(fIspezioneNonSvolta);
        bool fIspezioneNonSvolt2 = bool.Parse(fIspezioneNonSvolta2);
        bool fIspezioneRipianificata = bool.Parse(fIspezioneRipianificataValue);

        string reportName = string.Empty;
        if (!fIspezioneRipianificata) //!fIspezioneNonSvolt || !fIspezioneNonSvolt2 ||
        {
            reportName = ConfigurationManager.AppSettings["ReportNameIspezionePianificazioneConferma"];
            urlPdf = ReportingServices.GetIspezionePianificazioneConfermaReport(IDIspezione, reportName, reportUrl, reportPath, destinationFile, urlSite);
        }
        else
        {
            reportName = ConfigurationManager.AppSettings["ReportNameIspezionePianificazioneRipianificazione"];
            urlPdf = ReportingServices.GetIspezionePianificazioneRipianificazioneReport(IDIspezione, reportName, reportUrl, reportPath, destinationFile, urlSite);
        }
        

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
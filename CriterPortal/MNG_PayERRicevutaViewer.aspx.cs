using DataUtilityCore;
using EncryptionQS;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MNG_PayERRicevutaViewer : Page
{
    protected string IDMovimento
    {
        get
        {
            if (ConfigurationManager.AppSettings["fEncryptQueryString"] == "on")
            {
                #region Encrypt on
                QueryString qs = QueryString.FromCurrent();
                QueryString qsdec = Encryption.DecryptQueryString(qs);

                try
                {
                    if (qsdec.Count > 0)
                    {
                        if (qsdec[0] != null)
                        {
                            return (string) qsdec[0];
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
                catch
                {
                    Response.Redirect("~/ErrorPage.aspx");
                }
                #endregion
            }
            else
            {
                #region Encrypt off
                try
                {
                    if (Request.QueryString["IDMovimento"] != null)
                    {
                        return (string) Request.QueryString["IDMovimento"];
                    }
                }
                catch
                {
                    Response.Redirect("~/ErrorPage.aspx");
                }
                #endregion
            }
            return "";
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        string reportPath = ConfigurationManager.AppSettings["ReportPath"];
        string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
        string destinationFile = ConfigurationManager.AppSettings["UploadRicevutePagamento"];
        string urlSiteRicevutaReport = ConfigurationManager.AppSettings["UrlPortal"];

        string urlRicevutaPagamentoPdf = string.Empty;

        WebClient client = new WebClient();
        Byte[] buffer = null;

        string ReportNameRicevutaPagamento = ConfigurationManager.AppSettings["ReportNameRicevutaPagamento"];
        urlRicevutaPagamentoPdf = ReportingServices.GetRicevutaPagamentoReport(IDMovimento, ReportNameRicevutaPagamento, reportUrl, reportPath, destinationFile, urlSiteRicevutaReport);

        buffer = client.DownloadData(new Uri(urlRicevutaPagamentoPdf));

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
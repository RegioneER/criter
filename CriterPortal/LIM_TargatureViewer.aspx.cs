using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataUtilityCore;
using EncryptionQS;

public partial class LIM_TargatureViewer : System.Web.UI.Page
{
    protected string StrTargaturaImpianto
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
                            return (string)qsdec[0];
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
                    if (Request.QueryString["StrTargaturaImpianto"] != null)
                    {
                        return (string)Request.QueryString["StrTargaturaImpianto"];
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

    protected string TargaturaType
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
                        if (qsdec[1] != null)
                        {
                            return (string)qsdec[1];
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
                    if (Request.QueryString["TargaturaType"] != null)
                    {
                        return (string)Request.QueryString["TargaturaType"];
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
        string destinationFile = ConfigurationManager.AppSettings["UploadTargatureImpianti"];
        string urlSiteTargaturaReport = ConfigurationManager.AppSettings["UrlPortal"].ToString();

        string urlTargaturaPdf = "";

        WebClient client = new WebClient();
        Byte[] buffer = null;

        switch (TargaturaType)
        {
            case "StampaTargaturaA4":
                string reportNameTargaturaA4 = ConfigurationManager.AppSettings["ReportNameTargaturaA4"];
                urlTargaturaPdf = ReportingServices.GetTargaturaImpiantoReport(StrTargaturaImpianto, reportNameTargaturaA4, reportUrl, reportPath, destinationFile, urlSiteTargaturaReport);
                break;
            case "StampaTargaturaA7":
                string reportNameTargaturaA7 = ConfigurationManager.AppSettings["ReportNameTargaturaA7"];
                urlTargaturaPdf = ReportingServices.GetTargaturaImpiantoReport(StrTargaturaImpianto, reportNameTargaturaA7, reportUrl, reportPath, destinationFile, urlSiteTargaturaReport);
                break;
            case "StampaTargaturaEtichetta":
                string reportNameTargaturaEtichetta = ConfigurationManager.AppSettings["ReportNameTargaturaEtichetta"];
                urlTargaturaPdf = ReportingServices.GetTargaturaImpiantoReport(StrTargaturaImpianto, reportNameTargaturaEtichetta, reportUrl, reportPath, destinationFile, urlSiteTargaturaReport);
                break;
        }

        buffer = client.DownloadData(new Uri(urlTargaturaPdf));

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
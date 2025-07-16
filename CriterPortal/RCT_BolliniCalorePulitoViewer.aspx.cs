using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using DataUtilityCore;
using DataLayer;
using EncryptionQS;
using System.Web.UI;

public partial class RCT_BolliniCalorePulito : Page
{
    protected string StrBollinoCalorePulito
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
                    if (Request.QueryString["StrBollinoCalorePulito"] != null)
                    {
                        return (string) Request.QueryString["StrBollinoCalorePulito"];
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
    
    protected string BollinoType
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
                            return (string) qsdec[1];
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
                    if (Request.QueryString["BollinoType"] != null)
                    {
                        return (string) Request.QueryString["BollinoType"];
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
        string destinationFile = ConfigurationManager.AppSettings["UploadBolliniCalorePulito"];
        string urlSiteBollinoReport = ConfigurationManager.AppSettings["UrlPortal"];

        string urlBollinoPdf = string.Empty;

        WebClient client = new WebClient();
        Byte[] buffer = null;

        switch (BollinoType)
        {
            case "StampaBollinoA4":
                string reportNameBollinoA4 = ConfigurationManager.AppSettings["ReportNameBollinoA4"];
                urlBollinoPdf = ReportingServices.GetBolliniReport(StrBollinoCalorePulito, reportNameBollinoA4, reportUrl, reportPath, destinationFile, urlSiteBollinoReport);
                break;
            case "StampaBollinoA7":
                string reportNameBollinoA7 = ConfigurationManager.AppSettings["ReportNameBollinoA7"];
                urlBollinoPdf = ReportingServices.GetBolliniReport(StrBollinoCalorePulito, reportNameBollinoA7, reportUrl, reportPath, destinationFile, urlSiteBollinoReport);
                break;
            case "StampaBollinoEtichetta":
                string reportNameBollinoEtichetta = ConfigurationManager.AppSettings["ReportNameBollinoEtichetta"];
                urlBollinoPdf = ReportingServices.GetBolliniReport(StrBollinoCalorePulito, reportNameBollinoEtichetta, reportUrl, reportPath, destinationFile, urlSiteBollinoReport);
                break;
        }

        buffer = client.DownloadData(new Uri(urlBollinoPdf));

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
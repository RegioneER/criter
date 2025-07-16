using EncryptionQS;
using System;
using System.Configuration;
using System.IO;

public partial class LetteraIncaricoDownload : System.Web.UI.Page
{
    protected string IDIspezioneVisita
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

            return "";
        }
    }

    protected string IDIspettore
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

            return "";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string reportPath = ConfigurationManager.AppSettings["ReportPath"];
        string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
        string destinationFile = ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + IDIspezioneVisita;
        string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();
        string reportName = ConfigurationManager.AppSettings["ReportNameLetteraIncarico"];
        string urlPdf = DataUtilityCore.ReportingServices.GetIspezioneLetteraIncaricoReport(IDIspezioneVisita.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);

        string pathPdfFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + IDIspezioneVisita + @"\" + "LetteraIncarico_" + IDIspezioneVisita + ".pdf";
                        
        FileInfo file = new FileInfo(pathPdfFile);
        if (File.Exists(pathPdfFile))
        {
            Response.Clear();
            Response.AddHeader("Content-disposition", "attachment; filename=\"" + file.Name + "\"");
            //Response.AddHeader("Content-Length", file.Length.ToString());
            Response.WriteFile(file.FullName);
            Response.ContentType = "";
            Response.End();
        }
    }

}
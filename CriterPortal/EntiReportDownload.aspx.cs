using DataUtilityCore;
using EncryptionQS;
using System;
using System.Configuration;
using System.IO;

public partial class EntiReportDownload : System.Web.UI.Page
{
    protected string IDSoggetto
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
        
    protected void Page_Load(object sender, EventArgs e)
    {
        string pathExcelFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"] + @"\" + "ReportComuni_" + IDSoggetto + ".xlsx";

        //string reportPath = ConfigurationManager.AppSettings["ReportPath"];
        //string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
        //string destinationFile = ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"];
        //string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();

        //string reportName = "ReportComuni";
        //string urlExcel = ReportingServices.GetLibrettiImpiantoEntiReport(IDSoggetto, reportName, reportUrl, reportPath, destinationFile, urlSite);

        FileInfo file = new FileInfo(pathExcelFile);
        if (File.Exists(pathExcelFile))
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
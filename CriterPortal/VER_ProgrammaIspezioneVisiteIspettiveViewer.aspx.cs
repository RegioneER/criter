using DataUtilityCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VER_ProgrammaIspezioneVisiteIspettiveViewer : System.Web.UI.Page
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

        
        string reportName = ConfigurationManager.AppSettings["ReportNameProgrammaIspezioneVisiteIspettive"];
        string urlExcel = ReportingServices.GetProgrammaIspezioneVisiteIspettiveReport(IDProgrammaIspezione, reportName, reportUrl, reportPath, destinationFile, urlSite);

        string pathExcelFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + reportName + "_" + IDProgrammaIspezione + ".xlsx";
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
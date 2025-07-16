using DataLayer;
using DataUtilityCore;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

public partial class VER_IspezioniViewerRVI : System.Web.UI.Page
{
    protected string IDIspezione
    {
        get
        {
            return (string)Request.QueryString["IDIspezione"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        using (var ctx = new CriterDataModel())
        {
            long IDIspezioneParse = long.Parse(IDIspezione);
            var ispezione = ctx.VER_Ispezione.FirstOrDefault(a => a.IDIspezione == IDIspezioneParse);

            string urlDownload = ConfigurationManager.AppSettings["UrlPortal"].ToString() + ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + ispezione.IDIspezioneVisita + @"\" + ispezione.CodiceIspezione + @"\RapportoIspezione_" + IDIspezione + ".pdf";
            WebClient client = new WebClient();
            byte[] buffer = client.DownloadData(new Uri(urlDownload));
            if (buffer != null)
            {
                Response.Clear();
                string contentType = string.Empty;
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
                Response.Flush();
                Response.Close();
            }

        }

    }
}
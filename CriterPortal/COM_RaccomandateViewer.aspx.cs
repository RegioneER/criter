using DataUtilityCore;
using EncryptionQS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class COM_RaccomandateViewer : System.Web.UI.Page
{
    
    protected string IDRichiesta
    {
        get
        {
            return Request.QueryString["IDRichiesta"];           
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        var urlRaccomandata = UtilityPosteItaliane.GetUrlRaccomandata(IDRichiesta);

        WebClient client = new WebClient();
        byte[] buffer = client.DownloadData(new Uri(urlRaccomandata));

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
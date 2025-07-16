using System;
using DataUtilityCore;

public partial class VER_AccertamentiDownload : System.Web.UI.Page
{
    protected string iDAccertamenti
    {
        get
        {
            return Request.QueryString["iDAccertamenti"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UtilityVerifiche.GetZipAccertamentiDaFirmare(iDAccertamenti);
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayerLib.Riversamento;
using DataUtilityCore;
using System.IO;


public partial class MNG_PortafoglioRendicontazioneImport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
    }
    
    protected void btnElaboraRendicontazionePayer_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (UploadFilePayer.HasFile && UploadFilePayer.PostedFile != null)
            {
                //var directoryTempPath = UtilitySalvataggioDati.GetPathSalvataggioDirectoryTemp();
                //var fileName = UploadFilePayer.PostedFile.FileName;
                //var filePath = Path.Combine(directoryTempPath, fileName);




            }
        }
    }




}
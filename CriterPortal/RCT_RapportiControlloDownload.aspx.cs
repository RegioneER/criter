using EncryptionQS;
using System;
using System.Configuration;
using System.IO;

public partial class RCT_RapportiControlloDownload : System.Web.UI.Page
{
    protected string fileName
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
        string pathTextFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadRapportiControllo"] + @"\" + fileName;
        FileInfo file = new FileInfo(pathTextFile);
        if (File.Exists(pathTextFile))
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
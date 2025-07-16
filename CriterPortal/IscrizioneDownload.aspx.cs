using EncryptionQS;
using System;
using System.Configuration;
using System.IO;

public partial class IscrizioneDownload : System.Web.UI.Page
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

    protected string IDTipoSoggetto
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
        string pathPdfFile = string.Empty;
        switch (IDTipoSoggetto)
        {
            case "1": //Persona

                break;
            case "2": //Impresa
            case "3": //Terzo responsabile
                pathPdfFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"] + @"\" + "Iscrizione_" + IDSoggetto + ".pdf";
                break;
            case "4": //Persona Responsabile tecnico

                break;
            case "5": //Distributori di combustibile

                break;
            case "6": //Software house

                break;
            case "7": //Ispettori
                pathPdfFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspettori"] + @"\" + IDSoggetto + @"\" + "IscrizioneIspettore_" + IDSoggetto + ".pdf";
                break;
            case "9": //Enti locali
                pathPdfFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"] + @"\" + "IscrizioneEnteLocale_" + IDSoggetto + ".pdf";
                break;
        }
                
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
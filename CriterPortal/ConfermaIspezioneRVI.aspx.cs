using DataUtilityCore;
using EncryptionQS;
using System;
using System.Configuration;
using System.Web;
using System.Web.UI;

public partial class ConfermaIspezioneRVI : System.Web.UI.Page
{
    protected string IDIspezione
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
                            return (string)qsdec[0];
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
                        if (qsdec[1] != null)
                        {
                            return (string)qsdec[1];
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

    protected string codiceIspezione
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
                        if (qsdec[2] != null)
                        {
                            return (string)qsdec[2];
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

    UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            rowInfoIncaricoOk.Visible = false;
            rowFirmaDigitale.Visible = true;
            rowFirmaDigitale1.Visible = true;
            rowFirmaDigitale2.Visible = true;
        }
    }

    protected void TypeFirmaRVI(long iDIspezione, long iDIspezioneVisita)
    {
        #region FIRMA
        if (UploadFileP7m.HasFile && UploadFileP7m.PostedFile != null)
        {
            #region Firma
            string FileP7m = UploadFileP7m.FileName;
            string PathP7mFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + IDIspezioneVisita + @"\" + codiceIspezione;
            
            UploadFileP7m.SaveAs(PathP7mFile + @"\" + FileP7m);

            UtilityVerifiche.CambiaStatoIspezione(iDIspezione, 4);
            UtilityVerifiche.StoricizzaStatoIspezione(iDIspezione, int.Parse(userInfo.IDUtente.ToString()));

            rowInfoIncaricoOk.Visible = true;
            rowFirmaDigitale.Visible = false;
            rowFirmaDigitale1.Visible = false;
            rowFirmaDigitale2.Visible = false;
            
            QueryString qs = new QueryString();
            qs.Add("IDIspezione", iDIspezione.ToString());
            qs.Add("IDIspezioneVisita", iDIspezioneVisita.ToString());
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "VER_Ispezioni.aspx";
            url += qsEncrypted.ToString();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + ConfigurationManager.AppSettings["UrlPortal"].ToString() + url + "';},4000);", true);
            #endregion
        }
        else
        {
            lblCheckP7m.ForeColor = System.Drawing.ColorTranslator.FromHtml("#800000");
            lblCheckP7m.Text = "Attenzione: bisogna selezionare il file firmato digitalmente in formato .p7m da caricare!";
        }
        #endregion
    }
    
    protected void btnConfermaRVI_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDIspezione = long.Parse(IDIspezione);
            long iDIspezioneVisita = long.Parse(IDIspezioneVisita);

            TypeFirmaRVI(iDIspezione, iDIspezioneVisita);
        }
    }


    protected void btnAnnulla_Click(object sender, EventArgs e)
    {
        QueryString qs = new QueryString();
        qs.Add("IDIspezione", IDIspezione);
        qs.Add("IDIspezioneVisita", IDIspezioneVisita);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "VER_Ispezioni.aspx";
        url += qsEncrypted.ToString();

        Response.Redirect(url);
    }
}
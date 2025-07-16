using DataLayer;
using DataUtilityCore;
using EncryptionQS;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI;

public partial class IscrizioneConferma : Page
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
            else
            {
                #region Encrypt off
                try
                {
                    if (Request.QueryString["IDSoggetto"] != null)
                    {
                        return (string) Request.QueryString["IDSoggetto"];
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

    protected string fSpid
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
                        if (qsdec[2] != null)
                        {
                            return (string) qsdec[2];
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

    protected string codicefiscale
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
                        if (qsdec[3] != null)
                        {
                            return (string) qsdec[3];
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

    protected string streemKey
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
                        if (qsdec[4] != null)
                        {
                            return (string) qsdec[4];
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
        if (!Page.IsPostBack)
        {
            int? iDSoggetto = null;
            int? iDTipoSoggetto = null;
            if ((!string.IsNullOrEmpty(IDSoggetto)) && (!string.IsNullOrEmpty(IDTipoSoggetto)))
            {
                iDSoggetto = int.Parse(IDSoggetto);
                lblIDSoggetto.Text = iDSoggetto.ToString();
                iDTipoSoggetto = int.Parse(IDTipoSoggetto);
                lblIDTipoSoggetto.Text = iDTipoSoggetto.ToString();
                bool flagAccesso = SecurityManager.CheckAccessPageConfirmIscrizione(iDSoggetto);

                if (!flagAccesso)
                {
                    Response.Redirect("~/IscrizioneCredenzialiError.aspx");
                }
                else
                {
                    SetTypeConfirmIscrizione(iDSoggetto, iDTipoSoggetto);
                }
            }
        }
    }

    public void SetTypeConfirmIscrizione(int? iDSoggetto, int? iDTipoSoggetto)
    {
        switch (iDTipoSoggetto)
        {
            case 1: //Persona

                break;
            case 2: //Impresa
            case 3: //Terzo responsabile
                bool fSpidCheck = false;
                if (bool.TryParse(fSpid, out fSpidCheck))
                {
                    if (!fSpidCheck)
                    {
                        //No Spid
                        rowSpid.Visible = false;
                        rowFirmaDigitale.Visible = true;

                        string reportPath = ConfigurationManager.AppSettings["ReportPath"];
                        string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
                        string destinationFile = ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"];
                        string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();
                        string reportName = ConfigurationManager.AppSettings["ReportNameSchedaIscrizione"];
                        string urlPdf = ReportingServices.GetSchedaIscrizioneReport(iDSoggetto.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);

                        SetUrlDownload(iDSoggetto.ToString(), iDTipoSoggetto.ToString());
                    }
                    else
                    {
                        //Yes Spid
                        rowSpid.Visible = true;
                        rowFirmaDigitale.Visible = false;
                    }
                }
                break;
            case 4: //Persona Responsabile tecnico

                break;
            case 5: //Distributori di combustibile
                rowSpid.Visible = false;
                rowFirmaDigitale.Visible = false;
                SetUtenza(int.Parse(IDSoggetto), int.Parse(IDTipoSoggetto), false);
                break;
            case 6: //Software house

                break;
            case 7: //Ispettori
                rowSpid.Visible = false;
                rowFirmaDigitale.Visible = true;

                string reportPathIspettore = ConfigurationManager.AppSettings["ReportPath"];
                string reportUrlIspettore = ConfigurationManager.AppSettings["ReportRemoteURL"];
                string destinationFileIspettore = ConfigurationManager.AppSettings["UploadIspettori"] + @"\" + iDSoggetto.ToString();
                string urlSiteIspettore = ConfigurationManager.AppSettings["UrlPortal"].ToString();
                string reportNameIspettore = ConfigurationManager.AppSettings["ReportNameSchedaIscrizioneIspettore"];
                string urlPdfIspettore = ReportingServices.GetSchedaIscrizioneReport(iDSoggetto.ToString(), reportNameIspettore, reportUrlIspettore, reportPathIspettore, destinationFileIspettore, urlSiteIspettore);

                SetUrlDownload(iDSoggetto.ToString(), iDTipoSoggetto.ToString());
                break;
            case 9: //Enti Locali
                rowSpid.Visible = false;
                rowFirmaDigitale.Visible = true;

                string reportPathEnteLocale = ConfigurationManager.AppSettings["ReportPath"];
                string reportUrlEnteLocale = ConfigurationManager.AppSettings["ReportRemoteURL"];
                string destinationFileEnteLocale = ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"];
                string urlSiteEnteLocale = ConfigurationManager.AppSettings["UrlPortal"].ToString();
                string reportNameEnteLocale = ConfigurationManager.AppSettings["ReportNameSchedaIscrizioneEnteLocale"];
                string urlPdfEnteLocale = ReportingServices.GetSchedaIscrizioneReport(iDSoggetto.ToString(), reportNameEnteLocale, reportUrlEnteLocale, reportPathEnteLocale, destinationFileEnteLocale, urlSiteEnteLocale);

                SetUrlDownload(iDSoggetto.ToString(), iDTipoSoggetto.ToString());
                break;
        }
    }

    public void SetUrlDownload(string iDSoggetto, string iDTipoSoggetto)
    {
        QueryString qs = new QueryString();
        qs.Add("IDSoggetto", iDSoggetto.ToString());
        qs.Add("IDTipoSoggetto", iDTipoSoggetto.ToString());
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = ConfigurationManager.AppSettings["UrlPortal"] + "IscrizioneDownload.aspx";
        url += qsEncrypted.ToString();
        
        this.imgExportPdfIscrizione.Attributes.Add("onclick",
            "var win=dhtmlwindow.open('IscrizioneExport_" + iDSoggetto + "', 'iframe', '" +
            url +
            "', 'Scarica Scheda di Iscrizione', 'width=100px,height=100px,resize=1,scrolling=1,center=1'); win.hide();");
        this.imgExportPdfIscrizione.Attributes.Add("style", "cursor: pointer;");
    }

    public void SetUtenza(int iDSoggetto, int iDTipoSoggetto, bool fSpidCheck)
    {
        int? iDRuolo = null;
        switch (iDTipoSoggetto)
        {
            case 1: //Persona

                break;
            case 2: //Impresa
            case 3: //Terzo responsabile
                if (IDTipoSoggetto == "2")
                {
                    iDRuolo = 2;
                }
                else if (IDTipoSoggetto == "3")
                {
                    iDRuolo = 5;
                }
                
                tblInfoAziendaIscrizioneOk.Visible = true;
                tblConfirmIscrizione.Visible = false;
                if (!fSpidCheck)
                {
                    //No Spid
                    SecurityManager.ActivateUserCredential(iDSoggetto, int.Parse(IDTipoSoggetto), iDRuolo, string.Empty, string.Empty, "insert");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + ConfigurationManager.AppSettings["UrlPortal"].ToString() + "';},4000);", true);
                }
                else
                {
                    //Yes Spid
                    SecurityManager.ActivateUserCredential(iDSoggetto, int.Parse(IDTipoSoggetto), iDRuolo, string.Empty, string.Empty, "Spid");
                    Response.Headers.Add("codicefiscale", codicefiscale);
                    Response.Headers.Add("Key", streemKey);
                    Response.Redirect(ConfigurationManager.AppSettings["UrlPortal"].ToString() + "Login.aspx");
                }
                break;
            case 4: //Persona Responsabile tecnico

                break;
            case 5: //Distributori di combustibile
                iDRuolo = 4;
                tblInfoAziendaIscrizioneOk.Visible = true;
                tblConfirmIscrizione.Visible = false;
                SecurityManager.ActivateUserCredential(iDSoggetto, int.Parse(IDTipoSoggetto), iDRuolo, string.Empty, string.Empty, "insert");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + ConfigurationManager.AppSettings["UrlPortal"].ToString() + "';},4000);", true);
                break;
            case 6: //Software house

                break;
            case 7: //Ispettori
                if (IDTipoSoggetto == "7")
                {
                    iDRuolo = 8;
                }

                tblInfoAziendaIscrizioneOk.Visible = true;
                tblConfirmIscrizione.Visible = false;

                UtilitySoggetti.SetIscrizioneEffettuata(iDSoggetto);
                EmailNotify.SendIscrizioneIspettore(iDSoggetto);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + ConfigurationManager.AppSettings["UrlPortal"].ToString() + "';},4000);", true);
                break;
            case 9: //Ente locale
                if (IDTipoSoggetto == "9")
                {
                    iDRuolo = 16;
                }

                tblInfoAziendaIscrizioneOk.Visible = true;
                tblConfirmIscrizione.Visible = false;
                
                SecurityManager.ActivateUserCredential(iDSoggetto, int.Parse(IDTipoSoggetto), iDRuolo, string.Empty, string.Empty, "insert");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + ConfigurationManager.AppSettings["UrlPortal"].ToString() + "';},4000);", true);
                break;
        }
    }

    protected void TypeFirmaOrSpid(int iDSoggetto, int iDTipoSoggetto, bool fSpidCheck)
    {
        if (!fSpidCheck)
        {
            #region FIRMA
            if (UploadFileP7m.HasFile && UploadFileP7m.PostedFile != null)
            {
                #region Firma
                string FileP7m = UploadFileP7m.FileName;
                string PathTempP7mFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"] + @"\Temp" + @"\";
                string PathP7mFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"] + @"\";
                string PathOriginalFile = string.Empty;
                switch (iDTipoSoggetto)
                {
                    case 1: //Persona

                        break;
                    case 2: //Impresa
                    case 3: //Terzo responsabile
                        PathOriginalFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"] + @"\" + "Iscrizione_" + iDSoggetto + ".pdf";
                        break;
                    case 4: //Persona Responsabile tecnico

                        break;
                    case 5: //Distributori di combustibile

                        break;
                    case 6: //Software house

                        break;
                    case 7: //Ispettori
                        PathOriginalFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspettori"] + @"\" + iDSoggetto + @"\" + "IscrizioneIspettore_" + iDSoggetto + ".pdf";
                        break;
                    case 9: //Enti locali
                        PathOriginalFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"] + @"\" + "IscrizioneEnteLocale_" + iDSoggetto + ".pdf";
                        break;
                }
                
                string FullPathTempP7mFile = PathTempP7mFile + FileP7m;
                UploadFileP7m.SaveAs(FullPathTempP7mFile);

                //Devo manipolare il file firmato togliendo la parte firmata per il confronto
                byte[] p7mFileToByteArray = FirmaLib.FirmaDSS.NewInstance.FileToByteArray(FullPathTempP7mFile);
                byte[] p7mDecode = FirmaLib.FirmaDSS.NewInstance.Decodep7m(p7mFileToByteArray);

                string p7mFileDecode = PathTempP7mFile + "tmp_" + FileP7m.Replace(".p7m", ".pdf");
                File.WriteAllBytes(p7mFileDecode, p7mDecode.ToArray());

                bool sameCodiceFiscale = false;
                using (CriterDataModel ctx = new CriterDataModel())
                {
                    var soggetto = ctx.COM_AnagraficaSoggetti.Where(c => c.IDSoggetto == iDSoggetto).FirstOrDefault();
                    string codiceFiscale = soggetto.CodiceFiscale;
                    byte[] p7mFileToByteArrayInfo = FirmaLib.FirmaDSS.NewInstance.FileToByteArray(PathTempP7mFile + FileP7m);
                    sameCodiceFiscale = FirmaLib.FirmaDSS.NewInstance.CheckInfoSignerCodiceFiscale(p7mFileToByteArrayInfo, codiceFiscale);
                }

                if (sameCodiceFiscale)
                {
                    //bool SameFile = new FileInfo(p7mFileDecode).Length == new FileInfo(PathOriginalFile).Length
                    //    && File.ReadAllBytes(p7mFileDecode).SequenceEqual(File.ReadAllBytes(PathOriginalFile));
                    //if (SameFile)
                    //{
                    File.Delete(p7mFileDecode);

                    string PathP7mFileDoc = string.Empty;
                    switch (iDTipoSoggetto)
                    {
                        case 1: //Persona

                            break;
                        case 2: //Impresa
                        case 3: //Terzo responsabile
                            PathP7mFileDoc = PathP7mFile + "Iscrizione_" + iDSoggetto + ".p7m";
                            break;
                        case 4: //Persona Responsabile tecnico

                            break;
                        case 5: //Distributori di combustibile

                            break;
                        case 6: //Software house

                            break;
                        case 7: //Ispettori
                            PathP7mFileDoc = PathP7mFile + "IscrizioneIspettore_" + iDSoggetto + ".p7m";
                            break;
                        case 9: //Enti Locali
                            PathP7mFileDoc = PathP7mFile + "IscrizioneEnteLocale_" + iDSoggetto + ".p7m";
                            break;
                    }

                    if (File.Exists(PathP7mFileDoc))
                    {
                        File.Delete(PathP7mFileDoc);
                    }

                    File.Move(FullPathTempP7mFile, PathP7mFileDoc);

                    bool fpass = true;
                    using (var ctx = new CriterDataModel())
                    {
                        var firma = ctx.COM_AnagraficaSoggettiFirmaDigitale.Where(c => c.IDSoggetto == iDSoggetto).ToList();
                        if (firma.Count > 0)
                        {
                            fpass = false;
                        }
                    }

                    if (fpass)
                    {
                        #region InfoSigner
                        byte[] p7mFileToByteArrayInfoSigner = FirmaLib.FirmaDSS.NewInstance.FileToByteArray(PathP7mFileDoc);

                        object[] getValInfoSigner = new object[8];
                        getValInfoSigner = FirmaLib.FirmaDSS.NewInstance.GetInfoSignerCertificate(p7mFileToByteArrayInfoSigner);
                        int? iDFirmaDigitale = UtilitySoggetti.SaveInsertDeleteDatiSoggettiFirmaDigitale(iDSoggetto,
                                                                                  2,
                                                                                  PathP7mFileDoc,
                                                                                  DateTime.Now,
                                                                                  UtilityApp.GetUserIP(),
                                                                                  getValInfoSigner[0].ToString(),
                                                                                  getValInfoSigner[1].ToString(),
                                                                                  getValInfoSigner[2].ToString(),
                                                                                  getValInfoSigner[3].ToString(),
                                                                                  getValInfoSigner[4].ToString(),
                                                                                  getValInfoSigner[5].ToString(),
                                                                                  getValInfoSigner[6].ToString(),
                                                                                  getValInfoSigner[7].ToString());
                        #endregion

                        #region Set Stato di Accreditamento
                        //21-11-2022: Solo per le imprese setto lo stato di accreditamento
                        if (IDTipoSoggetto == "2")
                        {
                            UtilitySoggetti.SaveInsertDeleteDatiAccreditamento(
                            "insert",
                            iDSoggetto,
                            8,
                            DateTime.Now,
                            null,
                            null,
                            string.Empty,
                            null,
                            null,
                            string.Empty
                            );

                            UtilitySoggetti.StoricizzaStatoAccreditamento(iDSoggetto, 5805);
                        }
                        #endregion

                        SetUtenza(iDSoggetto, iDTipoSoggetto, fSpidCheck);
                    }
                }
                else
                {
                    lblCheckP7m.ForeColor = System.Drawing.ColorTranslator.FromHtml("#800000");
                    lblCheckP7m.Text = "Attenzione: i dati del dispositivo per la firma digitale non corrispondono a quelli del soggetto richiedente!<br/>E' necessario utilizzare il dispositivo personale del Legale Rappresentante inserito durante l'iscrizione";
                }
                #endregion
            }
            else
            {
                lblCheckP7m.ForeColor = System.Drawing.ColorTranslator.FromHtml("#800000");
                lblCheckP7m.Text = "Attenzione: bisogna selezionare il file firmato digitalmente in formato .p7m da caricare!";
            }
            #endregion
        }
        else
        {
            #region SPID
            int? iDSoggettoInsert = UtilitySoggetti.SaveInsertDeleteDatiSoggettiFirmaDigitale(iDSoggetto,
                                                                                   1,
                                                                                   string.Empty,
                                                                                   DateTime.Now,
                                                                                   UtilityApp.GetUserIP(),
                                                                                   string.Empty,
                                                                                   string.Empty,
                                                                                   string.Empty,
                                                                                   string.Empty,
                                                                                   string.Empty,
                                                                                   string.Empty,
                                                                                   string.Empty,
                                                                                   string.Empty);
            if (iDSoggettoInsert != null)
            {
                #region Set Stato di Accreditamento
                //21-11-2022: Solo per le imprese setto lo stato di accreditamento
                if (IDTipoSoggetto == "2")
                {
                    UtilitySoggetti.SaveInsertDeleteDatiAccreditamento(
                    "insert",
                    iDSoggetto,
                    8,
                    DateTime.Now,
                    null,
                    null,
                    string.Empty,
                    null,
                    null,
                    string.Empty
                    );

                    UtilitySoggetti.StoricizzaStatoAccreditamento(iDSoggetto, 5805);
                }
                #endregion
                SetUtenza(iDSoggetto, iDTipoSoggetto, fSpidCheck);
            }
            #endregion
        }
    }

    protected void btnConfermaIscrizione_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            bool fSpidCheck = bool.Parse(fSpid);
            if ((!string.IsNullOrEmpty(lblIDSoggetto.Text)) && (!string.IsNullOrEmpty(lblIDTipoSoggetto.Text)))
            {
                int iDSoggetto = int.Parse(lblIDSoggetto.Text);
                int iDTipoSoggetto = int.Parse(lblIDTipoSoggetto.Text);
                TypeFirmaOrSpid(iDSoggetto, iDTipoSoggetto, fSpidCheck);
            }
        }
    }

    protected void imgInviaLinkIscrizione_Click(object sender, ImageClickEventArgs e)
    {
        int iDSoggetto = int.Parse(IDSoggetto);
        EmailNotify.SendLinkCompletamentoIscrizione(iDSoggetto);
    }

}
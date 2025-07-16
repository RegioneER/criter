using DataLayer;
using DataUtilityCore;
using DevExpress.Web;
using EncryptionQS;
using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VER_Ispezioni : System.Web.UI.Page
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

    UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        sm.RegisterPostBackControl(fileManagerDocumenti);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetDatiAll(long.Parse(IDIspezione), long.Parse(IDIspezioneVisita));
            GetComboIspezioni(long.Parse(IDIspezioneVisita));
            GenerateRapportoIspezioneVuoto();

            FileManager();
        }

        Page.Form.Attributes.Add("enctype", "multipart/form-data");

        //FiltriIspezioneRapporto(long.Parse(IDIspezione));
        IspezioneRapportoControl.FiltriAll();
    }

    #region CUSTOM VALIDATOR
    public bool ControllaDocumentiIspezioneInseriti(string iDIspezione)
    {
        return IspezioneDocumenti.CheckAllCompiledDocument(iDIspezione);
    }


    public bool ControllaDoppiaMai()
    {
        if (!chkIsIspezioneSvolta.Checked && ddlSvolgimentoIspezione.SelectedItem.Value == "7")
        {
            if (chkfIspezioneNonSvolta.Checked && chkfIspezioneNonSvolta2.Checked)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }


    public bool ControllaByPassFirmaRVI()
    {
        if (!chkIsIspezioneSvolta.Checked && ddlSvolgimentoIspezione.SelectedItem.Value != "7")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    protected void FileManager()
    {
        string path = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspezioni"] + "\\" + IDIspezioneVisita + "\\" + lblCodiceIspezione.Text;
        fileManagerDocumenti.Settings.RootFolder = path;
        //fileManagerDocumenti.Settings.InitialFolder = path;
        fileManagerDocumenti.SettingsUpload.AdvancedModeSettings.TemporaryFolder = path;

        fileManagerDocumenti.SettingsEditing.AllowCreate = true;
        fileManagerDocumenti.SettingsEditing.AllowCopy = true;
        fileManagerDocumenti.SettingsEditing.AllowDelete = true;
        fileManagerDocumenti.SettingsEditing.AllowMove = true;
        fileManagerDocumenti.SettingsEditing.AllowRename = true;
        fileManagerDocumenti.SettingsEditing.AllowDownload = true;
    }

    protected void GenerateRapportoIspezioneVuoto()
    {
        string pathVisita = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + IDIspezioneVisita + @"\" + lblCodiceIspezione.Text;
        UtilityFileSystem.CreateDirectoryIfNotExists(pathVisita);


        string reportPath = ConfigurationManager.AppSettings["ReportPath"];
        string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
        string destinationFile = ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + IDIspezioneVisita + @"\" + lblCodiceIspezione.Text;
        string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();

        string reportName = "RapportoIspezioneVuoto";
        string estensione = "PDF";

        if (!System.IO.File.Exists(ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + reportName + "_" + IDIspezione + "." + estensione))
        {
            string urlPdf = ReportingServices.GetIspezioneDocumentiReport(IDIspezione, reportName, reportUrl, reportPath, destinationFile, urlSite, estensione);
        }
    }

    protected void CopyAccertamentoDocuments(long iDAccertamento)
    {
        var documenti = UtilityVerifiche.GetDocumentiAccertamenti(iDAccertamento);
        foreach (var documento in documenti)
        {
            string pathAccertamento = ConfigurationManager.AppSettings["PathDocument"]
                                    + ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + documento.CodiceAccertamento + @"\";

            string filesAccertamento = iDAccertamento + "_" + documento.IDProceduraAccertamento + "." + "pdf";

            string pathIspezione = ConfigurationManager.AppSettings["PathDocument"]
                                 + ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + IDIspezioneVisita + @"\" + lblCodiceIspezione.Text + @"\";

            if (!System.IO.File.Exists(pathIspezione + filesAccertamento))
            {
                //System.IO.File.Copy(pathAccertamento + filesAccertamento, pathIspezione + "Accertamento_" + filesAccertamento, true);
            }
        }
    }

    protected void LogicStatiIspezione(int? iDStatoIspezione)
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
            case "7": //Coordinatore
                rowGruppoVerifica.Visible = true;
                rowTitoloNotaCoordinatore.Visible = true;
                rowNotaCoordinatore.Visible = true;
                rowQuestionariQualita.Visible = true;

                rowDocumentiPostVerifica.Visible = true;
                rowDocumentiPostVerificaTitle.Visible = true;
                switch (iDStatoIspezione)
                {
                    case 1: //Ricerca Ispettore
                        btnSaveIspezione.Visible = false;
                        btnConcludiIspezioneIspettore.Visible = false;
                        btnConcludiIspezioneCoordinatoreConAccertamento.Visible = false;
                        btnConcludiIspezioneCoordinatoreSenzaAccertamento.Visible = false;
                        btnRimandaIspezioneAdIspettore.Visible = false;
                        btnAnnullaIspezione.Visible = false;

                        btnConcludiIspezioneCoordinatoreDoppioMancatoAccesso.Visible = false;
                        btnConcludiIspezioneCoordinatoreUtenteSconosciuto.Visible = false;
                        break;
                    case 2: //Ispezione da Pianificare
                        //lblDataIspezione.Visible = false;
                        btnSaveIspezione.Visible = true;
                        btnConcludiIspezioneIspettore.Visible = true;
                        btnConcludiIspezioneCoordinatoreConAccertamento.Visible = true;
                        btnConcludiIspezioneCoordinatoreSenzaAccertamento.Visible = true;
                        btnRimandaIspezioneAdIspettore.Visible = false;
                        btnAnnullaIspezione.Visible = true;

                        btnConcludiIspezioneCoordinatoreDoppioMancatoAccesso.Visible = true;
                        btnConcludiIspezioneCoordinatoreUtenteSconosciuto.Visible = true;
                        break;
                    case 3://Ispezione Pianificata
                        //txtDataIspezione.Visible = true;
                        //lblDataIspezione.Visible = false;
                        btnConcludiIspezioneIspettore.Visible = true;
                        btnSaveIspezione.Visible = true;
                        btnConcludiIspezioneCoordinatoreConAccertamento.Visible = true;
                        btnConcludiIspezioneCoordinatoreSenzaAccertamento.Visible = true;
                        btnRimandaIspezioneAdIspettore.Visible = false;
                        btnAnnullaIspezione.Visible = true;

                        btnConcludiIspezioneCoordinatoreDoppioMancatoAccesso.Visible = true;
                        btnConcludiIspezioneCoordinatoreUtenteSconosciuto.Visible = true;
                        break;
                    case 4: //Ispezione Conclusa da Ispettore
                        //lblDataIspezione.Visible = false;
                        btnSaveIspezione.Visible = true;
                        btnConcludiIspezioneIspettore.Visible = false;
                        btnConcludiIspezioneCoordinatoreConAccertamento.Visible = true;
                        btnConcludiIspezioneCoordinatoreSenzaAccertamento.Visible = true;
                        btnRimandaIspezioneAdIspettore.Visible = true;
                        IspezioneRapportoControl.EnableIspezioneRapporto(int.Parse(iDStatoIspezione.ToString()));
                        btnAnnullaIspezione.Visible = true;

                        btnConcludiIspezioneCoordinatoreDoppioMancatoAccesso.Visible = true;
                        btnConcludiIspezioneCoordinatoreUtenteSconosciuto.Visible = true;
                        break;
                    case 7: //Ispezione Pianificata confermata
                        btnConcludiIspezioneIspettore.Visible = true;
                        btnRimandaIspezioneAdIspettore.Visible = false;
                        btnAnnullaIspezione.Visible = true;
                        break;
                    case 8: //Annullata
                        btnSaveIspezione.Visible = false;
                        btnConcludiIspezioneIspettore.Visible = false;
                        btnConcludiIspezioneCoordinatoreConAccertamento.Visible = false;
                        btnConcludiIspezioneCoordinatoreSenzaAccertamento.Visible = false;
                        btnRimandaIspezioneAdIspettore.Visible = false;
                        btnAnnullaIspezione.Visible = false;

                        btnConcludiIspezioneCoordinatoreDoppioMancatoAccesso.Visible = false;
                        btnConcludiIspezioneCoordinatoreUtenteSconosciuto.Visible = false;
                        break;
                    case 5: //Ispezione Conclusa da Coordinatore con accertamento
                    case 9: //Ispezione Conclusa da Coordinatore senza accertamento
                    case 10: //Ispezione Conclusa da Coordinatore con doppio mancato accesso (avviso non recapitato)
                    case 11: //Ispezione Conclusa da Coordinatore con utente sconosciuto
                        //lblDataIspezione.Visible = false;
                        btnSaveIspezione.Visible = false;
                        btnConcludiIspezioneIspettore.Visible = false;
                        btnConcludiIspezioneCoordinatoreConAccertamento.Visible = false;
                        btnConcludiIspezioneCoordinatoreSenzaAccertamento.Visible = false;
                        btnRimandaIspezioneAdIspettore.Visible = false;
                        btnAnnullaIspezione.Visible = false;
                        IspezioneRapportoControl.EnableIspezioneRapporto(int.Parse(iDStatoIspezione.ToString()));

                        txtNote.Enabled = false;
                        chkfIspezioneNonSvolta.Enabled = false;
                        txtDataIspezioneRipianificazione.Enabled = false;
                        ddlOrarioDa.Enabled = false;
                        ddlOrarioA.Enabled = false;
                        chkfIspezioneNonSvolta2.Enabled = false;
                        //txtDescrizioneIspezione.Enabled = false;
                        txtNotaCoordinatore.Enabled = false;

                        btnConcludiIspezioneCoordinatoreDoppioMancatoAccesso.Visible = false;
                        btnConcludiIspezioneCoordinatoreUtenteSconosciuto.Visible = false;
                        break;
                }
                break;
            case "6": //Accertatore
            case "8": //Ispettore
            case "14": //Coordinatore/Ispettore
                rowGruppoVerifica.Visible = false;
                rowTitoloNotaCoordinatore.Visible = false;
                rowNotaCoordinatore.Visible = false;
                rowQuestionariQualita.Visible = false;

                rowDocumentiPostVerifica.Visible = false;
                rowDocumentiPostVerificaTitle.Visible = false;
                switch (iDStatoIspezione)
                {
                    case 1: //Ricerca Ispettore
                        btnSaveIspezione.Visible = false;
                        btnConcludiIspezioneIspettore.Visible = false;
                        btnConcludiIspezioneCoordinatoreConAccertamento.Visible = false;
                        btnConcludiIspezioneCoordinatoreSenzaAccertamento.Visible = false;
                        btnRimandaIspezioneAdIspettore.Visible = false;
                        btnAnnullaIspezione.Visible = false;
                        IspezioneRapportoControl.EnableIspezioneRapporto(int.Parse(iDStatoIspezione.ToString()));

                        btnConcludiIspezioneCoordinatoreDoppioMancatoAccesso.Visible = false;
                        btnConcludiIspezioneCoordinatoreUtenteSconosciuto.Visible = false;
                        break;
                    case 2: //Ispezione da Pianificare
                        lblDataIspezione.Visible = false;
                        btnSaveIspezione.Visible = true;
                        btnConcludiIspezioneIspettore.Visible = false;
                        btnConcludiIspezioneCoordinatoreConAccertamento.Visible = false;
                        btnConcludiIspezioneCoordinatoreSenzaAccertamento.Visible = false;
                        btnRimandaIspezioneAdIspettore.Visible = false;
                        btnAnnullaIspezione.Visible = false;
                        IspezioneRapportoControl.EnableIspezioneRapporto(int.Parse(iDStatoIspezione.ToString()));

                        btnConcludiIspezioneCoordinatoreDoppioMancatoAccesso.Visible = false;
                        btnConcludiIspezioneCoordinatoreUtenteSconosciuto.Visible = false;
                        break;
                    case 3://Ispezione Pianificata
                        lblDataIspezione.Visible = true;
                        btnSaveIspezione.Visible = true;
                        btnConcludiIspezioneIspettore.Visible = true;
                        btnSaveIspezione.Visible = true;
                        btnConcludiIspezioneCoordinatoreConAccertamento.Visible = false;
                        btnConcludiIspezioneCoordinatoreSenzaAccertamento.Visible = false;
                        btnRimandaIspezioneAdIspettore.Visible = false;
                        btnAnnullaIspezione.Visible = false;
                        IspezioneRapportoControl.EnableIspezioneRapporto(int.Parse(iDStatoIspezione.ToString()));

                        btnConcludiIspezioneCoordinatoreDoppioMancatoAccesso.Visible = false;
                        btnConcludiIspezioneCoordinatoreUtenteSconosciuto.Visible = false;
                        break;
                    case 4: //Ispezione Conclusa da Ispettore
                        lblDataIspezione.Visible = true;
                        fileManagerDocumenti.SettingsEditing.AllowCreate = false;
                        fileManagerDocumenti.SettingsEditing.AllowCopy = false;
                        fileManagerDocumenti.SettingsEditing.AllowDelete = false;
                        fileManagerDocumenti.SettingsEditing.AllowMove = false;
                        fileManagerDocumenti.SettingsEditing.AllowRename = false;
                        fileManagerDocumenti.SettingsEditing.AllowDownload = true;
                        fileManagerDocumenti.SettingsUpload.ShowUploadPanel = false;
                        btnSaveIspezione.Visible = false;
                        btnConcludiIspezioneIspettore.Visible = false;
                        btnConcludiIspezioneCoordinatoreConAccertamento.Visible = false;
                        btnConcludiIspezioneCoordinatoreSenzaAccertamento.Visible = false;
                        btnRimandaIspezioneAdIspettore.Visible = false;
                        btnAnnullaIspezione.Visible = false;

                        IspezioneRapportoControl.EnableIspezioneRapporto(int.Parse(iDStatoIspezione.ToString()));

                        txtNote.Enabled = false;
                        chkfIspezioneNonSvolta.Enabled = false;
                        txtDataIspezioneRipianificazione.Enabled = false;
                        ddlOrarioDa.Enabled = false;
                        ddlOrarioA.Enabled = false;
                        chkfIspezioneNonSvolta2.Enabled = false;
                        //txtDescrizioneIspezione.Enabled = false;
                        txtNotaCoordinatore.Enabled = false;

                        btnConcludiIspezioneCoordinatoreDoppioMancatoAccesso.Visible = false;
                        btnConcludiIspezioneCoordinatoreUtenteSconosciuto.Visible = false;

                        chkIsIspezioneSvolta.Enabled = false;
                        ddlSvolgimentoIspezione.Enabled = false;
                        break;
                    case 5: //Ispezione Conclusa da Coordinatore con accertamento
                    case 6: //Ispezione Confermata da Inviare LAI
                    case 8://Ispezione Annullata
                    case 9://Ispezione Conclusa da Coordinatore senza accertamento
                        lblDataIspezione.Visible = true;
                        fileManagerDocumenti.SettingsEditing.AllowCreate = false;
                        fileManagerDocumenti.SettingsEditing.AllowCopy = false;
                        fileManagerDocumenti.SettingsEditing.AllowDelete = false;
                        fileManagerDocumenti.SettingsEditing.AllowMove = false;
                        fileManagerDocumenti.SettingsEditing.AllowRename = false;
                        fileManagerDocumenti.SettingsEditing.AllowDownload = true;
                        fileManagerDocumenti.SettingsUpload.ShowUploadPanel = false;
                        btnSaveIspezione.Visible = false;
                        btnConcludiIspezioneIspettore.Visible = false;
                        btnConcludiIspezioneCoordinatoreConAccertamento.Visible = false;
                        btnConcludiIspezioneCoordinatoreSenzaAccertamento.Visible = false;
                        btnRimandaIspezioneAdIspettore.Visible = false;
                        IspezioneRapportoControl.EnableIspezioneRapporto(int.Parse(iDStatoIspezione.ToString()));

                        txtNote.Enabled = false;
                        chkfIspezioneNonSvolta.Enabled = false;
                        txtDataIspezioneRipianificazione.Enabled = false;
                        ddlOrarioDa.Enabled = false;
                        ddlOrarioA.Enabled = false;
                        chkfIspezioneNonSvolta2.Enabled = false;
                        //txtDescrizioneIspezione.Enabled = false;
                        txtNotaCoordinatore.Enabled = false;

                        btnConcludiIspezioneCoordinatoreDoppioMancatoAccesso.Visible = false;
                        btnConcludiIspezioneCoordinatoreUtenteSconosciuto.Visible = false;

                        chkIsIspezioneSvolta.Enabled = false;
                        ddlSvolgimentoIspezione.Enabled = false;
                        break;
                    case 7://Ispezione Pianificata confermata
                        lblDataIspezione.Visible = true;
                        btnSaveIspezione.Visible = true;

                        if (chkfIspezioneNonSvolta.Checked)
                        {
                            //chkfIspezioneNonSvolta.Enabled = false;
                            //txtDataIspezioneRipianificazione.Enabled = false;

                            if (!string.IsNullOrEmpty(txtDataIspezioneRipianificazione.Text))
                            {
                                DateTime dataIspezioneRipianificazione = DateTime.Parse(txtDataIspezioneRipianificazione.Text);
                                if (dataIspezioneRipianificazione.AddDays(-5) <= DateTime.Now)
                                {
                                    btnConcludiIspezioneIspettore.Enabled = true;
                                }
                                else
                                {
                                    btnConcludiIspezioneIspettore.Enabled = false;
                                }
                            }
                        }

                        btnConcludiIspezioneIspettore.Visible = true;
                        btnSaveIspezione.Visible = true;
                        btnConcludiIspezioneCoordinatoreConAccertamento.Visible = false;
                        btnConcludiIspezioneCoordinatoreSenzaAccertamento.Visible = false;
                        btnRimandaIspezioneAdIspettore.Visible = false;
                        IspezioneRapportoControl.EnableIspezioneRapporto(int.Parse(iDStatoIspezione.ToString()));

                        btnConcludiIspezioneCoordinatoreDoppioMancatoAccesso.Visible = false;
                        btnConcludiIspezioneCoordinatoreUtenteSconosciuto.Visible = false;
                        break;
                }
                break;
            case "12": //Segreteria Verifiche
                rowDocumentiPostVerifica.Visible = true;
                break;
        }
    }

    protected void LogicButtonConcludiIspezioneIspettore(bool fIspezioneNonSvolta, bool fIspezioneNonSvolta2, bool IsPagamentoMai1, bool IsPagamentoMai2)
    {
        var iDRuolo = userInfo.IDRuolo;

        rowIsPagamentoMai1.Visible = fIspezioneNonSvolta && (iDRuolo == 1 || iDRuolo == 7 || iDRuolo == 14);
        rowIsPagamentoMai2.Visible = fIspezioneNonSvolta && fIspezioneNonSvolta2 && (iDRuolo == 1 || iDRuolo == 7 || iDRuolo == 14);

        UploadFilePagamentoMai1.Visible = IsPagamentoMai1;
        btnUploadFilePagamentoMai1.Visible = IsPagamentoMai1;
        UploadFilePagamentoMai2.Visible = IsPagamentoMai2;
        btnUploadFilePagamentoMai2.Visible = IsPagamentoMai2;

        if (fIspezioneNonSvolta)
        {
            rowDataRipianificazioneIspezione.Visible = true;
            rowfIspezioneNonSvolta2.Visible = true;

            //RequiredFieldValidator rfvProgettista = (RequiredFieldValidator)IspezioneRapportoControl.FindControl("rfvProgettista");
            //rfvProgettista.Enabled = false;

            //RequiredFieldValidator rfvProtocolloDepositoComune = (RequiredFieldValidator)IspezioneRapportoControl.FindControl("rfvProtocolloDepositoComune");
            //rfvProtocolloDepositoComune.Enabled = false;

            //RequiredFieldValidator rfvPotenzaTermicaUtileProgetto = (RequiredFieldValidator)IspezioneRapportoControl.FindControl("rfvPotenzaTermicaUtileProgetto");
            //rfvPotenzaTermicaUtileProgetto.Enabled = false;

            //RequiredFieldValidator rfvDocumentazioneTecnicaAsseverata = (RequiredFieldValidator)IspezioneRapportoControl.FindControl("rfvDocumentazioneTecnicaAsseverata");
            //rfvDocumentazioneTecnicaAsseverata.Enabled = false;

            //RequiredFieldValidator rfvCategoria = (RequiredFieldValidator)IspezioneRapportoControl.FindControl("rfvCategoria");
            //rfvCategoria.Enabled = false;

            RequiredFieldValidator rfvInstallazioneInternaEsterna = (RequiredFieldValidator)IspezioneRapportoControl.FindControl("rfvInstallazioneInternaEsterna");
            rfvInstallazioneInternaEsterna.Enabled = false;

            RequiredFieldValidator rfvLibrettoUsoManutenzione = (RequiredFieldValidator)IspezioneRapportoControl.FindControl("rfvLibrettoUsoManutenzione");
            rfvLibrettoUsoManutenzione.Enabled = false;

            RequiredFieldValidator rfvDichiarazione = (RequiredFieldValidator)IspezioneRapportoControl.FindControl("rfvDichiarazione");
            rfvDichiarazione.Enabled = false;

            RequiredFieldValidator rfvPatentinoOperatoreUltimoControllo = (RequiredFieldValidator)IspezioneRapportoControl.FindControl("rfvPatentinoOperatoreUltimoControllo");
            rfvPatentinoOperatoreUltimoControllo.Enabled = false;


            if (fIspezioneNonSvolta2)
            {
                btnConcludiIspezioneIspettore.OnClientClick = "javascript:return confirm('Confermi di concludere Ispezione ed inviare al coordinatore?');";
                btnConcludiIspezioneIspettore.Text = "CONCLUDI ISPEZIONE ED INVIA A COORDINATORE";
            }
            else
            {
                chkIsPagamentoMai2.Checked = false;
                if (iDRuolo == 1 || iDRuolo == 7)
                {
                    btnConcludiIspezioneIspettore.OnClientClick = "javascript:return confirm('Confermi di concludere ispezione?');";
                    btnConcludiIspezioneIspettore.Text = "CONCLUDI ISPEZIONE";
                }
                else if (iDRuolo == 8 || iDRuolo == 14)
                {
                    btnConcludiIspezioneIspettore.OnClientClick = "javascript:return confirm('Confermi di firmare il rapporto di ispezione e concludere ispezione?');";
                    btnConcludiIspezioneIspettore.Text = "FIRMA RVI E CONCLUDI ISPEZIONE";
                }
            }
        }
        else
        {
            rowDataRipianificazioneIspezione.Visible = false;
            txtDataIspezioneRipianificazione.Text = string.Empty;
            rowfIspezioneNonSvolta2.Visible = false;
            chkIsPagamentoMai1.Checked = false;

            if (iDRuolo == 1 || iDRuolo == 7)
            {
                btnConcludiIspezioneIspettore.OnClientClick = "javascript:return confirm('Confermi di concludere ispezione?');";
                btnConcludiIspezioneIspettore.Text = "CONCLUDI ISPEZIONE";
            }
            else if (iDRuolo == 8 || iDRuolo == 14)
            {
                btnConcludiIspezioneIspettore.OnClientClick = "javascript:return confirm('Confermi di firmare il rapporto di ispezione e concludere ispezione?');";
                btnConcludiIspezioneIspettore.Text = "FIRMA RVI E CONCLUDI ISPEZIONE";
            }
        }
    }

    protected void chkfIspezioneNonSvolta_CheckedChanged(object sender, EventArgs e)
    {
        ddlOrarioDa.SelectedIndex = 0;
        ddlOrarioA.SelectedIndex = 0;
        LogicButtonConcludiIspezioneIspettore(chkfIspezioneNonSvolta.Checked, chkfIspezioneNonSvolta2.Checked, chkIsPagamentoMai1.Checked, chkIsPagamentoMai2.Checked);

        SetObblifgatorietaDocumentiIspezione();
    }

    protected void chkfIspezioneNonSvolta2_CheckedChanged(object sender, EventArgs e)
    {
        LogicButtonConcludiIspezioneIspettore(chkfIspezioneNonSvolta.Checked, chkfIspezioneNonSvolta2.Checked, chkIsPagamentoMai1.Checked, chkIsPagamentoMai2.Checked);

        SetObblifgatorietaDocumentiIspezione();
    }

    public void GetDatiAll(long iDIspezione, long iDIspezioneVisita)
    {
        GetDatiIspezione(iDIspezione, iDIspezioneVisita);
        GetDatiIspezioneStorico(iDIspezione);
        IspezioneRapportoControl.GetDatiRapportoAll(iDIspezione);
        IspezioneGruppoVerifica.GetGruppoVerifica(iDIspezione, iDIspezioneVisita);
        IspezioneDocumenti.IDIspezione = iDIspezione.ToString();
        IspezioneDocumenti.IDIspezioneVisita = iDIspezioneVisita.ToString();
        IspezioneDocumenti.CodiceIspezione = lblCodiceIspezione.Text;
        IspezioneDocumenti.EnableIspezioneDocumenti(int.Parse(lblIDStatoIspezione.Text));
        GetDatiRaccomandate(iDIspezione);
        GetDatiNotificaAperturaIspezione(iDIspezione);
        GetDatiQuestionariQualita(iDIspezione, iDIspezioneVisita);
    }

    public void GetDatiRaccomandate(long iDIspezione)
    {
        UCRaccomandate.IDIspezione = iDIspezione.ToString();
    }

    public void GetDatiQuestionariQualita(long iDIspezione, long iDIspezioneVisita)
    {
        UCIspezioniQuestionariQualita.IDIspezioneVisita = iDIspezioneVisita.ToString();
        UCIspezioniQuestionariQualita.IDIspezioneSelected = iDIspezione.ToString();
    }

    public void GetDatiNotificaAperturaIspezione(long iDIspezione)
    {
        UCIspezioneNotificaAperturaIspezione.IDIspezione = iDIspezione.ToString();
    }


    public void GetDatiIspezione(long iDIspezione, long iDIspezioneVisita)
    {
        LoadAllDropDownlist();

        using (var ctx = new CriterDataModel())
        {
            var ispezione = ctx.V_VER_Ispezioni.Where(c => c.IDIspezione == iDIspezione).FirstOrDefault();
            if (ispezione != null)
            {
                lblIDStatoIspezione.Text = ispezione.IDStatoIspezione.ToString();
                lblTipoIspezione.Text = ispezione.TipoIspezione;
                if (ispezione.IDAccertamento != null)
                {
                    rowAccertamento.Visible = true;

                    QueryString qs = new QueryString();
                    qs.Add("IDAccertamento", ispezione.IDAccertamento.ToString());
                    QueryString qsEncrypted = Encryption.EncryptQueryString(qs);
                    string url = "VER_Accertamenti.aspx";

                    url += qsEncrypted.ToString();
                    btnViewAccertamento.NavigateUrl = url;
                }
                else
                {
                    rowAccertamento.Visible = false;
                }


                lblCodiceVisita.Text = ispezione.IDIspezioneVisita.ToString();
                lblCodiceIspezione.Text = ispezione.CodiceIspezione;

                lblIspettore.Text = ispezione.Ispettore;
                lblOsservatore.Text = ispezione.Osservatore;
                lblStatoIspezione.Text = ispezione.StatoIspezione;

                var visitaInfo = ctx.VER_IspezioneVisitaInfo.Where(c => c.IDIspezioneVisitaInfo == ispezione.IDIspezioneVisitaInfo).FirstOrDefault();
                if (visitaInfo != null)
                {
                    lblNoteIspezione.Text = visitaInfo.NoteIspezioneVisita;
                }

                lblDataIspezione.Text = string.Format("{0:dd/MM/yyyy}", ispezione.DataIspezione);

                if (!string.IsNullOrEmpty(ispezione.OrarioDa) && !string.IsNullOrEmpty(ispezione.OrarioA))
                {
                    lblOrarioDa.Text = ispezione.OrarioDa;
                    lblOrarioA.Text = ispezione.OrarioA;
                    lblOrarioDaDesc.Visible = true;
                    lblOrarioADesc.Visible = true;
                }

                txtNote.Text = ispezione.Note;
                txtNotaCoordinatore.Text = ispezione.NotaCoordinatore;

                txtDataIspezioneRipianificazione.Text = string.Format("{0:dd/MM/yyyy}", ispezione.DataIspezioneRipianificazione);
                if (ispezione.IDOrarioDa != null)
                {
                    ddOrario(ddlOrarioDa, null);
                    ddlOrarioDa.SelectedValue = ispezione.IDOrarioDa.ToString();
                }
                else
                {
                    ddOrario(ddlOrarioDa, null);
                }
                if (ispezione.IDOrarioA != null)
                {
                    ddOrario(ddlOrarioA, null);
                    ddlOrarioA.SelectedValue = ispezione.IDOrarioA.ToString();
                }
                else
                {
                    ddOrario(ddlOrarioA, null);
                }

                chkfIspezioneNonSvolta.Checked = ispezione.fIspezioneNonSvolta;
                chkfIspezioneNonSvolta2.Checked = ispezione.fIspezioneNonSvolta2;
                chkIsPagamentoMai1.Checked = ispezione.IsPagamentoMai1;
                chkIsPagamentoMai2.Checked = ispezione.IsPagamentoMai2;

                LogicButtonConcludiIspezioneIspettore(ispezione.fIspezioneNonSvolta, ispezione.fIspezioneNonSvolta2, ispezione.IsPagamentoMai1, ispezione.IsPagamentoMai2);

                lblIDRapportoControllo.Text = ispezione.IDRapportoDiControllo.ToString();


                lblImpresaManutenzione.Text = ispezione.CodiceSoggetto + "&nbsp;-&nbsp;" + ispezione.NomeAzienda;
                lblImpresaManutenzioneIndirizzo.Text = ispezione.IndirizzoAzienda;
                lblImpresaManutenzioneTelefono.Text = ispezione.Telefono;
                lblImpresaManutenzioneEmail.Text = ispezione.Email;
                lblImpresaManutenzioneEmailPec.Text = ispezione.EmailPec;

                lblResponsabileImpianto.Text = ispezione.NomeResponsabile + " " + ispezione.CognomeResponsabile;
                lblResponsabileImpiantoIndirizzo.Text = ispezione.IndirizzoResponsabile + "&nbsp;" + ispezione.CivicoResponsabile + "&nbsp;" + ispezione.ComuneResponsabile;
                lblResponsabileImpiantoEmail.Text = ispezione.EmailResponsabile;
                lblResponsabileImpiantoEmailPec.Text = ispezione.EmailPecResponsabile;

                lblDatiImpiantoCodiceTargatura.Text = ispezione.CodiceTargatura;
                lblDatiImpiantoIndirizzo.Text = ispezione.Indirizzo + "&nbsp;" + ispezione.Civico + "&nbsp;" + ispezione.Comune + "&nbsp;(" + ispezione.SiglaProvincia + ")";
                lblDatiImpiantoPotenza.Text = ispezione.PotenzaTermicaUtileNominaleKw.ToString();
                lblDatiImpiantoCombustibile.Text = ispezione.TipologiaCombustibile;

                QueryString qsLibretto = new QueryString();
                qsLibretto.Add("IDLibrettoImpianto", ispezione.IDLibrettoImpianto.ToString());
                QueryString qsEncryptedLibretto = Encryption.EncryptQueryString(qsLibretto);
                string urlLibretto = "LIM_LibrettiImpianti.aspx";
                urlLibretto += qsEncryptedLibretto.ToString();
                btnViewLibrettoImpianto.NavigateUrl = urlLibretto;

                var CodiceProgressivo = ispezione.CodiceProgressivo != null ? (int)ispezione.CodiceProgressivo : 0;
                GetRapportiControllo((int)ispezione.IDTargaturaImpianto, ispezione.Prefisso, CodiceProgressivo);

                //Visualizza Modifica Libretto
                QueryString qsPopUp = new QueryString();
                qsPopUp.Add("IDLibrettoImpianto", ispezione.IDLibrettoImpianto.ToString());
                QueryString qsEncryptedPopUp = Encryption.EncryptQueryString(qsPopUp);

                string urlPopUp = "LIM_LibrettiImpiantiModifica.aspx";
                urlPopUp += qsEncryptedPopUp.ToString();
                ASPxPopupControlModificaLibretto.ContentUrl = urlPopUp;

                GetDatiIspezioniPrecedenti(ispezione.IDTargaturaImpianto, ispezione.IDIspezioneVisita);

                if (ispezione.IDAccertamento != null)
                {
                    long iDAccertamento = long.Parse(ispezione.IDAccertamento.ToString());
                    CopyAccertamentoDocuments(iDAccertamento);
                }

                if (ispezione.IsAvvisoPecTerzoResponsabile)
                {
                    rowTitoloAvvisoPecTerzoResponsabile.Visible = true;
                    rowAvvisoPecTerzoResponsabile.Visible = true;
                    lblAvvisoPecTerzoResponsabile.Text = ispezione.DettagliAvvisoPecTerzoResponsabile;
                }
                else
                {
                    rowTitoloAvvisoPecTerzoResponsabile.Visible = false;
                    rowAvvisoPecTerzoResponsabile.Visible = false;
                }

                QueryString qsPopUpRimandaIspettore = new QueryString();
                qsPopUpRimandaIspettore.Add("IDIspezione", ispezione.IDIspezione.ToString());
                QueryString qsEncryptedPopUpRimandaIspettore = Encryption.EncryptQueryString(qsPopUpRimandaIspettore);

                string urlPopUpRimandaIspettore = "VER_IspezioniRimandaAdIspettore.aspx";
                urlPopUpRimandaIspettore += qsEncryptedPopUpRimandaIspettore.ToString();
                ASPxPopupControlRimandaAdIspettore.ContentUrl = urlPopUpRimandaIspettore;

                chkIsIspezioneSvolta.Checked = ispezione.IsIspezioneSvolta;
                ddSvogimentoIspezione(ispezione.IDSvolgimentoIspezione, ispezione.IsIspezioneSvolta);
                VisibleHiddenAltroMotivoSvolgimentoIspezione(ispezione.IDSvolgimentoIspezione, ispezione.AltroSvolgimentoIspezione);

                if (ispezione.IDSvolgimentoIspezione != null)
                {
                    ddlSvolgimentoIspezione.SelectedValue = ispezione.IDSvolgimentoIspezione.ToString();
                }
                else
                {
                    ddlSvolgimentoIspezione.SelectedIndex = 0;
                }
                LogicStatiIspezione(ispezione.IDStatoIspezione);
            }
        }
    }

    #region Rapporti
    public void dgRapporti_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            if (lblIDRapportoControllo.Text == e.Item.Cells[0].Text)
            {
                e.Item.BackColor = System.Drawing.Color.Orange;
            }

            HyperLink lnkRapportoControllo = (HyperLink)(e.Item.Cells[4].FindControl("lnkRapportoControllo"));

            QueryString qsRapporto = new QueryString();
            qsRapporto.Add("IDRapportoControlloTecnico", e.Item.Cells[0].Text);
            qsRapporto.Add("IDTipologiaRCT", e.Item.Cells[1].Text);
            qsRapporto.Add("IDSoggetto", e.Item.Cells[2].Text);
            qsRapporto.Add("IDSoggettoDerived", e.Item.Cells[3].Text);
            QueryString qsEncryptedRapporto = Encryption.EncryptQueryString(qsRapporto);

            string urlRapporto = "RCT_RapportoDiControlloTecnico.aspx";
            urlRapporto += qsEncryptedRapporto.ToString();
            lnkRapportoControllo.NavigateUrl = urlRapporto;
        }
    }

    public void GetRapportiControllo(int iDTargaturaImpianto, string Prefisso, int CodiceProgressivo)
    {
        var rapporti = UtilityRapportiControllo.GetValoriRapportoControllo(iDTargaturaImpianto, Prefisso, CodiceProgressivo);
        dgRapporti.DataSource = rapporti;
        dgRapporti.DataBind();
    }

    #endregion

    protected void VisibleHiddenAltroMotivoSvolgimentoIspezione(int? IDSvolgimentoIspezione, string AltroSvolgimentoIspezione)
    {
        if (IDSvolgimentoIspezione == 1)
        {
            pnlAltroSvolgimentoIspezione.Visible = true;
            txtAltroSvolgimentoIspezione.Text = AltroSvolgimentoIspezione;
        }
        else
        {
            pnlAltroSvolgimentoIspezione.Visible = false;
            txtAltroSvolgimentoIspezione.Text = string.Empty;
        }
    }

    #region Ispezioni Precedenti

    public void dgIspezioniPrecedenti_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            ImageButton imgPdf = (ImageButton)(e.Item.Cells[1].FindControl("ImgPdf"));

            string pathPdfFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + e.Item.Cells[1].Text.ToString() + @"\" + e.Item.Cells[2].Text.ToString() + @"\RapportoIspezione_" + e.Item.Cells[0].Text.ToString() + ".pdf";
            if (System.IO.File.Exists(pathPdfFile))
            {
                imgPdf.Visible = true;
                imgPdf.Attributes.Add("onclick", "var winRVI=dhtmlwindow.open('RVI_" + e.Item.Cells[0].Text + "', 'iframe', 'VER_IspezioniViewerRVI.aspx?IDIspezione=" + e.Item.Cells[0].Text + "', 'RVI_" + e.Item.Cells[0].Text + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); ");
            }
            else
            {
                imgPdf.Visible = false;
            }
        }
    }

    public void GetDatiIspezioniPrecedenti(int? IDTargaturaImpianto, long IDIspezioneVisita)
    {
        var ispezioniPrecedenti = UtilityVerifiche.GetIspezioniPrecedenti(IDTargaturaImpianto, IDIspezioneVisita);
        dgIspezioniPrecedenti.DataSource = ispezioniPrecedenti;
        dgIspezioniPrecedenti.DataBind();

        rowTitleIspezioniPrecedenti.Visible = (ispezioniPrecedenti.Count > 0) ? true : false;
        rowIspezioniPrecedenti.Visible = (ispezioniPrecedenti.Count > 0) ? true : false;

    }

    #endregion

    public void GetComboIspezioni(long iDIspezioneVisita)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            var ispezioni = ctx.VER_Ispezione.Where(c => c.IDIspezioneVisita == iDIspezioneVisita).ToList();

            var iDRuolo = userInfo.IDRuolo;
            foreach (var c in ispezioni)
            {
                if (iDRuolo == 1 || iDRuolo == 7)
                {
                    ListEditItem list = new ListEditItem();
                    list.Value = c.IDIspezione;

                    if (c.IDStatoIspezione == 4 || c.IDStatoIspezione == 5 || c.IDStatoIspezione == 9)
                    {
                        list.Text = "<span style='color:green'><b>Codice Ispezione - " + c.CodiceIspezione + "</b></span>";
                    }
                    else
                    {
                        list.Text = "<b>Codice Ispezione - " + c.CodiceIspezione + "</b>";
                    }

                    rblIspezioni.Items.Add(list);
                    rblIspezioni.ToolTip = "Cliccare per cambiare ispezione";
                    rblIspezioni.DataBind();
                }
                else if (iDRuolo == 8 || iDRuolo == 14)
                {
                    ListEditItem list = new ListEditItem();
                    list.Value = c.IDIspezione;

                    if (c.IDStatoIspezione == 4 || c.IDStatoIspezione == 5 || c.IDStatoIspezione == 9)
                    {
                        list.Text = "<span style='color:green'><b>Codice Ispezione - " + c.CodiceIspezione + "</b></span>";
                    }
                    else
                    {
                        list.Text = "<b>Codice Ispezione - " + c.CodiceIspezione + "</b>";
                    }

                    rblIspezioni.Items.Add(list);
                    rblIspezioni.ToolTip = "Cliccare per cambiare ispezione";
                    rblIspezioni.DataBind();
                }
            }
            rblIspezioni.Value = IDIspezione;
        }
    }

    protected void LoadAllDropDownlist()
    {
        ComboBoxStatoIspezione(null);
    }

    protected void ddOrario(DropDownList ddlOrario, int? IDPresel)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_Orario(IDPresel);
        ddlOrario.DataValueField = "IDOrario";
        ddlOrario.DataTextField = "Orario";
        ddlOrario.DataSource = ls;
        ddlOrario.DataBind();

        ListItem myItem = new ListItem("-- Selezionare --", "0");
        ddlOrario.Items.Insert(0, myItem);
        ddlOrario.SelectedIndex = 0;
    }

    protected void ddSvogimentoIspezione(int? IDPresel, bool IsIspezioneSvolta)
    {
        ddlSvolgimentoIspezione.Items.Clear();

        var ls = LoadDropDownList.LoadDropDownList_SYS_SvolgimentoIspezione(IDPresel, IsIspezioneSvolta);
        ddlSvolgimentoIspezione.DataValueField = "IDSvolgimentoIspezione";
        ddlSvolgimentoIspezione.DataTextField = "SvolgimentoIspezione";
        ddlSvolgimentoIspezione.DataSource = ls;
        ddlSvolgimentoIspezione.DataBind();

        ListItem myItem = new ListItem("-- Selezionare --", "0");
        ddlSvolgimentoIspezione.Items.Insert(0, myItem);
        //ddlSvolgimentoIspezione.SelectedIndex = 0;
    }

    protected void ComboBoxStatoIspezione(int? idPresel)
    {
        //cmbStatoIspezione.Text = "";
        //cmbStatoIspezione.DataSource = LoadDropDownList.LoadDropDownList_SYS_StatoIspezione(idPresel);
        //cmbStatoIspezione.DataBind();

        //ListEditItem myItem = new ListEditItem("-- Selezionare --", "0");
        //cmbStatoIspezione.Items.Insert(0, myItem);

        //cmbStatoIspezione.SelectedIndex = 0;
    }

    public void GetDatiIspezioneStorico(long iDIspezione)
    {
        var ispezioniStorico = UtilityVerifiche.GetIspezioneStorico(iDIspezione);
        if (ispezioniStorico != null)
        {
            DataGrid.DataSource = ispezioniStorico;
            DataGrid.DataBind();
        }
    }

    protected void btnSaveIspezione_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblFeedbackDocumentiIspezioneInseriti.Visible = false;
            lblFeedbackDocumentiDoppiaMai.Visible = false;

            long iDIspezione = long.Parse(IDIspezione);

            UtilityVerifiche.SaveIspezione(iDIspezione,
                                           UtilityApp.ParseNullableDatetime(lblDataIspezione.Text),
                                           txtNote.Text,
                                           UtilityApp.ParseNullableDatetime(txtDataIspezioneRipianificazione.Text),
                                           chkfIspezioneNonSvolta.Checked,
                                           chkfIspezioneNonSvolta2.Checked,
                                           chkIsPagamentoMai1.Checked,
                                           chkIsPagamentoMai2.Checked,
                                           txtNotaCoordinatore.Text,
                                           chkIsIspezioneSvolta.Checked,
                                           UtilityApp.ParseNullableInt(ddlSvolgimentoIspezione.SelectedItem.Value),
                                           txtAltroSvolgimentoIspezione.Text
                                          );

            if (chkfIspezioneNonSvolta.Checked)
            {
                UtilityVerifiche.SaveDataIspezioneRewrite(iDIspezione,
                                                          UtilityApp.ParseNullableDatetime(txtDataIspezioneRipianificazione.Text),
                                                          UtilityApp.ParseNullableInt(ddlOrarioDa.SelectedValue),
                                                          UtilityApp.ParseNullableInt(ddlOrarioA.SelectedValue));
            }

            //if (IspezioneRapportoControl.fIspezioneConRapporto(iDIspezione))
            //{
            IspezioneRapportoControl.SalvaDatiIspezioneRapporto(iDIspezione);
            //}
            //if (UtilityVerifiche.fStatoIspezioneNonConclusa(long.Parse(IDIspezioneVisita)))
            //{
            //    UtilityVerifiche.CambiaStatoIspezioneMassivo(long.Parse(IDIspezioneVisita), 3 , UtilityApp.ParseNullableDatetime(txtDataIspezione.Text));
            //}

            UtilityVerifiche.SetObbligatorietaIspezioneDocumenti(iDIspezione);
            UtilityVerifiche.StoricizzaStatoIspezione(iDIspezione, int.Parse(userInfo.IDUtente.ToString()));
            GetDatiAll(iDIspezione, long.Parse(IDIspezioneVisita));

            IspezioneDocumenti.DataBind();
        }
    }

    protected void rblIspezioni_ValueChanged(object sender, EventArgs e)
    {
        if (rblIspezioni.SelectedIndex > -1)
        {
            QueryString qs = new QueryString();
            qs.Add("IDIspezione", rblIspezioni.Value.ToString());
            qs.Add("IDIspezioneVisita", IDIspezioneVisita);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "VER_Ispezioni.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
    }

    protected void btnConcludiIspezioneIspettore_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblFeedbackDocumentiIspezioneInseriti.Visible = false;
            lblFeedbackDocumentiDoppiaMai.Visible = false;

            long iDIspezione = long.Parse(IDIspezione);
            long iDIspezioneVisita = long.Parse(IDIspezioneVisita);

            UtilityVerifiche.SaveIspezione(iDIspezione,
                                           UtilityApp.ParseNullableDatetime(lblDataIspezione.Text),
                                           txtNote.Text,
                                           UtilityApp.ParseNullableDatetime(txtDataIspezioneRipianificazione.Text),
                                           chkfIspezioneNonSvolta.Checked,
                                           chkfIspezioneNonSvolta2.Checked,
                                           chkIsPagamentoMai1.Checked,
                                           chkIsPagamentoMai2.Checked,
                                           txtNotaCoordinatore.Text,
                                           chkIsIspezioneSvolta.Checked,
                                           UtilityApp.ParseNullableInt(ddlSvolgimentoIspezione.SelectedValue),
                                           txtAltroSvolgimentoIspezione.Text
                                          );

            IspezioneRapportoControl.SalvaDatiIspezioneRapporto(iDIspezione);

            //if (chkfIspezioneNonSvolta2.Checked)
            //{
            //    //Bypasso la firma perchè non si è svolta la verifica
            //    UtilityVerifiche.CambiaStatoIspezione(iDIspezione, 4);
            //    UtilityVerifiche.StoricizzaStatoIspezione(iDIspezione, int.Parse(userInfo.IDUtente.ToString()));
            //    GetDatiAll(iDIspezione, iDIspezioneVisita);
            //    rblIspezioni.DataBind();
            //}
            //else
            //{
            var iDRuolo = userInfo.IDRuolo;
            if (iDRuolo == 1 || iDRuolo == 7)
            {
                UtilityVerifiche.CambiaStatoIspezione(iDIspezione, 4);
                UtilityVerifiche.StoricizzaStatoIspezione(iDIspezione, int.Parse(userInfo.IDUtente.ToString()));
                GetDatiAll(iDIspezione, iDIspezioneVisita);
                rblIspezioni.DataBind();
            }
            else if (iDRuolo == 8 || iDRuolo == 14)
            {
                if (ControllaDocumentiIspezioneInseriti(iDIspezione.ToString()))
                {
                    if (ControllaDoppiaMai())
                    {
                        if (ControllaByPassFirmaRVI())
                        {
                            //Bypasso la firma perchè Ispezione non svolta e motivo tutti diversi da doppia mai
                            UtilityVerifiche.CambiaStatoIspezione(iDIspezione, 4);
                            UtilityVerifiche.StoricizzaStatoIspezione(iDIspezione, int.Parse(userInfo.IDUtente.ToString()));
                            GetDatiAll(iDIspezione, iDIspezioneVisita);
                            rblIspezioni.DataBind();
                        }
                        else
                        {
                            QueryString qs = new QueryString();
                            qs.Add("IDIspezione", iDIspezione.ToString());
                            qs.Add("IDIspezioneVisita", iDIspezioneVisita.ToString());
                            qs.Add("codiceIspezione", lblCodiceIspezione.Text);
                            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                            string url = "ConfermaIspezioneRVI.aspx";
                            url += qsEncrypted.ToString();
                            Response.Redirect(url);
                        }
                    }
                    else
                    {
                        lblFeedbackDocumentiDoppiaMai.Visible = true;
                    }
                }
                else
                {
                    lblFeedbackDocumentiIspezioneInseriti.Visible = true;
                }
            }
            //}           
        }
    }

    protected void btnConcludiIspezioneCoordinatoreConAccertamento_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDIspezione = long.Parse(IDIspezione);
            long iDIspezioneVisita = long.Parse(IDIspezioneVisita);
            long? iDRapportoControlloTecnico = null;

            if (!string.IsNullOrEmpty(lblIDRapportoControllo.Text))
            {
                iDRapportoControlloTecnico = long.Parse(lblIDRapportoControllo.Text);
            }

            UtilityVerifiche.SaveIspezione(iDIspezione,
                                           UtilityApp.ParseNullableDatetime(lblDataIspezione.Text),
                                           txtNote.Text,
                                           UtilityApp.ParseNullableDatetime(txtDataIspezioneRipianificazione.Text),
                                           chkfIspezioneNonSvolta.Checked,
                                           chkfIspezioneNonSvolta2.Checked,
                                           chkIsPagamentoMai1.Checked,
                                           chkIsPagamentoMai2.Checked,
                                           txtNotaCoordinatore.Text,
                                           chkIsIspezioneSvolta.Checked,
                                           UtilityApp.ParseNullableInt(ddlSvolgimentoIspezione.SelectedValue),
                                           txtAltroSvolgimentoIspezione.Text
                                          );

            IspezioneRapportoControl.SalvaDatiIspezioneRapporto(iDIspezione);

            UtilityVerifiche.CambiaStatoIspezione((long)iDIspezione, 5);
            UtilityVerifiche.StoricizzaStatoIspezione((long)iDIspezione, int.Parse(userInfo.IDUtente.ToString()));
            UtilityVerifiche.SottoponiIspezioneAdAccertamento(iDRapportoControlloTecnico, 2, iDIspezione);
            UtilityVerifiche.SetQuestionarioBozzaDefinitivo((long)iDIspezione, true);

            GetDatiAll((long)iDIspezione, iDIspezioneVisita);
            rblIspezioni.DataBind();
        }
    }

    protected void btnConcludiIspezioneCoordinatoreSenzaAccertamento_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDIspezione = long.Parse(IDIspezione);
            long iDIspezioneVisita = long.Parse(IDIspezioneVisita);

            UtilityVerifiche.SaveIspezione(iDIspezione,
                                           UtilityApp.ParseNullableDatetime(lblDataIspezione.Text),
                                           txtNote.Text,
                                           UtilityApp.ParseNullableDatetime(txtDataIspezioneRipianificazione.Text),
                                           chkfIspezioneNonSvolta.Checked,
                                           chkfIspezioneNonSvolta2.Checked,
                                           chkIsPagamentoMai1.Checked,
                                           chkIsPagamentoMai2.Checked,
                                           txtNotaCoordinatore.Text,
                                           chkIsIspezioneSvolta.Checked,
                                           UtilityApp.ParseNullableInt(ddlSvolgimentoIspezione.SelectedValue),
                                           txtAltroSvolgimentoIspezione.Text
                                          );

            IspezioneRapportoControl.SalvaDatiIspezioneRapporto(iDIspezione);

            UtilityVerifiche.CambiaStatoIspezione(iDIspezione, 9);
            UtilityVerifiche.StoricizzaStatoIspezione(iDIspezione, int.Parse(userInfo.IDUtente.ToString()));
            UtilityVerifiche.SetQuestionarioBozzaDefinitivo((long)iDIspezione, true);

            GetDatiAll(iDIspezione, iDIspezioneVisita);
            rblIspezioni.DataBind();
        }
    }


    protected void btnConcludiIspezioneCoordinatoreDoppioMancatoAccesso_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDIspezione = long.Parse(IDIspezione);
            long iDIspezioneVisita = long.Parse(IDIspezioneVisita);
            long? iDRapportoControlloTecnico = null;

            if (!string.IsNullOrEmpty(lblIDRapportoControllo.Text))
            {
                iDRapportoControlloTecnico = long.Parse(lblIDRapportoControllo.Text);
            }

            UtilityVerifiche.SaveIspezione(iDIspezione,
                                           UtilityApp.ParseNullableDatetime(lblDataIspezione.Text),
                                           txtNote.Text,
                                           UtilityApp.ParseNullableDatetime(txtDataIspezioneRipianificazione.Text),
                                           chkfIspezioneNonSvolta.Checked,
                                           chkfIspezioneNonSvolta2.Checked,
                                           chkIsPagamentoMai1.Checked,
                                           chkIsPagamentoMai2.Checked,
                                           txtNotaCoordinatore.Text,
                                           chkIsIspezioneSvolta.Checked,
                                           UtilityApp.ParseNullableInt(ddlSvolgimentoIspezione.SelectedValue),
                                           txtAltroSvolgimentoIspezione.Text
                                          );

            IspezioneRapportoControl.SalvaDatiIspezioneRapporto(iDIspezione);

            UtilityVerifiche.CambiaStatoIspezione((long)iDIspezione, 10);
            UtilityVerifiche.StoricizzaStatoIspezione((long)iDIspezione, int.Parse(userInfo.IDUtente.ToString()));
            UtilityVerifiche.SetQuestionarioBozzaDefinitivo((long)iDIspezione, true);

            GetDatiAll((long)iDIspezione, iDIspezioneVisita);
            rblIspezioni.DataBind();
        }
    }

    protected void btnConcludiIspezioneCoordinatoreUtenteSconosciuto_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDIspezione = long.Parse(IDIspezione);
            long iDIspezioneVisita = long.Parse(IDIspezioneVisita);

            UtilityVerifiche.SaveIspezione(iDIspezione,
                                           UtilityApp.ParseNullableDatetime(lblDataIspezione.Text),
                                           txtNote.Text,
                                           UtilityApp.ParseNullableDatetime(txtDataIspezioneRipianificazione.Text),
                                           chkfIspezioneNonSvolta.Checked,
                                           chkfIspezioneNonSvolta2.Checked,
                                           chkIsPagamentoMai1.Checked,
                                           chkIsPagamentoMai2.Checked,
                                           txtNotaCoordinatore.Text,
                                           chkIsIspezioneSvolta.Checked,
                                           UtilityApp.ParseNullableInt(ddlSvolgimentoIspezione.SelectedValue),
                                           txtAltroSvolgimentoIspezione.Text
                                          );

            IspezioneRapportoControl.SalvaDatiIspezioneRapporto(iDIspezione);

            UtilityVerifiche.CambiaStatoIspezione(iDIspezione, 11);
            UtilityVerifiche.StoricizzaStatoIspezione(iDIspezione, int.Parse(userInfo.IDUtente.ToString()));
            UtilityVerifiche.SetQuestionarioBozzaDefinitivo((long)iDIspezione, true);

            GetDatiAll(iDIspezione, iDIspezioneVisita);
            rblIspezioni.DataBind();
        }
    }


    //protected void btnRimandaIspezioneAdIspettore_Click(object sender, EventArgs e)
    //{
    //    long iDIspezione = long.Parse(IDIspezione);
    //    long iDIspezioneVisita = long.Parse(IDIspezioneVisita);

    //    var iDRuolo = userInfo.IDRuolo;
    //    if (iDRuolo == 1 || iDRuolo == 7)
    //    {
    //        UtilityVerifiche.CambiaStatoIspezione(iDIspezione, 7);
    //        UtilityVerifiche.StoricizzaStatoIspezione(iDIspezione, int.Parse(userInfo.IDUtente.ToString()));
    //        GetDatiAll(iDIspezione, iDIspezioneVisita);
    //        rblIspezioni.DataBind();
    //    }
    //}

    protected void btnAnnullaIspezione_Click(object sender, EventArgs e)
    {
        long iDIspezione = long.Parse(IDIspezione);
        long iDIspezioneVisita = long.Parse(IDIspezioneVisita);

        var iDRuolo = userInfo.IDRuolo;
        if (iDRuolo == 1 || iDRuolo == 7)
        {
            UtilityVerifiche.CambiaStatoIspezione(iDIspezione, 8);
            UtilityVerifiche.StoricizzaStatoIspezione(iDIspezione, int.Parse(userInfo.IDUtente.ToString()));
            GetDatiAll(iDIspezione, iDIspezioneVisita);
            rblIspezioni.DataBind();
        }
    }


    protected void chkIsPagamentoMai1_CheckedChanged(object sender, EventArgs e)
    {
        LogicButtonConcludiIspezioneIspettore(chkfIspezioneNonSvolta.Checked, chkfIspezioneNonSvolta2.Checked, chkIsPagamentoMai1.Checked, chkIsPagamentoMai2.Checked);
    }

    protected void chkIsPagamentoMai2_CheckedChanged(object sender, EventArgs e)
    {
        LogicButtonConcludiIspezioneIspettore(chkfIspezioneNonSvolta.Checked, chkfIspezioneNonSvolta2.Checked, chkIsPagamentoMai1.Checked, chkIsPagamentoMai2.Checked);
    }

    protected void btnUploadFilePagamentoMai1_Click(object sender, EventArgs e)
    {
        if (UploadFilePagamentoMai1.HasFile && UploadFilePagamentoMai1.PostedFile != null)
        {
            string FilePagamentoMai1 = UploadFilePagamentoMai1.FileName;
            string PathPagamentoMai1 = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspezioni"] + "\\" + IDIspezioneVisita + "\\" + lblCodiceIspezione.Text + "\\";
            string PathAndFilePagamentoMai1 = PathPagamentoMai1 + "MAI1_" + IDIspezione + ".pdf";
            UploadFilePagamentoMai1.SaveAs(PathAndFilePagamentoMai1);

            fileManagerDocumenti.Refresh();
        }
    }

    protected void btnUploadFilePagamentoMai2_Click(object sender, EventArgs e)
    {
        if (UploadFilePagamentoMai2.HasFile && UploadFilePagamentoMai2.PostedFile != null)
        {
            string FilePagamentoMai2 = UploadFilePagamentoMai2.FileName;
            string PathPagamentoMai2 = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspezioni"] + "\\" + IDIspezioneVisita + "\\" + lblCodiceIspezione.Text + "\\";
            string PathAndFilePagamentoMai2 = PathPagamentoMai2 + "MAI2_" + IDIspezione + ".pdf";
            UploadFilePagamentoMai2.SaveAs(PathAndFilePagamentoMai2);

            fileManagerDocumenti.Refresh();
        }
    }

    protected void chkIsIspezioneSvolta_CheckedChanged(object sender, EventArgs e)
    {
        ddSvogimentoIspezione(null, chkIsIspezioneSvolta.Checked);
        ddlSvolgimentoIspezione.SelectedIndex = 0;
        VisibleHiddenAltroMotivoSvolgimentoIspezione(null, null);
    }

    protected void ddlSvolgimentoIspezione_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenAltroMotivoSvolgimentoIspezione(int.Parse(ddlSvolgimentoIspezione.SelectedValue), null);

        SetObblifgatorietaDocumentiIspezione();
    }

    protected void SetObblifgatorietaDocumentiIspezione()
    {
        long iDIspezione = long.Parse(IDIspezione);
        long iDIspezioneVisita = long.Parse(IDIspezioneVisita);

        UtilityVerifiche.SaveIspezione(iDIspezione,
                                           UtilityApp.ParseNullableDatetime(lblDataIspezione.Text),
                                           txtNote.Text,
                                           UtilityApp.ParseNullableDatetime(txtDataIspezioneRipianificazione.Text),
                                           chkfIspezioneNonSvolta.Checked,
                                           chkfIspezioneNonSvolta2.Checked,
                                           chkIsPagamentoMai1.Checked,
                                           chkIsPagamentoMai2.Checked,
                                           txtNotaCoordinatore.Text,
                                           chkIsIspezioneSvolta.Checked,
                                           UtilityApp.ParseNullableInt(ddlSvolgimentoIspezione.SelectedValue),
                                           txtAltroSvolgimentoIspezione.Text
                                          );

        UtilityVerifiche.SetObbligatorietaIspezioneDocumenti(iDIspezione);

        IspezioneDocumenti.IDIspezione = iDIspezione.ToString();
        IspezioneDocumenti.IDIspezioneVisita = iDIspezioneVisita.ToString();
        IspezioneDocumenti.CodiceIspezione = lblCodiceIspezione.Text;

        IspezioneDocumenti.DataBind();

        //GetDatiAll(iDIspezione, iDIspezioneVisita);
    }

}
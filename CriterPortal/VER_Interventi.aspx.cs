using DataLayer;
using DataUtilityCore;
using DataUtilityCore.Enum;
using DevExpress.Web;
using EncryptionQS;
using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VER_Interventi : Page
{

    protected string IDAccertamento
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

    UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);

        sm.RegisterPostBackControl(fileManagerDocumenti);
        //sm.EnablePartialRendering = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetDatiIntervento(long.Parse(IDAccertamento));
            GetDatiAccertamentoInterventoStorico(long.Parse(IDAccertamento));
            GetDatiRaccomandate(long.Parse(IDAccertamento));
        }
        FileManager();
    }

    //protected void GetSanzioniUserControll(string iDAccertamento, string iDTipoAccertamento)
    //{
    //    UCSanzione.IDAccertamento = iDAccertamento;
    //    UCSanzione.TipoPageSanzione = "2";
    //}
    public void GetDatiRaccomandate(long iDAccertamento)
    {
        UCRaccomandate.IDAccertamento = iDAccertamento.ToString();
    }

    #region DropDownList
    protected void rbTipologiaInterventoAccertamento(RadioButtonList rblTipologiaInterventoAccertamento, int? IDPresel)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_TipologiaInterventoAccertamento(IDPresel);
        rblTipologiaInterventoAccertamento.DataValueField = "IDTipologiaInterventoAccertamento";
        rblTipologiaInterventoAccertamento.DataTextField = "TipologiaInterventoAccertamento";
        rblTipologiaInterventoAccertamento.DataSource = ls;
        rblTipologiaInterventoAccertamento.DataBind();

        rblTipologiaInterventoAccertamento.SelectedIndex = 0;
    }
    #endregion

    public void GetDatiIntervento(long iDAccertamento)
    {
        using (var ctx = new CriterDataModel())
        {
            var intervento = ctx.V_VER_Accertamenti.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();
            if (intervento != null)
            {
                lblIDTipoAccertamento.Text = intervento.IDTipoAccertamento.ToString();


                VisibleHiddenTipoAccertamento(intervento.IDTipoAccertamento.ToString());

                //GetSanzioniUserControll(iDAccertamento.ToString(), intervento.IDTipoAccertamento.ToString());

                if (intervento.IDIspezione != null)
                {
                    long iDIspezioneVisita = UtilityVerifiche.GetIDIspezioneVisitaFromVerifica(long.Parse(intervento.IDIspezione.ToString()));

                    QueryString qsIspezione = new QueryString();
                    qsIspezione.Add("IDIspezione", intervento.IDIspezione.ToString());
                    qsIspezione.Add("IDIspezioneVisita", iDIspezioneVisita.ToString());
                    QueryString qsEncryptedIspezione = Encryption.EncryptQueryString(qsIspezione);

                    string urlIspezione = "VER_Ispezioni.aspx";
                    urlIspezione += qsEncryptedIspezione.ToString();
                    btnViewIspezione.NavigateUrl = urlIspezione;
                }

                lblIDRapportoControllo.Text = intervento.IDRapportoDiControlloTecnicoBase.ToString();
                lblCodiceAccertamento.Text = intervento.CodiceAccertamento;
                QueryString qsAccertamento = new QueryString();
                qsAccertamento.Add("IDAccertamento", intervento.IDAccertamento.ToString());
                QueryString qsEncryptedAccertamento = Encryption.EncryptQueryString(qsAccertamento);
                string urlAccertamento = "VER_Accertamenti.aspx";
                urlAccertamento += qsEncryptedAccertamento.ToString();
                btnViewAccertamento.NavigateUrl = urlAccertamento;
                btnViewAccertamento.Text = intervento.CodiceAccertamento;
                                
                lblDataInvioRaccomandata.Text = string.Format("{0:dd/MM/yyyy}", intervento.DataInvioRaccomandata);
                lblDataRicevimentoRaccomandata.Text = string.Format("{0:dd/MM/yyyy}", intervento.DataRicevimentoRaccomandata);
                lblDataScadenzaIntervento.Text = string.Format("{0:dd/MM/yyyy}", intervento.DataScadenzaIntervento);
                lblStatoAccertamentoIntervento.Text = intervento.StatoAccertamentoIntervento;
                lblIDStatoAccertamentoIntervento.Text = intervento.IDStatoAccertamentoIntervento.ToString();
                
                lblImpresaManutenzione.Text = intervento.CodiceSoggetto + "&nbsp;-&nbsp;" + intervento.NomeAzienda;
                lblImpresaManutenzioneIndirizzo.Text = intervento.IndirizzoAzienda;
                lblImpresaManutenzioneTelefono.Text = intervento.Telefono;
                lblImpresaManutenzioneEmail.Text = intervento.Email;
                lblImpresaManutenzioneEmailPec.Text = intervento.EmailPec;

                lblDatiImpiantoCodiceTargatura.Text = intervento.CodiceTargatura;

                QueryString qsLibretto = new QueryString();
                qsLibretto.Add("IDLibrettoImpianto", intervento.IDLibrettoImpianto.ToString());
                QueryString qsEncryptedLibretto = Encryption.EncryptQueryString(qsLibretto);
                string urlLibretto = "LIM_LibrettiImpianti.aspx";
                urlLibretto += qsEncryptedLibretto.ToString();
                btnViewLibrettoImpianto.NavigateUrl = urlLibretto;

                lblComune.Text = ctx.SYS_CodiciCatastali.Where(c => c.IDCodiceCatastale == intervento.IDCodiceCatastale).FirstOrDefault().Comune;
                txtNote.Text = intervento.NoteInterventi;

                lblIDStatoAccertamentoSanzione.Text = intervento.IDStatoAccertamentoSanzione.ToString();

                #region Rapporti di controllo
                GetRapportiControllo(intervento.IDTargaturaImpianto, intervento.Prefisso, intervento.CodiceProgressivo);
                #endregion

                #region Non conformità
                GetNonConformita(intervento.IDAccertamento);
                #endregion

                //UCGoogleAutosuggest.IDLibrettoImpianto = intervento.IDLibrettoImpianto.ToString();

                //if ((!string.IsNullOrEmpty(intervento.NomeResponsabile) && (!string.IsNullOrEmpty(intervento.CognomeResponsabile))))
                //{
                //    lblResponsabile.Text = intervento.NomeResponsabile + "&nbsp;-&nbsp;" + intervento.CognomeResponsabile;
                //}
                //else
                //{
                //    lblResponsabile.Text = intervento.RagioneSocialeResponsabile;
                //}

                UCChangeResponsabileLibretto.IDLibrettoImpianto = intervento.IDLibrettoImpianto.ToString();
                UCChangeResponsabileLibretto.IDTargaturaImpianto = intervento.IDTargaturaImpianto.ToString();
                UCChangeResponsabileLibretto.IDAccertamento = intervento.IDAccertamento.ToString();

                VisibleHiddenButtonDataRicevimentoRaccomandata(intervento.DataRicevimentoRaccomandata);

                LogicStatiIntervento(intervento.IDAccertamento, intervento.IDStatoAccertamentoIntervento, (int)intervento.IDTipoAccertamento);
            }
        }
    }

    public void VisibleHiddenTipoAccertamento(string iDTipoAccertamento)
    {
        switch (iDTipoAccertamento)
        {
            case "1":
                rowTitleDatiIspezione.Visible = false;
                rowDatiIspezione.Visible = false;
                break;
            case "2":
                rowTitleDatiIspezione.Visible = true;
                rowDatiIspezione.Visible = true;
                break;
        }
    }

    public void GetDatiAccertamentoInterventoStorico(long iDAccertamento)
    {
        var accertamentoStorico = UtilityVerifiche.GetAccertamentoStorico(iDAccertamento, false);
        if (accertamentoStorico != null)
        {
            DataGrid.DataSource = accertamentoStorico;
            DataGrid.DataBind();
        }
    }

    public void LogicStatiIntervento(long iDAccertamento, int? iDStatoAccertamentoIntervento, int iDTipoAccertamento)
    {
        switch (iDStatoAccertamentoIntervento)
        {
            case 1: //Interventi in attesa di realizzazione
                switch (iDTipoAccertamento)
                {
                    case 1:
                        btnInviaInAttesaIspezione.Visible = true;
                        break;
                    case 2:
                        btnInviaInAttesaIspezione.Visible = false;
                        break;
                }

                btnInviaAccertamentoConSanzione.Visible = false;
                
                btnRevocaIntervento.Visible = true;
                btnRevocaInterventoSenzaComunicazione.Visible = true;

                btnSaveInterventoInAttesaDiRealizzazione.Visible = false;
                break;
            case 2: //Interventi realizzati in attesa di conferma
                switch (iDTipoAccertamento)
                {
                    case 1:
                        btnInviaInAttesaIspezione.Visible = true;
                        break;
                    case 2:
                        btnInviaInAttesaIspezione.Visible = false;
                        break;
                }
                btnInviaAccertamentoConSanzione.Visible = false;

                btnRevocaIntervento.Visible = true;
                btnRevocaInterventoSenzaComunicazione.Visible = true;
                btnSaveInterventoInAttesaDiRealizzazione.Visible = true;
                break;
            case 3://Interventi parzialmente realizzati
                switch (iDTipoAccertamento)
                {
                    case 1:
                        btnInviaInAttesaIspezione.Visible = true;
                        break;
                    case 2:
                        btnInviaInAttesaIspezione.Visible = false;
                        break;
                }

                btnInviaAccertamentoConSanzione.Visible = true;

                btnRevocaIntervento.Visible = true;
                btnRevocaInterventoSenzaComunicazione.Visible = true;
                btnSaveInterventoInAttesaDiRealizzazione.Visible = false;
                break;
            case 4: //Interventi correttamente realizzati
                btnInviaInAttesaIspezione.Visible = false;
                btnInviaAccertamentoConSanzione.Visible = false;

                btnRevocaIntervento.Visible = true;
                btnRevocaInterventoSenzaComunicazione.Visible = true;
                btnSaveInterventoInAttesaDiRealizzazione.Visible = false;
                break;
            case 5: //Interventi non realizzati - Ispezione
                btnInviaInAttesaIspezione.Visible = false;
                btnInviaAccertamentoConSanzione.Visible = false;

                btnRevocaIntervento.Visible = false;
                btnRevocaInterventoSenzaComunicazione.Visible = false;
                btnSaveInterventoInAttesaDiRealizzazione.Visible = false;
                break;
            case 9: //Interventi non realizzati - In attesa di Ispezione
                btnInviaInAttesaIspezione.Visible = false;
                btnInviaAccertamentoConSanzione.Visible = false;
                
                btnRevocaIntervento.Visible = true;
                btnRevocaInterventoSenzaComunicazione.Visible = true;
                btnSaveInterventoInAttesaDiRealizzazione.Visible = false;
                //rowSavePartial.Visible = false;
                //btnSaveIntervento.Visible = false;
                break;
            case 10: //Revoca Interventi
                fileManagerDocumenti.SettingsEditing.AllowCreate = false;
                fileManagerDocumenti.SettingsEditing.AllowCopy = false;
                fileManagerDocumenti.SettingsEditing.AllowDelete = false;
                fileManagerDocumenti.SettingsEditing.AllowMove = false;
                fileManagerDocumenti.SettingsEditing.AllowRename = false;
                fileManagerDocumenti.SettingsEditing.AllowDownload = true;
                fileManagerDocumenti.SettingsUpload.ShowUploadPanel = false;

                dgNonConformita.Enabled = false;
                                
                rowSavePartial.Visible = false;
                btnSaveIntervento.Visible = false;
                txtNote.Enabled = false;
                btnInviaInAttesaIspezione.Visible = false;
                btnInviaAccertamentoConSanzione.Visible = false;
                btnRevocaIntervento.Visible = false;
                btnRevocaInterventoSenzaComunicazione.Visible = false;
                btnSaveInterventoInAttesaDiRealizzazione.Visible = false;

                UtilityApp.SiDisableAllControls(UCChangeResponsabileLibretto);
                break;
            //case 6: //Interventi non realizzati - Notifica verbale di accertamento con diffida
            //    btnInviaInAttesaIspezione.Visible = false;
            //    btnInviaNexive.Visible = false;
            //    UCSanzione.Visible = false;
            //    btnRevocaIntervento.Visible = false;
            //    break;
            case 7: //Interventi non realizzati - Notifica verbale di accertamento con sanzione
                btnSaveIntervento.Visible = false;
                btnInviaInAttesaIspezione.Visible = false;
                btnInviaAccertamentoConSanzione.Visible = false;
                btnRevocaIntervento.Visible = false;
                btnRevocaInterventoSenzaComunicazione.Visible = false;
                btnSaveInterventoInAttesaDiRealizzazione.Visible = false;

                dgNonConformita.Enabled = false;
                rowSavePartial.Visible = false;
                txtNote.Enabled = false;

                UtilityApp.SiDisableAllControls(UCChangeResponsabileLibretto);
                break;
            case 8: //Interventi non realizzati - Data di scadenza oltre i termini
                switch (iDTipoAccertamento)
                {
                    case 1:
                        btnInviaInAttesaIspezione.Visible = true;
                        break;
                    case 2:
                        btnInviaInAttesaIspezione.Visible = false;
                        break;
                }

                btnInviaAccertamentoConSanzione.Visible = true;
                                              
                rowSavePartial.Visible = true;
                btnSaveIntervento.Visible = true;
                //btnInviaInAttesaIspezione.Visible = true;
                
                btnRevocaIntervento.Visible = true;
                btnRevocaInterventoSenzaComunicazione.Visible = true;
                btnSaveInterventoInAttesaDiRealizzazione.Visible = false;
                break;
        }
    }
    
    protected void FileManager()
    {
        string path = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadAccertamento"] + "\\" + lblCodiceAccertamento.Text;
        UtilityFileSystem.CreateDirectoryIfNotExists(path);

        fileManagerDocumenti.Settings.RootFolder = path;
        //fileManagerDocumenti.Settings.InitialFolder = path;
        fileManagerDocumenti.SettingsUpload.AdvancedModeSettings.TemporaryFolder = path;

        if (userInfo.IDRuolo == 1)
        {
            fileManagerDocumenti.SettingsEditing.AllowCreate = true;
            fileManagerDocumenti.SettingsEditing.AllowCopy = true;
            fileManagerDocumenti.SettingsEditing.AllowDelete = true;
            fileManagerDocumenti.SettingsEditing.AllowMove = true;
            fileManagerDocumenti.SettingsEditing.AllowRename = true;
            fileManagerDocumenti.SettingsEditing.AllowDownload = true;
        }
    }

    #region Non conformità

    protected void GetNonConformita(long iDAccertamento)
    {
        using (var ctx = new CriterDataModel())
        {
            var nonconformita = (from a in ctx.VER_AccertamentoNonConformita
                                 join l in ctx.SYS_ProceduraAccertamento on a.IDProceduraAccertamento equals l.IDProceduraAccertamento
                                 where (a.IDAccertamento == iDAccertamento && (a.fRaccomandazioneConferma == true || a.fPrescrizioneConferma == true))
                      select new
                          {
                              IDNonConformita = a.IDNonConformita,
                              IDAccertamento = a.IDAccertamento,
                              IDTipologiaInterventoAccertamento = a.IDTipologiaInterventoAccertamento,
                              fRealizzazioneIntervento = a.fRealizzazioneIntervento,
                              DataRealizzazioneIntervento = a.DataRealizzazioneIntervento,
                              Tipo = a.Tipo,
                              NonConformita = a.Tipo == "RACC" ? a.Raccomandazione : a.Prescrizione,
                              TipoNonConformita = a.Tipo == "RACC" ? "Raccomandazione" : "Prescrizione",
                              IDProceduraAccertamento = a.IDProceduraAccertamento,
                              ProceduraAccertamento = l.ProceduraAccertamento,
                              NoteIntervento = a.NoteIntervento,
                              IDUtenteIntervento = a.IDUtenteIntervento,
                              UtenteRealizzazioneIntervento = a.COM_Utenti.COM_AnagraficaSoggetti.Nome + " " + a.COM_Utenti.COM_AnagraficaSoggetti.Cognome
                      }
                      ).OrderBy(a => a.IDProceduraAccertamento).ToList();

            dgNonConformita.DataSource = nonconformita;
            dgNonConformita.DataBind();
        }
    }

    protected void dgNonConformita_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            ASPxCheckBox chkRealizzazioneIntervento = (ASPxCheckBox)e.Item.Cells[9].FindControl("chkRealizzazioneIntervento");
            RadioButtonList rblTipologiaInterventoAccertamento = ((RadioButtonList)(e.Item.Cells[9].FindControl("rblTipologiaInterventoAccertamento")));
            rbTipologiaInterventoAccertamento(rblTipologiaInterventoAccertamento, null);
            rblTipologiaInterventoAccertamento.SelectedValue = e.Item.Cells[5].Text;

            TableRow rowRealizzazioneIntervento0 = ((TableRow)(e.Item.Cells[9].FindControl("rowRealizzazioneIntervento0")));
            TableRow rowRealizzazioneIntervento1 = ((TableRow)(e.Item.Cells[9].FindControl("rowRealizzazioneIntervento1")));
            TableRow rowRealizzazioneIntervento2 = ((TableRow)(e.Item.Cells[9].FindControl("rowRealizzazioneIntervento2")));
            TableRow rowRealizzazioneIntervento3 = ((TableRow)(e.Item.Cells[9].FindControl("rowRealizzazioneIntervento3")));
            
            TextBox txtDataIntervento = ((TextBox)(e.Item.Cells[9].FindControl("txtDataIntervento")));
            TextBox txtNoteIntervento = ((TextBox)(e.Item.Cells[9].FindControl("txtNoteIntervento")));

            Label lblIDUtenteRealizzazioneIntervento = ((Label)(e.Item.Cells[9].FindControl("lblIDUtenteRealizzazioneIntervento")));

            SetVisibleRealizzazioneIntervento(chkRealizzazioneIntervento,
                                              rblTipologiaInterventoAccertamento,
                                              rowRealizzazioneIntervento0,
                                              rowRealizzazioneIntervento1,
                                              rowRealizzazioneIntervento2,
                                              rowRealizzazioneIntervento3,
                                              txtDataIntervento,
                                              txtNoteIntervento,
                                              lblIDUtenteRealizzazioneIntervento);


            ImageButton imgPdf = (ImageButton)(e.Item.Cells[10].FindControl("ImgPdf"));
            if (e.Item.Cells[11].Text == "1" || e.Item.Cells[11].Text == "2")
            {
                imgPdf.Visible = true;
                imgPdf.Attributes.Add("onclick", "var winLibretto=dhtmlwindow.open('Accertamento_" + IDAccertamento + "', 'iframe', 'VER_InterventiViewer.aspx?IDAccertamento=" + IDAccertamento + "&IDProceduraAccertamento=" + e.Item.Cells[11].Text + "&CodiceAccertamento=" + lblCodiceAccertamento.Text + "', 'Accertamento_" + IDAccertamento + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); ");
                imgPdf.Attributes.Add("onmouseout", "this.style.cursor='pointer'");
            }
            else
            {
                imgPdf.Visible = false;
            }
        }
    }

    protected void SetVisibleRealizzazioneIntervento(ASPxCheckBox chkRealizzazioneIntervento,
                                                     RadioButtonList rblTipologiaInterventoAccertamento,
                                                     TableRow rowRealizzazioneIntervento0,
                                                     TableRow rowRealizzazioneIntervento1,
                                                     TableRow rowRealizzazioneIntervento2,
                                                     TableRow rowRealizzazioneIntervento3,
                                                     TextBox txtDataIntervento,
                                                     TextBox txtNoteIntervento,
                                                     Label lblIDUtenteRealizzazioneIntervento)
    {
        if (chkRealizzazioneIntervento.Checked)
        {
            rowRealizzazioneIntervento0.Visible = true;
            rowRealizzazioneIntervento1.Visible = true;
            rowRealizzazioneIntervento2.Visible = true;
            rowRealizzazioneIntervento3.Visible = true;
        }
        else
        {
            rowRealizzazioneIntervento0.Visible = false;
            rowRealizzazioneIntervento1.Visible = false;
            rowRealizzazioneIntervento2.Visible = false;
            rowRealizzazioneIntervento3.Visible = false;

            rblTipologiaInterventoAccertamento.SelectedIndex = 0;
            txtDataIntervento.Text = string.Empty;
            txtNoteIntervento.Text = string.Empty;
            lblIDUtenteRealizzazioneIntervento.Text = string.Empty;
        }
    }


    protected void chkRealizzazioneIntervento_CheckedChanged(object sender, EventArgs e)
    {
        ASPxCheckBox chkRealizzazioneIntervento = (ASPxCheckBox)sender;
        TableCell cella = chkRealizzazioneIntervento.Parent as TableCell;
        DataGridItem item = cella.Parent.Parent.Parent.Parent as DataGridItem;

        RadioButtonList rblTipologiaInterventoAccertamento = ((RadioButtonList)(item.Cells[9].FindControl("rblTipologiaInterventoAccertamento")));
        TableRow rowRealizzazioneIntervento0 = ((TableRow)(item.Cells[9].FindControl("rowRealizzazioneIntervento0")));
        TableRow rowRealizzazioneIntervento1 = ((TableRow)(item.Cells[9].FindControl("rowRealizzazioneIntervento1")));
        TableRow rowRealizzazioneIntervento2 = ((TableRow)(item.Cells[9].FindControl("rowRealizzazioneIntervento2")));
        TableRow rowRealizzazioneIntervento3 = ((TableRow)(item.Cells[9].FindControl("rowRealizzazioneIntervento3")));
        TextBox txtDataIntervento = ((TextBox)(item.Cells[9].FindControl("txtDataIntervento")));
        TextBox txtNoteIntervento = ((TextBox)(item.Cells[9].FindControl("txtNoteIntervento")));

        Label lblIDUtenteRealizzazioneIntervento = ((Label)(item.Cells[9].FindControl("lblIDUtenteRealizzazioneIntervento")));

        SetVisibleRealizzazioneIntervento(chkRealizzazioneIntervento,
                                              rblTipologiaInterventoAccertamento,
                                              rowRealizzazioneIntervento0,
                                              rowRealizzazioneIntervento1,
                                              rowRealizzazioneIntervento2,
                                              rowRealizzazioneIntervento3,
                                              txtDataIntervento,
                                              txtNoteIntervento,
                                              lblIDUtenteRealizzazioneIntervento);
    }

    protected void SaveNonConformita()
    {
        for (int i = 0; i < dgNonConformita.Items.Count; i++)
        {
            DataGridItem item = dgNonConformita.Items[i];

            long IDNonConformita = long.Parse(item.Cells[0].Text);
            
            bool fRealizzazioneIntervento = ((ASPxCheckBox)(item.Cells[9].FindControl("chkRealizzazioneIntervento"))).Checked;
            string IDUtenteRealizzazioneIntervento = ((Label)(item.Cells[9].FindControl("lblIDUtenteRealizzazioneIntervento"))).Text;

            string iDUtente = string.Empty;
            if (fRealizzazioneIntervento)
            {
                if (!string.IsNullOrEmpty(IDUtenteRealizzazioneIntervento))
                {
                    iDUtente = IDUtenteRealizzazioneIntervento;
                }
                else
                {
                    iDUtente = userInfo.IDUtente.ToString();
                }
            }
            
            string iDTipologiaInterventoAccertamento = ((RadioButtonList)(item.Cells[9].FindControl("rblTipologiaInterventoAccertamento"))).SelectedValue;
            string noteIntervento = ((TextBox)(item.Cells[9].FindControl("txtNoteIntervento"))).Text;
            string dataRealizzazioneInterventi = ((TextBox)(item.Cells[9].FindControl("txtDataIntervento"))).Text;

            UtilityVerifiche.SaveNonConformitaIntervento(IDNonConformita,
                                                         fRealizzazioneIntervento,
                                                         UtilityApp.ParseNullableDatetime(dataRealizzazioneInterventi),
                                                         UtilityApp.ParseNullableInt(iDTipologiaInterventoAccertamento),
                                                         noteIntervento,
                                                         UtilityApp.ParseNullableInt(iDUtente)
                                                        );
        }
        long iDAccertamento = long.Parse(IDAccertamento);
        int iDStatoAccertamentoIntervento = int.Parse(lblIDStatoAccertamentoIntervento.Text);
        UtilityVerifiche.CambiaStatoInterventoFromNonConformita(iDAccertamento, iDStatoAccertamentoIntervento);
        UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, int.Parse(userInfo.IDUtente.ToString()));
    }

    #endregion
    
    protected void btnSaveIntervento_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDAccertamento = long.Parse(IDAccertamento);
            UtilityVerifiche.SaveIntervento(iDAccertamento,
                                             txtNote.Text
                                            );

            SaveNonConformita();
            GetDatiIntervento(iDAccertamento);
            GetDatiAccertamentoInterventoStorico(iDAccertamento);
        }
    }
    
    protected void btnSavePartial_Click(object sender, EventArgs e)
    {
        long iDAccertamento = long.Parse(IDAccertamento);
        UtilityVerifiche.SaveIntervento(iDAccertamento,
                                         txtNote.Text
                                        );

        SaveNonConformita();
        GetDatiIntervento(iDAccertamento);
        GetDatiAccertamentoInterventoStorico(iDAccertamento);
    }

    protected void btnInviaInAttesaIspezione_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDAccertamento = long.Parse(IDAccertamento);
            UtilityVerifiche.SaveIntervento(iDAccertamento,
                                             txtNote.Text
                                            );

            SaveNonConformita();
            UtilityVerifiche.CambiaStatoIntervento(iDAccertamento, 9);
            UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, int.Parse(userInfo.IDUtente.ToString()));
            GetDatiIntervento(iDAccertamento);
            GetDatiAccertamentoInterventoStorico(iDAccertamento);

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

    protected void btnRevocaIntervento_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string reportPath = ConfigurationManager.AppSettings["ReportPath"];
            string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
            string destinationFile = ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + lblCodiceAccertamento.Text;
            string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();
            string reportName = ConfigurationManager.AppSettings["ReportNameRevocaIntervento"];


            string IDProceduraAccertamento = string.Empty;
            if (lblIDTipoAccertamento.Text == "1")
            {
                IDProceduraAccertamento = "1";
            }
            else if (lblIDTipoAccertamento.Text == "2")
            {
                IDProceduraAccertamento = "5";
            }
            
            string urlPdf = ReportingServices.GetInterventiRevocaReport(IDAccertamento, IDProceduraAccertamento, reportName, reportUrl, reportPath, destinationFile, urlSite);
            
            UtilityPosteItaliane.SendToPosteItaliane(long.Parse(IDAccertamento), (int)EnumTypeofRaccomandata.TypeRevocaAccertamento);

            UtilityVerifiche.CambiaStatoIntervento(long.Parse(IDAccertamento), 10);
            UtilityVerifiche.StoricizzaStatoAccertamento(long.Parse(IDAccertamento), int.Parse(userInfo.IDUtente.ToString()));
            EmailNotify.SendMailRevocaAccertamento(long.Parse(IDAccertamento));
            GetDatiIntervento(long.Parse(IDAccertamento));
            GetDatiAccertamentoInterventoStorico(long.Parse(IDAccertamento));

            //Se c'è una sanzione allora la revoco
            //if (!string.IsNullOrEmpty(lblIDStatoAccertamentoSanzione.Text))
            //{
            //    UtilityVerifiche.CambiaStatoSanzione(long.Parse(IDAccertamento), 6);
            //}            
        }
    }

    protected void btnRevocaInterventoSenzaComunicazione_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string reportPath = ConfigurationManager.AppSettings["ReportPath"];
            string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
            string destinationFile = ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + lblCodiceAccertamento.Text;
            string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();
            string reportName = ConfigurationManager.AppSettings["ReportNameRevocaIntervento"];
            string urlPdf = ReportingServices.GetInterventiRevocaReport(IDAccertamento, "1", reportName, reportUrl, reportPath, destinationFile, urlSite);
                        
            UtilityVerifiche.CambiaStatoIntervento(long.Parse(IDAccertamento), 10);
            UtilityVerifiche.StoricizzaStatoAccertamento(long.Parse(IDAccertamento), int.Parse(userInfo.IDUtente.ToString()));
            GetDatiIntervento(long.Parse(IDAccertamento));
            GetDatiAccertamentoInterventoStorico(long.Parse(IDAccertamento));            
        }
    }

    protected void btnInterventoInAttesaDiRealizzazione_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDAccertamento = long.Parse(IDAccertamento);
            UtilityVerifiche.SaveIntervento(iDAccertamento,
                                             txtNote.Text
                                            );

            SaveNonConformita();
            UtilityVerifiche.CambiaStatoIntervento(iDAccertamento, 1);
            UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, int.Parse(userInfo.IDUtente.ToString()));
            GetDatiIntervento(iDAccertamento);
            GetDatiAccertamentoInterventoStorico(iDAccertamento);
        }
    }


    #region Data ricevimento raccomandata
    protected void btnUpdateDataRicevimentoRaccomandata_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDAccertamento = long.Parse(IDAccertamento);

            UtilityVerifiche.SetDataRicevimentoRaccomandataIntervento(iDAccertamento, txtDataRicevimentoRaccomandata.Text);
            UtilityVerifiche.SetDataScadenzaIntervento(iDAccertamento);
            pnlDataRicevimentoRaccomandata.Visible = false;
            GetDatiIntervento(iDAccertamento);
        }
    }

    protected void btnViewAggiungiDataRicevimentoRaccomandata_Click(object sender, EventArgs e)
    {
        pnlDataRicevimentoRaccomandata.Visible = true;
        btnViewAggiungiDataRicevimentoRaccomandata.Visible = false;
    }

    protected void btnAnnullaDataRicevimentoRaccomandata_Click(object sender, EventArgs e)
    {
        pnlDataRicevimentoRaccomandata.Visible = false;
        btnViewAggiungiDataRicevimentoRaccomandata.Visible = true;
        txtDataRicevimentoRaccomandata.Text = string.Empty;
    }

    protected void VisibleHiddenButtonDataRicevimentoRaccomandata(DateTime? dataRicevimentoRaccomandata)
    {
        if (dataRicevimentoRaccomandata != null)
        {
            btnViewAggiungiDataRicevimentoRaccomandata.Visible = false;
        }
        else
        {
            btnViewAggiungiDataRicevimentoRaccomandata.Visible = true;
        }
    }
    #endregion

    protected void btnInviaAccertamentoConSanzione_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDAccertamento = long.Parse(IDAccertamento);

            UtilityVerifiche.CambiaStatoIntervento(iDAccertamento, 7);
            UtilityVerifiche.SetCodiceSanzione(iDAccertamento);
            UtilityVerifiche.CambiaStatoSanzione(iDAccertamento, 2);

            UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
            UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, int.Parse(userInfo.IDUtente.ToString()));

            bool usaPec = bool.Parse(ConfigurationManager.AppSettings["NotificheIspezioniUsaEmailPec"]);
            EmailNotify.SendMailPerIspettore_SanzioniDaFirmare(iDAccertamento, usaPec);
            
            GetDatiIntervento(iDAccertamento);
            GetDatiAccertamentoInterventoStorico(iDAccertamento);
        }
    }

}
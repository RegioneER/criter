using DataLayer;
using DataUtilityCore;
using DataUtilityCore.Enum;
using DevExpress.Web.Data;
using DevExpress.Web;
using DocumentFormat.OpenXml.Wordprocessing;
using EncryptionQS;
using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUserControls_Sanzioni_UCAccertamentiSanzioni : System.Web.UI.UserControl
{
    public string IDAccertamento
    {
        get { return lblIDAccertamento.Text; }
        set
        {
            lblIDAccertamento.Text = value;
        }
    }

    public string TipoPageSanzione
    {
        get { return lblTipoPageSanzione.Text; }
        set
        {
            lblTipoPageSanzione.Text = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(IDAccertamento))
            {
                GetDatiSanzioni(long.Parse(IDAccertamento));
                GetDatiRaccomandate(long.Parse(IDAccertamento));
            }
        }
        FileManager();
    }

    public void GetDatiRaccomandate(long iDAccertamento)
    {
        UCRaccomandate.IDAccertamento = iDAccertamento.ToString();
    }

    protected void FileManager()
    {
        string pathAccertamento = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadSanzioni"] + @"\" + lblCodiceAccertamento.Text;
        UtilityFileSystem.CreateDirectoryIfNotExists(pathAccertamento);

        //string path = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadSanzioni"] + "\\" + lblCodiceAccertamento.Text;
        fileManagerDocumenti.Settings.RootFolder = pathAccertamento;
        //fileManagerDocumenti.Settings.InitialFolder = path;
        fileManagerDocumenti.SettingsUpload.AdvancedModeSettings.TemporaryFolder = pathAccertamento;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(Page);

        sm.RegisterPostBackControl(fileManagerDocumenti);
        //sm.EnablePartialRendering = false;
    }

    protected void GetDatiSanzioni(long iDAccertamento)
    {
        using (var ctx = new CriterDataModel())
        {
            var sanzione = ctx.V_VER_Accertamenti.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();
            if (sanzione != null)
            {
                #region Dati Generali
                if (sanzione.IDIspezione != null)
                {
                    long iDIspezioneVisita = UtilityVerifiche.GetIDIspezioneVisitaFromVerifica(long.Parse(sanzione.IDIspezione.ToString()));

                    QueryString qsIspezione = new QueryString();
                    qsIspezione.Add("IDIspezione", sanzione.IDIspezione.ToString());
                    qsIspezione.Add("IDIspezioneVisita", iDIspezioneVisita.ToString());
                    QueryString qsEncryptedIspezione = Encryption.EncryptQueryString(qsIspezione);

                    string urlIspezione = "~/VER_Ispezioni.aspx";
                    urlIspezione += qsEncryptedIspezione.ToString();
                    btnViewIspezione.NavigateUrl = urlIspezione;
                }

                QueryString qsAccertamento = new QueryString();
                qsAccertamento.Add("IDAccertamento", sanzione.IDAccertamento.ToString());
                QueryString qsEncryptedAccertamento = Encryption.EncryptQueryString(qsAccertamento);
                string urlAccertamento = "~/VER_Interventi.aspx";
                urlAccertamento += qsEncryptedAccertamento.ToString();
                btnViewAccertamento.NavigateUrl = urlAccertamento;
                btnViewAccertamento.Text = sanzione.CodiceAccertamento;


                lblDatiImpiantoCodiceTargatura.Text = sanzione.CodiceTargatura;

                QueryString qsLibretto = new QueryString();
                qsLibretto.Add("IDLibrettoImpianto", sanzione.IDLibrettoImpianto.ToString());
                QueryString qsEncryptedLibretto = Encryption.EncryptQueryString(qsLibretto);
                string urlLibretto = "~/LIM_LibrettiImpianti.aspx";
                urlLibretto += qsEncryptedLibretto.ToString();
                btnViewLibrettoImpianto.NavigateUrl = urlLibretto;

                
                var libretto = ctx.LIM_LibrettiImpianti.Where(c => c.IDLibrettoImpianto == sanzione.IDLibrettoImpianto).FirstOrDefault();
                //lblComune.Text = ctx.SYS_CodiciCatastali.Where(c => c.IDCodiceCatastale == sanzione.IDCodiceCatastale).FirstOrDefault().Comune;

                lblIndirizzoImpianto.Text = libretto.Indirizzo + " " + libretto.Civico + " " + ctx.SYS_CodiciCatastali.Where(c => c.IDCodiceCatastale == sanzione.IDCodiceCatastale).FirstOrDefault().Comune;
                #region Rapporti di controllo
                lblIDRapportoControllo.Text = sanzione.IDRapportoDiControlloTecnicoBase.ToString();
                GetRapportiControllo(sanzione.IDTargaturaImpianto, sanzione.Prefisso, sanzione.CodiceProgressivo);
                #endregion

                #region Responsabili
                //if ((!string.IsNullOrEmpty(sanzione.NomeResponsabile) && (!string.IsNullOrEmpty(sanzione.CognomeResponsabile))))
                //{
                //    lblResponsabile.Text = sanzione.NomeResponsabile + "&nbsp;-&nbsp;" + sanzione.CognomeResponsabile;
                //}
                //else
                //{
                //    lblResponsabile.Text = sanzione.RagioneSocialeResponsabile;
                //}

                UCChangeResponsabileLibretto.IDLibrettoImpianto = sanzione.IDLibrettoImpianto.ToString();
                UCChangeResponsabileLibretto.IDTargaturaImpianto = sanzione.IDTargaturaImpianto.ToString();
                UCChangeResponsabileLibretto.IDAccertamento = sanzione.IDAccertamento.ToString();
                #endregion


                #endregion

                lblCodiceAccertamento.Text = sanzione.CodiceAccertamento;
                lblCodiceAccertamentoSanzione.Text = sanzione.CodiceSanzione;
                lblStatoSanzioneAccertamento.Text = sanzione.StatoAccertamentoSanzione;

                lblDataInvioRaccomandata.Text = string.Format("{0:dd/MM/yyyy}", sanzione.DataInvioSanzione);
                lblDataRicevimentoRaccomandata.Text = string.Format("{0:dd/MM/yyyy}", sanzione.DataRicezioneSanzione);

                lblIDAccertatore.Text = sanzione.IDUtenteAccertatore.ToString();
                lblIDCoordinatore.Text = sanzione.IDUtenteCoordinatore.ToString();
                lblIDAgenteAccertatore.Text = sanzione.IDUtenteAgenteAccertatore.ToString();

                if (sanzione.DataScadenzaSanzione != null)
                {
                    lblDataScadenzaSanzione.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(sanzione.DataScadenzaSanzione.ToString()));
                }

                if (sanzione.DataScadenzaPagamentoRidottoSanzione != null)
                {
                    lblDataScadenzaPagamentoRidottoSanzione.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(sanzione.DataScadenzaPagamentoRidottoSanzione.ToString()));
                }

                if (sanzione.DataRicevutaPagamento != null)
                {
                    txtDataRicevutaPagamento.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(sanzione.DataRicevutaPagamento.ToString()));
                }
                
                txtNote.Text = sanzione.NoteSanzione;
                txtMotivoRevocaSanzione.Text = sanzione.MotivoSanzioneRevocata;
                chkRevocaSanzione.Checked = sanzione.IsSanzioneRevocata;

                WindowDocumentoSanzione.ContentUrl = "~/VER_SanzioniViewer.aspx?IDAccertamento=" + sanzione.IDAccertamento.ToString() + "&CodiceAccertamento=" + sanzione.CodiceAccertamento;

                VisibleHiddenRevocaSanzione(sanzione.IsSanzioneRevocata);
                VisibleHiddenButtonDataRicevimentoRaccomandata(sanzione.DataRicezioneSanzione);
                VisibleHiddenTipoAccertamento(sanzione.IDTipoAccertamento.ToString());
                LogicStatiSanzione(sanzione.IDAccertamento, sanzione.IDStatoAccertamentoSanzione);
            }
        }
    }

    public void VisibleHiddenTipoAccertamento(string iDTipoAccertamento)
    {
        //switch (iDTipoAccertamento)
        //{
        //    case "1":
        //        lblTitoloDatiRCTIspezione.Text = "Dati Rapporto di Controllo Tecnico";
        //        VER_Accertamenti_lblRapportoControlloTecnico.Text = "Dati Rapporto di Controllo";
        //        btnViewIspezione.Visible = false;
        //        dgRapporti.Visible = true;
        //        break;
        //    case "2":
        //        lblTitoloDatiRCTIspezione.Text = "Dati Ispezione";
        //        VER_Accertamenti_lblRapportoControlloTecnico.Text = "Dati Ispezione";
        //        dgRapporti.Visible = true;
        //        btnViewIspezione.Visible = true;
        //        break;
        //}
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

            string urlRapporto = "~/RCT_RapportoDiControlloTecnico.aspx";
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

    #region Sanzioni Note
    private CriterDataModel _CurrentDataContext;

    public CriterDataModel CurrentDataContext
    {
        get
        {
            if (_CurrentDataContext == null)
            {
                _CurrentDataContext = DataLayer.Common.ApplicationContext.Current.Context;
            }
            return _CurrentDataContext;
        }
    }

    protected bool IsDraftElementoGridPrincipale(int itemId)
    {
        return CurrentDataContext.VER_AccertamentoNote.Find(itemId).IDAccertamento == long.Parse(IDAccertamento);
    }

    protected void DetailGrid_DataBound(object sender, EventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        //if (!Enabled)
        //{
        //    EnableGridEditing(gridView, Enabled);
        //}

        //2016-07-28 espansione di tutte le righe di dettaglio
        gridView.DetailRows.ExpandAllRows();
        //2016-07-28 espansione di tutte le righe di dettaglio
    }

    protected void DetailGrid_DetailRowGetButtonVisibility(object sender, DevExpress.Web.ASPxGridViewDetailRowButtonEventArgs e)
    {
        if (IsDraftElementoGridPrincipale(Convert.ToInt32(e.KeyValue)))
            e.ButtonState = GridViewDetailRowButtonState.Hidden;
    }

    protected void grdPrincipale_RowInserting(object sender, ASPxDataInsertingEventArgs e)
    {
        e.NewValues["IDAccertamento"] = IDAccertamento;
        e.NewValues["IDUtente"] = SecurityManager.GetUserIDUtente(Page.User.Identity.Name);
        e.NewValues["IsSanzione"] = true;
        //e.NewValues["Data"] = DateTime.Now;
        //e.NewValues["Nota"] = "sasassas";
    }

    protected void grdPrincipale_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        //e.NewValues["IDUtenteUltimaModifica"] = SecurityManager.GetUserIDUtente(Page.User.Identity.Name);
        //e.NewValues["DataUltimaModifica"] = DateTime.Now;
    }

    protected void grdPrincipale_BeforePerformDataSelect(object sender, EventArgs e)
    {
        dsGridPrincipale.WhereParameters["IDAccertamento"].DefaultValue = IDAccertamento.ToString();
    }

    protected void grdPrincipale_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

    }

    protected void grdPrincipale_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete)
        {
            e.Visible = IsDraftElementoGridPrincipale(GetGridKeyValue(gridView, e.VisibleIndex));
        }
    }

    protected int GetGridKeyValue(ASPxGridView gridView, int visibleIndex)
    {
        return Convert.ToInt32(gridView.GetRowValues(visibleIndex, gridView.KeyFieldName));
    }

    protected void grdPrincipale_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (!gridView.IsNewRowEditing)
        {

        }
    }

    protected void grdPrincipale_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        //var dataEsercizioStart = int.Parse(e.OldValues["DataEsercizioStart"].ToString());
        //var dataEsercizioEnd = int.Parse(e.NewValues["DataEsercizioEnd"].ToString());

        //if (!dataEsercizioStart.Equals(dataEsercizioEnd))
        //{
        //    var idRiga = Convert.ToInt32(e.Keys[gridView.KeyFieldName]);

        //    var gridObject = CurrentDataContext.LIM_LibrettiImpiantiConsumoAcqua.Find(idRiga);


        //    CurrentDataContext.SaveChanges();
        //    gridView.DataBind();
        //}
    }

    protected void grdPrincipale_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        var idRiga = Convert.ToInt32(e.EditingKeyValue);

        e.Cancel = !IsDraftElementoGridPrincipale(idRiga);
    }

    protected void grdPrincipale_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {

    }

    #endregion

    protected void VisibleHiddenButtonDataRicevimentoRaccomandata(DateTime? dataRicevimentoRaccomandata )
    {
        if (dataRicevimentoRaccomandata!=null)
        {
            btnViewAggiungiDataRicevimentoRaccomandata.Visible = false;
        }
        else
        {
            btnViewAggiungiDataRicevimentoRaccomandata.Visible = true;
        }
    }

    public void LogicStatiSanzione(long iDAccertamento, int? iDStatoAccertamentoSanzione)
    {
        switch (iDStatoAccertamentoSanzione)
        {
            case null: //OK
                row0.Visible = false;
                row1.Visible = false;
                row2.Visible = false;
                row3.Visible = false;
                row4.Visible = false;
                row5.Visible = false;
                row6.Visible = false;
                row7.Visible = false;
                row8.Visible = false;
                row9.Visible = false;
                row10.Visible = false;
                row11.Visible = false;
                row12.Visible = false;
                row13.Visible = false;
                row14.Visible = false;

                btnInviaFascicoloSanzioneInRegione.Visible = false;
                btnInviaSanzioneRevoca.Visible = false;
                btnSalvaAccertamentoConSanzione.Visible = false;

                UCChangeResponsabileLibretto.Visible = false;
                break;
            case 2: //OK - Notifica verbale di Sanzione in attesa di firma
                row0.Visible = true;
                row1.Visible = false;
                row2.Visible = false;
                row3.Visible = false;
                row4.Visible = false;
                row5.Visible = false;
                row6.Visible = false;
                row7.Visible = false;
                row8.Visible = false;
                row9.Visible = false;
                row10.Visible = false;
                row11.Visible = false;
                row12.Visible = false;
                row13.Visible = false;
                row14.Visible = false;

                btnInviaAccertamentoConSanzione.Visible = false;
                btnInviaFascicoloSanzioneInRegione.Visible = false;
                btnInviaSanzioneRevoca.Visible = false;
                btnSalvaAccertamentoConSanzione.Visible = false;

                UCChangeResponsabileLibretto.Visible = true;
                UtilityApp.SiDisableAllControls(UCChangeResponsabileLibretto);
                break;
            case 1: //OK - Notifica verbale di Sanzione in attesa di validazione
                row0.Visible = true;
                row1.Visible = false;
                row2.Visible = false;
                row3.Visible = false;
                row4.Visible = false;
                row5.Visible = false;
                row6.Visible = false;
                row7.Visible = false;
                row8.Visible = false;
                row9.Visible = false;
                row10.Visible = false;
                row11.Visible = false;
                row12.Visible = false;
                row13.Visible = false;
                row14.Visible = false;

                btnInviaAccertamentoConSanzione.Visible = false;
                btnInviaFascicoloSanzioneInRegione.Visible = false;
                btnInviaSanzioneRevoca.Visible = false;
                btnSalvaAccertamentoConSanzione.Visible = false;

                UCChangeResponsabileLibretto.Visible = true;
                UtilityApp.SiDisableAllControls(UCChangeResponsabileLibretto);
                break;            
            case 3://OK - Notifica verbale di Sanzione inviato
            case 4://OK - Sanzione scaduta per scritti difensivi
            case 7://OK - Sanzione scaduta per pagamento in forma ridotta scaduta
                row0.Visible = true;
                row1.Visible = true;
                row2.Visible = true;
                row3.Visible = true;
                row4.Visible = true;
                row5.Visible = true;
                row6.Visible = true;
                row7.Visible = true;
                row8.Visible = true;
                row9.Visible = true;
                row10.Visible = true;
                row11.Visible = true;
                row12.Visible = true;
                row13.Visible = true;
                row14.Visible = true;

                btnSalvaAccertamentoConSanzione.Visible = true;
                btnInviaAccertamentoConSanzione.Visible = false;
                btnInviaFascicoloSanzioneInRegione.Visible = true;
                UCChangeResponsabileLibretto.Visible = true;
                //btnInviaSanzioneRevoca.Visible = true;
                break;
            case 6: //OK - Notifica verbale di Sanzione revocato
                row0.Visible = true;
                row1.Visible = true;
                row2.Visible = true;
                row3.Visible = true;
                row4.Visible = true;
                row5.Visible = true;
                row6.Visible = true;
                row7.Visible = true;
                row8.Visible = true;
                row9.Visible = true;
                row10.Visible = true;
                row11.Visible = true;
                row12.Visible = true;
                row13.Visible = true;
                row14.Visible = true;

                btnInviaAccertamentoConSanzione.Visible = false;
                btnInviaFascicoloSanzioneInRegione.Visible = false;
                btnInviaSanzioneRevoca.Visible = false;
                btnSalvaAccertamentoConSanzione.Visible = false;

                txtDataRicevutaPagamento.Enabled = false;
                txtNote.Enabled = false;
                chkRevocaSanzione.Enabled = false;
                txtMotivoRevocaSanzione.Enabled = false;

                grdPrincipale.SettingsDataSecurity.AllowInsert = grdPrincipale.SettingsDataSecurity.AllowEdit = grdPrincipale.SettingsDataSecurity.AllowDelete = false;
                UtilityApp.SiDisableAllControls(UCChangeResponsabileLibretto);
                break;
            case 5: //OK - Inviato fascicolo in Regione per sanzione
                row0.Visible = true;
                row1.Visible = true;
                row2.Visible = true;
                row3.Visible = true;
                row4.Visible = true;
                row5.Visible = true;
                row6.Visible = true;
                row7.Visible = true;
                row8.Visible = true;
                row9.Visible = true;
                row10.Visible = true;
                row11.Visible = true;
                row12.Visible = true;
                row13.Visible = true;
                row14.Visible = true;

                btnInviaAccertamentoConSanzione.Visible = false;
                btnInviaFascicoloSanzioneInRegione.Visible = false;
                btnInviaSanzioneRevoca.Visible = false;
                btnSalvaAccertamentoConSanzione.Visible = false;

                txtDataRicevutaPagamento.Enabled = false;
                txtNote.Enabled = false;
                chkRevocaSanzione.Enabled = false;
                txtMotivoRevocaSanzione.Enabled = false;

                grdPrincipale.SettingsDataSecurity.AllowInsert = grdPrincipale.SettingsDataSecurity.AllowEdit = grdPrincipale.SettingsDataSecurity.AllowDelete = false;
                UtilityApp.SiDisableAllControls(UCChangeResponsabileLibretto);
                break;       
        }
    }

    protected void btnInviaAccertamentoConSanzione_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long IDAccertamento = long.Parse(lblIDAccertamento.Text);
            
            switch (TipoPageSanzione)
            {
                case "1": //Sanzione proviene da accertamento
                    int? iDUtenteAccertatore = null;
                    if (!string.IsNullOrEmpty(lblIDAccertatore.Text))
                    {
                        iDUtenteAccertatore = int.Parse(lblIDAccertatore.Text);
                    }
                    
                    int? iDUtenteCoordinatore = null;
                    if (!string.IsNullOrEmpty(lblIDCoordinatore.Text))
                    {
                        iDUtenteCoordinatore = int.Parse(lblIDCoordinatore.Text);
                    }

                    int? iDUtenteAgenteAccertatore = null;
                    if (!string.IsNullOrEmpty(lblIDAgenteAccertatore.Text))
                    {
                        iDUtenteAgenteAccertatore = int.Parse(lblIDAgenteAccertatore.Text);
                    }

                    UtilityVerifiche.CambiaStatoAccertamento(IDAccertamento, 12, iDUtenteAccertatore, iDUtenteCoordinatore, iDUtenteAgenteAccertatore);
                    break;
                case "2": //Sanzione proviene da intervento
                    UtilityVerifiche.CambiaStatoIntervento(IDAccertamento, 7);
                    break;
            }

            UtilityVerifiche.CambiaStatoSanzione(IDAccertamento, 2);

            UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
            UtilityVerifiche.StoricizzaStatoAccertamento(IDAccertamento, int.Parse(userInfo.IDUtente.ToString()));

            GetDatiSanzioni(IDAccertamento);

            Response.Redirect(Request.RawUrl);
        }
    }

    protected void btnUpdateDataRicevimentoRaccomandata_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long IDAccertamento = long.Parse(lblIDAccertamento.Text);

            UtilityVerifiche.SetDataRicevimentoRaccomandataSanzione(IDAccertamento, txtDataRicevimentoRaccomandata.Text);
            UtilityVerifiche.SetDataScadenzaSanzione(IDAccertamento);
            UtilityVerifiche.SetDataScadenzaPagamentoRidottoSanzione(IDAccertamento);
            pnlDataRicevimentoRaccomandata.Visible = false;
            GetDatiSanzioni(IDAccertamento);
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

    protected void btnInviaFascicoloSanzioneInRegione_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long IDAccertamento = long.Parse(lblIDAccertamento.Text);

            UtilityVerifiche.SaveSanzione(IDAccertamento, UtilityApp.ParseNullableDatetime(txtDataRicevutaPagamento.Text), txtNote.Text, chkRevocaSanzione.Checked, txtMotivoRevocaSanzione.Text);
            UtilityVerifiche.CambiaStatoSanzione(IDAccertamento, 5);

            EmailNotify.SendMailUfficioSanzioniRegione(IDAccertamento, true);

            GetDatiSanzioni(IDAccertamento);
        }
    }

    protected void btnInviaSanzioneRevoca_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long IDAccertamento = long.Parse(lblIDAccertamento.Text);

            UtilityVerifiche.SaveSanzione(IDAccertamento, UtilityApp.ParseNullableDatetime(txtDataRicevutaPagamento.Text), txtNote.Text, chkRevocaSanzione.Checked, txtMotivoRevocaSanzione.Text);
            UtilityVerifiche.CambiaStatoSanzione(IDAccertamento, 6);
            UtilityPosteItaliane.SendToPosteItaliane(IDAccertamento, (int)EnumTypeofRaccomandata.TypeRevocaSanzione);

            GetDatiSanzioni(IDAccertamento);
        }
    }

    protected void btnSalvaAccertamentoConSanzione_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long IDAccertamento = long.Parse(lblIDAccertamento.Text);

            UtilityVerifiche.SaveSanzione(IDAccertamento, UtilityApp.ParseNullableDatetime(txtDataRicevutaPagamento.Text), txtNote.Text, chkRevocaSanzione.Checked, txtMotivoRevocaSanzione.Text);
            GetDatiSanzioni(IDAccertamento);
        }
    }

    protected void chkRevocaSanzione_CheckedChanged(object sender, EventArgs e)
    {
        VisibleHiddenRevocaSanzione(chkRevocaSanzione.Checked);
    }

    protected void VisibleHiddenRevocaSanzione(bool IsRevocaSanzione)
    {
        if (IsRevocaSanzione)
        {
            btnInviaSanzioneRevoca.Visible = true;
            pnlRevocaSanzione.Visible = true;
            txtMotivoRevocaSanzione.Visible = true;
        }
        else
        {
            btnInviaSanzioneRevoca.Visible = false;
            pnlRevocaSanzione.Visible = false;
            txtMotivoRevocaSanzione.Text = string.Empty;
        }
    }

}
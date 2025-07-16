using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataUtilityCore;
using DevExpress.Web;
using EncryptionQS;
using System.Configuration;
using DataLayer;

public partial class LIM_LibrettiImpianti : System.Web.UI.Page
{
    #region DataContext

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

    #endregion

    private DataLayer.LIM_LibrettiImpianti _LibrettoImpiantoCorrente;

    public DataLayer.LIM_LibrettiImpianti LibrettoImpiantoCorrente
    {
        get
        {
            if ((IDLibrettoImpianto == "") || (IDLibrettoImpianto == "0"))
                return null;

            if (_LibrettoImpiantoCorrente == null)
            {
                _LibrettoImpiantoCorrente = CurrentDataContext.LIM_LibrettiImpianti.Find(Convert.ToInt32(IDLibrettoImpianto));
            }
            return _LibrettoImpiantoCorrente;
        }
    }

    protected string IDLibrettoImpianto
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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PagePermission();
            LoadAllDropDownlist();
            //if ((IDLibrettoImpianto == "") || (IDLibrettoImpianto == "0"))
            //{
            //    //LoadAllDropDownlist();
            //    InfoPreliminary();
            //}
            //else
            //{
                InfoPostPreliminary();
                //LoadAllDropDownlist();
            if (string.IsNullOrEmpty(IDLibrettoImpianto))
            {
                Response.Redirect("LIM_LibrettiImpiantiNuovo.aspx");
            }
            else
            {
                GetDatiAll(IDLibrettoImpianto);
            }
                
            //}
            SetControlsVisibility();
            txtDataIntervento.Focus();
        }
        
        ASPxWebControl.RegisterBaseScript(Page);
        ctlCampiSolariTermici.SurfaceToCompare = GetSuperficieTotaleLordaFromPage();

        ScriptManager ScriptManager = ScriptManager.GetCurrent(this.Page);
        ScriptManager.RegisterPostBackControl(rblfTerzoResponsabile);

        LIM_LibrettiImpianti_btnViewLibrettoImpianto.Attributes.Add("onclick", "var winLibretto=dhtmlwindow.open('InfoAttestasto_" + IDLibrettoImpianto + "', 'iframe', 'LIM_LibrettiImpiantiViewer.aspx?IDLibrettoImpianto=" + IDLibrettoImpianto + "', 'Libretto_" + IDLibrettoImpianto + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); ");
    }

    #region CUSTOM CONTROL

    public void ControllaGeneratoriInseriti(Object sender, ServerValidateEventArgs e)
    {
        ASPxGridView caldaie = (ASPxGridView) ctlGruppiTermici.FindControl("grdGruppiTermici");
        int CaldaieRow = caldaie.VisibleRowCount;

        ASPxGridView macchineFrigorifere = (ASPxGridView) ctlMacchineFrigorifere.FindControl("grdPrincipale");
        int macchineFrigorifereRow = macchineFrigorifere.VisibleRowCount;

        ASPxGridView scambiatori = (ASPxGridView) ctlScambiatoriCalore.FindControl("grdPrincipale");
        int scambiatoriRow = scambiatori.VisibleRowCount;

        ASPxGridView cogeneratori = (ASPxGridView) ctlCogeneratori.FindControl("grdPrincipale");
        int cogeneratoriRow = cogeneratori.VisibleRowCount;

        if (CaldaieRow > 0 || macchineFrigorifereRow > 0 || scambiatoriRow > 0 || cogeneratoriRow > 0)
        {
            e.IsValid = true;
        }
        else
        {
            e.IsValid = false;
            cvGeneratoriInseriti.ErrorMessage = "<br/>Attenzione: inserire almeno un gruppo termico/caldaia, o una macchina frigorifera/pompa di calore, o uno scambiatore di calore, o un cogeneratore/trigeneratore";
        }
    }

    public void ControllaCf(Object sender, ServerValidateEventArgs e)
    {
        if (txtCodiceFiscaleResponsabile.Text != "")
        {
            bool fCodFis = fCodFis = CodiceFiscale.ControlloFormale(txtCodiceFiscaleResponsabile.Text);

            if (!fCodFis)
            {
                e.IsValid = false;
            }
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaDataIntervento(Object sender, ServerValidateEventArgs e)
    {
        DateTime DataIntervento;
        DateTime DataCorrente = DateTime.Now.Date;

        bool chkDate = DateTime.TryParse(txtDataIntervento.Text, out DataIntervento);

        if (chkDate && DataIntervento.Date <= DataCorrente)
        {
            e.IsValid = true;
        }
        else
        {
            e.IsValid = false;
        }
    }

    public void ControllaTipoClimatizzazione(Object sender, ServerValidateEventArgs e)
    {
        bool fClimatizzazioneAcs = chkClimatizzazioneAcs.Checked;
        bool fClimatizzazioneInvernale = chkClimatizzazioneInvernale.Checked;
        bool fClimatizzazioneEstiva = chkClimatizzazioneEstiva.Checked;
        
        if (fClimatizzazioneAcs || fClimatizzazioneInvernale || fClimatizzazioneEstiva)
        {
            e.IsValid = true;
        }
        else
        {
            e.IsValid = false;
        }
    }

    public void ControllaTipoFluidoVettore(Object sender, ServerValidateEventArgs e)
    {
        e.IsValid = false;
        foreach (ListItem item in cblTipologiaFluidoVettore.Items)
        {
            if (item.Selected == true) 
            { e.IsValid = true; }
        }
    }

    public void ControllaTipoGeneratori(Object sender, ServerValidateEventArgs e)
    {
        e.IsValid = false;
        foreach (ListItem item in cblTipologiaGeneratori.Items)
        {
            if (item.Selected == true)
            { e.IsValid = true; }
        }
    }

    public void ControllaPodPdr(Object sender, ServerValidateEventArgs e)
    {
        bool fPod = true;
        bool fPdr = true;
        string message = "";
        
        foreach (ListItem item in cblTipologiaGeneratori.Items)
        {
            if (item.Selected == true)
            {
                if (((item.Value == "3") || (item.Value == "4")) && (string.IsNullOrEmpty(txtNumeroPod.Text)))
                {
                    fPod = false;
                }
                else if (((item.Value == "1") || (item.Value == "2") || (item.Value == "7")) && (string.IsNullOrEmpty(txtNumeroPdr.Text)))
                {
                    fPdr = false;
                }
                else if ((item.Value == "5") || (item.Value == "6"))
                {

                }
            }
        }
        
        //if (fPdr)
        //{
        //Ulteriore controllo nel caso del Pdr in cui è obbligatorio solo se su almeno una delle schede 4.1 sia selezionato come combustibile "gas naturale"
        //using (var ctx = new CriterDataModel())
        //{
        //    var gt = ctx.LIM_LibrettiImpiantiGruppiTermici.Where(c => c.IDLibrettoImpianto == LibrettoImpiantoCorrente.IDLibrettoImpianto && c.IDTipologiaCombustibile == 2).ToList();
        //    if (gt.Count == 0)
        //    {
        //        fPdr = false;
        //    }
        //}
        //
        //}

        if (fPod && fPdr)
        {
            e.IsValid = true;
        }
        else
        {
            e.IsValid = false;
            if (!fPod)
            {
                message += "<br/>Numero POD: campo obbligatorio in presenza di generatori Pompa di calore e Macchine Frigorifere";
            }
            if (!fPdr)
            {
                message += "<br/>Numero PDR: campo obbligatorio in presenza di generatori Generatore a combustione, Cogenerazione / trigenerazione";
            }

            cvPodPdr.ErrorMessage = message;
        }
    }

    #endregion

    protected void SetControlsVisibility()
    {
        if (LibrettoImpiantoCorrente != null)
        {
            string[] getVal = new string[4];
            getVal = SecurityManager.GetDatiPermission();

            bool definitivo = IsLibrettoDefinitivo;
            bool revisionato = IsLibrettoRevisionato;
            bool annullato = IsLibrettoAnnullato;
            bool bozza = IsLibrettoBozza;

            bool readOnly = (definitivo || annullato);
            //bool write = (bozza || revisionato);

            DisableControlsEditing(Page, readOnly);
            //tblInfoGenerali.Enabled = !readOnly;
            //tblInfoLibrettoImpianto.Enabled = !readOnly;
            if (readOnly)
            {
                UtilityApp.DisableAllControls(this.Page);
            }
            
            LIM_LibrettiImpianti_btnSaveLibrettoImpianto.Visible = !readOnly;
            LIM_LibrettiImpianti_btnCloseLibrettoImpianto.Visible = !readOnly;

            if (getVal[1] != "13" && getVal[1] != "8" && getVal[1] != "16" && (LibrettoImpiantoCorrente.fAttivo))
            {
                if (definitivo)
                {
                    LIM_LibrettiImpianti_btnRevisioneLibrettoImpianto.Visible = true;
                }
                else if (annullato)
                {
                    LIM_LibrettiImpianti_btnRevisioneLibrettoImpianto.Visible = false;
                }
            }
            else
            {
                LIM_LibrettiImpianti_btnRevisioneLibrettoImpianto.Visible = false;
            }

            if ((getVal[1] != "13" && getVal[1] != "8" && getVal[1] != "16" && getVal[1] != "11") && (LibrettoImpiantoCorrente.fAttivo) && definitivo)
            {
                LIM_LibrettiImpianti_btnNuovoRapportoControllo.Visible = true;
            }
            else
            {
                LIM_LibrettiImpianti_btnNuovoRapportoControllo.Visible = false;
            }

            if ((getVal[1] == "13" || getVal[1] == "1") && (LibrettoImpiantoCorrente.fAttivo) && definitivo && !annullato)
            {
                LIM_LibrettiImpianti_btnModifica.Visible = true;
            }
            else
            {
                LIM_LibrettiImpianti_btnModifica.Visible = false;
            }

            if ((getVal[1] == "1" || getVal[1] == "2" || getVal[1] == "3" || getVal[1] == "5" || getVal[1] == "7" || getVal[1] == "12" || getVal[1] == "13") && (LibrettoImpiantoCorrente.fAttivo) && definitivo && !annullato)
            {
                LIM_LibrettiImpianti_btnDismetti.Visible = true;
            }
            else
            {
                LIM_LibrettiImpianti_btnDismetti.Visible = false;
            }

            if (definitivo || annullato)
            {
                pnlInsertDatiCatastali.Visible = false;
                dgDatiCatastali.Columns[7].Visible = false;
                dgDatiCatastali.Columns[8].Visible = false;
                btnUpdComuneLibretto.Visible = false;
                
            }
            else if (bozza || revisionato)
            {
                pnlInsertDatiCatastali.Visible = true;
                dgDatiCatastali.Columns[7].Visible = true;
                dgDatiCatastali.Columns[8].Visible = true;
                if (bozza)
                {
                    btnUpdComuneLibretto.Visible = true;
                }
                else if (revisionato)
                {
                    using (CriterDataModel ctx = new CriterDataModel())
                    {
                        int iDTargaturaImpiantoInt = int.Parse(lblIDTargaturaImpianto.Text);
                        var rapporti = ctx.RCT_RapportoDiControlloTecnicoBase.Where(c => c.IDTargaturaImpianto == iDTargaturaImpiantoInt).ToList();
                        if (!rapporti.Any())
                        {
                            btnUpdComuneLibretto.Visible = true;
                        }
                        else
                        {
                            btnUpdComuneLibretto.Visible = false;
                        }
                    }
                }
            }
            
            if ((getVal[1] == "1" || getVal[1] == "7") && (LibrettoImpiantoCorrente.fAttivo) && readOnly)
            {
                if (!IsLibrettoAnnullato)
                {
                    LIM_LibrettiImpianti_btnAnnullaLibrettoImpianto.Visible = true;
                }
                else
                {
                    LIM_LibrettiImpianti_btnAnnullaLibrettoImpianto.Visible = false;
                    LIM_LibrettiImpianti_btnRevisioneLibrettoImpianto.Visible = false;
                }
            }
            else
            {
                LIM_LibrettiImpianti_btnAnnullaLibrettoImpianto.Visible = false;
            }

            rowNumeroRevisioneLibrettoImpianto.Visible = revisionato;
            rowDataRevisioneLibrettoImpianto.Visible = revisionato;
            //LIM_LibrettiImpianti_btnViewDefinitivoLibrettoImpianto.Visible = definitivo;
            //ctlDocTerzoResponsabile.AllowEdit = !definitivo;

            //DisableControlsEditing(tblConsumi, false);
        }
    }

    private void DisableControlsEditing(Control control, bool state)
    {
        foreach (Control c in control.Controls)
        {
            if (c is CriterUserControl)
            {
                ((CriterUserControl)c).Enabled = !state;
            }
            if (c is Button)
            {
                var button = ((Button)c);

                if (button.ID.Contains("btnSavePartial"))
                {
                    button.Visible = !state;
                }
            }
            //// Get the Enabled property by reflection.
            //Type type = c.GetType();
            //PropertyInfo prop = type.GetProperty("Enabled");

            //// Set it to False to disable the control.
            //if (prop != null)
            //{
            //    prop.SetValue(c, state, null);
            //}

            // Recurse into child controls.
            if (c.Controls.Count > 0)
            {
                this.DisableControlsEditing(c, state);
            }
        }
    }

    protected bool IsLibrettoBozza
    {
        get
        {
            if (LibrettoImpiantoCorrente == null)
                return false;

            return LibrettoImpiantoCorrente.IDStatoLibrettoImpianto == 1;
        }
    }

    protected bool IsLibrettoDefinitivo
    {
        get
        {
            if (LibrettoImpiantoCorrente == null)
                return false;

            return LibrettoImpiantoCorrente.IDStatoLibrettoImpianto == 2;
        }
    }

    protected bool IsLibrettoRevisionato
    {
        get
        {
            if (LibrettoImpiantoCorrente == null)
                return false;

            return LibrettoImpiantoCorrente.IDStatoLibrettoImpianto == 3 || LibrettoImpiantoCorrente.NumeroRevisione != null;
        }
    }

    protected bool IsLibrettoAnnullato
    {
        get
        {
            if (LibrettoImpiantoCorrente == null)
                return false;

            return LibrettoImpiantoCorrente.IDStatoLibrettoImpianto == 4;
        }
    }

    protected int? iDTargaturaImpianto
    {
        get
        {
            return LibrettoImpiantoCorrente.IDTargaturaImpianto;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ctlAccumuli.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlAltriGeneratori.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlCampiSolariTermici.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlCircuitiInterrati.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlCogeneratori.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlGruppiTermici.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlImpiantiVMC.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlMacchineFrigorifere.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlPompeCircolazione.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlRaffreddatoriLiquido.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlRecuperatoriCalore.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlScambiatoriCalore.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlScambiatoriCaloreIntermedi.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlSistemiRegolazione.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlTerzoResponsabile.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        
        ctlTorriEvaporative.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlUnitaTrattamentoAria.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlValvoleRegolazione.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlDescrizioneSistemaTelegestione.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlDescrizioneSistemaContabilizzazione.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlVasiEspansione.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        
        ctlConsumoCombustibile.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlConsumoEnergiaElettrica.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlConsumoAcqua.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlConsumoProdottiChimici.SetIDLibrettoImpianto(this.IDLibrettoImpianto);

        GetDatiVerifichePeriodiche(int.Parse(IDLibrettoImpianto), iDTargaturaImpianto);
    }

    #region GET DATI

    protected decimal GetSuperficieTotaleLordaFromPage()
    {
        decimal toReturn = 0;
        
        decimal.TryParse(txtSuperficieTotaleLordaPannelli.Text, out toReturn);

        return toReturn;
    }
        
    public void GetDatiAll(string iDLibrettoImpianto)
    {
        GetDatiCatastali(iDLibrettoImpianto);
        GetDatiLibrettoImpianto(iDLibrettoImpianto);
        GetDatiLibrettoImpiantoTipologiaFluidoVettore(int.Parse(iDLibrettoImpianto));
        GetDatiLibrettoImpiantoTipologiaGeneratori(int.Parse(iDLibrettoImpianto));
        GetDatiLibrettoImpiantoTrattamentoAcquaInvernale(int.Parse(iDLibrettoImpianto));
        GetDatiLibrettoImpiantoTrattamentoAcquaAcs(int.Parse(iDLibrettoImpianto));
        GetDatiLibrettoImpiantoTrattamentoAcquaEstiva(int.Parse(iDLibrettoImpianto));

        GetDatiLibrettoImpiantoTipologiaFiltrazioni(int.Parse(iDLibrettoImpianto));
        GetDatiLibrettoImpiantoTipologiaAddolcimentoAcqua(int.Parse(iDLibrettoImpianto));
        GetDatiLibrettoImpiantoTipologiaCondizionamentoChimico(int.Parse(iDLibrettoImpianto));
        GetDatiLibrettoImpiantoTipologiaSistemaDistribuzione(int.Parse(iDLibrettoImpianto));
        InterventoControlloEfficienzaE.IDTargaturaImpianto = iDTargaturaImpianto !=null ? int.Parse(iDTargaturaImpianto.ToString()) : 0;

        VerificheIspettiveLibretto.IDTargaturaImpianto = iDTargaturaImpianto != null ? int.Parse(iDTargaturaImpianto.ToString()) : 0;
    }

    public void GetDatiVerifichePeriodiche(int iDLibrettoImpianto, int? iDTargaturaImpianto)
    {
        if (iDTargaturaImpianto != null)
        {
            //Generatori GT
            ucCellGT.IDTargaturaImpianto = int.Parse(iDTargaturaImpianto.ToString());
            ucCellGT.prefisso = "GT";
            
            //Generatori GF
            ucCellGF.IDTargaturaImpianto = int.Parse(iDTargaturaImpianto.ToString());
            ucCellGF.prefisso = "GF";

            //Generatori SC
            ucCellSC.IDTargaturaImpianto = int.Parse(iDTargaturaImpianto.ToString());
            ucCellSC.prefisso = "SC";
            
            //Generatori CG
            ucCellCG.IDTargaturaImpianto = int.Parse(iDTargaturaImpianto.ToString());
            ucCellCG.prefisso = "CG";
        }
    }

    public void GetDatiLibrettoImpianto(string iDLibrettoImpianto)
    {
        if (LibrettoImpiantoCorrente == null)
            return;
        
        #region INFORMAZIONI GENERALI LIBRETTO IMPIANTO

        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.IDSoggettoDerived.ToString()))
        {
            lblIDSoggettoDerived.Text = LibrettoImpiantoCorrente.IDSoggettoDerived.ToString();
        }
        if (LibrettoImpiantoCorrente.IDTargaturaImpianto != null)
        {
            lblIDTargaturaImpianto.Text = LibrettoImpiantoCorrente.IDTargaturaImpianto.ToString();
            pnlSetCodiceTargatura.Visible = false;
            PnlViewCodiceTargatura.Visible = true;
            imgBarcode.ImageUrl = "~/" + ConfigurationManager.AppSettings["UploadTargatureImpianti"].ToString() + "/" + LibrettoImpiantoCorrente.LIM_TargatureImpianti.CodiceTargatura + ".png";

            string StrTargaturaImpianto = LibrettoImpiantoCorrente.IDTargaturaImpianto.ToString() + "|1";
            QueryString qs = new QueryString();
            qs.Add("StrTargaturaImpianto", StrTargaturaImpianto);
            qs.Add("TargaturaType", "StampaTargaturaA4");
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "LIM_TargatureViewer.aspx";
            url += qsEncrypted.ToString();

            imgStampaTargatura.Attributes.Add("onclick", "dhtmlwindow.open('Targatura_" + "', 'iframe', '" + url + "', 'Targatura " + "', 'width=750px,height=500px,resize=1,scrolling=1,center=1'); return false");
            lblCodiceTargatura.Text = LibrettoImpiantoCorrente.LIM_TargatureImpianti.CodiceTargatura.ToString();

            QueryString qsPopUp = new QueryString();
            qsPopUp.Add("IDLibrettoImpianto", LibrettoImpiantoCorrente.IDLibrettoImpianto.ToString());
            QueryString qsEncryptedPopUp = Encryption.EncryptQueryString(qsPopUp);

            string urlPopUp = "LIM_LibrettiImpiantiDismissione.aspx";
            urlPopUp += qsEncryptedPopUp.ToString();

            ASPxPopupControlDismissioni.ContentUrl = urlPopUp;
        }
        else
        {
            pnlSetCodiceTargatura.Visible = true;
            PnlViewCodiceTargatura.Visible = false;
            ddCodiciTargature(LibrettoImpiantoCorrente.IDTargaturaImpianto, LibrettoImpiantoCorrente.IDSoggettoDerived, null);
            cmbTargature.Value = LibrettoImpiantoCorrente.IDTargaturaImpianto.ToString();
        }

        if (LibrettoImpiantoCorrente.DataInserimento != null)
        {
            lblDataRegistrazioneLibrettoImpianto.Text = LibrettoImpiantoCorrente.DataInserimento.ToString();
        }

        if (LibrettoImpiantoCorrente.DataAnnullamento != null)
        {
            rowDataAnnullamentoLibrettoImpianto.Visible = true;
            lblDataAnnullamentoLibrettoImpianto.Text = LibrettoImpiantoCorrente.DataAnnullamento.ToString();
        }
        else
        {
            rowDataAnnullamentoLibrettoImpianto.Visible = false;
        }
        
        if (LibrettoImpiantoCorrente.NumeroRevisione != null)
        {
            lblNumeroRevisioneLibrettoImpianto.Text = LibrettoImpiantoCorrente.NumeroRevisione.ToString();
        }

        if (LibrettoImpiantoCorrente.DataRevisione != null)
        {
            lblDataRevisioneLibrettoImpianto.Text = LibrettoImpiantoCorrente.DataRevisione.ToString();
        }

        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.COM_AnagraficaSoggetti1.NomeAzienda))
        {
            lblSoggettoDerived.Text = LibrettoImpiantoCorrente.COM_AnagraficaSoggetti1.NomeAzienda;
        }
        
        if (LibrettoImpiantoCorrente.IDSoggetto != null)
        {
            lblIDSoggetto.Text = LibrettoImpiantoCorrente.IDSoggetto.ToString();
            pnlSetAddetto.Visible = false;
            lblSoggetto.Visible = true;
            lblSoggetto.Text = LibrettoImpiantoCorrente.COM_AnagraficaSoggetti.Nome + " " + LibrettoImpiantoCorrente.COM_AnagraficaSoggetti.Cognome;
        }
        else
        {
            lblIDSoggetto.Text = LibrettoImpiantoCorrente.IDSoggetto.ToString();
            pnlSetAddetto.Visible = true;
            ddAddetti(null, LibrettoImpiantoCorrente.IDSoggettoDerived, LibrettoImpiantoCorrente.IDSoggetto);
            cmbAddetti.Value = LibrettoImpiantoCorrente.IDSoggetto.ToString();
            lblSoggetto.Visible = false;
        }

        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.IDStatoLibrettoImpianto.ToString()))
        {
            lblIDStatoLibrettoImpianto.Text = LibrettoImpiantoCorrente.IDStatoLibrettoImpianto.ToString();
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.IDStatoLibrettoImpianto.ToString()))
        {
            lblStatoLibrettoImpianto.Text = LibrettoImpiantoCorrente.SYS_StatoLibrettoImpianto.StatoLibrettoImpianto;
        }
        
        #endregion

        #region 1.1 TIPOLOGIA INTERVENTO

        if (LibrettoImpiantoCorrente.DataIntervento != null)
        {
            txtDataIntervento.Text = String.Format("{0:dd/MM/yyyy}", LibrettoImpiantoCorrente.DataIntervento);
        }
        if (LibrettoImpiantoCorrente.IDTipologiaIntervento != null)
        {
            rbTipoIntervento(LibrettoImpiantoCorrente.IDTipologiaIntervento);
            rblTipoIntervento.SelectedValue = LibrettoImpiantoCorrente.IDTipologiaIntervento.ToString();
        }
        else
        {
            rbTipoIntervento(null);
        }
        #endregion

        #region 1.2 UBICAZIONE E DESTINAZIONE DELL’EDIFICIO
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.Indirizzo))
        {
            txtIndirizzo.Text = LibrettoImpiantoCorrente.Indirizzo;
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.Civico))
        {
            txtNumeroCivico.Text = LibrettoImpiantoCorrente.Civico;
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.Palazzo))
        {
            txtPalazzo.Text = LibrettoImpiantoCorrente.Palazzo;
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.Scala))
        {
            txtScala.Text = LibrettoImpiantoCorrente.Scala;
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.Interno))
        {
            txtInterno.Text = LibrettoImpiantoCorrente.Interno;
        }

        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.IDCodiceCatastale.ToString()))
        {
            lblIDCodiceCatastale.Text = LibrettoImpiantoCorrente.IDCodiceCatastale.ToString();
        }
        //VisibleCodiceCatastale(false, getVal[12]);
        //if (getVal[12] != null && !string.IsNullOrEmpty(getVal[12].ToString()))
            //UCComune.ComuneId = int.Parse(getVal[12].ToString());

        if (LibrettoImpiantoCorrente.IDCodiceCatastale != null)
        {
            lblCodiceCatastale.Text = LibrettoImpiantoCorrente.SYS_CodiciCatastali.CodiceCatastale + " - " + LibrettoImpiantoCorrente.SYS_CodiciCatastali.Comune;
        }
        if (LibrettoImpiantoCorrente.IDCodiceCatastale != null)
        {
            if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.SYS_CodiciCatastali.SYS_Province.Provincia))
            {
                lblProvincia.Text = LibrettoImpiantoCorrente.SYS_CodiciCatastali.SYS_Province.Provincia;
            }
        }
        
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.fUnitaImmobiliare.ToString()))
        {
            rblSingolaUnitaImmobiliare.SelectedValue = LibrettoImpiantoCorrente.fUnitaImmobiliare.ToString();
        }
        if (LibrettoImpiantoCorrente.IDDestinazioneUso != null)
        {
            rbDestinazioneUso(LibrettoImpiantoCorrente.IDDestinazioneUso);
            rblDestinazioneUso.SelectedValue = LibrettoImpiantoCorrente.IDDestinazioneUso.ToString();
        }
        else
        {
            rbDestinazioneUso(null);
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.VolumeLordoRiscaldato.ToString()))
        {
            txtVolumeLordoRiscaldato.Text = LibrettoImpiantoCorrente.VolumeLordoRiscaldato.ToString();
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.VolumeLordoRaffrescato.ToString()))
        {
            txtVolumeLordoRaffrescato.Text = LibrettoImpiantoCorrente.VolumeLordoRaffrescato.ToString();
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.NumeroAPE))
        {
            txtNumeroApe.Text = LibrettoImpiantoCorrente.NumeroAPE;
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.NumeroPDR))
        {
            txtNumeroPdr.Text = LibrettoImpiantoCorrente.NumeroPDR;
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.NumeroPOD))
        {
            txtNumeroPod.Text = LibrettoImpiantoCorrente.NumeroPOD;
        }
        #endregion

        #region 1.3 IMPIANTO TERMICO DESTINATO A SODDISFARE I SEGUENTI SERVIZI

        chkClimatizzazioneAcs.Checked = LibrettoImpiantoCorrente.fAcs;
        VisibleClimatizzazioneAcs(chkClimatizzazioneAcs.Checked);

        if (LibrettoImpiantoCorrente.PotenzaAcs.HasValue)
        {
            txtClimatizzazionePotenzaAcs.Text = LibrettoImpiantoCorrente.PotenzaAcs.Value.ToString();
        }

        chkClimatizzazioneInvernale.Checked = LibrettoImpiantoCorrente.fClimatizzazioneInvernale;
        VisibleClimatizzazioneInvernale(chkClimatizzazioneInvernale.Checked);

        if (LibrettoImpiantoCorrente.PotenzaClimatizzazioneInvernale.HasValue)
        {
            txtClimatizzazionePotenzaInvernale.Text = LibrettoImpiantoCorrente.PotenzaClimatizzazioneInvernale.Value.ToString();
        }
        
        chkClimatizzazioneEstiva.Checked = LibrettoImpiantoCorrente.fClimatizzazioneEstiva;
        VisibleClimatizzazioneEstiva(chkClimatizzazioneEstiva.Checked);

        if (LibrettoImpiantoCorrente.PotenzaClimatizzazioneEstiva.HasValue)
        {
            txtClimatizzazionePotenzaEstiva.Text = LibrettoImpiantoCorrente.PotenzaClimatizzazioneEstiva.Value.ToString();
        }
        
        chkClimatizzazioneAltro.Checked = LibrettoImpiantoCorrente.fClimatizzazioneAltro;
        VisibleClimatizzazioneAltro(chkClimatizzazioneAltro.Checked);

        txtClimatizzazioneAltro.Text = LibrettoImpiantoCorrente.ClimatizzazioneAltro;
        
        #endregion
        
        #region 1.5 INDIVIDUAZIONE DELLA TIPOLOGIA DEI GENERATORI
        chkPannelliSolariTermici.Checked = LibrettoImpiantoCorrente.fPannelliSolariTermici;
        chkAltroPannelli.Checked = LibrettoImpiantoCorrente.fPannelliSolariTermiciAltro;
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.PannelliSolariTermiciAltro))
        {
            txtAltroPannelli.Text = LibrettoImpiantoCorrente.PannelliSolariTermiciAltro;
        }
        VisiblePannelliSolariTermici(LibrettoImpiantoCorrente.fPannelliSolariTermici, LibrettoImpiantoCorrente.fPannelliSolariTermiciAltro);
        
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.SuperficieTotaleSolariTermici.ToString()))
        {
            txtSuperficieTotaleLordaPannelli.Text = LibrettoImpiantoCorrente.SuperficieTotaleSolariTermici.ToString();
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.PotenzaSolariTermici.ToString()))
        {
            txtPotenzaUtilePannelli.Text = LibrettoImpiantoCorrente.PotenzaSolariTermici.ToString();
        }
                
        chkClimatizzazionePannelliAcs.Checked = LibrettoImpiantoCorrente.fPannelliSolariClimatizzazioneAcs;
        chkClimatizzazionePannelliInvernale.Checked = LibrettoImpiantoCorrente.fPannelliSolariClimatizzazioneInvernale;
        chkClimatizzazionePannelliEstiva.Checked = LibrettoImpiantoCorrente.fPannelliSolariClimatizzazioneEstiva;
        
        #endregion
        
        #region 1.6 RESPONSABILE DELL’IMPIANTO
        if(LibrettoImpiantoCorrente.IDTipologiaResponsabile.HasValue)
        {
            rblTipologiaResponsabili.SelectedValue = LibrettoImpiantoCorrente.IDTipologiaResponsabile.ToString();
        }
        if (LibrettoImpiantoCorrente.IDTipoSoggetto.HasValue)
        {
            rblTipologiaSoggetti.SelectedValue = LibrettoImpiantoCorrente.IDTipoSoggetto.ToString();
        }
        txtNomeResponsabile.Text = LibrettoImpiantoCorrente.NomeResponsabile;
        txtCognomeResponsabile.Text = LibrettoImpiantoCorrente.CognomeResponsabile;
        txtCodiceFiscaleResponsabile.Text = LibrettoImpiantoCorrente.CodiceFiscaleResponsabile;
        txtRagioneSocialeResponsabile.Text = LibrettoImpiantoCorrente.RagioneSocialeResponsabile;
        txtPartitaIvaResponsabile.Text = LibrettoImpiantoCorrente.PartitaIvaResponsabile;
        if (LibrettoImpiantoCorrente.IDComuneResponsabile.HasValue)
        {
            ASPxComboBox3.Value = LibrettoImpiantoCorrente.IDComuneResponsabile.ToString();
        }
        GetIDProvinciaByIDComune(LibrettoImpiantoCorrente.IDComuneResponsabile);
        txtIndirizzoResponsabile.Text = LibrettoImpiantoCorrente.IndirizzoResponsabile;
        txtNumeroCivicoResponsabile.Text = LibrettoImpiantoCorrente.CivicoResponsabile;

        txtEmailResponsabile.Text = LibrettoImpiantoCorrente.EmailResponsabile;
        txtEmailPecResponsabile.Text = LibrettoImpiantoCorrente.EmailPecResponsabile;

        rblfTerzoResponsabile.SelectedValue = LibrettoImpiantoCorrente.fTerzoResponsabile ? "1" : "0";

        VisibleTipologiaSoggetti();
        VisibleTerzoResponsabile(LibrettoImpiantoCorrente.fTerzoResponsabile);

        #endregion

        #region 2.1
        
        txtContentutoAcquaImpiantoClimatizzazione.Text=LibrettoImpiantoCorrente.ContenutoAcquaImpianto.HasValue ? LibrettoImpiantoCorrente.ContenutoAcquaImpianto.ToString() : "";
        
        #endregion

        #region 2.2

        txtDurezzaAcquaImpiantoClimatizzazione.Text = LibrettoImpiantoCorrente.DurezzaTotaleAcquaImpianto.HasValue ? LibrettoImpiantoCorrente.DurezzaTotaleAcquaImpianto.ToString() : "";

        #endregion

        #region 2.3 TRATTAMENTO DELL’ACQUA DELL’IMPIANTO DI CLIMATIZZAZIONE INVERNALE
        rbTrattamentoAcquaInvernale.SelectedValue = LibrettoImpiantoCorrente.fTrattamentoAcquaInvernale.ToString();
        txtDurezzaAcquaInvernale.Text = LibrettoImpiantoCorrente.DurezzaTotaleAcquaImpiantoInvernale.HasValue ? LibrettoImpiantoCorrente.DurezzaTotaleAcquaImpiantoInvernale.ToString() : "";
        rbProtezioneGelo.SelectedValue = LibrettoImpiantoCorrente.fProtezioneGelo.ToString();
        ddlTipologiaProtezioneGelo.SelectedValue = LibrettoImpiantoCorrente.IDTipologiaProtezioneGelo.HasValue ? LibrettoImpiantoCorrente.IDTipologiaProtezioneGelo.Value.ToString() : string.Empty;
        txtPercentualeGlicole.Text = LibrettoImpiantoCorrente.PercentualeGlicole.HasValue ? LibrettoImpiantoCorrente.PercentualeGlicole.ToString() : "";
        txtPhGlicole.Text = LibrettoImpiantoCorrente.PhGlicole.HasValue ? LibrettoImpiantoCorrente.PhGlicole.ToString() : "";
        VisibleProtezioneGelo();

        #endregion

        #region 2.4
        rbTrattamentoAcquaAcs.SelectedValue = LibrettoImpiantoCorrente.fTrattamentoAcquaAcs.ToString();

        txtDurezzaAcquaAcs.Text = LibrettoImpiantoCorrente.DurezzaTotaleAcquaAcs.HasValue? LibrettoImpiantoCorrente.DurezzaTotaleAcquaAcs.ToString() : "";
        #endregion

        #region 2.5
        rbTrattamentoAcquaEstiva.SelectedValue = LibrettoImpiantoCorrente.fTrattamentoAcquaEstiva.ToString();
        ddlTipologiaCircuitoRaffreddamento.SelectedValue = LibrettoImpiantoCorrente.IDTipologiaCircuitoRaffreddamento.HasValue ? LibrettoImpiantoCorrente.IDTipologiaCircuitoRaffreddamento.Value.ToString() : string.Empty;
        ddlTipologiaAcquaAlimento.SelectedValue = LibrettoImpiantoCorrente.IDTipologiaAcquaAlimento.HasValue ? LibrettoImpiantoCorrente.IDTipologiaAcquaAlimento.Value.ToString() : string.Empty;
        chkSistemaSpurgoAutomatico.Checked = LibrettoImpiantoCorrente.fSistemaSpurgoAutomatico;
        //VisibleSistemaSpurgoAutomatico(LibrettoImpiantoCorrente.fSistemaSpurgoAutomatico);
        txtConducibilitaAcquaIngresso.Text = LibrettoImpiantoCorrente.ConducibilitaAcquaIngresso.HasValue ? LibrettoImpiantoCorrente.ConducibilitaAcquaIngresso.ToString() : "";
        txtConducibilitaInizioSpurgo.Text = LibrettoImpiantoCorrente.ConducibilitaInizioSpurgo.HasValue ? LibrettoImpiantoCorrente.ConducibilitaInizioSpurgo.ToString() : "";
        #endregion

        #region 5.1 REGOLAZIONE PRIMARIA
        chkSistemaRegolazioneOnOff.Checked = LibrettoImpiantoCorrente.fSistemaRegolazioneOnOff;
        chkSistemaRegolazioneIntegrato.Checked = LibrettoImpiantoCorrente.fSistemaRegolazioneIntegrato;
        chkSistemaRegolazioneIndipendente.Checked = LibrettoImpiantoCorrente.fSistemaRegolazioneIndipendente;
        chkSistemaRegolazioneMultigradino.Checked = LibrettoImpiantoCorrente.fSistemaRegolazioneMultigradino;
        chkSistemaRegolazioneAInverter.Checked = LibrettoImpiantoCorrente.fSistemaRegolazioneAInverter;
        VisibleSistemaRegolazione(LibrettoImpiantoCorrente.fSistemaRegolazioneIndipendente);
        rblValvoleRegolazione.SelectedValue = LibrettoImpiantoCorrente.fValvoleRegolazione ? "1" : "0";
        VisibleValvoleRegolazione(LibrettoImpiantoCorrente.fValvoleRegolazione);
        chkAltroSistemaRegolazionePrimaria.Checked = LibrettoImpiantoCorrente.fAltroSistemaRegolazionePrimaria;
        VisibleAltroSistemaRegolazionePrimaria(LibrettoImpiantoCorrente.fAltroSistemaRegolazionePrimaria);
        txtAltroSistemaRegolazionePrimaria.Text = LibrettoImpiantoCorrente.SistemaRegolazionePrimariaAltro;
        #endregion

        #region 5.2 REGOLAZIONE SINGOLO AMBIENTE DI ZONA
        if (LibrettoImpiantoCorrente.IDTipologiaTermostatoZona.HasValue)
        {
            rblTipologiaTermostatoZona.SelectedValue = LibrettoImpiantoCorrente.IDTipologiaTermostatoZona.Value.ToString();
        }
        chkControlloEntalpico.Checked = LibrettoImpiantoCorrente.fControlloEntalpico;
        chkControlloPortataAriaVariabile.Checked = LibrettoImpiantoCorrente.fControlloPortataAriaVariabile;

        rblValvoleTermostatiche.SelectedValue = LibrettoImpiantoCorrente.fValvoleTermostatiche ? "1" : "0";
        rblValvoleDueVie.SelectedValue = LibrettoImpiantoCorrente.fValvoleDueVie ? "1" : "0";
        rblValvoleTreVie.SelectedValue = LibrettoImpiantoCorrente.fValvoleTreVie ? "1" : "0";
        txtNoteRegolazioneSingoloAmbiente.Text = LibrettoImpiantoCorrente.NoteRegolazioneSingoloAmbiente;
        #endregion

        #region 5.3 SISTEMI TELEMATICI DI TELELETTURA E TELEGESTIONE

        rblTelelettura.SelectedValue = LibrettoImpiantoCorrente.fTelelettura ? "1" : "0";
        rblTelegestione.SelectedValue = LibrettoImpiantoCorrente.fTelegestione ? "1" : "0";

        #endregion

        #region 5.4 CONTABILIZZAZIONE

        rblUnitaImmobiliariContabilizzate.SelectedValue = LibrettoImpiantoCorrente.fContabilizzazione ? "1" : "0";
        VisibleUnitaImmobiliariContabilizzate(LibrettoImpiantoCorrente.fUnitaImmobiliare);
        chkContabilizzazioneRiscaldamento.Checked = LibrettoImpiantoCorrente.fContabilizzazioneRiscaldamento;
        chkContabilizzazioneRaffrescamento.Checked = LibrettoImpiantoCorrente.fContabilizzazioneRaffrescamento;
        chkContabilizzazioneAcquaCalda.Checked = LibrettoImpiantoCorrente.fContabilizzazioneAcquaCalda;
        if (LibrettoImpiantoCorrente.IDTipologiaSistemaContabilizzazione.HasValue)
        {
            rblIDTipologiaSistemaContabilizzazione.SelectedValue = LibrettoImpiantoCorrente.IDTipologiaSistemaContabilizzazione.ToString();
        }

        #endregion
                
        //chkCanaliAria.Checked = LibrettoImpiantoCorrente.fCanaliAria;

        if (LibrettoImpiantoCorrente.fCoibentazioneReteDistribuzione.HasValue)
        {
            rblCoibentazioneReteDistribuzione.SelectedValue = LibrettoImpiantoCorrente.fCoibentazioneReteDistribuzione.Value ? "1" : "0";
        }
        txtNoteCoibentazioneReteDistribuzione.Text = LibrettoImpiantoCorrente.NoteCoibentazioneReteDistribuzione;

        //txtVX1Capacita.Text = LibrettoImpiantoCorrente.VX1CapacitaLt.ToString();
        //if(LibrettoImpiantoCorrente.fVX1Chiuso.HasValue)
        //{
        //    rblVX1ApertoChiuso.SelectedValue = LibrettoImpiantoCorrente.fVX1Chiuso.Value ? "1" : "0";
        //    VisibleVX1Pressione(LibrettoImpiantoCorrente.fVX1Chiuso.Value);
        //}
        //else
        //{
        //    VisibleVX1Pressione(false);
        //}
        //txtVX1Pressione.Text = LibrettoImpiantoCorrente.VX1PressionePrecaricaBar.ToString();

        //txtVX2Capacita.Text = LibrettoImpiantoCorrente.VX2CapacitaLt.ToString();
        //if (LibrettoImpiantoCorrente.fVX2Chiuso.HasValue)
        //{
        //    rblVX2ApertoChiuso.SelectedValue = LibrettoImpiantoCorrente.fVX2Chiuso.Value ? "1" : "0";
        //    VisibleVX2Pressione(LibrettoImpiantoCorrente.fVX2Chiuso.Value);
        //}
        //else
        //{
        //    VisibleVX2Pressione(false);
        //}
        //txtVX2Pressione.Text = LibrettoImpiantoCorrente.VX2PressionePrecaricaBar.ToString();

        //txtVX3Capacita.Text = LibrettoImpiantoCorrente.VX3CapacitaLt.ToString();
        //if (LibrettoImpiantoCorrente.fVX3Chiuso.HasValue)
        //{
        //    rblVX3ApertoChiuso.SelectedValue = LibrettoImpiantoCorrente.fVX3Chiuso.Value ? "1" : "0";
        //    VisibleVX3Pressione(LibrettoImpiantoCorrente.fVX3Chiuso.Value);
        //}
        //else
        //{
        //    VisibleVX3Pressione(false);
        //}
        //txtVX3Pressione.Text = LibrettoImpiantoCorrente.VX3PressionePrecaricaBar.ToString();
        

        #region 7.0 SISTEMA DI EMISSIONE

        GetDatiLibrettoImpiantoTipologiaSistemiEmissione();
        
        #endregion

    }

    public void GetDatiLibrettoImpiantoTipologiaFluidoVettore(int iDLibrettoImpianto)
    {
        var result = UtilityLibrettiImpianti.GetValoriLibrettoImpiantoTipologiaFluidoVettore(iDLibrettoImpianto);

        foreach (var row in result)
        {
            cblTipologiaFluidoVettore.Items.FindByValue(row.IDTipologiaFluidoVettore.ToString()).Selected = true;
            if (row.IDTipologiaFluidoVettore == 1)
            {
                txtTipologiaFluidoVettoreAltro.Text = row.TipologiaFluidoVettoreAltro;
            }
        }

        VisibleTipologiaFluidoVettoreAltro();
    }

    public void GetDatiLibrettoImpiantoTrattamentoAcquaInvernale(int iDLibrettoImpianto)
    {
        var result = UtilityLibrettiImpianti.GetValoriLibrettoImpiantoTrattamentoAcquaInvernale(iDLibrettoImpianto);

        foreach (var row in result)
        {
            cblTipologiaTrattamentoAcquaInvernale.Items.FindByValue(row.IDTipologiaTrattamentoAcqua.ToString()).Selected = true;
        }

        VisibleTrattamentoAcquaInvernale();
    }

    public void GetDatiLibrettoImpiantoTrattamentoAcquaAcs(int iDLibrettoImpianto)
    {
        var result = UtilityLibrettiImpianti.GetValoriLibrettoImpiantoTrattamentoAcquaAcs(iDLibrettoImpianto);

        foreach (var row in result)
        {
            cblTipologiaTrattamentoAcquaAcs.Items.FindByValue(row.IDTipologiaTrattamentoAcqua.ToString()).Selected = true;
        }
        
        VisibleTrattamentoAcquaAcs(bool.Parse(rbTrattamentoAcquaAcs.SelectedItem.Value));
    }

    public void GetDatiLibrettoImpiantoTrattamentoAcquaEstiva(int iDLibrettoImpianto)
    {
        var result = UtilityLibrettiImpianti.GetValoriLibrettoImpiantoTrattamentoAcquaEstiva(iDLibrettoImpianto);

        foreach (var row in result)
        {
            cblTipologiaTrattamentoAcquaEstiva.Items.FindByValue(row.IDTipologiaTrattamentoAcqua.ToString()).Selected = true;
        }

        VisibleTrattamentoAcquaEstiva(bool.Parse(rbTrattamentoAcquaEstiva.SelectedItem.Value));
    }

    public void GetDatiLibrettoImpiantoTipologiaGeneratori(int iDLibrettoImpianto)
    {
        var result = UtilityLibrettiImpianti.GetValoriLibrettoImpiantoTipologiaGeneratori(iDLibrettoImpianto);

        foreach (var row in result)
        {
            cblTipologiaGeneratori.Items.FindByValue(row.IDTipologiaGeneratori.ToString()).Selected = true;
            if (row.IDTipologiaGeneratori == 1)
            {
                txtTipologiaGeneratoriAltro.Text = row.TipologiaGeneratoriAltro;
            }
        }

        VisibleTipologiaGeneratoriAltro();
    }

    public void GetDatiLibrettoImpiantoTipologiaFiltrazioni(int iDLibrettoImpianto)
    {
        var result = UtilityLibrettiImpianti.GetValoriLibrettoImpiantoTipologiaFiltrazioni(iDLibrettoImpianto);

        foreach (var row in result)
        {
            cblTipologiaFiltrazione.Items.FindByValue(row.IDTipologiaFiltrazione.ToString()).Selected = true;
            if (row.IDTipologiaFiltrazione == 1)
            {
                txtTipologiaFiltrazioniAltro.Text = row.TipologiaFiltrazioneAltro;
            }
        }

        VisibleTipologiaFiltrazioniAltro();
    }

    public void GetDatiLibrettoImpiantoTipologiaAddolcimentoAcqua(int iDLibrettoImpianto)
    {
        var result = UtilityLibrettiImpianti.GetValoriLibrettoImpiantoTipologiaAddolcimentoAcqua(iDLibrettoImpianto);

        foreach (var row in result)
        {
            cblTipologiaAddolcimentoAcqua.Items.FindByValue(row.IDTipologiaAddolcimentoAcqua.ToString()).Selected = true;
            if (row.IDTipologiaAddolcimentoAcqua == 1)
            {
                txtTipologiaAddolcimentoAltro.Text = row.AddolcimentoAcquaAltro;
            }
        }

        VisibleTipologiaAddolcimentoAcquaAltro();
    }

    public void GetDatiLibrettoImpiantoTipologiaCondizionamentoChimico(int iDLibrettoImpianto)
    {
        var result = UtilityLibrettiImpianti.GetValoriLibrettoImpiantoTipologiaCondizionamentoChimico(iDLibrettoImpianto);

        foreach (var row in result)
        {
            cblTipologiaCondizionamentoChimico.Items.FindByValue(row.IDTipologiaCondizionamentoChimico.ToString()).Selected = true;
            if (row.IDTipologiaCondizionamentoChimico == 1)
            {
                txtTipologiaCondizionamentoChimicoAltro.Text = row.TipologiaCondizionamentoChimicoAltro;
            }
        }

        VisibleTipologiaCondizionamentoChimicoAltro();
    }

    public void GetDatiLibrettoImpiantoTipologiaSistemaDistribuzione(int iDLibrettoImpianto)
    {
        var result = UtilityLibrettiImpianti.GetValoriLibrettoImpiantoTipologiaSistemaDistribuzione(iDLibrettoImpianto);

        foreach (var row in result)
        {
            chkTipologiaDistribuzione.Items.FindByValue(row.IDTipologiaSistemaDistribuzione.ToString()).Selected = true;
            if (row.IDTipologiaSistemaDistribuzione == 1)
            {
                txtTipologiaDistribuzioneAltro.Text = row.TipologiaSistemaDistribuzioneAltro;
            }
        }

        VisibleTipologiaSistemaDistribuzioneAltro();
    }

    #endregion

    #region SAVE DATI

    public int SaveInsertLibrettoImpianto(string iDLibrettoImpianto)
    {
        int iDLibrettoImpiantoInserted = 0;

        if ((iDLibrettoImpianto == "") || (iDLibrettoImpianto == "0"))
        {
            #region Insert
            #endregion
        }
        else
        {
            #region Update

            int? iDTargaturaImpianto = null;
            if (!string.IsNullOrEmpty(lblIDTargaturaImpianto.Text))
            {
                iDTargaturaImpianto = int.Parse(lblIDTargaturaImpianto.Text);
            }
            else if(cmbTargature.Value != null)
            {
                iDTargaturaImpianto = int.Parse(cmbTargature.Value.ToString());
            }
           
            if (iDTargaturaImpianto != null)
            {
                LibrettoImpiantoCorrente.IDTargaturaImpianto = iDTargaturaImpianto;
            }

            int? iDSoggetto = null;
            if (!string.IsNullOrEmpty(lblIDSoggetto.Text))
            {
                iDSoggetto = int.Parse(lblIDSoggetto.Text);
            }
            else if (cmbAddetti.Value != null)
            {
                iDSoggetto = int.Parse(cmbAddetti.Value.ToString());
            }

            if (iDSoggetto != null)
            {
                LibrettoImpiantoCorrente.IDSoggetto = iDSoggetto;
            }
            

            #region 1.1 TIPOLOGIA INTERVENTO

            if (string.IsNullOrEmpty(txtDataIntervento.Text))
            {
                LibrettoImpiantoCorrente.DataIntervento = null;
            }
            else
            {
                LibrettoImpiantoCorrente.DataIntervento = Convert.ToDateTime(txtDataIntervento.Text);

            }

            if (string.IsNullOrEmpty(rblTipoIntervento.SelectedValue))
            {
                LibrettoImpiantoCorrente.IDTipologiaIntervento = null;
            }
            else
            {
                LibrettoImpiantoCorrente.IDTipologiaIntervento = Convert.ToInt32(rblTipoIntervento.SelectedValue);
            }

            #endregion

            #region 1.2 UBICAZIONE E DESTINAZIONE DELL’EDIFICIO

            LibrettoImpiantoCorrente.Indirizzo = txtIndirizzo.Text;
            LibrettoImpiantoCorrente.Civico = txtNumeroCivico.Text;
            LibrettoImpiantoCorrente.Palazzo = txtPalazzo.Text;
            LibrettoImpiantoCorrente.Scala = txtScala.Text;
            LibrettoImpiantoCorrente.Interno = txtInterno.Text;
            if(string.IsNullOrEmpty(lblIDCodiceCatastale.Text))
            {
                LibrettoImpiantoCorrente.IDCodiceCatastale = null;
            }
            else
            {
                LibrettoImpiantoCorrente.IDCodiceCatastale = int.Parse(lblIDCodiceCatastale.Text);
            }

            LibrettoImpiantoCorrente.fUnitaImmobiliare = Convert.ToBoolean(rblSingolaUnitaImmobiliare.SelectedValue);

            if(string.IsNullOrEmpty(rblDestinazioneUso.SelectedValue))
            {
                LibrettoImpiantoCorrente.IDDestinazioneUso = null;
            }
            else
            {
                LibrettoImpiantoCorrente.IDDestinazioneUso = int.Parse(rblDestinazioneUso.SelectedValue);
            }
            if (string.IsNullOrEmpty(txtVolumeLordoRiscaldato.Text))
            {
                LibrettoImpiantoCorrente.VolumeLordoRiscaldato = null;
            }
            else
            {
                LibrettoImpiantoCorrente.VolumeLordoRiscaldato = Convert.ToDecimal(txtVolumeLordoRiscaldato.Text);
            }
            if (string.IsNullOrEmpty(txtVolumeLordoRaffrescato.Text))
            {
                LibrettoImpiantoCorrente.VolumeLordoRaffrescato = null;
            }
            else
            {
                LibrettoImpiantoCorrente.VolumeLordoRaffrescato = Convert.ToDecimal(txtVolumeLordoRaffrescato.Text);
            }
            if (string.IsNullOrEmpty(txtNumeroApe.Text))
            {
                LibrettoImpiantoCorrente.NumeroAPE = null;
            }
            else
            {
                LibrettoImpiantoCorrente.NumeroAPE = txtNumeroApe.Text;
            }

            if (string.IsNullOrEmpty(txtNumeroPdr.Text))
            {
                LibrettoImpiantoCorrente.NumeroPDR = null;
            }
            else
            {
                LibrettoImpiantoCorrente.NumeroPDR = txtNumeroPdr.Text;
            }

            if (string.IsNullOrEmpty(txtNumeroPod.Text))
            {
                LibrettoImpiantoCorrente.NumeroPOD = null;
            }
            else
            {
                LibrettoImpiantoCorrente.NumeroPOD = txtNumeroPod.Text;
            }

            #endregion

            #region 1.3 IMPIANTO TERMICO DESTINATO A SODDISFARE I SEGUENTI SERVIZI

            LibrettoImpiantoCorrente.fAcs = chkClimatizzazioneAcs.Checked;

            if (string.IsNullOrEmpty(txtClimatizzazionePotenzaAcs.Text))
            {
                LibrettoImpiantoCorrente.PotenzaAcs = null;
            }
            else
            {
                LibrettoImpiantoCorrente.PotenzaAcs = Convert.ToDecimal(txtClimatizzazionePotenzaAcs.Text);
            }
            LibrettoImpiantoCorrente.fClimatizzazioneInvernale = chkClimatizzazioneInvernale.Checked;
            if (string.IsNullOrEmpty(txtClimatizzazionePotenzaInvernale.Text))
            {
                LibrettoImpiantoCorrente.PotenzaClimatizzazioneInvernale = null;
            }
            else
            {
                LibrettoImpiantoCorrente.PotenzaClimatizzazioneInvernale = Convert.ToDecimal(txtClimatizzazionePotenzaInvernale.Text);
            }
            
            LibrettoImpiantoCorrente.fClimatizzazioneEstiva = chkClimatizzazioneEstiva.Checked;
            if (string.IsNullOrEmpty(txtClimatizzazionePotenzaEstiva.Text))
            {
                LibrettoImpiantoCorrente.PotenzaClimatizzazioneEstiva = null;
            }
            else
            {
                LibrettoImpiantoCorrente.PotenzaClimatizzazioneEstiva = Convert.ToDecimal(txtClimatizzazionePotenzaEstiva.Text);
            }
            
            LibrettoImpiantoCorrente.fClimatizzazioneAltro = chkClimatizzazioneAltro.Checked;
            if (string.IsNullOrEmpty(txtClimatizzazioneAltro.Text))
            {
                LibrettoImpiantoCorrente.ClimatizzazioneAltro = null;
            }
            else
            {
                LibrettoImpiantoCorrente.ClimatizzazioneAltro = txtClimatizzazioneAltro.Text;
            }
            
            #endregion
            
            #region 1.5 INDIVIDUAZIONE DELLA TIPOLOGIA DEI GENERATORI
            LibrettoImpiantoCorrente.fPannelliSolariTermici = chkPannelliSolariTermici.Checked;

            if (string.IsNullOrEmpty(txtSuperficieTotaleLordaPannelli.Text))
            {
                LibrettoImpiantoCorrente.SuperficieTotaleSolariTermici = null;
            }
            else
            {
                LibrettoImpiantoCorrente.SuperficieTotaleSolariTermici = Convert.ToDecimal(txtSuperficieTotaleLordaPannelli.Text);
            }

            if (string.IsNullOrEmpty(txtPotenzaUtilePannelli.Text))
            {
                LibrettoImpiantoCorrente.PotenzaSolariTermici = null;
            }
            else
            {
                LibrettoImpiantoCorrente.PotenzaSolariTermici = Convert.ToDecimal(txtPotenzaUtilePannelli.Text);
            }
                       
            LibrettoImpiantoCorrente.fPannelliSolariTermiciAltro = chkAltroPannelli.Checked;

            if (string.IsNullOrEmpty(txtAltroPannelli.Text))
            {
                LibrettoImpiantoCorrente.PannelliSolariTermiciAltro = null;
            }
            else
            {
                LibrettoImpiantoCorrente.PannelliSolariTermiciAltro = txtAltroPannelli.Text;
            }
            
            LibrettoImpiantoCorrente.fPannelliSolariClimatizzazioneAcs = chkClimatizzazionePannelliAcs.Checked;
            LibrettoImpiantoCorrente.fPannelliSolariClimatizzazioneInvernale = chkClimatizzazionePannelliInvernale.Checked;
            LibrettoImpiantoCorrente.fPannelliSolariClimatizzazioneEstiva = chkClimatizzazionePannelliEstiva.Checked;

            #endregion

            #region 1.6 RESPONSABILE DELL’IMPIANTO

            if (rblTipologiaResponsabili.SelectedValue == string.Empty)
            {
                LibrettoImpiantoCorrente.IDTipologiaResponsabile = null;
            }
            else
            {
                LibrettoImpiantoCorrente.IDTipologiaResponsabile = Convert.ToInt32(rblTipologiaResponsabili.SelectedValue);
            }

            if (rblTipologiaSoggetti.SelectedValue == string.Empty)
            {
                LibrettoImpiantoCorrente.IDTipoSoggetto = null;
            }
            else
            {
                LibrettoImpiantoCorrente.IDTipoSoggetto = Convert.ToInt32(rblTipologiaSoggetti.SelectedValue);
            }

            LibrettoImpiantoCorrente.NomeResponsabile = txtNomeResponsabile.Text;
            LibrettoImpiantoCorrente.CognomeResponsabile = txtCognomeResponsabile.Text;
            LibrettoImpiantoCorrente.CodiceFiscaleResponsabile = txtCodiceFiscaleResponsabile.Text;
            LibrettoImpiantoCorrente.RagioneSocialeResponsabile = txtRagioneSocialeResponsabile.Text;
            if (txtIndirizzoResponsabile.Text != string.Empty)
            {
                LibrettoImpiantoCorrente.IndirizzoResponsabile = txtIndirizzoResponsabile.Text;
            }
            else
            {
                LibrettoImpiantoCorrente.IndirizzoResponsabile = null;
            }
            if (txtNumeroCivicoResponsabile.Text != string.Empty)
            {
                LibrettoImpiantoCorrente.CivicoResponsabile = txtNumeroCivicoResponsabile.Text;
            }
            else
            {
                LibrettoImpiantoCorrente.CivicoResponsabile = null;
            }
            if (ASPxComboBox3.Value != null)
            {
                LibrettoImpiantoCorrente.IDComuneResponsabile = int.Parse(ASPxComboBox3.Value.ToString());
            }
            else
            {
                LibrettoImpiantoCorrente.IDComuneResponsabile = null;
            }
            if (lblIDProvinciaResponsabile.Text != string.Empty)
            {
                LibrettoImpiantoCorrente.IDProvinciaResponsabile = int.Parse(lblIDProvinciaResponsabile.Text);
            }
            else
            {
                LibrettoImpiantoCorrente.IDProvinciaResponsabile = null;
            }

            LibrettoImpiantoCorrente.PartitaIvaResponsabile = txtPartitaIvaResponsabile.Text;
            LibrettoImpiantoCorrente.EmailResponsabile = txtEmailResponsabile.Text;
            LibrettoImpiantoCorrente.EmailPecResponsabile = txtEmailPecResponsabile.Text;

            LibrettoImpiantoCorrente.fTerzoResponsabile = rblfTerzoResponsabile.SelectedValue == "1";

            #endregion

            #region 2.1

            if (string.IsNullOrEmpty(txtContentutoAcquaImpiantoClimatizzazione.Text))
            {
                LibrettoImpiantoCorrente.ContenutoAcquaImpianto = null;
            }
            else
            {
                LibrettoImpiantoCorrente.ContenutoAcquaImpianto = Convert.ToDecimal(txtContentutoAcquaImpiantoClimatizzazione.Text);
            }

            #endregion

            #region 2.2

            if (string.IsNullOrEmpty(txtDurezzaAcquaImpiantoClimatizzazione.Text))
            {
                LibrettoImpiantoCorrente.DurezzaTotaleAcquaImpianto = null;
            }
            else
            {
                LibrettoImpiantoCorrente.DurezzaTotaleAcquaImpianto = Convert.ToDecimal(txtDurezzaAcquaImpiantoClimatizzazione.Text);
            }

            #endregion

            #region 2.3 TRATTAMENTO DELL’ACQUA DELL’IMPIANTO DI CLIMATIZZAZIONE INVERNALE

            LibrettoImpiantoCorrente.fTrattamentoAcquaInvernale = Convert.ToBoolean(rbTrattamentoAcquaInvernale.SelectedValue);
            if (string.IsNullOrEmpty(txtDurezzaAcquaInvernale.Text))
            {
                LibrettoImpiantoCorrente.DurezzaTotaleAcquaImpiantoInvernale = null;
            }
            else
            {
                LibrettoImpiantoCorrente.DurezzaTotaleAcquaImpiantoInvernale = Convert.ToDecimal(txtDurezzaAcquaInvernale.Text);
            }

            LibrettoImpiantoCorrente.fProtezioneGelo = Convert.ToBoolean(rbProtezioneGelo.SelectedValue);

            if (string.IsNullOrEmpty(ddlTipologiaProtezioneGelo.SelectedValue))
            {
                LibrettoImpiantoCorrente.IDTipologiaProtezioneGelo = null;
            }
            else
            {
                LibrettoImpiantoCorrente.IDTipologiaProtezioneGelo = Convert.ToInt32(ddlTipologiaProtezioneGelo.SelectedValue);
            }

            if (string.IsNullOrEmpty(txtPercentualeGlicole.Text))
            {
                LibrettoImpiantoCorrente.PercentualeGlicole = null;
            }
            else
            {
                LibrettoImpiantoCorrente.PercentualeGlicole = Convert.ToDecimal(txtPercentualeGlicole.Text);
            }

            if (string.IsNullOrEmpty(txtPhGlicole.Text))
            {
                LibrettoImpiantoCorrente.PhGlicole = null;
            }
            else
            {
                LibrettoImpiantoCorrente.PhGlicole = Convert.ToDecimal(txtPhGlicole.Text);
            }

            LibrettoImpiantoCorrente.fSistemaSpurgoAutomatico = chkSistemaSpurgoAutomatico.Checked;

            if (string.IsNullOrEmpty(txtConducibilitaAcquaIngresso.Text))
            {
                LibrettoImpiantoCorrente.ConducibilitaAcquaIngresso = null;
            }
            else
            {
                LibrettoImpiantoCorrente.ConducibilitaAcquaIngresso = Convert.ToDecimal(txtConducibilitaAcquaIngresso.Text);
            }

            if (string.IsNullOrEmpty(txtConducibilitaInizioSpurgo.Text))
            {
                LibrettoImpiantoCorrente.ConducibilitaInizioSpurgo = null;
            }
            else
            {
                LibrettoImpiantoCorrente.ConducibilitaInizioSpurgo = Convert.ToDecimal(txtConducibilitaInizioSpurgo.Text);
            }

            #endregion

            #region 2.4 TRATTAMENTO DELL’ACQUA CALDA SANITARIA
            LibrettoImpiantoCorrente.fTrattamentoAcquaAcs = Convert.ToBoolean(rbTrattamentoAcquaAcs.SelectedValue);
            if (string.IsNullOrEmpty(txtDurezzaAcquaAcs.Text))
            {
                LibrettoImpiantoCorrente.DurezzaTotaleAcquaAcs = null;
            }
            else
            {
                LibrettoImpiantoCorrente.DurezzaTotaleAcquaAcs = Convert.ToDecimal(txtDurezzaAcquaAcs.Text);
            }

            #endregion

            #region 2.5
            LibrettoImpiantoCorrente.fTrattamentoAcquaEstiva = Convert.ToBoolean(rbTrattamentoAcquaEstiva.SelectedValue);
            if (ddlTipologiaCircuitoRaffreddamento.SelectedValue =="0")
            {
                LibrettoImpiantoCorrente.IDTipologiaCircuitoRaffreddamento = null;
            }
            else
            {
                LibrettoImpiantoCorrente.IDTipologiaCircuitoRaffreddamento = Convert.ToInt32(ddlTipologiaCircuitoRaffreddamento.SelectedValue);
            }

            if (ddlTipologiaAcquaAlimento.SelectedValue =="0")
            {
                LibrettoImpiantoCorrente.IDTipologiaAcquaAlimento = null;
            }
            else
            {
                LibrettoImpiantoCorrente.IDTipologiaAcquaAlimento = Convert.ToInt32(ddlTipologiaAcquaAlimento.SelectedValue);
            }
            chkSistemaSpurgoAutomatico.Checked = LibrettoImpiantoCorrente.fSistemaSpurgoAutomatico;
            if (string.IsNullOrEmpty(txtConducibilitaAcquaIngresso.Text))
            {
                LibrettoImpiantoCorrente.ConducibilitaAcquaIngresso = null;
            }
            else
            {
                LibrettoImpiantoCorrente.ConducibilitaAcquaIngresso = Convert.ToDecimal(txtConducibilitaAcquaIngresso.Text);
            }
            if (string.IsNullOrEmpty(txtConducibilitaInizioSpurgo.Text))
            {
                LibrettoImpiantoCorrente.ConducibilitaInizioSpurgo = null;
            }
            else
            {
                LibrettoImpiantoCorrente.ConducibilitaInizioSpurgo = Convert.ToDecimal(txtConducibilitaInizioSpurgo.Text);
            }
            
            #endregion

            #region 5.1 REGOLAZIONE PRIMARIA

            LibrettoImpiantoCorrente.fSistemaRegolazioneOnOff = chkSistemaRegolazioneOnOff.Checked;
            LibrettoImpiantoCorrente.fSistemaRegolazioneIntegrato = chkSistemaRegolazioneIntegrato.Checked;
            LibrettoImpiantoCorrente.fSistemaRegolazioneIndipendente = chkSistemaRegolazioneIndipendente.Checked;
            LibrettoImpiantoCorrente.fSistemaRegolazioneMultigradino = chkSistemaRegolazioneMultigradino.Checked;
            LibrettoImpiantoCorrente.fSistemaRegolazioneAInverter = chkSistemaRegolazioneAInverter.Checked;
            LibrettoImpiantoCorrente.fAltroSistemaRegolazionePrimaria = chkAltroSistemaRegolazionePrimaria.Checked;
            
            LibrettoImpiantoCorrente.fValvoleRegolazione = Convert.ToBoolean(Convert.ToInt16(rblValvoleRegolazione.SelectedItem.Value));
            
            if (LibrettoImpiantoCorrente.fAltroSistemaRegolazionePrimaria)
            {
                LibrettoImpiantoCorrente.SistemaRegolazionePrimariaAltro = txtAltroSistemaRegolazionePrimaria.Text;
            }
            else
            {
                LibrettoImpiantoCorrente.SistemaRegolazionePrimariaAltro = null;
            }
            #endregion

            #region 5.2 REGOLAZIONE SINGOLO AMBIENTE DI ZONA

            if (rblTipologiaTermostatoZona.SelectedValue == string.Empty)
            {
                LibrettoImpiantoCorrente.IDTipologiaTermostatoZona = null;
            }
            else
            {
                LibrettoImpiantoCorrente.IDTipologiaTermostatoZona = int.Parse(rblTipologiaTermostatoZona.SelectedValue);
            }

            LibrettoImpiantoCorrente.fControlloEntalpico = chkControlloEntalpico.Checked;
            LibrettoImpiantoCorrente.fControlloPortataAriaVariabile = chkControlloPortataAriaVariabile.Checked;

            LibrettoImpiantoCorrente.fValvoleTermostatiche = rblValvoleTermostatiche.SelectedValue=="1";
            LibrettoImpiantoCorrente.fValvoleDueVie = rblValvoleDueVie.SelectedValue=="1";
            LibrettoImpiantoCorrente.fValvoleTreVie = rblValvoleTreVie.SelectedValue=="1";

            LibrettoImpiantoCorrente.NoteRegolazioneSingoloAmbiente = txtNoteRegolazioneSingoloAmbiente.Text;

            #endregion

            #region 5.3 SISTEMI TELEMATICI DI TELELETTURA E TELEGESTIONE

            LibrettoImpiantoCorrente.fTelelettura = rblTelelettura.SelectedValue =="1";
            LibrettoImpiantoCorrente.fTelegestione = rblTelegestione.SelectedValue =="1";

            #endregion

            #region 5.4 CONTABILIZZAZIONE

            LibrettoImpiantoCorrente.fContabilizzazione = rblUnitaImmobiliariContabilizzate.SelectedValue =="1";
            LibrettoImpiantoCorrente.fContabilizzazioneRiscaldamento = chkContabilizzazioneRiscaldamento.Checked;
            LibrettoImpiantoCorrente.fContabilizzazioneRaffrescamento = chkContabilizzazioneRaffrescamento.Checked;
            LibrettoImpiantoCorrente.fContabilizzazioneAcquaCalda = chkContabilizzazioneAcquaCalda.Checked;

            if (rblIDTipologiaSistemaContabilizzazione.SelectedValue == string.Empty)
            {
                LibrettoImpiantoCorrente.IDTipologiaSistemaContabilizzazione = null;
            }
            else
            {
                LibrettoImpiantoCorrente.IDTipologiaSistemaContabilizzazione = int.Parse(rblIDTipologiaSistemaContabilizzazione.SelectedValue);
            }

            #endregion

            #region 6.0 SISTEMI DI DISTRIBUZIONE
            
            LibrettoImpiantoCorrente.fCoibentazioneReteDistribuzione = rblCoibentazioneReteDistribuzione.SelectedValue=="1"? true : false;
            LibrettoImpiantoCorrente.NoteCoibentazioneReteDistribuzione = txtNoteCoibentazioneReteDistribuzione.Text;

            #endregion

            #region 7.0 SISTEMA DI EMISSIONE

            var sistemiAttuali = LibrettoImpiantoCorrente.LIM_LibrettiImpiantiTipologiaSistemiEmissione.ToList();

            foreach (var sistema in sistemiAttuali)
            {
                var listItem = cblTipologiaSistemiEmissione.Items.FindByValue(sistema.IDTipologiaSistemiEmissione.ToString());
                if(listItem==null || listItem.Selected==false)
                    CurrentDataContext.LIM_LibrettiImpiantiTipologiaSistemiEmissione.Remove(sistema);
            }
            foreach (ListItem item in cblTipologiaSistemiEmissione.Items)
            {
                if (!item.Selected) continue;
                //aggiungo i tipo operazione che non erano già presenti in lista
                if (!sistemiAttuali.Any(o => o.IDTipologiaSistemiEmissione == int.Parse(item.Value)))
                {
                    LibrettoImpiantoCorrente.LIM_LibrettiImpiantiTipologiaSistemiEmissione.Add(new LIM_LibrettiImpiantiTipologiaSistemiEmissione() { IDTipologiaSistemiEmissione = int.Parse(item.Value) });
                }
            }

            LibrettoImpiantoCorrente.SistemaEmissioneAltro = txtTipologiaSistemiEmissioneAltro.Text;
            #endregion

            try
            {
                CurrentDataContext.SaveChanges();
            }
            catch (Exception ex)
            {
               
            }

            iDLibrettoImpiantoInserted = LibrettoImpiantoCorrente.IDLibrettoImpianto;

            #endregion
        }

        return iDLibrettoImpiantoInserted;
    }

    public void SaveTipologiaFluidoVettore(string iDLibrettoImpianto)
    {
        List<string> valoriTipologiaFluidoVettore = new List<string>();
        foreach (ListItem item in cblTipologiaFluidoVettore.Items)
        {
            if (item.Selected)
            {
                valoriTipologiaFluidoVettore.Add(item.Value);
            }
        }
        UtilityLibrettiImpianti.SaveInsertDeleteDatiTipologiaFluidoVettore(int.Parse(iDLibrettoImpianto), txtTipologiaFluidoVettoreAltro.Text, valoriTipologiaFluidoVettore.ToArray<string>());
    }

    public void SaveTipologiaGeneratori(string iDLibrettoImpianto)
    {
        List<string> valoriTipologiaGeneratori = new List<string>();
        foreach (ListItem item in cblTipologiaGeneratori.Items)
        {
            if (item.Selected)
            {
                valoriTipologiaGeneratori.Add(item.Value);
            }
        }
        UtilityLibrettiImpianti.SaveInsertDeleteDatiTipologiaGeneratori(int.Parse(iDLibrettoImpianto), txtTipologiaGeneratoriAltro.Text, valoriTipologiaGeneratori.ToArray<string>());
    }

    public void SaveTipologiaTrattamentoAcquaInvernale(string iDLibrettoImpianto)
    {
        List<string> valoriTrattamentoAcqua = new List<string>();
        foreach (ListItem item in cblTipologiaTrattamentoAcquaInvernale.Items)
        {
            if (item.Selected)
            {
                valoriTrattamentoAcqua.Add(item.Value);
            }
        }
        UtilityLibrettiImpianti.SaveInsertDeleteDatiTrattamentoAcquaInvernale(int.Parse(iDLibrettoImpianto), valoriTrattamentoAcqua.ToArray<string>());
    }

    public void SaveTipologiaTrattamentoAcquaAcs(string iDLibrettoImpianto)
    {
        List<string> valoriTrattamentoAcqua = new List<string>();
        foreach (ListItem item in cblTipologiaTrattamentoAcquaAcs.Items)
        {
            if (item.Selected)
            {
                valoriTrattamentoAcqua.Add(item.Value);
            }
        }
        UtilityLibrettiImpianti.SaveInsertDeleteDatiTrattamentoAcquaAcs(int.Parse(iDLibrettoImpianto), valoriTrattamentoAcqua.ToArray<string>());
    }

    public void SaveTipologiaTrattamentoAcquaEstiva(string iDLibrettoImpianto)
    {
        List<string> valoriTrattamentoAcqua = new List<string>();
        foreach (ListItem item in cblTipologiaTrattamentoAcquaEstiva.Items)
        {
            if (item.Selected)
            {
                valoriTrattamentoAcqua.Add(item.Value);
            }
        }
        UtilityLibrettiImpianti.SaveInsertDeleteDatiTrattamentoAcquaEstiva(int.Parse(iDLibrettoImpianto), valoriTrattamentoAcqua.ToArray<string>());
    }

    protected void SaveTipologiaFiltrazioni(string iDLibrettoImpianto)
    {
        List<string> valoriTipologiaFiltrazioni = new List<string>();
        foreach (ListItem item in cblTipologiaFiltrazione.Items)
        {
            if (item.Selected)
            {
                valoriTipologiaFiltrazioni.Add(item.Value);
            }
        }
        UtilityLibrettiImpianti.SaveInsertDeleteDatiTipologiaFiltrazioni(int.Parse(iDLibrettoImpianto), txtTipologiaFiltrazioniAltro.Text, valoriTipologiaFiltrazioni.ToArray<string>());
    }

    protected void SaveTipologiaAddolcimentoAcqua(string iDLibrettoImpianto)
    {
        List<string> valoriTipologiaAddolcimentoAcqua = new List<string>();
        foreach (ListItem item in cblTipologiaAddolcimentoAcqua.Items)
        {
            if (item.Selected)
            {
                valoriTipologiaAddolcimentoAcqua.Add(item.Value);
            }
        }
        UtilityLibrettiImpianti.SaveInsertDeleteDatiTipologiaAddolcimentoAcqua(int.Parse(iDLibrettoImpianto), txtTipologiaAddolcimentoAltro.Text, valoriTipologiaAddolcimentoAcqua.ToArray<string>());
    }

    protected void SaveTipologiaCondizionamentoChimico(string iDLibrettoImpianto)
    {
        List<string> valoriTipologiaCondizionamentoChimico = new List<string>();
        foreach (ListItem item in cblTipologiaCondizionamentoChimico.Items)
        {
            if (item.Selected)
            {
                valoriTipologiaCondizionamentoChimico.Add(item.Value);
            }
        }
        UtilityLibrettiImpianti.SaveInsertDeleteDatiTipologiaCondizionamentoChimico(int.Parse(iDLibrettoImpianto), txtTipologiaCondizionamentoChimicoAltro.Text, valoriTipologiaCondizionamentoChimico.ToArray<string>());
    }

    public void SaveTipologiaSistemaDistribuzione(string iDLibrettoImpianto)
    {
        List<string> valoriTipologiaSistemaDistribuzione = new List<string>();
        foreach (ListItem item in chkTipologiaDistribuzione.Items)
        {
            if (item.Selected)
            {
                valoriTipologiaSistemaDistribuzione.Add(item.Value);
            }
        }
        UtilityLibrettiImpianti.SaveInsertDeleteDatiTipologiaSistemaDistribuzione(int.Parse(iDLibrettoImpianto), txtTipologiaDistribuzioneAltro.Text, valoriTipologiaSistemaDistribuzione.ToArray<string>());
    }

    #endregion

    public void InfoPreliminary()
    {
        lblCodiceTargatura.Visible = false;
        rowSavePreliminary.Visible = true;
        tblInfoLibrettoImpianto.Visible = false;
        tblComandiLibrettoImpianto.Visible = false;
        rowStatoLibrettoImpianto.Visible = false;
        //rowDataRegistrazioneLibrettoImpianto.Visible = false;

        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                ASPxComboBox1.Visible = true;
                //ASPxComboBox2.Visible = true;
                lblSoggettoDerived.Visible = false;
                //lblSoggetto.Visible = false;
                break;
            case "2": //Amministratore azienda
                ASPxComboBox1.Value = getVal[0].ToString();
                ASPxComboBox1.Visible = false;
                //ASPxComboBox2.Visible = true;
                GetComboBoxFilterByIDAzienda();
                lblSoggettoDerived.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0].ToString());
                lblSoggettoDerived.Visible = true;
                //lblSoggetto.Visible = false;
                break;
            case "3": //Operatore/Addetto
                ASPxComboBox1.Visible = false;
                //ASPxComboBox2.Visible = false;
                lblSoggettoDerived.Visible = true;
                //lblSoggetto.Visible = true;
                ASPxComboBox1.Value = getVal[2].ToString();
                //ASPxComboBox2.Value = getVal[0].ToString();
                //lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0].ToString());
                lblSoggettoDerived.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                break;
        }

        
    }

    public void InfoPostPreliminary()
    {
        ASPxComboBox1.Visible = false;
        //ASPxComboBox2.Visible = false;
        lblSoggettoDerived.Visible = true;
        //lblSoggetto.Visible = true;
        lblCodiceTargatura.Visible = true;
        rowSavePreliminary.Visible = false;
        rowStatoLibrettoImpianto.Visible = true;
        //rowDataRegistrazioneLibrettoImpianto.Visible = true;
        tblInfoLibrettoImpianto.Visible = true;
        tblComandiLibrettoImpianto.Visible = true;
    }

    public void VisibleCompileLibrettoImpianto()
    { 
    
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                ASPxComboBox1.Visible = true;
                //ASPxComboBox2.Visible = true;
                //lblSoggetto.Visible = false;
                lblSoggettoDerived.Visible = false;
                break;
            case "2": //Amministratore azienda
                ASPxComboBox1.Value = getVal[0].ToString();
                ASPxComboBox1.Visible = false;
                //ASPxComboBox2.Visible = true;
                GetComboBoxFilterByIDAzienda();

                //lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0].ToString());
                //lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = false;
                break;
            case "3": //Operatore/Addetto
                ASPxComboBox1.Visible = false;
                //ASPxComboBox2.Visible = false;
                //lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = true;
                ASPxComboBox1.Value = getVal[2].ToString();
                //ASPxComboBox2.Value = getVal[0].ToString();
                //lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                lblSoggettoDerived.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0].ToString());
                break;
            case "10": //Responsabile tecnico
                ASPxComboBox1.Value = getVal[2].ToString();
                ASPxComboBox1.Visible = false;
                //ASPxComboBox2.Visible = true;
                GetComboBoxFilterByIDAzienda();

                //lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                //lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = false;
                break;
        }
    }
    
    #region DROPDOWNLIST / RADIOBUTTONLIST / CHECKBOXLIST
    #region RICERCA AZIENDE
    protected void ASPxComboBox_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "2", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;
        int IdSoggetto = int.Parse(e.Value.ToString());
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "2", e.Value.ToString(), "");
        comboBox.DataBind();
    }

    protected void ASPxComboBox_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetComboBoxFilterByIDAzienda();
    }

    protected void GetComboBoxFilterByIDAzienda()
    {
        if (ASPxComboBox1.Value != null)
        {
            //ASPxComboBox2.Text = "";
            //ASPxComboBox2.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", ASPxComboBox1.Value.ToString(), string.Format("%{0}%", ""), (1).ToString(), (50 + 1).ToString());
            //ASPxComboBox2.DataBind();
        }
    }

    protected void ASPxComboBox1_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBox();
    }

    protected void RefreshAspxComboBox()
    {
        ASPxComboBox1.SelectedIndex = -1;
        ASPxComboBox1.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "2", "", "", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
        ASPxComboBox1.DataBind();
        //ASPxComboBox2.SelectedIndex = -1;
        //ASPxComboBox2.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", "0", string.Format("%{0}%", ""), (1).ToString(), (50 + 1).ToString());
        //ASPxComboBox2.DataBind();
    }

    #endregion

    #region RICERCA PERSONE
    protected void ASPxComboBox2_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox2_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "1", e.Value.ToString(), "");
        comboBox.DataBind();
    }

    protected void ASPxComboBox2_OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }

    #endregion

    protected void LoadAllDropDownlist()
    {
        rbTipoIntervento(null);
        rbDestinazioneUso(null);
        ddTipologiaFluidoVettore(null);
        cbTipologiaGeneratori(null);
        rbTipologiaResponsabili(null);
        rbTipologiaSoggetti(null);
        cbTipologiaTrattamentoAcqua(null, cblTipologiaTrattamentoAcquaAcs);
        cbTipologiaTrattamentoAcqua(null, cblTipologiaTrattamentoAcquaInvernale);
        cbTipologiaTrattamentoAcqua(null, cblTipologiaTrattamentoAcquaEstiva);
        ddTipologiaProtezioneGelo(null);
        ddTipologiaCircuitoRaffreddamento(null);
        ddTipologiaAcquaAlimento(null);
        cbTipologiaFiltrazione(null);
        cbTipologiaAddolcimentoAcqua(null);
        cbTipologiaCondizionamentoChimico(null);
        cbTipologiaSistemiEmissione(null);
        cbTipologiaSistemaDistribuzione(null);
        cbTipologiaSistemaContabilizzazione(null);
        cbTipologiaTermostatoZona(null);
    }

    protected void ddCodiciTargature(object iDPresel, object iDSoggetto, object iDSoggettoDerived)
    {
        cmbTargature.Items.Clear();

        cmbTargature.ValueField = "IDTargaturaImpianto";
        cmbTargature.TextField = "CodiceTargatura";
        cmbTargature.DataSource = LoadDropDownList.LoadDropDownList_LIM_TargatureImpianti(iDPresel, iDSoggetto, iDSoggettoDerived);
        cmbTargature.DataBind();
    }

    protected void ddAddetti(object iDPresel, object iDSoggetto, object iDSoggettoDerived)
    {
        cmbAddetti.Text = "";
        cmbAddetti.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", iDSoggetto.ToString(), string.Format("%{0}%", ""), (1).ToString(), (100 + 1).ToString());
        cmbAddetti.DataBind();
    }

    protected void rbTipoIntervento(int? iDPresel)
    {
        rblTipoIntervento.Items.Clear();
        
        rblTipoIntervento.DataValueField = "IDTipologiaIntervento";
        rblTipoIntervento.DataTextField = "TipologiaIntervento";
        rblTipoIntervento.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaIntervento(iDPresel);
        rblTipoIntervento.DataBind();
        
        rblTipoIntervento.SelectedIndex = 0;
    }

    protected void rbDestinazioneUso(int? iDPresel)
    {
        rblDestinazioneUso.Items.Clear();
                
        rblDestinazioneUso.DataValueField = "IDDestinazioneUso";
        rblDestinazioneUso.DataTextField = "DestinazioneUso";
        rblDestinazioneUso.DataSource = LoadDropDownList.LoadDropDownList_SYS_DestinazioneUso(iDPresel);
        rblDestinazioneUso.DataBind();
        
        rblDestinazioneUso.SelectedIndex = 0;
    }

    protected void ddTipologiaFluidoVettore(int? idPresel)
    {
        cblTipologiaFluidoVettore.Items.Clear();

        cblTipologiaFluidoVettore.DataValueField = "IDTipologiaFluidoVettore";
        cblTipologiaFluidoVettore.DataTextField = "TipologiaFluidoVettore";
        cblTipologiaFluidoVettore.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaFluidoVettore(idPresel);
        cblTipologiaFluidoVettore.DataBind();
    }

    protected void cbTipologiaGeneratori(int? iDPresel)
    {
        cblTipologiaGeneratori.Items.Clear();
                
        cblTipologiaGeneratori.DataValueField = "IDTipologiaGeneratori";
        cblTipologiaGeneratori.DataTextField = "TipologiaGeneratori";
        cblTipologiaGeneratori.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaGeneratori(iDPresel);
        cblTipologiaGeneratori.DataBind();
    }

    protected void cbTipologiaSistemiEmissione(int? idPresel)
    {
        cblTipologiaSistemiEmissione.Items.Clear();

        cblTipologiaSistemiEmissione.DataValueField = "IDTipologiaSistemiEmissione";
        cblTipologiaSistemiEmissione.DataTextField = "TipologiaSistemiEmissione";
        cblTipologiaSistemiEmissione.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaSistemiEmissione(idPresel);
        cblTipologiaSistemiEmissione.DataBind();
    }

    protected void cbTipologiaSistemaContabilizzazione(int? idPresel)
    {
        rblIDTipologiaSistemaContabilizzazione.Items.Clear();

        rblIDTipologiaSistemaContabilizzazione.DataValueField = "IDTipologiaSistemaContabilizzazione";
        rblIDTipologiaSistemaContabilizzazione.DataTextField = "TipologiaSistemaContabilizzazione";
        rblIDTipologiaSistemaContabilizzazione.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaSistemaContabilizzazione(idPresel);
        rblIDTipologiaSistemaContabilizzazione.DataBind();
    }

    protected void cbTipologiaTermostatoZona(int? idPresel)
    {
        rblTipologiaTermostatoZona.Items.Clear();

        rblTipologiaTermostatoZona.DataValueField = "IDTipologiaTermostatoZona";
        rblTipologiaTermostatoZona.DataTextField = "TipologiaTermostatoZona";
        rblTipologiaTermostatoZona.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaTermostatoZona(idPresel);
        rblTipologiaTermostatoZona.DataBind();
    }
    
    protected void cbTipologiaSistemaDistribuzione(int? idPresel)
    {
        chkTipologiaDistribuzione.Items.Clear();

        chkTipologiaDistribuzione.DataValueField = "IDTipologiaSistemaDistribuzione";
        chkTipologiaDistribuzione.DataTextField = "TipologiaSistemaDistribuzione";
        chkTipologiaDistribuzione.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaSistemaDistribuzione(idPresel);
        chkTipologiaDistribuzione.DataBind();
    }

    protected void rbTipologiaResponsabili(int? idPresel)
    {
        rblTipologiaResponsabili.Items.Clear();
        rblTipologiaResponsabili.DataValueField = "IDTipologiaResponsabile";
        rblTipologiaResponsabili.DataTextField = "TipologiaResponsabile";
        rblTipologiaResponsabili.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaResponsabile(idPresel);
        rblTipologiaResponsabili.DataBind();
        
        rblTipologiaResponsabili.SelectedIndex = 0;
    }

    protected void rbTipologiaSoggetti(int? idPresel)
    {
        rblTipologiaSoggetti.Items.Clear();
        rblTipologiaSoggetti.DataValueField = "IDTipoSoggetto";
        rblTipologiaSoggetti.DataTextField = "TipoSoggetto";
        rblTipologiaSoggetti.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipoSoggetto(idPresel, true);
        rblTipologiaSoggetti.DataBind();
        
        rblTipologiaSoggetti.SelectedIndex = 0;
    }

    protected void ddTipologiaProtezioneGelo(int? idPresel)
    {
        ddlTipologiaProtezioneGelo.Items.Clear();
                
        ddlTipologiaProtezioneGelo.DataValueField = "IDTipologiaProtezioneGelo";
        ddlTipologiaProtezioneGelo.DataTextField = "TipologiaProtezioneGelo";
        ddlTipologiaProtezioneGelo.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaProtezioneGelo(idPresel);
        ddlTipologiaProtezioneGelo.DataBind();

        ListItem myItem = new ListItem("-- Selezionare --", "");
        ddlTipologiaProtezioneGelo.Items.Insert(0, myItem);
        ddlTipologiaProtezioneGelo.SelectedIndex = 0;
    }

    protected void ddTipologiaCircuitoRaffreddamento(int? idPresel)
    {
        ddlTipologiaCircuitoRaffreddamento.Items.Clear();

        ddlTipologiaCircuitoRaffreddamento.DataValueField = "IDTipologiaCircuitoRaffreddamento";
        ddlTipologiaCircuitoRaffreddamento.DataTextField = "TipologiaCircuitoRaffreddamento";
        ddlTipologiaCircuitoRaffreddamento.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaCircuitoRaffreddamento(idPresel);
        ddlTipologiaCircuitoRaffreddamento.DataBind();
        
        ListItem myItem = new ListItem("-- Selezionare --", "0");
        ddlTipologiaCircuitoRaffreddamento.Items.Insert(0, myItem);
        ddlTipologiaCircuitoRaffreddamento.SelectedIndex = 0;
    }

    protected void ddTipologiaAcquaAlimento(int? idPresel)
    {
        ddlTipologiaAcquaAlimento.Items.Clear();
                
        ddlTipologiaAcquaAlimento.DataValueField = "IDTipologiaAcquaAlimento";
        ddlTipologiaAcquaAlimento.DataTextField = "TipologiaAcquaAlimento";
        ddlTipologiaAcquaAlimento.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaAcquaAlimento(idPresel);
        ddlTipologiaAcquaAlimento.DataBind();
        
        ListItem myItem = new ListItem("-- Selezionare --", "0");
        ddlTipologiaAcquaAlimento.Items.Insert(0, myItem);
        ddlTipologiaAcquaAlimento.SelectedIndex = 0;
    }

    protected void cbTipologiaTrattamentoAcqua(int? iDPresel, CheckBoxList cbTrattamentoAcqua)
    {
        cbTrattamentoAcqua.Items.Clear();
              
        cbTrattamentoAcqua.DataValueField = "IDTipologiaTrattamentoAcqua";
        cbTrattamentoAcqua.DataTextField = "TipologiaTrattamentoAcqua";
        cbTrattamentoAcqua.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaTrattamentoAcqua(iDPresel);
        cbTrattamentoAcqua.DataBind();
    }

    protected void cbTipologiaFiltrazione(int? iDPresel)
    {
        cblTipologiaFiltrazione.Items.Clear();
                
        cblTipologiaFiltrazione.DataValueField = "IDTipologiaFiltrazione";
        cblTipologiaFiltrazione.DataTextField = "TipologiaFiltrazione";
        cblTipologiaFiltrazione.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaFiltrazione(iDPresel);
        cblTipologiaFiltrazione.DataBind();
    }

    protected void cbTipologiaAddolcimentoAcqua(int? iDPresel)
    {
        cblTipologiaAddolcimentoAcqua.Items.Clear();
        
        cblTipologiaAddolcimentoAcqua.DataValueField = "IDTipologiaAddolcimentoAcqua";
        cblTipologiaAddolcimentoAcqua.DataTextField = "TipologiaAddolcimentoAcqua";
        cblTipologiaAddolcimentoAcqua.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaAddolcimentoAcqua(iDPresel);
        cblTipologiaAddolcimentoAcqua.DataBind();
    }

    protected void cbTipologiaCondizionamentoChimico(int? iDPresel)
    {
        cblTipologiaCondizionamentoChimico.Items.Clear();
                
        cblTipologiaCondizionamentoChimico.DataValueField = "IDTipologiaCondizionamentoChimico";
        cblTipologiaCondizionamentoChimico.DataTextField = "TipologiaCondizionamentoChimico";
        cblTipologiaCondizionamentoChimico.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaCondizionamentoChimico(iDPresel);
        cblTipologiaCondizionamentoChimico.DataBind();
    }

    #endregion

    #region DATI CATASTALI
    public void GetDatiCatastali(string iDLibrettoImpianto)
    {
        dgDatiCatastali.DataSource = UtilityLibrettiImpianti.GetValoriDatiCatastali(int.Parse(iDLibrettoImpianto));
        dgDatiCatastali.DataBind();
        if (dgDatiCatastali.Items.Count > 0)
        {
            pnlDatiCatastaliView.Visible = true;
        }
        else
        {
            pnlDatiCatastaliView.Visible = false;
        }
    }
    
    public void RowCommand(Object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                int iDLibrettoImpiantoDatiCatastaliInt = int.Parse(e.CommandArgument.ToString());
                var datiCatastali = new LIM_LibrettiImpiantiDatiCatastali();
                datiCatastali = ctx.LIM_LibrettiImpiantiDatiCatastali.FirstOrDefault(c => c.IDLibrettoImpiantoDatiCatastali == iDLibrettoImpiantoDatiCatastaliInt);

                ctx.LIM_LibrettiImpiantiDatiCatastali.Remove(datiCatastali);
                ctx.SaveChanges();
            }
            
            GetDatiCatastali(IDLibrettoImpianto);
        }
        else if (e.CommandName == "Modify")
        {
            VisibleDatiCatastali(false, null);
            using (CriterDataModel ctx = new CriterDataModel())
            {
                int IDLibrettoImpiantoDatiCatastaliInt = int.Parse(e.CommandArgument.ToString());
                var datiCatastali = new LIM_LibrettiImpiantiDatiCatastali();
                datiCatastali = ctx.LIM_LibrettiImpiantiDatiCatastali.FirstOrDefault(c => c.IDLibrettoImpiantoDatiCatastali == IDLibrettoImpiantoDatiCatastaliInt);

                lblIDLibrettoImpiantoDatiCatastali.Text = IDLibrettoImpiantoDatiCatastaliInt.ToString();
                if (datiCatastali.IDCodiceCatastaleSezione != null)
                {
                    ddlSezioneDatiCatastali.SelectedItem.Value = datiCatastali.IDCodiceCatastaleSezione.ToString();
                }
                if (!string.IsNullOrEmpty(datiCatastali.Foglio))
                {
                    txtFoglio.Text = datiCatastali.Foglio;
                }
                if (!string.IsNullOrEmpty(datiCatastali.Mappale))
                {
                    txtMappale.Text = datiCatastali.Mappale;
                }
                if (!string.IsNullOrEmpty(datiCatastali.Subalterno))
                {
                    txtSubalterno.Text = datiCatastali.Subalterno;
                }
                if (!string.IsNullOrEmpty(datiCatastali.Identificativo))
                {
                    txtIdentificativo.Text = datiCatastali.Identificativo;
                }
            }
            
            VisibleDatiCatastali(true, lblIDCodiceCatastale.Text);
        }
    }

    protected void VisibleDatiCatastali(bool fVisible, object IDCodiceCastale)
    {
        if (fVisible)
        {
            pnlDatiCatastaliInsert.Visible = true;
            lnkInsertDatiCatastali.Visible = false;
            pnlDatiCatastaliView.Visible = false;
        }
        else
        {
            pnlDatiCatastaliInsert.Visible = false;
            lnkInsertDatiCatastali.Visible = true;
            pnlDatiCatastaliView.Visible = true;
        }
    }

    protected void lnkInsertDatiCatastali_Click(object sender, EventArgs e)
    {
        VisibleDatiCatastali(true, lblIDCodiceCatastale.Text);
        ddSezioneDatiCatastali(null, int.Parse(lblIDCodiceCatastale.Text));

        if (ddlSezioneDatiCatastali.Items.Count == 1)
        {
            rowSezioneDatiCatastali.Visible = false;
        }
        else
        {
            rowSezioneDatiCatastali.Visible = true;
        }
    }

    protected void ResetDatiCatastali()
    {
        ddlSezioneDatiCatastali.SelectedIndex = -1;
        txtFoglio.Text = "";
        txtMappale.Text = "";
        txtSubalterno.Text = "";
        txtIdentificativo.Text = "";
        lblIDLibrettoImpiantoDatiCatastali.Text = "";
    }

    protected void ddSezioneDatiCatastali(int? iDPresel, int iDCodiceCatastale)
    {
        ddlSezioneDatiCatastali.Items.Clear();
        ddlSezioneDatiCatastali.DataValueField = "IDCodiceCatastaleSezione";
        ddlSezioneDatiCatastali.DataTextField = "Sezione";
        ddlSezioneDatiCatastali.DataSource = LoadDropDownList.LoadDropDownList_SYS_CodiciCatastaliSezioni(iDPresel, iDCodiceCatastale);
        ddlSezioneDatiCatastali.DataBind();

        ListItem myItem = new ListItem("-- Selezionare --", "0");
        ddlSezioneDatiCatastali.Items.Insert(0, myItem);


        if (ddlSezioneDatiCatastali.Items.Count == 2)
        {
            ddlSezioneDatiCatastali.SelectedIndex = 1;
        }
        else
        {
            ddlSezioneDatiCatastali.SelectedIndex = 0;
        }
    }
    
    protected void btnAnnullaCodiceCatastale_Click(object sender, EventArgs e)
    {
        ResetDatiCatastali();
        VisibleDatiCatastali(false, lblIDCodiceCatastale.Text);
    }

    protected void SaveDatiCatastali(string iDLibrettoImpiantoDatiCatastali)
    {
        using (var ctx = new CriterDataModel())
        {
            using (var dbContextTransaction = ctx.Database.BeginTransaction())
            {
                if (string.IsNullOrEmpty(iDLibrettoImpiantoDatiCatastali))
                {
                    
                    try
                            {
                                var datiCatastali = new LIM_LibrettiImpiantiDatiCatastali();
                                datiCatastali.IDLibrettoImpianto = int.Parse(IDLibrettoImpianto);
                                if (ddlSezioneDatiCatastali.SelectedIndex != 0)
                                {
                                    datiCatastali.IDCodiceCatastaleSezione = int.Parse(ddlSezioneDatiCatastali.SelectedItem.Value);
                                }
                                else
                                {
                                    datiCatastali.IDCodiceCatastaleSezione = null;
                                }
                                if (!string.IsNullOrEmpty(txtFoglio.Text))
                                {
                                    datiCatastali.Foglio = txtFoglio.Text;
                                }
                                if (!string.IsNullOrEmpty(txtMappale.Text))
                                {
                                    datiCatastali.Mappale = txtMappale.Text;
                                }
                                if (!string.IsNullOrEmpty(txtSubalterno.Text))
                                {
                                    datiCatastali.Subalterno = txtSubalterno.Text;
                                }
                                if (!string.IsNullOrEmpty(txtIdentificativo.Text))
                                {
                                    datiCatastali.Identificativo = txtIdentificativo.Text;
                                }

                                ctx.LIM_LibrettiImpiantiDatiCatastali.Add(datiCatastali);
                                ctx.SaveChanges();

                                dbContextTransaction.Commit();
                            }
                            catch (Exception)
                            {
                                dbContextTransaction.Rollback();
                            }
                }
                else
                {
                   
                    try
                            {
                                int iDLibrettoImpiantoDatiCatastaliInt = int.Parse(iDLibrettoImpiantoDatiCatastali);
                                var datiCatastali = new LIM_LibrettiImpiantiDatiCatastali();
                                datiCatastali = ctx.LIM_LibrettiImpiantiDatiCatastali.FirstOrDefault(c => c.IDLibrettoImpiantoDatiCatastali == iDLibrettoImpiantoDatiCatastaliInt);
                                datiCatastali.IDLibrettoImpianto = int.Parse(IDLibrettoImpianto);
                                if ((ddlSezioneDatiCatastali.SelectedIndex != 0) && (ddlSezioneDatiCatastali.SelectedIndex != -1))
                                {
                                    datiCatastali.IDCodiceCatastaleSezione = int.Parse(ddlSezioneDatiCatastali.SelectedItem.Value);
                                }
                                else
                                {
                                    datiCatastali.IDCodiceCatastaleSezione = null;
                                }
                                if (!string.IsNullOrEmpty(txtFoglio.Text))
                                {
                                    datiCatastali.Foglio = txtFoglio.Text;
                                }
                                if (!string.IsNullOrEmpty(txtMappale.Text))
                                {
                                    datiCatastali.Mappale = txtMappale.Text;
                                }
                                if (!string.IsNullOrEmpty(txtSubalterno.Text))
                                {
                                    datiCatastali.Subalterno = txtSubalterno.Text;
                                }
                                if (!string.IsNullOrEmpty(txtIdentificativo.Text))
                                {
                                    datiCatastali.Identificativo = txtIdentificativo.Text;
                                }

                                ctx.SaveChanges();

                                dbContextTransaction.Commit();
                            }
                            catch (Exception)
                            {
                                dbContextTransaction.Rollback();
                            }
                }
            }
        }       
    }

    protected void btnSaveDatiCatastali_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            SaveDatiCatastali(lblIDLibrettoImpiantoDatiCatastali.Text);
            ResetDatiCatastali();
            VisibleDatiCatastali(false, lblIDCodiceCatastale.Text);
            GetDatiCatastali(IDLibrettoImpianto);
        }
    }

    protected void btnAnnullaDatiCatastali_Click(object sender, EventArgs e)
    {
        VisibleDatiCatastali(false, lblIDCodiceCatastale.Text);
    }

    protected void ControllaDatiCatastaliPresenti(object sender, ServerValidateEventArgs e)
    {
        if ((lblIDCodiceCatastale.Text != null) && (txtFoglio.Text != string.Empty) && (txtMappale.Text != string.Empty) && (txtSubalterno.Text != string.Empty))
        {
            object[] getVal = new object[2];
            getVal = UtilityLibrettiImpianti.CheckComuneFoglioParticellaSub(lblIDCodiceCatastale.Text, txtFoglio.Text.Trim(),
                txtMappale.Text.Trim(), txtSubalterno.Text.Trim(), txtIdentificativo.Text.Trim(), string.Empty, IDLibrettoImpianto);

            if (bool.Parse(getVal[0].ToString()))
            {
                e.IsValid = false;
                cvDatiCatastaliPresenti.ErrorMessage = "Attenzione il dato catastale che si sta cercando di inserire è già presente in un libretto di impianto!";
            }
            else
            {
                e.IsValid = true;
            }           
        }        
    }

    #endregion

    public void RedirectPage(string IDLibrettoImpianto)
    {
        if (ConfigurationManager.AppSettings["fEncryptQueryString"] == "on")
        {
            QueryString qs = new QueryString();
            qs.Add("IDLibrettoImpianto", IDLibrettoImpianto);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "~/LIM_LibrettiImpianti.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url, true);
        }
        else
        {
            Response.Redirect("~/LIM_LibrettiImpianti.aspx?IDLibrettoImpianto=" + IDLibrettoImpianto);
        }
    }

    protected void LIM_LibrettiImpianti_btnSavePreliminary_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int iDLibrettoImpiantoInserted = SaveInsertLibrettoImpianto(IDLibrettoImpianto);
            RedirectPage(iDLibrettoImpiantoInserted.ToString());
        }
    }

    #region 1.2 UBICAZIONE E DESTINAZIONE DELL’EDIFICIO - Comune 

    protected void comboComune_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString(), true);
        comboBox.DataBind();
    }

    protected void comboComune_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastaliRequestedByValue(e.Value.ToString());
        comboBox.DataBind();
    }

    protected void comboComune_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshComboComuneLibretto();
    }

    protected void RefreshComboComuneLibretto()
    {
        RadComboBoxCodiciCatastali.SelectedIndex = -1;
        RadComboBoxCodiciCatastali.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString(), true);
        RadComboBoxCodiciCatastali.TextField = "Codice";
        RadComboBoxCodiciCatastali.ValueField = "IDCodiceCatastale";
        RadComboBoxCodiciCatastali.DataBind();

        btnSaveComuneLibretto.Visible = false;
    }
    #endregion
    
    #region 1.3 IMPIANTO TERMICO DESTINATO A SODDISFARE I SEGUENTI SERVIZI
    protected void VisibleClimatizzazioneAcs(bool fClimatizzazioneAcs)
    {
        if (fClimatizzazioneAcs)
        {
            pnlClimatizzazioneAcs.Visible = fClimatizzazioneAcs;
        }
        else
        {
            pnlClimatizzazioneAcs.Visible = fClimatizzazioneAcs;
            txtClimatizzazionePotenzaAcs.Text = "";
        }
        
    }

    protected void chkClimatizzazioneAcs_CheckedChanged(object sender, EventArgs e)
    {
        VisibleClimatizzazioneAcs(chkClimatizzazioneAcs.Checked);
    }

    protected void VisibleClimatizzazioneInvernale(bool fClimatizzazioneInvernale)
    {
        if (fClimatizzazioneInvernale)
        {
            pnlClimatizzazioneInvernale.Visible = fClimatizzazioneInvernale;
        }
        else
        {
            pnlClimatizzazioneInvernale.Visible = fClimatizzazioneInvernale;
            txtClimatizzazionePotenzaInvernale.Text = "";
        }
    }

    protected void chkClimatizzazioneInvernale_CheckedChanged(object sender, EventArgs e)
    {
        VisibleClimatizzazioneInvernale(chkClimatizzazioneInvernale.Checked);
    }
    
    protected void VisibleClimatizzazioneEstiva(bool fClimatizzazioneEstiva)
    {
        if (fClimatizzazioneEstiva)
        {
            pnlClimatizzazioneEstiva.Visible = fClimatizzazioneEstiva;
        }
        else
        {
            pnlClimatizzazioneEstiva.Visible = fClimatizzazioneEstiva;
            txtClimatizzazionePotenzaEstiva.Text = "";
        }
    }

    protected void chkClimatizzazioneEstiva_CheckedChanged(object sender, EventArgs e)
    {
        VisibleClimatizzazioneEstiva(chkClimatizzazioneEstiva.Checked);
    }
        
    protected void VisibleClimatizzazioneAltro(bool fClimatizzazioneAltro)
    {
        if (fClimatizzazioneAltro)
        {
            pnlClimatizzazioneAltro.Visible = fClimatizzazioneAltro;
        }
        else
        {
            pnlClimatizzazioneAltro.Visible = fClimatizzazioneAltro;
            txtClimatizzazioneAltro.Text = "";
        }
    }

    protected void chkClimatizzazioneAltro_CheckedChanged(object sender, EventArgs e)
    {
        VisibleClimatizzazioneAltro(chkClimatizzazioneAltro.Checked);
    }
    #endregion

    #region 1.4 TIPOLOGIA FLUIDO VETTORE
        
    protected void VisibleTipologiaFluidoVettoreAltro()
    {
        if (cblTipologiaFluidoVettore.SelectedItem != null)
        {
            foreach (ListItem item in cblTipologiaFluidoVettore.Items)
            {
                if ((item.Selected) && (item.Value == "1"))
                {
                    pnlTipologiaFluidoVettoreAltro.Visible = true;
                }
                else if ((!item.Selected) && (item.Value == "1"))
                {
                    pnlTipologiaFluidoVettoreAltro.Visible = false;
                    txtTipologiaFluidoVettoreAltro.Text = "";
                }
            }
        }
        else
        {
            pnlTipologiaFluidoVettoreAltro.Visible = false;
            txtTipologiaFluidoVettoreAltro.Text = "";
        }
    }

    protected void cblTipologiaFluidoVettore_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTipologiaFluidoVettoreAltro();
    }

    #endregion

    #region 1.5 INDIVIDUAZIONE DELLA TIPOLOGIA DEI GENERATORI
    protected void VisibleTipologiaGeneratoriAltro()
    {
        if (cblTipologiaGeneratori.SelectedItem != null)
        {
            foreach (ListItem item in cblTipologiaGeneratori.Items)
            {
                if ((item.Selected) && (item.Value == "1"))
                {
                    pnlTipologiaGeneratoriAltro.Visible = true;
                }
                else if ((!item.Selected) && (item.Value == "1"))
                {
                    pnlTipologiaGeneratoriAltro.Visible = false;
                    txtTipologiaGeneratoriAltro.Text = "";
                }
            }
        }
        else
        {
            pnlTipologiaGeneratoriAltro.Visible = false;
            txtTipologiaGeneratoriAltro.Text = "";
        }
    }

    protected void cblTipologiaGeneratori_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTipologiaGeneratoriAltro();
    }

    protected void VisiblePannelliSolariTermici(bool fPannelliSolariTermici, bool fPannelliSolariTermiciAltro)
    {
        if (fPannelliSolariTermici)
        {
            pnlSuperficieTotaleLordaPannelli.Visible = fPannelliSolariTermici;
            rowIntegrazioniPannelli.Visible = fPannelliSolariTermici;
        }
        else
        {
            pnlSuperficieTotaleLordaPannelli.Visible = fPannelliSolariTermici;
            rowIntegrazioniPannelli.Visible = fPannelliSolariTermici;
            txtSuperficieTotaleLordaPannelli.Text = "";
        }
        VisibleAltreIntegrazioniPannelli(fPannelliSolariTermici, fPannelliSolariTermiciAltro);
        VisibleAltroPannelli(fPannelliSolariTermiciAltro);
    }

    protected void VisibleAltroPannelli(bool fAltroPannelli)
    {
        if (fAltroPannelli)
        {
            pnlAltroPannelli.Visible = fAltroPannelli;
            pnlPotenzaUtilePannelli.Visible = fAltroPannelli;
        }
        else
        {
            pnlPotenzaUtilePannelli.Visible = fAltroPannelli;
            pnlAltroPannelli.Visible = fAltroPannelli;
            txtAltroPannelli.Text = "";
            txtPotenzaUtilePannelli.Text = "";
        }
        VisibleAltreIntegrazioniPannelli(chkPannelliSolariTermici.Checked, fAltroPannelli);
    }

    protected void VisibleAltreIntegrazioniPannelli(bool fPannelliSolariTermici, bool fAltroPannelli)
    {
        if (fPannelliSolariTermici || fAltroPannelli)
        {
            rowIntegrazioniPannelli.Visible = true;
        }
        else
        {
            rowIntegrazioniPannelli.Visible = false;

            chkClimatizzazionePannelliAcs.Checked = false;
            chkClimatizzazionePannelliInvernale.Checked = false;
            chkClimatizzazionePannelliEstiva.Checked = false;
        }
    }

    protected void chkAltroPannelli_CheckedChanged(object sender, EventArgs e)
    {
        VisibleAltroPannelli(chkAltroPannelli.Checked);
    }

    #endregion

    #region 1.6 RESPONSABILE DELL’IMPIANTO

    protected void VisibleTipologiaSoggetti()
    {
        switch (rblTipologiaSoggetti.SelectedValue)
        {
            case "1": //Persona fisica
                rowCodiceFiscaleResponsabile.Visible = true;
                rowRagioneSocialeResponsabile.Visible = false;
                rowPartitaIvaResponsabile.Visible = false;

                LIM_LibrettiImpianti_lblTitoloNomeResponsabile.Text = "Nome (*)";
                LIM_LibrettiImpianti_lblTitoloCognomeResponsabile.Text = "Cognome (*)";
                LIM_LibrettiImpianti_lblTitoloCodiceFiscaleResponsabile.Text = "Codice fiscale (*)";

                txtRagioneSocialeResponsabile.Text = "";
                txtPartitaIvaResponsabile.Text = "";
                break;
            case "2": //Persona giuridica
                rowCodiceFiscaleResponsabile.Visible = true;
                rowRagioneSocialeResponsabile.Visible = true;
                rowPartitaIvaResponsabile.Visible = true;

                LIM_LibrettiImpianti_lblTitoloNomeResponsabile.Text = "Nome legale rappresentante (*)";
                LIM_LibrettiImpianti_lblTitoloCognomeResponsabile.Text = "Cognome legale rappresentante (*)";
                LIM_LibrettiImpianti_lblTitoloCodiceFiscaleResponsabile.Text = "Codice fiscale legale rappresentante";
                rfvtxtCodiceFiscaleResponsabile.Enabled = false;
                txtCodiceFiscaleResponsabile.CssClass = "txtClass";
                //txtCodiceFiscaleResponsabile.Text = "";
                break;
        }
    }

    protected void rblTipologiaSoggetti_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTipologiaSoggetti();
    }
    
    protected void ASPxComboBox3_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox) source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString(), false);
        comboBox.DataBind();
    }

    protected void ASPxComboBox3_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox) source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastaliRequestedByValue(e.Value.ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox3_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetIDProvinciaByIDComune(ASPxComboBox3.Value);
    }

    protected void GetIDProvinciaByIDComune(object iDComune)
    {
        int iDComuneInt = 0;
        lblProvinciaResponsabile.Text = "";
        lblIDProvinciaResponsabile.Text = "";
        if (iDComune != null)
        {
            if (int.TryParse(iDComune.ToString(), out iDComuneInt))
            {
                var provincia = LoadDropDownList.LoadDropDownList_V_SYS_CodiciCatastali(iDComuneInt).FirstOrDefault();
                lblProvinciaResponsabile.Text = provincia.Provincia;
                lblIDProvinciaResponsabile.Text = provincia.IDProvincia.ToString();
            }
        }
    }

    protected void ASPxComboBox3_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshComboComune();
        RefreshProvincia();
    }

    protected void RefreshComboComune()
    {
        ASPxComboBox3.SelectedIndex = -1;
        ASPxComboBox3.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString(), true);
        ASPxComboBox3.TextField = "Codice";
        ASPxComboBox3.ValueField = "IDCodiceCatastale";
        ASPxComboBox3.DataBind();
    }

    protected void RefreshProvincia()
    {
        lblProvinciaResponsabile.Text = "";
        lblIDProvinciaResponsabile.Text = "";
    }

    #endregion

    #region 2.3 TRATTAMENTO DELL’ACQUA DELL’IMPIANTO DI CLIMATIZZAZIONE INVERNALE

    protected void VisibleTrattamentoAcquaInvernale()
    {
        if (bool.Parse(rbTrattamentoAcquaInvernale.SelectedItem.Value))
        {
            rowTrattamentoAcquaInvernale.Visible = true;
        }
        else
        {
            rowTrattamentoAcquaInvernale.Visible = false;
            cblTipologiaTrattamentoAcquaInvernale.ClearSelection();
        }
        VisibleTipologiaTrattamentoAcquaInvernale();
        //VisibleTipologiaProtezioneGelo("");
    }

    protected void VisibleTipologiaTrattamentoAcquaInvernale()
    {
        if (cblTipologiaTrattamentoAcquaInvernale.SelectedItem != null)
        {
            foreach (ListItem item in cblTipologiaTrattamentoAcquaInvernale.Items)
            {
                if ((item.Selected) && (item.Value == "2"))
                {
                    rowTrattamentoAcquaInvernaleDurezzaAcqua.Visible = true;
                    //rowTrattamentoAcquaInvernaleProtezioneGelo.Visible = true;
                }
                else if ((!item.Selected) && (item.Value == "2"))
                {
                    rowTrattamentoAcquaInvernaleDurezzaAcqua.Visible = false;
                    //rowTrattamentoAcquaInvernaleProtezioneGelo.Visible = false;
                    txtDurezzaAcquaInvernale.Text = "";
                    //ddlTipologiaProtezioneGelo.SelectedIndex = 0;
                }
            }
        }
        else
        {
            rowTrattamentoAcquaInvernaleDurezzaAcqua.Visible = false;
            //rowTrattamentoAcquaInvernaleProtezioneGelo.Visible = false;
            txtDurezzaAcquaInvernale.Text = "";
            //ddlTipologiaProtezioneGelo.SelectedIndex = 0;
            //cblTipologiaTrattamentoAcquaInvernale.ClearSelection();
        }
    }

    protected void VisibleProtezioneGelo()
    {
        if (bool.Parse(rbProtezioneGelo.SelectedItem.Value))
        {
            rowTrattamentoAcquaInvernaleProtezioneGelo.Visible = true;
        }
        else
        {
            rowTrattamentoAcquaInvernaleProtezioneGelo.Visible = false;
            ddlTipologiaProtezioneGelo.SelectedIndex = 0;
        }
        VisibleTipologiaProtezioneGelo();
    }

    protected void VisibleTipologiaProtezioneGelo()
    {
        if ((ddlTipologiaProtezioneGelo.SelectedValue != "0") && (ddlTipologiaProtezioneGelo.SelectedValue != ""))
        {
            rowTrattamentoAcquaInvernalePercentualeGlicole.Visible = true;
            rowTrattamentoAcquaInvernalePhGlicole.Visible = true;          
        }
        else
        {
            rowTrattamentoAcquaInvernalePercentualeGlicole.Visible = false;
            rowTrattamentoAcquaInvernalePhGlicole.Visible = false;
            txtPercentualeGlicole.Text = "";
            txtPhGlicole.Text = "";
        }
    }

    protected void rbTrattamentoAcquaInvernale_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTrattamentoAcquaInvernale();
    }

    protected void cblTipologiaTrattamentoAcquaInvernale_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTipologiaTrattamentoAcquaInvernale();
    }

    protected void ddlTipologiaProtezioneGelo_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTipologiaProtezioneGelo();
    }

    protected void rbProtezioneGelo_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleProtezioneGelo();
    }

    #endregion
    
    #region 2.4 TRATTAMENTO DELL’ACQUA DELL’IMPIANTO DI CLIMATIZZAZIONE ACS

    protected void VisibleTrattamentoAcquaAcs(bool fTrattamentoAcquaInvernale)
    {
        rowTrattamentoAcquaAcs.Visible = fTrattamentoAcquaInvernale;
        VisibleTipologiaTrattamentoAcquaAcs();
    }

    protected void VisibleTipologiaTrattamentoAcquaAcs()
    {
        if ((cblTipologiaTrattamentoAcquaAcs.SelectedItem != null) && (bool.Parse(rbTrattamentoAcquaAcs.SelectedItem.Value)))
        {
            foreach (ListItem item in cblTipologiaTrattamentoAcquaAcs.Items)
            {
                if ((item.Selected) && (item.Value == "2"))
                {
                    rowTrattamentoAcquaAcsDurezzaAcqua.Visible = true;
                }
                else if ((!item.Selected) && (item.Value == "2"))
                {
                    rowTrattamentoAcquaAcsDurezzaAcqua.Visible = false;
                    txtDurezzaAcquaAcs.Text = "";
                }
            }
        }
        else
        {
            rowTrattamentoAcquaAcsDurezzaAcqua.Visible = false;
            txtDurezzaAcquaAcs.Text = "";
            cblTipologiaTrattamentoAcquaAcs.ClearSelection();
        }
    }

    protected void cblTipologiaTrattamentoAcquaAcs_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTipologiaTrattamentoAcquaAcs();
    }

    protected void rbTrattamentoAcquaAcs_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTrattamentoAcquaAcs(bool.Parse(rbTrattamentoAcquaAcs.SelectedItem.Value));
    }
    #endregion

    #region 2.5 TRATTAMENTO DELL’ACQUA DELL’IMPIANTO DI CLIMATIZZAZIONE ESTIVA
    protected void cblTipologiaFiltrazione_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTipologiaFiltrazioniAltro();
    }

    protected void cblTipologiaAddolcimentoAcqua_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTipologiaAddolcimentoAcquaAltro();
    }

    protected void cblTipologiaCondizionamentoChimico_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTipologiaCondizionamentoChimicoAltro();
    }

    protected void VisibleTipologiaFiltrazioniAltro()
    {
        if (cblTipologiaFiltrazione.SelectedItem != null)
        {
            foreach (ListItem item in cblTipologiaFiltrazione.Items)
            {
                if ((item.Selected) && (item.Value == "1"))
                {
                    pnlTipologiaFiltrazioniAltro.Visible = true;
                }
                else if ((!item.Selected) && (item.Value == "1"))
                {
                    pnlTipologiaFiltrazioniAltro.Visible = false;
                    txtTipologiaFiltrazioniAltro.Text = "";
                }
            }
        }
        else
        {
            pnlTipologiaFiltrazioniAltro.Visible = false;
            txtTipologiaFiltrazioniAltro.Text = "";
        }
    }

    protected void VisibleTipologiaAddolcimentoAcquaAltro()
    {
        if (cblTipologiaAddolcimentoAcqua.SelectedItem != null)
        {
            foreach (ListItem item in cblTipologiaAddolcimentoAcqua.Items)
            {
                if ((item.Selected) && (item.Value == "1"))
                {
                    pnlTipologiaAddolcimentoAltro.Visible = true;
                }
                else if ((!item.Selected) && (item.Value == "1"))
                {
                    pnlTipologiaAddolcimentoAltro.Visible = false;
                    txtTipologiaAddolcimentoAltro.Text = "";
                }
            }
        }
        else
        {
            pnlTipologiaAddolcimentoAltro.Visible = false;
            txtTipologiaAddolcimentoAltro.Text = "";
        }
    }

    protected void VisibleTipologiaCondizionamentoChimicoAltro()
    {
        if (cblTipologiaCondizionamentoChimico.SelectedItem != null)
        {
            foreach (ListItem item in cblTipologiaCondizionamentoChimico.Items)
            {
                if ((item.Selected) && (item.Value == "1"))
                {
                    pnlTipologiaCondizionamentoChimicoAltro.Visible = true;
                }
                else if ((!item.Selected) && (item.Value == "1"))
                {
                    pnlTipologiaCondizionamentoChimicoAltro.Visible = false;
                    txtTipologiaCondizionamentoChimicoAltro.Text = "";
                }
            }
        }
        else
        {
            pnlTipologiaCondizionamentoChimicoAltro.Visible = false;
            txtTipologiaCondizionamentoChimicoAltro.Text = "";
        }
    }

    protected void VisibleTrattamentoAcquaEstiva(bool fTrattamentoAcquaEstiva)
    {
        if (fTrattamentoAcquaEstiva)
        {
            rowTrattamentoAcquaEstivaTipologiaCircuitoRaffreddamento.Visible = true;
            rowTrattamentoAcquaEstivaTipologiaAcquaAlimento.Visible = true;
            rowTrattamentoAcquaEstiva.Visible = true;
            rowTrattamentoAcquaEstivaSistemaSpurgoAutomatico.Visible = true;
        }
        else
        {
            rowTrattamentoAcquaEstivaTipologiaCircuitoRaffreddamento.Visible = false;
            rowTrattamentoAcquaEstivaTipologiaAcquaAlimento.Visible = false;
            rowTrattamentoAcquaEstiva.Visible = false;
            rowTrattamentoAcquaEstivaSistemaSpurgoAutomatico.Visible = false;

            ddlTipologiaCircuitoRaffreddamento.SelectedIndex = 0;
            ddlTipologiaAcquaAlimento.SelectedIndex = 0;
            cblTipologiaTrattamentoAcquaEstiva.ClearSelection();
            chkSistemaSpurgoAutomatico.Checked = false;
        }
        VisibleTipologiaTrattamentoFiltrazioneAddolcimentoCondizionamentoChimico();
        VisibleSistemaSpurgoAutomatico(chkSistemaSpurgoAutomatico.Checked);
    }

    protected void VisibleTipologiaTrattamentoFiltrazioneAddolcimentoCondizionamentoChimico()
    {
        if ((cblTipologiaTrattamentoAcquaEstiva.SelectedItem != null) && (bool.Parse(rbTrattamentoAcquaEstiva.SelectedItem.Value)))
        {
            foreach (ListItem item in cblTipologiaTrattamentoAcquaEstiva.Items)
            {
                if ((item.Selected) && (item.Value == "1"))
                {
                    rowTrattamentoAcquaEstivaTipologiaFiltrazione.Visible = true;
                    
                }
                else if ((item.Selected) && (item.Value == "2"))
                {
                    rowTrattamentoAcquaEstivaTipologiaAddolcimentoAcqua.Visible = true;

                }
                else if ((item.Selected) && (item.Value == "3"))
                {
                    rowTrattamentoAcquaEstivaTipologiaCondizionamentoChimico.Visible = true;

                }
                else if ((!item.Selected) && (item.Value == "1"))
                {
                    rowTrattamentoAcquaEstivaTipologiaFiltrazione.Visible = false;
                    cblTipologiaFiltrazione.ClearSelection();
                    
                }
                else if ((!item.Selected) && (item.Value == "2"))
                {
                    rowTrattamentoAcquaEstivaTipologiaAddolcimentoAcqua.Visible = false;
                    cblTipologiaAddolcimentoAcqua.ClearSelection();
                    
                }
                else if ((!item.Selected) && (item.Value == "3"))
                {
                    rowTrattamentoAcquaEstivaTipologiaCondizionamentoChimico.Visible = false;
                    cblTipologiaCondizionamentoChimico.ClearSelection();
                }
            }
        }
        else
        {
            rowTrattamentoAcquaEstivaTipologiaFiltrazione.Visible = false;
            rowTrattamentoAcquaEstivaTipologiaAddolcimentoAcqua.Visible = false;
            rowTrattamentoAcquaEstivaTipologiaCondizionamentoChimico.Visible = false;
            cblTipologiaFiltrazione.ClearSelection();
            cblTipologiaAddolcimentoAcqua.ClearSelection();
            cblTipologiaCondizionamentoChimico.ClearSelection();
        }
    }

    protected void VisibleSistemaSpurgoAutomatico(bool fSistemaSpurgoAutomatico)
    {
        if (fSistemaSpurgoAutomatico)
        {
            rowTrattamentoAcquaEstivaConducibilitaAcquaIngresso.Visible = true;
            rowTrattamentoAcquaEstivaConducibilitaInizioSpurgo.Visible = true;
        }
        else
        {
            rowTrattamentoAcquaEstivaConducibilitaAcquaIngresso.Visible = false;
            rowTrattamentoAcquaEstivaConducibilitaInizioSpurgo.Visible = false;
            txtConducibilitaAcquaIngresso.Text = "";
            txtConducibilitaInizioSpurgo.Text = "";
        }
    }

    protected void rbTrattamentoAcquaEstiva_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTrattamentoAcquaEstiva(bool.Parse(rbTrattamentoAcquaEstiva.SelectedItem.Value));
    }

    protected void cblTipologiaTrattamentoAcquaEstiva_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTipologiaTrattamentoFiltrazioneAddolcimentoCondizionamentoChimico();
    }

    protected void chkSistemaSpurgoAutomatico_CheckedChanged(object sender, EventArgs e)
    {
        VisibleSistemaSpurgoAutomatico(chkSistemaSpurgoAutomatico.Checked);
    }

    #endregion

    #region 5.1 REGOLAZIONE PRIMARIA

    protected void chkAltroSistemaRegolazionePrimaria_CheckedChanged(object sender, EventArgs e)
    {
        VisibleAltroSistemaRegolazionePrimaria(chkAltroSistemaRegolazionePrimaria.Checked);
    }
    protected void chkSistemaRegolazioneIndipendente_CheckedChanged(object sender, EventArgs e)
    {
        VisibleSistemaRegolazione(chkSistemaRegolazioneIndipendente.Checked);
    }

    protected void rblValvoleRegolazione_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool fValvoleRegolazione = Convert.ToBoolean(Convert.ToInt16(rblValvoleRegolazione.SelectedItem.Value));
        VisibleValvoleRegolazione(fValvoleRegolazione);
    }
    
    protected void VisibleAltroSistemaRegolazionePrimaria(bool altroSistemaRegolazionePrimaria)
    {
        pnlAltroSistemaRegolazionePrimaria.Visible = altroSistemaRegolazionePrimaria;
    }
    protected void VisibleSistemaRegolazione(bool sistemaRegolazioneIndipendente)
    {
        rowSistemiRegolazioneHeader.Visible = sistemaRegolazioneIndipendente;
        rowSistemiRegolazione.Visible = sistemaRegolazioneIndipendente;
    }
    protected void VisibleValvoleRegolazione(bool valvoleRegolazione)
    {
        rowValvoleRegolazioneHeader.Visible = valvoleRegolazione;
        rowValvoleRegolazione.Visible = valvoleRegolazione;
    }

    #endregion

    #region 5.4 CONTABILIZZAZIONE

    protected void rblUnitaImmobiliariContabilizzate_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleUnitaImmobiliariContabilizzate(rblUnitaImmobiliariContabilizzate.SelectedValue == "1");
    }

    protected void VisibleUnitaImmobiliariContabilizzate(bool visible)
    {
        rowDettaglioSistemaContabilizzazione1.Visible = visible;
        rowDettaglioSistemaContabilizzazione2.Visible = visible;
        rowDescrizioneSistemaContabilizzazione.Visible = visible;
    }

    #endregion

    #region 6.1 TIPOLOGIA DI DISTRIBUZIONE

    protected void chkTipologiaDistribuzione_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTipologiaSistemaDistribuzioneAltro();
    }

    protected void VisibleTipologiaSistemaDistribuzioneAltro()
    {
        if (chkTipologiaDistribuzione.SelectedItem != null)
        {
            foreach (ListItem item in chkTipologiaDistribuzione.Items)
            {
                if ((item.Selected) && (item.Value == "1"))
                {
                    pnlTipologiaDistribuzioneAltro.Visible = true;
                }
                else if ((!item.Selected) && (item.Value == "1"))
                {
                    pnlTipologiaDistribuzioneAltro.Visible = false;
                    txtTipologiaDistribuzioneAltro.Text = "";
                }
            }
        }
        else
        {
            pnlTipologiaDistribuzioneAltro.Visible = false;
            txtTipologiaDistribuzioneAltro.Text = "";
        }
    }
        
    #endregion

    #region 7.0 SISTEMI DI EMISSIONE

    public void GetDatiLibrettoImpiantoTipologiaSistemiEmissione()
    {
        foreach (var tipologia in LibrettoImpiantoCorrente.LIM_LibrettiImpiantiTipologiaSistemiEmissione)
        {
            cblTipologiaSistemiEmissione.Items.FindByValue(tipologia.IDTipologiaSistemiEmissione.ToString()).Selected = true;
        }
        txtTipologiaSistemiEmissioneAltro.Text = LibrettoImpiantoCorrente.SistemaEmissioneAltro;

        VisibleTipologiaSistemiEmissioneAltro();
    }

    protected void VisibleTipologiaSistemiEmissioneAltro()
    {
        if (cblTipologiaSistemiEmissione.SelectedItem != null)
        {
            foreach (ListItem item in cblTipologiaSistemiEmissione.Items)
            {
                if ((item.Selected) && (item.Value == "8"))
                {
                    pnlTipologiaSistemiEmissioneAltro.Visible = true;
                }
                else if ((!item.Selected) && (item.Value == "8"))
                {
                    pnlTipologiaSistemiEmissioneAltro.Visible = false;
                    txtTipologiaSistemiEmissioneAltro.Text = "";
                }
            }
        }
        else
        {
            pnlTipologiaSistemiEmissioneAltro.Visible = false;
            txtTipologiaSistemiEmissioneAltro.Text = "";
        }
    }

    protected void cblTipologiaSistemiEmissione_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTipologiaSistemiEmissioneAltro();
    }

    #endregion
    
    protected void chkPannelliSolariTermici_CheckedChanged(object sender, EventArgs e)
    {
        VisiblePannelliSolariTermici(chkPannelliSolariTermici.Checked, chkAltroPannelli.Checked);
    }
        
    protected void rblfTerzoResponsabile_SelectedIndexChanged(object sender, EventArgs e)
    {
        SaveInsertLibrettoImpianto(IDLibrettoImpianto);
        SaveTipologiaFluidoVettore(IDLibrettoImpianto);
        SaveTipologiaGeneratori(IDLibrettoImpianto);
        SaveTipologiaTrattamentoAcquaInvernale(IDLibrettoImpianto);
        SaveTipologiaTrattamentoAcquaAcs(IDLibrettoImpianto);
        SaveTipologiaTrattamentoAcquaEstiva(IDLibrettoImpianto);

        SaveTipologiaFiltrazioni(IDLibrettoImpianto);
        SaveTipologiaAddolcimentoAcqua(IDLibrettoImpianto);
        SaveTipologiaCondizionamentoChimico(IDLibrettoImpianto);

        SaveTipologiaSistemaDistribuzione(IDLibrettoImpianto);

        bool fTerzoResponsabile = false;
        if (rblfTerzoResponsabile.SelectedValue == "1")
        {
            fTerzoResponsabile = true;
        }
        
        VisibleTerzoResponsabile(fTerzoResponsabile);
    }

    protected void VisibleTerzoResponsabile(bool fTerzoResponsabile)
    {
        rowTerzoResponsabileHeader.Visible = rowTerzoResponsabile.Visible  = rowTerzoResponsabileFooter.Visible = fTerzoResponsabile;

        if (!fTerzoResponsabile)
        {
            ctlTerzoResponsabile.DeleteRow();
        }
        //if (rowTerzoResponsabile.Visible && ctlTerzoResponsabile.AllowInsert)
        //{
        //    ctlTerzoResponsabile.AddNewRow();
        //}
        //else
        //{
        //    ctlTerzoResponsabile.DeleteRow();
        //}
    }

    protected void LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click(object sender, EventArgs e)
    {
        SaveInsertLibrettoImpianto(IDLibrettoImpianto);
        SaveTipologiaFluidoVettore(IDLibrettoImpianto);
        SaveTipologiaGeneratori(IDLibrettoImpianto);
        SaveTipologiaTrattamentoAcquaInvernale(IDLibrettoImpianto);
        SaveTipologiaTrattamentoAcquaAcs(IDLibrettoImpianto);
        SaveTipologiaTrattamentoAcquaEstiva(IDLibrettoImpianto);

        SaveTipologiaFiltrazioni(IDLibrettoImpianto);
        SaveTipologiaAddolcimentoAcqua(IDLibrettoImpianto);
        SaveTipologiaCondizionamentoChimico(IDLibrettoImpianto);

        SaveTipologiaSistemaDistribuzione(IDLibrettoImpianto);
        Logger.LogUser(TipoEvento.ModificaDati, UtilityApp.GetCurrentPageName(), HttpContext.Current.User.Identity.Name, HttpContext.Current.Request.UserHostAddress);
    }

    protected void LIM_LibrettiImpianti_btnSaveLibrettoImpianto_Click(object sender, EventArgs e)
    {
        Page.Validate("vgLibrettoImpianto");
        if (Page.IsValid)
        {
            SaveInsertLibrettoImpianto(IDLibrettoImpianto);
            SaveTipologiaFluidoVettore(IDLibrettoImpianto);
            SaveTipologiaGeneratori(IDLibrettoImpianto);
            SaveTipologiaTrattamentoAcquaInvernale(IDLibrettoImpianto);
            SaveTipologiaTrattamentoAcquaAcs(IDLibrettoImpianto);
            SaveTipologiaTrattamentoAcquaEstiva(IDLibrettoImpianto);
            SaveTipologiaFiltrazioni(IDLibrettoImpianto);
            SaveTipologiaAddolcimentoAcqua(IDLibrettoImpianto);
            SaveTipologiaCondizionamentoChimico(IDLibrettoImpianto);
            SaveTipologiaSistemaDistribuzione(IDLibrettoImpianto);
            Logger.LogUser(TipoEvento.ModificaDati, UtilityApp.GetCurrentPageName(), HttpContext.Current.User.Identity.Name, HttpContext.Current.Request.UserHostAddress);
        }
    }
    
    protected void LIM_LibrettiImpianti_btnCloseLibrettoImpianto_Click(object sender, EventArgs e)
    {
        Page.Validate("vgLibrettoImpianto");
        if (Page.IsValid)
        {
            SaveInsertLibrettoImpianto(IDLibrettoImpianto);
            SaveTipologiaGeneratori(IDLibrettoImpianto);
            SaveTipologiaFiltrazioni(IDLibrettoImpianto);
            SaveTipologiaAddolcimentoAcqua(IDLibrettoImpianto);
            SaveTipologiaCondizionamentoChimico(IDLibrettoImpianto);

            LibrettoImpiantoCorrente.IDStatoLibrettoImpianto = 2;
            CurrentDataContext.SaveChanges();
            SetControlsVisibility();

            if (ConfigurationManager.AppSettings["fEncryptQueryString"] == "on")
            {
                QueryString qs = new QueryString();
                qs.Add("IDLibrettoImpianto", IDLibrettoImpianto);
                QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                string url = "LIM_LibrettiImpianti.aspx";
                url += qsEncrypted.ToString();
                Response.Redirect(url);
            }

            //notifica operazione completata
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "LIM_LibrettiImpianti_btnCloseLibrettoImpianto_Click",
            //   "alert('Il libretto è stato reso definitivo.');", true);
        }
    }

    protected void LIM_LibrettiImpianti_btnViewLibrettoImpianto_Click(object sender, EventArgs e)
    {
        
    }

    protected void LIM_LibrettiImpianti_btnCopyLibrettoImpianto_Click(object sender, EventArgs e)
    {
        //TODO: copia dati in nuovo libretto cambiando codice targatura tra quelli liberi
    }
    
    protected void LIM_LibrettiImpianti_btnRevisioneLibrettoImpianto_Click(object sender, EventArgs e)
    {
        DataLayer.LIM_LibrettiImpianti nuovaRevisione = UtilityLibrettiImpianti.RevisionaLibretto(LibrettoImpiantoCorrente);
        
        string url = "LIM_LibrettiImpianti.aspx";
        QueryString qs = new QueryString();
        qs.Add("IDLibrettoImpianto", nuovaRevisione.IDLibrettoImpianto.ToString());
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        url = "LIM_LibrettiImpianti.aspx";
        url += qsEncrypted.ToString();
        
        Response.Redirect(url);
    }
    
    protected void LIM_LibrettiImpianti_btnAnnullaLibrettoImpianto_Click(object sender, EventArgs e)
    {
        UtilityLibrettiImpianti.AnnullaLibretto(LibrettoImpiantoCorrente.IDLibrettoImpianto);
        
        QueryString qs = new QueryString();
        qs.Add("IDLibrettoImpianto", LibrettoImpiantoCorrente.IDLibrettoImpianto.ToString());
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "LIM_LibrettiImpianti.aspx";
        url += qsEncrypted.ToString();

        Response.Redirect(url);
    }
    
    protected void LIM_LibrettiImpianti_btnNuovoRapportoControllo_Click(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["fEncryptQueryString"] == "on")
        {
            QueryString qs = new QueryString();
            qs.Add("IDRapportoControlloTecnico", "");
            qs.Add("IDTipologiaRCT", "");
            qs.Add("IDSoggetto", lblIDSoggetto.Text);
            qs.Add("IDSoggettoDerived", lblIDSoggettoDerived.Text);
            qs.Add("codiceTargaturaImpianto", lblCodiceTargatura.Text);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "RCT_RapportoDiControlloTecnicoNuovo.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
    }

    protected void LIM_LibrettiImpianti_btnModifica_Click(object sender, EventArgs e)
    {
        QueryString qs = new QueryString();
        qs.Add("IDLibrettoImpianto", LibrettoImpiantoCorrente.IDLibrettoImpianto.ToString());
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "LIM_LibrettiImpiantiModifica.aspx";
        url += qsEncrypted.ToString();

        Response.Redirect(url);
    }

    protected void btnUpdComuneLibretto_Click(object sender, EventArgs e)
    {
        RadComboBoxCodiciCatastali.Visible = true;
        lblCodiceCatastale.Visible = false;
        btnUpdComuneLibretto.Visible = false;
        btnAnnullaUpdComuneLibretto.Visible = true;
    }

    protected void btnAnnullaUpdComuneLibretto_Click(object sender, EventArgs e)
    {
        RadComboBoxCodiciCatastali.Visible = false;
        lblCodiceCatastale.Visible = true;
        btnUpdComuneLibretto.Visible = true;
        btnAnnullaUpdComuneLibretto.Visible = false;
        btnSaveComuneLibretto.Visible = false;
        RefreshComboComuneLibretto();

    }

    protected void btnSaveComuneLibretto_Click(object sender, EventArgs e)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            int iDLibrettoImpianto = int.Parse(IDLibrettoImpianto);

            var ComuneLibretto = ctx.LIM_LibrettiImpianti.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto).FirstOrDefault();
            
            ComuneLibretto.IDCodiceCatastale = int.Parse(RadComboBoxCodiciCatastali.SelectedItem.Value.ToString());
            ctx.SaveChanges();
            
            lblCodiceCatastale.Text = ComuneLibretto.SYS_CodiciCatastali.CodiceCatastale + " - " + ComuneLibretto.SYS_CodiciCatastali.Comune;
            lblIDCodiceCatastale.Text = RadComboBoxCodiciCatastali.SelectedItem.Value.ToString();

            if (!string.IsNullOrEmpty(ComuneLibretto.SYS_CodiciCatastali.SYS_Province.Provincia))
            {
                lblProvincia.Text = ComuneLibretto.SYS_CodiciCatastali.SYS_Province.Provincia;
            }
        }

        btnAnnullaUpdComuneLibretto.Visible = false;
        RadComboBoxCodiciCatastali.Visible = false;
        lblCodiceCatastale.Visible = true;
        btnUpdComuneLibretto.Visible = true;
        btnSaveComuneLibretto.Visible = false;
    }

    protected void RadComboBoxCodiciCatastali_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadComboBoxCodiciCatastali.Visible = true;
        
        if(RadComboBoxCodiciCatastali.SelectedIndex != -1)
        {
            btnSaveComuneLibretto.Visible = true;
        }
        else
        {
            btnSaveComuneLibretto.Visible = false;
        }
    }

}
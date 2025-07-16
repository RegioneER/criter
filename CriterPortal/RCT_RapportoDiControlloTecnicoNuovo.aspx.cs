using DataLayer;
using DataUtilityCore;
using DataUtilityCore.Enum;
using DevExpress.Web;
using EncryptionQS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RCT_RapportoDiControlloTecnicoNuovo : System.Web.UI.Page
{
    protected string IDRapportoControlloTecnico
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

    protected string IDTipologiaRct
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

    protected string IDSoggettoDerived
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

    public string codiceTargaturaImpianto
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
                            return (string)qsdec[4];
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

    private readonly CriterDataModel ctx = new CriterDataModel();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (cmbAziende.Value != null && cmbAddetti.Value != null)
        {
            UCBolliniSelector.IDSoggetto = cmbAziende.Value.ToString();
            UCBolliniSelector.IDSoggettoDerived = cmbAddetti.Value.ToString();
        }
        
        if (!IsPostBack)
        {
            PagePermission();
            FixLottiDuplicati();
            AutomaticRCTFromLibretto(codiceTargaturaImpianto);
            if (!string.IsNullOrEmpty(IDRapportoControlloTecnico))
            {
                int iDRapportoControlloTecnico = int.Parse(IDRapportoControlloTecnico);
                var rapporto = ctx.RCT_RapportoDiControlloTecnicoBase.Find(iDRapportoControlloTecnico);

                QueryString qs = new QueryString();
                qs.Add("IDRapportoControlloTecnico", IDRapportoControlloTecnico);
                qs.Add("IDTipologiaRCT", IDTipologiaRct);
                qs.Add("IDSoggetto", IDSoggetto);
                qs.Add("IDSoggettoDerived", IDSoggettoDerived);
                QueryString qsEncrypted = Encryption.EncryptQueryString(qs);
                string url = "RCT_RapportoDiControlloTecnico.aspx";

                url += qsEncrypted.ToString();
                Response.Redirect(url);
            }
        }
    }

    protected void FixLottiDuplicati()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();
        string iDSoggetto = null;

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                
                break;
            case "2": //Amministratore azienda
                iDSoggetto = getVal[0].ToString();
                break;
            case "3": //Operatore/Addetto
            case "10": //Responsabile tecnico
                iDSoggetto = getVal[2].ToString();
                break;
        }

        //TODO: In futuro chiamare //Portafoglio.FixLottiDuplicati(iDSoggetto);
        if (!string.IsNullOrEmpty(iDSoggetto))
        {
            bool fFixedLotto = FixLottiDuplicati(iDSoggetto);
            if (!fFixedLotto)
            {
                btnRicerca.Visible = false;
                lblLottoDuplicatoBloccoRct.Visible = true;
                lblLottoDuplicatoBloccoRct.Text = "Attenzione: non è possibile inserire un nuovo RCT in quanto è presente una anomalia sul portafoglio contattare l'assistenza!";
            }
        }
    }

    public static bool FixLottiDuplicati(string iDSoggetto)
    {
        bool fFixedLotto = true;

        List<int?> lottiList = new List<int?>();

        string sqlSelect = "SELECT * FROM [dbo].[RCT_LottiBolliniCalorePulito] "
                           + " WHERE "
                           + " IdLottobolliniCalorePulito NOT IN (SELECT IdLottobolliniCalorePulito FROM [dbo].[COM_RigaPortafoglio] WHERE IdLottoBollinicalorePulito IS NOT NULL) "
                           + " AND YEAR(DataAcquisto)='" + DateTime.Now.Year + "' AND IDSoggetto=" + iDSoggetto;

        SqlDataReader dr = UtilityApp.GetDR(sqlSelect);
        while (dr.Read())
        {
            if (dr["IdLottobolliniCalorePulito"] != null)
            {
                lottiList.Add(int.Parse(dr["IdLottobolliniCalorePulito"].ToString()));
            }
        }

        //Se nei movimenti del soggetto esiste un lotto non presente allora devo cancellare questo lotto
        using (var ctx = new CriterDataModel())
        {
            if (lottiList.Count > 0)
            {
                foreach (var lotto in lottiList)
                {
                    if (lotto.HasValue)
                    {
                        try
                        {
                            //Cancello i bollini e il lotto non collegati alla riga del portafoglio del soggetto
                            var bolliniDaCancellare = ctx.RCT_BollinoCalorePulito.Where(c => c.IdLottoBolliniCalorePulito == lotto.Value && c.IDRapportoControlloTecnico == null).ToList();
                            ctx.RCT_BollinoCalorePulito.RemoveRange(bolliniDaCancellare);
                            ctx.SaveChanges();

                            //if (bolliniDaCancellare.Count > 0)
                            //{
                                ctx.RCT_LottiBolliniCalorePulito.RemoveRange(ctx.RCT_LottiBolliniCalorePulito.Where(c => c.IdLottobolliniCalorePulito == lotto.Value));
                                ctx.SaveChanges();
                            //}

                        }
                        catch (Exception ex)
                        {
                            //Se va in eccezione significa che c'è un lotto duplicato con già agganciati degli RCT, quindi per sicurezza blocco l'inserimento del nuovo RCT
                            fFixedLotto = false;
                            break;
                        }
                    }
                }
            }
        }

        return fFixedLotto;
    }


    protected void AutomaticRCTFromLibretto(string codice)
    {
        if (!string.IsNullOrEmpty(codice))
        {
            //cmbAziende.Value = int.Parse(IDSoggetto);

            //GetComboBoxFilterByIDAzienda();
            //cmbAddetti.Value = IDSoggettoDerived;
            txtCodiceTargatura.Text = codice;
            //GetTipiGruppiGeneratori(codice);
        }
    }

    #region  Azienda/Operatore
    protected void ASPxComboBox_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox) source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "2", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox) source;
        int IdSoggetto = int.Parse(e.Value.ToString());
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "2", e.Value.ToString(), "");
        comboBox.DataBind();
    }

    protected void ASPxComboBox_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetComboBoxFilterByIDAzienda();
    }

    protected void ASPxComboBox1_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBox();
    }

    protected void GetComboBoxFilterByIDAzienda()
    {
        if (cmbAziende.Value != null)
        {
            cmbAddetti.Text = "";
            cmbAddetti.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", cmbAziende.Value.ToString(), string.Format("%{0}%", ""), (1).ToString(), (100 + 1).ToString());
            cmbAddetti.DataBind();
        }
    }

    protected void RefreshAspxComboBox()
    {
        cmbAziende.SelectedIndex = -1;
        cmbAziende.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "2", "", "", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
        cmbAziende.DataBind();
        cmbAddetti.SelectedIndex = -1;
        cmbAddetti.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", "0", string.Format("%{0}%", ""), (1).ToString(), (100 + 1).ToString());
        cmbAddetti.DataBind();
    }

    protected void ASPxComboBox2_OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion

    #region Custom Controll
    public void ControllaBolliniSelezionatiInteroImpianto(Object sender, ServerValidateEventArgs e)
    {
        bool fInteroImpianto = rblTipoRapportoInteroImpianto.SelectedIndex == 1;
        if (fInteroImpianto)
        {
            decimal importoSelezionato = UCBolliniSelector.ImportoTotaleSelezionati;
            decimal importoRichiesto = decimal.Parse(lblImportoRichiesto.Text);

            if (importoRichiesto == importoSelezionato)
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaGeneratoriSelezionati(Object sender, ServerValidateEventArgs e)
    {
        #region GT
        int counterGT = 0;
        for (int i = 0; i <= gridGT.Items.Count - 1; i++)
        {
            DataGridItem item = gridGT.Items[i];
            CheckBox chk = (CheckBox)item.Cells[5].FindControl("chkSelezione");

            if (chk.Checked)
            {
                counterGT++;
            }
        }
        #endregion

        #region GF
        int counterGF = 0;
        for (int i = 0; i <= gridGF.Items.Count - 1; i++)
        {
            DataGridItem item = gridGF.Items[i];
            CheckBox chk = (CheckBox)item.Cells[5].FindControl("chkSelezione");

            if (chk.Checked)
            {
                counterGF++;
            }
        }
        #endregion

        #region SC
        int counterSC = 0;
        for (int i = 0; i <= gridSC.Items.Count - 1; i++)
        {
            DataGridItem item = gridSC.Items[i];
            CheckBox chk = (CheckBox)item.Cells[5].FindControl("chkSelezione");

            if (chk.Checked)
            {
                counterSC++;
            }
        }
        #endregion

        #region CG
        int counterCG = 0;
        for (int i = 0; i <= gridCG.Items.Count - 1; i++)
        {
            DataGridItem item = gridCG.Items[i];
            CheckBox chk = (CheckBox)item.Cells[5].FindControl("chkSelezione");

            if (chk.Checked)
            {
                counterCG++;
            }
        }
        #endregion

        if (counterGT > 0 || counterGF > 0 || counterSC > 0 || counterCG > 0)
        {
            e.IsValid = true;
        }
        else
        {
            e.IsValid = false;
        }
    }

    #endregion

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                cmbAziende.Visible = true;
                cmbAddetti.Visible = true;
                lblSoggetto.Visible = false;
                lblSoggettoDerived.Visible = false;
                break;
            case "2": //Amministratore azienda
                cmbAziende.Value = getVal[0];
                cmbAziende.Visible = false;
                cmbAddetti.Visible = true;
                GetComboBoxFilterByIDAzienda();

                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0]);
                lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = false;

                //Controllo che ha firmato il documento sulla privacy altrimenti non gli consento l'inserimento di un nuovo RCT
                int iDSoggetto = int.Parse(getVal[0]);
                bool fBloccaNuovoRct = UtilitySoggetti.fPrivacyBloccaNuovoRct(iDSoggetto, "Impresa");
                if (fBloccaNuovoRct)
                {
                    btnRicerca.Visible = false;
                    lblPrivacyBloccoRct.Visible = true;
                    lnkPrivacyLink.Visible = true;
                    lblPrivacyBloccoRct.Text = "Attenzione: non è possibile inserire un nuovo RCT in quanto non è stato ancora firmato il documento DESIGNAZIONE RESPONSABILE ESTERNO DEL TRATTAMENTO DEI DATI PERSONALI EX ART. 28 REG. UE 2016/679 NELL’AMBITO DELLA GESTIONE DEL CATASTO REGIONALE E DEL SISTEMA DI CONTROLLO DEGLI IMPIANTI TERMICI (CRITER). Procedere prima alla firma cliccando &nbsp;";
                }
                //////
                break;
            case "3": //Operatore/Addetto
                cmbAziende.Visible = false;
                cmbAddetti.Visible = false;
                lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = true;
                cmbAziende.Value = getVal[2];
                cmbAddetti.Value = getVal[0];
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2]);
                lblSoggettoDerived.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0]);
                
                //Controllo che ha firmato il documento sulla privacy altrimenti non gli consento l'inserimento di un nuovo RCT
                int iDSoggettoOperatore = int.Parse(getVal[0]);
                bool fBloccaNuovoRctOperatore = UtilitySoggetti.fPrivacyBloccaNuovoRct(iDSoggettoOperatore, "Operatore");
                if (fBloccaNuovoRctOperatore)
                {
                    btnRicerca.Visible = false;
                    lblPrivacyBloccoRct.Visible = true;
                    lnkPrivacyLink.Visible = false;
                    lblPrivacyBloccoRct.Text = "Attenzione: non è possibile inserire un nuovo RCT in quanto non è stato ancora firmato il documento DESIGNAZIONE RESPONSABILE ESTERNO DEL TRATTAMENTO DEI DATI PERSONALI EX ART. 28 REG. UE 2016/679 NELL’AMBITO DELLA GESTIONE DEL CATASTO REGIONALE E DEL SISTEMA DI CONTROLLO DEGLI IMPIANTI TERMICI (CRITER). E' necessario che l'utente Impresa proceda con la visione e firma del documento";
                }
                break;
            case "10": //Responsabile tecnico
                cmbAziende.Value = getVal[2];
                cmbAziende.Visible = false;
                cmbAddetti.Visible = true;
                GetComboBoxFilterByIDAzienda();

                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2]);
                lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = false;

                //Controllo che ha firmato il documento sulla privacy altrimenti non gli consento l'inserimento di un nuovo RCT
                int iDSoggettoResponsabile = int.Parse(getVal[0]);
                bool fBloccaNuovoRctResponsabile = UtilitySoggetti.fPrivacyBloccaNuovoRct(iDSoggettoResponsabile, "Operatore");
                if (fBloccaNuovoRctResponsabile)
                {
                    btnRicerca.Visible = false;
                    lblPrivacyBloccoRct.Visible = true;
                    lnkPrivacyLink.Visible = false;
                    lblPrivacyBloccoRct.Text = "Attenzione: non è possibile inserire un nuovo RCT in quanto non è stato ancora firmato il documento DESIGNAZIONE RESPONSABILE ESTERNO DEL TRATTAMENTO DEI DATI PERSONALI EX ART. 28 REG. UE 2016/679 NELL’AMBITO DELLA GESTIONE DEL CATASTO REGIONALE E DEL SISTEMA DI CONTROLLO DEGLI IMPIANTI TERMICI (CRITER). E' necessario che l'utente Impresa proceda con la visione e firma del documento";
                }
                break;
        }
    }

    public void GetTipiGruppiGeneratori(string codiceTargatura)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            int? iDTargatura = null;

            var targatura = (from LIM_TargatureImpianti in ctx.LIM_TargatureImpianti
                             where
                               LIM_TargatureImpianti.CodiceTargatura == codiceTargatura
                             select new
                             {
                                 LIM_TargatureImpianti.IDTargaturaImpianto
                             }).FirstOrDefault();

            if (targatura != null)
            {
                iDTargatura = targatura.IDTargaturaImpianto;
            }

            var libretto = ctx.LIM_LibrettiImpianti.Where(c => c.fAttivo == true && c.IDStatoLibrettoImpianto == 2 && c.IDTargaturaImpianto == iDTargatura);

            //var libretto = ctx.LIM_LibrettiImpianti.Where(c => c.fAttivo == true && c.IDStatoLibrettoImpianto == 2).AsQueryable();
            //if (!string.IsNullOrEmpty(codiceTargatura))
            //{
            //    libretto = libretto.Where(c => c.LIM_TargatureImpianti.CodiceTargatura == codiceTargatura);
            //}

            var dsLibretto = libretto.ToList();

            bool fBloccoRct = false;
            int? iDLibrettoImpianto = null;
            int? iDCodiceCatastale = null;
            if (dsLibretto.Count > 0)
            {
                iDLibrettoImpianto = libretto.FirstOrDefault().IDLibrettoImpianto;
                iDCodiceCatastale = libretto.FirstOrDefault().IDCodiceCatastale;

                fBloccoRct = VerificaBloccoRct(iDCodiceCatastale);

                rowNoResult.Visible = false;
            }

            if (!fBloccoRct)
            {
                #region Rct from Libretto
                if (iDLibrettoImpianto != null)
                {
                    #region GT
                    var gt = ctx.LIM_LibrettiImpiantiGruppiTermici.Select(c => new
                    {
                        Tipologia = c.SYS_TipologiaGruppiTermici.TipologiaGruppiTermici,
                        DataInstallazione = c.DataInstallazione,
                        Fabbricante = c.Fabbricante,
                        Modello = c.Modello,
                        Matricola = c.Matricola,
                        AnalisiFumoPrevisteNr = c.AnalisiFumoPrevisteNr,
                        IDLibrettoImpiantoGruppoTermico = c.IDLibrettoImpiantoGruppoTermico,
                        IDLibrettoImpianto = c.IDLibrettoImpianto,
                        Prefisso = c.Prefisso,
                        CodiceProgressivo = c.CodiceProgressivo,
                        fAttivo = c.fAttivo,
                        fDismesso = c.fDismesso,
                        PotenzaTermicaUtileNominaleKw = c.PotenzaTermicaUtileNominaleKw,
                        Combustibile = c.SYS_TipologiaCombustibile.TipologiaCombustibile,
                        IDTipologiaCombustibile = c.IDTipologiaCombustibile

                    }).AsQueryable();

                    gt = gt.Where(c => c.fAttivo == true);
                    gt = gt.Where(c => c.fDismesso == false);
                    gt = gt.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto);

                    var dsGT = gt.ToList();
                    if (dsGT.Count > 0)
                    {
                        gridGT.DataSource = dsGT;
                        gridGT.DataBind();
                        rowGT.Visible = true;
                        if (dsGT.Count > 1)
                        {
                            rowInteroImpianto.Visible = true;
                        }
                        else
                        {
                            rowInteroImpianto.Visible = false;
                        }
                    }
                    else
                    {
                        rowGT.Visible = false;
                        rowInteroImpianto.Visible = false;
                    }
                    #endregion

                    #region GF
                    var gf = ctx.LIM_LibrettiImpiantiMacchineFrigorifere.Select(c => new
                    {
                        IDLibrettoImpiantoMacchinaFrigorifera = c.IDLibrettoImpiantoMacchinaFrigorifera,
                        Tipologia = c.SYS_TipologiaMacchineFrigorifere.TipologiaMacchineFrigorifere,
                        NumCircuiti = c.NumCircuiti,
                        DataInstallazione = c.DataInstallazione,
                        Fabbricante = c.Fabbricante,
                        Modello = c.Modello,
                        Matricola = c.Matricola,
                        IDLibrettoImpianto = c.IDLibrettoImpianto,
                        Prefisso = c.Prefisso,
                        CodiceProgressivo = c.CodiceProgressivo,
                        fAttivo = c.fAttivo,
                        fDismesso = c.fDismesso

                    }).AsQueryable();

                    gf = gf.Where(c => c.fAttivo == true);
                    gf = gf.Where(c => c.fDismesso == false);
                    gf = gf.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto);

                    var dsGF = gf.ToList();
                    if (dsGF.Count > 0)
                    {
                        gridGF.DataSource = dsGF;
                        gridGF.DataBind();
                        rowGF.Visible = true;
                    }
                    else
                    {
                        rowGF.Visible = false;
                    }
                    #endregion

                    #region SC
                    var sc = ctx.LIM_LibrettiImpiantiScambiatoriCalore.Select(c => new
                    {
                        IDLibrettoImpiantoScambiatoreCalore = c.IDLibrettoImpiantoScambiatoreCalore,
                        DataInstallazione = c.DataInstallazione,
                        Fabbricante = c.Fabbricante,
                        Modello = c.Modello,
                        Matricola = c.Matricola,
                        IDLibrettoImpianto = c.IDLibrettoImpianto,
                        Prefisso = c.Prefisso,
                        CodiceProgressivo = c.CodiceProgressivo,
                        fAttivo = c.fAttivo,
                        fDismesso = c.fDismesso

                    }).AsQueryable();

                    sc = sc.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto);
                    sc = sc.Where(c => c.fAttivo == true);
                    sc = sc.Where(c => c.fDismesso == false);

                    var dsSC = sc.ToList();
                    if (dsSC.Count > 0)
                    {
                        gridSC.DataSource = dsSC;
                        gridSC.DataBind();
                        rowSC.Visible = true;
                    }
                    else
                    {
                        rowSC.Visible = false;
                    }
                    #endregion

                    #region CG
                    var cg = ctx.LIM_LibrettiImpiantiCogeneratori.Select(c => new
                    {
                        IDLibrettoImpiantoCogeneratore = c.IDLibrettoImpiantoCogeneratore,
                        Tipologia = c.SYS_TipologiaCogeneratore.TipologiaCogeneratore,
                        DataInstallazione = c.DataInstallazione,
                        Fabbricante = c.Fabbricante,
                        Modello = c.Modello,
                        Matricola = c.Matricola,
                        IDLibrettoImpianto = c.IDLibrettoImpianto,
                        Prefisso = c.Prefisso,
                        CodiceProgressivo = c.CodiceProgressivo,
                        fAttivo = c.fAttivo,
                        fDismesso = c.fDismesso

                    }).AsQueryable();

                    cg = cg.Where(c => c.IDLibrettoImpianto == iDLibrettoImpianto);
                    cg = cg.Where(c => c.fAttivo == true);
                    cg = cg.Where(c => c.fDismesso == false);

                    var dsCG = cg.ToList();
                    if (dsCG.Count > 0)
                    {
                        gridCG.DataSource = dsCG;
                        gridCG.DataBind();
                        rowCG.Visible = true;
                    }
                    else
                    {
                        rowCG.Visible = false;
                    }
                    #endregion

                    if ((dsGT.Count > 0) || (dsGF.Count > 0) || (dsSC.Count > 0) || (dsCG.Count > 0))
                    {
                        rowConfermaInserimento.Visible = true;
                        //txtCodiceTargatura.Enabled = false;
                        //btnRicerca.Enabled = false;
                    }
                    else
                    {
                        rowConfermaInserimento.Visible = false;
                        //txtCodiceTargatura.Enabled = true;
                        //btnRicerca.Enabled = true;
                    }

                    if ((dsGT.Count == 1) && (dsGF.Count == 0) && (dsSC.Count == 0) && (dsCG.Count == 0))
                    {
                        CheckBox chk = (CheckBox) gridGT.Items[0].Cells[5].FindControl("chkSelezione");
                        chk.Checked = true;
                    }
                    if ((dsGT.Count == 0) && (dsGF.Count == 1) && (dsSC.Count == 0) && (dsCG.Count == 0))
                    {
                        CheckBox chk = (CheckBox) gridGF.Items[0].Cells[5].FindControl("chkSelezione");
                        chk.Checked = true;
                    }
                    if ((dsGT.Count == 0) && (dsGF.Count == 0) && (dsSC.Count == 1) && (dsCG.Count == 0))
                    {
                        CheckBox chk = (CheckBox) gridSC.Items[0].Cells[5].FindControl("chkSelezione");
                        chk.Checked = true;
                    }
                    if ((dsGT.Count == 0) && (dsGF.Count == 0) && (dsSC.Count == 0) && (dsCG.Count == 1))
                    {
                        CheckBox chk = (CheckBox) gridCG.Items[0].Cells[5].FindControl("chkSelezione");
                        chk.Checked = true;
                    }
                }
                else
                {
                    rowNoResult.Visible = true;
                }
                #endregion
            }
        }
    }

    public bool VerificaBloccoRct(int? iDCodiceCatastale)
    {
        bool fBloccoRct = false;
        var codiciCatastali = LoadDropDownList.LoadDropDownList_V_SYS_CodiciCatastali(iDCodiceCatastale).FirstOrDefault();
        if (codiciCatastali.DataTermineBloccoRct != null)
        {
            if (codiciCatastali.DataTermineBloccoRct >= DateTime.Now)
            {
                fBloccoRct = true;
                string message = "ATTENZIONE: il sistema CRITER non permette di effettuare la registrazione del tuo Rapporto di controllo tecnico relativo all'impianto termico in quanto il comune di " + codiciCatastali.Comune + " ha attivato una campagna di accertamento ed ispezione fino al <b>" + String.Format("{0:dd/MM/yyyy}", codiciCatastali.DataTermineBloccoRct) + "</b>. ";
                message += "L'invio dei Rapporti di controllo deve essere effettuato presso l'ente competente di riferimento secondo le modalità stabilite da quest'ultimo.";
                lblBloccoRctMessage.Text = message;
                dmpBloccoRct.Enabled = true;
                dmpBloccoRct.Show();
            }
            else
            {
                fBloccoRct = false;
                dmpBloccoRct.Enabled = false;
                dmpBloccoRct.Hide();
            }
        }
        else
        {
            fBloccoRct = false;
            dmpBloccoRct.Enabled = false;
            dmpBloccoRct.Hide();
        }

        return fBloccoRct;
    }

    protected void btnRicerca_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetTipiGruppiGeneratori(txtCodiceTargatura.Text);
        }
    }

    protected void btnBloccoRctConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RCT_RapportoDiControlloTecnicoSearch.aspx");
    }

    protected void btnNuoviRapportiControllo_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                bool fInteroImpianto = rblTipoRapportoInteroImpianto.SelectedIndex == 1;
                object guidInteroImpianto = null;
                if (fInteroImpianto)
                {
                    guidInteroImpianto = Guid.NewGuid().ToString("N");
                }
                
                int iDSoggetto = int.Parse(cmbAziende.Value.ToString());
                int iDSoggettoDerived = int.Parse(cmbAddetti.Value.ToString());

                rowErroriRapportiControlloTecnico.Visible = false;
                #region Inserimento Rapporti di Controllo
                List<long> valuesRapporti = new List<long>();

                #region Inserimento GT
                for (int i = 0; i <= gridGT.Items.Count - 1; i++)
                {
                    DataGridItem elemento = gridGT.Items[i];
                    int iDLibrettoImpiantoGruppoTermico = int.Parse(elemento.Cells[0].Text);
                    int iDLibrettoImpianto = int.Parse(elemento.Cells[1].Text);
                    string prefisso = elemento.Cells[2].Text;
                    int codiceProgressivo = int.Parse(elemento.Cells[3].Text);

                    CheckBox chk = (CheckBox)elemento.Cells[5].FindControl("chkSelezione");

                    if (chk.Checked)
                    {
                        RCT_RapportoDiControlloTecnicoBase rapporto = new RCT_RapportoDiControlloTecnicoBase();
                        RCT_RapportoDiControlloTecnicoGT rapportoGT = new RCT_RapportoDiControlloTecnicoGT();
                        UtilityRapportiControllo.AutocompilatoreRapportoGT(iDSoggetto, iDSoggettoDerived, rapporto, rapportoGT, iDLibrettoImpianto, prefisso, codiceProgressivo, iDLibrettoImpiantoGruppoTermico, 1, guidInteroImpianto);

                        ctx.RCT_RapportoDiControlloTecnicoBase.Add(rapporto);
                        ctx.SaveChanges();
                        
                        rapportoGT.Id = rapporto.IDRapportoControlloTecnico;
                        ctx.RCT_RapportoDiControlloTecnicoGT.Add(rapportoGT);
                        ctx.SaveChanges();

                        valuesRapporti.Add(rapportoGT.Id);
                    }
                }
                
                if (fInteroImpianto)
                {
                    decimal importoBolliniDaUtilizzare = decimal.Parse(lblImportoRichiesto.Text);
                    var valuesBolliniSelezionati = UCBolliniSelector.SelectedValues.Distinct().ToList();
                    UtilityBollini.AssegnaBolliniInteroImpiantoSuRct(iDSoggetto, 
                                                                     iDSoggettoDerived, 
                                                                     valuesRapporti,
                                                                     valuesBolliniSelezionati);

                    var dataControllo = (DateTime)UtilityApp.CheckValidDatetimeWithMinValue(txtDataControllo.Text);
                    foreach (var iDRapporto in valuesRapporti)
                    {
                        UtilityRapportiControllo.SetDataControlloInteroImpianto(iDRapporto, dataControllo);
                    }
                }
                #endregion

                #region Inserimento GF
                for (int i = 0; i <= gridGF.Items.Count - 1; i++)
                {
                    DataGridItem elemento = gridGF.Items[i];
                    int iDLibrettoImpiantoMacchinaFrigorifera = int.Parse(elemento.Cells[0].Text);
                    int iDLibrettoImpianto = int.Parse(elemento.Cells[1].Text);
                    string prefisso = elemento.Cells[2].Text;
                    int codiceProgressivo = int.Parse(elemento.Cells[3].Text);

                    CheckBox chk = (CheckBox)elemento.Cells[5].FindControl("chkSelezione");

                    if (chk.Checked)
                    {
                        RCT_RapportoDiControlloTecnicoBase rapporto = new RCT_RapportoDiControlloTecnicoBase();
                        RCT_RapportoDiControlloTecnicoGF rapportoGF = new RCT_RapportoDiControlloTecnicoGF();
                        UtilityRapportiControllo.AutocompilatoreRapportoGF(iDSoggetto, iDSoggettoDerived, rapporto, rapportoGF, iDLibrettoImpianto, prefisso, codiceProgressivo, iDLibrettoImpiantoMacchinaFrigorifera, 2, null);

                        ctx.RCT_RapportoDiControlloTecnicoBase.Add(rapporto);
                        try
                        {
                            ctx.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            
                        }

                        rapportoGF.Id = rapporto.IDRapportoControlloTecnico;
                        ctx.RCT_RapportoDiControlloTecnicoGF.Add(rapportoGF);
                        ctx.SaveChanges();
                        valuesRapporti.Add(rapportoGF.Id);
                    }
                }
                #endregion

                #region Inserimento SC
                for (int i = 0; i <= gridSC.Items.Count - 1; i++)
                {
                    DataGridItem elemento = gridSC.Items[i];
                    int iDLibrettoImpiantoScambiatoreCalore = int.Parse(elemento.Cells[0].Text);
                    int iDLibrettoImpianto = int.Parse(elemento.Cells[1].Text);
                    string prefisso = elemento.Cells[2].Text;
                    int codiceProgressivo = int.Parse(elemento.Cells[3].Text);

                    CheckBox chk = (CheckBox)elemento.Cells[5].FindControl("chkSelezione");

                    if (chk.Checked)
                    {
                        RCT_RapportoDiControlloTecnicoBase rapporto = new RCT_RapportoDiControlloTecnicoBase();
                        RCT_RapportoDiControlloTecnicoSC rapportoSC = new RCT_RapportoDiControlloTecnicoSC();
                        UtilityRapportiControllo.AutocompilatoreRapportoSC(iDSoggetto, iDSoggettoDerived, rapporto, rapportoSC, iDLibrettoImpianto, prefisso, codiceProgressivo, iDLibrettoImpiantoScambiatoreCalore, 3, null);

                        ctx.RCT_RapportoDiControlloTecnicoBase.Add(rapporto);
                        try
                        {
                            ctx.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            
                        }

                        rapportoSC.Id = rapporto.IDRapportoControlloTecnico;
                        ctx.RCT_RapportoDiControlloTecnicoSC.Add(rapportoSC);
                        ctx.SaveChanges();
                        valuesRapporti.Add(rapportoSC.Id);
                    }
                }
                #endregion

                #region Inserimento CG
                for (int i = 0; i <= gridCG.Items.Count - 1; i++)
                {
                    DataGridItem elemento = gridCG.Items[i];
                    int iDLibrettoImpiantoCogeneratore = int.Parse(elemento.Cells[0].Text);
                    int iDLibrettoImpianto = int.Parse(elemento.Cells[1].Text);
                    string prefisso = elemento.Cells[2].Text;
                    int codiceProgressivo = int.Parse(elemento.Cells[3].Text);

                    CheckBox chk = (CheckBox)elemento.Cells[5].FindControl("chkSelezione");

                    if (chk.Checked)
                    {
                        RCT_RapportoDiControlloTecnicoBase rapporto = new RCT_RapportoDiControlloTecnicoBase();
                        RCT_RapportoDiControlloTecnicoCG rapportoCG = new RCT_RapportoDiControlloTecnicoCG();
                        UtilityRapportiControllo.AutocompilatoreRapportoCG(iDSoggetto, iDSoggettoDerived, rapporto, rapportoCG, iDLibrettoImpianto, prefisso, codiceProgressivo, iDLibrettoImpiantoCogeneratore, 4, null);

                        ctx.RCT_RapportoDiControlloTecnicoBase.Add(rapporto);
                        try
                        {
                            ctx.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            
                        }

                        rapportoCG.Id = rapporto.IDRapportoControlloTecnico;
                        ctx.RCT_RapportoDiControlloTecnicoCG.Add(rapportoCG);
                        ctx.SaveChanges();
                        valuesRapporti.Add(rapportoCG.Id);
                    }
                }
                #endregion

                string listIDRapportiControlloTecnici = string.Empty;
                foreach (var item in valuesRapporti)
                {
                    listIDRapportiControlloTecnici += item.ToString() + ",";
                }

                if (listIDRapportiControlloTecnici.Length > 0)
                {
                    listIDRapportiControlloTecnici = listIDRapportiControlloTecnici.Substring(0, listIDRapportiControlloTecnici.Length - 1);
                }

                QueryString qs = new QueryString();
                qs.Add("listIDRapportiControlloTecnici", listIDRapportiControlloTecnici);
                QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                string url = "RCT_RapportoDiControlloTecnicoSearch.aspx";
                url += qsEncrypted.ToString();
                Response.Redirect(url);
                #endregion
            }
            catch (Exception ex)
            {
                rowErroriRapportiControlloTecnico.Visible = true;
                lblErrors.Text = "ATTENZIONE: Si è verificato un errore durante l'inserimento dei Rapporti di Controllo selezionati. L'errore è causato dall'assenza di alcune informazioni necessarie nei generatori del Libretto. Controllare il libretto e completare i dati del generatore con i dati assenti.<br/>" + ex.Message;
            }   
        }
    }

    protected void rblTipoRapportoInteroImpianto_SelectedIndexChanged(object sender, EventArgs e)
    {
        LogicInteroImpianto();
    }

    #region INTERO IMPIANTO

    public void LogicInteroImpianto()
    {
        bool fInteroImpianto = rblTipoRapportoInteroImpianto.SelectedIndex == 1;

        #region Selezione della Grid con tutti i generatori
        List<long> valuesGeneratori = new List<long>();
        for (int i = 0; i <= gridGT.Items.Count - 1; i++)
        {
            DataGridItem elemento = gridGT.Items[i];
            CheckBox chk = (CheckBox)elemento.Cells[5].FindControl("chkSelezione");

            int iDLibrettoImpiantoGruppoTermico = int.Parse(elemento.Cells[0].Text);
            int iDTipologiaCombustibile = int.Parse(elemento.Cells[6].Text);

            if (fInteroImpianto)
            {
                if (iDTipologiaCombustibile == 2 ||
                    iDTipologiaCombustibile == 3 ||
                    iDTipologiaCombustibile == 4 ||
                    iDTipologiaCombustibile == 5 ||
                    iDTipologiaCombustibile == 10 ||
                    iDTipologiaCombustibile == 11 ||
                    iDTipologiaCombustibile == 12 ||
                    iDTipologiaCombustibile == 13 ||
                    iDTipologiaCombustibile == 14 ||
                    iDTipologiaCombustibile == 15)
                {
                    chk.Checked = true;
                    chk.Enabled = false;
                    valuesGeneratori.Add(iDLibrettoImpiantoGruppoTermico);
                }
            }
            else
            {
                chk.Checked = false;
                chk.Enabled = true;
            }
        }
        #endregion

        if (fInteroImpianto)
        {
            int iDSoggetto = int.Parse(cmbAziende.Value.ToString());
            int iDSoggettoDerived = int.Parse(cmbAddetti.Value.ToString());

            decimal? potenza = UtilityRapportiControllo.GetPotenzaTermicaUtileNominaleImpiantoFromCodiceTargatura(txtCodiceTargatura.Text);
            var result = UtilityBollini.GetImportoRichiesto(0, potenza.ToString(), txtDataControllo.Text, (int)RCT_TipoRapportoDiControlloTecnico.GT);

            lblTitoloDataControllo.Visible = true;
            txtDataControllo.Visible = true;
            rfvtxtDataControllo.Enabled = true;
            revtxtDataControllo.Enabled = true;
            pnlInteroImpianto.Visible = true;
            lblFeedbackRdp.Text = string.Empty;
            lblFeedbackImportoInsufficiente.Text = string.Empty;

            if (result.Item1)
            {
                //UCBolliniSelector.Visible = true;
                //lblPotenzaImpianto.Visible = true;
                cvControllaBolliniSelezionatiInteroImpianto.Enabled = true;
                lblFeedbackRdp.ForeColor = System.Drawing.Color.Black;
                UCBolliniSelector.Visible = true;

                decimal importoRichiesto = result.Item2;
                List<RCT_BollinoCalorePulito> bolliniUtilizzabili = UtilityBollini.GetBolliniUtilizzabili(iDSoggetto, iDSoggettoDerived, true);
                decimal importoDisponibile = UtilityBollini.GetImportoBollini(bolliniUtilizzabili);

                if (importoDisponibile >= importoRichiesto)
                {
                    lblImportoRichiesto.Text = importoRichiesto.ToString();
                    lblFeedbackRdp.Text = "Potenza intero impianto <b>" + potenza.ToString() + " kw</b> Importo richiesto in bollini Calore Pulito <b>" + importoRichiesto.ToString() + " €</b>";
                    btnNuoviRapportiControllo.Visible = true;                    
                }
                else
                {
                    lblFeedbackImportoInsufficiente.Text = "Attenzione: per redigere i rapporti di controllo sull'intero impianto sono richiesti " + importoRichiesto.ToString() + " € in bollini Calore Pulito ma a questo tecnico sono stati assegnati " + importoDisponibile.ToString() + " € in bollini Calore Pulito. Provvedere ad assegnare bollini al tecnico e/o procedere con l'acquisto.";
                    lblFeedbackImportoInsufficiente.Visible = true;
                    btnNuoviRapportiControllo.Visible = false;
                }
            }
            else
            {
                lblFeedbackRdp.Text = result.Item3;
                UCBolliniSelector.Visible = false;
                UCBolliniSelector.SelectedValues.Clear();
                btnNuoviRapportiControllo.Visible = false;
                lblFeedbackRdp.ForeColor = System.Drawing.Color.Red;
            }            
        }
        else
        {
            lblTitoloDataControllo.Visible = false;
            txtDataControllo.Text = string.Empty;
            txtDataControllo.Visible = false;
            rfvtxtDataControllo.Enabled = false;
            revtxtDataControllo.Enabled = false;
            lblFeedbackRdp.Text = string.Empty;


            pnlInteroImpianto.Visible = false;
            cvControllaBolliniSelezionatiInteroImpianto.Enabled = false;
            //UCBolliniSelector.Visible = false;
            UCBolliniSelector.SelectedValues.Clear();
            //lblImportoInsufficiente.Visible = false;
            //lblPotenzaImpianto.Visible = false;
            btnNuoviRapportiControllo.Visible = true;
        }
    }

    protected void UCBolliniSelector_Selezioneterminata(object sender, EventArgs e)
    {

        //UCBolliniSelector.SelectedValues.Clear();
    }

    #endregion


    protected void txtDataControllo_TextChanged(object sender, EventArgs e)
    {
        LogicInteroImpianto();
    }
}
using DataLayer;
using DataUtilityCore;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.Collections.Generic;
using EncryptionQS;
using System.Configuration;

public partial class VER_ProgrammaAccertamento : System.Web.UI.Page
{
    protected string IDTipoAccertamento
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

    UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PageTipoAccertamento();
            if (Session["RefreshParentPage"] != null)
            {
                if (bool.Parse(Session["RefreshParentPage"].ToString()))
                {
                    GetDatiAll();
                    Session["RefreshParentPage"] = false;
                    pageControl.ActiveTabIndex = 1;
                }
                else
                {
                    ASPxComboBox1.Focus();
                    GetDatiAll();
                    VisibleHiddenLivelloGravitaNC(chkCriticita.Checked);
                    cbLivelloGravitaNC();
                }
            }
            else
            {
                ASPxComboBox1.Focus();
                GetDatiAll();
                VisibleHiddenLivelloGravitaNC(chkCriticita.Checked);
                cbLivelloGravitaNC();
            }
        }
        
        ListaDeiAccertamentiSuProgramma();
    }

    protected void PageTipoAccertamento()
    {
        switch (IDTipoAccertamento)
        {
            case "1": //Accertamento su RCT
                lblTitoloPagina.Text = "GESTIONE PROGRAMMA ACCERTAMENTO SU RCT";
                chkCriticita.Checked = true;
                rowCriticita.Visible = true;
                rowCodiceIspezione.Visible = false;
                rowDataIspezione.Visible = false;
                GridViewListaNelProgramma.Columns[9].Visible = false;
                GridViewListaNelProgramma.Columns[10].Visible = false;
                break;
            case "2": //Accertamento su ispezione
                lblTitoloPagina.Text = "GESTIONE PROGRAMMA ACCERTAMENTO POST ISPEZIONE";
                chkCriticita.Checked = false;
                rowCriticita.Visible = false;
                rowCodiceIspezione.Visible = true;
                rowDataIspezione.Visible = true;
                GridViewListaNelProgramma.Columns[2].Visible = false;
                GridViewListaNelProgramma.Columns[6].Visible = false;
                break;
        }
    }

    protected void cbLivelloGravitaNC()
    {
        string[] gravita = { "1", "2", "3", "4", "5", "6", "7", "8", };

        foreach (string value in gravita)
        {
            ListItem item = new ListItem("Livello di gravità " + value, value);
            cblLivelloGravitaNC.Items.Add(item);
        }
    }

    #region RICERCA ACCERTAMENTI

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

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "2", e.Value.ToString(), "");
        comboBox.DataBind();
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
    }

    #endregion

    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpressionAccertamenti"] == null)
                return string.Empty;
            return ViewState["SortExpressionAccertamenti"].ToString();
        }
        set
        {
            ViewState["SortExpressionAccertamenti"] = value;
        }
    }

    public void DataGrid_Sorting(object sender, DataGridSortCommandEventArgs e)
    {
        this.SortExpression = UtilityApp.CheckSortExpression(e.SortExpression.ToString(), this.SortExpression);
        BindGrid();
    }

    public void BindGrid()
    {
        BindGrid(false);
    }

    public void BindGrid(bool reload)
    {
        string sql = BuildStr();
        string currentSortExpression = this.SortExpression;
        int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, this.DataGrid.CurrentPageIndex, DataGrid.PageSize);
        this.SortExpression = currentSortExpression;

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "ACCERTAMENTI CORRISPONDENTI AI PARAMETRI IMPOSTATI");
    }

    private string BuildStr()
    {
        List<object> valoriCriticita = new List<object>();
        foreach (ListItem item in cblLivelloGravitaNC.Items)
        {
            if (item.Selected)
            {
                valoriCriticita.Add(item.Value);
            }
        }

        string strSql = UtilityVerifiche.GetSqlValoriProgrammaAccertamentiFilter(IDTipoAccertamento, // IDTipoAccertamento - 1: Accertamento su RCT , 2: Accertamento su ispezione
                                                                                 ASPxComboBox1.Value,
                                                                                 1,
                                                                                 chkCriticita.Checked,
                                                                                 txtDataRilevazioneDa.Text,
                                                                                 txtDataRilevazioneAl.Text,
                                                                                 txtCodiceAccertamento.Text,
                                                                                 txtCodiceTargatura.Text,
                                                                                 valoriCriticita.ToArray<object>(),
                                                                                 txtDataIspezioneDa.Text,
                                                                                 txtDataIspezioneAl.Text,
                                                                                 txtCodiceIspezione.Text
                                                                                 );
        return strSql.ToString();
    }

    public void DataGrid_PageChanger(object source, DataGridPageChangedEventArgs e)
    {
        DataGrid.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
    }

    public void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            AjaxControlToolkit.Accordion Accordion4 = (AjaxControlToolkit.Accordion)(e.Item.Cells[6].FindControl("Accordion4"));
            TextBox txtOsservazioni = (TextBox)Accordion4.FindControl("txtOsservazioni");
            txtOsservazioni.Text = e.Item.Cells[14].Text == "&nbsp;" ? "" : e.Item.Cells[14].Text;

            AjaxControlToolkit.Accordion Accordion2 = (AjaxControlToolkit.Accordion)(e.Item.Cells[6].FindControl("Accordion2"));
            TextBox txtRaccomandazioni = (TextBox)Accordion2.FindControl("txtRaccomandazioni");
            txtRaccomandazioni.Text = e.Item.Cells[12].Text == "&nbsp;" ? "" : e.Item.Cells[12].Text;

            AjaxControlToolkit.Accordion Accordion3 = (AjaxControlToolkit.Accordion)(e.Item.Cells[6].FindControl("Accordion3"));
            TextBox txtPrescrizioni = (TextBox)Accordion3.FindControl("txtPrescrizioni");
            txtPrescrizioni.Text = e.Item.Cells[13].Text == "&nbsp;" ? "" : e.Item.Cells[13].Text;

            AjaxControlToolkit.Accordion Accordion1 = (AjaxControlToolkit.Accordion)(e.Item.Cells[6].FindControl("Accordion1"));
            GridView GridNC = (GridView)Accordion1.FindControl("GridNC");
            GridNC.DataSource = DataSourseNC(long.Parse(e.Item.Cells[0].Text));
            GridNC.DataBind();

            #region Link Accertamento
            HyperLink lnkAccertamento = (HyperLink)(e.Item.Cells[6].FindControl("lnkAccertamento"));

            QueryString qs = new QueryString();
            qs.Add("IDAccertamento", e.Item.Cells[0].Text);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "VER_Accertamenti.aspx";
            url += qsEncrypted.ToString();

            lnkAccertamento.NavigateUrl = url;
            lnkAccertamento.Target = "_blank";
            #endregion

            
            Label lblDescrizioneRctIspezione = (Label)(e.Item.Cells[8].FindControl("lblDescrizioneRctIspezione"));
            HyperLink lnkRctIspezione = (HyperLink)(e.Item.Cells[8].FindControl("lnkRctIspezione"));
            Label lblDescrizionePunteggioAccertamento = (Label)(e.Item.Cells[8].FindControl("lblDescrizionePunteggioAccertamento"));
            
            TableRow rowNonConformitaResult = (TableRow)(e.Item.Cells[8].FindControl("rowNonConformitaResult"));
            TableRow rowDatiIspezioneResult = (TableRow)(e.Item.Cells[8].FindControl("rowDatiIspezioneResult"));

            Label lblDescrizioneDataControllo = (Label)(e.Item.Cells[8].FindControl("lblDescrizioneDataControllo"));
            Label lblDataControllo = (Label)(e.Item.Cells[8].FindControl("lblDataControllo"));
            
            switch (IDTipoAccertamento)
            {
                case "1": //Accertamento su RCT
                    #region Link Rct
                    lblDescrizionePunteggioAccertamento.Visible = true;

                    lblDescrizioneRctIspezione.Text = "RTCEE:&nbsp;";
                    lnkRctIspezione.Text = e.Item.Cells[1].Text;

                    QueryString qsRapporto = new QueryString();
                    qsRapporto.Add("IDRapportoControlloTecnico", e.Item.Cells[1].Text);
                    qsRapporto.Add("IDTipologiaRCT", e.Item.Cells[15].Text);
                    qsRapporto.Add("IDSoggetto", e.Item.Cells[2].Text);
                    qsRapporto.Add("IDSoggettoDerived", e.Item.Cells[16].Text);
                    QueryString qsEncryptedRapporto = Encryption.EncryptQueryString(qsRapporto);

                    string urlRapporto = "RCT_RapportoDiControlloTecnico.aspx";
                    urlRapporto += qsEncryptedRapporto.ToString();
                    lnkRctIspezione.NavigateUrl = urlRapporto;
                    lnkRctIspezione.Target = "_blank";
                    lnkRctIspezione.Text = e.Item.Cells[1].Text;

                    rowNonConformitaResult.Visible = true;
                    rowDatiIspezioneResult.Visible = false;

                    lblDescrizioneDataControllo.Visible = true;
                    lblDataControllo.Visible = true;
                    #endregion
                    break;
                case "2": //Accertamento su ispezione
                    #region Link Ispezione
                    lblDescrizionePunteggioAccertamento.Visible = false;

                    string iDIspezioneVisita = UtilityVerifiche.GetIDIspezioneVisitaFromVerifica(long.Parse(e.Item.Cells[17].Text)).ToString();
                    lblDescrizioneRctIspezione.Text = "Ispezione:&nbsp;";
                    lnkRctIspezione.Text = UtilityVerifiche.GetCodiceIspezioneFromIDIspezione(long.Parse(e.Item.Cells[17].Text)).ToString();

                    QueryString qsIspezione = new QueryString();
                    qsIspezione.Add("IDIspezione", e.Item.Cells[17].Text);
                    qsIspezione.Add("IDIspezioneVisita", iDIspezioneVisita);
                    QueryString qsEncryptedIspezione = Encryption.EncryptQueryString(qsIspezione);

                    string urlIspezione = "VER_Ispezioni.aspx";
                    urlIspezione += qsEncryptedIspezione.ToString();
                    lnkRctIspezione.NavigateUrl = urlIspezione;
                    lnkRctIspezione.Target = "_blank";
                    lnkRctIspezione.Text = e.Item.Cells[17].Text;

                    rowNonConformitaResult.Visible = false;
                    rowDatiIspezioneResult.Visible = true;

                    lblDescrizioneDataControllo.Visible = false;
                    lblDataControllo.Visible = false;
                    #endregion
                    break;
            }

            bool fAccertamentoInCorso = UtilityVerifiche.ControllaAccettamentiInCorso(int.Parse(IDTipoAccertamento), 
                                                                                      e.Item.Cells[0].Text,
                                                                                      e.Item.Cells[4].Text,
                                                                                      e.Item.Cells[10].Text,
                                                                                      e.Item.Cells[11].Text);

            TableRow rowAccertamentoInCorso = (TableRow)(e.Item.Cells[8].FindControl("rowAccertamentoInCorso"));
            if (fAccertamentoInCorso)
            {
                rowAccertamentoInCorso.Visible = true;
            }
        }
    }

    public class NonConformitaDTo
    {
        public string NonConformita { get; set; }
        public int Punteggio { get; set; }
    }

    public List<NonConformitaDTo> DataSourseNC(long IdAccertamento)
    {
        var DSNonConformita = new List<NonConformitaDTo>();

        using (var ctx = new CriterDataModel())
        {
            var NonConformita = ctx.V_VER_AccertamentiProgramma.FirstOrDefault(c => c.IDAccertamento == IdAccertamento);

            if (NonConformita != null)
            {
                switch (NonConformita.Prefisso)
                {
                    case "GT":
                        if (NonConformita.RendimentoSupMinimo == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Rendimento > Rendimento minimo di legge", Punteggio = 6 });
                        if (NonConformita.COFumiSecchiNoAria1000 == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "CO corretto < 1000 ppm", Punteggio = 4 });
                        if (NonConformita.RispettaIndiceBacharach == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Rispetta l'indice di Bacharac", Punteggio = 6 });
                        if (NonConformita.RendimentoCombustione < NonConformita.RendimentoMinimo)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Rendimento di combustione", Punteggio = 6 });
                        if (NonConformita.CoCorretto > 1000)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "CO corretto", Punteggio = 4 });
                        if (NonConformita.ConformitaUNI10389 == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Risultati controllo, secondo UNI10389-1, conformi alla legge", Punteggio = 6 });
                        if (NonConformita.RiflussoProdottiCombustione == 1)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Presenza riflusso prodotti della combustione", Punteggio = 3 });
                        if (NonConformita.ScambiatoreFumiPulito == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Controllato e pulito lo scambiatore lato fumi", Punteggio = 4 });
                        if (NonConformita.ValvolaSicurezzaSovrappressione == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Valvola di sicurezza alla sovrapressione a scarico libero", Punteggio = 4 });
                        if (NonConformita.DispositiviSicurezza == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Dispositivi di sicurezza non manomessi e/o cortocircuitati", Punteggio = 4 });
                        if (NonConformita.DispositiviComandoRegolazione == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Dispositivi di comando/ regolazione funzionanti correttamente", Punteggio = 4 });
                        if (NonConformita.DepressioneCanaleFumo < 3)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Depressione nel canale da fumo", Punteggio = 3 });
                        if (NonConformita.TenutaImpiantoIdraulico == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Idonea tenuta impianto interno e raccordi con il generatore", Punteggio = 3 });
                        if (NonConformita.AssenzaPerditeCombustibile == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Assenza di perdite di combustibile liquido", Punteggio = 3 });
                        if (NonConformita.RegolazioneTemperaturaAmbiente == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Sistema di regolazione temperatura ambiente non funzionante", Punteggio = 6 });
                        if (NonConformita.ScarichiIdonei == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Canale da fumo o condotti di scarico non idonei (esame visivo)", Punteggio = 4 });
                        if (NonConformita.DimensioniApertureAdeguate == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Adeguate dimensioni aperture di ventlazione/aerazione", Punteggio = 4 });
                        if (NonConformita.ApertureLibere == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Aperture di ventilazione/aerazione ostruite", Punteggio = 3 });
                        if (NonConformita.GeneratoriIdonei == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Per installazione esterna: generatori idonei", Punteggio = 4 });
                        if (NonConformita.LocaleInstallazioneIdoneo == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Per installazione interna: in locale idoneo", Punteggio = 4 });
                        if (NonConformita.TrattamentoACS == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Trattamento in ACS", Punteggio = 7 });
                        if (NonConformita.TrattamentoRiscaldamento == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Trattamento in riscaldamento", Punteggio = 7 });
                        if (NonConformita.fLibrettoImpiantoCompilato == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Libretto impianto compilato in tutte le sue parti", Punteggio = 8 });
                        if (NonConformita.fUsoManutenzioneGeneratore == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Libretti uso/manutenzione generatori presenti", Punteggio = 8 });
                        if (NonConformita.fDichiarazioneConformita == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Dichiarazione conformità presente", Punteggio = 8 });
                        if (NonConformita.fImpiantoFunzionante == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Impianto non può funzionare", Punteggio = 1 });
                        break;
                    case "GF":
                        if (NonConformita.fDichiarazioneConformita == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Dichiarazione conformità presente", Punteggio = 8 });
                        if (NonConformita.fUsoManutenzioneGeneratore == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Libretti uso/manutenzione generatori presenti", Punteggio = 8 });
                        if (NonConformita.fLibrettoImpiantoCompilato == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Libretto impianto compilato in tutte le sue parti", Punteggio = 8 });
                        if (NonConformita.TrattamentoRiscaldamento == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Trattamento", Punteggio = 7 });
                        if (NonConformita.LocaleInstallazioneIdoneo == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Locale installazione idoneo", Punteggio = 3 });
                        if (NonConformita.DimensioniApertureAdeguate == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Dimensioni aperture di ventilazioni adeguate", Punteggio = 3 });
                        if (NonConformita.ApertureLibere == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Aperture di ventilazione libere da ostruzioni", Punteggio = 3 });
                        if (NonConformita.LineeElettricheIdonee == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Linee elettriche idonee", Punteggio = 3 });
                        if (NonConformita.CoibentazioniIdonee == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Coibentazioni idonee", Punteggio = 3 });
                        if (NonConformita.AssenzaPerditeRefrigerante == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Assenza perdite di gas refrigerante", Punteggio = 3 });
                        if (NonConformita.FiltriPuliti == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Filtri puliti", Punteggio = 4 });
                        if (NonConformita.LeakDetector == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Presenza apparecchiature automatica rilevazione diretta fughe refrigerante", Punteggio = 4 });
                        if (NonConformita.ScambiatoriLiberi == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Scambiatori di calore puliti e liberi da incrostazioni", Punteggio = 4 });
                        if (NonConformita.ParametriTermodinamici == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Presenza apparecchiatura automatica rilevazioni indiretta fughe refrigerante (parametri termodinamici)", Punteggio = 4 });
                        if (NonConformita.fImpiantoFunzionante == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Impianto non può funzionare", Punteggio = 1 });
                        break;
                    case "SC":
                        if (NonConformita.fDichiarazioneConformita == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Dichiarazione conformità presente", Punteggio = 8 });
                        if (NonConformita.fUsoManutenzioneGeneratore == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Libretti uso/manutenzione generatori presenti", Punteggio = 8 });
                        if (NonConformita.fLibrettoImpiantoCompilato == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Libretto impianto compilato in tutte le sue parti", Punteggio = 8 });
                        if (NonConformita.TrattamentoRiscaldamento == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Trattamento in riscaldamento", Punteggio = 7 });
                        if (NonConformita.TrattamentoACS == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Trattamento in ACS", Punteggio = 7 });
                        if (NonConformita.LocaleInstallazioneIdoneo == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Locale installazione idoneo", Punteggio = 3 });
                        if (NonConformita.LineeElettricheIdonee == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Linee elettriche idonee", Punteggio = 3 });
                        if (NonConformita.StatoCoibentazioniIdonee == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Stato delle coibentazioni idoneo", Punteggio = 3 }); // D. Controllo dell'impianto
                        if (NonConformita.TenutaImpiantoIdraulico == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Assenza perdite dal circuito idraulico", Punteggio = 3 });
                        if (NonConformita.PotenzaCompatibileProgetto == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Potenza compatibile con i dati di progetto", Punteggio = 4 });
                        if (NonConformita.StatoCoibentazioniIdonee == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Stato delle coibentazioni idoneo", Punteggio = 4 }); // E. Controllo e verifica energetica dello scambiatore
                        //if (NonConformita.Assenzatrafilamenti == 0) - perché non presente nella vista ma nella sql query si ? rebuild datamodel
                        //    DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Dispositivi di regolazione e controllo funzionanti (assenza di trafilamenti sulla valvola di regolazione)", Punteggio = 4 });
                        if (NonConformita.fImpiantoFunzionante == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Impianto non può funzionare", Punteggio = 1 });
                        break;
                    case "CG":
                        if (NonConformita.fDichiarazioneConformita == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Dichiarazione conformità presente", Punteggio = 8 });
                        if (NonConformita.fUsoManutenzioneGeneratore == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Libretti uso/manutenzione generatori presenti", Punteggio = 8 });
                        if (NonConformita.fLibrettoImpiantoCompilato == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Libretto impianto compilato in tutte le sue parti", Punteggio = 8 });
                        if (NonConformita.TrattamentoRiscaldamento == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Trattamento in riscaldamento", Punteggio = 7 });
                        if (NonConformita.TrattamentoACS == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Trattamento in ACS", Punteggio = 7 });
                        if (NonConformita.LocaleInstallazioneIdoneo == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Luogo di installazione idoneo (esame visivo)", Punteggio = 3 });
                        if (NonConformita.DimensioniApertureAdeguate == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Adeguate dimensioni aperture ventilazione (esame visivo)", Punteggio = 3 });
                        if (NonConformita.ApertureLibere == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Aperture ventilazione libere da ostruzioni (esame visivo)", Punteggio = 3 });
                        if (NonConformita.LineeElettricheIdonee == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Linee elettriche e cablaggi idonei (esame visivo)", Punteggio = 3 });
                        if (NonConformita.ScarichiIdonei == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Camino e canale da fumo idonei (esame visivo)", Punteggio = 3 });
                        if (NonConformita.CapsulaInsonorizzataIdonea == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Capsula insonorizzante idonea (esame visivo)", Punteggio = 3 });
                        if (NonConformita.TenutaImpiantoIdraulico == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Tenuta circuito idraulico idonea", Punteggio = 3 });
                        if (NonConformita.TenutaCircuitoOlioIdonea == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Tenuta circuito olio idonea", Punteggio = 3 });
                        if (NonConformita.AssenzaPerditeCombustibile == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Tenuta circuito alimentazione combustibile idonea", Punteggio = 3 });
                        if (NonConformita.FunzionalitàScambiatoreSeparazione == 0)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Funzionalità dello scambiatore di calore di separazione tra unità cogenerativa e impianto edificio (se presente) idonea", Punteggio = 3 });
                        if (NonConformita.fImpiantoFunzionante == false)
                            DSNonConformita.Add(new NonConformitaDTo { NonConformita = "Impianto non può funzionare", Punteggio = 1 });
                        break;
                }
            }
        }
        return DSNonConformita.OrderBy(c => c.Punteggio).ToList();
    }

    public void GetAccertamenti()
    {
        DataGrid.CurrentPageIndex = 0;
        BindGrid(true);
    }

    protected void btnRicerca_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetAccertamenti();
        }
    }

    #endregion

    #region INSERIMENTO ACCERTAMENTI

    protected void btnInserireAccertamenti_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[6].FindControl("chkSelezioneAccertamenti");
            if (chk.Checked)
            {
                long IdAccertamento = long.Parse(DataGrid.Items[i].Cells[0].Text);
                UtilityVerifiche.InsertDeleteAccertamentoNelProgramma("Insert", null, IdAccertamento, info.IDUtente, UtilityVerifiche.GetIDProgrammaAccertamentoAttivo(int.Parse(IDTipoAccertamento)));
            }
        }

        GetDatiAll();
        GetAccertamenti();
        AccertamentiSelezionati();
        btnInserireAccertamenti.Visible = false;
    }

    protected void chkSelezioneAllAccertamenti_CheckedChanged(object sender, EventArgs e)
    {
        bool fButtonVisible = false;

        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[6].FindControl("chkSelezioneAccertamenti");
            if (chk.Enabled)
            {
                chk.Checked = ((CheckBox)sender).Checked;
                fButtonVisible = ((CheckBox)sender).Checked;
            }
        }

        AccertamentiSelezionati();
        btnInserireAccertamenti.Visible = fButtonVisible;
    }

    protected void chkSelezioneAccertamenti_CheckedChanged(object sender, EventArgs e)
    {
        bool fButtonVisible = false;
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[6].FindControl("chkSelezioneAccertamenti");
            if (chk.Checked)
            {
                fButtonVisible = true;
                break;
            }
        }

        AccertamentiSelezionati();
        btnInserireAccertamenti.Visible = fButtonVisible;
    }

    public void AccertamentiSelezionati()
    {
        int Selezionati = 0;

        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[6].FindControl("chkSelezioneAccertamenti");
            if (chk.Checked)
            {
                Selezionati++;
            }
        }

        lblCountSelezionati.Text = string.Format("{0} - ACCERTAMENTI SELEZIONATI", Selezionati);
    }

    #endregion

    #region PROGRAMMA ACCERTAMENTI

    public void GetListProgrammaAccertamento()
    {
        int iDTipoAccertamento = int.Parse(IDTipoAccertamento);

        GridProgrammaAccertamento.DataSource = UtilityVerifiche.GetListProgrammaAccertamenti(iDTipoAccertamento);
        GridProgrammaAccertamento.DataBind();
    }

    protected void imgfAttivo_Init(object sender, EventArgs e)
    {
        GridViewDataItemTemplateContainer container = ((ASPxImage)sender).NamingContainer as GridViewDataItemTemplateContainer;
        int currentIndex = container.VisibleIndex;
        bool isfAttivo = Convert.ToBoolean(container.Grid.GetRowValues(currentIndex, "fAttivo"));

        if (isfAttivo)
        {
            ((ASPxImage)sender).ImageUrl = "images/si.png";

        }
        else
        {
            ((ASPxImage)sender).ImageUrl = "Images/no.png";

        }
    }

    public void ExitAttivo()
    {
        int IdProgrammaAccertamentoAttivo = UtilityVerifiche.GetIDProgrammaAccertamentoAttivo(int.Parse(IDTipoAccertamento));

        if (GridProgrammaAccertamento.FocusedRowIndex != -1)
        {
            if (IdProgrammaAccertamentoAttivo == int.Parse(GridProgrammaAccertamento.GetRowValues(GridProgrammaAccertamento.FocusedRowIndex, "IDProgrammaAccertamento").ToString()) || IdProgrammaAccertamentoAttivo == 0)
            {
                cvAttivo.EnableClientScript = false;
            }
            else
            {
                cvAttivo.EnableClientScript = true;
            }
        }
        else
        {
            if (IdProgrammaAccertamentoAttivo == 0)
            {
                cvAttivo.EnableClientScript = false;
            }
            else
            {
                cvAttivo.EnableClientScript = true;
            }
        }
    }

    public void fVisibleColumnInsertNelProgramma(int iDProgrammaAccertamentoAttivo)
    {
        if (iDProgrammaAccertamentoAttivo == 0)
        {
            DataGrid.Columns[9].Visible = false;
            CreaPacchetto.Visible = false;
        }
        else
        {
            DataGrid.Columns[9].Visible = true;
            CreaPacchetto.Visible = true;
        }
    }

    protected void btnNuovoProgrammaAccertamento_Click(object sender, EventArgs e)
    {
        GridProgrammaAccertamento.FocusedRowIndex = -1;
        ExitAttivo();
        btnNuovoProgrammaAccertamento.Visible = false;
        tblPanel1.Visible = false;
        tblPanel2.Visible = true;
        pageControl.Visible = false;

        lbltitoloPanel2.Text = "<h2>NUOVO PROGRAMMA ACCERTAMENTO </h2>";

        txtDescrizione.Text = "";
        txtDataFine.Text = "";
        txtDataInizio.Text = "";
        cbAttivo.Checked = true;
    }

    protected void btnModificaProgrammaAccertamento_Click(object sender, ImageClickEventArgs e)
    {
        ExitAttivo();
        tblPanel1.Visible = false;
        tblPanel2.Visible = true;
        pageControl.Visible = false;
        btnNuovoProgrammaAccertamento.Visible = false;

        lbltitoloPanel2.Text = "<h2>MODIFICA PROGRAMMA ACCERTAMENTO </h2>";

        int rowIndex = GridProgrammaAccertamento.FocusedRowIndex;

        string DescrizioneOld = GridProgrammaAccertamento.GetRowValues(rowIndex, "Descrizione").ToString();
        string DataInizioOld = GridProgrammaAccertamento.GetRowValues(rowIndex, "DataInizio").ToString();
        string DataFineOld = GridProgrammaAccertamento.GetRowValues(rowIndex, "DataFine").ToString();
        string fAttivoOld = GridProgrammaAccertamento.GetRowValues(rowIndex, "fAttivo").ToString();

        txtDescrizione.Text = DescrizioneOld;
        txtDataInizio.Text = DateTime.Parse(DataInizioOld).ToString("dd/MM/yyyy");
        txtDataFine.Text = DateTime.Parse(DataFineOld).ToString("dd/MM/yyyy");
        cbAttivo.Checked = bool.Parse(fAttivoOld);
    }

    protected void btnSalvaDati_Click(object sender, EventArgs e)
    {
        if (GridProgrammaAccertamento.FocusedRowIndex == -1)
        {
            UtilityVerifiche.SaveInsertDeleteDatiProgrammaAccertamento("insert",
                                                        null,
                                                        int.Parse(IDTipoAccertamento),
                                                        txtDescrizione.Text,
                                                        DateTime.Parse(txtDataInizio.Text),
                                                        //UtilityApp.ParseNullableDatetime(txtDataFine.Text),
                                                        DateTime.Parse(txtDataFine.Text),
                                                        cbAttivo.Checked);
        }
        else
        {
            UtilityVerifiche.SaveInsertDeleteDatiProgrammaAccertamento("update",
                                                        int.Parse(GridProgrammaAccertamento.GetRowValues(GridProgrammaAccertamento.FocusedRowIndex, "IDProgrammaAccertamento").ToString()),
                                                        int.Parse(IDTipoAccertamento),
                                                        txtDescrizione.Text,
                                                        DateTime.Parse(txtDataInizio.Text),
                                                        //UtilityApp.ParseNullableDatetime(txtDataFine.Text),
                                                        DateTime.Parse(txtDataFine.Text),
                                                        cbAttivo.Checked);
        }

        fVisibleColumnInsertNelProgramma(UtilityVerifiche.GetIDProgrammaAccertamentoAttivo(int.Parse(IDTipoAccertamento)));

        //GetListProgrammaAccertamento();
        tblPanel2.Visible = false;
        tblPanel1.Visible = true;
        pageControl.Visible = true;
        btnNuovoProgrammaAccertamento.Visible = true;
        GetDatiAll();
        GridViewVisita.DataBind();
    }

    protected void btnAnnullaDati_Click(object sender, EventArgs e)
    {
        tblPanel1.Visible = true;
        tblPanel2.Visible = false;
        pageControl.Visible = true;
        btnNuovoProgrammaAccertamento.Visible = true;
    }

    protected void GridProgrammaAccertamento_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                int iDProgrammaAccertamento= int.Parse(e.GetValue("IDProgrammaAccertamento").ToString());
                var CountAccertamenti = ctx.VER_AccertamentoProgramma.Where(c => c.IDProgrammaAccertamento == iDProgrammaAccertamento).ToList();

                Label lblCountAccertamenti = GridProgrammaAccertamento.FindRowCellTemplateControl(e.VisibleIndex, null, "lblCountAccertamenti") as Label;
                lblCountAccertamenti.Text = CountAccertamenti.Count().ToString();

                ImageButton btnProgrammaAccertamentoDetails = GridProgrammaAccertamento.FindRowCellTemplateControl(e.VisibleIndex, null, "btnProgrammaAccertamentoDetails") as ImageButton;
                btnProgrammaAccertamentoDetails.Attributes.Add("onclick", "OpenDetailsProgrammaAccertamento(this, " + iDProgrammaAccertamento + "); return false;");

                ImageButton btnProgrammaAccertamentoDelete = GridProgrammaAccertamento.FindRowCellTemplateControl(e.VisibleIndex, null, "btnProgrammaAccertamentoDelete") as ImageButton;
                if (int.Parse(lblCountAccertamenti.Text) == 0)
                {
                    btnProgrammaAccertamentoDelete.Visible = true;
                }
                else
                {
                    btnProgrammaAccertamentoDelete.Visible = false;
                }

                btnProgrammaAccertamentoDetails.Attributes.Add("onclick", "OpenDetailsProgrammaAccertamento(this, " + iDProgrammaAccertamento + "); return false;");
            }
        }
    }

    protected void GridProgrammaAccertamento_PageIndexChanged(object sender, EventArgs e)
    {
        var view = sender as ASPxGridView;
        if (view == null) return;
        var pageIndex = view.PageIndex;
        GridProgrammaAccertamento.PageIndex = pageIndex;
        GetListProgrammaAccertamento();
    }

    public void RowCommandProgrammaAccertamento(object sender, CommandEventArgs e)
    {
        int? iDProgrammaAccertamento = int.Parse(e.CommandArgument.ToString());

        if (e.CommandName == "DeleteProgrammaAccertamento")
        {
            UtilityVerifiche.SaveInsertDeleteDatiProgrammaAccertamento("Delete",
                                                        iDProgrammaAccertamento,
                                                        0,
                                                        string.Empty,
                                                        null,
                                                        null,
                                                        cbAttivo.Checked);


            GetDatiAll();   
        }
    }

    public void ListaDeiAccertamentiSuProgramma()
    {
        int IdProgrammaAccertamentoAttivo = UtilityVerifiche.GetIDProgrammaAccertamentoAttivo(int.Parse(IDTipoAccertamento));

        using (var ctx = new CriterDataModel())
        {
            //var query = (from v in ctx.V_VER_Accertamenti
            //             join c in ctx.SYS_CodiciCatastali on v.IDCodiceCatastale equals c.IDCodiceCatastale
            //             join a in ctx.VER_AccertamentoProgramma on v.IDAccertamento equals a.IDAccertamento
            //             where !a.VER_AccertamentoVisitaInfo.Any() && a.IDProgrammaAccertamento == IdProgrammaAccertamentoAttivo
            //             select new
            //             {
            //                 a.IDAccertamentoProgramma,
            //                 a.IDAccertamento,
            //                 v.TipologiaCombustibile,
            //                 CodiceCatastale = c.CodiceCatastale + " - " + c.Comune,
            //                 //v.IndirizzoAzienda,
            //                 //v.Indirizzo,
            //                 v.NomeAzienda,
            //                 v.CodiceAccertamento,
            //                 v.CodiceTargatura,
            //                 v.PotenzaTermicaNominale,
            //                 Generatore = v.Prefisso + v.CodiceProgressivo,
            //                 v.PunteggioNCAccertamento
            //             }).ToList();

            var query = (from VER_Accertamento in ctx.VER_Accertamento
                         join LIM_TargatureImpianti in ctx.LIM_TargatureImpianti on VER_Accertamento.IDTargaturaImpianto equals LIM_TargatureImpianti.IDTargaturaImpianto
                         join VER_AccertamentoProgramma in ctx.VER_AccertamentoProgramma on VER_Accertamento.IDAccertamento equals VER_AccertamentoProgramma.IDAccertamento
                         join COM_AnagraficaSoggetti in ctx.COM_AnagraficaSoggetti on VER_Accertamento.IDSoggetto equals COM_AnagraficaSoggetti.IDSoggetto into COM_AnagraficaSoggetti_join
                         from COM_AnagraficaSoggetti in COM_AnagraficaSoggetti_join.DefaultIfEmpty()
                         join SYS_TipologiaCombustibile in ctx.SYS_TipologiaCombustibile on new { IDTipologiaCombustibile = (int)VER_Accertamento.IDTipologiaCombustibile } equals new { IDTipologiaCombustibile = SYS_TipologiaCombustibile.IDTipologiaCombustibile } into SYS_TipologiaCombustibile_join
                         from SYS_TipologiaCombustibile in SYS_TipologiaCombustibile_join.DefaultIfEmpty()
                         join SYS_CodiciCatastali in ctx.SYS_CodiciCatastali on VER_Accertamento.IDCodiceCatastale equals SYS_CodiciCatastali.IDCodiceCatastale into SYS_CodiciCatastali_join
                         from SYS_CodiciCatastali in SYS_CodiciCatastali_join.DefaultIfEmpty()
                         join VER_Ispezione in ctx.VER_Ispezione on VER_Accertamento.IDIspezione equals VER_Ispezione.IDIspezione into VER_Ispezione_join
                         from VER_Ispezione in VER_Ispezione_join.DefaultIfEmpty()
                         where !VER_AccertamentoProgramma.VER_AccertamentoVisitaInfo.Any() && VER_AccertamentoProgramma.IDProgrammaAccertamento == IdProgrammaAccertamentoAttivo
                         select new
                         {
                             VER_Accertamento.IDAccertamento,
                             VER_AccertamentoProgramma.IDAccertamentoProgramma,
                             VER_Accertamento.CodiceAccertamento,
                             LIM_TargatureImpianti.CodiceTargatura,
                             CodiceCatastale = (SYS_CodiciCatastali.CodiceCatastale + " - " + SYS_CodiciCatastali.Comune),
                             TipologiaCombustibile = SYS_TipologiaCombustibile.TipologiaCombustibile,
                             Generatore = VER_Accertamento.Prefisso + "" + VER_Accertamento.CodiceProgressivo,
                             VER_Accertamento.PunteggioNCAccertamento,
                             VER_Accertamento.IDProgrammaAccertamento,
                             VER_Accertamento.IDLibrettoImpianto,
                             IDTipologiaCombustibile = (int?)VER_Accertamento.IDTipologiaCombustibile,
                             VER_Accertamento.PotenzaTermicaNominale,
                             NomeAzienda = COM_AnagraficaSoggetti.NomeAzienda,
                             CodiceIspezione = VER_Ispezione.CodiceIspezione,
                             Ispettore = VER_Ispezione.COM_AnagraficaSoggetti1.Nome + " " + VER_Ispezione.COM_AnagraficaSoggetti1.Cognome
                         }).ToList();

            GridViewListaNelProgramma.DataSource = query;
            GridViewListaNelProgramma.DataBind();
        }
    }
    
    protected void RowCommandListaPerPacchettoAccertamenti(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        long IdAccertamentoProgramma = long.Parse(commandArgs[0]);
        long IdAccertamento = long.Parse(commandArgs[1]);
        

        if (e.CommandName == "Delete")
        {
            UtilityVerifiche.InsertDeleteAccertamentoNelProgramma("Delete", IdAccertamentoProgramma, IdAccertamentoProgramma, info.IDUtente, UtilityVerifiche.GetIDProgrammaAccertamentoAttivo(int.Parse(IDTipoAccertamento)));

            GetDatiAll();
        }
        else if (e.CommandName == "InsertNelPacchetto")
        {
            long IdPacchettoAccertamento = Convert.ToInt32(GridViewVisita.GetRowValues(GridViewVisita.FocusedRowIndex, "IDAccertamentoVisita"));

            UtilityVerifiche.InsertDeleteAccertamentiNelPacchetto("Insert", IdPacchettoAccertamento, IdAccertamentoProgramma, info.IDUtente, IdAccertamento);
            //GetDatiAll();
            GridViewVisita.DataBind();
            ListaDeiAccertamentiSuProgramma();
        }
    }

    #endregion

    #region PACCHETTI

    protected void CreaPacchetto_Click(object sender, EventArgs e)
    {
        UtilityVerifiche.CreaDeletePacchettoAccertamento("Crea", int.Parse(IDTipoAccertamento), null);
        GridViewVisita.DataBind();
    }

    protected void grdAccertamentiNelPacchetto_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        string IdAccertamentoPacchetto = gridView.GetMasterRowKeyValue().ToString();

        dsAccertamentiNelPacchetto.WhereParameters["IDAccertamentoVisita"].DefaultValue = IdAccertamentoPacchetto;
    }

    protected void CommandButtonAccertamenti(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "DeleteAccertamento")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            long IdPacchettoAccertamento = long.Parse(commandArgs[0]);
            long IdAccertamentoProgramma = long.Parse(commandArgs[1]);
            long IdAccertamento = long.Parse(commandArgs[2]);

            UtilityVerifiche.InsertDeleteAccertamentiNelPacchetto("Delete", IdPacchettoAccertamento, IdAccertamentoProgramma, info.IDUtente, IdAccertamento);
            GridViewVisita.DataBind();
            ListaDeiAccertamentiSuProgramma();
        }
    }

    protected void GridViewVisita_BeforePerformDataSelect(object sender, EventArgs e)
    {
        int iDProgrammaAccertamentoAttivo = UtilityVerifiche.GetIDProgrammaAccertamentoAttivo(int.Parse(IDTipoAccertamento));
        dsPacchetti.WhereParameters["IDProgrammaAccertamento"].DefaultValue = iDProgrammaAccertamentoAttivo.ToString();

        ////Se il campo di testo di ricerca del codice targatura non è vuoto allora ricavo da V_VER_IspezioniVisite la lista DISTNCT o il 
        ////singolo record del campo IDIspezioneVisita visita poi riaggiorno la grid
        //if (!string.IsNullOrEmpty(txtCodiceTargaturaInVisite.Text))
        //{
        //    string codiceTargatura = txtCodiceTargaturaInVisite.Text.Trim();
        //    List<long> IDsList = UtilityVerifiche.GetListVisisteByCodiceTargatura(codiceTargatura);
        //    string visiteList = IDsList.Count > 0 ? "{" : string.Empty;
        //    foreach (int id in IDsList)
        //    {
        //        visiteList += id.ToString() + ",";
        //    }
        //    if (IDsList.Count > 0)
        //    {
        //        visiteList = visiteList.Substring(0, visiteList.Length - 1);
        //        visiteList += "}";
        //    }

        //    dsVisiteIspettive.Where = "(it.IDIspezioneVisita IN " + visiteList + ")";
        //}
    }

    public void RowCommandPacchetto(object sender, CommandEventArgs e)
    {
        long IdAccertamentoPacchetto = long.Parse(e.CommandArgument.ToString());

        //if (e.CommandName == "AssegnaPacchetto")
        //{
        //    int IdAccertatore = 0; // TODO
        //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ShowPanelIspezioni", "lp.Show();", true);
        //    //UtilityVerifiche.AssegnaPacchettoDeiAccertamenti(IdAccertamentoPacchetto , int.Parse(userInfo.IDUtente.ToString()), IdAccertatore);
        //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HidePanelIspezioni", "setInterval(function(){lp.Hide();},20000);", true);
        //}
        //else 
        if (e.CommandName == "DeletePacchetto")
        {
            UtilityVerifiche.CreaDeletePacchettoAccertamento("Delete", int.Parse(IDTipoAccertamento), IdAccertamentoPacchetto);
            GridViewVisita.FocusedRowIndex = -1;
            GridViewVisita.Selection.UnselectAll();
        }
        GridViewVisita.DataBind();
        GridViewListaNelProgramma.Columns[12].Visible = false;
        ListaDeiAccertamentiSuProgramma();
    }

    protected void GridViewVisita_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                long IdAccertamentoPacchetto = int.Parse(e.GetValue("IDAccertamentoVisita").ToString());
                var CountAccertamenti = ctx.VER_AccertamentoVisitaInfo.Where(c => c.IDAccertamentoVisita == IdAccertamentoPacchetto).ToList();
                bool fpacchettoAssegnato = UtilityVerifiche.fPacchettoAssegnato(IdAccertamentoPacchetto);

                ImageButton ImgAssegnaPacchetto = GridViewVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "ImgAssegnaPacchetto") as ImageButton;
                ImageButton ImgDeletePacchetto = GridViewVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "ImgDeletePacchetto") as ImageButton;
                Image imgfAssegnato = GridViewVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "imgfAssegnato") as Image;
                Label lblAccertatore = GridViewVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "lblAccertatore") as Label;

                if (CountAccertamenti.Count() > 0 && !fpacchettoAssegnato)
                {
                    ImgAssegnaPacchetto.Visible = true;
                    imgfAssegnato.ImageUrl = "images/no.png";
                }
                if (ImgAssegnaPacchetto.Visible == true || CountAccertamenti.Count() == 0)
                {
                    ImgDeletePacchetto.Visible = true;
                    imgfAssegnato.ImageUrl = "images/no.png";
                }
                if (fpacchettoAssegnato)
                {
                    imgfAssegnato.ImageUrl = "images/si.png";
                    ImgDeletePacchetto.Visible = false;
                    imgfAssegnato.Visible = false;
                    
                    var DescAccertatore = ctx.VER_AccertamentoVisita.FirstOrDefault(c => c.IDAccertamentoVisita == IdAccertamentoPacchetto);
                    if (DescAccertatore != null)
                    {
                        var ComAccertatore = ctx.COM_Utenti.FirstOrDefault(c => c.IDUtente == DescAccertatore.IDAccertatore);
                        lblAccertatore.Text = ComAccertatore.COM_AnagraficaSoggetti.Nome + " " + ComAccertatore.COM_AnagraficaSoggetti.Cognome; //+ " in data " + DescAccertatore.DataAssegnazione;

                        lblAccertatore.Visible = true;
                    }
                }
                
                Label lblCountAccertamentiNelPacchetto = GridViewVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "lblCountAccertamentiNelPacchetto") as Label;
                lblCountAccertamentiNelPacchetto.Text = CountAccertamenti.Count().ToString();
                                
                ImgAssegnaPacchetto.Attributes.Add("onclick", "OpenDetailsProgrammaAccertamentoAssegnazione(this, " + IdAccertamentoPacchetto.ToString() + "); return false;");
            }
        }
    }
    
    protected void GridViewVisita_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType == GridViewRowType.Data)
        //{
        //    using (CriterDataModel ctx = new CriterDataModel())
        //    {
                //int iDIspezioneVisita = int.Parse(e.GetValue("IDIspezioneVisita").ToString());

                //var CountLibrettiAccertamenti = ctx.VER_IspezioneVisitaInfo.Where(c => c.IDIspezioneVisita == iDIspezioneVisita).ToList();

                //ImageButton ImgCreaIspezioni = GridViewVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "ImgCreaIspezioni") as ImageButton;
                //ImageButton ImgDeleteVisita = GridViewVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "ImgDeleteVisita") as ImageButton;

                //if (GridViewVisita.FocusedRowIndex == e.VisibleIndex && !UtilityVerifiche.fVisitaInIspezione(iDIspezioneVisita))
                //{
                //    if (CountLibrettiAccertamenti.Count() > 0)
                //    {
                //        ImgCreaIspezioni.Visible = true;
                //    }
                //    ImgDeleteVisita.Visible = true;
                //}
                //else
                //{
                //    ImgCreaIspezioni.Visible = false;
                //    ImgDeleteVisita.Visible = false;
                //}
        //    }
        //}
    }

    protected void GridViewVisita_FocusedRowChanged(object sender, EventArgs e)
    {
        if (GridViewVisita.FocusedRowIndex != -1)
        {
            long IdAccertamentoPacchetto = long.Parse(GridViewVisita.GetRowValues(GridViewVisita.FocusedRowIndex, "IDAccertamentoVisita").ToString());

            if (!UtilityVerifiche.fPacchettoAssegnato(IdAccertamentoPacchetto))
            {
                GridViewListaNelProgramma.Columns[12].Visible = true;
                GridViewVisita.DataBind();
            }
            else
            {
                GridViewListaNelProgramma.Columns[12].Visible = false;
                GridViewVisita.DataBind();
                ListaDeiAccertamentiSuProgramma();
            }
        }
        else
        {
            GridViewListaNelProgramma.Columns[12].Visible = false;
            GridViewVisita.Selection.UnselectAll();
            GridViewVisita.DataBind();
            ListaDeiAccertamentiSuProgramma();
        }
    }

    #endregion

    public void GetDatiAll()
    {
        fVisibleColumnInsertNelProgramma(UtilityVerifiche.GetIDProgrammaAccertamentoAttivo(int.Parse(IDTipoAccertamento)));
        GetListProgrammaAccertamento();
        //GetAccertamenti();
        GridViewVisita.FocusedRowIndex = -1;
        GridViewVisita.DataBind();
        GridViewVisita.Selection.UnselectAll();
        ListaDeiAccertamentiSuProgramma();
    }

    protected void chkCriticita_CheckedChanged(object sender, EventArgs e)
    {
        VisibleHiddenLivelloGravitaNC(chkCriticita.Checked);
    }

    public void VisibleHiddenLivelloGravitaNC(bool fCriticita)
    {
        if (fCriticita)
        {
            rowLivelloGravitaNC.Visible = true;
        }
        else
        {
            rowLivelloGravitaNC.Visible = false;
        }
    }

    protected void WindowProgrammaAccertamentoAssegnazione_WindowCallback(object source, PopupWindowCallbackArgs e)
    {
        QueryString qs = new QueryString();
        qs.Add("IDTipoAccertamento", IDTipoAccertamento);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "VER_ProgrammaAccertamento.aspx";
        url += qsEncrypted.ToString();


        //DevExpress.Web.ASPxWebControl.RedirectOnCallback("VER_ProgrammaAccertamento.aspx");
        Response.RedirectLocation = ConfigurationManager.AppSettings["UrlPortal"].ToString() + url;
    }
}
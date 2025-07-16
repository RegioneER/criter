using DataUtilityCore;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EncryptionQS;
using System.Configuration;
using DataLayer;
using DevExpress.Web;

public partial class ANAG_AnagraficaSearch : Page
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
            else
            {
                #region Encrypt off
                try
                {
                    if (Request.QueryString["IDTipoSoggetto"] != null)
                    {
                        return (string) Request.QueryString["IDTipoSoggetto"];
                    }
                    else
                    {
                        return string.Empty;
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
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "ANAG_AnagraficaSearch.aspx");

        if (!Page.IsPostBack)
        {
            PagePermission();
            VisibleHiddenTipoSoggetto();
            LoadAllDropDownlist();
            txtAzienda.Focus();
        }
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                switch (IDTipoSoggetto)
                {
                    case "1": //Persona

                        break;
                    case "2": //Impresa
                    case "3": //Terzo responsabile
                        rowfIscrizione.Visible = true;
                        this.DataGrid.Columns[18].Visible = true;
                        break;
                    case "4": //Persona Responsabile tecnico

                        break;
                    case "5": //Distributori di combustibile
                        rowfIscrizione.Visible = true;
                        break;
                    case "6": //Software house

                        break;
                    case "7": //Ispettori
                        rowfIscrizione.Visible = true;
                        break;
                    case "8": //Cittadini
                        rowfIscrizione.Visible = false;
                        break;
                    case "9": //Enti locali
                        rowfIscrizione.Visible = true;
                        break;
                }
                break;
        }
    }

    protected void VisibleHiddenTipoSoggetto()
    {
        switch (IDTipoSoggetto)
        {
            case "1": //Operatore/Addetto
                rowAzienda.Visible = false;
                rowManutentore.Visible = true;
                rowFormaGiuridica.Visible = false;
                rowPartitaIva.Visible = false;
                rowDataIscrizione.Visible = false;
                rowStatoAccreditamento.Visible = false;
                COM_AnagraficaSearch_lblTitoloPersona.Text = "Operatore/Addetto";
                lblTitoloTipoSoggetto.Text = "RICERCA OPERATORI/ADDETTI";
                COM_AnagraficaSearch_btnProcess.Text = "RICERCA OPERATORI";
                COM_AnagraficaSearch_btnAdd.Text = "NUOVO OPERATORE";
                lblTitoloAnagraficaAttiva.Text = "Operatore/Addetto attivo?";
                rowIspettoreAttivo.Visible = false;
                break;
            case "2": //Azienda
                rowAzienda.Visible = true;
                rowManutentore.Visible = false;
                rowFormaGiuridica.Visible = true;
                rowPartitaIva.Visible = true;
                rowDataIscrizione.Visible = true;
                rowStatoAccreditamento.Visible = true;
                COM_AnagraficaSearch_btnProcess.Text = "RICERCA AZIENDE";
                lblTitoloTipoSoggetto.Text = "RICERCA AZIENDE";
                COM_AnagraficaSearch_btnAdd.Text = "NUOVA AZIENDA";
                lblTitoloAnagraficaAttiva.Text = "Azienda attiva?";
                rowIspettoreAttivo.Visible = false;
                break;
            case "4": //Persona Responsabile Tecnico
                rowAzienda.Visible = false;
                rowManutentore.Visible = true;
                rowFormaGiuridica.Visible = false;
                rowPartitaIva.Visible = false;
                rowDataIscrizione.Visible = false;
                rowStatoAccreditamento.Visible = false;
                COM_AnagraficaSearch_lblTitoloPersona.Text = "Responsabile tecnico";
                lblTitoloTipoSoggetto.Text = "RICERCA RESPONSABILI TECNICI";
                COM_AnagraficaSearch_btnProcess.Text = "RICERCA RESPONSABILI TECNICI";
                COM_AnagraficaSearch_btnAdd.Text = "NUOVO RESPONSABILE TECNICO";
                lblTitoloAnagraficaAttiva.Text = "Responsabile tecnico attivo?";
                rowIspettoreAttivo.Visible = false;
                break;
            case "5": //Distributori di combustibile
                rowAzienda.Visible = true;
                rowManutentore.Visible = false;
                rowFormaGiuridica.Visible = true;
                rowPartitaIva.Visible = true;
                rowDataIscrizione.Visible = true;
                rowStatoAccreditamento.Visible = false;
                COM_AnagraficaSearch_lblTitoloPersona.Text = "Distributore di combustibile";
                lblTitoloTipoSoggetto.Text = "RICERCA DISTRIBUTORI DI COMBUSTIBILE";
                COM_AnagraficaSearch_btnProcess.Text = "RICERCA DISTRIBUTORI DI COMBUSTIBILE";
                COM_AnagraficaSearch_btnAdd.Text = "NUOVO DISTRIBUTORE DI COMBUSTIBILE";
                lblTitoloAnagraficaAttiva.Text = "Distributore di combustibile attivo?";
                rowIspettoreAttivo.Visible = false;
                break;
            case "6": //Software house
                rowAzienda.Visible = true;
                rowManutentore.Visible = false;
                rowCodiceSoggetto.Visible = false;
                rowCodiceFiscale.Visible = false;
                rowFormaGiuridica.Visible = true;
                rowPartitaIva.Visible = true;
                rowDataIscrizione.Visible = false;
                rowStatoAccreditamento.Visible = false;
                COM_AnagraficaSearch_lblTitoloPersona.Text = "Software house";
                lblTitoloTipoSoggetto.Text = "RICERCA SOFTWARE HOUSE";
                COM_AnagraficaSearch_btnProcess.Text = "RICERCA SOFTWARE HOUSE";
                COM_AnagraficaSearch_btnAdd.Text = "NUOVA SOFTWARE HOUSE";
                lblTitoloAnagraficaAttiva.Text = "Software house attiva?";
                rowIspettoreAttivo.Visible = false;
                break;
            case "7": //Ispettori
                rowAzienda.Visible = false;
                rowManutentore.Visible = true;
                rowFormaGiuridica.Visible = false;
                rowPartitaIva.Visible = false;
                rowDataIscrizione.Visible = false;
                rowStatoAccreditamento.Visible = true;
                COM_AnagraficaSearch_lblTitoloPersona.Text = "Ispettore";
                lblTitoloTipoSoggetto.Text = "RICERCA ISPETTORI";
                COM_AnagraficaSearch_btnProcess.Text = "RICERCA ISPETTORI";
                COM_AnagraficaSearch_btnAdd.Text = "NUOVO ISPETTORE";
                lblTitoloAnagraficaAttiva.Text = "Ispettore attivo?";
                rowIspettoreAttivo.Visible = true;
                rowDataAccreditamento.Visible = true;
                rowDataRinnovo.Visible = true;
                break;
            case "8": //Cittadino
                rowAzienda.Visible = false;
                rowManutentore.Visible = true;
                rowFormaGiuridica.Visible = false;
                rowPartitaIva.Visible = false;
                rowDataIscrizione.Visible = true;
                rowfIscrizione.Visible = false;
                rowCodiceSoggetto.Visible = false;
                rowStatoAccreditamento.Visible = false;
                COM_AnagraficaSearch_lblTitoloPersona.Text = "Cittadino";
                lblTitoloTipoSoggetto.Text = "RICERCA CITTADINI";
                COM_AnagraficaSearch_btnProcess.Text = "RICERCA CITTADINI";
                COM_AnagraficaSearch_btnAdd.Visible = false;
                lblTitoloAnagraficaAttiva.Text = "Cittadino attivo?";
                rowIspettoreAttivo.Visible = false;
                break;
            case "9": //Enti locali
                rowAzienda.Visible = true;
                COM_AnagraficaSearch_lblTitoloAzienda.Text = "Ente Locale";
                rowCodiceSoggetto.Visible = false;
                rowManutentore.Visible = false;
                rowFormaGiuridica.Visible = true;
                rowPartitaIva.Visible = true;
                rowDataIscrizione.Visible = true;
                rowStatoAccreditamento.Visible = false;
                COM_AnagraficaSearch_btnProcess.Text = "RICERCA ENTI LOCALI";
                lblTitoloTipoSoggetto.Text = "RICERCA ENTI LOCALI";
                COM_AnagraficaSearch_btnAdd.Text = "NUOVO ENTE LOCALE";
                lblTitoloAnagraficaAttiva.Text = "Ente locale attivo?";
                rowIspettoreAttivo.Visible = false;
                break;
        }
    }

    #region DROPDOWNLIST / RADIOBUTTONLIST / CHECKBOXLIST
    protected void LoadAllDropDownlist()
    {
        ddFormaGiuridica(null, int.Parse(IDTipoSoggetto));
        rbStatoAccreditamento(null, int.Parse(IDTipoSoggetto));
    }

    protected void ddFormaGiuridica(int? IDPresel, int iDTipoSoggetto)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_FormeGiuridiche(IDPresel, iDTipoSoggetto);
        ddlFormaGiuridica.DataValueField = "IDFormaGiuridica";
        ddlFormaGiuridica.DataTextField = "FormaGiuridica";
        ddlFormaGiuridica.DataSource = ls;
        ddlFormaGiuridica.DataBind();

        ListItem myItem = new ListItem("-- Selezionare --", "0");
        ddlFormaGiuridica.Items.Insert(0, myItem);
        ddlFormaGiuridica.SelectedIndex = 0;
    }

    protected void rbStatoAccreditamento(int? idPresel, int iDTipoSoggetto)
    {
        rblStatoAccreditamento.Items.Clear();
        rblStatoAccreditamento.ValueField = "IDStatoAccreditamento";
        rblStatoAccreditamento.TextField = "StatoAccreditamento";
        rblStatoAccreditamento.ImageUrlField = "ImageUrlStatoAccreditamento";
        rblStatoAccreditamento.DataSource = LoadDropDownList.LoadDropDownList_SYS_StatoAccreditamento(idPresel, iDTipoSoggetto);
        rblStatoAccreditamento.DataBind();

        ListEditItem myItem = new ListEditItem("Tutti gli stati", "0");
        rblStatoAccreditamento.Items.Insert(0, myItem);
        rblStatoAccreditamento.SelectedIndex = 0;
    }

    #endregion

    #region ANAGRAFICHE SOGGETTI
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpressionSoggetti"] == null)
                return string.Empty;
            return ViewState["SortExpressionSoggetti"].ToString();
        }
        set
        {
            ViewState["SortExpressionSoggetti"] = value;
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
        string sql = BuildStr();// Session["sqlstrSoggetti"].ToString();
        string currentSortExpression = this.SortExpression;
        int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, this.DataGrid.CurrentPageIndex, this.DataGrid.PageSize);
        this.SortExpression = currentSortExpression;

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "SOGGETTI CORRISPONDENTI AI PARAMETRI IMPOSTATI");
    }

    private string BuildStr()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        object iDSoggettoAzienda = null;
        object soggetto = "";

        switch (IDTipoSoggetto)
        {
            case "1": //Persona
            case "4": //Responsabile tecnico
            case "7": //Ispettori
                soggetto = txtManutentore.Text;
                break;
            case "2": //Azienda
            case "5": //Distributori di combustibile
            case "6": //Software house
            case "9": //Ente locale
                soggetto = txtAzienda.Text;
                break;
            case "8": //Cittadino
                soggetto = txtManutentore.Text;
                break;
        }

        switch (getVal[1])
        {
            case "1": //Amministratore Criter

                break;
            case "2": //Amministratore azienda
                iDSoggettoAzienda = getVal[0];
                break;
            case "3": //Operatore/Addetto
                iDSoggettoAzienda = getVal[2];
                break;
        }

        bool fIscrizione = true;
        switch (rblStatoIscrizione.SelectedValue)
        {
            case ("0"):
                fIscrizione = true;
                break;
            case ("1"):
                fIscrizione = false;
                break;
        }

        object fAttivoAccreditamento = null;
        if (IDTipoSoggetto == "7")
        {
            switch (rblStatoIspettoreAttivo.SelectedValue)
            {
                case (""):
                    fAttivoAccreditamento = null;
                    break;
                case ("0"):
                    fAttivoAccreditamento = true;
                    break;
                case ("1"):
                    fAttivoAccreditamento = false;
                    break;
            }
        }

        string strSql = UtilitySoggetti.GetSqlValoriSoggettiFilter(iDSoggettoAzienda,
                                                            IDTipoSoggetto,
                                                            soggetto,
                                                            ddlFormaGiuridica.SelectedItem.Value,
                                                            txtCodiceSoggetto.Text,
                                                            txtCodiceFiscale.Text,
                                                            txtPartitaIva.Text,
                                                            txtEmail.Text,
                                                            txtDataIscrizioneInizio.Text,
                                                            txtDataIscrizioneFine.Text,
                                                            optAttivo.Checked,
                                                            fIscrizione,
                                                            rblStatoAccreditamento.SelectedItem.Value,
                                                            fAttivoAccreditamento,
                                                            txtDataAccreditamentoInizio.Text,
                                                            txtDataAccreditamentoFine.Text,
                                                            txtDataRinnovoInizio.Text,
                                                            txtDataRinnovoFine.Text);

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
            Image imgEdit = (Image) e.Item.FindControl("ImgEdit");
            Image imgEditAlbo = (Image)e.Item.FindControl("ImgEditAlbo");

            Panel pnlAzienda = (Panel) e.Item.FindControl("pnlAzienda");
            Panel pnlPersona = (Panel) e.Item.FindControl("pnlPersona");
            Panel pnlIspettore = (Panel) e.Item.FindControl("pnlIspettore");
            Panel pnlCittadino = (Panel)e.Item.FindControl("pnlCittadino");
            Panel pnlEnteLocale = (Panel)e.Item.FindControl("pnlEnteLocale");

            QueryString qs = new QueryString();
            qs.Add("IDSoggetto", e.Item.Cells[0].Text);
            qs.Add("IDTipoSoggetto", e.Item.Cells[2].Text);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "ANAG_Anagrafica.aspx";
            url += qsEncrypted.ToString();
            imgEdit.Attributes.Add("onclick", "javascript:location.href='" + url + "'");

            ImageButton imgFlagAttivo = (ImageButton) (e.Item.Cells[5].FindControl("imgFlagAttivo"));
            if (imgFlagAttivo != null)
            {
                imgFlagAttivo.ImageUrl = UtilityApp.BooleanFlagToImage(bool.Parse(e.Item.Cells[15].Text));
            }

            if ((e.Item.Cells[2].Text == "2") || (e.Item.Cells[2].Text == "3") || (e.Item.Cells[2].Text == "5") || (e.Item.Cells[2].Text == "6"))
            {
                pnlAzienda.Visible = true;
                pnlPersona.Visible = false;
                pnlIspettore.Visible = false;
                pnlCittadino.Visible = false;
                pnlEnteLocale.Visible = false;

                QueryString qsAlbo = new QueryString();
                qsAlbo.Add("IDSoggetto", e.Item.Cells[0].Text);

                QueryString qsEncryptedAlbo = Encryption.EncryptQueryString(qs);

                string urlAlbo = "ANAG_AnagraficaAlbo.aspx";
                urlAlbo += qsEncryptedAlbo.ToString();
                imgEditAlbo.Attributes.Add("onclick", "javascript:location.href='" + urlAlbo + "'");

                if (bool.Parse(e.Item.Cells[16].Text)) //fIscrizione
                {
                    if (e.Item.Cells[2].Text == "5") //distributore
                    {
                        Label lblFirmaDigitale = (Label)(e.Item.Cells[3].FindControl("lblFirmaDigitale"));
                        lblFirmaDigitale.Visible = true;
                        lblFirmaDigitale.Text = "Iscrizione avvenuta con modalità custom";
                    }
                    else
                    {
                        if (e.Item.Cells[6].Text == "1")
                        {
                            Image imgSpid = (Image)(e.Item.Cells[3].FindControl("imgSpid"));
                            imgSpid.Visible = true;
                        }
                        else if (e.Item.Cells[6].Text == "2")
                        {
                            Label lblFirmaDigitale = (Label)(e.Item.Cells[3].FindControl("lblFirmaDigitale"));
                            lblFirmaDigitale.Visible = true;
                            lblFirmaDigitale.Text = "Iscrizione avvenuta con Firma digitale:&nbsp;" + e.Item.Cells[12].Text;
                        }
                    }

                    #region Accreditamento Impresa
                    if (e.Item.Cells[2].Text == "2") //Impresa
                    {
                        ImageButton ImgAccreditamentoAzienda = (ImageButton)(e.Item.Cells[3].FindControl("ImgAccreditamentoAzienda"));
                        ImgAccreditamentoAzienda.ImageUrl = e.Item.Cells[19].Text;
                        ImgAccreditamentoAzienda.Visible = true;

                        QueryString qsAccreditamento = new QueryString();
                        qsAccreditamento.Add("IDSoggetto", e.Item.Cells[0].Text);
                        qsAccreditamento.Add("IDTipoSoggetto", e.Item.Cells[2].Text);
                        QueryString qsEncryptedAccreditamento = Encryption.EncryptQueryString(qsAccreditamento);

                        string urlAccreditamento = "ANAG_AnagraficaAccreditamento.aspx";
                        urlAccreditamento += qsEncryptedAccreditamento.ToString();
                        ImgAccreditamentoAzienda.Attributes.Add("onclick", "javascript:location.href='" + urlAccreditamento + "'");
                    }
                    #endregion
                }
                else
                {
                    if (e.Item.Cells[2].Text == "5") //distributore
                    {
                        //Step di iscrizione non completato (scelta utenza) mostro il link per inviare la pagina con i parametri giusti
                        ImageButton imgInviaLinkAccessoAzienda = (ImageButton)(e.Item.Cells[3].FindControl("imgInviaLinkAccessoAzienda"));
                        imgInviaLinkAccessoAzienda.Visible = true;
                    }
                    else
                    {
                        if ((e.Item.Cells[14].Text != "") && (e.Item.Cells[14].Text != "&nbsp;"))
                        {
                            //Step di iscrizione non completato (scelta utenza) mostro il link per inviare la pagina con i parametri giusti
                            ImageButton imgInviaLinkAccessoAzienda = (ImageButton)(e.Item.Cells[3].FindControl("imgInviaLinkAccessoAzienda"));
                            imgInviaLinkAccessoAzienda.Visible = true;
                        }
                        else
                        {
                            //Step di iscrizione non completato (firma) mostro il link per inviare la pagina con i parametri giusti
                            ImageButton imgInviaLinkIscrizione = (ImageButton)(e.Item.Cells[3].FindControl("imgInviaLinkIscrizione"));
                            imgInviaLinkIscrizione.Visible = true;
                        }
                    }
                }
            }
            else if ((e.Item.Cells[2].Text == "1") || (e.Item.Cells[2].Text == "4"))
            {
                pnlAzienda.Visible = false;
                pnlPersona.Visible = true;
                pnlIspettore.Visible = false;
                pnlCittadino.Visible = false;
                pnlEnteLocale.Visible = false;

                string[] getVal = new string[4];
                getVal = SecurityManager.GetDatiPermission();

                if (getVal[1] == "1") //Solo per gli ammministratori
                {
                    //Completamento accesso per i manutentori che non ricevono (o fanno finta di non ricevere) la email
                    ImageButton imgInviaLinkAccesso = (ImageButton)(e.Item.Cells[3].FindControl("imgInviaLinkAccesso"));
                    Label lblAttivazioneUtenza = (Label)(e.Item.Cells[3].FindControl("lblAttivazioneUtenza"));

                    int? iDSoggetto = UtilityApp.ParseNullableInt(e.Item.Cells[0].Text);
                    using (CriterDataModel ctx = new CriterDataModel())
                    {
                        var utenza = ctx.COM_Utenti.Where(c => c.IDSoggetto == iDSoggetto).ToList();

                        if (utenza.Count == 0)
                        {
                            imgInviaLinkAccesso.Visible = false;
                            lblAttivazioneUtenza.Visible = true;
                            lblAttivazioneUtenza.Text = "Soggetto senza credenziali di accesso al Sistema Criter";
                        }
                        else
                        {
                            lblAttivazioneUtenza.Text = "Soggetto con credenziali di accesso al Sistema Criter";
                            lblAttivazioneUtenza.Visible = true;
                            if (!string.IsNullOrEmpty(e.Item.Cells[17].Text) && (e.Item.Cells[17].Text != "&nbsp;"))
                            {
                                imgInviaLinkAccesso.Visible = false;
                            }
                            else
                            {
                                imgInviaLinkAccesso.Visible = true;
                            }
                        }
                    }
                }
            }
            else if (e.Item.Cells[2].Text == "7")
            {
                pnlAzienda.Visible = false;
                pnlPersona.Visible = false;
                pnlIspettore.Visible = true;
                pnlCittadino.Visible = false;
                pnlEnteLocale.Visible = false;

                if (bool.Parse(e.Item.Cells[16].Text)) //fIscrizione
                {
                    if (e.Item.Cells[6].Text == "1")
                    {
                        Image imgSpidIspettore = (Image)(e.Item.Cells[3].FindControl("imgSpidIspettore"));
                        imgSpidIspettore.Visible = true;
                    }
                    else if (e.Item.Cells[6].Text == "2")
                    {
                        Label lblFirmaDigitaleIspettore = (Label)(e.Item.Cells[3].FindControl("lblFirmaDigitaleIspettore"));
                        
                        lblFirmaDigitaleIspettore.Visible = true;
                        lblFirmaDigitaleIspettore.Text = "Iscrizione avvenuta con Firma digitale:&nbsp;" + e.Item.Cells[12].Text;

                        ImageButton ImgAccreditamentoIspettore = (ImageButton)(e.Item.Cells[3].FindControl("ImgAccreditamentoIspettore"));
                        ImgAccreditamentoIspettore.ImageUrl = e.Item.Cells[19].Text;

                        ImageButton imgFlagAttivoIspettore = (ImageButton)(e.Item.Cells[21].FindControl("imgFlagAttivoIspettore"));
                        imgFlagAttivoIspettore.ImageUrl = UtilityApp.BooleanFlagToImage(bool.Parse(e.Item.Cells[21].Text));
                        
                        QueryString qsAccreditamento = new QueryString();
                        qsAccreditamento.Add("IDSoggetto", e.Item.Cells[0].Text);
                        qsAccreditamento.Add("IDTipoSoggetto", e.Item.Cells[2].Text);
                        QueryString qsEncryptedAccreditamento = Encryption.EncryptQueryString(qsAccreditamento);

                        string urlAccreditamento = "ANAG_AnagraficaAccreditamento.aspx";
                        urlAccreditamento += qsEncryptedAccreditamento.ToString();
                        ImgAccreditamentoIspettore.Attributes.Add("onclick", "javascript:location.href='" + urlAccreditamento + "'");
                    }
                }
                else
                {
                    if ((e.Item.Cells[14].Text != "") && (e.Item.Cells[14].Text != "&nbsp;"))
                    {
                        //Step di iscrizione non completato (scelta utenza) mostro il link per inviare la pagina con i parametri giusti
                        ImageButton imgInviaLinkAccessoIspettore = (ImageButton)(e.Item.Cells[3].FindControl("imgInviaLinkAccessoIspettore"));
                        imgInviaLinkAccessoIspettore.Visible = true;
                    }
                    else
                    {
                        //Step di iscrizione non completato (firma) mostro il link per inviare la pagina con i parametri giusti
                        ImageButton imgInviaLinkIscrizioneIspettore = (ImageButton)(e.Item.Cells[3].FindControl("imgInviaLinkIscrizioneIspettore"));
                        imgInviaLinkIscrizioneIspettore.Visible = true;
                    }
                }
            }
            else if (e.Item.Cells[2].Text == "8")
            {
                pnlAzienda.Visible = false;
                pnlPersona.Visible = false;
                pnlIspettore.Visible = false;
                pnlCittadino.Visible = true;
                pnlEnteLocale.Visible = false;

                Image imgSpidCittadino = (Image)(e.Item.Cells[3].FindControl("imgSpidCittadino"));
                imgSpidCittadino.Visible = true;
            }
            else if (e.Item.Cells[2].Text == "9")
            {
                pnlAzienda.Visible = false;
                pnlPersona.Visible = false;
                pnlIspettore.Visible = false;
                pnlCittadino.Visible = false;
                pnlEnteLocale.Visible = true;
                                

                if (bool.Parse(e.Item.Cells[16].Text)) //fIscrizione
                {
                    if (e.Item.Cells[6].Text == "2")
                    {
                        Label lblFirmaDigitale = (Label)(e.Item.Cells[3].FindControl("lblFirmaDigitale"));
                        lblFirmaDigitale.Visible = true;
                        lblFirmaDigitale.Text = "Iscrizione avvenuta con Firma digitale:&nbsp;" + e.Item.Cells[12].Text;
                    }
                }
                else
                {
                    if ((e.Item.Cells[14].Text != "") && (e.Item.Cells[14].Text != "&nbsp;"))
                    {
                        //Step di iscrizione non completato (scelta utenza) mostro il link per inviare la pagina con i parametri giusti
                        ImageButton imgInviaLinkAccessoEnteLocale = (ImageButton)(e.Item.Cells[3].FindControl("imgInviaLinkAccessoEnteLocale"));
                        imgInviaLinkAccessoEnteLocale.Visible = true;
                    }
                    else
                    {
                        //Step di iscrizione non completato (firma) mostro il link per inviare la pagina con i parametri giusti
                        ImageButton imgInviaLinkIscrizioneEnteLocale = (ImageButton)(e.Item.Cells[3].FindControl("imgInviaLinkIscrizioneEnteLocale"));
                        imgInviaLinkIscrizioneEnteLocale.Visible = true;
                    }
                }
            }




        }
    }

    protected void DataGrid_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Pager)
        {
            foreach (TableCell item in e.Item.Cells)
            {
                foreach (Control c in item.Controls)
                {

                    if (c is WebControl)
                    {
                        (c as WebControl).TabIndex = 1;
                    }
                }
            }
        }
    }

    public void RowCommand(Object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            
        }
    }

    #endregion

    public void GetAnagraficaSoggetti()
    {
        System.Threading.Thread.Sleep(500);
        this.DataGrid.CurrentPageIndex = 0;
        BindGrid(true);
    }

    protected void COM_AnagraficaSearch_btnProcess_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetAnagraficaSoggetti();
            if (DataGrid.Items.Count > 0)
            {
                DataGrid.Items[0].Cells[4].Focus();
            }
        }
    }

    protected void COM_AnagraficaSearch_btnAdd_Click(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["fEncryptQueryString"] == "on")
        {
            QueryString qs = new QueryString();
            qs.Add("IDSoggetto", "");
            qs.Add("IDTipoSoggetto", IDTipoSoggetto);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "ANAG_Anagrafica.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
        else
        {
            Response.Redirect("ANAG_Anagrafica.aspx?IDSoggetto=&IDTipoSoggetto=" + IDTipoSoggetto + "");
        }
    }

    protected void ToggleFlagAttivo(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        int iDSoggetto = int.Parse(commandArgs[0].ToString());
        bool fAttivo = bool.Parse(commandArgs[1].ToString());

        bool newVal = UtilitySoggetti.ChangefAttivo(iDSoggetto, fAttivo);
        bool newValUtente = SecurityManager.ChangefAttivo(iDSoggetto, fAttivo);
        ImageButton imgbtn = (ImageButton) sender;
        if (imgbtn != null)
        {
            imgbtn.ImageUrl = UtilityApp.BooleanFlagToImage(newVal);
        }

        BindGrid(true);
    }

    protected void ToggleFlagAttivoIspettore(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        int iDSoggetto = int.Parse(commandArgs[0].ToString());
        bool fAttivoAccreditamento = bool.Parse(commandArgs[1].ToString());

        bool newVal = UtilitySoggetti.ChangefAttivoAccreditamento(iDSoggetto, fAttivoAccreditamento);
        //ImageButton imgbtn = (ImageButton)sender;
        //if (imgbtn != null)
        //{
        //    imgbtn.ImageUrl = UtilityApp.BooleanFlagToImage(newVal);
        //}

        BindGrid(true);
    }
    
    protected void btnInviaLinkIscrizione(object sender, CommandEventArgs e)
    {
        int iDSoggetto = int.Parse(e.CommandArgument.ToString());
        EmailNotify.SendLinkCompletamentoIscrizione(iDSoggetto);

        BindGrid(true);
    }

    protected void btnInviaLinkAccesso(object sender, CommandEventArgs e)
    {
        int iDSoggetto = int.Parse(e.CommandArgument.ToString());
        EmailNotify.SendLinkCredenziali(iDSoggetto);

        BindGrid(true);
    }

}
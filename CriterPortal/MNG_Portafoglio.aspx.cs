using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DataUtilityCore;
using PayerLib;
using DataLayer;
using DataUtilityCore.Portafoglio;
using DevExpress.Web;
using EncryptionQS;
using System.Configuration;
using System.Data.SqlClient;

public partial class MNG_Portafoglio : System.Web.UI.Page
{
    protected string iDSoggetto
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
                    if (Request.QueryString["iDSoggetto"] != null)
                    {
                        return (string) Request.QueryString["iDSoggetto"];
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

    protected string iDMovimento
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
                    if (Request.QueryString["iDMovimento"] != null)
                    {
                        return (string) Request.QueryString["iDMovimento"];
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
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "MNG_Portafoglio.aspx");

        if (!Page.IsPostBack)
        {
            PagePermission();
            InfoDefault();
            GridViewDataPortafoglio.DataSource = GetData();
            GridViewDataPortafoglio.DataBind();
            ASPxComboBox1.Focus();
            FixLottiDuplicati();
            GetSaldoPortafoglioImpresa();
        }
    }

    public void InfoDefault()
    {
        if ((!string.IsNullOrEmpty(iDSoggetto) && (!string.IsNullOrEmpty(iDMovimento))))
        {
            ASPxComboBox1.Value = iDSoggetto;
            txtIDMovimento.Text = iDMovimento;
        }
    }

    #region RICERCA AZIENDE
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

    protected void FixLottiDuplicati()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();
        string iDSoggetto = getVal[0].ToString();

        //TODO: In futuro chiamare //Portafoglio.FixLottiDuplicati(iDSoggetto);
        FixLottiDuplicati(iDSoggetto);
    }

    public static void FixLottiDuplicati(string iDSoggetto)
    {
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

                            if (bolliniDaCancellare.Count > 0)
                            {
                                ctx.RCT_LottiBolliniCalorePulito.RemoveRange(ctx.RCT_LottiBolliniCalorePulito.Where(c => c.IdLottobolliniCalorePulito == lotto.Value));
                                ctx.SaveChanges();
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            
                        }
                    }
                }
            }
        }
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                ASPxComboBox1.Visible = true;
                lblSoggetto.Visible = false;
                break;
            case "2": //Amministratore azienda
                ASPxComboBox1.Value = getVal[0].ToString();
                ASPxComboBox1.Visible = false;
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0].ToString());
                lblSoggetto.Visible = true;
                break;
            case "3": //Operatore/Addetto
                ASPxComboBox1.Visible = false;
                lblSoggetto.Visible = true;
                ASPxComboBox1.Value = getVal[2].ToString();
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                break;
        }
    }

    #region Metodi

    private void CustomGridDataBind(List<COM_RigaPortafoglio> data)
    {
        if (data == null || data.Count > 0)
        {
            GridViewDataPortafoglio.Visible = true;
            this.GridViewDataPortafoglio.DataSource = data;
            this.GridViewDataPortafoglio.DataBind();
        }
        else
        {
            GridViewDataPortafoglio.Visible = false;
        }
    }

    private List<COM_RigaPortafoglio> GetData(DateTime dateFrom, DateTime dateTo, string Sort, SortDirection SortDir = SortDirection.Descending)
    {
        if (ASPxComboBox1.Value != null)
        {
            int iDSoggetto = int.Parse(ASPxComboBox1.Value.ToString());
            Portafoglio portafoglio = Portafoglio.Load(iDSoggetto);
            if (portafoglio != null)
            {
                IEnumerable<COM_RigaPortafoglio> query = portafoglio.GetListaMovimenti();

                //FILTRO
                if (dateFrom != DateTime.MinValue)
                {
                    query = query.Where(c => c.DataRegistrazione >= dateFrom.Date);
                }
                if (dateTo != DateTime.MinValue)
                {
                    query = query.Where(c => c.DataRegistrazione <= dateTo.Date);
                }

                if (!string.IsNullOrEmpty(txtIDMovimento.Text))
                {
                    query = query.Where(c => c.IDMovimento == Guid.Parse(txtIDMovimento.Text));
                }

                switch (rblTipoPortafoglioRicarica.SelectedValue)
                {
                    case ("0"):
                        break;
                    case ("1"):
                        query = query.Where(c => c.IdPaymentRequest != null);
                        break;
                    case ("2"):
                        query = query.Where(c => c.IdMovimentoBonifico != null);
                        break;
                    case ("3"):
                        query = query.Where(c => c.IdMovimentoCassa != null);
                        break;
                }

                //SORT
                var pi = typeof(COM_RigaPortafoglio).GetProperty(Sort);
                if (pi != null)
                {
                    if (SortDir == SortDirection.Ascending)
                        query = query.OrderBy(x => pi.GetValue(x, null));
                    else
                        query = query.OrderByDescending(x => pi.GetValue(x, null));
                }
                                
                return query.ToList();
            }
        }
                
        return null;
    }

    private List<COM_RigaPortafoglio> GetData()
    {
        return GetData(GetFilterDateFrom(), GetFilterDateTo(), "DataRegistrazione");
    }

    private void GridViewSortDirection(GridView g, GridViewSortEventArgs e, out SortDirection d, out string f)
    {
        f = e.SortExpression;
        d = e.SortDirection;

        if (g.Attributes["CurrentSortField"] != null && g.Attributes["CurrentSortDir"] != null)
        {
            if (f == g.Attributes["CurrentSortField"])
            {
                d = SortDirection.Descending;
                if (g.Attributes["CurrentSortDir"] == "ASC")
                {
                    d = SortDirection.Ascending;
                }
            }

            g.Attributes["CurrentSortField"] = f;
            g.Attributes["CurrentSortDir"] = (d == SortDirection.Ascending ? "DESC" : "ASC");
        }

    }

    private DateTime GetFilterDateFrom()
    {
        return GetFilterDate(txtDataInizio.Text);
    }

    private DateTime GetFilterDateTo()
    {
        return GetFilterDate(txtDataFine.Text);
    }

    private DateTime GetFilterDate(string txt)
    {
        DateTime date = DateTime.MinValue;

        DateTime.TryParseExact(txt, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

        return date;
    }

    #endregion

    #region Eventi

    protected void GridViewDataPortafoglio_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int iDSoggetto = int.Parse(ASPxComboBox1.Value.ToString());
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            
        }
        else
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                COM_RigaPortafoglio rpg = ((COM_RigaPortafoglio) e.Row.DataItem);
                COM_RigaPortafoglio rp = ctx.COM_RigaPortafoglio.FirstOrDefault(r => r.IDMovimento == rpg.IDMovimento);
                Label lblImporto = e.Row.FindControl("lblImporto") as Label;
                Label lblDescrizione = e.Row.FindControl("lblDescrizione") as Label;
                Label lblEsito = e.Row.FindControl("lblEsito") as Label;
                var lnkRicevuta = e.Row.FindControl("lnkRicevuta") as HyperLink;
                if (lblImporto != null && lblDescrizione != null && lblEsito != null && lnkRicevuta != null)
                {
                    lblImporto.Text = String.Format("{0:0.00}", rp.Importo);

                    QueryString qs = new QueryString();
                    qs.Add("IDMovimento", rp.IDMovimento.ToString());
                    
                    QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                    string url = "MNG_PayERRicevutaViewer.aspx";
                    url += qsEncrypted.ToString();
                    lnkRicevuta.Attributes.Add("onclick", "dhtmlwindow.open('RicevutaPagamento_" + "', 'iframe', '" + url + "', 'Ricevuta " + rp.IDMovimento.ToString() + "', 'width=750px,height=500px,resize=1,scrolling=1,center=1'); return false");
                    
                    if (rp.IdLottoBollinicalorePulito != null)
                    {
                        lblImporto.Text = String.Format("{0:0.00}", rp.Importo);
                        if (!string.IsNullOrEmpty(rp.IdLottoBollinicalorePulito.ToString()))
                        {
                            lblDescrizione.Text = string.Format("Acquisto di <b>{0}</b> Bollini Calore Pulito - Lotto nr.&nbsp;<b>{1}</b>&nbsp;del&nbsp;{2}&nbsp;<br/>", rp.Quantita.ToString(), rp.RCT_LottiBolliniCalorePulito.IdLottobolliniCalorePulito, rp.RCT_LottiBolliniCalorePulito.DataAcquisto);
                        }
                    }

                    if (rp.COM_MovimentoBonifico != null)
                    {
                        if (rp.Importo == 0)
                        {
                            lblDescrizione.Text = string.Format(""+ rp.COM_MovimentoBonifico.Causale + " - Lotto nr.&nbsp;<b>{0}</b>&nbsp;del&nbsp;{1}&nbsp;con nr. <b>{2}</b> bollini<br/>", rp.RCT_LottiBolliniCalorePulito.IdLottobolliniCalorePulito, rp.RCT_LottiBolliniCalorePulito.DataAcquisto, rp.Quantita.ToString());
                        }
                        else
                        {
                            lblDescrizione.Text = "Incasso Bonifico del " + rp.COM_MovimentoBonifico.DataBonifico.ToShortDateString();
                            if (!string.IsNullOrEmpty(rp.IdLottoBollinicalorePulito.ToString()))
                            {
                                lblDescrizione.Text += string.Format("<br/>Acquisto di <b>{0}</b> Bollini Calore Pulito - Lotto nr.&nbsp;<b>{1}</b>&nbsp;del&nbsp;{2}&nbsp;<br/>", rp.Quantita.ToString(), rp.RCT_LottiBolliniCalorePulito.IdLottobolliniCalorePulito, rp.RCT_LottiBolliniCalorePulito.DataAcquisto);
                            }
                        }
                                                
                        lnkRicevuta.Visible = true;
                    }

                    if (rp.COM_PayerPaymentRequest != null)
                    {
                        lblDescrizione.Text += "Incasso PayER";
                        lblEsito.Text = PayerUtil.EsitoToString(rp.COM_PayerPaymentRequest.Esito);

                        if (rp.COM_PayerPaymentRequest.Esito.HasValue && rp.COM_PayerPaymentRequest.Esito.Value == (short) PayerEsitoTransazione.OK)
                        {
                            lnkRicevuta.Visible = true;
                        }
                    }
                    else
                    {
                        //per le altre tipologie di movimento
                        lblEsito.Text = "Operazione completata";
                    }

                    if (rp.COM_MovimentoCassa != null)
                    {
                        lblDescrizione.Text = "Incasso Diretto del " + rp.COM_MovimentoCassa.DataVersamento.ToShortDateString();
                        if (!string.IsNullOrEmpty(rp.IdLottoBollinicalorePulito.ToString()))
                        {
                            lblDescrizione.Text += string.Format("<br/>Acquisto di <b>{0}</b> Bollini Calore Pulito - Lotto nr.&nbsp;<b>{1}</b>&nbsp;del&nbsp;{2}&nbsp;<br/>", rp.Quantita.ToString(), rp.RCT_LottiBolliniCalorePulito.IdLottobolliniCalorePulito, rp.RCT_LottiBolliniCalorePulito.DataAcquisto);
                        }
                        lnkRicevuta.Visible = true;
                    }

                    if (rp.COM_MovimentoStorno != null)
                    {
                        //lblDescrizione.Text = rp.COM_MovimentoStorno.Descrizione;
                        //lnkRicevuta.Visible = false;
                        lblDescrizione.Text = "Accredito Residuo del " + rp.COM_MovimentoStorno.Data.ToShortDateString();
                        if (!string.IsNullOrEmpty(rp.IdLottoBollinicalorePulito.ToString()))
                        {
                            lblDescrizione.Text += string.Format("<br/>Acquisto di <b>{0}</b> Bollini Calore Pulito - Lotto nr.&nbsp;<b>{1}</b>&nbsp;del&nbsp;{2}&nbsp;<br/>", rp.Quantita.ToString(), rp.RCT_LottiBolliniCalorePulito.IdLottobolliniCalorePulito, rp.RCT_LottiBolliniCalorePulito.DataAcquisto);
                        }
                        lnkRicevuta.Visible = false;
                    }
                }
            }

        }
    }

    protected void GridViewDataPortafoglio_Sorting(object sender, GridViewSortEventArgs e)
    {
        String Sort;
        SortDirection SortDir;

        this.GridViewSortDirection(GridViewDataPortafoglio, e, out SortDir, out Sort);

        var data = this.GetData(GetFilterDateFrom(), GetFilterDateTo(), Sort, SortDir);
        CustomGridDataBind(data);
        //GridViewDataPortafoglio.DataSource = this.GetData(GetFilterDateFrom(), GetFilterDateTo(), Sort, SortDir);
        //GridViewDataPortafoglio.DataBind();
    }

    protected void GridViewDataPortafoglio_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SortDirection d = SortDirection.Ascending;
        string f = "DataRegistrazione";


        if (GridViewDataPortafoglio.Attributes["CurrentSortField"] != null && GridViewDataPortafoglio.Attributes["CurrentSortDir"] != null)
        {
            f = GridViewDataPortafoglio.Attributes["CurrentSortField"];
            d = (GridViewDataPortafoglio.Attributes["CurrentSortDir"] == "ASC") ? SortDirection.Descending : SortDirection.Ascending;
        }


        GridViewDataPortafoglio.PageIndex = e.NewPageIndex;

        var data = this.GetData(GetFilterDateFrom(), GetFilterDateTo(), f, d);
        CustomGridDataBind(data);
        //this.GridViewDataPortafoglio.DataSource = this.GetData(GetFilterDateFrom(), GetFilterDateTo(), f,d);
        //this.GridViewDataPortafoglio.DataBind();
    }

    protected void btnReloadPortafoglio_Click(Object sender, EventArgs e)
    {
        var data = this.GetData();
        CustomGridDataBind(data);
        //GridViewDataPortafoglio.DataSource = this.GetData();
        //GridViewDataPortafoglio.DataBind();
        UpdatePanelPortafoglio.Update();
    }

    protected void btnLoadPortafoglio_Click(Object sender, EventArgs e)
    {
        var data = this.GetData();
        CustomGridDataBind(data);
        UpdatePanelPortafoglio.Update();
        GetSaldoPortafoglioImpresa();
    }

    public void GetSaldoPortafoglioImpresa()
    {
        if (ASPxComboBox1.Value != null)
        {
            rowSaldoPortafoglio.Visible = true;
            int iDSoggetto = int.Parse(ASPxComboBox1.Value.ToString());
            lblSaldoPortafoglio.Text = Portafoglio.GetSaldoPortafoglio(iDSoggetto).ToString();
        }
    }

    #endregion

}
using DataUtilityCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using EncryptionQS;
using System.Web.UI;
using DevExpress.Web;
using System.Data.SqlClient;
using DataUtilityCore.Portafoglio;
using DataLayer;
using System.Linq;

public partial class RCT_BollinoCalorePulitoSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PagePermission();
            LoadAllDropDownlist();
            FixLottiDuplicati();
        }
    }

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

    #region DROPDOWNLIST / RADIOBUTTONLIST / CHECKBOXLIST
    protected void LoadAllDropDownlist()
    {
        ddCodiciLotto(null, ASPxComboBox1.Value);
    }

    protected void ddCodiciLotto(object iDPresel, object iDSoggetto)
    {
        SqlDataReader dr = LoadDropDownList.LoadDropDownList_RCT_LottiBolliniCalorePulito(iDPresel, iDSoggetto, false);
        ddlCodiciLotto.ValueField = "IDLottoBolliniCalorePulito";
        ddlCodiciLotto.TextField = "DescrizioneLotto";
        ddlCodiciLotto.DataSource = dr;
        ddlCodiciLotto.DataBind();
        dr.Close();

        ListEditItem myItem = new ListEditItem("-- Selezionare --", "0");
        ddlCodiciLotto.Items.Insert(0, myItem);
        ddlCodiciLotto.SelectedIndex = 0;
    }
    #endregion

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

    protected void ASPxComboBox_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetComboBoxFilterByIDAzienda();
        LoadAllDropDownlist();
    }

    protected void GetComboBoxFilterByIDAzienda()
    {
        if (ASPxComboBox1.Value != null)
        {
            ASPxComboBox2.Text = "";
            ASPxComboBox2.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", ASPxComboBox1.Value.ToString(), string.Format("%{0}%", ""), (1).ToString(), (100 + 1).ToString());
            ASPxComboBox2.DataBind();
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

        ASPxComboBox2.SelectedIndex = -1;
        ASPxComboBox2.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", "0", string.Format("%{0}%", ""), (1).ToString(), (100 + 1).ToString());
        ASPxComboBox2.DataBind();
    }
    #endregion

    #region RICERCA PERSONE AZIENDA
    protected void ASPxComboBox2_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox) source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox2_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox) source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "1", e.Value.ToString(), "");
        comboBox.DataBind();
    }

    protected void ASPxComboBox2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    #endregion
    
    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                ASPxComboBox1.Visible = true;
                ASPxComboBox2.Visible = true;
                lblSoggetto.Visible = false;
                lblSoggettoDerived.Visible = false;
                rfvASPxComboBox1.Enabled = false;
                rfvASPxComboBox2.Enabled = false;
                break;
            case "2": //Amministratore azienda
                ASPxComboBox1.Value = getVal[0].ToString();
                ASPxComboBox1.Visible = false;
                ASPxComboBox2.Visible = true;
                GetComboBoxFilterByIDAzienda();

                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0].ToString());
                lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = false;

                rfvASPxComboBox2.Enabled = false;
                break;
            case "3": //Operatore/Addetto
                ASPxComboBox1.Visible = false;
                ASPxComboBox2.Visible = false;
                lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = true;
                ASPxComboBox1.Value = getVal[2].ToString();
                ASPxComboBox2.Value = getVal[0].ToString();
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                lblSoggettoDerived.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0].ToString());
                break;
            case "10": //Responsabile tecnico
                ASPxComboBox1.Value = getVal[2].ToString();
                ASPxComboBox1.Visible = false;
                ASPxComboBox2.Visible = true;
                GetComboBoxFilterByIDAzienda();

                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = false;

                rfvASPxComboBox2.Enabled = false;
                break;
        }
    }
    
    private void GetBolliniCalorePulito()
    {
        System.Threading.Thread.Sleep(500);
        //Session["sqlstrBollini"] = buildStr();
        //Session["currentPageIndexBollini"] = 0;
        DataGrid.CurrentPageIndex = 0;
        BindGrid(true);
    }

    #region BOLLINI CALORE PULITO
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpressionBollini"] == null)
                return string.Empty;
            return ViewState["SortExpressionBollini"].ToString();
        }
        set
        {
            ViewState["SortExpressionBollini"] = value;
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
        string sql = buildStr();// Session["sqlstrBollini"].ToString();
        string currentSortExpression = this.SortExpression;
        DataGrid.PageSize = int.Parse(ASPxComboBox3.Value.ToString());

        //int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, (int) Session["currentPageIndexBollini"], DataGrid.PageSize);
        int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, this.DataGrid.CurrentPageIndex, DataGrid.PageSize);
        this.SortExpression = currentSortExpression;

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "BOLLINI CALORE PULITO CORRISPONDENTI AI PARAMETRI IMPOSTATI");
    }

    private string buildStr()
    {
        string strSql = UtilityBollini.GetSqlValoriBolliniFilter(ASPxComboBox1.Value, 
                                                                 ASPxComboBox2.Value, 
                                                                 ddlCodiciLotto.SelectedItem.Value, 
                                                                 rblStatoBolliniCalorePulito.SelectedItem.Value, 
                                                                 txtCodiceBollino.Text,
                                                                 rblStatoBolliniCalorePulitoNonAttivi.SelectedItem.Value,
                                                                 null);
        return strSql;
    }

    public void DataGrid_PageChanger(object source, DataGridPageChangedEventArgs e)
    {
        //Session["currentPageIndexBollini"] = e.NewPageIndex;
        //DataGrid.CurrentPageIndex = (int) Session["currentPageIndexBollini"];
        DataGrid.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
    }

    public void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Label lblRapportoDiControllo = (Label) (e.Item.Cells[11].FindControl("lblRapportoDiControllo"));
            if ((e.Item.Cells[1].Text != "") && (e.Item.Cells[1].Text != "&nbsp;"))
            {
                lblRapportoDiControllo.Visible = true;
                #region STILE
                if (e.Item.ItemType == ListItemType.Item)
                {
                    e.Item.Cells[11].Attributes.Add("onmouseover", "this.style.backgroundColor='#f6d18a';this.style.cursor='pointer'");
                    e.Item.Cells[11].Attributes.Add("onmouseout", "this.style.backgroundColor='#f5f2f2';this.style.cursor='pointer'");
                    e.Item.Cells[11].Style["cursor"] = "hand";
                }
                else if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    e.Item.Cells[11].Attributes.Add("onmouseover", "this.style.backgroundColor='#f6d18a';this.style.cursor='pointer'");
                    e.Item.Cells[11].Attributes.Add("onmouseout", "this.style.backgroundColor='#ffedad';this.style.cursor='pointer'");
                    e.Item.Cells[11].Style["cursor"] = "hand";
                }
                #endregion

                QueryString qs = new QueryString();
                qs.Add("IDRapportoControlloTecnico", e.Item.Cells[1].Text);
                qs.Add("IDTipologiaRCT", e.Item.Cells[10].Text);
                qs.Add("IDSoggetto", e.Item.Cells[2].Text);
                qs.Add("IDSoggettoDerived", e.Item.Cells[3].Text);
                qs.Add("codiceTargaturaImpianto", "");
                QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                string url = "RCT_RapportoDiControlloTecnicoNuovo.aspx";
                url += qsEncrypted.ToString();
                e.Item.Cells[11].Attributes.Add("onclick", "javascript:location.href='" + url + "'");
            }
            else
            {
                lblRapportoDiControllo.Visible = false;
            }

            CheckBox chkSelezione = (CheckBox)(e.Item.Cells[11].FindControl("chkSelezione"));
            Label lblTitoloDataDisattivazione = (Label)(e.Item.Cells[11].FindControl("lblTitoloDataDisattivazione"));
            if (!bool.Parse(e.Item.Cells[14].Text))
            {
                chkSelezione.Visible = false;
                lblTitoloDataDisattivazione.Visible = true;
            }


            Image imgBarcode = (Image) (e.Item.Cells[11].FindControl("imgBarcode"));
            string pathBarcode = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadBolliniCalorePulito"] + @"\" + e.Item.Cells[4].Text.ToString() + ".png";
            if (!System.IO.File.Exists(pathBarcode))
            {
                UtilityBollini.GetBarCodeUrl(e.Item.Cells[4].Text, e.Item.Cells[4].Text);
            }

            imgBarcode.ImageUrl = "~/" + ConfigurationManager.AppSettings["UploadBolliniCalorePulito"].ToString() + "/" + e.Item.Cells[4].Text + ".png";
        }
    }

    public void RowCommand(Object sender, CommandEventArgs e)
    {

    }

    #endregion
    
    protected void RCT_BollinoCalorePulitoSearch_btnProcess_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetBolliniCalorePulito();
            if (DataGrid.Items.Count > 0)
            {
                DataGrid.Items[0].Cells[12].Focus();
            }
        }
    }

    protected void chkSelezione_CheckedChanged(object sender, EventArgs e)
    {
        StampaLogic();
    }

    public void StampaLogic()
    {
        bool fVisible = false;
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox) DataGrid.Items[i].Cells[12].FindControl("chkSelezione");
            if (chk.Checked)
            {
                fVisible = true;
                break;
            }
        }
        rowStampa.Visible = fVisible;

        List<string> bollini = new List<string>();
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox) DataGrid.Items[i].Cells[12].FindControl("chkSelezione");
            if (chk.Checked)
            {
                bollini.Add(DataGrid.Items[i].Cells[0].Text);
            }
        }

        if (bollini.Count > 0)
        {
            string Strbollini = "";
            foreach (var item in bollini)
            {
                Strbollini += item + "|1,";
            }

            if (Strbollini.Length > 0)
            {
                Strbollini = Strbollini.Substring(0, Strbollini.Length - 1);
            }

            QueryString qs = new QueryString();
            qs.Add("StrBollinoCalorePulito", Strbollini);

            if (rblFormatoStampa.SelectedItem.Value.ToString() == "0")
            {
                qs.Add("BollinoType", "StampaBollinoA4");
            }
            else if (rblFormatoStampa.SelectedItem.Value.ToString() == "1")
            {
                qs.Add("BollinoType", "StampaBollinoA7");
            }
            else if (rblFormatoStampa.SelectedItem.Value.ToString() == "2")
            {
                qs.Add("BollinoType", "StampaBollinoEtichetta");
            }

            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "RCT_BolliniCalorePulitoViewer.aspx";
            url += qsEncrypted.ToString();

            btnStampa.Attributes.Add("onclick", "dhtmlwindow.open('Bollini_" + "', 'iframe', '" + url + "', 'Bollini " + "', 'width=750px,height=500px,resize=1,scrolling=1,center=1'); return false");
        }
    }
    
    protected void rblFormatoStampa_SelectedIndexChanged(object sender, EventArgs e)
    {
        StampaLogic();
    }

    protected void ASPxComboBox3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DataGrid.Items.Count > 0)
        {
            GetBolliniCalorePulito();
            DataGrid.Items[0].Cells[12].Focus();
        }
    }

    protected void chkSelezioneAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox) DataGrid.Items[i].Cells[12].FindControl("chkSelezione");
            if (chk.Enabled)
            {
                chk.Checked = ((CheckBox) sender).Checked;
                StampaLogic();
            }
        }
    }
    
}
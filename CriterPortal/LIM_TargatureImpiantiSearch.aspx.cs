using System.Data.SqlClient;
using DataUtilityCore;
using System;
using System.Configuration;
using System.Web.UI.WebControls;
using EncryptionQS;
using DevExpress.Web;
using System.Collections.Generic;
using System.Web.UI;
using System.Web;

public partial class LIM_TargatureImpiantiSearch : System.Web.UI.Page
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
            else
            {
                #region Encrypt off
                try
                {
                    if (Request.QueryString["iDSoggetto"] != null)
                    {
                        return (string)Request.QueryString["iDSoggetto"];
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

    protected string iDSoggettoDerived
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
            else
            {
                #region Encrypt off
                try
                {
                    if (Request.QueryString["iDSoggettoDerived"] != null)
                    {
                        return (string)Request.QueryString["iDSoggettoDerived"];
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

    protected string codiceLotto
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
                            return (string)qsdec[2];
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
                    if (Request.QueryString["codiceLotto"] != null)
                    {
                        return (string)Request.QueryString["codiceLotto"];
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
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "LIM_TargatureImpiantiSearch.aspx");

        if (!Page.IsPostBack)
        {
            PagePermission();
            InfoDefault();
            LoadAllDropDownlist();
            
            ASPxComboBox1.Focus();
        }
    }

    public void InfoDefault()
    {
        if ((codiceLotto != "") && (iDSoggetto != ""))
        {
            ASPxComboBox1.Value = iDSoggetto;
            
            GetComboBoxFilterByIDAzienda();
            if (iDSoggettoDerived != "")
            {
                ASPxComboBox2.Value = iDSoggettoDerived;
                ASPxComboBox2.SelectedItem = ASPxComboBox2.Items.FindByValue(int.Parse(iDSoggettoDerived));
            }
            
            rblStatoTargaturaCodiciImpianto.SelectedItem.Value = "1";
            ddCodiciLotto(codiceLotto, ASPxComboBox1.Value, ASPxComboBox2.Value);
            GetTargatureImpianti();
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

    #region DROPDOWNLIST / RADIOBUTTONLIST / CHECKBOXLIST
    protected void LoadAllDropDownlist()
    {
        ddCodiciLotto(codiceLotto, ASPxComboBox1.Value, ASPxComboBox2.Value);
    }

    protected void ddCodiciLotto(object iDPresel, object iDSoggetto, object iDSoggettoDerived)
    {
        if (iDSoggetto != null)
        {
            SqlDataReader dr = LoadDropDownList.LoadDropDownList_SYS_CodiciLottoTargaturaImpianti(iDPresel, iDSoggetto, iDSoggettoDerived);
            ddlCodiciLotto.ValueField = "CodiceLotto";
            ddlCodiciLotto.TextField = "DescrizioneLotto";
            ddlCodiciLotto.DataSource = dr;
            ddlCodiciLotto.DataBind();
            dr.Close();

            ListEditItem myItem = new ListEditItem("-- Selezionare --", "0");
            ddlCodiciLotto.Items.Insert(0, myItem);

            if (codiceLotto != "")
            {
                ddlCodiciLotto.Value = codiceLotto;
            }
            else
            {
                ddlCodiciLotto.SelectedIndex = 0;
            }
        }
    }
    
    #region RICERCA AZIENDE
    protected void ASPxComboBox_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "2", iDSoggetto, "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
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
        LoadAllDropDownlist();
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
        LoadAllDropDownlist();
    }

    #endregion

    #endregion

    #region TARGATURE IMPIANTI
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpressionTargature"] == null)
                return string.Empty;
            return ViewState["SortExpressionTargature"].ToString();
        }
        set
        {
            ViewState["SortExpressionTargature"] = value;
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
        string sql = buildStr();// Session["sqlstrTargature"].ToString();
        string currentSortExpression = this.SortExpression;
        DataGrid.PageSize = int.Parse(ASPxComboBox3.Value.ToString());

        //int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, (int)Session["currentPageIndexTargature"], DataGrid.PageSize);
        int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, this.DataGrid.CurrentPageIndex, DataGrid.PageSize);
        this.SortExpression = currentSortExpression;

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "TARGATURE IMPIANTI CORRISPONDENTI AI PARAMETRI IMPOSTATI");
    }

    private string buildStr()
    {
        object codiceLotto = null;
        
        try
        {
            if (ddlCodiciLotto.Value.ToString() != "0")
            {
                codiceLotto = ddlCodiciLotto.Value;
            }
        }
        catch (Exception)
        {
            
        }
        
        string strSql = UtilityTargaturaImpianti.GetSqlValoriTargatureFilter(ASPxComboBox1.Value, ASPxComboBox2.Value, codiceLotto, rblStatoTargaturaCodiciImpianto.SelectedItem.Value, txtCodiceTargatura.Text);
        return strSql;
    }

    public void DataGrid_PageChanger(object source, DataGridPageChangedEventArgs e)
    {
        //Session["currentPageIndexTargature"] = e.NewPageIndex;
        //DataGrid.CurrentPageIndex = (int)Session["currentPageIndexTargature"];
        DataGrid.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
    }

    public void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Label lblLibrettoImpianto = (Label)(e.Item.Cells[5].FindControl("lblLibrettoImpianto"));
            if ((e.Item.Cells[1].Text != "") && (e.Item.Cells[1].Text != "&nbsp;"))
            {
                lblLibrettoImpianto.Visible = true;
                #region STILE
                if (e.Item.ItemType == ListItemType.Item)
                {
                    e.Item.Cells[5].Attributes.Add("onmouseover", "this.style.backgroundColor='#f6d18a';this.style.cursor='pointer'");
                    e.Item.Cells[5].Attributes.Add("onmouseout", "this.style.backgroundColor='#f5f2f2';this.style.cursor='pointer'");
                    e.Item.Cells[5].Style["cursor"] = "hand";
                }
                else if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    e.Item.Cells[5].Attributes.Add("onmouseover", "this.style.backgroundColor='#f6d18a';this.style.cursor='pointer'");
                    e.Item.Cells[5].Attributes.Add("onmouseout", "this.style.backgroundColor='#ffedad';this.style.cursor='pointer'");
                    e.Item.Cells[5].Style["cursor"] = "hand";
                }
                #endregion

                if (ConfigurationManager.AppSettings["fEncryptQueryString"] == "on")
                {
                    QueryString qs = new QueryString();
                    qs.Add("IDLibrettoImpianto", e.Item.Cells[1].Text);
                    QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                    string url = "LIM_LibrettiImpianti.aspx";
                    url += qsEncrypted.ToString();
                    e.Item.Cells[5].Attributes.Add("onclick", "javascript:location.href='"+ url +"'");
                }
                else
                {
                    e.Item.Cells[5].Attributes.Add("onclick", "javascript:location.href='LIM_LibrettiImpianti.aspx?IDLibrettoImpianto=" + e.Item.Cells[1].Text + "'");  
                }
            }
            else
            {
                lblLibrettoImpianto.Visible = false;
            }

            Image imgBarcode = (Image)(e.Item.Cells[5].FindControl("imgBarcode"));
            
            string pathBarcode = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadTargatureImpianti"] + @"\" + e.Item.Cells[4].Text.ToString() + ".png";
            if (!System.IO.File.Exists(pathBarcode))
            {
                UtilityTargaturaImpianti.GetBarCodeUrl(e.Item.Cells[4].Text, e.Item.Cells[4].Text);
            }
            
            imgBarcode.ImageUrl = "~/" + ConfigurationManager.AppSettings["UploadTargatureImpianti"].ToString() + "/" + e.Item.Cells[4].Text + ".png";
        }
    }

    public void RowCommand(Object sender, CommandEventArgs e)
    {
        
    }

    #endregion

    public void GetTargatureImpianti()
    {
        System.Threading.Thread.Sleep(500);
        //Session["sqlstrTargature"] = buildStr();
        //Session["currentPageIndexTargature"] = 0;
        DataGrid.CurrentPageIndex = 0;
        BindGrid(true);
    }

    protected void LIM_TargatureImpiantiSearch_btnProcess_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetTargatureImpianti();
            //if (DataGrid.Items.Count > 0)
            //{
            //    DataGrid.Items[0].Cells[12].Focus();
            //}
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
            CheckBox chk = (CheckBox) DataGrid.Items[i].Cells[6].FindControl("chkSelezione");
            if (chk.Checked)
            {
                fVisible = true;
                break;
            }
        }

        rowStampa.Visible = fVisible;

        List<string> targature = new List<string>();
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox) DataGrid.Items[i].Cells[6].FindControl("chkSelezione");
            if (chk.Checked)
            {
                targature.Add(DataGrid.Items[i].Cells[0].Text);
            }
        }

        if (targature.Count > 0)
        {
            string StrTargaturaImpianto = "";
            foreach (var item in targature)
            {
                StrTargaturaImpianto += item + "|1,";
            }

            if (StrTargaturaImpianto.Length > 0)
            {
                StrTargaturaImpianto = StrTargaturaImpianto.Substring(0, StrTargaturaImpianto.Length - 1);
            }

            QueryString qs = new QueryString();
            qs.Add("StrTargaturaImpianto", StrTargaturaImpianto);
            if (rblFormatoStampa.SelectedItem.Value.ToString() == "0")
            {
                qs.Add("TargaturaType", "StampaTargaturaA4");
            }
            else if (rblFormatoStampa.SelectedItem.Value.ToString() == "1")
            {
                qs.Add("TargaturaType", "StampaTargaturaA7");
            }
            else if (rblFormatoStampa.SelectedItem.Value.ToString() == "2")
            {
                qs.Add("TargaturaType", "StampaTargaturaEtichetta");
            }

            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "LIM_TargatureViewer.aspx";
            url += qsEncrypted.ToString();

            btnStampa.Attributes.Add("onclick", "dhtmlwindow.open('Targatura_" + "', 'iframe', '" + url + "', 'Targatura " + "', 'width=750px,height=500px,resize=1,scrolling=1,center=1'); return false");
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
            GetTargatureImpianti();
            //DataGrid.Items[0].Cells[12].Focus();
        }
    }
    
    protected void chkSelezioneAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox) DataGrid.Items[i].Cells[6].FindControl("chkSelezione");
            if (chk.Enabled)
            {
                chk.Checked = ((CheckBox) sender).Checked;
                StampaLogic();
            }
        }
    }

}
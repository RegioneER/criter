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

public partial class RCT_BollinoCalorePulitoConvert : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PagePermission();
            LoadAllDropDownlist();
        }
    }

    #region DROPDOWNLIST / RADIOBUTTONLIST / CHECKBOXLIST
    protected void LoadAllDropDownlist()
    {
        ddCodiciLotto(null, ASPxComboBox1.Value);
    }

    protected void ddCodiciLotto(object iDPresel, object iDSoggetto)
    {
        SqlDataReader dr = LoadDropDownList.LoadDropDownList_RCT_LottiBolliniCalorePulito(iDPresel, iDSoggetto, true);
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

    protected void ASPxComboBox_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetComboBoxFilterByIDAzienda();
        LoadAllDropDownlist();
    }

    protected void GetComboBoxFilterByIDAzienda()
    {
        if (ASPxComboBox1.Value != null)
        {
            
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
                lblSoggetto.Visible = false;
                rfvASPxComboBox1.Enabled = false;
                break;
            case "2": //Amministratore azienda
                ASPxComboBox1.Value = getVal[0].ToString();
                ASPxComboBox1.Visible = false;
                GetComboBoxFilterByIDAzienda();

                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0].ToString());
                lblSoggetto.Visible = true;
                break;
            case "3": //Operatore/Addetto
                ASPxComboBox1.Visible = false;
                lblSoggetto.Visible = true;
                ASPxComboBox1.Value = getVal[2].ToString();
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                break;
            case "10": //Responsabile tecnico
                ASPxComboBox1.Value = getVal[2].ToString();
                ASPxComboBox1.Visible = false;
                GetComboBoxFilterByIDAzienda();

                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                lblSoggetto.Visible = true;
                break;
        }
    }

    private void GetBolliniCalorePulito()
    {
        System.Threading.Thread.Sleep(500);
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
        string sql = buildStr();
        string currentSortExpression = this.SortExpression;
        DataGrid.PageSize = int.Parse(ASPxComboBox3.Value.ToString());

        int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, this.DataGrid.CurrentPageIndex, DataGrid.PageSize);
        this.SortExpression = currentSortExpression;

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "BOLLINI CALORE PULITO CORRISPONDENTI AI PARAMETRI IMPOSTATI");
    }

    private string buildStr()
    {
        string strSql = UtilityBollini.GetSqlValoriBolliniFilter(ASPxComboBox1.Value,
                                                                 null,
                                                                 ddlCodiciLotto.SelectedItem.Value,
                                                                 1,
                                                                 txtCodiceBollino.Text,
                                                                 1,
                                                                 7);
        return strSql;
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
            Image imgBarcode = (Image)(e.Item.Cells[11].FindControl("imgBarcode"));
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
        ConvertiBollini();
    }

    public List<long> bolliniDaConvertire
    {
        get
        {
            if (ViewState["BolliniDaConvertireSelezionati"] == null)
            {
                ViewState["BolliniDaConvertireSelezionati"] = new List<long>();

            }
            return (List<long>)ViewState["BolliniDaConvertireSelezionati"];
        }
        set
        {
            ViewState["BolliniDaConvertireSelezionati"] = value;
        }
    }

    public void ConvertiBollini()
    {
        bool fVisible = false;
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[12].FindControl("chkSelezione");
            if (chk.Checked)
            {
                fVisible = true;
                break;
            }
        }
        rowConvertiBollini.Visible = fVisible;

        //List<long> bolliniDaConvertire = new List<long>();
        bolliniDaConvertire.Clear();
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[12].FindControl("chkSelezione");
            if (chk.Checked)
            {
                long iDBollinoCalorePulito = long.Parse(DataGrid.Items[i].Cells[0].Text);
                bolliniDaConvertire.Add(iDBollinoCalorePulito);
            }
        }

        //Avviso per l'utente del numero di bollini risultanti
        int bolliniTotali = (bolliniDaConvertire.Count * 4);
        lblTotaleBolliniConvert.Text = bolliniTotali.ToString();              
    }

    public void RedirectPage(string iDSoggetto, string iDMovimento)
    {
        QueryString qs = new QueryString();
        qs.Add("iDSoggetto", iDSoggetto);
        qs.Add("iDMovimento", iDMovimento);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "MNG_Portafoglio.aspx";
        url += qsEncrypted.ToString();
        Response.Redirect(url);
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
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[12].FindControl("chkSelezione");
            if (chk.Enabled)
            {
                chk.Checked = ((CheckBox)sender).Checked;
                ConvertiBollini();
            }
        }
    }


    protected void btnConvertiBollini_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            //Logica di conversione bollini
            if (ASPxComboBox1.Value != null && bolliniDaConvertire.Count > 0)
            {
                int iDSoggetto = int.Parse(ASPxComboBox1.Value.ToString());
                var response = UtilityBollini.ConvertiBolliniCalorePulito(bolliniDaConvertire, iDSoggetto).GetAwaiter().GetResult();

                if (response.Item1)
                {
                    RedirectPage(iDSoggetto.ToString(), response.Item3);
                }
                else
                {
                    //Qualcosa è andata male
                    lblFeedBackConversioneBollini.Text = response.Item2;
                }
            }
        }
    }
}
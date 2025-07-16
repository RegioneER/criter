using DataUtilityCore;
using DevExpress.Web;
using EncryptionQS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class COM_ContrattoIspettoreSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "COM_QualificaIspettoreSearch.aspx");
        if (!Page.IsPostBack)
        {
            cmbIspettore.Focus();
            PagePermission();
            LoadAllDropDownlist();
        }
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                cmbIspettore.Visible = true;
                lblSoggetto.Visible = false;
                break;
            case "7": //Ispettore
                cmbIspettore.Value = getVal[0];
                cmbIspettore.Visible = false;
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0]);
                lblSoggetto.Visible = true;
                break;
        }
    }

    #region ISPETTORE 

    protected void cmbIspettore_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "7", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void cmbIspettore_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;
        int IdSoggetto = int.Parse(e.Value.ToString());
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "7", e.Value.ToString(), "");
        comboBox.DataBind();
    }

    protected void cmbIspettore_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBox();
    }

    protected void RefreshAspxComboBox()
    {
        cmbIspettore.SelectedIndex = -1;
        cmbIspettore.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "7", "", "", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
        cmbIspettore.DataBind();
    }


    protected void LoadAllDropDownlist()
    {
        rbStatoContratto(null);
    }

    protected void rbStatoContratto(int? idPresel)
    {
        rblStatoContratto.Items.Clear();
        rblStatoContratto.DataValueField = "IDStatoContratto";
        rblStatoContratto.DataTextField = "StatoContratto";
        rblStatoContratto.DataSource = LoadDropDownList.LoadDropDownList_SYS_StatoContratto(idPresel);
        rblStatoContratto.DataBind();

        ListItem myItem = new ListItem("Tutti i Stati", "0");
        rblStatoContratto.Items.Insert(0, myItem);

        rblStatoContratto.SelectedIndex = 0;
    }

    #endregion

    #region RICERCA CONTRATTI ISPETTORI
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpressionContrattoIspettore"] == null)
                return string.Empty;
            return ViewState["SortExpressionContrattoIspettore"].ToString();
        }
        set
        {
            ViewState["SortExpressionContrattoIspettore"] = value;
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

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "CONTRATTO ISPETTORE");
    }

    private string BuildStr()
    {
        string strSql = UtilitySoggetti.GetSqlValoriContrattoIspettoreFilter(cmbIspettore.Value,
                                                                              txtNumeroIspezioniMax.Text,
                                                                              rblStatoContratto.SelectedIndex.ToString(),
                                                                              txtDataInizioDa.Text,
                                                                              txtDataInizioAl.Text,
                                                                              txtDataFineDa.Text,
                                                                              txtDataInizioAl.Text,
                                                                              cbAttivo.Checked);

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
            ImageButton imgEdit = (ImageButton)(e.Item.Cells[2].FindControl("ImgEdit"));
        }
    }

    public void RowCommand(Object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        if (e.CommandName == "Edit")
        {
            QueryString qs = new QueryString();
            qs.Add("IDContrattoIspettore", commandArgs[0]);
            qs.Add("IDIspettore", commandArgs[1]);

            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "COM_ContrattoIspettore.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
    }

    public void GetContrattoIspettore()
    {
        System.Threading.Thread.Sleep(500);
        DataGrid.CurrentPageIndex = 0;
        BindGrid(true);
    }


    #endregion

    protected void btnRicerca_Click(object sender, EventArgs e)
    {
        GetContrattoIspettore();
    }

    protected void btnNuovo_Click(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["fEncryptQueryString"] == "on")
        {
            QueryString qs = new QueryString();
            qs.Add("IDContrattoIspettore", string.Empty);
            qs.Add("IDIspettore", string.Empty);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "COM_ContrattoIspettore.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
    }
}
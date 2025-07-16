using DataUtilityCore;
using DevExpress.Web;
using EncryptionQS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VER_SanzioniSearch : System.Web.UI.Page
{
    UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "VER_SanzioniSearch.aspx");

        if (!Page.IsPostBack)
        {
            LoadAllDropDownlist();
            GetAccertamentiSanzioni();
            PagePermission();
        }
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter

                break;
            case "6": //Accertatore

                break;
            case "7": //Coordinatore

                break;
            case "8": //Ispettore

                break;
            case "9": //Segreteria Verifiche

                break;
        }
    }

    protected void ddStatoAccertamentoSanzione(int? idPresel)
    {
        rblStatoSanzione.Items.Clear();
        rblStatoSanzione.DataValueField = "IDStatoAccertamentoSanzione";
        rblStatoSanzione.DataTextField = "StatoAccertamentoSanzione";
        rblStatoSanzione.DataSource = LoadDropDownList.LoadDropDownList_SYS_StatoAccertamentoSanzione(idPresel);
        rblStatoSanzione.DataBind();

        ListItem myItem = new ListItem("Tutti gli stati", "0");
        rblStatoSanzione.Items.Insert(0, myItem);

        rblStatoSanzione.SelectedIndex = 0;
    }

    protected void LoadAllDropDownlist()
    {
        ddStatoAccertamentoSanzione(null);
    }
    
    #region ACCERTAMENTI SANZIONI
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpressionSanzioni"] == null)
                return string.Empty;
            return ViewState["SortExpressionSanzioni"].ToString();
        }
        set
        {
            ViewState["SortExpressionSanzioni"] = value;
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
        //int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, (int)Session["currentPageIndexAccertamenti"], DataGrid.PageSize);
        int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, this.DataGrid.CurrentPageIndex, this.DataGrid.PageSize);
        this.SortExpression = currentSortExpression;

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "NOTIFICHE VERBALI SANZIONI CORRISPONDENTI AI PARAMETRI IMPOSTATI");
    }

    private string BuildStr()
    {
        string strSql = UtilityVerifiche.GetSqlValoriSanzioniFilter(null,
                                                                    string.Empty,
                                                                    txtCodiceAccertamento.Text,
                                                                    txtCodiceTargatura.Text,
                                                                    rblStatoSanzione.SelectedItem.Value
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
            QueryString qs = new QueryString();
            qs.Add("IDAccertamento", e.Item.Cells[0].Text);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            //string[] getVal = new string[2];
            //getVal = UtilityVerifiche.GetProvenienzaSanzione(long.Parse(e.Item.Cells[0].Text));

            string url = "VER_Sanzioni.aspx";//getVal[0];

            url += qsEncrypted.ToString();

            ImageButton ImgView = (ImageButton)(e.Item.Cells[6].FindControl("ImgView"));
            ImgView.OnClientClick = "javascript:window.open('"+ url + "','newWindow');";
        }
    }
       

    public void RowCommand(object sender, CommandEventArgs e)
    {
        
    }

    #endregion

    public void GetAccertamentiSanzioni()
    {
        System.Threading.Thread.Sleep(500);
        DataGrid.CurrentPageIndex = 0;
        BindGrid(true);
    }

    protected void btnRicerca_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetAccertamentiSanzioni();
        }
    }

}
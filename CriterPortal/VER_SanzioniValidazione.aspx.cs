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

public partial class VER_SanzioniValidazione : System.Web.UI.Page
{
    UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "VER_SanzioniValidazione.aspx");

        if (!Page.IsPostBack)
        {
            GetAccertamentiSanzioni();
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

    protected void LoadAllDropDownlist()
    {
        
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
        string sql = BuildStr();// Session["sqlstrAccertamenti"].ToString();
        string currentSortExpression = this.SortExpression;
        //int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, (int)Session["currentPageIndexAccertamenti"], DataGrid.PageSize);
        int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, this.DataGrid.CurrentPageIndex, this.DataGrid.PageSize);
        this.SortExpression = currentSortExpression;

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "NOTIFICHE VERBALI SANZIONI DA VALIDARE CORRISPONDENTI AI PARAMETRI IMPOSTATI");
    }

    private string BuildStr()
    {
        string strSql = UtilityVerifiche.GetSqlValoriSanzioniFilter(null,
                                                                    string.Empty,
                                                                    txtCodiceAccertamento.Text,
                                                                    txtCodiceTargatura.Text,
                                                                    1
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
        //if(e.CommandName == "Validate")
        //{
        //    UtilityVerifiche.CambiaStatoSanzione(long.Parse(e.CommandArgument.ToString()), 2);

        //    BindGrid(true);
        //}
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

    protected void chkSelezione_CheckedChanged(object sender, EventArgs e)
    {
        bool fVisible = false;
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[7].FindControl("chkSelezione");
            if (chk.Checked)
            {
                fVisible = true;
                break;
            }
        }
        btnValidaSanzioni.Visible = fVisible;
    }

    protected void chkSelezioneAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[7].FindControl("chkSelezione");
            if (chk.Enabled)
            {
                chk.Checked = ((CheckBox)sender).Checked;
            }
        }

        bool fVisible = false;
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[7].FindControl("chkSelezione");
            if (chk.Checked)
            {
                fVisible = true;
                break;
            }
        }
        btnValidaSanzioni.Visible = fVisible;
    }

    protected void btnValidaSanzioni_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ValidaSanzioniLogic();
        }
    }

    protected void ValidaSanzioniLogic()
    {
        try
        {
            for (int i = 0; i < DataGrid.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[7].FindControl("chkSelezione");
                if (chk.Checked)
                {
                    int IDAccertamento = int.Parse(DataGrid.Items[i].Cells[0].Text);
                    string codiceAccertamento = DataGrid.Items[i].Cells[2].Text;

                    UtilityVerifiche.CambiaStatoSanzione(IDAccertamento, 3);
                    UtilityPosteItaliane.SendToPosteItaliane(IDAccertamento, (int)DataUtilityCore.Enum.EnumTypeofRaccomandata.TypeSanzione);
                    UtilityVerifiche.SetDataInvioSanzione(IDAccertamento);
                }
            }

            tblInfoValidaOk.Visible = true;
            tblAccertamenti.Visible = false;

            string url = "VER_SanzioniValidazione.aspx";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + url + "';},2000);", true);
        }
        catch (Exception)
        {

            throw;
        }
        
    }


}
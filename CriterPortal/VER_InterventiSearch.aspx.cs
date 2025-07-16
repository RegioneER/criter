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

public partial class VER_InterventiSearch : System.Web.UI.Page
{
    UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

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

    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "VER_InterventiSearch.aspx");

        if (!Page.IsPostBack)
        {
            LoadAllDropDownlist();
            ASPxComboBox1.Focus();
            cbCausale(null);
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
            case "14": //Coordinatore/Ispettore
                
                break;
        }
    }

    protected void LoadAllDropDownlist()
    {
        rbStatoIntervento(null);
    }

    protected void rbStatoIntervento(int? idPresel)
    {
        rblStatoIntervento.Items.Clear();
        rblStatoIntervento.DataValueField = "IDStatoAccertamentoIntervento";
        rblStatoIntervento.DataTextField = "StatoAccertamentoIntervento";
        rblStatoIntervento.DataSource = LoadDropDownList.LoadDropDownList_SYS_StatoAccertamentoIntervento(idPresel);
        rblStatoIntervento.DataBind();

        ListItem myItem = new ListItem("Tutti gli stati", "0");
        rblStatoIntervento.Items.Insert(0, myItem);

        rblStatoIntervento.SelectedIndex = 0;
    }

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

    #region RICERCA CAUSALE
    protected void cbCausale(int? idPresel)
    {
        cblCausale.Items.Clear();
        cblCausale.DataValueField = "IDCausale";
        cblCausale.DataTextField = "Causale";
        cblCausale.DataSource = LoadDropDownList.LoadDropDownList_SYS_CausaliRaccomandate(idPresel);
        cblCausale.DataBind();
    }
    #endregion

    #region INTERVENTI
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpressionInterventi"] == null)
                return string.Empty;
            return ViewState["SortExpressionInterventi"].ToString();
        }
        set
        {
            ViewState["SortExpressionInterventi"] = value;
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
        int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, this.DataGrid.CurrentPageIndex, DataGrid.PageSize);
        this.SortExpression = currentSortExpression;

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "INTERVENTI CORRISPONDENTI AI PARAMETRI IMPOSTATI");
    }

    private string BuildStr()
    {
        List<object> valoriCausale = new List<object>();
        foreach (ListItem item in cblCausale.Items)
        {
            if (item.Selected)
            {
                valoriCausale.Add(item.Value);
            }
        }
        
        string strSql = UtilityVerifiche.GetSqlValoriInterventiFilter(IDTipoAccertamento,
                                                                      ASPxComboBox1.Value,
                                                                      rblStatoIntervento.SelectedItem.Value,
                                                                      txtDataInvioRaccomandataDa.Text,
                                                                      txtDataInvioRaccomandataAl.Text,
                                                                      txtDataRicevimentoRaccomandataDa.Text,
                                                                      txtDataRicevimentoRaccomandataAl.Text,
                                                                      txtDataScadenzaInterventoDa.Text,
                                                                      txtDataScadenzaInterventoAl.Text,
                                                                      txtCodiceAccertamento.Text,
                                                                      txtCodiceTargatura.Text,
                                                                      valoriCausale.ToArray<object>(),
                                                                      0
                                                                      );
        return strSql.ToString();
    }

    public void DataGrid_PageChanger(object source, DataGridPageChangedEventArgs e)
    {
        //Session["currentPageIndexAccertamenti"] = e.NewPageIndex;
        //DataGrid.CurrentPageIndex = (int)Session["currentPageIndexAccertamenti"];
        DataGrid.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
    }

    public void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HyperLink lnkAccertamento = (HyperLink)(e.Item.Cells[2].FindControl("lnkAccertamento"));
            QueryString qs = new QueryString();
            qs.Add("IDAccertamento", e.Item.Cells[0].Text);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "VER_Accertamenti.aspx";
            url += qsEncrypted.ToString();

            lnkAccertamento.NavigateUrl = url;
            lnkAccertamento.Target = "_blank";

            DataGrid dgDocumenti = (DataGrid)e.Item.Cells[2].FindControl("dgDocumenti");
            GetDocumentiAccertamenti(dgDocumenti, long.Parse(e.Item.Cells[0].Text));
        }
    }

    public void RowCommand(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            QueryString qs = new QueryString();
            qs.Add("IDAccertamento", e.CommandArgument.ToString());
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "VER_Interventi.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
    }

    #endregion

    #region Documenti
    public void GetDocumentiAccertamenti(DataGrid dgDocumenti, long iDAccertamento)
    {
        var documenti = UtilityVerifiche.GetDocumentiAccertamenti(iDAccertamento);
        dgDocumenti.DataSource = documenti;
        dgDocumenti.DataBind();

        if (documenti.Count > 0)
        {
            dgDocumenti.Visible = true;
        }
        else
        {
            dgDocumenti.Visible = false;
        }
    }

    #endregion

    public void GetInterventi()
    {
        System.Threading.Thread.Sleep(500);
        //Session["sqlstrAccertamenti"] = BuildStr();
        //Session["currentPageIndexAccertamenti"] = 0;
        DataGrid.CurrentPageIndex = 0;
        BindGrid(true);
    }

    protected void btnRicerca_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetInterventi();
        }
    }
}
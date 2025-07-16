using DataUtilityCore;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class COM_QuestionarioSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "COM_QuestionarioSearch.aspx");

        if (!Page.IsPostBack)
        {
            PagePermission();
            LoadAllDropDownlist();
        }
    }

    protected void PagePermission()
    {
        
    }

    protected void LoadAllDropDownlist()
    {
        rbStatoQuestionario(null);
    }

    protected void rbStatoQuestionario(int? idPresel)
    {
        rblStatoQuestionario.Items.Clear();
        rblStatoQuestionario.DataValueField = "IDStatoQuestionario";
        rblStatoQuestionario.DataTextField = "StatoQuestionario";
        rblStatoQuestionario.DataSource = LoadDropDownList.LoadDropDownList_SYS_StatoQuestionario(idPresel);
        rblStatoQuestionario.DataBind();

        ListItem myItem = new ListItem("Tutti", "0");
        rblStatoQuestionario.Items.Insert(0, myItem);

        rblStatoQuestionario.SelectedIndex = 0;
    }

    #region QUESTIONARI
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpressionQuestionari"] == null)
                return string.Empty;
            return ViewState["SortExpressionQuestionari"].ToString();
        }
        set
        {
            ViewState["SortExpressionQuestionari"] = value;
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

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "LIBRETTI DI IMPIANTO SU CUI EFFETTUARE I QUESTIONARI");
    }

    private string BuildStr()
    {
        string strSql = UtilityQuestionari.GetSqlValoriQuestionariFilter(rblStatoQuestionario.SelectedItem.Value);
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
            ImageButton imgEffettuaQuestionario = (ImageButton)(e.Item.Cells[4].FindControl("imgEffettuaQuestionario"));
            imgEffettuaQuestionario.Attributes.Add("onclick", "OpenPopupWindows(this, "+ e.Item.Cells[0].Text + "); return false;");

            switch (e.Item.Cells[1].Text.Replace("&nbsp;", ""))
            {
                case "": //Questionario non presente
                    imgEffettuaQuestionario.ImageUrl = "~/images/buttons/call1.png";
                    break;
                case "1": //Questionario iniziato
                    imgEffettuaQuestionario.ImageUrl = "~/images/buttons/call2.png";
                    break;
                case "2": //Questionario completato
                    imgEffettuaQuestionario.ImageUrl = "~/images/buttons/call3.png";
                    break;
                case "3": //Questionario senza risposta
                    imgEffettuaQuestionario.ImageUrl = "~/images/buttons/delete.png";
                    break;
            }

            Label lblResponsabile = (Label)(e.Item.Cells[3].FindControl("lblResponsabile"));
            Label lblResponsabileIndirizzo = (Label)(e.Item.Cells[3].FindControl("lblResponsabileIndirizzo"));
            
            if (!string.IsNullOrEmpty(e.Item.Cells[5].Text.Replace("&nbsp;", "")) && !string.IsNullOrEmpty(e.Item.Cells[6].Text.Replace("&nbsp;", "")))
            {
                lblResponsabile.Text = e.Item.Cells[5].Text.Replace("&nbsp;", "") + " " + e.Item.Cells[6].Text.Replace("&nbsp;", "");
                
            }
            else
            {
                lblResponsabile.Text = e.Item.Cells[7].Text.Replace("&nbsp;", "");
            }
            lblResponsabileIndirizzo.Text = e.Item.Cells[9].Text + " " + e.Item.Cells[10].Text + " " + e.Item.Cells[11].Text;
        }
    }

    #endregion

    public void GetQuestionari()
    {
        System.Threading.Thread.Sleep(500);
        DataGrid.CurrentPageIndex = 0;
        BindGrid(true);
    }

    protected void btnRicerca_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetQuestionari();
        }
    }
    

}
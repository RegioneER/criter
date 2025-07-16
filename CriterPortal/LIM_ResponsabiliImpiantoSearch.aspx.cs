using DataUtilityCore;
using EncryptionQS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class LIM_ResponsabiliImpiantoSearch : System.Web.UI.Page
{
    protected string iDLibrettoImpianto
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
            
            return "";
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "LIM_ResponsabiliImpiantoSearch.aspx");

        if (!Page.IsPostBack)
        {
            PagePermission();
            ASPxComboBox1.Focus();
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
                rfvASPxComboBox1.Enabled = false;
                break;
            case "2": //Amministratore azienda
            case "5": //Terzo responsabile
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
            case "10": //Responsabile tecnico
                ASPxComboBox1.Value = getVal[2].ToString();
                ASPxComboBox1.Visible = false;

                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                lblSoggetto.Visible = true;
                break;
        }
    }

    #region DROPDOWNLIST / RADIOBUTTONLIST / CHECKBOXLIST
    
    #region RICERCA AZIENDE
    protected void ASPxComboBox_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox) source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(true, "2,3", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox) source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(true, "2,3", e.Value.ToString(), "");
        comboBox.DataBind();
    }
    
    protected void ASPxComboBox1_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBox();
    }

    protected void RefreshAspxComboBox()
    {
        ASPxComboBox1.SelectedIndex = -1;
        ASPxComboBox1.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(true, "2,3", "", "", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
        ASPxComboBox1.DataBind();
    }

    #endregion
    
    #region CODICE CATASTALE / DATI CATASTALI
    protected void ASPxComboBox3_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox) source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString(), true);
        comboBox.DataBind();
    }

    protected void ASPxComboBox3_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox) source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastaliRequestedByValue(e.Value.ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox3_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBox3();
    }

    protected void RefreshAspxComboBox3()
    {
        ASPxComboBox3.SelectedIndex = -1;
        ASPxComboBox3.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString(), true);
        ASPxComboBox3.DataBind();
    }
    #endregion

    #endregion

    #region LIBRETTI IMPIANTI
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpressionLibretti"] == null)
                return string.Empty;
            return ViewState["SortExpressionLibretti"].ToString();
        }
        set
        {
            ViewState["SortExpressionLibretti"] = value;
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
        string sql = BuildStr();// Session["sqlstrLibretti"].ToString();
        string currentSortExpression = this.SortExpression;
        //int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, (int) Session["currentPageIndexLibretti"], DataGrid.PageSize);
        int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, this.DataGrid.CurrentPageIndex, DataGrid.PageSize);
        this.SortExpression = currentSortExpression;

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "LIBRETTI IMPIANTI CORRISPONDENTI AI PARAMETRI IMPOSTATI");
    }

    private string BuildStr()
    {
        string strSql = UtilityLibrettiImpianti.GetSqlValoriLibrettiResponsabiliFilter(ASPxComboBox1.Value,
                                                                                       null,
                                                                                       txtCodiceTargatura.Text,
                                                                                       ASPxComboBox3.Value,
                                                                                       txtFoglio.Text,
                                                                                       txtMappale.Text,
                                                                                       txtSubalterno.Text,
                                                                                       txtIdentificativo.Text,
                                                                                       2,
                                                                                       txtResponsabile.Text,
                                                                                       txtCfPIvaResponsabile.Text,
                                                                                       txtCodicePod.Text,
                                                                                       txtCodicePdr.Text);

        return strSql.ToString();
    }

    public void DataGrid_PageChanger(object source, DataGridPageChangedEventArgs e)
    {
        //Session["currentPageIndexLibretti"] = e.NewPageIndex;
        //DataGrid.CurrentPageIndex = (int) Session["currentPageIndexLibretti"];
        DataGrid.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
    }

    public void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            ImageButton imgPdf = (ImageButton) (e.Item.Cells[7].FindControl("ImgPdf"));
            ImageButton imgDelete = (ImageButton) (e.Item.Cells[8].FindControl("ImgDelete"));

            string pathPdfFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadLibrettiImpianti"] + @"\" + "LibrettoImpianto_" + e.Item.Cells[0].Text.ToString() + ".pdf";
            if (System.IO.File.Exists(pathPdfFile))
            {
                imgPdf.Visible = true;
                imgPdf.Attributes.Add("onclick", "var winLibretto=dhtmlwindow.open('Libretto_" + e.Item.Cells[0].Text + "', 'iframe', 'LIM_LibrettiImpiantiViewer.aspx?IDLibrettoImpianto=" + e.Item.Cells[0].Text + "', 'Libretto_" + e.Item.Cells[0].Text + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); ");
            }
            else
            {
                imgPdf.Visible = false;
            }
        }
    }

    public void RowCommand(Object sender, CommandEventArgs e)
    {
        if (e.CommandName == "RevocaIncarico")
        {
            int iDLibrettoImpianto = Int32.Parse(e.CommandArgument.ToString());
            UtilityLibrettiImpianti.RecovaIncaricoTerzoResponsabile(iDLibrettoImpianto);
            BindGrid(true);
        }
        else if (e.CommandName == "Pdf")
        {

        }
    }

    #endregion

    public void GetLibrettiImpianti()
    {
        System.Threading.Thread.Sleep(500);
        //Session["sqlstrLibretti"] = BuildStr();
        //Session["currentPageIndexLibretti"] = 0;
        DataGrid.CurrentPageIndex = 0;
        BindGrid(true);
    }

    protected void LIM_LibrettiImpiantiSearch_btnProcess_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetLibrettiImpianti();
            if (DataGrid.Items.Count > 0)
            {
                if (DataGrid.Items[0].Cells[7].Text == "")
                {
                    DataGrid.Items[0].Cells[8].Focus();
                }
                else
                {
                    DataGrid.Items[0].Cells[7].Focus();
                }
            }
        }
    }
    
}
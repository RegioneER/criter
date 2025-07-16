using DataUtilityCore;
using EncryptionQS;
using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class LIM_LibrettiImpiantiSearch : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "LIM_LibrettiImpiantiSearch.aspx");

        if (!Page.IsPostBack)
        {
            LoadAllDropDownlist();
            PagePermission();
            ASPxComboBox1.Focus();

            UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
            if (info.IDRuolo == 13)
            {
                GetLibrettiImpianti();
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
            case "12": //Segreteria
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
            case "11": //Software house
                ASPxComboBox1.Visible = false;
                ASPxComboBox2.Visible = false;
                lblSoggetto.Visible = false;
                lblSoggettoDerived.Visible = false;
                rfvASPxComboBox1.Enabled = false;
                rfvASPxComboBox2.Enabled = false;
                rowAzienda.Visible = false;
                rowManutentore.Visible = false;
                LIM_LibrettiImpianti_btnAdd.Visible = false;

                UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
                lblApiKey.Text = info.KeyApi;
                break;
            case "13": //Cittadino
                ASPxComboBox1.Visible = false;
                ASPxComboBox2.Visible = false;
                lblSoggetto.Visible = false;
                lblSoggettoDerived.Visible = false;
                rfvASPxComboBox1.Enabled = false;
                rfvASPxComboBox2.Enabled = false;
                rowAzienda.Visible = false;
                rowManutentore.Visible = false;
                LIM_LibrettiImpianti_btnAdd.Visible = false;
                rowResponsabile.Visible = false;
                rowResponsabileCfPIva.Visible = false;
                rowStatoLibretto.Visible = false;
                rblStatoLibrettoImpianto.SelectedValue = "2";
                UserInfo infoUser = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
                txtCfPIvaResponsabile.Text = infoUser.cfPIva;
                break;
            case "16": //Ente locale
                ASPxComboBox1.Visible = false;
                ASPxComboBox2.Visible = false;
                lblSoggetto.Visible = false;
                lblSoggettoDerived.Visible = false;
                rfvASPxComboBox1.Enabled = false;
                rfvASPxComboBox2.Enabled = false;
                rowAzienda.Visible = false;
                rowManutentore.Visible = false;
                LIM_LibrettiImpianti_btnAdd.Visible = false;
                rowResponsabileCfPIva.Visible = false;
                rowStatoLibretto.Visible = false;
                rblStatoLibrettoImpianto.SelectedValue = "2";
                rowCodicePod.Visible = false;
                rowCodicePdr.Visible = false;
                rowDataRegistrazione.Visible = false;
                rowGeneratoriDismessi.Visible = false;
                break;
            case "6": //Accertatore
            case "7": //Coordinatore
                rfvASPxComboBox1.Enabled = false;
                rfvASPxComboBox2.Enabled = false;
                break;

        }
    }

    #region DROPDOWNLIST / RADIOBUTTONLIST / CHECKBOXLIST
    protected void LoadAllDropDownlist()
    {
        rbStatoLibrettoImpianto(null);
        rbTipologiaGeneratori(null);
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

    #endregion

    protected void rbStatoLibrettoImpianto(int? idPresel)
    {
        rblStatoLibrettoImpianto.Items.Clear();
        rblStatoLibrettoImpianto.DataValueField = "IDStatoLibrettoImpianto";
        rblStatoLibrettoImpianto.DataTextField = "StatoLibrettoImpianto";
        rblStatoLibrettoImpianto.DataSource = LoadDropDownList.LoadDropDownList_SYS_StatoLibrettoImpianto(idPresel);
        rblStatoLibrettoImpianto.DataBind();

        ListItem myItem = new ListItem("Tutti i libretti", "0");
        rblStatoLibrettoImpianto.Items.Insert(0, myItem);
        
        rblStatoLibrettoImpianto.SelectedIndex = 0;
    }

    protected void rbTipologiaGeneratori(int? idPresel)
    {
        rblTipologieGeneratori.Items.Clear();
        rblTipologieGeneratori.DataValueField = "IDTipologiaRCT";
        rblTipologieGeneratori.DataTextField = "DescrizioneRCT";
        rblTipologieGeneratori.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaRapportoDiControllo(idPresel);
        rblTipologieGeneratori.DataBind();

        rblTipologieGeneratori.SelectedIndex = 0;
    }

    #region CODICE CATASTALE / DATI CATASTALI
    protected void ASPxComboBox3_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString(),true);
        comboBox.DataBind();
    }

    protected void ASPxComboBox3_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;

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
        ASPxComboBox3.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString(),true);
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
        //int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, (int)Session["currentPageIndexLibretti"], DataGrid.PageSize);
        int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, this.DataGrid.CurrentPageIndex, DataGrid.PageSize);
        this.SortExpression = currentSortExpression;

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "LIBRETTI IMPIANTI CORRISPONDENTI AI PARAMETRI IMPOSTATI");
    }

    private string BuildStr()
    {
        string strSql = UtilityLibrettiImpianti.GetSqlValoriLibrettiFilter(ASPxComboBox1.Value, 
                                                                           ASPxComboBox2.Value, 
                                                                           txtCodiceTargatura.Text, 
                                                                           ASPxComboBox3.Value,
                                                                           txtFoglio.Text,
                                                                           txtMappale.Text,
                                                                           txtSubalterno.Text,
                                                                           txtIdentificativo.Text,
                                                                           rblStatoLibrettoImpianto.SelectedItem.Value,
                                                                           txtResponsabile.Text,
                                                                           txtCfPIvaResponsabile.Text,
                                                                           txtCodicePod.Text,
                                                                           txtCodicePdr.Text,
                                                                           txtDataRegistrazioneDa.Text,
                                                                           txtDataRegistrazioneAl.Text,
                                                                           lblApiKey.Text,
                                                                           chkGeneratoriDsmessi.Checked,
                                                                           txtIndirizzoImpianto.Text,
                                                                           txtCivicoImpianto.Text,
                                                                           txtMatricolaGeneratore.Text,
                                                                           rblTipologieGeneratori.SelectedItem.Value);
        return strSql.ToString();
    }

    public void DataGrid_PageChanger(object source, DataGridPageChangedEventArgs e)
    {
        //Session["currentPageIndexLibretti"] = e.NewPageIndex;
        //DataGrid.CurrentPageIndex = (int)Session["currentPageIndexLibretti"];
        DataGrid.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
    }

    public void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            #region Link Libretto Impianto
            HyperLink lnkLibretto = (HyperLink)(e.Item.Cells[6].FindControl("lnkLibretto"));
            QueryString qs = new QueryString();
            qs.Add("IDLibrettoImpianto", e.Item.Cells[0].Text);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);
            string url = "LIM_LibrettiImpianti.aspx";
            url += qsEncrypted.ToString();
            lnkLibretto.NavigateUrl = url;
            #endregion





            ImageButton imgEdit = (ImageButton)(e.Item.Cells[8].FindControl("ImgEdit"));
            ImageButton imgView = (ImageButton)(e.Item.Cells[8].FindControl("ImgView"));
            ImageButton imgDelete = (ImageButton)(e.Item.Cells[9].FindControl("ImgDelete"));
            ImageButton imgRevision = (ImageButton)(e.Item.Cells[10].FindControl("ImgRevision"));
            ImageButton imgPdf = (ImageButton)(e.Item.Cells[11].FindControl("ImgPdf"));

            switch (e.Item.Cells[4].Text)
            {
                case "1": //Libretto in bozza
                    imgEdit.Visible = true;
                    imgView.Visible = false;
                    imgRevision.Visible = false;
                    imgDelete.Visible = true;
                    imgDelete.ImageUrl = "~/images/Buttons/delete.png";
                    imgDelete.CommandName = "Delete";
                    imgDelete.OnClientClick = "javascript:return confirm('Confermi la cancellazione del libretto di impianto?')";
                    imgDelete.ToolTip = "Cancella libretto impianto";
                    break;
                case "2": //Libretto definitivo
                    imgEdit.Visible = false;
                    imgView.Visible = true;
                    if (e.Item.Cells[12].Text != "&nbsp;" && e.Item.Cells[12].Text != "")
                    {
                        imgRevision.Visible = true;
                    }
                    else
                    {
                        imgRevision.Visible = false;
                    }
                    imgDelete.Visible = false;
                    break;
                case "3": //Libretto revisionato
                    imgEdit.Visible = true;
                    imgView.Visible = false;
                    imgRevision.Visible = true;
                    imgDelete.Visible = true;
                    imgDelete.ImageUrl = "~/images/Buttons/undo.png";
                    imgDelete.CommandName = "DeleteRevisione";
                    imgDelete.OnClientClick = "javascript:return confirm('Confermi di cancellare la revisione del libretto di impianto e ripristinare la versione precedente?')";
                    imgDelete.ToolTip = "Ripristina la versione del libretto impianto";
                    break;
                case "4": //Libretto annullato
                    imgEdit.Visible = false;
                    imgView.Visible = true;
                    imgRevision.Visible = false;
                    imgDelete.Visible = false;
                    break;
            }

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

            string pathQrCodeFile = ConfigurationManager.AppSettings["UploadTargatureImpianti"].ToString() + "/" + e.Item.Cells[5].Text + ".png";

            Image imgBarcode = (Image)(e.Item.Cells[7].FindControl("imgBarcode"));
            if (System.IO.File.Exists(ConfigurationManager.AppSettings["PathDocument"] + pathQrCodeFile))
            {
                imgBarcode.Visible = true;
                imgBarcode.ImageUrl = "~/" + pathQrCodeFile;
            }
            else
            {
                imgBarcode.Visible = false;
            }
        }
    }

    public void RowCommand(Object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            int iDLibrettoImpianto = int.Parse(commandArgs[0].ToString());
            bool fAttivo = bool.Parse(commandArgs[1].ToString());

            UtilityLibrettiImpianti.ChangefAttivo(iDLibrettoImpianto, fAttivo);
            
            BindGrid(true);
        }
        else if (e.CommandName == "View")
        {
            QueryString qs = new QueryString();
            qs.Add("IDLibrettoImpianto", e.CommandArgument.ToString());
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "LIM_LibrettiImpianti.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
        else if (e.CommandName == "Edit")
        {
            QueryString qs = new QueryString();
            qs.Add("IDLibrettoImpianto", e.CommandArgument.ToString());
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "LIM_LibrettiImpianti.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
        else if (e.CommandName == "Revision")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            
            QueryString qs = new QueryString();
            qs.Add("IDLibrettoImpianto", commandArgs[0].ToString());
            qs.Add("IDTargaturaImpianto", commandArgs[1].ToString());
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "LIM_LibrettiImpiantiSearchRevisioni.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
        else if (e.CommandName == "DeleteRevisione")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            int iDLibrettoImpianto = int.Parse(commandArgs[0].ToString());
            bool fAttivo = bool.Parse(commandArgs[1].ToString());

            UtilityLibrettiImpianti.RipristinaRevisione(iDLibrettoImpianto);

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
                DataGrid.Items[0].Cells[7].Focus();
            }
        }
    }

    protected void LIM_LibrettiImpianti_btnAdd_Click(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["fEncryptQueryString"] == "on")
        {
            QueryString qs = new QueryString();
            qs.Add("IDLibrettoImpianto", "");
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "LIM_LibrettiImpiantiNuovo.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
    }
    
}
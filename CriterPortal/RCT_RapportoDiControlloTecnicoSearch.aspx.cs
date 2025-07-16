using System;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DataUtilityCore;
using DataLayer;
using System.Configuration;
using EncryptionQS;
using DevExpress.Web;
using System.Collections.Generic;

public partial class RCT_RCT_RapportoDiControlloTecnicoSearch : System.Web.UI.Page
{
    protected string listIDRapportiControlloTecnici
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
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "RCT_RapportoDiControlloTecnicoSearch.aspx");

        if (!Page.IsPostBack)
        {
            LoadAllDropDownlist();
            PagePermission();
            if (!string.IsNullOrEmpty(listIDRapportiControlloTecnici))
            {
                GetRapportiControllo();
            }
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
                rowCriticitaFilter.Visible = true;
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

                GetComboBoxFilterByIDAzienda();

                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                lblSoggetto.Visible = true;
                break;
            case "11": //Software house
                ASPxComboBox1.Visible = false;
                ASPxComboBox2.Visible = false;
                lblSoggetto.Visible = false;
                lblSoggettoDerived.Visible = false;
                
                rowAzienda.Visible = false;
                rowManutentore.Visible = false;
                btnNuovo.Visible = false;

                UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
                lblApiKey.Text = info.KeyApi;
                break;
            case "8": //Ispettore
                rowAzienda.Visible = false;
                rowManutentore.Visible = false;
                btnNuovo.Visible = false;
                rowStatoRapporto.Visible = false;
                rblStatoRapportoDiControllo.SelectedValue = "2";
                break;
            case "16": //Ente locale
                rowAzienda.Visible = false;
                rowManutentore.Visible = false;
                btnNuovo.Visible = false;
                rowStatoRapporto.Visible = false;
                rblStatoRapportoDiControllo.SelectedValue = "2";
                rowTipologieRapportiDiControllo.Visible = false;
                rowTipologieControllo.Visible = false;
                rowDataRegistrazione.Visible = false;
                rowDataFirma.Visible = false;
                rowDataEsecuzioneVerifica.Visible = false;
                rowCriticitaFilter.Visible = false;
                break;
        }
    }

    protected void LoadAllDropDownlist()
    {
        rbStatoRapportoDiControllo(null);
        rbTipologiaRapportoDiControllo(null);
        rbTipologiaControllo(null);
    }

    protected void rbStatoRapportoDiControllo(int? idPresel)
    {
        rblStatoRapportoDiControllo.Items.Clear();
        rblStatoRapportoDiControllo.DataValueField = "IDStatoRapportoDiControllo";
        rblStatoRapportoDiControllo.DataTextField = "StatoRapportoDiControllo";
        rblStatoRapportoDiControllo.DataSource = LoadDropDownList.LoadDropDownList_SYS_StatoRapportoDiControllo(idPresel);
        rblStatoRapportoDiControllo.DataBind();

        ListItem myItem = new ListItem("Tutti gli stati", "0");
        rblStatoRapportoDiControllo.Items.Insert(0, myItem);

        rblStatoRapportoDiControllo.SelectedIndex = 0;
    }

    protected void rbTipologiaRapportoDiControllo(int? idPresel)
    {
        rblTipologieRapportoDiControllo.Items.Clear();
        rblTipologieRapportoDiControllo.DataValueField = "IDTipologiaRCT";
        rblTipologieRapportoDiControllo.DataTextField = "DescrizioneRCT";
        rblTipologieRapportoDiControllo.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaRapportoDiControllo(idPresel);
        rblTipologieRapportoDiControllo.DataBind();

        ListItem myItem = new ListItem("Tutte le tipologie", "0");
        rblTipologieRapportoDiControllo.Items.Insert(0, myItem);

        rblTipologieRapportoDiControllo.SelectedIndex = 0;
    }

    protected void rbTipologiaControllo(int? idPresel)
    {
        rblTipologieControllo.Items.Clear();
        rblTipologieControllo.DataValueField = "IDTipologiaControllo";
        rblTipologieControllo.DataTextField = "TipologiaControllo";
        rblTipologieControllo.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaControllo(idPresel);
        rblTipologieControllo.DataBind();

        ListItem myItem = new ListItem("Tutti", "0");
        rblTipologieControllo.Items.Insert(0, myItem);

        rblTipologieControllo.SelectedIndex = 0;
    }

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

    #endregion

    #region CODICE CATASTALE / DATI CATASTALI
    protected void ASPxComboBox3_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString(), true);
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
        ASPxComboBox3.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString(), true);
        ASPxComboBox3.DataBind();
    }
    #endregion

    #region RAPPORTI DI CONTROLLO
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpressionRapporti"] == null)
                return string.Empty;
            return ViewState["SortExpressionRapporti"].ToString();
        }
        set
        {
            ViewState["SortExpressionRapporti"] = value;
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
        string sql = BuildStr(); // Session["sqlstrRapporti"].ToString();
        string currentSortExpression = this.SortExpression;
        //int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, (int) Session["currentPageIndexRapporti"], DataGrid.PageSize);
        int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, this.DataGrid.CurrentPageIndex, DataGrid.PageSize);
        this.SortExpression = currentSortExpression;

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "RAPPORTI DI CONTROLLO CORRISPONDENTI AI PARAMETRI IMPOSTATI");
    }

    private string BuildStr()
    {
        List<object> valoriCriticita = new List<object>();
        foreach (ListItem item in cblCriticita.Items)
        {
            if (item.Selected)
            {
                valoriCriticita.Add(item.Value);
            }
        }
        
        string strSql = UtilityRapportiControllo.GetSqlValoriRapportiFilter(ASPxComboBox1.Value,
                                                                            ASPxComboBox2.Value,
                                                                            txtCodiceTargatura.Text,
                                                                            rblStatoRapportoDiControllo.SelectedItem.Value,
                                                                            rblTipologieRapportoDiControllo.SelectedItem.Value,
                                                                            rblTipologieControllo.SelectedItem.Value,
                                                                            listIDRapportiControlloTecnici,
                                                                            valoriCriticita.ToArray<object>(),
                                                                            txtDataRegistrazioneDa.Text,
                                                                            txtDataRegistrazioneAl.Text,
                                                                            txtDataFirmaDa.Text,
                                                                            txtDataFirmaAl.Text,
                                                                            txtDataEsecuzioneVerificaDa.Text,
                                                                            txtDataEsecuzioneVerificaAl.Text,
                                                                            lblApiKey.Text, 
                                                                            ASPxComboBox3.Value,
                                                                            txtFoglio.Text,
                                                                            txtMappale.Text,
                                                                            txtSubalterno.Text,
                                                                            txtIdentificativo.Text,
                                                                            txtResponsabile.Text);
        return strSql.ToString();
    }

    public void DataGrid_PageChanger(object source, DataGridPageChangedEventArgs e)
    {
        //Session["currentPageIndexRapporti"] = e.NewPageIndex;
        //DataGrid.CurrentPageIndex = (int) Session["currentPageIndexRapporti"];
        DataGrid.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
    }

    public void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            #region Link Rapporto di Controllo
            HyperLink lnkRapportoControllo = (HyperLink)(e.Item.Cells[6].FindControl("lnkRapportoControllo"));

            QueryString qs = new QueryString();
            qs.Add("IDRapportoControlloTecnico", e.Item.Cells[0].Text);
            qs.Add("IDTipologiaRCT", e.Item.Cells[12].Text);
            qs.Add("IDSoggetto", e.Item.Cells[2].Text);
            qs.Add("IDSoggettoDerived", e.Item.Cells[3].Text);
            QueryString qsEncryptedRapporto = Encryption.EncryptQueryString(qs);

            string urlRapporto = "RCT_RapportoDiControlloTecnico.aspx";
            urlRapporto += qsEncryptedRapporto.ToString();
            lnkRapportoControllo.NavigateUrl = urlRapporto;
            #endregion



            ImageButton imgEdit = (ImageButton) (e.Item.Cells[8].FindControl("ImgEdit"));
            ImageButton imgView = (ImageButton) (e.Item.Cells[8].FindControl("ImgView"));
            ImageButton imgDelete = (ImageButton) (e.Item.Cells[9].FindControl("ImgDelete"));
            ImageButton imgPdf = (ImageButton) (e.Item.Cells[10].FindControl("ImgPdf"));

            switch (e.Item.Cells[4].Text)
            {
                case "1": //Rapporto in bozza
                    imgEdit.Visible = true;
                    imgView.Visible = false;

                    string guidInteroImpianto = e.Item.Cells[11].Text;
                    if (e.Item.Cells[11].Text != "&nbsp;")
                    {
                        if (UtilityRapportiControllo.GetRctDefinitiviInAttesaDiFirmaInteroImpianto(e.Item.Cells[11].Text))
                        {
                            imgDelete.Visible = false;
                        }
                        else
                        {
                            imgDelete.Visible = true;
                        }
                    }
                    else
                    {
                        imgDelete.Visible = true;
                    }
                    break;
                case "2": //Rapporto definitivo
                case "3": //Rapporto da firmare
                case "4": //Rapporto annullato
                    imgEdit.Visible = false;
                    imgView.Visible = true;
                    imgDelete.Visible = false;
                    break;
            }

            string pathPdfFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadRapportiControllo"] + @"\" + "RapportoControllo_" + e.Item.Cells[0].Text.ToString() + ".pdf";
            if (System.IO.File.Exists(pathPdfFile))
            {
                imgPdf.Visible = true;
                imgPdf.Attributes.Add("onclick", "var winRapporto=dhtmlwindow.open('InfoRapporto_" + e.Item.Cells[0].Text + "', 'iframe', 'RCT_RapportiControlloViewer.aspx?IDRapportoControlloTecnico=" + e.Item.Cells[0].Text + "', 'Rapporto_" + e.Item.Cells[0].Text + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); ");
            }
            else
            {
                imgPdf.Visible = false;
            }

            System.Web.UI.WebControls.Image imgBarcode = (System.Web.UI.WebControls.Image)(e.Item.Cells[7].FindControl("imgBarcode"));
            imgBarcode.ImageUrl = "~/" + ConfigurationManager.AppSettings["UploadTargatureImpianti"].ToString() + "/" + e.Item.Cells[5].Text + ".png";
        }
    }

    public void RowCommand(Object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        if (e.CommandName == "Delete")
        {
            long iDRapportoControlloTecnico = long.Parse(commandArgs[0]);
            UtilityRapportiControllo.DeleteRct(iDRapportoControlloTecnico);
            BindGrid(true);
        }
        else if (e.CommandName == "View")
        {
            QueryString qs = new QueryString();
            qs.Add("IDRapportoControlloTecnico", commandArgs[0]);
            qs.Add("IDTipologiaRCT", commandArgs[1]);
            qs.Add("IDSoggetto", commandArgs[2]);
            qs.Add("IDSoggettoDerived", commandArgs[3]);
            qs.Add("codiceTargaturaImpianto", "");
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "RCT_RapportoDiControlloTecnicoNuovo.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
        else if (e.CommandName == "Edit")
        {
            QueryString qs = new QueryString();
            qs.Add("IDRapportoControlloTecnico", commandArgs[0]);
            qs.Add("IDTipologiaRCT", commandArgs[1]);
            qs.Add("IDSoggetto", commandArgs[2]);
            qs.Add("IDSoggettoDerived", commandArgs[3]);
            qs.Add("codiceTargaturaImpianto", "");
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "RCT_RapportoDiControlloTecnicoNuovo.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
        else if (e.CommandName == "Pdf")
        {

        }
    }

    #endregion

    public void GetRapportiControllo()
    {
        System.Threading.Thread.Sleep(500);
        DataGrid.CurrentPageIndex = 0;
        BindGrid(true);
    }

    protected void btnRicerca_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetRapportiControllo();
            if (DataGrid.Items.Count > 0)
            {
                DataGrid.Items[0].Cells[6].Focus();
            }
        }
    }

    protected void btnNuovo_Click(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["fEncryptQueryString"] == "on")
        {
            QueryString qs = new QueryString();
            qs.Add("IDRapportoControlloTecnico", "");
            qs.Add("IDTipologiaRCT", "");
            qs.Add("IDSoggetto", "");
            qs.Add("IDSoggettoDerived", "");
            qs.Add("codiceTargaturaImpianto", "");
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "RCT_RapportoDiControlloTecnicoNuovo.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
    }
    
}
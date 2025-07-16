using DataLayer;
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

public partial class VER_AccertamentiSearch : System.Web.UI.Page
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
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "VER_AccertamentiSearch.aspx");

        if (!Page.IsPostBack)
        {
            PageTipoAccertamento();
            LoadAllDropDownlist();
            ASPxComboBox1.Focus();
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
                //if (rblStatoAccertamento.SelectedItem.Value == "12")
                //{
                //    rowSanzione.Visible = true;
                //}
                //else
                //{
                //    rowSanzione.Visible = false;
                //}
                rowAccertatore.Visible = true;
                switch (IDTipoAccertamento)
                {
                    case "1": //Accertamento su RCT
                    case "2": //Accertamento su ispezione
                        rblStatoAccertamento.Items.RemoveAt(12); //Remove status Notifica verbale di accertamento con sanzione
                        break;
                }
                break;
            case "7": //Coordinatore
                rowProcedure.Visible = true;
                //rowSanzione.Visible = false;
                rowAccertatore.Visible = true;
                break;
            case "6": //Accertatore
                rowProcedure.Visible = false;
                //rowSanzione.Visible = false;
                rowAccertatore.Visible = false;
                break;
            case "14": //Coordinatore/Ispettore
                rowProcedure.Visible = true;
                //if (rblStatoAccertamento.SelectedItem.Value == "12")
                //{
                //    rowSanzione.Visible = true;
                //}
                //else
                //{
                //    rowSanzione.Visible = false;
                //}
                rowAccertatore.Visible = false;
                break;
            case "17": //Agente Accertatore
                rowProcedure.Visible = false;
                //if (rblStatoAccertamento.SelectedItem.Value == "12")
                //{
                //    rowSanzione.Visible = true;
                //}
                //else
                //{
                //    rowSanzione.Visible = false;
                //}
                rowAccertatore.Visible = false;
                break;
        }
    }

    protected void PageTipoAccertamento()
    {
        switch (IDTipoAccertamento)
        {
            case "1": //Accertamento su RCT
                lblTitoloPagina.Text = "RICERCA RAPPORTI DI CONTROLLO TECNICO SOTTOPOSTI AD ACCERTAMENTO";
                break;
            case "2": //Accertamento su ispezione
                lblTitoloPagina.Text = "RICERCA ISPEZIONI SOTTOPOSTE AD ACCERTAMENTO";
                break;
        }
    }

    protected void LoadAllDropDownlist()
    {
        rbStatoAccertamento(null);
        rbProcedure(null, int.Parse(IDTipoAccertamento));
        //ddStatoAccertamentoSanzione(null);
    }

    protected void rbStatoAccertamento(int? idPresel)
    {
        rblStatoAccertamento.Items.Clear();
        rblStatoAccertamento.DataValueField = "IDStatoAccertamento";
        rblStatoAccertamento.DataTextField = "StatoAccertamento";
        rblStatoAccertamento.DataSource = LoadDropDownList.LoadDropDownList_SYS_StatoAccertamento(idPresel);
        rblStatoAccertamento.DataBind();

        ListItem myItem = new ListItem("Tutti gli stati", "0");
        rblStatoAccertamento.Items.Insert(0, myItem);

        rblStatoAccertamento.SelectedIndex = 0;
    }

    protected void rbProcedure(int? idPresel, int? iDTipoAccertamento)
    {
        cblProcedure.Items.Clear();
        cblProcedure.DataValueField = "IDProceduraAccertamento";
        cblProcedure.DataTextField = "ProceduraAccertamento";
        cblProcedure.DataSource = LoadDropDownList.LoadDropDownList_SYS_ProceduraAccertamento(idPresel, iDTipoAccertamento);
        cblProcedure.DataBind();
    }

    //protected void ddStatoAccertamentoSanzione(int? idPresel)
    //{
    //    ddlStatoSanzione.DataValueField = "IDStatoAccertamentoSanzione";
    //    ddlStatoSanzione.DataTextField = "StatoAccertamentoSanzione";
    //    ddlStatoSanzione.DataSource = LoadDropDownList.LoadDropDownList_SYS_StatoAccertamentoSanzione(idPresel);
    //    ddlStatoSanzione.DataBind();

    //    ListItem myItem = new ListItem("-- Selezionare --", "0");
    //    ddlStatoSanzione.Items.Insert(0, myItem);

    //    ddlStatoSanzione.SelectedIndex = 0;
    //}

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

    #region RICERCA ACCERTATORI
    protected void ASPxComboBoxAccertatore_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        using (CriterDataModel ctx = new CriterDataModel())
        {
            var Accertatori = (from a in ctx.COM_AnagraficaSoggetti
                               join u in ctx.COM_Utenti on a.IDSoggetto equals u.IDSoggetto
                               where (u.IDRuolo == 6 && u.fAttivo == true && a.fAttivo == true) // Accertatore
                               select new
                               {
                                   u.IDUtente,
                                   Accertatore = a.Nome + " " + a.Cognome
                               }).ToList();

            comboBox.DataSource = Accertatori;
        }

        comboBox.DataBind();
    }

    protected void ASPxComboBoxAccertatore_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;

        using (CriterDataModel ctx = new CriterDataModel())
        {
            var Accertatori = (from a in ctx.COM_AnagraficaSoggetti
                               join u in ctx.COM_Utenti on a.IDSoggetto equals u.IDSoggetto
                               where (u.IDRuolo == 6 && u.fAttivo == true && a.fAttivo == true) // Accertatore
                               select new
                               {
                                   u.IDUtente,
                                   Accertatore = a.Nome + " " + a.Cognome
                               }).ToList();

            comboBox.DataSource = Accertatori;
        }
        comboBox.DataBind();
    }

    protected void ASPxComboBoxAccertatore_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBoxAccertatori();
    }

    protected void RefreshAspxComboBoxAccertatori()
    {
        ASPxComboBoxAccertatore.SelectedIndex = -1;
        using (CriterDataModel ctx = new CriterDataModel())
        {
            var Accertatori = (from a in ctx.COM_AnagraficaSoggetti
                               join u in ctx.COM_Utenti on a.IDSoggetto equals u.IDSoggetto
                               where (u.IDRuolo == 6) // Accertatore
                               select new
                               {
                                   u.IDUtente,
                                   Accertatore = a.Nome + " " + a.Cognome
                               }).ToList();

            ASPxComboBoxAccertatore.DataSource = Accertatori;
        }
        ASPxComboBoxAccertatore.DataBind();
    }

    #endregion

    #region RICERCA PROGRAMMA ACCERTAMENTI
    protected void ASPxComboBox2_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox2_VER_ProgrammaAccertamento(string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString(), int.Parse(IDTipoAccertamento));
        comboBox.DataBind();
    }

    protected void ASPxComboBox2_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox2_VER_ProgrammaAccertamentoRequestedByValue(e.Value.ToString(), int.Parse(IDTipoAccertamento));
        comboBox.DataBind();
    }

    protected void ASPxComboBox2_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBox2();
    }

    protected void RefreshAspxComboBox2()
    {
        ASPxComboBox2.SelectedIndex = -1;
        ASPxComboBox2.DataSource = LoadDropDownList.ASPxComboBox2_VER_ProgrammaAccertamento(string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString(), int.Parse(IDTipoAccertamento));
        ASPxComboBox2.DataBind();
    }

    #endregion

    #region ACCERTAMENTI
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpressionAccertamenti"] == null)
                return string.Empty;
            return ViewState["SortExpressionAccertamenti"].ToString();
        }
        set
        {
            ViewState["SortExpressionAccertamenti"] = value;
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

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "ACCERTAMENTI CORRISPONDENTI AI PARAMETRI IMPOSTATI");
    }

    private string BuildStr()
    {
        List<object> valoriProcedure = new List<object>();
        foreach (ListItem item in cblProcedure.Items)
        {
            if (item.Selected)
            {
                valoriProcedure.Add(item.Value);
            }
        }

        int? iDAccertatore = null;
        if (info.IDRuolo == 6)
        {
            iDAccertatore = info.IDUtente;
        }
        else
        {
            if (ASPxComboBoxAccertatore.Value != null)
            {
                iDAccertatore = int.Parse(ASPxComboBoxAccertatore.Value.ToString());
            }
        }

        string strSql = UtilityVerifiche.GetSqlValoriAccertamentiFilter(IDTipoAccertamento,
                                                                        ASPxComboBox1.Value,
                                                                        rblStatoAccertamento.SelectedItem.Value,
                                                                        valoriProcedure.ToArray<object>(),
                                                                        txtDataRilevazioneDa.Text,
                                                                        txtDataRilevazioneAl.Text,
                                                                        txtCodiceAccertamento.Text,
                                                                        txtCodiceTargatura.Text,
                                                                        iDAccertatore,
                                                                        null,
                                                                        ASPxComboBox2.Value,
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
            //TableRow rowStatoSanzione = (TableRow)(e.Item.Cells[8].FindControl("rowStatoSanzione"));
            TableRow rowDatiIspezioni = (TableRow)(e.Item.Cells[8].FindControl("rowDatiIspezioni"));

            switch (IDTipoAccertamento)
            {
                case "1": //Accertamento su RCT
                    //rowStatoSanzione.Visible = false;
                    rowDatiIspezioni.Visible = false;
                    break;
                case "2": //Accertamento su ispezione
                    //rowStatoSanzione.Visible = true;
                    rowDatiIspezioni.Visible = true;
                    break;
            }

            ImageButton imgView = (ImageButton)(e.Item.Cells[9].FindControl("ImgView"));
            ImageButton ImgPresaInCarico = (ImageButton)(e.Item.Cells[10].FindControl("ImgPresaInCarico"));

            #region Link Accertamento
            HyperLink lnkAccertamento = (HyperLink)(e.Item.Cells[8].FindControl("lnkAccertamento"));

            QueryString qs = new QueryString();
            qs.Add("IDAccertamento", e.Item.Cells[0].Text);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "VER_Accertamenti.aspx";
            url += qsEncrypted.ToString();

            lnkAccertamento.NavigateUrl = url;
            lnkAccertamento.Target = "_blank";
            #endregion


            switch (e.Item.Cells[3].Text)
            {
                case "1": //In attesa di accertamento
                    if (info.IDRuolo == 1) //Admin
                    {
                        imgView.Visible = false;
                        ImgPresaInCarico.Visible = true;

                        ImgPresaInCarico.Visible = true;
                        ImgPresaInCarico.ImageUrl = "~/images/Buttons/PresaCarico.png";
                        ImgPresaInCarico.OnClientClick = "javascript:return confirm('Confermi di prendere in carico l accertamento codice " + e.Item.Cells[7].Text + "?')";
                        ImgPresaInCarico.ToolTip = "Presa in carico accertamento";
                        ImgPresaInCarico.AlternateText = "Presa in carico accertamento";
                    }
                    else if ((info.IDRuolo == 6) || (info.IDRuolo == 17))
                    {
                        imgView.Visible = false;
                        ImgPresaInCarico.Visible = false;
                        //16/10/2020 Accertatori Non possono più prendere in carico 
                        //ImgPresaInCarico.Visible = true;
                        //ImgPresaInCarico.ImageUrl = "~/images/Buttons/PresaCarico.png";
                        //ImgPresaInCarico.OnClientClick = "javascript:return confirm('Confermi di prendere in carico l accertamento codice " + e.Item.Cells[7].Text + "?')";
                        //ImgPresaInCarico.ToolTip = "Presa in carico accertamento";
                        //ImgPresaInCarico.AlternateText = "Presa in carico accertamento";
                    }
                    else if ((info.IDRuolo == 7) || (info.IDRuolo == 14))
                    {
                        imgView.Visible = false;
                        ImgPresaInCarico.Visible = true;

                        ImgPresaInCarico.Visible = true;
                        ImgPresaInCarico.ImageUrl = "~/images/Buttons/PresaCarico.png";
                        ImgPresaInCarico.OnClientClick = "javascript:return confirm('Confermi di prendere in carico l accertamento codice " + e.Item.Cells[7].Text + "?')";
                        ImgPresaInCarico.ToolTip = "Presa in carico accertamento";
                        ImgPresaInCarico.AlternateText = "Presa in carico accertamento";
                    }
                    break;
                case "2": //Assegnato ad accertatore
                case "7"://Accertamento sospeso in attesa di conferma
                    if (info.IDRuolo == 1) //Admin
                    {
                        imgView.Visible = true;

                        ImgPresaInCarico.Visible = true;
                        ImgPresaInCarico.ImageUrl = "~/images/Buttons/undo.png";
                        ImgPresaInCarico.OnClientClick = "javascript:return confirm('Confermi di annullare la presa in carico dell accertamento con codice " + e.Item.Cells[7].Text + "?')";
                        ImgPresaInCarico.ToolTip = "Annulla la presa in carico dell'accertamento";
                        ImgPresaInCarico.AlternateText = "Annulla la presa in carico dell'accertamento";
                    }
                    else if ((info.IDRuolo == 6) || (info.IDRuolo == 17))
                    {
                        imgView.Visible = true;
                        ImgPresaInCarico.Visible = false;
                    }
                    else if ((info.IDRuolo == 7) || (info.IDRuolo == 14)) // || (info.IDRuolo == 17))
                    {
                        imgView.Visible = true;

                        ImgPresaInCarico.Visible = true;
                        ImgPresaInCarico.ImageUrl = "~/images/Buttons/undo.png";
                        ImgPresaInCarico.OnClientClick = "javascript:return confirm('Confermi di annullare la presa in carico dell accertamento con codice " + e.Item.Cells[7].Text + "?')";
                        ImgPresaInCarico.ToolTip = "Annulla la presa in carico dell'accertamento";
                        ImgPresaInCarico.AlternateText = "Annulla la presa in carico dell'accertamento";
                    }
                    break;
                case "3": //In attesa di verifica da parte del Coordinatore
                    if (info.IDRuolo == 1) //Admin
                    {
                        imgView.Visible = true;

                        ImgPresaInCarico.Visible = true;
                        ImgPresaInCarico.ImageUrl = "~/images/Buttons/undo.png";
                        ImgPresaInCarico.OnClientClick = "javascript:return confirm('Confermi di annullare la presa in carico dell accertamento con codice " + e.Item.Cells[7].Text + "?')";
                        ImgPresaInCarico.ToolTip = "Annulla la presa in carico dell'accertamento";
                        ImgPresaInCarico.AlternateText = "Annulla la presa in carico dell'accertamento";
                    }
                    else if ((info.IDRuolo == 6) || (info.IDRuolo == 17))
                    {
                        imgView.Visible = true;
                        ImgPresaInCarico.Visible = false;
                    }
                    else if ((info.IDRuolo == 7) || (info.IDRuolo == 14))
                    {
                        imgView.Visible = false;

                        ImgPresaInCarico.Visible = true;
                        ImgPresaInCarico.ImageUrl = "~/images/Buttons/PresaCarico.png";
                        ImgPresaInCarico.OnClientClick = "javascript:return confirm('Confermi di prendere in carico l accertamento codice " + e.Item.Cells[7].Text + "?')";
                        ImgPresaInCarico.ToolTip = "Presa in carico accertamento";
                        ImgPresaInCarico.AlternateText = "Presa in carico accertamento";
                    }
                    break;
                case "4": //Assegnato a coordinatore
                    if (info.IDRuolo == 1) //Admin
                    {
                        imgView.Visible = true;

                        ImgPresaInCarico.Visible = true;
                        ImgPresaInCarico.ImageUrl = "~/images/Buttons/undo.png";
                        ImgPresaInCarico.OnClientClick = "javascript:return confirm('Confermi di annullare la presa in carico dell accertamento con codice " + e.Item.Cells[7].Text + "?')";
                        ImgPresaInCarico.ToolTip = "Annulla la presa in carico dell'accertamento";
                        ImgPresaInCarico.AlternateText = "Annulla la presa in carico dell'accertamento";
                    }
                    //else if (info.IDRuolo == 6)
                    //{
                    //    imgView.Visible = true;
                    //    ImgPresaInCarico.Visible = false;
                    //}
                    else if ((info.IDRuolo == 7) || (info.IDRuolo == 14) || (info.IDRuolo == 17) || (info.IDRuolo == 6))
                    {
                        imgView.Visible = true;
                        ImgPresaInCarico.Visible = false;
                    }
                    break;
                case "5": //Accertamento concluso
                    imgView.Visible = true;
                    ImgPresaInCarico.Visible = false;
                    break;
                case "6": //Accertamento in fase di firma
                    imgView.Visible = true;
                    ImgPresaInCarico.Visible = false;
                    break;
                case "8": //In attesa di verifica da parte dell'Agente Accertatore
                    if (info.IDRuolo == 1) //Admin
                    {
                        imgView.Visible = true;

                        ImgPresaInCarico.Visible = true;
                        ImgPresaInCarico.ImageUrl = "~/images/Buttons/undo.png";
                        ImgPresaInCarico.OnClientClick = "javascript:return confirm('Confermi di annullare la presa in carico dell accertamento con codice " + e.Item.Cells[7].Text + "?')";
                        ImgPresaInCarico.ToolTip = "Annulla la presa in carico dell'accertamento";
                        ImgPresaInCarico.AlternateText = "Annulla la presa in carico dell'accertamento";
                    }
                    else if (info.IDRuolo == 6 || (info.IDRuolo == 7) || (info.IDRuolo == 14))
                    {
                        imgView.Visible = true;
                        ImgPresaInCarico.Visible = false;
                    }
                    else if (info.IDRuolo == 17) // Agente Accertatore
                    {
                        imgView.Visible = false;

                        ImgPresaInCarico.Visible = true;
                        ImgPresaInCarico.ImageUrl = "~/images/Buttons/PresaCarico.png";
                        ImgPresaInCarico.OnClientClick = "javascript:return confirm('Confermi di prendere in carico l accertamento codice " + e.Item.Cells[7].Text + "?')";
                        ImgPresaInCarico.ToolTip = "Presa in carico accertamento";
                        ImgPresaInCarico.AlternateText = "Presa in carico accertamento";
                    }
                    break;
                case "9": //Assegnato ad Agente Accertatore
                    if (info.IDRuolo == 1) //Admin
                    {
                        imgView.Visible = true;

                        ImgPresaInCarico.Visible = true;
                        ImgPresaInCarico.ImageUrl = "~/images/Buttons/undo.png";
                        ImgPresaInCarico.OnClientClick = "javascript:return confirm('Confermi di annullare la presa in carico dell accertamento con codice " + e.Item.Cells[7].Text + "?')";
                        ImgPresaInCarico.ToolTip = "Annulla la presa in carico dell'accertamento";
                        ImgPresaInCarico.AlternateText = "Annulla la presa in carico dell'accertamento";
                    }
                    else if ((info.IDRuolo == 6) || (info.IDRuolo == 7) || (info.IDRuolo == 14) || (info.IDRuolo == 17))
                    {
                        imgView.Visible = true;
                        ImgPresaInCarico.Visible = false;
                    }
                    break;
                case "10": //Accertamento non inviato
                case "11": //Accertamento rigettato
                    imgView.Visible = true;
                    ImgPresaInCarico.Visible = false;
                    break;
            }

            //bool fAccertamentoInCorso = UtilityVerifiche.ControllaAccettamentiInCorso(e.Item.Cells[0].Text,
            //                                                                          e.Item.Cells[4].Text,
            //                                                                          e.Item.Cells[11].Text,
            //                                                                          e.Item.Cells[12].Text);

            //TableRow rowAccertamentoInCorso = (TableRow)(e.Item.Cells[8].FindControl("rowAccertamentoInCorso"));
            //if (fAccertamentoInCorso)
            //{
            //    rowAccertamentoInCorso.Visible = true;
            //}

            DataGrid dgDocumenti = (DataGrid)e.Item.Cells[9].FindControl("dgDocumenti");
            GetDocumentiAccertamenti(dgDocumenti, long.Parse(e.Item.Cells[0].Text));
        }
    }

    public void RowCommand(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "PresaInCarico")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            int iDAccertamento = int.Parse(commandArgs[0]);
            int iDStatoAccertamento = int.Parse(commandArgs[1]);
            int iDUtente = int.Parse(info.IDUtente.ToString());


            if (iDStatoAccertamento == 1)//In attesa di accertamento
            {
                if (info.IDRuolo == 1) //Admin
                {
                    UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 9, iDUtente, iDUtente, iDUtente); //Assegnato ad agente accertatore
                    UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
                }
                else if (info.IDRuolo == 6)
                {
                    //16/10/2020 Accertatori Non possono più prendere in carico 
                    //UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 2, iDUtente, null);
                    //UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
                }
                else if ((info.IDRuolo == 7) || (info.IDRuolo == 14))
                {
                    UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 4, iDUtente, iDUtente, null); //Assegnato a coordinatore
                    UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
                }
                else if (info.IDRuolo == 17)
                {
                    // niente ?
                }
            }
            else if ((iDStatoAccertamento == 2)) //Assegnato ad accertatore
            {
                UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 1, null, null, null);//In attesa di accertamento -- annulamento presa in carico
                UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
            }
            else if (iDStatoAccertamento == 3) //In attesa di verifica da parte del Coordinatore
            {
                if (info.IDRuolo == 1) //Admin
                {
                    int iDUtenteAccertatore = int.Parse(commandArgs[2]);
                    UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 2, iDUtenteAccertatore, null, null); //Assegnato ad accertatore - annulamento presa in carico
                    UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
                }
                else if (info.IDRuolo == 6)
                {
                    // niente
                }
                else if ((info.IDRuolo == 7) || (info.IDRuolo == 14))
                {
                    int iDUtenteAccertatore = int.Parse(commandArgs[2]);
                    UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 4, iDUtenteAccertatore, int.Parse(info.IDUtente.ToString()), null);
                    UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
                }
                else if (info.IDRuolo == 17)
                {
                    // niente
                }
            }
            else if (iDStatoAccertamento == 4)
            {
                int iDUtenteAccertatore = int.Parse(commandArgs[2]);
                UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 3, iDUtenteAccertatore, null, null);
                UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
            }
            else if (iDStatoAccertamento == 8)
            {
                if (info.IDRuolo == 1) //Admin
                {
                    int iDUtenteAccertatore = int.Parse(commandArgs[2]);
                    int iDUtenteCoordinatore = int.Parse(commandArgs[3]); // nuovo param IDUtenteCoordinatore
                    UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 4, iDUtenteAccertatore, iDUtenteCoordinatore, null);
                    UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
                }
                else if (info.IDRuolo == 17)
                {
                    //agente prende in carico
                    int iDUtenteAccertatore = int.Parse(commandArgs[2]);
                    int iDUtenteCoordinatore = int.Parse(commandArgs[3]); // nuovo param IDUtenteCoordinatore
                    UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 9, iDUtenteAccertatore, iDUtenteCoordinatore, int.Parse(info.IDUtente.ToString()));
                    UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
                }
            }
            else if (iDStatoAccertamento == 9)
            {
                if (info.IDRuolo == 1)
                {
                    int iDUtenteAccertatore = int.Parse(commandArgs[2]);
                    int iDUtenteCoordinatore = int.Parse(commandArgs[3]); // nuovo param IDUtenteCoordinatore
                    UtilityVerifiche.CambiaStatoAccertamento(iDAccertamento, 8, iDUtenteAccertatore, iDUtenteCoordinatore, null);
                    UtilityVerifiche.StoricizzaStatoAccertamento(iDAccertamento, iDUtente);
                }
            }

            BindGrid(true);
        }
        else if (e.CommandName == "View")
        {
            QueryString qs = new QueryString();
            qs.Add("IDAccertamento", e.CommandArgument.ToString());
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "VER_Accertamenti.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
    }

    #region Documenti
    public void dgDocumenti_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            ImageButton imgPdf = (ImageButton)(e.Item.Cells[5].FindControl("ImgPdf"));
            string pathPdfFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + e.Item.Cells[3].Text + @"\" + e.Item.Cells[1].Text.ToString() + "_" + e.Item.Cells[2].Text.ToString() + ".pdf";
            if (File.Exists(pathPdfFile))
            {
                imgPdf.Visible = true;
                imgPdf.Attributes.Add("onclick", "var winDocumento=dhtmlwindow.open('Accertamento_" + e.Item.Cells[0].Text + "', 'iframe', 'VER_AccertamentiViewer.aspx?IDAccertamento=" + e.Item.Cells[1].Text + "&IDProceduraAccertamento=" + e.Item.Cells[2].Text + "&IDTipoAccertamento=" + IDTipoAccertamento + "&CodiceAccertamento=" + e.Item.Cells[3].Text + "', 'Accertamento_" + e.Item.Cells[0].Text + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); ");
            }
            else
            {
                imgPdf.Visible = false;
            }
        }
    }

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

    #endregion

    public void GetAccertamenti()
    {
        System.Threading.Thread.Sleep(500);
        DataGrid.CurrentPageIndex = 0;
        BindGrid(true);
    }

    protected void btnRicerca_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetAccertamenti();
        }
    }

    //protected void rblStatoAccertamento_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    PagePermission();
    //}
}
using DataUtilityCore;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using EncryptionQS;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class COM_Raccomandate : System.Web.UI.Page
{
    UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            VisibleHiddenTypeRaccomandazione();
            TipoServizioRaccomandata();
            GetRaccomandate();
        }
    }

    public void VisibleHiddenTypeRaccomandazione()
    {
        switch (int.Parse(rblTipoRaccomandata.SelectedItem.Value))
        {
            case 0: //Tutti
                rowCodiceAccertamento.Visible = false;
                rowCodiceIspezione.Visible = false;
                break;
            case 1: //Accertamento
                rowCodiceAccertamento.Visible = true;
                rowCodiceIspezione.Visible = false;
                break;
            case 2: //Revoca accertamento
                rowCodiceAccertamento.Visible = true;
                rowCodiceIspezione.Visible = false;
                break;
            case 3://Conferma pianificazione ispezione
                rowCodiceAccertamento.Visible = false;
                rowCodiceIspezione.Visible = true;
                break;
            case 4: //Annullamento pianificazione ispezione
                rowCodiceAccertamento.Visible = false;
                rowCodiceIspezione.Visible = true;
                break;
            case 5: //Ripianificazione ispezione
                rowCodiceAccertamento.Visible = false;
                rowCodiceIspezione.Visible = true;
                break;
            case 6: //Notifica Sanzione
                rowCodiceAccertamento.Visible = true;
                rowCodiceIspezione.Visible = false;
                break;
            case 7: //Raccomandate libere

                break;
            case 8: //Revoca sanzioni

                break;
        }
    }

    protected void rblTipoServizioRaccomandata_SelectedIndexChanged(object sender, EventArgs e)
    {
        TipoServizioRaccomandata();
        GetRaccomandate();
    }

    public void TipoServizioRaccomandata()
    {
        switch (rblTipoServizioRaccomandata.SelectedItem.Value)
        {
            case "ROL":
                rowStatoSpedizioneROL.Visible = true;
                rowStatoSpedizioneAGOL.Visible = false;
                rowEsitoDeposito.Visible = true;
                rowTipoRaccomandata.Visible = true;
                break;
            case "AGOL":
                rowStatoSpedizioneROL.Visible = false;
                rowStatoSpedizioneAGOL.Visible = true;
                rowEsitoDeposito.Visible = false;
                rowTipoRaccomandata.Visible = false;
                break;
        }

        rblEsitoSpedizione.SelectedItem.Value = "0";
        rblStatoRaccomandataROL.SelectedItem.Value = "";
        rblStatoRaccomandataAGOL.SelectedItem.Value = "";
    }

    #region RACCOMANDATE
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpressionRaccomandate"] == null)
                return string.Empty;
            return ViewState["SortExpressionRaccomandate"].ToString();
        }
        set
        {
            ViewState["SortExpressionRaccomandate"] = value;
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
        int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, this.DataGrid.CurrentPageIndex, this.DataGrid.PageSize);
        this.SortExpression = currentSortExpression;

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "RACCOMANDATE CORRISPONDENTI AI PARAMETRI IMPOSTATI");
    }

    private string BuildStr()
    {
        string strSql = UtilityPosteItaliane.GetSqlValoriRaccomandateFilter(rblTipoRaccomandata.SelectedItem.Value,
                                                                            txtCodiceTargatura.Text,
                                                                            txtCodiceAccertamento.Text,
                                                                            txtCodiceIspezione.Text,
                                                                            rblEsitoSpedizione.SelectedItem.Value,
                                                                            txtDataInvioDa.Text,
                                                                            txtDataInvioAl.Text,
                                                                            txtDataUltimoAggiornamentoDa.Text,
                                                                            txtDataUltimoAggiornamentoAl.Text,
                                                                            rblStatoRaccomandataROL.SelectedItem.Value,
                                                                            rblStatoRaccomandataAGOL.SelectedItem.Value,
                                                                            txtIDRichiesta.Text,
                                                                            txtNumeroRaccomandata.Text,
                                                                            rblTipoServizioRaccomandata.SelectedItem.Value
                                                                            );
        return strSql.ToString();
    }

    public void DataGrid_PageChanger(object source, DataGridPageChangedEventArgs e)
    {
        DataGrid.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void rblTipoRaccomandata_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenTypeRaccomandazione();
    }

    public void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Label lblTitoloPratica = (Label)(e.Item.Cells[4].FindControl("lblTitoloPratica")); 
            HyperLink lnkPratica = (HyperLink)(e.Item.Cells[4].FindControl("lnkPratica"));
            //Label lblRaccomandataType = (Label)(e.Item.Cells[4].FindControl("lblRaccomandataType"));
            ImageButton ImgSendRaccomandata = (ImageButton)(e.Item.Cells[9].FindControl("ImgSendRaccomandata"));
            
            #region Raccomandate type
            string url = string.Empty;
            string linkText = string.Empty;
            //string typeRaccomandata = string.Empty;

            QueryString qs = new QueryString();
            switch (e.Item.Cells[1].Text)
            {
                case "1":
                    //typeRaccomandata = "Accertamento";
                    lblTitoloPratica.Text = "Accertamento: ";

                    qs.Add("IDAccertamento", e.Item.Cells[2].Text);
                    QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                    url = "VER_Accertamenti.aspx";
                    url += qsEncrypted.ToString();

                    linkText = e.Item.Cells[5].Text;
                    break;
                case "2":
                    //typeRaccomandata = "Revoca accertamento";

                    lblTitoloPratica.Text = "Gest. intervento: ";

                    qs.Add("IDAccertamento", e.Item.Cells[2].Text);
                    QueryString qsEncrypted1 = Encryption.EncryptQueryString(qs);

                    url = "VER_Interventi.aspx";
                    url += qsEncrypted1.ToString();

                    linkText = e.Item.Cells[5].Text;
                    break;
                case "3":
                    //typeRaccomandata = "Conferma pianificazione ispezione";

                    lblTitoloPratica.Text = "Ispezione: ";

                    qs.Add("IDIspezione", e.Item.Cells[3].Text);
                    qs.Add("IDIspezioneVisita", e.Item.Cells[7].Text);

                    QueryString qsEncrypted2 = Encryption.EncryptQueryString(qs);

                    url = "VER_Ispezioni.aspx";
                    url += qsEncrypted2.ToString();

                    linkText = e.Item.Cells[6].Text;
                    break;
                case "4":
                    //typeRaccomandata = "Annullamento pianificazione ispezione";

                    lblTitoloPratica.Text = "Ispezione: ";

                    qs.Add("IDIspezione", e.Item.Cells[3].Text);
                    qs.Add("IDIspezioneVisita", e.Item.Cells[7].Text);
                    QueryString qsEncrypted3 = Encryption.EncryptQueryString(qs);

                    url = "VER_Ispezioni.aspx";
                    url += qsEncrypted3.ToString();

                    linkText = e.Item.Cells[6].Text;
                    break;
                case "5":
                    //typeRaccomandata = "Ripianificazione ispezione";

                    lblTitoloPratica.Text = "Ispezione: ";

                    qs.Add("IDIspezione", e.Item.Cells[3].Text);
                    qs.Add("IDIspezioneVisita", e.Item.Cells[7].Text);
                    QueryString qsEncrypted4 = Encryption.EncryptQueryString(qs);

                    url = "VER_Ispezioni.aspx";
                    url += qsEncrypted4.ToString();

                    linkText = e.Item.Cells[6].Text;
                    break;
                case "6":
                    //typeRaccomandata = "Notifica Sanzione";

                    url = "#";
                    break;
                case "7":
                    //typeRaccomandata = "Raccomandate libere";

                    url = "#";
                    break;
            }

            
            lnkPratica.NavigateUrl = url;
            lnkPratica.Text = linkText;
            //lblRaccomandataType.Text = typeRaccomandata;
            #endregion

            if (bool.Parse(e.Item.Cells[10].Text))
            {
                ImgSendRaccomandata.Visible = false;
            }
            else
            {
                ImgSendRaccomandata.Visible = true;
            }
        }
    }
       

    public void RowCommand(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        if (e.CommandName == "InviaRaccomandata")
        {
            int iDRaccomandataType = int.Parse(commandArgs[2]);
            switch (iDRaccomandataType)
            {
                case 1:
                    UtilityPosteItaliane.SendToPosteItaliane(long.Parse(commandArgs[0]), (int)DataUtilityCore.Enum.EnumTypeofRaccomandata.TypeAccertamento);
                    break;
                case 2:
                    UtilityPosteItaliane.SendToPosteItaliane(long.Parse(commandArgs[0]), (int)DataUtilityCore.Enum.EnumTypeofRaccomandata.TypeRevocaAccertamento);
                    break;
                case 3:
                    UtilityPosteItaliane.SendToPosteItaliane(long.Parse(commandArgs[1]), (int)DataUtilityCore.Enum.EnumTypeofRaccomandata.TypePianificazioneIspezioneConferma);
                    break;
                case 4:
                    UtilityPosteItaliane.SendToPosteItaliane(long.Parse(commandArgs[1]), (int)DataUtilityCore.Enum.EnumTypeofRaccomandata.TypePianificazioneIspezioneAnnulla);
                    break;
                case 5:
                    UtilityPosteItaliane.SendToPosteItaliane(long.Parse(commandArgs[1]), (int)DataUtilityCore.Enum.EnumTypeofRaccomandata.TypePianificazioneIspezioneRipianificazione);
                    break;
                case 6:
                    UtilityPosteItaliane.SendToPosteItaliane(long.Parse(commandArgs[0]), (int)DataUtilityCore.Enum.EnumTypeofRaccomandata.TypeSanzione);
                    break;
                case 7:
                    
                    break;
                case 8:
                    UtilityPosteItaliane.SendToPosteItaliane(long.Parse(commandArgs[0]), (int)DataUtilityCore.Enum.EnumTypeofRaccomandata.TypeRevocaSanzione);
                    break;
            }

            BindGrid(true);
        }
    }

    #endregion

    public void GetRaccomandate()
    {
        System.Threading.Thread.Sleep(500);
        DataGrid.CurrentPageIndex = 0;
        BindGrid(true);
    }

    protected void btnRicerca_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetRaccomandate();
        }
    }


    protected void btnSaveNote_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            for (int i = 0; i < DataGrid.Items.Count; i++)
            {
                TextBox txtNoteRaccomandata = (TextBox)DataGrid.Items[i].Cells[4].FindControl("txtNoteRaccomandata");
                var iDRaccomandata = DataGrid.Items[i].Cells[0].Text;

                UtilityPosteItaliane.SaveNoteRaccomandata(iDRaccomandata, txtNoteRaccomandata.Text);
            }
        }
    }

    
}
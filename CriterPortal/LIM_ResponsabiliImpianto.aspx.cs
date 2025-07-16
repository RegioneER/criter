using DataUtilityCore;
using EncryptionQS;
using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DataLayer;

public partial class LIM_ResponsabiliImpianto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "LIM_ResponsabiliImpianto.aspx");

        if (!Page.IsPostBack)
        {
            VisibleHiddenTypeSearch(rblTipoRicercaLibretti.SelectedValue);
        }

        ASPxWebControl.RegisterBaseScript(Page);
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
            case "10": //Resposanbile tecnico
                ASPxComboBox1.Value = getVal[2].ToString();
                ASPxComboBox1.Visible = false;

                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                lblSoggetto.Visible = true;
                break;
        }
    }

    #region RICERCA AZIENDE
    protected void ASPxComboBox_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(true, "2,3", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(true, "2,3", e.Value.ToString(), "");
        comboBox.DataBind();
    }

    protected void ASPxComboBox_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBox();
        ResetAnagrafica();
    }

    protected void RefreshAspxComboBox()
    {
        ASPxComboBox1.SelectedIndex = -1;
        ASPxComboBox1.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(true, "2,3", "", "", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
        ASPxComboBox1.DataBind();
    }

    protected void ASPxComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ASPxComboBox1.Value != null)
        {
            int? iDSoggetto = int.Parse(ASPxComboBox1.Value.ToString());
            GetDatiAnagrafica(iDSoggetto);
        }
    }

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
        string sql = Session["sqlstrLibretti"].ToString();
        string currentSortExpression = this.SortExpression;
        int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, (int) Session["currentPageIndexLibretti"], DataGrid.PageSize);
        this.SortExpression = currentSortExpression;

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "LIBRETTI IMPIANTI CORRISPONDENTI AI PARAMETRI IMPOSTATI");
        if (this.DataGrid.Items.Count == 0)
        {
            lblCount.Visible = true;
        }
        else
        {
            lblCount.Visible = false;
        }
    }

    private string BuildStr()
    {
        string strSql = UtilityLibrettiImpianti.GetSqlValoriLibrettiFilter(null,
                                                                           null,
                                                                           txtCodiceTargatura.Text,
                                                                           null,
                                                                           string.Empty,
                                                                           string.Empty,
                                                                           string.Empty,
                                                                           string.Empty,
                                                                           2,
                                                                           null,
                                                                           txtCfPIvaResponsabile.Text,
                                                                           txtNumeroPodPdr.Text,
                                                                           txtNumeroPodPdr.Text,
                                                                           null,
                                                                           null,
                                                                           string.Empty,
                                                                           false,
                                                                           string.Empty,
                                                                           string.Empty,
                                                                           string.Empty,
                                                                           null);

        return strSql.ToString();
    }

    public void DataGrid_PageChanger(object source, DataGridPageChangedEventArgs e)
    {
        Session["currentPageIndexLibretti"] = e.NewPageIndex;
        DataGrid.CurrentPageIndex = (int) Session["currentPageIndexLibretti"];
        BindGrid();
    }

    public void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            ImageButton imgPdf = (ImageButton) (e.Item.Cells[7].FindControl("ImgPdf"));

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
        if (e.CommandName == "Pdf")
        {

        }
    }

    public void GetLibrettiImpianti()
    {
        System.Threading.Thread.Sleep(500);
        Session["sqlstrLibretti"] = BuildStr();
        Session["currentPageIndexLibretti"] = 0;
        BindGrid(true);
    }
    #endregion

    public void GetDatiAnagrafica(int? iDSoggetto)
    {
        if (iDSoggetto != null)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var responsabile = ctx.COM_AnagraficaSoggetti.Where(s => (s.IDSoggetto == iDSoggetto)
                                            ).OrderBy(s => s.IDSoggetto).FirstOrDefault();

                lblNomeLegaleRappresentante.Text = responsabile.Nome;
                lblCognomeLegaleRappresentante.Text = responsabile.Cognome;
                lblCodiceFiscale.Text = responsabile.CodiceFiscale;
                lblPartitaIva.Text = responsabile.PartitaIVA;
                lblEmail.Text = responsabile.Email;
                lblEmailPec.Text = responsabile.EmailPec;
            }
        }
    }

    public void ResetAnagrafica()
    {
        lblNomeLegaleRappresentante.Text = string.Empty;
        lblCognomeLegaleRappresentante.Text = string.Empty;
        lblCodiceFiscale.Text = string.Empty;
        lblPartitaIva.Text = string.Empty;
        lblEmail.Text = string.Empty;
        lblEmailPec.Text = string.Empty;
    }

    protected void LIM_LibrettiImpiantiSearch_btnProcess_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetLibrettiImpianti();
            if (DataGrid.Items.Count > 0)
            {
                rowTerzoResponsabile.Visible = true;
                rowAzienda.Visible = true;
                rowDataInizioResponsabilita.Visible = true;
                rowDataFineResponsabilita.Visible = true;
                rowIntestazioneResponsabilita.Visible = true;
                rowIntestazioneResponsabilitaLibretto.Visible = true;
                
                PagePermission();
                if(ASPxComboBox1.Value != null)
                {
                    int? iDSoggetto = int.Parse(ASPxComboBox1.Value.ToString());
                    GetDatiAnagrafica(iDSoggetto);
                }
                
                rowNomeLegaleRappresentante.Visible = true;
                rowCognomeLegaleRappresentante.Visible = true;
                rowCodiceFiscale.Visible = true;
                rowPartitaIva.Visible = true;
                rowEmail.Visible = true;
                rowEmailPec.Visible = true;
            }
            else
            {
                rowTerzoResponsabile.Visible = false;
                rowAzienda.Visible = false;
                rowDataInizioResponsabilita.Visible = false;
                rowDataFineResponsabilita.Visible = false;
                rowIntestazioneResponsabilita.Visible = false;
                if (DataGrid.Items.Count == 0)
                {
                    rowIntestazioneResponsabilitaLibretto.Visible = true;
                }
                else
                {
                    rowIntestazioneResponsabilitaLibretto.Visible = false;
                }
                
                rowNomeLegaleRappresentante.Visible = false;
                rowCognomeLegaleRappresentante.Visible = false;
                rowCodiceFiscale.Visible = false;
                rowPartitaIva.Visible = false;
                rowEmail.Visible = false;
                rowEmailPec.Visible = false;
            }
        }
    }

    protected void rblTipoRicercaLibretti_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenTypeSearch(rblTipoRicercaLibretti.SelectedValue);
    }

    protected void VisibleHiddenTypeSearch(string IDTipoRicercaLibretti)
    {
        if (IDTipoRicercaLibretti == "0")
        {
            rowRicerca2.Visible = true;
            rowRicerca3.Visible = false;
            txtNumeroPodPdr.Text = string.Empty;
            rowRicerca5.Visible = false;
            txtCfPIvaResponsabile.Text = string.Empty;
        }
        else if (IDTipoRicercaLibretti == "1")
        {
            rowRicerca2.Visible = false;
            txtCodiceTargatura.Text = string.Empty;
            rowRicerca3.Visible = true;
            rowRicerca5.Visible = true;
        }
    }

    #region Assunzione Responsabilità
    public void ControllaDateResponsabilita(Object sender, ServerValidateEventArgs e)
    {
        DateTime dataInizioResponsabile;
        DateTime dataFineResponsabile;

        bool chkDataInizio = DateTime.TryParse(txtDataInizioResponsabile.Text, out dataInizioResponsabile);
        bool chkDataFine = DateTime.TryParse(txtDataFineResponsabile.Text, out dataFineResponsabile);

        if (!chkDataInizio && !chkDataFine)
        {
            if (dataInizioResponsabile.Date < dataFineResponsabile)
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaResponsabilitaGiaInCarico(Object sender, ServerValidateEventArgs e)
    {
        for (int i = 0; (i <= (DataGrid.Items.Count - 1)); i++)
        {
            DataGridItem elemento = DataGrid.Items[i];
            int iDLibrettoImpianto = int.Parse(elemento.Cells[0].Text);
            
            using (CriterDataModel ctx = new CriterDataModel())
            {
                int iDSoggetto = int.Parse(ASPxComboBox1.Value.ToString());

                var responsabile = ctx.LIM_LibrettiImpiantiResponsabili.Where(s => (s.IDSoggetto == iDSoggetto)
                                                                                    && (s.IDLibrettoImpianto == iDLibrettoImpianto)
                                                                                    && (s.fAttivo == true)
                                                                                    && (s.DataDichiarazioneDismissioneIncarico == null)
                                                                                    
                            ).OrderBy(s => s.IDSoggetto).ToList();

                if (responsabile.Count > 0)
                {
                    e.IsValid = false;
                }
                else
                {
                    e.IsValid = true;
                }
            }
        }

            
        
    }

    public void ControllaResponsabilitaInCaricoAdAltri(Object sender, ServerValidateEventArgs e)
    {
        for (int i = 0; (i <= (DataGrid.Items.Count - 1)); i++)
        {
            DataGridItem elemento = DataGrid.Items[i];
            int iDLibrettoImpianto = int.Parse(elemento.Cells[0].Text);

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var responsabile = ctx.LIM_LibrettiImpiantiResponsabili.Where(s => (s.IDLibrettoImpianto == iDLibrettoImpianto)
                                                                                    && (s.fAttivo == true)
                                                                                    && (s.DataDichiarazioneDismissioneIncarico == null)
                                                                                    && (s.DataFine > DateTime.Now)
                            ).OrderBy(s => s.IDSoggetto).ToList();

                if (responsabile.Count > 0)
                {
                    e.IsValid = false;
                }
                else
                {
                    e.IsValid = true;
                }
            }
        }



    }

    protected void LIM_LibrettiImpiantiSearch_btnAssunzioneTerzoResponsabile_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                int iDSoggetto = int.Parse(ASPxComboBox1.Value.ToString());
                DateTime dtInizio = DateTime.Parse(txtDataInizioResponsabile.Text);
                DateTime dtFine = DateTime.Parse(txtDataFineResponsabile.Text);

                for (int i = 0; (i <= (DataGrid.Items.Count - 1)); i++)
                {
                    DataGridItem elemento = DataGrid.Items[i];
                    int iDLibrettoImpianto = int.Parse(elemento.Cells[0].Text);

                    UtilityLibrettiImpianti.AssunzioneIncaricoTerzoResponsabile(iDLibrettoImpianto, iDSoggetto, dtInizio, dtFine);

                    QueryString qs = new QueryString();
                    qs.Add("IDLibrettoImpianto", iDLibrettoImpianto.ToString());
                    QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                    string url = "LIM_ResponsabiliImpiantoSearch.aspx";
                    url += qsEncrypted.ToString();

                    tblInfoTestata.Visible = false;
                    tblInfoNominaOk.Visible = true;

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + url + "';},4000);", true);
                    //Response.Redirect(url);
                }
            }
            catch (Exception ex)
            {

            }           
        }
    }

    #endregion

    
}
using DataLayer;
using DataUtilityCore;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Collections;


public partial class VER_ProgrammaIspezione : System.Web.UI.Page
{
    UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetDatiAll();
        }
        CalcolareLaDistanza();
    }

    public void GetDatiAll()
    {
        fVisibleColumnInsertNelProgramma(UtilityVerifiche.GetIDProgrammaIspezioneAttivo());
        GetListProgrammaIspezione();
        //GetAccertamentiInAttesaIspezione();
        //GetLibrettiImpianti();
        rbCombustibile(null);
        rblTipologiaGruppoTermico(null);
        GridViewVisita.FocusedRowIndex = -1;
        GridViewVisita.DataBind();
        GridViewVisita.Selection.UnselectAll();
        DatiListaProgramma(null);
    }

    public void fVisibleColumnInsertNelProgramma(int iDProgrammaIspezioneAttivo)
    { 
        if(iDProgrammaIspezioneAttivo == 0)
        {
            DataGridAccertamentiInAttesaIspezione.Columns[8].Visible = false;
            DataGridLibretti.Columns[6].Visible = false;
            CreaVisitaIspettiva.Visible = false;
        }
        else
        {
            DataGridAccertamentiInAttesaIspezione.Columns[8].Visible = true;
            DataGridLibretti.Columns[6].Visible = true;
            CreaVisitaIspettiva.Visible = true;
        }
    }

    public void ResetFiltriRicercaDaInserireNelProgrammaIspezione()
    {
        txtCodicePdr.Text = "";
        txtCodicePod.Text = "";
        RefreshcmbDatiCatastali();
        txtFoglio.Text = "";
        txtMappale.Text = "";
        txtSubalterno.Text = "";
        txtIdentificativo.Text = "";       
        rbCombustibile(null);
        rblTipologiaGruppoTermico(null);
        txtPotenzaDa.Text = "";
        txtPotenzaA.Text = "";
        txtCodiceTargatura.Text = "";
        txtDataInserimentoDa.Text = "";
        txtDataInserimentoA.Text = "";
        txtDataInstallazioneDa.Text = "";
        txtDataInstallazioneA.Text = "";
    }

    protected void rblTipoRicerca_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblTipoRicerca.SelectedIndex == 0)
        {
            ResetFiltriRicercaDaInserireNelProgrammaIspezione();
            rowDataGridAccertamenti.Visible = true;
            rowDataGridLibretti.Visible = false;
            btnRicercaAccertamentiDaInserireNelProgrammaIspezione.Visible = true;
            btnRicercaLibrettiDaInserireNelProgrammaIspezione.Visible = false;
            //btnInserireLibretti.Visible = false;
            //BindGridLibretti();
            //BindGridAccertamentiInAttesaIspezione(true);
        }
        else
        {
            ResetFiltriRicercaDaInserireNelProgrammaIspezione();
            rowDataGridAccertamenti.Visible = false;
            rowDataGridLibretti.Visible = true;
            btnRicercaAccertamentiDaInserireNelProgrammaIspezione.Visible = false;
            btnRicercaLibrettiDaInserireNelProgrammaIspezione.Visible = true;
            //BindGridLibretti(true);
            //InsertLogicLibretti();
            //BindGridAccertamentiInAttesaIspezione();
        }
    }

    protected void rbCombustibile(int? iDPresel)
    {
        rblCombustibile.Items.Clear();
               
        rblCombustibile.DataValueField = "IDTipologiaCombustibile";
        rblCombustibile.DataTextField = "TipologiaCombustibile";
        rblCombustibile.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaCombustibile(iDPresel);
        rblCombustibile.DataBind();

        rblCombustibile.Items.Add(new ListItem("Tutti", ""));
        rblCombustibile.SelectedValue = "";
    }

    protected void rblTipologiaGruppoTermico(int? iDPresel)
    {
        rbTipologiaGruppoTermico.Items.Clear();
        rbTipologiaGruppoTermico.SelectedIndex = -1;
        rbTipologiaGruppoTermico.DataValueField = "IDTipologiaGruppiTermici";
        rbTipologiaGruppoTermico.DataTextField = "TipologiaGruppiTermici";
        rbTipologiaGruppoTermico.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaGruppiTermici(iDPresel);
        rbTipologiaGruppoTermico.DataBind();

        rbTipologiaGruppoTermico.Items.Add(new ListItem("Tutti", ""));
        rbTipologiaGruppoTermico.SelectedValue = "";
    }
    
    #region PROGRAMMA ISPEZIONE

    public void ExitAttivo()
    {
        int iDProgrammaIspezioneAttivo = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();

        if (Grid.FocusedRowIndex != -1)
        {
            if (iDProgrammaIspezioneAttivo == int.Parse(Grid.GetRowValues(Grid.FocusedRowIndex, "IDProgrammaIspezione").ToString()) || iDProgrammaIspezioneAttivo == 0)
            {
                cvAttivo.EnableClientScript = false;
            }
            else
            {
                cvAttivo.EnableClientScript = true;
            }
        }
        else
        {
            if (iDProgrammaIspezioneAttivo == 0)
            {
                cvAttivo.EnableClientScript = false;
            }
            else
            {
                cvAttivo.EnableClientScript = true;
            }
        }
    }

    protected void imgfAttivo_Init(object sender, EventArgs e)
    {
        GridViewDataItemTemplateContainer container = ((ASPxImage)sender).NamingContainer as GridViewDataItemTemplateContainer;
        int currentIndex = container.VisibleIndex;
        bool isfAttivo = Convert.ToBoolean(container.Grid.GetRowValues(currentIndex, "fAttivo"));

        if (isfAttivo)
        {
            ((ASPxImage)sender).ImageUrl = "images/si.png";

        }
        else
        {
            ((ASPxImage)sender).ImageUrl = "Images/no.png";

        }
    }

    public void GetListProgrammaIspezione()
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            var ProgrammaIspezione = ctx.VER_ProgrammaIspezione.OrderByDescending(c => c.fAttivo).ToList();

            Grid.DataSource = ProgrammaIspezione;
            Grid.DataBind();
        }
    }

    protected void Grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                int iDProgrammaIspezione = int.Parse(e.GetValue("IDProgrammaIspezione").ToString());
                var CountLibretti = ctx.VER_ProgrammaIspezioneInfo.Where(c => c.IDProgrammaIspezione == iDProgrammaIspezione).ToList();

                Label lblCountLibretti = Grid.FindRowCellTemplateControl(e.VisibleIndex, null, "lblCountLibretti") as Label;
                lblCountLibretti.Text = CountLibretti.Count().ToString();

                ImageButton btnPrintProgrammaIspezione = Grid.FindRowCellTemplateControl(e.VisibleIndex, null, "btnPrintProgrammaIspezione") as ImageButton;
                btnPrintProgrammaIspezione.Attributes.Add("onclick", "OpenPopupWindows(this, " + iDProgrammaIspezione + "); return false;");

                ImageButton btnPrintVisiteIspettiveProgrammaIspezione = Grid.FindRowCellTemplateControl(e.VisibleIndex, null, "btnPrintVisiteIspettiveProgrammaIspezione") as ImageButton;
                btnPrintVisiteIspettiveProgrammaIspezione.Attributes.Add("onclick", "OpenPopupWindowsVisiteIspettive(this, " + iDProgrammaIspezione + "); return false;");
            }
        }
    }

    #region BUTTON / CHECKBOX

    protected void btnNuovoProgrammaIspezione_Click(object sender, EventArgs e)
    {
        Grid.FocusedRowIndex = -1;
        ExitAttivo();
        btnNuovoProgrammaIspezione.Visible = false;
        tblPanel1.Visible = false;
        tblPanel2.Visible = true;
        pageControl.Visible = false;

        lbltitoloPanel2.Text = "<h2>NUOVO PROGRAMMA ISPEZIONE </h2>";

        txtDescrizione.Text = "";
        txtDataFine.Text = "";
        txtDataInizio.Text = "";
        cbAttivo.Checked = true;
    }

    protected void btnSalvaDati_Click(object sender, EventArgs e)
    {
        if (Grid.FocusedRowIndex == -1)
        {
            UtilityVerifiche.SaveInsertDeleteDatiProgrammaIspezione("insert",
                                                        null,
                                                        txtDescrizione.Text,
                                                        DateTime.Parse(txtDataInizio.Text),
                                                        UtilityApp.ParseNullableDatetime(txtDataFine.Text),
                                                        //DateTime.Parse(txtDataFine.Text),
                                                        cbAttivo.Checked);
        }
        else
        {
            UtilityVerifiche.SaveInsertDeleteDatiProgrammaIspezione("update",
                                                                  int.Parse(Grid.GetRowValues(Grid.FocusedRowIndex, "IDProgrammaIspezione").ToString()),
                                                                  txtDescrizione.Text,
                                                                  DateTime.Parse(txtDataInizio.Text),
                                                                  UtilityApp.ParseNullableDatetime(txtDataFine.Text),
                                                                  //DateTime.Parse(txtDataFine.Text),
                                                                  cbAttivo.Checked);
        }
        
        fVisibleColumnInsertNelProgramma(UtilityVerifiche.GetIDProgrammaIspezioneAttivo());

        GetListProgrammaIspezione();
        tblPanel2.Visible = false;
        tblPanel1.Visible = true;
        pageControl.Visible = true;
        btnNuovoProgrammaIspezione.Visible = true;
        GetDatiAll();
        GridViewVisita.DataBind();
    }

    protected void btnModificaProgrammaIspezione_Click(object sender, ImageClickEventArgs e)
    {
        ExitAttivo();
        tblPanel1.Visible = false;
        tblPanel2.Visible = true;
        pageControl.Visible = false;
        btnNuovoProgrammaIspezione.Visible = false;

        lbltitoloPanel2.Text = "<h2>MODIFICA PROGRAMMA ISPEZIONE </h2>";

        int rowIndex = Grid.FocusedRowIndex;

        string DescrizioneOld = Grid.GetRowValues(rowIndex, "Descrizione").ToString();
        string DataInizioOld = Grid.GetRowValues(rowIndex, "DataInizio").ToString();
        string DataFineOld = Grid.GetRowValues(rowIndex, "DataFine").ToString();
        string fAttivoOld = Grid.GetRowValues(rowIndex, "fAttivo").ToString();

        txtDescrizione.Text = DescrizioneOld;
        txtDataInizio.Text = DateTime.Parse(DataInizioOld).ToString("dd/MM/yyyy");
        txtDataFine.Text = DateTime.Parse(DataFineOld).ToString("dd/MM/yyyy");
        cbAttivo.Checked = bool.Parse(fAttivoOld);
    }

    protected void btnAnnullaDati_Click(object sender, EventArgs e)
    {
        tblPanel1.Visible = true;
        tblPanel2.Visible = false;
        pageControl.Visible = true;
        btnNuovoProgrammaIspezione.Visible = true;
    }

    #endregion

    #endregion

    #region CODICE CATASTALE / DATI CATASTALI

    protected void cmbDatiCatastali_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString(), true);
        comboBox.DataBind();
    }

    protected void cmbDatiCatastali_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastaliRequestedByValue(e.Value.ToString());
        comboBox.DataBind();
    }

    protected void cmbDatiCatastali_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshcmbDatiCatastali();
    }

    protected void RefreshcmbDatiCatastali()
    {
        cmbDatiCatastali.SelectedIndex = -1;
        cmbDatiCatastali.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString(), true);
        cmbDatiCatastali.DataBind();
    }

    #endregion

    #region RICERCA ACCERTAMENTI INTERVENTI DA INSERIRE NEL PROGRAMMA ISPEZIONE ATTIVO
    protected string SortExpressionAccertamentiInAttesaIspezione
    {
        get
        {
            if (ViewState["SortExpressionAccertamentiInAttesaIspezione"] == null)
                return string.Empty;
            return ViewState["SortExpressionAccertamentiInAttesaIspezione"].ToString();
        }
        set
        {
            ViewState["SortExpressionAccertamentiInAttesaIspezione"] = value;
        }
    }

    public void DataGridAccertamentiInAttesaIspezione_Sorting(object sender, DataGridSortCommandEventArgs e)
    {
        this.SortExpressionAccertamentiInAttesaIspezione = UtilityApp.CheckSortExpression(e.SortExpression.ToString(), this.SortExpressionAccertamentiInAttesaIspezione);
        BindGridAccertamentiInAttesaIspezione();
    }

    public void BindGridAccertamentiInAttesaIspezione()
    {
        BindGridAccertamentiInAttesaIspezione(false);
    }

    public void BindGridAccertamentiInAttesaIspezione(bool reload)
    {
        string sql = BuildStrAccertamentiInAttesaIspezione();// Session["sqlstrAccertamenti"].ToString();
        string currentSortExpression = this.SortExpressionAccertamentiInAttesaIspezione;
        //int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, (int)Session["currentPageIndexAccertamenti"], DataGrid.PageSize);
        int totRecords = UtilityApp.BindControl(reload, this.DataGridAccertamentiInAttesaIspezione, sql, currentSortExpression, this.DataGridAccertamentiInAttesaIspezione.CurrentPageIndex, DataGridAccertamentiInAttesaIspezione.PageSize);
        this.SortExpressionAccertamentiInAttesaIspezione = currentSortExpression;

        UtilityApp.SetVisuals(DataGridAccertamentiInAttesaIspezione, totRecords, lblCountAccertamentiInAttesaIspezione, "INTERVENTI SU ACCERTAMENTI NON REALIZZATI");
    }

    private string BuildStrAccertamentiInAttesaIspezione()
    {
        string strSql = UtilityVerifiche.GetSqlValoriAccertamentiProgrammaIspezioneFilter(
                                                                                   cmbDatiCatastali.Value,
                                                                                   txtFoglio.Text,
                                                                                   txtMappale.Text,
                                                                                   txtSubalterno.Text,
                                                                                   txtIdentificativo.Text,
                                                                                   txtCodicePod.Text,
                                                                                   txtCodicePdr.Text,
                                                                                   txtPotenzaDa.Text,
                                                                                   txtPotenzaA.Text,
                                                                                   rblCombustibile.SelectedValue,
                                                                                   rbTipologiaGruppoTermico.SelectedValue,
                                                                                   txtDataInstallazioneDa.Text,
                                                                                   txtDataInstallazioneA.Text,
                                                                                   txtDataInserimentoDa.Text,
                                                                                   txtDataInstallazioneA.Text,
                                                                                   txtCodiceTargatura.Text
                                                                                   //IDPROGRAMMAISPEZIONEATTIVO
                                                                                   );

        return strSql.ToString();
    }

    public void DataGridAccertamentiInAttesaIspezione_PageChanger(object source, DataGridPageChangedEventArgs e)
    {
        DataGridAccertamentiInAttesaIspezione.CurrentPageIndex = e.NewPageIndex;
        BindGridAccertamentiInAttesaIspezione();
    }

    public void GetAccertamentiInAttesaIspezione()
    {
        System.Threading.Thread.Sleep(500);
        DataGridAccertamentiInAttesaIspezione.CurrentPageIndex = 0;
        BindGridAccertamentiInAttesaIspezione(true);
    }

    protected void btnRicercaAccertamentiDaInserireNelProgrammaIspezione_Click(object sender, EventArgs e)
    {
        GetAccertamentiInAttesaIspezione();
    }

    //public void RowCommandAccertamento(object sender, CommandEventArgs e)
    //{
    //    if (e.CommandName == "InsertAccertamentoNelProgrammaIspezione")
    //    {
    //        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
    //        int iDAccertamento = int.Parse(commandArgs[0]);
    //        int iDLibrettoImpianto = int.Parse(commandArgs[1]);
    //        int iDLibrettoImpiantoGruppoTermico = int.Parse(commandArgs[2]);
    //        int iDTargaturaImpianto = int.Parse(commandArgs[3]);
    //        int iDProgrammaIspezioneAttivo = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();

    //        UtilityVerifiche.InsertDeleteGeneratoreNelProgrammaIspezioneAttivo("Insert", iDProgrammaIspezioneAttivo, iDLibrettoImpianto, iDTargaturaImpianto, iDAccertamento, iDLibrettoImpiantoGruppoTermico);

    //        GetAccertamentiInAttesaIspezione();
    //        GetDatiAll();
    //    }
    //}

    protected void chkSelezioneAllAccertamenti_CheckedChanged(object sender, EventArgs e)
    {
        bool fButtonVisible = false;

        for (int i = 0; i < DataGridAccertamentiInAttesaIspezione.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGridAccertamentiInAttesaIspezione.Items[i].Cells[8].FindControl("chkSelezioneAccertamenti");
            if (chk.Enabled)
            {
                chk.Checked = ((CheckBox)sender).Checked;
                fButtonVisible = ((CheckBox)sender).Checked;
            }
        }

        btnInserireLibretti.Visible = fButtonVisible;
    }

    protected void chkSelezioneAccertamenti_CheckedChanged(object sender, EventArgs e)
    {
        bool fButtonVisible = false;
        for (int i = 0; i < DataGridAccertamentiInAttesaIspezione.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGridAccertamentiInAttesaIspezione.Items[i].Cells[8].FindControl("chkSelezioneAccertamenti");
            if (chk.Checked)
            {
                fButtonVisible = true;
                break;
            }
        }

        btnInserireLibretti.Visible = fButtonVisible;
    }
    #endregion

    #region RICERCA GENERATORI DA INSERIRE NEL PROGRAMMA DI ISPEZIONE

    protected string SortExpressionLibretti
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
        this.SortExpressionLibretti = UtilityApp.CheckSortExpression(e.SortExpression.ToString(), this.SortExpressionLibretti);
        BindGridLibretti();
    }

    public void BindGridLibretti()
    {
        BindGridLibretti(false);
    }

    public void BindGridLibretti(bool reload)
    {
        string sql = BuildStrLibretti();
        string currentSortExpression = this.SortExpressionLibretti;
        int totRecords = UtilityApp.BindControl(reload, this.DataGridLibretti, sql, currentSortExpression, this.DataGridLibretti.CurrentPageIndex, DataGridLibretti.PageSize);
        this.SortExpressionLibretti = currentSortExpression;

        UtilityApp.SetVisuals(DataGridLibretti, totRecords, lblCountLibretti, "GENERATORI");
        lblCountLibretti.Text = lblCountLibretti.Text + " DI " + totRecords.ToString();// UtilityVerifiche.GetCountValoriLibrettiProgrammaIspezione();
    }

    private string BuildStrLibretti()
    {
        string strSql = UtilityVerifiche.GetSqlValoriLibrettiProgrammaIspezioneFilter(                                                                                   
                                                                                   cmbDatiCatastali.Value,
                                                                                   txtFoglio.Text,
                                                                                   txtMappale.Text,
                                                                                   txtSubalterno.Text,
                                                                                   txtIdentificativo.Text,
                                                                                   txtCodicePod.Text,
                                                                                   txtCodicePdr.Text,
                                                                                   txtPotenzaDa.Text,
                                                                                   txtPotenzaA.Text,
                                                                                   rblCombustibile.SelectedValue,
                                                                                   rbTipologiaGruppoTermico.SelectedValue,
                                                                                   txtDataInstallazioneDa.Text,
                                                                                   txtDataInstallazioneA.Text,
                                                                                   txtDataInserimentoDa.Text,
                                                                                   txtDataInserimentoA.Text,
                                                                                   txtCodiceTargatura.Text
                                                                                   );
        return strSql.ToString();
    }
        
    public void DataGridLibretti_PageChanger(object source, DataGridPageChangedEventArgs e)
    {
        DataGridLibretti.CurrentPageIndex = e.NewPageIndex;
        BindGridLibretti();
    }

    public void DataGridLibretti_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            ImageButton imgPdf = (ImageButton)(e.Item.Cells[6].FindControl("ImgPdf"));

            string pathPdfFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadLibrettiImpianti"] + @"\" + "LibrettoImpianto_" + e.Item.Cells[1].Text.ToString() + ".pdf";
            if (System.IO.File.Exists(pathPdfFile))
            {
                imgPdf.Visible = true;
                imgPdf.Attributes.Add("onclick", "var winLibretto=dhtmlwindow.open('Libretto_" + e.Item.Cells[1].Text + "', 'iframe', 'LIM_LibrettiImpiantiViewer.aspx?IDLibrettoImpianto=" + e.Item.Cells[1].Text + "', 'Libretto_" + e.Item.Cells[1].Text + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); ");
            }
            else
            {
                imgPdf.Visible = false;
            }
        }
    }

    public void GetLibrettiImpianti()
    {
        System.Threading.Thread.Sleep(500);
        DataGridLibretti.CurrentPageIndex = 0;
        BindGridLibretti(true);
    }

    //public void RowCommandLibretto(object sender, CommandEventArgs e)
    //{
    //if (e.CommandName == "InsertLibrettoNelProgrammaIspezione")
    //{
    //    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
    //    int iDLibrettoImpiantoGruppoTermico = int.Parse(commandArgs[0]);
    //    int iDLibrettoImpianto = int.Parse(commandArgs[1]);
    //    int iDProgrammaIspezioneAttivo = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();

    //    UtilityVerifiche.InsertDeleteGeneratoreNelProgrammaIspezioneAttivo("Insert", iDProgrammaIspezioneAttivo, iDLibrettoImpianto, null, iDLibrettoImpiantoGruppoTermico);

    //    GetDatiAll();
    //}
    //}

    //protected void btnInserireLibretti_Click(object sender, EventArgs e)
    //{
    //    List<string> libretti = new List<string>();
    //    for (int i = 0; i < DataGridLibretti.Items.Count; i++)
    //    {
    //        CheckBox chk = (CheckBox)DataGridLibretti.Items[i].Cells[6].FindControl("chkSelezioneLibretti");
    //        if (chk.Checked)
    //        {
    //            libretti.Add(DataGridLibretti.Items[i].Cells[0].Text);
    //        }
    //    }

    //    if (libretti.Count() > 0)
    //    {
    //        int iDLibrettoImpianto;
    //        int iDLibrettoImpiantoGruppoTermico;
    //        int iDProgrammaIspezioneAttivo = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();

    //        foreach (var item in libretti)
    //        {
    //            iDLibrettoImpiantoGruppoTermico = int.Parse(item);

    //            using (var ctx = new CriterDataModel())
    //            {
    //                iDLibrettoImpianto = ctx.LIM_LibrettiImpiantiGruppiTermici.Where(c => c.IDLibrettoImpiantoGruppoTermico == iDLibrettoImpiantoGruppoTermico)
    //                    .Select(c => c.IDLibrettoImpianto).FirstOrDefault();
    //            }

    //            UtilityVerifiche.InsertDeleteGeneratoreNelProgrammaIspezioneAttivo("Insert", iDProgrammaIspezioneAttivo, iDLibrettoImpianto, null, iDLibrettoImpiantoGruppoTermico);
    //        }
    //    }
    //    GetDatiAll();
    //    btnInserireLibretti.Visible = false;
    //}

    protected void chkSelezioneAllLibretti_CheckedChanged(object sender, EventArgs e)
    {
        bool fButtonVisible = false;

        for (int i = 0; i < DataGridLibretti.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGridLibretti.Items[i].Cells[6].FindControl("chkSelezioneLibretti");
            if (chk.Enabled)
            {
                chk.Checked = ((CheckBox)sender).Checked;
                fButtonVisible = ((CheckBox)sender).Checked;
            }
        }

        btnInserireLibretti.Visible = fButtonVisible;
    }

    protected void chkSelezioneLibretti_CheckedChanged(object sender, EventArgs e)
    {
        bool fButtonVisible = false;
        for (int i = 0; i < DataGridLibretti.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGridLibretti.Items[i].Cells[6].FindControl("chkSelezioneLibretti");
            if (chk.Checked)
            {
                fButtonVisible = true;
                break;
            }
        }

        btnInserireLibretti.Visible = fButtonVisible;
    }

    protected void btnRicercaLibrettiDaInserireNelProgrammaIspezione_Click(object sender, EventArgs e)
    {
        GetLibrettiImpianti();
    }

    #endregion

    #region CREA VISITE ISPETTIVE / ISPEZIONI DI VISITE ISPETTIVE

    protected void CreaVisitaIspettiva_Click(object sender, EventArgs e)
    {
        int iDProgrammaIspezioneAttivo = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();
        UtilityVerifiche.CreaDeleteVisitaIspetiva("Crea", iDProgrammaIspezioneAttivo, null);
        GridViewVisita.DataBind();
    }

    protected void grdLibrettiNellaVista_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        int idIspezioneVisita = Convert.ToInt32(gridView.GetMasterRowKeyValue());

        dsLibrettiNellaVista.WhereParameters["IDIspezioneVisita"].DefaultValue = idIspezioneVisita.ToString();
    }

    protected void grdLibrettiNellaVista_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            ASPxGridView grdLibrettiNellaVista = (ASPxGridView)sender;
            var noteIspezioneVisita = e.NewValues["NoteIspezioneVisita"].ToString();

            if (!string.IsNullOrEmpty(noteIspezioneVisita))
            {
                var idRiga = Convert.ToInt32(e.Keys[grdLibrettiNellaVista.KeyFieldName]);
                var generatoreInIspezione = ctx.VER_IspezioneVisitaInfo.Find(idRiga);
                generatoreInIspezione.NoteIspezioneVisita = noteIspezioneVisita;

                ctx.SaveChanges();
                grdLibrettiNellaVista.CancelEdit();
                e.Cancel = true;
            }
        }
    }

    protected void CommandButtonGeneratori(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "DeleteGeneratore")
        {
            int iDProgrammaIspezioneAttivo = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

            int iDLibrettoImpianto = int.Parse(commandArgs[0]);
            int iDIspezioneVisita = int.Parse(commandArgs[1]);
            int iDLibrettoImpiantoGruppoTermico = int.Parse(commandArgs[2]);
            long? iDIspezioneVisitaInfo = int.Parse(commandArgs[3]);

            UtilityVerifiche.InsertDeleteGeneratoreNelleVisiteIspettive("Delete", iDIspezioneVisitaInfo, iDIspezioneVisita, iDLibrettoImpianto, iDProgrammaIspezioneAttivo, iDLibrettoImpiantoGruppoTermico, null, null);
            GridViewVisita.DataBind();
            CalcolareLaDistanza();

            GridViewListaNelProgramma.DataBind();
        }
    }

    protected void GridViewVisita_BeforePerformDataSelect(object sender, EventArgs e)
    {
        lblMessageSearchCodiceTargatura.Visible = false;

        int iDProgrammaIspezioneAttivo = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();
        dsVisiteIspettive.WhereParameters["IDProgrammaIspezione"].DefaultValue = iDProgrammaIspezioneAttivo.ToString();

        //Se il campo di testo di ricerca del codice targatura non è vuoto allora ricavo da V_VER_IspezioniVisite la lista DISTNCT o il 
        //singolo record del campo IDIspezioneVisita visita poi riaggiorno la grid
        if (!string.IsNullOrEmpty(txtCodiceTargaturaInVisite.Text))
        {
            string codiceTargatura = txtCodiceTargaturaInVisite.Text.Trim();
            List<long> IDsList = UtilityVerifiche.GetListVisisteByCodiceTargatura(codiceTargatura);
            string visiteList = IDsList.Count > 0 ? "{" : string.Empty;
            foreach (int id in IDsList)
            {
                visiteList += id.ToString() + ",";
            }

            if (IDsList.Count > 0)
            {
                visiteList = visiteList.Substring(0, visiteList.Length - 1);
                visiteList += "}";
            }
            else
            {
                visiteList = "{0}";
                lblMessageSearchCodiceTargatura.Visible = true;
            }

            dsVisiteIspettive.Where = "(it.IDIspezioneVisita IN " + visiteList + ")";
        }

        //GridViewVisita.ClearSort();

        //// Ottieni la colonna corrispondente al campo fInIspezione
        //var column = GridViewVisita.Columns["fInIspezione"] as GridViewDataColumn;

        //if (column != null)
        //{
        //    // Ordina la griglia per il campo fInIspezione in ordine crescente
        //    GridViewVisita.SortBy(column, DevExpress.Data.ColumnSortOrder.Ascending);
        //}
    }


       


    protected void btnRicercaCodiceTargaturaInVisite_Click(object sender, EventArgs e)
    {
        GridViewVisita.DataBind();
    }

    public void RowCommandVisiteIspettive(object sender, CommandEventArgs e)
    {
        long iDIspezioneVisita = long.Parse(e.CommandArgument.ToString());

        if (e.CommandName == "CreaIspezioni")
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ShowPanelIspezioni", "lp.Show();", true);
            UtilityVerifiche.CreaIspezione(iDIspezioneVisita, 0, int.Parse(userInfo.IDUtente.ToString()));
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HidePanelIspezioni", "setInterval(function(){lp.Hide();},20000);", true);
        }
        else if (e.CommandName == "DeleteVisita")
        {
            int iDProgrammaIspezioneAttivo = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();
            UtilityVerifiche.CreaDeleteVisitaIspetiva("Delete", iDProgrammaIspezioneAttivo, iDIspezioneVisita);
            GridViewVisita.FocusedRowIndex = -1;
            GridViewVisita.Selection.UnselectAll();
            GridViewListaNelProgramma.Columns[13].Visible = false;
            CalcolareLaDistanza();
        }
        
        GridViewVisita.DataBind();
        GridViewListaNelProgramma.Columns[13].Visible = false;
        GridViewListaNelProgramma.Columns[9].Visible = false;
        DatiListaProgramma(null);
    }
           
    protected void GridViewVisita_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.GridViewRowType.Data) return;
        //if (e.RowType == GridViewRowType.Data)
        //{
        using (CriterDataModel ctx = new CriterDataModel())
            {
                int iDIspezioneVisita = int.Parse(e.GetValue("IDIspezioneVisita").ToString());
                var countLibretti = (IQueryable<VER_IspezioneVisitaInfo>)ctx.VER_IspezioneVisitaInfo.AsNoTracking();
                countLibretti = countLibretti.Where(a => a.IDIspezioneVisita == iDIspezioneVisita);
                
                var CountLibretti = countLibretti.Where(a => a.IDAccertamento == null).ToList();
                var CountAccertamenti = countLibretti.Where(a => a.IDAccertamento != null).ToList();

                var visita = (IQueryable<V_VER_IspezioniVisite>)ctx.V_VER_IspezioniVisite.AsNoTracking();
                visita = visita.Where(a => a.IDIspezioneVisita == iDIspezioneVisita);
                
                decimal ? importo = 0;
                if (visita != null)
                {
                    if (countLibretti.Count() > 0 || CountAccertamenti.Count() > 0)
                    {
                        importo = (decimal)visita.Sum(a => a.ImportoIspezione);
                    }                    
                }
                
                string geografyVisita = String.Join("<br/> ", visita.Select(s => s.CodiceCatastale.ToString()).Distinct());


            //var CountLibretti = ctx.VER_IspezioneVisitaInfo.Where(c => c.IDIspezioneVisita == iDIspezioneVisita && c.IDAccertamento == null).ToList();
            //var CountAccertamenti = ctx.VER_IspezioneVisitaInfo.Where(c => c.IDIspezioneVisita == iDIspezioneVisita && c.IDAccertamento != null).ToList();

            //bool fVisitaInIspezione;
            //bool fVisitaInIspezione = (bool.TryParse(e.GetValue("fInIspezione").ToString(), out fVisitaInIspezione));
            bool fVisitaInIspezione = UtilityVerifiche.fVisitaInIspezione(iDIspezioneVisita);


            ImageButton ImgCreaIspezioni = GridViewVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "ImgCreaIspezioni") as ImageButton;
                ImageButton ImgDeleteVisita = GridViewVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "ImgDeleteVisita") as ImageButton;
                Image imgfInIspezione = GridViewVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "imgfInIspezione") as Image;
                
                if ((CountLibretti.Count() > 0 || CountAccertamenti.Count() > 0) && !fVisitaInIspezione)
                {
                    ImgCreaIspezioni.Visible = true;
                    imgfInIspezione.ImageUrl = "images/no.png";
                }
                if (ImgCreaIspezioni.Visible == true || (CountLibretti.Count() == 0 || CountAccertamenti.Count() == 0))
                {
                    ImgDeleteVisita.Visible = true;
                    imgfInIspezione.ImageUrl = "images/no.png";
                }
                if (fVisitaInIspezione)
                {
                    ImgDeleteVisita.Visible = false;
                    imgfInIspezione.ImageUrl = "images/si.png";                   
                }
                
                Label lblCountLibrettiNellaVisita = GridViewVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "lblCountLibrettiNellaVisita") as Label;
                Label lblCountAccertamentiNellaVisita = GridViewVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "lblCountAccertamentiNellaVisita") as Label;
                Label lblImportoVisita = GridViewVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "lblImportoVisita") as Label;
                Label lblUbicazioniLibretti = GridViewVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "lblUbicazioniLibretti") as Label;

                lblCountLibrettiNellaVisita.Text = CountLibretti.Count().ToString();
                lblCountAccertamentiNellaVisita.Text = CountAccertamenti.Count().ToString();
                lblImportoVisita.Text = string.Format("{0:N0}",  importo);
                lblUbicazioniLibretti.Text = geografyVisita;
            }
        //}
    }

    protected void GridViewVisita_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                int iDIspezioneVisita = int.Parse(e.GetValue("IDIspezioneVisita").ToString());

                var CountLibrettiAccertamenti = ctx.VER_IspezioneVisitaInfo.Where(c => c.IDIspezioneVisita == iDIspezioneVisita).ToList();

                ImageButton ImgCreaIspezioni = GridViewVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "ImgCreaIspezioni") as ImageButton;
                ImageButton ImgDeleteVisita = GridViewVisita.FindRowCellTemplateControl(e.VisibleIndex, null, "ImgDeleteVisita") as ImageButton;

                if (GridViewVisita.FocusedRowIndex == e.VisibleIndex && !UtilityVerifiche.fVisitaInIspezione(iDIspezioneVisita))
                {
                    if (CountLibrettiAccertamenti.Count() > 0)
                    {
                        ImgCreaIspezioni.Visible = true;
                    }
                    ImgDeleteVisita.Visible = true;
                }
                else
                {
                    ImgCreaIspezioni.Visible = false;
                    ImgDeleteVisita.Visible = false;
                }
            }
        }
    }

    protected void GridViewVisita_FocusedRowChanged(object sender, EventArgs e)
    {
        if (GridViewVisita.FocusedRowIndex != -1)
        {
            int iDIspezioneVisita = int.Parse(GridViewVisita.GetRowValues(GridViewVisita.FocusedRowIndex, "IDIspezioneVisita").ToString());

            if (!UtilityVerifiche.fVisitaInIspezione(iDIspezioneVisita))
            {
                GridViewListaNelProgramma.Columns[13].Visible = true;
                GridViewVisita.DataBind();
                CalcolareLaDistanza();
            }
            else
            {
                GridViewListaNelProgramma.Columns[13].Visible = false;
                GridViewListaNelProgramma.Columns[9].Visible = false;
                GridViewVisita.DataBind();
                DatiListaProgramma(null);
            }
        }
        else
        {
            GridViewListaNelProgramma.Columns[13].Visible = false;
            GridViewListaNelProgramma.Columns[9].Visible = false;
            GridViewVisita.Selection.UnselectAll();
            GridViewVisita.DataBind();
            DatiListaProgramma(null);
        }
    }
    
    //protected void GridViewVisita_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    //{
    //    ASPxGridView grdLibrettiNellaVista = (ASPxGridView)GridViewVisita.FindDetailRowTemplateControl(e.VisibleIndex, "grdLibrettiNellaVista");
    //    if (grdLibrettiNellaVista != null)
    //    {
    //        for (int i = 0; i < grdLibrettiNellaVista.VisibleRowCount - 1; i++)
    //        {
    //            TextBox txtNoteIspezioneVisita = grdLibrettiNellaVista.FindRowCellTemplateControl(e.VisibleIndex, null, "txtNoteIspezioneVisita") as TextBox;
    //            var cc = txtNoteIspezioneVisita.Text;
    //        }
    //    }    
    //}

    #endregion

    #region RICERCA LIBRETTI/ACCERTAMENTI DA INSERIRE NELLE VISITE ISPETTIVE

    public void CalcolareLaDistanza()
    {
        if (GridViewVisita.FocusedRowIndex != -1)
        {
            int iDIspezioneVisita = int.Parse(GridViewVisita.GetRowValues(GridViewVisita.FocusedRowIndex, "IDIspezioneVisita").ToString());

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var InizioCalcolo = (from a in ctx.V_LIM_LibrettiImpianti
                                     join c in ctx.VER_IspezioneVisitaInfo on a.IDLibrettoImpianto equals c.IDLibrettoImpianto
                                     where (c.IDIspezioneVisita == iDIspezioneVisita)
                                     select a).AsQueryable().FirstOrDefault();

                if (InizioCalcolo != null && !UtilityVerifiche.fVisitaInIspezione(iDIspezioneVisita))
                {
                    DatiListaProgramma(int.Parse(InizioCalcolo.IDLibrettoImpianto.ToString()));
                    GridViewListaNelProgramma.Columns[9].Visible = true;
                }
                else
                {
                    DatiListaProgramma(null);
                    GridViewListaNelProgramma.Columns[9].Visible = false;
                }
            }
        }
        else
        {
            DatiListaProgramma(null);
            GridViewListaNelProgramma.Columns[9].Visible = false;
        }
        GridViewListaNelProgramma.DataBind();
    }

    protected void RowCommandListaPerVisiteIspettive(object sender, CommandEventArgs e)
    {
        int iDProgrammaIspezioneAttivo = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        int iDLibrettoImpianto = int.Parse(commandArgs[0]);
        int iDLibrettoImpiantoGruppoTermico = int.Parse(commandArgs[1]);
        int? iDAccertamento = null;
        int iDTargaturaImpianto = int.Parse(commandArgs[3]);

        if (!String.IsNullOrEmpty(commandArgs[2]))
        {
            iDAccertamento = int.Parse(commandArgs[2]);
        }

        if (e.CommandName == "DeleteLibretto")
        {
            UtilityVerifiche.InsertDeleteGeneratoreNelProgrammaIspezioneAttivo("Delete", iDProgrammaIspezioneAttivo, iDLibrettoImpianto, iDTargaturaImpianto, iDAccertamento, iDLibrettoImpiantoGruppoTermico);

            GridViewListaNelProgramma.DataBind();
            GetListProgrammaIspezione();
            GetAccertamentiInAttesaIspezione();
            GetLibrettiImpianti();
            CalcolareLaDistanza();
        }
        else if (e.CommandName == "InsertNellaVisitaIspettiva")
        {
            int iDVisitaIspettiva = Convert.ToInt32(GridViewVisita.GetRowValues(GridViewVisita.FocusedRowIndex, "IDIspezioneVisita"));

            UtilityVerifiche.InsertDeleteGeneratoreNelleVisiteIspettive("Insert", null, iDVisitaIspettiva, iDLibrettoImpianto, iDProgrammaIspezioneAttivo, iDLibrettoImpiantoGruppoTermico, iDAccertamento, null);
            GridViewVisita.DataBind();
            CalcolareLaDistanza();

            GridViewListaNelProgramma.DataBind();
        }
        else if (e.CommandName == "InsertNellaVisitaIspettivaNota")
        {
            lblHiddenIDLibrettoImpiantoGruppoTermico.Text = iDLibrettoImpiantoGruppoTermico.ToString();
            GridViewListaNelProgramma.Visible = false;
            tblAddNotaGridViewListaNelProgramma.Visible = true;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var generatoreInProgramma = ctx.VER_ProgrammaIspezioneInfo.Where(a => a.IDLibrettoImpiantoGruppoTermico == iDLibrettoImpiantoGruppoTermico).FirstOrDefault();
                if (!string.IsNullOrEmpty(generatoreInProgramma.NoteProgrammaIspezione))
                    txtNoteListaNelProgramma.Text = generatoreInProgramma.NoteProgrammaIspezione;
            }
        } 
    }

    public void DatiListaProgramma(int? iDLibrettoImpianto)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            int iDProgrammaIspezioneAttivo = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();

            if (iDLibrettoImpianto != null)
            {
                var coordinateLibretto = (from l in ctx.LIM_LibrettiImpianti
                                          where (l.IDLibrettoImpianto == iDLibrettoImpianto)
                                          select new
                                          {
                                              l.Coordinate
                                          }).FirstOrDefault();

                var GetListaNelProgramma = (from a in ctx.V_VER_ProgrammaIspezioneGeneratori
                                            where (a.fInVisitaIspettiva == false && a.IDProgrammaIspezione == iDProgrammaIspezioneAttivo)
                                            select new
                                            {
                                                a.IDLibrettoImpiantoGruppoTermico,
                                                Generatore = a.Prefisso + a.CodiceProgressivo,
                                                a.IDLibrettoImpianto,
                                                a.IDAccertamento,
                                                a.CodiceCatastale,
                                                indirizzo = a.Indirizzo + " " + a.Civico,
                                                a.CodiceTargatura,
                                                Responsabile = a.NomeResponsabile + " " + a.CognomeResponsabile,
                                                km = a.Coordinate.Distance(coordinateLibretto.Coordinate) / 1000,
                                                a.TipoIspezione,
                                                a.PotenzaTermicaUtileNominaleKw,
                                                a.TipologiaCombustibile,
                                                a.IDTargaturaImpianto,
                                                a.NoteProgrammaIspezione,
                                                TerzoResponsabile = (a.RagioneSocialeTerzoResponsabile == null && a.NomeTerzoResponsabile ==null && a.CognomeTerzoResponsabile == null) ? null : a.RagioneSocialeTerzoResponsabile !=null ? a.RagioneSocialeTerzoResponsabile : a.NomeTerzoResponsabile + " " + a.CognomeTerzoResponsabile,
                                                ImportoIspezione = a.ImportoIspezione,
                                                Impresa = a.SoggettoAzienda
                                            }).ToList();

                GridViewListaNelProgramma.DataSource = GetListaNelProgramma;
            }
            else
            {
                var GetListaNelProgramma = (from a in ctx.V_VER_ProgrammaIspezioneGeneratori
                                            where (a.fInVisitaIspettiva == false && a.IDProgrammaIspezione == iDProgrammaIspezioneAttivo)
                                            select new
                                            {
                                                a.IDLibrettoImpiantoGruppoTermico,
                                                Generatore = a.Prefisso + a.CodiceProgressivo,
                                                a.IDLibrettoImpianto,
                                                a.IDAccertamento,
                                                a.CodiceCatastale,
                                                indirizzo = a.Indirizzo + " " + a.Civico,
                                                a.CodiceTargatura,
                                                Responsabile = a.NomeResponsabile + " " + a.CognomeResponsabile,
                                                km = a.Coordinate.Distance(a.Coordinate) / 1000,
                                                a.TipoIspezione,
                                                a.PotenzaTermicaUtileNominaleKw,
                                                a.TipologiaCombustibile,
                                                a.IDTargaturaImpianto,
                                                a.NoteProgrammaIspezione,
                                                TerzoResponsabile = (a.RagioneSocialeTerzoResponsabile == null && a.NomeTerzoResponsabile == null && a.CognomeTerzoResponsabile == null) ? null : a.RagioneSocialeTerzoResponsabile != null ? a.RagioneSocialeTerzoResponsabile : a.NomeTerzoResponsabile + " " + a.CognomeTerzoResponsabile,
                                                ImportoIspezione = a.ImportoIspezione,
                                                Impresa = a.SoggettoAzienda
                                            }).ToList();

                GridViewListaNelProgramma.DataSource = GetListaNelProgramma;
            }
            GridViewListaNelProgramma.DataBind();
        }
    }
    
    protected void GridViewListaNelProgramma_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            object kM = e.GetValue("km");
            ImageButton ImgInsertNellaVisitaIspettiva = GridViewListaNelProgramma.FindRowCellTemplateControl(e.VisibleIndex, null, "ImgInsertNellaVisitaIspettiva") as ImageButton;
         
            if (kM != null)
            {
                decimal dKm = decimal.Parse(kM.ToString());
                if (dKm > 100) // se la distanza > 100km non sono da inserire 
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    //ImgInsertNellaVisitaIspettiva.Visible = false;
                }
            }
        }
    }
        
    protected void btnSaveNoteListaNelProgramma_Click(object sender, EventArgs e)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            int iDLibrettoImpiantoGruppoTermico = int.Parse(lblHiddenIDLibrettoImpiantoGruppoTermico.Text);

            if (!string.IsNullOrEmpty(txtNoteListaNelProgramma.Text))
            {
                var generatoreInProgramma = ctx.VER_ProgrammaIspezioneInfo.Where(a => a.IDLibrettoImpiantoGruppoTermico == iDLibrettoImpiantoGruppoTermico).FirstOrDefault();
                generatoreInProgramma.NoteProgrammaIspezione = txtNoteListaNelProgramma.Text;

                ctx.SaveChanges();
            }

            lblHiddenIDLibrettoImpiantoGruppoTermico.Text = string.Empty;
            GridViewListaNelProgramma.Visible = true;
            tblAddNotaGridViewListaNelProgramma.Visible = false;
            txtNoteListaNelProgramma.Text = string.Empty;

            DatiListaProgramma(null);
        }
    }

    protected void btnAnnullaNoteListaNelProgramma_Click(object sender, EventArgs e)
    {
        lblHiddenIDLibrettoImpiantoGruppoTermico.Text = string.Empty;
        GridViewListaNelProgramma.Visible = true;
        tblAddNotaGridViewListaNelProgramma.Visible = false;
        txtNoteListaNelProgramma.Text = string.Empty;
    }
    #endregion

    protected void btnInserisciLibrettiNelProgrammaIspezione_Click(object sender, EventArgs e)
    {
        int iDProgrammaIspezioneAttivo = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();

        if (rblTipoRicerca.SelectedIndex == 0) //Inserimento dei generatori da accertamenti
        {
            for (int i = 0; i < DataGridAccertamentiInAttesaIspezione.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)DataGridAccertamentiInAttesaIspezione.Items[i].Cells[8].FindControl("chkSelezioneAccertamenti");
                if (chk.Checked)
                {
                    int iDAccertamento = int.Parse(DataGridAccertamentiInAttesaIspezione.Items[i].Cells[0].Text);
                    int iDLibrettoImpianto = int.Parse(DataGridAccertamentiInAttesaIspezione.Items[i].Cells[1].Text);
                    int iDLibrettoImpiantoGruppoTermico = int.Parse(DataGridAccertamentiInAttesaIspezione.Items[i].Cells[2].Text);
                    int iDTargaturaImpianto = int.Parse(DataGridAccertamentiInAttesaIspezione.Items[i].Cells[5].Text);

                    UtilityVerifiche.InsertDeleteGeneratoreNelProgrammaIspezioneAttivo("Insert", iDProgrammaIspezioneAttivo, iDLibrettoImpianto, iDTargaturaImpianto, iDAccertamento, iDLibrettoImpiantoGruppoTermico);
                }
            }

            GetAccertamentiInAttesaIspezione();
        }
        else //Inserimento dei generatori da catasto
        {
            bool fReload = false;
            for (int i = 0; i < DataGridLibretti.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)DataGridLibretti.Items[i].Cells[6].FindControl("chkSelezioneLibretti");
                if (chk.Checked)
                {
                    int iDLibrettoImpiantoGruppoTermico = int.Parse(DataGridLibretti.Items[i].Cells[0].Text);
                    int iDLibrettoImpianto = int.Parse(DataGridLibretti.Items[i].Cells[1].Text);
                    int iDTargaturaImpianto = int.Parse(DataGridLibretti.Items[i].Cells[2].Text);
                    UtilityVerifiche.InsertDeleteGeneratoreNelProgrammaIspezioneAttivo("Insert", iDProgrammaIspezioneAttivo, iDLibrettoImpianto, iDTargaturaImpianto, null, iDLibrettoImpiantoGruppoTermico);
                    fReload = true;
                }
            }

            if (fReload)
            {
                GetLibrettiImpianti();
            }
        }
                
        GetDatiAll();
        btnInserireLibretti.Visible = false;
    }

    protected void btnRicercaLibrettiDaInserireNelProgrammaIspezioneRegoleControlli_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VER_ProgrammaIspezioneRegoleControlli.aspx");
    }

    protected void btnCreaVisiteIspettiveAutomatiche_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VER_ProgrammaIspezioneVisiteIspettiveAutomatico.aspx");
    }


    
}
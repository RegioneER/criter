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

public partial class VER_AccertamentiSearchFirma : System.Web.UI.Page
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
        //SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "VER_AccertamentiSearchFirma.aspx");

        if (!Page.IsPostBack)
        {
            PagePermission();
            LoadAllDropDownlist();
            ASPxComboBox1.Focus();
        }

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
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
        int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, this.DataGrid.CurrentPageIndex, this.DataGrid.PageSize);
        this.SortExpression = currentSortExpression;

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "ACCERTAMENTI CORRISPONDENTI AI PARAMETRI IMPOSTATI");
    }

    private string BuildStr()
    {
        List<object> valoriProcedure = new List<object>();
        
        int? iDAccertatore = null;
        if (info.IDRuolo == 6)
        {
            iDAccertatore = info.IDUtente;
        }

        string strSql = UtilityVerifiche.GetSqlValoriAccertamentiFilter(IDTipoAccertamento,
                                                                        ASPxComboBox1.Value,
                                                                        6,
                                                                        valoriProcedure.ToArray<object>(),
                                                                        txtDataRilevazioneDa.Text,
                                                                        txtDataRilevazioneAl.Text,
                                                                        txtCodiceAccertamento.Text,
                                                                        txtCodiceTargatura.Text,
                                                                        iDAccertatore,
                                                                        null,
                                                                        null,
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
            CheckBox chkSelezione = (CheckBox)e.Item.Cells[10].FindControl("chkSelezione");
            chkSelezione.Attributes.Add("UIDGuidAccertamento", e.Item.Cells[11].Text);

            DataGrid dgDocumenti = (DataGrid)e.Item.Cells[8].FindControl("dgDocumenti");
            GetDocumentiAccertamenti(dgDocumenti, long.Parse(e.Item.Cells[0].Text));

            GenerateAutomaticReport(long.Parse(e.Item.Cells[0].Text));
        }
    }

    public static void GenerateAutomaticReport(long iDAccertamento)
    {
        var documenti = UtilityVerifiche.GetDocumentiAccertamenti(iDAccertamento);
        foreach (var documento in documenti)
        {
            string destinationFile = ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + documento.CodiceAccertamento;
            string fileAccertamento = destinationFile + @"\" + documento.IDAccertamento + "_" + documento.IDProceduraAccertamento + ".pdf";
            if (!File.Exists(ConfigurationManager.AppSettings["PathDocument"] + fileAccertamento))
            {
                string reportPath = ConfigurationManager.AppSettings["ReportPath"];
                string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
                string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();
                string reportName = ConfigurationManager.AppSettings["ReportNameAccertamento"];
                string urlPdf = ReportingServices.GetAccertamentiReport(documento.IDAccertamento.ToString(), documento.IDProceduraAccertamento.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);
            }
        }
    }

    public void RowCommand(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            QueryString qs = new QueryString();
            qs.Add("IDAccertamento", e.CommandArgument.ToString());
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "VER_Accertamenti.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
    }

    #endregion

    public void GetAccertamenti()
    {
        System.Threading.Thread.Sleep(500);
        DataGrid.CurrentPageIndex = 0;
        BindGrid(true);
    }

    protected void chkSelezione_CheckedChanged(object sender, EventArgs e)
    {
        bool fVisible = false;
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[10].FindControl("chkSelezione");
            if (chk.Checked)
            {
                fVisible = true;
                break;
            }
        }
        btnConfirmFirma.Visible = fVisible;
    }

    protected void chkSelezioneAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[10].FindControl("chkSelezione");
            if (chk.Enabled)
            {
                chk.Checked = ((CheckBox)sender).Checked;
            }
        }

        bool fVisible = false;
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[10].FindControl("chkSelezione");
            if (chk.Checked)
            {
                fVisible = true;
                break;
            }
        }
        btnConfirmFirma.Visible = fVisible;
    }

    protected void btnRicerca_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetAccertamenti();
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

    #region Firma

    protected void btnConfirmFirma_Click(object sender, EventArgs e)
    {
        VisibleHiddenFirma(true);
        SetUrlDownload();
    }

    protected void btnFirmaAnnulla_Click(object sender, EventArgs e)
    {
        VisibleHiddenFirma(false);
    }

    protected void btnFirma_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            FirmaLogic();
        }
    }
    
    public void VisibleHiddenFirma(bool fVisible)
    {
        row1.Visible = !fVisible;
        row2.Visible = !fVisible;
        row3.Visible = !fVisible;
        row4.Visible = !fVisible;
        row6.Visible = !fVisible;
        row7.Visible = !fVisible;
        row8.Visible = !fVisible;

        if (!fVisible)
        {
            rowFirma.Visible = false;
        }
        else
        {
            rowFirma.Visible = true;
        }

        rowButtonFirma.Visible = fVisible;
    }

    protected void FirmaLogic()
    {
        #region FIRMA
        if (UploadFileP7m.HasFiles && UploadFileP7m.PostedFiles != null)
        {
            int counter = 0;

            HttpFileCollection fileCollection = Request.Files;
            for (int j = 0; j < fileCollection.Count; j++)
            {
                HttpPostedFile uploadfile = fileCollection[j];
                string fileName = Path.GetFileName(uploadfile.FileName);
                if (uploadfile.ContentLength > 0)
                {
                    string[] array = fileName.Replace(".pdf", "").Replace(".p7m", "").Split('_');
                    for (int i = 0; i < DataGrid.Items.Count; i++)
                    {
                        string iDAccertamento = string.Empty;

                        CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[10].FindControl("chkSelezione");
                        if (chk.Checked)
                        {
                            iDAccertamento = DataGrid.Items[i].Cells[0].Text;
                            int iDUtenteAccertatore = int.Parse(DataGrid.Items[i].Cells[5].Text);
                            int iDUtenteCoordinatore = int.Parse(DataGrid.Items[i].Cells[6].Text);
                            int iDUtenteAgenteAccertatore = int.Parse(DataGrid.Items[i].Cells[12].Text);

                            if (iDAccertamento == array[0])
                            {
                                var documenti = UtilityVerifiche.GetDocumentiAccertamenti(long.Parse(iDAccertamento));
                                foreach (var documento in documenti)
                                {
                                    if (documento.IDProceduraAccertamento.ToString() == array[1])
                                    {
                                        int iDUtente = int.Parse(info.IDUtente.ToString());
                                        int iDSoggetto = int.Parse(info.IDSoggetto.ToString());

                                        string PathP7mFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + documento.CodiceAccertamento + @"\";
                                        uploadfile.SaveAs(PathP7mFile + fileName);

                                        byte[] p7mFileToByteArrayInfoSigner = FirmaLib.FirmaDSS.NewInstance.FileToByteArray(PathP7mFile + fileName);

                                        object[] getValInfoSigner = new object[8];
                                        getValInfoSigner = FirmaLib.FirmaDSS.NewInstance.GetInfoSignerCertificate(p7mFileToByteArrayInfoSigner);

                                        UtilityVerifiche.SaveInsertDeleteDatiAccertamentiFirmaDigitale(
                                                                                                        documento.IDAccertamento,
                                                                                                        iDSoggetto,
                                                                                                        PathP7mFile + fileName,
                                                                                                        DateTime.Now,
                                                                                                        UtilityApp.GetUserIP(),
                                                                                                        getValInfoSigner[0].ToString(),
                                                                                                        getValInfoSigner[1].ToString(),
                                                                                                        getValInfoSigner[2].ToString(),
                                                                                                        getValInfoSigner[3].ToString(),
                                                                                                        getValInfoSigner[4].ToString(),
                                                                                                        getValInfoSigner[5].ToString(),
                                                                                                        getValInfoSigner[6].ToString(),
                                                                                                        getValInfoSigner[7].ToString()
                                                                                                      );

                                        UtilityVerifiche.CambiaStatoAccertamento(documento.IDAccertamento, 5, iDUtenteAccertatore, iDUtenteCoordinatore, iDUtenteAgenteAccertatore);
                                        UtilityVerifiche.StoricizzaStatoAccertamento(documento.IDAccertamento, iDUtente);
                                        //if (documento.IDProceduraAccertamento.ToString() == "1" || documento.IDProceduraAccertamento.ToString() == "2" 
                                        //    || documento.IDProceduraAccertamento.ToString() == "5" || documento.IDProceduraAccertamento.ToString() == "6") //Per Ispezioni
                                        //{
                                        //    EmailNotify.SendMailComunicazioneAccertamento(documento.IDAccertamento, documento.IDProceduraAccertamento);
                                        //}
                                        EmailNotify.SendMailComunicazioneAccertamento(documento.IDAccertamento, documento.IDProceduraAccertamento);
                                        UtilityPosteItaliane.SendToPosteItaliane(documento.IDAccertamento, (int)DataUtilityCore.Enum.EnumTypeofRaccomandata.TypeAccertamento);
                                        UtilityVerifiche.SottoponiAdIntervento(documento.IDAccertamento);

                                        counter++;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (counter > 0)
            {
                tblInfoFirmaOk.Visible = true;
                tblAccertamenti.Visible = false;

                QueryString qs = new QueryString();
                qs.Add("IDTipoAccertamento", IDTipoAccertamento);
                QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                string url = "VER_AccertamentiSearchFirma.aspx";
                url += qsEncrypted.ToString();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + url + "';},2000);", true);
            }
        }
        #endregion
    }

    public void SetUrlDownload()
    {
        string iDAccertamenti = string.Empty;
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[10].FindControl("chkSelezione");
            if (chk.Checked)
            {
                iDAccertamenti += DataGrid.Items[i].Cells[0].Text + ",";
            }
        }

        if (iDAccertamenti.Length > 0)
        {
            iDAccertamenti = iDAccertamenti.Substring(0, iDAccertamenti.Length - 1);
        }
        
        string url = ConfigurationManager.AppSettings["UrlPortal"] + "VER_AccertamentiDownload.aspx?iDAccertamenti=" + iDAccertamenti;
        
        this.imgExportZip.Attributes.Add("onclick",
            "var win=dhtmlwindow.open('ZipAccertamentiExport', 'iframe', '" +
            url +
            "', 'Scarica file zip accertamenti da firmare', 'width=100px,height=100px,resize=1,scrolling=1,center=1'); win.hide();");
        this.imgExportZip.Attributes.Add("style", "cursor: pointer;");
    }

    #endregion

}
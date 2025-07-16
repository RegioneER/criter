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

public partial class VER_SanzioniSearchFirma : System.Web.UI.Page
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
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "VER_SanzioniSearchFirma.aspx");

        if (!Page.IsPostBack)
        {
            PagePermission();
            LoadAllDropDownlist();
            GetAccertamentiSanzioni();
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
                cmbIspettore.Visible = true;
                lblSoggetto.Visible = false;
                break;
            case "6": //Accertatore

                break;
            case "7": //Coordinatore

                break;
            case "8": //Ispettore
                cmbIspettore.Value = getVal[0];
                cmbIspettore.Visible = false;
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0]);
                lblSoggetto.Visible = true;
                break;
            case "9": //Segreteria Verifiche

                break;
        }
    }

    protected void LoadAllDropDownlist()
    {
        
    }

    #region RICERCA ISPETTORE

    protected void cmbIspettore_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "7", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void cmbIspettore_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;
        int IdSoggetto = int.Parse(e.Value.ToString());
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "7", e.Value.ToString(), "");
        comboBox.DataBind();
    }

    protected void cmbIspettore_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBoxIspettori();
    }

    protected void RefreshAspxComboBoxIspettori()
    {
        cmbIspettore.SelectedIndex = -1;
        cmbIspettore.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "7", "", "", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
        cmbIspettore.DataBind();
    }

    #endregion

    #region ACCERTAMENTI
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpressionSanzioniFirma"] == null)
                return string.Empty;
            return ViewState["SortExpressionSanzioniFirma"].ToString();
        }
        set
        {
            ViewState["SortExpressionSanzioniFirma"] = value;
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

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "NOTIFICHE SANZIONI DA FIRMARE CORRISPONDENTI AI PARAMETRI IMPOSTATI");
    }

    private string BuildStr()
    {
        string strSql = UtilityVerifiche.GetSqlValoriSanzioniFilter(cmbIspettore.Value,
                                                                    txtCodiceIspezione.Text,
                                                                    txtCodiceAccertamento.Text,
                                                                    txtCodiceTargatura.Text,
                                                                    2
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
            CheckBox chkSelezione = (CheckBox)e.Item.Cells[7].FindControl("chkSelezione");
            chkSelezione.Attributes.Add("UIDGuidAccertamento", e.Item.Cells[8].Text);

            GenerateAutomaticReport(long.Parse(e.Item.Cells[0].Text), e.Item.Cells[3].Text);

            QueryString qs = new QueryString();
            qs.Add("IDAccertamento", e.Item.Cells[0].Text);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            //string[] getVal = new string[2];
            //getVal = UtilityVerifiche.GetProvenienzaSanzione(long.Parse(e.Item.Cells[0].Text));

            string url = "VER_Sanzioni.aspx"; //getVal[0];
            url += qsEncrypted.ToString();

            ImageButton ImgView = (ImageButton)(e.Item.Cells[6].FindControl("ImgView"));
            ImgView.OnClientClick = "javascript:window.open('" + url + "','newWindow');";
        }
    }

    public static void GenerateAutomaticReport(long iDAccertamento, string codiceAccertamento)
    {
        string destinationFile = ConfigurationManager.AppSettings["UploadSanzioni"] + @"\" + codiceAccertamento;
        string fileName = destinationFile + @"\" + ConfigurationManager.AppSettings["ReportNameSanzione"] + "_" + iDAccertamento + ".pdf";

        if (!File.Exists(ConfigurationManager.AppSettings["PathDocument"] + fileName))
        {
            string reportPath = ConfigurationManager.AppSettings["ReportPath"];
            string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
            string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();
            string reportName = ConfigurationManager.AppSettings["ReportNameSanzione"];
            string urlPdf = ReportingServices.GetVerbaleSanzioneReport(iDAccertamento.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);
        }
    }
    
    #endregion

    public void GetAccertamentiSanzioni()
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
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[7].FindControl("chkSelezione");
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
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[7].FindControl("chkSelezione");
            if (chk.Enabled)
            {
                chk.Checked = ((CheckBox)sender).Checked;
            }
        }

        bool fVisible = false;
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[7].FindControl("chkSelezione");
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
            GetAccertamentiSanzioni();
        }
    }

    #region FIRMA SANZIONI

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
        row0.Visible = !fVisible;
        row1.Visible = !fVisible;
        row2.Visible = !fVisible;
        row3.Visible = !fVisible;
        row4.Visible = !fVisible;
        row5.Visible = !fVisible;
        row6.Visible = !fVisible;

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
                        string codiceAccertamento = string.Empty;

                        CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[7].FindControl("chkSelezione");
                        if (chk.Checked)
                        {
                            iDAccertamento = DataGrid.Items[i].Cells[0].Text;
                            codiceAccertamento = DataGrid.Items[i].Cells[3].Text;

                            if (iDAccertamento == array[1])
                            {
                                int iDUtente = int.Parse(info.IDUtente.ToString());
                                int iDSoggetto = int.Parse(info.IDSoggetto.ToString());

                                string PathP7mFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadSanzioni"] + @"\" + codiceAccertamento + @"\";
                                UtilityFileSystem.CreateDirectoryIfNotExists(PathP7mFile);
                                uploadfile.SaveAs(PathP7mFile + fileName);

                                byte[] p7mFileToByteArrayInfoSigner = FirmaLib.FirmaDSS.NewInstance.FileToByteArray(PathP7mFile + fileName);

                                object[] getValInfoSigner = new object[8];
                                getValInfoSigner = FirmaLib.FirmaDSS.NewInstance.GetInfoSignerCertificate(p7mFileToByteArrayInfoSigner);

                                UtilityVerifiche.SaveInsertDeleteDatiAccertamentiFirmaDigitale(int.Parse(iDAccertamento),
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

                                UtilityVerifiche.CambiaStatoSanzione(int.Parse(iDAccertamento), 1);
                                //UtilityPosteItaliane.SendToPosteItaliane(int.Parse(iDAccertamento), (int)DataUtilityCore.Enum.EnumTypeofRaccomandata.TypeSanzione);

                                counter++;
                            }
                        }
                    }
                }
            }

            if (counter > 0)
            {
                tblInfoFirmaOk.Visible = true;
                tblAccertamenti.Visible = false;

                string url = "VER_SanzioniSearchFirma.aspx";
                
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
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[7].FindControl("chkSelezione");
            if (chk.Checked)
            {
                iDAccertamenti += DataGrid.Items[i].Cells[0].Text + ",";
            }
        }

        if (iDAccertamenti.Length > 0)
        {
            iDAccertamenti = iDAccertamenti.Substring(0, iDAccertamenti.Length - 1);
        }
        
        string url = ConfigurationManager.AppSettings["UrlPortal"] + "VER_SanzioniDownload.aspx?iDAccertamenti=" + iDAccertamenti;
        
        this.imgExportZip.Attributes.Add("onclick",
            "var win=dhtmlwindow.open('ZipSanzioniExport', 'iframe', '" +
            url +
            "', 'Scarica file zip sanzioni da firmare', 'width=100px,height=100px,resize=1,scrolling=1,center=1'); win.hide();");
        this.imgExportZip.Attributes.Add("style", "cursor: pointer;");
    }

    #endregion

}
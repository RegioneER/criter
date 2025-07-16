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
using System.IO;
using Criter.Rapporti;
using Newtonsoft.Json;
using System.Web.UI;

public partial class RCT_RapportoDiControlloTecnicoSearchFirma : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
                ASPxComboBox1.Visible = true;
                lblSoggetto.Visible = false;
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
                lblSoggetto.Visible = true;
                ASPxComboBox1.Value = getVal[2].ToString();
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                break;
            case "10": //Responsabile tecnico
                ASPxComboBox1.Value = getVal[2].ToString();
                ASPxComboBox1.Visible = false;

                GetComboBoxFilterByIDAzienda();

                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                lblSoggetto.Visible = true;
                break;
        }
    }

    protected void LoadAllDropDownlist()
    {
        rbTipologiaRapportoDiControllo(null);
        rbTipologiaControllo(null);
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

    #region RAPPORTI DI CONTROLLO
    protected string SortExpression
    {
        get
        {
            if (ViewState["SortExpressionRapportiDaFirmare"] == null)
                return string.Empty;
            return ViewState["SortExpressionRapportiDaFirmare"].ToString();
        }
        set
        {
            ViewState["SortExpressionRapportiDaFirmare"] = value;
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
        string sql = BuildStr();// Session["sqlstrRapportiDaFirmare"].ToString();
        string currentSortExpression = this.SortExpression;
        //int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, (int) Session["currentPageIndexRapportiDaFirmare"], DataGrid.PageSize);
        int totRecords = UtilityApp.BindControl(reload, this.DataGrid, sql, currentSortExpression, this.DataGrid.CurrentPageIndex, DataGrid.PageSize);
        this.SortExpression = currentSortExpression;

        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "RAPPORTI DI CONTROLLO DA FIRMARE CORRISPONDENTI AI PARAMETRI IMPOSTATI");
    }

    private string BuildStr()
    {
        string strSql = UtilityRapportiControllo.GetSqlValoriRapportiFilter(ASPxComboBox1.Value,
                                                                            ASPxComboBox2.Value,
                                                                            txtCodiceTargatura.Text,
                                                                            "3",
                                                                            rblTipologieRapportoDiControllo.SelectedItem.Value,
                                                                            rblTipologieControllo.SelectedItem.Value,
                                                                            string.Empty,
                                                                            null,
                                                                            null,
                                                                            null,
                                                                            null,
                                                                            null,
                                                                            null,
                                                                            null,
                                                                            string.Empty,
                                                                            null,
                                                                            string.Empty,
                                                                            string.Empty,
                                                                            string.Empty,
                                                                            string.Empty,
                                                                            string.Empty);
        return strSql.ToString();
    }

    public void DataGrid_PageChanger(object source, DataGridPageChangedEventArgs e)
    {
        //Session["currentPageIndexRapportiDaFirmare"] = e.NewPageIndex;
        //DataGrid.CurrentPageIndex = (int) Session["currentPageIndexRapportiDaFirmare"];
        DataGrid.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
    }

    public void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            ImageButton imgView = (ImageButton) (e.Item.Cells[6].FindControl("ImgView"));
            ImageButton imgPdf = (ImageButton) (e.Item.Cells[7].FindControl("ImgPdf"));

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
        }
    }

    public void RowCommand(Object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        if (e.CommandName == "Delete")
        {

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
        else if (e.CommandName == "Pdf")
        {

        }
    }

    #endregion

    public void GetRapportiControllo()
    {
        System.Threading.Thread.Sleep(500);
        //Session["sqlstrRapportiDaFirmare"] = BuildStr();
        //Session["currentPageIndexRapportiDaFirmare"] = 0;
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

    protected void chkSelezione_CheckedChanged(object sender, EventArgs e)
    {
        bool fVisible = false;
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox) DataGrid.Items[i].Cells[8].FindControl("chkSelezione");
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
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[8].FindControl("chkSelezione");
            if (chk.Enabled)
            {
                chk.Checked = ((CheckBox)sender).Checked;
            }
        }
        
        bool fVisible = false;
        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)DataGrid.Items[i].Cells[8].FindControl("chkSelezione");
            if (chk.Checked)
            {
                fVisible = true;
                break;
            }
        }
        btnConfirmFirma.Visible = fVisible;
    }

    protected void btnConfirmFirma_Click(object sender, EventArgs e)
    {
        VisibleHiddenFirma(true);
        string filename = GenerateGlobalTxtFile();
        SetUrlDownload(filename);
    }

    protected void btnFirmaAnnulla_Click(object sender, EventArgs e)
    {
        VisibleHiddenFirma(false);
        File.Delete(lblNameGlobalFile.Text);
    }

    public string GenerateGlobalTxtFile()
    {
        string filename = Guid.NewGuid().ToString() + ".txt";
        List<string> filetoSign = new List<string>();

        for (int i = 0; i < DataGrid.Items.Count; i++)
        {
            CheckBox chk = (CheckBox) DataGrid.Items[i].Cells[8].FindControl("chkSelezione");
            if (chk.Checked)
            {
                using (var ctx = new CriterDataModel())
                {
                    int iDRapportoControlloTecnico = int.Parse(DataGrid.Items[i].Cells[0].Text);
                    var rapporto = ctx.RCT_RapportoDiControlloTecnicoBase.FirstOrDefault(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico);

                    switch (rapporto.IDTipologiaRCT)
                    {
                        case 1: //Gruppi Termici
                            POMJ_RapportoControlloTecnico_GT rctGT = JsonConvert.DeserializeObject<POMJ_RapportoControlloTecnico_GT>(rapporto.JsonFormat);
                            List<string> fileGT = CriterAPI.Json.CriterJsonTranform.J_RCTGT2TXT(rctGT);
                            filetoSign.AddRange(fileGT);
                            break;
                        case 2: //Gruppi Frigo
                            POMJ_RapportoControlloTecnico_GF rctGF = JsonConvert.DeserializeObject<POMJ_RapportoControlloTecnico_GF>(rapporto.JsonFormat);
                            List<string> fileGF = CriterAPI.Json.CriterJsonTranform.J_RCTGF2TXT(rctGF);
                            filetoSign.AddRange(fileGF);
                            break;
                        case 3: //Scambiatori
                            POMJ_RapportoControlloTecnico_SC rctSC = JsonConvert.DeserializeObject<POMJ_RapportoControlloTecnico_SC>(rapporto.JsonFormat);
                            List<string> fileSC = CriterAPI.Json.CriterJsonTranform.J_RCTSC2TXT(rctSC);
                            filetoSign.AddRange(fileSC);
                            break;
                        case 4://Cogeneratori
                            POMJ_RapportoControlloTecnico_CG rctCG = JsonConvert.DeserializeObject<POMJ_RapportoControlloTecnico_CG>(rapporto.JsonFormat);
                            List<string> fileCG = CriterAPI.Json.CriterJsonTranform.J_RCTCG2TXT(rctCG);
                            filetoSign.AddRange(fileCG);
                            break;
                    }
                }
            }
        }

        string pathRapporti = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadRapportiControllo"] + @"\" + filename;
        File.WriteAllLines(pathRapporti, filetoSign.ToArray());
        txtJsonTxt.Text = File.ReadAllText(pathRapporti).Replace("<", " ").Replace(">", " ");

        lblNameGlobalFile.Text = pathRapporti;

        return filename;
    }

    protected void btnFirma_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
            if (info.IDRuolo == 1)
            {
                TypeFirmaOrSpid(null, null, 1, false);
            }
            else
            {
                TypeFirmaOrSpid(info.IDSoggetto, info.IDSoggettoDerived, info.IDRuolo, info.fSpid);
            }
        }
    }

    public void VisibleHiddenFirma(bool fVisible)
    {
        row1.Visible = !fVisible;
        row2.Visible = !fVisible;
        row3.Visible = !fVisible;
        row4.Visible = !fVisible;
        row5.Visible = !fVisible;
        row6.Visible = !fVisible;
        row7.Visible = !fVisible;
        row8.Visible = !fVisible;

        if (!fVisible)
        {
            rowSpid.Visible = false;
            rowFirma.Visible = false;
            rowFileTxt.Visible = false;
        }
        else
        {
            UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
            if (info.IDRuolo == 1)
            {
                int iDSoggetto = 0;
                if (ASPxComboBox1.Value != null)
                {
                    iDSoggetto = int.Parse(ASPxComboBox1.Value.ToString());
                    bool fSpidUser = SecurityManager.GetSpidUser(iDSoggetto);
                    if (fSpidUser)
                    {
                        rowSpid.Visible = true;
                        rowFirma.Visible = false;
                    }
                    else
                    {
                        rowSpid.Visible = false;
                        rowFirma.Visible = true;
                    }
                }
            }
            else
            {
                if (info.fSpid)
                {
                    rowSpid.Visible = true;
                    rowFirma.Visible = false;
                }
                else
                {
                    rowSpid.Visible = false;
                    rowFirma.Visible = true;
                }
            }
        }

        rowButtonFirma.Visible = fVisible;
        rowFileTxt.Visible = fVisible;
    }

    protected void TypeFirmaOrSpid(int? iDSoggetto, int? iDSoggettoDerived, int? iDRuolo, bool fSpidCheck)
    {
        if (!fSpidCheck)
        {
            #region FIRMA
            if (UploadFileP7m.HasFile && UploadFileP7m.PostedFile != null)
            {
                #region Firma
                string FileP7m = UploadFileP7m.FileName;
                string PathTempP7mFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadRapportiControllo"] + @"\Temp" + @"\";
                string PathP7mFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadRapportiControllo"] + @"\";
                string PathOriginalFile = lblNameGlobalFile.Text;
                string FullPathTempP7mFile = PathTempP7mFile + FileP7m;
                UploadFileP7m.SaveAs(FullPathTempP7mFile);

                //Devo manipolare il file firmato togliendo la parte firmata per il confronto
                byte[] p7mFileToByteArray = FirmaLib.FirmaDSS.NewInstance.FileToByteArray(FullPathTempP7mFile);
                byte[] p7mDecode = FirmaLib.FirmaDSS.NewInstance.Decodep7m(p7mFileToByteArray);

                string p7mFileDecode = PathTempP7mFile + "tmp_" + FileP7m.Replace(".p7m", ".txt");
                File.WriteAllBytes(p7mFileDecode, p7mDecode.ToArray());

                bool SameFile = true;// Encoding.UTF8.GetBytes(File.ReadAllText(p7mFileDecode)).SequenceEqual(Encoding.UTF8.GetBytes(txtJsonTxt.Text));

                if (SameFile)
                {
                    File.Delete(p7mFileDecode);

                    string GetFileOriginalFile = Path.GetFileNameWithoutExtension(PathOriginalFile);
                    string GetFileTempP7mFile = Path.GetFileNameWithoutExtension(FullPathTempP7mFile);
                    if (File.Exists(PathP7mFile + GetFileOriginalFile + ".p7m"))
                    {
                        File.Delete(PathP7mFile + GetFileOriginalFile + ".p7m");
                    }

                    File.Move(FullPathTempP7mFile, PathP7mFile + GetFileOriginalFile + ".p7m");

                    #region InfoSigner
                    byte[] p7mFileToByteArrayInfoSigner = FirmaLib.FirmaDSS.NewInstance.FileToByteArray(PathP7mFile + GetFileOriginalFile + ".p7m");

                    object[] getValInfoSigner = new object[8];
                    getValInfoSigner = FirmaLib.FirmaDSS.NewInstance.GetInfoSignerCertificate(p7mFileToByteArrayInfoSigner);

                    for (int i = 0; i < DataGrid.Items.Count; i++)
                    {
                        CheckBox chk = (CheckBox) DataGrid.Items[i].Cells[8].FindControl("chkSelezione");
                        if (chk.Checked)
                        {
                            if (iDRuolo == 1) //Amministratore Criter
                            {
                                iDSoggetto = int.Parse(DataGrid.Items[i].Cells[2].Text);
                                iDSoggettoDerived = int.Parse(DataGrid.Items[i].Cells[2].Text);
                            }
                            else if (iDRuolo == 2) //Amministratore azienda
                            {
                                iDSoggettoDerived = iDSoggetto;
                            }

                            int iDRapportoControllo = int.Parse(DataGrid.Items[i].Cells[0].Text);
                            bool fpass = true;
                            using (var ctx = new CriterDataModel())
                            {
                                var firma = ctx.RCT_FirmaDigitale.Where(c => c.IDRapportoControlloTecnico == iDRapportoControllo).ToList();
                                if (firma.Count > 0)
                                {
                                    fpass = false;
                                }
                            }

                            if (fpass)
                            {
                                UtilityRapportiControllo.SaveInsertDeleteDatiRapportiFirmaDigitale(
                                                                                        iDRapportoControllo,
                                                                                        (int)iDSoggetto,
                                                                                        (int)iDSoggettoDerived,
                                                                                        2,
                                                                                        PathP7mFile + GetFileOriginalFile + ".p7m",
                                                                                        DateTime.Now,
                                                                                        UtilityApp.GetUserIP(),
                                                                                        getValInfoSigner[0].ToString(),
                                                                                        getValInfoSigner[1].ToString(),
                                                                                        getValInfoSigner[2].ToString(),
                                                                                        getValInfoSigner[3].ToString(),
                                                                                        getValInfoSigner[4].ToString(),
                                                                                        getValInfoSigner[5].ToString(),
                                                                                        getValInfoSigner[6].ToString(),
                                                                                        getValInfoSigner[7].ToString());
                            }

                            UtilityRapportiControllo.ChangeStatoRct(iDRapportoControllo);
                            UtilityBollini.UtilizzaBollinisuRCT(iDRapportoControllo);
                                
                            int iDTargaturaImpianto = int.Parse(DataGrid.Items[i].Cells[3].Text);
                            string prefisso = DataGrid.Items[i].Cells[10].Text;
                            int codiceProgressivo = int.Parse(DataGrid.Items[i].Cells[11].Text);
                            DateTime dataControllo = DateTime.Parse(DataGrid.Items[i].Cells[12].Text);
                            UtilityVerifiche.ControllaInterventiPresenzaNuovoRCT(iDTargaturaImpianto, prefisso, codiceProgressivo, dataControllo);
                            UtilityVerifiche.SottoponiAdAccertamento(iDRapportoControllo, 1, null);
                        }
                    }
                    tblInfoFirmaOk.Visible = true;
                    tblRapportiControllo.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='RCT_RapportoDiControlloTecnicoSearchFirma.aspx';},2000);", true);
                    #endregion
                }
                else
                {
                    lblCheckP7m.ForeColor = ColorTranslator.FromHtml("#800000");
                    lblCheckP7m.Text = "<br/>Attenzione: i dati del file firmato .p7m non corrispondono con i dati inseriti sul Criter!";
                }

                #endregion
            }
            else
            {
                lblCheckP7m.ForeColor = ColorTranslator.FromHtml("#800000");
                lblCheckP7m.Text = "<br/>Attenzione: bisogna selezionare il file firmato digitalmente in formato .p7m da caricare!";
            }
            #endregion
        }
        else
        {
            #region SPID
            for (int i = 0; i < DataGrid.Items.Count; i++)
            {
                CheckBox chk = (CheckBox) DataGrid.Items[i].Cells[8].FindControl("chkSelezione");
                if (chk.Checked)
                {
                    if (iDRuolo == 1) //Amministratore Criter
                    {
                        iDSoggetto = int.Parse(DataGrid.Items[i].Cells[2].Text);
                        iDSoggettoDerived = int.Parse(DataGrid.Items[i].Cells[2].Text);
                    }
                    else if (iDRuolo == 2) //Amministratore azienda
                    {
                        iDSoggettoDerived = iDSoggetto;
                    }
                    int iDRapportoControllo = int.Parse(DataGrid.Items[i].Cells[0].Text);
                    int? iDSoggettoInsert = UtilityRapportiControllo.SaveInsertDeleteDatiRapportiFirmaDigitale(
                                                                                   iDRapportoControllo,
                                                                                   (int) iDSoggetto,
                                                                                   (int) iDSoggettoDerived,
                                                                                   1,
                                                                                   string.Empty,
                                                                                   DateTime.Now,
                                                                                   UtilityApp.GetUserIP(),
                                                                                   string.Empty,
                                                                                   string.Empty,
                                                                                   string.Empty,
                                                                                   string.Empty,
                                                                                   string.Empty,
                                                                                   string.Empty,
                                                                                   string.Empty,
                                                                                   string.Empty);

                    UtilityRapportiControllo.ChangeStatoRct(iDRapportoControllo);
                    UtilityBollini.UtilizzaBollinisuRCT(iDRapportoControllo);

                    int iDTargaturaImpianto = int.Parse(DataGrid.Items[i].Cells[3].Text);
                    string prefisso = DataGrid.Items[i].Cells[10].Text;
                    int codiceProgressivo = int.Parse(DataGrid.Items[i].Cells[11].Text);
                    DateTime dataControllo = DateTime.Parse(DataGrid.Items[i].Cells[12].Text);
                    UtilityVerifiche.ControllaInterventiPresenzaNuovoRCT(iDTargaturaImpianto, prefisso, codiceProgressivo, dataControllo);
                    
                    UtilityVerifiche.SottoponiAdAccertamento(iDRapportoControllo, 1, null);
                }
            }
            tblInfoFirmaOk.Visible = true;
            tblRapportiControllo.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='RCT_RapportoDiControlloTecnicoSearchFirma.aspx';},2000);", true);
            #endregion
        }
    }

    public void SetUrlDownload(string fileName)
    {
        QueryString qs = new QueryString();
        qs.Add("fileName", fileName.ToString());
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = ConfigurationManager.AppSettings["UrlPortal"] + "RCT_RapportiControlloDownload.aspx";
        url += qsEncrypted.ToString();

        this.imgExportJson.Attributes.Add("onclick",
            "var win=dhtmlwindow.open('JsonExport_" + fileName + "', 'iframe', '" +
            url +
            "', 'Scarica files dei rapporti da firmare', 'width=100px,height=100px,resize=1,scrolling=1,center=1'); win.hide();");
        this.imgExportJson.Attributes.Add("style", "cursor: pointer;");
    }

}
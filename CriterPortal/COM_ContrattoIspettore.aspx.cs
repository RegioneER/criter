using DataLayer;
using DataUtilityCore;
using DevExpress.Web;
using EncryptionQS;
using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class COM_ContrattoIspettore : System.Web.UI.Page
{
    protected string IDContrattoIspettore
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
            else
            {
                #region Encrypt off
                try
                {
                    if (Request.QueryString["IDContrattoIspettore"] != null)
                    {
                        return (string)Request.QueryString["IDContrattoIspettore"];
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
    
    protected string IDIspettore
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
                        if (qsdec[1] != null)
                        {
                            return (string)qsdec[1];
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
            else
            {
                #region Encrypt off
                try
                {
                    if (Request.QueryString["IDIspettore"] != null)
                    {
                        return (string)Request.QueryString["IDIspettore"];
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

    UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "COM_ContrattoIspettore.aspx");

        if (!Page.IsPostBack)
        {
            ModificaNuovoContrattoIspettore();
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
    }

    #region ISPETTORE / DROPDOWNLIST / FILEMANAGER / PAGEPERMISSION

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);

        sm.RegisterPostBackControl(fileManagerDocumenti);
        //sm.EnablePartialRendering = false;
    }

    protected void FileManager()
    {
        string path = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadContrattiIspettore"] + "\\" + IDIspettore;
        UtilityFileSystem.CreateDirectoryIfNotExists(path + @"\");

        fileManagerDocumenti.Settings.RootFolder = path;
        //fileManagerDocumenti.Settings.InitialFolder = path;
        fileManagerDocumenti.SettingsUpload.AdvancedModeSettings.TemporaryFolder = path;

        if (userInfo.IDRuolo == 1)
        {
            fileManagerDocumenti.SettingsEditing.AllowCreate = true;
            fileManagerDocumenti.SettingsEditing.AllowCopy = true;
            fileManagerDocumenti.SettingsEditing.AllowDelete = true;
            fileManagerDocumenti.SettingsEditing.AllowMove = true;
            fileManagerDocumenti.SettingsEditing.AllowRename = true;
            fileManagerDocumenti.SettingsEditing.AllowDownload = true;
        }
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
            case "7": //Ispettore
                cmbIspettore.Value = getVal[0];
                cmbIspettore.Visible = false;
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0]);
                lblSoggetto.Visible = true;
                break;
        }
    }

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
        RefreshAspxComboBox();
    }

    protected void RefreshAspxComboBox()
    {
        cmbIspettore.SelectedIndex = -1;
        cmbIspettore.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "7", "", "", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
        cmbIspettore.DataBind();
    }

    protected void LoadAllDropDownlist()
    {
        rbStatoContratto(null);
    }

    protected void rbStatoContratto(int? idPresel)
    {
        rblStatoContratto.Items.Clear();
        rblStatoContratto.DataValueField = "IDStatoContratto";
        rblStatoContratto.DataTextField = "StatoContratto";
        rblStatoContratto.DataSource = LoadDropDownList.LoadDropDownList_SYS_StatoContratto(idPresel);
        rblStatoContratto.DataBind();

        rblStatoContratto.SelectedIndex = -1;
    }
    #endregion

    public void ModificaNuovoContrattoIspettore()
    {
        LoadAllDropDownlist();

        if (string.IsNullOrEmpty(IDContrattoIspettore))
        {
            lblTitoloContrattoIspettore.Text = "<h2> NUOVO CONTRATTO ISPETTORE </h2> ";
        }
        else
        {
            lblTitoloContrattoIspettore.Text = "<h2> MODIFICA CONTRATTO ISPETTORE </h > ";
            cvControllaContrattoPresente.Enabled = false;
            cmbIspettore.Visible = false;
            lblSoggetto.Visible = true;
            rowDocumenti.Visible = true;
            rowFileManager.Visible = true;
            FileManager();         
            GetDatiModificaIspettoreContratto();
        }
    }
    
    public void GetDatiModificaIspettoreContratto()
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            int iDContrattoIspettore = int.Parse(IDContrattoIspettore);

            var contratto = ctx.V_COM_ContrattoIspettore.Where(c => c.IDContrattoIspettore == iDContrattoIspettore).FirstOrDefault();
            cmbIspettore.Value = IDIspettore;
            lblSoggetto.Text = contratto.Nome + " " + contratto.Cognome;
            txtDataInizioContratto.Text = contratto.DataInizioContratto.ToString("d");
            txtDataFineContratto.Text = contratto.DataFineContratto.ToString("d");
            txtNumeroIspezioniMax.Text = contratto.NumeroIspezioniMax.ToString();
            cbAttivo.Checked = contratto.fAttivo;

            rblStatoContratto.SelectedValue = contratto.IDStatoContratto.ToString();

            GetFirma(contratto.Firma);
        }
    }

    public void GetFirma(object firma)
    {
        if (firma != null)
        {
            System.IO.Stream fs = new System.IO.MemoryStream((byte[])firma);
            if (fs != null)
            {
                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                imgFirma.ImageUrl = "data:image/png;base64," + base64String;
                btnDeleteFirma.Visible = true;
            }
            else
            {
                btnDeleteFirma.Visible = false;
            }
        }
        else
        {
            btnDeleteFirma.Visible = false;
        }
    }

    protected void btnSalvaDati_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (string.IsNullOrEmpty(IDIspettore))
            {
                int iDContrattoIspettore = UtilitySoggetti.SaveInsertDeleteDatiContrattoIspettore("insert",
                                                                UtilityApp.ParseNullableInt(IDContrattoIspettore),
                                                                 int.Parse(cmbIspettore.Value.ToString()),
                                                                 int.Parse(rblStatoContratto.SelectedValue.ToString()),
                                                                 int.Parse(txtNumeroIspezioniMax.Text),
                                                                 DateTime.Parse(txtDataInizioContratto.Text.ToString()),
                                                                 DateTime.Parse(txtDataFineContratto.Text.ToString()),
                                                                 cbAttivo.Checked
                                                                );

                int iDIspettore = int.Parse(cmbIspettore.Value.ToString());

                string pathContratto = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadContrattiIspettore"] + @"\" + iDIspettore.ToString() + @"\";
                UtilityFileSystem.CreateDirectoryIfNotExists(pathContratto);

                UploadFirmaIspettore(iDContrattoIspettore);

                QueryString qs = new QueryString();
                qs.Add("IDContrattoIspettore", iDContrattoIspettore.ToString());
                qs.Add("IDIspettore", IDIspettore);
                QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                string url = "COM_ContrattoIspettore.aspx";
                url += qsEncrypted.ToString();
                Response.Redirect(url);
            }
            else
            {
                int iDContrattoIspettore = UtilitySoggetti.SaveInsertDeleteDatiContrattoIspettore("update",
                                                                         UtilityApp.ParseNullableInt(IDContrattoIspettore),
                                                                         int.Parse(IDIspettore),
                                                                         int.Parse(rblStatoContratto.SelectedValue.ToString()),
                                                                         int.Parse(txtNumeroIspezioniMax.Text),
                                                                         DateTime.Parse(txtDataInizioContratto.Text.ToString()),
                                                                         DateTime.Parse(txtDataFineContratto.Text.ToString()),                                                                        
                                                                         cbAttivo.Checked
                                                                         );

                QueryString qs = new QueryString();
                qs.Add("IDContrattoIspettore", iDContrattoIspettore.ToString());
                qs.Add("IDIspettore", IDIspettore);
                QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                string url = "COM_ContrattoIspettore.aspx";
                url += qsEncrypted.ToString();
                Response.Redirect(url);

            }
        }
    }

    protected void btnAnnulla_Click(object sender, EventArgs e)
    {
        GoToSearch();
    }

    public void GoToSearch()
    {
        string url = "COM_ContrattoIspettoreSearch.aspx";
        Response.Redirect(url);
    }

    public void ControllaContrattoPresente(Object sender, ServerValidateEventArgs e)
    {
        bool fContratto = UtilitySoggetti.CheckContrattoExit(int.Parse(cmbIspettore.Value.ToString()));
        if (fContratto)
        {
            e.IsValid = false;
            vgContratto.ShowSummary = true;
        }
        else
        {
            e.IsValid = true;
            vgContratto.ShowSummary = false;
        }
    }

    protected void btnUploadFirma_Click(object sender, EventArgs e)
    {
        UploadFirmaIspettore(null);
    }

    protected void UploadFirmaIspettore(int? iDContrattoIspettore)
    {
        if (updFirma.HasFile)
        {
            int? iDContrattoIspettoreInt = null;
            if (iDContrattoIspettore != null)
            {
                iDContrattoIspettoreInt = iDContrattoIspettore;
            }
            else
            {
                iDContrattoIspettoreInt = UtilityApp.ParseNullableInt(IDContrattoIspettore);
            }


            byte[] imageData = UtilityApp.GetImageBytes(updFirma.PostedFile.InputStream);
            UtilitySoggetti.SaveFirmaIspettore("update", iDContrattoIspettoreInt, imageData);
            GetDatiModificaIspettoreContratto();
        }
    }
    
    protected void btnDeleteFirma_Click(object sender, EventArgs e)
    {
        UtilitySoggetti.SaveFirmaIspettore("delete", UtilityApp.ParseNullableInt(IDContrattoIspettore), null);
        GetDatiModificaIspettoreContratto();
    }

}
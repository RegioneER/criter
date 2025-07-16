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

public partial class ANAG_AnagraficaAccreditamento : System.Web.UI.Page
{
    protected string IDSoggetto
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
                    if (Request.QueryString["IDSoggetto"] != null)
                    {
                        return (string)Request.QueryString["IDSoggetto"];
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

    protected string IDTipoSoggetto
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
                    if (Request.QueryString["IDTipoSoggetto"] != null)
                    {
                        return (string)Request.QueryString["IDTipoSoggetto"];
                    }
                    else
                    {
                        return string.Empty;
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

    UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            VisibleHiddenTipoSoggetto();
            GetDatiAccreditamento(int.Parse(IDSoggetto));
            GetDatiAccreditamentoStorico(int.Parse(IDSoggetto));
            
        }
        FileManager();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        sm.RegisterPostBackControl(fileManagerDocumenti);
        //sm.EnablePartialRendering = false;
    }

    protected void VisibleHiddenTipoSoggetto()
    {
        switch (IDTipoSoggetto)
        {
            case "1": //Operatore/Addetto
                
                break;
            case "2": //Azienda
                lblTitoloTipoSoggetto.Text = "INFORMAZIONI GENERALI ACCREDITAMENTO IMPRESA";
                lblTitoloSoggetto.Text = "Impresa";
                rowAttivo.Visible = false;
                rowAttivazioneUtenzaIspettore.Visible = false;
                rowTitoloDocumenti.Visible = false;
                rowDocumenti.Visible = false;
                break;
            case "4": //Persona Responsabile Tecnico
                
                break;
            case "5": //Distributori di combustibile
                
                break;
            case "6": //Software house
                
                break;
            case "7": //Ispettori
                lblTitoloTipoSoggetto.Text = "INFORMAZIONI GENERALI ACCREDITAMENTO ISPETTORE";
                lblTitoloSoggetto.Text = "Ispettore";
                rowAttivo.Visible = true;
                rowAttivazioneUtenzaIspettore.Visible = true;
                rowDocumenti.Visible = true;
                break;
            case "8": //Cittadino
                
                break;
            case "9": //Enti locali
                
                break;
        }
    }

    protected void rbStatoAccreditamento(int? iDPresel, DateTime? dataAccreditamento, string iDTipoSoggetto)
    {
        rblStatoAccreditamento.Items.Clear();
        var result = LoadDropDownList.LoadDropDownList_SYS_StatoAccreditamento(iDPresel, int.Parse(iDTipoSoggetto));
        foreach (var row in result)
        {
            ListEditItem item = new ListEditItem(row.StatoAccreditamento, row.IDStatoAccreditamento, row.ImageUrlStatoAccreditamento);
            rblStatoAccreditamento.Items.Add(item);
            if (iDPresel == row.IDStatoAccreditamento)
            {
                rblStatoAccreditamento.Items.FindByText(row.StatoAccreditamento).Selected = true;
            }
        }
    }

    public void GetDatiAccreditamento(int iDSoggetto)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            var accreditamento = ctx.V_COM_AnagraficaSoggetti.Where(c => c.IDSoggetto == iDSoggetto).FirstOrDefault();
            lblIDStatoAccreditamento.Text = accreditamento.IDStatoAccreditamento.ToString();

            switch (IDTipoSoggetto)
            {
                case "1": //Operatore/Addetto

                    break;
                case "2": //Azienda
                    lnkSoggetto.Text = accreditamento.NomeAzienda;
                    checkUtenzaIspettoreExist(int.Parse(IDSoggetto));
                    break;
                case "4": //Persona Responsabile Tecnico

                    break;
                case "5": //Distributori di combustibile

                    break;
                case "6": //Software house

                    break;
                case "7": //Ispettori
                    lnkSoggetto.Text = accreditamento.Nome + " " + accreditamento.Cognome;
                    break;
                case "8": //Cittadino

                    break;
                case "9": //Enti locali

                    break;
            }

            

            QueryString qs = new QueryString();
            qs.Add("IDSoggetto", accreditamento.IDSoggetto.ToString());
            qs.Add("IDTipoSoggetto", accreditamento.IDTipoSoggetto.ToString());
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "ANAG_Anagrafica.aspx";
            url += qsEncrypted.ToString();
            lnkSoggetto.NavigateUrl = url;

            lblEmail.Text = accreditamento.Email;
            lblEmailPec.Text = accreditamento.EmailPec;


            rbStatoAccreditamento(accreditamento.IDStatoAccreditamento, accreditamento.DataAccreditamento, accreditamento.IDTipoSoggetto.ToString());
            bool fAttivoAccreditamento = bool.Parse(accreditamento.fAttivoAccreditamento.ToString());
            imgFlagAttivo.ImageUrl = UtilityApp.BooleanFlagToImage(fAttivoAccreditamento);
            lblfAttivo.Text = accreditamento.fAttivoAccreditamento.ToString();

            lblDataAccreditamento.Text = string.Format("{0:dd/MM/yyyy}", accreditamento.DataAccreditamento);
            txtDataAccreditamento.Text = string.Format("{0:dd/MM/yyyy}", accreditamento.DataAccreditamento);

            lblDataRinnovo.Text = string.Format("{0:dd/MM/yyyy}", accreditamento.DataRinnovo);

            txtDataAnnullamento.Text = string.Format("{0:dd/MM/yyyy}", accreditamento.DataAnnullamento);
            txtMotivoAnnullamento.Text = accreditamento.MotivazioneAnnullamento;

            txtDataSospensioneDa.Text = string.Format("{0:dd/MM/yyyy}", accreditamento.DataSospensioneDa);
            txtDataSospensioneA.Text = string.Format("{0:dd/MM/yyyy}", accreditamento.DataSospensioneA);
            txtMotivoSospensione.Text = accreditamento.MotivazioneSospensione;

            LogicStatiAccreditamento(accreditamento.DataAccreditamento, accreditamento.IDStatoAccreditamento, IDTipoSoggetto);
        }
    }

    public void GetDatiAccreditamentoStorico(int iDSoggetto)
    {
        var accreditamentoStorico = UtilitySoggetti.GetAccreditamentoStorico(iDSoggetto);
        if (accreditamentoStorico != null)
        {
            DataGrid.DataSource = accreditamentoStorico;
            DataGrid.DataBind();
        }
    }

    public void checkUtenzaIspettoreExist(int iDSoggetto)
    {
        bool fexist = SecurityManager.CheckSoggettoWithUtenza(iDSoggetto);
        if (fexist)
        {
            rowAttivazioneUtenzaIspettore.Visible = false;
        }
        else
        {
            rowAttivazioneUtenzaIspettore.Visible = true;
        }
    }

    protected void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Label lblSospensioneAnnullamento = (Label)(e.Item.Cells[3].FindControl("lblSospensioneAnnullamento"));
            if (e.Item.Cells[2].Text == "6" || e.Item.Cells[2].Text == "7" || e.Item.Cells[2].Text == "9" || e.Item.Cells[2].Text == "10")
            {
                lblSospensioneAnnullamento.Visible = true;
                if (e.Item.Cells[2].Text == "6" || e.Item.Cells[2].Text == "9")
                {
                    if (e.Item.Cells[6].Text != "&nbsp;" & e.Item.Cells[8].Text != "&nbsp;")
                    {
                        lblSospensioneAnnullamento.Text = "<b>Data sospensione</b>:&nbsp;dal&nbsp;" + e.Item.Cells[6].Text + " al&nbsp;" + e.Item.Cells[7].Text + "<br/>Motivo sospensione:" + e.Item.Cells[8].Text;
                    }                   
                }
                else if (e.Item.Cells[2].Text == "7" || e.Item.Cells[2].Text == "10")
                {
                    if (e.Item.Cells[4].Text != "&nbsp;" & e.Item.Cells[5].Text != "&nbsp;")
                    {
                        lblSospensioneAnnullamento.Text = "<b>Data annullamento</b>:&nbsp;" + e.Item.Cells[4].Text + "<br/>Motivo annullamento:" + e.Item.Cells[5].Text;
                    }
                }
            }
            else
            {
                lblSospensioneAnnullamento.Visible = false;
            }
        }
    }

    protected void FileManager()
    {
        string path = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspettori"] + "\\" + IDSoggetto;
        UtilityFileSystem.CreateDirectoryIfNotExists(path);


        fileManagerDocumenti.Settings.RootFolder = path;
        //fileManagerDocumenti.Settings.InitialFolder = path;
        fileManagerDocumenti.SettingsUpload.AdvancedModeSettings.TemporaryFolder = path;

        fileManagerDocumenti.SettingsEditing.AllowCreate = true;
        fileManagerDocumenti.SettingsEditing.AllowCopy = true;
        fileManagerDocumenti.SettingsEditing.AllowDelete = true;
        fileManagerDocumenti.SettingsEditing.AllowMove = true;
        fileManagerDocumenti.SettingsEditing.AllowRename = true;
        fileManagerDocumenti.SettingsEditing.AllowDownload = true;
    }
    
    protected void btnSalvaDati_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int iDSoggetto = int.Parse(IDSoggetto);

            DateTime? dataRinnovo = null;
            if (IDTipoSoggetto == "7")
            {
                dataRinnovo = UtilitySoggetti.GetDataRinnovoAccreditamentoIspettori(UtilityApp.ParseNullableDatetime(txtDataAccreditamento.Text));
            }

            UtilitySoggetti.SaveInsertDeleteDatiAccreditamento(
                    "update",
                    iDSoggetto,
                    int.Parse(rblStatoAccreditamento.SelectedItem.Value.ToString()),
                    UtilityApp.ParseNullableDatetime(txtDataAccreditamento.Text),
                    dataRinnovo,
                    UtilityApp.ParseNullableDatetime(txtDataAnnullamento.Text),
                    txtMotivoAnnullamento.Text,
                    UtilityApp.ParseNullableDatetime(txtDataSospensioneDa.Text),
                    UtilityApp.ParseNullableDatetime(txtDataSospensioneA.Text),
                    txtMotivoSospensione.Text
                    );

            int iDStatoAccreditamentoAttuale = int.Parse(lblIDStatoAccreditamento.Text);
            int iDStatoAccreditamento = int.Parse(rblStatoAccreditamento.SelectedItem.Value.ToString());
            if (iDStatoAccreditamentoAttuale != iDStatoAccreditamento)
            {
                int iDUtente = int.Parse(info.IDUtente.ToString());
                UtilitySoggetti.StoricizzaStatoAccreditamento(iDSoggetto, iDUtente);
            }

            GetDatiAccreditamento(iDSoggetto);
            GetDatiAccreditamentoStorico(iDSoggetto);
        }
    }

    public void LogicStatiAccreditamento(DateTime? dataAccreditamento, int? iDStatoAccreditamento, string iDTipoSoggetto)
    {
        switch (iDStatoAccreditamento)
        {
            case 1: //In attesa di verifica
            case 2: //Affiancamento ispettore eseguito
            case 3://Ispettore qualificato - corso CRITER effettuato
                rowDataAccreditamento.Visible = false;
                rowDataRinnovo.Visible = false;
                rowDataAnnullamento.Visible = false;
                rowMotivoAnnullamento.Visible = false;
                rowDataSospensione.Visible = false;
                rowMotivoSospensione.Visible = false;


                txtDataAnnullamento.Text = String.Empty;
                txtMotivoAnnullamento.Text = String.Empty;
                txtDataSospensioneA.Text = String.Empty;
                txtDataSospensioneDa.Text = String.Empty;
                txtMotivoSospensione.Text = String.Empty;
                break;
            case 4: //Ispettore qualificato accreditato
            case 8: //Impresa Accreditata
                rowDataAccreditamento.Visible = true;
                
                rowDataAnnullamento.Visible = false;
                rowMotivoAnnullamento.Visible = false;
                rowDataSospensione.Visible = false;
                rowMotivoSospensione.Visible = false;

                txtDataAnnullamento.Text = String.Empty;
                txtMotivoAnnullamento.Text = String.Empty;
                txtDataSospensioneA.Text = String.Empty;
                txtDataSospensioneDa.Text = String.Empty;
                txtMotivoSospensione.Text = String.Empty;

                if (dataAccreditamento != null)
                {
                    lblDataAccreditamento.Visible = true;
                    txtDataAccreditamento.Visible = false;
                    if (IDTipoSoggetto == "7")
                    {
                        rowDataRinnovo.Visible = true;
                    }
                }
                else
                {
                    lblDataAccreditamento.Visible = false;
                    txtDataAccreditamento.Visible = true;
                    rowDataRinnovo.Visible = false;
                }
                break;
            case 5: //Ispettore qualificato scaduto
                
                break;
            case 6: //Ispettore qualificato sospeso
            case 9: //Impresa sospesa
                if (dataAccreditamento != null)
                {
                    lblDataAccreditamento.Visible = true;
                    txtDataAccreditamento.Visible = false;
                    if (IDTipoSoggetto == "7")
                    {
                        rowDataRinnovo.Visible = true;
                    }
                    rowDataAccreditamento.Visible = true;
                }
                else
                {
                    lblDataAccreditamento.Visible = false;
                    txtDataAccreditamento.Visible = true;
                    rowDataRinnovo.Visible = false;
                    rowDataAccreditamento.Visible = false;
                }
                                
                rowDataAnnullamento.Visible = false;
                rowMotivoAnnullamento.Visible = false;
                rowDataSospensione.Visible = true;
                rowMotivoSospensione.Visible = true;

                txtDataAnnullamento.Text = String.Empty;
                txtMotivoAnnullamento.Text = String.Empty;
                break;
            case 7: //Ispettore qualificato annullato
            case 10: //Impresa annullata
                if (dataAccreditamento != null)
                {
                    lblDataAccreditamento.Visible = true;
                    txtDataAccreditamento.Visible = false;
                    if (IDTipoSoggetto == "7")
                    {
                        rowDataRinnovo.Visible = true;
                    }
                    rowDataAccreditamento.Visible = true;
                }
                else
                {
                    lblDataAccreditamento.Visible = false;
                    txtDataAccreditamento.Visible = true;
                    rowDataRinnovo.Visible = false;
                    rowDataAccreditamento.Visible = false;
                }

                rowDataAnnullamento.Visible = true;
                rowMotivoAnnullamento.Visible = true;
                rowDataSospensione.Visible = false;
                rowMotivoSospensione.Visible = false;


                txtDataSospensioneA.Text = String.Empty;
                txtDataSospensioneDa.Text = String.Empty;
                txtMotivoSospensione.Text = String.Empty;
                break;
        }
    }

    protected void rblStatoAccreditamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime? dataAccreditamento = UtilityApp.ParseNullableDatetime(txtDataAccreditamento.Text);
        int iDStatoAccreditamento = int.Parse(rblStatoAccreditamento.SelectedItem.Value.ToString());
        LogicStatiAccreditamento(dataAccreditamento, iDStatoAccreditamento, IDTipoSoggetto);
    }

    protected void imgFlagAttivo_Click(object sender, ImageClickEventArgs e)
    {
        int iDSoggetto = int.Parse(IDSoggetto);
        bool fAttivo = bool.Parse(lblfAttivo.Text);
        bool newVal = UtilitySoggetti.ChangefAttivoAccreditamento(iDSoggetto, fAttivo);
        GetDatiAccreditamento(iDSoggetto);
        GetDatiAccreditamentoStorico(iDSoggetto);
    }
    
    protected void imgAttivaUtenzaIspettore_Click(object sender, ImageClickEventArgs e)
    {
        int iDSoggetto = int.Parse(IDSoggetto);
        SecurityManager.ActivateUserCredential(iDSoggetto, 7, 8, string.Empty, string.Empty, "insert");
        GetDatiAccreditamento(iDSoggetto);
        GetDatiAccreditamentoStorico(iDSoggetto);
        checkUtenzaIspettoreExist(iDSoggetto);
    }
}
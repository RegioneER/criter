using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DataUtilityCore;
using DevExpress.Web;
using SiMoka;
using DataLayer;
using System.Configuration;
using EncryptionQS;
using System.Web;
using System.Web.UI;
using System.Data.Entity.Validation;

public partial class LIM_LibrettiImpiantiNuovo : System.Web.UI.Page
{
    private CriterDataModel _ctx;

    public CriterDataModel ctx
    {
        get
        {
            if (_ctx == null)
            {
                _ctx = DataLayer.Common.ApplicationContext.Current.Context;
            }
            return _ctx;
        }
    }

    [Serializable]
    public struct DatiCatastaliStruct
    {
        public Guid IDDatiCatastali
        {
            get;
            set;
        }
        public string Foglio
        {
            get;
            set;
        }
        public string Mappatura
        {
            get;
            set;
        }
        public string SubMappatura
        {
            get;
            set;
        }
        public string Identificativo
        {
            get;
            set;
        }
        public Nullable<int> IDSezione
        {
            get;
            set;
        }
        public string Sezione
        {
            get;
            set;
        }

        public bool ValidazioneMoka
        {
            get;
            set;
        }
    }

    private const string DatiCatastaliKey = "DatiCatastali";

    public List<DatiCatastaliStruct> DatiCatastali
    {
        get
        {
            if (ViewState["DatiCatastali"] == null)
                ViewState["DatiCatastali"] = new List<DatiCatastaliStruct>();
            return (List<DatiCatastaliStruct>) ViewState["DatiCatastali"];
        }
        set
        {
            ViewState["DatiCatastali"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "LIM_LibrettiImpiantiNuovo.aspx");

        if (!IsPostBack)
        {
            PagePermission();
            cmbAziende.Focus();
        }
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                cmbAziende.Visible = true;
                cmbAddetti.Visible = true;
                lblSoggetto.Visible = false;
                lblSoggettoDerived.Visible = false;
                break;
            case "2": //Amministratore azienda
                cmbAziende.Value = getVal[0];
                cmbAziende.Visible = false;
                cmbAddetti.Visible = true;
                GetComboBoxFilterByIDAzienda();

                //ddCodiciTargature(null, cmbAziende.Value, cmbAddetti.Value);
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0]);
                lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = false;
                break;
            case "3": //Operatore/Addetto
                cmbAziende.Visible = false;
                cmbAddetti.Visible = false;
                lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = true;
                cmbAziende.Value = getVal[2];
                cmbAddetti.Value = getVal[0];
                ddCodiciTargature(null, cmbAziende.Value, cmbAddetti.Value);
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2]);
                lblSoggettoDerived.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0]);
                break;
            case "10": //Responsabile tecnico
                cmbAziende.Value = getVal[2];
                cmbAziende.Visible = false;
                cmbAddetti.Visible = true;
                GetComboBoxFilterByIDAzienda();

                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2]);
                lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = false;
                break;
        }
    }

    protected void VisibleCodiceCatastale(bool fVisible, object iDCodiceCastale)
    {
        if ((iDCodiceCastale.ToString() != "") && (iDCodiceCastale != null))
        {
            lnkInsertDatiCatastali.Visible = true;
            pnlDatiCatastaliView.Visible = true;
        }
        else
        {
            lnkInsertDatiCatastali.Visible = false;
            pnlDatiCatastaliView.Visible = false;
        }

        if (fVisible)
        {
            //pnlComuni.Visible = true;
            lblCodiceCatastale.Visible = false;
        }
        else
        {
            //pnlComuni.Visible = false;
            lblCodiceCatastale.Visible = true;
        }


    }

    protected void VisibleDatiCatastali(bool fVisible, object IDCodiceCastale)
    {
        if (fVisible)
        {
            pnlDatiCatastaliInsert.Visible = true;
            lnkInsertDatiCatastali.Visible = false;
        }
        else
        {
            pnlDatiCatastaliInsert.Visible = false;
            lnkInsertDatiCatastali.Visible = true;
        }

        //abilito la dropdown del comune se non ho ancora inserito alcun dato catastale
        RadComboBoxCodiciCatastali.Enabled = DatiCatastali.Count == 0 && !pnlDatiCatastaliInsert.Visible;
    }

    public void lnkSaveComune_Click(object sender, EventArgs e)
    {
        //RadCodiciCatastali(lblIDCodiceCatastale.Text);
        VisibleCodiceCatastale(true, "");
    }

    protected void ResetDatiCatastali()
    {
        ddlSezioneDatiCatastali.SelectedIndex = -1;
        txtFoglio.Text = "";
        txtMappale.Text = "";
        txtSubalterno.Text = "";
        txtIdentificativo.Text = "";
    }

    protected void btnSaveCodiceCatastale_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ResetDatiCatastali();
            //BindDataCertificato(lblIDCertificato.Text);
            VisibleCodiceCatastale(false, lblIDCodiceCatastale.Text);
        }
    }

    protected void btnAnnullaCodiceCatastale_Click(object sender, EventArgs e)
    {
        ResetDatiCatastali();
        VisibleCodiceCatastale(false, lblIDCodiceCatastale.Text);
    }

    protected void ddSezioneDatiCatastali(int? iDPresel, int iDCodiceCatastale)
    {
        ddlSezioneDatiCatastali.Items.Clear();
        ddlSezioneDatiCatastali.DataValueField = "IDCodiceCatastaleSezione";
        ddlSezioneDatiCatastali.DataTextField = "Sezione";
        ddlSezioneDatiCatastali.DataSource = LoadDropDownList.LoadDropDownList_SYS_CodiciCatastaliSezioni(iDPresel, iDCodiceCatastale);
        ddlSezioneDatiCatastali.DataBind();

        ListItem myItem = new ListItem("-- Selezionare --", "0");
        ddlSezioneDatiCatastali.Items.Insert(0, myItem);


        if (ddlSezioneDatiCatastali.Items.Count == 2)
        {
            ddlSezioneDatiCatastali.SelectedIndex = 1;
        }
        else
        {
            ddlSezioneDatiCatastali.SelectedIndex = 0;
        }
    }

    protected void lnkInsertDatiCatastali_Click(object sender, EventArgs e)
    {
        VisibleDatiCatastali(true, lblIDCodiceCatastale.Text);
        ddSezioneDatiCatastali(null, int.Parse(lblIDCodiceCatastale.Text));

        if (ddlSezioneDatiCatastali.Items.Count == 1)
        {
            rowSezioneDatiCatastali.Visible = false;
        }
        else
        {
            rowSezioneDatiCatastali.Visible = true;
        }
    }
    
    protected void SaveDatiCatastali()
    {
        var datiCatastali = DatiCatastali;

        bool fValidazioneMoka = false;
        if (ConfigurationManager.AppSettings["MokaEnabled"].Equals("on"))
        {
            var Comune = ctx.SYS_CodiciCatastali.Find(Int32.Parse(lblIDCodiceCatastale.Text));
            var Sezione = ctx.SYS_CodiciCatastaliSezioni.Find(Int32.Parse(ddlSezioneDatiCatastali.SelectedValue));

            string CodiceCatastale = (Comune != null && !string.IsNullOrEmpty(Comune.CodiceCatastale)) ? Comune.CodiceCatastale : string.Empty;
            string CodiceSezione = (Sezione != null && !string.IsNullOrEmpty(Sezione.CodiceSezione)) ? Sezione.CodiceSezione : string.Empty;

            MokaUtil.MokaRequestResult result = MokaUtil.ParametriCatastaliValidi(CodiceCatastale, CodiceSezione, txtFoglio.Text, txtMappale.Text, txtSubalterno.Text);
            fValidazioneMoka = result.RequestSucceded;
        }

        var datiCatastaliItem = new DatiCatastaliStruct()
        {
            IDDatiCatastali = Guid.NewGuid(),
            Foglio = txtFoglio.Text,
            Mappatura = txtMappale.Text,
            SubMappatura = txtSubalterno.Text,
            Identificativo = txtIdentificativo.Text,
            ValidazioneMoka = fValidazioneMoka,
        };

        int idSezione;
        if (int.TryParse(ddlSezioneDatiCatastali.SelectedValue, out idSezione))
        {
            if (idSezione > 0)
            {
                datiCatastaliItem.IDSezione = idSezione;
                datiCatastaliItem.Sezione = ddlSezioneDatiCatastali.SelectedItem.Text;
            }
        }
        datiCatastali.Add(datiCatastaliItem);
        //rimetto nel viewstate
        DatiCatastali = datiCatastali;
        ResetDatiCatastali();
        BindGrid();
        VisibleCodiceCatastale(false, lblIDCodiceCatastale.Text);
        VisibleDatiCatastali(false, lblIDCodiceCatastale.Text);
    }

    protected void btnSaveDatiCatastali_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            var argument = ((Button) sender).CommandName;
            if (argument != "ConfirmSaveDatiCatastali")
            {
                bool fEsiste = VerificaDatiCatastali();
                if (!fEsiste)
                {
                    SaveDatiCatastali();
                }
            }
            else
            {
                SaveDatiCatastali();
            }
        }
    }

    protected void btnAnnullaDatiCatastali_Click(object sender, EventArgs e)
    {
        VisibleDatiCatastali(false, lblIDCodiceCatastale.Text);
    }

    #region RICERCA AZIENDA
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
        int IdSoggetto = int.Parse(e.Value.ToString());
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "2", e.Value.ToString(), "");
        comboBox.DataBind();
    }

    protected void ASPxComboBox_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetComboBoxFilterByIDAzienda();
    }

    protected void ASPxComboBox1_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBox();
    }

    protected void RefreshAspxComboBox()
    {
        cmbAziende.SelectedIndex = -1;
        cmbAziende.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "2", "", "", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
        cmbAziende.DataBind();
        cmbAddetti.SelectedIndex = -1;
        cmbAddetti.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", "0", string.Format("%{0}%", ""), (1).ToString(), (100 + 1).ToString());
        cmbAddetti.DataBind();
    }

    protected void ASPxComboBox2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //ddCodiciTargature(null, cmbAziende.Value, cmbAddetti.Value);
        ddCodiciTargature(null, cmbAziende.Value, null);
    }

    protected void GetComboBoxFilterByIDAzienda()
    {
        if (cmbAziende.Value != null)
        {
            cmbAddetti.Text = "";
            cmbAddetti.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", cmbAziende.Value.ToString(), string.Format("%{0}%", ""), (1).ToString(), (100 + 1).ToString());
            cmbAddetti.DataBind();
        }
    }
    #endregion

    protected void ddCodiciTargature(object iDPresel, object iDSoggetto, object iDSoggettoDerived)
    {
        cmbTargature.Items.Clear();
                
        cmbTargature.ValueField = "IDTargaturaImpianto";
        cmbTargature.TextField = "CodiceTargatura";
        cmbTargature.DataSource = LoadDropDownList.LoadDropDownList_LIM_TargatureImpianti(iDPresel, iDSoggetto, iDSoggettoDerived);
        cmbTargature.DataBind();

        //ListEditItem myItem = new ListEditItem("-- Selezionare --", "0");
        //cmbTargature.Items.Insert(0, myItem);
        //cmbTargature.SelectedIndex = 0;
    }

    #region Custom Validation
    protected void ControllaDatiCatastaliCoerenza(object sender, ServerValidateEventArgs e)
    {
        //Richiesto di non controllare la coerenza dei dati catastali
        //bool fCoerente = false;
        //if ((txtFoglio.Text.Trim() != "0") && (txtMappale.Text.Trim() != "0") && (txtSubalterno.Text.Trim() != "0"))
        //{
        //    fCoerente = true;
        //}

        //if (fCoerente)
        //{
        //    e.IsValid = true;
        //}
        //else
        //{
        //    e.IsValid = false;
        //}
    }
    
    public bool VerificaDatiCatastali()
    {
        bool fEsiste = false;
        if ((RadComboBoxCodiciCatastali.Value != null) && (txtFoglio.Text != string.Empty) && (txtMappale.Text != string.Empty) && (txtSubalterno.Text != string.Empty))
        {
            object[] getVal = new object[2];
            getVal = UtilityLibrettiImpianti.CheckComuneFoglioParticellaSub(RadComboBoxCodiciCatastali.SelectedItem.Value.ToString(), txtFoglio.Text.Trim(),
                txtMappale.Text.Trim(), txtSubalterno.Text.Trim(), txtIdentificativo.Text.Trim(), string.Empty, string.Empty);
                        
            if (bool.Parse(getVal[0].ToString()))
            {
                string message = "Attenzione il dato catastale che si sta cercando di inserire è già presente in un libretto di impianto con codice targatura<br/><br/><b>" + getVal[1].ToString() + "</b><br/><br/>Confermi di inserirlo ugualmente?";
                lblDatiCatastaliMessage.Text = message;
                dmpDatiCatastali.Enabled = true;
                dmpDatiCatastali.Show();
                fEsiste = true;
            }
            else
            {
                dmpDatiCatastali.Enabled = false;
                dmpDatiCatastali.Hide();
            }
        }
        else
        {
            dmpDatiCatastali.Enabled = false;
            dmpDatiCatastali.Hide();
        }

        return fEsiste;
    }

    protected void btnSaveDatiCatastaliCancel_Click(object sender, EventArgs e)
    {
        ResetSaveDatiCatastali();
    }

    protected void ResetSaveDatiCatastali()
    {
        dmpDatiCatastali.Enabled = false;
        dmpDatiCatastali.Hide();
    }

    public void ControllaDatiCatastaliPresenti(object sender, ServerValidateEventArgs e)
    {
        //Quando aggiungo un dato catastale...

        // verifico 1. se in questa sessione di compilazione ha già inserito questi valori, nella griglia
        //2 .ora verifico tra i dati persistenti degli altri libretto di impianti

        if ((ddlSezioneDatiCatastali.SelectedIndex != -1) && (ddlSezioneDatiCatastali.SelectedIndex != 0))
        {
            //ho anche la sezione
            if (DatiCatastali.Any(c => c.IDSezione == Convert.ToInt32(ddlSezioneDatiCatastali.SelectedValue) &&
                 c.Foglio == txtFoglio.Text.Trim() &&
                                     c.Mappatura == txtMappale.Text.Trim() &&
                                     c.SubMappatura == txtSubalterno.Text.Trim()
                                     && c.Identificativo == txtIdentificativo.Text.Trim()))
            {
                cvDatiCatastaliPresenti.ErrorMessage = "Attenzione il dato castale che si sta cercando ci inserire è già presente in un altro libretto di impianto <br />";
                e.IsValid = false;
                return;
            }

            object[] getVal = new object[2];
            getVal = UtilityLibrettiImpianti.CheckComuneFoglioParticellaSub(RadComboBoxCodiciCatastali.SelectedItem.Value.ToString(), txtFoglio.Text.Trim(), txtMappale.Text.Trim(), txtSubalterno.Text.Trim(), txtIdentificativo.Text.Trim(), ddlSezioneDatiCatastali.SelectedItem.Value, string.Empty);

            e.IsValid = !bool.Parse(getVal[0].ToString());
        }
        else
        {
            if (DatiCatastali.Any(c => c.Foglio == txtFoglio.Text.Trim() &&
                                   c.Mappatura == txtMappale.Text.Trim() &&
                                   c.SubMappatura == txtSubalterno.Text.Trim()
                                   && c.Identificativo == txtIdentificativo.Text.Trim()))
            {
                cvDatiCatastaliPresenti.ErrorMessage = "Attenzione il dato castale che si sta cercando ci inserire è già presente in un altro libretto di impianto <br />";
                e.IsValid = false;
                return;
            }

            object[] getVal = new object[2];
            getVal = UtilityLibrettiImpianti.CheckComuneFoglioParticellaSub(RadComboBoxCodiciCatastali.SelectedItem.Value.ToString(), txtFoglio.Text.Trim(),
                txtMappale.Text.Trim(), txtSubalterno.Text.Trim(), txtIdentificativo.Text.Trim(), string.Empty, string.Empty);

            if (bool.Parse(getVal[0].ToString()))
            {
                cvDatiCatastaliPresenti.ErrorMessage = "Attenzione il dato catastale che si sta cercando ci inserire è già presente in un altro libretto di impianto <br />";
                e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
        }
    }

    protected void ControllaPresenzaAlmenoUnDatoCatastale(object source, ServerValidateEventArgs args)
    {
        //verifico che ce se sia almeno uno
        if (RadComboBoxCodiciCatastali.SelectedItem != null && RadComboBoxCodiciCatastali.SelectedItem.Value != null && DatiCatastali.Count == 0)
        {
            args.IsValid = false;
        }
    }

    protected void ControllaDatiCatastaliPresentiAllInserimento(object source, ServerValidateEventArgs args)
    {
        //verifico ancora una volta i dati catastali prima di salvare
        foreach (var datoCatastale in DatiCatastali)
        {
            object[] getVal = new object[2];
            getVal = UtilityLibrettiImpianti.CheckComuneFoglioParticellaSub(RadComboBoxCodiciCatastali.SelectedItem.Value.ToString(),
               datoCatastale.Foglio.Trim(), datoCatastale.Mappatura.Trim(), datoCatastale.SubMappatura.Trim(),
               datoCatastale.Identificativo.Trim(), datoCatastale.IDSezione.HasValue ? datoCatastale.IDSezione.ToString() : string.Empty, string.Empty);
                        
            if (bool.Parse(getVal[0].ToString()))
            {
                //metto messaggio di errore
                args.IsValid = false;
                return;
            }
        }
    }

    protected void ControllaGridDatiCatastali(object sender, ServerValidateEventArgs e)
    {
        int countItemDatiCatastali = DataGrid.Items.Count;

        if (countItemDatiCatastali > 0)
        {
            e.IsValid = true;
        }
        else
        {
            e.IsValid = false;
        }
    }

    protected void ControllaDatiCatastaliSigmater(object source, ServerValidateEventArgs args)
    {
        if (ConfigurationManager.AppSettings["MokaEnabled"].Equals("on"))
        {
            var Comune = ctx.SYS_CodiciCatastali.Find(Int32.Parse(lblIDCodiceCatastale.Text));
            var Sezione = ctx.SYS_CodiciCatastaliSezioni.Find(Int32.Parse(ddlSezioneDatiCatastali.SelectedValue));

            string CodiceCatastale = (Comune != null && !string.IsNullOrEmpty(Comune.CodiceCatastale)) ? Comune.CodiceCatastale : string.Empty;
            string CodiceSezione = (Sezione != null && !string.IsNullOrEmpty(Sezione.CodiceSezione)) ? Sezione.CodiceSezione : string.Empty;

            MokaUtil.MokaRequestResult result = MokaUtil.ParametriCatastaliValidi(CodiceCatastale, CodiceSezione, txtFoglio.Text, txtMappale.Text, txtSubalterno.Text);

            //La richiesta a Sigmater è stata fatta, continua se i dati sono validi.
            if (result.RequestSucceded)
            {
                args.IsValid = (result.DatiCatastaliValidi.HasValue && result.DatiCatastaliValidi.Value);
            }
            //La richiesta a Sigmater non è stata fatta (i dati catastali sono dati per validi)
            else
            {
                args.IsValid = true;
            }
        }
        else
        {
            args.IsValid = true;
        }
    }
    #endregion

    #region Visualizza dati Catastali

    public void BindGrid()
    {

        int totRecords = DatiCatastali.Count;
        DataGrid.DataSource = DatiCatastali;
        DataGrid.DataBind();
        UtilityApp.SetVisuals(DataGrid, totRecords, lblCount, "DATI CATASTALI CORRISPONDENTI AI PARAMETRI IMPOSTATI");
        lblCount.Visible = false;
    }

    public void RowCommand(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            var idDatiCatastali = Guid.Parse(e.CommandArgument.ToString());
            var datiCatastali = DatiCatastali;
            datiCatastali.Remove(datiCatastali.Where(c => c.IDDatiCatastali == idDatiCatastali).First());
            DatiCatastali = datiCatastali;

            BindGrid();
            VisibleDatiCatastali(false, lblIDCodiceCatastale.Text);
        }
    }

    #endregion

    protected void RadComboBoxCodiciCatastali_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(RadComboBoxCodiciCatastali.SelectedItem.Text))
        {
            lblIDCodiceCatastale.Text = RadComboBoxCodiciCatastali.SelectedItem.Value.ToString();
            lnkInsertDatiCatastali.Enabled = true;
        }
        else
        {
            lnkInsertDatiCatastali.Enabled = false;
        }
    }

    #region Comune
    protected void comboComune_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox) source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString(), true);
        comboBox.DataBind();
    }

    protected void comboComune_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox) source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastaliRequestedByValue(e.Value.ToString());
        comboBox.DataBind();
    }

    protected void comboComune_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshComboComune();
    }

    protected void RefreshComboComune()
    {
        RadComboBoxCodiciCatastali.SelectedIndex = -1;
        RadComboBoxCodiciCatastali.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString(), true);
        RadComboBoxCodiciCatastali.TextField = "Codice";
        RadComboBoxCodiciCatastali.ValueField = "IDCodiceCatastale";
        RadComboBoxCodiciCatastali.DataBind();
    }
    #endregion

    protected void btnConfermaInserimento_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            var libretto = new LIM_LibrettiImpianti();

            using (var ctx = new CriterDataModel())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        int idSoggetto = Convert.ToInt32(cmbAddetti.Value);
                        libretto.fAttivo = true;
                        libretto.IDSoggetto = idSoggetto;
                        libretto.IDCodiceCatastale = Int32.Parse(this.RadComboBoxCodiciCatastali.SelectedItem.Value.ToString());
                        libretto.DataInserimento = DateTime.Now;
                        libretto.DataIntervento = DateTime.Today;
                        libretto.DataUltimaModifica = DateTime.Now;
                        libretto.IDSoggettoDerived = Convert.ToInt32(cmbAziende.Value);
                        libretto.IDTargaturaImpianto = Convert.ToInt32(cmbTargature.Value);
                        libretto.IDStatoLibrettoImpianto = 1;
                        libretto.fAcs = false;
                        libretto.fClimatizzazioneInvernale = false;
                        libretto.fPannelliSolariTermici = false;
                        libretto.fPannelliSolariTermiciAltro = false;
                        libretto.IDTipologiaTermostatoZona = 1;
                        libretto.fCoibentazioneReteDistribuzione = true;

                        //dati catastali
                        foreach (var datoCatastale in DatiCatastali)
                        {
                            var datoCatastaleDb = new LIM_LibrettiImpiantiDatiCatastali();
                            datoCatastaleDb.LIM_LibrettiImpianti = libretto;
                            datoCatastaleDb.IDCodiceCatastaleSezione = datoCatastale.IDSezione;
                            datoCatastaleDb.Foglio = datoCatastale.Foglio;
                            datoCatastaleDb.Mappale = datoCatastale.Mappatura;
                            datoCatastaleDb.Subalterno = datoCatastale.SubMappatura;
                            datoCatastaleDb.Identificativo = datoCatastale.Identificativo;
                            datoCatastaleDb.fValidazioneMoka = datoCatastale.ValidazioneMoka;

                            ctx.LIM_LibrettiImpiantiDatiCatastali.Add(datoCatastaleDb);
                        }

                        ctx.LIM_LibrettiImpianti.Add(libretto);
                        ctx.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }

            if (libretto.IDLibrettoImpianto > 0)
            {
                QueryString qs = new QueryString();
                qs.Add("IDLibrettoImpianto", libretto.IDLibrettoImpianto.ToString());
                QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                string url = "LIM_LibrettiImpianti.aspx";
                url += qsEncrypted.ToString();
                Response.Redirect(url);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataUtilityCore;
using DevExpress.Web;
using EncryptionQS;
using System.Configuration;
using DataLayer;
using DevExpress.XtraExport;

public partial class LIM_LibrettiImpiantiModifica : System.Web.UI.Page
{
    #region DataContext

    private CriterDataModel _CurrentDataContext;

    public CriterDataModel CurrentDataContext
    {
        get
        {
            if (_CurrentDataContext == null)
            {
                _CurrentDataContext = DataLayer.Common.ApplicationContext.Current.Context;
            }
            return _CurrentDataContext;
        }
    }

    #endregion

    private LIM_LibrettiImpianti _LibrettoImpiantoCorrente;

    public LIM_LibrettiImpianti LibrettoImpiantoCorrente
    {
        get
        {
            if ((IDLibrettoImpianto == "") || (IDLibrettoImpianto == "0"))
                return null;

            if (_LibrettoImpiantoCorrente == null)
            {
                _LibrettoImpiantoCorrente = CurrentDataContext.LIM_LibrettiImpianti.Find(Convert.ToInt32(IDLibrettoImpianto));
            }
            return _LibrettoImpiantoCorrente;
        }
    }

    protected string IDLibrettoImpianto
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

    protected int? iDTargaturaImpianto
    {
        get
        {
            return LibrettoImpiantoCorrente.IDTargaturaImpianto;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ctlConsumoCombustibile.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlConsumoEnergiaElettrica.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlConsumoAcqua.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
        ctlConsumoProdottiChimici.SetIDLibrettoImpianto(this.IDLibrettoImpianto);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadAllDropDownlist();
            GetDatiAll(IDLibrettoImpianto);
        }
    }

    protected void LogicStatiIspezione(int? iDStatoIspezione)
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "13": //Cittadino
                tblConsumi.Visible = true;
                break;
            default:
                tblConsumi.Visible = false;
                break;
        }
    }

    #region GET DATI

    public void GetDatiAll(string iDLibrettoImpianto)
    {
        GetDatiCatastali(iDLibrettoImpianto);
        GetDatiLibrettoImpianto(iDLibrettoImpianto);
    }
    
    public void GetDatiLibrettoImpianto(string iDLibrettoImpianto)
    {
        if (LibrettoImpiantoCorrente == null)
            return;
        
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.IDSoggettoDerived.ToString()))
        {
            lblIDSoggettoDerived.Text = LibrettoImpiantoCorrente.IDSoggettoDerived.ToString();
        }
        if (LibrettoImpiantoCorrente.IDTargaturaImpianto != null)
        {
            lblIDTargaturaImpianto.Text = LibrettoImpiantoCorrente.IDTargaturaImpianto.ToString();
            imgBarcode.ImageUrl = "~/" + ConfigurationManager.AppSettings["UploadTargatureImpianti"].ToString() + "/" + LibrettoImpiantoCorrente.LIM_TargatureImpianti.CodiceTargatura + ".png";
            lblCodiceTargatura.Text = LibrettoImpiantoCorrente.LIM_TargatureImpianti.CodiceTargatura.ToString();
        }
                
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.COM_AnagraficaSoggetti1.NomeAzienda))
        {
            lblSoggettoDerived.Text = LibrettoImpiantoCorrente.COM_AnagraficaSoggetti1.NomeAzienda;
        }
        
        if (LibrettoImpiantoCorrente.IDSoggetto != null)
        {
            lblIDSoggetto.Text = LibrettoImpiantoCorrente.IDSoggetto.ToString();
            lblSoggetto.Text = LibrettoImpiantoCorrente.COM_AnagraficaSoggetti.Nome + " " + LibrettoImpiantoCorrente.COM_AnagraficaSoggetti.Cognome;
        }
        
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.IDStatoLibrettoImpianto.ToString()))
        {
            lblIDStatoLibrettoImpianto.Text = LibrettoImpiantoCorrente.IDStatoLibrettoImpianto.ToString();
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.IDStatoLibrettoImpianto.ToString()))
        {
            lblStatoLibrettoImpianto.Text = LibrettoImpiantoCorrente.SYS_StatoLibrettoImpianto.StatoLibrettoImpianto;
        }
        
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.Indirizzo))
        {
            txtIndirizzo.Text = LibrettoImpiantoCorrente.Indirizzo;
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.Civico))
        {
            txtNumeroCivico.Text = LibrettoImpiantoCorrente.Civico;
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.Palazzo))
        {
            txtPalazzo.Text = LibrettoImpiantoCorrente.Palazzo;
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.Scala))
        {
            txtScala.Text = LibrettoImpiantoCorrente.Scala;
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.Interno))
        {
            txtInterno.Text = LibrettoImpiantoCorrente.Interno;
        }

        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.IDCodiceCatastale.ToString()))
        {
            lblIDCodiceCatastale.Text = LibrettoImpiantoCorrente.IDCodiceCatastale.ToString();
        }
        
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.IDCodiceCatastale.ToString()))
        {
            lblCodiceCatastale.Text = LibrettoImpiantoCorrente.SYS_CodiciCatastali.CodiceCatastale + " - " + LibrettoImpiantoCorrente.SYS_CodiciCatastali.Comune;
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.SYS_CodiciCatastali.SYS_Province.Provincia))
        {
            lblProvincia.Text = LibrettoImpiantoCorrente.SYS_CodiciCatastali.SYS_Province.Provincia;
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.fUnitaImmobiliare.ToString()))
        {
            rblSingolaUnitaImmobiliare.SelectedValue = LibrettoImpiantoCorrente.fUnitaImmobiliare.ToString();
        }
        if (LibrettoImpiantoCorrente.IDDestinazioneUso != null)
        {
            rbDestinazioneUso(LibrettoImpiantoCorrente.IDDestinazioneUso);
            rblDestinazioneUso.SelectedValue = LibrettoImpiantoCorrente.IDDestinazioneUso.ToString();
        }
        else
        {
            rbDestinazioneUso(null);
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.VolumeLordoRiscaldato.ToString()))
        {
            txtVolumeLordoRiscaldato.Text = LibrettoImpiantoCorrente.VolumeLordoRiscaldato.ToString();
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.VolumeLordoRaffrescato.ToString()))
        {
            txtVolumeLordoRaffrescato.Text = LibrettoImpiantoCorrente.VolumeLordoRaffrescato.ToString();
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.NumeroAPE))
        {
            txtNumeroApe.Text = LibrettoImpiantoCorrente.NumeroAPE;
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.NumeroPDR))
        {
            txtNumeroPdr.Text = LibrettoImpiantoCorrente.NumeroPDR;
        }
        if (!string.IsNullOrEmpty(LibrettoImpiantoCorrente.NumeroPOD))
        {
            txtNumeroPod.Text = LibrettoImpiantoCorrente.NumeroPOD;
        }

        if (LibrettoImpiantoCorrente.IDTipologiaResponsabile.HasValue)
        {
            rblTipologiaResponsabili.SelectedValue = LibrettoImpiantoCorrente.IDTipologiaResponsabile.ToString();
        }
        if (LibrettoImpiantoCorrente.IDTipoSoggetto.HasValue)
        {
            rblTipologiaSoggetti.SelectedValue = LibrettoImpiantoCorrente.IDTipoSoggetto.ToString();
        }
        txtNomeResponsabile.Text = LibrettoImpiantoCorrente.NomeResponsabile;
        txtCognomeResponsabile.Text = LibrettoImpiantoCorrente.CognomeResponsabile;
        lblCodiceFiscaleResponsabile.Text = LibrettoImpiantoCorrente.CodiceFiscaleResponsabile;
        txtRagioneSocialeResponsabile.Text = LibrettoImpiantoCorrente.RagioneSocialeResponsabile;
        txtPartitaIvaResponsabile.Text = LibrettoImpiantoCorrente.PartitaIvaResponsabile;
        if (LibrettoImpiantoCorrente.IDComuneResponsabile.HasValue)
        {
            ASPxComboBox3.Value = LibrettoImpiantoCorrente.IDComuneResponsabile.ToString();
        }
        GetIDProvinciaByIDComune(LibrettoImpiantoCorrente.IDComuneResponsabile);
        txtIndirizzoResponsabile.Text = LibrettoImpiantoCorrente.IndirizzoResponsabile;
        txtNumeroCivicoResponsabile.Text = LibrettoImpiantoCorrente.CivicoResponsabile;

        txtEmailResponsabile.Text = LibrettoImpiantoCorrente.EmailResponsabile;
        txtEmailPecResponsabile.Text = LibrettoImpiantoCorrente.EmailPecResponsabile;

        rblfTerzoResponsabile.SelectedValue = LibrettoImpiantoCorrente.fTerzoResponsabile ? "1" : "0";

        VisibleTipologiaSoggetti();
    }
    
    #endregion

    #region SAVE DATI

    public int SaveInsertLibrettoImpianto(string iDLibrettoImpianto)
    {
        int iDLibrettoImpiantoInserted = 0;

        if ((iDLibrettoImpianto == "") || (iDLibrettoImpianto == "0"))
        {
           
        }
        else
        {
            #region Update
            LibrettoImpiantoCorrente.IDTargaturaImpianto = int.Parse(lblIDTargaturaImpianto.Text);
            LibrettoImpiantoCorrente.IDSoggetto = int.Parse(lblIDSoggetto.Text);
                     
            #region 1.2 UBICAZIONE E DESTINAZIONE DELL’EDIFICIO

            LibrettoImpiantoCorrente.Indirizzo = txtIndirizzo.Text;
            LibrettoImpiantoCorrente.Civico = txtNumeroCivico.Text;
            LibrettoImpiantoCorrente.Palazzo = txtPalazzo.Text;
            LibrettoImpiantoCorrente.Scala = txtScala.Text;
            LibrettoImpiantoCorrente.Interno = txtInterno.Text;
            if(string.IsNullOrEmpty(lblIDCodiceCatastale.Text))
            {
                LibrettoImpiantoCorrente.IDCodiceCatastale = null;
            }
            else
            {
                LibrettoImpiantoCorrente.IDCodiceCatastale = int.Parse(lblIDCodiceCatastale.Text);
            }

            LibrettoImpiantoCorrente.fUnitaImmobiliare = Convert.ToBoolean(rblSingolaUnitaImmobiliare.SelectedValue);

            if(string.IsNullOrEmpty(rblDestinazioneUso.SelectedValue))
            {
                LibrettoImpiantoCorrente.IDDestinazioneUso = null;
            }
            else
            {
                LibrettoImpiantoCorrente.IDDestinazioneUso = int.Parse(rblDestinazioneUso.SelectedValue);
            }
            if (string.IsNullOrEmpty(txtVolumeLordoRiscaldato.Text))
            {
                LibrettoImpiantoCorrente.VolumeLordoRiscaldato = null;
            }
            else
            {
                LibrettoImpiantoCorrente.VolumeLordoRiscaldato = Convert.ToDecimal(txtVolumeLordoRiscaldato.Text);
            }
            if (string.IsNullOrEmpty(txtVolumeLordoRaffrescato.Text))
            {
                LibrettoImpiantoCorrente.VolumeLordoRaffrescato = null;
            }
            else
            {
                LibrettoImpiantoCorrente.VolumeLordoRaffrescato = Convert.ToDecimal(txtVolumeLordoRaffrescato.Text);
            }
            if (string.IsNullOrEmpty(txtNumeroApe.Text))
            {
                LibrettoImpiantoCorrente.NumeroAPE = null;
            }
            else
            {
                LibrettoImpiantoCorrente.NumeroAPE = txtNumeroApe.Text;
            }

            if (string.IsNullOrEmpty(txtNumeroPdr.Text))
            {
                LibrettoImpiantoCorrente.NumeroPDR = null;
            }
            else
            {
                LibrettoImpiantoCorrente.NumeroPDR = txtNumeroPdr.Text;
            }

            if (string.IsNullOrEmpty(txtNumeroPod.Text))
            {
                LibrettoImpiantoCorrente.NumeroPOD = null;
            }
            else
            {
                LibrettoImpiantoCorrente.NumeroPOD = txtNumeroPod.Text;
            }

            #endregion
            
            #region 1.6 RESPONSABILE DELL’IMPIANTO

            if (rblTipologiaResponsabili.SelectedValue == string.Empty)
            {
                LibrettoImpiantoCorrente.IDTipologiaResponsabile = null;
            }
            else
            {
                LibrettoImpiantoCorrente.IDTipologiaResponsabile = Convert.ToInt32(rblTipologiaResponsabili.SelectedValue);
            }

            if (rblTipologiaSoggetti.SelectedValue == string.Empty)
            {
                LibrettoImpiantoCorrente.IDTipoSoggetto = null;
            }
            else
            {
                LibrettoImpiantoCorrente.IDTipoSoggetto = Convert.ToInt32(rblTipologiaSoggetti.SelectedValue);
            }

            LibrettoImpiantoCorrente.NomeResponsabile = txtNomeResponsabile.Text;
            LibrettoImpiantoCorrente.CognomeResponsabile = txtCognomeResponsabile.Text;
            LibrettoImpiantoCorrente.CodiceFiscaleResponsabile = lblCodiceFiscaleResponsabile.Text;
            LibrettoImpiantoCorrente.RagioneSocialeResponsabile = txtRagioneSocialeResponsabile.Text;
            if (txtIndirizzoResponsabile.Text != string.Empty)
            {
                LibrettoImpiantoCorrente.IndirizzoResponsabile = txtIndirizzoResponsabile.Text;
            }
            else
            {
                LibrettoImpiantoCorrente.IndirizzoResponsabile = null;
            }
            if (txtNumeroCivicoResponsabile.Text != string.Empty)
            {
                LibrettoImpiantoCorrente.CivicoResponsabile = txtNumeroCivicoResponsabile.Text;
            }
            else
            {
                LibrettoImpiantoCorrente.CivicoResponsabile = null;
            }
            if (ASPxComboBox3.Value != null)
            {
                LibrettoImpiantoCorrente.IDComuneResponsabile = int.Parse(ASPxComboBox3.Value.ToString());
            }
            else
            {
                LibrettoImpiantoCorrente.IDComuneResponsabile = null;
            }
            if (lblIDProvinciaResponsabile.Text != string.Empty)
            {
                LibrettoImpiantoCorrente.IDProvinciaResponsabile = int.Parse(lblIDProvinciaResponsabile.Text);
            }
            else
            {
                LibrettoImpiantoCorrente.IDProvinciaResponsabile = null;
            }

            LibrettoImpiantoCorrente.PartitaIvaResponsabile = txtPartitaIvaResponsabile.Text;
            LibrettoImpiantoCorrente.EmailResponsabile = txtEmailResponsabile.Text;
            LibrettoImpiantoCorrente.EmailPecResponsabile = txtEmailPecResponsabile.Text;

            LibrettoImpiantoCorrente.fTerzoResponsabile = rblfTerzoResponsabile.SelectedValue == "1";

            #endregion
            
            try
            {
                CurrentDataContext.SaveChanges();
            }
            catch (Exception ex)
            {
               
            }

            iDLibrettoImpiantoInserted = LibrettoImpiantoCorrente.IDLibrettoImpianto;

            #endregion
        }

        return iDLibrettoImpiantoInserted;
    }
    
    #endregion
                
    #region DROPDOWNLIST / RADIOBUTTONLIST / CHECKBOXLIST
    
    protected void LoadAllDropDownlist()
    {
        rbDestinazioneUso(null);
        
        rbTipologiaResponsabili(null);
        rbTipologiaSoggetti(null);
    }
    
    protected void rbDestinazioneUso(int? iDPresel)
    {
        rblDestinazioneUso.Items.Clear();
                
        rblDestinazioneUso.DataValueField = "IDDestinazioneUso";
        rblDestinazioneUso.DataTextField = "DestinazioneUso";
        rblDestinazioneUso.DataSource = LoadDropDownList.LoadDropDownList_SYS_DestinazioneUso(iDPresel);
        rblDestinazioneUso.DataBind();
        
        rblDestinazioneUso.SelectedIndex = 0;
    }
    
    protected void rbTipologiaResponsabili(int? idPresel)
    {
        rblTipologiaResponsabili.Items.Clear();
        rblTipologiaResponsabili.DataValueField = "IDTipologiaResponsabile";
        rblTipologiaResponsabili.DataTextField = "TipologiaResponsabile";
        rblTipologiaResponsabili.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaResponsabile(idPresel);
        rblTipologiaResponsabili.DataBind();
        
        rblTipologiaResponsabili.SelectedIndex = 0;
    }

    protected void rbTipologiaSoggetti(int? idPresel)
    {
        rblTipologiaSoggetti.Items.Clear();
        rblTipologiaSoggetti.DataValueField = "IDTipoSoggetto";
        rblTipologiaSoggetti.DataTextField = "TipoSoggetto";
        rblTipologiaSoggetti.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipoSoggetto(idPresel, true);
        rblTipologiaSoggetti.DataBind();
        
        rblTipologiaSoggetti.SelectedIndex = 0;
    }
    
    #endregion

    #region DATI CATASTALI
    public void GetDatiCatastali(string iDLibrettoImpianto)
    {
        dgDatiCatastali.DataSource = UtilityLibrettiImpianti.GetValoriDatiCatastali(int.Parse(iDLibrettoImpianto));
        dgDatiCatastali.DataBind();
        if (dgDatiCatastali.Items.Count > 0)
        {
            pnlDatiCatastaliView.Visible = true;
        }
        else
        {
            pnlDatiCatastaliView.Visible = false;
        }
    }
    
    public void RowCommand(Object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                int iDLibrettoImpiantoDatiCatastaliInt = int.Parse(e.CommandArgument.ToString());
                var datiCatastali = new LIM_LibrettiImpiantiDatiCatastali();
                datiCatastali = ctx.LIM_LibrettiImpiantiDatiCatastali.FirstOrDefault(c => c.IDLibrettoImpiantoDatiCatastali == iDLibrettoImpiantoDatiCatastaliInt);

                ctx.LIM_LibrettiImpiantiDatiCatastali.Remove(datiCatastali);
                ctx.SaveChanges();
            }
            
            GetDatiCatastali(IDLibrettoImpianto);
        }
        else if (e.CommandName == "Modify")
        {
            VisibleDatiCatastali(false, null);
            using (CriterDataModel ctx = new CriterDataModel())
            {
                int IDLibrettoImpiantoDatiCatastaliInt = int.Parse(e.CommandArgument.ToString());
                var datiCatastali = new LIM_LibrettiImpiantiDatiCatastali();
                datiCatastali = ctx.LIM_LibrettiImpiantiDatiCatastali.FirstOrDefault(c => c.IDLibrettoImpiantoDatiCatastali == IDLibrettoImpiantoDatiCatastaliInt);

                lblIDLibrettoImpiantoDatiCatastali.Text = IDLibrettoImpiantoDatiCatastaliInt.ToString();
                if (datiCatastali.IDCodiceCatastaleSezione != null)
                {
                    ddlSezioneDatiCatastali.SelectedItem.Value = datiCatastali.IDCodiceCatastaleSezione.ToString();
                }
                if (!string.IsNullOrEmpty(datiCatastali.Foglio))
                {
                    txtFoglio.Text = datiCatastali.Foglio;
                }
                if (!string.IsNullOrEmpty(datiCatastali.Mappale))
                {
                    txtMappale.Text = datiCatastali.Mappale;
                }
                if (!string.IsNullOrEmpty(datiCatastali.Subalterno))
                {
                    txtSubalterno.Text = datiCatastali.Subalterno;
                }
                if (!string.IsNullOrEmpty(datiCatastali.Identificativo))
                {
                    txtIdentificativo.Text = datiCatastali.Identificativo;
                }
            }
            
            VisibleDatiCatastali(true, lblIDCodiceCatastale.Text);
        }
    }

    protected void VisibleDatiCatastali(bool fVisible, object IDCodiceCastale)
    {
        if (fVisible)
        {
            pnlDatiCatastaliInsert.Visible = true;
            lnkInsertDatiCatastali.Visible = false;
            pnlDatiCatastaliView.Visible = false;
        }
        else
        {
            pnlDatiCatastaliInsert.Visible = false;
            lnkInsertDatiCatastali.Visible = true;
            pnlDatiCatastaliView.Visible = true;
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

    protected void ResetDatiCatastali()
    {
        ddlSezioneDatiCatastali.SelectedIndex = -1;
        txtFoglio.Text = "";
        txtMappale.Text = "";
        txtSubalterno.Text = "";
        txtIdentificativo.Text = "";
        lblIDLibrettoImpiantoDatiCatastali.Text = "";
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
    
    protected void btnAnnullaCodiceCatastale_Click(object sender, EventArgs e)
    {
        ResetDatiCatastali();
        VisibleDatiCatastali(false, lblIDCodiceCatastale.Text);
    }

    protected void SaveDatiCatastali(string iDLibrettoImpiantoDatiCatastali)
    {
        if (string.IsNullOrEmpty(iDLibrettoImpiantoDatiCatastali))
        {
            using (var ctx = DataLayer.Common.ApplicationContext.Current.Context)
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var datiCatastali = new LIM_LibrettiImpiantiDatiCatastali();
                        datiCatastali.IDLibrettoImpianto = int.Parse(IDLibrettoImpianto);
                        if (ddlSezioneDatiCatastali.SelectedIndex != 0)
                        {
                            datiCatastali.IDCodiceCatastaleSezione = int.Parse(ddlSezioneDatiCatastali.SelectedItem.Value);
                        }
                        else
                        {
                            datiCatastali.IDCodiceCatastaleSezione = null;
                        }
                        if (!string.IsNullOrEmpty(txtFoglio.Text))
                        {
                            datiCatastali.Foglio = txtFoglio.Text;
                        }
                        if (!string.IsNullOrEmpty(txtMappale.Text))
                        {
                            datiCatastali.Mappale = txtMappale.Text;
                        }
                        if (!string.IsNullOrEmpty(txtSubalterno.Text))
                        {
                            datiCatastali.Subalterno = txtSubalterno.Text;
                        }
                        if (!string.IsNullOrEmpty(txtIdentificativo.Text))
                        {
                            datiCatastali.Identificativo = txtIdentificativo.Text;
                        }
                        
                        ctx.LIM_LibrettiImpiantiDatiCatastali.Add(datiCatastali);
                        ctx.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
        }
        else
        {
            using (var ctx = DataLayer.Common.ApplicationContext.Current.Context)
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        int iDLibrettoImpiantoDatiCatastaliInt = int.Parse(iDLibrettoImpiantoDatiCatastali);
                        var datiCatastali = new LIM_LibrettiImpiantiDatiCatastali();
                        datiCatastali = ctx.LIM_LibrettiImpiantiDatiCatastali.FirstOrDefault(c => c.IDLibrettoImpiantoDatiCatastali == iDLibrettoImpiantoDatiCatastaliInt);
                        datiCatastali.IDLibrettoImpianto = int.Parse(IDLibrettoImpianto);
                        if ((ddlSezioneDatiCatastali.SelectedIndex != 0) && (ddlSezioneDatiCatastali.SelectedIndex != -1))
                        {
                            datiCatastali.IDCodiceCatastaleSezione = int.Parse(ddlSezioneDatiCatastali.SelectedItem.Value);
                        }
                        else
                        {
                            datiCatastali.IDCodiceCatastaleSezione = null;
                        }
                        if (!string.IsNullOrEmpty(txtFoglio.Text))
                        {
                            datiCatastali.Foglio = txtFoglio.Text;
                        }
                        if (!string.IsNullOrEmpty(txtMappale.Text))
                        {
                            datiCatastali.Mappale = txtMappale.Text;
                        }
                        if (!string.IsNullOrEmpty(txtSubalterno.Text))
                        {
                            datiCatastali.Subalterno = txtSubalterno.Text;
                        }
                        if (!string.IsNullOrEmpty(txtIdentificativo.Text))
                        {
                            datiCatastali.Identificativo = txtIdentificativo.Text;
                        }
                                                
                        ctx.SaveChanges();

                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
        }
    }

    protected void btnSaveDatiCatastali_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            SaveDatiCatastali(lblIDLibrettoImpiantoDatiCatastali.Text);
            ResetDatiCatastali();
            VisibleDatiCatastali(false, lblIDCodiceCatastale.Text);
            GetDatiCatastali(IDLibrettoImpianto);
        }
    }

    protected void btnAnnullaDatiCatastali_Click(object sender, EventArgs e)
    {
        VisibleDatiCatastali(false, lblIDCodiceCatastale.Text);
    }

    protected void ControllaDatiCatastaliPresenti(object sender, ServerValidateEventArgs e)
    {
        if ((lblIDCodiceCatastale.Text != null) && (txtFoglio.Text != string.Empty) && (txtMappale.Text != string.Empty) && (txtSubalterno.Text != string.Empty))
        {
            object[] getVal = new object[2];
            getVal = UtilityLibrettiImpianti.CheckComuneFoglioParticellaSub(lblIDCodiceCatastale.Text, txtFoglio.Text.Trim(),
                txtMappale.Text.Trim(), txtSubalterno.Text.Trim(), txtIdentificativo.Text.Trim(), string.Empty, IDLibrettoImpianto);

            if (bool.Parse(getVal[0].ToString()))
            {
                e.IsValid = false;
                cvDatiCatastaliPresenti.ErrorMessage = "Attenzione il dato catastale che si sta cercando di inserire è già presente in un libretto di impianto!";
            }
            else
            {
                e.IsValid = true;
            }           
        }        
    }

    #endregion

    public void ControllaPodPdr(Object sender, ServerValidateEventArgs e)
    {
        bool fPod = true;
        bool fPdr = true;
        string message = "";
        var result = UtilityLibrettiImpianti.GetValoriLibrettoImpiantoTipologiaGeneratori(int.Parse(IDLibrettoImpianto));

        foreach (var row in result)
        {
            if (((row.IDTipologiaGeneratori.ToString() == "3") || (row.IDTipologiaGeneratori.ToString() == "4")) && (string.IsNullOrEmpty(txtNumeroPod.Text)))
            {
                fPod = false;
            }
            else if (((row.IDTipologiaGeneratori.ToString() == "1") || (row.IDTipologiaGeneratori.ToString() == "2") || (row.IDTipologiaGeneratori.ToString() == "7")) && (string.IsNullOrEmpty(txtNumeroPdr.Text)))
            {
                fPdr = false;
            }
            else if ((row.IDTipologiaGeneratori.ToString() == "5") || (row.IDTipologiaGeneratori.ToString() == "6"))
            {

            }
        }
        
        if (fPod && fPdr)
        {
            e.IsValid = true;
        }
        else
        {
            e.IsValid = false;
            if (!fPod)
            {
                message += "<br/>Numero POD: campo obbligatorio in presenza di generatori Pompa di calore e Macchine Frigorifere";
            }
            if (!fPdr)
            {
                message += "<br/>Numero PDR: campo obbligatorio in presenza di generatori Generatore a combustione, Cogenerazione / trigenerazione";
            }

            cvPodPdr.ErrorMessage = message;
        }
    }

    public void RedirectPage(string IDLibrettoImpianto)
    {
        QueryString qs = new QueryString();
        qs.Add("IDLibrettoImpianto", IDLibrettoImpianto);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "~/LIM_LibrettiImpianti.aspx";
        url += qsEncrypted.ToString();
        Response.Redirect(url, true);
    }

    protected void LIM_LibrettiImpianti_btnSavePreliminary_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int iDLibrettoImpiantoInserted = SaveInsertLibrettoImpianto(IDLibrettoImpianto);
            RedirectPage(iDLibrettoImpiantoInserted.ToString());
        }
    }
    
    #region 1.6 RESPONSABILE DELL’IMPIANTO

    protected void VisibleTipologiaSoggetti()
    {
        switch (rblTipologiaSoggetti.SelectedValue)
        {
            case "1": //Persona fisica
                rowCodiceFiscaleResponsabile.Visible = true;
                rowRagioneSocialeResponsabile.Visible = false;
                rowPartitaIvaResponsabile.Visible = false;

                LIM_LibrettiImpianti_lblTitoloNomeResponsabile.Text = "Nome (*)";
                LIM_LibrettiImpianti_lblTitoloCognomeResponsabile.Text = "Cognome (*)";

                txtRagioneSocialeResponsabile.Text = "";
                txtPartitaIvaResponsabile.Text = "";
                break;
            case "2": //Persona giuridica
                rowCodiceFiscaleResponsabile.Visible = false;
                rowRagioneSocialeResponsabile.Visible = true;
                rowPartitaIvaResponsabile.Visible = true;

                LIM_LibrettiImpianti_lblTitoloNomeResponsabile.Text = "Nome legale rappresentante (*)";
                LIM_LibrettiImpianti_lblTitoloCognomeResponsabile.Text = "Cognome legale rappresentante (*)";

                lblCodiceFiscaleResponsabile.Text = "";
                break;
        }
    }

    protected void rblTipologiaSoggetti_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTipologiaSoggetti();
    }
    
    protected void ASPxComboBox3_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox) source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString(), false);
        comboBox.DataBind();
    }

    protected void ASPxComboBox3_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox) source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastaliRequestedByValue(e.Value.ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox3_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetIDProvinciaByIDComune(ASPxComboBox3.Value);
    }

    protected void GetIDProvinciaByIDComune(object iDComune)
    {
        int iDComuneInt = 0;
        lblProvinciaResponsabile.Text = "";
        lblIDProvinciaResponsabile.Text = "";
        if (iDComune != null)
        {
            if (int.TryParse(iDComune.ToString(), out iDComuneInt))
            {
                var provincia = LoadDropDownList.LoadDropDownList_V_SYS_CodiciCatastali(iDComuneInt).FirstOrDefault();
                lblProvinciaResponsabile.Text = provincia.Provincia;
                lblIDProvinciaResponsabile.Text = provincia.IDProvincia.ToString();
            }
        }
    }

    protected void ASPxComboBox3_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshComboComune();
        RefreshProvincia();
    }

    protected void RefreshComboComune()
    {
        ASPxComboBox3.SelectedIndex = -1;
        ASPxComboBox3.DataSource = LoadDropDownList.ASPxComboBox_SYS_CodiciCatastali("", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString(), true);
        ASPxComboBox3.TextField = "Codice";
        ASPxComboBox3.ValueField = "IDCodiceCatastale";
        ASPxComboBox3.DataBind();
    }

    protected void RefreshProvincia()
    {
        lblProvinciaResponsabile.Text = "";
        lblIDProvinciaResponsabile.Text = "";
    }

    #endregion
    
    protected void LIM_LibrettiImpianti_btnSavePartialLibrettoImpianto_Click(object sender, EventArgs e)
    {
        SaveInsertLibrettoImpianto(IDLibrettoImpianto);
        
        Logger.LogUser(TipoEvento.ModificaDati, UtilityApp.GetCurrentPageName(), HttpContext.Current.User.Identity.Name, HttpContext.Current.Request.UserHostAddress);
    }

    protected void LIM_LibrettiImpianti_btnAnnullaLibrettoImpianto_Click(object sender, EventArgs e)
    {
        RedirectPage(IDLibrettoImpianto);
    }
    
    protected void LIM_LibrettiImpianti_btnCloseLibrettoImpianto_Click(object sender, EventArgs e)
    {
        Page.Validate("vgLibrettoImpianto");
        if (Page.IsValid)
        {
            SaveInsertLibrettoImpianto(IDLibrettoImpianto);
            
            LibrettoImpiantoCorrente.IDStatoLibrettoImpianto = 2;
            CurrentDataContext.SaveChanges();
            
            RedirectPage(IDLibrettoImpianto);
        }
    }
    

}
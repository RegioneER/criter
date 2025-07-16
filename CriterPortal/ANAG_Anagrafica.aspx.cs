using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataUtilityCore;
using DevExpress.Web;
using EncryptionQS;
using DataLayer;
using System.IO;

public partial class ANAG_Anagrafica : Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "ANAG_Anagrafica.aspx");
        if (!Page.IsPostBack)
        {
            if (string.IsNullOrEmpty(IDSoggetto) && (string.IsNullOrEmpty(IDTipoSoggetto)))
            {
                tblInfoAzienda.Visible = false;
                tblInfoManutentore.Visible = false;
                tblInfoSoftwareHouse.Visible = false;
                tblInfoIspettore.Visible = false;
                tblInfoDistributoriCombustibile.Visible = false;
                tblInfoEnteLocale.Visible = false;
                //Caso in cui recupero i valori dall'account
                UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
                RedirectPage(info.IDSoggetto.ToString(), info.IDTipoSoggetto.ToString(), false);
            }
            else
            {
                VisibleHiddenTipoSoggetto();
            }
        }
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                if ((IDSoggetto == "") || (IDSoggetto == "0"))
                {
                    ASPxComboBox1.Visible = true;
                    lblSoggetto.Visible = false;
                }
                else
                {
                    string iDAzienda = SecurityManager.GetUserIDAziendaDelSoggetto(IDSoggetto);
                    if (!string.IsNullOrEmpty(iDAzienda))
                    {
                        ASPxComboBox1.Visible = false;
                        lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(iDAzienda);
                        lblSoggetto.Visible = true;
                    }
                }
                imgImportDatiAzienda.Visible = false;
                btnAnnullaAzienda.Visible = true;
                btnAnnullaManutentore.Visible = true;
                btnAnnullaSoftwareHouse.Visible = true;
                btnAnnullaDistributoriCombustibile.Visible = true;
                break;
            case "2": //Amministratore azienda
                ASPxComboBox1.Value = getVal[0].ToString();
                ASPxComboBox1.Visible = false;
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0].ToString());
                lblSoggetto.Visible = true;
                btnAnnullaAzienda.Visible = false;
                btnAnnullaManutentore.Visible = true;
                break;
            case "3": //Operatore/Addetto
                ASPxComboBox1.Visible = false;
                lblSoggetto.Visible = true;
                ASPxComboBox1.Value = getVal[2].ToString();
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                btnAnnullaAzienda.Visible = false;
                btnAnnullaManutentore.Visible = false;
                btnAnnullaDistributoriCombustibile.Visible = false;
                break;
            case "10": //Responsabile tecnico
                ASPxComboBox1.Value = getVal[2].ToString();
                ASPxComboBox1.Visible = false;
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                lblSoggetto.Visible = true;
                btnAnnullaAzienda.Visible = false;
                btnAnnullaManutentore.Visible = false;
                btnAnnullaDistributoriCombustibile.Visible = false;
                break;
            case "16": //Enti locali
                gridAreaTerritorialeEnteLocale.Enabled = false;//.Columns[3].Visible = false;
                break;

                
        }
    }

    public void VisibleHiddenAccreditamentoImpresa()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        if (getVal[1] == "1") //Amministratore Criter
        {
            rowAccreditamentoImpresa.Visible = true;
        }
    }

    public void VisibleHiddenTipoSoggetto()
    {
        switch (IDTipoSoggetto)
        {
            case "1": //Persona
                ASPxComboBox1.Focus();
                tblInfoAzienda.Visible = false;
                tblInfoManutentore.Visible = true;
                tblInfoSoftwareHouse.Visible = false;
                tblInfoIspettore.Visible = false;
                tblInfoDistributoriCombustibile.Visible = false;
                tblInfoCittadino.Visible = false;
                tblInfoEnteLocale.Visible = false;

                PagePermission();
                ddPaeseSoggetto(null, ddlPaeseManutentore);
                ddProvincia(null, ddlProvinciaManutentore);

                if ((IDSoggetto == "") || (IDSoggetto == "0"))
                {
                    InfoDefaultManutentore();
                    lblTitoloGestioneOperatoreAddetto.Text = "NUOVA ANAGRAFICA OPERATORE/ADDETTO";
                }
                else
                {
                    lblTitoloGestioneOperatoreAddetto.Text = "MODIFICA ANAGRAFICA OPERATORE/ADDETTO";
                    int iDSoggetto = int.Parse(IDSoggetto);
                    int iDTipoSoggetto = int.Parse(IDTipoSoggetto);
                    GetDatiUser(iDSoggetto, iDTipoSoggetto);
                }

                VisibleHiddenFieldCodiceSoggetto();
                VisibleHiddenFieldPaeseManutentore();
                VisibleHiddenFieldAttivazioneUtenza();
                SetRequiredEmailManutentore(chkAttivazioneUtenzaManutentore.Checked);
                break;
            case "2": //Azienda
                txtAzienda.Focus();
                tblInfoAzienda.Visible = true;
                tblInfoManutentore.Visible = false;
                tblInfoSoftwareHouse.Visible = false;
                tblInfoIspettore.Visible = false;
                tblInfoDistributoriCombustibile.Visible = false;
                tblInfoCittadino.Visible = false;
                tblInfoEnteLocale.Visible = false;

                ddFormaGiuridica(null, ddlFormaGiuridica, int.Parse(IDTipoSoggetto));
                ddPaeseSoggetto(null, ddlPaeseSedeLegale);
                ddPaeseSoggetto(null, ddlPaeseNascita);
                ddPaeseSoggetto(null, ddlPaeseResidenza);
                ddProvincia(null, ddlProvinciaSedeLegale);
                ddProvincia(null, ddlProvinciaNascita);
                ddProvincia(null, ddlProvinciaResidenza);
                ddTitoloSoggetto(null, ddlTitoloLegaleRappresentante);
                ddFunzioneSoggetto(null, ddlFunzioneLegaleRappresentante, int.Parse(IDTipoSoggetto));
                ddProvincia(null, ddlProvinciaAlboImprese);
                cbProvinceCompetenza(null);
                cbRuoliSoggetto();
                cbClassificazioniImpianto();
                cbAbilitazioniSoggetti(null);

                if ((IDSoggetto == "") || (IDSoggetto == "0"))
                {
                    InfoDefaultAzienda();
                    lblTitoloGestioneAzienda.Text = "NUOVA ANAGRAFICA AZIENDA";
                }
                else
                {
                    lblTitoloGestioneAzienda.Text = "MODIFICA ANAGRAFICA AZIENDA";
                    int iDSoggetto = int.Parse(IDSoggetto);
                    int iDTipoSoggetto = int.Parse(IDTipoSoggetto);
                    GetDatiUser(iDSoggetto, iDTipoSoggetto);
                    GetDatiUserRuoli(iDSoggetto);
                    GetDatiUserClassificazioniImpianto(iDSoggetto);
                    GetDatiUserProvinceCompetenza(iDSoggetto);
                    GetDatiUserAbilitazioniSoggetto(iDSoggetto);
                }

                VisibleHiddenFieldCodiceSoggetto();
                VisibleHiddenFieldPaeseSedeLegale();
                VisibleHiddenFieldPaeseNascita();
                VisibleHiddenFieldPaeseResidenza();
                VisibleHiddenAccreditamentoImpresa();
                break;
            case "4": //Persona Responsabile tecnico
                ASPxComboBox1.Focus();
                tblInfoAzienda.Visible = false;
                tblInfoManutentore.Visible = true;
                tblInfoSoftwareHouse.Visible = false;
                tblInfoIspettore.Visible = false;
                tblInfoDistributoriCombustibile.Visible = false;
                tblInfoCittadino.Visible = false;
                tblInfoEnteLocale.Visible = false;

                PagePermission();
                ddPaeseSoggetto(null, ddlPaeseManutentore);
                ddProvincia(null, ddlProvinciaManutentore);

                if ((IDSoggetto == "") || (IDSoggetto == "0"))
                {
                    InfoDefaultManutentore();
                    lblTitoloGestioneOperatoreAddetto.Text = "NUOVA ANAGRAFICA RESPONSABILE TECNICO";
                }
                else
                {
                    lblTitoloGestioneOperatoreAddetto.Text = "MODIFICA ANAGRAFICA RESPONSABILE TECNICO";
                    int iDSoggetto = int.Parse(IDSoggetto);
                    int iDTipoSoggetto = int.Parse(IDTipoSoggetto);
                    GetDatiUser(iDSoggetto, iDTipoSoggetto);
                }

                VisibleHiddenFieldCodiceSoggetto();
                VisibleHiddenFieldPaeseManutentore();
                VisibleHiddenFieldAttivazioneUtenza();
                SetRequiredEmailManutentore(chkAttivazioneUtenzaManutentore.Checked);
                break;
            case "5": //Distributore di combustibile
                txtDistributoriCombustibile.Focus();
                tblInfoAzienda.Visible = false;
                tblInfoManutentore.Visible = false;
                tblInfoSoftwareHouse.Visible = false;
                tblInfoIspettore.Visible = false;
                tblInfoDistributoriCombustibile.Visible = true;
                tblInfoCittadino.Visible = false;
                tblInfoEnteLocale.Visible = false;

                ddFormaGiuridica(null, ddlFormaGiuridicaDistributoriCombustibile, int.Parse(IDTipoSoggetto));
                ddPaeseSoggetto(null, ddlPaeseSedeLegaleDistributoriCombustibile);
                ddPaeseSoggetto(null, ddlPaeseNascitaDistributoriCombustibile);
                ddPaeseSoggetto(null, ddlPaeseResidenzaDistributoriCombustibile);
                ddProvincia(null, ddlProvinciaSedeLegaleDistributoriCombustibile);
                ddProvincia(null, ddlProvinciaNascitaDistributoriCombustibile);
                ddProvincia(null, ddlProvinciaResidenzaDistributoriCombustibile);
                ddProvincia(null, ddlProvinciaAlboImpreseDistributoriCombustibile);
                cbTipologiaDistributoriCombustibile();

                if ((IDSoggetto == "") || (IDSoggetto == "0"))
                {
                    InfoDefaultDistributoriCombustibile();
                    lblTitoloGestioneDistributoriCombustibile.Text = "NUOVO DISTRIBUTORE DI COMBUSTIBILE";
                }
                else
                {
                    lblTitoloGestioneDistributoriCombustibile.Text = "MODIFICA DISTRIBUTORE DI COMBUSTIBILE";
                    int iDSoggetto = int.Parse(IDSoggetto);
                    int iDTipoSoggetto = int.Parse(IDTipoSoggetto);
                    GetDatiUser(iDSoggetto, iDTipoSoggetto);
                    GetDatiUserTipiDistributoriCombustibile(iDSoggetto);
                }

                VisibleHiddenFieldPaeseSedeLegaleDistributoriCombustibile();
                VisibleHiddenFieldPaeseNascitaDistributoriCombustibile();
                VisibleHiddenFieldPaeseResidenzaDistributoriCombustibile();
                break;
            case "6": //Software house
                txtAzienda.Focus();
                tblInfoAzienda.Visible = false;
                tblInfoManutentore.Visible = false;
                tblInfoSoftwareHouse.Visible = true;
                tblInfoIspettore.Visible = false;
                tblInfoDistributoriCombustibile.Visible = false;
                tblInfoCittadino.Visible = false;
                tblInfoEnteLocale.Visible = false;

                ddFormaGiuridica(null, ddlFormaGiuridicaSoftwareHouse, int.Parse(IDTipoSoggetto));
                ddPaeseSoggetto(null, ddlPaeseSedeLegaleSoftwareHouse);
                ddProvincia(null, ddlProvinciaSedeLegaleSoftwareHouse);

                if ((IDSoggetto == "") || (IDSoggetto == "0"))
                {
                    InfoDefaultSoftwareHouse();
                    lblTitoloGestioneSoftwareHouse.Text = "NUOVA SOFTWARE HOUSE";
                }
                else
                {
                    lblTitoloGestioneSoftwareHouse.Text = "MODIFICA SOFTWARE HOUSE";
                    int iDSoggetto = int.Parse(IDSoggetto);
                    int iDTipoSoggetto = int.Parse(IDTipoSoggetto);
                    GetDatiUser(iDSoggetto, iDTipoSoggetto);
                }
                VisibleHiddenFieldPaeseSoftwareHouse();
                break;
            case "7": //Ispettore
                txtNomeIspettore.Focus();
                tblInfoAzienda.Visible = false;
                tblInfoManutentore.Visible = false;
                tblInfoSoftwareHouse.Visible = false;
                tblInfoIspettore.Visible = true;
                tblInfoDistributoriCombustibile.Visible = false;
                tblInfoCittadino.Visible = false;
                tblInfoEnteLocale.Visible = false;

                ddTitoloSoggetto(null, ddlTitoloIspettore);
                ddPaeseSoggetto(null, ddlPaeseNascitaIspettore);
                ddPaeseSoggetto(null, ddlPaeseResidenzaIspettore);
                ddPaeseSoggetto(null, ddlPaeseDomicilioIspettore);
                ddProvincia(null, ddlProvinciaNascitaIspettore);
                ddProvincia(null, ddlProvinciaResidenzaIspettore);
                ddProvincia(null, ddlProvinciaDomicilioIspettore);
                ddProvincia(null, ddlOrdineCollegioProvincia);
                rbQualificheIspettori(null);
                rbTipologiaOrdineCollegio(null);
                ddTipologiaTitoloStudio(null);
                ddPaeseSoggetto(null, ddlPaeseOrganizzazioneIspettore);
                ddProvincia(null, ddlProvinciaOrganizzazioneIspettore);

                if ((IDSoggetto == "") || (IDSoggetto == "0"))
                {
                    InfoDefaultIspettore();
                    lblTitoloGestioneIspettore.Text = "NUOVO ISPETTORE";
                }
                else
                {
                    lblTitoloGestioneIspettore.Text = "MODIFICA DATI ISPETTORE";
                    int iDSoggetto = int.Parse(IDSoggetto);
                    int iDTipoSoggetto = int.Parse(IDTipoSoggetto);
                    GetDatiUser(iDSoggetto, iDTipoSoggetto);
                }

                VisibleHiddenFieldPaeseOrganizzazioneIspettore();
                VisibleHiddenFieldPaeseNascitaIspettore();
                VisibleHiddenFieldPaeseResidenzaIspettore();
                VisibleHiddenFieldPaeseDomicilioIspettore();
                SetVisibleHiddenDomicilio(chkDomicilio.Checked);
                SetVisibleHiddenTipoQualificaIspettore(rblTipoQualificaIspettore.SelectedValue);
                SetVisibleHiddenIscrizioneRegistroGasFluorurati(chkIscrizioneRegistroGasFluoruratiIspettore.Checked);
                SetVisibleHiddenOrganizzazioneIspettore(Convert.ToBoolean(Convert.ToInt16(rblOrganizzazioneIspettore.SelectedItem.Value)));
                SetVisibleHiddenIdoneoIspezione(chkIdoneoIspezione.Checked);
                VisibleHiddenFieldCodiceSoggetto();
                SetVisibleHiddenTipologiaTitoloStudio(ddlTipologiaTitoloStudio.SelectedItem.Value, rblTipoInserimentoAziendaOrdineCollegio.SelectedItem.Value);
                break;
            case "8": //Cittadino
                tblInfoAzienda.Visible = false;
                tblInfoManutentore.Visible = false;
                tblInfoSoftwareHouse.Visible = false;
                tblInfoIspettore.Visible = false;
                tblInfoDistributoriCombustibile.Visible = false;
                tblInfoCittadino.Visible = true;
                tblInfoEnteLocale.Visible = false;

                lblTitoloGestioneCittadino.Text = "ANAGRAFICA CITTADINO";
                int iDSoggettoCittadino = int.Parse(IDSoggetto);
                int iDTipoSoggettoCittadino = int.Parse(IDTipoSoggetto);
                GetDatiUser(iDSoggettoCittadino, iDTipoSoggettoCittadino);
                break;

            case "9": //Enti locali
                txtEnteLocale.Focus();
                tblInfoAzienda.Visible = false;
                tblInfoManutentore.Visible = false;
                tblInfoSoftwareHouse.Visible = false;
                tblInfoIspettore.Visible = false;
                tblInfoDistributoriCombustibile.Visible = false;
                tblInfoCittadino.Visible = false;
                tblInfoEnteLocale.Visible = true;

                ddFormaGiuridica(null, ddlFormaGiuridicaEnteLocale, int.Parse(IDTipoSoggetto));
                ddFunzioneSoggetto(null, ddlFunzioneLegaleRappresentanteEnteLocale, int.Parse(IDTipoSoggetto));
                ddProvincia(null, ddlProvinciaSedeLegaleEnteLocale);
                ddProvincia(null, ddlProvinciaNascitaEnteLocale);
                ddProvincia(null, ddlProvinciaResidenzaLegaleRappresentanteEnteLocale);
                ddPaeseSoggetto(null, ddlPaeseNascitaEnteLocale);
                ddPaeseSoggetto(null, ddlPaeseResidenzaLegaleRappresentanteEnteLocale);

                if ((IDSoggetto == "") || (IDSoggetto == "0"))
                {
                    InfoDefaultAzienda();
                    lblTitoloGestioneEnteLocale.Text = "NUOVA ANAGRAFICA ENTE LOCALE";
                }
                else
                {
                    lblTitoloGestioneEnteLocale.Text = "MODIFICA ANAGRAFICA ENTE LOCALE";
                    int iDSoggetto = int.Parse(IDSoggetto);
                    int iDTipoSoggetto = int.Parse(IDTipoSoggetto);
                    GetDatiUser(iDSoggetto, iDTipoSoggetto);
                    GetDatiUserComuniCompetenza(iDSoggetto);
                    PagePermission();
                }

                VisibleHiddenFieldPaeseNascitaEnteLocale();
                VisibleHiddenFieldPaeseResidenzaEnteLocale();
                break;
        }
    }

    public void VisibleHiddenFieldAttivazioneUtenza()
    {
        UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
        if (info.IDSoggetto.ToString() == IDSoggetto)
        {
            rowAttivazioneUtenzaManutentore.Visible = false;
        }
        else
        {
            int? iDSoggetto = UtilityApp.ParseNullableInt(IDSoggetto);
            bool fexist = SecurityManager.CheckSoggettoWithUtenza(iDSoggetto);
            if (fexist)
            {
                rowAttivazioneUtenzaManutentore.Visible = false;
            }
            else
            {
                rowAttivazioneUtenzaManutentore.Visible = true;
            }
        }
    }

    public void SetRequiredEmailManutentore(bool fAttivazioneUtenza)
    {
        if (fAttivazioneUtenza)
        {
            lblEmailManutentore.Text = "Email (*)";
            txtEmailManutentore.CssClass = "txtClass_o";
            rfvtxtEmailManutentore.Enabled = true;
            cvEmailPresenteManutentore.Enabled = true;
        }
        else
        {
            int? iDSoggetto = UtilityApp.ParseNullableInt(IDSoggetto);
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var utenza = ctx.COM_Utenti.Where(c => c.IDSoggetto == iDSoggetto).ToList();

                if (utenza.Count == 0)
                {
                    lblEmailManutentore.Text = "Email";
                    txtEmailManutentore.CssClass = "txtClass";
                    rfvtxtEmailManutentore.Enabled = false;
                    cvEmailPresenteManutentore.Enabled = false;
                }
                else
                {
                    lblEmailManutentore.Text = "Email (*)";
                    txtEmailManutentore.CssClass = "txtClass_o";
                    rfvtxtEmailManutentore.Enabled = true;
                    cvEmailPresenteManutentore.Enabled = true;
                }
            }
        }
    }

    protected void chkAttivazioneUtenzaManutentore_CheckedChanged(object sender, EventArgs e)
    {
        SetRequiredEmailManutentore(chkAttivazioneUtenzaManutentore.Checked);
    }

    public void RedirectPage(string iDSoggetto, string iDTipoSoggetto, bool fSleep)
    {
        QueryString qs = new QueryString();
        qs.Add("IDSoggetto", iDSoggetto);
        qs.Add("IDTipoSoggetto", iDTipoSoggetto);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "ANAG_Anagrafica.aspx";
        url += qsEncrypted.ToString();
        if (fSleep)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + url + "';},4000);", true);
        }
        else
        {
            Response.Redirect(url, true);
        }
    }

    public void RedirectSearchPage(string iDSoggetto, string iDTipoSoggetto)
    {
        QueryString qs = new QueryString();
        qs.Add("IDSoggetto", iDSoggetto);
        qs.Add("IDTipoSoggetto", iDTipoSoggetto);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "ANAG_AnagraficaSearch.aspx";
        url += qsEncrypted.ToString();
        Response.Redirect(url, true);
    }

    public void InfoDefaultAzienda()
    {
        ddlPaeseSedeLegale.SelectedValue = "116";
        ddlPaeseNascita.SelectedValue = "116";
        ddlPaeseResidenza.SelectedValue = "116";
    }

    public void InfoDefaultManutentore()
    {
        ddlPaeseManutentore.SelectedValue = "116";
    }

    public void InfoDefaultSoftwareHouse()
    {
        ddlPaeseSedeLegaleSoftwareHouse.SelectedValue = "116";
    }

    public void InfoDefaultIspettore()
    {
        ddlPaeseNascitaIspettore.SelectedValue = "116";
        ddlPaeseResidenzaIspettore.SelectedValue = "116";
        ddlPaeseDomicilioIspettore.SelectedValue = "116";
    }

    public void InfoDefaultDistributoriCombustibile()
    {
        ddlPaeseSedeLegaleDistributoriCombustibile.SelectedValue = "116";
        ddlPaeseNascitaDistributoriCombustibile.SelectedValue = "116";
        ddlPaeseResidenzaDistributoriCombustibile.SelectedValue = "116";
    }

    public void GetDatiUser(int iDSoggetto, int iDTipoSoggetto)
    {
        using (var ctx = new CriterDataModel())
        {
            var soggetto = ctx.V_COM_AnagraficaSoggetti.FirstOrDefault(a => a.IDSoggetto == iDSoggetto);

            switch (iDTipoSoggetto)
            {
                case 1: //Persona
                case 4: //Responsabile tecnico
                    #region
                    lblCodiceSoggettoManutentore.Text = soggetto.CodiceSoggetto;
                    if (!string.IsNullOrEmpty(soggetto.IDSoggettoDerived.ToString()))
                    {
                        ASPxComboBox1.Value = soggetto.IDSoggettoDerived;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Nome))
                    {
                        txtNomeManutentore.Text = soggetto.Nome;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Cognome))
                    {
                        txtCognomeManutentore.Text = soggetto.Cognome;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Telefono))
                    {
                        txtTelefonoManutentore.Text = soggetto.Telefono;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Fax))
                    {
                        txtFaxManutentore.Text = soggetto.Fax;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Email))
                    {
                        txtEmailManutentore.Text = soggetto.Email;
                    }
                    if (!string.IsNullOrEmpty(soggetto.EmailPec))
                    {
                        txtEmailPecManutentore.Text = soggetto.EmailPec;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDPaeseSedeLegale.ToString()))
                    {
                        ddlPaeseManutentore.SelectedValue = soggetto.IDPaeseSedeLegale.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.IndirizzoSedeLegale))
                    {
                        txtIndirizzoManutentore.Text = soggetto.IndirizzoSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.NumeroCivicoSedeLegale))
                    {
                        txtNumeroCivicoManutentore.Text = soggetto.NumeroCivicoSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CapSedeLegale))
                    {
                        txtCapManutentore.Text = soggetto.CapSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CittaSedeLegale))
                    {
                        txtCittaManutentore.Text = soggetto.CittaSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDProvinciaSedeLegale.ToString()))
                    {
                        ddlProvinciaManutentore.SelectedValue = soggetto.IDProvinciaSedeLegale.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.CodiceFiscale))
                    {
                        txtCodiceFiscaleManutentore.Text = soggetto.CodiceFiscale;
                    }
                    #endregion
                    break;
                case 2: //Azienda
                    #region
                    ImgAccreditamentoAzienda.ImageUrl = soggetto.ImageUrlStatoAccreditamento;

                    QueryString qsAccreditamentoAzienda = new QueryString();
                    qsAccreditamentoAzienda.Add("IDSoggetto", soggetto.IDSoggetto.ToString());
                    qsAccreditamentoAzienda.Add("IDTipoSoggetto", soggetto.IDTipoSoggetto.ToString());
                    QueryString qsEncryptedAccreditamentoAzienda = Encryption.EncryptQueryString(qsAccreditamentoAzienda);

                    string urlAccreditamentoAzienda = "ANAG_AnagraficaAccreditamento.aspx";
                    urlAccreditamentoAzienda += qsEncryptedAccreditamentoAzienda.ToString();
                    ImgAccreditamentoAzienda.Attributes.Add("onclick", "javascript:location.href='" + urlAccreditamentoAzienda + "'");


                    lblCodiceSoggetto.Text = soggetto.CodiceSoggetto;
                    if (!string.IsNullOrEmpty(soggetto.NomeAzienda))
                    {
                        txtAzienda.Text = soggetto.NomeAzienda;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDFormaGiuridica.ToString()))
                    {
                        ddlFormaGiuridica.SelectedValue = soggetto.IDFormaGiuridica.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDPaeseSedeLegale.ToString()))
                    {
                        ddlPaeseSedeLegale.SelectedValue = soggetto.IDPaeseSedeLegale.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.IndirizzoSedeLegale))
                    {
                        txtIndirizzoSedeLegale.Text = soggetto.IndirizzoSedeLegale.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.NumeroCivicoSedeLegale))
                    {
                        txtNumeroCivicoSedeLegale.Text = soggetto.NumeroCivicoSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CapSedeLegale))
                    {
                        txtCapSedeLegale.Text = soggetto.CapSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CittaSedeLegale))
                    {
                        txtCittaSedeLegale.Text = soggetto.CittaSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDProvinciaSedeLegale.ToString()))
                    {
                        ddlProvinciaSedeLegale.SelectedValue = soggetto.IDProvinciaSedeLegale.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.PartitaIVA))
                    {
                        txtPartitaIva.Text = soggetto.PartitaIVA;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CodiceFiscale))
                    {
                        txtCodiceFiscale.Text = soggetto.CodiceFiscale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDTitoloSoggetto.ToString()))
                    {
                        ddlTitoloLegaleRappresentante.SelectedValue = soggetto.IDTitoloSoggetto.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDFunzioneSoggetto.ToString()))
                    {
                        ddlFunzioneLegaleRappresentante.SelectedValue = soggetto.IDFunzioneSoggetto.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.Nome))
                    {
                        txtNome.Text = soggetto.Nome;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Cognome))
                    {
                        txtCognome.Text = soggetto.Cognome;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Telefono))
                    {
                        txtTelefono.Text = soggetto.Telefono;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Fax))
                    {
                        txtFax.Text = soggetto.Fax;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Email))
                    {
                        txtEmail.Text = soggetto.Email;
                    }
                    if (!string.IsNullOrEmpty(soggetto.EmailPec))
                    {
                        txtEmailPec.Text = soggetto.EmailPec;
                    }
                    if (!string.IsNullOrEmpty(soggetto.SitoWeb))
                    {
                        txtSitoWeb.Text = soggetto.SitoWeb;
                    }
                    if (!string.IsNullOrEmpty(soggetto.NumeroIscrizioneAlboImprese))
                    {
                        txtNumeroAlboImprese.Text = soggetto.NumeroIscrizioneAlboImprese;
                    }
                    if (!string.IsNullOrEmpty(soggetto.NumeroIscrizioneAlboImprese))
                    {
                        ddlProvinciaAlboImprese.SelectedValue = soggetto.IDProvinciaIscrizioneAlboImprese.ToString();
                    }
                    if (soggetto.fIscrizioneRegistroGasFluorurati)
                    {
                        chkIscrizioneRegistroGasFluorurati.Checked = soggetto.fIscrizioneRegistroGasFluorurati;
                        SetVisibleHiddenIscrizioneRegistroGasFluorurati(soggetto.fIscrizioneRegistroGasFluorurati);
                    }
                    if (!string.IsNullOrEmpty(soggetto.NumeroIscrizioneRegistroGasFluorurati))
                    {
                        txtNumeroIscrizioneRegistroGasFluorurati.Text = soggetto.NumeroIscrizioneRegistroGasFluorurati;
                    }
                    if (soggetto.fPubblicazioneAlbo)
                    {
                        rblPubblicazioneAlbo.SelectedValue = (soggetto.fPubblicazioneAlbo ? 1 : 0).ToString();
                    }
                    if (soggetto.fPrivacy)
                    {
                        rblPrivacy.SelectedValue = (soggetto.fPrivacy ? 1 : 0).ToString();
                    }

                    if (!string.IsNullOrEmpty(soggetto.CodiceFiscaleAzienda))
                    {
                        txtCodiceFiscaleAzienda.Text = soggetto.CodiceFiscaleAzienda;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDPaeseNascita.ToString()))
                    {
                        ddlPaeseNascita.SelectedValue = soggetto.IDPaeseNascita.ToString();
                    }
                    if (soggetto.DataNascita != null)
                    {
                        txtDataNascita.Text = String.Format("{0:dd/MM/yyyy}", soggetto.DataNascita);
                    }
                    if (!string.IsNullOrEmpty(soggetto.CittaNascita))
                    {
                        txtCittaNascita.Text = soggetto.CittaNascita;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDProvinciaNascita.ToString()))
                    {
                        ddlProvinciaNascita.SelectedValue = soggetto.IDProvinciaNascita.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDPaeseResidenza.ToString()))
                    {
                        ddlPaeseResidenza.SelectedValue = soggetto.IDPaeseResidenza.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.IndirizzoResidenza))
                    {
                        txtIndirizzoResidenza.Text = soggetto.IndirizzoResidenza;
                    }
                    if (!string.IsNullOrEmpty(soggetto.NumeroCivicoResidenza))
                    {
                        txtNumeroCivicoResidenza.Text = soggetto.NumeroCivicoResidenza;
                    }

                    if (!string.IsNullOrEmpty(soggetto.CapResidenza))
                    {
                        txtCapResidenza.Text = soggetto.CapResidenza;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CittaResidenza))
                    {
                        txtCittaResidenza.Text = soggetto.CittaResidenza;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDProvinciaResidenza.ToString()))
                    {
                        ddlProvinciaResidenza.SelectedValue = soggetto.IDProvinciaResidenza.ToString();
                    }
                    lblfIscrizione.Text = soggetto.fIscrizione.ToString();

                    if (soggetto.fPrivacyNew)
                    {
                        pnlFilePrivacy.Visible = true;

                        string PrivacyPath = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"] + @"\" + "Privacy_" + iDSoggetto.ToString() + ".p7m";
                        FileInfo filePrivacy = new FileInfo(PrivacyPath);
                        if (File.Exists(PrivacyPath))
                        {
                            imgExportPrivacyDocument.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"] + @"\" + "Privacy_" + iDSoggetto.ToString() + ".p7m";
                        }
                    }
                    else
                    {
                        pnlFilePrivacy.Visible = false;
                    }
                    #endregion
                    break;
                case 5: //Distributore di combustibile
                    #region
                    if (!string.IsNullOrEmpty(soggetto.NomeAzienda))
                    {
                        txtDistributoriCombustibile.Text = soggetto.NomeAzienda;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDFormaGiuridica.ToString()))
                    {
                        ddlFormaGiuridicaDistributoriCombustibile.SelectedValue = soggetto.IDFormaGiuridica.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDPaeseSedeLegale.ToString()))
                    {
                        ddlPaeseSedeLegaleDistributoriCombustibile.SelectedValue = soggetto.IDPaeseSedeLegale.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.IndirizzoSedeLegale))
                    {
                        txtIndirizzoSedeLegaleDistributoriCombustibile.Text = soggetto.IndirizzoSedeLegale.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.NumeroCivicoSedeLegale))
                    {
                        txtCivicoSedeLegaleDistributoriCombustibile.Text = soggetto.NumeroCivicoSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CapSedeLegale))
                    {
                        txtCapSedeLegaleDistributoriCombustibile.Text = soggetto.CapSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CittaSedeLegale))
                    {
                        txtCittaSedeLegaleDistributoriCombustibile.Text = soggetto.CittaSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDProvinciaSedeLegale.ToString()))
                    {
                        ddlProvinciaSedeLegaleDistributoriCombustibile.SelectedValue = soggetto.IDProvinciaSedeLegale.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.PartitaIVA))
                    {
                        txtPartitaIvaDistributoriCombustibile.Text = soggetto.PartitaIVA;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CodiceFiscaleAzienda))
                    {
                        txtCodiceFiscaleAziendaDistributoriCombustibile.Text = soggetto.CodiceFiscaleAzienda;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Telefono))
                    {
                        txtTelefonoDistributoriCombustibile.Text = soggetto.Telefono;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Fax))
                    {
                        txtFaxDistributoriCombustibile.Text = soggetto.Fax;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Email))
                    {
                        txtEmailDistributoriCombustibile.Text = soggetto.Email;
                    }
                    if (!string.IsNullOrEmpty(soggetto.EmailPec))
                    {
                        txtEmailPecDistributoriCombustibile.Text = soggetto.EmailPec;
                    }
                    if (!string.IsNullOrEmpty(soggetto.SitoWeb))
                    {
                        txtSitoWebDistributoriCombustibile.Text = soggetto.SitoWeb;
                    }
                    if (!string.IsNullOrEmpty(soggetto.NumeroIscrizioneAlboImprese))
                    {
                        txtNumeroAlboImpreseDistributoriCombustibile.Text = soggetto.NumeroIscrizioneAlboImprese;
                    }
                    if (!string.IsNullOrEmpty(soggetto.NumeroIscrizioneAlboImprese))
                    {
                        ddlProvinciaAlboImpreseDistributoriCombustibile.SelectedValue = soggetto.IDProvinciaIscrizioneAlboImprese.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.Nome))
                    {
                        txtNomeDistributoriCombustibile.Text = soggetto.Nome;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Cognome))
                    {
                        txtCognomeDistributoriCombustibile.Text = soggetto.Cognome;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDPaeseNascita.ToString()))
                    {
                        ddlPaeseNascitaDistributoriCombustibile.SelectedValue = soggetto.IDPaeseNascita.ToString();
                    }
                    if (soggetto.DataNascita != null)
                    {
                        txtDataNascitaDistributoriCombustibile.Text = String.Format("{0:dd/MM/yyyy}", soggetto.DataNascita);
                    }
                    if (!string.IsNullOrEmpty(soggetto.CittaNascita))
                    {
                        txtCittaNascitaDistributoriCombustibile.Text = soggetto.CittaNascita;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDProvinciaNascita.ToString()))
                    {
                        ddlProvinciaNascitaDistributoriCombustibile.SelectedValue = soggetto.IDProvinciaNascita.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.CodiceFiscale))
                    {
                        txtCodiceFiscaleDistributoriCombustibile.Text = soggetto.CodiceFiscale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDPaeseResidenza.ToString()))
                    {
                        ddlPaeseResidenzaDistributoriCombustibile.SelectedValue = soggetto.IDPaeseResidenza.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.IndirizzoResidenza))
                    {
                        txtIndirizzoResidenzaDistributoriCombustibile.Text = soggetto.IndirizzoResidenza;
                    }
                    if (!string.IsNullOrEmpty(soggetto.NumeroCivicoResidenza))
                    {
                        txtNumeroCivicoResidenzaDistributoriCombustibile.Text = soggetto.NumeroCivicoResidenza;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CapResidenza))
                    {
                        txtCapResidenzaDistributoriCombustibile.Text = soggetto.CapResidenza;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CittaResidenza))
                    {
                        txtCittaResidenzaDistributoriCombustibile.Text = soggetto.CittaResidenza;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDProvinciaResidenza.ToString()))
                    {
                        ddlProvinciaResidenzaDistributoriCombustibile.SelectedValue = soggetto.IDProvinciaResidenza.ToString();
                    }
                    if (soggetto.fPrivacy)
                    {
                        rblPrivacyDistributoriCombustibile.SelectedValue = (soggetto.fPrivacy ? 1 : 0).ToString();
                    }
                    lblfIscrizioneDistributoriCombustibile.Text = soggetto.fIscrizione.ToString();
                    #endregion
                    break;
                case 6: //Software house
                    #region
                    if (!string.IsNullOrEmpty(soggetto.NomeAzienda))
                    {
                        txtSoftwareHouse.Text = soggetto.NomeAzienda;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDFormaGiuridica.ToString()))
                    {
                        ddlFormaGiuridicaSoftwareHouse.SelectedValue = soggetto.IDFormaGiuridica.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDPaeseSedeLegale.ToString()))
                    {
                        ddlPaeseSedeLegaleSoftwareHouse.SelectedValue = soggetto.IDPaeseSedeLegale.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.IndirizzoSedeLegale))
                    {
                        txtIndirizzoSedeLegaleSoftwareHouse.Text = soggetto.IndirizzoSedeLegale.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.NumeroCivicoSedeLegale))
                    {
                        txtCivicoSedeLegaleSoftwareHouse.Text = soggetto.NumeroCivicoSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CapSedeLegale))
                    {
                        txtCapSedeLegaleSoftwareHouse.Text = soggetto.CapSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CittaSedeLegale))
                    {
                        txtCittaSedeLegaleSoftwareHouse.Text = soggetto.CittaSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDProvinciaSedeLegale.ToString()))
                    {
                        ddlProvinciaSedeLegaleSoftwareHouse.SelectedValue = soggetto.IDProvinciaSedeLegale.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.PartitaIVA))
                    {
                        txtPartitaIvaSoftwareHouse.Text = soggetto.PartitaIVA;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CodiceFiscaleAzienda))
                    {
                        txtCodiceFiscaleSoftwareHouse.Text = soggetto.CodiceFiscaleAzienda;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Nome))
                    {
                        txtNomeSoftwareHouse.Text = soggetto.Nome;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Cognome))
                    {
                        txtCognomeSoftwareHouse.Text = soggetto.Cognome;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Telefono))
                    {
                        txtTelefonoSoftwareHouse.Text = soggetto.Telefono;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Fax))
                    {
                        txtFaxSoftwareHouse.Text = soggetto.Fax;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Email))
                    {
                        txtEmailSoftwareHouse.Text = soggetto.Email;
                    }
                    if (!string.IsNullOrEmpty(soggetto.EmailPec))
                    {
                        txtEmailPecSoftwareHouse.Text = soggetto.EmailPec;
                    }
                    if (!string.IsNullOrEmpty(soggetto.SitoWeb))
                    {
                        txtSitoWebSoftwareHouse.Text = soggetto.SitoWeb;
                    }

                    #endregion
                    break;
                case 7: //Ispettore
                    #region
                    ImgAccreditamentoIspettore.ImageUrl = soggetto.ImageUrlStatoAccreditamento;

                    QueryString qsAccreditamento = new QueryString();
                    qsAccreditamento.Add("IDSoggetto", soggetto.IDSoggetto.ToString());
                    qsAccreditamento.Add("IDTipoSoggetto", soggetto.IDTipoSoggetto.ToString());
                    QueryString qsEncryptedAccreditamento = Encryption.EncryptQueryString(qsAccreditamento);

                    string urlAccreditamento = "ANAG_AnagraficaAccreditamento.aspx";
                    urlAccreditamento += qsEncryptedAccreditamento.ToString();
                    ImgAccreditamentoIspettore.Attributes.Add("onclick", "javascript:location.href='" + urlAccreditamento + "'");


                    lblCodiceSoggettoIspettore.Text = soggetto.CodiceSoggetto;
                    if (!string.IsNullOrEmpty(soggetto.IDTitoloSoggetto.ToString()))
                    {
                        ddlTitoloIspettore.SelectedValue = soggetto.IDTitoloSoggetto.ToString();
                    }
                    lblfIscrizioneIspettore.Text = soggetto.fIscrizione.ToString();
                    if (!string.IsNullOrEmpty(soggetto.Nome))
                    {
                        txtNomeIspettore.Text = soggetto.Nome;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Cognome))
                    {
                        txtCognomeIspettore.Text = soggetto.Cognome;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDPaeseNascita.ToString()))
                    {
                        ddlPaeseNascitaIspettore.SelectedValue = soggetto.IDPaeseNascita.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDPaeseResidenza.ToString()))
                    {
                        ddlPaeseResidenzaIspettore.SelectedValue = soggetto.IDPaeseResidenza.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDPaeseDomicilio.ToString()))
                    {
                        ddlPaeseDomicilioIspettore.SelectedValue = soggetto.IDPaeseDomicilio.ToString();
                    }
                    if (soggetto.DataNascita != null)
                    {
                        txtDataNascitaIspettore.Text = String.Format("{0:dd/MM/yyyy}", soggetto.DataNascita);
                    }
                    if (!string.IsNullOrEmpty(soggetto.CittaNascita))
                    {
                        txtCittaNascitaIspettore.Text = soggetto.CittaNascita;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDProvinciaNascita.ToString()))
                    {
                        ddlProvinciaNascitaIspettore.SelectedValue = soggetto.IDProvinciaNascita.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.CodiceFiscale))
                    {
                        txtCodiceFiscaleIspettore.Text = soggetto.CodiceFiscale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.PartitaIVA))
                    {
                        txtPartitaIvaIspettore.Text = soggetto.PartitaIVA;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Telefono))
                    {
                        txtTelefonoIspettore.Text = soggetto.Telefono;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Cellulare))
                    {
                        txtCellulareIspettore.Text = soggetto.Cellulare;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Email))
                    {
                        txtEmailIspettore.Text = soggetto.Email;
                    }
                    if (!string.IsNullOrEmpty(soggetto.EmailPec))
                    {
                        txtEmailPecIspettore.Text = soggetto.EmailPec;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IndirizzoResidenza))
                    {
                        txtIndirizzoResidenzaIspettore.Text = soggetto.IndirizzoResidenza.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.NumeroCivicoResidenza))
                    {
                        txtNumeroCivicoResidenzaIspettore.Text = soggetto.NumeroCivicoResidenza;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CapResidenza))
                    {
                        txtCapResidenzaIspettore.Text = soggetto.CapResidenza;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CittaResidenza))
                    {
                        txtCittaResidenzaIspettore.Text = soggetto.CittaResidenza;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDProvinciaResidenza.ToString()))
                    {
                        ddlProvinciaResidenzaIspettore.SelectedValue = soggetto.IDProvinciaResidenza.ToString();
                    }
                    chkDomicilio.Checked = soggetto.fDomicilioUgualeResidenza;
                    if (!string.IsNullOrEmpty(soggetto.IndirizzoDomicilio))
                    {
                        txtIndirizzoDomicilioIspettore.Text = soggetto.IndirizzoDomicilio.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.NumeroCivicoDomicilio))
                    {
                        txtNumeroCivicoDomicilioIspettore.Text = soggetto.NumeroCivicoDomicilio;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CapDomicilio))
                    {
                        txtCapDomicilioIspettore.Text = soggetto.CapDomicilio;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CittaDomicilio))
                    {
                        txtCittaDomicilioIspettore.Text = soggetto.CittaDomicilio;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDProvinciaDomicilio.ToString()))
                    {
                        ddlProvinciaDomicilioIspettore.SelectedValue = soggetto.IDProvinciaDomicilio.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDTipologiaQualificaIspettore.ToString()))
                    {
                        rblTipoQualificaIspettore.SelectedValue = soggetto.IDTipologiaQualificaIspettore.ToString();
                    }
                    chkIdoneoIspezione.Checked = soggetto.fIdoneoIspezione;
                    chkNoCondanna.Checked = soggetto.fCondanna;
                    if (!string.IsNullOrEmpty(soggetto.IDTipologiaTitoloStudio.ToString()))
                    {
                        ddlTipologiaTitoloStudio.SelectedValue = soggetto.IDTipologiaTitoloStudio.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.TitoloStudioConseguitoPresso))
                    {
                        txtTitoloStudioConseguitoPresso.Text = soggetto.TitoloStudioConseguitoPresso;
                    }
                    if (soggetto.DataTitoloStudio != null)
                    {
                        txtDataTitoloStudio.Text = String.Format("{0:dd/MM/yyyy}", soggetto.DataTitoloStudio);
                    }
                    if (!string.IsNullOrEmpty(soggetto.StageAzienda))
                    {
                        txtStageAzienda.Text = soggetto.StageAzienda;
                    }
                    if (soggetto.DataStageAziendaDal != null)
                    {
                        txtDataStageAziendaDa.Text = String.Format("{0:dd/MM/yyyy}", soggetto.DataStageAziendaDal);
                    }
                    if (soggetto.DataStageAziendaAl != null)
                    {
                        txtDataStageAziendaAl.Text = String.Format("{0:dd/MM/yyyy}", soggetto.DataStageAziendaAl);
                    }
                    if (!string.IsNullOrEmpty(soggetto.CorsoFormazione))
                    {
                        txtCorsoFormazione.Text = soggetto.CorsoFormazione;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CorsoFormazioneOrganizzatore))
                    {
                        txtCorsoFormazioneOrganizzatore.Text = soggetto.CorsoFormazioneOrganizzatore;
                    }
                    chkRequisitoOrganizzativo.Checked = soggetto.fRequisitoOrganizzativo;
                    chkDisponibilitaApparecchiature.Checked = soggetto.fDisponibilitaApparecchiature;
                    chkDisponibilitaControlli.Checked = soggetto.fDisponibilitaControlli;
                    chkIscrizioneRegistroGasFluoruratiIspettore.Checked = soggetto.fIscrizioneRegistroGasFluorurati;
                    if (!string.IsNullOrEmpty(soggetto.NumeroIscrizioneRegistroGasFluorurati))
                    {
                        txtNumeroIscrizioneRegistroGasFluoruratiIspettore.Text = soggetto.NumeroIscrizioneRegistroGasFluorurati;
                    }
                    if (soggetto.fOrganizzazioneEsterna)
                    {
                        rblOrganizzazioneIspettore.SelectedValue = (soggetto.fOrganizzazioneEsterna ? 1 : 0).ToString();
                    }

                    if (!string.IsNullOrEmpty(soggetto.NomeAzienda))
                    {
                        txtAziendaIspettore.Text = soggetto.NomeAzienda;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDPaeseSedeLegale.ToString()))
                    {
                        ddlPaeseOrganizzazioneIspettore.SelectedValue = soggetto.IDPaeseSedeLegale.ToString();
                    }

                    if (!string.IsNullOrEmpty(soggetto.IndirizzoSedeLegale))
                    {
                        txtIndirizzoOrganizzazioneIspettore.Text = soggetto.IndirizzoSedeLegale.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.NumeroCivicoSedeLegale))
                    {
                        txtNumeroCivicoOrganizzazioneIspettore.Text = soggetto.NumeroCivicoSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CapSedeLegale))
                    {
                        txtCapOrganizzazioneIspettore.Text = soggetto.CapSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CittaSedeLegale))
                    {
                        txtCittaOrganizzazioneIspettore.Text = soggetto.CittaSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDProvinciaSedeLegale.ToString()))
                    {
                        ddlProvinciaOrganizzazioneIspettore.SelectedValue = soggetto.IDProvinciaSedeLegale.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.PartitaIVAOrganizzazione))
                    {
                        txtPartitaIvaOrganizzazioneIspettore.Text = soggetto.PartitaIVAOrganizzazione;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CodiceFiscaleAzienda))
                    {
                        txtCodiceFiscaleOrganizzazioneIspettore.Text = soggetto.CodiceFiscaleAzienda;
                    }
                    if (!string.IsNullOrEmpty(soggetto.TelefonoOrganizzazione))
                    {
                        txtTelefonoOrganizzazioneIspettore.Text = soggetto.TelefonoOrganizzazione;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Fax))
                    {
                        txtFaxOrganizzazioneIspettore.Text = soggetto.Fax;
                    }
                    if (!string.IsNullOrEmpty(soggetto.EmailOrganizzazione))
                    {
                        txtEmailOrganizzazioneIspettore.Text = soggetto.EmailOrganizzazione;
                    }
                    if (!string.IsNullOrEmpty(soggetto.EmailPecOrganizzazione))
                    {
                        txtEmailPecOrganizzazioneIspettore.Text = soggetto.EmailPecOrganizzazione;
                    }
                    if (!string.IsNullOrEmpty(soggetto.OrganismoIspezioneNumero))
                    {
                        txtOrganismoIspezioneNumeroIspettore.Text = soggetto.OrganismoIspezioneNumero;
                    }
                    if (!string.IsNullOrEmpty(soggetto.OrganismoIspezione))
                    {
                        txtOrganismoIspezioneIspettore.Text = soggetto.OrganismoIspezione;
                    }

                    string curriculumVitaePath = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspettori"] + @"\" + iDSoggetto.ToString() + @"\" + "CurriculumVitaeIspettore_" + iDSoggetto.ToString() + ".pdf";
                    string attestatoCorsoPath = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspettori"] + @"\" + iDSoggetto.ToString() + @"\" + "AttestatoCorsoIspettore_" + iDSoggetto.ToString() + ".pdf";
                    FileInfo fileCurriculumVitae = new FileInfo(curriculumVitaePath);
                    if (File.Exists(curriculumVitaePath))
                    {
                        lnkCurriculumVitae.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + ConfigurationManager.AppSettings["UploadIspettori"] + @"\" + iDSoggetto.ToString() + @"\" + "CurriculumVitaeIspettore_" + iDSoggetto.ToString() + ".pdf";
                    }

                    FileInfo fileAttestatoCorso = new FileInfo(attestatoCorsoPath);
                    if (File.Exists(attestatoCorsoPath))
                    {
                        lnkAttestatoCorso.NavigateUrl = ConfigurationManager.AppSettings["UrlPortal"] + ConfigurationManager.AppSettings["UploadIspettori"] + @"\" + iDSoggetto.ToString() + @"\" + "AttestatoCorsoIspettore_" + iDSoggetto.ToString() + ".pdf";
                    }

                    if (soggetto.fConsensoRequisitiDichiarati)
                    {
                        rblConsensoRequisitiDichiarati.SelectedValue = (soggetto.fConsensoRequisitiDichiarati ? 1 : 0).ToString();
                    }
                    if (soggetto.fPrivacy)
                    {
                        rblPrivacyIspettore.SelectedValue = (soggetto.fPrivacy ? 1 : 0).ToString();
                    }

                    if (!string.IsNullOrEmpty(soggetto.IDTipologiaOrdineCollegio.ToString()))
                    {
                        rblTipoInserimentoAziendaOrdineCollegio.SelectedValue = soggetto.IDTipologiaOrdineCollegio.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDProvinciaOrdineCollegio.ToString()))
                    {
                        ddlOrdineCollegioProvincia.SelectedValue = soggetto.IDProvinciaOrdineCollegio.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.SezioneOrdineCollegio))
                    {
                        txtOrdineCollegioSezione.Text = soggetto.SezioneOrdineCollegio;
                    }
                    if (!string.IsNullOrEmpty(soggetto.NumeroOrdineCollegio))
                    {
                        txtOrdineCollegioNumero.Text = soggetto.NumeroOrdineCollegio;
                    }
                    if (soggetto.DataOrdineCollegio != null)
                    {
                        txtOrdineCollegioData.Text = String.Format("{0:dd/MM/yyyy}", soggetto.DataOrdineCollegio);
                    }
                    #endregion
                    break;
                case 8: //Cittadino
                    #region
                    if (!string.IsNullOrEmpty(soggetto.Nome))
                    {
                        lblNomeCittadino.Text = soggetto.Nome;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Cognome))
                    {
                        lblCognomeCittadino.Text = soggetto.Cognome;
                    }
                    if (!string.IsNullOrEmpty(soggetto.PaeseNascita))
                    {
                        lblPaeseNascitaCittadino.Text = soggetto.PaeseNascita;
                    }
                    if (soggetto.DataNascita != null)
                    {
                        lblDataNascitaCittadino.Text = String.Format("{0:dd/MM/yyyy}", soggetto.DataNascita);
                    }
                    if (!string.IsNullOrEmpty(soggetto.CittaNascita))
                    {
                        lblCittaNascitaCittadino.Text = soggetto.CittaNascita;
                    }
                    if (!string.IsNullOrEmpty(soggetto.ProvinciaNascita))
                    {
                        lblProvinciaNascitaCittadino.Text = soggetto.ProvinciaNascita;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CodiceFiscale))
                    {
                        lblCodiceFiscaleCittadino.Text = soggetto.CodiceFiscale;
                    }
                    #endregion
                    break;
                case 9: //Ente Locale
                    #region
                    if (!string.IsNullOrEmpty(soggetto.NomeAzienda))
                    {
                        txtEnteLocale.Text = soggetto.NomeAzienda;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDFormaGiuridica.ToString()))
                    {
                        ddlFormaGiuridicaEnteLocale.SelectedValue = soggetto.IDFormaGiuridica.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.IndirizzoSedeLegale))
                    {
                        txtIndirizzoSedeLegaleEnteLocale.Text = soggetto.IndirizzoSedeLegale.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.NumeroCivicoSedeLegale))
                    {
                        txtNumeroCivicoSedeLegaleEnteLocale.Text = soggetto.NumeroCivicoSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CapSedeLegale))
                    {
                        txtCapSedeLegaleEnteLocale.Text = soggetto.CapSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CittaSedeLegale))
                    {
                        txtCittaSedeLegaleEnteLocale.Text = soggetto.CittaSedeLegale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDProvinciaSedeLegale.ToString()))
                    {
                        ddlProvinciaSedeLegaleEnteLocale.SelectedValue = soggetto.IDProvinciaSedeLegale.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.PartitaIVA))
                    {
                        txtPartitaIvaEnteLocale.Text = soggetto.PartitaIVA;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CodiceFiscaleAzienda))
                    {
                        txtCodiceFiscaleEnteLocale.Text = soggetto.CodiceFiscaleAzienda;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Telefono))
                    {
                        txtTelefonoEnteLocale.Text = soggetto.Telefono;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Fax))
                    {
                        txtFaxEnteLocale.Text = soggetto.Fax;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Email))
                    {
                        txtEmailEnteLocale.Text = soggetto.Email;
                    }
                    if (!string.IsNullOrEmpty(soggetto.EmailPec))
                    {
                        txtEmailPecEnteLocale.Text = soggetto.EmailPec;
                    }
                    if (!string.IsNullOrEmpty(soggetto.SitoWeb))
                    {
                        txtSitoWebEnteLocale.Text = soggetto.SitoWeb;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDFunzioneSoggetto.ToString()))
                    {
                        ddlFunzioneLegaleRappresentanteEnteLocale.SelectedValue = soggetto.IDFunzioneSoggetto.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.Nome))
                    {
                        txtNomeEnteLocale.Text = soggetto.Nome;
                    }
                    if (!string.IsNullOrEmpty(soggetto.Cognome))
                    {
                        txtCognomeEnteLocale.Text = soggetto.Cognome;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDPaeseNascita.ToString()))
                    {
                        ddlPaeseNascitaEnteLocale.SelectedValue = soggetto.IDPaeseNascita.ToString();
                    }
                    if (soggetto.DataNascita != null)
                    {
                        txtDataNascitaEnteLocale.Text = String.Format("{0:dd/MM/yyyy}", soggetto.DataNascita);
                    }
                    if (!string.IsNullOrEmpty(soggetto.CittaNascita))
                    {
                        txtCittaNascitaEnteLocale.Text = soggetto.CittaNascita;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDProvinciaNascita.ToString()))
                    {
                        ddlProvinciaNascitaEnteLocale.SelectedValue = soggetto.IDProvinciaNascita.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.CodiceFiscale))
                    {
                        txtCodiceFiscaleLegaleRappresentanteEnteLocale.Text = soggetto.CodiceFiscale;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDPaeseResidenza.ToString()))
                    {
                        ddlPaeseResidenzaLegaleRappresentanteEnteLocale.SelectedValue = soggetto.IDPaeseResidenza.ToString();
                    }
                    if (!string.IsNullOrEmpty(soggetto.IndirizzoResidenza))
                    {
                        txtIndirizzoResidenzaLegaleRappresentanteEnteLocale.Text = soggetto.IndirizzoResidenza;
                    }
                    if (!string.IsNullOrEmpty(soggetto.NumeroCivicoResidenza))
                    {
                        txtNumeroCivicoResidenzaLegaleRappresentanteEnteLocale.Text = soggetto.NumeroCivicoResidenza;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CapResidenza))
                    {
                        txtCapResidenzaLegaleRappresentanteEnteLocale.Text = soggetto.CapResidenza;
                    }
                    if (!string.IsNullOrEmpty(soggetto.CittaResidenza))
                    {
                        txtCittaResidenzaLegaleRappresentanteEnteLocale.Text = soggetto.CittaResidenza;
                    }
                    if (!string.IsNullOrEmpty(soggetto.IDProvinciaResidenza.ToString()))
                    {
                        ddlProvinciaResidenzaLegaleRappresentanteEnteLocale.SelectedValue = soggetto.IDProvinciaResidenza.ToString();
                    }
                    if (soggetto.fPrivacy)
                    {
                        rblPrivacyEnteLocale.SelectedValue = (soggetto.fPrivacy ? 1 : 0).ToString();
                    }
                    lblfIscrizioneEnteLocale.Text = soggetto.fIscrizione.ToString();
                    #endregion
                    break;
            }
        }
    }

    public void GetDatiUserRuoli(int iDSoggetto)
    {
        var result = UtilitySoggetti.GetValoriUsersRuoloSoggetto(iDSoggetto);

        foreach (var row in result)
        {
            cblRuoliSoggetto.Items.FindByValue(row.IDRuoloSoggetto.ToString()).Selected = true;
        }
    }

    public void GetDatiUserClassificazioniImpianto(int iDSoggetto)
    {
        var result = UtilitySoggetti.GetValoriUsersClassificazioniImpianto(iDSoggetto);

        foreach (var row in result)
        {
            cblClassificazioniImpianto.Items.FindByValue(row.IDClassificazioneImpianto.ToString()).Selected = true;
        }
    }

    public void GetDatiUserProvinceCompetenza(int iDSoggetto)
    {
        var result = UtilitySoggetti.GetValoriUsersProvinceCompetenza(iDSoggetto);

        foreach (var row in result)
        {
            cblProvinceCompetenza.Items.FindByValue(row.IDProvincia.ToString()).Selected = true;

        }
    }

    public void GetDatiUserAbilitazioniSoggetto(int iDSoggetto)
    {
        var result = UtilitySoggetti.GetValoriUsersAbilitazioniSoggetto(iDSoggetto);

        foreach (var row in result)
        {
            cblAbilitazioniSoggetto.Items.FindByValue(row.IDAbilitazioneSoggetto.ToString()).Selected = true;
        }
    }

    public void GetDatiUserTipiDistributoriCombustibile(int iDSoggetto)
    {
        var result = UtilitySoggetti.GetValoriUsersTipiDistributoriCombustibile(iDSoggetto);

        foreach (var row in result)
        {
            cblTipiDistributoriCombustibile.Items.FindByValue(row.IDTipologiaDistributoriCombustibile.ToString()).Selected = true;
            if (row.IDTipologiaDistributoriCombustibile == 1)
            {
                txtTipiDistributoriCombustibileAltro.Text = row.TipologiaDistributoriCombustibileAltro;
            }
        }

        VisibleTipiDistributoriCombustibileAltro();
    }

    protected void ddlPaeseSedeLegale_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFieldPaeseSedeLegale();
    }

    protected void ddlPaeseNascita_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFieldPaeseNascita();
    }

    protected void ddlPaeseResidenza_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFieldPaeseResidenza();
    }

    public void VisibleHiddenFieldPaeseResidenza()
    {
        if (ddlPaeseResidenza.SelectedValue == "116")
        {
            lblProvinciaResidenza.Visible = true;
            ddlProvinciaResidenza.Visible = true;
            rfvddlProvinciaResidenza.Visible = true;
        }
        else
        {
            lblProvinciaResidenza.Visible = false;
            ddlProvinciaResidenza.Visible = false;
            ddlProvinciaResidenza.SelectedIndex = 0;
            rfvddlProvinciaResidenza.Visible = false;
        }
    }

    protected void ddlPaeseManutentore_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFieldPaeseManutentore();
    }

    public void VisibleHiddenFieldPaeseSedeLegale()
    {
        if (ddlPaeseSedeLegale.SelectedValue == "116")
        {
            lblProvinciaSedeLegale.Visible = true;
            ddlProvinciaSedeLegale.Visible = true;
            rfvddlProvinciaSedeLegale.Visible = true;

            lblProvinciaAlboImprese.Visible = true;
            ddlProvinciaAlboImprese.Visible = true;
            rfvddlProvinciaAlboImprese.Visible = true;
            txtNumeroAlboImprese.CssClass = "txtClass_o";
            rfvtxtNumeroAlboImprese.Visible = true;
            lblNumeroAlboImprese.Text = "Numero iscrizione registro imprese (*)";
        }
        else
        {
            lblProvinciaSedeLegale.Visible = false;
            ddlProvinciaSedeLegale.Visible = false;
            ddlProvinciaSedeLegale.SelectedIndex = 0;
            rfvddlProvinciaSedeLegale.Visible = false;

            lblProvinciaAlboImprese.Visible = false;
            ddlProvinciaAlboImprese.Visible = false;
            rfvddlProvinciaAlboImprese.Visible = false;
            txtNumeroAlboImprese.CssClass = "txtClass";
            rfvtxtNumeroAlboImprese.Visible = false;
            ddlProvinciaAlboImprese.SelectedIndex = 0;
            lblNumeroAlboImprese.Text = "Numero iscrizione registro imprese";
        }
    }

    public void VisibleHiddenFieldPaeseManutentore()
    {
        if (ddlPaeseManutentore.SelectedValue == "116")
        {
            lblProvinciaManutentore.Visible = true;
            ddlProvinciaManutentore.Visible = true;
            rfvddlProvinciaManutentore.Visible = true;
        }
        else
        {
            lblProvinciaManutentore.Visible = false;
            ddlProvinciaManutentore.Visible = false;
            ddlProvinciaManutentore.SelectedIndex = 0;
            rfvddlProvinciaManutentore.Visible = false;
        }
    }

    public void VisibleHiddenFieldPaeseSoftwareHouse()
    {
        if (ddlPaeseSedeLegaleSoftwareHouse.SelectedValue == "116")
        {
            lblTitoloddlProvinciaSedeLegaleSoftwareHouse.Visible = true;
            ddlProvinciaSedeLegaleSoftwareHouse.Visible = true;
            rfvddlProvinciaSedeLegaleSoftwareHouse.Visible = true;
        }
        else
        {
            lblTitoloddlProvinciaSedeLegaleSoftwareHouse.Visible = false;
            ddlProvinciaSedeLegaleSoftwareHouse.Visible = false;
            ddlProvinciaSedeLegaleSoftwareHouse.SelectedIndex = 0;
            rfvddlProvinciaSedeLegaleSoftwareHouse.Visible = false;
        }
    }

    public void VisibleHiddenFieldPaeseNascita()
    {
        if (ddlPaeseNascita.SelectedValue == "116")
        {
            lblProvinciaNascita.Visible = true;
            ddlProvinciaNascita.Visible = true;
            rfvddlProvinciaNascita.Visible = true;
        }
        else
        {
            lblProvinciaNascita.Visible = false;
            ddlProvinciaNascita.Visible = false;
            ddlProvinciaNascita.SelectedIndex = 0;
            rfvddlProvinciaNascita.Visible = false;
        }
    }

    public void VisibleHiddenFieldCodiceSoggetto()
    {
        switch (IDTipoSoggetto)
        {
            case "1": //Persona
                if (lblCodiceSoggettoManutentore.Text != "")
                {
                    rowCodiceSoggettoManutentore.Visible = true;
                }
                else
                {
                    rowCodiceSoggettoManutentore.Visible = false;
                }
                break;
            case "2": //Azienda
                if (lblCodiceSoggetto.Text != "")
                {
                    rowCodiceSoggetto.Visible = true;
                }
                else
                {
                    rowCodiceSoggetto.Visible = false;
                }
                break;
            case "7": //Ispettore
                if (lblCodiceSoggettoIspettore.Text != "")
                {
                    rowCodiceSoggettoIspettore.Visible = true;
                }
                else
                {
                    rowCodiceSoggettoIspettore.Visible = false;
                }
                break;
        }
    }

    #region DROPDOWNLIST / RADIOBUTTONLIST / CHECKBOXLIST
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

    protected void ASPxComboBox_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetValoriFilterByIDAzienda();
        if (ASPxComboBox1.Value != null)
        {
            int? iDSoggetto = UtilityApp.ParseNullableInt(ASPxComboBox1.Value.ToString());
            GetImportaDatiAzienda(iDSoggetto);
        }
    }

    protected void GetValoriFilterByIDAzienda()
    {
        if (ASPxComboBox1.Value != null)
        {

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
    }
    #endregion

    protected void ddFormaGiuridica(int? IDPresel, DropDownList ddlFormaGiuridica, int iDTipoSoggetto)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_FormeGiuridiche(IDPresel, iDTipoSoggetto);
        ddlFormaGiuridica.DataValueField = "IDFormaGiuridica";
        ddlFormaGiuridica.DataTextField = "FormaGiuridica";
        ddlFormaGiuridica.DataSource = ls;
        ddlFormaGiuridica.DataBind();

        ListItem myItem = new ListItem("-- Selezionare --", "0");
        ddlFormaGiuridica.Items.Insert(0, myItem);
        ddlFormaGiuridica.SelectedIndex = 0;
    }

    protected void ddPaeseSoggetto(int? IDPresel, DropDownList ddlPaese)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_Paesi(IDPresel);
        ddlPaese.DataValueField = "IDPaese";
        ddlPaese.DataTextField = "Paese";
        ddlPaese.DataSource = ls;
        ddlPaese.DataBind();

        ListItem myItem = new ListItem("-- Selezionare --", "0");
        ddlPaese.Items.Insert(0, myItem);
        ddlPaese.SelectedIndex = 0;
    }

    protected void ddProvincia(int? IDPresel, DropDownList ddlProvincia)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_Province(IDPresel, false);
        ddlProvincia.DataValueField = "IDProvincia";
        ddlProvincia.DataTextField = "Provincia";
        ddlProvincia.DataSource = ls;
        ddlProvincia.DataBind();

        ListItem myItem = new ListItem("-- Selezionare --", "0");
        ddlProvincia.Items.Insert(0, myItem);
        ddlProvincia.SelectedIndex = 0;
    }

    protected void ddTitoloSoggetto(int? IDPresel, DropDownList ddlTitolo)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_TitoliSoggetti(IDPresel);
        ddlTitolo.DataValueField = "IDTitoloSoggetto";
        ddlTitolo.DataTextField = "TitoloSoggetto";
        ddlTitolo.DataSource = ls;
        ddlTitolo.DataBind();

        ListItem myItem = new ListItem("-- Selezionare --", "0");
        ddlTitolo.Items.Insert(0, myItem);
        ddlTitolo.SelectedIndex = 0;
    }

    protected void ddFunzioneSoggetto(int? IDPresel, DropDownList ddlFunzioneSoggetto, int iDTipoSoggetto)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_FunzioniSoggetti(IDPresel, iDTipoSoggetto);
        ddlFunzioneSoggetto.DataValueField = "IDFunzioneSoggetto";
        ddlFunzioneSoggetto.DataTextField = "FunzioneSoggetto";
        ddlFunzioneSoggetto.DataSource = ls;
        ddlFunzioneSoggetto.DataBind();

        ListItem myItem = new ListItem("-- Selezionare --", "0");
        ddlFunzioneSoggetto.Items.Insert(0, myItem);
        ddlFunzioneSoggetto.SelectedIndex = 0;
    }

    protected void cbProvinceCompetenza(int? IDPresel)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_Province(IDPresel, true);
        cblProvinceCompetenza.DataValueField = "IDProvincia";
        cblProvinceCompetenza.DataTextField = "Provincia";
        cblProvinceCompetenza.DataSource = ls;
        cblProvinceCompetenza.DataBind();
    }

    protected void cbRuoliSoggetto()
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_RuoloSoggetto(null);
        cblRuoliSoggetto.DataValueField = "IDRuoloSoggetto";
        cblRuoliSoggetto.DataTextField = "RuoloSoggetto";
        cblRuoliSoggetto.DataSource = ls;
        cblRuoliSoggetto.DataBind();
    }

    protected void cbAbilitazioniSoggetti(int? IDPresel)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_AbilitazioneSoggetto(IDPresel);
        cblAbilitazioniSoggetto.DataValueField = "IDAbilitazioneSoggetto";
        cblAbilitazioniSoggetto.DataTextField = "AbilitazioneSoggetto";
        cblAbilitazioniSoggetto.DataSource = ls;
        cblAbilitazioniSoggetto.DataBind();
    }

    protected void cbClassificazioniImpianto()
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_ClassificazioneImpianto(null);
        cblClassificazioniImpianto.DataValueField = "IDClassificazioneImpianto";
        cblClassificazioniImpianto.DataTextField = "ClassificazioneImpianto";
        cblClassificazioniImpianto.DataSource = ls;
        cblClassificazioniImpianto.DataBind();
    }

    protected void rbQualificheIspettori(int? IDPresel)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_TipologiaQualificaIspettore(IDPresel);
        rblTipoQualificaIspettore.DataValueField = "IDTipologiaQualificaIspettore";
        rblTipoQualificaIspettore.DataTextField = "TipologiaQualificaIspettore";
        rblTipoQualificaIspettore.DataSource = ls;
        rblTipoQualificaIspettore.DataBind();

        rblTipoQualificaIspettore.SelectedIndex = 0;
    }

    protected void ddTipologiaTitoloStudio(int? IDPresel)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_TipologiaTitoloStudio(IDPresel);
        ddlTipologiaTitoloStudio.DataValueField = "IDTipologiaTitoloStudio";
        ddlTipologiaTitoloStudio.DataTextField = "TipologiaTitoloStudio";
        ddlTipologiaTitoloStudio.DataSource = ls;
        ddlTipologiaTitoloStudio.DataBind();

        ListItem myItem = new ListItem("-- Selezionare --", "0");
        ddlTipologiaTitoloStudio.Items.Insert(0, myItem);
        ddlTipologiaTitoloStudio.SelectedIndex = 0;
    }

    protected void rbTipologiaOrdineCollegio(int? IDPresel)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_TipologiaOrdineCollegio(IDPresel);
        rblTipoInserimentoAziendaOrdineCollegio.DataValueField = "IDTipologiaOrdineCollegio";
        rblTipoInserimentoAziendaOrdineCollegio.DataTextField = "TipologiaOrdineCollegio";
        rblTipoInserimentoAziendaOrdineCollegio.DataSource = ls;
        rblTipoInserimentoAziendaOrdineCollegio.DataBind();

        rblTipoInserimentoAziendaOrdineCollegio.SelectedIndex = 0;
    }

    protected void cbTipologiaDistributoriCombustibile()
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_TipologiaDistributoriCombustibile(null);
        cblTipiDistributoriCombustibile.DataValueField = "IDTipologiaDistributoriCombustibile";
        cblTipiDistributoriCombustibile.DataTextField = "TipologiaDistributoriCombustibile";
        cblTipiDistributoriCombustibile.DataSource = ls;
        cblTipiDistributoriCombustibile.DataBind();
    }
    #endregion

    #region CUSTOM CONTROL
    public void ControllaCodiceFiscale(Object sender, ServerValidateEventArgs e)
    {
        string codicefiscale = "";
        if (txtCodiceFiscale.Text != "")
        {
            codicefiscale = txtCodiceFiscale.Text;
        }
        else if (txtCodiceFiscaleManutentore.Text != "")
        {
            codicefiscale = txtCodiceFiscaleManutentore.Text;
        }
        if (codicefiscale != "")
        {
            bool fCodFis = CodiceFiscale.ControlloFormale(codicefiscale);
            if (!fCodFis)
            {
                e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaPiva(Object sender, ServerValidateEventArgs e)
    {
        //Controllo solo se Italia
        if (ddlPaeseSedeLegale.SelectedValue == "116")
        {
            bool fPiva = UtilityApp.CheckPartitaIva(txtPartitaIva.Text);
            if (!fPiva)
            {
                e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaProvinceCompetenza(Object sender, ServerValidateEventArgs e)
    {
        int counter = 0;

        for (int i = 0; i < cblProvinceCompetenza.Items.Count; i++)
        {
            if (cblProvinceCompetenza.Items[i].Selected)
            {
                counter++;
            }

            e.IsValid = (counter == 0) ? false : true;
        }
    }

    public void ControllaRuoliSoggetto(Object sender, ServerValidateEventArgs e)
    {
        int counter = 0;

        for (int i = 0; i < cblRuoliSoggetto.Items.Count; i++)
        {
            if (cblRuoliSoggetto.Items[i].Selected)
            {
                counter++;
            }

            e.IsValid = (counter == 0) ? false : true;
        }
    }

    public void ControllaClassificazioniImpianto(Object sender, ServerValidateEventArgs e)
    {
        int counter = 0;

        for (int i = 0; i < cblClassificazioniImpianto.Items.Count; i++)
        {
            if (cblClassificazioniImpianto.Items[i].Selected)
            {
                counter++;
            }

            e.IsValid = (counter == 0) ? false : true;
        }
    }

    public void ControllaEmailPresente(Object sender, ServerValidateEventArgs e)
    {
        int? iDSoggetto = null;
        if (!string.IsNullOrWhiteSpace(IDSoggetto))
        {
            iDSoggetto = int.Parse(IDSoggetto);
        }

        bool fEmail = UtilitySoggetti.CheckfEmail(txtEmail.Text, iDSoggetto, int.Parse(IDTipoSoggetto));
        if (fEmail)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaEmailPecPresente(Object sender, ServerValidateEventArgs e)
    {
        int? iDSoggetto = null;
        if (!string.IsNullOrWhiteSpace(IDSoggetto))
        {
            iDSoggetto = int.Parse(IDSoggetto);
        }

        bool fEmail = UtilitySoggetti.CheckfEmailPec(txtEmailPec.Text, iDSoggetto, int.Parse(IDTipoSoggetto));
        if (fEmail)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaPartitaIvaPresente(Object sender, ServerValidateEventArgs e)
    {
        int? iDSoggetto = null;
        if (!string.IsNullOrWhiteSpace(IDSoggetto))
        {
            iDSoggetto = int.Parse(IDSoggetto);
        }

        bool fEsiste = UtilitySoggetti.CheckfPartitaIva(txtPartitaIva.Text, iDSoggetto, 2);
        if (fEsiste)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaCodiceFiscaleIspettore(Object sender, ServerValidateEventArgs e)
    {
        bool fCodFis = CodiceFiscale.ControlloFormale(txtCodiceFiscaleIspettore.Text);
        if (!fCodFis)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaEmailPresenteIspettore(Object sender, ServerValidateEventArgs e)
    {
        int? iDSoggetto = null;
        if (!string.IsNullOrWhiteSpace(IDSoggetto))
        {
            iDSoggetto = int.Parse(IDSoggetto);
        }

        bool fEmail = UtilitySoggetti.CheckfEmail(txtEmailIspettore.Text, iDSoggetto, int.Parse(IDTipoSoggetto));
        if (fEmail)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaEmailPecPresenteIspettore(Object sender, ServerValidateEventArgs e)
    {
        int? iDSoggetto = null;
        if (!string.IsNullOrWhiteSpace(IDSoggetto))
        {
            iDSoggetto = int.Parse(IDSoggetto);
        }

        bool fEmail = UtilitySoggetti.CheckfEmailPec(txtEmailPecIspettore.Text, iDSoggetto, int.Parse(IDTipoSoggetto));
        if (fEmail)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaEmailPresenteManutentore(Object sender, ServerValidateEventArgs e)
    {
        int? iDSoggetto = null;
        if (!string.IsNullOrWhiteSpace(IDSoggetto))
        {
            iDSoggetto = int.Parse(IDSoggetto);
        }

        bool fEmail = UtilitySoggetti.CheckfEmail(txtEmailManutentore.Text, iDSoggetto, int.Parse(IDTipoSoggetto));
        if (fEmail)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaCodiceFiscalePresenteManutentore(Object sender, ServerValidateEventArgs e)
    {
        int? iDSoggetto = null;
        if (!string.IsNullOrWhiteSpace(IDSoggetto))
        {
            iDSoggetto = int.Parse(IDSoggetto);
        }

        bool fEmail = UtilitySoggetti.CheckfEmail(txtCodiceFiscaleManutentore.Text, iDSoggetto, int.Parse(IDTipoSoggetto));
        if (fEmail)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }


    public void ControllaPivaDistributoriCombustibile(Object sender, ServerValidateEventArgs e)
    {
        //Controllo solo se Italia
        if (ddlPaeseSedeLegaleDistributoriCombustibile.SelectedValue == "116")
        {
            bool fPiva = UtilityApp.CheckPartitaIva(txtPartitaIvaDistributoriCombustibile.Text);
            if (!fPiva)
            {
                e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaTipiDistributoriCombustibile(Object sender, ServerValidateEventArgs e)
    {
        int counter = 0;

        for (int i = 0; i < cblTipiDistributoriCombustibile.Items.Count; i++)
        {
            if (cblTipiDistributoriCombustibile.Items[i].Selected)
            {
                counter++;
            }

            e.IsValid = (counter == 0) ? false : true;
        }
    }

    public void ControllaEmailPresenteDistributoriCombustibile(Object sender, ServerValidateEventArgs e)
    {
        int? iDSoggetto = null;
        if (!string.IsNullOrWhiteSpace(IDSoggetto))
        {
            iDSoggetto = int.Parse(IDSoggetto);
        }

        bool fEmail = UtilitySoggetti.CheckfEmail(txtEmailDistributoriCombustibile.Text, iDSoggetto, int.Parse(IDTipoSoggetto));
        if (fEmail)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaEmailPecPresenteDistributoriCombustibile(Object sender, ServerValidateEventArgs e)
    {
        int? iDSoggetto = null;
        if (!string.IsNullOrWhiteSpace(IDSoggetto))
        {
            iDSoggetto = int.Parse(IDSoggetto);
        }

        bool fEmail = UtilitySoggetti.CheckfEmailPec(txtEmailPecDistributoriCombustibile.Text, iDSoggetto, int.Parse(IDTipoSoggetto));
        if (fEmail)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaPartitaIvaPresenteDistributoriCombustibile(Object sender, ServerValidateEventArgs e)
    {
        int? iDSoggetto = null;
        if (!string.IsNullOrWhiteSpace(IDSoggetto))
        {
            iDSoggetto = int.Parse(IDSoggetto);
        }

        bool fEsiste = UtilitySoggetti.CheckfPartitaIva(txtPartitaIvaDistributoriCombustibile.Text, iDSoggetto, int.Parse(IDTipoSoggetto));
        if (fEsiste)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }

    #endregion

    protected void SetVisibleHiddenIscrizioneRegistroGasFluorurati(bool iscrizioneRegistroGasFluorurati)
    {
        if (iscrizioneRegistroGasFluorurati)
        {
            rowIscrizioneRegistroGasFluorurati.Visible = true;
        }
        else
        {
            rowIscrizioneRegistroGasFluorurati.Visible = false;
            txtNumeroIscrizioneRegistroGasFluorurati.Text = "";
        }
    }

    protected void chkIscrizioneRegistroGasFluorurati_CheckedChanged(object sender, EventArgs e)
    {
        SetVisibleHiddenIscrizioneRegistroGasFluorurati(chkIscrizioneRegistroGasFluorurati.Checked);
    }

    protected void SaveProvinceCompetenza(int? iDSoggetto)
    {
        List<string> valoriProvinceCompetenza = new List<string>();
        foreach (ListItem item in cblProvinceCompetenza.Items)
        {
            if (item.Selected)
            {
                valoriProvinceCompetenza.Add(item.Value);
            }
        }
        UtilitySoggetti.SaveInsertDeleteDatiProvinceCompetenza(int.Parse(iDSoggetto.ToString()), valoriProvinceCompetenza.ToArray<string>());
    }

    protected void SaveRuoliSoggetto(int? iDSoggetto)
    {
        List<string> valoriRuoliSoggetto = new List<string>();
        foreach (ListItem item in cblRuoliSoggetto.Items)
        {
            if (item.Selected)
            {
                valoriRuoliSoggetto.Add(item.Value);
            }
        }
        UtilitySoggetti.SaveInsertDeleteDatiRuoliSoggetto(int.Parse(iDSoggetto.ToString()), valoriRuoliSoggetto.ToArray<string>());
    }

    protected void SaveClassificazioniImpianto(int? iDSoggetto)
    {
        List<string> valoriClassificazioniImpianto = new List<string>();
        foreach (ListItem item in cblClassificazioniImpianto.Items)
        {
            if (item.Selected)
            {
                valoriClassificazioniImpianto.Add(item.Value);
            }
        }
        UtilitySoggetti.SaveInsertDeleteDatiClassificazioniImpiantoSoggetto(int.Parse(iDSoggetto.ToString()), valoriClassificazioniImpianto.ToArray<string>());
    }

    protected void SaveAbilitazioniSoggetto(int? iDSoggetto)
    {
        List<string> valoriAbilitazioniSoggetto = new List<string>();
        foreach (ListItem item in cblAbilitazioniSoggetto.Items)
        {
            if (item.Selected)
            {
                valoriAbilitazioniSoggetto.Add(item.Value);
            }
        }
        UtilitySoggetti.SaveInsertDeleteDatiAbilitazioniSoggetto(int.Parse(iDSoggetto.ToString()), valoriAbilitazioniSoggetto.ToArray<string>());
    }

    protected int SaveIscrizione(int? iDSoggetto, int iDTipoSoggetto)
    {
        int iDSoggettoInsert = 0;
        object[] coordinate = new object[2];
        switch (IDTipoSoggetto)
        {
            case "1": //Persona
            case "4": //Responsabile tecnico
                coordinate = UtilityApp.GetGeocodingAddress(txtIndirizzoManutentore.Text + " ," + txtNumeroCivicoManutentore.Text,
                                                    txtCapManutentore.Text,
                                                    txtCittaManutentore.Text,
                                                    ddlPaeseManutentore.SelectedItem.Text);
                break;
            case "2": //Azienda
                coordinate = UtilityApp.GetGeocodingAddress(txtIndirizzoSedeLegale.Text + " ," + txtNumeroCivicoSedeLegale.Text,
                                                    txtCapSedeLegale.Text,
                                                    txtCittaSedeLegale.Text,
                                                    ddlPaeseSedeLegale.SelectedItem.Text);
                break;
            case "5": //Distributori di Combustibile
                coordinate = UtilityApp.GetGeocodingAddress(txtIndirizzoSedeLegaleDistributoriCombustibile.Text + " ," + txtCivicoSedeLegaleDistributoriCombustibile.Text,
                                                    txtCapSedeLegaleDistributoriCombustibile.Text,
                                                    txtCittaSedeLegaleDistributoriCombustibile.Text,
                                                    ddlPaeseSedeLegaleDistributoriCombustibile.SelectedItem.Text);
                break;
            case "6": //Software house
                coordinate = UtilityApp.GetGeocodingAddress(txtIndirizzoSedeLegaleSoftwareHouse.Text + " ," + txtCivicoSedeLegaleSoftwareHouse.Text,
                                                    txtCapSedeLegaleSoftwareHouse.Text,
                                                    txtCittaSedeLegaleSoftwareHouse.Text,
                                                    ddlPaeseSedeLegaleSoftwareHouse.SelectedItem.Text);
                break;
            case "7": //Ispettori
                coordinate = UtilityApp.GetGeocodingAddress(txtIndirizzoResidenzaIspettore.Text + " ," + txtNumeroCivicoResidenzaIspettore.Text,
                                                            txtCapResidenzaIspettore.Text,
                                                            txtCittaResidenzaIspettore.Text,
                                                            string.Empty);
                break;
            case "9": //Ente locale
                coordinate = UtilityApp.GetGeocodingAddress(txtIndirizzoSedeLegaleEnteLocale.Text + " ," + txtNumeroCivicoSedeLegaleEnteLocale.Text,
                                                            txtCapSedeLegaleEnteLocale.Text,
                                                            txtCittaSedeLegaleEnteLocale.Text,
                                                            "Italia");
                break;
        }


        if (iDSoggetto == null)
        {
            switch (IDTipoSoggetto)
            {
                case "1": //Persona
                case "4": //Responsabile Tecnico
                    iDSoggettoInsert = UtilitySoggetti.SaveInsertDeleteDatiSoggetti(
                            "insert",
                            iDSoggetto,
                            lblCodiceSoggettoManutentore.Text,
                            UtilityApp.ParseNullableInt(ASPxComboBox1.Value.ToString()),
                            iDTipoSoggetto,
                            string.Empty,
                            null,
                            UtilityApp.ParseNullableInt(ddlPaeseManutentore.SelectedItem.Value),
                            txtIndirizzoManutentore.Text,
                            txtCapManutentore.Text,
                            txtCittaManutentore.Text,
                            txtNumeroCivicoManutentore.Text,
                            UtilityApp.ParseNullableInt(ddlProvinciaManutentore.SelectedItem.Value),
                            null,
                            null,
                            txtNomeManutentore.Text,
                            txtCognomeManutentore.Text,
                            null,
                            null,
                            string.Empty,
                            null,
                            null,
                            string.Empty,
                            null,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            txtCodiceFiscaleManutentore.Text,
                            txtTelefonoManutentore.Text,
                            txtFaxManutentore.Text,
                            txtEmailManutentore.Text,
                            txtEmailPecManutentore.Text,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            null,
                            false,
                            string.Empty,
                            DateTime.Now,
                            false,
                            true,
                            null,
                            DateTime.Now,
                            null,
                            DateTime.Now,
                            true,
                            coordinate,
                            true,

                            null,
                            null,
                            string.Empty,
                            null,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            null,
                            null,
                            null,
                            null,
                            string.Empty,
                            null,
                            string.Empty,
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null,
                            null,
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null,
                            
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null
                            );
                    break;
                case "2": //Azienda
                    iDSoggettoInsert = UtilitySoggetti.SaveInsertDeleteDatiSoggetti(
                           "insert",
                           null,
                           lblCodiceSoggetto.Text,
                           null,
                           iDTipoSoggetto,
                           txtAzienda.Text,
                           UtilityApp.ParseNullableInt(ddlFormaGiuridica.SelectedItem.Value),
                           UtilityApp.ParseNullableInt(ddlPaeseSedeLegale.SelectedItem.Value),
                           txtIndirizzoSedeLegale.Text,
                           txtCapSedeLegale.Text,
                           txtCittaSedeLegale.Text,
                           txtNumeroCivicoSedeLegale.Text,
                           UtilityApp.ParseNullableInt(ddlProvinciaSedeLegale.SelectedItem.Value),
                           UtilityApp.ParseNullableInt(ddlTitoloLegaleRappresentante.SelectedItem.Value),
                           UtilityApp.ParseNullableInt(ddlFunzioneLegaleRappresentante.SelectedItem.Value),
                           txtNome.Text,
                           txtCognome.Text,
                           UtilityApp.ParseNullableInt(ddlPaeseNascita.SelectedItem.Value),
                           UtilityApp.ParseNullableDatetime(txtDataNascita.Text),
                           txtCittaNascita.Text,
                           UtilityApp.ParseNullableInt(ddlProvinciaNascita.SelectedItem.Value),
                           UtilityApp.ParseNullableInt(ddlPaeseResidenza.SelectedItem.Value),
                           txtCittaResidenza.Text,
                           UtilityApp.ParseNullableInt(ddlProvinciaResidenza.SelectedItem.Value),
                           txtCapResidenza.Text,
                           txtIndirizzoResidenza.Text,
                           txtNumeroCivicoResidenza.Text,
                           txtCodiceFiscale.Text,
                           txtTelefono.Text,
                           txtFax.Text,
                           txtEmail.Text,
                           txtEmailPec.Text,
                           txtPartitaIva.Text,
                           txtCodiceFiscaleAzienda.Text,
                           txtSitoWeb.Text,
                           txtNumeroAlboImprese.Text,
                           UtilityApp.ParseNullableInt(ddlProvinciaAlboImprese.SelectedItem.Value),
                           chkIscrizioneRegistroGasFluorurati.Checked,
                           txtNumeroIscrizioneRegistroGasFluorurati.Text,
                           DateTime.Now,
                           Convert.ToBoolean(Convert.ToInt16(rblPubblicazioneAlbo.SelectedItem.Value)),
                           Convert.ToBoolean(Convert.ToInt16(rblPrivacy.SelectedItem.Value)),
                           null,
                           DateTime.Now,
                           null,
                           DateTime.Now,
                           true,
                           coordinate,
                           false,
                           null,
                           null,
                            string.Empty,
                            null,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            null,
                            null,
                            null,
                            null,
                            string.Empty,
                            null,
                            string.Empty,
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null,
                            null,
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null,
                            
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null
                    );
                    UtilitySoggetti.InsertDatiAlboSoggetti(iDSoggettoInsert);
                    break;
                case "5": //Distributori di combustibile
                    iDSoggettoInsert = UtilitySoggetti.SaveInsertDeleteDatiSoggetti(
                           "insert",
                           null,
                           string.Empty,
                           null,
                           iDTipoSoggetto,
                           txtDistributoriCombustibile.Text,
                           UtilityApp.ParseNullableInt(ddlFormaGiuridicaDistributoriCombustibile.SelectedItem.Value),
                           UtilityApp.ParseNullableInt(ddlPaeseSedeLegaleDistributoriCombustibile.SelectedItem.Value),
                           txtIndirizzoSedeLegaleDistributoriCombustibile.Text,
                           txtCapSedeLegaleDistributoriCombustibile.Text,
                           txtCittaSedeLegaleDistributoriCombustibile.Text,
                           txtCivicoSedeLegaleDistributoriCombustibile.Text,
                           UtilityApp.ParseNullableInt(ddlProvinciaSedeLegaleDistributoriCombustibile.SelectedItem.Value),
                           null,
                           null,
                           txtNomeDistributoriCombustibile.Text,
                           txtCognomeDistributoriCombustibile.Text,
                           UtilityApp.ParseNullableInt(ddlPaeseNascitaDistributoriCombustibile.SelectedItem.Value),
                           UtilityApp.ParseNullableDatetime(txtDataNascitaDistributoriCombustibile.Text),
                           txtCittaNascitaDistributoriCombustibile.Text,
                           UtilityApp.ParseNullableInt(ddlProvinciaNascitaDistributoriCombustibile.SelectedItem.Value),
                           UtilityApp.ParseNullableInt(ddlPaeseResidenzaDistributoriCombustibile.SelectedItem.Value),
                           txtCittaResidenzaDistributoriCombustibile.Text,
                           UtilityApp.ParseNullableInt(ddlProvinciaResidenzaDistributoriCombustibile.SelectedItem.Value),
                           txtCapResidenzaDistributoriCombustibile.Text,
                           txtIndirizzoResidenzaDistributoriCombustibile.Text,
                           txtNumeroCivicoResidenzaDistributoriCombustibile.Text,
                           txtCodiceFiscaleDistributoriCombustibile.Text,
                           txtTelefonoDistributoriCombustibile.Text,
                           txtFaxDistributoriCombustibile.Text,
                           txtEmailDistributoriCombustibile.Text,
                           txtEmailPecDistributoriCombustibile.Text,
                           txtPartitaIvaDistributoriCombustibile.Text,
                           txtCodiceFiscaleAziendaDistributoriCombustibile.Text,
                           txtSitoWebDistributoriCombustibile.Text,
                           txtNumeroAlboImpreseDistributoriCombustibile.Text,
                           UtilityApp.ParseNullableInt(ddlProvinciaAlboImpreseDistributoriCombustibile.SelectedItem.Value),
                           false,
                           string.Empty,
                           DateTime.Now,
                           false,
                           Convert.ToBoolean(Convert.ToInt16(rblPrivacyDistributoriCombustibile.SelectedItem.Value)),
                           null,
                           DateTime.Now,
                           null,
                           DateTime.Now,
                           true,
                           coordinate,
                           false,
                           null,
                           null,
                           string.Empty,
                           null,
                           string.Empty,
                           string.Empty,
                           string.Empty,
                           string.Empty,
                           string.Empty,
                           string.Empty,
                           string.Empty,
                           string.Empty,
                           null,
                           null,
                           null,
                           null,
                           string.Empty,
                           null,
                           string.Empty,
                           null,
                           null,
                           string.Empty,
                           string.Empty,
                           null,
                           null,
                           null,
                           null,
                           string.Empty,
                           string.Empty,
                           null,
                           
                           null,
                           null,
                           string.Empty,
                           string.Empty,
                           null
                        );
                    break;
                case "6": //Software house
                    iDSoggettoInsert = UtilitySoggetti.SaveInsertDeleteDatiSoggetti(
                           "insert",
                           null,
                           string.Empty,
                           null,
                           iDTipoSoggetto,
                           txtSoftwareHouse.Text,
                           UtilityApp.ParseNullableInt(ddlFormaGiuridicaSoftwareHouse.SelectedItem.Value),
                           UtilityApp.ParseNullableInt(ddlPaeseSedeLegaleSoftwareHouse.SelectedItem.Value),
                           txtIndirizzoSedeLegaleSoftwareHouse.Text,
                           txtCapSedeLegaleSoftwareHouse.Text,
                           txtCittaSedeLegaleSoftwareHouse.Text,
                           txtCivicoSedeLegaleSoftwareHouse.Text,
                           UtilityApp.ParseNullableInt(ddlProvinciaSedeLegaleSoftwareHouse.SelectedItem.Value),
                           null,
                           null,
                           txtNomeSoftwareHouse.Text,
                           txtCognomeSoftwareHouse.Text,
                           null,
                           null,
                           string.Empty,
                           null,
                           null,
                           string.Empty,
                           null,
                           string.Empty,
                           string.Empty,
                           string.Empty,
                           string.Empty,
                           txtTelefonoSoftwareHouse.Text,
                           txtFaxSoftwareHouse.Text,
                           txtEmailSoftwareHouse.Text,
                           txtEmailPecSoftwareHouse.Text,
                           txtPartitaIvaSoftwareHouse.Text,
                           txtCodiceFiscaleSoftwareHouse.Text,
                           txtSitoWebSoftwareHouse.Text,
                           string.Empty,
                           null,
                           false,
                           string.Empty,
                           DateTime.Now,
                           false,
                           true,
                           null,
                           DateTime.Now,
                           null,
                           DateTime.Now,
                           true,
                           coordinate,
                           true,
                           null,
                           null,

                            string.Empty,
                            null,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            null,
                            null,
                            null,
                            null,
                            string.Empty,
                            null,
                            string.Empty,
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null,
                            null,
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null,
                            
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null
                           );
                    break;
                case "7": //Ispettori
                    iDSoggettoInsert = UtilitySoggetti.SaveInsertDeleteDatiSoggetti(
                       "insert",                                                                           //
                       null,                                                                               //
                       "",                                                                                 //
                       null,                                                                               //
                       int.Parse(IDTipoSoggetto),                                                          //
                       txtAziendaIspettore.Text,                                                           //
                       null,
                       UtilityApp.ParseNullableInt(ddlPaeseOrganizzazioneIspettore.SelectedItem.Value),    //
                       txtIndirizzoOrganizzazioneIspettore.Text,                                           //
                       txtCapOrganizzazioneIspettore.Text,                                                 //
                       txtCittaOrganizzazioneIspettore.Text,                                               //
                       txtNumeroCivicoOrganizzazioneIspettore.Text,                                        //
                       UtilityApp.ParseNullableInt(ddlProvinciaOrganizzazioneIspettore.SelectedItem.Value),//
                       UtilityApp.ParseNullableInt(ddlTitoloIspettore.SelectedItem.Value),             //
                       null,                                                                           //
                       txtNomeIspettore.Text,                                                          //
                       txtCognomeIspettore.Text,                                                       //
                       UtilityApp.ParseNullableInt(ddlPaeseNascitaIspettore.SelectedItem.Value),       //
                       UtilityApp.ParseNullableDatetime(txtDataNascitaIspettore.Text),                 //
                       txtCittaNascitaIspettore.Text,                                                  //
                       UtilityApp.ParseNullableInt(ddlProvinciaNascitaIspettore.SelectedItem.Value),   //
                       UtilityApp.ParseNullableInt(ddlPaeseResidenzaIspettore.SelectedItem.Value),
                       txtCittaResidenzaIspettore.Text,                                                //
                       UtilityApp.ParseNullableInt(ddlProvinciaResidenzaIspettore.SelectedItem.Value), //
                       txtCapResidenzaIspettore.Text,                                                  //
                       txtIndirizzoResidenzaIspettore.Text,                                            //
                       txtNumeroCivicoResidenzaIspettore.Text,                                         //                    
                       txtCodiceFiscaleIspettore.Text,                                                 //
                       txtTelefonoIspettore.Text,                                                      //                                                    
                       txtFaxOrganizzazioneIspettore.Text,                                             //
                       txtEmailIspettore.Text,                                                         //
                       txtEmailPecIspettore.Text,                                                      //
                       txtPartitaIvaIspettore.Text,                                                    //

                       txtCodiceFiscaleOrganizzazioneIspettore.Text,                                   //
                       string.Empty,
                       string.Empty,
                       null,
                       chkIscrizioneRegistroGasFluoruratiIspettore.Checked,                            //
                       txtNumeroIscrizioneRegistroGasFluoruratiIspettore.Text,                         //
                       DateTime.Now,
                       null,
                       Convert.ToBoolean(Convert.ToInt16(rblPrivacyIspettore.SelectedItem.Value)),         //
                       null,
                       DateTime.Now,
                       null,
                       DateTime.Now,
                       true,
                       coordinate,                                                                         //
                       false,                                                                              //

                       chkDomicilio.Checked,                                                               // 
                       UtilityApp.ParseNullableInt(ddlPaeseDomicilioIspettore.SelectedItem.Value),
                       txtCittaDomicilioIspettore.Text,                                                    //
                       UtilityApp.ParseNullableInt(ddlProvinciaDomicilioIspettore.SelectedItem.Value),     //
                       txtCapDomicilioIspettore.Text,                                                      //
                       txtIndirizzoDomicilioIspettore.Text,                                                //
                       txtNumeroCivicoDomicilioIspettore.Text,                                             //
                       txtCellulareIspettore.Text,                                                         //
                       txtTelefonoOrganizzazioneIspettore.Text,                                            //
                       txtEmailOrganizzazioneIspettore.Text,                                               //
                       txtEmailPecOrganizzazioneIspettore.Text,                                            //
                       txtPartitaIvaOrganizzazioneIspettore.Text,                                          //
                       UtilityApp.ParseNullableInt(rblTipoQualificaIspettore.SelectedItem.Value),          //
                       chkIdoneoIspezione.Checked,                                                         //
                       chkNoCondanna.Checked,                                                              //
                       UtilityApp.ParseNullableInt(ddlTipologiaTitoloStudio.SelectedItem.Value),           //
                       txtTitoloStudioConseguitoPresso.Text,                                               //
                       UtilityApp.ParseNullableDatetime(txtDataTitoloStudio.Text),                         //
                       txtStageAzienda.Text,                                                               //
                       UtilityApp.ParseNullableDatetime(txtDataStageAziendaDa.Text),                       //
                       UtilityApp.ParseNullableDatetime(txtDataStageAziendaAl.Text),                       //
                       txtCorsoFormazione.Text,                                                            //
                       txtCorsoFormazioneOrganizzatore.Text,                                               //
                       chkRequisitoOrganizzativo.Checked,                                                  //
                       chkDisponibilitaApparecchiature.Checked,                                            //
                       chkDisponibilitaControlli.Checked,                                                  //
                       Convert.ToBoolean(Convert.ToInt16(rblOrganizzazioneIspettore.SelectedItem.Value)),  //
                       txtOrganismoIspezioneNumeroIspettore.Text,                                          //
                       txtOrganismoIspezioneIspettore.Text,                                                    //
                       Convert.ToBoolean(Convert.ToInt16(rblConsensoRequisitiDichiarati.SelectedItem.Value)),  //
                       
                       UtilityApp.ParseNullableInt(rblTipoInserimentoAziendaOrdineCollegio.SelectedItem.Value),
                       UtilityApp.ParseNullableInt(ddlOrdineCollegioProvincia.SelectedItem.Value),
                       txtOrdineCollegioSezione.Text,
                       txtOrdineCollegioNumero.Text,
                       UtilityApp.ParseNullableDatetime(txtOrdineCollegioData.Text)
                       );
                    break;
                case "9": //Ente locale
                    iDSoggettoInsert = UtilitySoggetti.SaveInsertDeleteDatiSoggetti(
                    "insert",
                    null,
                    "",
                    null,
                    int.Parse(IDTipoSoggetto),
                    txtEnteLocale.Text,
                    UtilityApp.ParseNullableInt(ddlFormaGiuridicaEnteLocale.SelectedItem.Value),
                    116,
                    txtIndirizzoSedeLegaleEnteLocale.Text,
                    txtCapSedeLegaleEnteLocale.Text,
                    txtCittaSedeLegaleEnteLocale.Text,
                    txtNumeroCivicoSedeLegaleEnteLocale.Text,
                    UtilityApp.ParseNullableInt(ddlProvinciaSedeLegaleEnteLocale.SelectedItem.Value),
                    null,
                    UtilityApp.ParseNullableInt(ddlFunzioneLegaleRappresentanteEnteLocale.SelectedItem.Value),
                    txtNomeEnteLocale.Text,
                    txtCognomeEnteLocale.Text,
                    UtilityApp.ParseNullableInt(ddlPaeseNascitaEnteLocale.SelectedItem.Value),
                    UtilityApp.ParseNullableDatetime(txtDataNascitaEnteLocale.Text),
                    txtCittaNascitaEnteLocale.Text,
                    UtilityApp.ParseNullableInt(ddlProvinciaNascitaEnteLocale.SelectedItem.Value),
                    UtilityApp.ParseNullableInt(ddlPaeseResidenzaLegaleRappresentanteEnteLocale.SelectedItem.Value),
                    txtCittaResidenzaLegaleRappresentanteEnteLocale.Text,
                    UtilityApp.ParseNullableInt(ddlProvinciaResidenzaLegaleRappresentanteEnteLocale.SelectedItem.Value),
                    txtCapResidenzaLegaleRappresentanteEnteLocale.Text,
                    txtIndirizzoResidenzaLegaleRappresentanteEnteLocale.Text,
                    txtNumeroCivicoResidenzaLegaleRappresentanteEnteLocale.Text,
                    txtCodiceFiscaleLegaleRappresentanteEnteLocale.Text,
                    txtTelefonoEnteLocale.Text,
                    txtFaxEnteLocale.Text,
                    txtEmailEnteLocale.Text,
                    txtEmailPecEnteLocale.Text,
                    txtPartitaIvaEnteLocale.Text,
                    txtCodiceFiscaleEnteLocale.Text,
                    txtSitoWebEnteLocale.Text,
                    string.Empty,
                    null,
                    false,
                    string.Empty,
                    DateTime.Now,
                    false,
                    Convert.ToBoolean(Convert.ToInt16(rblPrivacyEnteLocale.SelectedItem.Value)),
                    null,
                    DateTime.Now,
                    null,
                    DateTime.Now,
                    true,
                    coordinate,
                    false,
                    null,
                    null,
                    string.Empty,
                    null,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    null,
                    null,
                    null,
                    null,
                    string.Empty,
                    null,
                    string.Empty,
                    null,
                    null,
                    string.Empty,
                    string.Empty,
                    null,
                    null,
                    null,
                    null,
                    string.Empty,
                    string.Empty,
                    null,
                                        
                    null,
                    null,
                    string.Empty,
                    string.Empty,
                    null
                    );
                    break;
            }
        }
        else
        {
            switch (IDTipoSoggetto)
            {
                case "1": //Persona
                case "4": //Responsabile Tecnico
                    iDSoggettoInsert = UtilitySoggetti.SaveInsertDeleteDatiSoggetti(
                            "update",
                            iDSoggetto,
                            lblCodiceSoggettoManutentore.Text,
                            UtilityApp.ParseNullableInt(ASPxComboBox1.Value.ToString()),
                            iDTipoSoggetto,
                            string.Empty,
                            null,
                            UtilityApp.ParseNullableInt(ddlPaeseManutentore.SelectedItem.Value),
                            txtIndirizzoManutentore.Text,
                            txtCapManutentore.Text,
                            txtCittaManutentore.Text,
                            txtNumeroCivicoManutentore.Text,
                            UtilityApp.ParseNullableInt(ddlProvinciaManutentore.SelectedItem.Value),
                            null,
                            null,
                            txtNomeManutentore.Text,
                            txtCognomeManutentore.Text,
                            null,
                            null,
                            string.Empty,
                            null,
                            null,
                            string.Empty,
                            null,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            txtCodiceFiscaleManutentore.Text,
                            txtTelefonoManutentore.Text,
                            txtFaxManutentore.Text,
                            txtEmailManutentore.Text,
                            txtEmailPecManutentore.Text,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            null,
                            false,
                            string.Empty,
                            DateTime.Now,
                            false,
                            true,
                            null,
                            DateTime.Now,
                            null,
                            DateTime.Now,
                            true,
                            coordinate,
                            true,
                            null,
                            null,

                            string.Empty,
                            null,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            null,
                            null,
                            null,
                            null,
                            string.Empty,
                            null,
                            string.Empty,
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null,
                            null,
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null,
                            
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null
                            );
                    break;
                case "2": //Azienda
                    iDSoggettoInsert = UtilitySoggetti.SaveInsertDeleteDatiSoggetti(
                            "update",
                            iDSoggetto,
                            lblCodiceSoggetto.Text,
                            null,
                            iDTipoSoggetto,
                            txtAzienda.Text,
                            UtilityApp.ParseNullableInt(ddlFormaGiuridica.SelectedItem.Value),
                            UtilityApp.ParseNullableInt(ddlPaeseSedeLegale.SelectedItem.Value),
                            txtIndirizzoSedeLegale.Text,
                            txtCapSedeLegale.Text,
                            txtCittaSedeLegale.Text,
                            txtNumeroCivicoSedeLegale.Text,
                            UtilityApp.ParseNullableInt(ddlProvinciaSedeLegale.SelectedItem.Value),
                            UtilityApp.ParseNullableInt(ddlTitoloLegaleRappresentante.SelectedItem.Value),
                            UtilityApp.ParseNullableInt(ddlFunzioneLegaleRappresentante.SelectedItem.Value),
                            txtNome.Text,
                            txtCognome.Text,
                            UtilityApp.ParseNullableInt(ddlPaeseNascita.SelectedItem.Value),
                            UtilityApp.ParseNullableDatetime(txtDataNascita.Text),
                            txtCittaNascita.Text,
                            UtilityApp.ParseNullableInt(ddlProvinciaNascita.SelectedItem.Value),
                            UtilityApp.ParseNullableInt(ddlPaeseResidenza.SelectedItem.Value),
                            txtCittaResidenza.Text,
                            UtilityApp.ParseNullableInt(ddlProvinciaResidenza.SelectedItem.Value),
                            txtCapResidenza.Text,
                            txtIndirizzoResidenza.Text,
                            txtNumeroCivicoResidenza.Text,
                            txtCodiceFiscale.Text,
                            txtTelefono.Text,
                            txtFax.Text,
                            txtEmail.Text,
                            txtEmailPec.Text,
                            txtPartitaIva.Text,
                            txtCodiceFiscaleAzienda.Text,
                            txtSitoWeb.Text,
                            txtNumeroAlboImprese.Text,
                            UtilityApp.ParseNullableInt(ddlProvinciaAlboImprese.SelectedItem.Value),
                            chkIscrizioneRegistroGasFluorurati.Checked,
                            txtNumeroIscrizioneRegistroGasFluorurati.Text,
                            DateTime.Now,
                            Convert.ToBoolean(Convert.ToInt16(rblPubblicazioneAlbo.SelectedItem.Value)),
                            Convert.ToBoolean(Convert.ToInt16(rblPrivacy.SelectedItem.Value)),
                            null,
                            DateTime.Now,
                            null,
                            DateTime.Now,
                            true,
                            coordinate,
                            bool.Parse(lblfIscrizione.Text),
                            null,
                            null,
                            string.Empty,
                            null,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            null,
                            null,
                            null,
                            null,
                            string.Empty,
                            null,
                            string.Empty,
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null,
                            null,
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null,
                            
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null
                            );
                    break;
                case "5": //Distributori di combustibile
                    iDSoggettoInsert = UtilitySoggetti.SaveInsertDeleteDatiSoggetti(
                            "update",
                            iDSoggetto,
                            string.Empty,
                            null,
                            iDTipoSoggetto,
                            txtDistributoriCombustibile.Text,
                            UtilityApp.ParseNullableInt(ddlFormaGiuridicaDistributoriCombustibile.SelectedItem.Value),
                            UtilityApp.ParseNullableInt(ddlPaeseSedeLegaleDistributoriCombustibile.SelectedItem.Value),
                            txtIndirizzoSedeLegaleDistributoriCombustibile.Text,
                            txtCapSedeLegaleDistributoriCombustibile.Text,
                            txtCittaSedeLegaleDistributoriCombustibile.Text,
                            txtCivicoSedeLegaleDistributoriCombustibile.Text,
                            UtilityApp.ParseNullableInt(ddlProvinciaSedeLegaleDistributoriCombustibile.SelectedItem.Value),
                            null,
                            null,
                            txtNomeDistributoriCombustibile.Text,
                            txtCognomeDistributoriCombustibile.Text,
                            UtilityApp.ParseNullableInt(ddlPaeseNascitaDistributoriCombustibile.SelectedItem.Value),
                            UtilityApp.ParseNullableDatetime(txtDataNascitaDistributoriCombustibile.Text),
                            txtCittaNascitaDistributoriCombustibile.Text,
                            UtilityApp.ParseNullableInt(ddlProvinciaNascitaDistributoriCombustibile.SelectedItem.Value),
                            UtilityApp.ParseNullableInt(ddlPaeseResidenzaDistributoriCombustibile.SelectedItem.Value),
                            txtCittaResidenzaDistributoriCombustibile.Text,
                            UtilityApp.ParseNullableInt(ddlProvinciaResidenzaDistributoriCombustibile.SelectedItem.Value),
                            txtCapResidenzaDistributoriCombustibile.Text,
                            txtIndirizzoResidenzaDistributoriCombustibile.Text,
                            txtNumeroCivicoResidenzaDistributoriCombustibile.Text,
                            txtCodiceFiscaleDistributoriCombustibile.Text,
                            txtTelefonoDistributoriCombustibile.Text,
                            txtFaxDistributoriCombustibile.Text,
                            txtEmailDistributoriCombustibile.Text,
                            txtEmailPecDistributoriCombustibile.Text,
                            txtPartitaIvaDistributoriCombustibile.Text,
                            txtCodiceFiscaleAziendaDistributoriCombustibile.Text,
                            txtSitoWebDistributoriCombustibile.Text,
                            txtNumeroAlboImpreseDistributoriCombustibile.Text,
                            UtilityApp.ParseNullableInt(ddlProvinciaAlboImpreseDistributoriCombustibile.SelectedItem.Value),
                            false,
                            string.Empty,
                            DateTime.Now,
                            false,
                            Convert.ToBoolean(Convert.ToInt16(rblPrivacyDistributoriCombustibile.SelectedItem.Value)),
                            null,
                            DateTime.Now,
                            null,
                            DateTime.Now,
                            true,
                            coordinate,
                            bool.Parse(lblfIscrizioneDistributoriCombustibile.Text),
                            null,
                            null,
                            string.Empty,
                            null,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            null,
                            null,
                            null,
                            null,
                            string.Empty,
                            null,
                            string.Empty,
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null,
                            null,
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null,
                            
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null
                            );
                    break;
                case "6": //Software house
                    iDSoggettoInsert = UtilitySoggetti.SaveInsertDeleteDatiSoggetti(
                           "update",
                           iDSoggetto,
                           string.Empty,
                           null,
                           iDTipoSoggetto,
                           txtSoftwareHouse.Text,
                           UtilityApp.ParseNullableInt(ddlFormaGiuridicaSoftwareHouse.SelectedItem.Value),
                           UtilityApp.ParseNullableInt(ddlPaeseSedeLegaleSoftwareHouse.SelectedItem.Value),
                           txtIndirizzoSedeLegaleSoftwareHouse.Text,
                           txtCapSedeLegaleSoftwareHouse.Text,
                           txtCittaSedeLegaleSoftwareHouse.Text,
                           txtCivicoSedeLegaleSoftwareHouse.Text,
                           UtilityApp.ParseNullableInt(ddlProvinciaSedeLegaleSoftwareHouse.SelectedItem.Value),
                           null,
                           null,
                           txtNomeSoftwareHouse.Text,
                           txtCognomeSoftwareHouse.Text,
                           null,
                           null,
                           string.Empty,
                           null,
                           null,
                           string.Empty,
                           null,
                           string.Empty,
                           string.Empty,
                           string.Empty,
                           string.Empty,
                           txtTelefonoSoftwareHouse.Text,
                           txtFaxSoftwareHouse.Text,
                           txtEmailSoftwareHouse.Text,
                           txtEmailPecSoftwareHouse.Text,
                           txtPartitaIvaSoftwareHouse.Text,
                           txtCodiceFiscaleSoftwareHouse.Text,
                           txtSitoWebSoftwareHouse.Text,
                           string.Empty,
                           null,
                           false,
                           string.Empty,
                           DateTime.Now,
                           false,
                           true,
                           null,
                           DateTime.Now,
                           null,
                           DateTime.Now,
                           true,
                           coordinate,
                           true,
                           null,
                           null,

                            string.Empty,
                            null,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            null,
                            null,
                            null,
                            null,
                            string.Empty,
                            null,
                            string.Empty,
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null,
                            null,
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null,
                            
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null
                           );
                    break;
                case "7": //Ispettori
                    iDSoggettoInsert = UtilitySoggetti.SaveInsertDeleteDatiSoggetti(
                       "update",                                                                          
                       iDSoggetto,                                                                        
                       lblCodiceSoggettoIspettore.Text,                                                    
                       null,                                                                               
                       int.Parse(IDTipoSoggetto),                                                          
                       txtAziendaIspettore.Text,                                                           
                       null,
                       UtilityApp.ParseNullableInt(ddlPaeseOrganizzazioneIspettore.SelectedItem.Value),    
                       txtIndirizzoOrganizzazioneIspettore.Text,                                           
                       txtCapOrganizzazioneIspettore.Text,                                                 
                       txtCittaOrganizzazioneIspettore.Text,                                               
                       txtNumeroCivicoOrganizzazioneIspettore.Text,                                        
                       UtilityApp.ParseNullableInt(ddlProvinciaOrganizzazioneIspettore.SelectedItem.Value),
                       UtilityApp.ParseNullableInt(ddlTitoloIspettore.SelectedItem.Value),             
                       null,                                                                           
                       txtNomeIspettore.Text,                                                          
                       txtCognomeIspettore.Text,                                                       
                       UtilityApp.ParseNullableInt(ddlPaeseNascitaIspettore.SelectedItem.Value),       
                       UtilityApp.ParseNullableDatetime(txtDataNascitaIspettore.Text),                 
                       txtCittaNascitaIspettore.Text,                                                  
                       UtilityApp.ParseNullableInt(ddlProvinciaNascitaIspettore.SelectedItem.Value),   
                       UtilityApp.ParseNullableInt(ddlPaeseResidenzaIspettore.SelectedItem.Value),
                       txtCittaResidenzaIspettore.Text,                                                
                       UtilityApp.ParseNullableInt(ddlProvinciaResidenzaIspettore.SelectedItem.Value), 
                       txtCapResidenzaIspettore.Text,                                                  
                       txtIndirizzoResidenzaIspettore.Text,                                            
                       txtNumeroCivicoResidenzaIspettore.Text,                                                            
                       txtCodiceFiscaleIspettore.Text,                                                 
                       txtTelefonoIspettore.Text,                                                                                                          
                       txtFaxOrganizzazioneIspettore.Text,                                             
                       txtEmailIspettore.Text,                                                         
                       txtEmailPecIspettore.Text,                                                      
                       txtPartitaIvaIspettore.Text,                                                    

                       txtCodiceFiscaleOrganizzazioneIspettore.Text,                                   
                       string.Empty,
                       string.Empty,
                       null,
                       chkIscrizioneRegistroGasFluoruratiIspettore.Checked,                            
                       txtNumeroIscrizioneRegistroGasFluoruratiIspettore.Text,                         
                       DateTime.Now,
                       null,
                       Convert.ToBoolean(Convert.ToInt16(rblPrivacyIspettore.SelectedItem.Value)),         
                       null,
                       DateTime.Now,
                       null,
                       DateTime.Now,
                       true,
                       coordinate,                                                                         
                       bool.Parse(lblfIscrizioneIspettore.Text),

                       chkDomicilio.Checked,                                                                
                       UtilityApp.ParseNullableInt(ddlPaeseDomicilioIspettore.SelectedItem.Value),
                       txtCittaDomicilioIspettore.Text,                                                    
                       UtilityApp.ParseNullableInt(ddlProvinciaDomicilioIspettore.SelectedItem.Value),     
                       txtCapDomicilioIspettore.Text,                                                      
                       txtIndirizzoDomicilioIspettore.Text,                                                
                       txtNumeroCivicoDomicilioIspettore.Text,                                             
                       txtCellulareIspettore.Text,                                                         
                       txtTelefonoOrganizzazioneIspettore.Text,                                            
                       txtEmailOrganizzazioneIspettore.Text,                                               
                       txtEmailPecOrganizzazioneIspettore.Text,                                            
                       txtPartitaIvaOrganizzazioneIspettore.Text,                                          
                       UtilityApp.ParseNullableInt(rblTipoQualificaIspettore.SelectedItem.Value),          
                       chkIdoneoIspezione.Checked,                                                         
                       chkNoCondanna.Checked,                                                              
                       UtilityApp.ParseNullableInt(ddlTipologiaTitoloStudio.SelectedItem.Value),           
                       txtTitoloStudioConseguitoPresso.Text,                                               
                       UtilityApp.ParseNullableDatetime(txtDataTitoloStudio.Text),                         
                       txtStageAzienda.Text,                                                               
                       UtilityApp.ParseNullableDatetime(txtDataStageAziendaDa.Text),                       
                       UtilityApp.ParseNullableDatetime(txtDataStageAziendaAl.Text),                       
                       txtCorsoFormazione.Text,                                                            
                       txtCorsoFormazioneOrganizzatore.Text,                                               
                       chkRequisitoOrganizzativo.Checked,                                                  
                       chkDisponibilitaApparecchiature.Checked,                                            
                       chkDisponibilitaControlli.Checked,                                                  
                       Convert.ToBoolean(Convert.ToInt16(rblOrganizzazioneIspettore.SelectedItem.Value)),  
                       txtOrganismoIspezioneNumeroIspettore.Text,                                          
                       txtOrganismoIspezioneIspettore.Text,                                                    
                       Convert.ToBoolean(Convert.ToInt16(rblConsensoRequisitiDichiarati.SelectedItem.Value)),  
                       
                       UtilityApp.ParseNullableInt(rblTipoInserimentoAziendaOrdineCollegio.SelectedItem.Value),
                       UtilityApp.ParseNullableInt(ddlOrdineCollegioProvincia.SelectedItem.Value),
                       txtOrdineCollegioSezione.Text,
                       txtOrdineCollegioNumero.Text,
                       UtilityApp.ParseNullableDatetime(txtOrdineCollegioData.Text)
                       );
                    break;
                case "9": //Ente locale
                    iDSoggettoInsert = UtilitySoggetti.SaveInsertDeleteDatiSoggetti(
                            "update",
                            iDSoggetto,
                            string.Empty,
                            null,
                            iDTipoSoggetto,
                            txtEnteLocale.Text,
                            UtilityApp.ParseNullableInt(ddlFormaGiuridicaEnteLocale.SelectedItem.Value),
                            116,
                            txtIndirizzoSedeLegaleEnteLocale.Text,
                            txtCapSedeLegaleEnteLocale.Text,
                            txtCittaSedeLegaleEnteLocale.Text,
                            txtNumeroCivicoSedeLegaleEnteLocale.Text,
                            UtilityApp.ParseNullableInt(ddlProvinciaSedeLegaleEnteLocale.SelectedItem.Value),
                            null,
                            UtilityApp.ParseNullableInt(ddlFunzioneLegaleRappresentanteEnteLocale.SelectedItem.Value),
                            txtNomeEnteLocale.Text,
                            txtCognomeEnteLocale.Text,
                            UtilityApp.ParseNullableInt(ddlPaeseNascitaEnteLocale.SelectedItem.Value),
                            UtilityApp.ParseNullableDatetime(txtDataNascitaEnteLocale.Text),
                            txtCittaNascitaEnteLocale.Text,
                            UtilityApp.ParseNullableInt(ddlProvinciaNascitaEnteLocale.SelectedItem.Value),
                            UtilityApp.ParseNullableInt(ddlPaeseResidenzaLegaleRappresentanteEnteLocale.SelectedItem.Value),
                            txtCittaResidenzaLegaleRappresentanteEnteLocale.Text,
                            UtilityApp.ParseNullableInt(ddlProvinciaResidenzaLegaleRappresentanteEnteLocale.SelectedItem.Value),
                            txtCapResidenzaLegaleRappresentanteEnteLocale.Text,
                            txtIndirizzoResidenzaLegaleRappresentanteEnteLocale.Text,
                            txtNumeroCivicoResidenzaLegaleRappresentanteEnteLocale.Text,
                            txtCodiceFiscaleLegaleRappresentanteEnteLocale.Text,
                            txtTelefonoEnteLocale.Text,
                            txtFaxEnteLocale.Text,
                            txtEmailEnteLocale.Text,
                            txtEmailPecEnteLocale.Text,
                            txtPartitaIvaEnteLocale.Text,
                            txtCodiceFiscaleEnteLocale.Text,
                            txtSitoWebEnteLocale.Text,
                            string.Empty,
                            null,
                            false,
                            string.Empty,
                            DateTime.Now,
                            false,
                            Convert.ToBoolean(Convert.ToInt16(rblPrivacyEnteLocale.SelectedItem.Value)),
                            null,
                            DateTime.Now,
                            null,
                            DateTime.Now,
                            true,
                            coordinate,
                            false,
                            null,
                            null,
                            string.Empty,
                            null,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            null,
                            null,
                            null,
                            null,
                            string.Empty,
                            null,
                            string.Empty,
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null,
                            null,
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null,
                                                        
                            null,
                            null,
                            string.Empty,
                            string.Empty,
                            null
                            );
                    break;
            }
        }

        return iDSoggettoInsert;
    }

    protected void btnProcessAzienda_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int? iDSoggetto = null;
            if (!string.IsNullOrWhiteSpace(IDSoggetto))
            {
                iDSoggetto = int.Parse(IDSoggetto);
                iDSoggetto = SaveIscrizione(iDSoggetto, int.Parse(IDTipoSoggetto));
                SaveProvinceCompetenza(iDSoggetto);
                SaveRuoliSoggetto(iDSoggetto);
                SaveClassificazioniImpianto(iDSoggetto);
                SaveAbilitazioniSoggetto(iDSoggetto);
            }
            else
            {
                iDSoggetto = SaveIscrizione(iDSoggetto, int.Parse(IDTipoSoggetto));
                SaveProvinceCompetenza(iDSoggetto);
                SaveRuoliSoggetto(iDSoggetto);
                SaveClassificazioniImpianto(iDSoggetto);
                SaveAbilitazioniSoggetto(iDSoggetto);

                using (CriterDataModel ctx = new CriterDataModel())
                {
                    var utenza = ctx.COM_Utenti.Where(c => c.IDSoggetto == iDSoggetto).ToList();

                    if (utenza.Count == 0)
                    {
                        lblMessageAzienda.Visible = true;
                        lblMessageAzienda.Text = "Azienda inserita correttamente";
                        SecurityManager.ActivateUserCredential(iDSoggetto, 2, 2, string.Empty, string.Empty, "insert");
                    }
                }
            }
            Logger.LogUser(TipoEvento.ModificaDati, UtilityApp.GetCurrentPageName(), HttpContext.Current.User.Identity.Name, HttpContext.Current.Request.UserHostAddress);
            RedirectPage(iDSoggetto.ToString(), IDTipoSoggetto, true);
        }
    }

    protected void btnProcessManutentore_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int? iDSoggetto = null;
            if (!string.IsNullOrWhiteSpace(IDSoggetto))
            {
                iDSoggetto = int.Parse(IDSoggetto);
                iDSoggetto = SaveIscrizione(iDSoggetto, int.Parse(IDTipoSoggetto));
            }
            else
            {
                iDSoggetto = SaveIscrizione(iDSoggetto, int.Parse(IDTipoSoggetto));
                if (IDTipoSoggetto == "1")
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Operatore/Addetto inserito correttamente";
                }
                else if (IDTipoSoggetto == "4")
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Responsabile tecnico inserito correttamente";
                }


                //using (CriterDataModel ctx = new CriterDataModel())
                //{
                //    var utenza = ctx.COM_Utenti.Where(c => c.IDSoggetto == iDSoggetto).ToList();

                //    if (utenza.Count == 0)
                //    {
                //        if (IDTipoSoggetto == "1")
                //        {
                //            lblMessage.Visible = true;
                //            lblMessage.Text = "Operatore/Addetto inserito correttamente";
                //            SecurityManager.ActivateUserCredential(iDSoggetto, 1, 3, string.Empty, string.Empty, "insert");
                //        }
                //        else if (IDTipoSoggetto == "4")
                //        {
                //            lblMessage.Visible = true;
                //            lblMessage.Text = "Responsabile tecnico inserito correttamente";
                //            SecurityManager.ActivateUserCredential(iDSoggetto, 1, 10, string.Empty, string.Empty, "insert");
                //        }
                //    }
                //}
            }

            if (chkAttivazioneUtenzaManutentore.Checked)
            {
                if (IDTipoSoggetto == "1")
                {
                    SecurityManager.ActivateUserCredential(iDSoggetto, 1, 3, string.Empty, string.Empty, "insert");
                }
                else if (IDTipoSoggetto == "4")
                {
                    SecurityManager.ActivateUserCredential(iDSoggetto, 1, 10, string.Empty, string.Empty, "insert");
                }
            }

            Logger.LogUser(TipoEvento.ModificaDati, UtilityApp.GetCurrentPageName(), HttpContext.Current.User.Identity.Name, HttpContext.Current.Request.UserHostAddress);
            RedirectPage(iDSoggetto.ToString(), IDTipoSoggetto, true);
        }
    }

    protected void btnProcessSoftwareHouse_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int? iDSoggetto = null;
            if (!string.IsNullOrWhiteSpace(IDSoggetto))
            {
                iDSoggetto = int.Parse(IDSoggetto);
                iDSoggetto = SaveIscrizione(iDSoggetto, int.Parse(IDTipoSoggetto));
            }
            else
            {
                iDSoggetto = SaveIscrizione(iDSoggetto, int.Parse(IDTipoSoggetto));

                using (CriterDataModel ctx = new CriterDataModel())
                {
                    var utenza = ctx.COM_Utenti.Where(c => c.IDSoggetto == iDSoggetto).ToList();

                    if (utenza.Count == 0)
                    {
                        lblMessageSoftwareHouse.Visible = true;
                        lblMessageSoftwareHouse.Text = "Software house inserita correttamente";
                        SecurityManager.ActivateUserCredential(iDSoggetto, 6, 11, txtEmailSoftwareHouse.Text, txtPartitaIvaSoftwareHouse.Text, "SoftwareHouse");
                    }
                }
            }
            Logger.LogUser(TipoEvento.ModificaDati, UtilityApp.GetCurrentPageName(), HttpContext.Current.User.Identity.Name, HttpContext.Current.Request.UserHostAddress);
            RedirectPage(iDSoggetto.ToString(), IDTipoSoggetto, true);
        }
    }

    protected void btnProcessDistributoriCombustibile_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int? iDSoggetto = null;
            if (!string.IsNullOrWhiteSpace(IDSoggetto))
            {
                iDSoggetto = int.Parse(IDSoggetto);
                iDSoggetto = SaveIscrizione(iDSoggetto, int.Parse(IDTipoSoggetto));
                SaveTipologiaDistributoriCombustibile(iDSoggetto);
            }
            else
            {
                iDSoggetto = SaveIscrizione(iDSoggetto, int.Parse(IDTipoSoggetto));

                using (CriterDataModel ctx = new CriterDataModel())
                {
                    var utenza = ctx.COM_Utenti.Where(c => c.IDSoggetto == iDSoggetto).ToList();

                    if (utenza.Count == 0)
                    {
                        lblMessageDistributoriCombustibile.Visible = true;
                        lblMessageDistributoriCombustibile.Text = "Distributore di combustibile inserito correttamente";
                        SecurityManager.ActivateUserCredential(iDSoggetto, 5, 4, string.Empty, string.Empty, "insert");
                    }
                }
            }
            Logger.LogUser(TipoEvento.ModificaDati, UtilityApp.GetCurrentPageName(), HttpContext.Current.User.Identity.Name, HttpContext.Current.Request.UserHostAddress);
            RedirectPage(iDSoggetto.ToString(), IDTipoSoggetto, true);
        }
    }

    protected void btnProcessEnteLocale_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int? iDSoggetto = null;
            if (!string.IsNullOrWhiteSpace(IDSoggetto))
            {
                iDSoggetto = int.Parse(IDSoggetto);
                iDSoggetto = SaveIscrizione(iDSoggetto, int.Parse(IDTipoSoggetto));
                SaveComuniCompetenza((int)iDSoggetto);
            }
            else
            {
                iDSoggetto = SaveIscrizione(iDSoggetto, int.Parse(IDTipoSoggetto));
                SaveComuniCompetenza((int)iDSoggetto);

                using (CriterDataModel ctx = new CriterDataModel())
                {
                    var utenza = ctx.COM_Utenti.Where(c => c.IDSoggetto == iDSoggetto).ToList();

                    if (utenza.Count == 0)
                    {
                        lblMessageEnteLocale.Visible = true;
                        lblMessageEnteLocale.Text = "Ente locale inserito correttamente";
                        SecurityManager.ActivateUserCredential(iDSoggetto, 9, 16, string.Empty, string.Empty, "insert");
                    }
                }
            }
            Logger.LogUser(TipoEvento.ModificaDati, UtilityApp.GetCurrentPageName(), HttpContext.Current.User.Identity.Name, HttpContext.Current.Request.UserHostAddress);
            RedirectPage(iDSoggetto.ToString(), IDTipoSoggetto, true);
        }
    }


    public void GetImportaDatiAzienda(int? iDSoggetto)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            if (iDSoggetto != null)
            {
                var soggetto = ctx.COM_AnagraficaSoggetti.Where(c => c.IDSoggetto == iDSoggetto).FirstOrDefault();

                if (soggetto != null)
                {
                    if (soggetto.IDPaeseSedeLegale != null)
                    {
                        ddlPaeseManutentore.SelectedValue = soggetto.IDPaeseSedeLegale.ToString();
                    }
                    
                    txtIndirizzoManutentore.Text = soggetto.IndirizzoSedeLegale;
                    txtCapManutentore.Text = soggetto.CapSedeLegale;
                    txtCittaManutentore.Text = soggetto.CittaSedeLegale;
                    txtNumeroCivicoManutentore.Text = soggetto.NumeroCivicoSedeLegale;
                    ddlProvinciaManutentore.SelectedValue = soggetto.IDProvinciaSedeLegale.ToString();
                    txtTelefonoManutentore.Text = soggetto.Telefono;
                    txtFaxManutentore.Text = soggetto.Fax;
                }
            }

        }
    }

    protected void imgImportDatiAzienda_Click(object sender, ImageClickEventArgs e)
    {
        if (ASPxComboBox1.Value != null)
        {
            int? iDSoggetto = UtilityApp.ParseNullableInt(ASPxComboBox1.Value.ToString());
            GetImportaDatiAzienda(iDSoggetto);
        }
    }

    protected void btnAnnulla_Click(object sender, EventArgs e)
    {
        RedirectSearchPage(IDSoggetto, IDTipoSoggetto);
    }

    #region Ispettore

    protected void ddlPaeseNascitaIspettore_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFieldPaeseNascitaIspettore();
    }

    public void VisibleHiddenFieldPaeseNascitaIspettore()
    {
        if (ddlPaeseNascitaIspettore.SelectedValue == "116")
        {
            lblProvinciaNascitaIspettore.Visible = true;
            ddlProvinciaNascitaIspettore.Visible = true;
            rfvddlProvinciaNascitaIspettore.Visible = true;
        }
        else
        {
            lblProvinciaNascitaIspettore.Visible = false;
            ddlProvinciaNascitaIspettore.Visible = false;
            ddlProvinciaNascitaIspettore.SelectedIndex = 0;
            rfvddlProvinciaNascitaIspettore.Visible = false;
        }
    }

    public void VisibleHiddenFieldPaeseResidenzaIspettore()
    {
        if (ddlPaeseResidenzaIspettore.SelectedValue == "116")
        {
            lblProvinciaResidenzaIspettore.Visible = true;
            ddlProvinciaResidenzaIspettore.Visible = true;
            rfvddlProvinciaResidenzaIspettore.Visible = true;
        }
        else
        {
            lblProvinciaResidenzaIspettore.Visible = false;
            ddlProvinciaResidenzaIspettore.Visible = false;
            ddlProvinciaResidenzaIspettore.SelectedIndex = 0;
            rfvddlProvinciaResidenzaIspettore.Visible = false;
        }
    }

    public void VisibleHiddenFieldPaeseDomicilioIspettore()
    {
        if (ddlPaeseDomicilioIspettore.SelectedValue == "116")
        {
            lblProvinciaDomicilioIspettore.Visible = true;
            ddlProvinciaDomicilioIspettore.Visible = true;
            rfvddlProvinciaDomicilioIspettore.Visible = true;
        }
        else
        {
            lblProvinciaDomicilioIspettore.Visible = false;
            ddlProvinciaDomicilioIspettore.Visible = false;
            ddlProvinciaDomicilioIspettore.SelectedIndex = 0;
            rfvddlProvinciaDomicilioIspettore.Visible = false;
        }
    }

    public void VisibleHiddenFieldPaeseOrganizzazioneIspettore()
    {
        if (ddlPaeseOrganizzazioneIspettore.SelectedValue == "116")
        {
            lblProvinciaOrganizzazioneIspettore.Visible = true;
            ddlProvinciaOrganizzazioneIspettore.Visible = true;
            rfvddlProvinciaOrganizzazioneIspettore.Visible = true;
        }
        else
        {
            lblProvinciaOrganizzazioneIspettore.Visible = false;
            ddlProvinciaOrganizzazioneIspettore.Visible = false;
            ddlProvinciaOrganizzazioneIspettore.SelectedIndex = 0;
            rfvddlProvinciaOrganizzazioneIspettore.Visible = false;
        }
    }

    protected void ddlPaeseOrganizzazioneIspettore_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFieldPaeseOrganizzazioneIspettore();
    }

    protected void chkDomicilio_CheckedChanged(object sender, EventArgs e)
    {
        SetVisibleHiddenDomicilio(chkDomicilio.Checked);
        ResetDomicilio(chkDomicilio.Checked);
    }

    protected void SetVisibleHiddenDomicilio(bool fDomicilio)
    {
        if (fDomicilio)
        {
            rowDomicilio00.Visible = true;
            rowDomicilio0.Visible = true;
            rowDomicilio1.Visible = true;
        }
        else
        {
            rowDomicilio00.Visible = false;
            rowDomicilio0.Visible = false;
            rowDomicilio1.Visible = false;
        }
    }

    protected void ResetDomicilio(bool fDomicilio)
    {
        if (!fDomicilio)
        {
            ddlPaeseDomicilioIspettore.SelectedIndex = 0;
            txtIndirizzoDomicilioIspettore.Text = string.Empty;
            txtNumeroCivicoDomicilioIspettore.Text = string.Empty;
            txtCapDomicilioIspettore.Text = string.Empty;
            txtCittaDomicilioIspettore.Text = string.Empty;
            ddlProvinciaDomicilioIspettore.SelectedIndex = 0;
        }
    }

    protected void rblTipoQualificaIspettore_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetVisibleHiddenTipoQualificaIspettore(rblTipoQualificaIspettore.SelectedValue);
    }

    protected void SetVisibleHiddenTipoQualificaIspettore(string IDTipoQualificaIspettore)
    {
        if (IDTipoQualificaIspettore == "1")
        {
            rowIscrizioneRegistroGasFluoruratiIspettore.Visible = false;
            txtNumeroIscrizioneRegistroGasFluoruratiIspettore.Text = string.Empty;
            chkIscrizioneRegistroGasFluoruratiIspettore.Checked = false;
        }
        else if (IDTipoQualificaIspettore == "2")
        {
            rowIscrizioneRegistroGasFluoruratiIspettore.Visible = true;
        }
    }

    protected void SetVisibleHiddenOrganizzazioneIspettore(bool fOrganizzazioneEsterna)
    {
        if (fOrganizzazioneEsterna)
        {
            lblPartitaIvaIspettore.Text = "Partita Iva (*)";
            txtPartitaIvaIspettore.CssClass = "txtClass_o";
            rfvPartitaIvaIspettore.Enabled = true;

            rowOrganizzazioneIspettore.Visible = true;
            rowOrganizzazioneIspettore0.Visible = true;
            rowOrganizzazioneIspettore1.Visible = true;
            rowOrganizzazioneIspettore2.Visible = true;
            rowOrganizzazioneIspettore3.Visible = true;
            rowOrganizzazioneIspettore4.Visible = true;
            rowOrganizzazioneIspettore5.Visible = true;
            rowOrganizzazioneIspettore6.Visible = true;
            rowOrganizzazioneIspettore7.Visible = true;
        }
        else
        {
            lblPartitaIvaIspettore.Text = "Partita Iva";
            txtPartitaIvaIspettore.CssClass = "txtClass";
            rfvPartitaIvaIspettore.Enabled = false;

            rowOrganizzazioneIspettore.Visible = false;
            rowOrganizzazioneIspettore0.Visible = false;
            rowOrganizzazioneIspettore1.Visible = false;
            rowOrganizzazioneIspettore2.Visible = false;
            rowOrganizzazioneIspettore3.Visible = false;
            rowOrganizzazioneIspettore4.Visible = false;
            rowOrganizzazioneIspettore5.Visible = false;
            rowOrganizzazioneIspettore6.Visible = false;
            rowOrganizzazioneIspettore7.Visible = false;
        }
    }

    public void ResetOrganizzazioneIspettore(bool fOrganizzazioneEsterna)
    {
        if (!fOrganizzazioneEsterna)
        {
            txtAziendaIspettore.Text = string.Empty;
            ddlPaeseOrganizzazioneIspettore.SelectedIndex = 0;
            txtIndirizzoOrganizzazioneIspettore.Text = string.Empty;
            txtNumeroCivicoOrganizzazioneIspettore.Text = string.Empty;
            txtCapOrganizzazioneIspettore.Text = string.Empty;
            txtCittaOrganizzazioneIspettore.Text = string.Empty;
            ddlProvinciaOrganizzazioneIspettore.SelectedIndex = 0;
            txtPartitaIvaOrganizzazioneIspettore.Text = string.Empty;
            txtCodiceFiscaleOrganizzazioneIspettore.Text = string.Empty;
            txtTelefonoOrganizzazioneIspettore.Text = string.Empty;
            txtEmailOrganizzazioneIspettore.Text = string.Empty;
            txtEmailPecOrganizzazioneIspettore.Text = string.Empty;
            txtOrganismoIspezioneIspettore.Text = string.Empty;
        }
    }

    protected void rblOrganizzazioneIspettore_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetVisibleHiddenOrganizzazioneIspettore(Convert.ToBoolean(Convert.ToInt16(rblOrganizzazioneIspettore.SelectedItem.Value)));
        ResetOrganizzazioneIspettore(Convert.ToBoolean(Convert.ToInt16(rblOrganizzazioneIspettore.SelectedItem.Value)));
    }

    protected void chkIdoneoIspezione_CheckedChanged(object sender, EventArgs e)
    {
        SetVisibleHiddenIdoneoIspezione(chkIdoneoIspezione.Checked);
    }

    protected void SetVisibleHiddenIdoneoIspezione(bool fIdoneo)
    {
        if (fIdoneo)
        {
            rowTitoloStudio.Visible = false;
            rowInserimentoPressoAzienda.Visible = false;
            rowCorsoFormazione.Visible = false;

            ddlTipologiaTitoloStudio.SelectedIndex = 0;
            txtTitoloStudioConseguitoPresso.Text = string.Empty;
            txtDataTitoloStudio.Text = string.Empty;

            txtStageAzienda.Text = string.Empty;
            txtDataStageAziendaDa.Text = string.Empty;
            txtDataStageAziendaAl.Text = string.Empty;

            txtCorsoFormazione.Text = string.Empty;
            txtCorsoFormazioneOrganizzatore.Text = string.Empty;
        }
        else
        {
            rowTitoloStudio.Visible = true;
            rowInserimentoPressoAzienda.Visible = true;
            rowCorsoFormazione.Visible = true;
        }
    }

    protected void btnProcessIspettore_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int? iDSoggetto = null;
            if (!string.IsNullOrWhiteSpace(IDSoggetto))
            {
                iDSoggetto = int.Parse(IDSoggetto);
                iDSoggetto = SaveIscrizione(iDSoggetto, int.Parse(IDTipoSoggetto));
            }
            else
            {
                iDSoggetto = SaveIscrizione(iDSoggetto, int.Parse(IDTipoSoggetto));
            }
            RedirectPage(iDSoggetto.ToString(), IDTipoSoggetto, true);
        }
    }

    public void SetVisibleHiddenTipologiaTitoloStudio(string iDTipologiaTitoloStudio, string iDTipologiaOrdineCollegio)
    {
        if (iDTipologiaOrdineCollegio == "1")
        {
            if ((iDTipologiaTitoloStudio == "43")
            ||
            (iDTipologiaTitoloStudio == "44")
            ||
            (iDTipologiaTitoloStudio == "45")
            ||
            (iDTipologiaTitoloStudio == "46")
            ||
            (iDTipologiaTitoloStudio == "47")
            ||
            (iDTipologiaTitoloStudio == "48")
            ||
            (iDTipologiaTitoloStudio == "49")
            ||
            (iDTipologiaTitoloStudio == "50")
            ||
            (iDTipologiaTitoloStudio == "51")
            ||
            (iDTipologiaTitoloStudio == "52")
            ||
            (iDTipologiaTitoloStudio == "53")
            ||
            (iDTipologiaTitoloStudio == "54")
            ||
            (iDTipologiaTitoloStudio == "55")
            )
            {
                rowInserimentoAziendaOrdineCollegio.Visible = true;
                rowInserimentoPressoAzienda.Visible = true;
                rowOrdineCollegio0.Visible = false;
                rowOrdineCollegio1.Visible = false;

                ddlOrdineCollegioProvincia.SelectedIndex = 0;
                txtOrdineCollegioSezione.Text = string.Empty;
                txtOrdineCollegioNumero.Text = string.Empty;
                txtOrdineCollegioData.Text = string.Empty;
            }
            else
            {
                rowInserimentoAziendaOrdineCollegio.Visible = false;

                rblTipoInserimentoAziendaOrdineCollegio.SelectedIndex = 0;

                rowInserimentoPressoAzienda.Visible = false;
                rowOrdineCollegio0.Visible = false;
                rowOrdineCollegio1.Visible = false;

                txtStageAzienda.Text = string.Empty;
                txtDataStageAziendaDa.Text = string.Empty;
                txtDataStageAziendaAl.Text = string.Empty;
            }
        }
        else if (iDTipologiaOrdineCollegio == "2")
        {
            if ((iDTipologiaTitoloStudio == "43")
                ||
                (iDTipologiaTitoloStudio == "44")
                ||
                (iDTipologiaTitoloStudio == "45")
                ||
                (iDTipologiaTitoloStudio == "46")
                ||
                (iDTipologiaTitoloStudio == "47")
                ||
                (iDTipologiaTitoloStudio == "48")
                ||
                (iDTipologiaTitoloStudio == "49")
                ||
                (iDTipologiaTitoloStudio == "50")
                ||
                (iDTipologiaTitoloStudio == "51")
                ||
                (iDTipologiaTitoloStudio == "52")
                ||
                (iDTipologiaTitoloStudio == "53")
                ||
                (iDTipologiaTitoloStudio == "54")
                ||
                (iDTipologiaTitoloStudio == "55")
                )
            {
                rowInserimentoAziendaOrdineCollegio.Visible = true;
                rowInserimentoPressoAzienda.Visible = false;
                rowOrdineCollegio0.Visible = true;
                rowOrdineCollegio1.Visible = true;

                txtStageAzienda.Text = string.Empty;
                txtDataStageAziendaDa.Text = string.Empty;
                txtDataStageAziendaAl.Text = string.Empty;
            }
            else
            {
                rowInserimentoAziendaOrdineCollegio.Visible = false;
                rblTipoInserimentoAziendaOrdineCollegio.SelectedIndex = 0;

                rowOrdineCollegio0.Visible = false;
                rowOrdineCollegio1.Visible = false;

                ddlOrdineCollegioProvincia.SelectedIndex = 0;
                txtOrdineCollegioSezione.Text = string.Empty;
                txtOrdineCollegioNumero.Text = string.Empty;
                txtOrdineCollegioData.Text = string.Empty;
            }
        }
    }

    protected void ddlTipologiaTitoloStudio_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetVisibleHiddenTipologiaTitoloStudio(ddlTipologiaTitoloStudio.SelectedItem.Value, rblTipoInserimentoAziendaOrdineCollegio.SelectedItem.Value);
    }

    protected void rblTipoInserimentoAziendaOrdineCollegio_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetVisibleHiddenTipologiaTitoloStudio(ddlTipologiaTitoloStudio.SelectedItem.Value, rblTipoInserimentoAziendaOrdineCollegio.SelectedItem.Value);
    }

    protected void ddlPaeseDomicilioIspettore_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFieldPaeseDomicilioIspettore();
    }

    protected void ddlPaeseResidenzaIspettore_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFieldPaeseResidenzaIspettore();
    }

    #endregion

    #region Distributori di Combustibile
    protected void ddlPaeseSedeLegaleDistributoriCombustibile_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFieldPaeseSedeLegaleDistributoriCombustibile();
    }

    public void VisibleHiddenFieldPaeseSedeLegaleDistributoriCombustibile()
    {
        if (ddlPaeseSedeLegaleDistributoriCombustibile.SelectedValue == "116")
        {
            lblTitoloddlProvinciaSedeLegaleDistributoriCombustibile.Visible = true;
            ddlProvinciaSedeLegaleDistributoriCombustibile.Visible = true;
            rfvddlProvinciaSedeLegaleDistributoriCombustibile.Visible = true;
        }
        else
        {
            lblTitoloddlProvinciaSedeLegaleDistributoriCombustibile.Visible = false;
            ddlProvinciaSedeLegaleDistributoriCombustibile.Visible = false;
            ddlProvinciaSedeLegaleDistributoriCombustibile.SelectedIndex = 0;
            rfvddlProvinciaSedeLegaleDistributoriCombustibile.Visible = false;
        }
    }

    protected void ddlPaeseNascitaDistributoriCombustibile_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFieldPaeseNascitaDistributoriCombustibile();
    }

    public void VisibleHiddenFieldPaeseNascitaDistributoriCombustibile()
    {
        if (ddlPaeseNascitaDistributoriCombustibile.SelectedValue == "116")
        {
            lblProvinciaNascitaDistributoriCombustibile.Visible = true;
            ddlProvinciaNascitaDistributoriCombustibile.Visible = true;
            rfvddlProvinciaNascitaDistributoriCombustibile.Visible = true;
        }
        else
        {
            lblProvinciaNascitaDistributoriCombustibile.Visible = false;
            ddlProvinciaNascitaDistributoriCombustibile.Visible = false;
            ddlProvinciaNascitaDistributoriCombustibile.SelectedIndex = 0;
            rfvddlProvinciaNascitaDistributoriCombustibile.Visible = false;
        }
    }

    protected void ddlPaeseResidenzaDistributoriCombustibile_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFieldPaeseResidenzaDistributoriCombustibile();
    }

    public void VisibleHiddenFieldPaeseResidenzaDistributoriCombustibile()
    {
        if (ddlPaeseResidenzaDistributoriCombustibile.SelectedValue == "116")
        {
            lblProvinciaResidenzaDistributoriCombustibile.Visible = true;
            ddlProvinciaResidenzaDistributoriCombustibile.Visible = true;
            rfvddlProvinciaResidenzaDistributoriCombustibile.Visible = true;
        }
        else
        {
            lblProvinciaResidenzaDistributoriCombustibile.Visible = false;
            ddlProvinciaResidenzaDistributoriCombustibile.Visible = false;
            ddlProvinciaResidenzaDistributoriCombustibile.SelectedIndex = 0;
            rfvddlProvinciaResidenzaDistributoriCombustibile.Visible = false;
        }
    }

    protected void cblTipiDistributoriCombustibile_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTipiDistributoriCombustibileAltro();
    }

    protected void VisibleTipiDistributoriCombustibileAltro()
    {
        if (cblTipiDistributoriCombustibile.SelectedItem != null)
        {
            foreach (ListItem item in cblTipiDistributoriCombustibile.Items)
            {
                if ((item.Selected) && (item.Value == "1"))
                {
                    pnlTipiDistributoriCombustibileAltro.Visible = true;
                }
                else if ((!item.Selected) && (item.Value == "1"))
                {
                    pnlTipiDistributoriCombustibileAltro.Visible = false;
                    txtTipiDistributoriCombustibileAltro.Text = "";
                }
            }
        }
        else
        {
            pnlTipiDistributoriCombustibileAltro.Visible = false;
            txtTipiDistributoriCombustibileAltro.Text = "";
        }
    }

    public void SaveTipologiaDistributoriCombustibile(int? iDSoggetto)
    {
        List<string> valoriTipologiaDistributoriCombustibileVettore = new List<string>();
        foreach (ListItem item in cblTipiDistributoriCombustibile.Items)
        {
            if (item.Selected)
            {
                valoriTipologiaDistributoriCombustibileVettore.Add(item.Value);
            }
        }
        UtilitySoggetti.SaveInsertDeleteDatiTipologiaDistributoriCombustibile(int.Parse(iDSoggetto.ToString()), txtTipiDistributoriCombustibileAltro.Text, valoriTipologiaDistributoriCombustibileVettore.ToArray<string>());
    }

    #endregion

    #region Ente locale
    protected void ddlPaeseNascitaEnteLocale_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFieldPaeseNascitaEnteLocale();
    }

    protected void ddlPaeseResidenzaLegaleRappresentanteEnteLocale_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFieldPaeseResidenzaEnteLocale();
    }

    public void VisibleHiddenFieldPaeseNascitaEnteLocale()
    {
        if (ddlPaeseNascitaEnteLocale.SelectedValue == "116")
        {
            lblProvinciaNascitaEnteLocale.Visible = true;
            ddlProvinciaNascitaEnteLocale.Visible = true;
            rfvddlProvinciaNascitaEnteLocale.Visible = true;
        }
        else
        {
            lblProvinciaNascitaEnteLocale.Visible = false;
            ddlProvinciaNascitaEnteLocale.Visible = false;
            ddlProvinciaNascitaEnteLocale.SelectedIndex = 0;
            rfvddlProvinciaNascitaEnteLocale.Visible = false;
        }
    }

    public void VisibleHiddenFieldPaeseResidenzaEnteLocale()
    {
        if (ddlPaeseResidenzaLegaleRappresentanteEnteLocale.SelectedValue == "116")
        {
            lblProvinciaResidenzaLegaleRappresentanteEnteLocale.Visible = true;
            ddlProvinciaResidenzaLegaleRappresentanteEnteLocale.Visible = true;
            rfvddlProvinciaResidenzaLegaleRappresentanteEnteLocale.Visible = true;
        }
        else
        {
            lblProvinciaResidenzaLegaleRappresentanteEnteLocale.Visible = false;
            ddlProvinciaResidenzaLegaleRappresentanteEnteLocale.Visible = false;
            ddlProvinciaResidenzaLegaleRappresentanteEnteLocale.SelectedIndex = 0;
            rfvddlProvinciaResidenzaLegaleRappresentanteEnteLocale.Visible = false;
        }
    }
    
    protected void AggiornaComuni(ASPxGridView grid)
    {
        lblComuniSelezionati.Text = string.Empty;
        string sComuni = string.Empty;
        var comuniSelezionati = grid.GetSelectedFieldValues("Comune");
        foreach (var item in comuniSelezionati)
        {
            sComuni += item.ToString() + ",";
        }

        if (sComuni.Length > 0)
        {
            sComuni = sComuni.Substring(0, sComuni.Length - 1);
        }

        lblComuniSelezionati.Text = sComuni;
    }

    protected void gridAreaTerritorialeEnteLocale_SelectionChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        AggiornaComuni(grid);
    }

    public void GetDatiUserComuniCompetenza(int iDSoggetto)
    {
        var result = UtilitySoggetti.GetValoriUsersComuniCompetenza(iDSoggetto);
                
        lblComuniSelezionati.Text = string.Empty;
        string sComuni = string.Empty;
        
        foreach (var row in result)
        {
            gridAreaTerritorialeEnteLocale.Selection.SelectRowByKey(row.IDCodiceCatastale);
            sComuni += row.SYS_CodiciCatastali.Comune + ",";
        }

        if (sComuni.Length > 0)
        {
            sComuni = sComuni.Substring(0, sComuni.Length - 1);
        }

        lblComuniSelezionati.Text = sComuni;
    }

    protected void SaveComuniCompetenza(int iDSoggetto)
    {
        var comuniSelezionati = gridAreaTerritorialeEnteLocale.GetSelectedFieldValues("IDCodiceCatastale");
        UtilitySoggetti.SaveInsertDeleteDatiComuniCompetenza(iDSoggetto, comuniSelezionati);
    }

    public void ControllaComuneCompetenza(Object sender, ServerValidateEventArgs e)
    {
        int counter = 0;

        for (int i = 0; i < gridAreaTerritorialeEnteLocale.VisibleRowCount; i++)
        {
            if (gridAreaTerritorialeEnteLocale.Selection.IsRowSelected(i))
            {
                counter++;
            }

            e.IsValid = (counter == 0) ? false : true;
        }
    }

    public void ControllaEmailEnteLocalePresente(Object sender, ServerValidateEventArgs e)
    {
        bool fEmail = UtilitySoggetti.CheckfEmail(txtEmailEnteLocale.Text, int.Parse(IDSoggetto), int.Parse(IDTipoSoggetto));
        if (fEmail)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaEmailPecEnteLocalePresente(Object sender, ServerValidateEventArgs e)
    {
        bool fEmail = UtilitySoggetti.CheckfEmailPec(txtEmailPecEnteLocale.Text, int.Parse(IDSoggetto), int.Parse(IDTipoSoggetto));
        if (fEmail)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ControllaPartitaIvaEnteLocalePresente(Object sender, ServerValidateEventArgs e)
    {
        bool fEsiste = UtilitySoggetti.CheckfPartitaIva(txtPartitaIvaEnteLocale.Text, int.Parse(IDSoggetto), int.Parse(IDTipoSoggetto));
        if (fEsiste)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }

    #endregion


}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataUtilityCore;
using EncryptionQS;
using DevExpress.Web;

public partial class Iscrizione : Page
{
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

    protected string fSpid
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

            return "";
        }
    }

    protected string codicefiscale
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
                        if (qsdec[2] != null)
                        {
                            return (string)qsdec[2];
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

    protected string streemKey
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
                        if (qsdec[3] != null)
                        {
                            return (string)qsdec[3];
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
        if (!Page.IsPostBack)
        {
            InfoDefault();
        }
    }

    public void InfoDefault()
    {
        switch (IDTipoSoggetto)
        {
            case "1": //Persona

                break;
            case "2": //Impresa
            case "3": //Terzo responsabile
                ddFormaGiuridica(null, ddlFormaGiuridica, int.Parse(IDTipoSoggetto));
                ddPaeseSoggetto(null, ddlPaeseNascita);
                ddPaeseSoggetto(null, ddlPaeseSedeLegale);
                ddPaeseSoggetto(null, ddlPaeseResidenza);
                ddProvincia(null, ddlProvinciaSedeLegale);
                ddProvincia(null, ddlProvinciaNascita);
                ddProvincia(null, ddlProvinciaResidenza);
                ddTitoloSoggetto(null, ddlTitoloLegaleRappresentante);
                ddFunzioneSoggetto(null, ddlFunzioneLegaleRappresentante, int.Parse(IDTipoSoggetto));
                ddProvincia(null, ddlProvinciaAlboImprese);
                cbProvinceCompetenza(null);
                cbAbilitazioniSoggetti(null);
                cbRuoliSoggetto();
                cbClassificazioniImpianto();

                tblInfoAzienda.Visible = true;
                //rowParix.Visible = bool.Parse(ConfigurationManager.AppSettings["ParixAbilitato"]);
                ddlPaeseSedeLegale.SelectedValue = "116";
                ddlPaeseNascita.SelectedValue = "116";
                ddlPaeseResidenza.SelectedValue = "116";
                if (IDTipoSoggetto == "3")
                {
                    cblRuoliSoggetto.Enabled = false;
                    cblRuoliSoggetto.Items.FindByValue("3").Selected = true;
                }
                else
                {
                    cblRuoliSoggetto.Enabled = true;
                }

                bool fSpidCheck = false;
                if (bool.TryParse(fSpid, out fSpidCheck))
                {
                    if (fSpidCheck)
                    {
                        txtCodiceFiscale.Text = codicefiscale;
                        txtCodiceFiscale.Enabled = false;
                    }
                }
                VisibleHiddenFieldPaeseSedeLegale();
                VisibleHiddenFieldPaeseNascita();
                VisibleHiddenFieldPaeseResidenza();
                break;
            case "4": //Persona Responsabile tecnico

                break;
            case "5": //Distributori di combustibile
                tblInfoDistributoriCombustibile.Visible = true;

                ddFormaGiuridica(null, ddlFormaGiuridicaDistributoriCombustibile, int.Parse(IDTipoSoggetto));
                ddPaeseSoggetto(null, ddlPaeseNascitaDistributoriCombustibile);
                ddPaeseSoggetto(null, ddlPaeseSedeLegaleDistributoriCombustibile);
                ddPaeseSoggetto(null, ddlPaeseResidenzaDistributoriCombustibile);
                ddProvincia(null, ddlProvinciaSedeLegaleDistributoriCombustibile);
                ddProvincia(null, ddlProvinciaNascitaDistributoriCombustibile);
                ddProvincia(null, ddlProvinciaResidenzaDistributoriCombustibile);
                ddProvincia(null, ddlProvinciaAlboImpreseDistributoriCombustibile);
                cbTipologiaDistributoriCombustibile();

                ddlPaeseSedeLegaleDistributoriCombustibile.SelectedValue = "116";
                ddlPaeseNascitaDistributoriCombustibile.SelectedValue = "116";
                ddlPaeseResidenzaDistributoriCombustibile.SelectedValue = "116";

                VisibleHiddenFieldPaeseSedeLegale();
                VisibleHiddenFieldPaeseNascita();
                VisibleHiddenFieldPaeseResidenza();
                break;
            case "6": //Software house

                break;
            case "7": //Ispettori
                tblInfoIspettore.Visible = true;

                ddTitoloSoggetto(null, ddlTitoloIspettore);
                ddPaeseSoggetto(null, ddlPaeseNascitaIspettore);
                ddProvincia(null, ddlProvinciaNascitaIspettore);
                ddProvincia(null, ddlProvinciaResidenzaIspettore);
                ddProvincia(null, ddlProvinciaDomicilioIspettore);
                ddProvincia(null, ddlOrdineCollegioProvincia);
                rbQualificheIspettori(null);
                rbTipologiaOrdineCollegio(null);
                ddTipologiaTitoloStudio(null);
                ddPaeseSoggetto(null, ddlPaeseOrganizzazioneIspettore);
                ddProvincia(null, ddlProvinciaOrganizzazioneIspettore);
                ddPaeseSoggetto(null, ddlPaeseResidenzaIspettore);
                ddPaeseSoggetto(null, ddlPaeseDomicilioIspettore);

                ddlPaeseNascitaIspettore.SelectedValue = "116";
                ddlPaeseResidenzaIspettore.SelectedValue = "116";
                ddlPaeseDomicilioIspettore.SelectedValue = "116";

                VisibleHiddenFieldPaeseOrganizzazioneIspettore();
                VisibleHiddenFieldPaeseNascitaIspettore();
                VisibleHiddenFieldPaeseResidenzaIspettore();
                VisibleHiddenFieldPaeseDomicilioIspettore();

                SetVisibleHiddenDomicilio(chkDomicilio.Checked);
                SetVisibleHiddenTipoQualificaIspettore(rblTipoQualificaIspettore.SelectedValue);
                SetVisibleHiddenIscrizioneRegistroGasFluorurati(chkIscrizioneRegistroGasFluoruratiIspettore.Checked);
                SetVisibleHiddenOrganizzazioneIspettore(Convert.ToBoolean(Convert.ToInt16(rblOrganizzazioneIspettore.SelectedItem.Value)));
                SetVisibleHiddenIdoneoIspezione(chkIdoneoIspezione.Checked);
                SetVisibleHiddenTipologiaTitoloStudio(ddlTipologiaTitoloStudio.SelectedItem.Value, rblTipoInserimentoAziendaOrdineCollegio.SelectedItem.Value);
                break;
            case "9": //Ente locale
                tblInfoEnteLocale.Visible = true;

                ddFormaGiuridica(null, ddlFormaGiuridicaEnteLocale, int.Parse(IDTipoSoggetto));
                ddFunzioneSoggetto(null, ddlFunzioneLegaleRappresentanteEnteLocale, int.Parse(IDTipoSoggetto));
                ddProvincia(null, ddlProvinciaSedeLegaleEnteLocale);
                ddProvincia(null, ddlProvinciaNascitaEnteLocale);
                ddProvincia(null, ddlProvinciaResidenzaLegaleRappresentanteEnteLocale);
                ddPaeseSoggetto(null, ddlPaeseNascitaEnteLocale);
                ddPaeseSoggetto(null, ddlPaeseResidenzaLegaleRappresentanteEnteLocale);

                ddlPaeseNascitaEnteLocale.SelectedValue = "116";
                ddlPaeseResidenzaLegaleRappresentanteEnteLocale.SelectedValue = "116";

                VisibleHiddenFieldPaeseNascitaEnteLocale();
                VisibleHiddenFieldPaeseResidenzaEnteLocale();
                break;
        }
    }

    protected void ddlPaeseSedeLegale_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFieldPaeseSedeLegale();
    }

    protected void ddlPaeseNascita_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFieldPaeseNascita();
    }

    protected void ddlPaeseNascitaIspettore_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFieldPaeseNascitaIspettore();
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

    #region DROPDOWNLIST / RADIOBUTTONLIST / CHECKBOXLIST
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

    protected void ddFunzioneSoggetto(int? IDPresel, DropDownList ddlFunzioneLegaleRappresentante, int iDTipoSoggetto)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_FunzioniSoggetti(IDPresel, iDTipoSoggetto);
        ddlFunzioneLegaleRappresentante.DataValueField = "IDFunzioneSoggetto";
        ddlFunzioneLegaleRappresentante.DataTextField = "FunzioneSoggetto";
        ddlFunzioneLegaleRappresentante.DataSource = ls;
        ddlFunzioneLegaleRappresentante.DataBind();

        ListItem myItem = new ListItem("-- Selezionare --", "0");
        ddlFunzioneLegaleRappresentante.Items.Insert(0, myItem);
        ddlFunzioneLegaleRappresentante.SelectedIndex = 0;
    }

    protected void cbProvinceCompetenza(int? IDPresel)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_Province(IDPresel, true);
        cblProvinceCompetenza.DataValueField = "IDProvincia";
        cblProvinceCompetenza.DataTextField = "Provincia";
        cblProvinceCompetenza.DataSource = ls;
        cblProvinceCompetenza.DataBind();
    }

    protected void cbAbilitazioniSoggetti(int? IDPresel)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_AbilitazioneSoggetto(IDPresel);
        cblAbilitazioniSoggetto.DataValueField = "IDAbilitazioneSoggetto";
        cblAbilitazioniSoggetto.DataTextField = "AbilitazioneSoggetto";
        cblAbilitazioniSoggetto.DataSource = ls;
        cblAbilitazioniSoggetto.DataBind();
    }

    protected void cbRuoliSoggetto()
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_RuoloSoggetto(null);
        cblRuoliSoggetto.DataValueField = "IDRuoloSoggetto";
        cblRuoliSoggetto.DataTextField = "RuoloSoggetto";
        cblRuoliSoggetto.DataSource = ls;
        cblRuoliSoggetto.DataBind();
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

    protected void rbTipologiaOrdineCollegio(int? IDPresel)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_TipologiaOrdineCollegio(IDPresel);
        rblTipoInserimentoAziendaOrdineCollegio.DataValueField = "IDTipologiaOrdineCollegio";
        rblTipoInserimentoAziendaOrdineCollegio.DataTextField = "TipologiaOrdineCollegio";
        rblTipoInserimentoAziendaOrdineCollegio.DataSource = ls;
        rblTipoInserimentoAziendaOrdineCollegio.DataBind();

        rblTipoInserimentoAziendaOrdineCollegio.SelectedIndex = 0;
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
        bool fCodFis = CodiceFiscale.ControlloFormale(txtCodiceFiscale.Text);
        if (!fCodFis)
        {
            e.IsValid = false;
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
        bool fEmail = UtilitySoggetti.CheckfEmail(txtEmail.Text, null, 2);
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
        bool fEmail = UtilitySoggetti.CheckfEmailPec(txtEmailPec.Text, null, 2);
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
        bool fEsiste = UtilitySoggetti.CheckfPartitaIva(txtPartitaIva.Text, null, 2);
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
        bool fEmail = UtilitySoggetti.CheckfEmail(txtEmailIspettore.Text, null, 7);
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
        bool fEmail = UtilitySoggetti.CheckfEmailPec(txtEmailPecIspettore.Text, null, 7);
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
        bool fEmail = UtilitySoggetti.CheckfEmail(txtEmailDistributoriCombustibile.Text, null, 5);
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
        bool fEmail = UtilitySoggetti.CheckfEmailPec(txtEmailPecDistributoriCombustibile.Text, null, 5);
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
        bool fEsiste = UtilitySoggetti.CheckfPartitaIva(txtPartitaIvaDistributoriCombustibile.Text, null, 5);
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

    protected void SaveProvinceCompetenza(int iDSoggetto)
    {
        List<string> valoriProvinceCompetenza = new List<string>();
        foreach (ListItem item in cblProvinceCompetenza.Items)
        {
            if (item.Selected)
            {
                valoriProvinceCompetenza.Add(item.Value);
            }
        }
        UtilitySoggetti.SaveInsertDeleteDatiProvinceCompetenza(iDSoggetto, valoriProvinceCompetenza.ToArray<string>());
    }

    protected void SaveRuoliSoggetto(int iDSoggetto)
    {
        List<string> valoriRuoliSoggetto = new List<string>();
        foreach (ListItem item in cblRuoliSoggetto.Items)
        {
            if (item.Selected)
            {
                valoriRuoliSoggetto.Add(item.Value);
            }
        }
        UtilitySoggetti.SaveInsertDeleteDatiRuoliSoggetto(iDSoggetto, valoriRuoliSoggetto.ToArray<string>());
    }

    protected void SaveAbilitazioniSoggetto(int iDSoggetto)
    {
        List<string> valoriAbilitazioniSoggetto = new List<string>();
        foreach (ListItem item in cblAbilitazioniSoggetto.Items)
        {
            if (item.Selected)
            {
                valoriAbilitazioniSoggetto.Add(item.Value);
            }
        }
        UtilitySoggetti.SaveInsertDeleteDatiAbilitazioniSoggetto(iDSoggetto, valoriAbilitazioniSoggetto.ToArray<string>());
    }

    protected void SaveClassificazioniImpianto(int iDSoggetto)
    {
        List<string> valoriClassificazioniImpianto = new List<string>();
        foreach (ListItem item in cblClassificazioniImpianto.Items)
        {
            if (item.Selected)
            {
                valoriClassificazioniImpianto.Add(item.Value);
            }
        }
        UtilitySoggetti.SaveInsertDeleteDatiClassificazioniImpiantoSoggetto(iDSoggetto, valoriClassificazioniImpianto.ToArray<string>());
    }

    protected int SaveIscrizione()
    {
        int iDSoggettoInsert = 0;
        object[] coordinate = new object[2];

        switch (IDTipoSoggetto)
        {
            case "1": //Persona

                break;
            case "2": //Impresa
            case "3": //Terzo responsabile
                coordinate = UtilityApp.GetGeocodingAddress(txtIndirizzoSedeLegale.Text + " ," + txtNumeroCivicoSedeLegale.Text,
                                                            txtCapSedeLegale.Text,
                                                            txtCittaSedeLegale.Text,
                                                            ddlPaeseSedeLegale.SelectedItem.Text);

                iDSoggettoInsert = UtilitySoggetti.SaveInsertDeleteDatiSoggetti(
                    "insert",
                    null,
                    "",
                    null,
                    int.Parse(IDTipoSoggetto),
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
            case "4": //Persona Responsabile tecnico

                break;
            case "5": //Distributori di combustibile
                coordinate = UtilityApp.GetGeocodingAddress(txtIndirizzoSedeLegaleDistributoriCombustibile.Text + " ," + txtCivicoSedeLegaleDistributoriCombustibile.Text,
                                                    txtCapSedeLegaleDistributoriCombustibile.Text,
                                                    txtCittaSedeLegaleDistributoriCombustibile.Text,
                                                    ddlPaeseSedeLegaleDistributoriCombustibile.SelectedItem.Text);

                iDSoggettoInsert = UtilitySoggetti.SaveInsertDeleteDatiSoggetti(
                           "insert",
                           null,
                           string.Empty,
                           null,
                           int.Parse(IDTipoSoggetto),
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

                break;
            case "7": //Ispettori
                coordinate = UtilityApp.GetGeocodingAddress(txtIndirizzoResidenzaIspettore.Text + " ," + txtNumeroCivicoResidenzaIspettore.Text,
                                                            txtCapResidenzaIspettore.Text,
                                                            txtCittaResidenzaIspettore.Text,
                                                            string.Empty);

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

                UtilitySoggetti.SaveInsertDeleteDatiAccreditamento(
                    "insert",
                    iDSoggettoInsert,
                    1,
                    null,
                    null,
                    null,
                    string.Empty,
                    null,
                    null,
                    string.Empty
                    );

                UtilitySoggetti.StoricizzaStatoAccreditamento(iDSoggettoInsert, 5805);
                break;
            case "9": //Ente Locale
                coordinate = UtilityApp.GetGeocodingAddress(txtIndirizzoSedeLegaleEnteLocale.Text + " ," + txtNumeroCivicoSedeLegaleEnteLocale.Text,
                                                            txtCapSedeLegaleEnteLocale.Text,
                                                            txtCittaSedeLegaleEnteLocale.Text,
                                                            "Italia");

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

        return iDSoggettoInsert;
    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int iDSoggetto = 0;
            switch (IDTipoSoggetto)
            {
                case "1": //Persona

                    break;
                case "2": //Impresa
                case "3": //Terzo responsabile
                    iDSoggetto = SaveIscrizione();
                    SaveProvinceCompetenza(iDSoggetto);
                    SaveRuoliSoggetto(iDSoggetto);
                    SaveClassificazioniImpianto(iDSoggetto);
                    SaveAbilitazioniSoggetto(iDSoggetto);
                    break;
                case "4": //Persona Responsabile tecnico

                    break;
                case "5": //Distributori di combustibile
                    iDSoggetto = SaveIscrizione();
                    SaveTipologiaDistributoriCombustibile(iDSoggetto);
                    break;
                case "6": //Software house

                    break;
                case "7": //Ispettori
                    if ((UploadCurriculumVitae.HasFile && UploadCurriculumVitae.PostedFile != null)
                        && (UploadAttestatoCorso.HasFile && UploadAttestatoCorso.PostedFile != null))
                    {
                        iDSoggetto = SaveIscrizione();

                        string pathIscrizione = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspettori"] + @"\" + iDSoggetto.ToString() + @"\";
                        UtilityFileSystem.CreateDirectoryIfNotExists(pathIscrizione);

                        string curriculumVitaePath = pathIscrizione + "CurriculumVitaeIspettore_" + iDSoggetto.ToString() + ".pdf";
                        UploadCurriculumVitae.SaveAs(curriculumVitaePath);

                        string attestatoCorsoPath = pathIscrizione + "AttestatoCorsoIspettore_" + iDSoggetto.ToString() + ".pdf";
                        UploadAttestatoCorso.SaveAs(attestatoCorsoPath);
                    }
                    break;
                case "9": //Ente Locale
                    iDSoggetto = SaveIscrizione();
                    SaveComuniCompetenza(iDSoggetto);
                    break;
            }

            QueryString qs = new QueryString();
            qs.Add("IDSoggetto", iDSoggetto.ToString());
            qs.Add("fSpid", fSpid);
            qs.Add("IDTipoSoggetto", IDTipoSoggetto);
            qs.Add("codicefiscale", codicefiscale);
            qs.Add("key", streemKey);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "~/IscrizioneConferma.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
    }

    #region Parix

    protected void btnParix_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            //using (var ws = new WSParix.CRSimpleWSImplClient())
            //{
            //    string partitaIva = txtPartitaIvaParix.Value.ToString().Trim();
            //    string codicefiscaleAmministratore = txtCodiceFiscaleParix.Value.ToString().Trim().ToLower();
            //    if ((txtPartitaIvaParix.IsValid) && (txtCodiceFiscaleParix.IsValid))
            //    {
            //        string xmlImpresa = ws.RicercaImpreseNonCessatePerCodiceFiscale(partitaIva.ToString(), "no", ConfigurationManager.AppSettings["ParixWsUsername"], ConfigurationManager.AppSettings["ParixWsPassword"]);
            //        var xDoc = XDocument.Parse(xmlImpresa);
            //        var status = xDoc.Descendants("ESITO").Single().Value;
            //        if (status == "OK")
            //        {
            //            var xml = new XmlDocument();
            //            xml.LoadXml(xmlImpresa);

            //            XmlNode impresaNode = xml.SelectSingleNode("RISPOSTA/DATI/LISTA_IMPRESE/ESTREMI_IMPRESA/DENOMINAZIONE");
            //            XmlNode nreaNode = xml.SelectSingleNode("RISPOSTA/DATI/LISTA_IMPRESE/ESTREMI_IMPRESA/DATI_ISCRIZIONE_REA/NREA");
            //            XmlNode cciaaNode = xml.SelectSingleNode("RISPOSTA/DATI/LISTA_IMPRESE/ESTREMI_IMPRESA/DATI_ISCRIZIONE_REA/CCIAA");
            //            string impresa = impresaNode.InnerText;
            //            string nrea = nreaNode.InnerText;
            //            string cciaa = cciaaNode.InnerText;

            //            //foreach (XmlNode node in nodeList)
            //            //{
            //            //    XmlNode nodenrea = node.SelectSingleNode("DATI_ISCRIZIONE_REA/NREA");
            //            //    //nrea = node.FirstChild.InnerText;
            //            //    break;
            //            //}

            //            if ((!string.IsNullOrEmpty(nrea)) && (!string.IsNullOrEmpty(cciaa)))
            //            {
            //                string xmlDettaglioImpresa = ws.DettaglioCompletoImpresa(cciaa, nrea, "no", ConfigurationManager.AppSettings["ParixWsUsername"], ConfigurationManager.AppSettings["ParixWsPassword"]);

            //                var xmlParix = new XmlDocument();
            //                xmlParix.LoadXml(xmlDettaglioImpresa);

            //                string codicefiscaleLegaleRappresentanteNode = string.Empty;
            //                try
            //                {
            //                    codicefiscaleLegaleRappresentanteNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/PERSONE_SEDE/PERSONA/PERSONA_FISICA/CODICE_FISCALE").InnerText.ToLower();

            //                }
            //                catch (System.Xml.XPath.XPathException ex)
            //                {

            //                    throw;
            //                }

            //                string message = string.Empty;
            //                if (codicefiscaleLegaleRappresentanteNode == codicefiscaleAmministratore)
            //                {
            //                    lblXmlParix.Text = xmlDettaglioImpresa;
            //                    message = "Trovata l'impresa " + impresa + " sul servizio Parix.<br/>Confermi di importare i dati?";
            //                    lblParixMessage.Text = message;
            //                    dmpParix.Enabled = true;
            //                    dmpParix.Show();
            //                }
            //                else
            //                {
            //                    message = "Nessuna impresa corrisponde ai dati inseriti! Inserire i dati manualmente";
            //                    lblParixMessage.Text = message;
            //                    dmpParix.Enabled = true;
            //                    dmpParix.Show();
            //                }
            //            }
            //        }
            //        else
            //        {
            //            lblMessage.Text = "Attenzione: servizio Parix attualmente non disponibile! Compilare i campi manualmente";
            //        }
            //    }
            //}
        }
    }

    //public void GetDatiParix()
    //{
    //    if (!string.IsNullOrEmpty(lblXmlParix.Text))
    //    {
    //        var xmlParix = new XmlDocument();
    //        xmlParix.LoadXml(lblXmlParix.Text);
    //        try
    //        {
    //            string denominazioneNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/ESTREMI_IMPRESA/DENOMINAZIONE").InnerText;
    //            string codicefiscaleAziendaNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/ESTREMI_IMPRESA/CODICE_FISCALE").InnerText;
    //            string partitaIvaNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/ESTREMI_IMPRESA/PARTITA_IVA").InnerText;
    //            string numeroRegistroImpresaNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/ESTREMI_IMPRESA/DATI_ISCRIZIONE_RI/NUMERO_RI").InnerText.ToLower();
    //            string nreaNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/ESTREMI_IMPRESA/DATI_ISCRIZIONE_REA/NREA").InnerText.ToLower();

    //            string cognomeLegaleRappresentanteNode = UtilityApp.GetPrimaLetteraMaiuscola(xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/PERSONE_SEDE/PERSONA/PERSONA_FISICA/COGNOME").InnerText.ToLower());
    //            string nomeLegaleRappresentanteNode = UtilityApp.GetPrimaLetteraMaiuscola(xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/PERSONE_SEDE/PERSONA/PERSONA_FISICA/NOME").InnerText.ToLower());
    //            string codicefiscaleLegaleRappresentanteNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/PERSONE_SEDE/PERSONA/PERSONA_FISICA/CODICE_FISCALE").InnerText.ToUpper();

    //            string provinciaresidenzaLegaleRappresentanteNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/PERSONE_SEDE/PERSONA/PERSONA_FISICA/INDIRIZZO/PROVINCIA").InnerText.ToLower();
    //            string comuneresidenzaLegaleRappresentanteNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/PERSONE_SEDE/PERSONA/PERSONA_FISICA/INDIRIZZO/COMUNE").InnerText.ToLower();
    //            string toponimoresidenzaLegaleRappresentanteNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/PERSONE_SEDE/PERSONA/PERSONA_FISICA/INDIRIZZO/TOPONIMO").InnerText.ToLower();
    //            string viaresidenzaLegaleRappresentanteNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/PERSONE_SEDE/PERSONA/PERSONA_FISICA/INDIRIZZO/VIA").InnerText.ToLower();
    //            string ncivicoresidenzaLegaleRappresentanteNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/PERSONE_SEDE/PERSONA/PERSONA_FISICA/INDIRIZZO/N_CIVICO").InnerText.ToLower();
    //            string capresidenzaLegaleRappresentanteNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/PERSONE_SEDE/PERSONA/PERSONA_FISICA/INDIRIZZO/CAP").InnerText.ToLower();

    //            string datanascitaLegaleRappresentanteNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/PERSONE_SEDE/PERSONA/PERSONA_FISICA/ESTREMI_NASCITA/DATA").InnerText;
    //            DateTime dt = DateTime.ParseExact(datanascitaLegaleRappresentanteNode, "yyyyMMdd", CultureInfo.InvariantCulture);
    //            string datanascitaLegaleRappresentanteFormatNode = dt.ToString("dd") + "/" + dt.ToString("MM") + "/" + dt.ToString("yyyy");
    //            string provincianascitaLegaleRappresentanteNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/PERSONE_SEDE/PERSONA/PERSONA_FISICA/ESTREMI_NASCITA/PROVINCIA").InnerText.ToLower();
    //            string cittanascitaLegaleRappresentanteNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/PERSONE_SEDE/PERSONA/PERSONA_FISICA/ESTREMI_NASCITA/COMUNE").InnerText.ToLower();

    //            string provinciaNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/INFORMAZIONI_SEDE/INDIRIZZO/PROVINCIA").InnerText.ToLower();
    //            string comuneNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/INFORMAZIONI_SEDE/INDIRIZZO/COMUNE").InnerText.ToLower();
    //            string toponimoNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/INFORMAZIONI_SEDE/INDIRIZZO/TOPONIMO").InnerText.ToLower();
    //            string viaNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/INFORMAZIONI_SEDE/INDIRIZZO/VIA").InnerText.ToLower();
    //            string civicoNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/INFORMAZIONI_SEDE/INDIRIZZO/N_CIVICO").InnerText.ToLower();
    //            string capNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/INFORMAZIONI_SEDE/INDIRIZZO/CAP").InnerText.ToLower();
    //            string pecNode = xmlParix.SelectSingleNode("RISPOSTA/DATI/DATI_IMPRESA/INFORMAZIONI_SEDE/INDIRIZZO/INDIRIZZO_PEC").InnerText.ToLower();

    //            txtAzienda.Text = denominazioneNode;
    //            txtIndirizzoSedeLegale.Text = toponimoNode + " " + viaNode;
    //            txtNumeroCivicoSedeLegale.Text = civicoNode;
    //            txtCapSedeLegale.Text = capNode;
    //            ddlProvinciaSedeLegale.SelectedIndex = ddlProvinciaSedeLegale.Items.IndexOf(ddlProvinciaSedeLegale.Items.FindByText(UtilityApp.GetPrimaLetteraMaiuscola(provinciaNode)));
    //            txtCittaSedeLegale.Text = comuneNode;
    //            txtPartitaIva.Text = partitaIvaNode;
    //            txtCodiceFiscaleAzienda.Text = codicefiscaleAziendaNode;
    //            txtNome.Text = nomeLegaleRappresentanteNode;
    //            txtCognome.Text = cognomeLegaleRappresentanteNode;
    //            txtDataNascita.Text = datanascitaLegaleRappresentanteFormatNode;
    //            ddlProvinciaNascita.SelectedIndex = ddlProvinciaNascita.Items.IndexOf(ddlProvinciaNascita.Items.FindByText(UtilityApp.GetPrimaLetteraMaiuscola(provincianascitaLegaleRappresentanteNode)));

    //            txtCittaNascita.Text = cittanascitaLegaleRappresentanteNode;
    //            txtCodiceFiscale.Text = codicefiscaleLegaleRappresentanteNode;
    //            txtNumeroAlboImprese.Text = nreaNode;

    //            ddlProvinciaResidenza.SelectedIndex = ddlProvinciaResidenza.Items.IndexOf(ddlProvinciaResidenza.Items.FindByText(UtilityApp.GetPrimaLetteraMaiuscola(provinciaresidenzaLegaleRappresentanteNode)));
    //            txtCittaResidenza.Text = comuneresidenzaLegaleRappresentanteNode;
    //            txtIndirizzoResidenza.Text = toponimoresidenzaLegaleRappresentanteNode + " " + viaresidenzaLegaleRappresentanteNode;
    //            txtNumeroCivicoResidenza.Text = ncivicoresidenzaLegaleRappresentanteNode;
    //            txtCapResidenza.Text = capresidenzaLegaleRappresentanteNode;
    //            txtEmailPec.Text = pecNode;

    //        }
    //        catch (System.Xml.XPath.XPathException ex)
    //        {

    //        }
    //    }
    //}

    //protected void ResetParix()
    //{
    //    txtPartitaIvaParix.Text = string.Empty;
    //    txtCodiceFiscaleParix.Text = string.Empty;
    //    dmpParix.Enabled = false;
    //    dmpParix.Hide();
    //}

    //public void btnImportParix_Click(object sender, EventArgs e)
    //{
    //    ResetParix();
    //    //Popolamento campi
    //    GetDatiParix();
    //}

    //protected void btnImportParixCancel_Click(object sender, EventArgs e)
    //{
    //    ResetParix();
    //}

    #endregion

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
            //lblPartitaIvaIspettore.Text = "Partita Iva (*)";
            //txtPartitaIvaIspettore.CssClass = "txtClass_o";
            //rfvPartitaIvaIspettore.Enabled = true;

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
            //lblPartitaIvaIspettore.Text = "Partita Iva";
            //txtPartitaIvaIspettore.CssClass = "txtClass";
            //rfvPartitaIvaIspettore.Enabled = false;

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

    //public List<int> SelectedComuniValues
    //{
    //    get
    //    {
    //        if (ViewState["ComuniSelezionati"] == null)
    //        {
    //            ViewState["ComuniSelezionati"] = new List<int>();

    //        }
    //        return (List<int>)ViewState["ComuniSelezionati"];
    //    }
    //    set
    //    {
    //        ViewState["ComuniSelezionati"] = value;
    //    }
    //}

    protected void AggiornaComuni(ASPxGridView grid)
    {
        //SelectedComuniValues.Clear();
        
        //for (int i = 0; i < grid.VisibleRowCount; i++)
        //{
        //    if (grid.Selection.IsRowSelected(i))
        //    {
        //        SelectedComuniValues.Add(int.Parse(grid.GetRowValues(i, "IDCodiceCatastale").ToString()));
        //    }
        //}

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

    protected void SaveComuniCompetenza(int iDSoggetto)
    {
        //List<string> valoriComuniCompetenza = new List<string>();
        //foreach (ListItem item in cblProvinceCompetenza.Items)
        //{
        //    if (item.Selected)
        //    {
        //        valoriComuniCompetenza.Add(item.Value);
        //    }
        //}

        //foreach (var item in SelectedComuniValues)
        //{

        //}
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
        bool fEmail = UtilitySoggetti.CheckfEmail(txtEmailEnteLocale.Text, null, int.Parse(IDTipoSoggetto));
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
        bool fEmail = UtilitySoggetti.CheckfEmailPec(txtEmailPecEnteLocale.Text, null, int.Parse(IDTipoSoggetto));
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
        bool fEsiste = UtilitySoggetti.CheckfPartitaIva(txtPartitaIvaEnteLocale.Text, null, int.Parse(IDTipoSoggetto));
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
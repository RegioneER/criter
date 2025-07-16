using DataLayer;
using DataUtilityCore;
using EncryptionQS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VER_IspezioniPianificazioneDetails : System.Web.UI.Page
{
    protected string IDIspezione
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

    protected string IDIspezioneVisita
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

    UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PagePermission();
            GetDatiPianificazione(long.Parse(IDIspezione));
        }
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
            case "7": //Coordinatore
                rowIspezionePagamento1.Visible = true;
                rowIspezionePagamento2.Visible = true;
                break;
            case "8": //Ispettore
                rowIspezionePagamento1.Visible = false;
                rowIspezionePagamento2.Visible = false;
                break;
        }
    }

    protected void ddOrario(DropDownList ddlOrario, int? IDPresel)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_Orario(IDPresel);
        ddlOrario.DataValueField = "IDOrario";
        ddlOrario.DataTextField = "Orario";
        ddlOrario.DataSource = ls;
        ddlOrario.DataBind();

        ListItem myItem = new ListItem("-- Selezionare --", "0");
        ddlOrario.Items.Insert(0, myItem);
        ddlOrario.SelectedIndex = 0;
    }

    public void GetDatiPianificazione(long iDIspezione)
    {
        using (var ctx = new CriterDataModel())
        {
            var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == iDIspezione).FirstOrDefault();
            if (ispezione != null)
            {
                if (ispezione.DataIspezione != null)
                {
                    txtDataIspezione.Text = string.Format("{0:dd/MM/yyyy}", ispezione.DataIspezione);
                }
                else
                {
                    txtDataIspezione.Text = string.Empty;
                }
                if (ispezione.IDOrarioDa != null)
                {
                    ddOrario(ddlOrarioDa, null);
                    ddlOrarioDa.SelectedValue = ispezione.IDOrarioDa.ToString();
                }
                else
                {
                    ddOrario(ddlOrarioDa, null);
                }
                if (ispezione.IDOrarioA != null)
                {
                    ddOrario(ddlOrarioA, null);
                    ddlOrarioA.SelectedValue = ispezione.IDOrarioA.ToString();
                }
                else
                {
                    ddOrario(ddlOrarioA, null);
                }
                if (ispezione.Osservatore != null)
                {
                    txtOsservatoreIspezione.Text = ispezione.Osservatore;
                }
                chkIspezioneAPagamento.Checked = ispezione.fIspezionePagamento;
                VisibleHiddenPagamento(ispezione.fIspezionePagamento);

                if (ispezione.CostoIspezione != null)
                {
                    txtImportoIspezione.Text = ispezione.CostoIspezione.ToString();
                }

                UCGoogleAutosuggest.IDLibrettoImpianto = ispezione.VER_IspezioneVisitaInfo.IDLibrettoImpianto.ToString();
            }
        }
    }

    public void VisibleHiddenPagamento(bool fIspezioneAPagamento)
    {
        if (fIspezioneAPagamento & (userInfo.IDRuolo == 1 || userInfo.IDRuolo == 7))
        {
            rowIspezionePagamento2.Visible = true;
            txtImportoIspezione.Text = "";
        }
        else
        {
            rowIspezionePagamento2.Visible = false;
        }
    }

    protected void chkIspezioneAPagamento_CheckedChanged(object sender, EventArgs e)
    {
        VisibleHiddenPagamento(chkIspezioneAPagamento.Checked);
    }

    protected void ControllaNormalizeAddress(object sender, ServerValidateEventArgs e)
    {
        using (var ctx = new CriterDataModel())
        {
            int iDIspezione = int.Parse(IDIspezione);
            var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == iDIspezione).FirstOrDefault();
            if (ispezione != null)
            {
                var libretto = ctx.LIM_LibrettiImpianti.Where(c => c.IDLibrettoImpianto == ispezione.VER_IspezioneVisitaInfo.IDLibrettoImpianto).FirstOrDefault();
                if (libretto != null)
                {
                    if (!string.IsNullOrEmpty(libretto.CapResponsabile) && !string.IsNullOrEmpty(libretto.IndirizzoNormalizzatoResponsabile))
                    {
                        e.IsValid = true;
                    }
                    else
                    {
                        e.IsValid = false;
                        cvNormalizeAddress.ErrorMessage = "Attenzione per pianificare l'ispezione è necessario normalizzare l'indirizzo postale";
                    }
                }
                else
                {
                    e.IsValid = false;
                    cvNormalizeAddress.ErrorMessage = "Attenzione per pianificare l'ispezione è necessario normalizzare l'indirizzo postale";
                }
            }
            else
            {
                e.IsValid = false;
                cvNormalizeAddress.ErrorMessage = "Attenzione per pianificare l'ispezione è necessario normalizzare l'indirizzo postale";
            }
        }
    }

    protected void btnSalvaPianificazione_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long? iDIspezione = null;
            long iDIspezioneVisita = UtilityVerifiche.GetIDIspezioneVisitaFromVerifica(long.Parse(IDIspezione));

            if (!chkStessaDataIspezione.Checked)
            {
                iDIspezione = long.Parse(IDIspezione);
            }

            UtilityVerifiche.CambiaStatoIspezioneMassivo(iDIspezioneVisita,
                                                         3,
                                                         iDIspezione,
                                                         UtilityApp.ParseNullableDatetime(txtDataIspezione.Text),
                                                         UtilityApp.ParseNullableInt(ddlOrarioDa.SelectedValue),
                                                         UtilityApp.ParseNullableInt(ddlOrarioA.SelectedValue)
                                                         );

            UtilityVerifiche.StoricizzaStatoIspezione(long.Parse(IDIspezione), int.Parse(userInfo.IDUtente.ToString()));

            UtilityVerifiche.SetFieldAccessoriIspezione(long.Parse(IDIspezione),
                                                        txtOsservatoreIspezione.Text,
                                                        chkIspezioneAPagamento.Checked,
                                                        UtilityApp.ParseNullableDecimal(txtImportoIspezione.Text)
                                                        );

            RedirectPianificazionePage();
        }
    }

    public void RedirectPianificazionePage() 
    {
        QueryString qs = new QueryString();
        qs.Add("IDIspezioneVisita", IDIspezioneVisita);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "VER_IspezioniPianificazione.aspx";
        url += qsEncrypted.ToString();
        Response.Redirect(url);
    }

    public void ControllaDataIspezione(Object sender, ServerValidateEventArgs e)
    {
        DateTime DataIspezione;
        DateTime? DataFirmaLai = UtilityVerifiche.GetDataFirmaIncarico(long.Parse(IDIspezioneVisita));

        bool chkDate = DateTime.TryParse(txtDataIspezione.Text, out DataIspezione);

        if (chkDate && DataIspezione.Date > DataFirmaLai)
        {
            e.IsValid = true;
        }
        else
        {
            e.IsValid = false;
        }
    }

    protected void btnAnnullaPianificazione_Click(object sender, EventArgs e)
    {
        RedirectPianificazionePage();
    }
}
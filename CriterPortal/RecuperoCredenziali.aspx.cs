using DataUtilityCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RecuperoCredenziali : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtCodFiscalePiva.Focus();
            VisibleHiddenPanelTipoSoggetto(rblTipoSoggetto.SelectedValue);
        }
    }

    public void ControllaRecuperoPassword(Object sender, ServerValidateEventArgs e)
    {
        bool fRecupero = false;
        switch (rblTipoSoggetto.SelectedValue)
        {
            case "1": //Impresa
                fRecupero = SecurityManager.ControllaRecuperaPassword(txtCodFiscalePiva.Text.Trim(), txtCodiceSoggetto.Text.Trim(), rblTipoSoggetto.SelectedValue);
                break;
            case "2": //Manutentore
                fRecupero = SecurityManager.ControllaRecuperaPassword(txtCodFiscaleManutentore.Text.Trim(), txtCodiceSoggettoManutentore.Text.Trim(), rblTipoSoggetto.SelectedValue);
                break;
            case "3": //Distributore
                fRecupero = SecurityManager.ControllaRecuperaPassword(txtPartitaIvaDistributore.Text.Trim(), txtEmailDistributore.Text.Trim(), rblTipoSoggetto.SelectedValue);
                break;
            case "4": //Ente Locale
                fRecupero = SecurityManager.ControllaRecuperaPassword(txtPartitaIvaEnteLocale.Text.Trim(), txtEmailEnteLocale.Text.Trim(), rblTipoSoggetto.SelectedValue);
                break;
            case "5": //Software house
                fRecupero = SecurityManager.ControllaRecuperaPassword(txtPartitaIvaSoftwareHouse.Text.Trim(), txtEmailSoftwareHouse.Text.Trim(), rblTipoSoggetto.SelectedValue);
                break;
            case "6": //Ispettori
                fRecupero = SecurityManager.ControllaRecuperaPassword(txtPartitaIvaIspettore.Text.Trim(), txtEmailIspettore.Text.Trim(), rblTipoSoggetto.SelectedValue);
                break;
        }

        if (!fRecupero)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }

    public void ResetCredenziali(string codFiscalePiva, string codiceSoggetto, string tipoSoggetto)
    {
        bool fPass = SecurityManager.RecuperaCredenziali(codFiscalePiva, codiceSoggetto, tipoSoggetto);

        if (fPass)
        {
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Credenziali richieste inviate al suo indirizzo email!";
            lblMessage.Visible = true;

            string jScript = "setTimeout('Redirect()',2000); function Redirect() { location.href = 'Login.aspx'; }";
            string jScriptKey = "KeyScript";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), jScriptKey, jScript, true);
        }
        else
        {
            lblMessage.CssClass = "validation-error";
            lblMessage.Text = "Recupero credenziali non riuscite: informazioni richieste non inserite correttamente o utenza bloccata";
            lblMessage.Visible = true;
        }
    }

    protected void RecuperoCredenziali_btnRecovery_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            switch (rblTipoSoggetto.SelectedValue)
            {
                case "1": //Impresa
                    ResetCredenziali(txtCodFiscalePiva.Text.Trim(), txtCodiceSoggetto.Text.Trim(), rblTipoSoggetto.SelectedValue);
                    break;
                case "2": //Manutentore
                    ResetCredenziali(txtCodFiscaleManutentore.Text.Trim(), txtCodiceSoggettoManutentore.Text.Trim(), rblTipoSoggetto.SelectedValue);
                    break;
                case "3": //Distributore
                    ResetCredenziali(txtPartitaIvaDistributore.Text.Trim(), txtEmailDistributore.Text.Trim(), rblTipoSoggetto.SelectedValue);
                    break;
                case "4": //Ente Locale
                    ResetCredenziali(txtPartitaIvaEnteLocale.Text.Trim(), txtEmailEnteLocale.Text.Trim(), rblTipoSoggetto.SelectedValue);
                    break;
                case "5": //Software house
                    ResetCredenziali(txtPartitaIvaSoftwareHouse.Text.Trim(), txtEmailSoftwareHouse.Text.Trim(), rblTipoSoggetto.SelectedValue);
                    break;
                case "6": //Ispettori
                    ResetCredenziali(txtPartitaIvaIspettore.Text.Trim(), txtEmailIspettore.Text.Trim(), rblTipoSoggetto.SelectedValue);
                    break;
            }
        }
    }

    protected void rblTipoSoggetto_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenPanelTipoSoggetto(rblTipoSoggetto.SelectedValue);
    }

    public void VisibleHiddenPanelTipoSoggetto(string tipoSoggetto)
    {
        switch (tipoSoggetto)
        {
            case "1": //Impresa
                pnlImpresa.Visible = true;
                pnlManutentore.Visible = false;
                pnlDistributore.Visible = false;
                pnlEnteLocale.Visible = false;
                pnlSoftwareHouse.Visible = false;
                pnlIspettore.Visible = false;
                break;
            case "2": //Manutentore
                pnlImpresa.Visible = false;
                pnlManutentore.Visible = true;
                pnlDistributore.Visible = false;
                pnlEnteLocale.Visible = false;
                pnlSoftwareHouse.Visible = false;
                pnlIspettore.Visible = false;
                break;
            case "3": //Distributore
                pnlImpresa.Visible = false;
                pnlManutentore.Visible = false;
                pnlDistributore.Visible = true;
                pnlEnteLocale.Visible = false;
                pnlSoftwareHouse.Visible = false;
                pnlIspettore.Visible = false;
                break;
            case "4": //Ente Locale
                pnlImpresa.Visible = false;
                pnlManutentore.Visible = false;
                pnlDistributore.Visible = false;
                pnlEnteLocale.Visible = true;
                pnlSoftwareHouse.Visible = false;
                pnlIspettore.Visible = false;
                break;
            case "5": //Software house
                pnlImpresa.Visible = false;
                pnlManutentore.Visible = false;
                pnlDistributore.Visible = false;
                pnlEnteLocale.Visible = false;
                pnlSoftwareHouse.Visible = true;
                pnlIspettore.Visible = false;
                break;
            case "6": //Ispettori
                pnlImpresa.Visible = false;
                pnlManutentore.Visible = false;
                pnlDistributore.Visible = false;
                pnlEnteLocale.Visible = false;
                pnlSoftwareHouse.Visible = false;
                pnlIspettore.Visible = true;
                break;
        }
    }

}
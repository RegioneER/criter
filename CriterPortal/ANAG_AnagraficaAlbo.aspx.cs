using DataLayer;
using System;
using System.Linq;
using System.Configuration;
using EncryptionQS;
using DataUtilityCore;
using System.Web.UI;
using System.Web;

public partial class ANAG_AnagraficaAlbo : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (string.IsNullOrEmpty(IDSoggetto))
            {
                //Caso in cui recupero i valori dall'account
                UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
                RedirectPage(info.IDSoggetto.ToString());
            }
            else
            {
                int iDSoggetto = int.Parse(IDSoggetto);
                GetDaTa(iDSoggetto);
                EnabledDisableCheck();
            }
        }
    }

    public void RedirectPage(string iDSoggetto)
    {
        QueryString qs = new QueryString();
        qs.Add("IDSoggetto", iDSoggetto);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "ANAG_AnagraficaAlbo.aspx";
        url += qsEncrypted.ToString();
        Response.Redirect(url, true);
    }

    protected void GetDaTa(int iDSoggetto)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            var soggetto = ctx.COM_AnagraficaSoggettiAlbo.FirstOrDefault(a => a.IDSoggetto == iDSoggetto);
            lblImpresa.Text = soggetto.Impresa;
            lblIndirizzo.Text = soggetto.Indirizzo;
            lblCap.Text = soggetto.Cap;
            lblCitta.Text = soggetto.Citta;
            lbliDProvincia.Text = soggetto.IDProvincia.ToString();
            lblSoggetto.Text = "GESTIONE DATI ANAGRAFICI SU ALBO &nbsp;&mdash;&nbsp; " + soggetto.Impresa;

            if (!string.IsNullOrEmpty(soggetto.AmministratoreDelegato))
            {
                txtAmministratoreDelegato.Text = soggetto.AmministratoreDelegato;
            }
            ASPxCheckBoxAmministratoreDelegato.Checked = soggetto.fAmministratoreDelegato;

            if (!string.IsNullOrEmpty(soggetto.Telefono))
            {
                txtTelefono.Text = soggetto.Telefono;
            }
            ASPxCheckBoxTelefono.Checked = soggetto.fTelefono;

            if (!string.IsNullOrEmpty(soggetto.Email))
            {
                txtEmail.Text = soggetto.Email;
            }
            ASPxCheckBoxEmail.Checked = soggetto.fEmail;

            if (!string.IsNullOrEmpty(soggetto.EmailPec))
            {
                txtEmailPec.Text = soggetto.EmailPec;
            }
            ASPxCheckBoxEmailPec.Checked = soggetto.fEmailPec;

            if (!string.IsNullOrEmpty(soggetto.Fax))
            {
                txtFax.Text = soggetto.Fax;
            }
            ASPxCheckBoxFax.Checked = soggetto.fFax;

            if (!string.IsNullOrEmpty(soggetto.SitoWeb))
            {
                txtSitoWeb.Text = soggetto.SitoWeb;
            }
            ASPxCheckBoxSitoWeb.Checked = soggetto.fSitoWeb;

            if (!string.IsNullOrEmpty(soggetto.PartitaIVA))
            {
                txtPartitaIVA.Text = soggetto.PartitaIVA;
            }
            ASPxCheckBoxPartitaIVA.Checked = soggetto.fPartitaIVA;
        }
    }
    
    protected void btnProcessSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int? iDSoggetto = int.Parse(IDSoggetto);
            UtilitySoggetti.SaveInsertDeleteDatiAlboSoggetti(iDSoggetto,
                                                             lblImpresa.Text,
                                                             lblIndirizzo.Text,
                                                             lblCap.Text,
                                                             lblCitta.Text,
                                                             int.Parse(lbliDProvincia.Text),
                                                             txtAmministratoreDelegato.Text,
                                                             txtTelefono.Text,
                                                             txtFax.Text,
                                                             txtEmail.Text,
                                                             txtEmailPec.Text,
                                                             txtSitoWeb.Text,
                                                             txtPartitaIVA.Text,
                                                             ASPxCheckBoxAmministratoreDelegato.Checked,
                                                             ASPxCheckBoxTelefono.Checked,
                                                             ASPxCheckBoxFax.Checked,
                                                             ASPxCheckBoxEmail.Checked,
                                                             ASPxCheckBoxEmailPec.Checked,
                                                             ASPxCheckBoxSitoWeb.Checked,
                                                             ASPxCheckBoxPartitaIVA.Checked);
        }
    }

    #region Gestione Check
    protected void EnabledDisableCheck()
    {
        if (ASPxCheckBoxAmministratoreDelegato.Checked)
        {
            lblTitoloAmministratoreDelegato.Text = "Amministratore delegato (*)";
            txtAmministratoreDelegato.CssClass = "txtClass_o";
            rfvtxtAmministratoreDelegato.Enabled = true;
        }
        else
        {
            lblTitoloAmministratoreDelegato.Text = "Amministratore delegato";
            txtAmministratoreDelegato.CssClass = "txtClass";
            rfvtxtAmministratoreDelegato.Enabled = false;
        }

        if (ASPxCheckBoxTelefono.Checked)
        {
            lblTitoloTelefono.Text = "Telefono (*)";
            txtTelefono.CssClass = "txtClass_o";
            rfvtxtTelefono.Enabled = true;
        }
        else
        {
            lblTitoloTelefono.Text = "Telefono";
            txtTelefono.CssClass = "txtClass";
            rfvtxtTelefono.Enabled = false;
        }

        if (ASPxCheckBoxEmail.Checked)
        {
            lblTitoloEmail.Text = "Email (*)";
            txtEmail.CssClass = "txtClass_o";
            rfvtxtEmail.Enabled = true;
        }
        else
        {
            lblTitoloEmail.Text = "Email";
            txtEmail.CssClass = "txtClass";
            rfvtxtEmail.Enabled = false;
        }

        if (ASPxCheckBoxEmailPec.Checked)
        {
            lblTitoloEmailPec.Text = "EmailPec (*)";
            txtEmailPec.CssClass = "txtClass_o";
            rfvtxtEmailPec.Enabled = true;
        }
        else
        {
            lblTitoloEmailPec.Text = "EmailPec";
            txtEmailPec.CssClass = "txtClass";
            rfvtxtEmailPec.Enabled = false;
        }

        if (ASPxCheckBoxFax.Checked)
        {
            lblTitoloFax.Text = "Fax (*)";
            txtFax.CssClass = "txtClass_o";
            rfvtxtFax.Enabled = true;
        }
        else
        {
            lblTitoloFax.Text = "Fax";
            txtFax.CssClass = "txtClass";
            rfvtxtFax.Enabled = false;
        }

        if (ASPxCheckBoxSitoWeb.Checked)
        {
            lblTitoloSitoWeb.Text = "Sito Web (*)";
            txtSitoWeb.CssClass = "txtClass_o";
            rfvtxtSitoWeb.Enabled = true;
        }
        else
        {
            lblTitoloSitoWeb.Text = "Sito Web";
            txtSitoWeb.CssClass = "txtClass";
            rfvtxtSitoWeb.Enabled = false;
        }

        if (ASPxCheckBoxPartitaIVA.Checked)
        {
            lblTitoloPartitaIVA.Text = "Partita IVA (*)";
            txtPartitaIVA.CssClass = "txtClass_o";
            rfvtxtPartitaIVA.Enabled = true;
        }
        else
        {
            lblTitoloPartitaIVA.Text = "Partita IVA";
            txtPartitaIVA.CssClass = "txtClass";
            rfvtxtPartitaIVA.Enabled = false;
        }
    }

    protected void ASPxCheckBoxAmministratoreDelegato_CheckedChanged(object sender, EventArgs e)
    {
        EnabledDisableCheck();
    }

    protected void ASPxCheckBoxTelefono_CheckedChanged(object sender, EventArgs e)
    {
        EnabledDisableCheck();
    }

    protected void ASPxCheckBoxEmail_CheckedChanged(object sender, EventArgs e)
    {
        EnabledDisableCheck();
    }

    protected void ASPxCheckBoxEmailPec_CheckedChanged(object sender, EventArgs e)
    {
        EnabledDisableCheck();
    }

    protected void ASPxCheckBoxFax_CheckedChanged(object sender, EventArgs e)
    {
        EnabledDisableCheck();
    }

    protected void ASPxCheckBoxSitoWeb_CheckedChanged(object sender, EventArgs e)
    {
        EnabledDisableCheck();
    }

    protected void ASPxCheckBoxPartitaIVA_CheckedChanged(object sender, EventArgs e)
    {
        EnabledDisableCheck();
    }
    #endregion
}
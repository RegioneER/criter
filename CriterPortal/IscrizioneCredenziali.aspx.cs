using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataUtilityCore;
using System.Configuration;
using EncryptionQS;

public partial class IscrizioneCredenziali : System.Web.UI.Page
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
                            return (string) qsdec[1];
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
                        return (string) Request.QueryString["IDTipoSoggetto"];
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
            int? iDSoggetto = null;
            if (!string.IsNullOrEmpty(IDSoggetto))
            {
                iDSoggetto = int.Parse(IDSoggetto);
            }

            bool flagAccesso = SecurityManager.CheckAccessPageConfirmCredential(iDSoggetto);

            if (!flagAccesso)
            {
                Response.Redirect("~/IscrizioneCredenzialiError.aspx");
            }
        }
    }

    #region CUSTOM CONTROL
    public void ControllaUsernamePresente(Object sender, ServerValidateEventArgs e)
    {
        bool fEmail = SecurityManager.CheckUsername(txtUsername.Text, null);
        if (fEmail)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }
    #endregion

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if ((!string.IsNullOrWhiteSpace(IDSoggetto)) && (!string.IsNullOrWhiteSpace(IDTipoSoggetto)))
            {
                int? iDSoggetto = int.Parse(IDSoggetto);
                int?  iDTipoSoggetto = int.Parse(IDTipoSoggetto);
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;

                SecurityManager.ActivateUserCredential(iDSoggetto, iDTipoSoggetto, null, username, password, "update");

                tblInfoCredenziali.Visible = false;
                tblInfoConfermaCredenziali.Visible = true;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + ConfigurationManager.AppSettings["UrlPortal"].ToString() + "';},4000);", true);
            }
        }
    }

}
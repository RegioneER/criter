using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using System.Configuration;
using EncryptionQS;
using DataUtilityCore;

public partial class MNG_Utenti : System.Web.UI.Page
{
    protected string IDUtente
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
                            return (string) qsdec[0];
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
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "MNG_Utenti.aspx");

        if (!Page.IsPostBack)
        {
            GetData(IDUtente);
        }
    }

    public void GetData(string IDUtente)
    {
        using (var ctx = new CriterDataModel())
        {
            var utente = ctx.COM_Utenti.Find(int.Parse(IDUtente));
            if (utente.IDRuolo != null)
            {
                ddlGruppi(utente.IDRuolo);
                ddlUserRole.SelectedValue = utente.IDRuolo.ToString();
            }
            else
            {
                ddlGruppi(null);
            }
            if (utente.Username != null)
            {
                txtUsername.Text = utente.Username;
            }
            chkAttivo.Checked = utente.fAttivo;
            chkBloccato.Checked = utente.fBloccato;
        }
    }

    public void ddlGruppi(int? iDPresel)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_UserRole(iDPresel);
        ddlUserRole.DataValueField = "IDRole";
        ddlUserRole.DataTextField = "Role";
        ddlUserRole.DataSource = ls;
        ddlUserRole.DataBind();

        ddlUserRole.Items.Insert(0, "-- Selezionare --");
        ddlUserRole.SelectedIndex = 0;
    }

    public void ControllaUsernamePresente(Object sender, ServerValidateEventArgs e)
    {
        bool fUsername = SecurityManager.CheckUsername(txtUsername.Text, int.Parse(IDUtente));
        if (fUsername)
        {
            e.IsValid = false;
        }
        else
        {
            e.IsValid = true;
        }
    }

    protected void btnAnnulla_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MNG_UtentiSearch.aspx");
    }

    protected void btnSalva_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            using (var ctx = new CriterDataModel())
            {
                COM_Utenti utente = ctx.COM_Utenti.Find(int.Parse(IDUtente));
                if (ddlUserRole.SelectedIndex != 0)
                {
                    utente.IDRuolo = int.Parse(ddlUserRole.SelectedValue);
                }

                utente.Username = txtUsername.Text;
                utente.fAttivo = chkAttivo.Checked;
                utente.fBloccato = chkBloccato.Checked;
                if (!chkBloccato.Checked)
                {
                    utente.DataPrimoLogonFallito = null;
                    utente.NrTentativiFalliti = 0;
                }
                
                ctx.SaveChanges();

                QueryString qs = new QueryString();
                qs.Add("IDUtente", IDUtente);
                QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                string url = "~/MNG_Utenti.aspx";
                url += qsEncrypted.ToString();
                Response.Redirect(url, true);
            }
        }
    }



}
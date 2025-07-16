using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DataUtilityCore;
using EncryptionQS;

public partial class COM_Password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtPswCorrente.Focus();
        }
    }

    #region CUSTOM CONTROLL
    public void ControlliPassword(Object sender, ServerValidateEventArgs e)
    {
        string message = "";
        bool fA = true;

        string attualePassword = txtPswCorrente.Text.Trim();
        string newPwd = txtPswNuova.Text.Trim();
        string newConfirmPwd = txtPswNuovaConferma.Text.Trim();
        string minChar = ConfigurationManager.AppSettings["minCharPwd"];
        string username = HttpContext.Current.User.Identity.Name.ToString();
        
        SecurityManager.LoginStatus statusLogin = SecurityManager.ValidateUser(username, attualePassword, "fAttivo");

        if (statusLogin == SecurityManager.LoginStatus.LoginOK)
        {
            SecurityManager.ChangePwdStatus status = SecurityManager.ChangePassword(newPwd, newConfirmPwd, minChar, username);
            switch (status)
            {
                case SecurityManager.ChangePwdStatus.ChangeKo:
                    //---errore
                    message = "Errore nel cambio password";
                    fA = false;
                    break;
                case SecurityManager.ChangePwdStatus.ChangeOk:
                    //---cambio ok
                    Logger.LogUser(TipoEvento.ModificaDati, UtilityApp.GetCurrentPageName(), HttpContext.Current.User.Identity.Name, HttpContext.Current.Request.UserHostAddress);
                    fA = true;
                    break;
                case SecurityManager.ChangePwdStatus.DifferentPwd:
                    //---valori diversi
                    fA = false;
                    message = "Errore - Le password inserite sono diverse.";
                    break;
                case SecurityManager.ChangePwdStatus.EmptyPwd:
                    //---manca una delle due
                    fA = false;
                    message = "Errore - Entrambi i campi devono essere obbligatori.";
                    break;
                case SecurityManager.ChangePwdStatus.SamePwd:
                    //---stessa pwd vecchia
                    fA = false;
                    message = "Errore - Non è più possibile utilizzare la vecchia password.";
                    break;
                case SecurityManager.ChangePwdStatus.ShortPwd:
                    //---numero caratteri inferiori alla lunghezza minima
                    fA = false;
                    message = "Errore - Devi inserire una password di almeno " + minChar + " caratteri.";
                    break;
            }
        }
        else
        {
            fA = false;
            message = "Errore - Errore - L'attuale password non corrisponde.";
        }
   
        if (fA)
        {
            e.IsValid = true;
        }
        else
        {
            e.IsValid = false;
            COM_Password_cvCambioPassword.ErrorMessage = message;
        }
        
    }
    #endregion

    public void RedirectPage()
    {
        if (ConfigurationManager.AppSettings["fEncryptQueryString"] == "on")
        {
            QueryString qs = new QueryString();
            qs.Add("", "");
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "~/default.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url, true);
        }
        else
        {
            Response.Redirect("~/default.aspx");
        }
    }

    public void btnProcess_Click(Object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            RedirectPage();
        }
    }
}
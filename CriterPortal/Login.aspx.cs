using System;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls;
using DataUtilityCore;
using System.Web.UI;
using EncryptionQS;
using System.Web;
using System.Web.Script.Serialization;

public partial class Login : Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        LoginControl.Attributes.Add("summary", "Criter Login");
        this.Form.DefaultButton = LoginControl.FindControl("btnlogin").UniqueID;

        if (!Page.IsPostBack)
        {
            TextBox tbxUserName = (TextBox) LoginControl.FindControl("UserName");
            tbxUserName.Focus();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CheckSpidSingleSignOn();
        }
    }

    protected bool IsSpidRequest()
    {
        bool fResult = false;
        string streemCodicefiscale = Request.Headers["codicefiscale"];
        
        //CAMBIO CONFIGURAZIONE 25/01/2022
        //string streemKey = Request.Headers["key"];
        //if (streemKey == ConfigurationManager.AppSettings["KeyReverseProxySpid"])
        if (!string.IsNullOrEmpty(streemCodicefiscale))
        {
            fResult = true;
            //Debug
            ////////////////////////////////////////////////////////////////////////
            //string headerSpid = "";
            //foreach (string strKey in Request.Headers.AllKeys)
            //{
            //    headerSpid += strKey + " ==> " + Request.Headers[strKey] + "<br />";
            //}
            //EmailNotify.SendApplicationSpid(headerSpid);
            ////////////////////////////////////////////////////////////////////////
        }

        return fResult;
    }

    protected void CheckSpidSingleSignOn()
    {
        bool fSpidAbilitato = bool.Parse(ConfigurationManager.AppSettings["SpidAbilitato"]);
        ImageButton btnloginSpid = (ImageButton) LoginControl.FindControl("btnloginSpid");
        if (fSpidAbilitato)
        {
            btnloginSpid.Visible = true;
            #region Spid
            if (!Context.User.Identity.IsAuthenticated)
            {
                if (IsSpidRequest())
                {
                    string codiceFiscale = Request.Headers["codicefiscale"];
                    string streemKey = Request.Headers["key"];
                    string streemFirstname = Request.Headers["nome"];
                    string streemLastname = Request.Headers["cognome"];
                    //    //string streemTrustLevel = Request.Headers["trustLevel"];                            //affidabilità utente: Alto - Medio - Basso
                    //    //string streemPolicyLevel = Request.Headers["policyLevel"];                          //livello affidabilità password utente: Alto - Medio - Basso
                    //    //string streemAuthenticationMethod = Request.Headers["authenticationMethod"];
                    string streemLuogoNascita = Request.Headers["luogoNascita"];
                    string streemProvinciaNascita = Request.Headers["provinciaNascita"];

                    if (!string.IsNullOrEmpty(codiceFiscale))
                    {
                        object[] getValSpidSingleSignOn = new object[3];
                        getValSpidSingleSignOn = SecurityManager.SpidSingleSignOn(codiceFiscale);

                        if (bool.Parse(getValSpidSingleSignOn[0].ToString()))
                        {
                            CriterPrincipalSerializeModel serializeModel = new CriterPrincipalSerializeModel();
                            serializeModel.IsSpidUser = true;
                            JavaScriptSerializer serializer = new JavaScriptSerializer();

                            string userData = serializer.Serialize(serializeModel);

                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                     1,
                                     getValSpidSingleSignOn[1].ToString(),
                                     DateTime.Now,
                                     DateTime.Now.AddMinutes(120),
                                     false,
                                     userData);

                            string encTicket = FormsAuthentication.Encrypt(authTicket);
                            HttpCookie faCookie = new HttpCookie("SpidCookie", encTicket);
                            Response.Cookies.Add(faCookie);
                            Logger.LogIt(TipoEvento.LoginSpid);
                            Response.Redirect(ConfigurationManager.AppSettings["UrlPortal"].ToString() + "Default.aspx", true);
                        }
                        else
                        {
                            if (Request.QueryString["type"] == "azn")
                            {
                                QueryString qs = new QueryString();
                                qs.Add("IDTipoSoggetto", "2");
                                qs.Add("fSpid", "True");
                                qs.Add("codicefiscale", codiceFiscale);
                                qs.Add("streemKey", streemKey);
                                QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

                                string url = "~/Iscrizione.aspx";
                                url += qsEncrypted.ToString();
                                Response.Redirect(url);
                            }
                            else if (Request.QueryString["type"] == "ctz")
                            {
                                #region Cittadino
                                int iDSoggettoInsert = UtilitySoggetti.SaveInsertDeleteDatiSoggetti(
                                "insert",
                                null,
                                "",
                                null,
                                8,
                                string.Empty,
                                null,
                                null,
                                string.Empty,
                                string.Empty,
                                string.Empty,
                                string.Empty,
                                null,
                                null,
                                null,
                                streemFirstname,
                                streemLastname,
                                116,
                                UtilityApp.GetDataFromCodiceFiscale(codiceFiscale),
                                UtilityApp.GetComuneFromCodiceCatastale(streemLuogoNascita),
                                UtilityApp.GetIDProvinciaFromSigla(streemProvinciaNascita),
                                null,
                                string.Empty,
                                null,
                                string.Empty,
                                string.Empty,
                                string.Empty,
                                codiceFiscale,
                                string.Empty,
                                string.Empty,
                                string.Empty,
                                string.Empty,
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
                                null,
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
                                
                                UtilitySoggetti.SaveInsertDeleteDatiSoggettiFirmaDigitale(iDSoggettoInsert,
                                                                                            1,
                                                                                            string.Empty,
                                                                                            DateTime.Now,
                                                                                            UtilityApp.GetUserIP(),
                                                                                            string.Empty,
                                                                                            string.Empty,
                                                                                            string.Empty,
                                                                                            string.Empty,
                                                                                            string.Empty,
                                                                                            string.Empty,
                                                                                            string.Empty,
                                                                                            string.Empty);
                                
                                SecurityManager.ActivateUserCredential(iDSoggettoInsert, 8, 13, string.Empty, string.Empty, "SpidCittadino");

                                Response.Headers.Add("codicefiscale", codiceFiscale);
                                Response.Headers.Add("Key", streemKey);
                                Response.Redirect(ConfigurationManager.AppSettings["UrlPortal"].ToString() + "Login.aspx?type=ctz");
                                #endregion
                            }
                        }
                    }
                }
                else
                {
                    //
                }
            }
            #endregion
        }
        else
        {
            btnloginSpid.Visible = false;
        }
    }
    
    protected void LoginCtrl_Authenticate(object sender, AuthenticateEventArgs e)
    {
        TextBox tbxUserName = (TextBox) LoginControl.FindControl("UserName");
        string _username = tbxUserName.Text;
        TextBox tbxPassword = (TextBox) LoginControl.FindControl("Password");
        string _password = tbxPassword.Text;

        Label lblLoginFailureText = (Label) LoginControl.FindControl("FailureText");
        RequiredFieldValidator rfvPassword = (RequiredFieldValidator) LoginControl.FindControl("rfvPassword");

        Label Login_lblTitoloAccesso = (Label) LoginControl.FindControl("Login_lblTitoloAccesso");
        Label Login_lblPasswordScaduta = (Label) LoginControl.FindControl("Login_lblPasswordScaduta");

        Button btnLogin = (Button) LoginControl.FindControl("btnlogin");

        SecurityManager.LoginStatus status = SecurityManager.ValidateUser(_username, _password, "fAttivo");

        switch (status)
        {
            case SecurityManager.LoginStatus.LoginOK:
                e.Authenticated = true;
                Logger.LogIt(TipoEvento.Login);
                // verifica che l'utente sia già connesso
                string message;
                if (SecurityManager.CheckDoubleConnection(LoginControl.UserName, out message))
                {
                    LoginControl.FailureText = message;
                }
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(LoginControl.UserName, false);
                    LoginControl.DestinationPageUrl = "~/Default.aspx";
                }
                break;
            case SecurityManager.LoginStatus.LoginKO:
                //Logger.LogIt(TipoEvento.LoginFallito);
                Logger.LogUser(TipoEvento.LoginFallito, UtilityApp.GetCurrentPageName(), _username, HttpContext.Current.Request.UserHostAddress);
                e.Authenticated = false;
                LoginControl.FailureText = "Accesso negato: controllare username e/o password!";
                tbxUserName.Focus();
                break;
            case SecurityManager.LoginStatus.LoginFailed:
                //Logger.LogIt(TipoEvento.LoginFallito);
                Logger.LogUser(TipoEvento.LoginFallito, UtilityApp.GetCurrentPageName(), _username, HttpContext.Current.Request.UserHostAddress);
                LoginControl.FailureText = "Accesso negato: controllare username e/o password!";
                tbxUserName.Focus();
                break;
            case SecurityManager.LoginStatus.LoginExpired:
                Logger.LogIt(TipoEvento.LoginFallito);
                lblLoginFailureText.Visible = false;
                btnLogin.Visible = false;
                rfvPassword.Enabled = false;
                pnlPassword.Visible = true;
                LoginControl.Visible = false;
                lblPasswordScaduta.Visible = true;
                lblPasswordScaduta.Text = "Attenzione Password scaduta!<br />Imposta una nuova password per il tuo account.";
                txtNewPassword.Focus();
                break;
            case SecurityManager.LoginStatus.LoginInactive:
                e.Authenticated = false;
                lblLoginFailureText.Visible = false;
                btnLogin.Visible = false;
                rfvPassword.Enabled = false;
                pnlPassword.Visible = true;
                LoginControl.Visible = false;
                lblPasswordScaduta.Visible = true;
                lblPasswordScaduta.Text = "Attenzione Account inutilizzato da più di 180 giorni!<br />Imposta una nuova password per il tuo account.";
                tbxUserName.Focus();
                break;
            case SecurityManager.LoginStatus.LoginLocked:
                e.Authenticated = false;
                LoginControl.FailureText = "Accesso negato: account bloccato, superato il numero massimo di tentativi!";
                tbxUserName.Focus();
                break;
        }
    }

    protected void btnloginSpid_Click(object sender, ImageClickEventArgs e)
    {
        string type = Request.QueryString["type"];
        if (string.IsNullOrEmpty(type) || type == "azn")
        {
            Response.Redirect(ConfigurationManager.AppSettings["UrlRequestSpidAzienda"]);
        }
        else
        {
            Response.Redirect(ConfigurationManager.AppSettings["UrlRequestSpidCittadino"]);
        }
    }
    
    protected void btnCambiaPassword_Click(object sender, EventArgs e)
    {
        string newPwd = txtNewPassword.Text.Trim();
        string newConfirmPwd = TxtConfirmNewPassword.Text.Trim();
        string minChar = ConfigurationManager.AppSettings["minCharPwd"];

        TextBox tbxUserName = (TextBox) LoginControl.FindControl("UserName");
        string username = tbxUserName.Text.Trim();

        TextBox tbxPassword = (TextBox) LoginControl.FindControl("Password");
        string password = tbxPassword.Text.Trim();

        SecurityManager.ChangePwdStatus status = SecurityManager.ChangePassword(newPwd, newConfirmPwd, minChar, username);

        switch (status)
        {
            case SecurityManager.ChangePwdStatus.ChangeKo:
                //---errore
                lblErroreCambioPassword.Text = "Errore";
                break;
            case SecurityManager.ChangePwdStatus.ChangeOk:
                //---cambio ok

                pnlPassword.Visible = false;
                tbxPassword.Text = txtNewPassword.Text;

                AuthenticateEventArgs args = new AuthenticateEventArgs();

                args.Authenticated = true;
                FormsAuthentication.RedirectFromLoginPage(LoginControl.UserName, false);
                LoginControl.DestinationPageUrl = "~/Default.aspx";
                break;
            case SecurityManager.ChangePwdStatus.DifferentPwd:
                //---valori diversi
                lblErroreCambioPassword.Text = "Errore - Le password inserite sono diverse.";
                break;
            case SecurityManager.ChangePwdStatus.EmptyPwd:
                //---manca una delle due
                lblErroreCambioPassword.Text = "Errore - Entrambi i campi devono essere obbligatori.";
                break;
            case SecurityManager.ChangePwdStatus.SamePwd:
                //---stessa pwd vecchia
                lblErroreCambioPassword.Text = "Errore - Non è più possibile utilizzare la vecchia password.";
                break;
            case SecurityManager.ChangePwdStatus.ShortPwd:
                //---numero caratteri inferiori alla lunghezza minima
                lblErroreCambioPassword.Text = "Errore - Devi inserire una password di almeno " + minChar + " caratteri.";
                break;
        }
    }

    public void Login_lnkPasswordRecovery_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/RecuperoCredenziali.aspx");
    }

    public void Login_lnkIscrizione_OnClick(object sender, EventArgs e)
    {
        string url = "~/IscrizioneCriter.aspx";
        Response.Redirect(url);
    }

}
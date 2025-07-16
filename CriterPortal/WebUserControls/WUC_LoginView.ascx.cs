using DataUtilityCore;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

public partial class WebUserControls_WUC_LoginView : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoginName loginName = (LoginName)LoginViewer.FindControl("LoginName");
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
                loginName.FormatString = info.Ruolo + "&nbsp;connesso&nbsp;" + info.Utente;
                Label lblCodiceSoggetto = (loginName.FindControl("lblCodiceSoggetto") as Label);
                if (!string.IsNullOrEmpty(info.CodiceSoggetto))
                {
                    lblCodiceSoggetto.Visible = true;
                    string spidAutenticated = "";
                    if (info.fSpid)
                    {
                        spidAutenticated = "&nbsp;-&nbsp;Autenticato con Spid"; //&nbsp;<img class='spidFirmaDigitaleClass' alt='Autenticato con Spid' src='images/spid-logo-c-lb.png' />
                    }
                    else
                    {
                        if (info.IDRuolo == 2)
                        {
                            spidAutenticated = "&nbsp;-&nbsp;Autenticato con Firma Digitale"; //&nbsp;<img class='spidFirmaDigitaleClass' alt='Autenticato con Firma Digitale' src='images/spid-level3-access-icon-a-lb.png' />
                        }
                    }
                    lblCodiceSoggetto.Text = "Codice soggetto numero:&nbsp;" + info.CodiceSoggetto + spidAutenticated;
                }
                else
                {
                    if (info.IDRuolo == 13)
                    {
                        lblCodiceSoggetto.Visible = true;
                        lblCodiceSoggetto.Text = "Autenticato con Spid";
                    }
                    else
                    {
                        lblCodiceSoggetto.Visible = false;
                    }
                }              
            }
        }
    }

    protected void LoginStatus_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Request.Cookies.Clear();
        FormsAuthentication.SignOut();
        Session.Abandon();

        Response.Redirect("~/Default.aspx");
    }

}
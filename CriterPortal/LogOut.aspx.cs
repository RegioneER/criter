using DataUtilityCore;
using System;
using System.Web;
using System.Web.Security;

public partial class LogOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.UnRegisterCurrentUser();
        Logger.LogUser(TipoEvento.Logout, UtilityApp.GetCurrentPageName(), HttpContext.Current.User.Identity.Name, HttpContext.Current.Request.UserHostAddress);
        Request.Cookies.Clear();
        Response.Cookies.Clear();
        FormsAuthentication.SignOut();
        Session.Abandon();
        Response.Redirect("http://energia.regione.emilia-romagna.it/");
    }
}

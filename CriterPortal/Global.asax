<%@ Application Language="C#" %>
<%@ Import Namespace="System.Data.Entity" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Web.Http" %>
<%@ Import Namespace="DataUtilityCore" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>
<%@ Import Namespace="System.Web.Mvc" %>

<script RunAt="server">
    void Application_Start(object sender, EventArgs e)
    {
        //SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));

        RouteTable.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{action}/{id}",
            defaults: new { id = System.Web.Http.RouteParameter.Optional }
        );
    }

    void Application_PostAuthenticateRequest(Object sender, EventArgs e)
    {
        HttpCookie authCookie = Request.Cookies["SpidCookie"];

        if (authCookie != null)
        {
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            CriterPrincipalSerializeModel serializeModel = serializer.Deserialize<CriterPrincipalSerializeModel>(authTicket.UserData);
            CriterPrincipal newUser = new CriterPrincipal(authTicket.Name);
            newUser.IsSpidUser = serializeModel.IsSpidUser;

            HttpContext.Current.User = newUser;
        }
    }

    void Application_End(object sender, EventArgs e)
    {

    }

    void Application_AuthenticateRequest(Object sender, EventArgs e)
    {
        //  check for logged in or not
        if (HttpContext.Current.User != null &&
            HttpContext.Current.User.Identity != null
                && HttpContext.Current.User.Identity.IsAuthenticated)
        {

            //SecurityManager.RegisterCurrentUser();
        }
    }

    void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
    {

    }

    void Application_BeginRequest(object sender, EventArgs e)
    {
        DevExpress.Web.ASPxWebControl.SetIECompatibilityModeEdge();

        #region ReportViewer Fix
        if (Request == null || Request.Cookies == null) {
            return;
        }
        if (Request.Cookies.Count < 10) {
            return;
        }
        for (int i = 0; i < Request.Cookies.Count; ++i)
        {
            if (StringComparer.OrdinalIgnoreCase.Equals(Request.Cookies[i].Name, System.Web.Security.FormsAuthentication.FormsCookieName)) {
                continue;
            }
            if (!Request.Cookies[i].Name.EndsWith("_SKA", System.StringComparison.OrdinalIgnoreCase)) {
                continue;
            }
            if (i > 10) {
                break;
            }

            System.Web.HttpCookie c = new System.Web.HttpCookie(Request.Cookies[i].Name);
            c.Expires = DateTime.Now.AddDays(-1);
            c.Path = "/";
            c.Secure = false;
            c.HttpOnly = true;
            Response.Cookies.Set(c);
        }
        #endregion
    }

    void Application_EndRequest(object sender, EventArgs e)
    {
        DataLayer.Common.ApplicationContext.Current.DisposeContext();
    }

    void Application_Error(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["fActiveNotifyError"] == "on")
        {
            DataUtilityCore.UtilityApp.SetLogApplicationError();
            //EmailNotify.SendApplicationError();
        }
    }

    void Session_Start(object sender, EventArgs e)
    {

    }

    void Session_End(object sender, EventArgs e)
    {
        // ad ogni sessione terminata verifico la lista (almeno 1 ogni 20 minuti)
        SecurityManager.RefreshCurrentUsers();
    }
</script>
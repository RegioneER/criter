using System;

public partial class ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string jScript = "setTimeout('Redirect()',5000); function Redirect() { location.href = 'Default.aspx'; }";
        string jScriptKey = "KeyScript";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), jScriptKey, jScript, true);
    }
}
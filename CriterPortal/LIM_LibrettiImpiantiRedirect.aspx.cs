using EncryptionQS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LIM_LibrettiImpiantiRedirect : System.Web.UI.Page
{
    protected string IDLibrettoImpianto
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
            return "";
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            QueryString qs = new QueryString();
            qs.Add("IDLibrettoImpianto", IDLibrettoImpianto);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "LIM_LibrettiImpianti.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
    }
}
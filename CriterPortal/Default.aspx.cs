using DataUtilityCore;
using System;
using System.Web;

public partial class Default : System.Web.UI.Page
{
    UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Richiesta firma Privacy
            if (userInfo.IDRuolo == 3 || userInfo.IDRuolo == 5 || userInfo.IDRuolo == 2)
            {
                if (userInfo.IDRuolo == 2) //Admin Azienda
                {
                    int iDSoggetto = int.Parse(userInfo.IDSoggetto.ToString());
                    bool fPrivacy = UtilitySoggetti.GetSetPrivacySoggetti(iDSoggetto);

                    if (!fPrivacy)
                    {
                        Response.Redirect("~/CheckCriterPrivacy.aspx");
                    }
                }
                else if (userInfo.IDRuolo == 3 || userInfo.IDRuolo == 5) //Manutentore o Responsabile
                {
                    int iDSoggetto = int.Parse(userInfo.IDSoggettoDerived.ToString());
                    bool fPrivacy = UtilitySoggetti.GetSetPrivacySoggetti(iDSoggetto);
                }
            }
        }
    }
}
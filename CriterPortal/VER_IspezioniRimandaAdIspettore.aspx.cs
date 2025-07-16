using DataUtilityCore;
using EncryptionQS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VER_IspezioniRimandaAdIspettore : System.Web.UI.Page
{
    protected string IDIspezione
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

    UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnRimandaIspezioneAdIspettore_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            bool usaPec = bool.Parse(ConfigurationManager.AppSettings["NotificheIspezioniUsaEmailPec"]);

            long iDIspezione = long.Parse(IDIspezione);
            UtilityVerifiche.SaveNotificaIspettoreAperturaIspezione(iDIspezione, txtNotificaAdIspettore.Text, (int)userInfo.IDUtente);
            EmailNotify.SendMailPerIspettore_NotificaRiaperturaIspezione(iDIspezione, usaPec);
            UtilityVerifiche.CambiaStatoIspezione(iDIspezione, 7);
            UtilityVerifiche.StoricizzaStatoIspezione(iDIspezione, int.Parse(userInfo.IDUtente.ToString()));

            tblInfoGenerali.Visible = false;
            tblInfoNotificaOk.Visible = true;
        }
    }


}
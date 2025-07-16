using DataUtilityCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUserControls_Ispezioni_UCIspezioneNotificaAperturaIspezione : System.Web.UI.UserControl
{
    public string IDIspezione
    {
        get { return lblIDIspezione.Text; }
        set
        {
            lblIDIspezione.Text = value;
        }
    }


    public override void DataBind()
    {
        base.DataBind();
        GetDatiNotifiche();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetDatiNotifiche();
    }

    protected void GetDatiNotifiche()
    {
        int iDIspezione = int.Parse(IDIspezione);

        var notifiche = UtilityVerifiche.GetIspezioneNotificaRiaperturaIspezione(iDIspezione);
        gridIspezioniNotifiche.DataSource = notifiche;
        gridIspezioniNotifiche.DataBind();
    }


}
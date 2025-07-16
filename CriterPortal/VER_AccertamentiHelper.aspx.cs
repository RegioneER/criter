using DataUtilityCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VER_AccertamentiHelper : System.Web.UI.Page
{

    protected string tipo
    {
        get
        {
            return Request.QueryString["tipo"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (tipo == "1")
        {
            gridPrescrizioni.Visible = false;
            gridRaccomandazioni.DataSource = LoadDropDownList.LoadDropDownList_SYS_RCTTipologiaRaccomandazione(null, null);
            gridRaccomandazioni.DataBind();
        }
        else if (tipo == "2")
        {
            gridRaccomandazioni.Visible = false;
            gridPrescrizioni.DataSource = LoadDropDownList.LoadDropDownList_SYS_RCTTipologiaPrescrizione(null, null);
            gridPrescrizioni.DataBind();
        }
    }
}
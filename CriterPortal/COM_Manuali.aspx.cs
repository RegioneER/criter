using DataUtilityCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class COM_Manuali : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PagePermission();
        }
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                row4.Visible = true;
                row5.Visible = true;
                row6.Visible = true;
                break;
            case "4": //Distributori Energia
                row0.Visible = false;
                row1.Visible = false;
                row2.Visible = false;
                row3.Visible = false;
                row4.Visible = true;
                row5.Visible = true;
                break;
        }
    }

}
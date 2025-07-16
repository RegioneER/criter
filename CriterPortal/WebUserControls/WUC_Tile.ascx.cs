using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataUtilityCore;
using DataLayer;

public partial class WebUserControls_WUC_Tile : System.Web.UI.UserControl
{
    UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTile(info.IDUtente, info.IDRuolo);
        }
    }

    public void BindTile(int? IDUtente, int? IDRuolo)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            var source = ctx.V_MNU_Menu.Where(s => (s.fattivoMenu == true)
                                            && (s.fTile == true)
                                            && (s.fPrivate == true)
                                            && (s.fattivoAccessoUtente == true)
                                            && (s.fattivoUtente == true)
                                            && (s.IDUtente == IDUtente)
                                            && (s.IDRuolo == IDRuolo)
                                            ).OrderBy(s => s.TileOrdine).ToList();

            DLTile.DataSource = source;
            DLTile.DataBind();
            if (source.Count > 0)
            {
                DLTile.Items[0].Focus();
            }
        }
    }
}
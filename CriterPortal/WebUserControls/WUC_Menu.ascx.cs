using System;
using System.Web.UI.WebControls;
using DataUtilityCore;
using System.Web;
using System.Linq;
using DataLayer;

public partial class WebUserControls_WUC_Menu : System.Web.UI.UserControl
{
    UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMenu(info.IDUtente, info.IDRuolo);
        }
    }

    public void rptMenu_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            dynamic menu = e.Item.DataItem as dynamic;
            int IDMenu = menu.IDMenu;

            HyperLink hlprvMenu = (HyperLink) e.Item.FindControl("hlprvMenu");

            string url = "~/" + menu.PageUrl;

            //QueryString qs = new QueryString();
            //qs.Add("UtenteId", string.Empty);
            //QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            //url = "~/" + menu.PageUrl;
            //url += qsEncrypted.ToString();

            hlprvMenu.NavigateUrl = url;

            Repeater rptMenuSotto = (Repeater) e.Item.FindControl("rptMenuSotto");
            BindMenuSotto(info.IDUtente, info.IDRuolo, IDMenu, rptMenuSotto);
        }
    }

    public void rptMenuSotto_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            dynamic menu = e.Item.DataItem as dynamic;
            int IDMenu = menu.IDMenu;

            HyperLink hlMenuSotto = (HyperLink) e.Item.FindControl("hlMenuSotto");

            string url = "~/" + menu.PageUrl;
            //QueryString qs = new QueryString();
            //qs.Add("UtenteId", string.Empty);
            //QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            //url = "~/" + menu.PageUrl;
            //url += qsEncrypted.ToString();
            hlMenuSotto.NavigateUrl = url;
        }
    }

    public void BindMenu(int? iDUtente, int? iDRuolo)
    {
        bool fPrivate = false;
        bool? fattivoAccessoUtente = null;
        bool? fattivoUtente = null;

        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            fPrivate = true;
            fattivoAccessoUtente = true;
            fattivoUtente = true;
        }

        using (CriterDataModel ctx = new CriterDataModel())
        {
            var source = ctx.V_MNU_Menu.Where(s => (s.fattivoMenu == true)
                                    && (s.fPrivate == fPrivate)
                                    && ((s.fattivoAccessoUtente == fattivoAccessoUtente) || (s.fattivoAccessoUtente == null))
                                    && ((s.fattivoUtente == fattivoUtente) || (s.fattivoUtente == null))
                                    && (s.IDUtente == iDUtente)
                                    && (s.IDRuolo == iDRuolo)
                                    && (s.IDMenuParent == null)
                                    ).OrderBy(s => s.Ordine).ToList();

            rptMenu.DataSource = source;
            rptMenu.DataBind();
        }         
    }

    public void BindMenuSotto(int? iDUtente, int? iDRuolo, int? iDMenu, Repeater rptMenuSotto)
    {
        bool fPrivate = false;
        bool? fattivoAccessoUtente = null;
        bool? fattivoUtente = null;

        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            fPrivate = true;
            fattivoAccessoUtente = true;
            fattivoUtente = true;
        }

        using (CriterDataModel ctx = new CriterDataModel())
        {
            var source = ctx.V_MNU_Menu.Where(s => (s.fattivoMenu == true)
                                        && (s.fPrivate == fPrivate)
                                        && ((s.fattivoAccessoUtente == fattivoAccessoUtente) || (s.fattivoAccessoUtente == null))
                                        && ((s.fattivoUtente == fattivoUtente) || (s.fattivoUtente == null))
                                        && (s.IDUtente == iDUtente)
                                        && (s.IDRuolo == iDRuolo)
                                        && (s.IDMenuParent == iDMenu)
                                        ).OrderBy(s => s.Ordine).ToList();

            if (source.Count > 0)
            {
                rptMenuSotto.Visible = true;
                rptMenuSotto.DataSource = source;
                rptMenuSotto.DataBind();
            }
            else
            {
                rptMenuSotto.Visible = false;
            }
        }
    }
}
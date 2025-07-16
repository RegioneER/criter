using DataLayer;
using DataUtilityCore;
using DataUtilityCore.Enum;
using DevExpress.Web;
using EncryptionQS;
using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VER_Sanzioni : Page
{

    protected string IDAccertamento
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

    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);

        //sm.RegisterPostBackControl(fileManagerDocumenti);
        //sm.EnablePartialRendering = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetDatiSanzioni(long.Parse(IDAccertamento));
            //GetDatiAccertamentoInterventoStorico(long.Parse(IDAccertamento));
            //GetDatiRaccomandate(long.Parse(IDAccertamento));
        }
    }

    protected void GetSanzioniUserControll(string iDAccertamento, string iDTipoAccertamento)
    {
        UCSanzione.IDAccertamento = iDAccertamento;
        UCSanzione.TipoPageSanzione = "2";
        //UCSanzione.UserControlButtonClicked += new
        //            EventHandler(UCSanzione_UserControlButtonClicked);
    }
    

    public void GetDatiSanzioni(long iDAccertamento)
    {
        using (var ctx = new CriterDataModel())
        {
            var sanzione = ctx.V_VER_Accertamenti.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();
            if (sanzione != null)
            {
                //lblIDTipoAccertamento.Text = intervento.IDTipoAccertamento.ToString();
                GetSanzioniUserControll(iDAccertamento.ToString(), sanzione.IDTipoAccertamento.ToString());

                //lblIDStatoAccertamentoSanzione.Text = intervento.IDStatoAccertamentoSanzione.ToString();
            }
        }
    }
            
}
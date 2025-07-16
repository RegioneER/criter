using DataLayer;
using DataUtilityCore;
using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;

public partial class ConfermaAccertamento : System.Web.UI.Page
{
    protected string guidAccertamento
    {
        get
        {
            return Request.QueryString["guidAccertamento"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            using (var ctx = new CriterDataModel())
            {
                bool faccertamento = ctx.VER_Accertamento.Where(c => c.GuidAccertamento == guidAccertamento && c.RispostaEmail == null && c.fEmailConfermaAccertamento == true).Any();

                if (faccertamento)
                {
                    tblConfirmaAccertamento.Visible = true;
                }
                else
                {
                    tblConfirmaAccertamento.Visible = false;
                    tblConfermaAccertamentoGiaEffettuato.Visible = true;

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + ConfigurationManager.AppSettings["UrlPortal"].ToString() + "';},4000);", true);
                }
            }
        }
    }

    protected void btnConfermaAccertamento_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            using (var ctx = new CriterDataModel())
            {
                var accertamento = ctx.VER_Accertamento.Where(c => c.GuidAccertamento == guidAccertamento).FirstOrDefault();
                UtilityVerifiche.SaveAccertamento(accertamento.IDAccertamento,
                                                  accertamento.IDDistributore,
                                                  accertamento.Note,
                                                  accertamento.fEmailConfermaAccertamento,
                                                  accertamento.DataInvioEmail,
                                                  accertamento.TestoEmail,
                                                  accertamento.GiorniRealizzazioneInterventi,
                                                  txtRispostaAccertamento.Text
                                                 );

                tblConfirmaAccertamento.Visible = false;
                tblConfermaAccertamentoOk.Visible = true;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + ConfigurationManager.AppSettings["UrlPortal"].ToString() + "';},4000);", true);
            }
        }
    }

}
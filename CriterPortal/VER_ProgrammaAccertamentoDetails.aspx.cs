using DataLayer;
using DevExpress.Web;
using EncryptionQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VER_ProgrammaAccertamentoDetails : System.Web.UI.Page
{
    protected string IDProgrammaAccertamento
    {
        get
        {
            return (string)Request.QueryString["IDProgrammaAccertamento"];
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetListAccertamenti();
        }
    }

    public void GetListAccertamenti()
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            int iDProgrammaAccertamentoInt = int.Parse(IDProgrammaAccertamento);


            var accertamenti = (from VER_Accertamento in ctx.VER_Accertamento
                               join VER_AccertamentoProgramma in ctx.VER_AccertamentoProgramma on VER_Accertamento.IDAccertamento equals VER_AccertamentoProgramma.IDAccertamento
                               join LIM_TargatureImpianti in ctx.LIM_TargatureImpianti on VER_Accertamento.IDTargaturaImpianto equals LIM_TargatureImpianti.IDTargaturaImpianto
                               where VER_AccertamentoProgramma.IDProgrammaAccertamento == iDProgrammaAccertamentoInt
                                select new
                               {
                                   VER_AccertamentoProgramma.IDAccertamentoProgramma,
                                   VER_AccertamentoProgramma.IDProgrammaAccertamento,
                                    VER_Accertamento.IDAccertamento,
                                    VER_Accertamento.CodiceAccertamento,
                                   VER_Accertamento.PunteggioNCAccertamento,
                                   LIM_TargatureImpianti.CodiceTargatura
                               }).ToList();

            GridAccertamenti.DataSource = accertamenti;
            GridAccertamenti.DataBind();
        }
    }


    protected void GridAccertamenti_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            HyperLink lnkAccertamento = GridAccertamenti.FindRowCellTemplateControl(e.VisibleIndex, null, "lnkAccertamento") as HyperLink;
            lnkAccertamento.Text = e.GetValue("CodiceAccertamento").ToString();

            QueryString qs = new QueryString();
            qs.Add("IDAccertamento", e.GetValue("IDAccertamento").ToString());
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "VER_Accertamenti.aspx";
            url += qsEncrypted.ToString();

            lnkAccertamento.NavigateUrl = url;
            lnkAccertamento.Target = "_blank";
        }
    }

    protected void GridAccertamenti_PageIndexChanged(object sender, EventArgs e)
    {
        var view = sender as ASPxGridView;
        if (view == null) return;
        var pageIndex = view.PageIndex;
        GridAccertamenti.PageIndex = pageIndex;
        GetListAccertamenti();
    }
}
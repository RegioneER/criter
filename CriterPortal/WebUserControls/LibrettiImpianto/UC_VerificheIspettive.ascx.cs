using DataUtilityCore;
using DevExpress.Web;
using System;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

public partial class WebUserControls_LibrettiImpianto_UC_VerificheIspettive : System.Web.UI.UserControl
{
    public int IDTargaturaImpianto
    {
        get;
        set;
    }
        
    protected void Page_Load(object sender, EventArgs e)
    {
        var result = UtilityLibrettiImpianti.ListVerificheIspettiveLibretto(IDTargaturaImpianto);
        DataGrid.DataSource = result;
        DataGrid.DataBind();
    }

    protected void DataGrid_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            int IDIspezione = int.Parse((e.GetValue("IDIspezione").ToString()));
            ImageButton ImgPdf = DataGrid.FindRowCellTemplateControl(e.VisibleIndex, null, "ImgPdf") as ImageButton;
            ImgPdf.Attributes.Add("onclick", "OpenPopupWindowRapportiRVI(this, " + IDIspezione + "); return false;");
        }
    }
}
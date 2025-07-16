using DataUtilityCore;
using DevExpress.Web;
using System;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

public partial class WebUserControls_LibrettiImpianto_UC_VerifichePeriodicheGT : System.Web.UI.UserControl
{
    public int IDTargaturaImpianto
    {
        get;
        set;
    }

    public string prefisso
    {
        get;
        set;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var result = UtilityLibrettiImpianti.ListVerificheGT(IDTargaturaImpianto, prefisso);
        DataGrid.DataSource = result;
        DataGrid.DataBind();
    }

    protected void DataGrid_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            int IDRapportoDiControllo = int.Parse((e.GetValue("Id").ToString()));
            ImageButton ImgPdf = DataGrid.FindRowCellTemplateControl(e.VisibleIndex, null, "ImgPdf") as ImageButton;
            ImgPdf.Attributes.Add("onclick", "OpenPopupWindowRapporti(this, " + IDRapportoDiControllo + "); return false;");
        }
    }
}
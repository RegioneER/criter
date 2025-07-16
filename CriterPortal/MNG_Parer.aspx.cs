using DataUtilityCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MNG_Parer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetLogParer();
        }
    }

    public void GetLogParer()
    {
        DataGrid.DataSource = UtilityParer.GetDataParerLog(ddlEsito.SelectedItem.Value, 
                                                            txtObjectPec.Text,
                                                            txtDataInvioDa.Text,
                                                            txtDataInvioAl.Text
                                                          );
        DataGrid.DataBind();
    }





    #region ESITI PARER






    protected void DataGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //DataRowView rowView = (DataRowView)e.Row.DataItem;
            //string IDLogSiape = rowView["IDLogSiape"].ToString();

            //ImageButton imgDownloadXmlSiape = e.Row.FindControl("imgDownloadXmlSiape") as ImageButton;
            //if (imgDownloadXmlSiape != null)
            //{
            //    imgDownloadXmlSiape.Attributes.Add("onclick", "dhtmlwindow.open('InfoExport_" + IDLogSiape + "', 'iframe', '" + "CERT_SiapeExportXml.aspx?IDLogSiape=" + IDLogSiape + "', 'Esporta ape Siape', 'width=0px,height=0px,resize=1,scrolling=1,center=1'); return false");
            //    imgDownloadXmlSiape.Attributes.Add("style", "cursor: pointer;");
            //}
        }
    }

    protected void DataGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataGrid.PageIndex = e.NewPageIndex;
        GetLogParer();
    }



    #endregion



    protected void btnRicerca_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetLogParer();
        }
    }




    
}
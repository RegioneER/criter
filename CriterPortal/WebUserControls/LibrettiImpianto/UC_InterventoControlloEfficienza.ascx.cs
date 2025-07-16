using System;
using System.Web.UI.WebControls;
using DataUtilityCore;

public partial class UC_InterventoControlloEfficienza : CriterUserControl
{
    //public int IDTargaturaImpianto
    //{
    //    get;
    //    set;
    //}

    public int IDTargaturaImpianto
    {
        get { return int.Parse(lblIDTargaturaImpianto.Text); }
        set
        {
            lblIDTargaturaImpianto.Text = value.ToString();
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        var result = UtilityLibrettiImpianti.ListVerificheInterventiControlloEfficienzaEnergetica(IDTargaturaImpianto);
        this.grdGrigliaInterventi.DataSource = result;
        this.DataBind();
    }


    protected void grdGrigliaInterventi_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        //GridView grid = sender as GridView;

        //if (e.Row.RowType == DataControlRowType.Footer)
        //{

        //}
        //else if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    dynamic row = e.Row.DataItem;

        //    RCT_UC_Checkbox UCCheckboxRaccomandazioni = e.Row.FindControl("UCCheckboxRaccomandazioni") as RCT_UC_Checkbox;
        //    if (UCCheckboxRaccomandazioni != null)
        //    {
        //        UCCheckboxRaccomandazioni.Value = (!string.IsNullOrEmpty(row.Raccomandazioni)) ? DataUtilityCore.EnumStatoSiNoNc.Si : DataUtilityCore.EnumStatoSiNoNc.No;
        //    }

        //    RCT_UC_Checkbox UCCheckboxPrescrizioni = e.Row.FindControl("UCCheckboxPrescrizioni") as RCT_UC_Checkbox;
        //    if (UCCheckboxPrescrizioni != null)
        //    {
        //        UCCheckboxPrescrizioni.Value = (!string.IsNullOrEmpty(row.Prescrizioni)) ? DataUtilityCore.EnumStatoSiNoNc.Si : DataUtilityCore.EnumStatoSiNoNc.No;
        //    }

        //}
        //else if (e.Row.RowType == DataControlRowType.Header)
        //{

        //}
    }
}
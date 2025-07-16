using DataUtilityCore;
using DevExpress.Web;
using System;

public partial class WebUserControls_RapportiControlloTecnico_UCBolliniView : System.Web.UI.UserControl
{
    
    public string IDRapportoControlloTecnico
    {
        get { return lblIDRapportoControlloTecnico.Text; }
        set
        {
            lblIDRapportoControlloTecnico.Text = value;
        }
    }
    
    public override void DataBind()
    {
        base.DataBind();
        GetDatiAll();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        GetDatiAll();
    }

    protected void GetDatiAll()
    {
        long iDRapportoControlloTecnico = 0;

        if ((long.TryParse(lblIDRapportoControlloTecnico.Text, out iDRapportoControlloTecnico)))
        {
            var bollini = UtilityBollini.GetRapportoControlloBolliniAssociati(iDRapportoControlloTecnico);
            gridBollini.DataSource = bollini;
            gridBollini.DataBind();
        }
    }


}
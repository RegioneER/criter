using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DataUtilityCore;
using DevExpress.Web;

public partial class RCT_UC_UCBolliniSelector : System.Web.UI.UserControl
{

    public event EventHandler Selezioneterminata;

    public decimal ImportoRichiesto
    {
        get
        {
            if (ViewState["CostoBollini"] != null)
                return (decimal) ViewState["CostoBollini"];
            return 0;
        }
        set
        {
            ViewState["CostoBollini"] = value;
        }
    }

    public decimal ImportoTotaleSelezionati
    {
        get
        {
            if (ViewState["BolliniSelezionati"] != null)
                return GetImportoTotaleBolliniSelezionati();
            return -1;
        }
        set
        {
            lblImportoTotaleSelezionati.Text = value.ToString();
        }
    }

    public string IDSoggetto
    {
        get { return lblIDSoggetto.Text; }
        set
        {
            lblIDSoggetto.Text = value;
        }
    }
    
    public string IDSoggettoDerived
    {
        get { return lblIDSoggettoDerived.Text; }
        set
        {
            lblIDSoggettoDerived.Text = value;
        }
    }
    
    public List<long> SelectedValues
    {
        get
        {
            if (ViewState["BolliniSelezionati"] == null)
            {
                ViewState["BolliniSelezionati"] = new List<long>();
            }
            return (List<long>) ViewState["BolliniSelezionati"];
        }
        set
        {
            ViewState["BolliniSelezionati"] = value;
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
        int iDSoggetto = 0;

        if ((int.TryParse(lblIDSoggetto.Text, out iDSoggetto)))
        {
            int iDSoggettoDerived = 0;
            bool fIDSoggettoDerived = int.TryParse(lblIDSoggettoDerived.Text, out iDSoggettoDerived);

            var query = UtilityBollini.GetBolliniUtilizzabili(iDSoggetto, iDSoggettoDerived, true);
            gridSelezioneManuale.DataSource = query;
            gridSelezioneManuale.DataBind();

            AggiornaBollini(gridSelezioneManuale);
        }
    }

    protected void AggiornaBollini(ASPxGridView grid)
    {
        SelectedValues.Clear();
        
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            if (grid.Selection.IsRowSelected(i))
            {
                SelectedValues.Add(long.Parse(grid.GetRowValues(i, "IDBollinoCalorePulito").ToString()));
            }
        }

        SelectedValues = gridSelezioneManuale.GetSelectedFieldValues("IDBollinoCalorePulito").Cast<long>().ToList();
        if (SelectedValues.Count > 0)
        {
            lblImportoTotaleSelezionati.Text = string.Format("Hai selezionato un importo pari a {0} €", ImportoTotaleSelezionati.ToString());
        }
        else
        {
            lblImportoTotaleSelezionati.Text = string.Empty;
        }
    }

    protected decimal GetImportoTotaleBolliniSelezionati()
    {
        var sum = gridSelezioneManuale.GetSelectedFieldValues("CostoBollino").Cast<decimal>().ToList().Sum();

        //decimal sum = 0;

        //for (int i = 0; i < grid.VisibleRowCount; i++)
        //{
        //    if (grid.Selection.IsRowSelected(i))
        //    {
        //        sum += (decimal)grid.GetRowValues(i, "CostoBollino");
        //    }
        //}

        return sum;
    }

    protected void gridSelezioneManuale_SelectionChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        AggiornaBollini(grid);
    }
    
}
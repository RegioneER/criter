using DataLayer;
using DataUtilityCore;
using DevExpress.Web;
using System;
using System.Linq;
using DataUtilityCore.Portafoglio;

public partial class MNG_PortafoglioSaldo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PagePermission();
            cmbAziende.Focus();
        }
        
        PortafoglioAzienda(cmbAziende.Value);    
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                cmbAziende.Visible = true;
                lblSoggetto.Visible = false;
                break;
            case "2": //Azienda
                cmbAziende.Value = getVal[0];
                cmbAziende.Visible = false;
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0]);
                lblSoggetto.Visible = true;
                break;
        }
    }

    #region AZIENDA

    protected void ASPxComboBox_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "2", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;
        int IdSoggetto = int.Parse(e.Value.ToString());
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "2", e.Value.ToString(), "");
        comboBox.DataBind();
    }

    protected void ASPxComboBox1_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBox();
        SaldoPortafoglio.Text = "";
        NumeroBolliniDisponibili.Text = "";
        NumeroBolliniUtilizzati.Text = "";
    }

    protected void RefreshAspxComboBox()
    {
        cmbAziende.SelectedIndex = -1;
        cmbAziende.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "2", "", "", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
        cmbAziende.DataBind();
    }
    #endregion

    public void PortafoglioAzienda(object iDSoggetto)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            if(cmbAziende.Value != null)
            {
                int iDSoggettoInt = int.Parse(cmbAziende.Value.ToString());

                var getTotalePortafoglio = Portafoglio.GetSaldoPortafoglio(iDSoggettoInt);
                decimal saldototale = getTotalePortafoglio;
                SaldoPortafoglio.Text = saldototale.ToString() + "&nbsp;" + "€";

                var bolliniUtilizzabili = UtilityBollini.GetBolliniUtilizzabili(iDSoggettoInt, null, false);
                int Utilizzabili = bolliniUtilizzabili.Count();
                NumeroBolliniDisponibili.Text = Utilizzabili.ToString();

                var bolliniUtilizzati = UtilityBollini.GetBolliniUtilizzati(iDSoggettoInt);
                int Utilizzati = bolliniUtilizzati.Count();
                NumeroBolliniUtilizzati.Text = Utilizzati.ToString();
            }    
        }
    }
}
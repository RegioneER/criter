using DataUtilityCore;
using DevExpress.Web;
using EncryptionQS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUserControls_Ispezioni_UCRaccomandazioniPrescrizioniIspezione : System.Web.UI.UserControl
{
    protected string IDIspezione
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

    public string fActive
    {
        get
        {
            return lblfActive.Text;
        }
        set
        {
            lblfActive.Text = value;
        }
    }

    public string iDTipologiaRaccomandazionePrescrizioneIspezione
    {
        get
        {
            return lbliDTipologiaRaccomandazionePrescrizioneIspezione.Text;
        }
        set
        {
            lbliDTipologiaRaccomandazionePrescrizioneIspezione.Text = value;
        }
    }

    public override void DataBind()
    {
        base.DataBind();
        GetDatiAll();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        int idtipologiaRaccomandazionePrescrizioneIspezione = int.Parse(iDTipologiaRaccomandazionePrescrizioneIspezione);

        GetlbRaccomandazioni(null, idtipologiaRaccomandazionePrescrizioneIspezione);
        GetlbPrescrizioni(null, idtipologiaRaccomandazionePrescrizioneIspezione);
        SetHelperASPxListBox();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!this.Visible)
        //{
        //    if (!string.IsNullOrEmpty(IDIspezione))
        //    {
        //        lbPrescrizioni.UnselectAll();
        //        lbRaccomandazioni.UnselectAll();
        //        SaveRaccomandazioniPrescrizioni();
        //    }
        //}

        if (!string.IsNullOrEmpty(fActive) && !string.IsNullOrEmpty(IDIspezione))
        {           
            if (fActive == "0")
            {
                lbPrescrizioni.UnselectAll();
                lbRaccomandazioni.UnselectAll();
                SaveRaccomandazioniPrescrizioni();
            }
        }

        if (!string.IsNullOrEmpty(IDIspezione) && !Page.IsPostBack)
        {
            GetDatiAll();
        }
    }

    protected void GetDatiAll()
    {
        if (!string.IsNullOrEmpty(IDIspezione))
        {
            long idIspezione = long.Parse(IDIspezione);
            int idtipologiaRaccomandazionePrescrizioneIspezione = int.Parse(iDTipologiaRaccomandazionePrescrizioneIspezione);

            GetDatiIspezioneRaccomandazioni(idIspezione, idtipologiaRaccomandazionePrescrizioneIspezione);
            GetDatiIspezionePrescrizioni(idIspezione, idtipologiaRaccomandazionePrescrizioneIspezione);

            EnabledDisabledPrescrizioniRaccomandazioni();
        }
    }

    protected void SetHelperASPxListBox()
    {
        if (lbRaccomandazioni.Items.Count > 0)
        {
            cellRaccomandazioni.Visible = true;
        }
        else
        {
            cellRaccomandazioni.Visible = false;
        }

        if (lbPrescrizioni.Items.Count > 0)
        {
            cellPrescrizioni.Visible = true;
        }
        else
        {
            cellPrescrizioni.Visible = false;
        }

        if ((lbRaccomandazioni.Items.Count > 0) && (lbPrescrizioni.Items.Count > 0))
        {
            lblRaccomandazioniPrescrizioni.Visible = true;
        }
        else if (lbRaccomandazioni.Items.Count > 0)
        {
            lblRaccomandazioni.Visible = true;
        }
        else if (lbPrescrizioni.Items.Count > 0)
        {
            lblPrescrizioni.Visible = true;
        }
    }
    
    #region Load Raccomadanzioni/Prescrizioni
    public void GetlbRaccomandazioni(int? iDPresel, int? iDTipologiaRaccomandazionePrescrizioneIspezione)
    {
        lbRaccomandazioni.Items.Clear();

        lbRaccomandazioni.ValueField = "IDTipologiaRaccomandazione";
        lbRaccomandazioni.TextField = "Raccomandazione";
        lbRaccomandazioni.DataSource = LoadDropDownList.LoadDropDownList_SYS_RCTTipologiaRaccomandazione(iDPresel, iDTipologiaRaccomandazionePrescrizioneIspezione);
        lbRaccomandazioni.DataBind();
    }

    public void GetlbPrescrizioni(int? iDPresel, int? iDTipologiaRaccomandazionePrescrizioneIspezione)
    {
        lbPrescrizioni.Items.Clear();

        lbPrescrizioni.ValueField = "IDTipologiaPrescrizione";
        lbPrescrizioni.TextField = "Prescrizione";
        lbPrescrizioni.DataSource = LoadDropDownList.LoadDropDownList_SYS_RCTTipologiaPrescrizione(iDPresel, iDTipologiaRaccomandazionePrescrizioneIspezione);
        lbPrescrizioni.DataBind();
    }
    #endregion

    #region Get Dati Raccomandazioni/Prescrizioni
    public void GetDatiIspezioneRaccomandazioni(long iDIspezione, int iDTipologiaRaccomandazionePrescrizioneIspezione)
    {
        var result = UtilityVerifiche.GetValoriIspezioneRaccomandazioniPrescrizioni(iDIspezione, iDTipologiaRaccomandazionePrescrizioneIspezione, "Raccomandazioni");

        foreach (var row in result)
        {
            if (lbRaccomandazioni.Items.Count > 0)
            {
                lbRaccomandazioni.Items.FindByValue(row.IDTipologiaRaccomandazioneIspezione.ToString()).Selected = true;
            }
        }
    }

    public void GetDatiIspezionePrescrizioni(long iDIspezione, int iDTipologiaRaccomandazionePrescrizioneIspezione)
    {
        var result = UtilityVerifiche.GetValoriIspezioneRaccomandazioniPrescrizioni(iDIspezione, iDTipologiaRaccomandazionePrescrizioneIspezione, "Prescrizioni");

        foreach (var row in result)
        {
            if (lbPrescrizioni.Items.Count > 0)
            {
                lbPrescrizioni.Items.FindByValue(row.IDTipologiaPrescrizioneIspezione.ToString()).Selected = true;
            }
        }
    }
    #endregion

    #region Save Dati Raccomandazioni/Prescrizioni
    public void SaveRaccomandazioni(long iDIspezione, int iDTipologiaRaccomandazionePrescrizioneIspezione)
    {
        List<object> valoriRaccomadanzioni = new List<object>();
        foreach (ListEditItem item in lbRaccomandazioni.Items)
        {
            if (item.Selected)
            {
                valoriRaccomadanzioni.Add(item.Value);
            }
        }

        UtilityVerifiche.SaveInsertDeleteDatiRaccomandazioniPrescrizioni(iDIspezione, iDTipologiaRaccomandazionePrescrizioneIspezione, valoriRaccomadanzioni.ToArray<object>(), "Raccomandazioni");
    }

    public void SavePrescrizioni(long iDIspezione, int iDTipologiaRaccomandazionePrescrizioneIspezione)
    {
        List<object> valoriPrescrizioni = new List<object>();
        foreach (ListEditItem item in lbPrescrizioni.Items)
        {
            if (item.Selected)
            {
                valoriPrescrizioni.Add(item.Value);
            }
        }

        UtilityVerifiche.SaveInsertDeleteDatiRaccomandazioniPrescrizioni(iDIspezione, iDTipologiaRaccomandazionePrescrizioneIspezione, valoriPrescrizioni.ToArray<object>(), "Prescrizioni");
    }

    public void SaveRaccomandazioniPrescrizioni()
    {
        long idispezione = long.Parse(IDIspezione);
        int idtipologiaRaccomandazionePrescrizioneIspezione = int.Parse(iDTipologiaRaccomandazionePrescrizioneIspezione);
        SaveRaccomandazioni(idispezione, idtipologiaRaccomandazionePrescrizioneIspezione);
        SavePrescrizioni(idispezione, idtipologiaRaccomandazionePrescrizioneIspezione);
    }
    #endregion

    protected void EnabledDisabledPrescrizioniRaccomandazioni()
    {
        if ((lbRaccomandazioni.SelectedItems.Count > 0) && (lbPrescrizioni.SelectedItems.Count == 0))
        {
            lbPrescrizioni.Enabled = false;
            lbRaccomandazioni.Enabled = true;
        }
        else if ((lbPrescrizioni.SelectedItems.Count > 0) && (lbRaccomandazioni.SelectedItems.Count == 0))
        {
            lbPrescrizioni.Enabled = true;
            lbRaccomandazioni.Enabled = false;
        }
        else if ((lbRaccomandazioni.SelectedItems.Count == 0) && (lbPrescrizioni.SelectedItems.Count == 0))
        {
            lbPrescrizioni.Enabled = true;
            lbRaccomandazioni.Enabled = true;
        }
    }

    protected void lbRaccomandazioni_SelectedIndexChanged(object sender, EventArgs e)
    {
        EnabledDisabledPrescrizioniRaccomandazioni();
        SaveRaccomandazioniPrescrizioni();        
    }

    protected void lbPrescrizioni_SelectedIndexChanged(object sender, EventArgs e)
    {
        EnabledDisabledPrescrizioniRaccomandazioni();
        SaveRaccomandazioniPrescrizioni();       
    }

}
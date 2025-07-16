using DataUtilityCore;
using DevExpress.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

public partial class WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni : UserControl
{
    public string iDRapportoControlloTecnico
    {
        get
        {
            return lbliDRapportoControlloTecnico.Text;
        }
        set
        {
            lbliDRapportoControlloTecnico.Text = value;
        }
    }

    public string iDTipologiaRaccomandazionePrescrizioneRct
    {
        get
        {
            return lbliDTipologiaRaccomandazionePrescrizioneRct.Text;
        }
        set
        {
            lbliDTipologiaRaccomandazionePrescrizioneRct.Text = value;
        }
    }
    
    public override void DataBind()
    {
        base.DataBind();
        GetDatiAll();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{ 
        int idtipologiaRaccomandazionePrescrizioneRct = int.Parse(iDTipologiaRaccomandazionePrescrizioneRct);

        GetlbRaccomandazioni(null, idtipologiaRaccomandazionePrescrizioneRct);
        GetlbPrescrizioni(null, idtipologiaRaccomandazionePrescrizioneRct);
        SetHelperASPxListBox();
        //}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Visible)
        {
            if (!string.IsNullOrEmpty(iDRapportoControlloTecnico))
            {
                lbPrescrizioni.UnselectAll();
                lbRaccomandazioni.UnselectAll();
                //SaveRaccomandazioniPrescrizioni();
            }
        }

        //string controlName = Request.Params.Get("__EVENTTARGET");
        //if (string.IsNullOrEmpty(controlName))
        //{
        //    GetDatiAll();
        //}
        //else if ((!controlName.Contains("lbRaccomandazioni")) && (!controlName.Contains("lbPrescrizioni")))
        //{
        //    GetDatiAll();
        //}
    }

    protected void GetDatiAll()
    {
        if (!string.IsNullOrEmpty(iDRapportoControlloTecnico))
        {
            int idrapportoControlloTecnico = int.Parse(iDRapportoControlloTecnico);
            int idtipologiaRaccomandazionePrescrizioneRct = int.Parse(iDTipologiaRaccomandazionePrescrizioneRct);

            GetDatiRCTRaccomandazioni(idrapportoControlloTecnico, idtipologiaRaccomandazionePrescrizioneRct);
            GetDatiRCTPrescrizioni(idrapportoControlloTecnico, idtipologiaRaccomandazionePrescrizioneRct);

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
    public void GetlbRaccomandazioni(int? iDPresel, int? iDTipologiaRaccomandazionePrescrizioneRct)
    {
        lbRaccomandazioni.Items.Clear();

        lbRaccomandazioni.ValueField = "IDTipologiaRaccomandazione";
        lbRaccomandazioni.TextField = "Raccomandazione";
        lbRaccomandazioni.DataSource = LoadDropDownList.LoadDropDownList_SYS_RCTTipologiaRaccomandazione(iDPresel, iDTipologiaRaccomandazionePrescrizioneRct);
        lbRaccomandazioni.DataBind();
    }

    public void GetlbPrescrizioni(int? iDPresel, int? iDTipologiaRaccomandazionePrescrizioneRct)
    {
        lbPrescrizioni.Items.Clear();

        lbPrescrizioni.ValueField = "IDTipologiaPrescrizione";
        lbPrescrizioni.TextField = "Prescrizione";
        lbPrescrizioni.DataSource = LoadDropDownList.LoadDropDownList_SYS_RCTTipologiaPrescrizione(iDPresel, iDTipologiaRaccomandazionePrescrizioneRct);
        lbPrescrizioni.DataBind();
    }
    #endregion

    #region Get Dati Raccomandazioni/Prescrizioni
    public void GetDatiRCTRaccomandazioni(int iDRapportoControlloTecnico, int iDTipologiaRaccomandazionePrescrizioneRct)
    {
        var result = UtilityRapportiControllo.GetValoriRCTRaccomandazioniPrescrizioni(iDRapportoControlloTecnico, iDTipologiaRaccomandazionePrescrizioneRct, "Raccomandazioni");
        
        foreach (var row in result)
        {
            try
            {
                lbRaccomandazioni.Items.FindByValue(row.IDTipologiaRaccomandazione.ToString()).Selected = true;
            }
            catch (Exception)
            {

            }            
        }
    }

    public void GetDatiRCTPrescrizioni(int iDRapportoControlloTecnico, int iDTipologiaRaccomandazionePrescrizioneRct)
    {
        var result = UtilityRapportiControllo.GetValoriRCTRaccomandazioniPrescrizioni(iDRapportoControlloTecnico, iDTipologiaRaccomandazionePrescrizioneRct, "Prescrizioni");
       
        foreach (var row in result)
        {
            try
            {
                lbPrescrizioni.Items.FindByValue(row.IDTipologiaPrescrizione.ToString()).Selected = true;
            }
            catch (Exception)
            {
                
            }           
        }
    }
    #endregion

    #region Save Dati Raccomandazioni/Prescrizioni

    public void SaveRaccomandazioni(int iDRapportoControlloTecnico, int iDTipologiaRaccomandazionePrescrizioneRct)
    {
        List<object> valoriRaccomadanzioni = new List<object>();
        foreach (ListEditItem item in lbRaccomandazioni.Items)
        {
            if (item.Selected)
            {
                valoriRaccomadanzioni.Add(item.Value);
            }
        }

        
        //string keyRaccomandazionePrescrizione = "keyRaccomandazione_" + iDRapportoControlloTecnico + "_" + iDTipologiaRaccomandazionePrescrizioneRct;
        //var fkeysQuery = HttpContext.Current.Cache
        //    .Cast<DictionaryEntry>()
        //    .Select(entry => (string)entry.Key)
        //    .Where(key => key.Contains(keyRaccomandazionePrescrizione)).Any();

        //if (fkeysQuery)
        //{
        //    HttpContext.Current.Cache.Remove(keyRaccomandazionePrescrizione);
        //}




        //var itemsInCache = HttpContext.Current.Cache.GetEnumerator();
        //if(itemsInCache.c)

        //while (itemsInCache.MoveNext())
        //{
        //    if (itemsInCache.Key.ToString() != keyRaccomandazionePrescrizione)
        //    {
        //        HttpContext.Current.Cache.Remove(itemsInCache.Key.ToString());
        //    }
        //}

        //TODO: attenzione gestire la cache remove sulla base della lista valoriRaccomadanzioni 
        //HttpContext.Cache.Remove("ObjectList");

        UtilityRapportiControllo.SaveInsertDeleteDatiRaccomandazioniPrescrizioni(iDRapportoControlloTecnico, iDTipologiaRaccomandazionePrescrizioneRct, valoriRaccomadanzioni.ToArray<object>(), "Raccomandazioni");
    }

    public void SavePrescrizioni(int iDRapportoControlloTecnico, int iDTipologiaRaccomandazionePrescrizioneRct)
    {
        List<object> valoriPrescrizioni = new List<object>();
        foreach (ListEditItem item in lbPrescrizioni.Items)
        {
            if (item.Selected)
            {
                valoriPrescrizioni.Add(item.Value);
            }
        }
        
        UtilityRapportiControllo.SaveInsertDeleteDatiRaccomandazioniPrescrizioni(iDRapportoControlloTecnico, iDTipologiaRaccomandazionePrescrizioneRct, valoriPrescrizioni.ToArray<object>(), "Prescrizioni");
    }

    public void SaveRaccomandazioniPrescrizioni()
    {
        int idrapportoControlloTecnico = int.Parse(iDRapportoControlloTecnico);
        int idtipologiaRaccomandazionePrescrizioneRct = int.Parse(iDTipologiaRaccomandazionePrescrizioneRct);
        SaveRaccomandazioni(idrapportoControlloTecnico, idtipologiaRaccomandazionePrescrizioneRct);
        SavePrescrizioni(idrapportoControlloTecnico, idtipologiaRaccomandazionePrescrizioneRct);
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
    
    public void lbRaccomandazioni_SelectedIndexChanged(object sender, EventArgs e)
    {
        EnabledDisabledPrescrizioniRaccomandazioni();
        SaveRaccomandazioniPrescrizioni();
    }

    public void lbPrescrizioni_SelectedIndexChanged(object sender, EventArgs e)
    {
        EnabledDisabledPrescrizioniRaccomandazioni();
        SaveRaccomandazioniPrescrizioni();        
    }
    
}
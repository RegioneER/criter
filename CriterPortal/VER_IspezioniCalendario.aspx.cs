using DataLayer;
using DataUtilityCore;
using DevExpress.Web;
using EncryptionQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VER_IspezioniCalendario : System.Web.UI.Page
{
    
    [Serializable]
    public struct DatiIspezioniStruct
    {
        public long IDIspezione
        {
            get;
            set;
        }
        public long IDIspezioneVisita
        {
            get;
            set;
        }
        public int IDIspettore
        {
            get;
            set;
        }
        public DateTime DataIspezione
        {
            get;
            set;
        }
        public string CodiceIspezione
        {
            get;
            set;
        }

        public string OrarioDa
        {
            get;
            set;
        }

        public string OrarioA
        {
            get;
            set;
        }
    }

    private const string DatiIspezioniKey = "DatiIspezioni";

    public List<DatiIspezioniStruct> DatiIspezioni
    {
        get
        {
            if (ViewState["DatiIspezioni"] == null)
                ViewState["DatiIspezioni"] = new List<DatiIspezioniStruct>();
            return (List<DatiIspezioniStruct>)ViewState["DatiIspezioni"];
        }
        set
        {
            ViewState["DatiIspezioni"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ASPxCalendar1.SelectedDate = DateTime.Now;
            PagePermission();
            cmbIspettore.Focus();

            GetDataIspezioni();
        }
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
            case "14": //Coordinatore Ispezioni
            case "12": //Amministrazione
                cmbIspettore.Visible = true;
                lblSoggetto.Visible = false;
                break;
            case "8": //Ispettore
                cmbIspettore.Value = getVal[0];
                cmbIspettore.Visible = false;
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0]);
                lblSoggetto.Visible = true;
                break;
        }
    }

    #region ISPETTORE
    protected void cmbIspettore_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "7", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void cmbIspettore_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;
        int IdSoggetto = int.Parse(e.Value.ToString());
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "7", e.Value.ToString(), "");
        comboBox.DataBind();
    }

    protected void cmbIspettore_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        //RefreshAspxComboBox();
        Response.Redirect("VER_IspezioniCalendario.aspx");
    }

    //protected void RefreshAspxComboBox()
    //{
    //    cmbIspettore.SelectedIndex = -1;
    //    cmbIspettore.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "7", "", "", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
    //    cmbIspettore.DataBind();
    //}
    #endregion


    protected void GetDataIspezioni()
    {
        using (var ctx = new CriterDataModel())
        {
            var datiIspezioni = DatiIspezioni;

            var ispezioni = ctx.V_VER_Ispezioni.AsNoTracking().Where(c => c.IDStatoIspezione == 7).ToList();
            foreach (var isp in ispezioni)
            {
                var datiIspezioneItem = new DatiIspezioniStruct()
                {
                    IDIspezione = isp.IDIspezione,
                    IDIspezioneVisita = isp.IDIspezioneVisita,
                    IDIspettore = int.Parse(isp.IDIspettore.ToString()),
                    CodiceIspezione = isp.CodiceIspezione,
                    DataIspezione = DateTime.Parse(isp.DataIspezione.ToString()),
                    OrarioDa = isp.OrarioDa,
                    OrarioA = isp.OrarioA
                };
                DatiIspezioni.Add(datiIspezioneItem);                
            }
            //rimetto nel viewstate
            DatiIspezioni = datiIspezioni;
        }
    }
    
    protected void ASPxCalendar1_DayCellPrepared(object sender, CalendarDayCellPreparedEventArgs e)
    {
        //e.Cell.Font.Bold = true;
    }

    protected void ASPxCalendar1_DayCellCreated(object sender, CalendarDayCellCreatedEventArgs e)
    {
        Label lbl = new Label();
        lbl.Text = e.Date.Day.ToString();

        //if (cmbIspettore.Value != null)
        //{
        ASPxGridView grid = new ASPxGridView();
        grid.ID = "grid_" + Guid.NewGuid();
        grid.KeyFieldName = "IDIspezione";
        grid.AutoGenerateColumns = false;
        grid.ClientInstanceName = "gridIspezioni";
        grid.EnableCallBacks = false;
        grid.EnablePagingCallbackAnimation = true;
        grid.Visible = false;
        grid.Width = new Unit("100%");
        grid.SettingsPager.ShowDefaultImages = false;
        grid.SettingsPager.NextPageButton.Visible = false;
        grid.SettingsPager.Summary.Visible = false;
        grid.SettingsPager.PageSize = 20;
        grid.SettingsPager.ShowDisabledButtons = false;
        grid.SettingsPager.NumericButtonCount = 4;
        grid.StylesPager.Pager.Font.Italic = true;
        //grid.Styles.Row.CssClass = "GridItem";
        //grid.Styles.AlternatingRow.CssClass = "GridAlternativeItem";
        grid.Styles.Header.HorizontalAlign = HorizontalAlign.Center;
        grid.SettingsBehavior.AllowFocusedRow = true;
        grid.SettingsBehavior.AllowClientEventsOnLoad = false;
        grid.FocusedRowIndex = -1;
        grid.SettingsBehavior.ProcessFocusedRowChangedOnServer = true;
        grid.Settings.ShowColumnHeaders = false;
                

        GridViewDataTextColumn gridColumn = new GridViewDataTextColumn();
        gridColumn.DataItemTemplate = new MyHyperlinkTemplate(); // Create a template
        gridColumn.FieldName = "CodiceIspezione";
        gridColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        gridColumn.CellStyle.HorizontalAlign = HorizontalAlign.Center;
        gridColumn.CellStyle.Font.Bold = false;
        grid.Columns.Add(gridColumn);

        #region MyRegion

        var datiIspezioni = DatiIspezioni.AsQueryable();
        int iDSoggetto = 0;
        if (cmbIspettore.Value != null)
        {
            iDSoggetto = int.Parse(cmbIspettore.Value.ToString());
            datiIspezioni = datiIspezioni.Where(a => a.IDIspettore == iDSoggetto);
        }

        foreach (var c in datiIspezioni)
        {
            DateTime dataIspezione = c.DataIspezione;
            var SqlDateTime = dataIspezione.ToString("d");
            var CelllDateTime = e.Date.ToString("d");

            if (SqlDateTime == CelllDateTime)
            {
                grid.Visible = true;
                grid.DataSource = datiIspezioni.Where(a => a.DataIspezione == e.Date);
            }
        }
        #endregion

        e.Controls.Add(lbl);
        e.Controls.Add(grid);
        grid.DataBind();
    }
}


class MyHyperlinkTemplate : ITemplate
{
    public void InstantiateIn(Control container)
    {
        ASPxHyperLink link = new ASPxHyperLink();
        GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)container;
        var iDIspezione = gridContainer.KeyValue.ToString();
        var codiceIspezione = DataBinder.Eval(gridContainer.DataItem, "CodiceIspezione").ToString();
        var iDIspezioneVisita = DataBinder.Eval(gridContainer.DataItem, "IDIspezioneVisita").ToString();
                
        var OrarioDa = DataBinder.Eval(gridContainer.DataItem, "OrarioDa").ToString();
        var OrarioA = DataBinder.Eval(gridContainer.DataItem, "OrarioA").ToString();

        QueryString qs = new QueryString();
        qs.Add("IDIspezione", iDIspezione);
        qs.Add("IDIspezioneVisita", iDIspezioneVisita);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);
        string url = "VER_Ispezioni.aspx";
        url += qsEncrypted.ToString();

        link.NavigateUrl = url;
        link.Target = "_blank";
        link.Text = string.Format("Ispezione n. {0} dalle ore {1} alle ore {2}", codiceIspezione.ToString(), OrarioDa, OrarioA);
        link.ToolTip = "Visualizza dettaglio Ispezione";
        container.Controls.Add(link);
    }
}
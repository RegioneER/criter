using DataLayer;
using System;
using System.Web.UI.WebControls;
using System.Linq;
using DevExpress.Web;
using System.Web;
using DataUtilityCore;
using EncryptionQS;
using System.Web.UI;

public partial class VER_InterventiScadenziario : System.Web.UI.Page
{
    UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ASPxCalendar1.SelectedDate = DateTime.Now;
            PagePermission();
            cmbAziende.Focus();
        }
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
    }

    protected void RefreshAspxComboBox()
    {
        cmbAziende.SelectedIndex = -1;
        cmbAziende.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "2", "", "", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
        cmbAziende.DataBind();
    }
    #endregion

    protected void ASPxCalendar1_DayCellPrepared(object sender, CalendarDayCellPreparedEventArgs e)
    {
        e.Cell.Font.Bold = true;
    }

    protected void ASPxCalendar1_DayCellCreated(object sender, CalendarDayCellCreatedEventArgs e)
    {
        Label lbl = new Label();
        lbl.Text = e.Date.Day.ToString();

        if (cmbAziende.Value != null)
        {
            ASPxGridView grid = new ASPxGridView();
            grid.KeyFieldName = "IDAccertamento";
            grid.AutoGenerateColumns = false;
            grid.ClientInstanceName = "gridAccertamenti";
            grid.EnableCallBacks = false;
            grid.EnablePagingCallbackAnimation = true;
            grid.Visible = false;
            grid.Width = new Unit("100%");
            grid.SettingsPager.ShowDefaultImages = false;
            grid.SettingsPager.NextPageButton.Visible = false;
            grid.SettingsPager.Summary.Visible = false;
            grid.SettingsPager.PageSize = 5;
            grid.SettingsPager.ShowDisabledButtons = false;
            grid.SettingsPager.NumericButtonCount = 4;
            grid.StylesPager.Pager.Font.Italic = true;
            grid.Styles.Row.CssClass = "GridItem";
            grid.Styles.AlternatingRow.CssClass = "GridAlternativeItem";
            grid.Styles.Header.HorizontalAlign = HorizontalAlign.Center;
            grid.SettingsBehavior.AllowFocusedRow = true;
            grid.SettingsBehavior.AllowClientEventsOnLoad = false;
            grid.FocusedRowIndex = -1;
            grid.SettingsBehavior.ProcessFocusedRowChangedOnServer = true;
            grid.HtmlRowCreated += Grid_HtmlRowCreated;

            GridViewDataColumn gridColumn = new GridViewDataColumn();
            gridColumn.FieldName = "CodiceAccertamento";
            gridColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gridColumn.HeaderStyle.CssClass = "GridHeader";
            gridColumn.CellStyle.HorizontalAlign = HorizontalAlign.Center;
            gridColumn.CellStyle.Font.Bold = false;
            grid.Columns.Add(gridColumn);

            using (var ctx = new CriterDataModel())
            {
                int iDSoggetto = int.Parse(cmbAziende.Value.ToString());

                var accertamento = ctx.VER_Accertamento.Where(c => c.IDSoggetto == iDSoggetto && c.fAttivo == true && (c.IDStatoAccertamentoIntervento == 1 || c.IDStatoAccertamentoIntervento == 3 || c.IDStatoAccertamentoIntervento == 8)).ToList();

                foreach (var c in accertamento)
                {
                    var SqlDateTime = c.DataRilevazione.ToString("d");
                    var CelllDateTime = e.Date.ToString("d");

                    if (SqlDateTime == CelllDateTime)
                    {
                        grid.Visible = true;
                        grid.DataSource = accertamento.Where(x => x.DataRilevazione.ToString("d") == e.Date.ToString("d"));
                    }
                }
            }
            e.Controls.Add(lbl);
            e.Controls.Add(grid);
            grid.DataBind();
        }
    }

    private void Grid_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            e.Row.ToolTip = "Cliccare qui per visualizzare dettaglio dell'intervento";
            int iDAccertamento = int.Parse((e.GetValue("IDAccertamento").ToString()));

            QueryString qs = new QueryString();
            qs.Add("IDAccertamento", iDAccertamento.ToString());
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "VER_Interventi.aspx";
            url += qsEncrypted.ToString();
            e.Row.Attributes.Add("onclick", "javascript:location.href='" + url + "'");
            e.Row.Attributes.Add("style", "cursor: pointer;");
        }
    }

}
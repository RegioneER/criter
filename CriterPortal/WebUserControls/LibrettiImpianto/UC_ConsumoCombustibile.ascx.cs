using System;
using System.Web.UI.WebControls;
using DevExpress.XtraCharts.Native;
using DataLayer;
using DataUtilityCore;
using DevExpress.Web;
using DevExpress.Web.Data;


public partial class UC_ConsumoCombustibile: CriterUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override bool IsDraftElementoGridPrincipale(int itemId)
    {
        return CurrentDataContext.LIM_LibrettiImpiantiConsumoCombustibile.Find(itemId).IDLibrettoImpianto == IDLibrettoImpianto;
    }

    protected virtual void grdPrincipale_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        base.DetailGrid_RowInserting(sender, e);

        e.NewValues["IDLibrettoImpianto"] = IDLibrettoImpianto;
        
        var editorCombustibileAltro = GetTextboxCombustibileAltroControl(sender as ASPxGridView);

        if (editorCombustibileAltro != null)
            e.NewValues["CombustibileAltro"] = editorCombustibileAltro.Value;
        else
            e.NewValues["CombustibileAltro"] = null;
    }

    protected virtual void grdPrincipale_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        base.DetailGrid_RowUpdating(sender, e);

        var editorCombustibileAltro = GetTextboxCombustibileAltroControl(sender as ASPxGridView);

        if (editorCombustibileAltro != null)
            e.NewValues["CombustibileAltro"] = editorCombustibileAltro.Value;
        else
            e.NewValues["CombustibileAltro"] = null;
    }

    protected void grdPrincipale_BeforePerformDataSelect(object sender, EventArgs e)
    {
        dsGridPrincipale.WhereParameters["IDLibrettoImpianto"].DefaultValue = IDLibrettoImpianto.ToString();
    }

    protected virtual void grdPrincipale_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

    }

    protected void grdPrincipale_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView) sender;

        if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Edit || e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete)
        {
            e.Visible = IsDraftElementoGridPrincipale(GetGridKeyValue(gridView, e.VisibleIndex));
        }
    }

    protected int GetGridKeyValue(ASPxGridView gridView, int visibleIndex)
    {
        return Convert.ToInt32(gridView.GetRowValues(visibleIndex, gridView.KeyFieldName));
    }
    
    protected void grdPrincipale_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView) sender;

        if (!gridView.IsNewRowEditing)
        {

        }
    }

    protected void grdPrincipale_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView) sender;

        var dataEsercizioStart = int.Parse(e.OldValues["DataEsercizioStart"].ToString());
        var dataEsercizioEnd = int.Parse(e.NewValues["DataEsercizioEnd"].ToString());

        if (!dataEsercizioStart.Equals(dataEsercizioEnd))
        {
            var idRiga = Convert.ToInt32(e.Keys[gridView.KeyFieldName]);

            var gridObject = CurrentDataContext.LIM_LibrettiImpiantiConsumoCombustibile.Find(idRiga);

            
            CurrentDataContext.SaveChanges();
            gridView.DataBind();
            
        }
    }

    protected void grdPrincipale_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView) sender;

        var idRiga = Convert.ToInt32(e.EditingKeyValue);

        e.Cancel = !IsDraftElementoGridPrincipale(idRiga);
    }

    protected void grdSostituzioni_BeforePerformDataSelect(object sender, EventArgs e)
    {
        var grid = (sender as DevExpress.Web.ASPxGridView);

        var masterObject = CurrentDataContext.LIM_LibrettiImpiantiConsumoCombustibile.Find(Convert.ToInt32(grid.GetMasterRowKeyValue()));

        
    }

    protected void txtCombustibileAltro_Init(object sender, EventArgs e)
    {
        var txtCombustibileAltro = (ASPxTextBox) sender;

        var panel = txtCombustibileAltro.NamingContainer as DevExpress.Web.ASPxPanel;
        var gridContainer = panel.NamingContainer as GridViewEditItemTemplateContainer;

        txtCombustibileAltro.ValidationSettings.ValidationGroup = gridContainer.ValidationGroup;

        int keyValue = GetGridKeyValue(gridContainer.Grid, gridContainer.Grid.EditingRowVisibleIndex);

        panel.ClientVisible = keyValue != 0 && (CurrentDataContext.LIM_LibrettiImpiantiConsumoCombustibile.Find(keyValue).IDTipologiaCombustibile == 1);
    }

    private ASPxTextBox GetTextboxCombustibileAltroControl(ASPxGridView grid)
    {
        GridViewDataColumn col = grid.Columns["CombustibileAltro"] as GridViewDataColumn;

        var panel = grid.FindEditRowCellTemplateControl(col, "pnlCombustibileAltro") as DevExpress.Web.ASPxPanel;

        return panel.FindControl("txtCombustibileAltro") as ASPxTextBox;
    }

    protected void controllaCampiMinMax(object sender, ASPxDataValidationEventArgs e, string nomeCampo1, string nomeCaption1, string nomeCampo2, string nomeCaption2)
    {
        ASPxGridView grid = sender as ASPxGridView;

        if (grid != null)
        {
            int numberMin;
            int numberMax;

            if (int.TryParse(e.NewValues[nomeCampo1].ToString(), out numberMin) && int.TryParse(e.NewValues[nomeCampo2].ToString(), out numberMax))
            {
                if (numberMin > numberMax)
                {
                    //e.Errors[grid.Columns[nomeCampo1]] = "Valore non ammesso.";
                    //e.Errors[grid.Columns[nomeCampo2]] = "Valore non ammesso.";

                    e.RowError = "Errore: Il valore di " + nomeCaption1 + " risulta maggiore di " + nomeCaption2 + "!";
                }
            }
        }
    }

    protected void grdPrincipale_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        controllaCampiMinMax(sender, e, "DataEsercizioStart", "Data di esercizio iniziale", "DataEsercizioEnd", "Data di esercizio iniziale");
        
    }

}
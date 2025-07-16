using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using DataUtilityCore;
using DevExpress.Web;
using DevExpress.Web.Data;

public partial class WebUserControls_LibrettiImpianto_VasiEspansione : CriterUserControl
{
    public const string _Prefisso = "VX";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Grid principale

    protected override bool IsDraftElementoGridPrincipale(int itemId)
    {
        return CurrentDataContext.LIM_LibrettiImpiantiVasiEspansione.Find(itemId).IDLibrettoImpiantoInserimento == IDLibrettoImpianto;
    }

    protected virtual void grdPrincipale_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;

        base.DetailGrid_RowInserting(sender, e);

        e.NewValues["IDLibrettoImpiantoInserimento"] = IDLibrettoImpianto;
        e.NewValues["Prefisso"] = _Prefisso;
        e.NewValues["CodiceProgressivo"] = LibrettoImpianto.LIM_LibrettiImpiantiVasiEspansione.Count(l => l.fAttivo) + 1;
        e.NewValues["fAttivo"] = true;

        e.NewValues["fChiuso"] = GetRadioButtonChiusoControl(grid).Checked;
        SetValue(e.NewValues, "PressionePrecaricaBar", GetEditorControl(grid, "PressionePrecaricaBar", "pnlPressionePrecarica", "spePressionePrecaricaBar"));
    }

    protected virtual void grdPrincipale_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)sender;

        base.DetailGrid_RowUpdating(sender, e);

        bool chiuso = GetRadioButtonChiusoControl(grid).Checked;
        e.NewValues["fChiuso"] = chiuso;

        //fix per forzare l'update di questo campo, dato che oldvalue è sempre null
        e.OldValues["PressionePrecaricaBar"] = -1m;
        if (chiuso)
            SetValue(e.NewValues, "PressionePrecaricaBar", GetEditorControl(grid, "PressionePrecaricaBar", "pnlPressionePrecarica", "spePressionePrecaricaBar"));
        else
            e.NewValues["PressionePrecaricaBar"] = null;
    }

    private ASPxRadioButton GetRadioButtonChiusoControl(ASPxGridView grid)
    {
        GridViewDataColumn col = grid.Columns["fChiuso"] as GridViewDataColumn;

        return grid.FindEditRowCellTemplateControl(col, "btnChiuso") as ASPxRadioButton;
    }

    protected void grdPrincipale_BeforePerformDataSelect(object sender, EventArgs e)
    {
        dsGridPrincipale.WhereParameters["IDLibrettoImpianto"].DefaultValue = IDLibrettoImpianto.ToString();
    }

    protected virtual void grdPrincipale_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

    }

    protected void grdPrincipale_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (e.ButtonType == ColumnCommandButtonType.Edit)
        {
            //e.Visible = (IsBozza || (IsRevisione && IsDraftElementoGridPrincipale(GetGridKeyValue(gridView, e.VisibleIndex))));
            e.Visible = (IsBozza || IsRevisione);
        }
        if (e.ButtonType == ColumnCommandButtonType.Delete)
        {
            e.Visible = (IsBozza || (IsRevisione && IsDraftElementoGridPrincipale(GetGridKeyValue(gridView, e.VisibleIndex))));
            //e.Visible = (IsBozza || IsRevisione);
        }
    }

    protected void grdPrincipale_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (e.ButtonID == "cmdSostituisci")
        {
            //attivo la sostituzione solamente in caso di revisione
            if (IsRevisione && !IsDraftElementoGridPrincipale(GetGridKeyValue(gridView, e.VisibleIndex)))
            {
                e.Visible = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
            }
        }
    }

    protected int GetGridKeyValue(ASPxGridView gridView, int visibleIndex)
    {
        return Convert.ToInt32(gridView.GetRowValues(visibleIndex, gridView.KeyFieldName));
    }

    
    protected void grdPrincipale_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (e.ButtonID == "cmdSostituisci")
        {
            var idRiga = GetGridKeyValue(gridView, e.VisibleIndex);

            //creo una nuova riga con lo stesso codice e dismetto quella corrente
            var gridElement = CurrentDataContext.LIM_LibrettiImpiantiVasiEspansione.Find(idRiga);

            gridElement.IDUtenteUltimaModifica = int.Parse(SecurityManager.GetUserIDUtente(Page.User.Identity.Name));
            gridElement.DataUltimaModifica = DateTime.Now;
            gridElement.DataDismissione = DateTime.Today;
            gridElement.fAttivo = false;

            var replaceGridElement = new LIM_LibrettiImpiantiVasiEspansione();

            replaceGridElement.Prefisso = gridElement.Prefisso;
            replaceGridElement.CodiceProgressivo = gridElement.CodiceProgressivo;
            replaceGridElement.IDLibrettoImpiantoInserimento = gridElement.IDLibrettoImpianto;
            replaceGridElement.CapacitaLt = gridElement.CapacitaLt;
            replaceGridElement.fChiuso = gridElement.fChiuso;
            replaceGridElement.PressionePrecaricaBar = gridElement.PressionePrecaricaBar;
            replaceGridElement.IDUtenteInserimento = gridElement.IDUtenteUltimaModifica;
            replaceGridElement.DataInserimento = gridElement.DataUltimaModifica;
            replaceGridElement.DataInstallazione = gridElement.DataDismissione;
            replaceGridElement.fAttivo = true;

            LibrettoImpianto.LIM_LibrettiImpiantiVasiEspansione.Add(replaceGridElement);

            CurrentDataContext.SaveChanges();

            grdPrincipale.DataBind();
        }
    }

    protected void grdPrincipale_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (!gridView.IsNewRowEditing)
        {

        }
    }
    protected void spePressionePrecaricaBar_Init(object sender, EventArgs e)
    {
        var spePressionePrecaricaBar = (ASPxSpinEdit)sender;

        var panel = spePressionePrecaricaBar.NamingContainer as DevExpress.Web.ASPxPanel;
        var gridContainer = panel.NamingContainer as GridViewEditItemTemplateContainer;

        spePressionePrecaricaBar.ValidationSettings.ValidationGroup = gridContainer.ValidationGroup;

        int keyValue = GetGridKeyValue(gridContainer.Grid, gridContainer.Grid.EditingRowVisibleIndex);

        panel.ClientVisible = keyValue != 0 && (CurrentDataContext.LIM_LibrettiImpiantiVasiEspansione.Find(keyValue).fChiuso);
    }

    //private ASPxSpinEdit GetSpinEditPressionePrecaricaControl(ASPxGridView grid)
    //{
    //    GridViewDataColumn col = grid.Columns["PressionePrecaricaBar"] as GridViewDataColumn;

    //    var panel = grid.FindEditRowCellTemplateControl(col, "pnlPressionePrecarica") as DevExpress.Web.ASPxPanel;

    //    return panel.FindControl("spePressionePrecarica") as ASPxSpinEdit;
    //}

    protected void grdPrincipale_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        var dataInstallazionePrecedente = Convert.ToDateTime(e.OldValues["DataInstallazione"]);
        var dataInstallazioneCorrente = Convert.ToDateTime(e.NewValues["DataInstallazione"]);

        if (!dataInstallazioneCorrente.Equals(dataInstallazionePrecedente))
        {

            var idRiga = Convert.ToInt32(e.Keys[gridView.KeyFieldName]);

            var gridObject = CurrentDataContext.LIM_LibrettiImpiantiVasiEspansione.Find(idRiga);

            var componenteSostituito = LibrettoImpianto.LIM_LibrettiImpiantiVasiEspansione.Where(l => l.CodiceProgressivo == gridObject.CodiceProgressivo && l.fAttivo == false && l.DataDismissione.Equals(dataInstallazionePrecedente)).FirstOrDefault();

            if (componenteSostituito != null)
            {
                componenteSostituito.DataDismissione = dataInstallazioneCorrente;
                CurrentDataContext.SaveChanges();
                gridView.DataBind();
            }
        }
    }

    protected void grdPrincipale_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        var idRiga = Convert.ToInt32(e.EditingKeyValue);

        //e.Cancel = !IsDraftElementoGridPrincipale(idRiga);
    }

    protected void grdSostituzioni_BeforePerformDataSelect(object sender, EventArgs e)
    {
        var grid = (sender as DevExpress.Web.ASPxGridView);

        var masterObject = CurrentDataContext.LIM_LibrettiImpiantiVasiEspansione.Find(Convert.ToInt32(grid.GetMasterRowKeyValue()));

        dsGridPrincipaleSostituzioni.WhereParameters["IDLibrettoImpianto"].DefaultValue = IDLibrettoImpianto.ToString();
        dsGridPrincipaleSostituzioni.WhereParameters["CodiceProgressivo"].DefaultValue = masterObject.CodiceProgressivo.ToString();
    }

    #endregion

    protected void grdPrincipale_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
//        if (e.RowType == GridViewRowType.EditForm)
//        {
//            ASPxGridView grid = sender as ASPxGridView;
//            ASPxRadioButton aperto = grid.FindEditRowCellTemplateControl(grid.Columns["fChiuso"] as GridViewDataColumn, "btnAperto") as ASPxRadioButton;
//            ASPxRadioButton chiuso = grid.FindEditRowCellTemplateControl(grid.Columns["fChiuso"] as GridViewDataColumn, "btnChiuso") as ASPxRadioButton;

//            aperto.ClientInstanceName = "btnAperto" + e.VisibleIndex;
//            chiuso.ClientInstanceName = "btnChiuso" + e.VisibleIndex;

//            aperto.ClientSideEvents.CheckedChanged = String.Format(@"
//                        function (s,e) 
//                        {
//                            if(btnAperto{0}.GetChecked()) 
//                            {
//                                btnChiuso{0}.SetChecked(false);
//                                pnlPressionePrecarica.SetVisible(false);
//                            }
//                        }", e.VisibleIndex);

//            chiuso.ClientSideEvents.CheckedChanged = String.Format(@"
//                        function (s,e) 
//                        {
//                            if(btnChiuso{0}.GetChecked()) 
//                            {
//                                btnAperto{0}.SetChecked(false);
//                                pnlPressionePrecarica.SetVisible(true);
//                            }
//                        }", e.VisibleIndex);
//        }

    }

    protected void controllaCampoDataFutura(object sender, ASPxDataValidationEventArgs e, string nomeCampo, string nomeCaption)
    {
        ASPxGridView grid = sender as ASPxGridView;

        if (grid != null)
        {

            DateTime dt;

            if (DateTime.TryParse(e.NewValues[nomeCampo].ToString(), out dt))
            {
                DateTime _minDate = DateTime.Today.AddYears(-100);
                if ((dt.Date > DateTime.Today) || (dt.Date < _minDate))
                {
                    e.Errors[grid.Columns[nomeCampo]] = "Valore non ammesso.";

                    e.RowError = "Errore: La " + nomeCaption + " risulta non valida perchè maggiore di quella corrente, o data non valida!";
                }
            }
        }
    }
    
    protected void grdPrincipale_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        var grid = (sender as DevExpress.Web.ASPxGridView);

        controllaCampoDataFutura(sender, e, "DataInstallazione", "data installazione");
    }
}
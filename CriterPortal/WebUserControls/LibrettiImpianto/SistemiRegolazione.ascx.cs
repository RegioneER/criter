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

public partial class WebUserControls_LibrettiImpianto_SistemiRegolazione : CriterUserControl
{
    public const string _Prefisso = "SR";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Grid principale

    protected override bool IsDraftElementoGridPrincipale(int itemId)
    {
        return CurrentDataContext.LIM_LibrettiImpiantiSistemiRegolazione.Find(itemId).IDLibrettoImpiantoInserimento == IDLibrettoImpianto;
    }

    protected virtual void grdPrincipale_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        base.DetailGrid_RowInserting(sender, e);

        e.NewValues["IDLibrettoImpiantoInserimento"] = IDLibrettoImpianto;
        e.NewValues["Prefisso"] = _Prefisso;
        e.NewValues["CodiceProgressivo"] = LibrettoImpianto.LIM_LibrettiImpiantiSistemiRegolazione.Count(l => l.fAttivo) + 1;
        e.NewValues["fAttivo"] = true;
    }

    protected virtual void grdPrincipale_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        base.DetailGrid_RowUpdating(sender, e);
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

    protected int GetGridKeyValue(ASPxGridView gridView, int visibleIndex)
    {
        return Convert.ToInt32(gridView.GetRowValues(visibleIndex, gridView.KeyFieldName));
    }

    protected void grdPrincipale_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
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

    protected void grdPrincipale_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (e.ButtonID == "cmdSostituisci")
        {
            var idRiga = GetGridKeyValue(gridView, e.VisibleIndex);

            //creo una nuova riga con lo stesso codice e dismetto quella corrente
            var gridElement = CurrentDataContext.LIM_LibrettiImpiantiSistemiRegolazione.Find(idRiga);

            gridElement.IDUtenteUltimaModifica = int.Parse(SecurityManager.GetUserIDUtente(Page.User.Identity.Name));
            gridElement.DataUltimaModifica = DateTime.Now;
            gridElement.DataDismissione = DateTime.Today;
            gridElement.fAttivo = false;

            var replaceGridElement = new LIM_LibrettiImpiantiSistemiRegolazione();

            replaceGridElement.Prefisso = gridElement.Prefisso;
            replaceGridElement.CodiceProgressivo = gridElement.CodiceProgressivo;
            replaceGridElement.IDUtenteInserimento = gridElement.IDUtenteUltimaModifica;
            replaceGridElement.IDLibrettoImpiantoInserimento = gridElement.IDLibrettoImpianto;
            replaceGridElement.Fabbricante = gridElement.Fabbricante;
            replaceGridElement.LivelliTemperaturaNum = gridElement.LivelliTemperaturaNum;
            replaceGridElement.Modello = gridElement.Modello;
            replaceGridElement.PuntiRegolazioneNum = gridElement.PuntiRegolazioneNum;
            replaceGridElement.DataInserimento = gridElement.DataUltimaModifica;
            replaceGridElement.DataInstallazione = gridElement.DataDismissione;
            replaceGridElement.fAttivo = true;

            LibrettoImpianto.LIM_LibrettiImpiantiSistemiRegolazione.Add(replaceGridElement);

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

    protected void grdPrincipale_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        var dataInstallazionePrecedente = Convert.ToDateTime(e.OldValues["DataInstallazione"]);
        var dataInstallazioneCorrente = Convert.ToDateTime(e.NewValues["DataInstallazione"]);

        if (!dataInstallazioneCorrente.Equals(dataInstallazionePrecedente))
        {

            var idRiga = Convert.ToInt32(e.Keys[gridView.KeyFieldName]);

            var gridObject = CurrentDataContext.LIM_LibrettiImpiantiSistemiRegolazione.Find(idRiga);

            var componenteSostituito = LibrettoImpianto.LIM_LibrettiImpiantiSistemiRegolazione.Where(l => l.CodiceProgressivo == gridObject.CodiceProgressivo && l.fAttivo == false && l.DataDismissione.Equals(dataInstallazionePrecedente)).FirstOrDefault();

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

        var masterObject = CurrentDataContext.LIM_LibrettiImpiantiSistemiRegolazione.Find(Convert.ToInt32(grid.GetMasterRowKeyValue()));

        dsGridPrincipaleSostituzioni.WhereParameters["IDLibrettoImpianto"].DefaultValue = IDLibrettoImpianto.ToString();
        dsGridPrincipaleSostituzioni.WhereParameters["CodiceProgressivo"].DefaultValue = masterObject.CodiceProgressivo.ToString();
    }

    #endregion

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

    protected void grdPrincipale_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        var grid = (sender as DevExpress.Web.ASPxGridView);

        controllaCampoDataFutura(sender, e, "DataInstallazione", "data installazione");
    }
}
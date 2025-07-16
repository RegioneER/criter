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

public partial class WebUserControls_LibrettiImpianto_Cogeneratori : CriterUserControl
{
    public const string _Prefisso = "CG";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Grid principale

    protected override bool IsDraftElementoGridPrincipale(int itemId)
    {
        return CurrentDataContext.LIM_LibrettiImpiantiCogeneratori.Find(itemId).IDLibrettoImpiantoInserimento == IDLibrettoImpianto;
    }

    protected virtual void grdPrincipale_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        base.DetailGrid_RowInserting(sender, e);

        e.NewValues["IDLibrettoImpiantoInserimento"] = IDLibrettoImpianto;
        e.NewValues["Prefisso"] = _Prefisso;
        e.NewValues["CodiceProgressivo"] = LibrettoImpianto.LIM_LibrettiImpiantiCogeneratori.Count(l => l.fAttivo) + 1;
        e.NewValues["fAttivo"] = true;

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
            var gridElement = CurrentDataContext.LIM_LibrettiImpiantiCogeneratori.Find(idRiga);

            gridElement.IDUtenteUltimaModifica = int.Parse(SecurityManager.GetUserIDUtente(Page.User.Identity.Name));
            gridElement.DataUltimaModifica = DateTime.Now;
            gridElement.DataDismissione = DateTime.Today;
            gridElement.fAttivo = false;

            var replaceGridElement = new LIM_LibrettiImpiantiCogeneratori();

            replaceGridElement.Prefisso = gridElement.Prefisso;
            replaceGridElement.CodiceProgressivo = gridElement.CodiceProgressivo;
            replaceGridElement.IDUtenteInserimento = gridElement.IDUtenteUltimaModifica;
            replaceGridElement.IDLibrettoImpiantoInserimento = gridElement.IDLibrettoImpianto;
            replaceGridElement.CombustibileAltro = gridElement.CombustibileAltro;
            replaceGridElement.EmissioniCOMax = gridElement.EmissioniCOMax;
            replaceGridElement.EmissioniCOMin = gridElement.EmissioniCOMin;
            replaceGridElement.Fabbricante = gridElement.Fabbricante;
            replaceGridElement.IDTipologiaCogeneratore = gridElement.IDTipologiaCogeneratore;
            replaceGridElement.IDTipologiaCombustibile = gridElement.IDTipologiaCombustibile;
            replaceGridElement.Matricola = gridElement.Matricola;
            replaceGridElement.Modello = gridElement.Modello;
            replaceGridElement.PotenzaElettricaNominaleKw = gridElement.PotenzaElettricaNominaleKw;
            replaceGridElement.PotenzaTermicaNominaleKw = gridElement.PotenzaTermicaNominaleKw;
            replaceGridElement.TemperaturaAcquaIngressoGradiMax = gridElement.TemperaturaAcquaIngressoGradiMax;
            replaceGridElement.TemperaturaAcquaIngressoGradiMin = gridElement.TemperaturaAcquaIngressoGradiMin;
            replaceGridElement.TemperaturaAcquaMotoreMax = gridElement.TemperaturaAcquaMotoreMax;
            replaceGridElement.TemperaturaAcquaMotoreMin = gridElement.TemperaturaAcquaMotoreMin;
            replaceGridElement.TemperaturaAcquaUscitaGradiMax = gridElement.TemperaturaAcquaUscitaGradiMax;
            replaceGridElement.TemperaturaAcquaUscitaGradiMin = gridElement.TemperaturaAcquaUscitaGradiMin;
            replaceGridElement.TemperaturaFumiMonteMax = gridElement.TemperaturaFumiMonteMax;
            replaceGridElement.TemperaturaFumiMonteMin = gridElement.TemperaturaFumiMonteMin;
            replaceGridElement.TemperaturaFumiValleMax = gridElement.TemperaturaFumiValleMax;
            replaceGridElement.TemperaturaFumiValleMin = gridElement.TemperaturaFumiValleMin;
            replaceGridElement.DataInserimento = gridElement.DataUltimaModifica;
            replaceGridElement.DataInstallazione = gridElement.DataDismissione;
            replaceGridElement.fAttivo = true;

            LibrettoImpianto.LIM_LibrettiImpiantiCogeneratori.Add(replaceGridElement);

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

            var gridObject = CurrentDataContext.LIM_LibrettiImpiantiCogeneratori.Find(idRiga);

            var componenteSostituito = LibrettoImpianto.LIM_LibrettiImpiantiCogeneratori.Where(l => l.CodiceProgressivo == gridObject.CodiceProgressivo && l.fAttivo == false && l.DataDismissione.Equals(dataInstallazionePrecedente)).FirstOrDefault();

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

        var masterObject = CurrentDataContext.LIM_LibrettiImpiantiCogeneratori.Find(Convert.ToInt32(grid.GetMasterRowKeyValue()));

        dsGridPrincipaleSostituzioni.WhereParameters["IDLibrettoImpianto"].DefaultValue = IDLibrettoImpianto.ToString();
        dsGridPrincipaleSostituzioni.WhereParameters["CodiceProgressivo"].DefaultValue = masterObject.CodiceProgressivo.ToString();
    }

    protected void txtCombustibileAltro_Init(object sender, EventArgs e)
    {
        var txtCombustibileAltro = (ASPxTextBox) sender;

        var panel = txtCombustibileAltro.NamingContainer as DevExpress.Web.ASPxPanel;
        var gridContainer = panel.NamingContainer as GridViewEditItemTemplateContainer;

        txtCombustibileAltro.ValidationSettings.ValidationGroup = gridContainer.ValidationGroup;

        int keyValue = GetGridKeyValue(gridContainer.Grid, gridContainer.Grid.EditingRowVisibleIndex);

        panel.ClientVisible = keyValue != 0 && (CurrentDataContext.LIM_LibrettiImpiantiCogeneratori.Find(keyValue).IDTipologiaCombustibile == 1);
    }

    private ASPxTextBox GetTextboxCombustibileAltroControl(ASPxGridView grid)
    {
        GridViewDataColumn col = grid.Columns["CombustibileAltro"] as GridViewDataColumn;

        var panel = grid.FindEditRowCellTemplateControl(col, "pnlCombustibileAltro") as DevExpress.Web.ASPxPanel;

        return panel.FindControl("txtCombustibileAltro") as ASPxTextBox;
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

    protected void controllaCampiMinMax(object sender, ASPxDataValidationEventArgs e, string nomeCampo1, string nomeCaption1, string nomeCampo2, string nomeCaption2)
    {
        ASPxGridView grid = sender as ASPxGridView;

        if (grid != null)
        {
            Decimal numberMin;
            Decimal numberMax;

            if (e.NewValues[nomeCampo1] !=null && e.NewValues[nomeCampo2]!=null)
            {
                if (Decimal.TryParse(e.NewValues[nomeCampo1].ToString(), out numberMin) && Decimal.TryParse(e.NewValues[nomeCampo2].ToString(), out numberMax))
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
    }

    protected void grdPrincipale_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        controllaCampoDataFutura(sender, e, "DataInstallazione", "data installazione");
        controllaCampiMinMax(sender, e, "TemperaturaAcquaUscitaGradiMin", "temperatura acqua in uscita min", "TemperaturaAcquaUscitaGradiMax", "temperatura acqua in uscita max");
        controllaCampiMinMax(sender, e, "TemperaturaFumiValleMin", "temperatura fumi a valle dello scambiatore min", "TemperaturaFumiValleMax", "temperatura fumi a valle dello scambiatore max");
        controllaCampiMinMax(sender, e, "TemperaturaAcquaIngressoGradiMin", "temperatura acqua in ingresso min", "TemperaturaAcquaIngressoGradiMax", "temperatura acqua in ingresso max");
        controllaCampiMinMax(sender, e, "TemperaturaFumiMonteMin", "temperatura fumi a monte dello scambiatore min", "TemperaturaFumiMonteMax", "temperatura fumi a monte dello scambiatore max");
        controllaCampiMinMax(sender, e, "TemperaturaAcquaMotoreMin", "temperatura acqua motore min", "TemperaturaAcquaMotoreMax", "temperatura acqua motore max");
        controllaCampiMinMax(sender, e, "EmissioniCOMin", "emissioni di monossido di carbonio min", "EmissioniCOMax", "emissioni di monossido di carbonio max");
    }


    protected void grdPrincipale_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            bool fDismesso = (bool)e.GetValue("fDismesso");

            if (fDismesso)
            {
                grdPrincipale.Styles.Table.BackColor = System.Drawing.Color.FromArgb(255, 237, 173);

                DateTime? DataDismesso = (DateTime?)e.GetValue("DataDismesso");
                int? IDUtenteDismesso = (int?)e.GetValue("IDUtenteDismesso");
                Label lblGeneratoreDismesso = grdPrincipale.FindRowCellTemplateControl(e.VisibleIndex, null, "lblGeneratoreDismesso") as Label;
                lblGeneratoreDismesso.Visible = true;
                lblGeneratoreDismesso.Text = "</br>" + "Generatore dismesso&nbsp;in data &nbsp;" + String.Format("{0:dd/MM/yyyy}", DataDismesso) + "&nbsp;da&nbsp;" + SecurityManager.GetNomeCognomeFromIDUtente(IDUtenteDismesso);
            }
        }
    }
}
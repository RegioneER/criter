using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using DataUtilityCore;
using DevExpress.Web;
using DevExpress.Web.Data;

public partial class WebUserControls_LibrettiImpianto_GruppiTermici : CriterUserControl
{
    public const string _Prefisso = "GT";
    public const string _PrefissoBruciatori = "BR";
    public const string _PrefissoRecuperatori = "RC";

    protected void Page_Load(object sender, EventArgs e)
    {
 
    }

    #region Gruppi termici
    
    protected override bool IsDraftElementoGridPrincipale(int itemId)
    {
        return CurrentDataContext.LIM_LibrettiImpiantiGruppiTermici.Find(itemId).IDLibrettoImpiantoInserimento == IDLibrettoImpianto;
    }

    protected virtual void grdGruppiTermici_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        base.DetailGrid_RowInserting(sender, e);

        e.NewValues["IDLibrettoImpiantoInserimento"] = IDLibrettoImpianto;
        e.NewValues["Prefisso"] = _Prefisso;
        e.NewValues["CodiceProgressivo"] = LibrettoImpianto.LIM_LibrettiImpiantiGruppiTermici.Count(l => l.fAttivo) + 1;
        e.NewValues["fAttivo"] = true;

        var editorCombustibileAltro = GetTextboxCombustibileAltroControl(sender as ASPxGridView);
        var editorComboCombustibile = GetComboCombustibileControl(sender as ASPxGridView);
        if (editorComboCombustibile != null)
        {
            e.NewValues["IDTipologiaCombustibile"] = editorComboCombustibile.Value;

            if (editorComboCombustibile.Items[0].Selected)
            {
                if (editorCombustibileAltro != null)
                {
                    e.NewValues["CombustibileAltro"] = editorCombustibileAltro.Value;
                }
                else
                {
                    e.NewValues["CombustibileAltro"] = string.Empty;
                }
            }
            else
            {
                e.NewValues["CombustibileAltro"] = string.Empty;
            }
        }

        //if (editorCombustibileAltro != null)
        //    e.NewValues["CombustibileAltro"] = editorCombustibileAltro.Value;
        //else
        //    e.NewValues["CombustibileAltro"] = null;

        var editorFluidoTermovettoreAltro = GetTextboxFluidoTermovettoreAltroControl(sender as ASPxGridView);
        var editorComboFluidoTermovettore = GetComboFluidoTermovettoreControl(sender as ASPxGridView);
        if (editorComboFluidoTermovettore != null)
        {
            e.NewValues["IDTipologiaFluidoTermoVettore"] = editorComboFluidoTermovettore.Value;

            if (editorComboFluidoTermovettore.Items[0].Selected)
            {
                if (editorFluidoTermovettoreAltro != null)
                {
                    e.NewValues["FluidoTermovettoreAltro"] = editorFluidoTermovettoreAltro.Value;
                }
                else
                {
                    e.NewValues["FluidoTermovettoreAltro"] = string.Empty;
                }
            }
            else
            {
                e.NewValues["FluidoTermovettoreAltro"] = string.Empty;
            }
        }


        //if (editorFluidoTermovettoreAltro != null)
        //    e.NewValues["FluidoTermovettoreAltro"] = editorFluidoTermovettoreAltro.Value;
        //else
        //    e.NewValues["FluidoTermovettoreAltro"] = null;

        var editorNrAnalisiFumoPreviste = GetSpinNrAnalisiFumoPrevisteControl(sender as ASPxGridView);

        if(editorNrAnalisiFumoPreviste!=null)
            e.NewValues["AnalisiFumoPrevisteNr"] = editorNrAnalisiFumoPreviste.Value;
        else
            e.NewValues["AnalisiFumoPrevisteNr"] = null;


        //grdGruppiTermici.DetailRows.ExpandRowByKey(null);
    }

    protected virtual void grdGruppiTermici_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        base.DetailGrid_RowUpdating(sender, e);

        var editorCombustibileAltro = GetTextboxCombustibileAltroControl(sender as ASPxGridView);
        var editorComboCombustibile = GetComboCombustibileControl(sender as ASPxGridView);
        if (editorComboCombustibile != null)
        {
            e.NewValues["IDTipologiaCombustibile"] = editorComboCombustibile.Value;

            if (editorComboCombustibile.Items[0].Selected)
            {
                if (editorCombustibileAltro != null)
                {
                    e.NewValues["CombustibileAltro"] = editorCombustibileAltro.Value;
                }
                else
                {
                    e.NewValues["CombustibileAltro"] = string.Empty;
                }
            }
            else
            {
                e.NewValues["CombustibileAltro"] = string.Empty;
            }
        }

        var editorFluidoTermovettoreAltro = GetTextboxFluidoTermovettoreAltroControl(sender as ASPxGridView);
        var editorComboFluidoTermovettore = GetComboFluidoTermovettoreControl(sender as ASPxGridView);
        if (editorComboFluidoTermovettore != null)
        {
            if (editorComboFluidoTermovettore.Items[0].Selected)
            {
                e.NewValues["IDTipologiaFluidoTermoVettore"] = editorComboFluidoTermovettore.Value;

                if (editorFluidoTermovettoreAltro != null)
                {
                    e.NewValues["FluidoTermovettoreAltro"] = editorFluidoTermovettoreAltro.Value;
                }
                else
                {
                    e.NewValues["FluidoTermovettoreAltro"] = string.Empty;
                }
            }
            else
            {
                e.NewValues["FluidoTermovettoreAltro"] = string.Empty;
            }
        }

        //if (editorFluidoTermovettoreAltro != null)
        //    e.NewValues["FluidoTermovettoreAltro"] = editorFluidoTermovettoreAltro.Value;
        //else
        //    e.NewValues["FluidoTermovettoreAltro"] = null;

        var editorNrAnalisiFumoPreviste = GetSpinNrAnalisiFumoPrevisteControl(sender as ASPxGridView);

        if (editorNrAnalisiFumoPreviste != null)
            e.NewValues["AnalisiFumoPrevisteNr"] = editorNrAnalisiFumoPreviste.Value;
        else
            e.NewValues["AnalisiFumoPrevisteNr"] = null;
    }

    protected void grdGruppiTermici_BeforePerformDataSelect(object sender, EventArgs e)
    {
        dsGruppiTermici.WhereParameters["IDLibrettoImpianto"].DefaultValue = IDLibrettoImpianto.ToString();
    }

    protected virtual void grdGruppiTermici_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //uso cancellazione fisica, attivata solo su prima versione del libretto

        //var idRiga = Convert.ToInt32(e.Keys["IDLibrettoImpiantoGruppoTermico"]);

        //var gruppoTermico = CurrentDataContext.LIM_LibrettiImpiantiGruppiTermici.Find(idRiga);

        //gruppoTermico.fAttivo = false;
        //gruppoTermico.IDUtenteUltimaModifica = int.Parse(SecurityManager.GetUserIDUtente(Page.User.Identity.Name));
        //gruppoTermico.DataUltimaModifica = DateTime.Now;

        //CurrentDataContext.SaveChanges();

        //e.Cancel = true;
        //grdGruppiTermici.DataBind();

    }

    protected bool IsGeneratoreDismesso(int itemId)
    {
        return CurrentDataContext.LIM_LibrettiImpiantiGruppiTermici.Find(itemId).fDismesso;
    }

    protected void grdGruppiTermici_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;
                
        if (e.ButtonType == ColumnCommandButtonType.Edit)
        {
            //e.Visible = (IsBozza || IsRevisione && IsDraftElementoGridPrincipale(GetGridKeyValue(gridView, e.VisibleIndex))));
            e.Visible = ((IsBozza || IsRevisione) && !IsGeneratoreDismesso(GetGridKeyValue(gridView, e.VisibleIndex))); ;
        }
        if (e.ButtonType == ColumnCommandButtonType.Delete)
        {
            e.Visible = (IsBozza || IsRevisione && IsDraftElementoGridPrincipale(GetGridKeyValue(gridView, e.VisibleIndex)));
        }
    }

    protected void grdGruppiTermici_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (e.ButtonID == "cmdSostituisci")
        {
            //attivo la sostituzione solamente in caso di revisione
            if (IsRevisione && !IsDraftElementoGridPrincipale(GetGridKeyValue(gridView, e.VisibleIndex)) && !IsGeneratoreDismesso(GetGridKeyValue(gridView, e.VisibleIndex)))
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
        
    protected void grdGruppiTermici_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (e.ButtonID == "cmdSostituisci")
        {
            var idRiga = GetGridKeyValue(gridView, e.VisibleIndex);

            //creo una nuova riga con lo stesso codice e dismetto quella corrente
            var gruppoTermico = CurrentDataContext.LIM_LibrettiImpiantiGruppiTermici.Find(idRiga);

            gruppoTermico.IDUtenteUltimaModifica = int.Parse(SecurityManager.GetUserIDUtente(Page.User.Identity.Name));
            gruppoTermico.DataUltimaModifica = DateTime.Now;
            gruppoTermico.DataDismissione = DateTime.Today;
            gruppoTermico.fAttivo = false;

            var nuovoGruppo = new LIM_LibrettiImpiantiGruppiTermici();

            nuovoGruppo.Prefisso = gruppoTermico.Prefisso;
            nuovoGruppo.CodiceProgressivo = gruppoTermico.CodiceProgressivo;
            nuovoGruppo.IDUtenteInserimento = gruppoTermico.IDUtenteUltimaModifica;
            nuovoGruppo.IDLibrettoImpiantoInserimento = gruppoTermico.IDLibrettoImpianto;
            nuovoGruppo.IDTipologiaGruppiTermici = gruppoTermico.IDTipologiaGruppiTermici;
            nuovoGruppo.AnalisiFumoPrevisteNr = gruppoTermico.AnalisiFumoPrevisteNr;
            nuovoGruppo.CombustibileAltro = gruppoTermico.CombustibileAltro;
            nuovoGruppo.Fabbricante = gruppoTermico.Fabbricante;
            nuovoGruppo.FluidoTermovettoreAltro = gruppoTermico.FluidoTermovettoreAltro;
            nuovoGruppo.IDTipologiaCombustibile = gruppoTermico.IDTipologiaCombustibile;
            nuovoGruppo.IDTipologiaFluidoTermoVettore = gruppoTermico.IDTipologiaFluidoTermoVettore;
            nuovoGruppo.Matricola = gruppoTermico.Matricola;
            nuovoGruppo.Modello = gruppoTermico.Modello;
            nuovoGruppo.PotenzaTermicaUtileNominaleKw = gruppoTermico.PotenzaTermicaUtileNominaleKw;
            nuovoGruppo.RendimentoTermicoUtilePc = gruppoTermico.RendimentoTermicoUtilePc;
            nuovoGruppo.DataInserimento = gruppoTermico.DataUltimaModifica;
            nuovoGruppo.DataInstallazione = gruppoTermico.DataDismissione;
            nuovoGruppo.fAttivo = true;

            var recuperatori = CurrentDataContext.LIM_LibrettiImpiantiRecuperatori.Where(a => a.IDLibrettoImpiantoGruppoTermico == idRiga).ToList();
            foreach (var r in recuperatori)
            {
                r.IDUtenteUltimaModifica = int.Parse(SecurityManager.GetUserIDUtente(Page.User.Identity.Name));
                r.DataUltimaModifica = DateTime.Now;
                r.DataDismissione = DateTime.Today;
                r.fAttivo = false;

                var nuovoRecuperatore = new LIM_LibrettiImpiantiRecuperatori();

                nuovoRecuperatore.Prefisso = r.Prefisso;
                nuovoRecuperatore.LIM_LibrettiImpiantiGruppiTermici = nuovoGruppo;
                nuovoRecuperatore.CodiceProgressivo = r.CodiceProgressivo;
                nuovoRecuperatore.IDUtenteInserimento = r.IDUtenteUltimaModifica;
                nuovoRecuperatore.IDLibrettoImpiantoInserimento = IDLibrettoImpianto;
                nuovoRecuperatore.Fabbricante = r.Fabbricante;
                nuovoRecuperatore.Matricola = r.Matricola;
                nuovoRecuperatore.Modello = r.Modello;
                nuovoRecuperatore.PortataTermicaNominaleTotaleKw = r.PortataTermicaNominaleTotaleKw;
                nuovoRecuperatore.DataInserimento = r.DataUltimaModifica;
                nuovoRecuperatore.DataInstallazione = r.DataDismissione;
                nuovoRecuperatore.fAttivo = true;

                CurrentDataContext.LIM_LibrettiImpiantiRecuperatori.Add(nuovoRecuperatore);
            }

            var bruciatori = CurrentDataContext.LIM_LibrettiImpiantiBruciatori.Where(a => a.IDLibrettoImpiantoGruppoTermico == idRiga).ToList();
            foreach (var b in bruciatori)
            {
                b.IDUtenteUltimaModifica = int.Parse(SecurityManager.GetUserIDUtente(Page.User.Identity.Name));
                b.DataUltimaModifica = DateTime.Now;
                b.DataDismissione = DateTime.Today;
                b.fAttivo = false;

                var nuovoBruciatore = new LIM_LibrettiImpiantiBruciatori();
                nuovoBruciatore.Prefisso = b.Prefisso;
                nuovoBruciatore.LIM_LibrettiImpiantiGruppiTermici = nuovoGruppo;
                nuovoBruciatore.CodiceProgressivo = b.CodiceProgressivo;
                nuovoBruciatore.IDUtenteInserimento = b.IDUtenteUltimaModifica;
                nuovoBruciatore.IDLibrettoImpiantoInserimento = IDLibrettoImpianto;
                nuovoBruciatore.Combustibile = b.Combustibile;
                nuovoBruciatore.Fabbricante = b.Fabbricante;
                nuovoBruciatore.Matricola = b.Matricola;
                nuovoBruciatore.Modello = b.Modello;
                nuovoBruciatore.PortataTermicaMaxNominaleKw = b.PortataTermicaMaxNominaleKw;
                nuovoBruciatore.PortataTermicaMinNominaleKw = b.PortataTermicaMinNominaleKw;
                nuovoBruciatore.Tipologia = b.Tipologia;
                nuovoBruciatore.DataInserimento = b.DataUltimaModifica;
                nuovoBruciatore.DataInstallazione = b.DataDismissione;
                nuovoBruciatore.fAttivo = true;

                CurrentDataContext.LIM_LibrettiImpiantiBruciatori.Add(nuovoBruciatore);
            }

            LibrettoImpianto.LIM_LibrettiImpiantiGruppiTermici.Add(nuovoGruppo);

            CurrentDataContext.SaveChanges();

            grdGruppiTermici.DataBind();
        }
    }

    protected void grdGruppiTermici_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (!gridView.IsNewRowEditing)
        {
            //ASPxPanel panel = grdGruppiTermici.FindEditRowCellTemplateControl(grdGruppiTermici.Columns["CombustibileAltro"] as GridViewDataColumn, "pnlCombustibileAltro") as ASPxPanel;
            //bool fVisible = false;
            //Int32 keyValue = (Int32)gridView.GetRowValues(gridView.EditingRowVisibleIndex, new String[] { gridView.KeyFieldName });
            //ASPxComboBox combo = (ASPxComboBox)e.Editor;

            //if (combo != null && panel != null)
            //{
            //    if (combo.SelectedItem.Selected && combo.SelectedItem.Value == "1")
            //    {
            //        fVisible = true;
            //    }
            //    panel.ClientVisible = keyValue != 0 && (fVisible);
            //}
            
            //Int32 keyValue = (Int32)gridView.GetRowValues(gridView.EditingRowVisibleIndex, new String[] { gridView.KeyFieldName });

            //if (e.Column.FieldName == "AnalisiFumoPrevisteNr")
            //{
            //    ASPxSpinEdit cmbAccreditamento = gridView.FindEditRowCellTemplateControl(e.Column, "spinNrAnalisiFumoPreviste") as ASPxSpinEdit;

            //    cmbAccreditamento.ClientVisible = (CurrentDataContext.LIM_LibrettiImpiantiGruppiTermici.Find(keyValue).IDTipologiaGruppiTermici == 2);
            //}
        }
    }

    protected void spinNrAnalisiFumoPreviste_Init(object sender, EventArgs e)
    {
        var spinNrAnalisiFumoPreviste = (ASPxSpinEdit)sender;

        var panel = spinNrAnalisiFumoPreviste.NamingContainer as DevExpress.Web.ASPxPanel;
        var gridContainer = panel.NamingContainer as GridViewEditItemTemplateContainer;

        spinNrAnalisiFumoPreviste.ValidationSettings.ValidationGroup = gridContainer.ValidationGroup;

        int keyValue = GetGridKeyValue(gridContainer.Grid, gridContainer.Grid.EditingRowVisibleIndex);

        panel.ClientVisible = keyValue!=0 && (CurrentDataContext.LIM_LibrettiImpiantiGruppiTermici.Find(keyValue).IDTipologiaGruppiTermici == 2);
    }

    protected void txtCombustibileAltro_Init(object sender, EventArgs e)
    {
        var txtCombustibileAltro = (ASPxTextBox)sender;

        var panel = txtCombustibileAltro.NamingContainer as DevExpress.Web.ASPxPanel;
        var gridContainer = panel.NamingContainer as GridViewEditItemTemplateContainer;

        txtCombustibileAltro.ValidationSettings.ValidationGroup = gridContainer.ValidationGroup;

        int keyValue = GetGridKeyValue(gridContainer.Grid, gridContainer.Grid.EditingRowVisibleIndex);

        //panel.ClientVisible = keyValue != 0 && (CurrentDataContext.LIM_LibrettiImpiantiGruppiTermici.Find(keyValue).IDTipologiaCombustibile == 1);
    }

    protected void txtFluidoTermovettoreAltro_Init(object sender, EventArgs e)
    {
        var txtFluidoTermovettoreAltro = (ASPxTextBox)sender;

        var panel = txtFluidoTermovettoreAltro.NamingContainer as DevExpress.Web.ASPxPanel;
        var gridContainer = panel.NamingContainer as GridViewEditItemTemplateContainer;

        txtFluidoTermovettoreAltro.ValidationSettings.ValidationGroup = gridContainer.ValidationGroup;

        int keyValue = GetGridKeyValue(gridContainer.Grid, gridContainer.Grid.EditingRowVisibleIndex);

        //panel.ClientVisible = keyValue != 0 && (CurrentDataContext.LIM_LibrettiImpiantiGruppiTermici.Find(keyValue).IDTipologiaFluidoTermoVettore == 1);
    }

    private ASPxSpinEdit GetSpinNrAnalisiFumoPrevisteControl(ASPxGridView grid)
    {
        GridViewDataColumn col = grid.Columns["AnalisiFumoPrevisteNr"] as GridViewDataColumn;

        var panel = grid.FindEditRowCellTemplateControl(col, "pnlAnalisiFumoPreviste") as DevExpress.Web.ASPxPanel;
        
        return panel.FindControl("spinNrAnalisiFumoPreviste") as ASPxSpinEdit;
    }

    private ASPxTextBox GetTextboxCombustibileAltroControl(ASPxGridView grid)
    {
        GridViewDataColumn col = grid.Columns["CombustibileAltro"] as GridViewDataColumn;

        var panel = grid.FindEditRowCellTemplateControl(col, "pnlCombustibileAltro") as DevExpress.Web.ASPxPanel;

        return panel.FindControl("txtCombustibileAltro") as ASPxTextBox;
    }

    private ASPxTextBox GetTextboxFluidoTermovettoreAltroControl(ASPxGridView grid)
    {
        GridViewDataColumn col = grid.Columns["FluidoTermovettoreAltro"] as GridViewDataColumn;

        var panel = grid.FindEditRowCellTemplateControl(col, "pnlFluidoTermovettoreAltro") as DevExpress.Web.ASPxPanel;

        return panel.FindControl("txtFluidoTermovettoreAltro") as ASPxTextBox;
    }

    private ASPxComboBox GetComboCombustibileControl(ASPxGridView grid)
    {
        GridViewDataComboBoxColumn col = grid.Columns["IDTipologiaCombustibile"] as GridViewDataComboBoxColumn;
        ASPxComboBox combo = grid.FindEditRowCellTemplateControl(col, "ddlTipologiaCombustibile") as DevExpress.Web.ASPxComboBox;

        return combo;
    }

    private ASPxComboBox GetComboFluidoTermovettoreControl(ASPxGridView grid)
    {
        GridViewDataComboBoxColumn col = grid.Columns["IDTipologiaFluidoTermoVettore"] as GridViewDataComboBoxColumn;
        ASPxComboBox combo = grid.FindEditRowCellTemplateControl(col, "ddlTipologiaFluidoTermovettore") as DevExpress.Web.ASPxComboBox;

        return combo;
    }

    protected void grdGruppiTermici_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        var dataInstallazionePrecedente = Convert.ToDateTime(e.OldValues["DataInstallazione"]);
        var dataInstallazioneCorrente = Convert.ToDateTime(e.NewValues["DataInstallazione"]);

        if (!dataInstallazioneCorrente.Equals(dataInstallazionePrecedente))
        {

            var idRiga = Convert.ToInt32(e.Keys[gridView.KeyFieldName]);

            var gruppoTermico = CurrentDataContext.LIM_LibrettiImpiantiGruppiTermici.Find(idRiga);

            var componenteSostituito = LibrettoImpianto.LIM_LibrettiImpiantiGruppiTermici.Where(l => l.CodiceProgressivo == gruppoTermico.CodiceProgressivo && l.fAttivo == false && l.DataDismissione.Equals(dataInstallazionePrecedente)).FirstOrDefault();

            if (componenteSostituito != null)
            {
                componenteSostituito.DataDismissione = dataInstallazioneCorrente;
                CurrentDataContext.SaveChanges();
                gridView.DataBind();
            }
        }
    }

    protected void grdGruppiTermici_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        var idRiga = Convert.ToInt32(e.EditingKeyValue);

        //var gruppoTermico = CurrentDataContext.LIM_LibrettiImpiantiGruppiTermici.Find(idRiga);

        //e.Cancel = !IsDraftElementoGridPrincipale(idRiga);
    }

    protected void grdSostituzioni_BeforePerformDataSelect(object sender, EventArgs e)
    {
        var grid = (sender as DevExpress.Web.ASPxGridView);

        var gruppoTermico = CurrentDataContext.LIM_LibrettiImpiantiGruppiTermici.Find(Convert.ToInt32(grid.GetMasterRowKeyValue()));

        dsGruppiTermiciSostituzioni.WhereParameters["IDLibrettoImpianto"].DefaultValue = IDLibrettoImpianto.ToString();
        dsGruppiTermiciSostituzioni.WhereParameters["CodiceProgressivo"].DefaultValue = gruppoTermico.CodiceProgressivo.ToString();
    }

    protected void grdSostituzioni_DataBound(object sender, EventArgs e)
    {
        //var grid = sender as ASPxGridView;

        //GridViewDetailRowTemplateContainer container = grid.NamingContainer as GridViewDetailRowTemplateContainer;

        //var gridLabel = container.Grid.FindDetailRowTemplateControl(container.VisibleIndex, "lblGridSostituzioni") as ASPxLabel;

        //if (IsDraftElementoGridPrincipale(GetGridKeyValue(container.Grid, container.VisibleIndex)))
        //{
        //    grid.Visible = gridLabel.Visible = false;
        //}
    }

    protected void grdGruppiTermici_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            bool fDismesso = (bool)e.GetValue("fDismesso");

            if (fDismesso)
            {
                grdGruppiTermici.Styles.Table.BackColor = System.Drawing.Color.FromArgb(255, 237, 173);

                DateTime? DataDismesso = (DateTime?)e.GetValue("DataDismesso");
                int? IDUtenteDismesso = (int?)e.GetValue("IDUtenteDismesso");
                Label lblGeneratoreDismesso = grdGruppiTermici.FindRowCellTemplateControl(e.VisibleIndex, null, "lblGeneratoreDismesso") as Label;
                lblGeneratoreDismesso.Visible = true;
                lblGeneratoreDismesso.Text = "</br>" + "Generatore dismesso&nbsp;in data &nbsp;" + String.Format("{0:dd/MM/yyyy}", DataDismesso) + "&nbsp;da&nbsp;" + SecurityManager.GetNomeCognomeFromIDUtente(IDUtenteDismesso);
            }
        }
        else if (e.RowType == GridViewRowType.EditForm)
        {
            ASPxPanel panelCombustibileAltro = grdGruppiTermici.FindEditRowCellTemplateControl(grdGruppiTermici.Columns["CombustibileAltro"] as GridViewDataColumn, "pnlCombustibileAltro") as ASPxPanel;
            GridViewDataColumn columnCombustibile = ((ASPxGridView)sender).Columns["IDTipologiaCombustibile"] as GridViewDataColumn;
            ASPxComboBox ddlTipologiaCombustibile = ((ASPxGridView)sender).FindEditRowCellTemplateControl(columnCombustibile, "ddlTipologiaCombustibile") as ASPxComboBox;

            if (ddlTipologiaCombustibile.Items[0].Selected)
            {
                panelCombustibileAltro.ClientVisible = true;
            }
            else
            {
                panelCombustibileAltro.ClientVisible = false;
            }


            ASPxPanel panelFluidoTermovettoreAltro = grdGruppiTermici.FindEditRowCellTemplateControl(grdGruppiTermici.Columns["FluidoTermovettoreAltro"] as GridViewDataColumn, "pnlFluidoTermovettoreAltro") as ASPxPanel;
            GridViewDataColumn columnFluidoTermovettore = ((ASPxGridView)sender).Columns["IDTipologiaFluidoTermovettore"] as GridViewDataColumn;
            ASPxComboBox ddlTipologiaFluidoTermovettore = ((ASPxGridView)sender).FindEditRowCellTemplateControl(columnFluidoTermovettore, "ddlTipologiaFluidoTermovettore") as ASPxComboBox;

            if (ddlTipologiaFluidoTermovettore.Items[0].Selected)
            {
                panelFluidoTermovettoreAltro.ClientVisible = true;
            }
            else
            {
                panelFluidoTermovettoreAltro.ClientVisible = false;
            }
        }
    }

    #endregion

    #region Bruciatori

    protected bool IsDraftBruciatore(int itemId)
    {
        return CurrentDataContext.LIM_LibrettiImpiantiBruciatori.Find(itemId).IDLibrettoImpiantoInserimento == IDLibrettoImpianto;
    }

    protected void grdBruciatori_DetailRowGetButtonVisibility(object sender, DevExpress.Web.ASPxGridViewDetailRowButtonEventArgs e)
    {
        if (IsDraftBruciatore(Convert.ToInt32(e.KeyValue)))
            e.ButtonState = GridViewDetailRowButtonState.Hidden;
    }

    protected virtual void grdBruciatori_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;
        
        int idGruppoTermico = Convert.ToInt32(gridView.GetMasterRowKeyValue());

        e.NewValues["IDUtenteInserimento"] = SecurityManager.GetUserIDUtente(Page.User.Identity.Name);
        e.NewValues["DataInserimento"] = DateTime.Now;
        e.NewValues["IDLibrettoImpiantoInserimento"] = IDLibrettoImpianto;
        e.NewValues["IDLibrettoImpiantoGruppoTermico"] = idGruppoTermico;
        e.NewValues["Prefisso"] = _PrefissoBruciatori;
        e.NewValues["CodiceProgressivo"] = LibrettoImpianto.LIM_LibrettiImpiantiGruppiTermici.Single(g => g.IDLibrettoImpiantoGruppoTermico == idGruppoTermico).LIM_LibrettiImpiantiBruciatori.Count(l => l.fAttivo) + 1;
        e.NewValues["fAttivo"] = true;
    }

    protected virtual void grdBruciatori_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        base.DetailGrid_RowUpdating(sender, e);
    }

    protected void grdBruciatori_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        int idGruppoTermico = Convert.ToInt32(gridView.GetMasterRowKeyValue());

        dsBruciatori.WhereParameters["IDLibrettoImpiantoGruppoTermico"].DefaultValue = idGruppoTermico.ToString();
    }

    protected virtual void grdBruciatori_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

    }

    protected void grdBruciatori_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView) sender;
        
        if (e.ButtonType == ColumnCommandButtonType.Edit)
        {
            //e.Visible = (IsBozza || (IsRevisione && IsDraftBruciatore(GetGridKeyValue(gridView, e.VisibleIndex))));
            e.Visible = (IsBozza || IsRevisione);
        }
        if (e.ButtonType == ColumnCommandButtonType.Delete)
        {
            e.Visible = (IsBozza || (IsRevisione && IsDraftBruciatore(GetGridKeyValue(gridView, e.VisibleIndex))));
            //e.Visible = (IsBozza || IsRevisione);
        }
    }

    protected void grdBruciatori_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (e.ButtonID == "cmdSostituisciBruciatore")
        {
            //attivo la sostituzione solamente in caso di revisione
            if (IsRevisione && !IsDraftBruciatore(GetGridKeyValue(gridView, e.VisibleIndex)))
            {
                e.Visible = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
            }
        }
    }

    protected void grdBruciatori_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (e.ButtonID == "cmdSostituisciBruciatore")
        {
            var idRiga = GetGridKeyValue(gridView, e.VisibleIndex);

            int idGruppoTermico = Convert.ToInt32(gridView.GetMasterRowKeyValue());

            //creo una nuova riga con lo stesso codice e dismetto quella corrente
            var bruciatore = CurrentDataContext.LIM_LibrettiImpiantiBruciatori.Find(idRiga);

            bruciatore.IDUtenteUltimaModifica = int.Parse(SecurityManager.GetUserIDUtente(Page.User.Identity.Name));
            bruciatore.DataUltimaModifica = DateTime.Now;
            bruciatore.DataDismissione = DateTime.Today;
            bruciatore.fAttivo = false;

            var nuovoBruciatore = new LIM_LibrettiImpiantiBruciatori();

            nuovoBruciatore.Prefisso = bruciatore.Prefisso;
            nuovoBruciatore.IDLibrettoImpiantoGruppoTermico = idGruppoTermico;
            nuovoBruciatore.CodiceProgressivo = bruciatore.CodiceProgressivo;
            nuovoBruciatore.IDUtenteInserimento = bruciatore.IDUtenteUltimaModifica;
            nuovoBruciatore.IDLibrettoImpiantoInserimento = IDLibrettoImpianto;
            nuovoBruciatore.Combustibile = bruciatore.Combustibile;
            nuovoBruciatore.Fabbricante = bruciatore.Fabbricante;
            nuovoBruciatore.Matricola = bruciatore.Matricola;
            nuovoBruciatore.Modello = bruciatore.Modello;
            nuovoBruciatore.PortataTermicaMaxNominaleKw = bruciatore.PortataTermicaMaxNominaleKw;
            nuovoBruciatore.PortataTermicaMinNominaleKw = bruciatore.PortataTermicaMinNominaleKw;
            nuovoBruciatore.Tipologia = bruciatore.Tipologia;
            nuovoBruciatore.DataInserimento = bruciatore.DataUltimaModifica;
            nuovoBruciatore.DataInstallazione = bruciatore.DataDismissione;
            nuovoBruciatore.fAttivo = true;

            CurrentDataContext.LIM_LibrettiImpiantiBruciatori.Add(nuovoBruciatore);

            CurrentDataContext.SaveChanges();

            gridView.DataBind();
        }
    }

    protected void grdBruciatori_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (!gridView.IsNewRowEditing)
        {

        }
    }

    protected void grdBruciatori_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        var dataInstallazionePrecedente = Convert.ToDateTime(e.OldValues["DataInstallazione"]);
        var dataInstallazioneCorrente = Convert.ToDateTime(e.NewValues["DataInstallazione"]);

        if (!dataInstallazioneCorrente.Equals(dataInstallazionePrecedente))
        {

            var idRiga = Convert.ToInt32(e.Keys[gridView.KeyFieldName]);

            int idGruppoTermico = Convert.ToInt32(gridView.GetMasterRowKeyValue());

            //creo una nuova riga con lo stesso codice e dismetto quella corrente
            var gruppoTermico = CurrentDataContext.LIM_LibrettiImpiantiGruppiTermici.Find(idGruppoTermico);

            var bruciatore = gruppoTermico.LIM_LibrettiImpiantiBruciatori.Single(b => b.IDLibrettoImpiantoBruciatore == idRiga);

            var componenteSostituito = gruppoTermico.LIM_LibrettiImpiantiBruciatori.Where(l => l.CodiceProgressivo == bruciatore.CodiceProgressivo && l.fAttivo == false && l.DataDismissione.Equals(dataInstallazionePrecedente)).FirstOrDefault();

            if (componenteSostituito != null)
            {
                componenteSostituito.DataDismissione = dataInstallazioneCorrente;
                CurrentDataContext.SaveChanges();
                gridView.DataBind();
            }
        }
    }

    protected void grdBruciatori_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        var idRiga = Convert.ToInt32(e.EditingKeyValue);

        //var bruciatore = CurrentDataContext.LIM_LibrettiImpiantiBruciatori.Find(idRiga);

        //e.Cancel = !IsDraftBruciatore(idRiga);
    }

    protected void grdBruciatoriSostituzioni_BeforePerformDataSelect(object sender, EventArgs e)
    {
        var grid = (sender as DevExpress.Web.ASPxGridView);

        var bruciatore = CurrentDataContext.LIM_LibrettiImpiantiBruciatori.Find(Convert.ToInt32(grid.GetMasterRowKeyValue()));

        dsBruciatoriSostituzioni.WhereParameters["IDLibrettoImpiantoGruppoTermico"].DefaultValue = bruciatore.IDLibrettoImpiantoGruppoTermico.ToString();
        dsBruciatoriSostituzioni.WhereParameters["CodiceProgressivo"].DefaultValue = bruciatore.CodiceProgressivo.ToString();
    }

    #endregion

    #region Recuperatori

    protected bool IsDraftRecuperatore(int itemId)
    {
        return CurrentDataContext.LIM_LibrettiImpiantiRecuperatori.Find(itemId).IDLibrettoImpiantoInserimento == IDLibrettoImpianto;
    }

    protected void grdRecuperatori_DetailRowGetButtonVisibility(object sender, DevExpress.Web.ASPxGridViewDetailRowButtonEventArgs e)
    {
        if (IsDraftRecuperatore(Convert.ToInt32(e.KeyValue)))
            e.ButtonState = GridViewDetailRowButtonState.Hidden;
    }

    protected virtual void grdRecuperatori_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        int idGruppoTermico = Convert.ToInt32(gridView.GetMasterRowKeyValue());

        e.NewValues["IDUtenteInserimento"] = SecurityManager.GetUserIDUtente(Page.User.Identity.Name);
        e.NewValues["DataInserimento"] = DateTime.Now;
        e.NewValues["IDLibrettoImpiantoInserimento"] = IDLibrettoImpianto;
        e.NewValues["IDLibrettoImpiantoGruppoTermico"] = idGruppoTermico;
        e.NewValues["Prefisso"] = _PrefissoRecuperatori;
        e.NewValues["CodiceProgressivo"] = LibrettoImpianto.LIM_LibrettiImpiantiGruppiTermici.Single(g => g.IDLibrettoImpiantoGruppoTermico == idGruppoTermico).LIM_LibrettiImpiantiRecuperatori.Count(l => l.fAttivo) + 1;
        e.NewValues["fAttivo"] = true;
    }

    protected void grdRecuperatori_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        base.DetailGrid_RowUpdating(sender, e);
    }

    protected void grdRecuperatori_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        int idGruppoTermico = Convert.ToInt32(gridView.GetMasterRowKeyValue());

        dsRecuperatori.WhereParameters["IDLibrettoImpiantoGruppoTermico"].DefaultValue = idGruppoTermico.ToString();
    }

    protected void grdRecuperatori_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

    }

    protected void grdRecuperatori_CommandButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCommandButtonEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView) sender;
        
        if (e.ButtonType == ColumnCommandButtonType.Edit)
        {
            //e.Visible = (IsBozza || (IsRevisione && IsDraftRecuperatore(GetGridKeyValue(gridView, e.VisibleIndex))));
            e.Visible = (IsBozza || IsRevisione);
        }
        if (e.ButtonType == ColumnCommandButtonType.Delete)
        {
            e.Visible = (IsBozza || (IsRevisione && IsDraftRecuperatore(GetGridKeyValue(gridView, e.VisibleIndex))));
            //e.Visible = (IsBozza || IsRevisione);
        }
    }

    protected void grdRecuperatori_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (e.ButtonID == "cmdSostituisciRecuperatore")
        {
            //attivo la sostituzione solamente in caso di revisione
            if (IsRevisione && !IsDraftRecuperatore(GetGridKeyValue(gridView, e.VisibleIndex)))
            {
                e.Visible = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
            }
        }
    }

    protected void grdRecuperatori_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (e.ButtonID == "cmdSostituisciRecuperatore")
        {
            var idRiga = GetGridKeyValue(gridView, e.VisibleIndex);

            int idGruppoTermico = Convert.ToInt32(gridView.GetMasterRowKeyValue());

            //creo una nuova riga con lo stesso codice e dismetto quella corrente
            var recuperatore = CurrentDataContext.LIM_LibrettiImpiantiRecuperatori.Find(idRiga);

            recuperatore.IDUtenteUltimaModifica = int.Parse(SecurityManager.GetUserIDUtente(Page.User.Identity.Name));
            recuperatore.DataUltimaModifica = DateTime.Now;
            recuperatore.DataDismissione = DateTime.Today;
            recuperatore.fAttivo = false;

            var nuovoRecuperatore = new LIM_LibrettiImpiantiRecuperatori();

            nuovoRecuperatore.Prefisso = recuperatore.Prefisso;
            nuovoRecuperatore.IDLibrettoImpiantoGruppoTermico = idGruppoTermico;
            nuovoRecuperatore.CodiceProgressivo = recuperatore.CodiceProgressivo;
            nuovoRecuperatore.IDUtenteInserimento = recuperatore.IDUtenteUltimaModifica;
            nuovoRecuperatore.IDLibrettoImpiantoInserimento = IDLibrettoImpianto;
            nuovoRecuperatore.Fabbricante = recuperatore.Fabbricante;
            nuovoRecuperatore.Matricola = recuperatore.Matricola;
            nuovoRecuperatore.Modello = recuperatore.Modello;
            nuovoRecuperatore.PortataTermicaNominaleTotaleKw = recuperatore.PortataTermicaNominaleTotaleKw;
            nuovoRecuperatore.DataInserimento = recuperatore.DataUltimaModifica;
            nuovoRecuperatore.DataInstallazione = recuperatore.DataDismissione;
            nuovoRecuperatore.fAttivo = true;

            CurrentDataContext.LIM_LibrettiImpiantiRecuperatori.Add(nuovoRecuperatore);

            CurrentDataContext.SaveChanges();

            gridView.DataBind();
        }
    }

    protected void grdRecuperatori_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        if (!gridView.IsNewRowEditing)
        {

        }
    }

    protected void grdRecuperatori_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        var dataInstallazionePrecedente = Convert.ToDateTime(e.OldValues["DataInstallazione"]);
        var dataInstallazioneCorrente = Convert.ToDateTime(e.NewValues["DataInstallazione"]);

        if (!dataInstallazioneCorrente.Equals(dataInstallazionePrecedente))
        {
            var idRiga = Convert.ToInt32(e.Keys[gridView.KeyFieldName]);

            int idGruppoTermico = Convert.ToInt32(gridView.GetMasterRowKeyValue());

            //creo una nuova riga con lo stesso codice e dismetto quella corrente
            var gruppoTermico = CurrentDataContext.LIM_LibrettiImpiantiGruppiTermici.Find(idGruppoTermico);

            var recuperatore = gruppoTermico.LIM_LibrettiImpiantiRecuperatori.Single(b => b.IDLibrettoImpiantoRecuperatore == idRiga);

            var componenteSostituito = gruppoTermico.LIM_LibrettiImpiantiRecuperatori.Where(l => l.CodiceProgressivo == recuperatore.CodiceProgressivo && l.fAttivo == false && l.DataDismissione.Equals(dataInstallazionePrecedente)).FirstOrDefault();

            if (componenteSostituito != null)
            {
                componenteSostituito.DataDismissione = dataInstallazioneCorrente;
                CurrentDataContext.SaveChanges();
                gridView.DataBind();
            }
        }
    }
    protected void grdRecuperatori_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        var idRiga = Convert.ToInt32(e.EditingKeyValue);

        //e.Cancel = !IsDraftRecuperatore(idRiga);
    }

    protected void grdRecuperatoriSostituzioni_BeforePerformDataSelect(object sender, EventArgs e)
    {
        var grid = (sender as DevExpress.Web.ASPxGridView);

        var recuperatore = CurrentDataContext.LIM_LibrettiImpiantiRecuperatori.Find(Convert.ToInt32(grid.GetMasterRowKeyValue()));

        dsRecuperatoriSostituzioni.WhereParameters["IDLibrettoImpiantoGruppoTermico"].DefaultValue = recuperatore.IDLibrettoImpiantoGruppoTermico.ToString();
        dsRecuperatoriSostituzioni.WhereParameters["CodiceProgressivo"].DefaultValue = recuperatore.CodiceProgressivo.ToString();
    }
    #endregion

    protected void dsGruppiTermici_Inserted(object sender, Microsoft.AspNet.EntityDataSource.EntityDataSourceChangedEventArgs e)
    {
        int idGruppoTermico = ((LIM_LibrettiImpiantiGruppiTermici)e.Entity).IDLibrettoImpiantoGruppoTermico;

        grdGruppiTermici.DetailRows.CollapseAllRows();
        grdGruppiTermici.DetailRows.ExpandRowByKey(idGruppoTermico);
    }
    
    #region Validazione

    protected void controllaCampoDataFutura(object sender, ASPxDataValidationEventArgs e, string nomeCampo, string nomeCaption)
    {
        ASPxGridView grid = sender as ASPxGridView;

        if(grid != null)
        {
            DateTime dt;

            if(DateTime.TryParse(e.NewValues[nomeCampo].ToString(), out dt))
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


    protected void grdGruppiTermici_OnRowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        controllaCampoDataFutura(sender, e, "DataInstallazione", "data installazione");
    }


    protected void grdBruciatori_OnRowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        controllaCampoDataFutura(sender, e, "DataInstallazione", "data installazione");
    }


    protected void grdRecuperatori_OnRowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        controllaCampoDataFutura(sender, e, "DataInstallazione", "data installazione");
    }

    #endregion

    

}
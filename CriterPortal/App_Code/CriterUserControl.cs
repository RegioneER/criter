using System;
using DataLayer;
using DataUtilityCore;
using DevExpress.Web;

/// <summary>
/// Summary description for UserControl
/// </summary>
public class CriterUserControl : System.Web.UI.UserControl
{
    private CriterDataModel _CurrentDataContext;

    public CriterDataModel CurrentDataContext
    {
        get
        {
            if (_CurrentDataContext == null)
            {
                _CurrentDataContext = DataLayer.Common.ApplicationContext.Current.Context;
            }
            return _CurrentDataContext;
        }
    }

    private LIM_LibrettiImpianti _LibrettoImpianto;

    public LIM_LibrettiImpianti LibrettoImpianto
    {
        get 
        {
            if (_LibrettoImpianto == null)
            {
                _LibrettoImpianto = CurrentDataContext.LIM_LibrettiImpianti.Find(IDLibrettoImpianto);
            }
            return _LibrettoImpianto;
        }
    }
    
    public int IDLibrettoImpianto { get; set; }
       
    public CriterUserControl()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public virtual int? iDTargaturaImpianto
    {
        get
        {
            return LibrettoImpianto.IDTargaturaImpianto;
        }
    }

    public virtual bool IsBozza
    {
        get
        {
            if (LibrettoImpianto.IDStatoLibrettoImpianto == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public virtual bool IsDefinitivo
    {
        get
        {
            if (LibrettoImpianto.IDStatoLibrettoImpianto == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public virtual bool IsRevisione
    {
        get
        {
            if (LibrettoImpianto.IDStatoLibrettoImpianto == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
        
    protected virtual bool IsDraftElementoGridPrincipale(int itemId)
    {
        return false;
    }

    protected ASPxEdit GetEditorControl(ASPxGridView grid, string columnName, string containerPanelId, string editorName)
    {
        GridViewDataColumn col = grid.Columns[columnName] as GridViewDataColumn;

        var panel = grid.FindEditRowCellTemplateControl(col, containerPanelId) as DevExpress.Web.ASPxPanel;

        return panel.FindControl(editorName) as ASPxEdit;
    }
    
    protected void SetValue(System.Collections.Specialized.OrderedDictionary values, string key, ASPxEdit editor)
    {
        if (editor != null)
            values[key] = editor.Value;
        else
            values[key] = null;
    }

    private bool _Enabled = true;

    public bool Enabled
    {
        get
        {
            return _Enabled;
        }
        set
        {
            _Enabled = value;
            EnableControls(this, value);
        }
    }

    private void EnableControls(System.Web.UI.Control control, bool state)
    {
        foreach (System.Web.UI.Control c in control.Controls)
        {
            if (c is ASPxGridView)
            {
                EnableGridEditing(c as ASPxGridView, state);
            }
            // Recurse into child controls.
            if (c.Controls.Count > 0)
            {
                this.EnableControls(c, state);
            }
        }
    }

    public void SetIDLibrettoImpianto(string id)
    {
        int idLibretto = 0;

        int.TryParse(id, out idLibretto);

        this.IDLibrettoImpianto = idLibretto;
    }
    
    protected void DetailGrid_DetailRowGetButtonVisibility(object sender, DevExpress.Web.ASPxGridViewDetailRowButtonEventArgs e)
    {
        if (IsDraftElementoGridPrincipale(Convert.ToInt32(e.KeyValue)))
            e.ButtonState = GridViewDetailRowButtonState.Hidden;
    }
    
    protected virtual void DetailGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["IDLibrettoImpianto"] = IDLibrettoImpianto;
        e.NewValues["IDUtenteInserimento"] = SecurityManager.GetUserIDUtente(Page.User.Identity.Name);
        e.NewValues["DataInserimento"] = DateTime.Now;
    }

    protected virtual void DetailGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

    }

    protected virtual void DetailGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["IDUtenteUltimaModifica"] = SecurityManager.GetUserIDUtente(Page.User.Identity.Name);
        e.NewValues["DataUltimaModifica"] = DateTime.Now;
    }

    protected void DetailGrid_DataBound(object sender, EventArgs e)
    {
        ASPxGridView gridView = (ASPxGridView)sender;

        //Ho attivato questa opzioni in quanto le grid figlie ad esempio bruciatori e recuperatori con libretto definitivo si vedevano i pulsanti NUOVO
        if (!Enabled)
        {
            EnableGridEditing(gridView, Enabled);
        }

        //2016-07-28 espansione di tutte le righe di dettaglio
        gridView.DetailRows.ExpandAllRows();
        //2016-07-28 espansione di tutte le righe di dettaglio
    }

    protected void EnableGridEditing(ASPxGridView gridView, bool state)
    {
        gridView.SettingsDataSecurity.AllowInsert = gridView.SettingsDataSecurity.AllowEdit = gridView.SettingsDataSecurity.AllowDelete = state;
    }
    
}
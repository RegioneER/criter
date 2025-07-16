using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataUtilityCore;
using DevExpress.Web;
using EncryptionQS;
using NLog.Internal;
using DevExpress.Web.Data;
using DataLayer;

public partial class WebUserControls_LibrettiImpianto_TerzoResponsabile : CriterUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void grdTerziResponsabili_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        VisibleNewButton();
    }

    protected void VisibleNewButton()
    {
        int count = CurrentDataContext.LIM_LibrettiImpiantiResponsabili.Count(r => r.IDLibrettoImpianto == this.IDLibrettoImpianto && r.fAttivo);
        bool fVisible = true;
        if (count >= 1)
        {
            fVisible = false;
        }

        var column = (GridViewCommandColumn) grdTerziResponsabili.Columns[0];
        column.ShowNewButtonInHeader = fVisible;
    }

    protected void grdTerziResponsabili_BeforePerformDataSelect(object sender, EventArgs e)
    {
        dsTerziResponsabili.WhereParameters["IDLibrettoImpianto"].DefaultValue = IDLibrettoImpianto.ToString();
        VisibleNewButton();
    }

    protected virtual void grdTerziResponsabili_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        base.DetailGrid_RowInserting(sender, e);

        e.NewValues["IDSoggetto"] = LibrettoImpianto.IDSoggettoDerived;
        e.NewValues["fAttivo"] = true;
    }

    protected void grdTerziResponsabili_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
    {
        using (var ctx = new CriterDataModel())
        {
            var dbAnagrafica = ctx.COM_AnagraficaSoggetti.FirstOrDefault(c => c.IDSoggetto == LibrettoImpianto.IDSoggettoDerived);
            var countTerzoResponsabile = ctx.COM_RuoliSoggetti.Where(c => c.IDSoggetto == LibrettoImpianto.IDSoggettoDerived & c.IDRuoloSoggetto == 3).ToList().Count();

            if (countTerzoResponsabile > 0)
            {
                e.NewValues["Nome"] = dbAnagrafica.Nome;
                e.NewValues["Cognome"] = dbAnagrafica.Cognome;
                e.NewValues["RagioneSociale"] = dbAnagrafica.NomeAzienda;
                e.NewValues["PartitaIva"] = dbAnagrafica.PartitaIVA;
                e.NewValues["IDProvinciaCciaa"] = dbAnagrafica.IDProvinciaIscrizioneAlboImprese;
                e.NewValues["NumeroCciaa"] = dbAnagrafica.NumeroIscrizioneAlboImprese;
                e.NewValues["Email"] = dbAnagrafica.Email;
                e.NewValues["Pec"] = dbAnagrafica.EmailPec;
            }
        }
    }

    protected virtual void grdTerziResponsabili_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        var idTerzoResponsabile = Convert.ToInt32(e.Keys["IDLibrettoImpiantoResponsabili"]);

        var responsabile = CurrentDataContext.LIM_LibrettiImpiantiResponsabili.Find(idTerzoResponsabile);

        responsabile.fAttivo = false;

        CurrentDataContext.SaveChanges();

        e.Cancel = true;
        //VisibleNewButton();
        grdTerziResponsabili.DataBind();
    }

    public bool AllowInsert
    {
        get
        {
            return grdTerziResponsabili.SettingsDataSecurity.AllowInsert && CurrentDataContext.LIM_LibrettiImpiantiResponsabili.Count(r => r.IDLibrettoImpianto == this.IDLibrettoImpianto && r.fAttivo) == 0;
        }
    }

    public void AddNewRow()
    {
        if (AllowInsert)
        {
            //Page.ClientScript.RegisterStartupScript(typeof(string), "TerziResponsabili_AddNewRow", "grdTerziResponsabili.AddNewRow();", true);
            grdTerziResponsabili.AddNewRow();
        }
    }

    public void DeleteRow()
    {
        var responsabile = CurrentDataContext.LIM_LibrettiImpiantiResponsabili.Where(r => r.IDLibrettoImpianto == this.IDLibrettoImpianto && r.fAttivo).FirstOrDefault();
        if (responsabile != null)
        {
            CurrentDataContext.LIM_LibrettiImpiantiResponsabili.Remove(responsabile);
            CurrentDataContext.SaveChanges();
            grdTerziResponsabili.DataBind();
        }
    }


    protected void controllaCampiMinMax(object sender, ASPxDataValidationEventArgs e, string nomeCampo1, string nomeCaption1, string nomeCampo2, string nomeCaption2)
    {
        ASPxGridView grid = sender as ASPxGridView;

        if (grid != null)
        {
            DateTime numberMin;
            DateTime numberMax;

            if (DateTime.TryParse(e.NewValues[nomeCampo1].ToString(), out numberMin) && DateTime.TryParse(e.NewValues[nomeCampo2].ToString(), out numberMax))
            {
                if (numberMin > numberMax)
                {
                    //e.Errors[grid.Columns[nomeCampo1]] = "Valore non ammesso.";
                    //e.Errors[grid.Columns[nomeCampo2]] = "Valore non ammesso.";

                    e.RowError = "Errore: Il valore della " + nomeCaption1 + " risulta maggiore della " + nomeCaption2 + "!";
                }
            }
        }
    }

    protected void grdPrincipale_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        controllaCampiMinMax(sender, e, "DataInizio", "data inizio incarico terzo responsabile", "DataFine", "data fine incarico terzo responsabile");
    }




}
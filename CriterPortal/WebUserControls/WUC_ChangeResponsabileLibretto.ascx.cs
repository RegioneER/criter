using DataLayer;
using DataUtilityCore;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

public partial class WebUserControls_WUC_ChangeResponsabileLibretto : System.Web.UI.UserControl
{
    public string IDLibrettoImpianto
    {
        get { return lblIDLibrettoImpianto.Text; }
        set
        {
            lblIDLibrettoImpianto.Text = value;
        }
    }

    public string IDTargaturaImpianto
    {
        get { return lblIDTargaturaImpianto.Text; }
        set
        {
            lblIDTargaturaImpianto.Text = value;
        }
    }

    public string IDAccertamento
    {
        get { return lblIDAccertamento.Text; }
        set
        {
            lblIDAccertamento.Text = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindResponsabiliImpianto();
        }        
    }

    protected void BindResponsabiliImpianto()
    {
        using (var ctx = new CriterDataModel())
        {
            var IDTargaturaImpiantoInt = int.Parse(IDTargaturaImpianto);

            var Listlibretti = (from LIM_LibrettiImpianti in ctx.LIM_LibrettiImpianti
                            where LIM_LibrettiImpianti.IDTargaturaImpianto == IDTargaturaImpiantoInt
                            select new
                            {
                                LIM_LibrettiImpianti.IDLibrettoImpianto,
                                LIM_LibrettiImpianti.IDTargaturaImpianto,
                                LIM_LibrettiImpianti.IDStatoLibrettoImpianto,
                                Responsabile = (LIM_LibrettiImpianti.IDTipoSoggetto == 2) ? LIM_LibrettiImpianti.RagioneSocialeResponsabile + " - "+ (LIM_LibrettiImpianti.NomeResponsabile + " " + LIM_LibrettiImpianti.CognomeResponsabile) : (LIM_LibrettiImpianti.NomeResponsabile + " " + LIM_LibrettiImpianti.CognomeResponsabile),
                                IndirizzoResponsabile = (LIM_LibrettiImpianti.IndirizzoResponsabile + " " + LIM_LibrettiImpianti.CivicoResponsabile + " " + LIM_LibrettiImpianti.CapResponsabile + " " + LIM_LibrettiImpianti.SYS_CodiciCatastali1.Comune + "(" + LIM_LibrettiImpianti.SYS_CodiciCatastali1.SYS_Province.SiglaProvincia + ")"),
                                LIM_LibrettiImpianti.fAttivo,
                                LIM_LibrettiImpianti.IndirizzoNormalizzatoResponsabile,
                                LIM_LibrettiImpianti.CivicoNormalizzatoResponsabile,
                                LIM_LibrettiImpianti.IDComuneResponsabile,
                                LIM_LibrettiImpianti.IDProvinciaResponsabile
                            }).AsNoTracking().ToList();

            gridResponsabiliLibretti.DataSource = Listlibretti;
            gridResponsabiliLibretti.DataBind();

            gridResponsabiliLibretti.Selection.SetSelectionByKey(IDLibrettoImpianto, true);
        }       
    }

    protected void gridResponsabiliLibretti_SelectionChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        var IDLibrettoSelezionato = int.Parse(grid.GetSelectedFieldValues("IDLibrettoImpianto").FirstOrDefault().ToString());

        using (var ctx = new CriterDataModel())
        {
            var IDAccertamentoLong = long.Parse(IDAccertamento);
            var sanzione = ctx.VER_Accertamento.Where(c => c.IDAccertamento == IDAccertamentoLong).FirstOrDefault();
            sanzione.IDLibrettoImpianto = IDLibrettoSelezionato;
            ctx.SaveChanges();

        }
    }
}
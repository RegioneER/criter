using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataUtilityCore;
using DataLayer;
using System.Linq;
using System.Linq.Dynamic;
using DevExpress.Data.ODataLinq.Helpers;
using DevExpress.Data.WcfLinq.Helpers;
using DevExpress.Web;
using System.Data.Entity;

public partial class ElencoImpreseManutentriciAlbo : System.Web.UI.Page
{
    public string RandomNumber = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadProvince(null);
        }
        int iDProvincia = int.Parse(lbProvince.SelectedItem.Value.ToString());
        GetMapImage(iDProvincia.ToString());
        GetImprese(iDProvincia, txtNomeAzienda.Text, rblTipoRicerca.SelectedItem.Value.ToString());
        lblDataAggiornamento.Text = "aggiornato al&nbsp;" + string.Format("{0:dd/MM/yyyy}", DateTime.Now);
    }

    protected void LoadProvince(int? iDPresel)
    {
        lbProvince.ValueField = "iDProvincia";
        lbProvince.TextField = "Provincia";
        lbProvince.DataSource = LoadDropDownList.LoadDropDownList_SYS_Province(iDPresel, true);
        lbProvince.DataBind();

        lbProvince.SelectedIndex = 0;
    }

    protected void lbProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iDProvincia = int.Parse(lbProvince.SelectedItem.Value.ToString());
        GetMapImage(iDProvincia.ToString());
        GetImprese(iDProvincia, null, rblTipoRicerca.SelectedItem.Value.ToString());
    }

    public void GetImprese(int? iDProvincia, string impresa, string typeOfSearch)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            var rnd = new Random();
            var soggetti = (from aa in ctx.COM_AnagraficaSoggettiAlbo
                            join an in ctx.COM_AnagraficaSoggetti on aa.IDSoggetto equals an.IDSoggetto
                            join pc in ctx.COM_ProvinceCompetenza on an.IDSoggetto equals pc.IDSoggetto
                            join pro in ctx.SYS_Province on pc.IDProvincia equals pro.IDProvincia
                            join ana in ctx.COM_AnagraficaSoggettiAccreditamento on an.IDSoggetto equals ana.IDSoggetto
                            join sa in ctx.SYS_StatoAccreditamento on ana.IDStatoAccreditamento equals sa.IDStatoAccreditamento
                            select new
                            {
                                aa.IDSoggettoAlbo,
                                aa.IDSoggetto,
                                aa.AmministratoreDelegato,
                                aa.Impresa,
                                aa.Cap,
                                aa.Citta,
                                aa.Email,
                                aa.EmailPec,
                                aa.Fax,
                                aa.Indirizzo,
                                aa.PartitaIVA,
                                aa.SitoWeb,
                                aa.Telefono,
                                aa.fAmministratoreDelegato,
                                aa.fEmail,
                                aa.fEmailPec,
                                aa.fFax,
                                aa.fPartitaIVA,
                                aa.fSitoWeb,
                                aa.fTelefono,
                                sa.StatoAccreditamento,
                                sa.IDStatoAccreditamento,
                                pc.IDProvincia,
                                Provincia = pro.Provincia
                            }
                            ).AsQueryable().Where(a => a.IDStatoAccreditamento == 8 || a.IDStatoAccreditamento == 9); //Imprese Accreditate e Sospese

            if (typeOfSearch == "0")
            {
                soggetti = soggetti.Where(a => a.IDProvincia == iDProvincia);
            }
            else if (typeOfSearch == "1")
            {
                soggetti = soggetti.Where(a => a.Impresa.ToLower().Contains(impresa));
            }

            var result = soggetti.AsEnumerable().GroupBy(l => new { l.IDSoggetto, l.Impresa, l.StatoAccreditamento }).Select(l => new
            {
                IDSoggetto = l.Key.IDSoggetto,
                Impresa = l.Key.Impresa.ToUpper(),
                Provincia = string.Join(",", l.Select(i => i.Provincia)).ToUpper(),
                StatoAccreditamento = l.Key.StatoAccreditamento.ToUpper()
            });

            DataGrid.DataSource = result.ToList().OrderBy(x => rnd.Next());
            DataGrid.DataBind();
        }
    }

    public static string GetImpreseRuoli(int IDSoggetto)
    {
        string ruoli = string.Empty;

        using (CriterDataModel ctx = new CriterDataModel())
        {
            var ruoliDB = (from an in ctx.COM_AnagraficaSoggetti
                           join rs in ctx.COM_RuoliSoggetti on an.IDSoggetto equals rs.IDSoggetto
                           join lst in ctx.SYS_RuoloSoggetto on rs.IDRuoloSoggetto equals lst.IDRuoloSoggetto
                           where an.IDSoggetto == IDSoggetto
                           select new
                           {
                               lst.RuoloSoggetto

                           }
                            ).AsNoTracking().ToList().Select(query => query.RuoloSoggetto).Aggregate((a, b) => a + "<br/>" + b);

            return ruoli = ruoliDB.ToUpper();
        }
    }



    protected void callbackPanel_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            int iDSoggetto = Convert.ToInt32(e.Parameter);

            var soggetto = (from a in ctx.COM_AnagraficaSoggettiAlbo
                            where (a.IDSoggetto == iDSoggetto)
                            join c in ctx.SYS_Province on a.IDProvincia equals c.IDProvincia
                            select a).AsQueryable().FirstOrDefault();

            // Dettaglio Info

            lblSoggetto.Text = "Dati Impresa di Manutezione/Installazione&nbsp;&mdash;&nbsp;" + soggetto.Impresa;

            if (soggetto.fTelefono)
            {
                lblTelefono.Text = soggetto.Telefono;
            }
            if (soggetto.fEmail)
            {
                lblEmail.Text = soggetto.Email;
            }
            if (soggetto.fEmailPec)
            {
                lblEmailPec.Text = soggetto.EmailPec;
            }
            if (soggetto.fFax)
            {
                lblFax.Text = soggetto.Fax;
            }
            if (!string.IsNullOrEmpty(soggetto.Citta))
            {
                lblCitta.Text = soggetto.Citta;
            }
            if (!string.IsNullOrEmpty(soggetto.Indirizzo))
            {
                lblIndirizzo.Text = soggetto.Indirizzo;
            }
            if (!string.IsNullOrEmpty(soggetto.Cap))
            {
                lblCap.Text = soggetto.Cap;
            }
            lblProvincia.Text = soggetto.SYS_Province.Provincia;
            if (soggetto.fSitoWeb)
            {
                lblSitoWeb.Text = soggetto.SitoWeb;
            }
            if (soggetto.fPartitaIVA)
            {
                lblPartitaIVA.Text = soggetto.PartitaIVA;
            }
            if (soggetto.fAmministratoreDelegato)
            {
                lblAmministratoreDelegato.Text = soggetto.AmministratoreDelegato;
            }
        }
    }

    protected void mappaEmilia_Click(object sender, ImageMapEventArgs e)
    {
        GetMapImage(e.PostBackValue);
        lbProvince.Items.FindByValue(e.PostBackValue.ToString()).Selected = true;
        GetImprese(int.Parse(e.PostBackValue), null, rblTipoRicerca.SelectedItem.Value.ToString());
    }

    public void GetMapImage(string iDProvincia)
    {
        switch (iDProvincia)
        {
            case "8":
                mappaEmilia.ImageUrl = "~/images/ImageMapER/BO.png";
                break;
            case "23":
                mappaEmilia.ImageUrl = "~/images/ImageMapER/FC.png";
                break;
            case "53":
                mappaEmilia.ImageUrl = "~/images/ImageMapER/RN.png";
                break;
            case "37":
                mappaEmilia.ImageUrl = "~/images/ImageMapER/MO.png";
                break;
            case "51":
                mappaEmilia.ImageUrl = "~/images/ImageMapER/RE.png";
                break;
            case "46":
                mappaEmilia.ImageUrl = "~/images/ImageMapER/PR.png";
                break;
            case "50":
                mappaEmilia.ImageUrl = "~/images/ImageMapER/RA.png";
                break;
            case "40":
                mappaEmilia.ImageUrl = "~/images/ImageMapER/PC.png";
                break;
            case "20":
                mappaEmilia.ImageUrl = "~/images/ImageMapER/FE.png";
                break;
        }
    }

    protected void rblTipoRicerca_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblTipoRicerca.SelectedItem.Value.ToString() == "0")
        {
            tblFilterProvince.Visible = true;
            tblGrid.Visible = true;
            rowRicercaAzienda1.Visible = false;
            rowRicercaAzienda2.Visible = false;
            rowRicercaAzienda3.Visible = false;
            //lblInfoDataGrid.Visible = true;
            //lblInfoFilterProvince.Visible = true;
            btnRicerca.Visible = false;
        }
        else if (rblTipoRicerca.SelectedItem.Value.ToString() == "1")
        {
            tblFilterProvince.Visible = false;
            tblGrid.Visible = false;
            rowRicercaAzienda1.Visible = true;
            rowRicercaAzienda2.Visible = true;
            rowRicercaAzienda3.Visible = true;
            //lblInfoDataGrid.Visible = false;
            //lblInfoFilterProvince.Visible = false;
            btnRicerca.Visible = true;
        }
    }

    protected void btnRicerca_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            tblGrid.Visible = true;
            GetImprese(null, txtNomeAzienda.Text, rblTipoRicerca.SelectedItem.Value.ToString());
        }
    }

    protected void DataGrid_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            //string keyValue = DataGrid.GetRowValues(e.VisibleIndex, "IDSoggetto").ToString();

            //Label lblRuoli = DataGrid.FindRowCellTemplateControl(e.VisibleIndex, null, "lblRuoli") as Label;
            //lblRuoli.Text = GetImpreseRuoli(int.Parse(keyValue));
        }
    }
}
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using EncryptionQS;
using System.Configuration;
using DataUtilityCore;

public partial class MNG_UtentiSearch : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "MNG_UtentiSearch.aspx");

        if (!Page.IsPostBack)
        {
            ddlGruppi(null);
        }
    }

    public void ddlGruppi(int? iDPresel)
    {
        var ls = LoadDropDownList.LoadDropDownList_SYS_UserRole(iDPresel);
        ddlUserRole.DataValueField = "IDRole";
        ddlUserRole.DataTextField = "Role";
        ddlUserRole.DataSource = ls;
        ddlUserRole.DataBind();

        ddlUserRole.Items.Insert(0, "-- Selezionare --");
        ddlUserRole.SelectedIndex = 0;
    }
    
    protected void btnRicercaUtenti_Click(object sender, EventArgs e)
    {
        using (var ctx = new CriterDataModel())
        {
            var query = (from u in ctx.V_COM_Utenti
                          orderby u.IDUtente ascending
                          select new
                          {
                              IDUtente = u.IDUtente,
                              IDRuolo = u.IDRuolo,
                              Ruolo = u.Ruolo,
                              IDSoggetto = u.IDSoggetto,
                              Utente = u.Utente,
                              Username = u.Username,
                              Password = u.Password,
                              DataUltimaModificaPassword = u.DataUltimaModificaPassword,
                              DataScadenzaPassword = u.DataScadenzaPassword,
                              CodiceSoggetto = u.CodiceSoggetto,
                              DataUltimoAccesso = u.DataUltimoAccesso,
                              NrTentativiFalliti = u.NrTentativiFalliti,
                              fattivoUser = u.fattivoUser,
                              fBloccato = u.fBloccato,
                              DataPrimoLogonFallito = u.DataPrimoLogonFallito,
                              fSpid = u.fSpid,
                              keyApi = u.KeyApi
                          });
                        
            if (ddlUserRole.SelectedIndex != 0)
            {
                int IDRuolo = int.Parse(ddlUserRole.SelectedItem.Value);
                query = query.Where(u => u.IDRuolo == IDRuolo);
            }
            if (txtUserName.Text != string.Empty)
            {
                query = query.Where(u => u.Username == txtUserName.Text);
            }
            if (txtCodiceSoggetto.Text != string.Empty)
            {
                query = query.Where(u => u.CodiceSoggetto == txtCodiceSoggetto.Text);
            }
            if (!string.IsNullOrEmpty(txtSoggetto.Text))
            {
                query = query.Where(u => u.Utente.Contains(txtSoggetto.Text));
            }
            if (chkUtenzaAttiva.Checked)
            {
                query = query.Where(u => u.fattivoUser == true);
            }
            else
            {
                query = query.Where(u => u.fattivoUser == false);
            }
            switch (rblBloccati.SelectedValue)
            {
                case "0": break;
                case "1": query = query.Where(u => u.fBloccato == false); break;
                case "2": query = query.Where(u => u.fBloccato == true); break;
            }
            switch (rblTipoIscrizione.SelectedValue)
            {
                case "0": break;
                case "1": query = query.Where(u => u.fSpid == false); break;
                case "2": query = query.Where(u => u.fSpid == true); break;
            }

            dgUtenti.DataSource = query.ToList();
            dgUtenti.DataBind();

            UtilityApp.SetVisuals(dgUtenti, query.ToList().Count, lblCount, "UTENTI CORRISPONDENTI AI PARAMETRI IMPOSTATI");
        }
    }
    
    protected void dgUtenti_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgUtenti.CurrentPageIndex = e.NewPageIndex;
        btnRicercaUtenti_Click(null, null);
    }

    public void dgUtenti_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Label lblKeyApi = (Label)(e.Item.Cells[5].FindControl("lblKeyApi"));
            if ((e.Item.Cells[13].Text != "" && e.Item.Cells[13].Text != "&nbsp;"))
            {
                lblKeyApi.Visible = true;
                lblKeyApi.Text = "Api key: " + e.Item.Cells[13].Text;
            }
            else
            {
                lblKeyApi.Visible = false;
            }
            
            ImageButton btnAttiva = (ImageButton) (e.Item.Cells[12].FindControl("btnAttiva"));
            bool fAttivo = bool.Parse(e.Item.Cells[2].Text);
            if (fAttivo)
            {
                btnAttiva.Visible = false;
            }
            else
            {
                btnAttiva.Visible = true;
            }
        }
    }
    
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton) sender;
        DataGridItem dgi = (DataGridItem)btn.NamingContainer;
        if (ConfigurationManager.AppSettings["fEncryptQueryString"] == "on")
        {
            QueryString qs = new QueryString();
            qs.Add("IdUtente", dgi.Cells[0].Text);
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "MNG_Utenti.aspx";
            url += qsEncrypted.ToString();
            Response.Redirect(url);
        }
    }

    protected void btnAttiva_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton) sender;
        DataGridItem dgi = (DataGridItem) btn.NamingContainer;

        int iDSoggetto = int.Parse(dgi.Cells[1].Text);
        using (var ctx = new CriterDataModel())
        {
            var user = new COM_Utenti();
            user = ctx.COM_Utenti.FirstOrDefault(i => i.IDSoggetto == iDSoggetto);
            user.fAttivo = true;
            ctx.SaveChanges();
            EmailNotify.SendConfermaCredenziali(iDSoggetto);
        }
        btnRicercaUtenti_Click(null, null);
    }

}
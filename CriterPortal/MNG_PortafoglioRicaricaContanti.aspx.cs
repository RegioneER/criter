using System;
using System.Linq;
using System.Web;
using DataUtilityCore;
using DataLayer;
using DataUtilityCore.Portafoglio;
using System.Configuration;
using System.Globalization;
using DevExpress.Web;
using EncryptionQS;

public partial class MNG_PortafoglioRicaricaContanti : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "MNG_PortafoglioRicaricaContanti.aspx");

        cvDataVersamento.ValueToCompare = DateTime.Today.AddDays(-30).ToString("dd/MM/yyyy");

        if (!this.Page.IsPostBack)
        {
            PagePermission();
            ASPxComboBox1.Focus();
        }
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                ASPxComboBox1.Visible = true;
                lblSoggetto.Visible = false;

                rfvASPxComboBox1.Enabled = true;
                break;
            case "2": //Amministratore azienda
                ASPxComboBox1.Value = getVal[0].ToString();
                ASPxComboBox1.Visible = false;

                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0].ToString());
                lblSoggetto.Visible = true;
                break;
            case "3": //Operatore/Addetto
                ASPxComboBox1.Visible = false;
                lblSoggetto.Visible = true;
                ASPxComboBox1.Value = getVal[2].ToString();
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                break;
        }
    }

    #region RICERCA AZIENDE
    protected void ASPxComboBox_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox) source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "2", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox) source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "2", e.Value.ToString(), "");
        comboBox.DataBind();
    }

    protected void ASPxComboBox1_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBox();
    }

    protected void RefreshAspxComboBox()
    {
        ASPxComboBox1.SelectedIndex = -1;
        ASPxComboBox1.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "2", "", "", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
        ASPxComboBox1.DataBind();
    }
    #endregion

    public void RedirectPage(string iDSoggetto, string iDMovimento)
    {
        QueryString qs = new QueryString();
        qs.Add("iDSoggetto", iDSoggetto);
        qs.Add("iDMovimento", iDMovimento);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "MNG_Portafoglio.aspx";
        url += qsEncrypted.ToString();
        Response.Redirect(url);
    }

    protected void chkDisableValidationDate_OnCheckedChanged(object sender, EventArgs e)
    {
        cvDataVersamento.Enabled = !chkDisableValidationDate.Checked;
    }

    protected void btnContanti_Click(Object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            decimal importo = 0;
            int quantita = 0;
            decimal CostoBollino;
            decimal.TryParse(ConfigurationManager.AppSettings["CostoBollino"], NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out CostoBollino);

            if (decimal.TryParse(this.txtImportoContanti.Text, out importo))
            {
                if (ASPxComboBox1.Value != null)
                {
                    int iDSoggetto = int.Parse(ASPxComboBox1.Value.ToString());
                    Portafoglio portafoglio = Portafoglio.Load(iDSoggetto);
                    if (portafoglio != null)
                    {
                        using (var ctx = new CriterDataModel())
                        {
                            var soggetto = ctx.V_COM_AnagraficaSoggetti.FirstOrDefault(c => c.IDSoggetto == iDSoggetto);
                            string azienda = soggetto.NomeAzienda + " " + soggetto.IndirizzoSoggetto;

                            DateTime dt = DateTime.Parse(txtDataVersamento.Text);
                            decimal QuantitaBollini;
                            QuantitaBollini = Math.Floor(importo / CostoBollino);
                            quantita = int.Parse(QuantitaBollini.ToString());

                            var rp = portafoglio.Incasso(azienda, DateTime.Now, importo, quantita, dt);
                            portafoglio.Save();

                            UtilityBollini.AcquisisciBollini(iDSoggetto, rp.IDMovimento);
                            Portafoglio.AggiornaCreditoResiduo(rp.IDMovimento);
                            EmailNotify.SendRicevutaPagamentoAmministrazione(rp.IDMovimento);
                            RedirectPage(iDSoggetto.ToString(), rp.IDMovimento.ToString());
                        }
                    }
                }
            }
        }

    }

}
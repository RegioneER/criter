using System;
using System.Linq;
using System.Web;
using DataUtilityCore;
using PayerLib;
using DataLayer;
using DataUtilityCore.Portafoglio;
using System.Configuration;
using System.Globalization;
using DevExpress.Web;
using System.Web.UI.WebControls;

public partial class MNG_PagamentoPayER : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "MNG_PagamentoPayER.aspx");
        if (!this.Page.IsPostBack)
        {
            PagePermission();
            SetInfoDefaultPayer();
            GetImportoRicarica();
            ASPxComboBox1.Focus();
            txtQtaBollini.Focus();
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

    public void SetInfoDefaultPayer()
    {
        decimal importo = 0;
        if (string.IsNullOrEmpty(lblImporto.Text) && decimal.TryParse(lblImporto.Text, out importo))
        {
            lblImporto.Text = String.Format("{0:0.00}", importo);
        }

        this.pnlPayerServizioAttivo.Visible = UtilityConfig.PayerAbilitato;
        this.pnlPayerServizioSospeso.Visible = !UtilityConfig.PayerAbilitato;
    }

    protected void txtQtaBollini_TextChanged(object sender, EventArgs e)
    {
        GetImportoRicarica();
    }

    public void GetImportoRicarica()
    {
        decimal CostoBollino;
        decimal QuantitaBollini;
        decimal importo = 0;
        decimal.TryParse(ConfigurationManager.AppSettings["CostoBollino"], NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out CostoBollino);
        decimal.TryParse(txtQtaBollini.Text, out QuantitaBollini);

        if (decimal.TryParse(txtQtaBollini.Text, out QuantitaBollini))
        {
            importo = (QuantitaBollini * CostoBollino); //Math.Floor(QuantitaBollini * CostoBollino);
            lblImporto.Text = String.Format("{0:0.00}", importo);
        }
        else
        {
            lblImporto.Text = String.Format("{0:0.00}", importo);
        }
    }

    public void ControllaQuantitaBollini(Object sender, ServerValidateEventArgs e)
    {
        int bollini;
        bool chkbollini = int.TryParse(txtQtaBollini.Text, out bollini);

        if (chkbollini)
        {
            if (bollini >= 1 && bollini <= 857) //Limite PayER
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
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
    
    protected void btnPayPayer_Click(Object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            decimal importo = 0;
            int quantita = 0;
            if (UtilityConfig.PayerAbilitato && int.TryParse(this.txtQtaBollini.Text, out quantita) && decimal.TryParse(this.lblImporto.Text, out importo))
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
                            if (soggetto != null && soggetto.Email != null)
                            {
                                string azienda = soggetto.NomeAzienda + " " + soggetto.IndirizzoSoggetto;

                                //all'inizio avrò un esito null, verrà poi aggiornato dalla pagina di notifica
                                var dbPaymentRequest = portafoglio.IncassoPayer(azienda, DateTime.Now, importo, quantita);
                                portafoglio.Save();

                                //ora avrò il numero documento popolato, sarebbe dbPaymentRequest.IdPaymentRequest
                                PayerPaymentRequest payerPaymentRequest = new PayerPaymentRequest(new PayerConfig());
                                payerPaymentRequest.AnnoDocumento = dbPaymentRequest.DataOraInserimento.Year.ToString();
                                payerPaymentRequest.EmailUtente = soggetto.Email.Trim();
                                payerPaymentRequest.IdentificativoUtente = soggetto.PartitaIVA.Trim();
                                //if (!string.IsNullOrEmpty(soggetto.CodiceFiscale.Trim()))
                                //{
                                //    payerPaymentRequest.IdentificativoUtente = soggetto.CodiceFiscale.Trim();
                                //}
                                //else
                                //{
                                    
                                //}
                                payerPaymentRequest.Importo = importo;
                                payerPaymentRequest.NumeroOperazione = dbPaymentRequest.NumeroOperazione.ToString().ToUpper(); // 1.ToString("0000000000000");

                                //Prefisso per ambiente di test, altrimenti ogni volta abbiamo degli ID che sono uguali

                                if (!string.IsNullOrWhiteSpace(UtilityConfig.PayerPrefissoNumeroDocumento))
                                {
                                    payerPaymentRequest.NumeroDocumento =
                                      string.Format("{0}{1}-{2}", UtilityConfig.PayerPrefissoNumeroDocumento, soggetto.CodiceSoggetto,
                                          dbPaymentRequest.idPaymentRequest.ToString()
                                              .PadLeft(19 - soggetto.CodiceSoggetto.Length - UtilityConfig.PayerPrefissoNumeroDocumento.Length, '0')); //MAX 20 char!! 
                                }
                                else
                                {
                                    payerPaymentRequest.NumeroDocumento =
                                        string.Format("{0}-{1}", soggetto.CodiceSoggetto,
                                            dbPaymentRequest.idPaymentRequest.ToString()
                                                .PadLeft(19 - soggetto.CodiceSoggetto.Length, '0')); //MAX 20 char!! 
                                }

                                //SALVO SUL DB il numeroDocumento!!
                                //la rileggo agganciata a questo contesto per impostare il numero documento
                                dbPaymentRequest = ctx.COM_PayerPaymentRequest.Find(dbPaymentRequest.idPaymentRequest);
                                dbPaymentRequest.NumeroDocumento = payerPaymentRequest.NumeroDocumento;

                                ctx.SaveChanges();

                                string urlPagamento = PayerUtil.SendPaymentRequest(payerPaymentRequest);
                                Response.Redirect(urlPagamento);
                            }

                        }
                        // portafoglio.Incasso(this.LoggedUtente, DateTime.Now, this.qta, "IUV-000", StatoTransazionePayER.OK);
                        //portafoglio.Save();

                        //this.txtQta.Text = "0,00";
                        //Response.Redirect("~/MNG_Portafoglio.aspx");
                    }
                }
            }
        }
    }

}
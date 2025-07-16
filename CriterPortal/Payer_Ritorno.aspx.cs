using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataUtilityCore;
using PayerLib;
using DataUtilityCore.Portafoglio;
using DataLayer;

public partial class Payer_Ritorno : Page
{
    private const int NumeroMassimoRefresh = 100;

    public int NumeroTentativi
    {
        get
        {
            if (ViewState["NumeroTentativi"] != null)
                return Convert.ToInt32(ViewState["NumeroTentativi"]);
            return 0;

        }

        set { ViewState["NumeroTentativi"] = value; }
    }

    public Guid NumeroOperazione
    {
        get
        {
            if (ViewState["NumeroOperazione"] != null)
                return (Guid )ViewState["NumeroOperazione"];
            return Guid.Empty;

        }

        set { ViewState["NumeroOperazione"] = value; }
    }
        
    protected void Page_Load(object sender, EventArgs e)
    {
        MostraEsito();
        NumeroTentativi++;
       
        if (NumeroTentativi == NumeroMassimoRefresh)
        {
            Timer1.Enabled = false;
        }
    }

    private void MostraEsito()
    {
        lblEsitoDescrizione.Text = "Errore durante l'elaborazione. Ripartire dalla pagina iniziale.";
        //verrà sovrascritto
        imagesDiv.Visible = false;

        var pID = Request["buffer"];

        //Se avevo già il numero operazione non rifaccio la richiesta a Payer per ottimizzare
        if (NumeroOperazione == Guid.Empty && !string.IsNullOrWhiteSpace(pID))
        {
            //leggo il payment data
            var paymentData = PayerUtil.ElaboraNotificaPagamento(new PayerConfig(), pID);

            if (paymentData != null)
            {
                NumeroOperazione = Guid.Parse(paymentData.NumeroOperazione);
                //verifico se nel frattempo sono arrivate delle notifiche al S2S per capire lo stato di questa operazione
                try
                {
                    if (paymentData != null)
                    {
                        //salvo su db il payment data ricevuto ed eventualmente aggiorna lo stato della Payment request relativa
                        COM_PayerPaymentData dbPaymentData = Portafoglio.SalvaPaymentDataSuDb(paymentData);
                        var commitMsg = PayerUtil.GetCommitMsg(new PayerConfig(), paymentData);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                    EmailNotify.SendApplicationError();
                }
            }
            else
            {
                //Qualcosa è andato storto...  fermo il timer
                Timer1.Enabled = false;
            }
        }
       
        if (NumeroOperazione != Guid.Empty)
        {
            //Vedo sul db come è messa... se è arrivata la chiamata S2S
            var paymentRequest = Portafoglio.GetDbPaymentRequest(NumeroOperazione);

            if (paymentRequest != null)
            {
                //verifico se la paymentRequest era di questo utente, per maggiore sicurezza
                if (!string.IsNullOrEmpty(paymentRequest.IdSoggetto.ToString()))
                {
                    var esito = PayerUtil.EsitoToEnum(paymentRequest.Esito);
                    string url = "MNG_Portafoglio.aspx";

                    switch (esito)
                    {
                        case PayerEsitoTransazione.OK:
                            lblEsitoDescrizione.Text =
                                "<b>Operazione completata</b>. Pagamento effettuato con successo.";
                            Timer1.Enabled = false; //STOPPO IL TIMER
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + url + "';},4000);", true);
                            break;
                        case PayerEsitoTransazione.KO:
                            lblEsitoDescrizione.Text =
                                "<b>Operazione fallita</b>. Probabile autorizzazione negata dal circuito";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + url + "';},4000);", true);
                            break;
                        case PayerEsitoTransazione.OP:
                            lblEsitoDescrizione.Text =
                                "<b>Operazione in corso</b>. Si prega di attendere e non ricaricare o chiudere la pagina.";
                            imagesDiv.Visible = true;
                            break;
                        case PayerEsitoTransazione.UK:
                            lblEsitoDescrizione.Text = "<b>Operazione fallita</b>. Errore sconosciuto.";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + url + "';},4000);", true);
                            break;
                        case null:
                            lblEsitoDescrizione.Text =
                                "<b>Operazione in corso</b>. Si prega di attendere e non ricaricare o chiudere la pagina.";
                            imagesDiv.Visible = true;
                            break;
                    }
                }
            }
            else
            {
                //Qualcosa è andato storto...  fermo il timer
                Timer1.Enabled = false;
            }
        }
    }
    
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        MostraEsito();
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataUtilityCore;
using PayerLib;
using DataUtilityCore.Portafoglio;
using DataLayer;

public partial class Payer_Notifica :  Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var pID = Request["buffer"];

            if (!string.IsNullOrWhiteSpace(pID))
            {
                var paymentData = PayerUtil.ElaboraNotificaPagamento(new PayerConfig(), pID);

                if (paymentData != null)
                {
                    //salvo su db il payment data ricevuto ed eventualmente aggiorna lo stato della Payment request relativa
                    COM_PayerPaymentData dbPaymentData = Portafoglio.SalvaPaymentDataSuDb(paymentData);

                    var commitMsg = PayerUtil.GetCommitMsg(new PayerConfig(), paymentData);

                   
                    //se è andato tutto ok, mostro il commit msg
                    Response.Clear();
                    Response.Write(commitMsg);
                    //Response.Flush(); //Lasciamo stare il Flush che genere un chunked e su LBL della Regione non funziona
                    //Response.Close();


                }
            }
        }
        catch (Exception ex)
        {
            EmailNotify.SendApplicationError();
        }
    }


  
}
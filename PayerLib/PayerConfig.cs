using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayerLib
{
    //Le URL per le chiamate server-to-server e per il redirect del browser utente sono le seguenti:
    //https://securepayertest.lepida.it/payer/cart/extS2SRID.do
    //https://securepayertest.lepida.it/payer/cart/extS2SPID.do
    //https://payertest.lepida.it/payer/cart/extCart.do

    //Questi sono i dati parametrici con cui configurare il servizio:
    //CodiceUtente: 00RER
    //CodiceEnte: 99988
    //TipoUfficio:
    //CodiceUfficio:
    //TipologiaServizio: SAC
    //PortaleID: SACE
    //IV(chiave primaria per la generazione dell'hash): 87784325
    //Key (chiave secondaria per la generazione dell'hash): 187654921423456289674321

    public class PayerConfig
    {
        public string CodiceUtente { get; set; }
        public string CodiceEnte { get; set; }
        public string TipoUfficio { get; set; }
        public string CodiceUfficio { get; set; }
        public string TipologiaServizio { get; set; }
        public string PortaleID { get; set; }

        /// <summary>
        /// Chiave primaria per la generazione dell'hash
        /// </summary>
        public string IV { get; set; }

        /// <summary>
        /// Chiave secondaria per la generazione dell'hash
        /// </summary>
        public string Key { get; set; }
        public string UrlExtS2SRID { get; set; }
        public string UrlExtS2SPID { get; set; }
        public string UrlExtCart { get; set; }

        public string UrlDiRitorno { get; set; }
        public string UrlDiNotifica { get; set; }
        public string UrlBack { get; set; }
        public string CommitNotifica { get; set; }
        
        public PayerConfig()
        {
            //TODO leggerà i parametri da qualche parte
            CodiceUtente = ConfigurationManager.AppSettings["PayerCodiceUtente"]; //"00RER";

            CodiceEnte = ConfigurationManager.AppSettings["PayerCodiceEnte"]; //"99988";
            TipoUfficio = null;
            CodiceUfficio = null;
            TipologiaServizio = ConfigurationManager.AppSettings["PayerTipologiaServizio"]; //"SAC";

            PortaleID = ConfigurationManager.AppSettings["PayerPortaleID"]; //"SACE";
            IV = ConfigurationManager.AppSettings["PayerIV"]; //"87784325";
            Key = ConfigurationManager.AppSettings["PayerKey"]; //"187654921423456289674321";

            UrlExtS2SRID = ConfigurationManager.AppSettings["PayerUrlExtS2SRID"]; // "https://securepayertest.lepida.it/payer/cart/extS2SRID.do";
            UrlExtS2SPID = ConfigurationManager.AppSettings["PayerUrlExtS2SPID"];  //"https://securepayertest.lepida.it/payer/cart/extS2SPID.do";
            UrlExtCart = ConfigurationManager.AppSettings["PayerUrlExtCart"];  //"https://payertest.lepida.it/payer/cart/extCart.do";


            UrlDiRitorno = ConfigurationManager.AppSettings["PayerUrlDiRitorno"]; //"https://securepayertest.lepida.it/payer/cart/extS2SRID.do";
            UrlDiNotifica = ConfigurationManager.AppSettings["PayerUrlDiNotifica"]; //"https://securepayertest.lepida.it/payer/cart/extS2SPID.do";
            UrlBack = ConfigurationManager.AppSettings["PayerUrlBack"]; //"https://payertest.lepida.it/payer/cart/extCart.do";

            CommitNotifica = ConfigurationManager.AppSettings["PayerCommitNotifica"]; //S o N
            if (string.IsNullOrWhiteSpace(CommitNotifica))
                CommitNotifica = "S"; //default S se non ho specificato parametri
        }
    }


}

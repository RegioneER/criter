using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayerLib
{
    public class PayerPaymentRequest
    {

        public string EmailUtente { get; set; }
        public string IdentificativoUtente { get; set; }
        public string NumeroOperazione { get; set; }
        public string NumeroDocumento { get; set; }
        public string AnnoDocumento { get; set; }
        public string Valuta { get; set; }
        public decimal Importo { get; set; }

        public PayerConfig Config { get; private set; }

        public PayerPaymentRequest(PayerConfig config)
        {
            this.Config = config;
            Valuta = "EUR"; //valuta default
        }

        public string ToXml()
        {
            StringBuilder sb  = new StringBuilder();
            sb.Append("<PaymentRequest>");
            sb.AppendFormat("<PortaleID>{0}</PortaleID>", Config.PortaleID);
            sb.Append("<Funzione>PAGAMENTO</Funzione>");
            sb.AppendFormat("<URLDiRitorno>{0}</URLDiRitorno>", Config.UrlDiRitorno);
            sb.AppendFormat("<URLDiNotifica>{0}</URLDiNotifica>", Config.UrlDiNotifica);
            sb.AppendFormat("<URLBack>{0}</URLBack>", Config.UrlBack);
            sb.AppendFormat("<CommitNotifica>{0}</CommitNotifica>", Config.CommitNotifica);
            //sb.Append("<CommitNotifica>N</CommitNotifica>");
            //sb.Append("<CommitNotifica>S</CommitNotifica>");
            ///TODO qui ho disabilitato il commit notifica, sa mettere a S una volta che funzionerà per bene sui server della regione

            sb.Append("<UserData>");
            sb.AppendFormat("<EmailUtente>{0}</EmailUtente>", EmailUtente);
            sb.AppendFormat("<IdentificativoUtente>{0}</IdentificativoUtente>", IdentificativoUtente);
            sb.Append("</UserData>");

            sb.Append("<ServiceData>");
            sb.AppendFormat("<CodiceUtente>{0}</CodiceUtente>", Config.CodiceUtente);
            sb.AppendFormat("<CodiceEnte>{0}</CodiceEnte>", Config.CodiceEnte);
            sb.AppendFormat("<TipoUfficio>{0}</TipoUfficio>", Config.TipoUfficio);
            sb.AppendFormat("<CodiceUfficio>{0}</CodiceUfficio>", Config.CodiceUfficio);
            sb.AppendFormat("<TipologiaServizio>{0}</TipologiaServizio>", Config.TipologiaServizio);
            sb.AppendFormat("<NumeroOperazione>{0}</NumeroOperazione>", NumeroOperazione);
            sb.AppendFormat("<NumeroDocumento>{0}</NumeroDocumento>", NumeroDocumento);
            sb.AppendFormat("<AnnoDocumento>{0}</AnnoDocumento>",AnnoDocumento);
            sb.AppendFormat("<Valuta>{0}</Valuta>", Valuta);
            //Attenzione: importo espresso in centesimi!!
            sb.AppendFormat("<Importo>{0}</Importo>", (Importo * 100).ToString("0", CultureInfo.InvariantCulture));
            sb.Append("<DatiSpecifici />");
            sb.Append("</ServiceData>");
           
            sb.Append("</PaymentRequest>");

            return sb.ToString();
        }
    }
}

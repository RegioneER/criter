using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using com.seda.payer.ext;
using com.sun.org.apache.xml.@internal.serialize;

namespace PayerLib
{
    public static class PayerUtil
    {
        public static T ParseEnum<T>(string value)
        {
            return (T) Enum.Parse(typeof(T), value, true);
        }

        public static string SendPaymentRequest(PayerPaymentRequest request)
        {

            var BufferData = request.ToXml();

            Client payerClient = new Client(request.Config.IV, request.Config.Key, request.Config.PortaleID);

            String bufferPaymentRequest = payerClient.getBufferPaymentRequest(BufferData);


            Debug.WriteLine(bufferPaymentRequest);

            using (var webClient = new WebClient())
            {
                //webClient.Headers.Add("user-agent", "Only a test!");
                var values = new NameValueCollection();
                values["buffer"] = bufferPaymentRequest;

                var response = webClient.UploadValues(request.Config.UrlExtS2SRID, values);
                var responseString = Encoding.Default.GetString(response);
                string bufferRID = payerClient.getBufferRID(responseString);

                var bufferRidEncoded = System.Web.HttpUtility.UrlEncode(bufferRID);
                string urlPagamento = string.Format("{0}?buffer={1}", request.Config.UrlExtCart, bufferRidEncoded);

                return urlPagamento;
            }
        }
        public static void ElaboraNotificaPagamentoTest()
        {
            PayerConfig payerConfig = new PayerConfig();

            Client payerClient = new Client(payerConfig.IV, payerConfig.Key, payerConfig.PortaleID);
            var responseString = "<Buffer><TagOrario>201603141019</TagOrario><CodicePortale>SACE</CodicePortale><BufferDati>PFBheW1lbnREYXRhPjxQb3J0YWxlSUQ+U0FDRTwvUG9ydGFsZUlEPjxOdW1lcm9PcGVyYXppb25lPkIzMTJDMDlBLTg4OEEtNEY4Qi05RjQ5LTZCNDQwNzUwQzc5NjwvTnVtZXJvT3BlcmF6aW9uZT48SURPcmRpbmU+ZWRhNjYwNzgtYWNhYS00OTNkLWFkNjgtNDkwMWVmMmEwN2Q4PC9JRE9yZGluZT48RGF0YU9yYU9yZGluZT4yMDE2MDMxNDEwMTUxMTwvRGF0YU9yYU9yZGluZT48SURUcmFuc2F6aW9uZT4xNDU3OTQ3MDAyNzE5MDAxMTE8L0lEVHJhbnNhemlvbmU+PERhdGFPcmFUcmFuc2F6aW9uZT4yMDE2MDMxNDEwMTcyNjwvRGF0YU9yYVRyYW5zYXppb25lPjxTaXN0ZW1hUGFnYW1lbnRvPlBZPC9TaXN0ZW1hUGFnYW1lbnRvPjxTaXN0ZW1hUGFnYW1lbnRvRD5QYXlFUjwvU2lzdGVtYVBhZ2FtZW50b0Q+PENpcmN1aXRvQXV0b3JpenphdGl2bz5OTlBBPC9DaXJjdWl0b0F1dG9yaXp6YXRpdm8+PENpcmN1aXRvQXV0b3JpenphdGl2b0Q+IE5vZG8gTmF6aW9uYWxlIFBhZ2FtZW50aTwvQ2lyY3VpdG9BdXRvcml6emF0aXZvRD48SW1wb3J0b1RyYW5zYXRvPjIxMjwvSW1wb3J0b1RyYW5zYXRvPjxJbXBvcnRvQ29tbWlzc2lvbmk+MDwvSW1wb3J0b0NvbW1pc3Npb25pPjxJbXBvcnRvQ29tbWlzc2lvbmlFbnRlPjA8L0ltcG9ydG9Db21taXNzaW9uaUVudGU+PEVzaXRvPk9LPC9Fc2l0bz48RXNpdG9EPlN1Y2Nlc3NvIG5lbGwnb3BlcmF6aW9uZTwvRXNpdG9EPjxBdXRvcml6emF6aW9uZT5JbXJlZDM4bGI4bXV2NW0yOTg2OGk1MDY4dzlpczlhbmw8L0F1dG9yaXp6YXppb25lPjxEYXRpU3BlY2lmaWNpLz48L1BheW1lbnREYXRhPg==</BufferDati><Hash>b2fe75dfb4e5db371bb98ad33976ec12</Hash></Buffer>";
            int WINDOW_MINUTES = 10; //parametrizzare
            string paymentDataStr = payerClient.getPaymentData(responseString, WINDOW_MINUTES);
            Debug.WriteLine(paymentDataStr);
            var paymentData = GetPaymentData(paymentDataStr);
        }

        public static PayerPaymentData ElaboraNotificaPagamento(PayerConfig payerConfig, string pID)
        {
            //Questo metodo deve essere chiamato quando la nostra pagina riceve chiamata di notifica da Payer
            Client payerClient = new Client(payerConfig.IV, payerConfig.Key, payerConfig.PortaleID);

            string bufferPID = payerClient.getBufferPID(pID);

            using (var webClient = new WebClient())
            {
                var values = new NameValueCollection();
                values["buffer"] = bufferPID;

                var response = webClient.UploadValues(payerConfig.UrlExtS2SPID, values);
                var responseString = Encoding.Default.GetString(response);

                if (!string.IsNullOrEmpty(responseString))
                {
                    int WINDOW_MINUTES = 10; //parametrizzare


                    string paymentDataStr = payerClient.getPaymentData(responseString, WINDOW_MINUTES);


                    var paymentData = GetPaymentData(paymentDataStr);


                    return paymentData;
                }
            }

            return null;
        }

        public static string GetCommitMsg(PayerConfig payerConfig, PayerPaymentData paymentData)
        {

            //invio la richiesta CommitMsg per avvisare payer che ho ricevuto la notifica
            var commitMsg = new PayerCommitMsg();
            commitMsg.PortaleID = payerConfig.PortaleID;
            commitMsg.IDOrdine = paymentData.IDOrdine;
            commitMsg.Commit = "OK";
            commitMsg.NumeroOperazione = paymentData.NumeroOperazione;

            return commitMsg.ToXml();

        }

        public static PayerPaymentData GetPaymentData(string paymentDataStr)
        {
            //TODO verificare 
            XmlSerializer serializer = new XmlSerializer(typeof(PayerPaymentData));

            using (TextReader reader = new StringReader(paymentDataStr))
            {
                return (PayerPaymentData) serializer.Deserialize(reader);
            }

        }

        public static Guid? ConvertiGuid(string s)
        {
            Guid d;

            if (Guid.TryParse(s, out d))
            {
                return d;
            }
            return null;
        }

        public static DateTime? ConvertiData(string s)
        {
            DateTime d;

            if (DateTime.TryParseExact(s, "yyyyMMddHHmmss",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
            {
                return d;
            }
            return null;
        }

        public static decimal? ConvertiImporto(string importoStr)
        {
            if (!string.IsNullOrEmpty(importoStr))
            {
                decimal value;
                if (decimal.TryParse(importoStr, out value))
                {
                    return value / 100; //su Payer l'importo è in centesimi, invece noi usiamo i decimali sul db
                }
            }
            return null;
        }

        public static string EsitoToString(PayerEsitoTransazione? esito)
        {
            if (esito == null)
                return "Operazione non completata";

            switch (esito)
            {
                case PayerEsitoTransazione.OK:
                    return "Operazione completata";
                    break;
                case PayerEsitoTransazione.KO:
                    return "Operazione fallita";
                    break;
                case PayerEsitoTransazione.OP:
                    return "In attesa di riscontro";
                    break;
                case PayerEsitoTransazione.UK:
                    return "Esito sconosciuto";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static string EsitoToString(short? esito)
        {
            return EsitoToString(EsitoToEnum(esito));
        }

        public static PayerEsitoTransazione? EsitoToEnum(short? esito)
        {
            PayerEsitoTransazione? esitoEnum = (PayerEsitoTransazione?) esito;
            return esitoEnum;
        }
    }
}

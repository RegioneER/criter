using SiMoka.MokaWS;
using System;
using System.Configuration;
using System.ServiceModel;

namespace SiMoka
{
    public static class MokaUtil
    {
        public class MokaRequestResult
        {
            //public bool RequestExecuted { get; set; }
            public bool RequestSucceded { get; set; }
            public bool? DatiCatastaliValidi { get; set; }

            public string Comune { get; set; }
            public string Via { get; set; }
            public string Provincia { get; set; }
            public string Civico { get; set; }
            public string Nome { get; set; }
        }


        public static ConsultazioneUIUWebServiceClient GetConsultazioneUIUWebServiceClient()
        {
          
              var binding =  new BasicHttpsBinding();
              MokaWS.ConsultazioneUIUWebServiceClient client = new ConsultazioneUIUWebServiceClient(
                  binding, new EndpointAddress(MokaConfig.SigmaterWsUrlConsultazioneUIU));
                return client;
        }

        private static bool IsEnabled
        {
            get { return ConfigurationManager.AppSettings["MokaEnabled"].Equals("on"); }
        }

        public static MokaRequestResult ParametriCatastaliValidi(string codiceCatastale, string sezione, string foglio, string particella, string sub)
        {
            MokaRequestResult result = new MokaRequestResult();

            try
            {
                if (IsEnabled)
                {
                    var client = GetConsultazioneUIUWebServiceClient();
                    eseguiRicercaUIU ricerca = new eseguiRicercaUIU();
                    eseguiRicercaUIURequest request = new MokaWS.eseguiRicercaUIURequest(ricerca);

                    comune com = new comune();
                    ricercaPerIdCat cat = new ricercaPerIdCat();
                    ricerca.username = MokaConfig.SigmaterWsUsername;
                    ricerca.password = MokaConfig.SigmaterWsPassword;
                    cat.comune = com;
                    com.codCom = codiceCatastale;
                    cat.sezioneUrbana = sezione;
                    cat.foglio = foglio;
                    cat.numero = particella;
                    cat.subalterno = sub;
                    ricerca.ricercaPerIdCat = cat;
                    eseguiRicercaUIUResponse response = client.eseguiRicercaUIU(ricerca);
                    SiMoka.MokaWS.uiu[] risposta = response.@return.risultatoRicerca;

                    if (risposta != null)
                    {
                        result.RequestSucceded = true;
                        if (risposta[0].indirizziCatastali != null)
                        {
                            //result.RequestExecuted = true;
                            result.DatiCatastaliValidi = true;
                            result.RequestSucceded = true;result.Comune = risposta[0].nomeCom;
                            result.Via = risposta[0].indirizziCatastali[0].descriz1;
                            result.Civico = risposta[0].indirizziCatastali[0].civicoCat;
                            result.Nome = risposta[0].indirizziCatastali[0].dizione;
                            result.Provincia = risposta[0].siglaProv;
                        }
                        else
                        {
                            result.DatiCatastaliValidi = false;
                        }
                    }
                    else
                    {
                        //Dati catastali non presenti su Sigmater
                        result.RequestSucceded = true;
                        result.DatiCatastaliValidi = false;
                    }
                }
                else
                {
                    result.RequestSucceded = false;
                    result.DatiCatastaliValidi = true;
                    
                }
            }
            catch (Exception ex)
            {
                //se la chiamata al WS va in eccezione bisogna che l'utente vada avanti lo stesso
                result.RequestSucceded = false;
                result.DatiCatastaliValidi = true;
            }

            return result;
        }
    }
}

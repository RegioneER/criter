using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Xml.Serialization;
using System.IO;
using Pomiager.Service.Parer.ResponseDto;

namespace Pomiager.Service.Parer
{
    public class ParerService
    {
        // Riferimenti pagina 72 SpecificheServiziVersamentoUD_v3.1.pdf
        // Riferimenti pagina 123 SpecificheServiziVersamentoUD_v3.1.pdf

        //Portale Web Parer 
        //https://parer-pre.regione.emilia-romagna.it/sacer 
        //CREDENZIALI:
        //UserID: francesco.terranova
        //Password: yfXnx81b

        public static async Task<Tuple<bool, string, string>> SendToParer(dtoSendParameters parametersPec)
        {
            bool isSend = false;
            string response = string.Empty;
            string request = string.Empty;

            var dtoPec = ParerSip.GetUnitaDocumentaria(parametersPec);
            var xmlPec = UtilityXml.GetXml(dtoPec);

            #region Client Parer
            try
            {
                using (var client = new HttpClient() { BaseAddress = new Uri(ParerConfig.ParerEndPoint) })
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3; // SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 | SecurityProtocolType.Ssl3;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                    
                    var uriBuilder = new UriBuilder(client.BaseAddress);
                    uriBuilder.Path = "/sacerws/VersamentoSync";

                    using (var multiForm = new MultipartFormDataContent())
                    {
                        multiForm.Add(new StringContent(ParerConfig.ParerVersion), "VERSIONE");
                        multiForm.Add(new StringContent(ParerConfig.ParerUsername), "LOGINNAME");
                        multiForm.Add(new StringContent(ParerConfig.ParerPassword), "PASSWORD");
                        multiForm.Add(new StringContent(xmlPec), "XMLSIP");

                        //Documento Principale(è la pec deve essere sempre presente)
                        parametersPec.PecDocumentMain.StreamDocument.Position = 0;
                        var fileStreamDocumentMain = new StreamContent(parametersPec.PecDocumentMain.StreamDocument);
                        string mimeTypeDocumentMain = UtilityApp.GetMimeType(parametersPec.PecDocumentMain.FileName);
                        fileStreamDocumentMain.Headers.ContentType = new MediaTypeHeaderValue(mimeTypeDocumentMain);

                        multiForm.Add(fileStreamDocumentMain, parametersPec.PecDocumentMain.IDDocument, parametersPec.PecDocumentMain.FileName);

                        if (parametersPec.PecAttachments.Count > 0)
                        {
                            foreach (var attachment in parametersPec.PecAttachments)
                            {
                                attachment.StreamAttachment.Position = 0;
                                var fileStreamAttachment = new StreamContent(attachment.StreamAttachment);
                                string mimeTypeAttachment = UtilityApp.GetMimeType(attachment.FileName);
                                fileStreamAttachment.Headers.ContentType = new MediaTypeHeaderValue(mimeTypeAttachment);

                                multiForm.Add(fileStreamAttachment, attachment.IDAttachment, attachment.FileName);
                            }
                        }

                        var responseParer = await client.PostAsync(uriBuilder.ToString(), multiForm).ConfigureAwait(false);

                        if (responseParer.IsSuccessStatusCode)
                        {
                            response = await responseParer.Content.ReadAsStringAsync();
                            try
                            {
                                XmlSerializer serializer = new XmlSerializer(typeof(EsitoVersamento));
                                using (StringReader reader = new StringReader(response))
                                {
                                    var esito = (EsitoVersamento)serializer.Deserialize(reader);
                                    if (esito.EsitoGenerale.CodiceEsito != TypeEsitoEnum.NEGATIVO.ToString())
                                    {
                                        request = xmlPec;
                                        isSend = true;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                isSend = true;
                                request = xmlPec;
                                response = "PARER - Errore di deserializzazione response: " + ex.Message;                                
                            }
                        }
                        else
                        {
                            isSend = false;
                            request = xmlPec;
                            response = "PARER - " + ParerConfig.ParerSystemSender + " Errore di chiamata al servizio all'url " + ParerConfig.ParerEndPoint + ": " + responseParer.StatusCode;                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isSend = false;
                request = xmlPec;
                response = "PARER - " + ParerConfig.ParerSystemSender + " Errore di invio xml al servizio all'url " + ParerConfig.ParerEndPoint + ": " + ex.Message;                
            }
            #endregion

            var tupleResult = Tuple.Create(isSend, request, response);
            return tupleResult;
        }
    }
}

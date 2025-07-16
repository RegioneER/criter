using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace FirmaLib
{
    public static class FirmaCommon
    {

        public static WSFirma.beanDssCreaTransazioneVerifica AvviaTransazioneVerifica(byte[] contents)
        {
            return AvviaTransazioneVerifica(contents, ".pdf");
        }
        
        public static WSFirma.beanDssCreaTransazioneVerifica AvviaTransazioneVerifica(byte[] contents, string filenameExtension)
        {
            // Enumera i documenti selezionati
            // Se il documento esiste lo metto in lista (altrimenti ignoro)
            List<byte[]> b64Files = new List<byte[]>();
            List<String> filenames = new List<string>();

            // Avvia transazione di firma su interop.firma
            FirmaDSS firmaDss = FirmaDSS.NewInstance;
            long sessionId = firmaDss.GetEpochTime();
            var fileBytes = contents;
            var fileB64 = Encoding.ASCII.GetBytes(Convert.ToBase64String(fileBytes));
            b64Files.Add(fileB64);
            filenames.Add(sessionId + filenameExtension);

            var bean = firmaDss.AvviaTransazioneVerifica(sessionId, filenames.ToArray(), b64Files.ToArray());
            if (bean.returnCode != 0)
                throw new Exception(bean.returnMessage);
            return bean;
        }

        public static WSFirma.beanDssCreaTransazioneFirma AvviaTransazioneFirma(String filenameWithPath) {
            return AvviaTransazioneFirma(FirmaDSS.NewInstance.FileToByteArray(filenameWithPath), System.IO.Path.GetExtension(filenameWithPath));
        }

        public static WSFirma.beanDssCreaTransazioneFirma AvviaTransazioneFirma(byte[] contents)
        {
            return AvviaTransazioneFirma(contents, ".pdf");
        }

        public static WSFirma.beanDssCreaTransazioneFirma AvviaTransazioneFirma(byte[] contents, string filenameExtension){
            // Enumera i documenti selezionati
            // Se il documento esiste lo metto in lista (altrimenti ignoro)
            List<byte[]> b64Files = new List<byte[]>();
            List<String> filenames = new List<string>();

            // Avvia transazione di firma su interop.firma
            FirmaDSS firmaDss = FirmaDSS.NewInstance; 
            long sessionId = firmaDss.GetEpochTime();
            var fileBytes = contents;
            var fileB64 = Encoding.ASCII.GetBytes(Convert.ToBase64String(fileBytes));
            b64Files.Add(fileB64);
            filenames.Add(sessionId + filenameExtension);
            
            var bean = firmaDss.AvviaTransazione(sessionId, filenames.ToArray(), b64Files.ToArray());
            if (bean.returnCode != 0)
                throw new Exception(bean.returnMessage);
            return bean;
        }

        public static WSFirma.beanDssControllaTransazioneFirma AvviaControlloFileFirmato(byte[] contents)
        {
            // Enumera i documenti selezionati
            // Se il documento esiste lo metto in lista (altrimenti ignoro)

            // Avvia transazione di firma su interop.firma
            FirmaDSS firmaDss = FirmaDSS.NewInstance;
            long sessionId = firmaDss.GetEpochTime();
            var fileBytes = contents;
            var fileB64 = Encoding.ASCII.GetBytes(Convert.ToBase64String(fileBytes));

            var bean = firmaDss.ControllaFileFirmato(sessionId, fileB64);
            if (bean.returnCode != 0)
                throw new Exception(bean.returnMessage);
            return bean;
        }

        public static WSFirma.beanDssCompletaTransazioneFirma CompletaTransazioneFirma(Int64 sessionId) {
            // Completa transazione di firma su interop.firma
            // aggiornando lo stato db per ogni documento e caricando il file firmato su interop
            FirmaDSS firmaDss = FirmaDSS.NewInstance;
            WSFirma.beanDssCompletaTransazioneFirma bean = null;
            try
            {
                bean = firmaDss.RisultatoAvviaTransazione(sessionId);
                if (bean.returnCode == 0)
                {
                    return bean;
                }
                else
                {
                    throw new Exception(String.Format("Transazione di firma non completata, codice errore={0} messaggio errore={1}", bean.returnCode, bean.returnMessage)); // Questa non e' un eccezione. Serve solo per popup
                }
                
            }
            catch (Exception ex)
            {
                // Pulisce il Dss dai files
                firmaDss.CleanUpTransazione(sessionId);
                throw new Exception(String.Format("Messaggio:{0}", ex.Message));
            }        
        }

    }
}

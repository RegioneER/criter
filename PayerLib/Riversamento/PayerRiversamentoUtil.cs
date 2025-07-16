using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NLog;
using PayerLib.Riversamento.FlussoRiversamento;
using PayerLib.Riversamento.RichiestaPagamentoTelematico;

namespace PayerLib.Riversamento
{
    public static class PayerRiversamentoUtil
    {
        public const string PayerRiversamentoUtilLoggerKey = "PayerRiversamento";

        public static Logger logger = NLog.LogManager.GetLogger(PayerRiversamentoUtilLoggerKey);

        public static ctFlussoRiversamento LeggiFlussoRiversamento(string path)
        {
            logger.Info("LeggiFlussoRiversamento {0}", path);
            using (var reader = new XmlTextReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof (ctFlussoRiversamento));

                ctFlussoRiversamento xmlobj = (ctFlussoRiversamento) serializer.Deserialize(reader);

                return xmlobj;

            }

        }

        public static ctRicevutaTelematica LeggiRicevutaTelematica(string path)
        {
            logger.Info("LeggiRicevutaTelematica {0}", path);
            using (var reader = new XmlTextReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof (ctRicevutaTelematica));

                ctRicevutaTelematica xmlobj = (ctRicevutaTelematica) serializer.Deserialize(reader);

                return xmlobj;
            }
        }


        public static List<RecordVersamentoPayer> LeggiRecordVersamentoPayer(string path)
        {
            logger.Info("LeggiRecordVersamentoPayer {0}", path);
             List<RecordVersamentoPayer> list = new List<RecordVersamentoPayer>();

            var lines = File.ReadAllLines(path);
            //Tutte le linee corrispondo a un record tranne l'ultima che inizia per 4..che è il riepilogo
            for (int i = 0; i < lines.Length -1; i++)
            {
                var r = GetRecordVersamentoPayer(lines[i]);
                list.Add(r);
            }

            //TODO l'ultima va elaborata a parte per il riepilogo

            return list;

        }
        
        private static RecordVersamentoPayer GetRecordVersamentoPayer(string line)
        {
            RecordVersamentoPayer r = new RecordVersamentoPayer();
            r.CodiceFiscaleContribuente = line.Substring(173, 16);
            r.Importo = Convert.ToDecimal(line.Substring(36, 10)); 
            r.NumeroDocumento = line.Substring(151, 20);
            r.ProgressivoSelezione = Convert.ToInt32(line.Substring(8, 7));
            return r;
        }

        private static List<RecordRiferimentiRT> LeggiRecordRiferimentiRT(string path)
        {
            logger.Info("LeggiRecordRiferimentiRT {0}", path);
            List<RecordRiferimentiRT> list = new List<RecordRiferimentiRT>();

            var lines = File.ReadAllLines(path);
            //Tutte le linee corrispondo a un record tranne la prima che è l'header
            for (int i = 1; i < lines.Length ; i++)
            {
                var r = GetRecordRiferimentiRT(lines[i]);
                list.Add(r);
            }

            return list;
        }

        private static RecordRiferimentiRT GetRecordRiferimentiRT(string line)
        {
           RecordRiferimentiRT r = new RecordRiferimentiRT();
            var lineSplit = line.Split(';');
            r.IdRiga = Int32.Parse(lineSplit[0]);
            r.NomeFileRT = lineSplit[1].Replace("\"", String.Empty); //non mi interessano gli apici!
            return r;
        }

        /// <summary>
        /// Elabora la folder es. IVP00LEP00NNP9998820160407020
        /// </summary>
        /// <param name="folderPath"></param>
        public static List<RecordVersamentoPayer> ElaboraCartellaRendicontazionePayer(string folderPath)
        {
            logger.Info("ElaboraCartellaRendicontazionePayer {0}", folderPath);
            var folderName = new DirectoryInfo(folderPath).Name;
            //il file nella root ha lo stesso nome della folder
            var fileTxt = Path.Combine(folderPath, String.Format("{0}.txt", folderName));

            List<RecordVersamentoPayer> listaRecordFileTxt = LeggiRecordVersamentoPayer(fileTxt);

            //leggo il file csv
            var fileRiferimentiRTcsv = Path.Combine(folderPath, "PagoPA", "riferimenti_RT.csv");

            List<RecordRiferimentiRT> listaRecordRiferimentiRT = LeggiRecordRiferimentiRT(fileRiferimentiRTcsv);


            foreach (var recordVersamentoPayer in listaRecordFileTxt)
            {
                //trovo il riferimento nel csv
                RecordRiferimentiRT recordRiferimentiRt =
                    listaRecordRiferimentiRT.Where(c => c.IdRiga == recordVersamentoPayer.ProgressivoSelezione)
                    .FirstOrDefault();
                if (recordRiferimentiRt != null)
                {
                   
                        //trovo il suo file 
                        var fileRicevutaTelematicaPagoPA = Path.Combine(folderPath, "PagoPA",
                            recordRiferimentiRt.NomeFileRT);
                        try
                        {
                        //I primi 36 caratteri sono il guid
                        recordVersamentoPayer.IDOrdine =
                            PayerUtil.ConvertiGuid(recordRiferimentiRt.NomeFileRT.Substring(0, 36));


                        var ricevutaTelematicaPagoPA = LeggiRicevutaTelematica(fileRicevutaTelematicaPagoPA);

                        recordVersamentoPayer.RicevutaTelematicaPagoPA = ricevutaTelematicaPagoPA;

                        //devo salvare nel db lo IUV
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Eccezione durante elaborazione RecordRiferimentiRT riferito al ProgressivoSelezione: {0} File: {1}", recordVersamentoPayer.ProgressivoSelezione, recordRiferimentiRt.NomeFileRT);
                    }
                }
                else
                {
                    //TODO non dovrebbe succedere
                    logger.Warn("Non è stato trovato il RecordRiferimentiRT riferito al ProgressivoSelezione: {0}", recordVersamentoPayer.ProgressivoSelezione);
                }
            }

            return listaRecordFileTxt;
        }


        public static List<ctFlussoRiversamento> ElaboraCartellaFlussoRiversamentoPagoPA(string folderPath)
        {
            logger.Info("ElaboraCartellaFlussoRiversamentoPagoPA {0}", folderPath);
            //Ogni file XML dentro la folder è un flusso
            var xmlFiles = Directory.GetFiles(folderPath, "*.xml");
            List<ctFlussoRiversamento> list = new List<ctFlussoRiversamento>(xmlFiles.Length);
            foreach (var xmlFile in xmlFiles)
            {
                ctFlussoRiversamento flusso = LeggiFlussoRiversamento(Path.Combine(folderPath, xmlFile));
                list.Add(flusso);
            }

            return list;

        }

        public static DateTime TrovaDataDaFilePayerRendicontazione(string fileName)
        {
            //gli passo il percorso completo
            fileName = Path.GetFileName(fileName);
            //FILE NAME IVP00LEP00NNP9998820160415024.zip
            //La data è sempre in quel posto

            var dataStr = fileName.Substring(18, 8);

            return DateTime.ParseExact(dataStr, "yyyyMMdd", CultureInfo.InvariantCulture);

        }

        public static DateTime TrovaDataDaFilePagoPA(string fileName)
        {
            //gli passo il percorso completo
            fileName = Path.GetFileName(fileName);
            //e6e22fc9b46a315aa3039283bc77a9b3_IT47Q0200802480000002853795_20160408220004.zip
            //devo andare dopo il secondo underscore

            var secondoUnderscore = fileName.LastIndexOf("_", StringComparison.InvariantCulture);

            var dataStr = fileName.Substring(secondoUnderscore + 1, 8);

            return DateTime.ParseExact(dataStr, "yyyyMMdd", CultureInfo.InvariantCulture);
        }
    }

}

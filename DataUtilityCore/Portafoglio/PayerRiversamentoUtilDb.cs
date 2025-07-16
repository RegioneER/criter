using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using PayerLib;
using PayerLib.Riversamento;
using System.Threading;
using Ionic.Zip;
using PayerLib.Riversamento.FlussoRiversamento;
using DataLayer;
using stCodiceEsitoPagamento = PayerLib.Riversamento.RichiestaPagamentoTelematico.stCodiceEsitoPagamento;

namespace DataUtilityCore.Portafoglio
{
    public static class PayerRiversamentoUtilDb
    {
        public static NLog.Logger logger = NLog.LogManager.GetLogger(PayerRiversamentoUtil.PayerRiversamentoUtilLoggerKey);

        /// <summary>
        /// Elabora la folder es. IVP00LEP00NNP9998820160407020
        /// </summary>
        /// <param name="folderPath"></param>
        public static bool ElaboraCartellaRendicontazionePayer(string folderPath, List<string> errorList)
        {
            if (errorList == null)
                errorList = new List<string>();


            //Nel file ci sono solo record che hanno avuto esito positivo (forse)
            //Meglio controllare lo stato

            List<RecordVersamentoPayer> listaRecordVersamentoPayer = PayerRiversamentoUtil.ElaboraCartellaRendicontazionePayer(folderPath);


            //ora elaboro sul DB
            using (var ctx = new CriterDataModel())
            {
                foreach (var recordVersamentoPayer in listaRecordVersamentoPayer.Where(c => c.RicevutaTelematicaPagoPA != null ))
                {
                    if (recordVersamentoPayer.RicevutaTelematicaPagoPA.datiPagamento
                                           .identificativoUnivocoVersamento == "RF234023")
                        Debugger.Break();

                    //<xsd:element name="codiceEsitoPagamento" type="pay_i:stCodiceEsitoPagamento" minOccurs="1">
                    //    <xsd:annotation>
                    //      <xsd:documentation>Campo numerico indicante l’esito del pagamento.</xsd:documentation>
                    //      <xsd:documentation>Può assumere i seguenti valori:</xsd:documentation>
                    //      <xsd:documentation>0 - Pagamento eseguito</xsd:documentation>
                    //      <xsd:documentation>1 - Pagamento non eseguito</xsd:documentation>
                    //      <xsd:documentation>2 - Pagamento parzialmente  eseguito</xsd:documentation>
                    //      <xsd:documentation>3 - Decorrenza termini</xsd:documentation>
                    //      <xsd:documentation>4 - Decorrenza termini parziale</xsd:documentation>
                    //    </xsd:annotation>
                    //</xsd:element>

                    //Gestisco solo quello eseguito
                    if (recordVersamentoPayer.RicevutaTelematicaPagoPA.datiPagamento.codiceEsitoPagamento == stCodiceEsitoPagamento.Item0)
                    {

                        using (var dbContextTransaction = ctx.Database.BeginTransaction())
                        {
                            try
                            {
                                var inviaRicevuta = false;
                                logger.Info("Elaborazione NumeroDocumento: {0}", recordVersamentoPayer.NumeroDocumento);
                                //trovo il record sul db
                                var numeroDocumento = recordVersamentoPayer.NumeroDocumento;
                                //lo uso nella Where, quindi lo copio in locale per evitare problemi (in closure)
                                var dbPaymentRequest =
                                    ctx.COM_PayerPaymentRequest.Where(
                                        c => c.NumeroDocumento == numeroDocumento).FirstOrDefault();



                                if (dbPaymentRequest != null)
                                {
                                    //Per scrupolo controllo anche il codice fiscale
                                    if (
                                        dbPaymentRequest.COM_AnagraficaSoggetti.CodiceFiscale.ToUpperInvariant()
                                            .Trim() ==
                                        recordVersamentoPayer.CodiceFiscaleContribuente.ToUpperInvariant().Trim())
                                    {
                                       

                                        if (string.IsNullOrEmpty(dbPaymentRequest.IUV))
                                        {
                                            //Aggiorno lo IUV
                                            dbPaymentRequest.IUV =
                                                recordVersamentoPayer.RicevutaTelematicaPagoPA.datiPagamento
                                                    .identificativoUnivocoVersamento;
                                            logger.Info("NumeroDocumento: {0} - IUV aggiornato: {1}",
                                                recordVersamentoPayer.NumeroDocumento, dbPaymentRequest.IUV);
                                        }
                                        var vecchioEsito = dbPaymentRequest.Esito;

                                        //NON AGGIORNO l'esito NEGLI ALTRI CASI in cui l'esito è già stato definito
                                        //if (!dbPaymentRequest.Esito.HasValue || dbPaymentRequest.Esito.Value == (short) PayerEsitoTransazione.OP)
                                        //2016-04-28: cambio comunque l'esito, perchè abbiamo scoperto che in realtà un KO può non essere definitivo 
                                        if (!vecchioEsito.HasValue ||
                                            vecchioEsito.Value != (short) PayerEsitoTransazione.OK)
                                        {
                                            dbPaymentRequest.Esito = (short) PayerEsitoTransazione.OK;
                                            logger.Info("NumeroDocumento: {0} - Esito aggiornato: OK",
                                                recordVersamentoPayer.NumeroDocumento);


                                            var dbPaymentData = new COM_PayerPaymentData();
                                            dbPaymentData.DataRicezioneNotifica = DateTime.Now;
                                            //Questa è quella che ha anche delle lettere in genere..
                                            dbPaymentData.Autorizzazione =
                                                recordVersamentoPayer.RicevutaTelematicaPagoPA
                                                    .identificativoMessaggioRicevuta;
                                            dbPaymentData.CircuitoAutorizzativo = "NNPA"; //Nodo Pago PA

                                            dbPaymentData.DataOraOrdine =
                                                PayerUtil.ConvertiData(
                                                    recordVersamentoPayer.RicevutaTelematicaPagoPA
                                                        .riferimentoMessaggioRichiesta);

                                            dbPaymentData.DataOraTransazione =
                                                recordVersamentoPayer.RicevutaTelematicaPagoPA.dataOraMessaggioRicevuta;


                                            dbPaymentData.IDOrdine = recordVersamentoPayer.IDOrdine;
                                            //ce ne dovrebbe essere sempre uno
                                            var datiSingoloPagamento =
                                                recordVersamentoPayer.RicevutaTelematicaPagoPA.datiPagamento
                                                    .datiSingoloPagamento
                                                    .FirstOrDefault();
                                            if (datiSingoloPagamento != null)
                                                dbPaymentData.IDTransazione =
                                                    datiSingoloPagamento.identificativoUnivocoRiscossione;

                                            dbPaymentData.IdPaymentRequest = dbPaymentRequest.idPaymentRequest;
                                            dbPaymentData.ImportoCommissioni = 0; //default

                                            dbPaymentData.ImportoCommissioniEnte = 0;
                                            dbPaymentData.ImportoTransato = recordVersamentoPayer.Importo/100;
                                            //era in centesimi
                                            dbPaymentData.SistemaPagamento = "PY"; //fisso a Payer

                                            dbPaymentData.Esito = (short) PayerEsitoTransazione.OK;
                                            ctx.COM_PayerPaymentData.Add(dbPaymentData);

                                            //Dopo dovrà inviare la ricevuta, se l'esito precedente non era OK
                                            inviaRicevuta = true;
                                        }
                                    }
                                    else
                                    {
                                        //Codice fiscale non congruente

                                        logger.Error(
                                            "Codice fiscale non congruente durante l'elaborazione della payment request Id: {0}",
                                            dbPaymentRequest.idPaymentRequest);
                                        logger.Error(
                                            "CodiceFiscaleCalcolato: '{0}' -  recordVersamentoPayer.CodiceFiscaleContribuente: '{1}'",
                                            dbPaymentRequest.COM_AnagraficaSoggetti.CodiceFiscale
                                                .ToUpperInvariant(),
                                            recordVersamentoPayer.CodiceFiscaleContribuente.ToUpperInvariant());
                                        return false;
                                    }
                                }
                                else
                                {
                                    //Payment request non trovata
                                   logger.Error(
                                        "Payment request non trovata - recordVersamentoPayer.NumeroDocumento: {0}",
                                        recordVersamentoPayer.NumeroDocumento);
                                   continue; //elaboro comunque il file
                                }
                                ctx.SaveChanges();

                                dbContextTransaction.Commit();
                                logger.Info("NumeroDocumento: {0} - Transazione COMMITTED",
                                    recordVersamentoPayer.NumeroDocumento);

                                //in questo caso invio la ricevuta solo nel sistema online vero e non in test!
                                if (inviaRicevuta)
                                {
                                    logger.Info("NumeroDocumento: {0} - Invio Ricevuta",
                                        recordVersamentoPayer.NumeroDocumento);
                                    //dovrei avere al max una riga portafoglio!
                                    EmailNotify.SendRicevutaPagamentoAmministrazione(
                                        dbPaymentRequest.COM_RigaPortafoglio.First().IDMovimento);
                                }
                            }
                            catch (Exception ex)
                            {
                                dbContextTransaction.Rollback();
                                logger.Error(ex);
                                errorList.Add(ex.Message);
                                return false;
                            }
                        }

                    }
                }
            }

            return true;
        }


        public static bool ElaboraZipRendicontazionePayer(string zipFilePath, List<string> errorList )
        {
            if (errorList == null)
                errorList = new List<string>();

            logger.Info("-------- INIZIO ELABORAZIONE ZIP: {0}", zipFilePath);

            var fileInfo = new FileInfo(zipFilePath);
            var subDirectoryName = Path.GetFileNameWithoutExtension(zipFilePath);
            //la directory la chiamo come il file

            var subDirectoryInfo = new DirectoryInfo(Path.Combine(fileInfo.Directory.FullName, subDirectoryName));

            try
            {
                if (subDirectoryInfo.Exists)
                    subDirectoryInfo.Delete(true);
                subDirectoryInfo.Create();
                if (!Debugger.IsAttached)
                    Thread.Sleep(500); //Per l'antivirus che blocca i file do il tempo di fare la scansione
                //ZipFile.ExtractToDirectory(zipFilePath, subDirectoryInfo.FullName);
                using (ZipFile zip = ZipFile.Read(zipFilePath))
                {
                    zip.ExtractAll(subDirectoryInfo.FullName);
                }
                if (!Debugger.IsAttached)
                    Thread.Sleep(500); //Per l'antivirus che blocca i file do il tempo di fare la scansione

                bool ret = ElaboraCartellaRendicontazionePayer(subDirectoryInfo.FullName,  errorList);
                logger.Info("-------- FINE ELABORAZIONE ZIP: {0}", zipFilePath);
                return ret;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                errorList.Add(ex.Message);
                return false;
            }
            finally
            {
                subDirectoryInfo.Refresh();
                //Alla fine ripulisco 
                //SUI SERVER DELLA REGIONE FORSE DA PROBLEMI IL DELETE FINALE!
                if (subDirectoryInfo.Exists)
                    subDirectoryInfo.Delete(true);
            }
        }

        public static bool ElaboraZipFlussoRiversamentoPagoPA(string zipFilePath, List<string> errorList)
        {
            if (errorList == null)
                errorList = new List<string>();
            logger.Info("-------- INIZIO ELABORAZIONE ZIP: {0}", zipFilePath);
            var fileInfo = new FileInfo(zipFilePath);
            var subDirectoryName = Path.GetFileNameWithoutExtension(zipFilePath);
            //la directory la chiamo come il file

            var subDirectoryInfo = new DirectoryInfo(Path.Combine(fileInfo.Directory.FullName, subDirectoryName));

            try
            {
                if (subDirectoryInfo.Exists)
                    subDirectoryInfo.Delete(true);
                subDirectoryInfo.Create();

                if (!Debugger.IsAttached)
                    Thread.Sleep(500); //Per l'antivirus che blocca i file do il tempo di fare la scansione

                //ZipFile.ExtractToDirectory(zipFilePath, subDirectoryInfo.FullName);
                using (ZipFile zip = ZipFile.Read(zipFilePath))
                {
                    zip.ExtractAll(subDirectoryInfo.FullName);
                }
                if (!Debugger.IsAttached)
                    Thread.Sleep(500); //Per l'antivirus che blocca i file do il tempo di fare la scansione

                bool ret = ElaboraFlussoRiversamentoPagoPA(subDirectoryInfo.FullName, errorList);
                logger.Info("-------- INIZIO ELABORAZIONE ZIP: {0}", zipFilePath);
                return ret;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                errorList.Add(ex.Message);
                return false;
            }
            finally
            {
                subDirectoryInfo.Refresh();
                //Alla fine ripulisco 
                //SUI SERVER DELLA REGIONE FORSE DA PROBLEMI IL DELETE FINALE!
                if (subDirectoryInfo.Exists)
                    subDirectoryInfo.Delete(true);
            }
        }

        public static bool ElaboraFlussoRiversamentoPagoPA(string folderPath, List<string> errorList)
        {
            if (errorList == null)
                errorList = new List<string>();

            List<ctFlussoRiversamento> listaFlussiRiversamento = PayerRiversamentoUtil.ElaboraCartellaFlussoRiversamentoPagoPA(folderPath);

            //ora elaboro sul DB
            using (var ctx = new CriterDataModel())
            {
                foreach (var flussoRiversamento in listaFlussiRiversamento)
                {
                    using (var dbContextTransaction = ctx.Database.BeginTransaction())
                    {
                        try
                        {
                             logger.Info("Elaborazione Flusso Riversamento PagoPA: {0}", flussoRiversamento.identificativoFlusso);
                             logger.Info("Data Flusso Riversamento PagoPA: {0}", flussoRiversamento.dataOraFlusso.ToString("yyyy-MM-dd HH.mm.ss.fff"));
                            //trovo il record sul db
                            var identificativoFlusso = flussoRiversamento.identificativoFlusso;
                            //lo uso nella Where, quindi lo copio in locale per evitare problemi (in closure)
                            var dbFlussorendicontazione =
                                ctx.COM_PayerFlussoRiversamento.Where(
                                    c => c.identificativoFlusso == identificativoFlusso).FirstOrDefault();

                            if (dbFlussorendicontazione == null)
                            {
                                dbFlussorendicontazione = new COM_PayerFlussoRiversamento();
                                dbFlussorendicontazione.identificativoFlusso = identificativoFlusso;
                                ctx.COM_PayerFlussoRiversamento.Add(dbFlussorendicontazione);
                            
                               logger.Info("Flusso Rendicontazione CREATO - flussoRiversamento.identificativoFlusso: {0}", flussoRiversamento.identificativoFlusso);
                            }
                            else
                            {
                                 logger.Info("Flusso Rendicontazione trovato nel db - flussoRiversamento.identificativoFlusso: {0}", flussoRiversamento.identificativoFlusso);
                            }
                            dbFlussorendicontazione.numeroTotalePagamenti = Convert.ToInt32(flussoRiversamento.numeroTotalePagamenti);
                            dbFlussorendicontazione.importoTotalePagamenti = flussoRiversamento.importoTotalePagamenti;
                            //alla fine dovrò verificare se tornano i totali

                             dbFlussorendicontazione.dataOraFlusso = flussoRiversamento.dataOraFlusso;
                            dbFlussorendicontazione.dataRegolamento = flussoRiversamento.dataRegolamento;
                            dbFlussorendicontazione.identificativoUnivocoRegolamento = flussoRiversamento.identificativoUnivocoRegolamento;

                            dbFlussorendicontazione.codiceIdentificativoUnivocoMittente = flussoRiversamento.istitutoMittente.identificativoUnivocoMittente.codiceIdentificativoUnivoco;
                            dbFlussorendicontazione.codiceIdentificativoUnivocoRicevente = flussoRiversamento.istitutoRicevente.identificativoUnivocoRicevente.codiceIdentificativoUnivoco;
                            dbFlussorendicontazione.denominazioneMittente = flussoRiversamento.istitutoMittente.denominazioneMittente;
                            dbFlussorendicontazione.denominazioneRicevente = flussoRiversamento.istitutoRicevente.denominazioneRicevente;
                            dbFlussorendicontazione.tipoIdentificativoUnivocoMittente = flussoRiversamento.istitutoMittente.identificativoUnivocoMittente.tipoIdentificativoUnivoco.ToString();
                            dbFlussorendicontazione.tipoIdentificativoUnivocoRicevente = flussoRiversamento.istitutoRicevente.identificativoUnivocoRicevente.tipoIdentificativoUnivoco.ToString();


                            //Ora devo elaborare i pagamenti del flusso

                

                            foreach (var datoSingoliPagamenti in flussoRiversamento.datiSingoliPagamenti)
                            {
                                //devo trovare la payment request relativa
                                var identificativoUnivocoVersamento =
                                    datoSingoliPagamenti.identificativoUnivocoVersamento;
                                var identificativoUnivocoRiscossione =
                                    datoSingoliPagamenti.identificativoUnivocoRiscossione;
                                //verifica anche per id transazione
                                var dbPaymentRequestList =
                                    ctx.COM_PayerPaymentRequest.Where(c => c.IUV == identificativoUnivocoVersamento && c.COM_PayerPaymentData.Any(pd => pd.IDTransazione == identificativoUnivocoRiscossione)).ToArray();

                                //var dbPaymentRequestListCount = dbPaymentRequestList.Count();

                                if (dbPaymentRequestList.Length == 0)
                                {
                                    //Se non l'avevo trovata così, potrebbe essere perchè non c'è lo IUV... provo a identificarla comunque

                                    dbPaymentRequestList =
                                        ctx.COM_PayerPaymentRequest.Where(
                                            c =>
                                                c.COM_PayerPaymentData.Any(
                                                    pd =>
                                                        pd.IDTransazione == identificativoUnivocoRiscossione &&
                                                        DbFunctions.TruncateTime(pd.DataOraTransazione) ==
                                                        datoSingoliPagamenti.dataEsitoSingoloPagamento &&
                                                        pd.Esito == (short) PayerEsitoTransazione.OK) &&
                                                c.Importo == datoSingoliPagamenti.singoloImportoPagato).ToArray();

                                    if (dbPaymentRequestList.Length == 1) //se ne ho trovato una e solo una
                                    {
                                        //con questo metodo, aggiorno anche lo IUV che mancava
                                        dbPaymentRequestList[0].IUV = identificativoUnivocoVersamento;
                                        logger.Warn("Payment Request Id {0} identificata senza IUV. Id. Univoco Riscossione: {1}. Aggiornato IUV = {2}.", dbPaymentRequestList[0].idPaymentRequest, identificativoUnivocoRiscossione, identificativoUnivocoVersamento);
                                    }
                                    else if (dbPaymentRequestList.Length == 0)
                                    {
                                        //ultima spiaggia, provo solo con IUV
                                        dbPaymentRequestList =
                                            ctx.COM_PayerPaymentRequest.Where(
                                                c =>
                                                    c.IUV == identificativoUnivocoVersamento &&
                                                    DbFunctions.TruncateTime(c.DataOraInserimento) == datoSingoliPagamenti.dataEsitoSingoloPagamento &&
                                                    c.Esito == (short) PayerEsitoTransazione.OK &&
                                                    c.Importo == datoSingoliPagamenti.singoloImportoPagato)
                                                .ToArray();

                                        if (dbPaymentRequestList.Length == 1) //se ne ho trovato una e solo una
                                        {
                                            logger.Warn(
                                                "Payment Request Id {0} identificata solo tramite IUV = {1}. Id. Univoco Riscossione: {2}.",
                                                dbPaymentRequestList[0].idPaymentRequest,
                                                identificativoUnivocoVersamento, identificativoUnivocoRiscossione);
                                        }
                                        else if (dbPaymentRequestList.Length == 0)
                                        {
                                            var msg =
                                                string.Format(
                                                    "Payment Request non trovata nel db - identificativoUnivocoVersamento: {0} - identificativoUnivocoRiscossione: {1} \nIdentificativo flusso: {2}\nCartella:{3}",
                                                    identificativoUnivocoVersamento,
                                                    identificativoUnivocoRiscossione,
                                                    identificativoFlusso,
                                                    folderPath
                                                    );
                                            logger.Error(msg);
                                            errorList.Add(msg);
                                            //return false;
                                            continue; //elaboro comunque il file
                                        }
                                    }

                                }


                                if (dbPaymentRequestList.Length > 1)
                                {

                                    var msg = string.Format(
                                             "Payment Request multiple trovate nel db - identificativoUnivocoVersamento: {0} - identificativoUnivocoRiscossione: {1}\nIdentificativo flusso: {2}\nCartella:{3}",
                                            identificativoUnivocoVersamento, identificativoUnivocoRiscossione,
                                            identificativoFlusso,
                                            folderPath
                                            );
                                    errorList.Add(msg);
                                    return false;
                                }
                                else
                                {
                                    //ce n'è solo una come dovrebbe
                                    var dbPaymentRequest = dbPaymentRequestList[0];
                                    logger.Info("Payment Request trovata nel db - IdPaymentRequest: {0}", dbPaymentRequest.idPaymentRequest);
                                    dbPaymentRequest.COM_PayerFlussoRiversamento = dbFlussorendicontazione;
                                    logger.Info("Payment Request agganciata al flusso con identificativoFlusso: {0}", identificativoFlusso);
                                }
                            }

                            ctx.SaveChanges();


                            dbContextTransaction.Commit();
                            logger.Info("identificativoFlusso: {0} - Transazione COMMITTED", flussoRiversamento.identificativoFlusso);

                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            logger.Error(ex);
                            errorList.Add(ex.Message);
                            return false;
                        }
                    }

                }
            }

            return true;
        }

        //public static bool ElaboraZipGenerico(string filePath, List<string> errorList)
        //{
        //    var fileName = Path.GetFileNameWithoutExtension(filePath);
        //    if (fileName.StartsWith("IVP"))
        //    {
        //        return PayerRiversamentoUtilDb.ElaboraZipRendicontazionePayer(filePath, errorList);
        //    }
        //    else
        //    {
        //        return PayerRiversamentoUtilDb.ElaboraZipFlussoRiversamentoPagoPA(filePath, errorList);
        //    }
        //}
    }
}

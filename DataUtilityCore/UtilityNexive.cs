using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CsvHelper;
using DataLayer;
using DataUtilityCore.Enum;
using Ionic.Zip;

namespace DataUtilityCore
{
    public class UtilityNexive
    {
        //public static void SendToNexive(long iD, int typeofRaccomandata)
        //{
        //    string pathZip = GetZipFile(iD, typeofRaccomandata);
        //    bool fsend = UtilityFtpEngine.UploadSftpNexive(pathZip);
        //    //SetDocumentiRaccomandandateNexive(iD, fsend, typeofRaccomandata);
        //}
        
        //public static void DownloadFileEsitoFromNexive()
        //{
        //    // Download zip files from sftp Nexive
        //    // decompress zip file in temp directory --> ok
        //    // Read txt file
        //    // Insert data in table
        //    // drag pdf file in directory
        //    // delete temp files
        //    // delete directory
        //    // if ok rename zip file in sftp .STAMPATO
            
        //    string pathSave = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadAccertamento"] + @"\";
        //    string PathEsitiFile = pathSave + "EsitiRaccomandateNexive" + @"\";
        //    UtilityFileSystem.CreateDirectoryIfNotExists(PathEsitiFile);

        //    bool fDownload = UtilityFtpEngine.DownloadSftpNexive(PathEsitiFile);
        //    if (fDownload)
        //    {
        //        DirectoryInfo dir = new DirectoryInfo(PathEsitiFile);
        //        FileInfo[] listEsitiFile = dir.GetFiles("*.*");// dir.GetFiles("*.zip");
        //        for (int i = 0; i < listEsitiFile.Length; i++)
        //        {
        //            #region Create Directory
        //            string currentDir = Path.GetFileNameWithoutExtension(listEsitiFile[i].Name);
        //            if (!Directory.Exists(PathEsitiFile + currentDir))
        //            {
        //                Directory.CreateDirectory(PathEsitiFile + currentDir);
        //            }
        //            #endregion
        //            #region Unzip Files
        //            if (listEsitiFile[i].Name.Contains(".zip"))
        //            {
        //                using (ZipFile zip = ZipFile.Read(PathEsitiFile + listEsitiFile[i].Name))
        //                {
        //                    foreach (ZipEntry zipContent in zip)
        //                    {
        //                        zipContent.Extract(PathEsitiFile + currentDir, ExtractExistingFileAction.OverwriteSilently);
        //                    }
        //                }
        //            }                    
        //            #endregion
        //            #region Read Nexive txt file
        //            string[] filesTxt = Directory.GetFiles(PathEsitiFile + currentDir, "*.txt");
        //            foreach (string PathAndfileTxt in filesTxt)
        //            {
        //                List<string[]> lines = File.ReadLines(PathAndfileTxt)
        //                  .Select(line => line.Split(new char[] { '|' }))
        //                  .ToList();

        //                foreach (var field in lines)
        //                {
        //                    string barcode = field[0];
        //                    string servizio = field[1];
        //                    string dataAccettazione = field[2];
        //                    string filialeAccettazione = field[3];
        //                    string dataRecapito = field[4];
        //                    string dataPostalizzazione = field[5];
        //                    string dataReso = field[6];
        //                    string causale = field[7];
        //                    string latitudine = field[8];
        //                    string longitudine = field[9];
        //                    string via = field[10];
        //                    string numeroCivico = field[11];
        //                    string cap = field[12];
        //                    string localita = field[13];
        //                    string provincia = field[14];
        //                    string immagineBusta = field[15];
        //                    string immagineDistinta = field[16];
        //                    string immagineCartolina = field[17];
        //                    string destinatario = field[18];
        //                    string altro1 = field[19];
        //                    string altro2 = field[20];
        //                    string altro3 = field[21];
        //                    string altro4 = field[22];
        //                    string altro5 = field[23];

        //                    #region Save data Esiti into DB and Copy pdf file Esiti in directory
        //                    //Notifiche Nexive accertamenti
        //                    if (!string.IsNullOrEmpty(altro1) && altro1!= "\0")
        //                    {
        //                        long iDAccertamento = long.Parse(altro1.Split(new char[] { '_' })[0]);
        //                        int iDProceduraAccertamento = int.Parse(altro1.Split(new char[] { '_' })[1]);

        //                        SetEsitiRaccomandandateNexive(iDAccertamento, iDProceduraAccertamento, (int)EnumTypeofRaccomandata.TypeAccertamento,
        //                                                     barcode, servizio, dataAccettazione, filialeAccettazione,
        //                                                     dataRecapito, dataPostalizzazione, dataReso,
        //                                                     causale, latitudine, longitudine);

        //                        UtilityVerifiche.SetDataScadenzaInterventoNexive(iDAccertamento);

        //                        #region Copy pdf file Esiti in directory

        //                        var filePdfEsito = immagineCartolina.Split(new char[] { '\\' }).Last();
        //                        if (filePdfEsito != "\0")
        //                        {
        //                            using (var ctx = new CriterDataModel())
        //                            {
        //                                var accertamento = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iDAccertamento).FirstOrDefault();
        //                                if (accertamento != null)
        //                                {
        //                                    if (File.Exists(PathEsitiFile + currentDir + @"\" + filePdfEsito))
        //                                    {
        //                                        File.Copy(PathEsitiFile + currentDir + @"\" + filePdfEsito, pathSave + accertamento.CodiceAccertamento + @"\" + filePdfEsito, true);
        //                                    }
        //                                }
        //                            }
        //                        }                               
        //                        #endregion
        //                    }
        //                    //Notifiche Nexive revoca interventi non faccio nulla
        //                    else if (!string.IsNullOrEmpty(altro2) && altro2 != "\0")
        //                    {

        //                    } 
        //                    //Notifiche Ispezioni
        //                    else if (!string.IsNullOrEmpty(altro3) && altro3 != "\0")
        //                    {
        //                        int typeofRaccomandata = int.Parse(altro1.Split(new char[] { '_' })[0]);
        //                        int iDIspezioneVisita = int.Parse(altro1.Split(new char[] { '_' })[1]);
        //                        long iDIspezione = long.Parse(altro1.Split(new char[] { '_' })[2]);
                                

        //                        SetEsitiRaccomandandateNexive(iDIspezione, iDIspezioneVisita, typeofRaccomandata,
        //                                                     barcode, servizio, dataAccettazione, filialeAccettazione,
        //                                                     dataRecapito, dataPostalizzazione, dataReso,
        //                                                     causale, latitudine, longitudine);
                                                                
        //                        #region Copy pdf file Esiti in directory

        //                        var filePdfEsito = immagineCartolina.Split(new char[] { '\\' }).Last();
        //                        string pathIspezioni = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspezione"] + @"\";

        //                        if (filePdfEsito != "\0")
        //                        {
        //                            using (var ctx = new CriterDataModel())
        //                            {
        //                                var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == iDIspezione).FirstOrDefault();
        //                                if (ispezione != null)
        //                                {
        //                                    if (File.Exists(PathEsitiFile + currentDir + @"\" + filePdfEsito))
        //                                    {
        //                                        File.Copy(PathEsitiFile + currentDir + @"\" + filePdfEsito, pathIspezioni + iDIspezioneVisita + @"\" + ispezione.CodiceIspezione + @"\" + filePdfEsito, true);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        #endregion
        //                    }
        //                    //Notifiche Sanzioni
        //                    else if (!string.IsNullOrEmpty(altro4) && altro4 != "\0")
        //                    {
        //                        long iDAccertamento = long.Parse(altro4.Split(new char[] { '_' })[2]);


        //                        SetEsitiRaccomandandateNexive(iDAccertamento, 0, (int)EnumTypeofRaccomandata.TypeSanzione,
        //                                                     barcode, servizio, dataAccettazione, filialeAccettazione,
        //                                                     dataRecapito, dataPostalizzazione, dataReso,
        //                                                     causale, latitudine, longitudine);

        //                        UtilityVerifiche.SetDataScadenzaSanzione(iDAccertamento);
        //                    }
        //                    #endregion
        //                }
        //            }
        //            #endregion
        //            #region Move Zip file and delete tmp directory
        //            var pathGenericoConAnnoMese = Path.Combine(PathEsitiFile, DateTime.Today.ToString("yyyy-MM"));
        //            UtilityFileSystem.CreateDirectoryIfNotExists(pathGenericoConAnnoMese);
        //            if (File.Exists(PathEsitiFile + listEsitiFile[i].Name))
        //            {
        //                File.Copy(PathEsitiFile + listEsitiFile[i].Name, pathGenericoConAnnoMese + @"\" + listEsitiFile[i].Name, true);
        //            }
        //            if (Directory.Exists(PathEsitiFile + currentDir))
        //            {
        //                Directory.Delete(PathEsitiFile + currentDir, true);
        //            }
        //            #endregion
        //            #region Rename file in sFtp in .STAMPATO
        //            bool fRename = UtilityFtpEngine.RenameFileSftpNexive(listEsitiFile[i].Name);
        //            #endregion
        //        }
        //    }       
        //}

        //protected static string GetZipFile(long iD, int typeofRaccomandata)
        //{
        //    string fileAccompagnamento = FileAccompagnamentoNexive(iD, typeofRaccomandata);
        //    string NameZipNexive = string.Empty;
        //    string codiceAccertamento = string.Empty;
        //    string codiceIspezione = string.Empty;
        //    string iDIspezioneVisita = string.Empty;
        //    string reportName = string.Empty;

        //    string reportPath = ConfigurationManager.AppSettings["ReportPath"];
        //    string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
        //    string destinationFile = string.Empty;
        //    string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();

        //    switch (typeofRaccomandata)
        //    {
        //        case 1: //Accertamento
        //            #region Documenti Accertamenti
        //            NameZipNexive = "AccertamentiNexive_" + iD + ".zip";
        //            using (var ctx = new CriterDataModel())
        //            {
        //                codiceAccertamento = ctx.V_VER_Accertamenti.Where(c => c.IDAccertamento == iD).FirstOrDefault().CodiceAccertamento;
        //            }
        //            destinationFile = ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + codiceAccertamento;
        //            reportName = ConfigurationManager.AppSettings["ReportNameFrontespizioNexive"];

        //            using (ZipFile zip = new ZipFile())
        //            {
        //                var documenti = UtilityVerifiche.GetDocumentiAccertamenti(iD);
        //                foreach (var documento in documenti)
        //                {
        //                    //Inserimento del fronteSpizio Nexive
        //                    string ReportAccertamento = ReportingServices.GetAccertamentiNexiveReport(documento.IDAccertamento.ToString(), documento.IDProceduraAccertamento.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);

        //                    string file = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + ReportAccertamento;
        //                    FileInfo filePdf = new FileInfo(file);
        //                    if (File.Exists(file))
        //                    {
        //                        zip.AddFile(file, "");
        //                    }
        //                }
        //                zip.AddFile(fileAccompagnamento, "");
        //                zip.Save(ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + NameZipNexive);
        //            }
        //            #endregion
        //            break;
        //        case 2: //Revoca
        //            #region Revoca Interventi
        //            NameZipNexive = "AccertamentiInterventiNexive_" + iD + ".zip";
        //            using (var ctx = new CriterDataModel())
        //            {
        //                codiceAccertamento = ctx.V_VER_Accertamenti.Where(c => c.IDAccertamento == iD).FirstOrDefault().CodiceAccertamento;
        //            }
        //            destinationFile = ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + codiceAccertamento;
        //            reportName = ConfigurationManager.AppSettings["ReportNameFrontespizioVuotoNexive"];

        //            using (ZipFile zip = new ZipFile())
        //            {
        //                var documenti = UtilityVerifiche.GetDocumentiAccertamenti(iD);
        //                foreach (var documento in documenti)
        //                {
        //                    //Inserimento del fronteSpizio Nexive
        //                    string ReportAccertamento = string.Empty;

        //                    if (documento.IDProceduraAccertamento == 1 || documento.IDProceduraAccertamento == 2)
        //                    {
        //                        ReportAccertamento = ReportingServices.GetInterventiRevocaNexiveReport(documento.IDAccertamento.ToString(), documento.IDProceduraAccertamento.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);
        //                        string fileInterventi = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + ReportAccertamento;

        //                        if (File.Exists(fileInterventi))
        //                        {
        //                            zip.AddFile(fileInterventi, "");
        //                        }

        //                        break;
        //                    }

        //                    string file = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + ReportAccertamento;
        //                    FileInfo filePdf = new FileInfo(file);
        //                    if (File.Exists(file))
        //                    {
        //                        zip.AddFile(file, "");
        //                    }
        //                }
        //                zip.AddFile(fileAccompagnamento, "");
        //                zip.Save(ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + NameZipNexive);
        //            }
        //            #endregion
        //            break;
        //        case 3: //Conferma Pianificazione Ispezione
        //            #region Conferma Pianificazione Ispezione
        //            NameZipNexive = "IspezionePianificazioneConfermaNexive_" + iD + ".zip";
        //            reportName = ConfigurationManager.AppSettings["ReportNameIspezionePianificazioneConferma"];
                                        
        //            using (var ctx = new CriterDataModel())
        //            {
        //                var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == iD).FirstOrDefault();
        //                codiceIspezione = ispezione.CodiceIspezione;
        //                iDIspezioneVisita = ispezione.IDIspezioneVisita.ToString();
        //            }

        //            //Genero il Report Ispezione di base
        //            destinationFile = ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + iDIspezioneVisita + @"\" + codiceIspezione;
        //            ReportingServices.GetIspezionePianificazioneConfermaReport(iD.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);
                    
        //            using (ZipFile zip = new ZipFile())
        //            {
        //                string ReportIspezione = string.Empty;
        //                reportName = ConfigurationManager.AppSettings["ReportNameFrontespizioVuotoIspezioneNexive"];
        //                ReportIspezione = ReportingServices.GetIspezionePianificazioneConfermaNexiveReport(iD.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);
        //                string fileIspezione = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + ReportIspezione;

        //                if (File.Exists(fileIspezione))
        //                {
        //                    zip.AddFile(fileIspezione, "");
        //                }

        //                zip.AddFile(fileAccompagnamento, "");
        //                zip.Save(ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + NameZipNexive);
        //            }
        //            #endregion
        //            break;
        //        case 4: //Annullamento Pianificazione ispezione
        //            #region Annullamento Pianificazione ispezione
        //            NameZipNexive = "IspezioneAnnullamentoNexive_" + iD + ".zip";
        //            reportName = ConfigurationManager.AppSettings["ReportNameIspezioneAnnullamento"];
                                        
        //            using (var ctx = new CriterDataModel())
        //            {
        //                var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == iD).FirstOrDefault();
        //                codiceIspezione = ispezione.CodiceIspezione;
        //                iDIspezioneVisita = ispezione.IDIspezioneVisita.ToString();
        //            }

        //            //Genero il Report Ispezione di base
        //            destinationFile = ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + iDIspezioneVisita + @"\" + codiceIspezione;
        //            ReportingServices.GetIspezioneAnnullamentoConfermaReport(iD.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);

        //            using (ZipFile zip = new ZipFile())
        //            {
        //                string ReportIspezione = string.Empty;
        //                reportName = ConfigurationManager.AppSettings["ReportNameFrontespizioVuotoIspezioneNexive"];
        //                ReportIspezione = ReportingServices.GetIspezioneAnnullamentoNexiveReport(iD.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);
        //                string fileIspezione = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + ReportIspezione;

        //                if (File.Exists(fileIspezione))
        //                {
        //                    zip.AddFile(fileIspezione, "");
        //                }

        //                zip.AddFile(fileAccompagnamento, "");
        //                zip.Save(ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + NameZipNexive);
        //            }
        //            #endregion
        //            break;
        //        case 5: //Ripianificazione ispezione
        //            #region Ripianificazione ispezione
        //            NameZipNexive = "IspezionePianificazioneRipianificazioneNexive_" + iD + ".zip";
        //            reportName = ConfigurationManager.AppSettings["ReportNameIspezionePianificazioneRipianificazione"];

        //            using (var ctx = new CriterDataModel())
        //            {
        //                var ispezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == iD).FirstOrDefault();
        //                codiceIspezione = ispezione.CodiceIspezione;
        //                iDIspezioneVisita = ispezione.IDIspezioneVisita.ToString();
        //            }

        //            //Genero il Report Ispezione di base
        //            destinationFile = ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + iDIspezioneVisita + @"\" + codiceIspezione;
        //            ReportingServices.GetIspezionePianificazioneRipianificazioneReport(iD.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);

        //            using (ZipFile zip = new ZipFile())
        //            {
        //                string ReportIspezione = string.Empty;
        //                reportName = ConfigurationManager.AppSettings["ReportNameFrontespizioVuotoIspezioneNexive"];
        //                ReportIspezione = ReportingServices.GetIspezionePianificazioneRipianificazioneNexiveReport(iD.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);
        //                string fileIspezione = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + ReportIspezione;

        //                if (File.Exists(fileIspezione))
        //                {
        //                    zip.AddFile(fileIspezione, "");
        //                }

        //                zip.AddFile(fileAccompagnamento, "");
        //                zip.Save(ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + NameZipNexive);
        //            }
        //            #endregion
        //            break;
        //        case 6: //Sanzioni
        //            #region Sanzioni
        //            NameZipNexive = "SanzioniNexive_" + iD + ".zip";
        //            reportName = ConfigurationManager.AppSettings["ReportNameSanzione"];
        //            using (var ctx = new CriterDataModel())
        //            {
        //                codiceAccertamento = ctx.V_VER_Accertamenti.Where(c => c.IDAccertamento == iD).FirstOrDefault().CodiceAccertamento;
        //            }
        //            destinationFile = ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + codiceAccertamento;
                    
        //            using (ZipFile zip = new ZipFile())
        //            {
        //                string ReportSanzione = ReportingServices.GetSanzioniNexiveReport(iD.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);

        //                string fileSanzione = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + ReportSanzione;

        //                if (File.Exists(fileSanzione))
        //                {
        //                    zip.AddFile(fileSanzione, "");
        //                }

        //                zip.AddFile(fileAccompagnamento, "");
        //                zip.Save(ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + NameZipNexive);
        //            }
        //            #endregion
        //            break;
        //    }
            
        //    return ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + NameZipNexive;
        //}

        //protected static string FileAccompagnamentoNexive(long iD, int typeofRaccomandata)
        //{
        //    string pathTemplateFileIndice = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadAccertamento"] + @"\";
        //    string pathIspezioneFileIndice = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIspezioni"] + @"\";

        //    string codiceAccertamento = string.Empty;
        //    string codiceIspezione = string.Empty;
        //    string iDIspezioneVisita = string.Empty;
        //    string newFileName = string.Empty;
        //    string pathReturn = string.Empty;

        //    switch (typeofRaccomandata)
        //    {
        //        case 1: //Accertamento
        //            #region Accertamento
        //            newFileName = "NexiveFileIndice_" + iD.ToString() + ".csv";

        //            using (var ctx = new CriterDataModel())
        //            {
        //                var accertamento = ctx.V_VER_Accertamenti.Where(c => c.IDAccertamento == iD).FirstOrDefault();
        //                codiceAccertamento = accertamento.CodiceAccertamento;

        //                File.Copy(pathTemplateFileIndice + "TemplateNexiveFileIndice.csv", pathTemplateFileIndice + codiceAccertamento + @"\" + newFileName, true);

        //                using (var writer = new StreamWriter(pathTemplateFileIndice + codiceAccertamento + @"\" + newFileName, true, Encoding.ASCII))
        //                {
        //                    using (var csv = new CsvWriter(writer))
        //                    {
        //                        csv.Configuration.Delimiter = ";";
        //                        var documenti = UtilityVerifiche.GetDocumentiAccertamenti(iD);
        //                        foreach (var documento in documenti)
        //                        {
        //                            #region Accertamenti
        //                            string nomeDocumento = "Nexive_" + documento.IDAccertamento + "_" + documento.IDProceduraAccertamento + ".pdf";
        //                            csv.WriteField(nomeDocumento);                                                              //Nome_Documento_PDF
        //                            csv.WriteField("RR");                                                                       //Tipo_Postalizzazione --> Raccomandata AR Nexive
        //                            csv.WriteField(accertamento.NomeResponsabile + " " + accertamento.CognomeResponsabile);     //Rag_Soc_Recapito
        //                            csv.WriteField(accertamento.IndirizzoResponsabile + ", " + accertamento.CivicoResponsabile);//Indirizzo_Recapito
        //                            csv.WriteField(accertamento.CapResponsabile);                                               //CAP_Recapito
        //                            csv.WriteField(accertamento.ComuneResponsabile);                                            //Località_Recapito
        //                            csv.WriteField(accertamento.ProvinciaResponsabile);                                         //Provincia_Recapito
        //                            csv.WriteField("Italia");                                                                   //Nazione_Recapito
        //                            csv.WriteField("Arter SpA");                                                                //Rag_Soc_Mittenza --> Dati Mittente della cartolina di ritorno per le raccomandate AR
        //                            csv.WriteField("Via Gian Battista Morgagni, 6");                                            //Indirizzo_Mittenza
        //                            csv.WriteField("40122");                                                                    //CAP_Mittenza
        //                            csv.WriteField("Bologna");                                                                  //Località_Mittenza
        //                            csv.WriteField("BO");                                                                       //Provincia_Mittenza
        //                            csv.WriteField("Italia");                                                                   //Nazione_Mittenza
        //                            csv.WriteField("");                                                                         //Facciate --> Numero di facciate che conpongono il documento
        //                            csv.WriteField("");                                                                         //Fogli_Bollettino_da --> Numero di pagina documento CCP partenza
        //                            csv.WriteField("");                                                                         //Fogli_Bollettio_a --> Numero di pagina documento CCP fine

        //                            csv.WriteField(documento.IDAccertamento + "_" + documento.IDProceduraAccertamento);         //IdDoc_1 --> Campi di Servizio  --> 
        //                            csv.WriteField("");                       //IdDoc_2 --> Campi di Servizio
        //                            csv.WriteField("");                       //IdDoc_3 --> Campi di Servizio
        //                            csv.WriteField("");                       //IdDoc_4 --> Campi di Servizio
        //                            csv.WriteField("");                       //IdDoc_5 --> Campi di Servizio
        //                            csv.WriteField("");                       //IdDoc_6 --> Campi di Servizio
        //                            csv.WriteField("");                       //IdDoc_7 --> Campi di Servizio
        //                            csv.WriteField("");                       //IdDoc_8 --> Campi di Servizio

        //                            csv.WriteField("");                       //Allegato_1 --> Nome dell’allegato 1 Tipografico
        //                            csv.WriteField("");                       //Peso_Allegato --> Quantità di fogli (peso) Allegato 1
        //                            csv.WriteField("");                       //Allegato_2 --> Nome dell’allegato 2 Tipografico
        //                            csv.WriteField("");                       //Peso_Allegato_2 --> Quantità di fogli (peso) Allegato 2
        //                            csv.WriteField("R");                      //Modalita_Stampa --> F = Fronte R = Fronte e Retro
        //                            csv.WriteField("B");                      //Tipologia_Stampa --> B = Bianco e Nero C = Colore
        //                            csv.WriteField("");                       //Tipologia_Fatturazione --> R = Recupero IVA N = No Recupero IVA
        //                            csv.WriteField("");                       //Centro_Costo --> 
        //                            csv.NextRecord();
        //                            #endregion
        //                        }

        //                        writer.Flush();
        //                    }
        //                }
        //            }

        //            pathReturn = pathTemplateFileIndice + codiceAccertamento + @"\" + newFileName;
        //            #endregion
        //            break;
        //        case 2: //Revoca
        //            #region Revoca
        //            newFileName = "NexiveRevocaFileIndice_" + iD.ToString() + ".csv";

        //            using (var ctx = new CriterDataModel())
        //            {
        //                var accertamento = ctx.V_VER_Accertamenti.Where(c => c.IDAccertamento == iD).FirstOrDefault();
        //                codiceAccertamento = accertamento.CodiceAccertamento;

        //                File.Copy(pathTemplateFileIndice + "TemplateNexiveFileIndice.csv", pathTemplateFileIndice + codiceAccertamento + @"\" + newFileName, true);

        //                using (var writer = new StreamWriter(pathTemplateFileIndice + codiceAccertamento + @"\" + newFileName, true, Encoding.ASCII))
        //                {
        //                    using (var csv = new CsvWriter(writer))
        //                    {
        //                        csv.Configuration.Delimiter = ";";
        //                        var documenti = UtilityVerifiche.GetDocumentiAccertamenti(iD);
        //                        foreach (var documento in documenti)
        //                        {
        //                            #region Interventi
        //                            if (documento.IDProceduraAccertamento == 1 || documento.IDProceduraAccertamento == 2)
        //                            {
        //                                string nomeDocumento = "RevocaInterventiNexive_" + documento.IDAccertamento + ".pdf";
        //                                csv.WriteField(nomeDocumento);                                                              //Nome_Documento_PDF
        //                                csv.WriteField("RR");                                                                       //Tipo_Postalizzazione --> Raccomandata AR Nexive
        //                                csv.WriteField(accertamento.NomeResponsabile + " " + accertamento.CognomeResponsabile);     //Rag_Soc_Recapito
        //                                csv.WriteField(accertamento.IndirizzoResponsabile + ", " + accertamento.CivicoResponsabile);//Indirizzo_Recapito
        //                                csv.WriteField(accertamento.CapResponsabile);                                               //CAP_Recapito
        //                                csv.WriteField(accertamento.ComuneResponsabile);                                            //Località_Recapito
        //                                csv.WriteField(accertamento.ProvinciaResponsabile);                                         //Provincia_Recapito
        //                                csv.WriteField("Italia");                                                                   //Nazione_Recapito
        //                                csv.WriteField("Arter SpA");                                                                //Rag_Soc_Mittenza --> Dati Mittente della cartolina di ritorno per le raccomandate AR
        //                                csv.WriteField("Via Gian Battista Morgagni, 6");                                            //Indirizzo_Mittenza
        //                                csv.WriteField("40122");                                                                    //CAP_Mittenza
        //                                csv.WriteField("Bologna");                                                                  //Località_Mittenza
        //                                csv.WriteField("BO");                                                                       //Provincia_Mittenza
        //                                csv.WriteField("Italia");                                                                   //Nazione_Mittenza
        //                                csv.WriteField("");                                                                         //Facciate --> Numero di facciate che conpongono il documento
        //                                csv.WriteField("");                                                                         //Fogli_Bollettino_da --> Numero di pagina documento CCP partenza
        //                                csv.WriteField("");                                                                         //Fogli_Bollettio_a --> Numero di pagina documento CCP fine

        //                                csv.WriteField("");                       //IdDoc_1 --> Campi di Servizio  --> 
        //                                csv.WriteField(documento.IDAccertamento + "_" + documento.IDProceduraAccertamento);         //IdDoc_2 --> Campi di Servizio
        //                                csv.WriteField("");                       //IdDoc_3 --> Campi di Servizio
        //                                csv.WriteField("");                       //IdDoc_4 --> Campi di Servizio
        //                                csv.WriteField("");                       //IdDoc_5 --> Campi di Servizio
        //                                csv.WriteField("");                       //IdDoc_6 --> Campi di Servizio
        //                                csv.WriteField("");                       //IdDoc_7 --> Campi di Servizio
        //                                csv.WriteField("");                       //IdDoc_8 --> Campi di Servizio

        //                                csv.WriteField("");                       //Allegato_1 --> Nome dell’allegato 1 Tipografico
        //                                csv.WriteField("");                       //Peso_Allegato --> Quantità di fogli (peso) Allegato 1
        //                                csv.WriteField("");                       //Allegato_2 --> Nome dell’allegato 2 Tipografico
        //                                csv.WriteField("");                       //Peso_Allegato_2 --> Quantità di fogli (peso) Allegato 2
        //                                csv.WriteField("R");                      //Modalita_Stampa --> F = Fronte R = Fronte e Retro
        //                                csv.WriteField("B");                      //Tipologia_Stampa --> B = Bianco e Nero C = Colore
        //                                csv.WriteField("");                       //Tipologia_Fatturazione --> R = Recupero IVA N = No Recupero IVA
        //                                csv.WriteField("");                       //Centro_Costo --> 
        //                                csv.NextRecord();
        //                                break;
        //                            }
        //                            #endregion
        //                        }

        //                        writer.Flush();
        //                    }
        //                }
        //            }

        //            pathReturn = pathTemplateFileIndice + codiceAccertamento + @"\" + newFileName;
        //            #endregion
        //            break;
        //        case 3: //Conferma Pianificazione Ispezione
        //            #region Conferma Pianificazione Ispezione
        //            newFileName = "NexivePianificazioneIspezioneConfermaFileIndice_" + iD.ToString() + ".csv";
        //            using (var ctx = new CriterDataModel())
        //            {
        //                var ispezione = ctx.V_VER_Ispezioni.Where(c => c.IDIspezione == iD).FirstOrDefault();
        //                codiceIspezione = ispezione.CodiceIspezione;
        //                iDIspezioneVisita = ispezione.IDIspezioneVisita.ToString();

        //                File.Copy(pathTemplateFileIndice + "TemplateNexiveFileIndice.csv", pathIspezioneFileIndice + iDIspezioneVisita + @"\" + codiceIspezione + @"\" + newFileName, true);

        //                using (var writer = new StreamWriter(pathIspezioneFileIndice + iDIspezioneVisita + @"\" + codiceIspezione + @"\" + newFileName, true, Encoding.ASCII))
        //                {
        //                    using (var csv = new CsvWriter(writer))
        //                    {
        //                        csv.Configuration.Delimiter = ";";
        //                        string nomeDocumento = "IspezionePianificazioneConfermaNexive_" + ispezione.IDIspezione.ToString() + ".pdf";
        //                        csv.WriteField(nomeDocumento);                                                              //Nome_Documento_PDF
        //                        csv.WriteField("RR");                                                                       //Tipo_Postalizzazione --> Raccomandata AR Nexive
        //                        csv.WriteField(ispezione.NomeResponsabile + " " + ispezione.CognomeResponsabile);          //Rag_Soc_Recapito
        //                        csv.WriteField(ispezione.IndirizzoResponsabile + ", " + ispezione.CivicoResponsabile);     //Indirizzo_Recapito
        //                        csv.WriteField(ispezione.CapResponsabile);                                                 //CAP_Recapito
        //                        csv.WriteField(ispezione.ComuneResponsabile);                                            //Località_Recapito
        //                        csv.WriteField(ispezione.ProvinciaResponsabile);                                             //Provincia_Recapito
        //                        csv.WriteField("Italia");                                                                   //Nazione_Recapito
        //                        csv.WriteField("Arter SpA");                                                                //Rag_Soc_Mittenza --> Dati Mittente della cartolina di ritorno per le raccomandate AR
        //                        csv.WriteField("Via Gian Battista Morgagni, 6");                                            //Indirizzo_Mittenza
        //                        csv.WriteField("40122");                                                                    //CAP_Mittenza
        //                        csv.WriteField("Bologna");                                                                  //Località_Mittenza
        //                        csv.WriteField("BO");                                                                       //Provincia_Mittenza
        //                        csv.WriteField("Italia");                                                                   //Nazione_Mittenza
        //                        csv.WriteField("");                                                                         //Facciate --> Numero di facciate che conpongono il documento
        //                        csv.WriteField("");                                                                         //Fogli_Bollettino_da --> Numero di pagina documento CCP partenza
        //                        csv.WriteField("");                                                                         //Fogli_Bollettio_a --> Numero di pagina documento CCP fine

        //                        csv.WriteField("");                       //IdDoc_1 --> Campi di Servizio  --> 
        //                        csv.WriteField("");                       //IdDoc_2 --> Campi di Servizio
        //                        csv.WriteField(typeofRaccomandata.ToString() + "_" + ispezione.IDIspezioneVisita + "_" + ispezione.IDIspezione);                       //IdDoc_3 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_4 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_5 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_6 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_7 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_8 --> Campi di Servizio

        //                        csv.WriteField("");                       //Allegato_1 --> Nome dell’allegato 1 Tipografico
        //                        csv.WriteField("");                       //Peso_Allegato --> Quantità di fogli (peso) Allegato 1
        //                        csv.WriteField("");                       //Allegato_2 --> Nome dell’allegato 2 Tipografico
        //                        csv.WriteField("");                       //Peso_Allegato_2 --> Quantità di fogli (peso) Allegato 2
        //                        csv.WriteField("R");                      //Modalita_Stampa --> F = Fronte R = Fronte e Retro
        //                        csv.WriteField("B");                      //Tipologia_Stampa --> B = Bianco e Nero C = Colore
        //                        csv.WriteField("");                       //Tipologia_Fatturazione --> R = Recupero IVA N = No Recupero IVA
        //                        csv.WriteField("");                       //Centro_Costo --> 
        //                        csv.NextRecord();

        //                        writer.Flush();
        //                    }
        //                }
        //            }
        //            pathReturn = pathIspezioneFileIndice + iDIspezioneVisita + @"\" + codiceIspezione + @"\" + newFileName;
        //            #endregion
        //            break;
        //        case 4: //Annullamento Pianificazione ispezione
        //            #region Annullamento Pianificazione ispezione
        //            newFileName = "NexivePianificazioneIspezioneAnnullamentoFileIndice_" + iD.ToString() + ".csv";
        //            using (var ctx = new CriterDataModel())
        //            {
        //                var ispezione = ctx.V_VER_Ispezioni.Where(c => c.IDIspezione == iD).FirstOrDefault();
        //                codiceIspezione = ispezione.CodiceIspezione;
        //                iDIspezioneVisita = ispezione.IDIspezioneVisita.ToString();

        //                File.Copy(pathTemplateFileIndice + "TemplateNexiveFileIndice.csv", pathIspezioneFileIndice + iDIspezioneVisita + @"\" + codiceIspezione + @"\" + newFileName, true);

        //                using (var writer = new StreamWriter(pathIspezioneFileIndice + iDIspezioneVisita + @"\" + codiceIspezione + @"\" + newFileName, true, Encoding.ASCII))
        //                {
        //                    using (var csv = new CsvWriter(writer))
        //                    {
        //                        csv.Configuration.Delimiter = ";";
        //                        string nomeDocumento = "IspezioneAnnullamentoNexive_" + ispezione.IDIspezione.ToString() + ".pdf";
        //                        csv.WriteField(nomeDocumento);                                                              //Nome_Documento_PDF
        //                        csv.WriteField("RR");                                                                       //Tipo_Postalizzazione --> Raccomandata AR Nexive
        //                        csv.WriteField(ispezione.NomeResponsabile + " " + ispezione.CognomeResponsabile);     //Rag_Soc_Recapito
        //                        csv.WriteField(ispezione.IndirizzoResponsabile + ", " + ispezione.CivicoResponsabile);//Indirizzo_Recapito
        //                        csv.WriteField(ispezione.CapResponsabile);                                               //CAP_Recapito
        //                        csv.WriteField(ispezione.ComuneResponsabile);                                            //Località_Recapito
        //                        csv.WriteField(ispezione.ProvinciaResponsabile);                                         //Provincia_Recapito
        //                        csv.WriteField("Italia");                                                                   //Nazione_Recapito
        //                        csv.WriteField("Arter SpA");                                                                //Rag_Soc_Mittenza --> Dati Mittente della cartolina di ritorno per le raccomandate AR
        //                        csv.WriteField("Via Gian Battista Morgagni, 6");                                            //Indirizzo_Mittenza
        //                        csv.WriteField("40122");                                                                    //CAP_Mittenza
        //                        csv.WriteField("Bologna");                                                                  //Località_Mittenza
        //                        csv.WriteField("BO");                                                                       //Provincia_Mittenza
        //                        csv.WriteField("Italia");                                                                   //Nazione_Mittenza
        //                        csv.WriteField("");                                                                         //Facciate --> Numero di facciate che conpongono il documento
        //                        csv.WriteField("");                                                                         //Fogli_Bollettino_da --> Numero di pagina documento CCP partenza
        //                        csv.WriteField("");                                                                         //Fogli_Bollettio_a --> Numero di pagina documento CCP fine

        //                        csv.WriteField("");                       //IdDoc_1 --> Campi di Servizio  --> 
        //                        csv.WriteField("");                       //IdDoc_2 --> Campi di Servizio
        //                        csv.WriteField(typeofRaccomandata.ToString() + "_" + ispezione.IDIspezioneVisita + "_" + ispezione.IDIspezione);                       //IdDoc_3 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_4 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_5 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_6 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_7 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_8 --> Campi di Servizio

        //                        csv.WriteField("");                       //Allegato_1 --> Nome dell’allegato 1 Tipografico
        //                        csv.WriteField("");                       //Peso_Allegato --> Quantità di fogli (peso) Allegato 1
        //                        csv.WriteField("");                       //Allegato_2 --> Nome dell’allegato 2 Tipografico
        //                        csv.WriteField("");                       //Peso_Allegato_2 --> Quantità di fogli (peso) Allegato 2
        //                        csv.WriteField("R");                      //Modalita_Stampa --> F = Fronte R = Fronte e Retro
        //                        csv.WriteField("B");                      //Tipologia_Stampa --> B = Bianco e Nero C = Colore
        //                        csv.WriteField("");                       //Tipologia_Fatturazione --> R = Recupero IVA N = No Recupero IVA
        //                        csv.WriteField("");                       //Centro_Costo --> 
        //                        csv.NextRecord();

        //                        writer.Flush();
        //                    }
        //                }
        //            }
        //            pathReturn = pathIspezioneFileIndice + iDIspezioneVisita + @"\" + codiceIspezione + @"\" + newFileName;
        //            #endregion
        //            break;
        //        case 5: //Ripianificazione ispezione
        //            #region Ripianificazione ispezione
        //            newFileName = "NexivePianificazioneIspezioneRipianificazioneFileIndice_" + iD.ToString() + ".csv";
        //            using (var ctx = new CriterDataModel())
        //            {
        //                var ispezione = ctx.V_VER_Ispezioni.Where(c => c.IDIspezione == iD).FirstOrDefault();
        //                codiceIspezione = ispezione.CodiceIspezione;
        //                iDIspezioneVisita = ispezione.IDIspezioneVisita.ToString();

        //                File.Copy(pathTemplateFileIndice + "TemplateNexiveFileIndice.csv", pathIspezioneFileIndice + iDIspezioneVisita + @"\" + codiceIspezione + @"\" + newFileName, true);

        //                using (var writer = new StreamWriter(pathIspezioneFileIndice + iDIspezioneVisita + @"\" + codiceIspezione + @"\" + newFileName, true, Encoding.ASCII))
        //                {
        //                    using (var csv = new CsvWriter(writer))
        //                    {
        //                        csv.Configuration.Delimiter = ";";
        //                        string nomeDocumento = "IspezionePianificazioneRipianificazioneNexive_" + ispezione.IDIspezione.ToString() + ".pdf";
        //                        csv.WriteField(nomeDocumento);                                                              //Nome_Documento_PDF
        //                        csv.WriteField("RR");                                                                       //Tipo_Postalizzazione --> Raccomandata AR Nexive
        //                        csv.WriteField(ispezione.NomeResponsabile + " " + ispezione.CognomeResponsabile);     //Rag_Soc_Recapito
        //                        csv.WriteField(ispezione.IndirizzoResponsabile + ", " + ispezione.CivicoResponsabile);//Indirizzo_Recapito
        //                        csv.WriteField(ispezione.CapResponsabile);                                               //CAP_Recapito
        //                        csv.WriteField(ispezione.ComuneResponsabile);                                            //Località_Recapito
        //                        csv.WriteField(ispezione.ProvinciaResponsabile);                                         //Provincia_Recapito
        //                        csv.WriteField("Italia");                                                                   //Nazione_Recapito
        //                        csv.WriteField("Arter SpA");                                                                //Rag_Soc_Mittenza --> Dati Mittente della cartolina di ritorno per le raccomandate AR
        //                        csv.WriteField("Via Gian Battista Morgagni, 6");                                            //Indirizzo_Mittenza
        //                        csv.WriteField("40122");                                                                    //CAP_Mittenza
        //                        csv.WriteField("Bologna");                                                                  //Località_Mittenza
        //                        csv.WriteField("BO");                                                                       //Provincia_Mittenza
        //                        csv.WriteField("Italia");                                                                   //Nazione_Mittenza
        //                        csv.WriteField("");                                                                         //Facciate --> Numero di facciate che conpongono il documento
        //                        csv.WriteField("");                                                                         //Fogli_Bollettino_da --> Numero di pagina documento CCP partenza
        //                        csv.WriteField("");                                                                         //Fogli_Bollettio_a --> Numero di pagina documento CCP fine

        //                        csv.WriteField("");                       //IdDoc_1 --> Campi di Servizio  --> 
        //                        csv.WriteField("");                       //IdDoc_2 --> Campi di Servizio
        //                        csv.WriteField(typeofRaccomandata.ToString() + "_" + ispezione.IDIspezioneVisita + "_" + ispezione.IDIspezione);                       //IdDoc_3 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_4 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_5 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_6 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_7 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_8 --> Campi di Servizio

        //                        csv.WriteField("");                       //Allegato_1 --> Nome dell’allegato 1 Tipografico
        //                        csv.WriteField("");                       //Peso_Allegato --> Quantità di fogli (peso) Allegato 1
        //                        csv.WriteField("");                       //Allegato_2 --> Nome dell’allegato 2 Tipografico
        //                        csv.WriteField("");                       //Peso_Allegato_2 --> Quantità di fogli (peso) Allegato 2
        //                        csv.WriteField("R");                      //Modalita_Stampa --> F = Fronte R = Fronte e Retro
        //                        csv.WriteField("B");                      //Tipologia_Stampa --> B = Bianco e Nero C = Colore
        //                        csv.WriteField("");                       //Tipologia_Fatturazione --> R = Recupero IVA N = No Recupero IVA
        //                        csv.WriteField("");                       //Centro_Costo --> 
        //                        csv.NextRecord();

        //                        writer.Flush();
        //                    }
        //                }
        //            }
        //            pathReturn = pathIspezioneFileIndice + iDIspezioneVisita + @"\" + codiceIspezione + @"\" + newFileName;
        //            #endregion
        //            break;
        //        case 6: //Sanzione
        //            #region Revoca
        //            newFileName = "NexiveSanzioneFileIndice_" + iD.ToString() + ".csv";

        //            using (var ctx = new CriterDataModel())
        //            {
        //                var sanzione = ctx.V_VER_Accertamenti.Where(c => c.IDAccertamento == iD).FirstOrDefault();
        //                codiceAccertamento = sanzione.CodiceAccertamento;

        //                File.Copy(pathTemplateFileIndice + "TemplateNexiveFileIndice.csv", pathTemplateFileIndice + codiceAccertamento + @"\" + newFileName, true);

        //                using (var writer = new StreamWriter(pathTemplateFileIndice + codiceAccertamento + @"\" + newFileName, true, Encoding.ASCII))
        //                {
        //                    using (var csv = new CsvWriter(writer))
        //                    {
        //                        csv.Configuration.Delimiter = ";";

        //                        string nomeDocumento = "SanzioniNexive_" + iD + ".pdf";
        //                        csv.WriteField(nomeDocumento);                                                              //Nome_Documento_PDF
        //                        csv.WriteField("RR");                                                                       //Tipo_Postalizzazione --> Raccomandata AR Nexive
        //                        csv.WriteField(sanzione.NomeResponsabile + " " + sanzione.CognomeResponsabile);     //Rag_Soc_Recapito
        //                        csv.WriteField(sanzione.IndirizzoResponsabile + ", " + sanzione.CivicoResponsabile);//Indirizzo_Recapito
        //                        csv.WriteField(sanzione.CapResponsabile);                                               //CAP_Recapito
        //                        csv.WriteField(sanzione.ComuneResponsabile);                                            //Località_Recapito
        //                        csv.WriteField(sanzione.ProvinciaResponsabile);                                         //Provincia_Recapito
        //                        csv.WriteField("Italia");                                                                   //Nazione_Recapito
        //                        csv.WriteField("Arter SpA");                                                                //Rag_Soc_Mittenza --> Dati Mittente della cartolina di ritorno per le raccomandate AR
        //                        csv.WriteField("Via Gian Battista Morgagni, 6");                                            //Indirizzo_Mittenza
        //                        csv.WriteField("40122");                                                                    //CAP_Mittenza
        //                        csv.WriteField("Bologna");                                                                  //Località_Mittenza
        //                        csv.WriteField("BO");                                                                       //Provincia_Mittenza
        //                        csv.WriteField("Italia");                                                                   //Nazione_Mittenza
        //                        csv.WriteField("");                                                                         //Facciate --> Numero di facciate che conpongono il documento
        //                        csv.WriteField("");                                                                         //Fogli_Bollettino_da --> Numero di pagina documento CCP partenza
        //                        csv.WriteField("");                                                                         //Fogli_Bollettio_a --> Numero di pagina documento CCP fine

        //                        csv.WriteField("");                       //IdDoc_1 --> Campi di Servizio  --> 
        //                        csv.WriteField("");                       //IdDoc_2 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_3 --> Campi di Servizio
        //                        csv.WriteField(sanzione.IDAccertamento);  //IdDoc_4 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_5 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_6 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_7 --> Campi di Servizio
        //                        csv.WriteField("");                       //IdDoc_8 --> Campi di Servizio

        //                        csv.WriteField("");                       //Allegato_1 --> Nome dell’allegato 1 Tipografico
        //                        csv.WriteField("");                       //Peso_Allegato --> Quantità di fogli (peso) Allegato 1
        //                        csv.WriteField("");                       //Allegato_2 --> Nome dell’allegato 2 Tipografico
        //                        csv.WriteField("");                       //Peso_Allegato_2 --> Quantità di fogli (peso) Allegato 2
        //                        csv.WriteField("R");                      //Modalita_Stampa --> F = Fronte R = Fronte e Retro
        //                        csv.WriteField("B");                      //Tipologia_Stampa --> B = Bianco e Nero C = Colore
        //                        csv.WriteField("");                       //Tipologia_Fatturazione --> R = Recupero IVA N = No Recupero IVA
        //                        csv.WriteField("");                       //Centro_Costo --> 
        //                        csv.NextRecord();
                                
        //                        writer.Flush();
        //                    }
        //                }
        //            }

        //            pathReturn = pathTemplateFileIndice + codiceAccertamento + @"\" + newFileName;
        //            #endregion
        //            break;
        //    }
            
        //    return pathReturn;
        //}
                
        //public static void SetDocumentiRaccomandandateNexive(long iD, bool fFileDepositatoNexive, int typeofRaccomandata)
        //{
        //    switch (typeofRaccomandata)
        //    {
        //        case 1: //Accertamento
        //            #region Accertamento
        //            var documenti = UtilityVerifiche.GetDocumentiAccertamenti(iD);
        //            using (var ctx = new CriterDataModel())
        //            {
        //                foreach (var documento in documenti)
        //                {
        //                    var raccomandata = ctx.VER_AccertamentoDocumento.Where(c => c.IDAccertamentoDocumento == documento.IDAccertamentoDocumento).FirstOrDefault();
        //                    raccomandata.fFileDepositatoNexive = fFileDepositatoNexive;
        //                    if (fFileDepositatoNexive)
        //                    {
        //                        raccomandata.DataDepositoFileNexive = DateTime.Now;
        //                    }
        //                    else
        //                    {
        //                        raccomandata.DataDepositoFileNexive = null;
        //                    }
        //                    raccomandata.fRaccomandataInviata = false;
        //                    ctx.SaveChanges();
        //                }
        //            }
        //            #endregion
        //            break;
        //        case 2: //Revoca
        //            #region Revoca

        //            #endregion
        //            break;
        //        case 3: //Conferma Pianificazione Ispezione
        //        case 4: //Annullamento Pianificazione ispezione
        //        case 5: //Ripianificazione ispezione
        //            #region Conferma Pianificazione Ispezione
        //            using (var ctx = new CriterDataModel())
        //            {
        //                var notifica = new VER_IspezioneNotificaNexive();
        //                notifica.IDIspezione = iD;
        //                notifica.TypeOfDocument = typeofRaccomandata;
        //                notifica.DescriptionTypeOfDocument = UtilityApp.GetEnumDescription(typeofRaccomandata);
        //                notifica.fFileDepositatoNexive = true;
        //                notifica.DataDepositoFileNexive = DateTime.Now;
                        
        //                ctx.VER_IspezioneNotificaNexive.Add(notifica);
        //                ctx.SaveChanges();
        //            }
        //            #endregion
        //            break;
        //        case 6: //Sanzione
        //            #region Sanzione
        //            using (var ctx = new CriterDataModel())
        //            {
        //                var sanzione = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iD).FirstOrDefault();
        //                sanzione.DataInvioSanzione = DateTime.Now;
        //                ctx.SaveChanges();
        //            }
        //            #endregion
        //            break;
        //    }
        //}

        //public static void SetEsitiRaccomandandateNexive(long iD, int iDProceduraAccertamento, int typeofRaccomandata,
        //                                                 string barcode, string servizio, string dataAccettazione, string filialeAccettazione,
        //                                                 string dataRecapito, string dataPostalizzazione, string dataReso,
        //                                                 string causale, string latitudine, string longitudine)
        //{
        //    using (var ctx = new CriterDataModel())
        //    {
        //        switch (typeofRaccomandata)
        //        {
        //            case 1: //Accertamento
        //                #region Documenti Accertamenti
        //                var esitoDocumento = ctx.VER_AccertamentoDocumento.Where(c => c.IDAccertamento == iD && c.IDProceduraAccertamento == iDProceduraAccertamento).FirstOrDefault();
        //                if (esitoDocumento != null)
        //                {
        //                    if (!string.IsNullOrEmpty(barcode))
        //                    {
        //                        esitoDocumento.Barcode = barcode;
        //                    }
        //                    if (!string.IsNullOrEmpty(servizio))
        //                    {
        //                        esitoDocumento.Servizio = servizio;
        //                    }
        //                    DateTime dataAccettazioneF;
        //                    if (DateTime.TryParse(dataAccettazione, out dataAccettazioneF))
        //                    {
        //                        esitoDocumento.DataAccettazione = dataAccettazioneF;
        //                    }
        //                    if (!string.IsNullOrEmpty(filialeAccettazione))
        //                    {
        //                        esitoDocumento.FilialeAccettazione = filialeAccettazione;
        //                    }
        //                    DateTime dataRecapitoF;
        //                    if (DateTime.TryParse(dataRecapito, out dataRecapitoF))
        //                    {
        //                        esitoDocumento.DataRecapito = dataRecapitoF;
        //                    }
        //                    DateTime dataPostalizzazioneF;
        //                    if (DateTime.TryParse(dataPostalizzazione, out dataPostalizzazioneF))
        //                    {
        //                        esitoDocumento.DataPostalizzazione = dataPostalizzazioneF;
        //                    }
        //                    DateTime dataResoF;
        //                    if (DateTime.TryParse(dataReso, out dataResoF))
        //                    {
        //                        esitoDocumento.DataReso = dataResoF;
        //                    }
        //                    switch (causale)
        //                    {
        //                        case ("Assente inizio giacenza"):
        //                            esitoDocumento.IDCausale = 1;
        //                            break;
        //                        case ("Calamità naturali"):
        //                            esitoDocumento.IDCausale = 2;
        //                            break;
        //                        case ("Casella bancaria"):
        //                            esitoDocumento.IDCausale = 3;
        //                            break;
        //                        case ("Casella postale"):
        //                            esitoDocumento.IDCausale = 4;
        //                            break;
        //                        case ("Cessata attività"):
        //                            esitoDocumento.IDCausale = 5;
        //                            break;
        //                        case ("Chiuso per ferie "):
        //                            esitoDocumento.IDCausale = 6;
        //                            break;
        //                        case ("Codice cliente illeggibile"):
        //                            esitoDocumento.IDCausale = 7;
        //                            break;
        //                        case ("Compiuta giacenza"):
        //                            esitoDocumento.IDCausale = 8;
        //                            break;
        //                        case ("Deceduto"):
        //                            esitoDocumento.IDCausale = 9;
        //                            break;
        //                        case ("Fattorino: mancato appuntamento"):
        //                            esitoDocumento.IDCausale = 10;
        //                            break;
        //                        case ("Fermo posta festività"):
        //                            esitoDocumento.IDCausale = 11;
        //                            break;
        //                        case ("Indirizzo errato"):
        //                            esitoDocumento.IDCausale = 12;
        //                            break;
        //                        case ("Indirizzo insufficiente"):
        //                            esitoDocumento.IDCausale = 13;
        //                            break;
        //                        case ("Non postalizzata: restituita"):
        //                            esitoDocumento.IDCausale = 14;
        //                            break;
        //                        case ("Non certificata"):
        //                            esitoDocumento.IDCausale = 15;
        //                            break;
        //                        case ("Respinto"):
        //                            esitoDocumento.IDCausale = 16;
        //                            break;
        //                        case ("Ritirata dal destinatario"):
        //                        case ("\0"):
        //                            esitoDocumento.IDCausale = 17;
        //                            esitoDocumento.fRaccomandataInviata = true;
        //                            break;
        //                        case ("Ritiro digitale"):
        //                            esitoDocumento.IDCausale = 18;
        //                            break;
        //                        case ("Sconosciuto"):
        //                            esitoDocumento.IDCausale = 19;
        //                            break;
        //                        case ("Stabile demolito"):
        //                            esitoDocumento.IDCausale = 20;
        //                            break;
        //                        case ("Stabile inaccessibile"):
        //                            esitoDocumento.IDCausale = 21;
        //                            break;
        //                        case ("Trasferito"):
        //                            esitoDocumento.IDCausale = 22;
        //                            break;
        //                        case ("Zone non servite posta"):
        //                            esitoDocumento.IDCausale = 23;
        //                            break;
        //                        default:

        //                            break;
        //                    }

        //                    decimal latitudineF;
        //                    if (decimal.TryParse(latitudine.Replace(".", ","), out latitudineF))
        //                    {
        //                        esitoDocumento.Latitudine = latitudineF;
        //                    }
        //                    decimal longitudineF;
        //                    if (decimal.TryParse(longitudine.Replace(".", ","), out longitudineF))
        //                    {
        //                        esitoDocumento.Longitudine = longitudineF;
        //                    }

        //                    ctx.SaveChanges();
        //                }
        //                #endregion
        //                break;
        //            case 2: //Revoca
        //                #region Revoca Interventi
                        
        //                #endregion
        //                break;
        //            case 3: //Conferma Pianificazione Ispezione
        //            case 4: //Annullamento Pianificazione ispezione
        //            case 5: //Ripianificazione ispezione
        //                #region Notifiche Ispezioni
        //                var esitoDocumentoIspezione = ctx.VER_IspezioneNotificaNexive.Where(c => c.IDIspezione == iD && c.TypeOfDocument == typeofRaccomandata).FirstOrDefault();
        //                if (esitoDocumentoIspezione != null)
        //                {
        //                    if (!string.IsNullOrEmpty(barcode))
        //                    {
        //                        esitoDocumentoIspezione.Barcode = barcode;
        //                    }
        //                    if (!string.IsNullOrEmpty(servizio))
        //                    {
        //                        esitoDocumentoIspezione.Servizio = servizio;
        //                    }
        //                    DateTime dataAccettazioneI;
        //                    if (DateTime.TryParse(dataAccettazione, out dataAccettazioneI))
        //                    {
        //                        esitoDocumentoIspezione.DataAccettazione = dataAccettazioneI;
        //                    }
        //                    if (!string.IsNullOrEmpty(filialeAccettazione))
        //                    {
        //                        esitoDocumentoIspezione.FilialeAccettazione = filialeAccettazione;
        //                    }
        //                    DateTime dataRecapitoI;
        //                    if (DateTime.TryParse(dataRecapito, out dataRecapitoI))
        //                    {
        //                        esitoDocumentoIspezione.DataRecapito = dataRecapitoI;
        //                    }
        //                    DateTime dataPostalizzazioneI;
        //                    if (DateTime.TryParse(dataPostalizzazione, out dataPostalizzazioneI))
        //                    {
        //                        esitoDocumentoIspezione.DataPostalizzazione = dataPostalizzazioneI;
        //                    }
        //                    DateTime dataResoI;
        //                    if (DateTime.TryParse(dataReso, out dataResoI))
        //                    {
        //                        esitoDocumentoIspezione.DataReso = dataResoI;
        //                    }
        //                    switch (causale)
        //                    {
        //                        case ("Assente inizio giacenza"):
        //                            esitoDocumentoIspezione.IDCausale = 1;
        //                            break;
        //                        case ("Calamità naturali"):
        //                            esitoDocumentoIspezione.IDCausale = 2;
        //                            break;
        //                        case ("Casella bancaria"):
        //                            esitoDocumentoIspezione.IDCausale = 3;
        //                            break;
        //                        case ("Casella postale"):
        //                            esitoDocumentoIspezione.IDCausale = 4;
        //                            break;
        //                        case ("Cessata attività"):
        //                            esitoDocumentoIspezione.IDCausale = 5;
        //                            break;
        //                        case ("Chiuso per ferie "):
        //                            esitoDocumentoIspezione.IDCausale = 6;
        //                            break;
        //                        case ("Codice cliente illeggibile"):
        //                            esitoDocumentoIspezione.IDCausale = 7;
        //                            break;
        //                        case ("Compiuta giacenza"):
        //                            esitoDocumentoIspezione.IDCausale = 8;
        //                            break;
        //                        case ("Deceduto"):
        //                            esitoDocumentoIspezione.IDCausale = 9;
        //                            break;
        //                        case ("Fattorino: mancato appuntamento"):
        //                            esitoDocumentoIspezione.IDCausale = 10;
        //                            break;
        //                        case ("Fermo posta festività"):
        //                            esitoDocumentoIspezione.IDCausale = 11;
        //                            break;
        //                        case ("Indirizzo errato"):
        //                            esitoDocumentoIspezione.IDCausale = 12;
        //                            break;
        //                        case ("Indirizzo insufficiente"):
        //                            esitoDocumentoIspezione.IDCausale = 13;
        //                            break;
        //                        case ("Non postalizzata: restituita"):
        //                            esitoDocumentoIspezione.IDCausale = 14;
        //                            break;
        //                        case ("Non certificata"):
        //                            esitoDocumentoIspezione.IDCausale = 15;
        //                            break;
        //                        case ("Respinto"):
        //                            esitoDocumentoIspezione.IDCausale = 16;
        //                            break;
        //                        case ("Ritirata dal destinatario"):
        //                        case ("\0"):
        //                            esitoDocumentoIspezione.IDCausale = 17;
        //                            esitoDocumentoIspezione.fRaccomandataInviata = true;
        //                            break;
        //                        case ("Ritiro digitale"):
        //                            esitoDocumentoIspezione.IDCausale = 18;
        //                            break;
        //                        case ("Sconosciuto"):
        //                            esitoDocumentoIspezione.IDCausale = 19;
        //                            break;
        //                        case ("Stabile demolito"):
        //                            esitoDocumentoIspezione.IDCausale = 20;
        //                            break;
        //                        case ("Stabile inaccessibile"):
        //                            esitoDocumentoIspezione.IDCausale = 21;
        //                            break;
        //                        case ("Trasferito"):
        //                            esitoDocumentoIspezione.IDCausale = 22;
        //                            break;
        //                        case ("Zone non servite posta"):
        //                            esitoDocumentoIspezione.IDCausale = 23;
        //                            break;
        //                        default:

        //                            break;
        //                    }

        //                    decimal latitudineI;
        //                    if (decimal.TryParse(latitudine.Replace(".", ","), out latitudineI))
        //                    {
        //                        esitoDocumentoIspezione.Latitudine = latitudineI;
        //                    }
        //                    decimal longitudineI;
        //                    if (decimal.TryParse(longitudine.Replace(".", ","), out longitudineI))
        //                    {
        //                        esitoDocumentoIspezione.Longitudine = longitudineI;
        //                    }

        //                    ctx.SaveChanges();
        //                }
        //                #endregion
        //                break;
        //            case 6: //Sanzione
        //                #region Sanzione
        //                UtilityVerifiche.SetDataRicevimentoRaccomandataSanzione(iD, dataRecapito);

        //                //var sanzione = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iD).FirstOrDefault();

        //                //DateTime dataRecapitoI;
        //                //if (DateTime.TryParse(dataRecapito, out dataRecapitoI))
        //                //{
        //                //    sanzione.DataRicezioneSanzione = dataRecapitoI;
        //                //}
        //                //else
        //                //{
        //                //    sanzione.DataRicezioneSanzione = DateTime.Now;
        //                //}
                                                
        //                //ctx.SaveChanges();
        //                #endregion
        //                break;
        //        }
        //    }
        //}

    }
}

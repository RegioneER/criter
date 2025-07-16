using DataLayer;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace DataUtilityCore
{
    public static class UtilityFileSystem
    {
        private static object lockObj = new Object();
                
        public static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                lock (lockObj)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
            }
        }
                
        public static string GetFileFormatSize(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        public enum TipoRichiesta
        {
            PathCertificatoPdf, //PDF generato
            PathCertificatoPdfP7M, //PDF FIRMATO DAL CERTIFICATORE
            PathCertificatoXml, //XML GENERATO DA SACE
            PathCertificatoXmlP7M, //XML FIRMATO
            PathCertificatoXmlImportato, //XML IMPORTATO DAI SW DI CALCOLO
            DirectoryTemp, //Directory di lavoro generica per salvare il pdf
            PathEsitoRegoleControlliCertificato //TODO bisognerà gestire anche questo
        }

        public static string GetFileNameCertificatoPdf(string IDCertificato, string IDSoggetto)
        {
            return string.Format("Rapporto_{0}_{1}.pdf", IDSoggetto, IDCertificato);
        }

        public static string GetFileNameCertificatoPdfP7M(string IDCertificato, string IDSoggetto)
        {
            return string.Format("Rapporto_{0}_{1}.p7m", IDSoggetto, IDCertificato);
        }

        public static string GetFileNameCertificatoXml(string IDCertificato)
        {
            return string.Format("SaCeCertificato_{0}.xml", IDCertificato);
        }

        public static string GetFileNameCertificatoXmlP7M(string IDCertificato)
        {
            return string.Format("SaCeCertificato_{0}.p7m", IDCertificato);
        }

        public static string GetFileNameCertificatoXmlImportato(string IDCertificato)
        {
            return string.Format("SaCeXmlImport_{0}.xml", IDCertificato);
        }

        public static string GetPathSalvataggioDirectoryTemp()
        {
            return Path.Combine(UtilityConfig.PathSalvataggioDati, @"TEMP\");
        }

        public static string GetPathSalvataggioGenerico(string IDCertificato, TipoRichiesta tipoRichiesta)
        {
            string link; //NON MI SERVE in questo overload
            return GetPathSalvataggioGenerico(int.Parse(IDCertificato), tipoRichiesta, out link);
        }

        public static string GetPathSalvataggioGenerico(string IDCertificato, TipoRichiesta tipoRichiesta, out string link)
        {
            return GetPathSalvataggioGenerico(int.Parse(IDCertificato), tipoRichiesta, out link);
        }

        public static string GetPathSalvataggioGenerico(int IDCertificato, TipoRichiesta tipoRichiesta)
        {
            string link; //NON MI SERVE in questo overload
            return GetPathSalvataggioGenerico(IDCertificato, tipoRichiesta, out link);
        }

        public static string GetPathSalvataggioGenerico(long IDRapporto, TipoRichiesta tipoRichiesta, out string link)
        {
            //ritorna il path di salvataggio in base alla DataInserimento del rapporto - verificare che non sia null in alcuni certificati

            using (var ctx = DataLayer.Common.ApplicationContext.Current.Context)
            {
                var rapporto =
                    ctx.RCT_RapportoDiControlloTecnicoBase.Where(c => c.IDRapportoControlloTecnico == IDRapporto).Select(c => new
                    {
                        c.IDRapportoControlloTecnico,
                        c.LIM_LibrettiImpianti.DataInserimento,
                        c.LIM_LibrettiImpianti.IDSoggetto,
                        //c.IDSoggetto,
                        //c.VersioneCertificato,
                        //c.DataInserimento
                    }).FirstOrDefault();

                if (rapporto != null)
                {

                    //verifico la versione
                    //if (rapporto.VersioneCertificato < 2)
                    //{

                    var path = GetPathSalvataggioGenericoV2(IDRapporto, rapporto.IDSoggetto,
                        rapporto.DataInserimento, tipoRichiesta, out link);

                    //Quando ci sarà la migrazione dei file..alcuni saranno già spostati nelle nuove sottocartelle mentre altri no
                    //Per questo verifico se il file esiste per la NUOVA versione, altrimenti se non esiste lo cerco nella vecchia cartella
                    //Per la directory TEMP o altri nuovi path non faccio questa verifica, ma userà direttamente la V2
                    //La directory TEMP viene usata quando l'utente carica il pdf firmato
                    if ((tipoRichiesta == TipoRichiesta.PathCertificatoPdf
                        || tipoRichiesta == TipoRichiesta.PathCertificatoPdfP7M
                        || tipoRichiesta == TipoRichiesta.PathCertificatoXml
                        || tipoRichiesta == TipoRichiesta.PathCertificatoXmlP7M
                        || tipoRichiesta == TipoRichiesta.PathEsitoRegoleControlliCertificato
                        ) && !File.Exists(path))
                    {
                        path = GetPathSalvataggioGenerico(IDRapporto, rapporto.IDSoggetto, tipoRichiesta, out link);
                    }

                    return path;
                    //}
                    //else
                    //{
                    //    var path = GetPathSalvataggioGenericoV2(IDRapporto, rapporto.IDSoggetto.Value, rapporto.DataInserimento, tipoRichiesta, out link);

                    //    return path;
                    //}


                }
                else
                    throw new Exception("Rapporto non trovato. Non dovrebbe mai succedere.");

            }

        }



        //Per la vecchia versione..
        private static string GetPathSalvataggioGenerico(long IDCertificato, int? IDSoggetto, TipoRichiesta tipoRichiesta,
            out string link)
        {
            string fileName = string.Empty;

            switch (tipoRichiesta)
            {
                case TipoRichiesta.DirectoryTemp:
                    link = string.Empty; //NON USATO
                    return UtilityConfig.UploadRapportiControllo;
                    break;
                case TipoRichiesta.PathCertificatoPdf:
                    fileName = GetFileNameCertificatoPdf(IDCertificato.ToString(), IDSoggetto.ToString());
                    link = CombineUrl(UtilityConfig.LinkRapporti, fileName);
                    return Path.Combine(UtilityConfig.UploadRapportiControllo, fileName);
                    break;
                case TipoRichiesta.PathCertificatoPdfP7M:
                    fileName = GetFileNameCertificatoPdfP7M(IDCertificato.ToString(), IDSoggetto.ToString());
                    link = CombineUrl(UtilityConfig.LinkRapporti, fileName);
                    return Path.Combine(UtilityConfig.UploadRapportiControllo, fileName);
                    break;
                case TipoRichiesta.PathCertificatoXml:
                    fileName = GetFileNameCertificatoXml(IDCertificato.ToString());
                    link = string.Empty; //QUI FORSE NON è usato
                    return Path.Combine(UtilityConfig.PathXmlFile, fileName);
                    break;
                case TipoRichiesta.PathCertificatoXmlP7M:
                    fileName = GetFileNameCertificatoXmlP7M(IDCertificato.ToString());
                    link = CombineUrl(UtilityConfig.LinkP7m, fileName);
                    return Path.Combine(UtilityConfig.PathP7mFile, fileName);
                    break;
                case TipoRichiesta.PathEsitoRegoleControlliCertificato:
                    fileName = string.Format("EsitoRegoleControlliRapporto_{0}.pdf", IDCertificato);
                    link = CombineUrl(UtilityConfig.LinkRapporti, fileName);
                    return Path.Combine(UtilityConfig.UploadRapportiControllo, fileName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("tipoRichiesta", tipoRichiesta, null);
            }


        }


        //per la nuova versione
        private static string GetPathSalvataggioGenericoV2(long IDRapporto, int? IDSoggetto, DateTime? dataInserimento, TipoRichiesta tipoRichiesta, out string link)
        {
            if (!dataInserimento.HasValue)
                throw new Exception("DataInserimento è null e non dovrebbe succedere!");

            var pathGenericoConAnnoMese = Path.Combine(UtilityConfig.PathSalvataggioDati, DateTime.Today.ToString("yyyy-MM"));
            var linkGenericoConAnnoMese = CombineUrl(UtilityConfig.LinkSalvataggioDati, DateTime.Today.ToString("yyyy-MM"));


            //CreateDirectoryIfNotExists(pathGenericoConAnnoMese);
            //CreateDirectoryIfNotExists(Path.Combine(pathGenericoConAnnoMese, "PDF"));
            //CreateDirectoryIfNotExists(Path.Combine(pathGenericoConAnnoMese, "PDF-P7M"));
            //CreateDirectoryIfNotExists(Path.Combine(pathGenericoConAnnoMese, "XML"));
            //CreateDirectoryIfNotExists(Path.Combine(pathGenericoConAnnoMese, "XML-P7M"));
            //CreateDirectoryIfNotExists(Path.Combine(pathGenericoConAnnoMese, "EsitoControlli"));
            //CreateDirectoryIfNotExists(Path.Combine(pathGenericoConAnnoMese, "XML-Import"));

            ////Se non esiste la directory la creo
            //if (!Directory.Exists(pathGenericoConAnnoMese))
            //{
            //    lock (lockObj)
            //    {
            //        if (!Directory.Exists(pathGenericoConAnnoMese))
            //        {
            //            //TODO FORSE CONVIENE CREARLE TUTTE INSIEME nel global.asax periodicamente per evitare problemi di concorrenza
            //            Directory.CreateDirectory(pathGenericoConAnnoMese);
            //            //Creo anche tutte le altre sotto-directory già che ci sono
            //            Directory.CreateDirectory(Path.Combine(pathGenericoConAnnoMese, "PDF"));
            //            Directory.CreateDirectory(Path.Combine(pathGenericoConAnnoMese, "PDF-P7M"));
            //            //Directory.CreateDirectory(Path.Combine(pathGenericoConAnnoMese, "XML"));
            //            //Directory.CreateDirectory(Path.Combine(pathGenericoConAnnoMese, "XML-P7M"));
            //            Directory.CreateDirectory(Path.Combine(pathGenericoConAnnoMese, "EsitoControlli"));
            //            //Directory.CreateDirectory(Path.Combine(pathGenericoConAnnoMese, "XML-Import"));
            //        }
            //    }
            //}
            //if (!Directory.Exists(Path.Combine(UtilityConfig.PathSalvataggioDati, "TEMP")))
            //{
            //    Directory.CreateDirectory(Path.Combine(UtilityConfig.PathSalvataggioDati, "TEMP"));
            //    //NON DIVISA PER MESE/ANNO
            //}

            var fileName = string.Empty;

            switch (tipoRichiesta)
            {
                case TipoRichiesta.DirectoryTemp:
                    link = string.Empty; //NON USATO
                    return Path.Combine(UtilityConfig.PathSalvataggioDati, @"TEMP\");
                    //return GetPathSalvataggioDirectoryTemp();
                    break;
                case TipoRichiesta.PathCertificatoPdf:
                    fileName = GetFileNameCertificatoPdf(IDRapporto.ToString(), IDSoggetto.ToString());
                    link = CombineUrl(linkGenericoConAnnoMese, "PDF", fileName);
                    return Path.Combine(pathGenericoConAnnoMese, "PDF", fileName);
                    break;
                case TipoRichiesta.PathCertificatoPdfP7M:
                    fileName = GetFileNameCertificatoPdfP7M(IDRapporto.ToString(), IDSoggetto.ToString());
                    //stessa directory del pdf
                    link = CombineUrl(linkGenericoConAnnoMese, "PDF-P7M", fileName);
                    return Path.Combine(pathGenericoConAnnoMese, "PDF-P7M", fileName);
                    break;
                case TipoRichiesta.PathCertificatoXml:
                    fileName = GetFileNameCertificatoXml(IDRapporto.ToString());
                    link = string.Empty; //QUI FORSE NON è usato
                    return Path.Combine(pathGenericoConAnnoMese, "XML", fileName);
                    break;
                case TipoRichiesta.PathCertificatoXmlP7M:
                    fileName = GetFileNameCertificatoXmlP7M(IDRapporto.ToString());
                    link = CombineUrl(linkGenericoConAnnoMese, "XML-P7M", fileName);
                    return Path.Combine(pathGenericoConAnnoMese, "XML-P7M", fileName);
                    break;
                case TipoRichiesta.PathEsitoRegoleControlliCertificato:
                    fileName = string.Format("EsitoRegoleControlliRapporto_{0}.pdf", IDRapporto);
                    link = CombineUrl(linkGenericoConAnnoMese, "EsitoControlli", fileName);
                    return Path.Combine(pathGenericoConAnnoMese, "EsitoControlli", fileName);
                    break;
                case TipoRichiesta.PathCertificatoXmlImportato:
                    fileName = GetFileNameCertificatoXmlImportato(IDRapporto.ToString());
                    link = string.Empty; //QUI FORSE NON è usato
                    return Path.Combine(pathGenericoConAnnoMese, "XML-Import", fileName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("tipoRichiesta", tipoRichiesta, null);
            }
        }

        private static string CombineUrl(string baseUrl, string relativeUrl)
        {
            if (!string.IsNullOrEmpty(relativeUrl))
            {
                //Metto lo slash finale se non c'è
                if (!baseUrl.EndsWith("/"))
                    baseUrl = String.Concat(baseUrl, "/");

                return string.Format("{0}{1}", baseUrl, relativeUrl);
            }
            else
            {
                return baseUrl;
            }
        }

        private static string CombineUrl(string baseUrl, params string[] urls)
        {
            string result = baseUrl;
            foreach (var url in urls)
            {
                result = CombineUrl(result, url);
            }
            return result;
        }

        public static string GetPathDocumentoIspezione(int IDIspezione, int IDTipoDocumentoIspezione, string estensione, out string link)
        {
            return link = "";
            //    using (var ctx = DataLayer.Common.ApplicationContext.Current.Context)
            //    {
            //        var ispezione =
            //            ctx.VER_Ispezione.Where(c => c.IDIspezione == IDIspezione).Select(c => new
            //            {
            //                c.IDIspezione,
            //                c.VER_Accertamento.RCT_RapportoDiControlloTecnicoBase.Id,
            //                DataInserimentoCertificato = c.VER_Accertamento.RCT_RapportoDiControlloTecnicoBase.DataControllo,
            //                Codice = c.VER_Accertamento.Codice
            //            }).FirstOrDefault();

            //        if (ispezione != null)
            //        {
            //            if (!ispezione.DataInserimentoCertificato.HasValue)
            //                throw new Exception("DataInserimento è null e non dovrebbe succedere!");

            //            var pathGenericoConAnnoMese = Path.Combine(UtilityConfig.PathSalvataggioDati, ispezione.DataInserimentoCertificato.Value.ToString("yyyy-MM"));
            //            var linkGenericoConAnnoMese = CombineUrl(UtilityConfig.LinkSalvataggioDati, ispezione.DataInserimentoCertificato.Value.ToString("yyyy-MM"));

            //            var tipodocumento =
            //                ctx.SYS_TipoDocumentoIspezione.Where(c => c.IDTipoDocumentoIspezione == IDTipoDocumentoIspezione).Select(c => new
            //                {
            //                    c.IDTipoDocumentoIspezione,
            //                    c.NomeFileUpload,
            //                }).FirstOrDefault();

            //            if (tipodocumento != null)
            //            {
            //                //Costruzione del nome del file (solo nome), l'estensione dipende dal file caricato
            //                string fileName = string.Format(tipodocumento.NomeFileUpload, ispezione.Codice);
            //                //, ispezione.IDIspezione, ispezione.IDCertificato, tipodocumento.IDTipoDocumentoIspezione);

            //                link = CombineUrl(linkGenericoConAnnoMese, "EsitoControlli", fileName + estensione);
            //                return Path.Combine(pathGenericoConAnnoMese, "EsitoControlli", fileName + estensione);
            //            }
            //            else
            //                throw new Exception("Tipo documento ispezione non trovato. Non dovrebbe mai succedere.");
            //        }
            //        else
            //            throw new Exception("Ispezione non trovata. Non dovrebbe mai succedere.");
            //    }
        }

    }
}

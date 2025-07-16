using System;
using System.Configuration;
using System.Linq;
using System.IO;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;
using System.Net;
using iTextSharp.text.pdf;
using DataLayer;
using System.Collections.Generic;
using iTextSharp.text;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DataUtilityCore
{
    public class ReportingServices
    {
        public static string GetPdfPath(string IDRapporto, string stq, string reportName, string reportUrl, string reportPath, out string link, string destinationFile)
        {
            //Quando chiamo questo metodo, voglio comunque generare il PDF
            string pathPdf = UtilityFileSystem.GetPathSalvataggioGenerico(IDRapporto, UtilityFileSystem.TipoRichiesta.PathCertificatoPdf, out link);


            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }
            

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDRapporto", IDRapporto);

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

            Warning[] warnings = null;
            String[] streams = null;
            string name = String.Empty;
            string extension = String.Empty;
            string mime = String.Empty;
            string encoding = String.Empty;

            Byte[] ilReport = rview.ServerReport.Render("PDF", null, out mime, out encoding, out extension, out streams, out warnings);
            string fileName = "Rapporto" + "_" + stq + "_" + IDRapporto + "." + extension;

            pathPdf = destinationFile + @"\" + fileName;

            using (FileStream fs = new FileStream(pathPdf, FileMode.Create))
            {
                fs.Write(ilReport, 0, ilReport.Length);
                fs.Flush();
                fs.Close();
                fs.Dispose();
            }

            return pathPdf;
        }

        public static string GetTargaturaImpiantoReport(string strTargaturaImpianto, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSiteTargaturaImpiantoReport)
        {
            string pathTargaturaImpiantoReport = "";
            ReportParameter rp0;
            ReportParameter rp1;

            ReportViewer rview = new ReportViewer();
            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);

            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            //ReportParameter[] parameters = new ReportParameter[1];
            //parameters[0] = new ReportParameter("StrTargaturaImpianto", strTargaturaImpianto);
            //rview.ServerReport.SetParameters(parameters);
            rp0 = new ReportParameter("StrTargaturaImpianto", strTargaturaImpianto);
            rp1 = new ReportParameter("CriterUrl", urlSiteTargaturaImpiantoReport + destinationFile + "/");

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0, rp1 });

            Warning[] warnings = null;
            string[] streams = null;
            string format = "PDF";
            string name = String.Empty;
            string extension = String.Empty;
            string mime = String.Empty;
            string encoding = String.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);

            string fileName = reportName + "_" + Guid.NewGuid().ToString() + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            pathTargaturaImpiantoReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

            using (FileStream fs = new FileStream(pathTargaturaImpiantoReport, FileMode.Create))
            {
                fs.Write(ilReport, 0, ilReport.Length);
                fs.Flush();
                fs.Close();
            }

            return urlSiteTargaturaImpiantoReport + destinationFile + "/" + fileName;
        }

        public static string GetBolliniReport(string StrBollinoCalorePulito, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSiteBolliniReport)
        {
            string pathBolliniReport = "";
            ReportParameter rp0;
            ReportParameter rp1;

            ReportViewer rview = new ReportViewer();
            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);

            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            rp0 = new ReportParameter("StrBollinoCalorePulito", StrBollinoCalorePulito);
            rp1 = new ReportParameter("CriterUrl", urlSiteBolliniReport + destinationFile + "/");

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0, rp1 });

            Warning[] warnings = null;
            string[] streams = null;
            string format = "PDF";
            string name = String.Empty;
            string extension = String.Empty;
            string mime = String.Empty;
            string encoding = String.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);

            string fileName = reportName + "_" + Guid.NewGuid().ToString() + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            pathBolliniReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

            using (FileStream fs = new FileStream(pathBolliniReport, FileMode.Create))
            {
                fs.Write(ilReport, 0, ilReport.Length);
                fs.Flush();
                fs.Close();
            }

            return urlSiteBolliniReport + destinationFile + "/" + fileName;
        }

        public static string GetLibrettoImpiantoReport(string IDLibrettoImpianto, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            string pathLibrettoImpiantoReport = "";

            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDLibrettoImpianto", IDLibrettoImpianto);

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            String[] streams = null;
            string format = "PDF";
            string name = String.Empty;
            string extension = String.Empty;
            string mime = String.Empty;
            string encoding = String.Empty;

            Byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileName = reportName + "_" + IDLibrettoImpianto + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            pathLibrettoImpiantoReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

            using (FileStream fs = new FileStream(pathLibrettoImpiantoReport, FileMode.Create))
            {
                fs.Write(ilReport, 0, ilReport.Length);
                fs.Flush();
                fs.Close();
            }

            return urlSite + destinationFile + "/" + fileName;
        }

        public static string GetRapportoControlloReport(string IDRapportoControlloTecnico, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            string pathRapportoControlloReport = "";

            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            string CriterUrl = urlSite + ConfigurationManager.AppSettings["UploadBolliniCalorePulito"] + "/";
            ReportParameter rp0 = new ReportParameter("IDRapportoControlloTecnico", IDRapportoControlloTecnico);
            ReportParameter rp1 = new ReportParameter("CriterUrl", CriterUrl);

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0, rp1 });

            Warning[] warnings = null;
            string[] streams = null;
            string format = "PDF";
            string name = string.Empty;
            string extension = string.Empty;
            string mime = string.Empty;
            string encoding = string.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileName = reportName + "_" + IDRapportoControlloTecnico + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            pathRapportoControlloReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

            using (FileStream fs = new FileStream(pathRapportoControlloReport, FileMode.Create))
            {
                fs.Write(ilReport, 0, ilReport.Length);
                fs.Flush();
                fs.Close();
            }

            return urlSite + destinationFile + "/" + fileName;
        }

        public static string GetSchedaIscrizioneReport(string IDSoggetto, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            string pathSchedaIscrizioneReport = "";

            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDSoggetto", IDSoggetto);

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

            Warning[] warnings = null;
            string[] streams = null;
            string format = "PDF";
            string name = string.Empty;
            string extension = string.Empty;
            string mime = string.Empty;
            string encoding = string.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileName = reportName + "_" + IDSoggetto + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            pathSchedaIscrizioneReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

            using (FileStream fs = new FileStream(pathSchedaIscrizioneReport, FileMode.Create))
            {
                fs.Write(ilReport, 0, ilReport.Length);
                fs.Flush();
                fs.Close();
            }

            return urlSite + destinationFile + "/" + fileName;
        }

        public static string GetRicevutaPagamentoReport(string IDMovimento, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            string pathRicevutaPagamentoReport = "";

            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDMovimento", IDMovimento);

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            String[] streams = null;
            string format = "PDF";
            string name = String.Empty;
            string extension = String.Empty;
            string mime = String.Empty;
            string encoding = String.Empty;

            Byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileName = reportName + "_" + IDMovimento + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            pathRicevutaPagamentoReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

            using (FileStream fs = new FileStream(pathRicevutaPagamentoReport, FileMode.Create))
            {
                fs.Write(ilReport, 0, ilReport.Length);
                fs.Flush();
                fs.Close();
            }

            return urlSite + destinationFile + "/" + fileName;
        }

        public static string GetAccertamentiReport(string IDAccertamento, string IDProceduraAccertamento, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            using (var ctx = new CriterDataModel())
            {
                long iDAccertamento = long.Parse(IDAccertamento);
                var accertamento = ctx.VER_Accertamento.Where(a => a.IDAccertamento == iDAccertamento).FirstOrDefault();

                string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
                string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
                string credentialsDomain = string.Empty;

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
                {
                    credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
                }

                ReportViewer rview = new ReportViewer();
                rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
                rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
                rview.ServerReport.ReportPath = reportPath + reportName;

                ReportParameter rp0 = new ReportParameter("IDAccertamento", IDAccertamento);
                ReportParameter rp1 = new ReportParameter("IDProceduraAccertamento", IDProceduraAccertamento);

                rview.ServerReport.SetParameters(new ReportParameter[] { rp0, rp1 });

                Microsoft.Reporting.WebForms.Warning[] warnings = null;
                String[] streams = null;
                string format = "PDF";
                string name = String.Empty;
                string extension = String.Empty;
                string mime = String.Empty;
                string encoding = String.Empty;

                Byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
                string fileName = IDAccertamento + "_" + IDProceduraAccertamento + "." + extension;

                string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

                string guid = Guid.NewGuid().ToString("N").Substring(0, 12);
                string pathTmpAccertamentiReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + "tmp_" + guid + "_" + fileName;
                UtilityFileSystem.CreateDirectoryIfNotExists(ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\");

                using (FileStream fs = new FileStream(pathTmpAccertamentiReport, FileMode.Create))
                {
                    fs.Write(ilReport, 0, ilReport.Length);
                    fs.Flush();
                    fs.Close();
                }

                string pathPdfRapporto = string.Empty;
                switch (accertamento.IDTipoAccertamento)
                {
                    case 1:
                        #region Rapporto di controllo tecnico
                        string reportNameRapporto = ConfigurationManager.AppSettings["ReportNameRapportiControllo"];
                        string destinationRapporto = ConfigurationManager.AppSettings["UploadRapportiControllo"];
                        string urlPdf = GetRapportoControlloReport(accertamento.IDRapportoDiControlloTecnicoBase.ToString(), reportNameRapporto, reportUrl, reportPath, destinationRapporto, urlSite);
                        pathPdfRapporto = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadRapportiControllo"] + @"\" + "RapportoControllo_" + accertamento.IDRapportoDiControlloTecnicoBase.ToString() + ".pdf";
                        #endregion
                        break;
                    case 2:
                        #region Rapporto di Ispezione
                        string reportNameRapportoIspezione = "RapportoIspezione";
                        long iDIspezioneVisita = UtilityVerifiche.GetIDIspezioneVisitaFromVerifica(long.Parse(accertamento.IDIspezione.ToString()));
                        string codiceIspezione = UtilityVerifiche.GetCodiceIspezioneFromIDIspezione(long.Parse(accertamento.IDIspezione.ToString()));
                        string estensione = "PDF";
                        
                        string destinationRapportoIspezione = ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + iDIspezioneVisita + @"\" + codiceIspezione;
                        string urlPdfRapportoIspezione = GetIspezioneDocumentiReport(accertamento.IDIspezione.ToString(), reportNameRapportoIspezione, reportUrl, reportPath, destinationRapportoIspezione, urlSite, estensione);

                        pathPdfRapporto = ConfigurationManager.AppSettings["PathDocument"] + destinationRapportoIspezione + @"\" + "RapportoIspezione_" + accertamento.IDIspezione.ToString() + ".pdf";
                        #endregion
                        break;
                }

                string[] filesPath = new string[] { pathTmpAccertamentiReport, pathPdfRapporto };
                string pathPdfOutMergeFile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + "merge_" + fileName;
                MergePdf(filesPath, pathPdfOutMergeFile);

                string pathPdfAccertamentiReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;
                SetPdfVersion(pathPdfOutMergeFile, pathPdfAccertamentiReport);

                try
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    File.Delete(pathPdfOutMergeFile);
                    File.Delete(pathTmpAccertamentiReport);
                }
                catch (Exception ex)
                {

                }

                return urlSite + destinationFile + "/" + fileName;
            }
        }

        public static string GetAccertamentiNexiveReport(string IDAccertamento, string IDProceduraAccertamento, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            using (var ctx = new CriterDataModel())
            {
                string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
                string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
                string credentialsDomain = string.Empty;

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
                {
                    credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
                }

                ReportViewer rview = new ReportViewer();
                rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
                rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
                rview.ServerReport.ReportPath = reportPath + reportName;

                ReportParameter rp0 = new ReportParameter("IDAccertamento", IDAccertamento);
                //ReportParameter rp1 = new ReportParameter("IDProceduraAccertamento", IDProceduraAccertamento);

                rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

                Microsoft.Reporting.WebForms.Warning[] warnings = null;
                String[] streams = null;
                string format = "PDF";
                string name = String.Empty;
                string extension = String.Empty;
                string mime = String.Empty;
                string encoding = String.Empty;

                Byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
                string fileFronteSpizio = "FrontespizioNexive_" + IDAccertamento + "_" + IDProceduraAccertamento + "." + extension;
                string fileAccertamento = IDAccertamento + "_" + IDProceduraAccertamento + "." + extension;
                string fileNexive = "Nexive_" + IDAccertamento + "_" + IDProceduraAccertamento + "." + extension;

                string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
                string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

                string pathFronteSpizioReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileFronteSpizio;
                string pathAccertamentoReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileAccertamento;

                try
                {
                    using (FileStream fs = new FileStream(pathFronteSpizioReport, FileMode.Create))
                    {
                        fs.Write(ilReport, 0, ilReport.Length);
                        fs.Flush();
                        fs.Close();
                    }

                    string[] filesPath = new string[] { pathFronteSpizioReport, pathAccertamentoReport };
                    string pathPdfOutMergeFile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileNexive;
                    MergePdf(filesPath, pathPdfOutMergeFile);

                    string pathPdfAccertamentiReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileNexive;
                    SetPdfVersion(pathPdfOutMergeFile, pathPdfAccertamentiReport);
                    
                    if (File.Exists(ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileFronteSpizio))
                    {
                        try
                        {
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            File.Delete(ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileFronteSpizio);
                        }
                        catch (System.IO.IOException ex)
                        {

                        }
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    // Insert some logic here
                }
                catch (FileNotFoundException ex)
                {
                    // Insert some logic here
                }
                catch (IOException ex)
                {
                    // Insert some logic here
                }

                return fileNexive;
            }
        }

        public static string GetInterventiRevocaReport(string IDAccertamento, string IDProceduraAccertamento, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            string pathAccertamentiReport = "";

            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDAccertamento", IDAccertamento);
            ReportParameter rp1 = new ReportParameter("IDProceduraAccertamento", IDProceduraAccertamento);

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0, rp1 });

            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            String[] streams = null;
            string format = "PDF";
            string name = string.Empty;
            string extension = string.Empty;
            string mime = string.Empty;
            string encoding = string.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileName = reportName + "_" + IDAccertamento + "." + extension;

            pathAccertamentiReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

            using (FileStream fs = new FileStream(pathAccertamentiReport, FileMode.Create))
            {
                fs.Write(ilReport, 0, ilReport.Length);
                fs.Flush();
                fs.Close();
            }

            return urlSite + destinationFile + "/" + fileName;
        }

        public static string GetSanzioniRevocaReport(string IDAccertamento, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            string pathAccertamentiReport = "";

            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDAccertamento", IDAccertamento);
            
            rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            String[] streams = null;
            string format = "PDF";
            string name = string.Empty;
            string extension = string.Empty;
            string mime = string.Empty;
            string encoding = string.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileName = reportName + "_" + IDAccertamento + "." + extension;

            pathAccertamentiReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

            using (FileStream fs = new FileStream(pathAccertamentiReport, FileMode.Create))
            {
                fs.Write(ilReport, 0, ilReport.Length);
                fs.Flush();
                fs.Close();
            }

            return urlSite + destinationFile + "/" + fileName;
        }

        public static string GetInterventiRevocaNexiveReport(string IDAccertamento, string IDProceduraAccertamento, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDAccertamento", IDAccertamento);
            //ReportParameter rp1 = new ReportParameter("IDProceduraAccertamento", IDProceduraAccertamento);

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            String[] streams = null;
            string format = "PDF";
            string name = string.Empty;
            string extension = string.Empty;
            string mime = string.Empty;
            string encoding = string.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileFronteSpizio = "FrontespizioNexiveVuoto_" + IDAccertamento + "_" + IDProceduraAccertamento + "." + extension;
            string fileRevoca = "RevocaInterventi_" + IDAccertamento + "." + extension;
            string fileNexive = "RevocaInterventiNexive_" + IDAccertamento + "." + extension;

            //string fileName = reportName + "_" + IDAccertamento + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            string pathFronteSpizioReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileFronteSpizio;
            string pathAccertamentoReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileRevoca;

            try
            {

                using (FileStream fs = new FileStream(pathFronteSpizioReport, FileMode.Create))
                {
                    fs.Write(ilReport, 0, ilReport.Length);
                    fs.Flush();
                    fs.Close();
                }

                string[] filesPath = new string[] { pathFronteSpizioReport, pathAccertamentoReport };
                string pathPdfOutMergeFile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + "Merged_" + fileNexive;
                MergePdf(filesPath, pathPdfOutMergeFile);

                string pathPdfAccertamentiReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileNexive;
                SetPdfVersion(pathPdfOutMergeFile, pathPdfAccertamentiReport);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Insert some logic here
            }
            catch (FileNotFoundException ex)
            {
                // Insert some logic here
            }
            catch (IOException ex)
            {
                // Insert some logic here
            }

            return fileNexive;
        }

        public static void SetPdfVersion(string pathAccertamentiReport, string pathOutPutAccertamentiReport)
        {
            using (PdfReader reader = new PdfReader(pathAccertamentiReport))
            {
                try
                {
                    using (PdfStamper stamper = new PdfStamper(reader, new FileStream(pathOutPutAccertamentiReport, FileMode.Create, FileAccess.Write)))
                    {
                        stamper.Writer.SetPdfVersion(PdfWriter.PDF_VERSION_1_7);
                        stamper.FormFlattening = true;
                        stamper.SetFullCompression();
                        stamper.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        public static void MergePdf(string[] filesPath, string pathPdfOutMergeFile)
        {
            List<PdfReader> readerList = new List<PdfReader>();
            foreach (string filePath in filesPath)
            {
                PdfReader pdfReader = new PdfReader(filePath);
                readerList.Add(pdfReader);
            }

            Document document = new Document(PageSize.A4, 0, 0, 0, 0);
            using (PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pathPdfOutMergeFile, FileMode.Create)))
            {
                document.Open();

                foreach (PdfReader reader in readerList)
                {
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        PdfImportedPage page = writer.GetImportedPage(reader, i);
                        document.Add(Image.GetInstance(page));
                    }
                }
                document.Close();
            }
        }

        public static void GetLibrettiImpiantoEntiReport(string IDSoggettoEnte, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            try
            {
                string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
                string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
                string credentialsDomain = string.Empty;

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
                {
                    credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
                }

                ReportViewer rview = new ReportViewer();
                rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
                rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
                rview.ServerReport.ReportPath = reportPath + reportName;

                ReportParameter rp0 = new ReportParameter("IDSoggetto", IDSoggettoEnte);

                rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

                Warning[] warnings = null;
                String[] streams = null;
                string format = "EXCELOPENXML";
                //string format = "XML";
                string name = String.Empty;
                string extension = String.Empty;
                string mime = String.Empty;
                string encoding = String.Empty;

                Byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
                string fileName = reportName + "_" + IDSoggettoEnte + "." + extension;

                string pathXlsReport = destinationFile + @"\" + fileName;

                using (FileStream fs = new FileStream(pathXlsReport, FileMode.Create))
                {
                    fs.Write(ilReport, 0, ilReport.Length);
                    fs.Flush();
                    fs.Close();
                }               
            }
            catch (Exception ex)
            {

            }
        }

        public static void CreateReportsEnti()
        {
            string reportPath = ConfigurationManager.AppSettings["ReportPath"];
            string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
            string destinationFile = ConfigurationManager.AppSettings["PathDocument"] + ConfigurationManager.AppSettings["UploadIscrizioneSoggetti"] + @"\" + "ReportsComuni";
            string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();
            UtilityFileSystem.CreateDirectoryIfNotExists(destinationFile);
            string reportName = "ReportComuni";

            using (var ctx = new CriterDataModel())
            {
                var entiList = (from a in ctx.COM_AnagraficaSoggetti
                            where (a.IDTipoSoggetto == 9 && a.fAttivo == true)
                            select new
                            {
                                iDSoggetto = a.IDSoggetto
                            }).ToList();

                foreach (var ente in entiList)
                {
                    string pathExcelFile = destinationFile + @"\" + "ReportComuni_" + ente.iDSoggetto + ".xlsx";
                    FileInfo file = new FileInfo(pathExcelFile);
                    if (File.Exists(pathExcelFile))
                    {
                        if (file.CreationTime.AddDays(7) < DateTime.Now)
                        {
                            GetLibrettiImpiantoEntiReport(ente.iDSoggetto.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);
                        }
                    }
                    else
                    {
                        GetLibrettiImpiantoEntiReport(ente.iDSoggetto.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite);
                    }
                }
            }
        }

        #region Sanzioni
        public static string GetVerbaleSanzioneReport(string IDAccertamento, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            using (var ctx = new CriterDataModel())
            {
                long iDAccertamento = long.Parse(IDAccertamento);
                var accertamento = ctx.VER_Accertamento.Where(a => a.IDAccertamento == iDAccertamento).FirstOrDefault();

                string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
                string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
                string credentialsDomain = string.Empty;

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
                {
                    credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
                }

                ReportViewer rview = new ReportViewer();
                rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
                rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
                rview.ServerReport.ReportPath = reportPath + reportName;

                ReportParameter rp0 = new ReportParameter("IDAccertamento", IDAccertamento);
                
                rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

                Microsoft.Reporting.WebForms.Warning[] warnings = null;
                String[] streams = null;
                string format = "PDF";
                string name = String.Empty;
                string extension = String.Empty;
                string mime = String.Empty;
                string encoding = String.Empty;

                Byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
                string fileName = reportName + "_" + IDAccertamento + "." + extension;

                string guid = Guid.NewGuid().ToString("N").Substring(0, 12);
                string pathTmpSanzioneReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + "tmp_" + guid + "_" + fileName;
                UtilityFileSystem.CreateDirectoryIfNotExists(ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\");

                using (FileStream fs = new FileStream(pathTmpSanzioneReport, FileMode.Create))
                {
                    fs.Write(ilReport, 0, ilReport.Length);
                    fs.Flush();
                    fs.Close();
                }

                var documenti = UtilityVerifiche.GetDocumentiAccertamenti(iDAccertamento).FirstOrDefault(); //IL PRIMO VERBALE ACCERTAMENTO
                string destinationVerbaleAccertamento = ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + accertamento.CodiceAccertamento;
                string fileNameAccertamento = accertamento.IDAccertamento + "_" + documenti.IDProceduraAccertamento + ".pdf";

                string pathPdfAccertamentiReport = ConfigurationManager.AppSettings["PathDocument"] + destinationVerbaleAccertamento + @"\" + fileNameAccertamento;
                
                string[] filesPath = new string[] { pathTmpSanzioneReport, pathPdfAccertamentiReport };
                string pathPdfOutMergeFile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + "merge_" + fileName;
                MergePdf(filesPath, pathPdfOutMergeFile);

                string pathPdfSanzioneReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;
                SetPdfVersion(pathPdfOutMergeFile, pathPdfSanzioneReport);

                try
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    File.Delete(pathTmpSanzioneReport);
                    File.Delete(pathPdfOutMergeFile);
                }
                catch (Exception ex)
                {

                }

                return urlSite + destinationFile + "/" + fileName;
            }
        }

        #endregion


        #region Ispezioni
        public static string GetIspezioneLetteraIncaricoReport(string IDIspezioneVisita, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            string pathLetteraIncaricoReport = "";

            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDIspezioneVisita", IDIspezioneVisita);

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            String[] streams = null;
            string format = "PDF";
            string name = string.Empty;
            string extension = string.Empty;
            string mime = string.Empty;
            string encoding = string.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileName = reportName + "_" + IDIspezioneVisita + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            pathLetteraIncaricoReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

            using (FileStream fs = new FileStream(pathLetteraIncaricoReport, FileMode.Create))
            {
                fs.Write(ilReport, 0, ilReport.Length);
                fs.Flush();
                fs.Close();
            }

            return urlSite + destinationFile + "/" + fileName;
        }

        #region Ispezione pianificazione conferma
        public static string GetIspezionePianificazioneConfermaReport(string IDIspezione, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            string pathIspezioniReport = "";

            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDIspezione", IDIspezione);

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            String[] streams = null;
            string format = "PDF";
            string name = string.Empty;
            string extension = string.Empty;
            string mime = string.Empty;
            string encoding = string.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileName = reportName + "_" + IDIspezione + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            pathIspezioniReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

            using (FileStream fs = new FileStream(pathIspezioniReport, FileMode.Create))
            {
                fs.Write(ilReport, 0, ilReport.Length);
                fs.Flush();
                fs.Close();
            }

            return urlSite + destinationFile + "/" + fileName;
        }

        public static string GetIspezionePianificazioneConfermaNexiveReport(string IDIspezione, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDIspezione", IDIspezione);

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            String[] streams = null;
            string format = "PDF";
            string name = string.Empty;
            string extension = string.Empty;
            string mime = string.Empty;
            string encoding = string.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileFronteSpizio = "FrontespizioNexiveVuoto_" + IDIspezione + "." + extension;
            string filePianificazioneConferma = "IspezionePianificazioneConferma_" + IDIspezione + "." + extension;
            string fileNexive = "IspezionePianificazioneConfermaNexive_" + IDIspezione + "." + extension;

            //string fileName = reportName + "_" + IDAccertamento + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            string pathFronteSpizioReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileFronteSpizio;
            string pathPianificazioneConfermaReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + filePianificazioneConferma;

            try
            {

                using (FileStream fs = new FileStream(pathFronteSpizioReport, FileMode.Create))
                {
                    fs.Write(ilReport, 0, ilReport.Length);
                    fs.Flush();
                    fs.Close();
                }

                string[] filesPath = new string[] { pathFronteSpizioReport, pathPianificazioneConfermaReport };
                string pathPdfOutMergeFile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + "Merged_" + fileNexive;
                MergePdf(filesPath, pathPdfOutMergeFile);

                string pathPdfIspezioniReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileNexive;
                SetPdfVersion(pathPdfOutMergeFile, pathPdfIspezioniReport);

                if (File.Exists(ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileFronteSpizio))
                {
                    try
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        File.Delete(ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileFronteSpizio);
                    }
                    catch (System.IO.IOException ex)
                    {

                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                // Insert some logic here
            }
            catch (FileNotFoundException ex)
            {
                // Insert some logic here
            }
            catch (IOException ex)
            {
                // Insert some logic here
            }

            return fileNexive;
        }
        #endregion

        #region Ispezione Annullamento
        public static string GetIspezioneAnnullamentoConfermaReport(string IDIspezione, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            string pathIspezioniReport = "";

            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDIspezione", IDIspezione);

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            String[] streams = null;
            string format = "PDF";
            string name = string.Empty;
            string extension = string.Empty;
            string mime = string.Empty;
            string encoding = string.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileName = reportName + "_" + IDIspezione + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            pathIspezioniReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

            using (FileStream fs = new FileStream(pathIspezioniReport, FileMode.Create))
            {
                fs.Write(ilReport, 0, ilReport.Length);
                fs.Flush();
                fs.Close();
            }

            return urlSite + destinationFile + "/" + fileName;
        }

        public static string GetIspezioneAnnullamentoNexiveReport(string IDIspezione, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDIspezione", IDIspezione);

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            String[] streams = null;
            string format = "PDF";
            string name = string.Empty;
            string extension = string.Empty;
            string mime = string.Empty;
            string encoding = string.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileFronteSpizio = "FrontespizioNexiveVuoto_" + IDIspezione + "." + extension;
            string fileRevoca = "IspezioneAnnullamento_" + IDIspezione + "." + extension;
            string fileNexive = "IspezioneAnnullamentoNexive_" + IDIspezione + "." + extension;

            //string fileName = reportName + "_" + IDAccertamento + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            string pathFronteSpizioReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileFronteSpizio;
            string pathAccertamentoReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileRevoca;

            try
            {

                using (FileStream fs = new FileStream(pathFronteSpizioReport, FileMode.Create))
                {
                    fs.Write(ilReport, 0, ilReport.Length);
                    fs.Flush();
                    fs.Close();
                }

                string[] filesPath = new string[] { pathFronteSpizioReport, pathAccertamentoReport };
                string pathPdfOutMergeFile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + "Merged_" + fileNexive;
                MergePdf(filesPath, pathPdfOutMergeFile);

                string pathPdfIspezioniReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileNexive;
                SetPdfVersion(pathPdfOutMergeFile, pathPdfIspezioniReport);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Insert some logic here
            }
            catch (FileNotFoundException ex)
            {
                // Insert some logic here
            }
            catch (IOException ex)
            {
                // Insert some logic here
            }

            return fileNexive;
        }
        #endregion

        #region Ispezione ripianificazione ispezione
        public static string GetIspezionePianificazioneRipianificazioneReport(string IDIspezione, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            string pathIspezioniReport = "";

            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDIspezione", IDIspezione);

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            String[] streams = null;
            string format = "PDF";
            string name = string.Empty;
            string extension = string.Empty;
            string mime = string.Empty;
            string encoding = string.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileName = reportName + "_" + IDIspezione + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            pathIspezioniReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

            using (FileStream fs = new FileStream(pathIspezioniReport, FileMode.Create))
            {
                fs.Write(ilReport, 0, ilReport.Length);
                fs.Flush();
                fs.Close();
            }

            return urlSite + destinationFile + "/" + fileName;
        }

        public static string GetIspezionePianificazioneRipianificazioneNexiveReport(string IDIspezione, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDIspezione", IDIspezione);

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            String[] streams = null;
            string format = "PDF";
            string name = string.Empty;
            string extension = string.Empty;
            string mime = string.Empty;
            string encoding = string.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileFronteSpizio = "FrontespizioNexiveVuoto_" + IDIspezione + "." + extension;
            string fileRevoca = "IspezionePianificazioneRipianificazione_" + IDIspezione + "." + extension;
            string fileNexive = "IspezionePianificazioneRipianificazioneNexive_" + IDIspezione + "." + extension;

            //string fileName = reportName + "_" + IDAccertamento + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            string pathFronteSpizioReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileFronteSpizio;
            string pathIspezioneReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileRevoca;

            try
            {
                using (FileStream fs = new FileStream(pathFronteSpizioReport, FileMode.Create))
                {
                    fs.Write(ilReport, 0, ilReport.Length);
                    fs.Flush();
                    fs.Close();
                }

                string[] filesPath = new string[] { pathFronteSpizioReport, pathIspezioneReport };
                string pathPdfOutMergeFile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + "Merged_" + fileNexive;
                MergePdf(filesPath, pathPdfOutMergeFile);

                string pathPdfIspezioniReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileNexive;
                SetPdfVersion(pathPdfOutMergeFile, pathPdfIspezioniReport);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Insert some logic here
            }
            catch (FileNotFoundException ex)
            {
                // Insert some logic here
            }
            catch (IOException ex)
            {
                // Insert some logic here
            }

            return fileNexive;
        }
        #endregion

        public static string GetIspezioneDocumentiReport(string IDIspezione, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite, string estensione)
        {
            string pathIspezioniReport = "";

            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDIspezione", IDIspezione);

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            String[] streams = null;
            string format = estensione;
            string name = string.Empty;
            string extension = string.Empty;
            string mime = string.Empty;
            string encoding = string.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileName = reportName + "_" + IDIspezione + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            pathIspezioniReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

            using (FileStream fs = new FileStream(pathIspezioniReport, FileMode.Create))
            {
                fs.Write(ilReport, 0, ilReport.Length);
                fs.Flush();
                fs.Close();
            }

            return urlSite + destinationFile + "/" + fileName;
        }

        public static string GetProgrammaIspezioneReport(string IDProgrammaIspezione, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            string pathProgrammaIspezioneReport = "";

            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDProgrammaIspezione", IDProgrammaIspezione);
            
            rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

            Warning[] warnings = null;
            string[] streams = null;
            string format = "PDF";
            string name = string.Empty;
            string extension = string.Empty;
            string mime = string.Empty;
            string encoding = string.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileName = reportName + "_" + IDProgrammaIspezione + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            pathProgrammaIspezioneReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

            using (FileStream fs = new FileStream(pathProgrammaIspezioneReport, FileMode.Create))
            {
                fs.Write(ilReport, 0, ilReport.Length);
                fs.Flush();
                fs.Close();
            }

            return urlSite + destinationFile + "/" + fileName;
        }

        public static string GetProgrammaIspezioneVisiteIspettiveReport(string IDProgrammaIspezione, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            string pathProgrammaIspezioneReport = "";

            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDProgrammaIspezione", IDProgrammaIspezione);

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

            Warning[] warnings = null;
            string[] streams = null;
            string format = "EXCELOPENXML";
            string name = string.Empty;
            string extension = string.Empty;
            string mime = string.Empty;
            string encoding = string.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileName = reportName + "_" + IDProgrammaIspezione + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            pathProgrammaIspezioneReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

            using (FileStream fs = new FileStream(pathProgrammaIspezioneReport, FileMode.Create))
            {
                fs.Write(ilReport, 0, ilReport.Length);
                fs.Flush();
                fs.Close();
            }

            return urlSite + destinationFile + "/" + fileName;
        }

        //public static string GetIspezioneNonEffettuataReport(string IDIspezione, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        //{
        //    string pathIspezioniReport = "";

        //    string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
        //    string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());

        //    ReportViewer rview = new ReportViewer();
        //    rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, "");
        //    rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
        //    rview.ServerReport.ReportPath = reportPath + reportName;

        //    ReportParameter rp0 = new ReportParameter("IDIspezione", IDIspezione);

        //    rview.ServerReport.SetParameters(new ReportParameter[] { rp0 });

        //    Microsoft.Reporting.WebForms.Warning[] warnings = null;
        //    String[] streams = null;
        //    string format = "PDF";
        //    string name = string.Empty;
        //    string extension = string.Empty;
        //    string mime = string.Empty;
        //    string encoding = string.Empty;

        //    byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
        //    string fileName = reportName + "_" + IDIspezione + "." + extension;

        //    string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        //    string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

        //    pathIspezioniReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

        //    using (FileStream fs = new FileStream(pathIspezioniReport, FileMode.Create))
        //    {
        //        fs.Write(ilReport, 0, ilReport.Length);
        //        fs.Flush();
        //        fs.Close();
        //    }

        //    return urlSite + destinationFile + "/" + fileName;
        //}

        //public static string GetIspezioneRapportoIspezioneReport(string IDIspezione, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        //{
        //    string urlPortal = ConfigurationManager.AppSettings["UrlPortal"].ToString();

        //    string pathIspezioniReport = "";

        //    string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
        //    string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());

        //    ReportViewer rview = new ReportViewer();
        //    rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, "");
        //    rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
        //    rview.ServerReport.ReportPath = reportPath + reportName;

        //    ReportParameter rp0 = new ReportParameter("IDIspezione", IDIspezione);
        //    ReportParameter rp1 = new ReportParameter("CriterUrl", urlPortal + destinationFile + "/");

        //    rview.ServerReport.SetParameters(new ReportParameter[] { rp0, rp1 });

        //    Microsoft.Reporting.WebForms.Warning[] warnings = null;
        //    String[] streams = null;
        //    string format = "PDF";
        //    string name = string.Empty;
        //    string extension = string.Empty;
        //    string mime = string.Empty;
        //    string encoding = string.Empty;

        //    byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
        //    string fileName = reportName + "_" + IDIspezione + "." + extension;

        //    string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        //    string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

        //    pathIspezioniReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

        //    using (FileStream fs = new FileStream(pathIspezioniReport, FileMode.Create))
        //    {
        //        fs.Write(ilReport, 0, ilReport.Length);
        //        fs.Flush();
        //        fs.Close();
        //    }

        //    return urlSite + destinationFile + "/" + fileName;
        //}
        #endregion

        #region Dismissioni Generatori
        public static string GetRichiestaDismissioneGeneratoreReport(string iDLibrettoImpianto, string iDGeneratore, string prefisso, string reportName, string reportUrl, string reportPath, string destinationFile, string urlSite)
        {
            string pathDismissioneGeneratoreReport = "";

            string usernameEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionUsername"].ToString());
            string passwordEncripted = CredentialsCryptography.Decrypt(ConfigurationManager.AppSettings["EncryptionPassword"].ToString());
            string credentialsDomain = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CredentialsDomain"].ToString()))
            {
                credentialsDomain = ConfigurationManager.AppSettings["CredentialsDomain"].ToString();
            }

            ReportViewer rview = new ReportViewer();
            rview.ServerReport.ReportServerCredentials = new MyReportServerCredentials(usernameEncripted, passwordEncripted, credentialsDomain);
            rview.ServerReport.ReportServerUrl = new Uri(reportUrl);
            rview.ServerReport.ReportPath = reportPath + reportName;

            ReportParameter rp0 = new ReportParameter("IDLibrettoImpianto", iDLibrettoImpianto);
            ReportParameter rp1 = new ReportParameter("IDGeneratore", iDGeneratore);
            ReportParameter rp2 = new ReportParameter("Prefisso", prefisso);

            rview.ServerReport.SetParameters(new ReportParameter[] { rp0, rp1, rp2 });

            Warning[] warnings = null;
            string[] streams = null;
            string format = "PDF";
            string name = string.Empty;
            string extension = string.Empty;
            string mime = string.Empty;
            string encoding = string.Empty;

            byte[] ilReport = rview.ServerReport.Render(format, null, out mime, out encoding, out extension, out streams, out warnings);
            string fileName = prefisso + "_" + iDGeneratore + "_" + iDLibrettoImpianto + "." + extension;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            string sQueryString = System.Web.HttpContext.Current.Request.Url.Query;

            pathDismissioneGeneratoreReport = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;

            using (FileStream fs = new FileStream(pathDismissioneGeneratoreReport, FileMode.Create))
            {
                fs.Write(ilReport, 0, ilReport.Length);
                fs.Flush();
                fs.Close();
            }

            return pathDismissioneGeneratoreReport;
        }



        #endregion
        
    }

    public class CReportViewerCustomMessages : Microsoft.Reporting.WebForms.IReportViewerMessages
    {
        #region IReportViewerMessages Members

        public string BackButtonToolTip
        {
            get { return ("Indietro"); }
        }

        public string ChangeCredentialsText
        {
            get { return ("Cambia credenziali di accesso"); }
        }

        public string ChangeCredentialsToolTip
        {
            get { return ("Cambia credenziali di accesso"); }
        }

        public string CurrentPageTextBoxToolTip
        {
            get { return ("Pagina corrente"); }
        }

        public string DocumentMap
        {
            get { return ("DocumentMap"); }
        }

        public string DocumentMapButtonToolTip
        {
            get { return ("DocumentMapButtonToolTip"); }
        }

        public string ExportButtonText
        {
            get { return ("Esporta"); }
        }

        public string ExportButtonToolTip
        {
            get { return ("Esporta"); }
        }

        public string ExportFormatsToolTip
        {
            get { return ("Formati di esportazione"); }
        }

        public string FalseValueText
        {
            get { return ("FalseValueText"); }
        }

        public string FindButtonText
        {
            get { return ("Cerca"); }
        }

        public string FindButtonToolTip
        {
            get { return ("Cerca"); }
        }

        public string FindNextButtonText
        {
            get { return ("Prossimo"); }
        }

        public string FindNextButtonToolTip
        {
            get { return ("Prossimo"); }
        }

        public string FirstPageButtonToolTip
        {
            get { return ("Prima pagina"); }
        }

        public string InvalidPageNumber
        {
            get { return ("Numero di pagina non valido"); }
        }

        public string LastPageButtonToolTip
        {
            get { return ("Ultima pagina"); }
        }

        public string NextPageButtonToolTip
        {
            get { return ("Prossima pagina"); }
        }

        public string NoMoreMatches
        {
            get { return ("NoMoreMatches"); }
        }

        public string NullCheckBoxText
        {
            get { return ("NullCheckBoxText"); }
        }

        public string NullValueText
        {
            get { return ("NullValueText"); }
        }

        public string PageOf
        {
            get { return ("di"); }
        }

        public string ParameterAreaButtonToolTip
        {
            get { return ("ParameterAreaButtonToolTip"); }
        }

        public string PasswordPrompt
        {
            get { return ("PasswordPrompt"); }
        }

        public string PreviousPageButtonToolTip
        {
            get { return ("Pagina precedente"); }
        }

        public string PrintButtonToolTip
        {
            get { return ("Stampa report"); }
        }

        public string ProgressText
        {
            get { return ("Generazione del Report in corso..."); }
        }

        public string RefreshButtonToolTip
        {
            get { return ("Ricarica report"); }
        }

        public string SearchTextBoxToolTip
        {
            get { return ("Ricerca testo nel report"); }
        }

        public string SelectAValue
        {
            get { return ("Selezionare un valore"); }
        }

        public string SelectAll
        {
            get { return ("Selezionare tutti"); }
        }

        public string SelectFormat
        {
            get { return ("Seleziona un formato"); }
        }

        public string TextNotFound
        {
            get { return ("Testo non trovato"); }
        }

        public string TodayIs
        {
            get { return ("TodayIs"); }
        }

        public string TrueValueText
        {
            get { return ("TrueValueText"); }
        }

        public string UserNamePrompt
        {
            get { return ("UserNamePrompt"); }
        }

        public string ViewReportButtonText
        {
            get { return ("Visualizza Report"); }
        }

        public string ZoomControlToolTip
        {
            get { return ("Visualizza"); }
        }

        public string ZoomToPageWidth
        {
            get { return ("Grandezza pagina"); }
        }

        public string ZoomToWholePage
        {
            get { return ("Adatta alla pagina"); }
        }

        #endregion
    }

    [Serializable()]
    public sealed class MyReportServerCredentials : IReportServerCredentials
    {
        string _userName;
        string _password;
        string _domain;

        public MyReportServerCredentials(string userName, string password, string domain)
        {
            this._userName = userName;
            this._password = password;
            this._domain = domain;
        }

        public WindowsIdentity ImpersonationUser
        {
            get
            {
                // Use the default Windows user.  Credentials will be
                // provided by the NetworkCredentials property.            
                return null;
            }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                return new NetworkCredential(_userName, _password, _domain);
            }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;

            // Not using form credentials
            return false;
        }

    }
}
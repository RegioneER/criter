using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Vml;
using Pomiager.Service.Parer.RequestDto;
using PosteItaliane;
using PosteItaliane.AGOLWebService;
using PosteItaliane.ROLWebService;
using static PosteItaliane.ServiceTypeEnum;

namespace DataUtilityCore
{
    public class UtilityPosteItaliane
    {
        #region POSTE ITALIANE

        public static bool SendToPosteItaliane(long iD, int typeofRaccomandata)
        {
            var t = Task.Run(() =>
            {
                #region
                string mittente = "Arter SpA";
                string mittenteDug = "Via";
                string mittenteToponimo = "Gian Battista Morgagni";
                string mittenteNumeroCivico = "6";
                string mittenteCap = "40122";
                string mittenteCitta = "Bologna";
                string mittenteProvincia = "BO";
                #endregion

                string reportName = string.Empty;
                string destinationFile = string.Empty;
                string reportPath = ConfigurationManager.AppSettings["ReportPath"];
                string reportUrl = ConfigurationManager.AppSettings["ReportRemoteURL"];
                string urlSite = ConfigurationManager.AppSettings["UrlPortal"].ToString();
                bool fRaccomandataInviata = false;

                switch (typeofRaccomandata)
                {
                    case 1: //Accertamento
                        #region Documenti Accertamenti
                        using (var ctx = new CriterDataModel())
                        {
                            var accertamento = ctx.V_VER_Accertamenti.Where(c => c.IDAccertamento == iD).FirstOrDefault();

                            var TerzoResponsabile = UtilityLibrettiImpianti.GetDatiTerzoResponsabile((int)accertamento.IDLibrettoImpianto);
                            
                            var documenti = UtilityVerifiche.GetDocumentiAccertamenti(iD);
                            foreach (var documento in documenti)
                            {
                                reportName = ConfigurationManager.AppSettings["ReportNameAccertamento"];
                                destinationFile = ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + documento.CodiceAccertamento;

                                string fileName = documento.IDAccertamento + "_" + documento.IDProceduraAccertamento + ".pdf";

                                string pathAndfile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;
                                if (!File.Exists(pathAndfile))
                                {
                                    fileName = UtilityApp.GetFileNameFromUrl(ReportingServices.GetAccertamentiReport(documento.IDAccertamento.ToString(), documento.IDProceduraAccertamento.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite));
                                }

                                #region Invio Raccomandate
                                PosteItaliane.ROLService rolSvc = new PosteItaliane.ROLService();
                                PosteItaliane.Lettera raccomandata = new PosteItaliane.Lettera();
                                raccomandata.Mittente = new PosteItaliane.ROLWebService.Mittente()
                                {
                                    Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                    {
                                        RagioneSociale = mittente,
                                        Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                        {
                                            DUG = mittenteDug,
                                            Toponimo = mittenteToponimo,
                                            NumeroCivico = mittenteNumeroCivico
                                        },
                                        CAP = mittenteCap,
                                        Citta = mittenteCitta,
                                        Provincia = mittenteProvincia,
                                        TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                    }
                                };
                                //DATI RICEVUTA
                                raccomandata.DatiRicevuta = new PosteItaliane.ROLWebService.DatiRicevuta()
                                {
                                    Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                    {
                                        Nome = string.Empty,
                                        Cognome = string.Empty,
                                        ComplementoNominativo = "Organismo di Accreditamento ed Ispezione",
                                        RagioneSociale = mittente,
                                        Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                        {
                                            DUG = mittenteDug,
                                            Toponimo = mittenteToponimo,
                                            NumeroCivico = mittenteNumeroCivico
                                        },
                                        Frazione = string.Empty,
                                        CAP = mittenteCap,
                                        Citta = mittenteCitta,
                                        Provincia = mittenteProvincia,
                                        UfficioPostale = string.Empty,
                                        CasellaPostale = string.Empty,
                                        ForzaDestinazione = false,
                                        Stato = "Italia",
                                        TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                    }
                                };
                                raccomandata.Destinatari = new PosteItaliane.ROLWebService.Destinatario[1];

                                if (TerzoResponsabile!= null)
                                {
                                    #region Destinatario Terzo Responsabile
                                    raccomandata.Destinatari[0] = new PosteItaliane.ROLWebService.Destinatario()
                                    {
                                        Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                        {
                                            ComplementoNominativo = TerzoResponsabile.RagioneSocialeTerzoResponsabile,
                                            RagioneSociale = string.Empty,
                                            Cognome = TerzoResponsabile.CognomeTerzoResponsabile,
                                            Nome = TerzoResponsabile.NomeTerzoResponsabile,
                                            Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                            {
                                                DUG = string.Empty,
                                                Toponimo = TerzoResponsabile.IndirizzoTerzoResponsabile,
                                                NumeroCivico = TerzoResponsabile.CivicoTerzoResponsabile,
                                                Esponente = string.Empty
                                            },
                                            CAP = TerzoResponsabile.CapTerzoResponsabile,
                                            Citta = TerzoResponsabile.CittaTerzoResponsabile,
                                            Provincia = TerzoResponsabile.ProvinciaTerzoResponsabile,
                                            TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                        }
                                    };
                                    #endregion
                                }
                                else
                                {
                                    #region Destinatario Responsabile Impianto
                                    raccomandata.Destinatari[0] = new PosteItaliane.ROLWebService.Destinatario()
                                    {
                                        Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                        {
                                            ComplementoNominativo = accertamento.RagioneSocialeResponsabile,
                                            RagioneSociale = string.Empty,
                                            Cognome = accertamento.CognomeResponsabile,
                                            Nome = accertamento.NomeResponsabile,
                                            Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                            {
                                                DUG = string.Empty,
                                                Toponimo = accertamento.IndirizzoNormalizzatoResponsabile,
                                                NumeroCivico = accertamento.CivicoNormalizzatoResponsabile,
                                                Esponente = string.Empty
                                            },
                                            CAP = accertamento.CapResponsabile,
                                            Citta = accertamento.ComuneResponsabile,
                                            Provincia = accertamento.ProvinciaResponsabile,
                                            TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                        }
                                    };
                                    #endregion
                                }
                                
                                raccomandata.Documento = new PosteItaliane.Documento()
                                {
                                    TipoDoc = PosteItaliane.TipoDocumento.PDF,
                                    Doc = System.IO.File.ReadAllBytes(pathAndfile)
                                };

                                var richiestaInvioROL = rolSvc.RichiestaInvioRaccomandata(raccomandata, "SYSTEM", typeofRaccomandata, documento.IDAccertamento, documento.IDAccertamentoDocumento, null);
                                if (richiestaInvioROL.IsSuccess)
                                {
                                    //Aspetto 60 secondi
                                    System.Threading.Thread.Sleep(65000);
                                    // Verifico il risultato
                                    var v = rolSvc.Valorizza(richiestaInvioROL.IDRichiesta, "SYSTEM");
                                    // Un utente guarda il risultato
                                    if (v.CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                    {
                                        if (v.ServizioEnquiryResponse.Length > 0)
                                        {
                                            if (v.ServizioEnquiryResponse[0].CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                            {
                                                if (v.ServizioEnquiryResponse[0].StatoLavorazione.Id == "R")
                                                {
                                                    // Logica di accettazione
                                                    richiestaInvioROL.GuidUtente = v.ServizioEnquiryResponse[0].Richiesta.GuidUtente;
                                                    var preConfRes = rolSvc.PreConfermaInvio(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, "SYSTEM");
                                                    // Qui se volgio posso chiedere il documento prima di confermare
                                                    richiestaInvioROL.IdOrdine = preConfRes.IdOrdine;
                                                    var dcs = rolSvc.RecuperaDCS(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, "SYSTEM");
                                                    string pdfFile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + richiestaInvioROL.IDRichiesta + ".pdf";

                                                    if (dcs.Documento.Immagine != null)
                                                    {
                                                        System.IO.File.WriteAllBytes(pdfFile, dcs.Documento.Immagine);
                                                    }
                                                    
                                                    // Confermo l'invio
                                                    var confRes = rolSvc.ConfermaInvio(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, richiestaInvioROL.IdOrdine, "SYSTEM");
                                                    if (confRes.CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                                    {
                                                        fRaccomandataInviata = true;
                                                        SaveNumeroRaccomandata(richiestaInvioROL.IDRichiesta, ctx, confRes);
                                                    }
                                                }
                                                else
                                                {
                                                    // ERRORE :-)
                                                    // Vedi DOC PDF
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }

                            if (fRaccomandataInviata)
                            {
                                var accertamentoUpdate = ctx.VER_Accertamento.Where(c => c.IDAccertamento == iD).FirstOrDefault();
                                accertamentoUpdate.DataInvioRaccomandata = DateTime.Now;
                                ctx.SaveChanges();
                            }                            
                        }
                        #endregion
                        break;
                    case 2: //Revoca
                        #region Revoca Interventi
                        using (var ctx = new CriterDataModel())
                        {
                            var revoca = ctx.V_VER_Accertamenti.Where(c => c.IDAccertamento == iD).FirstOrDefault();

                            var TerzoResponsabile = UtilityLibrettiImpianti.GetDatiTerzoResponsabile((int)revoca.IDLibrettoImpianto);

                            var documenti = UtilityVerifiche.GetDocumentiAccertamenti(iD);
                            foreach (var documento in documenti)
                            {
                                reportName = ConfigurationManager.AppSettings["ReportNameRevocaIntervento"];
                                destinationFile = ConfigurationManager.AppSettings["UploadAccertamento"] + @"\" + revoca.CodiceAccertamento;

                                string fileName = reportName + "_" + documento.IDAccertamento + ".pdf";
                                string pathAndfile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;
                                if (!File.Exists(pathAndfile))
                                {
                                    fileName = UtilityApp.GetFileNameFromUrl(ReportingServices.GetInterventiRevocaReport(documento.IDAccertamento.ToString(), documento.IDProceduraAccertamento.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite));
                                }

                                #region Invio Raccomandate
                                PosteItaliane.ROLService rolSvc = new PosteItaliane.ROLService();
                                PosteItaliane.Lettera raccomandata = new PosteItaliane.Lettera();
                                raccomandata.Mittente = new PosteItaliane.ROLWebService.Mittente()
                                {
                                    Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                    {
                                        RagioneSociale = mittente,
                                        Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                        {
                                            DUG = mittenteDug,
                                            Toponimo = mittenteToponimo,
                                            NumeroCivico = mittenteNumeroCivico
                                        },
                                        CAP = mittenteCap,
                                        Citta = mittenteCitta,
                                        Provincia = mittenteProvincia,
                                        TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                    }
                                };
                                raccomandata.Destinatari = new PosteItaliane.ROLWebService.Destinatario[1];
                                if (TerzoResponsabile != null)
                                {
                                    #region Destinatario Terzo Responsabile
                                    raccomandata.Destinatari[0] = new PosteItaliane.ROLWebService.Destinatario()
                                    {
                                        Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                        {
                                            ComplementoNominativo = TerzoResponsabile.RagioneSocialeTerzoResponsabile,
                                            RagioneSociale = string.Empty,
                                            Cognome = TerzoResponsabile.CognomeTerzoResponsabile,
                                            Nome = TerzoResponsabile.NomeTerzoResponsabile,
                                            Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                            {
                                                DUG = string.Empty,
                                                Toponimo = TerzoResponsabile.IndirizzoTerzoResponsabile,
                                                NumeroCivico = TerzoResponsabile.CivicoTerzoResponsabile,
                                                Esponente = string.Empty
                                            },
                                            CAP = TerzoResponsabile.CapTerzoResponsabile,
                                            Citta = TerzoResponsabile.CittaTerzoResponsabile,
                                            Provincia = TerzoResponsabile.ProvinciaTerzoResponsabile,
                                            TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                        }
                                    };
                                    #endregion
                                }
                                else
                                {
                                    #region Destinatario Responsabile Impianto
                                    raccomandata.Destinatari[0] = new PosteItaliane.ROLWebService.Destinatario()
                                    {
                                        Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                        {
                                            ComplementoNominativo = revoca.RagioneSocialeResponsabile,
                                            RagioneSociale = string.Empty,
                                            Cognome = revoca.CognomeResponsabile,
                                            Nome = revoca.NomeResponsabile,
                                            Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                            {
                                                DUG = string.Empty,
                                                Toponimo = revoca.IndirizzoNormalizzatoResponsabile,
                                                NumeroCivico = revoca.CivicoNormalizzatoResponsabile,
                                                Esponente = string.Empty
                                            },
                                            CAP = revoca.CapResponsabile,
                                            Citta = revoca.ComuneResponsabile,
                                            Provincia = revoca.ProvinciaResponsabile,
                                            TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                        }
                                    };
                                    #endregion
                                }
                                //raccomandata.Destinatari[0] = new PosteItaliane.ROLWebService.Destinatario()
                                //{
                                //    Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                //    {
                                //        ComplementoNominativo = revoca.RagioneSocialeResponsabile,
                                //        RagioneSociale = string.Empty,
                                //        Cognome = revoca.CognomeResponsabile,
                                //        Nome = revoca.NomeResponsabile,
                                //        Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                //        {
                                //            DUG = "",
                                //            Toponimo = revoca.IndirizzoNormalizzatoResponsabile,
                                //            NumeroCivico = revoca.CivicoNormalizzatoResponsabile,
                                //            Esponente = string.Empty
                                //        },
                                //        CAP = revoca.CapResponsabile,
                                //        Citta = revoca.ComuneResponsabile,
                                //        Provincia = revoca.ProvinciaResponsabile,
                                //        TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                //    }
                                //};
                                raccomandata.Documento = new PosteItaliane.Documento()
                                {
                                    TipoDoc = PosteItaliane.TipoDocumento.PDF,
                                    Doc = System.IO.File.ReadAllBytes(pathAndfile)
                                };

                                var richiestaInvioROL = rolSvc.RichiestaInvioRaccomandata(raccomandata, "SYSTEM", typeofRaccomandata, documento.IDAccertamento, documento.IDAccertamentoDocumento, null);
                                if (richiestaInvioROL.IsSuccess)
                                {
                                    // Aspetto 60 secondi
                                    System.Threading.Thread.Sleep(65000);
                                    // Verifico il risultato
                                    var v = rolSvc.Valorizza(richiestaInvioROL.IDRichiesta, "SYSTEM");
                                    // Un utente guarda il risultato
                                    if (v.CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                    {
                                        if (v.ServizioEnquiryResponse.Length > 0)
                                        {
                                            if (v.ServizioEnquiryResponse[0].CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                            {
                                                if (v.ServizioEnquiryResponse[0].StatoLavorazione.Id == "R")
                                                {
                                                    // Logica di accettazione
                                                    richiestaInvioROL.GuidUtente = v.ServizioEnquiryResponse[0].Richiesta.GuidUtente;
                                                    var preConfRes = rolSvc.PreConfermaInvio(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, "SYSTEM");
                                                    // Qui se volgio posso chiedere il documento prima di confermare
                                                    richiestaInvioROL.IdOrdine = preConfRes.IdOrdine;
                                                    var dcs = rolSvc.RecuperaDCS(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, "SYSTEM");
                                                    string pdfFile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + richiestaInvioROL.IDRichiesta + ".pdf";

                                                    if (dcs.Documento.Immagine != null)
                                                    {
                                                        System.IO.File.WriteAllBytes(pdfFile, dcs.Documento.Immagine);
                                                    }
                                                    
                                                    // Confermo l'invio
                                                    var confRes = rolSvc.ConfermaInvio(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, richiestaInvioROL.IdOrdine, "SYSTEM");
                                                    if (confRes.CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                                    {
                                                        SaveNumeroRaccomandata(richiestaInvioROL.IDRichiesta, ctx, confRes);
                                                    }
                                                }
                                                else
                                                {
                                                    // ERRORE :-)
                                                    // Vedi DOC PDF
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion
                        break;
                    case 3: //Conferma Pianificazione Ispezione
                        #region Conferma Pianificazione Ispezione
                        using (var ctx = new CriterDataModel())
                        {
                            var ispezione = ctx.V_VER_Ispezioni.Where(c => c.IDIspezione == iD).FirstOrDefault();

                            var TerzoResponsabile = UtilityLibrettiImpianti.GetDatiTerzoResponsabile(ispezione.IDLibrettoImpianto);

                            reportName = ConfigurationManager.AppSettings["ReportNameIspezionePianificazioneConferma"];
                            destinationFile = ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + ispezione.IDIspezioneVisita.ToString() + @"\" + ispezione.CodiceIspezione;

                            string fileName = reportName + "_" + ispezione.IDIspezione + ".pdf";

                            string pathAndfile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;
                            if (!File.Exists(pathAndfile))
                            {
                                fileName = UtilityApp.GetFileNameFromUrl(ReportingServices.GetIspezionePianificazioneConfermaReport(iD.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite));
                            }

                            #region Invio Raccomandate
                            PosteItaliane.ROLService rolSvc = new PosteItaliane.ROLService();
                            PosteItaliane.Lettera raccomandata = new PosteItaliane.Lettera();
                            raccomandata.Mittente = new PosteItaliane.ROLWebService.Mittente()
                            {
                                Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                {
                                    RagioneSociale = mittente,
                                    Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                    {
                                        DUG = mittenteDug,
                                        Toponimo = mittenteToponimo,
                                        NumeroCivico = mittenteNumeroCivico
                                    },
                                    CAP = mittenteCap,
                                    Citta = mittenteCitta,
                                    Provincia = mittenteProvincia,
                                    TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                }
                            };
                            raccomandata.Destinatari = new PosteItaliane.ROLWebService.Destinatario[1];
                            if (TerzoResponsabile != null)
                            {
                                #region Destinatario Terzo Responsabile
                                raccomandata.Destinatari[0] = new PosteItaliane.ROLWebService.Destinatario()
                                {
                                    Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                    {
                                        ComplementoNominativo = TerzoResponsabile.RagioneSocialeTerzoResponsabile,
                                        RagioneSociale = string.Empty,
                                        Cognome = TerzoResponsabile.CognomeTerzoResponsabile,
                                        Nome = TerzoResponsabile.NomeTerzoResponsabile,
                                        Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                        {
                                            DUG = string.Empty,
                                            Toponimo = TerzoResponsabile.IndirizzoTerzoResponsabile,
                                            NumeroCivico = TerzoResponsabile.CivicoTerzoResponsabile,
                                            Esponente = string.Empty
                                        },
                                        CAP = TerzoResponsabile.CapTerzoResponsabile,
                                        Citta = TerzoResponsabile.CittaTerzoResponsabile,
                                        Provincia = TerzoResponsabile.ProvinciaTerzoResponsabile,
                                        TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                    }
                                };
                                #endregion
                            }
                            else
                            {
                                #region Destinatario Responsabile Impianto
                                raccomandata.Destinatari[0] = new PosteItaliane.ROLWebService.Destinatario()
                                {
                                    Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                    {
                                        ComplementoNominativo = ispezione.RagioneSocialeResponsabile,
                                        RagioneSociale = string.Empty,
                                        Cognome = ispezione.CognomeResponsabile,
                                        Nome = ispezione.NomeResponsabile,
                                        Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                        {
                                            DUG = string.Empty,
                                            Toponimo = ispezione.IndirizzoNormalizzatoResponsabile,
                                            NumeroCivico = ispezione.CivicoNormalizzatoResponsabile,
                                            Esponente = string.Empty
                                        },
                                        CAP = ispezione.CapResponsabile,
                                        Citta = ispezione.ComuneResponsabile,
                                        Provincia = ispezione.ProvinciaResponsabile,
                                        TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                    }
                                };
                                #endregion
                            }
                            //raccomandata.Destinatari[0] = new PosteItaliane.ROLWebService.Destinatario()
                            //{
                            //    Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                            //    {
                            //        ComplementoNominativo = ispezione.RagioneSocialeResponsabile,
                            //        RagioneSociale = string.Empty,
                            //        Cognome = ispezione.CognomeResponsabile,
                            //        Nome = ispezione.NomeResponsabile,
                            //        Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                            //        {
                            //            DUG = "",
                            //            Toponimo = ispezione.IndirizzoNormalizzatoResponsabile,
                            //            NumeroCivico = ispezione.CivicoNormalizzatoResponsabile,
                            //            Esponente = string.Empty
                            //        },
                            //        CAP = ispezione.CapResponsabile,
                            //        Citta = ispezione.ComuneResponsabile,
                            //        Provincia = ispezione.ProvinciaResponsabile,
                            //        TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                            //    }
                            //};
                            raccomandata.Documento = new PosteItaliane.Documento()
                            {
                                TipoDoc = PosteItaliane.TipoDocumento.PDF,
                                Doc = System.IO.File.ReadAllBytes(pathAndfile)
                            };

                            var richiestaInvioROL = rolSvc.RichiestaInvioRaccomandata(raccomandata, "SYSTEM", typeofRaccomandata, null, null, ispezione.IDIspezione);
                            if (richiestaInvioROL.IsSuccess)
                            {
                                // Aspetto 60 secondi
                                System.Threading.Thread.Sleep(65000);
                                // Verifico il risultato
                                var v = rolSvc.Valorizza(richiestaInvioROL.IDRichiesta, "SYSTEM");
                                // Un utente guarda il risultato
                                if (v.CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                {
                                    if (v.ServizioEnquiryResponse.Length > 0)
                                    {
                                        if (v.ServizioEnquiryResponse[0].CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                        {
                                            if (v.ServizioEnquiryResponse[0].StatoLavorazione.Id == "R")
                                            {
                                                // Logica di accettazione
                                                richiestaInvioROL.GuidUtente = v.ServizioEnquiryResponse[0].Richiesta.GuidUtente;
                                                var preConfRes = rolSvc.PreConfermaInvio(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, "SYSTEM");
                                                // Qui se volgio posso chiedere il documento prima di confermare
                                                richiestaInvioROL.IdOrdine = preConfRes.IdOrdine;
                                                var dcs = rolSvc.RecuperaDCS(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, "SYSTEM");
                                                string pdfFile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + richiestaInvioROL.IDRichiesta + ".pdf";

                                                if (dcs.Documento.Immagine != null)
                                                {
                                                    System.IO.File.WriteAllBytes(pdfFile, dcs.Documento.Immagine);
                                                }

                                                // Confermo l'invio
                                                var confRes = rolSvc.ConfermaInvio(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, richiestaInvioROL.IdOrdine, "SYSTEM");
                                                if (confRes.CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                                {
                                                    SaveNumeroRaccomandata(richiestaInvioROL.IDRichiesta, ctx, confRes);
                                                }
                                            }
                                            else
                                            {
                                                // ERRORE :-)
                                                // Vedi DOC PDF
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        #endregion
                        break;
                    case 4: //Annullamento Pianificazione ispezione
                        #region Annullamento Pianificazione ispezione
                        using (var ctx = new CriterDataModel())
                        {
                            var ispezione = ctx.V_VER_Ispezioni.Where(c => c.IDIspezione == iD).FirstOrDefault();

                            var TerzoResponsabile = UtilityLibrettiImpianti.GetDatiTerzoResponsabile(ispezione.IDLibrettoImpianto);

                            reportName = ConfigurationManager.AppSettings["ReportNameIspezioneAnnullamento"];
                            destinationFile = ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + ispezione.IDIspezioneVisita.ToString() + @"\" + ispezione.CodiceIspezione;

                            string fileName = reportName + "_" + ispezione.IDIspezione + ".pdf";

                            string pathAndfile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;
                            if (!File.Exists(pathAndfile))
                            {
                                fileName = UtilityApp.GetFileNameFromUrl(ReportingServices.GetIspezionePianificazioneConfermaReport(iD.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite));
                            }

                            #region Invio Raccomandate
                            PosteItaliane.ROLService rolSvc = new PosteItaliane.ROLService();
                            PosteItaliane.Lettera raccomandata = new PosteItaliane.Lettera();
                            raccomandata.Mittente = new PosteItaliane.ROLWebService.Mittente()
                            {
                                Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                {
                                    RagioneSociale = mittente,
                                    Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                    {
                                        DUG = mittenteDug,
                                        Toponimo = mittenteToponimo,
                                        NumeroCivico = mittenteNumeroCivico
                                    },
                                    CAP = mittenteCap,
                                    Citta = mittenteCitta,
                                    Provincia = mittenteProvincia,
                                    TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                }
                            };
                            raccomandata.Destinatari = new PosteItaliane.ROLWebService.Destinatario[1];
                            if (TerzoResponsabile != null)
                            {
                                #region Destinatario Terzo Responsabile
                                raccomandata.Destinatari[0] = new PosteItaliane.ROLWebService.Destinatario()
                                {
                                    Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                    {
                                        ComplementoNominativo = TerzoResponsabile.RagioneSocialeTerzoResponsabile,
                                        RagioneSociale = string.Empty,
                                        Cognome = TerzoResponsabile.CognomeTerzoResponsabile,
                                        Nome = TerzoResponsabile.NomeTerzoResponsabile,
                                        Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                        {
                                            DUG = string.Empty,
                                            Toponimo = TerzoResponsabile.IndirizzoTerzoResponsabile,
                                            NumeroCivico = TerzoResponsabile.CivicoTerzoResponsabile,
                                            Esponente = string.Empty
                                        },
                                        CAP = TerzoResponsabile.CapTerzoResponsabile,
                                        Citta = TerzoResponsabile.CittaTerzoResponsabile,
                                        Provincia = TerzoResponsabile.ProvinciaTerzoResponsabile,
                                        TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                    }
                                };
                                #endregion
                            }
                            else
                            {
                                #region Destinatario Responsabile Impianto
                                raccomandata.Destinatari[0] = new PosteItaliane.ROLWebService.Destinatario()
                                {
                                    Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                    {
                                        ComplementoNominativo = ispezione.RagioneSocialeResponsabile,
                                        RagioneSociale = string.Empty,
                                        Cognome = ispezione.CognomeResponsabile,
                                        Nome = ispezione.NomeResponsabile,
                                        Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                        {
                                            DUG = string.Empty,
                                            Toponimo = ispezione.IndirizzoNormalizzatoResponsabile,
                                            NumeroCivico = ispezione.CivicoNormalizzatoResponsabile,
                                            Esponente = string.Empty
                                        },
                                        CAP = ispezione.CapResponsabile,
                                        Citta = ispezione.ComuneResponsabile,
                                        Provincia = ispezione.ProvinciaResponsabile,
                                        TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                    }
                                };
                                #endregion
                            }
                            //raccomandata.Destinatari[0] = new PosteItaliane.ROLWebService.Destinatario()
                            //{
                            //    Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                            //    {
                            //        ComplementoNominativo = ispezione.RagioneSocialeResponsabile,
                            //        RagioneSociale = string.Empty,
                            //        Cognome = ispezione.CognomeResponsabile,
                            //        Nome = ispezione.NomeResponsabile,
                            //        Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                            //        {
                            //            DUG = "",
                            //            Toponimo = ispezione.IndirizzoNormalizzatoResponsabile,
                            //            NumeroCivico = ispezione.CivicoNormalizzatoResponsabile,
                            //            Esponente = string.Empty
                            //        },
                            //        CAP = ispezione.CapResponsabile,
                            //        Citta = ispezione.ComuneResponsabile,
                            //        Provincia = ispezione.ProvinciaResponsabile,
                            //        TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                            //    }
                            //};
                            raccomandata.Documento = new PosteItaliane.Documento()
                            {
                                TipoDoc = PosteItaliane.TipoDocumento.PDF,
                                Doc = System.IO.File.ReadAllBytes(pathAndfile)
                            };

                            var richiestaInvioROL = rolSvc.RichiestaInvioRaccomandata(raccomandata, "SYSTEM", typeofRaccomandata, null, null, ispezione.IDIspezione);
                            if (richiestaInvioROL.IsSuccess)
                            {
                                // Aspetto 60 secondi
                                System.Threading.Thread.Sleep(65000);
                                // Verifico il risultato
                                var v = rolSvc.Valorizza(richiestaInvioROL.IDRichiesta, "SYSTEM");
                                // Un utente guarda il risultato
                                if (v.CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                {
                                    if (v.ServizioEnquiryResponse.Length > 0)
                                    {
                                        if (v.ServizioEnquiryResponse[0].CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                        {
                                            if (v.ServizioEnquiryResponse[0].StatoLavorazione.Id == "R")
                                            {
                                                // Logica di accettazione
                                                richiestaInvioROL.GuidUtente = v.ServizioEnquiryResponse[0].Richiesta.GuidUtente;
                                                var preConfRes = rolSvc.PreConfermaInvio(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, "SYSTEM");
                                                // Qui se volgio posso chiedere il documento prima di confermare
                                                richiestaInvioROL.IdOrdine = preConfRes.IdOrdine;
                                                var dcs = rolSvc.RecuperaDCS(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, "SYSTEM");
                                                string pdfFile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + richiestaInvioROL.IDRichiesta + ".pdf";

                                                if (dcs.Documento.Immagine != null)
                                                {
                                                    System.IO.File.WriteAllBytes(pdfFile, dcs.Documento.Immagine);
                                                }

                                                // Confermo l'invio
                                                var confRes = rolSvc.ConfermaInvio(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, richiestaInvioROL.IdOrdine, "SYSTEM");
                                                if (confRes.CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                                {
                                                    SaveNumeroRaccomandata(richiestaInvioROL.IDRichiesta, ctx, confRes);
                                                }
                                            }
                                            else
                                            {
                                                // ERRORE :-)
                                                // Vedi DOC PDF
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        #endregion
                        break;
                    case 5: //Ripianificazione ispezione
                        #region Ripianificazione ispezione
                        using (var ctx = new CriterDataModel())
                        {
                            var ispezione = ctx.V_VER_Ispezioni.Where(c => c.IDIspezione == iD).FirstOrDefault();

                            var TerzoResponsabile = UtilityLibrettiImpianti.GetDatiTerzoResponsabile(ispezione.IDLibrettoImpianto);

                            reportName = ConfigurationManager.AppSettings["ReportNameIspezionePianificazioneRipianificazione"];
                            destinationFile = ConfigurationManager.AppSettings["UploadIspezioni"] + @"\" + ispezione.IDIspezioneVisita.ToString() + @"\" + ispezione.CodiceIspezione;

                            string fileName = reportName + "_" + ispezione.IDIspezione + ".pdf";
                            string pathAndfile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;
                            if (!File.Exists(pathAndfile))
                            {
                                fileName = UtilityApp.GetFileNameFromUrl(ReportingServices.GetIspezionePianificazioneConfermaReport(iD.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite));
                            }

                            #region Invio Raccomandate
                            PosteItaliane.ROLService rolSvc = new PosteItaliane.ROLService();
                            PosteItaliane.Lettera raccomandata = new PosteItaliane.Lettera();
                            raccomandata.Mittente = new PosteItaliane.ROLWebService.Mittente()
                            {
                                Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                {
                                    RagioneSociale = mittente,
                                    Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                    {
                                        DUG = mittenteDug,
                                        Toponimo = mittenteToponimo,
                                        NumeroCivico = mittenteNumeroCivico
                                    },
                                    CAP = mittenteCap,
                                    Citta = mittenteCitta,
                                    Provincia = mittenteProvincia,
                                    TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                }
                            };
                            raccomandata.Destinatari = new PosteItaliane.ROLWebService.Destinatario[1];
                            if (TerzoResponsabile != null)
                            {
                                #region Destinatario Terzo Responsabile
                                raccomandata.Destinatari[0] = new PosteItaliane.ROLWebService.Destinatario()
                                {
                                    Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                    {
                                        ComplementoNominativo = TerzoResponsabile.RagioneSocialeTerzoResponsabile,
                                        RagioneSociale = string.Empty,
                                        Cognome = TerzoResponsabile.CognomeTerzoResponsabile,
                                        Nome = TerzoResponsabile.NomeTerzoResponsabile,
                                        Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                        {
                                            DUG = string.Empty,
                                            Toponimo = TerzoResponsabile.IndirizzoTerzoResponsabile,
                                            NumeroCivico = TerzoResponsabile.CivicoTerzoResponsabile,
                                            Esponente = string.Empty
                                        },
                                        CAP = TerzoResponsabile.CapTerzoResponsabile,
                                        Citta = TerzoResponsabile.CittaTerzoResponsabile,
                                        Provincia = TerzoResponsabile.ProvinciaTerzoResponsabile,
                                        TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                    }
                                };
                                #endregion
                            }
                            else
                            {
                                #region Destinatario Responsabile Impianto
                                raccomandata.Destinatari[0] = new PosteItaliane.ROLWebService.Destinatario()
                                {
                                    Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                    {
                                        ComplementoNominativo = ispezione.RagioneSocialeResponsabile,
                                        RagioneSociale = string.Empty,
                                        Cognome = ispezione.CognomeResponsabile,
                                        Nome = ispezione.NomeResponsabile,
                                        Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                        {
                                            DUG = string.Empty,
                                            Toponimo = ispezione.IndirizzoNormalizzatoResponsabile,
                                            NumeroCivico = ispezione.CivicoNormalizzatoResponsabile,
                                            Esponente = string.Empty
                                        },
                                        CAP = ispezione.CapResponsabile,
                                        Citta = ispezione.ComuneResponsabile,
                                        Provincia = ispezione.ProvinciaResponsabile,
                                        TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                    }
                                };
                                #endregion
                            }

                            //raccomandata.Destinatari[0] = new PosteItaliane.ROLWebService.Destinatario()
                            //{
                            //    Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                            //    {
                            //        ComplementoNominativo = ispezione.RagioneSocialeResponsabile,
                            //        RagioneSociale = string.Empty,
                            //        Cognome = ispezione.CognomeResponsabile,
                            //        Nome = ispezione.NomeResponsabile,
                            //        Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                            //        {
                            //            DUG = "",
                            //            Toponimo = ispezione.IndirizzoNormalizzatoResponsabile,
                            //            NumeroCivico = ispezione.CivicoNormalizzatoResponsabile,
                            //            Esponente = string.Empty
                            //        },
                            //        CAP = ispezione.CapResponsabile,
                            //        Citta = ispezione.ComuneResponsabile,
                            //        Provincia = ispezione.ProvinciaResponsabile,
                            //        TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                            //    }
                            //};
                            raccomandata.Documento = new PosteItaliane.Documento()
                            {
                                TipoDoc = PosteItaliane.TipoDocumento.PDF,
                                Doc = System.IO.File.ReadAllBytes(pathAndfile)
                            };

                            var richiestaInvioROL = rolSvc.RichiestaInvioRaccomandata(raccomandata, "SYSTEM", typeofRaccomandata, null, null, ispezione.IDIspezione);
                            if (richiestaInvioROL.IsSuccess)
                            {
                                // Aspetto 60 secondi
                                System.Threading.Thread.Sleep(65000);
                                // Verifico il risultato
                                var v = rolSvc.Valorizza(richiestaInvioROL.IDRichiesta, "SYSTEM");
                                // Un utente guarda il risultato
                                if (v.CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                {
                                    if (v.ServizioEnquiryResponse.Length > 0)
                                    {
                                        if (v.ServizioEnquiryResponse[0].CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                        {
                                            if (v.ServizioEnquiryResponse[0].StatoLavorazione.Id == "R")
                                            {
                                                // Logica di accettazione
                                                richiestaInvioROL.GuidUtente = v.ServizioEnquiryResponse[0].Richiesta.GuidUtente;
                                                var preConfRes = rolSvc.PreConfermaInvio(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, "SYSTEM");
                                                // Qui se volgio posso chiedere il documento prima di confermare
                                                richiestaInvioROL.IdOrdine = preConfRes.IdOrdine;
                                                var dcs = rolSvc.RecuperaDCS(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, "SYSTEM");
                                                string pdfFile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + richiestaInvioROL.IDRichiesta + ".pdf";

                                                if (dcs.Documento.Immagine != null)
                                                {
                                                    System.IO.File.WriteAllBytes(pdfFile, dcs.Documento.Immagine);
                                                }
                                                
                                                // Confermo l'invio
                                                var confRes = rolSvc.ConfermaInvio(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, richiestaInvioROL.IdOrdine, "SYSTEM");
                                                if (confRes.CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                                {
                                                    SaveNumeroRaccomandata(richiestaInvioROL.IDRichiesta, ctx, confRes);
                                                }
                                            }
                                            else
                                            {
                                                // ERRORE :-)
                                                // Vedi DOC PDF
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        #endregion
                        break;
                    case 6: //Sanzioni
                        #region Varbale di Sanzione
                        using (var ctx = new CriterDataModel())
                        {
                            var sanzione = ctx.V_VER_Accertamenti.Where(c => c.IDAccertamento == iD).FirstOrDefault();

                            reportName = ConfigurationManager.AppSettings["ReportNameSanzione"];
                            destinationFile = ConfigurationManager.AppSettings["UploadSanzioni"] + @"\" + sanzione.CodiceAccertamento;

                            string fileName = reportName + "_" + sanzione.IDAccertamento + ".pdf";
                            string pathAndfile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;
                            if (!File.Exists(pathAndfile))
                            {
                                fileName = UtilityApp.GetFileNameFromUrl(ReportingServices.GetVerbaleSanzioneReport(sanzione.IDAccertamento.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite));
                            }
                            var documento = System.IO.File.ReadAllBytes(pathAndfile);

                            #region Atto Giudiziario
                            string CodiceContratto = ConfigurationManager.AppSettings["PosteItalianeCodiceContratto"];

                            PosteItaliane.AGOLService agolSvc = new PosteItaliane.AGOLService();
                            PosteItaliane.AttoGiudiziario attoGiudiziario = new PosteItaliane.AttoGiudiziario();
                            attoGiudiziario.Intestazione = new PosteItaliane.AGOLWebService.Intestazione()
                            {
                                CodiceContratto = CodiceContratto,
                                Prodotto = ProdottoPostaEvo.AGOL_B
                            };
                            attoGiudiziario.Mittente = new PosteItaliane.AGOLWebService.Mittente()
                            {
                                Nominativo = mittente,
                                Cap = mittenteCap,
                                Comune = mittenteCitta,
                                Provincia = mittenteProvincia,
                                Indirizzo = mittenteDug + " " + mittenteToponimo + " " + mittenteNumeroCivico + " " + mittenteCap + " " + mittenteCitta + " " + mittenteProvincia,
                                Nazione = "Italia"
                            };
                            attoGiudiziario.Destinatari = new PosteItaliane.AGOLWebService.Destinatario[1];
                            attoGiudiziario.Destinatari[0] = new PosteItaliane.AGOLWebService.Destinatario()
                            {
                                Nominativo = sanzione.NomeResponsabile + " " + sanzione.CognomeResponsabile,
                                ComplementoNominativo = sanzione.RagioneSocialeResponsabile,
                                Cap = sanzione.CapResponsabile,
                                Comune = sanzione.ComuneResponsabile,
                                Provincia = sanzione.ProvinciaResponsabile,
                                Indirizzo = sanzione.IndirizzoNormalizzatoResponsabile + " " + sanzione.CivicoNormalizzatoResponsabile,
                                Nazione = "Italia"                                
                            };
                            attoGiudiziario.Documenti = new PosteItaliane.AGOLWebService.Documento[1];
                            attoGiudiziario.Documenti[0] = new PosteItaliane.AGOLWebService.Documento()
                            {
                                MD5 = PosteItaliane.Utility.CalcMD5Hash(documento),
                                Estensione = PosteItaliane.TipoDocumento.PDF,
                                Contenuto = documento
                            };

                            var richiestaInvioAGOL = agolSvc.RichiestaInvioAttoGiudiziario(attoGiudiziario, "SYSTEM", typeofRaccomandata, sanzione.IDAccertamento, null, null);
                            if (richiestaInvioAGOL.IsSuccess)
                            {
                                // Aspetto 60 secondi
                                System.Threading.Thread.Sleep(60000);
                                var dcs = agolSvc.RecuperaDocumento(richiestaInvioAGOL.IDRichiesta, CodiceContratto);
                                if (dcs.IsSuccess)
                                {
                                    string pdfFile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + richiestaInvioAGOL.IDRichiesta + ".pdf";

                                    if (dcs.Document != null)
                                    {
                                        System.IO.File.WriteAllBytes(pdfFile, dcs.Document);
                                    }
                                }

                                var responseConfermaInvio = agolSvc.ConfermaInvio(richiestaInvioAGOL.IDRichiesta, CodiceContratto);
                                if (responseConfermaInvio.IsSuccess)
                                {
                                    //var responseDocumentoFinale = agolSvc.RecuperaDocumentoFinale(richiestaInvioAGOL.IDRichiesta, CodiceContratto);
                                    //if (responseDocumentoFinale.IsSuccess)
                                    //{
                                    //    string pdfFile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + richiestaInvioAGOL.IDRichiesta + ".pdf";

                                    //    if (responseDocumentoFinale.Document != null)
                                    //    {
                                    //        System.IO.File.WriteAllBytes(pdfFile, responseDocumentoFinale.Document);
                                    //    }
                                    //}
                                    UtilityPosteItaliane.SaveNumeroAttoGiudiziario(richiestaInvioAGOL.IDRichiesta, ctx, responseConfermaInvio.NumeroRaccomandata);
                                }
                            }

                            #endregion
                        }
                        #endregion
                        break;
                    case 8: //Revoca Sanzioni
                        #region Revoca Sanzione
                        using (var ctx = new CriterDataModel())
                        {
                            var sanzione = ctx.V_VER_Accertamenti.Where(c => c.IDAccertamento == iD).FirstOrDefault();

                            reportName = ConfigurationManager.AppSettings["ReportNameRevocaSanzione"];
                            destinationFile = ConfigurationManager.AppSettings["UploadSanzioni"] + @"\" + sanzione.CodiceAccertamento;

                            string fileName = reportName + "_" + sanzione.IDAccertamento + ".pdf";
                            string pathAndfile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + fileName;
                            if (!File.Exists(pathAndfile))
                            {
                                fileName = UtilityApp.GetFileNameFromUrl(ReportingServices.GetSanzioniRevocaReport(sanzione.IDAccertamento.ToString(), reportName, reportUrl, reportPath, destinationFile, urlSite));
                            }

                            #region Invio Raccomandate
                            PosteItaliane.ROLService rolSvc = new PosteItaliane.ROLService();
                            PosteItaliane.Lettera raccomandata = new PosteItaliane.Lettera();
                            raccomandata.Mittente = new PosteItaliane.ROLWebService.Mittente()
                            {
                                Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                {
                                    RagioneSociale = mittente,
                                    Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                    {
                                        DUG = mittenteDug,
                                        Toponimo = mittenteToponimo,
                                        NumeroCivico = mittenteNumeroCivico
                                    },
                                    CAP = mittenteCap,
                                    Citta = mittenteCitta,
                                    Provincia = mittenteProvincia,
                                    TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                }
                            };
                            raccomandata.Destinatari = new PosteItaliane.ROLWebService.Destinatario[1];
                            raccomandata.Destinatari[0] = new PosteItaliane.ROLWebService.Destinatario()
                            {
                                Nominativo = new PosteItaliane.ROLWebService.Nominativo()
                                {
                                    ComplementoNominativo = sanzione.RagioneSocialeResponsabile,
                                    RagioneSociale = string.Empty,
                                    Cognome = sanzione.CognomeResponsabile,
                                    Nome = sanzione.NomeResponsabile,
                                    Indirizzo = new PosteItaliane.ROLWebService.Indirizzo()
                                    {
                                        DUG = "",
                                        Toponimo = sanzione.IndirizzoResponsabile,
                                        NumeroCivico = sanzione.CivicoResponsabile,
                                        Esponente = string.Empty
                                    },
                                    CAP = sanzione.CapResponsabile,
                                    Citta = sanzione.ComuneResponsabile,
                                    Provincia = sanzione.ProvinciaResponsabile,
                                    TipoIndirizzo = PosteItaliane.ROLWebService.NominativoTipoIndirizzo.NORMALE
                                }
                            };
                            raccomandata.Documento = new PosteItaliane.Documento()
                            {
                                TipoDoc = PosteItaliane.TipoDocumento.PDF,
                                Doc = System.IO.File.ReadAllBytes(pathAndfile)
                            };

                            var richiestaInvioROL = rolSvc.RichiestaInvioRaccomandata(raccomandata, "SYSTEM", typeofRaccomandata, sanzione.IDAccertamento, null, null);
                            if (richiestaInvioROL.IsSuccess)
                            {
                                // Aspetto 60 secondi
                                System.Threading.Thread.Sleep(65000);
                                // Verifico il risultato
                                var v = rolSvc.Valorizza(richiestaInvioROL.IDRichiesta, "SYSTEM");
                                // Un utente guarda il risultato
                                if (v.CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                {
                                    if (v.ServizioEnquiryResponse.Length > 0)
                                    {
                                        if (v.ServizioEnquiryResponse[0].CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                        {
                                            if (v.ServizioEnquiryResponse[0].StatoLavorazione.Id == "R")
                                            {
                                                // Logica di accettazione
                                                richiestaInvioROL.GuidUtente = v.ServizioEnquiryResponse[0].Richiesta.GuidUtente;
                                                var preConfRes = rolSvc.PreConfermaInvio(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, "SYSTEM");
                                                // Qui se volgio posso chiedere il documento prima di confermare
                                                richiestaInvioROL.IdOrdine = preConfRes.IdOrdine;
                                                var dcs = rolSvc.RecuperaDCS(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, "SYSTEM");
                                                string pdfFile = ConfigurationManager.AppSettings["PathDocument"] + destinationFile + @"\" + richiestaInvioROL.IDRichiesta + ".pdf";

                                                if (dcs.Documento.Immagine != null)
                                                {
                                                    System.IO.File.WriteAllBytes(pdfFile, dcs.Documento.Immagine);
                                                }

                                                // Confermo l'invio
                                                var confRes = rolSvc.ConfermaInvio(richiestaInvioROL.IDRichiesta, richiestaInvioROL.GuidUtente, richiestaInvioROL.IdOrdine, "SYSTEM");
                                                if (confRes.CEResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                                                {
                                                    fRaccomandataInviata = true;
                                                    SaveNumeroRaccomandata(richiestaInvioROL.IDRichiesta, ctx, confRes);
                                                }
                                            }
                                            else
                                            {
                                                // ERRORE :-)
                                                // Vedi DOC PDF
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        #endregion
                        break;
                }
            });

            return true;
        }

        public static string GetUrlRaccomandata(string IDRichiesta)
        {
            string PathRaccomandata = string.Empty;
            using (var ctx = new CriterDataModel())
            {
                var raccomandata = ctx.COM_Raccomandate.Where(a => a.IDRichiesta==IDRichiesta).FirstOrDefault();

                switch (raccomandata.IDRaccomandataType)
                {
                    case 1: //Accertamento
                        #region Documenti Accertamenti
                        var accertamento = ctx.VER_Accertamento.Where(c => c.IDAccertamento == raccomandata.IDAccertamento).FirstOrDefault();
                        PathRaccomandata = ConfigurationManager.AppSettings["UploadAccertamento"] + "/" + accertamento.CodiceAccertamento;
                        #endregion
                        break;
                    case 2: //Revoca
                        #region Revoca Interventi
                        var revoca = ctx.VER_Accertamento.Where(c => c.IDAccertamento == raccomandata.IDAccertamento).FirstOrDefault();
                        PathRaccomandata = ConfigurationManager.AppSettings["UploadAccertamento"] + "/" + revoca.CodiceAccertamento;
                        #endregion
                        break; 
                    case 3: //Conferma Pianificazione Ispezione
                        #region Conferma Pianificazione Ispezione
                        var ConfermaPianificazioneIspezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == raccomandata.IDIspezione).FirstOrDefault();
                        PathRaccomandata = ConfigurationManager.AppSettings["UploadIspezioni"] + "/" + ConfermaPianificazioneIspezione.IDIspezioneVisita.ToString() + "/" + ConfermaPianificazioneIspezione.CodiceIspezione;
                        #endregion
                        break;
                    case 4: //Annullamento Pianificazione ispezione
                        #region Annullamento Pianificazione ispezione
                        var AnnullamentoPianificazioneIspezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == raccomandata.IDIspezione).FirstOrDefault();
                        PathRaccomandata = ConfigurationManager.AppSettings["UploadIspezioni"] + "/" + AnnullamentoPianificazioneIspezione.IDIspezioneVisita.ToString() + "/" + AnnullamentoPianificazioneIspezione.CodiceIspezione;
                        #endregion
                        break;
                    case 5: //Ripianificazione ispezione
                        #region Ripianificazione ispezione
                        var RipianificazioneIspezione = ctx.VER_Ispezione.Where(c => c.IDIspezione == raccomandata.IDIspezione).FirstOrDefault();
                        PathRaccomandata = ConfigurationManager.AppSettings["UploadIspezioni"] + "/" + RipianificazioneIspezione.IDIspezioneVisita.ToString() + "/" + RipianificazioneIspezione.CodiceIspezione;
                        #endregion
                        break;
                    case 6: //Sanzioni
                        #region Varbale di Sanzione
                        var sanzione = ctx.VER_Accertamento.Where(c => c.IDAccertamento == raccomandata.IDAccertamento).FirstOrDefault();
                        PathRaccomandata = ConfigurationManager.AppSettings["UploadSanzioni"] + "/" + sanzione.CodiceAccertamento;
                        #endregion
                        break;
                    case 8: //Revoca Sanzioni
                        #region Revoca Sanzione
                        var RevocaSanzione = ctx.V_VER_Accertamenti.Where(c => c.IDAccertamento == raccomandata.IDAccertamento).FirstOrDefault();
                        PathRaccomandata = ConfigurationManager.AppSettings["UploadSanzioni"] + "/" + RevocaSanzione.CodiceAccertamento;
                        #endregion
                        break;
                }

                string ExistRaccomandataFile = ConfigurationManager.AppSettings["PathDocument"] + PathRaccomandata + @"\" + IDRichiesta + ".pdf";
                if (!File.Exists(ExistRaccomandataFile))
                {
                    return string.Empty;
                }
            }
            
            return ConfigurationManager.AppSettings["UrlPortal"].ToString() + PathRaccomandata + "/" + IDRichiesta + ".pdf";
        }

        public static void DownloadEsitiROLFromPosteItaliane()
        {
            var t = Task.Run(() =>
            {
                #region Recupero stato raccomandate
                using (var ctx = new CriterDataModel())
                {
                    //var filterStatiDefinitivi = new string[] {"00", "02", "99"};
                    var filterStatiDefinitivi = new string[] { "01", "03", "04" };
                    var filterStatiTransitori = new string[] { "00", "02", "99" };

                    var raccomandate = (from COM_Raccomandate in ctx.COM_Raccomandate
                                        join COM_RaccomandateSteps in ctx.COM_RaccomandateSteps on COM_Raccomandate.IDRichiesta equals COM_RaccomandateSteps.IDRichiesta
                                        where
                                          COM_Raccomandate.RisultatoCodice == "0000" &&
                                          COM_Raccomandate.fRaccomandataRecapitata == false &&
                                          COM_Raccomandate.ServiceType == ServiceType.ROL.ToString() &&
                                          COM_RaccomandateSteps.PassaggioNumero == 13 &&
                                          COM_RaccomandateSteps.RisultatoCodice == "0000" &&
                                          !filterStatiDefinitivi.Contains(COM_Raccomandate.CodiceStatoRaccomandata)
                                        select new
                                        {
                                            COM_Raccomandate.IDRichiesta,
                                            COM_Raccomandate.Passaggio,
                                            COM_Raccomandate.RisultatoTipo,
                                            COM_Raccomandate.RisultatoCodice,
                                            COM_Raccomandate.RisultatoDescrizione,
                                            COM_Raccomandate.CreatoIl,
                                            COM_Raccomandate.CreatoDa,
                                            COM_Raccomandate.ModificatoIl,
                                            COM_Raccomandate.ModificatoDa,
                                            COM_Raccomandate.IDRaccomandataType,
                                            COM_Raccomandate.IDAccertamento,
                                            COM_Raccomandate.IDAccertamentoDocumento,
                                            COM_Raccomandate.IDIspezione,
                                            COM_RaccomandateSteps.PassaggioNumero,
                                            COM_Raccomandate.fRaccomandataRecapitata,
                                            RisultatoCodiceStep = COM_RaccomandateSteps.RisultatoCodice,
                                            COM_RaccomandateSteps.UtenteGuid,
                                            COM_RaccomandateSteps.IdOrdine,
                                            COM_RaccomandateSteps.PassaggioRisultatoJson
                                        }).ToList();

                    //var numbers2 = new List<string>() 
                    //{ 
                    //    "", 
                    //    "", 
                    //    "", 
                    //    ""
                    //};

                    foreach (var raccomandata in raccomandate)
                    {
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<PosteItalianeResultRaccomandata>(raccomandata.PassaggioRisultatoJson);
                        string numeroRaccomandata = result.DestinatariRaccomandata[0].NumeroRaccomandata;
                        if (!string.IsNullOrEmpty(numeroRaccomandata))
                        {
                            PosteItaliane.ROLService rolSvc = new PosteItaliane.ROLService();
                            
                            var r = rolSvc.RecuperaStatiInvii(raccomandata.IDRichiesta.ToString(), raccomandata.UtenteGuid);
                            if (r.CeResult.Code == PosteItaliane.ROLService.CEResultCodeIsOK)
                            {
                                var jsonRes = Newtonsoft.Json.JsonConvert.SerializeObject(r);

                                if (r.ArrayDiRichieste != null)
                                {
                                    foreach (var richieste in r.ArrayDiRichieste.ToList())
                                    {
                                        var statoRichiesta = richieste.StatoRichieste.OrderByDescending(a => DateTime.Parse(a.DataEsito)).FirstOrDefault();
                                        //foreach (var statoRichiesta in richieste.StatoRichieste.ToList())
                                        //{
                                        var raccomandataAR = ctx.COM_Raccomandate.Where(a => a.IDRichiesta == raccomandata.IDRichiesta).FirstOrDefault();
                                        raccomandataAR.CodiceStatoRaccomandata = statoRichiesta.StatoType;
                                        raccomandataAR.DescrizioneStatoRaccomandata = statoRichiesta.StatoDescrizione;
                                        raccomandataAR.DataStatoRaccomandata = DateTime.Parse(statoRichiesta.DataEsito);
                                        raccomandataAR.fRaccomandataRecapitata = statoRichiesta.StatoType == "01" || statoRichiesta.StatoType == "04" ? true : false;
                                        ctx.SaveChanges();

                                        if (statoRichiesta.StatoType == "01" || statoRichiesta.StatoType == "04") //RACCOMANDATA CONSEGNATA OPPURE CONSEGNATA ONLINE
                                        {
                                            switch (raccomandata.IDRaccomandataType)
                                            {
                                                case 1: //Accertamento
                                                    #region Documenti Accertamenti
                                                    UtilityVerifiche.SetDataRicevimentoRaccomandataIntervento((long)raccomandata.IDAccertamento, DateTime.Now.ToString());
                                                    UtilityVerifiche.SetDataScadenzaAccertamento((long)raccomandata.IDAccertamento);
                                                    #endregion
                                                    break;
                                                case 2: //Revoca
                                                    #region Revoca Interventi

                                                    #endregion
                                                    break;
                                                case 3: //Conferma Pianificazione Ispezione
                                                    #region Conferma Pianificazione Ispezione

                                                    #endregion
                                                    break;
                                                case 4: //Annullamento Pianificazione ispezione
                                                    #region Annullamento Pianificazione ispezione

                                                    #endregion
                                                    break;
                                                case 5: //Ripianificazione ispezione
                                                    #region Ripianificazione ispezione

                                                    #endregion
                                                    break;
                                                case 6: //Verbale Sanzione
                                                    #region Verbale di Sanzione
                                                    UtilityVerifiche.SetDataRicevimentoRaccomandataSanzione((long)raccomandata.IDAccertamento, DateTime.Now.ToString());
                                                    UtilityVerifiche.SetDataScadenzaSanzione((long)raccomandata.IDAccertamento);
                                                    UtilityVerifiche.SetDataScadenzaPagamentoRidottoSanzione((long)raccomandata.IDAccertamento);
                                                    #endregion
                                                    break;
                                                case 8: //Revoca Sanzione
                                                    #region Revoca Sanzione
                                                    
                                                    #endregion
                                                    break;
                                            }
                                        }
                                        //}                                    
                                    }
                                }                                
                            }
                        }

                    }
                }




                #endregion
            });
        }

        public static void DownloadEsitiAGOLFromPosteItaliane()
        {
            var t = Task.Run(() =>
            {
                #region Recupero stato atto giudiziario
                using (var ctx = new CriterDataModel())
                {
                    string CodiceContratto = ConfigurationManager.AppSettings["PosteItalianeCodiceContratto"];
                    
                    var raccomandate = (from COM_Raccomandate in ctx.COM_Raccomandate
                                        join COM_RaccomandateSteps in ctx.COM_RaccomandateSteps on COM_Raccomandate.IDRichiesta equals COM_RaccomandateSteps.IDRichiesta
                                        where
                                          COM_Raccomandate.fRaccomandataRecapitata == false &&
                                          COM_Raccomandate.ServiceType == ServiceType.AGOL.ToString() &&
                                          COM_RaccomandateSteps.PassaggioNumero == 3
                                        select new
                                        {
                                            COM_Raccomandate.IDRichiesta,
                                            COM_Raccomandate.IDRaccomandataType,
                                            COM_Raccomandate.IDAccertamento
                                        }).ToList();
                    
                    PosteItaliane.AGOLService agolSvc = new PosteItaliane.AGOLService();

                    foreach (var raccomandata in raccomandate)
                    {
                        string[] IDRichiestaArray = { raccomandata.IDRichiesta };
                        var response = agolSvc.RecuperaStatoAttoGiudiziario(IDRichiestaArray, CodiceContratto);
                        if (response.IsSuccess)
                        {
                            var StatoRichiestaAttoGiudiziario = response.StatiInvii.OrderByDescending(a => a.DataUltimaModifica).FirstOrDefault();
                            
                            var AttoGiudiziario = ctx.COM_Raccomandate.Where(a => a.IDRichiesta == raccomandata.IDRichiesta).FirstOrDefault();
                            AttoGiudiziario.CodiceStatoRaccomandata = StatoRichiestaAttoGiudiziario.CodiceStatoRichiesta;
                            AttoGiudiziario.DescrizioneStatoRaccomandata = StatoRichiestaAttoGiudiziario.DescrizioneStatoRichiesta;
                            AttoGiudiziario.DataStatoRaccomandata = StatoRichiestaAttoGiudiziario.DataUltimaModifica;
                            AttoGiudiziario.fRaccomandataRecapitata = StatoRichiestaAttoGiudiziario.CodiceStatoRichiesta == "S" ? true : false;
                            ctx.SaveChanges();

                            if (StatoRichiestaAttoGiudiziario.CodiceStatoRichiesta == "S")
                            {
                                switch (raccomandata.IDRaccomandataType)
                                {
                                    case 6: //Verbale Sanzione
                                        #region Verbale di Sanzione
                                        UtilityVerifiche.SetDataRicevimentoRaccomandataSanzione((long)raccomandata.IDAccertamento, StatoRichiestaAttoGiudiziario.DataUltimaModifica.ToString());
                                        UtilityVerifiche.SetDataScadenzaSanzione((long)raccomandata.IDAccertamento);
                                        UtilityVerifiche.SetDataScadenzaPagamentoRidottoSanzione((long)raccomandata.IDAccertamento);
                                        #endregion
                                        break;
                                }
                            }
                        }
                    }
                }

                #endregion
            });
        }

        public static void SaveNumeroRaccomandata(string iDRichiesta, CriterDataModel ctx, PosteItaliane.ROLWebService.ConfermaResult confRes)
        {
            var raccomandataUpdate = ctx.COM_Raccomandate.Where(c => c.IDRichiesta == iDRichiesta).FirstOrDefault();
            raccomandataUpdate.NumeroRaccomandata = confRes.DestinatariRaccomandata.FirstOrDefault().NumeroRaccomandata;
            ctx.SaveChanges();
        }

        public static void SaveNumeroAttoGiudiziario(string iDRichiesta, CriterDataModel ctx, string NumeroRaccomandata)
        {
            var raccomandataUpdate = ctx.COM_Raccomandate.Where(c => c.IDRichiesta == iDRichiesta).FirstOrDefault();
            raccomandataUpdate.NumeroRaccomandata = NumeroRaccomandata;
            ctx.SaveChanges();
        }

        public static void SaveNoteRaccomandata(string iDRichiesta, string note)
        {
            using (var ctx = new CriterDataModel())
            {
                var raccomandata = ctx.COM_Raccomandate.Where(c => c.IDRichiesta == iDRichiesta).FirstOrDefault();
                raccomandata.Note = !string.IsNullOrEmpty(note) ? note : null;
                ctx.SaveChanges();
            }
        }

        public static string GetSqlValoriRaccomandateFilter(
                                        object iDTipoRaccomandata,
                                        object CodiceTargatura,
                                        object CodiceAccertamento,
                                        object CodiceIspezione,
                                        object EsitoSpedizione,
                                        object DataInvioDal, object DataInvioAl,
                                        object DataUltimoAggiornamentoDal, object DataUltimoAggiornamentoAl,
                                        object StatoRaccomandataROL,
                                        object StatoRaccomandataAGOL,
                                        object iDRichiesta,
                                        object NumeroRaccomandata,
                                        object TipoServizio
           )
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" * ");
            strSql.Append(" FROM V_COM_Raccomandate ");
            strSql.Append(" WHERE 1=1 ");
            if (iDTipoRaccomandata.ToString() != "0")
            {
                strSql.Append("AND IDRaccomandataType=" + iDTipoRaccomandata.ToString() + " ");
                switch (iDTipoRaccomandata.ToString())
                {
                    case "1": //Accertamento
                    case "2": //Revoca accertamento
                    case "6": //Notifica Sanzione
                    case "8": //Revoca Sanzione
                        if (!string.IsNullOrEmpty(CodiceAccertamento.ToString()))
                        {
                            strSql.Append("AND CodiceAccertamento=" + CodiceAccertamento.ToString() + " ");
                        }
                        break;
                    case "3"://Conferma pianificazione ispezione
                    case "4": //Annullamento pianificazione ispezione
                    case "5": //Ripianificazione ispezione
                        if (!string.IsNullOrEmpty(CodiceIspezione.ToString()))
                        {
                            strSql.Append("AND CodiceIspezione=" + CodiceIspezione.ToString() + " ");
                        }
                        break;
                    case "7": //Raccomandate libere

                        break;
                }
            }

            if (!string.IsNullOrEmpty(iDRichiesta.ToString()))
            {
                strSql.Append("AND iDRichiesta='" + iDRichiesta.ToString() + "' ");
            }

            if (!string.IsNullOrEmpty(NumeroRaccomandata.ToString()))
            {
                strSql.Append("AND NumeroRaccomandata=" + NumeroRaccomandata.ToString() + " ");
            }

            switch (EsitoSpedizione.ToString())
            {
                case "0": //Tutte
                    
                    break;
                case "1": //Accettate da poste
                    strSql.Append("AND fEsitoDepositoRaccomandata=1");
                    break;
                case "2": //Non accettate da poste
                    strSql.Append("AND fEsitoDepositoRaccomandata=0");
                    break;
            }

            strSql.Append("AND ServiceType='" + TipoServizio.ToString() + "'");

            if (!string.IsNullOrEmpty(StatoRaccomandataROL.ToString()))
            {
                strSql.Append("AND DescrizioneStatoRaccomandata ='" + StatoRaccomandataROL + "'");
            }

            if (!string.IsNullOrEmpty(StatoRaccomandataAGOL.ToString()))
            {
                strSql.Append("AND CodiceStatoRaccomandata ='" + StatoRaccomandataROL + "'");
            }

            if ((DataInvioDal != null) && (DataInvioDal.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), CreatoIl, 126) >= '");
                DateTime dataInvioDa = DateTime.Parse(DataInvioDal.ToString());
                string newDataInvioDa = dataInvioDa.ToString("yyyy") + "-" + dataInvioDa.ToString("MM") + "-" + dataInvioDa.ToString("dd");
                strSql.Append(newDataInvioDa);
                strSql.Append("'");
            }

            if ((DataInvioAl != null) && (DataInvioAl.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), CreatoIl, 126) <= '");
                DateTime dataInvioAl = DateTime.Parse(DataInvioAl.ToString());
                string newdataInvioAl = dataInvioAl.ToString("yyyy") + "-" + dataInvioAl.ToString("MM") + "-" + dataInvioAl.ToString("dd");
                strSql.Append(newdataInvioAl);
                strSql.Append("'");
            }

            if ((DataUltimoAggiornamentoDal != null) && (DataUltimoAggiornamentoDal.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), ModificatoIl, 126) >= '");
                DateTime dataUltimoAggiornamentoDa = DateTime.Parse(DataUltimoAggiornamentoDal.ToString());
                string newDataUltimoAggiornamentoDa = dataUltimoAggiornamentoDa.ToString("yyyy") + "-" + dataUltimoAggiornamentoDa.ToString("MM") + "-" + dataUltimoAggiornamentoDa.ToString("dd");
                strSql.Append(newDataUltimoAggiornamentoDa);
                strSql.Append("'");
            }

            if ((DataUltimoAggiornamentoAl != null) && (DataUltimoAggiornamentoAl.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), ModificatoIl, 126) <= '");
                DateTime dataUltimoAggiornamentoAl = DateTime.Parse(DataUltimoAggiornamentoAl.ToString());
                string newdataUltimoAggiornamentoAl = dataUltimoAggiornamentoAl.ToString("yyyy") + "-" + dataUltimoAggiornamentoAl.ToString("MM") + "-" + dataUltimoAggiornamentoAl.ToString("dd");
                strSql.Append(newdataUltimoAggiornamentoAl);
                strSql.Append("'");
            }

            strSql.Append(" ORDER BY CreatoIl DESC");

            return UtilityApp.SanitizeInput(strSql.ToString(), SanitizeType.Select);
        }

        public class EPM
        {
            public string TimeStamp { get; set; }
            public object TranKey { get; set; }
            public object PostmarkToken { get; set; }
        }

        public class DestinatariRaccomandata
        {
            public string IdRicevuta { get; set; }
            public string NumeroRaccomandata { get; set; }
            public EPM EPM { get; set; }
        }

        public class CEResult
        {
            public string Type { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
        }

        public class PosteItalianeResultRaccomandata
        {
            public List<DestinatariRaccomandata> DestinatariRaccomandata { get; set; }
            public CEResult CEResult { get; set; }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Pomiager.Service.Parer.RequestDto;

namespace Pomiager.Service.Parer
{
    public class ParerSip
    {
        public static Parer.RequestDto.UnitaDocumentaria GetUnitaDocumentaria(dtoSendParameters parametersPec)
        {
            //UNITA' DOCUMENTARIA
            RequestDto.UnitaDocumentaria dtoPec = new RequestDto.UnitaDocumentaria();
            dtoPec.Intestazione = new Intestazione()
            {
                Versione = ParerConfig.ParerVersion,
                Versatore = new RequestDto.Versatore()
                {
                    Ambiente = ParerConfig.ParerAmbiente,
                    Ente = ParerConfig.ParerEnte,
                    Struttura = ParerConfig.ParerStruttura,
                    UserID = ParerConfig.ParerUsername
                },
                Chiave = new RequestDto.Chiave()
                {
                    Numero = parametersPec.PecGuid,
                    Anno = parametersPec.PecSendDate.Year.ToString(),
                    TipoRegistro = "PEC_" + ParerConfig.ParerSystemSender
                },
                TipologiaUnitaDocumentaria = ParerConfig.ParerTipoDocumento
            };
            dtoPec.Configurazione = new RequestDto.Configurazione()
            {
                // Il tag sarà valorizzato con la stringa fissa "VERSAMENTO_ANTICIPATO" che definisce il processo di conservazione relativo a singole unità documentarie che possono trovarsi ancora nella fase attiva del loro ciclo di vita
                TipoConservazione = "VERSAMENTO_ANTICIPATO",
                //Definisce il comportamento del Sistema in relazione agli esiti delle verifiche di firma e / o formato dei file contenuti nel SIP.Assume valori False o True.Valore di default: False.
                //False: il Sistema accetta il versamento dell’Unità documentaria solo se tutti i controlli relativi alla firma e al formato hanno esito positivo. 
                //True: il Sistema accetta il versamento dell’Unità documentaria anche nel caso in cui almeno uno dei controlli relativi alla firma e al formato hanno esito negativo. -->
                //ForzaAccettazione = false,
                //Definisce il comportamento del Sistema in relazione al versamento di SIP contenenti file non firmati.Assume valori False o True.Valore di default: False.
                //False: il Sistema accetta il versamento dell’Unità Documentaria solo se è presente almeno un file firmato. 
                //True: il Sistema accetta il versamento dell’Unità documentaria anche nel caso in cui nessuno dei file sia firmato
                ForzaConservazione = true,
                //Definisce il comportamento del Sistema in funzione della presenza o meno nel Sistema stesso delle Unità documentarie oggetto di collegamento.Assume valori False o True.Valore di default: False.
                //False: il Sistema accetta il versamento di Unità documentarie i cui eventuali Collegamenti siano rivolti a Unità documentarie già presenti nel Sistema.
                //True: il Sistema accetta il versamento di Unità documentarie anche nel caso in cui gli eventuali Collegamenti siano rivolti a Unità documentaria non presenti nel Sistema. -- >
                ForzaCollegamento = true,
                //Definisce il comportamento del Sistema in funzione della verifica dell’hash versato.Assume valori False o True.Valore di default: False.
                //False: il Sistema accetta il versamento di Unità documentarie il cui hash dichiarato nell’Indice SIP corrisponde a quello calcolato dal sistema
                //True: il Sistema accetta il versamento di Unità documentarie il cui hash dichiarato nell’Indice SIP non corrisponde a quello calcolato dal sistema-- >
                ForzaHash = true,
                //Definisce il comportamento del Sistema in funzione del formato del numero dell’unità documentaria versata.
                //False: il Sistema accetta il versamento di Unità documentarie il cui formato del numero è coerente alla configurazione definita
                //True: il Sistema accetta il versamento di Unità documentarie il cui formato del numero non è coerente alla configurazione definite-- >
                ForzaFormatoNumero = true,
                //Definisce il comportamento del Sistema in funzione del formato del file dichiarato e di quello calcolato dal Sistema in fase di versamento.
                //False: il Sistema accetta il versamento di Unità documentarie il cui formato del file dichiarato corrisponde a quello calcolato dal Sistema in fase di versamento
                //True: il Sistema accetta il versamento di Unità documentarie il cui formato del file dichiarato non corrisponde a quello calcolato dal Sistema in fase di versamento-- >
                ForzaFormatoFile = true,
                //Definisce il comportamento del Sistema in ordine al salvataggio dei SIP versati, consentendo, a soli fini di test, di simulare un versamento senza che il relativo SIP venga memorizzato nel Sistema.
                //False: il Sistema memorizza il SIP versato
                //True: il Sistema, pur inviando al sistema versante la risposta al versamento effettuato (Esito versamento), non esegue il salvataggio dell’Unità documentaria versata, né dei relativi file.
                SimulaSalvataggioDatiInDB = false
            };
            dtoPec.ProfiloUnitaDocumentaria = new ProfiloUnitaDocumentaria()
            {
                Oggetto = parametersPec.PecObject,
                Data = parametersPec.PecSendDate.ToString("yyyy-MM-dd")
            };
            dtoPec.ProfiloNormativo = new ProfiloNormativo()
            {
                Versione = ParerConfig.ParerProfiloNormativoVersion,
                ProfiloNormativoAgid = GetProfiloNormativo(parametersPec)
            };
            dtoPec.DatiSpecifici = new DatiSpecifici()
            {
                VersioneDatiSpecifici = "1.0",
                IndirizzoPECMittente = parametersPec.PecFrom,
                IndirizzoPECDestinatario = parametersPec.PecTo,
                GestorePEC = parametersPec.PecManager,
                CodiceAttivita = parametersPec.PecAttributes,
                DataRicezione = parametersPec.PecSendDate.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"),  // Sarà la stessa dell'invio ma poi vogliono l'orario nel doc amm informatico
                DataInvio = parametersPec.PecSendDate.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"),
                Movimento = "USCITA"
            };
            dtoPec.NumeroAllegati = parametersPec.PecAttachments.Count();
            dtoPec.NumeroAnnessi = 0;
            dtoPec.NumeroAnnotazioni = 0;
            dtoPec.DocumentoPrincipale = new RequestDto.DocumentoPrincipale
            {
                IDDocumento = parametersPec.PecGuid, //parametersPec.PecDocumentMain.IDDocument,// 
                TipoDocumento = ParerConfig.ParerTipoDocumento,
                ProfiloDocumento = new ProfiloDocumento()
                {
                    Descrizione = "Invio Email Pec da procedura automatizzata " + ParerConfig.ParerSystemSender,
                    Autore = ParerConfig.ParerSystemSender
                },
                StrutturaOriginale = new StrutturaOriginale()
                {
                    TipoStruttura = "DocumentoGenerico",
                    Componenti = new RequestDto.Componenti()
                    {
                        Componente = new RequestDto.Componente()
                        {
                            ID = parametersPec.PecDocumentMain.IDDocument,
                            OrdinePresentazione = 1,
                            NomeComponente = parametersPec.PecDocumentMain.FileName,
                            FormatoFileVersato = parametersPec.PecDocumentMain.FileExtension,
                            TipoComponente = "Contenuto",
                            TipoSupportoComponente = "FILE",
                            HashVersato = UtilityHash.HashFiles(parametersPec.PecDocumentMain.StreamDocument)
                        }
                    }
                }
            };

            int i = 1;
            //Lista di allegati
            List<RequestDto.Allegato> allegati = new List<RequestDto.Allegato>();

            foreach (var allegato in parametersPec.PecAttachments)
            {
                allegati.Add(new RequestDto.Allegato()
                {
                    IDDocumento = allegato.IDAttachment + "_" + i,
                    TipoDocumento = "GENERICO", //ParerConfig.ParerTipoDocumento,
                    ProfiloDocumento = new ProfiloDocumento()
                    {
                        Descrizione = "Invio Email Pec da procedura automatizzata " + ParerConfig.ParerSystemSender,
                        Autore = ParerConfig.ParerSystemSender
                    },
                    StrutturaOriginale = new StrutturaOriginale()
                    {
                        TipoStruttura = "DocumentoGenerico",
                        Componenti = new RequestDto.Componenti()
                        {
                            Componente = new RequestDto.Componente
                            {
                                ID = allegato.IDAttachment,
                                OrdinePresentazione = i,
                                NomeComponente = allegato.FileName,
                                FormatoFileVersato = allegato.FileExtension,
                                TipoComponente = "Contenuto",
                                TipoSupportoComponente = "FILE",
                                HashVersato = UtilityHash.HashFiles(allegato.StreamAttachment)
                            }
                        }
                    }
                });
                i++;
            }

            if (allegati.Count > 0)
            {
                dtoPec.Allegati = new Allegati_()
                {
                    Allegato = allegati
                };
            }

            return dtoPec;
        }

        public static Parer.RequestDto.DocumentoAmministrativoInformatico GetProfiloNormativo(dtoSendParameters parametersPec)
        {
            List<Parer.RequestDto.IndiceAllegati> indiceAllegati = new List<IndiceAllegati>();
            foreach (PecAttachment allegato in parametersPec.PecAttachments)
            {
                indiceAllegati.Add(new Parer.RequestDto.IndiceAllegati()
                {
                    IdDoc = new IdDoc()
                    {
                        ImprontaCrittograficaDelDocumento = new ImprontaCrittograficaDelDocumento()
                        {
                            Impronta = UtilityHash.HashFiles(allegato.StreamAttachment),
                            Algoritmo = "SHA-256"
                        },
                        Identificativo = allegato.IDAttachment
                        //Segnatura = "NON_DISPONIBILE"
                    },
                    Descrizione = allegato.FileName
                });
            }

            var soggetti = new Soggetti();
            soggetti.Ruolo = new List<Ruolo>();

            var ruoloMittente = new Ruolo();
            ruoloMittente.Mittente = new Mittente();
            ruoloMittente.Mittente.TipoRuolo = "Mittente";
            ruoloMittente.Mittente.PG = new PG();
            ruoloMittente.Mittente.PG.DenominazioneOrganizzazione = "Arter SpA";
            ruoloMittente.Mittente.PG.CodiceFiscale_PartitaIva = "03786281208";
            ruoloMittente.Mittente.PG.IndirizziDigitaliDiRiferimento = new List<string>()
            {
                parametersPec.PecFrom
            };
            soggetti.Ruolo.Add(ruoloMittente);

            if (parametersPec.PecToType == TypePecToTypeEnum.PF.ToString())
            {
                var ruoloDestinatarioPF = new Ruolo();
                ruoloDestinatarioPF.Destinatario = new Destinatario();
                ruoloDestinatarioPF.Destinatario.TipoRuolo = "Destinatario";
                ruoloDestinatarioPF.Destinatario.PF = new PF();
                ruoloDestinatarioPF.Destinatario.PF.Nome = parametersPec.PecToName;
                ruoloDestinatarioPF.Destinatario.PF.Cognome = parametersPec.PecToSurname;
                ruoloDestinatarioPF.Destinatario.PF.IndirizziDigitaliDiRiferimento = new List<string>()
                {
                    parametersPec.PecTo
                };
                
                soggetti.Ruolo.Add(ruoloDestinatarioPF);
            }
            else if (parametersPec.PecToType == TypePecToTypeEnum.PG.ToString())
            {
                var ruoloDestinatarioPG = new Ruolo();
                ruoloDestinatarioPG.Destinatario = new Destinatario();
                ruoloDestinatarioPG.Destinatario.TipoRuolo = "Destinatario";
                ruoloDestinatarioPG.Destinatario.PG = new PG();
                ruoloDestinatarioPG.Destinatario.PG.DenominazioneOrganizzazione = parametersPec.PecToName;
                ruoloDestinatarioPG.Destinatario.PG.CodiceFiscale_PartitaIva = "00000000000";
                ruoloDestinatarioPG.Destinatario.PG.IndirizziDigitaliDiRiferimento = new List<string>()
                {
                    parametersPec.PecTo
                };
                soggetti.Ruolo.Add(ruoloDestinatarioPG);
            }
            

            Parer.RequestDto.DocumentoAmministrativoInformatico dtoProfNorm = new Parer.RequestDto.DocumentoAmministrativoInformatico()
            {
                IdDoc = new IdDoc()
                {
                    Identificativo = parametersPec.PecGuid.ToString(),
                    Segnatura = "NON_DISPONIBILE",
                    ImprontaCrittograficaDelDocumento = new ImprontaCrittograficaDelDocumento()
                    {
                        Impronta = UtilityHash.HashFiles(parametersPec.PecDocumentMain.StreamDocument),
                        Algoritmo = "SHA-256"
                    }
                },
                ModalitaDiFormazione = "acquisizione di un documento informatico per via telematica o su supporto informatico, acquisizione della copia per immagine su supporto informatico di un documento analogico, acquisizione della copia informatica di un documento analogico",
                TipologiaDocumentale = "Corrispondenza – PEC",
                DatiDiRegistrazione = new DatiDiRegistrazione()
                {
                    TipologiaDiFlusso = "U",
                    TipoRegistro = new TipoRegistro()
                    {
                        Repertorio_Registro = new Repertorio_Registro()
                        {
                            TipoRegistro = @"Repertorio\Registro",  //"fixed" secondo l'xml di riferimento
                            DataRegistrazioneDocumento = parametersPec.PecSendDate.Date.ToString("yyyy-MM-dd"),
                            NumeroRegistrazioneDocumento = parametersPec.PecGuid, //pattern '[0-9]{7,}' for type 'NumProtType'
                            CodiceRegistro = parametersPec.PecGuid  //pattern '[A-Za-z0-9_.-]{1,16} for type 'CodiceRegistroType'
                        }
                    }
                },
                Soggetti = soggetti,
                //Soggetti = new Soggetti()
                //{
                //    Ruolo = new List<Ruolo>()
                //    {
                //        new Ruolo()
                //        {
                //            Mittente = new Mittente()
                //            {
                //                TipoRuolo = "Mittente",
                //                PG = new PG()
                //                {
                //                    DenominazioneOrganizzazione = "Arter SpA",
                //                    CodiceFiscale_PartitaIva = "03786281208",
                //                    IndirizziDigitaliDiRiferimento = new List<string>()
                //                    {
                //                        parametersPec.PecFrom
                //                    }
                //                }
                //            },



                //            //AmministrazioneCheEffettuaLaRegistrazione = new AmministrazioneCheEffettuaLaRegistrazione()
                //            //{
                //            //    TipoRuolo = "Amministrazione Che Effettua La Registrazione",
                //            //    PAI = new PAI()
                //            //    {
                //            //        IPAAmm = new IPAAmm()
                //            //        {
                //            //            CodiceIPA = "NON_DISPONIBILE",
                //            //            Denominazione = "NON_DISPONIBILE"
                //            //        },
                //            //        IPAAOO = new IPAAOO()
                //            //        {
                //            //            CodiceIPA = "NON_DISPONIBILE",
                //            //            Denominazione = "NON_DISPONIBILE"
                //            //        },
                //            //        IPAUOR = new IPAUOR()
                //            //        {
                //            //            CodiceIPA = "NON_DISPONIBILE",
                //            //            Denominazione = "NON_DISPONIBILE"
                //            //        },
                //            //        IndirizziDigitaliDiRiferimento = new List<string>()
                //            //        {
                //            //            "NON_DISPONIBILE"
                //            //        }
                //            //    }
                //            //}
                //        },
                //        new Ruolo()
                //        {
                //            Destinatario = new Destinatario()
                //            {
                //                TipoRuolo = "Destinatario",
                //                PF = new PF()
                //                {
                //                    Nome = parametersPec.PecToName,
                //                    Cognome = parametersPec.PecToSurname,
                //                    IndirizziDigitaliDiRiferimento = new List<string>()
                //                    {
                //                        parametersPec.PecTo
                //                    }
                //                },
                //                PG = new PG()
                //                { 
                //                     DenominazioneOrganizzazione = parametersPec.PecToName,
                //                     CodiceFiscale_PartitaIva = "NON_DISPONIBILE",
                //                     IndirizziDigitaliDiRiferimento = new List<string>()
                //                     {
                //                        parametersPec.PecTo
                //                     }
                //                }
                //            }
                //        }
                //    }
                //},

                ChiaveDescrittiva = new ChiaveDescrittiva()
                {
                    Oggetto = parametersPec.PecObject
                },
                Allegati = new RequestDto.Allegati()
                {
                    NumeroAllegati = parametersPec.PecAttachments.Count.ToString(),
                    IndiceAllegati = indiceAllegati
                },
                Classificazione = new Classificazione()
                {
                    IndiceDiClassificazione = "NON_DISPONIBILE",
                    Descrizione = "NON_DISPONIBILE",
                    PianoDiClassificazione = "NON_DISPONIBILE"
                },
                Riservato = false,
                IdentificativoDelFormato = new IdentificativoDelFormato()
                {
                    Formato = parametersPec.PecDocumentMain.FileExtension,
                    ProdottoSoftware = new ProdottoSoftware()
                    {
                        NomeProdotto = ParerConfig.ParerSystemSender
                        //VersioneProdotto = null,
                        //Produttore = null
                    }
                },
                Verifica = new Verifica()
                {
                    FirmatoDigitalmente = false,
                    SigillatoElettronicamente = false,
                    MarcaturaTemporale = false,
                    ConformitaCopieImmagineSuSupportoInformatico = false
                },
                NomeDelDocumento = parametersPec.PecObject,
                //Note = "NON_DISPONIBILE", 
                //TempoDiConservazione = "NON_DISPONIBILE", 
                VersioneDelDocumento = "NON_DISPONIBILE",
                //TracciatureModificheDocumento = new TracciatureModificheDocumento()
                //{ 
                //  DataModifica = "NON_DISPONIBILE", 
                //  OraModifica = "NON_DISPONIBILE", 
                //  TipoModifica = "NON_DISPONIBILE", 
                //  SoggettoAutoreDellaModifica = new SoggettoAutoreDellaModifica()
                //  { 

                //  }
                //}, 
                Agg = new Agg()
                {
                    TipoAgg = new List<TipoAgg>()
                    {
                        new TipoAgg()
                        {
                            IdAggregazione = parametersPec.PecGuid,
                            TipoAggregazione = "Serie Documentale"
                        }
                    }
                },
                IdIdentificativoDocumentoPrimario = new IdIdentificativoDocumentoPrimario()
                {
                    Identificativo = parametersPec.PecDocumentMain.IDDocument,
                    //Segnatura = "NON_DISPONIBILE",
                    ImprontaCrittograficaDelDocumento = new ImprontaCrittograficaDelDocumento()
                    {
                        Impronta = UtilityHash.HashFiles(parametersPec.PecDocumentMain.StreamDocument),
                        Algoritmo = "SHA-256"
                    }
                }
                //NomeDelDocumento = parametersPec.PecAttachments.First().FileName,
                //VersioneDelDocumento = "Versione univoca",
                //TracciatureModificheDocumento = new List<Modifica>()
                //{
                //    // lista vuota di oggetti "modifica"
                //}
                // TempoDiConservazione = 0, facoltativo
                // Note = null facoltativo
            };

            return dtoProfNorm;
        }
    }
}

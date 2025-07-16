using System.Collections.Generic;
using System.Linq;
using Criter.Libretto;
using Criter.Rapporti;
using Criter.Anagrafica;
using Criter.Lookup;
using System.Linq.Dynamic;
using Criter.Login;
using System.Configuration;
using POMJ_Lib.Lookup;
using System;
using DataLayer;
using DataUtilityCore.SaceDto;
using System.Data.Entity;
using Bender.Collections;
using DocumentFormat.OpenXml.Math;

namespace CriterAPI.DataSource
{
    public class EFCriterDataSource : ICriterDataSource
    {
        DataLayer.CriterDataModel db;
        public EFCriterDataSource()
        {
            db = new DataLayer.CriterDataModel();
        }

        #region Anagrafiche
        public POMJ_AnagraficaSoggetti GetAnagraficaImpresaByPIva(string Piva)
        {
            POMJ_AnagraficaSoggetti anagrafica = new POMJ_AnagraficaSoggetti();

            var ef_anagrafica = (from a in db.COM_AnagraficaSoggetti
                                 where (a.PartitaIVA == Piva &&
                                        a.IDSoggettoDerived == null &&
                                        a.CodiceSoggetto != null &&
                                        a.IDTipoSoggetto == 2 &&
                                        a.fIscrizione == true)
                                 select a).FirstOrDefault();

            if (ef_anagrafica != null)
            {
                anagrafica.IDSoggetto = ef_anagrafica.IDSoggetto;
                if (!string.IsNullOrEmpty(ef_anagrafica.CodiceSoggetto))
                {
                    anagrafica.CodiceSoggetto = ef_anagrafica.CodiceSoggetto;
                }
                if (anagrafica.IDSoggettoDerived != null)
                {
                    anagrafica.IDSoggettoDerived = ef_anagrafica.IDSoggettoDerived;
                }
                anagrafica.TipoSoggetto = ef_anagrafica.IDTipoSoggetto;
                anagrafica.NomeAzienda = ef_anagrafica.NomeAzienda;
                anagrafica.IDFormaGiuridica = ef_anagrafica.IDFormaGiuridica;
                anagrafica.IDPaeseSedeLegale = ef_anagrafica.IDPaeseSedeLegale;
                if (!string.IsNullOrEmpty(ef_anagrafica.IndirizzoSedeLegale))
                {
                    anagrafica.IndirizzoSedeLegale = ef_anagrafica.IndirizzoSedeLegale;
                }
                if (!string.IsNullOrEmpty(ef_anagrafica.CapSedeLegale))
                {
                    anagrafica.CapSedeLegale = ef_anagrafica.CapSedeLegale;
                }
                if (!string.IsNullOrEmpty(ef_anagrafica.CittaSedeLegale))
                {
                    anagrafica.CittaSedeLegale = ef_anagrafica.CittaSedeLegale;
                }
                if (!string.IsNullOrEmpty(ef_anagrafica.NumeroCivicoSedeLegale))
                {
                    anagrafica.NumeroCivicoSedeLegale = ef_anagrafica.NumeroCivicoSedeLegale;
                }
                if (ef_anagrafica.IDProvinciaSedeLegale != null)
                {
                    anagrafica.IDProvinciaSedeLegale = ef_anagrafica.IDProvinciaSedeLegale;
                }
                if (!string.IsNullOrEmpty(ef_anagrafica.Nome))
                {
                    anagrafica.Nome = ef_anagrafica.Nome;
                }
                if (!string.IsNullOrEmpty(anagrafica.Cognome))
                {
                    anagrafica.Cognome = ef_anagrafica.Cognome;
                }
                if (!string.IsNullOrEmpty(ef_anagrafica.Fax))
                {
                    anagrafica.Fax = ef_anagrafica.Fax;
                }
                if (!string.IsNullOrEmpty(ef_anagrafica.Telefono))
                {
                    anagrafica.Telefono = ef_anagrafica.Telefono;
                }
                if (!string.IsNullOrEmpty(ef_anagrafica.Email))
                {
                    anagrafica.Email = ef_anagrafica.Email;
                }
                if (!string.IsNullOrEmpty(ef_anagrafica.EmailPec))
                {
                    anagrafica.EmailPec = ef_anagrafica.EmailPec;
                }
                if (!string.IsNullOrEmpty(ef_anagrafica.PartitaIVA))
                {
                    anagrafica.PartitaIVA = ef_anagrafica.PartitaIVA;
                }
                if (!string.IsNullOrEmpty(ef_anagrafica.CodiceFiscale))
                {
                    anagrafica.CodiceFiscale = ef_anagrafica.CodiceFiscaleAzienda;
                }
                if (!string.IsNullOrEmpty(ef_anagrafica.SitoWeb))
                {
                    anagrafica.SitoWeb = ef_anagrafica.SitoWeb;
                }
                if (!string.IsNullOrEmpty(ef_anagrafica.NumeroIscrizioneAlboImprese))
                {
                    anagrafica.NumeroIscrizioneCCIAA = ef_anagrafica.NumeroIscrizioneAlboImprese;
                }
                if (anagrafica.IDProvinciaIscrizioneAlboImprese != null)
                {
                    anagrafica.IDProvinciaIscrizioneAlboImprese = ef_anagrafica.IDProvinciaIscrizioneAlboImprese;
                }
                anagrafica.fIscrizioneRegistroGasFluorurati = ef_anagrafica.fIscrizioneRegistroGasFluorurati;
                if (!string.IsNullOrEmpty(ef_anagrafica.NumeroIscrizioneRegistroGasFluorurati))
                {
                    anagrafica.NumeroIscrizioneRegistroGasFluorurati = ef_anagrafica.NumeroIscrizioneRegistroGasFluorurati;
                }
                anagrafica.fAttivo = ef_anagrafica.fAttivo;
            }
            else
            {
                anagrafica = null;
            }

            return anagrafica;
        }

        public string GetApiKeyImpresaByPIva(string Piva)
        {
            string KeyApi = string.Empty;

            var ef_utente = (from a in db.COM_AnagraficaSoggetti
                             join l in db.COM_Utenti on a.IDSoggetto equals l.IDSoggetto
                             where (a.PartitaIVA == Piva &&
                                    a.IDSoggettoDerived == null &&
                                    a.CodiceSoggetto != null &&
                                    a.IDTipoSoggetto == 2 &&
                                    a.fIscrizione == true)
                             select new
                             {
                                 KeyApi = l.KeyApi
                             }
                                 ).FirstOrDefault();

            if (ef_utente != null)
            {
                if (!string.IsNullOrEmpty(ef_utente.KeyApi))
                {
                    KeyApi = ef_utente.KeyApi;
                }
            }

            return KeyApi;
        }

        public string GetApiKeyImpresa(int iDSoggetto)
        {
            string ApiKey = string.Empty;

            var ef_anagrafica = (from a in db.COM_AnagraficaSoggetti
                                 where (a.IDSoggetto == iDSoggetto)
                                 select a).FirstOrDefault();



            if (ef_anagrafica != null)
            {
                int? iDSoggettoCalcolato = null;
                if (ef_anagrafica.IDTipoSoggetto == 1) //Manutentore
                {
                    iDSoggettoCalcolato = ef_anagrafica.IDSoggettoDerived;
                }
                else if (ef_anagrafica.IDTipoSoggetto == 2) //Impresa
                {
                    iDSoggettoCalcolato = ef_anagrafica.IDSoggetto;
                }

                var ef_utente = (from a in db.COM_Utenti
                                 where (a.IDSoggetto == iDSoggettoCalcolato)
                                 select new
                                 {
                                     apiKey = a.KeyApi
                                 }
                                 ).FirstOrDefault();

                if (!string.IsNullOrEmpty(ef_utente.apiKey))
                {
                    ApiKey = ef_utente.apiKey;
                }
            }

            return ApiKey;
        }

        public List<POMJ_AnagraficaSoggetti> GetAnagraficaManutentoriByIDAzienda(int IDSoggetto)
        {
            List<POMJ_AnagraficaSoggetti> manutentori = new List<POMJ_AnagraficaSoggetti>();

            var ef_manutentori = (from a in db.COM_AnagraficaSoggetti
                                  where (a.IDSoggettoDerived == IDSoggetto &&
                                         //a.CodiceSoggetto != null &&
                                         a.IDTipoSoggetto == 1 &&
                                         a.fIscrizione == true)
                                  select a).ToList();

            if (ef_manutentori.Count > 0)
            {
                foreach (var m in ef_manutentori)
                {
                    manutentori.Add(new POMJ_AnagraficaSoggetti
                    {
                        IDSoggetto = m.IDSoggetto,
                        CodiceSoggetto = m.CodiceSoggetto,
                        IDSoggettoDerived = m.IDSoggettoDerived,
                        TipoSoggetto = m.IDTipoSoggetto,
                        IDPaeseSedeLegale = m.IDPaeseSedeLegale,
                        IndirizzoSedeLegale = m.IndirizzoSedeLegale,
                        CapSedeLegale = m.CapSedeLegale,
                        CittaSedeLegale = m.CittaSedeLegale,
                        NumeroCivicoSedeLegale = m.NumeroCivicoSedeLegale,
                        IDProvinciaSedeLegale = m.IDProvinciaSedeLegale,
                        Nome = m.Nome,
                        Cognome = m.Cognome,
                        CodiceFiscale = m.CodiceFiscale,
                        Telefono = m.Telefono,
                        Fax = m.Telefono,
                        Email = m.Email,
                        EmailPec = m.EmailPec,
                        fAttivo = m.fAttivo
                    });
                }
            }
            else
            {
                manutentori = null;
            }

            return manutentori;
        }

        #endregion

        #region Login
        public POMJ_Login GetLoginCriter(string username, string password)
        {
            POMJ_Login utente = new POMJ_Login();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                DataUtilityCore.SecurityManager.LoginStatus status = DataUtilityCore.SecurityManager.ValidateUserApp(username, password, "fAttivo");
                switch (status)
                {
                    case DataUtilityCore.SecurityManager.LoginStatus.LoginOK:
                        int iDSoggetto = int.Parse(DataUtilityCore.SecurityManager.GetUserIDSoggettoDaUsername(username));
                        utente.esitoLogin = true;
                        utente.statusLogin = "Autenticazione a Criter effettuata con successo!";
                        utente.criterApiKeySoggetto = GetApiKeyImpresa(iDSoggetto);
                        POMJ_AnagraficaSoggetti anagrafica = new POMJ_AnagraficaSoggetti();
                        #region Anagrafica
                        var ef_anagrafica = (from a in db.COM_AnagraficaSoggetti
                                             where (a.IDSoggetto == iDSoggetto)
                                             select a).FirstOrDefault();

                        if (ef_anagrafica != null)
                        {
                            anagrafica.IDSoggetto = ef_anagrafica.IDSoggetto;
                            if (!string.IsNullOrEmpty(ef_anagrafica.CodiceSoggetto))
                            {
                                anagrafica.CodiceSoggetto = ef_anagrafica.CodiceSoggetto;
                            }
                            if (ef_anagrafica.IDSoggettoDerived != null)
                            {
                                anagrafica.IDSoggettoDerived = ef_anagrafica.IDSoggettoDerived;
                            }
                            anagrafica.TipoSoggetto = ef_anagrafica.IDTipoSoggetto;
                            if (ef_anagrafica.IDTipoSoggetto == 1)
                            {
                                var ef_anagraficaAzienda = (from a in db.COM_AnagraficaSoggetti
                                                            where (a.IDSoggetto == ef_anagrafica.IDSoggettoDerived)
                                                            select a).FirstOrDefault();

                                anagrafica.NomeAzienda = ef_anagraficaAzienda.NomeAzienda;
                            }
                            else
                            {
                                anagrafica.NomeAzienda = ef_anagrafica.NomeAzienda;
                            }

                            anagrafica.IDFormaGiuridica = ef_anagrafica.IDFormaGiuridica;
                            anagrafica.IDPaeseSedeLegale = ef_anagrafica.IDPaeseSedeLegale;
                            if (!string.IsNullOrEmpty(ef_anagrafica.IndirizzoSedeLegale))
                            {
                                anagrafica.IndirizzoSedeLegale = ef_anagrafica.IndirizzoSedeLegale;
                            }
                            if (!string.IsNullOrEmpty(ef_anagrafica.CapSedeLegale))
                            {
                                anagrafica.CapSedeLegale = ef_anagrafica.CapSedeLegale;
                            }
                            if (!string.IsNullOrEmpty(ef_anagrafica.CittaSedeLegale))
                            {
                                anagrafica.CittaSedeLegale = ef_anagrafica.CittaSedeLegale;
                            }
                            if (!string.IsNullOrEmpty(ef_anagrafica.NumeroCivicoSedeLegale))
                            {
                                anagrafica.NumeroCivicoSedeLegale = ef_anagrafica.NumeroCivicoSedeLegale;
                            }
                            if (ef_anagrafica.IDProvinciaSedeLegale != null)
                            {
                                anagrafica.IDProvinciaSedeLegale = ef_anagrafica.IDProvinciaSedeLegale;
                            }
                            if (!string.IsNullOrEmpty(ef_anagrafica.Nome))
                            {
                                anagrafica.Nome = ef_anagrafica.Nome;
                            }
                            if (!string.IsNullOrEmpty(ef_anagrafica.Cognome))
                            {
                                anagrafica.Cognome = ef_anagrafica.Cognome;
                            }
                            if (!string.IsNullOrEmpty(ef_anagrafica.Fax))
                            {
                                anagrafica.Fax = ef_anagrafica.Fax;
                            }
                            if (!string.IsNullOrEmpty(ef_anagrafica.Telefono))
                            {
                                anagrafica.Telefono = ef_anagrafica.Telefono;
                            }
                            if (!string.IsNullOrEmpty(ef_anagrafica.Email))
                            {
                                anagrafica.Email = ef_anagrafica.Email;
                            }
                            if (!string.IsNullOrEmpty(ef_anagrafica.EmailPec))
                            {
                                anagrafica.EmailPec = ef_anagrafica.EmailPec;
                            }
                            if (!string.IsNullOrEmpty(ef_anagrafica.PartitaIVA))
                            {
                                anagrafica.PartitaIVA = ef_anagrafica.PartitaIVA;
                            }
                            if (!string.IsNullOrEmpty(ef_anagrafica.CodiceFiscale))
                            {
                                anagrafica.CodiceFiscale = ef_anagrafica.CodiceFiscaleAzienda;
                            }
                            if (!string.IsNullOrEmpty(ef_anagrafica.SitoWeb))
                            {
                                anagrafica.SitoWeb = ef_anagrafica.SitoWeb;
                            }
                            if (!string.IsNullOrEmpty(ef_anagrafica.NumeroIscrizioneAlboImprese))
                            {
                                anagrafica.NumeroIscrizioneCCIAA = ef_anagrafica.NumeroIscrizioneAlboImprese;
                            }
                            if (anagrafica.IDProvinciaIscrizioneAlboImprese != null)
                            {
                                anagrafica.IDProvinciaIscrizioneAlboImprese = ef_anagrafica.IDProvinciaIscrizioneAlboImprese;
                            }
                            anagrafica.fIscrizioneRegistroGasFluorurati = ef_anagrafica.fIscrizioneRegistroGasFluorurati;
                            if (!string.IsNullOrEmpty(ef_anagrafica.NumeroIscrizioneRegistroGasFluorurati))
                            {
                                anagrafica.NumeroIscrizioneRegistroGasFluorurati = ef_anagrafica.NumeroIscrizioneRegistroGasFluorurati;
                            }
                            anagrafica.fAttivo = ef_anagrafica.fAttivo;
                        }
                        else
                        {
                            anagrafica = null;
                        }
                        #endregion
                        utente.userData = anagrafica;
                        break;
                    case DataUtilityCore.SecurityManager.LoginStatus.LoginKO:
                        utente.esitoLogin = false;
                        utente.statusLogin = "Autenticazione fallita: controllare username e/o password!";
                        utente.criterApiKeySoggetto = string.Empty;
                        utente.userData = null;
                        break;
                    case DataUtilityCore.SecurityManager.LoginStatus.LoginFailed:
                        utente.esitoLogin = false;
                        utente.statusLogin = "Autenticazione fallita: controllare username e/o password!";
                        utente.criterApiKeySoggetto = string.Empty;
                        utente.userData = null;
                        break;
                    case DataUtilityCore.SecurityManager.LoginStatus.LoginExpired:
                        utente.esitoLogin = false;
                        utente.statusLogin = "Autenticazione fallita: password scaduta!";
                        utente.criterApiKeySoggetto = string.Empty;
                        utente.userData = null;
                        break;
                    case DataUtilityCore.SecurityManager.LoginStatus.LoginInactive:
                        utente.esitoLogin = false;
                        utente.statusLogin = "Autenticazione fallita: account inutilizzato da più di 180 giorni!";
                        utente.criterApiKeySoggetto = string.Empty;
                        utente.userData = null;
                        break;
                    case DataUtilityCore.SecurityManager.LoginStatus.LoginLocked:
                        utente.esitoLogin = false;
                        utente.statusLogin = "Autenticazione fallita: account bloccato, superato il numero massimo di tentativi " + ConfigurationManager.AppSettings["logonRetriesMax"] + "!";
                        utente.criterApiKeySoggetto = string.Empty;
                        utente.userData = null;
                        break;
                }
            }
            else
            {
                utente.esitoLogin = false;
                utente.statusLogin = "Autenticazione fallita: username e password non possono essere vuoti!";
                utente.criterApiKeySoggetto = string.Empty;
                utente.userData = null;
            }

            return utente;
        }
        #endregion

        public POMJ_LibrettoImpianto GetLibrettoByCodiceImpianto(string codiceImpianto)
        {
            POMJ_LibrettoImpianto lib = new POMJ_LibrettoImpianto();

            var res = from l in db.LIM_LibrettiImpianti
                      .Include("COM_AnagraficaSoggetti")
                      .Include("SYS_StatoLibrettoImpianto")
                      .Include("SYS_TipologiaIntervento")
                      .Include("SYS_CodiciCatastali")
                      //.Include("SYS_CodiciCatastaliSezioni")
                      .Include("SYS_TipologiaProtezioneGelo")
                      .Include("SYS_TipologiaCircuitoRaffreddamento")
                      .Include("SYS_TipologiaAcquaAlimento")

                      .Include("SYS_TipologiaMacchineFrigorifere")
                      .Include("SYS_SorgenteLatoEsterno")
                      .Include("SYS_FluidoLatoUtenze")
                      .Include("SYS_TipologiaImpiantiVMC")

                      .Include("LIM_LibrettiImpiantiImpiantiVMC")
                      .Include("LIM_LibrettiImpiantiDatiCatastali")
                      .Include("LIM_LibrettiImpiantiResponsabili")
                      .Include("LIM_LibrettiImpiantiAddolcimentoAcqua")
                      .Include("LIM_LibrettiImpiantiGruppiTermici")
                      .Include("LIM_LibrettiImpiantiScambiatoriCalore")
                      .Include("LIM_LibrettiImpiantiMacchineFrigorifere")
                      .Include("LIM_LibrettiImpiantiCogeneratori")
                      .Include("LIM_LibrettiImpiantiCampiSolariTermici")
                      .Include("LIM_LibrettiImpiantiAltriGeneratori")
                      .Include("LIM_LibrettiImpiantiSistemiRegolazione")
                      .Include("LIM_LibrettiImpiantiValvoleRegolazione")
                      .Include("LIM_LibrettiImpiantiVasiEspansione")
                      .Include("LIM_LibrettiImpiantiPompeCircolazione")
                      .Include("LIM_LibrettiImpiantiAccumuli")
                      .Include("LIM_LibrettiImpiantiTorriEvaporative")
                      .Include("LIM_LibrettiImpiantiRaffreddatoriLiquido")
                      .Include("LIM_LibrettiImpiantiScambiatoriCaloreIntermedi")
                      .Include("LIM_LibrettiImpiantiCircuitiInterrati")
                      .Include("LIM_LibrettiImpiantiUnitaTrattamentoAria")
                      .Include("LIM_LibrettiImpiantiRecuperatoriCalore")

                      .Include("LIM_LibrettiImpiantiConsumoCombustibile")
                      .Include("LIM_LibrettiImpiantiConsumoEnergiaElettrica")
                      .Include("LIM_LibrettiImpiantiConsumoAcqua")
                      .Include("LIM_LibrettiImpiantiConsumoProdottiChimici")
                      join t in db.LIM_TargatureImpianti on l.IDTargaturaImpianto equals t.IDTargaturaImpianto
                      //join s in db.COM_AnagraficaSoggetti on l.IDSoggetto equals s.IDSoggetto
                      where (t.CodiceTargatura == codiceImpianto && l.fAttivo == true)
                      select l;

            var EF_lib = res.FirstOrDefault();
            if (EF_lib != null)
            {
                lib.CodiceSoggetto = EF_lib.COM_AnagraficaSoggetti1.CodiceSoggetto;

                lib.NomeAzienda = EF_lib.COM_AnagraficaSoggetti1.NomeAzienda;
                lib.Nome = EF_lib.COM_AnagraficaSoggetti.Nome;
                lib.Cognome = EF_lib.COM_AnagraficaSoggetti.Cognome;
                lib.CodiceTargaturaImpianto = new POMJ_TargaturaImpianto()
                {
                    CodiceTargatura = EF_lib.LIM_TargatureImpianti.CodiceTargatura
                };
                lib.StatoLibrettoImpianto = EF_lib.IDStatoLibrettoImpianto;
                lib.NumeroRevisione = EF_lib.NumeroRevisione;

                lib.DataIntervento = EF_lib.DataIntervento;
                lib.TipologiaIntervento = EF_lib.IDTipologiaIntervento.Value;

                lib.Indirizzo = EF_lib.Indirizzo;
                lib.Civico = EF_lib.Civico;
                lib.Palazzo = EF_lib.Palazzo;
                lib.Scala = EF_lib.Scala;
                lib.Interno = EF_lib.Interno;
                lib.CodiceCatastaleComune = EF_lib.SYS_CodiciCatastali.CodiceCatastale;

                lib.DatiCatastali = new List<POMJ_DatiCatastali>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiDatiCatastali)
                {
                    POMJ_DatiCatastali catastali = new POMJ_DatiCatastali()
                    {
                        Foglio = item.Foglio,
                        Identificativo = item.Identificativo,
                        Mappale = item.Mappale,
                        Subalterno = item.Subalterno
                        //CodiceSezione = item.SYS_CodiciCatastaliSezioni.CodiceSezione
                    };
                    lib.DatiCatastali.Add(catastali);
                }

                lib.fUnitaImmobiliare = EF_lib.fUnitaImmobiliare;
                lib.DestinazioneUso = EF_lib.IDDestinazioneUso;
                lib.VolumeLordoRiscaldato = EF_lib.VolumeLordoRiscaldato;
                lib.VolumeLordoRaffrescato = EF_lib.VolumeLordoRaffrescato;
                lib.NumeroAPE = EF_lib.NumeroAPE;
                lib.NumeroPDR = EF_lib.NumeroPDR;
                lib.NumeroPOD = EF_lib.NumeroPOD;

                lib.fAcs = EF_lib.fAcs;
                lib.PotenzaAcs = EF_lib.PotenzaAcs;
                lib.fClimatizzazioneInvernale = EF_lib.fClimatizzazioneInvernale;
                lib.PotenzaClimatizzazioneInvernale = EF_lib.PotenzaClimatizzazioneInvernale;
                lib.fClimatizzazioneEstiva = EF_lib.fClimatizzazioneEstiva;
                lib.PotenzaClimatizzazioneEstiva = EF_lib.PotenzaClimatizzazioneEstiva;
                lib.fClimatizzazioneAltro = EF_lib.fClimatizzazioneAltro;
                lib.ClimatizzazioneAltro = EF_lib.ClimatizzazioneAltro;

                lib.TipologiaFluidoVettore = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTipologiaFluidoVettore)
                {
                    lib.TipologiaFluidoVettore.Add(item.IDTipologiaFluidoVettore);
                }

                var tipFluidoVettoreAltro = EF_lib.LIM_LibrettiImpiantiTipologiaFluidoVettore.Where(a => a.IDTipologiaFluidoVettore == 1).FirstOrDefault();
                if (tipFluidoVettoreAltro != null)
                {
                    lib.TipologiaFluidoVettoreAltro = tipFluidoVettoreAltro.TipologiaFluidoVettoreAltro;
                }

                lib.TipologiaGeneratori = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTipologiaGeneratori)
                {
                    lib.TipologiaGeneratori.Add(item.IDTipologiaGeneratori);
                }

                var tipTipologiaGeneratoriAltro = EF_lib.LIM_LibrettiImpiantiTipologiaGeneratori.Where(a => a.IDTipologiaGeneratori == 1).FirstOrDefault();
                if (tipTipologiaGeneratoriAltro != null)
                {
                    lib.TipologiaGeneratoriAltro = tipTipologiaGeneratoriAltro.TipologiaGeneratoriAltro;
                }

                lib.fPannelliSolariTermici = EF_lib.fPannelliSolariTermici;
                lib.SuperficieTotaleSolariTermici = EF_lib.SuperficieTotaleSolariTermici;
                lib.fPannelliSolariTermiciAltro = EF_lib.fPannelliSolariTermiciAltro;
                lib.PannelliSolariTermiciAltro = EF_lib.PannelliSolariTermiciAltro;
                lib.PotenzaSolariTermici = EF_lib.PotenzaSolariTermici;
                lib.fPannelliSolariClimatizzazioneAcs = EF_lib.fPannelliSolariClimatizzazioneAcs;
                lib.fPannelliSolariClimatizzazioneInvernale = EF_lib.fPannelliSolariClimatizzazioneInvernale;
                lib.fPannelliSolariClimatizzazioneEstiva = EF_lib.fPannelliSolariClimatizzazioneEstiva;

                lib.TipologiaResponsabile = EF_lib.IDTipologiaResponsabile;
                lib.ResponsabileImpianto = new POMJ_AnagraficaSoggetti()
                {
                    Nome = EF_lib.NomeResponsabile,
                    Cognome = EF_lib.CognomeResponsabile,
                    CodiceFiscale = EF_lib.CodiceFiscaleResponsabile,
                    Indirizzo = EF_lib.IndirizzoResponsabile,
                    Civico = EF_lib.CivicoResponsabile,
                    Email = EF_lib.EmailResponsabile,
                    EmailPec = EF_lib.EmailPecResponsabile,
                    CodiceCatastaleComune = GetCodeFromID(EF_lib.IDComuneResponsabile),
                    PartitaIVA = EF_lib.PartitaIvaResponsabile,
                    TipoSoggetto = EF_lib.IDTipoSoggetto ?? 1,
                    NomeAzienda = EF_lib.RagioneSocialeResponsabile
                };
                lib.fTerzoResponsabile = EF_lib.fTerzoResponsabile;
                lib.ContenutoAcquaImpianto = EF_lib.ContenutoAcquaImpianto;
                lib.DurezzaTotaleAcquaImpianto = EF_lib.DurezzaTotaleAcquaImpianto;
                lib.fTrattamentoAcquaInvernale = EF_lib.fTrattamentoAcquaInvernale;
                lib.DurezzaTotaleAcquaImpiantoInvernale = EF_lib.DurezzaTotaleAcquaImpiantoInvernale;

                lib.TipologiaTrattamentoAcquaInvernale = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTrattamentoAcquaInvernale)
                {
                    lib.TipologiaTrattamentoAcquaInvernale.Add(item.IDTipologiaTrattamentoAcqua);
                }

                lib.fProtezioneGelo = EF_lib.fProtezioneGelo;
                lib.TipologiaProtezioneGelo = EF_lib.IDTipologiaProtezioneGelo;

                lib.fTrattamentoAcquaAcs = EF_lib.fTrattamentoAcquaAcs;
                lib.TipologiaTrattamentoAcquaAcs = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTrattamentoAcquaAcs)
                {
                    lib.TipologiaTrattamentoAcquaAcs.Add(item.IDTipologiaTrattamentoAcqua);
                }

                lib.DurezzaTotaleAcquaAcs = EF_lib.DurezzaTotaleAcquaAcs;
                lib.PercentualeGlicole = EF_lib.PercentualeGlicole;
                lib.PhGlicole = EF_lib.PhGlicole;

                lib.fTrattamentoAcquaEstiva = EF_lib.fTrattamentoAcquaEstiva;

                lib.TipologiaCircuitoRaffreddamento = EF_lib.IDTipologiaCircuitoRaffreddamento;
                lib.TipologiaAcquaAlimento = EF_lib.IDTipologiaAcquaAlimento;

                lib.TipologiaTrattamentoAcquaEstiva = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTrattamentoAcquaEstiva)
                {
                    lib.TipologiaTrattamentoAcquaEstiva.Add(item.IDTipologiaTrattamentoAcqua);
                }

                lib.TipologiaFiltrazione = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTipologiaFiltrazioni)
                {
                    lib.TipologiaFiltrazione.Add(item.IDTipologiaFiltrazione);
                }

                var tipTipologiaFiltrazioneAltro = EF_lib.LIM_LibrettiImpiantiTipologiaFiltrazioni.Where(a => a.IDTipologiaFiltrazione == 1).FirstOrDefault();
                if (tipTipologiaFiltrazioneAltro != null)
                {
                    lib.TipologiaFiltrazioneAltro = tipTipologiaFiltrazioneAltro.TipologiaFiltrazioneAltro;
                }

                lib.TipologiaAddolcimentoAcqua = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiAddolcimentoAcqua)
                {
                    lib.TipologiaAddolcimentoAcqua.Add(item.IDTipologiaAddolcimentoAcqua);
                }

                var tipTipologiaAddolcimentoAcquaAltro = EF_lib.LIM_LibrettiImpiantiAddolcimentoAcqua.Where(a => a.IDTipologiaAddolcimentoAcqua == 1).FirstOrDefault();
                if (tipTipologiaAddolcimentoAcquaAltro != null)
                {
                    lib.TipologiaAddolcimentoAcquaAltro = tipTipologiaAddolcimentoAcquaAltro.AddolcimentoAcquaAltro;
                }

                lib.TipologiaCondizionamentoChimico = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiCondizionamentoChimico)
                {
                    lib.TipologiaCondizionamentoChimico.Add(item.IDTipologiaCondizionamentoChimico);
                }

                var tipTipologiaCondizionamentoChimicoAltro = EF_lib.LIM_LibrettiImpiantiCondizionamentoChimico.Where(a => a.IDTipologiaCondizionamentoChimico == 1).FirstOrDefault();
                if (tipTipologiaCondizionamentoChimicoAltro != null)
                {
                    lib.TipologiaCondizionamentoChimicoAltro = tipTipologiaCondizionamentoChimicoAltro.TipologiaCondizionamentoChimicoAltro;
                }

                lib.fSistemaSpurgoAutomatico = EF_lib.fSistemaSpurgoAutomatico;
                lib.ConducibilitaAcquaIngresso = EF_lib.ConducibilitaAcquaIngresso;
                lib.ConducibilitaInizioSpurgo = EF_lib.ConducibilitaInizioSpurgo;

                DataLayer.LIM_LibrettiImpiantiResponsabili lib_resp = EF_lib.LIM_LibrettiImpiantiResponsabili.Where(resp => resp.fAttivo).FirstOrDefault();
                if (lib_resp != null)
                {
                    lib.TerzoResponsabile = new POMJ_AnagraficaSoggetti()
                    {
                        Nome = lib_resp.Nome,
                        Cognome = lib_resp.Cognome,
                        CodiceFiscale = lib_resp.CodiceFiscale,
                        NomeAzienda = lib_resp.RagioneSociale,
                        PartitaIVA = lib_resp.PartitaIva,
                        Email = lib_resp.Email,
                        EmailPec = lib_resp.EmailPec,
                        NumeroIscrizioneCCIAA = lib_resp.NumeroCciaa,
                        ProvinciaIscrizioneCCIAA = GetProvinciaByID(lib_resp.IDProvinciaCciaa),
                    };

                    lib.InizioAssunzioneIncarico = lib_resp.DataInizio;
                    lib.FineAssunzioneIncarico = lib_resp.DataFine;
                }

                lib.GruppiTermiciCaldaie = new List<POMJ_GruppiTermici>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiGruppiTermici)
                {
                    POMJ_GruppiTermici temp = new POMJ_GruppiTermici()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        AnalisiFumoPrevisteNr = item.AnalisiFumoPrevisteNr,
                        CombustibileAltro = item.CombustibileAltro,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        FluidoTermovettoreAltro = item.FluidoTermovettoreAltro,
                        Matricola = item.Matricola,
                        Modello = item.Modello,
                        PotenzaTermicaUtileNominaleKw = item.PotenzaTermicaUtileNominaleKw,
                        RendimentoTermicoUtilePc = item.RendimentoTermicoUtilePc,
                        TipologiaCombustibile = item.SYS_TipologiaCombustibile.IDTipologiaCombustibile,
                        TipologiaFluidoTermoVettore = item.SYS_TipologiaFluidoTermoVettore.IDTipologiaFluidoTermoVettore,
                        TipologiaGruppiTermici = item.SYS_TipologiaGruppiTermici.IDTipologiaGruppiTermici,
                        DataDismissione = item.DataDismesso != null ? DateTime.Parse(item.DataDismesso.ToString()).ToString("yyyy-MM-dd'T'HH:mm:ss") : null,
                        DataSostituzione = item.DataDismissione != null ? DateTime.Parse(item.DataDismissione.ToString()).ToString("yyyy-MM-dd'T'HH:mm:ss") : null,
                        Bruciatori = new List<POMJ_Bruciatori>(),
                        Recuperatori = new List<POMJ_Recuperatori>()
                    };

                    foreach (var b in item.LIM_LibrettiImpiantiBruciatori)
                    {
                        temp.Bruciatori.Add(new POMJ_Bruciatori()
                        {
                            CodiceProgressivo = b.CodiceProgressivo,
                            Combustibile = b.Combustibile,
                            DataInstallazione = b.DataInstallazione,
                            Fabbricante = b.Fabbricante,
                            Matricola = b.Matricola,
                            Modello = b.Modello,
                            Tipologia = b.Tipologia,
                            PortataTermicaMaxNominaleKw = b.PortataTermicaMaxNominaleKw,
                            PortataTermicaMinNominaleKw = b.PortataTermicaMinNominaleKw
                        });
                    }

                    foreach (var r in item.LIM_LibrettiImpiantiRecuperatori)
                    {
                        temp.Recuperatori.Add(new POMJ_Recuperatori()
                        {
                            CodiceProgressivo = r.CodiceProgressivo,
                            DataInstallazione = r.DataInstallazione,
                            Fabbricante = r.Fabbricante,
                            Matricola = r.Matricola,
                            Modello = r.Modello,
                            PortataTermicaNominaleTotaleKw = r.PortataTermicaNominaleTotaleKw

                        });
                    }

                    lib.GruppiTermiciCaldaie.Add(temp);
                }

                lib.ScambiatoriCaloreSottostazione = new List<POMJ_ScambiatoreCalore>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiScambiatoriCalore)
                {
                    POMJ_ScambiatoreCalore temp = new POMJ_ScambiatoreCalore()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Matricola = item.Matricola,
                        Modello = item.Modello,
                        PotenzaTermicaNominaleTotaleKw = item.PotenzaTermicaNominaleTotaleKw,
                        DataDismissione = item.DataDismesso != null ? DateTime.Parse(item.DataDismesso.ToString()).ToString("yyyy-MM-dd'T'HH:mm:ss") : null,
                        DataSostituzione = item.DataDismissione != null ? DateTime.Parse(item.DataDismissione.ToString()).ToString("yyyy-MM-dd'T'HH:mm:ss") : null,
                    };
                    lib.ScambiatoriCaloreSottostazione.Add(temp);
                }

                lib.GruppiFrigo = new List<POMJ_GruppiFrigo>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiMacchineFrigorifere)
                {
                    POMJ_GruppiFrigo temp = new POMJ_GruppiFrigo()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Matricola = item.Matricola,
                        Modello = item.Modello,
                        FiltroFrigorigeno = item.FiltroFrigorigeno,
                        TipologiaMacchineFrigorifere = item.SYS_TipologiaMacchineFrigorifere.IDTipologiaMacchineFrigorifere,
                        Combustibile = item.Combustibile,
                        SorgenteLatoEsterno = item.SYS_SorgenteLatoEsterno.IDSorgenteLatoEsterno,
                        FluidoLatoUtenze = item.SYS_FluidoLatoUtenze.IDFluidoLatoUtenze,
                        NumCircuiti = item.NumCircuiti,
                        CoefficienteRiscaldamento = item.CoefficienteRiscaldamento,
                        CoefficienteRaffrescamento = item.CoefficienteRaffrescamento,
                        PotenzaFrigoriferaNominaleKw = item.PotenzaFrigoriferaNominaleKw,
                        PortataTermicaNominaleKw = item.PortataTermicaNominaleKw,
                        PotenzaFrigoriferaAssorbitaNominaleKw = item.PotenzaFrigoriferaAssorbitaNominaleKw,
                        PotenzaTermicaAssorbitaNominaleKw = item.PotenzaTermicaAssorbitaNominaleKw,
                        DataDismissione = item.DataDismesso != null ? DateTime.Parse(item.DataDismesso.ToString()).ToString("yyyy-MM-dd'T'HH:mm:ss") : null,
                        DataSostituzione = item.DataDismissione != null ? DateTime.Parse(item.DataDismissione.ToString()).ToString("yyyy-MM-dd'T'HH:mm:ss") : null
                    };
                    lib.GruppiFrigo.Add(temp);
                }

                lib.GruppiVMC = new List<POMJ_GruppiVMC>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiImpiantiVMC)
                {
                    POMJ_GruppiVMC temp = new POMJ_GruppiVMC()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Modello = item.Modello,
                        TipologiaImpiantoVMC = item.SYS_TipologiaImpiantiVMC.IDTipologiaImpiantiVMC,
                        TipologiaImpiantoAltro = item.TipologiaImpiantoAltro,
                        PortataAriaMaxMch = item.PortataAriaMaxMch,
                        RendimentoRecuperoCop = item.RendimentoRecuperoCop
                    };
                    lib.GruppiVMC.Add(temp);
                }

                lib.SistemiRegolazione = new List<POMJ_SistemiRegolazione>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiSistemiRegolazione)
                {
                    POMJ_SistemiRegolazione temp = new POMJ_SistemiRegolazione()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Modello = item.Modello,
                        PuntiRegolazioneNum = item.PuntiRegolazioneNum,
                        LivelliTemperaturaNum = item.LivelliTemperaturaNum
                    };
                    lib.SistemiRegolazione.Add(temp);
                }


                lib.CogeneratoriTrigeneratori = new List<POMJ_Cogeneratore>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiCogeneratori)
                {
                    POMJ_Cogeneratore temp = new POMJ_Cogeneratore()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        CombustibileAltro = item.CombustibileAltro,
                        DataInstallazione = item.DataInstallazione,
                        EmissioniCOMax = item.EmissioniCOMax,
                        EmissioniCOMin = item.EmissioniCOMin,
                        Fabbricante = item.Fabbricante,
                        Matricola = item.Matricola,
                        Modello = item.Modello,
                        PotenzaElettricaNominaleKw = item.PotenzaElettricaNominaleKw,
                        PotenzaTermicaNominaleKw = item.PotenzaTermicaNominaleKw,
                        TemperaturaAcquaIngressoGradiMax = item.TemperaturaAcquaIngressoGradiMax,
                        TemperaturaAcquaIngressoGradiMin = item.TemperaturaAcquaIngressoGradiMin,
                        TemperaturaAcquaMotoreMax = item.TemperaturaAcquaMotoreMax,
                        TemperaturaAcquaMotoreMin = item.TemperaturaAcquaMotoreMin,
                        TemperaturaAcquaUscitaGradiMax = item.TemperaturaAcquaUscitaGradiMax,
                        TemperaturaAcquaUscitaGradiMin = item.TemperaturaAcquaUscitaGradiMin,
                        TemperaturaFumiMonteMax = item.TemperaturaFumiMonteMax,
                        TemperaturaFumiMonteMin = item.TemperaturaFumiMonteMin,
                        TemperaturaFumiValleMax = item.TemperaturaFumiValleMax,
                        TemperaturaFumiValleMin = item.TemperaturaFumiValleMin,
                        TipologiaCogeneratore = item.SYS_TipologiaCogeneratore.IDTipologiaCogeneratore,
                        TipologiaCombustibile = item.SYS_TipologiaCombustibile.IDTipologiaCombustibile,
                        DataDismissione = item.DataDismesso != null ? DateTime.Parse(item.DataDismesso.ToString()).ToString("yyyy-MM-dd'T'HH:mm:ss") : null,
                        DataSostituzione = item.DataDismissione != null ? DateTime.Parse(item.DataDismissione.ToString()).ToString("yyyy-MM-dd'T'HH:mm:ss") : null,
                    };
                    lib.CogeneratoriTrigeneratori.Add(temp);
                }

                lib.CampiSolariTermici = new List<POMJ_CampoSolareTermico>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiCampiSolariTermici)
                {
                    POMJ_CampoSolareTermico temp = new POMJ_CampoSolareTermico()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        CollettoriNum = item.CollettoriNum,
                        SuperficieTotaleAperturaMq = item.SuperficieTotaleAperturaMq
                    };
                    lib.CampiSolariTermici.Add(temp);
                }

                lib.AltriGeneratori = new List<POMJ_AltroGeneratore>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiAltriGeneratori)
                {
                    POMJ_AltroGeneratore temp = new POMJ_AltroGeneratore()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Matricola = item.Matricola,
                        Modello = item.Modello,
                        Tipologia = item.Tipologia,
                        PotenzaUtileKw = item.PotenzaUtileKw
                    };
                    lib.AltriGeneratori.Add(temp);
                }

                lib.fSistemaRegolazioneOnOff = EF_lib.fSistemaRegolazioneOnOff;
                lib.fSistemaRegolazioneIntegrato = EF_lib.fSistemaRegolazioneIntegrato;
                lib.fSistemaRegolazioneIndipendente = EF_lib.fSistemaRegolazioneIndipendente;
                lib.fSistemaRegolazioneMultigradino = EF_lib.fSistemaRegolazioneMultigradino;
                lib.fSistemaRegolazioneAInverter = EF_lib.fSistemaRegolazioneAInverter;
                lib.fAltroSistemaRegolazionePrimaria = EF_lib.fAltroSistemaRegolazionePrimaria;
                lib.SistemaRegolazionePrimariaAltro = EF_lib.SistemaRegolazionePrimariaAltro;
                lib.fValvoleRegolazione = EF_lib.fValvoleRegolazione;

                lib.ValvoleRegolazione = new List<POMJ_ValvolaRegolazione>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiValvoleRegolazione)
                {
                    POMJ_ValvolaRegolazione temp = new POMJ_ValvolaRegolazione()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Modello = item.Modello,
                        Servomotore = item.Servomotore,
                        VieNum = item.VieNum
                    };
                    lib.ValvoleRegolazione.Add(temp);
                }

                lib.TipologiaTermostatoZona = EF_lib.IDTipologiaTermostatoZona;
                lib.fControlloEntalpico = EF_lib.fControlloEntalpico;
                lib.fControlloPortataAriaVariabile = EF_lib.fControlloPortataAriaVariabile;
                lib.fValvoleTermostatiche = EF_lib.fValvoleTermostatiche;
                lib.fValvoleDueVie = EF_lib.fValvoleDueVie;
                lib.fValvoleTreVie = EF_lib.fValvoleTreVie;
                lib.NoteRegolazioneSingoloAmbiente = EF_lib.NoteRegolazioneSingoloAmbiente;
                lib.fTelelettura = EF_lib.fTelelettura;
                lib.fTelegestione = EF_lib.fTelegestione;

                DataLayer.LIM_LibrettiImpiantiDescrizioniSistemi desc_tel = EF_lib.LIM_LibrettiImpiantiDescrizioniSistemi.Where(d => d.IDTipoSistema == 1).FirstOrDefault();
                if (desc_tel != null)
                {
                    lib.DescrizioneSistemaTelematico = desc_tel.DescrizioneSistema;
                }

                lib.fContabilizzazione = EF_lib.fContabilizzazione;
                lib.fContabilizzazioneRiscaldamento = EF_lib.fContabilizzazioneRiscaldamento;
                lib.fContabilizzazioneRaffrescamento = EF_lib.fContabilizzazioneRaffrescamento;
                lib.fContabilizzazioneAcquaCalda = EF_lib.fContabilizzazioneAcquaCalda;
                lib.TipologiaSistemaContabilizzazione = EF_lib.IDTipologiaSistemaContabilizzazione;

                DataLayer.LIM_LibrettiImpiantiDescrizioniSistemi desc_cont = EF_lib.LIM_LibrettiImpiantiDescrizioniSistemi.Where(d => d.IDTipoSistema == 2).FirstOrDefault();
                if (desc_cont != null)
                {
                    lib.DescrizioneSistemaContabilizzazione = desc_cont.DescrizioneSistema;
                }

                lib.TipologiaSistemaDistribuzione = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTipologiaSistemaDistribuzione)
                {
                    lib.TipologiaSistemaDistribuzione.Add(item.IDTipologiaSistemaDistribuzione);
                }
                var tipTipologiaSistemaDistribuzioneAltro = EF_lib.LIM_LibrettiImpiantiTipologiaSistemaDistribuzione.Where(a => a.IDTipologiaSistemaDistribuzione == 1).FirstOrDefault();
                if (tipTipologiaSistemaDistribuzioneAltro != null)
                {
                    lib.TipologiaGeneratoriAltro = tipTipologiaSistemaDistribuzioneAltro.TipologiaSistemaDistribuzioneAltro;
                }


                lib.fCoibentazioneReteDistribuzione = EF_lib.fCoibentazioneReteDistribuzione;
                lib.NoteCoibentazioneReteDistribuzione = EF_lib.NoteCoibentazioneReteDistribuzione;

                lib.VasiEspansione = new List<POMJ_VasoEspansione>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiVasiEspansione)
                {
                    POMJ_VasoEspansione temp = new POMJ_VasoEspansione()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        CapacitaLt = item.CapacitaLt,
                        fChiuso = item.fChiuso,
                        PressionePrecaricaBar = item.PressionePrecaricaBar
                    };
                    lib.VasiEspansione.Add(temp);
                }

                lib.PompaCircolazione = new List<POMJ_PompaCircolazione>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiPompeCircolazione)
                {
                    POMJ_PompaCircolazione temp = new POMJ_PompaCircolazione()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Modello = item.Modello,
                        fGiriVariabili = item.fGiriVariabili,
                        PotenzaNominaleKw = item.PotenzaNominaleKw
                    };
                    lib.PompaCircolazione.Add(temp);
                }

                lib.TipologiaSistemiEmissione = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTipologiaSistemiEmissione)
                {
                    lib.TipologiaSistemiEmissione.Add(item.IDTipologiaSistemiEmissione);
                }
                lib.SistemaEmissioneAltro = EF_lib.SistemaEmissioneAltro;

                lib.Accumuli = new List<POMJ_Accumulo>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiAccumuli)
                {
                    POMJ_Accumulo temp = new POMJ_Accumulo()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        CapacitaLt = item.CapacitaLt,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        fAcquaCalda = item.fAcquaCalda,
                        fCoibentazionePresente = item.fCoibentazionePresente,
                        fRaffrescamento = item.fRaffrescamento,
                        fRiscaldamento = item.fRiscaldamento,
                        Matricola = item.Matricola,
                        Modello = item.Modello
                    };
                    lib.Accumuli.Add(temp);
                }

                lib.TorriEvaporative = new List<POMJ_TorreEvaporativa>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTorriEvaporative)
                {
                    POMJ_TorreEvaporativa temp = new POMJ_TorreEvaporativa()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        CapacitaNominaleLt = item.CapacitaNominaleLt,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Matricola = item.Matricola,
                        Modello = item.Modello,
                        TipologiaVentilatori = item.SYS_TipologiaVentilatori.IDTipologiaVentilatori,
                        TipologiaVentilatoriAltro = item.TipologiaVentilatoriAltro,
                        VentilatoriNum = item.VentilatoriNum
                    };
                    lib.TorriEvaporative.Add(temp);
                }

                lib.RaffreddatoriLiquido = new List<POMJ_RaffreddatoreLiquido>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiRaffreddatoriLiquido)
                {
                    POMJ_RaffreddatoreLiquido temp = new POMJ_RaffreddatoreLiquido()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Matricola = item.Matricola,
                        Modello = item.Modello,
                        TipologiaVentilatori = item.SYS_TipologiaVentilatori.IDTipologiaVentilatori,
                        TipologiaVentilatoriAltro = item.TipologiaVentilatoriAltro,
                        VentilatoriNum = item.VentilatoriNum
                    };
                    lib.RaffreddatoriLiquido.Add(temp);
                }

                lib.ScambiatoriCaloreIntermedio = new List<POMJ_ScambiatoreCaloreIntermedio>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiScambiatoriCaloreIntermedi)
                {
                    POMJ_ScambiatoreCaloreIntermedio temp = new POMJ_ScambiatoreCaloreIntermedio()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Modello = item.Modello
                    };
                    lib.ScambiatoriCaloreIntermedio.Add(temp);
                }

                lib.CircuitiInterrati = new List<POMJ_CircuitoInterrato>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiCircuitiInterrati)
                {
                    POMJ_CircuitoInterrato temp = new POMJ_CircuitoInterrato()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        LunghezzaCircuitoMt = item.LunghezzaCircuitoMt,
                        ProfonditaInstallazioneMt = item.ProfonditaInstallazioneMt,
                        SuperficieScambiatoreMq = item.SuperficieScambiatoreMq
                    };
                    lib.CircuitiInterrati.Add(temp);
                }

                lib.UnitaTrattamentoAria = new List<POMJ_UnitaTrattamentoAria>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiUnitaTrattamentoAria)
                {
                    POMJ_UnitaTrattamentoAria temp = new POMJ_UnitaTrattamentoAria()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Matricola = item.Matricola,
                        Modello = item.Modello,
                        PortataVentilatoreMandataLts = item.PortataVentilatoreMandataLts,
                        PortataVentilatoreRipresaLts = item.PortataVentilatoreRipresaLts,
                        PotenzaVentilatoreMandataKw = item.PotenzaVentilatoreMandataKw,
                        PotenzaVentilatoreRipresaKw = item.PotenzaVentilatoreRipresaKw
                    };
                    lib.UnitaTrattamentoAria.Add(temp);
                }

                lib.RecuperatoriCalore = new List<POMJ_RecuperatoreCalore>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiRecuperatoriCalore)
                {
                    POMJ_RecuperatoreCalore temp = new POMJ_RecuperatoreCalore()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        ModalitaInstallazioneRecuperatoriCalore = item.SYS_ModalitaInstallazioneRecuperatoriCalore.IDModalitaInstallazioneRecuperatoriCalore,
                        Tipologia = item.Tipologia,
                        PortataVentilatoreMandataLts = item.PortataVentilatoreMandataLts,
                        PortataVentilatoreRipresaLts = item.PortataVentilatoreRipresaLts,
                        PotenzaVentilatoreMandataKw = item.PotenzaVentilatoreMandataKw,
                        PotenzaVentilatoreRipresaKw = item.PotenzaVentilatoreRipresaKw
                    };

                    lib.RecuperatoriCalore.Add(temp);
                }

                lib.ConsumiCombustibile = new List<POMJ_ConsumoCombustibile>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiConsumoCombustibile)
                {
                    POMJ_ConsumoCombustibile temp = new POMJ_ConsumoCombustibile()
                    {
                        Acquisti = item.Acquisti,
                        CombustibileAltro = item.CombustibileAltro,
                        Consumo = item.Consumo,
                        DataEsercizioEnd = item.DataEsercizioEnd,
                        DataEsercizioStart = item.DataEsercizioStart,
                        LetturaFinale = item.LetturaFinale,
                        LetturaIniziale = item.LetturaIniziale,
                        TipologiaCombustibile = item.SYS_TipologiaCombustibile.IDTipologiaCombustibile,
                        UnitaMisura = item.SYS_UnitaMisura.IDUnitaMisura
                    };
                    lib.ConsumiCombustibile.Add(temp);
                }

                lib.ConsumiEnergiaElettrica = new List<POMJ_ConsumoEnergiaElettrica>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiConsumoEnergiaElettrica)
                {
                    POMJ_ConsumoEnergiaElettrica TEMP = new POMJ_ConsumoEnergiaElettrica()
                    {
                        ConsumoTotale = item.ConsumoTotale,
                        DataEsercizioEnd = item.DataEsercizioEnd,
                        DataEsercizioStart = item.DataEsercizioStart,
                        LetturaFinale = item.LetturaFinale,
                        LetturaIniziale = item.LetturaIniziale
                    };

                }

                lib.ConsumiAcqua = new List<POMJ_ConsumoAcqua>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiConsumoAcqua)
                {
                    POMJ_ConsumoAcqua temp = new POMJ_ConsumoAcqua()
                    {
                        ConsumoTotale = item.ConsumoTotale,
                        DataEsercizioEnd = item.DataEsercizioEnd,
                        DataEsercizioStart = item.DataEsercizioStart,
                        LetturaFinale = item.LetturaFinale,
                        LetturaIniziale = item.LetturaIniziale,
                        UnitaMisura = item.SYS_UnitaMisura.IDUnitaMisura
                    };
                    lib.ConsumiAcqua.Add(temp);
                }

                lib.ConsumiProdottiChimici = new List<POMJ_ConsumoProdottiChimici>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiConsumoProdottiChimici)
                {
                    POMJ_ConsumoProdottiChimici temp = new POMJ_ConsumoProdottiChimici()
                    {
                        Consumo = item.Consumo,
                        DataEsercizioEnd = item.DataEsercizioEnd,
                        DataEsercizioStart = item.DataEsercizioStart,
                        fAltriCircuiti = item.fAltriCircuiti,
                        fCircuitoAcs = item.fCircuitoAcs,
                        fCircuitoImpiantoTermico = item.fCircuitoImpiantoTermico,
                        NomeProdotto = item.NomeProdotto,
                        UnitaMisura = item.SYS_UnitaMisura.IDUnitaMisura
                    };
                    lib.ConsumiProdottiChimici.Add(temp);
                }

            }
            return lib;
        }

        public POMJ_LibrettoImpianto GetLibrettoByPodPdr(string pod, string pdr)
        {
            POMJ_LibrettoImpianto lib = new POMJ_LibrettoImpianto();

            var res = from l in db.LIM_LibrettiImpianti
                      .Include("COM_AnagraficaSoggetti")
                      .Include("SYS_StatoLibrettoImpianto")
                      .Include("SYS_TipologiaIntervento")
                      .Include("SYS_CodiciCatastali")
                      //.Include("SYS_CodiciCatastaliSezioni")
                      .Include("SYS_TipologiaProtezioneGelo")
                      .Include("SYS_TipologiaCircuitoRaffreddamento")
                      .Include("SYS_TipologiaAcquaAlimento")

                      .Include("SYS_TipologiaMacchineFrigorifere")
                      .Include("SYS_SorgenteLatoEsterno")
                      .Include("SYS_FluidoLatoUtenze")
                      .Include("SYS_TipologiaImpiantiVMC")

                      .Include("LIM_LibrettiImpiantiImpiantiVMC")
                      .Include("LIM_LibrettiImpiantiDatiCatastali")
                      .Include("LIM_LibrettiImpiantiResponsabili")
                      .Include("LIM_LibrettiImpiantiAddolcimentoAcqua")
                      .Include("LIM_LibrettiImpiantiGruppiTermici")
                      .Include("LIM_LibrettiImpiantiScambiatoriCalore")
                      .Include("LIM_LibrettiImpiantiMacchineFrigorifere")
                      .Include("LIM_LibrettiImpiantiCogeneratori")
                      .Include("LIM_LibrettiImpiantiCampiSolariTermici")
                      .Include("LIM_LibrettiImpiantiAltriGeneratori")
                      .Include("LIM_LibrettiImpiantiSistemiRegolazione")
                      .Include("LIM_LibrettiImpiantiValvoleRegolazione")
                      .Include("LIM_LibrettiImpiantiVasiEspansione")
                      .Include("LIM_LibrettiImpiantiPompeCircolazione")
                      .Include("LIM_LibrettiImpiantiAccumuli")
                      .Include("LIM_LibrettiImpiantiTorriEvaporative")
                      .Include("LIM_LibrettiImpiantiRaffreddatoriLiquido")
                      .Include("LIM_LibrettiImpiantiScambiatoriCaloreIntermedi")
                      .Include("LIM_LibrettiImpiantiCircuitiInterrati")
                      .Include("LIM_LibrettiImpiantiUnitaTrattamentoAria")
                      .Include("LIM_LibrettiImpiantiRecuperatoriCalore")

                      .Include("LIM_LibrettiImpiantiConsumoCombustibile")
                      .Include("LIM_LibrettiImpiantiConsumoEnergiaElettrica")
                      .Include("LIM_LibrettiImpiantiConsumoAcqua")
                      .Include("LIM_LibrettiImpiantiConsumoProdottiChimici")
                      join t in db.LIM_TargatureImpianti on l.IDTargaturaImpianto equals t.IDTargaturaImpianto
                      //join s in db.COM_AnagraficaSoggetti on l.IDSoggetto equals s.IDSoggetto
                      where ((l.NumeroPDR == pdr || l.NumeroPOD == pod) && l.fAttivo == true)
                      select l;

            var EF_lib = res.FirstOrDefault();
            if (EF_lib != null)
            {
                lib.NomeAzienda = EF_lib.COM_AnagraficaSoggetti1.NomeAzienda;
                lib.Nome = EF_lib.COM_AnagraficaSoggetti.Nome;
                lib.Cognome = EF_lib.COM_AnagraficaSoggetti.Cognome;
                lib.CodiceTargaturaImpianto = new POMJ_TargaturaImpianto()
                {
                    CodiceTargatura = EF_lib.LIM_TargatureImpianti.CodiceTargatura
                };
                lib.StatoLibrettoImpianto = EF_lib.IDStatoLibrettoImpianto;

                lib.DataIntervento = EF_lib.DataIntervento;
                lib.TipologiaIntervento = EF_lib.IDTipologiaIntervento.Value;

                lib.Indirizzo = EF_lib.Indirizzo;
                lib.Civico = EF_lib.Civico;
                lib.Palazzo = EF_lib.Palazzo;
                lib.Scala = EF_lib.Scala;
                lib.Interno = EF_lib.Interno;
                lib.CodiceCatastaleComune = EF_lib.SYS_CodiciCatastali.CodiceCatastale;

                lib.DatiCatastali = new List<POMJ_DatiCatastali>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiDatiCatastali)
                {
                    POMJ_DatiCatastali catastali = new POMJ_DatiCatastali()
                    {
                        Foglio = item.Foglio,
                        Identificativo = item.Identificativo,
                        Mappale = item.Mappale,
                        Subalterno = item.Subalterno
                        //CodiceSezione = item.SYS_CodiciCatastaliSezioni.CodiceSezione
                    };
                    lib.DatiCatastali.Add(catastali);
                }

                lib.fUnitaImmobiliare = EF_lib.fUnitaImmobiliare;
                lib.DestinazioneUso = EF_lib.IDDestinazioneUso;
                lib.VolumeLordoRiscaldato = EF_lib.VolumeLordoRiscaldato;
                lib.VolumeLordoRaffrescato = EF_lib.VolumeLordoRaffrescato;
                lib.NumeroAPE = EF_lib.NumeroAPE;
                lib.NumeroPDR = EF_lib.NumeroPDR;
                lib.NumeroPOD = EF_lib.NumeroPOD;

                lib.fAcs = EF_lib.fAcs;
                lib.PotenzaAcs = EF_lib.PotenzaAcs;
                lib.fClimatizzazioneInvernale = EF_lib.fClimatizzazioneInvernale;
                lib.PotenzaClimatizzazioneInvernale = EF_lib.PotenzaClimatizzazioneInvernale;
                lib.fClimatizzazioneEstiva = EF_lib.fClimatizzazioneEstiva;
                lib.PotenzaClimatizzazioneEstiva = EF_lib.PotenzaClimatizzazioneEstiva;
                lib.fClimatizzazioneAltro = EF_lib.fClimatizzazioneAltro;
                lib.ClimatizzazioneAltro = EF_lib.ClimatizzazioneAltro;

                lib.TipologiaFluidoVettore = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTipologiaFluidoVettore)
                {
                    lib.TipologiaFluidoVettore.Add(item.IDTipologiaFluidoVettore);
                }

                var tipFluidoVettoreAltro = EF_lib.LIM_LibrettiImpiantiTipologiaFluidoVettore.Where(a => a.IDTipologiaFluidoVettore == 1).FirstOrDefault();
                if (tipFluidoVettoreAltro != null)
                {
                    lib.TipologiaFluidoVettoreAltro = tipFluidoVettoreAltro.TipologiaFluidoVettoreAltro;
                }

                lib.TipologiaGeneratori = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTipologiaGeneratori)
                {
                    lib.TipologiaGeneratori.Add(item.IDTipologiaGeneratori);
                }

                var tipTipologiaGeneratoriAltro = EF_lib.LIM_LibrettiImpiantiTipologiaGeneratori.Where(a => a.IDTipologiaGeneratori == 1).FirstOrDefault();
                if (tipTipologiaGeneratoriAltro != null)
                {
                    lib.TipologiaGeneratoriAltro = tipTipologiaGeneratoriAltro.TipologiaGeneratoriAltro;
                }

                lib.fPannelliSolariTermici = EF_lib.fPannelliSolariTermici;
                lib.SuperficieTotaleSolariTermici = EF_lib.SuperficieTotaleSolariTermici;
                lib.fPannelliSolariTermiciAltro = EF_lib.fPannelliSolariTermiciAltro;
                lib.PannelliSolariTermiciAltro = EF_lib.PannelliSolariTermiciAltro;
                lib.PotenzaSolariTermici = EF_lib.PotenzaSolariTermici;
                lib.fPannelliSolariClimatizzazioneAcs = EF_lib.fPannelliSolariClimatizzazioneAcs;
                lib.fPannelliSolariClimatizzazioneInvernale = EF_lib.fPannelliSolariClimatizzazioneInvernale;
                lib.fPannelliSolariClimatizzazioneEstiva = EF_lib.fPannelliSolariClimatizzazioneEstiva;

                lib.TipologiaResponsabile = EF_lib.IDTipologiaResponsabile;
                lib.ResponsabileImpianto = new POMJ_AnagraficaSoggetti()
                {
                    Nome = EF_lib.NomeResponsabile,
                    Cognome = EF_lib.CognomeResponsabile,
                    CodiceFiscale = EF_lib.CodiceFiscaleResponsabile,
                    Indirizzo = EF_lib.IndirizzoResponsabile,
                    Civico = EF_lib.CivicoResponsabile,
                    Email = EF_lib.EmailResponsabile,
                    EmailPec = EF_lib.EmailPecResponsabile,
                    CodiceCatastaleComune = GetCodeFromID(EF_lib.IDComuneResponsabile),
                    PartitaIVA = EF_lib.PartitaIvaResponsabile,
                    TipoSoggetto = EF_lib.IDTipoSoggetto ?? 1,
                    NomeAzienda = EF_lib.RagioneSocialeResponsabile
                };
                lib.fTerzoResponsabile = EF_lib.fTerzoResponsabile;
                lib.ContenutoAcquaImpianto = EF_lib.ContenutoAcquaImpianto;
                lib.DurezzaTotaleAcquaImpianto = EF_lib.DurezzaTotaleAcquaImpianto;
                lib.fTrattamentoAcquaInvernale = EF_lib.fTrattamentoAcquaInvernale;
                lib.DurezzaTotaleAcquaImpiantoInvernale = EF_lib.DurezzaTotaleAcquaImpiantoInvernale;

                lib.TipologiaTrattamentoAcquaInvernale = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTrattamentoAcquaInvernale)
                {
                    lib.TipologiaTrattamentoAcquaInvernale.Add(item.IDTipologiaTrattamentoAcqua);
                }

                lib.fProtezioneGelo = EF_lib.fProtezioneGelo;
                lib.TipologiaProtezioneGelo = EF_lib.IDTipologiaProtezioneGelo;

                lib.fTrattamentoAcquaAcs = EF_lib.fTrattamentoAcquaAcs;
                lib.TipologiaTrattamentoAcquaAcs = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTrattamentoAcquaAcs)
                {
                    lib.TipologiaTrattamentoAcquaAcs.Add(item.IDTipologiaTrattamentoAcqua);
                }

                lib.DurezzaTotaleAcquaAcs = EF_lib.DurezzaTotaleAcquaAcs;
                lib.PercentualeGlicole = EF_lib.PercentualeGlicole;
                lib.PhGlicole = EF_lib.PhGlicole;

                lib.fTrattamentoAcquaEstiva = EF_lib.fTrattamentoAcquaEstiva;

                lib.TipologiaCircuitoRaffreddamento = EF_lib.IDTipologiaCircuitoRaffreddamento;
                lib.TipologiaAcquaAlimento = EF_lib.IDTipologiaAcquaAlimento;

                lib.TipologiaTrattamentoAcquaEstiva = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTrattamentoAcquaEstiva)
                {
                    lib.TipologiaTrattamentoAcquaEstiva.Add(item.IDTipologiaTrattamentoAcqua);
                }

                lib.TipologiaFiltrazione = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTipologiaFiltrazioni)
                {
                    lib.TipologiaFiltrazione.Add(item.IDTipologiaFiltrazione);
                }

                var tipTipologiaFiltrazioneAltro = EF_lib.LIM_LibrettiImpiantiTipologiaFiltrazioni.Where(a => a.IDTipologiaFiltrazione == 1).FirstOrDefault();
                if (tipTipologiaFiltrazioneAltro != null)
                {
                    lib.TipologiaFiltrazioneAltro = tipTipologiaFiltrazioneAltro.TipologiaFiltrazioneAltro;
                }

                lib.TipologiaAddolcimentoAcqua = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiAddolcimentoAcqua)
                {
                    lib.TipologiaAddolcimentoAcqua.Add(item.IDTipologiaAddolcimentoAcqua);
                }

                var tipTipologiaAddolcimentoAcquaAltro = EF_lib.LIM_LibrettiImpiantiAddolcimentoAcqua.Where(a => a.IDTipologiaAddolcimentoAcqua == 1).FirstOrDefault();
                if (tipTipologiaAddolcimentoAcquaAltro != null)
                {
                    lib.TipologiaAddolcimentoAcquaAltro = tipTipologiaAddolcimentoAcquaAltro.AddolcimentoAcquaAltro;
                }

                lib.TipologiaCondizionamentoChimico = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiCondizionamentoChimico)
                {
                    lib.TipologiaCondizionamentoChimico.Add(item.IDTipologiaCondizionamentoChimico);
                }

                var tipTipologiaCondizionamentoChimicoAltro = EF_lib.LIM_LibrettiImpiantiCondizionamentoChimico.Where(a => a.IDTipologiaCondizionamentoChimico == 1).FirstOrDefault();
                if (tipTipologiaCondizionamentoChimicoAltro != null)
                {
                    lib.TipologiaCondizionamentoChimicoAltro = tipTipologiaCondizionamentoChimicoAltro.TipologiaCondizionamentoChimicoAltro;
                }

                lib.fSistemaSpurgoAutomatico = EF_lib.fSistemaSpurgoAutomatico;
                lib.ConducibilitaAcquaIngresso = EF_lib.ConducibilitaAcquaIngresso;
                lib.ConducibilitaInizioSpurgo = EF_lib.ConducibilitaInizioSpurgo;

                DataLayer.LIM_LibrettiImpiantiResponsabili lib_resp = EF_lib.LIM_LibrettiImpiantiResponsabili.Where(resp => resp.fAttivo).FirstOrDefault();
                if (lib_resp != null)
                {
                    lib.TerzoResponsabile = new POMJ_AnagraficaSoggetti()
                    {
                        Nome = lib_resp.Nome,
                        Cognome = lib_resp.Cognome,
                        CodiceFiscale = lib_resp.CodiceFiscale,
                        NomeAzienda = lib_resp.RagioneSociale,
                        PartitaIVA = lib_resp.PartitaIva,
                        Email = lib_resp.Email,
                        EmailPec = lib_resp.EmailPec,
                        NumeroIscrizioneCCIAA = lib_resp.NumeroCciaa,
                        ProvinciaIscrizioneCCIAA = GetProvinciaByID(lib_resp.IDProvinciaCciaa),
                    };

                    lib.InizioAssunzioneIncarico = lib_resp.DataInizio;
                    lib.FineAssunzioneIncarico = lib_resp.DataFine;
                }

                lib.GruppiTermiciCaldaie = new List<POMJ_GruppiTermici>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiGruppiTermici)
                {
                    POMJ_GruppiTermici temp = new POMJ_GruppiTermici()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        AnalisiFumoPrevisteNr = item.AnalisiFumoPrevisteNr,
                        CombustibileAltro = item.CombustibileAltro,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        FluidoTermovettoreAltro = item.FluidoTermovettoreAltro,
                        Matricola = item.Matricola,
                        Modello = item.Modello,
                        PotenzaTermicaUtileNominaleKw = item.PotenzaTermicaUtileNominaleKw,
                        RendimentoTermicoUtilePc = item.RendimentoTermicoUtilePc,
                        TipologiaCombustibile = item.SYS_TipologiaCombustibile.IDTipologiaCombustibile,
                        TipologiaFluidoTermoVettore = item.SYS_TipologiaFluidoTermoVettore.IDTipologiaFluidoTermoVettore,
                        TipologiaGruppiTermici = item.SYS_TipologiaGruppiTermici.IDTipologiaGruppiTermici,
                        Bruciatori = new List<POMJ_Bruciatori>(),
                        Recuperatori = new List<POMJ_Recuperatori>()
                    };

                    foreach (var b in item.LIM_LibrettiImpiantiBruciatori)
                    {
                        temp.Bruciatori.Add(new POMJ_Bruciatori()
                        {
                            CodiceProgressivo = b.CodiceProgressivo,
                            Combustibile = b.Combustibile,
                            DataInstallazione = b.DataInstallazione,
                            Fabbricante = b.Fabbricante,
                            Matricola = b.Matricola,
                            Modello = b.Modello,
                            Tipologia = b.Tipologia,
                            PortataTermicaMaxNominaleKw = b.PortataTermicaMaxNominaleKw,
                            PortataTermicaMinNominaleKw = b.PortataTermicaMinNominaleKw
                        });
                    }

                    foreach (var r in item.LIM_LibrettiImpiantiRecuperatori)
                    {
                        temp.Recuperatori.Add(new POMJ_Recuperatori()
                        {
                            CodiceProgressivo = r.CodiceProgressivo,
                            DataInstallazione = r.DataInstallazione,
                            Fabbricante = r.Fabbricante,
                            Matricola = r.Matricola,
                            Modello = r.Modello,
                            PortataTermicaNominaleTotaleKw = r.PortataTermicaNominaleTotaleKw

                        });
                    }

                    lib.GruppiTermiciCaldaie.Add(temp);
                }

                lib.ScambiatoriCaloreSottostazione = new List<POMJ_ScambiatoreCalore>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiScambiatoriCalore)
                {
                    POMJ_ScambiatoreCalore temp = new POMJ_ScambiatoreCalore()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Matricola = item.Matricola,
                        Modello = item.Modello,
                        PotenzaTermicaNominaleTotaleKw = item.PotenzaTermicaNominaleTotaleKw
                    };
                    lib.ScambiatoriCaloreSottostazione.Add(temp);
                }

                lib.GruppiFrigo = new List<POMJ_GruppiFrigo>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiMacchineFrigorifere)
                {
                    POMJ_GruppiFrigo temp = new POMJ_GruppiFrigo()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Matricola = item.Matricola,
                        Modello = item.Modello,
                        FiltroFrigorigeno = item.FiltroFrigorigeno,
                        TipologiaMacchineFrigorifere = item.SYS_TipologiaMacchineFrigorifere.IDTipologiaMacchineFrigorifere,
                        Combustibile = item.Combustibile,
                        SorgenteLatoEsterno = item.SYS_SorgenteLatoEsterno.IDSorgenteLatoEsterno,
                        FluidoLatoUtenze = item.SYS_FluidoLatoUtenze.IDFluidoLatoUtenze,
                        NumCircuiti = item.NumCircuiti,
                        CoefficienteRiscaldamento = item.CoefficienteRiscaldamento,
                        CoefficienteRaffrescamento = item.CoefficienteRaffrescamento,
                        PotenzaFrigoriferaNominaleKw = item.PotenzaFrigoriferaNominaleKw,
                        PortataTermicaNominaleKw = item.PortataTermicaNominaleKw,
                        PotenzaFrigoriferaAssorbitaNominaleKw = item.PotenzaFrigoriferaAssorbitaNominaleKw,
                        PotenzaTermicaAssorbitaNominaleKw = item.PotenzaTermicaAssorbitaNominaleKw
                    };
                    lib.GruppiFrigo.Add(temp);
                }

                lib.GruppiVMC = new List<POMJ_GruppiVMC>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiImpiantiVMC)
                {
                    POMJ_GruppiVMC temp = new POMJ_GruppiVMC()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Modello = item.Modello,
                        TipologiaImpiantoVMC = item.SYS_TipologiaImpiantiVMC.IDTipologiaImpiantiVMC,
                        TipologiaImpiantoAltro = item.TipologiaImpiantoAltro,
                        PortataAriaMaxMch = item.PortataAriaMaxMch,
                        RendimentoRecuperoCop = item.RendimentoRecuperoCop
                    };
                    lib.GruppiVMC.Add(temp);
                }

                lib.SistemiRegolazione = new List<POMJ_SistemiRegolazione>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiSistemiRegolazione)
                {
                    POMJ_SistemiRegolazione temp = new POMJ_SistemiRegolazione()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Modello = item.Modello,
                        PuntiRegolazioneNum = item.PuntiRegolazioneNum,
                        LivelliTemperaturaNum = item.LivelliTemperaturaNum
                    };
                    lib.SistemiRegolazione.Add(temp);
                }


                lib.CogeneratoriTrigeneratori = new List<POMJ_Cogeneratore>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiCogeneratori)
                {
                    POMJ_Cogeneratore temp = new POMJ_Cogeneratore()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        CombustibileAltro = item.CombustibileAltro,
                        DataInstallazione = item.DataInstallazione,
                        EmissioniCOMax = item.EmissioniCOMax,
                        EmissioniCOMin = item.EmissioniCOMin,
                        Fabbricante = item.Fabbricante,
                        Matricola = item.Matricola,
                        Modello = item.Modello,
                        PotenzaElettricaNominaleKw = item.PotenzaElettricaNominaleKw,
                        PotenzaTermicaNominaleKw = item.PotenzaTermicaNominaleKw,
                        TemperaturaAcquaIngressoGradiMax = item.TemperaturaAcquaIngressoGradiMax,
                        TemperaturaAcquaIngressoGradiMin = item.TemperaturaAcquaIngressoGradiMin,
                        TemperaturaAcquaMotoreMax = item.TemperaturaAcquaMotoreMax,
                        TemperaturaAcquaMotoreMin = item.TemperaturaAcquaMotoreMin,
                        TemperaturaAcquaUscitaGradiMax = item.TemperaturaAcquaUscitaGradiMax,
                        TemperaturaAcquaUscitaGradiMin = item.TemperaturaAcquaUscitaGradiMin,
                        TemperaturaFumiMonteMax = item.TemperaturaFumiMonteMax,
                        TemperaturaFumiMonteMin = item.TemperaturaFumiMonteMin,
                        TemperaturaFumiValleMax = item.TemperaturaFumiValleMax,
                        TemperaturaFumiValleMin = item.TemperaturaFumiValleMin,
                        TipologiaCogeneratore = item.SYS_TipologiaCogeneratore.IDTipologiaCogeneratore,
                        TipologiaCombustibile = item.SYS_TipologiaCombustibile.IDTipologiaCombustibile
                    };
                    lib.CogeneratoriTrigeneratori.Add(temp);
                }

                lib.CampiSolariTermici = new List<POMJ_CampoSolareTermico>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiCampiSolariTermici)
                {
                    POMJ_CampoSolareTermico temp = new POMJ_CampoSolareTermico()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        CollettoriNum = item.CollettoriNum,
                        SuperficieTotaleAperturaMq = item.SuperficieTotaleAperturaMq
                    };
                    lib.CampiSolariTermici.Add(temp);
                }

                lib.AltriGeneratori = new List<POMJ_AltroGeneratore>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiAltriGeneratori)
                {
                    POMJ_AltroGeneratore temp = new POMJ_AltroGeneratore()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Matricola = item.Matricola,
                        Modello = item.Modello,
                        Tipologia = item.Tipologia,
                        PotenzaUtileKw = item.PotenzaUtileKw
                    };
                    lib.AltriGeneratori.Add(temp);
                }

                lib.fSistemaRegolazioneOnOff = EF_lib.fSistemaRegolazioneOnOff;
                lib.fSistemaRegolazioneIntegrato = EF_lib.fSistemaRegolazioneIntegrato;
                lib.fSistemaRegolazioneIndipendente = EF_lib.fSistemaRegolazioneIndipendente;
                lib.fSistemaRegolazioneMultigradino = EF_lib.fSistemaRegolazioneMultigradino;
                lib.fSistemaRegolazioneAInverter = EF_lib.fSistemaRegolazioneAInverter;
                lib.fAltroSistemaRegolazionePrimaria = EF_lib.fAltroSistemaRegolazionePrimaria;
                lib.SistemaRegolazionePrimariaAltro = EF_lib.SistemaRegolazionePrimariaAltro;
                lib.fValvoleRegolazione = EF_lib.fValvoleRegolazione;

                lib.ValvoleRegolazione = new List<POMJ_ValvolaRegolazione>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiValvoleRegolazione)
                {
                    POMJ_ValvolaRegolazione temp = new POMJ_ValvolaRegolazione()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Modello = item.Modello,
                        Servomotore = item.Servomotore,
                        VieNum = item.VieNum
                    };
                    lib.ValvoleRegolazione.Add(temp);
                }

                lib.TipologiaTermostatoZona = EF_lib.IDTipologiaTermostatoZona;
                lib.fControlloEntalpico = EF_lib.fControlloEntalpico;
                lib.fControlloPortataAriaVariabile = EF_lib.fControlloPortataAriaVariabile;
                lib.fValvoleTermostatiche = EF_lib.fValvoleTermostatiche;
                lib.fValvoleDueVie = EF_lib.fValvoleDueVie;
                lib.fValvoleTreVie = EF_lib.fValvoleTreVie;
                lib.NoteRegolazioneSingoloAmbiente = EF_lib.NoteRegolazioneSingoloAmbiente;
                lib.fTelelettura = EF_lib.fTelelettura;
                lib.fTelegestione = EF_lib.fTelegestione;

                DataLayer.LIM_LibrettiImpiantiDescrizioniSistemi desc_tel = EF_lib.LIM_LibrettiImpiantiDescrizioniSistemi.Where(d => d.IDTipoSistema == 1).FirstOrDefault();
                if (desc_tel != null)
                {
                    lib.DescrizioneSistemaTelematico = desc_tel.DescrizioneSistema;
                }

                lib.fContabilizzazione = EF_lib.fContabilizzazione;
                lib.fContabilizzazioneRiscaldamento = EF_lib.fContabilizzazioneRiscaldamento;
                lib.fContabilizzazioneRaffrescamento = EF_lib.fContabilizzazioneRaffrescamento;
                lib.fContabilizzazioneAcquaCalda = EF_lib.fContabilizzazioneAcquaCalda;
                lib.TipologiaSistemaContabilizzazione = EF_lib.IDTipologiaSistemaContabilizzazione;

                DataLayer.LIM_LibrettiImpiantiDescrizioniSistemi desc_cont = EF_lib.LIM_LibrettiImpiantiDescrizioniSistemi.Where(d => d.IDTipoSistema == 2).FirstOrDefault();
                if (desc_cont != null)
                {
                    lib.DescrizioneSistemaContabilizzazione = desc_cont.DescrizioneSistema;
                }

                lib.TipologiaSistemaDistribuzione = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTipologiaSistemaDistribuzione)
                {
                    lib.TipologiaSistemaDistribuzione.Add(item.IDTipologiaSistemaDistribuzione);
                }
                var tipTipologiaSistemaDistribuzioneAltro = EF_lib.LIM_LibrettiImpiantiTipologiaSistemaDistribuzione.Where(a => a.IDTipologiaSistemaDistribuzione == 1).FirstOrDefault();
                if (tipTipologiaSistemaDistribuzioneAltro != null)
                {
                    lib.TipologiaGeneratoriAltro = tipTipologiaSistemaDistribuzioneAltro.TipologiaSistemaDistribuzioneAltro;
                }


                lib.fCoibentazioneReteDistribuzione = EF_lib.fCoibentazioneReteDistribuzione;
                lib.NoteCoibentazioneReteDistribuzione = EF_lib.NoteCoibentazioneReteDistribuzione;

                lib.VasiEspansione = new List<POMJ_VasoEspansione>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiVasiEspansione)
                {
                    POMJ_VasoEspansione temp = new POMJ_VasoEspansione()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        CapacitaLt = item.CapacitaLt,
                        fChiuso = item.fChiuso,
                        PressionePrecaricaBar = item.PressionePrecaricaBar
                    };
                    lib.VasiEspansione.Add(temp);
                }

                lib.PompaCircolazione = new List<POMJ_PompaCircolazione>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiPompeCircolazione)
                {
                    POMJ_PompaCircolazione temp = new POMJ_PompaCircolazione()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Modello = item.Modello,
                        fGiriVariabili = item.fGiriVariabili,
                        PotenzaNominaleKw = item.PotenzaNominaleKw
                    };
                    lib.PompaCircolazione.Add(temp);
                }

                lib.TipologiaSistemiEmissione = new List<int>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTipologiaSistemiEmissione)
                {
                    lib.TipologiaSistemiEmissione.Add(item.IDTipologiaSistemiEmissione);
                }
                lib.SistemaEmissioneAltro = EF_lib.SistemaEmissioneAltro;

                lib.Accumuli = new List<POMJ_Accumulo>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiAccumuli)
                {
                    POMJ_Accumulo temp = new POMJ_Accumulo()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        CapacitaLt = item.CapacitaLt,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        fAcquaCalda = item.fAcquaCalda,
                        fCoibentazionePresente = item.fCoibentazionePresente,
                        fRaffrescamento = item.fRaffrescamento,
                        fRiscaldamento = item.fRiscaldamento,
                        Matricola = item.Matricola,
                        Modello = item.Modello
                    };
                    lib.Accumuli.Add(temp);
                }

                lib.TorriEvaporative = new List<POMJ_TorreEvaporativa>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiTorriEvaporative)
                {
                    POMJ_TorreEvaporativa temp = new POMJ_TorreEvaporativa()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        CapacitaNominaleLt = item.CapacitaNominaleLt,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Matricola = item.Matricola,
                        Modello = item.Modello,
                        TipologiaVentilatori = item.SYS_TipologiaVentilatori.IDTipologiaVentilatori,
                        TipologiaVentilatoriAltro = item.TipologiaVentilatoriAltro,
                        VentilatoriNum = item.VentilatoriNum
                    };
                    lib.TorriEvaporative.Add(temp);
                }

                lib.RaffreddatoriLiquido = new List<POMJ_RaffreddatoreLiquido>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiRaffreddatoriLiquido)
                {
                    POMJ_RaffreddatoreLiquido temp = new POMJ_RaffreddatoreLiquido()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Matricola = item.Matricola,
                        Modello = item.Modello,
                        TipologiaVentilatori = item.SYS_TipologiaVentilatori.IDTipologiaVentilatori,
                        TipologiaVentilatoriAltro = item.TipologiaVentilatoriAltro,
                        VentilatoriNum = item.VentilatoriNum
                    };
                    lib.RaffreddatoriLiquido.Add(temp);
                }

                lib.ScambiatoriCaloreIntermedio = new List<POMJ_ScambiatoreCaloreIntermedio>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiScambiatoriCaloreIntermedi)
                {
                    POMJ_ScambiatoreCaloreIntermedio temp = new POMJ_ScambiatoreCaloreIntermedio()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Modello = item.Modello
                    };
                    lib.ScambiatoriCaloreIntermedio.Add(temp);
                }

                lib.CircuitiInterrati = new List<POMJ_CircuitoInterrato>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiCircuitiInterrati)
                {
                    POMJ_CircuitoInterrato temp = new POMJ_CircuitoInterrato()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        LunghezzaCircuitoMt = item.LunghezzaCircuitoMt,
                        ProfonditaInstallazioneMt = item.ProfonditaInstallazioneMt,
                        SuperficieScambiatoreMq = item.SuperficieScambiatoreMq
                    };
                    lib.CircuitiInterrati.Add(temp);
                }

                lib.UnitaTrattamentoAria = new List<POMJ_UnitaTrattamentoAria>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiUnitaTrattamentoAria)
                {
                    POMJ_UnitaTrattamentoAria temp = new POMJ_UnitaTrattamentoAria()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        Fabbricante = item.Fabbricante,
                        Matricola = item.Matricola,
                        Modello = item.Modello,
                        PortataVentilatoreMandataLts = item.PortataVentilatoreMandataLts,
                        PortataVentilatoreRipresaLts = item.PortataVentilatoreRipresaLts,
                        PotenzaVentilatoreMandataKw = item.PotenzaVentilatoreMandataKw,
                        PotenzaVentilatoreRipresaKw = item.PotenzaVentilatoreRipresaKw
                    };
                    lib.UnitaTrattamentoAria.Add(temp);
                }

                lib.RecuperatoriCalore = new List<POMJ_RecuperatoreCalore>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiRecuperatoriCalore)
                {
                    POMJ_RecuperatoreCalore temp = new POMJ_RecuperatoreCalore()
                    {
                        CodiceProgressivo = item.CodiceProgressivo,
                        DataInstallazione = item.DataInstallazione,
                        ModalitaInstallazioneRecuperatoriCalore = item.SYS_ModalitaInstallazioneRecuperatoriCalore.IDModalitaInstallazioneRecuperatoriCalore,
                        Tipologia = item.Tipologia,
                        PortataVentilatoreMandataLts = item.PortataVentilatoreMandataLts,
                        PortataVentilatoreRipresaLts = item.PortataVentilatoreRipresaLts,
                        PotenzaVentilatoreMandataKw = item.PotenzaVentilatoreMandataKw,
                        PotenzaVentilatoreRipresaKw = item.PotenzaVentilatoreRipresaKw
                    };

                    lib.RecuperatoriCalore.Add(temp);
                }

                lib.ConsumiCombustibile = new List<POMJ_ConsumoCombustibile>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiConsumoCombustibile)
                {
                    POMJ_ConsumoCombustibile temp = new POMJ_ConsumoCombustibile()
                    {
                        Acquisti = item.Acquisti,
                        CombustibileAltro = item.CombustibileAltro,
                        Consumo = item.Consumo,
                        DataEsercizioEnd = item.DataEsercizioEnd,
                        DataEsercizioStart = item.DataEsercizioStart,
                        LetturaFinale = item.LetturaFinale,
                        LetturaIniziale = item.LetturaIniziale,
                        TipologiaCombustibile = item.SYS_TipologiaCombustibile.IDTipologiaCombustibile,
                        UnitaMisura = item.SYS_UnitaMisura.IDUnitaMisura
                    };
                    lib.ConsumiCombustibile.Add(temp);
                }

                lib.ConsumiEnergiaElettrica = new List<POMJ_ConsumoEnergiaElettrica>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiConsumoEnergiaElettrica)
                {
                    POMJ_ConsumoEnergiaElettrica TEMP = new POMJ_ConsumoEnergiaElettrica()
                    {
                        ConsumoTotale = item.ConsumoTotale,
                        DataEsercizioEnd = item.DataEsercizioEnd,
                        DataEsercizioStart = item.DataEsercizioStart,
                        LetturaFinale = item.LetturaFinale,
                        LetturaIniziale = item.LetturaIniziale
                    };

                }

                lib.ConsumiAcqua = new List<POMJ_ConsumoAcqua>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiConsumoAcqua)
                {
                    POMJ_ConsumoAcqua temp = new POMJ_ConsumoAcqua()
                    {
                        ConsumoTotale = item.ConsumoTotale,
                        DataEsercizioEnd = item.DataEsercizioEnd,
                        DataEsercizioStart = item.DataEsercizioStart,
                        LetturaFinale = item.LetturaFinale,
                        LetturaIniziale = item.LetturaIniziale,
                        UnitaMisura = item.SYS_UnitaMisura.IDUnitaMisura
                    };
                    lib.ConsumiAcqua.Add(temp);
                }

                lib.ConsumiProdottiChimici = new List<POMJ_ConsumoProdottiChimici>();
                foreach (var item in EF_lib.LIM_LibrettiImpiantiConsumoProdottiChimici)
                {
                    POMJ_ConsumoProdottiChimici temp = new POMJ_ConsumoProdottiChimici()
                    {
                        Consumo = item.Consumo,
                        DataEsercizioEnd = item.DataEsercizioEnd,
                        DataEsercizioStart = item.DataEsercizioStart,
                        fAltriCircuiti = item.fAltriCircuiti,
                        fCircuitoAcs = item.fCircuitoAcs,
                        fCircuitoImpiantoTermico = item.fCircuitoImpiantoTermico,
                        NomeProdotto = item.NomeProdotto,
                        UnitaMisura = item.SYS_UnitaMisura.IDUnitaMisura
                    };
                    lib.ConsumiProdottiChimici.Add(temp);
                }

            }
            return lib;
        }

        public List<POMJ_TargaturaImpianto> GeCodiciTargaturaByCodiceSoggetto(string codiceSoggetto)
        {
            List<POMJ_TargaturaImpianto> targaturelist = new List<POMJ_TargaturaImpianto>();

            if (!string.IsNullOrEmpty(codiceSoggetto))
            {
                char[] separator = new char[1] { '-' };
                string[] codiciSoggetti = codiceSoggetto.Split(separator);

                if (codiciSoggetti.Count() == 2) //Manutentore
                {
                    string codiceSoggettoManutentore = codiceSoggetto;

                    var targatureManutentore = (from t in db.LIM_TargatureImpianti
                                                join c in db.COM_AnagraficaSoggetti on t.IDSoggettoDerived equals c.IDSoggetto
                                                where !(db.LIM_LibrettiImpianti.Any(x => x.IDTargaturaImpianto == t.IDTargaturaImpianto))
                                                 && (c.CodiceSoggetto == codiceSoggettoManutentore)
                                                select new
                                                {
                                                    codiceTargatura = t.CodiceTargatura
                                                }
                                               ).ToList();

                    foreach (var item in targatureManutentore)
                    {
                        targaturelist.Add(new POMJ_TargaturaImpianto { CodiceTargatura = item.codiceTargatura });
                    }
                }
                else //Impresa
                {
                    string codiceSoggettoImpresa = codiciSoggetti[0];

                    var targatureImpresa = (from t in db.LIM_TargatureImpianti
                                            join c in db.COM_AnagraficaSoggetti on t.IDSoggetto equals c.IDSoggetto
                                            where !(db.LIM_LibrettiImpianti.Any(x => x.IDTargaturaImpianto == t.IDTargaturaImpianto))
                                            && (c.CodiceSoggetto == codiceSoggettoImpresa)
                                            select new
                                            {
                                                codiceTargatura = t.CodiceTargatura
                                            }
                                            ).ToList();

                    foreach (var item in targatureImpresa)
                    {
                        targaturelist.Add(new POMJ_TargaturaImpianto { CodiceTargatura = item.codiceTargatura });
                    }
                }
            }

            return targaturelist;
        }

        private string GetProvinciaByID(int? IDProvinciaCciaa)
        {
            string codiceProvincia = "";
            if (IDProvinciaCciaa.HasValue)
            {
                codiceProvincia = db.SYS_Province
                    .Where(p => p.IDProvincia == IDProvinciaCciaa.Value)
                    .Select(p => p.Provincia)
                    .FirstOrDefault();
            }
            return codiceProvincia;
        }

        private string GetCodeFromID(int? iDComuneResponsabile)
        {
            string codiceComune = "";
            if (iDComuneResponsabile.HasValue)
            {
                codiceComune = db.SYS_CodiciCatastali
                    .Where(c => c.IDCodiceCatastale == iDComuneResponsabile.Value)
                    .Select(c => c.CodiceCatastale)
                    .FirstOrDefault();
            }
            return codiceComune;
        }

        private string GetTecnicoIntervento(int? iDSoggetto)
        {
            string TecnicoIntervento = string.Empty;
            if (iDSoggetto != null)
            {
                var tecnico = db.COM_AnagraficaSoggetti.FirstOrDefault(c => c.IDSoggetto == iDSoggetto);
                if (tecnico != null)
                {
                    TecnicoIntervento = tecnico.Nome + " " + tecnico.Cognome;
                }
            }

            return TecnicoIntervento;
        }

        public POMJ_RapportoControlloTecnico_GT GetRapportoTecnico_GT_ByID(int idRapporto)
        {
            POMJ_RapportoControlloTecnico_GT rapporto = new POMJ_RapportoControlloTecnico_GT();

            var ef_rapporto = (from r in db.RCT_RapportoDiControlloTecnicoBase
                               .Include("SYS_StatoRapportoDiControllo")
                               .Include("SYS_TipologiaCombustibile")
                               .Include("SYS_TipologiaFluidoTermoVettore")
                               .Include("SYS_TipologiaSistemaDistribuzione")
                               .Include("SYS_TipologiaContabilizzazione")
                               .Include("SYS_TipologiaCombustibile")

                              .Include("RCT_RapportoDiControlloTecnicoBaseCheckList")
                              .Include("RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs")
                              .Include("RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale")
                              .Include("RCT_RaccomandazioniPrescrizioni")
                              //.Include("RCT_BollinoCalorePulito")
                              .Include("RCT_RapportoDiControlloTecnicoGT")
                               join t in db.LIM_TargatureImpianti on r.IDTargaturaImpianto equals t.IDTargaturaImpianto
                               where r.IDRapportoControlloTecnico == idRapporto
                               select r).FirstOrDefault();

            if (ef_rapporto != null)
            {
                // RCT_RapportoDiControlloTecnicoBase fields
                rapporto.CodiceTargaturaImpianto = new POMJ_TargaturaImpianto()
                {
                    CodiceTargatura = ef_rapporto.LIM_TargatureImpianti.CodiceTargatura
                };

                rapporto.PotenzaTermicaNominaleTotaleMax = ef_rapporto.PotenzaTermicaNominaleTotaleMax;
                rapporto.Indirizzo = ef_rapporto.Indirizzo;
                rapporto.Civico = ef_rapporto.Civico;
                rapporto.Palazzo = ef_rapporto.Palazzo;
                rapporto.Scala = ef_rapporto.Scala;
                rapporto.Interno = ef_rapporto.Interno;
                rapporto.StatoRapportoDiControllo = ef_rapporto.SYS_StatoRapportoDiControllo.IDStatoRapportoDiControllo;
                rapporto.TipologiaControllo = ef_rapporto.SYS_TipologiaControllo.IDTipologiaControllo;
                rapporto.TipologiaResponsabile = ef_rapporto.SYS_TipologiaResponsabile.IDTipologiaResponsabile;

                rapporto.ResponsabileImpianto = new POMJ_AnagraficaSoggetti()
                {
                    TipoSoggetto = ef_rapporto.IDTipoSoggetto.Value,
                    Nome = ef_rapporto.NomeResponsabile,
                    Cognome = ef_rapporto.CognomeResponsabile,
                    CodiceFiscale = ef_rapporto.CodiceFiscaleResponsabile,
                    NomeAzienda = ef_rapporto.RagioneSocialeResponsabile,
                    PartitaIVA = ef_rapporto.PartitaIVAResponsabile,
                    Indirizzo = ef_rapporto.IndirizzoResponsabile,
                    Civico = ef_rapporto.Civico,
                    CodiceCatastaleComune = ef_rapporto.ComuneResponsabile,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaResponsabile.HasValue ? ef_rapporto.IDProvinciaResponsabile.Value : 0
                };

                rapporto.TerzoResponsabile = new POMJ_AnagraficaSoggetti()
                {
                    TipoSoggetto = ef_rapporto.IDTipoSoggetto.Value,
                    NomeAzienda = ef_rapporto.RagioneSocialeTerzoResponsabile,
                    PartitaIVA = ef_rapporto.PartitaIVATerzoResponsabile,
                    Indirizzo = ef_rapporto.IndirizzoTerzoResponsabile,
                    Civico = ef_rapporto.Civico,
                    CodiceCatastaleComune = ef_rapporto.ComuneTerzoResponsabile,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaTerzoResponsabile.HasValue ? ef_rapporto.IDProvinciaTerzoResponsabile.Value : 0,

                };

                rapporto.ImpresaManutentrice = new POMJ_AnagraficaSoggetti()
                {
                    TipoSoggetto = ef_rapporto.IDTipoSoggetto.Value,
                    NomeAzienda = ef_rapporto.RagioneSocialeImpresaManutentrice,
                    PartitaIVA = ef_rapporto.PartitaIVAImpresaManutentrice,
                    Indirizzo = ef_rapporto.IndirizzoImpresaManutentrice,
                    Civico = ef_rapporto.CivicoImpresaManutentrice,
                    CodiceCatastaleComune = ef_rapporto.ComuneImpresaManutentrice,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaImpresaManutentrice.HasValue ? ef_rapporto.IDProvinciaImpresaManutentrice.Value : 0
                };

                rapporto.fDichiarazioneConformita = ef_rapporto.fDichiarazioneConformita;
                rapporto.fLibrettoImpiantoPresente = ef_rapporto.fLibrettoImpiantoCompilato;
                rapporto.fUsoManutenzioneGeneratore = ef_rapporto.fUsoManutenzioneGeneratore;
                rapporto.fLibrettoImpiantoCompilato = ef_rapporto.fLibrettoImpiantoCompilato;

                rapporto.DurezzaAcqua = ef_rapporto.DurezzaAcqua;
                rapporto.TrattamentoRiscaldamento = ef_rapporto.TrattamentoRiscaldamento;
                rapporto.TrattamentoACS = ef_rapporto.TrattamentoACS;

                rapporto.TipoTrattamentoRiscaldamento = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale)
                {
                    rapporto.TipoTrattamentoRiscaldamento.Add(item.SYS_TipologiaTrattamentoAcqua.IDTipologiaTrattamentoAcqua);
                }

                rapporto.TipoTrattamentoACS = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs)
                {
                    rapporto.TipoTrattamentoACS.Add(item.SYS_TipologiaTrattamentoAcqua.IDTipologiaTrattamentoAcqua);
                }

                rapporto.LocaleInstallazioneIdoneo = ef_rapporto.LocaleInstallazioneIdoneo;
                rapporto.GeneratoriIdonei = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.GeneratoriIdonei;
                rapporto.DimensioniApertureAdeguate = ef_rapporto.DimensioniApertureAdeguate;
                rapporto.ApertureLibere = ef_rapporto.ApertureLibere;
                rapporto.AssenzaPerditeCombustibile = ef_rapporto.AssenzaPerditeCombustibile;
                rapporto.TenutaImpiantoIdraulico = ef_rapporto.TenutaImpiantoIdraulico;
                rapporto.ScarichiIdonei = ef_rapporto.ScarichiIdonei;
                rapporto.Contabilizzazione = ef_rapporto.Contabilizzazione;
                rapporto.Termoregolazione = ef_rapporto.Termoregolazione;
                rapporto.CorrettoFunzionamentoContabilizzazione = ef_rapporto.CorrettoFunzionamentoContabilizzazione;
                rapporto.RegolazioneTemperaturaAmbiente = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.RegolazioneTemperaturaAmbiente;

                rapporto.CodiceProgressivo = ef_rapporto.CodiceProgressivo;
                rapporto.Fabbricante = ef_rapporto.Fabbricante;
                rapporto.Modello = ef_rapporto.Modello;
                rapporto.Matricola = ef_rapporto.Matricola;

                rapporto.TipologiaGruppiTermici = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.IdTipologiaGruppiTermici;
                rapporto.TipologiaSistemaDistribuzione = ef_rapporto.IDTipologiaSistemaDistribuzione;
                rapporto.TipologiaContabilizzazione = ef_rapporto.IDTipologiaContabilizzazione;

                rapporto.TipologiaGeneratoriTermici = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.IdTipologiaGeneratoriTermici;
                rapporto.PotenzaTermicaNominaleFocolare = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.PotenzaTermicaNominaleFocolare;
                rapporto.PotenzaTermicaNominale = ef_rapporto.PotenzaTermicaNominale;
                rapporto.fClimatizzazioneInvernale = ef_rapporto.fClimatizzazioneInvernale;
                rapporto.fProduzioneACS = ef_rapporto.fProduzioneACS;
                rapporto.TipologiaCombustibile = ef_rapporto.SYS_TipologiaCombustibile.IDTipologiaCombustibile;
                rapporto.AltroCombustibile = ef_rapporto.AltroCombustibile;
                rapporto.EvacuazioneForzata = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.EvacuazioneForzata;
                rapporto.EvacuazioneNaturale = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.EvacuazioneNaturale;
                rapporto.DepressioneCanaleFumo = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.DepressioneCanaleFumo;
                rapporto.DispositiviComandoRegolazione = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.DispositiviComandoRegolazione;
                rapporto.DispositiviSicurezza = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.DispositiviSicurezza;
                rapporto.ValvolaSicurezzaSovrappressione = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.ValvolaSicurezzaSovrappressione;
                rapporto.ScambiatoreFumiPulito = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.ScambiatoreFumiPulito;
                rapporto.RiflussoProdottiCombustione = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.RiflussoProdottiCombustione;
                rapporto.ConformitaUNI10389 = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.ConformitaUNI10389;
                rapporto.ModuloTermico = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.ModuloTermico;
                rapporto.TemperaturaFumi = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.TemperaturaFumi;
                rapporto.TemperaraturaComburente = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.TemperaraturaComburente;
                rapporto.O2 = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.O2;
                rapporto.Co2 = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.Co2;
                rapporto.bacharach1 = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.bacharach1;
                rapporto.bacharach2 = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.bacharach2;
                rapporto.bacharach3 = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.bacharach3;
                rapporto.COFumiSecchi = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.COFumiSecchi;
                rapporto.CoCorretto = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.CoCorretto;
                rapporto.PortataCombustibile = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.PortataCombustibile;
                rapporto.PotenzaTermicaEffettiva = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.PotenzaTermicaEffettiva;
                rapporto.RendimentoCombustione = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.RendimentoCombustione;
                rapporto.RendimentoMinimo = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.RendimentoMinimo;
                rapporto.RispettaIndiceBacharach = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.RispettaIndiceBacharach;
                rapporto.COFumiSecchiNoAria1000 = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.COFumiSecchiNoAria1000;
                rapporto.RendimentoSupMinimo = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.RendimentoSupMinimo;

                rapporto.CheckList = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseCheckList)
                {
                    rapporto.CheckList.Add(item.IDCheckList);
                }

                rapporto.Osservazioni = ef_rapporto.Osservazioni;
                rapporto.Raccomandazioni = ef_rapporto.Raccomandazioni;
                rapporto.Prescrizioni = ef_rapporto.Prescrizioni;
                rapporto.fImpiantoFunzionante = ef_rapporto.fImpiantoFunzionante;
                rapporto.DataManutenzioneConsigliata = ef_rapporto.DataManutenzioneConsigliata;
                rapporto.DataControllo = ef_rapporto.DataControllo;
                rapporto.OraArrivo = ef_rapporto.OraArrivo;
                rapporto.OraPartenza = ef_rapporto.OraPartenza;

                rapporto.TecnicoIntervento = GetTecnicoIntervento(ef_rapporto.IDSoggettoDerived);
                rapporto.GuidRapportoTecnico = ef_rapporto.GuidRapportoTecnico;

                rapporto.BolliniCalorePulito = new List<string>();
                foreach (var item in ef_rapporto.RCT_BollinoCalorePulito)
                {
                    rapporto.BolliniCalorePulito.Add(item.CodiceBollino.ToString());
                }

                rapporto.RaccomandazioniPrescrizioni = new List<POMJ_RaccomandazioniPrescrizioni>();
                foreach (var rapp in ef_rapporto.RCT_RaccomandazioniPrescrizioni)
                {
                    POMJ_RaccomandazioniPrescrizioni raccomandazionePrescrizione = new POMJ_RaccomandazioniPrescrizioni()
                    {
                        IDCampoRct = rapp.IDTipologiaRaccomandazionePrescrizioneRct,
                        IDNonConformita = (rapp.IDTipologiaRaccomandazione != null) ? (int)rapp.IDTipologiaRaccomandazione : (int)rapp.IDTipologiaPrescrizione,
                        TipoNonConformita = (rapp.IDTipologiaRaccomandazione != null) ? "Raccomandazione" : "Prescrizione"
                    };
                    rapporto.RaccomandazioniPrescrizioni.Add(raccomandazionePrescrizione);
                }
            }

            return rapporto;
        }

        public POMJ_RapportoControlloTecnico_GT GetRapportoTecnico_GT_ByGuid(string guidrapporto)
        {
            POMJ_RapportoControlloTecnico_GT rapporto = new POMJ_RapportoControlloTecnico_GT();

            var ef_rapporto = (from r in db.RCT_RapportoDiControlloTecnicoBase

                               .Include("SYS_StatoRapportoDiControllo")
                               .Include("SYS_TipologiaCombustibile")
                               .Include("SYS_TipologiaFluidoTermoVettore")
                               .Include("SYS_TipologiaSistemaDistribuzione")
                               .Include("SYS_TipologiaContabilizzazione")
                               .Include("SYS_TipologiaCombustibile")

                              .Include("RCT_RapportoDiControlloTecnicoBaseCheckList")
                              .Include("RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs")
                              .Include("RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale")
                              .Include("RCT_RaccomandazioniPrescrizioni")
                              //.Include("RCT_BollinoCalorePulito")
                              .Include("RCT_RapportoDiControlloTecnicoGT")
                               join t in db.LIM_TargatureImpianti on r.IDTargaturaImpianto equals t.IDTargaturaImpianto
                               where r.GuidRapportoTecnico == guidrapporto
                               select r).FirstOrDefault();
            if (ef_rapporto != null)
            {
                rapporto.CodiceTargaturaImpianto = new POMJ_TargaturaImpianto()
                {
                    CodiceTargatura = ef_rapporto.LIM_TargatureImpianti.CodiceTargatura
                };

                rapporto.PotenzaTermicaNominaleTotaleMax = ef_rapporto.PotenzaTermicaNominaleTotaleMax;
                rapporto.Indirizzo = ef_rapporto.Indirizzo;
                rapporto.Civico = ef_rapporto.Civico;
                rapporto.Palazzo = ef_rapporto.Palazzo;
                rapporto.Scala = ef_rapporto.Scala;
                rapporto.Interno = ef_rapporto.Interno;
                rapporto.StatoRapportoDiControllo = ef_rapporto.SYS_StatoRapportoDiControllo.IDStatoRapportoDiControllo;
                rapporto.TipologiaControllo = ef_rapporto.SYS_TipologiaControllo.IDTipologiaControllo;
                rapporto.TipologiaResponsabile = ef_rapporto.SYS_TipologiaResponsabile.IDTipologiaResponsabile;

                rapporto.ResponsabileImpianto = new POMJ_AnagraficaSoggetti()
                {
                    TipoSoggetto = ef_rapporto.SYS_TipoSoggetto.IDTipoSoggetto,
                    Nome = ef_rapporto.NomeResponsabile,
                    Cognome = ef_rapporto.CognomeResponsabile,
                    CodiceFiscale = ef_rapporto.CodiceFiscaleResponsabile,
                    NomeAzienda = ef_rapporto.RagioneSocialeResponsabile,
                    PartitaIVA = ef_rapporto.PartitaIVAResponsabile,
                    Indirizzo = ef_rapporto.IndirizzoResponsabile,
                    Civico = ef_rapporto.Civico,
                    CodiceCatastaleComune = ef_rapporto.ComuneResponsabile,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaResponsabile.HasValue ? ef_rapporto.IDProvinciaResponsabile.Value : 0
                };

                rapporto.TerzoResponsabile = new POMJ_AnagraficaSoggetti()
                {
                    NomeAzienda = ef_rapporto.RagioneSocialeTerzoResponsabile,
                    PartitaIVA = ef_rapporto.PartitaIVATerzoResponsabile,
                    Indirizzo = ef_rapporto.IndirizzoTerzoResponsabile,
                    Civico = ef_rapporto.Civico,
                    CodiceCatastaleComune = ef_rapporto.ComuneTerzoResponsabile,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaTerzoResponsabile.HasValue ? ef_rapporto.IDProvinciaTerzoResponsabile.Value : 0,

                };

                rapporto.ImpresaManutentrice = new POMJ_AnagraficaSoggetti()
                {
                    NomeAzienda = ef_rapporto.RagioneSocialeImpresaManutentrice,
                    PartitaIVA = ef_rapporto.PartitaIVAImpresaManutentrice,
                    Indirizzo = ef_rapporto.IndirizzoImpresaManutentrice,
                    Civico = ef_rapporto.CivicoImpresaManutentrice,
                    CodiceCatastaleComune = ef_rapporto.ComuneImpresaManutentrice,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaImpresaManutentrice.HasValue ? ef_rapporto.IDProvinciaImpresaManutentrice.Value : 0
                };

                rapporto.fDichiarazioneConformita = ef_rapporto.fDichiarazioneConformita;
                rapporto.fLibrettoImpiantoPresente = ef_rapporto.fLibrettoImpiantoCompilato;
                rapporto.fUsoManutenzioneGeneratore = ef_rapporto.fUsoManutenzioneGeneratore;
                rapporto.fLibrettoImpiantoCompilato = ef_rapporto.fLibrettoImpiantoCompilato;

                rapporto.DurezzaAcqua = ef_rapporto.DurezzaAcqua;
                rapporto.TrattamentoRiscaldamento = ef_rapporto.TrattamentoRiscaldamento;
                rapporto.TrattamentoACS = ef_rapporto.TrattamentoACS;

                rapporto.TipoTrattamentoRiscaldamento = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale)
                {
                    rapporto.TipoTrattamentoRiscaldamento.Add(item.SYS_TipologiaTrattamentoAcqua.IDTipologiaTrattamentoAcqua);
                }

                rapporto.TipoTrattamentoACS = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs)
                {
                    rapporto.TipoTrattamentoACS.Add(item.SYS_TipologiaTrattamentoAcqua.IDTipologiaTrattamentoAcqua);
                }


                rapporto.LocaleInstallazioneIdoneo = ef_rapporto.LocaleInstallazioneIdoneo;
                rapporto.GeneratoriIdonei = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.GeneratoriIdonei;
                rapporto.DimensioniApertureAdeguate = ef_rapporto.DimensioniApertureAdeguate;
                rapporto.ApertureLibere = ef_rapporto.ApertureLibere;
                rapporto.AssenzaPerditeCombustibile = ef_rapporto.AssenzaPerditeCombustibile;
                rapporto.TenutaImpiantoIdraulico = ef_rapporto.TenutaImpiantoIdraulico;
                rapporto.ScarichiIdonei = ef_rapporto.ScarichiIdonei;
                rapporto.Contabilizzazione = ef_rapporto.Contabilizzazione;
                rapporto.Termoregolazione = ef_rapporto.Termoregolazione;
                rapporto.CorrettoFunzionamentoContabilizzazione = ef_rapporto.CorrettoFunzionamentoContabilizzazione;
                rapporto.RegolazioneTemperaturaAmbiente = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.RegolazioneTemperaturaAmbiente;

                rapporto.CodiceProgressivo = ef_rapporto.CodiceProgressivo;
                rapporto.Fabbricante = ef_rapporto.Fabbricante;
                rapporto.Modello = ef_rapporto.Modello;
                rapporto.Matricola = ef_rapporto.Matricola;

                rapporto.TipologiaGruppiTermici = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.IdTipologiaGruppiTermici;//.SYS_TipologiaGruppiTermici !=null ? ef_rapporto.RCT_RapportoDiControlloTecnicoGT.SYS_TipologiaGruppiTermici.IDTipologiaGruppiTermici : null;
                rapporto.TipologiaGeneratoriTermici = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.IdTipologiaGeneratoriTermici;
                rapporto.PotenzaTermicaNominaleFocolare = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.PotenzaTermicaNominaleFocolare;
                rapporto.PotenzaTermicaNominale = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.PotenzaTermicaEffettiva;
                rapporto.fClimatizzazioneInvernale = ef_rapporto.fClimatizzazioneInvernale;
                rapporto.fProduzioneACS = ef_rapporto.fProduzioneACS;
                rapporto.TipologiaCombustibile = ef_rapporto.SYS_TipologiaCombustibile.IDTipologiaCombustibile;
                rapporto.AltroCombustibile = ef_rapporto.AltroCombustibile;
                rapporto.EvacuazioneForzata = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.EvacuazioneForzata;
                rapporto.EvacuazioneNaturale = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.EvacuazioneNaturale;
                rapporto.DepressioneCanaleFumo = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.DepressioneCanaleFumo;
                rapporto.DispositiviComandoRegolazione = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.DispositiviComandoRegolazione;
                rapporto.DispositiviSicurezza = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.DispositiviSicurezza;
                rapporto.ValvolaSicurezzaSovrappressione = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.ValvolaSicurezzaSovrappressione;
                rapporto.ScambiatoreFumiPulito = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.ScambiatoreFumiPulito;
                rapporto.RiflussoProdottiCombustione = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.RiflussoProdottiCombustione;
                rapporto.ConformitaUNI10389 = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.ConformitaUNI10389;
                rapporto.ModuloTermico = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.ModuloTermico;
                rapporto.TemperaturaFumi = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.TemperaturaFumi;
                rapporto.TemperaraturaComburente = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.TemperaraturaComburente;
                rapporto.O2 = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.O2;
                rapporto.Co2 = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.Co2;
                rapporto.bacharach1 = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.bacharach1;
                rapporto.bacharach2 = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.bacharach2;
                rapporto.bacharach3 = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.bacharach3;
                rapporto.COFumiSecchi = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.COFumiSecchi;
                rapporto.CoCorretto = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.CoCorretto;
                rapporto.PortataCombustibile = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.PortataCombustibile;
                rapporto.PotenzaTermicaEffettiva = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.PotenzaTermicaEffettiva;
                rapporto.RendimentoCombustione = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.RendimentoCombustione;
                rapporto.RendimentoMinimo = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.RendimentoMinimo;
                rapporto.RispettaIndiceBacharach = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.RispettaIndiceBacharach;
                rapporto.COFumiSecchiNoAria1000 = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.COFumiSecchiNoAria1000;
                rapporto.RendimentoSupMinimo = ef_rapporto.RCT_RapportoDiControlloTecnicoGT.RendimentoSupMinimo;

                rapporto.CheckList = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseCheckList)
                {
                    rapporto.CheckList.Add(item.IDCheckList);
                }

                rapporto.Osservazioni = ef_rapporto.Osservazioni;
                rapporto.Raccomandazioni = ef_rapporto.Raccomandazioni;
                rapporto.Prescrizioni = ef_rapporto.Prescrizioni;
                rapporto.fImpiantoFunzionante = ef_rapporto.fImpiantoFunzionante;
                rapporto.DataManutenzioneConsigliata = ef_rapporto.DataManutenzioneConsigliata;
                rapporto.DataControllo = ef_rapporto.DataControllo;
                rapporto.OraArrivo = ef_rapporto.OraArrivo;
                rapporto.OraPartenza = ef_rapporto.OraPartenza;

                rapporto.TecnicoIntervento = GetTecnicoIntervento(ef_rapporto.IDSoggettoDerived);
                rapporto.GuidRapportoTecnico = ef_rapporto.GuidRapportoTecnico;

                rapporto.BolliniCalorePulito = new List<string>();
                foreach (var item in ef_rapporto.RCT_BollinoCalorePulito)
                {
                    rapporto.BolliniCalorePulito.Add(item.CodiceBollino.ToString());
                }

                rapporto.RaccomandazioniPrescrizioni = new List<POMJ_RaccomandazioniPrescrizioni>();
                foreach (var rapp in ef_rapporto.RCT_RaccomandazioniPrescrizioni)
                {
                    POMJ_RaccomandazioniPrescrizioni raccomandazionePrescrizione = new POMJ_RaccomandazioniPrescrizioni()
                    {
                        IDCampoRct = rapp.IDTipologiaRaccomandazionePrescrizioneRct,
                        IDNonConformita = (rapp.IDTipologiaRaccomandazione != null) ? (int)rapp.IDTipologiaRaccomandazione : (int)rapp.IDTipologiaPrescrizione,
                        TipoNonConformita = (rapp.IDTipologiaRaccomandazione != null) ? "Raccomandazione" : "Prescrizione"
                    };
                    rapporto.RaccomandazioniPrescrizioni.Add(raccomandazionePrescrizione);
                }
            }

            return rapporto;
        }

        public POMJ_RapportoControlloTecnico_GF GetRapportoTecnico_GF_ByID(int idRapporto)
        {
            POMJ_RapportoControlloTecnico_GF rapporto = new POMJ_RapportoControlloTecnico_GF();

            var ef_rapporto = (from r in db.RCT_RapportoDiControlloTecnicoBase

                              .Include("SYS_StatoRapportoDiControllo")
                              .Include("SYS_TipologiaCombustibile")
                              .Include("SYS_TipologiaFluidoTermoVettore")
                              .Include("SYS_TipologiaSistemaDistribuzione")
                              .Include("SYS_TipologiaContabilizzazione")
                              .Include("SYS_TipologiaCombustibile")

                             .Include("RCT_RapportoDiControlloTecnicoBaseCheckList")
                             .Include("RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs")
                             .Include("RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale")
                             .Include("RCT_RaccomandazioniPrescrizioni")
                             .Include("RCT_RapportoDiControlloTecnicoCG")
                               join t in db.LIM_TargatureImpianti on r.IDTargaturaImpianto equals t.IDTargaturaImpianto
                               where r.IDRapportoControlloTecnico == idRapporto
                               select r).FirstOrDefault();
            if (ef_rapporto != null)
            {
                rapporto.CodiceTargaturaImpianto = new POMJ_TargaturaImpianto()
                {
                    CodiceTargatura = ef_rapporto.LIM_TargatureImpianti.CodiceTargatura
                };

                rapporto.PotenzaTermicaNominaleTotaleMax = ef_rapporto.PotenzaTermicaNominaleTotaleMax;
                rapporto.Indirizzo = ef_rapporto.Indirizzo;
                rapporto.Civico = ef_rapporto.Civico;
                rapporto.Palazzo = ef_rapporto.Palazzo;
                rapporto.Scala = ef_rapporto.Scala;
                rapporto.Interno = ef_rapporto.Interno;
                rapporto.StatoRapportoDiControllo = ef_rapporto.SYS_StatoRapportoDiControllo.IDStatoRapportoDiControllo;
                rapporto.TipologiaControllo = ef_rapporto.SYS_TipologiaControllo.IDTipologiaControllo;
                rapporto.TipologiaResponsabile = ef_rapporto.SYS_TipologiaResponsabile.IDTipologiaResponsabile;

                rapporto.ResponsabileImpianto = new Criter.Anagrafica.POMJ_AnagraficaSoggetti()
                {
                    TipoSoggetto = ef_rapporto.IDTipoSoggetto.Value,
                    Nome = ef_rapporto.NomeResponsabile,
                    Cognome = ef_rapporto.CognomeResponsabile,
                    CodiceFiscale = ef_rapporto.CodiceFiscaleResponsabile,
                    NomeAzienda = ef_rapporto.RagioneSocialeResponsabile,
                    PartitaIVA = ef_rapporto.PartitaIVAResponsabile,
                    Indirizzo = ef_rapporto.IndirizzoResponsabile,
                    Civico = ef_rapporto.Civico,
                    CodiceCatastaleComune = ef_rapporto.ComuneResponsabile,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaResponsabile.HasValue ? ef_rapporto.IDProvinciaResponsabile.Value : 0
                };

                rapporto.TerzoResponsabile = new Criter.Anagrafica.POMJ_AnagraficaSoggetti()
                {
                    TipoSoggetto = ef_rapporto.IDTipoSoggetto.Value,
                    NomeAzienda = ef_rapporto.RagioneSocialeTerzoResponsabile,
                    PartitaIVA = ef_rapporto.PartitaIVATerzoResponsabile,
                    Indirizzo = ef_rapporto.IndirizzoTerzoResponsabile,
                    Civico = ef_rapporto.Civico,
                    CodiceCatastaleComune = ef_rapporto.ComuneTerzoResponsabile,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaTerzoResponsabile.HasValue ? ef_rapporto.IDProvinciaTerzoResponsabile.Value : 0,

                };

                rapporto.ImpresaManutentrice = new Criter.Anagrafica.POMJ_AnagraficaSoggetti()
                {
                    TipoSoggetto = ef_rapporto.IDTipoSoggetto.Value,
                    NomeAzienda = ef_rapporto.RagioneSocialeImpresaManutentrice,
                    PartitaIVA = ef_rapporto.PartitaIVAImpresaManutentrice,
                    Indirizzo = ef_rapporto.IndirizzoImpresaManutentrice,
                    Civico = ef_rapporto.CivicoImpresaManutentrice,
                    CodiceCatastaleComune = ef_rapporto.ComuneImpresaManutentrice,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaImpresaManutentrice.HasValue ? ef_rapporto.IDProvinciaImpresaManutentrice.Value : 0
                };

                rapporto.fDichiarazioneConformita = ef_rapporto.fDichiarazioneConformita;
                rapporto.fLibrettoImpiantoPresente = ef_rapporto.fLibrettoImpiantoCompilato;
                rapporto.fUsoManutenzioneGeneratore = ef_rapporto.fUsoManutenzioneGeneratore;
                rapporto.fLibrettoImpiantoCompilato = ef_rapporto.fLibrettoImpiantoCompilato;

                rapporto.DurezzaAcqua = ef_rapporto.DurezzaAcqua;
                rapporto.TrattamentoRiscaldamento = ef_rapporto.TrattamentoRiscaldamento;

                rapporto.TipoTrattamentoRiscaldamento = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale)
                {
                    rapporto.TipoTrattamentoRiscaldamento.Add(item.SYS_TipologiaTrattamentoAcqua.IDTipologiaTrattamentoAcqua);
                }

                rapporto.TipoTrattamentoACS = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs)
                {
                    rapporto.TipoTrattamentoACS.Add(item.SYS_TipologiaTrattamentoAcqua.IDTipologiaTrattamentoAcqua);
                }

                rapporto.TrattamentoACS = ef_rapporto.TrattamentoACS;
                rapporto.LocaleInstallazioneIdoneo = ef_rapporto.LocaleInstallazioneIdoneo;
                rapporto.DimensioniApertureAdeguate = ef_rapporto.DimensioniApertureAdeguate;
                rapporto.ApertureLibere = ef_rapporto.ApertureLibere;
                rapporto.LineeElettricheIdonee = ef_rapporto.LineeElettricheIdonee;
                rapporto.CoibentazioniIdonee = ef_rapporto.CoibentazioniIdonee;

                rapporto.CodiceProgressivo = ef_rapporto.CodiceProgressivo;
                rapporto.DataInstallazione = ef_rapporto.DataInstallazione;
                rapporto.Fabbricante = ef_rapporto.Fabbricante;
                rapporto.Modello = ef_rapporto.Modello;
                rapporto.Matricola = ef_rapporto.Matricola;

                rapporto.TipologiaSistemaDistribuzione = ef_rapporto.IDTipologiaSistemaDistribuzione;
                rapporto.TipologiaContabilizzazione = ef_rapporto.IDTipologiaContabilizzazione;

                rapporto.Contabilizzazione = ef_rapporto.Contabilizzazione;
                rapporto.Termoregolazione = ef_rapporto.Termoregolazione;
                rapporto.CorrettoFunzionamentoContabilizzazione = ef_rapporto.CorrettoFunzionamentoContabilizzazione;

                rapporto.NCircuitiTotali = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.NCircuitiTotali;
                rapporto.Potenzafrigorifera = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.Potenzafrigorifera;
                rapporto.PotenzaTermicaNominale = ef_rapporto.PotenzaTermicaNominale;
                rapporto.fClimatizzazioneInvernale = ef_rapporto.fClimatizzazioneInvernale;
                rapporto.fClimatizzazioneEstiva = ef_rapporto.fClimatizzazioneEstiva;
                rapporto.fProduzioneACS = ef_rapporto.fProduzioneACS;
                //rapporto.fRiscaldamento = ?
                //rapporto.fRaffrescamento = ?
                rapporto.TipologiaMacchineFrigorifere = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.SYS_TipologiaMacchineFrigorifere.IDTipologiaMacchineFrigorifere;
                rapporto.AssenzaPerditeRefrigerante = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.AssenzaPerditeRefrigerante;
                rapporto.FiltriPuliti = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.FiltriPuliti;
                rapporto.LeakDetector = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.LeakDetector;
                rapporto.ScambiatoriLiberi = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.ScambiatoriLiberi;
                rapporto.ParametriTermodinamici = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.ParametriTermodinamici;
                rapporto.NCircuiti = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.NCircuiti;
                rapporto.TemperaturaSurriscaldamento = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TemperaturaSurriscaldamento;
                rapporto.TemperaturaSottoraffreddamento = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TemperaturaSottoraffreddamento;
                rapporto.TemperaturaCondensazione = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TemperaturaCondensazione;
                rapporto.TemperaturaEvaporazione = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TemperaturaEvaporazione;
                rapporto.TInglatoEst = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TInglatoEst;
                rapporto.TUscLatoEst = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TUscLatoEst;
                rapporto.TIngLatoUtenze = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TIngLatoUtenze;
                rapporto.TUscLatoUtenze = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TUscLatoUtenze;
                rapporto.PotenzaAssorbita = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.PotenzaAssorbita;
                rapporto.TUscitaFluido = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TUscitaFluido;
                rapporto.TBulboUmidoAria = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TBulboUmidoAria;
                rapporto.TIngressoLatoEsterno = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TIngressoLatoEsterno;
                rapporto.TUscitaLatoEsterno = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TUscitaLatoEsterno;
                rapporto.TIngressoLatoMacchina = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TIngressoLatoMacchina;
                rapporto.TUscitaLatoMacchina = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TUscitaLatoMacchina;


                rapporto.CheckList = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseCheckList)
                {
                    rapporto.CheckList.Add(item.IDCheckList);
                }

                rapporto.Osservazioni = ef_rapporto.Osservazioni;
                rapporto.Raccomandazioni = ef_rapporto.Raccomandazioni;
                rapporto.Prescrizioni = ef_rapporto.Prescrizioni;
                rapporto.fImpiantoFunzionante = ef_rapporto.fImpiantoFunzionante;
                rapporto.DataManutenzioneConsigliata = ef_rapporto.DataManutenzioneConsigliata;
                rapporto.DataControllo = ef_rapporto.DataControllo;
                rapporto.OraArrivo = ef_rapporto.OraArrivo;
                rapporto.OraPartenza = ef_rapporto.OraPartenza;

                rapporto.TecnicoIntervento = GetTecnicoIntervento(ef_rapporto.IDSoggettoDerived);
                rapporto.GuidRapportoTecnico = ef_rapporto.GuidRapportoTecnico;

                rapporto.BolliniCalorePulito = new List<string>();
                foreach (var item in ef_rapporto.RCT_BollinoCalorePulito)
                {
                    rapporto.BolliniCalorePulito.Add(item.CodiceBollino.ToString());
                }

                rapporto.RaccomandazioniPrescrizioni = new List<POMJ_RaccomandazioniPrescrizioni>();
                foreach (var rapp in ef_rapporto.RCT_RaccomandazioniPrescrizioni)
                {
                    POMJ_RaccomandazioniPrescrizioni raccomandazionePrescrizione = new POMJ_RaccomandazioniPrescrizioni()
                    {
                        IDCampoRct = rapp.IDTipologiaRaccomandazionePrescrizioneRct,
                        IDNonConformita = (rapp.IDTipologiaRaccomandazione != null) ? (int)rapp.IDTipologiaRaccomandazione : (int)rapp.IDTipologiaPrescrizione,
                        TipoNonConformita = (rapp.IDTipologiaRaccomandazione != null) ? "Raccomandazione" : "Prescrizione"
                    };
                    rapporto.RaccomandazioniPrescrizioni.Add(raccomandazionePrescrizione);
                }
            }
            return rapporto;
        }

        public POMJ_RapportoControlloTecnico_GF GetRapportoTecnico_GF_ByGuid(string guidrapporto)
        {
            POMJ_RapportoControlloTecnico_GF rapporto = new POMJ_RapportoControlloTecnico_GF();

            var ef_rapporto = (from r in db.RCT_RapportoDiControlloTecnicoBase
                              .Include("SYS_StatoRapportoDiControllo")
                              .Include("SYS_TipologiaCombustibile")
                              .Include("SYS_TipologiaFluidoTermoVettore")
                              .Include("SYS_TipologiaSistemaDistribuzione")
                              .Include("SYS_TipologiaContabilizzazione")
                              .Include("SYS_TipologiaCombustibile")

                              .Include("RCT_RapportoDiControlloTecnicoBaseCheckList")
                              .Include("RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs")
                              .Include("RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale")
                              .Include("RCT_RaccomandazioniPrescrizioni")
                              .Include("RCT_RapportoDiControlloTecnicoCG")
                               join t in db.LIM_TargatureImpianti on r.IDTargaturaImpianto equals t.IDTargaturaImpianto
                               where r.GuidRapportoTecnico == guidrapporto
                               select r).FirstOrDefault();
            if (ef_rapporto != null)
            {
                rapporto.CodiceTargaturaImpianto = new POMJ_TargaturaImpianto()
                {
                    CodiceTargatura = ef_rapporto.LIM_TargatureImpianti.CodiceTargatura
                };

                // RCT_RapportoDiControlloTecnicoBase fields
                rapporto.PotenzaTermicaNominaleTotaleMax = ef_rapporto.PotenzaTermicaNominaleTotaleMax;
                rapporto.Indirizzo = ef_rapporto.Indirizzo;
                rapporto.Civico = ef_rapporto.Civico;
                rapporto.Palazzo = ef_rapporto.Palazzo;
                rapporto.Scala = ef_rapporto.Scala;
                rapporto.Interno = ef_rapporto.Interno;
                rapporto.StatoRapportoDiControllo = ef_rapporto.SYS_StatoRapportoDiControllo.IDStatoRapportoDiControllo;
                rapporto.TipologiaControllo = ef_rapporto.SYS_TipologiaControllo.IDTipologiaControllo;
                rapporto.TipologiaResponsabile = ef_rapporto.SYS_TipologiaResponsabile.IDTipologiaResponsabile;

                rapporto.ResponsabileImpianto = new POMJ_AnagraficaSoggetti()
                {
                    TipoSoggetto = ef_rapporto.SYS_TipoSoggetto.IDTipoSoggetto,
                    Nome = ef_rapporto.NomeResponsabile,
                    Cognome = ef_rapporto.CognomeResponsabile,
                    CodiceFiscale = ef_rapporto.CodiceFiscaleResponsabile,
                    NomeAzienda = ef_rapporto.RagioneSocialeResponsabile,
                    PartitaIVA = ef_rapporto.PartitaIVAResponsabile,
                    Indirizzo = ef_rapporto.IndirizzoResponsabile,
                    Civico = ef_rapporto.Civico,
                    CodiceCatastaleComune = ef_rapporto.ComuneResponsabile,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaResponsabile.HasValue ? ef_rapporto.IDProvinciaResponsabile.Value : 0
                };

                rapporto.TerzoResponsabile = new POMJ_AnagraficaSoggetti()
                {
                    NomeAzienda = ef_rapporto.RagioneSocialeTerzoResponsabile,
                    PartitaIVA = ef_rapporto.PartitaIVATerzoResponsabile,
                    Indirizzo = ef_rapporto.IndirizzoTerzoResponsabile,
                    Civico = ef_rapporto.Civico,
                    CodiceCatastaleComune = ef_rapporto.ComuneTerzoResponsabile,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaTerzoResponsabile.HasValue ? ef_rapporto.IDProvinciaTerzoResponsabile.Value : 0,

                };

                rapporto.ImpresaManutentrice = new Criter.Anagrafica.POMJ_AnagraficaSoggetti()
                {
                    NomeAzienda = ef_rapporto.RagioneSocialeImpresaManutentrice,
                    PartitaIVA = ef_rapporto.PartitaIVAImpresaManutentrice,
                    Indirizzo = ef_rapporto.IndirizzoImpresaManutentrice,
                    Civico = ef_rapporto.CivicoImpresaManutentrice,
                    CodiceCatastaleComune = ef_rapporto.ComuneImpresaManutentrice,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaImpresaManutentrice.HasValue ? ef_rapporto.IDProvinciaImpresaManutentrice.Value : 0
                };

                rapporto.fDichiarazioneConformita = ef_rapporto.fDichiarazioneConformita;
                rapporto.fLibrettoImpiantoPresente = ef_rapporto.fLibrettoImpiantoCompilato;
                rapporto.fUsoManutenzioneGeneratore = ef_rapporto.fUsoManutenzioneGeneratore;
                rapporto.fLibrettoImpiantoCompilato = ef_rapporto.fLibrettoImpiantoCompilato;

                rapporto.DurezzaAcqua = ef_rapporto.DurezzaAcqua;
                rapporto.TrattamentoRiscaldamento = ef_rapporto.TrattamentoRiscaldamento;

                rapporto.TipoTrattamentoRiscaldamento = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale)
                {
                    rapporto.TipoTrattamentoRiscaldamento.Add(item.SYS_TipologiaTrattamentoAcqua.IDTipologiaTrattamentoAcqua);
                }

                rapporto.TipoTrattamentoACS = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs)
                {
                    rapporto.TipoTrattamentoACS.Add(item.SYS_TipologiaTrattamentoAcqua.IDTipologiaTrattamentoAcqua);
                }
                rapporto.TrattamentoACS = ef_rapporto.TrattamentoACS;

                rapporto.LocaleInstallazioneIdoneo = ef_rapporto.LocaleInstallazioneIdoneo;
                rapporto.DimensioniApertureAdeguate = ef_rapporto.DimensioniApertureAdeguate;
                rapporto.ApertureLibere = ef_rapporto.ApertureLibere;
                rapporto.LineeElettricheIdonee = ef_rapporto.LineeElettricheIdonee;
                rapporto.CoibentazioniIdonee = ef_rapporto.CoibentazioniIdonee;

                rapporto.CodiceProgressivo = ef_rapporto.CodiceProgressivo;
                rapporto.DataInstallazione = ef_rapporto.DataInstallazione;
                rapporto.Fabbricante = ef_rapporto.Fabbricante;
                rapporto.Modello = ef_rapporto.Modello;
                rapporto.Matricola = ef_rapporto.Matricola;

                rapporto.TipologiaSistemaDistribuzione = ef_rapporto.IDTipologiaSistemaDistribuzione;
                rapporto.TipologiaContabilizzazione = ef_rapporto.IDTipologiaContabilizzazione;

                rapporto.Contabilizzazione = ef_rapporto.Contabilizzazione;
                rapporto.Termoregolazione = ef_rapporto.Termoregolazione;
                rapporto.CorrettoFunzionamentoContabilizzazione = ef_rapporto.CorrettoFunzionamentoContabilizzazione;

                rapporto.NCircuitiTotali = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.NCircuitiTotali;
                rapporto.Potenzafrigorifera = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.Potenzafrigorifera;
                rapporto.PotenzaTermicaNominale = ef_rapporto.PotenzaTermicaNominale;
                rapporto.fClimatizzazioneInvernale = ef_rapporto.fClimatizzazioneInvernale;
                rapporto.fClimatizzazioneEstiva = ef_rapporto.fClimatizzazioneEstiva;
                rapporto.fProduzioneACS = ef_rapporto.fProduzioneACS;
                //rapporto.fRiscaldamento = ?
                //rapporto.fRaffrescamento = ?
                rapporto.TipologiaMacchineFrigorifere = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.SYS_TipologiaMacchineFrigorifere.IDTipologiaMacchineFrigorifere;
                rapporto.AssenzaPerditeRefrigerante = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.AssenzaPerditeRefrigerante;
                rapporto.FiltriPuliti = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.FiltriPuliti;
                rapporto.LeakDetector = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.LeakDetector;
                rapporto.ScambiatoriLiberi = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.ScambiatoriLiberi;
                rapporto.ParametriTermodinamici = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.ParametriTermodinamici;
                rapporto.NCircuiti = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.NCircuiti;
                rapporto.TemperaturaSurriscaldamento = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TemperaturaSurriscaldamento;
                rapporto.TemperaturaSottoraffreddamento = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TemperaturaSottoraffreddamento;
                rapporto.TemperaturaCondensazione = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TemperaturaCondensazione;
                rapporto.TemperaturaEvaporazione = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TemperaturaEvaporazione;
                rapporto.TInglatoEst = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TInglatoEst;
                rapporto.TUscLatoEst = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TUscLatoEst;
                rapporto.TIngLatoUtenze = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TIngLatoUtenze;
                rapporto.TUscLatoUtenze = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TUscLatoUtenze;
                rapporto.PotenzaAssorbita = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.PotenzaAssorbita;
                rapporto.TUscitaFluido = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TUscitaFluido;
                rapporto.TBulboUmidoAria = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TBulboUmidoAria;
                rapporto.TIngressoLatoEsterno = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TIngressoLatoEsterno;
                rapporto.TUscitaLatoEsterno = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TUscitaLatoEsterno;
                rapporto.TIngressoLatoMacchina = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TIngressoLatoMacchina;
                rapporto.TUscitaLatoMacchina = ef_rapporto.RCT_RapportoDiControlloTecnicoGF.TUscitaLatoMacchina;

                rapporto.CheckList = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseCheckList)
                {
                    rapporto.CheckList.Add(item.IDCheckList);
                }

                rapporto.Osservazioni = ef_rapporto.Osservazioni;
                rapporto.Raccomandazioni = ef_rapporto.Raccomandazioni;
                rapporto.Prescrizioni = ef_rapporto.Prescrizioni;
                rapporto.fImpiantoFunzionante = ef_rapporto.fImpiantoFunzionante;
                rapporto.DataManutenzioneConsigliata = ef_rapporto.DataManutenzioneConsigliata;
                rapporto.DataControllo = ef_rapporto.DataControllo;
                rapporto.OraArrivo = ef_rapporto.OraArrivo;
                rapporto.OraPartenza = ef_rapporto.OraPartenza;

                rapporto.TecnicoIntervento = GetTecnicoIntervento(ef_rapporto.IDSoggettoDerived);
                rapporto.GuidRapportoTecnico = ef_rapporto.GuidRapportoTecnico;

                rapporto.BolliniCalorePulito = new List<string>();
                foreach (var item in ef_rapporto.RCT_BollinoCalorePulito)
                {
                    rapporto.BolliniCalorePulito.Add(item.CodiceBollino.ToString());
                }

                rapporto.RaccomandazioniPrescrizioni = new List<POMJ_RaccomandazioniPrescrizioni>();
                foreach (var rapp in ef_rapporto.RCT_RaccomandazioniPrescrizioni)
                {
                    POMJ_RaccomandazioniPrescrizioni raccomandazionePrescrizione = new POMJ_RaccomandazioniPrescrizioni()
                    {
                        IDCampoRct = rapp.IDTipologiaRaccomandazionePrescrizioneRct,
                        IDNonConformita = (rapp.IDTipologiaRaccomandazione != null) ? (int)rapp.IDTipologiaRaccomandazione : (int)rapp.IDTipologiaPrescrizione,
                        TipoNonConformita = (rapp.IDTipologiaRaccomandazione != null) ? "Raccomandazione" : "Prescrizione"
                    };
                    rapporto.RaccomandazioniPrescrizioni.Add(raccomandazionePrescrizione);
                }
            }
            return rapporto;
        }

        public POMJ_RapportoControlloTecnico_SC GetRapportoTecnico_SC_ByID(int idRapporto)
        {
            POMJ_RapportoControlloTecnico_SC rapporto = new POMJ_RapportoControlloTecnico_SC();

            var ef_rapporto = (from r in db.RCT_RapportoDiControlloTecnicoBase

                              .Include("SYS_StatoRapportoDiControllo")
                              .Include("SYS_TipologiaCombustibile")
                              .Include("SYS_TipologiaFluidoTermoVettore")
                              .Include("SYS_TipologiaSistemaDistribuzione")
                              .Include("SYS_TipologiaContabilizzazione")
                              .Include("SYS_TipologiaCombustibile")

                             .Include("RCT_RapportoDiControlloTecnicoBaseCheckList")
                             .Include("RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs")
                             .Include("RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale")
                             .Include("RCT_RaccomandazioniPrescrizioni")
                             .Include("RCT_RapportoDiControlloTecnicoCG")
                               join t in db.LIM_TargatureImpianti on r.IDTargaturaImpianto equals t.IDTargaturaImpianto
                               where r.IDRapportoControlloTecnico == idRapporto
                               select r).FirstOrDefault();

            if (ef_rapporto != null)
            {
                rapporto.CodiceTargaturaImpianto = new POMJ_TargaturaImpianto()
                {
                    CodiceTargatura = ef_rapporto.LIM_TargatureImpianti.CodiceTargatura
                };

                // RCT_RapportoDiControlloTecnicoBase fields
                rapporto.PotenzaTermicaNominaleTotaleMax = ef_rapporto.PotenzaTermicaNominaleTotaleMax;
                rapporto.Indirizzo = ef_rapporto.Indirizzo;
                rapporto.Civico = ef_rapporto.Civico;
                rapporto.Palazzo = ef_rapporto.Palazzo;
                rapporto.Scala = ef_rapporto.Scala;
                rapporto.Interno = ef_rapporto.Interno;
                rapporto.StatoRapportoDiControllo = ef_rapporto.SYS_StatoRapportoDiControllo.IDStatoRapportoDiControllo;
                rapporto.TipologiaControllo = ef_rapporto.SYS_TipologiaControllo.IDTipologiaControllo;
                rapporto.TipologiaResponsabile = ef_rapporto.SYS_TipologiaResponsabile.IDTipologiaResponsabile;

                rapporto.ResponsabileImpianto = new Criter.Anagrafica.POMJ_AnagraficaSoggetti()
                {
                    TipoSoggetto = ef_rapporto.IDTipoSoggetto.Value,
                    Nome = ef_rapporto.NomeResponsabile,
                    Cognome = ef_rapporto.CognomeResponsabile,
                    CodiceFiscale = ef_rapporto.CodiceFiscaleResponsabile,
                    NomeAzienda = ef_rapporto.RagioneSocialeResponsabile,
                    PartitaIVA = ef_rapporto.PartitaIVAResponsabile,
                    Indirizzo = ef_rapporto.IndirizzoResponsabile,
                    Civico = ef_rapporto.Civico,
                    CodiceCatastaleComune = ef_rapporto.ComuneResponsabile,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaResponsabile.HasValue ? ef_rapporto.IDProvinciaResponsabile.Value : 0
                };

                rapporto.TerzoResponsabile = new Criter.Anagrafica.POMJ_AnagraficaSoggetti()
                {
                    TipoSoggetto = ef_rapporto.IDTipoSoggetto.Value,
                    NomeAzienda = ef_rapporto.RagioneSocialeTerzoResponsabile,
                    PartitaIVA = ef_rapporto.PartitaIVATerzoResponsabile,
                    Indirizzo = ef_rapporto.IndirizzoTerzoResponsabile,
                    Civico = ef_rapporto.Civico,
                    CodiceCatastaleComune = ef_rapporto.ComuneTerzoResponsabile,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaTerzoResponsabile.HasValue ? ef_rapporto.IDProvinciaTerzoResponsabile.Value : 0,

                };

                rapporto.ImpresaManutentrice = new Criter.Anagrafica.POMJ_AnagraficaSoggetti()
                {
                    TipoSoggetto = ef_rapporto.IDTipoSoggetto.Value,
                    NomeAzienda = ef_rapporto.RagioneSocialeImpresaManutentrice,
                    PartitaIVA = ef_rapporto.PartitaIVAImpresaManutentrice,
                    Indirizzo = ef_rapporto.IndirizzoImpresaManutentrice,
                    Civico = ef_rapporto.CivicoImpresaManutentrice,
                    CodiceCatastaleComune = ef_rapporto.ComuneImpresaManutentrice,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaImpresaManutentrice.HasValue ? ef_rapporto.IDProvinciaImpresaManutentrice.Value : 0
                };

                rapporto.fDichiarazioneConformita = ef_rapporto.fDichiarazioneConformita;
                rapporto.fLibrettoImpiantoPresente = ef_rapporto.fLibrettoImpiantoCompilato;
                rapporto.fUsoManutenzioneGeneratore = ef_rapporto.fUsoManutenzioneGeneratore;
                rapporto.fLibrettoImpiantoCompilato = ef_rapporto.fLibrettoImpiantoCompilato;

                rapporto.DurezzaAcqua = ef_rapporto.DurezzaAcqua;
                rapporto.TrattamentoRiscaldamento = ef_rapporto.TrattamentoRiscaldamento;
                rapporto.TrattamentoACS = ef_rapporto.TrattamentoACS;

                rapporto.TipoTrattamentoRiscaldamento = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale)
                {
                    rapporto.TipoTrattamentoRiscaldamento.Add(item.SYS_TipologiaTrattamentoAcqua.IDTipologiaTrattamentoAcqua);
                }


                rapporto.TipoTrattamentoACS = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs)
                {
                    rapporto.TipoTrattamentoACS.Add(item.SYS_TipologiaTrattamentoAcqua.IDTipologiaTrattamentoAcqua);
                }

                rapporto.LocaleInstallazioneIdoneo = ef_rapporto.LocaleInstallazioneIdoneo;
                rapporto.LineeElettricheIdonee = ef_rapporto.LineeElettricheIdonee;
                rapporto.StatoCoibentazioniIdonee = ef_rapporto.StatoCoibentazioniIdonee;
                rapporto.CoibentazioniIdonee = ef_rapporto.CoibentazioniIdonee;

                rapporto.AssenzaPerditeCombustibile = ef_rapporto.AssenzaPerditeCombustibile;

                rapporto.CodiceProgressivo = ef_rapporto.CodiceProgressivo;
                rapporto.DataInstallazione = ef_rapporto.DataInstallazione;
                rapporto.Fabbricante = ef_rapporto.Fabbricante;
                rapporto.Modello = ef_rapporto.Modello;
                rapporto.Matricola = ef_rapporto.Matricola;

                rapporto.TipologiaSistemaDistribuzione = ef_rapporto.IDTipologiaSistemaDistribuzione;
                rapporto.TipologiaContabilizzazione = ef_rapporto.IDTipologiaContabilizzazione;

                rapporto.TenutaImpiantoIdraulico = ef_rapporto.TenutaImpiantoIdraulico;
                rapporto.Contabilizzazione = ef_rapporto.Contabilizzazione;
                rapporto.Termoregolazione = ef_rapporto.Termoregolazione;
                rapporto.CorrettoFunzionamentoContabilizzazione = ef_rapporto.CorrettoFunzionamentoContabilizzazione;

                rapporto.PotenzaTermicaNominale = ef_rapporto.PotenzaTermicaNominale;
                rapporto.fClimatizzazioneInvernale = ef_rapporto.fClimatizzazioneInvernale;
                rapporto.fClimatizzazioneEstiva = ef_rapporto.fClimatizzazioneEstiva;
                rapporto.fProduzioneACS = ef_rapporto.fProduzioneACS;
                rapporto.TipologiaFluidoTermoVettore = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.SYS_TipologiaFluidoTermoVettore.IDTipologiaFluidoTermoVettore;
                rapporto.PotenzaCompatibileProgetto = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.PotenzaCompatibileProgetto;
                rapporto.AssenzaTrafilamenti = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.AssenzaTrafilamenti;
                rapporto.TemperaturaEsterna = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.TemperaturaEsterna;
                rapporto.TemperaturaMandataPrimario = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.TemperaturaMandataPrimario;
                rapporto.TemperaturaRitornoPrimario = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.TemperaturaRitornoPrimario;
                rapporto.PortataFluidoPrimario = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.PortataFluidoPrimario;
                rapporto.TemperaturaMandataSecondario = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.TemperaturaMandataSecondario;
                rapporto.TemperaturaRitornoSecondario = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.TemperaturaRitornoSecondario;
                rapporto.PotenzaTermica = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.PotenzaTermica;

                rapporto.CheckList = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseCheckList)
                {
                    rapporto.CheckList.Add(item.IDCheckList);
                }

                rapporto.Osservazioni = ef_rapporto.Osservazioni;
                rapporto.Raccomandazioni = ef_rapporto.Raccomandazioni;
                rapporto.Prescrizioni = ef_rapporto.Prescrizioni;
                rapporto.fImpiantoFunzionante = ef_rapporto.fImpiantoFunzionante;
                rapporto.DataManutenzioneConsigliata = ef_rapporto.DataManutenzioneConsigliata;
                rapporto.DataControllo = ef_rapporto.DataControllo;
                rapporto.OraArrivo = ef_rapporto.OraArrivo;
                rapporto.OraPartenza = ef_rapporto.OraPartenza;

                rapporto.TecnicoIntervento = GetTecnicoIntervento(ef_rapporto.IDSoggettoDerived);
                rapporto.GuidRapportoTecnico = ef_rapporto.GuidRapportoTecnico;

                rapporto.BolliniCalorePulito = new List<string>();
                foreach (var item in ef_rapporto.RCT_BollinoCalorePulito)
                {
                    rapporto.BolliniCalorePulito.Add(item.CodiceBollino.ToString());
                }

                rapporto.RaccomandazioniPrescrizioni = new List<POMJ_RaccomandazioniPrescrizioni>();
                foreach (var rapp in ef_rapporto.RCT_RaccomandazioniPrescrizioni)
                {
                    POMJ_RaccomandazioniPrescrizioni raccomandazionePrescrizione = new POMJ_RaccomandazioniPrescrizioni()
                    {
                        IDCampoRct = rapp.IDTipologiaRaccomandazionePrescrizioneRct,
                        IDNonConformita = (rapp.IDTipologiaRaccomandazione != null) ? (int)rapp.IDTipologiaRaccomandazione : (int)rapp.IDTipologiaPrescrizione,
                        TipoNonConformita = (rapp.IDTipologiaRaccomandazione != null) ? "Raccomandazione" : "Prescrizione"
                    };
                    rapporto.RaccomandazioniPrescrizioni.Add(raccomandazionePrescrizione);
                }
            }
            return rapporto;
        }

        public POMJ_RapportoControlloTecnico_SC GetRapportoTecnico_SC_ByGuid(string guidrapporto)
        {
            POMJ_RapportoControlloTecnico_SC rapporto = new POMJ_RapportoControlloTecnico_SC();

            var ef_rapporto = (from r in db.RCT_RapportoDiControlloTecnicoBase

                              .Include("SYS_StatoRapportoDiControllo")
                              .Include("SYS_TipologiaCombustibile")
                              .Include("SYS_TipologiaFluidoTermoVettore")
                              .Include("SYS_TipologiaSistemaDistribuzione")
                              .Include("SYS_TipologiaContabilizzazione")
                              .Include("SYS_TipologiaCombustibile")

                             .Include("RCT_RapportoDiControlloTecnicoBaseCheckList")
                             .Include("RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs")
                             .Include("RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale")
                             .Include("RCT_RaccomandazioniPrescrizioni")
                             .Include("RCT_RapportoDiControlloTecnicoCG")
                               join t in db.LIM_TargatureImpianti on r.IDTargaturaImpianto equals t.IDTargaturaImpianto
                               where r.GuidRapportoTecnico == guidrapporto
                               select r).FirstOrDefault();

            if (ef_rapporto != null)
            {
                rapporto.CodiceTargaturaImpianto = new POMJ_TargaturaImpianto()
                {
                    CodiceTargatura = ef_rapporto.LIM_TargatureImpianti.CodiceTargatura
                };

                // RCT_RapportoDiControlloTecnicoBase fields
                rapporto.PotenzaTermicaNominaleTotaleMax = ef_rapporto.PotenzaTermicaNominaleTotaleMax;
                rapporto.Indirizzo = ef_rapporto.Indirizzo;
                rapporto.Civico = ef_rapporto.Civico;
                rapporto.Palazzo = ef_rapporto.Palazzo;
                rapporto.Scala = ef_rapporto.Scala;
                rapporto.Interno = ef_rapporto.Interno;
                rapporto.StatoRapportoDiControllo = ef_rapporto.SYS_StatoRapportoDiControllo.IDStatoRapportoDiControllo;
                rapporto.TipologiaControllo = ef_rapporto.SYS_TipologiaControllo.IDTipologiaControllo;
                rapporto.TipologiaResponsabile = ef_rapporto.SYS_TipologiaResponsabile.IDTipologiaResponsabile;

                rapporto.ResponsabileImpianto = new Criter.Anagrafica.POMJ_AnagraficaSoggetti()
                {
                    TipoSoggetto = ef_rapporto.SYS_TipoSoggetto.IDTipoSoggetto,
                    Nome = ef_rapporto.NomeResponsabile,
                    Cognome = ef_rapporto.CognomeResponsabile,
                    CodiceFiscale = ef_rapporto.CodiceFiscaleResponsabile,
                    NomeAzienda = ef_rapporto.RagioneSocialeResponsabile,
                    PartitaIVA = ef_rapporto.PartitaIVAResponsabile,
                    Indirizzo = ef_rapporto.IndirizzoResponsabile,
                    Civico = ef_rapporto.Civico,
                    CodiceCatastaleComune = ef_rapporto.ComuneResponsabile,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaResponsabile.HasValue ? ef_rapporto.IDProvinciaResponsabile.Value : 0
                };

                rapporto.TerzoResponsabile = new Criter.Anagrafica.POMJ_AnagraficaSoggetti()
                {
                    NomeAzienda = ef_rapporto.RagioneSocialeTerzoResponsabile,
                    PartitaIVA = ef_rapporto.PartitaIVATerzoResponsabile,
                    Indirizzo = ef_rapporto.IndirizzoTerzoResponsabile,
                    Civico = ef_rapporto.Civico,
                    CodiceCatastaleComune = ef_rapporto.ComuneTerzoResponsabile,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaTerzoResponsabile.HasValue ? ef_rapporto.IDProvinciaTerzoResponsabile.Value : 0,

                };

                rapporto.ImpresaManutentrice = new Criter.Anagrafica.POMJ_AnagraficaSoggetti()
                {
                    NomeAzienda = ef_rapporto.RagioneSocialeImpresaManutentrice,
                    PartitaIVA = ef_rapporto.PartitaIVAImpresaManutentrice,
                    Indirizzo = ef_rapporto.IndirizzoImpresaManutentrice,
                    Civico = ef_rapporto.CivicoImpresaManutentrice,
                    CodiceCatastaleComune = ef_rapporto.ComuneImpresaManutentrice,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaImpresaManutentrice.HasValue ? ef_rapporto.IDProvinciaImpresaManutentrice.Value : 0
                };

                rapporto.fDichiarazioneConformita = ef_rapporto.fDichiarazioneConformita;
                rapporto.fLibrettoImpiantoPresente = ef_rapporto.fLibrettoImpiantoCompilato;
                rapporto.fUsoManutenzioneGeneratore = ef_rapporto.fUsoManutenzioneGeneratore;
                rapporto.fLibrettoImpiantoCompilato = ef_rapporto.fLibrettoImpiantoCompilato;

                rapporto.DurezzaAcqua = ef_rapporto.DurezzaAcqua;
                rapporto.TrattamentoRiscaldamento = ef_rapporto.TrattamentoRiscaldamento;
                rapporto.TrattamentoACS = ef_rapporto.TrattamentoACS;

                rapporto.TipoTrattamentoRiscaldamento = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale)
                {
                    rapporto.TipoTrattamentoRiscaldamento.Add(item.SYS_TipologiaTrattamentoAcqua.IDTipologiaTrattamentoAcqua);
                }


                rapporto.TipoTrattamentoACS = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs)
                {
                    rapporto.TipoTrattamentoACS.Add(item.SYS_TipologiaTrattamentoAcqua.IDTipologiaTrattamentoAcqua);
                }

                rapporto.LocaleInstallazioneIdoneo = ef_rapporto.LocaleInstallazioneIdoneo;
                rapporto.LineeElettricheIdonee = ef_rapporto.LineeElettricheIdonee;
                rapporto.StatoCoibentazioniIdonee = ef_rapporto.StatoCoibentazioniIdonee;
                rapporto.CoibentazioniIdonee = ef_rapporto.CoibentazioniIdonee;
                rapporto.AssenzaPerditeCombustibile = ef_rapporto.AssenzaPerditeCombustibile;

                rapporto.CodiceProgressivo = ef_rapporto.CodiceProgressivo;
                rapporto.DataInstallazione = ef_rapporto.DataInstallazione;
                rapporto.Fabbricante = ef_rapporto.Fabbricante;
                rapporto.Modello = ef_rapporto.Modello;
                rapporto.Matricola = ef_rapporto.Matricola;

                rapporto.TipologiaSistemaDistribuzione = ef_rapporto.IDTipologiaSistemaDistribuzione;
                rapporto.TipologiaContabilizzazione = ef_rapporto.IDTipologiaContabilizzazione;

                rapporto.TenutaImpiantoIdraulico = ef_rapporto.TenutaImpiantoIdraulico;
                rapporto.Contabilizzazione = ef_rapporto.Contabilizzazione;
                rapporto.Termoregolazione = ef_rapporto.Termoregolazione;
                rapporto.CorrettoFunzionamentoContabilizzazione = ef_rapporto.CorrettoFunzionamentoContabilizzazione;

                rapporto.PotenzaTermicaNominale = ef_rapporto.PotenzaTermicaNominale;
                rapporto.fClimatizzazioneInvernale = ef_rapporto.fClimatizzazioneInvernale;
                rapporto.fClimatizzazioneEstiva = ef_rapporto.fClimatizzazioneEstiva;
                rapporto.fProduzioneACS = ef_rapporto.fProduzioneACS;
                rapporto.TipologiaFluidoTermoVettore = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.SYS_TipologiaFluidoTermoVettore.IDTipologiaFluidoTermoVettore;
                rapporto.PotenzaCompatibileProgetto = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.PotenzaCompatibileProgetto;
                rapporto.AssenzaTrafilamenti = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.AssenzaTrafilamenti;
                rapporto.TemperaturaEsterna = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.TemperaturaEsterna;
                rapporto.TemperaturaMandataPrimario = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.TemperaturaMandataPrimario;
                rapporto.TemperaturaRitornoPrimario = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.TemperaturaRitornoPrimario;
                rapporto.PortataFluidoPrimario = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.PortataFluidoPrimario;
                rapporto.TemperaturaMandataSecondario = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.TemperaturaMandataSecondario;
                rapporto.TemperaturaRitornoSecondario = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.TemperaturaRitornoSecondario;
                rapporto.PotenzaTermica = ef_rapporto.RCT_RapportoDiControlloTecnicoSC.PotenzaTermica;

                rapporto.CheckList = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseCheckList)
                {
                    rapporto.CheckList.Add(item.IDCheckList);
                }

                rapporto.Osservazioni = ef_rapporto.Osservazioni;
                rapporto.Raccomandazioni = ef_rapporto.Raccomandazioni;
                rapporto.Prescrizioni = ef_rapporto.Prescrizioni;
                rapporto.fImpiantoFunzionante = ef_rapporto.fImpiantoFunzionante;
                rapporto.DataManutenzioneConsigliata = ef_rapporto.DataManutenzioneConsigliata;
                rapporto.DataControllo = ef_rapporto.DataControllo;
                rapporto.OraArrivo = ef_rapporto.OraArrivo;
                rapporto.OraPartenza = ef_rapporto.OraPartenza;

                rapporto.TecnicoIntervento = GetTecnicoIntervento(ef_rapporto.IDSoggettoDerived);
                rapporto.GuidRapportoTecnico = ef_rapporto.GuidRapportoTecnico;

                rapporto.BolliniCalorePulito = new List<string>();
                foreach (var item in ef_rapporto.RCT_BollinoCalorePulito)
                {
                    rapporto.BolliniCalorePulito.Add(item.CodiceBollino.ToString());
                }

                rapporto.RaccomandazioniPrescrizioni = new List<POMJ_RaccomandazioniPrescrizioni>();
                foreach (var rapp in ef_rapporto.RCT_RaccomandazioniPrescrizioni)
                {
                    POMJ_RaccomandazioniPrescrizioni raccomandazionePrescrizione = new POMJ_RaccomandazioniPrescrizioni()
                    {
                        IDCampoRct = rapp.IDTipologiaRaccomandazionePrescrizioneRct,
                        IDNonConformita = (rapp.IDTipologiaRaccomandazione != null) ? (int)rapp.IDTipologiaRaccomandazione : (int)rapp.IDTipologiaPrescrizione,
                        TipoNonConformita = (rapp.IDTipologiaRaccomandazione != null) ? "Raccomandazione" : "Prescrizione"
                    };
                    rapporto.RaccomandazioniPrescrizioni.Add(raccomandazionePrescrizione);
                }
            }
            return rapporto;
        }

        public POMJ_RapportoControlloTecnico_CG GetRapportoTecnico_CG_ByID(int idRapporto)
        {
            POMJ_RapportoControlloTecnico_CG rapporto = new POMJ_RapportoControlloTecnico_CG();

            var ef_rapporto = (from r in db.RCT_RapportoDiControlloTecnicoBase

                              .Include("SYS_StatoRapportoDiControllo")
                              .Include("SYS_TipologiaCombustibile")
                              .Include("SYS_TipologiaFluidoTermoVettore")
                              .Include("SYS_TipologiaSistemaDistribuzione")
                              .Include("SYS_TipologiaContabilizzazione")
                              .Include("SYS_TipologiaCombustibile")

                             .Include("RCT_RapportoDiControlloTecnicoBaseCheckList")
                             .Include("RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs")
                             .Include("RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale")
                             .Include("RCT_RaccomandazioniPrescrizioni")
                             .Include("RCT_RapportoDiControlloTecnicoCG")
                               join t in db.LIM_TargatureImpianti on r.IDTargaturaImpianto equals t.IDTargaturaImpianto
                               where r.IDRapportoControlloTecnico == idRapporto
                               select r).FirstOrDefault();
            if (ef_rapporto != null)
            {
                rapporto.CodiceTargaturaImpianto = new POMJ_TargaturaImpianto()
                {
                    CodiceTargatura = ef_rapporto.LIM_TargatureImpianti.CodiceTargatura
                };

                // RCT_RapportoDiControlloTecnicoBase fields
                rapporto.PotenzaTermicaNominaleTotaleMax = ef_rapporto.PotenzaTermicaNominaleTotaleMax;
                rapporto.Indirizzo = ef_rapporto.Indirizzo;
                rapporto.Civico = ef_rapporto.Civico;
                rapporto.Palazzo = ef_rapporto.Palazzo;
                rapporto.Scala = ef_rapporto.Scala;
                rapporto.Interno = ef_rapporto.Interno;
                rapporto.StatoRapportoDiControllo = ef_rapporto.SYS_StatoRapportoDiControllo.IDStatoRapportoDiControllo;
                rapporto.TipologiaControllo = ef_rapporto.SYS_TipologiaControllo.IDTipologiaControllo;
                rapporto.TipologiaResponsabile = ef_rapporto.SYS_TipologiaResponsabile.IDTipologiaResponsabile;

                rapporto.ResponsabileImpianto = new POMJ_AnagraficaSoggetti()
                {
                    TipoSoggetto = ef_rapporto.IDTipoSoggetto.Value,
                    Nome = ef_rapporto.NomeResponsabile,
                    Cognome = ef_rapporto.CognomeResponsabile,
                    CodiceFiscale = ef_rapporto.CodiceFiscaleResponsabile,
                    NomeAzienda = ef_rapporto.RagioneSocialeResponsabile,
                    PartitaIVA = ef_rapporto.PartitaIVAResponsabile,
                    Indirizzo = ef_rapporto.IndirizzoResponsabile,
                    Civico = ef_rapporto.Civico,
                    CodiceCatastaleComune = ef_rapporto.ComuneResponsabile,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaResponsabile.HasValue ? ef_rapporto.IDProvinciaResponsabile.Value : 0
                };

                rapporto.TerzoResponsabile = new POMJ_AnagraficaSoggetti()
                {
                    TipoSoggetto = ef_rapporto.IDTipoSoggetto.Value,
                    NomeAzienda = ef_rapporto.RagioneSocialeTerzoResponsabile,
                    PartitaIVA = ef_rapporto.PartitaIVATerzoResponsabile,
                    Indirizzo = ef_rapporto.IndirizzoTerzoResponsabile,
                    Civico = ef_rapporto.Civico,
                    CodiceCatastaleComune = ef_rapporto.ComuneTerzoResponsabile,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaTerzoResponsabile.HasValue ? ef_rapporto.IDProvinciaTerzoResponsabile.Value : 0,

                };

                rapporto.ImpresaManutentrice = new Criter.Anagrafica.POMJ_AnagraficaSoggetti()
                {
                    TipoSoggetto = ef_rapporto.IDTipoSoggetto.Value,
                    NomeAzienda = ef_rapporto.RagioneSocialeImpresaManutentrice,
                    PartitaIVA = ef_rapporto.PartitaIVAImpresaManutentrice,
                    Indirizzo = ef_rapporto.IndirizzoImpresaManutentrice,
                    Civico = ef_rapporto.CivicoImpresaManutentrice,
                    CodiceCatastaleComune = ef_rapporto.ComuneImpresaManutentrice,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaImpresaManutentrice.HasValue ? ef_rapporto.IDProvinciaImpresaManutentrice.Value : 0
                };

                rapporto.fDichiarazioneConformita = ef_rapporto.fDichiarazioneConformita;
                rapporto.fLibrettoImpiantoPresente = ef_rapporto.fLibrettoImpiantoCompilato;
                rapporto.fUsoManutenzioneGeneratore = ef_rapporto.fUsoManutenzioneGeneratore;
                rapporto.fLibrettoImpiantoCompilato = ef_rapporto.fLibrettoImpiantoCompilato;

                rapporto.DurezzaAcqua = ef_rapporto.DurezzaAcqua;
                rapporto.TrattamentoRiscaldamento = ef_rapporto.TrattamentoRiscaldamento;
                rapporto.TrattamentoACS = ef_rapporto.TrattamentoACS;

                rapporto.TipoTrattamentoRiscaldamento = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale)
                {
                    rapporto.TipoTrattamentoRiscaldamento.Add(item.SYS_TipologiaTrattamentoAcqua.IDTipologiaTrattamentoAcqua);
                }

                rapporto.TipoTrattamentoACS = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs)
                {
                    rapporto.TipoTrattamentoACS.Add(item.SYS_TipologiaTrattamentoAcqua.IDTipologiaTrattamentoAcqua);
                }

                rapporto.LocaleInstallazioneIdoneo = ef_rapporto.LocaleInstallazioneIdoneo;
                rapporto.DimensioniApertureAdeguate = ef_rapporto.DimensioniApertureAdeguate;
                rapporto.ApertureLibere = ef_rapporto.ApertureLibere;
                rapporto.LineeElettricheIdonee = ef_rapporto.LineeElettricheIdonee;
                rapporto.TenutaImpiantoIdraulico = ef_rapporto.TenutaImpiantoIdraulico;


                rapporto.ScarichiIdonei = ef_rapporto.ScarichiIdonei;
                rapporto.Contabilizzazione = ef_rapporto.Contabilizzazione;
                rapporto.Termoregolazione = ef_rapporto.Termoregolazione;
                rapporto.CorrettoFunzionamentoContabilizzazione = ef_rapporto.CorrettoFunzionamentoContabilizzazione;
                rapporto.AssenzaPerditeCombustibile = ef_rapporto.AssenzaPerditeCombustibile;


                rapporto.CodiceProgressivo = ef_rapporto.CodiceProgressivo;
                rapporto.DataInstallazione = ef_rapporto.DataInstallazione;
                rapporto.Fabbricante = ef_rapporto.Fabbricante;
                rapporto.Modello = ef_rapporto.Modello;
                rapporto.Matricola = ef_rapporto.Matricola;

                rapporto.TipologiaSistemaDistribuzione = ef_rapporto.IDTipologiaSistemaDistribuzione;
                rapporto.TipologiaContabilizzazione = ef_rapporto.IDTipologiaContabilizzazione;

                rapporto.PotenzaElettricaMorsetti = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.PotenzaElettricaMorsetti;
                rapporto.PotenzaAssorbitaCombustibile = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.PotenzaAssorbitaCombustibile;
                rapporto.PotenzaMassimoRecupero = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.PotenzaMassimoRecupero;
                rapporto.PotenzaByPass = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.PotenzaByPass;

                rapporto.fClimatizzazioneInvernale = ef_rapporto.fClimatizzazioneInvernale;
                rapporto.fClimatizzazioneEstiva = ef_rapporto.fClimatizzazioneEstiva;
                rapporto.fProduzioneACS = ef_rapporto.fProduzioneACS;
                rapporto.TipologiaFluidoTermoVettore = ef_rapporto.IDTipologiaFluidoTermoVettore;

                rapporto.PotenzaAiMorsetti = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.PotenzaAiMorsetti;
                rapporto.TemperaturaAriaComburente = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.TemperaturaAriaComburente;
                rapporto.TemperaturaAcquaIngresso = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.TemperaturaAcquaIngresso;
                rapporto.TemperaturaAcquauscita = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.TemperaturaAcquauscita;
                rapporto.TemperaturaAcquaMotore = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.TemperaturaAcquaMotore;
                rapporto.TemperaturaFumiValle = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.TemperaturaFumiValle;
                rapporto.EmissioneMonossido = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.EmissioneMonossido;

                rapporto.SovrafrequenzaSogliaInterv1 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaSogliaInterv1;
                rapporto.SovrafrequenzaSogliaInterv2 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaSogliaInterv2;
                rapporto.SovrafrequenzaSogliaInterv3 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaSogliaInterv3;
                rapporto.SovrafrequenzaTempoInterv1 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaTempoInterv1;
                rapporto.SovrafrequenzaTempoInterv2 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaTempoInterv2;
                rapporto.SovrafrequenzaTempoInterv3 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaTempoInterv3;
                rapporto.SottofrequenzaSogliaInterv1 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottofrequenzaSogliaInterv1;
                rapporto.SottofrequenzaSogliaInterv2 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottofrequenzaSogliaInterv2;
                rapporto.SottofrequenzaSogliaInterv3 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottofrequenzaSogliaInterv3;
                rapporto.SottofrequenzaTempoInterv1 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottofrequenzaTempoInterv1;
                rapporto.SottofrequenzaTempoInterv2 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottofrequenzaTempoInterv2;
                rapporto.SottofrequenzaTempoInterv3 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottofrequenzaTempoInterv3;
                rapporto.SovratensioneSogliaInterv1 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovratensioneSogliaInterv1;
                rapporto.SovratensioneSogliaInterv2 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovratensioneSogliaInterv2;
                rapporto.SovratensioneSogliaInterv3 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovratensioneSogliaInterv3;
                rapporto.SovratensioneTempoInterv1 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovratensioneTempoInterv1;
                rapporto.SovratensioneTempoInterv2 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovratensioneTempoInterv2;
                rapporto.SovratensioneTempoInterv3 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovratensioneTempoInterv3;
                rapporto.SottotensioneSogliaInterv1 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottotensioneSogliaInterv1;
                rapporto.SottotensioneSogliaInterv2 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottotensioneSogliaInterv2;
                rapporto.SottotensioneSogliaInterv3 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottotensioneSogliaInterv3;
                rapporto.SottotensioneTempoInterv1 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottotensioneTempoInterv1;
                rapporto.SottotensioneTempoInterv2 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottotensioneTempoInterv2;
                rapporto.SottotensioneTempoInterv3 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottotensioneTempoInterv3;

                rapporto.CheckList = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseCheckList)
                {
                    rapporto.CheckList.Add(item.IDCheckList);
                }

                rapporto.Osservazioni = ef_rapporto.Osservazioni;
                rapporto.Raccomandazioni = ef_rapporto.Raccomandazioni;
                rapporto.Prescrizioni = ef_rapporto.Prescrizioni;
                rapporto.fImpiantoFunzionante = ef_rapporto.fImpiantoFunzionante;
                rapporto.DataManutenzioneConsigliata = ef_rapporto.DataManutenzioneConsigliata;
                rapporto.DataControllo = ef_rapporto.DataControllo;
                rapporto.OraArrivo = ef_rapporto.OraArrivo;
                rapporto.OraPartenza = ef_rapporto.OraPartenza;

                rapporto.TecnicoIntervento = GetTecnicoIntervento(ef_rapporto.IDSoggettoDerived);
                rapporto.GuidRapportoTecnico = ef_rapporto.GuidRapportoTecnico;

                rapporto.BolliniCalorePulito = new List<string>();
                foreach (var item in ef_rapporto.RCT_BollinoCalorePulito)
                {
                    rapporto.BolliniCalorePulito.Add(item.CodiceBollino.ToString());
                }

                rapporto.RaccomandazioniPrescrizioni = new List<POMJ_RaccomandazioniPrescrizioni>();
                foreach (var rapp in ef_rapporto.RCT_RaccomandazioniPrescrizioni)
                {
                    POMJ_RaccomandazioniPrescrizioni raccomandazionePrescrizione = new POMJ_RaccomandazioniPrescrizioni()
                    {
                        IDCampoRct = rapp.IDTipologiaRaccomandazionePrescrizioneRct,
                        IDNonConformita = (rapp.IDTipologiaRaccomandazione != null) ? (int)rapp.IDTipologiaRaccomandazione : (int)rapp.IDTipologiaPrescrizione,
                        TipoNonConformita = (rapp.IDTipologiaRaccomandazione != null) ? "Raccomandazione" : "Prescrizione"
                    };
                    rapporto.RaccomandazioniPrescrizioni.Add(raccomandazionePrescrizione);
                }
            }
            return rapporto;
        }

        public POMJ_RapportoControlloTecnico_CG GetRapportoTecnico_CG_ByGuid(string guidrapporto)
        {
            POMJ_RapportoControlloTecnico_CG rapporto = new POMJ_RapportoControlloTecnico_CG();

            var ef_rapporto = (from r in db.RCT_RapportoDiControlloTecnicoBase

                              .Include("SYS_StatoRapportoDiControllo")
                              .Include("SYS_TipologiaCombustibile")
                              .Include("SYS_TipologiaFluidoTermoVettore")
                              .Include("SYS_TipologiaSistemaDistribuzione")
                              .Include("SYS_TipologiaContabilizzazione")
                              .Include("SYS_TipologiaCombustibile")

                              .Include("RCT_RapportoDiControlloTecnicoBaseCheckList")
                              .Include("RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs")
                              .Include("RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale")
                              .Include("RCT_RaccomandazioniPrescrizioni")
                              .Include("RCT_RapportoDiControlloTecnicoCG")
                               join t in db.LIM_TargatureImpianti on r.IDTargaturaImpianto equals t.IDTargaturaImpianto
                               where r.GuidRapportoTecnico == guidrapporto
                               select r).FirstOrDefault();
            if (ef_rapporto != null)
            {
                rapporto.CodiceTargaturaImpianto = new POMJ_TargaturaImpianto()
                {
                    CodiceTargatura = ef_rapporto.LIM_TargatureImpianti.CodiceTargatura
                };

                rapporto.PotenzaTermicaNominaleTotaleMax = ef_rapporto.PotenzaTermicaNominaleTotaleMax;
                rapporto.Indirizzo = ef_rapporto.Indirizzo;
                rapporto.Civico = ef_rapporto.Civico;
                rapporto.Palazzo = ef_rapporto.Palazzo;
                rapporto.Scala = ef_rapporto.Scala;
                rapporto.Interno = ef_rapporto.Interno;
                rapporto.StatoRapportoDiControllo = ef_rapporto.SYS_StatoRapportoDiControllo.IDStatoRapportoDiControllo;
                rapporto.TipologiaControllo = ef_rapporto.SYS_TipologiaControllo.IDTipologiaControllo;
                rapporto.TipologiaResponsabile = ef_rapporto.SYS_TipologiaResponsabile.IDTipologiaResponsabile;

                rapporto.ResponsabileImpianto = new Criter.Anagrafica.POMJ_AnagraficaSoggetti()
                {
                    TipoSoggetto = ef_rapporto.SYS_TipoSoggetto.IDTipoSoggetto,
                    Nome = ef_rapporto.NomeResponsabile,
                    Cognome = ef_rapporto.CognomeResponsabile,
                    CodiceFiscale = ef_rapporto.CodiceFiscaleResponsabile,
                    NomeAzienda = ef_rapporto.RagioneSocialeResponsabile,
                    PartitaIVA = ef_rapporto.PartitaIVAResponsabile,
                    Indirizzo = ef_rapporto.IndirizzoResponsabile,
                    Civico = ef_rapporto.Civico,
                    CodiceCatastaleComune = ef_rapporto.ComuneResponsabile,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaResponsabile.HasValue ? ef_rapporto.IDProvinciaResponsabile.Value : 0
                };

                rapporto.TerzoResponsabile = new Criter.Anagrafica.POMJ_AnagraficaSoggetti()
                {
                    NomeAzienda = ef_rapporto.RagioneSocialeTerzoResponsabile,
                    PartitaIVA = ef_rapporto.PartitaIVATerzoResponsabile,
                    Indirizzo = ef_rapporto.IndirizzoTerzoResponsabile,
                    Civico = ef_rapporto.Civico,
                    CodiceCatastaleComune = ef_rapporto.ComuneTerzoResponsabile,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaTerzoResponsabile.HasValue ? ef_rapporto.IDProvinciaTerzoResponsabile.Value : 0,

                };

                rapporto.ImpresaManutentrice = new Criter.Anagrafica.POMJ_AnagraficaSoggetti()
                {
                    NomeAzienda = ef_rapporto.RagioneSocialeImpresaManutentrice,
                    PartitaIVA = ef_rapporto.PartitaIVAImpresaManutentrice,
                    Indirizzo = ef_rapporto.IndirizzoImpresaManutentrice,
                    Civico = ef_rapporto.CivicoImpresaManutentrice,
                    CodiceCatastaleComune = ef_rapporto.ComuneImpresaManutentrice,
                    ProvinciaSedeLegale = ef_rapporto.IDProvinciaImpresaManutentrice.HasValue ? ef_rapporto.IDProvinciaImpresaManutentrice.Value : 0
                };

                rapporto.fDichiarazioneConformita = ef_rapporto.fDichiarazioneConformita;
                rapporto.fLibrettoImpiantoPresente = ef_rapporto.fLibrettoImpiantoCompilato;
                rapporto.fUsoManutenzioneGeneratore = ef_rapporto.fUsoManutenzioneGeneratore;
                rapporto.fLibrettoImpiantoCompilato = ef_rapporto.fLibrettoImpiantoCompilato;

                rapporto.DurezzaAcqua = ef_rapporto.DurezzaAcqua;
                rapporto.TrattamentoRiscaldamento = ef_rapporto.TrattamentoRiscaldamento;
                rapporto.TrattamentoACS = ef_rapporto.TrattamentoACS;

                rapporto.TipoTrattamentoRiscaldamento = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale)
                {
                    rapporto.TipoTrattamentoRiscaldamento.Add(item.SYS_TipologiaTrattamentoAcqua.IDTipologiaTrattamentoAcqua);
                }

                rapporto.TipoTrattamentoACS = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs)
                {
                    rapporto.TipoTrattamentoACS.Add(item.SYS_TipologiaTrattamentoAcqua.IDTipologiaTrattamentoAcqua);
                }

                rapporto.LocaleInstallazioneIdoneo = ef_rapporto.LocaleInstallazioneIdoneo;
                rapporto.DimensioniApertureAdeguate = ef_rapporto.DimensioniApertureAdeguate;
                rapporto.ApertureLibere = ef_rapporto.ApertureLibere;
                rapporto.LineeElettricheIdonee = ef_rapporto.LineeElettricheIdonee;
                rapporto.TenutaImpiantoIdraulico = ef_rapporto.TenutaImpiantoIdraulico;


                rapporto.ScarichiIdonei = ef_rapporto.ScarichiIdonei;
                rapporto.Contabilizzazione = ef_rapporto.Contabilizzazione;
                rapporto.Termoregolazione = ef_rapporto.Termoregolazione;
                rapporto.CorrettoFunzionamentoContabilizzazione = ef_rapporto.CorrettoFunzionamentoContabilizzazione;
                rapporto.AssenzaPerditeCombustibile = ef_rapporto.AssenzaPerditeCombustibile;


                rapporto.CodiceProgressivo = ef_rapporto.CodiceProgressivo;
                rapporto.DataInstallazione = ef_rapporto.DataInstallazione;
                rapporto.Fabbricante = ef_rapporto.Fabbricante;
                rapporto.Modello = ef_rapporto.Modello;
                rapporto.Matricola = ef_rapporto.Matricola;

                rapporto.TipologiaSistemaDistribuzione = ef_rapporto.IDTipologiaSistemaDistribuzione;
                rapporto.TipologiaContabilizzazione = ef_rapporto.IDTipologiaContabilizzazione;

                rapporto.PotenzaElettricaMorsetti = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.PotenzaElettricaMorsetti;
                rapporto.PotenzaAssorbitaCombustibile = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.PotenzaAssorbitaCombustibile;
                rapporto.PotenzaMassimoRecupero = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.PotenzaMassimoRecupero;
                rapporto.PotenzaByPass = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.PotenzaByPass;

                rapporto.fClimatizzazioneInvernale = ef_rapporto.fClimatizzazioneInvernale;
                rapporto.fClimatizzazioneEstiva = ef_rapporto.fClimatizzazioneEstiva;
                rapporto.fProduzioneACS = ef_rapporto.fProduzioneACS;
                rapporto.TipologiaFluidoTermoVettore = ef_rapporto.IDTipologiaFluidoTermoVettore;

                rapporto.PotenzaAiMorsetti = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.PotenzaAiMorsetti;
                rapporto.TemperaturaAriaComburente = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.TemperaturaAriaComburente;
                rapporto.TemperaturaAcquaIngresso = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.TemperaturaAcquaIngresso;
                rapporto.TemperaturaAcquauscita = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.TemperaturaAcquauscita;
                rapporto.TemperaturaAcquaMotore = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.TemperaturaAcquaMotore;
                rapporto.TemperaturaFumiValle = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.TemperaturaFumiValle;
                rapporto.EmissioneMonossido = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.EmissioneMonossido;

                rapporto.SovrafrequenzaSogliaInterv1 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaSogliaInterv1;
                rapporto.SovrafrequenzaSogliaInterv2 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaSogliaInterv2;
                rapporto.SovrafrequenzaSogliaInterv3 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaSogliaInterv3;
                rapporto.SovrafrequenzaTempoInterv1 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaTempoInterv1;
                rapporto.SovrafrequenzaTempoInterv2 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaTempoInterv2;
                rapporto.SovrafrequenzaTempoInterv3 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovrafrequenzaTempoInterv3;
                rapporto.SottofrequenzaSogliaInterv1 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottofrequenzaSogliaInterv1;
                rapporto.SottofrequenzaSogliaInterv2 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottofrequenzaSogliaInterv2;
                rapporto.SottofrequenzaSogliaInterv3 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottofrequenzaSogliaInterv3;
                rapporto.SottofrequenzaTempoInterv1 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottofrequenzaTempoInterv1;
                rapporto.SottofrequenzaTempoInterv2 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottofrequenzaTempoInterv2;
                rapporto.SottofrequenzaTempoInterv3 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottofrequenzaTempoInterv3;
                rapporto.SovratensioneSogliaInterv1 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovratensioneSogliaInterv1;
                rapporto.SovratensioneSogliaInterv2 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovratensioneSogliaInterv2;
                rapporto.SovratensioneSogliaInterv3 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovratensioneSogliaInterv3;
                rapporto.SovratensioneTempoInterv1 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovratensioneTempoInterv1;
                rapporto.SovratensioneTempoInterv2 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovratensioneTempoInterv2;
                rapporto.SovratensioneTempoInterv3 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SovratensioneTempoInterv3;
                rapporto.SottotensioneSogliaInterv1 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottotensioneSogliaInterv1;
                rapporto.SottotensioneSogliaInterv2 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottotensioneSogliaInterv2;
                rapporto.SottotensioneSogliaInterv3 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottotensioneSogliaInterv3;
                rapporto.SottotensioneTempoInterv1 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottotensioneTempoInterv1;
                rapporto.SottotensioneTempoInterv2 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottotensioneTempoInterv2;
                rapporto.SottotensioneTempoInterv3 = ef_rapporto.RCT_RapportoDiControlloTecnicoCG.SottotensioneTempoInterv3;

                rapporto.CheckList = new List<int>();
                foreach (var item in ef_rapporto.RCT_RapportoDiControlloTecnicoBaseCheckList)
                {
                    rapporto.CheckList.Add(item.IDCheckList);
                }

                rapporto.Osservazioni = ef_rapporto.Osservazioni;
                rapporto.Raccomandazioni = ef_rapporto.Raccomandazioni;
                rapporto.Prescrizioni = ef_rapporto.Prescrizioni;
                rapporto.fImpiantoFunzionante = ef_rapporto.fImpiantoFunzionante;
                rapporto.DataManutenzioneConsigliata = ef_rapporto.DataManutenzioneConsigliata;
                rapporto.DataControllo = ef_rapporto.DataControllo;
                rapporto.OraArrivo = ef_rapporto.OraArrivo;
                rapporto.OraPartenza = ef_rapporto.OraPartenza;

                rapporto.TecnicoIntervento = GetTecnicoIntervento(ef_rapporto.IDSoggettoDerived);
                rapporto.GuidRapportoTecnico = ef_rapporto.GuidRapportoTecnico;

                rapporto.BolliniCalorePulito = new List<string>();
                foreach (var item in ef_rapporto.RCT_BollinoCalorePulito)
                {
                    rapporto.BolliniCalorePulito.Add(item.CodiceBollino.ToString());
                }

                rapporto.RaccomandazioniPrescrizioni = new List<POMJ_RaccomandazioniPrescrizioni>();
                foreach (var rapp in ef_rapporto.RCT_RaccomandazioniPrescrizioni)
                {
                    POMJ_RaccomandazioniPrescrizioni raccomandazionePrescrizione = new POMJ_RaccomandazioniPrescrizioni()
                    {
                        IDCampoRct = rapp.IDTipologiaRaccomandazionePrescrizioneRct,
                        IDNonConformita = (rapp.IDTipologiaRaccomandazione != null) ? (int)rapp.IDTipologiaRaccomandazione : (int)rapp.IDTipologiaPrescrizione,
                        TipoNonConformita = (rapp.IDTipologiaRaccomandazione != null) ? "Raccomandazione" : "Prescrizione"
                    };
                    rapporto.RaccomandazioniPrescrizioni.Add(raccomandazionePrescrizione);
                }
            }
            return rapporto;
        }

        #region LookUp

        public List<object> GetLookUp(string SysTableName, bool soloAttivi)
        {
            List<object> lookup = new List<object>();

            switch (SysTableName)
            {
                #region MyRegion
                case "SYS_AbilitazioneSoggetto":
                    var resSYS_AbilitazioneSoggetto = (from a in db.SYS_AbilitazioneSoggetto
                                                       select new
                                                       {
                                                           ID = a.IDAbilitazioneSoggetto,
                                                           Descrizione = a.AbilitazioneSoggetto,
                                                           fAttivo = a.fAttivo
                                                       });

                    if (soloAttivi)
                    {
                        resSYS_AbilitazioneSoggetto = resSYS_AbilitazioneSoggetto.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_AbilitazioneSoggetto.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_AbilitazioneSoggetto
                        {
                            IDAbilitazioneSoggetto = item.ID,
                            AbilitazioneSoggetto = item.Descrizione,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_CausaliRaccomandate":
                    var resSYS_CausaliRaccomandate = (from a in db.SYS_CausaliRaccomandate
                                                      select new
                                                      {
                                                          ID = a.IDCausale,
                                                          Descrizione = a.Causale,
                                                          fAttivo = a.fAttivo
                                                      });

                    if (soloAttivi)
                    {
                        resSYS_CausaliRaccomandate = resSYS_CausaliRaccomandate.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_CausaliRaccomandate.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_CausaliRaccomandate
                        {
                            IDCausale = item.ID,
                            Causale = item.Descrizione,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_ClassificazioneImpianto":
                    var resSYS_ClassificazioneImpianto = (from a in db.SYS_ClassificazioneImpianto
                                                          select new
                                                          {
                                                              ID = a.IDClassificazioneImpianto,
                                                              Descrizione = a.ClassificazioneImpianto,
                                                              fAttivo = a.fAttivo
                                                          });

                    if (soloAttivi)
                    {
                        resSYS_ClassificazioneImpianto = resSYS_ClassificazioneImpianto.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_ClassificazioneImpianto.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_ClassificazioneImpianto
                        {
                            IDClassificazioneImpianto = item.ID,
                            ClassificazioneImpianto = item.Descrizione,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_CodiciCatastali":
                    var resSYS_CodiciCatastali = (from a in db.SYS_CodiciCatastali
                                                  select new
                                                  {
                                                      ID = a.IDCodiceCatastale,
                                                      Cap = a.Cap,
                                                      CodiceCatastale = a.CodiceCatastale,
                                                      Comune = a.Comune,
                                                      EmailPec = a.EmailPec,
                                                      IDProvincia = a.IDProvincia,
                                                      ZonaClimatica = a.ZonaClimatica,
                                                      GradiGiorno = a.GradiGiorno,
                                                      fAttivo = a.fAttivo
                                                  });

                    if (soloAttivi)
                    {
                        resSYS_CodiciCatastali = resSYS_CodiciCatastali.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_CodiciCatastali.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_CodiciCatastali
                        {
                            IDCodiceCatastale = item.ID,
                            CodiceCatastale = item.CodiceCatastale,
                            Cap = item.Cap,
                            Comune = item.Comune,
                            EmailPec = item.EmailPec,
                            IDProvincia = item.IDProvincia,
                            ZonaClimatica = item.ZonaClimatica,
                            GradiGiorno = item.GradiGiorno,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_CodiciCatastaliSezioni":
                    var resSYS_CodiciCatastaliSezioni = (from a in db.SYS_CodiciCatastaliSezioni
                                                         select new
                                                         {
                                                             ID = a.IDCodiceCatastaleSezione,
                                                             IDCodiceCatastale = a.IDCodiceCatastale,
                                                             Sezione = a.Sezione,
                                                             CodiceSezione = a.CodiceSezione,
                                                             fAttivo = a.fAttivo
                                                         });

                    if (soloAttivi)
                    {
                        resSYS_CodiciCatastaliSezioni = resSYS_CodiciCatastaliSezioni.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_CodiciCatastaliSezioni.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_CodiciCatastaliSezioni
                        {
                            IDCodiceCatastaleSezione = item.ID,
                            IDCodiceCatastale = item.IDCodiceCatastale,
                            Sezione = item.Sezione,
                            CodiceSezione = item.CodiceSezione,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_DestinazioneUso":
                    var resSYS_DestinazioneUso = (from a in db.SYS_DestinazioneUso
                                                  select new
                                                  {
                                                      ID = a.IDDestinazioneUso,
                                                      DestinazioneUso = a.DestinazioneUso,
                                                      CodiceDestinazioneUso = a.CodiceDestinazioneUso,
                                                      fAttivo = a.fAttivo
                                                  });

                    if (soloAttivi)
                    {
                        resSYS_DestinazioneUso = resSYS_DestinazioneUso.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_DestinazioneUso.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_DestinazioneUso
                        {
                            IDDestinazioneUso = item.ID,
                            DestinazioneUso = item.DestinazioneUso,
                            CodiceDestinazioneUso = item.CodiceDestinazioneUso,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_FasceContributive":
                    var resSYS_FasceContributive = (from a in db.SYS_FasceContributive
                                                    select new
                                                    {
                                                        ID = a.IDFasciaContributiva,
                                                        IDTipologiaRCT = a.IDTipologiaRCT,
                                                        NumeroBollini = a.NumeroBollini,
                                                        PotenzaMassima = a.PotenzaMassima,
                                                        fAttivo = a.fAttivo
                                                    });

                    if (soloAttivi)
                    {
                        resSYS_FasceContributive = resSYS_FasceContributive.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_FasceContributive.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_FasceContributive
                        {
                            IDFasciaContributiva = item.ID,
                            IDTipologiaRCT = item.IDTipologiaRCT,
                            NumeroBollini = item.NumeroBollini,
                            PotenzaMassima = item.PotenzaMassima,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_FluidoLatoUtenze":
                    var resSYS_FluidoLatoUtenze = (from a in db.SYS_FluidoLatoUtenze
                                                   select new
                                                   {
                                                       ID = a.IDFluidoLatoUtenze,
                                                       FluidoLatoUtenze = a.FluidoLatoUtenze,
                                                       fAttivo = a.fAttivo
                                                   });

                    if (soloAttivi)
                    {
                        resSYS_FluidoLatoUtenze = resSYS_FluidoLatoUtenze.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_FluidoLatoUtenze.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_FluidoLatoUtenze
                        {
                            IDFluidoLatoUtenze = item.ID,
                            FluidoLatoUtenze = item.FluidoLatoUtenze,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_FormeGiuridiche":
                    var resSYS_FormeGiuridiche = (from a in db.SYS_FormeGiuridiche
                                                  select new
                                                  {
                                                      ID = a.IDFormaGiuridica,
                                                      FormaGiuridica = a.FormaGiuridica,
                                                      IDTipoSoggetto = a.IDTipoSoggetto,
                                                      fAttivo = a.fAttivo
                                                  });

                    if (soloAttivi)
                    {
                        resSYS_FormeGiuridiche = resSYS_FormeGiuridiche.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_FormeGiuridiche.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_FormeGiuridiche
                        {
                            IDFormaGiuridica = item.ID,
                            FormaGiuridica = item.FormaGiuridica,
                            IDTipoSoggetto = item.IDTipoSoggetto,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_FunzioniSoggetti":
                    var resSYS_FunzioniSoggetti = (from a in db.SYS_FunzioniSoggetti
                                                   select new
                                                   {
                                                       ID = a.IDFunzioneSoggetto,
                                                       FunzioneSoggetto = a.FunzioneSoggetto,
                                                       IDTipoSoggetto = a.IDTipoSoggetto,
                                                       fAttivo = a.fAttivo
                                                   });

                    if (soloAttivi)
                    {
                        resSYS_FunzioniSoggetti = resSYS_FunzioniSoggetti.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_FunzioniSoggetti.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_FunzioniSoggetti
                        {
                            IDFunzioneSoggetto = item.ID,
                            FunzioneSoggetto = item.FunzioneSoggetto,
                            IDTipoSoggetto = item.IDTipoSoggetto,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_ModalitaInstallazioneRecuperatoriCalore":
                    var resSYS_ModalitaInstallazioneRecuperatoriCalore = (from a in db.SYS_ModalitaInstallazioneRecuperatoriCalore
                                                                          select new
                                                                          {
                                                                              ID = a.IDModalitaInstallazioneRecuperatoriCalore,
                                                                              ModalitaInstallazione = a.ModalitaInstallazione,
                                                                              fAttivo = a.fAttivo
                                                                          });

                    if (soloAttivi)
                    {
                        resSYS_ModalitaInstallazioneRecuperatoriCalore = resSYS_ModalitaInstallazioneRecuperatoriCalore.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_ModalitaInstallazioneRecuperatoriCalore.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_ModalitaInstallazioneRecuperatoriCalore
                        {
                            IDModalitaInstallazioneRecuperatoriCalore = item.ID,
                            ModalitaInstallazione = item.ModalitaInstallazione,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_Paesi":
                    var resSYS_Paesi = (from a in db.SYS_Paesi
                                        select new
                                        {
                                            ID = a.IDPaese,
                                            Paese = a.Paese,
                                            fAttivo = a.fAttivo
                                        });

                    if (soloAttivi)
                    {
                        resSYS_Paesi = resSYS_Paesi.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_Paesi.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_Paesi
                        {
                            IDPaese = item.ID,
                            Paese = item.Paese,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_Province":
                    var resSYS_Province = (from a in db.SYS_Province
                                           select new
                                           {
                                               ID = a.IDProvincia,
                                               IDRegione = a.IDRegione,
                                               Provincia = a.Provincia,
                                               SiglaProvincia = a.SiglaProvincia,
                                               fAttivo = a.fAttivo
                                           });

                    if (soloAttivi)
                    {
                        resSYS_Province = resSYS_Province.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_Province.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_Province
                        {
                            IDProvincia = item.ID,
                            IDRegione = item.IDRegione,
                            Provincia = item.Provincia,
                            SiglaProvincia = item.SiglaProvincia,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_RCTCheckList":
                    var resSYS_RCTCheckList = (from a in db.SYS_RCTCheckList
                                               select new
                                               {
                                                   ID = a.IDCheckList,
                                                   TestoCheckList = a.TestoCheckList,
                                                   fAttivo = a.fAttivo
                                               });

                    if (soloAttivi)
                    {
                        resSYS_RCTCheckList = resSYS_RCTCheckList.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_RCTCheckList.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_RCTCheckList
                        {
                            IDCheckList = item.ID,
                            TestoCheckList = item.TestoCheckList,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_RCTTipologiaCheckList":
                    var resSYS_RCTTipologiaCheckList = (from a in db.SYS_RCTTipologiaCheckList
                                                        select new
                                                        {
                                                            ID = a.IDTipologieCheckListRCT,
                                                            IDCheckList = a.IDCheckList,
                                                            IDTipologiaRCT = a.IDTipologiaRCT
                                                        });

                    foreach (var item in resSYS_RCTTipologiaCheckList.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_RCTTipologiaCheckList
                        {
                            IDTipologieCheckListRCT = item.ID,
                            IDCheckList = item.IDCheckList,
                            IDTipologiaRCT = item.IDTipologiaRCT
                        });
                    }
                    break;
                case "SYS_RCTTipologiaPrescrizione":
                    var resSYS_RCTTipologiaPrescrizione = (from a in db.SYS_RCTTipologiaPrescrizione
                                                           select new
                                                           {
                                                               ID = a.IDTipologiaPrescrizione,
                                                               IDTipologiaRaccomandazionePrescrizioneRct = a.IDTipologiaRaccomandazionePrescrizioneRct,
                                                               Prescrizione = a.Prescrizione,
                                                               fAttivo = a.fAttivo

                                                           });

                    if (soloAttivi)
                    {
                        resSYS_RCTTipologiaPrescrizione = resSYS_RCTTipologiaPrescrizione.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_RCTTipologiaPrescrizione.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_RCTTipologiaPrescrizione
                        {
                            IDTipologiaPrescrizione = item.ID,
                            IDTipologiaRaccomandazionePrescrizioneRct = item.IDTipologiaRaccomandazionePrescrizioneRct,
                            Prescrizione = item.Prescrizione,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_RCTTipologiaRaccomandazione":
                    var resSYS_RCTTipologiaRaccomandazione = (from a in db.SYS_RCTTipologiaRaccomandazione
                                                              select new
                                                              {
                                                                  ID = a.IDTipologiaRaccomandazione,
                                                                  IDTipologiaRaccomandazionePrescrizioneRct = a.IDTipologiaRaccomandazionePrescrizioneRct,
                                                                  Raccomandazione = a.Raccomandazione,
                                                                  fAttivo = a.fAttivo

                                                              });

                    if (soloAttivi)
                    {
                        resSYS_RCTTipologiaRaccomandazione = resSYS_RCTTipologiaRaccomandazione.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_RCTTipologiaRaccomandazione.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_RCTTipologiaRaccomandazione
                        {
                            IDTipologiaRaccomandazione = item.ID,
                            IDTipologiaRaccomandazionePrescrizioneRct = item.IDTipologiaRaccomandazionePrescrizioneRct,
                            Raccomandazione = item.Raccomandazione,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_Regioni":
                    var resSYS_Regioni = (from a in db.SYS_Regioni
                                          select new
                                          {
                                              ID = a.IDRegione,
                                              Descrizione = a.Regione,
                                              fAttivo = a.fAttivo
                                          });

                    if (soloAttivi)
                    {
                        resSYS_Regioni = resSYS_Regioni.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_Regioni.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_Regioni
                        {
                            IDRegione = item.ID,
                            Regione = item.Descrizione,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_RuoloSoggetto":
                    var resSYS_RuoloSoggetto = (from a in db.SYS_RuoloSoggetto
                                                select new
                                                {
                                                    ID = a.IDRuoloSoggetto,
                                                    RuoloSoggetto = a.RuoloSoggetto,
                                                    fAttivo = a.fAttivo
                                                });

                    if (soloAttivi)
                    {
                        resSYS_RuoloSoggetto = resSYS_RuoloSoggetto.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_RuoloSoggetto.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_RuoloSoggetto
                        {
                            IDRuoloSoggetto = item.ID,
                            RuoloSoggetto = item.RuoloSoggetto,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_SorgenteLatoEsterno":
                    var resSYS_SorgenteLatoEsterno = (from a in db.SYS_SorgenteLatoEsterno
                                                      select new
                                                      {
                                                          ID = a.IDSorgenteLatoEsterno,
                                                          SorgenteLatoEsterno = a.SorgenteLatoEsterno,
                                                          fAttivo = a.fAttivo
                                                      });

                    if (soloAttivi)
                    {
                        resSYS_SorgenteLatoEsterno = resSYS_SorgenteLatoEsterno.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_SorgenteLatoEsterno.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_SorgenteLatoEsterno
                        {
                            IDSorgenteLatoEsterno = item.ID,
                            SorgenteLatoEsterno = item.SorgenteLatoEsterno,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_StatoLibrettoImpianto":
                    var resSYS_StatoLibrettoImpianto = (from a in db.SYS_StatoLibrettoImpianto
                                                        select new
                                                        {
                                                            ID = a.IDStatoLibrettoImpianto,
                                                            StatoLibrettoImpianto = a.StatoLibrettoImpianto,
                                                            fAttivo = a.fAttivo
                                                        });

                    if (soloAttivi)
                    {
                        resSYS_StatoLibrettoImpianto = resSYS_StatoLibrettoImpianto.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_StatoLibrettoImpianto.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_StatoLibrettoImpianto
                        {
                            IDStatoLibrettoImpianto = item.ID,
                            StatoLibrettoImpianto = item.StatoLibrettoImpianto,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_StatoRapportoDiControllo":
                    var resSYS_StatoRapportoDiControllo = (from a in db.SYS_StatoRapportoDiControllo
                                                           select new
                                                           {
                                                               ID = a.IDStatoRapportoDiControllo,
                                                               StatoRapportoDiControllo = a.StatoRapportoDiControllo,
                                                               fAttivo = a.fAttivo
                                                           });

                    if (soloAttivi)
                    {
                        resSYS_StatoRapportoDiControllo = resSYS_StatoRapportoDiControllo.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_StatoRapportoDiControllo.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_StatoRapportoDiControllo
                        {
                            IDStatoRapportoDiControllo = item.ID,
                            StatoRapportoDiControllo = item.StatoRapportoDiControllo,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaAcquaAlimento":
                    var resSYS_TipologiaAcquaAlimento = (from a in db.SYS_TipologiaAcquaAlimento
                                                         select new
                                                         {
                                                             ID = a.IDTipologiaAcquaAlimento,
                                                             TipologiaAcquaAlimento = a.TipologiaAcquaAlimento,
                                                             fAttivo = a.fAttivo
                                                         });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaAcquaAlimento = resSYS_TipologiaAcquaAlimento.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaAcquaAlimento.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaAcquaAlimento
                        {
                            IDTipologiaAcquaAlimento = item.ID,
                            TipologiaAcquaAlimento = item.TipologiaAcquaAlimento,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaAddolcimentoAcqua":
                    var resSYS_TipologiaAddolcimentoAcqua = (from a in db.SYS_TipologiaAddolcimentoAcqua
                                                             select new
                                                             {
                                                                 ID = a.IDTipologiaAddolcimentoAcqua,
                                                                 TipologiaAddolcimentoAcqua = a.TipologiaAddolcimentoAcqua,
                                                                 fAttivo = a.fAttivo
                                                             });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaAddolcimentoAcqua = resSYS_TipologiaAddolcimentoAcqua.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaAddolcimentoAcqua.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaAddolcimentoAcqua
                        {
                            IDTipologiaAddolcimentoAcqua = item.ID,
                            TipologiaAddolcimentoAcqua = item.TipologiaAddolcimentoAcqua,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaCircuitoRaffreddamento":
                    var resSYS_TipologiaCircuitoRaffreddamento = (from a in db.SYS_TipologiaCircuitoRaffreddamento
                                                                  select new
                                                                  {
                                                                      ID = a.IDTipologiaCircuitoRaffreddamento,
                                                                      TipologiaCircuitoRaffreddamento = a.TipologiaCircuitoRaffreddamento,
                                                                      fAttivo = a.fAttivo
                                                                  });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaCircuitoRaffreddamento = resSYS_TipologiaCircuitoRaffreddamento.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaCircuitoRaffreddamento.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaCircuitoRaffreddamento
                        {
                            IDTipologiaCircuitoRaffreddamento = item.ID,
                            TipologiaCircuitoRaffreddamento = item.TipologiaCircuitoRaffreddamento,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaCogeneratore":
                    var resSYS_TipologiaCogeneratore = (from a in db.SYS_TipologiaCogeneratore
                                                        select new
                                                        {
                                                            ID = a.IDTipologiaCogeneratore,
                                                            TipologiaCogeneratore = a.TipologiaCogeneratore,
                                                            fAttivo = a.fAttivo
                                                        });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaCogeneratore = resSYS_TipologiaCogeneratore.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaCogeneratore.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaCogeneratore
                        {
                            IDTipologiaCogeneratore = item.ID,
                            TipologiaCogeneratore = item.TipologiaCogeneratore,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaCombustibile":
                    var resSYS_TipologiaCombustibile = (from a in db.SYS_TipologiaCombustibile
                                                        select new
                                                        {
                                                            ID = a.IDTipologiaCombustibile,
                                                            TipologiaCombustibile = a.TipologiaCombustibile,
                                                            Biomassa = a.Biomassa,
                                                            Gas = a.Gas,
                                                            LimiteFisicoCO2 = a.LimiteFisicoCO2,
                                                            Liquido = a.Liquido,
                                                            fAttivo = a.fAttivo
                                                        });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaCombustibile = resSYS_TipologiaCombustibile.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaCombustibile.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaCombustibile
                        {
                            IDTipologiaCombustibile = item.ID,
                            TipologiaCombustibile = item.TipologiaCombustibile,
                            Biomassa = item.Biomassa,
                            Gas = item.Gas,
                            LimiteFisicoCO2 = item.LimiteFisicoCO2,
                            Liquido = item.Liquido,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaCondizionamentoChimico":
                    var resSYS_TipologiaCondizionamentoChimico = (from a in db.SYS_TipologiaCondizionamentoChimico
                                                                  select new
                                                                  {
                                                                      ID = a.IDTipologiaCondizionamentoChimico,
                                                                      TipologiaCondizionamentoChimico = a.TipologiaCondizionamentoChimico,
                                                                      fAttivo = a.fAttivo
                                                                  });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaCondizionamentoChimico = resSYS_TipologiaCondizionamentoChimico.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaCondizionamentoChimico.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaCondizionamentoChimico
                        {
                            IDTipologiaCondizionamentoChimico = item.ID,
                            TipologiaCondizionamentoChimico = item.TipologiaCondizionamentoChimico,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaContabilizzazione":
                    var resSYS_TipologiaContabilizzazione = (from a in db.SYS_TipologiaContabilizzazione
                                                             select new
                                                             {
                                                                 ID = a.IDTipologiaContabilizzazione,
                                                                 TipologiaContabilizzazione = a.TipologiaContabilizzazione,
                                                                 fAttivo = a.fAttivo
                                                             });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaContabilizzazione = resSYS_TipologiaContabilizzazione.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaContabilizzazione.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaContabilizzazione
                        {
                            IDTipologiaContabilizzazione = item.ID,
                            TipologiaContabilizzazione = item.TipologiaContabilizzazione,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaControllo":
                    var resSYS_TipologiaControllo = (from a in db.SYS_TipologiaControllo
                                                     select new
                                                     {
                                                         ID = a.IDTipologiaControllo,
                                                         TipologiaControllo = a.TipologiaControllo,
                                                         fAttivo = a.fAttivo
                                                     });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaControllo = resSYS_TipologiaControllo.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaControllo.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaControllo
                        {
                            IDTipologiaControllo = item.ID,
                            TipologiaControllo = item.TipologiaControllo,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaDistributori":
                    var resSYS_TipologiaDistributori = (from a in db.SYS_TipologiaDistributori
                                                        select new
                                                        {
                                                            ID = a.IDDistributore,
                                                            IDCodiceCatastale = a.IDCodiceCatastale,
                                                            Distributore = a.Distributore,
                                                            EmailPec = a.EmailPec,
                                                            Indirizzo = a.Indirizzo,
                                                            fAttivo = a.fAttivo
                                                        });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaDistributori = resSYS_TipologiaDistributori.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaDistributori.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaDistributori
                        {
                            IDDistributore = item.ID,
                            IDCodiceCatastale = item.IDCodiceCatastale,
                            Distributore = item.Distributore,
                            EmailPec = item.EmailPec,
                            Indirizzo = item.Indirizzo,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaDistributoriCombustibile":
                    var resSYS_TipologiaDistributoriCombustibile = (from a in db.SYS_TipologiaDistributoriCombustibile
                                                                    select new
                                                                    {
                                                                        ID = a.IDTipologiaDistributoriCombustibile,
                                                                        TipologiaDistributoriCombustibile = a.TipologiaDistributoriCombustibile,
                                                                        fAttivo = a.fAttivo
                                                                    });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaDistributoriCombustibile = resSYS_TipologiaDistributoriCombustibile.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaDistributoriCombustibile.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaDistributoriCombustibile
                        {
                            IDTipologiaDistributoriCombustibile = item.ID,
                            TipologiaDistributoriCombustibile = item.TipologiaDistributoriCombustibile,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaFiltrazione":
                    var resSYS_TipologiaFiltrazione = (from a in db.SYS_TipologiaFiltrazione
                                                       select new
                                                       {
                                                           ID = a.IDTipologiaFiltrazione,
                                                           TipologiaFiltrazione = a.TipologiaFiltrazione,
                                                           fAttivo = a.fAttivo
                                                       });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaFiltrazione = resSYS_TipologiaFiltrazione.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaFiltrazione.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaFiltrazione
                        {
                            IDTipologiaFiltrazione = item.ID,
                            TipologiaFiltrazione = item.TipologiaFiltrazione,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaFluidoTermoVettore":
                    var resSYS_TipologiaFluidoTermoVettore = (from a in db.SYS_TipologiaFluidoTermoVettore
                                                              select new
                                                              {
                                                                  ID = a.IDTipologiaFluidoTermoVettore,
                                                                  TipologiaFluidoTermoVettore = a.TipologiaFluidoTermoVettore,
                                                                  fAttivo = a.fAttivo
                                                              });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaFluidoTermoVettore = resSYS_TipologiaFluidoTermoVettore.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaFluidoTermoVettore.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaFluidoTermoVettore
                        {
                            IDTipologiaFluidoTermoVettore = item.ID,
                            TipologiaFluidoTermoVettore = item.TipologiaFluidoTermoVettore,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaFluidoVettore":
                    var resSYS_TipologiaFluidoVettore = (from a in db.SYS_TipologiaFluidoVettore
                                                         select new
                                                         {
                                                             ID = a.IDTipologiaFluidoVettore,
                                                             TipologiaFluidoVettore = a.TipologiaFluidoVettore,
                                                             fAttivo = a.fAttivo
                                                         });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaFluidoVettore = resSYS_TipologiaFluidoVettore.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaFluidoVettore.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaFluidoVettore
                        {
                            IDTipologiaFluidoVettore = item.ID,
                            TipologiaFluidoVettore = item.TipologiaFluidoVettore,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaGeneratori":
                    var resSYS_TipologiaGeneratori = (from a in db.SYS_TipologiaGeneratori
                                                      select new
                                                      {
                                                          ID = a.IDTipologiaGeneratori,
                                                          TipologiaGeneratori = a.TipologiaGeneratori,
                                                          fAttivo = a.fAttivo
                                                      });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaGeneratori = resSYS_TipologiaGeneratori.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaGeneratori.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaGeneratori
                        {
                            IDTipologiaGeneratori = item.ID,
                            TipologiaGeneratori = item.TipologiaGeneratori,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaGeneratoriTermici":
                    var resSYS_TipologiaGeneratoriTermici = (from a in db.SYS_TipologiaGeneratoriTermici
                                                             select new
                                                             {
                                                                 ID = a.IdTipologiaGeneratoriTermici,
                                                                 Descrizione = a.Descrizione,
                                                                 fAttivo = a.fAttivo
                                                             });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaGeneratoriTermici = resSYS_TipologiaGeneratoriTermici.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaGeneratoriTermici.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaGeneratoriTermici
                        {
                            IdTipologiaGeneratoriTermici = item.ID,
                            Descrizione = item.Descrizione,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaGruppiTermici":
                    var resSYS_TipologiaGruppiTermici = (from a in db.SYS_TipologiaGruppiTermici
                                                         select new
                                                         {
                                                             ID = a.IDTipologiaGruppiTermici,
                                                             TipologiaGruppiTermici = a.TipologiaGruppiTermici,
                                                             fAttivo = a.fAttivo
                                                         });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaGruppiTermici = resSYS_TipologiaGruppiTermici.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaGruppiTermici.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaGruppiTermici
                        {
                            IDTipologiaGruppiTermici = item.ID,
                            TipologiaGruppiTermici = item.TipologiaGruppiTermici,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaImpiantiVMC":
                    var resSYS_TipologiaImpiantiVMC = (from a in db.SYS_TipologiaImpiantiVMC
                                                       select new
                                                       {
                                                           ID = a.IDTipologiaImpiantiVMC,
                                                           TipologiaImpianto = a.TipologiaImpianto,
                                                           fAttivo = a.fAttivo
                                                       });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaImpiantiVMC = resSYS_TipologiaImpiantiVMC.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaImpiantiVMC.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaImpiantiVMC
                        {
                            IDTipologiaImpiantiVMC = item.ID,
                            TipologiaImpianto = item.TipologiaImpianto,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaIntervento":
                    var resSYS_TipologiaIntervento = (from a in db.SYS_TipologiaIntervento
                                                      select new
                                                      {
                                                          ID = a.IDTipologiaIntervento,
                                                          TipologiaIntervento = a.TipologiaIntervento,
                                                          fAttivo = a.fAttivo
                                                      });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaIntervento = resSYS_TipologiaIntervento.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaIntervento.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaIntervento
                        {
                            IDTipologiaIntervento = item.ID,
                            TipologiaIntervento = item.TipologiaIntervento,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaMacchineFrigorifere":
                    var resSYS_TipologiaMacchineFrigorifere = (from a in db.SYS_TipologiaMacchineFrigorifere
                                                               select new
                                                               {
                                                                   ID = a.IDTipologiaMacchineFrigorifere,
                                                                   TipologiaMacchineFrigorifere = a.TipologiaMacchineFrigorifere,
                                                                   fAttivo = a.fAttivo
                                                               });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaMacchineFrigorifere = resSYS_TipologiaMacchineFrigorifere.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaMacchineFrigorifere.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaMacchineFrigorifere
                        {
                            IDTipologiaMacchineFrigorifere = item.ID,
                            TipologiaMacchineFrigorifere = item.TipologiaMacchineFrigorifere,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaOrdineCollegio":
                    var resSYS_TipologiaOrdineCollegio = (from a in db.SYS_TipologiaOrdineCollegio
                                                          select new
                                                          {
                                                              ID = a.IDTipologiaOrdineCollegio,
                                                              TipologiaOrdineCollegio = a.TipologiaOrdineCollegio,
                                                              fAttivo = a.fAttivo
                                                          });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaOrdineCollegio = resSYS_TipologiaOrdineCollegio.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaOrdineCollegio.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaOrdineCollegio
                        {
                            IDTipologiaOrdineCollegio = item.ID,
                            TipologiaOrdineCollegio = item.TipologiaOrdineCollegio,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaProtezioneGelo":
                    var resSYS_TipologiaProtezioneGelo = (from a in db.SYS_TipologiaProtezioneGelo
                                                          select new
                                                          {
                                                              ID = a.IDTipologiaProtezioneGelo,
                                                              TipologiaProtezioneGelo = a.TipologiaProtezioneGelo,
                                                              fAttivo = a.fAttivo
                                                          });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaProtezioneGelo = resSYS_TipologiaProtezioneGelo.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaProtezioneGelo.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaProtezioneGelo
                        {
                            IDTipologiaProtezioneGelo = item.ID,
                            TipologiaProtezioneGelo = item.TipologiaProtezioneGelo,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaRaccomandazionePrescrizioneRct":
                    var resSYS_TipologiaRaccomandazionePrescrizioneRct = (from a in db.SYS_TipologiaRaccomandazionePrescrizioneRct
                                                                          select new
                                                                          {
                                                                              ID = a.IDTipologiaRaccomandazionePrescrizioneRct,
                                                                              IDTipologiaRCT = a.IDTipologiaRCT,
                                                                              TipologiaRaccomandazionePrescrizioneRct = a.TipologiaRaccomandazionePrescrizioneRct,
                                                                              fAttivo = a.fAttivo
                                                                          });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaRaccomandazionePrescrizioneRct = resSYS_TipologiaRaccomandazionePrescrizioneRct.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaRaccomandazionePrescrizioneRct.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaRaccomandazionePrescrizioneRct
                        {
                            IDTipologiaRaccomandazionePrescrizioneRct = item.ID,
                            IDTipologiaRCT = item.IDTipologiaRCT,
                            TipologiaRaccomandazionePrescrizioneRct = item.TipologiaRaccomandazionePrescrizioneRct,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaRapportoDiControllo":
                    var resSYS_TipologiaRapportoDiControllo = (from a in db.SYS_TipologiaRapportoDiControllo
                                                               select new
                                                               {
                                                                   ID = a.IDTipologiaRCT,
                                                                   DescrizioneRCT = a.DescrizioneRCT,
                                                                   fAttivo = a.fAttivo
                                                               });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaRapportoDiControllo = resSYS_TipologiaRapportoDiControllo.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaRapportoDiControllo.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaRapportoDiControllo
                        {
                            IDTipologiaRCT = item.ID,
                            DescrizioneRCT = item.DescrizioneRCT,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaResponsabile":
                    var resSYS_TipologiaResponsabile = (from a in db.SYS_TipologiaResponsabile
                                                        select new
                                                        {
                                                            ID = a.IDTipologiaResponsabile,
                                                            TipologiaResponsabile = a.TipologiaResponsabile,
                                                            fAttivo = a.fAttivo
                                                        });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaResponsabile = resSYS_TipologiaResponsabile.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaResponsabile.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaResponsabile
                        {
                            IDTipologiaResponsabile = item.ID,
                            TipologiaResponsabile = item.TipologiaResponsabile,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaSistemaContabilizzazione":
                    var resSYS_TipologiaSistemaContabilizzazione = (from a in db.SYS_TipologiaSistemaContabilizzazione
                                                                    select new
                                                                    {
                                                                        ID = a.IDTipologiaSistemaContabilizzazione,
                                                                        TipologiaSistemaContabilizzazione = a.TipologiaSistemaContabilizzazione,
                                                                        fAttivo = a.fAttivo
                                                                    });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaSistemaContabilizzazione = resSYS_TipologiaSistemaContabilizzazione.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaSistemaContabilizzazione.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaSistemaContabilizzazione
                        {
                            IDTipologiaSistemaContabilizzazione = item.ID,
                            TipologiaSistemaContabilizzazione = item.TipologiaSistemaContabilizzazione,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaSistemaDistribuzione":
                    var resSYS_TipologiaSistemaDistribuzione = (from a in db.SYS_TipologiaSistemaDistribuzione
                                                                select new
                                                                {
                                                                    ID = a.IDTipologiaSistemaDistribuzione,
                                                                    TipologiaSistemaDistribuzione = a.TipologiaSistemaDistribuzione,
                                                                    fAttivo = a.fAttivo
                                                                });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaSistemaDistribuzione = resSYS_TipologiaSistemaDistribuzione.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaSistemaDistribuzione.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaSistemaDistribuzione
                        {
                            IDTipologiaSistemaDistribuzione = item.ID,
                            TipologiaSistemaDistribuzione = item.TipologiaSistemaDistribuzione,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaSistemiEmissione":
                    var resSYS_TipologiaSistemiEmissione = (from a in db.SYS_TipologiaSistemiEmissione
                                                            select new
                                                            {
                                                                ID = a.IDTipologiaSistemiEmissione,
                                                                TipologiaSistemiEmissione = a.TipologiaSistemiEmissione,
                                                                fAttivo = a.fAttivo
                                                            });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaSistemiEmissione = resSYS_TipologiaSistemiEmissione.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaSistemiEmissione.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaSistemiEmissione
                        {
                            IDTipologiaSistemiEmissione = item.ID,
                            TipologiaSistemiEmissione = item.TipologiaSistemiEmissione,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaTermostatoZona":
                    var resSYS_TipologiaTermostatoZona = (from a in db.SYS_TipologiaTermostatoZona
                                                          select new
                                                          {
                                                              ID = a.IDTipologiaTermostatoZona,
                                                              TipologiaTermostatoZona = a.TipologiaTermostatoZona,
                                                              fAttivo = a.fAttivo
                                                          });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaTermostatoZona = resSYS_TipologiaTermostatoZona.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaTermostatoZona.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaTermostatoZona
                        {
                            IDTipologiaTermostatoZona = item.ID,
                            TipologiaTermostatoZona = item.TipologiaTermostatoZona,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaTitoloStudio":
                    var resSYS_TipologiaTitoloStudio = (from a in db.SYS_TipologiaTitoloStudio
                                                        select new
                                                        {
                                                            ID = a.IDTipologiaTitoloStudio,
                                                            TipologiaTitoloStudio = a.TipologiaTitoloStudio,
                                                            fAttivo = a.fAttivo
                                                        });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaTitoloStudio = resSYS_TipologiaTitoloStudio.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaTitoloStudio.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaTitoloStudio
                        {
                            IDTipologiaTitoloStudio = item.ID,
                            TipologiaTitoloStudio = item.TipologiaTitoloStudio,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaTrattamentoAcqua":
                    var resSYS_TipologiaTrattamentoAcqua = (from a in db.SYS_TipologiaTrattamentoAcqua
                                                            select new
                                                            {
                                                                ID = a.IDTipologiaTrattamentoAcqua,
                                                                TipologiaTrattamentoAcqua = a.TipologiaTrattamentoAcqua,
                                                                fAttivo = a.fAttivo
                                                            });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaTrattamentoAcqua = resSYS_TipologiaTrattamentoAcqua.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaTrattamentoAcqua.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaTrattamentoAcqua
                        {
                            IDTipologiaTrattamentoAcqua = item.ID,
                            TipologiaTrattamentoAcqua = item.TipologiaTrattamentoAcqua,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipologiaVentilatori":
                    var resSYS_TipologiaVentilatori = (from a in db.SYS_TipologiaVentilatori
                                                       select new
                                                       {
                                                           ID = a.IDTipologiaVentilatori,
                                                           TipologiaVentilatori = a.TipologiaVentilatori,
                                                           fAttivo = a.fAttivo
                                                       });

                    if (soloAttivi)
                    {
                        resSYS_TipologiaVentilatori = resSYS_TipologiaVentilatori.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipologiaVentilatori.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipologiaVentilatori
                        {
                            IDTipologiaVentilatori = item.ID,
                            TipologiaVentilatori = item.TipologiaVentilatori,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipoSistema":
                    var resSYS_TipoSistema = (from a in db.SYS_TipoSistema
                                              select new
                                              {
                                                  ID = a.IDTipoSistema,
                                                  TipoSistema = a.TipoSistema,
                                                  fAttivo = a.fAttivo
                                              });

                    if (soloAttivi)
                    {
                        resSYS_TipoSistema = resSYS_TipoSistema.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TipoSistema.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipoSistema
                        {
                            IDTipoSistema = item.ID,
                            TipoSistema = item.TipoSistema,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TipoSoggetto":
                    var resSYS_TipoSoggetto = (from a in db.SYS_TipoSoggetto
                                               select new
                                               {
                                                   ID = a.IDTipoSoggetto,
                                                   TipoSoggetto = a.TipoSoggetto,
                                                   fAttivo = a.fattivo,
                                                   fIscrizione = a.fIscrizione
                                               });

                    if (soloAttivi)
                    {
                        resSYS_TipoSoggetto = resSYS_TipoSoggetto.Where(a => a.fAttivo == soloAttivi);
                    }
                    resSYS_TipoSoggetto = resSYS_TipoSoggetto.Where(a => a.fIscrizione == true);

                    foreach (var item in resSYS_TipoSoggetto.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TipoSoggetto
                        {
                            IDTipoSoggetto = item.ID,
                            TipoSoggetto = item.TipoSoggetto,
                            fattivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_TitoliSoggetti":
                    var resSYS_TitoliSoggetti = (from a in db.SYS_TitoliSoggetti
                                                 select new
                                                 {
                                                     ID = a.IDTitoloSoggetto,
                                                     TitoloSoggetto = a.TitoloSoggetto,
                                                     fAttivo = a.fAttivo
                                                 });

                    if (soloAttivi)
                    {
                        resSYS_TitoliSoggetti = resSYS_TitoliSoggetti.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_TitoliSoggetti.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_TitoliSoggetti
                        {
                            IDTitoloSoggetto = item.ID,
                            TitoloSoggetto = item.TitoloSoggetto,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                case "SYS_UnitaMisura":
                    var resSYS_UnitaMisura = (from a in db.SYS_UnitaMisura
                                              select new
                                              {
                                                  ID = a.IDUnitaMisura,
                                                  Descrizione = a.UnitaMisura,
                                                  fAttivo = a.fAttivo
                                              });

                    if (soloAttivi)
                    {
                        resSYS_UnitaMisura = resSYS_UnitaMisura.Where(a => a.fAttivo == soloAttivi);
                    }

                    foreach (var item in resSYS_UnitaMisura.ToList())
                    {
                        lookup.Add(new Criter.Lookup.SYS_UnitaMisura
                        {
                            IDUnitaMisura = item.ID,
                            UnitaMisura = item.Descrizione,
                            fAttivo = item.fAttivo
                        });
                    }
                    break;
                    #endregion
            }

            return lookup;
        }

        public List<RaccomandazioniPrescrizioni> GetRaccomandazioniPrescrizioni()
        {
            List<RaccomandazioniPrescrizioni> lookup = new List<RaccomandazioniPrescrizioni>();
            var raccomandazioniPrescrizioni = (
                                                from SYS_TipologiaRaccomandazionePrescrizioneRct in db.SYS_TipologiaRaccomandazionePrescrizioneRct
                                                join SYS_RCTTipologiaRaccomandazione in db.SYS_RCTTipologiaRaccomandazione on SYS_TipologiaRaccomandazionePrescrizioneRct.IDTipologiaRaccomandazionePrescrizioneRct equals SYS_RCTTipologiaRaccomandazione.IDTipologiaRaccomandazionePrescrizioneRct
                                                where
                                                  SYS_TipologiaRaccomandazionePrescrizioneRct.fAttivo == true &&
                                                  SYS_RCTTipologiaRaccomandazione.fAttivo == true
                                                select new
                                                {
                                                    TipoRct = SYS_TipologiaRaccomandazionePrescrizioneRct.IDTipologiaRCT,
                                                    IDCampoRct = SYS_TipologiaRaccomandazionePrescrizioneRct.IDTipologiaRaccomandazionePrescrizioneRct,
                                                    CampoRct = SYS_TipologiaRaccomandazionePrescrizioneRct.TipologiaRaccomandazionePrescrizioneRct,
                                                    TipoNonConformita = "Raccomandazione",
                                                    IDNonConformita = SYS_RCTTipologiaRaccomandazione.IDTipologiaRaccomandazione,
                                                    NonConformita = SYS_RCTTipologiaRaccomandazione.Raccomandazione
                                                }
                                                ).Concat
                                                (
                                                    from SYS_TipologiaRaccomandazionePrescrizioneRct in db.SYS_TipologiaRaccomandazionePrescrizioneRct
                                                    join SYS_RCTTipologiaPrescrizione in db.SYS_RCTTipologiaPrescrizione on SYS_TipologiaRaccomandazionePrescrizioneRct.IDTipologiaRaccomandazionePrescrizioneRct equals SYS_RCTTipologiaPrescrizione.IDTipologiaRaccomandazionePrescrizioneRct
                                                    where
                                                      SYS_TipologiaRaccomandazionePrescrizioneRct.fAttivo == true &&
                                                      SYS_RCTTipologiaPrescrizione.fAttivo == true
                                                    select new
                                                    {
                                                        TipoRct = SYS_TipologiaRaccomandazionePrescrizioneRct.IDTipologiaRCT,
                                                        IDCampoRct = SYS_TipologiaRaccomandazionePrescrizioneRct.IDTipologiaRaccomandazionePrescrizioneRct,
                                                        CampoRct = SYS_TipologiaRaccomandazionePrescrizioneRct.TipologiaRaccomandazionePrescrizioneRct,
                                                        TipoNonConformita = "Prescrizione",
                                                        IDNonConformita = SYS_RCTTipologiaPrescrizione.IDTipologiaPrescrizione,
                                                        NonConformita = SYS_RCTTipologiaPrescrizione.Prescrizione
                                                    }
                                                );

            int index = 1;
            foreach (var item in raccomandazioniPrescrizioni.ToList())
            {
                lookup.Add(new RaccomandazioniPrescrizioni
                {
                    ID = index++,
                    TipoRct = item.TipoRct,
                    IDCampoRct = item.IDCampoRct,
                    CampoRct = item.CampoRct,
                    TipoNonConformita = item.TipoNonConformita,
                    IDNonConformita = item.IDNonConformita,
                    NonConformita = item.NonConformita
                });
            };

            return lookup;
        }


        #endregion


        #region Api for Sace.Net
        public ResponseCriterDto<List<ResponseLibrettiImpiantiDto>> GetLibrettiImpiantiByParameters(RequestLibrettoImpiantoDto dto)
        {
            List<ResponseLibrettiImpiantiDto> LibrettiImpianti = new List<ResponseLibrettiImpiantiDto>();
            
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var libretti = ctx.V_LIM_LibrettiImpianti
                        .Where(t => t.fAttivo == true && t.IDStatoLibrettoImpianto == 2)
                        .AsNoTracking()
                        .Select(t => new
                        {
                            t.IDLibrettoImpianto,
                            t.CodiceCatastale,
                            t.CodiceTargatura,
                            t.DataInserimento,
                            t.Indirizzo,
                            t.Civico,
                            t.NumeroPOD,
                            t.NumeroPDR,
                            t.CodiceFiscaleResponsabile,
                            t.PartitaIvaResponsabile,
                            t.IDCodiceCatastale,
                            t.SoggettoAzienda,
                            t.SoggettoManutentore,
                            t.IDTipologiaResponsabile,
                            t.NomeResponsabile,
                            t.CognomeResponsabile,
                            t.RagioneSocialeResponsabile,
                            t.IDTargaturaImpianto
                        });

                if (!string.IsNullOrEmpty(dto.CodiceTargaturaImpianto))
                {
                    libretti = libretti.Where(t => t.CodiceTargatura == dto.CodiceTargaturaImpianto);
                }

                if (!string.IsNullOrEmpty(dto.CodiceCatastale))
                {
                    var CodiceCatastale = ctx.SYS_CodiciCatastali.Where(a => a.CodiceCatastale == dto.CodiceCatastale).AsNoTracking().FirstOrDefault();
                    if (CodiceCatastale != null)
                    {
                        libretti = libretti.Where(t => t.IDCodiceCatastale == CodiceCatastale.IDCodiceCatastale);
                    }
                }

                if (!string.IsNullOrEmpty(dto.MatricolaGeneratore))
                {
                    var matricolaQuery = ctx.LIM_LibrettiImpianti.Where(l => l.fAttivo == true && l.IDStatoLibrettoImpianto == 2).AsNoTracking()
                                        .Select(t => new
                                        {
                                            t.IDLibrettoImpianto
                                        });

                    switch (dto.TipoGeneratore)
                    {
                        case TipoGeneratore.GT:
                            matricolaQuery = matricolaQuery.Where(l => ctx.LIM_LibrettiImpiantiGruppiTermici
                                .Any(g => g.IDLibrettoImpianto == l.IDLibrettoImpianto && g.fAttivo == true && g.fDismesso == false && g.Matricola.Contains(dto.MatricolaGeneratore)));
                            break;
                        case TipoGeneratore.GF:
                            matricolaQuery = matricolaQuery.Where(l => ctx.LIM_LibrettiImpiantiMacchineFrigorifere
                                .Any(m => m.IDLibrettoImpianto == l.IDLibrettoImpianto && m.fAttivo == true && m.fDismesso == false && m.Matricola.Contains(dto.MatricolaGeneratore)));
                            break;
                        case TipoGeneratore.SC:
                            matricolaQuery = matricolaQuery.Where(l => ctx.LIM_LibrettiImpiantiScambiatoriCalore
                                .Any(s => s.IDLibrettoImpianto == l.IDLibrettoImpianto && s.fAttivo == true && s.fDismesso == false && s.Matricola.Contains(dto.MatricolaGeneratore)));
                            break;
                        case TipoGeneratore.CG:
                            matricolaQuery = matricolaQuery.Where(l => ctx.LIM_LibrettiImpiantiCogeneratori
                                .Any(c => c.IDLibrettoImpianto == l.IDLibrettoImpianto && c.fAttivo == true && c.fDismesso == false && c.Matricola.Contains(dto.MatricolaGeneratore)));
                            break;
                    }

                    var librettoIds = matricolaQuery.Select(l => l.IDLibrettoImpianto).ToList();
                    libretti = libretti.Where(t => librettoIds.Contains(t.IDLibrettoImpianto));
                }

                if (!string.IsNullOrEmpty(dto.CodicePod))
                {
                    libretti = libretti.Where(t => t.NumeroPOD == dto.CodicePod);
                }

                if (!string.IsNullOrEmpty(dto.CodicePdr))
                {
                    libretti = libretti.Where(t => t.NumeroPDR == dto.CodicePdr);
                }

                if (!string.IsNullOrEmpty(dto.Indirizzo))
                {
                    libretti = libretti.Where(t => t.Indirizzo.Contains(dto.Indirizzo));
                }

                if (!string.IsNullOrEmpty(dto.NumeroCivico))
                {
                    libretti = libretti.Where(t => t.Civico.Contains(dto.NumeroCivico));
                }

                if (!string.IsNullOrEmpty(dto.CfPIvaResponsabile))
                {
                    libretti = libretti.Where(t => (t.CodiceFiscaleResponsabile == dto.CfPIvaResponsabile || t.PartitaIvaResponsabile == dto.CfPIvaResponsabile));
                }

                var ListLibrettiImpianti = libretti.ToList();

                if (ListLibrettiImpianti.Count == 1)
                {
                    foreach (var libretto in ListLibrettiImpianti)
                    {
                        LibrettiImpianti.Add(new ResponseLibrettiImpiantiDto
                        {
                            Azienda = libretto.SoggettoAzienda,
                            OperatoreAddetto = libretto.SoggettoManutentore,
                            Responsabile = libretto.IDTipologiaResponsabile == 1 ? libretto.NomeResponsabile + " " + libretto.CognomeResponsabile : libretto.NomeResponsabile + " " + libretto.CognomeResponsabile + " - " + libretto.RagioneSocialeResponsabile,
                            CFPIvaResponsabile = libretto.IDTipologiaResponsabile == 1 ? libretto.CodiceFiscaleResponsabile : libretto.PartitaIvaResponsabile,
                            Indirizzo = libretto.Indirizzo + " " + libretto.Civico,
                            Comune = libretto.CodiceCatastale,
                            Pod = libretto.NumeroPOD,
                            Pdr = libretto.NumeroPDR,
                            Impianto = $"Impianto codice targatura {libretto.CodiceTargatura} - Responsabile {libretto.NomeResponsabile} {libretto.CognomeResponsabile}",
                            CodiceTargatura = libretto.CodiceTargatura,
                            IDTargaturaImpianto = (int)libretto.IDTargaturaImpianto
                        });
                    }

                    return new ResponseCriterDto<List<ResponseLibrettiImpiantiDto>>(LibrettiImpianti);
                }
                else if (ListLibrettiImpianti.Count > 1)
                {
                    return new ResponseCriterDto<List<ResponseLibrettiImpiantiDto>>("Sono stati trovati troppi risultati. Affinare la ricerca inserendo ulteriori parametri di filtro!");
                }
                else if (ListLibrettiImpianti.Count == 0)
                {
                    return new ResponseCriterDto<List<ResponseLibrettiImpiantiDto>>("La ricerca sul catasto impianti termici Criter non ha prodotto risultati.");
                }
            }

            return new ResponseCriterDto<List<ResponseLibrettiImpiantiDto>>("Errore inatteso durante la ricerca delle utenze distributori su Criter.");
        }

        public ResponseCriterDto<List<ResponsePodPdrDto>> GetPodPdrByAddress(RequestPodPdrDto dto)
        {
            List<ResponsePodPdrDto> UtenzeDistributori = new List<ResponsePodPdrDto>();

            if (!string.IsNullOrEmpty(dto.CodiceIstat) && !string.IsNullOrEmpty(dto.Indirizzo) && !string.IsNullOrEmpty(dto.NumeroCivico) && !string.IsNullOrEmpty(dto.Cap))
            {
                using (CriterDataModel ctx = new CriterDataModel())
                {
                    int CodiceIstat = int.Parse(dto.CodiceIstat);

                    var utenze = ctx.UTE_DatiFornituraCliente
                           .Where(t => t.AnnoRiferimento == DateTime.Now.Year - 2 && t.CodiceISTATComune.Contains(CodiceIstat.ToString()))
                           .AsNoTracking()
                           .Select(t => new
                           {
                               IDDatiFornituraCliente = t.Id,
                               CodicePod = t.CodicePdrPod.Contains("IT") ? t.CodicePdrPod : null,
                               CodicePdr = !(t.CodicePdrPod.Contains("IT")) ? t.CodicePdrPod : null,
                               Indirizzo = t.Indirizzo,
                               Civico = t.Civico,
                               Cap = t.Cap
                           });

                    if (!string.IsNullOrEmpty(dto.Indirizzo))
                    {
                        var indirizzoRicerca = dto.Indirizzo.Trim().ToLower();

                        // Se la stringa inizia con "via " la rimuovo
                        if (indirizzoRicerca.StartsWith("via "))
                        {
                            indirizzoRicerca = indirizzoRicerca.Substring(4);
                        }

                        utenze = utenze.Where(t => t.Indirizzo.ToLower().Contains(indirizzoRicerca));
                    }

                    if (!string.IsNullOrEmpty(dto.NumeroCivico))
                    {
                        utenze = utenze.Where(t => t.Civico.Contains(dto.NumeroCivico));
                    }

                    if (!string.IsNullOrEmpty(dto.Cap))
                    {
                        utenze = utenze.Where(t => t.Cap.Contains(dto.Cap));
                    }

                    if (dto.DatiCatastali != null && dto.DatiCatastali.Count() > 0)
                    {
                        var catastoKeys = dto.DatiCatastali
                                        .Where(d => !string.IsNullOrEmpty(d.Foglio) &&
                                                    !string.IsNullOrEmpty(d.Mappale) &&
                                                    !string.IsNullOrEmpty(d.Subalterno))
                                        .Select(d => d.Foglio + "|" + d.Mappale + "|" + d.Subalterno)
                                        .ToList();


                        if (catastoKeys.Any())
                        {
                            var DatiFornituraClienteIds = ctx.UTE_DatiCatastali
                                                        .AsNoTracking()
                                                        .Where(d => catastoKeys.Contains(d.Foglio + "|" + d.Mappale + "|" + d.Subalterno))
                                                        .Select(d => d.IdDatiFornituraCliente)
                                                        .ToList();

                            if (DatiFornituraClienteIds.Any())
                            {
                                utenze = utenze.Where(t => DatiFornituraClienteIds.Contains(t.IDDatiFornituraCliente));
                            }
                        }
                    }

                    foreach (var utenza in utenze.ToList())
                    {
                        string tipoCodice = !string.IsNullOrEmpty(utenza.CodicePod) ? "Pod" : "Pdr";
                        string codiceValore = !string.IsNullOrEmpty(utenza.CodicePod) ? utenza.CodicePod : utenza.CodicePdr;

                        // Costruzione dinamica della descrizione
                        string descrizione = $"Utenza in via {utenza.Indirizzo} {utenza.Civico} con codice {tipoCodice} {codiceValore}";

                        UtenzeDistributori.Add(new ResponsePodPdrDto
                        {
                            Utenza = descrizione,
                            CodicePod = utenza.CodicePod,
                            CodicePdr = utenza.CodicePdr
                        });
                    }

                    return new ResponseCriterDto<List<ResponsePodPdrDto>>(UtenzeDistributori);
                }
            }
            else
            {
                return new ResponseCriterDto<List<ResponsePodPdrDto>>("Nella ricerca sulle utenze dei distribuoti di combustibile è necessario indicare CodiceIstat,Indirizzo,NumeroCivico,Cap.");
            }
        }
        
        #endregion

    }


}
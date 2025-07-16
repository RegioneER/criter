using Bender.Collections;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Z.EntityFramework.Plus;

namespace DataUtilityCore
{

    public class UtilityRapportiControllo
    {
        public static int? GetIDSoggettoDaIDRapporto(long iDRapportoControlloTecnico)
        {
            int? iDSoggetto;
            using (CriterDataModel ctx = new CriterDataModel())
            {
                iDSoggetto = ctx.RCT_RapportoDiControlloTecnicoBase.Where(r => r.IDRapportoControlloTecnico == iDRapportoControlloTecnico).Select(r => r.LIM_LibrettiImpianti.IDSoggetto).FirstOrDefault();
            }

            return iDSoggetto;
        }

        #region AutoCompilatori Rapporto di Controllo
        public static void AutocompilatoreRapportobase(CriterDataModel ctx, int iDSoggetto, int iDSoggettoDerived, RCT_RapportoDiControlloTecnicoBase rapporto, int iDLibrettoImpianto, string prefisso, int codiceProgressivo, int iDTipologiaRCT, object guidInteroImpianto)
        {
            //int iDSoggetto = int.Parse(cmbAziende.Value.ToString());
            //int iDSoggettoDerived = int.Parse(cmbAddetti.Value.ToString());
            var librettoQuery = ctx.LIM_LibrettiImpianti.Single(c => c.fAttivo == true && c.IDLibrettoImpianto == iDLibrettoImpianto);
            rapporto.IDLibrettoImpianto = iDLibrettoImpianto;
            rapporto.IDStatoRapportoDiControllo = 1;
            rapporto.CodiceProgressivo = codiceProgressivo;
            rapporto.Prefisso = prefisso;
            rapporto.IDTipologiaRCT = iDTipologiaRCT;
            rapporto.IDTipologiaControllo = 1;
            rapporto.IDSoggetto = iDSoggetto;
            rapporto.IDSoggettoDerived = iDSoggettoDerived;
            rapporto.TrattamentoACS = 2;
            rapporto.TrattamentoRiscaldamento = 2;
            rapporto.NomeResponsabile = librettoQuery.NomeResponsabile;
            rapporto.CognomeResponsabile = librettoQuery.CognomeResponsabile;
            rapporto.CodiceFiscaleResponsabile = librettoQuery.CodiceFiscaleResponsabile;
            if (librettoQuery.RagioneSocialeResponsabile != null)
            {
                rapporto.RagioneSocialeResponsabile = librettoQuery.RagioneSocialeResponsabile;
            }
            else
            {
                rapporto.RagioneSocialeResponsabile = null;
            }
            if (librettoQuery.PartitaIvaResponsabile != null)
            {
                rapporto.PartitaIVAResponsabile = librettoQuery.PartitaIvaResponsabile;
            }
            else
            {
                rapporto.PartitaIVAResponsabile = null;
            }
            rapporto.IDTipoSoggetto = librettoQuery.IDTipoSoggetto;
            rapporto.IDTipologiaResponsabile = librettoQuery.IDTipologiaResponsabile;
            rapporto.IndirizzoResponsabile = librettoQuery.IndirizzoResponsabile;
            rapporto.CivicoResponsabile = librettoQuery.CivicoResponsabile;
            var comuneResponsabile = ctx.SYS_CodiciCatastali.Where(c => c.IDCodiceCatastale == librettoQuery.IDComuneResponsabile).FirstOrDefault();
            if (comuneResponsabile != null)
            {
                rapporto.ComuneResponsabile = comuneResponsabile.Comune;
                rapporto.IDProvinciaResponsabile = comuneResponsabile.IDProvincia;
            }

            //CICCIO: Modificato 12/12/2023 - Sbagliava a pescare l'indirizzo del terzo responsabile
            if (librettoQuery.fTerzoResponsabile == true)
            {
                DateTime oggi = DateTime.Now.Date;
                var terzoResponsabile = ctx.LIM_LibrettiImpiantiResponsabili.Where(c => c.IDLibrettoImpianto == librettoQuery.IDLibrettoImpianto && c.fAttivo == true && c.DataInizio.HasValue && c.DataInizio.Value <= oggi && (!c.DataFine.HasValue || c.DataFine.Value > oggi)).OrderByDescending(c => c.DataInizio).FirstOrDefault();
                if (terzoResponsabile != null)
                {
                    rapporto.RagioneSocialeTerzoResponsabile = terzoResponsabile.RagioneSociale;
                    rapporto.PartitaIVATerzoResponsabile = terzoResponsabile.PartitaIva;

                    if (terzoResponsabile.IDSoggetto != null)
                    {
                        if (terzoResponsabile.COM_AnagraficaSoggetti.IndirizzoSedeLegale != null)
                        {
                            rapporto.IndirizzoTerzoResponsabile = terzoResponsabile.COM_AnagraficaSoggetti.IndirizzoSedeLegale;
                        }
                        if (terzoResponsabile.COM_AnagraficaSoggetti.NumeroCivicoSedeLegale != null)
                        {
                            rapporto.CivicoTerzoResponsabile = terzoResponsabile.COM_AnagraficaSoggetti.NumeroCivicoSedeLegale;
                        }
                        if (terzoResponsabile.COM_AnagraficaSoggetti.CittaSedeLegale != null)
                        {
                            rapporto.ComuneTerzoResponsabile = terzoResponsabile.COM_AnagraficaSoggetti.CittaSedeLegale;
                        }
                        if (terzoResponsabile.COM_AnagraficaSoggetti.IDProvinciaSedeLegale != null)
                        {
                            rapporto.IDProvinciaTerzoResponsabile = terzoResponsabile.COM_AnagraficaSoggetti.IDProvinciaSedeLegale;
                        }
                    }
                }
            }

            //if (librettoQuery.fTerzoResponsabile == true)
            //{
            //    DateTime oggi = DateTime.Now.Date;
            //    var terzoResponsabile = ctx.V_LIM_LibrettiImpiantiTerziResponsabili.Where(a => a.IDLibrettoImpianto == librettoQuery.IDLibrettoImpianto && a.DataInizio.HasValue && a.DataInizio.Value <= oggi && (!a.DataFine.HasValue || a.DataFine.Value > oggi)).OrderByDescending(c => c.DataInizio).FirstOrDefault();
            //    if (terzoResponsabile != null)
            //    {
            //        rapporto.RagioneSocialeTerzoResponsabile = terzoResponsabile.RagioneSocialeTerzoResponsabile;
            //        rapporto.PartitaIVATerzoResponsabile = terzoResponsabile.PartitaIvaTerzoResponsabile;

            //        if (terzoResponsabile.IDSoggetto != null)
            //        {
            //            if (terzoResponsabile.IndirizzoTerzoResponsabile != null)
            //            {
            //                rapporto.IndirizzoTerzoResponsabile = terzoResponsabile.IndirizzoTerzoResponsabile;
            //            }
            //            if (terzoResponsabile.CivicoTerzoResponsabile != null)
            //            {
            //                rapporto.CivicoTerzoResponsabile = terzoResponsabile.CivicoTerzoResponsabile;
            //            }
            //            if (terzoResponsabile.CittaTerzoResponsabile != null)
            //            {
            //                rapporto.ComuneTerzoResponsabile = terzoResponsabile.CittaTerzoResponsabile;
            //            }
            //            if (terzoResponsabile.IDProvinciaTerzoResponsabile != null)
            //            {
            //                rapporto.IDProvinciaTerzoResponsabile = terzoResponsabile.IDProvinciaTerzoResponsabile;
            //            }
            //        }
            //    }
            //}

            rapporto.IDTargaturaImpianto = int.Parse(librettoQuery.IDTargaturaImpianto.ToString());
            rapporto.Indirizzo = librettoQuery.Indirizzo;
            if (!string.IsNullOrEmpty(librettoQuery.Civico))
            {
                rapporto.Civico = librettoQuery.Civico;
            }
            else
            {
                rapporto.Civico = null;
            }
            if (!string.IsNullOrEmpty(librettoQuery.Palazzo))
            {
                rapporto.Palazzo = librettoQuery.Palazzo;
            }
            else
            {
                rapporto.Palazzo = null;
            }
            if (!string.IsNullOrEmpty(librettoQuery.Scala))
            {
                rapporto.Scala = librettoQuery.Scala;
            }
            else
            {
                rapporto.Scala = null;
            }
            if (!string.IsNullOrEmpty(librettoQuery.Interno))
            {
                rapporto.Interno = librettoQuery.Interno;
            }
            else
            {
                rapporto.Interno = null;
            }

            rapporto.IDCodiceCatastale = librettoQuery.IDCodiceCatastale;
            rapporto.fClimatizzazioneInvernale = librettoQuery.fClimatizzazioneInvernale;
            rapporto.fClimatizzazioneEstiva = librettoQuery.fClimatizzazioneEstiva;
            rapporto.fProduzioneACS = librettoQuery.fClimatizzazioneAltro;
            rapporto.DurezzaAcqua = null;

            var azienda = ctx.COM_AnagraficaSoggetti.FirstOrDefault(c => c.IDSoggetto == iDSoggetto);
            if (azienda != null)
            {
                rapporto.RagioneSocialeImpresaManutentrice = azienda.NomeAzienda;
                rapporto.PartitaIVAImpresaManutentrice = azienda.PartitaIVA;
                rapporto.IndirizzoImpresaManutentrice = azienda.IndirizzoSedeLegale;
                rapporto.CivicoImpresaManutentrice = azienda.NumeroCivicoSedeLegale;
                rapporto.ComuneImpresaManutentrice = azienda.CittaSedeLegale;
                rapporto.IDProvinciaImpresaManutentrice = azienda.IDProvinciaSedeLegale;
            }
            rapporto.OraArrivo = DateTime.Now;
            rapporto.OraPartenza = DateTime.Now;

            rapporto.fDichiarazioneConformita = true;
            rapporto.fLibrettoImpiantoPresente = true;
            rapporto.fUsoManutenzioneGeneratore = true;
            rapporto.fLibrettoImpiantoCompilato = true;

            rapporto.LocaleInstallazioneIdoneo = (int)EnumStatoSiNoNc.NonClassificabile;
            rapporto.ApertureLibere = (int)EnumStatoSiNoNc.NonClassificabile;
            rapporto.DimensioniApertureAdeguate = (int)EnumStatoSiNoNc.NonClassificabile;
            rapporto.LineeElettricheIdonee = (int)EnumStatoSiNoNc.NonClassificabile;
            rapporto.CoibentazioniIdonee = (int)EnumStatoSiNoNc.NonClassificabile;
            rapporto.StatoCoibentazioniIdonee = (int)EnumStatoSiNoNc.NonClassificabile;
            rapporto.AssenzaPerditeCombustibile = (int)EnumStatoSiNoNc.NonClassificabile;
            rapporto.TenutaImpiantoIdraulico = (int)EnumStatoSiNoNc.NonClassificabile;
            rapporto.ScarichiIdonei = (int)EnumStatoSiNoNc.NonClassificabile;

            rapporto.Contabilizzazione = (int)EnumStatoSiNoNc.NonApplicabile;
            rapporto.Termoregolazione = (int)EnumStatoSiNoNc.NonApplicabile;
            rapporto.CorrettoFunzionamentoContabilizzazione = (int)EnumStatoSiNoNc.NonApplicabile;
            rapporto.GuidRapportoTecnico = Guid.NewGuid().ToString();
            rapporto.DataInserimento = DateTime.Now;
            rapporto.fImpiantoFunzionante = true;

            if (guidInteroImpianto != null)
            {
                rapporto.guidInteroImpianto = guidInteroImpianto.ToString();
            }
            else
            {
                rapporto.guidInteroImpianto = null;
            }
        }

        public static void AutocompilatoreRapportoGT(int iDSoggetto, int iDSoggettoDerived, RCT_RapportoDiControlloTecnicoBase rapporto, RCT_RapportoDiControlloTecnicoGT rapportoGT, int iDLibrettoImpianto, string prefisso, int codiceProgressivo, int iDLibrettoImpiantoGruppoTermico, int iDTipologiaRCT, object guidInteroImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                AutocompilatoreRapportobase(ctx, iDSoggetto, iDSoggettoDerived, rapporto, iDLibrettoImpianto, prefisso, codiceProgressivo, iDTipologiaRCT, guidInteroImpianto);

                var gruppo = ctx.LIM_LibrettiImpiantiGruppiTermici.Single(c => c.IDLibrettoImpiantoGruppoTermico == iDLibrettoImpiantoGruppoTermico);

                rapporto.DataInstallazione = gruppo.DataInstallazione;
                rapporto.Fabbricante = WebUtility.HtmlDecode(gruppo.Fabbricante); // gruppo.Fabbricante;
                rapporto.Modello = gruppo.Modello;
                rapporto.Matricola = gruppo.Matricola;

                rapporto.PotenzaTermicaNominale = gruppo.PotenzaTermicaUtileNominaleKw;
                rapportoGT.IdTipologiaGruppiTermici = gruppo.IDTipologiaGruppiTermici;
                rapporto.IDTipologiaCombustibile = gruppo.IDTipologiaCombustibile;
                rapporto.AltroCombustibile = gruppo.CombustibileAltro;
                rapportoGT.IDLIM_LibrettiImpiantiGruppitermici = gruppo.IDLibrettoImpiantoGruppoTermico;

                rapportoGT.GeneratoriIdonei = (int)EnumStatoSiNoNc.NonClassificabile;
                rapportoGT.RegolazioneTemperaturaAmbiente = (int)EnumStatoSiNoNc.NonClassificabile;
                rapportoGT.DispositiviComandoRegolazione = (int)EnumStatoSiNoNc.NonClassificabile;
                rapportoGT.DispositiviSicurezza = (int)EnumStatoSiNoNc.NonClassificabile;
                rapportoGT.ValvolaSicurezzaSovrappressione = (int)EnumStatoSiNoNc.NonClassificabile;
                rapportoGT.ScambiatoreFumiPulito = (int)EnumStatoSiNoNc.NonClassificabile;
                rapportoGT.RiflussoProdottiCombustione = (int)EnumStatoSiNoNc.NonClassificabile;
                rapportoGT.ConformitaUNI10389 = (int)EnumStatoSiNoNc.NonClassificabile;
                rapportoGT.ModuloTermico = string.Empty;
                rapportoGT.EvacuazioneNaturale = false;
                rapportoGT.EvacuazioneForzata = true;
                rapportoGT.RispettaIndiceBacharach = true;
                rapportoGT.COFumiSecchiNoAria1000 = true;
                rapportoGT.RendimentoSupMinimo = true;
                decimal potTermicaFocolare = (rapporto.PotenzaTermicaNominale.Value * 100 / gruppo.RendimentoTermicoUtilePc.Value);
                rapportoGT.PotenzaTermicaNominaleFocolare = decimal.Round(potTermicaFocolare, 2, MidpointRounding.AwayFromZero);
            }
        }

        public static void AutocompilatoreRapportoGF(int iDSoggetto, int iDSoggettoDerived, RCT_RapportoDiControlloTecnicoBase rapporto, RCT_RapportoDiControlloTecnicoGF rapportoGF, int iDLibrettoImpianto, string prefisso, int codiceProgressivo, int iDLibrettoImpiantoMacchinaFrigorifera, int iDTipologiaRCT, object guidInteroImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                AutocompilatoreRapportobase(ctx, iDSoggetto, iDSoggettoDerived, rapporto, iDLibrettoImpianto, prefisso, codiceProgressivo, iDTipologiaRCT, guidInteroImpianto);

                var gruppo = ctx.LIM_LibrettiImpiantiMacchineFrigorifere.Single(c => c.IDLibrettoImpiantoMacchinaFrigorifera == iDLibrettoImpiantoMacchinaFrigorifera);

                rapporto.DataInstallazione = gruppo.DataInstallazione;
                rapporto.Fabbricante = WebUtility.HtmlDecode(gruppo.Fabbricante); //gruppo.Fabbricante;
                rapporto.Modello = gruppo.Modello;
                rapporto.Matricola = gruppo.Matricola;

                rapportoGF.NCircuitiTotali = gruppo.NumCircuiti;
                rapportoGF.Potenzafrigorifera = decimal.Parse(gruppo.PotenzaFrigoriferaNominaleKw.ToString());
                rapporto.PotenzaTermicaNominale = decimal.Parse(gruppo.PortataTermicaNominaleKw.ToString());
                rapportoGF.IDTipologiemacchineFrigorifere = gruppo.IDTipologiaMacchineFrigorifere;
                rapportoGF.IdLIM_LibrettiImpiantiMacchineFrigorifere = gruppo.IDLibrettoImpiantoMacchinaFrigorifera;

                rapportoGF.AssenzaPerditeRefrigerante = (int)EnumStatoSiNoNc.NonClassificabile;
                rapportoGF.LeakDetector = (int)EnumStatoSiNoNc.NonClassificabile;
                rapportoGF.ParametriTermodinamici = (int)EnumStatoSiNoNc.NonClassificabile;
                rapportoGF.ScambiatoriLiberi = (int)EnumStatoSiNoNc.NonClassificabile;
                rapportoGF.FiltriPuliti = (int)EnumStatoSiNoNc.NonClassificabile;
                rapportoGF.ProvaRaffrescamento = true;
                rapportoGF.ProvaRiscaldamento = false;
            }
        }

        public static void AutocompilatoreRapportoSC(int iDSoggetto, int iDSoggettoDerived, RCT_RapportoDiControlloTecnicoBase rapporto, RCT_RapportoDiControlloTecnicoSC rapportoSC, int iDLibrettoImpianto, string prefisso, int codiceProgressivo, int iDLibrettoImpiantoScambiatoreCalore, int iDTipologiaRCT, object guidInteroImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                AutocompilatoreRapportobase(ctx, iDSoggetto, iDSoggettoDerived, rapporto, iDLibrettoImpianto, prefisso, codiceProgressivo, iDTipologiaRCT, guidInteroImpianto);

                var gruppo = ctx.LIM_LibrettiImpiantiScambiatoriCalore.Single(c => c.IDLibrettoImpiantoScambiatoreCalore == iDLibrettoImpiantoScambiatoreCalore);

                rapporto.DataInstallazione = gruppo.DataInstallazione;
                rapporto.Fabbricante = WebUtility.HtmlDecode(gruppo.Fabbricante);// gruppo.Fabbricante;
                rapporto.Modello = gruppo.Modello;
                rapporto.Matricola = gruppo.Matricola;

                rapporto.PotenzaTermicaNominale = gruppo.PotenzaTermicaNominaleTotaleKw;
                rapportoSC.IdLIM_LibrettiImpiantiScambaitoriCalore = gruppo.IDLibrettoImpiantoScambiatoreCalore;

                rapportoSC.TemperaturaEsterna = 0;
                rapportoSC.TemperaturaMandataPrimario = 0;
                rapportoSC.TemperaturaRitornoPrimario = 0;
                rapportoSC.TemperaturaRitornoSecondario = 0;
                rapportoSC.PotenzaCompatibileProgetto = (int)EnumStatoSiNoNc.NonClassificabile;
                rapportoSC.PotenzaTermica = 0;
                rapportoSC.PortataFluidoPrimario = 0;
                rapportoSC.AssenzaTrafilamenti = (int)EnumStatoSiNoNc.NonClassificabile;
                rapportoSC.TemperaturaEsterna = 0;
                rapportoSC.TemperaturaMandataPrimario = 0;
                rapportoSC.TemperaturaMandataSecondario = 0;
                rapportoSC.TemperaturaRitornoPrimario = 0;
                rapportoSC.TemperaturaRitornoSecondario = 0;

                rapportoSC.DispositiviComandoRegolazione = 0;
            }
        }

        public static void AutocompilatoreRapportoCG(int iDSoggetto, int iDSoggettoDerived, RCT_RapportoDiControlloTecnicoBase rapporto, RCT_RapportoDiControlloTecnicoCG rapportoCG, int iDLibrettoImpianto, string prefisso, int codiceProgressivo, int iDLibrettoImpiantoCogeneratore, int iDTipologiaRCT, object guidInteroImpianto)
        {
            using (var ctx = new CriterDataModel())
            {
                AutocompilatoreRapportobase(ctx, iDSoggetto, iDSoggettoDerived, rapporto, iDLibrettoImpianto, prefisso, codiceProgressivo, iDTipologiaRCT, guidInteroImpianto);

                var gruppo = ctx.LIM_LibrettiImpiantiCogeneratori.Single(c => c.IDLibrettoImpiantoCogeneratore == iDLibrettoImpiantoCogeneratore);

                rapporto.DataInstallazione = gruppo.DataInstallazione;
                rapporto.Fabbricante = WebUtility.HtmlDecode(gruppo.Fabbricante); //gruppo.Fabbricante;
                rapporto.Modello = gruppo.Modello;
                rapporto.Matricola = gruppo.Matricola;
                rapporto.IDTipologiaCombustibile = gruppo.IDTipologiaCombustibile;
                rapporto.AltroCombustibile = gruppo.CombustibileAltro;
                rapportoCG.IDTipologiaCogeneratore = gruppo.IDTipologiaCogeneratore;
                rapportoCG.PotenzaAiMorsetti = gruppo.PotenzaElettricaNominaleKw;
                rapportoCG.PotenzaMassimoRecupero = gruppo.PotenzaTermicaNominaleKw;
                rapportoCG.EmissioneMonossido = gruppo.EmissioniCOMax;
                rapportoCG.IdLIM_LibrettiImpiantiCogeneratori = gruppo.IDLibrettoImpiantoCogeneratore;

                rapportoCG.CapsulaInsonorizzataIdonea = (int)EnumStatoSiNoNc.NonClassificabile;
                rapportoCG.TenutaCircuitoOlioIdonea = (int)EnumStatoSiNoNc.NonClassificabile;
                rapportoCG.FunzionalitàScambiatoreSeparazione = (int)EnumStatoSiNoNc.NonClassificabile;
            }
        }

        #endregion

        public static string GetSqlValoriRapportiFilter(object iDSoggettoAzienda,
            object iDSoggettoManutentore, object codiceTargaturaImpianto,
            object IDStatoRapportoDiControllo, object IDTipologiaRCT,
            object IDTipologiaControllo, string listIDRapportiControlloTecnici,
            object[] valoriCriticitaSelected,
            object DataRegistrazioneDal, object DataRegistrazioneAl,
            object DataFirmaDal, object DataFirmaAl,
            object DataEsecuzioneVerificaDal, object DataEsecuzioneVerificaAl,
            string KeyApi,
            object iDCodiceCastale,
            object foglio,
            object mappale,
            object subalterno,
            object identificativo,
            string responsabile)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" * ");
            strSql.Append(" FROM V_RCT_RapportiControlloTecnico ");
            strSql.Append(" WHERE 1=1 ");

            if ((iDSoggettoAzienda != "") && (iDSoggettoAzienda != "-1") && (iDSoggettoAzienda != null))
            {
                strSql.Append(" AND IDSoggettoAzienda=" + iDSoggettoAzienda);
            }

            if ((iDSoggettoManutentore != "") && (iDSoggettoManutentore != "-1") && (iDSoggettoManutentore != null))
            {
                strSql.Append(" AND IDSoggettoManutentore=" + iDSoggettoManutentore);
            }

            if (!string.IsNullOrEmpty(KeyApi))
            {
                strSql.Append(" AND keyApi = ");
                strSql.Append("'");
                strSql.Append(KeyApi);
                strSql.Append("'");
            }

            if (codiceTargaturaImpianto.ToString() != "")
            {
                strSql.Append(" AND CodiceTargatura = ");
                strSql.Append("'");
                strSql.Append(codiceTargaturaImpianto);
                strSql.Append("'");
            }

            if (IDStatoRapportoDiControllo.ToString() != "0")
            {
                strSql.Append(" AND IDStatoRapportoDiControllo=");
                strSql.Append(IDStatoRapportoDiControllo);
            }

            if (IDTipologiaRCT.ToString() != "0")
            {
                strSql.Append(" AND IDTipologiaRCT=");
                strSql.Append(IDTipologiaRCT);
            }

            if (IDTipologiaControllo.ToString() != "0")
            {
                strSql.Append(" AND IDTipologiaControllo=");
                strSql.Append(IDTipologiaControllo);
            }

            if (!string.IsNullOrEmpty(listIDRapportiControlloTecnici))
            {
                strSql.Append(" AND IDRapportoControlloTecnico IN ");
                strSql.Append("(" + listIDRapportiControlloTecnici + ")");
            }

            if (valoriCriticitaSelected != null)
            {
                for (int i = 0; i < valoriCriticitaSelected.Length; i++)
                {
                    if (valoriCriticitaSelected[i].ToString() == "0") //Osservazioni
                    {
                        strSql.Append(" AND Osservazioni IS NOT NULL");
                    }
                    else if (valoriCriticitaSelected[i].ToString() == "1") //Raccomandazioni
                    {
                        strSql.Append(" AND Raccomandazioni IS NOT NULL");
                    }
                    else if (valoriCriticitaSelected[i].ToString() == "2") //Prescrizioni
                    {
                        strSql.Append(" AND Prescrizioni IS NOT NULL");
                    }
                    else if (valoriCriticitaSelected[i].ToString() == "3") //Impianto può funzionare
                    {
                        strSql.Append(" AND fImpiantoFunzionante=0 ");
                    }
                }
            }

            if ((DataRegistrazioneDal != null) && (DataRegistrazioneDal.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataInserimento, 126) >= '");
                DateTime dataInserimentoDa = DateTime.Parse(DataRegistrazioneDal.ToString());
                string newDataInserimentoDa = dataInserimentoDa.ToString("yyyy") + "-" + dataInserimentoDa.ToString("MM") + "-" + dataInserimentoDa.ToString("dd");
                strSql.Append(newDataInserimentoDa);
                strSql.Append("'");
            }

            if ((DataRegistrazioneAl != null) && (DataRegistrazioneAl.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataInserimento, 126) <= '");
                DateTime dataInserimentoAl = DateTime.Parse(DataRegistrazioneAl.ToString());
                string newdataInserimentoeAl = dataInserimentoAl.ToString("yyyy") + "-" + dataInserimentoAl.ToString("MM") + "-" + dataInserimentoAl.ToString("dd");
                strSql.Append(newdataInserimentoeAl);
                strSql.Append("'");
            }

            if ((DataFirmaDal != null) && (DataFirmaDal.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataFirma, 126) >= '");
                DateTime dataFirmaDa = DateTime.Parse(DataFirmaDal.ToString());
                string newDataFirmaDa = dataFirmaDa.ToString("yyyy") + "-" + dataFirmaDa.ToString("MM") + "-" + dataFirmaDa.ToString("dd");
                strSql.Append(newDataFirmaDa);
                strSql.Append("'");
            }

            if ((DataFirmaAl != null) && (DataFirmaAl.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataFirma, 126) <= '");
                DateTime dataFirmaAl = DateTime.Parse(DataFirmaAl.ToString());
                string newdataFirmaAl = dataFirmaAl.ToString("yyyy") + "-" + dataFirmaAl.ToString("MM") + "-" + dataFirmaAl.ToString("dd");
                strSql.Append(newdataFirmaAl);
                strSql.Append("'");
            }

            if ((DataEsecuzioneVerificaDal != null) && (DataEsecuzioneVerificaDal.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataControllo, 126) >= '");
                DateTime dataControlloDa = DateTime.Parse(DataEsecuzioneVerificaDal.ToString());
                string newDataControlloDa = dataControlloDa.ToString("yyyy") + "-" + dataControlloDa.ToString("MM") + "-" + dataControlloDa.ToString("dd");
                strSql.Append(newDataControlloDa);
                strSql.Append("'");
            }

            if ((DataEsecuzioneVerificaAl != null) && (DataEsecuzioneVerificaAl.ToString() != ""))
            {
                strSql.Append(" AND convert(varchar(10), DataControllo, 126) <= '");
                DateTime dataControlloAl = DateTime.Parse(DataEsecuzioneVerificaAl.ToString());
                string newdataControlloeAl = dataControlloAl.ToString("yyyy") + "-" + dataControlloAl.ToString("MM") + "-" + dataControlloAl.ToString("dd");
                strSql.Append(newdataControlloeAl);
                strSql.Append("'");
            }

            if (iDCodiceCastale != null)
            {
                strSql.Append(" AND IDCodiceCatastale=" + iDCodiceCastale + "");
            }

            if ((foglio.ToString() != "") || (mappale.ToString() != "") || (subalterno.ToString() != "") || (identificativo.ToString() != ""))
            {
                strSql.Append(" AND IDLibrettoImpianto IN ");
                string sqlDatiCatastali = "";

                if (foglio.ToString() != "")
                {
                    sqlDatiCatastali += "AND Foglio = '" + foglio.ToString() + "'";
                }
                if (mappale.ToString() != "")
                {
                    sqlDatiCatastali += "AND Mappale = '" + mappale.ToString() + "'";
                }
                if (subalterno.ToString() != "")
                {
                    sqlDatiCatastali += "AND Subalterno = '" + subalterno.ToString() + "'";
                }

                strSql.Append("( SELECT IDLibrettoImpianto FROM LIM_LibrettiImpiantiDatiCatastali WHERE 1=1 " + sqlDatiCatastali + ")");
            }

            if (!string.IsNullOrEmpty(responsabile))
            {
                strSql.Append(string.Format(" AND ([NomeResponsabile] + ' '  + [CognomeResponsabile] LIKE '%{0}%' OR [RagioneSocialeResponsabile] LIKE '%{1}%') ", responsabile.Replace("'", ""), responsabile.Replace("'", "")));
            }


            UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);
            if (info.IDRuolo == 16)
            {
                strSql.Append(" AND IDCodiceCatastale IN ");
                strSql.Append("( SELECT IDCodiceCatastale FROM COM_CodiciCatastaliCompetenza WHERE IDSoggetto= " + info.IDSoggetto + ")");
            }

            strSql.Append(" ORDER BY DataControllo DESC");

            return UtilityApp.SanitizeInput(strSql.ToString(), SanitizeType.Select);
        }

        public static void SaveInsertDeleteDatiTrattamentoAcquaInvernale(
                int iDRapportoControlloTecnico,
                string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var trattamentiAttuali = db.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale.Where(i => i.IDRapportoControlloTecnico == iDRapportoControlloTecnico).ToList();

            foreach (var trattamenti in trattamentiAttuali)
            {
                if (!valoriSelected.Contains(trattamenti.IDTipologiaTrattamentoAcqua.ToString()))
                {
                    db.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale.Remove(trattamenti);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!trattamentiAttuali.Any(o => o.IDTipologiaTrattamentoAcqua.ToString() == valoriSelected[i]))
                {
                    db.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale.Add(new RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale() { IDTipologiaTrattamentoAcqua = int.Parse(valoriSelected[i]), IDRapportoControlloTecnico = iDRapportoControlloTecnico });
                }
            }

            db.SaveChanges();
        }

        public static void SaveInsertDeleteDatiTrattamentoAcquaAcs(
                int iDRapportoControlloTecnico,
                string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var trattamentiAttuali = db.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs.Where(i => i.IDRapportoControlloTecnico == iDRapportoControlloTecnico).ToList();

            foreach (var trattamenti in trattamentiAttuali)
            {
                if (!valoriSelected.Contains(trattamenti.IDTipologiaTrattamentoAcqua.ToString()))
                {
                    db.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs.Remove(trattamenti);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!trattamentiAttuali.Any(o => o.IDTipologiaTrattamentoAcqua.ToString() == valoriSelected[i]))
                {
                    db.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs.Add(new RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs() { IDTipologiaTrattamentoAcqua = int.Parse(valoriSelected[i]), IDRapportoControlloTecnico = iDRapportoControlloTecnico });
                }
            }

            db.SaveChanges();
        }

        public static void SaveInsertDeleteDatiCheckList(
                int iDRapportoControlloTecnico,
                string[] valoriSelected)
        {
            var db = DataLayer.Common.ApplicationContext.Current.Context;
            var checklistAttuali = db.RCT_RapportoDiControlloTecnicoBaseCheckList.Where(i => i.IDRapportoControlloTecnico == iDRapportoControlloTecnico).ToList();

            foreach (var checkList in checklistAttuali)
            {
                if (!valoriSelected.Contains(checkList.IDCheckList.ToString()))
                {
                    db.RCT_RapportoDiControlloTecnicoBaseCheckList.Remove(checkList);
                }
            }
            for (int i = 0; i < valoriSelected.Length; i++)
            {
                if (!checklistAttuali.Any(o => o.IDCheckList.ToString() == valoriSelected[i]))
                {
                    db.RCT_RapportoDiControlloTecnicoBaseCheckList.Add(new RCT_RapportoDiControlloTecnicoBaseCheckList() { IDCheckList = int.Parse(valoriSelected[i]), IDRapportoControlloTecnico = iDRapportoControlloTecnico });
                }
            }

            db.SaveChanges();
        }

        public static List<RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale> GetValoriRapportoControlloTrattamentoAcquaInvernale(int iDRapportoControlloTecnico)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoInvernale.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnico).OrderBy(s => s.IDTipologiaTrattamentoAcqua).ToList();

                return result;
            }
        }

        public static List<RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs> GetValoriRapportoControlloTrattamentoAcquaAcs(int iDRapportoControlloTecnico)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.RCT_RapportoDiControlloTecnicoBaseTrattamentoAcs.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnico).OrderBy(s => s.IDTipologiaTrattamentoAcqua).ToList();

                return result;
            }
        }

        public static List<RCT_RapportoDiControlloTecnicoBaseCheckList> GetValoriRapportoControlloCheckList(int iDRapportoControlloTecnico)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.RCT_RapportoDiControlloTecnicoBaseCheckList.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnico).OrderBy(s => s.IDCheckList).ToList();

                return result;
            }
        }

        public static List<RCT_FirmaDigitale> GetValoriFirmaDigitale(int iDRapportoControlloTecnico)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.RCT_FirmaDigitale.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnico).OrderBy(s => s.IDRapportoControlloTecnico).ToList();

                return result;
            }
        }

        public static int? SaveInsertDeleteDatiRapportiFirmaDigitale(int iDRapportoControlloTecnico,
                                                                     int iDSoggetto,
                                                                     int iDSoggettoDerived,
                                                                     int TipoFirma,
                                                                     string RdpPdf,
                                                                     DateTime dataFirma,
                                                                     string IpClient,
                                                                     string SignerQualifier,
                                                                     string SignerName,
                                                                     string SignerSurname,
                                                                     string SignerIdentifier,
                                                                     string SignerFullName,
                                                                     string SignerAuthority,
                                                                     string SignerCertificationAuthority,
                                                                     string SignerSerialNumber)
        {
            int? iDSoggettoInsert = null;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var firma = new RCT_FirmaDigitale();
                firma.IDRapportoControlloTecnico = iDRapportoControlloTecnico;
                firma.IDSoggetto = iDSoggetto;
                firma.IDSoggettoDerived = iDSoggettoDerived;
                firma.TipoFirma = TipoFirma;
                if (!string.IsNullOrEmpty(RdpPdf))
                {
                    firma.RdpPdf = RdpPdf;
                }
                else
                {
                    firma.RdpPdf = null;
                }
                firma.DataFirma = dataFirma;

                if (!string.IsNullOrEmpty(IpClient))
                {
                    firma.IPClient = IpClient;
                }
                else
                {
                    firma.IPClient = null;
                }
                if (!string.IsNullOrEmpty(SignerQualifier))
                {
                    firma.SignerQualifier = SignerQualifier;
                }
                else
                {
                    firma.SignerQualifier = null;
                }
                if (!string.IsNullOrEmpty(SignerName))
                {
                    firma.SignerName = SignerName;
                }
                else
                {
                    firma.SignerName = null;
                }
                if (!string.IsNullOrEmpty(SignerSurname))
                {
                    firma.SignerSurname = SignerSurname;
                }
                else
                {
                    firma.SignerSurname = null;
                }
                if (!string.IsNullOrEmpty(SignerIdentifier))
                {
                    firma.SignerIdentifier = SignerIdentifier;
                }
                else
                {
                    firma.SignerIdentifier = SignerIdentifier;
                }
                if (!string.IsNullOrEmpty(SignerFullName))
                {
                    firma.SignerFullName = SignerFullName;
                }
                else
                {
                    firma.SignerFullName = null;
                }
                if (!string.IsNullOrEmpty(SignerAuthority))
                {
                    firma.SignerAuthority = SignerAuthority;
                }
                else
                {
                    firma.SignerAuthority = SignerAuthority;
                }
                if (!string.IsNullOrEmpty(SignerCertificationAuthority))
                {
                    firma.SignerCertificationAuthority = SignerCertificationAuthority;
                }
                else
                {
                    firma.SignerCertificationAuthority = null;
                }
                if (!string.IsNullOrEmpty(SignerSerialNumber))
                {
                    firma.SignerSerialNumber = SignerSerialNumber;
                }
                else
                {
                    firma.SignerSerialNumber = null;
                }
                ctx.RCT_FirmaDigitale.Add(firma);

                try
                {
                    ctx.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }


                //try
                //{
                //    ctx.SaveChanges();
                //}
                //catch (Exception ex)
                //{

                //}

                return iDSoggettoInsert = firma.IDSoggetto;
            }
        }

        public static void ChangeStatoRct(int iDRapportoControlloTecnico)
        {
            try
            {
                using (var ctx = new CriterDataModel())
                {
                    var rapporto = ctx.RCT_RapportoDiControlloTecnicoBase.Where(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico).FirstOrDefault();
                    rapporto.IDStatoRapportoDiControllo = 2;
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void DeleteRct(long iDRapportoControlloTecnico)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var rapportoBase = ctx.RCT_RapportoDiControlloTecnicoBase.Find(iDRapportoControlloTecnico);
                if (rapportoBase.guidInteroImpianto != null)
                {
                    //Caso di Rapporto di controllo intero impianto
                    foreach (var rapporto in ctx.RCT_RapportoDiControlloTecnicoBase.Where(c => c.guidInteroImpianto == rapportoBase.guidInteroImpianto).ToList())
                    {
                        foreach (var bollino in ctx.RCT_BollinoCalorePulito.Where(c => c.IDRapportoControlloTecnico == rapporto.IDRapportoControlloTecnico).ToList())
                        {
                            bollino.IDRapportoControlloTecnico = null;
                        }
                        ctx.SaveChanges();

                        ctx.RCT_RapportoDiControlloTecnicoBase.Remove(rapporto);
                        ctx.SaveChanges();
                        //var rapportoInteroImpianto = ctx.RCT_RapportoDiControlloTecnicoBase.Find(rapportoBase.guidInteroImpianto);
                    }
                }
                else
                {
                    //Caso di Rapporto di controllo singolo
                    foreach (var bollino in ctx.RCT_BollinoCalorePulito.Where(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico).ToList())
                    {
                        bollino.IDRapportoControlloTecnico = null;
                    }
                    ctx.SaveChanges();

                    ctx.RCT_RapportoDiControlloTecnicoBase.Remove(rapportoBase);
                    ctx.SaveChanges();
                }
            }
        }

        public static void AnnullaRapportoControllo(int iDRapportoControlloTecnico)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var rapporto = ctx.RCT_RapportoDiControlloTecnicoBase.FirstOrDefault(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico);
                rapporto.IDStatoRapportoDiControllo = 4;
                rapporto.DataAnnullamento = DateTime.Now;
                ctx.SaveChanges();
            }
        }

        public static void AnnullaRapportoControlloMassive(int IDTargaturaImpianto)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                ctx.RCT_RapportoDiControlloTecnicoBase.Where(c => c.IDTargaturaImpianto == IDTargaturaImpianto).Update(c => new RCT_RapportoDiControlloTecnicoBase()
                {
                    IDStatoRapportoDiControllo = 4,
                    DataAnnullamento = DateTime.Now
                });


                //var rapporto = new RCT_RapportoDiControlloTecnicoBase();
                //rapporto = ctx.RCT_RapportoDiControlloTecnicoBase.FirstOrDefault(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico);
                //rapporto.IDStatoRapportoDiControllo = 4;
                //rapporto.DataAnnullamento = DateTime.Now;
                //ctx.SaveChanges();
            }
        }

        public static void AnnullaRapportoControlloInAttesaDiFirma(int iDRapportoControlloTecnico)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var rapportoBase = ctx.RCT_RapportoDiControlloTecnicoBase.Find(iDRapportoControlloTecnico);
                if (rapportoBase.guidInteroImpianto != null)
                {
                    //Caso di Rapporto di controllo intero impianto
                    //Non sgancio i bollini
                }
                else
                {
                    //Caso di Rapporto di controllo singolo
                    var bollini = ctx.RCT_BollinoCalorePulito.Where(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico).ToList();

                    if (bollini.Count > 0)
                    {
                        bollini.ForEach(b =>
                        {
                            b.IDRapportoControlloTecnico = null;
                            b.DataOraUtilizzo = null;
                            b.IDSoggettoDerived = null;
                            b.IDSoggettoUtilizzatore = null;
                        });
                        ctx.SaveChanges();
                    }
                }

                var rapporto = new RCT_RapportoDiControlloTecnicoBase();
                rapporto = ctx.RCT_RapportoDiControlloTecnicoBase.FirstOrDefault(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico);
                rapporto.IDStatoRapportoDiControllo = 1;
                ctx.SaveChanges();
            }
        }

        #region Raccomandazioni/Prescrizioni
        public static List<RCT_RaccomandazioniPrescrizioni> GetValoriRCTRaccomandazioniPrescrizioni(int iDRapportoControlloTecnico, int? iDTipologiaRaccomandazionePrescrizioneRct, string type)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var result = ctx.RCT_RaccomandazioniPrescrizioni.AsNoTracking().AsQueryable();
                if (type == "Raccomandazioni")
                {
                    result = result.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnico && a.IDTipologiaRaccomandazione != null);
                }
                else if (type == "Prescrizioni")
                {
                    result = result.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnico && a.IDTipologiaPrescrizione != null);
                }

                if (iDTipologiaRaccomandazionePrescrizioneRct != null)
                {
                    result = result.Where(a => a.IDTipologiaRaccomandazionePrescrizioneRct == iDTipologiaRaccomandazionePrescrizioneRct);
                }

                return result.OrderBy(s => s.IDRaccomandazioniPrescrizioni).ToList();
            }
        }

        public static void SaveInsertDeleteDatiRaccomandazioniPrescrizioni(
                int iDRapportoControlloTecnico,
                int iDTipologiaRaccomandazionePrescrizioneRct,
                object[] valoriSelected, string type)
        {
            //var db = DataLayer.Common.ApplicationContext.Current.Context;
            using (CriterDataModel ctx = new CriterDataModel())
            {
                if (type == "Raccomandazioni")
                {
                    var raccomandazioniAttuali = ctx.RCT_RaccomandazioniPrescrizioni.Where(i => i.IDRapportoControlloTecnico == iDRapportoControlloTecnico & i.IDTipologiaRaccomandazionePrescrizioneRct == iDTipologiaRaccomandazionePrescrizioneRct & i.IDTipologiaRaccomandazione != null).ToList();
                    foreach (var raccomandazioni in raccomandazioniAttuali)
                    {
                        if (!valoriSelected.Contains(raccomandazioni.IDTipologiaRaccomandazione.ToString()))
                        {
                            ctx.RCT_RaccomandazioniPrescrizioni.Remove(raccomandazioni);
                        }
                    }
                    for (int i = 0; i < valoriSelected.Length; i++)
                    {
                        if (!raccomandazioniAttuali.Any(o => o.IDTipologiaRaccomandazione.ToString() == valoriSelected[i].ToString()))
                        {
                            ctx.RCT_RaccomandazioniPrescrizioni.Add(new RCT_RaccomandazioniPrescrizioni() { IDTipologiaRaccomandazione = int.Parse(valoriSelected[i].ToString()), IDRapportoControlloTecnico = iDRapportoControlloTecnico, IDTipologiaRaccomandazionePrescrizioneRct = iDTipologiaRaccomandazionePrescrizioneRct });
                        }
                    }
                }
                else if (type == "Prescrizioni")
                {
                    var prescrizioniAttuali = ctx.RCT_RaccomandazioniPrescrizioni.Where(i => i.IDRapportoControlloTecnico == iDRapportoControlloTecnico & i.IDTipologiaRaccomandazionePrescrizioneRct == iDTipologiaRaccomandazionePrescrizioneRct & i.IDTipologiaPrescrizione != null).ToList();
                    foreach (var prescrizioni in prescrizioniAttuali)
                    {
                        if (!valoriSelected.Contains(prescrizioni.IDTipologiaPrescrizione.ToString()))
                        {
                            ctx.RCT_RaccomandazioniPrescrizioni.Remove(prescrizioni);
                        }
                    }
                    for (int i = 0; i < valoriSelected.Length; i++)
                    {
                        if (!prescrizioniAttuali.Any(o => o.IDTipologiaPrescrizione.ToString() == valoriSelected[i].ToString()))
                        {
                            ctx.RCT_RaccomandazioniPrescrizioni.Add(new RCT_RaccomandazioniPrescrizioni() { IDTipologiaPrescrizione = int.Parse(valoriSelected[i].ToString()), IDRapportoControlloTecnico = iDRapportoControlloTecnico, IDTipologiaRaccomandazionePrescrizioneRct = iDTipologiaRaccomandazionePrescrizioneRct });
                        }
                    }
                }

                ctx.SaveChanges();
            }
        }

        public static void SetFieldsRaccomandazioniPrescrizioni(int iDRapportoControlloTecnico)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var raccomandazioni = (from a in ctx.RCT_RaccomandazioniPrescrizioni
                                       join c in ctx.SYS_RCTTipologiaRaccomandazione on a.IDTipologiaRaccomandazione equals c.IDTipologiaRaccomandazione
                                       where a.IDRapportoControlloTecnico == iDRapportoControlloTecnico
                                       select c.Raccomandazione
                                      ).ToList();

                var prescrizioni = (from a in ctx.RCT_RaccomandazioniPrescrizioni
                                    join c in ctx.SYS_RCTTipologiaPrescrizione on a.IDTipologiaPrescrizione equals c.IDTipologiaPrescrizione
                                    where a.IDRapportoControlloTecnico == iDRapportoControlloTecnico
                                    select c.Prescrizione
                                   ).ToList();

                var rapporto = (from a in ctx.RCT_RapportoDiControlloTecnicoBase
                                where a.IDRapportoControlloTecnico == iDRapportoControlloTecnico
                                select a
                                   ).FirstOrDefault();

                string tempRaccomandazioni = string.Empty;
                foreach (var r in raccomandazioni)
                {
                    tempRaccomandazioni += r + "\n\n";
                }

                string tempPrescrizioni = string.Empty;
                foreach (var p in prescrizioni)
                {
                    tempPrescrizioni += p + "\n\n";
                }

                string raccomandazioniLibere = string.Empty;
                if (!string.IsNullOrEmpty(rapporto.Raccomandazioni) && (!string.IsNullOrEmpty(tempRaccomandazioni)))
                {
                    string raccomandazioniLiberetemp = rapporto.Raccomandazioni;
                    foreach (var r in raccomandazioni)
                    {
                        raccomandazioniLiberetemp = raccomandazioniLiberetemp.Replace(r, "");
                    }
                    raccomandazioniLibere = raccomandazioniLiberetemp.Replace("\n", "");
                }

                string prescrizioniLibere = string.Empty;
                if (!string.IsNullOrEmpty(rapporto.Prescrizioni) && (!string.IsNullOrEmpty(tempPrescrizioni)))
                {
                    string prescrizioniLiberetemp = rapporto.Prescrizioni;
                    foreach (var p in prescrizioni)
                    {
                        prescrizioniLiberetemp = prescrizioniLiberetemp.Replace(p, "");
                    }
                    prescrizioniLibere = prescrizioniLiberetemp.Replace("\n", "");
                }

                var rapportoRP = new RCT_RapportoDiControlloTecnicoBase();
                rapportoRP = ctx.RCT_RapportoDiControlloTecnicoBase.FirstOrDefault(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico);
                if (!string.IsNullOrEmpty(raccomandazioniLibere.Replace("\n", "")))
                {
                    rapportoRP.Raccomandazioni = raccomandazioniLibere + "\n\n" + tempRaccomandazioni;
                }
                else
                {
                    if (!string.IsNullOrEmpty(tempRaccomandazioni))
                    {
                        rapportoRP.Raccomandazioni = tempRaccomandazioni;
                    }
                    //else
                    //{
                    //    rapportoRP.Raccomandazioni = null;
                    //}
                }
                if (!string.IsNullOrEmpty(prescrizioniLibere.Replace("\n", "")))
                {
                    rapportoRP.Prescrizioni = prescrizioniLibere + "\n\n" + tempPrescrizioni;
                }
                else
                {
                    if (!string.IsNullOrEmpty(tempPrescrizioni))
                    {
                        rapportoRP.Prescrizioni = tempPrescrizioni;
                    }
                    //else
                    //{
                    //    rapportoRP.Prescrizioni = null;
                    //}
                }

                ctx.SaveChanges();
            }
        }

        #endregion

        #region Cache
        //public static bool fRaccomandazioniPrescrizioniInCache(int iDRapportoControlloTecnico, string type)
        //{
        //    bool fItemsInCache = false;

        //    string keyRaccomandazioniPrescrizioniInCache = "key" + type + "_" + iDRapportoControlloTecnico;
        //    var itemsInCache = HttpContext.Current.Cache.GetEnumerator();
        //    while (itemsInCache.MoveNext())
        //    {
        //        if (itemsInCache.Key.ToString() == keyRaccomandazioniPrescrizioniInCache)
        //        {
        //            fItemsInCache = true;
        //            HttpContext.Current.Cache.Insert(keyRaccomandazioniPrescrizioniInCache, true, null, DateTime.Now.AddHours(2), System.Web.Caching.Cache.NoSlidingExpiration);
        //            break;
        //        }
        //    }

        //    return fItemsInCache;
        //}


        ////Cache.Insert("Website", strName, new CacheDependency(Sever.MapPath("Website.txt") DateTime.Now.Addminutes(5), TimeSpan.Zero);


        //public static List<RCT_RaccomandazioniPrescrizioni> GetValoriRCTRaccomandazioniPrescrizioniCache(int iDRapportoControlloTecnico, int? iDTipologiaRaccomandazionePrescrizioneRct, string type)
        //{
        //    bool fItemsInCache = fRaccomandazioniPrescrizioniInCache(iDRapportoControlloTecnico, type);
        //    string keyItemsInCache = "key" + type + "_" + iDRapportoControlloTecnico;

        //    List<RCT_RaccomandazioniPrescrizioni> result = new List<RCT_RaccomandazioniPrescrizioni>();
        //    if (fItemsInCache)
        //    {
        //        if (type == "Raccomandazioni")
        //        {
        //            //TODO: devo tornare la lista dalla cache
        //            var raccomandazioniFromCache = HttpContext.Current.Cache.GetEnumerator();
        //            while (raccomandazioniFromCache.MoveNext())
        //            {
        //                if (raccomandazioniFromCache.Key.ToString().Contains("key" + type + "_" + iDRapportoControlloTecnico + "_"))
        //                {
        //                    result.Add(HttpContext.Current.Cache.Get("key" + type + "_" + iDRapportoControlloTecnico + "_").ToString());
        //                }
        //            }
        //        }
        //        else if (type == "Prescrizioni")
        //        {

        //        }
        //    }
        //    else
        //    {
        //        using (CriterDataModel ctx = new CriterDataModel())
        //        {
        //            if (type == "Raccomandazioni")
        //            {
        //                result = ctx.RCT_RaccomandazioniPrescrizioni.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnico && a.IDTipologiaRaccomandazione != null).OrderBy(s => s.IDRaccomandazioniPrescrizioni).ToList();
        //                foreach (var item in result)
        //                {
        //                    string keyRaccomandazione = "key"+ type +"_" + iDRapportoControlloTecnico + "_" + item.IDTipologiaRaccomandazione;
        //                    HttpContext.Current.Cache.Insert(keyRaccomandazione, item.IDTipologiaRaccomandazione, null, DateTime.Now.AddHours(2), System.Web.Caching.Cache.NoSlidingExpiration);
        //                }

        //                HttpContext.Current.Cache.Insert(keyItemsInCache, true, null, DateTime.Now.AddHours(2), System.Web.Caching.Cache.NoSlidingExpiration);
        //            }
        //            else if (type == "Prescrizioni")
        //            {
        //                result = ctx.RCT_RaccomandazioniPrescrizioni.Where(a => a.IDRapportoControlloTecnico == iDRapportoControlloTecnico && a.IDTipologiaPrescrizione != null).OrderBy(s => s.IDRaccomandazioniPrescrizioni).ToList();
        //                foreach (var item in result)
        //                {
        //                    string keyRaccomandazione = "key" + type + "_" + iDRapportoControlloTecnico + "_" + item.IDTipologiaPrescrizione;
        //                    HttpContext.Current.Cache.Insert(keyRaccomandazione, item.IDTipologiaPrescrizione, null, DateTime.Now.AddHours(2), System.Web.Caching.Cache.NoSlidingExpiration);
        //                }

        //                HttpContext.Current.Cache.Insert(keyItemsInCache, true, null, DateTime.Now.AddHours(2), System.Web.Caching.Cache.NoSlidingExpiration);
        //            }
        //        }
        //    }

        //    return result;
        //}

        //public static void SaveInsertDeleteDatiRaccomandazioniPrescrizioniCache(
        //        int iDRapportoControlloTecnico,
        //        int iDTipologiaRaccomandazionePrescrizioneRct,
        //        object[] valoriSelected, string type)
        //{
        //    using (CriterDataModel ctx = new CriterDataModel())
        //    {
        //        if (type == "Raccomandazioni")
        //        {
        //            var raccomandazioniAttuali = ctx.RCT_RaccomandazioniPrescrizioni.Where(i => i.IDRapportoControlloTecnico == iDRapportoControlloTecnico & i.IDTipologiaRaccomandazionePrescrizioneRct == iDTipologiaRaccomandazionePrescrizioneRct & i.IDTipologiaRaccomandazione != null).ToList();
        //            foreach (var raccomandazioni in raccomandazioniAttuali)
        //            {
        //                if (!valoriSelected.Contains(raccomandazioni.IDTipologiaRaccomandazione.ToString()))
        //                {
        //                    ctx.RCT_RaccomandazioniPrescrizioni.Remove(raccomandazioni);
        //                }
        //            }
        //            for (int i = 0; i < valoriSelected.Length; i++)
        //            {
        //                if (!raccomandazioniAttuali.Any(o => o.IDTipologiaRaccomandazione.ToString() == valoriSelected[i].ToString()))
        //                {
        //                    ctx.RCT_RaccomandazioniPrescrizioni.Add(new RCT_RaccomandazioniPrescrizioni() { IDTipologiaRaccomandazione = int.Parse(valoriSelected[i].ToString()), IDRapportoControlloTecnico = iDRapportoControlloTecnico, IDTipologiaRaccomandazionePrescrizioneRct = iDTipologiaRaccomandazionePrescrizioneRct });
        //                }
        //            }
        //        }
        //        else if (type == "Prescrizioni")
        //        {
        //            var prescrizioniAttuali = ctx.RCT_RaccomandazioniPrescrizioni.Where(i => i.IDRapportoControlloTecnico == iDRapportoControlloTecnico & i.IDTipologiaRaccomandazionePrescrizioneRct == iDTipologiaRaccomandazionePrescrizioneRct & i.IDTipologiaPrescrizione != null).ToList();
        //            foreach (var prescrizioni in prescrizioniAttuali)
        //            {
        //                if (!valoriSelected.Contains(prescrizioni.IDTipologiaPrescrizione.ToString()))
        //                {
        //                    ctx.RCT_RaccomandazioniPrescrizioni.Remove(prescrizioni);
        //                }
        //            }
        //            for (int i = 0; i < valoriSelected.Length; i++)
        //            {
        //                if (!prescrizioniAttuali.Any(o => o.IDTipologiaPrescrizione.ToString() == valoriSelected[i].ToString()))
        //                {
        //                    ctx.RCT_RaccomandazioniPrescrizioni.Add(new RCT_RaccomandazioniPrescrizioni() { IDTipologiaPrescrizione = int.Parse(valoriSelected[i].ToString()), IDRapportoControlloTecnico = iDRapportoControlloTecnico, IDTipologiaRaccomandazionePrescrizioneRct = iDTipologiaRaccomandazionePrescrizioneRct });
        //                }
        //            }
        //        }

        //        ctx.SaveChanges();
        //    }
        //}
        #endregion


        public static decimal? GetRendimentoMinimoDiLegge(object iDTipologiaGeneratori, DateTime? dataInstallazione, decimal? potenzaUtileNominale)
        {
            decimal? RendimentoMinimo = null;

            if ((iDTipologiaGeneratori != null) && (dataInstallazione != null) && (potenzaUtileNominale != null))
            {
                int? iDTipologiaGeneratoriTermici = int.Parse(iDTipologiaGeneratori.ToString());

                decimal potenzaLogaritmica = 0;
                if (potenzaUtileNominale >= 400)
                {
                    potenzaLogaritmica = (decimal)Math.Log10((double)400);
                }
                else
                {
                    potenzaLogaritmica = (decimal)Math.Log10((double)potenzaUtileNominale);
                }

                if ((dataInstallazione < DateTime.Parse("29/10/1993")) && ((iDTipologiaGeneratoriTermici == 1) || (iDTipologiaGeneratoriTermici == 2) || (iDTipologiaGeneratoriTermici == 3)))
                {
                    RendimentoMinimo = 82 + 2 * potenzaLogaritmica;
                }
                else if ((dataInstallazione >= DateTime.Parse("29/10/1993")) && ((dataInstallazione <= DateTime.Parse("31/12/1997"))) && ((iDTipologiaGeneratoriTermici == 1) || (iDTipologiaGeneratoriTermici == 2) || (iDTipologiaGeneratoriTermici == 3)))
                {
                    RendimentoMinimo = 84 + 2 * potenzaLogaritmica;
                }
                else if ((dataInstallazione >= DateTime.Parse("01/01/1998")) && ((dataInstallazione <= DateTime.Parse("07/10/2005"))) && iDTipologiaGeneratoriTermici == 1)
                {
                    RendimentoMinimo = 84 + 2 * potenzaLogaritmica;
                }
                else if ((dataInstallazione >= DateTime.Parse("01/01/1998")) && ((dataInstallazione <= DateTime.Parse("07/10/2005"))) && iDTipologiaGeneratoriTermici == 2)
                {
                    RendimentoMinimo = 87.5m + 1.5m * potenzaLogaritmica;
                }
                else if ((dataInstallazione >= DateTime.Parse("01/01/1998")) && ((dataInstallazione <= DateTime.Parse("07/10/2005"))) && iDTipologiaGeneratoriTermici == 3)
                {
                    RendimentoMinimo = 91 + 1 * potenzaLogaritmica;
                }
                else if ((dataInstallazione >= DateTime.Parse("08/10/2005") && ((iDTipologiaGeneratoriTermici == 1) || (iDTipologiaGeneratoriTermici == 2))))
                {
                    RendimentoMinimo = 87 + 2 * potenzaLogaritmica;
                }
                else if ((dataInstallazione >= DateTime.Parse("08/10/2005") && ((iDTipologiaGeneratoriTermici == 3))))
                {
                    RendimentoMinimo = 89 + 2 * potenzaLogaritmica;
                }
                else if ((dataInstallazione < DateTime.Parse("29/10/1993")) && ((iDTipologiaGeneratoriTermici == 4)))
                {
                    RendimentoMinimo = 77 + 2 * potenzaLogaritmica;
                }
                else if ((dataInstallazione >= DateTime.Parse("29/10/1993")) && ((iDTipologiaGeneratoriTermici == 4)))
                {
                    RendimentoMinimo = 80 + 2 * potenzaLogaritmica;
                }
            }

            return RendimentoMinimo;
        }

        public static bool GetfUnitaImmobiliare(int iDRapportoDiControlloTecnico)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var rapporto = (from r in ctx.RCT_RapportoDiControlloTecnicoBase
                                join l in ctx.LIM_LibrettiImpianti on r.IDLibrettoImpianto equals l.IDLibrettoImpianto
                                where (r.IDRapportoControlloTecnico == iDRapportoDiControlloTecnico)
                                select new
                                {
                                    fUnitaImmobiliare = l.fUnitaImmobiliare
                                }
                             ).FirstOrDefault();

                return rapporto.fUnitaImmobiliare;
            }
        }

        public static List<V_RCT_RapportiControlloTecnico> GetValoriRapportoControllo(int iDTargaturaImpianto, string prefisso, int codiceProgressivo)
        {
            using (var ctx = new CriterDataModel())
            {
                var result = ctx.V_RCT_RapportiControlloTecnico.Where(a => a.IDTargaturaImpianto == iDTargaturaImpianto && a.Prefisso == prefisso && a.CodiceProgressivo == codiceProgressivo && a.IDStatoRapportoDiControllo == 2).OrderBy(s => s.IDRapportoControlloTecnico).ToList();

                return result;
            }
        }

        public static decimal? GetPotenzaTermicaUtileNominaleImpiantoFromCodiceTargatura(object codiceTargatura)
        {
            decimal? potenzaTermicaUtileNominale = null;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var query = (from LIM_LibrettiImpianti in ctx.LIM_LibrettiImpianti
                             join LIM_TargatureImpianti in ctx.LIM_TargatureImpianti on new { IDTargaturaImpianto = (int)LIM_LibrettiImpianti.IDTargaturaImpianto } equals new { IDTargaturaImpianto = LIM_TargatureImpianti.IDTargaturaImpianto }
                             join LIM_LibrettiImpiantiGruppiTermici in ctx.LIM_LibrettiImpiantiGruppiTermici on LIM_LibrettiImpianti.IDLibrettoImpianto equals LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpianto
                             join SYS_TipologiaCombustibile in ctx.SYS_TipologiaCombustibile on new { IDTipologiaCombustibile = (int)LIM_LibrettiImpiantiGruppiTermici.IDTipologiaCombustibile } equals new { IDTipologiaCombustibile = SYS_TipologiaCombustibile.IDTipologiaCombustibile }
                             where
                               LIM_LibrettiImpianti.fAttivo == true &&
                               LIM_LibrettiImpiantiGruppiTermici.fAttivo == true &&
                               LIM_LibrettiImpiantiGruppiTermici.fDismesso == false &&
                               //SYS_TipologiaCombustibile.Biomassa == false &&
                               (LIM_LibrettiImpiantiGruppiTermici.IDTipologiaCombustibile == 2 ||
                                LIM_LibrettiImpiantiGruppiTermici.IDTipologiaCombustibile == 3 ||
                                LIM_LibrettiImpiantiGruppiTermici.IDTipologiaCombustibile == 4 ||
                                LIM_LibrettiImpiantiGruppiTermici.IDTipologiaCombustibile == 5 ||
                                LIM_LibrettiImpiantiGruppiTermici.IDTipologiaCombustibile == 10 ||
                                LIM_LibrettiImpiantiGruppiTermici.IDTipologiaCombustibile == 11 ||
                                LIM_LibrettiImpiantiGruppiTermici.IDTipologiaCombustibile == 12 ||
                                LIM_LibrettiImpiantiGruppiTermici.IDTipologiaCombustibile == 13 ||
                                LIM_LibrettiImpiantiGruppiTermici.IDTipologiaCombustibile == 14 ||
                                LIM_LibrettiImpiantiGruppiTermici.IDTipologiaCombustibile == 15
                                )
                             group new { LIM_TargatureImpianti, LIM_LibrettiImpiantiGruppiTermici } by new
                             {
                                 LIM_TargatureImpianti.CodiceTargatura
                             } into g
                             where g.Key.CodiceTargatura == codiceTargatura.ToString()
                             select new
                             {
                                 g.Key.CodiceTargatura,
                                 PotenzaTermicaUtileNominaleImpianto = g.Sum(p => ((System.Decimal?)p.LIM_LibrettiImpiantiGruppiTermici.PotenzaTermicaUtileNominaleKw ?? (System.Decimal?)0))
                             }
                            ).FirstOrDefault();

                if (query != null)
                {
                    potenzaTermicaUtileNominale = query.PotenzaTermicaUtileNominaleImpianto;
                }

                return potenzaTermicaUtileNominale;
            }
        }

        public static decimal? GetPotenzaTermicaUtileNominaleImpianto(object guidInteroImpianto)
        {
            decimal? potenzaTermicaUtileNominale = null;

            using (CriterDataModel ctx = new CriterDataModel())
            {
                var query = (from RCT_RapportoDiControlloTecnicoBase in ctx.RCT_RapportoDiControlloTecnicoBase
                             join RCT_RapportoDiControlloTecnicoGT in ctx.RCT_RapportoDiControlloTecnicoGT on new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoBase.IDRapportoControlloTecnico } equals new { IDRapportoControlloTecnico = RCT_RapportoDiControlloTecnicoGT.Id }
                             join LIM_LibrettiImpiantiGruppiTermici in ctx.LIM_LibrettiImpiantiGruppiTermici on new { IDLIM_LibrettiImpiantiGruppitermici = (int)RCT_RapportoDiControlloTecnicoGT.IDLIM_LibrettiImpiantiGruppitermici } equals new { IDLIM_LibrettiImpiantiGruppitermici = LIM_LibrettiImpiantiGruppiTermici.IDLibrettoImpiantoGruppoTermico }
                             group new { RCT_RapportoDiControlloTecnicoBase, LIM_LibrettiImpiantiGruppiTermici } by new
                             {
                                 RCT_RapportoDiControlloTecnicoBase.guidInteroImpianto
                             } into g
                             where g.Key.guidInteroImpianto == guidInteroImpianto.ToString()
                             select new
                             {
                                 g.Key.guidInteroImpianto,
                                 PotenzaTermicaUtileNominaleImpianto = g.Sum(p => ((decimal?)p.LIM_LibrettiImpiantiGruppiTermici.PotenzaTermicaUtileNominaleKw ?? (decimal?)0))
                             }
                                             ).FirstOrDefault();

                if (query != null)
                {
                    potenzaTermicaUtileNominale = query.PotenzaTermicaUtileNominaleImpianto;
                }

                return potenzaTermicaUtileNominale;
            }
        }

        public static bool GetRctDefinitiviInAttesaDiFirmaInteroImpianto(string guidInteroImpianto)
        {
            bool fInteroImpianto = false;
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var rctStatus = new List<int> { 2, 3 };
                fInteroImpianto = ctx.RCT_RapportoDiControlloTecnicoBase.Where(c => c.guidInteroImpianto == guidInteroImpianto && rctStatus.Contains(c.IDStatoRapportoDiControllo)).ToList().Any();
            }

            return fInteroImpianto;
        }

        public static void SetDataControlloInteroImpianto(long iDRapportoControlloTecnico, DateTime dataControllo)
        {
            using (var ctx = new CriterDataModel())
            {
                var rapporto = ctx.RCT_RapportoDiControlloTecnicoBase.Where(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico).FirstOrDefault();
                rapporto.DataControllo = dataControllo;

                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
        }

        

    }
}
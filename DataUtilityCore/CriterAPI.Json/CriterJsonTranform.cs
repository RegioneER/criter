using Criter.Rapporti;
using CriterAPI.DataSource;
using System;
using System.Collections.Generic;

namespace CriterAPI.Json
{
    public class CriterJsonTranform
    {
        public static List<string> J_RCTGT2TXT(POMJ_RapportoControlloTecnico_GT rapportoTecnico)
        {
            List<String> txtToSign = new List<string>();
            LookUpHelper helper = new LookUpHelper();

            txtToSign.Add("RAPPORTO DI CONTROLLO TECNICO TIPO 1 (GRUPPI TERMICI)");
            txtToSign.Add("Timestamp: " + DateTime.Now.ToLongTimeString());
            txtToSign.Add("-----------------------------------------------------");

            #region rapporto controllo tecnico base
            txtToSign.Add("TipologiaControllo : " + helper.TipologiaControllo(rapportoTecnico.TipologiaControllo));
            txtToSign.Add("StatoRapportoDiControllo : " + helper.StatoRapportoDiControllo(rapportoTecnico.StatoRapportoDiControllo));
            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("Codice Targatura Impianto  ");
            if (rapportoTecnico.CodiceTargaturaImpianto != null)
            {
                txtToSign.Add("CodiceTargatura : " + rapportoTecnico.CodiceTargaturaImpianto.CodiceTargatura);
                //txtToSign.Add("CodiceRandom : " + rapportoTecnico.CodiceTargaturaImpianto.CodiceRandom);
                //txtToSign.Add("Anno : " + rapportoTecnico.CodiceTargaturaImpianto.Anno);
            }
            else
            {
                txtToSign.Add("CodiceTargatura : " + "");
                //txtToSign.Add("CodiceRandom : " + "");
                //txtToSign.Add("Anno : " + "");
            }

           
            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("PotenzaTermicaNominaleTotaleMax : " + rapportoTecnico.PotenzaTermicaNominaleTotaleMax);
            txtToSign.Add("Comune : " + rapportoTecnico.Comune);
            txtToSign.Add("Indirizzo : " + rapportoTecnico.Indirizzo);
            txtToSign.Add("Civico : " + rapportoTecnico.Civico);
            txtToSign.Add("Palazzo : " + rapportoTecnico.Palazzo);
            txtToSign.Add("Scala : " + rapportoTecnico.Scala);
            txtToSign.Add("Interno : " + rapportoTecnico.Interno);
            txtToSign.Add("TipologiaResponsabile : " + helper.TipologiaResponsabile(rapportoTecnico.TipologiaResponsabile));
            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("Responsabile Impianto ");
            if (rapportoTecnico.ResponsabileImpianto.TipoSoggetto != null)
            {
                txtToSign.Add("TipoSoggetto : " + helper.TipoSoggetto(rapportoTecnico.ResponsabileImpianto.TipoSoggetto));
            }
            txtToSign.Add("NomeAzienda : " + rapportoTecnico.ResponsabileImpianto.NomeAzienda);
            txtToSign.Add("IndirizzoSedeLegale : " + rapportoTecnico.ResponsabileImpianto.IndirizzoSedeLegale);
            txtToSign.Add("CapSedeLegale : " + rapportoTecnico.ResponsabileImpianto.CapSedeLegale);
            txtToSign.Add("CittaSedeLegale : " + rapportoTecnico.ResponsabileImpianto.CittaSedeLegale);
            txtToSign.Add("NumeroCivicoSedeLegale : " + rapportoTecnico.ResponsabileImpianto.NumeroCivicoSedeLegale);
            txtToSign.Add("ProvinciaSedeLegale : " + rapportoTecnico.ResponsabileImpianto.ProvinciaSedeLegale);
            txtToSign.Add("Nome : " + rapportoTecnico.ResponsabileImpianto.Nome);
            txtToSign.Add("Cognome : " + rapportoTecnico.ResponsabileImpianto.Cognome);
            txtToSign.Add("Indirizzo : " + rapportoTecnico.ResponsabileImpianto.Indirizzo);
            txtToSign.Add("Civico : " + rapportoTecnico.ResponsabileImpianto.Civico);
            txtToSign.Add("CodiceCatastaleComune : " + rapportoTecnico.ResponsabileImpianto.CodiceCatastaleComune);
            txtToSign.Add("Email : " + rapportoTecnico.ResponsabileImpianto.Email);
            txtToSign.Add("EmailPec : " + rapportoTecnico.ResponsabileImpianto.EmailPec);
            txtToSign.Add("PartitaIVA : " + rapportoTecnico.ResponsabileImpianto.PartitaIVA);
            txtToSign.Add("CodiceFiscale : " + rapportoTecnico.ResponsabileImpianto.CodiceFiscale);
            txtToSign.Add("NumeroIscrizioneCCIAA : " + rapportoTecnico.ResponsabileImpianto.NumeroIscrizioneCCIAA);
            txtToSign.Add("ProvinciaIscrizioneCCIAA : " + rapportoTecnico.ResponsabileImpianto.ProvinciaIscrizioneCCIAA);
            
            
            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("TerzoResponsabile  ");
            if (rapportoTecnico.TerzoResponsabile.TipoSoggetto != null)
            {
                txtToSign.Add("TipoSoggetto : " + helper.TipoSoggetto(rapportoTecnico.TerzoResponsabile.TipoSoggetto));
            }
            txtToSign.Add("NomeAzienda : " + rapportoTecnico.TerzoResponsabile.NomeAzienda);
            txtToSign.Add("IndirizzoSedeLegale : " + rapportoTecnico.TerzoResponsabile.IndirizzoSedeLegale);
            txtToSign.Add("CapSedeLegale : " + rapportoTecnico.TerzoResponsabile.CapSedeLegale);
            txtToSign.Add("CittaSedeLegale : " + rapportoTecnico.TerzoResponsabile.CittaSedeLegale);
            txtToSign.Add("NumeroCivicoSedeLegale : " + rapportoTecnico.TerzoResponsabile.NumeroCivicoSedeLegale);
            txtToSign.Add("ProvinciaSedeLegale : " + rapportoTecnico.TerzoResponsabile.ProvinciaSedeLegale);
            txtToSign.Add("Nome : " + rapportoTecnico.TerzoResponsabile.Nome);
            txtToSign.Add("Cognome : " + rapportoTecnico.TerzoResponsabile.Cognome);
            txtToSign.Add("Indirizzo : " + rapportoTecnico.TerzoResponsabile.Indirizzo);
            txtToSign.Add("Civico : " + rapportoTecnico.TerzoResponsabile.Civico);
            txtToSign.Add("CodiceCatastaleComune : " + rapportoTecnico.TerzoResponsabile.CodiceCatastaleComune);
            txtToSign.Add("Email : " + rapportoTecnico.TerzoResponsabile.Email);
            txtToSign.Add("EmailPec : " + rapportoTecnico.TerzoResponsabile.EmailPec);
            txtToSign.Add("PartitaIVA : " + rapportoTecnico.TerzoResponsabile.PartitaIVA);
            txtToSign.Add("CodiceFiscale : " + rapportoTecnico.TerzoResponsabile.CodiceFiscale);
            txtToSign.Add("NumeroIscrizioneCCIAA : " + rapportoTecnico.TerzoResponsabile.NumeroIscrizioneCCIAA);
            txtToSign.Add("ProvinciaIscrizioneCCIAA : " + rapportoTecnico.TerzoResponsabile.ProvinciaIscrizioneCCIAA);
            
            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("Impresa Manutentrice ");
            txtToSign.Add("TipoSoggetto : " + rapportoTecnico.ImpresaManutentrice);
            txtToSign.Add("NomeAzienda : " + rapportoTecnico.ImpresaManutentrice.NomeAzienda);
            txtToSign.Add("IndirizzoSedeLegale : " + rapportoTecnico.ImpresaManutentrice.IndirizzoSedeLegale);
            txtToSign.Add("CapSedeLegale : " + rapportoTecnico.ImpresaManutentrice.CapSedeLegale);
            txtToSign.Add("CittaSedeLegale : " + rapportoTecnico.ImpresaManutentrice.CittaSedeLegale);
            txtToSign.Add("NumeroCivicoSedeLegale : " + rapportoTecnico.ImpresaManutentrice.NumeroCivicoSedeLegale);
            txtToSign.Add("ProvinciaSedeLegale : " + rapportoTecnico.ImpresaManutentrice.ProvinciaSedeLegale);
            txtToSign.Add("Nome : " + rapportoTecnico.ImpresaManutentrice.Nome);
            txtToSign.Add("Cognome : " + rapportoTecnico.ImpresaManutentrice.Cognome);
            txtToSign.Add("Indirizzo : " + rapportoTecnico.ImpresaManutentrice.Indirizzo);
            txtToSign.Add("Civico : " + rapportoTecnico.ImpresaManutentrice.Civico);
            txtToSign.Add("CodiceCatastaleComune : " + rapportoTecnico.ImpresaManutentrice.CodiceCatastaleComune);
            txtToSign.Add("Email : " + rapportoTecnico.ImpresaManutentrice.Email);
            txtToSign.Add("EmailPec : " + rapportoTecnico.ImpresaManutentrice.EmailPec);
            txtToSign.Add("PartitaIVA : " + rapportoTecnico.ImpresaManutentrice.PartitaIVA);
            txtToSign.Add("CodiceFiscale : " + rapportoTecnico.ImpresaManutentrice.CodiceFiscale);
            txtToSign.Add("NumeroIscrizioneCCIAA : " + rapportoTecnico.ImpresaManutentrice.NumeroIscrizioneCCIAA);
            txtToSign.Add("ProvinciaIscrizioneCCIAA : " + rapportoTecnico.ImpresaManutentrice.ProvinciaIscrizioneCCIAA);
            
            #endregion

            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("fDichiarazioneConformita : " + rapportoTecnico.fDichiarazioneConformita);
            txtToSign.Add("fLibrettoImpiantoPresente : " + rapportoTecnico.fLibrettoImpiantoPresente);
            txtToSign.Add("fUsoManutenzioneGeneratore : " + rapportoTecnico.fUsoManutenzioneGeneratore);
            txtToSign.Add("fLibrettoImpiantoCompilato : " + rapportoTecnico.fLibrettoImpiantoCompilato);
            txtToSign.Add("DurezzaAcqua : " + rapportoTecnico.DurezzaAcqua);

            txtToSign.Add("TrattamentoRiscaldamento : " + helper.Presente_Assente(rapportoTecnico.TrattamentoRiscaldamento));
            foreach (var item in rapportoTecnico.TipoTrattamentoRiscaldamento)
            {
                txtToSign.Add("     ***** TipoTrattamentoRiscaldamento : " + helper.TipoTrattamentoRiscaldamento(item));
            }

            

            txtToSign.Add("TrattamentoACS : " + helper.Presente_Assente(rapportoTecnico.TrattamentoACS));
            foreach (var item in rapportoTecnico.TipoTrattamentoACS)
            {
                txtToSign.Add("     ***** TipoTrattamentoACS : " + helper.TipoTrattamentoACS(item));
            }

            txtToSign.Add("LocaleInstallazioneIdoneo : " + helper.Classificabile_SiNo(rapportoTecnico.LocaleInstallazioneIdoneo));
            txtToSign.Add("DimensioniApertureAdeguate : " + helper.Classificabile_SiNo(rapportoTecnico.DimensioniApertureAdeguate));
            txtToSign.Add("ApertureLibere : " + helper.Classificabile_SiNo(rapportoTecnico.ApertureLibere));



            txtToSign.Add("AssenzaPerditeCombustibile: " + helper.Classificabile_SiNo(rapportoTecnico.AssenzaPerditeCombustibile));
            txtToSign.Add("TenutaImpiantoIdraulico: " + helper.Classificabile_SiNo(rapportoTecnico.TenutaImpiantoIdraulico));
            txtToSign.Add("CodiceProgressivo: " + rapportoTecnico.CodiceProgressivo);
            String datainst = rapportoTecnico.DataInstallazione.HasValue ? rapportoTecnico.DataInstallazione.Value.ToShortDateString() : "";
            txtToSign.Add("DataInstallazione: " + datainst);
            txtToSign.Add("Fabbricante: " + rapportoTecnico.Fabbricante);
            txtToSign.Add("Modello: " + rapportoTecnico.Modello);
            txtToSign.Add("Matricola: " + rapportoTecnico.Matricola);

            if (rapportoTecnico.TipologiaGruppiTermici.HasValue)
            {
                txtToSign.Add("TipologiaGruppiTermici: " + helper.TipologiaGruppiTermici(rapportoTecnico.TipologiaGruppiTermici.Value));
            }
            else
            {
                txtToSign.Add("TipologiaGruppiTermici: " + "");
            }
            if (rapportoTecnico.TipologiaGeneratoriTermici.HasValue)
            {
                txtToSign.Add("TipologiaGeneratoriTermici: " + helper.TipologiaGeneratoriTermici(rapportoTecnico.TipologiaGeneratoriTermici.Value));
            }
            else
            {
                txtToSign.Add("TipologiaGeneratoriTermici: " + "");
            }
            txtToSign.Add("PotenzaTermicaNominaleFocolare: " + rapportoTecnico.PotenzaTermicaNominaleFocolare);
            txtToSign.Add("PotenzaTermicaNominale: " + rapportoTecnico.PotenzaTermicaNominale);
            txtToSign.Add("fClimatizzazioneInvernale: " + rapportoTecnico.fClimatizzazioneInvernale);
            txtToSign.Add("fProduzioneACS: " + rapportoTecnico.fProduzioneACS);
            txtToSign.Add("TipologiaCombustibile: " + helper.TipologiaCombustibile(rapportoTecnico.TipologiaCombustibile));
            txtToSign.Add("AltroCombustibile: " + rapportoTecnico.AltroCombustibile);
            txtToSign.Add("EvacuazioneForzata: " + rapportoTecnico.EvacuazioneForzata);
            txtToSign.Add("EvacuazioneNaturale: " + rapportoTecnico.EvacuazioneNaturale);
            txtToSign.Add("DepressioneCanaleFumo: " + rapportoTecnico.DepressioneCanaleFumo);
            txtToSign.Add("DispositiviComandoRegolazione: " + helper.Classificabile_SiNo(rapportoTecnico.DispositiviComandoRegolazione));
            txtToSign.Add("DispositiviSicurezza: " + helper.Classificabile_SiNo(rapportoTecnico.DispositiviSicurezza));
            txtToSign.Add("ValvolaSicurezzaSovrappressione: " + rapportoTecnico.ValvolaSicurezzaSovrappressione);
            txtToSign.Add("ScambiatoreFumiPulito: " + helper.Classificabile_SiNo(rapportoTecnico.ScambiatoreFumiPulito));
            txtToSign.Add("RiflussoProdottiCombustione: " + helper.Classificabile_SiNo(rapportoTecnico.RiflussoProdottiCombustione));
            txtToSign.Add("ConformitaUNI10389: " + helper.Classificabile_SiNo(rapportoTecnico.ConformitaUNI10389));
            txtToSign.Add("ModuloTermico: " + rapportoTecnico.ModuloTermico);
            txtToSign.Add("TemperaturaFumi: " + rapportoTecnico.TemperaturaFumi);
            txtToSign.Add("TemperaraturaComburente: " + rapportoTecnico.TemperaraturaComburente);
            txtToSign.Add("O2: " + rapportoTecnico.O2);
            txtToSign.Add("Co2: " + rapportoTecnico.Co2);
            txtToSign.Add("bacharach1: " + rapportoTecnico.bacharach1);
            txtToSign.Add("bacharach2: " + rapportoTecnico.bacharach2);
            txtToSign.Add("bacharach3: " + rapportoTecnico.bacharach3);
            txtToSign.Add("COFumiSecchi: " + rapportoTecnico.COFumiSecchi);
            txtToSign.Add("CoCorretto: " + rapportoTecnico.CoCorretto);
            txtToSign.Add("PortataCombustibile: " + rapportoTecnico.PortataCombustibile);
            txtToSign.Add("PotenzaTermicaEffettiva: " + rapportoTecnico.PotenzaTermicaEffettiva);
            txtToSign.Add("RendimentoCombustione: " + rapportoTecnico.RendimentoCombustione);
            txtToSign.Add("RendimentoMinimo: " + rapportoTecnico.RendimentoMinimo);
            txtToSign.Add("RispettaIndiceBacharach: " + rapportoTecnico.RispettaIndiceBacharach);
            txtToSign.Add("COFumiSecchiNoAria1000: " + rapportoTecnico.COFumiSecchiNoAria1000);
            txtToSign.Add("RendimentoSupMinimo: " + rapportoTecnico.RendimentoSupMinimo);

            #region tail rapporto tecnico
            txtToSign.Add("     ***** CheckList ***** ");
            foreach (var item in rapportoTecnico.CheckList)
            {
                helper.CheckList(item);
            }

            txtToSign.Add("Osservazioni : " + rapportoTecnico.Osservazioni);
            txtToSign.Add("Raccomandazioni : " + rapportoTecnico.Raccomandazioni);
            txtToSign.Add("Prescrizioni : " + rapportoTecnico.Prescrizioni);
            txtToSign.Add("fImpiantoFunzionante : " + rapportoTecnico.fImpiantoFunzionante);


            var dataManutenzioneConsigliata = rapportoTecnico.DataManutenzioneConsigliata.HasValue ? rapportoTecnico.DataManutenzioneConsigliata.Value.ToShortDateString() : "";
            var dataControllo = rapportoTecnico.DataControllo.HasValue ? rapportoTecnico.DataControllo.Value.ToShortDateString() : "";
            var oraArrivo = rapportoTecnico.OraArrivo.HasValue? rapportoTecnico.OraArrivo.Value.ToShortTimeString() : "";
            var oraPartenza = rapportoTecnico.OraPartenza.HasValue ? rapportoTecnico.OraPartenza.Value.ToShortTimeString() : "";
            
            txtToSign.Add("DataManutenzioneConsigliata : " + dataManutenzioneConsigliata);
            txtToSign.Add("DataControllo : " + dataControllo);
            txtToSign.Add("OraArrivo : " + oraArrivo);
            txtToSign.Add("OraPartenza : " + oraPartenza);
            txtToSign.Add("TecnicoIntervento : " + rapportoTecnico.TecnicoIntervento);

            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("BolliniCalorePulito");

            foreach (var item in rapportoTecnico.BolliniCalorePulito)
            {
                txtToSign.Add(item);
            }

            #endregion

            return txtToSign;
        }

        public static List<string> J_RCTSC2TXT(POMJ_RapportoControlloTecnico_SC rapportoTecnico)
        {
            List<String> txtToSign = new List<string>();
            LookUpHelper helper = new LookUpHelper();

            txtToSign.Add("RAPPORTO DI CONTROLLO TECNICO TIPO 3 (SCAMBIATORE)");
            txtToSign.Add("Timestamp: " + DateTime.Now.ToLongTimeString());
            txtToSign.Add("-----------------------------------------------------");

            #region rapporto controllo tecnico base
            txtToSign.Add("TipologiaControllo : " + helper.TipologiaControllo(rapportoTecnico.TipologiaControllo));
            txtToSign.Add("StatoRapportoDiControllo : " + helper.StatoRapportoDiControllo(rapportoTecnico.StatoRapportoDiControllo));
            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("Codice Targatura Impianto  ");
            if (rapportoTecnico.CodiceTargaturaImpianto != null)
            {
                txtToSign.Add("CodiceTargatura : " + rapportoTecnico.CodiceTargaturaImpianto.CodiceTargatura);
                //txtToSign.Add("CodiceRandom : " + rapportoTecnico.CodiceTargaturaImpianto.CodiceRandom);
                //txtToSign.Add("Anno : " + rapportoTecnico.CodiceTargaturaImpianto.Anno);
            }
            else
            {
                txtToSign.Add("CodiceTargatura : " + "");
                //txtToSign.Add("CodiceRandom : " + "");
                //txtToSign.Add("Anno : " + "");
            }


            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("PotenzaTermicaNominaleTotaleMax : " + rapportoTecnico.PotenzaTermicaNominaleTotaleMax);
            txtToSign.Add("Comune : " + rapportoTecnico.Comune);
            txtToSign.Add("Indirizzo : " + rapportoTecnico.Indirizzo);
            txtToSign.Add("Civico : " + rapportoTecnico.Civico);
            txtToSign.Add("Palazzo : " + rapportoTecnico.Palazzo);
            txtToSign.Add("Scala : " + rapportoTecnico.Scala);
            txtToSign.Add("Interno : " + rapportoTecnico.Interno);
            txtToSign.Add("TipologiaResponsabile : " + helper.TipologiaResponsabile(rapportoTecnico.TipologiaResponsabile));
            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("Responsabile Impianto ");
            if (rapportoTecnico.ResponsabileImpianto.TipoSoggetto != null)
            {
                txtToSign.Add("TipoSoggetto : " + helper.TipoSoggetto(rapportoTecnico.ResponsabileImpianto.TipoSoggetto));
            }
            txtToSign.Add("NomeAzienda : " + rapportoTecnico.ResponsabileImpianto.NomeAzienda);
            txtToSign.Add("IndirizzoSedeLegale : " + rapportoTecnico.ResponsabileImpianto.IndirizzoSedeLegale);
            txtToSign.Add("CapSedeLegale : " + rapportoTecnico.ResponsabileImpianto.CapSedeLegale);
            txtToSign.Add("CittaSedeLegale : " + rapportoTecnico.ResponsabileImpianto.CittaSedeLegale);
            txtToSign.Add("NumeroCivicoSedeLegale : " + rapportoTecnico.ResponsabileImpianto.NumeroCivicoSedeLegale);
            txtToSign.Add("ProvinciaSedeLegale : " + rapportoTecnico.ResponsabileImpianto.ProvinciaSedeLegale);
            txtToSign.Add("Nome : " + rapportoTecnico.ResponsabileImpianto.Nome);
            txtToSign.Add("Cognome : " + rapportoTecnico.ResponsabileImpianto.Cognome);
            txtToSign.Add("Indirizzo : " + rapportoTecnico.ResponsabileImpianto.Indirizzo);
            txtToSign.Add("Civico : " + rapportoTecnico.ResponsabileImpianto.Civico);
            txtToSign.Add("CodiceCatastaleComune : " + rapportoTecnico.ResponsabileImpianto.CodiceCatastaleComune);
            txtToSign.Add("Email : " + rapportoTecnico.ResponsabileImpianto.Email);
            txtToSign.Add("EmailPec : " + rapportoTecnico.ResponsabileImpianto.EmailPec);
            txtToSign.Add("PartitaIVA : " + rapportoTecnico.ResponsabileImpianto.PartitaIVA);
            txtToSign.Add("CodiceFiscale : " + rapportoTecnico.ResponsabileImpianto.CodiceFiscale);
            txtToSign.Add("NumeroIscrizioneCCIAA : " + rapportoTecnico.ResponsabileImpianto.NumeroIscrizioneCCIAA);
            txtToSign.Add("ProvinciaIscrizioneCCIAA : " + rapportoTecnico.ResponsabileImpianto.ProvinciaIscrizioneCCIAA);
                                   
            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("TerzoResponsabile  ");
            if (rapportoTecnico.TerzoResponsabile.TipoSoggetto != null)
            {
                txtToSign.Add("TipoSoggetto : " + helper.TipoSoggetto(rapportoTecnico.TerzoResponsabile.TipoSoggetto));
            }
            txtToSign.Add("NomeAzienda : " + rapportoTecnico.TerzoResponsabile.NomeAzienda);
            txtToSign.Add("IndirizzoSedeLegale : " + rapportoTecnico.TerzoResponsabile.IndirizzoSedeLegale);
            txtToSign.Add("CapSedeLegale : " + rapportoTecnico.TerzoResponsabile.CapSedeLegale);
            txtToSign.Add("CittaSedeLegale : " + rapportoTecnico.TerzoResponsabile.CittaSedeLegale);
            txtToSign.Add("NumeroCivicoSedeLegale : " + rapportoTecnico.TerzoResponsabile.NumeroCivicoSedeLegale);
            txtToSign.Add("ProvinciaSedeLegale : " + rapportoTecnico.TerzoResponsabile.ProvinciaSedeLegale);
            txtToSign.Add("Nome : " + rapportoTecnico.TerzoResponsabile.Nome);
            txtToSign.Add("Cognome : " + rapportoTecnico.TerzoResponsabile.Cognome);
            txtToSign.Add("Indirizzo : " + rapportoTecnico.TerzoResponsabile.Indirizzo);
            txtToSign.Add("Civico : " + rapportoTecnico.TerzoResponsabile.Civico);
            txtToSign.Add("CodiceCatastaleComune : " + rapportoTecnico.TerzoResponsabile.CodiceCatastaleComune);
            txtToSign.Add("Email : " + rapportoTecnico.TerzoResponsabile.Email);
            txtToSign.Add("EmailPec : " + rapportoTecnico.TerzoResponsabile.EmailPec);
            txtToSign.Add("PartitaIVA : " + rapportoTecnico.TerzoResponsabile.PartitaIVA);
            txtToSign.Add("CodiceFiscale : " + rapportoTecnico.TerzoResponsabile.CodiceFiscale);
            txtToSign.Add("NumeroIscrizioneCCIAA : " + rapportoTecnico.TerzoResponsabile.NumeroIscrizioneCCIAA);
            txtToSign.Add("ProvinciaIscrizioneCCIAA : " + rapportoTecnico.TerzoResponsabile.ProvinciaIscrizioneCCIAA);
             
            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("Impresa Manutentrice ");
            if (rapportoTecnico.ImpresaManutentrice.TipoSoggetto != null)
            {
                txtToSign.Add("TipoSoggetto : " + helper.TipoSoggetto(rapportoTecnico.ImpresaManutentrice.TipoSoggetto));
            }
            txtToSign.Add("NomeAzienda : " + rapportoTecnico.ImpresaManutentrice.NomeAzienda);
            txtToSign.Add("IndirizzoSedeLegale : " + rapportoTecnico.ImpresaManutentrice.IndirizzoSedeLegale);
            txtToSign.Add("CapSedeLegale : " + rapportoTecnico.ImpresaManutentrice.CapSedeLegale);
            txtToSign.Add("CittaSedeLegale : " + rapportoTecnico.ImpresaManutentrice.CittaSedeLegale);
            txtToSign.Add("NumeroCivicoSedeLegale : " + rapportoTecnico.ImpresaManutentrice.NumeroCivicoSedeLegale);
            txtToSign.Add("ProvinciaSedeLegale : " + rapportoTecnico.ImpresaManutentrice.ProvinciaSedeLegale);
            txtToSign.Add("Nome : " + rapportoTecnico.ImpresaManutentrice.Nome);
            txtToSign.Add("Cognome : " + rapportoTecnico.ImpresaManutentrice.Cognome);
            txtToSign.Add("Indirizzo : " + rapportoTecnico.ImpresaManutentrice.Indirizzo);
            txtToSign.Add("Civico : " + rapportoTecnico.ImpresaManutentrice.Civico);
            txtToSign.Add("CodiceCatastaleComune : " + rapportoTecnico.ImpresaManutentrice.CodiceCatastaleComune);
            txtToSign.Add("Email : " + rapportoTecnico.ImpresaManutentrice.Email);
            txtToSign.Add("EmailPec : " + rapportoTecnico.ImpresaManutentrice.EmailPec);
            txtToSign.Add("PartitaIVA : " + rapportoTecnico.ImpresaManutentrice.PartitaIVA);
            txtToSign.Add("CodiceFiscale : " + rapportoTecnico.ImpresaManutentrice.CodiceFiscale);
            txtToSign.Add("NumeroIscrizioneCCIAA : " + rapportoTecnico.ImpresaManutentrice.NumeroIscrizioneCCIAA);
            txtToSign.Add("ProvinciaIscrizioneCCIAA : " + rapportoTecnico.ImpresaManutentrice.ProvinciaIscrizioneCCIAA);
            
            #endregion

            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("fDichiarazioneConformita : " + rapportoTecnico.fDichiarazioneConformita);
            txtToSign.Add("fLibrettoImpiantoPresente : " + rapportoTecnico.fLibrettoImpiantoPresente);
            txtToSign.Add("fUsoManutenzioneGeneratore : " + rapportoTecnico.fUsoManutenzioneGeneratore);
            txtToSign.Add("fLibrettoImpiantoCompilato : " + rapportoTecnico.fLibrettoImpiantoCompilato);
            txtToSign.Add("DurezzaAcqua : " + rapportoTecnico.DurezzaAcqua);

            txtToSign.Add("TrattamentoRiscaldamento : " + helper.Presente_Assente(rapportoTecnico.TrattamentoRiscaldamento));
            foreach (var item in rapportoTecnico.TipoTrattamentoRiscaldamento)
            {
                txtToSign.Add("     ***** TipoTrattamentoRiscaldamento : " + helper.TipoTrattamentoRiscaldamento(item));
            }

            txtToSign.Add("TrattamentoACS : " + helper.Presente_Assente(rapportoTecnico.TrattamentoACS));
            foreach (var item in rapportoTecnico.TipoTrattamentoACS)
            {
                txtToSign.Add("     ***** TipoTrattamentoACS : " + helper.TipoTrattamentoACS(item));
            }

            txtToSign.Add("LocaleInstallazioneIdoneo : " + helper.Classificabile_SiNo(rapportoTecnico.LocaleInstallazioneIdoneo));
            txtToSign.Add("LineeElettricheIdonee : " + helper.Classificabile_SiNo(rapportoTecnico.LineeElettricheIdonee));
            txtToSign.Add("StatoCoibentazioniIdonee : " + helper.Classificabile_SiNo(rapportoTecnico.StatoCoibentazioniIdonee));
            txtToSign.Add("AssenzaPerditeCombustibile : " + helper.Classificabile_SiNo(rapportoTecnico.AssenzaPerditeCombustibile));

            txtToSign.Add("CodiceProgressivo: " + rapportoTecnico.CodiceProgressivo);
            String datainst = rapportoTecnico.DataInstallazione.HasValue ? rapportoTecnico.DataInstallazione.Value.ToShortDateString() : "";
            txtToSign.Add("DataInstallazione: " + datainst);
            txtToSign.Add("Fabbricante: " + rapportoTecnico.Fabbricante);
            txtToSign.Add("Modello: " + rapportoTecnico.Modello);
            txtToSign.Add("Matricola: " + rapportoTecnico.Matricola);

            txtToSign.Add("PotenzaTermicaNominale: " + rapportoTecnico.PotenzaTermicaNominale);
            txtToSign.Add("ClimatizzazioneInvernale: " + rapportoTecnico.fClimatizzazioneInvernale);
            txtToSign.Add("ClimatizzazioneEstiva: " + rapportoTecnico.fClimatizzazioneEstiva);
            txtToSign.Add("ProduzioneACS: " + rapportoTecnico.fProduzioneACS);
            txtToSign.Add("TipologiaFluidoTermoVettore: " + helper.TipologiaFluidoTermoVettore(rapportoTecnico.TipologiaFluidoTermoVettore));
            txtToSign.Add("PotenzaCompatibileProgetto: " + helper.Classificabile_SiNo(rapportoTecnico.PotenzaCompatibileProgetto));
            txtToSign.Add("AssenzaTrafilamenti: " + helper.Classificabile_SiNo(rapportoTecnico.AssenzaTrafilamenti));

            txtToSign.Add("TemperaturaEsterna: " + rapportoTecnico.TemperaturaEsterna);
            txtToSign.Add("TemperaturaMandataPrimario: " + rapportoTecnico.TemperaturaMandataPrimario);
            txtToSign.Add("TemperaturaRitornoPrimario: " + rapportoTecnico.TemperaturaRitornoPrimario);
            txtToSign.Add("PortataFluidoPrimario: " + rapportoTecnico.PortataFluidoPrimario);
            txtToSign.Add("TemperaturaMandataSecondario: " + rapportoTecnico.TemperaturaMandataSecondario);
            txtToSign.Add("TemperaturaRitornoSecondario: " + rapportoTecnico.TemperaturaRitornoSecondario);
            txtToSign.Add("PotenzaTermica: " + rapportoTecnico.PotenzaTermica);


            #region tail rapporto tecnico
            txtToSign.Add("     ***** CheckList ***** ");
            foreach (var item in rapportoTecnico.CheckList)
            {
                helper.CheckList(item);
            }

            txtToSign.Add("Osservazioni : " + rapportoTecnico.Osservazioni);
            txtToSign.Add("Raccomandazioni : " + rapportoTecnico.Raccomandazioni);
            txtToSign.Add("Prescrizioni : " + rapportoTecnico.Prescrizioni);
            txtToSign.Add("fImpiantoFunzionante : " + rapportoTecnico.fImpiantoFunzionante);


            var dataManutenzioneConsigliata = rapportoTecnico.DataManutenzioneConsigliata.HasValue ? rapportoTecnico.DataManutenzioneConsigliata.Value.ToShortDateString() : "";
            var dataControllo = rapportoTecnico.DataControllo.HasValue ? rapportoTecnico.DataControllo.Value.ToShortDateString() : "";
            var oraArrivo = rapportoTecnico.OraArrivo.HasValue ? rapportoTecnico.OraArrivo.Value.ToShortTimeString() : "";
            var oraPartenza = rapportoTecnico.OraPartenza.HasValue ? rapportoTecnico.OraPartenza.Value.ToShortTimeString() : "";



            txtToSign.Add("DataManutenzioneConsigliata : " + dataManutenzioneConsigliata);
            txtToSign.Add("DataControllo : " + dataControllo);
            txtToSign.Add("OraArrivo : " + oraArrivo);
            txtToSign.Add("OraPartenza : " + oraPartenza);
            txtToSign.Add("TecnicoIntervento : " + rapportoTecnico.TecnicoIntervento);

            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("BolliniCalorePulito");

            foreach (var item in rapportoTecnico.BolliniCalorePulito)
            {
                txtToSign.Add(item);
            }

            #endregion

            return txtToSign;
        }

        public static List<string> J_RCTGF2TXT(POMJ_RapportoControlloTecnico_GF rapportoTecnico)
        {
            List<String> txtToSign = new List<string>();
            LookUpHelper helper = new LookUpHelper();

            txtToSign.Add("RAPPORTO DI CONTROLLO TECNICO TIPO 2 (GRUPPO FRIGO)");
            txtToSign.Add("Timestamp: " + DateTime.Now.ToLongTimeString());
            txtToSign.Add("-----------------------------------------------------");

            #region rapporto controllo tecnico base
            txtToSign.Add("TipologiaControllo : " + helper.TipologiaControllo(rapportoTecnico.TipologiaControllo));
            txtToSign.Add("StatoRapportoDiControllo : " + helper.StatoRapportoDiControllo(rapportoTecnico.StatoRapportoDiControllo));
            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("Codice Targatura Impianto  ");
            if (rapportoTecnico.CodiceTargaturaImpianto != null)
            {
                txtToSign.Add("CodiceTargatura : " + rapportoTecnico.CodiceTargaturaImpianto.CodiceTargatura);
                //txtToSign.Add("CodiceRandom : " + rapportoTecnico.CodiceTargaturaImpianto.CodiceRandom);
                //txtToSign.Add("Anno : " + rapportoTecnico.CodiceTargaturaImpianto.Anno);
            }
            else
            {
                txtToSign.Add("CodiceTargatura : " + "");
                //txtToSign.Add("CodiceRandom : " + "");
                //txtToSign.Add("Anno : " + "");
            }


            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("PotenzaTermicaNominaleTotaleMax : " + rapportoTecnico.PotenzaTermicaNominaleTotaleMax);
            txtToSign.Add("Comune : " + rapportoTecnico.Comune);
            txtToSign.Add("Indirizzo : " + rapportoTecnico.Indirizzo);
            txtToSign.Add("Civico : " + rapportoTecnico.Civico);
            txtToSign.Add("Palazzo : " + rapportoTecnico.Palazzo);
            txtToSign.Add("Scala : " + rapportoTecnico.Scala);
            txtToSign.Add("Interno : " + rapportoTecnico.Interno);
            txtToSign.Add("TipologiaResponsabile : " + helper.TipologiaResponsabile(rapportoTecnico.TipologiaResponsabile));
            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("Responsabile Impianto ");
            if (rapportoTecnico.ResponsabileImpianto.TipoSoggetto != null)
            {
                txtToSign.Add("TipoSoggetto : " + helper.TipoSoggetto(rapportoTecnico.ResponsabileImpianto.TipoSoggetto));
            }
            txtToSign.Add("NomeAzienda : " + rapportoTecnico.ResponsabileImpianto.NomeAzienda);
            txtToSign.Add("IndirizzoSedeLegale : " + rapportoTecnico.ResponsabileImpianto.IndirizzoSedeLegale);
            txtToSign.Add("CapSedeLegale : " + rapportoTecnico.ResponsabileImpianto.CapSedeLegale);
            txtToSign.Add("CittaSedeLegale : " + rapportoTecnico.ResponsabileImpianto.CittaSedeLegale);
            txtToSign.Add("NumeroCivicoSedeLegale : " + rapportoTecnico.ResponsabileImpianto.NumeroCivicoSedeLegale);
            txtToSign.Add("ProvinciaSedeLegale : " + rapportoTecnico.ResponsabileImpianto.ProvinciaSedeLegale);
            txtToSign.Add("Nome : " + rapportoTecnico.ResponsabileImpianto.Nome);
            txtToSign.Add("Cognome : " + rapportoTecnico.ResponsabileImpianto.Cognome);
            txtToSign.Add("Indirizzo : " + rapportoTecnico.ResponsabileImpianto.Indirizzo);
            txtToSign.Add("Civico : " + rapportoTecnico.ResponsabileImpianto.Civico);
            txtToSign.Add("CodiceCatastaleComune : " + rapportoTecnico.ResponsabileImpianto.CodiceCatastaleComune);
            txtToSign.Add("Email : " + rapportoTecnico.ResponsabileImpianto.Email);
            txtToSign.Add("EmailPec : " + rapportoTecnico.ResponsabileImpianto.EmailPec);
            txtToSign.Add("PartitaIVA : " + rapportoTecnico.ResponsabileImpianto.PartitaIVA);
            txtToSign.Add("CodiceFiscale : " + rapportoTecnico.ResponsabileImpianto.CodiceFiscale);
            txtToSign.Add("NumeroIscrizioneCCIAA : " + rapportoTecnico.ResponsabileImpianto.NumeroIscrizioneCCIAA);
            txtToSign.Add("ProvinciaIscrizioneCCIAA : " + rapportoTecnico.ResponsabileImpianto.ProvinciaIscrizioneCCIAA);
                        
            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("TerzoResponsabile  ");
            if (rapportoTecnico.TerzoResponsabile.TipoSoggetto != null)
            {
                txtToSign.Add("TipoSoggetto : " + helper.TipoSoggetto(rapportoTecnico.TerzoResponsabile.TipoSoggetto));
            }
            txtToSign.Add("NomeAzienda : " + rapportoTecnico.TerzoResponsabile.NomeAzienda);
            txtToSign.Add("IndirizzoSedeLegale : " + rapportoTecnico.TerzoResponsabile.IndirizzoSedeLegale);
            txtToSign.Add("CapSedeLegale : " + rapportoTecnico.TerzoResponsabile.CapSedeLegale);
            txtToSign.Add("CittaSedeLegale : " + rapportoTecnico.TerzoResponsabile.CittaSedeLegale);
            txtToSign.Add("NumeroCivicoSedeLegale : " + rapportoTecnico.TerzoResponsabile.NumeroCivicoSedeLegale);
            txtToSign.Add("ProvinciaSedeLegale : " + rapportoTecnico.TerzoResponsabile.ProvinciaSedeLegale);
            txtToSign.Add("Nome : " + rapportoTecnico.TerzoResponsabile.Nome);
            txtToSign.Add("Cognome : " + rapportoTecnico.TerzoResponsabile.Cognome);
            txtToSign.Add("Indirizzo : " + rapportoTecnico.TerzoResponsabile.Indirizzo);
            txtToSign.Add("Civico : " + rapportoTecnico.TerzoResponsabile.Civico);
            txtToSign.Add("CodiceCatastaleComune : " + rapportoTecnico.TerzoResponsabile.CodiceCatastaleComune);
            txtToSign.Add("Email : " + rapportoTecnico.TerzoResponsabile.Email);
            txtToSign.Add("EmailPec : " + rapportoTecnico.TerzoResponsabile.EmailPec);
            txtToSign.Add("PartitaIVA : " + rapportoTecnico.TerzoResponsabile.PartitaIVA);
            txtToSign.Add("CodiceFiscale : " + rapportoTecnico.TerzoResponsabile.CodiceFiscale);
            txtToSign.Add("NumeroIscrizioneCCIAA : " + rapportoTecnico.TerzoResponsabile.NumeroIscrizioneCCIAA);
            txtToSign.Add("ProvinciaIscrizioneCCIAA : " + rapportoTecnico.TerzoResponsabile.ProvinciaIscrizioneCCIAA);
                        
            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("Impresa Manutentrice ");
            if (rapportoTecnico.ImpresaManutentrice.TipoSoggetto != null)
            {
                txtToSign.Add("TipoSoggetto : " + helper.TipoSoggetto(rapportoTecnico.ImpresaManutentrice.TipoSoggetto));
            }
            txtToSign.Add("NomeAzienda : " + rapportoTecnico.ImpresaManutentrice.NomeAzienda);
            txtToSign.Add("IndirizzoSedeLegale : " + rapportoTecnico.ImpresaManutentrice.IndirizzoSedeLegale);
            txtToSign.Add("CapSedeLegale : " + rapportoTecnico.ImpresaManutentrice.CapSedeLegale);
            txtToSign.Add("CittaSedeLegale : " + rapportoTecnico.ImpresaManutentrice.CittaSedeLegale);
            txtToSign.Add("NumeroCivicoSedeLegale : " + rapportoTecnico.ImpresaManutentrice.NumeroCivicoSedeLegale);
            txtToSign.Add("ProvinciaSedeLegale : " + rapportoTecnico.ImpresaManutentrice.ProvinciaSedeLegale);
            txtToSign.Add("Nome : " + rapportoTecnico.ImpresaManutentrice.Nome);

            txtToSign.Add("Cognome : " + rapportoTecnico.ImpresaManutentrice.Cognome);

            txtToSign.Add("Indirizzo : " + rapportoTecnico.ImpresaManutentrice.Indirizzo);
            txtToSign.Add("Civico : " + rapportoTecnico.ImpresaManutentrice.Civico);
            txtToSign.Add("CodiceCatastaleComune : " + rapportoTecnico.ImpresaManutentrice.CodiceCatastaleComune);
            txtToSign.Add("Email : " + rapportoTecnico.ImpresaManutentrice.Email);
            txtToSign.Add("EmailPec : " + rapportoTecnico.ImpresaManutentrice.EmailPec);
            txtToSign.Add("PartitaIVA : " + rapportoTecnico.ImpresaManutentrice.PartitaIVA);
            txtToSign.Add("CodiceFiscale : " + rapportoTecnico.ImpresaManutentrice.CodiceFiscale);
                        
            txtToSign.Add("NumeroIscrizioneCCIAA : " + rapportoTecnico.ImpresaManutentrice.NumeroIscrizioneCCIAA);

            txtToSign.Add("ProvinciaIscrizioneCCIAA : " + rapportoTecnico.ImpresaManutentrice.ProvinciaIscrizioneCCIAA);
            
            #endregion

            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("fDichiarazioneConformita : " + rapportoTecnico.fDichiarazioneConformita);
            txtToSign.Add("fLibrettoImpiantoPresente : " + rapportoTecnico.fLibrettoImpiantoPresente);
            txtToSign.Add("fUsoManutenzioneGeneratore : " + rapportoTecnico.fUsoManutenzioneGeneratore);
            txtToSign.Add("fLibrettoImpiantoCompilato : " + rapportoTecnico.fLibrettoImpiantoCompilato);
            txtToSign.Add("DurezzaAcqua : " + rapportoTecnico.DurezzaAcqua);

            txtToSign.Add("TrattamentoRiscaldamento : " + helper.Presente_Assente(rapportoTecnico.TrattamentoRiscaldamento));
            foreach (var item in rapportoTecnico.TipoTrattamentoRiscaldamento)
            {
                txtToSign.Add("     ***** TipoTrattamentoRiscaldamento : " + helper.TipoTrattamentoRiscaldamento(item));
            }

            //txtToSign.Add("TrattamentoACS : " + helper.Presente_Assente(rapportoTecnico.TrattamentoACS)); ??
            foreach (var item in rapportoTecnico.TipoTrattamentoACS)
            {
                txtToSign.Add("     ***** TipoTrattamentoACS : " + helper.TipoTrattamentoACS(item));
            }

            txtToSign.Add("LocaleInstallazioneIdoneo : " + helper.Classificabile_SiNo(rapportoTecnico.LocaleInstallazioneIdoneo));
            txtToSign.Add("DimensioniApertureAdeguate : " + helper.Classificabile_SiNo(rapportoTecnico.DimensioniApertureAdeguate));
            txtToSign.Add("ApertureLibere : " + helper.Classificabile_SiNo(rapportoTecnico.ApertureLibere));
            txtToSign.Add("LineeElettricheIdonee : " + helper.Classificabile_SiNo(rapportoTecnico.LineeElettricheIdonee));
            txtToSign.Add("CoibentazioniIdonee : " + helper.Classificabile_SiNo(rapportoTecnico.CoibentazioniIdonee));

            txtToSign.Add("CodiceProgressivo: " + rapportoTecnico.CodiceProgressivo);
            String datainst = rapportoTecnico.DataInstallazione.HasValue ? rapportoTecnico.DataInstallazione.Value.ToShortDateString() : "";
            txtToSign.Add("DataInstallazione: " + datainst);
            txtToSign.Add("Fabbricante: " + rapportoTecnico.Fabbricante);
            txtToSign.Add("Modello: " + rapportoTecnico.Modello);
            txtToSign.Add("Matricola: " + rapportoTecnico.Matricola);

            txtToSign.Add("NCircuitiTotali: " + rapportoTecnico.NCircuitiTotali);
            txtToSign.Add("Potenzafrigorifera: " + rapportoTecnico.Potenzafrigorifera);
            txtToSign.Add("PotenzaTermicaNominale: " + rapportoTecnico.PotenzaTermicaNominale);
            txtToSign.Add("fClimatizzazioneInvernale: " + rapportoTecnico.fClimatizzazioneInvernale);
            txtToSign.Add("fClimatizzazioneEstiva: " + rapportoTecnico.fClimatizzazioneEstiva);
            txtToSign.Add("fProduzioneACS: " + rapportoTecnico.fProduzioneACS);
            txtToSign.Add("fRiscaldamento: " + rapportoTecnico.fRiscaldamento);

            txtToSign.Add("fRaffrescamento: " + rapportoTecnico.fRaffrescamento);
            txtToSign.Add("TipologiaMacchineFrigorifere: " + helper.TipologiaMacchineFrigorifere(rapportoTecnico.TipologiaMacchineFrigorifere));
            txtToSign.Add("AssenzaPerditeRefrigerante: " + helper.Classificabile_SiNo(rapportoTecnico.AssenzaPerditeRefrigerante));
            txtToSign.Add("FiltriPuliti: " + helper.Classificabile_SiNo(rapportoTecnico.FiltriPuliti));
            txtToSign.Add("LeakDetector: " + helper.Classificabile_SiNo(rapportoTecnico.LeakDetector));
            txtToSign.Add("ScambiatoriLiberi: " + helper.Classificabile_SiNo(rapportoTecnico.ScambiatoriLiberi));
            txtToSign.Add("ParametriTermodinamici: " + helper.Classificabile_SiNo(rapportoTecnico.ParametriTermodinamici));

            txtToSign.Add("NCircuiti: " + rapportoTecnico.NCircuiti);
            txtToSign.Add("TemperaturaSurriscaldamento: " + rapportoTecnico.TemperaturaSurriscaldamento);
            txtToSign.Add("TemperaturaSottoraffreddamento: " + rapportoTecnico.TemperaturaSottoraffreddamento);
            txtToSign.Add("TemperaturaCondensazione: " + rapportoTecnico.TemperaturaCondensazione);
            txtToSign.Add("TemperaturaEvaporazione: " + rapportoTecnico.TemperaturaEvaporazione);
            txtToSign.Add("TInglatoEst: " + rapportoTecnico.TInglatoEst);
            txtToSign.Add("TUscLatoEst: " + rapportoTecnico.TUscLatoEst);
            txtToSign.Add("TIngLatoUtenze: " + rapportoTecnico.TIngLatoUtenze);
            txtToSign.Add("TUscLatoUtenze: " + rapportoTecnico.TUscLatoUtenze);
            txtToSign.Add("PotenzaAssorbita: " + rapportoTecnico.PotenzaAssorbita);
            txtToSign.Add("TUscitaFluido: " + rapportoTecnico.TUscitaFluido);

            txtToSign.Add("TBulboUmidoAria: " + rapportoTecnico.TBulboUmidoAria);
            txtToSign.Add("TIngressoLatoEsterno: " + rapportoTecnico.TIngressoLatoEsterno);
            txtToSign.Add("TUscitaLatoEsterno: " + rapportoTecnico.TUscitaLatoEsterno);
            txtToSign.Add("TIngressoLatoMacchina: " + rapportoTecnico.TIngressoLatoMacchina);
            txtToSign.Add("TUscitaLatoMacchina: " + rapportoTecnico.TUscitaLatoMacchina);

            #region tail rapporto tecnico
            txtToSign.Add("     ***** CheckList ***** ");
            foreach (var item in rapportoTecnico.CheckList)
            {
                helper.CheckList(item);
            }

            txtToSign.Add("Osservazioni : " + rapportoTecnico.Osservazioni);
            txtToSign.Add("Raccomandazioni : " + rapportoTecnico.Raccomandazioni);
            txtToSign.Add("Prescrizioni : " + rapportoTecnico.Prescrizioni);
            txtToSign.Add("fImpiantoFunzionante : " + rapportoTecnico.fImpiantoFunzionante);


            var dataManutenzioneConsigliata = rapportoTecnico.DataManutenzioneConsigliata.HasValue ? rapportoTecnico.DataManutenzioneConsigliata.Value.ToShortDateString() : "";
            var dataControllo = rapportoTecnico.DataControllo.HasValue ? rapportoTecnico.DataControllo.Value.ToShortDateString() : "";
            var oraArrivo = rapportoTecnico.OraArrivo.HasValue ? rapportoTecnico.OraArrivo.Value.ToShortTimeString() : "";
            var oraPartenza = rapportoTecnico.OraPartenza.HasValue ? rapportoTecnico.OraPartenza.Value.ToShortTimeString() : "";



            txtToSign.Add("DataManutenzioneConsigliata : " + dataManutenzioneConsigliata);
            txtToSign.Add("DataControllo : " + dataControllo);
            txtToSign.Add("OraArrivo : " + oraArrivo);
            txtToSign.Add("OraPartenza : " + oraPartenza);
            txtToSign.Add("TecnicoIntervento : " + rapportoTecnico.TecnicoIntervento);

            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("BolliniCalorePulito");

            foreach (var item in rapportoTecnico.BolliniCalorePulito)
            {
                txtToSign.Add(item);
            }

            #endregion

            return txtToSign;
        }

        public static List<string> J_RCTCG2TXT(POMJ_RapportoControlloTecnico_CG rapportoTecnico)
        {
            List<String> txtToSign = new List<string>();
            LookUpHelper helper = new LookUpHelper();

            txtToSign.Add("RAPPORTO DI CONTROLLO TECNICO TIPO 4 (COGENERATORE)");
            txtToSign.Add("Timestamp: " + DateTime.Now.ToLongTimeString());
            txtToSign.Add("-----------------------------------------------------");

            #region rapporto controllo tecnico base
            txtToSign.Add("TipologiaControllo : " + helper.TipologiaControllo(rapportoTecnico.TipologiaControllo));
            txtToSign.Add("StatoRapportoDiControllo : " + helper.StatoRapportoDiControllo(rapportoTecnico.StatoRapportoDiControllo));
            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("Codice Targatura Impianto  ");
            if (rapportoTecnico.CodiceTargaturaImpianto != null)
            {
                txtToSign.Add("CodiceTargatura : " + rapportoTecnico.CodiceTargaturaImpianto.CodiceTargatura);
            }
            else
            {
                txtToSign.Add("CodiceTargatura : " + "");
            }


            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("PotenzaTermicaNominaleTotaleMax : " + rapportoTecnico.PotenzaTermicaNominaleTotaleMax);
            txtToSign.Add("Comune : " + rapportoTecnico.Comune);
            txtToSign.Add("Indirizzo : " + rapportoTecnico.Indirizzo);
            txtToSign.Add("Civico : " + rapportoTecnico.Civico);
            txtToSign.Add("Palazzo : " + rapportoTecnico.Palazzo);
            txtToSign.Add("Scala : " + rapportoTecnico.Scala);
            txtToSign.Add("Interno : " + rapportoTecnico.Interno);
            txtToSign.Add("TipologiaResponsabile : " + helper.TipologiaResponsabile(rapportoTecnico.TipologiaResponsabile));
            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("Responsabile Impianto ");
            if (rapportoTecnico.ResponsabileImpianto.TipoSoggetto != null)
            {
                txtToSign.Add("TipoSoggetto : " + helper.TipoSoggetto(rapportoTecnico.ResponsabileImpianto.TipoSoggetto));
            }
            txtToSign.Add("NomeAzienda : " + rapportoTecnico.ResponsabileImpianto.NomeAzienda);
            txtToSign.Add("IndirizzoSedeLegale : " + rapportoTecnico.ResponsabileImpianto.IndirizzoSedeLegale);
            txtToSign.Add("CapSedeLegale : " + rapportoTecnico.ResponsabileImpianto.CapSedeLegale);
            txtToSign.Add("CittaSedeLegale : " + rapportoTecnico.ResponsabileImpianto.CittaSedeLegale);
            txtToSign.Add("NumeroCivicoSedeLegale : " + rapportoTecnico.ResponsabileImpianto.NumeroCivicoSedeLegale);
            txtToSign.Add("ProvinciaSedeLegale : " + rapportoTecnico.ResponsabileImpianto.ProvinciaSedeLegale);
            txtToSign.Add("Nome : " + rapportoTecnico.ResponsabileImpianto.Nome);
            txtToSign.Add("Cognome : " + rapportoTecnico.ResponsabileImpianto.Cognome);
            txtToSign.Add("Indirizzo : " + rapportoTecnico.ResponsabileImpianto.Indirizzo);
            txtToSign.Add("Civico : " + rapportoTecnico.ResponsabileImpianto.Civico);
            txtToSign.Add("CodiceCatastaleComune : " + rapportoTecnico.ResponsabileImpianto.CodiceCatastaleComune);
            txtToSign.Add("Email : " + rapportoTecnico.ResponsabileImpianto.Email);
            txtToSign.Add("EmailPec : " + rapportoTecnico.ResponsabileImpianto.EmailPec);
            txtToSign.Add("PartitaIVA : " + rapportoTecnico.ResponsabileImpianto.PartitaIVA);
            txtToSign.Add("CodiceFiscale : " + rapportoTecnico.ResponsabileImpianto.CodiceFiscale);
            txtToSign.Add("NumeroIscrizioneCCIAA : " + rapportoTecnico.ResponsabileImpianto.NumeroIscrizioneCCIAA);
            txtToSign.Add("ProvinciaIscrizioneCCIAA : " + rapportoTecnico.ResponsabileImpianto.ProvinciaIscrizioneCCIAA);
                        
            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("TerzoResponsabile  ");
            if (rapportoTecnico.TerzoResponsabile.TipoSoggetto != null)
            {
                txtToSign.Add("TipoSoggetto : " + helper.TipoSoggetto(rapportoTecnico.TerzoResponsabile.TipoSoggetto));
            }
            txtToSign.Add("NomeAzienda : " + rapportoTecnico.TerzoResponsabile.NomeAzienda);
            txtToSign.Add("IndirizzoSedeLegale : " + rapportoTecnico.TerzoResponsabile.IndirizzoSedeLegale);
            txtToSign.Add("CapSedeLegale : " + rapportoTecnico.TerzoResponsabile.CapSedeLegale);
            txtToSign.Add("CittaSedeLegale : " + rapportoTecnico.TerzoResponsabile.CittaSedeLegale);
            txtToSign.Add("NumeroCivicoSedeLegale : " + rapportoTecnico.TerzoResponsabile.NumeroCivicoSedeLegale);
            txtToSign.Add("ProvinciaSedeLegale : " + rapportoTecnico.TerzoResponsabile.ProvinciaSedeLegale);
            txtToSign.Add("Nome : " + rapportoTecnico.TerzoResponsabile.Nome);
            txtToSign.Add("Cognome : " + rapportoTecnico.TerzoResponsabile.Cognome);
            txtToSign.Add("Indirizzo : " + rapportoTecnico.TerzoResponsabile.Indirizzo);
            txtToSign.Add("Civico : " + rapportoTecnico.TerzoResponsabile.Civico);
            txtToSign.Add("CodiceCatastaleComune : " + rapportoTecnico.TerzoResponsabile.CodiceCatastaleComune);
            txtToSign.Add("Email : " + rapportoTecnico.TerzoResponsabile.Email);
            txtToSign.Add("EmailPec : " + rapportoTecnico.TerzoResponsabile.EmailPec);
            txtToSign.Add("PartitaIVA : " + rapportoTecnico.TerzoResponsabile.PartitaIVA);
            txtToSign.Add("CodiceFiscale : " + rapportoTecnico.TerzoResponsabile.CodiceFiscale);
            txtToSign.Add("NumeroIscrizioneCCIAA : " + rapportoTecnico.TerzoResponsabile.NumeroIscrizioneCCIAA);
            txtToSign.Add("ProvinciaIscrizioneCCIAA : " + rapportoTecnico.TerzoResponsabile.ProvinciaIscrizioneCCIAA);
            
            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("Impresa Manutentrice ");
            if (rapportoTecnico.ImpresaManutentrice.TipoSoggetto != null)
            {
                txtToSign.Add("TipoSoggetto : " + helper.TipoSoggetto(rapportoTecnico.ImpresaManutentrice.TipoSoggetto));
            }
            txtToSign.Add("NomeAzienda : " + rapportoTecnico.ImpresaManutentrice.NomeAzienda);
            txtToSign.Add("IndirizzoSedeLegale : " + rapportoTecnico.ImpresaManutentrice.IndirizzoSedeLegale);
            txtToSign.Add("CapSedeLegale : " + rapportoTecnico.ImpresaManutentrice.CapSedeLegale);
            txtToSign.Add("CittaSedeLegale : " + rapportoTecnico.ImpresaManutentrice.CittaSedeLegale);
            txtToSign.Add("NumeroCivicoSedeLegale : " + rapportoTecnico.ImpresaManutentrice.NumeroCivicoSedeLegale);
            txtToSign.Add("ProvinciaSedeLegale : " + rapportoTecnico.ImpresaManutentrice.ProvinciaSedeLegale);
            txtToSign.Add("Nome : " + rapportoTecnico.ImpresaManutentrice.Nome);

            txtToSign.Add("Cognome : " + rapportoTecnico.ImpresaManutentrice.Cognome);

            txtToSign.Add("Indirizzo : " + rapportoTecnico.ImpresaManutentrice.Indirizzo);
            txtToSign.Add("Civico : " + rapportoTecnico.ImpresaManutentrice.Civico);
            txtToSign.Add("CodiceCatastaleComune : " + rapportoTecnico.ImpresaManutentrice.CodiceCatastaleComune);
            txtToSign.Add("Email : " + rapportoTecnico.ImpresaManutentrice.Email);
            txtToSign.Add("EmailPec : " + rapportoTecnico.ImpresaManutentrice.EmailPec);
            txtToSign.Add("PartitaIVA : " + rapportoTecnico.ImpresaManutentrice.PartitaIVA);
            txtToSign.Add("CodiceFiscale : " + rapportoTecnico.ImpresaManutentrice.CodiceFiscale);
            txtToSign.Add("NumeroIscrizioneCCIAA : " + rapportoTecnico.ImpresaManutentrice.NumeroIscrizioneCCIAA);

            txtToSign.Add("ProvinciaIscrizioneCCIAA : " + rapportoTecnico.ImpresaManutentrice.ProvinciaIscrizioneCCIAA);
            
            #endregion

            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("fDichiarazioneConformita : " + rapportoTecnico.fDichiarazioneConformita);
            txtToSign.Add("fLibrettoImpiantoPresente : " + rapportoTecnico.fLibrettoImpiantoPresente);
            txtToSign.Add("fUsoManutenzioneGeneratore : " + rapportoTecnico.fUsoManutenzioneGeneratore);
            txtToSign.Add("fLibrettoImpiantoCompilato : " + rapportoTecnico.fLibrettoImpiantoCompilato);
            txtToSign.Add("DurezzaAcqua : " + rapportoTecnico.DurezzaAcqua);

            txtToSign.Add("TrattamentoRiscaldamento : " + helper.Presente_Assente(rapportoTecnico.TrattamentoRiscaldamento));
            foreach (var item in rapportoTecnico.TipoTrattamentoRiscaldamento)
            {
                txtToSign.Add("     ***** TipoTrattamentoRiscaldamento : " + helper.TipoTrattamentoRiscaldamento(item));
            }

            txtToSign.Add("TrattamentoACS : " + helper.Presente_Assente(rapportoTecnico.TrattamentoACS)); 
            foreach (var item in rapportoTecnico.TipoTrattamentoACS)
            {
                txtToSign.Add("     ***** TipoTrattamentoACS : " + helper.TipoTrattamentoACS(item));
            }

            txtToSign.Add("LocaleInstallazioneIdoneo : " + helper.Classificabile_SiNo(rapportoTecnico.LocaleInstallazioneIdoneo));
            txtToSign.Add("DimensioniApertureAdeguate : " + helper.Classificabile_SiNo(rapportoTecnico.DimensioniApertureAdeguate));
            txtToSign.Add("ApertureLibere : " + helper.Classificabile_SiNo(rapportoTecnico.ApertureLibere));
            txtToSign.Add("LineeElettricheIdonee : " + helper.Classificabile_SiNo(rapportoTecnico.LineeElettricheIdonee));
            txtToSign.Add("CaminoCanaleFumoIdonei : " + helper.Classificabile_SiNo(rapportoTecnico.CaminoCanaleFumoIdonei));
            txtToSign.Add("CapsulaInsonorizzataIdonea : " + helper.Classificabile_SiNo(rapportoTecnico.CapsulaInsonorizzataIdonea));
            txtToSign.Add("TenutaImpiantoIdraulico : " + helper.Classificabile_SiNo(rapportoTecnico.TenutaImpiantoIdraulico));
            txtToSign.Add("TenutaCircuitoOlioIdonea : " + helper.Classificabile_SiNo(rapportoTecnico.TenutaCircuitoOlioIdonea));
            txtToSign.Add("FunzionalitàScambiatoreSeparazione : " + helper.Classificabile_SiNo(rapportoTecnico.FunzionalitàScambiatoreSeparazione));

            txtToSign.Add("CodiceProgressivo: " + rapportoTecnico.CodiceProgressivo);
            String datainst = rapportoTecnico.DataInstallazione.HasValue ? rapportoTecnico.DataInstallazione.Value.ToShortDateString() : "";
            txtToSign.Add("DataInstallazione: " + datainst);
            txtToSign.Add("Fabbricante: " + rapportoTecnico.Fabbricante);
            txtToSign.Add("Modello: " + rapportoTecnico.Modello);
            txtToSign.Add("Matricola: " + rapportoTecnico.Matricola);

            txtToSign.Add("PotenzaElettricaMorsetti: " + rapportoTecnico.PotenzaElettricaMorsetti);
            txtToSign.Add("PotenzaAssorbitaCombustibile: " + rapportoTecnico.PotenzaAssorbitaCombustibile);
            txtToSign.Add("PotenzaMassimoRecupero: " + rapportoTecnico.PotenzaMassimoRecupero);
            txtToSign.Add("PotenzaByPass: " + rapportoTecnico.PotenzaByPass);
            txtToSign.Add("ClimatizzazioneEstiva: " + rapportoTecnico.fClimatizzazioneEstiva);
            txtToSign.Add("ClimatizzazioneInvernale: " + rapportoTecnico.fClimatizzazioneInvernale);
            txtToSign.Add("ProduzioneACS: " + rapportoTecnico.fProduzioneACS);
            if (rapportoTecnico.TipologiaFluidoTermoVettore != null)
            {
                txtToSign.Add("TipologiaFluidoTermoVettore: " + helper.TipologiaFluidoTermoVettore((int)rapportoTecnico.TipologiaFluidoTermoVettore));
            }
            
            txtToSign.Add("AltroFluidoTermoVettore: " + rapportoTecnico.AltroFluidoTermoVettore);


            txtToSign.Add("PotenzaAiMorsetti: " + rapportoTecnico.PotenzaAiMorsetti);
            txtToSign.Add("TemperaturaAriaComburente: " + rapportoTecnico.TemperaturaAriaComburente);
            txtToSign.Add("TemperaturaAcquaIngresso: " + rapportoTecnico.TemperaturaAcquaIngresso);
            txtToSign.Add("TemperaturaAcquauscita: " + rapportoTecnico.TemperaturaAcquauscita);
            txtToSign.Add("TemperaturaAcquaMotore: " + rapportoTecnico.TemperaturaAcquaMotore);
            txtToSign.Add("TemperaturaFumiMonte: " + rapportoTecnico.TemperaturaFumiMonte);
            txtToSign.Add("TemperaturaFumiValle: " + rapportoTecnico.TemperaturaFumiValle);
            txtToSign.Add("EmissioneMonossido: " + rapportoTecnico.EmissioneMonossido);


            txtToSign.Add("SovrafrequenzaSogliaInterv1: " +     rapportoTecnico.SovrafrequenzaSogliaInterv1);
            txtToSign.Add("SovrafrequenzaSogliaInterv2: " +     rapportoTecnico.SovrafrequenzaSogliaInterv2);
            txtToSign.Add("SovrafrequenzaSogliaInterv3: " +     rapportoTecnico.SovrafrequenzaSogliaInterv3);
            txtToSign.Add("SovrafrequenzaTempoInterv1: " +      rapportoTecnico.SovrafrequenzaTempoInterv1);
            txtToSign.Add("SovrafrequenzaTempoInterv2: " +      rapportoTecnico.SovrafrequenzaTempoInterv2);
            txtToSign.Add("SovrafrequenzaTempoInterv3: " +      rapportoTecnico.SovrafrequenzaTempoInterv3);
            txtToSign.Add("SottofrequenzaSogliaInterv1: " +     rapportoTecnico.SottofrequenzaSogliaInterv1);
            txtToSign.Add("SottofrequenzaSogliaInterv2: " +     rapportoTecnico.SottofrequenzaSogliaInterv2);
            txtToSign.Add("SottofrequenzaSogliaInterv3: " +     rapportoTecnico.SottofrequenzaSogliaInterv3);
            txtToSign.Add("SottofrequenzaTempoInterv1: " +      rapportoTecnico.SottofrequenzaTempoInterv1);
            txtToSign.Add("SottofrequenzaTempoInterv2: " +      rapportoTecnico.SottofrequenzaTempoInterv2);
            txtToSign.Add("SottofrequenzaTempoInterv3: " +      rapportoTecnico.SottofrequenzaTempoInterv3);
            txtToSign.Add("SovratensioneSogliaInterv1: " +      rapportoTecnico.SovratensioneSogliaInterv1);
            txtToSign.Add("SovratensioneSogliaInterv2: " +      rapportoTecnico.SovratensioneSogliaInterv2);
            txtToSign.Add("SovratensioneSogliaInterv3: " +      rapportoTecnico.SovratensioneSogliaInterv3);
            txtToSign.Add("SovratensioneTempoInterv1: " +       rapportoTecnico.SovratensioneTempoInterv1);
            txtToSign.Add("SovratensioneTempoInterv2: " +       rapportoTecnico.SovratensioneTempoInterv2);
            txtToSign.Add("SovratensioneTempoInterv3: " +       rapportoTecnico.SovratensioneTempoInterv3);
            txtToSign.Add("SottotensioneSogliaInterv1: " +      rapportoTecnico.SottotensioneSogliaInterv1);
            txtToSign.Add("SottotensioneSogliaInterv2: " +      rapportoTecnico.SottotensioneSogliaInterv2);
            txtToSign.Add("SottotensioneSogliaInterv3: " +      rapportoTecnico.SottotensioneSogliaInterv3);
            txtToSign.Add("SottotensioneTempoInterv1: " +       rapportoTecnico.SottotensioneTempoInterv1);
            txtToSign.Add("SottotensioneTempoInterv2: " +       rapportoTecnico.SottotensioneTempoInterv2);
            txtToSign.Add("SottotensioneTempoInterv3: " +       rapportoTecnico.SottotensioneTempoInterv3);

            #region tail rapporto tecnico
            txtToSign.Add("     ***** CheckList ***** ");
            foreach (var item in rapportoTecnico.CheckList)
            {
                helper.CheckList(item);
            }

            txtToSign.Add("Osservazioni : " + rapportoTecnico.Osservazioni);
            txtToSign.Add("Raccomandazioni : " + rapportoTecnico.Raccomandazioni);
            txtToSign.Add("Prescrizioni : " + rapportoTecnico.Prescrizioni);
            txtToSign.Add("fImpiantoFunzionante : " + rapportoTecnico.fImpiantoFunzionante);


            var dataManutenzioneConsigliata = rapportoTecnico.DataManutenzioneConsigliata.HasValue ? rapportoTecnico.DataManutenzioneConsigliata.Value.ToShortDateString() : "";
            var dataControllo = rapportoTecnico.DataControllo.HasValue ? rapportoTecnico.DataControllo.Value.ToShortDateString() : "";
            var oraArrivo = rapportoTecnico.OraArrivo.HasValue ? rapportoTecnico.OraArrivo.Value.ToShortTimeString() : "";
            var oraPartenza = rapportoTecnico.OraPartenza.HasValue ? rapportoTecnico.OraPartenza.Value.ToShortTimeString() : "";



            txtToSign.Add("DataManutenzioneConsigliata : " + dataManutenzioneConsigliata);
            txtToSign.Add("DataControllo : " + dataControllo);
            txtToSign.Add("OraArrivo : " + oraArrivo);
            txtToSign.Add("OraPartenza : " + oraPartenza);
            txtToSign.Add("TecnicoIntervento : " + rapportoTecnico.TecnicoIntervento);

            txtToSign.Add("-----------------------------------------------------");
            txtToSign.Add("BolliniCalorePulito");

            foreach (var item in rapportoTecnico.BolliniCalorePulito)
            {
                txtToSign.Add(item);
            }

            #endregion

            return txtToSign;
        }
    }
}
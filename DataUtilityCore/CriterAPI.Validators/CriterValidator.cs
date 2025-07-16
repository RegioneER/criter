using Criter.Libretto;
using Criter.Rapporti;
using CriterAPI.Codici;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Criter.Anagrafica;

namespace CriterAPI.Validators
{
    public class CriterValidator
    {
        public static bool IsInArrayOfValues(List<int> values, int value)
        {
            return values.Contains(value);
        }
        private static bool IsInArrayOfNullableValues(List<int> list, int? integer)
        {
            bool result = true;
            if (integer.HasValue && !IsInArrayOfValues(list, integer.Value))
                result = false;
            return result;
        }
        public static bool IsInArrayOfValues(List<int> values, List<int> values2)
        {
            bool result = true;
            foreach (int item in values2)
            {
                if (!values.Contains(item))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public static String ValidateRapportoControlloTecnico_CG(POMJ_RapportoControlloTecnico_CG rapporto)
        {
            String result = "Json Rapporto controllo tecnico CG corretto";

            if (!IsInArrayOfValues(new List<int> { 1, 2 }, rapporto.TipologiaControllo))
                result = "Tipologia Controllo non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, rapporto.StatoRapportoDiControllo))
                result = "Stato Rapporto Di Controllo non valido";
            else if (CodiciComuni.IsComuneValid(rapporto.Comune))
                result = "Codice Catastale Comuni non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, rapporto.TipologiaResponsabile))
                result = "Tipologia Responsabile non valido";
            else if (!IsAnagraficaSoggettoIsValid(rapporto.ResponsabileImpianto))
                result = "Responsabile Impianto non valido";
            else if (!IsAnagraficaSoggettoIsValid(rapporto.TerzoResponsabile))
                result = "Terzo Responsabile non valido";
            else if (!IsAnagraficaSoggettoIsValid(rapporto.ImpresaManutentrice))
                result = "Impresa Manutentrice non valida";
            else if (!IsInArrayOfNullableValues(new List<int> { 1, 2, 3 }, rapporto.TrattamentoRiscaldamento))
                result = "Trattamento Riscaldamento non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, rapporto.TipoTrattamentoRiscaldamento))
                result = "Tipo Trattamento Riscaldamento non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { 1, 2, 3 }, rapporto.TrattamentoACS))
                result = "Trattamento ACS non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.LocaleInstallazioneIdoneo))
                result = "Locale Installazione Idoneo non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.DimensioniApertureAdeguate))
                result = "Dimensioni Aperture Adeguate non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.ApertureLibere))
                result = "Aperture Libere non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.LineeElettricheIdonee))
                result = "Linee Elettriche Idonee non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.CaminoCanaleFumoIdonei))
                result = "Camino Canale Fumo Idonei non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.CapsulaInsonorizzataIdonea))
                result = "Capsula Insonorizzata Idonea non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.TenutaImpiantoIdraulico))
                result = "Tenuta Impianto Idraulico non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.TenutaCircuitoOlioIdonea))
                result = "Tenuta Circuito Olio Idonea non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.AssenzaPerditeCombustibile))
                result = "Tenuta Circuito Alimentazione Combustibile Idonea non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.FunzionalitàScambiatoreSeparazione))
                result = "Funzionalità Scambiatore Separazione Idonea non valido";

            /********************************************************************************/
            else if (!IsInArrayOfNullableValues(new List<int> { 1, 2, 3, 4, 5, 6 }, rapporto.TipologiaFluidoTermoVettore))
                result = "Tipologia Fluido TermoVettore non valido";

            /***********************************************************************************/

            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4, 5, 6 }, rapporto.CheckList))
                result = "CheckList non valido";
           

            return result;
        }

        public static String ValidateRapportoControlloTecnico_GF(POMJ_RapportoControlloTecnico_GF rapporto)
        {
            String result = "Json Rapporto controllo tecnico GF corretto";

            if (!IsInArrayOfValues(new List<int> { 1, 2 }, rapporto.TipologiaControllo))
                result = "Tipologia Controllo non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, rapporto.StatoRapportoDiControllo))
                result = "Stato Rapporto Di Controllo non valido";
            else if (CodiciComuni.IsComuneValid(rapporto.Comune))
                result = "Codice Catastale Comuni non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, rapporto.TipologiaResponsabile))
                result = "Tipologia Responsabile non valido";
            else if (!IsAnagraficaSoggettoIsValid(rapporto.ResponsabileImpianto))
                result = "Responsabile Impianto non valido";
            else if (!IsAnagraficaSoggettoIsValid(rapporto.TerzoResponsabile))
                result = "Terzo Responsabile non valido";
            else if (!IsAnagraficaSoggettoIsValid(rapporto.ImpresaManutentrice))
                result = "Impresa Manutentrice non valida";
            else if (!IsInArrayOfNullableValues(new List<int> { 1, 2, 3 }, rapporto.TrattamentoRiscaldamento))
                result = "Trattamento Riscaldamento non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, rapporto.TipoTrattamentoRiscaldamento))
                result = "Tipo Trattamento Riscaldamento non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, rapporto.TipoTrattamentoACS))
                result = "Trattamento ACS non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.LocaleInstallazioneIdoneo))
                result = "Locale Installazione Idoneo non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.DimensioniApertureAdeguate))
                result = "Dimensioni Aperture Adeguate non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.ApertureLibere))
                result = "Aperture Libere non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.LineeElettricheIdonee))
                result = "Linee Elettriche Idonee non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.CoibentazioniIdonee))
                result = "Camino Canale Fumo Idonei non valido";

            /********************************************************************************/
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4 }, rapporto.TipologiaMacchineFrigorifere))
                result = "Tipologia Macchine Frigorifere non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.AssenzaPerditeRefrigerante))
                result = "Assenza Perdite Refrigerante non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.FiltriPuliti))
                result = "Filtri Puliti non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.LeakDetector))
                result = "LeakDetector non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.ScambiatoriLiberi))
                result = "Scambiatori Liberi non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.ParametriTermodinamici))
                result = "Parametri Termodinamici non valido";

            /***********************************************************************************/

            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4, 5, 6 }, rapporto.CheckList))
                result = "CheckList non valido";


            return result;
        }

        public static String ValidateRapportoControlloTecnico_GT(POMJ_RapportoControlloTecnico_GT rapporto)
        {
            String result = "Json Rapporto controllo tecnico GT corretto";

            if (!IsInArrayOfValues(new List<int> { 1, 2 }, rapporto.TipologiaControllo))
                result = "Tipologia Controllo non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, rapporto.StatoRapportoDiControllo))
                result = "Stato Rapporto Di Controllo non valido";
            else if (CodiciComuni.IsComuneValid(rapporto.Comune))
                result = "Codice Catastale Comuni non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, rapporto.TipologiaResponsabile))
                result = "Tipologia Responsabile non valido";
            else if (!IsAnagraficaSoggettoIsValid(rapporto.ResponsabileImpianto))
                result = "Responsabile Impianto non valido";
            else if (!IsAnagraficaSoggettoIsValid(rapporto.TerzoResponsabile))
                result = "Terzo Responsabile non valido";
            else if (!IsAnagraficaSoggettoIsValid(rapporto.ImpresaManutentrice))
                result = "Impresa Manutentrice non valida";
            else if (!IsInArrayOfNullableValues(new List<int> { 1, 2, 3 }, rapporto.TrattamentoRiscaldamento))
                result = "Trattamento Riscaldamento non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, rapporto.TipoTrattamentoRiscaldamento))
                result = "Tipo Trattamento Riscaldamento non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { 1, 2, 3 }, rapporto.TrattamentoACS))
                result = "Locale Installazione Idoneo non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, rapporto.TipoTrattamentoACS))
                result = "Trattamento ACS non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.LocaleInstallazioneIdoneo))
                result = "Locale Installazione Idoneo non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.GeneratoriIdonei))
                result = "Generatori Idonei non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.DimensioniApertureAdeguate))
                result = "Dimensioni Aperture Adeguate non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.ApertureLibere))
                result = "Aperture Libere non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.AssenzaPerditeCombustibile))
                result = "Assenza Perdite Combustibile non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.TenutaImpiantoIdraulico))
                result = "Tenuta Impianto Idraulico non valido";

            /********************************************************************************/
            else if (!IsInArrayOfNullableValues(new List<int> { 1, 2, 3, 4 }, rapporto.TipologiaGruppiTermici))
                result = "Tipologia Macchine Frigorifere non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { 1, 2, 3, 4 }, rapporto.TipologiaGeneratoriTermici))
                result = "Tipologia Generatori Termici non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 }, rapporto.TipologiaCombustibile))
                result = "Assenza Perdite Refrigerante non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.DispositiviComandoRegolazione))
                result = "Dispositivi Comando Regolazione non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.DispositiviSicurezza))
                result = "Dispositivi Sicurezza non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.ValvolaSicurezzaSovrappressione))
                result = "Valvola Sicurezza Sovrappressione non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.ScambiatoreFumiPulito))
                result = "Scambiatore Fumi Pulito non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.RiflussoProdottiCombustione))
                result = "Riflusso Prodotti Combustione non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.ConformitaUNI10389))
                result = "Conformita UNI10389 non valido";

            /***********************************************************************************/

            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4, 5, 6 }, rapporto.CheckList))
                result = "CheckList non valido";


            return result;
        }

        public static String ValidateRapportoControlloTecnico_SC(POMJ_RapportoControlloTecnico_SC rapporto)
        {
            String result = "Json Rapporto controllo tecnico SC corretto";

            if (!IsInArrayOfValues(new List<int> { 1, 2 }, rapporto.TipologiaControllo))
                result = "Tipologia Controllo non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, rapporto.StatoRapportoDiControllo))
                result = "Stato Rapporto Di Controllo non valido";
            else if (CodiciComuni.IsComuneValid(rapporto.Comune))
                result = "Codice Catastale Comuni non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, rapporto.TipologiaResponsabile))
                result = "Tipologia Responsabile non valido";
            else if (!IsAnagraficaSoggettoIsValid(rapporto.ResponsabileImpianto))
                result = "Responsabile Impianto non valido";
            else if (!IsAnagraficaSoggettoIsValid(rapporto.TerzoResponsabile))
                result = "Terzo Responsabile non valido";
            else if (!IsAnagraficaSoggettoIsValid(rapporto.ImpresaManutentrice))
                result = "Impresa Manutentrice non valida";
            else if (!IsInArrayOfNullableValues(new List<int> { 1, 2, 3 }, rapporto.TrattamentoRiscaldamento))
                result = "Trattamento Riscaldamento non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, rapporto.TipoTrattamentoRiscaldamento))
                result = "Tipo Trattamento Riscaldamento non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { 1, 2, 3 }, rapporto.TrattamentoACS))
                result = "Locale Installazione Idoneo non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, rapporto.TipoTrattamentoACS))
                result = "Trattamento ACS non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.LocaleInstallazioneIdoneo))
                result = "Locale Installazione Idoneo non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.LineeElettricheIdonee))
                result = "Linee Elettriche Idonee non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.StatoCoibentazioniIdonee))
                result = "Stato Coibentazioni Idonee non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.AssenzaPerditeCombustibile))
                result = "Assenza Perdite Combustibile non valido";

            /********************************************************************************/
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4, 5, 6 }, rapporto.TipologiaFluidoTermoVettore))
                result = "Tipologia Fluido TermoVettore non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.PotenzaCompatibileProgetto))
                result = "Potenza Compatibile Progetto non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { -1, 0, 1 }, rapporto.AssenzaTrafilamenti))
                result = "Assenza Trafilamenti non valido";

            /***********************************************************************************/

            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4, 5, 6 }, rapporto.CheckList))
                result = "CheckList non valido";


            return result;
        }

        public static String ValidateLibrettoImpianto(POMJ_LibrettoImpianto librettoImpianto)
        {
            String result = "Json Libretto Impianto corretto";

            if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4 }, librettoImpianto.StatoLibrettoImpianto))
                result = "StatoLibrettoImpianto non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4 }, librettoImpianto.TipologiaIntervento))
                result = "TipologiaIntervento non valido";
            else if (!CodiciComuni.IsComuneValid(librettoImpianto.CodiceCatastaleComune))
                result = "Codice Catastale Comuni non valido";
            else if (librettoImpianto.DestinazioneUso.HasValue && !IsInArrayOfValues(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 }, librettoImpianto.DestinazioneUso.Value))
                result = "Destinazione d'uso non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, librettoImpianto.TipologiaFluidoVettore))
                result = "Tipologia Fluido Vettore non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4, 5, 6, 7 }, librettoImpianto.TipologiaGeneratori))
                result = "Tipologia GeneratoriVettore non valido";
            else if (librettoImpianto.TipologiaResponsabile.HasValue && !IsInArrayOfValues(new List<int> { 1, 2, 3 }, librettoImpianto.TipologiaResponsabile.Value))
                result = "Tipologia Responsabile d'uso non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, librettoImpianto.TipologiaTrattamentoAcquaInvernale))
                result = "Tipologia Trattamento Acqua Invernale non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { 1, 2 }, librettoImpianto.TipologiaProtezioneGelo))
                result = "Tipologia Protezione Gelo non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4 }, librettoImpianto.TipologiaTrattamentoAcquaAcs))
                result = "Tipologia Trattamento Acqua Calda Sanitari non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { 1, 2, 3 }, librettoImpianto.TipologiaCircuitoRaffreddamento))
                result = "Tipologia Circuito Raffreddamento non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { 1, 2, 3 }, librettoImpianto.TipologiaAcquaAlimento))
                result = "Tipologia Acqua Alimento non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, librettoImpianto.TipologiaTrattamentoAcquaEstiva))
                result = "Tipologia Trattamento Acqua Estiva non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4 }, librettoImpianto.TipologiaFiltrazione))
                result = "Tipologia Filtrazione non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4 }, librettoImpianto.TipologiaAddolcimentoAcqua))
                result = "Tipologia Addolcimento Acqua non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4, 5 }, librettoImpianto.TipologiaCondizionamentoChimico))
                result = "Tipologia Condizionamento Chimico non valido";
            else if (!GruppiTermiciCaldaieAreValid(librettoImpianto.GruppiTermiciCaldaie))
                result = "Errore nella sezione Gruppi Termici Caldaie";
            else if (!CogeneratoriTrigeneratoriAreValid(librettoImpianto.CogeneratoriTrigeneratori))
                result = "Errore nella sezione Cogeneratori Trigeneratori";
            else if (!IsInArrayOfNullableValues(new List<int> { 1, 2, 3 }, librettoImpianto.TipologiaTermostatoZona))
                result = "Tipologia Termostato Zona non valido";
            else if (!IsInArrayOfNullableValues(new List<int> { 1, 2 }, librettoImpianto.TipologiaSistemaContabilizzazione))
                result = "Tipologia Sistema Contabilizzazione non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4 }, librettoImpianto.TipologiaSistemaDistribuzione))
                result = "Tipologia Sistema Distribuzione non valido";
            else if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 }, librettoImpianto.TipologiaSistemiEmissione))
                result = "Tipologia Sistemi Emissione non valido";
            else if (!TorriEvaporativeAreValid(librettoImpianto.TorriEvaporative))
                result = "Errore nella sezione Torri Evaporative";
            else if (!RaffreddatoriLiquidoAreValid(librettoImpianto.RaffreddatoriLiquido))
                result = "Errore nella sezione Raffreddatori Liquido";
            else if (!RecuperatoriCaloreAreValid(librettoImpianto.RecuperatoriCalore))
                result = "Errore nella sezione Recuperatori Calore";
            else if (!ConsumiCombustibileAreValid(librettoImpianto.ConsumiCombustibile))
                result = "Errore nella sezione Consumi Combustibile";
            else if (!ConsumiAcquaAreValid(librettoImpianto.ConsumiAcqua))
                result = "Errore nella sezione Consumi Acqua";
            else if (!ConsumiProdottiChimiciAreValid(librettoImpianto.ConsumiProdottiChimici))
                result = "Errore nella sezione Consumi Prodotti Chimici";

            return result;
        }

        private static bool IsAnagraficaSoggettoIsValid(POMJ_AnagraficaSoggetti responsabileImpianto)
        {
            bool isValid = true;

            if (!IsInArrayOfNullableValues(new List<int> { 1, 2, 3, 4 }, responsabileImpianto.TipoSoggetto))
                isValid = false;

            // prima dobbiamo mettere a nullable i campi

            //else if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, responsabileImpianto.FormaGiuridica))
            //    isValid = false;
            //else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, responsabileImpianto.FunzioneSoggetto))
            //    isValid = false;
            //else if (!IsInArrayOfValues(new List<int> { 1, 2 }, responsabileImpianto.AbilitazioniSoggetto))
            //    isValid = false;
            //else if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, responsabileImpianto.RuoliSoggetti))
            //    isValid = false;

            return isValid;
        }

        private static bool ConsumiProdottiChimiciAreValid(List<POMJ_ConsumoProdottiChimici> consumiProdottiChimici)
        {
            bool isValid = true;
            foreach (POMJ_ConsumoProdottiChimici item in consumiProdottiChimici)
            {
                if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4 }, item.UnitaMisura))
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        private static bool ConsumiAcquaAreValid(List<POMJ_ConsumoAcqua> consumiAcqua)
        {
            bool isValid = true;
            foreach (POMJ_ConsumoAcqua item in consumiAcqua)
            {
                if (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4 }, item.UnitaMisura))
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        private static bool ConsumiCombustibileAreValid(List<POMJ_ConsumoCombustibile> consumiCombustibile)
        {
            bool isValid = true;
            foreach (POMJ_ConsumoCombustibile item in consumiCombustibile)
            {
                if ((!IsInArrayOfValues(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 }, item.TipologiaCombustibile)) ||
                    (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4 }, item.UnitaMisura)))
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        private static bool RecuperatoriCaloreAreValid(List<POMJ_RecuperatoreCalore> recuperatoriCalore)
        {
            bool isValid = true;
            foreach (POMJ_RecuperatoreCalore item in recuperatoriCalore)
            {
                if (!IsInArrayOfValues(new List<int> { 1, 2 }, item.ModalitaInstallazioneRecuperatoriCalore))
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        private static bool RaffreddatoriLiquidoAreValid(List<POMJ_RaffreddatoreLiquido> raffreddatoriLiquido)
        {
            bool isValid = true;
            foreach (POMJ_RaffreddatoreLiquido item in raffreddatoriLiquido)
            {
                if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, item.TipologiaVentilatori))
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        private static bool TorriEvaporativeAreValid(List<POMJ_TorreEvaporativa> torriEvaporative)
        {
            bool isValid = true;
            foreach (POMJ_TorreEvaporativa item in torriEvaporative)
            {
                if (!IsInArrayOfValues(new List<int> { 1, 2, 3 }, item.TipologiaVentilatori))
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        private static bool CogeneratoriTrigeneratoriAreValid(List<POMJ_Cogeneratore> cogeneratoriTrigeneratori)
        {
            bool isValid = true;
            foreach (POMJ_Cogeneratore item in cogeneratoriTrigeneratori)
            {
                if ((!IsInArrayOfValues(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 }, item.TipologiaCombustibile)) ||
                    (!IsInArrayOfValues(new List<int> { 1, 2 }, item.TipologiaCogeneratore)))
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        private static bool GruppiTermiciCaldaieAreValid(List<POMJ_GruppiTermici> gruppiTermiciCaldaie)
        {
            bool isValid = true;
            foreach (POMJ_GruppiTermici item in gruppiTermiciCaldaie)
            {
                if ((!IsInArrayOfValues(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 }, item.TipologiaCombustibile)) ||
                    (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4, 5, 6 }, item.TipologiaFluidoTermoVettore)) ||
                    (!IsInArrayOfValues(new List<int> { 1, 2, 3, 4 }, item.TipologiaGruppiTermici)))
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }
    }
}
using Criter.Anagrafica;
using Criter.Libretto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Criter.Rapporti
{
    /// <summary>
    /// RAPPORTO DI CONTROLLO TECNICO TIPO 2 (GRUPPO FRIGO)
    /// </summary>
    public class POMJ_RapportoControlloTecnico_GF
    {
        /// <summary>
        /// Chiave assegnata per le chiamate API - <font color='red'><b>obbligatorio</b></font>
        /// </summary>
        [Required(ErrorMessage = "Criter Api Key obbligatorio")]
        public string CriterAPIKey { get; set; }

        /// <summary>
        /// Codice Soggetto attribuito dal Sistema Criter in fase di Registrazione - <font color='red'><b>obbligatorio</b></font>
        /// Il Codice Soggetto attribuito all'azienda manutentrice è nel formato ad esempio 000001-0001
        /// La prima parte del codice rappresenta l'azienda manutentrice (000001), mentre la seconda parte rappresenta il singolo manutentore (0001)
        /// </summary>
        [Required(ErrorMessage = "Codice soggetto obbligatorio")]
        public string CodiceSoggetto { get; set; }
        /// <summary>
        /// Chiave assegnata alle aziende manutentrici per le chiamate API - <font color='red'><b>obbligatorio</b></font>
        /// </summary>
        [Required(ErrorMessage = "Criter Api Key Soggetto obbligatorio")]
        public string CriterAPIKeySoggetto { get; set; }
        /// <summary>
        /// Codice di targatura dell'impianto - <font color='red'><b>obbligatorio</b></font>
        /// </summary>
        [Required(ErrorMessage = "Codice targatura impianto obbligatorio")]
        public POMJ_TargaturaImpianto CodiceTargaturaImpianto { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Di efficienza energetica</item>
        /// <item>2	Funzionale e manutenzione</item>
        /// </list>
        /// </summary>
        [Range(1, 2, ErrorMessage = "Tipologia di controllo valori consentiti compresi da 1 a 2")]
        public int TipologiaControllo { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Bozza</item>
        /// </list>
        /// </summary>
        [Required(ErrorMessage = "Stato rapporto campo obbligatorio")]
        [Range(1, 1, ErrorMessage = "Stato rapporto valore consentito 1 (rapporto in bozza)")]
        public int StatoRapportoDiControllo { get; set; }
        /**************************************************************************
         * 
         * A. DATI IDENTIFICATIVI - Impianto
         * 
         * ***********************************************************************/
        /// <summary>
        /// Valore fornito dal fabbricante che è riportato all‘interno 
        /// del libretto di installazione, uso e manutenzione
        /// </summary>
        public decimal? PotenzaTermicaNominaleTotaleMax { get; set; }
        /// <summary>
        /// Codice Catastale
        /// Vedi <a href="http://criter.pomiager.com/ApiDocumentation/Codici/CodiciComuni.txt">questo File</a> Per i codici catastali dei comuni
        /// </summary>
        public string Comune { get; set; }
        /// <summary>
        /// Indirizzo dell'impianto
        /// </summary>
        public string Indirizzo { get; set; }
        /// <summary>
        /// Numero civico dell'impianto
        /// </summary>
        public string Civico { get; set; }
        /// <summary>
        /// Palazzo dell'impianto
        /// </summary>
        public string Palazzo { get; set; }
        /// <summary>
        /// Scala dell'impianto
        /// </summary>
        public string Scala { get; set; }
        /// <summary>
        /// Interno dell'impianto
        /// </summary>
        public string Interno { get; set; }

        /**************************************************************************
        * 
        * A. DATI IDENTIFICATIVI - Responsabile dell'impianto
        * 
        * ***********************************************************************/
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Proprietario</item>
        /// <item>2	Occupante</item>
        /// <item>3 Amministratore di condominio</item>
        /// </list>	
        /// </summary>
        [Range(1, 3, ErrorMessage = "Tipologia di responsabile valori consentiti compresi da 1 a 3")]
        public int TipologiaResponsabile { get; set; }
        /// <summary>
        /// Dati del responsabile impianto
        /// </summary>
        public POMJ_AnagraficaSoggetti ResponsabileImpianto { get; set; }

        /**************************************************************************
        * 
        * A. DATI IDENTIFICATIVI - Terzo Responsabile (se nominato)
        * 
        ***************************************************************************/
        /// <summary>
        /// Dati del terzo responsabile
        /// </summary>
        public POMJ_AnagraficaSoggetti TerzoResponsabile { get; set; }

        /**************************************************************************
        * 
        * A. DATI IDENTIFICATIVI - Impresa manutentrice
        * 
        ***************************************************************************/
        /// <summary>
        /// Dati impresa manutentrice
        /// </summary>
        public POMJ_AnagraficaSoggetti ImpresaManutentrice { get; set; }

        /**************************************************************************
        * 
        * B. DOCUMENTAZIONE TECNICA A CORREDO
        * 
        ***************************************************************************/
        /// <summary>
        /// Dichiarazione di Conformità presente
        /// Booleano
        /// </summary>
        public bool fDichiarazioneConformita { get; set; }
        /// <summary>
        /// Libretto impianto presente
        /// booleano
        /// </summary>
        public bool fLibrettoImpiantoPresente { get; set; }
        /// <summary>
        /// Libretti uso/manutenzione generatore presenti
        /// booleano
        /// </summary>
        public bool fUsoManutenzioneGeneratore { get; set; }
        /// <summary>
        /// Libretto compilato in tutte le sue parti
        /// booleano
        /// </summary>
        public bool fLibrettoImpiantoCompilato { get; set; }

        /**************************************************************************
         * 
         * C. TRATTAMENTO DELL'ACQUA
         * 
         ***************************************************************************/
        /// <summary>
        /// Durezza Totale dell'acqua
        /// </summary>
        public decimal? DurezzaAcqua { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>0	Assente</item>
        /// <item>1	Presente</item>
        /// <item>2 Non Richiesto</item>
        /// </list>	
        /// </summary>
        [Range(0, 2, ErrorMessage = "Tipologia trattamento in Riscaldamento valori consentiti compresi da 0 a 2")]
        public int? TrattamentoRiscaldamento { get; set; }
        /// <summary>
        /// Tipo trattamento in riscaldamento
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Filtrazione</item>
        /// <item>2	Addolcimento</item>
        /// <item>3 Condizionamento chimico</item>
        /// </list>	
        /// </summary>
        public List<int> TipoTrattamentoRiscaldamento { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Filtrazione</item>
        /// <item>2	Addolcimento</item>
        /// <item>3 Condizionamento chimico</item>
        /// </list>	
        /// </summary>
        public List<int> TipoTrattamentoACS { get; set; }
        /// <summary>
        /// Tipo trattamento in Acs
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>0	Assente</item>
        /// <item>1	Presente</item>
        /// <item>2 Non Richiesto</item>
        /// </list>	
        /// </summary>
        [Range(0, 2, ErrorMessage = "Tipologia trattamento in Acs valori consentiti compresi da 0 a 2")]
        public int? TrattamentoACS { get; set; }
        /**************************************************************************
         * 
         * D. CONTROLLO DELL'IMPIANTO
         * 
         ***************************************************************************/
        /// <summary>
        /// Locale di installazione idoneo
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? LocaleInstallazioneIdoneo { get; set; }
        /// <summary>
        /// Dimensioni aperture di ventilazione adeguate
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? DimensioniApertureAdeguate { get; set; }
        /// <summary>
        /// Aperture di ventilazione libere da ostruzioni
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? ApertureLibere { get; set; }
        /// <summary>
        /// Linee elettriche idonee
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? LineeElettricheIdonee { get; set; }
        /// <summary>
        /// Coibentazioni idonee
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? CoibentazioniIdonee { get; set; }


        /**************************************************************************
        * 
        * E. CONTROLLO E VERIFICA ENERGETICA DEL GRUPPO FRIGO
        * 
        ***************************************************************************/
        /// <summary>
        /// Campo Precompilato
        /// </summary>
        public int CodiceProgressivo { get; set; }
        /// <summary>
        /// Campo Precompilato
        /// Nel caso in cui non si riesca a conoscere la data esatta di 
        /// installazione è indispensabile inserire una data verosimile, 
        /// che in caso di ispezione verrà verificata
        /// </summary>
        public DateTime? DataInstallazione { get; set; }
        /// <summary>
        /// Fabbricante
        /// </summary>
        public string Fabbricante { get; set; }
        /// <summary>
        /// Modello
        /// </summary>
        public string Modello { get; set; }
        /// <summary>
        /// Indicare il numero di matricola o il numero di 
        /// serie fornito dal fabbricante
        /// </summary>
        public string Matricola { get; set; }

        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Altro</item>
        /// <item>2	Verticale a colonne montanti</item>
        /// <item>3	Orizzontale a zone</item>
        /// <item>4	Canali d'aria</item>
        /// </list>	
        /// </summary>
        [Range(1, 4, ErrorMessage = "Tipologia sistema di distribuzione valori consentiti compresi da 1 a 4")]
        public int? TipologiaSistemaDistribuzione { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Diretta</item>
        /// <item>2	Indiretta</item>
        /// <item>3	Non applicabile</item>
        /// </list>	
        /// </summary>
        [Range(1, 3, ErrorMessage = "Tipologia contabilizzazione valori consentiti compresi da 1 a 3")]
        public int? TipologiaContabilizzazione { get; set; }
        
        /// <summary>
        /// Numero circuiti
        /// </summary>
        public decimal? NCircuitiTotali { get; set; }
        /// <summary>
        /// Potenza frigorifera nominale in raffrescamento (kW)
        /// </summary>
        public decimal? Potenzafrigorifera { get; set; }
        /// <summary>
        /// Potenza termica nominale in riscaldamento (kW)
        /// </summary>
        public decimal? PotenzaTermicaNominale { get; set; }
        /// <summary>
        /// Servizi di climatizzazione invernale presenti
        /// </summary>
        public bool fClimatizzazioneInvernale { get; set; }
        /// <summary>
        /// Servizi di climatizzazione estiva presenti
        /// </summary>
        public bool fClimatizzazioneEstiva { get; set; }
        /// <summary>
        /// Servizi di produzione acqua calda sanitaria presenti
        /// </summary>
        public bool fProduzioneACS { get; set; }
        /// <summary>
        /// Prova eseguita in modalità riscaldamento
        /// booleano
        /// </summary>
        public bool fRiscaldamento { get; set; }
        /// <summary>
        /// Prova eseguita in modalità raffrescamento
        /// booleano
        /// </summary>
        public bool fRaffrescamento { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Ad assorbimento per recupero di calore</item>
        /// <item>2	Ad assorbimento a fiamma diretta con combustibile</item>
        /// <item>3	A ciclo di compressione con motore elettrico</item>
        /// <item>4	A ciclo di compressione con motore endotermico</item>
        /// </list>	
        /// </summary>
        
        /// <summary>
        /// Contabilizzazione
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? Contabilizzazione { get; set; }
        /// <summary>
        /// Termoregolazione
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? Termoregolazione { get; set; }
        /// <summary>
        /// Corretto Funzionamento Contabilizzazione 
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? CorrettoFunzionamentoContabilizzazione { get; set; }
        /// <summary>
        /// Termoregolazione
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1 Ad assorbimento per recupero di calore</item>
        /// <item>2	Ad assorbimento a fiamma diretta con combustibile</item>
        /// <item>3 A ciclo di compressione con motore elettrico</item>
        /// <item>4 A ciclo di compressione con motore endotermico</item>
        /// </list>	
        /// </summary>
        [Range(1, 4, ErrorMessage = "Tipologia di macchine frigorifere valori consentiti compresi da 1 a 4")]
        public int TipologiaMacchineFrigorifere { get; set; }
        /// <summary>
        /// Assenza di perdite di gas refrigerante
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? AssenzaPerditeRefrigerante { get; set; }
        /// <summary>
        /// Filtri puliti
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? FiltriPuliti { get; set; }
        /// <summary>
        /// Presenza apparecchiatura automatica rilevazione 
        /// diretta fughe refrigerante (leak detector)
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? LeakDetector { get; set; }
        /// <summary>
        /// Scambiatori di calore puliti e liberi da incrostazioni
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? ScambiatoriLiberi { get; set; }
        /// <summary>
        /// Presenza apparecchiatura automatica rilevazione indiretta 
        /// fughe refrigerante (parametri termodinamici)
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? ParametriTermodinamici { get; set; }

        /**************************************************************************
        * 
        * E. CONTROLLO E VERIFICA ENERGETICA DEL GRUPPO FRIGO - VALORI MISURATI
        * 
        ***************************************************************************/
        /// <summary>
        /// Indicare il numero del circuito cui si riferisce il controllo
        /// </summary>
        public decimal? NCircuiti { get; set; }
        /// <summary>
        /// Valore della temperatura di surriscaldamento, espresso in gradi centigradi (°C)
        /// </summary>
        public decimal? TemperaturaSurriscaldamento { get; set; }
        /// <summary>
        /// Valore della temperatura di sottoraffreddamento, espresso in gradi centigradi (°C)
        /// </summary>
        public decimal? TemperaturaSottoraffreddamento { get; set; }
        /// <summary>
        /// Valore della temperatura di condensazione, espresso in gradi centigradi (°C)
        /// </summary>
        public decimal? TemperaturaCondensazione { get; set; }
        /// <summary>
        /// Valore della temperatura di evaporazione, espresso in gradi centigradi (°C)
        /// </summary>
        public decimal? TemperaturaEvaporazione { get; set; }
        /// <summary>
        /// Temperatura ingresso lato esterno
        /// Valore della temperatura di evaporazione, espresso in gradi centigradi (°C)
        /// </summary>
        public decimal? TInglatoEst { get; set; }
        /// <summary>
        /// Temperatura uscita lato esterno
        ///  Valore della temperatura di evaporazione, espresso in gradi centigradi (°C)
        /// </summary>
        public decimal? TUscLatoEst { get; set; }
        /// <summary>
        /// Temperatura ingresso lato utenze
        /// Valore della temperatura di evaporazione, espresso in gradi centigradi (°C)
        /// </summary>
        public decimal? TIngLatoUtenze { get; set; }
        /// <summary>
        /// Temperatura uscita lato utenze
        /// Valore della temperatura di evaporazione, espresso in gradi centigradi (°C)
        /// </summary>
        public decimal? TUscLatoUtenze { get; set; }
        /// <summary>
        /// Valore della Potenza assorbita, espressa in kW, in sede di controllo
        /// </summary>
        public decimal? PotenzaAssorbita { get; set; }
        /// <summary>
        /// Questo campo deve essere compilato quando è presente una torre di raffreddamento o un raffreddatore a fluido
        /// Valore della temperatura del fluido del circuito in uscita, rilevato in sede di controllo,
        /// espresso in gradi centigradi(°C)
        /// </summary>
        public decimal? TUscitaFluido { get; set; }
        /// <summary>
        /// Questo campo deve essere compilato quando è presente una torre di raffreddamento o un raffreddatore a fluido
        /// Valore della temperatura del bulbo umido in aria rilevato in sede di controllo, espresso 
        /// in gradi centigradi(°C)
        /// </summary>
        public decimal? TBulboUmidoAria { get; set; }
        /// <summary>
        /// Questo campo deve essere compilato quando è usato uno scambiatore di calore intermedio
        /// Valore della temperatura di ingresso lato esterno rilevato in sede di controllo, 
        /// espresso in gradi centigradi(°C)
        /// </summary>
        public decimal? TIngressoLatoEsterno { get; set; }
        /// <summary>
        /// Questo campo deve essere compilato quando è usato uno scambiatore di calore intermedio
        /// Valore della temperatura di ingresso lato esterno rilevato in sede di controllo, 
        /// espresso in gradi centigradi(°C)
        /// </summary>
        public decimal? TUscitaLatoEsterno { get; set; }
        /// <summary>
        /// Questo campo deve essere compilato quando è usato uno scambiatore di calore intermedio
        /// Valore della temperatura di ingresso lato esterno rilevato in sede di controllo, 
        /// espresso in gradi centigradi(°C)
        /// </summary>
        public decimal? TIngressoLatoMacchina { get; set; }
        /// <summary>
        /// Questo campo deve essere compilato quando è usato uno scambiatore di calore intermedio
        /// Valore della temperatura di ingresso lato esterno rilevato in sede di controllo, 
        /// espresso in gradi centigradi(°C)
        /// </summary>
        public decimal? TUscitaLatoMacchina { get; set; }

        /**************************************************************************
        * 
        * F. CHECK-LIST
        * 
        ***************************************************************************/
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>5 La sostituzione di un sistema di regolazione on/off con sistema programmabile su più livelli di temperatura</item>
        /// <item>6 L'isolamento della rete di ditribuzione acqua refrigerata/calda nei locali non climatizzati</item>
        /// <item>7 L'isolamento dei canali di distribuzione aria fredda/calda nei locali non climatizzati</item>
        /// <item>11 La sostituzione di generatori a regolazione on/off con altri di pari potenza a più gradini o a regolazione continua</item>
        /// </list>
        /// </summary>
        public List<int> CheckList { get; set; }
        /// <summary>
        /// Indicare le cause dei dati negativi rilevati e gli eventuali 
        /// interventi manutentivi eseguiti per risolvere il problema
        /// </summary>
        public string Osservazioni { get; set; }
        /// <summary>
        /// Indicare le raccomandazioni dettagliate finalizzate alla risoluzione di carenze riscontrate e non eliminate, m tali comunque da non 
        /// arrecare immediato pericolo alle persone, agli animali domestici e ai beni.
        /// </summary>
        public string Raccomandazioni { get; set; }
        /// <summary>
        /// Indicare dettagliatamente le operazioni necessarie al ripristino delle condizioni di sicurezza 
        /// dell'impianto. Le carenze riscontrate devono essere tali da arrecare un immediato pericolo alle 
        /// persone, agli animali domestici, ai beni e da richiedere la messa fuori servizio dell'apparecchio 
        /// e la diffida di utilizzo dello stesso nei confronti del Responsabile dell'impianto.
        /// </summary>
        public string Prescrizioni { get; set; }
        /// <summary>
        /// L'impianto può funzionare
        /// BOOLEANO
        /// </summary>
        public bool fImpiantoFunzionante { get; set; }
        /// <summary>
        /// Data intervento manutentivo raccomandata
        /// </summary>
        public System.DateTime? DataManutenzioneConsigliata { get; set; }
        /// <summary>
        /// Data del presente controllo
        /// </summary>
        public System.DateTime? DataControllo { get; set; }
        /// <summary>
        /// Ora di arrivo presso l'impianto 
        /// </summary>
        public System.DateTime? OraArrivo { get; set; }
        /// <summary>
        /// Ora di partenza presso l'impianto
        /// </summary>
        public System.DateTime? OraPartenza { get; set; }
        /// <summary>
        /// Tecnico che ha effettuato l'intervento
        /// </summary>
        public string TecnicoIntervento { get; set; }

        /// <summary>
        /// Guid automaticamente assegnato dal sistema
        /// </summary>
        /// 
        public string GuidRapportoTecnico { get; set; }
        /// <summary>
        /// Bollini Calore Pulito necessari per questo Rapporto di controllo
        /// </summary>
        public List<String> BolliniCalorePulito { get; set; }

        public List<POMJ_RaccomandazioniPrescrizioni> RaccomandazioniPrescrizioni { get; set; }
    }
}

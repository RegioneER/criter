using Criter.Anagrafica;
using Criter.Libretto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Criter.Rapporti
{
    /// <summary>
    /// RAPPORTO DI CONTROLLO TECNICO TIPO 4 (COGENERATORE) 
    /// </summary>
    public class POMJ_RapportoControlloTecnico_CG
    {
        /// <summary>
        /// Chiave assegnata per le chiamate API - <font color='red'><b>obbligatorio</b></font>
        /// </summary>
        [Required(ErrorMessage = "Criter Api Key obbligatorio")]
        public string CriterAPIKey { get; set; }

        /// <summary>
        /// Codice Soggetto Azienda attribuito dal Sistema Criter in fase di Registrazione - <font color='red'><b>obbligatorio</b></font>
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
        /// Potenza termica nominale totale max (*)
        /// </summary>
        public decimal? PotenzaTermicaNominaleTotaleMax { get; set; }
        /// <summary>
        /// Codice Catastale
        /// Vedi <a href="http://criter.pomiager.com/ApiDocumentation/Codici/CodiciComuni.txt">Questo File</a> Per i codici catastali dei comuni
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
        /// Dati Responsabile Impianto
        /// </summary>
        public POMJ_AnagraficaSoggetti ResponsabileImpianto { get; set; }
        /**************************************************************************
        * 
        * A. DATI IDENTIFICATIVI - Terzo Responsabile (se nominato)
        * 
        ***************************************************************************/
        /// <summary>
        /// Dati terzo responsabile
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
        /// Durezza Totale dell'acqua (*fr)
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
        /// <item>0	Assente</item>
        /// <item>1	Presente</item>
        /// <item>2 Non Richiesto</item>
        /// </list>	
        /// </summary>
        [Range(0, 2, ErrorMessage = "Tipologia trattamento in Acs valori consentiti compresi da 0 a 2")]
        public int? TrattamentoACS { get; set; }
        /// <summary>
        /// Tipo trattamento in ACS
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Filtrazione</item>
        /// <item>2	Addolcimento</item>
        /// <item>3 Condizionamento chimico</item>
        /// </list>	
        /// </summary>
        public List<int> TipoTrattamentoACS { get; set; }

        /**************************************************************************
         * 
         * D. CONTROLLO DELL'IMPIANTO
         * 
         ***************************************************************************/
        /// <summary>
        /// Luogo di installazione idoneo (esame visivo)
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? LocaleInstallazioneIdoneo { get; set; }
        /// <summary>
        /// Adeguate dimensioni aperture di ventilazione (esame visivo)
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? DimensioniApertureAdeguate { get; set; }
        /// <summary>
        /// Aperture di ventilazione libere da ostruzioni (esame visivo)
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? ApertureLibere { get; set; }
        /// <summary>
        /// Linee elettriche e cablaggi idonei (esame visivo)
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? LineeElettricheIdonee { get; set; }
        /// <summary>
        /// Camino e canale da fumo idonei (esame visivo)
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int CaminoCanaleFumoIdonei { get; set; }
        /// <summary>
        /// Capsula insonorizzante idonea (esame visivo)
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int CapsulaInsonorizzataIdonea { get; set; }
        /// <summary>
        /// Tenuta circuito idraulico idonea
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? TenutaImpiantoIdraulico { get; set; }
        /// <summary>
        /// Tenuta circuito olio idonea
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int TenutaCircuitoOlioIdonea { get; set; }
        /// <summary>
        /// Funzionalità dello scambiatore di calore di 
        /// separazione fra unità cogenerativa e impianto 
        /// edificio (se presente) idonea
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int FunzionalitàScambiatoreSeparazione { get; set; }

        /// <summary>
        /// Scarichi Idonei
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? ScarichiIdonei { get; set; }
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
        /// Assenza di perdite di combustibile liquido
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? AssenzaPerditeCombustibile { get; set; }

        /**************************************************************************
        * 
        * E. CONTROLLO E VERIFICA ENERGETICA DEL COGENERATORE
        * 
        ***************************************************************************/
        /// <summary>
        /// Campo precompilato
        /// </summary>
        public int CodiceProgressivo { get; set; }
        /// <summary>
        /// Campo Precompilato
        /// Nel caso in cui non si riesca a conoscere la data esatta di 
        /// installazione è indispensabile inserire una data verosimile, 
        /// che in caso di ispezione verrà verificata
        /// </summary>
        public System.DateTime? DataInstallazione { get; set; }
        /// <summary>
        /// Fabbricante
        /// </summary>
        public string Fabbricante { get; set; }
        /// <summary>
        /// Modello
        /// </summary>
        public string Modello { get; set; }
        /// <summary>
        /// Numero di matricola o il numero di 
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
        /// Potenza elettrica nominale ai morsetti (kW)
        /// </summary>
        public decimal? PotenzaElettricaMorsetti { get; set; }
        /// <summary>
        /// Potenza assorbita con il combustibile (kW)
        /// </summary>
        public decimal? PotenzaAssorbitaCombustibile { get; set; }
        /// <summary>
        /// Potenza termica nominale (massimo recupero) (kW)
        /// </summary>
        public decimal? PotenzaMassimoRecupero { get; set; }
        /// <summary>
        /// Potenza termica a pieno regime con by-pass fumi aperto (se presente) (kW)
        /// </summary>
        public decimal? PotenzaByPass { get; set; }
        /// <summary>
        /// Servizi di climatizzazione invernale presenti
        /// </summary>
        public bool fClimatizzazioneInvernale { get; set; }
        /// <summary>
        /// Servizi di climatizzazione estiva presenti
        /// </summary>
        public bool fClimatizzazioneEstiva { get; set; }
        /// <summary>
        /// Servizi di produzione acqua calda sanitaria
        /// </summary>
        public bool fProduzioneACS { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1 Altro</item>
        /// <item>2	Acqua calda</item>
        /// <item>3 Acqua surriscaldata</item>
        /// <item>3 Acqua surriscaldata</item>
        /// <item>4 Vapore</item>
        /// <item>5 Aria</item>
        /// <item>6 Olio Diatermico</item>
        /// </list>	
        /// </summary>
        [Range(1, 6, ErrorMessage = "Tipologia di fluido termovettore valori consentiti compresi da 1 a 6")]
        public int? TipologiaFluidoTermoVettore { get; set; }
        /// <summary>
        /// Indicare altro tipo di Fluido Termo Vettore
        /// </summary>
        public string AltroFluidoTermoVettore { get; set; }


        /**************************************************************************
        * 
        * E. CONTROLLO E VERIFICA ENERGETICA DEL GRUPPO FRIGO - VALORI MISURATI
        * 
        ***************************************************************************/
        /// <summary>
        /// Potenza ai morsetti del generatore (kW)
        /// </summary>
        public decimal? PotenzaAiMorsetti { get; set; }
        /// <summary>
        /// Temperatura aria comburente (°C)
        /// </summary>
        public decimal? TemperaturaAriaComburente { get; set; }
        /// <summary>
        /// Temperatura acqua in ingresso (°C)
        /// </summary>
        public decimal? TemperaturaAcquaIngresso { get; set; }
        /// <summary>
        /// Temperatura acqua in uscita (°C)
        /// </summary>
        public decimal? TemperaturaAcquauscita { get; set; }
        /// <summary>
        /// Temperatura acqua motore {solo m.c.i }(°C)
        /// </summary>
        public decimal? TemperaturaAcquaMotore { get; set; }
        /// <summary>
        ///Temperatura fumi a monte dello scambiatore(°C)
        /// </summary>
        public decimal? TemperaturaFumiMonte { get; set; }
        /// <summary>
        /// Temperatura fumi a valle dello scambiatore (°C)
        /// </summary>
        public decimal? TemperaturaFumiValle { get; set; }
        /// <summary>
        /// Emissione di monossido di carbonio CO (riportato al 5% di O2 nei fumi)
        /// </summary>
        public decimal? EmissioneMonossido { get; set; }



        /// <summary>
        /// valore 1 di regolazione della soglia di intervento per sovrafrequenza a L1, L2,L3
        /// </summary>
        public decimal? SovrafrequenzaSogliaInterv1 { get; set; }
        /// <summary>
        /// valore 2 di regolazione della soglia di intervento per sovrafrequenza a L1, L2,L3
        /// </summary>
        public decimal? SovrafrequenzaSogliaInterv2 { get; set; }
        /// <summary>
        /// valore 3 di regolazione della soglia di intervento per sovrafrequenza a L1, L2,L3
        /// </summary>
        public decimal? SovrafrequenzaSogliaInterv3 { get; set; }
        /// <summary>
        /// valore 1 di regolazione del tempo di intervento per sovrafrequenza a L1, L2,L3
        /// </summary>
        public decimal? SovrafrequenzaTempoInterv1 { get; set; }
        /// <summary>
        /// valore 2 di regolazione del tempo di intervento per sovrafrequenza a L1, L2,L3
        /// </summary>
        public decimal? SovrafrequenzaTempoInterv2 { get; set; }
        /// <summary>
        /// valore 3 di regolazione del tempo di intervento per sovrafrequenza a L1, L2,L3
        /// </summary>
        public decimal? SovrafrequenzaTempoInterv3 { get; set; }
        /// <summary>
        /// Valore 1 di regolazione della soglia di intervento per sottofrequenza a L1, L2,L3
        /// </summary>
        public decimal? SottofrequenzaSogliaInterv1 { get; set; }
        /// <summary>
        /// Valore 2 di regolazione della soglia di intervento per sottofrequenza a L1, L2,L3
        /// </summary>
        public decimal? SottofrequenzaSogliaInterv2 { get; set; }
        /// <summary>
        /// Valore 3 di regolazione della soglia di intervento per sottofrequenza a L1, L2,L3
        /// </summary>
        public decimal? SottofrequenzaSogliaInterv3 { get; set; }
        /// <summary>
        /// Valore 1 di regolazione del tempo di intervento per sottofrequenza a L1, L2,L3
        /// </summary>
        public decimal? SottofrequenzaTempoInterv1 { get; set; }
        /// <summary>
        /// Valore 2 di regolazione del tempo di intervento per sottofrequenza a L1, L2,L3
        /// </summary>
        public decimal? SottofrequenzaTempoInterv2 { get; set; }
        /// <summary>
        /// Valore 3 di regolazione del tempo di intervento per sottofrequenza a L1, L2,L3
        /// </summary>
        public decimal? SottofrequenzaTempoInterv3 { get; set; }
        /// <summary>
        /// valore 1 di regolazione della soglia di intervento per sovratensione a L1, L2,L3
        /// </summary>
        public decimal? SovratensioneSogliaInterv1 { get; set; }
        /// <summary>
        /// valore 2 di regolazione della soglia di intervento per sovratensione a L1, L2,L3
        /// </summary>
        public decimal? SovratensioneSogliaInterv2 { get; set; }
        /// <summary>
        /// valore 3 di regolazione della soglia di intervento per sovratensione a L1, L2,L3
        /// </summary>
        public decimal? SovratensioneSogliaInterv3 { get; set; }
        /// <summary>
        /// valore 1 di regolazione del tempo di intervento per sovratensione a L1, L2,L3
        /// </summary>
        public decimal? SovratensioneTempoInterv1 { get; set; }
        /// <summary>
        /// valore 2 di regolazione del tempo di intervento per sovratensione a L1, L2,L3
        /// </summary>
        public decimal? SovratensioneTempoInterv2 { get; set; }
        /// <summary>
        /// valore 3 di regolazione del tempo di intervento per sovratensione a L1, L2,L3
        /// </summary>
        public decimal? SovratensioneTempoInterv3 { get; set; }
        /// <summary>
        /// valore 1 di regolazione della soglia di intervento per sottotensione a L1, L2,L3
        /// </summary>
        public decimal? SottotensioneSogliaInterv1 { get; set; }
        /// <summary>
        /// valore 2 di regolazione della soglia di intervento per sottotensione a L1, L2,L3
        /// </summary>
        public decimal? SottotensioneSogliaInterv2 { get; set; }
        /// <summary>
        /// valore 3 di regolazione della soglia di intervento per sottotensione a L1, L2,L3
        /// </summary>
        public decimal? SottotensioneSogliaInterv3 { get; set; }
        /// <summary>
        /// valore 1 di regolazione del tempo di intervento per sottotensione a L1, L2,L3
        /// </summary>
        public decimal? SottotensioneTempoInterv1 { get; set; }
        /// <summary>
        /// valore 2 di regolazione del tempo di intervento per sottotensione a L1, L2,L3
        /// </summary>
        public decimal? SottotensioneTempoInterv2 { get; set; }
        /// <summary>
        /// valore 3 di regolazione del tempo di intervento per sottotensione a L1, L2,L3
        /// </summary>
        public decimal? SottotensioneTempoInterv3 { get; set; }

        /**************************************************************************
        * 
        * F. CHECK-LIST
        * 
        ***************************************************************************/
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	L'adozione di valvole termostatiche sui corpi scaldanti</item>
        /// <item>2	L'isolamento della rete di distribuzione nei locali non riscaldati</item>
        /// <item>3 L'introduzione di un sistema di trattamento dell'acqua sanitaria e per riscaldamento ove assente</item>
        /// <item>5 La sostituzione di un sistema di regolazione on/off con sistema programmabile su più livelli di temperatura</item>
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
        /// AltroTipologiaFluidoTermoVettoreUscita
        /// </summary>
	    public string AltroTipologiaFluidoTermoVettoreUscita { get; set; }

        /// <summary>
        /// Bollini Calore Pulito necessari per questo Rapporto di controllo
        /// </summary>
        public List<String> BolliniCalorePulito { get; set; }

        public List<POMJ_RaccomandazioniPrescrizioni> RaccomandazioniPrescrizioni { get; set; }
    }
}

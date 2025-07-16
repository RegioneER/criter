using Criter.Anagrafica;
using Criter.Libretto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Criter.Rapporti
{
    /// <summary>
    /// RAPPORTO DI CONTROLLO TECNICO TIPO 1 (GRUPPI TERMICI)
    /// </summary>
    public class POMJ_RapportoControlloTecnico_GT
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
        /// Dati relativi al responsabile dell'impianto
        /// </summary>
        public POMJ_AnagraficaSoggetti ResponsabileImpianto { get; set; }
        /**************************************************************************
        * 
        * A. DATI IDENTIFICATIVI - Terzo Responsabile (se nominato)
        * 
        ***************************************************************************/
        /// <summary>
        /// Dati relativi al Terzo responsabile
        /// </summary>
        public POMJ_AnagraficaSoggetti TerzoResponsabile { get; set; }
        /**************************************************************************
        * 
        * A. DATI IDENTIFICATIVI - Impresa manutentrice
        * 
        ***************************************************************************/
        /// <summary>
        /// Dati relativi all'impresa manutentrice
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
        /// Per installazione esterna: generatori idonei
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? GeneratoriIdonei { get; set; }
        /// <summary>
        /// Adeguate dimensioni aperture di ventilazione/aerazione
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? DimensioniApertureAdeguate { get; set; }
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
        /// <item>-2 Non Applicabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? Contabilizzazione { get; set; }
        /// <summary>
        /// Termoregolazione
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-2 Non Applicabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? Termoregolazione { get; set; }
        /// <summary>
        /// Corretto Funzionamento Contabilizzazione
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-2 Non Applicabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? CorrettoFunzionamentoContabilizzazione { get; set; }

        /// <summary>
        /// RegolazioneTemperaturaAmbiente
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? RegolazioneTemperaturaAmbiente { get; set; }
        /// <summary>
        /// Aperture di ventilazione/aerazione libere da ostruzioni
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? ApertureLibere { get; set; }
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
        /// <summary>
        /// Idonea tenuta dell'impianto interno e raccordi con il generatore
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? TenutaImpiantoIdraulico { get; set; }
        /**************************************************************************
        * 
        * E. CONTROLLO E VERIFICA ENERGETICA DEL GRUPPO TERMICO
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
        /// Numero di matricola o il numero di 
        /// serie fornito dal fabbricante
        /// </summary>
        public string Matricola { get; set; }

        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Gruppo termico singolo</item>
        /// <item>2	Gruppo termico modulare</item>
        /// <item>3	Tubo/nastro radiante</item>
        /// <item>4	Generatore aria calda</item>
        /// </list>	
        /// </summary>
        [Range(1, 4, ErrorMessage = "Tipologia gruppi termici valori consentiti compresi da 1 a 4")]
        public int? TipologiaGruppiTermici { get; set; }
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
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Standard</item>
        /// <item>2	Bassa temperatura</item>
        /// <item>3	Condensazione</item>
        /// <item>4	Ad aria calda</item>
        /// </list>	
        /// </summary>
        [Range(1, 4, ErrorMessage = "Tipologia di generatori termici valori consentiti compresi da 1 a 4")]
        public int? TipologiaGeneratoriTermici { get; set; }
        /// <summary>
        /// Potenza termica nominale max al focolare (kW) (*)
        /// </summary>
        public decimal? PotenzaTermicaNominaleFocolare { get; set; }
        /// <summary>
        /// Potenza termica nominale utile (kW)
        /// </summary>
        public decimal? PotenzaTermicaNominale { get; set; }
       /// <summary>
       /// Servizi climatizzazione invernale presenti
       /// booleano
       /// </summary>
        public bool fClimatizzazioneInvernale { get; set; }
        /// <summary>
        /// presenza servizi produzione acqua calda santaria presenti
        /// booleano
        /// </summary>
        public bool fProduzioneACS { get; set; }
        /// <summary>
        /// <list type="bullet">
        /// <item> 1 Altro</item>
        /// <item> 2 Gas naturale</item>
        /// <item> 3 Gpl </item>
        /// <item> 4 Gasolio</item>
        /// <item> 5 Olio combustibile </item>
        /// <item> 6 Pellet</item>
        /// <item> 7 Legna </item>
        /// <item> 8 Cippato</item>
        /// <item> 9 Bricchette </item>
        /// <item> 10 Biogas </item>
        /// <item> 11 Syngas</item>
        /// <item> 12 Kerosene</item>
        /// <item> 13 Biodiesel </item>
        /// <item> 14 Aria propanata</item>
        /// </list>
        /// </summary>
        [Range(1, 14, ErrorMessage = "Tipologia di combustibile valori consentiti compresi da 1 a 14")]
        public int TipologiaCombustibile { get; set; }
        /// <summary>
        /// Altro Combustibile
        /// </summary>
        public string AltroCombustibile { get; set; }
        /// <summary>
        /// Modalità di evacuazione forzata fumi booleano
        /// </summary>
        public bool EvacuazioneForzata { get; set; }
        /// <summary>
        /// Modalità di evacuazione naturale fumi booleano
        /// </summary>
        public bool EvacuazioneNaturale { get; set; }
        /// <summary>
        /// Depressione del canale da fumo (Pa)
        /// </summary>
        public decimal? DepressioneCanaleFumo { get; set; }
        /// <summary>
        /// Dispositivi di comando e regolazione funzionanti correttamente
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? DispositiviComandoRegolazione { get; set; }
        /// <summary>
        /// Dispositivi di sicurezza non manomessi e/o cortocircuitati
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? DispositiviSicurezza { get; set; }
        /// <summary>
        /// Valvola di sicireza alla sovrappressione a scarico libero
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? ValvolaSicurezzaSovrappressione { get; set; }
        /// <summary>
        /// Controllato e pulito lo scambiatore lato fumi
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? ScambiatoreFumiPulito { get; set; }
        /// <summary>
        /// Presenza riflusso dei prodotti della combustione
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? RiflussoProdottiCombustione { get; set; }
        /// <summary>
        /// Risultati controllo, secondo UNI 10389-1, conformi alla legge
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? ConformitaUNI10389 { get; set; }

        /**************************************************************************
         * 
         * E. CONTROLLO E VERIFICA ENERGETICA DEL GRUPPO TERMICO - VALORI MISURATI
         * 
         ***************************************************************************/
        /// <summary>
        /// devono essere compilati tanti Rapporti di controllo tecnico quante sono le analisi fumi previste.
        /// </summary>
        public string ModuloTermico { get; set; }
        /// <summary>
        /// Valore della temperatura dei fumi rilevata al 
        /// momento della verifica, espresso in gradi centigradi (°C)
        /// </summary>
        public decimal? TemperaturaFumi { get; set; }
        /// <summary>
        /// Temperatura Aria Comburente (°C)
        /// </summary>
        public decimal? TemperaraturaComburente { get; set; }
        /// <summary>
        /// Valore della concentrazione di ossigeno (O2) nei fumi 
        /// rilevata al momento della verifica, espresso in percentuale.
        /// </summary>
        public decimal? O2 { get; set; }
        /// <summary>
        /// Valore della concentrazione di anidride carbonica (CO2) nei fumi 
        /// rilevata al momento della verifica, espresso in percentuale.
        /// </summary>
        public decimal? Co2 { get; set; }
        /// <summary>
        /// Campo obbligatorio solo per i gruppi termici alimentati a combustibile liquido ovvero 
        /// quando nel campo Combustibile della sezione E del Rapporto di controllo è 
        /// presente una dei seguenti valori: Gasolio o Olio combustibile
        /// </summary>
        public decimal? bacharach1 { get; set; }
        /// <summary>
        /// Campo obbligatorio solo per i gruppi termici alimentati a combustibile liquido ovvero 
        /// quando nel campo Combustibile della sezione E del Rapporto di controllo è 
        /// presente una dei seguenti valori: Gasolio o Olio combustibile
        /// </summary>
        public decimal? bacharach2 { get; set; }
        /// <summary>
        /// Campo obbligatorio solo per i gruppi termici alimentati a combustibile liquido ovvero 
        /// quando nel campo Combustibile della sezione E del Rapporto di controllo è 
        /// presente una dei seguenti valori: Gasolio o Olio combustibile
        /// </summary>
        public decimal? bacharach3 { get; set; }
        /// <summary>
        /// Valore di monossido di carbonio (CO) nei fumi secchi, espresso in parti 
        /// per milione (ppm), ricordando che è ottenuto dalla media aritmetica delle tre misure 
        /// significative rilevate dallo strumento
        /// </summary>
        public decimal? COFumiSecchi { get; set; }
        /// <summary>
        /// Valore della concentrazione di monossido di carbonio (CO) corretto nei fumi, 
        /// espresso in parti per milione (ppm), calcolata in condizioni di combustione stechiometrica 
        /// (senza eccesso d'aria).
        /// Il valore del CO corretto calcolato è sempre superiore a quello misurato(CO nei fumi secchi) 
        /// in sede di verifica.
        /// </summary>
        public decimal? CoCorretto { get; set; }
        /// <summary>
        /// Valore della portata di combustibile, espresso in metri cubi all'ora (m3/h), corrispondente 
        /// a quello rilevato durante la misura del rendimento in condizioni di prova
        /// </summary>
        public decimal? PortataCombustibile { get; set; }
        /// <summary>
        /// Valore della potenza termica effettiva massima del focolare del generatore di calore, 
        /// espresso il kilowatt (kW), con il quale si esegue la prova del rendimento di combustione
        /// </summary>
        public decimal? PotenzaTermicaEffettiva { get; set; }
        /// <summary>
        /// Valore percentuale del Rendimento di Combustione misurato e calcolato al momento della verifica, 
        /// ricordando che è ottenuto dalla media aritmetica delle tre misure significative. 
        /// Il valore ottenuto deve essere maggiorato di due punti percentuali.
        /// Campo obbligatorio solo per i gruppi termici alimentati da combustibile liquido o gassoso, 
        /// ovvero nel caso in cui nel campo Combustibile della sezione E del Rapporto di controllo tecnico 
        /// sia stata indicata una delle seguenti opzioni: Gas naturale, GPL, Gasolio e Olio combustibile
        /// </summary>
        public decimal? RendimentoCombustione { get; set; }
        /// <summary>
        /// Valore del Rendimento minimo di legge calcolato in funzione della tipologia del generatore, 
        /// così come definito dall'Allegato C del Regolamento XX.
        /// Campo obbligatorio e solo per i gruppi termici alimentati da combustibile liquido o gassoso, 
        /// ovvero nel caso in cui nel campo Combustibile della sezione E del Rapporto di controllo tecnico
        /// sia stata indicata una delle seguenti opzioni:Gas naturale, GPL, Gasolio e Olio combustibile
        /// </summary>
        public decimal? RendimentoMinimo { get; set; }
        /// <summary>
        /// Rispetta l'indice di Bacharach
        /// booleano
        /// </summary>
        public bool RispettaIndiceBacharach { get; set; }
        /// <summary>
        /// CO fumi senz'aria minore di 1.000 ppm v/v
        /// </summary>
        public bool COFumiSecchiNoAria1000 { get; set; }
        /// <summary>
        /// Rendimento maggiore del rendimento minimo	
        /// </summary>
        public bool RendimentoSupMinimo { get; set; }

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
        /// Bollini Calore Pulito necessari per questo Rapporto di controllo
        /// </summary>
        /// 
        public List<String> BolliniCalorePulito { get; set; }

        public List<POMJ_RaccomandazioniPrescrizioni> RaccomandazioniPrescrizioni { get; set; }
    }
}

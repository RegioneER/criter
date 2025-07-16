using Criter.Anagrafica;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Criter.Libretto
{
    /// <summary>
    /// Libretto di impianto conforme al modello adottato con Delibera di Giunta Regionale
    /// </summary>
    public class POMJ_LibrettoImpianto
        {
        /// <summary>
        /// Chiave assegnata per le chiamate API - <font color='red'><b>obbligatorio</b></font>
        /// </summary>
        [Required(ErrorMessage = "Criter Api Key obbligatorio")]
        public string CriterAPIKey { get; set; }
        /// <summary>
        /// Codice Soggetto attribuito dal Sistema Criter in fase di Registrazione.
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

        /**************************************************************************
        * 
        * INFORMAZIONI GENERALI LIBRETTO IMPIANTO
        * 
        * ***********************************************************************/
        /// <summary>
        /// Azienda che ha in carico l'impianto
        /// </summary>
        public string NomeAzienda { get; set; }
        /// <summary>
        /// Nome del manutentore
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Cognome del manutentore
        /// </summary>
        public string Cognome { get; set; }
        /// <summary>
        /// Codice di targatura dell'impianto
        /// </summary>
        public POMJ_TargaturaImpianto CodiceTargaturaImpianto { get; set; }
        /// <summary>
        /// Stato del libretto - obbligatorio
        /// <list type="bullet">
        /// <item> 1  Bozza </item>
        /// </list>
        /// </summary>
        [Required(ErrorMessage = "Stato libretto campo obbligatorio")]
        [Range(1, 1, ErrorMessage = "Stato libretto valore consentito 1 (libretto in bozza)")]
        public int StatoLibrettoImpianto { get; set; }
        ///// <summary>
        ///// Booleano
        ///// L’impianto risulta già targato con altro sistema?
        ///// </summary>
        //public bool fTargaturaImpiantoAltro { get; set; }
        /// <summary>
        /// Targatura altro sistema
        /// </summary>
        public int? NumeroRevisione { get; set; }

        /**************************************************************************
        * 
        * 1.0 SCHEDA IDENTIFICATIVA DELL’IMPIANTO
        *   1.1 TIPOLOGIA INTERVENTO
        * 
        * ***********************************************************************/
        /// <summary>
        /// Data dell'intervento - obbligatorio
        /// </summary>
        [Required(ErrorMessage = "Data intervento campo obbligatorio")]
        public System.DateTime? DataIntervento { get; set; }
        /// <summary>
        /// Tipologia di intervento - <font color='red'><b>obbligatorio</b></font>
        /// <list type="bullet">
        /// <item>
        /// 1	Nuova installazione
        /// </item>
        /// <item>
        /// 2	Ristrutturazione
        /// </item>
        /// <item>
        /// 3	Sostituzione del generatore
        /// </item>
        /// <item>
        /// 4	Compilazione libretto impianto esistente
        /// </item>
        /// </list>
        /// </summary>
        [Required(ErrorMessage = "Tipologia di intervento campo obbligatorio")]
        [Range(1, 4, ErrorMessage = "Tipologia di intervento valori consentiti compreso da 1 a 4")]
        public int TipologiaIntervento { get; set; }

        /**************************************************************************
        * 
        * 1.0 SCHEDA IDENTIFICATIVA DELL’IMPIANTO
        *   1.2 UBICAZIONE E DESTINAZIONE DELL’EDIFICIO
        * 
        * ***********************************************************************/
        /// <summary>
        /// Indirizzo dell'impianto
        /// </summary>
        public string Indirizzo { get; set; }
        /// <summary>
        /// Numero civico dell'impianto
        /// </summary>
        public string Civico { get; set; }
        /// <summary>
        /// Palazzo dove è ubicato l'impianto
        /// </summary>
        public string Palazzo { get; set; }
        /// <summary>
        /// Indicazione della scala
        /// </summary>
        public string Scala { get; set; }
        /// <summary>
        /// Interno
        /// </summary>
        public string Interno { get; set; }
        /// <summary>
        /// Codice catastale del comune - <font color='red'><b>obbligatorio</b></font>
        /// Vedi <a href="http://criter.pomiager.com/ApiDocumentation/Codici/CodiciComuni.txt">questo File</a> Per i codici catastali dei comuni
        /// </summary>
        [Required(ErrorMessage = "Codice catastale comune campo obbligatorio")]
        public string CodiceCatastaleComune { get; set; }
        /// <summary>
        /// Identificativo catastale dell'impianto
        /// </summary>
        public List<POMJ_DatiCatastali> DatiCatastali { get; set; }
        /// <summary>
        /// Booleano
        /// Singola unità immobiliare - <font color='red'><b>obbligatorio</b></font>
        /// </summary>
        [Required(ErrorMessage = "Singola unità immobiliare campo obbligatorio")]
        public bool fUnitaImmobiliare { get; set; }
        /// <summary>
        /// Categoria della destinazioni dell'edificio - <font color='red'><b>obbligatorio</b></font>
        /// <list type="bullet">
        /// <item>1	E1 - Abitazioni civili e rurali a residenza a carattere continuativo</item>
        /// <item>2	E2 - Edifici adibiti a uffici e assimilabili</item>
        /// <item>3	E3 - Edifici adibiti a ospedali, cliniche o case di cura e assimilabili</item>
        /// <item>4	E4 - Edifici adibiti ad attività ricreative, associative o di culto e assimilabili</item>
        /// <item>5	E5 - Edifici adibiti ad attività commerciali e assimilabili</item>
        /// <item>6	E6 - Edifici adibiti ad attività sportive</item>
        /// <item>7	E7 - Edifici adibiti ad attività scolastiche a tutti i livelli assimilabili</item>
        /// <item>8	E8 - Edifici adibiti ad attività industriali ed artigianali e assimilabili</item>
        /// </list>
        /// </summary>
        [Required(ErrorMessage = "Categoria della destinazioni dell'edificio campo obbligatorio")]
        [Range(1, 8, ErrorMessage = "Categoria della destinazioni dell'edificio valori consentiti compreso da 1 a 8")]
        public int? DestinazioneUso { get; set; }
        /// <summary>
        /// Volume lordo riscaldato
        /// Espresso in mc
        /// </summary>
        public decimal? VolumeLordoRiscaldato { get; set; }
        /// <summary>
        /// Volume lordo raffrescato 
        /// Espresso in mc
        /// </summary>
        public decimal? VolumeLordoRaffrescato { get; set; }
        /// <summary>
        /// Numero attestato prestazione energetica (APE)
        /// </summary>
        public string NumeroAPE { get; set; }
        /// <summary>
        /// Numero punto riconsegna combustibile (PDR)
        /// </summary>
        public string NumeroPDR { get; set; }
        /// <summary>
        /// Numero punto riconsegna energia elettrica (POD)
        /// </summary>
        public string NumeroPOD { get; set; }

        /**************************************************************************
         * 
         * 1.0 SCHEDA IDENTIFICATIVA DELL’IMPIANTO
         *   1.3 IMPIANTO TERMICO DESTINATO A SODDISFARE I SEGUENTI SERVIZI
         * 
         **************************************************************************/
        /// <summary>
        /// Produzione di acqua calda sanitaria (acs)
        /// Booleano
        /// </summary>
        public bool fAcs { get; set; }
        /// <summary>
        /// Potenza utile (kW)
        /// </summary>
        public decimal? PotenzaAcs { get; set; }
        /// <summary>
        /// Climatizzazione invernale
        /// Booleano
        /// </summary>
        public bool fClimatizzazioneInvernale { get; set; }
        /// <summary>
        /// Potenza utile (kW)
        /// </summary>
        public decimal? PotenzaClimatizzazioneInvernale { get; set; }
        /// <summary>
        /// Climatizzazione estiva
        /// Booleano
        /// </summary>
        public bool fClimatizzazioneEstiva { get; set; }
        /// <summary>
        /// Potenza utile (kW)
        /// </summary>
        public decimal? PotenzaClimatizzazioneEstiva { get; set; }
        /// <summary>
        /// Altro tipo di Climatizzazione
        /// Booleano
        /// </summary>     
        public bool fClimatizzazioneAltro { get; set; }
        /// <summary>
        /// Descrizione di altro tipo di climatizzazione
        /// </summary>
        public string ClimatizzazioneAltro { get; set; }

        /**************************************************************************
         * 
         * 1.4 TIPOLOGIA FLUIDO VETTORE
         * 
         **************************************************************************/
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Altro</item>
        /// <item>2	Acqua</item>
        /// <item>3	Aria</item>
        /// </list>
        /// </summary>
        public List<int> TipologiaFluidoVettore { get; set; }

        /// <summary>
        /// Altra tipologia di fluido vettore
        /// </summary>
        public string TipologiaFluidoVettoreAltro { get; set; }

        /**************************************************************************
         * 
         * 1.5 INDIVIDUAZIONE DELLA TIPOLOGIA DEI GENERATORI
         * 
         **************************************************************************/
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Altro</item>
        /// <item>2	Generatore a combustione</item>
        /// <item>3	Pompa di calore</item>
        /// <item>4	Macchina frigorifera</item>
        /// <item>5	Teleriscaldamento</item>
        /// <item>6	Teleraffrescamento</item>
        /// <item>7	Cogenerazione / trigenerazione</item>
        /// </list>
        /// </summary>
        public List<int> TipologiaGeneratori { get; set; }
        /// <summary>
        /// Eventuali integrazioni con altro generatore
        /// </summary>
        public string TipologiaGeneratoriAltro { get; set; }
        /// <summary>
        /// Presenza di pannelli solari termici
        /// </summary>
        public bool fPannelliSolariTermici { get; set; }   
        /// <summary>
        /// Altri sistemi integrati
        /// </summary>
        public bool fPannelliSolariTermiciAltro { get; set; }
        /// <summary>
        /// Indicazione di altri sistemi integrati
        /// </summary>
        public string PannelliSolariTermiciAltro { get; set; }
        /// <summary>
        /// Potenza del solare termico
        /// </summary>
        public decimal? PotenzaSolariTermici { get; set; }
        /// <summary>
        /// Potenza del solare termico
        /// </summary>
        public decimal? SuperficieTotaleSolariTermici { get; set; }
        /// <summary>
        /// servizio reso dai pannelli di tipo produzione acqua calda sanitaria
        /// </summary>
        public bool fPannelliSolariClimatizzazioneAcs { get; set; }
        /// <summary>
        /// servizio reso dai pannelli di tipo climatizzazione invernale
        /// </summary>
        public bool fPannelliSolariClimatizzazioneInvernale { get; set; }
        /// <summary>
        /// servizio reso dai pannelli di tipo climatizzazione estiva
        /// </summary>
        public bool fPannelliSolariClimatizzazioneEstiva { get; set; }


        /*************************************************************************************
         * 
         * 1.6 RESPONSABILE DELL’IMPIANTO O DELEGANTE (NEL CASO NOMINA DI TERZO RESPONSABILE)
         * 
         *************************************************************************************/
        /// <summary>
        /// Tipologia di responsabile - <font color='red'><b>obbligatorio</b></font>
        /// <list type="bullet">
        /// <item>1	Proprietario</item>
        /// <item>2	Occupante</item>
        /// <item>3	Amministratore di condominio</item>
        /// </list>
        /// </summary>
        [Required(ErrorMessage = "Tipo Responsabile campo obbligatorio")]
        [Range(1, 3, ErrorMessage = "Tipo Responsabile valori consentiti da 1 a 3")]
        public int? TipologiaResponsabile { get; set; }

        /// <summary>
        /// Tipo di soggetto - <font color='red'><b>obbligatorio</b></font>
        /// <list type="bullet">
        /// <item>1	Persona fisica</item>
        /// <item>2	Persona giuridica</item>
        /// </list>
        /// </summary>
        //[Required(ErrorMessage = "Tipo soggetto Responsabile campo obbligatorio")]
        [Range(1, 2, ErrorMessage = "Tipo soggetto Responsabile valori consentiti da 1 a 2")]
        public int? TipoSoggetto { get; set; }
        /// <summary>
        /// Responsabile impianto - <font color='red'><b>obbligatorio</b></font>
        /// <para>
        /// Il responsabile dell'impianto termico è:
        /// </para>
        /// <list type="bullet">
        /// <item>l'occupante, a qualsiasi titolo, in caso di singole unità immobiliari residenziali</item>
        /// <item>il proprietario, in caso di singole unità immobiliari residenziali non locate</item>
        /// <item>l'amministratore, in caso di edifici dotati di impianti termici centralizzati amministrati in condominio</item>
        /// <item>il proprietario o l'amministratore delegato in caso di edifici di proprietà di soggetti diversi dalle persone fisiche</item>
        /// </list>
        /// </summary>
        public POMJ_AnagraficaSoggetti ResponsabileImpianto { get; set; }
        /// <summary>
        /// E' stato nominato un terzo responsabile
        /// </summary>
        public bool fTerzoResponsabile { get; set; }
        /**************************************************************************
         * 
         * 2.0 TRATTAMENTO ACQUA
         *   2.1 CONTENUTO D’ACQUA DELL’IMPIANTO DI CLIMATIZZAZIONE
         * 
         **************************************************************************/
        /// <summary>
        /// Contenuto di acqua dell'impianto espresso in metri cubi (m3). (1 litro = 0,001 m3)
        /// </summary>
        public decimal? ContenutoAcquaImpianto { get; set; }

        /**************************************************************************
         * 
         * 2.0 TRATTAMENTO ACQUA
         *   2.2 DUREZZA TOTALE DELL’ACQUA
         * 
         **************************************************************************/
        /// <summary>
        /// Durezza totale dell’acqua dell’impianto(°f), 
        /// espresso in gradi francesi. 
        /// Dove possibile inserire il valore fornito dal gestore dell‘acquedotto.
        /// </summary>
        public decimal? DurezzaTotaleAcquaImpianto { get; set; }

        /**************************************************************************
         * 
         * 2.0 TRATTAMENTO ACQUA
         *   2.3 TRATTAMENTO DELL’ACQUA DELL’IMPIANTO DI CLIMATIZZAZIONE INVERNALE
         * 
         **************************************************************************/
        /// <summary>
        /// indica se è presente un trattamento di acqua del'impianto di climatizzazione invernale
        /// </summary>
        public bool fTrattamentoAcquaInvernale { get; set; }


        /// <summary>
        /// Durezza totale acqua impianto invernale
        /// </summary>
        public decimal? DurezzaTotaleAcquaImpiantoInvernale { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Filtrazione</item>
        /// <item>2	Addolcimento</item>
        /// <item>3	Condizionamento chimico</item>
        /// </list>
        /// </summary>
        /// 
        public List<int> TipologiaTrattamentoAcquaInvernale { get; set; }
        
        /// <summary>
        /// indica se è presente un trattamento di protezione del gelo dell'acqua presente nell'impianto
        /// </summary>
        public bool fProtezioneGelo { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Glicole etilenico</item>
        /// <item>2	Glicole propilenico</item>
        /// </list>
        /// </summary>
        public int? TipologiaProtezioneGelo { get; set; }

        /**************************************************************************
         * 
         * 2.0 TRATTAMENTO ACQUA
         *   2.4 TRATTAMENTO DELL’ACQUA CALDA SANITARIA
         * 
         **************************************************************************/
        /// <summary>
        /// indica se è presente un sistema di trattamento dell'acqua calda sanitaria
        /// </summary>
        public bool fTrattamentoAcquaAcs { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Filtrazione</item>
        /// <item>2	Addolcimento</item>
        /// <item>3	Condizionamento chimico</item>
        /// </list>
        /// </summary>
        public List<int> TipologiaTrattamentoAcquaAcs { get; set; }

        /// <summary>
        /// Durezza totale dell’acqua dell’impianto(°f), 
        /// espresso in gradi francesi. 
        /// Dove possibile inserire il valore fornito dal gestore dell‘acquedotto.
        /// </summary>
        public decimal? DurezzaTotaleAcquaAcs { get; set; }

        /// <summary>
        /// Percentuale glicole
        /// </summary>
        public decimal? PercentualeGlicole { get; set; }
        /// <summary>
        /// Ph glicole
        /// </summary>
        public decimal? PhGlicole { get; set; }
        
        /**************************************************************************
         * 
         * 2.0 TRATTAMENTO ACQUA
         *   2.5 TRATTAMENTO DELL’ACQUA DI RAFFREDDAMENTO DELL’IMPIANTO DI CLIMATIZZAZIONE ESTIVA
         * 
         **************************************************************************/
        /// <summary>
        /// indica se è presente un sistema di climatizzazione estiva in cui l’acqua utilizzata 
        /// dall’impianto stesso viene raffreddata da un circuito, sempre ad acqua, che utilizza 
        /// il calore assorbito tramite uno scambiatore o una torre di raffreddamento
        /// </summary>
        public bool fTrattamentoAcquaEstiva { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Senza recupero termico</item>
        /// <item>2	A recupero termico parziale</item>
        /// <item>3	A recupero termico totale</item>
        /// </list>	
        /// </summary>
        public int? TipologiaCircuitoRaffreddamento { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Acquedotto</item>
        /// <item>2	Pozzo</item>
        /// <item>3	Acqua superficiale</item>
        /// </list>
        /// </summary>
        //[Range(1, 3, ErrorMessage = "Tipologia acqua di alimento valori consentiti compreso da 1 a 3")]
        public int? TipologiaAcquaAlimento { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Filtrazione</item>
        /// <item>2	Addolcimento</item>
        /// <item>3	Condizionamento chimico</item>
        /// </list>
        /// </summary>
        public List<int> TipologiaTrattamentoAcquaEstiva { get; set; }
        /// <summary>
        /// <para>Tipologia di filtrazione. Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Altro</item>
        /// <item>2	Filtrazione di sicurezza</item>
        /// <item>3	Filtrazione a masse</item>
        /// </list>
        /// </summary>
        public List<int> TipologiaFiltrazione { get; set; }
        /// <summary>
        /// Altra tipologia di filtrazione
        /// </summary>
        public string TipologiaFiltrazioneAltro { get; set; }
        /// <summary>
        /// <para>Tipologia di addolcimento acqua. Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Altro</item>
        /// <item>2	Addolcimento</item>
        /// <item>3	Osmosi inversa</item>
        /// <item>4	Demineralizzazione</item>
        /// </list>
        /// </summary>
        public List<int> TipologiaAddolcimentoAcqua { get; set; }
        /// <summary>
        /// Altro tipo di addolcimento acqua
        /// </summary>
        public string TipologiaAddolcimentoAcquaAltro { get; set; }
        /// <summary>
        /// <para>Tipologia di condizionamento chimico. Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Altro</item>
        /// <item>2	A prevalente azione antincrostante</item>
        /// <item>3	A prevalente azione anticorrosiva</item>
        /// <item>4	Azione antincrostante e anticorrosiva</item>
        /// <item>5	Biocida</item>
        /// </list>
        /// </summary>
        public List<int> TipologiaCondizionamentoChimico { get; set; }
        /// <summary>
        /// Altro tipo di condizionamento chimico
        /// </summary>
        public string TipologiaCondizionamentoChimicoAltro { get; set; }
        /// <summary>
        /// Gestione torre raffreddamento
        /// Presenza sistema spurgo automatico (per circuito a recupero parziale)
        /// </summary>
        public bool fSistemaSpurgoAutomatico { get; set; }
        /// <summary>
        /// ConducibilitaAcquaIngresso
        /// </summary>
        public decimal? ConducibilitaAcquaIngresso { get; set; }
        /// <summary>
        /// ConducibilitaInizioSpurgo
        /// </summary>
        public decimal? ConducibilitaInizioSpurgo { get; set; }

        /**************************************************************************
         * 
         * 3.0 NOMINA DEL TERZO RESPONSABILE DELL’IMPIANTO TERMICO
         * 
         **************************************************************************/
        /// <summary>
        /// Terzo responsabile nominato
        /// </summary>
        public POMJ_AnagraficaSoggetti TerzoResponsabile { get; set; }
        /// <summary>
        /// Data inizio assunzione incarico
        /// </summary>
        public System.DateTime? InizioAssunzioneIncarico { get; set; }
        /// <summary>
        /// Data fine assunzione incarico
        /// </summary>
        public System.DateTime? FineAssunzioneIncarico { get; set; }

         /**************************************************************************
         * 
         * 4.0 GENERATORI
         *   4.1 GRUPPI TERMICI O CALDAIE
         * 
         **************************************************************************/
         /// <summary>
         /// Lista dei gruppi termici / caldaie
         /// </summary>
        public List<POMJ_GruppiTermici> GruppiTermiciCaldaie { get; set; }

        /// <summary>
        /// Lista dei gruppi frigo
        /// </summary>
        public List<POMJ_GruppiFrigo> GruppiFrigo { get; set; }
        /// <summary>
        /// Lista dei gruppi vmc
        /// </summary>
        public List<POMJ_GruppiVMC> GruppiVMC { get; set; }
        /// <summary>
        /// Lista dei gruppi vmc
        /// </summary>
        public List<POMJ_SistemiRegolazione> SistemiRegolazione { get; set; }
        
        /**************************************************************************
         * 
         * 4.0 GENERATORI
         * 4.5 SCAMBIATORI DI CALORE DELLA SOTTOSTAZIONE DI TELERISCALDAMENTO / TELERAFFRESCAMENTO
         * 
         **************************************************************************/
        /// <summary>
        /// Lista degli scambiatori
        /// </summary>
        public List<POMJ_ScambiatoreCalore> ScambiatoriCaloreSottostazione { get; set; }

        /**************************************************************************
         * 
         * 4.0 GENERATORI
         *   4.6 COGENERATORI / TRIGENERATORI
         * 
         **************************************************************************/
        /// <summary>
        /// Lista dei Cogeneratori / Trigeneratori
        /// </summary>
        public List<POMJ_Cogeneratore> CogeneratoriTrigeneratori { get; set; }
        
        /**************************************************************************
         * 
         * 4.0 GENERATORI
         *   4.7 CAMPI SOLARI TERMICI
         * 
         **************************************************************************/
        /// <summary>
        /// Lista dei campi solari termici
        /// </summary>
        public List<POMJ_CampoSolareTermico> CampiSolariTermici { get; set; }

        /**************************************************************************
         * 
         * 4.0 GENERATORI
         *   4.8 ALTRI GENERATORI
         * 
         **************************************************************************/
        /// <summary>
        /// Lista di generatori altri
        /// </summary>
        public List<POMJ_AltroGeneratore> AltriGeneratori { get; set; }
        
        /**************************************************************************
         * 
         * 5.0 SISTEMI DI REGOLAZIONE E CONTABILIZZAZIONE
         *   5.1 REGOLAZIONE PRIMARIA (Situazione alla prima installazione o alla ristrutturazione dell’impianto termico)
         * 
         **************************************************************************/
        /// <summary>
        /// indica se se il sistema di regolazione della temperatura regola unicamente 
        /// l‘accensione e lo spegnimento del generatore al raggiungimento di una data temperatura 
        /// (es. nel caso di termostato, cronotermostato)
        /// </summary>
        public bool fSistemaRegolazioneOnOff { get; set; }
        /// <summary>
        /// indica se il sistema di regolazione è composto da una centralina di controllo, integrata nel generatore, 
        /// che agisce direttamente sulla temperatura di mandata all’impianto, fissata una curva climatica
        /// all’interno di una famiglia di curve, in modo da adeguare il regime di funzionamento alle caratteristiche 
        /// specifiche dell’impianto
        /// </summary>
        public bool fSistemaRegolazioneIntegrato { get; set; }
        /// <summary>
        /// indica se il sistema è analogo al sistema di regolazione composto, ma del tipo indipendente rispetto al 
        /// corpo del generatore (presenza di una centralina esterna non integrata nel generatore).
        /// </summary>
        public bool fSistemaRegolazioneIndipendente { get; set; }       
        /// <summary>
        /// presenza di un sistema di regolazione multigradino
        /// </summary>
        public bool fSistemaRegolazioneMultigradino { get; set; }
        /// <summary>
        /// presenza di un sistema di regolazione a Inverter
        /// </summary>
        public bool fSistemaRegolazioneAInverter { get; set; }
        /// <summary>
        /// presenza di un altro sistema di regolazione primaria
        /// </summary>
        public bool fAltroSistemaRegolazionePrimaria { get; set; }
        /// <summary>
        /// descrizione dell'altro sistema di regolazione
        /// </summary>
        public string SistemaRegolazionePrimariaAltro { get; set; }
        /// <summary>
        /// indica la presenza di Valvole di regolazione non incorporate nel generatore (valvole miscelatrici)
        /// </summary>
        public bool fValvoleRegolazione { get; set; }
        /// <summary>
        /// Lista delle valvole di regolazione
        /// </summary>
        public List<POMJ_ValvolaRegolazione> ValvoleRegolazione { get; set; }

        /**************************************************************************
         * 
         * 5.0 SISTEMI DI REGOLAZIONE E CONTABILIZZAZIONE
         *   5.2 REGOLAZIONE SINGOLO AMBIENTE DI ZONA
         * 
         **************************************************************************/
        /// <summary>
        /// <para>Termostato. Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Termostato assente</item>
        /// <item>2	Termostato con controllo ON-OFF</item>
        /// <item>3	Termostato con controllo proporzionale</item>
        /// </list>
        /// </summary>
        [Range(1, 3, ErrorMessage = "Tipologia termostato valori consentiti compreso da 1 a 3")]
        public int? TipologiaTermostatoZona { get; set; }
        /// <summary>
        /// indica se è presente, per gli impianti canalizzati, una modulazione della serranda 
        /// dell’aria esterna in base alla temperatura dell’ambiente
        /// </summary>
        public bool fControlloEntalpico { get; set; }
        /// <summary>
        /// indica se è presente, per gli impianti a tutt’aria a portata variabile, un controllo che provvede 
        /// a variare la quantità immessa adeguandola, istante per istante, alle necessità delle zona
        /// </summary>
        public bool fControlloPortataAriaVariabile { get; set; }
        /// <summary>
        /// indica se sono presenti delle valvole termostatiche sui corpi scaldanti
        /// </summary>
        public bool fValvoleTermostatiche { get; set; }
        /// <summary>
        /// indica se se sono presenti delle valvole a due vie del singolo ambiente
        /// </summary>
        public bool fValvoleDueVie { get; set; }
        /// <summary>
        /// indica se sono presenti delle valvole a tre vie del singolo ambiente
        /// </summary>
        public bool fValvoleTreVie { get; set; }
        /// <summary>
        /// Note sulla regolazione del singolo ambiente
        /// </summary>
        public string NoteRegolazioneSingoloAmbiente { get; set; }

        /**************************************************************************
         * 
         * 5.0 SISTEMI DI REGOLAZIONE E CONTABILIZZAZIONE
         *   5.3 SISTEMI TELEMATICI DI TELELETTURA E TELEGESTIONE
         * 
         **************************************************************************/
        /// <summary>
        /// indica se è presente un sistema remoto per la sola acquisizione e lettura dei dati dell’impianto
        /// </summary>
        public bool fTelelettura { get; set; }
        /// <summary>
        /// indica se è presente un sistema remoto sia per l’acquisizione e lettura che per la 
        /// modifica e la parametrizzazione dei dati dell’impianto
        /// </summary>
        public bool fTelegestione { get; set; }
        /// <summary>
        /// Descrizione del sistema telematico
        /// </summary>
        public string DescrizioneSistemaTelematico { get; set; }

        /**************************************************************************
         * 
         * 5.0 SISTEMI DI REGOLAZIONE E CONTABILIZZAZIONE
         *   5.4 CONTABILIZZAZIONE
         * 
         **************************************************************************/
        /// <summary>
        /// indica la presenza di sistemi di contabilizzazione del calore
        /// </summary>
        public bool fContabilizzazione { get; set; }
        /// <summary>
        /// indica se sono contabilizzati i consumi del Riscaldamento
        /// </summary>
        public bool fContabilizzazioneRiscaldamento { get; set; }
        /// <summary>
        /// indica se sono contabilizzati i consumi del raffreddamento
        /// </summary>
        public bool fContabilizzazioneRaffrescamento { get; set; }
        /// <summary>
        /// indica se sono contabilizzati i consumi dell'acqua calda
        /// </summary>
        public bool fContabilizzazioneAcquaCalda { get; set; }
        /// <summary>
        /// <para>Tipologia del Sistema di Contabilizzazione. Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	diretto</item>
        /// <item>2	indiretto</item>
        /// </list>
        /// </summary>
        [Range(1, 2, ErrorMessage = "Tipologia del Sistema di Contabilizzazione da 1 a 2")]
        public int? TipologiaSistemaContabilizzazione { get; set; }
        /// <summary>
        /// Descrizione del sistema di contabilizzazione
        /// </summary>
        public string DescrizioneSistemaContabilizzazione { get; set; }

        /**************************************************************************
         * 
         * 6.0 SISTEMI DI DISTRIBUZIONE
         *   6.1 TIPO DI DISTRIBUZIONE
         * 
         **************************************************************************/
        /// <summary>
        /// <para>Tipologia del Sistema di Distribuzione. Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Altro</item>
        /// <item>2	Verticale a colonne montanti</item>
        /// <item>3 Orizzontale a zone</item>
        /// <item>4 Canali d'aria</item>
        /// </list>
        /// </summary>
        public List<int> TipologiaSistemaDistribuzione { get; set; }
        /// <summary>
        /// Descrizione di altri sistemi di Ditribuzione
        /// </summary>
        public string TipologiaSistemaDistribuzioneAltro { get; set; }

        /**************************************************************************
         * 
         * 6.0 SISTEMI DI DISTRIBUZIONE
         *   6.2 COIBENTAZIONE RETE DI DISTRIBUZIONE
         * 
         **************************************************************************/
        /// <summary>
        /// indica la presenza o meno della coibentazione della rete di distribuzione
        /// </summary>
        public bool? fCoibentazioneReteDistribuzione { get; set; }
        /// <summary>
        /// Note sulla Coibentazione della Rete di Distribuzione
        /// </summary>
        public string NoteCoibentazioneReteDistribuzione { get; set; }

        /**************************************************************************
         * 
         * 6.0 SISTEMI DI DISTRIBUZIONE
         *   6.3 VASI DI ESPANSIONE
         * 
         **************************************************************************/
         /// <summary>
         /// Lista dei vasi di espansione
         /// </summary>
        public List<POMJ_VasoEspansione> VasiEspansione { get; set; }

        /**************************************************************************
         * 
         * 6.0 SISTEMI DI DISTRIBUZIONE
         *   6.4 POMPE DI CIRCOLAZIONE (se non incorporate nel generatore)
         * 
         **************************************************************************/
        /// <summary>
        /// Lista delle Pompe di Circolazione
        /// </summary>
        public List<POMJ_PompaCircolazione> PompaCircolazione { get; set; }

        /**************************************************************************
         * 
         * 7.0 SISTEMA DI EMISSIONE
         * 
         **************************************************************************/
        /// <summary>
        /// <para>Tipologia del Sistema di Distribuzione. Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Radiatori</item>
        /// <item>2	Termoconvettori</item>
        /// <item>3 Ventilconvettori</item>
        /// <item>4 Pannelli radianti</item>
        /// <item>5 Bocchette</item>
        /// <item>6 Strisce radianti</item>
        /// <item>7 Travi fredde</item>
        /// <item>8 Altro</item>
        /// </list>
        /// </summary>
        public List<int> TipologiaSistemiEmissione { get; set; }
        /// <summary>
        /// Descrizione altro sistema di emissione
        /// </summary>
        public string SistemaEmissioneAltro { get; set; }

        /**************************************************************************
         * 
         * 8.0 SISTEMA DI ACCUMULO
         *   8.1 ACCUMULI (se non incorporati nel gruppo termico o caldaia)
         * 
         **************************************************************************/
         /// <summary>
         /// Lista sistemi di accumulo
         /// </summary>
        public List<POMJ_Accumulo> Accumuli { get; set; }


        /**************************************************************************
         * 
         * 9.0 ALTRI COMPONENTI DELL’IMPIANTO
         *   9.1 TORRI EVAPORATIVE
         * 
         **************************************************************************/
        /// <summary>
        /// Lista torri evaporative
        /// <para>Di norma si tratta di torri di raffreddamento, che tramite uno scambiatore di calore a tubi vengono
        /// utilizzate per raffreddare l’acqua proveniente dal condensatore del gruppo frigorifero (impianti di raffreddamento 
        /// a recupero parziale)</para>
        /// </summary>
        public List<POMJ_TorreEvaporativa> TorriEvaporative { get; set; }

        /**************************************************************************
         * 
         * 9.0 ALTRI COMPONENTI DELL’IMPIANTO
         *   9.2 RAFFREDDATORI DI LIQUIDO (a circuito chiuso)
         * 
         **************************************************************************/
        /// <summary>
        /// Lista raffreddatori di liquido
        /// <para>Si tratta di sistemi a circuito chiuso in cui si effettua un raffreddamento del fluido termovettore, 
        /// ad aria mediante ventilatori. I raffreddatori di liquido vengono utilizzati per dissipare il calore che arriva dalle unità interne raffreddate ad acqua.</para>
        /// </summary>
        public List<POMJ_RaffreddatoreLiquido> RaffreddatoriLiquido { get; set; }

        /**************************************************************************
         * 
         * 9.0 ALTRI COMPONENTI DELL’IMPIANTO
         *   9.3 SCAMBIATORI DI CALORE INTERMEDI (per acqua di superficie o di falda)
         * 
         **************************************************************************/
        /// <summary>
        /// Lista scambiatori intermedi
        /// <para>Si tratta di scambiatori, presenti solo nelle pompe di calore geotermiche e che
        /// effettuano lo scambio termico sfruttando acqua di superficie o di falda</para>
        /// </summary>
        public List<POMJ_ScambiatoreCaloreIntermedio> ScambiatoriCaloreIntermedio { get; set; }

        /**************************************************************************
         * 
         * 9.0 ALTRI COMPONENTI DELL’IMPIANTO
         *   9.4 CIRCUITI INTERRATI A CONDENSAZIONE / ESPANSIONE DIRETTA
         * 
         **************************************************************************/
        /// <summary>
        /// Lista circuiti interrati
        /// <para>Si tratta di circuiti nei quali il terreno e il pavimento radiante fungono rispettivamente da 
        /// evaporatore e da condensatore. Le tubazioni, in prevalenza con sviluppo orizzontale nel sottosuolo, 
        /// veicolano il fluido frigorigeno in un circuito chiuso attraverso la pompa di calore</para>
        /// </summary>
        public List<POMJ_CircuitoInterrato> CircuitiInterrati { get; set; }

        /**************************************************************************
         * 
         * 9.0 ALTRI COMPONENTI DELL’IMPIANTO
         *   9.5 UNITÀ DI TRATTAMENTO ARIA
         * 
         **************************************************************************/
         /// <summary>
         /// Lista Unità trattamento aria
         /// </summary>
        public List<POMJ_UnitaTrattamentoAria> UnitaTrattamentoAria { get; set; }

        /**************************************************************************
         * 
         * 9.0 ALTRI COMPONENTI DELL’IMPIANTO
         *   9.6 RECUPERATORI DI CALORE (aria ambiente)
         * 
         **************************************************************************/
        /// <summary>
        /// Lista recuperatori di calore
        /// <para>Si tratta di dispositivi che effettuano il ricambio di aria ambiente recuperando il calore 
        /// presente in essa tramite uno scambiatore e immettendo dell’aria esterna tramite ventilatori. 
        /// Tali apparecchiature possono essere montate singolarmente o inserire in Unità di trattamento 
        /// aria (UTA) o in sistemi di ventilazione meccanica controllata (VMC</para>
        /// </summary>
        public List<POMJ_RecuperatoreCalore> RecuperatoriCalore { get; set; }


        /**************************************************************************
         * 
         * 14.0 REGISTRAZIONE DEI CONSUMI NEI VARI ESERCIZI
         *   14.1 CONSUMO DI COMBUSTIBILE
         * 
         **************************************************************************/
         /// <summary>
         /// Lista Consumi Combstibile
         /// </summary>
        public List<POMJ_ConsumoCombustibile> ConsumiCombustibile { get; set; }

        /**************************************************************************
         * 
         * 14.0 REGISTRAZIONE DEI CONSUMI NEI VARI ESERCIZI
         *   14.2 CONSUMO ENERGIA ELETTRICA
         * 
         **************************************************************************/
         /// <summary>
         /// Lista consumi energia elettrica
         /// </summary>
        public List<POMJ_ConsumoEnergiaElettrica> ConsumiEnergiaElettrica { get; set; }


        /**************************************************************************
         * 
         * 14.0 REGISTRAZIONE DEI CONSUMI NEI VARI ESERCIZI
         *   14.3 CONSUMO ACQUA DI REINTEGRO NEL CIRCUITO DELL'IMPIANTO TERMICO
         * 
         **************************************************************************/
         /// <summary>
         /// Lista consumi Acqua
         /// </summary>
        public List<POMJ_ConsumoAcqua> ConsumiAcqua { get; set; }


        /**************************************************************************
         * 
         * 14.0 REGISTRAZIONE DEI CONSUMI NEI VARI ESERCIZI
         *   14.4 CONSUMO DI PRODOTTI CHIMICI PER IL TRATTAMENTO ACQUA DEL CIRCUITO DELL'IMPIANTO TERMICO
         * 
         **************************************************************************/
         /// <summary>
         /// Lista consumi Prodotti chimici
         /// </summary>
        public List<POMJ_ConsumoProdottiChimici> ConsumiProdottiChimici { get; set; }
        }

}

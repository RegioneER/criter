using Criter.Anagrafica;
using Criter.Libretto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Criter.Rapporti
{
    /// <summary>
    /// RAPPORTO DI CONTROLLO TECNICO TIPO 3 (SCAMBIATORE) 
    /// </summary>
    public class POMJ_RapportoControlloTecnico_SC
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
        /// Indicare il valore fornito dal fabbricante che è riportato all‘interno 
        /// del libretto di installazione, uso e manutenzione
        /// </summary>
        public decimal? PotenzaTermicaNominaleTotaleMax { get; set; }
        /// <summary>
        /// Codice Catastale
        /// Vedi File CodiciComuni.txt
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
        /// Terzo responsabile
        /// </summary>
        public POMJ_AnagraficaSoggetti TerzoResponsabile { get; set; }
        /**************************************************************************
        * 
        * A. DATI IDENTIFICATIVI - Impresa manutentrice
        * 
        ***************************************************************************/
        /// <summary>
        /// Impresa Manutentrice
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
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? LocaleInstallazioneIdoneo { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? LineeElettricheIdonee { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? StatoCoibentazioniIdonee { get; set; }


        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? CoibentazioniIdonee { get; set; }


        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? TenutaImpiantoIdraulico { get; set; }

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
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>


        /// <summary>
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
        * E. CONTROLLO E VERIFICA ENERGETICA DELLO SCAMBIATORE
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
        /// Indicare il valore di potenza termica nominale totale 
        /// fornito dal fabbricante, espresso in kW.
        /// </summary>
        public decimal? PotenzaTermicaNominale { get; set; }
        /// <summary>
        /// Servizio climatizzazione invernale presente 
        /// booleano
        /// </summary>
        public bool fClimatizzazioneInvernale { get; set; }
        /// <summary>
        /// Servizio climatizzazione estiva presente 
        /// booleano
        /// </summary>
        public bool fClimatizzazioneEstiva { get; set; }
        /// <summary>
        /// Servizio produzione acqua calda sanitaria presente 
        /// booleano
        /// </summary>
        public bool fProduzioneACS { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1 Altro</item>
        /// <item>2	Acqua calda</item>
        /// <item>3 Acqua surriscaldata</item>
        /// <item>4 Acqua surriscaldata</item>
        /// <item>5 Vapore</item>
        /// </list>	
        /// </summary>
        [Range(1, 5, ErrorMessage = "Alimentazione valori consentiti compresi da 1 a 5")]
        public int TipologiaAlimentazione { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1 Altro</item>
        /// <item>2	Acqua calda</item>
        /// <item>4 Vapore</item>
        /// </list>	
        /// </summary>
        [Range(1, 4, ErrorMessage = "Tipologia fluido termovettore in uscita valori consentiti compresi da 1 a 4")]
        public int TipologiaFluidoTermoVettore { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public Nullable<int> PotenzaCompatibileProgetto { get; set; }
        /// <summary>
        /// Dispositivi di regolazione e controllo funzionanti Assenza di 
        /// trafilamenti sulla valvola di rregolazione
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>-1 Non Classificabile</item>
        /// <item>0	No</item>
        /// <item>1 Si</item>
        /// </list>	
        /// </summary>
        public int? AssenzaTrafilamenti { get; set; }

        /**************************************************************************
        * 
        * E. CONTROLLO E VERIFICA ENERGETICA DELLO SCAMBIATORE - VALORI MISURATI
        * 
        ***************************************************************************/
        /// <summary>
        /// Indicare il valore della temperatura esterna (ambiente esterno), rilevato nel momento della verifica, 
        /// espresso in gradi centigradi (°C)
        /// </summary>
        public decimal? TemperaturaEsterna { get; set; }
        /// <summary>
        /// Indicare il valore della temperatura del fluido di mandata, 
        /// rilevato nel momento della verifica, espresso in gradi centigradi (°C)
        /// </summary>
        public decimal? TemperaturaMandataPrimario { get; set; }
        /// <summary>
        /// Indicare il valore della temperatura del fluido in uscita (ritorno), rilevato nel 
        /// momento della verifica, espresso in gradi centigradi (°C)
        /// </summary>
        public decimal? TemperaturaRitornoPrimario { get; set; }
        /// <summary>
        /// Indicare il valore della portata di fluido nel circuito primario rilevato nel momento 
        /// della verifica, espresso in gradi centigradi (°C)
        /// </summary>
        public decimal? PortataFluidoPrimario { get; set; }
        /// <summary>
        /// Indicare il valore della temperatura del fluido di mandata (uscita utente)
        /// del circuito secondario rilevato nel momento della verifica, espresso in gradi centigradi (°C)
        /// </summary>
        public Nullable<decimal> TemperaturaMandataSecondario { get; set; }
        /// <summary>
        /// Indicare il valore della temperatura del fluido in ingresso (entrata scambiatore) del 
        /// circuito secondario rilevato nel momento della verifica, espresso in gradi centigradi (°C)
        /// </summary>
        public Nullable<decimal> TemperaturaRitornoSecondario { get; set; }
        /// <summary>
        /// Indicare il valore della potenza termica nominale, espresso in kW, definito dal fabbricante
        /// </summary>
        public decimal? PotenzaTermica { get; set; }

        /**************************************************************************
        * 
        * F. CHECK-LIST
        * 
        ***************************************************************************/
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	L'adozione di valvole termostatiche sui corpi scaldanti</item>
        /// <item>8 Verifica coerenza tra parametri della curva climatica impostati sulla centralina ed i valori di temperatura ambiente</item>
        /// <item>9 Verifica presenza perdite d'acqua</item>
        /// <item>10 Installazione di adeguato "involucro" di coibentazione per lo scambiatore se non presente</item>
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
        /// booleano
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
        /// AltroTipologiaFluidoTermoVettoreEntrata
        /// </summary>
	    public string AltroTipologiaFluidoTermoVettoreEntrata { get; set; }
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

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Criter.Libretto
{
    /// <summary>
    /// Entità usata per descrivere un prodotto, con unica certificazione e unico numero di matricola, 
    /// comprendente caldaia e bruciatore
    /// <para>Se caldaia e bruciatore sono due prodotti separati si crea un'istanza di POMJ_GruppiTermici, mentre va creata un'istanza di POMJ_Bruciatori</para>
    /// <para> Nel caso di impianti con più gruppi termici o caldaie dovranno essere create tante istanze
    /// quanti sono i singoli gruppi termici</para>
    /// <para>Il sistema assegnerà un numero progressivo ad ogni gruppo termico o caldaia aggiunto (GT01, GT02, GT03, ecc)</para>
    /// </summary>
    public class POMJ_GruppiTermici
    {
        /// <summary>
        /// Data di installazione
        /// </summary>
        public System.DateTime? DataInstallazione { get; set; }
        /// <summary>
        /// Fabbricante
        /// </summary>
        public string Fabbricante { get; set; }
        /// <summary>
        /// Dato desumibile dalla documentazione del produttore
        /// </summary>
        public string Modello { get; set; }
        /// <summary>
        /// Dato desumibile dalla documentazione del produttore
        /// </summary>
        public string Matricola { get; set; }
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
        /// Altro tipo di combustibile
        /// </summary>
        public string CombustibileAltro { get; set; }
        /// <summary>
        /// <list type="bullet">
        /// <item>
        /// 1	Altro
        /// </item>
        /// <item>
        /// 2	Acqua calda
        /// </item>
        /// <item>
        /// 3	Acqua surriscaldata
        /// </item> /// <item>
        /// 4	Vapore
        /// </item>
        /// <item>
        /// 5	Aria
        /// </item>
        /// <item>
        /// 6	Olio Diatermico
        /// </item>
        /// </list>
        /// </summary>
        [Range(1, 6, ErrorMessage = "Tipologia di fluido termovettore valori consentiti compresi da 1 a 6")]
        public int TipologiaFluidoTermoVettore { get; set; }
        /// <summary>
        /// Altro tipo di Fluido non nella fattispecie
        /// </summary>
        public string FluidoTermovettoreAltro { get; set; }
        /// <summary>
        /// Indicare il valore fornito dal fabbricante che è riportato all‘interno del libretto di installazione, uso e manutenzione
        /// </summary>
        public decimal? PotenzaTermicaUtileNominaleKw { get; set; }
        /// <summary>
        /// Indicare il valore fornito dal fabbricante che è 
        /// riportato all‘interno del libretto di installazione, uso e manutenzione
        /// </summary>
        public decimal? RendimentoTermicoUtilePc { get; set; }
        /// <summary>
        /// <list type="bullet">
        /// <item>
        /// 1	Gruppo termico singolo
        /// </item>
        /// <item>
        /// 2	Gruppo termico modulare
        /// </item>
        /// <item>
        /// 3	Tubo/nastro radiante
        /// </item>
        /// <item>
        /// 4	Generatore aria calda
        /// </item>
        /// </list>
        /// </summary>
        [Range(1, 4, ErrorMessage = "Tipologia gruppi termici valori consentiti compresi da 1 a 4")]
        public int TipologiaGruppiTermici { get; set; }
        /// <summary>
        /// Numero analisi fumo previste
        /// </summary>
        public decimal? AnalisiFumoPrevisteNr { get; set; }
        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }
        /// <summary>
        /// Lista dei Bruciatori
        /// </summary>
        public List<POMJ_Bruciatori> Bruciatori { get; set; }
        /// <summary>
        /// Lista dei Recuperatori
        /// </summary>
        public List<POMJ_Recuperatori> Recuperatori { get; set; }

        /// <summary>
        /// Data di dismissione
        /// </summary>
        public string DataDismissione { get; set; }

        /// <summary>
        /// Data di sostituzione
        /// </summary>
        public string DataSostituzione { get; set; }
    }
}
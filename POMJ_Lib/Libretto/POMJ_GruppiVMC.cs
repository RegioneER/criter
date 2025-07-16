using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Criter.Libretto
{
    /// <summary>
    /// Gruppi Frigo. Nel caso di impianti, aventi come sottosistema di generazione, 
    /// più gruppi frigo dovranno essere create tante entità quante 
    /// sono le singole apparecchiature
    /// </summary>
    public class POMJ_GruppiVMC
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
        /// <list type="bullet">
        /// <item> 1 Sola estrazione</item>
        /// <item> 2 Flusso doppio con recupero tramite scambiatore a flussi incrociati</item>
        /// <item> 3 Flusso doppio con recupero termodinamico </item>
        /// <item> 4 Altro</item>
        /// </list>
        /// </summary>
        [Range(1, 4, ErrorMessage = "Tipologia impianto VMC valori consentiti compresi da 1 a 4")]
        public int TipologiaImpiantoVMC { get; set; }
        /// <summary>
        /// Tipologia impianto VMC altro
        /// </summary>
        public string TipologiaImpiantoAltro { get; set; }
        /// <summary>
        /// Indicare il valore fornito dal fabbricante che è riportato all‘interno del libretto di installazione, uso e manutenzione
        /// </summary>
        public decimal? PortataAriaMaxMch { get; set; }
        /// <summary>
        /// Indicare il valore fornito dal fabbricante che è riportato all‘interno del libretto di installazione, uso e manutenzione
        /// </summary>
        public decimal? RendimentoRecuperoCop { get; set; }

        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }
    }
}

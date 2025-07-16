namespace Criter.Libretto
{
    /// <summary>
    /// vasi di espansione non assemblati nel generatore, non riguarda cioè i vasi di espansione 
    /// integrati nelle caldaie domestiche ma quelli tipici delle centrali termiche
    /// </summary>
    public class POMJ_VasoEspansione
    {
        /// <summary>
        /// Data di installazione
        /// </summary>
        public System.DateTime? DataInstallazione { get; set; }
        /// <summary>
        /// Indicare il valore della capacità del vaso espressa in litri (l)
        /// </summary>
        public decimal? CapacitaLt { get; set; }
        /// <summary>
        /// specifica se il vaso è Aperto/Chiuso, mediante selezione della relativa casella. 
        /// <para>Nel caso di selezione dell'opzione Chiuso, verrà chiesto di indicare il valore di 
        /// Pressione di precarica, espresso in bar</para>
        /// </summary>
        public bool fChiuso { get; set; }
        /// <summary>
        ///  Valore di Pressione di precarica
        /// </summary>
        public decimal? PressionePrecaricaBar { get; set; }

        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }
    }
}
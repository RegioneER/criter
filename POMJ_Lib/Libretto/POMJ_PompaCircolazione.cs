namespace Criter.Libretto
{
    /// <summary>
    /// Pompa di Circolazione
    /// </summary>
    public class POMJ_PompaCircolazione
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
        /// indica se trattasi di pompa a giri variabili
        /// </summary>
        public bool? fGiriVariabili { get; set; }
        /// <summary>
        /// Indicare il valore fornito dal fabbricante ed è riportato all‘interno del libretto 
        /// di installazione, uso e manutenzione
        /// </summary>
        public decimal? PotenzaNominaleKw { get; set; }

        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }
    }
}
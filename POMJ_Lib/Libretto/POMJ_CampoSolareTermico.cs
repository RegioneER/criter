namespace Criter.Libretto
{
    /// <summary>
    /// sottosistema di generazione per impianti
    /// </summary>
    public class POMJ_CampoSolareTermico
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
        /// Indicare il numero di collettori che compongono il campo solare
        /// </summary>
        public decimal? CollettoriNum { get; set; }
        /// <summary>
        /// Indicare la superficie totale di apertura del campo solare espressa in metri quadrati (m2)
        /// </summary>
        public decimal? SuperficieTotaleAperturaMq { get; set; }

        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }
    }
}
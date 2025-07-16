namespace Criter.Libretto
{
    /// <summary>
    /// Scambiatore clore intermedio
    /// </summary>
    public class POMJ_ScambiatoreCaloreIntermedio
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
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }
    }
}
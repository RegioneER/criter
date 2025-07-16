namespace Criter.Libretto
{
    /// <summary>
    /// RECUPERATORI/CONDENSATORI LATO FUMI non incorporati nel generatore. 
    /// <para>Tali dispositivi consentono di recuperare parzialmente l‘energia termica che è ancora presente nei fumi che fuoriescono dal generatore aumentando il rendimento del sistema</para>
    /// </summary>
    public class POMJ_Recuperatori
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
        /// Indicare il valore fornito dal fabbricante ed è riportato all‘interno del libretto 
        /// di installazione, uso e manutenzione
        /// </summary>
        public decimal? PortataTermicaNominaleTotaleKw { get; set; }
        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }
    }
}
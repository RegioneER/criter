namespace Criter.Libretto
{
    /// <summary>
    /// Valvola regolazione
    /// </summary>
    public class POMJ_ValvolaRegolazione
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
        /// Numero di Vie
        /// </summary>
        public decimal? VieNum { get; set; }
        /// <summary>
        /// Descrizione del Servo motore
        /// </summary>
        public string Servomotore { get; set; }

        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }
    }
}
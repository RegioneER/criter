namespace Criter.Libretto
{
    /// <summary>
    /// Circuito interrato
    /// </summary>
    public class POMJ_CircuitoInterrato
    {
        /// <summary>
        /// Data di installazione
        /// </summary>
        public System.DateTime? DataInstallazione { get; set; }
        /// <summary>
        /// Lunghezza Circuito
        /// </summary>
        public decimal? LunghezzaCircuitoMt { get; set; }
        /// <summary>
        /// Superfice Scambiatore (in Mq.)
        /// </summary>
        public decimal? SuperficieScambiatoreMq { get; set; }
        /// <summary>
        /// Profondità Installazione (in Mt.)
        /// </summary>
        public decimal? ProfonditaInstallazioneMt { get; set; }

        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }
    }
}
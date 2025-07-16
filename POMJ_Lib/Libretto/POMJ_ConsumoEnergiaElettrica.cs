namespace Criter.Libretto
{
    /// <summary>
    /// Consumo di energia elettrica
    /// </summary>
    public class POMJ_ConsumoEnergiaElettrica
    {
        /// <summary>
        /// Inserire l'anno di esercizio iniziale della stagione di riscaldamento
        /// </summary>
        public int? DataEsercizioStart { get; set; }
        /// <summary>
        /// Inserire l'anno di esercizio finale della stagione di riscaldamento
        /// </summary>
        public int? DataEsercizioEnd { get; set; }
        /// <summary>
        /// Indicare la lettura iniziale del contatore
        /// </summary>
        public decimal? LetturaIniziale { get; set; }
        /// <summary>
        /// Indicare la lettura finale del contatore
        /// </summary>
        public decimal? LetturaFinale { get; set; }
        /// <summary>
        /// Indicare il consumi finale dato dalla differenza tra il valore della lettura iniziale e finale
        /// </summary>
        public decimal ConsumoTotale { get; set; }


    }
}
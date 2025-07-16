using System.ComponentModel.DataAnnotations;

namespace Criter.Libretto
{
    /// <summary>
    /// Consumo Acqua
    /// </summary>
    public class POMJ_ConsumoAcqua
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
        /// Indicare il consumi finale dato dalla differenza tra il
        /// valore della lettura iniziale e finale
        /// </summary>
        public decimal ConsumoTotale { get; set; }
        /// <summary>
        /// Indicare accanto al numero l’unità di misura: per esempio m3 per gas naturale, 
        /// kg oppure l per GPL e combustibili liquidi, kg per i combustibili solidi, 
        /// kWh per teleriscaldamento / teleraffrescamento seguendo il seguente schema
        /// <list type="bullet">
        /// <item>1	m3</item>
        /// <item>2	kg</item>
        /// <item>3	l</item>
        /// <item>4	kWh</item>
        /// </list>
        /// </summary>
        [Range(1, 4, ErrorMessage = "Unità di misura valori consentiti compresi da 1 a 4")]
        public int UnitaMisura { get; set; }
    }
}
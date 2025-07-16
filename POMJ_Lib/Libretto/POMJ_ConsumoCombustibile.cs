using System.ComponentModel.DataAnnotations;

namespace Criter.Libretto
{
    /// <summary>
    /// Consumo combustibile
    /// </summary>
    public class POMJ_ConsumoCombustibile
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
        /// Inserire il quantitativo del combustibile acquistato. 
        /// <para>
        /// Per i combustibili liquidi quantificare in base agli approvvigionamenti effettuati 
        /// ed alle letture di livello del combustibile nei serbatoi.
        /// </para> 
        /// <para>
        /// Per i combustibili gassosi indicare le letture effettive del contatore (quando questo esista).
        /// </para>
        /// </summary>
        public decimal? Acquisti { get; set; }
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
        public decimal Consumo { get; set; }
        /// <summary>
        /// Indicare il combustibile con la seguente codicfica
        /// <list type="bullet">
        /// <item>
        /// 1	Altro
        /// </item>
        /// <item>
        /// 2	Gas naturale
        /// </item>
        /// <item>
        /// 3	Gpl
        /// </item>
        /// <item>
        /// 4	Gasolio
        /// </item>
        /// <item>
        /// 5	Olio combustibile
        /// </item>
        /// <item>
        /// 6	Pellet
        /// </item>
        /// <item>
        /// 7	Legna
        /// </item>
        /// <item>
        /// 8	Cippato
        /// </item>
        /// <item>
        /// 9	Bricchette
        /// </item>
        /// <item>
        ///  10	Biogas
        /// </item>
        /// <item>
        ///  11	Syngas
        /// </item>
        /// <item>
        /// 12	Kerosene
        /// </item>
        /// <item>
        /// 13	Biodiesel
        /// </item>
        /// <item>
        /// 14	Aria propanata
        /// </item>
        /// </list>
        /// </summary>
        [Range(1, 14, ErrorMessage = "Tipologia di combustibile valori consentiti compresi da 1 a 14")]
        public int TipologiaCombustibile { get; set; }
        /// <summary>
        /// Altro combustibile non nella fattispecie
        /// </summary>
        public string CombustibileAltro { get; set; }
        /// <summary>
        /// Unità di misura del combustibile
        /// Seguire la codifica
        /// <list type="bullet">
        /// <item> 1	m3  </item>
        /// <item> 2	kg  </item>
        /// <item> 3	l  </item>
        /// <item> 4	kWh  </item>
        /// </list>
        /// </summary>
        [Range(1, 4, ErrorMessage = "Unità di misura valori consentiti compresi da 1 a 4")]
        public int UnitaMisura { get; set; }

    }
}
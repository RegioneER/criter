namespace Criter.Libretto
{
    /// <summary>
    /// Consumo di prodotti chimici
    /// </summary>
    public class POMJ_ConsumoProdottiChimici
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
        /// Impianto termico interessato al trattamento
        /// booleano
        /// </summary>
        public bool fCircuitoImpiantoTermico { get; set; }
        /// <summary>
        /// Circuito ACS interessato al trattamento
        /// booleano
        /// </summary>
        public bool fCircuitoAcs { get; set; }
        /// <summary>
        /// Altro circuito interessato al trattamento
        /// booleano
        /// </summary>
        public bool fAltriCircuiti { get; set; }
        /// <summary>
        /// Indicare il nome del prodotto usato per il trattamento
        /// </summary>
        public string NomeProdotto { get; set; }
        /// <summary>
        /// Indicare il consumo totale del prodotto utilizzato
        /// </summary>
        public decimal Consumo { get; set; }
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
        public int UnitaMisura { get; set; }
   

    }
}
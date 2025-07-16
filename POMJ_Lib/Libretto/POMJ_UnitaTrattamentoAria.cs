namespace Criter.Libretto
{
    /// <summary>
    /// UNità di trattamento aria
    /// </summary>
    public class POMJ_UnitaTrattamentoAria
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
        /// Indica la potenza del ventilatore di mandata espressa in litri al secondo (l/s); 
        /// tale dato è rinvenibile dalla documentazione di prodotto o dagli eventuali dati di targa
        /// </summary>
        public decimal? PortataVentilatoreMandataLts { get; set; }
        /// <summary>
        /// Indica la portata del ventilatore di ripresa espressa in litri al secondo (l/s); tale
        /// dato è rinvenibile dalla documentazione di prodotto o dagli eventuali dati di targa
        /// </summary>
        public decimal? PortataVentilatoreRipresaLts { get; set; }
        /// <summary>
        /// Indica la potenza del ventilatore di mandata espressa in kW; tale dato è rinvenibile dalla 
        /// documentazione di prodotto o dagli eventuali dati di targa
        /// </summary>
        public decimal? PotenzaVentilatoreMandataKw { get; set; }
        /// <summary>
        /// Indicare la potenza del ventilatore di ripresa espressa in kW; tale dato è rinvenibile
        /// dalla documentazione di prodotto o dagli eventuali dati di targa
        /// </summary>
        public decimal? PotenzaVentilatoreRipresaKw { get; set; }

        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }
    }
}


namespace Criter.Libretto
{
    /// <summary>
    /// Descrizione del Sistema di accumulo
    /// </summary>
    public class POMJ_Accumulo
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
        /// indicare la capacità di accumulo espressa in litri (l)
        /// </summary>
        public decimal? CapacitaLt { get; set; }
        /// <summary>
        /// Indicare se il servizio reso dall’accumulo è di tipo Acqua calda sanitaria
        /// </summary>
        public bool? fAcquaCalda { get; set; }
        /// <summary>
        /// Indicare se il servizio reso dall’accumulo è di tipo Riscaldamento
        /// </summary>
        public bool? fRiscaldamento { get; set; }
        /// <summary>
        /// Indicare se il servizio reso dall’accumulo è di tipo Raffrescamento
        /// </summary>
        public bool? fRaffrescamento { get; set; }
        /// <summary>
        /// Indicare la presenza o assenza di coibentazione
        /// </summary>
        public bool? fCoibentazionePresente { get; set; }

        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }

    }
}
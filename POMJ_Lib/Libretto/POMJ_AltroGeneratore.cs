namespace Criter.Libretto
{
    /// <summary>
    /// Generatore non rientrante nelle tipologie di fattispecie
    /// </summary>
    public class POMJ_AltroGeneratore
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
        /// Tipologia del generatore che non rientra nei casi specifici
        /// </summary>
        public string Tipologia { get; set; } 
        /// <summary>
        /// Potenza utile espressa in KW
        /// </summary>
        public decimal? PotenzaUtileKw { get; set; }

        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }
    }
}
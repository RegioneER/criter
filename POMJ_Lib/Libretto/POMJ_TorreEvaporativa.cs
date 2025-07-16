using System.ComponentModel.DataAnnotations;

namespace Criter.Libretto
{
    /// <summary>
    /// si tratta di torri di raffreddamento, che tramite uno scambiatore di calore a tubi vengono utilizzate per raffreddare l’acqua proveniente dal condensatore del gruppo frigorifero (impianti di raffreddamento a recupero parziale).
    /// </summary>
    public class POMJ_TorreEvaporativa
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
        /// Indicare la capacità di accumulo espressa in litri
        /// </summary>
        public decimal? CapacitaNominaleLt { get; set; }
        /// <summary>
        /// Indicare il numero di ventilatori presenti nella torre
        /// </summary>
        public decimal? VentilatoriNum { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Altro</item>
        /// <item>2	Assiali</item>
        /// <item>3 Centrifughi</item>
        /// </list>
        /// </summary>
        [Range(1, 3, ErrorMessage = "Tipologia di ventilatori valori consentiti compresi da 1 a 3")]
        public int TipologiaVentilatori { get; set; }
        /// <summary>
        /// Descrizione altro tipo di ventilatori
        /// </summary>
        public string TipologiaVentilatoriAltro { get; set; }

        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }
    }
}
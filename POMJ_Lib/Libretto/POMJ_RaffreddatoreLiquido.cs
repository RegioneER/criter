using System.ComponentModel.DataAnnotations;

namespace Criter.Libretto
{
    /// <summary>
    /// Si tratta di sistemi a circuito chiuso in cui si effettua un raffreddamento del fluido termovettore, ad aria mediante ventilatori. I raffreddatori di liquido vengono utilizzati per dissipare il calore che arriva dalle unità interne raffreddate ad acqua
    /// </summary>
    public class POMJ_RaffreddatoreLiquido
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
        /// Indicare il numero di ventilatori presenti nella torre
        /// </summary>
        public decimal? VentilatoriNum { get; set; }
        /// <summary>
        /// <para>Seguire la codifica</para>
        /// <list type="bullet">
        /// <item>1	Altro</item>
        /// <item>2	Assiali</item>
        /// <item>3	Centrifughi</item>
        /// </list>
        /// </summary>
        [Range(1, 4, ErrorMessage = "Tipologia ventilatori valori consentiti compresi da 1 a 3")]
        public int TipologiaVentilatori { get; set; }
        /// <summary>
        /// Altro tipo di ventilatori
        /// </summary>
        public string TipologiaVentilatoriAltro { get; set; }

        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }

    }
}
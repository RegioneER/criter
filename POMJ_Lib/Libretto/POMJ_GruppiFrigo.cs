using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Criter.Libretto
{
    /// <summary>
    /// Gruppi Frigo. Nel caso di impianti, aventi come sottosistema di generazione, 
    /// più gruppi frigo dovranno essere create tante entità quante 
    /// sono le singole apparecchiature
    /// </summary>
    public class POMJ_GruppiFrigo
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
        /// Fluido Frigorigeno
        /// </summary>
        public string FiltroFrigorigeno { get; set; }

        /// <summary>
        /// <list type="bullet">
        /// <item> 1 Ad assorbimento per recupero di calore</item>
        /// <item> 2 Ad assorbimento a fiamma diretta con combustibile</item>
        /// <item> 3 A ciclo di compressione con motore elettrico </item>
        /// <item> 4 A ciclo di compressione con motore endotermico</item>
        /// </list>
        /// </summary>
        [Range(1, 4, ErrorMessage = "Tipologia di macchine frigorifere valori consentiti compresi da 1 a 4")]
        public int TipologiaMacchineFrigorifere { get; set; }

        /// <summary>
        /// Combustibile
        /// </summary>
        public string Combustibile { get; set; }

        /// <summary>
        /// <list type="bullet">
        /// <item> 1 Aria</item>
        /// <item> 2 Acqua</item>
        /// <item> 3 Geotermica </item>
        /// </list>
        /// </summary>
        [Range(1, 3, ErrorMessage = "Sorgente lato esterno valori consentiti compresi da 1 a 3")]
        public int SorgenteLatoEsterno { get; set; }

        /// <summary>
        /// <list type="bullet">
        /// <item> 1 Aria</item>
        /// <item> 2 Acqua</item>
        /// <item> 3 Altro </item>
        /// </list>
        /// </summary>
        [Range(1, 3, ErrorMessage = "Fluido lato utenze valori consentiti compresi da 1 a 3")]
        public int FluidoLatoUtenze { get; set; }
                       
        /// <summary>
        /// Indicare il valore fornito dal fabbricante che è riportato all‘interno del libretto di installazione, uso e manutenzione
        /// </summary>
        public decimal? NumCircuiti { get; set; }
        /// <summary>
        /// Indicare il valore fornito dal fabbricante che è riportato all‘interno del libretto di installazione, uso e manutenzione
        /// </summary>
        public decimal? CoefficienteRiscaldamento { get; set; }
        /// <summary>
        /// Indicare il valore fornito dal fabbricante che è riportato all‘interno del libretto di installazione, uso e manutenzione
        /// </summary>
        public decimal? CoefficienteRaffrescamento { get; set; }
        /// <summary>
        /// Indicare il valore fornito dal fabbricante che è riportato all‘interno del libretto di installazione, uso e manutenzione
        /// </summary>
        public decimal? PotenzaFrigoriferaNominaleKw { get; set; }
        /// <summary>
        /// Indicare il valore fornito dal fabbricante che è riportato all‘interno del libretto di installazione, uso e manutenzione
        /// </summary>
        public decimal? PortataTermicaNominaleKw { get; set; }
        /// <summary>
        /// Indicare il valore fornito dal fabbricante che è riportato all‘interno del libretto di installazione, uso e manutenzione
        /// </summary>
        public decimal? PotenzaFrigoriferaAssorbitaNominaleKw { get; set; }
        /// <summary>
        /// Indicare il valore fornito dal fabbricante che è riportato all‘interno del libretto di installazione, uso e manutenzione
        /// </summary>
        public decimal? PotenzaTermicaAssorbitaNominaleKw { get; set; }
        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }

        /// <summary>
        /// Data di dismissione
        /// </summary>
        public string DataDismissione { get; set; }

        /// <summary>
        /// Data di sostituzione
        /// </summary>
        public string DataSostituzione { get; set; }
    }
}

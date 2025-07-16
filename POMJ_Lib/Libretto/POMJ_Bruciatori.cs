namespace Criter.Libretto
{
    /// <summary>
    /// il bruciatore collegato ai gruppi termici/caldaie che costituiscono un apparecchio 
    /// a sé stante (non incorporato nel generatore) individuato da una propria targhetta e/o 
    /// marcatura CE.
    /// </summary>
    public class POMJ_Bruciatori
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
        /// tipo di bruciatore (atmosferico, pressurizzato, monostadio, pluristadio, modulare, ecc.)
        /// </summary>
        public string Tipologia { get; set; }  
        /// <summary>
        /// Combustibile utilizzato
        /// </summary>
        public string Combustibile { get; set; }
        /// <summary>
        /// Indicare il valore di potenza termica massima nominale fornito dal fabbricante, 
        /// espresso in kW. La potenza nominale max non deve risultare maggiore di quella del 
        /// generatore con cui il bruciatore è collegato.
        /// </summary>
        public decimal? PortataTermicaMaxNominaleKw { get; set; }
        /// <summary>
        /// Indicare il valore di potenza termica minima nominale fornito dal fabbricante, espresso in kW.
        /// </summary>
        public decimal? PortataTermicaMinNominaleKw { get; set; }
        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }
    }
}
namespace Criter.Libretto
{
    /// <summary>
    /// apparecchiature che sfruttano il calore o il freddo proveniente da una rete di te riscaldamento/teleraffrescamento
    /// </summary>
    public class POMJ_SistemiRegolazione
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
        /// Indicare il valore fornito dal fabbricante ed è riportato all‘interno del libretto 
        /// di installazione, uso e manutenzione
        /// </summary>
        public decimal? PuntiRegolazioneNum { get; set; }
        /// <summary>
        /// Indicare il valore fornito dal fabbricante ed è riportato all‘interno del libretto 
        /// di installazione, uso e manutenzione
        /// </summary>
        public decimal? LivelliTemperaturaNum { get; set; }

        /// <summary>
        /// Codice progressivo
        /// </summary>
        public int CodiceProgressivo { get; set; }
    }
}
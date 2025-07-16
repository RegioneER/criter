namespace Criter.Libretto
{
    /// <summary>
    /// Usata per indicare i dati catastali dell'Impianto
    /// 
    /// </summary>
    public class POMJ_DatiCatastali
    {
        /// <summary>
        /// Foglio
        /// <para>Porzione di territorio comunale che il catasto rappresenta nelle proprie mappe 
        /// cartografiche</para>
        /// </summary>
        public string Foglio { get; set; }
        /// <summary>
        /// Mappale
        /// <para>è chiamata anche particella o numero di mappa e rappresenta all’interno del 
        /// foglio, una porzione di terreno, o il fabbricato e l’eventuale area di pertinenza</para>
        /// </summary>
        public string Mappale { get; set; }
        /// <summary>
        /// Subalterno
        /// <para> identifica la singola unità immobiliare esistente su una particella.  
        /// Nell’ipotesi di un intero fabbricato formato da tante unità immobiliari , ciascuna unità immobiliare è 
        /// identificata da un proprio subalterno </para>
        /// </summary>
        public string Subalterno { get; set; }  
        /// <summary>
        /// Identificativo
        /// <para>Il dato catastale</para>
        /// </summary>
        public string Identificativo { get; set; }
        //// <summary>
        //// Codice Sezione
        //// <para>Seguire la codifica</para>
        //// <list type="bullet">
        //// <item>A     Sezione Cesena</item>
        //// <item>B     Sezione Roversano(ROV)</item>
        //// <item>A     Sezione Castrocaro</item>
        //// <item>B     Sezione Sadurano(SAD)</item>
        //// <item>B     Sezione Santa Sofia</item>
        //// <item>A     Sezione Mortano(MOR)</item>
        //// <item>001	Sezione Parma - 001</item>
        //// <item>002	Sezione San Lazzaro - 002</item>
        //// <item>003	Sezione Cortile San Martino - 003</item>
        //// <item>004	Sezione San Pancrazio - 004</item>
        //// <item>005	Sezione Golese - 005</item>
        //// <item>006	Sezione Parma Vigatto - 006</item>
        //// <item>RA    Sezione Ravenna(RA)</item>
        //// <item>S     Sezione Savio(S)</item>
        //// <item>SA    Sezione Sant'Alberto (SA)</item>
        //// <item>A     Sezione Casteldelci</item>
        //// <item>B     Sezione Scavolino</item>
        //// <item>A     Seziona Novafeltria</item>
        //// <item>B     Sezione S.Agata Feltria</item>
        //// <item>A     Seziona Pennabilli</item>
        //// <item>B     Sezione Scavolino</item>
        //// </list>	
        //// </summary>
        //public string CodiceSezione { get; set; }
    }
}
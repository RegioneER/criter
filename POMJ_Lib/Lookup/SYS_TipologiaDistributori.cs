namespace Criter.Lookup
{
    public partial class SYS_TipologiaDistributori
    {   
        public int IDDistributore { get; set; }
        public int IDCodiceCatastale { get; set; }
        public string Distributore { get; set; }
        public string Indirizzo { get; set; }
        public string PartitaIva { get; set; }
        public string EmailPec { get; set; }
        public bool fAttivo { get; set; }
    }
}
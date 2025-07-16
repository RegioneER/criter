namespace Criter.Lookup
{    
    public partial class SYS_TipologiaCombustibile
    {  
        public int IDTipologiaCombustibile { get; set; }
        public string TipologiaCombustibile { get; set; }
        public bool fAttivo { get; set; }
        public bool Biomassa { get; set; }
        public decimal LimiteFisicoCO2 { get; set; }
        public bool Liquido { get; set; }
        public bool Gas { get; set; }
    }
}
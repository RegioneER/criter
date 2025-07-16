using System;

namespace Criter.Lookup
{
    public partial class SYS_CodiciCatastali
    {
        public int IDCodiceCatastale { get; set; }
        public string CodiceCatastale { get; set; }
        public string Comune { get; set; }
        public int? IDProvincia { get; set; }
        public bool fAttivo { get; set; }
        public string ZonaClimatica { get; set; }
        public double? GradiGiorno { get; set; }
        public DateTime? DataTermineBloccoRct { get; set; }
        public string EmailPec { get; set; }
        public string Cap { get; set; }        
    }
}
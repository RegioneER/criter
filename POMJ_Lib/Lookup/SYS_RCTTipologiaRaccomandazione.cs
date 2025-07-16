namespace Criter.Lookup
{   
    public partial class SYS_RCTTipologiaRaccomandazione
    {
        public int IDTipologiaRaccomandazione { get; set; }
        public string Raccomandazione { get; set; }
        public int IDTipologiaRaccomandazionePrescrizioneRct { get; set; }
        public bool fAttivo { get; set; }
    }
}
namespace Criter.Lookup
{   
    public partial class SYS_RCTTipologiaPrescrizione
    {    
        public int IDTipologiaPrescrizione { get; set; }
        public string Prescrizione { get; set; }
        public int IDTipologiaRaccomandazionePrescrizioneRct { get; set; }
        public bool fAttivo { get; set; }
    }
}
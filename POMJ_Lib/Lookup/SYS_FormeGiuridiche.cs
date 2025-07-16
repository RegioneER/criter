namespace Criter.Lookup
{
    public partial class SYS_FormeGiuridiche
    {    
        public int IDFormaGiuridica { get; set; }
        public string FormaGiuridica { get; set; }
        public bool fAttivo { get; set; }
        public int? IDTipoSoggetto { get; set; }
    }
}

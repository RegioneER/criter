//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class SYS_StatoPianificazioneIspettore
    {
        public SYS_StatoPianificazioneIspettore()
        {
            this.VER_IspezioneGruppoVerifica = new HashSet<VER_IspezioneGruppoVerifica>();
        }
    
        public int IDStatoPianificazioneIspettore { get; set; }
        public string StatoPianificazioneIspettore { get; set; }
        public Nullable<bool> fAttivo { get; set; }
    
        public virtual ICollection<VER_IspezioneGruppoVerifica> VER_IspezioneGruppoVerifica { get; set; }
    }
}

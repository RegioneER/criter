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
    
    public partial class SYS_TipologiaIspezioneRapportoCheckList
    {
        public SYS_TipologiaIspezioneRapportoCheckList()
        {
            this.VER_IspezioneRapportoCheckList = new HashSet<VER_IspezioneRapportoCheckList>();
        }
    
        public int IDTipologiaCheckList { get; set; }
        public string TipologiaCheckList { get; set; }
        public bool fAttivo { get; set; }
        public int IDTipoCheckList { get; set; }
    
        public virtual ICollection<VER_IspezioneRapportoCheckList> VER_IspezioneRapportoCheckList { get; set; }
    }
}

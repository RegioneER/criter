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
    
    public partial class SYS_TipologiaRapportoDiControllo
    {
        public SYS_TipologiaRapportoDiControllo()
        {
            this.SYS_RCTTipologiaCheckList = new HashSet<SYS_RCTTipologiaCheckList>();
            this.RCT_RapportoDiControlloTecnicoBase = new HashSet<RCT_RapportoDiControlloTecnicoBase>();
            this.SYS_FasceContributive = new HashSet<SYS_FasceContributive>();
        }
    
        public int IDTipologiaRCT { get; set; }
        public string DescrizioneRCT { get; set; }
        public bool fAttivo { get; set; }
    
        public virtual ICollection<SYS_RCTTipologiaCheckList> SYS_RCTTipologiaCheckList { get; set; }
        public virtual ICollection<RCT_RapportoDiControlloTecnicoBase> RCT_RapportoDiControlloTecnicoBase { get; set; }
        public virtual ICollection<SYS_FasceContributive> SYS_FasceContributive { get; set; }
    }
}

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
    
    public partial class SYS_TipologiaMacchineFrigorifere
    {
        public SYS_TipologiaMacchineFrigorifere()
        {
            this.RCT_RapportoDiControlloTecnicoGF = new HashSet<RCT_RapportoDiControlloTecnicoGF>();
            this.LIM_LibrettiImpiantiMacchineFrigorifere = new HashSet<LIM_LibrettiImpiantiMacchineFrigorifere>();
        }
    
        public int IDTipologiaMacchineFrigorifere { get; set; }
        public string TipologiaMacchineFrigorifere { get; set; }
        public bool fAttivo { get; set; }
    
        public virtual ICollection<RCT_RapportoDiControlloTecnicoGF> RCT_RapportoDiControlloTecnicoGF { get; set; }
        public virtual ICollection<LIM_LibrettiImpiantiMacchineFrigorifere> LIM_LibrettiImpiantiMacchineFrigorifere { get; set; }
    }
}

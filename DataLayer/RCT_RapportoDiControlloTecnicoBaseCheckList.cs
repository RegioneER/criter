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
    
    public partial class RCT_RapportoDiControlloTecnicoBaseCheckList
    {
        public int IDRapportoControlloTecnicoCheckList { get; set; }
        public long IDRapportoControlloTecnico { get; set; }
        public int IDCheckList { get; set; }
    
        public virtual SYS_RCTCheckList SYS_RCTCheckList { get; set; }
        public virtual RCT_RapportoDiControlloTecnicoBase RCT_RapportoDiControlloTecnicoBase { get; set; }
    }
}

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
    
    public partial class COM_RuoliSoggetti
    {
        public int IDRuoliSoggetti { get; set; }
        public int IDSoggetto { get; set; }
        public int IDRuoloSoggetto { get; set; }
    
        public virtual SYS_RuoloSoggetto SYS_RuoloSoggetto { get; set; }
        public virtual COM_AnagraficaSoggetti COM_AnagraficaSoggetti { get; set; }
    }
}

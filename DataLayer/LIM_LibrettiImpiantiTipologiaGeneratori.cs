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
    
    public partial class LIM_LibrettiImpiantiTipologiaGeneratori
    {
        public int IDLibrettoImpiantoTipologiaGeneratori { get; set; }
        public int IDLibrettoImpianto { get; set; }
        public int IDTipologiaGeneratori { get; set; }
        public string TipologiaGeneratoriAltro { get; set; }
    
        public virtual SYS_TipologiaGeneratori SYS_TipologiaGeneratori { get; set; }
        public virtual LIM_LibrettiImpianti LIM_LibrettiImpianti { get; set; }
    }
}

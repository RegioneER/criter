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
    
    public partial class VER_AccertamentoVisitaInfo
    {
        public long IDAccertamentoVisitaInfo { get; set; }
        public long IDAccertamentoProgramma { get; set; }
        public long IDAccertamentoVisita { get; set; }
        public long IDAccertamento { get; set; }
    
        public virtual VER_AccertamentoProgramma VER_AccertamentoProgramma { get; set; }
        public virtual VER_AccertamentoVisita VER_AccertamentoVisita { get; set; }
        public virtual VER_Accertamento VER_Accertamento { get; set; }
    }
}

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
    
    public partial class V_VER_AccertamentiNota
    {
        public long IDAccertamentoNote { get; set; }
        public long IDAccertamento { get; set; }
        public System.DateTime Data { get; set; }
        public string Nota { get; set; }
        public int IDUtente { get; set; }
        public string Utente { get; set; }
        public bool IsSanzione { get; set; }
    }
}

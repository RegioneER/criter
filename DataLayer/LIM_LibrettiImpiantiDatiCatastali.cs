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
    
    public partial class LIM_LibrettiImpiantiDatiCatastali
    {
        public int IDLibrettoImpiantoDatiCatastali { get; set; }
        public int IDLibrettoImpianto { get; set; }
        public Nullable<int> IDCodiceCatastaleSezione { get; set; }
        public string Foglio { get; set; }
        public string Mappale { get; set; }
        public string Subalterno { get; set; }
        public string Identificativo { get; set; }
        public Nullable<int> IDUtenteInserimento { get; set; }
        public Nullable<System.DateTime> DataInserimento { get; set; }
        public Nullable<int> IDUtenteUltimaModifica { get; set; }
        public Nullable<System.DateTime> DataUltimaModifica { get; set; }
        public Nullable<bool> fValidazioneMoka { get; set; }
    
        public virtual SYS_CodiciCatastaliSezioni SYS_CodiciCatastaliSezioni { get; set; }
        public virtual LIM_LibrettiImpianti LIM_LibrettiImpianti { get; set; }
    }
}

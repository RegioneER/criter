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
    
    public partial class VER_AccertamentoDocumento
    {
        public VER_AccertamentoDocumento()
        {
            this.COM_Raccomandate = new HashSet<COM_Raccomandate>();
        }
    
        public long IDAccertamentoDocumento { get; set; }
        public long IDAccertamento { get; set; }
        public int IDProceduraAccertamento { get; set; }
        public bool fFileDepositatoNexive { get; set; }
        public Nullable<System.DateTime> DataDepositoFileNexive { get; set; }
        public bool fRaccomandataInviata { get; set; }
        public string Barcode { get; set; }
        public string Servizio { get; set; }
        public Nullable<System.DateTime> DataAccettazione { get; set; }
        public string FilialeAccettazione { get; set; }
        public Nullable<System.DateTime> DataRecapito { get; set; }
        public Nullable<System.DateTime> DataPostalizzazione { get; set; }
        public Nullable<System.DateTime> DataReso { get; set; }
        public Nullable<int> IDCausale { get; set; }
        public Nullable<decimal> Latitudine { get; set; }
        public Nullable<decimal> Longitudine { get; set; }
    
        public virtual SYS_CausaliRaccomandate SYS_CausaliRaccomandate { get; set; }
        public virtual SYS_ProceduraAccertamento SYS_ProceduraAccertamento { get; set; }
        public virtual VER_Accertamento VER_Accertamento { get; set; }
        public virtual ICollection<COM_Raccomandate> COM_Raccomandate { get; set; }
    }
}

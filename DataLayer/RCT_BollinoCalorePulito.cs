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
    
    public partial class RCT_BollinoCalorePulito
    {
        public RCT_BollinoCalorePulito()
        {
            this.RCT_BollinoCalorePulitoConvertito = new HashSet<RCT_BollinoCalorePulitoConvertito>();
        }
    
        public long IDBollinoCalorePulito { get; set; }
        public System.Guid CodiceBollino { get; set; }
        public Nullable<long> IDRapportoControlloTecnico { get; set; }
        public Nullable<System.DateTime> DataOraUtilizzo { get; set; }
        public int IdLottoBolliniCalorePulito { get; set; }
        public Nullable<int> IDSoggettoDerived { get; set; }
        public Nullable<int> IDSoggettoUtilizzatore { get; set; }
        public bool fBollinoUtilizzato { get; set; }
        public Nullable<decimal> CostoBollino { get; set; }
        public Nullable<System.DateTime> DataDisattivazione { get; set; }
        public bool fAttivo { get; set; }
    
        public virtual RCT_LottiBolliniCalorePulito RCT_LottiBolliniCalorePulito { get; set; }
        public virtual RCT_RapportoDiControlloTecnicoBase RCT_RapportoDiControlloTecnicoBase { get; set; }
        public virtual ICollection<RCT_BollinoCalorePulitoConvertito> RCT_BollinoCalorePulitoConvertito { get; set; }
    }
}

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
    
    public partial class V_LIM_VerifichePeriodicheSC
    {
        public Nullable<long> RowNum { get; set; }
        public long Id { get; set; }
        public int IDLibrettoImpianto { get; set; }
        public int IDTargaturaImpianto { get; set; }
        public int IDSoggetto { get; set; }
        public Nullable<int> IDSoggettoDerived { get; set; }
        public int IDTipologiaControllo { get; set; }
        public int IDStatoRapportoDiControllo { get; set; }
        public Nullable<System.DateTime> DataControllo { get; set; }
        public Nullable<decimal> TemperaturaEsterna { get; set; }
        public Nullable<decimal> TemperaturaMandataPrimario { get; set; }
        public Nullable<decimal> TemperaturaRitornoPrimario { get; set; }
        public Nullable<decimal> TemperaturaMandataSecondario { get; set; }
        public Nullable<decimal> TemperaturaRitornoSecondario { get; set; }
        public Nullable<decimal> PortataFluidoPrimario { get; set; }
        public Nullable<decimal> PotenzaTermica { get; set; }
        public Nullable<int> PotenzaCompatibileProgetto { get; set; }
        public Nullable<int> CoibentazioniIdonee { get; set; }
        public Nullable<int> AssenzaTrafilamenti { get; set; }
        public Nullable<int> IdLIM_LibrettiImpiantiScambaitoriCalore { get; set; }
        public string Prefisso { get; set; }
        public int CodiceProgressivo { get; set; }
    }
}

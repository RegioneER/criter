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
    
    public partial class SYS_Province
    {
        public SYS_Province()
        {
            this.COM_ProvinceCompetenza = new HashSet<COM_ProvinceCompetenza>();
            this.SYS_CodiciCatastali = new HashSet<SYS_CodiciCatastali>();
            this.COM_AnagraficaSoggettiAlbo = new HashSet<COM_AnagraficaSoggettiAlbo>();
            this.LIM_LibrettiImpiantiResponsabili = new HashSet<LIM_LibrettiImpiantiResponsabili>();
            this.COM_AnagraficaSoggetti = new HashSet<COM_AnagraficaSoggetti>();
            this.COM_AnagraficaSoggetti1 = new HashSet<COM_AnagraficaSoggetti>();
            this.COM_AnagraficaSoggetti2 = new HashSet<COM_AnagraficaSoggetti>();
            this.COM_AnagraficaSoggetti3 = new HashSet<COM_AnagraficaSoggetti>();
            this.COM_AnagraficaSoggetti4 = new HashSet<COM_AnagraficaSoggetti>();
        }
    
        public int IDProvincia { get; set; }
        public string SiglaProvincia { get; set; }
        public string Provincia { get; set; }
        public bool fProvinciaCompetenza { get; set; }
        public int IDRegione { get; set; }
        public bool fAttivo { get; set; }
    
        public virtual ICollection<COM_ProvinceCompetenza> COM_ProvinceCompetenza { get; set; }
        public virtual SYS_Regioni SYS_Regioni { get; set; }
        public virtual ICollection<SYS_CodiciCatastali> SYS_CodiciCatastali { get; set; }
        public virtual ICollection<COM_AnagraficaSoggettiAlbo> COM_AnagraficaSoggettiAlbo { get; set; }
        public virtual ICollection<LIM_LibrettiImpiantiResponsabili> LIM_LibrettiImpiantiResponsabili { get; set; }
        public virtual ICollection<COM_AnagraficaSoggetti> COM_AnagraficaSoggetti { get; set; }
        public virtual ICollection<COM_AnagraficaSoggetti> COM_AnagraficaSoggetti1 { get; set; }
        public virtual ICollection<COM_AnagraficaSoggetti> COM_AnagraficaSoggetti2 { get; set; }
        public virtual ICollection<COM_AnagraficaSoggetti> COM_AnagraficaSoggetti3 { get; set; }
        public virtual ICollection<COM_AnagraficaSoggetti> COM_AnagraficaSoggetti4 { get; set; }
    }
}

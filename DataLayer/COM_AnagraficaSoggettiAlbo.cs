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
    
    public partial class COM_AnagraficaSoggettiAlbo
    {
        public int IDSoggettoAlbo { get; set; }
        public int IDSoggetto { get; set; }
        public string Impresa { get; set; }
        public string Indirizzo { get; set; }
        public string Cap { get; set; }
        public string Citta { get; set; }
        public Nullable<int> IDProvincia { get; set; }
        public string AmministratoreDelegato { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string EmailPec { get; set; }
        public string SitoWeb { get; set; }
        public string PartitaIVA { get; set; }
        public bool fAmministratoreDelegato { get; set; }
        public bool fTelefono { get; set; }
        public bool fFax { get; set; }
        public bool fEmail { get; set; }
        public bool fEmailPec { get; set; }
        public bool fSitoWeb { get; set; }
        public bool fPartitaIVA { get; set; }
    
        public virtual SYS_Province SYS_Province { get; set; }
        public virtual COM_AnagraficaSoggetti COM_AnagraficaSoggetti { get; set; }
    }
}

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
    
    public partial class UTE_DatiFornituraCliente
    {
        public UTE_DatiFornituraCliente()
        {
            this.UTE_DatiCatastali = new HashSet<UTE_DatiCatastali>();
        }
    
        public long Id { get; set; }
        public int IdComunicazione { get; set; }
        public int PfPg { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string CfPiva { get; set; }
        public int AnnoRiferimento { get; set; }
        public int NumeroMesiFatturazione { get; set; }
        public string Toponimo { get; set; }
        public string Indirizzo { get; set; }
        public string Civico { get; set; }
        public string Cap { get; set; }
        public string CodiceISTATComune { get; set; }
        public Nullable<int> CodiceAssenzaDatiCatastali { get; set; }
        public string CodicePdrPod { get; set; }
        public int TipoContratto { get; set; }
        public int CategoriaUtilizzo { get; set; }
        public int Combustibile { get; set; }
        public decimal ConsumoAnnuo { get; set; }
        public int UnitaMisuraConsumo { get; set; }
        public Nullable<decimal> VolumetriaRiscaldata { get; set; }
    
        public virtual UTE_Comunicazioni UTE_Comunicazioni { get; set; }
        public virtual ICollection<UTE_DatiCatastali> UTE_DatiCatastali { get; set; }
    }
}

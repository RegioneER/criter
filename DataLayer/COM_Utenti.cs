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
    
    public partial class COM_Utenti
    {
        public COM_Utenti()
        {
            this.VER_AccertamentoNote = new HashSet<VER_AccertamentoNote>();
            this.VER_AccertamentoNonConformita = new HashSet<VER_AccertamentoNonConformita>();
            this.VER_AccertamentoStato = new HashSet<VER_AccertamentoStato>();
            this.VER_AccertamentoStato1 = new HashSet<VER_AccertamentoStato>();
            this.COM_AnagraficaSoggettiAccreditamentoStato = new HashSet<COM_AnagraficaSoggettiAccreditamentoStato>();
            this.VER_IspezioneStato = new HashSet<VER_IspezioneStato>();
            this.VER_AccertamentoProgramma = new HashSet<VER_AccertamentoProgramma>();
            this.VER_AccertamentoVisita = new HashSet<VER_AccertamentoVisita>();
            this.VER_Accertamento = new HashSet<VER_Accertamento>();
            this.VER_Accertamento1 = new HashSet<VER_Accertamento>();
            this.VER_Accertamento2 = new HashSet<VER_Accertamento>();
        }
    
        public int IDUtente { get; set; }
        public int IDSoggetto { get; set; }
        public int IDRuolo { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> DataUltimaModificaPassword { get; set; }
        public Nullable<System.DateTime> DataScadenzaPassword { get; set; }
        public bool fVerificaCredenziali { get; set; }
        public string CodiceVerificaCredenziali { get; set; }
        public string Salt { get; set; }
        public Nullable<System.DateTime> DataUltimoAccesso { get; set; }
        public Nullable<System.DateTime> DataPrimoLogonFallito { get; set; }
        public int NrTentativiFalliti { get; set; }
        public bool fAttivo { get; set; }
        public bool fBloccato { get; set; }
        public bool fSpid { get; set; }
        public string KeyApi { get; set; }
    
        public virtual SYS_UserRole SYS_UserRole { get; set; }
        public virtual ICollection<VER_AccertamentoNote> VER_AccertamentoNote { get; set; }
        public virtual ICollection<VER_AccertamentoNonConformita> VER_AccertamentoNonConformita { get; set; }
        public virtual ICollection<VER_AccertamentoStato> VER_AccertamentoStato { get; set; }
        public virtual ICollection<VER_AccertamentoStato> VER_AccertamentoStato1 { get; set; }
        public virtual ICollection<COM_AnagraficaSoggettiAccreditamentoStato> COM_AnagraficaSoggettiAccreditamentoStato { get; set; }
        public virtual ICollection<VER_IspezioneStato> VER_IspezioneStato { get; set; }
        public virtual COM_AnagraficaSoggetti COM_AnagraficaSoggetti { get; set; }
        public virtual ICollection<VER_AccertamentoProgramma> VER_AccertamentoProgramma { get; set; }
        public virtual ICollection<VER_AccertamentoVisita> VER_AccertamentoVisita { get; set; }
        public virtual ICollection<VER_Accertamento> VER_Accertamento { get; set; }
        public virtual ICollection<VER_Accertamento> VER_Accertamento1 { get; set; }
        public virtual ICollection<VER_Accertamento> VER_Accertamento2 { get; set; }
    }
}

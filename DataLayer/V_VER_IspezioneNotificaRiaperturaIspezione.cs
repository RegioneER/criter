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
    
    public partial class V_VER_IspezioneNotificaRiaperturaIspezione
    {
        public int IDNotificaRiaperturaIspezione { get; set; }
        public long IDIspezione { get; set; }
        public string NotificaAdIspettore { get; set; }
        public System.DateTime DataNotifica { get; set; }
        public int IDUtenteUltimaModifica { get; set; }
        public string Utente { get; set; }
    }
}

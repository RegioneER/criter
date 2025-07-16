using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayerLib.Riversamento.FlussoRiversamento;
using PayerLib.Riversamento.RichiestaPagamentoTelematico;

namespace PayerLib.Riversamento
{
    /// <summary>
    /// Questa classe corrisponde al Record Versamento – Bollettino Premarcato - ES. nome file IVP000ER00RER9998820160331002.txt
    /// </summary>

    public class RecordVersamentoPayer
    {
        
        //public string ProgressivoCaricamento { get; set; }
        public int ProgressivoSelezione { get; set; }
        public decimal Importo { get; set; }
        public string NumeroDocumento { get; set; }
        public string CodiceFiscaleContribuente { get; set; }
        public Guid? IDOrdine { get; set; }
        public ctRicevutaTelematica RicevutaTelematicaPagoPA { get; set; }
    }
}

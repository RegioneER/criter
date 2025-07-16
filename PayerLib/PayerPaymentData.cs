using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PayerLib
{

    [XmlRoot(ElementName = "PaymentData")]
    public class PayerPaymentData
    {
        [XmlElement(ElementName = "PortaleID")]
        public string PortaleID { get; set; }
        [XmlElement(ElementName = "NumeroOperazione")]
        public string NumeroOperazione { get; set; }
        [XmlElement(ElementName = "IDOrdine")]
        public string IDOrdine { get; set; }
        [XmlElement(ElementName = "DataOraOrdine")]
        public string DataOraOrdine { get; set; }
        [XmlElement(ElementName = "IDTransazione")]
        public string IDTransazione { get; set; }
        [XmlElement(ElementName = "DataOraTransazione")]
        public string DataOraTransazione { get; set; }
        [XmlElement(ElementName = "SistemaPagamento")]
        public string SistemaPagamento { get; set; }
        [XmlElement(ElementName = "SistemaPagamentoD")]
        public string SistemaPagamentoD { get; set; }
        [XmlElement(ElementName = "CircuitoAutorizzativo")]
        public string CircuitoAutorizzativo { get; set; }
        [XmlElement(ElementName = "CircuitoAutorizzativoD")]
        public string CircuitoAutorizzativoD { get; set; }
        [XmlElement(ElementName = "ImportoTransato")]
        public string ImportoTransato { get; set; }
        [XmlElement(ElementName = "ImportoCommissioni")]
        public string ImportoCommissioni { get; set; }
        [XmlElement(ElementName = "ImportoCommissioniEnte")]
        public string ImportoCommissioniEnte { get; set; }
        [XmlElement(ElementName = "Esito")]
        public string Esito { get; set; }
        [XmlElement(ElementName = "EsitoD")]
        public string EsitoD { get; set; }
        [XmlElement(ElementName = "Autorizzazione")]
        public string Autorizzazione { get; set; }
        [XmlElement(ElementName = "DatiSpecifici")]
        public string DatiSpecifici { get; set; }
    }
   
}

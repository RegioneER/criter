using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayerLib
{
    public enum  PayerEsitoTransazione
    {
        OK = 1, //Successo
        KO, //Autorizzazione negata dal circuito
        OP, //Transazione pending
        UK //Transazione non presente
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CriterAPI
{
    public enum ResultCode
    {
        LibrettoInseritoCorrettamente,
        RapportoInseritoCorrettamente,
        InserimentoNonAndatoABuonFine,
        LibrettoNonTrovato,
        CodiceImpiantoNonCorretto

    };
}
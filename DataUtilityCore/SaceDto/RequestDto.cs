using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUtilityCore.SaceDto
{
    public class RequestLibrettoImpiantoDto
    {
        public string CodiceTargaturaImpianto { get; set; }

        public TipoGeneratore TipoGeneratore { get; set; }

        public string CodiceCatastale { get; set; }

        public string Indirizzo { get; set; }

        public string NumeroCivico { get; set; }

        public string MatricolaGeneratore { get; set; }

        public string CodicePod { get; set; }
        public string CodicePdr { get; set; }
        public string CfPIvaResponsabile { get; set; }
    }

    public enum TipoGeneratore
    {
        GT = 1,
        GF = 2,
        SC = 3,
        CG = 4
    }


    public class RequestPodPdrDto
    {
        public string CodiceIstat { get; set; }

        public string Indirizzo { get; set; }

        public string NumeroCivico { get; set; }

        public string Cap { get; set; }

        public virtual ICollection<DatiCatastaliDto> DatiCatastali { get; set; } = new List<DatiCatastaliDto>();
    }

    public class DatiCatastaliDto
    {
        public string Foglio { get; set; }

        public string Mappale { get; set; }

        public string Subalterno { get; set; }
    }
}

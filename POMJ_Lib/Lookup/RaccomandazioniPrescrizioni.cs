using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criter.Lookup
{
    public class RaccomandazioniPrescrizioni
    {
        public int ID { get; set; }

        public int TipoRct { get; set; }
        public int IDCampoRct { get; set; }
        public string CampoRct { get; set; }
        public string TipoNonConformita { get; set; }

        public int IDNonConformita { get; set; }

        public string NonConformita { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DataLayer;
using DataUtilityCore.Enum;

namespace DataUtilityCore
{
    public partial class FasceContribuzione
    {
        [Key]

        public int IdFasciaContributiva { get; set; }

        public int NumeroBollini { get; set; }

        public RCT_TipoRapportoDiControlloTecnico TipoRapporto { get; set; }

        public decimal PotenzaMassima { get; set; }
    }
}

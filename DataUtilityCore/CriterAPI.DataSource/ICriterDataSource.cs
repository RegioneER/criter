using Criter.Libretto;
using Criter.Rapporti;
using Criter.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CriterAPI.DataSource
{
    public interface ICriterDataSource
    {
        POMJ_LibrettoImpianto GetLibrettoByCodiceImpianto(string codiceImpianto);
        POMJ_RapportoControlloTecnico_GT GetRapportoTecnico_GT_ByID(int idRapporto);
        POMJ_RapportoControlloTecnico_CG GetRapportoTecnico_CG_ByID(int iD_RapportoTecnico);
        POMJ_RapportoControlloTecnico_GF GetRapportoTecnico_GF_ByID(int iD_RapportoTecnico);
        POMJ_RapportoControlloTecnico_SC GetRapportoTecnico_SC_ByID(int iD_RapportoTecnico);
        POMJ_Login GetLoginCriter(string username, string password);

    }
}
using Criter.Anagrafica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criter.Login
{
    public class POMJ_Login
    {
        /// <summary>
        /// Esito del Login
        /// </summary>

        public bool esitoLogin { get; set; }

        /// <summary>
        /// Status del login
        /// </summary>
        public string statusLogin { get; set; }

        /// <summary>
        /// Chiave assegnata alle aziende manutentrici per le chiamate API
        /// </summary>
        public string criterApiKeySoggetto { get; set; }

        /// <summary>
        /// Informazioni utente
        /// </summary>
        public POMJ_AnagraficaSoggetti userData { get; set; }

    }
}

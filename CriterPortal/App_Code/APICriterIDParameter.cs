using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CriterAPI
{
    /// <summary>
    /// Summary description for APICriterIDParameter
    /// </summary>
    public class APICriterIDParameter
    {
        /// <summary>
        /// Chiave assegnata per le chiamate API
        /// </summary>
        public string CriterAPIKey { get; set; }
        /// <summary>
        /// ID di ricerca
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Codice soggetto
        /// </summary>
        public string CodiceSoggetto { get; set; }

        /// <summary>
        /// Codice targatura impianto
        /// </summary>
        public string CodiceTargatura { get; set; }

        /// <summary>
        /// Codice Pod
        /// </summary>
        public string CodicePod { get; set; }

        /// <summary>
        /// Codice Pdr
        /// </summary>
        public string CodicePdr { get; set; }

        /// <summary>
        /// Chiave assegnata alle aziende manutentrici per le chiamate API
        /// </summary>
        public string CriterAPIKeySoggetto { get; set; }

        /// <summary>
        /// Table name of SYS tables
        /// </summary>
        public string SysTableName { get; set; }

        /// <summary>
        /// Active records of SYS tables
        /// </summary>
        public string OnlyActive { get; set; }

        /// <summary>
        /// Username Criter
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// Password Criter
        /// </summary>
        public string password { get; set; }
    }
}
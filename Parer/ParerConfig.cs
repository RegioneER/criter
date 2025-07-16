using System.Configuration;

namespace Pomiager.Service.Parer
{
    public static class ParerConfig
    {
        public static string ReadSetting(string key)
        {
            var appSettings = new AssemblySettings();
            return appSettings[key] ?? "Chiave di settings '" + key + "' non trovata!";
        }

        public static string ParerEnabled
        {
            get
            {
                return ReadSetting("ParerEnabled") != string.Empty ? ReadSetting("ParerEnabled") : ConfigurationManager.AppSettings["ParerEnabled"];
            }
        }

        public static string ParerSystemSender
        {
            get
            {
                return ReadSetting("ParerSystemSender") != string.Empty ? ReadSetting("ParerSystemSender") : ConfigurationManager.AppSettings["ParerSystemSender"]; //Sace o Criter 
            }
        }

        #region Configurazione WS Parer
        public static string ParerVersion
        {
            get
            {
                return ReadSetting("ParerVersion") != string.Empty ? ReadSetting("ParerVersion") : ConfigurationManager.AppSettings["ParerVersion"];
            }
        }

        public static string ParerProfiloNormativoVersion
        {
            get
            {
                return ReadSetting("ParerProfiloNormativoVersion") != string.Empty ? ReadSetting("ParerProfiloNormativoVersion") : ConfigurationManager.AppSettings["ParerProfiloNormativoVersion"];
            }
        }


        public static string ParerEndPoint
        {
            get
            {
                return ReadSetting("ParerEndPoint") != string.Empty ? ReadSetting("ParerEndPoint") : ConfigurationManager.AppSettings["ParerEndPoint"];
            }
        }
        public static string ParerUsername
        {
            get
            {
                return ReadSetting("ParerUsername") != string.Empty ? ReadSetting("ParerUsername") : ConfigurationManager.AppSettings["ParerUsername"];
            }
        }
        public static string ParerPassword
        {
            get
            {
                return ReadSetting("ParerPassword") != string.Empty ? ReadSetting("ParerPassword") : ConfigurationManager.AppSettings["ParerPassword"];
            }
        }
        #endregion

        #region Configurazione Parametri Xml che ci fornisce Parer 
        public static string ParerAmbiente
        {
            get
            {
                return ReadSetting("ParerAmbiente") != string.Empty ? ReadSetting("ParerAmbiente") : ConfigurationManager.AppSettings["ParerAmbiente"];
            }
        }

        public static string ParerEnte
        {
            get
            {
                return ReadSetting("ParerEnte") != string.Empty ? ReadSetting("ParerEnte") : ConfigurationManager.AppSettings["ParerEnte"];
            }
        }

        public static string ParerStruttura
        {
            get
            {
                return ReadSetting("ParerStruttura") != string.Empty ? ReadSetting("ParerStruttura") : ConfigurationManager.AppSettings["ParerStruttura"];
            }
        }

        public static string ParerTipoDocumento
        {
            get
            {
                return ReadSetting("ParerTipoDocumento") != string.Empty ? ReadSetting("ParerTipoDocumento") : ConfigurationManager.AppSettings["ParerTipoDocumento"];
            }
        }

        #endregion

    }
}

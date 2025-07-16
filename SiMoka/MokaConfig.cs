using System.Configuration;

namespace SiMoka
{
    public static class MokaConfig
    {
        public static string SigmaterWsUrlConsultazioneUIU
        {
            get
            {
                return ConfigurationManager.AppSettings["SigmaterWsUrlConsultazioneUIU"];
            }
        }
        public static string SigmaterWsUsername
        {
            get
            {
                return ConfigurationManager.AppSettings["SigmaterWsUsername"];
            }
        }
        public static string SigmaterWsPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["SigmaterWsPassword"];
            }
        }

    }
}
